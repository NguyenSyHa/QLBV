using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_BC_TVTaiNanDoThuongTich_subBNChuyenVien : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_BC_TVTaiNanDoThuongTich_subBNChuyenVien()
        {
            InitializeComponent();
        }

        public void Bindingdata()
        {
            cel_BNC_TenCoSo.DataBindings.Add("Text", DataSource, "TenCS_BNC");
            cel_TongSoBNC.DataBindings.Add("Text", DataSource, "TN_T_BNC");
            cel_TongBNCTV.DataBindings.Add("Text", DataSource, "TV_T_BNC");
            cel_BNC_TNGT_T.DataBindings.Add("Text", DataSource, "TNGT_T_BNC");
            cel_BNC_TNGT_TV.DataBindings.Add("Text", DataSource, "TNGT_TV_T_BNC");
            cel_BNC_DuoiNuoc_T.DataBindings.Add("Text", DataSource, "DuoiNuoc_T_BNC");
            cel_BNC_DuoiNuoc_TV.DataBindings.Add("Text", DataSource, "DuoiNuoc_TV_T_BNC");
            cel_BNC_NgoDoc_T.DataBindings.Add("Text", DataSource, "NGTP_T_BNC");
            cel_BNC_NgoDoc_TV.DataBindings.Add("Text", DataSource, "NGTP_TV_T_BNC");
            cel_BNC_TuTu_T.DataBindings.Add("Text", DataSource, "TuTu_T_BNC");
            cel_BNC_TuTu_TV.DataBindings.Add("Text", DataSource, "TuTu_TV_T_BNC");
            cel_BNC_TNLD_T.DataBindings.Add("Text", DataSource, "TNLD_T_BNC");
            cel_BNC_TNLD_TV.DataBindings.Add("Text", DataSource, "TNLD_TV_T_BNC");
            cel_BNC_BaoLuc_T.DataBindings.Add("Text", DataSource, "BLXD_T_BNC");
            cel_BNC_BaoLuc_TV.DataBindings.Add("Text", DataSource, "BLXD_TV_T_BNC");
            cel_BNC_TNKhac_T.DataBindings.Add("Text", DataSource, "TNKhac_T_BNC");
            cel_BNC_TNKhac_TV.DataBindings.Add("Text", DataSource, "TNKhac_TV_T_BNC");

            gr_TongBNC.DataBindings.Add("Text", DataSource, "TN_T_BNC");
            gr_TongTV_BNC.DataBindings.Add("Text", DataSource, "TV_T_BNC");
            gr_TongTNGT_BNC.DataBindings.Add("Text", DataSource, "TNGT_T_BNC");
            gr_TongTNGT_TV_BNC.DataBindings.Add("Text", DataSource, "TNGT_TV_T_BNC");
            gr_TongDuoiNuoc_BNC.DataBindings.Add("Text", DataSource, "DuoiNuoc_T_BNC");
            gr_TongDUoiNuoc_TV_BNC.DataBindings.Add("Text", DataSource, "DuoiNuoc_TV_T_BNC");
            gr_TongNgoDoc_BNC.DataBindings.Add("Text", DataSource, "NGTP_T_BNC");
            gr_TongNgoDoc_TV_BNC.DataBindings.Add("Text", DataSource, "NGTP_TV_T_BNC");
            gr_TongTuTu_BNC.DataBindings.Add("Text", DataSource, "TuTu_T_BNC");
            gr_TongTuTu_TV_BNC.DataBindings.Add("Text", DataSource, "TuTu_TV_T_BNC");
            gr_TongTNLD_BNC.DataBindings.Add("Text", DataSource, "TNLD_T_BNC");
            gr_TongTNLD_TV_BNC.DataBindings.Add("Text", DataSource, "TNLD_TV_T_BNC");
            gr_TongBL_BNC.DataBindings.Add("Text", DataSource, "BLXD_T_BNC");
            gr_TongBL_TV_BNC.DataBindings.Add("Text", DataSource, "BLXD_TV_T_BNC");
            gr_TongTNKhac_BNC.DataBindings.Add("Text", DataSource, "TNKhac_T_BNC");
            gr_TongTNKhac_TV_BNC.DataBindings.Add("Text", DataSource, "TNKhac_TV_T_BNC");
        }
    }
}
