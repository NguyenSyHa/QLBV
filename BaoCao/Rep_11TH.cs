using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class Rep_11TH : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_11TH()
        {
            InitializeComponent();
        }
        public void BindingData()
        {
            colMatinh.DataBindings.Add("Text", DataSource, "Matinh").FormatString = DungChung.Bien.FormatString[1];
            colNoitruBH.DataBindings.Add("Text", DataSource, "NoitruBH").FormatString = DungChung.Bien.FormatString[1];
            colNgoaitruBH.DataBindings.Add("Text", DataSource, "NgoaitruBH").FormatString = DungChung.Bien.FormatString[1];
            colTentinh.DataBindings.Add("Text", DataSource, "Tentinh").FormatString = DungChung.Bien.FormatString[1];
            colgrnt.DataBindings.Add("Text", DataSource, "NoitruBH");
            colGRngt.DataBindings.Add("Text", DataSource, "NgoaitruBH");
            colSTTT.DataBindings.Add("Text", DataSource, "TTTT");
            colSLTT.DataBindings.Add("Text", DataSource, "SLTT");
        }
        QLBV_Database.QLBVEntities _Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        int song=0;
        int sont=0;
        private void colTentinh_BeforePrint(object sender, CancelEventArgs e)
        {
            DateTime Ngaytu =DungChung.Ham.NgayTu( Convert.ToDateTime(ngaytu.Value));
            DateTime Ngayden =DungChung.Ham.NgayDen( Convert.ToDateTime(ngayden.Value));
            if((GetCurrentColumnValue("Matinh")!=null))
            {
                string Matinh =GetCurrentColumnValue("Matinh").ToString();
                var q = (from bn in _Data.BenhNhans.Where(p => p.NoiTinh == 3)
                         join tinh in _Data.DmTinhs.Where(p => p.MaTinh == Matinh) on bn.SThe.Substring(3, 2) equals tinh.MaTinh
                         join vp in _Data.VienPhis on bn.MaBNhan equals vp.MaBNhan
                         where (vp.NgayTT >= Ngaytu && vp.NgayTT <= Ngayden)
                         where(vp.Duyet==1||vp.Duyet==2)
                         group new { bn, tinh, vp } by new { bn.MaBNhan, bn.NoiTru } into kp
                         select new
                         {
                             Soluot = kp.Key.MaBNhan,
                             nt=kp.Key.NoiTru
                         }).ToList();
                if (q.Count > 0)
                {
                    int soluotnoitru = q.Where(p => p.nt == 1).Select(p => p.Soluot).Count();
                    int soluotngoaitru = q.Where(p => p.nt == 0).Select(p => p.Soluot).Count();
                    sont = sont + soluotnoitru;
                    song = song + soluotngoaitru;
                    colSoNoitru.Text = soluotnoitru.ToString();
                    colSoNgoaitru.Text = soluotngoaitru.ToString();
                }
            }

        }

        private void xrTableCell33_BeforePrint(object sender, CancelEventArgs e)
        {
            colgrSont.Text = sont.ToString();
            colgrSongt.Text = song.ToString();
        }
    }
}
