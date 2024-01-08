using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_BcTaiNan : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_BcTaiNan()
        {
            InitializeComponent();
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            colTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {

            colT0t.Text = sumT0.ToString();
            colT5t.Text = sumT5.ToString();
            colT15t.Text = sumT15.ToString();
            colT20t.Text = sumT20.ToString();
            colT60t.Text = sumT60.ToString();
            colNamt.Text = sumNam.ToString();
            colNut.Text = sumNu.ToString();
            colTongSot.Text = sumTongSo.ToString();
            colVaoVient.Text = sumVaoVien.ToString();
            colChuyenVient.Text = sumChuyenVien.ToString();
            colTuVongt.Text = sumTuVong.ToString();
            colNLB.Text = DungChung.Bien.NguoiLapBieu;
        }
        internal void databinding()
        {

            colSTT.DataBindings.Add("Text", DataSource, "Stt");
            lblSTT.DataBindings.Add("Text", DataSource, "Id");
            lblIDParent.DataBindings.Add("Text", DataSource, "IdParent");
            colNoidung.DataBindings.Add("Text", DataSource, "Tenloai");
            colT0.DataBindings.Add("Text", DataSource, "T0");
            colT5.DataBindings.Add("Text", DataSource, "T5");
            colT15.DataBindings.Add("Text", DataSource, "T15");
            colT20.DataBindings.Add("Text", DataSource, "T20");
            colT60.DataBindings.Add("Text", DataSource, "T60");
            colNam.DataBindings.Add("Text", DataSource, "Nam");
            colNu.DataBindings.Add("Text", DataSource, "Nu");
            colTongSo.DataBindings.Add("Text", DataSource, "Tongso");
            colVaoVien.DataBindings.Add("Text", DataSource, "Vaovien");
            colChuyenVien.DataBindings.Add("Text", DataSource, "Chuyenvien");
            colTuVong.DataBindings.Add("Text", DataSource, "Tuvong");

            //colT0t.DataBindings.Add("Text", DataSource, "T0");
            //colT5t.DataBindings.Add("Text", DataSource, "T5");
            //colT15t.DataBindings.Add("Text", DataSource, "T15");
            //colT20t.DataBindings.Add("Text", DataSource, "T20");
            //colT60t.DataBindings.Add("Text", DataSource, "T60");
            //colNamt.DataBindings.Add("Text", DataSource, "Nam");
            //colNut.DataBindings.Add("Text", DataSource, "Nu");
            //colTongSot.DataBindings.Add("Text", DataSource, "Tongso");
            //colVaoVient.DataBindings.Add("Text", DataSource, "Vaovien");
            //colChuyenVient.DataBindings.Add("Text", DataSource, "Chuyenvien");
            //colTuVongt.DataBindings.Add("Text", DataSource, "Tuvong");
        }

        int sumT0 = 0,
            sumT5 = 0,
            sumT15 = 0,
            sumT20 = 0,
            sumT60 = 0,
            sumNam = 0,
            sumNu = 0,
            sumTongSo = 0,
            sumVaoVien = 0,
            sumChuyenVien = 0,
            sumTuVong = 0;

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
           
            if (this.GetCurrentColumnValue("IdParent") == null || Convert.ToInt32(this.GetCurrentColumnValue("IdParent")) == 0)
            {
                if (this.GetCurrentColumnValue("T0") != null)
                {
                    sumT0 += Convert.ToInt32(this.GetCurrentColumnValue("T0"));
                }

                if (this.GetCurrentColumnValue("T5") != null)
                {
                    sumT5 += Convert.ToInt32(this.GetCurrentColumnValue("T5"));
                }
                if (this.GetCurrentColumnValue("T15") != null)
                {
                    sumT15 += Convert.ToInt32(this.GetCurrentColumnValue("T15"));
                }
                if (this.GetCurrentColumnValue("T20") != null)
                {
                    sumT20 += Convert.ToInt32(this.GetCurrentColumnValue("T20"));
                }
                if (this.GetCurrentColumnValue("T60") != null)
                {
                    sumT60 += Convert.ToInt32(this.GetCurrentColumnValue("T60"));
                }
                if (this.GetCurrentColumnValue("Nam") != null)
                {
                    sumNam += Convert.ToInt32(this.GetCurrentColumnValue("Nam"));
                }
                if (this.GetCurrentColumnValue("Nu") != null)
                {
                    sumNu += Convert.ToInt32(this.GetCurrentColumnValue("Nu"));
                }
                if (this.GetCurrentColumnValue("Tongso") != null)
                {
                    sumTongSo += Convert.ToInt32(this.GetCurrentColumnValue("Tongso"));
                }
                if (this.GetCurrentColumnValue("Vaovien") != null)
                {
                    sumVaoVien += Convert.ToInt32(this.GetCurrentColumnValue("Vaovien"));
                }
                if (this.GetCurrentColumnValue("Chuyenvien") != null)
                {
                    sumChuyenVien += Convert.ToInt32(this.GetCurrentColumnValue("Chuyenvien"));
                }
                if (this.GetCurrentColumnValue("Tuvong") != null)
                {
                    sumTuVong += Convert.ToInt32(this.GetCurrentColumnValue("Tuvong"));
                }
            }

        }
    }
}
