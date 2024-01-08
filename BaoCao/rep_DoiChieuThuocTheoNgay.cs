using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
namespace QLBV.BaoCao
{
    public partial class rep_DoiChieuThuocTheoNgay : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_DoiChieuThuocTheoNgay()
        {
            InitializeComponent();
        }
           DateTime[] _songay;
           int _makho;
           int _makhoa;
           string _dtuong;
           public rep_DoiChieuThuocTheoNgay(DateTime[] ngay, int makho, int makhoa, string dt)
        {
            InitializeComponent();
            _songay = ngay;
            _makho = makho;
            _makhoa = makhoa;
            _dtuong = dt;
        }
        
        public void BindingData()
        {
            colTenNhomDVGh1.DataBindings.Add("Text", DataSource, "TenNhomDV");
            colTenDV.DataBindings.Add("Text", DataSource, "TenDV");
            txtMaDV.DataBindings.Add("Text", DataSource, "MaDV");
            colDonVi.DataBindings.Add("Text", DataSource, "DonVi");
            colSoLuong.DataBindings.Add("Text", DataSource, "SLT").FormatString = "{0:##,###}"; 
            colDonGia.DataBindings.Add("Text", DataSource, "DonGia").FormatString = "{0:##,###}";
            colThanhTien.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = "{0:##,###}";
            colThanhTienTong.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = "{0:##,###}";
            col1de.DataBindings.Add("Text", DataSource, "SL1").FormatString = "{0:##,###}";
            col2de.DataBindings.Add("Text", DataSource, "SL2").FormatString = "{0:##,###}";
            col3de.DataBindings.Add("Text", DataSource, "SL3").FormatString = "{0:##,###}";
            col4de.DataBindings.Add("Text", DataSource, "SL4").FormatString = "{0:##,###}";
            col5de.DataBindings.Add("Text", DataSource, "SL5").FormatString = "{0:##,###}";
            col6de.DataBindings.Add("Text", DataSource, "SL6").FormatString = "{0:##,###}";
            col7de.DataBindings.Add("Text", DataSource, "SL7").FormatString = "{0:##,###}";
            col8de.DataBindings.Add("Text", DataSource, "SL8").FormatString = "{0:##,###}";
            col9de.DataBindings.Add("Text", DataSource, "SL9").FormatString = "{0:##,###}";
            col10de.DataBindings.Add("Text", DataSource, "SL10").FormatString = "{0:##,###}";
            col11de.DataBindings.Add("Text", DataSource, "SL11").FormatString = "{0:##,###}";
            col12de.DataBindings.Add("Text", DataSource, "SL12").FormatString = "{0:##,###}";
            col13de.DataBindings.Add("Text", DataSource, "SL13").FormatString = "{0:##,###}";
            col14de.DataBindings.Add("Text", DataSource, "SL14").FormatString = "{0:##,###}";
            col15de.DataBindings.Add("Text", DataSource, "SL15").FormatString = "{0:##,###}";
            col16de.DataBindings.Add("Text", DataSource, "SL16").FormatString = "{0:##,###}";
            col17de.DataBindings.Add("Text", DataSource, "SL17").FormatString = "{0:##,###}";
            col18de.DataBindings.Add("Text", DataSource, "SL18").FormatString = "{0:##,###}";
            col19de.DataBindings.Add("Text", DataSource, "SL19").FormatString = "{0:##,###}";
            col20de.DataBindings.Add("Text", DataSource, "SL20").FormatString = "{0:##,###}";
            col21de.DataBindings.Add("Text", DataSource, "SL21").FormatString = "{0:##,###}";
            col22de.DataBindings.Add("Text", DataSource, "SL22").FormatString = "{0:##,###}";
            col23de.DataBindings.Add("Text", DataSource, "SL23").FormatString = "{0:##,###}";
            col24de.DataBindings.Add("Text", DataSource, "SL24").FormatString = "{0:##,###}";
            col25de.DataBindings.Add("Text", DataSource, "SL25").FormatString = "{0:##,###}";
            col26de.DataBindings.Add("Text", DataSource, "SL26").FormatString = "{0:##,###}";
            col27de.DataBindings.Add("Text", DataSource, "SL27").FormatString = "{0:##,###}";
            col28de.DataBindings.Add("Text", DataSource, "SL28").FormatString = "{0:##,###}";
            col29de.DataBindings.Add("Text", DataSource, "SL29").FormatString = "{0:##,###}";
            col30de.DataBindings.Add("Text", DataSource, "SL30").FormatString = "{0:##,###}";
            col31de.DataBindings.Add("Text", DataSource, "SL31").FormatString = "{0:##,###}";
            col32de.DataBindings.Add("Text", DataSource, "SL32").FormatString = "{0:##,###}"; 

            GroupHeader1.GroupFields.Add(new GroupField("TenNhomDV"));
        }
        
        //private static string SL(int madv, double DonGia, DateTime ngay, string makho, string makhoa, string _dtuong)
        //{
            
        //    QLBV_Database.QLBVEntities _Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        //    string soluong = "";
        //    var dt2 = (from bn in _Data.BenhNhans.Where(p => p.DTuong.Contains(_dtuong))
        //               join dthc in _Data.DThuocs.Where(p => p.MaKP== (makhoa)).Where(p => p.MaKXuat== makho) on bn.MaBNhan equals dthc.MaBNhan
        //              join dtct in _Data.DThuoccts.Where(p=>p.TrongBH==1) on dthc.IDDon equals dtct.IDDon
        //              where (dtct.MaDV== madv && dtct.DonGia == DonGia)
        //              where (dthc.NgayKe.Value.Day == ngay.Day && dthc.NgayKe.Value.Month == ngay.Month && dthc.NgayKe.Value.Year == ngay.Year)
        //              group dtct by dtct.DonGia into kq
        //              select new {SoLuong=kq.Sum(p=>p.SoLuong) }).ToList();
        //    if (dt2.Count > 0) {
        //        if (dt2.Sum(p=>p.SoLuong) >0 )
        //            soluong = dt2.Sum(p => p.SoLuong).ToString();
        //    }
        //     return soluong;                                    
        //}
        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
             colCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
             colCQ.Text = DungChung.Bien.TenCQ.ToUpper() ;
            string[] sngay = new string[50];
            for (int a = 0; a < 50; a++)
            {
                sngay[a] = "";
            }


            for (int i = 0; i < _songay.Length; i++)
            {
                if(_songay[i].Year>2010)
                sngay[i] = _songay[i].ToShortDateString();
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

    }
}
