using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_BCDoiChieuGia_20001_TT15 : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_BCDoiChieuGia_20001_TT15()
        {
            InitializeComponent();
        }

        internal void BindingData()
        {
            cel2.DataBindings.Add("Text", DataSource, "MaQD");
            cel3.DataBindings.Add("Text", DataSource, "TenDV");
            cel5.DataBindings.Add("Text", DataSource, "SoLuong").FormatString = DungChung.Bien.FormatString[0];

            cel6_1.DataBindings.Add("Text", DataSource, "DonGiaDV").FormatString = DungChung.Bien.FormatString[1];
            cel7_1.DataBindings.Add("Text", DataSource, "ThanhTienDV").FormatString = DungChung.Bien.FormatString[1];

            cel6.DataBindings.Add("Text", DataSource, "DonGia2").FormatString = DungChung.Bien.FormatString[1];
            cel7.DataBindings.Add("Text", DataSource, "ThanhTien2").FormatString = DungChung.Bien.FormatString[1];

            cel8.DataBindings.Add("Text", DataSource, "DonGiaBHYT").FormatString = DungChung.Bien.FormatString[1];
            cel9.DataBindings.Add("Text", DataSource, "ThanhTienBHYT").FormatString = DungChung.Bien.FormatString[1];            

            cel10.DataBindings.Add("Text", DataSource, "DonGiaTT15").FormatString = DungChung.Bien.FormatString[1];
            cel11.DataBindings.Add("Text", DataSource, "ThanhTienTT15").FormatString = DungChung.Bien.FormatString[1];
            cel12.DataBindings.Add("Text", DataSource, "ChenhLech").FormatString = DungChung.Bien.FormatString[1];
            cel13.DataBindings.Add("Text", DataSource, "ChenhLechDV").FormatString = DungChung.Bien.FormatString[1];
            cel15.DataBindings.Add("Text", DataSource, "Thang");
            cel16.DataBindings.Add("Text", DataSource, "tuyen");
            cel17.DataBindings.Add("Text", DataSource, "Loai");

            celThanhTien2_G1_1.DataBindings.Add("Text", DataSource, "ThanhTienDV").FormatString = DungChung.Bien.FormatString[1];
            celThanhTien2_G2_1.DataBindings.Add("Text", DataSource, "ThanhTienDV").FormatString = DungChung.Bien.FormatString[1];
            celThanhTien2_R_1.DataBindings.Add("Text", DataSource, "ThanhTienDV").FormatString = DungChung.Bien.FormatString[1];
            xrTableCell142.DataBindings.Add("Text", DataSource, "ThanhTienDV").FormatString = DungChung.Bien.FormatString[1];

            celThanhTien2_G1.DataBindings.Add("Text", DataSource, "ThanhTien2").FormatString = DungChung.Bien.FormatString[1];
            celThanhTien2_G2.DataBindings.Add("Text", DataSource, "ThanhTien2").FormatString = DungChung.Bien.FormatString[1];
            celThanhTien2_R.DataBindings.Add("Text", DataSource, "ThanhTien2").FormatString = DungChung.Bien.FormatString[1];
            xrTableCell125.DataBindings.Add("Text", DataSource, "ThanhTien2").FormatString = DungChung.Bien.FormatString[1];

            celThanhTienDVG1.DataBindings.Add("Text", DataSource, "ThanhTienBHYT").FormatString = DungChung.Bien.FormatString[1];
            celThanhTienDVG2.DataBindings.Add("Text", DataSource, "ThanhTienBHYT").FormatString = DungChung.Bien.FormatString[1];
            celThanhTienDVR.DataBindings.Add("Text", DataSource, "ThanhTienBHYT").FormatString = DungChung.Bien.FormatString[1];
            xrTableCell127.DataBindings.Add("Text", DataSource, "ThanhTienBHYT").FormatString = DungChung.Bien.FormatString[1];

            celThanhTienBHYTG1.DataBindings.Add("Text", DataSource, "ThanhTienTT15").FormatString = DungChung.Bien.FormatString[1];
            celThanhTienBHYTG2.DataBindings.Add("Text", DataSource, "ThanhTienTT15").FormatString = DungChung.Bien.FormatString[1];
            celThanhTienBHYTR.DataBindings.Add("Text", DataSource, "ThanhTienTT15").FormatString = DungChung.Bien.FormatString[1];
            xrTableCell129.DataBindings.Add("Text", DataSource, "ThanhTienTT15").FormatString = DungChung.Bien.FormatString[1];

            celTienChenhG1.DataBindings.Add("Text", DataSource, "ChenhLech").FormatString = DungChung.Bien.FormatString[1];
            celTienChenhG2.DataBindings.Add("Text", DataSource, "ChenhLech").FormatString = DungChung.Bien.FormatString[1];
            celTienChenhR.DataBindings.Add("Text", DataSource, "ChenhLech").FormatString = DungChung.Bien.FormatString[1];
            xrTableCell130.DataBindings.Add("Text", DataSource, "ChenhLech").FormatString = DungChung.Bien.FormatString[1];

            celTienChenh2G1.DataBindings.Add("Text", DataSource, "ChenhLechDV").FormatString = DungChung.Bien.FormatString[1];
            celTienChenh2G2.DataBindings.Add("Text", DataSource, "ChenhLechDV").FormatString = DungChung.Bien.FormatString[1];
            celTienChenh2R.DataBindings.Add("Text", DataSource, "ChenhLechDV").FormatString = DungChung.Bien.FormatString[1];
            xrTableCell131.DataBindings.Add("Text", DataSource, "ChenhLechDV").FormatString = DungChung.Bien.FormatString[1];

            //xrTableCell102.DataBindings.Add("Text", DataSource, "SoLuongNT").FormatString = DungChung.Bien.FormatString[0];
            xrTableCell84.DataBindings.Add("Text", DataSource, "SoLuong").FormatString = DungChung.Bien.FormatString[0];

            lblG2.DataBindings.Add("Text", DataSource, "NoiTinh");
            cel2_G1.DataBindings.Add("Text", DataSource, "TenTN");
            lblG1.DataBindings.Add("Text", DataSource, "PhanLoai");
            GroupHeader2.GroupFields.Add(new GroupField("PhanLoai"));
            GroupHeader1.GroupFields.Add(new GroupField("PLoai"));
            GroupHeader3.GroupFields.Add(new GroupField("NoiTinh"));
        }

        private void GroupHeader1_BeforePrint(object sender, CancelEventArgs e)
        {
            if (this.GetCurrentColumnValue("PhanLoai") != null)
            {
                string phanloai = this.GetCurrentColumnValue("PhanLoai").ToString();
                switch (phanloai)
                {
                    case "A":
                        xrTableCell103.Text = "Tiền Khám";
                        xrTableCell96.Text = "A";
                        //tongcong += "A";
                        celG1.Text = " Cộng: A";
                        break;
                    case "B":
                        xrTableCell103.Text = "Tiền Giường";
                        xrTableCell96.Text = "B";
                        //tongcong += "+B";
                        celG1.Text = " Cộng: B";
                        break;
                    case "C":
                        xrTableCell103.Text = "Xét Nghiệm";
                        xrTableCell96.Text = "C";
                        //tongcong += "+B";
                        celG1.Text = " Cộng: C";
                        break;
                    case "D":
                        xrTableCell103.Text = "CĐHA";
                        xrTableCell96.Text = "D";
                        //tongcong += "+B";
                        celG1.Text = " Cộng: D";
                        break;
                    case "E":
                        xrTableCell103.Text = "Phẫu thuật, thủ thuật";
                        xrTableCell96.Text = "E";
                        //tongcong += "+B";
                        celG1.Text = " Cộng: E";
                        break;
                    case "F":
                        xrTableCell103.Text = "Vận chuyển";
                        xrTableCell96.Text = "F";
                        //tongcong += "+B";
                        celG1.Text = " Cộng: F";
                        break;
                    case "G":
                        xrTableCell103.Text = "DVKT thanh toán theo tỷ lệ";
                        xrTableCell96.Text = "G";
                        //tongcong += "+B";
                        celG1.Text = " Cộng: G";
                        break;
                    case "H":
                        xrTableCell103.Text = "Thăm dò chức năng";
                        xrTableCell96.Text = "H";
                        //tongcong += "+B";
                        celG1.Text = " Cộng: H";
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
                        celG2.Text = " Cộng: A + B + C + D + E + F + G + H";
                        break;
                    case "2":
                        cel2_G2.Text = "Bệnh nhân đa tuyến nội tỉnh (tuyến 2)";
                        cel1_G2.Text = "II";
                        //tongcong += "+B";
                        celG2.Text = " Cộng: A + B + C + D + E + F + G + H";
                        break;
                    case "3":
                        cel2_G2.Text = "Bệnh nhân đa tuyến ngoại tỉnh (tuyến 3)";
                        cel1_G2.Text = "III";
                        //tongcong += "+C";
                        celG2.Text = " Cộng: A + B + C + D + E + F + G + H";
                        break;
                }

            }
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            lblTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            lblTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
        }

        private void GroupHeader1_BeforePrint_1(object sender, CancelEventArgs e)
        {
            //if (this.GetCurrentColumnValue("TenTN") != null)
            //{
            //    string noitinh = this.GetCurrentColumnValue("TenTN").ToString();
            //    xrTableCell121.Text = " Tổng " + noitinh;
            //}
        }
    }
}
