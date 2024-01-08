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
    public partial class frm_BaoCaoHoatDongCLS_12122 : DevExpress.XtraEditors.XtraForm
    {
        public frm_BaoCaoHoatDongCLS_12122()
        {
            InitializeComponent();
        }

        List<DichVu> _ldvAll = new List<DichVu>();
        List<TieuNhomDV> qtn = new List<TieuNhomDV>();
        bool load = false;
        private void frm_BangTHCongNo_12121_Load(object sender, EventArgs e)
        {
            lupNgaytu.DateTime = DateTime.Now;
            lupngayden.DateTime = DateTime.Now;

           
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            DateTime tungay = DungChung.Ham.NgayTu(lupNgaytu.DateTime);
            DateTime denngay = DungChung.Ham.NgayDen(lupngayden.DateTime);
            List<int> lMaDV = new List<int>();


            List<KPhong> lKp = data.KPhongs.Where(p => p.PLoai == "Lâm sàng"  ).OrderBy(p=>p.TenKP).ToList();
            List<int> lKp_PK = data.KPhongs.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.PhongKham).Select(p=>p.MaKP).ToList();

            int[] lMaKP = new int[] { 0, 0, 0, 0, 0, 0, 0, 0 };
            BaoCao.rep_BCHoatDongCanLamSang_12122 rep = new BaoCao.rep_BCHoatDongCanLamSang_12122();
            frmIn frm = new frmIn();
            for (int i = 1; i < 9; i++)
            {
                if (i <= lKp.Count)
                {
                    lMaKP[i - 1] = lKp.Skip(i - 1).Select(p => p.MaKP).FirstOrDefault();

                    switch (i)
                    {
                        case 1:
                            rep.celTit2.Text = lKp.Skip(i - 1).Select(p => p.TenKP).FirstOrDefault();
                            break;
                        case 2:
                            rep.celTit3.Text = lKp.Skip(i - 1).Select(p => p.TenKP).FirstOrDefault();
                            break;
                        case 3:
                            rep.celTit4.Text = lKp.Skip(i - 1).Select(p => p.TenKP).FirstOrDefault();
                            break;
                        case 4:
                            rep.celTit5.Text = lKp.Skip(i - 1).Select(p => p.TenKP).FirstOrDefault();
                            break;
                        case 5:
                            rep.celTit6.Text = lKp.Skip(i - 1).Select(p => p.TenKP).FirstOrDefault();
                            break;
                        case 6:
                            rep.celTit7.Text = lKp.Skip(i - 1).Select(p => p.TenKP).FirstOrDefault();
                            break;
                        case 7:
                            rep.celTit8.Text = lKp.Skip(i - 1).Select(p => p.TenKP).FirstOrDefault();
                            break;
                        case 8:
                            rep.celTit9.Text = lKp.Skip(i - 1).Select(p => p.TenKP).FirstOrDefault();
                            break;                       
                    }
                }
                else
                    break;
            }

            List<BC> lBC = new List<BC>();
            if (cboTT.Text.Contains("thực hiện"))
            {
                var qXNDom = (from cls in data.CLS.Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay)
                              join cd in data.ChiDinhs.Where(p => p.Status == 1) on cls.IdCLS equals cd.IdCLS
                              join dv in data.DichVus on cd.MaDV equals dv.MaDV
                              join tn in data.TieuNhomDVs.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNDom) on dv.IdTieuNhom equals tn.IdTieuNhom
                              join clsct in data.CLScts.Where(p => p.Status == 1) on cd.IDCD equals clsct.IDCD
                              select new { cls.MaKP, cls.MaBNhan, cd.MaDV, dv.SoTT, KetQua = clsct.KetQua == null ? "" : clsct.KetQua.Trim(), tn.TenRG, dv.TenDV,cls.NgayTH }).ToList();                   
                var q1 = (from a in qXNDom group a by new { a.MaKP, a.MaBNhan, a.MaDV, a.SoTT,a.NgayTH } into kq select new {MaKP = kq.Key.MaKP,MaBNhan = kq.Key.MaBNhan,MaDV = kq.Key.MaDV,SoTT = kq.Key.SoTT,NgayTH = kq.Key.NgayTH }).ToList();
                if (RadDK.SelectedIndex == 1)
                    q1 = (from a in qXNDom select new { MaKP = a.MaKP, MaBNhan = a.MaBNhan, MaDV = a.MaDV, SoTT = a.SoTT, NgayTH = a.NgayTH }).ToList();
                BC bc = new BC();
                bc.NoiDung = "Số lần XN đờm";
                bc.DonVi = "Lần";
                bc.kp1 = q1.Where(p => lKp_PK.Count > 0 && lKp_PK.Contains(p.MaKP ?? 0)).Count();
                bc.kp2 = q1.Where(p => p.MaKP == lMaKP[0]).Count();
                bc.kp3 = q1.Where(p => p.MaKP == lMaKP[1]).Count();
                bc.kp4 = q1.Where(p => p.MaKP == lMaKP[2]).Count();
                bc.kp5 = q1.Where(p => p.MaKP == lMaKP[3]).Count();
                bc.kp6 = q1.Where(p => p.MaKP == lMaKP[4]).Count();
                bc.kp7 = q1.Where(p => p.MaKP == lMaKP[5]).Count();
                bc.kp8 = q1.Where(p => p.MaKP == lMaKP[6]).Count();
                bc.kp9 = q1.Where(p => p.MaKP == lMaKP[7]).Count();
                bc.Tong = q1.Count();
                lBC.Add(bc);
                var q2 = (from a in qXNDom.Where(p => (p.SoTT == 1 || p.SoTT == 2) && (p.KetQua == "1+" || p.KetQua == "2+" || p.KetQua == "3+")) group a by new { a.MaKP, a.MaBNhan, a.MaDV, a.SoTT, a.NgayTH } into kq select new { MaKP = kq.Key.MaKP, MaBNhan = kq.Key.MaBNhan, MaDV = kq.Key.MaDV, SoTT = kq.Key.SoTT, NgayTH = kq.Key.NgayTH }).ToList();
                if (RadDK.SelectedIndex == 1)
                    q2 = (from a in qXNDom.Where(p => (p.SoTT == 1 || p.SoTT == 2) && (p.KetQua == "1+" || p.KetQua == "2+" || p.KetQua == "3+")) select new { MaKP = a.MaKP, MaBNhan = a.MaBNhan, MaDV = a.MaDV, SoTT = a.SoTT, NgayTH = a.NgayTH }).ToList();
                bc = new BC();
                bc.NoiDung = "Bệnh nhân AFB(+) phát hiện";
                bc.DonVi = "BN";
                bc.kp1 = q2.Where(p => lKp_PK.Count > 0 && lKp_PK.Contains(p.MaKP ?? 0)).Count();
                bc.kp2 = q2.Where(p => p.MaKP == lMaKP[0]).Count();
                bc.kp3 = q2.Where(p => p.MaKP == lMaKP[1]).Count();
                bc.kp4 = q2.Where(p => p.MaKP == lMaKP[2]).Count();
                bc.kp5 = q2.Where(p => p.MaKP == lMaKP[3]).Count();
                bc.kp6 = q2.Where(p => p.MaKP == lMaKP[4]).Count();
                bc.kp7 = q2.Where(p => p.MaKP == lMaKP[5]).Count();
                bc.kp8 = q2.Where(p => p.MaKP == lMaKP[6]).Count();
                bc.kp9 = q2.Where(p => p.MaKP == lMaKP[7]).Count();
                bc.Tong = q2.Count();
                lBC.Add(bc);
                var q3 = (from a in qXNDom.Where(p => (p.SoTT == 1 || p.SoTT == 2) && (p.KetQua == "Âm tính" || p.KetQua == "-")) group a by new { a.MaKP, a.MaBNhan, a.MaDV, a.SoTT, a.NgayTH } into kq select new { MaKP = kq.Key.MaKP, MaBNhan = kq.Key.MaBNhan, MaDV = kq.Key.MaDV, SoTT = kq.Key.SoTT, NgayTH = kq.Key.NgayTH }).ToList();
                if (RadDK.SelectedIndex == 1)
                    q3 = (from a in qXNDom.Where(p => (p.SoTT == 1 || p.SoTT == 2) && (p.KetQua == "Âm tính" || p.KetQua == "-")) select new { MaKP = a.MaKP, MaBNhan = a.MaBNhan, MaDV = a.MaDV, SoTT = a.SoTT, NgayTH = a.NgayTH }).ToList();
                bc = new BC();
                bc.NoiDung = "Bệnh nhân AFB(-) phát hiện";
                bc.DonVi = "BN";
                bc.kp1 = q3.Where(p => lKp_PK.Count > 0 && lKp_PK.Contains(p.MaKP ?? 0)).Count();
                bc.kp2 = q3.Where(p => p.MaKP == lMaKP[0]).Count();
                bc.kp3 = q3.Where(p => p.MaKP == lMaKP[1]).Count();
                bc.kp4 = q3.Where(p => p.MaKP == lMaKP[2]).Count();
                bc.kp5 = q3.Where(p => p.MaKP == lMaKP[3]).Count();
                bc.kp6 = q3.Where(p => p.MaKP == lMaKP[4]).Count();
                bc.kp7 = q3.Where(p => p.MaKP == lMaKP[5]).Count();
                bc.kp8 = q3.Where(p => p.MaKP == lMaKP[6]).Count();
                bc.kp9 = q3.Where(p => p.MaKP == lMaKP[7]).Count();
                bc.Tong = q3.Count();
                lBC.Add(bc);
                var qXN = (from cls in data.CLS.Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay)
                           join cd in data.ChiDinhs.Where(p => p.Status == 1) on cls.IdCLS equals cd.IdCLS
                           join dv in data.DichVus on cd.MaDV equals dv.MaDV
                           join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                           select new { cls.MaKP, cls.MaBNhan, cd.MaDV, tn.TenRG, TenDV = dv.TenDV.ToLower() }).ToList();

                if(RadDK.SelectedIndex == 1)
                qXN = (from cls in data.CLS.Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay)
                           join cd in data.ChiDinhs.Where(p => p.Status == 1) on cls.IdCLS equals cd.IdCLS
                           join dv in data.DichVus on cd.MaDV equals dv.MaDV
                           join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                           join clsct in data.CLScts.Where(p => p.Status == 1) on cd.IDCD equals clsct.IDCD into k1
                           from k in k1.DefaultIfEmpty()
                           select new { cls.MaKP, cls.MaBNhan, cd.MaDV, tn.TenRG, TenDV = dv.TenDV.ToLower() }).ToList();

                var q4 = qXN.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).ToList();
                bc = new BC();
                bc.NoiDung = "Xét nghiệm nước tiểu";
                bc.DonVi = "Lượt";
                bc.kp1 = q4.Where(p => lKp_PK.Count > 0 && lKp_PK.Contains(p.MaKP ?? 0)).Count();
                bc.kp2 = q4.Where(p => p.MaKP == lMaKP[0]).Count();
                bc.kp3 = q4.Where(p => p.MaKP == lMaKP[1]).Count();
                bc.kp4 = q4.Where(p => p.MaKP == lMaKP[2]).Count();
                bc.kp5 = q4.Where(p => p.MaKP == lMaKP[3]).Count();
                bc.kp6 = q4.Where(p => p.MaKP == lMaKP[4]).Count();
                bc.kp7 = q4.Where(p => p.MaKP == lMaKP[5]).Count();
                bc.kp8 = q4.Where(p => p.MaKP == lMaKP[6]).Count();
                bc.kp9 = q4.Where(p => p.MaKP == lMaKP[7]).Count();
                bc.Tong = q4.Count();
                lBC.Add(bc);
                var q5 = qXN.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).ToList();
                bc = new BC();
                bc.NoiDung = "Xét nghiệm huyết học";
                bc.DonVi = "Lượt";
                bc.kp1 = q5.Where(p => lKp_PK.Count > 0 && lKp_PK.Contains(p.MaKP ?? 0)).Count();
                bc.kp2 = q5.Where(p => p.MaKP == lMaKP[0]).Count();
                bc.kp3 = q5.Where(p => p.MaKP == lMaKP[1]).Count();
                bc.kp4 = q5.Where(p => p.MaKP == lMaKP[2]).Count();
                bc.kp5 = q5.Where(p => p.MaKP == lMaKP[3]).Count();
                bc.kp6 = q5.Where(p => p.MaKP == lMaKP[4]).Count();
                bc.kp7 = q5.Where(p => p.MaKP == lMaKP[5]).Count();
                bc.kp8 = q5.Where(p => p.MaKP == lMaKP[6]).Count();
                bc.kp9 = q5.Where(p => p.MaKP == lMaKP[7]).Count();
                bc.Tong = q5.Count();
                lBC.Add(bc);
                var q6 = qXN.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau).ToList();
                bc = new BC();
                bc.NoiDung = "Xét nghiệm hóa sinh máu";
                bc.DonVi = "Lượt";
                bc.kp1 = q6.Where(p => lKp_PK.Count > 0 && lKp_PK.Contains(p.MaKP ?? 0)).Count();
                bc.kp2 = q6.Where(p => p.MaKP == lMaKP[0]).Count();
                bc.kp3 = q6.Where(p => p.MaKP == lMaKP[1]).Count();
                bc.kp4 = q6.Where(p => p.MaKP == lMaKP[2]).Count();
                bc.kp5 = q6.Where(p => p.MaKP == lMaKP[3]).Count();
                bc.kp6 = q6.Where(p => p.MaKP == lMaKP[4]).Count();
                bc.kp7 = q6.Where(p => p.MaKP == lMaKP[5]).Count();
                bc.kp8 = q6.Where(p => p.MaKP == lMaKP[6]).Count();
                bc.kp9 = q6.Where(p => p.MaKP == lMaKP[7]).Count();
                bc.Tong = q6.Count();
                lBC.Add(bc);
                var q7 = qXN.Where(p => p.TenDV.Contains("hiv")).ToList();
                bc = new BC();
                bc.NoiDung = "Xét nghiệm HIV";
                bc.DonVi = "Lượt";
                bc.kp1 = q7.Where(p => lKp_PK.Count > 0 && lKp_PK.Contains(p.MaKP ?? 0)).Count();
                bc.kp2 = q7.Where(p => p.MaKP == lMaKP[0]).Count();
                bc.kp3 = q7.Where(p => p.MaKP == lMaKP[1]).Count();
                bc.kp4 = q7.Where(p => p.MaKP == lMaKP[2]).Count();
                bc.kp5 = q7.Where(p => p.MaKP == lMaKP[3]).Count();
                bc.kp6 = q7.Where(p => p.MaKP == lMaKP[4]).Count();
                bc.kp7 = q7.Where(p => p.MaKP == lMaKP[5]).Count();
                bc.kp8 = q7.Where(p => p.MaKP == lMaKP[6]).Count();
                bc.kp9 = q7.Where(p => p.MaKP == lMaKP[7]).Count();
                bc.Tong = q7.Count();
                lBC.Add(bc);
                var q8 = qXN.Where(p => p.TenDV.Contains("hbsag")).ToList();
                bc = new BC();
                bc.NoiDung = "Xét nghiệm HBsAg";
                bc.DonVi = "Lượt";
                bc.kp1 = q8.Where(p => lKp_PK.Count > 0 && lKp_PK.Contains(p.MaKP ?? 0)).Count();
                bc.kp2 = q8.Where(p => p.MaKP == lMaKP[0]).Count();
                bc.kp3 = q8.Where(p => p.MaKP == lMaKP[1]).Count();
                bc.kp4 = q8.Where(p => p.MaKP == lMaKP[2]).Count();
                bc.kp5 = q8.Where(p => p.MaKP == lMaKP[3]).Count();
                bc.kp6 = q8.Where(p => p.MaKP == lMaKP[4]).Count();
                bc.kp7 = q8.Where(p => p.MaKP == lMaKP[5]).Count();
                bc.kp8 = q8.Where(p => p.MaKP == lMaKP[6]).Count();
                bc.kp9 = q8.Where(p => p.MaKP == lMaKP[7]).Count();
                bc.Tong = q8.Count();
                lBC.Add(bc);
                var q9 = qXN.Where(p => p.TenDV.Contains("hcv")).ToList();
                bc = new BC();
                bc.NoiDung = "Xét nghiệm HCV";
                bc.DonVi = "Lượt";
                bc.kp1 = q9.Where(p => lKp_PK.Count > 0 && lKp_PK.Contains(p.MaKP ?? 0)).Count();
                bc.kp2 = q9.Where(p => p.MaKP == lMaKP[0]).Count();
                bc.kp3 = q9.Where(p => p.MaKP == lMaKP[1]).Count();
                bc.kp4 = q9.Where(p => p.MaKP == lMaKP[2]).Count();
                bc.kp5 = q9.Where(p => p.MaKP == lMaKP[3]).Count();
                bc.kp6 = q9.Where(p => p.MaKP == lMaKP[4]).Count();
                bc.kp7 = q9.Where(p => p.MaKP == lMaKP[5]).Count();
                bc.kp8 = q9.Where(p => p.MaKP == lMaKP[6]).Count();
                bc.kp9 = q9.Where(p => p.MaKP == lMaKP[7]).Count();
                bc.Tong = q9.Count();
                lBC.Add(bc);
                var q10 = qXN.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang).ToList();
                bc = new BC();
                bc.NoiDung = "Chụp XQuang";
                bc.DonVi = "Lượt";
                bc.kp1 = q10.Where(p => lKp_PK.Count > 0 && lKp_PK.Contains(p.MaKP ?? 0)).Count();
                bc.kp2 = q10.Where(p => p.MaKP == lMaKP[0]).Count();
                bc.kp3 = q10.Where(p => p.MaKP == lMaKP[1]).Count();
                bc.kp4 = q10.Where(p => p.MaKP == lMaKP[2]).Count();
                bc.kp5 = q10.Where(p => p.MaKP == lMaKP[3]).Count();
                bc.kp6 = q10.Where(p => p.MaKP == lMaKP[4]).Count();
                bc.kp7 = q10.Where(p => p.MaKP == lMaKP[5]).Count();
                bc.kp8 = q10.Where(p => p.MaKP == lMaKP[6]).Count();
                bc.kp9 = q10.Where(p => p.MaKP == lMaKP[7]).Count();
                bc.Tong = q10.Count();
                lBC.Add(bc);
                var q11 = qXN.Where(p => p.TenDV.Contains("nuôi cấy") && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNKhac).ToList();
                bc = new BC();
                bc.NoiDung = "Nuôi cấy";
                bc.DonVi = "Lượt";
                bc.kp1 = q11.Where(p => lKp_PK.Count > 0 && lKp_PK.Contains(p.MaKP ?? 0)).Count();
                bc.kp2 = q11.Where(p => p.MaKP == lMaKP[0]).Count();
                bc.kp3 = q11.Where(p => p.MaKP == lMaKP[1]).Count();
                bc.kp4 = q11.Where(p => p.MaKP == lMaKP[2]).Count();
                bc.kp5 = q11.Where(p => p.MaKP == lMaKP[3]).Count();
                bc.kp6 = q11.Where(p => p.MaKP == lMaKP[4]).Count();
                bc.kp7 = q11.Where(p => p.MaKP == lMaKP[5]).Count();
                bc.kp8 = q11.Where(p => p.MaKP == lMaKP[6]).Count();
                bc.kp9 = q11.Where(p => p.MaKP == lMaKP[7]).Count();
                bc.Tong = q11.Count();
                lBC.Add(bc);
                var q12 = qXN.Where(p => p.TenDV.Contains("gene xpert") && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNKhac).ToList();
                bc = new BC();
                bc.NoiDung = "Gene Xpert";
                bc.DonVi = "Lượt";
                bc.kp1 = q12.Where(p => lKp_PK.Count > 0 && lKp_PK.Contains(p.MaKP ?? 0)).Count();
                bc.kp2 = q12.Where(p => p.MaKP == lMaKP[0]).Count();
                bc.kp3 = q12.Where(p => p.MaKP == lMaKP[1]).Count();
                bc.kp4 = q12.Where(p => p.MaKP == lMaKP[2]).Count();
                bc.kp5 = q12.Where(p => p.MaKP == lMaKP[3]).Count();
                bc.kp6 = q12.Where(p => p.MaKP == lMaKP[4]).Count();
                bc.kp7 = q12.Where(p => p.MaKP == lMaKP[5]).Count();
                bc.kp8 = q12.Where(p => p.MaKP == lMaKP[6]).Count();
                bc.kp9 = q12.Where(p => p.MaKP == lMaKP[7]).Count();
                bc.Tong = q12.Count();
                lBC.Add(bc);
                var q13 = qXN.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNKhac && !p.TenDV.Contains("gene xpert") && !p.TenDV.Contains("hiv") && !p.TenDV.Contains("hbsag") && !p.TenDV.Contains("hcv") && !p.TenDV.Contains("nuôi cấy")).ToList();
                bc = new BC();
                bc.NoiDung = "Xét nghiệm khác";
                bc.DonVi = "Lượt";
                bc.kp1 = q13.Where(p => lKp_PK.Count > 0 && lKp_PK.Contains(p.MaKP ?? 0)).Count();
                bc.kp2 = q13.Where(p => p.MaKP == lMaKP[0]).Count();
                bc.kp3 = q13.Where(p => p.MaKP == lMaKP[1]).Count();
                bc.kp4 = q13.Where(p => p.MaKP == lMaKP[2]).Count();
                bc.kp5 = q13.Where(p => p.MaKP == lMaKP[3]).Count();
                bc.kp6 = q13.Where(p => p.MaKP == lMaKP[4]).Count();
                bc.kp7 = q13.Where(p => p.MaKP == lMaKP[5]).Count();
                bc.kp8 = q13.Where(p => p.MaKP == lMaKP[6]).Count();
                bc.kp9 = q13.Where(p => p.MaKP == lMaKP[7]).Count();
                bc.Tong = q13.Count();
                lBC.Add(bc);

                rep.celNgayThang.Text = "Từ ngày " + tungay.ToString("dd/MM/yyyy") + " đến ngày " + denngay.ToString("dd/MM/yyyy");
                rep.DataSource = lBC;
                rep.BindingData();
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
            }
            else if (cboTT.Text.Contains("thanh toán"))
            {
                var qXNDom = (from vienphi in data.VienPhis.Where(p => p.NgayTT >= tungay).Where(p => p.NgayTT <= denngay)
                              join cls in data.CLS on vienphi.MaBNhan equals cls.MaBNhan
                              join cd in data.ChiDinhs.Where(p => p.Status == 1) on cls.IdCLS equals cd.IdCLS
                              join dv in data.DichVus on cd.MaDV equals dv.MaDV
                              join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                              join clsct in data.CLScts.Where(p => p.Status == 1) on cd.IDCD equals clsct.IDCD
                              select new { cls.MaKP, cls.MaBNhan, cd.MaDV, dv.SoTT, KetQua = clsct.KetQua == null ? "" : clsct.KetQua.Trim(), tn.TenRG, dv.TenDV }).ToList();
                
                var q1 = (from a in qXNDom.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNDom) group a by new { a.MaKP, a.MaBNhan, a.MaDV, a.SoTT } into kq select new { kq.Key.MaKP, kq.Key.MaBNhan, kq.Key.MaDV, kq.Key.SoTT }).ToList();
                BC bc = new BC();
                bc.NoiDung = "Số lần XN đờm";
                bc.DonVi = "Lần";
                bc.kp1 = q1.Where(p => lKp_PK.Count > 0 && lKp_PK.Contains(p.MaKP ?? 0)).Count();
                bc.kp2 = q1.Where(p => p.MaKP == lMaKP[0]).Count();
                bc.kp3 = q1.Where(p => p.MaKP == lMaKP[1]).Count();
                bc.kp4 = q1.Where(p => p.MaKP == lMaKP[2]).Count();
                bc.kp5 = q1.Where(p => p.MaKP == lMaKP[3]).Count();
                bc.kp6 = q1.Where(p => p.MaKP == lMaKP[4]).Count();
                bc.kp7 = q1.Where(p => p.MaKP == lMaKP[5]).Count();
                bc.kp8 = q1.Where(p => p.MaKP == lMaKP[6]).Count();
                bc.kp9 = q1.Where(p => p.MaKP == lMaKP[7]).Count();
                bc.Tong = q1.Count();
                lBC.Add(bc);
                var q2 = (from a in qXNDom.Where(p => (p.SoTT == 1 || p.SoTT == 2) && (p.KetQua == "1+" || p.KetQua == "2+" || p.KetQua == "3+")) group a by new { a.MaKP, a.MaBNhan } into kq select new { kq.Key.MaKP, kq.Key.MaBNhan }).ToList();
                bc = new BC();
                bc.NoiDung = "Bệnh nhân AFB(+) phát hiện";
                bc.DonVi = "BN";
                bc.kp1 = q2.Where(p => lKp_PK.Count > 0 && lKp_PK.Contains(p.MaKP ?? 0)).Count();
                bc.kp2 = q2.Where(p => p.MaKP == lMaKP[0]).Count();
                bc.kp3 = q2.Where(p => p.MaKP == lMaKP[1]).Count();
                bc.kp4 = q2.Where(p => p.MaKP == lMaKP[2]).Count();
                bc.kp5 = q2.Where(p => p.MaKP == lMaKP[3]).Count();
                bc.kp6 = q2.Where(p => p.MaKP == lMaKP[4]).Count();
                bc.kp7 = q2.Where(p => p.MaKP == lMaKP[5]).Count();
                bc.kp8 = q2.Where(p => p.MaKP == lMaKP[6]).Count();
                bc.kp9 = q2.Where(p => p.MaKP == lMaKP[7]).Count();
                bc.Tong = q2.Count();
                lBC.Add(bc);
                var q3 = (from a in qXNDom.Where(p => (p.SoTT == 1 || p.SoTT == 2) && (p.KetQua == "Âm tính" || p.KetQua == "-")) group a by new { a.MaKP, a.MaBNhan } into kq select new { kq.Key.MaKP, kq.Key.MaBNhan }).ToList();
                bc = new BC();
                bc.NoiDung = "Bệnh nhân AFB(-) phát hiện";
                bc.DonVi = "BN";
                bc.kp1 = q3.Where(p => lKp_PK.Count > 0 && lKp_PK.Contains(p.MaKP ?? 0)).Count();
                bc.kp2 = q3.Where(p => p.MaKP == lMaKP[0]).Count();
                bc.kp3 = q3.Where(p => p.MaKP == lMaKP[1]).Count();
                bc.kp4 = q3.Where(p => p.MaKP == lMaKP[2]).Count();
                bc.kp5 = q3.Where(p => p.MaKP == lMaKP[3]).Count();
                bc.kp6 = q3.Where(p => p.MaKP == lMaKP[4]).Count();
                bc.kp7 = q3.Where(p => p.MaKP == lMaKP[5]).Count();
                bc.kp8 = q3.Where(p => p.MaKP == lMaKP[6]).Count();
                bc.kp9 = q3.Where(p => p.MaKP == lMaKP[7]).Count();
                bc.Tong = q3.Count();
                lBC.Add(bc);
                var qXN = (from vienphi in data.VienPhis.Where(p => p.NgayTT >= tungay).Where(p => p.NgayTT <= denngay)
                           join cls in data.CLS on vienphi.MaBNhan equals cls.MaBNhan
                           join cd in data.ChiDinhs.Where(p => p.Status == 1) on cls.IdCLS equals cd.IdCLS
                           join dv in data.DichVus on cd.MaDV equals dv.MaDV
                           join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                           select new { cls.MaKP, cls.MaBNhan, cd.MaDV, tn.TenRG, TenDV = dv.TenDV.ToLower() }).ToList();

                if (RadDK.SelectedIndex == 1)
                    qXN = (from vienphi in data.VienPhis.Where(p => p.NgayTT >= tungay).Where(p => p.NgayTT <= denngay)
                           join cls in data.CLS on vienphi.MaBNhan equals cls.MaBNhan
                           join cd in data.ChiDinhs.Where(p => p.Status == 1) on cls.IdCLS equals cd.IdCLS
                           join dv in data.DichVus on cd.MaDV equals dv.MaDV
                           join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                           join clsct in data.CLScts.Where(p => p.Status == 1) on cd.IDCD equals clsct.IDCD
                           select new { cls.MaKP, cls.MaBNhan, cd.MaDV, tn.TenRG, TenDV = dv.TenDV.ToLower() }).ToList();

                var q4 = qXN.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).ToList();
                bc = new BC();
                bc.NoiDung = "Xét nghiệm nước tiểu";
                bc.DonVi = "Lượt";
                bc.kp1 = q4.Where(p => lKp_PK.Count > 0 && lKp_PK.Contains(p.MaKP ?? 0)).Count();
                bc.kp2 = q4.Where(p => p.MaKP == lMaKP[0]).Count();
                bc.kp3 = q4.Where(p => p.MaKP == lMaKP[1]).Count();
                bc.kp4 = q4.Where(p => p.MaKP == lMaKP[2]).Count();
                bc.kp5 = q4.Where(p => p.MaKP == lMaKP[3]).Count();
                bc.kp6 = q4.Where(p => p.MaKP == lMaKP[4]).Count();
                bc.kp7 = q4.Where(p => p.MaKP == lMaKP[5]).Count();
                bc.kp8 = q4.Where(p => p.MaKP == lMaKP[6]).Count();
                bc.kp9 = q4.Where(p => p.MaKP == lMaKP[7]).Count();
                bc.Tong = q4.Count();
                lBC.Add(bc);
                var q5 = qXN.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).ToList();
                bc = new BC();
                bc.NoiDung = "Xét nghiệm huyết học";
                bc.DonVi = "Lượt";
                bc.kp1 = q5.Where(p => lKp_PK.Count > 0 && lKp_PK.Contains(p.MaKP ?? 0)).Count();
                bc.kp2 = q5.Where(p => p.MaKP == lMaKP[0]).Count();
                bc.kp3 = q5.Where(p => p.MaKP == lMaKP[1]).Count();
                bc.kp4 = q5.Where(p => p.MaKP == lMaKP[2]).Count();
                bc.kp5 = q5.Where(p => p.MaKP == lMaKP[3]).Count();
                bc.kp6 = q5.Where(p => p.MaKP == lMaKP[4]).Count();
                bc.kp7 = q5.Where(p => p.MaKP == lMaKP[5]).Count();
                bc.kp8 = q5.Where(p => p.MaKP == lMaKP[6]).Count();
                bc.kp9 = q5.Where(p => p.MaKP == lMaKP[7]).Count();
                bc.Tong = q5.Count();
                lBC.Add(bc);
                var q6 = qXN.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau).ToList();
                bc = new BC();
                bc.NoiDung = "Xét nghiệm hóa sinh máu";
                bc.DonVi = "Lượt";
                bc.kp1 = q6.Where(p => lKp_PK.Count > 0 && lKp_PK.Contains(p.MaKP ?? 0)).Count();
                bc.kp2 = q6.Where(p => p.MaKP == lMaKP[0]).Count();
                bc.kp3 = q6.Where(p => p.MaKP == lMaKP[1]).Count();
                bc.kp4 = q6.Where(p => p.MaKP == lMaKP[2]).Count();
                bc.kp5 = q6.Where(p => p.MaKP == lMaKP[3]).Count();
                bc.kp6 = q6.Where(p => p.MaKP == lMaKP[4]).Count();
                bc.kp7 = q6.Where(p => p.MaKP == lMaKP[5]).Count();
                bc.kp8 = q6.Where(p => p.MaKP == lMaKP[6]).Count();
                bc.kp9 = q6.Where(p => p.MaKP == lMaKP[7]).Count();
                bc.Tong = q6.Count();
                lBC.Add(bc);
                var q7 = qXN.Where(p => p.TenDV.Contains("hiv")).ToList();
                bc = new BC();
                bc.NoiDung = "Xét nghiệm HIV";
                bc.DonVi = "Lượt";
                bc.kp1 = q7.Where(p => lKp_PK.Count > 0 && lKp_PK.Contains(p.MaKP ?? 0)).Count();
                bc.kp2 = q7.Where(p => p.MaKP == lMaKP[0]).Count();
                bc.kp3 = q7.Where(p => p.MaKP == lMaKP[1]).Count();
                bc.kp4 = q7.Where(p => p.MaKP == lMaKP[2]).Count();
                bc.kp5 = q7.Where(p => p.MaKP == lMaKP[3]).Count();
                bc.kp6 = q7.Where(p => p.MaKP == lMaKP[4]).Count();
                bc.kp7 = q7.Where(p => p.MaKP == lMaKP[5]).Count();
                bc.kp8 = q7.Where(p => p.MaKP == lMaKP[6]).Count();
                bc.kp9 = q7.Where(p => p.MaKP == lMaKP[7]).Count();
                bc.Tong = q7.Count();
                lBC.Add(bc);
                var q8 = qXN.Where(p => p.TenDV.Contains("hbsag")).ToList();
                bc = new BC();
                bc.NoiDung = "Xét nghiệm HBsAg";
                bc.DonVi = "Lượt";
                bc.kp1 = q8.Where(p => lKp_PK.Count > 0 && lKp_PK.Contains(p.MaKP ?? 0)).Count();
                bc.kp2 = q8.Where(p => p.MaKP == lMaKP[0]).Count();
                bc.kp3 = q8.Where(p => p.MaKP == lMaKP[1]).Count();
                bc.kp4 = q8.Where(p => p.MaKP == lMaKP[2]).Count();
                bc.kp5 = q8.Where(p => p.MaKP == lMaKP[3]).Count();
                bc.kp6 = q8.Where(p => p.MaKP == lMaKP[4]).Count();
                bc.kp7 = q8.Where(p => p.MaKP == lMaKP[5]).Count();
                bc.kp8 = q8.Where(p => p.MaKP == lMaKP[6]).Count();
                bc.kp9 = q8.Where(p => p.MaKP == lMaKP[7]).Count();
                bc.Tong = q8.Count();
                lBC.Add(bc);
                var q9 = qXN.Where(p => p.TenDV.Contains("hcv")).ToList();
                bc = new BC();
                bc.NoiDung = "Xét nghiệm HCV";
                bc.DonVi = "Lượt";
                bc.kp1 = q9.Where(p => lKp_PK.Count > 0 && lKp_PK.Contains(p.MaKP ?? 0)).Count();
                bc.kp2 = q9.Where(p => p.MaKP == lMaKP[0]).Count();
                bc.kp3 = q9.Where(p => p.MaKP == lMaKP[1]).Count();
                bc.kp4 = q9.Where(p => p.MaKP == lMaKP[2]).Count();
                bc.kp5 = q9.Where(p => p.MaKP == lMaKP[3]).Count();
                bc.kp6 = q9.Where(p => p.MaKP == lMaKP[4]).Count();
                bc.kp7 = q9.Where(p => p.MaKP == lMaKP[5]).Count();
                bc.kp8 = q9.Where(p => p.MaKP == lMaKP[6]).Count();
                bc.kp9 = q9.Where(p => p.MaKP == lMaKP[7]).Count();
                bc.Tong = q9.Count();
                lBC.Add(bc);
                var q10 = qXN.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang).ToList();
                bc = new BC();
                bc.NoiDung = "Chụp XQuang";
                bc.DonVi = "Lượt";
                bc.kp1 = q10.Where(p => lKp_PK.Count > 0 && lKp_PK.Contains(p.MaKP ?? 0)).Count();
                bc.kp2 = q10.Where(p => p.MaKP == lMaKP[0]).Count();
                bc.kp3 = q10.Where(p => p.MaKP == lMaKP[1]).Count();
                bc.kp4 = q10.Where(p => p.MaKP == lMaKP[2]).Count();
                bc.kp5 = q10.Where(p => p.MaKP == lMaKP[3]).Count();
                bc.kp6 = q10.Where(p => p.MaKP == lMaKP[4]).Count();
                bc.kp7 = q10.Where(p => p.MaKP == lMaKP[5]).Count();
                bc.kp8 = q10.Where(p => p.MaKP == lMaKP[6]).Count();
                bc.kp9 = q10.Where(p => p.MaKP == lMaKP[7]).Count();
                bc.Tong = q10.Count();
                lBC.Add(bc);
                var q11 = qXN.Where(p => p.TenDV.Contains("nuôi cấy") && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNKhac).ToList();
                bc = new BC();
                bc.NoiDung = "Nuôi cấy";
                bc.DonVi = "Lượt";
                bc.kp1 = q11.Where(p => lKp_PK.Count > 0 && lKp_PK.Contains(p.MaKP ?? 0)).Count();
                bc.kp2 = q11.Where(p => p.MaKP == lMaKP[0]).Count();
                bc.kp3 = q11.Where(p => p.MaKP == lMaKP[1]).Count();
                bc.kp4 = q11.Where(p => p.MaKP == lMaKP[2]).Count();
                bc.kp5 = q11.Where(p => p.MaKP == lMaKP[3]).Count();
                bc.kp6 = q11.Where(p => p.MaKP == lMaKP[4]).Count();
                bc.kp7 = q11.Where(p => p.MaKP == lMaKP[5]).Count();
                bc.kp8 = q11.Where(p => p.MaKP == lMaKP[6]).Count();
                bc.kp9 = q11.Where(p => p.MaKP == lMaKP[7]).Count();
                bc.Tong = q11.Count();
                lBC.Add(bc);
                var q12 = qXN.Where(p => p.TenDV.Contains("gene xpert") && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNKhac).ToList();
                bc = new BC();
                bc.NoiDung = "Gene Xpert";
                bc.DonVi = "Lượt";
                bc.kp1 = q12.Where(p => lKp_PK.Count > 0 && lKp_PK.Contains(p.MaKP ?? 0)).Count();
                bc.kp2 = q12.Where(p => p.MaKP == lMaKP[0]).Count();
                bc.kp3 = q12.Where(p => p.MaKP == lMaKP[1]).Count();
                bc.kp4 = q12.Where(p => p.MaKP == lMaKP[2]).Count();
                bc.kp5 = q12.Where(p => p.MaKP == lMaKP[3]).Count();
                bc.kp6 = q12.Where(p => p.MaKP == lMaKP[4]).Count();
                bc.kp7 = q12.Where(p => p.MaKP == lMaKP[5]).Count();
                bc.kp8 = q12.Where(p => p.MaKP == lMaKP[6]).Count();
                bc.kp9 = q12.Where(p => p.MaKP == lMaKP[7]).Count();
                bc.Tong = q12.Count();
                lBC.Add(bc);
                var q13 = qXN.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNKhac && !p.TenDV.Contains("gene xpert") && !p.TenDV.Contains("hiv") && !p.TenDV.Contains("hbsag") && !p.TenDV.Contains("hcv") && !p.TenDV.Contains("nuôi cấy")).ToList();
                bc = new BC();
                bc.NoiDung = "Xét nghiệm khác";
                bc.DonVi = "Lượt";
                bc.kp1 = q13.Where(p => lKp_PK.Count > 0 && lKp_PK.Contains(p.MaKP ?? 0)).Count();
                bc.kp2 = q13.Where(p => p.MaKP == lMaKP[0]).Count();
                bc.kp3 = q13.Where(p => p.MaKP == lMaKP[1]).Count();
                bc.kp4 = q13.Where(p => p.MaKP == lMaKP[2]).Count();
                bc.kp5 = q13.Where(p => p.MaKP == lMaKP[3]).Count();
                bc.kp6 = q13.Where(p => p.MaKP == lMaKP[4]).Count();
                bc.kp7 = q13.Where(p => p.MaKP == lMaKP[5]).Count();
                bc.kp8 = q13.Where(p => p.MaKP == lMaKP[6]).Count();
                bc.kp9 = q13.Where(p => p.MaKP == lMaKP[7]).Count();
                bc.Tong = q13.Count();
                lBC.Add(bc);

                rep.celNgayThang.Text = "Từ ngày " + tungay.ToString("dd/MM/yyyy") + " đến ngày " + denngay.ToString("dd/MM/yyyy");
                rep.DataSource = lBC;
                rep.BindingData();
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
            }

        }

        public class BC
        {
            public string NoiDung { set; get; }
            public string DonVi { set; get; }
            public int kp1 { set; get; }
            public int kp2 { set; get; }
            public int kp3 { set; get; }
            public int kp4 { set; get; }
            public int kp5 { set; get; }
            public int kp6 { set; get; }
            public int kp7 { set; get; }
            public int kp8 { set; get; }
            public int kp9 { set; get; }
            public int Tong { set; get; }
        }

        private void frm_BaoCaoHoatDongCLS_12122_Enter(object sender, EventArgs e)
        {
            btnOK_Click(sender, e);
        }

        
    }
}