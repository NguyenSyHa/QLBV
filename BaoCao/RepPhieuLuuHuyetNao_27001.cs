using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class RepPhieuLuuHuyetNao_27001 : DevExpress.XtraReports.UI.XtraReport
    {
        QLBV_Database.QLBVEntities data;
        private int idCD = 0;
        public RepPhieuLuuHuyetNao_27001()
        {
            InitializeComponent();
        }
        internal void BindingData(int idCD)
        {
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            this.idCD = idCD;
            var chidinh = (from cd in data.ChiDinhs
                           join dv in data.DichVus on cd.MaDV equals dv.MaDV
                           join cls in data.CLS on cd.IdCLS equals cls.IdCLS
                           join kp in data.KPhongs on cls.MaKP equals kp.MaKP
                           select new
                           {
                               kp.IsDongY,
                               cd.IDCD,
                               cd.LoiDan,
                               cd.ChiDinh1,
                               cd.KetLuan,
                               cd.IdCLS,
                               dv.TenDV,
                               cd.Status,
                               cls.MaKPth
                           }).Where(p => p.IDCD == idCD).ToList();

            if (chidinh.Count > 0)
            {
                if (DungChung.Bien._Visible_CDHA[1] == false)
                {
                    if (DungChung.Bien.MaBV != "01071" && DungChung.Bien.MaBV != "01049")
                    {
                        if (chidinh.First().Status == 1)
                            DungChung.Bien._Visible_CDHA[2] = true;
                        else
                            DungChung.Bien._Visible_CDHA[2] = false;
                    }
                }
                int idCLS = Convert.ToInt32(chidinh.First().IdCLS);
                xrLabel59.Text = "Số phiếu: " + idCLS;
                //var q = (from  kb in data.BNKBs join 
                //         cls in data.CLS on new { kb.MaBNhan, kb.MaKP } equals new { cls.MaBNhan, cls.MaKP }
                //         select new { kb.IDKB, kb.ChanDoan, kb.MaBNhan, kb.MaKP, kb.NgayKham,kb.Buong, kb.Giuong,BSCD = kb.MaCB, cls.MaCB, cls.IdCLS, cls.NgayTH }).Where(p => p.IdCLS == idCLS).ToList();
                var qCLS = data.CLS.Where(p => p.IdCLS == idCLS).ToList();
                int _mabn = 0, _makp = 0;

                if (qCLS.Count > 0)
                {
                    _makp = qCLS.First().MaKP == null ? 0 : Convert.ToInt32(qCLS.First().MaKP);
                    var rs = qCLS.First();
                    #region hiển thị thông tin bệnh nhân
                    var bnInfo = data.BenhNhans.Where(p => p.MaBNhan == rs.MaBNhan);
                    if (bnInfo.Count() > 0)
                    {
                        _mabn = bnInfo.First().MaBNhan;
                        if (!String.IsNullOrEmpty(bnInfo.First().TenBNhan))
                            xrLabel71.Text = bnInfo.First().TenBNhan.ToUpper().ToString();//(DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "56789") ? bnInfo.First().TenBNhan.ToUpper().ToString() : bnInfo.First().TenBNhan.ToString();
                        if (bnInfo.First().Tuoi != null)
                            xrLabel70.Text
                            = DungChung.Bien.MaBV == "14018" ? DungChung.Ham.TinhTuoiBenhNhan(data, _mabn)
                            : DungChung.Bien.MaBV == "24012" ? DungChung.Ham.TuoitheoThang(data, _mabn, DungChung.Bien.formatAge_24012)
                            : bnInfo.First().Tuoi.ToString();
                        if (bnInfo.First().GTinh != null)
                            xrLabel69.Text = bnInfo.First().GTinh == 1 ? "Nam" : "Nữ";
                        if (!String.IsNullOrEmpty(bnInfo.First().SThe))
                            xrLabel72.Text = bnInfo.First().SThe.ToString();
                        if (!String.IsNullOrEmpty(bnInfo.First().DChi))
                            xrLabel74.Text = bnInfo.First().DChi;
                        if (rs.NgayThang != null)
                            xrLabel93.Text = DungChung.Ham.NgaySangChu(Convert.ToDateTime(rs.NgayThang));
                    }
                    var _ttkb = data.BNKBs.Where(p => p.MaBNhan == _mabn && p.MaKP == _makp).ToList();
                    if (_ttkb.Count > 0)
                    {

                        xrLabel87.Text = _ttkb.First().Buong;
                        xrLabel89.Text = _ttkb.First().Giuong;
                        xrLabel75.Text = DungChung.Bien.MaBV == "14018" ? DungChung.Ham.GetChanDoanKB(data, _mabn) : DungChung.Bien.MaBV == "01049" ? (DungChung.Ham.FreshString(_ttkb.First().ChanDoan + ";" + _ttkb.First().BenhKhac) + "  Mã ICD:" + DungChung.Ham.FreshString(_ttkb.First().MaICD + ";" + _ttkb.First().MaICD2)) : DungChung.Bien.MaBV == "30372" ? _ttkb.First().ChanDoanBD + ";" + _ttkb.First().ChanDoan + ";" + _ttkb.First().BenhKhac : _ttkb.First().ChanDoan + ";" + _ttkb.First().BenhKhac;
                    }
                    #endregion hiển thị thông tin bệnh nhân
                    // hiển thị tên khoa phòng chỉ định
                    var kpName = data.KPhongs.Where(p => p.MaKP == rs.MaKP).ToList();
                    if (kpName.Count > 0)
                    {
                        if (!String.IsNullOrEmpty(kpName.First().TenKP))
                            xrLabel73.Text = kpName.First().TenKP.ToString();
                    }
                    // hiển thị bác sỹ chỉ định
                    string _macbcd = "", _macbth = "";
                    _macbcd = rs.MaCB;
                    _macbth = rs.MaCBth;
                    xrLabel94.Text = getTenCB(_macbcd);
                    lblBSCLS.Text = "Nguyễn Thị Lài";//;getTenCB(_macbth);
                    if (chidinh.Count > 0)
                    {
                        string tendv = "";
                        if (!String.IsNullOrEmpty(chidinh.First().TenDV))
                        {
                            tendv = chidinh.First().TenDV;
                            xrLabel90.Text = tendv;
                        }
                        if (!String.IsNullOrEmpty(chidinh.First().ChiDinh1))
                            xrLabel90.Text =  tendv + (String.IsNullOrEmpty(chidinh.First().ChiDinh1) ? "" : "- " + chidinh.First().ChiDinh1.ToString());
                    }
                }
            }
        }
        private string getTenCB(string maCB)
        {
            string tenBs = "";
            var bsCD = data.CanBoes.Where(p => p.MaCB == maCB).ToList();
            if (bsCD.Count > 0)
            {
                if (!String.IsNullOrEmpty(bsCD.First().TenCB))
                    tenBs = bsCD.First().TenCB.ToString();
            }
            return tenBs;
        }
    }
}
