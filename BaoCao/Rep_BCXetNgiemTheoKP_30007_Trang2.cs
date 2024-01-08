using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_BCXetNgiemTheoKP_30007_Trang2 : DevExpress.XtraReports.UI.XtraReport
    {
        int _sotrang = 1;
        public Rep_BCXetNgiemTheoKP_30007_Trang2(int sotrang)
        {
            InitializeComponent();
            _sotrang = sotrang;
        }
        public void BinDingData()
        {

            colTenDV.DataBindings.Add("Text", DataSource, "TenDV");
            celLoai.DataBindings.Add("Text", DataSource, "LoaiPTTT");
            if (_sotrang == 1)
            {
                celNoiBHYT1.DataBindings.Add("Text", DataSource, "NoiTBHYT4").FormatString = DungChung.Bien.FormatString[0];
                celNoiTDV1.DataBindings.Add("Text", DataSource, "NoiTDV4").FormatString = DungChung.Bien.FormatString[0];
                celNgTBHYT1.DataBindings.Add("Text", DataSource, "NgTBHYT4").FormatString = DungChung.Bien.FormatString[0];
                celNgTDV1.DataBindings.Add("Text", DataSource, "NgTDV4").FormatString = DungChung.Bien.FormatString[0];

                celNoiBHYT2.DataBindings.Add("Text", DataSource, "NoiTBHYT5").FormatString = DungChung.Bien.FormatString[0];
                celNoiTDV2.DataBindings.Add("Text", DataSource, "NoiTDV5").FormatString = DungChung.Bien.FormatString[0];
                celNgTBHYT2.DataBindings.Add("Text", DataSource, "NgTBHYT5").FormatString = DungChung.Bien.FormatString[0];
                celNgTDV2.DataBindings.Add("Text", DataSource, "NgTDV5").FormatString = DungChung.Bien.FormatString[0];

                celNoiBHYT3.DataBindings.Add("Text", DataSource, "NoiTBHYT6").FormatString = DungChung.Bien.FormatString[0];
                celNoiTDV3.DataBindings.Add("Text", DataSource, "NoiTDV6").FormatString = DungChung.Bien.FormatString[0];
                celNgTBHYT3.DataBindings.Add("Text", DataSource, "NgTBHYT6").FormatString = DungChung.Bien.FormatString[0];
                celNgTDV3.DataBindings.Add("Text", DataSource, "NgTDV6").FormatString = DungChung.Bien.FormatString[0];

                celNoiBHYT4.DataBindings.Add("Text", DataSource, "NoiTBHYT7").FormatString = DungChung.Bien.FormatString[0];
                celNoiTDV4.DataBindings.Add("Text", DataSource, "NoiTDV7").FormatString = DungChung.Bien.FormatString[0];
                celNgTBHYT4.DataBindings.Add("Text", DataSource, "NgTBHYT7").FormatString = DungChung.Bien.FormatString[0];
                celNgTDV4.DataBindings.Add("Text", DataSource, "NgTDV7").FormatString = DungChung.Bien.FormatString[0];

            }
            else if(_sotrang==2)
            {
                celNoiBHYT1.DataBindings.Add("Text", DataSource, "NoiTBHYT8").FormatString = DungChung.Bien.FormatString[0];
                celNoiTDV1.DataBindings.Add("Text", DataSource, "NoiTDV8").FormatString = DungChung.Bien.FormatString[0];
                celNgTBHYT1.DataBindings.Add("Text", DataSource, "NgTBHYT8").FormatString = DungChung.Bien.FormatString[0];
                celNgTDV1.DataBindings.Add("Text", DataSource, "NgTDV8").FormatString = DungChung.Bien.FormatString[0];

                celNoiBHYT2.DataBindings.Add("Text", DataSource, "NoiTBHYT9").FormatString = DungChung.Bien.FormatString[0];
                celNoiTDV2.DataBindings.Add("Text", DataSource, "NoiTDV9").FormatString = DungChung.Bien.FormatString[0];
                celNgTBHYT2.DataBindings.Add("Text", DataSource, "NgTBHYT9").FormatString = DungChung.Bien.FormatString[0];
                celNgTDV2.DataBindings.Add("Text", DataSource, "NgTDV9").FormatString = DungChung.Bien.FormatString[0];

                celNoiBHYT3.DataBindings.Add("Text", DataSource, "NoiTBHYT10").FormatString = DungChung.Bien.FormatString[0];
                celNoiTDV3.DataBindings.Add("Text", DataSource, "NoiTDV10").FormatString = DungChung.Bien.FormatString[0];
                celNgTBHYT3.DataBindings.Add("Text", DataSource, "NgTBHYT10").FormatString = DungChung.Bien.FormatString[0];
                celNgTDV3.DataBindings.Add("Text", DataSource, "NgTDV10").FormatString = DungChung.Bien.FormatString[0];

                celNoiBHYT4.DataBindings.Add("Text", DataSource, "NoiTBHYT11").FormatString = DungChung.Bien.FormatString[0];
                celNoiTDV4.DataBindings.Add("Text", DataSource, "NoiTDV11").FormatString = DungChung.Bien.FormatString[0];
                celNgTBHYT4.DataBindings.Add("Text", DataSource, "NgTBHYT11").FormatString = DungChung.Bien.FormatString[0];
                celNgTDV4.DataBindings.Add("Text", DataSource, "NgTDV11").FormatString = DungChung.Bien.FormatString[0];
            }
            //celTong.DataBindings.Add("Text", DataSource, "Tong").FormatString = DungChung.Bien.FormatString[0];

            celTenNhom.DataBindings.Add("Text", DataSource, "TenNhom");
            GroupHeader1.GroupFields.Add(new GroupField("IDNhom"));
        }
        int a = 1;
        private void GroupHeader1_BeforePrint(object sender, CancelEventArgs e)
        {
            switch (a)
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

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            txtTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            txtTenCQCQ.Text = DungChung.Bien.TenCQCQ;
        }
    }
}
