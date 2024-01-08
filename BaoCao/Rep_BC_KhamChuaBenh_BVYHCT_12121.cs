using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_BC_KhamChuaBenh_BVYHCT_12121 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_BC_KhamChuaBenh_BVYHCT_12121()
        {
            InitializeComponent();
        }

        public void Bindingdata()
        {
            cel_TongSo.DataBindings.Add("Text", DataSource, "TongSo");
            cel_BN_BHYT_Duoi60.DataBindings.Add("Text", DataSource, "BN_BHYT_Duoi60");
            cel_BN_BHYT_Tren60.DataBindings.Add("Text", DataSource, "BN_BHYT_Tren60");
            cel_BN_BHYT_Tren80.DataBindings.Add("Text", DataSource, "BN_BHYT_Tren80");
            cel_BN_KhongBH.DataBindings.Add("Text", DataSource, "BN_TuVanKhongBH");
            cel_BN_BHNguoiNgheo_Duoi60.DataBindings.Add("Text", DataSource, "BN_BHNguoiNgheo_Duoi60");
            cel_BN_BHNguoiNgheo_Tren60.DataBindings.Add("Text", DataSource, "BN_BHNguoiNgheo_Tren60");
            cel_BN_BHNguoiNgheo_Tren80.DataBindings.Add("Text", DataSource, "BN_BHNguoiNgheo_Tren80");
            cel_TreEm_Duoi6.DataBindings.Add("Text", DataSource, "TreEm_Duoi6");
            cel_BN_VienPhi_Duoi60.DataBindings.Add("Text", DataSource, "BN_VienPhi_Duoi60");
            cel_BN_VienPhi_Tren60.DataBindings.Add("Text", DataSource, "BN_VienPhi_Tren60");
            cel_BN_VienPhi_Tren80.DataBindings.Add("Text", DataSource, "BN_VienPhi_Tren80");
            cel_BN_NuocNgoai_Duoi60.DataBindings.Add("Text", DataSource, "BN_NuocNgoai_Duoi60");
            cel_BN_NuocNgoai_Tren60.DataBindings.Add("Text", DataSource, "BN_NuocNgoai_Tren60");
            cel_DanhMuc.DataBindings.Add("Text", DataSource, "TenKhoa");

            lblSTT.DataBindings.Add("Text", DataSource, "SttHT"); 
            cel_TongSoG.DataBindings.Add("Text", DataSource, "TongSo");
            celSTT.DataBindings.Add("Text", DataSource, "Stt"); 
            cel_BN_BHYT_Duoi60G.DataBindings.Add("Text", DataSource, "BN_BHYT_Duoi60");
            cel_BN_BHYT_Tren60G.DataBindings.Add("Text", DataSource, "BN_BHYT_Tren60");
            cel_BN_BHYT_Tren80G.DataBindings.Add("Text", DataSource, "BN_BHYT_Tren80");
            cel_BN_KhongBHG.DataBindings.Add("Text", DataSource, "BN_TuVanKhongBH");
            cel_BN_BHNguoiNgheo_Duoi60G.DataBindings.Add("Text", DataSource, "BN_BHNguoiNgheo_Duoi60");
            cel_BN_BHNguoiNgheo_Tren60G.DataBindings.Add("Text", DataSource, "BN_BHNguoiNgheo_Tren60");
            cel_BN_BHNguoiNgheo_Tren80G.DataBindings.Add("Text", DataSource, "BN_BHNguoiNgheo_Tren80");
            cel_TreEm_Duoi6G.DataBindings.Add("Text", DataSource, "TreEm_Duoi6");
            cel_BN_VienPhi_Duoi60G.DataBindings.Add("Text", DataSource, "BN_VienPhi_Duoi60");
            cel_BN_VienPhi_Tren60G.DataBindings.Add("Text", DataSource, "BN_VienPhi_Tren60");
            cel_BN_VienPhi_Tren80G.DataBindings.Add("Text", DataSource, "BN_VienPhi_Tren80");
            cel_BN_NuocNgoai_Duoi60G.DataBindings.Add("Text", DataSource, "BN_NuocNgoai_Duoi60");
            cel_BN_NuocNgoai_Tren60G.DataBindings.Add("Text", DataSource, "BN_NuocNgoai_Tren60");
            cel_DanhMucG.DataBindings.Add("Text", DataSource, "TenDanhMuc");

            GroupHeader1.GroupFields.Add(new GroupField("SttHT"));

            //STT.DataBindings.Add("Text", DataSource, "Stt");
            //gr_DanhMuc.DataBindings.Add("Text", DataSource, "TenDanhMuc");
            //cel_Tong.DataBindings.Add("Text", DataSource, "TongSo");
            //cel_BHYT_Duoi60.DataBindings.Add("Text", DataSource, "BN_BHYT_Duoi60");
            //cel_BHYT_Tren60.DataBindings.Add("Text", DataSource, "BN_BHYT_Tren60");
            //cel_BHYT_Tren80.DataBindings.Add("Text", DataSource, "BN_BHYT_Tren80");
            //cel_KhamKBH.DataBindings.Add("Text", DataSource, "BN_TuVanKhongBH");
            //cel_NgheoBH_Duoi60.DataBindings.Add("Text", DataSource, "BN_BHNguoiNgheo_Duoi60");
            //cel_NgheoBH_Tren60.DataBindings.Add("Text", DataSource, "BN_BHNguoiNgheo_Tren60");
            //cel_NgheoBH_Tren80.DataBindings.Add("Text", DataSource, "BN_BHNguoiNgheo_Tren80");
            //cel_TreEm.DataBindings.Add("Text", DataSource, "TreEm_Duoi6");
            //cel_VienPhi_Duoi60.DataBindings.Add("Text", DataSource, "BN_VienPhi_Duoi60");
            //cel_VienPhi_Tren60.DataBindings.Add("Text", DataSource, "BN_VienPhi_Tren60");
            //cel_VienPhi_Tren80.DataBindings.Add("Text", DataSource, "BN_VienPhi_Tren80");
            //cel_NuocNgoai_Duoi60.DataBindings.Add("Text", DataSource, "BN_NuocNgoai_Duoi60");
            //cel_NuocNgoai_Tren60.DataBindings.Add("Text", DataSource, "BN_NuocNgoai_Tren60");

            //GroupHeader2.GroupFields.Add(new GroupField("TenDanhMuc"));
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
          
            if (this.GetCurrentColumnValue("TenKhoa") == null || this.GetCurrentColumnValue("TenKhoa").ToString() == "")
            {
                xrTableRow5.Visible = false;
            }
            else
            {
                xrTableRow5.Visible = true;
            }
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            celTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            celTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
        }

        private void xrLabel5_BeforePrint(object sender, CancelEventArgs e)
        {
            xrLabel5.Text = DungChung.Bien.DiaDanh + ", ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;
            colThuKho.Text = DungChung.Bien.NguoiLapBieu;
        }
    }
}
