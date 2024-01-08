using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_phieusoket15ngay : DevExpress.XtraReports.UI.XtraReport
    {
        private int soNgayDieuTri;
        string _BSDT;

        public rep_phieusoket15ngay()
        {
            InitializeComponent();
        }
        ChucNang.frm_PhieuSoKet15Ngay frm = new ChucNang.frm_PhieuSoKet15Ngay();

        public rep_phieusoket15ngay(int soNgayDieuTri, string BSDT)
        {
            // TODO: Complete member initialization
            InitializeComponent();

            this.soNgayDieuTri = soNgayDieuTri;
            _BSDT = BSDT;
        }

       

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            if (this.gtinh.Value.ToString() == "1")
            {
                xrLabel4.Visible = false;
            }
            else
                xrLabel3.Visible = false;

            lbltit.Text = "PHIẾU SƠ KẾT " + soNgayDieuTri + " NGÀY ĐIỀU TRỊ";
            if (DungChung.Bien.MaBV == "24272")
            {
                xrPictureBox1.Image = DungChung.Ham.GetLogo();
            }
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "12122" || DungChung.Bien.MaBV == "20001"|| DungChung.Bien.MaBV=="24012" ||DungChung.Bien.MaBV == "24009" || DungChung.Bien.MaBV == "14017")
            {
                Sub12122.Visible = true;
                SubChung.Visible = false;
                if (DungChung.Bien.MaBV == "24012")
                {
                    QLBV_Database.QLBVEntities DaTaContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                    DaTaContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

                   
                    txtTruongKhoa.Text = DungChung.Bien.TruongKhoaLS;
                    txtBSDT.Text = _BSDT;
                    txtNgayThang1.Text = Convert.ToString(DungChung.Ham.NgaySangChu(Convert.ToDateTime(NgayRa.Value)));
                    //txtNgayThang1.Text = "Ngày " +  + " tháng " " năm " ;
                }
            }
            
        }

        private void SubBand3_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV != "14018" && DungChung.Bien.MaBV != "14017" && DungChung.Bien.MaBV != "24012")
            {
                xrTableCell51.Text = "Họ tên:..........................";
                xrTableCell53.Text = "Họ tên:..........................";
            }
        }

    }
}
