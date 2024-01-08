using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class Rep_GiayHenKham_TKy : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_GiayHenKham_TKy()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities _Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            colTenCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            colTenKP.Text = DungChung.Bien.TenCQ.ToUpper();
            


            if (DungChung.Bien.MaBV == "30003")
            {
                QLBV.Visible = false;
                sub30003.Visible = true;
            }
            else if (DungChung.Bien.MaBV == "30007")
            {
                QLBV.Visible = false;
                sub30003.Visible = false;
                SubBand30007.Visible = true;
            }
            if (DungChung.Bien.MaBV == "30009")
            {
                QLBV.Visible = false;
                SubBand1.Visible = true;
                
            }
            //if (DungChung.Bien.MaBV == "30007")
            //{
            //    celThoiGianhenkham2.Visible = true;
            //    SubBand1.Visible = false;
            //    SubBand2.Visible = true;
            //    xrLabel14.Visible = true;
            //}
            //if (DungChung.Bien.MaBV == "30009")
            //    colTenBS.Visible = false;
            //if (DungChung.Bien.MaBV == "30003")
            //    celSo.Visible = false;
            //if (DungChung.Bien.MaBV == "30003")
            //    celThoigianHenKham.Text = "Giấy hẹn khám lại chỉ có giá trị sử dụng 01 (một) lần.";
            //else
            //    celThoigianHenKham.Text = "Giấy hẹn khám lại chỉ có giá trị sử dụng 01 (một) lần trong thời hạn 10 ngày làm việc, kể từ ngày được hẹn khám lại.";
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "30003")
            {
                SubBand3.Visible = true;
                SubBand4.Visible = false;
            }
            else if (DungChung.Bien.MaBV == "30009")
            {
                SubBand2.Visible = true;
                SubBand4.Visible = false;
                
            }
            else
            {
                SubBand4.Visible = true;
            }

        }

    }
}
