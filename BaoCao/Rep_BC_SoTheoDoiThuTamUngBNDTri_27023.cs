using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_BC_SoTheoDoiThuTamUngBNDTri_27023 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_BC_SoTheoDoiThuTamUngBNDTri_27023()
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
            celGroup.DataBindings.Add("Text", DataSource, "DTuong");

            celNgayVao.DataBindings.Add("Text", DataSource, "NgayVao").FormatString = "{0:dd/MM}";
            celDT.DataBindings.Add("Text", DataSource, "DT");
            celHoTen.DataBindings.Add("Text", DataSource, "HoTen");
            celDiaChi.DataBindings.Add("Text", DataSource, "DiaChi");
            celTU1.DataBindings.Add("Text", DataSource, "TULan1").FormatString = DungChung.Bien.FormatString[1];
            celTU2.DataBindings.Add("Text", DataSource, "TULan2").FormatString = DungChung.Bien.FormatString[1];
            celTU3.DataBindings.Add("Text", DataSource, "TULan3").FormatString = DungChung.Bien.FormatString[1];
            celTU4.DataBindings.Add("Text", DataSource, "TULan4").FormatString = DungChung.Bien.FormatString[1];
            celTongTU.DataBindings.Add("Text", DataSource, "TongTU").FormatString = DungChung.Bien.FormatString[1];
            celThanhToan.DataBindings.Add("Text", DataSource, "ThanhToan");
            celChiTra.DataBindings.Add("Text", DataSource, "ChiTra").FormatString = DungChung.Bien.FormatString[1];
            celThuThieu.DataBindings.Add("Text", DataSource, "ThuThieu").FormatString = DungChung.Bien.FormatString[1];

            celTU1_T.DataBindings.Add("Text", DataSource, "TULan1").FormatString = DungChung.Bien.FormatString[1];
            celTU2_T.DataBindings.Add("Text", DataSource, "TULan2").FormatString = DungChung.Bien.FormatString[1];
            celTU3_T.DataBindings.Add("Text", DataSource, "TULan3").FormatString = DungChung.Bien.FormatString[1];
            celTU4_T.DataBindings.Add("Text", DataSource, "TULan4").FormatString = DungChung.Bien.FormatString[1];
            celTongTU_T.DataBindings.Add("Text", DataSource, "TongTU").FormatString = DungChung.Bien.FormatString[1];
            celThanhToan_T.DataBindings.Add("Text", DataSource, "ThanhToan");
            celChiTra_T.DataBindings.Add("Text", DataSource, "ChiTra").FormatString = DungChung.Bien.FormatString[1];
            celThuThieu_T.DataBindings.Add("Text", DataSource, "ThuThieu").FormatString = DungChung.Bien.FormatString[1];

            celTU1_T.Summary.FormatString = DungChung.Bien.FormatString[1];
            celTU2_T.Summary.FormatString = DungChung.Bien.FormatString[1];
            celTU3_T.Summary.FormatString = DungChung.Bien.FormatString[1];
            celTU4_T.Summary.FormatString = DungChung.Bien.FormatString[1];
            celTongTU_T.Summary.FormatString = DungChung.Bien.FormatString[1];
            celThanhToan_T.Summary.FormatString = DungChung.Bien.FormatString[1];
            celChiTra_T.Summary.FormatString = DungChung.Bien.FormatString[1];
            celThuThieu_T.Summary.FormatString = DungChung.Bien.FormatString[1];
          
            GroupHeader1.GroupFields.Add(new GroupField("DTuong"));
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            celNgayThang.Text = DungChung.Bien.DiaDanh + ", ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;
            celNguoiLapBieu.Text = DungChung.Bien.NguoiLapBieu;
            celKTT.Text = DungChung.Bien.KeToanTruong;
            celGD.Text = DungChung.Bien.GiamDoc;
        }
    }
}
