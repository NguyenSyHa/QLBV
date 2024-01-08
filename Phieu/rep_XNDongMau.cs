using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_XNDongMau : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_XNDongMau()
        {
            InitializeComponent();
        }



        internal void BindingData()
        {
            celTenDV.DataBindings.Add("Text", DataSource, "TenTN");
            GroupHeader1.GroupFields.Add(new GroupField("TenTN"));
            celTenDVct.DataBindings.Add("Text", DataSource, "TenDVct");            
            celChiSobt.DataBindings.Add("Text", DataSource, "TSBT");           
            if(DungChung.Bien.MaBV == "27023")
            {
                celKQ.DataBindings.Add("Text", DataSource, "DonVi");
                celDonVi.DataBindings.Add("Text", DataSource, "KetQua");
            }
            else
            {
                celKQ.DataBindings.Add("Text", DataSource, "KetQua");
                celDonVi.DataBindings.Add("Text", DataSource, "DonVi");

            }
            GroupHeader1.GroupFields.Add(new GroupField("TenTN"));
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            if(DungChung.Bien.MaBV=="14017")
            {
                SubBand1.Visible = false;
                xrTableCell5.Text = "KHOA XÉT NGHIỆM";
            }
            else
            {
                SubBand2.Visible = false;
            }
            if (DungChung.Bien.MaBV == "27023")
            {
                celTitle.Text = "Phiếu xét nghiệm chẩn đoán rối loạn đông cầm máu".ToUpper();
                this.celTitle.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                xrTableRow12.Visible = true;
                xrTableCell2.Text = "Tên xét nghiệm";
                lbl3.Text = "Đơn vị";
                lbl5.Text = "Kết quả của bệnh nhân";
                xrTable9.Visible = true;
                GroupHeader1.Visible = false;
            }
            else
            {
              
                GroupHeader1.Visible = true;
            }
            if (DungChung.Bien.MaBV == "26007")
                xrBarCode1.Visible = true;
            if (DungChung.Bien.MaBV != "12345" && DungChung.Bien.MaBV != "24297")
            {
                celNamSinh.Visible = false;
            }
          
        }
        int stt = 0;
        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "27022" || DungChung.Bien.MaBV=="14017")
            {
                stt++;
                celSTT.Text = stt.ToString();
            }
        }
    }
}
