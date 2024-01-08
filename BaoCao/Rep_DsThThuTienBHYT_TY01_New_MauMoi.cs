using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_DsThThuTienBHYT_TY01_New_MauMoi : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_DsThThuTienBHYT_TY01_New_MauMoi()
        {
            InitializeComponent();
        }
        public void BindingData()
        {
            xrNgay.DataBindings.Add("Text", DataSource, "Ngay").FormatString = "{0:dd/MM/yyyy}"; 
            colNTN.DataBindings.Add("Text", DataSource, "NgayTT").FormatString = "{0:dd/MM}";
            clNgayDuyet.DataBindings.Add("Text", DataSource, "NgayDuyet").FormatString = "{0:dd/MM}"; 
            colTenBNhan.DataBindings.Add("Text", DataSource, "TenBNhan");
            colDiaChi.DataBindings.Add("Text", DataSource, "DChi");
            colTuoi.DataBindings.Add("Text", DataSource, "Tuoi");
            colMucThu.DataBindings.Add("Text", DataSource, "MucThu");

            colTongSoTien.DataBindings.Add("Text", DataSource, "TongTien").FormatString = DungChung.Bien.FormatString[1];
            colSoTienNop.DataBindings.Add("Text", DataSource, "SoTienNop").FormatString = DungChung.Bien.FormatString[1];

            colTongSoTienT.DataBindings.Add("Text", DataSource, "TongTien").FormatString = DungChung.Bien.FormatString[1];
            colSoTienNopT.DataBindings.Add("Text", DataSource, "SoTienNop").FormatString = DungChung.Bien.FormatString[1];

            colgrTongtien.DataBindings.Add("Text", DataSource, "TongTien");
            colgrNopTongtien.DataBindings.Add("Text", DataSource, "SoTienNop");

            GroupField grField = new GroupField("Ngay");
            GroupHeader1.GroupFields.Add(grField);

        }
        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            colTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            colTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            colGiamDoc.Text = DungChung.Bien.GiamDoc;
            if (this.GetCurrentColumnValue("TongTien") != null || this.GetCurrentColumnValue("TongTien") != "")
            {
                //if(colSoTienNopT.Text!=null || colSoTienNopT.Text!="")
                //{
                double st = Convert.ToDouble(TongTien.Value);
                st = Math.Round(st, 0);
                txtsotien.Text = "Số tiền bằng chữ: " + DungChung.Ham.DocTienBangChu(st, " đồng chẵn ./.");
            }
            else { txtsotien.Text = "Số tiền bằng chữ:................................................................................."; }

        }
        double TN = 0;
        private void colSoTienNop_BeforePrint(object sender, CancelEventArgs e)
        {
            //    if (!string.IsNullOrEmpty(col20.Text)) ;
            //    {
            //        TN = TN + Convert.ToDouble(col20.Text);
            //    }
            //    if (!string.IsNullOrEmpty(col5.Text))
            //    {
            //        TN = TN + Convert.ToDouble(col5.Text);
            //    }
            //    if(!string.IsNullOrEmpty(col44.Text))
            //    {
            //        TN=TN+Convert.ToDouble(col44.Text);
            //    }
            //    if(!string.IsNullOrEmpty(col33.Text))
            //    {
            //        TN=TN+Convert.ToDouble(col33.Text);
            //     }
            //    colSoTienNop.Text = TN.ToString("0,0");
        }
        //double ST = 0;
        //double ST40 = 0;
        //double ST5 = 0;
        //double ST44 = 0;
        //double ST33 = 0;
        private void colSoTienNopT_BeforePrint(object sender, CancelEventArgs e)
        {
            // if (!string.IsNullOrEmpty(col20T.Text)) ;
            //{
            //    ST40 =  Convert.ToDouble(col20T.Text);
            //}
            //if (!string.IsNullOrEmpty(col5T.Text))
            //{
            //    ST5 =  Convert.ToDouble(col5T.Text);
            //}
            //if(!string.IsNullOrEmpty(col44T.Text))
            //{
            //    ST44 =  Convert.ToDouble(col44T.Text);
            //}
            //if(!string.IsNullOrEmpty(col33T.Text))
            //{
            //    ST33 = Convert.ToDouble(col33T.Text);
            // }
            //colSoTienNopT.Text = (ST40+ST5+ST44+ST33).ToString("0,0");
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            //var a = GetCurrentColumnValue("colMucThu");
        }

    }
}
