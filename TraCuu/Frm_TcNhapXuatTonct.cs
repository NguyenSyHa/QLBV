using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;

namespace QLBV.FormTraCuu
{
    public partial class Frm_TcNhapXuatTonct : DevExpress.XtraEditors.XtraForm
    {
        public Frm_TcNhapXuatTonct()
        {
            InitializeComponent();
        }
        int id = 0;
        public Frm_TcNhapXuatTonct(int _id)
        {
            InitializeComponent();
            id = _id;
        }
        private void EnableControl(bool status)
        {
            txtIDNhap.Properties.ReadOnly = status;
            txtNgayNhap.Properties.ReadOnly = status;
            txtSoCT.Properties.ReadOnly = status;
            txtKho.Properties.ReadOnly = status;
            txtNhaCC.Properties.ReadOnly = status;
            txtPL.Properties.ReadOnly = status;
            txtNoiNhan.Properties.ReadOnly = status;
            txtLiDo.Properties.ReadOnly = status;
            txtNguoiGiao.Properties.ReadOnly = status;
        }
        private class NXTct
        {
            private int ID;
            private string tendv;
            private string solo;
            private string sodangky;
            private string handung;
            private string donvi;
            private double dongiact;
            private int vat;
            private double dongia;
            private double soluong;
            private double thanhtien;
            public int IDNhapct
            { set { ID = value; } get { return ID; } }
            public string TenDV
            { set { tendv = value; } get { return tendv; } }
            public string SoLo
            { set { solo = value; } get { return solo; } }
            public string SoDangKy
            { set { sodangky = value; } get { return sodangky; } }
            public string HanDung
            { set { handung = value; } get { return handung; } }
            public string DonVi
            { set { donvi = value; } get { return donvi; } }
            public double DonGiaCT
            { set { dongiact = value; } get { return dongiact; } }
            public int VAT
            { set { vat = value; } get { return vat; } }
            public double DonGia
            { set { dongia = value; } get { return dongia; } }
            public double SoLuong
            { set { soluong = value; } get { return soluong; } }
            public double ThanhTien
            { set { thanhtien = value; } get { return thanhtien; } }
        }
        List<NXTct> _NXTct = new List<NXTct>();
        private void Frm_TcNhapXuatTonct_Load(object sender, EventArgs e)
        {
            EnableControl(false);
            _NXTct.Clear();
            QLBV_Database.QLBVEntities DataContect = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                       //if (!string.IsNullOrEmpty(txtIDNhap.Text))
            //{ _id = Convert.ToInt32(txtIDNhap.Text); }
            var qnct = (from nx in DataContect.NhapDcts.Where(p => p.IDNhap == id)
                         join dv in DataContect.DichVus on nx.MaDV equals dv.MaDV
                         join nd in DataContect.NhapDs on nx.IDNhap equals nd.IDNhap
                         select new {nx.SoLuongX,nx.ThanhTienX, nx.IDNhapct, dv.TenDV, nx.SoLo, nx.SoDangKy, nx.HanDung, nx.DonVi, nx.DonGiaCT, nx.VAT, nx.DonGia, nx.SoLuongN, nx.ThanhTienN }).ToList();

           
                foreach(var a in qnct)
                {
                    NXTct them = new NXTct();
                    them.IDNhapct = a.IDNhapct;
                    them.TenDV = a.TenDV;
                    them.SoLo = a.SoLo;
                    them.SoDangKy = a.SoDangKy;
                    them.HanDung = a.HanDung.ToString();
                    them.DonVi = a.DonVi;
                    them.DonGiaCT =Convert.ToDouble(a.DonGiaCT);
                    them.VAT =Convert.ToInt32(a.VAT);
                    them.DonGia = Convert.ToDouble(a.DonGia);
                    them.SoLuong = a.SoLuongN + a.SoLuongX;
                    them.ThanhTien = a.ThanhTienN + a.ThanhTienX;
                    _NXTct.Add(them);
                }

            grcNhapCT.DataSource =_NXTct.ToList();
            var qnx = (from nx in DataContect.NhapDs.Where(p => p.IDNhap == id)
                       join kp in DataContect.KPhongs on nx.MaKP equals kp.MaKP
                       select new { nx.NgayNhap, nx.SoCT, kp.TenKP, nx.MaCC, nx.TenNguoiCC, nx.KieuDon, nx.MaBNhan, nx.MaKPnx, nx.MaCB, nx.GhiChu }).ToList();
            if (qnx.Count() > 0)
            {
                txtIDNhap.Text = id.ToString();
                txtNgayNhap.Text = Convert.ToDateTime(qnx.First().NgayNhap).ToString();
                txtSoCT.Text = qnx.First().SoCT.ToString();
                txtKho.Text = qnx.First().TenKP.ToString();
                if (qnx.First().MaCC != null && qnx.First().MaCC != "")
                {
                    var qcc = (from ncc in DataContect.NhaCCs.Where(p => p.MaCC == qnx.First().MaCC)
                               select new { ncc.TenCC }).ToList();
                    if (qcc.Count > 0) { txtNCC.Text = qcc.First().TenCC.ToString(); } else txtNCC.Text = "";
                }
                if (qnx.First().KieuDon != null)
                {
                    if (qnx.First().KieuDon == 0) { txtPL.Text = "Xuất ngoại trú"; }
                    if (qnx.First().KieuDon == 1) { txtPL.Text = "Xuất nội trú"; }
                    if (qnx.First().KieuDon == 2) { txtPL.Text = "Xuất nội bộ"; }
                    if (qnx.First().KieuDon == 3) { txtPL.Text = "Xuất ngoài BV"; }
                    if (qnx.First().KieuDon == 4) { txtPL.Text = "Xuất Nhân dân"; }
                    if (qnx.First().KieuDon == 5) { txtPL.Text = "Xuất Cận lâm sàng"; }
                    if (qnx.First().KieuDon == 6) { txtPL.Text = "Xuất Tủ trực"; }
                }
                if (qnx.First().KieuDon == 0)
                {
                    var qnn1 = (from nn in DataContect.NhapDs.Where(p => p.IDNhap == id)
                                join bn in DataContect.BenhNhans on nn.MaBNhan equals bn.MaBNhan
                                select new { bn.TenBNhan }).ToList();
                    if (qnn1.Count > 0)
                    {
                        txtNoiNhan.Text = qnn1.First().TenBNhan.ToString();
                    }
                }
                else
                {
                    var qnn2 = (from nn in DataContect.NhapDs.Where(p => p.IDNhap == id)
                                join kp in DataContect.KPhongs on nn.MaKPnx equals kp.MaKP
                                select new { kp.TenKP }).ToList();
                    if (qnn2.Count > 0)
                    {
                        txtNoiNhan.Text = qnn2.First().TenKP.ToString();
                    }
                }
                txtNguoiGiao.Text = qnx.First().TenNguoiCC.ToString();
                txtLiDo.Text = qnx.First().GhiChu.ToString();

            }


                
            //}

        }

        private void grvNhapCT_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column == STT)
            {
                e.DisplayText = Convert.ToString(e.RowHandle + 1);
            }
        }

        private void txtNCC_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtIDNhap_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}