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
    public partial class repDTNoiTruHangNgay_HL01a : DevExpress.XtraReports.UI.XtraReport
    {
        public repDTNoiTruHangNgay_HL01a()
        {
            InitializeComponent();
        }
           string[] _songay;
          public string _mbn = "";
          List<FormNhap.usDieuTri.l_CTThuoc> _ldtct=new List<FormNhap.usDieuTri.l_CTThuoc>();
          public repDTNoiTruHangNgay_HL01a(string[] ngay, string mabn, List<FormNhap.usDieuTri.l_CTThuoc> CTThuoc)
        {
            InitializeComponent();
            _songay = ngay;
            _mbn = mabn;
            _ldtct = CTThuoc;
        }
        
        public void BindingData()
        {
            
            colTenNhomDVGh1.DataBindings.Add("Text", DataSource, "TenNhomDV");
            colTenDV.DataBindings.Add("Text", DataSource, "TenDV");
            txtMaDV.DataBindings.Add("Text", DataSource, "MaDV");
            colDonVi.DataBindings.Add("Text", DataSource, "DonVi");
            colSoLuong.DataBindings.Add("Text", DataSource, "SoLuongT").FormatString = DungChung.Bien.FormatString[1]; 
            colDonGia.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[1];
            colThanhTien.DataBindings.Add("Text", DataSource, "ThanhTienT").FormatString = DungChung.Bien.FormatString[1];
            colThanhTienTong.DataBindings.Add("Text", DataSource, "ThanhTienT").FormatString = DungChung.Bien.FormatString[1]; 
            GroupHeader1.GroupFields.Add(new GroupField("TenNhomDV"));
        }
        private string SL(int MaDV, double DonGia, DateTime ngay, string mbn)
        {
            
            string soluong = "";
            var dt2 = _ldtct.Where(p => p.NgayKe.Day == ngay.Day).Where(p => p.NgayKe.Month == ngay.Month).Where(p => p.NgayKe.Year == ngay.Year).Where(p => p.DonGia == DonGia).Where(p => p.MaDV == MaDV).Sum(p => p.SoLuong);
            if (dt2 != null && dt2.ToString() != "" && dt2.ToString()!="0")
            {
                soluong = dt2.ToString();
                return soluong;
            }
            else
                return null;
            
        }
        private static string SL2(int madv, double DonGia, DateTime ngay,int mbn)
        {
            
            QLBV_Database.QLBVEntities _Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            string soluong = "";
            var dt2 = (from dthc in _Data.DThuocs.Where(p=>p.MaBNhan== (mbn))
                      join dtct in _Data.DThuoccts.Where(p=>p.TrongBH==1) on dthc.IDDon equals dtct.IDDon
                      where (dtct.MaDV==madv && dtct.DonGia == DonGia)
                      where (dthc.NgayKe.Value.Day == ngay.Day && dthc.NgayKe.Value.Month == ngay.Month && dthc.NgayKe.Value.Year == ngay.Year)
                      group dtct by dtct.DonGia into kq
                      select new {SoLuong=kq.Sum(p=>p.SoLuong) }).ToList();
            if (dt2.Count > 0) {
                if (dt2.First().SoLuong > 0)
                {
                    soluong = dt2.First().SoLuong.ToString();
                }
            }
             return soluong;                                    
        }
        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "08204") {
                ReportFooter.Visible = true;
            }
            colCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
             colCQ.Text = DungChung.Bien.TenCQ.ToUpper() ;
            string[] sngay = new string[50];
            for (int a = 0; a < 50; a++)
            {
                sngay[a] = "01/01/2000";
            }


            for (int i = 0; i < _songay.Length; i++)
            {
                sngay[i] = _songay[i];
            }
            if(sngay[0].Length>=5)
            col1.Text = sngay[0].ToString().Substring(0,5);
            if (sngay[1].Length >= 5)
                col2.Text = sngay[1].ToString().Substring(0, 5);
            if (sngay[2].Length >= 5)
                col3.Text = sngay[2].ToString().Substring(0, 5);
            if (sngay[3].Length >= 5)
                col4.Text = sngay[3].ToString().Substring(0, 5);
            if (sngay[4].Length >= 5)
                col5.Text = sngay[4].ToString().Substring(0, 5);
            if (sngay[5].Length >= 5)
                col6.Text = sngay[5].ToString().Substring(0, 5);
            if (sngay[6].Length >= 5)
                col7.Text = sngay[6].ToString().Substring(0, 5);
            if (sngay[7].Length >= 5)
                col8.Text = sngay[7].ToString().Substring(0, 5);
            if (sngay[8].Length >= 5)
                col9.Text = sngay[8].ToString().Substring(0, 5);
            if (sngay[9].Length >= 5)
                col10.Text = sngay[9].ToString().Substring(0, 5);
            if (sngay[10].Length >= 5)
                col11.Text = sngay[10].ToString().Substring(0, 5);
            if (sngay[11].Length >= 5)
                col12.Text = sngay[11].ToString().Substring(0, 5);
            if (sngay[12].Length >= 5)
                col13.Text = sngay[12].ToString().Substring(0, 5);
            if (sngay[13].Length >= 5)
                col14.Text = sngay[13].ToString().Substring(0, 5);
            if (sngay[14].Length >= 5)
                col15.Text = sngay[14].ToString().Substring(0, 5);
            if (sngay[15].Length >= 5)
                col16.Text = sngay[15].ToString().Substring(0, 5);
            if (sngay[16].Length >= 5)
                col17.Text = sngay[16].ToString().Substring(0, 5);
            if (sngay[17].Length >= 5)
                col18.Text = sngay[17].ToString().Substring(0, 5);
            if (sngay[18].Length >= 5)
                col19.Text = sngay[18].ToString().Substring(0, 5);
            if (sngay[19].Length >= 5)
                col20.Text = sngay[19].ToString().Substring(0, 5);
            if (sngay[20].Length >= 5)
                col21.Text = sngay[20].ToString().Substring(0, 5);
            if (sngay[21].Length >= 5)
                col22.Text = sngay[21].ToString().Substring(0, 5);
            if (sngay[22].Length >= 5)
                col23.Text = sngay[22].ToString().Substring(0, 5);
            if (sngay[23].Length >= 5)
                col24.Text = sngay[23].ToString().Substring(0, 5);
            if (sngay[24].Length >= 5)
                col25.Text = sngay[24].ToString().Substring(0, 5);
            if (sngay[25].Length >= 5)
                col26.Text = sngay[25].ToString().Substring(0, 5);
            if (sngay[26].Length >= 5)
                col27.Text = sngay[26].ToString().Substring(0, 5);
            if (sngay[27].Length >= 5)
                col28.Text = sngay[27].ToString().Substring(0, 5);
            if (sngay[28].Length >= 5)
                col29.Text = sngay[28].ToString().Substring(0, 5);
            if (sngay[29].Length >= 5)
                col30.Text = sngay[29].ToString().Substring(0, 5);
            if (sngay[30].Length >= 5)
                col31.Text = sngay[30].ToString().Substring(0, 5);
            if (sngay[31].Length >= 5)
                col32.Text = sngay[31].ToString().Substring(0, 5);
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            double _soLuong = 0;
            int _madv = 0;
            double DonGia = 0;
            if (GetCurrentColumnValue("MaDV") != null)
                _madv = Convert.ToInt32( GetCurrentColumnValue("MaDV"));
            if (GetCurrentColumnValue("DonGia") != null)
                DonGia = Convert.ToDouble(GetCurrentColumnValue("DonGia"));
            foreach (var a in _songay)
            {
                if (SL(_madv, DonGia, DungChung.Ham.ConvertNgay(a), _mbn) != null && SL(_madv, DonGia, DungChung.Ham.ConvertNgay(a), _mbn)!="")
                _soLuong += Convert.ToDouble(SL(_madv, DonGia, DungChung.Ham.ConvertNgay(a), _mbn));
            }
            if (_soLuong <= 0)
            {
                xrTable2.Visible = false;
                xrLine1.Visible = false;
            }
            else
            {
                xrTable2.Visible = true;
                xrLine1.Visible = true;
            }
            col1de.Text = SL(_madv, DonGia, DungChung.Ham.ConvertNgay(_songay[0]),_mbn);
            col2de.Text = SL(_madv, DonGia, DungChung.Ham.ConvertNgay(_songay[1]), _mbn);
            col3de.Text = SL(_madv, DonGia, DungChung.Ham.ConvertNgay(_songay[2]), _mbn);
            col4de.Text = SL(_madv, DonGia, DungChung.Ham.ConvertNgay(_songay[3]),_mbn);
            col5de.Text = SL(_madv, DonGia, DungChung.Ham.ConvertNgay(_songay[4]),_mbn);
            col6de.Text = SL(_madv, DonGia, DungChung.Ham.ConvertNgay(_songay[5]),_mbn);
            col7de.Text = SL(_madv, DonGia, DungChung.Ham.ConvertNgay(_songay[6]),_mbn);
                col8de.Text = SL(_madv, DonGia, DungChung.Ham.ConvertNgay(_songay[7]), _mbn);
            col9de.Text = SL(_madv, DonGia, DungChung.Ham.ConvertNgay(_songay[8]), _mbn);
            col10de.Text = SL(_madv, DonGia, DungChung.Ham.ConvertNgay(_songay[9]), _mbn);
            col11de.Text = SL(_madv, DonGia, DungChung.Ham.ConvertNgay(_songay[10]), _mbn);
            col12de.Text = SL(_madv, DonGia, DungChung.Ham.ConvertNgay(_songay[11]), _mbn);
            col13de.Text = SL(_madv, DonGia, DungChung.Ham.ConvertNgay(_songay[12]), _mbn);
            col14de.Text = SL(_madv, DonGia, DungChung.Ham.ConvertNgay(_songay[13]), _mbn);
            col15de.Text = SL(_madv, DonGia, DungChung.Ham.ConvertNgay(_songay[14]), _mbn);
            col16de.Text = SL(_madv, DonGia, DungChung.Ham.ConvertNgay(_songay[15]), _mbn);
            col17de.Text = SL(_madv, DonGia, DungChung.Ham.ConvertNgay(_songay[16]), _mbn);
            col18de.Text = SL(_madv, DonGia, DungChung.Ham.ConvertNgay(_songay[17]), _mbn);
            col19de.Text = SL(_madv, DonGia, DungChung.Ham.ConvertNgay(_songay[18]), _mbn);
            col20de.Text = SL(_madv, DonGia, DungChung.Ham.ConvertNgay(_songay[19]), _mbn);
            col21de.Text = SL(_madv, DonGia, DungChung.Ham.ConvertNgay(_songay[20]), _mbn);
            col22de.Text = SL(_madv, DonGia, DungChung.Ham.ConvertNgay(_songay[21]), _mbn);
            col23de.Text = SL(_madv, DonGia, DungChung.Ham.ConvertNgay(_songay[22]), _mbn);
            col24de.Text = SL(_madv, DonGia, DungChung.Ham.ConvertNgay(_songay[23]), _mbn);
            col25de.Text = SL(_madv, DonGia, DungChung.Ham.ConvertNgay(_songay[24]), _mbn);
            col26de.Text = SL(_madv, DonGia, DungChung.Ham.ConvertNgay(_songay[25]), _mbn);
            col27de.Text = SL(_madv, DonGia, DungChung.Ham.ConvertNgay(_songay[26]), _mbn);
            col28de.Text = SL(_madv, DonGia, DungChung.Ham.ConvertNgay(_songay[27]), _mbn);
            col29de.Text = SL(_madv, DonGia, DungChung.Ham.ConvertNgay(_songay[28]), _mbn);
            col30de.Text = SL(_madv, DonGia, DungChung.Ham.ConvertNgay(_songay[29]), _mbn);
            col31de.Text = SL(_madv, DonGia, DungChung.Ham.ConvertNgay(_songay[30]), _mbn);
            col32de.Text = SL(_madv, DonGia, DungChung.Ham.ConvertNgay(_songay[31]), _mbn);
        }

    }
}
