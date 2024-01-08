using System;
using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class fep_phieutamthuthainguyen : DevExpress.XtraReports.UI.XtraReport
    {
        int rgPhanLoai;
        public fep_phieutamthuthainguyen()
        {
            InitializeComponent();
        }
        public fep_phieutamthuthainguyen(int _rgPhanLoai)
        {
            InitializeComponent();
            rgPhanLoai = _rgPhanLoai;
            if (DungChung.Bien.MaBV == "24012" && rgPhanLoai == 2)
            {
                xrLabel1.Text = "BẢNG KÊ - TẠM THU PHÒNG NHU CẦU";
                xrTableCell50.Text = "TẠM THU PHÒNG NHU CẦU";
            }
        }
        public string tungays1;
        public string demngays2;
        public void hamloatbaocaotamthu()
        {
            tungay.Text = tungays1;
            denngay.Text = demngays2;
            CQCQ.Text = DungChung.Bien.TenCQCQ;
            CQ.Text = DungChung.Bien.TenCQ;
            MABNHAN.DataBindings.Add("Text", DataSource, "MaBNhan");
            TENBENHNHAN.DataBindings.Add("Text", DataSource, "TenBNhan");
            SQHDTT.DataBindings.Add("Text", DataSource, "QuyenHD");
            SOBL.DataBindings.Add("Text", DataSource, "SoHD");
            SQHDT.DataBindings.Add("Text", DataSource, "soquyen");
            SQBLT.DataBindings.Add("Text", DataSource, "sohoadon");
            sotienThanhToan.DataBindings.Add("Text", DataSource, "sotien1").FormatString = DungChung.Bien.FormatString[1];
            sotienThanhToan_G1.DataBindings.Add("Text", DataSource, "sotien1");
            sotienThanhToan_G2.DataBindings.Add("Text", DataSource, "sotien1");
            sotienTamUng_G1.DataBindings.Add("Text", DataSource, "SoTien").FormatString = DungChung.Bien.FormatString[1];
            sotienTamUng_G2.DataBindings.Add("Text", DataSource, "SoTien").FormatString = DungChung.Bien.FormatString[1];
            sotienTamUng.DataBindings.Add("Text", DataSource, "SoTien").FormatString = DungChung.Bien.FormatString[1];
            lblKhoaPhongRaVien.DataBindings.Add("Text", DataSource, "TenKP");//
            sotienTamUng_G1.Summary.FormatString = DungChung.Bien.FormatString[1];
            sotienTamUng_G2.Summary.FormatString = DungChung.Bien.FormatString[1];
            sotienThanhToan_G1.Summary.FormatString = DungChung.Bien.FormatString[1];
            sotienThanhToan_G2.Summary.FormatString = DungChung.Bien.FormatString[1];
            THU_G1.Summary.FormatString = DungChung.Bien.FormatString[1];
            THU_G2.Summary.FormatString = DungChung.Bien.FormatString[1];
            CHI_G1.Summary.FormatString = DungChung.Bien.FormatString[1];
            CHI_G2.Summary.FormatString = DungChung.Bien.FormatString[1];

            if (DungChung.Bien.MaBV == "30005")
            {
                THU.DataBindings.Add("Text", DataSource, "thu").FormatString = DungChung.Bien.FormatString[1];
                THU_G1.DataBindings.Add("Text", DataSource, "thu");
                THU_G2.DataBindings.Add("Text", DataSource, "thu");
            }
            else
            {
                THU.DataBindings.Add("Text", DataSource, "sotien1").FormatString = DungChung.Bien.FormatString[1];
                THU_G1.DataBindings.Add("Text", DataSource, "sotien1");
                THU_G2.DataBindings.Add("Text", DataSource, "sotien1");
            }
            CHI.DataBindings.Add("Text", DataSource, "chi").FormatString = DungChung.Bien.FormatString[1];
            CHI_G1.DataBindings.Add("Text", DataSource, "chi");
            CHI_G2.DataBindings.Add("Text", DataSource, "chi");
            TON.DataBindings.Add("Text", DataSource, "");

            NGAY.DataBindings.Add("Text", DataSource, "ngaythu").FormatString = "{0:dd/MM/yyyy}";
            GroupHeader1.GroupFields.Add(new GroupField("ngaythu"));


            GroupHeader1.Visible = false;
        }
    }
}
