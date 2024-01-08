using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_BCSDThuocGayNghien_24009 : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_BCSDThuocGayNghien_24009()
        {
            InitializeComponent();
        }
        public void BindingData()
        {
            cel_TenDV.DataBindings.Add("Text", DataSource, "TenDichVu");
            cel_DVT.DataBindings.Add("Text", DataSource, "DonVi");
            cel_TonDKSL.DataBindings.Add("Text", DataSource, "TonDKSL").FormatString = DungChung.Bien.FormatString[1];
            cel_MuaTKSL.DataBindings.Add("Text", DataSource, "NhapTKSL").FormatString = DungChung.Bien.FormatString[1];
            cel_TongSo.DataBindings.Add("Text", DataSource, "Tong").FormatString = DungChung.Bien.FormatString[1];//
            cel_XuatTrongKySL.DataBindings.Add("Text", DataSource, "XuatTKSL").FormatString = DungChung.Bien.FormatString[1];
            //cel_HHaoSL.DataBindings.Add("Text", DataSource, "XuatNoiTruSL").FormatString = DungChung.Bien.FormatString[1];
            cel_TonCKSL.DataBindings.Add("Text", DataSource, "TonCKSL").FormatString = DungChung.Bien.FormatString[1];
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            lblCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            lab_TenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            cel_GD.Text = DungChung.Bien.GiamDoc;
            cel_TruongKhoaDuoc.Text = DungChung.Bien.TruongKhoaDuoc;
            
        }
    }
}
