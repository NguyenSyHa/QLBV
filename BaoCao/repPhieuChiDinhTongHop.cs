using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class repPhieuChiDinhTongHop : DevExpress.XtraReports.UI.XtraReport
    {
        public repPhieuChiDinhTongHop()
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
                //colTenBSDT.Text = DungChung.Ham._getTenCB(DataContect, MaCBDT.Value.ToString());
            }
          
            if (Macb.Value!=null)
            {
                //colTenTKXN.Text = DungChung.Ham._getTenCB(DataContect, Macb.Value.ToString());
            }
          
        }
/*
        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
             var qcls = (from dvct in DataContect.DichVucts 
                       join dv in DataContect.DichVus on dvct.MaDV equals dv.MaDV
                          join tnhomdv in DataContect.TieuNhomDVs.Where(p => p.TenRG == "XN hóa sinh miễn dịch") on dv.IdTieuNhom equals tnhomdv.IdTieuNhom                       
                       select new {tnhomdv.TenTN,dvct.MaDVct,dvct.TenDVct,dvct.TSBT, dvct.STT, dv.MaQD, tnhomdv.TenRG }).ToList();
            
            if (qcls.Count > 0)
            {
                #region xn hóa sinh miễn dịch
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
               
                if (qhh.Count > 0 )
                {
                    #region xét nghiệm hóa sinh miễn dịch
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

                #endregion
                }
             


            }
           
        }
*/
        public void BindingData()
        {
            celThanhTien.DataBindings.Add("Text", DataSource, "ThanhTien");
            celTenDV.DataBindings.Add("Text", DataSource, "TenDV");
            celDongia.DataBindings.Add("Text", DataSource, "DonGia");
            celSoluong.DataBindings.Add("Text", DataSource, "SoLuong");
            //celTongTien.DataBindings.Add("Text", DataSource, "");
        }
        private void ReportFooter_BeforePrint_1(object sender, CancelEventArgs e)
        {
            celNgayThang.Text = "Bắc ninh, ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;
        }
    }
}
