using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_BC_TTVPTheonNgay_BNDV_26007cs : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_BC_TTVPTheonNgay_BNDV_26007cs()
        {
            InitializeComponent();
        }

        public void BindingData()
        {
            cellSoPhieu.DataBindings.Add("Text", DataSource, "SoHD");
            cellDiaChi.DataBindings.Add("Text", DataSource, "DChi");
            cellNoiDung.DataBindings.Add("Text", DataSource, "TenDV");
            cellTen.DataBindings.Add("Text", DataSource, "TenBNhan");
            cellTuoi.DataBindings.Add("Text", DataSource, "NamSinh");
            cellSoTien.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];
            cellTongCong.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];

            xrTableCell21.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];
            //xrTableCell47.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];
            //gr_ghichu2.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];
        }

        private void GroupHeader1_BeforePrint(object sender, CancelEventArgs e)
        {
            gr_ngay.DataBindings.Add("Text", DataSource, "NgayThu");
            GroupHeader1.GroupFields.Add(new GroupField("NgayThu"));
        }

        private void GroupFooter1_BeforePrint(object sender, CancelEventArgs e)
        {
           
        }

    }
}
