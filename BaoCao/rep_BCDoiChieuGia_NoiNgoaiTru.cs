using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_BCDoiChieuGia_NoiNgoaiTru : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_BCDoiChieuGia_NoiNgoaiTru()
        {
            InitializeComponent();
        }


        internal void BindingData()
        {
            cel2.DataBindings.Add("Text", DataSource, "MaQD");
            cel3.DataBindings.Add("Text", DataSource, "TenDV");
            cel4.DataBindings.Add("Text", DataSource, "SoLuongNgT").FormatString = DungChung.Bien.FormatString[0];
            cel5.DataBindings.Add("Text", DataSource, "SoLuongNT").FormatString = DungChung.Bien.FormatString[0];
            cel6.DataBindings.Add("Text", DataSource, "DonGiaBHYT").FormatString = DungChung.Bien.FormatString[1];// giá BHYT đang sử dụng
            cel7.DataBindings.Add("Text", DataSource, "ThanhTienBHYTNgT").FormatString = DungChung.Bien.FormatString[1];
            cel8.DataBindings.Add("Text", DataSource, "ThanhTienBHYTNT").FormatString = DungChung.Bien.FormatString[1];
            cel9.DataBindings.Add("Text", DataSource, "DonGia2").FormatString = DungChung.Bien.FormatString[1];// giá bảo hiểm cũ
            cel10.DataBindings.Add("Text", DataSource, "ThanhTien2NgT").FormatString = DungChung.Bien.FormatString[1];
            cel11.DataBindings.Add("Text", DataSource, "ThanhTien2NT").FormatString = DungChung.Bien.FormatString[1];
            cel12.DataBindings.Add("Text", DataSource, "DonGiaDV").FormatString = DungChung.Bien.FormatString[1];// giá dịch vụ cũ
            cel13.DataBindings.Add("Text", DataSource, "ThanhTienDVNgT").FormatString = DungChung.Bien.FormatString[1];
            cel14.DataBindings.Add("Text", DataSource, "ThanhTienDVNT").FormatString = DungChung.Bien.FormatString[1];
            cel15.DataBindings.Add("Text", DataSource, "ChenhLechNgT").FormatString = DungChung.Bien.FormatString[1];
            cel16.DataBindings.Add("Text", DataSource, "ChenhLechNT").FormatString = DungChung.Bien.FormatString[1];
            cel17.DataBindings.Add("Text", DataSource, "ChenhLechDVNgT").FormatString = DungChung.Bien.FormatString[1];
            cel18.DataBindings.Add("Text", DataSource, "ChenhLechDVNT").FormatString = DungChung.Bien.FormatString[1];

            cel4G1.DataBindings.Add("Text", DataSource, "SoLuongNgT").FormatString = DungChung.Bien.FormatString[0];
            cel5G1.DataBindings.Add("Text", DataSource, "SoLuongNT").FormatString = DungChung.Bien.FormatString[0];           
            cel7G1.DataBindings.Add("Text", DataSource, "ThanhTienBHYTNgT").FormatString = DungChung.Bien.FormatString[1];
            cel8G1.DataBindings.Add("Text", DataSource, "ThanhTienBHYTNT").FormatString = DungChung.Bien.FormatString[1];           
            cel10G1.DataBindings.Add("Text", DataSource, "ThanhTien2NgT").FormatString = DungChung.Bien.FormatString[1];
            cel11G1.DataBindings.Add("Text", DataSource, "ThanhTien2NT").FormatString = DungChung.Bien.FormatString[1];           
            cel13G1.DataBindings.Add("Text", DataSource, "ThanhTienDVNgT").FormatString = DungChung.Bien.FormatString[1];
            cel14G1.DataBindings.Add("Text", DataSource, "ThanhTienDVNT").FormatString = DungChung.Bien.FormatString[1];
            cel15G1.DataBindings.Add("Text", DataSource, "ChenhLechNgT").FormatString = DungChung.Bien.FormatString[1];
            cel16G1.DataBindings.Add("Text", DataSource, "ChenhLechNT").FormatString = DungChung.Bien.FormatString[1];
            cel17G1.DataBindings.Add("Text", DataSource, "ChenhLechDVNgT").FormatString = DungChung.Bien.FormatString[1];
            cel18G1.DataBindings.Add("Text", DataSource, "ChenhLechDVNT").FormatString = DungChung.Bien.FormatString[1];

            cel4G2.DataBindings.Add("Text", DataSource, "SoLuongNgT").FormatString = DungChung.Bien.FormatString[0];
            cel5G2.DataBindings.Add("Text", DataSource, "SoLuongNT").FormatString = DungChung.Bien.FormatString[0];
            cel7G2.DataBindings.Add("Text", DataSource, "ThanhTienBHYTNgT").FormatString = DungChung.Bien.FormatString[1];
            cel8G2.DataBindings.Add("Text", DataSource, "ThanhTienBHYTNT").FormatString = DungChung.Bien.FormatString[1];
            cel10G2.DataBindings.Add("Text", DataSource, "ThanhTien2NgT").FormatString = DungChung.Bien.FormatString[1];
            cel11G2.DataBindings.Add("Text", DataSource, "ThanhTien2NT").FormatString = DungChung.Bien.FormatString[1];
            cel13G2.DataBindings.Add("Text", DataSource, "ThanhTienDVNgT").FormatString = DungChung.Bien.FormatString[1];
            cel14G2.DataBindings.Add("Text", DataSource, "ThanhTienDVNT").FormatString = DungChung.Bien.FormatString[1];
            cel15G2.DataBindings.Add("Text", DataSource, "ChenhLechNgT").FormatString = DungChung.Bien.FormatString[1];
            cel16G2.DataBindings.Add("Text", DataSource, "ChenhLechNT").FormatString = DungChung.Bien.FormatString[1];
            cel17G2.DataBindings.Add("Text", DataSource, "ChenhLechDVNgT").FormatString = DungChung.Bien.FormatString[1];
            cel18G2.DataBindings.Add("Text", DataSource, "ChenhLechDVNT").FormatString = DungChung.Bien.FormatString[1];

            cel4R.DataBindings.Add("Text", DataSource, "SoLuongNgT").FormatString = DungChung.Bien.FormatString[0];
            cel5R.DataBindings.Add("Text", DataSource, "SoLuongNT").FormatString = DungChung.Bien.FormatString[0];
            cel7R.DataBindings.Add("Text", DataSource, "ThanhTienBHYTNgT").FormatString = DungChung.Bien.FormatString[1];
            cel8R.DataBindings.Add("Text", DataSource, "ThanhTienBHYTNT").FormatString = DungChung.Bien.FormatString[1];
            cel10R.DataBindings.Add("Text", DataSource, "ThanhTien2NgT").FormatString = DungChung.Bien.FormatString[1];
            cel11R.DataBindings.Add("Text", DataSource, "ThanhTien2NT").FormatString = DungChung.Bien.FormatString[1];
            cel13R.DataBindings.Add("Text", DataSource, "ThanhTienDVNgT").FormatString = DungChung.Bien.FormatString[1];
            cel14R.DataBindings.Add("Text", DataSource, "ThanhTienDVNT").FormatString = DungChung.Bien.FormatString[1];
            cel15R.DataBindings.Add("Text", DataSource, "ChenhLechNgT").FormatString = DungChung.Bien.FormatString[1];
            cel16R.DataBindings.Add("Text", DataSource, "ChenhLechNT").FormatString = DungChung.Bien.FormatString[1];
            cel17R.DataBindings.Add("Text", DataSource, "ChenhLechDVNgT").FormatString = DungChung.Bien.FormatString[1];
            cel18R.DataBindings.Add("Text", DataSource, "ChenhLechDVNT").FormatString = DungChung.Bien.FormatString[1];

            cel4G1.Summary.FormatString = DungChung.Bien.FormatString[0];
            cel5G1.Summary.FormatString = DungChung.Bien.FormatString[0];
            cel7G1.Summary.FormatString = DungChung.Bien.FormatString[1];
            cel8G1.Summary.FormatString = DungChung.Bien.FormatString[1];
            cel10G1.Summary.FormatString = DungChung.Bien.FormatString[1];
            cel11G1.Summary.FormatString = DungChung.Bien.FormatString[1];
            cel13G1.Summary.FormatString = DungChung.Bien.FormatString[1];
            cel14G1.Summary.FormatString = DungChung.Bien.FormatString[1];
            cel15G1.Summary.FormatString = DungChung.Bien.FormatString[1];
            cel16G1.Summary.FormatString = DungChung.Bien.FormatString[1];
            cel17G1.Summary.FormatString = DungChung.Bien.FormatString[1];
            cel18G1.Summary.FormatString = DungChung.Bien.FormatString[1];

            cel4G2.Summary.FormatString = DungChung.Bien.FormatString[0];
            cel5G2.Summary.FormatString = DungChung.Bien.FormatString[0];
            cel7G2.Summary.FormatString = DungChung.Bien.FormatString[1];
            cel8G2.Summary.FormatString = DungChung.Bien.FormatString[1];
            cel10G2.Summary.FormatString = DungChung.Bien.FormatString[1];
            cel11G2.Summary.FormatString = DungChung.Bien.FormatString[1];
            cel13G2.Summary.FormatString = DungChung.Bien.FormatString[1];
            cel14G2.Summary.FormatString = DungChung.Bien.FormatString[1];
            cel15G2.Summary.FormatString = DungChung.Bien.FormatString[1];
            cel16G2.Summary.FormatString = DungChung.Bien.FormatString[1];
            cel17G2.Summary.FormatString = DungChung.Bien.FormatString[1];
            cel18G2.Summary.FormatString = DungChung.Bien.FormatString[1];

            cel4R.Summary.FormatString = DungChung.Bien.FormatString[0];
            cel5R.Summary.FormatString = DungChung.Bien.FormatString[0];
            cel7R.Summary.FormatString = DungChung.Bien.FormatString[1];
            cel8R.Summary.FormatString = DungChung.Bien.FormatString[1];
            cel10R.Summary.FormatString = DungChung.Bien.FormatString[1];
            cel11R.Summary.FormatString = DungChung.Bien.FormatString[1];
            cel13R.Summary.FormatString = DungChung.Bien.FormatString[1];
            cel14R.Summary.FormatString = DungChung.Bien.FormatString[1];
            cel15R.Summary.FormatString = DungChung.Bien.FormatString[1];
            cel16R.Summary.FormatString = DungChung.Bien.FormatString[1];
            cel17R.Summary.FormatString = DungChung.Bien.FormatString[1];
            cel18R.Summary.FormatString = DungChung.Bien.FormatString[1];

            lblG2.DataBindings.Add("Text", DataSource, "NoiTinh");
            if(DungChung.Bien.MaBV == "20001")
            {
                cel2_G1.DataBindings.Add("Text", DataSource, "TenTN");
                GroupHeader1.GroupFields.Add(new GroupField("TenTN"));
            }
            else
            {
                lblG1.DataBindings.Add("Text", DataSource, "PhanLoai");
                GroupHeader1.GroupFields.Add(new GroupField("PhanLoai"));
            }
            GroupHeader2.GroupFields.Add(new GroupField("NoiTinh"));
        }

        private void GroupHeader1_BeforePrint(object sender, CancelEventArgs e)
        {
            if(DungChung.Bien.MaBV ==  "20001")
            {

            }
            else
            {
                if (this.GetCurrentColumnValue("PhanLoai") != null)
                {
                    string phanloai = this.GetCurrentColumnValue("PhanLoai").ToString();
                    switch (phanloai)
                    {
                        case "A":
                            cel2_G1.Text = "Tiền Khám";
                            cel1_G1.Text = "A";

                            break;
                        case "B":
                            cel2_G1.Text = "Dịch vụ kỹ thuật";
                            cel1_G1.Text = "B";

                            break;

                    }
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
                        
                        break;
                    case "2":
                        cel2_G2.Text = "Bệnh nhân đa tuyến nội tỉnh (tuyến 2)";
                        cel1_G2.Text = "II";
                       
                        break;
                    case "3":
                        cel2_G2.Text = "Bệnh nhân đa tuyến ngoại tỉnh (tuyến 3)";
                        cel1_G2.Text = "III";
                        
                        break;
                }

            }
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
          
            lblTenCQ.Text = "Tên cơ sở y tế: " +  DungChung.Bien.TenCQ;
            lblMaBV.Text = "Mã cơ sở y tế: " + DungChung.Bien.MaBV;

        }
    }
}
