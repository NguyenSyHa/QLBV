using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class Rep_SoThuTienKSK_PH01 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_SoThuTienKSK_PH01()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

        public void BindingData()
        {
            colDTuong.DataBindings.Add("Text", DataSource, "TChung");

            txtMaBNhan.DataBindings.Add("Text", DataSource, "MaBNhan");
            colTenBNhan.DataBindings.Add("Text", DataSource, "TenBNhan");
            colDiaChi.DataBindings.Add("Text", DataSource, "DChi");
            if(DungChung.Bien.MaBV=="30007")
            colSoGKSK.DataBindings.Add("Text", DataSource, "MaBNhan");
            else
            colSoGKSK.DataBindings.Add("Text", DataSource, "SoTT");
            colSoTo.DataBindings.Add("Text", DataSource, "SoTo");
            colThanhTien.DataBindings.Add("Text", DataSource, "SoTien").FormatString = DungChung.Bien.FormatString[1];
            colThanhTiengf.DataBindings.Add("Text", DataSource, "SoTien").FormatString = DungChung.Bien.FormatString[1];
            colThanhTienrf.DataBindings.Add("Text", DataSource, "SoTien").FormatString = DungChung.Bien.FormatString[1];

            GroupHeader1.GroupFields.Add(new GroupField("TChung"));
        }
        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            colTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            colTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
        }
        int stt = 1;
        string TongCong = "Tổng (";
        private void GroupHeader1_BeforePrint(object sender, CancelEventArgs e)
        {
            switch (stt)
            {
                case 1:
                    colSTTgh.Text = "A";
                    colSTTgf.Text = "Tổng A:";
                    TongCong += "A";
                    break;
                case 2:
                    colSTTgh.Text = "B";
                    colSTTgf.Text = "Tổng B:";
                    TongCong += "+B";
                    break;
                case 3:
                    colSTTgh.Text = "C";
                    colSTTgf.Text = "Tổng C:";
                    TongCong += "+C";
                    break;
                case 4:
                    colSTTgh.Text = "D";
                    colSTTgf.Text = "Tổng D:";
                    TongCong += "+D";
                    break;
                case 5:
                    colSTTgh.Text = "E";
                    colSTTgf.Text = "Tổng E:";
                    TongCong += "+E";
                    break;

            }
            TongCong += " ";
            colSTTrf.Text = TongCong +")";
            stt++;
        }

        private void colThanhTien_BeforePrint(object sender, CancelEventArgs e)
        {
            //if (GetCurrentColumnValue("TChung") != null)
            //{
            //    DateTime tungay = System.DateTime.Now.Date;
            //    DateTime denngay = System.DateTime.Now.Date;
            //    tungay = DungChung.Ham.NgayTu(Convert.ToDateTime(TuNgay.Value));
            //    denngay = DungChung.Ham.NgayDen(Convert.ToDateTime(DenNgay.Value));
            //    string _mabn = GetCurrentColumnValue("MaBNhan").ToString();
            //    var qvp = (from bn in data.BenhNhans.Where(p => p.DTuong.Contains("KSK"))
            //             join tu in data.TamUngs on bn.MaBNhan equals tu.MaBNhan
            //             where (tu.NgayThu >= tungay && tu.NgayThu <= denngay)
            //             select new { tu.MaBNhan,tu.SoTien}).ToList();
            //    if (qvp.Count > 0)
            //    {
            //        double a = Convert.ToDouble(qvp.Where(p => p.MaBNhan== (_mabn)).Sum(p => p.SoTien).ToString());
            //        colThanhTien.Text = a.ToString("##,###");
            //    }
            //}
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            var qtcb = (from cb in data.CanBoes.Where(p => p.MaCB== (DungChung.Bien.MaCB))
                        select new { cb.TenCB }).ToList();
            if (qtcb.Count() > 0)
            {
                colCBTT.Text = qtcb.First().TenCB;
            }

        }

    }
}
