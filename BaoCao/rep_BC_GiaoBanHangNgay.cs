using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_BC_GiaoBanHangNgay : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_BC_GiaoBanHangNgay()
        {
            InitializeComponent();
        }
        int gr1 = 1;
        private void GroupHeader1_BeforePrint(object sender, CancelEventArgs e)
        {
            switch (gr1)
            {
                case 1:
                    GR1.Text = "A";
                    
                    break;
                case 2:
                    GR1.Text = "B";
                    break;
                case 3:
                    GR1.Text = "C";
                    break;
                case 4:
                    GR1.Text = "D";
                    break;
                case 5:
                    GR1.Text = "E";
                    break;

            }
            gr1++;

        }

        public void BindingData()
        {
            tbkhoa.DataBindings.Add("Text", DataSource, "TenKP");
            grkhoa.DataBindings.Add("Text", DataSource, "khoa");
            // Bệnh nhân cũ
            tbbnctt.DataBindings.Add("Text", DataSource, "bnctt").FormatString=("{0:##,###}");
            tbbncct.DataBindings.Add("Text", DataSource, "bncct").FormatString = ("{0:##,###}");
            tbbncvt.DataBindings.Add("Text", DataSource, "bncvt").FormatString = ("{0:##,###}");
            tbbncdv.DataBindings.Add("Text", DataSource, "bncdv").FormatString = ("{0:##,###}");
            tbbnctong.DataBindings.Add("Text", DataSource, "bnctong").FormatString = ("{0:##,###}");

            grbnctt.DataBindings.Add("Text", DataSource, "bnctt");
            grbncct.DataBindings.Add("Text", DataSource, "bncct");
            grbncvt.DataBindings.Add("Text", DataSource, "bncvt");
            grbncdv.DataBindings.Add("Text", DataSource, "bncdv");
            grbnctong.DataBindings.Add("Text", DataSource, "bnctong");

            xrTableCell88.DataBindings.Add("Text", DataSource, "bnctt").FormatString = ("{0:##,###}");
            xrTableCell89.DataBindings.Add("Text", DataSource, "bncct").FormatString = ("{0:##,###}");
            xrTableCell90.DataBindings.Add("Text", DataSource, "bncvt").FormatString = ("{0:##,###}");
            xrTableCell91.DataBindings.Add("Text", DataSource, "bncdv").FormatString = ("{0:##,###}");
            xrTableCell92.DataBindings.Add("Text", DataSource, "bnctong").FormatString = ("{0:##,###}");

            // Bệnh nhân vào viện
            tbbnvvtt.DataBindings.Add("Text", DataSource, "bnvvtt").FormatString = ("{0:##,###}");
            tbbnvvct.DataBindings.Add("Text", DataSource, "bnvvct").FormatString = ("{0:##,###}");
            tbbnvvvt.DataBindings.Add("Text", DataSource, "bnvvvt").FormatString = ("{0:##,###}");
            tbbnvvdv.DataBindings.Add("Text", DataSource, "bnvvdv").FormatString = ("{0:##,###}");
            tbbnvvtong.DataBindings.Add("Text", DataSource, "bnvvtong").FormatString = ("{0:##,###}");

            grbnvvtt.DataBindings.Add("Text", DataSource, "bnvvtt");
            grbnvvct.DataBindings.Add("Text", DataSource, "bnvvct");
            grbnvvvt.DataBindings.Add("Text", DataSource, "bnvvvt");
            grbnvvdv.DataBindings.Add("Text", DataSource, "bnvvdv");
            grbnvvtong.DataBindings.Add("Text", DataSource, "bnvvtong");

            xrTableCell93.DataBindings.Add("Text", DataSource, "bnvvtt").FormatString = ("{0:##,###}");
            xrTableCell94.DataBindings.Add("Text", DataSource, "bnvvct").FormatString = ("{0:##,###}");
            xrTableCell95.DataBindings.Add("Text", DataSource, "bnvvvt").FormatString = ("{0:##,###}");
            xrTableCell96.DataBindings.Add("Text", DataSource, "bnvvdv").FormatString = ("{0:##,###}");
            xrTableCell97.DataBindings.Add("Text", DataSource, "bnvvtong").FormatString = ("{0:##,###}");

            // bệnh nhân ra viện
            tbbnrvtt.DataBindings.Add("Text", DataSource, "bnrvtt").FormatString = ("{0:##,###}");
            tbbnrvct.DataBindings.Add("Text", DataSource, "bnrvct").FormatString = ("{0:##,###}");
            tbbnrvvt.DataBindings.Add("Text", DataSource, "bnrvvt").FormatString = ("{0:##,###}");
            tbbnrvdv.DataBindings.Add("Text", DataSource, "bnrvdv").FormatString = ("{0:##,###}");
            tbbnrvtong.DataBindings.Add("Text", DataSource, "bnrvtong").FormatString = ("{0:##,###}");

            grbnrvtt.DataBindings.Add("Text", DataSource, "bnrvtt");
            grbnrvct.DataBindings.Add("Text", DataSource, "bnrvct");
            grbnrvvt.DataBindings.Add("Text", DataSource, "bnrvvt");
            grbnrvdv.DataBindings.Add("Text", DataSource, "bnrvdv");
            grbnrvtong.DataBindings.Add("Text", DataSource, "bnrvtong");

            xrTableCell98.DataBindings.Add("Text", DataSource, "bnrvtt").FormatString = ("{0:##,###}");
            xrTableCell99.DataBindings.Add("Text", DataSource, "bnrvct").FormatString = ("{0:##,###}");
            xrTableCell100.DataBindings.Add("Text", DataSource, "bnrvvt").FormatString = ("{0:##,###}");
            xrTableCell101.DataBindings.Add("Text", DataSource, "bnrvdv").FormatString = ("{0:##,###}");
            xrTableCell102.DataBindings.Add("Text", DataSource, "bnrvtong").FormatString = ("{0:##,###}");

            // bệnh nhân hiện có
            tbbnhctt.DataBindings.Add("Text", DataSource, "bnhctt").FormatString = ("{0:##,###}");
            tbbnhcct.DataBindings.Add("Text", DataSource, "bnhcct").FormatString = ("{0:##,###}");
            tbbnhcvt.DataBindings.Add("Text", DataSource, "bnhcvt").FormatString = ("{0:##,###}");
            tbbnhcdv.DataBindings.Add("Text", DataSource, "bnhcdv").FormatString = ("{0:##,###}");
            tong.DataBindings.Add("Text", DataSource, "bnhctong").FormatString = ("{0:##,###}");

            grbnhctt.DataBindings.Add("Text", DataSource, "bnhctt");
            grbnhcct.DataBindings.Add("Text", DataSource, "bnhcct");
            grbnhcvt.DataBindings.Add("Text", DataSource, "bnhcvt");
            grbnhcdv.DataBindings.Add("Text", DataSource, "bnhcdv");
            grbnhctong.DataBindings.Add("Text", DataSource, "bnhctong");

            xrTableCell103.DataBindings.Add("Text", DataSource, "bnhctt").FormatString = ("{0:##,###}");
            xrTableCell104.DataBindings.Add("Text", DataSource, "bnhcct").FormatString = ("{0:##,###}");
            xrTableCell105.DataBindings.Add("Text", DataSource, "bnhcvt").FormatString = ("{0:##,###}");
            xrTableCell106.DataBindings.Add("Text", DataSource, "bnhcdv").FormatString = ("{0:##,###}");
            xrTableCell107.DataBindings.Add("Text", DataSource, "bnhctong").FormatString = ("{0:##,###}");

            GroupHeader1.GroupFields.Add(new GroupField("khoa"));
        }

    }
}
