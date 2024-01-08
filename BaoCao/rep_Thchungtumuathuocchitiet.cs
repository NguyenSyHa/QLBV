using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_Thchungtumuathuocchitiet : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_Thchungtumuathuocchitiet()
        {
            InitializeComponent();
            if (DungChung.Bien.MaBV == "30002")
            {
                SubBand1.Visible = false;
            }
            else
            {
                SubBand2.Visible = false;
            }
        }
        public void databinding()
        {
            col_madv.DataBindings.Add("Text", DataSource, "MaDV");
            col_cc.DataBindings.Add("Text", DataSource, "TenCC");   
            col_tendv.DataBindings.Add("Text", DataSource, "TenDV");
            col_soluong.DataBindings.Add("Text", DataSource, "soluong").FormatString = DungChung.Bien.FormatString[0];
            col_sltong.DataBindings.Add("Text", DataSource, "soluong").FormatString = DungChung.Bien.FormatString[0];
            colsltong.DataBindings.Add("Text", DataSource, "soluong").FormatString = DungChung.Bien.FormatString[0];
            col_thanhtien.DataBindings.Add("Text", DataSource, "TT").FormatString = DungChung.Bien.FormatString[1];
            col_tttong.DataBindings.Add("Text", DataSource, "TT").FormatString = DungChung.Bien.FormatString[1];
            coltttong.DataBindings.Add("Text", DataSource, "TT").FormatString = DungChung.Bien.FormatString[1];
            coltong.DataBindings.Add("Text", DataSource, "TT").FormatString = DungChung.Bien.FormatString[1];
            col_dongia.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[1];
            col_donvi.DataBindings.Add("Text", DataSource, "DonVi");           
            col_soct.DataBindings.Add("Text", DataSource, "SoCT");
            GroupHeader1.GroupFields.Add(new GroupField("NgayNhap"));
            GroupHeader1.GroupFields.Add(new GroupField("SoCT"));
            lblSoLo.DataBindings.Add("Text", DataSource, "Solo");
            lblHanDung.DataBindings.Add("Text", DataSource, "handung").FormatString = "{0:dd/MM/yyyy}";
            col_ngay.DataBindings.Add("Text", DataSource, "NgayNhap").FormatString = "{0:dd/MM/yyyy}";
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            
        }
    }
}
