using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_BCSDDuocLieu_ViThuocYHCT : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_BCSDDuocLieu_ViThuocYHCT()
        {
            InitializeComponent();
        }
        public void BindingData()
        {
            cel_TenDV.DataBindings.Add("Text", DataSource, "TenDichVu");
            cel_TenHC.DataBindings.Add("Text", DataSource, "TenHC");
            celTrongNuoc.DataBindings.Add("Text", DataSource, "ThuocNam");
            celNgoaiNuoc.DataBindings.Add("Text", DataSource, "ThuocBac");
            cel_DVT.DataBindings.Add("Text", DataSource, "DonVi");//
            celSoLuong.DataBindings.Add("Text", DataSource, "SoLuong").FormatString = DungChung.Bien.FormatString[1];
            //cel_HHaoSL.DataBindings.Add("Text", DataSource, "XuatNoiTruSL").FormatString = DungChung.Bien.FormatString[1];
         
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            lblCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            lab_TenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            cel_GD.Text = DungChung.Bien.GiamDoc;
           
            
        }
    }
}
