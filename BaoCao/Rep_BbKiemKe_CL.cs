using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_BbKiemKe_CL : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_BbKiemKe_CL()
        {
            InitializeComponent();
        }
        public void DataBinding()
        {
            colTenNhom.DataBindings.Add("Text", DataSource, "TenNhomCT1");
            colTN.DataBindings.Add("Text", DataSource, "TenRG1");
            colTenDV.DataBindings.Add("Text",DataSource,"TenDV");
            colDVT.DataBindings.Add("Text",DataSource,"DonVi");
            colSLTT.DataBindings.Add("Text", DataSource, "SLTT").FormatString = DungChung.Bien.FormatString[0];
            colSLBA.DataBindings.Add("Text", DataSource, "SLBA").FormatString = DungChung.Bien.FormatString[0];
            colTong.DataBindings.Add("Text", DataSource, "TongSo").FormatString = DungChung.Bien.FormatString[0];
            colTong.DataBindings.Add("Text", DataSource, "TongSo").FormatString = DungChung.Bien.FormatString[0];
            colDonGia.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[1];
            colThanhTien.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];
            GroupHeader2.GroupFields.Add(new GroupField("TenNhomCT1"));
            GroupHeader1.GroupFields.Add(new GroupField("TenRG1"));
        }

        private void GroupHeader2_BeforePrint(object sender, CancelEventArgs e)
        {
            if (InNhom.Value.ToString() != null)
            {
                string _nhom = InNhom.Value.ToString();
                if (_nhom == "1")
                    GroupHeader2.Visible = false;
            }
        }

        private void GroupHeader1_BeforePrint(object sender, CancelEventArgs e)
        {
            if (InTieuNhom.Value.ToString() != null)
            {
                string _tnhom = InTieuNhom.Value.ToString();
                if (_tnhom == "1")
                    GroupHeader1.Visible = false;
            }
        }

        private void colSLTT_BeforePrint(object sender, CancelEventArgs e)
        {
            if (this.GetCurrentColumnValue("TT1") != null)
            {
                int _tt = Convert.ToInt32(this.GetCurrentColumnValue("TT1"));
                if (_tt > 0)
                {
                    colSLTT.Text = _tt.ToString();
                }
                else colSLTT.Text = " ";
            }
        }

        private void colSLBA_BeforePrint(object sender, CancelEventArgs e)
        {
            if (this.GetCurrentColumnValue("BA1") != null)
            {
                int _ba = Convert.ToInt32(this.GetCurrentColumnValue("BA1"));
                if (_ba > 0)
                {
                    colSLBA.Text = _ba.ToString();
                }
                else colSLBA.Text = " ";
            }
        }

        private void colTong_BeforePrint(object sender, CancelEventArgs e)
        {
            if (this.GetCurrentColumnValue("Tong1") != null)
            {
                int _tong = Convert.ToInt32(this.GetCurrentColumnValue("Tong1"));
                if (_tong > 0)
                {
                    colTong.Text = _tong.ToString();
                }
                else colTong.Text = " ";
            }
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            colTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
        }
    }
}
