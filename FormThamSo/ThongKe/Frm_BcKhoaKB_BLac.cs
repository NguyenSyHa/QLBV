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
    public partial class Frm_BcKhoaKB_BLac : DevExpress.XtraEditors.XtraForm
    {
        public Frm_BcKhoaKB_BLac()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        string _madt;
        private bool KTtaoBc()
        {
            if (lupTuNgay.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn ngày bắt đầu in báo cáo");
                lupTuNgay.Focus();
                return false;
            }
            if (lupDenNgay.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn đến ngày kết thúc in báo cáo");
                lupDenNgay.Focus();
                return false;
            }

            return true;
        }
        private class KPhong
        {
            private string TenKP;
            private int MaKP;
            private bool Chon;
            public string tenkp
            { set { TenKP = value; } get { return TenKP; } }
            public int makp
            { set { MaKP = value; } get { return MaKP; } }
            public bool chon
            { set { Chon = value; } get { return Chon; } }
        }

        List<KPhong> _Kphong = new List<KPhong>();
        private void Frm_BcKhoaKB_BLac_Load(object sender, EventArgs e)
        {

            lupTuNgay.Focus();
            lupTuNgay.DateTime = System.DateTime.Now;
            lupDenNgay.DateTime = System.DateTime.Now;
            var kphong = (from kp in data.KPhongs.Where(p => p.PLoai == "Phòng khám") select new { kp.TenKP, kp.MaKP }).ToList();
            if (kphong.Count > 0)
            {
                KPhong themmoi1 = new KPhong();
                themmoi1.tenkp = "Chọn tất cả";
                themmoi1.makp = 0 ;
                themmoi1.chon = true;
                _Kphong.Add(themmoi1);
                foreach (var a in kphong)
                {
                    KPhong themmoi = new KPhong();
                    themmoi.tenkp = a.TenKP;
                    themmoi.makp = a.MaKP;
                    themmoi.chon = true;
                    _Kphong.Add(themmoi);
                }
                grcKhoaphong.DataSource = _Kphong.ToList();
            }
        }
       
        private void btnInBC_Click(object sender, EventArgs e)
        {
            DateTime tungay = DungChung.Ham.NgayTu(lupTuNgay.DateTime);
            DateTime denngay = DungChung.Ham.NgayDen(lupDenNgay.DateTime);
            List<KPhong> _lKhoaP = new List<KPhong>();
      
           frmIn frm = new frmIn();
           BaoCao.Rep_BcKhoaKB_BLac rep = new BaoCao.Rep_BcKhoaKB_BLac();
           #region Hiển thị thời gian
           int nam = Convert.ToInt32(denngay.Year);
           int thang = Convert.ToInt32(denngay.Month);
           if (radIn.SelectedIndex == 0)
           { rep.NgayThang.Value = "Từ ngày " + lupTuNgay.Text + " Đến ngày " + lupDenNgay.Text; }
           if (radIn.SelectedIndex == 1)
           { rep.NgayThang.Value = "Tháng " + thang + " năm " + nam; }
          if (radIn.SelectedIndex == 2)
           {
               if (thang > 1 && thang <= 3) { rep.NgayThang.Value = "Quý I năm " + nam; }
               if (thang > 3 && thang <= 6) { rep.NgayThang.Value = "Quý II năm " + nam; }
               if (thang > 6 && thang <= 9) { rep.NgayThang.Value = "Quý III năm " + nam; }
               if (thang > 9 && thang <= 12) { rep.NgayThang.Value = "Quý IV năm " + nam; }
           }
           if (radIn.SelectedIndex == 3)
           {
               rep.NgayThang.Value = "Báo cáo 6 tháng/ năm " + nam;
           }
           if (radIn.SelectedIndex == 4)
           {
               rep.NgayThang.Value = "Báo cáo 9 tháng/ năm " + nam;
           }
           if (radIn.SelectedIndex == 5)
           { rep.NgayThang.Value = "Năm " + nam; }
           #endregion
           _lKhoaP = _Kphong.Where(p => p.chon == true).ToList();
           _lKhoaP.Add(new KPhong { makp = 0, tenkp = "" });
    
           if (KTtaoBc())
            {
                var id = (from kb in data.BNKBs.Where(p => p.NgayKham >= tungay).Where(p => p.NgayKham <= denngay)
                          join k in data.KPhongs.Where(p => p.PLoai == "Phòng khám") on kb.MaKP equals k.MaKP
                          group kb by kb.MaBNhan into kq
                          select new { kq.Key, IDKB = kq.Max(p => p.IDKB) }).ToList();

                var qkb1 = (from k in id
                            join kb in data.BNKBs on k.IDKB equals kb.IDKB
                            join bn in data.BenhNhans on kb.MaBNhan equals bn.MaBNhan
                            group new { kb, bn } by new {kb.NgayKham, kb.MaBNhan, bn.NoiTru, kb.MaKP, bn.Tuoi, bn.DTuong, bn.GTinh, bn.MaDTuong, bn.DTNT, kb.PhuongAn, bn.TChung } into kq
                            select new {kq.Key.NgayKham, kq.Key.GTinh, kq.Key.MaBNhan, kq.Key.NoiTru, kq.Key.MaKP, kq.Key.Tuoi, kq.Key.DTuong, kq.Key.MaDTuong, kq.Key.DTNT, kq.Key.PhuongAn, kq.Key.TChung }).ToList();

                //var qvp1 = (from vp in data.VienPhis.Where(p => p.NgayTT >= tungay && p.NgayTT <= denngay) 
                //            join bn in data.BenhNhans on vp.MaBNhan equals bn.MaBNhan
                //            join rv in data.RaViens on vp.MaBNhan equals rv.MaBNhan
                //            select new { vp.NgayTT, vp.MaBNhan, bn.NoiTru, bn.DTuong, bn.DTNT,bn.Tuoi,bn.GTinh,bn.MaDTuong,bn.MaKP,bn.TChung, rv.Status,rv.SoNgaydt }).ToList();
               //var qkb = (from vp in qvp1.Where(p => p.NgayTT >= tungay && p.NgayTT <= denngay).Where(p => p.DTuong == "BHYT" || p.DTuong == "Dịch vụ")
               //              group vp by new { } into kq
               //              select new
               var qkb = (from ma in _lKhoaP
                          join p in qkb1.Where(p => p.DTuong == "BHYT" || p.DTuong == "Dịch vụ").Where(p=>p.NgayKham>=tungay&&p.NgayKham<=denngay) on ma.makp equals p.MaKP
                          group p by new { } into kq
                          select new
                               {
                                   NgT1 = kq.Select(p => p.MaBNhan).Count(),
                                   NgT11 = kq.Where(p => p.GTinh == 0).Select(p => p.MaBNhan).Count(),
                                   NgT2 = kq.Where(p => p.DTuong == "BHYT").Select(p => p.MaBNhan).Count(),
                                   NgT21 = kq.Where(p => p.DTuong == "BHYT" && p.MaDTuong == "HN").Select(p => p.MaBNhan).Count(),
                                   NgT3 = kq.Where(p => p.NoiTru == 0 && p.Tuoi < 15 && p.DTNT == false && p.PhuongAn != 2).Select(p => p.MaBNhan).Count(),
                                   NgT31 = kq.Where(p => p.NoiTru == 0 && p.Tuoi < 15 && p.DTuong == "BHYT" && p.DTNT == false && p.PhuongAn != 2).Select(p => p.MaBNhan).Count(),
                                   NgT4 = kq.Where(p => p.NoiTru == 0 && p.Tuoi < 6 && p.DTNT == false && p.PhuongAn != 2).Select(p => p.MaBNhan).Count(),
                                   NgT41 = kq.Where(p => p.NoiTru == 0 && p.Tuoi < 6 && p.DTuong == "BHYT" && p.DTNT == false && p.PhuongAn != 2).Select(p => p.MaBNhan).Count(),
                                   NgT5 = kq.Where(p => p.NoiTru == 0 && p.Tuoi < 5 && p.DTNT == false && p.PhuongAn != 2).Select(p => p.MaBNhan).Count(),
                                   NgT51 = kq.Where(p => p.NoiTru == 0 && p.Tuoi < 5 && p.DTuong == "BHYT" && p.DTNT == false && p.PhuongAn != 2).Select(p => p.MaBNhan).Count(),
                                   NgT6 = kq.Where(p => p.NoiTru == 0 && p.Tuoi > 60 && p.DTNT == false && p.PhuongAn != 2).Select(p => p.MaBNhan).Count(),
                                   NgT61 = kq.Where(p => p.NoiTru == 0 && p.Tuoi > 60 && p.DTuong == "BHYT" && p.DTNT == false && p.PhuongAn != 2).Select(p => p.MaBNhan).Count(),
                                   NgT62 = kq.Where(p => p.NoiTru == 0 && p.Tuoi > 60 && p.DTuong == "BHYT" && p.MaDTuong == "HN" && p.DTNT == false && p.PhuongAn != 2).Select(p => p.MaBNhan).Count(),
                                   NgT7 = kq.Where(p => p.NoiTru == 0 && p.Tuoi > 80 && p.DTNT == false && p.PhuongAn != 2).Select(p => p.MaBNhan).Count(),
                                   NgT71 = kq.Where(p => p.NoiTru == 0 && p.Tuoi > 80 && p.DTuong == "BHYT" && p.DTNT == false && p.PhuongAn != 2).Select(p => p.MaBNhan).Count(),
                                   NgT72 = kq.Where(p => p.NoiTru == 0 && p.Tuoi > 80 && p.DTuong == "BHYT" && p.MaDTuong == "HN" && p.DTNT == false && p.PhuongAn != 2).Select(p => p.MaBNhan).Count(),

                                   NT8 = kq.Where(p => p.NoiTru == 1).Select(p => p.MaBNhan).Count(),
                                   NT81 = kq.Where(p => p.NoiTru == 1 && p.GTinh == 0).Select(p => p.MaBNhan).Count(),
                                   NT9 = kq.Where(p => p.NoiTru == 1 && p.DTuong == "BHYT").Select(p => p.MaBNhan).Count(),
                                   NT91 = kq.Where(p => p.NoiTru == 1 && p.DTuong == "BHYT" && p.MaDTuong == "HN").Select(p => p.MaBNhan).Count(),
                                   NT10 = kq.Where(p => p.NoiTru == 1 && p.Tuoi < 15).Select(p => p.MaBNhan).Count(),
                                   NT101 = kq.Where(p => p.NoiTru == 1 && p.Tuoi < 15 && p.DTuong == "BHYT").Select(p => p.MaBNhan).Count(),
                                   NT102 = kq.Where(p => p.NoiTru == 1 && p.Tuoi > 6 && p.Tuoi < 15).Select(p => p.MaBNhan).Count(),
                                   NT11 = kq.Where(p => p.NoiTru == 1 && p.Tuoi > 60).Select(p => p.MaBNhan).Count(),
                                   NT111 = kq.Where(p => p.NoiTru == 1 && p.Tuoi > 60 && p.DTuong == "BHYT").Select(p => p.MaBNhan).Count(),
                                   NT112 = kq.Where(p => p.NoiTru == 1 && p.Tuoi > 60 && p.MaDTuong == "HN").Select(p => p.MaBNhan).Count(),
                                   NT12 = kq.Where(p => p.NoiTru == 1 && p.Tuoi > 80).Select(p => p.MaBNhan).Count(),
                                   NT121 = kq.Where(p => p.NoiTru == 1 && p.Tuoi > 80 && p.DTuong == "BHYT").Select(p => p.MaBNhan).Count(),
                                   NT122 = kq.Where(p => p.NoiTru == 1 && p.Tuoi > 80 && p.MaDTuong == "HN").Select(p => p.MaBNhan).Count(),

                                   NT16 = kq.Where(p => p.NoiTru == 0 && p.DTNT == true).Select(p => p.MaBNhan).Count(),
                                   NT161 = kq.Where(p => p.NoiTru == 0 && p.DTNT == true && p.MaDTuong == "HN").Select(p => p.MaBNhan).Count(),
                                   NT162 = kq.Where(p => p.NoiTru == 0 && p.DTNT == true && p.DTuong == "BHYT").Select(p => p.MaBNhan).Count(),

                                   NT17 = kq.Where(p => p.PhuongAn == 2).Select(p => p.MaBNhan).Count(),
                                   //NT171 = kq.Where(p => p.PhuongAn == 2 && p.DTNT == true && p.MaDTuong == "HN").Select(p => p.MaBNhan).Count(),
                                   //NT172 = kq.Where(p => p.PhuongAn == 2 && p.DTNT == true && p.DTuong == "BHYT").Select(p => p.MaBNhan).Count(),

                                
                               }).ToList();


                    if (qkb.Count > 0)
                {
                    rep.NgT1.Value = qkb.First().NgT1;
                    rep.NgT11.Value = qkb.First().NgT11;
                    rep.NgT2.Value = qkb.First().NgT2;
                    rep.NgT21.Value = qkb.First().NgT21;
                    rep.NgT3.Value = qkb.First().NgT3;
                    rep.NgT31.Value = qkb.First().NgT31;
                    rep.NgT4.Value = qkb.First().NgT41;
                    rep.NgT41.Value = qkb.First().NgT41;
                    rep.NgT5.Value = qkb.First().NgT5;
                    rep.NgT51.Value = qkb.First().NgT51;
                    rep.NgT6.Value = qkb.First().NgT6;
                    rep.NgT61.Value = qkb.First().NgT61;
                    rep.NgT62.Value = qkb.First().NgT62;
                    rep.NgT7.Value = qkb.First().NgT7;
                    rep.NgT71.Value = qkb.First().NgT71;
                    rep.NgT72.Value = qkb.First().NgT72;

                    rep.NT8.Value = qkb.First().NT8;
                    rep.NT81.Value = qkb.First().NT81;
                    rep.NT9.Value = qkb.First().NT9;
                    rep.NT91.Value = qkb.First().NT91;
                    rep.NT10.Value = qkb.First().NT10;
                    rep.NT101.Value = qkb.First().NT101;
                    rep.NT102.Value = qkb.First().NT102;
                    rep.NT11.Value = qkb.First().NT11;
                    rep.NT111.Value = qkb.First().NT111;
                    rep.NT112.Value = qkb.First().NT112;
                    rep.NT12.Value = qkb.First().NT12;
                    rep.NT121.Value = qkb.First().NT121;
                    rep.NT122.Value = qkb.First().NT122;

                    rep.NT16.Value = qkb.First().NT16;
                    rep.NT161.Value = qkb.First().NT161;
                    rep.NT162.Value = qkb.First().NT162;

                    rep.NT17.Value = qkb.First().NT17;
             
                }
                    var qcdha1 = (from dt in data.DThuocs
                                 join dtct in data.DThuoccts on dt.IDDon equals dtct.IDDon
                                 join dv in data.DichVus on dtct.MaDV equals dv.MaDV
                                 join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                                 join bn in data.BenhNhans on dt.MaBNhan equals bn.MaBNhan
                                // join vp in data.VienPhis on dt.MaBNhan equals vp.MaBNhan
                                 select new {bn.MaBNhan, bn.NoiTru, tn.TenRG,dtct.NgayNhap, dtct.SoLuong}).ToList();
                    var qcdha = (from q in qcdha1.Where(p => p.TenRG == "X-Quang" || p.TenRG.Contains("Siêu âm")).Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay)
                                 group q by new { } into kq
                                 select new {
                                    NT13=kq.Where(p=>p.TenRG=="X-Quang").Sum(p=>p.SoLuong),
                                    NT131 = kq.Where(p => p.TenRG == "X-Quang" && p.NoiTru == 1).Sum(p => p.SoLuong),
                                    NT132 = kq.Where(p => p.TenRG == "X-Quang" && p.NoiTru == 0).Sum(p => p.SoLuong),
                                    NT14 = kq.Where(p => p.TenRG.Contains("Siêu âm")).Sum(p => p.SoLuong),
                                    NT141 = kq.Where(p => p.TenRG.Contains("Siêu âm") && p.NoiTru == 1).Sum(p => p.SoLuong),
                                    NT142 = kq.Where(p => p.TenRG.Contains("Siêu âm") && p.NoiTru == 0).Sum(p => p.SoLuong),
                                 }).ToList();
                    if (qcdha.Count > 0)
                    {
                        rep.NT13.Value = qcdha.First().NT13;
                        rep.NT131.Value = qcdha.First().NT131;
                        rep.NT132.Value = qcdha.First().NT132;
                        rep.NT14.Value = qcdha.First().NT14;
                        rep.NT141.Value = qcdha.First().NT141;
                        rep.NT142.Value = qcdha.First().NT142;
                    }
                    if (chkBN.Checked == false)
                    {
                        var qksk = (from ma in _lKhoaP
                                    join p in qkb1.Where(p => p.DTuong == "KSK").Where(p => p.NgayKham >= tungay && p.NgayKham <= denngay) on ma.makp equals p.MaKP
                                    group p by new { } into kq
                                    select new
                                    {
                                        NT15 = kq.Where(p => p.NoiTru == 0).Select(p => p.MaBNhan).Count(),
                                        NT152 = kq.Where(p => p.NoiTru == 0 && p.TChung != "Khám sức khỏe định kỳ").Select(p => p.MaBNhan).Count(),
                                        NT153 = kq.Where(p => p.NoiTru == 0 && p.TChung == "Khám sức khỏe định kỳ").Select(p => p.MaBNhan).Count(),
                                    }).ToList();
                        if (qksk.Count > 0)
                        {
                            rep.NT15.Value = qksk.First().NT15;
                            rep.NT152.Value = qksk.First().NT152;
                            rep.NT153.Value = qksk.First().NT153;
                        }
                    }
                    else
                    {
                        var qksk = (from bn in data.BenhNhans.Where(p => p.DTuong == "KSK").Where(p => p.NNhap >= tungay && p.NNhap <= denngay)
                                    group bn by new { } into kq
                                    select new
                                    {
                                        NT15 = kq.Where(p => p.NoiTru == 0).Select(p => p.MaBNhan).Count(),
                                        NT152 = kq.Where(p => p.NoiTru == 0 && p.TChung != "Khám sức khỏe định kỳ").Select(p => p.MaBNhan).Count(),
                                        NT153 = kq.Where(p => p.NoiTru == 0 && p.TChung == "Khám sức khỏe định kỳ").Select(p => p.MaBNhan).Count(),
                                    }).ToList();
                        if (qksk.Count > 0)
                        {
                            rep.NT15.Value = qksk.First().NT15;
                            rep.NT152.Value = qksk.First().NT152;
                            rep.NT153.Value = qksk.First().NT153;
                        }
                    }
                    rep.DiaDanh.Value = DungChung.Bien.DiaDanh + ", ngày ..... tháng ..... năm .....";
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
                
            }
    
 
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

     
        private void grvDTuong_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {

            if (e.Column.Name == "Chọn")
            {
                if (grvKhoaphong.GetFocusedRowCellValue("tenkp") != null)
                {
                    string Ten = grvKhoaphong.GetFocusedRowCellValue("tenkp").ToString();

                    if (Ten == "Chọn tất cả")
                    {
                        if (_Kphong.First().chon == true)
                        {
                            foreach (var a in _Kphong)
                            {
                                a.chon = false;
                            }
                        }
                        else
                        {
                            foreach (var a in _Kphong)
                            {
                                a.chon = true;
                            }
                        }
                        grcKhoaphong.DataSource = "";
                        grcKhoaphong.DataSource = _Kphong.ToList();
                    }
                }
            
            }
        }

        private void grvKhoaphong_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {

        }

        private void grvKhoaphong_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "colChon")
            {
                if (grvKhoaphong.GetFocusedRowCellValue("tenkp") != null)
                {
                    string Ten = grvKhoaphong.GetFocusedRowCellValue("tenkp").ToString();

                    if (Ten == "Chọn tất cả")
                    {
                        if (_Kphong.First().chon == true)
                        {
                            foreach (var a in _Kphong)
                            {
                                a.chon = false;
                            }
                        }
                        else
                        {
                            foreach (var a in _Kphong)
                            {
                                a.chon = true;
                            }
                        }
                        grcKhoaphong.DataSource = "";
                        grcKhoaphong.DataSource = _Kphong.ToList();
                    }
                }
            }
        }

      

      
    }
}