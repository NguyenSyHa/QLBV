using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_SoXN_SoiTrucTiep : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_SoXN_SoiTrucTiep()
        {
            InitializeComponent();
        }


        internal void DataBinding()
        {
            celSoXN.DataBindings.Add("Text", DataSource, "soXN");
            celNgayNhanMau.DataBindings.Add("Text", DataSource, "NgayNhanMau").FormatString = "{0:dd/MM/yy}";
            celHoTen.DataBindings.Add("Text", DataSource, "TenBNhan");
            celTuoiNam.DataBindings.Add("Text", DataSource, "TuoiNam");
            celTuoiNu.DataBindings.Add("Text", DataSource, "TuoiNu");
            celDiaChi.DataBindings.Add("Text", DataSource, "DChi");
            celDviYeuCau.DataBindings.Add("Text", DataSource, "TenKP");
            celBPDom1.DataBindings.Add("Text", DataSource, "BPDom");
            celBPDom2.DataBindings.Add("Text", DataSource, "BPKhac");
            trangThaiBP4.DataBindings.Add("Text", DataSource, "TrangThaiBP");
            Phathien_PH.DataBindings.Add("Text", DataSource, "LydoXN1");
            PhatHienHTest.DataBindings.Add("Text", DataSource, "LydoXN2");
            elThangTD.DataBindings.Add("Text", DataSource, "LydoXN3");
            M1.DataBindings.Add("Text", DataSource, "M1");
            M2.DataBindings.Add("Text", DataSource, "M2");
            celQLBN.DataBindings.Add("Text", DataSource, "QLBN");
            XNVienKT.DataBindings.Add("Text", DataSource, "CanBoTH");           
            GhiChu.DataBindings.Add("Text", DataSource, "GhiChu");
           
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            M1.ForeColor = System.Drawing.Color.Black;
            this.M1.StylePriority.UseForeColor = false;
            M2.ForeColor = System.Drawing.Color.Black;
            this.M2.StylePriority.UseForeColor = false;

            if (this.GetCurrentColumnValue("M1") != null)
            {
                string kq1 = this.GetCurrentColumnValue("M1").ToString();
                if (kq1 != "Âm")
                {
                    M1.ForeColor = System.Drawing.Color.Red;
                    this.M1.StylePriority.UseForeColor = false;
                }  
            }
            if (this.GetCurrentColumnValue("M2") != null)
            {
                string kq2 = this.GetCurrentColumnValue("M2").ToString();
                if (kq2 != "Âm")
                {
                    M2.ForeColor = System.Drawing.Color.Red;
                    this.M2.StylePriority.UseForeColor = false;
                }

            }
        }
    }
}
