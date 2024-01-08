using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_bangkevp : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_bangkevp()
        {
            InitializeComponent();
        }
        public bool hientong;
        public void bindingdata() 
        {
         //p.TenBnhan, p.DChi, p.SoTien, ngaythu = Convert.ToDateTime(p.NgayThu).ToShortDateString() 
            hoten.DataBindings.Add("Text", DataSource, "TenBnhan");
            diachi.DataBindings.Add("Text", DataSource, "DChi");
            colSoHD.DataBindings.Add("Text", DataSource, "IDTamUng");
            bhyt.DataBindings.Add("Text", DataSource, "SoTien").FormatString = DungChung.Bien.FormatString[0];
            //Ngayht.DataBindings.Add("Text", DataSource, "ngaythu");
            GroupHeader1.GroupFields.Add(new GroupField("ngaythu"));
            tongtien.DataBindings.Add("Text", DataSource, "SoTien").FormatString = DungChung.Bien.FormatString[0];
            Tongtient.DataBindings.Add("Text", DataSource, "SoTien").FormatString = DungChung.Bien.FormatString[0];
            colSoHD.DataBindings.Add("Text", DataSource, "SoHD");
            soQ.DataBindings.Add("Text", DataSource, "QuyenHD");
       

        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            colCQ.Text = DungChung.Bien.TenCQ.ToUpper();
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            colNLB.Text = DungChung.Bien.NguoiLapBieu;
        }

        private void Ngayht_BeforePrint(object sender, CancelEventArgs e)
        {
            if (this.GetCurrentColumnValue("NgayThu") != null)
            {
                string date = this.GetCurrentColumnValue("NgayThu").ToString();
                Ngayht.Text = date.Substring(0,5);
            }

        }
    }
}
