using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_PLThuoctheothang : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_PLThuoctheothang()
        {
            InitializeComponent();
        }
        public void BindingData()
        {
            Tendv.DataBindings.Add("Text", DataSource, "TenDV");
            Donvi.DataBindings.Add("Text", DataSource, "DonVi");
            S1.DataBindings.Add("Text", DataSource, "SoLuong");
            S2.DataBindings.Add("Text", DataSource, "Ngay2");
            S3.DataBindings.Add("Text", DataSource, "Ngay3");
            S4.DataBindings.Add("Text", DataSource, "Ngay4");
            S5.DataBindings.Add("Text", DataSource, "Ngay5");
            ThanhTien.DataBindings.Add("Text", DataSource, "DonGia");
            RFThanhTien.DataBindings.Add("Text", DataSource, "DonGia");
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            xrLabel1.Text = DungChung.Bien.TenCQ;
        }

        private void Donvi_BeforePrint(object sender, CancelEventArgs e)
        {
            if (GetCurrentColumnValue("SoLuong") != null && GetCurrentColumnValue("SoLuong").ToString() != "")
            {
                double _sluong = Convert.ToDouble(GetCurrentColumnValue("SoLuong"));
                string soluongso = "";
                if (_sluong.ToString().Length == 1)
                    soluongso = "0" + _sluong;
                else
                    soluongso = _sluong.ToString();
                SL1.Text = soluongso;
            }
            if (GetCurrentColumnValue("Ngay2") != null && GetCurrentColumnValue("Ngay2").ToString() != "")
            {
                double _sluong = Convert.ToDouble(GetCurrentColumnValue("Ngay2"));
                string soluongso = "";
                if (_sluong.ToString().Length == 1)
                    soluongso = "0" + _sluong;
                else
                    soluongso = _sluong.ToString();
                SL2.Text = soluongso;
            }
            if (GetCurrentColumnValue("Ngay3") != null && GetCurrentColumnValue("Ngay3").ToString() != "")
            {
                double _sluong = Convert.ToDouble(GetCurrentColumnValue("Ngay3"));
                string soluongso = "";
                if (_sluong.ToString().Length == 1)
                    soluongso = "0" + _sluong;
                else
                    soluongso = _sluong.ToString();
                SL3.Text = soluongso;
            }
            if (GetCurrentColumnValue("Ngay4") != null && GetCurrentColumnValue("Ngay4").ToString() != "")
            {
                double _sluong = Convert.ToDouble(GetCurrentColumnValue("Ngay4"));
                string soluongso = "";
                if (_sluong.ToString().Length == 1)
                    soluongso = "0" + _sluong;
                else
                    soluongso = _sluong.ToString();
                SL4.Text = soluongso;
            }
            if (GetCurrentColumnValue("Ngay5") != null && GetCurrentColumnValue("Ngay5").ToString() != "")
            {
                double _sluong = Convert.ToDouble(GetCurrentColumnValue("Ngay5"));
                string soluongso = "";
                if (_sluong.ToString().Length == 1)
                    soluongso = "0" + _sluong;
                else
                    soluongso = _sluong.ToString();
                SL5.Text = soluongso;
            }
        }

    }
}
