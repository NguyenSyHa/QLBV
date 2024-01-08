using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class Rep_Bke_New_2018TK_InDoc_34019 : DevExpress.XtraReports.UI.XtraReport
    {
        int _idper = 0, _mabn = 0;
        public Rep_Bke_New_2018TK_InDoc_34019(int idper, int mabn)
        {
            InitializeComponent();
            _idper = idper;
            _mabn = mabn;
        }
        public void Bindingdata()
        {
            celTenDV.DataBindings.Add("Text", DataSource, "TenDV");
            celDonVi.DataBindings.Add("Text", DataSource, "DonVi");
            celSoLuong.DataBindings.Add("Text", DataSource, "SoLuong").FormatString = DungChung.Bien.FormatString[0];
            celDonGiaBV.DataBindings.Add("Text", DataSource, "DonGiaBV").FormatString = DungChung.Bien.FormatString[1];
            celDonGiaBH.DataBindings.Add("Text", DataSource, "DonGiaBH").FormatString = DungChung.Bien.FormatString[1];
            celTyLeDV.DataBindings.Add("Text", DataSource, "TyLeTTDV");

            celThanhTienBV.DataBindings.Add("Text", DataSource, "ThanhTienBV").FormatString = DungChung.Bien.FormatString[1];
            celThanhTianBV_T.DataBindings.Add("Text", DataSource, "ThanhTienBV");
            celThanhTianBV_T.Summary.FormatString = DungChung.Bien.FormatString[1];
            celTTBV_G2.DataBindings.Add("Text", DataSource, "ThanhTienBV");
            celTTBV_G2.Summary.FormatString = DungChung.Bien.FormatString[1];
            celTTBV_G1.DataBindings.Add("Text", DataSource, "ThanhTienBV");
            celTTBV_G1.Summary.FormatString = DungChung.Bien.FormatString[1];

            celThanhTienBH.DataBindings.Add("Text", DataSource, "ThanhTienBH").FormatString = DungChung.Bien.FormatString[1];
            celThanhTienBH_T.DataBindings.Add("Text", DataSource, "ThanhTienBH");
            celThanhTienBH_T.Summary.FormatString = DungChung.Bien.FormatString[1];
            celTTBH_G.DataBindings.Add("Text", DataSource, "ThanhTienBH");
            celTTBH_G.Summary.FormatString = DungChung.Bien.FormatString[1];
            celTTBH_G1.DataBindings.Add("Text", DataSource, "ThanhTienBH");
            celTTBH_G1.Summary.FormatString = DungChung.Bien.FormatString[1];

            celTyLeBH.DataBindings.Add("Text", DataSource, "TyLeTTBH");
            celTienBH.DataBindings.Add("Text", DataSource, "TienBH").FormatString = DungChung.Bien.FormatString[1];
            celTienBH_T.DataBindings.Add("Text", DataSource, "TienBH");
            celTienBH_T.Summary.FormatString = DungChung.Bien.FormatString[1];
            celTienBH_G.DataBindings.Add("Text", DataSource, "TienBH");
            celTienBH_G.Summary.FormatString = DungChung.Bien.FormatString[1];
            celTienBH_G1.DataBindings.Add("Text", DataSource, "TienBH");
            celTienBH_G1.Summary.FormatString = DungChung.Bien.FormatString[1];

            celTienBN.DataBindings.Add("Text", DataSource, "TienBN").FormatString = DungChung.Bien.FormatString[1];
            celTienBN_T.DataBindings.Add("Text", DataSource, "TienBN");
            celTienBN_T.Summary.FormatString = DungChung.Bien.FormatString[1];
            celTienBN_G.DataBindings.Add("Text", DataSource, "TienBN");
            celTienBN_G.Summary.FormatString = DungChung.Bien.FormatString[1];
            celTienBN_G1.DataBindings.Add("Text", DataSource, "TienBN");
            celTienBN_G1.Summary.FormatString = DungChung.Bien.FormatString[1];

            celTienNGDM.DataBindings.Add("Text", DataSource, "TienNgBH").FormatString = DungChung.Bien.FormatString[1];
            celTienNGDM_T.DataBindings.Add("Text", DataSource, "TienNgBH");
            celTienNGDM_T.Summary.FormatString = DungChung.Bien.FormatString[1];
            celTienNGDM_G.DataBindings.Add("Text", DataSource, "TienNgBH");
            celTienNGDM_G.Summary.FormatString = DungChung.Bien.FormatString[1];
            celTienNGDM_G1.DataBindings.Add("Text", DataSource, "TienNgBH");
            celTienNGDM_G1.Summary.FormatString = DungChung.Bien.FormatString[1];

            coltienkhac.DataBindings.Add("Text", DataSource, "TienKhac").FormatString = DungChung.Bien.FormatString[1];
            coltienkhac_R.DataBindings.Add("Text", DataSource, "TienKhac");
            coltienkhac_R.Summary.FormatString = DungChung.Bien.FormatString[1];
            coltienkhac_G.DataBindings.Add("Text", DataSource, "TienKhac");
            coltienkhac_G.Summary.FormatString = DungChung.Bien.FormatString[1];
            coltienkhac_G1.DataBindings.Add("Text", DataSource, "TienKhac");
            coltienkhac_G1.Summary.FormatString = DungChung.Bien.FormatString[1];

            celNhom.DataBindings.Add("Text", DataSource, "TenNhom");
            celTieuNhom.DataBindings.Add("Text", DataSource, "TenTieuNhom");

            GroupHeader2.GroupFields.Add(new GroupField("STTNhom"));
            GroupHeader1.GroupFields.Add(new GroupField("STTTieuN"));
        }
        int a = 1;
        private void GroupHeader2_BeforePrint(object sender, CancelEventArgs e)
        {
            if (this.GetCurrentColumnValue("STTNhom") != null)
            {
                string n = GetCurrentColumnValue("STTNhom").ToString();
                if (n == "2" || n == "11")
                {
                    GroupHeader1.Visible = true;
                    celTienBH_G.Text = "";
                    celTTBV_G2.Text = "";
                    celTTBH_G.Text = "";
                    celTienBN_G.Text = "";
                    celTienNGDM_G.Text = "";
                    //DevExpress.XtraReports.UI.XRSummary xrSummary1 = new DevExpress.XtraReports.UI.XRSummary();
                    //xrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.None;
                    //this.celTienBH_G.Summary = xrSummary1;

                    //DevExpress.XtraReports.UI.XRSummary xrSummary2 = new DevExpress.XtraReports.UI.XRSummary();
                    //xrSummary2.Running = DevExpress.XtraReports.UI.SummaryRunning.None;
                    //this.celTTBV_G2.Summary = xrSummary2;

                    //DevExpress.XtraReports.UI.XRSummary xrSummary3 = new DevExpress.XtraReports.UI.XRSummary();
                    //xrSummary3.Running = DevExpress.XtraReports.UI.SummaryRunning.None;
                    //this.celTTBH_G.Summary = xrSummary3;

                    //DevExpress.XtraReports.UI.XRSummary xrSummary4 = new DevExpress.XtraReports.UI.XRSummary();
                    //xrSummary4.Running = DevExpress.XtraReports.UI.SummaryRunning.None;
                    //this.celTienBN_G.Summary = xrSummary4;

                    //DevExpress.XtraReports.UI.XRSummary xrSummary5 = new DevExpress.XtraReports.UI.XRSummary();
                    //xrSummary5.Running = DevExpress.XtraReports.UI.SummaryRunning.None;
                    //this.ceTienDV_G.Summary = xrSummary5;
                }
                else if (n == "5")
                {
                    GroupHeader1.Visible = false;
                    Detail.Visible = false;
                    celTienBH_G.Text = "";
                    celTTBV_G2.Text = "";
                    celTTBH_G.Text = "";
                    celTienBN_G.Text = "";
                    celTienNGDM_G.Text = "";
                }
                else
                {
                    GroupHeader1.Visible = false;
                    Detail.Visible = true;
                    //DevExpress.XtraReports.UI.XRSummary xrSummary1 = new DevExpress.XtraReports.UI.XRSummary();
                    //xrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
                    //this.celTienBH_G.Summary = xrSummary1;

                    //DevExpress.XtraReports.UI.XRSummary xrSummary2 = new DevExpress.XtraReports.UI.XRSummary();
                    //xrSummary2.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
                    //this.celTTBV_G2.Summary = xrSummary2;

                    //DevExpress.XtraReports.UI.XRSummary xrSummary3 = new DevExpress.XtraReports.UI.XRSummary();
                    //xrSummary3.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
                    //this.celTTBH_G.Summary = xrSummary3;

                    //DevExpress.XtraReports.UI.XRSummary xrSummary4 = new DevExpress.XtraReports.UI.XRSummary();
                    //xrSummary4.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
                    //this.celTienBN_G.Summary = xrSummary4;

                    //DevExpress.XtraReports.UI.XRSummary xrSummary5 = new DevExpress.XtraReports.UI.XRSummary();
                    //xrSummary5.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
                    //this.ceTienDV_G.Summary = xrSummary5;
                }

            }
            //sttNhom.Text = a.ToString() + ". ";
            //a++;
            //b = 1;
        }
        int b = 1;
        private void stttn_BeforePrint(object sender, CancelEventArgs e)
        {
            //stttn.Text = a.ToString() + "." + b.ToString() + ". ";
            //b++;
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            txtNguoiLap.Text = DungChung.Bien.NguoiLapBieu;
            if (DungChung.Bien.MaBV == "30003")
                xrTable18.Visible = false;
            if (_idper > 0)
            {
                xrSubreport1.Visible = true;
            }
            xrPictureBox1.Image = DungChung.Ham.GetLogo();
            if (DungChung.Bien.MaBV == "24272")
            {
                SubBand1.Visible = false;
                SubBand2.Visible = true;
            }
        }

        private void TopMargin_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "27023")
                TopMargin.HeightF = 95F;
               
        }

        private void Rep_Bke_New_2018TK_InDoc_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "27023")
                TopMargin.HeightF = 95F;
        }

        private void xrSubreport1_BeforePrint(object sender, CancelEventArgs e)
        {
            _InSubReport(xrSubreport1);
        }
        private void _InSubReport(XRSubreport repsub)
        {
            QLBV_Database.QLBVEntities _dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            BaoCao.rep_BkeNew_sub_InDoc rep = new BaoCao.rep_BkeNew_sub_InDoc();
            repsub.ReportSource = rep;
            var _Benhnhan = _dataContext.BenhNhans.Where(p => p.MaBNhan == _mabn).FirstOrDefault();
            var _lrv = _dataContext.RaViens.Where(p => p.MaBNhan == _mabn).FirstOrDefault();
            var _lvp = _dataContext.VienPhis.Where(p => p.MaBNhan == _mabn).FirstOrDefault();
            var dichvu = (from dv in _dataContext.DichVus
                          join tn in _dataContext.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                          join nhomdv in _dataContext.NhomDVs on tn.IDNhom equals nhomdv.IDNhom
                          select new { dv.SoTT, dv.TrongDM, dv.MaDV, dv.DonGia, dv.DonGia2, dv.DonGiaBHYT, dv.DonGiaDV2, dv.GiaBHGioiHanTT, TenRGTN = tn.TenRG, dv.TenHC, dv.HamLuong, dv.MaQD, dv.SoQD, nhomdv.TenNhom, nhomdv.TenNhomCT, nhomdv.STT, dv.TenDV, dv.DonVi, dv.TenRG, nhomdv.IDNhom }).ToList();
            var _ldvbn1 = (from vp in _dataContext.VienPhis.Where(p => p.MaBNhan == _mabn)
                           join vpct in _dataContext.VienPhicts.Where(p => (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01071") ? (p.TrongBH == 1) : (p.TrongBH != 2 && p.TrongBH != 3)).Where(p => p.ThanhToan == 0).Where(p => p.AttachIDDonct == null) on vp.idVPhi equals vpct.idVPhi
                           select new
                           {
                               vp.MaBNhan,
                               vp.NgayTT,
                               vpct.MaDV,
                               vpct.TrongBH,
                               vpct.DonVi,
                               vpct.DonGia,
                               vpct.TyLeTT,
                               vpct.TyLeBHTT,
                               vpct.IDPerson,
                               vpct.SoLuong,
                               vpct.ThanhTien,
                               vpct.TBNCTT,
                               vpct.TBNTT,
                               vpct.TienBH
                           }).ToList();
            var _ldvbn = (from vp in _ldvbn1.Where(p => p.IDPerson == _idper)
                          //    _dataContext.VienPhis.Where(p => p.MaBNhan == _mabn)
                          //join vpct in _dataContext.VienPhicts.Where(p => (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01071") ? (p.TrongBH == 1) : (p.TrongBH != 2 && p.TrongBH != 3)).Where(p => p.ThanhToan == 0).Where(p => p.AttachIDDonct == null) on vp.idVPhi equals vpct.idVPhi
                          group new { vp } by new { vp.MaBNhan, vp.NgayTT, vp.MaDV, vp.TrongBH, vp.DonVi, vp.DonGia, vp.TyLeTT, vp.TyLeBHTT, vp.IDPerson } into kq
                          select new
                          {
                              kq.Key.MaBNhan,
                              kq.Key.NgayTT,
                              kq.Key.MaDV,
                              kq.Key.TrongBH,
                              kq.Key.DonVi,
                              kq.Key.DonGia,
                              kq.Key.TyLeTT,
                              kq.Key.TyLeBHTT,
                              kq.Key.IDPerson,
                              SoLuong = kq.Sum(p => p.vp.SoLuong),
                              ThanhTien = kq.Sum(p => p.vp.ThanhTien),
                              TBNCTT = kq.Sum(p => p.vp.TBNCTT),
                              TBNTT = kq.Sum(p => p.vp.TBNTT),
                              TienBH = kq.Sum(p => p.vp.TienBH)
                          }).ToList();
            int dungtuyen = 0;
            string _muc = "";
            if (_Benhnhan.Tuyen != null)
                dungtuyen = int.Parse(_Benhnhan.Tuyen.ToString());
            double tongcp1 = _ldvbn1.Count() > 0 ? _ldvbn1.Sum(p => p.ThanhTien) : 0;
            if (_lvp.NgayTachCP != null)
                rep.CPKCB.Value = "Chi phí KCB tính từ ngày " + _lvp.NgayTachCP.Value.AddDays(1).ToShortDateString() + " đến ngày " + _lrv.NgayRa.Value.ToShortDateString();
            var sthe = _dataContext.People.Where(p => p.IDPerson == _idper).FirstOrDefault();
            if (sthe != null)
            {
                if (!string.IsNullOrEmpty(sthe.SThe.ToString()) && sthe.SThe.Length >= 15)
                {
                    rep.THE1.Value = sthe.SThe.Substring(0, 2);
                    rep.THE2.Value = sthe.SThe.Substring(2, 1);
                    rep.THE3.Value = sthe.SThe.Substring(3, 2);
                    rep.THE4.Value = sthe.SThe.Substring(5);
                    _muc = sthe.SThe.Substring(2, 1);
                }
                rep.GIOIHANTHE.Value = "Giá trị từ " + sthe.HanBHTu.Value.ToShortDateString() + " đến " + sthe.HanBHDen.Value.ToShortDateString();
            }
            int _hangbv = DungChung.Ham.hangBV(DungChung.Bien.MaBV);
            if (dungtuyen == 1)
            {
                //if (capcuu != 1)
                //    rep.DUNGTUYEN.Value = "X";
                if (_hangbv == 4 || tongcp1 < DungChung.Bien.GHanTT100)
                    rep.MUCHUONG.Value = "100%";
                else
                    rep.MUCHUONG.Value = DungChung.Ham._PtramTT(_dataContext, _muc) + "%";
            }
            else if (dungtuyen == 2)
            {
                string kvuc = "";
                if (_Benhnhan.KhuVuc != null)
                    kvuc = _Benhnhan.KhuVuc;
                string muchuong = "";
                switch (_hangbv)
                {
                    case 1:
                        muchuong = DungChung.Ham._PtramTT(_dataContext, _muc) * 0.4 + "%";
                        break;
                    case 2:
                        if (DungChung.Bien.MaBV == "01830")
                            muchuong = DungChung.Ham._PtramTT(_dataContext, _muc) * 0.7 + "%";
                        else
                            muchuong = DungChung.Ham._PtramTT(_dataContext, _muc) * 0.6 + "%";
                        break;
                    case 3:
                        if (kvuc.ToLower().Contains("k"))
                            muchuong = DungChung.Ham._PtramTT(_dataContext, _muc) + "%";
                        else
                            muchuong = DungChung.Ham._PtramTT(_dataContext, _muc) * 0.7 + "%";
                        break;
                    case 4:
                        muchuong = DungChung.Ham._PtramTT(_dataContext, _muc) + "%";
                        break;
                }
                rep.MUCHUONG.Value = muchuong;
            }


            List<C_BKe_New> _lkq = new List<C_BKe_New>();
            List<KPhong> _lkp = _dataContext.KPhongs.ToList();
            //Công khám
            var _lcongkham = (from vp in _ldvbn
                              join dv in dichvu.Where(p => p.IDNhom == 13) on vp.MaDV equals dv.MaDV
                              select new { vp, dv.TenNhom, dv.DonGia, dv.DonGia2, dv.DonGiaBHYT, dv.DonGiaDV2, dv.TenDV }).ToList();
            if (_lcongkham.Count > 0)
            {
                foreach (var item in _lcongkham)
                {
                    C_BKe_New moi = new C_BKe_New();
                    moi.STTNhom = 1;
                    moi.TenNhom = "1. Khám bệnh";
                    moi.STTTieuN = 1;
                    moi.TenTieuNhom = item.TenNhom;
                    moi.TenDV = item.TenDV;
                    moi.SoLuong = item.vp.SoLuong;
                    moi.DonVi = item.vp.DonVi;
                    if (item.vp.TrongBH == 1)
                    {

                        moi.DonGiaBV = item.vp.DonGia;// DonGiaBV;
                        moi.DonGiaBH = item.vp.DonGia;
                        moi.ThanhTienBV = Math.Round(item.vp.DonGia * item.vp.SoLuong, DungChung.Bien.LamTronSo); //DonGiaBV * item.vp.SoLuong * item.vp.TyLeTT / 100;
                        moi.ThanhTienBH = Math.Round(item.vp.DonGia * item.vp.SoLuong * item.vp.TyLeTT / 100, DungChung.Bien.LamTronSo);
                        moi.TienBH = item.vp.TienBH;
                        moi.TienBN = item.vp.TBNCTT;
                        moi.TienNgBH = item.vp.TBNTT;
                        moi.TyLeTTBH = 100;
                    }
                    else
                    {
                        moi.DonGiaBV = item.vp.DonGia;
                        moi.ThanhTienBV = item.vp.ThanhTien;
                        moi.TyLeTTBH = 0;
                        moi.TienBH = item.vp.TienBH;
                        moi.TienBN = item.vp.TBNCTT;
                        moi.TienNgBH = item.vp.TBNTT;
                    }
                    moi.TyLeTTDV = item.vp.TyLeTT;
                    _lkq.Add(moi);
                }
            }
            //
            //Ngày giường
            var _lngaygiuong = (from vp in _ldvbn
                                join dv in dichvu.Where(p => p.IDNhom == 15 || p.IDNhom == 14) on vp.MaDV equals dv.MaDV
                                select new { vp, dv.TenNhom, dv.DonGia, dv.DonGia2, dv.DonGiaBHYT, dv.DonGiaDV2, dv.TenDV, dv.IDNhom }).ToList();
            if (_lngaygiuong.Count > 0)
            {
                foreach (var item in _lngaygiuong)
                {
                    C_BKe_New moi = new C_BKe_New();
                    moi.STTNhom = 2;
                    moi.TenNhom = "2. Ngày giường";
                    moi.STTTieuN = 1;
                    moi.TenTieuNhom = (_hangbv == 4 || DungChung.Bien.MaBV == "30280" || DungChung.Bien.MaBV == "30303") ? "2.3 Ngày giường lưu:" : (_Benhnhan.NoiTru == 1 ? "2.2. Ngày giường điều trị nội trú:" : "2.1 Ngày giường lưu");
                    moi.TenDV = item.TenDV;
                    moi.SoLuong = item.vp.SoLuong;
                    moi.DonVi = item.vp.DonVi;
                    if (item.vp.TrongBH == 1)
                    {
                        moi.DonGiaBV = item.vp.DonGia;// DonGiaBV;
                        moi.DonGiaBH = item.vp.DonGia;
                        moi.ThanhTienBV = Math.Round(item.vp.DonGia * item.vp.SoLuong, DungChung.Bien.LamTronSo); //DonGiaBV * item.vp.SoLuong * item.vp.TyLeTT / 100;
                        moi.ThanhTienBH = Math.Round(item.vp.DonGia * item.vp.SoLuong * item.vp.TyLeTT / 100, DungChung.Bien.LamTronSo);
                        moi.TienBH = item.vp.TienBH;
                        moi.TienBN = item.vp.TBNCTT;
                        moi.TienNgBH = item.vp.TBNTT;
                        moi.TyLeTTBH = 100;
                    }
                    else
                    {
                        moi.DonGiaBV = item.vp.DonGia;
                        moi.ThanhTienBV = item.vp.ThanhTien;
                        moi.TyLeTTBH = 0;
                        moi.TienBH = item.vp.TienBH;
                        moi.TienBN = item.vp.TBNCTT;
                        moi.TienNgBH = item.vp.TBNTT;
                    }
                    moi.TyLeTTDV = item.vp.TyLeTT;
                    _lkq.Add(moi);
                }
            }
            //

            //Xét nghiệm
            var _lxetnghiem = (from vp in _ldvbn
                               join dv in dichvu.Where(p => p.IDNhom == 1) on vp.MaDV equals dv.MaDV
                               select new { vp, dv.TenNhom, dv.DonGia, dv.DonGia2, dv.DonGiaBHYT, dv.DonGiaDV2, dv.TenDV }).ToList();
            if (_lxetnghiem.Count > 0)
            {
                foreach (var item in _lxetnghiem)
                {
                    C_BKe_New moi = new C_BKe_New();
                    moi.STTNhom = 3;
                    moi.TenNhom = "3. Xét nghiệm";
                    moi.STTTieuN = 1;
                    moi.TenTieuNhom = item.TenNhom;
                    moi.TenDV = item.TenDV;
                    moi.SoLuong = item.vp.SoLuong;
                    moi.DonVi = item.vp.DonVi;
                    if (item.vp.TrongBH == 1 || item.vp.TrongBH == 2)
                    {

                        moi.DonGiaBV = item.vp.DonGia;// DonGiaBV;
                        moi.DonGiaBH = item.vp.DonGia;
                        moi.ThanhTienBV = item.vp.DonGia * item.vp.SoLuong; //DonGiaBV * item.vp.SoLuong * item.vp.TyLeTT / 100;
                        moi.ThanhTienBH = item.vp.DonGia * item.vp.SoLuong;
                        moi.TienBH = item.vp.TienBH;
                        moi.TienBN = item.vp.TBNCTT;
                        moi.TienNgBH = item.vp.TBNTT;
                        moi.TyLeTTBH = 100;
                    }
                    else
                    {
                        moi.DonGiaBV = item.vp.DonGia;
                        moi.ThanhTienBV = item.vp.ThanhTien;
                        moi.TyLeTTBH = 0;
                        moi.TienBH = item.vp.TienBH;
                        moi.TienBN = item.vp.TBNCTT;
                        moi.TienNgBH = item.vp.TBNTT;
                    }
                    moi.TyLeTTDV = item.vp.TyLeTT;
                    _lkq.Add(moi);
                }
            }
            //
            //CĐHA
            var _lcdha = (from vp in _ldvbn
                          join dv in dichvu.Where(p => p.IDNhom == 2) on vp.MaDV equals dv.MaDV
                          select new { vp, dv.TenNhom, dv.DonGia, dv.DonGia2, dv.DonGiaBHYT, dv.DonGiaDV2, dv.TenDV }).ToList();
            if (_lcdha.Count > 0)
            {
                foreach (var item in _lcdha)
                {
                    C_BKe_New moi = new C_BKe_New();
                    moi.STTNhom = 4;
                    moi.TenNhom = "4. Chẩn đoán hình ảnh";
                    moi.STTTieuN = 1;
                    moi.TenTieuNhom = item.TenNhom;
                    moi.TenDV = item.TenDV;
                    moi.SoLuong = item.vp.SoLuong;
                    moi.DonVi = item.vp.DonVi;
                    if (item.vp.TrongBH == 1 || item.vp.TrongBH == 2)
                    {
                        moi.DonGiaBV = item.vp.DonGia;// DonGiaBV;
                        moi.DonGiaBH = item.vp.DonGia;
                        moi.ThanhTienBV = item.vp.DonGia * item.vp.SoLuong; //DonGiaBV * item.vp.SoLuong * item.vp.TyLeTT / 100;
                        moi.ThanhTienBH = item.vp.DonGia * item.vp.SoLuong;
                        moi.TienBH = item.vp.TienBH;
                        moi.TienBN = item.vp.TBNCTT;
                        moi.TienNgBH = item.vp.TBNTT;
                        moi.TyLeTTBH = 100;
                    }
                    else
                    {
                        moi.DonGiaBV = item.vp.DonGia;
                        moi.ThanhTienBV = item.vp.ThanhTien;
                        moi.TyLeTTBH = 0;
                        moi.TienBH = item.vp.TienBH;
                        moi.TienBN = item.vp.TBNCTT;
                        moi.TienNgBH = item.vp.TBNTT;
                    }
                    moi.TyLeTTDV = item.vp.TyLeTT;
                    _lkq.Add(moi);
                }

            }
            //
            //thăm dò chức năng
            var _ltdcn = (from vp in _ldvbn
                          join dv in dichvu.Where(p => p.IDNhom == 3) on vp.MaDV equals dv.MaDV
                          select new { vp, dv.TenNhom, dv.DonGia, dv.DonGia2, dv.DonGiaBHYT, dv.DonGiaDV2, dv.TenDV }).ToList();
            if (_ltdcn.Count > 0)
            {
                foreach (var item in _ltdcn)
                {
                    C_BKe_New moi = new C_BKe_New();
                    moi.STTNhom = 6;
                    moi.TenNhom = "5. Thăm dò chức năng";
                    moi.STTTieuN = 1;
                    moi.TenTieuNhom = item.TenNhom;
                    moi.TenDV = item.TenDV;
                    moi.SoLuong = item.vp.SoLuong;
                    moi.DonVi = item.vp.DonVi;
                    if (item.vp.TrongBH == 1 || item.vp.TrongBH == 2)
                    {
                        moi.DonGiaBV = item.vp.DonGia;// DonGiaBV;
                        moi.DonGiaBH = item.vp.DonGia;
                        moi.ThanhTienBV = item.vp.DonGia * item.vp.SoLuong; //DonGiaBV * item.vp.SoLuong * item.vp.TyLeTT / 100;
                        moi.ThanhTienBH = item.vp.DonGia * item.vp.SoLuong;
                        moi.TienBH = item.vp.TienBH;
                        moi.TienBN = item.vp.TBNCTT;
                        moi.TienNgBH = item.vp.TBNTT;
                        moi.TyLeTTBH = 100;
                    }
                    else
                    {
                        moi.DonGiaBV = item.vp.DonGia;
                        moi.ThanhTienBV = item.vp.ThanhTien;
                        moi.TyLeTTBH = 0;
                        moi.TienBH = item.vp.TienBH;
                        moi.TienBN = item.vp.TBNCTT;
                        moi.TienNgBH = item.vp.TBNTT;
                    }
                    moi.TyLeTTDV = item.vp.TyLeTT;
                    _lkq.Add(moi);
                }
            }
            //
            //thủ thuật, phẫu thuật
            var _lttpt = (from vp in _ldvbn
                          join dv in dichvu.Where(p => p.IDNhom == 8) on vp.MaDV equals dv.MaDV
                          select new { vp, dv.TenNhom, dv.DonGia, dv.DonGia2, dv.DonGiaBHYT, dv.DonGiaDV2, dv.TenDV }).ToList();
            if (_lttpt.Count > 0)
            {
                foreach (var item in _lttpt)
                {
                    C_BKe_New moi = new C_BKe_New();
                    moi.STTNhom = 7;
                    moi.TenNhom = "6. Thủ thuật, phẫu thuật";
                    moi.STTTieuN = 1;
                    moi.TenTieuNhom = item.TenNhom;
                    moi.TenDV = item.TenDV;
                    moi.SoLuong = item.vp.SoLuong;
                    moi.DonVi = item.vp.DonVi;
                    if (item.vp.TrongBH == 1 || item.vp.TrongBH == 2)
                    {
                        moi.DonGiaBV = item.vp.DonGia;// DonGiaBV;
                        moi.DonGiaBH = item.vp.DonGia;
                        moi.ThanhTienBV = Math.Round(item.vp.DonGia * item.vp.SoLuong, DungChung.Bien.LamTronSo); //DonGiaBV * item.vp.SoLuong * item.vp.TyLeTT / 100;
                        moi.ThanhTienBH = Math.Round(item.vp.DonGia * item.vp.SoLuong * item.vp.TyLeTT / 100, DungChung.Bien.LamTronSo);
                        moi.TienBH = item.vp.TienBH;
                        moi.TienBN = item.vp.TBNCTT;
                        moi.TienNgBH = item.vp.TBNTT;
                        moi.TyLeTTBH = 100;
                    }
                    else
                    {
                        moi.DonGiaBV = item.vp.DonGia;
                        moi.ThanhTienBV = item.vp.ThanhTien;
                        moi.TyLeTTBH = 0;
                        moi.TienBH = item.vp.TienBH;
                        moi.TienBN = item.vp.TBNCTT;
                        moi.TienNgBH = item.vp.TBNTT;
                    }
                    moi.TyLeTTDV = item.vp.TyLeTT;
                    _lkq.Add(moi);
                }
            }
            //
            //máu
            var _lmau = (from vp in _ldvbn
                         join dv in dichvu.Where(p => p.IDNhom == 7) on vp.MaDV equals dv.MaDV
                         select new { vp, dv.TenNhom, dv.DonGia, dv.DonGia2, dv.DonGiaBHYT, dv.DonGiaDV2, dv.TenDV }).ToList();
            if (_lmau.Count > 0)
            {
                foreach (var item in _lmau)
                {
                    C_BKe_New moi = new C_BKe_New();
                    moi.STTNhom = 8;
                    moi.TenNhom = "7. Máu, chế phẩm máu, vận chuyển máu";
                    moi.STTTieuN = 1;
                    moi.TenTieuNhom = item.TenNhom;
                    moi.TenDV = item.TenDV;
                    moi.SoLuong = item.vp.SoLuong;
                    moi.DonVi = item.vp.DonVi;
                    if (item.vp.TrongBH == 1 || item.vp.TrongBH == 2)
                    {
                        moi.DonGiaBV = item.vp.DonGia;// DonGiaBV;
                        moi.DonGiaBH = item.vp.DonGia;
                        moi.ThanhTienBV = item.vp.DonGia * item.vp.SoLuong; //DonGiaBV * item.vp.SoLuong * item.vp.TyLeTT / 100;
                        moi.ThanhTienBH = item.vp.DonGia * item.vp.SoLuong;
                        moi.TienBH = item.vp.TienBH;
                        moi.TienBN = item.vp.TBNCTT;
                        moi.TienNgBH = item.vp.TBNTT;
                        moi.TyLeTTBH = 100;
                    }
                    else
                    {
                        moi.DonGiaBV = item.vp.DonGia;
                        moi.ThanhTienBV = item.vp.ThanhTien;
                        moi.TyLeTTBH = 0;
                        moi.TienBH = item.vp.TienBH;
                        moi.TienBN = item.vp.TBNCTT;
                        moi.TienNgBH = item.vp.TBNTT;
                    }
                    moi.TyLeTTDV = item.vp.TyLeTT;
                    _lkq.Add(moi);
                }
            }
            //
            //Thuốc, dịch truyền
            var _lThuoc = (from vp in _ldvbn
                           join dv in dichvu.Where(p => p.IDNhom == 5 || p.IDNhom == 6 || p.IDNhom == 4) on vp.MaDV equals dv.MaDV
                           select new { vp, dv.TenNhom, dv.DonGia, dv.DonGia2, dv.DonGiaBHYT, dv.DonGiaDV2, dv.TenDV, dv.HamLuong }).ToList();
            if (_lThuoc.Count > 0)
            {
                foreach (var item in _lThuoc)
                {
                    C_BKe_New moi = new C_BKe_New();
                    moi.STTNhom = 9;
                    moi.TenNhom = "8. Thuốc, dịch truyền";
                    moi.STTTieuN = 1;
                    moi.TenTieuNhom = item.TenNhom;
                    moi.TenDV = (item.TenDV == null ? "" : item.TenDV) + (DungChung.Bien.MaBV == "27001" ? (item.HamLuong == null ? "" : (" (" + item.HamLuong + ")")) : "");
                    moi.SoLuong = item.vp.SoLuong;
                    moi.DonVi = item.vp.DonVi;
                    if (item.vp.TrongBH == 1 || item.vp.TrongBH == 2)
                    {
                        moi.DonGiaBV = item.vp.DonGia;// DonGiaBV;
                        moi.DonGiaBH = item.vp.DonGia;
                        moi.ThanhTienBV = Math.Round(item.vp.DonGia * item.vp.SoLuong, DungChung.Bien.LamTronSo); //DonGiaBV * item.vp.SoLuong * item.vp.TyLeTT / 100;
                        moi.ThanhTienBH = Math.Round(item.vp.DonGia * item.vp.SoLuong * item.vp.TyLeTT / 100, DungChung.Bien.LamTronSo);
                        moi.TienBH = item.vp.TienBH;
                        moi.TienBN = item.vp.TBNCTT;
                        moi.TienNgBH = item.vp.TBNTT;
                        moi.TyLeTTDV = 100;
                        moi.TyLeTTBH = item.vp.TyLeTT;
                    }
                    else
                    {
                        moi.DonGiaBV = item.vp.DonGia;
                        moi.ThanhTienBV = item.vp.ThanhTien;
                        moi.TyLeTTBH = 0;
                        //moi.TienNgBH = item.vp.ThanhTien;
                        moi.TienBH = item.vp.TienBH;
                        moi.TienBN = item.vp.TBNCTT;
                        moi.TienNgBH = item.vp.TBNTT;
                        moi.TyLeTTDV = item.vp.TyLeTT;
                    }

                    _lkq.Add(moi);
                }
            }
            //
            //Vật tư y tế

            var _lvtyt = (from vp in _ldvbn
                          join dv in dichvu.Where(p => p.IDNhom == 10 || p.IDNhom == 11) on vp.MaDV equals dv.MaDV
                          select new { vp, dv.TenNhom, dv.DonGia, dv.DonGia2, dv.DonGiaBHYT, dv.DonGiaDV2, dv.TenDV, dv.MaDV, dv.GiaBHGioiHanTT }).ToList();
            if (_lvtyt.Count > 0)
            {
                foreach (var item in _lvtyt)
                {
                    C_BKe_New moi = new C_BKe_New();
                    moi.STTNhom = 10;
                    moi.TenNhom = "9. Vật tư y tế";
                    moi.STTTieuN = 1;
                    moi.TenTieuNhom = item.TenNhom;
                    moi.TenDV = item.TenDV;
                    moi.SoLuong = item.vp.SoLuong;
                    moi.DonVi = item.vp.DonVi;
                    if (item.vp.TrongBH == 1 || item.vp.TrongBH == 2)
                    {

                        moi.DonGiaBV = item.vp.DonGia;// DonGiaBV;
                        moi.DonGiaBH = item.vp.DonGia;
                        moi.ThanhTienBV = item.vp.DonGia * item.vp.SoLuong; //DonGiaBV * item.vp.SoLuong * item.vp.TyLeTT / 100;
                        moi.ThanhTienBH = item.vp.DonGia * item.vp.SoLuong;
                        moi.TienBH = item.vp.TienBH;
                        moi.TienBN = item.vp.TBNCTT;
                        moi.TienNgBH = item.vp.TBNTT;
                        moi.TyLeTTBH = 100;
                        if (DungChung.Bien.MaBV == "27022" && item.GiaBHGioiHanTT > 0 && item.GiaBHGioiHanTT == item.vp.DonGia)
                        {
                            var qdtct = (from dt in _dataContext.DThuocs.Where(p => p.MaBNhan == _mabn)
                                         join dtct in _dataContext.DThuoccts
                                         on dt.IDDon equals dtct.IDDon
                                         where (dtct.MaDV == item.MaDV && dtct.TrongBH == item.vp.TrongBH && dtct.TyLeTT == item.vp.TyLeTT)
                                         select dtct).FirstOrDefault();
                            if (qdtct != null && qdtct.DonGia > item.GiaBHGioiHanTT)
                            {
                                moi.DonGiaBV = qdtct.DonGia;
                                moi.ThanhTienBV = item.vp.ThanhTien;
                            }
                        }
                    }
                    else
                    {
                        moi.DonGiaBV = item.vp.DonGia;
                        moi.ThanhTienBV = item.vp.ThanhTien;
                        moi.TyLeTTBH = 0;
                        moi.TienBH = item.vp.TienBH;
                        moi.TienBN = item.vp.TBNCTT;
                        moi.TienNgBH = item.vp.TBNTT;
                    }
                    moi.TyLeTTDV = item.vp.TyLeTT;
                    _lkq.Add(moi);
                }
            }
            //

            // gói vật tư
            var _ldvbngoi = (from vp in _dataContext.VienPhis.Where(p => p.MaBNhan == _mabn)
                             join vpct in _dataContext.VienPhicts.Where(p => p.IDPerson == _idper).Where(p => p.TrongBH != 2).Where(p => p.ThanhToan == 0).Where(p => p.AttachIDDonct != null && p.AttachIDDonct > 0) on vp.idVPhi equals vpct.idVPhi
                             group new { vp, vpct } by new { vp.MaBNhan, vp.NgayTT, vpct.MaDV, vpct.TBNTT, vpct.TBNCTT, vpct.TrongBH, vpct.DonVi, vpct.DonGia, vpct.TyLeTT, vpct.TyLeBHTT, vpct.AttachIDDonct, vpct.IDPerson } into kq
                             select new
                             {
                                 kq.Key.MaBNhan,
                                 kq.Key.NgayTT,
                                 kq.Key.MaDV,
                                 kq.Key.TrongBH,
                                 kq.Key.DonVi,
                                 kq.Key.DonGia,
                                 kq.Key.TyLeTT,
                                 kq.Key.TyLeBHTT,
                                 kq.Key.AttachIDDonct,
                                 kq.Key.IDPerson,
                                 SoLuong = kq.Sum(p => p.vpct.SoLuong),
                                 ThanhTien = kq.Sum(p => p.vpct.ThanhTien),
                                 TBNCTT = kq.Sum(p => p.vpct.TBNCTT),
                                 TienBH = kq.Sum(p => p.vpct.TienBH),
                                 TBNTT = kq.Sum(p => p.vpct.TBNTT),
                             }).ToList();
            var _lvtytgoi = (from vp in _ldvbngoi.Where(p => p.AttachIDDonct != null && p.AttachIDDonct > 0)
                             join dv in dichvu.Where(p => p.IDNhom == 10 || p.IDNhom == 11) on vp.MaDV equals dv.MaDV
                             select new { vp, dv.TenNhom, dv.DonGia, dv.DonGia2, dv.DonGiaBHYT, dv.DonGiaDV2, dv.GiaBHGioiHanTT, dv.TenDV, vp.MaDV, vp.AttachIDDonct, vp.ThanhTien }).ToList();
            if (_lvtytgoi.Count > 0)
            {
                var dthuoc = (from dt in _dataContext.DThuocs.Where(p => p.MaBNhan == _mabn)
                              join dtct in _dataContext.DThuoccts on dt.IDDon equals dtct.IDDon
                              select new { dtct.IDDonct, dtct.MaDV, dtct.TrongBH, dtct.DonGia, dtct.TyLeTT }).ToList();
                var _lAttachIDDonct = _lvtytgoi.Select(p => p.AttachIDDonct).Distinct().ToList();
                int sttg = 1;
                foreach (var a in _lAttachIDDonct)
                {
                    var tendv0 = (from dt in dthuoc.Where(p => p.IDDonct == a)
                                  join dv in dichvu on dt.MaDV equals dv.MaDV
                                  select new { dv.TenDV, dv.MaDV, dt.TrongBH, dt.DonGia, dt.TyLeTT }).ToList();
                    var tendv = (from dt in tendv0
                                 select new { dt.TenDV }).FirstOrDefault();
                    //var tendv = (from dt in dthuoc.Where(p => p.IDDonct == a)
                    //             join dv in dichvu on dt.MaDV equals dv.MaDV
                    //             select new { dv.TenDV, dv.MaDV, dv.DonGia }).FirstOrDefault();
                    if (tendv != null)
                    {
                        string tengoi = "10." + sttg.ToString() + " Gói vật tư y tế " + sttg.ToString() + " (" + tendv.TenDV.ToString() + ")";
                        foreach (var item in _lvtytgoi.Where(p => p.AttachIDDonct == a).ToList())
                        {
                            C_BKe_New moi = new C_BKe_New();
                            moi.STTNhom = 11;
                            moi.TenNhom = "10. Gói vật tư y tế";

                            moi.STTTieuN = sttg;
                            moi.TenTieuNhom = tengoi;
                            moi.TenDV = item.TenDV;
                            moi.SoLuong = item.vp.SoLuong;
                            moi.DonVi = item.vp.DonVi;
                            if (item.vp.TrongBH == 1 || item.vp.TrongBH == 2)
                            {
                                moi.DonGiaBV = item.vp.DonGia;// DonGiaBV;
                                moi.DonGiaBH = item.vp.DonGia;
                                moi.ThanhTienBV = item.vp.DonGia * item.vp.SoLuong; //DonGiaBV * item.vp.SoLuong * item.vp.TyLeTT / 100;
                                moi.ThanhTienBH = item.vp.DonGia * item.vp.SoLuong;
                                moi.TienBH = item.vp.TienBH;
                                moi.TienBN = item.vp.TBNCTT;
                                moi.TienNgBH = item.vp.TBNTT;
                                moi.TyLeTTBH = 100;
                                if (DungChung.Bien.MaBV == "27022" && item.GiaBHGioiHanTT > 0 && item.GiaBHGioiHanTT == item.vp.DonGia)
                                {
                                    var qdtct = (from dtct in tendv0
                                                 where (dtct.MaDV == item.MaDV.Value && dtct.TrongBH == item.vp.TrongBH && dtct.TyLeTT == item.vp.TyLeTT)
                                                 select dtct).FirstOrDefault();
                                    if (qdtct != null && qdtct.DonGia > item.GiaBHGioiHanTT)
                                    {
                                        moi.DonGiaBV = qdtct.DonGia;
                                        moi.ThanhTienBV = item.ThanhTien;
                                    }

                                }
                            }
                            else
                            {
                                moi.DonGiaBV = item.vp.DonGia;
                                moi.ThanhTienBV = item.vp.ThanhTien;
                                moi.TyLeTTBH = 0;
                                moi.TienBH = item.vp.TienBH;
                                moi.TienBN = item.vp.TBNCTT;
                                moi.TienNgBH = item.vp.TBNTT;
                            }
                            moi.TyLeTTDV = item.vp.TyLeTT;
                            _lkq.Add(moi);
                        }
                        sttg++;
                    }
                }
            }
            //thuốc được kê là dịch vụ đính kèm thì lên vào nhóm thuốc
            var _thuocgoi = (from vp in _ldvbngoi.Where(p => p.AttachIDDonct != null && p.AttachIDDonct > 0)
                             join dv in dichvu.Where(p => p.IDNhom == 5 || p.IDNhom == 6 || p.IDNhom == 4) on vp.MaDV equals dv.MaDV
                             select new { vp, dv.TenNhom, dv.DonGia, dv.DonGia2, dv.DonGiaBHYT, dv.DonGiaDV2, dv.TenDV }).ToList();
            if (_thuocgoi.Count > 0)
            {
                foreach (var item in _thuocgoi)
                {
                    C_BKe_New moi = new C_BKe_New();
                    moi.STTNhom = 9;
                    moi.TenNhom = "8. Thuốc, dịch truyền";
                    moi.STTTieuN = 1;
                    moi.TenTieuNhom = item.TenNhom;
                    moi.TenDV = item.TenDV;
                    moi.SoLuong = item.vp.SoLuong;
                    moi.DonVi = item.vp.DonVi;
                    if (item.vp.TrongBH == 1)
                    {
                        moi.DonGiaBV = item.vp.DonGia;// DonGiaBV;
                        moi.DonGiaBH = item.vp.DonGia;
                        moi.ThanhTienBV = item.vp.DonGia * item.vp.SoLuong; //DonGiaBV * item.vp.SoLuong * item.vp.TyLeTT / 100;
                        moi.ThanhTienBH = item.vp.DonGia * item.vp.SoLuong;
                        moi.TienBH = item.vp.TienBH;
                        moi.TienBN = item.vp.TBNCTT;
                        moi.TienNgBH = item.vp.TBNTT;
                        moi.TyLeTTBH = 100;
                    }
                    else
                    {
                        moi.DonGiaBV = item.vp.DonGia;
                        moi.ThanhTienBV = item.vp.ThanhTien;
                        moi.TyLeTTBH = 0;
                        moi.TienBH = item.vp.TienBH;
                        moi.TienBN = item.vp.TBNCTT;
                        moi.TienNgBH = item.vp.TBNTT;
                    }
                    moi.TyLeTTDV = item.vp.TyLeTT;
                    _lkq.Add(moi);
                }
            }
            //
            //vận chuyển
            var _lvanchuyen = (from vp in _ldvbn
                               join dv in dichvu.Where(p => p.IDNhom == 12) on vp.MaDV equals dv.MaDV
                               select new { vp, dv.TenNhom, dv.DonGia, dv.DonGia2, dv.DonGiaBHYT, dv.DonGiaDV2, dv.TenDV }).ToList();
            if (_lvanchuyen.Count > 0)
            {
                foreach (var item in _lvanchuyen)
                {
                    C_BKe_New moi = new C_BKe_New();
                    moi.STTNhom = 12;
                    moi.TenNhom = "11. Vận chuyển người bệnh";
                    moi.STTTieuN = 1;
                    moi.TenTieuNhom = item.TenNhom;
                    moi.TenDV = item.TenDV;
                    moi.SoLuong = item.vp.SoLuong;
                    moi.DonVi = item.vp.DonVi;
                    if (item.vp.TrongBH == 1 || item.vp.TrongBH == 2)
                    {
                        moi.DonGiaBV = item.vp.DonGia;// DonGiaBV;
                        moi.DonGiaBH = item.vp.DonGia;
                        moi.ThanhTienBV = item.vp.DonGia * item.vp.SoLuong; //DonGiaBV * item.vp.SoLuong * item.vp.TyLeTT / 100;
                        moi.ThanhTienBH = item.vp.DonGia * item.vp.SoLuong;
                        moi.TienBH = item.vp.TienBH;
                        moi.TienBN = item.vp.TBNCTT;
                        moi.TienNgBH = item.vp.TBNTT;
                        moi.TyLeTTBH = 100;
                    }
                    else
                    {
                        moi.DonGiaBV = item.vp.DonGia;
                        moi.ThanhTienBV = item.vp.ThanhTien;
                        moi.TyLeTTBH = 0;
                        moi.TienBH = item.vp.TienBH;
                        moi.TienBN = item.vp.TBNCTT;
                        moi.TienNgBH = item.vp.TBNTT;
                    }
                    moi.TyLeTTDV = item.vp.TyLeTT;
                    _lkq.Add(moi);
                }
            }
            var _lhoachat = (from vp in _ldvbn
                             join dv in dichvu.Where(p => p.IDNhom > 17) on vp.MaDV equals dv.MaDV
                             select new { vp, dv.TenNhom, dv.DonGia, dv.DonGia2, dv.DonGiaBHYT, dv.DonGiaDV2, dv.TenDV }).ToList();
            if (_lhoachat.Count > 0)
            {
                foreach (var item in _lhoachat)
                {
                    C_BKe_New moi = new C_BKe_New();
                    moi.STTNhom = 13;
                    moi.TenNhom = "12. Khác";
                    moi.STTTieuN = 1;
                    moi.TenTieuNhom = item.TenNhom;
                    moi.TenDV = item.TenDV;
                    moi.SoLuong = item.vp.SoLuong;
                    moi.DonVi = item.vp.DonVi;
                    if (item.vp.TrongBH == 1 || item.vp.TrongBH == 2)
                    {
                        moi.DonGiaBV = item.vp.DonGia;// DonGiaBV;
                        moi.DonGiaBH = item.vp.DonGia;
                        moi.ThanhTienBV = item.vp.DonGia * item.vp.SoLuong; //DonGiaBV * item.vp.SoLuong * item.vp.TyLeTT / 100;
                        moi.ThanhTienBH = item.vp.DonGia * item.vp.SoLuong;
                        moi.TienBH = item.vp.TienBH;
                        moi.TienBN = item.vp.TBNCTT;
                        moi.TienNgBH = item.vp.TBNTT;
                        moi.TyLeTTBH = 100;
                    }
                    else
                    {
                        moi.DonGiaBV = item.vp.DonGia;
                        moi.ThanhTienBV = item.vp.ThanhTien;
                        moi.TyLeTTBH = 0;
                        moi.TienBH = item.vp.TienBH;
                        moi.TienBN = item.vp.TBNCTT;
                        moi.TienNgBH = item.vp.TBNTT;
                    }
                    moi.TyLeTTDV = item.vp.TyLeTT;
                    _lkq.Add(moi);
                }
            }
            if (_lkq.Count > 0)
            {
                rep.DataSource = _lkq;
                rep.Bindingdata();
            }
        }
        public class C_BKe_New
        {
            public int STTNhom { get; set; }
            public string TenNhom { get; set; }
            public int STTTieuN { get; set; }
            public string TenTieuNhom { get; set; }
            public string TenDV { get; set; }
            public string DonVi { get; set; }
            public double SoLuong { get; set; }
            public double DonGiaBV { get; set; }
            public double DonGiaBH { get; set; }
            public double TyLeTTDV { get; set; }
            public double ThanhTienBV { get; set; }
            public double ThanhTienBH { get; set; }
            public double TyLeTTBH { get; set; }
            public double TienBH { get; set; }
            public double TienBN { get; set; }
            public double TienNgBH { get; set; }
            public double TienKhac { get; set; }
        }
    }
}
