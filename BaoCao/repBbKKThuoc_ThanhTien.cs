using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class repBbKKThuoc_ThanhTien : DevExpress.XtraReports.UI.XtraReport
    {
        bool _Thang = true;
        public repBbKKThuoc_ThanhTien(bool Thang)
        {
            InitializeComponent();
            _Thang = Thang;
        }
        
        public void BindingData()
        {
            //txtNT.DataBindings.Add("Text", DataSource, "NgayThang");
            //txtCT.DataBindings.Add("Text", DataSource, "SoCT");
            colTenThuocGh1.DataBindings.Add("Text", DataSource, "TenTieuNhom");            
            colTenThuoc.DataBindings.Add("Text", DataSource, "TenDV");
            colDonVi.DataBindings.Add("Text", DataSource, "DonVi");
            celSDK.DataBindings.Add("Text", DataSource, "SoDK");          
            colSoKiemSoat.DataBindings.Add("Text", DataSource, "SoLo");
            colNuocSX.DataBindings.Add("Text", DataSource, "NuocSX");
            colHanDung.DataBindings.Add("Text", DataSource, "HanDung");//.FormatString="{0:dd/MM/yyyy}";
            colSoLuongSS.DataBindings.Add("Text", DataSource, "SoLuongSS").FormatString = DungChung.Bien.FormatString[0];
            colGia.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[1];
          //  colSoLuongSSTong.DataBindings.Add("Text", DataSource, "SoLuongSS").FormatString = DungChung.Bien.FormatString[0];
            colTTSS.DataBindings.Add("Text", DataSource, "ThanhTienSS").FormatString = DungChung.Bien.FormatString[1];
            colTTSSgh.DataBindings.Add("Text", DataSource, "ThanhTienSS");
            colTTSSgh.Summary.FormatString = DungChung.Bien.FormatString[1];
            colTTSStc.DataBindings.Add("Text", DataSource, "ThanhTienSS");
            colTTSStc.Summary.FormatString = DungChung.Bien.FormatString[1];
            string __tt = TT.Value.ToString();
       
            if (__tt == "A")
            {
                colSoLuongTT.DataBindings.Add("Text", DataSource, "SoLuongSS").FormatString = DungChung.Bien.FormatString[0];
                colTTTT.DataBindings.Add("Text", DataSource, "ThanhTienSS").FormatString = DungChung.Bien.FormatString[1];
                colTTTTgh.DataBindings.Add("Text", DataSource, "ThanhTienSS");
                colTTTTgh.Summary.FormatString = DungChung.Bien.FormatString[1];
                colTTTTtc.DataBindings.Add("Text", DataSource, "ThanhTienSS");
                colTTTTtc.Summary.FormatString = DungChung.Bien.FormatString[1];
     
            }
            //colSoLuongTTGh1.DataBindings.Add("Text", DataSource, "SoLuongKK").FormatString = DungChung.Bien.FormatString[1];
            //colSoLuongTT.DataBindings.Add("Text", DataSource, "SoLuongKK").FormatString = DungChung.Bien.FormatString[1];
            //colSoLuongTTTong.DataBindings.Add("Text", DataSource, "SoLuongKK").FormatString = DungChung.Bien.FormatString[1];
            //colHongVoGh1.DataBindings.Add("Text", DataSource, "HongVo").FormatString = DungChung.Bien.FormatString[1];
            colHongVo.DataBindings.Add("Text", DataSource, "HongVo").FormatString = DungChung.Bien.FormatString[1];
            if (DungChung.Bien.MaBV != "20001")
                colHongVoTong.DataBindings.Add("Text", DataSource, "HongVo").FormatString = DungChung.Bien.FormatString[1];
            
            colGhiChu.DataBindings.Add("Text", DataSource, "GhiChu");
            GroupHeader1.GroupFields.Add(new GroupField("TenTieuNhom"));
            //GroupHeader2.GroupFields.Add(new GroupField("SoCT"));
            //GroupHeader3.GroupFields.Add(new GroupField("NgayThang"));

           

        }

        private void repBbKKThuoc_BeforePrint(object sender, CancelEventArgs e)
        {
            TenCQCQ.Value = DungChung.Bien.TenCQCQ;
            TenCQ.Value = DungChung.Bien.TenCQ;
            string _nt= NT.Value.ToString();
            string _ct = NT.Value.ToString();
            xrLine2.Visible = DungChung.Bien.MaBV == "20001" ? true : false;
            //if (_nt == "1") { GroupHeader3.Visible = false; }
            //else GroupHeader3.Visible = true;
            //if (_ct == "1") { GroupHeader2.Visible = false; }
            //else GroupHeader2.Visible = true;
            if (DungChung.Bien.MaBV == "20001")
            {
                colThanhVienKK1r.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                colThanhVienKK2r.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                colThanhVienKK3r.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                colThanhVienKK4r.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                colThanhVienKK5r.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                xrLabel11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            }
           
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
            //xrRichText1.Text = TV1goi.Value.ToString();
        }

        private void GroupHeader3_BeforePrint(object sender, CancelEventArgs e)
        {
            if (this.GetCurrentColumnValue("NgayThang") != null)
            {
                string nt = this.GetCurrentColumnValue("NgayThang").ToString();
                //colNgayThang.Text = "Ngày " + nt.ToString().Substring(0, 2) + " tháng " + nt.ToString().Substring(3, 2) + " năm " + nt.ToString().Substring(6, 4);
            }
        }

        private void GroupHeader2_BeforePrint(object sender, CancelEventArgs e)
        {
            if (this.GetCurrentColumnValue("SoCT") != null)
            {
                string ct = this.GetCurrentColumnValue("SoCT").ToString();
                //colSoCT.Text = "Số chứng từ: " + ct;
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

                xrLabel69.Visible = true;
                xrLabel51.Visible = true;
                xrLabel50.Visible = true;
                xrLabel47.Visible = true;
                xrLabel32.Visible = true;
                xrLabel16.Visible = true;
                xrLabel33.Visible = true;
                xrLabel15.Visible = true;
                xrLabel14.Visible = true;
                xrLabel13.Visible = true;
                xrLabel12.Visible = true;
                xrLabel36.Visible = true;
                xrLabel35.Visible = true;
                xrLabel34.Visible = true;
            }
            else
            {
                SubBand1.Visible = true;
            }
        }

       
    }
}
