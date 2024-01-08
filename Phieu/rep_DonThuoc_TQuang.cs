using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class rep_DonThuoc_TQuang : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_DonThuoc_TQuang()
        {
            InitializeComponent();
        }
        //QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            if (GetCurrentColumnValue("HuongDan") != null && GetCurrentColumnValue("HuongDan").ToString() != "")
            {
                rowhuongdan.Visible = true;
            }
            else {
                rowhuongdan.Visible = false;
            }
        }
        public void BindData() {
            TenDV.DataBindings.Add("Text", DataSource, "TenDV");
            colDonVi.DataBindings.Add("Text", DataSource, "DonVi");
            colSoLuong.DataBindings.Add("Text", DataSource, "SoLuong");
            HuongDan.DataBindings.Add("Text", DataSource, "HuongDan");
            //colDonGia.DataBindings.Add("Text", DataSource, "DonGia").FormatString=DungChung.Bien.FormatString[1];
            //colThanhTien.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];
            //colGFThanhTien.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];
            //colRFThanhTien.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];
            colGHTenNhom.DataBindings.Add("Text", DataSource, "PLDV");
            GroupHeader1.GroupFields.Add(new GroupField("PLDV"));
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
            txtTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            
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

            //txtbnky.Visible = false;
            //txttenBN.Visible = false;
            
        }
        
        private void GroupHeader1_BeforePrint(object sender, CancelEventArgs e)
        {
            int PLDV = 0;
            if (GetCurrentColumnValue("PLDV") != null && GetCurrentColumnValue("PLDV").ToString() != "")
                PLDV = Convert.ToInt32(GetCurrentColumnValue("PLDV"));
            switch (PLDV)
            {
                case 1:
                    colSTTGH.Text = "Chỉ định dùng thuốc:";
                    break;
                case 2:
                    colSTTGH.Text = "Dịch vụ kỹ thuật(nếu có):";
                    break;
            }
        }

        private void colSTTGH_BeforePrint(object sender, CancelEventArgs e)
        {
             
        }

    }
}
