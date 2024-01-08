using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class PhieulinhthuocGNHTT2lien_27023 : DevExpress.XtraReports.UI.XtraReport
    {
        public PhieulinhthuocGNHTT2lien_27023()
        {
            InitializeComponent();
        }
        public void BindingData()
        {
            colTenHH.DataBindings.Add("Text", DataSource, "TenDV");
            txtsoluongso.DataBindings.Add("Text", DataSource, "SoLuong").FormatString = DungChung.Bien.FormatString[1];
            if (DungChung.Bien.MaBV == "24009")
                colsoluongyc.DataBindings.Add("Text", DataSource, "SoLuong").FormatString = DungChung.Bien.FormatString[1];
            colDVT.DataBindings.Add("Text", DataSource, "DonVi");
            colTenHH2.DataBindings.Add("Text", DataSource, "TenDV");
            txtsoluongso.DataBindings.Add("Text", DataSource, "SoLuong").FormatString = DungChung.Bien.FormatString[1];
            //if (DungChung.Bien.MaBV == "24009")
            //    colsoluongyc.DataBindings.Add("Text", DataSource, "SoLuong").FormatString = DungChung.Bien.FormatString[1];
            colDVT2.DataBindings.Add("Text", DataSource, "DonVi");
            celSoLo.DataBindings.Add("Text", DataSource, "SoLo");
            celHanDung.DataBindings.Add("Text", DataSource, "HanDung").FormatString = "{0: dd/MM/yyyy}";
            celSoLo2.DataBindings.Add("Text", DataSource, "SoLo");
            celHanDung2.DataBindings.Add("Text", DataSource, "HanDung").FormatString = "{0: dd/MM/yyyy}";

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
            colNguoiLapBieu2.Text = DungChung.Bien.NguoiLapBieu;
            colTruongKLS.Text = DungChung.Bien.TruongKhoaLS;
            colTruongKD.Text = DungChung.Bien.TruongKhoaDuoc;
            colTruongKLS2.Text = DungChung.Bien.TruongKhoaLS;
            colTruongKD2.Text = DungChung.Bien.TruongKhoaDuoc;
        }
        string _tenRG = "";
        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV != "24009" )
            {
                if (GetCurrentColumnValue("TenRG") != null && GetCurrentColumnValue("TenRG").ToString() != "")
                    _tenRG = GetCurrentColumnValue("TenRG").ToString();
                if (GetCurrentColumnValue("SoLuong") != null && GetCurrentColumnValue("SoLuong").ToString() != "")
                {
                    double _sluong = Convert.ToDouble(GetCurrentColumnValue("SoLuong"));
                    if (_tenRG == "Thuốc gây nghiện")
                    {
                        colsoluongyc.Text = DungChung.Ham.DocTienBangChu(_sluong, "");
                        colsoluongyc2.Text = DungChung.Ham.DocTienBangChu(_sluong, "");
                    }
                    else
                    {

                        string soluongso = "";
                        if (_sluong.ToString().Length == 1)
                            soluongso = "0" + _sluong;
                        else
                            soluongso = _sluong.ToString();
                        colsoluongyc.Text = soluongso;
                        colsoluongyc2.Text = soluongso;

                    }
                   
                    if(DungChung.Bien.MaBV=="30002")
                        colsoluongtp.Text=DungChung.Ham.DocTienBangChu(_sluong, "");
                }
            }
        }

        private void ReportHeader_BeforePrint_1(object sender, CancelEventArgs e)
        {
            txtTenBV.Text = DungChung.Bien.TenCQ;
            txtTenCQ2.Text = DungChung.Bien.TenCQ;
        //    txtTenCQCQ.Text = DungChung.Bien.TenCQCQ;
        }
    }
     
}
