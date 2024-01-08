using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_baocaotienthucungchicha : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_baocaotienthucungchicha()
        {
            InitializeComponent();
        }
        //.FormatString = "{0: dd/mm/yyyy}";
        public void binhdin() 
        { 
            hten.DataBindings.Add("Text", DataSource, "TenBNhan");
            diachibvc.DataBindings.Add("Text", DataSource, "DChi");
            colSoHD.DataBindings.Add("Text", DataSource, "idVPhi");
            bhyt.DataBindings.Add("Text", DataSource, "SoTien").FormatString = DungChung.Bien.FormatString[0];
           // Ngayht.DataBindings.Add("Text", DataSource, "ngayTT").FormatString = "{0: dd/mm}";
            GroupHeader2.GroupFields.Add(new GroupField("NgayThu"));
            tongtien.DataBindings.Add("Text", DataSource, "SoTien").FormatString = DungChung.Bien.FormatString[0];
            Tongtient.DataBindings.Add("Text", DataSource, "SoTien").FormatString = DungChung.Bien.FormatString[0];
            QHD.DataBindings.Add("Text", DataSource, "QuyenHD");
            colSoHD.DataBindings.Add("Text", DataSource, "SoHD");
            
           
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            colNLB.Text = DungChung.Bien.NguoiLapBieu;
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            colTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
        }

        private void Ngayht_BeforePrint(object sender, CancelEventArgs e)
        {
            if (this.GetCurrentColumnValue("NgayThu") != null)
            {
                string date = this.GetCurrentColumnValue("NgayThu").ToString();
                Ngayht.Text = date.Substring(0, 5);
            }
        }
       
        
        
        }
    }

