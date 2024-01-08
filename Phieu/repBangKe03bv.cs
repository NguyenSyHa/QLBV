using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class repBangKe03bv : DevExpress.XtraReports.UI.XtraReport
    {
        public repBangKe03bv()
        {
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

            colTenDV.DataBindings.Add("Text", DataSource, "TenDV");
            colDonVI.DataBindings.Add("Text", DataSource, "Donvi");
            colDonGia.DataBindings.Add("Text", DataSource, "DonGia").FormatString=DungChung.Bien.FormatString[1];
          
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
        int sttgroup2 = 1;
        private void GroupHeader2_BeforePrint(object sender, CancelEventArgs e)
        {
            sotttv = 1;
            txtSoTTg2.Text = " " + sttgroup2.ToString() + ".";
            sttgroup2++;

        }


        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var qbv = data.BenhViens.Where(parameters => parameters.MaBV == DungChung.Bien.MaBV).FirstOrDefault();  
            if(qbv != null && qbv.MaChuQuan == "12001")
            {
                var qbv2 = (from bv1 in data.BenhViens.Where(parameters => parameters.MaBV == qbv.MaChuQuan) join bv2 in data.BenhViens on bv1.MaChuQuan equals bv2.MaBV
                            select new { bv1, bv2 }).FirstOrDefault();
                if (qbv2 != null)
                {
                    txtTenCQCQ.Text = qbv2.bv2.TenBV.ToUpper();
                    txtTenCQ.Text = qbv2.bv1.TenBV.ToUpper();
                }
               //DungChung.Bien.TenCQCQ.ToUpper();
                this.TenKP.Value = DungChung.Bien.TenCQ;
                lblTenCQ.Text = this.TenKP.Value.ToString();
            } 
            else
            {
                txtTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
                txtTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            }

           
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

            if (this.NgayVao.Value != null ) {
               if(this.NgayVao.Value.ToString().Length > 35)
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
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            txtTT.Text = "- Tổng chi phí đợt điều trị: " + DungChung.Ham.DocTienBangChu(Math.Round(Convert.ToDouble(Tongtien.Value),0), " đồng!");
            txtTienBN.Text = "- Số tiền người bệnh trả: " + DungChung.Ham.DocTienBangChu(Math.Round(Convert.ToDouble(TienBN.Value),0), " đồng!");
            txtTienBH.Text = "- Số tiền quỹ BHYT thanh toán: " + DungChung.Ham.DocTienBangChu(Math.Round(Convert.ToDouble(TienBH.Value),0), " đồng!");
            txtNguoiLapBieu.Text = DungChung.Bien.NguoiLapBieu;
                txtTenKeToanVP.Text = DungChung.Bien.GiamDoc;
            if (koBH.Value != null && koBH.Value.ToString().ToUpper().Contains("X"))
            {
          
                txtTT.Text = "- Tổng chi phí đợt điều trị: " + Convert.ToDouble(Tongtien.Value).ToString("##,###") + " - Bằng chữ: " + DungChung.Ham.DocTienBangChu(Convert.ToDouble(Tongtien.Value), " đồng!");
                txtTienBH.Text = "- Số tiền thu trực tiếp: " + Math.Round(Convert.ToDouble(ThuTT.Value), 3).ToString("##,###") + " - Bằng chữ: " + DungChung.Ham.DocTienBangChu(Convert.ToDouble(ThuTT.Value), " đồng!");
                double _tienconTT = Math.Round(Convert.ToDouble(Tongtien.Value) - Convert.ToDouble(ThuTT.Value));
                txtTienBN.Text = "- Số tiền còn phải thanh toán: " + _tienconTT.ToString("##,###") + " - Bằng chữ: " + DungChung.Ham.DocTienBangChu(_tienconTT, " đồng!");
                double _tienchenh = Math.Round(Convert.ToDouble(TienBN.Value) - Convert.ToDouble(TamThu.Value) - Convert.ToDouble(ThuTT.Value), 3);
                txtNguonKhac.Text = "- Số tiền tạm thu: " + Math.Round(Convert.ToDouble(TamThu.Value), 3).ToString("##,###") + " - Bằng chữ: " + DungChung.Ham.DocTienBangChu(Convert.ToDouble(TamThu.Value), " đồng!");
                if (_tienchenh >= 0)
                    txtTienTra.Text = "- Số tiền người bệnh trả: " + _tienchenh.ToString("##,###") + " - Bằng chữ: " + DungChung.Ham.DocTienBangChu(_tienchenh, " đồng!");
                else
                {
                    _tienchenh = _tienchenh * (-1);
                    txtTienTra.Text = "- Số tiền người bệnh nhận lại: " + _tienchenh.ToString("##,###") + " - Bằng chữ: " + DungChung.Ham.DocTienBangChu(_tienchenh, " đồng!");
                }
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
