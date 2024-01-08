using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
namespace QLBV.FormThamSo
{
    public partial class frm_DoiChieuThuocTheoNgay : DevExpress.XtraEditors.XtraForm
    {
        public frm_DoiChieuThuocTheoNgay()
        {
            InitializeComponent();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private class BC
        {
            private string tendv, donvi, tennhom;
            private int madv;
            private double sl1, sl2, sl3, sl4, sl5, sl6, sl7, sl8, sl9, sl10, sl11, sl12, sl13, sl14, sl15, sl16, sl17, sl18, sl19, sl20, sl21, sl22, sl23, sl24, sl25, sl26, sl27, sl28, sl29, sl30, sl31, sl32, slt, dongia, thanhtien;
            public string TenDV
            { set { tendv = value; } get { return tendv; } }
            public int MaDV
            { set { madv = value; } get { return madv; } }
            public string DonVi
            { set { donvi = value; } get { return donvi; } }
            public string TenNhomDV
            { set { tennhom = value; } get { return tennhom; } }
            public double SL1
            { set { sl1 = value; } get { return sl1; } }
            public double SL2
            { set { sl2 = value; } get { return sl2; } }
            public double SL3
            { set { sl3 = value; } get { return sl3; } }
            public double SL4
            { set { sl4 = value; } get { return sl4; } }
            public double SL5
            { set { sl5 = value; } get { return sl5; } }
            public double SL6
            { set { sl6 = value; } get { return sl6; } }
            public double SL7
            { set { sl7 = value; } get { return sl7; } }
            public double SL8
            { set { sl8 = value; } get { return sl8; } }
            public double SL9
            { set { sl9 = value; } get { return sl9; } }
            public double SL10
            { set { sl10 = value; } get { return sl10; } }
            public double SL11
            { set { sl11 = value; } get { return sl11; } }
            public double SL12
            { set { sl12 = value; } get { return sl12; } }
            public double SL13
            { set { sl13 = value; } get { return sl13; } }
            public double SL14
            { set { sl14 = value; } get { return sl14; } }
            public double SL15
            { set { sl15 = value; } get { return sl15; } }
            public double SL16
            { set { sl16 = value; } get { return sl16; } }
            public double SL17
            { set { sl17 = value; } get { return sl17; } }
            public double SL18
            { set { sl18 = value; } get { return sl18; } }
            public double SL19
            { set { sl19 = value; } get { return sl19; } }
            public double SL20
            { set { sl20 = value; } get { return sl20; } }
            public double SL21
            { set { sl21 = value; } get { return sl21; } }
            public double SL22
            { set { sl22 = value; } get { return sl22; } }
            public double SL23
            { set { sl23 = value; } get { return sl23; } }
            public double SL24
            { set { sl24 = value; } get { return sl24; } }
            public double SL25
            { set { sl25 = value; } get { return sl25; } }
            public double SL26
            { set { sl26 = value; } get { return sl26; } }
            public double SL27
            { set { sl27 = value; } get { return sl27; } }
            public double SL28
            { set { sl28 = value; } get { return sl28; } }
            public double SL29
            { set { sl29 = value; } get { return sl29; } }
            public double SL30
            { set { sl30 = value; } get { return sl30; } }
            public double SL31
            { set { sl31 = value; } get { return sl31; } }
            public double SL32
            { set { sl32 = value; } get { return sl32; } }
            public double SLT
            { set { slt = value; } get { return slt; } }
            public double DonGia
            { set { dongia = value; } get { return dongia; } }
            public double ThanhTien
            { set { thanhtien = value; } get { return thanhtien; } }
        }
        List<BC> _BC = new List<BC>();
        //BC _BC = new BC();
        private void btnInBC_Click(object sender, EventArgs e)
        {
            if ((dateDenNgay.DateTime - dateTuNgay.DateTime).Days > 32 || (dateDenNgay.DateTime - dateTuNgay.DateTime).Days < 0) { 
                MessageBox.Show("Giới hạn ngày không hợp lệ");
                return;
            }
            int _trongbh = rgTrongDM.SelectedIndex;
            string _dtuong = "";
            if (cboDoiTuong.SelectedIndex == 0 || cboDoiTuong.SelectedIndex == 1)
                _dtuong = cboDoiTuong.Text;
            DateTime tungay = System.DateTime.Now.Date;
            DateTime denngay = System.DateTime.Now.Date;
            DateTime ngayvao = System.DateTime.Now.Date;
            DateTime ngayra = System.DateTime.Now.Date;
            ngayvao = DungChung.Ham.NgayTu(dateTuNgay.DateTime);
            ngayra = DungChung.Ham.NgayDen(dateDenNgay.DateTime);
            frmIn frm = new frmIn();
            int _makho = 0, _makhoa = 0;
            if (lupKhoa.EditValue != null && lupKhoke.EditValue != null)
            {
                _makhoa = Convert.ToInt32(lupKhoa.EditValue);
                _makho = Convert.ToInt32(lupKhoke.EditValue);
                //QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                //frmIn frm = new frmIn();
                //BaoCao.repDTNoiTruHangNgay_HL01a rep = new BaoCao.repDTNoiTruHangNgay_HL01a();
                //_mbn = txtMaBNhan.Text;
                DateTime[] _songay;
                _songay = new DateTime[50];
                for (int j = 0; j < 50; j++)
                {
                    _songay[j] = Convert.ToDateTime("01/01/2001");
                }
                //var ngaykd1 = (from nk in data.DThuocs.Where(p => p.MaKP== (_makhoa)).Where(p=>p.MaKXuat== (_makho)).Where(p => p.PLDV == 1).Where(p=>p.NgayKe<=ngayra).Where(p=>p.NgayKe>=ngayvao)
                //              group nk by nk.NgayKe into kq
                //              select new { kq.Key.Value }).OrderBy(p => p.Value).ToList();
                var tong = (from bn in data.BenhNhans.Where(p => cboDoiTuong.SelectedIndex == 2 ? true : p.DTuong == _dtuong)
                            join dthc in data.DThuocs.Where(p => _makhoa == 0 ? true : p.MaKP == _makhoa).Where(p => p.MaKXuat == _makho) on bn.MaBNhan equals dthc.MaBNhan
                            join dtct in data.DThuoccts.Where(p => _trongbh == 3 ? true : p.TrongBH == _trongbh) on dthc.IDDon equals dtct.IDDon
                            join dv in data.DichVus on dtct.MaDV equals dv.MaDV
                            join nhomdv in data.NhomDVs on dv.IDNhom equals nhomdv.IDNhom
                            where (dthc.NgayKe >= ngayvao && dthc.NgayKe <= ngayra)
                            select new { dtct.DonGia, dtct.SoLuong, dtct.ThanhTien, dv.DonVi, dv.TenDV, dv.MaDV, nhomdv.TenNhom, dthc.NgayKe }).ToList();

                var ngay1 = (from ngay in tong
                             select new { ngay.NgayKe.Value.Date }).OrderBy(p => p.Date).ToList().Distinct().ToList();

                var dt2 = (from h in tong
                           group new { h } by new { h.DonGia, h.MaDV, h.NgayKe, h.SoLuong } into kq
                           select new { kq.Key.DonGia, kq.Key.MaDV, kq.Key.NgayKe, kq.Key.SoLuong }).ToList();
                //int i = 0;
                for (int i = 0; i < ngay1.Count; i++)
                {
                    _songay[i] = ngay1.Skip(i).First().Date.Date;
                }
                BaoCao.rep_DoiChieuThuocTheoNgay rep = new BaoCao.rep_DoiChieuThuocTheoNgay(_songay, _makho, _makhoa, _dtuong);
                rep.Ngay.Value = lupKhoa.Text;
                switch(_trongbh)
                {
                    case 0:
                        rep.TieuDe.Value = "PHIẾU ĐỐI CHIẾU THUỐC NGOÀI BHYT " + lupKhoa.Text.ToUpper();
                        break;
                    case 1:
                        rep.TieuDe.Value = "PHIẾU ĐỐI CHIẾU THUỐC BHYT " + lupKhoa.Text.ToUpper();
                        break;
                    case 2:
                        rep.TieuDe.Value = "PHIẾU ĐỐI CHIẾU THUỐC KHÔNG THANH TOÁN " + lupKhoa.Text.ToUpper();
                        break;
                    case 3:
                        rep.TieuDe.Value = "PHIẾU ĐỐI CHIẾU THUỐC " + lupKhoa.Text.ToUpper();
                        break;
                }
                
                double thuoc = 0;

                var q = (from k in tong
                         group k by new { k.MaDV, k.DonGia, k.DonVi, k.TenDV, k.TenNhom } into kq
                         select new BC
                         {
                             MaDV = kq.Key.MaDV,
                             DonGia = kq.Key.DonGia,
                             TenNhomDV = kq.Key.TenNhom.ToUpper(),
                             TenDV = kq.Key.TenDV,
                             DonVi = kq.Key.DonVi,
                             SLT = kq.Sum(p => p.SoLuong),
                             ThanhTien = kq.Sum(p => p.ThanhTien),
                             SL1 = kq.Where(p => p.NgayKe.Value.Date == _songay[0]).Sum(p => p.SoLuong),
                             SL2 = kq.Where(p => p.NgayKe.Value.Date  == _songay[1]).Sum(p => p.SoLuong),
                             SL3 = kq.Where(p => p.NgayKe.Value.Date  == _songay[2]).Sum(p => p.SoLuong),
                             SL4 = kq.Where(p => p.NgayKe.Value.Date  == _songay[3]).Sum(p => p.SoLuong),
                             SL5 = kq.Where(p => p.NgayKe.Value.Date  == _songay[4]).Sum(p => p.SoLuong),
                             SL6 = kq.Where(p => p.NgayKe.Value.Date  == _songay[5]).Sum(p => p.SoLuong),
                             SL7 = kq.Where(p => p.NgayKe.Value.Date  == _songay[6]).Sum(p => p.SoLuong),
                             SL8 = kq.Where(p => p.NgayKe.Value.Date  == _songay[7]).Sum(p => p.SoLuong),
                             SL9 = kq.Where(p => p.NgayKe.Value.Date  == _songay[8]).Sum(p => p.SoLuong),
                             SL10 = kq.Where(p => p.NgayKe.Value.Date  == _songay[9]).Sum(p => p.SoLuong),
                             SL11 = kq.Where(p => p.NgayKe.Value.Date  == _songay[10]).Sum(p => p.SoLuong),
                             SL12 = kq.Where(p => p.NgayKe.Value.Date  == _songay[11]).Sum(p => p.SoLuong),
                             SL13 = kq.Where(p => p.NgayKe.Value.Date  == _songay[12]).Sum(p => p.SoLuong),
                             SL14 = kq.Where(p => p.NgayKe.Value.Date  == _songay[13]).Sum(p => p.SoLuong),
                             SL15 = kq.Where(p => p.NgayKe.Value.Date  == _songay[14]).Sum(p => p.SoLuong),
                             SL16 = kq.Where(p => p.NgayKe.Value.Date  == _songay[15]).Sum(p => p.SoLuong),
                             SL17 = kq.Where(p => p.NgayKe.Value.Date  == _songay[16]).Sum(p => p.SoLuong),
                             SL18 = kq.Where(p => p.NgayKe.Value.Date  == _songay[17]).Sum(p => p.SoLuong),
                             SL19 = kq.Where(p => p.NgayKe.Value.Date  == _songay[18]).Sum(p => p.SoLuong),
                             SL20 = kq.Where(p => p.NgayKe.Value.Date  == _songay[19]).Sum(p => p.SoLuong),
                             SL21 = kq.Where(p => p.NgayKe.Value.Date  == _songay[20]).Sum(p => p.SoLuong),
                             SL22 = kq.Where(p => p.NgayKe.Value.Date  == _songay[21]).Sum(p => p.SoLuong),
                             SL23 = kq.Where(p => p.NgayKe.Value.Date  == _songay[22]).Sum(p => p.SoLuong),
                             SL24 = kq.Where(p => p.NgayKe.Value.Date  == _songay[23]).Sum(p => p.SoLuong),
                             SL25 = kq.Where(p => p.NgayKe.Value.Date  == _songay[24]).Sum(p => p.SoLuong),
                             SL26 = kq.Where(p => p.NgayKe.Value.Date  == _songay[25]).Sum(p => p.SoLuong),
                             SL27 = kq.Where(p => p.NgayKe.Value.Date  == _songay[26]).Sum(p => p.SoLuong),
                             SL28 = kq.Where(p => p.NgayKe.Value.Date  == _songay[27]).Sum(p => p.SoLuong),
                             SL29 = kq.Where(p => p.NgayKe.Value.Date  == _songay[28]).Sum(p => p.SoLuong),
                             SL30 = kq.Where(p => p.NgayKe.Value.Date  == _songay[29]).Sum(p => p.SoLuong),
                             SL31 = kq.Where(p => p.NgayKe.Value.Date  == _songay[30]).Sum(p => p.SoLuong),
                             SL32 = kq.Where(p => p.NgayKe.Value.Date  == _songay[31]).Sum(p => p.SoLuong),
                         }).ToList();

                rep.DataSource = q;
                rep.BindingData();
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Bạn chưa chọn kho hoặc khoa");
            }
        }

        private void frm_DoiChieuThuocTheoNgay_Load(object sender, EventArgs e)
        {
            dateDenNgay.DateTime = System.DateTime.Now;
            dateTuNgay.DateTime = System.DateTime.Now;
            var a = data.KPhongs.OrderBy(p => p.TenKP).ToList();
            var _lkp = a.Where(p => p.PLoai == "Lâm sàng").Select(p => new { p.MaKP, p.TenKP }).ToList();
            _lkp.Add(new { MaKP = 0, TenKP = "Tất cả" });
            lupKhoa.Properties.DataSource = _lkp.OrderBy(p => p.TenKP).ToList();
            lupKhoa.EditValue = 0;
            var _lkho = a.Where(p => p.PLoai == "Lâm sàng").Select(p => new { p.MaKP, p.TenKP }).ToList();
            //_lkho.Add(new { MaKP = 0, TenKP = "Tất cả" });
            lupKhoke.Properties.DataSource = a.Where(p => p.PLoai == "Khoa dược").ToList();
        }
    }
}