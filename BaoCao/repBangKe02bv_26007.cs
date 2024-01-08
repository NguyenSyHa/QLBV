using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class repBangKe02bv_26007 : DevExpress.XtraReports.UI.XtraReport
    {
        public repBangKe02bv_26007()
        {
            InitializeComponent();
        }

        public repBangKe02bv_26007(System.Collections.Generic.List<int> lMaThuocHoiChan)
        {
            // TODO: Complete member initialization
            this.lMaThuocHoiChan = lMaThuocHoiChan;
            InitializeComponent();
        }
        public void BindingData()
        {
            //int _size = 10;
            //if (DungChung.Bien.MaBV == "30009")
            //{
            //    _size = 10;
            //}
            //colTenDV.Font = new Font("Times New Roman", _size);
            //colDonVI.Font = new Font("Times New Roman", _size);
            //colDonGia.Font = new Font("Times New Roman", _size);
            //colSoLuong.Font = new Font("Times New Roman", _size);
            //colThanhTien.Font = new Font("Times New Roman", _size);
            //colBHYT.Font = new Font("Times New Roman", _size);
            //colBNhan.Font = new Font("Times New Roman", _size);
            //colNhomDV.Font = new Font("Times New Roman", _size, System.Drawing.FontStyle.Bold);
            //colTrongDMkt.Font = new Font("Times New Roman", _size);
            //colThanhTienG.Font = new Font("Times New Roman", _size, System.Drawing.FontStyle.Bold);
            //colBNhanG.Font = new Font("Times New Roman", _size, System.Drawing.FontStyle.Bold);
            //colBHYTg.Font = new Font("Times New Roman", _size, System.Drawing.FontStyle.Bold);
            //colThanhTienrep.Font = new Font("Times New Roman", _size, System.Drawing.FontStyle.Bold);
            //colBHYTrep.Font = new Font("Times New Roman", _size, System.Drawing.FontStyle.Bold);
            //colBNhanrep.Font = new Font("Times New Roman", _size, System.Drawing.FontStyle.Bold);
            //colNhomDVthuoc.Font = new Font("Times New Roman", _size, System.Drawing.FontStyle.Bold);
            //colTrongDM.Font = new Font("Times New Roman", _size, System.Drawing.FontStyle.Bold);
            //colTD_Cong.Font = new Font("Times New Roman", _size, System.Drawing.FontStyle.Bold);
            //colTD_TongCong.Font = new Font("Times New Roman", _size, System.Drawing.FontStyle.Bold);
            lblMaDV.DataBindings.Add("Text", DataSource, "MaDV");
            colTenDV.DataBindings.Add("Text", DataSource, "TenDV");
            celMaQD.DataBindings.Add("Text", DataSource, "MaQD");
            celSoQD.DataBindings.Add("Text", DataSource, "SoQD");
            colDonVI.DataBindings.Add("Text", DataSource, "Donvi");
            colDonGia.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[1];
            colSoLuong.DataBindings.Add("Text", DataSource, "SoLuong").FormatString = DungChung.Bien.FormatString[0];
            colThanhTien.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];
            colBHYT.DataBindings.Add("Text", DataSource, "TienBH").FormatString = DungChung.Bien.FormatString[1];
            colBNhan.DataBindings.Add("Text", DataSource, "TienBN").FormatString = DungChung.Bien.FormatString[1];
            colNhomDV.DataBindings.Add("Text", DataSource, "TenNhom").FormatString = DungChung.Bien.FormatString[1];
            colTrongDMkt.DataBindings.Add("Text", DataSource, "TrongBH").FormatString = DungChung.Bien.FormatString[1];
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
            }
            else
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
                    colTrongDM.Text = "Ngoài danh mục BHYT";
                }
                else
                {
                    if (TrongDM == 0)
                    {
                        colTrongDM.Text = "Trong danh mục BHYT";
                    }
                }

            }
            else
            {
                e.Cancel = true;
            }
        }
        int sttgroup2 = 1;
        private System.Collections.Generic.List<int> lMaThuocHoiChan = new System.Collections.Generic.List<int>();
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

            if (this.NgayVao.Value != null)
            {
                if (this.NgayVao.Value.ToString().Length > 35)
                    xrLabel60.Font = new Font("Times New Roman", 8);
                else if (this.NgayVao.Value.ToString().Length > 30)
                    xrLabel60.Font = new Font("Times New Roman", 10);
            }
            if (this.NgayRa.Value != null)
            {
                if (this.NgayRa.Value.ToString().Length > 35)
                    xrLabel23.Font = new Font("Times New Roman", 8);
                else if (this.NgayRa.Value.ToString().Length > 30)
                    xrLabel23.Font = new Font("Times New Roman", 10);
            }
            if (DungChung.Bien.MaBV == "26007")
            { xrTable10.Visible = true; }
            else
                xrTable10.Visible = false;
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {

            txtTT.Text = "- Tổng chi phí đợt điều trị: " + DungChung.Ham.DocTienBangChu(Math.Round(Convert.ToDouble(Tongtien.Value), 0), " đồng!");
            txtTienBN.Text = "- Số tiền người bệnh trả: " + DungChung.Ham.DocTienBangChu(Math.Round(Convert.ToDouble(TienBN.Value), 0), " đồng!");
            txtTienBH.Text = "- Số tiền quỹ BHYT thanh toán: " + DungChung.Ham.DocTienBangChu(Math.Round(Convert.ToDouble(TienBH.Value), 0), " đồng!");
            txtNguoiLapBieu.Text = DungChung.Bien.NguoiLapBieu;
            if (coBH.Value != null && coBH.Value.ToString().Length > 0)
                txtTenKeToanVP.Text = DungChung.Bien.KeToanVPnt;
            else
                txtTenKeToanVP.Text = DungChung.Bien.KeToanVPdv;
            txtTenGiamDinhBH.Text = DungChung.Bien.GiamDinhBH2;
            if (DungChung.Bien.MaBV == "12121")
                txtNguoiBenh.Text = tenBN.Value == null ? "" : tenBN.Value.ToString();
            if (koBH.Value != null && koBH.Value.ToString().ToUpper().Contains("X"))
            {
                colGiamDinhBH.Visible = false;
                txtTenGiamDinhBH.Visible = false;
                txtNgayGD.Visible = false;
                kygdbh.Visible = false;
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
            if (DungChung.Bien.MaBV == "33080")
            {
                colKetoanVP2.Visible = true;
                kyktvp.Visible = true;
                txtTenKTVP2.Visible = true;
                ColGiamDInhBH2.Visible = true;
                txtTenGDBH2.Visible = true;
                kygdbh2.Visible = true;
                txtNgayGD.Visible = false;
                colGiamDinhBH.Visible = false;
                kygdbh.Visible = false;
                txtTenGiamDinhBH.Visible = false;
                colKeToanVP.Text = "Đại diện cơ sở khám, chữa bệnh \n(Ký. ghi rõ họ tên)";
                txtTenKeToanVP.Text = "";
                txtTenGDBH2.Text = DungChung.Bien.GiamDinhBH2;
                txtTenKTVP2.Text = DungChung.Bien.KeToanVP;
            }
            else
            {

            }
            if (DungChung.Bien.MaBV == "24208")
            {
                colKeToanVP.Text = "Trưởng trạm";
                txtTenKeToanVP.Text = DungChung.Bien.GiamDoc;
            }
            if (DungChung.Bien.MaBV == "04256")
            {

                colKetoanVP2.Visible = true;
                kyktvp.Visible = true;
                colKetoanVP2.Text = "Đại diện CSKCB";
            }
        }

        private void xrTable5_BeforePrint(object sender, CancelEventArgs e)
        {

        }

        private void GroupFooter2_BeforePrint(object sender, CancelEventArgs e)
        {
            colTD_Cong.Text = "Cộng " + (sttgroup2 - 1).ToString() + ": ";
        }
        int sotttv = 1;
        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
              if (this.GetCurrentColumnValue("MaDV") != null)
              {
                  int madv = Convert.ToInt32(this.GetCurrentColumnValue("MaDV").ToString());
                  if(lMaThuocHoiChan.Count > 0 && lMaThuocHoiChan.Contains(madv))
                  {
                      colTenDV.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

                  }
                  else
                  {
                      colTenDV.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                  }
              }
              if (DungChung.Bien.MaBV == "20001")
              {
                  celsottdv.Text = sotttv.ToString() + ".";
                  sotttv++;
              }
              else
              {
                  celsottdv.Text = "-";
              }
        }

    }
}
