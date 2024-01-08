using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_BCKSK_LaiXe_Mau2_24009 : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_BCKSK_LaiXe_Mau2_24009()
        {
            InitializeComponent();
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            lblTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            lab_TenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            lblngaythang.Text = (DungChung.Bien.MaBV == "24009" ? "Bắc Giang, ngày " : "Ngày ") + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;
            celGD.Text = DungChung.Bien.GiamDoc;
        }


        internal void databinding()
        {
            celTenCty.DataBindings.Add("Text", DataSource, "tenDVi");
            celTS.DataBindings.Add("Text", DataSource, "Tongso");
            celDuSK.DataBindings.Add("Text", DataSource, "dudksk");
            celKDuSK.DataBindings.Add("Text", DataSource, "Kdudksk");
            celSTTNhom.DataBindings.Add("Text", DataSource, "STTNhom");
            celTenNhom.DataBindings.Add("Text", DataSource, "TenNhom");
            GroupHeader1.GroupFields.Add(new GroupField("STTNhom"));
        }
    }
}
