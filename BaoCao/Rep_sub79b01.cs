using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
namespace QLBV.BaoCao
{
    public partial class Rep_sub79b01 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_sub79b01()
        {
            InitializeComponent();
        }
        public void BindingData()
        {
            //colMathe1.DataBindings.Add("Text", DataSource, "SThe");
            colHoten.DataBindings.Add("Text", DataSource, "TenBNhan");
            //colNgayvao.DataBindings.Add("Text", DataSource, "Ngayvao");
            //colNgayra.DataBindings.Add("Text", DataSource, "Ngayra");
            //txtNgayvao.DataBindings.Add("Text", DataSource, "Ngayvao");
            xrLabel1.DataBindings.Add("Text", DataSource, "NgayRa");
            colSotienDNTT.DataBindings.Add("Text", DataSource, "Tongcong").FormatString = DungChung.Bien.FormatString[1];
            //colSotienNguoibenh.DataBindings.Add("Text", DataSource, "Nguoibenhchitra").FormatString = DungChung.Bien.FormatString[1];
            colSotienquyettoan.DataBindings.Add("Text", DataSource, "TongcongBHYT").FormatString = DungChung.Bien.FormatString[1];
            colLydo.DataBindings.Add("Text", DataSource, "Lydo");
            colSotienTC.DataBindings.Add("Text", DataSource, "TienChenh").FormatString = DungChung.Bien.FormatString[1];
            xrTableCell33.DataBindings.Add("Text", DataSource, "TienChenh").FormatString = DungChung.Bien.FormatString[1];
            xrTableCell39.DataBindings.Add("Text", DataSource, "TienChenh").FormatString = DungChung.Bien.FormatString[1];
            xrTableCell53.DataBindings.Add("Text", DataSource, "TienChenh").FormatString = DungChung.Bien.FormatString[1];
            //colGFSotienNguoibenh.DataBindings.Add("Text", DataSource, "Nguoibenhchitra").FormatString = DungChung.Bien.FormatString[1];
            //colGFSotienQT.DataBindings.Add("Text", DataSource, "TongcongBHYT").FormatString = DungChung.Bien.FormatString[1];
            colGF2Sotien.DataBindings.Add("Text", DataSource, "Tongcong").FormatString = DungChung.Bien.FormatString[1];
            colGF1Sotien.DataBindings.Add("Text", DataSource, "Tongcong").FormatString = DungChung.Bien.FormatString[1];
            colRFSotien.DataBindings.Add("Text", DataSource, "Tongcong").FormatString = DungChung.Bien.FormatString[1];
            //colGF2Sotiennguoibenh.DataBindings.Add("Text", DataSource, "Nguoibenhchitra").FormatString = DungChung.Bien.FormatString[1];
            colGF2SotienQT.DataBindings.Add("Text", DataSource, "TongcongBHYT").FormatString = DungChung.Bien.FormatString[1];
            xrTableCell38.DataBindings.Add("Text", DataSource, "TongcongBHYT").FormatString = DungChung.Bien.FormatString[1];
            xrTableCell52.DataBindings.Add("Text", DataSource, "TongcongBHYT").FormatString = DungChung.Bien.FormatString[1];
            colGF1Sotien.DataBindings.Add("Text", DataSource, "Tongcong").FormatString = DungChung.Bien.FormatString[1];
            //colRFNguoibenh.DataBindings.Add("Text", DataSource, "Nguoibenhchitra").FormatString = DungChung.Bien.FormatString[1];
            //colRFSotienQT.DataBindings.Add("Text", DataSource, "TongcongBHYT").FormatString = DungChung.Bien.FormatString[1];
            GroupHeader2.GroupFields.Add(new GroupField("NoiTinh"));
            GroupHeader1.GroupFields.Add(new GroupField("Tuyen"));
        }
        string tongcong = " Tổng cộng ";
        private void GroupHeader2_BeforePrint(object sender, CancelEventArgs e)
        {
            if (this.GetCurrentColumnValue("NoiTinh") != null)
            {
                string noitinh = this.GetCurrentColumnValue("NoiTinh").ToString();
                switch (noitinh)
                {
                    case "1":
                        xrTableCell18.Text = " Bệnh nhân nội tỉnh kcb ban đầu".ToUpper();
                        xrTableCell17.Text = "A";
                        tongcong += "A";
                        xrTableCell36.Text = " Cộng: A";
                        break;
                    case "2":
                        xrTableCell18.Text = " Bệnh nhân nội tỉnh đến".ToUpper();
                        xrTableCell17.Text = "B";
                        tongcong += "+B";
                        xrTableCell36.Text = " Cộng: B";
                        break;
                    case "3":
                        xrTableCell18.Text = " Bệnh nhân ngoại tỉnh đến".ToUpper();
                        xrTableCell17.Text = "C";
                        tongcong += "+C";
                        xrTableCell36.Text = " Cộng: C";

                        break;
                }
            }
        }

        private void GroupHeader1_BeforePrint(object sender, CancelEventArgs e)
        {
            if (this.GetCurrentColumnValue("Tuyen") != null)
            {
                int sttg2 = int.Parse(this.GetCurrentColumnValue("Tuyen").ToString().Trim());
                if (sttg2 == 2)
                {
                    xrTableCell19.Text = "II";
                }
                else
                {
                    if (sttg2 == 1)
                    {
                        xrTableCell19.Text = "I";
                    }
                }
            }
            if (this.GetCurrentColumnValue("Tuyen") != null)
            {
                int tuyen = int.Parse(this.GetCurrentColumnValue("Tuyen").ToString().Trim());
                //int tt;
                //if (tuyen == 1)
                //{ tt = 0; }
                //else
                //{ tt = 1; }
                if (tuyen == 2)
                {
                    xrTableCell24.Text = " Trái tuyến";
                    xrTableCell30.Text = " Cộng trái tuyến";
                }
                if (tuyen == 1)
                {
                    xrTableCell24.Text = " Đúng tuyến";
                    xrTableCell30.Text = " Cộng đúng tuyến";
                }
            }
        }

        private void colHoten_BeforePrint(object sender, CancelEventArgs e)
        {
            if (this.GetCurrentColumnValue("NgayRa") != null)
            {
                txtNgayra.Text = this.GetCurrentColumnValue("NgayRa").ToString().Substring(0, 5);
            }
        }
    }
}
