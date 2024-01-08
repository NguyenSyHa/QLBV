using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
namespace QLBV.BaoCao
{
    public partial class repThuChi_C30_C31 : DevExpress.XtraReports.UI.XtraReport
    {
        public repThuChi_C30_C31()
        {
            InitializeComponent();
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            txtTenCQ.Text = DungChung.Bien.TenCQ;
            txtDiaChi.Text = DungChung.Bien.DiaChi;
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var a=_data.CanBoes.Where(p=>p.MaCB== (DungChung.Bien.MaCB)).ToList();
            if (a.Count > 0)
                colNguoiThu.Text = a.First().TenCB;
        }

    }
}
