using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using System.Linq;
using System.Collections.Generic;

namespace QLBV.BaoCao
{
    public partial class rep_GiayGiuThe : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_GiayGiuThe()
        {
            InitializeComponent();
        }
        int sub1 = 0, sub2 = 0;
        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            lblTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            lblTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01071")
           {
               var tenkp = data.KPhongs.Where(p => p.MaKP == DungChung.Bien.MaKP).Select(p => p.TenKP).FirstOrDefault();
               if (tenkp != null)
                   txtKPIn.Text = tenkp;
               txtngayin.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
           }
            _lkhoa.Clear();
            var qcls = (from kp in data.KPhongs.Where(p => p.PLoai.Equals("Cận lâm sàng") && p.Status == 1)
                        select new
                        {
                            kp.TenKP,
                            kp.DChi
                        }).ToList();
            for (int i = 0; i < qcls.Count; i++)
            {
                Khoa k = new Khoa();
                k.Stt = i + 1;
                k.TenKP = qcls[i].TenKP + " " + qcls[i].DChi;
                _lkhoa.Add(k);
            }
            int sokhoa=  _lkhoa.Count();
            int sodu=sokhoa % 3; 
          switch (sodu){
              case 0:
                  sub1 = sokhoa / 3;
                  sub2 = sub1 * 2;
                  break;
              case 1:
                     sub1 = (sokhoa / 3) +1 ;
                     sub2 = ((sokhoa / 3) * 2)+1;
                  break;
              case 2: 
                     sub1 = (sokhoa / 3)+1;
                     sub2 = (sub1 * 2) ;
                  break;
          }

        }

        private class Khoa
        {
            public int Stt { get; set; }
            public string TenKP { get; set; }
        }
        List<Khoa> _lkhoa = new List<Khoa>();

        private void xrSubreport1_BeforePrint(object sender, CancelEventArgs e)
        {
           
            rep_GiayGiuThe_Sub1 repSub = (rep_GiayGiuThe_Sub1)xrSubreport1.ReportSource;
            repSub.DataSource = _lkhoa.Where(p => p.Stt <= sub1).ToList();
            repSub.BindingData();
        }

        private void xrSubreport2_BeforePrint(object sender, CancelEventArgs e)
        {
         
            rep_GiayGiuThe_Sub2 repSub = (rep_GiayGiuThe_Sub2)xrSubreport2.ReportSource;
            repSub.DataSource = _lkhoa.Where(p => p.Stt > sub1 && p.Stt <= sub2).ToList();
            repSub.BindingData();
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
       
        }

        private void xrSubreport3_BeforePrint(object sender, CancelEventArgs e)
        {
          
            xrSubreport3.ReportSource = new  BaoCao.rep_GiayGiuThe_Sub3();
            rep_GiayGiuThe_Sub3 repSub = (rep_GiayGiuThe_Sub3)xrSubreport3.ReportSource;
            repSub.DataSource = _lkhoa.Where(p => p.Stt > sub2).ToList();
            repSub.BindingData();
        }
    }
}
