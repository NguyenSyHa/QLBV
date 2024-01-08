using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_SoThuChiTU_BG : DevExpress.XtraReports.UI.XtraReport
    {

        public Rep_SoThuChiTU_BG()
        {
            InitializeComponent();

        }
        public void BindingData()
        {
            ton = Convert.ToDouble(this.TonDK.Value);
            colNTN.DataBindings.Add("Text", DataSource, "NgayThu").FormatString = "{0: dd/MM}";
            lblNgayThu.DataBindings.Add("Text", DataSource, "NgayThu").FormatString = "{0: dd/MM}";
            colTenBN.DataBindings.Add("Text", DataSource, "TenBN");
            colDChi.DataBindings.Add("Text", DataSource, "DChi");
            colKhoa.DataBindings.Add("Text", DataSource, "TenKP");
            //colMSThu.DataBindings.Add("Text", DataSource, "MSTHU");
            //colMSChi.DataBindings.Add("Text", DataSource, "MSCHI");
            colTienThu.DataBindings.Add("Text", DataSource, "TienThu").FormatString = DungChung.Bien.FormatString[1];
            colTienChi.DataBindings.Add("Text", DataSource, "TienChi").FormatString = DungChung.Bien.FormatString[1];
            colTon.DataBindings.Add("Text", DataSource, "Ton").FormatString = DungChung.Bien.FormatString[1];
            colTienThugf.DataBindings.Add("Text", DataSource, "TienThu").FormatString = DungChung.Bien.FormatString[1];
            colTienChigf.DataBindings.Add("Text", DataSource, "TienChi").FormatString = DungChung.Bien.FormatString[1];
            colTongf.DataBindings.Add("Text", DataSource, "Ton").FormatString = DungChung.Bien.FormatString[1];
            colTienThurf.DataBindings.Add("Text", DataSource, "TienThu").FormatString = DungChung.Bien.FormatString[1];
            colTienChirf.DataBindings.Add("Text", DataSource, "TienChi").FormatString = DungChung.Bien.FormatString[1];
            GroupHeader1.GroupFields.Add(new GroupField("NgayThu"));
        }

        private void GroupHeader1_BeforePrint(object sender, CancelEventArgs e)
        {

        }

        double ton;
        string ngay = "";
        bool htNgay = true;

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            if (this.GetCurrentColumnValue("MSThu") != null)
            {
                string mst = this.GetCurrentColumnValue("MSThu").ToString().Trim();
                if (mst == "0")
                    colMSThu.Text = "";
                else
                    colMSThu.Text = mst;
            }
            if (this.GetCurrentColumnValue("MSChi") != null)
            {
                string msc = this.GetCurrentColumnValue("MSChi").ToString().Trim();
                if (msc == "0")
                    colMSChi.Text = "";
                else
                    colMSChi.Text = msc;
            }

            //if (this.GetCurrentColumnValue("NgayThu") != null)
            //{
            //    string date = this.GetCurrentColumnValue("NgayThu").ToString();
            //    if (date != ngay)
            //        colNgay.Text = date.Substring(0, 4);
            //    else
            //        colNgay.Text = "";
            //    ngay = date;
            //}


            if (this.GetCurrentColumnValue("NgayThu") != null)
            {
                if (htNgay)
                {
                    string date = Convert.ToDateTime( this.GetCurrentColumnValue("NgayThu")).ToString("dd/MM");
                    colNgay.Text = date;
                    htNgay = false;
                }
                else
                {
                    colNgay.Text = "";
                }

            }
            else
            {
                colNgay.Text = "";
            }


        }
        double thu = 0, chi = 0;
        private void colTongf_BeforePrint(object sender, CancelEventArgs e)
        {
            ton = thu - chi + ton;
            colTongf.Text = ton.ToString("#,##.00");
            thu = 0;
            chi = 0;
        }

        private void colTienThu_AfterPrint(object sender, EventArgs e)
        {
            if (this.GetCurrentColumnValue("TienThu") != null)
            {
                thu += double.Parse(this.GetCurrentColumnValue("TienThu").ToString().Trim());
            }
        }

        private void colTienChi_AfterPrint(object sender, EventArgs e)
        {
            if (this.GetCurrentColumnValue("TienChi") != null)
            {
                chi += double.Parse(this.GetCurrentColumnValue("TienChi").ToString().Trim());
            }
        }

        private void colTonrf_BeforePrint(object sender, CancelEventArgs e)
        {
            colTonrf.Text = ton.ToString("#,##.00");
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            XX.Text = DungChung.Bien.TenCQ.ToUpper();
        }

        private void colNguoiLapBieu_BeforePrint(object sender, CancelEventArgs e)
        {
            colNguoiLapBieu.Text = DungChung.Bien.NguoiLapBieu;
        }

        private void colNTN_BeforePrint(object sender, CancelEventArgs e)
        {
            //if (this.GetCurrentColumnValue("NgayThu") != null)
            //{
            //    string date = this.GetCurrentColumnValue("NgayThu").ToString();
            //    colNTN.Text = date.Substring(0, 5);
            //}           
         
        }

        private void GroupFooter1_BeforePrint(object sender, CancelEventArgs e)
        {
            htNgay = true;
        }








    }
}
