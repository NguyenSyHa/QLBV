using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
namespace QLBV.BaoCao
{
    public partial class PhieutrathuocGNHTT : DevExpress.XtraReports.UI.XtraReport
    {
        public PhieutrathuocGNHTT()
        {
            InitializeComponent();
        }
        public void BindingData()
        {
            colTenHH.DataBindings.Add("Text", DataSource, "TenDV");
            txtsoluongso.DataBindings.Add("Text", DataSource, "SoLuong").FormatString = "{0:0,0}";
            if (DungChung.Bien.MaBV == "24009")
                colsoluongyc.DataBindings.Add("Text", DataSource, "SoLuong").FormatString = "{0:0,0}";
            colDVT.DataBindings.Add("Text", DataSource, "DonVi");

        }
        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            Benhvien.Value = DungChung.Bien.TenCQ;
            Boyte.Value = DungChung.Bien.TenCQCQ;

           
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            if (Ngaythang.Value != null && Ngaythang.Value.ToString() != "")
                txtNgayThang.Text = "Ngày ... tháng ... năm 20...";
                //txtNgayThang.Text = "Ngày " + System.DateTime.Now.Day + " tháng " + System.DateTime.Now.Month + " năm " + System.DateTime.Now.Year;
            txtNguoiLapBieu.Text = DungChung.Bien.NguoiLapBieu;
            txtTruongKhoaLS.Text = DungChung.Bien.TruongKhoaLS;
            colTruongKDuoc.Text = DungChung.Bien.TruongKhoaDuoc;
        }
        int _soPL = 0;
        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            string _tenRG = "";
            if (DungChung.Bien.MaBV != "24009")
            {
                if (GetCurrentColumnValue("SoLuong") != null && GetCurrentColumnValue("SoLuong").ToString() != "")
                {
                    double _sluong = Convert.ToDouble(GetCurrentColumnValue("SoLuong"));
                    colsoluongyc.Text = DungChung.Ham.DocTienBangChu(_sluong, "");
                     if (GetCurrentColumnValue("TenRG") != null && GetCurrentColumnValue("TenRG").ToString() != "")
                    _tenRG = GetCurrentColumnValue("TenRG").ToString();
                    if (_tenRG == "Thuốc gây nghiện")
                        colsoluongyc.Text = DungChung.Ham.DocTienBangChu(_sluong, "");
                    else
                    {
                        if (DungChung.Bien.MaBV == "30004")
                        {
                            string soluongso = "";
                            if (_sluong.ToString().Length == 1)
                                soluongso = "0" + _sluong;
                            else
                                soluongso = _sluong.ToString();
                            colsoluongyc.Text = soluongso;
                        }
                        else
                        {
                        colsoluongyc.Text = DungChung.Ham.DocTienBangChu(_sluong, "");
                        }
                    }
                }
            }
           
        }

        private void ReportHeader_BeforePrint_1(object sender, CancelEventArgs e)
        {
            txtTenBV.Text = DungChung.Bien.TenCQ;
            txtTenCQCQ.Text = DungChung.Bien.TenCQCQ;
            if (this.SoPL.Value != null && this.SoPL.Value.ToString() != "")
            {
                _soPL = Convert.ToInt32(this.SoPL.Value.ToString());
            }
        }

        private void PageHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "30004")
            {
                xrLabel10.Text = "Trả";
                xrLabel11.Text = "Nhập";
            }
        }
    }
     
}
