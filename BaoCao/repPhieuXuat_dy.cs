using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class repPhieuXuat_dy : DevExpress.XtraReports.UI.XtraReport
    {
        public repPhieuXuat_dy()
        {
            InitializeComponent();
        }
        public void BindingData()
        {
            //colTenHH.DataBindings.Add("Text", DataSource, "TenDV");
            colTenDV.DataBindings.Add("Text", DataSource, "TenDV");
            colThanhtien.DataBindings.Add("Text", DataSource, "ThanhTienX").FormatString=DungChung.Bien.FormatString[1];
            colsoluongtt.DataBindings.Add("Text", DataSource, "SoLuongX").FormatString = DungChung.Bien.FormatString[1];
            colTongtienRep.DataBindings.Add("Text", DataSource, "ThanhTienX").FormatString = DungChung.Bien.FormatString[1];
            colGia.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[1];
            colDonvi.DataBindings.Add("Text", DataSource, "DonVi");
            
      
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            txtCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            TenDV.Value = DungChung.Bien.TenCQ.ToUpper();
            Diachi.Value = DungChung.Bien.DiaChi;
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            
            //double tongtien = 0;
            //if(TongTien.Value!=null && TongTien.Value.ToString()!="")
            //tongtien =Convert.ToDouble(TongTien.Value);
            //txtsotien.Text = "Bằng chữ: " + DungChung.Ham.DocTienBangChu(tongtien," đồng!");

            
            //colNguoiLapPhieu.Text = DungChung.Bien.NguoiLapBieu;
            colKeToanTruong.Text = DungChung.Bien.KeToanTruong;
            colThuKho.Text = DungChung.Bien.ThuKho;
            //colTTDV.Text = DungChung.Bien.GiamDoc;
        }

        private void txtsotien_BeforePrint(object sender, CancelEventArgs e)
        {
            Double st = Convert.ToDouble(TongTien.Value);
            st = Math.Round(st, 0);
            txtsotien.Text = "Tổng số tiền (bằng chữ): " + DungChung.Ham.DocTienBangChu(st, " đồng!");
        }

    }
}
