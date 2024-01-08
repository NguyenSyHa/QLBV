using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_PhieuTTRV38 : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_PhieuTTRV38()
        {
            InitializeComponent();
        }
        public void BindingData()
        {
            colTenDV.DataBindings.Add("Text", DataSource, "TenDV");
            colDonVI.DataBindings.Add("Text", DataSource, "Donvi");
            colDonGia.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[1];
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

            if (DungChung.Bien.MaBV == "27021")
            {
                GroupHeader3.Visible = true;
                GroupHeader3.GroupFields.Add(new GroupField("Dongy"));
                colploai.DataBindings.Add("Text", DataSource, "Dongy");
            }
            else
            {
                GroupHeader3.Visible = false;
            }
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

                if (DungChung.Bien.MaBV == "27021")
                {
                    GroupHeader3.Visible = true;
                }
                else
                {
                    GroupHeader3.Visible = false;
                }
                if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049")
                {
                    GroupHeader1.Visible = false;
                }
                else
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
            if (DungChung.Bien.MaBV == "30009")
            {
                xrLabel9.Text = "Nơi ĐK KCB ban đầu";
                txtMaCS.Visible = true;
            }
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            //ColBangChu.Text = " (Bằng chữ): " + DungChung.Ham.DocTienBangChu(Convert.ToDouble(Tongtien.Value), " đồng!");
            // txtGiamDoc.Text ="Họ tên:"+ DungChung.Bien.GiamDoc;

            if (DungChung.Bien.MaBV != "30005" && DungChung.Bien.MaBV != "30281")
                txtKeToanVP.Text = "Họ tên:" + DungChung.Bien.KeToanVP;
            if (DungChung.Bien.MaBV != "30281")
            {
                txtGiamDinhBH.Text = "Họ tên:" + DungChung.Bien.GiamDinhBH;
            }

        }


        private void ColBangChu_BeforePrint(object sender, CancelEventArgs e)
        {
            ColBangChu.Text = " Bằng chữ: " + DungChung.Ham.DocTienBangChu(Convert.ToDouble(Tongtien.Value), " đồng.");
        }
        int sttdongy = 1;
        private void GroupHeader3_BeforePrint(object sender, CancelEventArgs e)
        {

            colsttdongy.Text = " " + (sttgroup2 - 1).ToString() + "." + (sttg1 - 2).ToString() + "." + sttdongy.ToString();
            sttdongy++;

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
