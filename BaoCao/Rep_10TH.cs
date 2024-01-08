using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class Rep_10TH : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_10TH()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        public void BindingData()
        {
            colbenhvien.DataBindings.Add("Text", DataSource, "Benhvien").FormatString = DungChung.Bien.FormatString[1];
            colbhytngoaitru.DataBindings.Add("Text", DataSource, "BHYTNgoai").FormatString = DungChung.Bien.FormatString[1];
            colbhytngoaitrugf.DataBindings.Add("Text", DataSource, "BHYTNgoai").FormatString = DungChung.Bien.FormatString[1];
            colbhytnoigf.DataBindings.Add("Text", DataSource, "BHYTNoi").FormatString = DungChung.Bien.FormatString[1];
            colbhytnoitru.DataBindings.Add("Text", DataSource, "BHYTNoi").FormatString = DungChung.Bien.FormatString[1];
            colngoaids.DataBindings.Add("Text", DataSource, "Ngoaids").FormatString = DungChung.Bien.FormatString[1];
            colngoaidsgf.DataBindings.Add("Text", DataSource, "Ngoaids").FormatString = DungChung.Bien.FormatString[1];
            colnoidsgf.DataBindings.Add("Text", DataSource, "Noids").FormatString = DungChung.Bien.FormatString[1];
            colnoingoaids.DataBindings.Add("Text", DataSource, "Noids").FormatString = DungChung.Bien.FormatString[1];
           // colsoluotngoaigf.DataBindings.Add("Text", DataSource, "SLNgoai").FormatString = DungChung.Bien.FormatString[1];
            //colsoluotngoaitru.DataBindings.Add("Text", DataSource, "SLNgoai").FormatString = DungChung.Bien.FormatString[1];
            //colsoluotnoigf.DataBindings.Add("Text", DataSource, "SLNoi").FormatString = DungChung.Bien.FormatString[1];
            //colsoluotnt.DataBindings.Add("Text", DataSource, "SLNoi").FormatString = DungChung.Bien.FormatString[1];
            txttuyen.DataBindings.Add("Text", DataSource, "tuyen").FormatString = DungChung.Bien.FormatString[1];
            GroupHeader1.GroupFields.Add(new GroupField("tuyen"));
        }

        private void GroupHeader1_BeforePrint(object sender, CancelEventArgs e)
        {
            if (this.GetCurrentColumnValue("tuyen") != null)
            {
                string tn = this.GetCurrentColumnValue("tuyen").ToString().Trim();
                switch (tn)
                {
                    case"A":
                        colBenhvienGH.Text = "Tuyến trung ương";
                        colstt.Text = "I";
                        break;
                    case"B":
                        colBenhvienGH.Text = "Tuyến tỉnh";
                        colstt.Text = "II";
                        break;
                    case"C":
                        colBenhvienGH.Text = "Tuyến huyện";
                        colstt.Text = "III";
                        break;
                    case"D":
                        colBenhvienGH.Text = "Tuyến xã";
                        colstt.Text = "IV";
                        break;
                }
            }
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            txtBenhvien.Text = DungChung.Bien.TenCQ;
            txtSoyt.Text = DungChung.Bien.TenCQCQ;
        }
        int SLNoitruT = 0;
        int SLNgoaitruT = 0;
        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            DateTime Tungay=DungChung.Ham.NgayTu(Convert.ToDateTime(Ngaytu.Value));
            DateTime Denngay=DungChung.Ham.NgayDen(Convert.ToDateTime(Ngayden.Value));
            if (this.GetCurrentColumnValue("Mabn") != null)
            {
                string _MaBV = this.GetCurrentColumnValue("Mabn").ToString().Trim();
                var q = (from bn in Data.BenhNhans.Where(p => p.MaCS== (_MaBV))
                         join Vp in Data.VienPhis on bn.MaBNhan equals Vp.MaBNhan
                         where (Vp.NgayTT >= Tungay && Vp.NgayTT <= Denngay)
                         where (Vp.Duyet == 1 || Vp.Duyet == 2)
                         group new { bn } by new { bn.MaBNhan, bn.NoiTru } into kq
                         select new
                         {
                             Mabv = kq.Key.MaBNhan,
                             NT=kq.Key.NoiTru
                         }).ToList();

                if (q.Count > 0)
                {
                    int SLNT = q.Where(p=>p.NT==1).Select(p => p.Mabv).Count();
                    int SLNgoaitru = q.Where(p => p.NT == 0).Select(p => p.Mabv).Count();
                    SLNoitruT = SLNT + SLNoitruT;
                    SLNgoaitruT = SLNgoaitruT + SLNgoaitru;
                    colsoluotngoaitru.Text = SLNgoaitru.ToString();
                    colsoluotnt.Text = SLNT.ToString();
                }
            }
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            colsoluotngoaigf.Text = SLNgoaitruT.ToString();
            colsoluotnoigf.Text = SLNoitruT.ToString();

        }
    }
}
