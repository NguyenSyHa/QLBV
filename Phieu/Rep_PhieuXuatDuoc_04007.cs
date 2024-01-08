using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.Phieu
{
    public partial class Rep_PhieuXuatDuoc_04007 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_PhieuXuatDuoc_04007()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        public int Formatdate;
        public void BindingData1() // Đối với phiếu xuất dượ 04007
        {
        
            xrLabel23.Text = DungChung.Ham.NgaySangChu(System.DateTime.Now,1);
            colTenSP.DataBindings.Add("Text", DataSource, "TenDV");
            colSoLo.DataBindings.Add("Text", DataSource, "SoLo");
            colDonViTinh.DataBindings.Add("Text", DataSource, "DonViN");
            colSoDangKy.DataBindings.Add("Text", DataSource, "SoDK");
            colHanDung.DataBindings.Add("Text", DataSource, "HanDung").FormatString = "{0: dd/MM/yyyy}";
            colYeuCau.DataBindings.Add("Text", DataSource, "SoLuongX").FormatString = "{0: #,##}"; ;
            colDonGia.DataBindings.Add("Text", DataSource, "DonGia").FormatString = "{0: #,##}"; ;
            ColThucXuat.DataBindings.Add("Text", DataSource, "SoLuongX").FormatString = "{0: #,##}"; ;
            colThanhTien.DataBindings.Add("Text", DataSource, "ThanhTienX").FormatString = "{0: #,##}";

            colTenSP1.DataBindings.Add("Text", DataSource, "TenDV");
            colDonViTinh1.DataBindings.Add("Text", DataSource, "DonViN");
            colSoDangKy1.DataBindings.Add("Text", DataSource, "MaTam");
            colYeuCau1.DataBindings.Add("Text", DataSource, "SoLuongX").FormatString = "{0: #,##}"; ;
            colDonGia1.DataBindings.Add("Text", DataSource, "DonGia").FormatString = "{0: #,##}"; ;
            ColThucXuat1.DataBindings.Add("Text", DataSource, "SoLuongX").FormatString = "{0: #,##}"; ;
            colThanhTien1.DataBindings.Add("Text", DataSource, "ThanhTienX").FormatString = "{0: #,##}";
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "24012")
            {
                SubBand1.Visible = false;
                SubBand2.Visible = true;
            }
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "24012")
            {
                xrTable7.Visible = false;
                xrTable8.Visible = true;
                xrTableCell117.Text = "Người nhận hàng";
                xrTableCell123.Text = "(Hoặc người phụ trách bộ phận)\n(Ký, họ tên)";
            }
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "24012")
            {
                xrTable2.Visible = false;
                xrTable5.Visible = true;
            }
        }
    }
}
