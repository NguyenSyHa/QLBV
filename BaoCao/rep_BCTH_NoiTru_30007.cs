using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_BCTH_NoiTru_30007 : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_BCTH_NoiTru_30007()
        {
            InitializeComponent();
        }


        internal void BindingData()
        {
            celTenKhoa.DataBindings.Add("Text", DataSource, "TenKP");
            celTS.DataBindings.Add("Text", DataSource, "TSBNrv");
            colTSRV.DataBindings.Add("Text", DataSource, "TSBNrv");

            celTs_n.DataBindings.Add("Text", DataSource, "TSBNrv_ngaydt");
            colTSRV_n.DataBindings.Add("Text", DataSource, "TSBNrv_ngaydt");

            celBHYT.DataBindings.Add("Text", DataSource, "BNBHYT");
            colTSBHYT.DataBindings.Add("Text", DataSource, "BNBHYT");

            celBHYT_n.DataBindings.Add("Text", DataSource, "BNBHYT_ngaydt");
            colTSBHYT_n.DataBindings.Add("Text", DataSource, "BNBHYT_ngaydt");

            cel_VP.DataBindings.Add("Text", DataSource, "BNVP");
            colTSVP.DataBindings.Add("Text", DataSource, "BNVP");

            celVP_n.DataBindings.Add("Text", DataSource, "BNVP_ngaydt");
            colTSVP_n.DataBindings.Add("Text", DataSource, "BNVP_ngaydt");

            celKhoi.DataBindings.Add("Text", DataSource, "BN_Khoi");
            colTSKhoi.DataBindings.Add("Text", DataSource, "BN_Khoi");

            celKhoi_n.DataBindings.Add("Text", DataSource, "BN_Khoi_ngaydt");
            colTSKhoi_n.DataBindings.Add("Text", DataSource, "BN_Khoi_ngaydt");

            celDoGiam.DataBindings.Add("Text", DataSource, "BN_Dogiam");
            colTSDo.DataBindings.Add("Text", DataSource, "BN_Dogiam");

            celDoGiam_n.DataBindings.Add("Text", DataSource, "BN_Dogiam_ngaydt");
            colTSDo_n.DataBindings.Add("Text", DataSource, "BN_Dogiam_ngaydt");

            cel_KTD.DataBindings.Add("Text", DataSource, "BN_KhongThayDoi");
            colTSKTD.DataBindings.Add("Text", DataSource, "BN_KhongThayDoi");

            celKTD_n.DataBindings.Add("Text", DataSource, "BN_KhongThayDoi_ngaydt");
            colKTD_n.DataBindings.Add("Text", DataSource, "BN_KhongThayDoi_ngaydt");

            celNang.DataBindings.Add("Text", DataSource, "BN_NangHon");
            colTSNang.DataBindings.Add("Text", DataSource, "BN_NangHon");

            celNang_n.DataBindings.Add("Text", DataSource, "BN_NangHon_ngaydt");
            colTSNang_n.DataBindings.Add("Text", DataSource, "BN_NangHon_ngaydt");

            cel_TV.DataBindings.Add("Text", DataSource, "BN_TuVong");
            colTSTV.DataBindings.Add("Text", DataSource, "BN_TuVong");

            cel6.DataBindings.Add("Text", DataSource, "BN6");
            colTS6.DataBindings.Add("Text", DataSource, "BN6");

            cel15.DataBindings.Add("Text", DataSource, "BN15");
            colTS15.DataBindings.Add("Text", DataSource, "BN15");

            cel60.DataBindings.Add("Text", DataSource, "BN60");
            colTS60.DataBindings.Add("Text", DataSource, "BN60");

            celChuyenv.DataBindings.Add("Text", DataSource, "BNChuyenV");
            colTSCV.DataBindings.Add("Text", DataSource, "BNChuyenV");

            celCapcuu.DataBindings.Add("Text", DataSource, "BNCapCuu");
            colTSCC.DataBindings.Add("Text", DataSource, "BNCapCuu");
           
        }
    }
}
