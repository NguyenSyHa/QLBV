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
    public partial class Frm_BcKCBTe_BLam : DevExpress.XtraEditors.XtraForm
    {
        public Frm_BcKCBTe_BLam()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities _Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

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

            else return true;
        }

        private void Frm_BcKCBTe_BLam_Load(object sender, EventArgs e)
        {
            lupTuNgay.Focus();
            lupTuNgay.DateTime = System.DateTime.Now;
            lupDenNgay.DateTime = System.DateTime.Now;
            if (!File.Exists("TextKCBTe.txt"))
            {
                FileStream fs;
                fs = new FileStream("TextKCBTe.txt", FileMode.Create);
                StreamWriter sWriter = new StreamWriter(fs, Encoding.UTF8);
                sWriter.WriteLine("");
                sWriter.Flush();
                fs.Close();

            }
            string[] lines = File.ReadAllLines("TextKCBTe.txt");
            if (lines[lines.Length - 1] == "1")
            {
               
                txtKG.Text = lines[lines.Length - 2];
            }

       
        }

        private void btnInBC_Click(object sender, EventArgs e)
        {
            DateTime tungay = System.DateTime.Now.Date;
            DateTime denngay = System.DateTime.Now.Date;


            if (KTtaoBc())
            {
                FileStream fs = new FileStream("TextKCBTe.txt", FileMode.Append);
                StreamWriter writeFile = new StreamWriter(fs, Encoding.UTF8);//dùng streamwriter để ghi file

                writeFile.WriteLine(txtKG.Text);
                writeFile.WriteLine("1");//ghi từng dòng vào file TextBBKiemKeThuoc.txt
                writeFile.Flush();
                writeFile.Close();

                tungay = DungChung.Ham.NgayTu(lupTuNgay.DateTime);
                denngay = DungChung.Ham.NgayDen(lupDenNgay.DateTime);
                frmIn frm = new frmIn();
                BaoCao.Rep_BcKCBTe_BLam rep = new BaoCao.Rep_BcKCBTe_BLam();
                #region Hiển thị thời gian
                int nam = Convert.ToInt32(tungay.Year);
                int thang = Convert.ToInt32(tungay.Month);
                if (radIn.SelectedIndex == 0)
                { rep.KyHan.Value = "Từ ngày " + lupTuNgay.Text + " Đến ngày " + lupDenNgay.Text; }
                if (radIn.SelectedIndex == 1)
                {
                    if (thang > 1 && thang <= 3) { rep.KyHan.Value = "Quý I năm " + nam; }
                    if (thang > 3 && thang <= 6) { rep.KyHan.Value = "Quý II năm " + nam; }
                    if (thang > 6 && thang <= 9) { rep.KyHan.Value = "Quý III năm " + nam; }
                    if (thang > 9 && thang <= 12) { rep.KyHan.Value = "Quý IV năm " + nam; }
                }
                if (radIn.SelectedIndex == 2)
                {
                    rep.KyHan.Value = "6 tháng/ năm " + nam;
                }
                if (radIn.SelectedIndex == 3)
                {
                    rep.KyHan.Value = "9 tháng/ năm " + nam;
                }
                if (radIn.SelectedIndex == 4)
                { rep.KyHan.Value = "năm " + nam; }
                #endregion
                if (txtKG.Text != null && txtKG.Text != "") { rep.KinhGui.Value = txtKG.Text; }
                else
                {
                    rep.KinhGui.Value = "";
                }
                     #region Thống kê theo ngày khám bệnh
                    if (radTK.SelectedIndex == 0)
                    {
                        var id = (from kb in _Data.BNKBs.Where(p => p.NgayKham >= tungay).Where(p => p.NgayKham <= denngay)
                                  join k in _Data.KPhongs on kb.MaKP equals k.MaKP
                                  group kb by kb.MaBNhan into kq
                                  select new { kq.Key, IDKB = kq.Max(p => p.IDKB) }).ToList();

                        var qkb1 = (from k in id
                                    join kb in _Data.BNKBs on k.IDKB equals kb.IDKB
                                    join bn in _Data.BenhNhans on kb.MaBNhan equals bn.MaBNhan
                                    group new { kb, bn } by new { kb.NgayKham, kb.MaBNhan,bn.SThe, bn.NoiTru, bn.Tuoi, bn.DTuong, bn.DTNT } into kq
                                    select new { kq.Key.NgayKham, kq.Key.MaBNhan,kq.Key.SThe, kq.Key.NoiTru, kq.Key.Tuoi, kq.Key.DTuong, kq.Key.DTNT, }).ToList();

                        var qkb = (from p in qkb1.Where(p => p.Tuoi <6).Where(p => p.NgayKham >= tungay && p.NgayKham <= denngay)
                                   group p by new { } into kq
                                   select new
                                        {
                                            Sl1 = kq.Where(p=>p.SThe.Contains("TE")&&p.SThe.Substring(7,3)!="000").Select(p => p.MaBNhan).Count(),
                                            SL2 = kq.Where(p => p.SThe.Contains("TE") && p.SThe.Substring(7, 3) != "000"&&p.NoiTru==0&&p.DTNT==true).Select(p => p.MaBNhan).Count(),
                                            Sl3 = kq.Where(p => p.SThe.Contains("TE") && p.SThe.Substring(7, 3) != "000"&& p.NoiTru == 1).Select(p => p.MaBNhan).Count(),
                                            Sl4 = kq.Where(p => p.SThe.Contains("TE") && p.SThe.Substring(7, 3) == "000").Select(p => p.MaBNhan).Count(),
                                            SL5 = kq.Where(p => p.SThe.Contains("TE") && p.SThe.Substring(7, 3) == "000" && p.NoiTru == 0 && p.DTNT == true).Select(p => p.MaBNhan).Count(),
                                            Sl6 = kq.Where(p => p.SThe.Contains("TE") && p.SThe.Substring(7, 3) == "000" && p.NoiTru == 1).Select(p => p.MaBNhan).Count(),
                                        }).ToList();
                        if (qkb.Count > 0)
                        {
                            rep.SL1.Value = qkb.First().Sl1;
                            rep.SL2.Value = qkb.First().SL2;
                            rep.SL3.Value = qkb.First().Sl3;
                            rep.Sl4.Value = qkb.First().Sl4;
                            rep.SL5.Value = qkb.First().SL5;
                            rep.SL6.Value = qkb.First().Sl6;
                        }
                    }
                    #endregion
                    #region Thống kê theo ngày thanh toán
                    var qbn = (from vp in _Data.VienPhis
                                   join vpct in _Data.VienPhicts on vp.idVPhi equals vpct.idVPhi
                               join bn in _Data.BenhNhans on vp.MaBNhan equals bn.MaBNhan
                                   select new { vp.NgayTT, vp.MaBNhan,bn.Tuoi,bn.SThe, bn.NoiTru, bn.DTNT, bn.DTuong, vpct.TienBH }).ToList();

                    var qvp1 = (from kb in qbn.Where(p => p.Tuoi < 6).Where(p => p.NgayTT >= tungay && p.NgayTT <= denngay)
                               group kb by new { kb.MaBNhan,kb.DTNT,kb.DTuong,kb.NoiTru,kb.SThe} into kq
                               select new
                               {
                                   MaBNhan = kq.Key.MaBNhan,
                                   DTNT=kq.Key.DTNT,
                                   NoiTru=kq.Key.NoiTru,
                                   DTuong=kq.Key.DTuong,
                                   TienBH=kq.Sum(p=>p.TienBH),
                                   SThe=kq.Key.SThe,
                               }).ToList(); 
                var qvp = (from kb in qvp1
                                   group kb by new { } into kq
                                   select new
                                   {
                                       Sl1 = kq.Where(p => p.SThe.Contains("TE") && p.SThe.Substring(7, 3) != "000").Select(p => p.MaBNhan).Count(),
                                       SL2 = kq.Where(p => p.SThe.Contains("TE") && p.SThe.Substring(7, 3) != "000" && p.NoiTru == 0 && p.DTNT == true).Select(p => p.MaBNhan).Count(),
                                       Sl3 = kq.Where(p => p.SThe.Contains("TE") && p.SThe.Substring(7, 3) != "000" && p.NoiTru == 1).Select(p => p.MaBNhan).Count(),
                                       Sl4 = kq.Where(p => p.SThe.Contains("TE") && p.SThe.Substring(7, 3) == "000").Select(p => p.MaBNhan).Count(),
                                       SL5 = kq.Where(p => p.SThe.Contains("TE") && p.SThe.Substring(7, 3) == "000" && p.NoiTru == 0 && p.DTNT == true).Select(p => p.MaBNhan).Count(),
                                       Sl6 = kq.Where(p => p.SThe.Contains("TE") && p.SThe.Substring(7, 3) == "000" && p.NoiTru == 1).Select(p => p.MaBNhan).Count(),
                                       
                                       KP1 = kq.Where(p => p.SThe.Contains("TE") && p.SThe.Substring(7, 3) != "000").Sum(p => p.TienBH),
                                       KP2 = kq.Where(p => p.SThe.Contains("TE") && p.SThe.Substring(7, 3) != "000" && p.NoiTru == 0 && p.DTNT == true).Sum(p => p.TienBH),
                                       KP3 = kq.Where(p => p.SThe.Contains("TE") && p.SThe.Substring(7, 3) != "000" && p.NoiTru == 1).Sum(p => p.TienBH),
                                       KP4 = kq.Where(p => p.SThe.Contains("TE") && p.SThe.Substring(7, 3) == "000").Sum(p => p.TienBH),
                                       KP5 = kq.Where(p => p.SThe.Contains("TE") && p.SThe.Substring(7, 3) == "000" && p.NoiTru == 0 && p.DTNT == true).Sum(p => p.TienBH),
                                       KP6 = kq.Where(p => p.SThe.Contains("TE") && p.SThe.Substring(7, 3) == "000" && p.NoiTru == 1).Sum(p => p.TienBH),
                                   }).ToList();
                        if (qvp.Count > 0)
                        {
                            if(radTK.SelectedIndex==1)
                            {
                                rep.SL1.Value = qvp.First().Sl1;
                                rep.SL2.Value = qvp.First().SL2;
                                rep.SL3.Value = qvp.First().Sl3;
                                rep.Sl4.Value = qvp.First().Sl4;
                                rep.SL5.Value = qvp.First().SL5;
                                rep.SL6.Value = qvp.First().Sl6;
                                
                                rep.KP1.Value = qvp.First().KP1;
                                rep.KP2.Value = qvp.First().KP2;
                                rep.KP3.Value = qvp.First().KP3;
                                rep.KP4.Value = qvp.First().KP4;
                                rep.KP5.Value = qvp.First().KP5;
                                rep.KP6.Value = qvp.First().KP6;
                            }
                                
                         }
                    
                    #endregion
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();

                

            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       
    }
}