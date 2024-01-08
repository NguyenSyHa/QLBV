using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

namespace QLBV.BaoCao
{
    public partial class repBC_SoTiepDon : DevExpress.XtraReports.UI.XtraReport
    {
        public repBC_SoTiepDon()
        {
            InitializeComponent();
        }

        public void DataBinding()
        {
            //cellSTT.DataBindings.Add("Text", DataSource, "SoTT");
            cellMaBNhan.DataBindings.Add("Text", DataSource, "MaBNhan");
            cellTenBNhan.DataBindings.Add("Text", DataSource, "TenBNhan");
            cellNgaySinh.DataBindings.Add("Text", DataSource, "Ngaysinh");
            cellGioiTinh.DataBindings.Add("Text", DataSource, "GTinh");
            cellDChi.DataBindings.Add("Text", DataSource, "DChi");
            cellDTuong.DataBindings.Add("Text", DataSource, "DTuong");
            cellBHYT.DataBindings.Add("Text", DataSource, "SThe");
            cellSDT.DataBindings.Add("Text", DataSource, "DienThoai");
            cellCMT.DataBindings.Add("Text", DataSource, "CMT");
            cellNoiGioiThieu.DataBindings.Add("Text", DataSource, "CDNoiGT");
            cellKPhong.DataBindings.Add("Text", DataSource, "TenKP");
            cellTenBSKhamBenh.DataBindings.Add("Text", DataSource, "TenCB");
            cellNgayNhap.DataBindings.Add("Text", DataSource, "NNhap");
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            lblTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            lblTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            lblTieuDeSoTiepDon.Text = DungChung.Bien.TieuDeSoTiepDon;
        }
    }
}
