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
    public partial class repCTTonThucSDNoiTru : DevExpress.XtraReports.UI.XtraReport
    {
        public repCTTonThucSDNoiTru()
        {
            InitializeComponent();
        }
        List<QLBV.FormThamSo.frm_CTTonThucSDNoiTru.KhoaPhong> _lKP = new List<QLBV.FormThamSo.frm_CTTonThucSDNoiTru.KhoaPhong>();
        public repCTTonThucSDNoiTru( List<QLBV.FormThamSo.frm_CTTonThucSDNoiTru.KhoaPhong> _q2)
        {
            InitializeComponent();
            _lKP = _q2;
        }
        public void BindingData() {
            ColTenThuoc.DataBindings.Add("Text", DataSource, "TenDV");
            colDonVi.DataBindings.Add("Text", DataSource, "DonVi");
            //colSoLuong01.DataBindings.Add("Text", DataSource, "SoLuong").FormatString = DungChung.Bien.FormatString[1];

            colSLK1.DataBindings.Add("Text", DataSource, "sl1").FormatString = DungChung.Bien.FormatString[1];
            colTTK1.DataBindings.Add("Text", DataSource, "tt1").FormatString = DungChung.Bien.FormatString[1];
            colSLK2.DataBindings.Add("Text", DataSource, "sl2").FormatString = DungChung.Bien.FormatString[1];
            colTTK2.DataBindings.Add("Text", DataSource, "tt2").FormatString = DungChung.Bien.FormatString[1];
            colSLK3.DataBindings.Add("Text", DataSource, "sl3").FormatString = DungChung.Bien.FormatString[1];
            colTTK3.DataBindings.Add("Text", DataSource, "tt3").FormatString = DungChung.Bien.FormatString[1];
            colSLK4.DataBindings.Add("Text", DataSource, "sl4").FormatString = DungChung.Bien.FormatString[1];
            colTTK4.DataBindings.Add("Text", DataSource, "tt4").FormatString = DungChung.Bien.FormatString[1];
            colSLK5.DataBindings.Add("Text", DataSource, "sl5").FormatString = DungChung.Bien.FormatString[1];
            colTTK5.DataBindings.Add("Text", DataSource, "tt5").FormatString = DungChung.Bien.FormatString[1];
            colSLK6.DataBindings.Add("Text", DataSource, "sl6").FormatString = DungChung.Bien.FormatString[1];
            colTTK6.DataBindings.Add("Text", DataSource, "tt6").FormatString = DungChung.Bien.FormatString[1];

            colSLK7.DataBindings.Add("Text", DataSource, "sl7").FormatString = DungChung.Bien.FormatString[1];
            colTTK7.DataBindings.Add("Text", DataSource, "tt7").FormatString = DungChung.Bien.FormatString[1];

            colSLK8.DataBindings.Add("Text", DataSource, "sl8").FormatString = DungChung.Bien.FormatString[1];
            colTTK8.DataBindings.Add("Text", DataSource, "tt8").FormatString = DungChung.Bien.FormatString[1];
            colGrTTK1.DataBindings.Add("Text", DataSource, "tt1").FormatString = DungChung.Bien.FormatString[1];
            colGrTTK2.DataBindings.Add("Text", DataSource, "tt2").FormatString = DungChung.Bien.FormatString[1];
            colGrTTK3.DataBindings.Add("Text", DataSource, "tt3").FormatString = DungChung.Bien.FormatString[1];
            colGrTTK4.DataBindings.Add("Text", DataSource, "tt4").FormatString = DungChung.Bien.FormatString[1];
            colGrTTK5.DataBindings.Add("Text", DataSource, "tt5").FormatString = DungChung.Bien.FormatString[1];
            colGrTTK6.DataBindings.Add("Text", DataSource, "tt6").FormatString = DungChung.Bien.FormatString[1];
            colGrTTK7.DataBindings.Add("Text", DataSource, "tt7").FormatString = DungChung.Bien.FormatString[1];
            colGrTTK8.DataBindings.Add("Text", DataSource, "tt8").FormatString = DungChung.Bien.FormatString[1];
            colTongCong1.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];
            colGrTongCong.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];
            colDonGia.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[1];
        }

        int soTrang = 0;
        private void PageHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            soTrang++;
            lbTrang.Text = "trang " + soTrang;
            int colNum= 0;
            foreach (var kp in _lKP)
	        {
                if (colNum == 0 && kp.TenKP != "")
                {
                    DatTenCot(colKhoa1, kp.TenKP, colSL1, colTT1);
                }
                if (colNum == 1 && kp.TenKP != "")
                {
                    DatTenCot(colKhoa2, kp.TenKP, colSL2, colTT2);
                }
                if (colNum == 2 && kp.TenKP != "")
                {
                    DatTenCot(colKhoa3, kp.TenKP, colSL3, colTT3);
                }
                if (colNum == 3 && kp.TenKP != "")
                {
                    DatTenCot(colKhoa4, kp.TenKP, colSL4, colTT4);
                }
                if (colNum == 4 && kp.TenKP != "")
                {
                    DatTenCot(colKhoa5, kp.TenKP, colSL5, colTT5);
                    
                }
                if (colNum == 5 && kp.TenKP != "")
                {
                    DatTenCot(colKhoa6, kp.TenKP, colSL6, colTT6);
                }
                if (colNum == 6 && kp.TenKP != "")
                {
                    DatTenCot(colKhoa7, kp.TenKP, colSL7, colTT7);
                }
                if (colNum == 7 && kp.TenKP != "")
                {
                    DatTenCot(colKhoa8, kp.TenKP, colSL8, colTT8);
                }
                colNum++;
            }
        }
        public void DatTenCot(XRTableCell colKhoa,string tenKhoa, XRTableCell colSL, XRTableCell colTT) {
            colKhoa.Text = tenKhoa;
            colSL.Text = "SL";
            colTT.Text = "T. Tiền";
        }
    }
}
