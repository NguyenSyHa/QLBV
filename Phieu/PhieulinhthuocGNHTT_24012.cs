using System;
using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
namespace QLBV.BaoCao
{
    public partial class PhieulinhthuocGNHTT_24012 : DevExpress.XtraReports.UI.XtraReport
    {
        private readonly bool _huongtt;
        public PhieulinhthuocGNHTT_24012(bool huongtt)
        {
            _huongtt = huongtt;

            InitializeComponent();
        }
        string _tenRG = "";
        int _soPL = 0;
        public void BindingData()
        {
            colTenHH.DataBindings.Add("Text", DataSource, "TenDV");
            colTenHH1.DataBindings.Add("Text", DataSource, "TenDV");
            colhamLuong1.DataBindings.Add("Text", DataSource, "HamLuong");
            colhamLuong.DataBindings.Add("Text", DataSource, "HamLuong");
            txtsoluongso.DataBindings.Add("Text", DataSource, "SoLuong").FormatString = DungChung.Bien.FormatString[1];
            txtsoluongso1.DataBindings.Add("Text", DataSource, "SoLuong").FormatString = DungChung.Bien.FormatString[1];
            if (DungChung.Bien.MaBV == "24009")
                colsoluongyc.DataBindings.Add("Text", DataSource, "SoLuong").FormatString = DungChung.Bien.FormatString[1];
            if (DungChung.Bien.MaBV == "24009")
                colsoluongyc1.DataBindings.Add("Text", DataSource, "SoLuong").FormatString = DungChung.Bien.FormatString[1];
            colDVT.DataBindings.Add("Text", DataSource, "DonVi");
            colDVT1.DataBindings.Add("Text", DataSource, "DonVi");

        }
        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            Benhvien.Value = DungChung.Bien.TenCQ;
            Boyte.Value = DungChung.Bien.TenCQCQ;


        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            txtNguoiLapBieu1.Text = DungChung.Bien.NguoiLapBieu;
            txtTruongKhoaLS1.Text = DungChung.Bien.TruongKhoaLS;
            colTruongKDuoc1.Text = DungChung.Bien.TruongKhoaDuoc;
            txtNguoiLapBieu.Text = DungChung.Bien.NguoiLapBieu;
            txtTruongKhoaLS.Text = DungChung.Bien.TruongKhoaLS;
            colTruongKDuoc.Text = DungChung.Bien.TruongKhoaDuoc;
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
            if (dt3.Count > 0)
            {
                txtNT.Text = "Ngày " + dt3.First().Value.Day + " tháng " + dt3.First().Value.Month + " năm " + dt3.First().Value.Year;
                txtNT0.Text = "Ngày " + dt3.First().Value.Day + " tháng " + dt3.First().Value.Month + " năm " + dt3.First().Value.Year;
                txtNT1.Text = "Ngày " + dt3.First().Value.Day + " tháng " + dt3.First().Value.Month + " năm " + dt3.First().Value.Year;
                txtNT2.Text = "Ngày " + dt3.First().Value.Day + " tháng " + dt3.First().Value.Month + " năm " + dt3.First().Value.Year;
            }
            else
            {
                txtNT.Text = "Ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;
                txtNT0.Text = "Ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;
                txtNT1.Text = "Ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;
                txtNT2.Text = "Ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;
            }
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            if (GetCurrentColumnValue("TenRG") != null && GetCurrentColumnValue("TenRG").ToString() != "")
                _tenRG = GetCurrentColumnValue("TenRG").ToString();
            if (GetCurrentColumnValue("SoLuong") != null && GetCurrentColumnValue("SoLuong").ToString() != "")
            {
                double _sluong = Convert.ToDouble(GetCurrentColumnValue("SoLuong"));
                if (_tenRG == "Thuốc gây nghiện")
                {
                    colsoluongtp.Text = colsoluongyc.Text = DungChung.Ham.DocTienBangChu(_sluong, "");
                    colsoluongtp1.Text = colsoluongyc1.Text = DungChung.Ham.DocTienBangChu(_sluong, "");
                }
                else
                {
                    if (!_huongtt)
                    {
                        colsoluongtp.Text = colsoluongyc.Text = DungChung.Ham.DocTienBangChu(_sluong, "");
                        colsoluongtp1.Text = colsoluongyc1.Text = DungChung.Ham.DocTienBangChu(_sluong, "");
                    }
                    else
                    {
                        colsoluongtp.Text = colsoluongyc.Text = _sluong.ToString();
                        colsoluongtp1.Text = colsoluongyc1.Text = _sluong.ToString();
                    }
                }
            }

        }

        private void ReportHeader_BeforePrint_1(object sender, CancelEventArgs e)
        {
            txtTenBV.Text = DungChung.Bien.TenCQ;
            txtTenBV1.Text = DungChung.Bien.TenCQ;
            txtTenCQCQ.Text = DungChung.Bien.TenCQCQ;
        }

        private void GroupFooter1_BeforePrint(object sender, CancelEventArgs e)
        {
           

        }

        private void GroupFooter4_BeforePrint(object sender, CancelEventArgs e)
        {

        }

        private void GroupFooter2_BeforePrint(object sender, CancelEventArgs e)
        {
           
        }
    }

}
