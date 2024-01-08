using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
using QLBV.FormThamSo;

namespace QLBV.BaoCao
{
    public partial class rep_BC_PhieuXuat_27022 : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_BC_PhieuXuat_27022()
        {
            InitializeComponent();

        }
        private int _PPX;
        public rep_BC_PhieuXuat_27022(int PPX)
        {
            InitializeComponent();
            this._PPX = PPX;
        }
        public void BindingData()
        {
            grKP.DataBindings.Add("Text", DataSource, "TenKP");

            colTenDV.DataBindings.Add("Text", DataSource, "TenDV");
            colThanhtien.DataBindings.Add("Text", DataSource, "ThanhTienX").FormatString = DungChung.Bien.FormatString[1];
            colsoluongtt.DataBindings.Add("Text", DataSource, "SoLuongX").FormatString = DungChung.Bien.FormatString[0];
            if (DungChung.Bien.MaBV == "27023" && _PPX == 1)
            {

                colsoluongyc.DataBindings.Add("Text", DataSource, "SoLuongX").FormatString = DungChung.Bien.FormatString[0];
            }
            colTongtienRep.DataBindings.Add("Text", DataSource, "ThanhTienX").FormatString = DungChung.Bien.FormatString[1];
            colGia.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[1];
            colDonvi.DataBindings.Add("Text", DataSource, "DonVi");
            colMaSo.DataBindings.Add("Text", DataSource, "MaTam");
            colThanhtienTenNhom.DataBindings.Add("Text", DataSource, "ThanhTienX").FormatString = DungChung.Bien.FormatString[1];
            colThanhtienTieuNhom.DataBindings.Add("Text", DataSource, "ThanhTienX").FormatString = DungChung.Bien.FormatString[1];

            GroupHeader3.GroupFields.Add(new GroupField("TenNhom"));
            grtennhom.DataBindings.Add("Text", DataSource, "TenNhom");
            GroupHeader2.GroupFields.Add(new GroupField("TenTieuNhom"));
            grtieunhom.DataBindings.Add("Text", DataSource, "TenTieuNhom");

            GroupHeader1.GroupFields.Add(new GroupField("TenKP"));
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        //int sttGh2 = 1;
        //int sttGh1 = 1;
        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            TenDV.Value = DungChung.Bien.TenCQ;
            Diachi.Value = DungChung.Bien.DiaChi;
            if (DungChung.Bien.MaBV == "27023")
            {
                xrLabel26.Visible = false;
                xrLabel27.Visible = false;
            }
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            colTTDV.Text = DungChung.Bien.GiamDoc;
            colThuKho.Text = DungChung.Bien.ThuKho;
            //if (Nguoinhanhang.Value != null )
            //    colNguoiNhanHang.Text = Nguoinhanhang.Value.ToString();
            //if (DungChung.Bien.MaBV == "30007" || DungChung.Bien.MaBV == "30009")
            //{
            //    colKeToanTruong.Text = DungChung.Bien.TruongKhoaDuoc;
            //    colNguoiLapPhieu.Text = "";
            //    col_KTT_td.Text = "Trưởng khoa dược";
            //    col_NLP_td.Text = "Thống kê dược";
            //}
            //else
            //    if (DungChung.Bien.MaBV == "26007")
            //    {
            //        xrTableCell30.Text = "Thủ kho";
            //        xrTableCell31.Text = "Trưởng khoa dược";
            //        colThuKho.Text = DungChung.Bien.TruongKhoaDuoc;
            //        colNguoiNhanHang.Text = DungChung.Bien.ThuKho;
            //        xrTableRow4.Visible = true;
            //        xrTableRow5.Visible = true;
            //        xrTableCell9.Text = "Người nhận hàng";
            //        xrTableCell13.Text = "(Ký, họ và tên)";
            //        cell_NguoiNhan.Visible = false;
            //        xrTableCell17.Visible = false;
            //    }
            //    else
            //    {
            colNguoiLapPhieu.Text = DungChung.Bien.NguoiLapBieu;
            colKeToanTruong.Text = DungChung.Bien.KeToanTruong;
            //}
        }

        private void txtsotien_BeforePrint(object sender, CancelEventArgs e)
        {

        }

        private void grtennhom_BeforePrint(object sender, CancelEventArgs e)
        {
            //switch (sttGh2)
            //{
            //    case 1:
            //        grtennhom1.Text = "I";
            //        break;
            //    case 2:
            //        grtennhom1.Text = "II";
            //        break;
            //    case 3:
            //        grtennhom1.Text = "III";
            //        break;
            //    case 4:
            //        grtennhom1.Text = "IV";
            //        break;
            //    case 5:
            //        grtennhom1.Text = "IV";
            //        break;

            //}
            //sttGh2++;
            //sttGh1 = 1;
        }

    }
}
