using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_PhieusTHu_01071_Sub : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_PhieusTHu_01071_Sub()
        {
            InitializeComponent();
        }
        int ploai = 0;
        public void bindingdata(int a)
        {
            //coltendv.DataBindings.Add("Text", DataSource, "TenDV");
            //colchiphi.DataBindings.Add("Text", DataSource, "ChiPhi").FormatString = "{0:##,###}";
            //coltienbn.DataBindings.Add("Text", DataSource, "TenBN").FormatString = "{0:##,###}";
            //coltennhom.DataBindings.Add("Text", DataSource, "TenNhom");
            //GroupHeadersub.GroupFields.Add(new GroupField("STT"));
            ////colstt.DataBindings.Add("Text", DataSource, "STT");
            //colchiphitong.DataBindings.Add("Text", DataSource, "ChiPhi");
            //coltienbntong.DataBindings.Add("Text", DataSource, "TenBN");
            //colcpt.DataBindings.Add("Text", DataSource, "ChiPhi");
            //coltbnt.DataBindings.Add("Text", DataSource, "TenBN");
            //coltong.DataBindings.Add("Text", DataSource, "TenBN");
            //if (a == 1)
            //{
            //    GroupHeader1.GroupFields.Add(new GroupField("TrongBH"));
            //    coltrongdm.DataBindings.Add("Text", DataSource, "TrongDM");
            //    coltongcpbh.DataBindings.Add("Text", DataSource, "ChiPhi");
            //    colbntrabh.DataBindings.Add("Text", DataSource, "TenBN");
            //}
            coltendv.DataBindings.Add("Text", DataSource, "TenDV");
            colchiphi.DataBindings.Add("Text", DataSource, "ChiPhi").FormatString = "{0:##,###}";
            coltienbn.DataBindings.Add("Text", DataSource, "TenBN").FormatString = "{0:##,###}";
            coltennhom.DataBindings.Add("Text", DataSource, "TenNhom");
            GroupHeadersub.GroupFields.Add(new GroupField("STT"));
            //colstt.DataBindings.Add("Text", DataSource, "STT");
            colchiphitong.DataBindings.Add("Text", DataSource, "ChiPhi");
            coltienbntong.DataBindings.Add("Text", DataSource, "TenBN");
            colcpt.DataBindings.Add("Text", DataSource, "ChiPhi");
            coltbnt.DataBindings.Add("Text", DataSource, "TenBN");
            coltong.DataBindings.Add("Text", DataSource, "TenBN");
            if (a == 1)
            {
                GroupHeader1.GroupFields.Add(new GroupField("TrongBH"));
                coltrongdm.DataBindings.Add("Text", DataSource, "TrongDM");
                coltongcpbh.DataBindings.Add("Text", DataSource, "ChiPhi");
                colbntrabh.DataBindings.Add("Text", DataSource, "TenBN");
            }

        }
        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            ploai = Convert.ToInt32(loai.Value);
            if (ploai == 1)
            {
                Detail.Visible = false;
            }
            else
            {
                xrTableRow13.Visible = false;
                GroupHeader1.Visible = false;
            }
        }
        int i = 1;
        private void GroupHeader1_BeforePrint(object sender, CancelEventArgs e)
        {
            if (ploai == 1)
            {
                colstt2.Text = i.ToString();

            }
            else
            {
                switch (i)
                {
                    case 1:
                        colstt2.Text = "I.";
                        break;
                    case 2:
                        colstt2.Text = "II.";
                        break;
                    case 3:
                        colstt2.Text = "III.";
                        break;
                    case 4:
                        colstt2.Text = "IV.";
                        break;
                    case 5:
                        colstt2.Text = "V.";
                        break;
                    case 6:
                        colstt2.Text = "VI.";
                        break;
                }
            }
            i++;
        }
        int b=1;
        private void GroupHeader1_BeforePrint_1(object sender, CancelEventArgs e)
        {
            switch (b)
            {
                case 1:
                    colstt2.Text = "I.";
                    break;
                case 2:
                    colstt2.Text = "II.";
                    break;
            }
            b++;
        }

    }
}
