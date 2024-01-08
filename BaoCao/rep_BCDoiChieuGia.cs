using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_BCDoiChieuGia : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_BCDoiChieuGia()
        {
            InitializeComponent();
        }


        internal void BindingData()
        {
            cel2.DataBindings.Add("Text", DataSource, "MaQD");
            cel3.DataBindings.Add("Text", DataSource, "TenDV");
            cel4.DataBindings.Add("Text", DataSource, "SoLuong").FormatString = DungChung.Bien.FormatString[0];
            cel5.DataBindings.Add("Text", DataSource, "DonGia2").FormatString = DungChung.Bien.FormatString[1];
            cel6.DataBindings.Add("Text", DataSource, "ThanhTien2").FormatString = DungChung.Bien.FormatString[1];
            cel7.DataBindings.Add("Text", DataSource, "DonGiaDV").FormatString = DungChung.Bien.FormatString[1];
            cel8.DataBindings.Add("Text", DataSource, "ThanhTienDV").FormatString = DungChung.Bien.FormatString[1];
            cel9.DataBindings.Add("Text", DataSource, "DonGiaBHYT").FormatString = DungChung.Bien.FormatString[1];
            cel10.DataBindings.Add("Text", DataSource, "ThanhTienBHYT").FormatString = DungChung.Bien.FormatString[1];
            cel11.DataBindings.Add("Text", DataSource, "ChenhLech").FormatString = DungChung.Bien.FormatString[1];
            cel12.DataBindings.Add("Text", DataSource, "ChenhLechDV").FormatString = DungChung.Bien.FormatString[1];
            cel14.DataBindings.Add("Text", DataSource, "Thang");
            cel15.DataBindings.Add("Text", DataSource, "tuyen");
            cel16.DataBindings.Add("Text", DataSource, "Loai");

            celThanhTien2_G1.DataBindings.Add("Text", DataSource, "ThanhTien2").FormatString = DungChung.Bien.FormatString[1];
            celThanhTien2_G2.DataBindings.Add("Text", DataSource, "ThanhTien2").FormatString = DungChung.Bien.FormatString[1];
            celThanhTien2_R.DataBindings.Add("Text", DataSource, "ThanhTien2").FormatString = DungChung.Bien.FormatString[1];

            celThanhTienDVG1.DataBindings.Add("Text", DataSource, "ThanhTienDV").FormatString = DungChung.Bien.FormatString[1];
            celThanhTienDVG2.DataBindings.Add("Text", DataSource, "ThanhTienDV").FormatString = DungChung.Bien.FormatString[1];
            celThanhTienDVR.DataBindings.Add("Text", DataSource, "ThanhTienDV").FormatString = DungChung.Bien.FormatString[1];

            celThanhTienBHYTG1.DataBindings.Add("Text", DataSource, "ThanhTienBHYT").FormatString = DungChung.Bien.FormatString[1];
            celThanhTienBHYTG2.DataBindings.Add("Text", DataSource, "ThanhTienBHYT").FormatString = DungChung.Bien.FormatString[1];
            celThanhTienBHYTR.DataBindings.Add("Text", DataSource, "ThanhTienBHYT").FormatString = DungChung.Bien.FormatString[1];

            celTienChenhG1.DataBindings.Add("Text", DataSource, "ChenhLech").FormatString = DungChung.Bien.FormatString[1];
            celTienChenhG2.DataBindings.Add("Text", DataSource, "ChenhLech").FormatString = DungChung.Bien.FormatString[1];
            celTienChenhR.DataBindings.Add("Text", DataSource, "ChenhLech").FormatString = DungChung.Bien.FormatString[1];

            celTienChenh2G1.DataBindings.Add("Text", DataSource, "ChenhLechDV").FormatString = DungChung.Bien.FormatString[1];
            celTienChenh2G2.DataBindings.Add("Text", DataSource, "ChenhLechDV").FormatString = DungChung.Bien.FormatString[1];
            celTienChenh2R.DataBindings.Add("Text", DataSource, "ChenhLechDV").FormatString = DungChung.Bien.FormatString[1];

            lblG2.DataBindings.Add("Text", DataSource, "NoiTinh");
            lblG1.DataBindings.Add("Text", DataSource, "PhanLoai");
            GroupHeader1.GroupFields.Add(new GroupField("PhanLoai"));
            GroupHeader2.GroupFields.Add(new GroupField("NoiTinh"));
        }

        private void GroupHeader1_BeforePrint(object sender, CancelEventArgs e)
        {
            if (this.GetCurrentColumnValue("PhanLoai") != null)
            {
                string phanloai = this.GetCurrentColumnValue("PhanLoai").ToString();
                switch (phanloai)
                {
                    case "A":
                        cel2_G1.Text = "Tiền Khám";
                        cel1_G1.Text = "A";
                        //tongcong += "A";
                        celG1.Text = " Cộng: A";
                        break;
                    case "B":
                        cel2_G1.Text = "Dịch vụ kỹ thuật";
                        cel1_G1.Text = "B";
                        //tongcong += "+B";
                        celG1.Text = " Cộng: B";
                        break;
                   
                }

            }
        }

        private void GroupHeader2_BeforePrint(object sender, CancelEventArgs e)
        {
            if (this.GetCurrentColumnValue("NoiTinh") != null)
            {
                string noitinh = this.GetCurrentColumnValue("NoiTinh").ToString();
                switch (noitinh)
                {
                    case "1":
                        cel2_G2.Text = "Bệnh nhân đăng ký KCB ban đầu (tuyến 1)";
                        cel1_G2.Text = "I";
                        //tongcong += "A";
                        celG2.Text = " Cộng: A + B";
                        break;
                    case "2":
                        cel2_G2.Text = "Bệnh nhân đa tuyến nội tỉnh (tuyến 2)";
                        cel1_G2.Text = "II";
                        //tongcong += "+B";
                        celG2.Text = " Cộng: A + B";
                        break;
                    case "3":
                        cel2_G2.Text = "Bệnh nhân đa tuyến ngoại tỉnh (tuyến 3)";
                        cel1_G2.Text = "III";
                        //tongcong += "+C";
                        celG2.Text = " Cộng: A + B";
                        break;
                }

            }
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            lblTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            lblTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
        }
    }
}
