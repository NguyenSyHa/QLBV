using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
using System.Collections.Generic;

namespace QLBV.BaoCao
{
    public partial class repDonThuoc_01071 : DevExpress.XtraReports.UI.XtraReport
    {
        private string nhomduoc;

        public repDonThuoc_01071()
        {
            InitializeComponent();
        }
         List<ChiDinh> _lcd = new List<ChiDinh>();
         public repDonThuoc_01071(List<ChiDinh> _l)
        {
            InitializeComponent();
            _lcd = _l;
        }
       
        //QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
        }
        public void BindData()
        {
            txtTenDV.DataBindings.Add("Text", DataSource, "TenDV");
            colDonVi.DataBindings.Add("Text", DataSource, "DonVi");
            colSoLuong.DataBindings.Add("Text", DataSource, "SoLuong").FormatString = DungChung.Bien.FormatString[0];               
            txtIDCD.DataBindings.Add("Text", DataSource, "IDCD");
            colGHTenNhom.DataBindings.Add("Text", DataSource, "TenNhom");
            GroupHeader1.GroupFields.Add(new GroupField("STT"));  

        }
        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            txtTenCQ.Text = DungChung.Bien.TenCQ;
            if (DungChung.Bien.MaBV == "30002")
            {
                txttenBN.Text = "Chữ ký của bệnh nhân";
            }
            if (DungChung.Bien.MaBV == "24009")
            {
                txtMaICD.Visible = true;
                txtICD.Visible = true;
            }
            if (DungChung.Bien.MaBV == "30009")
            {
                txtMaICD.Visible = true;
                txtICD.Visible = true;
                colHoTenBoMe.Visible = true;
                colLoiDanBS.Visible = true;
                xrTableCell10.Visible = true;
                colHoTenBoMe.Visible = true;
                colLoiDanBS.Visible = true;
            }
            if (DungChung.Bien.MaBV == "01830" )
                lab_TenKP.Visible = true;
            if(DungChung.Bien.MaBV == "27001")
                xrLabel7.Visible = true;
            if(DungChung.Bien.MaBV=="30007")
            {
                if(SThe.Value !=null && SThe.Value.ToString().Length>=15)
                    txtTieuDe.Text="ĐƠN THUỐC BHYT";
            }
            //if(nhomduoc == "Thuốc đông y" && DungChung.Bien.MaBV == "20001")
            //{
            //    xrLabel3.ForeColor = Color.ForestGreen;
            //    xrLabel5.ForeColor = Color.ForestGreen;
            //}

        }

        private void xrTableCell12_BeforePrint(object sender, CancelEventArgs e)
        {

        }
        int sttgroup2 = 1;
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
                colHoTenBoMe.Visible = false;
                colLoiDanBS.Visible = false;
                xrTableCell10.Visible = false;
                colHoTenBoMe.Visible = false;
                colLoiDanBS.Visible = false;

            }
            if(DungChung.Bien.MaBV.Substring(0,2)=="30")
            xrTableCell5.Text = "Bác sỹ khám bệnh";
        }

        private void SoLuong_BeforePrint(object sender, CancelEventArgs e)
        {

        }

        private void GroupHeader1_BeforePrint(object sender, CancelEventArgs e)
        {
            switch (sttgroup2)
            {
                case 1:
                    colSTTGH.Text = "I-";
                  
                    break;
                case 2:
                    colSTTGH.Text = "II-";
                   
                    break;
                case 3:
                    colSTTGH.Text = "III-";
                  
                    break;
                case 4:
                    colSTTGH.Text = "IV-";
                   
                    break;
                case 5:
                    colSTTGH.Text = "V-";
                    
                    break;
            }
            sttgroup2++;
        }

        private void TenDV_BeforePrint(object sender, CancelEventArgs e)
        {
            if (this.GetCurrentColumnValue("IDCD") != null && (DungChung.Bien.MaBV == "30009" || DungChung.Bien.MaBV == "30003"))
            {
                int id = Convert.ToInt32(this.GetCurrentColumnValue("IDCD"));
                if (_lcd.Where(p => p.IDCD == id).ToList().Count > 0)
                {
                    TenDV.Text = this.GetCurrentColumnValue("TenDV").ToString() + "(" + _lcd.Where(p => p.IDCD == id).ToList().First().ChiDinh1 + ")";
                }
                else
                {
                    if (this.GetCurrentColumnValue("TenDV") != null)
                        TenDV.Text = this.GetCurrentColumnValue("TenDV").ToString();
                }
            }
            else
            {
                if (this.GetCurrentColumnValue("TenDV") != null)
                    TenDV.Text = this.GetCurrentColumnValue("TenDV").ToString();
            }
        }

    }
}
