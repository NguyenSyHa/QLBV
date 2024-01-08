using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class repBangKe01bv : DevExpress.XtraReports.UI.XtraReport
    {
        public repBangKe01bv()
        {
            InitializeComponent();
        }
        public void BindingData()
        {
            colTenDV.DataBindings.Add("Text", DataSource, "TenDV");
            colDonVI.DataBindings.Add("Text", DataSource, "Donvi");
            colDonGia.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[1];
            colSoLuong.DataBindings.Add("Text", DataSource, "SoLuong").FormatString = DungChung.Bien.FormatString[0];
            colThanhTien.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];
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
            if (DungChung.Bien.MaBV == "12122")
            {
                celSoLuongG2.DataBindings.Add("Text", DataSource, "SoLuong");
                celSoLuongT.DataBindings.Add("Text", DataSource, "SoLuong");
            } else
            {
                celSoLuongG2.DataBindings.Add("Text", DataSource, "abc");
                celSoLuongT.DataBindings.Add("Text", DataSource, "abc");
            }
            //colSTT.DataBindings.Add("Text", DataSource, "TieuNhom");
            colNhomDVthuoc.DataBindings.Add("Text", DataSource, "TenNhom");
            GroupHeader2.GroupFields.Add(new GroupField("STT"));
            GroupHeader1.GroupFields.Add(new GroupField("TrongBH"));
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
            int TrongDM = this.GetCurrentColumnValue<int>("TrongBH");
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
            sotttv = 1;
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

            GroupHeader1.Visible = false;
            if (DungChung.Bien.MaBV == "30002" || DungChung.Bien.MaBV == "30009" || DungChung.Bien.MaBV == "30012")
                GroupFooter2.Visible = true;
            else GroupFooter2.Visible = false;

        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            txtTT.Text = "- Tổng chi phí đợt điều trị: " + DungChung.Ham.DocTienBangChu(Convert.ToDouble(Tongtien.Value), " đồng.");
            txtTienBN.Text = "- Số tiền người bệnh trả: " + DungChung.Ham.DocTienBangChu(Convert.ToDouble(TienBN.Value), " đồng.");
            txtTienBH.Text = "- Số tiền quỹ BHYT thanh toán: " + DungChung.Ham.DocTienBangChu(Convert.ToDouble(TienBH.Value), " đồng.");
            txtNguoiLapBieu.Text = DungChung.Bien.NguoiLapBieu;
            if (coBH.Value != null && coBH.Value.ToString().Length > 0)
                txtKeToanVP.Text = DungChung.Bien.KeToanVP;
            else
                txtKeToanVP.Text = DungChung.Bien.KeToanVPdv;
            txtGiamDinhBH.Text = DungChung.Bien.GiamDinhBH;
            if (DungChung.Bien.MaBV == "12121")
                txtNguoiBenh.Text = tenBN.Value == null ? "" : tenBN.Value.ToString();
            if (koBH.Value != null && koBH.Value.ToString().ToLower().Contains("x"))
            {
                xrLabel36.Visible = false;
                txtGiamDinhBH.Visible = false;
                // xrLabel32.Visible = false;
                txtNgayGD.Visible = false;
                txtTT.Text = "- Tổng chi phí đợt điều trị: " + Convert.ToDouble(Tongtien.Value).ToString("##,###") + " - Bằng chữ: " + DungChung.Ham.DocTienBangChu(Convert.ToDouble(Tongtien.Value), " đồng!");
                //txtTienBH.Text = "- Số tiền thu trực tiếp: " + Math.Round(Convert.ToDouble(ThuTT.Value), 3).ToString("##,###") + " - Bằng chữ: " + DungChung.Ham.DocTienBangChu(Convert.ToDouble(ThuTT.Value), " đồng!");
                double _tienconTT = Math.Round(Convert.ToDouble(Tongtien.Value) - Convert.ToDouble(ThuTT.Value));
                //txtTienBN.Text = "- Số tiền còn phải thanh toán: " + _tienconTT.ToString("##,###") + " - Bằng chữ: " + DungChung.Ham.DocTienBangChu(_tienconTT, " đồng!");
                double _tienchenh = Math.Round(Convert.ToDouble(TienBN.Value) - Convert.ToDouble(TamThu.Value), 3);
                txtTienBH.Text = "- Số tiền tạm thu: " + Math.Round(Convert.ToDouble(TamThu.Value), 3).ToString("##,###") + " - Bằng chữ: " + DungChung.Ham.DocTienBangChu(Convert.ToDouble(TamThu.Value), " đồng!");
                if (_tienchenh >= 0)
                    txtTienBN.Text = "- Số tiền người bệnh trả: " + _tienchenh.ToString("##,###") + " - Bằng chữ: " + DungChung.Ham.DocTienBangChu(_tienchenh, " đồng!");
                else
                {
                    _tienchenh = _tienchenh * (-1);
                    txtTienBN.Text = "- Số tiền người bệnh nhận lại: " + _tienchenh.ToString("##,###") + " - Bằng chữ: " + DungChung.Ham.DocTienBangChu(_tienchenh, " đồng!");
                }
            }
            if (DungChung.Bien.MaBV == "04256")
            {
                txtDaiDien.Visible = false;
                txtKyHT.Visible = false;
            }
            if (DungChung.Bien.MaBV == "24208")
            {
                colKTVP.Text = "Trưởng trạm";
                txtKeToanVP.Text = DungChung.Bien.GiamDoc;
            }
            //if (DungChung.Bien.MaBV == "04256")
            //{
            //    txtDaiDien.Visible = true;
            //    txtKyHT.Visible = true;

            //}
        }
        int sotttv = 1;
        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            //if (DungChung.Bien.MaBV == "06007")
            //{
            //    if (GetCurrentColumnValue("TienBN") != null && GetCurrentColumnValue("TienBN").ToString() != "" && GetCurrentColumnValue("TienBN").ToString() != "0")
            //    {

            //    }
            //    else
            //    {
            //        //String.Format("{0:0,0}", colThanhTien.Text);
            //        colBNhan.Text = "0";
            //    }
            //}
            if(DungChung.Bien.MaBV=="20001")
            {
                celsottdv.Text = sotttv.ToString() + ".";
                sotttv++;
            }
            else
            {
                celsottdv.Text = "-";
            }
        }

        private void xrTable7_BeforePrint(object sender, CancelEventArgs e)
        {
            //double stien = 0;
            //double stienBN = 0;
            //double stienBH = 0;
            //if (GetCurrentColumnValue(colThanhTienrep.Text) != null)
            //    stien = Convert.ToDouble(GetCurrentColumnValue(colThanhTienrep.Text).ToString());
            //txtTT.Text = "- Tổng chi phí đợt điều trị: " + DungChung.Ham.DocTienBangChu(stien, " đồng.");
            //if (GetCurrentColumnValue(colBNhanrep.Text) != null)
            //    stienBN = Convert.ToDouble(GetCurrentColumnValue(colBNhanrep.Text).ToString());
            //txtTienBN.Text = "- Số tiền người bệnh trả: " + DungChung.Ham.DocTienBangChu(stienBN, " đồng.");
            //if (GetCurrentColumnValue(colBHYTrep.Text) != null)
            //    stienBH = Convert.ToDouble(GetCurrentColumnValue(colBHYTrep.Text).ToString());
            //txtTienBH.Text = "- Số tiền quỹ BHYT thanh toán: " + DungChung.Ham.DocTienBangChu(stienBH, " đồng.");
        }

        private void GroupFooter2_BeforePrint(object sender, CancelEventArgs e)
        {
            colCong_GF.Text = "Cộng " + (sttgroup2 - 1).ToString() + ": ";
        }

    }
}
