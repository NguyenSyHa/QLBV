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
    public partial class Frm_BcHoatDongKCB_VY01 : DevExpress.XtraEditors.XtraForm
    {
        public Frm_BcHoatDongKCB_VY01()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

        private bool kt()
        {
            if (lupNgaytu.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn ngày bắt đầu in báo cáo");
                lupNgaytu.Focus();
                return false;
            }
            if (lupNgayden.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn ngày kết thúc in báo cáo");
                lupNgayden.Focus();
                return false;
            }

            else return true;
        }
        private string theoquy()
        {
            string quy = "";

            if (radIn.SelectedIndex == 1)
            {
               
                        quy = (" Báo cáo hoạt động khám chữa bệnh tháng "+cbo_thang.Text+" năm "+txtNam.Text);
                      

            }
            if (radIn.SelectedIndex == 0)
            {
                quy = ("Báo cáo hoạt động khám chữa bệnh từ ngày  " + lupNgaytu.DateTime.ToString().Substring(0, 10) + "  đến ngày  " + lupNgayden.DateTime.ToString().Substring(0, 10)).ToUpper();

            }
            else if (radIn.SelectedIndex == 2)
            {
                quy = (" Báo cáo hoạt động khám chữa bệnh quý "+cbo_Quy.Text+ " năm "+ txtNam.Text);
            }
            return quy;
        }

  
        private void Frm_BcHoatDongKCB_VY01_Load(object sender, EventArgs e)
        {
            lupNgaytu.Focus();
            radIn_SelectedIndexChanged(sender, e);
            lupNgaytu.DateTime = System.DateTime.Now;
            lupNgayden.DateTime = System.DateTime.Now;
            try
            {
                if (!File.Exists("TextKT.txt"))
                {
                    FileStream fs;
                    fs = new FileStream("TextKT.txt", FileMode.Create);
                    StreamWriter sWriter = new StreamWriter(fs, Encoding.UTF8);
                    sWriter.WriteLine("");
                    sWriter.Flush();
                    fs.Close();

                }
                string[] lines = File.ReadAllLines("TextKT.txt");
                if (lines[lines.Length - 1] == "1")
                {

                    txtKT.Text = lines[lines.Length - 2];
                }
            }
            catch (Exception)
            {
                txtKT.Text = "";
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                FileStream fs = new FileStream("TextKT.txt", FileMode.Append);
                StreamWriter writeFile = new StreamWriter(fs, Encoding.UTF8);
                writeFile.WriteLine(txtKT.Text);
                writeFile.WriteLine("1");
                writeFile.Flush();
                writeFile.Close();
            }
            catch (Exception)
            {

            }
            frmIn frm = new frmIn();

            if (kt())
            {
                   BaoCao.Rep_BcHoatDongKCB_VY01 rep = new BaoCao.Rep_BcHoatDongKCB_VY01();
                dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                DateTime ngaytu = System.DateTime.Now.Date;
                DateTime ngayden = System.DateTime.Now.Date;
                DateTime ngaytu2 = System.DateTime.Now.Date;
                DateTime ngayden2 = System.DateTime.Now.Date;
                ngaytu = DungChung.Ham.NgayTu(lupNgaytu.DateTime);
                ngayden = DungChung.Ham.NgayDen(lupNgayden.DateTime);
            
                ngaytu2 = DungChung.Ham.NgayTu(lupNgaytu.DateTime.AddMonths(-1));
                ngayden2 = ngayden.AddDays(-1);
               
                if (radIn.SelectedIndex == 1)
                {
                 
                    ngayden2 = ngaytu.AddDays(-1);
                    int thang = ngayden2.Month;
                    ngaytu2 = GetFirstDayOfMonth(thang, ngayden2.Year);
                }
                if (radIn.SelectedIndex == 2)
                { 
                    ngayden2 = ngaytu.AddDays(-1);
                    int thang = ngayden2.AddMonths(-2).Month;
                    ngaytu2 = GetFirstDayOfMonth(thang, ngayden2.Year);
                    rep.xrTableCell4.Text = "So sánh quý trước(*)";

                }
                int tuoiTu = 0, tuoiDen = 200;
                if (!string.IsNullOrEmpty(txtTuoiTu.Text))
                    tuoiTu = Convert.ToInt32(txtTuoiTu.Text);
                if (!string.IsNullOrEmpty(txtTuoiden.Text))
                    tuoiDen = Convert.ToInt32(txtTuoiden.Text);
             
                rep.TuNgayDenNgay.Value = theoquy();
                rep.KemTheo.Value = txtKT.Text;
                var qkb = (from bn in dataContext.BenhNhans.Where(p=>p.NoiTru == 0).Where(p => p.NNhap >= ngaytu).Where(p => p.NNhap <= ngayden)
                           join rv in dataContext.RaViens on bn.MaBNhan equals rv.MaBNhan into kq
                           from kq1 in kq.DefaultIfEmpty()
                           where (bn.Tuoi != null && bn.Tuoi >= tuoiTu && bn.Tuoi <= tuoiDen)                          
                           select new { MaBNhan = bn.MaBNhan, DTuong = bn.DTuong, SThe =bn.SThe, NoiTru = bn.NoiTru, bn.DTNT, kq1 }).ToList();
                var qkb2 = (from bn in dataContext.BenhNhans.Where(p=>p.NoiTru == 0).Where(p => p.NNhap >= ngaytu2).Where(p => p.NNhap <= ngayden2)
                            join rv in dataContext.RaViens on bn.MaBNhan equals rv.MaBNhan into kq
                            from kq1 in kq.DefaultIfEmpty()
                           where (bn.Tuoi != null && bn.Tuoi >= tuoiTu && bn.Tuoi <= tuoiDen)
                           select new { MaBNhan = bn.MaBNhan, DTuong = bn.DTuong, SThe = bn.SThe, NoiTru = bn.NoiTru, bn.DTNT, kq1 }).ToList();

                var qdtri = (from bn in dataContext.BenhNhans.Where(p => p.NoiTru == 1)
                              join vv in dataContext.VaoViens on bn.MaBNhan equals vv.MaBNhan
                              join rv in dataContext.RaViens on bn.MaBNhan equals rv.MaBNhan into kq
                              from kq1 in kq.DefaultIfEmpty()
                            where (vv.NgayVao >= ngaytu && vv.NgayVao <= ngayden) ||
                              (kq1 != null && ((kq1.NgayRa >= ngaytu && kq1.NgayRa <= ngayden)) || (kq1 != null && (vv.NgayVao < ngaytu && kq1.NgayRa > ngayden)))
                             select new { MaBNhan = bn.MaBNhan, DTuong = bn.DTuong, SThe = bn.SThe, NoiTru = bn.NoiTru, SoNgaydt = kq1 != null ? kq1.SoNgaydt.Value : 0, KetQua = kq1.KetQua != null ? kq1.KetQua : "", Status = kq1 != null ? kq1.Status.Value : 0 }).ToList();

                var qdtri2 = (from bn in dataContext.BenhNhans.Where(p => p.NoiTru == 1)
                            join vv in dataContext.VaoViens on bn.MaBNhan equals vv.MaBNhan
                            join rv in dataContext.RaViens on bn.MaBNhan equals rv.MaBNhan into kq
                            from kq1 in kq.DefaultIfEmpty()
                            where (vv.NgayVao >= ngaytu2 && vv.NgayVao <= ngayden2) ||
                              (kq1 != null && ((kq1.NgayRa >= ngaytu2 && kq1.NgayRa <= ngayden2) || (vv.NgayVao < ngaytu2 && kq1.NgayRa > ngayden2)))
                            select new { MaBNhan = bn.MaBNhan, DTuong = bn.DTuong, SThe = bn.SThe, NoiTru = bn.NoiTru, kq1 }).ToList();


                if (qkb.Count > 0)
                {
                    //
                    int a1 = qkb.Select(p => p.MaBNhan).Count();
                    if (a1 > 0)
                        rep.TH1.Value = a1.ToString();
                    else
                        rep.TH1.Value = "";
                    int b1 = qkb2.Select(p => p.MaBNhan).Count();
                    rep.TTr1.Value = (a1 - b1).ToString();
                    //
                    int a2 = qkb.Where(p => p.DTuong.Contains("BHYT")).Select(p => p.MaBNhan).Count();
                    if (a2 > 0)
                        rep.TH2.Value = a2;
                    else rep.TH2.Value = "";
                    int b2 = qkb2.Where(p => p.DTuong.Contains("BHYT")).Select(p => p.MaBNhan).Count();
                    rep.TTr2.Value = a2 - b2;
                    //

                    int a3 = qkb.Where(p => p.DTuong.Contains("BHYT")).Where(p => p.SThe.ToString().Substring(0, 2).Contains("HN")).Select(p => p.MaBNhan).Count();
                    if (a3 > 0)
                        rep.TH3.Value = a3;
                    else rep.TH3.Value = "";
                    int b3 = qkb2.Where(p => p.DTuong.Contains("BHYT")).Where(p => p.SThe.ToString().Substring(0, 2).Contains("HN")).Select(p => p.MaBNhan).Count();
                    rep.TTr3.Value = a3 - b3;
                    //
                    int a4 = qdtri.Where(p => p.DTuong.Contains("BHYT")).Where(p => p.SThe.ToString().Substring(0, 2).Contains("HN")).Select(p => p.MaBNhan).Count();
                    if (a4 > 0)
                    {
                        rep.TH4.Value = a4;
                    }
                    else rep.TH4.Value = "";
                    int b4 = qdtri2.Where(p => p.DTuong.Contains("BHYT")).Where(p => p.SThe.ToString().Substring(0, 2).Contains("HN")).Select(p => p.MaBNhan).Count();
                    rep.TTr4.Value = a4 - b4;
                    //
                    int a5 = qdtri.Select(p => p.MaBNhan).Count();
                    rep.TH5.Value = a5 == 0 ? "" : a5.ToString();
                    int b5 = qdtri2.Select(p => p.MaBNhan).Count();
                    rep.TTr5.Value = a5 - b5;
                    //int a6 = qkb.Where(p => p.NoiTru == 0).Select(p => p.MaBNhan).Count();
                    ////rep.TH6.Value = a6 == 0?"":a6.ToString();
                    //if (a6 > 0)
                    //    rep.TH6.Value = a6.ToString();
                    //else
                    //    rep.TH6.Value = "";

                }

                //var qct = (from kb in dataContext.BNKBs.Where(p => p.NgayKham >= ngaytu).Where(p => p.NgayKham <= ngayden)
                //           join bn in dataContext.BenhNhans on kb.MaBNhan equals bn.MaBNhan
                //           where (bn.Tuoi != null && bn.Tuoi >= tuoiTu && bn.Tuoi <= tuoiDen)
                //           group new { bn, kb } by new { bn.DTuong, kb.MaBNhan, bn.SThe, bn.NoiTru, kb.PhuongAn } into kq
                //           select new { MaBNhan = kq.Key.MaBNhan, NoiTru = kq.Key.NoiTru, PhuongAn = kq.Key.PhuongAn }).ToList();
                //var qct2 = (from kb in dataContext.BNKBs.Where(p => p.NgayKham >= ngaytu2).Where(p => p.NgayKham <= ngayden2)
                //            join bn in dataContext.BenhNhans on kb.MaBNhan equals bn.MaBNhan
                //            where (bn.Tuoi != null && bn.Tuoi >= tuoiTu && bn.Tuoi <= tuoiDen)
                //            group new { bn, kb } by new { bn.DTuong, kb.MaBNhan, bn.SThe, bn.NoiTru, kb.PhuongAn } into kq
                //            select new { MaBNhan = kq.Key.MaBNhan, NoiTru = kq.Key.NoiTru, PhuongAn = kq.Key.PhuongAn }).ToList();
              
                    int a7 = qdtri.Where(p=> p.Status == 1).Select(p => p.MaBNhan).Count();
                    if (a7 > 0)
                        rep.TH7.Value = a7;
                    else rep.TH7.Value = "";
                    int b7 = qdtri.Where(p => p.Status == 2).Select(p => p.MaBNhan).Count();
                    rep.TTr7.Value = a7 - b7;
                //var qrv = (from rv in dataContext.RaViens.Where(p => p.NgayRa >= ngaytu).Where(p => p.NgayRa <= ngayden)
                //           join bn in dataContext.BenhNhans on rv.MaBNhan equals bn.MaBNhan
                //           where (bn.Tuoi != null && bn.Tuoi >= tuoiTu && bn.Tuoi <= tuoiDen)
                //           group new { bn, rv } by new { rv.SoNgaydt, bn.NoiTru, rv.MaBNhan, rv.KetQua } into kq
                //           select new { SoNgaydt = kq.Sum(p => p.rv.SoNgaydt), NoiTru = kq.Key.NoiTru, MaBnhan = kq.Key.MaBNhan, KetQua = kq.Key.KetQua }).ToList();
                //var qrv2 = (from rv in dataContext.RaViens.Where(p => p.NgayRa >= ngaytu2).Where(p => p.NgayRa <= ngayden2)
                //            join bn in dataContext.BenhNhans on rv.MaBNhan equals bn.MaBNhan
                //            where (bn.Tuoi != null && bn.Tuoi >= tuoiTu && bn.Tuoi <= tuoiDen)
                //            group new { bn, rv } by new { rv.SoNgaydt, bn.NoiTru, rv.MaBNhan, rv.KetQua } into kq
                //            select new { SoNgaydt = kq.Sum(p => p.rv.SoNgaydt), NoiTru = kq.Key.NoiTru, MaBnhan = kq.Key.MaBNhan, KetQua = kq.Key.KetQua }).ToList();
               
                    int a8 = qdtri.Sum(p => p.SoNgaydt);
                    if (a8 > 0)
                        rep.TH8.Value = a8;
                    else rep.TH8.Value = "";
                    int b8 = qdtri2.Where(p => p.kq1 != null).Sum(p => p.kq1.SoNgaydt ?? 0);
                    rep.TTr8.Value = a8 - b8;
                //var qdtnt = (from bn in dataContext.BenhNhans.Where(p => p.NNhap >= ngaytu).Where(p => p.NNhap <= ngayden)
                //             where (bn.Tuoi != null && bn.Tuoi >= tuoiTu && bn.Tuoi <= tuoiDen)
                //             select new { MaBNhan = bn.MaBNhan, DTuong = bn.DTuong, bn.SThe, bn.NoiTru, }).ToList();
                //var qdtnt2 = (from
                //               bn in dataContext.BenhNhans.Where(p => p.NNhap >= ngaytu2).Where(p => p.NNhap <= ngayden2)
                //              where (bn.Tuoi != null && bn.Tuoi >= tuoiTu && bn.Tuoi <= tuoiDen)
                //              select new { MaBNhan = bn.MaBNhan, DTuong = bn.DTuong, SThe = bn.SThe, NoiTru = bn.NoiTru, }).ToList();
                if (qkb.Count > 0)
                {
                    int a6 = qkb.Where(p => p.DTNT == false).Select(p => p.MaBNhan).Count();
                    rep.TH6.Value = a6 == 0 ? "" : a6.ToString();
                    int b6 = qkb2.Where(p => p.DTNT == false).Select(p => p.MaBNhan).Count();
                    rep.TTr6.Value = a6 - b6;


                    int a9 = qkb.Where(p => p.kq1 != null).Sum(p => p.kq1.SoNgaydt ?? 0);
                    if (a9 > 0)
                        rep.TH9.Value = a9;
                    else rep.TH9.Value = "";
                    int b9 = qkb2.Where(p => p.kq1 != null).Sum(p => p.kq1.SoNgaydt ?? 0);                    
                    rep.TTr9.Value = a9 - b9;

                }
                //int a9 = Convert.ToInt32(qrv.Where(p => p.NoiTru == 0).;
                //if (a9 > 0)
                //    rep.TH9.Value = a9;
                //else rep.TH9.Value = "";

                //var qtv = (from rv in qrv.Where(p => p.KetQua != null && p.KetQua.Contains("Tử vong"))
                //           group new { rv } by new { rv.MaBnhan } into kq
                //           select new { MaBNhan = kq.Key.MaBnhan }).ToList();
                //var qtv2 = (from rv in qrv2.Where(p => p.KetQua != null && p.KetQua.Contains("Tử vong"))
                //            group new { rv } by new { rv.MaBnhan } into kq
                //            select new { MaBNhan = kq.Key.MaBnhan }).ToList();

                int a10 = qdtri.Where(p => p.KetQua == "Tử vong").Count();
                if (a10 > 0)
                    rep.TH10.Value = a10;
                else rep.TH10.Value = "";
                int b10 = qdtri2.Where(p => p.kq1 != null && p.kq1.KetQua == "Tử vong").Count();
                rep.TTr10.Value = a10 - b10;

                var dvu = (from dv in dataContext.DichVus
                           join tnhomdv in dataContext.TieuNhomDVs on dv.IdTieuNhom equals tnhomdv.IdTieuNhom
                           select new { dv.MaDV, tnhomdv.TenRG }).ToList();
                var qcls = (from cls in dataContext.CLS
                            join cd in dataContext.ChiDinhs on cls.IdCLS equals cd.IdCLS
                            join bn in dataContext.BenhNhans on cls.MaBNhan equals bn.MaBNhan
                            where (bn.Tuoi != null && bn.Tuoi >= tuoiTu && bn.Tuoi <= tuoiDen)
                            where (cls.NgayTH >= ngaytu && cls.NgayTH <= ngayden)
                            select new { bn.MaBNhan, cd.MaDV, cls.Status }).ToList();
                var qcls1 = (from cls in qcls
                             join dv in dvu on cls.MaDV equals dv.MaDV
                             group new { cls, dv } by new { cls.MaBNhan, dv.TenRG, cls.Status } into kq
                             select new
                             {

                                 MaBNhan = kq.Key.MaBNhan,
                                 TenRG = kq.Key.TenRG,
                                 Status = kq.Key.Status,
                                 SL = kq.Select(p => p.cls.MaBNhan).Count(),


                             }).ToList();
                var qcls11 = (from cls in dataContext.CLS
                              join cd in dataContext.ChiDinhs on cls.IdCLS equals cd.IdCLS
                              join bn in dataContext.BenhNhans on cls.MaBNhan equals bn.MaBNhan
                              where (bn.Tuoi != null && bn.Tuoi >= tuoiTu && bn.Tuoi <= tuoiDen)
                              where (cls.NgayTH >= ngaytu2 && cls.NgayTH <= ngayden2)
                              select new { bn.MaBNhan, cd.MaDV, cls.Status }).ToList();
                var qcls2 = (from cls in qcls11
                             join dv in dvu on cls.MaDV equals dv.MaDV
                             group new { cls, dv } by new { cls.MaBNhan, dv.TenRG, cls.Status } into kq
                             select new
                             {

                                 MaBNhan = kq.Key.MaBNhan,
                                 TenRG = kq.Key.TenRG,
                                 Status = kq.Key.Status,
                                 SL = kq.Select(p => p.cls.MaBNhan).Count(),


                             }).ToList();

                int a11 = qcls1.Where(p => p.Status == 1).Where(p => p.TenRG.Contains("Phẫu thuật")).Sum(p => p.SL);
                if (a11 > 0)
                    rep.TH11.Value = a11.ToString();
                else
                    rep.TH11.Value = "";
                int b11 = qcls2.Where(p => p.Status == 1).Where(p => p.TenRG.Contains("Phẫu thuật")).Sum(p => p.SL);
                rep.TTr11.Value = a11 - b11;
                //
                int a12 = qcls1.Where(p => p.Status == 1).Where(p => p.TenRG.Contains("XN")).Sum(p => p.SL);
                int b12 = qcls2.Where(p => p.Status == 1).Where(p => p.TenRG.Contains("XN")).Sum(p => p.SL);
                if (a12 > 0)
                    rep.TH12.Value = a12.ToString();
                else
                    rep.TH12.Value = "";
                rep.TTr12.Value = a12 - b12;
                //
                int a13 = qcls1.Where(p => p.Status == 1).Where(p => p.TenRG.Contains("X-Quang")).Sum(p => p.SL);
                int b13 = qcls2.Where(p => p.Status == 1).Where(p => p.TenRG.Contains("X-Quang")).Sum(p => p.SL);
                if (a13 > 0)
                    rep.TH13.Value = a13.ToString();
                else
                    rep.TH13.Value = "";
                rep.TTr13.Value = a13 - b13;
                //
                int a14 = qcls1.Where(p => p.Status == 1).Where(p => p.TenRG.Contains("Siêu âm")).Sum(p => p.SL);
                int b14 = qcls2.Where(p => p.Status == 1).Where(p => p.TenRG.Contains("Siêu âm")).Sum(p => p.SL);
                if (a14 > 0)
                    rep.TH14.Value = a14.ToString();
                else
                    rep.TH14.Value = "";
                rep.TTr14.Value = a14 - b14;
                int a15 = qcls1.Where(p => p.Status == 1).Where(p => p.TenRG.Contains("Điện tim")).Sum(p => p.SL);
                int b15 = qcls2.Where(p => p.Status == 1).Where(p => p.TenRG.Contains("Điện tim")).Sum(p => p.SL);
                if (a15 > 0)
                    rep.TH15.Value = a15.ToString();
                else
                    rep.TH15.Value = "";
                rep.TTr15.Value = a15 - b15;
                //
                int a16 = qcls1.Where(p => p.Status == 1).Where(p => p.TenRG.Contains("Lưu huyết não")).Sum(p => p.SL);
                int b16 = qcls2.Where(p => p.Status == 1).Where(p => p.TenRG.Contains("Lưu huyết não")).Sum(p => p.SL);
                if (a16 > 0)
                    rep.TH16.Value = a16.ToString();
                else
                    rep.TH16.Value = "";
                rep.TTr16.Value = a16 - b16;
                int a17 = qcls1.Where(p => p.Status == 1).Where(p => p.TenRG.Contains("Nội soi Tai-Mũi-Họng")).Sum(p => p.SL);
                int b17 = qcls2.Where(p => p.Status == 1).Where(p => p.TenRG.Contains("Nội soi Tai-Mũi-Họng")).Sum(p => p.SL);
                if (a17 > 0)
                    rep.TH17.Value = a17.ToString();
                else
                    rep.TH17.Value = "";
                rep.TTr17.Value = a17 - b17;
                int a18 = qcls1.Where(p => p.Status == 1).Where(p => p.TenRG.Equals("Nội soi")).Sum(p => p.SL);
                int b18 = qcls2.Where(p => p.Status == 1).Where(p => p.TenRG.Equals("Nội soi")).Sum(p => p.SL);
                if (a18 > 0)
                    rep.TH18.Value = a18.ToString();
                else
                    rep.TH18.Value = "";
                rep.TTr18.Value = a18 - b18;
                //else MessageBox.Show("Không có dữ liệu");
                rep.BindingData();
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();

            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void radIn_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (radIn.SelectedIndex == 0)
            {
                grp_Ngay.Enabled = true;
                grp_Thang.Enabled = false;
                grp_Quy.Enabled = false;
                cbo_thang.SelectedIndex = -1;
                cbo_Quy.SelectedIndex = -1;
            }
            if (radIn.SelectedIndex == 1)
            {

                grp_Ngay.Enabled = false;
                grp_Thang.Enabled = true;
                grp_Quy.Enabled = false;
                cbo_thang.SelectedIndex = -1;
                cbo_Quy.SelectedIndex = -1;
            } if (radIn.SelectedIndex == 2)
            {

                grp_Ngay.Enabled = false;
                grp_Thang.Enabled = false;
                grp_Quy.Enabled = true;
                cbo_thang.SelectedIndex = -1;
                cbo_Quy.SelectedIndex = -1;
            }
        }
        public static DateTime GetFirstDayOfMonth(int iMonth, int year)
        {
            DateTime dtResult = new DateTime(year, iMonth, 1);
            dtResult = dtResult.AddDays((-dtResult.Day) + 1);
            return dtResult;
        }
        public static DateTime GetLastDayOfMonth(int iMonth, int year)
        {
            DateTime dtResult = new DateTime(year, iMonth, 1);
            dtResult = dtResult.AddMonths(1);
            dtResult = dtResult.AddDays(-(dtResult.Day));
            return dtResult;
        }
        private void textEdit1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int thang = cbo_thang.SelectedIndex+1;
            if (thang > 0)
            {
                lupNgaytu.DateTime = GetFirstDayOfMonth(thang,Convert.ToInt32(txtNam.Text));
                lupNgayden.DateTime = GetLastDayOfMonth(thang, Convert.ToInt32(txtNam.Text));
            }
        }

        private void cboNam_SelectedIndexChanged(object sender, EventArgs e)
        {
            int thang = 0;
            int quy = cbo_Quy.SelectedIndex+1;
            switch (quy)
            {
                case 1:
                    thang = 1;
                    break;
                case 2:
                    thang = 4;
                    break;
                case 3:
                    thang = 7;
                    break;
                case 4:
                    thang = 10;
                    break;
            }
            if (thang > 0)
            {
                lupNgaytu.DateTime = GetFirstDayOfMonth(thang,Convert.ToInt32(txtNam.Text));
                lupNgayden.DateTime = GetLastDayOfMonth(thang + 2,Convert.ToInt32(txtNam.Text));
            }

        }

        private void lupNgayden_EditValueChanged(object sender, EventArgs e)
        {
            txtNam.Text = lupNgayden.DateTime.Year.ToString();
        }
    }
}