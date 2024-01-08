using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_PhieuTongXuat_12122 : DevExpress.XtraReports.UI.XtraReport
    {
        private bool ht;

        public rep_PhieuTongXuat_12122()
        {
            InitializeComponent();
        }

        public rep_PhieuTongXuat_12122(bool ht)
        {
            InitializeComponent();
            this.ht = ht;
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            txtNgayThang.Text = "Ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;
            txtNguoiLapBieu.Text = DungChung.Bien.NguoiLapBieu;
            txtThuKho.Text = DungChung.Bien.ThuKho;
            txtKeToanTruong.Text = DungChung.Bien.KeToanTruong;
            txtGiamDoc.Text = DungChung.Bien.GiamDoc;
        }


        internal void Bindingdata()
        {
            celTN.DataBindings.Add("Text", DataSource, "TenTN");
            celTenDV.DataBindings.Add("Text", DataSource, "TenDV");
            celDVT.DataBindings.Add("Text", DataSource, "DonVi");
            celDonGia.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[1];
            celSL.DataBindings.Add("Text", DataSource, "SoLuong").FormatString = DungChung.Bien.FormatString[1];
            celThanhTien.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];
            celTongG.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];
            celThanhTienT.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];
            GroupHeader1.GroupFields.Add(new GroupField("TenTN"));
        }

        private void GroupHeader1_BeforePrint(object sender, CancelEventArgs e)
        {           
                GroupHeader1.Visible = ht;
        }
    }
}
