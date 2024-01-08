using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_BC_ThongKeTaiNanThuongTich : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_BC_ThongKeTaiNanThuongTich()
        {
            InitializeComponent();
        }

        public void BindingData()
        {
            gr_STT.DataBindings.Add("Text", DataSource, "Stt");
            gr_TenNhom.DataBindings.Add("Text", DataSource, "TenNhom");
            gr_Tong_M.DataBindings.Add("Text", DataSource, "Tong_M");
            gr_Tong_C.DataBindings.Add("Text", DataSource, "Tong_C");
            gr_TongNu_C.DataBindings.Add("Text", DataSource, "TongNu_C");
            gr_TongNu_M.DataBindings.Add("Text", DataSource, "TongNu_M");
            gr_4Tuoi_M.DataBindings.Add("Text", DataSource, "Duoi4Tuoi_M");
            gr_4Tuoi_C.DataBindings.Add("Text", DataSource, "Duoi4Tuoi_C");
            gr_Nu4Tuoi_M.DataBindings.Add("Text", DataSource, "NuDuoi4Tuoi_M");
            gr_Nu4Tuoi_C.DataBindings.Add("Text", DataSource, "NuDuoi4Tuoi_C");
            gr_14Tuoi_M.DataBindings.Add("Text", DataSource, "Duoi14Tuoi_M");
            gr_14Tuoi_C.DataBindings.Add("Text", DataSource, "Duoi14Tuoi_C");
            gr_Nu14Tuoi_M.DataBindings.Add("Text", DataSource, "NuDuoi14Tuoi_M");
            gr_Nu14Tuoi_C.DataBindings.Add("Text", DataSource, "NuDuoi14Tuoi_C");
            gr_19Tuoi_C.DataBindings.Add("Text", DataSource, "Duoi19Tuoi_C");
            gr_19Tuoi_M.DataBindings.Add("Text", DataSource, "Duoi19Tuoi_M");
            gr_Nu19Tuoi_M.DataBindings.Add("Text", DataSource, "NuDuoi19Tuoi_M");
            gr_Nu19Tuoi_C.DataBindings.Add("Text", DataSource, "NuDuoi19Tuoi_C");
            gr_60Tuoi_M.DataBindings.Add("Text", DataSource, "Duoi60Tuoi_M");
            gr_60Tuoi_C.DataBindings.Add("Text", DataSource, "Duoi60Tuoi_C");
            gr_Nu60Tuoi_M.DataBindings.Add("Text", DataSource, "NuDuoi60Tuoi_M");
            gr_Nu60Tuoi_C.DataBindings.Add("Text", DataSource, "NuDuoi60Tuoi_C");
            gr_Tren60Tuoi_C.DataBindings.Add("Text", DataSource, "Tren60Tuoi_C");
            gr_Tren60Tuoi_M.DataBindings.Add("Text", DataSource, "Tren60Tuoi_M");
            gr_NuTren60_M.DataBindings.Add("Text", DataSource, "NuTren60Tuoi_M");
            gr_NuTren60_C.DataBindings.Add("Text", DataSource, "NuTren60Tuoi_C");

            cel_ChiTiet.DataBindings.Add("Text", DataSource, "ChiTiet");
            cel_Tong_M.DataBindings.Add("Text", DataSource, "Tong_M");
            cel_Tong_C.DataBindings.Add("Text", DataSource, "Tong_C");
            cel_TongNu_C.DataBindings.Add("Text", DataSource, "TongNu_C");
            cel_TongNu_M.DataBindings.Add("Text", DataSource, "TongNu_M");
            cel_4Tuoi_M.DataBindings.Add("Text", DataSource, "Duoi4Tuoi_M");
            cel_4Tuoi_C.DataBindings.Add("Text", DataSource, "Duoi4Tuoi_C");
            cel_Nu4Tuoi_M.DataBindings.Add("Text", DataSource, "NuDuoi4Tuoi_M");
            cel_Nu4Tuoi_C.DataBindings.Add("Text", DataSource, "NuDuoi4Tuoi_C");
            cel_14Tuoi_M.DataBindings.Add("Text", DataSource, "Duoi14Tuoi_M");
            cel_14Tuoi_C.DataBindings.Add("Text", DataSource, "Duoi14Tuoi_C");
            cel_Nu14Tuoi_M.DataBindings.Add("Text", DataSource, "NuDuoi14Tuoi_M");
            cel_Nu14Tuoi_C.DataBindings.Add("Text", DataSource, "NuDuoi14Tuoi_C");
            cel_19Tuoi_C.DataBindings.Add("Text", DataSource, "Duoi19Tuoi_C");
            cel_19Tuoi_M.DataBindings.Add("Text", DataSource, "Duoi19Tuoi_M");
            cel_Nu19Tuoi_M.DataBindings.Add("Text", DataSource, "NuDuoi19Tuoi_M");
            cel_Nu19Tuoi_C.DataBindings.Add("Text", DataSource, "NuDuoi19Tuoi_C");
            cel_60Tuoi_M.DataBindings.Add("Text", DataSource, "Duoi60Tuoi_M");
            cel_60Tuoi_C.DataBindings.Add("Text", DataSource, "Duoi60Tuoi_C");
            cel_Nu60Tuoi_M.DataBindings.Add("Text", DataSource, "NuDuoi60Tuoi_M");
            cel_Nu60Tuoi_C.DataBindings.Add("Text", DataSource, "NuDuoi60Tuoi_C");
            cel_Tren60_C.DataBindings.Add("Text", DataSource, "Tren60Tuoi_C");
            cel_Tren60_M.DataBindings.Add("Text", DataSource, "Tren60Tuoi_M");
            cel_NuTren60_M.DataBindings.Add("Text", DataSource, "NuTren60Tuoi_M");
            cel_NuTren60_C.DataBindings.Add("Text", DataSource, "NuTren60Tuoi_C");

            GroupHeader1.GroupFields.Add(new GroupField("Stt"));
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            if (this.GetCurrentColumnValue("ChiTiet") != null)
            {
                string nt = this.GetCurrentColumnValue("ChiTiet").ToString();
                if (nt == "")
                    xrTableRow5.Visible = false;
                else
                    xrTableRow5.Visible = true;

            }
        }
    }
}
