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
    public partial class rep79aCT : DevExpress.XtraReports.UI.XtraReport
    {
        List<BNKB> _bnkb = new List<BNKB>();
        string _dt = "BHYT";
        public rep79aCT()
        {
            InitializeComponent();
            QLBV_Database.QLBVEntities _dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            List<BNKB> bnkb = new List<BNKB>();
            bnkb = _dataContext.BNKBs.ToList();
            _bnkb = bnkb;
        }
        public rep79aCT(string dtuong)
        {
            InitializeComponent();
            QLBV_Database.QLBVEntities _dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            List<BNKB> bnkb = new List<BNKB>();
            bnkb = _dataContext.BNKBs.ToList();
            _bnkb = bnkb;
            _dt = dtuong;
        }
        public void BindingData()
        {
            colCDHA.DataBindings.Add("Text", DataSource, "CDHA").FormatString = DungChung.Bien.FormatString[1];
            colCDHAGF1.DataBindings.Add("Text", DataSource, "CDHA").FormatString = DungChung.Bien.FormatString[1];
            colCDHAGF2.DataBindings.Add("Text", DataSource, "CDHA").FormatString = DungChung.Bien.FormatString[1];
            colCDHARF.DataBindings.Add("Text", DataSource, "CDHA").FormatString = DungChung.Bien.FormatString[1];
            colcongkham1.DataBindings.Add("Text", DataSource, "Congkham").FormatString = DungChung.Bien.FormatString[1];
            colcongkham2.DataBindings.Add("Text", DataSource, "Congkham").FormatString = DungChung.Bien.FormatString[1];
            colcongkham3.DataBindings.Add("Text", DataSource, "Congkham").FormatString = DungChung.Bien.FormatString[1];
            colcongkham4.DataBindings.Add("Text", DataSource, "Congkham").FormatString = DungChung.Bien.FormatString[1];
            colCPNgoaiBHYT.DataBindings.Add("Text", DataSource, "CPNgoaiBH").FormatString = DungChung.Bien.FormatString[1];
            colCPNgoaiBHYTGF1.DataBindings.Add("Text", DataSource, "CPNgoaiBH").FormatString = DungChung.Bien.FormatString[1];
            colCPNgoaiBHYTGF2.DataBindings.Add("Text", DataSource, "CPNgoaiBH").FormatString = DungChung.Bien.FormatString[1];
            colCPNgoaiBHYTRF.DataBindings.Add("Text", DataSource, "CPNgoaiBH").FormatString = DungChung.Bien.FormatString[1];
            //colcongkham1.DataBindings.Add("Text", DataSource, "CPVanchuyen").FormatString = DungChung.Bien.FormatString[1];
            //colcongkham2.DataBindings.Add("Text", DataSource, "CPVanchuyen").FormatString = DungChung.Bien.FormatString[1];
            //colcongkham3.DataBindings.Add("Text", DataSource, "CPVanchuyen").FormatString = DungChung.Bien.FormatString[1];
            //colcongkham4.DataBindings.Add("Text", DataSource, "CPVanchuyen").FormatString = DungChung.Bien.FormatString[1];
            colDVKTC.DataBindings.Add("Text", DataSource, "VTTT").FormatString = DungChung.Bien.FormatString[1];
            colDVKTCGF2.DataBindings.Add("Text", DataSource, "VTTT").FormatString = DungChung.Bien.FormatString[1];
            colDVKTGF1.DataBindings.Add("Text", DataSource, "VTTT").FormatString = DungChung.Bien.FormatString[1];
            colDVKTCRF.DataBindings.Add("Text", DataSource, "VTTT").FormatString = DungChung.Bien.FormatString[1];
            txtGioitinh.DataBindings.Add("Text", DataSource, "GTinh");
            colHotenBN.DataBindings.Add("Text", DataSource, "TenBNhan");
            colMabenh.DataBindings.Add("Text", DataSource, "MaICD");
            colMaCS.DataBindings.Add("Text", DataSource, "MaCS");
            colMau.DataBindings.Add("Text", DataSource, "Mau").FormatString = DungChung.Bien.FormatString[1];
            colMauGF1.DataBindings.Add("Text", DataSource, "Mau").FormatString = DungChung.Bien.FormatString[1];
            colMauGF2.DataBindings.Add("Text", DataSource, "Mau").FormatString = DungChung.Bien.FormatString[1];
            colMauRF.DataBindings.Add("Text", DataSource, "Mau").FormatString = DungChung.Bien.FormatString[1];
            colMStheBHYT.DataBindings.Add("Text", DataSource, "SThe");
            txtNamsinh.DataBindings.Add("Text", DataSource, "NSinh");
            colNgaykham.DataBindings.Add("Text", DataSource, "Ngaykham").FormatString = "{0:dd/MM/yy}";
            colNguoibenhchitra.DataBindings.Add("Text", DataSource, "TienBN").FormatString = DungChung.Bien.FormatString[1];
            colNguoibenhchitraRF.DataBindings.Add("Text", DataSource, "TienBN").FormatString = DungChung.Bien.FormatString[1];
            colNguoibenhchitraGF1.DataBindings.Add("Text", DataSource, "TienBN").FormatString = DungChung.Bien.FormatString[1];
            colNguoibenhchitraGF2.DataBindings.Add("Text", DataSource, "TienBN").FormatString = DungChung.Bien.FormatString[1];
            //colTennhomGF2.DataBindings.Add("Text", DataSource, "Tennhom");
            colThuoc.DataBindings.Add("Text", DataSource, "Thuoc").FormatString = DungChung.Bien.FormatString[1];
            colThuocGF1.DataBindings.Add("Text", DataSource, "Thuoc").FormatString = DungChung.Bien.FormatString[1];
            colThuocGF2.DataBindings.Add("Text", DataSource, "Thuoc").FormatString = DungChung.Bien.FormatString[1];
            colThuocRF.DataBindings.Add("Text", DataSource, "Thuoc").FormatString = DungChung.Bien.FormatString[1];
            colCongkhamGF1.DataBindings.Add("Text", DataSource, "ThuocKCTG").FormatString = DungChung.Bien.FormatString[1];
            colCongkhamGF2.DataBindings.Add("Text", DataSource, "ThuocKTCG").FormatString = DungChung.Bien.FormatString[1];
            colCongkhamRF.DataBindings.Add("Text", DataSource, "ThuocKCTG").FormatString = DungChung.Bien.FormatString[1];
            colThuocKCTGRF.DataBindings.Add("Text", DataSource, "ThuocKCTG").FormatString = DungChung.Bien.FormatString[1];
            colTongchi.DataBindings.Add("Text", DataSource, "CPVanchuyen").FormatString = DungChung.Bien.FormatString[1];
            colTongchiGF1.DataBindings.Add("Text", DataSource, "CPVanchuyen").FormatString = DungChung.Bien.FormatString[1];
            colTongchiGF2.DataBindings.Add("Text", DataSource, "CPVanchuyen").FormatString = DungChung.Bien.FormatString[1];
            colTongchiRF.DataBindings.Add("Text", DataSource, "CPVanchuyen").FormatString = DungChung.Bien.FormatString[1];
            colTongcong.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];
            colTongcongBHYT.DataBindings.Add("Text", DataSource, "TienBH").FormatString = DungChung.Bien.FormatString[1];
            colTongcongBHYTGF1.DataBindings.Add("Text", DataSource, "TienBH").FormatString = DungChung.Bien.FormatString[1];
            colTongcongBHYTGF2.DataBindings.Add("Text", DataSource, "TienBH").FormatString = DungChung.Bien.FormatString[1];
            colTongcongBHYTRF.DataBindings.Add("Text", DataSource, "TienBH").FormatString = DungChung.Bien.FormatString[1];
            colTongcongGF1.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];
            colTongcongGF2.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];
            colTongcongRF2.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];
            colTTPT.DataBindings.Add("Text", DataSource, "TTPT").FormatString = DungChung.Bien.FormatString[1];
            colTTPTGF1.DataBindings.Add("Text", DataSource, "TTPT").FormatString = DungChung.Bien.FormatString[1];
            colTTPTGF2.DataBindings.Add("Text", DataSource, "TTPT").FormatString = DungChung.Bien.FormatString[1];
            colTTPTRF.DataBindings.Add("Text", DataSource, "TTPT").FormatString = DungChung.Bien.FormatString[1];
            colTuyen.DataBindings.Add("Text", DataSource, "Tuyen").FormatString = DungChung.Bien.FormatString[1];
            colThuocKCTGGF1.DataBindings.Add("Text", DataSource, "TTG").FormatString = DungChung.Bien.FormatString[1];
            colThuocKCTGGF2.DataBindings.Add("Text", DataSource, "TTG").FormatString = DungChung.Bien.FormatString[1];
            colThuocKCTGRF.DataBindings.Add("Text", DataSource, "TTG").FormatString = DungChung.Bien.FormatString[1];
            //colcongkham1.DataBindings.Add("Text", DataSource, "Vanchuyen").FormatString = DungChung.Bien.FormatString[1];
            //colcongkham2.DataBindings.Add("Text", DataSource, "Vanchuyen").FormatString = DungChung.Bien.FormatString[1];
            //colcongkham3.DataBindings.Add("Text", DataSource, "Vanchuyen").FormatString = DungChung.Bien.FormatString[1];
            //colcongkham4.DataBindings.Add("Text", DataSource, "Vanchuyen").FormatString = DungChung.Bien.FormatString[1];
            colVTYT.DataBindings.Add("Text", DataSource, "VTYTH").FormatString = DungChung.Bien.FormatString[1];
            colVTYTGF1.DataBindings.Add("Text", DataSource, "VTYTH").FormatString = DungChung.Bien.FormatString[1];
            colVTYTGF2.DataBindings.Add("Text", DataSource, "VTYTH").FormatString = DungChung.Bien.FormatString[1];
            colVTYTRF.DataBindings.Add("Text", DataSource, "VTYTH").FormatString = DungChung.Bien.FormatString[1];
            colxetnghiem.DataBindings.Add("Text", DataSource, "xetnghiem").FormatString = DungChung.Bien.FormatString[1];
            ColXetnghiemGF1.DataBindings.Add("Text", DataSource, "xetnghiem").FormatString = DungChung.Bien.FormatString[1];
            colXetnghiemGF2.DataBindings.Add("Text", DataSource, "xetnghiem").FormatString = DungChung.Bien.FormatString[1];
            colXetnghiemRF.DataBindings.Add("Text", DataSource, "xetnghiem").FormatString = DungChung.Bien.FormatString[1];
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
                        colTennhomGF2.Text=" Cộng: A";
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
        //int sttg2 = 1;
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
                //int tt;
                //if (tuyen == 1)
                //{ tt = 0; }
                //else
                //{ tt = 1; }
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
        }
        private void colGiotinh_BeforePrint(object sender, CancelEventArgs e)
        {
        //    if (colGiotinh.Text == "1")
        //    {
        //        colGiotinh.Text = "Nam";
        //    }
        //    else
        //    {
        //        colGiotinh.Text = "Nữ";
        //    }
        }
        //private string MaICD(string _Mabn)
        //{
        //    //int i = q.Count;
        //    string ICD = "";
        //    if (_bnkb.Where(p => p.MaBNhan == _Mabn).OrderByDescending(p => p.IDKB).First().MaICD.ToList().Count > 0)
        //    {
        //        ICD = _bnkb.Where(p => p.MaBNhan == _Mabn).OrderByDescending(p => p.IDKB).Skip(0).Take(1).First().MaICD.ToString();
        //    }
        //    //MessageBox.Show(ICD);
        //    return ICD;
        //}
        //private void colMabenh_BeforePrint(object sender, CancelEventArgs e)
        //{
        //    if (this.GetCurrentColumnValue("Mabn") != null)
        //    {
        //        colMabenh.Text = MaICD(txtMabn.Text);
        //    }
        //}

        private void colNgaykham_BeforePrint(object sender, CancelEventArgs e)
        {
            //if (this.GetCurrentColumnValue("Ngaykham") != null)
            //{
            //    colNgaykham.Text = txtngaykham.Text.ToString().Substring(0, 5);
            //}
        }
        private void xrLabel28_BeforePrint(object sender, CancelEventArgs e)
        {
            Double st = Convert.ToDouble(SotienDKTT.Value.ToString());
            //MessageBox.Show(st.ToString());
            st = Math.Round(st, 0);
            sotien.Text = DungChung.Ham.DocTienBangChu(st, " đồng./");
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            if (this.GetCurrentColumnValue("GTinh") != null && this.GetCurrentColumnValue("NSinh") != null)
            {
                int gt = Convert.ToInt32(this.GetCurrentColumnValue("GTinh").ToString());
                string ns = this.GetCurrentColumnValue("NSinh").ToString();
                if (gt == 1)
                {
                    colNamsinh.Text = ns;
                    colGiotinh.Text = "";
                }
                else
                {
                    if (gt == 0)
                    {
                        colNamsinh.Text = "";
                        colGiotinh.Text = ns;
                    }
                    else
                    {
                        colNamsinh.Text = "";
                        colGiotinh.Text = "";
                    }
                }
            }
            else {
                colNamsinh.Text = "";
                colGiotinh.Text = "";
            }
        }

        private void colMStheBHYT_BeforePrint(object sender, CancelEventArgs e)
        {
            if (GetCurrentColumnValue("SThe") != null && GetCurrentColumnValue("SThe").ToString().Length == 2)
            {
                QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                string _madt = GetCurrentColumnValue("SThe").ToString();
                var nhom = _data.DTuongs.Where(p => p.MaDTuong == _madt).Select(p => p.Nhom).ToList();
                if (nhom.Count > 0)
                    colMStheBHYT.Text = nhom.First().ToString();
            }
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            if (_dt != "BHYT")
            {
                GroupFooter1.Visible = false;
                GroupFooter2.Visible = false;
                GroupHeader1.Visible = false;
                GroupHeader2.Visible = false;
                txtTieuDe.Text = "DANH SÁCH NGƯỜI BỆNH NHÂN DÂN KHÁM CHỮA BỆNH NGOẠI TRÚ";
                txtSoTien.Text = "Tổng số tiền thanh toán (viết bằng chữ): ";
            }
        }

    }
}
