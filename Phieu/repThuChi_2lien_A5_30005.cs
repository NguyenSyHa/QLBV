using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
namespace QLBV.BaoCao
{
    public partial class repThuChi_2lien_A5_30005 : DevExpress.XtraReports.UI.XtraReport
    {
        public repThuChi_2lien_A5_30005()
        {
            InitializeComponent();
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            txtTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            // txtTenCQ2.Text = DungChung.Bien.TenCQ.ToUpper();
            lblMadonvi.Text = " Mã đơn vị SĐNS: " + DungChung.Bien.MaBV;
            if (DungChung.Bien.MaBV == "30009" || DungChung.Bien.MaBV == "30303")
            { // theo TT 133
                lblMadonvi.Visible = false;
                xrLabel10.Text = "Mẫu số 01-TT \n(Ban hành theo Thông tư số 133/2016/TT-BTC\n ngày 26/08/2016 của Bộ tài chính)";
                xrLabel11.Text = DungChung.Bien.DiaChi;
            }
            else {
                xrLabel11.Text =TenKP.Value==null?"": TenKP.Value.ToString();
            }
            if (DungChung.Bien.MaBV == "12122")
            {
                Detail.Visible = false;
                DetailReport.Visible = true;
            }
            else
            {
                Detail.Visible = true;
                DetailReport.Visible = false;

            }
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var a = _data.CanBoes.Where(p => p.MaCB == (DungChung.Bien.MaCB)).ToList();
            if (a.Count > 0)
                cel_NguoiLap.Text = a.First().TenCB;
            cel_GiamDoc.Text = DungChung.Bien.GiamDoc;
            cel_KTT.Text = DungChung.Bien.KeToanTruong;


        }

    }
}
