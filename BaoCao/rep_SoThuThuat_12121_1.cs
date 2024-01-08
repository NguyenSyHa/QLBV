using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_SoThuThuat_12121_1 : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_SoThuThuat_12121_1()
        {
            InitializeComponent();
        }


        internal void BindingData()
        {
            celNgay.DataBindings.Add("Text", DataSource, "NgayThang").FormatString = "{0:dd/MM/yyyy}";
            cel3.DataBindings.Add("Text", DataSource, "TenBNhan");
            cel4.DataBindings.Add("Text", DataSource, "ChanDoan");
            cel5.DataBindings.Add("Text", DataSource, "TuoiNam");
            cel6.DataBindings.Add("Text", DataSource, "TuoiNu");
            cel7.DataBindings.Add("Text", DataSource, "BHYT");
            cel8.DataBindings.Add("Text", DataSource, "");
            cel9.DataBindings.Add("Text", DataSource, "NguoiNgheo");
            cel10.DataBindings.Add("Text", DataSource, "DtuongVP");
            cel11.DataBindings.Add("Text", DataSource, "KSK");
            cel12.DataBindings.Add("Text", DataSource, "DienCham");
            cel13.DataBindings.Add("Text", DataSource, "ThuyCham");
            cel14.DataBindings.Add("Text", DataSource, "Cuu");
            cel15.DataBindings.Add("Text", DataSource, "Giac");
            cel16.DataBindings.Add("Text", DataSource, "TaTay");
            cel17.DataBindings.Add("Text", DataSource, "TapVanDong");
            cel18.DataBindings.Add("Text", DataSource, "Xoabop");
            cel19.DataBindings.Add("Text", DataSource, "XongHoiThuoc");
           
            
            cel7T.DataBindings.Add("Text", DataSource, "BHYT");
            cel8T.DataBindings.Add("Text", DataSource, "");
            cel9T.DataBindings.Add("Text", DataSource, "NguoiNgheo");
            cel10T.DataBindings.Add("Text", DataSource, "DtuongVP");
            cel11T.DataBindings.Add("Text", DataSource, "KSK");
            cel12T.DataBindings.Add("Text", DataSource, "DienCham");
            cel13T.DataBindings.Add("Text", DataSource, "ThuyCham");
            cel14T.DataBindings.Add("Text", DataSource, "Cuu");
            cel15T.DataBindings.Add("Text", DataSource, "Giac");
            cel16T.DataBindings.Add("Text", DataSource, "TaTay");
            cel17T.DataBindings.Add("Text", DataSource, "TapVanDong");
            cel18T.DataBindings.Add("Text", DataSource, "Xoabop");
            cel19T.DataBindings.Add("Text", DataSource, "XongHoiThuoc");

            GroupHeader1.GroupFields.Add(new GroupField("NgayThang"));
            
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            lblCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            lbCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
        }
    }
}
