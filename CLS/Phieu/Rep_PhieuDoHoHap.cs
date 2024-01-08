using System.ComponentModel;

namespace QLBV.BaoCao
{
    public partial class Rep_PhieuDoHoHap : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_PhieuDoHoHap()
        {
            InitializeComponent();
            if (DungChung.Bien.MaBV == "24009" || DungChung.Bien.MaBV == "27777")
            {
                SubBand2.Visible = false;
                SubBand3.Visible = true;
                xrPictureBox1.Image = DungChung.Ham.GetLogo();
                colDiaChi.Text = DungChung.Ham.GetDiaChiBV();
            }
            else
            {
                SubBand2.Visible = true;
                SubBand3.Visible = false;
            }
        }

        private QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            xrLabel27.Text = colTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            xrLabel26.Text = colTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            if (DungChung.Bien.MaBV == "27023")
            {
                xrLabel3.Visible = true;
                xrLine3.Visible = false;
            }
            if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "56789")
            {
                thuong.Visible = true;
                thuong1.Visible = true;
                capcuu.Visible = true;
                capcuu1.Visible = true;
                lab2.Visible = false;
            }
            if (DungChung.Bien.MaBV == "26007")
                xrBarCode1.Visible = true;
        }

        public class BC
        {
            public string TenDV { set; get; }
            public string KQ1 { set; get; }
            public string KQ2 { set; get; }
            public string KQ3 { set; get; }
            public string KQ4 { set; get; }
            public string KQ5 { set; get; }
        }

        public void DataBindings()
        {
            col1.DataBindings.Add("Text", DataSource, "TenDV");
            if (DungChung.Bien.MaBV == "24272")
            {
                col1.DataBindings.Add("Text", DataSource, "TenRG");
            }
            col2.DataBindings.Add("Text", DataSource, "KQ1");
            col3.DataBindings.Add("Text", DataSource, "KQ2");
            col4.DataBindings.Add("Text", DataSource, "KQ3");
            col5.DataBindings.Add("Text", DataSource, "KQ4");
            col6.DataBindings.Add("Text", DataSource, "KQ5");
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            //if (DungChung.Bien.MaBV == "30009")
            //{
            //    lab89.Visible = false;
            //    lab90.Visible = false;
            //    colBSDT.Visible = false;
            //}
            if (BSDT.Value != null)
            {
                colBSDT.Text = DungChung.Ham._getTenCB(data, BSDT.Value.ToString());
            }
            if (BSCK.Value != null)
            {
                colBSCK.Text = DungChung.Ham._getTenCB(data, BSCK.Value.ToString());
            }
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
        }
    }
}