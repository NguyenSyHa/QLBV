using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.CLS.Phieu
{
    public partial class rep_PhieuXNGiaiPhauSinhThiet : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_PhieuXNGiaiPhauSinhThiet()
        {
            InitializeComponent();
            if (DungChung.Bien.MaBV != "12345"&& DungChung.Bien.MaBV != "24297")
            {
                xrTableCell20.Visible = false;
                celNamSinh.Visible = false;
            }
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            //if (DungChung.Bien.MaBV == "27023")
                //xrTableCell35.Text = ("Bác sĩ điều trị").ToUpper();
            if (DungChung.Bien.MaBV == "26007")
                xrBarCode1.Visible = true;
            if (DungChung.Bien.MaBV == "27023")
            {
                xrTable5.Visible = false;
                xrTable7.Visible = true;
                xrTableCell16.Text = "";
                celBSDocKQ.Text = "";
                xrTableCell35.Text = "BÁC SĨ ĐỌC KẾT QUẢ";

            }
            else
            {
                xrTable7.Visible = false;
                xrTable5.Visible = true;
                
            }
        }

    }
}
