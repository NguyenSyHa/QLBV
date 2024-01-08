using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class Rep_GiayHenKham_YS01 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_GiayHenKham_YS01()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities _Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "30003")
                xrLabel2.Visible = true;
            if (DungChung.Bien.MaBV == "08204")
                txtGhiChu.Visible = false;
            colTenCQ.Text = DungChung.Bien.TenCQ;
            int _kphong = 0;
            if (KPhong.Value != null)
            {
                _kphong = Convert.ToInt32(KPhong.Value);
                var kp = (from p in _Data.KPhongs.Where(p => p.MaKP == _kphong) select new { p.TenKP }).ToList();
                if (kp.Count > 0)
                {
                    colTenKP.Text = kp.First().TenKP;
                }
            }
             string _bskb="";
          if (BSKB.Value != null)
            {
                _bskb = BSKB.Value.ToString();
                var q=(from cb in _Data.CanBoes.Where(p=>p.MaCB==_bskb) select new{cb.TenCB}).ToList();
                if(q.Count>0)
                {
                    colTenBS.Text=q.First().TenCB;
                }
            }
          string _dt = "";
          if (DanToc.Value != null)
          {
              _dt =DanToc.Value.ToString();
              var qdt = (from dt in _Data.DanTocs.Where(p => p.MaDT == _dt) select new { dt.TenDT }).ToList();
              if (qdt.Count > 0)
              {
                  txtDanToc.Text = qdt.First().TenDT;
              }
          }
            
        }
     
    }
}
