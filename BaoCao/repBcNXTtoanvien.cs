using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class repBcNXTtoanvien : DevExpress.XtraReports.UI.XtraReport
    {
        public repBcNXTtoanvien()
        {
            InitializeComponent();
        }
        public string tungaydenngay; 
        public void baocaonxttv()
        {
            TNDN.Text = tungaydenngay;
            CQ.Text = DungChung.Bien.TenCQ;
            CQCQ.Text = DungChung.Bien.TenCQCQ;
            TenDV.DataBindings.Add("Text",DataSource,"TenDV");
            donvi.DataBindings.Add("Text", DataSource, "DonVi");
            DonGia.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[1];
            soluong1.DataBindings.Add("Text", DataSource, "tondky").FormatString = DungChung.Bien.FormatString[0];
            thanhtien1.DataBindings.Add("Text", DataSource, "ttdk").FormatString = DungChung.Bien.FormatString[1];
            soluong2.DataBindings.Add("Text", DataSource, "nhaptk").FormatString = DungChung.Bien.FormatString[0];
            thanhtien2.DataBindings.Add("Text", DataSource, "ttntk").FormatString = DungChung.Bien.FormatString[1];
            soluong3.DataBindings.Add("Text", DataSource, "xuattk").FormatString = DungChung.Bien.FormatString[0];
            thanhtien3.DataBindings.Add("Text", DataSource, "ttxtk").FormatString = DungChung.Bien.FormatString[1];
            soluong4.DataBindings.Add("Text", DataSource, "soltonck").FormatString = DungChung.Bien.FormatString[0];
            thanhtien4.DataBindings.Add("Text", DataSource, "ttck").FormatString = DungChung.Bien.FormatString[1];
            GroupHeader1.GroupFields.Add(new GroupField("TenNhom"));
            tennhom.DataBindings.Add("Text", DataSource, "TenNhom");
            //tongsoluong1.DataBindings.Add("Text", DataSource, "tondky").FormatString = DungChung.Bien.FormatString[0];
            tongthanhtien1.DataBindings.Add("Text", DataSource, "ttdk").FormatString = DungChung.Bien.FormatString[1];
            //tongsoluong2.DataBindings.Add("Text", DataSource, "nhaptk").FormatString = DungChung.Bien.FormatString[0];
            tongthanhtien2.DataBindings.Add("Text", DataSource, "ttntk").FormatString = DungChung.Bien.FormatString[1];
            //tongsoluong3.DataBindings.Add("Text", DataSource, "xuattk").FormatString = DungChung.Bien.FormatString[0];
            tongthanhtien3.DataBindings.Add("Text", DataSource,"ttxtk").FormatString = DungChung.Bien.FormatString[1];
            //tongsoluong4.DataBindings.Add("Text", DataSource, "soltonck").FormatString = DungChung.Bien.FormatString[0];
            Tongthanhtien4.DataBindings.Add("Text", DataSource, "ttck").FormatString = DungChung.Bien.FormatString[1];
        }

 
    }
}
