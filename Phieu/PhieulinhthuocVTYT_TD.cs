using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class PhieulinhthuocVTYT_TD : DevExpress.XtraReports.UI.XtraReport
    {
        int _dongy = 0;
        public PhieulinhthuocVTYT_TD()
        {
            InitializeComponent();
        }
        public PhieulinhthuocVTYT_TD(int dy)
        {
            InitializeComponent();
            _dongy = dy;
        }
        public void BindingData()
        {
            colTenHH.DataBindings.Add("Text", DataSource, "TenDV");
            colsoluongyc.DataBindings.Add("Text", DataSource, "SoLuong").FormatString=DungChung.Bien.FormatString[1];
            if(DungChung.Bien.MaBV=="30002")
                colsoluongtp.DataBindings.Add("Text", DataSource, "SoLuong").FormatString = DungChung.Bien.FormatString[1];
            colMa.DataBindings.Add("Text", DataSource, "MaTam");
            colThuoc139.DataBindings.Add("Text", DataSource, "SoLuong139").FormatString = DungChung.Bien.FormatString[1];
            colBHYT.DataBindings.Add("Text", DataSource, "SoLuongBHYT").FormatString = DungChung.Bien.FormatString[1];
            colTE.DataBindings.Add("Text", DataSource, "SoLuongTE").FormatString = DungChung.Bien.FormatString[1];
            colThuPhi.DataBindings.Add("Text", DataSource, "SoLuongDichVu").FormatString = DungChung.Bien.FormatString[1];
            colDVT.DataBindings.Add("Text", DataSource, "DonVi");
            colTenBN.DataBindings.Add("Text", DataSource, "TenBNhan");
            colDiaChi.DataBindings.Add("Text", DataSource, "DChi");
            GroupHeader1.GroupFields.Add(new GroupField("TenBNhan"));

        }
        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
           txtTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
           txtTenCQCQ.Text= DungChung.Bien.TenCQCQ.ToUpper();
           if (_dongy == 6)
               GroupHeader1.Visible = true;
           
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            if (Ngaythang.Value != null && Ngaythang.Value.ToString() != "")
                txtNgayThang.Text = "Ngày ..... tháng ..... năm 20...";
                //txtNgayThang.Text = "Ngày " + System.DateTime.Now.Day + " tháng " + System.DateTime.Now.Month + " năm " + System.DateTime.Now.Year;
        }
    }
     
}
