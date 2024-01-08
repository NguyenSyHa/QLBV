using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace QLBV.FormThamSo
{
    public partial class frm_XemTB : DevExpress.XtraEditors.XtraForm
    {
        public frm_XemTB()
        {
            InitializeComponent();
        }
        public delegate void getString(int SoTBNew);
        public getString getdata;
        private void labelControl1_Click(object sender, EventArgs e)
        {

        }
        public class NDThongBao
        {
            public int ID { get; set; }
            public string MaCB { get; set; }
            public string TenCB { get; set; }
            public DateTime NgayNhap { get; set; }
            public string NoiDung { get; set; }
        }
        List<NDThongBao> _lNDNew = new List<NDThongBao>();
        List<NDThongBao> _lNDOld = new List<NDThongBao>();
        List<CanBo> _lCB = new List<CanBo>();
        QLBV_Database.QLBVEntities data;
        private void frm_XemTB_Load(object sender, EventArgs e)
        {
            _lNDOld.Clear();
            _lNDNew.Clear();
            string _macb = ";" + DungChung.Bien.MaCB + ";";
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            _lCB = data.CanBoes.Where(p => p.Status == 1).ToList();
            _lCB.Add(new CanBo { MaCB = "tatca", TenCB = "Tất cả" });
            lupcanbo.Properties.DataSource = _lCB.OrderBy(p => p.TenCB);
            lupcanbo.EditValue = "tatca";
            dttungay.DateTime = DateTime.Now;
            dtdenngay.DateTime = DateTime.Now;
            var _lTb = data.ThongBaos.Where(p => p.Status == 1).Where(p => DungChung.Bien.PLoaiKP == DungChung.Bien.st_PhanLoaiKP.Admin ? true : (p.PLoai == "Toàn viện" || p.PLoai == DungChung.Bien.PLoaiKP)).ToList();
            foreach (var item in _lTb)
            {
                if (item.DSCBDaXem != null && item.DSCBDaXem.Contains(_macb))
                {
                    //NDThongBao old = new NDThongBao();
                    //old.NgayNhap = Convert.ToDateTime(item.NgayNhap);
                    //var tencb = _lCB.Where(p => p.MaCB == item.MaCB).Select(p => p.TenCB).FirstOrDefault();
                    //if (tencb != null)
                    //    old.TenCB = tencb;
                    //old.NoiDung = item.NDung;
                    //old.ID = item.ID;
                    //old.MaCB = item.MaCB;
                    //_lNDOld.Add(old);
                }
                else
                {
                    NDThongBao moi = new NDThongBao();
                    moi.NgayNhap = Convert.ToDateTime(item.NgayNhap);
                    var tencb = _lCB.Where(p => p.MaCB == item.MaCB).Select(p => p.TenCB).FirstOrDefault();
                    if (tencb != null)
                        moi.TenCB = tencb;
                    moi.NoiDung = item.NDung;
                    moi.ID = item.ID;
                    moi.MaCB = item.MaCB;
                    _lNDNew.Add(moi);
                }
            }
           
            //grcTBCu.DataSource = _lNDOld.OrderBy(p => p.NgayNhap).ToList();
            grcTBMoi.DataSource = null;
            grcTBMoi.DataSource = _lNDNew.OrderBy(p => p.NgayNhap).ToList();

        }
        void setdaxem(QLBV_Database.QLBVEntities data, int ID)
        {
            ThongBao sua = data.ThongBaos.Where(p => p.ID == ID).FirstOrDefault();
            if (sua != null)
            {
                string cb = ";" + DungChung.Bien.MaCB + ";";
                if (sua.DSCBDaXem!=null && sua.DSCBDaXem.Contains(cb))
                {

                }
                else
                {
                    sua.DSCBDaXem += cb;
                    data.SaveChanges();
                }
            }
        }

        private void grvTBMoi_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {

            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            if (grvTBMoi.GetFocusedRowCellValue(colID1) != null)
            {
                int ID = Convert.ToInt32(grvTBMoi.GetFocusedRowCellValue(colID1));
                if (grvTBMoi.GetFocusedRowCellValue(colNgayNhap1) != null)
                    deNgayNhap.DateTime = Convert.ToDateTime(grvTBMoi.GetFocusedRowCellValue(colNgayNhap1));
                if (grvTBMoi.GetFocusedRowCellValue(colNDung1) != null)
                    mmNDung.Text = grvTBMoi.GetFocusedRowCellValue(colNDung1).ToString();
                else
                    mmNDung.Text = "";
                if (grvTBMoi.GetFocusedRowCellValue(colCBNhap1) != null)
                    txtTenCB.Text = grvTBMoi.GetFocusedRowCellValue(colCBNhap1).ToString();
                else
                    txtTenCB.Text = "";
                setdaxem(data, ID);
            }
            else
                reset();

        }

        private void grvTBCu_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (grvTBCu.GetFocusedRowCellValue(colID) != null)
            {
                int ID = Convert.ToInt32(grvTBCu.GetFocusedRowCellValue(colID));
                if (grvTBCu.GetFocusedRowCellValue(colNgayNhap2) != null)
                    deNgayNhap.DateTime = Convert.ToDateTime(grvTBCu.GetFocusedRowCellValue(colNgayNhap2));
                if (grvTBCu.GetFocusedRowCellValue(colNDung2) != null)
                    mmNDung.Text = grvTBCu.GetFocusedRowCellValue(colNDung2).ToString();
                else
                    mmNDung.Text = "";
                if (grvTBCu.GetFocusedRowCellValue(colCBTB2) != null)
                    txtTenCB.Text = grvTBCu.GetFocusedRowCellValue(colCBTB2).ToString();
                else
                    txtTenCB.Text = "";
            }
            else
                reset();
        }
        void reset()
        {
            deNgayNhap.DateTime = DateTime.Now;
            mmNDung.Text = "";
            txtTenCB.Text = "";
        }
        void TimKiem()
        {
            _lNDOld.Clear();
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            DateTime _tungay = DungChung.Ham.NgayTu(dttungay.DateTime);
            DateTime _denngay = DungChung.Ham.NgayDen(dtdenngay.DateTime);
            string _macbTB = "";
            string _macb = ";" + DungChung.Bien.MaCB + ";";
            if (lupcanbo.EditValue != null)
                _macbTB = lupcanbo.EditValue.ToString();
            var _ltbold = data.ThongBaos.Where(p => p.NgayNhap >= _tungay && p.NgayNhap <= _denngay).Where(p => p.DSCBDaXem != null && p.DSCBDaXem.Contains(_macb)).Where(p => _macbTB == "tatca" ? true : p.MaCB == _macbTB).ToList();
            foreach (var item in _ltbold)
            {
                 NDThongBao moi = new NDThongBao();
                 moi.NgayNhap = Convert.ToDateTime(item.NgayNhap);
                 var tencb = _lCB.Where(p => p.MaCB == item.MaCB).Select(p => p.TenCB).FirstOrDefault();
                 if (tencb != null)
                     moi.TenCB = tencb;
                 moi.NoiDung = item.NDung;
                 moi.ID = item.ID;
                 moi.MaCB = item.MaCB;
                 _lNDOld.Add(moi);
            }
            grcTBCu.DataSource = null;
            grcTBCu.DataSource = _lNDOld;
        }

        private void frm_XemTB_FormClosed(object sender, FormClosedEventArgs e)
        {
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var _lTb = data.ThongBaos.Where(p => p.Status == 1).Where(p => DungChung.Bien.PLoaiKP == DungChung.Bien.st_PhanLoaiKP.Admin ? true : p.PLoai == "Toàn viện" || p.PLoai == DungChung.Bien.PLoaiKP).ToList();
            int dem = 0;
            string _macb = ";" + DungChung.Bien.MaCB + ";";
            foreach (var item in _lTb)
            {
                if (item.DSCBDaXem != null && item.DSCBDaXem.Contains(_macb))
                {

                }
                else
                {
                    dem++;
                }
            }
            getdata(dem);
        }

        private void dttungay_EditValueChanged(object sender, EventArgs e)
        {
            TimKiem();
        }

        private void dtdenngay_EditValueChanged(object sender, EventArgs e)
        {
            TimKiem();
        }

        private void lupcanbo_EditValueChanged(object sender, EventArgs e)
        {
            TimKiem();
        }
    }
}