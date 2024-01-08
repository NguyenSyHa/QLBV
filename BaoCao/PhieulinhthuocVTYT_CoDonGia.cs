using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class PhieulinhthuocVTYT_CoDonGia : DevExpress.XtraReports.UI.XtraReport
    {
        int _dongy = 0;
        public PhieulinhthuocVTYT_CoDonGia() 
        {
            InitializeComponent();
        }
        public PhieulinhthuocVTYT_CoDonGia(int dy)
        {
            InitializeComponent();
            _dongy = dy;
        }
        public void BindingData()
        {
            if (DungChung.Bien.MaBV == "27022")
            {
                GroupHeader3.Visible = false;
                GroupHeader2.Visible = false;
                SbDetail24012.Visible = false;
                SbVTYT24012.Visible = false;
                Sb24012.Visible = false;
                SbVTYT3_24012.Visible = false;
                sbthuocgaynghien.Visible = false;
            }
            double _sluong = Convert.ToDouble(GetCurrentColumnValue("SoLuong"));
            string _fomat = DungChung.Bien.FormatString[0];
            if (DungChung.Bien.MaBV == "24009" || DungChung.Bien.MaBV == "30003")
                _fomat = "{0:00}";
            else
                _fomat = "{0:##,###.##}";
            colTenHH.DataBindings.Add("Text", DataSource, "TenDV");
            if (DungChung.Bien.MaBV == "24009" || DungChung.Bien.MaBV == "30003")
            {
                colsoluongyc.DataBindings.Add("Text", DataSource, "SoLuong").FormatString = _fomat;
            }
            else {
                colsoluongyc.DataBindings.Add("Text", DataSource, "SoLuong");
            }
            if (DungChung.Bien.MaBV == "24009")
                colsoluongtp.DataBindings.Add("Text", DataSource, "SoLuong").FormatString = _fomat;//.FormatString = DungChung.Bien.FormatString[0];
            if (DungChung.Bien.MaBV == "30002" || DungChung.Bien.MaBV == "30009" || DungChung.Bien.MaBV == "20001")
                colsoluongtp.DataBindings.Add("Text", DataSource, "SoLuong").FormatString = _fomat; ;//.FormatString = DungChung.Bien.FormatString[0];

            if (DungChung.Bien.MaBV == "24012" || (txtTitle.Text.ToUpper() == "PHIẾU LĨNH THUỐC" || txtTitle.Text.ToUpper() == "PHIẾU LĨNH THUỐC TRẺ EM" || txtTitle.Text.ToUpper() == "PHIẾU LĨNH THUỐC ĐÔNG Y" || txtTitle.Text.ToUpper() == "PHIẾU LĨNH THUỐC THÀNH PHẨM GÂY NGHIỆN, THUỐC THÀNH PHẨM HƯỚNG TÂM THẦN, THUỐC THÀNH PHẨM TIỀN CHẤT"))
            {
                colMaDV1.DataBindings.Add("Text", DataSource, "MaTam");
                colsoluongyc1.DataBindings.Add("Text", DataSource, "SoLuong");
                 colsoluongtp1.DataBindings.Add("Text", DataSource, "SoLuong");
                colDVT1.DataBindings.Add("Text", DataSource, "DonVi");
                colTenHH1.DataBindings.Add("Text", DataSource, "TenDV");
                colHamluong.DataBindings.Add("Text", DataSource, "HamLuong");
            }

            else if (DungChung.Bien.MaBV == "24012" || (txtTitle.Text.ToUpper() == "PHIẾU LĨNH VẬT TƯ Y TẾ TIÊU HAO" || txtTitle.Text.ToUpper() == "PHIẾU LĨNH HÓA CHẤT"))
            {
                colMaDV2.DataBindings.Add("Text", DataSource, "MaTam");
                colsoluongyc2.DataBindings.Add("Text", DataSource, "SoLuong");
                colsoluongtp2.DataBindings.Add("Text", DataSource, "SoLuong");
                colDVT2.DataBindings.Add("Text", DataSource, "DonVi");
                colTenHH2.DataBindings.Add("Text", DataSource, "TenDV");
            }
            //colTsongluongG.DataBindings.Add("Text", DataSource, "SoLuong").FormatString=DungChung.Bien.FormatString[1];
            colDVT.DataBindings.Add("Text", DataSource, "DonVi");
            colTenBN.DataBindings.Add("Text", DataSource, "TenBNhan");
            colDiaChi.DataBindings.Add("Text", DataSource, "DChi");
            colTuoi.DataBindings.Add("Text", DataSource, "Tuoi");
            colMaDV.DataBindings.Add("Text", DataSource, "MaTam");
            colDonGia.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[1];
            colThanhTien.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];
            colThanhTien_R.DataBindings.Add("Text", DataSource, "ThanhTien");
            colThanhTien_R.Summary.FormatString = DungChung.Bien.FormatString[1];
            GroupHeader1.GroupFields.Add(new GroupField("MaBNhan"));

        }
        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            QLBV_Database.QLBVEntities _data1 = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

            int _soPL = 0;
            if (this.SoPL.Value != null && this.SoPL.Value.ToString() != "")
            {
                _soPL = Convert.ToInt32(this.SoPL.Value.ToString());
            }

            var dt3 = (from bn in _data1.BenhNhans
                       join dt in _data1.DThuocs on bn.MaBNhan equals dt.MaBNhan
                       join dtct in _data1.DThuoccts.Where(p => p.SoPL == _soPL) on dt.IDDon equals dtct.IDDon
                       select dtct.NgayNhap).ToList();
            


            txtTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            txtTenCQCQ.Text= DungChung.Bien.TenCQCQ.ToUpper();
            if (_dongy == 6)
                GroupHeader1.Visible = true;
            if (DungChung.Bien.MaBV == "30004")
            {
                
                var gc = (from bn in _data1.BenhNhans
                            join dt in _data1.DThuocs on bn.MaBNhan equals dt.MaBNhan
                            join dtct in _data1.DThuoccts.Where(p => p.SoPL == _soPL) on dt.IDDon equals dtct.IDDon
                            select bn.MaDTuong).ToList();
                if (gc.Count > 0)
                {
                    if (gc.Count == gc.Where(p => p == "TE").ToList().Count)
                    {
                        this.Chuthich.Value = "Trẻ em " + this.Chuthich.Value;
                    }
                }
            }
            if (DungChung.Bien.MaBV == "24012" || (txtTitle.Text.ToUpper() == "PHIẾU LĨNH THUỐC" || txtTitle.Text.ToUpper() == "PHIẾU LĨNH THUỐC TRẺ EM" || txtTitle.Text.ToUpper() == "PHIẾU LĨNH THUỐC ĐÔNG Y"))
            { 
                GroupFooter1.Visible = true;
                GroupHeader2.Visible = true;
                Sb24012.Visible = true;
                GroupHeader3.Visible = false;
                SbVTYT24012.Visible = false;
                SbVTYT3_24012.Visible = false;
                SubBand1.Visible = false;
                GroupHeader1.Visible = false;
                PageHeader.Visible = false;
                ReportFooter.Visible = false;
                sbthuocgaynghien.Visible = false;
                txtNgayThang1.Text = "Ngày " + dt3.First().Value.Day +  " tháng " + dt3.First().Value.Month + " năm " + dt3.First().Value.Year; 
            }
            else if (DungChung.Bien.MaBV == "24012" || txtTitle.Text == "PHIẾU LĨNH THUỐC THÀNH PHẨM GÂY NGHIỆN, THUỐC THÀNH PHẨM HƯỚNG TÂM THẦN, THUỐC THÀNH PHẨM TIỀN CHẤT")
            {
                GroupHeader2.Visible = true;
                Sb24012.Visible = false;
                GroupHeader3.Visible = false;
                SbVTYT24012.Visible = false;
                SbVTYT3_24012.Visible = false;
                SubBand1.Visible = false;
                GroupHeader1.Visible = false;
                PageHeader.Visible = false;
                ReportFooter.Visible = false;
                sbthuocgaynghien.Visible = true;
            }
            else if (DungChung.Bien.MaBV == "24012" || (txtTitle.Text.ToUpper() == "PHIẾU LĨNH VẬT TƯ Y TẾ TIÊU HAO" || txtTitle.Text.ToUpper() == "PHIẾU LĨNH HÓA CHẤT"))
            {
                GroupHeader3.Visible = true;
                SbVTYT24012.Visible = true;
                SbVTYT3_24012.Visible = true;
                GroupHeader2.Visible = false;
                GroupHeader1.Visible = false;
                SubBand1.Visible = false;
                SbDetail24012.Visible = false;
                Sb24012.Visible = false;
                sbthuocgaynghien.Visible = false;
                txtNgayThang3.Text = "Ngày " + dt3.First().Value.Day + " tháng " + dt3.First().Value.Month + " năm " + dt3.First().Value.Year;

            }

        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            QLBV_Database.QLBVEntities _data1 = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

            int _soPL = 0;
            if (this.SoPL.Value != null && this.SoPL.Value.ToString() != "")
            {
                _soPL = Convert.ToInt32(this.SoPL.Value.ToString());
            }

            var dt4 = (from bn in _data1.BenhNhans
                       join dt in _data1.DThuocs on bn.MaBNhan equals dt.MaBNhan
                       join dtct in _data1.DThuoccts.Where(p => p.SoPL == _soPL) on dt.IDDon equals dtct.IDDon
                       select dtct.NgayNhap).ToList();

            var dt3 = (from dtct in _data1.DThuoccts.Where(p => p.SoPL == _soPL)
                       join dt in _data1.DThuocs on dtct.IDDon equals dt.IDDon
                       select dtct.NgayNhap).ToList();

            if (Ngaythang.Value != null && Ngaythang.Value.ToString() != "")
            txtNgayThang.Text = "Ngày " + dt3.FirstOrDefault().Value.Day + " tháng " + dt3.FirstOrDefault().Value.Month + " năm " + dt3.FirstOrDefault().Value.Year;


            //txtNgayThang.Text = "Ngày " + System.DateTime.Now.Day + " tháng " + System.DateTime.Now.Month + " năm " + System.DateTime.Now.Year;
            if (Ycu.Value.ToString() == "7" && DungChung.Bien.MaBV=="30009")
            {
                xrTable7.Visible = true;
            }
            else
            {
                xrTable7.Visible = false;
            
            }
          
            QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            string tenkp = "";
            if (Khoa.Value != null)
                tenkp = Khoa.Value.ToString();
            var kp = _data.KPhongs.Where(p => p.TenKP == tenkp).Select(p => p.PLoai).ToList();
            if (kp.Count > 0 && kp.First() == "Cận lâm sàng")
            {

                xrTableCell14.Text = "TRƯỞNG KHOA C.LÂM SÀNG";
            }
            else
            {
                xrTableCell14.Text = "TRƯỞNG KHOA LÂM SÀNG";
            }

            if (DungChung.Bien.MaBV == "20001")
            {
                txtNguoiNhan.Text = "NGƯỜI PHÁT";
                txtNguoiTra.Text = "NGƯỜI LĨNH";
                xrTableCell14.Text = "TRƯỞNG KHOA LÂM SÀNG";
            }

            // START HIS-1435
            if (CPloai.Value.ToString() == "5" && DungChung.Bien.MaBV == "27022")
            {
                xrTableCell1.Text = "Tên văn phòng phẩm - HC";
                xrTableCell12.Text = "NGƯỜI LĨNH";
                txtNguoiNhan.Text = "THỦ KHO";
                txtNguoiTra.Text = "TRƯỞNG KHOA, PHÒNG";
                xrTableCell14.Text = "KẾ TOÁN";
            }
            // END HIS-1435
            if (DungChung.Bien.MaBV == "24012")
            {
                txtNguoiNhan.Text = "NGƯỜI NHẬN";
                txtNguoiTra.Text = "NGƯỜI TRẢ";
            }


        }
        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            if (GetCurrentColumnValue("SoLuong") != null && GetCurrentColumnValue("SoLuong").ToString() != "")
            {
                double _sluong = Convert.ToDouble(GetCurrentColumnValue("SoLuong"));
                
                if (DungChung.Bien.MaBV == "24012" && txtTitle.Text.ToUpper() == "PHIẾU LĨNH THUỐC THÀNH PHẨM GÂY NGHIỆN")
                {
                    colsoluongtp1.Text = DungChung.Ham.DocTienBangChu(_sluong, "");
                    colsoluongyc.Text = DungChung.Ham.DocTienBangChu(_sluong, "");
                    colsoluongyc1.Text = DungChung.Ham.DocTienBangChu(_sluong, "");
                    colsoluongyc2.Text = DungChung.Ham.DocTienBangChu(_sluong, "");
                    colTenHH1.Text = "test";
                    colTenHH1.Text = "test";
                    colTenHH2.Text = "test";
                    colHamluong.Text = "test";
                }
            }
        }

        private void SbDetail24012_BeforePrint(object sender, CancelEventArgs e)
        {
            
        }

        private void SbVTYT24012_BeforePrint(object sender, CancelEventArgs e)
        {
            if (GetCurrentColumnValue("SoLuong") != null && GetCurrentColumnValue("SoLuong").ToString() != "")
            {
                double _sluong = Convert.ToDouble(GetCurrentColumnValue("SoLuong"));

                if (DungChung.Bien.MaBV == "24012" && txtTitle.Text.ToUpper() == "PHIẾU LĨNH THUỐC THÀNH PHẨM GÂY NGHIỆN")
                {
                    colsoluongtp1.Text = DungChung.Ham.DocTienBangChu(_sluong, "");
                    colsoluongyc.Text = DungChung.Ham.DocTienBangChu(_sluong, "");
                    colsoluongyc1.Text = DungChung.Ham.DocTienBangChu(_sluong, "");
                    colsoluongyc2.Text = DungChung.Ham.DocTienBangChu(_sluong, "");
                    colTenHH1.Text = "test";
                    colTenHH1.Text = "test";
                    colTenHH2.Text = "test";
                    colHamluong.Text = "test";
                }
            }
        }

        private void GroupFooter1_BeforePrint(object sender, CancelEventArgs e)
        {
            //if (DungChung.Bien.MaBV == "24012")
            //{
            //    txtNguoiNhan1.Text = "NGƯỜI NHẬN";
            //    txtNguoiTra1.Text = "NGƯỜI TRẢ";
            //    txtNguoiNhan2.Text = "NGƯỜI NHẬN";
            //    txtNguoiTra2.Text = "NGƯỜI TRẢ";
            //}
        }
    }
     
}
