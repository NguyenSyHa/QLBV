using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_BcSuDungTS : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_BcSuDungTS()
        {
            InitializeComponent();
        }
        public void BindingData()
        {
            colTenKP.DataBindings.Add("Text", DataSource, "TenKP");
            txtNTN.DataBindings.Add("Text", DataSource, "Ngay");
            colCB.DataBindings.Add("Text", DataSource, "CanBo");
            colHTTS.DataBindings.Add("Text",DataSource,"HTTS");
            colNoiDung.DataBindings.Add("Text",DataSource,"NoiDung");
            GroupHeader1.GroupFields.Add(new GroupField("TenKP"));
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            colTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            string _pl = In.Value.ToString();
            if (_pl == "0")
            {
                colKhoa.Visible = false;

            }
            else GroupHeader1.Visible = false;
        }

        private void colNTN_BeforePrint(object sender, CancelEventArgs e)
        {
            if (this.GetCurrentColumnValue("Ngay") != null && this.GetCurrentColumnValue("Ngay").ToString().Length > 10)
            {
                string _ngay = this.GetCurrentColumnValue("Ngay").ToString();
                colNTN.Text = _ngay.ToString().Substring(0,10);
            }
        }
    }
}
