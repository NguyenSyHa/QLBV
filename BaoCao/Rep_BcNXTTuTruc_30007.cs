using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_BcNXTTuTruc_30007 : DevExpress.XtraReports.UI.XtraReport
    {
        private bool hienthi = false;

        public Rep_BcNXTTuTruc_30007()
        {
            InitializeComponent();
        }

        public Rep_BcNXTTuTruc_30007(bool p)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.hienthi = p;
        }
        public void BindingData()
        {
            
            colTenDV.DataBindings.Add("Text", DataSource, "TenDV");
            txtMaDV.DataBindings.Add("Text", DataSource, "MaDV");
            colMaNB.DataBindings.Add("Text", DataSource, "MaNB");

            colDVT.DataBindings.Add("Text", DataSource, "DVT");
            colDonGia.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[1];

            txtTonDKSL.DataBindings.Add("Text", DataSource, "sltondk").FormatString = DungChung.Bien.FormatString[1];
            txtTonDKTT.DataBindings.Add("Text", DataSource, "tttondk").FormatString = DungChung.Bien.FormatString[1];
            colTonDKTTTong.DataBindings.Add("Text", DataSource, "tttondk").FormatString = DungChung.Bien.FormatString[1];
            
            txtNhapTKSL.DataBindings.Add("Text", DataSource, "slnhap").FormatString = DungChung.Bien.FormatString[1];
            txtNhapTKTT.DataBindings.Add("Text", DataSource, "ttnhap").FormatString = DungChung.Bien.FormatString[1];
            colNhapTKTTTong.DataBindings.Add("Text", DataSource, "ttnhap").FormatString = DungChung.Bien.FormatString[1];

            txtXuatTKSL.DataBindings.Add("Text", DataSource, "slxuat").FormatString = DungChung.Bien.FormatString[1];
            txtXuatTKTT.DataBindings.Add("Text", DataSource, "ttxuat").FormatString = DungChung.Bien.FormatString[1];
            colXTKTTTong.DataBindings.Add("Text", DataSource, "ttxuat").FormatString = DungChung.Bien.FormatString[1];

            txtTonCKSL.DataBindings.Add("Text", DataSource, "sltonck").FormatString = DungChung.Bien.FormatString[1];
            txtTonCKTT.DataBindings.Add("Text", DataSource, "tttonck").FormatString = DungChung.Bien.FormatString[1];
            colTonCKTTTong.DataBindings.Add("Text", DataSource, "tttonck").FormatString = DungChung.Bien.FormatString[1];

            colTieuNhomDV.DataBindings.Add("Text", DataSource, "TieuNhomDV");
            colTonDKTTG.DataBindings.Add("Text", DataSource, "tttondk").FormatString = DungChung.Bien.FormatString[1];
            colNhapTKTTG.DataBindings.Add("Text", DataSource, "ttnhap").FormatString = DungChung.Bien.FormatString[1];
            colXTKTTG.DataBindings.Add("Text", DataSource, "ttxuat").FormatString = DungChung.Bien.FormatString[1];
            colTonCKTTG.DataBindings.Add("Text", DataSource, "tttonck").FormatString = DungChung.Bien.FormatString[1];
           
            if(hienthi)
            {
               
                GroupHeader1.GroupFields.Add(new GroupField("TieuNhomDV"));
            }
            //GroupHeader1.GroupFields.Add(new GroupField("TieuNhomDV"));
            //GroupHeader2.GroupFields.Add(new GroupField("TenNhomDuoc"));
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            colTenCQCQ.Text = DungChung.Bien.TenCQ;
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            colNguoiLapBang.Text = DungChung.Bien.NguoiLapBieu;
            colGiamDoc.Text = DungChung.Bien.GiamDoc;
            //GroupFooter1.Visible = hienthi;
            //GroupHeader1.Visible = hienthi;
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            int _tondksl = 0; int _tondktt = 0; int _nhaptksl = 0; int _nhaptktt = 0; int _xuattksl = 0; int _xuattktt = 0; int _toncksl = 0; int _toncktt = 0;
            if (this.GetCurrentColumnValue("sltondk") != null)
            {
                _tondksl = Convert.ToInt32(this.GetCurrentColumnValue("sltondk"));
                if (_tondksl != 0)
                {
                    colTonDKSL.Text = _tondksl.ToString("#,#");
                }
                else { colTonDKSL.Text = ""; }
            }
            if (this.GetCurrentColumnValue("tttondk") != null)
            {
                _tondktt = Convert.ToInt32(this.GetCurrentColumnValue("tttondk"));
                if (_tondktt != 0)
                {
                    colTonDKTT.Text = _tondktt.ToString("#,#");
                }
                else { colTonDKTT.Text = ""; }
            }
             if (this.GetCurrentColumnValue("slnhap") != null)
            {
                _nhaptksl = Convert.ToInt32(this.GetCurrentColumnValue("slnhap"));
                if (_nhaptksl != 0)
                {
                    colNhapTKSL.Text = _nhaptksl.ToString("#,#");
                }
                else { colNhapTKSL.Text = ""; }
            }
            if (this.GetCurrentColumnValue("ttnhap") != null)
            {
                _nhaptktt = Convert.ToInt32(this.GetCurrentColumnValue("ttnhap"));
                if (_nhaptktt != 0)
                {
                    colNhapTKTT.Text = _nhaptktt.ToString("#,#");
                }
                else { colNhapTKTT.Text = ""; }
            }
              if (this.GetCurrentColumnValue("slxuat") != null)
            {
                _xuattksl = Convert.ToInt32(this.GetCurrentColumnValue("slxuat"));
                if (_xuattksl != 0)
                {
                    colXTKSL.Text = _xuattksl.ToString("#,#");
                }
                else { colXTKSL.Text = ""; }
            }
            if (this.GetCurrentColumnValue("ttxuat") != null)
            {
                _xuattktt = Convert.ToInt32(this.GetCurrentColumnValue("ttxuat"));
                if (_xuattktt != 0)
                {
                    colXTKTT.Text = _xuattktt.ToString("#,#");
                }
                else { colXTKTT.Text = ""; }
            }
            if (this.GetCurrentColumnValue("sltonck") != null)
            {
                _toncksl = Convert.ToInt32(this.GetCurrentColumnValue("sltonck"));
                if (_toncksl != 0)
                {
                    colTonCKSL.Text = _toncksl.ToString("#,#");
                }
                else { colTonCKSL.Text = ""; }
            }
            if (this.GetCurrentColumnValue("tttonck") != null)
            {
                _toncktt = Convert.ToInt32(this.GetCurrentColumnValue("tttonck"));
                if (_toncktt != 0)
                {
                    colTonCKTT.Text = _toncktt.ToString("#,#");
                }
                else { colTonCKTT.Text = ""; }
            }
        }

        private void GroupFooter1_BeforePrint(object sender, CancelEventArgs e)
        {
            if (this.GetCurrentColumnValue("TieuNhomDV") != null)
            {
                celCongNhom.Text = "Cộng nhóm " + this.GetCurrentColumnValue("TieuNhomDV").ToString();
            }
            GroupFooter1.Visible = hienthi;
        }

        private void GroupHeader1_BeforePrint(object sender, CancelEventArgs e)
        {
            GroupHeader1.Visible = hienthi;
           
        }
    }
}
