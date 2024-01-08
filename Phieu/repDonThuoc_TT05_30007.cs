using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class repDonThuoc_TT05_30007 : DevExpress.XtraReports.UI.XtraReport
    {
        private string nhomduoc;

        public repDonThuoc_TT05_30007()
        {
            InitializeComponent();
        }

       
        //QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
        }
        public void BindData()
        {
            TenDV.DataBindings.Add("Text", DataSource, "TenDV");
            SoLuong.DataBindings.Add("Text", DataSource, "SoLuong");
            DonVi.DataBindings.Add("Text", DataSource, "DonVi");
            HuongDan.DataBindings.Add("Text", DataSource, "HuongDan");

        }
        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            txtsodt.Text = DungChung.Bien.SDTCQ;
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var _macqcq = data.BenhViens.Where(p => p.MaBV == DungChung.Bien.MaBV).FirstOrDefault();
            txtTenCQ.Text = DungChung.Bien.MaBV == "27183" ? ("Công ty cổ phần bệnh viện Hà Nội Bắc Ninh" + "\n" + DungChung.Bien.TenCQ) : DungChung.Bien.TenCQ;
            if (DungChung.Bien.MaBV == "30002")
            {
                txttenBN.Text = "Chữ ký của bệnh nhân";
            }
            if (DungChung.Bien.MaBV == "24009" || (_macqcq != null && _macqcq.MaChuQuan == "24009"))
            {
                txtMaICD.Visible = true;
                txtICD.Visible = true;
            }
            if (DungChung.Bien.MaBV == "30009")
            {
                txtMaICD.Visible = true;
                txtICD.Visible = true;
                //colLoiDanBS.Visible = true;
                xrTableCell10.Visible = true;
                //colLoiDanBS.Visible = true;
            }
            if (DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "56789")
                xrTableCell5.Text = "Bác sỹ khám bệnh";
            //if(DungChung.Bien.MaBV=="30007")
            //{
            //    if(SThe.Value !=null && SThe.Value.ToString().Length>=15)
            //        txtTieuDe.Text="ĐƠN THUỐC BHYT";
            //}
            //if(nhomduoc == "Thuốc đông y" && DungChung.Bien.MaBV == "20001")
            //{
            //    xrLabel3.ForeColor = Color.ForestGreen;
            //    xrLabel5.ForeColor = Color.ForestGreen;
            //}
            if(DungChung.Bien.MaBV=="27022")
            {
                txtTieuDe.Text = "ĐƠN TƯ VẤN";
                xrLabel6.Text = "Điều trị:";
            }

        }

        private void xrTableCell12_BeforePrint(object sender, CancelEventArgs e)
        {

        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "24009")
            {
                txtbnky.Visible = false;
                txttenBN.Visible = false;
            }
            if (DungChung.Bien.MaBV == "30002" || DungChung.Bien.MaBV == "30007" || DungChung.Bien.MaBV == "26007")
            {
                xrThuKho.Visible = true;
                xrThuKhoky.Visible = true;
                xrThuKhoNguoi.Visible = true;
            }
            if (DungChung.Bien.MaBV == "30004")
                xrTenBS.Visible = false;
            if (DungChung.Bien.MaBV == "30009")
            {
                txtbnky.Visible = false;
                txttenBN.Visible = false;
                //colLoiDanBS.Visible = false;
                xrTableCell10.Visible = false;
                //colLoiDanBS.Visible = false;

            }
            //if(DungChung.Bien.MaBV=="01830")
            //{
            //    xrTableCell8.Visible = true;
            //    xrTableCell10.Visible=true;
            //}
            if (DungChung.Bien.MaBV.Substring(0, 2) == "30" && DungChung.Bien.MaBV != "30007")
                xrTableCell5.Text = "Bác sỹ khám bệnh";
            
            //if (DungChung.Bien.MaBV == "04011")
            //{
            //    txttenBN.Text = "Họ tên bệnh nhân";
            //    txtbnky.Text = "(Ký, ghi rõ họ tên)";
            //}

            if (DungChung.Bien.MaBV == "20001")
            {
                SubBand2.Visible = true;
                SubBand1.Visible = false;
                xrLabel5.Visible = false;
            }
            else
            {
                SubBand2.Visible = false;
                SubBand1.Visible = true;
                xrLabel5.Visible = true;
            }
        }

        private void SoLuong_BeforePrint(object sender, CancelEventArgs e)
        {

            if (this.GetCurrentColumnValue("SoLuong") != null)
            {
                int ot;
                if (Int32.TryParse(this.GetCurrentColumnValue("SoLuong").ToString(), out ot))
                {
                    int sl = Convert.ToInt32(this.GetCurrentColumnValue("SoLuong").ToString());
                    if (sl < 10)
                        SoLuong.Text = sl.ToString("D2");
                    else
                        SoLuong.Text = sl.ToString();
                    
                }
            }
        }

    }
}
