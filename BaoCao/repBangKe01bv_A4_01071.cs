using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class repBangKe01bv_A4_01071 : DevExpress.XtraReports.UI.XtraReport
    {
        public repBangKe01bv_A4_01071()
        {
            InitializeComponent();
        }
        public void BindingData()
        {
          
          
            colTenDV.DataBindings.Add("Text", DataSource, "TenDV");
            colDonVI.DataBindings.Add("Text", DataSource, "Donvi");
            colDonGia.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[1];
            
                colSoLuong.DataBindings.Add("Text", DataSource, "SoLuong").FormatString = DungChung.Bien.FormatString[0];
            
            colThanhTien.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString=DungChung.Bien.FormatString[1];
           
            colNhomDV.DataBindings.Add("Text", DataSource, "TenNhom");            
            colThanhTienG.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];
           
            colThanhTienrep.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];
           
            colThanhTienG.Summary.FormatString = DungChung.Bien.FormatString[1];
            colThanhTienrep.Summary.FormatString = DungChung.Bien.FormatString[1];
           
                celSoLuongG2.DataBindings.Add("Text", DataSource, "abc");
                celSoLuongT.DataBindings.Add("Text", DataSource, "abc");
            
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
           
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            NguoiLapBieu.Value = DungChung.Bien.NguoiLapBieu;
            GDBH.Value = DungChung.Bien.GiamDinhBH;
            KeToanVP.Value = DungChung.Bien.KeToanVP;
            celGiamDoc.Text = DungChung.Bien.GiamDoc;
           
            //if (koBH.Value != null && koBH.Value.ToString().ToUpper().Contains("X"))
            //{
                
            //    txtTT.Text =       "- Tổng chi phí đợt điều trị: " + Convert.ToDouble(Tongtien.Value).ToString("##,###") + " - Bằng chữ: " + DungChung.Ham.DocTienBangChu(Convert.ToDouble(Tongtien.Value), " đồng!");
            //    //txtTienBH.Text = "- Số tiền thu trực tiếp: " + Math.Round(Convert.ToDouble(ThuTT.Value), 3).ToString("##,###") + " - Bằng chữ: " + DungChung.Ham.DocTienBangChu(Convert.ToDouble(ThuTT.Value), " đồng!");
            //   double _tienconTT= Math.Round(Convert.ToDouble(Tongtien.Value) - Convert.ToDouble(ThuTT.Value));
            //   //txtTienBN.Text = "- Số tiền còn phải thanh toán: " + _tienconTT.ToString("##,###") + " - Bằng chữ: " + DungChung.Ham.DocTienBangChu(_tienconTT, " đồng!");
            //    double _tienchenh = Math.Round(Convert.ToDouble(TienBN.Value) - Convert.ToDouble(TamThu.Value), 3);
               
            //    if (_tienchenh >= 0)
            //        txtTienBN.Text = "- Số tiền người bệnh trả: " + _tienchenh.ToString("##,###") + " - Bằng chữ: " + DungChung.Ham.DocTienBangChu(_tienchenh, " đồng!");
            //    else
            //    {
            //        _tienchenh = _tienchenh * (-1);
            //        txtTienBN.Text = "- Số tiền người bệnh nhận lại: " + _tienchenh.ToString("##,###") + " - Bằng chữ: " + DungChung.Ham.DocTienBangChu(_tienchenh, " đồng!");
            //    }
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
