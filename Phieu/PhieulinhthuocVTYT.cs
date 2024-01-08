using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class PhieulinhthuocVTYT : DevExpress.XtraReports.UI.XtraReport
    {
        int _dongy = 0;

        public PhieulinhthuocVTYT()
        {
            InitializeComponent();
        }
        public PhieulinhthuocVTYT(int dy)
        {
            InitializeComponent();
            _dongy = dy;
            
        }
        public void BindingData()
        {
            string _fomat = DungChung.Bien.FormatString[0];
            if (DungChung.Bien.MaBV == "24009" || DungChung.Bien.MaBV == "30003")
                _fomat = "{0:00}";
            else
                _fomat = "{0:##,###.##}";
            colTenHH.DataBindings.Add("Text", DataSource, "TenDV");
            if (DungChung.Bien.MaBV == "24009" || DungChung.Bien.MaBV == "30003")
            {
                colsoluongyc.DataBindings.Add("Text", DataSource, "SoLuong").FormatString = _fomat;
            }
            else
            {
                colsoluongyc.DataBindings.Add("Text", DataSource, "SoLuong");
            }
            if (DungChung.Bien.MaBV == "24009")
                colsoluongtp.DataBindings.Add("Text", DataSource, "SoLuong").FormatString = _fomat;//.FormatString = DungChung.Bien.FormatString[0];
            if (DungChung.Bien.MaBV == "30002" || DungChung.Bien.MaBV == "30009" || DungChung.Bien.MaBV == "20001" || DungChung.Bien.MaBV == "27023")
                colsoluongtp.DataBindings.Add("Text", DataSource, "SoLuong"); //.FormatString = DungChung.Bien.FormatString[0];
            //colTsongluongG.DataBindings.Add("Text", DataSource, "SoLuong").FormatString=DungChung.Bien.FormatString[1];
            colDVT.DataBindings.Add("Text", DataSource, "DonVi");
            colTenBN.DataBindings.Add("Text", DataSource, "TenBNhan");
            colDiaChi.DataBindings.Add("Text", DataSource, "DChi");
            colTuoi.DataBindings.Add("Text", DataSource, "Tuoi");
            colTenBN1.DataBindings.Add("Text", DataSource, "TenBNhan");
            colDiaChi1.DataBindings.Add("Text", DataSource, "DChi");
            colTuoi1.DataBindings.Add("Text", DataSource, "Tuoi");
            colMaDV.DataBindings.Add("Text", DataSource, "MaTam");
            GroupHeader1.GroupFields.Add(new GroupField("MaBNhan"));

            if (DungChung.Bien.MaBV == "14017")
            {
                this.PaperKind = System.Drawing.Printing.PaperKind.A5;
                this.SubBand1.Visible = false;
                this.SubBand3.Visible = false;
                this.SubBand7.Visible = false;
                this.SubBand9.Visible = false;
                this.SubBand2.Visible = true;
                this.SubBand11.Visible = true;
                this.SubBand8.Visible = true;
                this.SubBand10.Visible = true;
                colMaDV1.DataBindings.Add("Text", DataSource, "MaTam");
                colTenHH1.DataBindings.Add("Text", DataSource, "TenDV");
                colHamLuong.DataBindings.Add("Text", DataSource, "HamLuong");
                colDVT1.DataBindings.Add("Text", DataSource, "DonVi");
                colsoluongyc1.DataBindings.Add("Text", DataSource, "SoLuong");
                colsoluongtp1.DataBindings.Add("Text", DataSource, "SoLuong");
                //txtNgayThang1.Text = "Ngày " + System.DateTime.Now.Day + " tháng " + System.DateTime.Now.Month + " năm " + System.DateTime.Now.Year;
                xrLabel25.Text = "/" + System.DateTime.Now.Year;
            }

        }
        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            txtKhoa.Text.ToUpper(); 
            txtTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            txtTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            if (_dongy == 6)
                GroupHeader1.Visible = true;
                this.SubBand5.Visible = false;
            if(DungChung.Bien.MaBV=="30010" || DungChung.Bien.MaBV=="24009")
            {
                this.SubBand4.Visible = true;
            }
            if (DungChung.Bien.MaBV == "30004")
            {
                int _soPL = 0;
                if (this.SoPL.Value != null && this.SoPL.Value.ToString() != "")
                {
                    _soPL = Convert.ToInt32(this.SoPL.Value.ToString());
                }
                QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                var gc = (from bn in _data.BenhNhans
                          join dt in _data.DThuocs on bn.MaBNhan equals dt.MaBNhan
                          join dtct in _data.DThuoccts.Where(p => p.SoPL == _soPL) on dt.IDDon equals dtct.IDDon
                          select bn.MaDTuong).ToList();
                if (gc.Count > 0)
                {
                    if (gc.Count == gc.Where(p => p == "TE").ToList().Count)
                    {
                        this.Chuthich.Value = "Trẻ em " + this.Chuthich.Value;
                    }
                }
            }
            //if (SoPL.Value != null)
            //{
            //    QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            //    int _PL = Convert.ToInt32(SoPL.Value.ToString());
            //    var No = (from DT in _data.DThuocs.Where(p => p.SoPL == _PL)
            //              join bn in _data.BenhNhans on DT.MaBNhan equals bn.MaBNhan
            //              select new { bn.NoThe }).ToList();
            //    if (No.Count > 0 && No.First().NoThe != null && No.First().NoThe.Value == true)
            //    {
            //        NoThe.Text = "Dành cho bệnh nhân nợ thẻ";
            //    }
            //    else
            //    { NoThe.Text = "";}
            //}

        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            if (Ngaythang.Value != null && Ngaythang.Value.ToString() != "")
                txtNgayThang.Text = "Ngày ..... tháng ..... năm 20...";
            //txtNgayThang.Text = "Ngày " + System.DateTime.Now.Day + " tháng " + System.DateTime.Now.Month + " năm " + System.DateTime.Now.Year;
            if (Ycu.Value.ToString() == "7" && DungChung.Bien.MaBV == "30009")
            {
                xrTable7.Visible = true;
            }
            else
            {
                xrTable7.Visible = false;

            }
            QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            string tenkp = "";
            if (Khoa.Value != null)
                tenkp = Khoa.Value.ToString();
            var kp = _data.KPhongs.Where(p => p.TenKP == tenkp).Select(p => p.PLoai).ToList();
            if (kp.Count > 0 && kp.First() == "Cận lâm sàng")
            {

                xrTableCell14.Text = "TRƯỞNG KHOA C.LÂM SÀNG";
            }
            else
            {
                xrTableCell14.Text = "TRƯỞNG KHOA LÂM SÀNG";
            }

            if (DungChung.Bien.MaBV == "20001")
            {
                xrTableCell15.Text = "NGƯỜI PHÁT";
                xrTableCell13.Text = "NGƯỜI LĨNH";
                xrTableCell14.Text = "TRƯỞNG KHOA LÂM SÀNG";
                xrTableCell12.Text = "TRƯỞNG KHOA DƯỢC";
            }
            if (DungChung.Bien.MaBV == "14017")
            {
                xrTableCell68.Visible = false;
                xrTableCell69.Visible = false;
                xrTableCell70.Visible = false;
                xrTableCell71.Visible = false;
            }
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
   
        }

        private void colTenHH1_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "14017")
            {
                colTenHH1.Text =  colTenHH1.Text+ " (" + colHamLuong.Text + ")";
            }
        }

        private void xrLabel6_BeforePrint(object sender, CancelEventArgs e)
        {
           
        }
    }

}
