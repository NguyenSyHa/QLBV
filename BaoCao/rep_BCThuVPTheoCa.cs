using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_BCThuVPTheoCa : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_BCThuVPTheoCa()
        {
            InitializeComponent();
            
        }
       // int _row = 0;

        internal void BindingData()
        {
            celKhoa.DataBindings.Add("Text", DataSource, "KP");
            celHoTen.DataBindings.Add("Text", DataSource, "TenBNhan");
            celSoVV.DataBindings.Add("Text", DataSource, "SoVV");
            celKham.DataBindings.Add("Text", DataSource, "Kham").FormatString = DungChung.Bien.FormatString[1];
            celDichVu.DataBindings.Add("Text", DataSource, "DichVu").FormatString = DungChung.Bien.FormatString[1];
            celThuoc.DataBindings.Add("Text", DataSource, "Thuoc").FormatString = DungChung.Bien.FormatString[1];
            celTamUng.DataBindings.Add("Text", DataSource, "TamUng").FormatString = DungChung.Bien.FormatString[1];
            celVienPhi.DataBindings.Add("Text", DataSource, "VienPhi").FormatString = DungChung.Bien.FormatString[1];
            celTraLai.DataBindings.Add("Text", DataSource, "TraLai").FormatString = DungChung.Bien.FormatString[1];
            celThuThem.DataBindings.Add("Text", DataSource, "ThuThem").FormatString = DungChung.Bien.FormatString[1];
            celTongTien.DataBindings.Add("Text", DataSource, "Tong").FormatString = DungChung.Bien.FormatString[1];

            celKhamG.DataBindings.Add("Text", DataSource, "Kham").FormatString = DungChung.Bien.FormatString[1];
            celDichVuG.DataBindings.Add("Text", DataSource, "DichVu").FormatString = DungChung.Bien.FormatString[1];
            celThuocG.DataBindings.Add("Text", DataSource, "Thuoc").FormatString = DungChung.Bien.FormatString[1];
            celTamUngG.DataBindings.Add("Text", DataSource, "TamUng").FormatString = DungChung.Bien.FormatString[1];
            celVienPhiG.DataBindings.Add("Text", DataSource, "VienPhi").FormatString = DungChung.Bien.FormatString[1];
            celTraLaiG.DataBindings.Add("Text", DataSource, "TraLai").FormatString = DungChung.Bien.FormatString[1];
            celThuThemG.DataBindings.Add("Text", DataSource, "ThuThem").FormatString = DungChung.Bien.FormatString[1];
            celTongTienG.DataBindings.Add("Text", DataSource, "Tong").FormatString = DungChung.Bien.FormatString[1];

            celKhamR.DataBindings.Add("Text", DataSource, "Kham").FormatString = DungChung.Bien.FormatString[1];
            celDichVuR.DataBindings.Add("Text", DataSource, "DichVu").FormatString = DungChung.Bien.FormatString[1];
            celThuocR.DataBindings.Add("Text", DataSource, "Thuoc").FormatString = DungChung.Bien.FormatString[1];
            celTamUngR.DataBindings.Add("Text", DataSource, "TamUng").FormatString = DungChung.Bien.FormatString[1];
            celVienPhiR.DataBindings.Add("Text", DataSource, "VienPhi").FormatString = DungChung.Bien.FormatString[1];
            celTraLaiR.DataBindings.Add("Text", DataSource, "TraLai").FormatString = DungChung.Bien.FormatString[1];
            celThuThemR.DataBindings.Add("Text", DataSource, "ThuThem").FormatString = DungChung.Bien.FormatString[1];
            celTongTienR.DataBindings.Add("Text", DataSource, "Tong").FormatString = DungChung.Bien.FormatString[1];

            celKhamG.Summary.FormatString = DungChung.Bien.FormatString[1];
            celDichVuG.Summary.FormatString = DungChung.Bien.FormatString[1];
            celThuocG.Summary.FormatString = DungChung.Bien.FormatString[1];
            celTamUngG.Summary.FormatString = DungChung.Bien.FormatString[1];
            celVienPhiG.Summary.FormatString = DungChung.Bien.FormatString[1];
            celTraLaiG.Summary.FormatString = DungChung.Bien.FormatString[1];
            celThuThemG.Summary.FormatString = DungChung.Bien.FormatString[1];
            celTongTienG.Summary.FormatString = DungChung.Bien.FormatString[1];

            celKhamR.Summary.FormatString = DungChung.Bien.FormatString[1];
            celDichVuR.Summary.FormatString = DungChung.Bien.FormatString[1];
            celThuocR.Summary.FormatString = DungChung.Bien.FormatString[1];
            celTamUngR.Summary.FormatString = DungChung.Bien.FormatString[1];
            celVienPhiR.Summary.FormatString = DungChung.Bien.FormatString[1];
            celTraLaiR.Summary.FormatString = DungChung.Bien.FormatString[1];
            celThuThemR.Summary.FormatString = DungChung.Bien.FormatString[1];
            celTongTienR.Summary.FormatString = DungChung.Bien.FormatString[1];
            GroupHeader1.GroupFields.Add(new GroupField("KP"));
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            celTenCQCQ.Text = DungChung.Bien.TenCQCQ;
            celTenCQ.Text = DungChung.Bien.TenCQ;
            celDiaChi.Text = "Địa chỉ: " + DungChung.Bien.DiaChi;
           // _row = Convert.ToInt32(rowCount.Value);
        }

        private void GroupFooter1_BeforePrint(object sender, CancelEventArgs e)
        {
            if (this.GetCurrentColumnValue("KP") != null)
            {
                string tenkhoa = this.GetCurrentColumnValue("KP").ToString();
                celKhoaFooter.Text = "Cộng " + tenkhoa;
            }
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            celNguoiLapBieu.Text = DungChung.Bien.NguoiLapBieu;
            

        }  //   int countDetail = 0;
       

      

       
       
    }
}
