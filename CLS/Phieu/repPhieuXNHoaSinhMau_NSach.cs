using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class repPhieuXNHoaSinhMau_NSach : DevExpress.XtraReports.UI.XtraReport
    {
        public repPhieuXNHoaSinhMau_NSach()
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
            if (DungChung.Bien.MaBV == "30004")
                xrTableCell4.Visible = false;
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
             var qcls = (from dvct in DataContect.DichVucts 
                       join dv in DataContect.DichVus on dvct.MaDV equals dv.MaDV
                       join tnhomdv in DataContect.TieuNhomDVs on dv.IdTieuNhom equals tnhomdv.IdTieuNhom
                         where (tnhomdv.TenRG.Contains("Hóa sinh máu"))
                       select new {tnhomdv.TenTN,dvct.MaDVct,dvct.TenDVct,dvct.TSBT,dvct.TSBTnu, dvct.STT }).ToList();
            if (qcls.Count > 0)
            {
                if (qcls.Where(p => p.STT == 1).Count()>0 && qcls.Where(p => p.STT == 1).First().TenDVct != null)
                {
                    b1.Text = qcls.Where(p => p.STT == 1).First().TenDVct.ToString();
                }
                if (qcls.Where(p => p.STT == 1).Count() > 0 && qcls.Where(p => p.STT == 1).First().TSBT != null)
                {
                    c1.Text = qcls.Where(p => p.STT == 1).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 1).Count() > 0 && qcls.Where(p => p.STT == 1).First().TSBTnu != null)
                {
                    c12.Text = qcls.Where(p => p.STT == 1).First().TSBTnu.ToString();
                }
                if (qcls.Where(p => p.STT == 2).Count() > 0 && qcls.Where(p => p.STT == 2).First().TenDVct != null)
                {
                    b2.Text = qcls.Where(p => p.STT == 2).First().TenDVct.ToString();
                 }
                if (qcls.Where(p => p.STT == 2).Count() > 0 && qcls.Where(p => p.STT == 2).First().TSBT != null)
                {
                    c2.Text = qcls.Where(p => p.STT == 2).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 2).Count() > 0 && qcls.Where(p => p.STT == 2).First().TSBTnu != null)
                {
                    c22.Text = qcls.Where(p => p.STT == 2).First().TSBTnu.ToString();
                }
                if (qcls.Where(p => p.STT == 3).Count() > 0 && qcls.Where(p => p.STT == 3).First().TenDVct != null)
                {
                    b3.Text = qcls.Where(p => p.STT == 3).First().TenDVct.ToString();
                }
                if (qcls.Where(p => p.STT == 3).Count() > 0 && qcls.Where(p => p.STT == 3).First().TSBT != null)
                {
                     c3.Text = qcls.Where(p => p.STT == 3).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 3).Count() > 0 && qcls.Where(p => p.STT == 3).First().TSBTnu != null)
                {
                    c32.Text = qcls.Where(p => p.STT == 3).First().TSBTnu.ToString();
                }
                if (qcls.Where(p => p.STT == 4).Count() > 0 && qcls.Where(p => p.STT == 4).First().TenDVct != null)
                {
                    b4.Text = qcls.Where(p => p.STT == 4).First().TenDVct.ToString();
                 }
                if (qcls.Where(p => p.STT == 4).Count() > 0 && qcls.Where(p => p.STT == 4).First().TSBT != null)
                {
                     c4.Text = qcls.Where(p => p.STT == 4).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 4).Count() > 0 && qcls.Where(p => p.STT == 4).First().TSBTnu != null)
                {
                    c42.Text = qcls.Where(p => p.STT == 4).First().TSBTnu.ToString();
                }
                if (qcls.Where(p => p.STT == 5).Count() > 0 && qcls.Where(p => p.STT == 5).First().TenDVct != null)
                {
                    b5.Text = qcls.Where(p => p.STT == 5).First().TenDVct.ToString();
                 }
                if (qcls.Where(p => p.STT == 5).Count() > 0 && qcls.Where(p => p.STT == 5).First().TSBT != null)
                {
                     c5.Text = qcls.Where(p => p.STT == 5).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 5).Count() > 0 && qcls.Where(p => p.STT == 5).First().TSBTnu != null)
                {
                    c52.Text = qcls.Where(p => p.STT == 5).First().TSBTnu.ToString();
                }
                if (qcls.Where(p => p.STT == 6).Count() > 0 && qcls.Where(p => p.STT == 6).First().TenDVct != null)
                {
                    b6.Text = qcls.Where(p => p.STT == 6).First().TenDVct.ToString();
                }
                if (qcls.Where(p => p.STT == 6).Count() > 0 && qcls.Where(p => p.STT == 6).First().TSBT != null)
                {
                     c6.Text = qcls.Where(p => p.STT == 6).First().TSBT.ToString();
                }
               
                if (qcls.Where(p => p.STT == 7).Count() > 0 && qcls.Where(p => p.STT == 7).First().TenDVct != null)
                {
                    b7.Text = qcls.Where(p => p.STT == 7).First().TenDVct.ToString();
                  }
                if (qcls.Where(p => p.STT == 7).Count() > 0 && qcls.Where(p => p.STT == 7).First().TSBT != null)
                {
                    c7.Text = qcls.Where(p => p.STT == 7).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 8).Count() > 0 && qcls.Where(p => p.STT == 8).First().TenDVct != null)
                {
                    b8.Text = qcls.Where(p => p.STT == 8).First().TenDVct.ToString();
                }
                if (qcls.Where(p => p.STT == 8).Count() > 0 && qcls.Where(p => p.STT == 8).First().TSBT != null)
                {
                    c8.Text = qcls.Where(p => p.STT == 8).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 8).Count() > 0 && qcls.Where(p => p.STT == 8).First().TSBTnu != null)
                {
                    c82.Text = qcls.Where(p => p.STT == 8).First().TSBTnu.ToString();
                }
                if (qcls.Where(p => p.STT == 9).Count() > 0 && qcls.Where(p => p.STT == 9).First().TenDVct != null)
                {
                    b9.Text = qcls.Where(p => p.STT == 9).First().TenDVct.ToString();
                }
                if (qcls.Where(p => p.STT == 9).Count() > 0 && qcls.Where(p => p.STT == 9).First().TSBT != null)
                {
                      c9.Text = qcls.Where(p => p.STT == 9).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 9).Count() > 0 && qcls.Where(p => p.STT == 9).First().TSBTnu != null)
                {
                    c92.Text = qcls.Where(p => p.STT == 9).First().TSBTnu.ToString();
                }
                if (qcls.Where(p => p.STT == 10).Count() > 0 && qcls.Where(p => p.STT == 10).First().TenDVct != null)
                {
                    b10.Text = qcls.Where(p => p.STT == 10).First().TenDVct.ToString();
                }
                if (qcls.Where(p => p.STT == 10).Count() > 0 && qcls.Where(p => p.STT == 10).First().TSBT != null)
                {
                     c10.Text = qcls.Where(p => p.STT == 10).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 11).Count() > 0 && qcls.Where(p => p.STT == 11).First().TenDVct != null)
                {
                    b11.Text = qcls.Where(p => p.STT == 11).First().TenDVct.ToString();
                }
                if (qcls.Where(p => p.STT == 11).Count() > 0 && qcls.Where(p => p.STT == 11).First().TSBT != null)
                {
                    c11.Text = qcls.Where(p => p.STT == 11).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 12).Count() > 0 && qcls.Where(p => p.STT == 12).First().TenDVct != null)
                {
                    b12.Text = qcls.Where(p => p.STT == 12).First().TenDVct.ToString();
                 }
                if (qcls.Where(p => p.STT == 12).Count() > 0 && qcls.Where(p => p.STT == 12).First().TSBT != null)
                {
                     c12.Text = qcls.Where(p => p.STT == 12).First().TSBT.ToString();
                }
             
                if (qcls.Where(p => p.STT == 13).Count() > 0 && qcls.Where(p => p.STT == 13).First().TenDVct != null)
                {
                    b13.Text = qcls.Where(p => p.STT == 13).First().TenDVct.ToString();
                 }
                if (qcls.Where(p => p.STT == 13).Count() > 0 && qcls.Where(p => p.STT == 13).First().TSBT != null)
                {
                    c13.Text = qcls.Where(p => p.STT == 13).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 14).Count() > 0 && qcls.Where(p => p.STT == 14).First().TenDVct != null)
                {
                    b14.Text = qcls.Where(p => p.STT == 14).First().TenDVct.ToString();
                }
                if (qcls.Where(p => p.STT == 14).Count() > 0 && qcls.Where(p => p.STT == 14).First().TSBT != null)
                {
                     c14.Text = qcls.Where(p => p.STT == 14).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 15).Count() > 0 && qcls.Where(p => p.STT == 15).First().TenDVct != null)
                {
                    b15.Text = qcls.Where(p => p.STT == 15).First().TenDVct.ToString();
                }
                if (qcls.Where(p => p.STT == 15).Count() > 0 && qcls.Where(p => p.STT == 15).First().TSBT != null)
                {
                    c15.Text = qcls.Where(p => p.STT == 15).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 16).Count() > 0 && qcls.Where(p => p.STT == 16).First().TenDVct != null)
                {
                    b16.Text = qcls.Where(p => p.STT == 16).First().TenDVct.ToString();
                 }
                if (qcls.Where(p => p.STT == 16).Count() > 0 && qcls.Where(p => p.STT == 16).First().TSBT != null)
                {
                     c16.Text = qcls.Where(p => p.STT == 16).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 17).Count() > 0 && qcls.Where(p => p.STT == 17).First().TenDVct != null)
                {
                    b17.Text = qcls.Where(p => p.STT == 17).First().TenDVct.ToString();
                 }
                if (qcls.Where(p => p.STT == 17).Count() > 0 && qcls.Where(p => p.STT == 17).First().TSBT != null)
                {
                    c17.Text = qcls.Where(p => p.STT == 17).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 18).Count() > 0 && qcls.Where(p => p.STT == 18).First().TenDVct != null)
                {
                    b18.Text = qcls.Where(p => p.STT == 18).First().TenDVct.ToString();
                }
                if (qcls.Where(p => p.STT == 18).Count() > 0 && qcls.Where(p => p.STT == 18).First().TSBT != null)
                {
                     c18.Text = qcls.Where(p => p.STT == 18).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 19).Count() > 0 && qcls.Where(p => p.STT == 19).First().TenDVct != null)
                {
                    b19.Text = qcls.Where(p => p.STT == 19).First().TenDVct.ToString();
                    }
                if (qcls.Where(p => p.STT == 19).Count() > 0 && qcls.Where(p => p.STT == 19).First().TSBT != null)
                {
                    c19.Text = qcls.Where(p => p.STT == 19).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 20).Count() > 0 && qcls.Where(p => p.STT == 20).First().TenDVct != null)
                {
                    b20.Text = qcls.Where(p => p.STT == 20).First().TenDVct.ToString();
                 }
                if (qcls.Where(p => p.STT == 20).Count() > 0 && qcls.Where(p => p.STT == 20).First().TSBT != null)
                {
                    c20.Text = qcls.Where(p => p.STT == 20).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 21).Count() > 0 && qcls.Where(p => p.STT == 21).First().TenDVct != null)
                {
                    b21.Text = qcls.Where(p => p.STT == 21).First().TenDVct.ToString();
                }
               
                if (qcls.Where(p => p.STT == 21).Count() > 0 && qcls.Where(p => p.STT == 21).First().TSBT != null)
                {
                    c21.Text = qcls.Where(p => p.STT == 21).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 22).Count() > 0 && qcls.Where(p => p.STT == 22).First().TenDVct != null)
                {
                    b22.Text = qcls.Where(p => p.STT == 22).First().TenDVct.ToString();
                }
                if (qcls.Where(p => p.STT == 22).Count() > 0 && qcls.Where(p => p.STT == 22).First().TSBT != null)
                {
                    c22a.Text = qcls.Where(p => p.STT == 22).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 23).Count() > 0 && qcls.Where(p => p.STT == 23).First().TenDVct != null)
                {
                    b23.Text = qcls.Where(p => p.STT == 23).First().TenDVct.ToString();
                }
                if (qcls.Where(p => p.STT == 23).Count() > 0 && qcls.Where(p => p.STT == 23).First().TSBT != null)
                {
                     c23.Text = qcls.Where(p => p.STT == 23).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 24).Count() > 0 && qcls.Where(p => p.STT == 24).First().TenDVct != null)
                {
                    b24.Text = qcls.Where(p => p.STT == 24).First().TenDVct.ToString();
                }
                if (qcls.Where(p => p.STT == 24).Count() > 0 && qcls.Where(p => p.STT == 24).First().TSBT != null)
                {
                     c24.Text = qcls.Where(p => p.STT == 24).First().TSBT.ToString();
                }
               
                ////////if (qcls.Where(p => p.STT == 43).Count() > 0 && qcls.Where(p => p.STT == 43).First().TSBT != null)
                ////////{
                ////////    lab109.Text = qcls.Where(p => p.STT == 43).First().TSBT.ToString();
                ////////    lab109.Font =new Font("Tahoma",12F,System.Drawing.FontStyle.Bold);
                ////////}
          

                int sophieu = int.Parse(SoPhieu.Value.ToString());

                var qhh = ( from cls in DataContect.CLS.Where(p=>p.IdCLS==sophieu)
                            join chidinh in DataContect.ChiDinhs on cls.IdCLS equals chidinh.IdCLS
                            join dichvu in DataContect.DichVus on chidinh.MaDV equals dichvu.MaDV
                            join tn in DataContect.TieuNhomDVs.Where(p => p.TenTN.Contains("hoá sinh")) on dichvu.IdTieuNhom equals tn.IdTieuNhom
                            join clsct in DataContect.CLScts on chidinh.IDCD equals clsct.IDCD
                           join dvct in DataContect.DichVucts on clsct.MaDVct  equals dvct.MaDVct
                           select new { clsct.MaDVct, clsct.KetQua, dvct.STT }).ToList();
                if (qhh.Count > 0)
                {
                    if (qhh.Where(p => p.STT == 1).Count() > 0 && qhh.Where(p => p.STT == 1).First().MaDVct != null)
                    {
                        a1.Text = "X";
                    }
                    if (qhh.Where(p => p.STT == 1).Count() > 0 && qhh.Where(p => p.STT == 1).First().KetQua != null)
                    {
                        d1.Text = qhh.Where(p => p.STT == 1).First().KetQua.ToString();
                    }
                    if (qhh.Where(p => p.STT == 2).Count() > 0 && qhh.Where(p => p.STT == 2).First().MaDVct != null)
                    {
                        a2.Text = "X";
                    }
                    if (qhh.Where(p => p.STT == 2).Count() > 0 && qhh.Where(p => p.STT == 2).First().KetQua != null)
                    {
                        d2.Text = qhh.Where(p => p.STT == 2).First().KetQua.ToString();
                    }
                    if (qhh.Where(p => p.STT == 3).Count() > 0 && qhh.Where(p => p.STT == 3).First().MaDVct != null)
                    {
                        a3.Text = "X";
                    }
                    if (qhh.Where(p => p.STT == 3).Count() > 0 && qhh.Where(p => p.STT == 3).First().KetQua != null)
                    {
                        d3.Text = qhh.Where(p => p.STT == 3).First().KetQua.ToString();
                    }
                    if (qhh.Where(p => p.STT == 4).Count() > 0 && qhh.Where(p => p.STT == 4).First().MaDVct != null)
                    {
                        a4.Text = "X";
                    }
                    if (qhh.Where(p => p.STT == 4).Count() > 0 && qhh.Where(p => p.STT == 4).First().KetQua != null)
                    {
                        d4.Text = qhh.Where(p => p.STT == 4).First().KetQua.ToString();
                    }
                    if (qhh.Where(p => p.STT == 5).Count() > 0 && qhh.Where(p => p.STT == 5).First().MaDVct != null)
                    {
                        a5.Text = "X";
                    }
                    if (qhh.Where(p => p.STT == 5).Count() > 0 && qhh.Where(p => p.STT == 5).First().KetQua != null)
                    {
                        d5.Text = qhh.Where(p => p.STT == 5).First().KetQua.ToString();
                    }
                    if (qhh.Where(p => p.STT == 6).Count() > 0 && qhh.Where(p => p.STT == 6).First().MaDVct != null)
                    {
                        a6.Text = "X";
                    }
                    if (qhh.Where(p => p.STT == 6).Count() > 0 && qhh.Where(p => p.STT == 6).First().KetQua != null)
                    {
                        d6.Text = qhh.Where(p => p.STT == 6).First().KetQua.ToString();
                    }
                    if (qhh.Where(p => p.STT == 7).Count() > 0 && qhh.Where(p => p.STT == 7).First().MaDVct != null)
                    {
                        a7.Text = "X";
                    }
                    if (qhh.Where(p => p.STT == 7).Count() > 0 && qhh.Where(p => p.STT == 7).First().KetQua != null)
                    {
                        d7.Text = qhh.Where(p => p.STT == 7).First().KetQua.ToString();
                    }
                    if (qhh.Where(p => p.STT == 8).Count() > 0 && qhh.Where(p => p.STT == 8).First().MaDVct != null)
                    {
                        a8.Text = "X";
                    }
                    if (qhh.Where(p => p.STT == 8).Count() > 0 && qhh.Where(p => p.STT == 8).First().KetQua != null)
                    {
                        d8.Text = qhh.Where(p => p.STT == 8).First().KetQua.ToString();
                    }
                    if (qhh.Where(p => p.STT == 9).Count() > 0 && qhh.Where(p => p.STT == 9).First().MaDVct != null)
                    {
                        a9.Text = "X";
                    }
                    if (qhh.Where(p => p.STT == 9).Count() > 0 && qhh.Where(p => p.STT == 9).First().KetQua != null)
                    {
                        d9.Text = qhh.Where(p => p.STT == 9).First().KetQua.ToString();
                    }
                    if (qhh.Where(p => p.STT == 10).Count() > 0 && qhh.Where(p => p.STT == 10).First().MaDVct != null)
                    {
                        a10.Text = "X";
                    }
                    if (qhh.Where(p => p.STT == 10).Count() > 0 && qhh.Where(p => p.STT == 10).First().KetQua != null)
                    {
                        d10.Text = qhh.Where(p => p.STT == 10).First().KetQua.ToString();
                    }
                    if (qhh.Where(p => p.STT == 11).Count() > 0 && qhh.Where(p => p.STT == 11).First().MaDVct != null)
                    {
                        a11.Text = "X";
                    }
                    if (qhh.Where(p => p.STT == 11).Count() > 0 && qhh.Where(p => p.STT == 11).First().KetQua != null)
                    {
                        d11.Text = qhh.Where(p => p.STT == 11).First().KetQua.ToString();
                    }
                    if (qhh.Where(p => p.STT == 12).Count() > 0 && qhh.Where(p => p.STT == 12).First().MaDVct != null)
                    {
                        a12.Text = "X";
                    }
                    if (qhh.Where(p => p.STT == 12).Count() > 0 && qhh.Where(p => p.STT == 12).First().KetQua != null)
                    {
                        d12.Text = qhh.Where(p => p.STT == 12).First().KetQua.ToString();
                    }
                    if (qhh.Where(p => p.STT == 13).Count() > 0 && qhh.Where(p => p.STT == 13).First().MaDVct != null)
                    {
                        a23.Text = "X";
                    }
                    if (qhh.Where(p => p.STT == 13).Count() > 0 && qhh.Where(p => p.STT == 13).First().KetQua != null)
                    {
                        d13.Text = qhh.Where(p => p.STT == 13).First().KetQua.ToString();
                    }
                    if (qhh.Where(p => p.STT == 14).Count() > 0 && qhh.Where(p => p.STT == 14).First().MaDVct != null)
                    {
                        a24.Text = "X";
                    }
                    if (qhh.Where(p => p.STT == 14).Count() > 0 && qhh.Where(p => p.STT == 14).First().KetQua != null)
                    {
                        d14.Text = qhh.Where(p => p.STT == 14).First().KetQua.ToString();
                    }
                    if (qhh.Where(p => p.STT == 15).Count() > 0 && qhh.Where(p => p.STT == 15).First().MaDVct != null)
                    {
                        a25.Text = "X";
                    }
                    if (qhh.Where(p => p.STT == 15).Count() > 0 && qhh.Where(p => p.STT == 15).First().KetQua != null)
                    {
                        d15.Text = qhh.Where(p => p.STT == 15).First().KetQua.ToString();
                    }
                    if (qhh.Where(p => p.STT == 16).Count() > 0 && qhh.Where(p => p.STT == 16).First().MaDVct != null)
                    {
                        a26.Text = "X";
                    }
                    if (qhh.Where(p => p.STT == 16).Count() > 0 && qhh.Where(p => p.STT == 16).First().KetQua != null)
                    {
                      d16.Text = qhh.Where(p => p.STT == 16).First().KetQua.ToString();
                    }
                    if (qhh.Where(p => p.STT == 17).Count() > 0 && qhh.Where(p => p.STT == 17).First().MaDVct != null)
                    {
                        a27.Text = "X";
                    }
                    if (qhh.Where(p => p.STT == 17).Count() > 0 && qhh.Where(p => p.STT == 17).First().KetQua != null)
                    {
                       d17.Text = qhh.Where(p => p.STT == 17).First().KetQua.ToString();
                    }
                    if (qhh.Where(p => p.STT == 18).Count() > 0 && qhh.Where(p => p.STT == 18).First().MaDVct != null)
                    {
                        a28.Text = "X";
                    }
                    if (qhh.Where(p => p.STT == 18).Count() > 0 && qhh.Where(p => p.STT == 18).First().KetQua != null)
                    {
                        d18.Text = qhh.Where(p => p.STT == 18).First().KetQua.ToString();
                    }
                    if (qhh.Where(p => p.STT == 19).Count() > 0 && qhh.Where(p => p.STT == 19).First().MaDVct != null)
                    {
                        a29.Text = "X";
                    }
                    if (qhh.Where(p => p.STT == 19).Count() > 0 && qhh.Where(p => p.STT == 19).First().KetQua != null)
                    {
                        d19.Text = qhh.Where(p => p.STT == 19).First().KetQua.ToString();
                    }
                    if (qhh.Where(p => p.STT == 20).Count() > 0 && qhh.Where(p => p.STT == 20).First().MaDVct != null)
                    {
                        a30.Text = "X";
                    }
                    if (qhh.Where(p => p.STT == 20).Count() > 0 && qhh.Where(p => p.STT == 20).First().KetQua != null)
                    {
                        d20.Text = qhh.Where(p => p.STT == 20).First().KetQua.ToString();
                    }
                    if (qhh.Where(p => p.STT == 21).Count() > 0 && qhh.Where(p => p.STT == 21).First().MaDVct != null)
                    {
                        a31.Text = "X";
                    }
                    if (qhh.Where(p => p.STT == 21).Count() > 0 && qhh.Where(p => p.STT == 21).First().KetQua != null)
                    {
                        d21.Text = qhh.Where(p => p.STT == 21).First().KetQua.ToString();
                    }
                    if (qhh.Where(p => p.STT == 22).Count() > 0 && qhh.Where(p => p.STT == 22).First().MaDVct != null)
                    {
                        a32.Text = "X";
                    }
                    if (qhh.Where(p => p.STT == 22).Count() > 0 && qhh.Where(p => p.STT == 22).First().KetQua != null)
                    {
                        d22.Text = qhh.Where(p => p.STT == 22).First().KetQua.ToString();
                    }
                    if (qhh.Where(p => p.STT == 23).Count() > 0 && qhh.Where(p => p.STT == 23).First().MaDVct != null)
                    {
                        a33.Text = "X";
                    }
                    if (qhh.Where(p => p.STT == 23).Count() > 0 && qhh.Where(p => p.STT == 23).First().KetQua != null)
                    {
                        d23.Text = qhh.Where(p => p.STT == 23).First().KetQua.ToString();
                    }
                    if (qhh.Where(p => p.STT == 24).Count() > 0 && qhh.Where(p => p.STT == 24).First().MaDVct != null)
                    {
                        a34.Text = "X";
                    }
                    if (qhh.Where(p => p.STT == 24).Count() > 0 && qhh.Where(p => p.STT == 24).First().KetQua != null)
                    {
                        d24.Text = qhh.Where(p => p.STT == 24).First().KetQua.ToString();
                    }
               
                }
            
                 
            }
           
        }

        private void xrTable1_BeforePrint(object sender, CancelEventArgs e)
        {

        }

        private void xrTableCell25_BeforePrint(object sender, CancelEventArgs e)
        {

        }

        private void xrTableCell71_BeforePrint(object sender, CancelEventArgs e)
        {

        }

        private void xrTableCell1_BeforePrint(object sender, CancelEventArgs e)
        {

        }

    }
}
