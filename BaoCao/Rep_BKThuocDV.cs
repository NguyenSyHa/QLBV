using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_BKThuocDV : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_BKThuocDV()
        {
            InitializeComponent();
        }
        public void BindingData()
        {
            if(DungChung.Bien.MaBV == "20001")
            {
                
                colTenBN1.DataBindings.Add("Text", DataSource, "TenBN");
                colSNDT1.DataBindings.Add("Text", DataSource, "Ngaydt");
                colSA1.DataBindings.Add("Text", DataSource, "SA").FormatString = DungChung.Bien.FormatString[1];
                colXN1.DataBindings.Add("Text", DataSource, "XN").FormatString = DungChung.Bien.FormatString[1];
                colXQ1.DataBindings.Add("Text", DataSource, "XQ").FormatString = DungChung.Bien.FormatString[1];
                colNS1.DataBindings.Add("Text", DataSource, "NS").FormatString = DungChung.Bien.FormatString[1];
                colDT1.DataBindings.Add("Text", DataSource, "DT").FormatString = DungChung.Bien.FormatString[1];
                colThuoc1.DataBindings.Add("Text", DataSource, "Thuoc").FormatString = DungChung.Bien.FormatString[1];
                colck1.DataBindings.Add("Text", DataSource, "CongKham").FormatString = DungChung.Bien.FormatString[1];
                colVT1.DataBindings.Add("Text", DataSource, "VTTH").FormatString = DungChung.Bien.FormatString[1];
                colTT1.DataBindings.Add("Text", DataSource, "TTPT").FormatString = DungChung.Bien.FormatString[1];
                colTG1.DataBindings.Add("Text", DataSource, "TienGiuong").FormatString = DungChung.Bien.FormatString[1];
                colTong1.DataBindings.Add("Text", DataSource, "TT").FormatString = DungChung.Bien.FormatString[1];
                colTXN1.DataBindings.Add("Text", DataSource, "XN").FormatString = DungChung.Bien.FormatString[1];
                colTXQ1.DataBindings.Add("Text", DataSource, "XQ").FormatString = DungChung.Bien.FormatString[1];
                colTSA1.DataBindings.Add("Text", DataSource, "SA").FormatString = DungChung.Bien.FormatString[1];
                colTNS1.DataBindings.Add("Text", DataSource, "NS").FormatString = DungChung.Bien.FormatString[1];
                colTDT1.DataBindings.Add("Text", DataSource, "DT").FormatString = DungChung.Bien.FormatString[1];
                colTThuoc1.DataBindings.Add("Text", DataSource, "Thuoc").FormatString = DungChung.Bien.FormatString[1];
                colTCK1.DataBindings.Add("Text", DataSource, "Congkham").FormatString = DungChung.Bien.FormatString[1];
                colTVT1.DataBindings.Add("Text", DataSource, "VTTH").FormatString = DungChung.Bien.FormatString[1];
                colTTT1.DataBindings.Add("Text", DataSource, "TTPT").FormatString = DungChung.Bien.FormatString[1];
                colTTG1.DataBindings.Add("Text", DataSource, "TienGiuong").FormatString = DungChung.Bien.FormatString[1];
                colTNDT1.DataBindings.Add("Text", DataSource, "Ngaydt");
                colTC1.DataBindings.Add("Text", DataSource, "TT").FormatString = DungChung.Bien.FormatString[1];
            }
            else
            {
                colTenBN.DataBindings.Add("Text", DataSource, "TenBN");
                colSNDT.DataBindings.Add("Text", DataSource, "Ngaydt");
                colSA.DataBindings.Add("Text", DataSource, "SA").FormatString = DungChung.Bien.FormatString[1];
                colXN.DataBindings.Add("Text", DataSource, "XN").FormatString = DungChung.Bien.FormatString[1];
                colXQ.DataBindings.Add("Text", DataSource, "XQ").FormatString = DungChung.Bien.FormatString[1];
                colNS.DataBindings.Add("Text", DataSource, "NS").FormatString = DungChung.Bien.FormatString[1];
                colDT.DataBindings.Add("Text", DataSource, "DT").FormatString = DungChung.Bien.FormatString[1];
                colThuoc.DataBindings.Add("Text", DataSource, "Thuoc").FormatString = DungChung.Bien.FormatString[1];
                colVT.DataBindings.Add("Text", DataSource, "VTTH").FormatString = DungChung.Bien.FormatString[1];
                colTT.DataBindings.Add("Text", DataSource, "TTPT").FormatString = DungChung.Bien.FormatString[1];
                colTG.DataBindings.Add("Text", DataSource, "Congkham").FormatString = DungChung.Bien.FormatString[1];
                colTong.DataBindings.Add("Text", DataSource, "TT").FormatString = DungChung.Bien.FormatString[1];
                colTXN.DataBindings.Add("Text", DataSource, "XN").FormatString = DungChung.Bien.FormatString[1];
                colTXQ.DataBindings.Add("Text", DataSource, "XQ").FormatString = DungChung.Bien.FormatString[1];
                colTSA.DataBindings.Add("Text", DataSource, "SA").FormatString = DungChung.Bien.FormatString[1];
                colTNS.DataBindings.Add("Text", DataSource, "NS").FormatString = DungChung.Bien.FormatString[1];
                colTDT.DataBindings.Add("Text", DataSource, "DT").FormatString = DungChung.Bien.FormatString[1];
                colTThuoc.DataBindings.Add("Text", DataSource, "Thuoc").FormatString = DungChung.Bien.FormatString[1];
                colTVT.DataBindings.Add("Text", DataSource, "VTTH").FormatString = DungChung.Bien.FormatString[1];
                colTTT.DataBindings.Add("Text", DataSource, "TTPT").FormatString = DungChung.Bien.FormatString[1];
                colTTG.DataBindings.Add("Text", DataSource, "Congkham").FormatString = DungChung.Bien.FormatString[1];
                colTNDT.DataBindings.Add("Text", DataSource, "Ngaydt");
                colTC.DataBindings.Add("Text", DataSource, "TT").FormatString = DungChung.Bien.FormatString[1];
            }
            
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            txtTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            txtTCQ.Text = DungChung.Bien.TenCQ.ToUpper();
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "20001")
            {
                SubBand5.Visible = false;
            }
            else
            {
                SubBand6.Visible = false;
            }
            colNguoiLB.Text = DungChung.Bien.NguoiLapBieu;
            colTruongKhoa.Text = DungChung.Bien.TruongKhoaLS;
            colNguoiLB1.Text = DungChung.Bien.NguoiLapBieu;
            colTruongKhoa1.Text = DungChung.Bien.TruongKhoaLS;
        }

        private void PageHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            if(DungChung.Bien.MaBV == "20001")
            {
                SubBand1.Visible = false;
            }
            else
            {
                SubBand2.Visible = false;
            }
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "20001")
            {
                SubBand3.Visible = false;
            }
            else
            {

                SubBand4.Visible = false;
            }
        }
    }
}
