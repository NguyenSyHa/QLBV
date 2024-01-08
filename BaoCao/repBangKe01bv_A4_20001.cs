using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class repBangKe01bv_A4_20001 : DevExpress.XtraReports.UI.XtraReport
    {
        public repBangKe01bv_A4_20001()
        {
            InitializeComponent();
        }
        public void BindingData(){
            int _size = 11;
            if (DungChung.Bien.MaBV == "30009")
            {
                _size = 10;

                colTenDV.Font = new Font("Times New Roman", _size);
                colDonVI.Font = new Font("Times New Roman", _size);
                colDonGia.Font = new Font("Times New Roman", _size);
                colSoLuong.Font = new Font("Times New Roman", _size);
                colThanhTien.Font = new Font("Times New Roman", _size);
                colBHYT.Font = new Font("Times New Roman", _size);
                colBNhan.Font = new Font("Times New Roman", _size);
                colNhomDV.Font = new Font("Times New Roman", _size, System.Drawing.FontStyle.Bold);
                colTrongDMkt.Font = new Font("Times New Roman", _size);
                colThanhTienG.Font = new Font("Times New Roman", _size, System.Drawing.FontStyle.Bold);
                colBNhanG.Font = new Font("Times New Roman", _size, System.Drawing.FontStyle.Bold);
                colBHYTg.Font = new Font("Times New Roman", _size, System.Drawing.FontStyle.Bold);
                colThanhTienrep.Font = new Font("Times New Roman", _size, System.Drawing.FontStyle.Bold);
                colBHYTrep.Font = new Font("Times New Roman", _size, System.Drawing.FontStyle.Bold);
                colBNhanrep.Font = new Font("Times New Roman", _size, System.Drawing.FontStyle.Bold);
                colNhomDVthuoc.Font = new Font("Times New Roman", _size, System.Drawing.FontStyle.Bold);
                colTrongDM.Font = new Font("Times New Roman", _size, System.Drawing.FontStyle.Bold);
                colTD_Cong.Font = new Font("Times New Roman", _size, System.Drawing.FontStyle.Bold);
                colTD_TongCong.Font = new Font("Times New Roman", _size, System.Drawing.FontStyle.Bold);
            }
            colTenDV.DataBindings.Add("Text", DataSource, "TenDV");
            colDonVI.DataBindings.Add("Text", DataSource, "Donvi");
            colDonGia.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[1];
            
                colSoLuong.DataBindings.Add("Text", DataSource, "SoLuong").FormatString = DungChung.Bien.FormatString[0];
            
            colThanhTien.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString=DungChung.Bien.FormatString[1];
            colBHYT.DataBindings.Add("Text", DataSource, "TienBH").FormatString = DungChung.Bien.FormatString[1];
            colBNhan.DataBindings.Add("Text", DataSource, "TienBN").FormatString = DungChung.Bien.FormatString[1];
            colNhomDV.DataBindings.Add("Text", DataSource, "TenNhom");
            colTrongDMkt.DataBindings.Add("Text", DataSource, "TrongBH");
            colThanhTienG.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];
            colBHYTg.DataBindings.Add("Text", DataSource, "TienBH").FormatString = DungChung.Bien.FormatString[1];
            colBNhanG.DataBindings.Add("Text", DataSource, "TienBN").FormatString = DungChung.Bien.FormatString[1];
            colThanhTienrep.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];
            colBHYTrep.DataBindings.Add("Text", DataSource, "TienBH").FormatString = DungChung.Bien.FormatString[1];
            colBNhanrep.DataBindings.Add("Text", DataSource, "TienBN").FormatString = DungChung.Bien.FormatString[1];
            colBNhanG.Summary.FormatString = DungChung.Bien.FormatString[1];
            colBNhanrep.Summary.FormatString = DungChung.Bien.FormatString[1];
            colThanhTienG.Summary.FormatString = DungChung.Bien.FormatString[1];
            colThanhTienrep.Summary.FormatString = DungChung.Bien.FormatString[1];
            colBHYTg.Summary.FormatString = DungChung.Bien.FormatString[1];
            colBHYTrep.Summary.FormatString = DungChung.Bien.FormatString[1];

            //colSTT.DataBindings.Add("Text", DataSource, "TieuNhom");
            colNhomDVthuoc.DataBindings.Add("Text", DataSource, "TenNhom");
            if(DungChung.Bien.MaBV == "12122")
            {
                celSoLuongG2.DataBindings.Add("Text", DataSource, "SoLuong");
                celSoLuongT.DataBindings.Add("Text", DataSource, "SoLuong");
            }
            else
            {
                celSoLuongG2.DataBindings.Add("Text", DataSource, "abc");
                celSoLuongT.DataBindings.Add("Text", DataSource, "abc");
            }
            GroupHeader2.GroupFields.Add(new GroupField("STT"));
            GroupHeader1.GroupFields.Add(new GroupField("TrongBH"));
        }
        int sttg1 = 1;
        private void GroupHeader1_BeforePrint(object sender, CancelEventArgs e)
        {
            sttdv = 1;
            sttg1 = sttgroup2 -1;
            string nhom = "";
            if (GetCurrentColumnValue("TenNhom") != null)
            {
                nhom = this.GetCurrentColumnValue("TenNhom").ToString();
            }
            int TrongDM = this.GetCurrentColumnValue<int>("TrongBH");
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
            txtTenCQCQ.Text = DungChung.Bien.TenCQCQ;
            TenCQ.Value = DungChung.Bien.TenCQ;
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
          

            NguoiLapBieu.Value = DungChung.Bien.NguoiLapBieu;
            GDBH.Value = DungChung.Bien.GiamDinhBH;
            KeToanVP.Value = DungChung.Bien.KeToanVP;
           
            
            if (DungChung.Bien.MaBV == "20001")
            {
                row2.Visible = true;
            }
            else
            {
                row2.Visible = false;
            }

            //if (DungChung.Bien.MaBV == "04256")
            //{

            //    txtDaiDien.Visible = true;
            //    txtDaiDien_KT.Visible = true;
            //}
        }

        private void GroupFooter2_BeforePrint(object sender, CancelEventArgs e)
        {
            colTD_Cong.Text = "Cộng " + (sttgroup2 - 1).ToString() +": ";
        }

        private void colKTVP_BeforePrint(object sender, CancelEventArgs e)
        {

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
