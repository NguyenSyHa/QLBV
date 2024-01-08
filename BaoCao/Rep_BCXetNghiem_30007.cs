using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;

namespace QLBV.BaoCao
{
    public partial class Rep_BCXetNghiem_30007 : DevExpress.XtraReports.UI.XtraReport
    {
        List<QLBV.FormThamSo.frm_BCXetNghiem_30007.BCTruyenMau> _kq = new List<FormThamSo.frm_BCXetNghiem_30007.BCTruyenMau>();
        public Rep_BCXetNghiem_30007(List<QLBV.FormThamSo.frm_BCXetNghiem_30007.BCTruyenMau> kq)
        {
            InitializeComponent();
            _kq = kq;
        }
        public void BindingData()
        {
            colKP.DataBindings.Add("Text", DataSource, "TenKP");

            colHHNTru.DataBindings.Add("Text", DataSource, "HHocNTru");
            colHHNgTru.DataBindings.Add("Text", DataSource, "HHocNgTru");
            colHHKSK.DataBindings.Add("Text", DataSource, "HHocKSK");

            colSHNTru.DataBindings.Add("Text", DataSource, "SHoaNTru");
            colSHNgTru.DataBindings.Add("Text", DataSource, "SHoaNgTru");
            colSHKSK.DataBindings.Add("Text", DataSource, "SHoaKSK");

            colVSNTru.DataBindings.Add("Text", DataSource, "VSinhNTru");
            colVSNgTru.DataBindings.Add("Text", DataSource, "VSinhNgTru");
            colVSKSK.DataBindings.Add("Text", DataSource, "VSinhKSK");

            colHIVNTru.DataBindings.Add("Text", DataSource, "HIVNTru");
            colHIVNgTru.DataBindings.Add("Text", DataSource, "HIVNgTru");
            colHIVKSK.DataBindings.Add("Text", DataSource, "HIVKSK");

            colSTNTru.DataBindings.Add("Text", DataSource, "STietNTru");
            colSTNgTru.DataBindings.Add("Text", DataSource, "STietNgTru");
            colSTKSK.DataBindings.Add("Text", DataSource, "STietKSK");

            colHHNTru_T.DataBindings.Add("Text", DataSource, "HHocNTru");
            colHHNgTru_T.DataBindings.Add("Text", DataSource, "HHocNgTru");
            colHHKSK_T.DataBindings.Add("Text", DataSource, "HHocKSK");

            colSHNTru_T.DataBindings.Add("Text", DataSource, "SHoaNTru");
            colSHNgTru_T.DataBindings.Add("Text", DataSource, "SHoaNgTru");
            colSHKSK_T.DataBindings.Add("Text", DataSource, "SHoaKSK");

            colVSNTru_T.DataBindings.Add("Text", DataSource, "VSinhNTru");
            colVSNgTru_T.DataBindings.Add("Text", DataSource, "VSinhNgTru");
            colVSKSK_T.DataBindings.Add("Text", DataSource, "VSinhKSK");

            colHIVNTru_T.DataBindings.Add("Text", DataSource, "HIVNTru");
            colHIVNgTru_T.DataBindings.Add("Text", DataSource, "HIVNgTru");
            colHIVKSK_T.DataBindings.Add("Text", DataSource, "HIVKSK");

            colSTNTru_T.DataBindings.Add("Text", DataSource, "STietNTru");
            colSTNgTru_T.DataBindings.Add("Text", DataSource, "STietNgTru");
            colSTKSK_T.DataBindings.Add("Text", DataSource, "STietKSK");

            //xrTableCell1.DataBindings.Add("Text", DataSource, "STietKSK");
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            txtCQCQ.Text = DungChung.Bien.TenCQCQ;
            txtCQ.Text = DungChung.Bien.TenCQ.ToUpper();
        }

        private void xrSubreport1_BeforePrint(object sender, CancelEventArgs e)
        {
            if (_kq.Count > 0)
                _InSubReport(xrSubreport1);
        }
        private void _InSubReport(XRSubreport repsub)
        {
            QLBV.BaoCao.Rep_BCXetNghiem_30007_Sub rep = new BaoCao.Rep_BCXetNghiem_30007_Sub();
            repsub.ReportSource = rep;
            rep.DataSource = _kq;
            rep.BindingData();
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            txtNgayKi.Text = DungChung.Bien.DiaDanh + ", " + DungChung.Ham.NgaySangChu(DateTime.Now, DungChung.Bien.FormatDate);
        }
    }
}
