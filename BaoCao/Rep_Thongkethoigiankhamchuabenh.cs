using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_Thongkethoigiankhamchuabenh : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_Thongkethoigiankhamchuabenh()
        {
            InitializeComponent();
        }
        public string tun;
        public string denn;
        public void hambctgkb()
        {
            tungay.Text = tun;
            denngay.Text = denn;
            CQCQ.Text = DungChung.Bien.TenCQCQ;
            CQ.Text = DungChung.Bien.TenCQ;
            Hoten.DataBindings.Add("Text", DataSource, "TenBNhan");
            Namsinh.DataBindings.Add("Text", DataSource, "NamSinh");
            doituong.DataBindings.Add("Text", DataSource, "DTuong");
            Scls.DataBindings.Add("Text", DataSource, "Solan");
            Thoigiandk.DataBindings.Add("Text", DataSource, "NNhap").FormatString = "{0:dd/MM/yyyy hh:mm}";
            Thoigianketthuc.DataBindings.Add("Text", DataSource, "NgayTT").FormatString = "{0:dd/MM/yyyy hh:mm}";
            Thoigiankham.DataBindings.Add("Text", DataSource, "Giokham").FormatString = "{0:#,0.#}";
            lblNgayVaoVien.DataBindings.Add("Text", DataSource, "NgayVaoVien");
        }
    }
}
