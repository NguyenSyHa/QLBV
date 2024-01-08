using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class repBbKKThuoc14017 : DevExpress.XtraReports.UI.XtraReport
    {
        private bool HTNuocSX;
        bool _Thang = true;
        public repBbKKThuoc14017()
        {
            InitializeComponent();
           
        }

        public repBbKKThuoc14017(bool HTNuocSX, bool Thang)
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
            colSoKiemSoat.DataBindings.Add("Text", DataSource, "SoLo");
            colNuocSX.DataBindings.Add("Text", DataSource, "NuocSX");
            colHanDung.DataBindings.Add("Text", DataSource, "HanDung");//.FormatString="{0:dd/MM/yyyy}";
            colSoLuongSS.DataBindings.Add("Text", DataSource, "SoLuongSS").FormatString = DungChung.Bien.FormatString[0];
            colSoLuongTT.DataBindings.Add("Text", DataSource, "SoLuongKK").FormatString = DungChung.Bien.FormatString[0];
            //colSoLuongTTTong.DataBindings.Add("Text", DataSource, "SoLuongKK").FormatString = DungChung.Bien.FormatString[1];
            //colHongVoGh1.DataBindings.Add("Text", DataSource, "HongVo").FormatString = DungChung.Bien.FormatString[1];
            colHongVo.DataBindings.Add("Text", DataSource, "HongVo").FormatString = DungChung.Bien.FormatString[0];   // 14017 ko lấy sl hỏng vỡ trong DB 
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

        private void repBbKKThuoc14017_BeforePrint(object sender, CancelEventArgs e)
        {
            TenCQCQ.Value = DungChung.Bien.TenCQCQ;
            TenCQ.Value = DungChung.Bien.TenCQ;
            string _nt = NT.Value.ToString();
            string _ct = NT.Value.ToString();
            GroupHeader3.Visible = false;
            GroupHeader2.Visible = false;
            
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            colTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            colTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            //xrRichText1.Text = TV1goi.Value.ToString();
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
            
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            if(DungChung.Bien.MaBV == "27001") 
            {
                xrLabel34.Visible = true;
                xrLabel33.Visible = true;
                xrLabel32.Visible = true;
                xrLabel10.Visible = true;
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

        private void PageFooter_BeforePrint(object sender, CancelEventArgs e)
        {

        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {

        }

        private void colHongVo_BeforePrint(object sender, CancelEventArgs e)
        {
            if (colHanDung.Text != "")
            {
                int rs = DateTime.Compare(DungChung.Ham.NgayTu(Convert.ToDateTime(colHanDung.Text)), DungChung.Ham.NgayTu(DateTime.Now));
                if (rs <= 0)
                    colHongVo.Text = rs < 0 ? colSoLuongSS.Text : "";
            }
            else
            {
                colHongVo.Text = colSoLuongSS.Text;
            }
            if (colHongVo.Text == "0")
                colHongVo.Text = "";
        }

        private void colSoLuongTT_BeforePrint(object sender, CancelEventArgs e)
        {
            if (colHanDung.Text != "")
            {
                int rs = DateTime.Compare(DungChung.Ham.NgayTu(Convert.ToDateTime(colHanDung.Text)), DungChung.Ham.NgayTu(DateTime.Now));
                colSoLuongTT.Text = rs < 0 ? "" : colSoLuongSS.Text;
            }
            else
                colSoLuongTT.Text = "";
        }
    }
}
