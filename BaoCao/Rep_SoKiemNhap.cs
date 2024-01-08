using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
namespace QLBV.BaoCao
{
    public partial class Rep_SoKiemNhap : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_SoKiemNhap()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        public void BindingData()
        {
            celtenthuoc.DataBindings.Add("Text", DataSource, "TenDV");
            
            celncc.DataBindings.Add("Text", DataSource, "NhaSX");
            if(DungChung.Bien.MaBV == "20001")
            {
                celncc.DataBindings.Add("Text", DataSource, "TenCC");
                celchuacb.Text = "";
                celdacb.Text = "X";
            }
            else
            {
                celchuacb.DataBindings.Add("Text", DataSource, "chuacb");
                celdacb.DataBindings.Add("Text", DataSource, "dacb");
            }
            celsolo.DataBindings.Add("Text", DataSource, "SoLo");
            celhandung.DataBindings.Add("Text", DataSource, "HanDung").FormatString = "{0:dd/MM/yyyy}";
            celkhoilg.DataBindings.Add("Text", DataSource, "SoLuongN");
            GroupHeader1.GroupFields.Add(new GroupField("IDNhap"));
            col_soct.DataBindings.Add("Text", DataSource, "SoCT");


        }



        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "20001")
            {
                xrTableCell27.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular);
                xrTableCell30.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold);
                xrTableCell27.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                xrTableCell30.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                xrLine2.Visible = true;
            }
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            if(DungChung.Bien.MaBV == "20001")
            {
                xrTableCell51.Text = "Đạt";
                xrTableCell39.Text = "TRƯỞNG KHOA DƯỢC";
                xrTableCell59.Text = DungChung.Bien.TruongKhoaDuoc;
                xrTableCell33.Text = "PHÓ GIÁM ĐỐC";
                xrTableCell34.Text = "KẾ TOÁN TRƯỞNG";
                xrTableCell35.Text = "THỐNG KÊ DƯỢC";
                xrTableCell36.Text = "CÁN BỘ CUNG ỨNG";
                xrTableCell37.Text = "THỦ KHO";
            }
        }

        private void PageHeader_BeforePrint(object sender, CancelEventArgs e)
        {

        }

        private void colBSTH_BeforePrint(object sender, CancelEventArgs e)
        {
            
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            cel_ketoantruong.Text = DungChung.Bien.KeToanTruong;
            cel_thukho.Text = DungChung.Bien.ThuKho;
            
        }
    }

}

