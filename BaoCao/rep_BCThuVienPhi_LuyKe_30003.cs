using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_BCThuVienPhi_LuyKe_30003 : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_BCThuVienPhi_LuyKe_30003()
        {
            InitializeComponent();
        }

        public rep_BCThuVienPhi_LuyKe_30003(bool p)
        {
            InitializeComponent();
            this.hienthi = p;
        }

        string fomat = "{0:#,#}";
        private bool hienthi;
        internal void BindingData()
        {
            celTenKhoact.DataBindings.Add("Text", DataSource, "TenKPhongCt");
            cel1.DataBindings.Add("Text", DataSource, "Luot").FormatString = fomat;
            cel2.DataBindings.Add("Text", DataSource, "Mau").FormatString = fomat;
            cel4.DataBindings.Add("Text", DataSource, "ThuocDY").FormatString = fomat;
            cel5.DataBindings.Add("Text", DataSource, "ThuocDich").FormatString = fomat;
            cel6.DataBindings.Add("Text", DataSource, "VTYT").FormatString = fomat;
            cel7.DataBindings.Add("Text", DataSource, "TienGiuong").FormatString = fomat;
            //cel7.DataBindings.Add("Text", DataSource, "KhamBenh").FormatString = fomat;
            cel8.DataBindings.Add("Text", DataSource, "XetNghiem").FormatString = fomat;
            cel9.DataBindings.Add("Text", DataSource, "KSK").FormatString = fomat;
            cel10.DataBindings.Add("Text", DataSource, "SA").FormatString = fomat;
            cel11.DataBindings.Add("Text", DataSource, "DienTim").FormatString = fomat;
            cel12.DataBindings.Add("Text", DataSource, "TTPT").FormatString = fomat;
            cel13.DataBindings.Add("Text", DataSource, "Khac").FormatString = fomat;
            cel14.DataBindings.Add("Text", DataSource, "Tong").FormatString = fomat;

            celTenKhoa.DataBindings.Add("Text", DataSource, "TenKhoa");
            celG1.DataBindings.Add("Text", DataSource, "Luot").FormatString = fomat;
            celG2.DataBindings.Add("Text", DataSource, "Mau").FormatString = fomat;
            celG4.DataBindings.Add("Text", DataSource, "ThuocDY").FormatString = fomat;
            celG5.DataBindings.Add("Text", DataSource, "ThuocDich").FormatString = fomat;
            celG6.DataBindings.Add("Text", DataSource, "VTYT").FormatString = fomat;
            celG7.DataBindings.Add("Text", DataSource, "TienGiuong").FormatString = fomat;
           // celG7.DataBindings.Add("Text", DataSource, "KhamBenh").FormatString = fomat;
            celG8.DataBindings.Add("Text", DataSource, "XetNghiem").FormatString = fomat;
            celG9.DataBindings.Add("Text", DataSource, "KSK").FormatString = fomat;
            celG10.DataBindings.Add("Text", DataSource, "SA").FormatString = fomat;
            celG11.DataBindings.Add("Text", DataSource, "DienTim").FormatString = fomat;
            celG12.DataBindings.Add("Text", DataSource, "TTPT").FormatString = fomat;
            celG13.DataBindings.Add("Text", DataSource, "Khac").FormatString = fomat;
            celG14.DataBindings.Add("Text", DataSource, "Tong").FormatString = fomat;

            celT1.DataBindings.Add("Text", DataSource, "Luot").FormatString = fomat;
            celT2.DataBindings.Add("Text", DataSource, "Mau").FormatString = fomat;
            celT4.DataBindings.Add("Text", DataSource, "ThuocDY").FormatString = fomat;
            celT5.DataBindings.Add("Text", DataSource, "ThuocDich").FormatString = fomat;
            celT6.DataBindings.Add("Text", DataSource, "VTYT").FormatString = fomat;
            celT7.DataBindings.Add("Text", DataSource, "TienGiuong").FormatString = fomat;
           // celT7.DataBindings.Add("Text", DataSource, "KhamBenh").FormatString = fomat;
            celT8.DataBindings.Add("Text", DataSource, "XetNghiem").FormatString = fomat;
            celT9.DataBindings.Add("Text", DataSource, "KSK").FormatString = fomat;
            celT10.DataBindings.Add("Text", DataSource, "SA").FormatString = fomat;
            celT11.DataBindings.Add("Text", DataSource, "DienTim").FormatString = fomat;
            celT12.DataBindings.Add("Text", DataSource, "TTPT").FormatString = fomat;
            celT13.DataBindings.Add("Text", DataSource, "Khac").FormatString = fomat;
            celT14.DataBindings.Add("Text", DataSource, "Tong").FormatString = fomat;

            GroupHeader1.GroupFields.Add(new GroupField("TenKhoa"));


        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            lab_TenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            lab_TenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
        }

        private void ReportFooter_AfterPrint(object sender, EventArgs e)
        {

        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            lab_TenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            lab_TenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
        }

        private void xrTableRow2_BeforePrint(object sender, CancelEventArgs e)
        {
            if (hienthi)
            {
                if (this.GetCurrentColumnValue("TenKPhongCt") != null)
                {
                    string tenkpct = this.GetCurrentColumnValue("TenKPhongCt").ToString();
                    if (tenkpct == "")
                        xrTableRow2.Visible = false;
                    else
                        xrTableRow2.Visible = true;

                }
            }
            else
                xrTableRow2.Visible = false;


        }
    }
}
