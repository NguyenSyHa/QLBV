using System;
using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class PhieulinhthuocVTYT_14017 : DevExpress.XtraReports.UI.XtraReport
    {
        int _dongy = 0;
        public PhieulinhthuocVTYT_14017()
        {
            InitializeComponent();
        }
        public PhieulinhthuocVTYT_14017(int dy)
        {
            InitializeComponent();
            _dongy = dy;
        }
        public void BindingData()
        {
            string _fomat = DungChung.Bien.FormatString[0];
            _fomat = "{0:##,###.##}";
            colTenHH.DataBindings.Add("Text", DataSource, "TenDV");

            colsoluongyc.DataBindings.Add("Text", DataSource, "SoLuong");

            colsoluongtp.DataBindings.Add("Text", DataSource, "SoLuong").FormatString = _fomat;//.FormatString = DungChung.Bien.FormatString[0];

            colDVT.DataBindings.Add("Text", DataSource, "DonVi");
            celTongTien.DataBindings.Add("Text", DataSource, "Thanhtien").FormatString = DungChung.Bien.FormatString[1];

        }
        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            txtTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            txtTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            if (Ngaythang.Value != null && Ngaythang.Value.ToString() != "")
                txtNgayThang.Text = "Ngày ..... tháng ..... năm 20...";
            
            QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            string tenkp = "";
            if (Khoa.Value != null)
                tenkp = Khoa.Value.ToString();
            var kp = _data.KPhongs.Where(p => p.TenKP == tenkp).Select(p => p.PLoai).ToList();
            if (kp.Count > 0 && kp.First() == "Cận lâm sàng")
            {

                xrTableCell14.Text = "TRƯỞNG KHOA C.LÂM SÀNG";
            }
            else
            {
                xrTableCell14.Text = "TRƯỞNG KHOA LÂM SÀNG";
            }


        }
    }

}
