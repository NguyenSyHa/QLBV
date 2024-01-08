using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_BCSudungthuockhoaDT : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_BCSudungthuockhoaDT()
        {
            InitializeComponent();
        }
        public void BindingData()
        {
            //colTenThuocGh1.DataBindings.Add("Text", DataSource, "TenNhomThuoc");
            colTenThuoc.DataBindings.Add("Text", DataSource, "TenThuoc");
            colDonGia.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[1];
            //colHamLuong.DataBindings.Add("Text", DataSource, "HamLuong");
            //colSoDK.DataBindings.Add("Text", DataSource, "SoDK");
            colDonVi.DataBindings.Add("Text", DataSource, "DonVi");
            ColSoLuong.DataBindings.Add("Text", DataSource, "SoLuong").FormatString = DungChung.Bien.FormatString[0];
            ////ColSoLuongGp1.DataBindings.Add("Text", DataSource, "SoLuong").FormatString = DungChung.Bien.FormatString[1];
            //colThanhTienGp1.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];
            colThanhTien.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];
            colThanhTienTong.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];
          //  GroupHeader1.GroupFields.Add(new GroupField("TenNhomThuoc"));
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            txtTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            lbTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
        }

    }
}
