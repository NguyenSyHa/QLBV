using DevExpress.Data;
using DevExpress.XtraGrid.Columns;
using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QLBV.ChucNang
{
    public partial class frm_ChiDinh_TongHop : Form
    {
        int maBNhan;
        public frm_ChiDinh_TongHop(int _maBNhan)
        {
            InitializeComponent();
            this.maBNhan = _maBNhan;
        }

        private void frm_ChiDinh_TongHop_Load(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            LoadDataToGrid(dataContext, maBNhan);
        }

        private void LoadDataToGrid(QLBV_Database.QLBVEntities dataContext, int maBNhan)
        {
            var chiDinhByCode = (from cl in dataContext.CLS.Where(p => p.MaBNhan == maBNhan)
                                 join chidinh in dataContext.ChiDinhs on cl.IdCLS equals chidinh.IdCLS
                                 join dv in dataContext.DichVus on chidinh.MaDV equals dv.MaDV
                                 join tn in dataContext.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                                 group cl by new { chidinh.MaDV, dv.TenDV, tn.TenRG } into kq
                                 select new
                                 {
                                     kq.Key.TenDV,
                                     ListNgayThang = kq.Select(o => o.NgayThang),
                                     kq.Key.MaDV,
                                     kq.Key.TenRG,
                                     SoLuong = kq.Count()
                                 }).ToList();

            List<DateTime> listNgay = new List<DateTime>();
            foreach (var item in chiDinhByCode)
            {
                listNgay.AddRange(item.ListNgayThang.Select(o => o.Value.Date).ToList());
            }
            listNgay = listNgay.Distinct().OrderBy(o => o).ToList();
            List<dynamic> listChiDinh = new List<dynamic>();
            List<GridColumn> listColumns = new List<GridColumn>();
            foreach (var item in chiDinhByCode)
            {
                dynamic chiDinhTongHop = new ExpandoObject();
                ((IDictionary<String, Object>)chiDinhTongHop)["MaDV"] = item.MaDV ?? 0;
                ((IDictionary<String, Object>)chiDinhTongHop)["TenDV"] = item.TenDV;
                ((IDictionary<String, Object>)chiDinhTongHop)["SoLuong"] = item.SoLuong;
                ((IDictionary<String, Object>)chiDinhTongHop)["TenRG"] = item.TenRG;
                int i = 0;
                foreach (var item1 in listNgay)
                {
                    i++;
                    string nameProp = "Ngay" + i;
                    bool check = false;
                    if (item.ListNgayThang.ToList().Exists(o => o.Value.Date == item1))
                        check = true;
                    ((IDictionary<String, Object>)chiDinhTongHop)[nameProp] = check;
                    GridColumn column = new GridColumn();
                    column.ColumnEdit = repositoryItemCheckEdit_Enable;
                    column.FieldName = nameProp;
                    column.Visible = true;
                    column.Width = 70;
                    column.Caption = item1.ToString("dd/MM/yyyy");
                    column.Tag = item1;
                    listColumns.Add(column);
                }
                listChiDinh.Add(chiDinhTongHop);
            }

            var nameColume = listColumns.Select(o => o.FieldName).Distinct();

            listColumns = listColumns.Distinct().OrderBy(o => o.Tag).ToList();
            foreach (var item in nameColume)
            {
                gridViewChiDinhTongHop.Columns.Add(listColumns.FirstOrDefault(o => o.FieldName == item));
            }

            gridControlChiDinhTongHop.BeginUpdate();
            gridControlChiDinhTongHop.DataSource = listChiDinh;
            gridControlChiDinhTongHop.EndUpdate();

        }

        private void gridViewChiDinhTongHop_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            if (e.IsGetData && e.Column.UnboundType == UnboundColumnType.Object)
            {
                if (e.Column.FieldName == "STT")
                {
                    e.Value = e.ListSourceRowIndex + 1;
                }
            }
        }

        //public class DynamicClass : DynamicObject
        //{
        //    private Dictionary<string, KeyValuePair<Type, object>> _fields;

        //    public DynamicClass(List<Field> fields)
        //    {
        //        _fields = new Dictionary<string, KeyValuePair<Type, object>>();
        //        fields.ForEach(x => _fields.Add(x.FieldName,
        //            new KeyValuePair<Type, object>(x.FieldType, null)));
        //    }

        //    public override bool TrySetMember(SetMemberBinder binder, object value)
        //    {
        //        if (_fields.ContainsKey(binder.Name))
        //        {
        //            var type = _fields[binder.Name].Key;
        //            if (value.GetType() == type)
        //            {
        //                _fields[binder.Name] = new KeyValuePair<Type, object>(type, value);
        //                return true;
        //            }
        //            else throw new Exception("Value " + value + " is not of type " + type.Name);
        //        }
        //        return false;
        //    }

        //    public override bool TryGetMember(GetMemberBinder binder, out object result)
        //    {
        //        result = _fields[binder.Name].Value;
        //        return true;
        //    }
        //}
    }
}
