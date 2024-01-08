using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_DanhmucthuocSDCSDKCB : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_DanhmucthuocSDCSDKCB()
        {
            InitializeComponent();
        }
        public void inbaocao() 
        {
            CQ.Text = CQ2.Text = DungChung.Bien.TenCQ;
            CQCQ.Text = CQCQ2.Text = DungChung.Bien.TenCQCQ;
            mathuoc.DataBindings.Add("Text",DataSource,"SoTTqd");
            Mathuocbv.DataBindings.Add("Text", DataSource, "MaTam");
            hoachat.DataBindings.Add("Text", DataSource, "TenHC");
            maduongdung.DataBindings.Add("Text", DataSource, "");
            duongdung.DataBindings.Add("Text", DataSource, "DuongD");
            hamluong.DataBindings.Add("Text", DataSource, "HamLuong");
            tenthuoc.DataBindings.Add("Text", DataSource, "TenDV");
            sodangky.DataBindings.Add("Text", DataSource, "SoDK");
            donggoi.DataBindings.Add("Text", DataSource, "QCPC");
            donvitinh.DataBindings.Add("Text", DataSource, "DonVi");
            dongia.DataBindings.Add("Text", DataSource, "DonGia");
            soluong.DataBindings.Add("Text", DataSource, "");
            macsdkcb.Text = DungChung.Bien.MaBV;
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            xrPictureBox1.Image = DungChung.Ham.GetLogo();
            if (DungChung.Bien.MaBV == "24272")
            {
                SubBand1.Visible = false;
                SubBand2.Visible = true;
            }
        }
    }
}
