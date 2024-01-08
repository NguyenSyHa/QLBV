using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_SoKhamBenh_27022 : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_SoKhamBenh_27022()
        {
            InitializeComponent();
        }

        public void BindingData()
        {
            CellHoTen.DataBindings.Add("Text", DataSource, "tenBN");
            CellNam.DataBindings.Add("Text", DataSource, "nam");
            CellNu.DataBindings.Add("Text", DataSource, "nu");
            CellDiaChi.DataBindings.Add("Text", DataSource, "diaChi");
            CellBHYT.DataBindings.Add("Text", DataSource, "Bhyt");
            CellNoiGT.DataBindings.Add("Text", DataSource, "noiGT");
            CellChanDoan.DataBindings.Add("Text", DataSource, "chanDoan");
            CellBNChoVe.DataBindings.Add("Text", DataSource, "choVe");
            CellNgoaiTru.DataBindings.Add("Text", DataSource, "dtNgoaiC");
            CellNoiTru.DataBindings.Add("Text", DataSource, "dtNoiC");
            CellCKC.DataBindings.Add("Text", DataSource, "kck");
            CellTL.DataBindings.Add("Text", DataSource, "tl");
            CellNA.DataBindings.Add("Text", DataSource, "na");
            CellKX.DataBindings.Add("Text", DataSource, "kx");
            CellTK.DataBindings.Add("Text", DataSource, "tk");
            CellSDM.DataBindings.Add("Text", DataSource, "sdm");
            CellJaval.DataBindings.Add("Text", DataSource, "javal");
            CellSieuAm.DataBindings.Add("Text", DataSource, "sieuam");
            CellXNMau.DataBindings.Add("Text", DataSource, "mau");
            CellXNnuocTieu.DataBindings.Add("Text", DataSource, "nuocTieu");
            CellSoiTT.DataBindings.Add("Text", DataSource, "soiTT");
            CellHSM.DataBindings.Add("Text", DataSource, "hsm");
            CellSoiGocTP.DataBindings.Add("Text", DataSource, "soigocTP");
            CellKinh3MatGuong.DataBindings.Add("Text", DataSource, "sdmBangKinh3Mat");
            CellBSkham.DataBindings.Add("Text", DataSource, "tenBS");
        }

    }
}
