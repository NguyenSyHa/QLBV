using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
namespace QLBV.BaoCao
{
    public partial class rep_TKBNTTkoXuatDuoc : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_TKBNTTkoXuatDuoc()
        {
            InitializeComponent();
        }
        bool _chk = true;
        public rep_TKBNTTkoXuatDuoc(bool chk)
        {
            InitializeComponent();
            _chk = chk;
        }
        QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        public void BindingData() {
            colTenKP.DataBindings.Add("Text", DataSource, "TenKP");
            colTenBN.DataBindings.Add("Text", DataSource, "TenBNhan");
            colTienThuoc.DataBindings.Add("Text", DataSource, "TienThuoc").FormatString=DungChung.Bien.FormatString[1];
           // colTienXD.DataBindings.Add("Text", DataSource, "TienDV").FormatString = DungChung.Bien.FormatString[1];
           // coLTongTien.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];
            colTienThuocRF.DataBindings.Add("Text", DataSource, "TienThuoc").FormatString = DungChung.Bien.FormatString[1];
           // colTienDVRF.DataBindings.Add("Text", DataSource, "TienDV").FormatString = DungChung.Bien.FormatString[1];
            colMaBN.DataBindings.Add("Text", DataSource, "MaBNhan");
            GroupHeader1.GroupFields.Add(new GroupField("TenKP"));
        }
        private double ThanhTienTT(int mabn)
        {
            double a = 0;
            try
            {
                var q = (from vp in _data.NhapDs.Where(p => p.MaBNhan==mabn)
                         join vpct in _data.NhapDcts on vp.IDNhap equals vpct.IDNhap
                         //join dv in _data.DichVus.Where(p => p.PLoai == 1) on vpct.MaDV equals dv.MaDV
                         group new { vpct, vp } by new { vp.MaBNhan } into kq
                         select new { kq.Key.MaBNhan, TTTT = kq.Sum(p => p.vpct.ThanhTienX) }).ToList();
                if (q.Count > 0)
                    a = q.Sum(p => p.TTTT);
            }
            catch (Exception)
            {
                a = 0;
            }
            return a;
        }
        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
         
            txtNguoiLap.Text = DungChung.Bien.NguoiLapBieu;
        }

        private void txtTenCQ_BeforePrint(object sender, CancelEventArgs e)
        {
            txtTenCQ.Text = DungChung.Bien.TenCQ;
        }
        double TTTT=0;
        private void colTienXD_BeforePrint(object sender, CancelEventArgs e)
        {
            if (_chk == false)
            {
                int  _mabn = 0;
                if (GetCurrentColumnValue("MaBNhan") != null)
                    _mabn =Convert.ToInt32( GetCurrentColumnValue("MaBNhan"));
                double b = ThanhTienTT(_mabn);
                TTTT += b;
                colTienXD.Text = b.ToString("##,###");
            }
        }

        private void PageHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV != "30005")
            {
                GroupHeader1.Visible = false;
            }
            //if (_chk == true) {
            //    txtTien1.Text = "Tiền thuốc XD";
            //    tctTien2.Text = "Tiền thuốc TT";
            //}
        }

        private void colTienDVRF_BeforePrint(object sender, CancelEventArgs e)
        {
            if (_chk == false)
            colTienDVRF.Text = TTTT.ToString("##,###");
        }
        double TienChenh = 0;
        private void coLTongTien_BeforePrint(object sender, CancelEventArgs e)
        {
            if (_chk == false)
            {
                double _tien = 0;
                if (GetCurrentColumnValue("TienThuoc") != null && GetCurrentColumnValue("TienThuoc").ToString() != "")
                    _tien = Convert.ToDouble(GetCurrentColumnValue("TienThuoc"));
                int  _mabn = 0;
                if (GetCurrentColumnValue("MaBNhan") != null )
                    _mabn = Convert.ToInt32( GetCurrentColumnValue("MaBNhan"));
                double b = ThanhTienTT(_mabn);
                coLTongTien.Text = (_tien - b).ToString("##,###");
                TienChenh += (_tien - b);
            }
        }

        private void colThanhTienRF_BeforePrint(object sender, CancelEventArgs e)
        {
            if (_chk == false)
                colThanhTienRF.Text = TienChenh.ToString("##,###");
        }

    }
}
