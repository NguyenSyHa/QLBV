using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_SoVaoVien_ys : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_SoVaoVien_ys()
        {
            InitializeComponent();
        }
        public void BindingData()
        {
            colTenBN.DataBindings.Add("Text", DataSource, "TenBN");
            colGT.DataBindings.Add("Text", DataSource, "gt");
            colTuoi.DataBindings.Add("Text", DataSource, "Tuoi");
            colDiachi.DataBindings.Add("Text", DataSource, "Diachi");
            colSothe.DataBindings.Add("Text", DataSource, "Sothe");
            colNoiGT.DataBindings.Add("Text", DataSource, "NoiGT");
            colCDTuyenduoi.DataBindings.Add("Text", DataSource, "CDNoiGT");
            colCDKKB.DataBindings.Add("Text", DataSource, "CD");
            colPA.DataBindings.Add("Text", DataSource, "Phuongan");
            colBS.DataBindings.Add("Text", DataSource, "TenBS");
            colDT.DataBindings.Add("Text", DataSource, "DTuong");
            TXTCT.DataBindings.Add("Text", DataSource, "ct");
            colNgayVao.DataBindings.Add("Text", DataSource, "NgayVao").FormatString="{0:dd/MM}";
            colNgayRa.DataBindings.Add("Text", DataSource, "NgayRa").FormatString = "{0:dd/MM}";
            colSoNgayDT.DataBindings.Add("Text", DataSource, "SoNgayDT");
            colNhomTuoi_GH1.DataBindings.Add("Text", DataSource, "NhomTuoi");
            colMaBN_gh1.DataBindings.Add("Text", DataSource, "TenBN");
            GroupHeader1.GroupFields.Add(new GroupField("NhomTuoi"));
        }

        private void colNam_BeforePrint(object sender, CancelEventArgs e)
        {
            if (this.GetCurrentColumnValue("gt") != null)
            {
                string GT = this.GetCurrentColumnValue("gt").ToString();
                if (GT == "1")
                {
                    if (this.GetCurrentColumnValue("tuoi") != null)
                    {
                        colNam.Text = this.GetCurrentColumnValue("tuoi").ToString();
                        colNu.Text = "";
                    }
                }
                else
                {
                    if (this.GetCurrentColumnValue("tuoi") != null)
                    {
                        colNam.Text = "";
                        colNu.Text = this.GetCurrentColumnValue("tuoi").ToString();
                    }
                }
            }
        }

        private void colCDDT_BeforePrint(object sender, CancelEventArgs e)
        {
            if (this.GetCurrentColumnValue("phuongan") != null)
            {
                string PA = this.GetCurrentColumnValue("phuongan").ToString();
                switch (PA)
                {
                    case "1":
                        if (this.GetCurrentColumnValue("ct") != null)
                        {
                            int RV = Convert.ToInt32(this.GetCurrentColumnValue("ct").ToString());
                            if (RV == 2)
                            {
                                colVVien.Text = "X";
                                colNgoaitru.Text = " ";
                                colTuyentren.Text = " ";
                            }
                            else
                            {
                                colVVien.Text = " ";
                                colNgoaitru.Text = " ";
                                colTuyentren.Text = "X";
                            }
                        }
                        break;
                    case "0":
                        if (this.GetCurrentColumnValue("ct") != null)
                        {
                            int RV = Convert.ToInt32(this.GetCurrentColumnValue("ct").ToString());
                            if (RV == 2)
                            {
                                colVVien.Text = " ";
                                colNgoaitru.Text = "X";
                                colTuyentren.Text = " ";
                            }
                            else
                            {
                                colVVien.Text = " ";
                                colNgoaitru.Text = " ";
                                colTuyentren.Text = "X";
                            }
                        }
                        break;
                    //default:
                    //    colNgoaitru.Text = "";
                    //    colVVien.Text = " ";
                    //    colTuyentren.Text = "X";
                    //    break;
                }
            }
        }

        private void colBS_BeforePrint(object sender, CancelEventArgs e)
        {
            if (this.GetCurrentColumnValue("dtuong") != null)
            {
                string dt = this.GetCurrentColumnValue("dtuong").ToString();
                if (dt == "Dịch vụ")
                {
                    colThuphi.Text = "X";
                }
            }
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            txtTenBV.Text = DungChung.Bien.TenCQ.ToUpper(); ;

        }
    }
}
