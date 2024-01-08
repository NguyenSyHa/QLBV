using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class Rep_NopTienVaoQuy_GLoc : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_NopTienVaoQuy_GLoc()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        public void BindingData()
        {
            colMaBNhan.DataBindings.Add("Text", DataSource, "MaBNhan");
            colTenBNhan.DataBindings.Add("Text", DataSource, "TenBNhan");
            colNgayLP.DataBindings.Add("Text", DataSource, "NgayLP");
            colSoPhieu.DataBindings.Add("Text", DataSource, "SoPhieu");
            colTienUng.DataBindings.Add("Text", DataSource, "TienUng").FormatString = DungChung.Bien.FormatString[1];
            colTienThu.DataBindings.Add("Text", DataSource, "ThuNoiTru").FormatString = DungChung.Bien.FormatString[1];
            celThuNgoaiTru.DataBindings.Add("Text", DataSource, "ThuNgoaiTru").FormatString = DungChung.Bien.FormatString[1];
            celTienThuoc.DataBindings.Add("Text", DataSource, "TienThuoc").FormatString = DungChung.Bien.FormatString[1];
            colTienChi.DataBindings.Add("Text", DataSource, "TienChi").FormatString = DungChung.Bien.FormatString[1];


            colTienUngT.DataBindings.Add("Text", DataSource, "TienUng");
            colTienUngT.Summary.FormatString = DungChung.Bien.FormatString[1];
            colTienThuT.DataBindings.Add("Text", DataSource, "ThuNoiTru");
            colTienThuT.Summary.FormatString = DungChung.Bien.FormatString[1];
            celThuNgoaiTruT.DataBindings.Add("Text", DataSource, "ThuNgoaiTru");
            celThuNgoaiTruT.Summary.FormatString = DungChung.Bien.FormatString[1];
            colTienChiT.DataBindings.Add("Text", DataSource, "TienChi");
            colTienChiT.Summary.FormatString = DungChung.Bien.FormatString[1];
            celTienThuocT.DataBindings.Add("Text", DataSource, "TienThuoc");
            celTienThuocT.Summary.FormatString = DungChung.Bien.FormatString[1];


        }
        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            colTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            colTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            //colNguoiLapBieu.Text = DungChung.Bien.NguoiLapBieu;
            colKeToan.Text = DungChung.Bien.KeToanTruong;
            colTTDV.Text = DungChung.Bien.GiamDoc;
            Double st = 0;
            if (TongTien.Value != null)
                st = Convert.ToDouble(TongTien.Value);
            st = Math.Round(st, 0);
            txtsotien.Text = st.ToString("##,###") + " (" + DungChung.Ham.DocTienBangChu(st, " đồng)");
        }

        private void colKetDu_BeforePrint(object sender, CancelEventArgs e)
        {
            
            Double st =0;
            if(TongTien.Value!=null)
            st = Convert.ToDouble(TongTien.Value);
            st = Math.Round(st, 0);
            colKetDu.Text = st.ToString("##,###");

        }

        private void txtsotien_BeforePrint(object sender, CancelEventArgs e)
        {
         
        }

    }
}
