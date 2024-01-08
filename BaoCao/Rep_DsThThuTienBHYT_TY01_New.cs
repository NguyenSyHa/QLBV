using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_DsThThuTienBHYT_TY01_New : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_DsThThuTienBHYT_TY01_New()
        {
            InitializeComponent();
        }
        public void BindingData()
        {

            colNTN.DataBindings.Add("Text", DataSource, "NgayTT").FormatString = "{0:dd/MM}"; ;
            colTenBNhan.DataBindings.Add("Text", DataSource, "TenBNhan");
            colDiaChi.DataBindings.Add("Text", DataSource, "DChi");
            colTuoi.DataBindings.Add("Text", DataSource, "Tuoi");

            colTongSoTien.DataBindings.Add("Text", DataSource, "TongTien").FormatString = DungChung.Bien.FormatString[1];
            col20.DataBindings.Add("Text", DataSource, "Tien20").FormatString = DungChung.Bien.FormatString[1];
            col5.DataBindings.Add("Text", DataSource, "Tien5").FormatString = DungChung.Bien.FormatString[1];
            col44.DataBindings.Add("Text", DataSource, "Tien44").FormatString = DungChung.Bien.FormatString[1];
            col33.DataBindings.Add("Text", DataSource, "Tien33").FormatString = DungChung.Bien.FormatString[1];
            col30.DataBindings.Add("Text", DataSource, "Tien30").FormatString = DungChung.Bien.FormatString[1];
            colCpNgoaiDM.DataBindings.Add("Text", DataSource, "NgoaiDM").FormatString = DungChung.Bien.FormatString[1];
            colSoTienNop.DataBindings.Add("Text", DataSource, "SoTienNop").FormatString = DungChung.Bien.FormatString[1];

            colTongSoTienT.DataBindings.Add("Text", DataSource, "TongTien").FormatString = DungChung.Bien.FormatString[1];
            col20T.DataBindings.Add("Text", DataSource, "Tien20").FormatString = DungChung.Bien.FormatString[1];
            col5T.DataBindings.Add("Text", DataSource, "Tien5").FormatString = DungChung.Bien.FormatString[1];
            col44T.DataBindings.Add("Text", DataSource, "Tien44").FormatString = DungChung.Bien.FormatString[1];
            col33T.DataBindings.Add("Text", DataSource, "Tien33").FormatString = DungChung.Bien.FormatString[1];
            col30T.DataBindings.Add("Text", DataSource, "Tien30").FormatString = DungChung.Bien.FormatString[1];
            colTongNgoaiDM.DataBindings.Add("Text", DataSource, "NgoaiDM").FormatString = DungChung.Bien.FormatString[1];
            colSoTienNopT.DataBindings.Add("Text", DataSource, "SoTienNop").FormatString = DungChung.Bien.FormatString[1];


        }
        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            colTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            colTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            colKeToan.Text = DungChung.Bien.KeToanTruong;
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

    }
}
