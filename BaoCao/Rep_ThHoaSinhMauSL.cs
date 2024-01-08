using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class Rep_ThHoaSinhMauSL : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_ThHoaSinhMauSL()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        public void BindingData()
        {
            colHoten.DataBindings.Add("Text", DataSource, "tenBN");
            txtMaBNhan.DataBindings.Add("Text", DataSource, "maBN");
            colGioitinh.DataBindings.Add("Text", DataSource, "gioitinh");
            colTuoi.DataBindings.Add("Text", DataSource, "tuoi");
            colDiachi.DataBindings.Add("Text", DataSource, "diachi");
            colBHYT.DataBindings.Add("Text", DataSource, "bhyt");
            colTenKP.DataBindings.Add("Text", DataSource, "noigui");
            colChanDoan.DataBindings.Add("Text", DataSource, "chandoan");
            colKQ1.DataBindings.Add("Text", DataSource, "sl1");
            colKQ2.DataBindings.Add("Text", DataSource, "sl2");
            colKQ3.DataBindings.Add("Text", DataSource, "sl3");
            colKQ4.DataBindings.Add("Text", DataSource, "sl4");
            colKQ5.DataBindings.Add("Text", DataSource, "sl5");
            colKQ6.DataBindings.Add("Text", DataSource, "sl6");
            colKQ7.DataBindings.Add("Text", DataSource, "sl7");
            colKQ8.DataBindings.Add("Text", DataSource, "sl8");
            colKQ9.DataBindings.Add("Text", DataSource, "sl9");
            colKQ10.DataBindings.Add("Text", DataSource, "sl10");
            colKQ11.DataBindings.Add("Text", DataSource, "sl11");
            colKQ12.DataBindings.Add("Text", DataSource, "sl12");
            colKQ13.DataBindings.Add("Text", DataSource, "sl13");
            colKQ14.DataBindings.Add("Text", DataSource, "sl14");
            colKQ15.DataBindings.Add("Text", DataSource, "sl15");
            colKQ16.DataBindings.Add("Text", DataSource, "sl16");
            colKQ17.DataBindings.Add("Text", DataSource, "sl17");
            colKQ18.DataBindings.Add("Text", DataSource, "sl18");
            colKQ19.DataBindings.Add("Text", DataSource, "sl19");
            colKQ20.DataBindings.Add("Text", DataSource, "sl20");
            colKQ21.DataBindings.Add("Text", DataSource, "sl21");
            colKQ22.DataBindings.Add("Text", DataSource, "sl22");
            colKQ23.DataBindings.Add("Text", DataSource, "sl23");
            colKQ24.DataBindings.Add("Text", DataSource, "sl24");
            colKQ25.DataBindings.Add("Text", DataSource, "sl25");
            colKQ26.DataBindings.Add("Text", DataSource, "sl26");
            colKQ27.DataBindings.Add("Text", DataSource, "sl27");
            colKQ28.DataBindings.Add("Text", DataSource, "sl28");
            colKQ29.DataBindings.Add("Text", DataSource, "sl29");
            colKQ30.DataBindings.Add("Text", DataSource, "sl30");
            
            colT1.DataBindings.Add("Text", DataSource, "sl1");
            colT2.DataBindings.Add("Text", DataSource, "sl2");
            colT3.DataBindings.Add("Text", DataSource, "sl3");
            colT4.DataBindings.Add("Text", DataSource, "sl4");
            colT5.DataBindings.Add("Text", DataSource, "sl5");
            colT6.DataBindings.Add("Text", DataSource, "sl6");
            colT7.DataBindings.Add("Text", DataSource, "sl7");
            colT8.DataBindings.Add("Text", DataSource, "sl8");
            colT9.DataBindings.Add("Text", DataSource, "sl9");
            colT10.DataBindings.Add("Text", DataSource, "sl10");
            colT11.DataBindings.Add("Text", DataSource, "sl11");
            colT12.DataBindings.Add("Text", DataSource, "sl12");
            colT13.DataBindings.Add("Text", DataSource, "sl13");
            colT14.DataBindings.Add("Text", DataSource, "sl14");
            colT15.DataBindings.Add("Text", DataSource, "sl15");
            colT16.DataBindings.Add("Text", DataSource, "sl16");
            colT17.DataBindings.Add("Text", DataSource, "sl17");
            colT18.DataBindings.Add("Text", DataSource, "sl18");
            colT19.DataBindings.Add("Text", DataSource, "sl19");
            colT20.DataBindings.Add("Text", DataSource, "sl20");
            colT21.DataBindings.Add("Text", DataSource, "sl21");
            colT22.DataBindings.Add("Text", DataSource, "sl22");
            colT23.DataBindings.Add("Text", DataSource, "sl23");
            colT24.DataBindings.Add("Text", DataSource, "sl24");
            colT25.DataBindings.Add("Text", DataSource, "sl25");
            colT26.DataBindings.Add("Text", DataSource, "sl26");
            colT27.DataBindings.Add("Text", DataSource, "sl27");
            colT28.DataBindings.Add("Text", DataSource, "sl28");
            colT29.DataBindings.Add("Text", DataSource, "sl29");
            colT30.DataBindings.Add("Text", DataSource, "sl30");
         }

        private void colTenKP_BeforePrint(object sender, CancelEventArgs e)
        {
          
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            colTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            int _makp = DungChung.Bien.MaKP;
            var q = from kp in _data.KPhongs.Where(p => p.MaKP == _makp) select new { kp.TenKP };
            if (q.Count() > 0)
            {
                colKP.Text = q.First().TenKP;
            }
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            colNLB.Text = DungChung.Bien.NguoiLapBieu;
        }
    }
}
