using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.Phieu
{
    public partial class rep_lskcb : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_lskcb()
        {
            InitializeComponent();
        }
        public void BindData()
        {
            TenDV.DataBindings.Add("Text", DataSource, "TenDV");
            txtKQ.DataBindings.Add("Text", DataSource, "KetQua");
            txtSoluong.DataBindings.Add("Text", DataSource, "SoLuong");
            DonVi.DataBindings.Add("Text", DataSource, "DonVi");
            HuongDan.DataBindings.Add("Text", DataSource, "HuongDan");
            colBSKe.DataBindings.Add("Text", DataSource, "TenCB");
            colNgayKe.DataBindings.Add("Text", DataSource, "NgayKe");
            colChanDoan.DataBindings.Add("Text", DataSource, "ChanDoan");
            colNgayNhap.DataBindings.Add("Text", DataSource, "NNhap");
           // xrTableCell12.DataBindings.Add("Text", DataSource, "HuongDan");
            GroupHeader1.GroupFields.Add(new GroupField("PhanLoai"));
            GroupHeader2.GroupFields.Add(new GroupField("NNhap"));
        }
        int i = 0;
        private void STTdot_BeforePrint(object sender, CancelEventArgs e)
        {
            i++;
            STTdot.Text = i.ToString();
        }

        private void SoLuong_BeforePrint(object sender, CancelEventArgs e)
        {
            if (this.GetCurrentColumnValue("SoLuong") != null && this.GetCurrentColumnValue("SoLuong").ToString() != "")
            {
                string a = this.GetCurrentColumnValue("SoLuong").ToString();
                if (a == "0")
                {
                    SoLuong.Text = txtKQ.Text;
                }
                else
                {
                    SoLuong.Text = txtSoluong.Text;
                }
            }
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {

            if (this.GetCurrentColumnValue("PhanLoai") != null && this.GetCurrentColumnValue("PhanLoai").ToString() == "1")
            {
                xrTableRow2.Visible = true;
            }
            else {
                xrTableRow2.Visible = false;
            }
        }

        private void GroupHeader1_BeforePrint(object sender, CancelEventArgs e)
        {
            if (this.GetCurrentColumnValue("PhanLoai") != null && this.GetCurrentColumnValue("PhanLoai").ToString() == "1")
            {
                xrTableCell2.Text = "Ngày kê";
                xrTableCell3.Text = "Bác sĩ kê đơn";

            }
            else {
                xrTableCell2.Text = "Ngày chỉ định";
                xrTableCell3.Text = "Bác sĩ chỉ định";
            }
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            txtTenCQ.Text = txtTenCQ2.Text = DungChung.Bien.TenCQ;
            if (DungChung.Bien.MaBV == "24272")
            {
                SubBand1.Visible = false;
                SubBand2.Visible = true;
                xrPictureBox1.Image = DungChung.Ham.GetLogo();
            }
        }
    }
}
