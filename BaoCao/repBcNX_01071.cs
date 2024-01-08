using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class repBcNX_01071 : DevExpress.XtraReports.UI.XtraReport
    {
        public repBcNX_01071()
        {
            InitializeComponent();
        }
        string[] ar;
        public repBcNX_01071(string[] ar1)
        {
            InitializeComponent();
            ar = ar1;
        }
        public void BindingData()
        {
            colTenHamLuongGh2.DataBindings.Add("Text", DataSource, "TenNhomDuoc");
            colTenHamLuong.DataBindings.Add("Text", DataSource, "TenHamLuong");
            colDVT.DataBindings.Add("Text", DataSource, "DonVi");
            colDonGia.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[1];
            colTonDKSL.DataBindings.Add("Text", DataSource, "TonDKSL").FormatString = DungChung.Bien.FormatString[0];
            colNhapTKSL.DataBindings.Add("Text", DataSource, "NhapTKSL").FormatString = DungChung.Bien.FormatString[0];
            colNhapTraDuocSL.DataBindings.Add("Text", DataSource, "NhapTra_SL").FormatString = DungChung.Bien.FormatString[0];
            colTonCKSL.DataBindings.Add("Text", DataSource, "TonCKSL").FormatString = DungChung.Bien.FormatString[0];
            colXuat1SL.DataBindings.Add("Text", DataSource, ar[0]).FormatString = DungChung.Bien.FormatString[0];
            colXuat2SL.DataBindings.Add("Text", DataSource, ar[1]).FormatString = DungChung.Bien.FormatString[0];
            colXuat3SL.DataBindings.Add("Text", DataSource, ar[2]).FormatString = DungChung.Bien.FormatString[0];
            colXuat4SL.DataBindings.Add("Text", DataSource, ar[3]).FormatString = DungChung.Bien.FormatString[0];
            colXuat5SL.DataBindings.Add("Text", DataSource, ar[4]).FormatString = DungChung.Bien.FormatString[0];
            colXuat6SL.DataBindings.Add("Text", DataSource, ar[5]).FormatString = DungChung.Bien.FormatString[0];

            GroupHeader1.GroupFields.Add(new GroupField("TenTN"));
            colTenHamLuongGh1.DataBindings.Add("Text", DataSource, "TenTN");
            GroupHeader3.GroupFields.Add(new GroupField("NuocSX"));
            GroupHeader2.GroupFields.Add(new GroupField("TenNhomDuoc"));
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            colNguoiLapBang.Text = DungChung.Bien.NguoiLapBieu;
            colKhoaDuoc.Text = DungChung.Bien.TruongKhoaDuoc;
            colKeToan.Text = DungChung.Bien.KeToanTruong;
            colGiamDoc.Text = DungChung.Bien.GiamDoc;

            colKhoaDuoc2.Text = DungChung.Bien.TruongKhoaDuoc;
            colKeToan2.Text = DungChung.Bien.KeToanTruong;
            colGiamDoc2.Text = DungChung.Bien.GiamDoc;

            colKeToan3.Text = DungChung.Bien.KeToanTruong;
            colThuKho3.Text = DungChung.Bien.ThuKho;
            colpgd3.Text = DungChung.Bien.TruongKhoaDuoc;

            SubBand2.Visible = false;
            SubBand3.Visible = false;

            GroupHeader1.Visible = true;
            GroupHeader3.Visible = true;
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            TenCQ.Value = DungChung.Bien.TenCQ;
            TenCQCQ.Value = DungChung.Bien.TenCQCQ;
        }

        int sttGh2 = 1;
        private void GroupHeader2_BeforePrint(object sender, CancelEventArgs e)
        {
            switch (sttGh2)
            {
                case 1:
                    colSTTGh2.Text = "I";
                    break;
                case 2:
                    colSTTGh2.Text = "II";
                    break;
                case 3:
                    colSTTGh2.Text = "III";
                    break;
                case 4:
                    colSTTGh2.Text = "IV";
                    break;
                case 5:
                    colSTTGh2.Text = "IV";
                    break;

            }
            sttGh2++;
        }

        private void PageHeader_BeforePrint(object sender, CancelEventArgs e)
        {

        }

        private void GroupHeader3_BeforePrint(object sender, CancelEventArgs e)
        {
            if (this.GetCurrentColumnValue("NuocSX") != null)
            {
                if (this.GetCurrentColumnValue("NuocSX").ToString() == "1")
                    celTrongNgoaiNuoc.Text = "Thuốc nội";
                else
                    celTrongNgoaiNuoc.Text = "Thuốc ngoại";
            }
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            int _tongxuat = 0; int _xuat1 = 0; int _xuat2 = 0; int _xuat3 = 0; int _xuat4 = 0; int _xuat5 = 0; int _xuat6 = 0;
            if (this.GetCurrentColumnValue(ar[0]) != null)
            {
                _xuat1 = Convert.ToInt32(this.GetCurrentColumnValue(ar[0]));
                _tongxuat = _tongxuat + _xuat1;
            }
            if (this.GetCurrentColumnValue(ar[1]) != null)
            {
                _xuat2 = Convert.ToInt32(this.GetCurrentColumnValue(ar[1]));
                _tongxuat = _tongxuat + _xuat2;
            }
            if (this.GetCurrentColumnValue(ar[2]) != null)
            {
                _xuat3 = Convert.ToInt32(this.GetCurrentColumnValue(ar[2]));
                _tongxuat = _tongxuat + _xuat3;
            }
            if (this.GetCurrentColumnValue(ar[3]) != null)
            {
                _xuat4 = Convert.ToInt32(this.GetCurrentColumnValue(ar[3]));
                _tongxuat = _tongxuat + _xuat4;
            }
            if (this.GetCurrentColumnValue(ar[4]) != null)
            {
                _xuat5 = Convert.ToInt32(this.GetCurrentColumnValue(ar[4]));
                _tongxuat = _tongxuat + _xuat5;
            }
            if (this.GetCurrentColumnValue(ar[5]) != null)
            {
                _xuat6 = Convert.ToInt32(this.GetCurrentColumnValue(ar[5]));
                _tongxuat = _tongxuat + _xuat6;
            }
            colTongXuat.Text = _tongxuat.ToString("#,#");
        }
    }
}
