using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class repBbKKThuoc_20001 : DevExpress.XtraReports.UI.XtraReport
    {
        private bool HTNuocSX;
        bool _Thang = true;
        public repBbKKThuoc_20001()
        {
            InitializeComponent();
           
        }
        public repBbKKThuoc_20001(bool HTNuocSX, bool Thang)
        {
            // TODO: Complete member initialization
            this.HTNuocSX = HTNuocSX;
            _Thang = Thang;
            InitializeComponent();
        }
        public void BindingData()
        {

            txtNT.DataBindings.Add("Text", DataSource, "NgayThang");
            txtCT.DataBindings.Add("Text", DataSource, "SoCT");
            colTenThuocGh1.DataBindings.Add("Text", DataSource, "TenTieuNhom");
            colTenThuoc.DataBindings.Add("Text", DataSource, "TenDV");
            colDonVi.DataBindings.Add("Text", DataSource, "DonVi");
            celSDK.DataBindings.Add("Text", DataSource, "SoDK");
            colSoKiemSoat.DataBindings.Add("Text", DataSource, "SoLo");
            colNuocSX.DataBindings.Add("Text", DataSource, "NuocSX");
            colHanDung.DataBindings.Add("Text", DataSource, "HanDung");//.FormatString="{0:dd/MM/yyyy}";
            colSoLuongSS.DataBindings.Add("Text", DataSource, "SoLuongSS").FormatString = DungChung.Bien.FormatString[0];
            colSoLuongTT.DataBindings.Add("Text", DataSource, "SoLuongKK").FormatString = DungChung.Bien.FormatString[0];
            clMaDV.DataBindings.Add("Text", DataSource, "MaDV");
            //colSoLuongTTTong.DataBindings.Add("Text", DataSource, "SoLuongKK").FormatString = DungChung.Bien.FormatString[1];
            //colHongVoGh1.DataBindings.Add("Text", DataSource, "HongVo").FormatString = DungChung.Bien.FormatString[1];
            if (DungChung.Bien.MaBV != "20001")
                colHongVo.DataBindings.Add("Text", DataSource, "HongVo").FormatString = DungChung.Bien.FormatString[0];
            //colHongVoTong.DataBindings.Add("Text", DataSource, "HongVo").FormatString = DungChung.Bien.FormatString[0];
            string __tt = TT.Value.ToString();
            if (__tt == "A")
            {
                colSoLuongTT.DataBindings.Add("Text", DataSource, "SoLuongSS").FormatString = DungChung.Bien.FormatString[0];
            }
            colGhiChu.DataBindings.Add("Text", DataSource, "GhiChu");
            GroupHeader1.GroupFields.Add(new GroupField("TenTieuNhom"));
            GroupHeader2.GroupFields.Add(new GroupField("SoCT"));
            GroupHeader3.GroupFields.Add(new GroupField("NgayThang"));
            if (HTNuocSX)
            {
                lblThuocNoi.DataBindings.Add("Text", DataSource, "NuocSXGr");
                GroupHeader4.GroupFields.Add(new GroupField("NuocSXGr"));
            }


        }

        private void repBbKKThuoc_BeforePrint(object sender, CancelEventArgs e)
        {
            TenCQCQ.Value = DungChung.Bien.TenCQCQ;
            TenCQ.Value = DungChung.Bien.TenCQ;
            string _nt = NT.Value.ToString();
            string _ct = NT.Value.ToString();
            if (_nt == "1") { GroupHeader3.Visible = false; }
            else GroupHeader3.Visible = true;
            if (_ct == "1") { GroupHeader2.Visible = false; }
            else GroupHeader2.Visible = true;
            if (DungChung.Bien.MaBV == "20001")
            {
                colThanhVienKK1r.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                colThanhVienKK2r.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                colThanhVienKK3r.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                colThanhVienKK4r.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                colThanhVienKK5r.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                xrLabel20.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            }

        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            colTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            colTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            //xrRichText1.Text = TV1goi.Value.ToString();
            xrLine2.Visible = DungChung.Bien.MaBV == "20001" ? true : false;
        }

        private void GroupHeader3_BeforePrint(object sender, CancelEventArgs e)
        {
            if (this.GetCurrentColumnValue("NgayThang") != null)
            {
                string nt = this.GetCurrentColumnValue("NgayThang").ToString();
                colNgayThang.Text = "Ngày " + nt.ToString().Substring(0, 2) + " tháng " + nt.ToString().Substring(3, 2) + " năm " + nt.ToString().Substring(6, 4);
            }
        }

        private void GroupHeader2_BeforePrint(object sender, CancelEventArgs e)
        {
            if (this.GetCurrentColumnValue("SoCT") != null)
            {
                string ct = this.GetCurrentColumnValue("SoCT").ToString();
                colSoCT.Text = "Số chứng từ: " + ct;
            }
        }

        private void colHanDung_BeforePrint(object sender, CancelEventArgs e)
        {
            //if (this.GetCurrentColumnValue("HanDung") != null)
            //{
            //    string hd = this.GetCurrentColumnValue("HanDung").ToString();
            //    colHanDung.Text = hd.ToString().Substring(0,10);
            //}
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "27001")
            {
                if (_Thang == true)
                    SubBand3.Visible = true;
                else
                    SubBand2.Visible = true;
            }
            else
            {
                SubBand1.Visible = true;
            }
        }

        private void GroupHeader4_BeforePrint(object sender, CancelEventArgs e)
        {
            if (HTNuocSX)
            {
                GroupHeader4.Visible = true;
                GroupFooter4.Visible = true;
                if (this.GetCurrentColumnValue("NuocSXGr") != null)
                {
                    string nguongoc = this.GetCurrentColumnValue("NuocSXGr").ToString();
                    if (nguongoc == "1")
                        celNuocSXGr.Text = "Thuốc nội";
                    else if (nguongoc == "2")
                        celNuocSXGr.Text = "Thuốc ngoại";
                }
            }
            else
            {
                GroupHeader4.Visible = false;
                GroupFooter4.Visible = false;
            }

        }


    }
}
