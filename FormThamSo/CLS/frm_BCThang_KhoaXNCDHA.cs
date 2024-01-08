using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace QLBV.FormThamSo
{
    public partial class frm_BCThang_KhoaXNCDHA : DevExpress.XtraEditors.XtraForm
    {
        public frm_BCThang_KhoaXNCDHA()
        {
            InitializeComponent();
        }

        private void frm_BCThang_KhoaXNCDHA_Load(object sender, EventArgs e)
        {
            lupNgaytu.DateTime = DateTime.Now;
            lupngayden.DateTime = DateTime.Now;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            DateTime tungay = DungChung.Ham.NgayTu(lupNgaytu.DateTime);
            DateTime dengay = DungChung.Ham.NgayDen(lupngayden.DateTime);
            frmIn frm = new frmIn();
            BaoCao.rep_BCThang_KhoaXNCDHA rep = new BaoCao.rep_BCThang_KhoaXNCDHA();

            var qdv = (from tn in data.TieuNhomDVs.Where(p => p.IDNhom == 1 || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm)
                       join dv in data.DichVus on tn.IdTieuNhom equals dv.IdTieuNhom
                       select new { dv.MaDV, tn.TenRG, tn.IDNhom, dv.SoTT, dv.TenDV }).ToList();

            var qcls = (from cls in data.CLS.Where(p => p.NgayTH >= tungay && p.NgayTH <= dengay)
                        join bn in data.BenhNhans on cls.MaBNhan equals bn.MaBNhan
                        join cd in data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                        //join kp in data.KPhongs on cls.MaKP equals kp.MaKP
                        select new
                        {
                            bn.NoiTru,
                            bn.MaBNhan,
                            bn.DTuong,
                            cd.MaDV,
                            cd.IDCD,
                            cls.IdCLS,
                            cls.MaKP
                            //kp.MaKP,
                            //kp.PLoai,
                            //kp.TenKP
                        }).ToList();

            var qkphong = data.KPhongs.ToList();
            var qcls0 = (from cls in qcls
                         join dv in qdv on cls.MaDV equals dv.MaDV
                         join kp in qkphong on cls.MaKP equals kp.MaKP
                         select new
                         {
                             dv.MaDV,
                             dv.TenDV,
                             dv.TenRG,
                             dv.IDNhom,
                             dv.SoTT,
                             cls.NoiTru,
                             cls.MaBNhan,
                             cls.DTuong,
                             cls.IdCLS,
                             cls.IDCD,
                             kp.MaKP,
                             kp.PLoai,
                             kp.TenKP
                         }).ToList();
            #region xét nghiệm
            var qtsxn = qcls0.Where(p => p.IDNhom == 1).ToList();
            rep.celXN_T.Text = (qtsxn.Count > 0) ? (qtsxn.Count.ToString()) : "";
            rep.celXN_NT.Text = (qtsxn.Where(p => p.NoiTru == 1).Count() > 0) ? (qtsxn.Where(p => p.NoiTru == 1).Count().ToString()) : "";
            rep.celXN_ngt.Text = (qtsxn.Where(p => p.NoiTru == 0).Count() > 0) ? (qtsxn.Where(p => p.NoiTru == 0).Count().ToString()) : "";

            var qHuyetHoc = qtsxn.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).ToList();
            rep.XNHuyetHoc_TS.Text = (qHuyetHoc.Count > 0) ? (qHuyetHoc.Count.ToString()) : "";
            rep.XNHuyetHoc_NT.Text = (qHuyetHoc.Where(p => p.NoiTru == 1).Count() > 0) ? (qHuyetHoc.Where(p => p.NoiTru == 1).Count().ToString()) : "";
            rep.XNHuyetHoc_NgT.Text = (qHuyetHoc.Where(p => p.NoiTru == 0).Count() > 0) ? (qHuyetHoc.Where(p => p.NoiTru == 0).Count().ToString()) : "";

            var qSinhHoaMau = qtsxn.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau).ToList();
            rep.XNSinhHoaMau_TS.Text = (qSinhHoaMau.Count > 0) ? (qSinhHoaMau.Count.ToString()) : "";
            rep.XNSinhHoaMau_nt.Text = (qSinhHoaMau.Where(p => p.NoiTru == 1).Count() > 0) ? (qSinhHoaMau.Where(p => p.NoiTru == 1).Count().ToString()) : "";
            rep.XNSinhHoaMau_ngt.Text = (qSinhHoaMau.Where(p => p.NoiTru == 0).Count() > 0) ? (qSinhHoaMau.Where(p => p.NoiTru == 0).Count().ToString()) : "";

            var qXNNuocTieu = qtsxn.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).ToList();
            rep.XNNuocTieu_TS.Text = (qXNNuocTieu.Count > 0) ? (qXNNuocTieu.Count.ToString()) : "";
            rep.XNNuocTieu_nt.Text = (qXNNuocTieu.Where(p => p.NoiTru == 1).Count() > 0) ? (qXNNuocTieu.Where(p => p.NoiTru == 1).Count().ToString()) : "";
            rep.XNNuocTieu_ngt.Text = (qXNNuocTieu.Where(p => p.NoiTru == 0).Count() > 0) ? (qXNNuocTieu.Where(p => p.NoiTru == 0).Count().ToString()) : "";

            //var qXNViSinh = qtsxn.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNViSinh).ToList();
            //var qXNViSinh_Lao = qXNViSinh.Where(p => p.SoTT == 1).ToList();
            var qXNViSinh_Lao = (from cls in qtsxn.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNDom)
                                 join ttbx in data.TTboXungs.Where(p => p.ChanDoanLao != null) on cls.MaBNhan equals ttbx.MaBNhan
                                 select new
                                 {
                                     cls.MaDV,
                                     cls.TenDV,
                                     cls.TenRG,
                                     cls.IDNhom,
                                     cls.SoTT,
                                     cls.IdCLS,
                                     cls.IDCD,
                                     cls.NoiTru,
                                     cls.MaBNhan,
                                     ttbx.ThangTheoDoi,
                                 }).ToList();
            if (qXNViSinh_Lao != null && qXNViSinh_Lao.Count > 0)
            {
                var qxnlaoTS = (from a in qXNViSinh_Lao select new { a.MaBNhan, a.NoiTru }).Distinct().ToList();
                var qXNAFB = (from cls in qXNViSinh_Lao.Where(p => p.SoTT == 1 || p.SoTT == 2)
                              join clsct in data.CLScts on cls.IDCD equals clsct.IDCD //.Where(p=>p.KetQua == "1+" || p.KetQua == "2+" ||p.KetQua == "3+")
                              select new { cls.MaBNhan, cls.ThangTheoDoi, cls.TenDV, cls.MaDV, cls.IDCD, clsct.KetQua }).ToList();

                {
                    rep.celLao_TS.Text = (qxnlaoTS.Count > 0) ? (qxnlaoTS.Count().ToString()) : "";
                    rep.celLao_nt.Text = (qxnlaoTS.Where(p => p.NoiTru == 1).Count() > 0) ? (qxnlaoTS.Where(p => p.NoiTru == 1).Count().ToString()) : "";
                    rep.celLao_ngt.Text = (qxnlaoTS.Where(p => p.NoiTru == 0).Count() > 0) ? (qxnlaoTS.Where(p => p.NoiTru == 0).Count().ToString()) : "";
                    rep.celLao_XN.Text = qxnlaoTS.Select(p => p.MaBNhan).Distinct().Count().ToString();
                    rep.celLao_PH.Text = qXNAFB.Where(p => p.KetQua == "1+" || p.KetQua == "2+" || p.KetQua == "3+").Select(p => p.MaBNhan).Distinct().Count().ToString();
                    rep.celLao_PH_AFB.Text = rep.celLao_PH.Text;
                    rep.celLao_TD.Text = qXNAFB.Where(p => p.ThangTheoDoi != null && p.ThangTheoDoi > 0).Where(p => p.KetQua == "1+" || p.KetQua == "2+" || p.KetQua == "3+").Select(p => p.MaBNhan).Distinct().Count().ToString();
                    rep.celLao_TD_AFB.Text = rep.celLao_TD.Text;
                }
            }

            var qnhuomsoi = qtsxn.Where(p => p.TenRG == "XN khác").Where(p => p.TenDV.Contains("nhuộm soi")).ToList();
            rep.NhuomSoi_TS.Text = (qnhuomsoi.Count > 0) ? (qnhuomsoi.Count.ToString()) : "";
            rep.NhuomSoi_nt.Text = (qnhuomsoi.Where(p => p.NoiTru == 1).Count() > 0) ? (qnhuomsoi.Where(p => p.NoiTru == 1).Count().ToString()) : "";
            rep.NhuomSoi_ngt.Text = (qnhuomsoi.Where(p => p.NoiTru == 0).Count() > 0) ? (qnhuomsoi.Where(p => p.NoiTru == 0).Count().ToString()) : "";

            var qxnPhan = qtsxn.Where(p => p.TenRG == "XN khác").Where(p => p.TenDV.Contains("trong phân")).ToList();
            rep.cePhan_TS.Text = (qxnPhan.Count > 0) ? (qxnPhan.Count.ToString()) : "";
            rep.cePhan_nt.Text = (qxnPhan.Where(p => p.NoiTru == 1).Count() > 0) ? (qxnPhan.Where(p => p.NoiTru == 1).Count().ToString()) : "";
            rep.cePhan_ngt.Text = (qxnPhan.Where(p => p.NoiTru == 0).Count() > 0) ? (qxnPhan.Where(p => p.NoiTru == 0).Count().ToString()) : "";

            var qxnSotRet = qtsxn.Where(p => p.TenRG == "XN khác").Where(p => p.TenDV.Contains("sốt rét") && !p.TenDV.Contains("nhuộm soi")).ToList();
            rep.celSotRet_TS.Text = (qxnSotRet.Count > 0) ? (qxnSotRet.Count.ToString()) : "";
            rep.celSotRet_nt.Text = (qxnSotRet.Where(p => p.NoiTru == 1).Count() > 0) ? (qxnSotRet.Where(p => p.NoiTru == 1).Count().ToString()) : "";
            rep.celSotRet_ngt.Text = (qxnSotRet.Where(p => p.NoiTru == 0).Count() > 0) ? (qxnSotRet.Where(p => p.NoiTru == 0).Count().ToString()) : "";


            #region HIV
            var qHIV = qtsxn.Where(p => p.TenRG == "XN khác").Where(p => p.TenDV.Contains("HIV")).ToList();
            var qkp = (from kp in qHIV
                       group kp by new { kp.MaKP, kp.TenKP, kp.PLoai } into kq
                       select new { kq.Key.MaKP, kq.Key.TenKP, kq.Key.PLoai }).ToList();
            var qkp_LS = qkp.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.LamSang).OrderBy(p => p.TenKP).ToList();
            var qkp_PK = qkp.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.PhongKham).OrderBy(p => p.TenKP).ToList();
            int num = 1;
            rep.celHIV.Text = (qHIV.Count > 0) ? (qHIV.Count.ToString()) : "";
            rep.celHIV_nt.Text = (qHIV.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.LamSang).Count() > 0) ? (qHIV.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.LamSang).Count().ToString()) : "";
            rep.celHIV_ngt.Text = (qHIV.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.PhongKham).Count() > 0) ? (qHIV.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.PhongKham).Count().ToString()) : "";
            foreach (var a in qkp_LS)
            {
                switch (num)
                {
                    case 1:
                        rep.celHIV_nt1.Text = (qHIV.Where(p => p.MaKP == a.MaKP).Count() > 0) ? (qHIV.Where(p => p.MaKP == a.MaKP).Count().ToString()) : "";
                        rep.celHIV_ntT1.Text = a.TenKP;
                        break;
                    case 2:
                        rep.celHIV_nt2.Text = (qHIV.Where(p => p.MaKP == a.MaKP).Count() > 0) ? (qHIV.Where(p => p.MaKP == a.MaKP).Count().ToString()) : "";
                        rep.celHIV_ntT2.Text = a.TenKP;
                        break;
                    case 3:
                        rep.celHIV_nt3.Text = (qHIV.Where(p => p.MaKP == a.MaKP).Count() > 0) ? (qHIV.Where(p => p.MaKP == a.MaKP).Count().ToString()) : "";
                        rep.celHIV_ntT3.Text = a.TenKP;
                        break;
                    case 4:
                        rep.celHIV_nt4.Text = (qHIV.Where(p => p.MaKP == a.MaKP).Count() > 0) ? (qHIV.Where(p => p.MaKP == a.MaKP).Count().ToString()) : "";
                        rep.celHIV_ntT4.Text = a.TenKP;
                        break;
                    case 5:
                        rep.celHIV_nt5.Text = (qHIV.Where(p => p.MaKP == a.MaKP).Count() > 0) ? (qHIV.Where(p => p.MaKP == a.MaKP).Count().ToString()) : "";
                        rep.celHIV_ntT5.Text = a.TenKP;
                        break;
                    case 6:
                        rep.celHIV_nt6.Text = (qHIV.Where(p => p.MaKP == a.MaKP).Count() > 0) ? (qHIV.Where(p => p.MaKP == a.MaKP).Count().ToString()) : "";
                        rep.celHIV_ntT6.Text = a.TenKP;
                        break;
                    case 7:
                        rep.celHIV_nt7.Text = (qHIV.Where(p => p.MaKP == a.MaKP).Count() > 0) ? (qHIV.Where(p => p.MaKP == a.MaKP).Count().ToString()) : "";
                        rep.celHIV_ntT7.Text = a.TenKP;
                        break;
                    case 8:
                        rep.celHIV_nt8.Text = (qHIV.Where(p => p.MaKP == a.MaKP).Count() > 0) ? (qHIV.Where(p => p.MaKP == a.MaKP).Count().ToString()) : "";
                        rep.celHIV_ntT8.Text = a.TenKP;
                        break;
                    case 9:
                        rep.celHIV_nt9.Text = (qHIV.Where(p => p.MaKP == a.MaKP).Count() > 0) ? (qHIV.Where(p => p.MaKP == a.MaKP).Count().ToString()) : "";
                        rep.celHIV_ntT9.Text = a.TenKP;
                        break;
                    case 10:
                        rep.celHIV_nt10.Text = (qHIV.Where(p => p.MaKP == a.MaKP).Count() > 0) ? (qHIV.Where(p => p.MaKP == a.MaKP).Count().ToString()) : "";
                        rep.celHIV_ntT10.Text = a.TenKP;
                        break;
                }
                num++;
            }
            num = 1;
            foreach (var a in qkp_PK)
            {
                switch (num)
                {
                    case 1:
                        rep.celHIV_ngt1.Text = (qHIV.Where(p => p.MaKP == a.MaKP).Count() > 0) ? (qHIV.Where(p => p.MaKP == a.MaKP).Count().ToString()) : "";
                        rep.celHIV_ngtT1.Text = a.TenKP;
                        break;
                    case 2:
                        rep.celHIV_ngt2.Text = (qHIV.Where(p => p.MaKP == a.MaKP).Count() > 0) ? (qHIV.Where(p => p.MaKP == a.MaKP).Count().ToString()) : "";
                        rep.celHIV_ngtT2.Text = a.TenKP;
                        break;
                    case 3:
                        rep.celHIV_ngt3.Text = (qHIV.Where(p => p.MaKP == a.MaKP).Count() > 0) ? (qHIV.Where(p => p.MaKP == a.MaKP).Count().ToString()) : "";
                        rep.celHIV_ngtT3.Text = a.TenKP;
                        break;
                    case 4:
                        rep.celHIV_ngt4.Text = (qHIV.Where(p => p.MaKP == a.MaKP).Count() > 0) ? (qHIV.Where(p => p.MaKP == a.MaKP).Count().ToString()) : "";
                        rep.celHIV_ngtT4.Text = a.TenKP;
                        break;
                    case 5:
                        rep.celHIV_ngt5.Text = (qHIV.Where(p => p.MaKP == a.MaKP).Count() > 0) ? (qHIV.Where(p => p.MaKP == a.MaKP).Count().ToString()) : "";
                        rep.celHIV_ngtT5.Text = a.TenKP;
                        break;
                    case 6:
                        rep.celHIV_ngt6.Text = (qHIV.Where(p => p.MaKP == a.MaKP).Count() > 0) ? (qHIV.Where(p => p.MaKP == a.MaKP).Count().ToString()) : "";
                        rep.celHIV_ngtT6.Text = a.TenKP;
                        break;
                    case 7:
                        rep.celHIV_ngt7.Text = (qHIV.Where(p => p.MaKP == a.MaKP).Count() > 0) ? (qHIV.Where(p => p.MaKP == a.MaKP).Count().ToString()) : "";
                        rep.celHIV_ngtT7.Text = a.TenKP;
                        break;
                    case 8:
                        rep.celHIV_ngt8.Text = (qHIV.Where(p => p.MaKP == a.MaKP).Count() > 0) ? (qHIV.Where(p => p.MaKP == a.MaKP).Count().ToString()) : "";
                        rep.celHIV_ngtT8.Text = a.TenKP;
                        break;
                    case 9:
                        rep.celHIV_ngt9.Text = (qHIV.Where(p => p.MaKP == a.MaKP).Count() > 0) ? (qHIV.Where(p => p.MaKP == a.MaKP).Count().ToString()) : "";
                        rep.celHIV_ngtT9.Text = a.TenKP;
                        break;
                    case 10:
                        rep.celHIV_ngt10.Text = (qHIV.Where(p => p.MaKP == a.MaKP).Count() > 0) ? (qHIV.Where(p => p.MaKP == a.MaKP).Count().ToString()) : "";
                        rep.celHIV_ngtT10.Text = a.TenKP;
                        break;
                }
                num++;
            }
            #endregion
            var qxntruyenmau = qtsxn.Where(p => p.TenRG == "XN khác").Where(p => p.TenDV.Contains("truyền máu")).ToList();
            rep.celTruyenMAu.Text = (qxntruyenmau.Count > 0) ? (qxntruyenmau.Count.ToString()) : "";
            #endregion

            #region xquang
            var qxq = qcls0.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang).ToList();
            rep.celxq1.Text = (qxq.Select(p => p.MaBNhan).Distinct().Count() > 0) ? (qxq.Select(p => p.MaBNhan).Distinct().Count().ToString()) : "";
            rep.celxq2.Text = (qxq.Count() > 0) ? (qxq.Count().ToString()) : "";
            rep.celxq3.Text = (qxq.Where(p => p.DTuong != "BHYT").Select(p => p.MaBNhan).Distinct().Count() > 0) ? (qxq.Where(p => p.DTuong != "BHYT").Select(p => p.MaBNhan).Distinct().Count().ToString()) : "";
            rep.celxq4.Text = (qxq.Where(p => p.DTuong != "BHYT").Count() > 0) ? (qxq.Where(p => p.DTuong != "BHYT").Count().ToString()) : "";
            rep.celxq5.Text = (qxq.Where(p => p.DTuong != "BHYT").Where(p => p.NoiTru == 1).Count() > 0) ? (qxq.Where(p => p.DTuong != "BHYT").Where(p => p.NoiTru == 1).Count().ToString()) : "";
            rep.celxq6.Text = (qxq.Where(p => p.DTuong != "BHYT").Where(p => p.NoiTru == 0).Count() > 0) ? (qxq.Where(p => p.DTuong != "BHYT").Where(p => p.NoiTru == 0).Count().ToString()) : "";
            rep.celxq7.Text = (qxq.Where(p => p.DTuong == "BHYT").Select(p => p.MaBNhan).Distinct().Count() > 0) ? (qxq.Where(p => p.DTuong == "BHYT").Select(p => p.MaBNhan).Distinct().Count().ToString()) : "";
            rep.celxq8.Text = (qxq.Where(p => p.DTuong == "BHYT").Count() > 0) ? (qxq.Where(p => p.DTuong == "BHYT").Count().ToString()) : "";
            rep.celxq9.Text = (qxq.Where(p => p.DTuong == "BHYT").Where(p => p.NoiTru == 1).Count() > 0) ? (qxq.Where(p => p.DTuong == "BHYT").Where(p => p.NoiTru == 1).Count().ToString()) : "";
            rep.celxq10.Text = (qxq.Where(p => p.DTuong == "BHYT").Where(p => p.NoiTru == 0).Count() > 0) ? (qxq.Where(p => p.DTuong == "BHYT").Where(p => p.NoiTru == 0).Count().ToString()) : "";
            #endregion

            #region sieuam
            var qsieuam = qcls0.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm).ToList();
            rep.celSA1.Text = (qsieuam.Select(p => p.MaBNhan).Distinct().Count() > 0) ? (qsieuam.Select(p => p.MaBNhan).Distinct().Count().ToString()) : "";
            rep.celSA2.Text = (qsieuam.Where(p => p.DTuong == "BHYT").Select(p => p.MaBNhan).Distinct().Count() > 0) ? (qsieuam.Where(p => p.DTuong == "BHYT").Select(p => p.MaBNhan).Distinct().Count().ToString()) : "";
            rep.celSA3.Text = (qsieuam.Where(p => p.DTuong == "BHYT").Where(p => p.NoiTru == 1).Select(p => p.MaBNhan).Distinct().Count() > 0) ? (qsieuam.Where(p => p.DTuong == "BHYT").Where(p => p.NoiTru == 1).Select(p => p.MaBNhan).Distinct().Count().ToString()) : "";
            rep.celSA4.Text = (qsieuam.Where(p => p.DTuong == "BHYT").Where(p => p.NoiTru == 0).Select(p => p.MaBNhan).Distinct().Count() > 0) ? (qsieuam.Where(p => p.DTuong == "BHYT").Where(p => p.NoiTru == 0).Select(p => p.MaBNhan).Distinct().Count().ToString()) : "";
            rep.celSA5.Text = (qsieuam.Where(p => p.DTuong != "BHYT").Select(p => p.MaBNhan).Distinct().Count() > 0) ? (qsieuam.Where(p => p.DTuong != "BHYT").Select(p => p.MaBNhan).Distinct().Count().ToString()) : "";
            rep.celSA6.Text = (qsieuam.Where(p => p.DTuong != "BHYT").Where(p => p.NoiTru == 1).Select(p => p.MaBNhan).Distinct().Count() > 0) ? (qsieuam.Where(p => p.DTuong != "BHYT").Where(p => p.NoiTru == 1).Select(p => p.MaBNhan).Distinct().Count().ToString()) : "";
            rep.celSA7.Text = (qsieuam.Where(p => p.DTuong != "BHYT").Where(p => p.NoiTru == 0).Select(p => p.MaBNhan).Distinct().Count() > 0) ? (qsieuam.Where(p => p.DTuong != "BHYT").Where(p => p.NoiTru == 0).Select(p => p.MaBNhan).Distinct().Count().ToString()) : "";
            #endregion

            rep.CreateDocument();
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();
        }
    }
}