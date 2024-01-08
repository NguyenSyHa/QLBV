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
    public partial class frm_BCCongTacKCB_12122 : DevExpress.XtraEditors.XtraForm
    {
        public frm_BCCongTacKCB_12122()
        {
            InitializeComponent();
        }

        private void frm_BCCongTacKCB_12122_Load(object sender, EventArgs e)
        {
            lupTuNgay.DateTime = DateTime.Now;
            lupDenNgay.DateTime = DateTime.Now;

        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            DateTime tungay = DungChung.Ham.NgayTu(lupTuNgay.DateTime);
            DateTime denngay = DungChung.Ham.NgayDen(lupDenNgay.DateTime);
            var qbn = (from bn in data.BenhNhans.Where(p => p.NNhap >= tungay && p.NNhap <= denngay)
                       join vv in data.VaoViens on bn.MaBNhan equals vv.MaBNhan into kq
                       from kq1 in kq.DefaultIfEmpty()
                       join rv in data.RaViens on bn.MaBNhan equals rv.MaBNhan into kqrv
                       from kq2 in kqrv.DefaultIfEmpty()
                       select new { bn.MaBNhan, bn.MaBV, bn.DTuong, bn.NoiTru,bn.Tuoi, bn.DTNT, bn.MaDTuong, kq1, kq2 }).ToList();
            var qbh = from bn in qbn.Where(p => p.DTuong == "BHYT") select new { bn.MaBNhan, bn.DTuong, bn.NoiTru, bn.Tuoi, bn.DTNT, bn.MaBV,MaDTuong = bn.MaDTuong == null ? "" : bn.MaDTuong.Trim().ToUpper(), bn.kq1, bn.kq2 };
            var qbndv = qbn.Where(p=>p.DTuong != "BHYT");
            var qdv = (from dv in data.DichVus join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom 
                       join n in data.NhomDVs on tn.IDNhom equals n.IDNhom
                       select new {dv.MaDV, tn.TenRG, n.TenNhomCT}).ToList();

            List<BC> _lbc = new List<BC>();
            BC bc = new BC();
            bc.stt = "1";
            bc.NoiDung = "Tổng số lượt khám";
            bc.DonVi = "Lượt";
            bc.Tong = qbn.Count();
            bc.TE6 = qbh.Where(p => p.Tuoi < 6 || p.Tuoi == null).Where(p=>p.MaDTuong != "HN").Count();
            bc.TE15 = qbh.Where(p => p.Tuoi >= 6 && p.Tuoi < 15).Where(p => p.MaDTuong != "HN").Count();
            bc.BH60 = qbh.Where(p => p.Tuoi > 60).Where(p => p.MaDTuong != "HN").Count();
            bc.HN = qbh.Where(p =>  p.MaDTuong == "HN" ).Count();
            bc.BHKhac = qbh.Where(p => p.MaDTuong != "HN" && p.Tuoi >= 15 && p.Tuoi <= 60).Count();
            bc.DV = qbndv.Count();
            _lbc.Add(bc);

            bc = new BC();
            bc.stt = "2";
            bc.NoiDung = "Tổng số bệnh nhân kê đơn";
            bc.DonVi = "Lượt";
            bc.Tong = qbn.Where(p=>p.kq1 == null).Count();
            bc.TE6 = qbh.Where(p => p.kq1 == null).Where(p => p.Tuoi < 6 || p.Tuoi == null).Where(p => p.MaDTuong != "HN").Count();
            bc.TE15 = qbh.Where(p => p.kq1 == null).Where(p => p.Tuoi >= 6 && p.Tuoi < 15).Where(p => p.MaDTuong != "HN").Count();
            bc.BH60 = qbh.Where(p => p.kq1 == null).Where(p => p.Tuoi > 60).Where(p => p.MaDTuong != "HN").Count();
            bc.HN = qbh.Where(p => p.kq1 == null).Where(p => p.MaDTuong == "HN").Count();
            bc.BHKhac = qbh.Where(p => p.kq1 == null).Where(p => p.MaDTuong != "HN" && p.Tuoi >= 15 && p.Tuoi <= 60).Count();
            bc.DV = qbndv.Where(p => p.kq1 == null).Count();
            _lbc.Add(bc);

            bc = new BC();
            bc.stt = "3";
            bc.NoiDung = "Tổng số bệnh nhân vào viện";
            bc.DonVi = "Bệnh nhân";
            bc.Tong = qbn.Where(p => p.kq1 != null).Count();
            bc.TE6 = qbh.Where(p => p.kq1 != null).Where(p => p.Tuoi < 6 || p.Tuoi == null).Where(p => p.MaDTuong != "HN").Count();
            bc.TE15 = qbh.Where(p => p.kq1 != null).Where(p => p.Tuoi >= 6 && p.Tuoi < 15).Where(p => p.MaDTuong != "HN").Count();
            bc.BH60 = qbh.Where(p => p.kq1 != null).Where(p => p.Tuoi > 60).Where(p => p.MaDTuong != "HN").Count();
            bc.HN = qbh.Where(p => p.kq1 != null).Where(p => p.MaDTuong == "HN").Count();
            bc.BHKhac = qbh.Where(p => p.kq1 != null).Where(p => p.MaDTuong != "HN" && p.Tuoi >= 15 && p.Tuoi <= 60).Count();
            bc.DV = qbndv.Where(p => p.kq1 != null).Count();
            _lbc.Add(bc);

            var qdt = (from bn in data.BenhNhans.Where(p => p.NNhap >= tungay && p.NNhap <= denngay)
                         join dt in data.DThuocs on bn.MaBNhan equals dt.MaBNhan
                         join dtct in data.DThuoccts on dt.IDDon equals dtct.IDDon
                         select new { bn.MaBNhan,bn.DTuong, dt.IDDon, dtct.MaDV, dtct.SoLuong }).ToList();
            var qdt1 = (from dt in qdt
                         join dv in qdv on dt.MaDV equals dv.MaDV
                         join bn in qbn on dt.MaBNhan equals bn.MaBNhan
                         select new { dt.MaBNhan, dt.DTuong,dv.MaDV, dt.SoLuong, dv.TenRG, dv.TenNhomCT, bn.kq1, bn.kq2, bn.Tuoi, MaDTuong = bn.MaDTuong == null ? "" : bn.MaDTuong.Trim().ToUpper() }).ToList();
            var qCPKhac = qdt1.Where(p => p.TenNhomCT != "Khám bệnh").Select(p=>p.MaBNhan).Distinct().ToList();
            var qkham = qdt1.Where(p => qCPKhac == null ? true : !qCPKhac.Contains(p.MaBNhan)).ToList();
            bc = new BC();
            bc.stt = "4";
            bc.NoiDung = "TS BN được khám (nhưng không điều trị + kê đơn + cận lâm sàng)";
            bc.DonVi = "Lượt";
            bc.Tong = qkham.Where(p => p.kq1 == null).Count();
            bc.TE6 = qkham.Where(p => p.DTuong == "BHYT").Where(p => p.kq1 == null).Where(p => p.Tuoi < 6 || p.Tuoi == null).Where(p => p.MaDTuong != "HN").Count();
            bc.TE15 = qkham.Where(p => p.DTuong == "BHYT").Where(p => p.kq1 == null).Where(p => p.Tuoi >= 6 && p.Tuoi < 15).Where(p => p.MaDTuong != "HN").Count();
            bc.BH60 = qkham.Where(p => p.DTuong == "BHYT").Where(p => p.kq1 == null).Where(p => p.Tuoi > 60).Where(p => p.MaDTuong != "HN").Count();
            bc.HN = qkham.Where(p => p.DTuong == "BHYT").Where(p => p.kq1 == null).Where(p => p.MaDTuong == "HN").Count();
            bc.BHKhac = qkham.Where(p => p.DTuong == "BHYT").Where(p => p.kq1 == null).Where(p => p.MaDTuong != "HN" && p.Tuoi >= 15 && p.Tuoi <= 60).Count();
            bc.DV = qkham.Where(p => p.DTuong != "BHYT").Where(p => p.kq1 == null).Count();
            _lbc.Add(bc);

            var qcls = (from cls in data.CLS.Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay)
                        join bn in data.BenhNhans on cls.MaBNhan equals bn.MaBNhan
                        join cd in data.ChiDinhs.Where(p=>p.Status == 1) on cls.IdCLS equals cd.IdCLS
                        join dv in data.DichVus on cd.MaDV equals dv.MaDV
                        join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                        select new { cls.MaBNhan, dv.TenDV, dv.MaDV, tn.TenRG, tn.IdTieuNhom,tn.IDNhom, bn.Tuoi, MaDTuong = bn.MaDTuong== null ? "" : bn.MaDTuong.Trim().ToUpper()  , bn.DTuong}).ToList();
            var qclsBH = qcls.Where(p => p.DTuong == "BHYT").ToList();

            bc = new BC();
            bc.stt = "5";
            bc.NoiDung = "Xét nghiệm chung";
            bc.DonVi = "Lượt";
            bc.Tong = qcls.Where(p => p.IDNhom == 1).Count();
            bc.TE6 = qclsBH.Where(p => p.IDNhom == 1).Where(p => p.Tuoi < 6 || p.Tuoi == null).Where(p => p.MaDTuong != "HN").Count();
            bc.TE15 = qclsBH.Where(p => p.IDNhom == 1).Where(p => p.Tuoi >= 6 && p.Tuoi < 15).Where(p => p.MaDTuong != "HN").Count();
            bc.BH60 = qclsBH.Where(p => p.IDNhom == 1).Where(p => p.Tuoi > 60).Where(p => p.MaDTuong != "HN").Count();
            bc.HN = qclsBH.Where(p => p.IDNhom == 1).Where(p => p.MaDTuong == "HN").Count();
            bc.BHKhac = qclsBH.Where(p => p.IDNhom == 1).Where(p => p.MaDTuong != "HN" && p.Tuoi >= 15 && p.Tuoi <= 60).Count();
            bc.DV = qcls.Where(p => p.DTuong != "BHYT").Where(p => p.IDNhom == 1).Count();
            _lbc.Add(bc);

            bc = new BC();
            bc.stt = "";
            bc.NoiDung = "Tổng số thủ thuật";
            bc.DonVi = "Lượt";
            bc.Tong = qcls.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat).Count();
            bc.TE6 = qclsBH.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat).Where(p => p.Tuoi < 6 || p.Tuoi == null).Where(p => p.MaDTuong != "HN").Count();
            bc.TE15 = qclsBH.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat).Where(p => p.Tuoi >= 6 && p.Tuoi < 15).Where(p => p.MaDTuong != "HN").Count();
            bc.BH60 = qclsBH.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat).Where(p => p.Tuoi > 60).Where(p => p.MaDTuong != "HN").Count();
            bc.HN = qclsBH.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat).Where(p => p.MaDTuong == "HN").Count();
            bc.BHKhac = qclsBH.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat).Where(p => p.MaDTuong != "HN" && p.Tuoi >= 15 && p.Tuoi <= 60).Count();
            bc.DV = qcls.Where(p => p.DTuong != "BHYT").Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat).Where(p => p.DTuong != "BHYT").Count();
            _lbc.Add(bc);

            bc = new BC();
            bc.stt = "6";
            bc.NoiDung = "Siêu âm";
            bc.DonVi = "Lượt";
            bc.Tong = qcls.Where(p => p.TenRG.Contains("Siêu âm")).Count();
            bc.TE6 = qclsBH.Where(p => p.TenRG.Contains("Siêu âm")).Where(p => p.Tuoi < 6 || p.Tuoi == null).Where(p => p.MaDTuong != "HN").Count();
            bc.TE15 = qclsBH.Where(p => p.TenRG.Contains("Siêu âm")).Where(p => p.Tuoi >= 6 && p.Tuoi < 15).Where(p => p.MaDTuong != "HN").Count();
            bc.BH60 = qclsBH.Where(p => p.TenRG.Contains("Siêu âm")).Where(p => p.Tuoi > 60).Where(p => p.MaDTuong != "HN").Count();
            bc.HN = qclsBH.Where(p => p.TenRG.Contains("Siêu âm")).Where(p => p.MaDTuong == "HN").Count();
            bc.BHKhac = qclsBH.Where(p => p.TenRG.Contains("Siêu âm")).Where(p => p.MaDTuong != "HN" && p.Tuoi >= 15 && p.Tuoi <= 60).Count();
            bc.DV = qcls.Where(p => p.DTuong != "BHYT").Where(p => p.TenRG.Contains("Siêu âm")).Where(p => p.DTuong != "BHYT").Count();
            _lbc.Add(bc);


            bc = new BC();
            bc.stt = "7";
            bc.NoiDung = "Đo chức năng hô hấp";
            bc.DonVi = "Lượt";
            bc.Tong = qcls.Where(p => p.TenRG == "Chức năng hô hấp").Count();
            bc.TE6 = qclsBH.Where(p => p.TenRG == "Chức năng hô hấp").Where(p => p.Tuoi < 6 || p.Tuoi == null).Where(p => p.MaDTuong != "HN").Count();
            bc.TE15 = qclsBH.Where(p => p.TenRG == "Chức năng hô hấp").Where(p => p.Tuoi >= 6 && p.Tuoi < 15).Where(p => p.MaDTuong != "HN").Count();
            bc.BH60 = qclsBH.Where(p => p.TenRG == "Chức năng hô hấp").Where(p => p.Tuoi > 60).Where(p => p.MaDTuong != "HN").Count();
            bc.HN = qclsBH.Where(p => p.TenRG == "Chức năng hô hấp").Where(p => p.MaDTuong == "HN").Count();
            bc.BHKhac = qclsBH.Where(p => p.TenRG == "Chức năng hô hấp").Where(p => p.MaDTuong != "HN" && p.Tuoi >= 15 && p.Tuoi <= 60).Count();
            bc.DV = qcls.Where(p => p.DTuong != "BHYT").Where(p => p.TenRG == "Chức năng hô hấp").Where(p => p.DTuong != "BHYT").Count();
            _lbc.Add(bc);

            bc = new BC();
            bc.stt = "8";
            bc.NoiDung = "Điện tim";
            bc.DonVi = "Lượt";
            bc.Tong = qcls.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim).Count();
            bc.TE6 = qclsBH.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim).Where(p => p.Tuoi < 6 || p.Tuoi == null).Where(p => p.MaDTuong != "HN").Count();
            bc.TE15 = qclsBH.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim).Where(p => p.Tuoi >= 6 && p.Tuoi < 15).Where(p => p.MaDTuong != "HN").Count();
            bc.BH60 = qclsBH.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim).Where(p => p.Tuoi > 60).Where(p => p.MaDTuong != "HN").Count();
            bc.HN = qclsBH.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim).Where(p => p.MaDTuong == "HN").Count();
            bc.BHKhac = qclsBH.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim).Where(p => p.MaDTuong != "HN" && p.Tuoi >= 15 && p.Tuoi <= 60).Count();
            bc.DV = qcls.Where(p => p.DTuong != "BHYT").Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim).Where(p => p.DTuong != "BHYT").Count();
            _lbc.Add(bc);

            bc = new BC();
            bc.stt = "9";
            bc.NoiDung = "Nội soi Tai Mũi Họng";
            bc.DonVi = "Lượt";
            bc.Tong = qcls.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoiTaiMuiHong).Count();
            bc.TE6 = qclsBH.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoiTaiMuiHong).Where(p => p.Tuoi < 6 || p.Tuoi == null).Where(p => p.MaDTuong != "HN").Count();
            bc.TE15 = qclsBH.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoiTaiMuiHong).Where(p => p.Tuoi >= 6 && p.Tuoi < 15).Where(p => p.MaDTuong != "HN").Count();
            bc.BH60 = qclsBH.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoiTaiMuiHong).Where(p => p.Tuoi > 60).Where(p => p.MaDTuong != "HN").Count();
            bc.HN = qclsBH.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoiTaiMuiHong).Where(p => p.MaDTuong == "HN").Count();
            bc.BHKhac = qclsBH.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoiTaiMuiHong).Where(p => p.MaDTuong != "HN" && p.Tuoi >= 15 && p.Tuoi <= 60).Count();
            bc.DV = qcls.Where(p => p.DTuong != "BHYT").Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoiTaiMuiHong).Where(p => p.DTuong != "BHYT").Count();
            _lbc.Add(bc);


            bc = new BC();
            bc.stt = "10";
            bc.NoiDung = "Chụp XQuang";
            bc.DonVi = "Lượt";
            bc.Tong = qcls.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang).Count();
            bc.TE6 = qclsBH.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang).Where(p => p.Tuoi < 6 || p.Tuoi == null).Where(p => p.MaDTuong != "HN").Count();
            bc.TE15 = qclsBH.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang).Where(p => p.Tuoi >= 6 && p.Tuoi < 15).Where(p => p.MaDTuong != "HN").Count();
            bc.BH60 = qclsBH.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang).Where(p => p.Tuoi > 60).Where(p => p.MaDTuong != "HN").Count();
            bc.HN = qclsBH.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang).Where(p => p.MaDTuong == "HN").Count();
            bc.BHKhac = qclsBH.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang).Where(p => p.MaDTuong != "HN" && p.Tuoi >= 15 && p.Tuoi <= 60).Count();
            bc.DV = qcls.Where(p => p.DTuong != "BHYT").Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang).Where(p => p.DTuong != "BHYT").Count();
            _lbc.Add(bc);

          
            var qbnkb = (from bn in data.BenhNhans.Where(p => p.NNhap >= tungay && p.NNhap <= denngay)
                         join bnkb in data.BNKBs on bn.MaBNhan equals bnkb.MaBNhan
                         join ck in data.ChuyenKhoas on bnkb.MaCK equals ck.MaCK
                         select new { bn.MaBNhan, bn.MaBV, bn.DTuong, bn.NoiTru, bn.Tuoi, bn.DTNT, MaDTuong = bn.MaDTuong == null ? "" : bn.MaDTuong.Trim().ToUpper(),ck.TenCK ,MaICD = bnkb.MaICD + ";" +bnkb.MaICD2}).ToList();
            var qYHCT = (from bn in qbnkb.Where(p => p.TenCK == "Đông y" || p.TenCK.Contains("YHCT")) join cls in data.CLS on bn.MaBNhan equals cls.MaBNhan group bn by new { bn.MaBNhan, bn.MaBV, bn.DTuong, bn.NoiTru, bn.Tuoi, bn.DTNT, MaDTuong = bn.MaDTuong == null ? "" : bn.MaDTuong.Trim().ToUpper() }  into kq select new { kq.Key.MaBNhan, kq.Key.MaBV, kq.Key.DTuong, kq.Key.NoiTru, kq.Key.Tuoi, kq.Key.DTNT, kq.Key.MaDTuong }).ToList();
            bc = new BC();
            bc.stt = "11";
            bc.NoiDung = "Số bệnh nhân dùng y học cổ truyền kết hợp YHHĐ";
            bc.DonVi = "Bệnh nhân";
            bc.Tong = qYHCT.Count();
            bc.TE6 = qYHCT.Where(p => p.DTuong == "BHYT").Where(p => p.Tuoi < 6 || p.Tuoi == null).Where(p => p.MaDTuong != "HN").Count();
            bc.TE15 = qYHCT.Where(p => p.DTuong == "BHYT").Where(p => p.Tuoi >= 6 && p.Tuoi < 15).Where(p => p.MaDTuong != "HN").Count();
            bc.BH60 = qYHCT.Where(p => p.DTuong == "BHYT").Where(p => p.Tuoi > 60).Where(p => p.MaDTuong != "HN").Count();
            bc.HN = qYHCT.Where(p => p.DTuong == "BHYT").Where(p => p.MaDTuong == "HN").Count();
            bc.BHKhac = qYHCT.Where(p => p.DTuong == "BHYT").Where(p => p.MaDTuong != "HN" && p.Tuoi >= 15 && p.Tuoi <= 60).Count();
            bc.DV = qYHCT.Where(p => p.DTuong != "BHYT").Count();
            _lbc.Add(bc);

            bc = new BC();
            bc.stt = "12";
            bc.NoiDung = "Tổng số bệnh nhân điều trị ngoại trú, trong đó";
            bc.DonVi = "Bệnh nhân";
            bc.Tong = qbn.Where(p=>p.NoiTru == 0 && p.DTNT == true).Count();
            bc.TE6 = qbh.Where(p => p.NoiTru == 0 && p.DTNT == true).Where(p => p.Tuoi < 6 || p.Tuoi == null).Where(p => p.MaDTuong != "HN").Count();
            bc.TE15 = qbh.Where(p => p.NoiTru == 0 && p.DTNT == true).Where(p => p.Tuoi >= 6 && p.Tuoi < 15).Where(p => p.MaDTuong != "HN").Count();
            bc.BH60 = qbh.Where(p => p.NoiTru == 0 && p.DTNT == true).Where(p => p.Tuoi > 60).Where(p => p.MaDTuong != "HN").Count();
            bc.HN = qbh.Where(p => p.NoiTru == 0 && p.DTNT == true).Where(p => p.MaDTuong == "HN").Count();
            bc.BHKhac = qbh.Where(p => p.NoiTru == 0 && p.DTNT == true).Where(p => p.MaDTuong != "HN" && p.Tuoi >= 15 && p.Tuoi <= 60).Count();
            bc.DV = qbndv.Where(p => p.NoiTru == 0 && p.DTNT == true).Count();
            _lbc.Add(bc);

            // thiếu mục 14,15

            //var qbnkb = (from bn in data.BenhNhans.Where(p => p.NNhap >= tungay && p.NNhap <= denngay)
            //             join bnkb in data.BNKBs on bn.MaBNhan equals bnkb.MaBNhan
            //             join ck in data.ChuyenKhoas on bnkb.MaCK equals ck.MaCK
            //             select new { bn.MaBNhan, bn.MaBV, bn.DTuong, bn.NoiTru, bn.Tuoi, bn.DTNT, bn.MaDTuong, ck.TenCK, MaICD = bnkb.MaICD + ";" + bnkb.MaICD2 }).ToList();

            var qLao =(from bn in qbnkb.Where(p=>p.MaICD.Contains("A15") || p.MaICD.Contains("A17") || p.MaICD.Contains("A16")|| p.MaICD.Contains("A18")|| p.MaICD.Contains("O98")|| p.MaICD.Contains("P37") || p.MaICD.Contains("P19") || p.MaICD.Contains("Z03") || p.MaICD.Contains("Z11"))group bn by new { bn.MaBNhan, bn.MaBV, bn.DTuong, bn.NoiTru, bn.Tuoi, bn.DTNT, bn.MaDTuong }  into kq select new { kq.Key.MaBNhan, kq.Key.MaBV, kq.Key.DTuong, kq.Key.NoiTru, kq.Key.Tuoi, kq.Key.DTNT, kq.Key.MaDTuong }).ToList();
            bc = new BC();
            bc.stt = "";
            bc.NoiDung = "Lao";
            bc.DonVi = "Bệnh nhân";
            bc.Tong = qLao.Where(p => p.NoiTru == 0 && p.DTNT == true).Count();
            bc.TE6 = qLao.Where(p => p.DTuong == "BHYT").Where(p => p.NoiTru == 0 && p.DTNT == true).Where(p => p.Tuoi < 6 || p.Tuoi == null).Where(p => p.MaDTuong != "HN").Count();
            bc.TE15 = qLao.Where(p => p.DTuong == "BHYT").Where(p => p.NoiTru == 0 && p.DTNT == true).Where(p => p.Tuoi >= 6 && p.Tuoi < 15).Where(p => p.MaDTuong != "HN").Count();
            bc.BH60 = qLao.Where(p => p.DTuong == "BHYT").Where(p => p.NoiTru == 0 && p.DTNT == true).Where(p => p.Tuoi > 60).Where(p => p.MaDTuong != "HN").Count();
            bc.HN = qLao.Where(p => p.DTuong == "BHYT").Where(p => p.NoiTru == 0 && p.DTNT == true).Where(p => p.MaDTuong == "HN").Count();
            bc.BHKhac = qLao.Where(p => p.DTuong == "BHYT").Where(p => p.NoiTru == 0 && p.DTNT == true).Where(p => p.MaDTuong != "HN" && p.Tuoi >= 15 && p.Tuoi <= 60).Count();
            bc.DV = qLao.Where(p => p.DTuong != "BHYT").Where(p => p.NoiTru == 0 && p.DTNT == true).Count();
            _lbc.Add(bc);

            var qCOPD = (from bn in qbnkb.Where(p => p.MaICD.Contains("J44")) group bn by new { bn.MaBNhan, bn.MaBV, bn.DTuong, bn.NoiTru, bn.Tuoi, bn.DTNT, bn.MaDTuong } into kq select new { kq.Key.MaBNhan, kq.Key.MaBV, kq.Key.DTuong, kq.Key.NoiTru, kq.Key.Tuoi, kq.Key.DTNT, kq.Key.MaDTuong }).ToList();
            bc = new BC();
            bc.stt = "";
            bc.NoiDung = "COPD";
            bc.DonVi = "Bệnh nhân";
            bc.Tong = qCOPD.Where(p => p.NoiTru == 0 && p.DTNT == true).Count();
            bc.TE6 = qCOPD.Where(p => p.DTuong == "BHYT").Where(p => p.NoiTru == 0 && p.DTNT == true).Where(p => p.Tuoi < 6 || p.Tuoi == null).Where(p => p.MaDTuong != "HN").Count();
            bc.TE15 = qCOPD.Where(p => p.DTuong == "BHYT").Where(p => p.NoiTru == 0 && p.DTNT == true).Where(p => p.Tuoi >= 6 && p.Tuoi < 15).Where(p => p.MaDTuong != "HN").Count();
            bc.BH60 = qCOPD.Where(p => p.DTuong == "BHYT").Where(p => p.NoiTru == 0 && p.DTNT == true).Where(p => p.Tuoi > 60).Where(p => p.MaDTuong != "HN").Count();
            bc.HN = qCOPD.Where(p => p.DTuong == "BHYT").Where(p => p.NoiTru == 0 && p.DTNT == true).Where(p => p.MaDTuong == "HN").Count();
            bc.BHKhac = qCOPD.Where(p => p.DTuong == "BHYT").Where(p => p.NoiTru == 0 && p.DTNT == true).Where(p => p.MaDTuong != "HN" && p.Tuoi >= 15 && p.Tuoi <= 60).Count();
            bc.DV = qCOPD.Where(p => p.DTuong != "BHYT").Where(p => p.NoiTru == 0 && p.DTNT == true).Count();
            _lbc.Add(bc);

            var qbv = data.BenhViens.ToList();
            var tuyenbv = qbv.Where(p=>p.MaBV == DungChung.Bien.MaBV).FirstOrDefault();
            int tuyen = 3;
            if(tuyenbv != null)
            {
            if( tuyenbv.TuyenBV.Trim() == "A")
                tuyen = 1;
                else if(tuyenbv.TuyenBV.Trim() == "B")
                tuyen = 2;
                else if (tuyenbv.TuyenBV.Trim() == "C")
                tuyen = 3;
                else if (tuyenbv.TuyenBV.Trim() == "D")
                tuyen = 4;
            }

            var qchuyentuyen1 = (from rv in data.RaViens.Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay)
                                join bn in data.BenhNhans on rv.MaBNhan equals bn.MaBNhan
                                join bv in data.BenhViens on rv.MaBVC equals bv.MaBV
                                select new {bn.MaBNhan, bn.DTuong, bn.NoiTru, bn.Tuoi, bn.DTNT, bn.MaBV, rv.MaBVC, MaDTuong = bn.MaDTuong == null ? "" : bn.MaDTuong.Trim().ToUpper(),  Tuyen =bv.TuyenBV == null ? -1 : (  bv.TuyenBV.Trim() == "A" ? 1 : (bv.TuyenBV.Trim() == "B" ? 2 : (bv.TuyenBV.Trim() == "C" ? 3 : (bv.TuyenBV.Trim() == "D" ? 4 : -1))) ) }).ToList();//qbn.Where(p => p.kq2 != null && p.kq2.Status == 1).ToList();
            var qchuyentuyen2 = qchuyentuyen1.Where(p=>p.Tuyen  != -1 && p.Tuyen <= tuyen).ToList();
            bc = new BC();
            bc.stt = "13";
            bc.NoiDung = "Tổng số bệnh nhân chuyển tuyến trên, ngang tuyến trong đó";
            bc.DonVi = "Lượt";
            bc.Tong = qchuyentuyen2.Count();
            bc.TE6 = qchuyentuyen2.Where(p => p.DTuong == "BHYT").Where(p => p.Tuoi < 6 || p.Tuoi == null).Where(p => p.MaDTuong != "HN").Count();
            bc.TE15 = qchuyentuyen2.Where(p => p.DTuong == "BHYT").Where(p => p.Tuoi >= 6 && p.Tuoi < 15).Where(p => p.MaDTuong != "HN").Count();
            bc.BH60 = qchuyentuyen2.Where(p => p.DTuong == "BHYT").Where(p => p.Tuoi > 60).Where(p => p.MaDTuong != "HN").Count();
            bc.HN = qchuyentuyen2.Where(p => p.DTuong == "BHYT").Where(p => p.MaDTuong == "HN").Count();
            bc.BHKhac = qchuyentuyen2.Where(p => p.DTuong == "BHYT").Where(p => p.MaDTuong != "HN" && p.Tuoi >= 15 && p.Tuoi <= 60).Count();
            bc.DV = qchuyentuyen2.Where(p => p.DTuong != "BHYT").Count();
            _lbc.Add(bc);

            bc = new BC();
            bc.stt = "";
            bc.NoiDung = "Phổi TW + 74 TW";
            bc.DonVi = "Lượt";
            bc.Tong = qchuyentuyen2.Where(p => p.MaBVC == "01910" || p.MaBVC == "26010").Count();
            bc.TE6 = qchuyentuyen2.Where(p => p.DTuong == "BHYT").Where(p => p.MaBVC == "01910" || p.MaBVC == "26010").Where(p => p.Tuoi < 6 || p.Tuoi == null).Where(p => p.MaDTuong != "HN").Count();
            bc.TE15 = qchuyentuyen2.Where(p => p.DTuong == "BHYT").Where(p => p.MaBVC == "01910" || p.MaBVC == "26010").Where(p => p.Tuoi >= 6 && p.Tuoi < 15).Where(p => p.MaDTuong != "HN").Count();
            bc.BH60 = qchuyentuyen2.Where(p => p.DTuong == "BHYT").Where(p => p.MaBVC == "01910" || p.MaBVC == "26010").Where(p => p.Tuoi > 60).Where(p => p.MaDTuong != "HN").Count();
            bc.HN = qchuyentuyen2.Where(p => p.DTuong == "BHYT").Where(p => p.MaBVC == "01910" || p.MaBVC == "26010").Where(p => p.MaDTuong == "HN").Count();
            bc.BHKhac = qchuyentuyen2.Where(p => p.DTuong == "BHYT").Where(p => p.MaBVC == "01910" || p.MaBVC == "26010").Where(p => p.MaDTuong != "HN" && p.Tuoi >= 15 && p.Tuoi <= 60).Count();
            bc.DV = qchuyentuyen2.Where(p => p.DTuong != "BHYT").Where(p => p.MaBVC == "01910" || p.MaBVC == "26010").Count();
            _lbc.Add(bc);

            bc = new BC();
            bc.stt = "";
            bc.NoiDung = "Bệnh viện đa khoa tỉnh";
            bc.DonVi = "Lượt";
            bc.Tong = qchuyentuyen2.Where(p => p.MaBVC == "12096" ).Count();
            bc.TE6 = qchuyentuyen2.Where(p => p.DTuong == "BHYT").Where(p => p.MaBVC == "12096" ).Where(p => p.Tuoi < 6 || p.Tuoi == null).Where(p => p.MaDTuong != "HN").Count();
            bc.TE15 = qchuyentuyen2.Where(p => p.DTuong == "BHYT").Where(p => p.MaBVC == "12096").Where(p => p.Tuoi >= 6 && p.Tuoi < 15).Where(p => p.MaDTuong != "HN").Count();
            bc.BH60 = qchuyentuyen2.Where(p => p.DTuong == "BHYT").Where(p => p.MaBVC == "12096").Where(p => p.Tuoi > 60).Where(p => p.MaDTuong != "HN").Count();
            bc.HN = qchuyentuyen2.Where(p => p.DTuong == "BHYT").Where(p => p.MaBVC == "12096").Where(p => p.MaDTuong == "HN").Count();
            bc.BHKhac = qchuyentuyen2.Where(p => p.DTuong == "BHYT").Where(p => p.MaBVC == "12096").Where(p => p.MaDTuong != "HN" && p.Tuoi >= 15 && p.Tuoi <= 60).Count();
            bc.DV = qchuyentuyen2.Where(p => p.DTuong != "BHYT").Where(p => p.MaBVC == "12096").Count();
            _lbc.Add(bc);

            bc = new BC();
            bc.stt = "";
            bc.NoiDung = "Khác";
            bc.DonVi = "Lượt";
            bc.Tong = qchuyentuyen2.Where(p => p.MaBVC != "01910" && p.MaBVC != "26010" && p.MaBVC != "12096").Count();
            bc.TE6 = qchuyentuyen2.Where(p => p.DTuong == "BHYT").Where(p => p.MaBVC != "01910" && p.MaBVC != "26010" && p.MaBVC != "12096").Where(p => p.Tuoi < 6 || p.Tuoi == null).Where(p => p.MaDTuong != "HN").Count();
            bc.TE15 = qchuyentuyen2.Where(p => p.DTuong == "BHYT").Where(p => p.MaBVC != "01910" && p.MaBVC != "26010" && p.MaBVC != "12096").Where(p => p.Tuoi >= 6 && p.Tuoi < 15).Where(p => p.MaDTuong != "HN").Count();
            bc.BH60 = qchuyentuyen2.Where(p => p.DTuong == "BHYT").Where(p => p.MaBVC != "01910" && p.MaBVC != "26010" && p.MaBVC != "12096").Where(p => p.Tuoi > 60).Where(p => p.MaDTuong != "HN").Count();
            bc.HN = qchuyentuyen2.Where(p => p.DTuong == "BHYT").Where(p => p.MaBVC != "01910" && p.MaBVC != "26010" && p.MaBVC != "12096").Where(p => p.MaDTuong == "HN").Count();
            bc.BHKhac = qchuyentuyen2.Where(p => p.DTuong == "BHYT").Where(p => p.MaBVC != "01910" && p.MaBVC != "26010" && p.MaBVC != "12096").Where(p => p.MaDTuong != "HN" && p.Tuoi >= 15 && p.Tuoi <= 60).Count();
            bc.DV = qchuyentuyen2.Where(p => p.DTuong != "BHYT").Where(p => p.MaBVC != "01910" && p.MaBVC != "26010" && p.MaBVC != "12096").Count();
            _lbc.Add(bc);

            var qchuyentuyen3 = qchuyentuyen1.Where(p => p.Tuyen != -1 && p.Tuyen > tuyen).ToList();
            bc = new BC();
            bc.stt = "14";
            bc.NoiDung = "TS bệnh nhân chuyển về tuyến dưới";
            bc.DonVi = "Bệnh nhân";
            bc.Tong = qchuyentuyen3.Count();
            bc.TE6 = qchuyentuyen3.Where(p => p.DTuong == "BHYT").Where(p => p.Tuoi < 6 || p.Tuoi == null).Where(p => p.MaDTuong != "HN").Count();
            bc.TE15 = qchuyentuyen3.Where(p => p.DTuong == "BHYT").Where(p => p.Tuoi >= 6 && p.Tuoi < 15).Where(p => p.MaDTuong != "HN").Count();
            bc.BH60 = qchuyentuyen3.Where(p => p.DTuong == "BHYT").Where(p => p.Tuoi > 60).Where(p => p.MaDTuong != "HN").Count();
            bc.HN = qchuyentuyen3.Where(p => p.DTuong == "BHYT").Where(p => p.MaDTuong == "HN").Count();
            bc.BHKhac = qchuyentuyen3.Where(p => p.DTuong == "BHYT").Where(p => p.MaDTuong != "HN" && p.Tuoi >= 15 && p.Tuoi <= 60).Count();
            bc.DV = qchuyentuyen3.Where(p => p.DTuong != "BHYT").Count();
            _lbc.Add(bc);

            var qTuyenDuoiLen = (from bn in qbn
                                 join bv in data.BenhViens on bn.MaBV equals bv.MaBV
                                 select new { bn.MaBNhan, bn.DTuong, bn.NoiTru, bn.Tuoi, bn.DTNT, bn.MaBV, MaDTuong = bn.MaDTuong == null ? "" : bn.MaDTuong.Trim().ToUpper(), bn.kq1, Tuyen =bv.TuyenBV == null ? -1 : (  bv.TuyenBV.Trim() == "A" ? 1 : (bv.TuyenBV.Trim() == "B" ? 2 : (bv.TuyenBV.Trim() == "C" ? 3 : (bv.TuyenBV.Trim() == "D" ? 4 : -1))) )}).Where(p=>p.Tuyen > tuyen).ToList();

            bc = new BC();
            bc.stt = "15";
            bc.NoiDung = "TS bệnh nhân chuyển tuyến dưới lên";
            bc.DonVi = "Bệnh nhân";
            bc.Tong = qTuyenDuoiLen.Count();
            bc.TE6 = qTuyenDuoiLen.Where(p => p.DTuong == "BHYT").Where(p => p.Tuoi < 6 || p.Tuoi == null).Where(p => p.MaDTuong != "HN").Count();
            bc.TE15 = qTuyenDuoiLen.Where(p => p.DTuong == "BHYT").Where(p => p.Tuoi >= 6 && p.Tuoi < 15).Where(p => p.MaDTuong != "HN").Count();
            bc.BH60 = qTuyenDuoiLen.Where(p => p.DTuong == "BHYT").Where(p => p.Tuoi > 60).Where(p => p.MaDTuong != "HN").Count();
            bc.HN = qTuyenDuoiLen.Where(p => p.DTuong == "BHYT").Where(p => p.MaDTuong == "HN").Count();
            bc.BHKhac = qTuyenDuoiLen.Where(p => p.DTuong == "BHYT").Where(p => p.MaDTuong != "HN" && p.Tuoi >= 15 && p.Tuoi <= 60).Count();
            bc.DV = qTuyenDuoiLen.Where(p => p.DTuong != "BHYT").Count();
            _lbc.Add(bc);

            BaoCao.rep_BCCongTacKCB_12122 rep = new BaoCao.rep_BCCongTacKCB_12122();
            frmIn frm = new frmIn();
            rep.lblTuNgay.Text = "Từ ngày " + tungay.ToString("dd/MM/yyyy") + " đến ngày " + denngay.ToString("dd/MM/yyyy");
            rep.DataSource = _lbc;
            rep.BindingData();
            rep.CreateDocument();
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();
        }
        public class BC
        {
            public string stt { set; get; }
            public int? MaBNhan { set; get; }
            public string NoiDung { set; get; }
            public string DonVi { set; get; }
            public int Tong { set; get; }
            public int TE6 { set; get; }
            public int TE15 { set; get; }
            public int BH60 { set; get; }
            public int HN { set; get; }
            public int BHKhac { set; get; }
            public int DV { set; get; }

        }
    }
}