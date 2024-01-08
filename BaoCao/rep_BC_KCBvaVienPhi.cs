using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_BC_KCBvaVienPhi : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_BC_KCBvaVienPhi()
        {
            InitializeComponent();
        }
        public void databinding()
        {
            celkhoa.DataBindings.Add("Text", DataSource, "TenKP");
            celtsorv.DataBindings.Add("Text", DataSource, "SOBN").FormatString = ("{##,###}");
            celtsongaydt.DataBindings.Add("Text", DataSource, "SONGAY").FormatString = ("{0:##,###}");
            celtsotienvp.DataBindings.Add("Text", DataSource, "TONGTIEN").FormatString = DungChung.Bien.FormatString[1];
            celcptb.DataBindings.Add("Text", DataSource, "CHIPHITB").FormatString = DungChung.Bien.FormatString[1];
            celngaydttb.DataBindings.Add("Text", DataSource, "SONGAYTB").FormatString = ("{0:##,###}");
            celtongbnbv.DataBindings.Add("Text", DataSource, "SOBN").FormatString = ("{0:##,###}");
            celtongngaybv.DataBindings.Add("Text", DataSource, "SONGAY").FormatString = ("{0:##,###}");
            celtongvpbv.DataBindings.Add("Text", DataSource, "TONGTIEN").FormatString = DungChung.Bien.FormatString[1];
        }
    }
}
