using DevExpress.XtraReports.UI;
using System.ComponentModel;

namespace QLBV.BaoCao
{
    public partial class Rep_PhieuTHCD : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_PhieuTHCD()
        {
            InitializeComponent();
            if (DungChung.Bien.MaBV == "14018")
            {
                SubBand1.Visible = false;
                SubBand3.Visible = false;
                xrLabel6.Visible = false;
                txtGDBHYT.Visible = false;
            }
            else if (DungChung.Bien.MaBV == "27001" || DungChung.Bien.MaBV == "27183")
            {
                SubBand1.Visible = false;
                SubBand2.Visible = false;
                SubBand8.Visible = true;
                GroupHeader3.Visible = true;
                GroupHeader2.Visible = false;
                GroupHeader1.Visible = false;
                SubBand3.Visible = false;
                SubBand4.Visible = false;
                SubBand9.Visible = true;
            }
            else
            {
                SubBand2.Visible = false;
                SubBand4.Visible = false;
            }
            if (DungChung.Bien.MaBV == "24272")
            {
                sub_VienKhac.Visible = false;
                sub_24272.Visible = true;
            }
        }

        public void BindingData()
        {
            if (DungChung.Bien.MaBV == "14018")
            {
                xrTableCell11.DataBindings.Add("Text", DataSource, "TenTN");
                xrTableCell14.DataBindings.Add("Text", DataSource, "SoLuong");
                xrTableCell13.DataBindings.Add("Text", DataSource, "TenDV");
                GroupHeader2.GroupFields.Add(new GroupField("TenTN"));
                GroupHeader1.Visible = false;
            }
            else
            {
                colTenTN.DataBindings.Add("Text", DataSource, "TenTN");
                colTenTN2.DataBindings.Add("Text", DataSource, "TenTN");
                //if (DungChung.Bien.MaBV == "24272")
                //{
                //    colTenTN.DataBindings.Add("Text", DataSource, "TenRG");
                //}
                colSoLuong.DataBindings.Add("Text", DataSource, "SoLuong");
                colTenchidinh.DataBindings.Add("Text", DataSource, "TenDV");
                colTenchidinh2.DataBindings.Add("Text", DataSource, "TenDV");
                if (DungChung.Bien.MaBV == "24012")
                {
                    colTenchidinh.DataBindings.Add("Text", DataSource, "ylenh");
                    colTenchidinh2.DataBindings.Add("Text", DataSource, "ylenh");
                }
                GroupHeader1.GroupFields.Add(new GroupField("TenTN"));
                GroupHeader3.GroupFields.Add(new GroupField("TenTN"));
                GroupHeader2.Visible = false;
            }
            if (DungChung.Bien.MaBV == "30010")
                celDiachi.DataBindings.Add("Text", DataSource, "DChi");
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            colTenBSDT.Text = TenBS.Value.ToString();
            txtGDBHYT.Text = DungChung.Bien.GiamDinhBH;
            if (DungChung.Bien.MaBV == "30007" || DungChung.Bien.MaBV == "30004")
                colTenBSDT.Visible = false;
            //if(DungChung.Bien.MaBV =="14018")
            //{
            //    SubBand5.Visible = true;
            //}
            if (DungChung.Bien.MaBV != "12345" && DungChung.Bien.MaBV != "24297")
            {
                xrLabel7.Visible = false;
                xrLabel8.Visible = false;
            }
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            colTenCQCQ.Text = colTenCQCQ2.Text = DungChung.Bien.TenCQ;
            xrPictureBox1.Image = DungChung.Ham.GetLogo();
        }
    }
}