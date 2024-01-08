using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
using System.Collections.Generic;
namespace QLBV.BaoCao
{
    public partial class Rep_BcHoatDongKKB_26007 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_BcHoatDongKKB_26007()
        {
            InitializeComponent();
        }


        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {

        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            colTenCQ.Text = DungChung.Bien.TenCQ;
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            colNguoiLapBieu.Text = DungChung.Bien.NguoiLapBieu;
            colTTDV.Text = DungChung.Bien.GiamDoc;
        }

        public void BindingData()
        {
            celND_Gr.DataBindings.Add("Text", DataSource, "Group");
            celTS_T.DataBindings.Add("Text", DataSource, "TS");
            celBHYT_T.DataBindings.Add("Text", DataSource, "BHYT");
            celVP_T.DataBindings.Add("Text", DataSource, "VP");
            celCC_T.DataBindings.Add("Text", DataSource, "CC");
            celVV_T.DataBindings.Add("Text", DataSource, "VV");
            celCV_T.DataBindings.Add("Text", DataSource, "CV");

            celTenKhoa.DataBindings.Add("Text", DataSource, "tenkp");
            celTS.DataBindings.Add("Text", DataSource, "TS");
            celBHYT.DataBindings.Add("Text", DataSource, "BHYT");
            celVP.DataBindings.Add("Text", DataSource, "VP");
            celCC.DataBindings.Add("Text", DataSource, "CC");
            celVV.DataBindings.Add("Text", DataSource, "VV");
            celCV.DataBindings.Add("Text", DataSource, "CV");
            colBHYTNN.DataBindings.Add("Text", DataSource, "HN");

            GroupHeader1.GroupFields.Add(new GroupField("Group"));
        }

        private void colBHYTNN_BeforePrint(object sender, CancelEventArgs e)
        {

        }
    }
}
