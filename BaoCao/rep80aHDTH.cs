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
    public partial class rep80aHDTH : DevExpress.XtraReports.UI.XtraReport
    {
        List<BNKB> _bnkb = new List<BNKB>();

        public rep80aHDTH()
        {
            InitializeComponent();
            QLBV_Database.QLBVEntities _dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            List<BNKB> bnkb = new List<BNKB>();
            bnkb =  _dataContext.BNKBs.ToList();
            _bnkb = bnkb;
        }
        public void BindingData()
        {
            
            colHoten.DataBindings.Add("Text", DataSource, "TenBNhan");
            colNamsinhnam.DataBindings.Add("Text", DataSource, "NSinh");
            colNamsinhnu.DataBindings.Add("Text", DataSource, "NSinh");
            colMathe.DataBindings.Add("Text", DataSource, "SThe");
            colMacoso.DataBindings.Add("Text", DataSource, "MaCS");
            colMabenh.DataBindings.Add("Text", DataSource, "MaICD");
            txtngayvao.DataBindings.Add("Text", DataSource, "Ngayvao");
            txtngayra.DataBindings.Add("Text", DataSource, "Ngayra");
            colTongngayDT.DataBindings.Add("Text", DataSource, "Songay");
            xrTableCell14.DataBindings.Add("Text", DataSource, "Songay");
            xrTableCell16.DataBindings.Add("Text", DataSource, "Songay");
            xrTableCell17.DataBindings.Add("Text", DataSource, "Songay");
            colTongcong.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];
            colXetnghiem.DataBindings.Add("Text", DataSource, "xetnghiem").FormatString = DungChung.Bien.FormatString[1];
            colCDHA.DataBindings.Add("Text", DataSource, "CDHA").FormatString = DungChung.Bien.FormatString[1];
            colThuoc.DataBindings.Add("Text", DataSource, "Thuoc").FormatString = DungChung.Bien.FormatString[1];
            colMau.DataBindings.Add("Text", DataSource, "Mau").FormatString = DungChung.Bien.FormatString[1];
            colTTPT.DataBindings.Add("Text", DataSource, "TTPT").FormatString = DungChung.Bien.FormatString[1];
            colCPNgoaiGH1.DataBindings.Add("Text", DataSource, "CPVanchuyen").FormatString = DungChung.Bien.FormatString[1];
            colVTYT.DataBindings.Add("Text", DataSource, "VTYT").FormatString = DungChung.Bien.FormatString[1];
            colVTYTGH1.DataBindings.Add("Text", DataSource, "VTYT").FormatString = DungChung.Bien.FormatString[1];
            colDVKTC.DataBindings.Add("Text", DataSource, "DVKTC").FormatString = DungChung.Bien.FormatString[1];
            colThuocKCTG.DataBindings.Add("Text", DataSource, "ThuocKCTG").FormatString = DungChung.Bien.FormatString[1];
            colThuocKCTGGH1.DataBindings.Add("Text",DataSource,"ThuocKCTG").FormatString = DungChung.Bien.FormatString[1];
            colCPVanchuyen.DataBindings.Add("Text", DataSource, "Congkham").FormatString = DungChung.Bien.FormatString[1];
            colTiengiuongGH1.DataBindings.Add("Text", DataSource, "ThuocKCTG").FormatString = DungChung.Bien.FormatString[1];
            colDVKTCGH1.DataBindings.Add("Text", DataSource, "VTTT").FormatString = DungChung.Bien.FormatString[1];
            colTongchi.DataBindings.Add("Text", DataSource, "CPVanchuyen").FormatString = DungChung.Bien.FormatString[1];
            colNguoibenhchitra.DataBindings.Add("Text", DataSource, "Nguoibenhchitra").FormatString = DungChung.Bien.FormatString[1];
            colTongcongBHYT.DataBindings.Add("Text", DataSource, "TongcongBHYT").FormatString = DungChung.Bien.FormatString[1];
            colCPNgoai.DataBindings.Add("Text", DataSource, "CPVanchuyen").FormatString = DungChung.Bien.FormatString[1];
            colCDHAGF1.DataBindings.Add("Text", DataSource, "CDHA").FormatString = DungChung.Bien.FormatString[1];
            colCDHAGH1.DataBindings.Add("Text", DataSource, "CDHA").FormatString = DungChung.Bien.FormatString[1];
            colCPNgoaiGF1.DataBindings.Add("Text", DataSource, "CPVanchuyen").FormatString = DungChung.Bien.FormatString[1];
            colCPVanchuyenGF1.DataBindings.Add("Text", DataSource, "Congkham").FormatString = DungChung.Bien.FormatString[1];
            colDVKTCGF1.DataBindings.Add("Text", DataSource, "DVKTC").FormatString = DungChung.Bien.FormatString[1];
            colMauGF1.DataBindings.Add("Text", DataSource, "Mau").FormatString = DungChung.Bien.FormatString[1];
            colMauGH1.DataBindings.Add("Text", DataSource, "Mau").FormatString = DungChung.Bien.FormatString[1];
            colNguoibenhcungchitraGF1.DataBindings.Add("Text", DataSource, "Nguoibenhchitra").FormatString = DungChung.Bien.FormatString[1];
            colTiengiuongGF1.DataBindings.Add("Text", DataSource, "Tiengiuong").FormatString = DungChung.Bien.FormatString[1];
            colTongcongBHYTGF1.DataBindings.Add("Text", DataSource, "TongcongBHYT").FormatString = DungChung.Bien.FormatString[1];
            colTongcongGF1.DataBindings.Add("Text", DataSource, "Tongcong").FormatString = DungChung.Bien.FormatString[1];
            colTongcongGH1.DataBindings.Add("Text", DataSource, "Tongcong").FormatString = DungChung.Bien.FormatString[1];
            colTongchiGF1.DataBindings.Add("Text", DataSource, "CPVanchuyen").FormatString = DungChung.Bien.FormatString[1];
            colTTPTGF1.DataBindings.Add("Text", DataSource, "TTPT").FormatString = DungChung.Bien.FormatString[1];
            colTTPTGH1.DataBindings.Add("Text", DataSource, "TTPT").FormatString = DungChung.Bien.FormatString[1];
            colThuocGF1.DataBindings.Add("Text", DataSource, "Thuoc").FormatString = DungChung.Bien.FormatString[1];
            colThuocGH1.DataBindings.Add("Text", DataSource, "Thuoc").FormatString = DungChung.Bien.FormatString[1];
            colThuocKCTGGF1.DataBindings.Add("Text", DataSource, "ThuocKCTG").FormatString = DungChung.Bien.FormatString[1];
            colVTYTGF1.DataBindings.Add("Text", DataSource, "VTYT").FormatString = DungChung.Bien.FormatString[1];
            colXetnghiemGF1.DataBindings.Add("Text", DataSource, "Xetnghiem").FormatString = DungChung.Bien.FormatString[1];
            colXetnghiemGH1.DataBindings.Add("Text", DataSource, "Xetnghiem").FormatString = DungChung.Bien.FormatString[1];
            colCDHARF1.DataBindings.Add("Text", DataSource, "CDHA").FormatString = DungChung.Bien.FormatString[1];
            colCPNgoaiRF1.DataBindings.Add("Text", DataSource, "CPVanchuyen").FormatString = DungChung.Bien.FormatString[1];
            colCPVanchuyenRF1.DataBindings.Add("Text", DataSource, "Congkham").FormatString = DungChung.Bien.FormatString[1];
            colDVKTCRF1.DataBindings.Add("Text", DataSource, "DVKTC").FormatString = DungChung.Bien.FormatString[1];
            colMauRF1.DataBindings.Add("Text", DataSource, "Mau").FormatString = DungChung.Bien.FormatString[1];
            colNguoibenhcungchitraRF1.DataBindings.Add("Text", DataSource, "Nguoibenhchitra").FormatString = DungChung.Bien.FormatString[1];
            //colTenRF1.DataBindings.Add("Text", DataSource, "Ten").FormatString = DungChung.Bien.FormatString[1];
            colTiengiuongRF1.DataBindings.Add("Text", DataSource, "Tiengiuong").FormatString = DungChung.Bien.FormatString[1];
            colTongcongBHYTRF11.DataBindings.Add("Text", DataSource, "TongcongBHYT").FormatString = DungChung.Bien.FormatString[1];
            colTongcongRF.DataBindings.Add("Text", DataSource, "Tongcong").FormatString = DungChung.Bien.FormatString[1];
            colTongchiRF1.DataBindings.Add("Text", DataSource, "CPVanchuyen").FormatString = DungChung.Bien.FormatString[1];
            colTTPTRF1.DataBindings.Add("Text", DataSource, "TTPT").FormatString = DungChung.Bien.FormatString[1];
            colThuocKCTGRF1.DataBindings.Add("Text", DataSource, "ThuocKCTG").FormatString = DungChung.Bien.FormatString[1];
            colThuocRF1.DataBindings.Add("Text", DataSource, "Thuoc").FormatString = DungChung.Bien.FormatString[1];
            colVTYTRF1.DataBindings.Add("Text", DataSource, "VTYT").FormatString = DungChung.Bien.FormatString[1];
            colXetnghiemRF1.DataBindings.Add("Text", DataSource, "Xetnghiem").FormatString = DungChung.Bien.FormatString[1];
            colNamnu.DataBindings.Add("Text", DataSource, "Nam").FormatString = DungChung.Bien.FormatString[1];
            colGF2CDHA.DataBindings.Add("Text", DataSource, "CDHA").FormatString = DungChung.Bien.FormatString[1];
            colGF2CPNgoai.DataBindings.Add("Text", DataSource, "CPVanchuyen").FormatString = DungChung.Bien.FormatString[1];
            colGF2CPVanchuyen.DataBindings.Add("Text", DataSource, "Congkham").FormatString = DungChung.Bien.FormatString[1];
            colCPvanchuyenGH1.DataBindings.Add("Text", DataSource, "Congkham").FormatString = DungChung.Bien.FormatString[1];
            colGF2DVKTC.DataBindings.Add("Text", DataSource, "DVKTC").FormatString = DungChung.Bien.FormatString[1];
            colGF2Mau.DataBindings.Add("Text", DataSource, "Mau").FormatString = DungChung.Bien.FormatString[1];
            colGF2Nguoibenhcungchitra.DataBindings.Add("Text", DataSource, "Nguoibenhchitra").FormatString = DungChung.Bien.FormatString[1];
            colNguoibenhchitraGH1.DataBindings.Add("Text", DataSource, "Nguoibenhchitra").FormatString = DungChung.Bien.FormatString[1];
            colGF2Tiengiuong.DataBindings.Add("Text", DataSource, "Tiengiuong").FormatString = DungChung.Bien.FormatString[1];
            colGF2Tongcong.DataBindings.Add("Text", DataSource, "Tongcong").FormatString = DungChung.Bien.FormatString[1];
            colGF2TongcongBHYT.DataBindings.Add("Text", DataSource, "TongcongBHYT").FormatString = DungChung.Bien.FormatString[1];
            colTongcongBHYTGH1.DataBindings.Add("Text", DataSource, "TongcongBHYT").FormatString = DungChung.Bien.FormatString[1];
            colGF2Tongchi.DataBindings.Add("Text", DataSource, "CPVanchuyen").FormatString = DungChung.Bien.FormatString[1];
            colTongchiGH1.DataBindings.Add("Text", DataSource, "CPVanchuyen").FormatString = DungChung.Bien.FormatString[1];
            colGF2TTPT.DataBindings.Add("Text", DataSource, "TTPT").FormatString = DungChung.Bien.FormatString[1];
            colGF2Thuoc.DataBindings.Add("Text", DataSource, "Thuoc").FormatString = DungChung.Bien.FormatString[1];
            colGF2ThuocKCTG.DataBindings.Add("Text", DataSource, "ThuocKCTG").FormatString = DungChung.Bien.FormatString[1];
            colGF2VTYT.DataBindings.Add("Text", DataSource, "VTYT").FormatString = DungChung.Bien.FormatString[1];
            colGF2xetnghiem.DataBindings.Add("Text", DataSource, "Xetnghiem").FormatString = DungChung.Bien.FormatString[1];
            txtNoiTinh.DataBindings.Add("Text", DataSource, "NoiTinh");
            txtMabn.DataBindings.Add("Text", DataSource, "MaBNhan");
            GroupHeader2.GroupFields.Add(new GroupField("NoiTinh"));
            GroupHeader1.GroupFields.Add(new GroupField("Tuyen"));
           


        }
        string tongcong = " Tổng cộng ";
        int sttg2 = 1;

     //   int sttg2 = 1;
        private void GroupHeader2_BeforePrint(object sender, CancelEventArgs e)
        {
            if (this.GetCurrentColumnValue("NoiTinh") != null)
            {
                string noitinh = this.GetCurrentColumnValue("NoiTinh").ToString();
                switch (noitinh)
                {
                    case "1":
                        colGH2Ten.Text = " Bệnh nhân nội tỉnh kcb ban đầu".ToUpper();
                        colGH2Muc.Text = "A";
                        tongcong += "A";
                        colGF2Ten.Text = " Cộng: A";
                        break;
                    case "2":
                        colGH2Ten.Text = " Bệnh nhân nội tỉnh đến".ToUpper();
                        colGH2Muc.Text = "B";
                        tongcong += "+B";
                        colGF2Ten.Text = " Cộng: B";
                        break;
                    case "3":
                        colGH2Ten.Text = " Bệnh nhân ngoại tỉnh đến".ToUpper();
                        colGH2Muc.Text = "C";
                        tongcong += "+C";
                        colGF2Ten.Text = " Cộng: C";
                        break;
                }

            }
        }
        private void GroupHeader1_BeforePrint(object sender, CancelEventArgs e)
        {
            if(this.GetCurrentColumnValue("Tuyen")!=null)
            {
            sttg2 = int.Parse(this.GetCurrentColumnValue("Tuyen").ToString().Trim());
            if (sttg2 == 1)
            {
                colmuc.Text = "I";
            }
            else
            {
                if (sttg2 == 2)
                {
                    colmuc.Text = "II";
                }
            }
            }
            if (this.GetCurrentColumnValue("Tuyen") != null)
            {
                int tuyen = int.Parse(this.GetCurrentColumnValue("Tuyen").ToString().Trim());
                if (tuyen == 1)
                {
                    colTennhomGH1.Text = " Đúng tuyến";
                    colTennhomGF1.Text = " Cộng đúng tuyến";
                }
                if (tuyen == 2)
                {
                    colTennhomGH1.Text = " Trái tuyến";
                    colTennhomGF1.Text = " Cộng trái tuyến";
                }
            }
        }

        string rong="";
        private void colNamsinhnam_BeforePrint(object sender, CancelEventArgs e)
        {
            if (colNamnu.Text == "1")
            {
                colNamsinhnu.Text = rong;
            }
            else
            {
                colNamsinhnam.Text = rong;
            }
        }

        private void colMathe_BeforePrint(object sender, CancelEventArgs e)
        {

        }
        private string MaICD(int _Mabn)
        {


           // int i = q.Count;
            string ICD = "";
            if (_bnkb.Where(p => p.MaBNhan == _Mabn).OrderByDescending(p => p.MaICD).First().MaICD.ToList().Count > 0)
            {
                ICD = _bnkb.Where(p => p.MaBNhan == _Mabn).OrderByDescending(p => p.MaICD).First().MaICD.ToString();
            }
            //MessageBox.Show(ICD);
            return ICD;

        }
        private void colMabenh_BeforePrint(object sender, CancelEventArgs e)
        {
            if (this.GetCurrentColumnValue("MaBNhan") != null)
            {

                colMabenh.Text = MaICD(Convert.ToInt32( txtMabn.Text));

            }
        }

        private void colNgayvao_BeforePrint(object sender, CancelEventArgs e)
        {
            if (this.GetCurrentColumnValue("Ngayvao") != null)
            {
                colNgayvao.Text = txtngayvao.Text.ToString().Substring(0, 5);
                colNgayra.Text = txtngayra.Text.ToString().Substring(0, 5);
            }
        }

        private void xrLabel28_BeforePrint(object sender, CancelEventArgs e)
        {
        }
        int st = 1;
        private void PageHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            txttrang.Text = st.ToString();
            st = st + 1;
            TenCQ.Value = DungChung.Bien.TenCQ.ToUpper();
        }
      
    }
}
