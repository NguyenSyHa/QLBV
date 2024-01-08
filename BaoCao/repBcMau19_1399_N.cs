using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using QLBV.FormThamSo;

namespace QLBV.BaoCao
{
    public partial class repBcMau19_1399_N : DevExpress.XtraReports.UI.XtraReport
    {
        public repBcMau19_1399_N()
        {
            InitializeComponent();
            if (DungChung.Bien.MaBV == "30350")
            {
                xrTableCell31.Text = xrTableCell107.Text = xrTableCell92.Text = "Phụ trách kho dược";
                xrTableCell33.Text = xrTableCell108.Text = xrTableCell93.Text = "Bệnh xá trưởng";
            }
        }
        bool inTieuNhom = false;
        public repBcMau19_1399_N(bool inTN)
        {
            InitializeComponent();
            inTieuNhom = inTN;
            if (DungChung.Bien.MaBV == "30350")
            {
                SubBand11.Visible = false;
                SubBand12.Visible = false;
                SubBand13.Visible = true;
                xrTableCell31.Text = xrTableCell107.Text = xrTableCell92.Text = "Phụ trách kho dược";
                xrTableCell33.Text = xrTableCell108.Text = xrTableCell93.Text = "Bệnh xá trưởng";
            }
        }
        public void BindingData()
        {
            colTenThuocGh2.DataBindings.Add("Text", DataSource, "Tennhom");
            colTenThuocGh1.DataBindings.Add("Text", DataSource, "TenTN");
            colTenHC.DataBindings.Add("Text", DataSource, "TenHC");
            colQCPC.DataBindings.Add("Text", DataSource, "QCPC");
            if (DungChung.Bien.MaBV != "30002" && DungChung.Bien.MaBV !="24012")
            colSTT.DataBindings.Add("Text", DataSource, "SoQD");
            if(DungChung.Bien.MaBV == "12122")
                colSTT.DataBindings.Add("Text", DataSource, "SoTTqd");
            colSTTQD.DataBindings.Add("Text", DataSource, "MaQD");
            colTenThuoc.DataBindings.Add("Text", DataSource, "Ten_thuoc");
            colDonGia.DataBindings.Add("Text", DataSource, "Don_gia").FormatString = DungChung.Bien.FormatString[1];
            colGiaMua.DataBindings.Add("Text", DataSource, "Don_gia").FormatString = DungChung.Bien.FormatString[1];
            colDonVi.DataBindings.Add("Text", DataSource, "Don_vi_tinh");
            ColSoLuongNoiT.DataBindings.Add("Text", DataSource, "SoluongNT").FormatString = DungChung.Bien.FormatString[0];
            ColSoLuong.DataBindings.Add("Text", DataSource, "SoluongNgT").FormatString = DungChung.Bien.FormatString[0];
            //ColSoLuongGp1.DataBindings.Add("Text", DataSource, "SoLuong").FormatString = DungChung.Bien.FormatString[1];
            colThanhTienGp1.DataBindings.Add("Text", DataSource, "Thanh_tien");
            colThanhTienGp1.Summary.FormatString = DungChung.Bien.FormatString[1];
            colThanhTienGp2.DataBindings.Add("Text", DataSource, "Thanh_tien");
            colThanhTienGp2.Summary.FormatString = DungChung.Bien.FormatString[1];
            colThanhTien.DataBindings.Add("Text", DataSource, "Thanh_tien").FormatString = DungChung.Bien.FormatString[1];
            colThanhTienTong.DataBindings.Add("Text", DataSource, "Thanh_tien");
            colThanhTienTong.Summary.FormatString = DungChung.Bien.FormatString[1];
            GroupHeader2.GroupFields.Add(new GroupField("Tennhom"));
            if (inTieuNhom)
            GroupHeader1.GroupFields.Add(new GroupField("TenTN"));
            if (DungChung.Bien.MaBV == "24012" && DungChung.Bien.Check_CV3762 == true) 
            {
                SubBand1.Visible = false;
                SubBand2.Visible = false;
                SubBand9.Visible = true;
                SubBand3.Visible = false;
                SubBand4.Visible = true;
                SubBand5.Visible = false;
                //SubBand6.Visible = true;
                SubBand7.Visible = false;
                SubBand8.Visible = true;
                SubBand10.Visible = false;
                SubBand11.Visible = true;
                //tableTong.Visible = false;
                //tableTong24012.Visible = true;

                colSoTT24012.DataBindings.Add("Text", DataSource, "num");
                colSTTQD24012.DataBindings.Add("Text", DataSource, "MaQD");
                colTenHC24012.DataBindings.Add("Text", DataSource, "TenHC");
                colTenThuoc24012.DataBindings.Add("Text", DataSource, "Ten_thuoc");
                colQCPC24012.DataBindings.Add("Text", DataSource, "QCPC");
                colDonVi24012.DataBindings.Add("Text", DataSource, "Don_vi_tinh");
               

                colSoLuongNoiT24012.DataBindings.Add("Text", DataSource, "SoluongNT").FormatString = DungChung.Bien.FormatString[0];
                //colSLNoiT_24012Gp1.DataBindings.Add("Text", DataSource, "SoluongNT");//.FormatString = DungChung.Bien.FormatString[0];
                //colSLNoiT_24012Gp2.DataBindings.Add("Text", DataSource, "SoluongNT").FormatString = DungChung.Bien.FormatString[0];
                //colSLNoiT_24012Tong.DataBindings.Add("Text", DataSource, "SoluongNT").FormatString = DungChung.Bien.FormatString[0];

                colSoLuong24012.DataBindings.Add("Text", DataSource, "SoluongNgT").FormatString = DungChung.Bien.FormatString[0];
                //colSL_24012Gp1.DataBindings.Add("Text", DataSource, "SoluongNgT");//.FormatString = DungChung.Bien.FormatString[0];
                //colSL_24012Gp2.DataBindings.Add("Text", DataSource, "SoluongNgT").FormatString = DungChung.Bien.FormatString[0];
                //colSL_24012Tong.DataBindings.Add("Text", DataSource, "SoluongNgT").FormatString = DungChung.Bien.FormatString[0];

                colDonGia24012.DataBindings.Add("Text", DataSource, "Don_gia").FormatString = DungChung.Bien.FormatString[1];
                //colDonGia_24012Gp1.DataBindings.Add("Text", DataSource, "Don_gia");
                //colDonGia_24012Gp1.Summary.FormatString = DungChung.Bien.FormatString[1];
                //colDonGia_24012Gp2.DataBindings.Add("Text", DataSource, "Don_gia");
                //colDonGia_24012Gp2.Summary.FormatString = DungChung.Bien.FormatString[1];
                //colDonGia_24012Tong.DataBindings.Add("Text", DataSource, "Don_gia");
                //colDonGia_24012Tong.Summary.FormatString = DungChung.Bien.FormatString[1];

                //ColSoLuongGp1.DataBindings.Add("Text", DataSource, "SoLuong").FormatString = DungChung.Bien.FormatString[1];
                colDonGiaBHYT24012.DataBindings.Add("Text", DataSource, "Don_giaBHYT").FormatString = DungChung.Bien.FormatString[1];
                colDonGiaBH_24012Gp1.DataBindings.Add("Text", DataSource, "Don_giaBHYT");
                colDonGiaBH_24012Gp1.Summary.FormatString = DungChung.Bien.FormatString[1];
                colDonGiaBH_24012Gp2.DataBindings.Add("Text", DataSource, "Don_giaBHYT");
                colDonGiaBH_24012Gp2.Summary.FormatString = DungChung.Bien.FormatString[1];
                //colDonGiaBH_24012Tong.DataBindings.Add("Text", DataSource, "Don_giaBHYT");
                //colDonGiaBH_24012Tong.Summary.FormatString = DungChung.Bien.FormatString[1];

                colTyLeTT24012.DataBindings.Add("Text", DataSource, "TyLeTT");
                colTyleTT_24012Gp1.DataBindings.Add("Text", DataSource, "TyLeTT");
                colTyleTT_24012Gp1.Summary.FormatString = DungChung.Bien.FormatString[1];
                colTyleTT_24012Gp2.DataBindings.Add("Text", DataSource, "TyLeTT");
                colTyleTT_24012Gp2.Summary.FormatString = DungChung.Bien.FormatString[1];
                //colTyLeTT24012Tong.DataBindings.Add("Text", DataSource, "TyLeTT");
                //colTyLeTT24012Tong.Summary.FormatString = DungChung.Bien.FormatString[1];

                colThanhTien24012.DataBindings.Add("Text", DataSource, "Thanh_tien").FormatString = DungChung.Bien.FormatString[1];
                //
                colThanhTien_24012Gp1.DataBindings.Add("Text", DataSource, "Thanh_tien");
                colThanhTien_24012Gp1.Summary.FormatString = DungChung.Bien.FormatString[1];
                colThanhTien_24012Gp2.DataBindings.Add("Text", DataSource, "Thanh_tien");
                colThanhTien_24012Gp2.Summary.FormatString = DungChung.Bien.FormatString[1];
                colThanhTien24012Tong.DataBindings.Add("Text", DataSource, "Thanh_tien");
                colThanhTien24012Tong.Summary.FormatString = DungChung.Bien.FormatString[1];
                //
                colThanhTienBHYT24012.DataBindings.Add("Text", DataSource, "Thanh_tien_BHYT").FormatString = DungChung.Bien.FormatString[1];
                colThanhToanBH_24012Gp1.DataBindings.Add("Text", DataSource, "Thanh_tien_BHYT");
                colThanhToanBH_24012Gp1.Summary.FormatString = DungChung.Bien.FormatString[1];
                colThanhToanBH_24012Gp2.DataBindings.Add("Text", DataSource, "Thanh_tien_BHYT");
                colThanhToanBH_24012Gp2.Summary.FormatString = DungChung.Bien.FormatString[1];
                colThanhToanBH_24012Tong.DataBindings.Add("Text", DataSource, "Thanh_tien_BHYT");
                colThanhToanBH_24012Tong.Summary.FormatString = DungChung.Bien.FormatString[1];
            }
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            txtTKD.Text = DungChung.Bien.TruongKhoaDuoc;
            colNguoiLap.Text = DungChung.Bien.NguoiLapBieu;
            colGiamDoc.Text = DungChung.Bien.GiamDoc;
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            colTenCQ.Text = "Tên CSYT: "+DungChung.Bien.TenCQ.ToUpper();
            colMaCQ.Text = "Mã CSYT: " +DungChung.Bien.MaBV;
            GroupHeader1.Visible = inTieuNhom;
            GroupFooter1.Visible = inTieuNhom;
            tableTong.Visible = true;
            tableTong24012.Visible = false;
            if (DungChung.Bien.MaBV == "24012" && DungChung.Bien.Check_CV3762 == true)
            {
                tableTong.Visible = false;
                tableTong24012.Visible = true;
            }
        }
        int stt = 1;
        int SoTT = 1;
        private void GroupHeader1_BeforePrint(object sender, CancelEventArgs e)
        {
            switch (stt)
            {
                case 1:
                    colSTTGh1.Text = "A";
                    colCongGr.Text = "Cộng A:";
                    break;
                case 2:
                    colSTTGh1.Text = "B";
                    colCongGr.Text = "Cộng B:";
                    break;
                case 3:
                    colSTTGh1.Text = "C";
                    colCongGr.Text = "Cộng C:";
                    break;
                case 4:
                    colSTTGh1.Text = "D";
                    colCongGr.Text = "Cộng D:";
                    break;
                case 5:
                    colSTTGh1.Text = "E";
                    colCongGr.Text = "Cộng E:";
                    break;
                case 6:
                    colSTTGh1.Text = "F";
                    colCongGr.Text = "Cộng F:";
                    break;
                case 7:
                    colSTTGh1.Text = "G";
                    colCongGr.Text = "Cộng G:";
                    break;
                case 8:
                    colSTTGh1.Text = "H";
                    colCongGr.Text = "Cộng H:";
                    break;
                case 9:
                    colSTTGh1.Text = "I";
                    colCongGr.Text = "Cộng I:";
                    break;

            }
            stt++;
        }

        private void GroupFooter2_BeforePrint(object sender, CancelEventArgs e)
        {
        }

        private void PageHeader_BeforePrint(object sender, CancelEventArgs e)
        {
           
         
        }

        private void rdFont_Properties_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "30002")
            {
                colSTT.Text = SoTT.ToString();
                SoTT++;
            }
        }
    }
}
