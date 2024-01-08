using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class rep_PhieuCongKhaiThuoc_TT23 : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_PhieuCongKhaiThuoc_TT23()
        {
            InitializeComponent();
        }

        private string[] _songay;
        public int _mbn = 0;
        private List<FormNhap.usDieuTri.l_CTThuoc> _ldtct = new List<FormNhap.usDieuTri.l_CTThuoc>();
        private bool _InTrang2 = false;

        public rep_PhieuCongKhaiThuoc_TT23(string[] ngay, int mabn, List<FormNhap.usDieuTri.l_CTThuoc> CTThuoc, bool _in)
        {
            InitializeComponent();
            _songay = ngay;
            _mbn = mabn;
            _ldtct = CTThuoc;
            _InTrang2 = _in;
        }

        public void BindingData()
        {
            colTenNhomDVGh1.DataBindings.Add("Text", DataSource, "TenNhomDV");
            colTenDV.DataBindings.Add("Text", DataSource, "TenDV");
            xrTableCell110.DataBindings.Add("Text", DataSource, "TenDV");
            colTenDV24272.DataBindings.Add("Text", DataSource, "TenDV");
            txtMaDV.DataBindings.Add("Text", DataSource, "MaDV");
            xrLabel4.DataBindings.Add("Text", DataSource, "MaDV");
            txtMaDV24272.DataBindings.Add("Text", DataSource, "MaDV");
            colDonVi.DataBindings.Add("Text", DataSource, "DonVi");
            xrTableCell112.DataBindings.Add("Text", DataSource, "DonVi");
            colDonVi24272.DataBindings.Add("Text", DataSource, "DonVi");
            colSoLuong.DataBindings.Add("Text", DataSource, "SoLuongT").FormatString = DungChung.Bien.FormatString[0];
            xrTableCell131.DataBindings.Add("Text", DataSource, "SoLuongT").FormatString = DungChung.Bien.FormatString[0];
            colTongSo24272.DataBindings.Add("Text", DataSource, "SoLuongT").FormatString = DungChung.Bien.FormatString[0];
            colDonGia.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[1];
            colThanhTien.DataBindings.Add("Text", DataSource, "ThanhTienT").FormatString = DungChung.Bien.FormatString[1];
            colThanhTienTong.DataBindings.Add("Text", DataSource, "ThanhTienT").FormatString = DungChung.Bien.FormatString[1];
            //  colMaDV.DataBindings.Add("Text", DataSource, "MaDV");
            GroupHeader1.GroupFields.Add(new GroupField("TenNhomDV"));
        }

        private string SL(int MaDV, double DonGia, DateTime ngay, int mbn)
        {
            string soluong = "";
            var dt2 = _ldtct.Where(p => p.NgayKe.Day == ngay.Day).Where(p => p.NgayKe.Month == ngay.Month).Where(p => p.NgayKe.Year == ngay.Year).Where(p => p.DonGia == DonGia).Where(p => p.MaDV == MaDV).Sum(p => p.SoLuong);
            if (dt2 != null && dt2.ToString() != "" && dt2.ToString() != "0")
            {
                soluong = dt2.ToString();
                return soluong;
            }
            else
                return null;
        }

        #region bỏ

        //private static string SL2(int madv, double DonGia, DateTime ngay,string mbn)
        //{
        //    QLBV_Database.QLBVEntities _Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        //    string soluong = "";
        //    var dt2 = (from dthc in _Data.DThuocs.Where(p=>p.MaBNhan== (mbn))
        //              join dtct in _Data.DThuoccts.Where(p=>p.TrongBH==1 || p.TrongBH==0) on dthc.IDDon equals dtct.IDDon
        //              where (dtct.MaDV== (MaDV) && dtct.DonGia == DonGia)
        //              where (dthc.NgayKe.Value.Day == ngay.Day && dthc.NgayKe.Value.Month == ngay.Month && dthc.NgayKe.Value.Year == ngay.Year)
        //              group dtct by dtct.DonGia into kq
        //              select new {SoLuong=kq.Sum(p=>p.SoLuong) }).ToList();
        //    if (dt2.Count > 0) {
        //        if (dt2.First().SoLuong > 0)
        //        {
        //            soluong = dt2.First().SoLuong.ToString();
        //        }
        //    }
        //     return soluong;
        //}

        #endregion bỏ

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "24297")
                SubBand7.Visible = false;
            else
                SubBand8.Visible = false;
            if (DungChung.Bien.MaBV == "20001")
            {
                colMaChanDoan.Visible = false;
                xrTableCell19.Visible = false;
            }

            colCQCQ.Text = colCQCQ2.Text = lblCQCQ24272.Text =  DungChung.Bien.TenCQCQ.ToUpper();
            colCQ.Text = colCQ2.Text = lblCQ24272.Text = DungChung.Bien.TenCQ.ToUpper();
            if (DungChung.Bien.MaBV == "24272")
            {
                RH_24272.Visible = true;
                SubBand3.Visible = SubBand4.Visible = SubBand7.Visible = SubBand8.Visible = false;
                xrPictureBox2.Image = DungChung.Ham.GetLogo();
            }
            string[] sngay = new string[50];
            if (DungChung.Bien.MaBV == "24272")
            {
                sngay = new string[18];
            }
            for (int a = 0; a < sngay.Count(); a++)
            {
                sngay[a] = "01/01/2000";
            }

            for (int i = 0; i < _songay.Length; i++)
            {
                sngay[i] = _songay[i];
            }

            int k = 0;

            if (DungChung.Bien.MaBV == "24297")
            {
                foreach (XRTableCell cell in xrTableRow22)
                {
                    cell.Text = "";
                    if (k >= 17)
                        break;
                    if (cell.Index == k)
                    {
                        if (sngay[k].Length >= 5)
                            cell.Text = sngay[k].ToString().Substring(0, 5);
                        k++;
                    }
                }
            }
            else if (DungChung.Bien.MaBV == "24272")
            {
                foreach (XRTableCell cell in tbrNgayThang24272)
                {
                    cell.Text = "";
                    if (k > 18)
                        break;
                    if (cell.Index == k)
                    {
                        if (sngay[k].Count() >= 5)
                            cell.Text = sngay[k].ToString().Substring(0, 5);
                        k++;
                    }
                }
            }
            else
            {
                foreach (XRTableCell cell in xrTableRow3)
                {
                    cell.Text = "";
                    if (k >= 17)
                        break;
                    if (cell.Index == k)
                    {
                        if (sngay[k].Length >= 5)
                            cell.Text = sngay[k].ToString().Substring(0, 5);
                        k++;
                    }
                }
            }

            //if(sngay[0].Length>=5)
            //col1.Text = sngay[0].ToString().Substring(0,5);
            //if (sngay[1].Length >= 5)
            //    col2.Text = sngay[1].ToString().Substring(0, 5);
            //if (sngay[2].Length >= 5)
            //    col3.Text = sngay[2].ToString().Substring(0, 5);
            //if (sngay[3].Length >= 5)
            //    col4.Text = sngay[3].ToString().Substring(0, 5);
            //if (sngay[4].Length >= 5)
            //    col5.Text = sngay[4].ToString().Substring(0, 5);
            //if (sngay[5].Length >= 5)
            //    col6.Text = sngay[5].ToString().Substring(0, 5);
            //if (sngay[6].Length >= 5)
            //    col7.Text = sngay[6].ToString().Substring(0, 5);
            //if (sngay[7].Length >= 5)
            //    col8.Text = sngay[7].ToString().Substring(0, 5);
            //if (sngay[8].Length >= 5)
            //    col9.Text = sngay[8].ToString().Substring(0, 5);
            //if (sngay[9].Length >= 5)
            //    col10.Text = sngay[9].ToString().Substring(0, 5);
            //if (sngay[10].Length >= 5)
            //    col11.Text = sngay[10].ToString().Substring(0, 5);
            //if (sngay[11].Length >= 5)
            //    col12.Text = sngay[11].ToString().Substring(0, 5);
            //if (sngay[12].Length >= 5)
            //    col13.Text = sngay[12].ToString().Substring(0, 5);
            //if (sngay[13].Length >= 5)
            //    col14.Text = sngay[13].ToString().Substring(0, 5);
            //if (sngay[14].Length >= 5)
            //    col15.Text = sngay[14].ToString().Substring(0, 5);
            //if (sngay[15].Length >= 5)
            //    col16.Text = sngay[15].ToString().Substring(0, 5);
            //if (sngay[16].Length >= 5)
            //    col17.Text = sngay[16].ToString().Substring(0, 5);
            //if (sngay[17].Length >= 5)
            //    col18.Text = sngay[17].ToString().Substring(0, 5);
        }

        private int stt = 0;

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            double _soLuong = 0;
            int _madv = 0;
            double DonGia = 0;
            if (GetCurrentColumnValue("MaDV") != null)
                _madv = Convert.ToInt32(GetCurrentColumnValue("MaDV"));
            if (GetCurrentColumnValue("DonGia") != null)
                DonGia = Convert.ToDouble(GetCurrentColumnValue("DonGia"));
            foreach (var a in _songay)
            {
                if (SL(_madv, DonGia, DungChung.Ham.ConvertNgay(a), _mbn) != null && SL(_madv, DonGia, DungChung.Ham.ConvertNgay(a), _mbn) != "")
                    _soLuong += Convert.ToDouble(SL(_madv, DonGia, DungChung.Ham.ConvertNgay(a), _mbn));
            }
            if (_soLuong == 0)
            {
                xrTable2.Visible = false;
                xrLine1.Visible = false;
            }
            else
            {
                stt++;
                xrTable2.Visible = true;
                xrLine1.Visible = true;
            }
            colSoTT.Text = stt.ToString();
            //
            int i = 0;

            if (DungChung.Bien.MaBV == "24297")
            {
                xrTable20.Visible = true;
                xrTable2.Visible = false;
                SubBand5.Visible = true;
                SubBand6.Visible = false;
                foreach (XRTableCell cell in xrTableRow23)
                {
                    if (i >= 17)
                        break;
                    if (cell.Index == i + 3)
                    {
                        cell.Text = SL(_madv, DonGia, DungChung.Ham.ConvertNgay(_songay[i]), _mbn);
                        i++;
                    }
                }
            }
            else if (DungChung.Bien.MaBV == "24272")
            {
                Detail_24272.Visible = true;
                SubBand5.Visible = false;
                SubBand6.Visible = false;
                foreach (XRTableCell cell in tblRow24272)
                {
                    if (i >= 17)
                        break;
                    if (cell.Index == i + 3)
                    {
                        cell.Text = SL(_madv, DonGia, DungChung.Ham.ConvertNgay(_songay[i]), _mbn);
                        i++;
                    }
                }
            }
            else
            {
                xrTable2.Visible = true;
                xrTable20.Visible = false;
                SubBand5.Visible = false;
                SubBand6.Visible = true;
                foreach (XRTableCell cell in xrTableRow2)
                {
                    if (i >= 17)
                        break;
                    if (cell.Index == i + 3)
                    {
                        cell.Text = SL(_madv, DonGia, DungChung.Ham.ConvertNgay(_songay[i]), _mbn);
                        i++;
                    }
                }
            }

            //
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            colMaDV.Text = xrTableCell178.Text = celSoKhoan24272.Text =  stt.ToString();
            if (_InTrang2 == false)
                colThanhTienTong.Text = "";
            if (DungChung.Bien.MaBV == "27021" || DungChung.Bien.MaBV == "27022" || DungChung.Bien.MaBV == "27023")
                xrTable16.Visible = true;
            else
                xrTable16.Visible = false;
            if (DungChung.Bien.MaBV == "20001")
                xrTable15.Visible = false;
            if (DungChung.Bien.MaBV == "24297")
            {
                SubBand1.Visible = false;
            }
            else if (DungChung.Bien.MaBV == "24272")
            {
                SubBand1.Visible = false;
                SubBand2.Visible = false;
                RF_24272.Visible = true;
            }
            else
            {
                SubBand2.Visible = false;
            }
        }

        private void GroupHeader1_BeforePrint(object sender, CancelEventArgs e)
        {

        }
    }
}