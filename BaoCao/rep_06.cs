using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_06 : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_06()
        {
            InitializeComponent();
        }
        public void BindingData()
        {
            colChiphi.DataBindings.Add("Text", DataSource, "Chiphi").FormatString = DungChung.Bien.FormatString[1];
            colCPthuoc.DataBindings.Add("Text", DataSource, "CPthuoc").FormatString = DungChung.Bien.FormatString[1];
            colDangBC.DataBindings.Add("Text", DataSource, "DangBC");
            colDuongdung.DataBindings.Add("Text", DataSource, "Duongdung");
            colDVT.DataBindings.Add("Text", DataSource, "DVT");
            colGiaBHYTTT.DataBindings.Add("Text", DataSource, "GiaBHYT").FormatString = DungChung.Bien.FormatString[1];
            colGiadenghi.DataBindings.Add("Text", DataSource, "Giadenghi").FormatString = DungChung.Bien.FormatString[1];
            colGiaTT.DataBindings.Add("Text", DataSource, "GiaTT").FormatString = DungChung.Bien.FormatString[1];
            colNhaSX.DataBindings.Add("Text", DataSource, "NhaSX");
            colNuocSX.DataBindings.Add("Text", DataSource, "NuocSX");
            colQuycach.DataBindings.Add("Text", DataSource, "Quycach");
            colSLsudung.DataBindings.Add("Text", DataSource, "SLsudung");
            colSoDK.DataBindings.Add("Text", DataSource, "SoDK");
            colTenduoc.DataBindings.Add("Text", DataSource, "Tenduoc");
            txtTL.DataBindings.Add("Text", DataSource, "TLTT");
            txttn.DataBindings.Add("Text", DataSource, "tieunhom");
            GroupHeader2.GroupFields.Add(new GroupField("tieunhom"));
        }

        private void GroupHeader1_BeforePrint(object sender, CancelEventArgs e)
        {
            
        }

        private void GroupHeader2_BeforePrint(object sender, CancelEventArgs e)
        {
            if (this.GetCurrentColumnValue("tieunhom") != null)
            {
                string tn = this.GetCurrentColumnValue("tieunhom").ToString();
                if (tn.Contains("Đông y"))
                {
                    colnd.Text = "Phần II. Thuốc đông y";
                    colTN.Text = "A. Thuốc có tên trong danh mục, bao gồm các thuốc phối hợp nhiều thành phần";

                }
                else
                {
                    colnd.Text = "Phần I. Thuốc tân dược";
                    colTN.Text = "A. Thuốc có tên trong danh mục, bao gồm các thuốc phối hợp nhiều thành phần";
                }

            }
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            txtSoyte.Text = DungChung.Bien.TenCQCQ;
            txtTenCS.Text = DungChung.Bien.TenCQ;
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            if (this.GetCurrentColumnValue("TLTT") != null)
            {
                string tl = this.GetCurrentColumnValue("TLTT").ToString();
                if (tl == "1")
                {
                    colTLTT.Text = "100%";
                }
            }
        }

    }
}
