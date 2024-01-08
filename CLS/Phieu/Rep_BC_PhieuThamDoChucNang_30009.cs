using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_BC_PhieuThamDoChucNang_30009 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_BC_PhieuThamDoChucNang_30009()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tt">
        /// 1: (Phiếu thăm dò chức năng) kết quả đo mật độ loãng xương
        /// 2: (Phiếu thăm dò chức năng) Kết quả theo dõi nhịp tim thai và cơn co tử cung bằng monitor sản khoa
        /// 3: Đo khúc xạ máy
        /// </param>
        /// 
        public Rep_BC_PhieuThamDoChucNang_30009(int tt)
        {
            InitializeComponent();
            if (tt == 1)
            {
                cel_KetQua.Text = "KẾT QUẢ ĐO MẬT ĐỘ LOÃNG XƯƠNG";
            }
            else if (tt == 2)
            {
                cel_KetQua.Text = "KẾT QUẢ  THEO DÕI NHỊP TIM THAI VÀ CƠN CO TỬ CUNG  BẰNG MONITOR SẢN KHOA";
            }
            else if (tt == 3)
            {
                cel_KetQua.Text = "KẾT QUẢ ĐO KHÚC XẠ MÁY";
            }
        }

        public void BindingData()
        {
            lblHoTen.DataBindings.Add("Text", DataSource, "HoTen");
            lblDiaChi.DataBindings.Add("Text", DataSource, "DiaChi");
            lblTheBHYT.DataBindings.Add("Text", DataSource, "STheBHYT");
            lblKhoa.DataBindings.Add("Text", DataSource, "Khoa");
            lblBuong.DataBindings.Add("Text", DataSource, "Buong");
            lblGiuong.DataBindings.Add("Text", DataSource, "Giuong");
            lblChanDoan.DataBindings.Add("Text", DataSource, "ChanDoan");
            cel_YeuCau.DataBindings.Add("Text", DataSource, "YeuCau");
            celBSChiDinh.DataBindings.Add("Text", DataSource, "BSChiDinh");
            cel_KQ.DataBindings.Add("Text", DataSource, "KetQua");
            lblLoiDan.DataBindings.Add("Text", DataSource, "LoiDan");
            celBSCK.DataBindings.Add("Text", DataSource, "BSCK");
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            xrPictureBox1.Image = DungChung.Ham.GetLogo();
        }

        private void Rep_BC_PhieuThamDoChucNang_30009_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "30009")
            {
                foreach (XRControl item in xrTable1.Rows[0].Cells[0].Controls)
                {
                    item.Visible = DungChung.Bien._Visible_CDHA[0];
                }
                foreach (XRControl item in xrTable3.Rows[0].Cells[0].Controls)
                {
                    item.Visible = DungChung.Bien._Visible_CDHA[1];
                }
                foreach (XRControl item in xrTable9.Rows[0].Cells[0].Controls)
                {
                    item.Visible = DungChung.Bien._Visible_CDHA[0];
                }
                //xrTable1.Visible = DungChung.Bien._Visible_CDHA[0];
                //ReportHeader.Visible = DungChung.Bien._Visible_CDHA[0];
                //if (!DungChung.Bien._Visible_CDHA[0])
                //{
                //    xrTable3.TopF = xrTable1.TopF + xrTable1.HeightF + this.ReportHeader.HeightF + this.Margins.Top + this.Margins.Bottom;
                //}

                //xrTable3.Visible = DungChung.Bien._Visible_CDHA[1];
            }
        }
    }
}
