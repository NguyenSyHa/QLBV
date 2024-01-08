using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
using System.Collections.Generic;

namespace QLBV.BaoCao
{
    public partial class repPhieuXuat_12001 : DevExpress.XtraReports.UI.XtraReport
    {
        List<QLBV.FormNhap.usNhapDuoc.PhieuNhap_12001> _kq = new List<FormNhap.usNhapDuoc.PhieuNhap_12001>();
        public repPhieuXuat_12001(List<QLBV.FormNhap.usNhapDuoc.PhieuNhap_12001> a)
        {
            InitializeComponent();
            _kq = a;
        }
        //public void BindingData()
        //{
        //    //colTenHH.DataBindings.Add("Text", DataSource, "TenDV");
        //    colTenDV.DataBindings.Add("Text", DataSource, "TenDV");
        //    colThanhtien.DataBindings.Add("Text", DataSource, "ThanhTienX").FormatString = DungChung.Bien.FormatString[1];
        //    colsoluongtt.DataBindings.Add("Text", DataSource, "SoLuongX").FormatString = DungChung.Bien.FormatString[0];
        //    colTongtienRep.DataBindings.Add("Text", DataSource, "ThanhTienX").FormatString = DungChung.Bien.FormatString[1];
        //    colGia.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[1];
        //    colDonvi.DataBindings.Add("Text", DataSource, "DonVi");
        //    colMaSo.DataBindings.Add("Text", DataSource, "MaTam");
        //}
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        string tenkho = "";
        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "12122")
            {
                TenDV.Value = DungChung.Bien.TenCQCQ;
                Diachi.Value = DungChung.Bien.TenCQ;
            }
            else
            {
                TenDV.Value = DungChung.Bien.TenCQ;
                Diachi.Value = DungChung.Bien.DiaChi;
            }
            if (tenkho != null)
                tenkho = Khoxuat.Value.ToString();
            if (tenkho.ToLower().Contains("chương trình sốt rét") || tenkho.ToLower().Contains("chương trình lao"))
            {
                QLBV.BaoCao.rep_subPhieuNhap_12001_KhoSotRet rep = new rep_subPhieuNhap_12001_KhoSotRet();
                xrSubreport1.ReportSource = rep;
                rep.DataSource = _kq;
                rep.BindingData();
            }
            else if (tenkho.ToLower().Contains("chương trình tiêm chủng"))
            {
                //QLBV.BaoCao.rep_subPhieuNhap_12001_KhoTiemChung repSub = (QLBV.BaoCao.rep_subPhieuNhap_12001_KhoTiemChung)xrSubreport1.ReportSource;
                //repSub.DataSource = _kq;
                //repSub.BindingData();
                QLBV.BaoCao.rep_subPhieuNhap_12001_KhoTiemChung rep = new rep_subPhieuNhap_12001_KhoTiemChung();
                xrSubreport1.ReportSource = rep;
                rep.DataSource = _kq;
                rep.BindingData();
            }
            else
            {
                //QLBV.BaoCao.rep_subPhieuNhap_12001 repSub = (QLBV.BaoCao.rep_subPhieuNhap_12001)xrSubreport1.ReportSource;
                //repSub.DataSource = _kq;
                //repSub.BindingData();
                QLBV.BaoCao.rep_subPhieuNhap_12001 rep = new rep_subPhieuNhap_12001();
                xrSubreport1.ReportSource = rep;
                rep.DataSource = _kq;
                rep.BindingData();
            }
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {

            //double tongtien = 0;
            //if(TongTien.Value!=null && TongTien.Value.ToString()!="")
            //tongtien =Convert.ToDouble(TongTien.Value);
            //txtsotien.Text = "Bằng chữ: " + DungChung.Ham.DocTienBangChu(tongtien," đồng!");
            colTTDV.Text = DungChung.Bien.GiamDoc;
            colThuKho.Text = DungChung.Bien.ThuKho;
            if (Nguoinhanhang.Value != null)
                colNguoiNhanHang.Text = Nguoinhanhang.Value.ToString();
            //if(DungChung.Bien.MaBV=="12122")
            //{
            //    colNguoiNhanHang.Text = "";
            //}
            if (DungChung.Bien.MaBV == "30007" || DungChung.Bien.MaBV == "30009")
            {
                colKeToanTruong.Text = DungChung.Bien.TruongKhoaDuoc;
                colNguoiLapPhieu.Text = "";
                col_KTT_td.Text = "Trưởng khoa dược";
                col_NLP_td.Text = "Thống kê dược";
            }
            else
                if (DungChung.Bien.MaBV == "26007")
                {
                    xrTableCell30.Text = "Thủ kho";
                    xrTableCell31.Text = "Trưởng khoa dược";
                    colThuKho.Text = DungChung.Bien.TruongKhoaDuoc;
                    colNguoiNhanHang.Text = DungChung.Bien.ThuKho;
                    xrTableRow4.Visible = true;
                    xrTableRow5.Visible = true;
                    xrTableCell9.Text = "Người nhận hàng";
                    xrTableCell13.Text = "(Ký, họ và tên)";
                    cell_NguoiNhan.Visible = false;
                    xrTableCell17.Visible = false;
                }
                else
                {

                    colNguoiLapPhieu.Text = DungChung.Bien.NguoiLapBieu;
                    colKeToanTruong.Text = DungChung.Bien.KeToanTruong;
                    if(DungChung.Bien.MaBV=="24009")
                    colNguoiNhanHang.Text = NoiNhan.Value.ToString();

                }
        }

        private void txtsotien_BeforePrint(object sender, CancelEventArgs e)
        {
            //Double st = Convert.ToDouble(colTongtienRep.Text);
            //st = Math.Round(st, 0);
            //txtSotien.Text = DungChung.Ham.DocTienBangChu(st, " đồng!");
        }

    }
}
