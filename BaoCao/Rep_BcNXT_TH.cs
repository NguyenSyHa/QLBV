using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_BcNXT_TH : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_BcNXT_TH()
        {
            InitializeComponent();
        }
        public void BindingData()
        {
            colTenNhom.DataBindings.Add("Text", DataSource, "TenNhomCT");
            colTieuNhomDV.DataBindings.Add("Text", DataSource, "TenTN");

            colTenDV.DataBindings.Add("Text", DataSource, "TenDV");

            colDVT.DataBindings.Add("Text", DataSource, "DVT");
            colDonGia.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[1];

            sl1.DataBindings.Add("Text", DataSource, "TonDKSL").FormatString = DungChung.Bien.FormatString[0];
            tt1.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];
            colTonDKTTTong.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];

            sl2.DataBindings.Add("Text", DataSource, "NhapTKSL").FormatString = DungChung.Bien.FormatString[0];
            tt2.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];
            colNhapTKTTTong.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];

            sl3.DataBindings.Add("Text", DataSource, "NhapTKckSL").FormatString = DungChung.Bien.FormatString[0];
            tt3.DataBindings.Add("Text", DataSource, "NhapTKckTT").FormatString = DungChung.Bien.FormatString[1];
            colNhapTKckTTTong.DataBindings.Add("Text", DataSource, "NhapTKckTT").FormatString = DungChung.Bien.FormatString[1];
            
            sl4.DataBindings.Add("Text", DataSource, "XuatTKSL").FormatString = DungChung.Bien.FormatString[0];
            tt4.DataBindings.Add("Text", DataSource, "XuatTKTT").FormatString = DungChung.Bien.FormatString[1];
            colXuatTKxTTTong.DataBindings.Add("Text", DataSource, "XuatTKTT").FormatString = DungChung.Bien.FormatString[1];

            sl5.DataBindings.Add("Text", DataSource, "XuatTKSLCK").FormatString = DungChung.Bien.FormatString[0];
            tt5.DataBindings.Add("Text", DataSource, "XuatTKTTCK").FormatString = DungChung.Bien.FormatString[1];
            colXuatTKckTTTong.DataBindings.Add("Text", DataSource, "XuatTKTTCK").FormatString = DungChung.Bien.FormatString[1];

            sl6.DataBindings.Add("Text", DataSource, "XuatTKSLTong").FormatString = DungChung.Bien.FormatString[0];
            tt6.DataBindings.Add("Text", DataSource, "XuatTKTTTong").FormatString = DungChung.Bien.FormatString[1];
            colXTKTTTong.DataBindings.Add("Text", DataSource, "XuatTKTTTong").FormatString = DungChung.Bien.FormatString[1];

            sl7.DataBindings.Add("Text", DataSource, "TonCKSL").FormatString = DungChung.Bien.FormatString[0];
            tt7.DataBindings.Add("Text", DataSource, "TonCKTT").FormatString = DungChung.Bien.FormatString[1];
            colTonCKTTTong.DataBindings.Add("Text", DataSource, "TonCKTT").FormatString = DungChung.Bien.FormatString[1];

            GroupHeader1.GroupFields.Add(new GroupField("TenTN"));
            GroupHeader2.GroupFields.Add(new GroupField("TenNhomCT"));
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            colTenCQCQ.Text = DungChung.Bien.TenCQ.ToUpper() ;
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            colNguoiLapBang.Text = DungChung.Bien.NguoiLapBieu;
            colGiamDoc.Text = DungChung.Bien.GiamDoc;
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            if (this.GetCurrentColumnValue("TonDKSL") !=null)
            {
                double sl1 = Convert.ToDouble(this.GetCurrentColumnValue("TonDKSL"));
                colTonDKSL.Text = sl1.ToString("##,###");
            }

            if (this.GetCurrentColumnValue("TonDKTT") != null)
            {
                double tt1 = Convert.ToDouble(this.GetCurrentColumnValue("TonDKTT"));
                colTonDKTT.Text = tt1.ToString("##,###");
            }
            if (this.GetCurrentColumnValue("NhapTKSL") != null)
            {
                double sl2 = Convert.ToDouble(this.GetCurrentColumnValue("NhapTKSL"));
                colNhapTKSL.Text = sl2.ToString("##,###");
            }

            if (this.GetCurrentColumnValue("NhapTKTT") != null)
            {
                double tt2 = Convert.ToDouble(this.GetCurrentColumnValue("NhapTKTT"));
                colNhapTKTT.Text = tt2.ToString("##,###");
            }

            if (this.GetCurrentColumnValue("NhapTKckSL") != null)
            {
                double sl3 = Convert.ToDouble(this.GetCurrentColumnValue("NhapTKckSL"));
                colNhapTKckSL.Text = sl3.ToString("##,###");
            }

            if (this.GetCurrentColumnValue("NhapTKckTT") != null)
            {
                double tt3 = Convert.ToDouble(this.GetCurrentColumnValue("NhapTKckTT"));
                colNhapTKckTT.Text = tt3.ToString("##,###");
            }
            if (this.GetCurrentColumnValue("XuatTKSL") != null)
            {
                double sl4 = Convert.ToDouble(this.GetCurrentColumnValue("XuatTKSL"));
                colXuatTKxSL.Text = sl4.ToString("##,###");
            }

            if (this.GetCurrentColumnValue("XuatTKTT") != null)
            {
                double tt4 = Convert.ToDouble(this.GetCurrentColumnValue("XuatTKTT"));
                colXuatTKxTT.Text = tt4.ToString("##,###");
            }
            if (this.GetCurrentColumnValue("XuatTKSLCK") != null)
            {
                double sl5 = Convert.ToDouble(this.GetCurrentColumnValue("XuatTKSLCK"));
                colXuatTKckSL.Text = sl5.ToString("##,###");
            }

            if (this.GetCurrentColumnValue("XuatTKTTCK") != null)
            {
                double tt5 = Convert.ToDouble(this.GetCurrentColumnValue("XuatTKTTCK"));
                colXuatTKckTT.Text = tt5.ToString("##,###");
            }
            if (this.GetCurrentColumnValue("XuatTKSLTong") != null)
            {
                double sl6 = Convert.ToDouble(this.GetCurrentColumnValue("XuatTKSLTong"));
                colXTKSL.Text = sl6.ToString("##,###");
            }

            if (this.GetCurrentColumnValue("XuatTKTTTong") != null)
            {
                double tt6 = Convert.ToDouble(this.GetCurrentColumnValue("XuatTKTTTong"));
                colXTKTT.Text = tt6.ToString("##,###");
            }
            if (this.GetCurrentColumnValue("TonCKSL") != null)
            {
                double sl7 = Convert.ToDouble(this.GetCurrentColumnValue("TonCKSL"));
                colTonCKSL.Text = sl7.ToString("##,###");
            }

            if (this.GetCurrentColumnValue("TonCKTT") != null)
            {
                double tt7 = Convert.ToDouble(this.GetCurrentColumnValue("TonCKTT"));
                colTonCKTT.Text = tt7.ToString("##,###");
            } 
        }

        private void GroupHeader2_BeforePrint(object sender, CancelEventArgs e)
        {
            if (Convert.ToInt32(Nhom.Value) == 1)
            {   
               // int t =Convert.ToInt32(Nhom.Value);
             GroupHeader2.Visible = false; }
        }

        private void GroupHeader1_BeforePrint(object sender, CancelEventArgs e)
        {
            if (Convert.ToInt32(TNhom.Value) == 1)
            { GroupHeader1.Visible = false; }
        }

        private void xrLabel22_BeforePrint(object sender, CancelEventArgs e)
        {

        }
    }
}
