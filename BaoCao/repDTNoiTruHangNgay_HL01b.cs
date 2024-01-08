using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Data;
using System.Text;
using DevExpress.XtraEditors;
namespace QLBV.BaoCao
{
    public partial class repDTNoiTruHangNgay_HL01b : DevExpress.XtraReports.UI.XtraReport
    {
        public repDTNoiTruHangNgay_HL01b()
        {
            InitializeComponent();
        }
        public repDTNoiTruHangNgay_HL01b(List<THDV> _lth2, List<THDV> _lth3)
        {
            InitializeComponent();
            _lthdv2 = _lth2.ToList();
            _lthdv3 = _lth3.ToList();
        }
        List<THDV> _lthdv2 = new List<THDV>();
        List<THDV> _lthdv3 = new List<THDV>();
        public class THDV {
            public string tendv;
            public string tennhom;
            public double soluong;
            public int stt;
            public double dongia;
            public double thanhtien;
            public string ngaynhap;
            public string NgayNhap {
                set { ngaynhap = value; }
                get { return ngaynhap; }
            }
            public string TenNhomDV {
                set { tennhom = value; }
                get { return tennhom; }
            }
            public int STT {
                set { stt = value; }
                get { return stt; }
            }
            public string TenDV {
                set { tendv = value; }
                get { return tendv; }
            }
            public double SoLuong {
                set { soluong = value; }
                get { return soluong; }
            }
            public double DonGia {
                set { dongia = value; }
                get { return dongia; }
            }
            public double ThanhTien {
                set { thanhtien = value; }
                get { return thanhtien; }
            }
        }
       // repDTNoiTruHangNgay_HL01b rep = (repDTNoiTruHangNgay_HL01b)xrSubreport1.ReportSource;
        //public void BindingData()
        //{
        //    colTenNhomDV.DataBindings.Add("Text", DataSource, "TenNhomDV");
        //    colTenDV.DataBindings.Add("Text", DataSource, "TenDV");
        //    colSoLuong.DataBindings.Add("Text", DataSource, "SoLuongT").FormatString = DungChung.Bien.FormatString[1];
        //    colDonGia.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[1];
        //    colThanhTien.DataBindings.Add("Text", DataSource, "ThanhTienT").FormatString = DungChung.Bien.FormatString[1];
        //    colThanhTienTong.DataBindings.Add("Text", DataSource, "ThanhTienT").FormatString = DungChung.Bien.FormatString[1];
        //    GroupHeader1.GroupFields.Add(new GroupField("TenNhomDV"));

        //}

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            colNguoiLapBieu.Text = DungChung.Bien.NguoiLapBieu;
            colTruongKhoaDT.Text = DungChung.Bien.TruongKhoaLS;
            colKeToan.Text = DungChung.Bien.KeToanTruong;
            colBHXH.Text = DungChung.Bien.GiamDinhBH;
            xrTableCell16.Text = "GĐV BHXH TỈNH" + DungChung.Bien.DiaDanh;
            if (DungChung.Bien.MaBV == "04012")
            {
                xrTableCell16.Text = "Giám định BHYT";
            }
        }
        int i = 0;
        private void xrSubreport1_BeforePrint(object sender, CancelEventArgs e)
        {
            i++;
            QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
           // var q =(from a in _data.VienPhicts select new {SoLuongT= a.SoLuong,ThanhTienT= a.ThanhTien }).ToList();
            repDTNoiTruHangNgay_HL01b2 rep = (repDTNoiTruHangNgay_HL01b2)xrSubreport1.ReportSource;
                      // xrSubreport1.ReportSource =new repDTNoiTruHangNgay_HL01b2();
            rep.Ngay.Value = this.Ngay.Value;
                rep.DataSource = _lthdv2.ToList();
            rep.BindingData();
        }

        private void c_BeforePrint(object sender, CancelEventArgs e)
        {
            //repDTNoiTruHangNgay_HL01b2 rep = (repDTNoiTruHangNgay_HL01b2)c.ReportSource;
            //rep.DataSource = _lthdv.ToList();
        }

        private void xrSubreport2_BeforePrint(object sender, CancelEventArgs e)
        {
            repDTNoiTruHangNgay_HL01b3 rep = (repDTNoiTruHangNgay_HL01b3)xrSubreport2.ReportSource;
            rep.Ngay.Value=this.Ngay.Value;
            rep.DataSource = _lthdv3.ToList();
            rep.BindingData();
        }

        private void xrTableCell16_BeforePrint(object sender, CancelEventArgs e)
        {
          
        }
    }
}
