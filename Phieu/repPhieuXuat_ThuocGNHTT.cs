using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class repPhieuXuat_ThuocGNHTT : DevExpress.XtraReports.UI.XtraReport
    {
        public repPhieuXuat_ThuocGNHTT()
        {
            InitializeComponent();
        }
           QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        public void BindingData()
        {
            //colTenHH.DataBindings.Add("Text", DataSource, "TenDV");
            colTenDV.DataBindings.Add("Text", DataSource, "TenDV");
            colDVT.DataBindings.Add("Text", DataSource, "DonVi");
            colSLX.DataBindings.Add("Text", DataSource, "SoLuongX").FormatString = DungChung.Bien.FormatString[1];
            txtLoSX.DataBindings.Add("Text", DataSource, "SoLo").FormatString = DungChung.Bien.FormatString[1];
            txtHanDung.DataBindings.Add("Text", DataSource, "HanDung").FormatString = DungChung.Bien.FormatString[1];
            txtNhaSX.DataBindings.Add("Text", DataSource, "NhaSX").FormatString = DungChung.Bien.FormatString[1];
            txtNuocSX.DataBindings.Add("Text", DataSource, "NuocSX").FormatString = DungChung.Bien.FormatString[1];
            colGhiChu.DataBindings.Add("Text", DataSource, "GhiChu").FormatString = DungChung.Bien.FormatString[1];
       
        }
     
        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            TenDV.Value = DungChung.Bien.TenCQ;
            Diachi.Value = DungChung.Bien.DiaChi;
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
          
            colNguoiGiao.Text = DungChung.Bien.NguoiLapBieu;
            colTTDV.Text = DungChung.Bien.GiamDoc;
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            string solo = ""; string handung = ""; ; string nhasx = ""; string nuocsx = "";
            if (this.GetCurrentColumnValue("SoLo") != null && this.GetCurrentColumnValue("SoLo") != "")
            {
                 solo = this.GetCurrentColumnValue("SoLo").ToString();
            }
            if (this.GetCurrentColumnValue("HanDung") != null && this.GetCurrentColumnValue("HanDung") != "")
            {
                 handung = this.GetCurrentColumnValue("HanDung").ToString();
            }
            if (this.GetCurrentColumnValue("NhaSX") != null && this.GetCurrentColumnValue("NhaSX") != "")
            {
                 nhasx = this.GetCurrentColumnValue("NhaSX").ToString();
            }
            if (this.GetCurrentColumnValue("NuocSX") != null && this.GetCurrentColumnValue("NuocSX") != "")
            {
                 nuocsx = this.GetCurrentColumnValue("NuocSX").ToString();
            }
            if (solo != null&&solo!="" && handung != null&& handung!="") { colSoLoHD.Text = solo.ToString() +" - "+ handung.ToString().Substring(0, 10); }
            if (solo != null&&solo!="" && (handung == null||handung=="")) { colSoLoHD.Text = solo.ToString(); }
            if ((solo == null||solo=="") && handung != null&&handung!="") { colSoLoHD.Text = handung.ToString().Substring(0, 10); }

            if (nhasx != null && nhasx != "" && nuocsx != null && nuocsx != "") { colNhaSX.Text = nhasx.ToString() + " - " + nuocsx.ToString(); }
            if (nhasx != null && nhasx != "" && (nuocsx == null || nuocsx == "")) { colNhaSX.Text = nhasx.ToString(); }
            if ((nhasx == null || nhasx == "") && nuocsx != null && nuocsx != "") { colNhaSX.Text = nuocsx.ToString(); }
        }

       

    }
}
