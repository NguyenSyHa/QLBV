using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_BCChiDinhMienDich : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_BCChiDinhMienDich()
        {
            InitializeComponent();
        }


        internal void BindingData()
        {
            celNgay.DataBindings.Add("Text", DataSource, "Ngay").FormatString = "{0:dd/MM}";
            celID.DataBindings.Add("Text", DataSource, "ID");
            celTEnBN.DataBindings.Add("Text", DataSource, "TenBNhan");
            celBSCD.DataBindings.Add("Text", DataSource, "TenBS");
            cel1.DataBindings.Add("Text", DataSource, "col1").FormatString = "{0:#,#}";
            cel2.DataBindings.Add("Text", DataSource, "col2").FormatString = "{0:#,#}";
            cel3.DataBindings.Add("Text", DataSource, "col3").FormatString = "{0:#,#}";
            cel4.DataBindings.Add("Text", DataSource, "col4").FormatString = "{0:#,#}";
            cel5.DataBindings.Add("Text", DataSource, "col5").FormatString = "{0:#,#}";
            cel6.DataBindings.Add("Text", DataSource, "col6").FormatString = "{0:#,#}";
            cel7.DataBindings.Add("Text", DataSource, "col7").FormatString = "{0:#,#}";
            cel8.DataBindings.Add("Text", DataSource, "col8").FormatString = "{0:#,#}";
            cel9.DataBindings.Add("Text", DataSource, "col9").FormatString = "{0:#,#}";
            cel10.DataBindings.Add("Text", DataSource, "col10").FormatString = "{0:#,#}";
            cel11.DataBindings.Add("Text", DataSource, "col11").FormatString = "{0:#,#}";
            cel12.DataBindings.Add("Text", DataSource, "col12").FormatString = "{0:#,#}";
            cel13.DataBindings.Add("Text", DataSource, "col13").FormatString = "{0:#,#}";
            cel14.DataBindings.Add("Text", DataSource, "col14").FormatString = "{0:#,#}";
            celThanhTien.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];

            cel1T.DataBindings.Add("Text", DataSource, "col1").FormatString = "{0:#,#}";
            cel2T.DataBindings.Add("Text", DataSource, "col2").FormatString = "{0:#,#}";
            cel3T.DataBindings.Add("Text", DataSource, "col3").FormatString = "{0:#,#}";
            cel4T.DataBindings.Add("Text", DataSource, "col4").FormatString = "{0:#,#}";
            cel5T.DataBindings.Add("Text", DataSource, "col5").FormatString = "{0:#,#}";
            cel6T.DataBindings.Add("Text", DataSource, "col6").FormatString = "{0:#,#}";
            cel7T.DataBindings.Add("Text", DataSource, "col7").FormatString = "{0:#,#}";
            cel8T.DataBindings.Add("Text", DataSource, "col8").FormatString = "{0:#,#}";
            cel9T.DataBindings.Add("Text", DataSource, "col9").FormatString = "{0:#,#}";
            cel10T.DataBindings.Add("Text", DataSource, "col10").FormatString = "{0:#,#}";
            cel11T.DataBindings.Add("Text", DataSource, "col11").FormatString = "{0:#,#}";
            cel12T.DataBindings.Add("Text", DataSource, "col12").FormatString = "{0:#,#}";
            cel13T.DataBindings.Add("Text", DataSource, "col13").FormatString = "{0:#,#}";
            cel14T.DataBindings.Add("Text", DataSource, "col14").FormatString = "{0:#,#}";
            celThanhTienT.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = "{0:#,#}";

            cel1T.Summary.FormatString = "{0:#,#}";
            cel2T.Summary.FormatString = "{0:#,#}";
            cel3T.Summary.FormatString = "{0:#,#}";
            cel4T.Summary.FormatString = "{0:#,#}";
            cel5T.Summary.FormatString = "{0:#,#}";
            cel6T.Summary.FormatString = "{0:#,#}";
            cel7T.Summary.FormatString = "{0:#,#}";
            cel8T.Summary.FormatString = "{0:#,#}";
            cel9T.Summary.FormatString = "{0:#,#}";
            cel10T.Summary.FormatString = "{0:#,#}";
            cel11T.Summary.FormatString = "{0:#,#}";
            cel12T.Summary.FormatString = "{0:#,#}";
            cel13T.Summary.FormatString = "{0:#,#}";
            cel14T.Summary.FormatString = "{0:#,#}";
            celThanhTienT.Summary.FormatString = DungChung.Bien.FormatString[1];
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            celTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            celTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
        }
    }
}
