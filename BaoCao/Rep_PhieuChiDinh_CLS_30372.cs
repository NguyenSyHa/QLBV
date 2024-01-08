using DevExpress.XtraReports.UI;
using QLBV.Utilities.Commons;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

namespace QLBV.BaoCao
{
    public partial class Rep_PhieuChiDinh_CLS_30372 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_PhieuChiDinh_CLS_30372()
        {
            InitializeComponent();
        }
        bool HienThiGH = false;
        public Rep_PhieuChiDinh_CLS_30372(bool hienthi)
        {
            InitializeComponent();
            HienThiGH = hienthi;
        }
        public void BindingData()
        {
            colYCKT1.DataBindings.Add("Text", DataSource, "tendv");
            colTenNhomDVg2.DataBindings.Add("Text", DataSource, "TenTN");
            colTenNhomDVSB.DataBindings.Add("Text", DataSource, "Tnhom");
            colNoiThucHien.DataBindings.Add("Text", DataSource, "NoiTH");
            ColSL.DataBindings.Add("Text", DataSource, "NoiTH");
            colDonGia.DataBindings.Add("Text", DataSource, "TrongDM");
            GroupHeader1.GroupFields.Add(new GroupField("Tnhom"));
            GroupHeader2.GroupFields.Add(new GroupField("TenTN"));
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            xrPictureBox1.Image = DungChung.Ham.GetLogo();
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
        }

        private void ColDGDV_BeforePrint(object sender, CancelEventArgs e)
        {
            int dgdv = 0;
            int dgbh = 0;
            int trongdm = 0;
            if (GetCurrentColumnValue("DonGiaBHYT") != null)
            {
                dgdv = int.Parse(this.GetCurrentColumnValue("DonGiaBHYT").ToString());
                if (GetCurrentColumnValue("TrongDM") != null)
                {
                    trongdm = int.Parse(this.GetCurrentColumnValue("TrongDM").ToString());
                    if (trongdm == 1)
                    {
                        ColDGBHYT.Text = dgdv.ToString("###,###.##");
                        ColDGDV.Text = "0";
                    }
                }
            }
            if (GetCurrentColumnValue("DonGiaDV") != null)
            {
                dgbh = int.Parse(this.GetCurrentColumnValue("DonGiaDV").ToString());
                if (GetCurrentColumnValue("TrongDM") != null)
                {
                    trongdm = int.Parse(this.GetCurrentColumnValue("TrongDM").ToString());
                    if (trongdm == 0)
                    {
                        ColDGDV.Text = dgbh.ToString("###,###.##");
                        ColDGBHYT.Text = "0";
                    }
                }
            }
        }
       
        int i = 1;
        private void xrTableCell4_BeforePrint(object sender, CancelEventArgs e)
        {
            xrTableCell4.Text = ChuyenSoLaMa.ToRoman(i);
            i++;
        }
    }
}
