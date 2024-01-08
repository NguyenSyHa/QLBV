using System;
using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class Rep_BNNTLAYTHUOC : DevExpress.XtraReports.UI.XtraReport
    {
        QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        public Rep_BNNTLAYTHUOC()
        {
            InitializeComponent();
        }
        public void BindingData()
        {
            colMaBN.DataBindings.Add("Text", DataSource, "MaBNhan");
            colDiaChi.DataBindings.Add("Text", DataSource, "DChi");
            colGT.DataBindings.Add("Text", DataSource, "GTinh");
            colHoTen.DataBindings.Add("Text", DataSource, "TenBNhan");
            colTuoi.DataBindings.Add("Text", DataSource, "Tuoi");
            colDTuong.DataBindings.Add("Text", DataSource, "SThe");
            colNgayra.DataBindings.Add("Text", DataSource, "MaICD");
            colNgaynhan.DataBindings.Add("Text", DataSource, "NgayKham");
            

        }

        private void colGT_BeforePrint(object sender, CancelEventArgs e)
        {
            
        }

        private void colTuoi_BeforePrint(object sender, CancelEventArgs e)
        {
            int mabn = 0;
            if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" && colTuoi.Text != "")
            {
                var bn = _data.BenhNhans.Where(p => p.SThe == colDTuong.Text).First();
                if (bn != null)
                {
                    mabn = bn.MaBNhan;
                }
                colTuoi.Text = DungChung.Ham.TuoitheoThang(_data, mabn, "12-30");
            }
        }
    }
}
