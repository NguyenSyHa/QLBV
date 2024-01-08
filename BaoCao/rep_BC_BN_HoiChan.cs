using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_BC_BN_HoiChan : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_BC_BN_HoiChan()
        {
            InitializeComponent();
        }
        public void BindingData()
        {
            colTenBNhan.DataBindings.Add("Text", DataSource, "hoTen");
            colDiachi.DataBindings.Add("Text", DataSource, "diaChi");
            colSoTheBHYT.DataBindings.Add("Text", DataSource, "SThe");
            colChandoan.DataBindings.Add("Text", DataSource, "ChanDoan");
            colNgayHoichan.DataBindings.Add("Text", DataSource, "NgayHC");
            colBSyHoichan.DataBindings.Add("Text", DataSource, "BSHC");
            colTuoiNam.DataBindings.Add("Text", DataSource, "tuoiNam");
            colTuoiNu.DataBindings.Add("Text", DataSource, "tuoiNu");
            colKhoaHoiChan.DataBindings.Add("Text", DataSource, "khoaHC");
            colBSyHoichan.DataBindings.Add("Text", DataSource, "bsHC");
            colThuocHoichan.DataBindings.Add("Text", DataSource, "thuocHC");
        }

    }
}
