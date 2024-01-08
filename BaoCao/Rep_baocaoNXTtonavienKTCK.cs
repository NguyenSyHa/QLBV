using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_baocaoNXTtonavienKTCK : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_baocaoNXTtonavienKTCK()
        {
            InitializeComponent();
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {

        }
        public string tungaydenngay;
        public void haminbaocao() 
        {
            TNDN.Text = tungaydenngay;
            CQ.Text = DungChung.Bien.TenCQ;
            CQCQ.Text = DungChung.Bien.TenCQCQ;
            TenDV.DataBindings.Add("Text", DataSource, "TenDV");
            donvi.DataBindings.Add("Text", DataSource, "DonVi");
            DonGia.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[1];
            soluong1.DataBindings.Add("Text", DataSource, "tondky").FormatString = DungChung.Bien.FormatString[0];
            thanhtien1.DataBindings.Add("Text", DataSource, "ttdk").FormatString = DungChung.Bien.FormatString[1];
            nhaptrongky.DataBindings.Add("Text", DataSource, "nhaptrongky1").FormatString = DungChung.Bien.FormatString[0];
            thanhtiennhaptrongky.DataBindings.Add("Text", DataSource, "thangtiennhaptrongky1").FormatString = DungChung.Bien.FormatString[1];
            nhapchuyenkho.DataBindings.Add("Text", DataSource, "NCK").FormatString = DungChung.Bien.FormatString[0];
            thanhtiennhapchuyenkho.DataBindings.Add("Text", DataSource, "thanhtiennhapchuyenkho").FormatString = DungChung.Bien.FormatString[1];
            xuattrongky.DataBindings.Add("Text", DataSource, "xuattrongky1").FormatString = DungChung.Bien.FormatString[0];
            thanhtienxuattrongky.DataBindings.Add("Text", DataSource,"thanhtienxuattrongky1").FormatString = DungChung.Bien.FormatString[1];
            xuatchuyenkho.DataBindings.Add("Text", DataSource, "xuatchuyenkhoa").FormatString = DungChung.Bien.FormatString[0];
            thanhtienxuatchuyenkho.DataBindings.Add("Text", DataSource, "thanhtienxuatchuyenkho").FormatString = DungChung.Bien.FormatString[1];
            toncuoiky.DataBindings.Add("Text", DataSource, "soltonck").FormatString = DungChung.Bien.FormatString[0];
            thanhtiencuoiky.DataBindings.Add("Text", DataSource, "ttck ").FormatString = DungChung.Bien.FormatString[1];
            GroupHeader1.GroupFields.Add(new GroupField("TenNhom"));
            tennhom.DataBindings.Add("Text", DataSource, "TenNhom");
            sumtoncuoiky.DataBindings.Add("Text", DataSource, "soltonck").FormatString = DungChung.Bien.FormatString[0];
            sumthanhtiencuoiky.DataBindings.Add("Text", DataSource, "ttck").FormatString = DungChung.Bien.FormatString[1];

        }

    }
}
