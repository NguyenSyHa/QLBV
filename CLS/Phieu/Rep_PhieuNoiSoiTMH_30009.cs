using System;
using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using QLBV_Library;

namespace QLBV.BaoCao
{
    public partial class Rep_PhieuNoiSoiTMH_30009 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_PhieuNoiSoiTMH_30009()
        {
            InitializeComponent();
        }

        public void hienthi(string ketqua, string duongdan, int[] index)
        {
            string[] arrKQ = QLBV_Ham.LayChuoi('|', ketqua);
            string[] arrDD = QLBV_Ham.LayChuoi('|', duongdan);
            for (int i = 0; i < arrKQ.Length; i++)
            {
                switch (i)
                {
                    case 0:
                        celTaiPhai.Text = arrKQ[i];
                        xpicTaiPhai.ImageUrl = arrDD[index[0]];
                        break;
                    case 1:
                        celTaiTrai.Text = arrKQ[i];
                        xpicTaiTrai.ImageUrl = arrDD[index[1]];
                        break;
                    case 2:
                        celMuiPhai.Text = arrKQ[i];
                        xpicMuiphai.ImageUrl = arrDD[index[2]];
                        break;
                    case 3:
                        celMuiTrai.Text = arrKQ[i];
                        xpicMuiTrai.ImageUrl = arrDD[index[3]];
                        break;
                    case 4:
                        celVom.Text = arrKQ[i];

                        break;
                    case 5:
                        celHong.Text = arrKQ[i];

                        break;
                    case 6:
                        celThanhQuan.Text = arrKQ[i];

                        break;
                }
            }
        }
        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "24297")
            {
                xrLabel34.Visible = false;
                xrLabel33.Visible = false;
                xrLabel28.Visible = false;
                xrLabel29.Visible = false;
                xrLabel40.Visible = false;
                xrLabel39.Visible = false;
                xrLabel38.Visible = false;
                xrLabel37.Visible = false;
                xrLabel18.Visible = false;
            }
            TXTCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            txtTenBV.Text = DungChung.Bien.TenCQ.ToUpper();
            //txtSoYT.Text = DungChung.Bien.TenCQCQ.ToUpper();
            txtTenBV1.Text = DungChung.Bien.TenCQ.ToUpper();
            txtsdt.Text = "Điện thoại: " + DungChung.Bien.SDTCQ;
            txtdchi.Text = "Địa chỉ: " + DungChung.Bien.DiaChi;
            if (DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "56789")
            {
                //xrPictureBox4.Visible = true;
                SubBand2.Visible = true;
                SubBand1.Visible = false;
                txtLuuY.Text = "Khi đến khám lại, bệnh nhân đem theo kết quả này!";
                SubBand3.Visible = false;
                SubBand4.Visible = true;
                this.txtLuuY.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                if (DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297")
                {
                    xrPictureBox1.Visible = true;
                    xrPictureBox1.Image = DungChung.Ham.GetLogo();
                    xrPictureBox10.Visible = false;
                    if (DungChung.Bien.MaBV != "24297")
                        xrPictureBox2.Visible = false;
                    else
                        xrPictureBox1.Visible = false;
                    txtLuuY.Text = "Khi đến khám lại, bệnh nhân đem theo kết quả này. ";
                    txtLuuY.Text += "Khám bệnh tất cả các ngày trong tuần \r\n";
                    txtLuuY.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
                    xrTableCell27.Text = "BÁC SĨ CHỈ ĐỊNH";
                    xrTableCell36.Visible = false;
                    SubBand3.Visible = true;
                    SubBand4.Visible = false;

                }
                else
                {
                    xrPictureBox1.Visible = false;
                    xrPictureBox10.Visible = true;
                    xrPictureBox2.Visible = false;
                }
            }
            else
            {
                SubBand2.Visible = false;
                SubBand1.Visible = true;
                SubBand3.Visible = true;
                SubBand4.Visible = false;
                this.txtLuuY.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
                txtLuuY.Text = "      Chúng tôi không ngừng nỗ lực nâng cao chất lượng khám và điều trị kết hợp đổi mới phong cách, thái độ \nphục vụ với quyết tâm mang lại sự hài lòng cao nhất cho Quý bệnh nhân \n      Quý bệnh nhân lưu ý, nếu sử dụng thẻ BHYT trong quá trình khám bệnh, mong Quý bệnh nhân giữ lại phiếu này để thanh toán với cơ quan BHYT. Kính chào và chúc Quý bệnh nhân bình an";
            }
        }
        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            //if (DungChung.Bien.MaBV == "30009") {
            //    colNgayCD.Visible = false;
            //    colBSDT.Visible = false;
            //    colBSK.Visible = false;
            //}
        }


        private void xrTableCell17_BeforePrint(object sender, CancelEventArgs e)
        {

        }
    }
}
