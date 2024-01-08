using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using System.IO;

namespace QLBV.FormThamSo
{
    public partial class Frm_BcCongTacKCB_BLac : DevExpress.XtraEditors.XtraForm
    {
        public Frm_BcCongTacKCB_BLac()
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
            var kphong = (from kp in data.KPhongs.Where(p=>p.PLoai == "Lâm sàng" || p.PLoai == "Phòng khám") select new { kp.TenKP, kp.MaKP }).ToList();
            if (kphong.Count > 0)
            {
                KPhong themmoi1 = new KPhong();
                themmoi1.tenkp = "Chọn tất cả";
                themmoi1.makp = 0;
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
            if (!File.Exists("TextCongTacKCB.txt"))
            {
                FileStream fs;
                fs = new FileStream("TextCongTacKCB.txt", FileMode.Create);
                StreamWriter sWriter = new StreamWriter(fs, Encoding.UTF8);
                sWriter.WriteLine("");
                sWriter.Flush();
                fs.Close();

            }
            string[] lines = File.ReadAllLines("TextCongTacKCB.txt");
            if (lines[lines.Length - 1] == "1")
            {

                txtHD.Text = lines[lines.Length - 4];
                txtCT.Text = lines[lines.Length - 3];
                txtKH.Text = lines[lines.Length - 2];
            }
        }
       
        private void btnInBC_Click(object sender, EventArgs e)
        {
            FileStream fs = new FileStream("TextCongTacKCB.txt", FileMode.Append);
            StreamWriter writeFile = new StreamWriter(fs, Encoding.UTF8);//dùng streamwriter để ghi file

            writeFile.WriteLine(txtHD.Text);
            writeFile.WriteLine(txtCT.Text);
            writeFile.WriteLine(txtKH.Text);
            writeFile.WriteLine("1");//ghi từng dòng vào file TextBBKiemKeThuoc.txt
            writeFile.Flush();
            writeFile.Close();
            
            DateTime tungay = DungChung.Ham.NgayTu(lupTuNgay.DateTime);
            DateTime denngay = DungChung.Ham.NgayDen(lupDenNgay.DateTime);
            List<KPhong> _lKhoaP = new List<KPhong>();
      
           frmIn frm = new frmIn();
           BaoCao.Rep_BcCongTacKCB_BLac rep = new BaoCao.Rep_BcCongTacKCB_BLac();
           if (txtHD.Text != null && txtHD.Text != "") { rep.HD.Value = txtHD.Text; }
           else
           {rep.HD.Value = "";}
           if (txtCT.Text != null && txtCT.Text != "") { rep.CT.Value = txtCT.Text; }
           else
           { rep.CT.Value = ""; }
           if (txtKH.Text != null && txtKH.Text != "") { rep.KH.Value = txtKH.Text; }
           else
           { rep.KH.Value = ""; }
           rep.DiaDanh.Value = DungChung.Bien.DiaDanh + ", ngày ..... tháng ..... năm .....";
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
           int _nt=-1,_ngt=-1;string _dt1="", _dt2="";
            if(radBN.SelectedIndex==0){_nt=1;_ngt=0;}
            if(radBN.SelectedIndex==1){_nt=1;_ngt=-1;}
            if(radBN.SelectedIndex==2){_nt=-1;_ngt=0;}
            if(radDT.SelectedIndex==0){_dt1="BHYT";_dt2="Dịch vụ";}
            if(radDT.SelectedIndex==1){_dt1="BHYT";_dt2="";}
            if(radDT.SelectedIndex==2){_dt1="";_dt2="Dịch vụ";}
           if (KTtaoBc())
            {
                var qvp1 = (from vp in data.VienPhis
                            join bn in data.BenhNhans on vp.MaBNhan equals bn.MaBNhan
                            join rv in data.RaViens on vp.MaBNhan equals rv.MaBNhan
                            select new { vp.NgayTT, vp.MaBNhan, bn.NoiTru, bn.DTuong, bn.DTNT,bn.Tuoi, rv.Status,rv.SoNgaydt }).ToList();
                var qvpkb = (from vp in qvp1.Where(p => p.NgayTT >= tungay && p.NgayTT <= denngay).Where(p => p.NoiTru == _nt || p.NoiTru == _ngt).Where(p => p.DTuong == _dt1 || p.DTuong == _dt2)
                             group vp by new { } into kq
                             select new
                             {
                                 KB1 = kq.Select(p => p.MaBNhan).Count(),
                                 KB13=kq.Where(p=>p.Tuoi<6).Select(p=>p.MaBNhan).Count(),
                                 KB3=kq.Where(p=>p.NoiTru==1).Select(p=>p.MaBNhan).Count(),
                                 KB32=kq.Where(p=>p.NoiTru==1&&p.Tuoi<6).Select(p=>p.MaBNhan).Count(),
                                 KB4=kq.Where(p=>p.NoiTru==1).Sum(p=>p.SoNgaydt),
                                 KB42 = kq.Where(p => p.NoiTru == 1&&p.Tuoi<6).Sum(p => p.SoNgaydt),
                                 KB6=kq.Where(p=>p.NoiTru==0&&p.DTNT==true).Select(p=>p.MaBNhan).Count(),
                                 KB62 = kq.Where(p => p.NoiTru == 0&&p.DTNT==true&&p.Tuoi<6).Select(p => p.MaBNhan).Count(),
                                 KB7 = kq.Where(p => p.Status==1).Select(p => p.MaBNhan).Count(),
                             }).ToList();
               if(qvpkb.Count>0)
                   {
                       rep.C1.Value = qvpkb.First().KB1;
                       rep.C1b.Value = qvpkb.First().KB13;
                       rep.C3.Value = qvpkb.First().KB3;
                       rep.C32.Value = qvpkb.First().KB32;
                       rep.C4.Value = qvpkb.First().KB4;
                       rep.C42.Value = qvpkb.First().KB42;
                       rep.C5.Value = qvpkb.First().KB3 != 0 ? qvpkb.First().KB4 / qvpkb.First().KB3 : 0;
                       rep.C6.Value = qvpkb.First().KB6;
                       rep.C62.Value = qvpkb.First().KB62;
                       rep.C7.Value = qvpkb.First().KB7;
                   }
            
               var qdt1 = (from dtct in data.DThuoccts  //.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay)
                           join dt in data.DThuocs on dtct.IDDon equals dt.IDDon
                           join vp in data.VienPhis on dt.MaBNhan equals vp.MaBNhan
                           join bn in data.BenhNhans on dt.MaBNhan equals bn.MaBNhan
                           join dv in data.DichVus on dtct.MaDV equals dv.MaDV
                           join tnhom in data.TieuNhomDVs on dv.IdTieuNhom equals tnhom.IdTieuNhom
                           select new { bn.MaBNhan, bn.TenBNhan,bn.DTuong, bn.NoiTru, dtct.MaKP,dtct.IDCD, vp.NgayTT, tnhom.TenRG,dtct.SoLuong }).ToList();
               var qdt = (from kp in _lKhoaP
                          join qd in qdt1 on kp.makp equals qd.MaKP
                          //group new { qd } by new {qd.NgayTT,qd.IDCD,qd.TenRG, qd.MaBNhan, qd.NoiTru, qd.TenBNhan, qd.DTuong,  kp.makp, kp.tenkp,qd.SoLuong } into kq
                          //select new {kq.Key.NgayTT,kq.Key.IDCD,kq.Key.TenRG, kq.Key.MaBNhan, kq.Key.NoiTru, kq.Key.TenBNhan, kq.Key.DTuong,kq.Key.makp, kq.Key.tenkp,kq.Key.SoLuong }).ToList();
                          select new { qd.NgayTT, qd.IDCD, qd.TenRG, qd.MaBNhan, qd.NoiTru, qd.TenBNhan, qd.DTuong, kp.makp, kp.tenkp, qd.SoLuong }).ToList();
               var qvpcls = (from cls in qdt.Where(p => p.NgayTT >= tungay && p.NgayTT <= denngay).Where(p => p.NoiTru == _nt || p.NoiTru == _ngt).Where(p => p.DTuong == _dt1 || p.DTuong == _dt2)
                              group cls by new { } into kq
                              select new {
                                  CLS8 = kq.Where(p => p.TenRG.Contains("XN")).Sum(p => p.SoLuong),
                              CLS9 = kq.Where(p => p.TenRG=="X-Quang").Sum(p => p.SoLuong),
                                  CLS10 = kq.Where(p => p.TenRG.Contains("Siêu âm")).Sum(p => p.SoLuong),
                              CLS11 = kq.Where(p => p.TenRG=="Điện tim").Sum(p => p.SoLuong),
                              CLS12 = kq.Where(p => p.TenRG=="Phẫu thuật").Sum(p => p.SoLuong),
                              CLS13 = kq.Where(p => p.TenRG.Contains("Nội soi")).Sum(p => p.SoLuong),
                              }).ToList();
                if (qvpcls.Count > 0)
                {
                    rep.C8.Value = qvpcls.First().CLS8;
                    rep.C9.Value = qvpcls.First().CLS9;
                    rep.C10.Value = qvpcls.First().CLS10;
                    rep.C11.Value = qvpcls.First().CLS11;
                    rep.C12.Value = qvpcls.First().CLS12;
                    rep.C13.Value = qvpcls.First().CLS13;
                }
                var id = (from kb in data.BNKBs
                          group kb by kb.MaBNhan into kq
                          select new { kq.Key, IDKB = kq.Max(p => p.IDKB) }).ToList();

                var qkb = (from k in id
                           join kb in data.BNKBs on k.IDKB equals kb.IDKB
                           join bn in data.BenhNhans on kb.MaBNhan equals bn.MaBNhan
                           group new { bn, kb } by new { kb.MaBNhan, kb.ChanDoan,bn.NoiTru,bn.DTuong,bn.DTNT,bn.Tuoi} into kq
                           select new { kq.Key.MaBNhan,kq.Key.ChanDoan, kq.Key.NoiTru, kq.Key.DTNT,kq.Key.DTuong,kq.Key.Tuoi }).ToList();

                var qb = (from kb in qkb.Where(p => p.NoiTru == _nt || p.NoiTru == _ngt).Where(p => p.DTuong == _dt1 || p.DTuong == _dt2)
                          join p in data.VienPhis.Where(p => p.NgayTT >= tungay && p.NgayTT <= denngay) on kb.MaBNhan equals p.MaBNhan
                               group  kb  by new {} into kq
                               select new
                               {
                                  C16=kq.Where(p=>p.ChanDoan.Contains("cúm")||p.ChanDoan.Contains("Cúm")).Select(p=>p.MaBNhan).Count(),
                                  C17 = kq.Where(p => p.ChanDoan.Contains("uai bị")).Select(p => p.MaBNhan).Count(),
                                  C18 = kq.Where(p => p.ChanDoan.Contains("ủy đậu") || p.ChanDoan.Contains("uỷ đậu")).Select(p => p.MaBNhan).Count(),
                                  C19 = kq.Where(p => p.ChanDoan.Contains("ội chứng lỵ") || p.ChanDoan.Contains("ội chứng lị")).Select(p => p.MaBNhan).Count(),
                                  C20 = kq.Where(p => p.ChanDoan.Contains("sởi") || p.ChanDoan.Contains("Sởi")).Select(p => p.MaBNhan).Count(),
                                  C21 = kq.Where(p => p.ChanDoan.Contains("iêu chảy")).Select(p => p.MaBNhan).Count(),
                                  C22 = kq.Where(p => p.ChanDoan.Contains("ubela")).Select(p => p.MaBNhan).Count(),
                                  C23 = kq.Where(p => p.ChanDoan.Contains("denovirut")).Select(p => p.MaBNhan).Count(),
                                  C24 = kq.Where(p => p.ChanDoan.Contains("gan") || p.ChanDoan.Contains("Gan")).Select(p => p.MaBNhan).Count(),
                                  C25 = kq.Where(p => p.ChanDoan.Contains("hiệt thán")).Select(p => p.MaBNhan).Count(),
                                  C26 = kq.Where(p => p.ChanDoan.Contains("tay chân miệng") || p.ChanDoan.Contains("chân tay miệng") || p.ChanDoan.Contains("Tay-Chân-Miệng") || p.ChanDoan.Contains("Tay-Chân-Miệng")).Select(p => p.MaBNhan).Count(),
                                  C27 = kq.Where(p => p.ChanDoan.Contains("ốt rét")).Select(p => p.MaBNhan).Count(),
                                  C271 = kq.Where(p => p.ChanDoan.Contains("ốt rét")&&p.Tuoi<1).Select(p => p.MaBNhan).Count(),
                                  C272 = kq.Where(p => p.ChanDoan.Contains("ốt rét")&&p.Tuoi<5).Select(p => p.MaBNhan).Count(),
                                }).ToList();

                if (qb.Count > 0)
                {
                    rep.C16.Value = qb.First().C16;
                    rep.C17.Value = qb.First().C17;
                    rep.C18.Value = qb.First().C18;
                    rep.C19.Value = qb.First().C19;
                    rep.C20.Value = qb.First().C20;
                    rep.C21.Value = qb.First().C21;
                    rep.C22.Value = qb.First().C22;
                    rep.C23.Value = qb.First().C23;
                    rep.C24.Value = qb.First().C24;
                    rep.C25.Value = qb.First().C25;
                    rep.C26.Value = qb.First().C26;
                    rep.C27.Value = qb.First().C27;
                    rep.C27a.Value = qb.First().C271;
                    rep.C27b.Value = qb.First().C272;
                }
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

        private void radioGroup2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void memoEdit1_EditValueChanged(object sender, EventArgs e)
        {

        }

      

      
    }
}