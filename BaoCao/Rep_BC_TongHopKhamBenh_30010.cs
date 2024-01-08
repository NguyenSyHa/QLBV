using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_BC_TongHopKhamBenh_30010 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_BC_TongHopKhamBenh_30010()
        {
            InitializeComponent();
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            lblTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            lblTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
        }

        public void BindingData()
        {
            cel_NgayThang.DataBindings.Add("Text", DataSource, "Ngay");
            cel_KhoaNoi_BHYT_KB.DataBindings.Add("Text", DataSource, "BHYT_TongKB");
            cel_KhoaNoi_BHYT_BNTren60.DataBindings.Add("Text", DataSource, "BHYT_KB_BNTren60");
            cel_KhoaNoi_BHYT_VaoVien.DataBindings.Add("Text", DataSource, "BHYT_TongVV");
            cel_KhoaNoi_BHYT_BNTren60_VV.DataBindings.Add("Text", DataSource, "BHYT_VV_BNTren60");
            cel_KhoaNoi_ND_KB.DataBindings.Add("Text", DataSource, "ND_TongKB");
            cel_KhoaNoi_ND_BNTren60_KB.DataBindings.Add("Text", DataSource, "ND_KB_BNTren60");
            cel_KhoaNoi_ND_VaoVien.DataBindings.Add("Text", DataSource, "ND_TongVV");
            cel_KhoaNoi_ND_BNTren60_VV.DataBindings.Add("Text", DataSource, "ND_VV_BNTren60");
            cel_KhoaKB_TreEmDuoi15_BHYT_KB.DataBindings.Add("Text", DataSource, "TEDuoi15_BHYT_TongKB");
            cel_KhoaKB_TreEmDuoi15_BHYT_VaoVien.DataBindings.Add("Text", DataSource, "TEDuoi15_BHYT_VV");
            cel_KhoaKB_TEDuoi15_ND_KB.DataBindings.Add("Text", DataSource, "TEDuoi15_ND_TongKB");
            cel_KhoaKB_TEDuoi15_ND_VaoVien.DataBindings.Add("Text", DataSource, "TEDuoi15_ND_VV");
            cel_KhoaKB_TEDuoi6_KB.DataBindings.Add("Text", DataSource, "TEDuoi6_TongKB");
            cel_KhoaKB_TEDuoi6_VaoVien.DataBindings.Add("Text", DataSource, "TEDuoi6_TongVV");
            cel_KhoaKB_TongKhamChung.DataBindings.Add("Text", DataSource, "TongKB");
            cel_KhoaKB_NuocNgoai.DataBindings.Add("Text", DataSource, "KB_NguoiNuocNgoai");
            cel_KhoaKB_BNTren60.DataBindings.Add("Text", DataSource, "KB_BNTren60");
            cel_KhoaKB_VaoVien.DataBindings.Add("Text", DataSource, "TongVV");
            cel_KhoaKB_VV_NuocNgoai.DataBindings.Add("Text", DataSource, "VV_NguoiNuocNgoai");
            cel_KhoaKB_VV_BNTren60.DataBindings.Add("Text", DataSource, "VV_BNTren60");
            cel_HSCC_ChuyenVien.DataBindings.Add("Text", DataSource, "TongChuyenVien");
            cel_HSCC_VaoVien.DataBindings.Add("Text", DataSource, "VV_CapCuu");

            cel_BHYT_KB_T.DataBindings.Add("Text", DataSource, "BHYT_TongKB");
            cel_BHYT_BNTren60_T.DataBindings.Add("Text", DataSource, "BHYT_KB_BNTren60");
            cel_BHYT_VV_T.DataBindings.Add("Text", DataSource, "BHYT_TongVV");
            cel_BHYT_BNTren60_VV_T.DataBindings.Add("Text", DataSource, "BHYT_VV_BNTren60");
            cel_ND_KB_T.DataBindings.Add("Text", DataSource, "ND_TongKB");
            cel_ND_BNTren60_T.DataBindings.Add("Text", DataSource, "ND_KB_BNTren60");
            cel_ND_VV_T.DataBindings.Add("Text", DataSource, "ND_TongVV");
            cel_ND_BNTren60_VV_T.DataBindings.Add("Text", DataSource, "ND_VV_BNTren60");
            cel_TEDuoi15_BHYT_KB_T.DataBindings.Add("Text", DataSource, "TEDuoi15_BHYT_TongKB");
            cel_TEDuoi15_BHYT_VV_T.DataBindings.Add("Text", DataSource, "TEDuoi15_BHYT_VV");
            cel_TEDuoi15_ND_KB_T.DataBindings.Add("Text", DataSource, "TEDuoi15_ND_TongKB");
            cel_TEDuoi15_ND_VV_T.DataBindings.Add("Text", DataSource, "TEDuoi15_ND_VV");
            cel_TEDuoi6_KB_T.DataBindings.Add("Text", DataSource, "TEDuoi6_TongKB");
            cel_TEDuoi6_VV_T.DataBindings.Add("Text", DataSource, "TEDuoi6_TongVV");
            cel_KB_T.DataBindings.Add("Text", DataSource, "TongKB");
            cel_NguoiNuocNgoai_T.DataBindings.Add("Text", DataSource, "KB_NguoiNuocNgoai");
            cel_BNTren60_KB_T.DataBindings.Add("Text", DataSource, "KB_BNTren60");
            cel_VV_T.DataBindings.Add("Text", DataSource, "TongVV");
            cel_NguoiNuocNgoai_VV_T.DataBindings.Add("Text", DataSource, "VV_NguoiNuocNgoai");
            cel_BNTren60_VV_T.DataBindings.Add("Text", DataSource, "VV_BNTren60");
            cel_CV_T.DataBindings.Add("Text", DataSource, "TongChuyenVien");
            cel_CC_T.DataBindings.Add("Text", DataSource, "VV_CapCuu");
        }
    }
}
