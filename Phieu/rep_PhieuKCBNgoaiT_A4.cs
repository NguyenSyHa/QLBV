using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
using System.Collections.Generic;
namespace QLBV.BaoCao
{
    public partial class rep_PhieuKCBNgoaiT_A4 : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_PhieuKCBNgoaiT_A4()
        {
            InitializeComponent();
        }
        List<ChiDinh> _lcd = new List<ChiDinh>();
        public rep_PhieuKCBNgoaiT_A4(List<ChiDinh> _l)
        {
            InitializeComponent();
            _lcd = _l;
        }
        //QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            
        }
        public void BindData() {
            txtTenDV.DataBindings.Add("Text", DataSource, "TenDV");
            colDonVi.DataBindings.Add("Text", DataSource, "DonVi");
            colSoLuong.DataBindings.Add("Text", DataSource, "SoLuong").FormatString = DungChung.Bien.FormatString[0];
            colDonGia.DataBindings.Add("Text", DataSource, "DonGia").FormatString=DungChung.Bien.FormatString[1];
            colThanhTien.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];
            colGFThanhTien.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];
            colRFThanhTien.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];
            txtIDCD.DataBindings.Add("Text", DataSource, "IDCD");
            colGHTenNhom.DataBindings.Add("Text", DataSource, "TenNhom");
            GroupHeader1.GroupFields.Add(new GroupField("STT"));
        }
        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            
            string sthe = SThe.Value.ToString();

            if (sthe.Length == 15)
            {
                txtThe1.Text = sthe.Substring(0, 2);
                txtThe2.Text = sthe.Substring(2, 1);
                txtThe3.Text = sthe.Substring(3, 2);
                txtThe4.Text = sthe.Substring(5, 2);
                txtThe5.Text = sthe.Substring(7, 3);
                txtThe6.Text = sthe.Substring(10, 5);
            }
            else
            {
                txtThe1.Visible = false;
                txtThe2.Visible = false;
                txtThe3.Visible = false;
                txtThe4.Visible = false;
                txtThe5.Visible = false;
                txtThe6.Visible = false;
                lblMaCS.Visible = false;
                lblsudung.Visible = false;
            }
            
            txtTenCQ.Text = DungChung.Bien.TenCQ;
            
            if (this.GTinh.Value.ToString() == "Nam")
            {
                txtNam.Visible = false;
            }
            else
                txtNu.Visible = false;

        }

        private void xrTableCell12_BeforePrint(object sender, CancelEventArgs e)
        {

        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "30004")
                xrTenBS.Visible = false;
            //txtbnky.Visible = false;
            //txttenBN.Visible = false;
            if (DungChung.Bien.MaBV == "30003") {
                txttenBN.Text = "Giám định viên";
                xrTableCell18.Text = "Bệnh nhân";
            }
            //if (DungChung.Bien.MaBV == "04011") {
            //    colNguoiPhat_kt.Text = "";
            //    colNguoiPhat_td.Text = "Kế toán viện phí";
            //}
            //if (DungChung.Bien.MaBV == "30009") {
            //    colNguoiPhat_kt.Text = DungChung.Bien.ThuKho;
            //}
        }
        int sttgroup2 = 1;
        private void GroupHeader1_BeforePrint(object sender, CancelEventArgs e)
        {
            switch (sttgroup2)
            {
                case 1:
                    colSTTGH.Text = "I-";
                    colCongGF.Text = "Cộng I:";
                    break;
                case 2:
                    colSTTGH.Text = "II-";
                    colCongGF.Text = "Cộng II:";
                    break;
                case 3:
                    colSTTGH.Text = "III-";
                    colCongGF.Text = "Cộng III:";
                    break;
                case 4:
                    colSTTGH.Text = "IV-";
                    colCongGF.Text = "Cộng IV:";
                    break;
                case 5:
                    colSTTGH.Text = "V-";
                    colCongGF.Text = "Cộng V:";
                    break;
            }
            sttgroup2++;
        }

        private void colSTTGH_BeforePrint(object sender, CancelEventArgs e)
        {
             
        }

        private void TenDV_BeforePrint(object sender, CancelEventArgs e)
        {
            if (this.GetCurrentColumnValue("IDCD") != null && (DungChung.Bien.MaBV == "30009" || DungChung.Bien.MaBV == "30003"))
            {
                int id = Convert.ToInt32(this.GetCurrentColumnValue("IDCD"));
                if (_lcd.Where(p => p.IDCD == id).ToList().Count > 0 )
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
                if (this.GetCurrentColumnValue("TenDV")!=null)
                TenDV.Text = this.GetCurrentColumnValue("TenDV").ToString();
            }
        }

    }
}
