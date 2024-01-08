using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_PhieuChamSoc : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_PhieuChamSoc()
        {
            InitializeComponent();
            if (DungChung.Bien.MaBV == "30007" || DungChung.Bien.MaBV == "14018" || DungChung.Bien.MaBV == "14017")
            {
                SubBand1.Visible = true;
                SubBand3.Visible = true;
            }
            else
                ReportHeader.Visible = true;
            if (DungChung.Bien.MaBV == "24012")
            {
                SubBand4.Visible = false;
                lblPageNumb.Visible = true;
                lblPageNumb.Text = "1";
                xrPageInfo1.Visible = false;
                celNgay1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
                celgio1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
                celNgay11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            }
            else
            {
                lblPageNumb.Visible = false;
                SubBand4.Visible = false;
                xrPageInfo1.Visible = true;
            }
        }
        public void BindingData()
        {
            if(DungChung.Bien.MaBV=="24012")
            {
                celNgay1.DataBindings.Add("Text", DataSource, "Ngay");
                celNgay11.DataBindings.Add("Text", DataSource, "NgayNhap");
                celgio1.DataBindings.Add("Text", DataSource, "Gio");
                celDienbien1.DataBindings.Add("Text", DataSource, "DienBien1");
                celylenh1.DataBindings.Add("Text", DataSource, "YLenh");
                celTenCBTH1.DataBindings.Add("Text", DataSource, "TenCB");
            }
            else
            celNgay.DataBindings.Add("Text", DataSource, "Ngay");
            celgio.DataBindings.Add("Text", DataSource, "Gio");
            celDienbien.DataBindings.Add("Text", DataSource, "DienBien1");
            celylenh.DataBindings.Add("Text", DataSource, "YLenh");
            celTenCBTH.DataBindings.Add("Text", DataSource, "TenCB");
        }
        int dem = 0;
        int so = 0;
        int checkvisible = 1;
        int pageindex = 1;
        private void PageHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            if (dem % 2 == 0)
            {
                if (DungChung.Bien.MaBV == "14018" || DungChung.Bien.MaBV == "14017")
                {
                    SubBand1.Visible = false;
                    SubBand3.Visible = true;
                }
                else
                {
                    SubBand1.Visible = false;
                    SubBand3.Visible = false;
                }
                so++;
                xrTableCell8.Text = string.Format("Phiếu số: {0}", so.ToString("D2"));
                if (string.IsNullOrWhiteSpace(lineNam.Text))
                {
                    xrCheckBoxNam.Checked = true;
                    xrCheckBoxNu.Checked = false;
                }
                else
                {
                    xrCheckBoxNam.Checked = false;
                    xrCheckBoxNu.Checked = true;
                }
            }
            else
            {
                SubBand1.Visible = false;
                SubBand3.Visible = false;
            }
            dem++;
            if (DungChung.Bien.MaBV == "24012")
            {
                if (checkvisible % 2 == 0)
                {
                    SubBand4.Visible = false;
                }
                else
                    SubBand4.Visible = true;
                if (SubBand4.Visible == true)
                {
                    lblPageNumb.Text = pageindex.ToString();
                    pageindex++;
                }
                checkvisible++;
            }
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            txtTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            txtTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            //if (DungChung.Bien.MaBV == "30007")
            //{
            //    if (dem % 2 == 0)
            //    {
            //        SubBand1.Visible = true;
            //        SubBand2.Visible = false;
            //    }
            //    else
            //    {
            //        SubBand2.Visible = true;
            //        SubBand1.Visible = false;
            //    }
            //    dem++;
            //}
            if (DungChung.Bien.MaBV == "24012")
            {
                SubBand5.Visible = false;
                SubBand3.Visible = false;
                SubBand1.Visible = false;
                SubBand4.Visible = true;
                SubBand2.Visible = true;
                SubBand7.Visible = true;
                SubBand6.Visible = true;
                SubBand2.Visible = false;
                GroupFooter1.Visible = true;
                ReportFooter.Visible = false;
                ReportHeader.Visible = false;
            }
        }

        DateTime ngaythang;
        DateTime ngaythang1;
        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "24012")
            {
                if (celNgay11.Text != null && celNgay11.Text != "")
                {
                    ngaythang = Convert.ToDateTime(celNgay11.Text);

                    if (ngaythang1.Date == ngaythang.Date)
                    {
                        xrLine1.Visible = false;
                    }

                    else
                    {
                        xrLine1.Visible = true;
                    }
                    ngaythang1 = Convert.ToDateTime(celNgay11.Text);
                }
            }
        }
        
        private void xrLine1_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "24012")
            {
                if (celNgay11.Text != null && celNgay11.Text != "")
                {
                    ngaythang = Convert.ToDateTime(celNgay11.Text);

                    if (ngaythang1.Date == ngaythang.Date)
                    {
                        xrLine1.Visible = false;
                        celNgay1.Text = "";
                    }

                    else
                    {
                        xrLine1.Visible = true;
                        
                    }
                    ngaythang1 = ngaythang;
                }
            }
        }
    }
}
