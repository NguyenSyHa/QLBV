using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_XNRoiLoanDongMau : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_XNRoiLoanDongMau()
        {
            InitializeComponent();
        }

        int x = 1;

        internal void BindingData()
        {

        }

        private void xrTableCell17_BeforePrint(object sender, CancelEventArgs e)
        {
            xrTableCell17.Text = x.ToString();
            x++;
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            if(DungChung.Bien.MaBV == "30012")
            {
                xrTableCell17.Text = "Thời gian prothrombin(PT:Prothrombin Time - thời gian Prothrombin) (Các tên khác như TQ: tỷ lệ Prothrombin) bằng máy bán tự động";
                xrTableCell33.Text = "Thời gian thromboplastin một phần hoạt hoá (APTT: Activated Partial Thromboplastin Time), (Tên khác: TCK) bằng máy bán tự động";
                xrTableCell43.Text = "Thời gian thrombin (TT: Thrombin Time) bằng máy bán tự động";
                xrTableCell53.Text = "Định lượng Fibrinogen (Tên khác: Định lượng yếu tố I), phương pháp Clauss- phương pháp trực tiếp, bằng máy bán tự động";
            }
            else if (DungChung.Bien.MaBV == "30010")
            {
                xrTableCell17.Text = "Thời gian prothrombin (PT: Prothrombin Time), (Các tên khác: TQ; Tỷ lệ Prothrombin) bằng máy tự động";
                xrTableCell33.Text = "Thời gian thromboplastin một phần hoạt hóa (APTT: Activated Partial Thromboplastin Time), (Tên khác: TCK) bằng máy tự động";
                xrTableCell43.Text = "Thời gian thrombin (TT: Thrombin Time) bằng máy tự động";
                xrTableCell53.Text = "Định lượng Fibrinogen (Tên khác: Định lượng yếu tố I), phương pháp Clauss- phương pháp trực tiếp, bằng máy tự động";
            }
        }
    }
}
