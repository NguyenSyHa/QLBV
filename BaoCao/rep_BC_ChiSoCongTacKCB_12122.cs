using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_BC_ChiSoCongTacKCB_12122 : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_BC_ChiSoCongTacKCB_12122()
        {
            InitializeComponent();
        }


        internal void Bindingdata()
        {
            celSTT.DataBindings.Add("Text", DataSource, "sttHT");
            celNoiDung.DataBindings.Add("Text", DataSource, "noidung");
            celDonvi.DataBindings.Add("Text", DataSource, "donvi");
            celPhongkham.DataBindings.Add("Text", DataSource, "Phongkham");
            cel1.DataBindings.Add("Text", DataSource, "Khoa1");
            cel2.DataBindings.Add("Text", DataSource, "Khoa2");
            cel3.DataBindings.Add("Text", DataSource, "Khoa3");
            cel4.DataBindings.Add("Text", DataSource, "Khoa4");
            cel5.DataBindings.Add("Text", DataSource, "Khoa5");
        }

        private void TopMargin_BeforePrint(object sender, CancelEventArgs e)
        {

        }
    }
}
