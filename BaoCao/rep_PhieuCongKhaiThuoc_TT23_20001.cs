using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
using System.Data;
using System.Collections.Generic;
using System.Text;

namespace QLBV.BaoCao
{
    public partial class rep_PhieuCongKhaiThuoc_TT23_20001 : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_PhieuCongKhaiThuoc_TT23_20001()
        {
            InitializeComponent();
        }
        string[] _songay;
        public int _mbn = 0;
        List<FormNhap.usDieuTri.l_CTThuoc> _ldtct = new List<FormNhap.usDieuTri.l_CTThuoc>();
        bool _InTrang2 = false;
        public rep_PhieuCongKhaiThuoc_TT23_20001(string[] ngay, int mabn, List<FormNhap.usDieuTri.l_CTThuoc> CTThuoc, bool _in)
        {
            InitializeComponent();
            _songay = ngay;
            _mbn = mabn;
            _ldtct = CTThuoc;
            _InTrang2 = _in;
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
        public void BindingData()
        {

            colTenNhomDVGh1.DataBindings.Add("Text", DataSource, "TenNhomDV");
            colTenDV.DataBindings.Add("Text", DataSource, "TenDV");
            txtMaDV.DataBindings.Add("Text", DataSource, "MaDV");
            colDonVi.DataBindings.Add("Text", DataSource, "DonVi");
            colSoLuong.DataBindings.Add("Text", DataSource, "SoLuongT").FormatString = DungChung.Bien.FormatString[0];
            //colDonGia.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[1];
            //colThanhTien.DataBindings.Add("Text", DataSource, "ThanhTienT").FormatString = DungChung.Bien.FormatString[1];
            //colThanhTienTong.DataBindings.Add("Text", DataSource, "ThanhTienT").FormatString = DungChung.Bien.FormatString[1];
            //  colMaDV.DataBindings.Add("Text", DataSource, "MaDV");
            GroupHeader1.GroupFields.Add(new GroupField("TenNhomDV"));
        }
        int stt = 0;
        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            colCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            colCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            string[] sngay = new string[50];
            for (int a = 0; a < 50; a++)
            {
                sngay[a] = "01/01/2000";
            }


            for (int i = 0; i < _songay.Length; i++)
            {
                sngay[i] = _songay[i];
            }

            int k = 0;

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
                //xrLine1.Visible = false;
            }
            else
            {
                stt++;
                xrTable2.Visible = true;
                //xrLine1.Visible = true;
            }
            colSoTT.Text = stt.ToString();
            //
            int i = 0;

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
    }
}
