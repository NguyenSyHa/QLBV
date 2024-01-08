using System;
using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
using System.Collections.Generic;

namespace QLBV.BaoCao
{
    public partial class rep_BC_ChiDinhXetNghiem : DevExpress.XtraReports.UI.XtraReport
    {
        List<int> _lIdCLS = new List<int>();
        List<int?> _lMaDV = new List<int?>();
        public rep_BC_ChiDinhXetNghiem()
        {
            InitializeComponent();
        }
        public rep_BC_ChiDinhXetNghiem(List<int> _lIdCLS, List<int?> _lMaDV)
        {
            InitializeComponent();
            this._lMaDV = _lMaDV;
            this._lIdCLS = _lIdCLS;
        }
        QLBV_Database.QLBVEntities DataContect = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);       

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            var qcls = (from dvct in DataContect.DichVucts
                        join dv in DataContect.DichVus on dvct.MaDV equals dv.MaDV
                        join tnhomdv in DataContect.TieuNhomDVs on dv.IdTieuNhom equals tnhomdv.IdTieuNhom
                        where (tnhomdv.TenRG.Contains("XN miễn dịch"))
                        select new { tnhomdv.TenTN, dvct.MaDVct, dvct.TenDVct, dvct.TSBT, dvct.STT }).ToList();
            if (qcls.Count > 0)
            {
                #region cot c
                if (qcls.Where(p => p.STT == 1).Count() > 0 && qcls.Where(p => p.STT == 1).First().TenDVct != null)
                {
                    c1.Text = qcls.Where(p => p.STT == 1).First().TenDVct.ToString();
                }
                if (qcls.Where(p => p.STT == 2).Count() > 0 && qcls.Where(p => p.STT == 2).First().TenDVct != null)
                {
                    c2.Text = qcls.Where(p => p.STT == 2).First().TenDVct.ToString();
                }
                if (qcls.Where(p => p.STT == 3).Count() > 0 && qcls.Where(p => p.STT == 3).First().TenDVct != null)
                {
                    c3.Text = qcls.Where(p => p.STT == 3).First().TenDVct.ToString();
                }
                if (qcls.Where(p => p.STT == 4).Count() > 0 && qcls.Where(p => p.STT == 4).First().TenDVct != null)
                {
                    c4.Text = qcls.Where(p => p.STT == 4).First().TenDVct.ToString();
                }
                if (qcls.Where(p => p.STT == 5).Count() > 0 && qcls.Where(p => p.STT == 5).First().TenDVct != null)
                {
                    c5.Text = qcls.Where(p => p.STT == 5).First().TenDVct.ToString();
                }
                if (qcls.Where(p => p.STT == 6).Count() > 0 && qcls.Where(p => p.STT == 6).First().TenDVct != null)
                {
                    c6.Text = qcls.Where(p => p.STT == 6).First().TenDVct.ToString();
                }
                if (qcls.Where(p => p.STT == 7).Count() > 0 && qcls.Where(p => p.STT == 7).First().TenDVct != null)
                {
                    c7.Text = qcls.Where(p => p.STT == 7).First().TenDVct.ToString();
                }
                if (qcls.Where(p => p.STT == 8).Count() > 0 && qcls.Where(p => p.STT == 8).First().TenDVct != null)
                {
                    c8.Text = qcls.Where(p => p.STT == 8).First().TenDVct.ToString();
                }
                if (qcls.Where(p => p.STT == 9).Count() > 0 && qcls.Where(p => p.STT == 9).First().TenDVct != null)
                {
                    c9.Text = qcls.Where(p => p.STT == 9).First().TenDVct.ToString();
                }
                if (qcls.Where(p => p.STT == 10).Count() > 0 && qcls.Where(p => p.STT == 10).First().TenDVct != null)
                {
                    c10.Text = qcls.Where(p => p.STT == 10).First().TenDVct.ToString();
                }
                if (qcls.Where(p => p.STT == 11).Count() > 0 && qcls.Where(p => p.STT == 11).First().TenDVct != null)
                {
                    c11.Text = qcls.Where(p => p.STT == 11).First().TenDVct.ToString();
                }
                if (qcls.Where(p => p.STT == 12).Count() > 0 && qcls.Where(p => p.STT == 12).First().TenDVct != null)
                {
                    c12.Text = qcls.Where(p => p.STT == 12).First().TenDVct.ToString();
                }
                if (qcls.Where(p => p.STT == 13).Count() > 0 && qcls.Where(p => p.STT == 13).First().TenDVct != null)
                {
                    c13.Text = qcls.Where(p => p.STT == 13).First().TenDVct.ToString();
                }
                if (qcls.Where(p => p.STT == 14).Count() > 0 && qcls.Where(p => p.STT == 14).First().TenDVct != null)
                {
                    c14.Text = qcls.Where(p => p.STT == 14).First().TenDVct.ToString();
                }
                if (qcls.Where(p => p.STT == 15).Count() > 0 && qcls.Where(p => p.STT == 15).First().TenDVct != null)
                {
                    c15.Text = qcls.Where(p => p.STT == 15).First().TenDVct.ToString();
                }
                if (qcls.Where(p => p.STT == 16).Count() > 0 && qcls.Where(p => p.STT == 16).First().TenDVct != null)
                {
                    c16.Text = qcls.Where(p => p.STT == 16).First().TenDVct.ToString();
                }
                if (qcls.Where(p => p.STT == 17).Count() > 0 && qcls.Where(p => p.STT == 17).First().TenDVct != null)
                {
                    c17.Text = qcls.Where(p => p.STT == 17).First().TenDVct.ToString();
                }
                if (qcls.Where(p => p.STT == 18).Count() > 0 && qcls.Where(p => p.STT == 18).First().TenDVct != null)
                {
                    c18.Text = qcls.Where(p => p.STT == 18).First().TenDVct.ToString();
                }
                if (qcls.Where(p => p.STT == 19).Count() > 0 && qcls.Where(p => p.STT == 19).First().TenDVct != null)
                {
                    c19.Text = qcls.Where(p => p.STT == 19).First().TenDVct.ToString();
                }
                if (qcls.Where(p => p.STT == 20).Count() > 0 && qcls.Where(p => p.STT == 20).First().TenDVct != null)
                {
                    c20.Text = qcls.Where(p => p.STT == 20).First().TenDVct.ToString();
                }

                #endregion

                #region cot d
                if (qcls.Where(p => p.STT == 1).Count() > 0 && qcls.Where(p => p.STT == 1).First().TSBT != null)
                {
                    d1.Text = qcls.Where(p => p.STT == 1).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 2).Count() > 0 && qcls.Where(p => p.STT == 2).First().TSBT != null)
                {
                    d2.Text = qcls.Where(p => p.STT == 2).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 3).Count() > 0 && qcls.Where(p => p.STT == 3).First().TSBT != null)
                {
                    d3.Text = qcls.Where(p => p.STT == 3).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 4).Count() > 0 && qcls.Where(p => p.STT == 4).First().TSBT != null)
                {
                    d4.Text = qcls.Where(p => p.STT == 4).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 5).Count() > 0 && qcls.Where(p => p.STT == 5).First().TSBT != null)
                {
                    d5.Text = qcls.Where(p => p.STT == 5).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 6).Count() > 0 && qcls.Where(p => p.STT == 6).First().TSBT != null)
                {
                    d6.Text = qcls.Where(p => p.STT == 6).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 7).Count() > 0 && qcls.Where(p => p.STT == 7).First().TSBT != null)
                {
                    d7.Text = qcls.Where(p => p.STT == 7).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 8).Count() > 0 && qcls.Where(p => p.STT == 8).First().TSBT != null)
                {
                    d8.Text = qcls.Where(p => p.STT == 8).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 9).Count() > 0 && qcls.Where(p => p.STT == 9).First().TSBT != null)
                {
                    d9.Text = qcls.Where(p => p.STT == 9).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 10).Count() > 0 && qcls.Where(p => p.STT == 10).First().TSBT != null)
                {
                    d10.Text = qcls.Where(p => p.STT == 10).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 11).Count() > 0 && qcls.Where(p => p.STT == 11).First().TSBT != null)
                {
                    d11.Text = qcls.Where(p => p.STT == 11).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 12).Count() > 0 && qcls.Where(p => p.STT == 12).First().TSBT != null)
                {
                    d12.Text = qcls.Where(p => p.STT == 12).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 13).Count() > 0 && qcls.Where(p => p.STT == 13).First().TSBT != null)
                {
                    d13.Text = qcls.Where(p => p.STT == 13).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 14).Count() > 0 && qcls.Where(p => p.STT == 14).First().TSBT != null)
                {
                    d14.Text = qcls.Where(p => p.STT == 14).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 15).Count() > 0 && qcls.Where(p => p.STT == 15).First().TSBT != null)
                {
                    d15.Text = qcls.Where(p => p.STT == 15).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 16).Count() > 0 && qcls.Where(p => p.STT == 16).First().TSBT != null)
                {
                    d16.Text = qcls.Where(p => p.STT == 16).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 17).Count() > 0 && qcls.Where(p => p.STT == 17).First().TSBT != null)
                {
                    d17.Text = qcls.Where(p => p.STT == 17).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 18).Count() > 0 && qcls.Where(p => p.STT == 18).First().TSBT != null)
                {
                    d18.Text = qcls.Where(p => p.STT == 18).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 19).Count() > 0 && qcls.Where(p => p.STT == 19).First().TSBT != null)
                {
                    d19.Text = qcls.Where(p => p.STT == 19).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 20).Count() > 0 && qcls.Where(p => p.STT == 20).First().TSBT != null)
                {
                    d20.Text = qcls.Where(p => p.STT == 20).First().TSBT.ToString();
                }

                #endregion

                int mabenhnhan = Convert.ToInt32(MaBenhNhan.Value);
                int IDCls = Convert.ToInt32(Idcls.Value);
                var qhh = (from bn in DataContect.BenhNhans.Where(p => p.MaBNhan == mabenhnhan)
                           join cls in DataContect.CLS.Where(p => DungChung.Bien.MaBV == "30372" ? _lIdCLS.Contains(p.IdCLS) : p.IdCLS == IDCls) on bn.MaBNhan equals cls.MaBNhan
                           join cd in DataContect.ChiDinhs on cls.IdCLS equals cd.IdCLS
                           join dv in DataContect.DichVus on cd.MaDV equals dv.MaDV
                           join tndv in DataContect.TieuNhomDVs.Where(p => p.TenRG == "XN miễn dịch") on dv.IdTieuNhom equals tndv.IdTieuNhom
                           join clsct in DataContect.CLScts on cd.IDCD equals clsct.IDCD
                           join dvct in DataContect.DichVucts on clsct.MaDVct equals dvct.MaDVct
                           join KP in DataContect.KPhongs on cls.MaKP equals KP.MaKP
                           select new
                           {
                               clsct.MaDVct,
                               clsct.KetQua,
                               dvct.STT,
                               dvct.TSBTnu,
                               dvct.TSnDen,
                               dvct.TSnuTu,
                               dvct.TSnuDen,
                               cd.MaDV,
                               cls.Status,
                           }).ToList();

                if (qhh.Count > 0)
                {
                    #region cot a
                    if (qhh.Where(p => p.STT == 1 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 1).First().MaDVct != null)
                    {
                        a1.Text = "X";
                    }

                    if (qhh.Where(p => p.STT == 2 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 2).First().MaDVct != null)
                    {
                        a2.Text = "X";
                    }

                    if (qhh.Where(p => p.STT == 3 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 3).First().MaDVct != null)
                    {
                        a3.Text = "X";
                    }
                    if (qhh.Where(p => p.STT == 4 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 4).First().MaDVct != null)
                    {
                        a4.Text = "X";
                    }

                    if (qhh.Where(p => p.STT == 5 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 5).First().MaDVct != null)
                    {
                        a5.Text = "X";
                    }
                    if (qhh.Where(p => p.STT == 6 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 6).First().MaDVct != null)
                    {
                        a6.Text = "X";
                    }

                    if (qhh.Where(p => p.STT == 7 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 7).First().MaDVct != null)
                    {
                        a7.Text = "X";
                    }
                    if (qhh.Where(p => p.STT == 8 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 8).First().MaDVct != null)
                    {
                        a8.Text = "X";
                    }

                    if (qhh.Where(p => p.STT == 9 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 9).First().MaDVct != null)
                    {
                        a9.Text = "X";
                    }
                    if (qhh.Where(p => p.STT == 10 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 10).First().MaDVct != null)
                    {
                        a10.Text = "X";
                    }

                    if (qhh.Where(p => p.STT == 11 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 11).First().MaDVct != null)
                    {
                        a11.Text = "X";
                    }
                    if (qhh.Where(p => p.STT == 12 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 12).First().MaDVct != null)
                    {
                        a12.Text = "X";
                    }

                    if (qhh.Where(p => p.STT == 13 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 13).First().MaDVct != null)
                    {
                        a13.Text = "X";
                    }
                    if (qhh.Where(p => p.STT == 14 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 14).First().MaDVct != null)
                    {
                        a14.Text = "X";
                    }

                    if (qhh.Where(p => p.STT == 15 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 15).First().MaDVct != null)
                    {
                        a15.Text = "X";
                    }
                    if (qhh.Where(p => p.STT == 16 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 16).First().MaDVct != null)
                    {
                        a16.Text = "X";
                    }

                    if (qhh.Where(p => p.STT == 17 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 17).First().MaDVct != null)
                    {
                        a17.Text = "X";
                    }
                    if (qhh.Where(p => p.STT == 18 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 18).First().MaDVct != null)
                    {
                        a18.Text = "X";
                    }

                    if (qhh.Where(p => p.STT == 19 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 19).First().MaDVct != null)
                    {
                        a19.Text = "X";
                    }
                    if (qhh.Where(p => p.STT == 20 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 20).First().MaDVct != null)
                    {
                        a20.Text = "X";
                    }


                    #endregion
                    #region Cột D

                    if (qhh.Where(p => p.STT == 1 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 1).First().KetQua != null)
                    {
                        e1.Text = qhh.Where(p => p.STT == 1 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).First().KetQua.ToString();

                    }
                    if (qhh.Where(p => p.STT == 2 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 2).First().KetQua != null)
                    {
                        e2.Text = qhh.Where(p => p.STT == 2 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).First().KetQua.ToString();

                    }
                    if (qhh.Where(p => p.STT == 3 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 3).First().KetQua != null)
                    {
                        e3.Text = qhh.Where(p => p.STT == 3 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).First().KetQua.ToString();

                    }
                    if (qhh.Where(p => p.STT == 4 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 4).First().KetQua != null)
                    {
                        e4.Text = qhh.Where(p => p.STT == 4 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).First().KetQua.ToString();

                    }
                    if (qhh.Where(p => p.STT == 5 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 5).First().KetQua != null)
                    {
                        e5.Text = qhh.Where(p => p.STT == 5 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).First().KetQua.ToString();

                    }
                    if (qhh.Where(p => p.STT == 6 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 6).First().KetQua != null)
                    {
                        e6.Text = qhh.Where(p => p.STT == 6 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).First().KetQua.ToString();

                    }
                    if (qhh.Where(p => p.STT == 7 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 7).First().KetQua != null)
                    {
                        e7.Text = qhh.Where(p => p.STT == 7 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).First().KetQua.ToString();

                    }
                    if (qhh.Where(p => p.STT == 8 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 8).First().KetQua != null)
                    {
                        e8.Text = qhh.Where(p => p.STT == 8 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).First().KetQua.ToString();

                    }
                    if (qhh.Where(p => p.STT == 9 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 9).First().KetQua != null)
                    {
                        e9.Text = qhh.Where(p => p.STT == 9 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).First().KetQua.ToString();

                    }
                    if (qhh.Where(p => p.STT == 10 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 10).First().KetQua != null)
                    {
                        e10.Text = qhh.Where(p => p.STT == 10 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).First().KetQua.ToString();

                    }
                    if (qhh.Where(p => p.STT == 11 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 11).First().KetQua != null)
                    {
                        e11.Text = qhh.Where(p => p.STT == 11 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).First().KetQua.ToString();

                    }
                    if (qhh.Where(p => p.STT == 12 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 12).First().KetQua != null)
                    {
                        e12.Text = qhh.Where(p => p.STT == 12 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).First().KetQua.ToString();

                    }
                    if (qhh.Where(p => p.STT == 13 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 13).First().KetQua != null)
                    {
                        e13.Text = qhh.Where(p => p.STT == 13 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).First().KetQua.ToString();

                    }
                    if (qhh.Where(p => p.STT == 14 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 14).First().KetQua != null)
                    {
                        e14.Text = qhh.Where(p => p.STT == 14 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).First().KetQua.ToString();

                    }
                    if (qhh.Where(p => p.STT == 15 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 15).First().KetQua != null)
                    {
                        e15.Text = qhh.Where(p => p.STT == 15 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).First().KetQua.ToString();

                    }
                    if (qhh.Where(p => p.STT == 16 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 16).First().KetQua != null)
                    {
                        e16.Text = qhh.Where(p => p.STT == 16 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).First().KetQua.ToString();

                    }
                    if (qhh.Where(p => p.STT == 17 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 17).First().KetQua != null)
                    {
                        e17.Text = qhh.Where(p => p.STT == 17 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).First().KetQua.ToString();

                    }
                    if (qhh.Where(p => p.STT == 18 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 18).First().KetQua != null)
                    {
                        e18.Text = qhh.Where(p => p.STT == 18 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).First().KetQua.ToString();

                    }
                    if (qhh.Where(p => p.STT == 19 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 19).First().KetQua != null)
                    {
                        e19.Text = qhh.Where(p => p.STT == 19 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).First().KetQua.ToString();

                    }
                    if (qhh.Where(p => p.STT == 20 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 20).First().KetQua != null)
                    {
                        e20.Text = qhh.Where(p => p.STT == 20 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).First().KetQua.ToString();

                    }


                    #endregion
                }

                #region check kq null
                if (qhh.Count > 0)
                {
                    if (qhh.FirstOrDefault(p => p.STT == 1) != null && qhh.Where(p => p.STT == 1).FirstOrDefault().Status == 0)
                    {
                        e1.Text = "";
                    }
                    if (qhh.FirstOrDefault(p => p.STT == 2) != null && qhh.Where(p => p.STT == 2).FirstOrDefault().Status == 0)
                    {
                        e2.Text = "";
                    }
                    if (qhh.FirstOrDefault(p => p.STT == 3) != null && qhh.Where(p => p.STT == 3).FirstOrDefault().Status == 0)
                    {
                        e3.Text = "";
                    }
                    if (qhh.FirstOrDefault(p => p.STT == 4) != null && qhh.Where(p => p.STT == 4).FirstOrDefault().Status == 0)
                    {
                        e4.Text = "";
                    }
                    if (qhh.FirstOrDefault(p => p.STT == 5) != null && qhh.Where(p => p.STT == 5).FirstOrDefault().Status == 0)
                    {
                        e5.Text = "";
                    }
                    if (qhh.FirstOrDefault(p => p.STT == 6) != null && qhh.Where(p => p.STT == 6).FirstOrDefault().Status == 0)
                    {
                        e6.Text = "";
                    }
                    if (qhh.FirstOrDefault(p => p.STT == 7) != null && qhh.Where(p => p.STT == 7).FirstOrDefault().Status == 0)
                    {
                        e7.Text = "";
                    }
                    if (qhh.FirstOrDefault(p => p.STT == 8) != null && qhh.Where(p => p.STT == 8).FirstOrDefault().Status == 0)
                    {
                        e8.Text = "";
                    }
                    if (qhh.FirstOrDefault(p => p.STT == 9) != null && qhh.Where(p => p.STT == 9).FirstOrDefault().Status == 0)
                    {
                        e9.Text = "";
                    }
                    if (qhh.FirstOrDefault(p => p.STT == 10) != null && qhh.Where(p => p.STT == 10).FirstOrDefault().Status == 0)
                    {
                        e10.Text = "";
                    }
                    if (qhh.FirstOrDefault(p => p.STT == 11) != null && qhh.Where(p => p.STT == 11).FirstOrDefault().Status == 0)
                    {
                        e11.Text = "";
                    }
                    if (qhh.FirstOrDefault(p => p.STT == 12) != null && qhh.Where(p => p.STT == 12).FirstOrDefault().Status == 0)
                    {
                        e12.Text = "";
                    }
                    if (qhh.FirstOrDefault(p => p.STT == 13) != null && qhh.Where(p => p.STT == 13).FirstOrDefault().Status == 0)
                    {
                        e13.Text = "";
                    }
                    if (qhh.FirstOrDefault(p => p.STT == 14) != null && qhh.Where(p => p.STT == 14).FirstOrDefault().Status == 0)
                    {
                        e14.Text = "";
                    }
                    if (qhh.FirstOrDefault(p => p.STT == 15) != null && qhh.Where(p => p.STT == 15).FirstOrDefault().Status == 0)
                    {
                        e15.Text = "";
                    }
                    if (qhh.FirstOrDefault(p => p.STT == 16) != null && qhh.Where(p => p.STT == 16).FirstOrDefault().Status == 0)
                    {
                        e16.Text = "";
                    }
                    if (qhh.FirstOrDefault(p => p.STT == 17) != null && qhh.Where(p => p.STT == 17).FirstOrDefault().Status == 0)
                    {
                        e17.Text = "";
                    }
                    if (qhh.FirstOrDefault(p => p.STT == 18) != null && qhh.Where(p => p.STT == 18).FirstOrDefault().Status == 0)
                    {
                        e18.Text = "";
                    }
                    if (qhh.FirstOrDefault(p => p.STT == 19) != null && qhh.Where(p => p.STT == 19).FirstOrDefault().Status == 0)
                    {
                        e19.Text = "";
                    }
                    if (qhh.FirstOrDefault(p => p.STT == 20) != null && qhh.Where(p => p.STT == 20).FirstOrDefault().Status == 0)
                    {
                        e20.Text = "";
                    }
                }
                #endregion

            }
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "24272")
            {
                SubBand1.Visible = true;
                SubBand2.Visible = false;
            }
            else
            {
                SubBand2.Visible = true;
                SubBand1.Visible = false;
            }

            if (DungChung.Bien.MaBV == "30009" || DungChung.Bien.MaBV == "30303")
            {
                xrTableCell22.Text = "TRƯỞNG KHOA XÉT NGHIỆM ";
            }

            if (DungChung.Bien.MaBV == "24272")
            {
                xrPictureBox1.Visible = false;
                xrPictureBox2.Visible = true;
                xrPictureBox2.Image = DungChung.Ham.GetLogo();
            }

            if (DungChung.Bien.MaBV == "30372")
            {
                xrPictureBox1.Visible = false;
                xrPictureBox3.Visible = true;
            }
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            if(DungChung.Bien.MaBV == "24009")
            {
                xrTableCell22.Text = "TRƯỞNG KHOA XÉT NGHIỆM";
                MaCanBoTH.Text = "BS: Hoàng Văn Luận";
            }
        }
        
        

    }
}
