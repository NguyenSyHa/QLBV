using System;
using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
using QLBV.FormNhap;

namespace QLBV.BaoCao

{
    public partial class rep_thekho : DevExpress.XtraReports.UI.XtraReport
    {
        double dongia;
        public rep_thekho(double dongia)
        {
            InitializeComponent();
            this.dongia = dongia;
        }

        private void repsothekho_BeforePrint(object sender, CancelEventArgs e)
        {

        }
        double sltck = 0;
        double sltck1 = 0;
        QLBV_Database.QLBVEntities Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void sltdk()
        {

            int _Maduoc = Madv.Value == null ? 0 : Convert.ToInt32(Madv.Value);
            DateTime _ngaytu = Convert.ToDateTime(ngaytu.Value);
            DateTime _ngayden = Convert.ToDateTime(Ngaythang.Value);
            int _Makp = Convert.ToInt32(Khoaphong.Value);
            var b1 = (from nx in Data.NhapDs.Where(p => p.MaKP == _Makp).Where(p => p.NgayNhap < _ngaytu)
                      join nxct in Data.NhapDcts.Where(p => p.MaDV == _Maduoc).Where(p => (dongia > 0 ? (p.DonGia == dongia) : true)) on nx.IDNhap equals nxct.IDNhap
                      select new
                      {
                          PLoai = nx.PLoai,
                          SLN = nxct.SoLuongN,
                          SLX = nxct.SoLuongX,
                          MaKP = nx.MaKP,
                          Ngay = nx.NgayNhap,
                      }).ToList();
            var b = (from a in b1
                     group a by new { a.PLoai, a.MaKP } into kp // mã khoa phòng
                     select new
                     {
                         SLTDK = kp.Sum(p => p.SLN) - kp.Sum(p => p.SLX),
                         Makp = kp.Key.MaKP,
                     }).ToList();
            if (b.Count > 0)
            {
                int _makp = String.IsNullOrEmpty(Khoaphong.Value.ToString()) ? 0 : Convert.ToInt32(Khoaphong.Value);
                //double sltck = b.Sum(p => p.SLTDK);
                sltck = b.Where(p => p.Makp == _makp).Sum(p => p.SLTDK);
                sltck1 = b.Where(p => p.Makp == _makp).Sum(p => p.SLTDK);
                //sltck = Math.Round(sltck, 2);
            }
        }
        //int sltdk = 0;
        string tonCuoi = "";
        private void xrTableCell19_BeforePrint(object sender, CancelEventArgs e)
        {
            if (this.GetCurrentColumnValue("SLNhap") != null)
            {

                sltck += Convert.ToDouble(GetCurrentColumnValue("SLNhap")); //Math.Round(Convert.ToDouble(GetCurrentColumnValue("SLNhap")),0);
                sltck1 += Convert.ToDouble(GetCurrentColumnValue("SLNhap"));
            }
            if (this.GetCurrentColumnValue("SLXuat") != null)
            {

                sltck += -Convert.ToDouble(GetCurrentColumnValue("SLXuat")); //Math.Round(Convert.ToDouble(GetCurrentColumnValue("SLXuat")),0);
                sltck1 += -Convert.ToDouble(GetCurrentColumnValue("SLXuat"));
            }

            var sltckFormat = String.Format("{0:0,0}", sltck);
            var sltckFormat1 = String.Format("{0:0,0}", sltck1);
            colTon.Text = sltckFormat;
            tonCuoi = sltckFormat;
        }

        private void colSCTn_BeforePrint(object sender, CancelEventArgs e)
        {
            //colDiengiai.Text = "";
        }
        public void BindingData()
        {
            colPhanloai.DataBindings.Add("Text", DataSource, "Ngaythang");
            colSCTn.DataBindings.Add("Text", DataSource, "SCTn");
            colSCTx.DataBindings.Add("Text", DataSource, "SCTx");
            colSolo.DataBindings.Add("Text", DataSource, "Solo");
            colHandung.DataBindings.Add("Text", DataSource, "Handung").FormatString = "{0:dd/MM/yyyy}";
            colSoluongton.DataBindings.Add("Text", DataSource, "Soluongton").FormatString = DungChung.Bien.FormatString[0];
            colXuat.DataBindings.Add("Text", DataSource, "SLXuat").FormatString = DungChung.Bien.FormatString[0];
            colNhap.DataBindings.Add("Text", DataSource, "SLNhap").FormatString = DungChung.Bien.FormatString[0];
            celXuatT.DataBindings.Add("Text", DataSource, "SLXuat").FormatString = DungChung.Bien.FormatString[0];
            celNhapT.DataBindings.Add("Text", DataSource, "SLNhap").FormatString = DungChung.Bien.FormatString[0];
            celXuatT1.DataBindings.Add("Text", DataSource, "SLXuat").FormatString = DungChung.Bien.FormatString[0];
            celNhapT1.DataBindings.Add("Text", DataSource, "SLNhap").FormatString = DungChung.Bien.FormatString[0];
            //LbLuykeNhap.DataBindings.Add("Text", DataSource, "SLNhap").FormatString = DungChung.Bien.FormatString[0];
            //LbLuykeXuat.DataBindings.Add("Text", DataSource, "SLXuat").FormatString = DungChung.Bien.FormatString[0];
            //colTon.DataBindings.Add("Text", DataSource, "Ton");
            colphanl.DataBindings.Add("Text", DataSource, "Phanloai");
            colDiengiai.DataBindings.Add("Text", DataSource, "GChu");


            colPhanloai1.DataBindings.Add("Text", DataSource, "Ngaythang");
            colSCTn1.DataBindings.Add("Text", DataSource, "SCTn");
            colSCTx1.DataBindings.Add("Text", DataSource, "SCTx");
            colSolo1.DataBindings.Add("Text", DataSource, "Solo");
            colHandung1.DataBindings.Add("Text", DataSource, "Handung").FormatString = "{0:dd/MM/yyyy}";
            colSoluongton1.DataBindings.Add("Text", DataSource, "Soluongton").FormatString = DungChung.Bien.FormatString[0];
            colXuat1.DataBindings.Add("Text", DataSource, "SLXuat").FormatString = DungChung.Bien.FormatString[0];
            colNhap1.DataBindings.Add("Text", DataSource, "SLNhap").FormatString = DungChung.Bien.FormatString[0];
            colMaBN.DataBindings.Add("Text", DataSource, "MaBNhan");
            colTenBN.DataBindings.Add("Text", DataSource, "TenBNhan");
            //LbLuykeNhap.DataBindings.Add("Text", DataSource, "SLNhap").FormatString = DungChung.Bien.FormatString[0];
            //LbLuykeXuat.DataBindings.Add("Text", DataSource, "SLXuat").FormatString = DungChung.Bien.FormatString[0];
            //colTon.DataBindings.Add("Text", DataSource, "Ton");

        }
        //int tdk = 0;
        private void xrTable2_BeforePrint(object sender, CancelEventArgs e)
        {

        }
        // int td=0;
        int i = 0;
        private void colSoluongton_BeforePrint(object sender, CancelEventArgs e)
        {
            //var sltckFormat = String.Format(DungChung.Bien.FormatString[0], sltck);
            var sltckFormat = String.Format("{0:0,0}", sltck);

            colSoluongton.Text = sltckFormat;
            i++;
            if (i == 1)
            {
                colTonDau.Text = sltckFormat;
            }
        }

        private void colPhanloai_BeforePrint(object sender, CancelEventArgs e)
        {

            if (this.GetCurrentColumnValue("Ngaythang") != null)
            {
                string nt = this.GetCurrentColumnValue("Ngaythang").ToString();
                if (nt.Length >= 10)
                {
                    colPhanloai.Text = nt.Substring(0, 10);
                }
                else
                {
                    colPhanloai.Text = nt;
                }
            }
        }

        private void colNgaythang_BeforePrint(object sender, CancelEventArgs e)
        {
            //colNgayt.Text = colNgaythang.Text.Substring(0, 10);
            //ToString().Substring(0, 10);

        }

        private void colNgayt_BeforePrint(object sender, CancelEventArgs e)
        {

        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            //if (this.GetCurrentColumnValue("Ngaythang") != null)
            //{
            //    string nt = this.GetCurrentColumnValue("Ngaythang").ToString();
            //    colPhanloai.Text = nt.Substring(0, 10);
            //}
          

        }

        private void xrLabel5_BeforePrint(object sender, CancelEventArgs e)
        {
            sltdk();
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            //txtKeToanTruong.Text = DungChung.Bien.KeToanTruong;
            txtThuKho.Text = DungChung.Bien.ThuKho;
            //txtTruongKD.Text = DungChung.Bien.TruongKhoaDuoc;
            if (DungChung.Bien.MaBV == "27022")
            {
                xrTable4.Visible = true;
                xrTableRow9.Visible = true;

            }
            else
            {
                xrTableRow9.Visible = false;
                xrTable4.LocationF = new PointF(0F, 48.96F);
            }



        }

        private void xrTableCell14_BeforePrint(object sender, CancelEventArgs e)
        {
            colTonCuoi.Text = tonCuoi;
        }

        private void colSoluongton1_BeforePrint(object sender, CancelEventArgs e)
        {
            var sltckFormat = String.Format("{0:0,0}", sltck1);

            colSoluongton1.Text = sltckFormat;
            i++;
            if (i == 1)
            {
                colTonDau.Text = sltckFormat;
                colTonDau1.Text = sltckFormat;
            }
        }

        private void xrTable6_BeforePrint(object sender, CancelEventArgs e)
        {

        }

        private void colPhanloai1_BeforePrint(object sender, CancelEventArgs e)
        {
            if (this.GetCurrentColumnValue("Ngaythang") != null)
            {
                string nt = this.GetCurrentColumnValue("Ngaythang").ToString();
                if (nt.Length >= 10)
                {
                    colPhanloai1.Text = nt.Substring(0, 10);
                }
                else
                {
                    colPhanloai1.Text = nt;
                }
            }
        }

        private void colTon1_BeforePrint(object sender, CancelEventArgs e)
        {
            if (this.GetCurrentColumnValue("SLNhap") != null)
            {
                sltck1 += Convert.ToDouble(GetCurrentColumnValue("SLNhap")); //Math.Round(Convert.ToDouble(GetCurrentColumnValue("SLNhap")),0);
            }
            if (this.GetCurrentColumnValue("SLXuat") != null)
            {

                sltck1 += -Convert.ToDouble(GetCurrentColumnValue("SLXuat")); //Math.Round(Convert.ToDouble(GetCurrentColumnValue("SLXuat")),0);
            }

            var sltckFormat = String.Format("{0:0,0}", sltck1);
            colTon1.Text = sltckFormat;
            tonCuoi = sltckFormat;
        }

        private void colTenBN_BeforePrint(object sender, CancelEventArgs e)
        {
            if (colMaBN.Text != null && colMaBN.Text != "")
            {
                int a = Convert.ToInt32(colMaBN.Text);
                string tenBN = Data.BenhNhans.Where(p => p.MaBNhan == a).Select(p => p.TenBNhan).FirstOrDefault();
                colTenBN.Text = tenBN;
            }
            else
            {
                colTenBN.Text = "";
            }
        }

        private void colDiengiai_BeforePrint(object sender, CancelEventArgs e)
        {

        }

        private void colTonCuoi1_BeforePrint(object sender, CancelEventArgs e)
        {
            colTonCuoi1.Text = tonCuoi;
        }
    }
}
