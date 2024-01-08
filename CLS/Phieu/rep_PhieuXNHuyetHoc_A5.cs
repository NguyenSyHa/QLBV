using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class rep_PhieuXNHuyetHoc_A5 : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_PhieuXNHuyetHoc_A5()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities DataContect = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            lab1.Text = "PHIẾU XÉT NGHIỆM HUYẾT HỌC";
            colTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            colTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            if (MaCBDT.Value != null)
            {
                colTenBSDT.Text = DungChung.Ham._getTenCB(DataContect, MaCBDT.Value.ToString());
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
            if (DungChung.Bien.MaBV == "30009")
                xrRichText1.Visible = true;
            else
                xrRichText1.Visible = false;
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            int sophieu = int.Parse(SoPhieu.Value.ToString());

            var qhh = (from cls in DataContect.CLS.Where(p => p.IdCLS == sophieu)
                       join chidinh in DataContect.ChiDinhs on cls.IdCLS equals chidinh.IdCLS
                       join clsct in DataContect.CLScts on chidinh.IDCD equals clsct.IDCD
                       join dvct in DataContect.DichVucts on clsct.MaDVct equals dvct.MaDVct
                       join dichvu in DataContect.DichVus on dvct.MaDV equals dichvu.MaDV
                       join tnhomdv in DataContect.TieuNhomDVs on dichvu.IdTieuNhom equals tnhomdv.IdTieuNhom
                       where (tnhomdv.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc)
                       select new { clsct.Status, clsct.MaDVct, clsct.KetQua, dvct.STT, chidinh.MaDV }).ToList();
            var qmadv = qhh.Select(p => p.MaDV.Value).Distinct().ToList();

            var qcls = (from dvct in DataContect.DichVucts
                        join dv in DataContect.DichVus.Where(p => qmadv.Contains(p.MaDV)) on dvct.MaDV equals dv.MaDV
                        join tnhomdv in DataContect.TieuNhomDVs on dv.IdTieuNhom equals tnhomdv.IdTieuNhom
                        where (tnhomdv.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc)
                        select new { dvct.MaDVct, dvct.TenDVct, dvct.TSBT, dvct.STT }).ToList();

            if (qcls.Count > 0)
            {
                //if (qcls.Where(p => p.STT == 1).Count() > 0 && qcls.Where(p => p.STT == 1).First().TenDVct != null && qcls.Where(p => p.STT == 1).First().TSBT == null)
                //{

                //    col1.Text = qcls.Where(p => p.STT == 1).First().TenDVct.ToString();
                //}
                //else 
                if (qcls.Where(p => p.STT == 1).Count() > 0 && qcls.Where(p => p.STT == 1).First().TenDVct != null && qcls.Where(p => p.STT == 1).First().TSBT != null)
                {
                    string a = qcls.Where(p => p.STT == 1).First().TenDVct.ToString() + qcls.Where(p => p.STT == 1).First().TSBT.ToString();
                    a = a.Replace("#", "\n");
                    col1.Text = a;
                }
                //if (qcls.Where(p => p.STT == 2).Count() > 0 && qcls.Where(p => p.STT == 2).First().TenDVct != null && qcls.Where(p => p.STT == 2).First().TSBT == null)
                //{
                //    col2.Text = qcls.Where(p => p.STT == 2).First().TenDVct.ToString();
                //}
                //else
                if (qcls.Where(p => p.STT == 2).Count() > 0 && qcls.Where(p => p.STT == 2).First().TenDVct != null && qcls.Where(p => p.STT == 2).First().TSBT != null)
                {
                    string a = qcls.Where(p => p.STT == 2).First().TenDVct.ToString() + qcls.Where(p => p.STT == 2).First().TSBT.ToString();
                    a = a.Replace("#", "\n");
                    col2.Text = a;
                }
                //if (qcls.Where(p => p.STT == 3).Count() > 0 && qcls.Where(p => p.STT == 3).First().TenDVct != null && qcls.Where(p => p.STT == 3).First().TSBT == null)
                //{
                //    col3.Text = qcls.Where(p => p.STT == 3).First().TenDVct.ToString();
                //}
                //else
                if (qcls.Where(p => p.STT == 3).Count() > 0 && qcls.Where(p => p.STT == 3).First().TenDVct != null && qcls.Where(p => p.STT == 3).First().TSBT != null)
                {
                    string a = qcls.Where(p => p.STT == 3).First().TenDVct.ToString() + qcls.Where(p => p.STT == 3).First().TSBT.ToString();
                    a = a.Replace("#", "\n");
                    col3.Text = a;
                }
                //if (qcls.Where(p => p.STT == 4).Count() > 0 && qcls.Where(p => p.STT == 4).First().TenDVct != null && qcls.Where(p => p.STT == 4).First().TSBT == null)
                //{
                //    col4.Text = qcls.Where(p => p.STT == 4).First().TenDVct.ToString();
                //}
                //else
                if (qcls.Where(p => p.STT == 4).Count() > 0 && qcls.Where(p => p.STT == 4).First().TenDVct != null && qcls.Where(p => p.STT == 4).First().TSBT != null)
                {
                    string a = qcls.Where(p => p.STT == 4).First().TenDVct.ToString() + qcls.Where(p => p.STT == 4).First().TSBT.ToString();
                    a = a.Replace("#", "\n");
                    col4.Text = a;
                }
                //if (qcls.Where(p => p.STT == 5).Count() > 0 && qcls.Where(p => p.STT == 5).First().TenDVct != null && qcls.Where(p => p.STT == 5).First().TSBT == null)
                //{
                //    col5.Text = qcls.Where(p => p.STT == 5).First().TenDVct.ToString();
                //}
                //else 
                if (qcls.Where(p => p.STT == 5).Count() > 0 && qcls.Where(p => p.STT == 5).First().TenDVct != null && qcls.Where(p => p.STT == 5).First().TSBT != null)
                {
                    string a = qcls.Where(p => p.STT == 5).First().TenDVct.ToString() + qcls.Where(p => p.STT == 5).First().TSBT.ToString();
                    a = a.Replace("#", "\n");
                    col5.Text = a;
                }
                //if (qcls.Where(p => p.STT == 6).Count() > 0 && qcls.Where(p => p.STT == 6).First().TenDVct != null && qcls.Where(p => p.STT == 6).First().TSBT == null)
                //{
                //    col6.Text = qcls.Where(p => p.STT == 6).First().TenDVct.ToString();
                //}
                //else 
                if (qcls.Where(p => p.STT == 6).Count() > 0 && qcls.Where(p => p.STT == 6).First().TenDVct != null && qcls.Where(p => p.STT == 6).First().TSBT != null)
                {
                    string a = qcls.Where(p => p.STT == 6).First().TenDVct.ToString() + qcls.Where(p => p.STT == 6).First().TSBT.ToString();
                    a = a.Replace("#", "\n");
                    col6.Text = a;
                }
                //if (qcls.Where(p => p.STT == 7).Count() > 0 && qcls.Where(p => p.STT == 7).First().TenDVct != null && qcls.Where(p => p.STT == 7).First().TSBT == null)
                //{
                //    col7.Text = qcls.Where(p => p.STT == 7).First().TenDVct.ToString();
                //}
                //else 
                if (qcls.Where(p => p.STT == 7).Count() > 0 && qcls.Where(p => p.STT == 7).First().TenDVct != null && qcls.Where(p => p.STT == 7).First().TSBT != null)
                {
                    string a = qcls.Where(p => p.STT == 7).First().TenDVct.ToString() + qcls.Where(p => p.STT == 7).First().TSBT.ToString();
                    a = a.Replace("#", "\n");
                    col7.Text = a;
                }
                //if (qcls.Where(p => p.STT == 8).Count() > 0 && qcls.Where(p => p.STT == 8).First().TenDVct != null && qcls.Where(p => p.STT == 8).First().TSBT == null)
                //{
                //    col8.Text = qcls.Where(p => p.STT == 8).First().TenDVct.ToString();
                //}
                //else 
                if (qcls.Where(p => p.STT == 8).Count() > 0 && qcls.Where(p => p.STT == 8).First().TenDVct != null && qcls.Where(p => p.STT == 8).First().TSBT != null)
                {
                    string a = qcls.Where(p => p.STT == 8).First().TenDVct.ToString() + qcls.Where(p => p.STT == 8).First().TSBT.ToString();
                    a = a.Replace("#", "\n");
                    col8.Text = a;
                }
                //if (qcls.Where(p => p.STT == 9).Count() > 0 && qcls.Where(p => p.STT ==9).First().TenDVct != null && qcls.Where(p => p.STT == 9).First().TSBT == null)
                //{
                //    col9.Text = qcls.Where(p => p.STT == 9).First().TenDVct.ToString();
                //}
                //else 
                if (qcls.Where(p => p.STT == 9).Count() > 0 && qcls.Where(p => p.STT == 9).First().TenDVct != null && qcls.Where(p => p.STT == 9).First().TSBT != null)
                {
                    string a = qcls.Where(p => p.STT == 9).First().TenDVct.ToString() + qcls.Where(p => p.STT == 9).First().TSBT.ToString();
                    a = a.Replace("#", "\n");
                    col9.Text = a;
                }
                //if (qcls.Where(p => p.STT == 10).Count() > 0 && qcls.Where(p => p.STT == 10).First().TenDVct != null && qcls.Where(p => p.STT == 10).First().TSBT == null)
                //{
                //    col10.Text = qcls.Where(p => p.STT == 10).First().TenDVct.ToString();
                //}
                //else
                if (qcls.Where(p => p.STT == 10).Count() > 0 && qcls.Where(p => p.STT == 10).First().TenDVct != null && qcls.Where(p => p.STT == 10).First().TSBT != null)
                {
                    string a = qcls.Where(p => p.STT == 10).First().TenDVct.ToString() + qcls.Where(p => p.STT == 10).First().TSBT.ToString();
                    a = a.Replace("#", "\n");
                    col10.Text = a;
                }
                //if (qcls.Where(p => p.STT == 11).Count() > 0 && qcls.Where(p => p.STT == 11).First().TenDVct != null && qcls.Where(p => p.STT == 11).First().TSBT == null)
                //{
                //    col11.Text = qcls.Where(p => p.STT == 11).First().TenDVct.ToString();
                //}
                //else 
                if (qcls.Where(p => p.STT == 11).Count() > 0 && qcls.Where(p => p.STT == 11).First().TenDVct != null && qcls.Where(p => p.STT == 11).First().TSBT != null)
                {
                    string a = qcls.Where(p => p.STT == 11).First().TenDVct.ToString() + qcls.Where(p => p.STT == 11).First().TSBT.ToString();
                    a = a.Replace("#", "\n");
                    col11.Text = a;
                }
                //if (qcls.Where(p => p.STT == 12).Count() > 0 && qcls.Where(p => p.STT == 12).First().TenDVct != null && qcls.Where(p => p.STT == 12).First().TSBT == null)
                //{
                //    col12.Text = qcls.Where(p => p.STT == 12).First().TenDVct.ToString();
                //}
                //else 
                if (qcls.Where(p => p.STT == 12).Count() > 0 && qcls.Where(p => p.STT == 12).First().TenDVct != null && qcls.Where(p => p.STT == 12).First().TSBT != null)
                {
                    string a = qcls.Where(p => p.STT == 12).First().TenDVct.ToString() + qcls.Where(p => p.STT == 12).First().TSBT.ToString();
                    a = a.Replace("#", "\n");
                    col12.Text = a;
                }
                //if (qcls.Where(p => p.STT == 13).Count() > 0 && qcls.Where(p => p.STT == 13).First().TenDVct != null && qcls.Where(p => p.STT == 13).First().TSBT == null)
                //{
                //    col13.Text = qcls.Where(p => p.STT == 13).First().TenDVct.ToString();
                //}
                //else

                if (qcls.Where(p => p.STT == 13).Count() > 0 && qcls.Where(p => p.STT == 13).First().TenDVct != null && qcls.Where(p => p.STT == 13).First().TSBT != null)
                {
                    string a = qcls.Where(p => p.STT == 13).First().TenDVct.ToString() + qcls.Where(p => p.STT == 13).First().TSBT.ToString();
                    a = a.Replace("#", "\n");
                    col13.Text = a;
                }
                //if (qcls.Where(p => p.STT == 14).Count() > 0 && qcls.Where(p => p.STT == 14).First().TenDVct != null && qcls.Where(p => p.STT == 14).First().TSBT == null)
                //{
                //    col14.Text = qcls.Where(p => p.STT == 14).First().TenDVct.ToString();
                //}
                //else
                if (qcls.Where(p => p.STT == 14).Count() > 0 && qcls.Where(p => p.STT == 14).First().TenDVct != null && qcls.Where(p => p.STT == 14).First().TSBT != null)
                {
                    string a = qcls.Where(p => p.STT == 14).First().TenDVct.ToString() + qcls.Where(p => p.STT == 14).First().TSBT.ToString();
                    a = a.Replace("#", "\n");
                    col14.Text = a;
                }
                //if (qcls.Where(p => p.STT == 15).Count() > 0 && qcls.Where(p => p.STT == 15).First().TenDVct != null && qcls.Where(p => p.STT == 15).First().TSBT == null)
                //{
                //    col15.Text = qcls.Where(p => p.STT == 15).First().TenDVct.ToString();
                //}
                //else
                if (qcls.Where(p => p.STT == 15).Count() > 0 && qcls.Where(p => p.STT == 15).First().TenDVct != null && qcls.Where(p => p.STT == 15).First().TSBT != null)
                {
                    string a = qcls.Where(p => p.STT == 15).First().TenDVct.ToString() + qcls.Where(p => p.STT == 15).First().TSBT.ToString();
                    a = a.Replace("#", "\n");
                    col15.Text = a;
                }
                //if (qcls.Where(p => p.STT == 16).Count() > 0 && qcls.Where(p => p.STT == 16).First().TenDVct != null && qcls.Where(p => p.STT == 16).First().TSBT == null)
                //{
                //    col16.Text = qcls.Where(p => p.STT == 16).First().TenDVct.ToString();
                //}
                //else 
                if (qcls.Where(p => p.STT == 16).Count() > 0 && qcls.Where(p => p.STT == 16).First().TenDVct != null && qcls.Where(p => p.STT == 16).First().TSBT != null)
                {
                    string a = qcls.Where(p => p.STT == 16).First().TenDVct.ToString() + qcls.Where(p => p.STT == 16).First().TSBT.ToString();
                    a = a.Replace("#", "\n");
                    col16.Text = a;
                }
                //if (qcls.Where(p => p.STT == 17).Count() > 0 && qcls.Where(p => p.STT == 17).First().TenDVct != null && qcls.Where(p => p.STT == 17).First().TSBT == null)
                //{
                //    col17.Text = qcls.Where(p => p.STT == 17).First().TenDVct.ToString();
                //}
                //else
                if (qcls.Where(p => p.STT == 17).Count() > 0 && qcls.Where(p => p.STT == 17).First().TenDVct != null && qcls.Where(p => p.STT == 17).First().TSBT != null)
                {
                    string a = qcls.Where(p => p.STT == 17).First().TenDVct.ToString() + qcls.Where(p => p.STT == 17).First().TSBT.ToString();
                    a = a.Replace("#", "\n");
                    col17.Text = a;
                }
                //if (qcls.Where(p => p.STT == 18).Count() > 0 && qcls.Where(p => p.STT == 18).First().TenDVct != null && qcls.Where(p => p.STT == 18).First().TSBT == null)
                //{
                //    col18.Text = qcls.Where(p => p.STT == 18).First().TenDVct.ToString();
                //}
                //else 
                if (qcls.Where(p => p.STT == 18).Count() > 0 && qcls.Where(p => p.STT == 18).First().TenDVct != null && qcls.Where(p => p.STT == 18).First().TSBT != null)
                {
                    string a = qcls.Where(p => p.STT == 18).First().TenDVct.ToString() + qcls.Where(p => p.STT == 18).First().TSBT.ToString();
                    a = a.Replace("#", "\n");
                    col18.Text = a;
                }
                //if (qcls.Where(p => p.STT == 19).Count() > 0 && qcls.Where(p => p.STT == 19).First().TenDVct != null && qcls.Where(p => p.STT == 19).First().TSBT == null)
                //{
                //    col19.Text = qcls.Where(p => p.STT == 19).First().TenDVct.ToString();
                //}
                //else 
                if (qcls.Where(p => p.STT == 19).Count() > 0 && qcls.Where(p => p.STT == 19).First().TenDVct != null && qcls.Where(p => p.STT == 19).First().TSBT != null)
                {
                    string a = qcls.Where(p => p.STT == 19).First().TenDVct.ToString() + qcls.Where(p => p.STT == 19).First().TSBT.ToString();
                    a = a.Replace("#", "\n");
                    col19.Text = a;
                }
                //if (qcls.Where(p => p.STT == 20).Count() > 0 && qcls.Where(p => p.STT == 20).First().TenDVct != null && qcls.Where(p => p.STT == 20).First().TSBT == null)
                //{
                //    col20.Text = qcls.Where(p => p.STT == 20).First().TenDVct.ToString();
                //}
                //else
                if (qcls.Where(p => p.STT == 20).Count() > 0 && qcls.Where(p => p.STT == 20).First().TenDVct != null && qcls.Where(p => p.STT == 20).First().TSBT != null)
                {
                    string a = qcls.Where(p => p.STT == 20).First().TenDVct.ToString() + qcls.Where(p => p.STT == 20).First().TSBT.ToString();
                    a = a.Replace("#", "\n");
                    col20.Text = a;
                }
                //if (qcls.Where(p => p.STT == 21).Count() > 0 && qcls.Where(p => p.STT == 21).First().TenDVct != null && qcls.Where(p => p.STT == 21).First().TSBT == null)
                //{
                //    col21.Text = qcls.Where(p => p.STT == 21).First().TenDVct.ToString();
                //}
                //else 
                if (qcls.Where(p => p.STT == 21).Count() > 0 && qcls.Where(p => p.STT == 21).First().TenDVct != null && qcls.Where(p => p.STT == 21).First().TSBT != null)
                {
                    string a = qcls.Where(p => p.STT == 21).First().TenDVct.ToString() + qcls.Where(p => p.STT == 21).First().TSBT.ToString();
                    a = a.Replace("#", "\n");
                    col21.Text = a;
                }
                //if (qcls.Where(p => p.STT == 22).Count() > 0 && qcls.Where(p => p.STT == 22).First().TenDVct != null && qcls.Where(p => p.STT == 22).First().TSBT == null)
                //{
                //    col22.Text = qcls.Where(p => p.STT == 22).First().TenDVct.ToString();
                //}
                //else 
                if (qcls.Where(p => p.STT == 22).Count() > 0 && qcls.Where(p => p.STT == 22).First().TenDVct != null && qcls.Where(p => p.STT == 22).First().TSBT != null)
                {
                    string a = qcls.Where(p => p.STT == 22).First().TenDVct.ToString() + qcls.Where(p => p.STT == 22).First().TSBT.ToString();
                    a = a.Replace("#", "\n");
                    col22.Text = a;
                }
                //if (qcls.Where(p => p.STT == 23).Count() > 0 && qcls.Where(p => p.STT == 23).First().TenDVct != null && qcls.Where(p => p.STT == 23).First().TSBT == null)
                //{
                //    col23.Text = qcls.Where(p => p.STT == 23).First().TenDVct.ToString();
                //}
                //else
                if (qcls.Where(p => p.STT == 23).Count() > 0 && qcls.Where(p => p.STT == 23).First().TenDVct != null && qcls.Where(p => p.STT == 23).First().TSBT != null)
                {
                    string a = qcls.Where(p => p.STT == 23).First().TenDVct.ToString() + qcls.Where(p => p.STT == 23).First().TSBT.ToString();
                    a = a.Replace("#", "\n");
                    col23.Text = a;
                }
                //if (qcls.Where(p => p.STT == 24).Count() > 0 && qcls.Where(p => p.STT == 24).First().TenDVct != null && qcls.Where(p => p.STT == 24).First().TSBT == null)
                //{
                //    col24.Text = qcls.Where(p => p.STT == 24).First().TenDVct.ToString();
                //}
                //else
                if (qcls.Where(p => p.STT == 24).Count() > 0 && qcls.Where(p => p.STT == 24).First().TenDVct != null && qcls.Where(p => p.STT == 24).First().TSBT != null)
                {
                    string a = qcls.Where(p => p.STT == 24).First().TenDVct.ToString() + qcls.Where(p => p.STT == 24).First().TSBT.ToString();
                    a = a.Replace("#", "\n");
                    col24.Text = a;
                }
                //if (qcls.Where(p => p.STT == 25).Count() > 0 && qcls.Where(p => p.STT == 25).First().TenDVct != null && qcls.Where(p => p.STT == 25).First().TSBT == null)
                //{
                //    col25.Text = qcls.Where(p => p.STT == 25).First().TenDVct.ToString();
                //}
                //else 
                if (qcls.Where(p => p.STT == 25).Count() > 0 && qcls.Where(p => p.STT == 25).First().TenDVct != null && qcls.Where(p => p.STT == 25).First().TSBT != null)
                {
                    string a = qcls.Where(p => p.STT == 25).First().TenDVct.ToString() + qcls.Where(p => p.STT == 25).First().TSBT.ToString();
                    a = a.Replace("#", "\n");
                    col25.Text = a;
                }
                //if (qcls.Where(p => p.STT == 26).Count() > 0 && qcls.Where(p => p.STT == 26).First().TenDVct != null && qcls.Where(p => p.STT == 26).First().TSBT == null)
                //{
                //    col26.Text = qcls.Where(p => p.STT == 26).First().TenDVct.ToString();
                //}
                //else 
                if (qcls.Where(p => p.STT == 26).Count() > 0 && qcls.Where(p => p.STT == 26).First().TenDVct != null && qcls.Where(p => p.STT == 26).First().TSBT != null)
                {
                    string a = qcls.Where(p => p.STT == 26).First().TenDVct.ToString() + qcls.Where(p => p.STT == 26).First().TSBT.ToString();
                    a = a.Replace("#", "\n");
                    col26.Text = a;
                }
                //if (qcls.Where(p => p.STT == 27).Count() > 0 && qcls.Where(p => p.STT == 27).First().TenDVct != null && qcls.Where(p => p.STT == 27).First().TSBT == null)
                //{
                //    col27.Text = qcls.Where(p => p.STT == 27).First().TenDVct.ToString();
                //}
                //else 
                if (qcls.Where(p => p.STT == 27).Count() > 0 && qcls.Where(p => p.STT == 27).First().TenDVct != null && qcls.Where(p => p.STT == 27).First().TSBT != null)
                {
                    string a = qcls.Where(p => p.STT == 27).First().TenDVct.ToString() + qcls.Where(p => p.STT == 27).First().TSBT.ToString();
                    a = a.Replace("#", "\n");
                    col27.Text = a;
                }


            }
            //string mabn = MaBNhan.Value.ToString();

            string _tebaoNV = "";
            for (int i = 1; i < 22; i++)
            {
                if (qhh.Where(p => p.STT == i).Count() > 0 && qhh.Where(p => p.STT == i).First().MaDVct != null)
                {
                    _tebaoNV = "X";
                    break;
                }
            }
            if (qhh.Count > 0)
            {
                labT0.Text = _tebaoNV;


                foreach (XRTableRow _tableRow in xrTable1)
                {

                    foreach (XRTableCell _tableCell in _tableRow)
                    {
                        if (DungChung.Bien.MaBV == "24009" || DungChung.Bien.MaBV == "30005" || DungChung.Bien.MaBV == "30007" || DungChung.Bien.MaBV == "30280" || DungChung.Bien.MaBV == "34019" || DungChung.Bien.MaBV == "27023")
                        {
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


                        for (int i = 1; i < 28; i++)//colKQ14
                        {
                            if (_tableCell.Name == "colKQ" + (i).ToString() || _tableCell.Name == "colkq" + (i).ToString())
                            {

                                if (qhh.Where(p => p.STT == i).Count() > 0 && qhh.Where(p => p.STT == i).First().KetQua != null)
                                    _tableCell.Text = qhh.Where(p => p.STT == i).First().KetQua;
                            }
                            if (i > 24)
                            {
                                if (_tableCell.Name == "labT" + (i).ToString())
                                {
                                    if (qhh.Where(p => p.STT == i).Count() > 0 && qhh.Where(p => p.STT == i).First().MaDVct != null)
                                        _tableCell.Text = "X";
                                }
                            }
                        }
                    }

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
                    if (DungChung.Bien.MaBV == "24009")
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

            }
            if (DungChung.Bien.MaBV == "04011")
            {
                tab2.Visible = false;
            }
        }

        private void xrSubreport1_BeforePrint(object sender, CancelEventArgs e)
        {
            rep_PhieuXN_Sub_A5 repsub = (rep_PhieuXN_Sub_A5)xrSubreport1.ReportSource;
            repsub.NGAYKIXN.Value = NgayTH.Value.ToString();
            repsub.NGAYKYDT.Value = NgayCD.Value.ToString();
            repsub.BSDT.Value = DungChung.Ham._getTenCB(DataContect, MaCBDT.Value.ToString());
            repsub.TKXN.Value = DungChung.Ham._getTenCB(DataContect, Macb.Value.ToString());
            repsub.tb12128.ForeColor = System.Drawing.Color.Red;
        }

        private void xrTableRow1_BeforePrint(object sender, CancelEventArgs e)
        {

        }


        private void lab44_BeforePrint_1(object sender, CancelEventArgs e)
        {

        }

        private void lab49_BeforePrint_1(object sender, CancelEventArgs e)
        {

        }

    }
}
