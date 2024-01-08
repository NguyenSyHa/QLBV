using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
using System.Data;
using System.Collections.Generic;
using System.Text;
namespace QLBV.BaoCao
{
    public partial class repDTNoiTruHangNgay_HL01a_Moi : DevExpress.XtraReports.UI.XtraReport
    {
        public repDTNoiTruHangNgay_HL01a_Moi()
        {
            InitializeComponent();
        }
        DateTime[] _ngay;
        int _trang = 2;
        public repDTNoiTruHangNgay_HL01a_Moi(DateTime[] ngay,int trang)
        {
            InitializeComponent();
            _ngay = ngay;
            _trang = trang;
        }
        public void BindingData()
        {
            
            colTenNhomDVGh1.DataBindings.Add("Text", DataSource, "TenNhomDV");
            colTenDV.DataBindings.Add("Text", DataSource, "tendv");
            colDonVi.DataBindings.Add("Text", DataSource, "DonVi");
            colSoLuong.DataBindings.Add("Text", DataSource, "SL");//.FormatString = DungChung.Bien.FormatString[1]; 
            colDonGia.DataBindings.Add("Text", DataSource, "DonGia");//.FormatString = DungChung.Bien.FormatString[1];
            colThanhTien.DataBindings.Add("Text", DataSource, "TT");//.FormatString = DungChung.Bien.FormatString[1];
            colThanhTienTong.DataBindings.Add("Text", DataSource, "TT");//.FormatString = DungChung.Bien.FormatString[1]; 
        
            XRTableCell cell = new XRTableCell();
            int i = _trang;
            foreach (XRTableCell c in xrTableRow2) {
                if (c.Index == i)
                {
                    string SL = "SL"+(i-1);
                    c.DataBindings.Add("Text", DataSource, SL);
                    i++;
                    if (i == 33)
                        break;
                }
                 
            }
            GroupHeader1.GroupFields.Add(new GroupField("IDNHOM"));
        }
        
   
        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            colCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            colCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();

            if (DungChung.Bien.MaBV == "30003")
            {
                Row1.Visible = true;
                Row2.Visible = false;
                tb_KyNhan.Visible = true;
                xrLine2.Visible = true;
            }
            else 
            {
                Row1.Visible = false;
                Row2.Visible = true;
                tb_KyNhan.Visible = false;
                xrLine2.Visible = false;
            }
            if (DungChung.Bien.MaBV == "24272")
            {
                xrPictureBox1.Image = DungChung.Ham.GetLogo();
            }
        }
        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaTinh == "04") {
                xrTableCell24.Text = "Giám định BHYT";
                xrTable5.Visible = false;
            }
            if (DungChung.Bien.MaBV == "04006") {
                xrTable5.Visible = false;
            }
        }

        private void PageHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            int i = _trang;
            foreach (XRTableCell c in xrTableRow7)
            {
            
                if (c.Index == i)
                {
                    if (_ngay[i - 2] !=Convert.ToDateTime("01/01/0001"))
                    c.Text = _ngay[i - 2].ToString("dd/MM");
                    i++;
                }

            }
        }

    }
}
