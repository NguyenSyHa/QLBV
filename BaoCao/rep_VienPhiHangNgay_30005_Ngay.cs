using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_VienPhiHangNgay_30005_Ngay : DevExpress.XtraReports.UI.XtraReport
    {
        private bool HienThi = false;//= true: chỉ hiển thị viện phí theo ngày

        public rep_VienPhiHangNgay_30005_Ngay()
        {
            InitializeComponent();
        }

        public rep_VienPhiHangNgay_30005_Ngay(bool hienthi)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.HienThi = hienthi;
        }


        internal void BindingData()
        {
            celNgay.DataBindings.Add("Text", DataSource, "NgayTT").FormatString = "{0: dd/MM/yyyy}";
            celHoTen.DataBindings.Add("Text", DataSource, "TenBNhan");
            

            celXN.DataBindings.Add("Text", DataSource, "XN").FormatString = DungChung.Bien.FormatString[1];
            celTDCN.DataBindings.Add("Text", DataSource, "TDCN").FormatString = DungChung.Bien.FormatString[1];
            celSieuAm.DataBindings.Add("Text", DataSource, "SieuAm").FormatString = DungChung.Bien.FormatString[1];
            celDienTim.DataBindings.Add("Text", DataSource, "DienTim").FormatString = DungChung.Bien.FormatString[1];
            celPttt.DataBindings.Add("Text", DataSource, "ThuThuatPT").FormatString = DungChung.Bien.FormatString[1];
            celNoisoi.DataBindings.Add("Text", DataSource, "NoiSoi").FormatString = DungChung.Bien.FormatString[1];
            celKSK.DataBindings.Add("Text", DataSource, "KSK").FormatString = DungChung.Bien.FormatString[1];
            celCongKham.DataBindings.Add("Text", DataSource, "Congkham").FormatString = DungChung.Bien.FormatString[1];
            celTienVC.DataBindings.Add("Text", DataSource, "TienVC").FormatString = DungChung.Bien.FormatString[1];
            celChupXQ.DataBindings.Add("Text", DataSource, "XQ").FormatString = DungChung.Bien.FormatString[1];          
            celBHNgoaiTru.DataBindings.Add("Text", DataSource, "NgoaiTruBH").FormatString = DungChung.Bien.FormatString[1];
            celTienBNDV.DataBindings.Add("Text", DataSource, "TienBN").FormatString = DungChung.Bien.FormatString[1];
            ColTienNGDM.DataBindings.Add("Text", DataSource, "TienNGDM").FormatString = DungChung.Bien.FormatString[1];
            celTong.DataBindings.Add("Text", DataSource, "Tong").FormatString = DungChung.Bien.FormatString[1];

            celXN_G.DataBindings.Add("Text", DataSource, "XN").FormatString = DungChung.Bien.FormatString[1];
            celTDCN_G.DataBindings.Add("Text", DataSource, "TDCN").FormatString = DungChung.Bien.FormatString[1];
            celSieuAm_G.DataBindings.Add("Text", DataSource, "SieuAm").FormatString = DungChung.Bien.FormatString[1];
            celDienTim_G.DataBindings.Add("Text", DataSource, "DienTim").FormatString = DungChung.Bien.FormatString[1];
            celPttt_G.DataBindings.Add("Text", DataSource, "ThuThuatPT").FormatString = DungChung.Bien.FormatString[1];
            celNoisoi_G.DataBindings.Add("Text", DataSource, "NoiSoi").FormatString = DungChung.Bien.FormatString[1];
            celKSK_G.DataBindings.Add("Text", DataSource, "KSK").FormatString = DungChung.Bien.FormatString[1];
            celCongKham_G.DataBindings.Add("Text", DataSource, "Congkham").FormatString = DungChung.Bien.FormatString[1];
            celTienVC_G.DataBindings.Add("Text", DataSource, "TienVC").FormatString = DungChung.Bien.FormatString[1];
            celChupXQ_G.DataBindings.Add("Text", DataSource, "XQ").FormatString = DungChung.Bien.FormatString[1];
            celBHNgoaiTru_G.DataBindings.Add("Text", DataSource, "NgoaiTruBH").FormatString = DungChung.Bien.FormatString[1];
            celTienBNDV_G.DataBindings.Add("Text", DataSource, "TienBN").FormatString = DungChung.Bien.FormatString[1];
            colTienNGDM_G.DataBindings.Add("Text", DataSource, "TienNGDM").FormatString = DungChung.Bien.FormatString[1];
            //celTong_G.DataBindings.Add("Text", DataSource, "NgoaiTruBH").FormatString = DungChung.Bien.FormatString[1];
            celTong_G.DataBindings.Add("Text", DataSource, "Tong").FormatString = DungChung.Bien.FormatString[1];
           

            celXN_R.DataBindings.Add("Text", DataSource, "XN").FormatString = DungChung.Bien.FormatString[1];
            celTDCN_R.DataBindings.Add("Text", DataSource, "TDCN").FormatString = DungChung.Bien.FormatString[1];
            celSieuAm_R.DataBindings.Add("Text", DataSource, "SieuAm").FormatString = DungChung.Bien.FormatString[1];
            celDienTim_R.DataBindings.Add("Text", DataSource, "DienTim").FormatString = DungChung.Bien.FormatString[1];
            celPttt_R.DataBindings.Add("Text", DataSource, "ThuThuatPT").FormatString = DungChung.Bien.FormatString[1];
            celNoisoi_R.DataBindings.Add("Text", DataSource, "NoiSoi").FormatString = DungChung.Bien.FormatString[1];
            celKSK_R.DataBindings.Add("Text", DataSource, "KSK").FormatString = DungChung.Bien.FormatString[1];
            celCongKham_R.DataBindings.Add("Text", DataSource, "Congkham").FormatString = DungChung.Bien.FormatString[1];
            celTienVC_R.DataBindings.Add("Text", DataSource, "TienVC").FormatString = DungChung.Bien.FormatString[1];
            celChupXQ_R.DataBindings.Add("Text", DataSource, "XQ").FormatString = DungChung.Bien.FormatString[1];
            celBHNgoaiTru_R.DataBindings.Add("Text", DataSource, "NgoaiTruBH").FormatString = DungChung.Bien.FormatString[1];
            celTienBNDV_R.DataBindings.Add("Text", DataSource, "TienBN").FormatString = DungChung.Bien.FormatString[1];
            ColTienNGDM_R.DataBindings.Add("Text", DataSource, "TienNGDM").FormatString = DungChung.Bien.FormatString[1];
            //celTong_R.DataBindings.Add("Text", DataSource, "NgoaiTruBH").FormatString = DungChung.Bien.FormatString[1];
            celTong_R.DataBindings.Add("Text", DataSource, "Tong").FormatString = DungChung.Bien.FormatString[1];
           if(HienThi)
               celCongNgay.DataBindings.Add("Text", DataSource, "NgayTT").FormatString = "{0: dd/MM/yyyy}";

            GroupHeader1.GroupFields.Add(new GroupField("NgayTT"));

        }

        private void GroupHeader1_BeforePrint(object sender, CancelEventArgs e)
        {
            GroupHeader1.Visible = !HienThi;
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            Detail.Visible = !HienThi;
        }

        private void PageHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            if (HienThi)
                lblHoTen.Text = "Ngày tháng";
            else
                lblHoTen.Text = "Họ tên";
        }
        int stt = 0;
        private void GroupFooter1_BeforePrint(object sender, CancelEventArgs e)
        {
            stt++;
            celSTTNgay.Text = stt.ToString();
        }
    }
}
