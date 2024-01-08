using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class repDonThuocTV: DevExpress.XtraReports.UI.XtraReport
    {
        public repDonThuocTV()
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
                txtCMTND.Visible = false;
        }

        private void txtTenCQ_BeforePrint(object sender, CancelEventArgs e)
        {
            txtTenCQ.Text = DungChung.Bien.TenCQ;
        }

        private void SoLuong_BeforePrint(object sender, CancelEventArgs e)
        {
            if (this.GetCurrentColumnValue("SoLuong") != null)
            {
                int ot;
                if (Int32.TryParse(this.GetCurrentColumnValue("SoLuong").ToString(), out ot))
                {
                    int sl = Convert.ToInt32(this.GetCurrentColumnValue("SoLuong").ToString());
                    if (DungChung.Bien.MaBV == "30007")
                        SoLuong.Text = DungChung.Ham.DocTienBangChu(sl, "");
                }
            }
        }
    }
}
