using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
using System.Collections.Generic;
namespace QLBV.BaoCao
{
    public partial class Rep_TkSoLieuKCB_YSon02 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_TkSoLieuKCB_YSon02()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

        public void BindingData()
        {
            colChuyenKhoa.DataBindings.Add("Text", DataSource, "ChuyenKhoa");
            colTSChuyenKhoa.DataBindings.Add("Text", DataSource, "TS").FormatString = "{0:##,###}";
            colHNChuyenKhoa.DataBindings.Add("Text", DataSource, "HN").FormatString = "{0:##,###}";
            colKhacCK.DataBindings.Add("Text", DataSource, "Khac").FormatString = "{0:##,###}";
            colCK6.DataBindings.Add("Text", DataSource, "CK6").FormatString = "{0:##,###}";
            colNhanDanCK.DataBindings.Add("Text", DataSource, "CKND").FormatString = "{0:##,###}";
            colCT.DataBindings.Add("Text", DataSource, "CT").FormatString = "{0:##,###}";
            colCT6ck.DataBindings.Add("Text", DataSource, "CT6").FormatString = "{0:##,###}";
            //colTSChuyenKhoa.DataBindings.Add("Text", DataSource, "TS").FormatString = DungChung.Bien.FormatString[1];
            //colHNChuyenKhoa.DataBindings.Add("Text", DataSource, "HN").FormatString = DungChung.Bien.FormatString[1];
            //colKhacCK.DataBindings.Add("Text", DataSource, "Khac").FormatString = DungChung.Bien.FormatString[1];
            //colCK6.DataBindings.Add("Text", DataSource, "CK6").FormatString = DungChung.Bien.FormatString[1];
            //colNhanDanCK.DataBindings.Add("Text", DataSource, "CKND").FormatString = DungChung.Bien.FormatString[1];
            //colCT.DataBindings.Add("Text", DataSource, "CT").FormatString = DungChung.Bien.FormatString[1];
            //colCT6ck.DataBindings.Add("Text", DataSource, "CT6").FormatString = DungChung.Bien.FormatString[1];

            colTSChuyenKhoaTS.DataBindings.Add("Text", DataSource, "TS").FormatString = "{0:##,###}";
            colHNChuyenKhoaTS.DataBindings.Add("Text", DataSource, "HN").FormatString = "{0:##,###}";
            colKhacCKTS.DataBindings.Add("Text", DataSource, "Khac").FormatString = "{0:##,###}";
            colCK6TS.DataBindings.Add("Text", DataSource, "CK6").FormatString = "{0:##,###}";
            colNhanDanCKTS.DataBindings.Add("Text", DataSource, "CKND").FormatString = "{0:##,###}";
            colCTTS.DataBindings.Add("Text", DataSource, "CT").FormatString = "{0:##,###}";
            colCT6ckTS.DataBindings.Add("Text", DataSource, "CT6").FormatString = "{0:##,###}";
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            colTenCQ.Text = DungChung.Bien.TenCQ;

        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            colNLB.Text = DungChung.Bien.NguoiLapBieu;
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {



        }

        private void colChuyenKhoa_BeforePrint(object sender, CancelEventArgs e)
        {
            string _chuyenkhoa = "";
            if (GetCurrentColumnValue("ChuyenKhoa") != null)
                _chuyenkhoa = GetCurrentColumnValue("ChuyenKhoa").ToString();
            if (string.IsNullOrEmpty(_chuyenkhoa))
                colChuyenKhoa.Text = "Khám chung";
        }
    }
}
