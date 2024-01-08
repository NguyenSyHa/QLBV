using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class repBbKKThuoc : DevExpress.XtraReports.UI.XtraReport
    {
        private bool HTNuocSX;
        bool _Thang = true;
        public repBbKKThuoc()
        {
            InitializeComponent();
           
        }

        public repBbKKThuoc(bool HTNuocSX, bool Thang)
        {
            // TODO: Complete member initialization
            this.HTNuocSX = HTNuocSX;
            _Thang = Thang;
            InitializeComponent();
        }
        public void BindingData()
        {

            txtNT.DataBindings.Add("Text", DataSource, "NgayThang");
            txtCT.DataBindings.Add("Text", DataSource, "SoCT");
            colTenThuocGh1.DataBindings.Add("Text", DataSource, "TenTieuNhom");
            colTenThuoc.DataBindings.Add("Text", DataSource, "TenDV");
            colDonVi.DataBindings.Add("Text", DataSource, "DonVi");
            celSDK.DataBindings.Add("Text", DataSource, "SoDK");
            colSoKiemSoat.DataBindings.Add("Text", DataSource, "SoLo");
            colNuocSX.DataBindings.Add("Text", DataSource, "NuocSX");
            colHanDung.DataBindings.Add("Text", DataSource, "HanDung");//.FormatString="{0:dd/MM/yyyy}";
            colSoLuongSS.DataBindings.Add("Text", DataSource, "SoLuongSS").FormatString = DungChung.Bien.FormatString[0];
            colSoLuongTT.DataBindings.Add("Text", DataSource, "SoLuongKK").FormatString = DungChung.Bien.FormatString[0];
            //colSoLuongTTTong.DataBindings.Add("Text", DataSource, "SoLuongKK").FormatString = DungChung.Bien.FormatString[1];
            //colHongVoGh1.DataBindings.Add("Text", DataSource, "HongVo").FormatString = DungChung.Bien.FormatString[1];
            if (DungChung.Bien.MaBV != "20001")
                colHongVo.DataBindings.Add("Text", DataSource, "HongVo").FormatString = DungChung.Bien.FormatString[0];
            //colHongVoTong.DataBindings.Add("Text", DataSource, "HongVo").FormatString = DungChung.Bien.FormatString[0];
            string __tt = TT.Value.ToString();
            if (__tt == "A")
            {
                colSoLuongTT.DataBindings.Add("Text", DataSource, "SoLuongSS").FormatString = DungChung.Bien.FormatString[0];
            }
            colGhiChu.DataBindings.Add("Text", DataSource, "GhiChu");
            GroupHeader1.GroupFields.Add(new GroupField("TenTieuNhom"));
            GroupHeader2.GroupFields.Add(new GroupField("SoCT"));
            GroupHeader3.GroupFields.Add(new GroupField("NgayThang"));
            if (HTNuocSX)
            {
                lblThuocNoi.DataBindings.Add("Text", DataSource, "NuocSXGr");
                GroupHeader4.GroupFields.Add(new GroupField("NuocSXGr"));
            }
            if (DungChung.Bien.MaBV == "30009")
            {
                colTenThuoc1.DataBindings.Add("Text", DataSource, "TenDV");
                colMaNoiBo.DataBindings.Add("Text", DataSource, "MaTam");
                colDonVi1.DataBindings.Add("Text", DataSource, "DonVi");
                celSDK1.DataBindings.Add("Text", DataSource, "SoDK");
                colSoKiemSoat1.DataBindings.Add("Text", DataSource, "SoLo");
                colNuocSX1.DataBindings.Add("Text", DataSource, "NuocSX");
                colHanDung1.DataBindings.Add("Text", DataSource, "HanDung");//.FormatString="{0:dd/MM/yyyy}";
                colSoLuongSS1.DataBindings.Add("Text", DataSource, "SoLuongSS").FormatString = DungChung.Bien.FormatString[0];
                colSoLuongTT1.DataBindings.Add("Text", DataSource, "SoLuongKK").FormatString = DungChung.Bien.FormatString[0];
                colGhiChu1.DataBindings.Add("Text", DataSource, "GhiChu");
                colHongVo1.DataBindings.Add("Text", DataSource, "HongVo").FormatString = DungChung.Bien.FormatString[0];
                __tt = TT.Value.ToString();
                if (__tt == "A")
                {
                    colSoLuongTT1.DataBindings.Add("Text", DataSource, "SoLuongSS").FormatString = DungChung.Bien.FormatString[0];
                }
                colTenThuocGh2.DataBindings.Add("Text", DataSource, "TenTieuNhom");
            }

        }

        private void repBbKKThuoc_BeforePrint(object sender, CancelEventArgs e)
        {
            TenCQCQ.Value = DungChung.Bien.TenCQCQ;
            TenCQ.Value = DungChung.Bien.TenCQ;
            string _nt = NT.Value.ToString();
            string _ct = NT.Value.ToString();
            if (_nt == "1") { GroupHeader3.Visible = false; }
            else GroupHeader3.Visible = true;
            if (_ct == "1") { GroupHeader2.Visible = false; }
            else GroupHeader2.Visible = true;
            if (DungChung.Bien.MaBV == "20001")
            {
                
                colThanhVienKK1r.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                colThanhVienKK2r.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                colThanhVienKK3r.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                colThanhVienKK4r.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                colThanhVienKK5r.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                xrLabel20.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            }
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            colTenCQCQ.Text = colTenCQCQ2.Text = DungChung.Bien.TenCQCQ.ToUpper();
            colTenCQ.Text = colTenCQ2.Text = DungChung.Bien.TenCQ.ToUpper();
            xrPictureBox1.Image = DungChung.Ham.GetLogo();
            if (DungChung.Bien.MaBV == "24272")
            {
                SubBand4.Visible = false;
                SubBand5.Visible = true;
            }
            //xrRichText1.Text = TV1goi.Value.ToString();
            xrLine2.Visible = DungChung.Bien.MaBV == "20001" ? true : false;
            if (DungChung.Bien.MaBV == "30009")
            {
                xrTable2.Visible = false;
                xrPanel1.Visible = true;
                xrTable8.Visible = false;
                xrTable20.Visible = true;
            }
        }
        private void GroupHeader3_BeforePrint(object sender, CancelEventArgs e)
        {
            if (this.GetCurrentColumnValue("NgayThang") != null)
            {
                string nt = this.GetCurrentColumnValue("NgayThang").ToString();
                colNgayThang.Text = "Ngày " + nt.ToString().Substring(0, 2) + " tháng " + nt.ToString().Substring(3, 2) + " năm " + nt.ToString().Substring(6, 4);
            }
        }

        private void GroupHeader2_BeforePrint(object sender, CancelEventArgs e)
        {
            if (this.GetCurrentColumnValue("SoCT") != null)
            {
                string ct = this.GetCurrentColumnValue("SoCT").ToString();
                colSoCT.Text = "Số chứng từ: " + ct;
            }
        }

        private void colHanDung_BeforePrint(object sender, CancelEventArgs e)
        {
            //if (this.GetCurrentColumnValue("HanDung") != null)
            //{
            //    string hd = this.GetCurrentColumnValue("HanDung").ToString();
            //    colHanDung.Text = hd.ToString().Substring(0,10);
            //}
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "30009")
            {
                xrTable3.Visible = false;
            } 
            else
            {
                xrTable22.Visible = false;
            }
        }

        private void GroupHeader4_BeforePrint(object sender, CancelEventArgs e)
        {
            if (HTNuocSX)
            {
                GroupHeader4.Visible = true;
                GroupFooter4.Visible = true;
                if (this.GetCurrentColumnValue("NuocSXGr") != null)
                {
                    string nguongoc = this.GetCurrentColumnValue("NuocSXGr").ToString();
                    if (nguongoc == "1")
                        celNuocSXGr.Text = "Thuốc nội";
                    else if (nguongoc == "2")
                        celNuocSXGr.Text = "Thuốc ngoại";
                }
            }
            else
            {
                GroupHeader4.Visible = false;
                GroupFooter4.Visible = false;
            }

        }

        private void PageFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "27001")
            {
                if (_Thang == true)
                {
                    SubBand3.Visible = true;
                    SubBand2.Visible = false;
                    SubBand1.Visible = false;
                }
                else
                {
                    SubBand2.Visible = true;
                    SubBand3.Visible = false;
                    SubBand1.Visible = false;
                }
                xrLabel41.Visible = true;
                xrLabel45.Visible = true;
                xrLabel46.Visible = true;
                xrLabel43.Visible = true;
                xrLabel69.Visible = true;
                xrLabel51.Visible = true;
                xrLabel50.Visible = true;
                xrLabel47.Visible = true;
            }
            else
            {
                SubBand1.Visible = true;
                SubBand2.Visible = false;
                SubBand3.Visible = false;
            }
        }

        private void GroupHeader1_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "30009")
            {
                xrTable21.Visible = true;
                xrTable20.Visible = true;
                xrTable7.Visible = false;
            }
         }
    }
}
