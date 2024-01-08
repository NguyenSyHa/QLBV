﻿using System;
using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class repBangKeCPNgoaiBH_A4 : DevExpress.XtraReports.UI.XtraReport
    {
        public repBangKeCPNgoaiBH_A4()
        {
            InitializeComponent();
        }
        public void BindingData(){
            colTenDV.DataBindings.Add("Text", DataSource, "TenDV");
            colDonVI.DataBindings.Add("Text", DataSource, "Donvi");
            colDonGia.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[1];
            colSoLuong.DataBindings.Add("Text", DataSource, "SoLuong").FormatString = DungChung.Bien.FormatString[0]; 
            colThanhTien.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString=DungChung.Bien.FormatString[1];
            colNhomDV.DataBindings.Add("Text", DataSource, "TenNhom");
            colTrongDMkt.DataBindings.Add("Text", DataSource, "TrongDM");
            colThanhTienG.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];
            colThanhTienrep.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];
            colThanhTienG.Summary.FormatString = DungChung.Bien.FormatString[1];
            colThanhTienrep.Summary.FormatString = DungChung.Bien.FormatString[1];
            //colSTT.DataBindings.Add("Text", DataSource, "TieuNhom");
            txtNguoiLapBieu.Text = DungChung.Bien.NguoiLapBieu;
            xrLabel115.Text = DungChung.Bien.NguoiLapBieu;
            colNhomDVthuoc.DataBindings.Add("Text", DataSource, "TenNhomBK02");
            GroupHeader2.GroupFields.Add(new GroupField("STT"));
            GroupHeader1.GroupFields.Add(new GroupField("TrongDM"));
        }
        int sttg1 = 1;
        private void GroupHeader1_BeforePrint(object sender, CancelEventArgs e)
        {
            sttg1 = sttgroup2 -1;
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
                    colSTTg1.Text = " "+sttg1.ToString()+".1.";
                    colTrongDM.Text = "Trong danh mục BHYT";
                }
                else {
                    if (TrongDM == 0) {
                        colTrongDM.Text = "Ngoài danh mục BHYT";
                    }
                }

            }
            else {
                e.Cancel = true;
            }
        }

        private void colTrongDM_BeforePrint(object sender, CancelEventArgs e)
        {
            //if (GetCurrentColumnValue("colTrongDM") != null && GetCurrentColumnValue("colTrongDM").ToString() != "")
            //{
            //    System.Windows.Forms.MessageBox.Show(GetCurrentColumnValue("colTrongDM").ToString());

            //    if (GetCurrentColumnValue("colTrongDM").ToString() == "1")
            //    {
            //        GroupHeader1.Visible = true;
            //    }
            //}
            //else
            //{
            //    GroupHeader1.Visible = false;
            //}
        }
        int sttgroup2 = 1;
        private void GroupHeader2_BeforePrint(object sender, CancelEventArgs e)
        {
            txtSoTTg2.Text =" "+ sttgroup2.ToString()+".";
            sttgroup2++;
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            if(DungChung.Bien.MaBV == "14017")
            {
                SubBand1.Visible = false;
                SubBand2.Visible = true;
            }
            else if (DungChung.Bien.MaBV == "24272")
            {
                SubBand1.Visible = false;
                SubBand2.Visible = false;
                SubBand5.Visible = true;
            }
            else
            {
                SubBand2.Visible = false;
            }
            xrPictureBox1.Image = DungChung.Ham.GetLogo();
            txtTenCQCQ.Text = txtTenCQCQ2.Text = DungChung.Bien.TenCQCQ;
            cqcq.Text = DungChung.Bien.TenCQCQ;
            TenCQ.Value = DungChung.Bien.TenCQ;
            colDiaChi.Text = DungChung.Ham.GetDiaChiBV();
            string sthe = SoThe.Value.ToString();
            if (sthe.Length == 15)
            {
                txtThe1.Text = xrLabel75.Text = sthe.Substring(0, 2);
                txtThe2.Text = xrLabel76.Text= sthe.Substring(2, 1);
                txtThe3.Text = xrLabel77.Text = sthe.Substring(3, 2);
                txtThe4.Text = xrLabel78.Text = sthe.Substring(5, 2);
                txtThe5.Text = xrLabel79.Text = sthe.Substring(7, 3);
                txtThe6.Text = xrLabel80.Text = sthe.Substring(10, 5);
            }
            if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049")
            {
                colDiaChi.Visible = false;
            }
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            if(DungChung.Bien.MaBV == "14017")
            {
                SubBand3.Visible = false;
            }
            else
            {
                SubBand4.Visible = false;
            }
            txtTT.Text = "- Tổng chi phí đợt điều trị: " + DungChung.Ham.DocTienBangChu(Convert.ToDouble(Tongtien.Value), " đồng.");
            if (DungChung.Bien.MaBV != "30003" && txtNguoiLapBieu.Text =="")
            {
                txtNguoiLapBieu.Text = DungChung.Bien.NguoiLapBieu;
            }
            xrLabel113.Text = DungChung.Bien.NguoiLapBieu;
            txtKeToanVP.Text = DungChung.Bien.KeToanVP;
            xrLabel114.Text = DungChung.Bien.KeToanVP;
            if (DungChung.Bien.MaBV == "24208")
            {
                colKTVP.Text = "Trưởng trạm";
                txtKeToanVP.Text = DungChung.Bien.GiamDoc;
            }

        }

        private void repBangKeCPNgoaiBH_A4_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "27023")
                TopMargin.HeightF = 95F;
        }

       

    }
}
