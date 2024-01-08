using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class repBbKKVTYTTieuHao_ThanhTien : DevExpress.XtraReports.UI.XtraReport
    {
        bool _Thang = true;
        public repBbKKVTYTTieuHao_ThanhTien(bool thang)
        {
            InitializeComponent();
            _Thang = thang;
        }
        public void BindingData()
        {
            txtNT.DataBindings.Add("Text", DataSource, "NgayThang");
            txtCT.DataBindings.Add("Text", DataSource, "SoCT"); colTenVTYTGh1.DataBindings.Add("Text", DataSource, "TenTieuNhom");
            colTenVTYT.DataBindings.Add("Text", DataSource, "TenDV");
            colDV.DataBindings.Add("Text", DataSource, "DonVi");
            colSoKiemSoat.DataBindings.Add("Text", DataSource, "SoLo");
            colNuocSX.DataBindings.Add("Text", DataSource, "NuocSX");
            colDonGia.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[1];
            colSoLuongSS.DataBindings.Add("Text", DataSource, "SoLuongSS").FormatString = DungChung.Bien.FormatString[0];
            colSoLuongSSTong.DataBindings.Add("Text", DataSource, "SoLuongSS").FormatString = DungChung.Bien.FormatString[0];
            colThanhTien.DataBindings.Add("Text", DataSource, "ThanhTienSS").FormatString = DungChung.Bien.FormatString[1];
            colThanhTien_T.DataBindings.Add("Text", DataSource, "ThanhTienSS").FormatString = DungChung.Bien.FormatString[1];
            colThanhTien_Gh1.DataBindings.Add("Text", DataSource, "ThanhTienSS").FormatString = DungChung.Bien.FormatString[1];
            //colSoLuongTTGh1.DataBindings.Add("Text", DataSource, "SoLuongKK").FormatString = DungChung.Bien.FormatString[1];
            string __tt = TT.Value.ToString();
            if (__tt == "A")
            {
                colSoLuongTT.DataBindings.Add("Text", DataSource, "SoLuongSS").FormatString = DungChung.Bien.FormatString[0];
                colThanhTien.DataBindings.Add("Text", DataSource, "ThanhTienSS").FormatString = DungChung.Bien.FormatString[1];
                colThanhTien_T.DataBindings.Add("Text", DataSource, "ThanhTienSS").FormatString = DungChung.Bien.FormatString[1];
                colThanhTien_Gh1.DataBindings.Add("Text", DataSource, "ThanhTienSS").FormatString = DungChung.Bien.FormatString[1];
            }
          //  colSoLuongTT.DataBindings.Add("Text", DataSource, "SoLuongKK").FormatString = DungChung.Bien.FormatString[0];
            //colSoLuongTTTong.DataBindings.Add("Text", DataSource, "SoLuongKK").FormatString = DungChung.Bien.FormatString[1];
            //colHongVoGh1.DataBindings.Add("Text", DataSource, "SoLuongX").FormatString = DungChung.Bien.FormatString[1];
            if (DungChung.Bien.MaBV != "20001")
                colHongVo.DataBindings.Add("Text", DataSource, "HongVo").FormatString = DungChung.Bien.FormatString[0];
            //colHongVoTong.DataBindings.Add("Text", DataSource, "HongVo").FormatString = DungChung.Bien.FormatString[0];
            //colHongVoGh1.DataBindings.Add("Text", DataSource, "HongVo").FormatString = DungChung.Bien.FormatString[1];
            colGhiChu.DataBindings.Add("Text", DataSource, "GhiChu");
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

                xrLabel44.Visible = true;
                xrLabel51.Visible = true;
                xrLabel50.Visible = true;
                xrLabel43.Visible = true;
                xrLabel41.Visible = true;
                xrLabel42.Visible = true;
                xrLabel40.Visible = true;
                xrLabel52.Visible = true;
                xrLabel53.Visible = true;
                xrLabel49.Visible = true;
                xrLabel48.Visible = true;
                xrLabel46.Visible = true;
                xrLabel47.Visible = true;
                xrLabel45.Visible = true;
            }
            else
            {
                SubBand1.Visible = true;
            }
        }

       
    }
}
