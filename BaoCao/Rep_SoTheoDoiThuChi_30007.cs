using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_SoTheoDoiThuChi_30007 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_SoTheoDoiThuChi_30007()
        {
            InitializeComponent();
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            celTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            celTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
        }

        public void BindingData()
        {

            celNgayVao.DataBindings.Add("Text", DataSource, "NgayVao").FormatString = "{0:dd/MM}";
            celDT.DataBindings.Add("Text", DataSource, "DT");
            celHoTen.DataBindings.Add("Text", DataSource, "HoTen");
            celDiaChi.DataBindings.Add("Text", DataSource, "DiaChi");
            celTongTU.DataBindings.Add("Text", DataSource, "TongTU").FormatString = DungChung.Bien.FormatString[1];
            celThanhToan.DataBindings.Add("Text", DataSource, "ThanhToan").FormatString = DungChung.Bien.FormatString[1];
            celChiTra.DataBindings.Add("Text", DataSource, "ChiTra").FormatString = DungChung.Bien.FormatString[1];
            celThuThieu.DataBindings.Add("Text", DataSource, "ThuThieu").FormatString = DungChung.Bien.FormatString[1];

            celTongTU_T.DataBindings.Add("Text", DataSource, "TongTU");
            celThanhToan_T.DataBindings.Add("Text", DataSource, "ThanhToan");
            celChiTra_T.DataBindings.Add("Text", DataSource, "ChiTra");
            celThuThieu_T.DataBindings.Add("Text", DataSource, "ThuThieu");

            celTongTU_T.Summary.FormatString = DungChung.Bien.FormatString[1];
            celThanhToan_T.Summary.FormatString = DungChung.Bien.FormatString[1];
            celChiTra_T.Summary.FormatString = DungChung.Bien.FormatString[1];
            celThuThieu_T.Summary.FormatString = DungChung.Bien.FormatString[1];
          
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            celNgayThang.Text = DungChung.Bien.DiaDanh + "," + DungChung.Ham.NgaySangChu(DateTime.Now, DungChung.Bien.FormatDate);
            celNguoiLapBieu.Text = DungChung.Bien.NguoiLapBieu;
            celKTT.Text = DungChung.Bien.KeToanTruong;
            celGD.Text = DungChung.Bien.GiamDoc;
        }
    }
}
