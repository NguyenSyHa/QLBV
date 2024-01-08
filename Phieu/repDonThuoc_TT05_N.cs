using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class repDonThuoc_TT05_N: DevExpress.XtraReports.UI.XtraReport
    {
        public repDonThuoc_TT05_N()
        {
            InitializeComponent();
        }
        //QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {

        }
        public void BindData() {
            TenDV.DataBindings.Add("Text", DataSource, "TenDV");
            SoLuong.DataBindings.Add("Text", DataSource, "SoLuong");
            DonVi.DataBindings.Add("Text", DataSource, "DonVi");
            HuongDan.DataBindings.Add("Text", DataSource, "HuongDan");
            
        }
        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            txtTenCQ.Text = DungChung.Bien.TenCQ;
            if (DungChung.Bien.MaBV == "24009" )
            {
                txtMaICD.Visible = true;
                txtICD.Visible = true;
            }
            if (DungChung.Bien.MaBV == "30009") {
                txtMaICD.Visible = true;
                txtICD.Visible = true;
                
            }

            lab_DienThoai.Text = "Điện thoại:................... ";
            if (DungChung.Bien.MaBV == "30007")
                tt.Visible = false;
            if (DungChung.Bien.MaBV == "24272")
            {
                SubBand1.Visible = false;
                SubBand2.Visible = true;
                xrPictureBox1.Image = DungChung.Ham.GetLogo();
            }
        }

        private void txtTenCQ_BeforePrint(object sender, CancelEventArgs e)
        {
            txtTenCQ.Text = DungChung.Bien.TenCQ;
            txtTenCQ2.Text = DungChung.Bien.TenCQ;
        }

    }
}
