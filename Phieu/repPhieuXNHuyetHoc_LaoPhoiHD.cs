using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class repPhieuXNHuyetHoc_LaoPhoiHD : DevExpress.XtraReports.UI.XtraReport
    {
        public repPhieuXNHuyetHoc_LaoPhoiHD()
        {
            InitializeComponent();
           
        }
        QLBV_Database.QLBVEntities DataContect = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "30002") {
                lab16.Text = "1. Tổng phân tích tế bào máu ngoại vi:";
                lab16.WidthF = 300f;
                labT0.LocationF = new DevExpress.Utils.PointFloat(300.00F, 0F);
            }
            var qcls = (from dvct in DataContect.DichVucts 
                       join dv in DataContect.DichVus on dvct.MaDV equals dv.MaDV
                       join tnhomdv in DataContect.TieuNhomDVs on dv.IdTieuNhom equals tnhomdv.IdTieuNhom
                         where (tnhomdv.TenRG== DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc)
                       select new {dvct.MaDVct,dvct.TenDVct,dvct.TSBT, dvct.STT }).ToList();
            if (qcls.Count > 0)
            {
                 
                    if (qcls.Where(p => p.STT == 1).Count() > 0 && qcls.Where(p => p.STT == 1).First().TenDVct != null && qcls.Where(p => p.STT == 1).First().TSBT != null)
                {
                    string a =qcls.Where(p => p.STT == 1).First().TenDVct.ToString() + qcls.Where(p => p.STT == 1).First().TSBT.ToString();
                    a=a.Replace("#","\n");
                    col1.Text = a;
                }
               
                    if (qcls.Where(p => p.STT == 2).Count() > 0 && qcls.Where(p => p.STT == 2).First().TenDVct != null && qcls.Where(p => p.STT == 2).First().TSBT != null)
                {
                    string a = qcls.Where(p => p.STT == 2).First().TenDVct.ToString() + qcls.Where(p => p.STT == 2).First().TSBT.ToString();
                    a = a.Replace("#", "\n");
                    col2.Text = a;
                }
               
                    if (qcls.Where(p => p.STT == 3).Count() > 0 && qcls.Where(p => p.STT == 3).First().TenDVct != null && qcls.Where(p => p.STT == 3).First().TSBT != null)
                {
                    string a = qcls.Where(p => p.STT == 3).First().TenDVct.ToString() + qcls.Where(p => p.STT == 3).First().TSBT.ToString();
                    a = a.Replace("#", "\n");
                    col3.Text = a;
                }
               
                    if (qcls.Where(p => p.STT == 4).Count() > 0 && qcls.Where(p => p.STT == 4).First().TenDVct != null && qcls.Where(p => p.STT == 4).First().TSBT != null)
                {
                    string a =  qcls.Where(p => p.STT == 4).First().TenDVct.ToString() + qcls.Where(p => p.STT == 4).First().TSBT.ToString();
                    a = a.Replace("#", "\n");
                    col4.Text = a;
                }
               
                    if (qcls.Where(p => p.STT ==5).Count() > 0 && qcls.Where(p => p.STT == 5).First().TenDVct != null && qcls.Where(p => p.STT == 5).First().TSBT != null)
                {
                    string a =  qcls.Where(p => p.STT ==5).First().TenDVct.ToString() + qcls.Where(p => p.STT == 5).First().TSBT.ToString();
                    a = a.Replace("#", "\n");
                    col5.Text = a;
                }
               
                    if (qcls.Where(p => p.STT == 6).Count() > 0 && qcls.Where(p => p.STT == 6).First().TenDVct != null && qcls.Where(p => p.STT == 6).First().TSBT != null)
                {
                    string a =qcls.Where(p => p.STT == 6).First().TenDVct.ToString() + qcls.Where(p => p.STT == 6).First().TSBT.ToString();
                    a = a.Replace("#", "\n");
                    col6.Text = a;
                }
               
                    if (qcls.Where(p => p.STT == 7).Count() > 0 && qcls.Where(p => p.STT == 7).First().TenDVct != null && qcls.Where(p => p.STT ==7).First().TSBT != null)
                {
                    string a = qcls.Where(p => p.STT == 7).First().TenDVct.ToString() + qcls.Where(p => p.STT == 7).First().TSBT.ToString();
                    a = a.Replace("#", "\n");
                    col7.Text = a;
                }
               
                    if (qcls.Where(p => p.STT == 8).Count() > 0 && qcls.Where(p => p.STT == 8).First().TenDVct != null && qcls.Where(p => p.STT == 8).First().TSBT != null)
                {
                    string a = qcls.Where(p => p.STT == 8).First().TenDVct.ToString() + qcls.Where(p => p.STT == 8).First().TSBT.ToString();
                    a = a.Replace("#", "\n");
                    col8.Text = a;
                }
               
                    if (qcls.Where(p => p.STT == 9).Count() > 0 && qcls.Where(p => p.STT == 9).First().TenDVct != null && qcls.Where(p => p.STT == 9).First().TSBT != null)
                {
                    string a = qcls.Where(p => p.STT == 9).First().TenDVct.ToString() + qcls.Where(p => p.STT == 9).First().TSBT.ToString();
                    a = a.Replace("#", "\n");
                    col9.Text = a;
                }
               
                    if (qcls.Where(p => p.STT == 10).Count() > 0 && qcls.Where(p => p.STT == 10).First().TenDVct != null && qcls.Where(p => p.STT == 10).First().TSBT != null)
                {
                    string a = qcls.Where(p => p.STT == 10).First().TenDVct.ToString() + qcls.Where(p => p.STT == 10).First().TSBT.ToString();
                    a = a.Replace("#", "\n");
                    col10.Text = a;
                }
               
                    if (qcls.Where(p => p.STT == 11).Count() > 0 && qcls.Where(p => p.STT == 11).First().TenDVct != null && qcls.Where(p => p.STT == 11).First().TSBT != null)
                {
                    string a =  qcls.Where(p => p.STT == 11).First().TenDVct.ToString() + qcls.Where(p => p.STT == 11).First().TSBT.ToString();
                    a = a.Replace("#", "\n");
                    col11.Text = a;
                }
               
                    if (qcls.Where(p => p.STT == 12).Count() > 0 && qcls.Where(p => p.STT == 12).First().TenDVct != null && qcls.Where(p => p.STT == 12).First().TSBT != null)
                {
                    string a = qcls.Where(p => p.STT == 12).First().TenDVct.ToString() + qcls.Where(p => p.STT == 12).First().TSBT.ToString();
                    a = a.Replace("#", "\n");
                    col12.Text = a;
                }
               
                    
                    if (qcls.Where(p => p.STT == 13).Count() > 0 && qcls.Where(p => p.STT == 13).First().TenDVct != null && qcls.Where(p => p.STT == 13).First().TSBT != null)
                {
                    string a =qcls.Where(p => p.STT == 13).First().TenDVct.ToString() + qcls.Where(p => p.STT == 13).First().TSBT.ToString();
                    a = a.Replace("#", "\n");
                    col13.Text = a;
                }
              
                    if (qcls.Where(p => p.STT == 14).Count() > 0 && qcls.Where(p => p.STT == 14).First().TenDVct != null && qcls.Where(p => p.STT == 14).First().TSBT != null)
                {
                    string a = qcls.Where(p => p.STT == 14).First().TenDVct.ToString() + qcls.Where(p => p.STT == 14).First().TSBT.ToString();
                    a = a.Replace("#", "\n");
                    col14.Text = a;
                }
               
                    if (qcls.Where(p => p.STT == 15).Count() > 0 && qcls.Where(p => p.STT == 15).First().TenDVct != null && qcls.Where(p => p.STT == 15).First().TSBT != null)
                {
                    string a = qcls.Where(p => p.STT == 15).First().TenDVct.ToString() + qcls.Where(p => p.STT == 15).First().TSBT.ToString();
                    a = a.Replace("#", "\n");
                    col15.Text = a;
                }
               
                    if (qcls.Where(p => p.STT == 16).Count() > 0 && qcls.Where(p => p.STT == 16).First().TenDVct != null && qcls.Where(p => p.STT == 16).First().TSBT != null)
                {
                    string a = qcls.Where(p => p.STT == 16).First().TenDVct.ToString() + qcls.Where(p => p.STT == 16).First().TSBT.ToString();
                    a = a.Replace("#", "\n");
                    col16.Text = a;
                }
              
                    if (qcls.Where(p => p.STT == 17).Count() > 0 && qcls.Where(p => p.STT == 17).First().TenDVct != null && qcls.Where(p => p.STT == 17).First().TSBT != null)
                {
                    string a = qcls.Where(p => p.STT == 17).First().TenDVct.ToString() + qcls.Where(p => p.STT == 17).First().TSBT.ToString();
                    a = a.Replace("#", "\n");
                    col17.Text = a;
                }
               
                    if (qcls.Where(p => p.STT == 18).Count() > 0 && qcls.Where(p => p.STT == 18).First().TenDVct != null && qcls.Where(p => p.STT == 18).First().TSBT != null)
                {
                    string a = qcls.Where(p => p.STT == 18).First().TenDVct.ToString() + qcls.Where(p => p.STT == 18).First().TSBT.ToString();
                    a = a.Replace("#", "\n");
                    col18.Text = a;
                }
                
                    if (qcls.Where(p => p.STT == 19).Count() > 0 && qcls.Where(p => p.STT == 19).First().TenDVct != null && qcls.Where(p => p.STT == 19).First().TSBT != null)
                {
                    string a = qcls.Where(p => p.STT == 19).First().TenDVct.ToString() + qcls.Where(p => p.STT == 19).First().TSBT.ToString();
                    a = a.Replace("#", "\n");
                    col19.Text = a;
                }
                
                    if (qcls.Where(p => p.STT == 20).Count() > 0 && qcls.Where(p => p.STT == 20).First().TenDVct != null && qcls.Where(p => p.STT == 20).First().TSBT != null)
                {
                    string a = qcls.Where(p => p.STT == 20).First().TenDVct.ToString() + qcls.Where(p => p.STT == 20).First().TSBT.ToString();
                    a = a.Replace("#", "\n");
                    col20.Text = a;
                }
             
                    if (qcls.Where(p => p.STT == 21).Count() > 0 && qcls.Where(p => p.STT == 21).First().TenDVct != null && qcls.Where(p => p.STT == 21).First().TSBT != null)
                {
                    string a = qcls.Where(p => p.STT == 21).First().TenDVct.ToString() + qcls.Where(p => p.STT == 21).First().TSBT.ToString();
                    a = a.Replace("#", "\n");
                    col21.Text = a;
                }
             
                    if (qcls.Where(p => p.STT == 22).Count() > 0 && qcls.Where(p => p.STT == 22).First().TenDVct != null && qcls.Where(p => p.STT == 22).First().TSBT != null)
                {
                    string a = qcls.Where(p => p.STT == 22).First().TenDVct.ToString() + qcls.Where(p => p.STT == 22).First().TSBT.ToString();
                    a = a.Replace("#", "\n");
                    col22.Text = a;
                }
               
                    if (qcls.Where(p => p.STT == 23).Count() > 0 && qcls.Where(p => p.STT == 23).First().TenDVct != null && qcls.Where(p => p.STT == 23).First().TSBT != null)
                {
                    string a =  qcls.Where(p => p.STT == 23).First().TenDVct.ToString() + qcls.Where(p => p.STT == 23).First().TSBT.ToString();
                    a = a.Replace("#", "\n");
                    col23.Text = a;
                }
               
                    if (qcls.Where(p => p.STT == 24).Count() > 0 && qcls.Where(p => p.STT == 24).First().TenDVct != null && qcls.Where(p => p.STT == 24).First().TSBT != null)
                {
                    string a =qcls.Where(p => p.STT == 24).First().TenDVct.ToString() + qcls.Where(p => p.STT == 24).First().TSBT.ToString();
                    a = a.Replace("#", "\n");
                    col24.Text = a;
                }
               
                    if (qcls.Where(p => p.STT == 25).Count() > 0 && qcls.Where(p => p.STT == 25).First().TenDVct != null && qcls.Where(p => p.STT == 25).First().TSBT != null)
                {
                    string a = qcls.Where(p => p.STT == 25).First().TenDVct.ToString() + qcls.Where(p => p.STT == 25).First().TSBT.ToString();
                    a = a.Replace("#", "\n");
                    col25.Text = a;
                }
              
                    if (qcls.Where(p => p.STT == 26).Count() > 0 && qcls.Where(p => p.STT == 26).First().TenDVct != null && qcls.Where(p => p.STT == 26).First().TSBT != null)
                {
                    string a = qcls.Where(p => p.STT == 26).First().TenDVct.ToString() + qcls.Where(p => p.STT == 26).First().TSBT.ToString();
                    a = a.Replace("#", "\n");
                    col26.Text = a;
                }
              
                    if (qcls.Where(p => p.STT == 27).Count() > 0 && qcls.Where(p => p.STT == 27).First().TenDVct != null && qcls.Where(p => p.STT == 27).First().TSBT != null)
                {
                    string a =qcls.Where(p => p.STT == 27).First().TenDVct.ToString() + qcls.Where(p => p.STT == 27).First().TSBT.ToString();
                    a = a.Replace("#", "\n");
                    col27.Text = a;
                }

                    if (qcls.Where(p => p.STT == 30).Count() > 0 && qcls.Where(p => p.STT == 30).First().TenDVct != null && qcls.Where(p => p.STT == 30).First().TSBT != null)
                    {
                        string a = qcls.Where(p => p.STT == 30).First().TenDVct.ToString() + qcls.Where(p => p.STT == 30).First().TSBT.ToString();
                        a = a.Replace("#", "\n");
                        col30.Text = a;
                    }

                    if (qcls.Where(p => p.STT == 31).Count() > 0 && qcls.Where(p => p.STT == 31).First().TenDVct != null && qcls.Where(p => p.STT == 31).First().TSBT != null)
                    {
                        string a = qcls.Where(p => p.STT == 31).First().TenDVct.ToString() + qcls.Where(p => p.STT == 31).First().TSBT.ToString();
                        a = a.Replace("#", "\n");
                        col31.Text = a;
                    }
            }
            //string mabn = MaBNhan.Value.ToString();
            int sophieu = int.Parse(SoPhieu.Value.ToString());

            var qhh = (from cls in DataContect.CLS.Where(p=>p.IdCLS==sophieu)
                        join chidinh in DataContect.ChiDinhs on cls.IdCLS equals chidinh.IdCLS
                        join clsct in DataContect.CLScts on chidinh.IDCD equals clsct.IDCD
                        join dvct in DataContect.DichVucts on clsct.MaDVct equals dvct.MaDVct
                       join dichvu in DataContect.DichVus on dvct.MaDV equals dichvu.MaDV
                        select new {clsct.Status, clsct.MaDVct, clsct.KetQua, dvct.STT }).ToList();
            string _tebaoNV = "";
            for (int i = 1; i < 22; i++) {
                if (qhh.Where(p => p.STT == i).Count() > 0 && qhh.Where(p => p.STT == i).First().MaDVct != null)
                {
                    _tebaoNV = "X";
                    break;
                }
            }
                if (qhh.Count > 0)
                {
                    labT0.Text = _tebaoNV;
                    if (DungChung.Bien.MaBV == "24009" || DungChung.Bien.MaBV == "30005" || DungChung.Bien.MaBV == "30007" || DungChung.Bien.MaBV == "30280")
                
                    foreach (XRTableRow _tableRow in xrTable1)
                    {
                        
                        foreach (XRTableCell _tableCell in _tableRow)
                        {
                            if (_tableCell.Name == "xrTableCell32")
                            {
                                if (qhh.Where(p => p.STT == 14).Count() > 0 && qhh.Where(p => p.STT == 14).First().MaDVct != null && qhh.Where(p => p.STT == 14).First().Status!=-1)
                                {
                                    labT14.Text = "X";
                                }
                                if (qhh.Where(p => p.STT == 15).Count() > 0 && qhh.Where(p => p.STT == 15).First().MaDVct != null && qhh.Where(p => p.STT == 15).First().Status != -1)
                                {
                                    labT15.Text = "X";
                                }
                            }
                            else
                            for (int i = 1; i < 24; i++)
                            {
                                if (qhh.Where(p => p.STT == i).Count() > 0 && qhh.Where(p => p.STT == i).First().MaDVct != null && qhh.Where(p => p.STT == i).First().Status != -1)
                                {
                                    if (_tableCell.Name == "labT" + (i).ToString())
                                    {
                                        _tableCell.Text = "X";
                                        break;
                                    }
                                }
                            }
                    }
                             
                    }
                        if (qhh.Where(p => p.STT == 1).Count() > 0 && qhh.Where(p => p.STT == 1).First().MaDVct != null)
                        {
                            //labT1.Text = "X";
                        }
                    if (qhh.Where(p => p.STT == 1).Count() > 0 && qhh.Where(p => p.STT == 1).First().KetQua != null)
                    {
                        colKQ1.Text = qhh.Where(p => p.STT == 1).First().KetQua.ToString();
                    }
                    if (qhh.Where(p => p.STT == 2).Count() > 0 && qhh.Where(p => p.STT == 2).First().MaDVct != null)
                    {
                        //labT2.Text = "X";
                    }
                    if (qhh.Where(p => p.STT == 2).Count() > 0 && qhh.Where(p => p.STT == 2).First().KetQua != null)
                    {
                        colKQ2.Text = qhh.Where(p => p.STT == 2).First().KetQua.ToString();
                    }
                    if (qhh.Where(p => p.STT == 3).Count() > 0 && qhh.Where(p => p.STT == 3).First().MaDVct != null)
                    {
                        //labT3.Text = "X";
                    }
                    if (qhh.Where(p => p.STT == 3).Count() > 0 && qhh.Where(p => p.STT == 3).First().KetQua != null)
                    {
                        colKQ3.Text = qhh.Where(p => p.STT == 3).First().KetQua.ToString();
                    }
                    if (qhh.Where(p => p.STT == 4).Count() > 0 && qhh.Where(p => p.STT == 4).First().MaDVct != null)
                    {
                        //labT4.Text = "X";
                    }
                    if (qhh.Where(p => p.STT == 4).Count() > 0 && qhh.Where(p => p.STT == 4).First().KetQua != null)
                    {
                        colKQ4.Text = qhh.Where(p => p.STT == 4).First().KetQua.ToString();
                    }
                    if (qhh.Where(p => p.STT == 5).Count() > 0 && qhh.Where(p => p.STT == 5).First().MaDVct != null)
                    {
                        //labT5.Text = "X";
                    }
                    if (qhh.Where(p => p.STT == 5).Count() > 0 && qhh.Where(p => p.STT == 5).First().KetQua != null)
                    {
                        colKQ5.Text = qhh.Where(p => p.STT == 5).First().KetQua.ToString();
                    }
                    if (qhh.Where(p => p.STT == 6).Count() > 0 && qhh.Where(p => p.STT == 6).First().MaDVct != null)
                    {
                        //labT6.Text = "X";
                    }
                    if (qhh.Where(p => p.STT == 6).Count() > 0 && qhh.Where(p => p.STT == 6).First().KetQua != null)
                    {
                        colKQ6.Text = qhh.Where(p => p.STT == 6).First().KetQua.ToString();
                    }
                    if (qhh.Where(p => p.STT == 7).Count() > 0 && qhh.Where(p => p.STT == 7).First().MaDVct != null)
                    {
                        //labT7.Text = "X";
                    }

                    if (qhh.Where(p => p.STT == 7).Count() > 0 && qhh.Where(p => p.STT == 7).First().KetQua != null)
                    {
                        colKQ7.Text = qhh.Where(p => p.STT == 7).First().KetQua.ToString();
                    }
                    if (qhh.Where(p => p.STT == 8).Count() > 0 && qhh.Where(p => p.STT == 8).First().MaDVct != null)
                    {
                        //labT8.Text = "X";
                    }
                    if (qhh.Where(p => p.STT == 8).Count() > 0 && qhh.Where(p => p.STT == 8).First().KetQua != null)
                    {
                        colKQ8.Text = qhh.Where(p => p.STT == 8).First().KetQua.ToString();
                    }
                    if (qhh.Where(p => p.STT == 9).Count() > 0 && qhh.Where(p => p.STT == 9).First().MaDVct != null)
                    {
                        //labT9.Text = "X";
                    }
                    if (qhh.Where(p => p.STT == 9).Count() > 0 && qhh.Where(p => p.STT == 9).First().KetQua != null)
                    {
                        colKQ9.Text = qhh.Where(p => p.STT == 9).First().KetQua.ToString();
                    }
                    if (qhh.Where(p => p.STT == 10).Count() > 0 && qhh.Where(p => p.STT == 10).First().MaDVct != null)
                    {
                        //labT10.Text = "X";
                    }
                    if (qhh.Where(p => p.STT == 10).Count() > 0 && qhh.Where(p => p.STT == 10).First().KetQua != null)
                    {
                        colKQ10.Text = qhh.Where(p => p.STT == 10).First().KetQua.ToString();
                    }
                    if (qhh.Where(p => p.STT == 11).Count() > 0 && qhh.Where(p => p.STT == 11).First().MaDVct != null)
                    {
                        //labT11.Text = "X";
                    }
                    if (qhh.Where(p => p.STT == 11).Count() > 0 && qhh.Where(p => p.STT == 11).First().KetQua != null)
                    {
                        colKQ11.Text = qhh.Where(p => p.STT == 11).First().KetQua.ToString();
                    }
                    if (qhh.Where(p => p.STT == 12).Count() > 0 && qhh.Where(p => p.STT == 12).First().MaDVct != null)
                    {
                        //labT12.Text = "X";
                    }
                    if (qhh.Where(p => p.STT == 12).Count() > 0 && qhh.Where(p => p.STT == 12).First().KetQua != null)
                    {
                        colKQ12.Text = qhh.Where(p => p.STT == 12).First().KetQua.ToString();
                    }
                    if (qhh.Where(p => p.STT == 13).Count() > 0 && qhh.Where(p => p.STT == 13).First().MaDVct != null)
                    {
                        //labT13.Text = "X";
                    }
                    if (qhh.Where(p => p.STT == 13).Count() > 0 && qhh.Where(p => p.STT == 13).First().KetQua != null)
                    {
                        colKQ13.Text = qhh.Where(p => p.STT == 13).First().KetQua.ToString();
                    }
                    if (qhh.Where(p => p.STT == 14).Count() > 0 && qhh.Where(p => p.STT == 14).First().MaDVct != null)
                    {
                        //labT14.Text = "X";
                    }
                    if (qhh.Where(p => p.STT == 14).Count() > 0 && qhh.Where(p => p.STT == 14).First().KetQua != null)
                    {
                        colKQ14.Text = qhh.Where(p => p.STT == 14).First().KetQua.ToString();
                    }
                    if (qhh.Where(p => p.STT == 15).Count() > 0 && qhh.Where(p => p.STT == 15).First().MaDVct != null)
                    {
                        //labT15.Text = "X";
                    }
                    if (qhh.Where(p => p.STT == 15).Count() > 0 && qhh.Where(p => p.STT == 15).First().KetQua != null)
                    {
                        colKQ15.Text = qhh.Where(p => p.STT == 15).First().KetQua.ToString();
                    }
                    if (qhh.Where(p => p.STT == 16).Count() > 0 && qhh.Where(p => p.STT == 16).First().MaDVct != null)
                    {
                        //labT16.Text = "X";
                    }
                    if (qhh.Where(p => p.STT == 16).Count() > 0 && qhh.Where(p => p.STT == 16).First().KetQua != null)
                    {
                        colKQ16.Text = qhh.Where(p => p.STT == 16).First().KetQua.ToString();
                    }
                    if (qhh.Where(p => p.STT == 17).Count() > 0 && qhh.Where(p => p.STT == 17).First().MaDVct != null)
                    {
                        //labT17.Text = "X";
                    }
                    if (qhh.Where(p => p.STT == 17).Count() > 0 && qhh.Where(p => p.STT == 17).First().KetQua != null)
                    {
                        colKQ17.Text = qhh.Where(p => p.STT == 17).First().KetQua.ToString();
                    }
                    if (qhh.Where(p => p.STT == 18).Count() > 0 && qhh.Where(p => p.STT == 18).First().MaDVct != null)
                    {
                        //labT18.Text = "X";
                    }
                    if (qhh.Where(p => p.STT == 18).Count() > 0 && qhh.Where(p => p.STT == 18).First().KetQua != null)
                    {
                        colKQ18.Text = qhh.Where(p => p.STT == 18).First().KetQua.ToString();
                    }
                    if (qhh.Where(p => p.STT == 19).Count() > 0 && qhh.Where(p => p.STT == 19).First().MaDVct != null)
                    {
                        //labT19.Text = "X";
                    }
                    if (qhh.Where(p => p.STT == 19).Count() > 0 && qhh.Where(p => p.STT == 19).First().KetQua != null)
                    {
                        colKQ19.Text = qhh.Where(p => p.STT == 19).First().KetQua.ToString();
                    }
                    if (qhh.Where(p => p.STT == 20).Count() > 0 && qhh.Where(p => p.STT == 20).First().MaDVct != null)
                    {
                        //labT20.Text = "X";
                    }
                    if (qhh.Where(p => p.STT == 20).Count() > 0 && qhh.Where(p => p.STT == 20).First().KetQua != null)
                    {
                        colKQ20.Text = qhh.Where(p => p.STT == 20).First().KetQua.ToString();
                    }
                    if (qhh.Where(p => p.STT == 21).Count() > 0 && qhh.Where(p => p.STT == 21).First().MaDVct != null)
                    {
                        //labT21.Text = "X";
                    }
                    if (qhh.Where(p => p.STT == 21).Count() > 0 && qhh.Where(p => p.STT == 21).First().KetQua != null)
                    {
                        colKQ21.Text = qhh.Where(p => p.STT == 21).First().KetQua.ToString();
                    }
                    if (qhh.Where(p => p.STT == 22).Count() > 0 && qhh.Where(p => p.STT == 22).First().MaDVct != null)
                    {
                         //if(DungChung.Bien.MaBV=="30004")
                             //labT22.Text = "X";
                    }
                    if (qhh.Where(p => p.STT == 22).Count() > 0 && qhh.Where(p => p.STT == 22).First().KetQua != null)
                    {
                        colKQ22.Text = qhh.Where(p => p.STT == 22).First().KetQua.ToString();
                    }
                    if (qhh.Where(p => p.STT == 23).Count() > 0 && qhh.Where(p => p.STT == 23).First().MaDVct != null)
                    {

                        ////labT23.Text = "X";
                    }
                    if (qhh.Where(p => p.STT == 23).Count() > 0 && qhh.Where(p => p.STT == 23).First().KetQua != null)
                    {
                        colKQ23.Text = qhh.Where(p => p.STT == 23).First().KetQua.ToString();
                    }
                    if (qhh.Where(p => p.STT == 24).Count() > 0 && qhh.Where(p => p.STT == 24).First().MaDVct != null)
                    {
                        labT24.Text = "X";
                        lab46.Visible = true;
                        if (DungChung.Bien.MaBV == "24009")
                            lab46.Text = "giây...";
                    }
                    if (qhh.Where(p => p.STT == 24).Count() > 0 && qhh.Where(p => p.STT == 24).First().KetQua != null)
                    {
                        colkq24.Text = qhh.Where(p => p.STT == 24).First().KetQua.ToString();
                    }
                    if (qhh.Where(p => p.STT == 25).Count() > 0 && qhh.Where(p => p.STT == 25).First().MaDVct != null)
                    {
                        labT25.Text = "X";
                        lab48.Visible = true;
                        if (DungChung.Bien.MaBV == "24009" )
                            lab48.Text = "giây...";
                    }
                    if (qhh.Where(p => p.STT == 25).Count() > 0 && qhh.Where(p => p.STT == 25).First().KetQua != null)
                    {
                        colkq25.Text = qhh.Where(p => p.STT == 25).First().KetQua.ToString();
                    }
                    if (qhh.Where(p => p.STT == 26).Count() > 0 && qhh.Where(p => p.STT == 26).First().MaDVct != null)
                    {
                        labT26.Text = "X";
                    }
                    if (qhh.Where(p => p.STT == 26).Count() > 0 && qhh.Where(p => p.STT == 26).First().KetQua != null)
                    {
                        colkq26.Text = qhh.Where(p => p.STT == 26).First().KetQua.ToString();
                    }
                    if (qhh.Where(p => p.STT == 27).Count() > 0 && qhh.Where(p => p.STT == 27).First().MaDVct != null)
                    {
                        labT27.Text = "X";
                    }
                    if (qhh.Where(p => p.STT == 27).Count() > 0 && qhh.Where(p => p.STT == 27).First().KetQua != null)
                    {
                        colkq27.Text = qhh.Where(p => p.STT == 27).First().KetQua.ToString();
                    }
                    if (qhh.Where(p => p.STT == 30).Count() > 0 && qhh.Where(p => p.STT == 30).First().KetQua != null)
                    {
                        colkq30.Text = qhh.Where(p => p.STT == 24).First().KetQua.ToString();
                    }
                    if (qhh.Where(p => p.STT == 30).Count() > 0 && qhh.Where(p => p.STT == 30).First().KetQua != null)
                    {
                        colkq31.Text = qhh.Where(p => p.STT == 24).First().KetQua.ToString();
                    }

                }
            if(DungChung.Bien.MaBV=="04011")  
            {
                tab2.Visible = false;
            }
             
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {         
            if (DungChung.Bien.MaBV == "04011" || DungChung.Bien.MaBV == "08602" || DungChung.Bien.MaBV=="30009")
            {
                colSo.Visible = false;
                colTenTKXN.Visible = true;
            }
            else {
                colSo.Visible = false;
                
            }
            colTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper() ;
            colTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();  
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            
            if (DungChung.Bien.MaBV == "04012")
                lab55.Text = "Trưởng khoa cận lâm sàng".ToUpper();

            if (MaCBDT.Value!=null)
            {
                colTenBSDT.Text = DungChung.Ham._getTenCB(DataContect, MaCBDT.Value.ToString());
            }
          
            if (Macb.Value!=null)
            {
                colTenTKXN.Text = DungChung.Ham._getTenCB(DataContect, Macb.Value.ToString());
            }
            if (DungChung.Bien.MaBV == "30009")
                xrRichText1.Visible = true;
            else
                xrRichText1.Visible = false;
            if (DungChung.Bien.MaBV == "30004")
            {
                xrTableCell5.Visible = false;
                colTenBSDT.Visible = false;
                colTenTKXN.Visible = false;
            }
        }

        private void lab44_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "04005") {
                lab44.Text = "Đông, cầm máu:";
            }
        }

        private void lab55_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "30007")
            {
                lab55.Text = "Bác Sỹ Cận Lâm Sàng".ToUpper();
            }
        }


        internal void bindingData()
        {
            celMAQD.DataBindings.Add("Text", DataSource, "MaQD");
            celCheckDV.DataBindings.Add("Text", DataSource, "ck");
            celTenDV.DataBindings.Add("Text", DataSource, "TenDV");  
        }
    }
}
