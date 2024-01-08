using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class repBbKKThuoc_ThanhTien_27021 : DevExpress.XtraReports.UI.XtraReport
    {
        public repBbKKThuoc_ThanhTien_27021()
        {
            InitializeComponent();
        }

        public void BindingData()
        {
            //txtNT.DataBindings.Add("Text", DataSource, "NgayThang");
            //txtCT.DataBindings.Add("Text", DataSource, "SoCT");
            colTenThuocGh1.DataBindings.Add("Text", DataSource, "TenTieuNhom");
            colTenThuoc.DataBindings.Add("Text", DataSource, "TenDV");
            colDonVi.DataBindings.Add("Text", DataSource, "DonVi");

            celDonGia.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[1];

            colSoLuongTT.DataBindings.Add("Text", DataSource, "SoLuongSS").FormatString = DungChung.Bien.FormatString[0];
            colTTTT.DataBindings.Add("Text", DataSource, "ThanhTienSS").FormatString = DungChung.Bien.FormatString[1];
            colTTTTgh.DataBindings.Add("Text", DataSource, "ThanhTienSS").FormatString = DungChung.Bien.FormatString[1];
            colTTTTtc.DataBindings.Add("Text", DataSource, "ThanhTienSS").FormatString = DungChung.Bien.FormatString[1];

            if (DungChung.Bien.MaBV != "20001")
                colHongVo.DataBindings.Add("Text", DataSource, "HongVo").FormatString = DungChung.Bien.FormatString[1];
            colHongVoTong.DataBindings.Add("Text", DataSource, "HongVo").FormatString = DungChung.Bien.FormatString[1];


            GroupHeader1.GroupFields.Add(new GroupField("TenTieuNhom"));

        }

        private void repBbKKThuoc_BeforePrint(object sender, CancelEventArgs e)
        {
            TenCQCQ.Value = DungChung.Bien.TenCQCQ;
            TenCQ.Value = DungChung.Bien.TenCQ;
            string _nt= NT.Value.ToString();
            string _ct = NT.Value.ToString(); 
           
           
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            colTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            colTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            //xrRichText1.Text = TV1goi.Value.ToString();
        }

        private void GroupHeader3_BeforePrint(object sender, CancelEventArgs e)
        {
            if (this.GetCurrentColumnValue("NgayThang") != null)
            {
                string nt = this.GetCurrentColumnValue("NgayThang").ToString();
            }
        }

        private void GroupHeader2_BeforePrint(object sender, CancelEventArgs e)
        {
            if (this.GetCurrentColumnValue("SoCT") != null)
            {
                string ct = this.GetCurrentColumnValue("SoCT").ToString();
               

            }
        }

        private void colHanDung_BeforePrint(object sender, CancelEventArgs e)
        {
         

        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
           
                xrTable4.Visible = true;
          

        }

       
    }
}
