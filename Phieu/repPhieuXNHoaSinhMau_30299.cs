using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class repPhieuXNHoaSinhMau_30299 : DevExpress.XtraReports.UI.XtraReport
    {
        public repPhieuXNHoaSinhMau_30299()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities DataContect = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
           
            colTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            colTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
          
         
      }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            
            if (MaCBDT.Value!=null)
            {
                colTenBSDT.Text = DungChung.Ham._getTenCB(DataContect, MaCBDT.Value.ToString());
            }
          
            if (Macb.Value!=null)
            {
                colTenTKXN.Text = DungChung.Ham._getTenCB(DataContect, Macb.Value.ToString());
            }
          
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
             var qcls0 = (from dvct in DataContect.DichVucts 
                       join dv in DataContect.DichVus on dvct.MaDV equals dv.MaDV
                          join tnhomdv in DataContect.TieuNhomDVs.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNDichChocDo) on dv.IdTieuNhom equals tnhomdv.IdTieuNhom                       
                       select new {tnhomdv.TenTN,dvct.MaDVct,dvct.TenDVct,dvct.TSBT, dvct.STT, dv.MaQD, tnhomdv.TenRG }).ToList();
             var qcls = qcls0.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau).ToList();
            // var qcls1 = qcls0.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNDichChocDo).ToList();
            if (qcls.Count > 0)
            {
                #region xn hóa sinh máu
                if (qcls.Where(p => p.STT == 1).Count()>0 && qcls.Where(p => p.STT == 1).First().TenDVct != null)
                {
                    lab22.Text = qcls.Where(p => p.STT == 1).First().TenDVct.ToString();                   
                }
                if (qcls.Where(p => p.STT == 1).Count() > 0 && qcls.Where(p => p.STT == 1).First().MaQD != null)
                {                   
                    ma1.Text = qcls.Where(p => p.STT == 1).First().MaQD.ToString();
                }
                if (qcls.Where(p => p.STT == 1).Count() > 0 && qcls.Where(p => p.STT == 1).First().TSBT != null)
                {
                    lab23.Text = qcls.Where(p => p.STT == 1).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 2).Count() > 0 && qcls.Where(p => p.STT == 2).First().TenDVct != null)
                {
                    lab26.Text = qcls.Where(p => p.STT == 2).First().TenDVct.ToString();
                 }
                if (qcls.Where(p => p.STT == 2).Count() > 0 && qcls.Where(p => p.STT == 2).First().MaQD != null)
                {
                    ma2.Text = qcls.Where(p => p.STT == 2).First().MaQD.ToString();
                }
                if (qcls.Where(p => p.STT == 2).Count() > 0 && qcls.Where(p => p.STT == 2).First().TSBT != null)
                {
                    lab27.Text = qcls.Where(p => p.STT == 2).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 3).Count() > 0 && qcls.Where(p => p.STT == 3).First().TenDVct != null)
                {
                    lab30.Text = qcls.Where(p => p.STT == 3).First().TenDVct.ToString();
                }
                if (qcls.Where(p => p.STT == 3).Count() > 0 && qcls.Where(p => p.STT == 3).First().MaQD != null)
                {
                    ma3.Text = qcls.Where(p => p.STT == 3).First().MaQD.ToString();
                }
                if (qcls.Where(p => p.STT == 3).Count() > 0 && qcls.Where(p => p.STT == 3).First().TSBT != null)
                {
                     lab31.Text = qcls.Where(p => p.STT == 3).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 4).Count() > 0 && qcls.Where(p => p.STT == 4).First().TenDVct != null)
                {
                    lab34.Text = qcls.Where(p => p.STT == 4).First().TenDVct.ToString();
                 }
                if (qcls.Where(p => p.STT == 4).Count() > 0 && qcls.Where(p => p.STT == 4).First().MaQD != null)
                {
                    ma4.Text = qcls.Where(p => p.STT == 4).First().MaQD.ToString();
                }
                if (qcls.Where(p => p.STT == 4).Count() > 0 && qcls.Where(p => p.STT == 4).First().TSBT != null)
                {
                     lab35.Text = qcls.Where(p => p.STT == 4).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 5).Count() > 0 && qcls.Where(p => p.STT == 5).First().TenDVct != null)
                {
                    lab38.Text = qcls.Where(p => p.STT == 5).First().TenDVct.ToString();
                 }
                if (qcls.Where(p => p.STT == 5).Count() > 0 && qcls.Where(p => p.STT == 5).First().MaQD != null)
                {
                    ma5.Text = qcls.Where(p => p.STT == 5).First().MaQD.ToString();
                }
                if (qcls.Where(p => p.STT == 5).Count() > 0 && qcls.Where(p => p.STT == 5).First().TSBT != null)
                {
                     lab39.Text = qcls.Where(p => p.STT == 5).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 6).Count() > 0 && qcls.Where(p => p.STT == 6).First().TenDVct != null)
                {
                    lab42.Text = qcls.Where(p => p.STT == 6).First().TenDVct.ToString();
                }
                if (qcls.Where(p => p.STT == 6).Count() > 0 && qcls.Where(p => p.STT == 6).First().MaQD != null)
                {
                    ma6.Text = qcls.Where(p => p.STT == 6).First().MaQD.ToString();
                }
                if (qcls.Where(p => p.STT == 6).Count() > 0 && qcls.Where(p => p.STT == 6).First().TSBT != null)
                {
                     lab43.Text = qcls.Where(p => p.STT == 6).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 7).Count() > 0 && qcls.Where(p => p.STT == 7).First().TenDVct != null)
                {
                    lab46.Text = qcls.Where(p => p.STT == 7).First().TenDVct.ToString();
                  }
                if (qcls.Where(p => p.STT == 7).Count() > 0 && qcls.Where(p => p.STT == 7).First().MaQD != null)
                {
                    ma7.Text = qcls.Where(p => p.STT == 7).First().MaQD.ToString();
                }
                if (qcls.Where(p => p.STT == 7).Count() > 0 && qcls.Where(p => p.STT == 7).First().TSBT != null)
                {
                    lab47.Text = qcls.Where(p => p.STT == 7).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 8).Count() > 0 && qcls.Where(p => p.STT == 8).First().TenDVct != null)
                {
                    lab50.Text = qcls.Where(p => p.STT == 8).First().TenDVct.ToString();
                }
                if (qcls.Where(p => p.STT == 8).Count() > 0 && qcls.Where(p => p.STT == 8).First().MaQD != null)
                {
                    ma8.Text = qcls.Where(p => p.STT == 8).First().MaQD.ToString();
                }
                if (qcls.Where(p => p.STT == 8).Count() > 0 && qcls.Where(p => p.STT == 8).First().TSBT != null)
                {
                    lab51.Text = qcls.Where(p => p.STT == 8).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 9).Count() > 0 && qcls.Where(p => p.STT == 9).First().TenDVct != null)
                {
                    lab54.Text = qcls.Where(p => p.STT == 9).First().TenDVct.ToString();
                }
                if (qcls.Where(p => p.STT == 9).Count() > 0 && qcls.Where(p => p.STT == 9).First().MaQD != null)
                {
                    ma9.Text = qcls.Where(p => p.STT == 9).First().MaQD.ToString();
                }
                if (qcls.Where(p => p.STT == 9).Count() > 0 && qcls.Where(p => p.STT == 9).First().TSBT != null)
                {
                      lab55.Text = qcls.Where(p => p.STT == 9).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 10).Count() > 0 && qcls.Where(p => p.STT == 10).First().TenDVct != null)
                {
                    lab58.Text = qcls.Where(p => p.STT == 10).First().TenDVct.ToString();
                }
                 if (qcls.Where(p => p.STT == 10).Count() > 0 && qcls.Where(p => p.STT == 10).First().MaQD != null)
                {
                    ma10.Text = qcls.Where(p => p.STT == 10).First().MaQD.ToString();
                }
                if (qcls.Where(p => p.STT == 10).Count() > 0 && qcls.Where(p => p.STT == 10).First().TSBT != null)
                {
                     lab59.Text = qcls.Where(p => p.STT == 10).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 11).Count() > 0 && qcls.Where(p => p.STT == 11).First().TenDVct != null)
                {
                    lab62.Text = qcls.Where(p => p.STT == 11).First().TenDVct.ToString();
                }
                 if (qcls.Where(p => p.STT == 11).Count() > 0 && qcls.Where(p => p.STT == 11).First().MaQD != null)
                {
                    ma11.Text = qcls.Where(p => p.STT == 11).First().MaQD.ToString();
                }
                if (qcls.Where(p => p.STT == 11).Count() > 0 && qcls.Where(p => p.STT == 11).First().TSBT != null)
                {
                    lab63.Text = qcls.Where(p => p.STT == 11).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 12).Count() > 0 && qcls.Where(p => p.STT == 12).First().TenDVct != null)
                {
                    lab66.Text = qcls.Where(p => p.STT == 12).First().TenDVct.ToString();
                 }
                 if (qcls.Where(p => p.STT == 12).Count() > 0 && qcls.Where(p => p.STT == 12).First().MaQD != null)
                {
                    ma12.Text = qcls.Where(p => p.STT == 12).First().MaQD.ToString();
                }
                if (qcls.Where(p => p.STT == 12).Count() > 0 && qcls.Where(p => p.STT == 12).First().TSBT != null)
                {
                     lab67.Text = qcls.Where(p => p.STT == 12).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 13).Count() > 0 && qcls.Where(p => p.STT == 13).First().TenDVct != null)
                {
                    lab70.Text = qcls.Where(p => p.STT == 13).First().TenDVct.ToString();
                  }
                 if (qcls.Where(p => p.STT == 13).Count() > 0 && qcls.Where(p => p.STT == 13).First().MaQD != null)
                {
                    ma13.Text = qcls.Where(p => p.STT == 13).First().MaQD.ToString();
                }
                if (qcls.Where(p => p.STT == 13).Count() > 0 && qcls.Where(p => p.STT == 13).First().TSBT != null)
                {
                    lab71.Text = qcls.Where(p => p.STT == 13).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 14).Count() > 0 && qcls.Where(p => p.STT == 14).First().TenDVct != null)
                {
                    lab74.Text = qcls.Where(p => p.STT == 14).First().TenDVct.ToString();
                }
                if (qcls.Where(p => p.STT == 14).Count() > 0 && qcls.Where(p => p.STT == 14).First().TSBT != null)
                {
                     lab75.Text = qcls.Where(p => p.STT == 14).First().TSBT.ToString();
                }
                 if (qcls.Where(p => p.STT == 14).Count() > 0 && qcls.Where(p => p.STT == 14).First().MaQD != null)
                {
                    ma14.Text = qcls.Where(p => p.STT == 14).First().MaQD.ToString();
                }
                if (qcls.Where(p => p.STT == 15).Count() > 0 && qcls.Where(p => p.STT == 15).First().TenDVct != null)
                {
                    lab78.Text = qcls.Where(p => p.STT == 15).First().TenDVct.ToString();
                }
                 if (qcls.Where(p => p.STT == 15).Count() > 0 && qcls.Where(p => p.STT == 15).First().MaQD != null)
                {
                    ma15.Text = qcls.Where(p => p.STT == 15).First().MaQD.ToString();
                }
                if (qcls.Where(p => p.STT == 15).Count() > 0 && qcls.Where(p => p.STT == 15).First().TSBT != null)
                {
                     lab79.Text = qcls.Where(p => p.STT == 15).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 16).Count() > 0 && qcls.Where(p => p.STT == 16).First().TenDVct != null)
                {
                    lab82.Text = qcls.Where(p => p.STT == 16).First().TenDVct.ToString();
                 }
                if (qcls.Where(p => p.STT == 16).Count() > 0 && qcls.Where(p => p.STT == 16).First().TSBT != null)
                {
                    lab83.Text = qcls.Where(p => p.STT == 16).First().TSBT.ToString();
                }
                 if (qcls.Where(p => p.STT == 16).Count() > 0 && qcls.Where(p => p.STT == 16).First().MaQD != null)
                {
                    ma16.Text = qcls.Where(p => p.STT == 16).First().MaQD.ToString();
                }
                if (qcls.Where(p => p.STT == 17).Count() > 0 && qcls.Where(p => p.STT == 17).First().TenDVct != null)
                {
                    lab86.Text = qcls.Where(p => p.STT == 17).First().TenDVct.ToString();
                 }
                 if (qcls.Where(p => p.STT == 17).Count() > 0 && qcls.Where(p => p.STT == 17).First().MaQD != null)
                {
                    ma17.Text = qcls.Where(p => p.STT == 17).First().MaQD.ToString();
                }
                if (qcls.Where(p => p.STT == 17).Count() > 0 && qcls.Where(p => p.STT == 17).First().TSBT != null)
                {
                    lab87.Text = qcls.Where(p => p.STT == 17).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 18).Count() > 0 && qcls.Where(p => p.STT == 18).First().TenDVct != null)
                {
                    lab90.Text = qcls.Where(p => p.STT == 18).First().TenDVct.ToString();
                }
                 if (qcls.Where(p => p.STT == 18).Count() > 0 && qcls.Where(p => p.STT == 18).First().MaQD != null)
                {
                    ma18.Text = qcls.Where(p => p.STT == 18).First().MaQD.ToString();
                }
                if (qcls.Where(p => p.STT == 18).Count() > 0 && qcls.Where(p => p.STT == 18).First().TSBT != null)
                {
                     lab91.Text = qcls.Where(p => p.STT == 18).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 19).Count() > 0 && qcls.Where(p => p.STT == 19).First().TenDVct != null)
                {
                    lab94.Text = qcls.Where(p => p.STT == 19).First().TenDVct.ToString();
                }
                 if (qcls.Where(p => p.STT == 19).Count() > 0 && qcls.Where(p => p.STT == 19).First().MaQD != null)
                {
                    ma19.Text = qcls.Where(p => p.STT == 19).First().MaQD.ToString();
                }
                if (qcls.Where(p => p.STT == 19).Count() > 0 && qcls.Where(p => p.STT == 19).First().TSBT != null)
                {
                     lab95.Text = qcls.Where(p => p.STT == 19).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 20).Count() > 0 && qcls.Where(p => p.STT == 20).First().TenDVct != null)
                {
                    lab98.Text = qcls.Where(p => p.STT == 20).First().TenDVct.ToString();
                }
                 if (qcls.Where(p => p.STT == 20).Count() > 0 && qcls.Where(p => p.STT == 20).First().MaQD != null)
                {
                    ma20.Text = qcls.Where(p => p.STT == 20).First().MaQD.ToString();
                }
                if (qcls.Where(p => p.STT == 20).Count() > 0 && qcls.Where(p => p.STT == 20).First().TSBT != null)
                {
                     lab99.Text = qcls.Where(p => p.STT == 20).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 21).Count() > 0 && qcls.Where(p => p.STT == 21).First().TenDVct != null)
                {
                    lab102.Text = qcls.Where(p => p.STT == 21).First().TenDVct.ToString();
                 }
                 if (qcls.Where(p => p.STT == 21).Count() > 0 && qcls.Where(p => p.STT == 21).First().MaQD != null)
                {
                    ma21.Text = qcls.Where(p => p.STT == 21).First().MaQD.ToString();
                }
                if (qcls.Where(p => p.STT == 21).Count() > 0 && qcls.Where(p => p.STT == 21).First().TSBT != null)
                {
                     lab103.Text = qcls.Where(p => p.STT == 21).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 22).Count() > 0 && qcls.Where(p => p.STT == 22).First().TenDVct != null)
                {
                    lab106.Text = qcls.Where(p => p.STT == 22).First().TenDVct.ToString();
                 }
                 if (qcls.Where(p => p.STT == 22).Count() > 0 && qcls.Where(p => p.STT == 22).First().MaQD != null)
                {
                    ma22.Text = qcls.Where(p => p.STT == 22).First().MaQD.ToString();
                }
                if (qcls.Where(p => p.STT == 22).Count() > 0 && qcls.Where(p => p.STT == 22).First().TSBT != null)
                {
                    lab107.Text = qcls.Where(p => p.STT == 22).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 23).Count() > 0 && qcls.Where(p => p.STT == 23).First().TenDVct != null)
                {
                    lab24.Text = qcls.Where(p => p.STT == 23).First().TenDVct.ToString();
                 }
                 if (qcls.Where(p => p.STT == 23).Count() > 0 && qcls.Where(p => p.STT == 23).First().MaQD != null)
                {
                    ma23.Text = qcls.Where(p => p.STT == 23).First().MaQD.ToString();
                }
                if (qcls.Where(p => p.STT == 23).Count() > 0 && qcls.Where(p => p.STT == 23).First().TSBT != null)
                {
                    lab25.Text = qcls.Where(p => p.STT == 23).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 24).Count() > 0 && qcls.Where(p => p.STT == 24).First().TenDVct != null)
                {
                    lab28.Text = qcls.Where(p => p.STT == 24).First().TenDVct.ToString();
                }
                 if (qcls.Where(p => p.STT == 24).Count() > 0 && qcls.Where(p => p.STT == 24 ).First().MaQD != null)
                {
                    ma24.Text = qcls.Where(p => p.STT == 24).First().MaQD.ToString();
                }
                if (qcls.Where(p => p.STT == 24).Count() > 0 && qcls.Where(p => p.STT == 24).First().TSBT != null)
                {
                     lab29.Text = qcls.Where(p => p.STT == 24).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 25).Count() > 0 && qcls.Where(p => p.STT == 25).First().TenDVct != null)
                {
                    lab32.Text = qcls.Where(p => p.STT == 25).First().TenDVct.ToString();
                }
                 if (qcls.Where(p => p.STT == 25).Count() > 0 && qcls.Where(p => p.STT == 25).First().MaQD != null)
                {
                    ma25.Text = qcls.Where(p => p.STT == 25).First().MaQD.ToString();
                }
                if (qcls.Where(p => p.STT == 25).Count() > 0 && qcls.Where(p => p.STT == 25).First().TSBT != null)
                {
                    lab33.Text = qcls.Where(p => p.STT == 25).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 26).Count() > 0 && qcls.Where(p => p.STT == 26).First().TenDVct != null)
                {
                    lab36.Text = qcls.Where(p => p.STT == 26).First().TenDVct.ToString();
                 }
                 if (qcls.Where(p => p.STT == 26).Count() > 0 && qcls.Where(p => p.STT == 26).First().MaQD != null)
                {
                    ma26.Text = qcls.Where(p => p.STT == 26).First().MaQD.ToString();
                }
                if (qcls.Where(p => p.STT == 26).Count() > 0 && qcls.Where(p => p.STT == 26).First().TSBT != null)
                {
                     lab37.Text = qcls.Where(p => p.STT == 26).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 27).Count() > 0 && qcls.Where(p => p.STT == 27).First().TenDVct != null)
                {
                    lab40.Text = qcls.Where(p => p.STT == 27).First().TenDVct.ToString();
                 }
                 if (qcls.Where(p => p.STT == 27).Count() > 0 && qcls.Where(p => p.STT == 27).First().MaQD != null)
                {
                    ma27.Text = qcls.Where(p => p.STT == 27).First().MaQD.ToString();
                }
                if (qcls.Where(p => p.STT == 27).Count() > 0 && qcls.Where(p => p.STT == 27).First().TSBT != null)
                {
                    lab41.Text = qcls.Where(p => p.STT == 27).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 28).Count() > 0 && qcls.Where(p => p.STT == 28).First().TenDVct != null)
                {
                    lab44.Text = qcls.Where(p => p.STT == 28).First().TenDVct.ToString();
                }
                 if (qcls.Where(p => p.STT == 28).Count() > 0 && qcls.Where(p => p.STT == 28).First().MaQD != null)
                {
                    ma28.Text = qcls.Where(p => p.STT == 28).First().MaQD.ToString();
                }
                if (qcls.Where(p => p.STT == 28).Count() > 0 && qcls.Where(p => p.STT == 28).First().TSBT != null)
                {
                     lab45.Text = qcls.Where(p => p.STT == 28).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 29).Count() > 0 && qcls.Where(p => p.STT == 29).First().TenDVct != null)
                {
                    lab48.Text = qcls.Where(p => p.STT == 29).First().TenDVct.ToString();
                    }
                 if (qcls.Where(p => p.STT == 29).Count() > 0 && qcls.Where(p => p.STT == 29).First().MaQD != null)
                {
                    ma29.Text = qcls.Where(p => p.STT ==29 ).First().MaQD.ToString();
                }
                if (qcls.Where(p => p.STT == 29).Count() > 0 && qcls.Where(p => p.STT == 29).First().TSBT != null)
                {
                    lab49.Text = qcls.Where(p => p.STT == 29).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 30).Count() > 0 && qcls.Where(p => p.STT == 30).First().TenDVct != null)
                {
                    lab52.Text = qcls.Where(p => p.STT == 30).First().TenDVct.ToString();
                 }
                 if (qcls.Where(p => p.STT == 30).Count() > 0 && qcls.Where(p => p.STT == 30).First().MaQD != null)
                {
                    ma30.Text = qcls.Where(p => p.STT == 30).First().MaQD.ToString();
                }
                if (qcls.Where(p => p.STT == 30).Count() > 0 && qcls.Where(p => p.STT == 30).First().TSBT != null)
                {
                    lab53.Text = qcls.Where(p => p.STT == 30).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 31).Count() > 0 && qcls.Where(p => p.STT == 31).First().TenDVct != null)
                {
                    lab56.Text = qcls.Where(p => p.STT == 31).First().TenDVct.ToString();
                }
                if (qcls.Where(p => p.STT == 31).Count() > 0 && qcls.Where(p => p.STT == 31).First().MaQD != null)
                {
                    ma31.Text = qcls.Where(p => p.STT == 31).First().MaQD.ToString();
                }
                if (qcls.Where(p => p.STT == 31).Count() > 0 && qcls.Where(p => p.STT == 31).First().TSBT != null)
                {
                    lab57.Text = qcls.Where(p => p.STT == 31).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 32).Count() > 0 && qcls.Where(p => p.STT == 32).First().TenDVct != null)
                {
                    lab60.Text = qcls.Where(p => p.STT == 32).First().TenDVct.ToString();
                }
                 if (qcls.Where(p => p.STT == 32).Count() > 0 && qcls.Where(p => p.STT == 32).First().MaQD != null)
                {
                    ma32.Text = qcls.Where(p => p.STT == 32).First().MaQD.ToString();
                }
                if (qcls.Where(p => p.STT == 32).Count() > 0 && qcls.Where(p => p.STT == 32).First().TSBT != null)
                {
                    lab61.Text = qcls.Where(p => p.STT == 32).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 33).Count() > 0 && qcls.Where(p => p.STT == 33).First().TenDVct != null)
                {
                    lab64.Text = qcls.Where(p => p.STT == 33).First().TenDVct.ToString();
                }
                 if (qcls.Where(p => p.STT == 33).Count() > 0 && qcls.Where(p => p.STT == 33).First().MaQD != null)
                {
                    ma33.Text = qcls.Where(p => p.STT == 33).First().MaQD.ToString();
                }
                if (qcls.Where(p => p.STT == 33).Count() > 0 && qcls.Where(p => p.STT == 33).First().TSBT != null)
                {
                     lab65.Text = qcls.Where(p => p.STT == 33).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 34).Count() > 0 && qcls.Where(p => p.STT == 34).First().TenDVct != null)
                {
                    lab68.Text = qcls.Where(p => p.STT == 34).First().TenDVct.ToString();
                }
                 if (qcls.Where(p => p.STT == 34).Count() > 0 && qcls.Where(p => p.STT == 34).First().MaQD != null)
                {
                    ma34.Text = qcls.Where(p => p.STT == 34).First().MaQD.ToString();
                }
                if (qcls.Where(p => p.STT == 34).Count() > 0 && qcls.Where(p => p.STT == 34).First().TSBT != null)
                {
                     lab69.Text = qcls.Where(p => p.STT == 34).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 35).Count() > 0 && qcls.Where(p => p.STT == 35).First().TenDVct != null)
                {
                    lab72.Text = qcls.Where(p => p.STT == 35).First().TenDVct.ToString();
                }
                 if (qcls.Where(p => p.STT == 35).Count() > 0 && qcls.Where(p => p.STT == 35).First().MaQD != null)
                {
                    ma35.Text = qcls.Where(p => p.STT == 35).First().MaQD.ToString();
                }
                if (qcls.Where(p => p.STT == 35).Count() > 0 && qcls.Where(p => p.STT == 35).First().TSBT != null)
                {
                     lab73.Text = qcls.Where(p => p.STT == 35).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 36).Count() > 0 && qcls.Where(p => p.STT == 36).First().TenDVct != null)
                {
                    lab76.Text = qcls.Where(p => p.STT == 36).First().TenDVct.ToString();
                 }
                 if (qcls.Where(p => p.STT == 36).Count() > 0 && qcls.Where(p => p.STT == 36).First().MaQD != null)
                {
                    ma36.Text = qcls.Where(p => p.STT == 36).First().MaQD.ToString();
                }
                if (qcls.Where(p => p.STT == 36).Count() > 0 && qcls.Where(p => p.STT == 36).First().TSBT != null)
                {
                    lab77.Text = qcls.Where(p => p.STT == 36).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 37).Count() > 0 && qcls.Where(p => p.STT == 37).First().TenDVct != null)
                {
                    lab80.Text = qcls.Where(p => p.STT == 37).First().TenDVct.ToString();
                }
                 if (qcls.Where(p => p.STT ==37 ).Count() > 0 && qcls.Where(p => p.STT == 37).First().MaQD != null)
                {
                    ma37.Text = qcls.Where(p => p.STT ==37 ).First().MaQD.ToString();
                }
                if (qcls.Where(p => p.STT == 37).Count() > 0 && qcls.Where(p => p.STT == 37).First().TSBT != null)
                {
                    lab81.Text = qcls.Where(p => p.STT == 37).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 38).Count() > 0 && qcls.Where(p => p.STT == 38).First().TenDVct != null)
                {
                    lab84.Text = qcls.Where(p => p.STT == 38).First().TenDVct.ToString();
                }
                 if (qcls.Where(p => p.STT ==38 ).Count() > 0 && qcls.Where(p => p.STT == 38).First().MaQD != null)
                {
                    ma38.Text = qcls.Where(p => p.STT == 38).First().MaQD.ToString();
                }
                if (qcls.Where(p => p.STT == 38).Count() > 0 && qcls.Where(p => p.STT == 38).First().TSBT != null)
                {
                    lab85.Text = qcls.Where(p => p.STT == 38).First().TSBT.ToString();
                }

                #endregion
                #region xét nghiệm dịch chọc dò
                //if (qcls1.Count > 0)
                //{
                //    if (qcls.Where(p => p.STT == 1).Count() > 0 && qcls.Where(p => p.STT == 1).First().TenDVct != null)
                //    {
                //        lab92.Text = qcls.Where(p => p.STT == 1).First().TenDVct.ToString();                       
                //    }
                //    if (qcls.Where(p => p.STT == 1).Count() > 0 && qcls.Where(p => p.STT == 1).First().MaQD != null)
                //    {
                //        ma39.Text = qcls.Where(p => p.STT == 1).First().MaQD.ToString();
                //    }
                //    if (qcls.Where(p => p.STT == 1).Count() > 0 && qcls.Where(p => p.STT == 1).First().TSBT != null)
                //    {
                //        lab93.Text = qcls.Where(p => p.STT == 1).First().TSBT.ToString();
                //    }
                //    if (qcls.Where(p => p.STT == 2).Count() > 0 && qcls.Where(p => p.STT == 2).First().TenDVct != null)
                //    {
                //        lab96.Text = qcls.Where(p => p.STT == 2).First().TenDVct.ToString();
                //    }
                //    if (qcls.Where(p => p.STT == 2).Count() > 0 && qcls.Where(p => p.STT == 2).First().MaQD != null)
                //    {
                //        ma40.Text = qcls.Where(p => p.STT == 2).First().MaQD.ToString();
                //    }
                //    if (qcls.Where(p => p.STT == 2).Count() > 0 && qcls.Where(p => p.STT == 2).First().TSBT != null)
                //    {
                //        lab97.Text = qcls.Where(p => p.STT == 2).First().TSBT.ToString();
                //    }
                //    if (qcls.Where(p => p.STT == 3).Count() > 0 && qcls.Where(p => p.STT == 3).First().TenDVct != null)
                //    {
                //        lab100.Text = qcls.Where(p => p.STT == 3).First().TenDVct.ToString();
                //    }
                //    if (qcls.Where(p => p.STT == 3).Count() > 0 && qcls.Where(p => p.STT == 3).First().MaQD != null)
                //    {
                //        ma41.Text = qcls.Where(p => p.STT == 3).First().MaQD.ToString();
                //    }
                //    if (qcls.Where(p => p.STT == 3).Count() > 0 && qcls.Where(p => p.STT == 3).First().TSBT != null)
                //    {
                //        lab101.Text = qcls.Where(p => p.STT == 3).First().TSBT.ToString();
                //    }
                //    if (qcls.Where(p => p.STT == 4).Count() > 0 && qcls.Where(p => p.STT == 4).First().TenDVct != null)
                //    {
                //        lab104.Text = qcls.Where(p => p.STT == 4).First().TenDVct.ToString();
                //    }
                //    if (qcls.Where(p => p.STT == 4).Count() > 0 && qcls.Where(p => p.STT == 4).First().MaQD != null)
                //    {
                //        ma42.Text = qcls.Where(p => p.STT == 4).First().MaQD.ToString();
                //    }
                //    if (qcls.Where(p => p.STT == 4).Count() > 0 && qcls.Where(p => p.STT == 4).First().TSBT != null)
                //    {
                //        lab105.Text = qcls.Where(p => p.STT == 4).First().TSBT.ToString();
                //    }
                //    if (qcls.Where(p => p.STT == 5).Count() > 0 && qcls.Where(p => p.STT == 5).First().TenDVct != null)
                //    {
                //        lab108.Text = qcls.Where(p => p.STT == 5).First().TenDVct.ToString();
                //    }
                //    if (qcls.Where(p => p.STT == 5).Count() > 0 && qcls.Where(p => p.STT == 5).First().MaQD != null)
                //    {
                //        ma43.Text = qcls.Where(p => p.STT == 5).First().MaQD.ToString();
                //    }
                //    if (qcls.Where(p => p.STT == 5).Count() > 0 && qcls.Where(p => p.STT == 5).First().TSBT != null)
                //    {
                //        lab109.Text = qcls.Where(p => p.STT == 5).First().TSBT.ToString();
                //    }
                //}
                #endregion
                int sophieu = int.Parse(SoPhieu.Value.ToString());
                //int sophieu = 956;
                var qhh = ( from cls in DataContect.CLS.Where(p=>p.IdCLS==sophieu)
                            join chidinh in DataContect.ChiDinhs on cls.IdCLS equals chidinh.IdCLS
                            join dichvu in DataContect.DichVus on chidinh.MaDV equals dichvu.MaDV
                            join tn in DataContect.TieuNhomDVs on dichvu.IdTieuNhom equals tn.IdTieuNhom
                            join clsct in DataContect.CLScts on chidinh.IDCD equals clsct.IDCD
                           join dvct in DataContect.DichVucts on clsct.MaDVct  equals dvct.MaDVct
                           select new { clsct.MaDVct, clsct.KetQua, dvct.STT, tn.TenRG }).ToList();
               
                if (qhh.Count > 0 && qhh.First().TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau)
                {
                    #region xét nghiệm hóa sinh máu
                    if (qhh.Where(p => p.STT == 1).Count() > 0 && qhh.Where(p => p.STT == 1).First().MaDVct != null)
                    {
                        a1.Text = "X";
                    }
                    if (qhh.Where(p => p.STT == 1).Count() > 0 && qhh.Where(p => p.STT == 1).First().KetQua != null)
                    {
                        col1.Text = qhh.Where(p => p.STT == 1).First().KetQua.ToString();
                    }
                    if (qhh.Where(p => p.STT == 2).Count() > 0 && qhh.Where(p => p.STT == 2).First().MaDVct != null)
                    {
                        a2.Text = "X";
                    }
                    if (qhh.Where(p => p.STT == 2).Count() > 0 && qhh.Where(p => p.STT == 2).First().KetQua != null)
                    {
                        col2.Text = qhh.Where(p => p.STT == 2).First().KetQua.ToString();
                    }
                    if (qhh.Where(p => p.STT == 3).Count() > 0 && qhh.Where(p => p.STT == 3).First().MaDVct != null)
                    {
                        a3.Text = "X";
                    }
                    if (qhh.Where(p => p.STT == 3).Count() > 0 && qhh.Where(p => p.STT == 3).First().KetQua != null)
                    {
                        col3.Text = qhh.Where(p => p.STT == 3).First().KetQua.ToString();
                    }
                    if (qhh.Where(p => p.STT == 4).Count() > 0 && qhh.Where(p => p.STT == 4).First().MaDVct != null)
                    {
                        a4.Text = "X";
                    }
                    if (qhh.Where(p => p.STT == 4).Count() > 0 && qhh.Where(p => p.STT == 4).First().KetQua != null)
                    {
                        col4.Text = qhh.Where(p => p.STT == 4).First().KetQua.ToString();
                    }
                    if (qhh.Where(p => p.STT == 5).Count() > 0 && qhh.Where(p => p.STT == 5).First().MaDVct != null)
                    {
                        a5.Text = "X";
                    }
                    if (qhh.Where(p => p.STT == 5).Count() > 0 && qhh.Where(p => p.STT == 5).First().KetQua != null)
                    {
                        col5.Text = qhh.Where(p => p.STT == 5).First().KetQua.ToString();
                    }
                    if (qhh.Where(p => p.STT == 6).Count() > 0 && qhh.Where(p => p.STT == 6).First().MaDVct != null)
                    {
                        a6.Text = "X";
                    }
                    if (qhh.Where(p => p.STT == 6).Count() > 0 && qhh.Where(p => p.STT == 6).First().KetQua != null)
                    {
                        col6.Text = qhh.Where(p => p.STT == 6).First().KetQua.ToString();
                    }
                    if (qhh.Where(p => p.STT == 7).Count() > 0 && qhh.Where(p => p.STT == 7).First().MaDVct != null)
                    {
                        a7.Text = "X";
                    }
                    if (qhh.Where(p => p.STT == 7).Count() > 0 && qhh.Where(p => p.STT == 7).First().KetQua != null)
                    {
                        col7.Text = qhh.Where(p => p.STT == 7).First().KetQua.ToString();
                    }
                    if (qhh.Where(p => p.STT == 8).Count() > 0 && qhh.Where(p => p.STT == 8).First().MaDVct != null)
                    {
                        a8.Text = "X";
                    }
                    if (qhh.Where(p => p.STT == 8).Count() > 0 && qhh.Where(p => p.STT == 8).First().KetQua != null)
                    {
                        col8.Text = qhh.Where(p => p.STT == 8).First().KetQua.ToString();
                    }
                    if (qhh.Where(p => p.STT == 9).Count() > 0 && qhh.Where(p => p.STT == 9).First().MaDVct != null)
                    {
                        a9.Text = "X";
                    }
                    if (qhh.Where(p => p.STT == 9).Count() > 0 && qhh.Where(p => p.STT == 9).First().KetQua != null)
                    {
                        col9.Text = qhh.Where(p => p.STT == 9).First().KetQua.ToString();
                    }
                    if (qhh.Where(p => p.STT == 10).Count() > 0 && qhh.Where(p => p.STT == 10).First().MaDVct != null)
                    {
                        a10.Text = "X";
                    }
                    if (qhh.Where(p => p.STT == 10).Count() > 0 && qhh.Where(p => p.STT == 10).First().KetQua != null)
                    {
                        col10.Text = qhh.Where(p => p.STT == 10).First().KetQua.ToString();
                    }
                    if (qhh.Where(p => p.STT == 11).Count() > 0 && qhh.Where(p => p.STT == 11).First().MaDVct != null)
                    {
                        a11.Text = "X";
                    }
                    if (qhh.Where(p => p.STT == 11).Count() > 0 && qhh.Where(p => p.STT == 11).First().KetQua != null)
                    {
                        col11.Text = qhh.Where(p => p.STT == 11).First().KetQua.ToString();
                    }
                    if (qhh.Where(p => p.STT == 12).Count() > 0 && qhh.Where(p => p.STT == 12).First().MaDVct != null)
                    {
                        a12.Text = "X";
                    }
                    if (qhh.Where(p => p.STT == 12).Count() > 0 && qhh.Where(p => p.STT == 12).First().KetQua != null)
                    {
                        col12.Text = qhh.Where(p => p.STT == 12).First().KetQua.ToString();
                    }
                    if (qhh.Where(p => p.STT == 13).Count() > 0 && qhh.Where(p => p.STT == 13).First().MaDVct != null)
                    {
                        a13.Text = "X";
                    }
                    if (qhh.Where(p => p.STT == 13).Count() > 0 && qhh.Where(p => p.STT == 13).First().KetQua != null)
                    {
                        col13.Text = qhh.Where(p => p.STT == 13).First().KetQua.ToString();
                    }
                    if (qhh.Where(p => p.STT == 14).Count() > 0 && qhh.Where(p => p.STT == 14).First().MaDVct != null)
                    {
                        a14.Text = "X";
                    }
                    if (qhh.Where(p => p.STT == 14).Count() > 0 && qhh.Where(p => p.STT == 14).First().KetQua != null)
                    {
                        col14.Text = qhh.Where(p => p.STT == 14).First().KetQua.ToString();
                    }
                    if (qhh.Where(p => p.STT == 15).Count() > 0 && qhh.Where(p => p.STT == 15).First().MaDVct != null)
                    {
                        a15.Text = "X";
                    }
                    if (qhh.Where(p => p.STT == 15).Count() > 0 && qhh.Where(p => p.STT == 15).First().KetQua != null)
                    {
                        col15.Text = qhh.Where(p => p.STT == 15).First().KetQua.ToString();
                    }
                    if (qhh.Where(p => p.STT == 16).Count() > 0 && qhh.Where(p => p.STT == 16).First().MaDVct != null)
                    {
                        a16.Text = "X";
                    }
                    if (qhh.Where(p => p.STT == 16).Count() > 0 && qhh.Where(p => p.STT == 16).First().KetQua != null)
                    {
                        col16.Text = qhh.Where(p => p.STT == 16).First().KetQua.ToString();
                    }
                    if (qhh.Where(p => p.STT == 17).Count() > 0 && qhh.Where(p => p.STT == 17).First().MaDVct != null)
                    {
                        a17.Text = "X";
                    }
                    if (qhh.Where(p => p.STT == 17).Count() > 0 && qhh.Where(p => p.STT == 17).First().KetQua != null)
                    {
                        col17.Text = qhh.Where(p => p.STT == 17).First().KetQua.ToString();
                    }
                    if (qhh.Where(p => p.STT == 18).Count() > 0 && qhh.Where(p => p.STT == 18).First().MaDVct != null)
                    {
                        a18.Text = "X";
                    }
                    if (qhh.Where(p => p.STT == 18).Count() > 0 && qhh.Where(p => p.STT == 18).First().KetQua != null)
                    {
                        col18.Text = qhh.Where(p => p.STT == 18).First().KetQua.ToString();
                    }
                    if (qhh.Where(p => p.STT == 19).Count() > 0 && qhh.Where(p => p.STT == 19).First().MaDVct != null)
                    {
                        a19.Text = "X";
                    }
                    if (qhh.Where(p => p.STT == 19).Count() > 0 && qhh.Where(p => p.STT == 19).First().KetQua != null)
                    {
                        col19.Text = qhh.Where(p => p.STT == 19).First().KetQua.ToString();
                    }
                    if (qhh.Where(p => p.STT == 20).Count() > 0 && qhh.Where(p => p.STT == 20).First().MaDVct != null)
                    {
                        a20.Text = "X";
                    }
                    if (qhh.Where(p => p.STT == 20).Count() > 0 && qhh.Where(p => p.STT == 20).First().KetQua != null)
                    {
                        col20.Text = qhh.Where(p => p.STT == 20).First().KetQua.ToString();
                    }
                    if (qhh.Where(p => p.STT == 21).Count() > 0 && qhh.Where(p => p.STT == 21).First().MaDVct != null)
                    {
                        a21.Text = "X";
                    }
                    if (qhh.Where(p => p.STT == 21).Count() > 0 && qhh.Where(p => p.STT == 21).First().KetQua != null)
                    {
                        col21.Text = qhh.Where(p => p.STT == 21).First().KetQua.ToString();
                    }
                    if (qhh.Where(p => p.STT == 22).Count() > 0 && qhh.Where(p => p.STT == 22).First().MaDVct != null)
                    {
                        a22.Text = "X";
                    }
                    if (qhh.Where(p => p.STT == 22).Count() > 0 && qhh.Where(p => p.STT == 22).First().KetQua != null)
                    {
                        col22.Text = qhh.Where(p => p.STT == 22).First().KetQua.ToString();
                    }
                    if (qhh.Where(p => p.STT == 23).Count() > 0 && qhh.Where(p => p.STT == 23).First().MaDVct != null)
                    {
                        a23.Text = "X";
                    }
                    if (qhh.Where(p => p.STT == 23).Count() > 0 && qhh.Where(p => p.STT == 23).First().KetQua != null)
                    {
                        col23.Text = qhh.Where(p => p.STT == 23).First().KetQua.ToString();
                    }
                    if (qhh.Where(p => p.STT == 24).Count() > 0 && qhh.Where(p => p.STT == 24).First().MaDVct != null)
                    {
                        a24.Text = "X";
                    }
                    if (qhh.Where(p => p.STT == 24).Count() > 0 && qhh.Where(p => p.STT == 24).First().KetQua != null)
                    {
                        col24.Text = qhh.Where(p => p.STT == 24).First().KetQua.ToString();
                    }
                    if (qhh.Where(p => p.STT == 25).Count() > 0 && qhh.Where(p => p.STT == 25).First().MaDVct != null)
                    {
                        a25.Text = "X";
                    }
                    if (qhh.Where(p => p.STT == 25).Count() > 0 && qhh.Where(p => p.STT == 25).First().KetQua != null)
                    {
                        col25.Text = qhh.Where(p => p.STT == 25).First().KetQua.ToString();
                    }
                    if (qhh.Where(p => p.STT == 26).Count() > 0 && qhh.Where(p => p.STT == 26).First().MaDVct != null)
                    {
                        a26.Text = "X";
                    }
                    if (qhh.Where(p => p.STT == 26).Count() > 0 && qhh.Where(p => p.STT == 26).First().KetQua != null)
                    {
                        col26.Text = qhh.Where(p => p.STT == 26).First().KetQua.ToString();
                    }
                    if (qhh.Where(p => p.STT == 27).Count() > 0 && qhh.Where(p => p.STT == 27).First().MaDVct != null)
                    {
                        a27.Text = "X";
                    }
                    if (qhh.Where(p => p.STT == 27).Count() > 0 && qhh.Where(p => p.STT == 27).First().KetQua != null)
                    {
                        col27.Text = qhh.Where(p => p.STT == 27).First().KetQua.ToString();
                    }
                    if (qhh.Where(p => p.STT == 28).Count() > 0 && qhh.Where(p => p.STT ==28).First().MaDVct != null)
                    {
                        a28.Text = "X";
                    }
                    if (qhh.Where(p => p.STT == 28).Count() > 0 && qhh.Where(p => p.STT == 28).First().KetQua != null)
                    {
                        col28.Text = qhh.Where(p => p.STT == 28).First().KetQua.ToString();
                    }
                    if (qhh.Where(p => p.STT == 29).Count() > 0 && qhh.Where(p => p.STT == 29).First().MaDVct != null)
                    {
                        a29.Text = "X";
                    }
                    if (qhh.Where(p => p.STT == 29).Count() > 0 && qhh.Where(p => p.STT == 29).First().KetQua != null)
                    {
                        col29.Text = qhh.Where(p => p.STT == 29).First().KetQua.ToString();
                    }
                    if (qhh.Where(p => p.STT == 30).Count() > 0 && qhh.Where(p => p.STT == 30).First().MaDVct != null)
                    {
                        a30.Text = "X";
                    }
                    if (qhh.Where(p => p.STT == 30).Count() > 0 && qhh.Where(p => p.STT == 30).First().KetQua != null)
                    {
                        col30.Text = qhh.Where(p => p.STT == 30).First().KetQua.ToString();
                    }
                    if (qhh.Where(p => p.STT == 31).Count() > 0 && qhh.Where(p => p.STT == 31).First().MaDVct != null)
                    {
                        a31.Text = "X";
                    }
                    if (qhh.Where(p => p.STT == 31).Count() > 0 && qhh.Where(p => p.STT == 31).First().KetQua != null)
                    {
                        col31.Text = qhh.Where(p => p.STT == 31).First().KetQua.ToString();
                    }
                    if (qhh.Where(p => p.STT == 32).Count() > 0 && qhh.Where(p => p.STT == 32).First().MaDVct != null)
                    {
                        a32.Text = "X";
                    }
                    if (qhh.Where(p => p.STT == 32).Count() > 0 && qhh.Where(p => p.STT == 32).First().KetQua != null)
                    {
                        col32.Text = qhh.Where(p => p.STT == 32).First().KetQua.ToString();
                    }
                    if (qhh.Where(p => p.STT == 33).Count() > 0 && qhh.Where(p => p.STT == 33).First().MaDVct != null)
                    {
                        a33.Text = "X";
                    }
                    if (qhh.Where(p => p.STT == 33).Count() > 0 && qhh.Where(p => p.STT == 33).First().KetQua != null)
                    {
                        col33.Text = qhh.Where(p => p.STT == 33).First().KetQua.ToString();
                    }
                    if (qhh.Where(p => p.STT == 34).Count() > 0 && qhh.Where(p => p.STT == 34).First().MaDVct != null)
                    {
                        a34.Text = "X";
                    }
                    if (qhh.Where(p => p.STT == 34).Count() > 0 && qhh.Where(p => p.STT == 34).First().KetQua != null)
                    {
                        col34.Text = qhh.Where(p => p.STT == 34).First().KetQua.ToString();
                    }
                    if (qhh.Where(p => p.STT == 35).Count() > 0 && qhh.Where(p => p.STT == 35).First().MaDVct != null)
                    {
                        a35.Text = "X";
                    }
                    if (qhh.Where(p => p.STT == 35).Count() > 0 && qhh.Where(p => p.STT == 35).First().KetQua != null)
                    {
                        col35.Text = qhh.Where(p => p.STT == 35).First().KetQua.ToString();
                     }
                    if (qhh.Where(p => p.STT == 36).Count() > 0 && qhh.Where(p => p.STT == 36).First().MaDVct != null)
                    {
                        a36.Text = "X";
                    }
                    if (qhh.Where(p => p.STT == 36).Count() > 0 && qhh.Where(p => p.STT == 36).First().KetQua != null)
                    {
                        col36.Text = qhh.Where(p => p.STT == 36).First().KetQua.ToString();
                     }
                    if (qhh.Where(p => p.STT == 37).Count() > 0 && qhh.Where(p => p.STT == 37).First().MaDVct != null)
                    {
                        a37.Text = "X";
                    }
                    if (qhh.Where(p => p.STT == 37).Count() > 0 && qhh.Where(p => p.STT == 37).First().KetQua != null)
                    {
                        col37.Text = qhh.Where(p => p.STT == 37).First().KetQua.ToString();
                     }
                    if (qhh.Where(p => p.STT == 38).Count() > 0 && qhh.Where(p => p.STT == 38).First().MaDVct != null)
                    {
                        a38.Text = "X";
                    }
                    if (qhh.Where(p => p.STT == 38).Count() > 0 && qhh.Where(p => p.STT == 38).First().KetQua != null)
                    {
                        col38.Text = qhh.Where(p => p.STT == 38).First().KetQua.ToString();
                    }

                #endregion
                }
              
            }
           
        }

        private void lab113_BeforePrint(object sender, CancelEventArgs e)
        {
        }

       

    }
}
