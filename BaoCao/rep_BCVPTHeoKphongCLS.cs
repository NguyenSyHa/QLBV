using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_BCVPTHeoKphongCLS : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_BCVPTHeoKphongCLS()
        {
            InitializeComponent();
        }
        public void bindingdata()
        {
            grmabn.DataBindings.Add("Text", DataSource, "MaBNhan");
            grtenbn.DataBindings.Add("Text", DataSource, "TenBNhan");
            grsthe.DataBindings.Add("Text", DataSource, "SThe");
            celtendv.DataBindings.Add("Text", DataSource, "TenDV");
            celdongia.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[1];
            celthanhtien.DataBindings.Add("Text", DataSource, "DonGia").FormatString=DungChung.Bien.FormatString[1];
            celngayth.DataBindings.Add("Text", DataSource, "NgayTH").FormatString = "{0:dd/MM/yyyy}";
            GroupHeader1.GroupFields.Add(new GroupField("MaBNhan"));
            celthanhtiengr.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[1];
            celtttong.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[1];
        }

    }
}
