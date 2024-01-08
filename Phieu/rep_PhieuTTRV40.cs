using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_PhieuTTRV40 : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_PhieuTTRV40()
        {
            InitializeComponent();
        }
        public void BindingData()
        {
            colTenDV.DataBindings.Add("Text", DataSource, "TenDV");
            colDonVI.DataBindings.Add("Text", DataSource, "Donvi");
            colDonGia.DataBindings.Add("Text", DataSource, "DonGia").FormatString=DungChung.Bien.FormatString[1];
            colSoLuong.DataBindings.Add("Text", DataSource, "SoLuong").FormatString = DungChung.Bien.FormatString[1];
            colThanhTien.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];
            colNhomDV.DataBindings.Add("Text", DataSource, "TenNhom").FormatString = DungChung.Bien.FormatString[1];
            colTrongDMkt.DataBindings.Add("Text", DataSource, "TrongDM").FormatString = DungChung.Bien.FormatString[1];
            colThanhTienG.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];
            colThanhTienrep.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];
            //colSTT.DataBindings.Add("Text", DataSource, "TieuNhom");
            colNhomDVthuoc.DataBindings.Add("Text", DataSource, "TenNhom");
            GroupHeader2.GroupFields.Add(new GroupField("STT"));
            GroupHeader1.GroupFields.Add(new GroupField("TrongDM"));
        }

        int sttg1 = 1;
        private void GroupHeader1_BeforePrint(object sender, CancelEventArgs e)
        {
            
            sttg1 = sttgroup2 - 1;
            string nhom = "";
            if (GetCurrentColumnValue("TenNhom") != null)
            {
                nhom = this.GetCurrentColumnValue("TenNhom").ToString();
            }
            int TrongDM = this.GetCurrentColumnValue<int>("TrongDM");
            if (nhom.Contains("Thuốc"))
            {
                if (TrongDM == 1)
                {
                    colSTTg1.Text = " " + sttg1.ToString() + ".1.";
                    colTrongDM.Text = "Trong danh mục BHYT";
                }
                else
                {
                    if (TrongDM == 0)
                    {
                        colTrongDM.Text = "Ngoài danh mục BHYT";
                    }
                }

            }
            else
            {
                e.Cancel = true;
            }
        }
        int sttgroup2 = 1;
        private void GroupHeader2_BeforePrint(object sender, CancelEventArgs e)
        {
            sttdv = 1;
            txtSoTTg2.Text = " " + sttgroup2.ToString() + ".";
            sttgroup2++;

        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            txtTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            txtTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            string sthe = SoThe.Value.ToString();
            if (sthe.Length == 15)
            {
                txtThe1.Text = sthe.Substring(0, 2);
                txtThe2.Text = sthe.Substring(2, 1);
                txtThe3.Text = sthe.Substring(3, 2);
                txtThe4.Text = sthe.Substring(5, 2);
                txtThe5.Text = sthe.Substring(7, 3);
                txtThe6.Text = sthe.Substring(10, 5);
            }
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            //ColBangChu.Text = " (Bằng chữ): " + DungChung.Ham.DocTienBangChu(Convert.ToDouble(Tongtien.Value), " đồng!");
            txtNguoiLapBieu.Text ="Họ tên:"+ DungChung.Bien.NguoiLapBieu;
            txtKeToanVP.Text = "Họ tên:" + DungChung.Bien.KeToanVP;
            txtGiamDinhBH.Text = "Họ tên:" + DungChung.Bien.GiamDinhBH;
        }

        private void xrTable5_BeforePrint(object sender, CancelEventArgs e)
        {

        }

        private void ColBangChu_BeforePrint(object sender, CancelEventArgs e)
        {
         ColBangChu.Text=   " Bằng chữ: " + DungChung.Ham.DocTienBangChu(Convert.ToDouble(Tongtien.Value), " đồng.");
        }
        int sttdv = 1;
        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "20001")
            {
                txtsottdv.Text = sttdv.ToString() + ".";
                sttdv++;
            }
            else
            {
                txtsottdv.Text = "-";
            }
        }

    }
}
