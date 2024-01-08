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
    public partial class frm_BaocaoTT37_Bieu9 : DevExpress.XtraEditors.XtraForm
    {
        public frm_BaocaoTT37_Bieu9()
        {
            InitializeComponent();
        }
       public class Doituong 
        {
            public int GiuongKH { get; set; }
            public int GiuongTT { get; set; }
            public int TongGiuong { get; set; }

           //Số lượt khám bệnh
            public int Nu_KB { get; set; }
            public int BHYT_KB { get; set; }
            public int TE_KB { get; set; }
            public int tongSoLuot_KB { get; set; }

           // Số lượt điều trị nội trú
            public int Nu_NT { get; set; }
            public int BHYT_NT { get; set; }
            public int TE_NT { get; set; }
            public int tongSoLuotNT { get; set; }
            public int? songayDTNT { get; set; }

           //Hoạt động CLS
            public int XetNghiem { get; set; }
            public int SieuAm { get; set; }
            public int XQuang { get; set; }
            public int XQuangCT { get; set; }
            public int XQuangMRI { get; set; }
            public int Chuyentuyen { get; set; }
        }

        public int tachchuoi(string s)
        {
            int year = DateTime.Now.Year;
            int sogiuong = 0;
            string[] arr = s.Split('|');
            
            for(int i=0; i<arr.Count();i++)
            {
                if(arr[i] !="")
                {
                    string[] arrGi = arr[i].Split(':');
                    if (year == Convert.ToInt32(arrGi[0].ToString()))
                    {
                        sogiuong = Convert.ToInt32(arrGi[1].ToString());
                    }
                }
                
            }
            return sogiuong;
        }

        private void btnBaoCao_Click(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            //string[] n1 = new string[5]{ "01/01/","31/03/","30/06/","30/09/","31/12/"};
            Doituong _dtuong = new Doituong();
            List<Doituong> _listDT = new List<Doituong>();
            DateTime tungay = DateTime.Now;
            DateTime denngay = DateTime.Now;
            int year = DateTime.Now.Year;
            Dictionary<string, object> _dic = new Dictionary<string, object>();
            if (txtTuNgay.Text !="")
                tungay= DungChung.Ham.NgayTu(Convert.ToDateTime(txtTuNgay.Text));
            if(txtDenNgay.Text !="")
                denngay=DungChung.Ham.NgayDen(Convert.ToDateTime(txtDenNgay.Text));

            _dic.Add("TuNgayDenNgay", "Từ ngày " + tungay.ToString("dd/MM/yyyy") + " đến ngày " + denngay.ToString("dd/MM/yyyy"));

            var q1 = (from bn in data.BenhNhans
                     join bnkb in data.BNKBs.Where(p=>p.Giuong !=null) on bn.MaBNhan equals bnkb.MaBNhan
                      join rv in data.RaViens.Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay) on bn.MaBNhan equals rv.MaBNhan
                     select new { bn.MaBNhan, bn.TenBNhan, bnkb.Giuong, bnkb.MaKP, bn.GTinh, bn.DTuong,bn.DTNT,bn.NoiTru,bn.Tuoi }).ToList();


            var q2 = (from bn in data.BenhNhans
                      join bnkb in data.BNKBs.Where(p => p.NgayKham >= tungay && p.NgayKham <= denngay).Where(p => p.Giuong != null) on bn.MaBNhan equals bnkb.MaBNhan
                      
                      select new { bn.MaBNhan, bn.TenBNhan, bnkb.Giuong, bnkb.MaKP, bn.GTinh, bn.DTuong, bn.DTNT, bn.NoiTru,bn.Tuoi }).ToList();

            int getGiuongTT = q2.Select(p => p.Giuong).Distinct().Count();
            
            var GiuongKH = (from kp in data.KPhongs.Where(p => p.SoGiuongKH != null) select new { kp.MaKP,kp.TenKP,kp.SoGiuongKH }).ToList();
            int TongGiuong = 0;
            for(int i=0;i<GiuongKH.Count;i++)
            {
                int SoGiuongTungKhoa = tachchuoi(GiuongKH[i].SoGiuongKH);
                TongGiuong += SoGiuongTungKhoa;
            }

            var k = (from bn in data.BenhNhans.Where(p => p.DTuong != "KSK")
                     join cls in data.CLS.Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay) on bn.MaBNhan equals cls.MaBNhan
                     join chidinh in data.ChiDinhs on cls.IdCLS equals chidinh.IdCLS

                     select new
                     {
                         bn.MaBNhan,
                         bn.DTuong,
                         cls.MaKP,
                         chidinh.MaDV
                     }).ToList();
            var k3 = (from item in k
                      join dichvu in data.DichVus.Where(p => p.IDNhom == 1) on item.MaDV equals dichvu.MaDV
                      join tieunhomdv in data.TieuNhomDVs on dichvu.IdTieuNhom equals tieunhomdv.IdTieuNhom
                      join kphong in data.KPhongs.Where(p => p.PLoai == "Phòng khám") on item.MaKP equals kphong.MaKP

                      select new
                      {
                          item.MaBNhan,
                          tieunhomdv.TenRG,
                          item.DTuong
                      }).ToList();
            // tổng toàn bộ xét nghiệm
            int tsxn = k3.Count; _dtuong.XetNghiem = tsxn;

            var k2 = (from item in k
                      join dichvu in data.DichVus on item.MaDV equals dichvu.MaDV
                      join tieunhomdv in data.TieuNhomDVs on dichvu.IdTieuNhom equals tieunhomdv.IdTieuNhom
                      join kphong in data.KPhongs.Where(p => p.PLoai == "Phòng khám") on item.MaKP equals kphong.MaKP

                      select new
                      {
                          item.MaBNhan,
                          tieunhomdv.TenRG,
                          item.DTuong
                      }).ToList();
            


            var f2 = (from item in k2.Where(p => p.TenRG=="X-Quang") 
                      select new
                      {
                          item.MaBNhan,
                          item.TenRG,
                          item.DTuong
                      }).ToList();
            // Tổng số bệnh nhận làm X-QuangCT
            int xquang = f2.Count; _dtuong.XQuang = xquang;

            var f6 = (from item in k2.Where(p => p.TenRG == "X-QuangCT")
                      select new
                      {
                          item.MaBNhan,
                          item.TenRG,
                          item.DTuong
                      }).ToList();
            // Tổng số bệnh nhận làm X-QuangCT
            int xquangCT = f6.Count; _dtuong.XQuangCT = xquangCT;


            // Tổng số bệnh nhận làm X-QuangMRI
            var f7 = (from item in k2.Where(p => p.TenRG.Contains("MRI"))
                      select new
                      {
                          item.MaBNhan,
                          item.TenRG,
                          item.DTuong
                      }).ToList();
            int xquangMRI = f7.Count; _dtuong.XQuangMRI = xquangMRI;

            var f3 = (from item in k2.Where(p => p.TenRG.Contains("Siêu âm")) 
                      select new
                      {
                          item.MaBNhan,
                          item.TenRG,
                          item.DTuong
                      }).ToList();
            //Tổng số bệnh nhân làm Siêu âm
            int tsSieuam = f3.Count; _dtuong.SieuAm = tsSieuam;
            //Lấy số ngày điều trị trong khoảng thời gian của các bệnh nhân đã ra viện
            var ngayDT= (from bn in data.BenhNhans join bnkb in data.BNKBs on bn.MaBNhan equals bnkb.MaBNhan join rv in data.RaViens.Where(p=>p.NgayRa>=tungay &&p.NgayRa<=denngay && p.SoNgaydt !=null) on bn.MaBNhan equals rv.MaBNhan select new {bn.MaBNhan,rv.SoNgaydt,rv.MaBVC}).ToList();

            int? TongNgayDaDT = 0;
            for (int i = 0; i < ngayDT.Count; i++)
            {
                TongNgayDaDT += ngayDT[i].SoNgaydt;
            }

            _dtuong.Chuyentuyen = ngayDT.Where(p => p.MaBVC != null).Count();

            //Lấy số ngày điều trị trong khoảng thời gian của các bệnh nhân đã ra viện
            var ngayDT2 = (from bn in data.BenhNhans join bnkb in data.BNKBs on bn.MaBNhan equals bnkb.MaBNhan join vv in data.VaoViens.Where(p => p.NgayVao >= tungay && p.NgayVao <= denngay) on bn.MaBNhan equals vv.MaBNhan select new { bn.MaBNhan, vv.NgayVao }).ToList();

            var NgayDT3 = ngayDT2.Where(o => !ngayDT.Select(p => p.MaBNhan).Contains(o.MaBNhan)).Select(t=>t.NgayVao).ToList();

            
            //Tổng số ngày điều trị của bệnh nhân
            int? TongNgayBNDangDieuTri = 0;
            TimeSpan? ts = denngay - tungay;
            
                for (int i = 0; i < NgayDT3.Count; i++)
                {
                        if (NgayDT3[i] != null)
                            ts = denngay - NgayDT3[i];
                        TongNgayBNDangDieuTri += ts.Value.Days;
                    
                }
            
            int? TongNgayDieuTriTaiVien = TongNgayDaDT + TongNgayBNDangDieuTri;
            _dtuong.songayDTNT = TongNgayDieuTriTaiVien;

            if (radNgayKham.Checked == true)
            {
                _listDT.Clear();
                
                _dtuong.GiuongKH = TongGiuong;
                _dtuong.GiuongTT = getGiuongTT;

                var q3 = q2.Where(p => p.GTinh == 0 && p.DTuong == "BHYT").Distinct().ToList();
                _dtuong.Nu_KB = q2.Where(p => p.GTinh == 0 && p.DTuong == "BHYT").Select(p=>p.MaBNhan).Distinct().Count();
                _dtuong.BHYT_KB = q2.Where(p => p.GTinh != 0 && p.DTuong == "BHYT" && p.Tuoi >= 15).Select(p => p.MaBNhan).Distinct().Count();
                _dtuong.TE_KB = q2.Where(p => p.Tuoi < 15 && p.DTuong == "BHYT").Select(p => p.MaBNhan).Distinct().Count();
                _dtuong.tongSoLuot_KB = _dtuong.Nu_KB + _dtuong.BHYT_KB + _dtuong.TE_KB;

                _dtuong.Nu_NT = q2.Where(p => p.GTinh == 0 && p.DTuong == "BHYT" && p.NoiTru == 1).Select(p => p.MaBNhan).Distinct().Count();
                _dtuong.BHYT_NT = q2.Where(p => p.GTinh != 0 && p.DTuong == "BHYT" && p.NoiTru == 1 && p.Tuoi >= 15).Select(p => p.MaBNhan).Distinct().Count();
                _dtuong.TE_NT = q2.Where(p => p.Tuoi < 15 && p.DTuong == "BHYT" && p.NoiTru == 1).Select(p => p.MaBNhan).Distinct().Count();
                _dtuong.tongSoLuotNT = _dtuong.Nu_NT + _dtuong.BHYT_NT + _dtuong.TE_NT;

                _listDT.Add(_dtuong);
               
            }
            if (radNgayRaVien.Checked == true)
            {
                _listDT.Clear();

                _dtuong.GiuongKH = TongGiuong;
                _dtuong.GiuongTT = getGiuongTT;

                _dtuong.Nu_KB = q1.Where(p => p.GTinh == 0 && p.DTuong == "BHYT").Select(p => p.MaBNhan).Distinct().Count();
                _dtuong.BHYT_KB = q1.Where(p => p.GTinh != 0 && p.DTuong == "BHYT" && p.Tuoi >= 15).Select(p => p.MaBNhan).Distinct().Count();
                _dtuong.TE_KB = q1.Where(p => p.Tuoi < 15 && p.DTuong == "BHYT").Select(p => p.MaBNhan).Distinct().Count();
                _dtuong.tongSoLuot_KB = _dtuong.Nu_KB + _dtuong.BHYT_KB + _dtuong.TE_KB;

                _dtuong.Nu_NT = q1.Where(p => p.GTinh == 0 && p.DTuong == "BHYT" && p.NoiTru == 1).Select(p => p.MaBNhan).Distinct().Count();
                _dtuong.BHYT_NT = q1.Where(p => p.GTinh != 0 && p.DTuong == "BHYT" && p.NoiTru == 1 && p.Tuoi >= 15).Select(p => p.MaBNhan).Distinct().Count();
                _dtuong.TE_NT = q1.Where(p => p.Tuoi < 15 && p.DTuong == "BHYT" && p.NoiTru == 1).Select(p => p.MaBNhan).Distinct().Count();
                _dtuong.tongSoLuotNT = _dtuong.Nu_NT + _dtuong.BHYT_NT + _dtuong.TE_NT;
                _listDT.Add(_dtuong);
            }
            if (DungChung.Bien.MaBV == "24012" || DungChung.Bien.MaBV == "24389" )
                DungChung.Ham.Print(DungChung.PrintConfig.Rep_CSGB_HDKCB_24012, _listDT, _dic, false);
            else
                DungChung.Ham.Print(DungChung.PrintConfig.Rep_CSGB_HDKCB_27023, _listDT, _dic, false);
            
        }

        private void frm_BaocaoTT37_Bieu9_Load(object sender, EventArgs e)
        {
            txtTuNgay.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtDenNgay.Text = DateTime.Now.ToString("dd/MM/yyyy");
            radNgayKham.Checked = true;
        }
    }
}