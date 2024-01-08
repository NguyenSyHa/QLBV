using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_BC_THThuocVTYT : DevExpress.XtraReports.UI.XtraReport
    {
        private bool htbn;
        int i = 0;// stt nhóm
        int num = 0;//stt  số stt trong BC
        public Rep_BC_THThuocVTYT()
        {
            InitializeComponent();
        }
        public Rep_BC_THThuocVTYT(bool htbn)
        {
            InitializeComponent();
            this.htbn = htbn;
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            lblTenBV.Text = DungChung.Bien.TenCQ.ToUpper();
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            colGiamDoc.Text = DungChung.Bien.GiamDoc;
            colKeToan.Text = DungChung.Bien.KeToanTruong;
            colNguoiLapBieu.Text = DungChung.Bien.NguoiLapBieu;
        }

        public void BindingData()
        {
            //group
            lblSTT.DataBindings.Add("Text", DataSource, "sttDichVu");
            celTenThuoc.DataBindings.Add("Text", DataSource, "TenDV1");
            celDVi.DataBindings.Add("Text", DataSource, "DVT1");
            celDonGia.DataBindings.Add("Text", DataSource, "DonGia1").FormatString = DungChung.Bien.FormatString[1];
            celSLDuoc.DataBindings.Add("Text", DataSource, "DuocSL1").FormatString = DungChung.Bien.FormatString[1];
            celTTDuoc.DataBindings.Add("Text", DataSource, "DuocTT1").FormatString = DungChung.Bien.FormatString[1];
            celVPDKSL.DataBindings.Add("Text", DataSource, "VPDKSL1").FormatString = DungChung.Bien.FormatString[1];
            celVPDKTT.DataBindings.Add("Text", DataSource, "VPDKTT1").FormatString = DungChung.Bien.FormatString[1];
            celBHYTSL.DataBindings.Add("Text", DataSource, "VPTKBHYTSL1").FormatString = DungChung.Bien.FormatString[1];
            celBHYTTT.DataBindings.Add("Text", DataSource, "VPTKBHYTTT1").FormatString = DungChung.Bien.FormatString[1];
            celKhongTTSL.DataBindings.Add("Text", DataSource, "VPTKKhongTTSL1").FormatString = DungChung.Bien.FormatString[1];
            celKhongTTTT.DataBindings.Add("Text", DataSource, "VPTKKhongTTTT1").FormatString = DungChung.Bien.FormatString[1];
            celDVSL.DataBindings.Add("Text", DataSource, "VPTKDVSL1").FormatString = DungChung.Bien.FormatString[1];
            celDVTT.DataBindings.Add("Text", DataSource, "VPTKDVTT1").FormatString = DungChung.Bien.FormatString[1];
            celVPCKSL.DataBindings.Add("Text", DataSource, "VPCKSL1").FormatString = DungChung.Bien.FormatString[1];
            celVPCKTT.DataBindings.Add("Text", DataSource, "VPCKTT1").FormatString = DungChung.Bien.FormatString[1];
            celVPTKBASL.DataBindings.Add("Text", DataSource, "VPTKBASL1").FormatString = DungChung.Bien.FormatString[1];
            celVPTKBATT.DataBindings.Add("Text", DataSource, "VPTKBATT1").FormatString = DungChung.Bien.FormatString[1];

            //details
            //colTenBN.DataBindings.Add("Text", DataSource, "TenBN");
            //// lblTenDV.DataBindings.Add("Text", DataSource, "TenDV1");            
            //colDuocSL.DataBindings.Add("Text", DataSource, "DuocSL1").FormatString = DungChung.Bien.FormatString[1];
            //colDuocTT.DataBindings.Add("Text", DataSource, "DuocTT1").FormatString = DungChung.Bien.FormatString[1];
            //colVPDKSL.DataBindings.Add("Text", DataSource, "VPDKSL1").FormatString = DungChung.Bien.FormatString[1];
            //colVPDKTT.DataBindings.Add("Text", DataSource, "VPDKTT1").FormatString = DungChung.Bien.FormatString[1];
            //colBHYTSL.DataBindings.Add("Text", DataSource, "VPTKBHYTSL1").FormatString = DungChung.Bien.FormatString[1];
            //colBHYTTT.DataBindings.Add("Text", DataSource, "VPTKBHYTTT1").FormatString = DungChung.Bien.FormatString[1];
            //colKhongTTSL.DataBindings.Add("Text", DataSource, "VPTKKhongTTSL1").FormatString = DungChung.Bien.FormatString[1];
            //colKhongTTTT.DataBindings.Add("Text", DataSource, "VPTKKhongTTTT1").FormatString = DungChung.Bien.FormatString[1];
            //colDVSL.DataBindings.Add("Text", DataSource, "VPTKDVSL1").FormatString = DungChung.Bien.FormatString[1];
            //colDVTT.DataBindings.Add("Text", DataSource, "VPTKDVTT1").FormatString = DungChung.Bien.FormatString[1];
            //colVPCKSL.DataBindings.Add("Text", DataSource, "VPCKSL1").FormatString = DungChung.Bien.FormatString[1];
            //colVPCKTT.DataBindings.Add("Text", DataSource, "VPCKTT1").FormatString = DungChung.Bien.FormatString[1];
            
            //tổng
            celSLDuoc_T.DataBindings.Add("Text", DataSource, "DuocSL1").FormatString = DungChung.Bien.FormatString[1];
            celTTDuoc_T.DataBindings.Add("Text", DataSource, "DuocTT1").FormatString = DungChung.Bien.FormatString[1];
            celVPDKSL_T.DataBindings.Add("Text", DataSource, "VPDKSL1").FormatString = DungChung.Bien.FormatString[1];
            celVPDKTT_T.DataBindings.Add("Text", DataSource, "VPDKTT1").FormatString = DungChung.Bien.FormatString[1];
            celBHYTSL_T.DataBindings.Add("Text", DataSource, "VPTKBHYTSL1").FormatString = DungChung.Bien.FormatString[1];
            celBHYTTT_T.DataBindings.Add("Text", DataSource, "VPTKBHYTTT1").FormatString = DungChung.Bien.FormatString[1];
            celKhongTTSL_T.DataBindings.Add("Text", DataSource, "VPTKKhongTTSL1").FormatString = DungChung.Bien.FormatString[1];
            celKhongTTTT_T.DataBindings.Add("Text", DataSource, "VPTKKhongTTTT1").FormatString = DungChung.Bien.FormatString[1];
            celDVSL_T.DataBindings.Add("Text", DataSource, "VPTKDVSL1").FormatString = DungChung.Bien.FormatString[1];
            celDVTT_T.DataBindings.Add("Text", DataSource, "VPTKDVTT1").FormatString = DungChung.Bien.FormatString[1];
            celVPCKSL_T.DataBindings.Add("Text", DataSource, "VPCKSL1").FormatString = DungChung.Bien.FormatString[1];
            celVPCKTT_T.DataBindings.Add("Text", DataSource, "VPCKTT1").FormatString = DungChung.Bien.FormatString[1];
            celVPTKBASL_T.DataBindings.Add("Text", DataSource, "VPTKBASL1").FormatString = DungChung.Bien.FormatString[1];
            celVPTKBATT_T.DataBindings.Add("Text", DataSource, "VPTKBATT1").FormatString = DungChung.Bien.FormatString[1];

            //GroupHeader1.GroupFields.Add(new GroupField("sttDichVu"));
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            i++;
            num = 0;
            celSTT.Text = i.ToString();
        }

        private void GroupHeader1_BeforePrint(object sender, CancelEventArgs e)
        {
            
        }

    }
}
