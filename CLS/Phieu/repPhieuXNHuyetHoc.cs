using System;
using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;

namespace QLBV.BaoCao
{
    public partial class repPhieuXNHuyetHoc : DevExpress.XtraReports.UI.XtraReport
    {
        List<int> _lIdCLS = new List<int>();
        List<int> _lIdCD = new List<int>();
        List<int?> _lMaDV = new List<int?>();
        public repPhieuXNHuyetHoc()
        {
            InitializeComponent();
        }
        public repPhieuXNHuyetHoc(List<int> _lIdCLS, List<int?> _lMaDV, List<int> _lIdCD)
        {
            InitializeComponent();
            this._lIdCLS = _lIdCLS;
            this._lIdCD = _lIdCD;
            this._lMaDV = _lMaDV;
            if (DungChung.Bien.MaBV == "27777")
            {
                colDiaChi.Text = DungChung.Ham.GetDiaChiBV();
            }
        }
        QLBV_Database.QLBVEntities DataContect = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {

        }
        public void TaoPhieu(int kieuphieu)
        {
            // 0 : xét nghiệm tế bào dịch subband3
            //1: Xét nghiệm huyết học subband1,2
            if (kieuphieu == 0)
            {

                SubBand1.Visible = false;
                SubBand3.Visible = true;
                if (DungChung.Bien.MaBV == "27023")
                {
                    lab48.Visible = lab46.Visible = true;
                }
            }
            else if (kieuphieu == 1)
            {
                if (DungChung.Bien.MaBV == "24009" || DungChung.Bien.MaBV == "27777")
                {
                    SubBand1.Visible = false;
                    SubBand2.Visible = true;
                    xrPictureBox2.Image = DungChung.Ham.GetLogo();
                    xrLabel42.Text = DungChung.Ham.GetDiaChiBV();
                    if (DungChung.Bien.MaBV == "27777")
                    {
                        colDiaChi.Text = "Số 307 - Minh Khai, Phố Mới, Đồng Nguyên,Từ Sơn, Bắc Ninh";
                    }
                }
            }
            if (DungChung.Bien.MaBV == "27001")
            {
                SubBand1.Visible = false;
                SubBand2.Visible = false;
                SubBand3.Visible = false;
                SubBand4.Visible = true;
            }
            if (DungChung.Bien.MaBV == "30002")
            {
                lab16.Text = "1. Tổng phân tích tế bào máu ngoại vi:";
                lab16.WidthF = 300f;
                labT0.LocationF = new DevExpress.Utils.PointFloat(300.00F, 0F);
            }

            if  (DungChung.Bien.MaBV=="30009" || DungChung.Bien.MaBV == "30303")
            {
                lab55.Text = "TRƯỞNG KHOA XÉT NGHIỆM ";
            }
            
            if (DungChung.Bien.MaBV == "24009" || DungChung.Bien.MaBV == "27777")//his-124 11/05
            {
                xrTableRow21.Visible = true;
                xrTableRow20.Visible = true;
                xrTableRow19.Visible = true;
                xrTableRow18.Visible = true;
                xrTableRow17.Visible = true;
            }
                int sophieu = int.Parse(SoPhieu.Value.ToString());

            var qhh = (from cls in DataContect.CLS.Where(p => p.IdCLS == sophieu)
                       join chidinh in DataContect.ChiDinhs on cls.IdCLS equals chidinh.IdCLS
                       join clsct in DataContect.CLScts on chidinh.IDCD equals clsct.IDCD
                       join dvct in DataContect.DichVucts on clsct.MaDVct equals dvct.MaDVct
                       join dichvu in DataContect.DichVus on dvct.MaDV equals dichvu.MaDV
                       join tnhomdv in DataContect.TieuNhomDVs on dichvu.IdTieuNhom equals tnhomdv.IdTieuNhom
                       where (kieuphieu == 0 ? (tnhomdv.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.TeBaoDich) : (tnhomdv.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc))
                       select new { clsct.Status, clsct.MaDVct, clsct.KetQua, dvct.STT, chidinh.MaDV, dvct.TSnuTu, dvct.TSnuDen, dvct.TSnTu, dvct.TSnDen, cls.MaBNhan }).ToList();
            if (_lIdCLS != null)
            {
                if (DungChung.Bien.MaBV == "30372")
                {
                    qhh = (from cls in DataContect.CLS
                           join chidinh in DataContect.ChiDinhs on cls.IdCLS equals chidinh.IdCLS
                           join clsct in DataContect.CLScts on chidinh.IDCD equals clsct.IDCD
                           join dvct in DataContect.DichVucts on clsct.MaDVct equals dvct.MaDVct
                           join dichvu in DataContect.DichVus on dvct.MaDV equals dichvu.MaDV
                           join tnhomdv in DataContect.TieuNhomDVs on dichvu.IdTieuNhom equals tnhomdv.IdTieuNhom
                           where (kieuphieu == 0 ? (tnhomdv.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.TeBaoDich) : (tnhomdv.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc))
                           where _lIdCD.Contains(chidinh.IDCD)
                           select new { clsct.Status, clsct.MaDVct, clsct.KetQua, dvct.STT, chidinh.MaDV, dvct.TSnuTu, dvct.TSnuDen, dvct.TSnTu, dvct.TSnDen, cls.MaBNhan }).ToList();
                }
                else
                {
                    qhh = (from cls in DataContect.CLS.Where(p => DungChung.Bien.MaBV == "30372" ? _lIdCLS.Contains(p.IdCLS) : p.IdCLS == sophieu)
                           join chidinh in DataContect.ChiDinhs on cls.IdCLS equals chidinh.IdCLS
                           join clsct in DataContect.CLScts on chidinh.IDCD equals clsct.IDCD
                           join dvct in DataContect.DichVucts on clsct.MaDVct equals dvct.MaDVct
                           join dichvu in DataContect.DichVus on dvct.MaDV equals dichvu.MaDV
                           join tnhomdv in DataContect.TieuNhomDVs on dichvu.IdTieuNhom equals tnhomdv.IdTieuNhom
                           where (kieuphieu == 0 ? (tnhomdv.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.TeBaoDich) : (tnhomdv.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc))
                           select new { clsct.Status, clsct.MaDVct, clsct.KetQua, dvct.STT, chidinh.MaDV, dvct.TSnuTu, dvct.TSnuDen, dvct.TSnTu, dvct.TSnDen, cls.MaBNhan }).ToList();

                }
            }
            //var qhh1 = (from cls in DataContect.CLS.Where(p => p.IdCLS == sophieu)
            //           join chidinh in DataContect.ChiDinhs on cls.IdCLS equals chidinh.IdCLS
            //           join clsct in DataContect.CLScts on chidinh.IDCD equals clsct.IDCD
            //           join dvct in DataContect.DichVucts on clsct.MaDVct equals dvct.MaDVct
            //           join dichvu in DataContect.DichVus on dvct.MaDV equals dichvu.MaDV
            //           join tnhomdv in DataContect.TieuNhomDVs on dichvu.IdTieuNhom equals tnhomdv.IdTieuNhom
            //           where (kieuphieu == 0 ? (tnhomdv.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.TeBaoDich) : (tnhomdv.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc))
            //           select new { clsct.Status, clsct.MaDVct, clsct.KetQua, dvct.STT, chidinh.MaDV, dvct.TSnuTu, dvct.TSnuDen, dvct.TSnTu, dvct.TSnDen, cls.MaBNhan }).ToList();
            var qmadv = qhh.Select(p => p.MaDV.Value).Distinct().ToList();

            
            var qcls = (from dvct in DataContect.DichVucts
                        join dv in DataContect.DichVus.Where(p => qmadv.Contains(p.MaDV)) on dvct.MaDV equals dv.MaDV
                        join tnhomdv in DataContect.TieuNhomDVs on dv.IdTieuNhom equals tnhomdv.IdTieuNhom
                        where (kieuphieu == 0 ? (tnhomdv.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.TeBaoDich) : (tnhomdv.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc))
                        select new { dvct.MaDVct, dvct.TenDVct, dvct.TSBT, dvct.STT, dv.MaDV }).ToList();
            
            int Gtinh = 0;
            if (qhh.Count > 0)
            {
                int _mabn = qhh.First().MaBNhan ?? 0;
                var ttbn = DataContect.BenhNhans.Where(p => p.MaBNhan == _mabn).FirstOrDefault();
                if (ttbn != null)
                    Gtinh = ttbn.GTinh ?? 0;
            }
            if (qcls.Count > 0)
            {
                if (DungChung.Bien.MaBV == "24009")//his-124 11/05
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

                    if (qcls.Where(p => p.STT == 38).Count() > 0 && qcls.Where(p => p.STT == 38).First().TenDVct != null && qcls.Where(p => p.STT == 38).First().TSBT != null)
                    {
                        string a = qcls.Where(p => p.STT == 38).First().TenDVct.ToString() + qcls.Where(p => p.STT == 38).First().TSBT.ToString();
                        a = a.Replace("#", "\n");
                        col38.Text = a;
                    }

                    if (qcls.Where(p => p.STT == 39).Count() > 0 && qcls.Where(p => p.STT == 39).First().TenDVct != null && qcls.Where(p => p.STT == 39).First().TSBT != null)
                    {
                        string a = qcls.Where(p => p.STT == 39).First().TenDVct.ToString() + qcls.Where(p => p.STT == 39).First().TSBT.ToString();
                        a = a.Replace("#", "\n");
                        col39.Text = a;
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

                    if (qcls.Where(p => p.STT == 32).Count() > 0 && qcls.Where(p => p.STT == 32).First().TenDVct != null && qcls.Where(p => p.STT == 32).First().TSBT != null)
                    {
                        string a = qcls.Where(p => p.STT == 32).First().TenDVct.ToString() + qcls.Where(p => p.STT == 32).First().TSBT.ToString();
                        a = a.Replace("#", "\n");
                        col32.Text = a;
                    }

                    if (qcls.Where(p => p.STT == 33).Count() > 0 && qcls.Where(p => p.STT == 33).First().TenDVct != null && qcls.Where(p => p.STT == 33).First().TSBT != null)
                    {
                        string a = qcls.Where(p => p.STT == 33).First().TenDVct.ToString() + qcls.Where(p => p.STT == 33).First().TSBT.ToString();
                        a = a.Replace("#", "\n");
                        col33.Text = a;
                    }

                    if (qcls.Where(p => p.STT == 34).Count() > 0 && qcls.Where(p => p.STT == 34).First().TenDVct != null && qcls.Where(p => p.STT == 34).First().TSBT != null)
                    {
                        string a = qcls.Where(p => p.STT == 34).First().TenDVct.ToString() + qcls.Where(p => p.STT == 34).First().TSBT.ToString();
                        a = a.Replace("#", "\n");
                        col34.Text = a;
                    }

                    if (qcls.Where(p => p.STT == 35).Count() > 0 && qcls.Where(p => p.STT == 35).First().TenDVct != null && qcls.Where(p => p.STT == 35).First().TSBT != null)
                    {
                        string a = qcls.Where(p => p.STT == 35).First().TenDVct.ToString() + qcls.Where(p => p.STT == 35).First().TSBT.ToString();
                        a = a.Replace("#", "\n");
                        col35.Text = a;
                    }

                    if (qcls.Where(p => p.STT == 36).Count() > 0 && qcls.Where(p => p.STT == 36).First().TenDVct != null && qcls.Where(p => p.STT == 36).First().TSBT != null)
                    {
                        string a = qcls.Where(p => p.STT == 36).First().TenDVct.ToString() + qcls.Where(p => p.STT == 36).First().TSBT.ToString();
                        a = a.Replace("#", "\n");
                        col36.Text = a;
                    }

                    if (qcls.Where(p => p.STT == 37).Count() > 0 && qcls.Where(p => p.STT == 37).First().TenDVct != null && qcls.Where(p => p.STT == 37).First().TSBT != null)
                    {
                        string a = qcls.Where(p => p.STT == 37).First().TenDVct.ToString() + qcls.Where(p => p.STT == 37).First().TSBT.ToString();
                        a = a.Replace("#", "\n");
                        col37.Text = a;
                    }
                }else
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

            }
            //string mabn = MaBNhan.Value.ToString();

            string _tebaoNV = "";
            if (DungChung.Bien.MaBV == "24009")//his-124 11/05
            {
                for (int i = 1; i < 22; i++)
                {
                    if (qhh.Where(p => p.STT == i).Count() > 0 && qhh.Where(p => p.STT == i).First().MaDVct != null)
                    {
                        _tebaoNV = "X";
                        break;
                    }
                }
            }
            else if(DungChung.Bien.MaBV == "30372")
            {
                for (int i = 1; i < 22; i++)
                {
                    if (qhh.Where(p => p.STT == i && _lMaDV.Contains(p.MaDV)).Count() > 0 && qhh.Where(p => p.STT == i).First().MaDVct != null)
                    {
                        _tebaoNV = "X";
                        break;
                    }
                }
            }
            else
            {
                for (int i = 1; i < 22; i++)
                {
                    if (qhh.Where(p => p.STT == i).Count() > 0 && qhh.Where(p => p.STT == i).First().MaDVct != null)
                    {
                        _tebaoNV = "X";
                        break;
                    }
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
                            if (DungChung.Bien.MaBV == "24009")//his-124 11/05
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
                            else
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
                        }

                        if (DungChung.Bien.MaBV == "24009")//his-124 11/05
                        {
                            for (int i = 1; i < 40; i++)//colKQ14
                            {
                                if (_tableCell.Name == "colKQ" + (i).ToString() || _tableCell.Name == "colkq" + (i).ToString())
                                {

                                    if (qhh.Where(p => p.STT == i).Count() > 0 && qhh.Where(p => p.STT == i).First().KetQua != null)
                                    {
                                        try
                                        {
                                            if (Gtinh == 0)
                                            {
                                                if (qhh.Where(p => p.STT == i).First().TSnuTu == null && qhh.Where(p => p.STT == i).First().TSnuDen == null)
                                                {
                                                    _tableCell.Text = qhh.Where(p => p.STT == i).First().KetQua.ToString();
                                                }
                                                else
                                                {
                                                    if (qhh.Where(p => p.STT == i).First().TSnuTu != null && qhh.Where(p => p.STT == i).First().TSnuDen != null)
                                                    {
                                                        double tstu = Convert.ToDouble(qhh.Where(p => p.STT == i).First().TSnuTu);
                                                        double tsden = Convert.ToDouble(qhh.Where(p => p.STT == i).First().TSnuDen);
                                                        double kq = Convert.ToDouble(qhh.Where(p => p.STT == i).First().KetQua);
                                                        if (tstu <= kq && kq <= tsden)
                                                        {
                                                            _tableCell.Text = qhh.Where(p => p.STT == i).First().KetQua.ToString();
                                                        }
                                                        else if (kq <= tstu)
                                                        {
                                                            _tableCell.Text = qhh.Where(p => p.STT == i).First().KetQua.ToString();
                                                            _tableCell.ForeColor = System.Drawing.Color.Red;
                                                            _tableCell.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                            _tableCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                                        }
                                                        else if (kq >= tsden)
                                                        {
                                                            _tableCell.Text = qhh.Where(p => p.STT == i).First().KetQua.ToString();
                                                            _tableCell.ForeColor = System.Drawing.Color.Red;
                                                            _tableCell.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                            _tableCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                                        }
                                                    }
                                                    else if (qhh.Where(p => p.STT == i).First().TSnuTu != null && qhh.Where(p => p.STT == i).First().TSnuDen == null)//trị số nhỏ nhất
                                                    {
                                                        double tstu = Convert.ToDouble(qhh.Where(p => p.STT == i).First().TSnuTu);
                                                        double kq = Convert.ToDouble(qhh.Where(p => p.STT == i).First().KetQua);
                                                        if (tstu <= kq)
                                                        {
                                                            _tableCell.Text = qhh.Where(p => p.STT == i).First().KetQua.ToString();
                                                        }
                                                        else
                                                        {
                                                            _tableCell.Text = qhh.Where(p => p.STT == i).First().KetQua.ToString();
                                                            _tableCell.ForeColor = System.Drawing.Color.Red;
                                                            _tableCell.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                            _tableCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                                        }
                                                    }
                                                    else if (qhh.Where(p => p.STT == i).First().TSnuTu == null && qhh.Where(p => p.STT == i).First().TSnuDen != null)//giá trị lớn nhất
                                                    {
                                                        double tsden = Convert.ToDouble(qhh.Where(p => p.STT == i).First().TSnuDen);
                                                        double kq = Convert.ToDouble(qhh.Where(p => p.STT == i).First().KetQua);
                                                        if (kq <= tsden)
                                                        {
                                                            _tableCell.Text = qhh.Where(p => p.STT == i).First().KetQua.ToString();
                                                        }
                                                        else
                                                        {
                                                            _tableCell.Text = qhh.Where(p => p.STT == i).First().KetQua.ToString();
                                                            _tableCell.ForeColor = System.Drawing.Color.Red;
                                                            _tableCell.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                            _tableCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                                        }
                                                    }
                                                }
                                                //double tstu = Convert.ToDouble(qhh.Where(p => p.STT == i).First().TSnuTu);
                                                //double tsden = Convert.ToDouble(qhh.Where(p => p.STT == i).First().TSnuDen);
                                                //double kq = Convert.ToDouble(qhh.Where(p => p.STT == i).First().KetQua);
                                                //if (tstu <= kq && kq <= tsden)
                                                //    _tableCell.Text = qhh.Where(p => p.STT == i).First().KetQua.ToString();
                                                //else
                                                //{
                                                //    _tableCell.Text = qhh.Where(p => p.STT == i).First().KetQua.ToString();
                                                //    _tableCell.ForeColor = System.Drawing.Color.Black;
                                                //    _tableCell.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                //}
                                            }
                                            else
                                            {
                                                if (qhh.Where(p => p.STT == i).First().TSnTu == null && qhh.Where(p => p.STT == i).First().TSnDen == null)
                                                {
                                                    _tableCell.Text = qhh.Where(p => p.STT == i).First().KetQua.ToString();
                                                }
                                                else
                                                {
                                                    if (qhh.Where(p => p.STT == i).First().TSnTu != null && qhh.Where(p => p.STT == i).First().TSnDen != null)
                                                    {
                                                        double tstu = Convert.ToDouble(qhh.Where(p => p.STT == i).First().TSnTu);
                                                        double tsden = Convert.ToDouble(qhh.Where(p => p.STT == i).First().TSnDen);
                                                        double kq = Convert.ToDouble(qhh.Where(p => p.STT == i).First().KetQua);
                                                        if (tstu <= kq && kq <= tsden)
                                                        {
                                                            _tableCell.Text = qhh.Where(p => p.STT == i).First().KetQua.ToString();
                                                        }
                                                        else if (kq <= tstu)
                                                        {
                                                            _tableCell.Text = qhh.Where(p => p.STT == i).First().KetQua.ToString();
                                                            _tableCell.ForeColor = System.Drawing.Color.Red;
                                                            _tableCell.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                            _tableCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                                        }
                                                        else if (kq >= tsden)
                                                        {
                                                            _tableCell.Text = qhh.Where(p => p.STT == i).First().KetQua.ToString();
                                                            _tableCell.ForeColor = System.Drawing.Color.Red;
                                                            _tableCell.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                            _tableCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                                        }
                                                    }
                                                    else if (qhh.Where(p => p.STT == i).First().TSnTu != null && qhh.Where(p => p.STT == i).First().TSnDen == null)//trị số nhỏ nhất
                                                    {
                                                        double tstu = Convert.ToDouble(qhh.Where(p => p.STT == i).First().TSnTu);
                                                        double kq = Convert.ToDouble(qhh.Where(p => p.STT == i).First().KetQua);
                                                        if (tstu <= kq)
                                                        {
                                                            _tableCell.Text = qhh.Where(p => p.STT == i).First().KetQua.ToString();
                                                        }
                                                        else
                                                        {
                                                            _tableCell.Text = qhh.Where(p => p.STT == i).First().KetQua.ToString();
                                                            _tableCell.ForeColor = System.Drawing.Color.Red;
                                                            _tableCell.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                            _tableCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                                        }
                                                    }
                                                    else if (qhh.Where(p => p.STT == i).First().TSnTu == null && qhh.Where(p => p.STT == i).First().TSnDen != null)//giá trị lớn nhất
                                                    {
                                                        double tsden = Convert.ToDouble(qhh.Where(p => p.STT == i).First().TSnDen);
                                                        double kq = Convert.ToDouble(qhh.Where(p => p.STT == i).First().KetQua);
                                                        if (kq <= tsden)
                                                        {
                                                            _tableCell.Text = qhh.Where(p => p.STT == i).First().KetQua.ToString();
                                                        }
                                                        else
                                                        {
                                                            _tableCell.Text = qhh.Where(p => p.STT == i).First().KetQua.ToString();
                                                            _tableCell.ForeColor = System.Drawing.Color.Red;
                                                            _tableCell.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                            _tableCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                                        }
                                                    }
                                                }
                                                //double tstu = Convert.ToDouble(qhh.Where(p => p.STT == i).First().TSnTu);
                                                //double tsden = Convert.ToDouble(qhh.Where(p => p.STT == i).First().TSnDen);
                                                //double kq = Convert.ToDouble(qhh.Where(p => p.STT == i).First().KetQua);
                                                //if (tstu <= kq && kq <= tsden)
                                                //    _tableCell.Text = qhh.Where(p => p.STT == i).First().KetQua.ToString();
                                                //else
                                                //{
                                                //    _tableCell.Text = qhh.Where(p => p.STT == i).First().KetQua.ToString();
                                                //    _tableCell.ForeColor = System.Drawing.Color.Black;
                                                //    _tableCell.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                //}
                                            }
                                        }
                                        catch
                                        {
                                            _tableCell.Text = qhh.Where(p => p.STT == i).First().KetQua.ToString();
                                        }
                                    }
                                    //_tableCell.Text = qhh.Where(p => p.STT == i).First().KetQua;
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
                        else
                        {
                            for (int i = 1; i < 28; i++)//colKQ14
                            {
                                if (_tableCell.Name == "colKQ" + (i).ToString() || _tableCell.Name == "colkq" + (i).ToString())
                                {

                                    if (qhh.Where(p => p.STT == i && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == i).First().KetQua != null)
                                    {
                                        try
                                        {
                                            if (Gtinh == 0)
                                            {
                                                if (qhh.Where(p => p.STT == i).First().TSnuTu == null && qhh.Where(p => p.STT == i).First().TSnuDen == null)
                                                {
                                                    _tableCell.Text = qhh.Where(p => p.STT == i).First().KetQua.ToString();
                                                }
                                                else
                                                {
                                                    if (qhh.Where(p => p.STT == i).First().TSnuTu != null && qhh.Where(p => p.STT == i).First().TSnuDen != null)
                                                    {
                                                        double tstu = Convert.ToDouble(qhh.Where(p => p.STT == i).First().TSnuTu);
                                                        double tsden = Convert.ToDouble(qhh.Where(p => p.STT == i).First().TSnuDen);
                                                        double kq = Convert.ToDouble(qhh.Where(p => p.STT == i).First().KetQua);
                                                        if (tstu <= kq && kq <= tsden)
                                                        {
                                                            _tableCell.Text = qhh.Where(p => p.STT == i).First().KetQua.ToString();
                                                        }
                                                        else if (kq <= tstu)
                                                        {
                                                            _tableCell.Text = qhh.Where(p => p.STT == i).First().KetQua.ToString();
                                                            _tableCell.ForeColor = System.Drawing.Color.Red;
                                                            _tableCell.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                            _tableCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                                        }
                                                        else if (kq >= tsden)
                                                        {
                                                            _tableCell.Text = qhh.Where(p => p.STT == i).First().KetQua.ToString();
                                                            _tableCell.ForeColor = System.Drawing.Color.Red;
                                                            _tableCell.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                            _tableCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                                        }
                                                    }
                                                    else if (qhh.Where(p => p.STT == i).First().TSnuTu != null && qhh.Where(p => p.STT == i).First().TSnuDen == null)//trị số nhỏ nhất
                                                    {
                                                        double tstu = Convert.ToDouble(qhh.Where(p => p.STT == i).First().TSnuTu);
                                                        double kq = Convert.ToDouble(qhh.Where(p => p.STT == i).First().KetQua);
                                                        if (tstu <= kq)
                                                        {
                                                            _tableCell.Text = qhh.Where(p => p.STT == i).First().KetQua.ToString();
                                                        }
                                                        else
                                                        {
                                                            _tableCell.Text = qhh.Where(p => p.STT == i).First().KetQua.ToString();
                                                            _tableCell.ForeColor = System.Drawing.Color.Red;
                                                            _tableCell.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                            _tableCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                                        }
                                                    }
                                                    else if (qhh.Where(p => p.STT == i).First().TSnuTu == null && qhh.Where(p => p.STT == i).First().TSnuDen != null)//giá trị lớn nhất
                                                    {
                                                        double tsden = Convert.ToDouble(qhh.Where(p => p.STT == i).First().TSnuDen);
                                                        double kq = Convert.ToDouble(qhh.Where(p => p.STT == i).First().KetQua);
                                                        if (kq <= tsden)
                                                        {
                                                            _tableCell.Text = qhh.Where(p => p.STT == i).First().KetQua.ToString();
                                                        }
                                                        else
                                                        {
                                                            _tableCell.Text = qhh.Where(p => p.STT == i).First().KetQua.ToString();
                                                            _tableCell.ForeColor = System.Drawing.Color.Red;
                                                            _tableCell.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                            _tableCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                                        }
                                                    }
                                                }
                                                //double tstu = Convert.ToDouble(qhh.Where(p => p.STT == i).First().TSnuTu);
                                                //double tsden = Convert.ToDouble(qhh.Where(p => p.STT == i).First().TSnuDen);
                                                //double kq = Convert.ToDouble(qhh.Where(p => p.STT == i).First().KetQua);
                                                //if (tstu <= kq && kq <= tsden)
                                                //    _tableCell.Text = qhh.Where(p => p.STT == i).First().KetQua.ToString();
                                                //else
                                                //{
                                                //    _tableCell.Text = qhh.Where(p => p.STT == i).First().KetQua.ToString();
                                                //    _tableCell.ForeColor = System.Drawing.Color.Black;
                                                //    _tableCell.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                //}
                                            }
                                            else
                                            {
                                                if (qhh.Where(p => p.STT == i).First().TSnTu == null && qhh.Where(p => p.STT == i).First().TSnDen == null)
                                                {
                                                    _tableCell.Text = qhh.Where(p => p.STT == i).First().KetQua.ToString();
                                                }
                                                else
                                                {
                                                    if (qhh.Where(p => p.STT == i).First().TSnTu != null && qhh.Where(p => p.STT == i).First().TSnDen != null)
                                                    {
                                                        double tstu = Convert.ToDouble(qhh.Where(p => p.STT == i).First().TSnTu);
                                                        double tsden = Convert.ToDouble(qhh.Where(p => p.STT == i).First().TSnDen);
                                                        double kq = Convert.ToDouble(qhh.Where(p => p.STT == i).First().KetQua);
                                                        if (tstu <= kq && kq <= tsden)
                                                        {
                                                            _tableCell.Text = qhh.Where(p => p.STT == i).First().KetQua.ToString();
                                                        }
                                                        else if (kq <= tstu)
                                                        {
                                                            _tableCell.Text = qhh.Where(p => p.STT == i).First().KetQua.ToString();
                                                            _tableCell.ForeColor = System.Drawing.Color.Red;
                                                            _tableCell.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                            _tableCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                                        }
                                                        else if (kq >= tsden)
                                                        {
                                                            _tableCell.Text = qhh.Where(p => p.STT == i).First().KetQua.ToString();
                                                            _tableCell.ForeColor = System.Drawing.Color.Red;
                                                            _tableCell.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                            _tableCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                                        }
                                                    }
                                                    else if (qhh.Where(p => p.STT == i).First().TSnTu != null && qhh.Where(p => p.STT == i).First().TSnDen == null)//trị số nhỏ nhất
                                                    {
                                                        double tstu = Convert.ToDouble(qhh.Where(p => p.STT == i).First().TSnTu);
                                                        double kq = Convert.ToDouble(qhh.Where(p => p.STT == i).First().KetQua);
                                                        if (tstu <= kq)
                                                        {
                                                            _tableCell.Text = qhh.Where(p => p.STT == i).First().KetQua.ToString();
                                                        }
                                                        else
                                                        {
                                                            _tableCell.Text = qhh.Where(p => p.STT == i).First().KetQua.ToString();
                                                            _tableCell.ForeColor = System.Drawing.Color.Red;
                                                            _tableCell.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                            _tableCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                                        }
                                                    }
                                                    else if (qhh.Where(p => p.STT == i).First().TSnTu == null && qhh.Where(p => p.STT == i).First().TSnDen != null)//giá trị lớn nhất
                                                    {
                                                        double tsden = Convert.ToDouble(qhh.Where(p => p.STT == i).First().TSnDen);
                                                        double kq = Convert.ToDouble(qhh.Where(p => p.STT == i).First().KetQua);
                                                        if (kq <= tsden)
                                                        {
                                                            _tableCell.Text = qhh.Where(p => p.STT == i).First().KetQua.ToString();
                                                        }
                                                        else
                                                        {
                                                            _tableCell.Text = qhh.Where(p => p.STT == i).First().KetQua.ToString();
                                                            _tableCell.ForeColor = System.Drawing.Color.Red;
                                                            _tableCell.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                            _tableCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                                        }
                                                    }
                                                }
                                                //double tstu = Convert.ToDouble(qhh.Where(p => p.STT == i).First().TSnTu);
                                                //double tsden = Convert.ToDouble(qhh.Where(p => p.STT == i).First().TSnDen);
                                                //double kq = Convert.ToDouble(qhh.Where(p => p.STT == i).First().KetQua);
                                                //if (tstu <= kq && kq <= tsden)
                                                //    _tableCell.Text = qhh.Where(p => p.STT == i).First().KetQua.ToString();
                                                //else
                                                //{
                                                //    _tableCell.Text = qhh.Where(p => p.STT == i).First().KetQua.ToString();
                                                //    _tableCell.ForeColor = System.Drawing.Color.Black;
                                                //    _tableCell.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                //}
                                            }
                                        }
                                        catch
                                        {
                                            _tableCell.Text = qhh.Where(p => p.STT == i).First().KetQua.ToString();
                                        }
                                    }
                                    //_tableCell.Text = qhh.Where(p => p.STT == i).First().KetQua;
                                }
                                if (i > 24)
                                {
                                    if (_tableCell.Name == "labT" + (i).ToString())
                                    {
                                        if(DungChung.Bien.MaBV == "30372")
                                        {
                                            if (qhh.Where(p => p.STT == i && _lMaDV.Contains(p.MaDV)).Count() > 0 && qhh.Where(p => p.STT == i).First().MaDVct != null)
                                                _tableCell.Text = "X";
                                        }
                                        else if (qhh.Where(p => p.STT == i).Count() > 0 && qhh.Where(p => p.STT == i).First().MaDVct != null)
                                            _tableCell.Text = "X";
                                    }
                                }
                            }
                        }
                        
                    }

                }

                if (qhh.Where(p => p.STT == 24 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 24).FirstOrDefault().MaDVct != null)
                {
                    labT24.Text = "X";
                    lab46.Visible = true;
                    if (DungChung.Bien.MaBV == "24009")
                        lab46.Text = "giây...";
                }
                if (qhh.Where(p => p.STT == 24 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 24).FirstOrDefault().KetQua != null)
                {
                    colkq24.Text = qhh.Where(p => p.STT == 24).First().KetQua.ToString();
                }
                if (qhh.Where(p => p.STT == 25 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 25).FirstOrDefault().MaDVct != null)
                {

                    labT25.Text = "X";
                    lab48.Visible = true;
                    if (DungChung.Bien.MaBV == "24009")
                        lab48.Text = "giây...";
                }
                if (qhh.Where(p => p.STT == 25 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 25).FirstOrDefault().KetQua != null)
                {
                    colkq25.Text = qhh.Where(p => p.STT == 25).First().KetQua.ToString();
                }

                if (qhh.Where(p => p.STT == 26 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 26).FirstOrDefault().MaDVct != null)
                {
                    labT26.Text = "X";

                }
                if (qhh.Where(p => p.STT == 26 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 26).FirstOrDefault().KetQua != null)
                {
                    colkq26.Text = qhh.Where(p => p.STT == 26).First().KetQua.ToString();
                }

                if (qhh.Where(p => p.STT == 27 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 27).FirstOrDefault().MaDVct != null)
                {
                    labT27.Text = "X";
                }
                if (qhh.Where(p => p.STT == 27 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 27).FirstOrDefault().KetQua != null)
                {
                    colkq27.Text = qhh.Where(p => p.STT == 27).First().KetQua.ToString();
                }

            }
            if (DungChung.Bien.MaBV == "04011")
            {
                tab2.Visible = false;
            }

        }
        //
        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV != "12345" && DungChung.Bien.MaBV != "24297")
            {
                xrLabel127.Visible = false;
                xrLabel128.Visible = false;
                xrLabel129.Visible = false;
                xrLabel130.Visible = false;
                xrLabel131.Visible = false;
                xrLabel132.Visible = false;
                xrLabel133.Visible = false;
                xrLabel103.Visible = false;
            }
            //if (DungChung.Bien.MaBV == "12122")
            //{
            //    colTenBSDT.Visible = false;
            //    lab54.Visible = false;
            //    lab52.Visible = false;
            //}
            if (DungChung.Bien.MaBV == "26007")
            {
                txtbarcode.Visible = false;
                xrBarCode1.Visible = true;
                colTenBSDT.Visible = false;
            }
            if (DungChung.Bien.MaBV == "04011" || DungChung.Bien.MaBV == "08602" || DungChung.Bien.MaBV == "30009")
            {
                colSo.Visible = false;
                colTenTKXN.Visible = true;
            }
            else
            {
                colSo.Visible = false;

            }
            if (DungChung.Bien.MaBV == "27001")
            {
                SubBand1.Visible = false;
                SubBand2.Visible = false;
                SubBand3.Visible = false;
                SubBand4.Visible = true;
            }
            if (DungChung.Bien.MaBV == "27023" || DungChung.Bien.MaBV == "20001")
            { colSo.Visible = true; }
            xrLabel114.Text = xrLabel75.Text = xrLabel16.Text = colTenCQCQ.Text = colTenCQCQ24272.Text = DungChung.Bien.TenCQCQ.ToUpper();
            xrLabel113.Text = xrLabel74.Text = xrLabel17.Text = colTenCQ.Text = colTenCQ24272.Text = DungChung.Bien.TenCQ.ToUpper();

            if (DungChung.Bien.MaBV == "27183")
            {
                xrPictureBox3.Visible = true;
                colTenCQCQ.Visible = false;
                colTenCQ.Visible = false;
                xrLabel3.Visible = false;
            }
            if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "20001" || DungChung.Bien.MaBV == "01049")
            {
                xrLabel3.Visible = false;
            }
            if (DungChung.Bien.MaBV == "20001")
            {
                SubBand4.Visible = true;
                SubBand3.Visible = false;
                SubBand2.Visible = false;
                SubBand1.Visible = false;
                xrLine2.Visible = true;
                xrLabel114.Font = new Font("Times New Roman", 10f, FontStyle.Regular);
            }
            if (DungChung.Bien.MaBV == "30004")
            {
                lab48.Text = "";
            }

            if (DungChung.Bien.MaBV == "24272")
            {
                SubBand1.Visible = false;
                SubBand2.Visible = false;
                SubBand3.Visible = false;
                SubBand4.Visible = false;
                SubBand5.Visible = true;
                xrPictureBox6.Visible = true;
                xrPictureBox6.Image = DungChung.Ham.GetLogo();
            }

            if (DungChung.Bien.MaBV == "30372")
            {
                SubBand1.Visible = false;
                SubBand2.Visible = true;
                xrPictureBox2.Visible = false;
                xrPictureBox7.Visible = true;
            }
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "27001") 
            {
                lab54.Visible = false;
                lab52.Visible = false;
                colTenBSDT.Visible = false;
            }    

            if (DungChung.Bien.MaBV == "04012")
                lab55.Text = "Trưởng khoa cận lâm sàng".ToUpper();
            if (DungChung.Bien.MaBV == "30005" || DungChung.Bien.MaBV == "27023" || DungChung.Bien.MaBV == "20001" || DungChung.Bien.MaBV == "24009" || DungChung.Bien.MaBV == "30012")
                lab55.Text = "TRƯỞNG KHOA XÉT NGHIỆM";

            if (MaCBDT.Value != null)
            {
                if (DungChung.Bien.MaBV == "27023")
                    colTenBSDT.Text = "Họ tên: " + DungChung.Ham._getTenCB(DataContect, MaCBDT.Value.ToString());
                else
                    colTenBSDT.Text = DungChung.Ham._getTenCB(DataContect, MaCBDT.Value.ToString());
            }

            if (Macb.Value != null)
            {
                if (DungChung.Bien.MaBV == "30005" || DungChung.Bien.MaBV == "20001" || DungChung.Bien.MaBV == "24009")
                {
                    hotenck.Value = colTenTKXN.Text = DungChung.Ham._getTenCB(DataContect, Macb.Value.ToString());

                }

                else if (DungChung.Bien.MaBV == "27183")
                {
                    //var canBo = DataContect.CanBoes.FirstOrDefault(p => p.MaCB == Macb.Value);
                    //if (canBo != null)
                    //{
                    //    colTenTKXN.Text = canBo.CapBac + ". " + canBo.TenCB;
                    //    colTenTKXN.Visible = true;
                    //}
                }
                else
                {
                    var tencb = DataContect.CanBoes.Where(p => p.MaCB == Macb.Value).Select(p => p.TenCB).FirstOrDefault();
                    var chucVu = DataContect.CanBoes.Where(p => p.MaCB == Macb.Value).Select(p => p.CapBac).FirstOrDefault();
                    if (tencb != null && tencb != "")
                    {
                        string tenbs = tencb;
                        if (DungChung.Bien.MaBV == "12001")
                            tenbs = "BS: " + tenbs;
                        else if (DungChung.Bien.MaBV == "27001")
                            colTenTKXN.Text = tencb;
                        else if (DungChung.Bien.MaBV == "26007")
                            colTenTKXN.Text = chucVu + ": " + tencb;
                        else if (DungChung.Bien.MaBV != "27023")
                        {
                            tenbs = "Người thực hiện: " + tenbs;
                        }
                        else
                            colTenTKXN.Text = "Họ tên: " + tenbs;
                    }
                }
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
            if (DungChung.Bien.MaBV == "27001")
            {
                lab54.Text = "Y BÁC SỸ";
                lab55.Text = "PHÒNG XÉT NGHIỆM";
            }
        }

        private void lab44_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "04005")
            {
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

        private void repPhieuXNHuyetHoc_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "12122")
            {
                IEnumerable allLable = this.AllControls<XRLabel>();
                foreach (XRLabel a in allLable)
                {
                    a.ForeColor = Color.Black;
                }
                IEnumerable allCel = this.AllControls<XRTableCell>();
                foreach (XRTableCell a in allCel)
                {
                    a.ForeColor = Color.Black;
                    a.BorderColor = Color.Black;
                }

            }
            if (DungChung.Bien.MaBV == "12128")
            {
                GroupFooter1.Visible = true;
                ReportFooter.Visible = false;
            }
        }

        private void xrSubreport1_BeforePrint(object sender, CancelEventArgs e)
        {
            rep_PhieuXN_Sub repsub = (rep_PhieuXN_Sub)xrSubreport1.ReportSource;
            repsub.NGAYKIXN.Value = NgayTH.Value.ToString();
            repsub.NGAYKYDT.Value = NgayCD.Value.ToString();
            repsub.BSDT.Value = DungChung.Ham._getTenCB(DataContect, MaCBDT.Value.ToString());
            repsub.TKXN.Value = DungChung.Ham._getTenCB(DataContect, Macb.Value.ToString());
            repsub.tb12128.ForeColor = System.Drawing.Color.Red;
        }
    }
}
