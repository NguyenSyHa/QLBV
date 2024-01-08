using System;
using System.Collections.Generic;

namespace QLBV.BaoCao
{
    public partial class Rep_BC_TVTaiNanDoThuongTich : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_BC_TVTaiNanDoThuongTich()
        {
            InitializeComponent();
        }

        DateTime _tungay = new DateTime();
        DateTime _denngay = new DateTime();
        List<BNhan> _listBN = new List<BNhan>();
        List<TongBV> list1 = new List<TongBV>();
        List<TongBNChuyen> list2 = new List<TongBNChuyen>();

        public Rep_BC_TVTaiNanDoThuongTich(List<TongBV> list1, List<TongBNChuyen> list2, DateTime tungay, DateTime denngay)
        {
            InitializeComponent();

            this.list1 = list1;
            this.list2 = list2;
            _tungay = tungay;
            _denngay = denngay;
           
        }

        public class BNhan
        {
            public int MaBN { get; set; }
            public int MaKP { get; set; }
            public DateTime NgayVao { get; set; }
            public string TenKP { get; set; }
            public string KetQua { get; set; }
            public string ChuyenKhoa { get; set; }
            public string MaBVC { get; set; }
        }

        public class TongBV
        {
            public string TenCS { get; set; }
            public int MaKP { get; set; }
            public int MaBN { get; set; }
            public int TN_T { get; set; }
            public int TV_T { get; set; }
            public int TNGT_T { get; set; }
            public int TNGT_TV_T { get; set; }
            public int DuoiNuoc_T { get; set; }
            public int DuoiNuoc_TV_T { get; set; }
            public int NGTP_T { get; set; }
            public int NGTP_TV_T { get; set; }
            public int TuTu_T { get; set; }
            public int TuTu_TV_T { get; set; }
            public int TNLD_T { get; set; }
            public int TNLD_TV_T { get; set; }
            public int BLXD_T { get; set; }
            public int BLXD_TV_T { get; set; }
            public int TNKhac_T { get; set; }
            public int TNKhac_TV_T { get; set; }
        }

        public class TongBNChuyen
        {
            public string TenCS_BNC { get; set; }
            public int MaKP_BNC { get; set; }
            public int MaBN_BNC { get; set; }
            public int TN_T_BNC { get; set; }
            public int TV_T_BNC { get; set; }
            public int TNGT_T_BNC { get; set; }
            public int TNGT_TV_T_BNC { get; set; }
            public int DuoiNuoc_T_BNC { get; set; }
            public int DuoiNuoc_TV_T_BNC { get; set; }
            public int NGTP_T_BNC { get; set; }
            public int NGTP_TV_T_BNC { get; set; }
            public int TuTu_T_BNC { get; set; }
            public int TuTu_TV_T_BNC { get; set; }
            public int TNLD_T_BNC { get; set; }
            public int TNLD_TV_T_BNC { get; set; }
            public int BLXD_T_BNC { get; set; }
            public int BLXD_TV_T_BNC { get; set; }
            public int TNKhac_T_BNC { get; set; }
            public int TNKhac_TV_T_BNC { get; set; }
        }

        private void ReportHeader_BeforePrint(object sender, System.ComponentModel.CancelEventArgs e)
        {
            lbl_CQCQ.Text = DungChung.Bien.TenCQCQ;
            lbl_TenCQ.Text = DungChung.Bien.TenCQ;
            lbl_tungay.Text = "(Báo cáo: từ ngày " + _tungay.ToString("dd/MM/yyyy") + " đến ngày " + _denngay.ToString("dd/MM/yyyy") + ")";
        }

        private void xrSubreport1_BeforePrint(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Rep_BC_TVTaiNanDoThuongTich_subTongBV _repSub = (Rep_BC_TVTaiNanDoThuongTich_subTongBV)xrSubreport1.ReportSource;
            _repSub.DataSource = list1;
            _repSub.Bindingdata();
            _repSub.CreateDocument();
        }

        private void xrSubreport2_BeforePrint(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Rep_BC_TVTaiNanDoThuongTich_subBNChuyenVien _repSub = (Rep_BC_TVTaiNanDoThuongTich_subBNChuyenVien)xrSubreport2.ReportSource;
            _repSub.DataSource = list2;
            _repSub.Bindingdata();
            _repSub.CreateDocument();
        }

        private void ReportFooter_BeforePrint(object sender, System.ComponentModel.CancelEventArgs e)
        {
            colGiamDoc.Text = DungChung.Bien.GiamDoc;
            colNguoiLapBieu.Text = DungChung.Bien.NguoiLapBieu;
        }
    }
}
