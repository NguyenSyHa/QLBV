using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_bcTongHopThuTheoTungPhongCLS : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_bcTongHopThuTheoTungPhongCLS()
        {
            InitializeComponent();
        }
        public void Bindding()
        {
            databindding(colTenBN, "TenBN");
            databindding(colTuoi, "Tuoi");
            databindding(colNoiGui, "NoiGui");
            databindding(colMaChiTieu, "MaChiTieu");
            databindding(colTenChiTieu, "TenChiTieu");
            databindding(colSoHD, "SoHD");
            databindding(colCBCD, "Tencbcd");
            databindding(colCBTH, "Tencbth");
            colTongTien.DataBindings.Add("Text", DataSource, "TongTien").FormatString = DungChung.Bien.FormatString[1];
            colGiam.DataBindings.Add("Text", DataSource, "Giam").FormatString = DungChung.Bien.FormatString[1];
            colThucThu.DataBindings.Add("Text", DataSource, "ThucThu").FormatString = DungChung.Bien.FormatString[1];
            colSoLuong.DataBindings.Add("Text", DataSource, "soluong").FormatString = DungChung.Bien.FormatString[1];
            
            databindding(colDoiTuong, "DoiTuong");
            databindding(colPhongCLS, "KPth");
            databindding(colDichVu, "Tendv");

            GroupHeader3.GroupFields.Add(new GroupField("DoiTuong"));
            GroupHeader2.GroupFields.Add(new GroupField("KPth"));
            GroupHeader1.GroupFields.Add(new GroupField("Tendv"));

            colTSoLuong.DataBindings.Add("Text", DataSource, "soluong");
            colTSoLuong.Summary.FormatString = DungChung.Bien.FormatString[1];
            colTSoLuong2.DataBindings.Add("Text", DataSource, "soluong");
            colTSoLuong2.Summary.FormatString = DungChung.Bien.FormatString[1];

            colTTongTien.DataBindings.Add("Text", DataSource, "TongTien");
            colTTongTien.Summary.FormatString = DungChung.Bien.FormatString[1];
            colTTongTien2.DataBindings.Add("Text", DataSource, "TongTien");
            colTTongTien2.Summary.FormatString = DungChung.Bien.FormatString[1];

            colTGiam.DataBindings.Add("Text", DataSource, "Giam");
            colTGiam.Summary.FormatString = DungChung.Bien.FormatString[1];
            colTGiam2.DataBindings.Add("Text", DataSource, "Giam");
            colTGiam2.Summary.FormatString = DungChung.Bien.FormatString[1];

            colTThucThu.DataBindings.Add("Text", DataSource, "ThucThu");
            colTThucThu.Summary.FormatString = DungChung.Bien.FormatString[1];
            colTThucThu2.DataBindings.Add("Text", DataSource, "ThucThu");
            colTThucThu2.Summary.FormatString = DungChung.Bien.FormatString[1];


        }
        private void databindding(XRTableCell cell, string data)
        {
            cell.DataBindings.Add("Text", DataSource, data);
        }
    }
}
