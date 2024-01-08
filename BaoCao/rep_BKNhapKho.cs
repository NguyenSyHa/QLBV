using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_BKNhapKho : DevExpress.XtraReports.UI.XtraReport
    {
        bool _gr = false;
        bool _th = false;
        public rep_BKNhapKho(bool gr, bool th)
        {
            InitializeComponent();
            _gr = gr;
            _th = th;
        }
        public void BindingData()
        {
            if (_th)
            {
                colTenGH3.DataBindings.Add("Text", DataSource, "TenCC");
                colThanhTienGH3.DataBindings.Add("Text", DataSource, "ThanhTienN").FormatString = DungChung.Bien.FormatString[1];
                colThanhTienCong.DataBindings.Add("Text", DataSource, "ThanhTienN").FormatString = DungChung.Bien.FormatString[1];
            }
            else {
                colTenDuoc.DataBindings.Add("Text", DataSource, "TenDV");
                colMaDV.DataBindings.Add("Text", DataSource, "MaDV");
                colNgay.DataBindings.Add("Text", DataSource, "NgayNhap").FormatString = "{0:dd/MM/yy}";
                colDVT.DataBindings.Add("Text", DataSource, "DonVi");
                colDonGia.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[1];
                colSLTN.DataBindings.Add("Text", DataSource, "SoLuongN").FormatString = DungChung.Bien.FormatString[1];
                colThanhTien.DataBindings.Add("Text", DataSource, "ThanhTienN").FormatString = DungChung.Bien.FormatString[1];
                colThanhTienGF.DataBindings.Add("Text", DataSource, "ThanhTienN").FormatString = DungChung.Bien.FormatString[1];
                colThanhTienCong.DataBindings.Add("Text", DataSource, "ThanhTienN").FormatString = DungChung.Bien.FormatString[1];
                colSoCT.DataBindings.Add("Text", DataSource, "SoCT");
                txtID.DataBindings.Add("Text", DataSource, "IDNhap");
                if (_gr)
                {
                    colTenNCCGR.DataBindings.Add("Text", DataSource, "TenCC");
                    GroupHeader2.GroupFields.Add(new GroupField("makp"));
                }
                else
                    colTenCC.DataBindings.Add("Text", DataSource, "TenCC");
                GroupHeader1.GroupFields.Add(new GroupField("NgayNhap"));
                GroupHeader1.GroupFields.Add(new GroupField("SoCT"));
            }
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            colNLB.Text = DungChung.Bien.NguoiLapBieu;
        }

    

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            txtTenCQ.Text ="Đơn vị: "+ DungChung.Bien.TenCQ;
            txtDiaChi.Text ="Địa chỉ: "+ DungChung.Bien.DiaChi;
            if(!_th && _gr)
                    GroupHeader2.Visible = true;
        }

        private void rep_BKNhapKho_BeforePrint(object sender, CancelEventArgs e)
        {
            if (_th)
            {
                SubBand2.Visible = true;
                SubBand4.Visible = true;
                SubBand1.Visible = false;
                SubBand3.Visible = false;
                GroupHeader2.Visible = false;
                GroupHeader1.Visible = false;
                GroupFooter1.Visible = false;
            }
        }
    }
}
