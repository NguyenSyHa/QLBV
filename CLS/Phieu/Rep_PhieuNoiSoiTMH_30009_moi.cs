using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using QLBV_Library;

namespace QLBV.BaoCao
{
    public partial class Rep_PhieuNoiSoiTMH_30009_moi : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_PhieuNoiSoiTMH_30009_moi()
        {
            InitializeComponent();
        }

        public void hienthi(string ketqua, string duongdan,int[] index) 
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
           
            TXTCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            txtTenBV.Text = DungChung.Bien.TenCQ.ToUpper();
            SubBand1.Visible = true;
            //txtLuuY.Text = "      Chúng tôi không ngừng nỗ lực nâng cao chất lượng khám và điều trị kết hợp đổi mới phong cách, thái độ \nphục vụ với quyết tâm mang lại sự hài lòng cao nhất cho Quý bệnh nhân \n      Quý bệnh nhân lưu ý, nếu sử dụng thẻ BHYT trong quá trình khám bệnh, mong Quý bệnh nhân giữ lại phiếu này để thanh toán với cơ quan BHYT. Kính chào và chúc Quý bệnh nhân bình an";
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
