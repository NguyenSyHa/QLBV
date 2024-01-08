using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_DSTreKhamChuaKThe : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_DSTreKhamChuaKThe()
        {
            InitializeComponent();
        }
        public void BindingData()
        {
            celNgay.DataBindings.Add("Text", DataSource, "Ngay").FormatString = "{0:dd/MM/yyyy}";
            celHoTen.DataBindings.Add("Text", DataSource, "TenBNhan");
            celNgaySinh.DataBindings.Add("Text", DataSource, "NgaySinh");
            cellNu.DataBindings.Add("Text", DataSource, "Nu");
            celNam.DataBindings.Add("Text", DataSource, "Nam");
            celDChi.DataBindings.Add("Text", DataSource, "DChi");
            celNguoiGiamHo.DataBindings.Add("Text", DataSource, "NguoiGiamHo");
            celChiPhi.DataBindings.Add("Text", DataSource, "Tong");
            celChiPhiTong.DataBindings.Add("Text", DataSource, "Tong");

        }
    }
}
