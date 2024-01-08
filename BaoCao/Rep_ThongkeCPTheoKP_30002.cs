using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_ThongkeCPTheoKP_30002 : DevExpress.XtraReports.UI.XtraReport
    {
        //private int _mau = 0;// mẫu: 0: Chi tiết; 1: Tổng hợp

        public Rep_ThongkeCPTheoKP_30002()
        {
            InitializeComponent();
        }

        //public Rep_ThongkeCPTheoKP(int mau)
        //{
           
        //    InitializeComponent();
        //    this._mau = mau;
        //}
        public void BindingData()
        {
            colTenKP.DataBindings.Add("Text", DataSource, "TenKP");
            colLuotKham.DataBindings.Add("Text", DataSource, "SoLuotKham").FormatString = DungChung.Bien.FormatString[0];
            colSoNgaydt.DataBindings.Add("Text", DataSource, "SoNgaydt").FormatString = DungChung.Bien.FormatString[0];
            colXetnghiem.DataBindings.Add("Text", DataSource, "Xetnghiem").FormatString = DungChung.Bien.FormatString[1];
            colCDHA.DataBindings.Add("Text", DataSource, "CDHA").FormatString = DungChung.Bien.FormatString[1];
            colTDCN.DataBindings.Add("Text", DataSource, "TDCN").FormatString = DungChung.Bien.FormatString[1];
            colThuoc.DataBindings.Add("Text", DataSource, "ThuocDT").FormatString = DungChung.Bien.FormatString[1];
            colMau.DataBindings.Add("Text", DataSource, "Mau").FormatString = DungChung.Bien.FormatString[1];
            colTTPT.DataBindings.Add("Text", DataSource, "TTPT").FormatString = DungChung.Bien.FormatString[1];
            colVTTH.DataBindings.Add("Text", DataSource, "VTYTTieuHao").FormatString = DungChung.Bien.FormatString[1];
            colVTTT.DataBindings.Add("Text", DataSource, "VTYTTyLe").FormatString = DungChung.Bien.FormatString[1];
            colDVKTC.DataBindings.Add("Text", DataSource, "DVKTC").FormatString = DungChung.Bien.FormatString[1];
            colTTG.DataBindings.Add("Text", DataSource, "TTG").FormatString = DungChung.Bien.FormatString[1];
            colCongKham.DataBindings.Add("Text", DataSource, "Congkham").FormatString = DungChung.Bien.FormatString[1];
            colVanChuyen.DataBindings.Add("Text", DataSource, "Vanchuyen").FormatString = DungChung.Bien.FormatString[1];
            celTienGiuong.DataBindings.Add("Text", DataSource, "TienGiuong").FormatString = DungChung.Bien.FormatString[1];
            colTongCP.DataBindings.Add("Text", DataSource, "TongCP").FormatString = DungChung.Bien.FormatString[1];
            colBNChiTra.DataBindings.Add("Text", DataSource, "BNCungChiTra").FormatString = DungChung.Bien.FormatString[1];

            colXetnghiem_G1.DataBindings.Add("Text", DataSource, "Xetnghiem");
            colXetnghiem_G1.Summary.FormatString = DungChung.Bien.FormatString[1];
            colCDHA_G1.DataBindings.Add("Text", DataSource, "CDHA");
            colCDHA_G1.Summary.FormatString = DungChung.Bien.FormatString[1];
            colTDCN_G1.DataBindings.Add("Text", DataSource, "TDCN");
            colTDCN_G1.Summary.FormatString = DungChung.Bien.FormatString[1];
            colThuoc_G1.DataBindings.Add("Text", DataSource, "ThuocDT");
            colThuoc_G1.Summary.FormatString = DungChung.Bien.FormatString[1];
            colMau_G1.DataBindings.Add("Text", DataSource, "Mau");
            colMau_G1.Summary.FormatString = DungChung.Bien.FormatString[1];
            colTTPT_G1.DataBindings.Add("Text", DataSource, "TTPT");
            colTTPT_G1.Summary.FormatString = DungChung.Bien.FormatString[1];
            colVTTH_G1.DataBindings.Add("Text", DataSource, "VTYTTieuHao");
            colVTTH_G1.Summary.FormatString = DungChung.Bien.FormatString[1];
            colVTTT_G1.DataBindings.Add("Text", DataSource, "VTYTTyLe");
            colVTTT_G1.Summary.FormatString = DungChung.Bien.FormatString[1];
            colDVKTC_G1.DataBindings.Add("Text", DataSource, "DVKTC");
            colDVKTC_G1.Summary.FormatString = DungChung.Bien.FormatString[1];
            colTTG_G1.DataBindings.Add("Text", DataSource, "TTG");
            colTTG_G1.Summary.FormatString = DungChung.Bien.FormatString[1];
            colCongKham_G1.DataBindings.Add("Text", DataSource, "Congkham");
            colCongKham_G1.Summary.FormatString = DungChung.Bien.FormatString[1];
            colVanChuyen_G1.DataBindings.Add("Text", DataSource, "Vanchuyen");
            colVanChuyen_G1.Summary.FormatString = DungChung.Bien.FormatString[1];
            colTongCP_G1.DataBindings.Add("Text", DataSource, "TongCP");
            colTongCP_G1.Summary.FormatString = DungChung.Bien.FormatString[1];
            colBNChiTra_G1.DataBindings.Add("Text", DataSource, "BNCungChiTra");
            colBNChiTra_G1.Summary.FormatString = DungChung.Bien.FormatString[1];
            celTienGiuong_G1.DataBindings.Add("Text", DataSource, "TienGiuong");
            celTienGiuong_G1.Summary.FormatString = DungChung.Bien.FormatString[1];

            colXetnghiem_G2.DataBindings.Add("Text", DataSource, "Xetnghiem");
            colXetnghiem_G2.Summary.FormatString = DungChung.Bien.FormatString[1];
            colCDHA_G2.DataBindings.Add("Text", DataSource, "CDHA");
            colCDHA_G2.Summary.FormatString = DungChung.Bien.FormatString[1];
            colTDCN_G2.DataBindings.Add("Text", DataSource, "TDCN");
            colTDCN_G2.Summary.FormatString = DungChung.Bien.FormatString[1];
            colThuoc_G2.DataBindings.Add("Text", DataSource, "ThuocDT");
            colThuoc_G2.Summary.FormatString = DungChung.Bien.FormatString[1];
            colMau_G2.DataBindings.Add("Text", DataSource, "Mau");
            colMau_G2.Summary.FormatString = DungChung.Bien.FormatString[1];
            colTTPT_G2.DataBindings.Add("Text", DataSource, "TTPT");
            colTTPT_G2.Summary.FormatString = DungChung.Bien.FormatString[1];
            colVTTH_G2.DataBindings.Add("Text", DataSource, "VTYTTieuHao");
            colVTTH_G2.Summary.FormatString = DungChung.Bien.FormatString[1];
            colVTTT_G2.DataBindings.Add("Text", DataSource, "VTYTTyLe");
            colVTTT_G2.Summary.FormatString = DungChung.Bien.FormatString[1];
            colDVKTC_G2.DataBindings.Add("Text", DataSource, "DVKTC");
            colDVKTC_G2.Summary.FormatString = DungChung.Bien.FormatString[1];
            colTTG_G2.DataBindings.Add("Text", DataSource, "TTG");
            colTTG_G2.Summary.FormatString = DungChung.Bien.FormatString[1];
            colCongKham_G2.DataBindings.Add("Text", DataSource, "Congkham");
            colCongKham_G2.Summary.FormatString = DungChung.Bien.FormatString[1];
            colVanChuyen_G2.DataBindings.Add("Text", DataSource, "Vanchuyen");
            colVanChuyen_G2.Summary.FormatString = DungChung.Bien.FormatString[1];
            colTongCP_G2.DataBindings.Add("Text", DataSource, "TongCP");
            colTongCP_G2.Summary.FormatString = DungChung.Bien.FormatString[1];
            colBNChiTra_G2.DataBindings.Add("Text", DataSource, "BNCungChiTra");
            colBNChiTra_G2.Summary.FormatString = DungChung.Bien.FormatString[1];
            celTienGiuong_G2.DataBindings.Add("Text", DataSource, "TienGiuong");
            celTienGiuong_G2.Summary.FormatString = DungChung.Bien.FormatString[1];

            colRFXetnghiem.DataBindings.Add("Text", DataSource, "Xetnghiem");
            colRFXetnghiem.Summary.FormatString = DungChung.Bien.FormatString[1];
            colRFCDHA.DataBindings.Add("Text", DataSource, "CDHA");
            colRFCDHA.Summary.FormatString = DungChung.Bien.FormatString[1];
            colRFTDCN.DataBindings.Add("Text", DataSource, "TDCN");
            colRFTDCN.Summary.FormatString = DungChung.Bien.FormatString[1];
            colRFThuoc.DataBindings.Add("Text", DataSource, "ThuocDT");
            colRFThuoc.Summary.FormatString = DungChung.Bien.FormatString[1];
            colRFMau.DataBindings.Add("Text", DataSource, "Mau");
            colRFMau.Summary.FormatString = DungChung.Bien.FormatString[1];
            colRFTTPT.DataBindings.Add("Text", DataSource, "TTPT");
            colRFTTPT.Summary.FormatString = DungChung.Bien.FormatString[1];
            colRFVTTH.DataBindings.Add("Text", DataSource, "VTYTTieuHao");
            colRFVTTH.Summary.FormatString = DungChung.Bien.FormatString[1];
            colRFVTTT.DataBindings.Add("Text", DataSource, "VTYTTyLe");
            colRFVTTT.Summary.FormatString = DungChung.Bien.FormatString[1];
            colRFDVKTC.DataBindings.Add("Text", DataSource, "DVKTC");
            colRFDVKTC.Summary.FormatString = DungChung.Bien.FormatString[1];
            colRFTTG.DataBindings.Add("Text", DataSource, "TTG");
            colRFTTG.Summary.FormatString = DungChung.Bien.FormatString[1];
            colRFCongKham.DataBindings.Add("Text", DataSource, "Congkham");
            colRFCongKham.Summary.FormatString = DungChung.Bien.FormatString[1];
            colRFVanChuyen.DataBindings.Add("Text", DataSource, "Vanchuyen");
            colRFVanChuyen.Summary.FormatString = DungChung.Bien.FormatString[1];
            colRFTongCP.DataBindings.Add("Text", DataSource, "TongCP");
            colRFTongCP.Summary.FormatString = DungChung.Bien.FormatString[1];
            colRFBN.DataBindings.Add("Text", DataSource, "BNCungChiTra");
            colRFBN.Summary.FormatString = DungChung.Bien.FormatString[1];
            celTienGiuong_T.DataBindings.Add("Text", DataSource, "TienGiuong");
            celTienGiuong_T.Summary.FormatString = DungChung.Bien.FormatString[1];

            TenNhom1.DataBindings.Add("Text", DataSource, "TenNhom1");
            TenNhom2.DataBindings.Add("Text", DataSource, "TenNhom2");
            GroupHeader2.GroupFields.Add(new GroupField("IDNhom1"));
            GroupHeader1.GroupFields.Add(new GroupField("IDNhom2"));
            

        }

        private void colGH_BeforePrint(object sender, CancelEventArgs e)
        {

        }


        private void colGF_BeforePrint(object sender, CancelEventArgs e)
        {

        }
        
        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {

        }
        int stt2 = 1;
        private void GroupHeader1_BeforePrint(object sender, CancelEventArgs e)
        {
            switch (stt2)
            {
                case 1:
                    STTNhom2.Text = "I.";
                    break;
                case 2:
                    STTNhom2.Text = "II.";
                    break;
                case 3:
                    STTNhom2.Text = "III.";
                    break;
                case 4:
                    STTNhom2.Text = "IV.";
                    break;
            }
            stt2++;
        }

        private void GroupFooter1_BeforePrint(object sender, CancelEventArgs e)
        {

        }
        int stt1 = 1;
        private void GroupHeader2_BeforePrint(object sender, CancelEventArgs e)
        {
            switch(stt1)
            {
                case 1:
                    STTNhom1.Text = "A.";
                    break;
                case 2:
                    STTNhom1.Text = "B.";
                    break;
            }
            stt1++;
            stt2 = 1;
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            txtTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            txtTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
        }
    }
}
