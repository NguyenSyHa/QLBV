using System;
using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
using System.Data;
using System.Windows.Forms;
using System.Collections.Generic;

namespace QLBV.BaoCao
{
    public partial class rep_phieuthu01071 : DevExpress.XtraReports.UI.XtraReport
    {
        int _maBN = 0, _ploai = 0, _iDVP = 0, ktradong = 0;
        bool _inChiTiet = false;
        public rep_phieuthu01071(int mabn, int idvp, int ploai, bool _inChitiet)
        {
            InitializeComponent();
            _maBN = mabn;
            _ploai = ploai;
            _iDVP = idvp;
            _inChiTiet = _inChitiet;
        }
        private class Listdv01071
        {
            public int? STT { get; set; }
            public int? STT_F { get; set; }
            public string TenNhom { get; set; }
            public int MaDV { get; set; }
            public string TenDV { get; set; }
            public double ChiPhi { get; set; }
            public double TenBN { get; set; }
            public string TrongDM { get; set; }
            public int TrongBH { get; set; }
        }
        QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

        private void xrSubreport1_BeforePrint(object sender, CancelEventArgs e)
        {
            _InSubReport(xrSubreport1);
            if (ktradong > 5)
                xrPageBreak1.Visible = true;

        }
        private void _InSubReport(XRSubreport repsub)
        {
            List<Listdv01071> _lkq = new List<Listdv01071>();
            QLBV.FormThamSo.rep_PhieuThu_01071 rep = new FormThamSo.rep_PhieuThu_01071(_ploai, _inChiTiet);
            repsub.ReportSource = rep;
            var _ldv = (from n in _data.NhomDVs
                        join tn in _data.TieuNhomDVs on n.IDNhom equals tn.IDNhom
                        join dv in _data.DichVus on tn.IdTieuNhom equals dv.IdTieuNhom
                        select new { n.TenNhomCT, dv.PLoai, dv.TenDV, tn.TenRG, dv.MaDV, n.IDNhom, n.STT, dv.GiaBHGioiHanTT }).ToList();
            var _lTTbn = (from bn in _data.BenhNhans.Where(p => p.MaBNhan == _maBN)
                          select new
                          {
                              bn.TenBNhan,
                              bn.GTinh,
                              bn.SThe,
                              bn.Tuoi,
                              bn.NNhap,
                              bn.DChi,
                              bn.DTuong,
                              bn.MucHuong,
                              bn.Tuyen
                          }).ToList();
            #region thu sau khi duyệt
            if (_ploai == 1)
            {
                double _PhuThu = 0;

                var _lvphict = (from vp in _data.VienPhis.Where(p => p.MaBNhan == _maBN)
                                join vpct in _data.VienPhicts.Where(p => p.ThanhToan == 0).Where(p => p.TrongBH != 2) on vp.idVPhi equals vpct.idVPhi
                                select new { vpct.MaDV, vpct.TienBN, vpct.ThanhTien, vpct.TrongBH, vpct.DonGia, vpct.SoLuong, vpct.TBNCTT, vpct.TBNTT }).ToList();
                if (_lTTbn.Count() > 0)
                {
                    rep.tenbn.Value = _lTTbn.First().TenBNhan.ToUpper();
                    rep.tuoi.Value = _lTTbn.First().Tuoi.ToString();
                    rep.gtinh.Value = _lTTbn.First().GTinh == 1 ? "Nam" : "Nữ";
                    rep.sophieuthu.Value = "Số phiếu khám: " + _maBN.ToString();
                    rep.dchi.Value = _lTTbn.First().DChi;
                    rep.sthe.Value = _lTTbn.First().SThe == null ? "" : _lTTbn.First().SThe;
                    rep.ngayvao.Value = _lTTbn.First().NNhap.Value.ToShortDateString();
                    var ngayra = _data.RaViens.Where(p => p.MaBNhan == _maBN).Select(p => p.NgayRa).ToList();
                    if (ngayra.Count() > 0)
                        rep.ngayra.Value = ngayra.First().Value.ToShortDateString();
                    rep.sophieu.Value = _iDVP.ToString();
                    rep.kytenbn.Value = _lTTbn.First().TenBNhan;
                    if (DungChung.Bien.MaBV != "56789")
                    {
                        rep.kytennlb.Value = DungChung.Bien.KeToanVP;
                    }
                    //rep.labngaygio.Text = System.DateTime.Now.ToString();
                    if (DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297")
                    {
                        var sdt = _data.HTHONGs.Where(p => p.MaBV == DungChung.Bien.MaBV).ToList();
                        rep.cqcq.Value = DungChung.Bien.TenCQ.ToUpper();
                        rep.cq.Value = "SĐT: " + (sdt != null ? sdt.First().SDT : "");
                        rep.DiaChiCQ.Value = "ĐC: " + (sdt != null ? sdt.First().DiaChi : "");
                    }
                    else
                    {
                        rep.cqcq.Value = DungChung.Bien.TenCQCQ.ToUpper();
                        rep.cq.Value = DungChung.Bien.TenCQ.ToUpper();
                    }
                    rep.mabn.Value = _maBN;

                    string Dt = _lTTbn.First().DTuong;
                    rep.dtuong.Value = _lTTbn.First().DTuong;
                    var _lCkham = (from dv in _ldv
                                   join vp in _lvphict on dv.MaDV equals vp.MaDV
                                   group new { vp, dv } by new { dv.MaDV, dv.TenNhomCT, dv.TenDV, dv.STT, vp.TrongBH, vp.DonGia, vp.SoLuong } into kq
                                   select new
                                   {
                                       kq.Key.MaDV,
                                       kq.Key.TenNhomCT,
                                       TrongDM = kq.Key.TrongBH == 1 ? "Chi phí trong DM" : (kq.Key.TrongBH == 0 ? "Chi phí ngoài DM" : "Chi phí phụ thu"),
                                       TrongBH = kq.Key.TrongBH == 1 ? 1 : (kq.Key.TrongBH == 0 ? 2 : 3),
                                       kq.Key.TenDV,
                                       kq.Key.STT,
                                       chiphi = kq.Sum(p => p.vp.ThanhTien),
                                       tienbn = kq.Sum(p => p.vp.TBNCTT) + kq.Sum(p => p.vp.TBNTT),
                                       kq.Key.SoLuong,
                                       kq.Key.DonGia
                                   }).ToList();


                    foreach (var item in _lCkham)
                    {
                        Listdv01071 moi = new Listdv01071();
                        moi.STT = item.STT;
                        moi.TenNhom = item.TrongBH == 1 ? (item.TenNhomCT.Contains("Khám") ? item.TenDV : item.TenNhomCT) : (item.TenNhomCT.Contains("Khám") ? item.TenDV : item.TenNhomCT.Replace("trong danh mục BHYT", ""));
                        moi.TenDV = item.TenDV;
                        moi.ChiPhi = item.chiphi;
                        moi.TenBN = item.tienbn;
                        moi.TrongDM = item.TrongDM;
                        moi.TrongBH = item.TrongBH;
                        _lkq.Add(moi);
                        if (item.TrongBH == 3)
                            _PhuThu += item.chiphi;
                    }

                    rep.trathem.Value = _PhuThu.ToString("##,###");
                    var KetQua = (from k in _lkq
                                  group k by k into kq
                                  select new
                                  {
                                      kq.Key.STT,
                                      kq.Key.TenNhom,
                                      kq.Key.TenDV,
                                      kq.Key.ChiPhi,
                                      kq.Key.TenBN,
                                      kq.Key.TrongDM,
                                      kq.Key.TrongBH
                                  }).OrderBy(p => p.STT).ToList();
                    if (_PhuThu > 0)
                        rep.phuthu.Value = _PhuThu.ToString("##,###");
                    else
                        rep.phuthu.Value = "0";
                    double tongtien = KetQua.Sum(p => p.TenBN);
                    rep.doctien.Value = DungChung.Ham.DocTienBangChu(tongtien, " đồng");
                    var tamung = _data.TamUngs.Where(p => p.MaBNhan == _maBN).ToList();
                    if (tamung.Where(p => p.PhanLoai == 0).ToList().Count() > 0)
                    {
                        double tu = Convert.ToDouble(tamung.Where(p => p.PhanLoai == 0).Sum(p => p.SoTien));
                        //rep.tamung.Value = tu.ToString() + " đồng";
                        rep.chutamung.Value = "Số tiền đã tạm ứng:";
                        rep.tientu.Value = tu.ToString("##,###");
                        rep.celdong.Text = " đồng";
                        rep.celbangchu.Text = "Bằng chữ:";
                        rep.doctientu.Value = DungChung.Ham.DocTienBangChu(tu, " đồng");
                        double tienbn = 0;
                        tienbn = tongtien - tu;
                        //rep.trathem.Value = tienbn.ToString() + " đồng";
                        if (tienbn > 0)
                        {
                            rep.tienchenh.Value = "Tiền BN trả thêm:";
                            rep.trathem.Value = tienbn.ToString("##,###");
                            rep.tamung.Value = DungChung.Ham.DocTienBangChu(tienbn, " đồng");
                        }
                        else
                        {
                            tienbn = tienbn * (-1);
                            rep.tienchenh.Value = "Tiền BN nhận lại:";
                            rep.trathem.Value = tienbn.ToString("##,###");
                            rep.tamung.Value = DungChung.Ham.DocTienBangChu(tienbn, " đồng");
                        }
                    }
                    else
                    {
                        rep.tienchenh.Value = "Tiền BN trả thêm:";
                        rep.tamung.Value = DungChung.Ham.DocTienBangChu(tongtien, " đồng");
                        rep.trathem.Value = tongtien.ToString("##,###");
                    }
                    var thu = _data.TamUngs.Where(p => p.MaBNhan == _maBN).Where(p => p.PhanLoai == 1).FirstOrDefault();

                    if (thu != null)
                    {
                        DateTime ngaythu = Convert.ToDateTime(thu.NgayThu);
                        if (DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297")
                            rep.ngaythang.Value = DungChung.Ham.NgaySangChu(ngaythu, DungChung.Bien.FormatDate);
                        else
                            rep.ngaythang.Value = "Ngày " + ngaythu.Day + " tháng " + ngaythu.Month + " Năm " + ngaythu.Year;
                    }
                    else
                        rep.ngaythang.Value = "Ngày.....tháng.....năm.....";
                    ktradong = KetQua.Count();
                    rep.DataSource = KetQua;
                    rep.bindingdata(_ploai);
                }
                #endregion
            }
            else
            {
                var _lTamThu = (from tu in _data.TamUngs.Where(p => p.IDTamUng == _iDVP)
                                select new { tu.IDTamUng, tu.MaBNhan, tu.NgayThu, tu.IDGoiDV, tu.SoTien, tu.Mien, tu.TongTien, tu.DuyetPhieuThu, tu.NguoiKiemDuyet }).ToList();
                var _lTamThu1 = (from tu in _data.TamUngs.Where(p => p.IDTamUng == _iDVP)
                                 join cb in _data.CanBoes on tu.MaCB equals cb.MaCB
                                 select new { cb.TenCB }).ToList();
                if (_lTamThu.Count() > 0)
                {


                    var _lTamungct = (from tuct in _data.TamUngcts.Where(p => p.IDTamUng == _iDVP).Where(p => p.Status == 0)
                                      select new { tuct.MaDV, tuct.TienBN, tuct.ThanhTien }).ToList();
                    if (_lTTbn.Count() > 0)
                    {

                        //FormThamSo.rep_PhieuThu_01071 rep = new FormThamSo.rep_PhieuThu_01071(_ploai);
                        rep.tenbn.Value = _lTTbn.First().TenBNhan.ToUpper();
                        rep.tuoi.Value = _lTTbn.First().Tuoi.ToString();
                        rep.gtinh.Value = _lTTbn.First().GTinh == 1 ? "Nam" : "Nữ";
                        rep.sophieuthu.Value = "Số phiếu khám: " + _maBN.ToString();
                        rep.dchi.Value = _lTTbn.First().DChi;
                        rep.sthe.Value = _lTTbn.First().SThe == null ? "" : _lTTbn.First().SThe;
                        rep.ngayvao.Value = _lTTbn.First().NNhap.Value.ToShortDateString();
                        rep.sophieu.Value = _iDVP.ToString();
                        rep.kytenbn.Value = _lTTbn.First().TenBNhan;

                        if (DungChung.Bien.MaBV != "56789")
                        {
                            if (DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297")
                            {
                                bool? DuyetPhieu = _lTamThu.First().DuyetPhieuThu == null ? false : _lTamThu.First().DuyetPhieuThu;
                                var tk = _lTamThu.First().NguoiKiemDuyet ?? "";
                                rep.NguoiLapPhieu.Value = _lTamThu1.First().TenCB;
                                if (tk != "" && DuyetPhieu == true)
                                {
                                    rep.kytennlb.Value = _data.ADMINs.Where(p => p.TenDN == tk).FirstOrDefault().TenGoi;

                                }


                            }
                            else
                            {
                                rep.kytennlb.Value = DungChung.Bien.KeToanVP;
                            }
                        }

                        else
                            rep.kytennlb.Value = _lTamThu1.First().TenCB;

                        //rep.labngaygio.Text = System.DateTime.Now.ToString();
                        if (DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297")
                        {
                            var sdt = _data.HTHONGs.Where(p => p.MaBV == DungChung.Bien.MaBV).ToList();
                            rep.cqcq.Value = DungChung.Bien.TenCQ.ToUpper();
                            rep.cq.Value = "SĐT: " + (sdt != null ? sdt.First().SDT : "");
                            rep.DiaChiCQ.Value = "ĐC: " + (sdt != null ? sdt.First().DiaChi : "");
                        }
                        else
                        {
                            rep.cqcq.Value = DungChung.Bien.TenCQCQ.ToUpper();
                            rep.cq.Value = DungChung.Bien.TenCQ.ToUpper();
                        }
                        rep.mabn.Value = _maBN;
                        var ngayra = _data.RaViens.Where(p => p.MaBNhan == _maBN).Select(p => p.NgayRa).ToList();
                        if (ngayra.Count() > 0)
                            rep.ngayra.Value = ngayra.First().Value.ToShortDateString();
                        if (_lTamThu.First().IDGoiDV != null && _lTamThu.First().IDGoiDV > 0 && (DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "56789" || DungChung.Bien.MaBV == "24297"))//in gói dịch vụ
                        {
                            int IDGoiDV = _lTamThu.First().IDGoiDV ?? 0;
                            var ttgoi = _data.DmGoiDVs.Where(p => p.IDGoi == IDGoiDV).ToList();
                            if (ttgoi.Count() > 0)
                            {
                                Listdv01071 moi = new Listdv01071();
                                moi.TrongBH = 0;
                                moi.TrongDM = ttgoi.First().TenGoi;
                                moi.TenNhom = ttgoi.First().TenGoi;
                                moi.TenDV = ttgoi.First().TenGoi;
                                moi.ChiPhi = ttgoi.First().DonGia;
                                moi.TenBN = _lTamThu.First().SoTien ?? 0;
                                _lkq.Add(moi);
                            }
                        }
                        else
                        {
                            var _lCkham = (from dv in _ldv
                                           join vp in _lTamungct on dv.MaDV equals vp.MaDV
                                           group new { vp, dv } by new { dv.MaDV, dv.TenDV, dv.TenNhomCT, dv.STT, dv.TenRG } into kq
                                           select new
                                           {
                                               kq.Key.MaDV,
                                               kq.Key.TenDV,
                                               kq.Key.TenNhomCT,
                                               kq.Key.STT,
                                               kq.Key.TenRG,
                                               chiphi = kq.Sum(p => p.vp.ThanhTien),
                                               tienbn = kq.Sum(p => p.vp.TienBN)
                                           }).ToList();
                            foreach (var item in _lCkham)
                            {
                                Listdv01071 moi = new Listdv01071();
                                moi.TrongBH = item.STT ?? 0;
                                moi.TrongDM = item.TenNhomCT.Contains("Khám") ? item.TenDV : item.TenNhomCT;
                                moi.TenNhom = item.TenRG;
                                moi.TenDV = item.TenDV;
                                moi.ChiPhi = item.chiphi;
                                moi.TenBN = item.tienbn;
                                _lkq.Add(moi);
                            }
                        }
                        var KetQua = (from k in _lkq
                                      group k by k into kq
                                      select new
                                      {
                                          kq.Key.STT,
                                          TenNhom = (kq.Key.TrongBH == 1) ? kq.Key.TenNhom : kq.Key.TenNhom.Replace("trong danh mục BHYT", ""),
                                          kq.Key.TenDV,
                                          kq.Key.ChiPhi,
                                          kq.Key.TenBN,
                                          kq.Key.TrongBH,
                                          kq.Key.TrongDM
                                      }).OrderBy(p => p.STT).ToList();
                        double tongtien = KetQua.Sum(p => p.TenBN);
                        if (_lTamThu.First().Mien > 0)
                        {
                            rep.celMien.Text = "Số tiền miễn/giảm:";
                            double aa = Convert.ToDouble(_lTamThu.Sum(p => p.TongTien)) - Convert.ToDouble(_lTamThu.Sum(p => p.SoTien));
                            rep.celMien2.Text = aa.ToString("##,###");
                            rep.celMien3.Text = "đồng";
                            rep.Mien4.Text = "Bằng chữ:";
                            rep.Mien5.Text = DungChung.Ham.DocTienBangChu(aa, " đồng");
                        }
                        rep.doctien.Value = DungChung.Ham.DocTienBangChu(tongtien, " đồng");
                        var tamung = _data.TamUngs.Where(p => p.MaBNhan == _maBN).Where(p => p.PhanLoai == 0).ToList();
                        if (tamung.Count() > 0)
                        {
                            double? tu = tamung.Sum(p => p.SoTien);
                            rep.tamung.Value = tu.ToString() + " đồng";
                            double? tienbn = tongtien - tu;
                            rep.trathem.Value = tienbn.ToString() + " đồng";
                        }
                        else
                        {
                            rep.tamung.Value = "0 đồng";
                            rep.trathem.Value = tongtien.ToString("##,###") + " đồng";
                        }
                        DateTime ngaythu = Convert.ToDateTime(_lTamThu.First().NgayThu);
                        if (DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297")
                            rep.ngaythang.Value = DungChung.Ham.NgaySangChu(ngaythu, DungChung.Bien.FormatDate);
                        else
                            rep.ngaythang.Value = "Ngày " + ngaythu.Day + " tháng " + ngaythu.Month + " Năm " + ngaythu.Year;
                        rep.DataSource = KetQua;
                        rep.bindingdata(_ploai);
                    }
                }

            }
        }
        private void xrSubreport2_BeforePrint(object sender, CancelEventArgs e)
        {

            _InSubReport(xrSubreport2);
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049")
            {
                PageFooter.Visible = true;
                labngaygio.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
                xrLabel1.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            }
            if (_inChiTiet == true)
            {
                xrLine1.Visible = false;
                labngaygio.Visible = false;
                xrSubreport2.Visible = false;
            }
        }
    }
}
