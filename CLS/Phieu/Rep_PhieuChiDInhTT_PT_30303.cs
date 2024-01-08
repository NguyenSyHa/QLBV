using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_PhieuChiDInhTT_PT_30303 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_PhieuChiDInhTT_PT_30303()
        {
            InitializeComponent();
        }
        public void BindingData()
        {
            coldongia.DataBindings.Add("Text", DataSource, "DonGia");
            colSLUONG.DataBindings.Add("Text", DataSource, "SoLuong");
            colTenchidinh.DataBindings.Add("Text", DataSource, "TenDV");
            //GroupHeader1.GroupFields.Add(new GroupField("TenTN"));
            colThanhTien.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[1];
            colbhyt.DataBindings.Add("Text", DataSource, "BHYT");

            colThanhTienT.DataBindings.Add("Text", DataSource, "DonGia");
            colThanhTienT.Summary.FormatString = DungChung.Bien.FormatString[1];

            colSLuong_T.DataBindings.Add("Text", DataSource, "SoLuong");
            colSLuong_T.Summary.FormatString = DungChung.Bien.FormatString[0];
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            colTenBSDT.Text = TenBS.Value.ToString();
            if (DungChung.Bien.MaBV == "30004")
            {
                colTenBSDT.Visible = false;
                //colTenTKXN.Visible = false;
            }
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            colTenCQCQ.Text = DungChung.Bien.TenCQ;
            txttencq.Text = DungChung.Bien.TenCQ;
            txtDchi.Text = "Địa chỉ: " + DungChung.Bien.DiaChi;
            txtSoDT.Text = "Điện thoại: " + DungChung.Bien.SDTCQ;
        }

    }
}
