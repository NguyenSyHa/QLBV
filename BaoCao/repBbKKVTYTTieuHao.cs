using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class repBbKKVTYTTieuHao : DevExpress.XtraReports.UI.XtraReport
    {
        bool _Thang = true;
        public repBbKKVTYTTieuHao(bool thang)
        {
            InitializeComponent();
            _Thang = thang;
        }
        public void BindingData()
        {
            txtNT.DataBindings.Add("Text", DataSource, "NgayThang");
            txtCT.DataBindings.Add("Text", DataSource, "SoCT"); colTenVTYTGh2.DataBindings.Add("Text", DataSource, "TenTieuNhom");
            txtCT.DataBindings.Add("Text", DataSource, "SoCT"); colTenVTYTGh1.DataBindings.Add("Text", DataSource, "TenTieuNhom");
            if (DungChung.Bien.MaBV == "27001" || DungChung.Bien.MaBV == "27023")
            {
                PanelNew.Visible = true;
                xrTable21.Visible = true;
                xrTable22.Visible = true;
                xrTable23.Visible = true;
                PanelOld.Visible = false;
                xrTable7.Visible = false;
                xrTable8.Visible = false;
                xrTable3.Visible = false;

                colTHC1.DataBindings.Add("Text", DataSource, "TenDV");
                colDV1.DataBindings.Add("Text", DataSource, "DonVi");
                colSKS1.DataBindings.Add("Text", DataSource, "SoLo");
                colNSS1.DataBindings.Add("Text", DataSource, "NuocSX");
                colHD1.DataBindings.Add("Text", DataSource, "HanDung");
                colSLSS1.DataBindings.Add("Text", DataSource, "SoLuongSS").FormatString = DungChung.Bien.FormatString[0];
                colSoLuongSSTong1.DataBindings.Add("Text", DataSource, "SoLuongSS").FormatString = DungChung.Bien.FormatString[0];
                //colSoLuongTTGh1.DataBindings.Add("Text", DataSource, "SoLuongKK").FormatString = DungChung.Bien.FormatString[1];
                string __tt = TT.Value.ToString();
                if (__tt == "A")
                {
                    colSLTT1.DataBindings.Add("Text", DataSource, "SoLuongSS").FormatString = DungChung.Bien.FormatString[0];

                }
                //  colSoLuongTT.DataBindings.Add("Text", DataSource, "SoLuongKK").FormatString = DungChung.Bien.FormatString[0];
                //colSoLuongTTTong.DataBindings.Add("Text", DataSource, "SoLuongKK").FormatString = DungChung.Bien.FormatString[1];
                //colHongVoGh1.DataBindings.Add("Text", DataSource, "SoLuongX").FormatString = DungChung.Bien.FormatString[1];
                if (DungChung.Bien.MaBV != "20001")
                    colHV1.DataBindings.Add("Text", DataSource, "HongVo").FormatString = DungChung.Bien.FormatString[0];
                //colHongVoTong.DataBindings.Add("Text", DataSource, "HongVo").FormatString = DungChung.Bien.FormatString[0];
                //colHongVoGh1.DataBindings.Add("Text", DataSource, "HongVo").FormatString = DungChung.Bien.FormatString[1];
                colGC1.DataBindings.Add("Text", DataSource, "GhiChu");
            } else
            {
                PanelNew.Visible = false;
                xrTable21.Visible = false;
                xrTable22.Visible = false;
                xrTable23.Visible = false;
                PanelOld.Visible = true;
                xrTable7.Visible = true;
                xrTable8.Visible = true;
                xrTable3.Visible = true;
                colTenVTYT.DataBindings.Add("Text", DataSource, "TenDV");
                colDV.DataBindings.Add("Text", DataSource, "DonVi");
                colSoKiemSoat.DataBindings.Add("Text", DataSource, "SoLo");
                colNuocSX.DataBindings.Add("Text", DataSource, "NuocSX");
                colSoLuongSS.DataBindings.Add("Text", DataSource, "SoLuongSS").FormatString = DungChung.Bien.FormatString[0];
                colSoLuongSSTong.DataBindings.Add("Text", DataSource, "SoLuongSS").FormatString = DungChung.Bien.FormatString[0];
                //colSoLuongTTGh1.DataBindings.Add("Text", DataSource, "SoLuongKK").FormatString = DungChung.Bien.FormatString[1];
                string __tt = TT.Value.ToString();
                if (__tt == "A")
                {
                    colSoLuongTT.DataBindings.Add("Text", DataSource, "SoLuongSS").FormatString = DungChung.Bien.FormatString[0];

                }
                //  colSoLuongTT.DataBindings.Add("Text", DataSource, "SoLuongKK").FormatString = DungChung.Bien.FormatString[0];
                //colSoLuongTTTong.DataBindings.Add("Text", DataSource, "SoLuongKK").FormatString = DungChung.Bien.FormatString[1];
                //colHongVoGh1.DataBindings.Add("Text", DataSource, "SoLuongX").FormatString = DungChung.Bien.FormatString[1];
                if (DungChung.Bien.MaBV != "20001")
                    colHongVo.DataBindings.Add("Text", DataSource, "HongVo").FormatString = DungChung.Bien.FormatString[0];
                //colHongVoTong.DataBindings.Add("Text", DataSource, "HongVo").FormatString = DungChung.Bien.FormatString[0];
                //colHongVoGh1.DataBindings.Add("Text", DataSource, "HongVo").FormatString = DungChung.Bien.FormatString[1];
                colGhiChu.DataBindings.Add("Text", DataSource, "GhiChu");
            }

            GroupHeader1.GroupFields.Add(new GroupField("TenTieuNhom"));
            GroupHeader2.GroupFields.Add(new GroupField("SoCT"));
            GroupHeader3.GroupFields.Add(new GroupField("NgayThang"));

            
            
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            colTenCQCQ.Text = colTenCQCQ2.Text = DungChung.Bien.TenCQCQ.ToUpper();
            colTenCQ.Text = colTenCQ2.Text = DungChung.Bien.TenCQ.ToUpper();
            xrPictureBox1.Image = DungChung.Ham.GetLogo();
            if (DungChung.Bien.MaBV == "24272")
            {
                SubBand4.Visible = false;
                SubBand5.Visible = true;
            }
            string _nt = NT.Value.ToString();
            string _ct = NT.Value.ToString();
            if (_nt == "1") { GroupHeader3.Visible = false; }
            else GroupHeader3.Visible = true;
            if (_ct == "1") { GroupHeader2.Visible = false; }
            else GroupHeader2.Visible = true;
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

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "27001")
            {
                if (_Thang == true)
                    SubBand3.Visible = true;
                else
                    SubBand2.Visible = true;

                xrLabel65.Visible = true;
                xrLabel63.Visible = true;
                xrLabel61.Visible = true;
                xrLabel60.Visible = true;
                xrLabel71.Visible = true;
                xrLabel68.Visible = true;
                xrLabel67.Visible = true;
                xrLabel66.Visible = true;
            }
            else
            {
                SubBand1.Visible = true;
            }
        }

       
    }
}
