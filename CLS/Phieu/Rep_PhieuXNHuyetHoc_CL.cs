using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
using System.Collections.Generic;

namespace QLBV.BaoCao
{
    public partial class Rep_PhieuXNHuyetHoc_CL : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_PhieuXNHuyetHoc_CL()
        {
            InitializeComponent();
            _lTen.Add("XN huyết học");
        }
        QLBV_Database.QLBVEntities DataContect = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        List<string> _lTen = new List<string>();
        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            var qcls = (from dvct in DataContect.DichVucts
                        join dv in DataContect.DichVus on dvct.MaDV equals dv.MaDV
                        join tnhomdv in DataContect.TieuNhomDVs on dv.IdTieuNhom equals tnhomdv.IdTieuNhom
                        where (tnhomdv.TenRG.Contains("XN huyết học"))
                        select new
                        {
                            dvct.MaDVct,
                            dvct.TenDVct,
                            dvct.TSBT,
                            dvct.STT,
                            dvct.DonVi
                        }).ToList();
            if (qcls.Count > 0)
            {
                if (qcls.Where(p => p.STT == 1).Count() > 0 && qcls.Where(p => p.STT == 1).First().TenDVct != null)
                {
                    XN1.Text = qcls.Where(p => p.STT == 1).First().TenDVct.ToString();
                }
                else { XN1.Text = ""; }
                //TS1a.Text = qcls.Where(p => p.STT == 1).First().TSBT.ToString();

                if (qcls.Where(p => p.STT == 1).Count() > 0 && qcls.Where(p => p.STT == 1).First().DonVi != null)
                {
                    DV1.Text = qcls.Where(p => p.STT == 1).First().DonVi.ToString();
                    DV1a.Text = qcls.Where(p => p.STT == 1).First().DonVi.ToString();
                }
                else { DV1.Text = ""; DV1a.Text = ""; }
                if (qcls.Where(p => p.STT == 2).Count() > 0 && qcls.Where(p => p.STT == 2).First().TenDVct != null)
                {
                    XN2.Text = qcls.Where(p => p.STT == 2).First().TenDVct.ToString();
                }
                else { XN2.Text = ""; }

                if (qcls.Where(p => p.STT == 2).Count() > 0 && qcls.Where(p => p.STT == 2).First().DonVi != null)
                {
                    DV2.Text = qcls.Where(p => p.STT == 2).First().DonVi.ToString();
                    DV2a.Text = qcls.Where(p => p.STT == 2).First().DonVi.ToString();
                }
                else { DV2.Text = ""; DV2a.Text = ""; }
                if (qcls.Where(p => p.STT == 3).Count() > 0 && qcls.Where(p => p.STT == 3).First().TenDVct != null)
                {
                    XN3.Text = qcls.Where(p => p.STT == 3).First().TenDVct.ToString();
                }
                else { XN3.Text = ""; }
                if (qcls.Where(p => p.STT == 3).Count() > 0 && qcls.Where(p => p.STT == 3).First().DonVi != null)
                {
                    DV3.Text = qcls.Where(p => p.STT == 3).First().DonVi.ToString();
                    DV3a.Text = qcls.Where(p => p.STT == 3).First().DonVi.ToString();
                }
                else { DV3.Text = ""; DV3a.Text = ""; }
                if (qcls.Where(p => p.STT == 4).Count() > 0 && qcls.Where(p => p.STT == 4).First().TenDVct != null)
                {
                    XN4.Text = qcls.Where(p => p.STT == 4).First().TenDVct.ToString();
                }
                else { XN4.Text = ""; }

                if (qcls.Where(p => p.STT == 4).Count() > 0 && qcls.Where(p => p.STT == 4).First().TSBT != null)
                {
                    TS4.Text = qcls.Where(p => p.STT == 4).First().TSBT.ToString();
                }
                else { TS4.Text = ""; }

                if (qcls.Where(p => p.STT == 4).Count() > 0 && qcls.Where(p => p.STT == 4).First().DonVi != null)
                {
                    DV4.Text = qcls.Where(p => p.STT == 4).First().DonVi.ToString();
                }
                else { DV4.Text = ""; }
                if (qcls.Where(p => p.STT == 5).Count() > 0 && qcls.Where(p => p.STT == 5).First().TenDVct != null)
                {
                    XN5.Text = qcls.Where(p => p.STT == 5).First().TenDVct.ToString();
                }
                else { XN5.Text = ""; }

                if (qcls.Where(p => p.STT == 5).Count() > 0 && qcls.Where(p => p.STT == 5).First().DonVi != null)
                {
                    DV5.Text = qcls.Where(p => p.STT == 5).First().DonVi.ToString();
                    DV5a.Text = qcls.Where(p => p.STT == 5).First().DonVi.ToString();
                }
                else { DV5.Text = ""; DV5a.Text = ""; }
                if (qcls.Where(p => p.STT == 6).Count() > 0 && qcls.Where(p => p.STT == 6).First().TenDVct != null)
                {
                    XN6.Text = qcls.Where(p => p.STT == 6).First().TenDVct.ToString();
                }
                else { XN6.Text = ""; }

                if (qcls.Where(p => p.STT == 6).Count() > 0 && qcls.Where(p => p.STT == 6).First().TSBT != null)
                {
                    TS6.Text = qcls.Where(p => p.STT == 6).First().TSBT.ToString();
                }
                else { TS6.Text = ""; }

                if (qcls.Where(p => p.STT == 6).Count() > 0 && qcls.Where(p => p.STT == 6).First().DonVi != null)
                {
                    DV6.Text = qcls.Where(p => p.STT == 6).First().DonVi.ToString();
                }
                else { DV6.Text = ""; }

                if (qcls.Where(p => p.STT == 7).Count() > 0 && qcls.Where(p => p.STT == 7).First().TenDVct != null)
                {
                    XN7.Text = qcls.Where(p => p.STT == 7).First().TenDVct.ToString();
                }
                else { XN7.Text = ""; }

                if (qcls.Where(p => p.STT == 7).Count() > 0 && qcls.Where(p => p.STT == 7).First().TSBT != null)
                {
                    TS7.Text = qcls.Where(p => p.STT == 7).First().TSBT.ToString();
                }
                else { TS7.Text = ""; }

                if (qcls.Where(p => p.STT == 7).Count() > 0 && qcls.Where(p => p.STT == 7).First().DonVi != null)
                {
                    DV7.Text = qcls.Where(p => p.STT == 7).First().DonVi.ToString();
                }
                else { DV7.Text = ""; }
                if (qcls.Where(p => p.STT == 8).Count() > 0 && qcls.Where(p => p.STT == 8).First().TenDVct != null)
                {
                    XN8.Text = qcls.Where(p => p.STT == 8).First().TenDVct.ToString();
                }
                else { XN8.Text = ""; }

                if (qcls.Where(p => p.STT == 8).Count() > 0 && qcls.Where(p => p.STT == 8).First().TSBT != null)
                {
                    TS8.Text = qcls.Where(p => p.STT == 8).First().TSBT.ToString();
                }
                else { TS8.Text = ""; }

                if (qcls.Where(p => p.STT == 8).Count() > 0 && qcls.Where(p => p.STT == 8).First().DonVi != null)
                {
                    DV8.Text = qcls.Where(p => p.STT == 8).First().DonVi.ToString();
                }
                else { DV8.Text = ""; }
                // 11
                if (qcls.Where(p => p.STT == 11).Count() > 0 && qcls.Where(p => p.STT == 11).First().TenDVct != null)
                {
                    XN11.Text = qcls.Where(p => p.STT == 11).First().TenDVct.ToString();
                }
                else if (qcls.Where(p => p.STT == 11).Count() > 0 && qcls.Where(p => p.STT == 11).First().TenDVct != null && qcls.Where(p => p.STT == 11).First().TSBT != null)
                {
                    XN11.Text = qcls.Where(p => p.STT == 11).First().TenDVct.ToString() + qcls.Where(p => p.STT == 11).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 11).Count() > 0 && qcls.Where(p => p.STT == 11).First().TSBT != null)
                {
                    TS11.Text = qcls.Where(p => p.STT == 11).First().TSBT.ToString();
                }
                else { TS11.Text = ""; }
                if (qcls.Where(p => p.STT == 11).Count() > 0 && qcls.Where(p => p.STT == 11).First().DonVi != null)
                {
                    DV11.Text = qcls.Where(p => p.STT == 11).First().DonVi.ToString();
                }
                else { DV11.Text = ""; }
                // 12
                if (qcls.Where(p => p.STT == 12).Count() > 0 && qcls.Where(p => p.STT == 12).First().TenDVct != null)
                {
                    XN12.Text = qcls.Where(p => p.STT == 12).First().TenDVct.ToString();
                }
                else if (qcls.Where(p => p.STT == 12).Count() > 0 && qcls.Where(p => p.STT == 12).First().TenDVct != null && qcls.Where(p => p.STT == 12).First().TSBT != null)
                {
                    XN12.Text = qcls.Where(p => p.STT == 12).First().TenDVct.ToString() + qcls.Where(p => p.STT == 12).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 12).Count() > 0 && qcls.Where(p => p.STT == 12).First().TSBT != null)
                {
                    TS12.Text = qcls.Where(p => p.STT == 12).First().TSBT.ToString();
                }
                else { TS12.Text = ""; }
                if (qcls.Where(p => p.STT == 12).Count() > 0 && qcls.Where(p => p.STT == 12).First().DonVi != null)
                {
                    DV12.Text = qcls.Where(p => p.STT == 12).First().DonVi.ToString();
                }
                else { DV12.Text = ""; }
                // 13
                if (qcls.Where(p => p.STT == 13).Count() > 0 && qcls.Where(p => p.STT == 13).First().TenDVct != null)
                {
                    XN13.Text = qcls.Where(p => p.STT == 13).First().TenDVct.ToString();
                }
                else if (qcls.Where(p => p.STT == 13).Count() > 0 && qcls.Where(p => p.STT == 13).First().TenDVct != null && qcls.Where(p => p.STT == 13).First().TSBT != null)
                {
                    XN13.Text = qcls.Where(p => p.STT == 13).First().TenDVct.ToString() + qcls.Where(p => p.STT == 13).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 13).Count() > 0 && qcls.Where(p => p.STT == 13).First().TSBT != null)
                {
                    TS13.Text = qcls.Where(p => p.STT == 13).First().TSBT.ToString();
                }
                else { TS13.Text = ""; }
                if (qcls.Where(p => p.STT == 13).Count() > 0 && qcls.Where(p => p.STT == 13).First().DonVi != null)
                {
                    DV13.Text = qcls.Where(p => p.STT == 13).First().DonVi.ToString();
                }
                else { DV13.Text = ""; }
                // 14
                if (qcls.Where(p => p.STT == 14).Count() > 0 && qcls.Where(p => p.STT == 14).First().TenDVct != null)
                {
                    XN14.Text = qcls.Where(p => p.STT == 14).First().TenDVct.ToString();
                }
                else if (qcls.Where(p => p.STT == 14).Count() > 0 && qcls.Where(p => p.STT == 14).First().TenDVct != null && qcls.Where(p => p.STT == 14).First().TSBT != null)
                {
                    XN14.Text = qcls.Where(p => p.STT == 14).First().TenDVct.ToString() + qcls.Where(p => p.STT == 14).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 14).Count() > 0 && qcls.Where(p => p.STT == 14).First().TSBT != null)
                {
                    TS14.Text = qcls.Where(p => p.STT == 14).First().TSBT.ToString();
                }
                else { TS14.Text = ""; }
                if (qcls.Where(p => p.STT == 14).Count() > 0 && qcls.Where(p => p.STT == 14).First().DonVi != null)
                {
                    DV14.Text = qcls.Where(p => p.STT == 14).First().DonVi.ToString();
                }
                else { DV14.Text = ""; }
                // 15
                if (qcls.Where(p => p.STT == 15).Count() > 0 && qcls.Where(p => p.STT == 15).First().TenDVct != null)
                {
                    XN15.Text = qcls.Where(p => p.STT == 15).First().TenDVct.ToString();
                }
                else if (qcls.Where(p => p.STT == 15).Count() > 0 && qcls.Where(p => p.STT == 15).First().TenDVct != null && qcls.Where(p => p.STT == 15).First().TSBT != null)
                {
                    XN15.Text = qcls.Where(p => p.STT == 15).First().TenDVct.ToString() + qcls.Where(p => p.STT == 15).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 15).Count() > 0 && qcls.Where(p => p.STT == 15).First().TSBT != null)
                {
                    TS15.Text = qcls.Where(p => p.STT == 15).First().TSBT.ToString();
                }
                else { TS15.Text = ""; }
                if (qcls.Where(p => p.STT == 15).Count() > 0 && qcls.Where(p => p.STT == 15).First().DonVi != null)
                {
                    DV15.Text = qcls.Where(p => p.STT == 15).First().DonVi.ToString();
                }
                else { DV15.Text = ""; }
                // 16
                if (qcls.Where(p => p.STT == 16).Count() > 0 && qcls.Where(p => p.STT == 16).First().TenDVct != null)
                {
                    XN16.Text = qcls.Where(p => p.STT == 16).First().TenDVct.ToString();
                }
                else if (qcls.Where(p => p.STT == 16).Count() > 0 && qcls.Where(p => p.STT == 16).First().TenDVct != null && qcls.Where(p => p.STT == 16).First().TSBT != null)
                {
                    XN16.Text = qcls.Where(p => p.STT == 16).First().TenDVct.ToString() + qcls.Where(p => p.STT == 16).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 16).Count() > 0 && qcls.Where(p => p.STT == 16).First().TSBT != null)
                {
                    TS16.Text = qcls.Where(p => p.STT == 16).First().TSBT.ToString();
                }
                else { TS16.Text = ""; }
                if (qcls.Where(p => p.STT == 16).Count() > 0 && qcls.Where(p => p.STT == 16).First().DonVi != null)
                {
                    DV16.Text = qcls.Where(p => p.STT == 16).First().DonVi.ToString();
                }
                else { DV16.Text = ""; }
                // 17
                if (qcls.Where(p => p.STT == 17).Count() > 0 && qcls.Where(p => p.STT == 17).First().TenDVct != null)
                {
                    XN17.Text = qcls.Where(p => p.STT == 17).First().TenDVct.ToString();
                }
                else if (qcls.Where(p => p.STT == 17).Count() > 0 && qcls.Where(p => p.STT == 17).First().TenDVct != null && qcls.Where(p => p.STT == 17).First().TSBT != null)
                {
                    XN17.Text = qcls.Where(p => p.STT == 17).First().TenDVct.ToString() + qcls.Where(p => p.STT == 17).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 17).Count() > 0 && qcls.Where(p => p.STT == 17).First().TSBT != null)
                {
                    TS17.Text = qcls.Where(p => p.STT == 17).First().TSBT.ToString();
                }
                else { TS17.Text = ""; }
                if (qcls.Where(p => p.STT == 17).Count() > 0 && qcls.Where(p => p.STT == 17).First().DonVi != null)
                {
                    DV17.Text = qcls.Where(p => p.STT == 17).First().DonVi.ToString();
                }
                else { DV17.Text = ""; }
                // 18
                if (qcls.Where(p => p.STT == 18).Count() > 0 && qcls.Where(p => p.STT == 18).First().TenDVct != null)
                {
                    XN18.Text = qcls.Where(p => p.STT == 18).First().TenDVct.ToString();
                }
                else if (qcls.Where(p => p.STT == 18).Count() > 0 && qcls.Where(p => p.STT == 18).First().TenDVct != null && qcls.Where(p => p.STT == 18).First().TSBT != null)
                {
                    XN18.Text = qcls.Where(p => p.STT == 18).First().TenDVct.ToString() + qcls.Where(p => p.STT == 18).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 18).Count() > 0 && qcls.Where(p => p.STT == 18).First().TSBT != null)
                {
                    TS18.Text = qcls.Where(p => p.STT == 18).First().TSBT.ToString();
                }
                else { TS18.Text = ""; }
                if (qcls.Where(p => p.STT == 18).Count() > 0 && qcls.Where(p => p.STT == 18).First().DonVi != null)
                {
                    DV18.Text = qcls.Where(p => p.STT == 18).First().DonVi.ToString();
                }
                else { DV18.Text = ""; }
                // 19
                if (qcls.Where(p => p.STT == 19).Count() > 0 && qcls.Where(p => p.STT == 19).First().TenDVct != null)
                {
                    XN19.Text = qcls.Where(p => p.STT == 19).First().TenDVct.ToString();
                }
                else if (qcls.Where(p => p.STT == 19).Count() > 0 && qcls.Where(p => p.STT == 19).First().TenDVct != null && qcls.Where(p => p.STT == 19).First().TSBT != null)
                {
                    XN19.Text = qcls.Where(p => p.STT == 19).First().TenDVct.ToString() + qcls.Where(p => p.STT == 19).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 19).Count() > 0 && qcls.Where(p => p.STT == 19).First().TSBT != null)
                {
                    TS19.Text = qcls.Where(p => p.STT == 19).First().TSBT.ToString();
                }
                else { TS19.Text = ""; }
                if (qcls.Where(p => p.STT == 19).Count() > 0 && qcls.Where(p => p.STT == 19).First().DonVi != null)
                {
                    DV19.Text = qcls.Where(p => p.STT == 19).First().DonVi.ToString();
                }
                else { DV19.Text = ""; }
                // 20
                if (qcls.Where(p => p.STT == 20).Count() > 0 && qcls.Where(p => p.STT == 20).First().TenDVct != null)
                {
                    XN20.Text = qcls.Where(p => p.STT == 20).First().TenDVct.ToString();
                }
                else if (qcls.Where(p => p.STT == 20).Count() > 0 && qcls.Where(p => p.STT == 20).First().TenDVct != null && qcls.Where(p => p.STT == 20).First().TSBT != null)
                {
                    XN20.Text = qcls.Where(p => p.STT == 20).First().TenDVct.ToString() + qcls.Where(p => p.STT == 20).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 20).Count() > 0 && qcls.Where(p => p.STT == 20).First().TSBT != null)
                {
                    TS20.Text = qcls.Where(p => p.STT == 20).First().TSBT.ToString();
                }
                else { TS20.Text = ""; }
                if (qcls.Where(p => p.STT == 20).Count() > 0 && qcls.Where(p => p.STT == 20).First().DonVi != null)
                {
                    DV20.Text = qcls.Where(p => p.STT == 20).First().DonVi.ToString();
                }
                else { DV20.Text = ""; }
                ///////////////////////////////////////
                int sophieu = int.Parse(paramIdCLS.Value.ToString());
                string _nam = ""; string _nu = "";
                if (Nam.Value != null) { _nam = Nam.Value.ToString(); }
                if (Nu.Value != null)
                { _nu = Nu.Value.ToString(); }
                var qhh = (from cls in DataContect.CLS.Where(p => p.IdCLS == sophieu)
                           join chidinh in DataContect.ChiDinhs on cls.IdCLS equals chidinh.IdCLS
                           join clsct in DataContect.CLScts on chidinh.IDCD equals clsct.IDCD
                           join dvct in DataContect.DichVucts on clsct.MaDVct equals dvct.MaDVct
                           join dv in DataContect.DichVus on dvct.MaDV equals dv.MaDV
                           join tndv in DataContect.TieuNhomDVs on dv.IdTieuNhom equals tndv.IdTieuNhom
                           where tndv.TenRG == "XN huyết học"
                           select new { clsct.MaDVct, clsct.KetQua, dvct.STT, dvct.TSBT, dvct.TSBTnu }).ToList();
                if (qhh.Count > 0)
                {
                    if (_nam == "X")
                    {
                        if (qhh.Where(p => p.STT == 1).Count() > 0 && qhh.Where(p => p.STT == 1).First().MaDVct != null)
                        {
                            C1.Text = "X";
                        }
                        if (qhh.Where(p => p.STT == 1).Count() > 0 && qhh.Where(p => p.STT == 1).First().KetQua != null)
                        {
                            KQ1a.Text = qhh.Where(p => p.STT == 1).First().KetQua.ToString();
                        }
                        if (qhh.Where(p => p.STT == 2).Count() > 0 && qhh.Where(p => p.STT == 2).First().MaDVct != null)
                        {
                            C2.Text = "X";
                        }
                        if (qhh.Where(p => p.STT == 2).Count() > 0 && qhh.Where(p => p.STT == 2).First().KetQua != null)
                        {
                            KQ2a.Text = qhh.Where(p => p.STT == 2).First().KetQua.ToString();
                        }
                        if (qhh.Where(p => p.STT == 3).Count() > 0 && qhh.Where(p => p.STT == 3).First().MaDVct != null)
                        {
                            C3.Text = "X";
                        }
                        if (qhh.Where(p => p.STT == 3).Count() > 0 && qhh.Where(p => p.STT == 3).First().KetQua != null)
                        {
                            KQ3a.Text = qhh.Where(p => p.STT == 3).First().KetQua.ToString();
                        }
                        if (qhh.Where(p => p.STT == 4).Count() > 0 && qhh.Where(p => p.STT == 4).First().MaDVct != null)
                        {
                            C4.Text = "X";
                        }
                        if (qhh.Where(p => p.STT == 4).Count() > 0 && qhh.Where(p => p.STT == 4).First().KetQua != null)
                        {
                            KQ4.Text = qhh.Where(p => p.STT == 4).First().KetQua.ToString();
                        }
                        if (qhh.Where(p => p.STT == 5).Count() > 0 && qhh.Where(p => p.STT == 5).First().MaDVct != null)
                        {
                            C5.Text = "X";
                        }
                        if (qhh.Where(p => p.STT == 5).Count() > 0 && qhh.Where(p => p.STT == 5).First().KetQua != null)
                        {
                            KQ5a.Text = qhh.Where(p => p.STT == 5).First().KetQua.ToString();
                        }

                    }
                    if (_nu == "X")
                    {
                        if (qhh.Where(p => p.STT == 1).Count() > 0 && qhh.Where(p => p.STT == 1).First().MaDVct != null)
                        {
                            C1.Text = "X";
                        }
                        if (qhh.Where(p => p.STT == 1).Count() > 0 && qhh.Where(p => p.STT == 1).First().KetQua != null)
                        {
                            KQ1.Text = qhh.Where(p => p.STT == 1).First().KetQua.ToString();
                        }
                        if (qhh.Where(p => p.STT == 2).Count() > 0 && qhh.Where(p => p.STT == 2).First().MaDVct != null)
                        {
                            C2.Text = "X";
                        }
                        if (qhh.Where(p => p.STT == 2).Count() > 0 && qhh.Where(p => p.STT == 2).First().KetQua != null)
                        {
                            KQ2.Text = qhh.Where(p => p.STT == 2).First().KetQua.ToString();
                        }
                        if (qhh.Where(p => p.STT == 3).Count() > 0 && qhh.Where(p => p.STT == 3).First().MaDVct != null)
                        {
                            C3.Text = "X";
                        }
                        if (qhh.Where(p => p.STT == 3).Count() > 0 && qhh.Where(p => p.STT == 3).First().KetQua != null)
                        {
                            KQ3.Text = qhh.Where(p => p.STT == 3).First().KetQua.ToString();
                        }
                        if (qhh.Where(p => p.STT == 4).Count() > 0 && qhh.Where(p => p.STT == 4).First().MaDVct != null)
                        {
                            C4.Text = "X";
                        }
                        if (qhh.Where(p => p.STT == 4).Count() > 0 && qhh.Where(p => p.STT == 4).First().KetQua != null)
                        {
                            KQ4.Text = qhh.Where(p => p.STT == 4).First().KetQua.ToString();
                        }
                        if (qhh.Where(p => p.STT == 5).Count() > 0 && qhh.Where(p => p.STT == 5).First().MaDVct != null)
                        {
                            C5.Text = "X";
                        }
                        if (qhh.Where(p => p.STT == 5).Count() > 0 && qhh.Where(p => p.STT == 5).First().KetQua != null)
                        {
                            KQ5.Text = qhh.Where(p => p.STT == 5).First().KetQua.ToString();
                        }
                    }
                    if (qhh.Where(p => p.STT == 4).Count() > 0 && qhh.Where(p => p.STT == 4).First().MaDVct != null)
                    {
                        C6.Text = "X";
                    }
                    if (qhh.Where(p => p.STT == 6).Count() > 0 && qhh.Where(p => p.STT == 6).First().MaDVct != null)
                    {
                        C6.Text = "X";
                    }
                    if (qhh.Where(p => p.STT == 6).Count() > 0 && qhh.Where(p => p.STT == 6).First().KetQua != null)
                    {
                        KQ6.Text = qhh.Where(p => p.STT == 6).First().KetQua.ToString();
                    }
                    if (qhh.Where(p => p.STT == 7).Count() > 0 && qhh.Where(p => p.STT == 7).First().MaDVct != null)
                    {
                        C7.Text = "X";
                    }

                    if (qhh.Where(p => p.STT == 7).Count() > 0 && qhh.Where(p => p.STT == 7).First().KetQua != null)
                    {
                        KQ7.Text = qhh.Where(p => p.STT == 7).First().KetQua.ToString();
                    }
                    if (qhh.Where(p => p.STT == 8).Count() > 0 && qhh.Where(p => p.STT == 8).First().MaDVct != null)
                    {
                        C8.Text = "X";
                    }
                    if (qhh.Where(p => p.STT == 8).Count() > 0 && qhh.Where(p => p.STT == 8).First().KetQua != null)
                    {
                        KQ8.Text = qhh.Where(p => p.STT == 8).First().KetQua.ToString();
                    }
                    if (qhh.Where(p => p.STT == 11).Count() > 0 && qhh.Where(p => p.STT == 11).First().MaDVct != null)
                    {
                        C11.Text = "X";
                    }
                    if (qhh.Where(p => p.STT == 11).Count() > 0 && qhh.Where(p => p.STT == 11).First().KetQua != null)
                    {
                        KQ11.Text = qhh.Where(p => p.STT == 11).First().KetQua.ToString();
                    }
                    if (qhh.Where(p => p.STT == 12).Count() > 0 && qhh.Where(p => p.STT == 12).First().MaDVct != null)
                    {
                        C12.Text = "X";
                    }
                    if (qhh.Where(p => p.STT == 12).Count() > 0 && qhh.Where(p => p.STT == 12).First().KetQua != null)
                    {
                        KQ12.Text = qhh.Where(p => p.STT == 12).First().KetQua.ToString();
                    }
                    if (qhh.Where(p => p.STT == 13).Count() > 0 && qhh.Where(p => p.STT == 13).First().MaDVct != null)
                    {
                        C13.Text = "X";
                    }
                    if (qhh.Where(p => p.STT == 13).Count() > 0 && qhh.Where(p => p.STT == 13).First().KetQua != null)
                    {
                        KQ13.Text = qhh.Where(p => p.STT == 13).First().KetQua.ToString();
                    }
                    if (qhh.Where(p => p.STT == 14).Count() > 0 && qhh.Where(p => p.STT == 14).First().MaDVct != null)
                    {
                        C14.Text = "X";
                    }
                    if (qhh.Where(p => p.STT == 14).Count() > 0 && qhh.Where(p => p.STT == 14).First().KetQua != null)
                    {
                        KQ14.Text = qhh.Where(p => p.STT == 14).First().KetQua.ToString();
                    }
                    if (qhh.Where(p => p.STT == 15).Count() > 0 && qhh.Where(p => p.STT == 15).First().MaDVct != null)
                    {
                        C15.Text = "X";
                    }
                    if (qhh.Where(p => p.STT == 15).Count() > 0 && qhh.Where(p => p.STT == 15).First().KetQua != null)
                    {
                        KQ15.Text = qhh.Where(p => p.STT == 15).First().KetQua.ToString();
                    }
                    if (qhh.Where(p => p.STT == 16).Count() > 0 && qhh.Where(p => p.STT == 16).First().MaDVct != null)
                    {
                        C16.Text = "X";
                    }
                    if (qhh.Where(p => p.STT == 16).Count() > 0 && qhh.Where(p => p.STT == 16).First().KetQua != null)
                    {
                        KQ16.Text = qhh.Where(p => p.STT == 16).First().KetQua.ToString();
                    }
                    if (qhh.Where(p => p.STT == 17).Count() > 0 && qhh.Where(p => p.STT == 17).First().MaDVct != null)
                    {
                        C17.Text = "X";
                    }
                    if (qhh.Where(p => p.STT == 17).Count() > 0 && qhh.Where(p => p.STT == 17).First().KetQua != null)
                    {
                        KQ17.Text = qhh.Where(p => p.STT == 17).First().KetQua.ToString();
                    }
                    if (qhh.Where(p => p.STT == 18).Count() > 0 && qhh.Where(p => p.STT == 18).First().MaDVct != null)
                    {
                        C18.Text = "X";
                    }
                    if (qhh.Where(p => p.STT == 18).Count() > 0 && qhh.Where(p => p.STT == 18).First().KetQua != null)
                    {
                        KQ18.Text = qhh.Where(p => p.STT == 18).First().KetQua.ToString();
                    }
                    if (qhh.Where(p => p.STT == 19).Count() > 0 && qhh.Where(p => p.STT == 19).First().MaDVct != null)
                    {
                        C19.Text = "X";
                    }
                    if (qhh.Where(p => p.STT == 19).Count() > 0 && qhh.Where(p => p.STT == 19).First().KetQua != null)
                    {
                        KQ19.Text = qhh.Where(p => p.STT == 19).First().KetQua.ToString();
                    }
                    if (qhh.Where(p => p.STT == 20).Count() > 0 && qhh.Where(p => p.STT == 20).First().MaDVct != null)
                    {
                        C20.Text = "X";
                    }
                    if (qhh.Where(p => p.STT == 20).Count() > 0 && qhh.Where(p => p.STT == 20).First().KetQua != null)
                    {
                        KQ20.Text = qhh.Where(p => p.STT == 20).First().KetQua.ToString();
                    }
                }
            }
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            colTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            colTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            int sophieu = int.Parse(paramIdCLS.Value.ToString());

            var qtpt = (from cls in DataContect.CLS.Where(p => p.IdCLS == sophieu)
                        join chidinh in DataContect.ChiDinhs on cls.IdCLS equals chidinh.IdCLS
                        join dichvu in DataContect.DichVus.Where(p => p.TenDV.Contains("Xét nghiệm tổng phân tích tế bào ngoại vi (máy tự động)")) on chidinh.MaDV equals dichvu.MaDV
                        join tn in DataContect.TieuNhomDVs.Where(p => p.TenRG.Contains("XN huyết học")) on dichvu.IdTieuNhom equals tn.IdTieuNhom
                        select new { dichvu.TenDV }).ToList();
            if (qtpt.Count > 0)
            {
                ChTPT.Text = "X".ToUpper();
            }
            else ChTPT.Text = " ";
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            int sophieu = int.Parse(paramIdCLS.Value.ToString());

            var qml = (from cls in DataContect.CLS.Where(p => p.IdCLS == sophieu)
                       join chidinh in DataContect.ChiDinhs on cls.IdCLS equals chidinh.IdCLS
                       join dichvu in DataContect.DichVus on chidinh.MaDV equals dichvu.MaDV
                       join dvct in DataContect.DichVucts on dichvu.MaDV equals dvct.MaDV
                       select new { dichvu.TenDV, dvct.STT }).ToList();
            if (qml.Where(p => p.STT == 25).Count() > 0)
            {
                ChML.Text = "X".ToUpper();
            }
            else ChML.Text = " ";
            if (qml.Where(p => p.STT == 26).Count() > 0)
            {
                ChMC.Text = "X".ToUpper();
            }
            else ChMC.Text = " ";
            if (qml.Where(p => p.STT == 27).Count() > 0)
            {
                ChMD.Text = "X".ToUpper();
            }
            else ChMD.Text = " ";

            if (qml.Where(p => p.STT == 28 || p.STT == 29).Count() > 0)
            {
                ChDNM.Text = "X".ToUpper();
            }
            else ChDNM.Text = " ";

            if (MaBSDT.Value != null)
            {
                colTenBSDT.Text = DungChung.Ham._getTenCB(DataContect, MaBSDT.Value.ToString());
            }

            if (Macb.Value != null)
            {
                if (DungChung.Bien.MaBV == "27183")
                {
                    var canBo = DataContect.CanBoes.FirstOrDefault(p => p.MaCB == Macb.Value);
                    if (canBo != null)
                    {
                        colTenTKXN.Text = canBo.CapBac + ". " + canBo.TenCB;
                    }
                }
                else
                    colTenTKXN.Text = DungChung.Ham._getTenCB(DataContect, Macb.Value.ToString());
            }
        }

        private void TS1_BeforePrint(object sender, CancelEventArgs e)
        {
            TS1.Text = DungChung.Ham.layTSBT(DataContect, "1", _lTen, 0)[4];
        }

        private void TS1a_BeforePrint(object sender, CancelEventArgs e)
        {
            TS1a.Text = DungChung.Ham.layTSBT(DataContect, "1", _lTen, 0)[3];
        }

        private void TS2_BeforePrint(object sender, CancelEventArgs e)
        {
            TS2.Text = DungChung.Ham.layTSBT(DataContect, "2", _lTen, 0)[4];
        }

        private void TS2a_BeforePrint(object sender, CancelEventArgs e)
        {
            TS2a.Text = DungChung.Ham.layTSBT(DataContect, "2", _lTen, 0)[3];
        }

        private void TS3_BeforePrint(object sender, CancelEventArgs e)
        {
            TS3.Text = DungChung.Ham.layTSBT(DataContect, "3", _lTen, 0)[4];
        }

        private void TS3a_BeforePrint(object sender, CancelEventArgs e)
        {
            TS3a.Text = DungChung.Ham.layTSBT(DataContect, "3", _lTen, 0)[3];
        }

        private void TS5_BeforePrint(object sender, CancelEventArgs e)
        {
            TS5.Text = DungChung.Ham.layTSBT(DataContect, "5", _lTen, 0)[4];
        }

        private void TS5a_BeforePrint(object sender, CancelEventArgs e)
        {
            TS5a.Text = DungChung.Ham.layTSBT(DataContect, "5", _lTen, 0)[3];
        }
    }
}
