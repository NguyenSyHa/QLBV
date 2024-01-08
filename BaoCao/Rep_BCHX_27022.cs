using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_BCHX_27022 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_BCHX_27022()
        {
            InitializeComponent();
        }
        public void databinding()
        {
            cel_ngay.DataBindings.Add("Text", DataSource, "NgayNhap");
            cel_soct2.DataBindings.Add("Text", DataSource, "SoCT");
            cel_madv.DataBindings.Add("Text", DataSource, "MaDV");
            cel_tenthuoc.DataBindings.Add("Text", DataSource, "TenDV");
            cel_donvi.DataBindings.Add("Text", DataSource, "DonVi").FormatString = DungChung.Bien.FormatString[0];
            cel_soluong.DataBindings.Add("Text", DataSource, "SoLuongX");
            cel_dongia.DataBindings.Add("Text", DataSource, "DonGia").FormatString=DungChung.Bien.FormatString[1];
            cel_thanhtien.DataBindings.Add("Text", DataSource, "ThanhTienX").FormatString = DungChung.Bien.FormatString[1];
            cel_kp1.DataBindings.Add("Text", DataSource, "TenKP");
            GroupHeader1.GroupFields.Add(new GroupField("NgayNhap"));
            GroupHeader2.GroupFields.Add(new GroupField("SoCT"));
            cel_tongtien.DataBindings.Add("Text", DataSource, "ThanhTienX").FormatString = DungChung.Bien.FormatString[1];
            
        }
      
    }
}
