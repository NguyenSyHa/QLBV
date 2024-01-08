using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class repGiayCVNgoaitru : DevExpress.XtraReports.UI.XtraReport
    {
        public repGiayCVNgoaitru()
        {
            InitializeComponent();
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            
           
            if (Gioitinh.Value.ToString() == "1")
            {
                txtGT.Text = "Nam";
            }
            else
            {
                txtGT.Text = "Nữ";
            }
            if (Gioitinh.Value.ToString() == "1")
            {
                txtGT1.Text = "Nam";
            }
            else
            {
                txtGT1.Text = "Nữ";
            }
            if (Ngoaikieu.Value.ToString() == "0")
            {
                txtngoaikieu.Text = "";
            }
            else
            {
                txtngoaikieu.Text = "Việt kiều";
            }
            if (Ngoaikieu.Value.ToString() == "0")
            {
                txtNgoaikieu1.Text = "";
            }
            else
            {
                txtNgoaikieu1.Text = "Việt kiều";
            }
            txtTenCQ.Text = DungChung.Bien.TenCQ + " giới thiệu ";
            txtgiamdoc.Text = DungChung.Bien.GiamDoc;
        }


        public string TenCB { get; set; }
    }
}
