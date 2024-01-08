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
    public partial class frm_BCHD_CTKB_01049 : DevExpress.XtraEditors.XtraForm
    {
        public frm_BCHD_CTKB_01049()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

        private void frm_BCHD_CTKB_01049_Load(object sender, EventArgs e)
        {
            donvi.Visible = false;
            lup_Ngayden.DateTime = DateTime.Now;
            lup_Ngaytu.DateTime = DateTime.Now;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private class bc
        {
            public string ND { get; set; }
            public int? BHYT { get; set; }
            public int? DV { get; set; }
            public int? Tong { get; set; }
            public string DonVi { get; set; }
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            DateTime ngaytu = DungChung.Ham.NgayTu(lup_Ngaytu.DateTime);
            DateTime ngayden = DungChung.Ham.NgayDen(lup_Ngayden.DateTime);

            //var bn = _data.BenhNhans.ToList();
            //var rv = _data.RaViens.Where(p => p.NgayRa >= ngaytu && p.NgayRa <= ngayden).ToList();
            string HangBV = _data.BenhViens.Where(p => p.MaBV == "01049").FirstOrDefault().TuyenBV;
            int Tuyen = HangBV == "A" ? 1 : (HangBV == "B" ? 2 : (HangBV == "C" ? 3 : 4));
            var bn1 = (from a in _data.BenhNhans.ToList()
                       join b in _data.RaViens on a.MaBNhan equals b.MaBNhan
                       join c in _data.BenhViens on b.MaBVC equals c.MaBV into k
                       from kq in k.DefaultIfEmpty()
                       select new { a.MaBNhan, a.NNhap, b.NgayRa, a.CapCuu, b.Status, a.DTuong, Tuyen = kq != null ? (kq.TuyenBV == "A" ? 1 : (kq.TuyenBV == "B" ? 2 : (kq.TuyenBV == "C" ? 3 : 4))) : 5 }).ToList();

            var kb = (from a in _data.BNKBs.Where(p => p.NgayKham >= ngaytu && p.NgayKham <= ngayden)
                      join b in _data.KPhongs on a.MaKP equals b.MaKP
                      join c in _data.BenhNhans on a.MaBNhan equals c.MaBNhan
                      select new {a.MaICD, a.MaBNhan, a.IDKB, b.TenKP, c.DTuong, c.NoiTru, c.DTNT, a.NgayKham, NgayNghi = a.NgayNghi != null? a.NgayNghi : DateTime.Now }).ToList();

            var dt = (from a in _data.DThuocs.Where(p => p.PLDV == 2)
                      join b in _data.DThuoccts.Where(p => p.NgayNhap >= ngaytu && p.NgayNhap <= ngayden) on a.IDDon equals b.IDDon
                      select new { b.MaDV, a.MaBNhan, b.MaKP, b.IDCD}).ToList();

            var dv = (from a in _data.DichVus.Where(p => p.PLoai == 2)
                      join b in _data.TieuNhomDVs on a.IdTieuNhom equals b.IdTieuNhom
                      join c in _data.NhomDVs on a.IDNhom equals c.IDNhom
                      select new { a.MaDV, b.TenRG, b.TenTN, c.TenNhom, c.IDNhom, a.TenDV}).ToList();

            var dt1 = (from a in dt
                       join b in dv on a.MaDV equals b.MaDV
                       join c in _data.KPhongs on a.MaKP equals c.MaKP
                       join d in _data.BenhNhans on a.MaBNhan equals d.MaBNhan
                       select new { a.MaDV, a.MaBNhan, b.IDNhom, b.TenNhom, b.TenRG, b.TenTN, c.TenKP, d.DTuong, b.TenDV, a.IDCD }).ToList();

            var cd = (from a in _data.CLS.Where(p => p.NgayThang >= ngaytu && p.NgayThang <= ngayden)
                      join b in _data.ChiDinhs on a.IdCLS equals b.IdCLS
                      select new { b.MaDV, a.MaBNhan }).ToList();

            var cd1 = (from a in cd
                       join b in dv on a.MaDV equals b.MaDV
                       select new { a.MaDV, b.TenRG }).ToList();
            if (radioGroup1.SelectedIndex == 0)
            {
                BaoCao.rep_BCHDPhongKham_01049 rep = new BaoCao.rep_BCHDPhongKham_01049();
                rep.ngaylay.Value = "Từ ngày " + ngaytu.Day + "/" + ngaytu.Month + "/" + ngaytu.Year + " đến ngày " + ngayden.Day + "/" + ngayden.Month + "/" + ngayden.Year;

                rep.Kham_1.Value = bn1.Where(p => p.NNhap >= ngaytu && p.NNhap <= ngayden && p.CapCuu == 1).Count();
                rep.Kham_2.Value = bn1.Where(p => p.NgayRa >= ngaytu && p.NgayRa <= ngayden).Where(p => p.Status == 1).Count();
                rep.Kham_3.Value = kb.Count();
                rep.Kham_31.Value = kb.Where(p => p.TenKP == "Phòng Khám Nội").Count();
                rep.Kham_32.Value = kb.Where(p => p.TenKP == "Phòng Khám Ngoại").Count();
                rep.Kham_33.Value = kb.Where(p => p.TenKP == "Phòng Khám Sản").Count();
                rep.Kham_34.Value = kb.Where(p => p.TenKP == "Phòng Khám RHM").Count();
                rep.Kham_35.Value = kb.Where(p => p.TenKP == "Phòng Khám TMH").Count();
                rep.Kham_36.Value = kb.Where(p => p.TenKP == "Phòng Khám Đông Y").Count();
                rep.Kham_42.Value = dt1.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_QuangCT || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_QuangKTS).Count();
                rep.Kham_43.Value = dt1.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler).Count();
                rep.Kham_43_2D.Value = dt1.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler).Where(p => !p.TenDV.Contains("4D")).Count();
                rep.Kham_43_4D.Value = dt1.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler).Where(p => p.TenDV.Contains("4D")).Count();
                rep.Kham_44.Value = dt1.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoi).Count();

                rep.BHYT_1.Value = bn1.Where(p => p.NNhap >= ngaytu && p.NNhap <= ngayden && p.CapCuu == 1).Where(p => p.DTuong == "BHYT").Count();
                rep.BHYT_2.Value = bn1.Where(p => p.NgayRa >= ngaytu && p.NgayRa <= ngayden).Where(p => p.Status == 1).Where(p => p.DTuong == "BHYT").Count();
                rep.BHYT_3.Value = kb.Where(p => p.DTuong == "BHYT").Count();
                rep.BHYT_31.Value = kb.Where(p => p.TenKP == "Phòng Khám Nội").Where(p => p.DTuong == "BHYT").Count();
                rep.BHYT_32.Value = kb.Where(p => p.TenKP == "Phòng Khám Ngoại").Where(p => p.DTuong == "BHYT").Count();
                rep.BHYT_33.Value = kb.Where(p => p.TenKP == "Phòng Khám Sản").Where(p => p.DTuong == "BHYT").Count();
                rep.BHYT_34.Value = kb.Where(p => p.TenKP == "Phòng Khám RHM").Where(p => p.DTuong == "BHYT").Count();
                rep.BHYT_35.Value = kb.Where(p => p.TenKP == "Phòng Khám TMH").Where(p => p.DTuong == "BHYT").Count();
                rep.BHYT_36.Value = kb.Where(p => p.TenKP == "Phòng Khám Đông Y").Where(p => p.DTuong == "BHYT").Count();
                rep.BHYT_42.Value = dt1.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_QuangCT || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_QuangKTS).Where(p => p.DTuong == "BHYT").Count();
                rep.BHYT_43.Value = dt1.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler).Where(p => p.DTuong == "BHYT").Count();
                rep.BHYT_43_2D.Value = dt1.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler).Where(p => !p.TenDV.Contains("4D")).Where(p => p.DTuong == "BHYT").Count();
                rep.BHYT_43_4D.Value = dt1.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler).Where(p => p.TenDV.Contains("4D")).Where(p => p.DTuong == "BHYT").Count();
                rep.BHYT_44.Value = dt1.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoi).Where(p => p.DTuong == "BHYT").Count();

                rep.DV_1.Value = bn1.Where(p => p.NNhap >= ngaytu && p.NNhap <= ngayden && p.CapCuu == 1).Where(p => p.DTuong == "ko làm ").Count();
                rep.DV_2.Value = bn1.Where(p => p.NgayRa >= ngaytu && p.NgayRa <= ngayden).Where(p => p.Status == 1).Where(p => p.DTuong == "Dịch vụ").Count();
                rep.DV_3.Value = kb.Where(p => p.DTuong == "Dịch vụ").Count();
                rep.DV_31.Value = kb.Where(p => p.TenKP == "Phòng Khám Nội").Where(p => p.DTuong == "Dịch vụ").Count();
                rep.DV_32.Value = kb.Where(p => p.TenKP == "Phòng Khám Ngoại").Where(p => p.DTuong == "Dịch vụ").Count();
                rep.DV_33.Value = kb.Where(p => p.TenKP == "Phòng Khám Sản").Where(p => p.DTuong == "Dịch vụ").Count();
                rep.DV_34.Value = kb.Where(p => p.TenKP == "Phòng Khám RHM").Where(p => p.DTuong == "Dịch vụ").Count();
                rep.DV_35.Value = kb.Where(p => p.TenKP == "Phòng Khám TMH").Where(p => p.DTuong == "Dịch vụ").Count();
                rep.DV_36.Value = kb.Where(p => p.TenKP == "Phòng Khám Đông Y").Where(p => p.DTuong == "Dịch vụ").Count();
                rep.DV_42.Value = dt1.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_QuangCT || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_QuangKTS).Where(p => p.DTuong == "Dịch vụ").Count();
                rep.DV_43.Value = dt1.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler).Where(p => p.DTuong == "Dịch vụ").Count();
                rep.DV_43_2D.Value = dt1.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler).Where(p => !p.TenDV.Contains("4D")).Where(p => p.DTuong == "Dịch vụ").Count();
                rep.DV_43_4D.Value = dt1.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler).Where(p => p.TenDV.Contains("4D")).Where(p => p.DTuong == "Dịch vụ").Count();
                rep.DV_44.Value = dt1.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoi).Where(p => p.DTuong == "Dịch vụ").Count();

                rep.ThuThuat_31.Value = dt1.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat).Where(p => p.TenKP == "Phòng Khám Nội").Count();
                rep.ThuThuat_32.Value = dt1.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat).Where(p => p.TenKP == "Phòng Khám Ngoại").Count();
                rep.ThuThuat_33.Value = dt1.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat).Where(p => p.TenKP == "Phòng Khám Sản").Count();
                rep.ThuThuat_34.Value = dt1.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat).Where(p => p.TenKP == "Phòng Khám RHM").Count();
                rep.ThuThuat_35.Value = dt1.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat).Where(p => p.TenKP == "Phòng Khám TMH").Count();
                rep.ThuThuat_36.Value = dt1.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat).Where(p => p.TenKP == "Phòng Khám Đông Y").Count();

                rep.ThuThuatBHYT_31.Value = dt1.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat).Where(p => p.TenKP == "Phòng Khám Nội").Where(p => p.DTuong == "BHYT").Count();
                rep.ThuThuatBHYT_32.Value = dt1.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat).Where(p => p.TenKP == "Phòng Khám Ngoại").Where(p => p.DTuong == "BHYT").Count();
                rep.ThuThuatBHYT_33.Value = dt1.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat).Where(p => p.TenKP == "Phòng Khám Sản").Where(p => p.DTuong == "BHYT").Count();
                rep.ThuThuatBHYT_34.Value = dt1.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat).Where(p => p.TenKP == "Phòng Khám RHM").Where(p => p.DTuong == "BHYT").Count();
                rep.ThuThuatBHYT_35.Value = dt1.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat).Where(p => p.TenKP == "Phòng Khám TMH").Where(p => p.DTuong == "BHYT").Count();
                rep.ThuThuatBHYT_36.Value = dt1.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat).Where(p => p.TenKP == "Phòng Khám Đông Y").Where(p => p.DTuong == "BHYT").Count();

                rep.ThuThuatDV_31.Value = dt1.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat).Where(p => p.TenKP == "Phòng Khám Nội").Where(p => p.DTuong == "Dịch vụ").Count();
                rep.ThuThuatDV_32.Value = dt1.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat).Where(p => p.TenKP == "Phòng Khám Ngoại").Where(p => p.DTuong == "Dịch vụ").Count();
                rep.ThuThuatDV_33.Value = dt1.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat).Where(p => p.TenKP == "Phòng Khám Sản").Where(p => p.DTuong == "Dịch vụ").Count();
                rep.ThuThuatDV_34.Value = dt1.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat).Where(p => p.TenKP == "Phòng Khám RHM").Where(p => p.DTuong == "Dịch vụ").Count();
                rep.ThuThuatDV_35.Value = dt1.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat).Where(p => p.TenKP == "Phòng Khám TMH").Where(p => p.DTuong == "Dịch vụ").Count();
                rep.ThuThuatDV_36.Value = dt1.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat).Where(p => p.TenKP == "Phòng Khám Đông Y").Where(p => p.DTuong == "Dịch vụ").Count();

                rep.BindingData();
                rep.CreateDocument();
                frmIn frm = new frmIn();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
            }
            else
            {
                List<bc> _bc = new List<bc>();
                _bc.Clear();
                bc moi = new bc();
                moi.ND = "Tổng số BN cho nghỉ điều trị ngoại trú";
                moi.BHYT = null;
                moi.DV = null;
                moi.Tong = null;
                moi.DonVi = "";
                _bc.Add(moi);

                bc moi1 = new bc();
                moi1.ND = "Tổng số ngày cho nghỉ điều trị ngoại trú";
                moi1.BHYT = null;
                moi1.DV = null;
                moi1.Tong = null;
                moi1.DonVi = "";
                _bc.Add(moi1);

                bc moi2 = new bc();
                moi2.ND = "Tổng số BN chuyển lên tuyến trên";
                moi2.BHYT = bn1.Where(p => p.NgayRa >= ngaytu && p.NgayRa <= ngayden).Where(p => p.Status == 1 && p.Tuyen < Tuyen).Where(p => p.DTuong == "BHYT").Count();
                moi2.DV = bn1.Where(p => p.NgayRa >= ngaytu && p.NgayRa <= ngayden).Where(p => p.Status == 1 && p.Tuyen < Tuyen).Where(p => p.DTuong == "Dịch vụ").Count();
                moi2.Tong = bn1.Where(p => p.NgayRa >= ngaytu && p.NgayRa <= ngayden).Where(p => p.Status == 1 && p.Tuyen < Tuyen).Count();
                moi2.DonVi = "Bệnh nhân";
                _bc.Add(moi2);

                bc moi3 = new bc();
                moi3.ND = "Tổng số BN nội soi TMH";
                moi3.BHYT = dt1.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoiTaiMuiHong).Where(p => p.DTuong == "BHYT").Count();
                moi3.DV = dt1.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoiTaiMuiHong).Where(p => p.DTuong == "Dịch vụ").Count();
                moi3.Tong = dt1.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoiTaiMuiHong).Count();
                moi3.DonVi = "Lượt";
                _bc.Add(moi3);

                bc moi4 = new bc();
                moi4.ND = "Tổng số BN nội soi tiêu hóa";
                moi4.BHYT = dt1.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoi).Where(p => p.DTuong == "BHYT").Count();
                moi4.DV = dt1.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoi).Where(p => p.DTuong == "Dịch vụ").Count();
                moi4.Tong = dt1.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoi).Count();
                moi4.DonVi = "Lượt";
                _bc.Add(moi4);

                bc moi5 = new bc();
                moi5.ND = "Tổng số BN chụp Xquang trong khám bệnh";
                moi5.BHYT = dt1.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_QuangCT || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_QuangKTS).Where(p => p.DTuong == "BHYT").Select(p => p.MaBNhan).Distinct().Count();
                moi5.DV = dt1.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_QuangCT || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_QuangKTS).Where(p => p.DTuong == "Dịch vụ").Select(p => p.MaBNhan).Distinct().Count();
                moi5.Tong = dt1.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_QuangCT || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_QuangKTS).Select(p => p.MaBNhan).Distinct().Count();
                moi5.DonVi = "Bệnh nhân";
                _bc.Add(moi5);

                bc moi6 = new bc();
                moi6.ND = "Tổng số BN xét nghiệm trong khám bệnh";
                moi6.BHYT = null;
                moi6.DV = null;
                moi6.Tong = null;
                moi6.DonVi = "Lượt";
                _bc.Add(moi6);

                bc moi7 = new bc();
                moi7.ND = "Tổng số BN siêu âm trong khám bệnh";
                moi7.BHYT = dt1.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler).Where(p => p.DTuong == "BHYT").Count();
                moi7.DV = dt1.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler).Where(p => p.DTuong == "Dịch vụ").Count();
                moi7.Tong = dt1.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler).Count();
                moi7.DonVi = "Lượt";
                _bc.Add(moi7);

                bc moi8 = new bc();
                moi8.ND = "Tổng số BN làm thủ thuật";
                moi8.BHYT = dt1.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat).Where(p => p.DTuong == "BHYT").Count();
                moi8.DV = dt1.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat).Where(p => p.DTuong == "Dịch vụ").Count();
                moi8.Tong = dt1.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat).Count();
                moi8.DonVi = "Lượt";
                _bc.Add(moi8);

                bc moi9 = new bc();
                moi9.ND = "Tổng số BN nạo thai, hút điều hòa KN";
                moi9.BHYT = null;
                moi9.DV = null;
                moi9.Tong = null;
                moi9.DonVi = "";
                _bc.Add(moi9);

                bc moi10 = new bc();
                moi10.ND = "Tổng số người đặt dụng cụng tránh thai";
                moi10.BHYT = null;
                moi10.DV = null;
                moi10.Tong = null;
                moi10.DonVi = "";
                _bc.Add(moi10);

                bc moi11 = new bc();
                moi11.ND = "Tổng số người tháo vòng";
                moi11.BHYT = null;
                moi11.DV = null;
                moi11.Tong = null;
                moi11.DonVi = "";
                _bc.Add(moi11);

                bc moi12 = new bc();
                moi12.ND = "Khám và kê đơn ngoại trú";
                moi12.BHYT = kb.Where(p => p.DTNT == false && p.NoiTru == 0).Where(p => p.DTuong == "BHYT").Count();
                moi12.DV = kb.Where(p => p.DTNT == false && p.NoiTru == 0).Where(p => p.DTuong == "Dịch vụ").Count(); ;
                moi12.Tong = kb.Where(p => p.DTNT == false && p.NoiTru == 0).Count();
                moi12.DonVi = "Lượt";
                _bc.Add(moi12);

                bc moi13 = new bc();
                moi13.ND = "Khám cấp cứu lưu bệnh nhân";
                moi13.BHYT = null;
                moi13.DV = null ;
                moi13.Tong = null;
                moi13.DonVi = "Người";
                _bc.Add(moi13);

                bc moi14 = new bc();
                moi14.ND = "Khám TMH";
                moi14.BHYT = kb.Where(p => p.TenKP == "Phòng Khám TMH").Where(p => p.DTuong == "BHYT").Select(p => p.MaBNhan).Distinct().Count();
                moi14.DV = kb.Where(p => p.TenKP == "Phòng Khám TMH").Where(p => p.DTuong == "Dịch vụ").Select(p => p.MaBNhan).Distinct().Count();
                moi14.Tong = kb.Where(p => p.TenKP == "Phòng Khám TMH").Select(p => p.MaBNhan).Distinct().Count();
                moi14.DonVi = "Người";
                _bc.Add(moi14);

                bc moi15 = new bc();
                moi15.ND = "Khám RHM";
                moi15.BHYT = kb.Where(p => p.TenKP == "Phòng Khám RHM").Where(p => p.DTuong == "BHYT").Select(p => p.MaBNhan).Distinct().Count();
                moi15.DV = kb.Where(p => p.TenKP == "Phòng Khám RHM").Where(p => p.DTuong == "Dịch vụ").Select(p => p.MaBNhan).Distinct().Count();
                moi15.Tong = kb.Where(p => p.TenKP == "Phòng Khám RHM").Select(p => p.MaBNhan).Distinct().Count();
                moi15.DonVi = "Người";
                _bc.Add(moi15);

                bc moi16 = new bc();
                moi16.ND = "Khám YHCT";
                moi16.BHYT = kb.Where(p => p.TenKP == "Phòng Khám Đông Y").Where(p => p.DTuong == "BHYT").Select(p => p.MaBNhan).Distinct().Count();
                moi16.DV = kb.Where(p => p.TenKP == "Phòng Khám Đông Y").Where(p => p.DTuong == "Dịch vụ").Select(p => p.MaBNhan).Distinct().Count();
                moi16.Tong = kb.Where(p => p.TenKP == "Phòng Khám Đông Y").Select(p => p.MaBNhan).Distinct().Count();
                moi16.DonVi = "Người";
                _bc.Add(moi16);

                bc moi17 = new bc();
                moi17.ND = "Tổng số khám thai";
                moi17.BHYT = kb.Where(p => p.MaICD.Contains("Z34")).Where(p => p.DTuong == "BHYT").Count();
                moi17.DV = kb.Where(p => p.MaICD.Contains("Z34")).Where(p => p.DTuong == "Dịch vụ").Count(); ;
                moi17.Tong = kb.Where(p => p.MaICD.Contains("Z34")).Count();
                moi17.DonVi = "Người";
                _bc.Add(moi17);

                bc moi18 = new bc();
                moi18.ND = "Tổng số khám phụ khoa được chữa";
                moi18.BHYT = null;
                moi18.DV = null;
                moi18.Tong = null;
                moi18.DonVi = "Người";
                _bc.Add(moi18);

                if (!donvi.Checked)
                {
                    BaoCao.rep_BCCTKCB rep = new BaoCao.rep_BCCTKCB();
                    rep.ngay.Value = "Từ ngày " + ngaytu.Day + "/" + ngaytu.Month + "/" + ngaytu.Year + " đến ngày " + ngayden.Day + "/" + ngayden.Month + "/" + ngayden.Year;
                    rep.KhamBHYT.Value = kb.Where(p => p.DTuong == "BHYT").Count();
                    rep.KhamDV.Value = kb.Where(p => p.DTuong == "Dịch vụ").Count();
                    rep.DV20.Value = cd1.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoi).Count();

                    rep.DataSource = _bc.ToList();
                    rep.BindingData();
                    rep.CreateDocument();
                    frmIn frm = new frmIn();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                }
                else
                {
                    BaoCao.rep_BCCTKCB_Donvi rep = new BaoCao.rep_BCCTKCB_Donvi();
                    rep.ngay.Value = "Từ ngày " + ngaytu.Day + "/" + ngaytu.Month + "/" + ngaytu.Year + " đến ngày " + ngayden.Day + "/" + ngayden.Month + "/" + ngayden.Year;
                    rep.KhamBHYT.Value = kb.Where(p => p.DTuong == "BHYT").Count();
                    rep.KhamDV.Value = kb.Where(p => p.DTuong == "Dịch vụ").Count();
                    rep.DV20.Value = cd1.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoi).Count();

                    rep.DataSource = _bc.ToList();
                    rep.BindingData();
                    rep.CreateDocument();
                    frmIn frm = new frmIn();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                }
            }
        }

        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (radioGroup1.SelectedIndex == 1)
            {
                donvi.Visible = true;
            }
            else
                donvi.Visible = false;
        }

    }
}