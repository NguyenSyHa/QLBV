using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_PhieuPTTT_27006 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_PhieuPTTT_27006()
        {
            InitializeComponent();
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            colTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            colTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            String[] arrDuongDan = QLBV_Library.QLBV_Ham.LayChuoi('|', HinhAnh.Value.ToString());
            for (int i = 0; i < arrDuongDan.Length; i++)
            {
                switch (i)
                {
                    case 0:
                        this.xrPictureBox1.ImageUrl = arrDuongDan[0];
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
