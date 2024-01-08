using System;
using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class rep_TKBNChuyenVien : DevExpress.XtraReports.UI.XtraReport
    {
        QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        public rep_TKBNChuyenVien()
        {
            InitializeComponent();
        }
        public void BindingData() {
            colTenBN.DataBindings.Add("Text", DataSource, "TenBNhan");
            celSThe.DataBindings.Add("Text", DataSource, "SThe");
            colTenBV.DataBindings.Add("Text", DataSource, "TenBV");
            colDiaChi.DataBindings.Add("Text", DataSource, "DChi");
            colTuoi.DataBindings.Add("Text", DataSource, "Tuoi");
            colBSDT.DataBindings.Add("Text", DataSource, "TenBSDT");
            colChanDoan.DataBindings.Add("Text", DataSource, "ChanDoan");
            celmaicd.DataBindings.Add("Text", DataSource, "MaICD");
            colTenBNhanGF.DataBindings.Add("Text", DataSource, "TenBNhan");
            colTenBNhanRF.DataBindings.Add("Text", DataSource, "TenBNhan");
            colDTuong.DataBindings.Add("Text", DataSource, "DTuong");
            GroupHeader1.GroupFields.Add(new GroupField("DTuong"));
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            txtTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
        }
        int i = 1;
        private void GroupHeader1_BeforePrint(object sender, CancelEventArgs e)
        {
            if (i == 1)
            {
                colSTTGH.Text = "I";
                colSoLuotGF.Text = "Cộng I: ";
            }
            else
            {
                colSTTGH.Text = "II";
                colSoLuotGF.Text = "Cộng II: ";
            }
            i++;
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            colNguoiLapBieu.Text = DungChung.Bien.NguoiLapBieu;
        }

        private void colTuoi_BeforePrint(object sender, CancelEventArgs e)
        {
            int mabn = 0;
            if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" && colTuoi.Text != "")
            {
                var bn = _data.BenhNhans.Where(p => p.SThe == celSThe.Text).First();
                if (bn != null)
                {
                    mabn = bn.MaBNhan;
                }
                colTuoi.Text = DungChung.Ham.TuoitheoThang(_data, mabn, "12-30");
            }
        }
    }
}
