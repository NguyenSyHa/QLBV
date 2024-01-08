using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class repDThuoc_HoaAn : DevExpress.XtraReports.UI.XtraReport
    {
        public repDThuoc_HoaAn()
        {
            InitializeComponent();
        }
        //QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            string _tendv = "",_tenHC="";
                if (this.GetCurrentColumnValue("TenDV") != null)
                    _tendv = this.GetCurrentColumnValue("TenDV").ToString().Trim();
            else
                    _tendv="";
            if (DungChung.Bien.MaBV == "04018") {
                
                if (this.GetCurrentColumnValue("TenHC") != null)
                    _tenHC = this.GetCurrentColumnValue("TenHC").ToString().Trim();
                if (!string.IsNullOrEmpty(_tenHC))
                    TenDV.Text = _tendv + "(" + _tenHC + ")";
                else
                    TenDV.Text = _tendv;
            } else
                TenDV.Text = _tendv;
        }
        public void BindData()
        {
           // TenDV.DataBindings.Add("Text", DataSource, "TenDV");
            SoLuong.DataBindings.Add("Text", DataSource, "SoLuong");
            DonVi.DataBindings.Add("Text", DataSource, "DonVi");
            HuongDan.DataBindings.Add("Text", DataSource, "HuongDan");

        }
        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            string sthe = SThe.Value.ToString();
            if (sthe.Length == 15)
            {
                txtThe1.Text = sthe.Substring(0, 2);
                txtThe2.Text = sthe.Substring(2, 1);
                txtThe3.Text = sthe.Substring(3, 2);
                txtThe4.Text = sthe.Substring(5, 2);
                txtThe5.Text = sthe.Substring(7, 3);
                txtThe6.Text = sthe.Substring(10, 5);
            }
            txtTenCQ.Text = DungChung.Bien.TenCQ;
            if (ChuyenVien.Value.ToString().Contains("2"))
            {
                //System.Windows.Forms.MessageBox.Show("123");
                xrCongKham.Visible = true;
                txtNoiChuyen.Visible = true;
                xrChiTiet.Visible = false;
            }


        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "24009" || DungChung.Bien.MaBV == "04005")
            {
                if (Tuoi.Value.ToString().Contains("tháng"))
                {
                    colbome.Visible = true;
                }
                colTenBN.Visible = false;
                txtbnky.Visible = false;
                if (DungChung.Bien.MaBV == "30004")
                {
                    colTenCB.Visible = false;
                }
            }
            if (DungChung.Bien.MaBV == "04018")
            {
                colCongKhoan.Visible = true;
                colCongKhoan.Text = "Đã nhận .......... loại thuốc";
            }
            else {
                colCongKhoan.Visible = false;
            }

        }


    }
}
