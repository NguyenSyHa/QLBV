using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_BC_PhieuLuuHuyetNao_30009 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_BC_PhieuLuuHuyetNao_30009()
        {
            InitializeComponent();
        }

        public void BindingData() 
        {
            lblHoTen.DataBindings.Add("Text", DataSource, "HoTen");
            lblDiaChi.DataBindings.Add("Text", DataSource, "DiaChi");
            lblTheBHYT.DataBindings.Add("Text", DataSource, "STheBHYT");
            lblKhoa.DataBindings.Add("Text", DataSource, "Khoa");
            lblBuong.DataBindings.Add("Text", DataSource, "Buong");
            lblGiuong.DataBindings.Add("Text", DataSource, "Giuong");
            lblChanDoan.DataBindings.Add("Text", DataSource, "ChanDoan");
            cel_YeuCau.DataBindings.Add("Text", DataSource, "YeuCau");
            celBSChiDinh.DataBindings.Add("Text", DataSource, "BSChiDinh");
            cel_KQ.DataBindings.Add("Text", DataSource, "KetQua");
            cell_KL.DataBindings.Add("Text", DataSource, "KetLuan");
            lblLoiDan.DataBindings.Add("Text", DataSource, "LoiDan");
            celBSCK.DataBindings.Add("Text", DataSource, "BSCK");
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            lblTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            lblTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
        }
    }
}
