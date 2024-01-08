using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_BCXetNgiemTheoKP_30007 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_BCXetNgiemTheoKP_30007()
        {
            InitializeComponent();
        }
        public void BinDingData()
        {

            colTenDV.DataBindings.Add("Text", DataSource, "TenDV");
            celLoai.DataBindings.Add("Text", DataSource, "LoaiPTTT");
            celNoiBHYT1.DataBindings.Add("Text", DataSource, "NoiTBHYT1").FormatString = DungChung.Bien.FormatString[0];
            celNoiTDV1.DataBindings.Add("Text", DataSource, "NoiTDV1").FormatString = DungChung.Bien.FormatString[0];
            celNgTBHYT1.DataBindings.Add("Text", DataSource, "NgTBHYT1").FormatString = DungChung.Bien.FormatString[0];
            celNgTDV1.DataBindings.Add("Text", DataSource, "NgTDV1").FormatString = DungChung.Bien.FormatString[0];

            celNoiBHYT2.DataBindings.Add("Text", DataSource, "NoiTBHYT2").FormatString = DungChung.Bien.FormatString[0];
            celNoiTDV2.DataBindings.Add("Text", DataSource, "NoiTDV2").FormatString = DungChung.Bien.FormatString[0];
            celNgTBHYT2.DataBindings.Add("Text", DataSource, "NgTBHYT2").FormatString = DungChung.Bien.FormatString[0];
            celNgTDV2.DataBindings.Add("Text", DataSource, "NgTDV2").FormatString = DungChung.Bien.FormatString[0];

            celNoiBHYT3.DataBindings.Add("Text", DataSource, "NoiTBHYT3").FormatString = DungChung.Bien.FormatString[0];
            celNoiTDV3.DataBindings.Add("Text", DataSource, "NoiTDV3").FormatString = DungChung.Bien.FormatString[0];
            celNgTBHYT3.DataBindings.Add("Text", DataSource, "NgTBHYT3").FormatString = DungChung.Bien.FormatString[0];
            celNgTDV3.DataBindings.Add("Text", DataSource, "NgTDV3").FormatString = DungChung.Bien.FormatString[0];

            celTong.DataBindings.Add("Text", DataSource, "Tong").FormatString = DungChung.Bien.FormatString[0];

            celTenNhom.DataBindings.Add("Text", DataSource, "TenNhom");
            GroupHeader1.GroupFields.Add(new GroupField("IDNhom"));
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            txtTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            txtTenCQCQ.Text = DungChung.Bien.TenCQCQ;
        }
        int a = 1;
        private void GroupHeader1_BeforePrint(object sender, CancelEventArgs e)
        {
            switch(a)
            {
                case 1:
                    txtSTTG.Text = "I";
                    break;
                case 2:
                    txtSTTG.Text = "II";
                    break;
                case 3:
                    txtSTTG.Text = "III";
                    break;
                case 4:
                    txtSTTG.Text = "IV";
                    break;
                case 5:
                    txtSTTG.Text = "V";
                    break;
                case 6:
                    txtSTTG.Text = "VI";
                    break;
                case 7:
                    txtSTTG.Text = "VII";
                    break;
                case 8:
                    txtSTTG.Text = "VIII";
                    break;
            }
            a++;
        }
    }
}
