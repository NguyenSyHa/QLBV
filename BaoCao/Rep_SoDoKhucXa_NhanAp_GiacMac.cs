using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_SoDoKhucXa_NhanAp_GiacMac : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_SoDoKhucXa_NhanAp_GiacMac()
        {
            InitializeComponent();
        }
        internal void DataBinDing()
        {
            celTenBNhan.DataBindings.Add("Text", DataSource, "TenBNhan");
            celNam.DataBindings.Add("Text", DataSource, "Nam");
            celNu.DataBindings.Add("Text", DataSource, "Nu");
            celDChi.DataBindings.Add("Text", DataSource, "DChi");
            celBHYT.DataBindings.Add("Text", DataSource, "BHYT");
            celKhac.DataBindings.Add("Text", DataSource, "Khac");
            celNoiGui.DataBindings.Add("Text", DataSource, "TenKP");
            celChanDoan.DataBindings.Add("Text", DataSource, "ChanDoan");
            celYeuCau.DataBindings.Add("Text", DataSource, "TenDV");
            celKetQua.DataBindings.Add("Text", DataSource, "KetQua");
            celNguoiTH.DataBindings.Add("Text", DataSource, "TenCB");
            celNgayTH.DataBindings.Add("Text", DataSource, "NgayTH").FormatString = "{0: dd/MM}";
        }
    }
}
