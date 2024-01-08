using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class rep_mauxetnghiemdom : DevExpress.XtraReports.UI.XtraReport
    {

        public rep_mauxetnghiemdom()
        {
            InitializeComponent();
            if (DungChung.Bien.MaBV == "27777")
            {
                picLogo.Image = DungChung.Ham.GetLogo();
            }
        }

        public void BindingData()
        {
            celNgayNhan.DataBindings.Add("Text", DataSource, "Ngaythang");
            colMauDom.DataBindings.Add("Text", DataSource, "Stt");
            celTenDV.DataBindings.Add("Text", DataSource, "TenDV");

            ketquaxn1.DataBindings.Add("Text", DataSource, "Kqxn1");
            ketquaxn2.DataBindings.Add("Text", DataSource, "Kqxn2");
            ketquaxn3.DataBindings.Add("Text", DataSource, "Kqxn3");
            ketquaxn4.DataBindings.Add("Text", DataSource, "Kqxn4");
            ketquaxn5.DataBindings.Add("Text", DataSource, "Kqxn5");
           
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "27777")
            {
                picLogo.Image = DungChung.Ham.GetLogo();
            }
        }
    }
}
