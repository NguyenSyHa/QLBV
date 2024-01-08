using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class Rep_ThTTChuyenTuyen_YS_30007 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_ThTTChuyenTuyen_YS_30007()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        public void BindingData()
        {
            colTenBNhan.DataBindings.Add("Text",DataSource,"TenBNhan");
            txtMaBNhan.DataBindings.Add("Text",DataSource,"MaBNhan");
            colNam.DataBindings.Add("Text",DataSource,"Nam");
            colNu.DataBindings.Add("Text",DataSource,"Nu");
            colChanDoan.DataBindings.Add("Text",DataSource,"ChanDoan");
            colBSChuyen.DataBindings.Add("Text", DataSource, "BSChuyen");
            colNgayChuyen.DataBindings.Add("Text", DataSource, "Ngaychuyen").FormatString = "{0:dd/MM/yyyy HH:mm:ss}";
            colBHYT.DataBindings.Add("Text", DataSource, "DTuong");
            colKP.DataBindings.Add("Text",DataSource,"MaKP");
            colNoiNhan.DataBindings.Add("Text", DataSource, "MaBVC");
            col1a.DataBindings.Add("Text", DataSource, "S1a");
            col1aT.DataBindings.Add("Text", DataSource, "S1a");
            col1b.DataBindings.Add("Text", DataSource, "S1b");
            col1bT.DataBindings.Add("Text", DataSource, "S1b");
            col2.DataBindings.Add("Text", DataSource, "S2");
            col2T.DataBindings.Add("Text", DataSource, "S2");
            col3.DataBindings.Add("Text", DataSource, "S3");
            col3T.DataBindings.Add("Text", DataSource, "S3");
            col4.DataBindings.Add("Text", DataSource, "S4");
            col4T.DataBindings.Add("Text", DataSource, "S4");
            col5.DataBindings.Add("Text", DataSource, "S5");
            col5T.DataBindings.Add("Text", DataSource, "S5"); 
            

        }
        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            colTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            colNguoiLapBang.Text = DungChung.Bien.NguoiLapBieu;
            colGiamDoc.Text = DungChung.Bien.GiamDoc;
        }

    
      
      

    }
}
