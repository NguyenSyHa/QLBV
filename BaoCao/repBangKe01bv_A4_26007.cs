using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class repBangKe01bv_A4_26007 : DevExpress.XtraReports.UI.XtraReport
    {
        public repBangKe01bv_A4_26007()
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
            celMaQD.DataBindings.Add("Text", DataSource, "MaQD");
            celMaTT37.DataBindings.Add("Text", DataSource, "SoQD");
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
            sttdv = 1;
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
            txtTT.Text = "- Tổng chi phí đợt điều trị: " + DungChung.Ham.DocTienBangChu(Convert.ToDouble(Tongtien.Value), " đồng.");
            txtTienBN.Text = "- Số tiền người bệnh trả: " + DungChung.Ham.DocTienBangChu(Convert.ToDouble(TienBN.Value), " đồng.");
            txtTienBH.Text = "- Số tiền quỹ BHYT thanh toán: " + DungChung.Ham.DocTienBangChu(Convert.ToDouble(TienBH.Value), " đồng.");
            txtNguoiLapBieu.Text = DungChung.Bien.NguoiLapBieu;
            txtGiamDinhBH.Text = DungChung.Bien.GiamDinhBH;
            txtGDBH2.Text = DungChung.Bien.GiamDinhBH; ;
            if(coBH.Value!=null && coBH.Value.ToString().Length>0)
            txtKeToanVP.Text = DungChung.Bien.KeToanVP;
            else
            txtKeToanVP.Text = DungChung.Bien.KeToanVPdv;
            if (DungChung.Bien.MaBV=="12121")
                txtNguoiBenh.Text = tenBN.Value==null?"": tenBN.Value.ToString() ;
            if (koBH.Value != null && koBH.Value.ToString().ToUpper().Contains("X"))
            {
                txtGDBH1.Visible = false;
                txtGiamDinhBH.Visible = false;
                txtGDBH1ky.Visible = false;
                txtNgayGD.Visible = false;
                txtTT.Text =       "- Tổng chi phí đợt điều trị: " + Convert.ToDouble(Tongtien.Value).ToString("##,###") + " - Bằng chữ: " + DungChung.Ham.DocTienBangChu(Convert.ToDouble(Tongtien.Value), " đồng!");
                //txtTienBH.Text = "- Số tiền thu trực tiếp: " + Math.Round(Convert.ToDouble(ThuTT.Value), 3).ToString("##,###") + " - Bằng chữ: " + DungChung.Ham.DocTienBangChu(Convert.ToDouble(ThuTT.Value), " đồng!");
               double _tienconTT= Math.Round(Convert.ToDouble(Tongtien.Value) - Convert.ToDouble(ThuTT.Value));
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
            if (DungChung.Bien.MaBV == "24208")
            {
                colKTVP.Text = "Trưởng trạm";
                txtKeToanVP.Text = DungChung.Bien.GiamDoc;
            }
            if (DungChung.Bien.MaBV == "04256")
            {
                txtDaiDien.Visible = false;
                txtDaiDien_KT.Visible = false;
            }
            if (DungChung.Bien.MaBV == "33080") {
                txtDaiDien.Text = "Kế toán viện phí";
                txtKeToanVP.Visible = false;
                txtDaiDien_KT.Visible = true;
                colKTVP.Text = "Đại diện cơ sở khám, chữa bệnh";
                txtDaiDien.Visible = true;
                txtKeToanVP2.Visible = true;
                if (coBH.Value != null && coBH.Value.ToString().Length > 0)
                    txtKeToanVP2.Text = DungChung.Bien.KeToanVP;
                else
                    txtKeToanVP2.Text = DungChung.Bien.KeToanVPdv;
            }
            if (DungChung.Bien.MaBV == "33080") {
                txtNgayGD.Visible = false;
                txtNgayGD2.Visible = true;
                txtGDBH2.Visible = true;
                txtGDBH21.Visible = true;
                txtGDBH2ky.Visible = true;
                txtGDBH1.Visible = false;
                txtGDBH1ky.Visible = false;
                txtGiamDinhBH.Visible = false;
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
