using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class Rep_PhieuXNHuyetHoc_VY : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_PhieuXNHuyetHoc_VY()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities DataContect = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            var qcls = (from dvct in DataContect.DichVucts
                        join dv in DataContect.DichVus on dvct.MaDV equals dv.MaDV
                        join tnhomdv in DataContect.TieuNhomDVs on dv.IdTieuNhom equals tnhomdv.IdTieuNhom
                        where (tnhomdv.TenRG.Contains("Huyết học"))
                        select new { dvct.MaDVct, dvct.TenDVct, dvct.TSBT, dvct.STT }).ToList();
            if (qcls.Count > 0)
            {
                if (qcls.Where(p => p.STT == 1).Count() > 0 && qcls.Where(p => p.STT == 1).First().TenDVct != null && qcls.Where(p => p.STT == 1).First().TSBT == null)
                {
                    col1.Text = qcls.Where(p => p.STT == 1).First().TenDVct.ToString();
                }
                else if (qcls.Where(p => p.STT == 1).Count() > 0 && qcls.Where(p => p.STT == 1).First().TenDVct != null && qcls.Where(p => p.STT == 1).First().TSBT != null)
                {
                    col1.Text = qcls.Where(p => p.STT == 1).First().TenDVct.ToString() + qcls.Where(p => p.STT == 1).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 2).Count() > 0 && qcls.Where(p => p.STT == 2).First().TenDVct != null && qcls.Where(p => p.STT == 2).First().TSBT == null)
                {
                    col2.Text = qcls.Where(p => p.STT == 2).First().TenDVct.ToString();
                }
                else if (qcls.Where(p => p.STT == 2).Count() > 0 && qcls.Where(p => p.STT == 2).First().TenDVct != null && qcls.Where(p => p.STT == 2).First().TSBT != null)
                {
                    col2.Text = qcls.Where(p => p.STT == 2).First().TenDVct.ToString() + qcls.Where(p => p.STT == 2).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 3).Count() > 0 && qcls.Where(p => p.STT == 3).First().TenDVct != null && qcls.Where(p => p.STT == 3).First().TSBT == null)
                {
                    col3.Text = qcls.Where(p => p.STT == 3).First().TenDVct.ToString();
                }
                else if (qcls.Where(p => p.STT == 3).Count() > 0 && qcls.Where(p => p.STT == 3).First().TenDVct != null && qcls.Where(p => p.STT == 3).First().TSBT != null)
                {
                    col3.Text = qcls.Where(p => p.STT == 3).First().TenDVct.ToString() + qcls.Where(p => p.STT == 3).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 4).Count() > 0 && qcls.Where(p => p.STT == 4).First().TenDVct != null && qcls.Where(p => p.STT == 4).First().TSBT == null)
                {
                    col41.Text = qcls.Where(p => p.STT == 4).First().TenDVct.ToString();
                }
                else if (qcls.Where(p => p.STT == 4).Count() > 0 && qcls.Where(p => p.STT == 4).First().TenDVct != null && qcls.Where(p => p.STT == 4).First().TSBT != null)
                {
                    col41.Text = qcls.Where(p => p.STT == 4).First().TenDVct.ToString() + qcls.Where(p => p.STT == 4).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 5).Count() > 0 && qcls.Where(p => p.STT == 5).First().TenDVct != null && qcls.Where(p => p.STT == 5).First().TSBT == null)
                {
                    col42.Text = qcls.Where(p => p.STT == 5).First().TenDVct.ToString();
                }
                else if (qcls.Where(p => p.STT == 5).Count() > 0 && qcls.Where(p => p.STT == 5).First().TenDVct != null && qcls.Where(p => p.STT == 5).First().TSBT != null)
                {
                    col42.Text = qcls.Where(p => p.STT == 5).First().TenDVct.ToString() + qcls.Where(p => p.STT == 5).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 6).Count() > 0 && qcls.Where(p => p.STT == 6).First().TenDVct != null && qcls.Where(p => p.STT == 6).First().TSBT == null)
                {
                    col5.Text = qcls.Where(p => p.STT == 6).First().TenDVct.ToString();
                }
                else if (qcls.Where(p => p.STT == 6).Count() > 0 && qcls.Where(p => p.STT == 6).First().TenDVct != null && qcls.Where(p => p.STT == 6).First().TSBT != null)
                {
                    col5.Text = qcls.Where(p => p.STT == 6).First().TenDVct.ToString() + qcls.Where(p => p.STT == 6).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 7).Count() > 0 && qcls.Where(p => p.STT == 7).First().TenDVct != null && qcls.Where(p => p.STT == 7).First().TSBT == null)
                {
                    col6.Text = qcls.Where(p => p.STT == 7).First().TenDVct.ToString();
                }
                else if (qcls.Where(p => p.STT == 7).Count() > 0 && qcls.Where(p => p.STT == 7).First().TenDVct != null && qcls.Where(p => p.STT == 7).First().TSBT != null)
                {
                    col6.Text = qcls.Where(p => p.STT == 7).First().TenDVct.ToString() + qcls.Where(p => p.STT == 7).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 8).Count() > 0 && qcls.Where(p => p.STT == 8).First().TenDVct != null && qcls.Where(p => p.STT == 8).First().TSBT == null)
                {
                    col7.Text = qcls.Where(p => p.STT == 8).First().TenDVct.ToString();
                }
                else if (qcls.Where(p => p.STT == 8).Count() > 0 && qcls.Where(p => p.STT == 8).First().TenDVct != null && qcls.Where(p => p.STT == 8).First().TSBT != null)
                {
                    col7.Text = qcls.Where(p => p.STT == 8).First().TenDVct.ToString() + qcls.Where(p => p.STT == 8).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 9).Count() > 0 && qcls.Where(p => p.STT == 9).First().TenDVct != null && qcls.Where(p => p.STT == 9).First().TSBT == null)
                {
                    col8.Text = qcls.Where(p => p.STT == 9).First().TenDVct.ToString();
                }
                else if (qcls.Where(p => p.STT == 9).Count() > 0 && qcls.Where(p => p.STT == 9).First().TenDVct != null && qcls.Where(p => p.STT == 9).First().TSBT != null)
                {
                    col8.Text = qcls.Where(p => p.STT == 9).First().TenDVct.ToString() + qcls.Where(p => p.STT == 9).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 10).Count() > 0 && qcls.Where(p => p.STT == 10).First().TenDVct != null && qcls.Where(p => p.STT == 10).First().TSBT == null)
                {
                    col9.Text = qcls.Where(p => p.STT == 10).First().TenDVct.ToString();
                }
                else if (qcls.Where(p => p.STT == 10).Count() > 0 && qcls.Where(p => p.STT == 10).First().TenDVct != null && qcls.Where(p => p.STT == 10).First().TSBT != null)
                {
                    col9.Text = qcls.Where(p => p.STT == 10).First().TenDVct.ToString() + qcls.Where(p => p.STT == 10).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 11).Count() > 0 && qcls.Where(p => p.STT == 11).First().TenDVct != null && qcls.Where(p => p.STT == 11).First().TSBT == null)
                {
                    col10.Text = qcls.Where(p => p.STT == 11).First().TenDVct.ToString();
                }
                else if (qcls.Where(p => p.STT == 11).Count() > 0 && qcls.Where(p => p.STT == 11).First().TenDVct != null && qcls.Where(p => p.STT == 11).First().TSBT != null)
                {
                    col10.Text = qcls.Where(p => p.STT == 11).First().TenDVct.ToString() + qcls.Where(p => p.STT == 11).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 12).Count() > 0 && qcls.Where(p => p.STT == 12).First().TenDVct != null && qcls.Where(p => p.STT == 12).First().TSBT == null)
                {
                    col11.Text = qcls.Where(p => p.STT == 12).First().TenDVct.ToString();
                }
                else if (qcls.Where(p => p.STT == 12).Count() > 0 && qcls.Where(p => p.STT == 12).First().TenDVct != null && qcls.Where(p => p.STT == 12).First().TSBT != null)
                {
                    col11.Text = qcls.Where(p => p.STT == 12).First().TenDVct.ToString() + qcls.Where(p => p.STT == 12).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 13).Count() > 0 && qcls.Where(p => p.STT == 13).First().TenDVct != null && qcls.Where(p => p.STT == 13).First().TSBT == null)
                {
                    col111.Text = qcls.Where(p => p.STT == 13).First().TenDVct.ToString();
                }
                else if (qcls.Where(p => p.STT == 13).Count() > 0 && qcls.Where(p => p.STT == 13).First().TenDVct != null && qcls.Where(p => p.STT == 13).First().TSBT != null)
                {
                    col111.Text = qcls.Where(p => p.STT == 13).First().TenDVct.ToString() + qcls.Where(p => p.STT == 13).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 14).Count() > 0 && qcls.Where(p => p.STT == 14).First().TenDVct != null && qcls.Where(p => p.STT == 14).First().TSBT == null)
                {
                    col112.Text = qcls.Where(p => p.STT == 14).First().TenDVct.ToString();
                }
                else if (qcls.Where(p => p.STT == 14).Count() > 0 && qcls.Where(p => p.STT == 14).First().TenDVct != null && qcls.Where(p => p.STT == 14).First().TSBT != null)
                {
                    col112.Text = qcls.Where(p => p.STT == 14).First().TenDVct.ToString() + qcls.Where(p => p.STT == 14).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 15).Count() > 0 && qcls.Where(p => p.STT == 15).First().TenDVct != null && qcls.Where(p => p.STT == 15).First().TSBT == null)
                {
                    col113.Text = qcls.Where(p => p.STT == 15).First().TenDVct.ToString();
                }
                else if (qcls.Where(p => p.STT == 15).Count() > 0 && qcls.Where(p => p.STT == 15).First().TenDVct != null && qcls.Where(p => p.STT == 15).First().TSBT != null)
                {
                    col113.Text = qcls.Where(p => p.STT == 15).First().TenDVct.ToString() + qcls.Where(p => p.STT == 15).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 16).Count() > 0 && qcls.Where(p => p.STT == 16).First().TenDVct != null && qcls.Where(p => p.STT == 16).First().TSBT == null)
                {
                    col121.Text = qcls.Where(p => p.STT == 16).First().TenDVct.ToString();
                }
                else if (qcls.Where(p => p.STT == 16).Count() > 0 && qcls.Where(p => p.STT == 16).First().TenDVct != null && qcls.Where(p => p.STT == 16).First().TSBT != null)
                {
                    col121.Text = qcls.Where(p => p.STT == 16).First().TenDVct.ToString() + qcls.Where(p => p.STT == 16).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 17).Count() > 0 && qcls.Where(p => p.STT == 17).First().TenDVct != null && qcls.Where(p => p.STT == 17).First().TSBT == null)
                {
                    col131.Text = qcls.Where(p => p.STT == 17).First().TenDVct.ToString();
                }
                else if (qcls.Where(p => p.STT == 17).Count() > 0 && qcls.Where(p => p.STT == 17).First().TenDVct != null && qcls.Where(p => p.STT == 17).First().TSBT != null)
                {
                    col131.Text = qcls.Where(p => p.STT == 17).First().TenDVct.ToString() + qcls.Where(p => p.STT == 17).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 18).Count() > 0 && qcls.Where(p => p.STT == 18).First().TenDVct != null && qcls.Where(p => p.STT == 18).First().TSBT == null)
                {
                    col132.Text = qcls.Where(p => p.STT == 18).First().TenDVct.ToString();
                }
                else if (qcls.Where(p => p.STT == 18).Count() > 0 && qcls.Where(p => p.STT == 18).First().TenDVct != null && qcls.Where(p => p.STT == 18).First().TSBT != null)
                {
                    col132.Text = qcls.Where(p => p.STT == 18).First().TenDVct.ToString() + qcls.Where(p => p.STT == 18).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 19).Count() > 0 && qcls.Where(p => p.STT == 19).First().TenDVct != null && qcls.Where(p => p.STT == 19).First().TSBT == null)
                {
                    col141.Text = qcls.Where(p => p.STT == 19).First().TenDVct.ToString();
                }
                else if (qcls.Where(p => p.STT == 19).Count() > 0 && qcls.Where(p => p.STT == 19).First().TenDVct != null && qcls.Where(p => p.STT == 19).First().TSBT != null)
                {
                    col141.Text = qcls.Where(p => p.STT == 19).First().TenDVct.ToString() + qcls.Where(p => p.STT == 19).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 20).Count() > 0 && qcls.Where(p => p.STT == 20).First().TenDVct != null && qcls.Where(p => p.STT == 20).First().TSBT == null)
                {
                    col17.Text = qcls.Where(p => p.STT == 20).First().TenDVct.ToString();
                }
                else if (qcls.Where(p => p.STT == 20).Count() > 0 && qcls.Where(p => p.STT == 20).First().TenDVct != null && qcls.Where(p => p.STT == 20).First().TSBT != null)
                {
                    col17.Text = qcls.Where(p => p.STT == 20).First().TenDVct.ToString() + qcls.Where(p => p.STT == 20).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 21).Count() > 0 && qcls.Where(p => p.STT == 21).First().TenDVct != null && qcls.Where(p => p.STT == 21).First().TSBT == null)
                {
                    col18.Text = qcls.Where(p => p.STT == 21).First().TenDVct.ToString();
                }
                else if (qcls.Where(p => p.STT == 21).Count() > 0 && qcls.Where(p => p.STT == 21).First().TenDVct != null && qcls.Where(p => p.STT == 21).First().TSBT != null)
                {
                    col18.Text = qcls.Where(p => p.STT == 21).First().TenDVct.ToString() + qcls.Where(p => p.STT == 21).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 22).Count() > 0 && qcls.Where(p => p.STT == 22).First().TenDVct != null && qcls.Where(p => p.STT == 22).First().TSBT == null)
                {
                    col19.Text = qcls.Where(p => p.STT == 22).First().TenDVct.ToString();
                }
                else if (qcls.Where(p => p.STT == 22).Count() > 0 && qcls.Where(p => p.STT == 22).First().TenDVct != null && qcls.Where(p => p.STT == 22).First().TSBT != null)
                {
                    col19.Text = qcls.Where(p => p.STT == 22).First().TenDVct.ToString() + qcls.Where(p => p.STT == 22).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 23).Count() > 0 && qcls.Where(p => p.STT == 23).First().TenDVct != null && qcls.Where(p => p.STT == 23).First().TSBT == null)
                {
                    col20.Text = qcls.Where(p => p.STT == 21).First().TenDVct.ToString();
                }
                else if (qcls.Where(p => p.STT == 23).Count() > 0 && qcls.Where(p => p.STT == 23).First().TenDVct != null && qcls.Where(p => p.STT == 23).First().TSBT != null)
                {
                    col20.Text = qcls.Where(p => p.STT == 23).First().TenDVct.ToString() + qcls.Where(p => p.STT == 23).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 24).Count() > 0 && qcls.Where(p => p.STT == 24).First().TenDVct != null && qcls.Where(p => p.STT == 24).First().TSBT == null)
                {
                    col21.Text = qcls.Where(p => p.STT == 22).First().TenDVct.ToString();
                }
                else if (qcls.Where(p => p.STT == 24).Count() > 0 && qcls.Where(p => p.STT == 24).First().TenDVct != null && qcls.Where(p => p.STT == 24).First().TSBT != null)
                {
                    col21.Text = qcls.Where(p => p.STT == 24).First().TenDVct.ToString() + qcls.Where(p => p.STT == 24).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 25).Count() > 0 && qcls.Where(p => p.STT == 25).First().TenDVct != null && qcls.Where(p => p.STT == 25).First().TSBT == null)
                {
                    col22.Text = qcls.Where(p => p.STT == 25).First().TenDVct.ToString();
                }
                else if (qcls.Where(p => p.STT == 25).Count() > 0 && qcls.Where(p => p.STT == 25).First().TenDVct != null && qcls.Where(p => p.STT == 25).First().TSBT != null)
                {
                    col22.Text = qcls.Where(p => p.STT == 25).First().TenDVct.ToString() + qcls.Where(p => p.STT == 25).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 26).Count() > 0 && qcls.Where(p => p.STT == 26).First().TenDVct != null && qcls.Where(p => p.STT == 26).First().TSBT == null)
                {
                    col23.Text = qcls.Where(p => p.STT == 26).First().TenDVct.ToString();
                }
                else if (qcls.Where(p => p.STT == 26).Count() > 0 && qcls.Where(p => p.STT == 26).First().TenDVct != null && qcls.Where(p => p.STT == 26).First().TSBT != null)
                {
                    col23.Text = qcls.Where(p => p.STT == 26).First().TenDVct.ToString() + qcls.Where(p => p.STT == 26).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 27).Count() > 0 && qcls.Where(p => p.STT == 27).First().TenDVct != null && qcls.Where(p => p.STT == 27).First().TSBT == null)
                {
                    col231.Text = qcls.Where(p => p.STT == 27).First().TenDVct.ToString();
                }
                else if (qcls.Where(p => p.STT == 27).Count() > 0 && qcls.Where(p => p.STT == 27).First().TenDVct != null && qcls.Where(p => p.STT == 27).First().TSBT != null)
                {
                    col231.Text = qcls.Where(p => p.STT == 27).First().TenDVct.ToString() + qcls.Where(p => p.STT == 27).First().TSBT.ToString();
                }
            }
            ///////////////////////////////////////
            int sophieu = int.Parse(SoPhieu.Value.ToString());

            var qhh = (from cls in DataContect.CLS.Where(p => p.IdCLS == sophieu)
                       join chidinh in DataContect.ChiDinhs on cls.IdCLS equals chidinh.IdCLS
                       join dichvu in DataContect.DichVus on chidinh.MaDV equals dichvu.MaDV
                       join tn in DataContect.TieuNhomDVs.Where(p => p.TenRG.Contains("XN huyết học")) on dichvu.IdTieuNhom equals tn.IdTieuNhom
                       join clsct in DataContect.CLScts on chidinh.IDCD equals clsct.IDCD
                       join dvct in DataContect.DichVucts on clsct.MaDVct equals dvct.MaDVct
                       select new { clsct.MaDVct, clsct.KetQua, dvct.STT }).ToList();
            if (qhh.Count > 0)
            {
                if (qhh.Where(p => p.STT == 1).Count() > 0 && qhh.Where(p => p.STT == 1).First().MaDVct != null)
                {
                    labT1.Text = "X";
                }
                if (qhh.Where(p => p.STT == 1).Count() > 0 && qhh.Where(p => p.STT == 1).First().KetQua != null)
                {
                    colKQ1.Text = qhh.Where(p => p.STT == 1).First().KetQua.ToString();
                }
                if (qhh.Where(p => p.STT == 2).Count() > 0 && qhh.Where(p => p.STT == 2).First().MaDVct != null)
                {
                    labT2.Text = "X";
                }
                if (qhh.Where(p => p.STT == 2).Count() > 0 && qhh.Where(p => p.STT == 2).First().KetQua != null)
                {
                    colKQ2.Text = qhh.Where(p => p.STT == 2).First().KetQua.ToString();
                }
                if (qhh.Where(p => p.STT == 3).Count() > 0 && qhh.Where(p => p.STT == 3).First().MaDVct != null)
                {
                    labT3.Text = "X";
                }
                if (qhh.Where(p => p.STT == 3).Count() > 0 && qhh.Where(p => p.STT == 3).First().KetQua != null)
                {
                    colKQ3.Text = qhh.Where(p => p.STT == 3).First().KetQua.ToString();
                }
                if (qhh.Where(p => p.STT == 4).Count() > 0 && qhh.Where(p => p.STT == 4).First().MaDVct != null)
                {
                    labT41.Text = "X";
                }
                if (qhh.Where(p => p.STT == 4).Count() > 0 && qhh.Where(p => p.STT == 4).First().KetQua != null)
                {
                    colKQ41.Text = qhh.Where(p => p.STT == 4).First().KetQua.ToString();
                }
                if (qhh.Where(p => p.STT == 5).Count() > 0 && qhh.Where(p => p.STT == 5).First().MaDVct != null)
                {
                    labT42.Text = "X";
                }
                if (qhh.Where(p => p.STT == 5).Count() > 0 && qhh.Where(p => p.STT == 5).First().KetQua != null)
                {
                    colKQ42.Text = qhh.Where(p => p.STT == 5).First().KetQua.ToString();
                }
                if (qhh.Where(p => p.STT == 6).Count() > 0 && qhh.Where(p => p.STT == 6).First().MaDVct != null)
                {
                    labT5.Text = "X";
                }
                if (qhh.Where(p => p.STT == 6).Count() > 0 && qhh.Where(p => p.STT == 6).First().KetQua != null)
                {
                    colKQ5.Text = qhh.Where(p => p.STT == 6).First().KetQua.ToString();
                }
                if (qhh.Where(p => p.STT == 7).Count() > 0 && qhh.Where(p => p.STT == 7).First().MaDVct != null)
                {
                    labT6.Text = "X";
                }

                if (qhh.Where(p => p.STT == 7).Count() > 0 && qhh.Where(p => p.STT == 7).First().KetQua != null)
                {
                    colKQ6.Text = qhh.Where(p => p.STT == 7).First().KetQua.ToString();
                }
                if (qhh.Where(p => p.STT == 8).Count() > 0 && qhh.Where(p => p.STT == 8).First().MaDVct != null)
                {
                    labT7.Text = "X";
                }
                if (qhh.Where(p => p.STT == 8).Count() > 0 && qhh.Where(p => p.STT == 8).First().KetQua != null)
                {
                    colKQ7.Text = qhh.Where(p => p.STT == 8).First().KetQua.ToString();
                }
                if (qhh.Where(p => p.STT == 9).Count() > 0 && qhh.Where(p => p.STT == 9).First().MaDVct != null)
                {
                    labT8.Text = "X";
                }
                if (qhh.Where(p => p.STT == 9).Count() > 0 && qhh.Where(p => p.STT == 9).First().KetQua != null)
                {
                    colKQ8.Text = qhh.Where(p => p.STT == 9).First().KetQua.ToString();
                }
                if (qhh.Where(p => p.STT == 10).Count() > 0 && qhh.Where(p => p.STT == 10).First().MaDVct != null)
                {
                    labT9.Text = "X";
                }
                if (qhh.Where(p => p.STT == 10).Count() > 0 && qhh.Where(p => p.STT == 10).First().KetQua != null)
                {
                    colKQ9.Text = qhh.Where(p => p.STT == 10).First().KetQua.ToString();
                }
                if (qhh.Where(p => p.STT == 11).Count() > 0 && qhh.Where(p => p.STT == 11).First().MaDVct != null)
                {
                    labT10.Text = "X";
                }
                if (qhh.Where(p => p.STT == 11).Count() > 0 && qhh.Where(p => p.STT == 11).First().KetQua != null)
                {
                    colKQ10.Text = qhh.Where(p => p.STT == 11).First().KetQua.ToString();
                }
                if (qhh.Where(p => p.STT == 12).Count() > 0 && qhh.Where(p => p.STT == 12).First().MaDVct != null)
                {
                    labT11.Text = "X";
                }
                if (qhh.Where(p => p.STT == 12).Count() > 0 && qhh.Where(p => p.STT == 12).First().KetQua != null)
                {
                    colKQ11.Text = qhh.Where(p => p.STT == 12).First().KetQua.ToString();
                }
                if (qhh.Where(p => p.STT == 13).Count() > 0 && qhh.Where(p => p.STT == 13).First().MaDVct != null)
                {
                    labT111.Text = "X";
                }
                if (qhh.Where(p => p.STT == 13).Count() > 0 && qhh.Where(p => p.STT == 13).First().KetQua != null)
                {
                    colKQ111.Text = qhh.Where(p => p.STT == 13).First().KetQua.ToString();
                }
                if (qhh.Where(p => p.STT == 14).Count() > 0 && qhh.Where(p => p.STT == 14).First().MaDVct != null)
                {
                    labT112.Text = "X";
                }
                if (qhh.Where(p => p.STT == 14).Count() > 0 && qhh.Where(p => p.STT == 14).First().KetQua != null)
                {
                    colKQ112.Text = qhh.Where(p => p.STT == 14).First().KetQua.ToString();
                }
                if (qhh.Where(p => p.STT == 15).Count() > 0 && qhh.Where(p => p.STT == 15).First().MaDVct != null)
                {
                    labT121.Text = "X";
                }
                if (qhh.Where(p => p.STT == 15).Count() > 0 && qhh.Where(p => p.STT == 15).First().KetQua != null)
                {
                    colKQ121.Text = qhh.Where(p => p.STT == 15).First().KetQua.ToString();
                }
                if (qhh.Where(p => p.STT == 16).Count() > 0 && qhh.Where(p => p.STT == 16).First().MaDVct != null)
                {
                    labT131.Text = "X";
                }
                if (qhh.Where(p => p.STT == 16).Count() > 0 && qhh.Where(p => p.STT == 16).First().KetQua != null)
                {
                    colKQ131.Text = qhh.Where(p => p.STT == 16).First().KetQua.ToString();
                }
                if (qhh.Where(p => p.STT == 17).Count() > 0 && qhh.Where(p => p.STT == 17).First().MaDVct != null)
                {
                    labT132.Text = "X";
                }
                if (qhh.Where(p => p.STT == 17).Count() > 0 && qhh.Where(p => p.STT == 17).First().KetQua != null)
                {
                    colKQ132.Text = qhh.Where(p => p.STT == 17).First().KetQua.ToString();
                }
                if (qhh.Where(p => p.STT == 18).Count() > 0 && qhh.Where(p => p.STT == 18).First().MaDVct != null)
                {
                    labT141.Text = "X";
                }
                if (qhh.Where(p => p.STT == 18).Count() > 0 && qhh.Where(p => p.STT == 18).First().KetQua != null)
                {
                    colKQ141.Text = qhh.Where(p => p.STT == 18).First().KetQua.ToString();
                }
                if (qhh.Where(p => p.STT == 19).Count() > 0 && qhh.Where(p => p.STT == 19).First().MaDVct != null)
                {
                    labT17.Text = "X";
                }
                if (qhh.Where(p => p.STT == 19).Count() > 0 && qhh.Where(p => p.STT == 19).First().KetQua != null)
                {
                    colKQ17.Text = qhh.Where(p => p.STT == 19).First().KetQua.ToString();
                }
                if (qhh.Where(p => p.STT == 20).Count() > 0 && qhh.Where(p => p.STT == 20).First().MaDVct != null)
                {
                    labT18.Text = "X";
                }
                if (qhh.Where(p => p.STT == 20).Count() > 0 && qhh.Where(p => p.STT == 20).First().KetQua != null)
                {
                    colKQ18.Text = qhh.Where(p => p.STT == 20).First().KetQua.ToString();
                }
                if (qhh.Where(p => p.STT == 21).Count() > 0 && qhh.Where(p => p.STT == 21).First().MaDVct != null)
                {
                    labT19.Text = "X";
                }
                if (qhh.Where(p => p.STT == 21).Count() > 0 && qhh.Where(p => p.STT == 21).First().KetQua != null)
                {
                    colKQ19.Text = qhh.Where(p => p.STT == 21).First().KetQua.ToString();
                }
                if (qhh.Where(p => p.STT == 22).Count() > 0 && qhh.Where(p => p.STT == 22).First().MaDVct != null)
                {
                    labT20.Text = "X";
                }
                if (qhh.Where(p => p.STT == 22).Count() > 0 && qhh.Where(p => p.STT == 22).First().KetQua != null)
                {
                    colKQ21.Text = qhh.Where(p => p.STT == 22).First().KetQua.ToString();
                }
                if (qhh.Where(p => p.STT == 23).Count() > 0 && qhh.Where(p => p.STT == 23).First().MaDVct != null)
                {
                    labT22.Text = "X";
                }
                if (qhh.Where(p => p.STT == 23).Count() > 0 && qhh.Where(p => p.STT == 23).First().KetQua != null)
                {
                    colKQ22.Text = qhh.Where(p => p.STT == 23).First().KetQua.ToString();
                }
                if (qhh.Where(p => p.STT == 24).Count() > 0 && qhh.Where(p => p.STT == 24).First().MaDVct != null)
                {
                    labT23.Text = "X";
                }
                if (qhh.Where(p => p.STT == 24).Count() > 0 && qhh.Where(p => p.STT == 24).First().KetQua != null)
                {
                    colKQ23.Text = qhh.Where(p => p.STT == 24).First().KetQua.ToString();
                }
                if (qhh.Where(p => p.STT == 25).Count() > 0 && qhh.Where(p => p.STT == 25).First().MaDVct != null)
                {
                    labT24.Text = "X";
                }
                if (qhh.Where(p => p.STT == 25).Count() > 0 && qhh.Where(p => p.STT == 25).First().KetQua != null)
                {
                    colKQ24.Text = qhh.Where(p => p.STT == 25).First().KetQua.ToString();
                }

            }

        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            colTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            colTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            string _bs = MaCBDT.Value.ToString();

            var bsdt = (from bs in DataContect.CanBoes
                        where (bs.MaCB == _bs)
                        select new { bs.TenCB }).ToList();
            if (bsdt.Count > 0)
            {
                colTenBSDT.Text = bsdt.First().TenCB;
            }
            var bsxn = (from bs in DataContect.CanBoes.Where(p => p.MaCB == Macb.Value)
                        select bs).ToList();
            if (bsxn.Count > 0)
            {
                if (DungChung.Bien.MaBV == "27183")
                {

                    colTenTKXN.Text = bsxn.FirstOrDefault().CapBac + ". " + bsxn.FirstOrDefault().TenCB;
                }
                else
                    colTenTKXN.Text = bsxn.First().TenCB;
            }

            if (DungChung.Bien.MaBV == "30004")
            {
                colTenBSDT.Visible = false;
                colTenTKXN.Visible = false;
            }
        }
    }
}
