using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_BCTuan : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_BCTuan()
        {
            InitializeComponent();
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            txtTenBV.Text = DungChung.Bien.TenCQ.ToUpper();
        }

        public void BindingData()
        {
            GroupHeader1.GroupFields.Add(new GroupField("STT"));
            celstt.DataBindings.Add("Text", DataSource, "STT");
            noidung.DataBindings.Add("Text", DataSource, "Noidung");
            tongso.DataBindings.Add("Text", DataSource, "Tong");
            BH.DataBindings.Add("Text", DataSource, "Bh");
            TP.DataBindings.Add("Text", DataSource, "Tp");
            PK.DataBindings.Add("Text", DataSource, "Pk");
            noi.DataBindings.Add("Text", DataSource, "Noi");
            ngoai.DataBindings.Add("Text", DataSource, "Ngoai");

            celtedv.DataBindings.Add("Text", DataSource, "NoidungCT");
            celslg.DataBindings.Add("Text", DataSource, "TongCT");
        }

        private void GroupHeader1_BeforePrint(object sender, CancelEventArgs e)
        {
            if (this.GetCurrentColumnValue("STT") != null)
            {
                string nt = this.GetCurrentColumnValue("STT").ToString();
                int stt = 0;
                stt = Convert.ToInt32(nt);
                if (stt<20)
                    Detail.Visible = false;
                else
                    Detail.Visible = true;
            }
            if (this.GetCurrentColumnValue("Noidung") != null)
            {
                string nt = this.GetCurrentColumnValue("Noidung").ToString();
                if(nt=="Tổng số dịch vụ kỹ thuật làm TB - CN")
                {
                    this.noidung.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
                }
            }
        }
    }
}
