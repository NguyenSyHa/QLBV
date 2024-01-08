using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_BcTKSoLuotBNKNgT_HL03 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_BcTKSoLuotBNKNgT_HL03()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

        public void BindingData()
        {
            txtMaBNhan.DataBindings.Add("Text", DataSource, "MaBNhan");
            colTenBNhan.DataBindings.Add("Text", DataSource, "TenBNhan");
            colSThe.DataBindings.Add("Text", DataSource, "SThe");
            txtNgayKham.DataBindings.Add("Text", DataSource, "NgayTT");
            colBHTTde.DataBindings.Add("Text", DataSource, "TienBH").FormatString = DungChung.Bien.FormatString[1];
            colBHTTgf.DataBindings.Add("Text", DataSource, "TienBH").FormatString = DungChung.Bien.FormatString[1];
            colBHTTrf.DataBindings.Add("Text", DataSource, "TienBH").FormatString = DungChung.Bien.FormatString[1];
            colBNCCTde.DataBindings.Add("Text", DataSource, "TienBN").FormatString = DungChung.Bien.FormatString[1];
            colBNCCTgf.DataBindings.Add("Text", DataSource, "TienBN").FormatString = DungChung.Bien.FormatString[1];
            colBNCCTrf.DataBindings.Add("Text", DataSource, "TienBN").FormatString = DungChung.Bien.FormatString[1];
            colTCPde.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];
            colTCPgf.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];
            colTCPrf.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];

            GroupHeader1.GroupFields.Add(new GroupField("SThe"));
      
        }

        private void colNgayKhamde_BeforePrint(object sender, CancelEventArgs e)
        {
            if (GetCurrentColumnValue("NgayTT") != null && GetCurrentColumnValue("NgayTT").ToString().Length >= 10)
            {
                colNgayKhamde.Text =txtNgayKham.Text.Substring(0, 10);
            }
            else colNgayKhamde.Text = "";
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            colTenCQCQ.Text = DungChung.Bien.TenCQCQ;
            colTenCQ.Text = DungChung.Bien.TenCQ;
        }
    }
}
