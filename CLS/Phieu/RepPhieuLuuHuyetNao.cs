using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;


namespace QLBV.BaoCao
{
    public partial class RepPhieuLuuHuyetNao : DevExpress.XtraReports.UI.XtraReport
    {
        QLBV_Database.QLBVEntities data;
        private int idCD = 0;
        public RepPhieuLuuHuyetNao()
        {
            InitializeComponent();
            if (DungChung.Bien.MaBV == "24009" || DungChung.Bien.MaBV == "30372" || DungChung.Bien.MaBV == "27777")
            {
                if (DungChung.Bien.MaBV == "24009" || DungChung.Bien.MaBV == "27777") 
                {
                    xrLabel57.Visible = true;
                }
                else if(DungChung.Bien.MaBV == "30372")
                {
                    xrLabel45.Visible = true;
                    xrLabel57.Visible = false;
                }
                SubBand2.Visible = false;
                SubBand3.Visible = true;
                xrPictureBox1.Image = DungChung.Ham.GetLogo();
                colDiaChi.Text = DungChung.Ham.GetDiaChiBV();
            }
            else
            {
                SubBand2.Visible = true;
                SubBand3.Visible = false;
            }
            if(DungChung.Bien.MaBV=="27001")
            {
                xrTableCell5.Visible = false;
                xrLabel14.Visible = false;
                lblNgayThangNam.Visible = false;
                lblTenBsy.Visible = false;
                sub_27001.Visible = true;
                SubBand2.Visible = false;
                SubBand3.Visible = false;
            }    

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="idCD">id Chỉ định</param>
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
          
            if (chidinh.Count > 0 /*&& DungChung.Bien.MaBV != "27001"*/)
            {
                if (DungChung.Bien.MaBV == "30010")
                {
                    int mkpth = Convert.ToInt32(chidinh.First().MaKPth.Value);
                    KPTH.Value = data.KPhongs.Where(p => p.MaKP == mkpth).Select(p => p.TenKP).First().ToString();
                }//yenbg1
                if (DungChung.Bien._Visible_CDHA[1] == false)
                {
                    if (DungChung.Bien.MaBV != "01071" && DungChung.Bien.MaBV != "01049" /*&& DungChung.Bien.MaBV != "27001"*/)
                    {
                        if (chidinh.First().Status == 1)
                            DungChung.Bien._Visible_CDHA[2] = true;
                        else
                            DungChung.Bien._Visible_CDHA[2] = false;
                    }
                }
                int idCLS = Convert.ToInt32(chidinh.First().IdCLS);
                colSoPhieu.Text = xrLabel59.Text = labSoPhieu.Text = "Số phiếu: " + idCLS;
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
                            xrLabel71.Text = xrLabel28.Text = lblHoTen.Text = bnInfo.First().TenBNhan.ToUpper().ToString();//(DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "56789") ? bnInfo.First().TenBNhan.ToUpper().ToString() : bnInfo.First().TenBNhan.ToString();
                        if (bnInfo.First().Tuoi != null)
                            lblTuoi.Text = xrLabel27.Text = xrLabel70.Text
                                = DungChung.Bien.MaBV == "14018" ? DungChung.Ham.TinhTuoiBenhNhan(data, _mabn) 
                                : DungChung.Bien.MaBV == "24012" ? DungChung.Ham.TuoitheoThang(data, _mabn, DungChung.Bien.formatAge_24012) 
                                : lblTuoi.Text = bnInfo.First().Tuoi.ToString();
                        if (bnInfo.First().GTinh != null)
                            xrLabel69.Text = xrLabel26.Text = lblGioiTinh.Text = bnInfo.First().GTinh == 1 ? "Nam" : "Nữ";
                        if (!String.IsNullOrEmpty(bnInfo.First().SThe))
                            xrLabel72.Text = xrLabel30.Text = lblSthe.Text = bnInfo.First().SThe.ToString();
                        if (!String.IsNullOrEmpty(bnInfo.First().DChi))
                            xrLabel74.Text = xrLabel32.Text = lblDchi.Text = bnInfo.First().DChi;
                        if (rs.NgayThang != null)
                            xrLabel93.Text = xrLabel51.Text = lblNgayThangNam.Text = DungChung.Ham.NgaySangChu(Convert.ToDateTime(rs.NgayThang));
                        if (!String.IsNullOrEmpty(rs.NgayTH.ToString()))
                        {
                            lblNgayCLS.Text = DungChung.Ham.NgaySangChu(Convert.ToDateTime(rs.NgayTH));
                            lblNgayCLS1.Text = DungChung.Ham.NgaySangChu(Convert.ToDateTime(rs.NgayTH));
                        }

                    }
                    var _ttkb = data.BNKBs.Where(p => p.MaBNhan == _mabn && p.MaKP == _makp).ToList();
                    if (_ttkb.Count > 0)
                    {

                        xrLabel87.Text = xrLabel47.Text = lblBuong.Text = _ttkb.First().Buong;
                        xrLabel89.Text = xrLabel49.Text = lblGiuong.Text = _ttkb.First().Giuong;
                        if(DungChung.Bien.MaBV == "24297" && chidinh.First().IsDongY == true)
                        {
                            xrLabel33.Text = lblChanDoan.Text=DungChung.Ham.GetChanDoanKB_24297(data, _mabn);
                        }
                        else
                        {
                            xrLabel75.Text = xrLabel33.Text = lblChanDoan.Text = (_ttkb.First().ChanDoan != null || _ttkb.First().ChanDoan != "") && DungChung.Bien.MaBV == "24272" ? _ttkb.First().ChanDoan + ";" : "";
                            if (DungChung.Bien.MaBV != "24272")
                            {
                                xrLabel75.Text = xrLabel33.Text = lblChanDoan.Text += DungChung.Bien.MaBV == "14018" ? DungChung.Ham.GetChanDoanKB(data, _mabn) : DungChung.Bien.MaBV == "01049" ? (DungChung.Ham.FreshString(_ttkb.First().ChanDoan + ";" + _ttkb.First().BenhKhac) + "  Mã ICD:" + DungChung.Ham.FreshString(_ttkb.First().MaICD + ";" + _ttkb.First().MaICD2)) : DungChung.Bien.MaBV == "30372" ? _ttkb.First().ChanDoanBD + ";" + _ttkb.First().ChanDoan + ";" + _ttkb.First().BenhKhac : _ttkb.First().ChanDoan + ";" + _ttkb.First().BenhKhac;
                            }
                            
                        }
                        

                    }
                    #endregion hiển thị thông tin bệnh nhân
                    // hiển thị tên khoa phòng chỉ định
                    var kpName = data.KPhongs.Where(p => p.MaKP == rs.MaKP).ToList();
                    if (kpName.Count > 0)
                    {
                        if (!String.IsNullOrEmpty(kpName.First().TenKP))
                            xrLabel73.Text = xrLabel31.Text = lblKP.Text = kpName.First().TenKP.ToString();
                    }
                    // hiển thị bác sỹ chỉ định
                    string _macbcd = "", _macbth = "";
                    _macbcd = rs.MaCB;
                    _macbth = rs.MaCBth;
                    xrLabel94.Text = xrLabel52.Text = lblTenBsy.Text = getTenCB(_macbcd);
                    lblBSCLS.Text = getTenCB(_macbth);
                    lblBSCLS1.Text = getTenCB(_macbth);
                    if (chidinh.Count > 0)
                    {
                        string tendv = "";
                        if (!String.IsNullOrEmpty(chidinh.First().KetLuan))
                            xrRichText1.Html = (QLBV_Library.QLBV_Ham.convertHTML(chidinh.First().KetLuan, "black", "black", '\r', '|', "yes"));
                        if (!String.IsNullOrEmpty(chidinh.First().LoiDan))
                        {
                            lblLoidanBS.Text = chidinh.First().LoiDan;
                            lblLoidanBS1.Text = chidinh.First().LoiDan;
                        }
                        if (!String.IsNullOrEmpty(chidinh.First().TenDV))
                        {
                            tendv = chidinh.First().TenDV;
                            xrLabel90.Text = xrLabel45.Text = lblChiDinh.Text = tendv;
                            TenDV.Value = tendv;
                        }
                        if (!String.IsNullOrEmpty(chidinh.First().ChiDinh1))
                        {
                            xrLabel90.Text = xrLabel45.Text = lblChiDinh.Text = tendv + (String.IsNullOrEmpty(chidinh.First().ChiDinh1) ? "" : "- " + chidinh.First().ChiDinh1.ToString());
                            TenDV.Value = tendv + (String.IsNullOrEmpty(chidinh.First().ChiDinh1) ? "" : " (" + chidinh.First().ChiDinh1.ToString() + ") ");
                        }
                    }
                }
            }

            if (DungChung.Bien.MaBV == "26007")
            {
                xrLabel29.Visible = false;
                lblLoidanBS.Visible = false;
                xrLabel12.Visible = false;
                lblLoidanBS1.Visible = false;
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
        private void Detail2_BeforePrint(object sender, CancelEventArgs e)
        {
            //lblNgayThangNam.Text = DungChung.Ham.NgaySangChu(kq.First().NgayCD.Value, DungChung.Bien.FormatDate);
        }

        private void xrSubreport1_BeforePrint(object sender, CancelEventArgs e)
        {
            if (idCD > 0)
            {
                data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                var q1 = (from clsct in data.CLScts
                          join cd in data.ChiDinhs on clsct.IDCD equals cd.IDCD
                          where clsct.IDCD == idCD
                          select new
                          {
                              clsct.Id,
                              clsct.MaDVct,
                              clsct.KetQua,
                              cd.IDCD,
                              cd.KetLuan,
                              // clsct.STTHT

                          }).ToList();
                var q2 = (from x1 in q1
                          join x2 in data.DichVucts on x1.MaDVct equals x2.MaDVct
                          select new { x1.Id, x1.MaDVct, x1.KetLuan, x1.KetQua, x1.IDCD, x2.TenDVct, x2.STT }).OrderBy(p => p.STT).ToList();

                switch (this.Mau.Value.ToString())
                {
                    case "0":// phiếu lưu huyết não
                        if (DungChung.Bien.MaBV == "19048")
                        {
                            RepPhieuLuuHuyetNao_sub_19048 repSub = (RepPhieuLuuHuyetNao_sub_19048)xrSubreport1.ReportSource;
                            repSub.DataSource = q2;
                            repSub.dataBinding();
                        }
                        else if (DungChung.Bien.MaBV == "24009")
                        {
                            RepPhieuLuuHuyetNao_sub24009 repSub = (RepPhieuLuuHuyetNao_sub24009)xrSubreport1.ReportSource;
                            repSub.DataSource = q2;
                            repSub.dataBinding();
                        }
                        else if (DungChung.Bien.MaBV == "12121" || DungChung.Bien.MaBV == "30007" || DungChung.Bien.MaBV == "30009")
                        {
                            RepPhieuLuuHuyetNao_sub_12121 repSub = (RepPhieuLuuHuyetNao_sub_12121)xrSubreport1.ReportSource;
                            if (q2.Count >= 1)
                            {
                                repSub.Text = q2.First().KetLuan;
                                repSub.txtKetQua.Text = q2.First().KetQua;

                            }
                        }
                        else
                        {
                            RepPhieuLuuHuyetNao_sub repSub = (RepPhieuLuuHuyetNao_sub)xrSubreport1.ReportSource;
                            repSub.DataSource = q2;
                            repSub.dataBinding();
                        }
                        break;
                    case "1":// đo mật độ loãng xương
                        if (DungChung.Bien.MaBV == "12121" || DungChung.Bien.MaBV == "30009")
                        {

                            RepPhieuLuuHuyetNao_sub_LoangXuong_12121 repSub2 = (RepPhieuLuuHuyetNao_sub_LoangXuong_12121)xrSubreport1.ReportSource;
                            if (q2.Count >= 1)
                            {
                                repSub2.txtKetQua.Text = q2.First().KetQua;
                            }
                            else
                                repSub2.txtKetQua.Text = "";


                        }
                        else
                        {
                            RepPhieuLuuHuyetNao_sub_LoangXuong repSub2 = (RepPhieuLuuHuyetNao_sub_LoangXuong)xrSubreport1.ReportSource;
                            int c = 0;
                            float number;
                            //repSub2.DataSource = q2;
                            if (q2.Count >= 2)
                            {
                                if (q2.First().KetQua != null && float.TryParse(q2.First().KetQua.ToString(), out number))
                                    repSub2.BMD = Convert.ToDouble(q2.First().KetQua);
                                else repSub2.BMD = -1000;
                                var q2th = q2.Skip(1).Take(1).First();
                                if (q2th.KetQua != null && float.TryParse(q2th.KetQua.ToString(), out number))
                                    repSub2.SCore = Convert.ToDouble(q2th.KetQua);
                                else repSub2.SCore = -1000;

                            }
                            else if (q2.Count >= 1)
                            {
                                if (q2.First().KetQua != null && float.TryParse(q2.First().KetQua.ToString(), out number))
                                    repSub2.BMD = Convert.ToDouble(q2.First().KetQua);
                                else repSub2.BMD = -1000;
                            }

                            repSub2.dataBinding();
                        }
                        break;
                    case "2":// phiếu lưu huyết não                    
                        RepPhieuLuuHuyetNao_sub_DopplerXuyenSo repSub3 = (RepPhieuLuuHuyetNao_sub_DopplerXuyenSo)xrSubreport1.ReportSource;
                        repSub3.DataSource = q2;
                        repSub3.dataBinding(q2.Count);

                        break;

                    case "3":// phiếu điện não                  
                        RepPhieuLuuHuyetNao_sub_12121 repSub4 = (RepPhieuLuuHuyetNao_sub_12121)xrSubreport1.ReportSource;
                        if (q2.Count >= 1)
                        {
                            //repSub4.labTinhTrangNB.Html = "";
                            repSub4.labTinhTrangNB.Text = (q2.First().KetLuan == null) ? "" : q2.First().KetLuan;
                            repSub4.txtKetQua.Text = q2.First().KetQua != null ? q2.First().KetQua : "";
                            //repSub4.trangthai.Text = "";
                            //repSub4.trangthai.Text = (q2.First().KetLuan == null) ? "" : q2.First().KetLuan;
                            //repSub4.ketqua.Text = q2.First().KetQua != null ? q2.First().KetQua : "";
                        }

                        break;
                }

            }
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "56789")
                xrTable1.Visible = true;
            xrLabel43.Text = txtTenCQ.Text = colTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            xrLabel20.Text = txtTenCQCQ.Text = colCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            tb_ChiDinh.Visible = DungChung.Bien._Visible_CDHA[0];
            tb_KetQua.Visible = DungChung.Bien._Visible_CDHA[1];
            tb_KetQua.Visible = DungChung.Bien._Visible_CDHA[2];
            xrSubreport1.Visible = DungChung.Bien._Visible_CDHA[1];
            xrSubreport1.Visible = DungChung.Bien._Visible_CDHA[2];
            xrLabel56.Visible = DungChung.Bien._Visible_CDHA[1];
            xrLabel56.Visible = DungChung.Bien._Visible_CDHA[2];
            xrTable6.Visible = DungChung.Bien._Visible_CDHA[1];
            xrTable6.Visible = DungChung.Bien._Visible_CDHA[2];
            ReportFooter.Visible = DungChung.Bien._Visible_CDHA[1];
            ReportFooter.Visible = DungChung.Bien._Visible_CDHA[2];
            if(DungChung.Bien.MaBV == "30004")
            {
                xrSubreport1.Visible = DungChung.Bien._Visible_CDHA[1];
                xrLabel56.Visible = DungChung.Bien._Visible_CDHA[1];
                xrTable6.Visible = DungChung.Bien._Visible_CDHA[1];
                ReportFooter.Visible = DungChung.Bien._Visible_CDHA[1];
            }

            if (DungChung.Bien.MaBV == "24009")
            {
                xrTable4.Visible = DungChung.Bien._Visible_CDHA[0];
                tb_KetQua.Visible = DungChung.Bien._Visible_CDHA[1];
                xrSubreport1.Visible = DungChung.Bien._Visible_CDHA[1];
                ReportFooter.Visible = DungChung.Bien._Visible_CDHA[1];
                xrLabel56.Visible = DungChung.Bien._Visible_CDHA[1];
                xrTable6.Visible = DungChung.Bien._Visible_CDHA[1];
            }

            if (DungChung.Bien.MaBV == "12121" && this.Mau.Value.ToString() == "0")
                lab_HenGhi.Visible = true;
            if (DungChung.Bien.MaBV == "12121" || DungChung.Bien.MaBV == "30007")
            {
                lblHuyetApName.Visible = false;
                lblHuyetAp.Visible = false;
                lblSthe.Visible = false;
                lab_SoThe.Visible = false;
            }
            if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "56789")
            {
                thuong.Visible = true;
                thuong1.Visible = true;
                capcuu.Visible = true;
                capcuu1.Visible = true;
            }
            if (DungChung.Bien.MaBV == "27001")
            {
                tb_ChiDinh.Visible = true;
                tb_KetQua.Visible = true;
            }
            if (DungChung.Bien.MaBV == "24297")
            {
                if (idCD > 0)
                {
                    data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                    var q1 = (from clsct in data.CLScts
                              join cd in data.ChiDinhs on clsct.IDCD equals cd.IDCD
                              where clsct.IDCD == idCD
                              select new
                              {
                                  clsct.Id,
                                  clsct.MaDVct,
                                  clsct.KetQua,
                                  cd.IDCD,
                                  cd.KetLuan,

                              }).ToList();
                    var q2 = (from x1 in q1
                              join x2 in data.DichVucts on x1.MaDVct equals x2.MaDVct
                              select new { x1.Id, x1.MaDVct, x1.KetLuan, x1.KetQua, x1.IDCD, x2.TenDVct, x2.STT }).OrderBy(p => p.STT).ToList();
                    string[] a = new string[20];
                    if (q2.First().KetQua != null)
                    {
                        var lhn = q2.First().KetQua.Split(';');
                        for (int i = 0; i < lhn.Count(); i++)
                        {
                            a[i] = lhn[i];
                        }
                    }
                    txt01.Text = a[0];
                    txt02.Text = a[1];
                    txt03.Text = a[2];
                    txt04.Text = a[3];
                    txt05.Text = a[4];
                    txt06.Text = a[5];
                    txt07.Text = a[6];
                    txt08.Text = a[7];
                    txt09.Text = a[8];
                    txt10.Text = a[9];
                    txt11.Text = a[10];
                    txt12.Text = a[11];
                    txt13.Text = a[12];
                    txt14.Text = a[13];
                    txt15.Text = a[14];
                    txt16.Text = a[15];
                }
            }
            if (DungChung.Bien.MaBV == "26007")
                xrBarCode1.Visible = true;

            if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049")
            {

                txtTenCQCQ.Font = new Font(txtTenCQCQ.Font, FontStyle.Bold);
                txtTenCQ.Font = new Font(txtTenCQ.Font, FontStyle.Bold);
                xrLabel15.Font = new Font(xrLabel15.Font, FontStyle.Regular);

                lblHoTen.Font = new Font(lblHoTen.Font, FontStyle.Bold);
                sovv.Font = new Font(sovv.Font, FontStyle.Regular);
                xrLabel3.Font = new Font(xrLabel3.Font, FontStyle.Regular);
                xrTableCell6.Font = new Font(xrTableCell6.Font, FontStyle.Regular);
                xrTableCell7.Font = new Font(xrTableCell7.Font, FontStyle.Regular);
                xrLabel14.Font = new Font(xrLabel14.Font, FontStyle.Regular);
                xrLabel9.Font = new Font(xrLabel9.Font, FontStyle.Regular);
                lblTenBsy.Font = new Font(lblTenBsy.Font, FontStyle.Regular);
                labSoPhieu.Font = new Font(labSoPhieu.Font, FontStyle.Regular);
                xrLabel29.Font = new Font(xrLabel29.Font, FontStyle.Regular);
                xrLabel12.Font = new Font(xrLabel12.Font, FontStyle.Regular);
                xrLabel24.Font = new Font(xrLabel24.Font, FontStyle.Regular);
                xrTableCell2.Font = new Font(xrTableCell2.Font, FontStyle.Regular);
                xrTableCell8.Font = new Font(xrTableCell8.Font, FontStyle.Regular);
                xrLabel7.Font = new Font(xrLabel7.Font, FontStyle.Regular);
                xrLabel35.Font = new Font(xrLabel35.Font, FontStyle.Regular);
                lblBSCLS.Font = new Font(lblBSCLS.Font, FontStyle.Regular);
                lblBSCLS1.Font = new Font(lblBSCLS1.Font, FontStyle.Regular);
                xrTable1.LocationF = xrLabel15.LocationF;
                xrLabel15.Visible = labSoPhieu.Visible = sovv.Visible = false;
            }

            if (DungChung.Bien.MaBV == "27001")
            {
                if (Title.Value.ToString() != "PHIẾU ĐO MẬT ĐỘ XƯƠNG")
                {
                    xrLabel92.Visible = false;
                }
            }
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "12121" || DungChung.Bien.MaBV == "30007")
            {
                xrLabel24.Visible = false;
                lblKetLuan.Visible = false;

            }
            if (DungChung.Bien.MaBV == "24297")
            {

                SubBand1.Visible = false;
                SubBand5_24297.Visible = true;

            }
        }

        private void GroupHeader1_BeforePrint(object sender, CancelEventArgs e)
        {
            //    if (idCD > 0)
            //    {
            //        data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            //        var q1 = (from clsct in data.CLScts
            //                  join cd in data.ChiDinhs on clsct.IDCD equals cd.IDCD
            //                  where clsct.IDCD == idCD
            //                  select new
            //                  {
            //                      clsct.Id,
            //                      clsct.MaDVct,
            //                      clsct.KetQua,
            //                      cd.IDCD,
            //                      cd.KetLuan,
            //                      // clsct.STTHT

            //                  }).ToList();
            //        var q2 = (from x1 in q1
            //                  join x2 in data.DichVucts on x1.MaDVct equals x2.MaDVct
            //                  select new { x1.Id, x1.MaDVct, x1.KetLuan, x1.KetQua, x1.IDCD, x2.TenDVct, x2.STT }).OrderBy(p => p.STT).ToList();
            //        if(q2.Count>0)
            //        {
            //            trangthai.Text = "";
            //            trangthai.Text = (q2.First().KetLuan == null) ? "" : q2.First().KetLuan;
            //            ketqua.Text = q2.First().KetQua != null ? q2.First().KetQua : "";
            //        }
            //    }
        }

        private void SubBand3_BeforePrint(object sender, CancelEventArgs e)
        {

        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "24297")
            {
                SubBand4.Visible = false;
                SubBand4_24297.Visible = true;
            }
        }
    }
}
