using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
using System.Collections.Generic;
namespace QLBV.BaoCao
{
    public partial class Rep_BcHoatDongKKB_TH01_30010 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_BcHoatDongKKB_TH01_30010()
        {
            InitializeComponent();
        }
        public void BindingData()
        {
            celTenCK.DataBindings.Add("Text", DataSource, "ChuyenKhoa");
            colTenKP.DataBindings.Add("Text", DataSource, "kphong");
            col1.DataBindings.Add("Text", DataSource, "TS");
            col2.DataBindings.Add("Text", DataSource, "Nu");
            col3.DataBindings.Add("Text", DataSource, "BH");
            col4.DataBindings.Add("Text", DataSource, "YHCT");
            col5.DataBindings.Add("Text", DataSource, "TE");
            col6.DataBindings.Add("Text", DataSource, "CC");
            col7.DataBindings.Add("Text", DataSource, "VV");
            col8.DataBindings.Add("Text", DataSource, "CV");
            col9.DataBindings.Add("Text", DataSource, "TSDTri");
            col10.DataBindings.Add("Text", DataSource, "SoNgay");
            colte6.DataBindings.Add("Text", DataSource, "TE6");


            cel1_T.DataBindings.Add("Text", DataSource, "TS");
            cel2_T.DataBindings.Add("Text", DataSource, "Nu");
            cel3_T.DataBindings.Add("Text", DataSource, "BH");
            cel4_T.DataBindings.Add("Text", DataSource, "YHCT");
            cel5_T.DataBindings.Add("Text", DataSource, "TE");
            cel6_T.DataBindings.Add("Text", DataSource, "CC");
            cel7_T.DataBindings.Add("Text", DataSource, "VV");
            cel8_T.DataBindings.Add("Text", DataSource, "CV");
            cel9_T.DataBindings.Add("Text", DataSource, "TSDTri");
            cel10_T.DataBindings.Add("Text", DataSource, "SoNgay");
            col_te6.DataBindings.Add("Text", DataSource, "TE6");

            celT1.DataBindings.Add("Text", DataSource, "TS");
            celT2.DataBindings.Add("Text", DataSource, "Nu");
            celT3.DataBindings.Add("Text", DataSource, "BH");
            celT4.DataBindings.Add("Text", DataSource, "YHCT");
            celT5.DataBindings.Add("Text", DataSource, "TE");
            celT6.DataBindings.Add("Text", DataSource, "CC");
            celT7.DataBindings.Add("Text", DataSource, "VV");
            celT8.DataBindings.Add("Text", DataSource, "CV");
            celT9.DataBindings.Add("Text", DataSource, "TSDTri");
            celT10.DataBindings.Add("Text", DataSource, "SoNgay");
            celTTE.DataBindings.Add("Text", DataSource, "TE6");

            GroupHeader1.GroupFields.Add(new GroupField("ChuyenKhoa"));
           
        }
        
        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            string tenCK = "";
            count++;
            if (this.GetCurrentColumnValue("ChuyenKhoa") != null)
            {
                tenCK = this.GetCurrentColumnValue("ChuyenKhoa").ToString().ToLower();
                if (tenCK.Contains("liên")) // nếu là liên chuyên khoa thì hiển thị các khoa chi tiết
                    xrTableRow2.Visible = true;
                else
                    xrTableRow2.Visible = false;
            }
            celSTT.Text = countG.ToString() + "." + count.ToString();
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
              colTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
              colTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            colNguoiLapBieu.Text = DungChung.Bien.NguoiLapBieu;
            colTTDV.Text = DungChung.Bien.GiamDoc;
        }

    

        private void colBHYTNN_BeforePrint(object sender, CancelEventArgs e)
        {
        
        }
        private int countG = 0;
        private int count = 0;
        private void GroupHeader1_BeforePrint(object sender, CancelEventArgs e)
        {
            countG++;
            count = 0;
            celSTT_G.Text = countG.ToString();
        }
        

    }
}
