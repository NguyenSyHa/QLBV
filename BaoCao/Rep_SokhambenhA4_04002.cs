using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_SokhambenhA4_04002 : DevExpress.XtraReports.UI.XtraReport
    {
        int a = 0, _ngay = 0;
        public Rep_SokhambenhA4_04002()
        {
            InitializeComponent();
        }
        public Rep_SokhambenhA4_04002(int _a,int ngay)
        {
            InitializeComponent();
            a = _a;
            _ngay = ngay;
        }
        public void BindingData()
        {
            colTenBN.DataBindings.Add("Text", DataSource, "TenBN");
            // txtMaBN.DataBindings.Add("Text", DataSource, "maBN");
            colNam.DataBindings.Add("Text", DataSource, "tuoin");
            colNu.DataBindings.Add("Text", DataSource, "tuoinu");
            colDiachi.DataBindings.Add("Text", DataSource, "Diachi");
            colSothe.DataBindings.Add("Text", DataSource, "Sothe");
            //colNoiGT.DataBindings.Add("Text", DataSource, "NoiGT");
            //colCDTuyenduoi.DataBindings.Add("Text", DataSource, "CDNoiGT");
            if (a == 2)
            { colCDKKB.DataBindings.Add("Text", DataSource, "ticd"); }
            else
            { colCDKKB.DataBindings.Add("Text", DataSource, "noigt"); }
            colCDDT.DataBindings.Add("Text", DataSource, "ticd");
            colVVien.DataBindings.Add("Text", DataSource, "VV1");
            colTuyentren.DataBindings.Add("Text", DataSource, "TT1");
            colTuyenduoi.DataBindings.Add("Text", DataSource, "TD1");
            colNgoaitru.DataBindings.Add("Text", DataSource, "NT1");
            colVenha.DataBindings.Add("Text", DataSource, "VN1");
            //colTT.DataBindings.Add("Text", DataSource, "TT1");
            colChuyenkhoa.DataBindings.Add("Text", DataSource, "KhamCK1");
            colBS.DataBindings.Add("Text", DataSource, "TenBS");
            cell_ngayKham.DataBindings.Add("Text", DataSource, "nkb").FormatString = "{0:dd/MM}";
            cell_DanToc.DataBindings.Add("Text", DataSource, "DanToc1");
            cell_NgheNghiep.DataBindings.Add("Text", DataSource, "NgheNgiep1");
            cell_TrieuChung.DataBindings.Add("Text", DataSource, "TrieuChung1");
        }


        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            txtTenBV.Text = DungChung.Bien.TenCQ.ToUpper();
            txtTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            if (_ngay == 0)
                cell_TD_SoNgayDT.Text = "Ngày ra viện";
            else if (_ngay == 1)
                cell_TD_SoNgayDT.Text = "Ngày thanh toán";
            else if (_ngay == 2)
                cell_TD_SoNgayDT.Text = "Ngày khám";
        }

        private void xrTableCell4_BeforePrint(object sender, CancelEventArgs e)
        {
            if (a == 2)
            { xrTableCell7.Text = "Chẩn đoán"; }
            else
            { xrTableCell7.Text = "Nơi giới thiệu"; }
        }


    }
}
