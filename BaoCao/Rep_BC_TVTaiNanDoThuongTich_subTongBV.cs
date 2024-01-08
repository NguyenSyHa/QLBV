using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_BC_TVTaiNanDoThuongTich_subTongBV : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_BC_TVTaiNanDoThuongTich_subTongBV()
        {
            InitializeComponent();
        }

        internal void Bindingdata()
        {
            cel_TenCoSo.DataBindings.Add("Text", DataSource, "TenCS");
            cel_TongSo.DataBindings.Add("Text", DataSource, "TN_T");
            cel_TongTV.DataBindings.Add("Text", DataSource, "TV_T");
            cel_TNGT_T.DataBindings.Add("Text", DataSource, "TNGT_T");
            cel_TNGT_TV.DataBindings.Add("Text", DataSource, "TNGT_TV_T");
            cel_DuoiNuoc_T.DataBindings.Add("Text", DataSource, "DuoiNuoc_T");
            cel_DuoiNuoc_TV.DataBindings.Add("Text", DataSource, "DuoiNuoc_TV_T");
            cel_NgoDoc_T.DataBindings.Add("Text", DataSource, "NGTP_T");
            cel_NgoDoc_TV.DataBindings.Add("Text", DataSource, "NGTP_TV_T");
            cel_TuTu_T.DataBindings.Add("Text", DataSource, "TuTu_T");
            cel_TuTu_TV.DataBindings.Add("Text", DataSource, "TuTu_TV_T");
            cel_TNLD_T.DataBindings.Add("Text", DataSource, "TNLD_T");
            cel_TNLD_TV.DataBindings.Add("Text", DataSource, "TNLD_TV_T");
            cel_BaoLuc_T.DataBindings.Add("Text", DataSource, "BLXD_T");
            cel_BaoLuc_TV.DataBindings.Add("Text", DataSource, "BLXD_TV_T");
            cel_TNKhac_T.DataBindings.Add("Text", DataSource, "TNKhac_T");
            cel_TNKhac_TV.DataBindings.Add("Text", DataSource, "TNKhac_TV_T");

            gr_Tong.DataBindings.Add("Text", DataSource, "TN_T");
            gr_TongTV.DataBindings.Add("Text", DataSource, "TV_T");
            gr_TongTNGT.DataBindings.Add("Text", DataSource, "TNGT_T");
            gr_TongTNGT_TV.DataBindings.Add("Text", DataSource, "TNGT_TV_T");
            gr_TongDuoiNuoc.DataBindings.Add("Text", DataSource, "DuoiNuoc_T");
            gr_TongDuoiNuoc_TV.DataBindings.Add("Text", DataSource, "DuoiNuoc_TV_T");
            gr_TongNgoDoc.DataBindings.Add("Text", DataSource, "NGTP_T");
            gr_TongNgoDoc_TV.DataBindings.Add("Text", DataSource, "NGTP_TV_T");
            gr_TongTuTu.DataBindings.Add("Text", DataSource, "TuTu_T");
            gr_TongTuTu_TV.DataBindings.Add("Text", DataSource, "TuTu_TV_T");
            gr_TongTNLD.DataBindings.Add("Text", DataSource, "TNLD_T");
            gr_TongTNLD_TV.DataBindings.Add("Text", DataSource, "TNLD_TV_T");
            gr_TongBL.DataBindings.Add("Text", DataSource, "BLXD_T");
            gr_TongBL_TV.DataBindings.Add("Text", DataSource, "BLXD_TV_T");
            gr_TongTNKhac.DataBindings.Add("Text", DataSource, "TNKhac_T");
            gr_TongTNKhac_TV.DataBindings.Add("Text", DataSource, "TNKhac_TV_T");
        }
    }
}
