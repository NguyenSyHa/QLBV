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
    public partial class frm_BCCTKCBNgoaiTru : DevExpress.XtraEditors.XtraForm
    {
        public frm_BCCTKCBNgoaiTru()
        {
            InitializeComponent();
        }
        public class BC
        {
            private int bHYT, dichVu, tong;
            private string noiDung, sTT;

            public string NoiDung
            {
                get { return noiDung; }
                set { noiDung = value; }
            }
            public int Tong
            {
                get { return tong; }
                set { tong = value; }
            }

            public int DichVu
            {
                get { return dichVu; }
                set { dichVu = value; }
            }

            public int BHYT
            {
                get { return bHYT; }
                set { bHYT = value; }
            }

            public string STT
            {
                get { return sTT; }
                set { sTT = value; }
            }
        }
        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        List<BC> ds = new List<BC>();
        private void btnInBC_Click(object sender, EventArgs e)
        {
            ds.Clear();
            int thang = Convert.ToInt32(comboBoxEdit1.Text);
            int nam = Convert.ToInt32(comboBoxEdit2.Text);
            DateTime tungay = GetFirstDayOfMonth(thang,nam);
            DateTime denngay = DungChung.Ham.NgayDen(GetLastDayOfMonth(thang, nam));
            BC moi = new BC();
            BaoCao.rep_BCCTKCBNgoaiChu rep = new BaoCao.rep_BCCTKCBNgoaiChu();
            frmIn frm = new frmIn();
            rep.NGAYTHANG.Text = "THÁNG " + thang + " NĂM " + nam;
            var bn = (from a in _data.BNKBs.Where(p => p.NgayKham >= tungay && p.NgayKham <= denngay)
                      join b in _data.KPhongs.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.PhongKham) on a.MaKP equals b.MaKP
                      join c in _data.BenhNhans on a.MaBNhan equals c.MaBNhan 
                      join d in _data.TTboXungs on c.MaBNhan equals d.MaBNhan into k
                      from k1 in k.DefaultIfEmpty()
                      select new
                      {
                          a.IDKB,
                          a.MaKP,
                          a.MaBNhan,
                          c.Tuoi,
                          c.DTuong,
                          k1.NgoaiKieu
                      }).ToList();
            var dv = (from a in _data.DichVus
                      join b in _data.NhomDVs on a.IDNhom equals b.IDNhom
                      join c in _data.TieuNhomDVs on a.IdTieuNhom equals c.IdTieuNhom
                      select new { a.MaDV, a.TenDV, a.IDNhom, a.IdTieuNhom, b.TenNhom, c.TenRG, c.TenTN }).ToList();
            var cls = (from a in _data.CLS
                       join b in _data.ChiDinhs.Where(p => p.Status == 1).Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay) on a.IdCLS equals b.IdCLS
                       select new { a.MaBNhan, b.MaDV }).ToList();
            var bncls = (from a in cls
                         join b in dv on a.MaDV equals b.MaDV
                         join c in _data.BenhNhans on a.MaBNhan equals c.MaBNhan
                         select new { a, b, c }).ToList();
            string mabv = DungChung.Bien.MaBV;
            var bv = (from a in _data.BenhViens select new { a.MaBV, tuyen = (a.TuyenBV == "D") ? 1 : (a.TuyenBV == "C" ? 2 : (a.TuyenBV == "B" ? 3 : 4))}).ToList();
            int tuyen = bv.Where(p => p.MaBV == mabv).Max(p => p.tuyen);
            var rv = (from c in bv
                      join a in _data.RaViens.Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay) on c.MaBV equals a.MaBVC
                      join b in _data.BenhNhans on a.MaBNhan equals b.MaBNhan
                      select new { a,b,c.tuyen}).ToList();
            

            #region 1. Tổng số lượt khám bệnh
            moi = new BC();
            moi.STT = "1";
            moi.NoiDung = "Tổng số lượt khám bệnh";
            moi.BHYT = bn.Where(p => p.DTuong == "BHYT").Count();
            moi.DichVu = bn.Where(p => p.DTuong == "Dịch vụ").Count();
            moi.Tong = bn.Count;
            ds.Add(moi);

            moi = new BC();
            moi.STT = "";
            moi.NoiDung = "1. <=6";
            moi.BHYT = bn.Where(p => p.DTuong == "BHYT").Where(p => p.Tuoi <= 6).Count();
            moi.DichVu = bn.Where(p => p.DTuong == "Dịch vụ").Where(p => p.Tuoi <= 6).Count();
            moi.Tong = bn.Where(p => p.Tuoi <= 6).Count();
            ds.Add(moi);

            moi = new BC();
            moi.STT = "";
            moi.NoiDung = "2. >=60";
            moi.BHYT = bn.Where(p => p.DTuong == "BHYT").Where(p => p.Tuoi >= 60).Count();
            moi.DichVu = bn.Where(p => p.DTuong == "Dịch vụ").Where(p => p.Tuoi >= 60).Count();
            moi.Tong = bn.Where(p => p.Tuoi >= 60).Count();
            ds.Add(moi);

            moi = new BC();
            moi.STT = "";
            moi.NoiDung = "3. Người nước ngoài";
            moi.BHYT = bn.Where(p => p.DTuong == "BHYT" && p.NgoaiKieu != "Việt Nam").Count();
            moi.DichVu = bn.Where(p => p.DTuong == "Dịch vụ" && p.NgoaiKieu != "Việt Nam").Count();
            moi.Tong = bn.Where(p => p.NgoaiKieu != "Việt Nam").Count();
            ds.Add(moi);
            #endregion

            #region 2. Tổng số khám sức khỏe
            moi = new BC();
            moi.STT = "2";
            moi.NoiDung = "Tổng số khám sức khỏe";
            //moi.BHYT = bn.Where(p => p.DTuong == "BHYT").Count();
            //moi.DichVu = bn.Where(p => p.DTuong == "Dịch vụ").Count();
            //moi.Tong = bn.Count;
            ds.Add(moi);

            moi = new BC();
            moi.STT = "";
            moi.NoiDung = "1. Khám sức khỏe thu phí trực tiếp";
            //moi.BHYT = bn.Where(p => p.DTuong == "BHYT").Where(p => p.Tuoi <= 6).Count();
            //moi.DichVu = bn.Where(p => p.DTuong == "Dịch vụ").Where(p => p.Tuoi <= 6).Count();
            //moi.Tong = bn.Where(p => p.Tuoi <= 6).Count();
            ds.Add(moi);

            moi = new BC();
            moi.STT = "";
            moi.NoiDung = "2. Khám hợp đồng tuyển dụng";
            //moi.BHYT = bn.Where(p => p.DTuong == "BHYT").Where(p => p.Tuoi >= 60).Count();
            //moi.DichVu = bn.Where(p => p.DTuong == "Dịch vụ").Where(p => p.Tuoi >= 60).Count();
            //moi.Tong = bn.Where(p => p.Tuoi >= 60).Count();
            ds.Add(moi);

            moi = new BC();
            moi.STT = "";
            moi.NoiDung = "3. Khám sức khỏe cho người nước ngoài";
            //moi.BHYT = bn.Where(p => p.DTuong == "BHYT" && p.NgoaiKieu != "Việt Nam").Count();
            //moi.DichVu = bn.Where(p => p.DTuong == "Dịch vụ" && p.NgoaiKieu != "Việt Nam").Count();
            //moi.Tong = bn.Where(p => p.NgoaiKieu != "Việt Nam").Count();
            ds.Add(moi);
            #endregion

            #region 3. Tổng số bệnh nhân cho nghỉ điều trị ngoại trú
            moi = new BC();
            moi.STT = "3";
            moi.NoiDung = "Tổng số bệnh nhân cho nghỉ điều trị ngoại trú";
            //moi.BHYT = bn.Where(p => p.DTuong == "BHYT").Count();
            //moi.DichVu = bn.Where(p => p.DTuong == "Dịch vụ").Count();
            //moi.Tong = bn.Count;
            ds.Add(moi);
            #endregion

            #region 4. Tổng số bệnh nhân cho nghỉ điều trị ngoại trú
            moi = new BC();
            moi.STT = "4";
            moi.NoiDung = "Tổng số ngày nghỉ điều trị ngoại trú";
            //moi.BHYT = bn.Where(p => p.DTuong == "BHYT").Count();
            //moi.DichVu = bn.Where(p => p.DTuong == "Dịch vụ").Count();
            //moi.Tong = bn.Count;
            ds.Add(moi);
            #endregion

            #region 5. Tổng số bệnh nhân chuyển lên tuyến trên
            moi = new BC();
            moi.STT = "5";
            moi.NoiDung = "Tổng số bệnh nhân chuyển lên tuyến trên";
            moi.BHYT = rv.Where(p => p.b.DTuong == "BHYT" && p.tuyen > tuyen).Count();
            moi.DichVu = rv.Where(p => p.b.DTuong == "Dịch vụ" && p.tuyen > tuyen).Count();
            moi.Tong = rv.Where(p => p.tuyen > tuyen).Count();
            ds.Add(moi);
            #endregion

            #region 6. Tổng số bệnh nhân chụp CT Scanner trong khám bệnh
            moi = new BC();
            moi.STT = "6";
            moi.NoiDung = "Tổng số bệnh nhân chụp CT Scanner trong khám bệnh";
            moi.BHYT = bncls.Where(p => p.c.DTuong == "BHYT" ).Where(p => p.b.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_QuangCT).GroupBy(p => p.a.MaBNhan).Count();
            moi.DichVu = bncls.Where(p => p.c.DTuong == "Dịch vụ").Where(p => p.b.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_QuangCT).GroupBy(p => p.a.MaBNhan).Count();
            moi.Tong = bncls.Where(p => p.b.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_QuangCT).GroupBy(p => p.a.MaBNhan).Count();
            ds.Add(moi);
            #endregion

            #region 7. Tổng số bệnh nhân chụp Xquang trong khám bệnh
            moi = new BC();
            moi.STT = "7";
            moi.NoiDung = "Tổng số bệnh nhân chụp Xquang trong khám bệnh";
            moi.BHYT = bncls.Where(p => p.c.DTuong == "BHYT").Where(p => p.b.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang).GroupBy(p => p.a.MaBNhan).Count();
            moi.DichVu = bncls.Where(p => p.c.DTuong == "Dịch vụ").Where(p => p.b.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang).GroupBy(p => p.a.MaBNhan).Count();
            moi.Tong = bncls.Where(p => p.b.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang).GroupBy(p => p.a.MaBNhan).Count();
            ds.Add(moi);
            #endregion

            #region 8. Tổng số bệnh nhân Xét nghiệm trong khám bệnh
            moi = new BC();
            moi.STT = "7";
            moi.NoiDung = "Tổng số bệnh nhân Xét nghiệm trong khám bệnh";
            moi.BHYT = bncls.Where(p => p.c.DTuong == "BHYT").Where(p => p.b.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNDom|| 
                p.b.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau || p.b.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMienDich ||
                p.b.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhNTTo || p.b.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc ||
                p.b.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNKhac || p.b.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNMoBenhHoc ||
                    p.b.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu || p.b.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNTeBaoNuocDich ||
                    p.b.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNViSinh ).GroupBy(p => p.a.MaBNhan).Count();
            moi.DichVu = bncls.Where(p => p.c.DTuong == "Dịch vụ").Where(p => p.b.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNDom ||
                p.b.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau || p.b.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMienDich ||
                p.b.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhNTTo || p.b.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc ||
                p.b.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNKhac || p.b.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNMoBenhHoc ||
                    p.b.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu || p.b.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNTeBaoNuocDich ||
                    p.b.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNViSinh).GroupBy(p => p.a.MaBNhan).Count();
            moi.Tong = bncls.Where(p => p.b.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNDom ||
                p.b.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau || p.b.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMienDich ||
                p.b.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhNTTo || p.b.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc ||
                p.b.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNKhac || p.b.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNMoBenhHoc ||
                    p.b.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu || p.b.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNTeBaoNuocDich ||
                    p.b.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNViSinh).GroupBy(p => p.a.MaBNhan).Count();
            ds.Add(moi);
            #endregion

            #region 9. Tổng số bệnh nhân Siêu âm trong khám bệnh
            moi = new BC();
            moi.STT = "9";
            moi.NoiDung = "Tổng số bệnh nhân Siêu ấm trong khám bệnh";
            moi.BHYT = bncls.Where(p => p.c.DTuong == "BHYT").Where(p => p.b.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm || p.b.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler).GroupBy(p => p.a.MaBNhan).Count();
            moi.DichVu = bncls.Where(p => p.c.DTuong == "Dịch vụ").Where(p => p.b.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm || p.b.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler).GroupBy(p => p.a.MaBNhan).Count();
            moi.Tong = bncls.Where(p => p.b.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm || p.b.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler).GroupBy(p => p.a.MaBNhan).Count();
            ds.Add(moi);
            #endregion

            #region 10. Tổng số bệnh nhân nội saoi Tai Mũi Họng
            moi = new BC();
            moi.STT = "10";
            moi.NoiDung = "Tổng số bệnh nhân nội saoi Tai Mũi Họng";
            moi.BHYT = bncls.Where(p => p.c.DTuong == "BHYT").Where(p => p.b.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoiTaiMuiHong).GroupBy(p => p.a.MaBNhan).Count();
            moi.DichVu = bncls.Where(p => p.c.DTuong == "Dịch vụ").Where(p => p.b.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoiTaiMuiHong).GroupBy(p => p.a.MaBNhan).Count();
            moi.Tong = bncls.Where(p => p.b.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoiTaiMuiHong).GroupBy(p => p.a.MaBNhan).Count();
            ds.Add(moi);
            #endregion

            #region 11. Tổng số bệnh nhân có bệnh án điều trị ngoại trú
            moi = new BC();
            moi.STT = "11";
            moi.NoiDung = "Tổng số bệnh nhân có bệnh án điều trị ngoại trú";
            //moi.BHYT = bncls.Where(p => p.c.DTuong == "BHYT").Where(p => p.b.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoiTaiMuiHong).GroupBy(p => p.a.MaBNhan).Count();
            //moi.DichVu = bncls.Where(p => p.c.DTuong == "Dịch vụ").Where(p => p.b.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoiTaiMuiHong).GroupBy(p => p.a.MaBNhan).Count();
            //moi.Tong = bncls.Where(p => p.b.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoiTaiMuiHong).GroupBy(p => p.a.MaBNhan).Count();
            ds.Add(moi);
            #endregion

            #region 12. Tổng số bệnh nhân Điện tim
            moi = new BC();
            moi.STT = "12";
            moi.NoiDung = "Tổng số bệnh nhân Điện tim";
            moi.BHYT = bncls.Where(p => p.c.DTuong == "BHYT").Where(p => p.b.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim).GroupBy(p => p.a.MaBNhan).Count();
            moi.DichVu = bncls.Where(p => p.c.DTuong == "Dịch vụ").Where(p => p.b.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim).GroupBy(p => p.a.MaBNhan).Count();
            moi.Tong = bncls.Where(p => p.b.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim).GroupBy(p => p.a.MaBNhan).Count();
            ds.Add(moi);
            #endregion

            rep.DataSource = ds.ToList();
            rep.BindingData();
            rep.CreateDocument();
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();
        }
        public static DateTime GetFirstDayOfMonth(int iMonth, int iYear)
        {
            DateTime dtResult = new DateTime(iYear, iMonth, 1);
            dtResult = dtResult.AddDays((-dtResult.Day) + 1);
            return dtResult;
        }
        public static DateTime GetLastDayOfMonth(int iMonth, int iYear)
        {
            DateTime dtResult = new DateTime(iYear, iMonth, 1);
            dtResult = dtResult.AddMonths(1);
            dtResult = dtResult.AddDays(-(dtResult.Day));
            return dtResult;
        }

        private void btnInBC_KeyDown(object sender, KeyEventArgs e)
        {
            btnInBC_Click(sender, e);
        }
    }
}