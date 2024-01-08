using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_KSKLaiXeKhongDat_Mau2_24009 : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_KSKLaiXeKhongDat_Mau2_24009()
        {
            InitializeComponent();
        }


        internal void databinding()
        {
            CEL_hOtEN.DataBindings.Add("Text", DataSource, "tenBN");
            celNamsinh.DataBindings.Add("Text", DataSource, "Nsinh");
            celDiaChi.DataBindings.Add("Text", DataSource, "Dchi");
            celNgayKham.DataBindings.Add("Text", DataSource, "NgayKham").FormatString = "{0:dd/MM/yyyy}";

           
           
        }
    }
}
