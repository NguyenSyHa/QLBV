using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class Rep_TongHopTienThuTheoCa_30303 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_TongHopTienThuTheoCa_30303()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        public void BindingData()
        {
            colTenBNhan.DataBindings.Add("Text", DataSource, "TenBNhan");
            colSoVV.DataBindings.Add("Text", DataSource, "SoBA");
            colTienKham.DataBindings.Add("Text", DataSource, "TienKham").FormatString = DungChung.Bien.FormatString[1];
            colThuDVu.DataBindings.Add("Text", DataSource, "TienDVu").FormatString = DungChung.Bien.FormatString[1];
            colTienThuocVTYT.DataBindings.Add("Text", DataSource, "TienThuocVTYT").FormatString = DungChung.Bien.FormatString[1];
            colTienTU.DataBindings.Add("Text", DataSource, "TienTU").FormatString = DungChung.Bien.FormatString[1];
            colTienVP.DataBindings.Add("Text", DataSource, "TienVP").FormatString = DungChung.Bien.FormatString[1];
            colTongTien.DataBindings.Add("Text", DataSource, "TienTong").FormatString = DungChung.Bien.FormatString[1];
            colMien.DataBindings.Add("Text", DataSource, "Mien").FormatString = DungChung.Bien.FormatString[1];
            colTienMien.DataBindings.Add("Text", DataSource, "TienMien").FormatString = DungChung.Bien.FormatString[1];
            colSoHD.DataBindings.Add("Text", DataSource, "SoHD").FormatString = DungChung.Bien.FormatString[1];
            

            colTienKhamT.DataBindings.Add("Text", DataSource, "TienKham");
            colTienKhamT.Summary.FormatString = DungChung.Bien.FormatString[1];

            colTienDVuT.DataBindings.Add("Text", DataSource, "TienDVu");
            colTienDVuT.Summary.FormatString = DungChung.Bien.FormatString[1];

            colTienThuocT.DataBindings.Add("Text", DataSource, "TienThuocVTYT");
            colTienThuocT.Summary.FormatString = DungChung.Bien.FormatString[1];

            colTienVPT.DataBindings.Add("Text", DataSource, "TienVP");
            colTienVPT.Summary.FormatString = DungChung.Bien.FormatString[1];

            colTongTienT.DataBindings.Add("Text", DataSource, "TienTong");
            colTongTienT.Summary.FormatString = DungChung.Bien.FormatString[1];

            colTienTUT.DataBindings.Add("Text", DataSource, "TienTU");
            colTienTUT.Summary.FormatString = DungChung.Bien.FormatString[1];

            colSumTienMien.DataBindings.Add("Text", DataSource, "TienMien");
            colSumTienMien.Summary.FormatString = DungChung.Bien.FormatString[1];
            

        }
        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            colTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            colTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            if(DungChung.Bien.MaBV=="30372")
            {
                xrTableCell18.Text = "% BHYT";
            }

        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            //colNguoiLapBieu.Text = DungChung.Bien.NguoiLapBieu;
            //colKeToan.Text = DungChung.Bien.KeToanTruong;
            //colTTDV.Text = DungChung.Bien.GiamDoc;
            //Double st = 0;
            //if (TongTien.Value != null)
            //    st = Convert.ToDouble(TongTien.Value);
            //st = Math.Round(st, 0);
            //txtsotien.Text = st.ToString("##,###") + " (" + DungChung.Ham.DocTienBangChu(st, " đồng)");
        }

        private void colKetDu_BeforePrint(object sender, CancelEventArgs e)
        {
            
            //Double st =0;
            //if(TongTien.Value!=null)
            //st = Convert.ToDouble(TongTien.Value);
            //st = Math.Round(st, 0);
            //colKetDu.Text = st.ToString("##,###");

        }

        private void txtsotien_BeforePrint(object sender, CancelEventArgs e)
        {
         
        }

    }
}
