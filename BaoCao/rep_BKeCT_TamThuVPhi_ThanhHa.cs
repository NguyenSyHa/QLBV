using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_BKeCT_TamThuVPhi_ThanhHa : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_BKeCT_TamThuVPhi_ThanhHa()
        {
            InitializeComponent();
        }


        public void BindingData()
        {
            colHoTen.DataBindings.Add("Text", DataSource, "TenBN");
            colDChi.DataBindings.Add("Text", DataSource, "DChi");
            colKPhong.DataBindings.Add("Text", DataSource, "TenKP");            
            colNDung.DataBindings.Add("Text", DataSource, "NoiDung").FormatString = DungChung.Bien.FormatString[1];
            colSTien.DataBindings.Add("Text", DataSource, "SoTien").FormatString = DungChung.Bien.FormatString[1];
            colSTienGr.DataBindings.Add("Text", DataSource, "SoTien").FormatString = DungChung.Bien.FormatString[1];
            colSTienT.DataBindings.Add("Text", DataSource, "SoTien").FormatString = DungChung.Bien.FormatString[1];  
            GroupHeader1.GroupFields.Add(new GroupField("NgayThu"));
        }

        private void colNgayGr_BeforePrint(object sender, CancelEventArgs e)
        {
            if (this.GetCurrentColumnValue("NgayThu") != null)
            {
                string date = this.GetCurrentColumnValue("NgayThu").ToString();
                colNgayGr.Text = date.Substring(0, 10);
            }       
        }

        private void rep_BKeCT_TamThuVPhi_ThanhHa_BeforePrint(object sender, CancelEventArgs e)
        {
            ColNguoiLapBieu.Text = DungChung.Bien.NguoiLapBieu;
            colKeToan.Text = DungChung.Bien.KeToanTruong;
            colBanGD.Text = DungChung.Bien.GiamDoc;
            colNgayLapBieu.Text = "Ngày " + DateTime.Now.Day.ToString() + " tháng " + DateTime.Now.Month.ToString() + " Năm " + DateTime.Now.Year.ToString();
        }
    }
}
