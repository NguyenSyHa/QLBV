using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;

namespace QLBV.FormTraCuu
{
    public partial class us_TCNhapXuatTon : DevExpress.XtraEditors.XtraUserControl
    {
        public us_TCNhapXuatTon()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        DateTime _dttu = DungChung.Ham.NgayTu(System.DateTime.Now);
        DateTime _dtden = DungChung.Ham.NgayDen(System.DateTime.Now);
        int makho = 0;
        int madv = 0;
        string mancc = "";
        private void us_TCNhapXuatTon_Load(object sender, EventArgs e)
        {
            dtTimTuNgay.DateTime = DungChung.Ham.ConvertNgay("01/01/2014");
            dtTimDenNgay.DateTime = System.DateTime.Now;
            var kho = from kp in _data.KPhongs.Where(p => p.PLoai.Contains("dược")) select new {kp.TenKP,kp.MaKP};
            lupKhoXuat.Properties.DataSource = kho;
            var qcc = from CC in _data.NhaCCs select new { CC.MaCC, CC.TenCC };
            lupNhaCC.Properties.DataSource = qcc.ToList();
        }
        private void lupNhaCC_EditValueChanged(object sender, EventArgs e)
        {
            if (lupNhaCC.EditValue != null && lupNhaCC.EditValue.ToString() != "")
                mancc = lupNhaCC.EditValue.ToString();
            var cc = (from nd in _data.NhapDs.Where(p => p.MaCC == mancc)
                      join kp in _data.KPhongs on nd.MaKP equals kp.MaKP
                      select new { kp.MaKP, kp.TenKP }).ToList();

            lupKhoXuat.Properties.DataSource = cc.ToList();
        }
        private void lupKhoXuat_EditValueChanged(object sender, EventArgs e)
        {
            if (lupKhoXuat.EditValue != null && Convert.ToInt32(lupKhoXuat.EditValue) != 0)
                makho = Convert.ToInt32( lupKhoXuat.EditValue);
            var duoc = (from tenduoc in _data.DichVus.Where(p => p.PLoai == 1)
                        join nduocct in _data.NhapDcts on tenduoc.MaDV equals nduocct.MaDV
                        join nduoc in _data.NhapDs.Where(p => p.PLoai == 1).Where(p => p.MaKP== makho) on nduocct.IDNhap equals nduoc.IDNhap
                        group new { tenduoc, nduocct, nduoc } by new { nduoc.MaKP, tenduoc.TenDV, tenduoc.MaDV, tenduoc.DonVi } into kq
                        select new { kq.Key.TenDV, kq.Key.MaDV, kq.Key.DonVi, kq.Key.MaKP }
                        ).OrderBy(p => p.TenDV).ToList();
            lupMaDV.Properties.DataSource = duoc.ToList();
        }
 
        private void lupMaDV_EditValueChanged(object sender, EventArgs e)
        {
            cboDonGia.Text = "";
            for (int a = 0; a < cboDonGia.Properties.Items.Count; a++)
            {
                cboDonGia.Properties.Items.RemoveAt(a);
            }
            if (lupMaDV.EditValue != null)
            {
                madv = Convert.ToInt32( lupMaDV.EditValue);
                var gia = (from nhapduoc in _data.NhapDcts.Where(p => p.MaDV== madv)
                           join nduoc in _data.NhapDs.Where(p => p.PLoai == 1).Where(p => p.MaKP== makho) on nhapduoc.IDNhap equals nduoc.IDNhap
                           group new { nhapduoc } by new { nhapduoc.DonGia } into kq
                           select new { kq.Key.DonGia }).ToList();
                if (gia.Count > 0)
                {
                    foreach (var g in gia)
                    {
                        cboDonGia.Properties.Items.Add(g.DonGia);
                    }
                }
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
        //    int status = -1;
            _dttu = DungChung.Ham.NgayTu(dtTimTuNgay.DateTime);
            _dtden = DungChung.Ham.NgayDen(dtTimDenNgay.DateTime);
            double dongia = 0;
            if (!string.IsNullOrEmpty(cboDonGia.Text))
            {
                dongia = Convert.ToDouble(cboDonGia.EditValue);
                if (!string.IsNullOrEmpty(lupKhoXuat.Text))
                {
                    makho = Convert.ToInt32( lupKhoXuat.EditValue);
                    if (!string.IsNullOrEmpty(lupNhaCC.Text))
                    {
                        mancc = lupNhaCC.EditValue.ToString();
                        var nxt = (from nd in _data.NhapDs.Where(p => p.MaKP == makho).Where(p => p.NgayNhap >= _dttu).Where(p => p.NgayNhap <= _dtden)
                                   join ndct in _data.NhapDcts.Where(p => p.MaCC == mancc).Where(p => p.DonGia == dongia) on nd.IDNhap equals ndct.IDNhap
                                   select new { nd.NgayNhap, nd.SoCT, ndct.DonVi, ndct.SoLuongN, ndct.SoLuongX }).ToList();
                        grcTraCuu.DataSource = nxt;
                        int a = int.Parse(nxt.Sum(p => p.SoLuongN).ToString());
                        int b = int.Parse(nxt.Sum(p => p.SoLuongX).ToString());
                        if (b > 0)
                        {
                            labSLTon.Text = (a - b).ToString();
                        }
                        else labSLTon.Text = a.ToString();
                    }
                    else
                    {
                        var nxt = (from nd in _data.NhapDs.Where(p => p.MaKP==makho).Where(p => p.NgayNhap >= _dttu).Where(p => p.NgayNhap <= _dtden)
                                   join ndct in _data.NhapDcts.Where(p => p.MaDV== madv).Where(p => p.DonGia == dongia) on nd.IDNhap equals ndct.IDNhap
                                   select new { nd.NgayNhap, nd.SoCT, ndct.DonVi, ndct.SoLuongN, ndct.SoLuongX }).ToList();
                        grcTraCuu.DataSource = nxt;
                        int a =int.Parse(nxt.Sum(p => p.SoLuongN).ToString());
                        int b = int.Parse(nxt.Sum(p => p.SoLuongX).ToString());
                        if (b > 0)
                        {
                            labSLTon.Text = (a - b).ToString();
                        }
                        else labSLTon.Text = a.ToString();
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(lupNhaCC.Text))
                    {
                        mancc = lupNhaCC.EditValue.ToString();
                        var nxt = (from nd in _data.NhapDs.Where(p => p.NgayNhap >= _dttu).Where(p => p.NgayNhap <= _dtden)
                                   join ndct in _data.NhapDcts.Where(p => p.MaCC == mancc).Where(p => p.DonGia == dongia) on nd.IDNhap equals ndct.IDNhap
                                   select new { nd.NgayNhap, nd.SoCT, ndct.DonVi, ndct.SoLuongN, ndct.SoLuongX }).ToList();
                        grcTraCuu.DataSource = nxt;
                        int a = int.Parse(nxt.Sum(p => p.SoLuongN).ToString());
                        int b = int.Parse(nxt.Sum(p => p.SoLuongX).ToString());
                        if (b > 0)
                        {
                            labSLTon.Text = (a - b).ToString();
                        }
                        else labSLTon.Text = a.ToString();
                    }
                    else
                    {
                        var nxt = (from nd in _data.NhapDs.Where(p => p.NgayNhap >= _dttu).Where(p => p.NgayNhap <= _dtden)
                                   join ndct in _data.NhapDcts.Where(p => p.MaDV== madv).Where(p => p.DonGia == dongia) on nd.IDNhap equals ndct.IDNhap
                                   select new { nd.NgayNhap, nd.SoCT, ndct.DonVi, ndct.SoLuongN, ndct.SoLuongX }).ToList();
                        grcTraCuu.DataSource = nxt;
                        int a = int.Parse(nxt.Sum(p => p.SoLuongN).ToString());
                        int b = int.Parse(nxt.Sum(p => p.SoLuongX).ToString());
                        if (b > 0)
                        {
                            labSLTon.Text = (a - b).ToString();
                        }
                        else labSLTon.Text = a.ToString();
                    }
                }

            }
            else
            {
                MessageBox.Show("Bạn chưa chọn đơn giá");
            }
        }

        private void grvTraCuu_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column == STT)
            {
                e.DisplayText = Convert.ToString(e.RowHandle + 1);
            }
        }
    }
}
