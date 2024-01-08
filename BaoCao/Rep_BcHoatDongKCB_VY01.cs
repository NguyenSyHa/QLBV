using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class Rep_BcHoatDongKCB_VY01 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_BcHoatDongKCB_VY01()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

        public void BindingData()
        {
            txtMaDV.DataBindings.Add("Text", DataSource, "MaDV");
            colTenTN.DataBindings.Add("Text", DataSource, "TenTN");
            colDVT.DataBindings.Add("Text", DataSource, "DVT");
            colTH.DataBindings.Add("Text", DataSource, "TH");
            colSSTT.DataBindings.Add("Text", DataSource, "SSTT");
            colSSCK.DataBindings.Add("Text", DataSource, "SSCK");
        }
        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            colTenCQ.Text = "Đơn vị nộp báo cáo: " + DungChung.Bien.TenCQ;
            colTenCQCQ.Text = "Đơn vị nhận BC: " + DungChung.Bien.TenCQCQ;

          
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            colNguoiLapBieu.Text = DungChung.Bien.NguoiLapBieu;
            colTTDV.Text = DungChung.Bien.GiamDoc;
        }

        private void colDVT_BeforePrint(object sender, CancelEventArgs e)
        {
            string _tentn = "";
            DateTime tungay = System.DateTime.Now.Date;
            DateTime denngay = System.DateTime.Now.Date;
            tungay = DungChung.Ham.NgayTu(Convert.ToDateTime(TuNgay.Value));
            denngay = DungChung.Ham.NgayDen(Convert.ToDateTime(DenNgay.Value));
            if (GetCurrentColumnValue("TenTN") != null)
                _tentn = GetCurrentColumnValue("TenTN").ToString();
            {

                var qkcb = (from dt in dataContext.DThuocs
                            join dtct in dataContext.DThuoccts on dt.IDDon equals dtct.IDDon
                            join dv in dataContext.DichVus on dtct.MaDV equals dv.MaDV
                            join tnhomdv in dataContext.TieuNhomDVs on dv.IdTieuNhom equals tnhomdv.IdTieuNhom
                            where (dt.NgayKe >= tungay && dt.NgayKe <= denngay)
                            group new { tnhomdv, dt, dv } by new { dt.MaBNhan, tnhomdv.TenTN } into kq
                            select new
                            {
                                MaBNhan = kq.Key.MaBNhan,
                                TenDV = kq.Key.TenTN,
                            }).ToList();
                if (qkcb.Count() > 0)
                {
                    int a = qkcb.Where(p => p.TenDV.Contains(_tentn)).Select(p => p.MaBNhan).Count();
                    colTH.Text = a.ToString();
                    {
                        if (_tentn.Contains("xét nghiệm"))
                        {
                            colDVT.Text = "Tiêu bản";
                        }
                        else colDVT.Text = "Lượt";
                    }
                }
            }
        }

        private void colSTT_BeforePrint(object sender, CancelEventArgs e)
        {
            int i=9;
            for (i=9;i<17;i++)
            {
                colSTT.Text=i.ToString();
            }
            i++;
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            Detail.Visible = false;
        }

    }
}
