using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_DsTonGoiThuoc : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_DsTonGoiThuoc()
        {
            InitializeComponent();
        }

        public Rep_DsTonGoiThuoc(bool htbn)
        {
            InitializeComponent();
            this.htbn = htbn;
        }
        int i = 0;// stt nhóm
        int num = 0;//stt  số stt trong BC
        private bool htbn;
        public void BindingData()
        {
            //details
            colTenBN.DataBindings.Add("Text", DataSource, "TenBN");
           // lblTenDV.DataBindings.Add("Text", DataSource, "TenDV1");            
            colDuocSL.DataBindings.Add("Text", DataSource, "DuocSL1").FormatString = DungChung.Bien.FormatString[1];
            colDuocTT.DataBindings.Add("Text", DataSource, "DuocTT1").FormatString = DungChung.Bien.FormatString[1];
            colVPDKSL.DataBindings.Add("Text", DataSource, "VPDKSL1").FormatString = DungChung.Bien.FormatString[1];
            colVPDKTT.DataBindings.Add("Text", DataSource, "VPDKTT1").FormatString = DungChung.Bien.FormatString[1];
            colVPTKSL.DataBindings.Add("Text", DataSource, "VPTKSL1").FormatString = DungChung.Bien.FormatString[1];
            colVPTKTT.DataBindings.Add("Text", DataSource, "VPTKTT1").FormatString = DungChung.Bien.FormatString[1];
            colVPCKSL.DataBindings.Add("Text", DataSource, "VPCKSL1").FormatString = DungChung.Bien.FormatString[1];
            colVPCKTT.DataBindings.Add("Text", DataSource, "VPCKTT1").FormatString = DungChung.Bien.FormatString[1];
            colVPTKBASL.DataBindings.Add("Text", DataSource, "VPTKBASL1").FormatString = DungChung.Bien.FormatString[1];
            colVPTKBATT.DataBindings.Add("Text", DataSource, "VPTKBATT1").FormatString = DungChung.Bien.FormatString[1];

            //group
            celSTT.DataBindings.Add("Text", DataSource, "sttDichVu");
            colTenDVG.DataBindings.Add("Text", DataSource, "TenDV1");
            colDVTG.DataBindings.Add("Text", DataSource, "DVT1");
            colDonGiaG.DataBindings.Add("Text", DataSource, "DonGia1").FormatString = DungChung.Bien.FormatString[1];
            colDuocSLG.DataBindings.Add("Text", DataSource, "DuocSL1").FormatString = DungChung.Bien.FormatString[1];
            colDuocTTG.DataBindings.Add("Text", DataSource, "DuocTT1").FormatString = DungChung.Bien.FormatString[1];
            colVPDKSLG.DataBindings.Add("Text", DataSource, "VPDKSL1").FormatString = DungChung.Bien.FormatString[1];
            colVPDKTTG.DataBindings.Add("Text", DataSource, "VPDKTT1").FormatString = DungChung.Bien.FormatString[1];
            colVPTKSLG.DataBindings.Add("Text", DataSource, "VPTKSL1").FormatString = DungChung.Bien.FormatString[1];
            colVPTKTTG.DataBindings.Add("Text", DataSource, "VPTKTT1").FormatString = DungChung.Bien.FormatString[1];
            colVPCKSLG.DataBindings.Add("Text", DataSource, "VPCKSL1").FormatString = DungChung.Bien.FormatString[1];
            colVPCKTTG.DataBindings.Add("Text", DataSource, "VPCKTT1").FormatString = DungChung.Bien.FormatString[1];
            colVPBATKSL.DataBindings.Add("Text", DataSource, "VPTKBASL1").FormatString = DungChung.Bien.FormatString[1];
            colVPBATKTT.DataBindings.Add("Text", DataSource, "VPTKBATT1").FormatString = DungChung.Bien.FormatString[1];

            //report
            colDuocTTT.DataBindings.Add("Text", DataSource, "DuocTT1").FormatString = DungChung.Bien.FormatString[1];
            colVPDKTTT.DataBindings.Add("Text", DataSource, "VPDKTT1").FormatString = DungChung.Bien.FormatString[1];
            colVPTKTTT.DataBindings.Add("Text", DataSource, "VPTKTT1").FormatString = DungChung.Bien.FormatString[1];
            colVPCKTTT.DataBindings.Add("Text", DataSource, "VPCKTT1").FormatString = DungChung.Bien.FormatString[1];
            colVPTKBATTT.DataBindings.Add("Text", DataSource, "VPTKBATT1").FormatString = DungChung.Bien.FormatString[1];

            GroupHeader1.GroupFields.Add(new GroupField("sttDichVu"));
        }
        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            colTenCQ.Text = DungChung.Bien.TenCQ;
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            colTTDV.Text = DungChung.Bien.NguoiLapBieu;
        }

        private void GroupHeader1_BeforePrint(object sender, CancelEventArgs e)
        {
            i++;
            num = 0;
            colSTTG.Text = i.ToString();
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            if (htbn)
            {
                if (this.GetCurrentColumnValue("MaBNhan") != null)
                {
                    if (this.GetCurrentColumnValue("MaBNhan").ToString() == "-1")
                    {
                        xrTableRow2.Visible = false;
                    }
                    else
                    {
                        num++;
                        celSTTrep.Text = num.ToString();
                        xrTableRow2.Visible = true;
                    }
                }
            }
            else
                xrTableRow2.Visible = false;
        }

    }
}
