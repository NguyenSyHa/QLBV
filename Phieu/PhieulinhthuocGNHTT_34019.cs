using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
namespace QLBV.BaoCao
{
    public partial class PhieulinhthuocGNHTT_34019 : DevExpress.XtraReports.UI.XtraReport
    {
        public PhieulinhthuocGNHTT_34019()
        {
            InitializeComponent();
        }
        string _tenRG = "";
        int _soPL = 0;
        public void BindingData()
        {
            colTenHH.DataBindings.Add("Text", DataSource, "TenDV");
            txtsoluongso.DataBindings.Add("Text", DataSource, "SoLuong").FormatString = DungChung.Bien.FormatString[1];
            if (DungChung.Bien.MaBV == "24009")
                colsoluongyc.DataBindings.Add("Text", DataSource, "SoLuong").FormatString = DungChung.Bien.FormatString[1];
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
       
        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV != "24009")
            {
                if (GetCurrentColumnValue("TenRG") != null && GetCurrentColumnValue("TenRG").ToString() != "")
                    _tenRG = GetCurrentColumnValue("TenRG").ToString();
                if (GetCurrentColumnValue("SoLuong") != null && GetCurrentColumnValue("SoLuong").ToString() != "")
                {
                    double _sluong = Convert.ToDouble(GetCurrentColumnValue("SoLuong"));
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
                    if (DungChung.Bien.MaBV == "30004")
                    {
                        int _madv = 0;
                        if (GetCurrentColumnValue("MaDV") != null && GetCurrentColumnValue("MaDV").ToString() != "")
                            _madv = Convert.ToInt32(GetCurrentColumnValue("MaDV"));
                       
                       
                        QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                        var gc = (from bn in _data.BenhNhans
                                  join dt in _data.DThuocs on bn.MaBNhan equals dt.MaBNhan
                                  join dtct in _data.DThuoccts.Where(p => p.MaDV == _madv).Where(p => p.SoPL == _soPL) on dt.IDDon equals dtct.IDDon
                                  select new { bn.MaDTuong, bn.TenBNhan, dtct.DonVi, dtct.SoLuong,bn.Tuoi }).ToList();
                        string ghichu = "";
                       
                        if (gc.Count > 0)
                        {
                            foreach (var a in gc)
                            {
                                string[] _tenbn = a.TenBNhan.Split(' ');
                                ghichu += _tenbn[_tenbn.Length - 1]+"-"+ a.Tuoi+"t: " + a.SoLuong + " " + a.DonVi + ", ";
                            }
                        }
                        colghichu.Text = ghichu;

                    }
                    if (DungChung.Bien.MaBV == "30002")
                        colsoluongtp.Text = DungChung.Ham.DocTienBangChu(_sluong, "");
                }
            }
        }

        private void ReportHeader_BeforePrint_1(object sender, CancelEventArgs e)
        {
            txtTenBV.Text = DungChung.Bien.TenCQ;
            txtTenCQCQ.Text = DungChung.Bien.TenCQCQ;
            if (DungChung.Bien.MaBV == "30004")
            {
                if (this.SoPL.Value != null && this.SoPL.Value.ToString() != "")
                {
                    _soPL = Convert.ToInt32(this.SoPL.Value.ToString());
                }
                QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                var gc = (from bn in _data.BenhNhans
                          join dt in _data.DThuocs on bn.MaBNhan equals dt.MaBNhan
                          join dtct in _data.DThuoccts.Where(p => p.SoPL == _soPL) on dt.IDDon equals dtct.IDDon
                          select bn.MaDTuong).ToList();
                if (gc.Count > 0)
                {
                    if (gc.Count == gc.Where(p => p == "TE").ToList().Count)
                    {
                        this.Chuthich.Value = "Trẻ em " + this.Chuthich.Value;
                    }
                }
            }
        }
    }

}
