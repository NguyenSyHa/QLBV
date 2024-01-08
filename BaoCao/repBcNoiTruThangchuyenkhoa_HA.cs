using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class repBcNoiTruThangchuyenkhoa_HA : DevExpress.XtraReports.UI.XtraReport
    {
        public repBcNoiTruThangchuyenkhoa_HA()
        {
            InitializeComponent();
        }
        public void BindingData()
        {
            colTenKP.DataBindings.Add("Text", DataSource, "ChuyenKhoa");
            txtTS.DataBindings.Add("Text", DataSource, "TongSo").FormatString = DungChung.Bien.FormatString[1];
            colTongSoT.DataBindings.Add("Text", DataSource, "TongSo").FormatString = DungChung.Bien.FormatString[1];
            txtBHYT.DataBindings.Add("Text", DataSource, "BHYT").FormatString = DungChung.Bien.FormatString[1];
            colBHYTT.DataBindings.Add("Text", DataSource, "BHYT").FormatString = DungChung.Bien.FormatString[1];
            txtVP.DataBindings.Add("Text", DataSource, "VienPhi").FormatString = DungChung.Bien.FormatString[1];
            colVienPhiT.DataBindings.Add("Text", DataSource, "VienPhi").FormatString = DungChung.Bien.FormatString[1];
            //colKhongThuDuoc.DataBindings.Add("Text", DataSource, "KhongThuDuoc").FormatString = DungChung.Bien.FormatString[1];
            //colKhongThuDuocT.DataBindings.Add("Text", DataSource, "KhongThuDuoc").FormatString = DungChung.Bien.FormatString[1];
            txtCC.DataBindings.Add("Text", DataSource, "CapCuu").FormatString = DungChung.Bien.FormatString[1];
            colCapCuuT.DataBindings.Add("Text", DataSource, "CapCuu").FormatString = DungChung.Bien.FormatString[1];
            txtBNVV.DataBindings.Add("Text", DataSource, "BNVV").FormatString = DungChung.Bien.FormatString[1];
            colBNVVT.DataBindings.Add("Text", DataSource, "BNVV").FormatString = DungChung.Bien.FormatString[1];
         }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            colNguoiLapBang.Text = DungChung.Bien.NguoiLapBieu;
            colGiamDoc.Text = DungChung.Bien.GiamDoc;
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            TenCQ.Value = DungChung.Bien.TenCQ.ToUpper();
        }

        private void colVienPhi_BeforePrint(object sender, CancelEventArgs e)
        {

            //if(this.GetCurrentColumnValue("VienPhi")!="0")
            //{
            //    colVienPhi=this.GetCurrentColumnValue("VienPhi").ToString()
            //}
        }

        private void colTenKP_BeforePrint(object sender, CancelEventArgs e)
        {
            string _chuyenkhoa = "";
            if (GetCurrentColumnValue("ChuyenKhoa") != null)
                _chuyenkhoa = GetCurrentColumnValue("ChuyenKhoa").ToString();
            if (string.IsNullOrEmpty(_chuyenkhoa))
                colTenKP.Text = "Khám chung";
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            int _ts = 0; int _bhyt = 0; int _vp = 0; int _cc = 0; int _vv = 0;
            if (this.GetCurrentColumnValue("TongSo") != null)
            {
                _ts = Convert.ToInt32(this.GetCurrentColumnValue("TongSo"));
                if (_ts > 0)
                {
                    colTongSo.Text = _ts.ToString("##,###");
                }
                else colTongSo.Text = "";

            }
            if (this.GetCurrentColumnValue("BHYT") != null)
            {
                _bhyt = Convert.ToInt32(this.GetCurrentColumnValue("BHYT"));
                if (_bhyt > 0)
                {
                    colBHYT.Text = _bhyt.ToString("##,###");
                }
                else colBHYT.Text = "";

            }
            if (this.GetCurrentColumnValue("VienPhi") != null)
            {
                _vp = Convert.ToInt32(this.GetCurrentColumnValue("VienPhi"));
                if (_vp > 0)
                {
                    colVienPhi.Text = _vp.ToString("##,###");
                }
                else colVienPhi.Text = "";

            }
            if (this.GetCurrentColumnValue("CapCuu") != null)
            {
                _cc = Convert.ToInt32(this.GetCurrentColumnValue("CapCuu"));
                if (_cc > 0)
                {
                    colCapCuu.Text = _cc.ToString("##,###");
                }
                else colCapCuu.Text = "";

            }
            if (this.GetCurrentColumnValue("BNVV") != null)
            {
                _vv = Convert.ToInt32(this.GetCurrentColumnValue("BNVV"));
                if (_vv > 0)
                {
                    colBNVV.Text = _vv.ToString("##,###");
                }
                else colBNVV.Text = "";

            }
            
        }
    }
}
