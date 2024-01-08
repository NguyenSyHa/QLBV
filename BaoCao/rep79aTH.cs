using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class rep79aTH : DevExpress.XtraReports.UI.XtraReport
    {
        List<BNKB> _bnkb = new List<BNKB>();
        string _dt = "BHYT";
        public rep79aTH()
        {
            InitializeComponent();
            //QLBV_Database.QLBVEntities _dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            //List<BNKB> bnkb = new List<BNKB>();
            //bnkb = _dataContext.BNKBs.ToList();
            //_bnkb = bnkb; 
        }
        public rep79aTH(string dtuong)
        {
            InitializeComponent();
            _dt = dtuong;
            //QLBV_Database.QLBVEntities _dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            //List<BNKB> bnkb = new List<BNKB>();
            //bnkb = _dataContext.BNKBs.ToList();
            //_bnkb = bnkb;
        }
        public void BindingData()
        {
            colCDHA.DataBindings.Add("Text", DataSource, "CDHA").FormatString = DungChung.Bien.FormatString[1];
            colCDHAGF1.DataBindings.Add("Text", DataSource, "CDHA").FormatString = DungChung.Bien.FormatString[1];
            colCDHAGF2.DataBindings.Add("Text", DataSource, "CDHA").FormatString = DungChung.Bien.FormatString[1];
            colCDHARF.DataBindings.Add("Text", DataSource, "CDHA").FormatString = DungChung.Bien.FormatString[1];
            colghCDHA.DataBindings.Add("Text", DataSource, "CDHA").FormatString = DungChung.Bien.FormatString[1];
            colcongkham1.DataBindings.Add("Text", DataSource, "Congkham").FormatString = DungChung.Bien.FormatString[1];
            colcongkham2.DataBindings.Add("Text", DataSource, "Congkham").FormatString = DungChung.Bien.FormatString[1];
            colcongkham3.DataBindings.Add("Text", DataSource, "Congkham").FormatString = DungChung.Bien.FormatString[1];
            colcongkham4.DataBindings.Add("Text", DataSource, "Congkham").FormatString = DungChung.Bien.FormatString[1];
            Congkham.DataBindings.Add("Text", DataSource, "Congkham").FormatString = DungChung.Bien.FormatString[1];
            colCPNgoaiBHYT.DataBindings.Add("Text", DataSource, "CPVanchuyen").FormatString = DungChung.Bien.FormatString[1];
            colCPNgoaiBHYTGF1.DataBindings.Add("Text", DataSource, "CPVanchuyen").FormatString = DungChung.Bien.FormatString[1];
            colCPNgoaiBHYTGF2.DataBindings.Add("Text", DataSource, "CPVanchuyen").FormatString = DungChung.Bien.FormatString[1];
            colCPNgoaiBHYTRF.DataBindings.Add("Text", DataSource, "CPVanchuyen").FormatString = DungChung.Bien.FormatString[1];
            colDungTuyen.DataBindings.Add("Text", DataSource, "CPVanchuyen").FormatString = DungChung.Bien.FormatString[1];
            colTongchi.DataBindings.Add("Text", DataSource, "CPVanchuyen").FormatString = DungChung.Bien.FormatString[1];
            colTongchiGF1.DataBindings.Add("Text", DataSource, "CPVanchuyen").FormatString = DungChung.Bien.FormatString[1];
            colTongchiGF2.DataBindings.Add("Text", DataSource, "CPVanchuyen").FormatString = DungChung.Bien.FormatString[1];
            colTongchiRF.DataBindings.Add("Text", DataSource, "CPVanchuyen").FormatString = DungChung.Bien.FormatString[1];
            colDVKTC.DataBindings.Add("Text", DataSource, "DVKTC").FormatString = DungChung.Bien.FormatString[1];
            colDVKTCGF2.DataBindings.Add("Text", DataSource, "DVKTC").FormatString = DungChung.Bien.FormatString[1];
            colDVKTGF1.DataBindings.Add("Text", DataSource, "DVKTC").FormatString = DungChung.Bien.FormatString[1];
            colDVKTCRF.DataBindings.Add("Text", DataSource, "DVKTC").FormatString = DungChung.Bien.FormatString[1];
            colghDVKCT.DataBindings.Add("Text", DataSource, "DVKTC").FormatString = DungChung.Bien.FormatString[1];
            colGiotinh.DataBindings.Add("Text", DataSource, "GTinh");
            colHotenBN.DataBindings.Add("Text", DataSource, "TenBNhan");
            colMabenh.DataBindings.Add("Text", DataSource, "MaICD");
            colMaCS.DataBindings.Add("Text", DataSource, "MaCS");
            colMau.DataBindings.Add("Text", DataSource, "Mau").FormatString = DungChung.Bien.FormatString[1];
            colMauGF1.DataBindings.Add("Text", DataSource, "Mau").FormatString = DungChung.Bien.FormatString[1];
            colMauGF2.DataBindings.Add("Text", DataSource, "Mau").FormatString = DungChung.Bien.FormatString[1];
            colMauRF.DataBindings.Add("Text", DataSource, "Mau").FormatString = DungChung.Bien.FormatString[1];
            colghMau.DataBindings.Add("Text", DataSource, "Mau").FormatString = DungChung.Bien.FormatString[1];
            colMStheBHYT.DataBindings.Add("Text", DataSource, "SThe");
            //txttuoi.DataBindings.Add("Text",DataSource,"Tuoi");
            colNamsinh.DataBindings.Add("Text", DataSource, "NSinh");
            //txtngaykham.DataBindings.Add("Text", DataSource, "Ngaykham");
            colNguoibenhchitra.DataBindings.Add("Text", DataSource, "TienBN").FormatString = DungChung.Bien.FormatString[1];
            colNguoibenhchitraRF.DataBindings.Add("Text", DataSource, "TienBN").FormatString = DungChung.Bien.FormatString[1];
            colNguoibenhchitraGF1.DataBindings.Add("Text", DataSource, "TienBN").FormatString = DungChung.Bien.FormatString[1];
            colNguoibenhchitraGF2.DataBindings.Add("Text", DataSource, "TienBN").FormatString = DungChung.Bien.FormatString[1];
            colghnguoibenh.DataBindings.Add("Text", DataSource, "TienBN").FormatString = DungChung.Bien.FormatString[1];
            //colTennhomGF2.DataBindings.Add("Text", DataSource, "Tennhom");
            colThuoc.DataBindings.Add("Text", DataSource, "Thuoc").FormatString = DungChung.Bien.FormatString[1];
            colThuocGF1.DataBindings.Add("Text", DataSource, "Thuoc").FormatString = DungChung.Bien.FormatString[1];
            colThuocGF2.DataBindings.Add("Text", DataSource, "Thuoc").FormatString = DungChung.Bien.FormatString[1];
            colThuocRF.DataBindings.Add("Text", DataSource, "Thuoc").FormatString = DungChung.Bien.FormatString[1];
            colghThuoc.DataBindings.Add("Text", DataSource, "Thuoc").FormatString = DungChung.Bien.FormatString[1];
            colVTYTTT.DataBindings.Add("Text", DataSource, "VTTT").FormatString = DungChung.Bien.FormatString[1];
            colVTYTTTgf1.DataBindings.Add("Text", DataSource, "VTTT").FormatString = DungChung.Bien.FormatString[1];
            colVTYTTTgf2.DataBindings.Add("Text", DataSource, "VTTT").FormatString = DungChung.Bien.FormatString[1];
            colVTYTTTrf.DataBindings.Add("Text", DataSource, "VTTT").FormatString = DungChung.Bien.FormatString[1];
            colghTTG.DataBindings.Add("Text", DataSource, "ThuocKCTG").FormatString = DungChung.Bien.FormatString[1];
            colThuocKCTG.DataBindings.Add("Text", DataSource, "ThuocKCTG").FormatString = DungChung.Bien.FormatString[1];
            colThuocKCTGGF1.DataBindings.Add("Text", DataSource, "ThuocKCTG").FormatString = DungChung.Bien.FormatString[1];
            colThuocKCTGRF.DataBindings.Add("Text", DataSource, "ThuocKCTG").FormatString = DungChung.Bien.FormatString[1];
            colThuocKCTGGF2.DataBindings.Add("Text", DataSource, "ThuocKCTG").FormatString = DungChung.Bien.FormatString[1];
            //colTongchi.DataBindings.Add("Text", DataSource, "Tongchi1").FormatString = DungChung.Bien.FormatString[1];
            //colTongchiGF1.DataBindings.Add("Text", DataSource, "Tongchi1").FormatString = DungChung.Bien.FormatString[1];
            //colTongchiGF2.DataBindings.Add("Text", DataSource, "tongchi1").FormatString = DungChung.Bien.FormatString[1];
            //colTongchiRF.DataBindings.Add("Text", DataSource, "tongchi1").FormatString = DungChung.Bien.FormatString[1];
            colTongcong.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];
            colTongcongBHYT.DataBindings.Add("Text", DataSource, "TienBH").FormatString = DungChung.Bien.FormatString[1];
            colTongcongBHYTGF1.DataBindings.Add("Text", DataSource, "TienBH").FormatString = DungChung.Bien.FormatString[1];
            colTongcongBHYTGF2.DataBindings.Add("Text", DataSource, "TienBH").FormatString = DungChung.Bien.FormatString[1];
            colTongcongBHYTRF.DataBindings.Add("Text", DataSource, "TienBH").FormatString = DungChung.Bien.FormatString[1];
            colghBHTT.DataBindings.Add("Text", DataSource, "TienBH").FormatString = DungChung.Bien.FormatString[1];
            colTongcongGF1.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];
            colghTongcong.DataBindings.Add("Text",DataSource,"ThanhTien").FormatString = DungChung.Bien.FormatString[1];
            colTongcongGF2.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];
            colTongcongRF2.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];
            colTTPT.DataBindings.Add("Text", DataSource, "TTPT").FormatString = DungChung.Bien.FormatString[1];
            colTTPTGF1.DataBindings.Add("Text", DataSource, "TTPT").FormatString = DungChung.Bien.FormatString[1];
            colTTPTGF2.DataBindings.Add("Text", DataSource, "TTPT").FormatString = DungChung.Bien.FormatString[1];
            colTTPTRF.DataBindings.Add("Text", DataSource, "TTPT").FormatString = DungChung.Bien.FormatString[1];
            colghTTPT.DataBindings.Add("Text", DataSource, "TTPT").FormatString = DungChung.Bien.FormatString[1];
            colTuyen.DataBindings.Add("Text", DataSource, "Tuyen").FormatString = DungChung.Bien.FormatString[1];
            colThuocKCTGGF1.DataBindings.Add("Text", DataSource, "TTG").FormatString = DungChung.Bien.FormatString[1];
            colThuocKCTGGF2.DataBindings.Add("Text", DataSource, "TTG").FormatString = DungChung.Bien.FormatString[1];
            colThuocKCTGRF.DataBindings.Add("Text", DataSource, "TTG").FormatString = DungChung.Bien.FormatString[1];
            //colcongkham1.DataBindings.Add("Text", DataSource, "Vanchuyen").FormatString = DungChung.Bien.FormatString[1];
            //colcongkham2.DataBindings.Add("Text", DataSource, "Vanchuyen").FormatString = DungChung.Bien.FormatString[1];
            //colcongkham3.DataBindings.Add("Text", DataSource, "Vanchuyen").FormatString = DungChung.Bien.FormatString[1];
            //colcongkham4.DataBindings.Add("Text", DataSource, "Vanchuyen").FormatString = DungChung.Bien.FormatString[1];
            colghVanchuyen.DataBindings.Add("Text", DataSource, "CPVanchuyen").FormatString = DungChung.Bien.FormatString[1]; //
            colVTYT.DataBindings.Add("Text", DataSource, "VTYTH").FormatString = DungChung.Bien.FormatString[1];
            colVTYTGF1.DataBindings.Add("Text", DataSource, "VTYTH").FormatString = DungChung.Bien.FormatString[1];
            colVTYTGF2.DataBindings.Add("Text", DataSource, "VTYTH").FormatString = DungChung.Bien.FormatString[1];
            colVTYTRF.DataBindings.Add("Text", DataSource, "VTYTH").FormatString = DungChung.Bien.FormatString[1];
            colghVTTH.DataBindings.Add("Text", DataSource, "VTYTH").FormatString = DungChung.Bien.FormatString[1];
            colghVTTT.DataBindings.Add("Text", DataSource, "VTTT").FormatString = DungChung.Bien.FormatString[1];
            colxetnghiem.DataBindings.Add("Text", DataSource, "xetnghiem").FormatString = DungChung.Bien.FormatString[1];
            ColXetnghiemGF1.DataBindings.Add("Text", DataSource, "xetnghiem").FormatString = DungChung.Bien.FormatString[1];
            colXetnghiemGF2.DataBindings.Add("Text", DataSource, "xetnghiem").FormatString = DungChung.Bien.FormatString[1];
            colXetnghiemRF.DataBindings.Add("Text", DataSource, "xetnghiem").FormatString = DungChung.Bien.FormatString[1];
            colghxetnghiem.DataBindings.Add("Text", DataSource, "xetnghiem").FormatString = DungChung.Bien.FormatString[1];
            txtNoiTinh.DataBindings.Add("Text", DataSource, "NoiTinh").FormatString = DungChung.Bien.FormatString[1];
            txtMabn.DataBindings.Add("Text", DataSource, "Mabn");
            GroupHeader1.GroupFields.Add(new GroupField("NoiTinh"));
            GroupHeader2.GroupFields.Add(new GroupField("Tuyen"));
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            txtTongcong.Text = tongcong;
            //Nguoilapbieu.Value = "Dinh";
            if (this.GetCurrentColumnValue("TienBH") != null) {
                double tien=Convert.ToDouble(this.GetCurrentColumnValue("TienBH"));
               // txtSoTienBangChu.Text = DungChung.Ham.DocTienBangChu(tien, " đồng chẵn");
            }
        }
        string tongcong = " Tổng cộng ";
        private void GroupHeader1_BeforePrint(object sender, CancelEventArgs e)
        {
            if (this.GetCurrentColumnValue("NoiTinh") != null)
            {
                string noitinh = this.GetCurrentColumnValue("NoiTinh").ToString();
                switch (noitinh)
                {
                    case "1":
                        colNhomBNG1.Text = " Bệnh nhân nội tỉnh kcb ban đầu".ToUpper();
                        colSTTG1.Text = "A";
                        tongcong += "A";
                        colTennhomGF2.Text = " Cộng: A";
                        
                        break;
                    case "2":
                        colNhomBNG1.Text = " Bệnh nhân nội tỉnh đến".ToUpper();
                        colSTTG1.Text = "B";
                        tongcong += "+B";
                        colTennhomGF2.Text = " Cộng: B";
                        
                        break;
                    case "3":
                        colNhomBNG1.Text = " Bệnh nhân ngoại tỉnh đến".ToUpper();
                        colSTTG1.Text = "C";
                        tongcong += "+C";
                        colTennhomGF2.Text = " Cộng: C";
                        
                        break;
                }
            }
        }
        private void GroupHeader2_BeforePrint(object sender, CancelEventArgs e)
        {
            if (this.GetCurrentColumnValue("Tuyen") != null)
            {
            int sttg2 = int.Parse(this.GetCurrentColumnValue("Tuyen").ToString().Trim());
            if (sttg2 == 2)
            {
                colSoTTg2.Text = "II";
            }
            else
            {
                if (sttg2 == 1)
                {
                    colSoTTg2.Text = "I";
                }
            }
         }
            if (this.GetCurrentColumnValue("Tuyen") != null)
            {
                int tuyen = int.Parse(this.GetCurrentColumnValue("Tuyen").ToString().Trim());
                if (tuyen == 2)
                {
                    colTuyenGrp2.Text = " Trái tuyến";
                    colTennhomGF1.Text = " Cộng trái tuyến";
                    
                }
                if (tuyen == 1)
                {
                    colTuyenGrp2.Text = " Đúng tuyến";
                    colTennhomGF1.Text = " Cộng đúng tuyến";
                }

            }
            //tỉnh số lượt
        }
     
        private void xrLabel28_BeforePrint(object sender, CancelEventArgs e)
        {
        }
        int i = 1;
        private void PageHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            txttrang.Text = i.ToString();
            i = i + 1;
        }
        private void txttuoi_BeforePrint(object sender, CancelEventArgs e)
        {
            
        }

        private void colTuyenGrp2_BeforePrint(object sender, CancelEventArgs e)
        {
            //if(this.GetCurrentColumnValue("")
        }

        private void colSLGH2_BeforePrint(object sender, CancelEventArgs e)
        {

            if (this.GetCurrentColumnValue("NoiTinh") != null)
            {
                string noitinh = this.GetCurrentColumnValue("NoiTinh").ToString();
                switch (noitinh)
                {
                    case "1":
                        if (this.GetCurrentColumnValue("Tuyen")!=null && this.GetCurrentColumnValue("Tuyen").ToString() == "1")
                        colSLGH2.Text = SLAdungtuyen.Value.ToString(); 
                        else
                        colSLGH2.Text = SLAtraituyen.Value.ToString(); 
                        break;
                    case "2":
                        if (this.GetCurrentColumnValue("Tuyen") != null && this.GetCurrentColumnValue("Tuyen").ToString() == "1")
                        { colSLGH2.Text = SLBdungtuyen.Value.ToString(); }
                        else
                        { colSLGH2.Text = SLBTraituyen.Value.ToString(); }
                        break;
                    case "3":
                        if (this.GetCurrentColumnValue("Tuyen")!=null && this.GetCurrentColumnValue("Tuyen").ToString() == "1")
                        { colSLGH2.Text = SLCdungtuyen.Value.ToString(); }
                        else
                        { colSLGH2.Text = SLCTraituyen.Value.ToString(); }
                        break;
                }
            }

        }

        private void colSLGF2_BeforePrint(object sender, CancelEventArgs e)
        {
            if (this.GetCurrentColumnValue("NoiTinh") != null)
            {
                string noitinh = this.GetCurrentColumnValue("NoiTinh").ToString();
                switch (noitinh)
                {
                    case "1":
                        if (SLAdungtuyen.Value != null && SLAdungtuyen.ToString() != "")
                        {
                            if (SLAtraituyen.Value != null && SLAtraituyen.Value.ToString()!="")
                            {
                                colSLGF2.Text = (Convert.ToInt32(SLAdungtuyen.Value) + Convert.ToInt32(SLAtraituyen.Value)).ToString();
                            }
                            else
                            {
                                colSLGF2.Text =SLAdungtuyen.Value.ToString();
                            }
                        }
                        else
                        {
                            if (SLAtraituyen.Value != null && SLAtraituyen.Value.ToString() != "")
                            {
                                colSLGF2.Text = SLAtraituyen.Value.ToString();
                            }
                        } 
                        break;
                    case "2":
                        if (SLBdungtuyen.Value != null && SLBdungtuyen.Value.ToString()!="")
                        {
                            if (SLBTraituyen.Value != null && SLBTraituyen.Value.ToString()!="")
                            {
                                colSLGF2.Text = (Convert.ToInt32(SLBdungtuyen.Value) + Convert.ToInt32(SLBTraituyen.Value)).ToString();
                            }
                            else
                            {
                                colSLGF2.Text = SLBdungtuyen.Value.ToString();
                            }
                        }
                        else
                        {
                            if (SLBTraituyen.Value != null && SLBTraituyen.Value.ToString()!="")
                            {
                                colSLGF2.Text = SLBTraituyen.Value.ToString();
                            }
                        }
                        break;
                    case "3":
                        if (SLCdungtuyen.Value != null && SLCdungtuyen.Value.ToString()!="")
                        {
                            if (SLCTraituyen.Value != null && SLCdungtuyen.Value.ToString()!="")
                            {
                                colSLGF2.Text = (Convert.ToInt32(SLCdungtuyen.Value) + Convert.ToInt32(SLCTraituyen.Value)).ToString();
                            }
                            else
                            {
                                colSLGF2.Text = SLCdungtuyen.Value.ToString();
                            }
                        }
                        else
                        {
                            if (SLCTraituyen.Value != null && SLCTraituyen.Value.ToString()!="")
                            {
                                colSLGF2.Text = SLCTraituyen.Value.ToString();
                            }
                        }
                        break;
                }
            }

        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            if (_dt != "BHYT") {
                GroupFooter1.Visible = false;
                GroupFooter2.Visible = false;
                GroupHeader1.Visible = false;
                GroupHeader2.Visible = false;
                txtTieuDe.Text = "TỔNG HỢP CHI PHÍ KHÁM CHỮA BỆNH NGOÀI BHYT NGOẠI TRÚ";
                txtSoTien.Text = "Tổng số tiền thanh toán (viết bằng chữ): ";
            }
        }

      
    }
}
