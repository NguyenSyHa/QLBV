using DevExpress.XtraReports.UI;
using System.ComponentModel;

namespace QLBV.BaoCao
{
    public partial class rep_BangKe_NgoaiTru_01071 : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_BangKe_NgoaiTru_01071()
        {
            InitializeComponent();
        }

        public void Databinding(int i)
        {
            if (i == 1)
            {
                colTenDV.DataBindings.Add("Text", DataSource, "TenDV");
                colDonvi.DataBindings.Add("Text", DataSource, "DonVi");
                colSoLuong.DataBindings.Add("Text", DataSource, "Sluong");
                ////colTrongDM.DataBindings.Add("Text", DataSource, "TrongDM");
                colTenKP.DataBindings.Add("Text", DataSource, "TenKP");
                colTenNhom.DataBindings.Add("Text", DataSource, "TenNhom");
                colTenTN.DataBindings.Add("Text", DataSource, "TenTN");

                GroupHeader1.GroupFields.Add(new GroupField("STTTN"));
                GroupHeader1.GroupFields.Add(new GroupField("TenTN"));
                GroupHeader2.GroupFields.Add(new GroupField("STTN"));
                GroupHeader3.GroupFields.Add(new GroupField("TenKP"));
                //GroupHeader4.GroupFields.Add(new GroupField("STT1"));
            }
            else
            {
                colTenDV.DataBindings.Add("Text", DataSource, "TenDV");
                colDonvi.DataBindings.Add("Text", DataSource, "DonVi");
                colSoLuong.DataBindings.Add("Text", DataSource, "Sluong");
                ////colTrongDM.DataBindings.Add("Text", DataSource, "TrongDM");
                colTenKP.DataBindings.Add("Text", DataSource, "TenKP");
                if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049")
                {
                    colTenNhom.DataBindings.Add("Text", DataSource, "TenNhomBK02");
                }
                else
                {
                    colTenNhom.DataBindings.Add("Text", DataSource, "TenNhom");
                }
                colTenTN.DataBindings.Add("Text", DataSource, "TenTN");

                GroupHeader1.GroupFields.Add(new GroupField("STTTN"));
                GroupHeader1.GroupFields.Add(new GroupField("TenTN"));
                GroupHeader2.GroupFields.Add(new GroupField("STTN"));
                GroupHeader3.GroupFields.Add(new GroupField("TenKP"));
                //GroupHeader4.GroupFields.Add(new GroupField("STT1"));
            }
        }

        public void Databinding()
        {
            colTenDV.DataBindings.Add("Text", DataSource, "TenDV");
            colDonvi.DataBindings.Add("Text", DataSource, "DonVi");
            colSoLuong.DataBindings.Add("Text", DataSource, "Sluong");
            ////colTrongDM.DataBindings.Add("Text", DataSource, "TrongDM");
            colTenKP.DataBindings.Add("Text", DataSource, "TenKP");
            colTenNhom.DataBindings.Add("Text", DataSource, "TenNhom");
            colTenTN.DataBindings.Add("Text", DataSource, "TenTN");

            GroupHeader1.GroupFields.Add(new GroupField("STTTN"));
            GroupHeader1.GroupFields.Add(new GroupField("TenTN"));
            GroupHeader2.GroupFields.Add(new GroupField("STTN"));
            GroupHeader3.GroupFields.Add(new GroupField("TenKP"));
            //GroupHeader4.GroupFields.Add(new GroupField("STT1"));
        }

        private int stt4 = 1;

        private void GroupHeader4_BeforePrint(object sender, CancelEventArgs e)
        {
            switch (stt4)
            {
                case 1:
                    colsst4.Text = "I.";
                    break;

                case 2:
                    colsst4.Text = "II.";
                    break;
            }
            stt4++;
        }

        private int stt3 = 1;

        private void GroupHeader3_BeforePrint(object sender, CancelEventArgs e)
        {
            stt3 = 1;
        }

        private int stt2 = 1;

        private void GroupHeader2_BeforePrint(object sender, CancelEventArgs e)
        {
            switch (stt3)
            {
                case 1:
                    colstt2.Text = "1.";
                    break;

                case 2:
                    colstt2.Text = "2.";
                    break;

                case 3:
                    colstt2.Text = "3.";
                    break;

                case 4:
                    colstt2.Text = "4.";
                    break;

                case 5:
                    colstt2.Text = "5.";
                    break;

                case 6:
                    colstt2.Text = "6.";
                    break;
            }
            stt3++;
        }

        private int stt1 = 1;

        private void GroupHeader1_BeforePrint(object sender, CancelEventArgs e)
        {
            //switch (stt1)
            //{
            //    case 1:
            //        colstt1.Text = stt4 + "." + stt3 + "."+stt2+"1.";
            //        break;
            //    case 2:
            //        colstt2.Text = stt4 + "." + stt3 + "." + stt2 + "2.";
            //        break;
            //    case 3:
            //        colstt2.Text = stt4 + "." + stt3 + "." + stt2 + "3.";
            //        break;
            //    case 4:
            //        colstt2.Text = stt4 + "." + stt3 + "." + stt2 + "4.";
            //        break;
            //    case 5:
            //        colstt2.Text = stt4 + "." + stt3 + "." + stt2 + "5.";
            //        break;
            //    case 6:
            //        colstt2.Text = stt4 + "." + stt3 + "." + stt2 + "6.";
            //        break;
            //}
            //stt2++;
        }
    }
}