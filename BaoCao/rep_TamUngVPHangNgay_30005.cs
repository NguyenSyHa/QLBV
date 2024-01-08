using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_TamUngVPHangNgay_30005 : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_TamUngVPHangNgay_30005()
        {
            InitializeComponent();
        }


        internal void BindingData()
        {
            celNgay.DataBindings.Add("Text", DataSource, "NgayThu").FormatString = "{0: dd/MM/yyyy}";
            celHoTen.DataBindings.Add("Text", DataSource, "TenBNhan");
            celTuoi.DataBindings.Add("Text", DataSource, "Tuoi");
            celDiachi.DataBindings.Add("Text", DataSource, "DChi");
            celSoBL.DataBindings.Add("Text", DataSource, "SoHD");
            celKhoa.DataBindings.Add("Text", DataSource, "TenKP");
            celVP.DataBindings.Add("Text", DataSource, "vphi").FormatString = DungChung.Bien.FormatString[1];
            celBH.DataBindings.Add("Text", DataSource, "Bhiem").FormatString = DungChung.Bien.FormatString[1];
            celTong.DataBindings.Add("Text", DataSource, "Tong").FormatString = DungChung.Bien.FormatString[1];

            celVP_G.DataBindings.Add("Text", DataSource, "vphi").FormatString = DungChung.Bien.FormatString[1];
            celBH_G.DataBindings.Add("Text", DataSource, "Bhiem").FormatString = DungChung.Bien.FormatString[1];
            celTong_G.DataBindings.Add("Text", DataSource, "Tong").FormatString = DungChung.Bien.FormatString[1];


            celVP_R.DataBindings.Add("Text", DataSource, "vphi").FormatString = DungChung.Bien.FormatString[1];
            celBH_R.DataBindings.Add("Text", DataSource, "Bhiem").FormatString = DungChung.Bien.FormatString[1];
            celTong_R.DataBindings.Add("Text", DataSource, "Tong").FormatString = DungChung.Bien.FormatString[1];

            GroupHeader1.GroupFields.Add(new GroupField("NgayThu"));

        }
    }
}
