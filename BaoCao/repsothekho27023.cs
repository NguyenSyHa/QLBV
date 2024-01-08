using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
using System.Collections.Generic;

namespace QLBV.BaoCao
{
    public partial class repsothekho27023 : DevExpress.XtraReports.UI.XtraReport
    {
        public repsothekho27023(List<lds> ds)
        {
            InitializeComponent();
            _lds = ds;
        }

        public class lds
        {

            int pLoai;
            int maDV;

            public int MaDV
            {
                get { return maDV; }
                set { maDV = value; }
            }
            public int PLoai
            {
                get { return pLoai; }
                set { pLoai = value; }
            }
            int maKP;

            public int MaKP
            {
                get { return maKP; }
                set { maKP = value; }
            }
            double sLN;

            public double SLN
            {
                get { return sLN; }
                set { sLN = value; }
            }
            double sLX;

            public double SLX
            {
                get { return sLX; }
                set { sLX = value; }
            }
            double donGia;
        }
        List<lds> _lds = new List<lds>();
        private void repsothekho_BeforePrint(object sender, CancelEventArgs e)
        {

        }
        double sltck = 0, thanhtienck = 0;

        private void sltdk()
        {
            double Gia = Convert.ToDouble(GetCurrentColumnValue("DonGia"));
            int _Maduoc = 0;
            if (this.GetCurrentColumnValue("MaDV") != null)
                _Maduoc = Convert.ToInt32(this.GetCurrentColumnValue("MaDV"));
            DateTime _ngaytu = Convert.ToDateTime(ngaytu.Value);
            DateTime _ngayden = Convert.ToDateTime(Ngaythang.Value);
            int _makp = 0;
            if (Khoaphong.Value != null)
                _makp = Convert.ToInt32(Khoaphong.Value);


            var b = (from a in _lds
                     where a.MaDV == _Maduoc
                     group a by new { a.PLoai, a.MaKP} into kp
                     select new
                     {
                         SLTDK = kp.Sum(p => p.SLN) - kp.Sum(p => p.SLX),
                         Makp = kp.Key.MaKP,
                     }).ToList();
            if (b != null && b.Count() > 0)
            {
                sltck = b.Sum(p => p.SLTDK);
                thanhtienck = sltck * Gia;
            }
            else
            {
                sltck = 0;
            }
        }
        private void xrTableCell19_BeforePrint(object sender, CancelEventArgs e)
        {
            
        }

        private void colSCTn_BeforePrint(object sender, CancelEventArgs e)
        {
        }
        public void BindingData()
        {
            colNgaythang.DataBindings.Add("Text", DataSource, "Ngaythang");
            colSCTn.DataBindings.Add("Text", DataSource, "SCT");
            colSCTx.DataBindings.Add("Text", DataSource, "SCT");
            colSolo.DataBindings.Add("Text", DataSource, "SoLo");
            colHandung.DataBindings.Add("Text", DataSource, "HanDung");
            colDiengiai.DataBindings.Add("Text", DataSource, "GChu");
            colDonGia.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[0];
            colTonDK.DataBindings.Add("Text", DataSource, "Soluongton").FormatString = DungChung.Bien.FormatString[0];
            colTTTonDK.DataBindings.Add("Text", DataSource, "ThanhTienTonDauKy").FormatString = DungChung.Bien.FormatString[0];
            colNhap.DataBindings.Add("Text", DataSource, "SLNhap").FormatString = DungChung.Bien.FormatString[0];
            colTTNhap.DataBindings.Add("Text", DataSource, "ThanhTienNhap").FormatString = DungChung.Bien.FormatString[0];
            colXuat.DataBindings.Add("Text", DataSource, "SLXuat").FormatString = DungChung.Bien.FormatString[0];
            colTTXuat.DataBindings.Add("Text", DataSource, "ThanhTienXuat").FormatString = DungChung.Bien.FormatString[0];
            colTonCK.DataBindings.Add("Text", DataSource, "Ton").FormatString = DungChung.Bien.FormatString[0];
            colTTTonCK.DataBindings.Add("Text", DataSource, "ThanhTienTonCK").FormatString = DungChung.Bien.FormatString[0];
            colQCPC.DataBindings.Add("Text", DataSource, "HamLuong");
            colDonVi.DataBindings.Add("Text", DataSource, "DonVi");
            colphanl.DataBindings.Add("Text", DataSource, "Phanloai");
            txtMaDV.DataBindings.Add("Text", DataSource, "MaDV");
            colMaDV.DataBindings.Add("Text", DataSource, "MaDV");
            colTenDV.DataBindings.Add("Text", DataSource, "TenDV");
            GroupHeader1.GroupFields.Add(new GroupField("MaDV"));
            coltongN.DataBindings.Add("Text", DataSource, "SLNhap").FormatString = DungChung.Bien.FormatString[1];
            coltongX.DataBindings.Add("Text", DataSource, "SLXuat").FormatString = DungChung.Bien.FormatString[1];
        }
        private void xrTable2_BeforePrint(object sender, CancelEventArgs e)
        {

        }
        private void colSoluongton_BeforePrint(object sender, CancelEventArgs e)
        {
            double TT = 0;
            double Gia = Convert.ToDouble(GetCurrentColumnValue("DonGia"));
            colTonDK.Text = string.Format(DungChung.Bien.FormatString[0], sltck);
            TT = sltck * Gia;
            colTTTonDK.Text = string.Format(DungChung.Bien.FormatString[0], TT);
        }
        string rong = "";
        private void colPhanloai_BeforePrint(object sender, CancelEventArgs e)
        {
            if (colphanl.Text == "1")
            {
                colSCTx.Text = rong;

            }
            else
            {
                colSCTn.Text = rong;
            }
            if (this.GetCurrentColumnValue("Ngaythang") != null)
            {
                string nt = this.GetCurrentColumnValue("Ngaythang").ToString();
                if (nt.Length < 10)
                {
                    colPhanloai.Text = nt;
                }
                else colPhanloai.Text = nt.Substring(0, 10);
            }
        }

        private void colNgaythang_BeforePrint(object sender, CancelEventArgs e)
        {
        }

        private void colNgayt_BeforePrint(object sender, CancelEventArgs e)
        {

        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
        }

        int _break = 0;
        private void GroupHeader1_BeforePrint(object sender, CancelEventArgs e)
        {
            sltdk();

            if (_break == 0)
            {
                xrPageBreak1.Visible = false;
                xrLine2.Visible = false;
            }
            else
            {
                xrLine2.Visible = true;
                xrPageBreak1.Visible = Convert.ToBoolean(PhanTrang.Value);
            }
            _break++;
            if (DungChung.Bien.MaBV == "12122")
                GroupFooter1.Visible = true;
        }

        private void GroupFooter1_BeforePrint(object sender, CancelEventArgs e)
        {

        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
        }

        private void colTonCK_BeforePrint(object sender, CancelEventArgs e)
        {
            double TT = 0;
            double Gia = Convert.ToDouble(GetCurrentColumnValue("DonGia"));
            if (!string.IsNullOrEmpty(colNhap.Text))
            {
                sltck += +Convert.ToInt32(GetCurrentColumnValue("SLNhap"));
            }
            if (!string.IsNullOrEmpty(colXuat.Text))
            {
                sltck += -Convert.ToInt32(GetCurrentColumnValue("SLXuat"));
            }
            colTonCK.Text = string.Format(DungChung.Bien.FormatString[0], sltck);
            TT = sltck * Gia;
            colTTTonCK.Text = string.Format(DungChung.Bien.FormatString[0], TT);
        }
    }
}
