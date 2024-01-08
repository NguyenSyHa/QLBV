using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;

namespace QLBV.FormNhap
{
    public partial class frmSoThekho : DevExpress.XtraEditors.XtraForm
    {
        public frmSoThekho()
        {
            InitializeComponent();
        }
        private bool ktcd()
        {
            if (lupKho.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn kho");
                lupKho.Focus();
                return false;
            }
            if (lupngay.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn ngày tháng");
                lupngay.Focus();
                return false;
            }
            return true;
        }

        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void frmThekho_Load(object sender, EventArgs e)
        {
            if(DungChung.Bien.MaBV =="30009")
            {
                radTheoNgay.Visible = true;
                radTheoThang.Visible = true;
                ckGopThuocTheoNgay.Visible = false;
            }
            var D = from tk in data.KPhongs.Where(p => p.PLoai== ("Khoa dược")) select new { tk.TenKP, tk.MaKP };
            lupKho.Properties.DataSource = D.ToList();
            lupTieuNhom.Properties.DataSource = (from nhom in data.NhomDVs.Where(p => p.Status==1) join tn in data.TieuNhomDVs on nhom.IDNhom equals tn.IDNhom select tn).ToList();
            lupngaytu.DateTime = System.DateTime.Now;
            lupngay.DateTime = System.DateTime.Now;
            var c = (from ncc in data.NhaCCs select new { ncc.TenCC, ncc.MaCC }).ToList();
            if (c.Count > 0)
            {
                lupnhathau.Properties.DataSource = c;
            }
            if (DungChung.Bien.MaBV == "27021")
                ckGopThuocTheoNgay.Checked =  true;
            else
                ckGopThuocTheoNgay.Checked = false;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (ktcd())
            {
                List<KPhong> _lKP = data.KPhongs.ToList();
                List<NhaCC> _lCC = data.NhaCCs.ToList();
                frmIn frm = new frmIn();
                int _Maduoc = 0;
                int _makho = 0;
                int IdTieuNhom = -1;
                if (lupTieuNhom.EditValue != null && lupTieuNhom.EditValue.ToString() != "")
                    IdTieuNhom = Convert.ToInt32(lupTieuNhom.EditValue);
                if (lupTenDV.EditValue != null)
                    _Maduoc = Convert.ToInt32(lupTenDV.EditValue);
                if (lupKho.EditValue != null)
                    _makho = Convert.ToInt32(lupKho.EditValue);
                DateTime ngayden = System.DateTime.Now.Date;
                DateTime ngaytu = System.DateTime.Now;
                ngaytu = DungChung.Ham.NgayTu(lupngaytu.DateTime);
                ngayden = DungChung.Ham.NgayDen(lupngay.DateTime);
                var qtong = (from nx in data.NhapDs.Where(p => p.MaKP == _makho)
                             join nxct in data.NhapDcts on nx.IDNhap equals nxct.IDNhap
                             join nxx in data.DichVus on nxct.MaDV equals nxx.MaDV
                             where nx.PLoai == 1 || nx.PLoai == 2 || nx.PLoai == 3
                             join dv in data.DichVus on nxct.MaDV equals dv.MaDV
                             join Kh in data.KPhongs on nx.MaKP equals Kh.MaKP
                             where (nx.NgayNhap <= ngayden && nx.NgayNhap >= ngaytu)
                             && (IdTieuNhom == -1 ? true : dv.IdTieuNhom == IdTieuNhom)
                             select new { nx,nxx, nxct, dv, Kh }).ToList();
                if (ckcCoGia.Checked)
                {
                    if (DungChung.Bien.MaBV != "30009")
                    {
                        #region gộp thuốc theo ngày
                        if (ckGopThuocTheoNgay.Checked)
                        {
                            var qtong1 = (from a in qtong
                                          select new
                                          {
                                              NgayNhap = a.nx.NgayNhap.Value.Date,
                                              a.nx.IDNhap,
                                              a.nx.PLoai,
                                              a.nx.SoCT,
                                              a.nx.MaKP,
                                              a.nx.GhiChu,
                                              a.nx.KieuDon,
                                              a.nx.MaKPnx,
                                              a.nx.MaBNhan,
                                              a.nxct.MaDV,
                                              a.nxct.SoLo,
                                              a.nxct.HanDung,
                                              SoLuongN = a.nx.PLoai == 1 ? a.nxct.SoLuongN : 0,
                                              SoLuongX = (a.nx.PLoai == 2 || a.nx.PLoai == 3) ? a.nxct.SoLuongX : 0,
                                              a.nxct.DonGia,
                                              a.dv.TenDV,
                                              a.dv.MaCC,
                                              a.Kh.TenKP,
                                              a.dv.QCPC,
                                              a.dv.DonVi,
                                              a.dv.TenHC,
                                              TenKPNX = _lKP.Where(p => p.MaKP == a.nx.MaKPnx).Select(p => p.TenKP).FirstOrDefault(),
                                              GChu = (DungChung.Bien.MaBV != "12122" && DungChung.Bien.MaBV != "27023") ? a.nx.GhiChu : (a.nx.PLoai == 1 ? (a.nx.KieuDon == 1 ? _lCC.Where(p => p.MaCC == a.dv.MaCC).Select(p => p.TenCC).FirstOrDefault() : _lKP.Where(p => p.MaKP == a.nx.MaKPnx).Select(p => p.TenKP).FirstOrDefault()) : (a.nx.KieuDon == 0 ? (a.nx.MaBNhan == null ? "" : a.nx.MaBNhan.ToString()) : _lKP.Where(p => p.MaKP == a.nx.MaKPnx).Select(p => p.TenKP).FirstOrDefault())),
                                          }).ToList();
                            var q2 = (from t in qtong1
                                      group t by new
                                      {
                                          t.MaDV,
                                          t.TenDV,
                                          t.MaCC,
                                          t.NgayNhap,
                                          t.SoLo,
                                          t.HanDung,
                                          t.PLoai,
                                          t.TenKP,
                                          t.DonVi,
                                          t.DonGia,
                                          t.MaKP,
                                          t.TenHC
                                      } into kq
                                      select new
                                      {
                                          Ngaythang = kq.Key.NgayNhap,
                                          TenDV = (DungChung.Bien.MaBV == "27022" && _makho == 13 && !string.IsNullOrEmpty(kq.Key.TenHC) && !kq.Key.TenDV.Equals(kq.Key.TenHC)) ? kq.Key.TenDV + " (" + kq.Key.TenHC + ") " : kq.Key.TenDV,
                                          kq.Key.MaDV,
                                          QCPC = string.Join(";", kq.Where(p => p.QCPC != null && p.QCPC.Trim() != "").Select(p => p.QCPC).Distinct()),
                                          kq.Key.DonVi,
                                          SCT = string.Join(";", kq.Where(p => p.SoCT != null && p.SoCT.Trim() != "").Select(p => p.SoCT).Distinct()),
                                          SoLo = kq.Key.SoLo,
                                          HanDung = kq.Key.HanDung,
                                          Soluongton = kq.Sum(p => p.SoLuongN) - kq.Sum(p => p.SoLuongX),
                                          SLNhap = kq.Sum(p => p.SoLuongN),
                                          SLXuat = kq.Sum(p => p.SoLuongX),
                                          Ton = kq.Sum(p => p.SoLuongN) - kq.Sum(p => p.SoLuongX),
                                          DonGia = kq.Key.DonGia,
                                          ThanhTienTonDauKy = kq.Key.DonGia * (kq.Sum(p => p.SoLuongN) - kq.Sum(p => p.SoLuongX)),
                                          ThanhTienNhap = kq.Key.DonGia * (kq.Sum(p => p.SoLuongN)),
                                          ThanhTienXuat = kq.Key.DonGia * (kq.Sum(p => p.SoLuongX)),
                                          ThanhTienTonCK = kq.Key.DonGia * (kq.Sum(p => p.SoLuongN) - kq.Sum(p => p.SoLuongX)),
                                          Phanloai = kq.Key.PLoai,
                                          GChu = (string.Join(";", kq.Where(p => p.GChu != null && p.GChu.Trim() != "").Select(p => p.GChu).Distinct())),
                                          Ghichu = (DungChung.Bien.MaBV == "30005") ? (string.Join(";", kq.Where(p => p.TenKPNX != null && p.TenKPNX.Trim() != "").Select(p => p.TenKPNX).Distinct())) : "",
                                      });
                            var q = q2.OrderBy(p => p.Ngaythang).ToList();
                            if (q.Count > 0)
                            {
                                List<BaoCao.repsothekho27023.lds> _lds = new List<BaoCao.repsothekho27023.lds>();
                                _lds = (from nx in data.NhapDs.Where(p => p.MaKP == _makho).Where(p => p.NgayNhap < ngaytu)
                                        where nx.PLoai == 1 || nx.PLoai == 2 || nx.PLoai == 3
                                        join nxct in data.NhapDcts on nx.IDNhap equals nxct.IDNhap
                                        select new BaoCao.repsothekho27023.lds
                                        {
                                            PLoai = nx.PLoai ?? 0,
                                            SLN = nxct.SoLuongN,
                                            SLX = nxct.SoLuongX,
                                            MaKP = nx.MaKP ?? 0,
                                            MaDV = nxct.MaDV ?? 0,
                                        }).ToList();
                                BaoCao.repsothekho27023 rep = new BaoCao.repsothekho27023(_lds);
                                rep.PhanTrang.Value = chkPhanTrang.Checked;
                                if (IdTieuNhom != -1)
                                {
                                    rep.TieuNhom.Value = lupTieuNhom.Text.ToUpper();
                                }
                                rep.CQCQ.Value = DungChung.Bien.TenCQCQ.ToUpper();
                                rep.TenDV.Value = DungChung.Bien.TenCQ.ToUpper();
                                //rep.TenDV.Value = lupTenDV.Text;
                                rep.TenHH.Value = lupTenDV.Text;
                                rep.ngaytu.Value = ngaytu;
                                rep.Madv.Value = _Maduoc;
                                rep.Ngaythang.Value = ngayden;
                                rep.Khoaphong.Value = lupKho.EditValue.ToString();
                                var a = (from dv in data.DichVus.Where(p => p.MaDV == _Maduoc) select new { dv.HamLuong, dv.DonVi, dv.MaDV, dv.TenDV }).ToList();
                                int _Kho = 0;
                                rep.Nhacc.Value = lupnhathau.Text;
                                _Kho = lupKho.EditValue == null ? 0 : Convert.ToInt32(lupKho.EditValue);
                                string Ncc = lupnhathau.EditValue == null ? "" : lupnhathau.EditValue.ToString();
                                rep.DataSource = q.ToList();
                                rep.BindingData();
                                rep.CreateDocument();
                                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                frm.ShowDialog();
                            }
                            else
                            { MessageBox.Show("Không có dữ liệu"); }
                        }
                        #endregion
                        #region Không gộp thuốc theo ngày
                        else
                        {
                            var q2 = (from t in qtong
                                      group new { t } by new
                                      {
                                          t.nxct.MaDV,
                                          t.dv.TenDV,
                                          t.dv.MaCC,
                                          t.nx.NgayNhap,
                                          t.nx.IDNhap,
                                          t.nxct.SoLo,
                                          t.nxct.HanDung,
                                          t.nx.SoCT,
                                          t.nx.PLoai,
                                          t.nx.MaKP,
                                          t.nx.GhiChu,
                                          t.Kh.TenKP,
                                          t.dv.QCPC,
                                          t.dv.DonVi,
                                          t.nxct.DonGia,
                                          t.nx.KieuDon,
                                          t.nx.MaKPnx,
                                          t.nx.MaBNhan,
                                          t.dv.TenHC
                                      } into kq
                                      select new
                                      {
                                          Ngaythang = kq.Key.NgayNhap,
                                          TenDV = (DungChung.Bien.MaBV == "27022" && _makho == 13 && !string.IsNullOrEmpty(kq.Key.TenHC) && !kq.Key.TenDV.Equals(kq.Key.TenHC)) ? kq.Key.TenDV + " (" + kq.Key.TenHC + ") " : kq.Key.TenDV,
                                          kq.Key.MaDV,
                                          kq.Key.QCPC,
                                          kq.Key.DonVi,
                                          SCT = kq.Key.SoCT,
                                          SoLo = kq.Key.SoLo,
                                          HanDung = kq.Key.HanDung,
                                          MaKP = kq.Key.MaKP,
                                          DonGia = kq.Key.DonGia,
                                          Soluongton = kq.Where(p => p.t.nx.PLoai == 1).Sum(p => p.t.nxct.SoLuongN) - kq.Where(p => p.t.nx.PLoai == 2 || p.t.nx.PLoai == 3).Sum(p => p.t.nxct.SoLuongX),
                                          SLNhap = kq.Sum(p => p.t.nxct.SoLuongN),
                                          SLXuat = kq.Where(p => p.t.nx.PLoai == 2 || p.t.nx.PLoai == 3).Sum(p => p.t.nxct.SoLuongX),
                                          Ton = kq.Sum(p => p.t.nxct.SoLuongN) - kq.Where(p => p.t.nx.PLoai == 2 || p.t.nx.PLoai == 3).Sum(p => p.t.nxct.SoLuongX),
                                          ThanhTienTonDauKy = kq.Key.DonGia * (kq.Where(p => p.t.nx.PLoai == 1).Sum(p => p.t.nxct.SoLuongN) - kq.Where(p => p.t.nx.PLoai == 2 || p.t.nx.PLoai == 3).Sum(p => p.t.nxct.SoLuongX)),
                                          ThanhTienNhap = kq.Key.DonGia * (kq.Sum(p => p.t.nxct.SoLuongN)),
                                          ThanhTienXuat = kq.Key.DonGia * (kq.Where(p => p.t.nx.PLoai == 2 || p.t.nx.PLoai == 3).Sum(p => p.t.nxct.SoLuongX)),
                                          ThanhTienTonCK = kq.Key.DonGia * (kq.Sum(p => p.t.nxct.SoLuongN) - kq.Where(p => p.t.nx.PLoai == 2 || p.t.nx.PLoai == 3).Sum(p => p.t.nxct.SoLuongX)),
                                          Phanloai = kq.Key.PLoai,
                                          GChu = (DungChung.Bien.MaBV != "12122" && DungChung.Bien.MaBV != "27023") ? kq.Key.GhiChu : (kq.Key.PLoai == 1 ? (kq.Key.KieuDon == 1 ? _lCC.Where(p => p.MaCC == kq.Key.MaCC).Select(p => p.TenCC).FirstOrDefault() : _lKP.Where(p => p.MaKP == kq.Key.MaKPnx).Select(p => p.TenKP).FirstOrDefault()) : (kq.Key.KieuDon == 0 ? (kq.Key.MaBNhan == null ? "" : kq.Key.MaBNhan.ToString()) : _lKP.Where(p => p.MaKP == kq.Key.MaKPnx).Select(p => p.TenKP).FirstOrDefault())),

                                          Ghichu = (DungChung.Bien.MaBV == "30005") ? _lKP.Where(p => p.MaKP == kq.Key.MaKPnx).Select(p => p.TenKP).FirstOrDefault() : "",
                                      });
                            var q = q2.OrderBy(p => p.Ngaythang).ToList();
                            if (q.Count > 0)
                            {
                                List<BaoCao.repsothekho27023.lds> _lds = new List<BaoCao.repsothekho27023.lds>();
                                _lds = (from nx in data.NhapDs.Where(p => p.MaKP == _makho).Where(p => p.NgayNhap < ngaytu)
                                        where nx.PLoai == 1 || nx.PLoai == 2 || nx.PLoai == 3
                                        join nxct in data.NhapDcts on nx.IDNhap equals nxct.IDNhap
                                        select new BaoCao.repsothekho27023.lds
                                        {
                                            PLoai = nx.PLoai ?? 0,
                                            SLN = nx.PLoai == 1 ? nxct.SoLuongN : 0,
                                            SLX = (nx.PLoai == 2 || nx.PLoai == 3) ? nxct.SoLuongX : 0,
                                            MaKP = nx.MaKP ?? 0,
                                            MaDV = nxct.MaDV ?? 0,
                                        }).ToList();
                                BaoCao.repsothekho27023 rep = new BaoCao.repsothekho27023(_lds);
                                rep.PhanTrang.Value = chkPhanTrang.Checked;
                                if (IdTieuNhom != -1)
                                {
                                    rep.TieuNhom.Value = lupTieuNhom.Text.ToUpper();
                                }
                                rep.CQCQ.Value = DungChung.Bien.TenCQCQ.ToUpper();
                                rep.TenDV.Value = DungChung.Bien.TenCQ.ToUpper();
                                rep.TenHH.Value = lupTenDV.Text;
                                rep.ngaytu.Value = ngaytu;
                                rep.Madv.Value = _Maduoc;
                                rep.Ngaythang.Value = ngayden;
                                rep.Khoaphong.Value = lupKho.EditValue.ToString();
                                var a = (from dv in data.DichVus.Where(p => p.MaDV == _Maduoc) select new { dv.HamLuong, dv.DonVi, dv.MaDV, dv.TenDV }).ToList();
                                int _Kho = 0;
                                rep.Nhacc.Value = lupnhathau.Text;
                                _Kho = lupKho.EditValue == null ? 0 : Convert.ToInt32(lupKho.EditValue);
                                string Ncc = lupnhathau.EditValue == null ? "" : lupnhathau.EditValue.ToString();
                                rep.DataSource = q.ToList();
                                rep.BindingData();
                                rep.CreateDocument();
                                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                frm.ShowDialog();
                            }
                            //}
                            else
                            { MessageBox.Show("Không có dữ liệu"); }
                        }
                        #endregion
                    }
                    else
                    {
                        #region Gộp theo ngày
                        if (radTheoNgay.Checked)
                        {
                            var qtong1 = (from a in qtong
                                          select new
                                          {
                                              NgayNhap = a.nx.NgayNhap.Value.Date,
                                              
                                              a.nx.IDNhap,
                                              a.nxx.HamLuong,
                                              a.nx.PLoai,
                                              a.nx.SoCT,
                                              a.nx.MaKP,
                                              a.nx.GhiChu,
                                              a.nx.KieuDon,
                                              a.nx.MaKPnx,
                                              a.nx.MaBNhan,
                                              a.nxct.MaDV,
                                              a.nxct.SoLo,
                                              a.nxct.HanDung,
                                              SoLuongN = a.nx.PLoai == 1 ? a.nxct.SoLuongN : 0,
                                              SoLuongX = (a.nx.PLoai == 2 || a.nx.PLoai == 3) ? a.nxct.SoLuongX : 0,
                                              a.nxct.DonGia,
                                              a.dv.TenDV,
                                              a.dv.MaCC,
                                              a.Kh.TenKP,
                                              a.dv.QCPC,
                                              a.dv.DonVi,
                                              TenKPNX = _lKP.Where(p => p.MaKP == a.nx.MaKPnx).Select(p => p.TenKP).FirstOrDefault(),
                                              GChu = (DungChung.Bien.MaBV != "12122" && DungChung.Bien.MaBV != "27023") ? a.nx.GhiChu : (a.nx.PLoai == 1 ? (a.nx.KieuDon == 1 ? _lCC.Where(p => p.MaCC == a.dv.MaCC).Select(p => p.TenCC).FirstOrDefault() : _lKP.Where(p => p.MaKP == a.nx.MaKPnx).Select(p => p.TenKP).FirstOrDefault()) : (a.nx.KieuDon == 0 ? (a.nx.MaBNhan == null ? "" : a.nx.MaBNhan.ToString()) : _lKP.Where(p => p.MaKP == a.nx.MaKPnx).Select(p => p.TenKP).FirstOrDefault())),
                                          }).ToList();
                            var q2 = (from t in qtong1
                                      group t by new
                                      {
                                          t.HamLuong,
                                          t.MaDV,
                                          t.TenDV,
                                          t.MaCC,
                                          t.NgayNhap,
                                          t.SoLo,
                                          t.HanDung,
                                          t.PLoai,
                                          t.TenKP,
                                          t.DonVi,
                                          t.DonGia,
                                      } into kq
                                      select new
                                      {
                                          Ngaythang = kq.Key.NgayNhap,
                                          kq.Key.HamLuong,
                                          kq.Key.TenDV,
                                          kq.Key.MaDV,
                                          QCPC = string.Join(";", kq.Where(p => p.QCPC != null && p.QCPC.Trim() != "").Select(p => p.QCPC).Distinct()),
                                          kq.Key.DonVi,
                                          SCT = string.Join(";", kq.Where(p => p.SoCT != null && p.SoCT.Trim() != "").Select(p => p.SoCT).Distinct()),
                                          SoLo = kq.Key.SoLo,
                                          HanDung = kq.Key.HanDung,
                                          Soluongton = kq.Sum(p => p.SoLuongN) - kq.Sum(p => p.SoLuongX),
                                          SLNhap = kq.Sum(p => p.SoLuongN),
                                          SLXuat = kq.Sum(p => p.SoLuongX),
                                          Ton = kq.Sum(p => p.SoLuongN) - kq.Sum(p => p.SoLuongX),
                                          ThanhTienTonDauKy = kq.Key.DonGia * (kq.Sum(p => p.SoLuongN) - kq.Sum(p => p.SoLuongX)),
                                          ThanhTienNhap = kq.Key.DonGia * (kq.Sum(p => p.SoLuongN)),
                                          ThanhTienXuat = kq.Key.DonGia * (kq.Sum(p => p.SoLuongX)),
                                          ThanhTienTonCK = kq.Key.DonGia * (kq.Sum(p => p.SoLuongN) - kq.Sum(p => p.SoLuongX)),
                                          DonGia = kq.Key.DonGia,
                                          Phanloai = kq.Key.PLoai,
                                          GChu = (string.Join(";", kq.Where(p => p.GChu != null && p.GChu.Trim() != "").Select(p => p.GChu).Distinct())),
                                          Ghichu = (DungChung.Bien.MaBV == "30005") ? (string.Join(";", kq.Where(p => p.TenKPNX != null && p.TenKPNX.Trim() != "").Select(p => p.TenKPNX).Distinct())) : "",
                                      });
                            var q = q2.OrderBy(p => p.Ngaythang).ToList();
                            if (q.Count > 0)
                            {
                                List<BaoCao.repsothekho27023.lds> _lds = new List<BaoCao.repsothekho27023.lds>();
                                _lds = (from nx in data.NhapDs.Where(p => p.MaKP == _makho).Where(p => p.NgayNhap < ngaytu)
                                        where nx.PLoai == 1 || nx.PLoai == 2 || nx.PLoai == 3
                                        join nxct in data.NhapDcts on nx.IDNhap equals nxct.IDNhap
                                        select new BaoCao.repsothekho27023.lds
                                        {
                                            PLoai = nx.PLoai ?? 0,
                                            SLN = nxct.SoLuongN,
                                            SLX = nxct.SoLuongX,
                                            MaKP = nx.MaKP ?? 0,
                                            MaDV = nxct.MaDV ?? 0,
                                        }).ToList();
                                BaoCao.repsothekho27023 rep = new BaoCao.repsothekho27023(_lds);
                                rep.PhanTrang.Value = chkPhanTrang.Checked;
                                if (IdTieuNhom != -1)
                                {
                                    rep.TieuNhom.Value = lupTieuNhom.Text.ToUpper();
                                }
                                rep.CQCQ.Value = DungChung.Bien.TenCQCQ.ToUpper();
                                rep.TenDV.Value = DungChung.Bien.TenCQ.ToUpper();
                                //rep.TenDV.Value = lupTenDV.Text;
                                rep.TenHH.Value = lupTenDV.Text;
                                rep.ngaytu.Value = ngaytu;
                                rep.Madv.Value = _Maduoc;
                                rep.Ngaythang.Value = ngayden;
                                rep.Khoaphong.Value = lupKho.EditValue.ToString();
                                var a = (from dv in data.DichVus.Where(p => p.MaDV == _Maduoc) select new { dv.HamLuong, dv.DonVi, dv.MaDV, dv.TenDV }).ToList();
                                int _Kho = 0;
                                rep.Nhacc.Value = lupnhathau.Text;
                                _Kho = lupKho.EditValue == null ? 0 : Convert.ToInt32(lupKho.EditValue);
                                string Ncc = lupnhathau.EditValue == null ? "" : lupnhathau.EditValue.ToString();
                                rep.DataSource = q.ToList();
                                rep.BindingData();
                                rep.CreateDocument();
                                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                frm.ShowDialog();
                            }
                            //}
                            else
                            { MessageBox.Show("Không có dữ liệu"); }
                        }
                        #endregion
                        #region Gộp theo tháng
                        else if (radTheoThang.Checked)
                        {
                            var qtong1 = (from a in qtong
                                          select new
                                          {
                                              NgayNhap = a.nx.NgayNhap.Value.Date,
                                              a.nx.IDNhap,
                                              a.nxx.HamLuong,
                                              a.nx.PLoai,
                                              a.nx.SoCT,
                                              a.nx.MaKP,
                                              a.nx.GhiChu,
                                              a.nx.KieuDon,
                                              a.nx.MaKPnx,
                                              a.nx.MaBNhan,
                                              a.nxct.MaDV,
                                              a.nxct.SoLo,
                                              a.nxct.HanDung,
                                              SoLuongN = a.nx.PLoai == 1 ? a.nxct.SoLuongN : 0,
                                              SoLuongX = (a.nx.PLoai == 2 || a.nx.PLoai == 3) ? a.nxct.SoLuongX : 0,
                                              a.nxct.DonGia,
                                              a.dv.TenDV,
                                              a.dv.MaCC,
                                              a.Kh.TenKP,
                                              a.dv.QCPC,
                                              a.dv.DonVi,
                                              TenKPNX = _lKP.Where(p => p.MaKP == a.nx.MaKPnx).Select(p => p.TenKP).FirstOrDefault(),
                                              GChu = (DungChung.Bien.MaBV != "12122" && DungChung.Bien.MaBV != "27023") ? a.nx.GhiChu : (a.nx.PLoai == 1 ? (a.nx.KieuDon == 1 ? _lCC.Where(p => p.MaCC == a.dv.MaCC).Select(p => p.TenCC).FirstOrDefault() : _lKP.Where(p => p.MaKP == a.nx.MaKPnx).Select(p => p.TenKP).FirstOrDefault()) : (a.nx.KieuDon == 0 ? (a.nx.MaBNhan == null ? "" : a.nx.MaBNhan.ToString()) : _lKP.Where(p => p.MaKP == a.nx.MaKPnx).Select(p => p.TenKP).FirstOrDefault())),
                                          }).ToList();
                            var q2 = (from t in qtong1
                                      group t by new
                                      {
                                          t.MaDV,
                                          t.HamLuong,
                                          t.TenDV,
                                          t.MaCC,
                                          t.NgayNhap,
                                          t.SoLo,
                                          t.HanDung,
                                          t.PLoai,
                                          t.TenKP,
                                          t.DonVi,
                                          t.DonGia,
                                      } into kq
                                      select new
                                      {
                                          Ngaythang = kq.Key.NgayNhap,
                                          kq.Key.HamLuong,
                                          kq.Key.TenDV,
                                          kq.Key.MaDV,
                                          QCPC = string.Join(";", kq.Where(p => p.QCPC != null && p.QCPC.Trim() != "").Select(p => p.QCPC).Distinct()),
                                          kq.Key.DonVi,
                                          SCT = string.Join(";", kq.Where(p => p.SoCT != null && p.SoCT.Trim() != "").Select(p => p.SoCT).Distinct()),
                                          SoLo = kq.Key.SoLo,
                                          HanDung = kq.Key.HanDung,
                                          Soluongton = kq.Sum(p => p.SoLuongN) - kq.Sum(p => p.SoLuongX),
                                          SLNhap = kq.Sum(p => p.SoLuongN),
                                          SLXuat = kq.Sum(p => p.SoLuongX),
                                          Ton = kq.Sum(p => p.SoLuongN) - kq.Sum(p => p.SoLuongX),
                                          ThanhTienTonDauKy = kq.Key.DonGia * (kq.Sum(p => p.SoLuongN) - kq.Sum(p => p.SoLuongX)),
                                          ThanhTienNhap = kq.Key.DonGia * (kq.Sum(p => p.SoLuongN)),
                                          ThanhTienXuat = kq.Key.DonGia * (kq.Sum(p => p.SoLuongX)),
                                          ThanhTienTonCK = kq.Key.DonGia * (kq.Sum(p => p.SoLuongN) - kq.Sum(p => p.SoLuongX)),
                                          DonGia = kq.Key.DonGia,
                                          Phanloai = kq.Key.PLoai,
                                          GChu = (string.Join(";", kq.Where(p => p.GChu != null && p.GChu.Trim() != "").Select(p => p.GChu).Distinct())),
                                          Ghichu = (DungChung.Bien.MaBV == "30005") ? (string.Join(";", kq.Where(p => p.TenKPNX != null && p.TenKPNX.Trim() != "").Select(p => p.TenKPNX).Distinct())) : "",
                                      });
                            var q = q2.OrderBy(p => p.Ngaythang).ToList();

                            var q4 = (from a in q
                                      group a by new
                                      {
                                          a.Ngaythang.Month,
                                          a.MaDV,
                                          a.TenDV,
                                          a.DonGia,
                                          a.HamLuong
                                      } into kq
                                      select new
                                      {
                                          Ngaythang = kq.Key.Month,
                                          kq.Key.HamLuong,
                                          kq.Key.TenDV,
                                          kq.Key.MaDV,
                                          Soluongton = kq.Sum(p => p.Soluongton),
                                          SLNhap = kq.Sum(p => p.SLNhap),
                                          SLXuat = kq.Sum(p => p.SLXuat),
                                          Ton = kq.Sum(p => p.Ton),
                                          DonGia = kq.Key.DonGia,
                                      }).ToList().OrderBy(p => p.Ngaythang).ToList();

                            if (q4.Count > 0)
                            {
                                List<BaoCao.repsothekho27023.lds> _lds = new List<BaoCao.repsothekho27023.lds>();
                                _lds = (from nx in data.NhapDs.Where(p => p.MaKP == _makho).Where(p => p.NgayNhap < ngaytu)
                                        where nx.PLoai == 1 || nx.PLoai == 2 || nx.PLoai == 3
                                        join nxct in data.NhapDcts on nx.IDNhap equals nxct.IDNhap
                                        select new BaoCao.repsothekho27023.lds
                                        {
                                            PLoai = nx.PLoai ?? 0,
                                            SLN = nxct.SoLuongN,
                                            SLX = nxct.SoLuongX,
                                            MaKP = nx.MaKP ?? 0,
                                            MaDV = nxct.MaDV ?? 0,
                                        }).ToList();
                                BaoCao.repsothekho27023 rep = new BaoCao.repsothekho27023(_lds);
                                rep.PhanTrang.Value = chkPhanTrang.Checked;
                                if (IdTieuNhom != -1)
                                {
                                    rep.TieuNhom.Value = lupTieuNhom.Text.ToUpper();
                                }
                                rep.CQCQ.Value = DungChung.Bien.TenCQCQ.ToUpper();
                                rep.TenDV.Value = DungChung.Bien.TenCQ.ToUpper();
                                //rep.TenDV.Value = lupTenDV.Text;
                                rep.TenHH.Value = lupTenDV.Text;
                                rep.ngaytu.Value = ngaytu;
                                rep.Madv.Value = _Maduoc;
                                rep.Ngaythang.Value = ngayden;
                                rep.Khoaphong.Value = lupKho.EditValue.ToString();
                                var a = (from dv in data.DichVus.Where(p => p.MaDV == _Maduoc) select new { dv.HamLuong, dv.DonVi, dv.MaDV, dv.TenDV }).ToList();
                                int _Kho = 0;
                                rep.Nhacc.Value = lupnhathau.Text;
                                _Kho = lupKho.EditValue == null ? 0 : Convert.ToInt32(lupKho.EditValue);
                                string Ncc = lupnhathau.EditValue == null ? "" : lupnhathau.EditValue.ToString();
                                rep.DataSource = q4.ToList();
                                rep.BindingData();
                                rep.CreateDocument();
                                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                frm.ShowDialog();
                            }
                            //}
                            else
                            { MessageBox.Show("Không có dữ liệu"); }
                        }
                        #endregion
                    }
                }
                else
                {
                    if (DungChung.Bien.MaBV != "30009")
                    {
                        #region gộp thuốc theo ngày
                        if (ckGopThuocTheoNgay.Checked)
                        {
                            var qtong1 = (from a in qtong
                                          select new
                                          {
                                              NgayNhap = a.nx.NgayNhap.Value.Date,
                                              a.nx.IDNhap,
                                              a.nx.PLoai,
                                              a.nx.SoCT,
                                              a.nx.MaKP,
                                              a.nx.GhiChu,
                                              a.nx.KieuDon,
                                              a.nx.MaKPnx,
                                              a.nx.MaBNhan,
                                              a.nxct.MaDV,
                                              a.nxct.SoLo,
                                              a.nxct.HanDung,
                                              SoLuongN = a.nx.PLoai == 1 ? a.nxct.SoLuongN : 0,
                                              SoLuongX = (a.nx.PLoai == 2 || a.nx.PLoai == 3) ? a.nxct.SoLuongX : 0,
                                              a.dv.DonGia,
                                              a.dv.TenDV,
                                              a.dv.MaCC,
                                              a.Kh.TenKP,
                                              a.dv.QCPC,
                                              a.dv.DonVi,
                                              a.dv.TenHC,
                                              TenKPNX = _lKP.Where(p => p.MaKP == a.nx.MaKPnx).Select(p => p.TenKP).FirstOrDefault(),
                                              GChu = (DungChung.Bien.MaBV != "12122" && DungChung.Bien.MaBV != "27023") ? a.nx.GhiChu : (a.nx.PLoai == 1 ? (a.nx.KieuDon == 1 ? _lCC.Where(p => p.MaCC == a.dv.MaCC).Select(p => p.TenCC).FirstOrDefault() : _lKP.Where(p => p.MaKP == a.nx.MaKPnx).Select(p => p.TenKP).FirstOrDefault()) : (a.nx.KieuDon == 0 ? (a.nx.MaBNhan == null ? "" : a.nx.MaBNhan.ToString()) : _lKP.Where(p => p.MaKP == a.nx.MaKPnx).Select(p => p.TenKP).FirstOrDefault())),
                                          }).ToList();
                            var q2 = (from t in qtong1
                                      group t by new
                                      {
                                          t.MaDV,
                                          t.TenDV,
                                          t.MaCC,
                                          t.NgayNhap,
                                          t.SoLo,
                                          t.HanDung,
                                          t.PLoai,
                                          t.TenKP,
                                          t.DonVi,
                                          t.TenHC,
                                          t.MaKP
                                      } into kq
                                      select new
                                      {
                                          Ngaythang = kq.Key.NgayNhap,
                                          kq.Key.MaDV,
                                          QCPC = string.Join(";", kq.Where(p => p.QCPC != null && p.QCPC.Trim() != "").Select(p => p.QCPC).Distinct()),
                                          kq.Key.DonVi,
                                          SCT = string.Join(";", kq.Where(p => p.SoCT != null && p.SoCT.Trim() != "").Select(p => p.SoCT).Distinct()),
                                          SoLo = kq.Key.SoLo,
                                          HanDung = kq.Key.HanDung,
                                          Soluongton = kq.Sum(p => p.SoLuongN) - kq.Sum(p => p.SoLuongX),
                                          SLNhap = kq.Sum(p => p.SoLuongN),
                                          SLXuat = kq.Sum(p => p.SoLuongX),
                                          Ton = kq.Sum(p => p.SoLuongN) - kq.Sum(p => p.SoLuongX),
                                          Phanloai = kq.Key.PLoai,
                                          TenDV = (DungChung.Bien.MaBV == "27022" && _makho == 13 && !string.IsNullOrEmpty(kq.Key.TenHC) && !kq.Key.TenDV.Equals(kq.Key.TenHC)) ? kq.Key.TenDV + " (" + kq.Key.TenHC + ") " : kq.Key.TenDV,
                                          GChu = (string.Join(";", kq.Where(p => p.GChu != null && p.GChu.Trim() != "").Select(p => p.GChu).Distinct())),
                                          Ghichu = (DungChung.Bien.MaBV == "30005") ? (string.Join(";", kq.Where(p => p.TenKPNX != null && p.TenKPNX.Trim() != "").Select(p => p.TenKPNX).Distinct())) : "",
                                      });
                            var q = q2.OrderBy(p => p.Ngaythang).ToList();
                            if (q.Count > 0)
                            {
                                List<BaoCao.repsothekho.lds> _lds = new List<BaoCao.repsothekho.lds>();
                                _lds = (from nx in data.NhapDs.Where(p => p.MaKP == _makho).Where(p => p.NgayNhap < ngaytu)
                                        where nx.PLoai == 1 || nx.PLoai == 2 || nx.PLoai == 3
                                        join nxct in data.NhapDcts on nx.IDNhap equals nxct.IDNhap
                                        select new BaoCao.repsothekho.lds
                                        {
                                            PLoai = nx.PLoai ?? 0,
                                            SLN = nxct.SoLuongN,
                                            SLX = nxct.SoLuongX,
                                            MaKP = nx.MaKP ?? 0,
                                            MaDV = nxct.MaDV ?? 0,
                                        }).ToList();
                                BaoCao.repsothekho rep = new BaoCao.repsothekho(_lds);
                                rep.PhanTrang.Value = chkPhanTrang.Checked;
                                if (IdTieuNhom != -1)
                                {
                                    rep.TieuNhom.Value = lupTieuNhom.Text.ToUpper();
                                }
                                rep.CQCQ.Value = DungChung.Bien.TenCQCQ.ToUpper();
                                rep.TenDV.Value = DungChung.Bien.TenCQ.ToUpper();
                                //rep.TenDV.Value = lupTenDV.Text;
                                rep.TenHH.Value = lupTenDV.Text;
                                rep.ngaytu.Value = ngaytu;
                                rep.Madv.Value = _Maduoc;
                                rep.Ngaythang.Value = ngayden;
                                rep.Khoaphong.Value = lupKho.EditValue.ToString();
                                var a = (from dv in data.DichVus.Where(p => p.MaDV == _Maduoc) select new { dv.HamLuong, dv.DonVi, dv.MaDV, dv.TenDV }).ToList();
                                int _Kho = 0;
                                rep.Nhacc.Value = lupnhathau.Text;
                                _Kho = lupKho.EditValue == null ? 0 : Convert.ToInt32(lupKho.EditValue);
                                string Ncc = lupnhathau.EditValue == null ? "" : lupnhathau.EditValue.ToString();
                                rep.DataSource = q.ToList();
                                rep.BindingData();
                                rep.CreateDocument();
                                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                frm.ShowDialog();
                            }
                            //}
                            else
                            { MessageBox.Show("Không có dữ liệu"); }
                        }
                        #endregion
                        #region Không gộp thuốc theo ngày
                        else
                        {
                            var q2 = (from t in qtong
                                      group new { t } by new
                                      {
                                          t.nxct.MaDV,
                                          t.dv.TenDV,
                                          t.dv.MaCC,
                                          t.nx.NgayNhap,
                                          t.nx.IDNhap,
                                          t.nxct.SoLo,
                                          t.nxct.HanDung,
                                          t.nx.SoCT,
                                          t.nx.PLoai,
                                          t.nx.MaKP,
                                          t.nx.GhiChu,
                                          t.Kh.TenKP,
                                          t.dv.QCPC,
                                          t.dv.DonVi,
                                          t.nx.KieuDon,
                                          t.nx.MaKPnx,
                                          t.nx.MaBNhan,
                                          t.dv.TenHC
                                      } into kq
                                      select new
                                      {
                                          Ngaythang = kq.Key.NgayNhap,
                                          kq.Key.MaDV,
                                          kq.Key.QCPC,
                                          kq.Key.DonVi,
                                          SCT = kq.Key.SoCT,
                                          SoLo = kq.Key.SoLo,
                                          HanDung = kq.Key.HanDung,
                                          MaKP = kq.Key.MaKP,
                                          TenDV = (DungChung.Bien.MaBV == "27022" && _makho == 13 && !string.IsNullOrEmpty(kq.Key.TenHC) && !kq.Key.TenDV.Equals(kq.Key.TenHC)) ? kq.Key.TenDV + " (" + kq.Key.TenHC + ") " : kq.Key.TenDV,
                                          Soluongton = kq.Where(p => p.t.nx.PLoai == 1).Sum(p => p.t.nxct.SoLuongN) - kq.Where(p => p.t.nx.PLoai == 2 || p.t.nx.PLoai == 3).Sum(p => p.t.nxct.SoLuongX),
                                          SLNhap = kq.Sum(p => p.t.nxct.SoLuongN),
                                          SLXuat = kq.Where(p => p.t.nx.PLoai == 2 || p.t.nx.PLoai == 3).Sum(p => p.t.nxct.SoLuongX),
                                          Ton = kq.Sum(p => p.t.nxct.SoLuongN) - kq.Where(p => p.t.nx.PLoai == 2 || p.t.nx.PLoai == 3).Sum(p => p.t.nxct.SoLuongX),
                                          Phanloai = kq.Key.PLoai,
                                          GChu = (DungChung.Bien.MaBV != "12122" && DungChung.Bien.MaBV != "27023") ? kq.Key.GhiChu : (kq.Key.PLoai == 1 ? (kq.Key.KieuDon == 1 ? _lCC.Where(p => p.MaCC == kq.Key.MaCC).Select(p => p.TenCC).FirstOrDefault() : _lKP.Where(p => p.MaKP == kq.Key.MaKPnx).Select(p => p.TenKP).FirstOrDefault()) : (kq.Key.KieuDon == 0 ? (kq.Key.MaBNhan == null ? "" : kq.Key.MaBNhan.ToString()) : _lKP.Where(p => p.MaKP == kq.Key.MaKPnx).Select(p => p.TenKP).FirstOrDefault())),

                                          Ghichu = (DungChung.Bien.MaBV == "30005") ? _lKP.Where(p => p.MaKP == kq.Key.MaKPnx).Select(p => p.TenKP).FirstOrDefault() : "",
                                      });
                            var q = q2.OrderBy(p => p.Ngaythang).ToList();
                            if (q.Count > 0)
                            {
                                List<BaoCao.repsothekho.lds> _lds = new List<BaoCao.repsothekho.lds>();
                                _lds = (from nx in data.NhapDs.Where(p => p.MaKP == _makho).Where(p => p.NgayNhap < ngaytu)
                                        where nx.PLoai == 1 || nx.PLoai == 2 || nx.PLoai == 3
                                        join nxct in data.NhapDcts on nx.IDNhap equals nxct.IDNhap
                                        select new BaoCao.repsothekho.lds
                                        {
                                            PLoai = nx.PLoai ?? 0,
                                            SLN = nx.PLoai == 1 ? nxct.SoLuongN : 0,
                                            SLX = (nx.PLoai == 2 || nx.PLoai == 3) ? nxct.SoLuongX : 0,
                                            MaKP = nx.MaKP ?? 0,
                                            MaDV = nxct.MaDV ?? 0,
                                        }).ToList();
                                BaoCao.repsothekho rep = new BaoCao.repsothekho(_lds);
                                rep.PhanTrang.Value = chkPhanTrang.Checked;
                                if (IdTieuNhom != -1)
                                {
                                    rep.TieuNhom.Value = lupTieuNhom.Text.ToUpper();
                                }
                                rep.CQCQ.Value = DungChung.Bien.TenCQCQ.ToUpper();
                                rep.TenDV.Value = DungChung.Bien.TenCQ.ToUpper();
                                rep.TenHH.Value = lupTenDV.Text;
                                rep.ngaytu.Value = ngaytu;
                                rep.Madv.Value = _Maduoc;
                                rep.Ngaythang.Value = ngayden;
                                rep.Khoaphong.Value = lupKho.EditValue.ToString();
                                var a = (from dv in data.DichVus.Where(p => p.MaDV == _Maduoc) select new { dv.HamLuong, dv.DonVi, dv.MaDV, dv.TenDV, dv.TenHC }).ToList();
                                int _Kho = 0;
                                rep.Nhacc.Value = lupnhathau.Text;
                                _Kho = lupKho.EditValue == null ? 0 : Convert.ToInt32(lupKho.EditValue);
                                string Ncc = lupnhathau.EditValue == null ? "" : lupnhathau.EditValue.ToString();
                                rep.DataSource = q.ToList();
                                rep.BindingData();
                                rep.CreateDocument();
                                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                frm.ShowDialog();
                            }
                            //}
                            else
                            { MessageBox.Show("Không có dữ liệu"); }
                        }
                        #endregion
                    }
                    else
                    {
                        #region Gộp theo ngày
                        if (radTheoNgay.Checked)
                        {
                            var qtong1 = (from a in qtong
                                          select new
                                          {
                                              NgayNhap = a.nx.NgayNhap.Value.Date,
                                              a.nxx.HamLuong,
                                              a.nx.IDNhap,
                                              a.nx.PLoai,
                                              a.nx.SoCT,
                                              a.nx.MaKP,
                                              a.nx.GhiChu,
                                              a.nx.KieuDon,
                                              a.nx.MaKPnx,
                                              a.nx.MaBNhan,
                                              a.nxct.MaDV,
                                              a.nxct.SoLo,
                                              a.nxct.HanDung,
                                              SoLuongN = a.nx.PLoai == 1 ? a.nxct.SoLuongN : 0,
                                              SoLuongX = (a.nx.PLoai == 2 || a.nx.PLoai == 3) ? a.nxct.SoLuongX : 0,
                                              a.dv.TenDV,
                                              a.dv.MaCC,
                                              a.Kh.TenKP,
                                              a.dv.QCPC,
                                              a.dv.DonVi,
                                              TenKPNX = _lKP.Where(p => p.MaKP == a.nx.MaKPnx).Select(p => p.TenKP).FirstOrDefault(),
                                              GChu = (DungChung.Bien.MaBV != "12122" && DungChung.Bien.MaBV != "27023") ? a.nx.GhiChu : (a.nx.PLoai == 1 ? (a.nx.KieuDon == 1 ? _lCC.Where(p => p.MaCC == a.dv.MaCC).Select(p => p.TenCC).FirstOrDefault() : _lKP.Where(p => p.MaKP == a.nx.MaKPnx).Select(p => p.TenKP).FirstOrDefault()) : (a.nx.KieuDon == 0 ? (a.nx.MaBNhan == null ? "" : a.nx.MaBNhan.ToString()) : _lKP.Where(p => p.MaKP == a.nx.MaKPnx).Select(p => p.TenKP).FirstOrDefault())),
                                          }).ToList();
                            var q2 = (from t in qtong1
                                      group t by new
                                      {
                                          t.HamLuong,
                                          t.MaDV,
                                          t.TenDV,
                                          t.MaCC,
                                          t.NgayNhap,
                                          t.SoLo,
                                          t.HanDung,
                                          t.PLoai,
                                          t.TenKP,
                                          t.DonVi,
                                      } into kq
                                      select new
                                      {
                                          Ngaythang = kq.Key.NgayNhap,
                                          kq.Key.TenDV,
                                          kq.Key.HamLuong,
                                          kq.Key.MaDV,
                                          QCPC = string.Join(";", kq.Where(p => p.QCPC != null && p.QCPC.Trim() != "").Select(p => p.QCPC).Distinct()),
                                          kq.Key.DonVi,
                                          SCT = string.Join(";", kq.Where(p => p.SoCT != null && p.SoCT.Trim() != "").Select(p => p.SoCT).Distinct()),
                                          SoLo = kq.Key.SoLo,
                                          HanDung = kq.Key.HanDung,
                                          Soluongton = kq.Sum(p => p.SoLuongN) - kq.Sum(p => p.SoLuongX),
                                          SLNhap = kq.Sum(p => p.SoLuongN),
                                          SLXuat = kq.Sum(p => p.SoLuongX),
                                          Ton = kq.Sum(p => p.SoLuongN) - kq.Sum(p => p.SoLuongX),
                                          Phanloai = kq.Key.PLoai,
                                          GChu = (string.Join(";", kq.Where(p => p.GChu != null && p.GChu.Trim() != "").Select(p => p.GChu).Distinct())),
                                          Ghichu = (DungChung.Bien.MaBV == "30005") ? (string.Join(";", kq.Where(p => p.TenKPNX != null && p.TenKPNX.Trim() != "").Select(p => p.TenKPNX).Distinct())) : "",
                                      });
                            var q = q2.OrderBy(p => p.Ngaythang).ToList();
                            if (q.Count > 0)
                            {
                                List<BaoCao.repsothekho.lds> _lds = new List<BaoCao.repsothekho.lds>();
                                _lds = (from nx in data.NhapDs.Where(p => p.MaKP == _makho).Where(p => p.NgayNhap < ngaytu)
                                        where nx.PLoai == 1 || nx.PLoai == 2 || nx.PLoai == 3
                                        join nxct in data.NhapDcts on nx.IDNhap equals nxct.IDNhap
                                        select new BaoCao.repsothekho.lds
                                        {
                                            PLoai = nx.PLoai ?? 0,
                                            SLN = nxct.SoLuongN,
                                            SLX = nxct.SoLuongX,
                                            MaKP = nx.MaKP ?? 0,
                                            MaDV = nxct.MaDV ?? 0,
                                        }).ToList();
                                BaoCao.repsothekho rep = new BaoCao.repsothekho(_lds);
                                rep.PhanTrang.Value = chkPhanTrang.Checked;
                                if (IdTieuNhom != -1)
                                {
                                    rep.TieuNhom.Value = lupTieuNhom.Text.ToUpper();
                                }
                                rep.CQCQ.Value = DungChung.Bien.TenCQCQ.ToUpper();
                                rep.TenDV.Value = DungChung.Bien.TenCQ.ToUpper();
                                //rep.TenDV.Value = lupTenDV.Text;
                                rep.TenHH.Value = lupTenDV.Text;
                                rep.ngaytu.Value = ngaytu;
                                rep.Madv.Value = _Maduoc;
                                rep.Ngaythang.Value = ngayden;
                                rep.Khoaphong.Value = lupKho.EditValue.ToString();
                                var a = (from dv in data.DichVus.Where(p => p.MaDV == _Maduoc) select new { dv.HamLuong, dv.DonVi, dv.MaDV, dv.TenDV }).ToList();
                                int _Kho = 0;
                                rep.Nhacc.Value = lupnhathau.Text;
                                _Kho = lupKho.EditValue == null ? 0 : Convert.ToInt32(lupKho.EditValue);
                                string Ncc = lupnhathau.EditValue == null ? "" : lupnhathau.EditValue.ToString();
                                rep.DataSource = q.ToList();
                                rep.BindingData();
                                rep.CreateDocument();
                                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                frm.ShowDialog();
                            }
                            //}
                            else
                            { MessageBox.Show("Không có dữ liệu"); }
                        }
                        #endregion
                        #region Gộp theo tháng
                        else if (radTheoThang.Checked)
                        {
                            var qtong1 = (from a in qtong
                                          select new
                                          {
                                              NgayNhap = a.nx.NgayNhap.Value.Date,
                                              a.nx.IDNhap,
                                              a.nxx.HamLuong,
                                              a.nx.PLoai,
                                              a.nx.SoCT,
                                              a.nx.MaKP,
                                              a.nx.GhiChu,
                                              a.nx.KieuDon,
                                              a.nx.MaKPnx,
                                              a.nx.MaBNhan,
                                              a.nxct.MaDV,
                                              a.nxct.SoLo,
                                              a.nxct.HanDung,
                                              SoLuongN = a.nx.PLoai == 1 ? a.nxct.SoLuongN : 0,
                                              SoLuongX = (a.nx.PLoai == 2 || a.nx.PLoai == 3) ? a.nxct.SoLuongX : 0,

                                              a.dv.TenDV,
                                              a.dv.MaCC,
                                              a.Kh.TenKP,
                                              a.dv.QCPC,
                                              a.dv.DonVi,
                                              TenKPNX = _lKP.Where(p => p.MaKP == a.nx.MaKPnx).Select(p => p.TenKP).FirstOrDefault(),
                                              GChu = (DungChung.Bien.MaBV != "12122" && DungChung.Bien.MaBV != "27023") ? a.nx.GhiChu : (a.nx.PLoai == 1 ? (a.nx.KieuDon == 1 ? _lCC.Where(p => p.MaCC == a.dv.MaCC).Select(p => p.TenCC).FirstOrDefault() : _lKP.Where(p => p.MaKP == a.nx.MaKPnx).Select(p => p.TenKP).FirstOrDefault()) : (a.nx.KieuDon == 0 ? (a.nx.MaBNhan == null ? "" : a.nx.MaBNhan.ToString()) : _lKP.Where(p => p.MaKP == a.nx.MaKPnx).Select(p => p.TenKP).FirstOrDefault())),
                                          }).ToList();
                            var q2 = (from t in qtong1
                                      group t by new
                                      {
                                          t.HamLuong,
                                          t.MaDV,
                                          t.TenDV,
                                          t.MaCC,
                                          t.NgayNhap,
                                          t.SoLo,
                                          t.HanDung,
                                          t.PLoai,
                                          t.TenKP,
                                          t.DonVi,
                                      } into kq
                                      select new
                                      {
                                          Ngaythang = kq.Key.NgayNhap,
                                          kq.Key.HamLuong,
                                          kq.Key.TenDV,
                                          kq.Key.MaDV,
                                          QCPC = string.Join(";", kq.Where(p => p.QCPC != null && p.QCPC.Trim() != "").Select(p => p.QCPC).Distinct()),
                                          kq.Key.DonVi,
                                          SCT = string.Join(";", kq.Where(p => p.SoCT != null && p.SoCT.Trim() != "").Select(p => p.SoCT).Distinct()),
                                          SoLo = kq.Key.SoLo,
                                          HanDung = kq.Key.HanDung,
                                          Soluongton = kq.Sum(p => p.SoLuongN) - kq.Sum(p => p.SoLuongX),
                                          SLNhap = kq.Sum(p => p.SoLuongN),
                                          SLXuat = kq.Sum(p => p.SoLuongX),
                                          Ton = kq.Sum(p => p.SoLuongN) - kq.Sum(p => p.SoLuongX),
                                          Phanloai = kq.Key.PLoai,
                                          GChu = (string.Join(";", kq.Where(p => p.GChu != null && p.GChu.Trim() != "").Select(p => p.GChu).Distinct())),
                                          Ghichu = (DungChung.Bien.MaBV == "30005") ? (string.Join(";", kq.Where(p => p.TenKPNX != null && p.TenKPNX.Trim() != "").Select(p => p.TenKPNX).Distinct())) : "",
                                      });
                            var q = q2.OrderBy(p => p.Ngaythang).ToList();

                            var q4 = (from a in q
                                      group a by new
                                      {
                                          a.Ngaythang.Month,
                                          a.MaDV,
                                          a.TenDV,
                                          a.HamLuong,
                                      } into kq
                                      select new
                                      {
                                          Ngaythang = kq.Key.Month,
                                          kq.Key.HamLuong,
                                          kq.Key.TenDV,
                                          kq.Key.MaDV,
                                          Soluongton = kq.Sum(p => p.Soluongton),
                                          SLNhap = kq.Sum(p => p.SLNhap),
                                          SLXuat = kq.Sum(p => p.SLXuat),
                                          Ton = kq.Sum(p => p.Ton),
                                      }).ToList().OrderBy(p => p.Ngaythang).ToList();

                            if (q4.Count > 0)
                            {
                                List<BaoCao.repsothekho.lds> _lds = new List<BaoCao.repsothekho.lds>();
                                _lds = (from nx in data.NhapDs.Where(p => p.MaKP == _makho).Where(p => p.NgayNhap < ngaytu)
                                        where nx.PLoai == 1 || nx.PLoai == 2 || nx.PLoai == 3
                                        join nxct in data.NhapDcts on nx.IDNhap equals nxct.IDNhap
                                        select new BaoCao.repsothekho.lds
                                        {
                                            PLoai = nx.PLoai ?? 0,
                                            SLN = nxct.SoLuongN,
                                            SLX = nxct.SoLuongX,
                                            MaKP = nx.MaKP ?? 0,
                                            MaDV = nxct.MaDV ?? 0,
                                        }).ToList();
                                BaoCao.repsothekho rep = new BaoCao.repsothekho(_lds);
                                rep.PhanTrang.Value = chkPhanTrang.Checked;
                                if (IdTieuNhom != -1)
                                {
                                    rep.TieuNhom.Value = lupTieuNhom.Text.ToUpper();
                                }
                                rep.CQCQ.Value = DungChung.Bien.TenCQCQ.ToUpper();
                                rep.TenDV.Value = DungChung.Bien.TenCQ.ToUpper();
                                //rep.TenDV.Value = lupTenDV.Text;
                                rep.TenHH.Value = lupTenDV.Text;
                                rep.ngaytu.Value = ngaytu;
                                rep.Madv.Value = _Maduoc;
                                rep.Ngaythang.Value = ngayden;
                                rep.Khoaphong.Value = lupKho.EditValue.ToString();
                                var a = (from dv in data.DichVus.Where(p => p.MaDV == _Maduoc) select new { dv.HamLuong, dv.DonVi, dv.MaDV, dv.TenDV }).ToList();
                                int _Kho = 0;
                                rep.Nhacc.Value = lupnhathau.Text;
                                _Kho = lupKho.EditValue == null ? 0 : Convert.ToInt32(lupKho.EditValue);
                                string Ncc = lupnhathau.EditValue == null ? "" : lupnhathau.EditValue.ToString();
                                rep.DataSource = q4.ToList();
                                rep.BindingData();
                                rep.CreateDocument();
                                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                frm.ShowDialog();
                            }
                            //}
                            else
                            { MessageBox.Show("Không có dữ liệu"); }
                        }
                        #endregion
                    }
                }
            }
        }

        private void lupKho_EditValueChanged(object sender, EventArgs e)
        {
           
        }
    }
}