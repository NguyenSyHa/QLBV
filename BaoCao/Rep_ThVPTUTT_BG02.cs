using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraReports.UI;
using System.Linq;


namespace QLBV.BaoCao
{
    public partial class Rep_ThVPTUTT_BG02 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_ThVPTUTT_BG02()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities _Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        public void BindingData()
        {

            colNgayTT.DataBindings.Add("Text", DataSource, "NTN").FormatString = "{0:dd/MM}"; ;
            colTamUng.DataBindings.Add("Text", DataSource, "TamUng").FormatString = DungChung.Bien.FormatString[1];
            colTamUngT.DataBindings.Add("Text", DataSource, "TamUng").FormatString = DungChung.Bien.FormatString[1];
            colThanhToan.DataBindings.Add("Text", DataSource, "ThanhToan").FormatString = DungChung.Bien.FormatString[1];
            colThanhToanT.DataBindings.Add("Text", DataSource, "ThanhToan").FormatString = DungChung.Bien.FormatString[1];
   
         }
        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            colTenCQCQ.Text = DungChung.Bien.TenCQ.ToUpper();
         }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            colNguoiLapBieu.Text = DungChung.Bien.NguoiLapBieu;
        }

        private void colNgayTT_BeforePrint(object sender, CancelEventArgs e)
        {
            //if (this.GetCurrentColumnValue("NgayTT") != null && this.GetCurrentColumnValue("NgayTT").ToString().Length >= 10)
            //{
            //    colNgayTT.Text = txtNgayTT.Text.ToString().Substring(0, 5);
            //}
            //else colNgayTT.Text = "";
        }
        double TTDK = 0;
        double TTCK = 0;
        double TD = 0;
        int i = 1;
        private void colDauKy_BeforePrint(object sender, CancelEventArgs e)
        {

            if (i == 1 && DuDK.Value!=null)
            {
                TD =Convert.ToDouble(DuDK.Value);
                colDauKy.Text = TD.ToString("0,0");
                i++;
            }
            else
            {
                colDauKy.Text = TD.ToString("0,0");
            }
        }
        int sltck = 0;
        QLBV_Database.QLBVEntities Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void sltdk()
        {
            //string _Maduoc=Madv.Value.ToString();
            //DateTime _ngay = Convert.ToDateTime(colNgayTT.Text);
            //var b = (from nx in Data.NhapDs
            //         join nxct in Data.NhapDcts.Where(p => p.MaDV == _Maduoc) on nx.IDNhap equals nxct.IDNhap
            //         join Kh in Data.KPhongs on nx.MaKP equals Kh.MaKP
            //         group new { nx, nxct } by new {  nx.MaKP } into kp
            //         select new
            //         {
            //             SLTDK = kp.Where(p => p.nx.NgayNhap < _ngaytu).Sum(p => p.nxct.SoLuongN) - kp.Where(p => p.nx.NgayNhap < _ngaytu).Sum(p => p.nxct.SoLuongX),
            //             Makp = kp.Key.MaKP
            //         }).ToList();
            //if (b.Count > 0)
            //{
            //    string _makp=Khoaphong.Value.ToString();
            //    string sl="";
            //    sl = b.Where(p => p.Makp == _makp).Skip(0).First().SLTDK.ToString();
            //    if (!string.IsNullOrEmpty(sl))
            //    {
            //        sltck = Convert.ToInt32(sl);
            //    }
            //}
        
        }

        private void colConLai_BeforePrint(object sender, CancelEventArgs e)
        {
            //MessageBox.Show(colTamUng.Text);
            //string M = colTamUng.Text.Trim();
            //double MM = Convert.ToDouble(M);
            //MessageBox.Show(MM.ToString());
            if (!string.IsNullOrEmpty(colTamUng.Text))
            {
                TD = TD + Convert.ToDouble(colTamUng.Text);
            }
            if (!string.IsNullOrEmpty(colThanhToan.Text))
            {
                TD = TD - Convert.ToDouble(colThanhToan.Text);
            }
            colConLai.Text = TD.ToString("0,0");
        }
              

        private void colDuCuoiThang_BeforePrint(object sender, CancelEventArgs e)
        {
            //if (!string.IsNullOrEmpty(colTamUng.Text))
            //{
            //    TD = TD + Convert.ToDouble(colTamUng.Text);
            //}
            //if (!string.IsNullOrEmpty(colThanhToan.Text))
            //{
            //    TD = TD - Convert.ToDouble(colThanhToan.Text);
            //}
            colDuCuoiThang.Text = TD.ToString("0,0");
        }

    }
}
