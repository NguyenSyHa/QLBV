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
    public partial class frmSoXNT_GayNghien_HTT_PhongXa : DevExpress.XtraEditors.XtraForm
    {
        public frmSoXNT_GayNghien_HTT_PhongXa()
        {
            InitializeComponent();
        }
        private bool ktcd()
        {
            
            if (lupngay.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn ngày tháng");
                lupngay.Focus();
                return false;
            }
            return true;
        }



        private void comboBoxEdit1_EditValueChanged(object sender, EventArgs e)
        {

        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void frmThekho_Load(object sender, EventArgs e)
        {
            var tieunhom = (from nhom in data.NhomDVs.Where(p => p.Status==1) join tn in data.TieuNhomDVs on nhom.IDNhom equals tn.IDNhom select tn).ToList();
            TieuNhomDV moi = new TieuNhomDV();
            moi.IdTieuNhom = 0;
            moi.TenTN = "Tất cả";
            tieunhom.Add(moi);
            lupTieuNhom.Properties.DataSource = tieunhom.OrderBy(p => p.IdTieuNhom).ToList();
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

            var dskp = (from kp in data.KPhongs.Where(p => p.PLoai == "Khoa dược")
                        select new
                        {
                            MaKP = kp.MaKP,
                            TenKP = kp.TenKP
                        }).OrderBy(p => p.TenKP).ToList();
            cklKP.DataSource = dskp;
            for (int i = 0; i < cklKP.ItemCount; i++)
            {
                if (cklKP.GetItemValue(i) != null && Convert.ToInt32(cklKP.GetItemValue(i)) == DungChung.Bien.MaKP)
                    cklKP.SetItemChecked(i, true);
                else
                    cklKP.SetItemChecked(i, false);
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            List<KPhong> _kpChon = new List<KPhong>();
            for (int i = 0; i < cklKP.ItemCount; i++)
            {
                if (cklKP.GetItemChecked(i))
                    _kpChon.Add(new KPhong { TenKP = cklKP.GetItemText(i), MaKP = cklKP.GetItemValue(i) == null ? 0 : Convert.ToInt32(cklKP.GetItemValue(i)) });
            }
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
                int sokho = _kpChon.Count;
                DateTime ngayden = System.DateTime.Now.Date;
                DateTime ngaytu = System.DateTime.Now;
                ngaytu = DungChung.Ham.NgayTu(lupngaytu.DateTime);
                ngayden = DungChung.Ham.NgayDen(lupngay.DateTime);
                var dv1 = data.DichVus.Where(p => IdTieuNhom == -1 ? true : (p.IdTieuNhom == IdTieuNhom || IdTieuNhom == 0)).ToList();
                #region mẫu 0
                if (radMau.SelectedIndex == 0)
                {
                    var nx1 = (from nx in data.NhapDs.Where(p => p.NgayNhap >= ngaytu && p.NgayNhap <= ngayden).Where(p => p.PLoai == 1 || p.PLoai == 2 || p.PLoai == 3)
                               join nxct in data.NhapDcts on nx.IDNhap equals nxct.IDNhap
                               select new
                               {
                                   nx.NgayNhap,
                                   nx.IDNhap,
                                   nx.PLoai,
                                   nx.SoCT,
                                   nx.MaKP,
                                   nx.GhiChu,
                                   nx.KieuDon,
                                   nx.MaKPnx,
                                   MaBNhan = nx.MaBNhan,
                                   MaBNhan1 = nxct.MaBNhan,
                                   nxct.MaDV,
                                   nxct.SoLo,
                                   nxct.HanDung,
                                   SoLuongN = nx.PLoai == 1 ? nxct.SoLuongN : 0,
                                   SoLuongX = (nx.PLoai == 2 || nx.PLoai == 3) ? nxct.SoLuongX : 0,

                                   TenBNhan = ""
                               }).ToList();
                    if (DungChung.Bien.MaBV == "27001")
                    {
                        nx1 = (from nx in data.NhapDs.Where(p => p.NgayNhap >= ngaytu && p.NgayNhap <= ngayden).Where(p => p.PLoai == 1 || p.PLoai == 2 || p.PLoai == 3)
                               join nxct in data.NhapDcts on nx.IDNhap equals nxct.IDNhap
                               join bn in data.BenhNhans on nx.MaBNhan equals bn.MaBNhan into kq from kq1 in kq.DefaultIfEmpty()
                               select new
                               {
                                   nx.NgayNhap,
                                   nx.IDNhap,
                                   nx.PLoai,
                                   nx.SoCT,
                                   nx.MaKP,
                                   nx.GhiChu,
                                   nx.KieuDon,
                                   nx.MaKPnx,
                                   MaBNhan = nx.MaBNhan,
                                   MaBNhan1 = nxct.MaBNhan,
                                   nxct.MaDV,
                                   nxct.SoLo,
                                   nxct.HanDung,
                                   nxct.SoLuongN,
                                   nxct.SoLuongX,
                                   TenBNhan = kq1 == null ? "" :  kq1.TenBNhan
                               }).ToList();
                    }
                    var qkp = data.KPhongs.ToList();
                    var qtong = (from kho in _kpChon
                                 join nx in nx1 on kho.MaKP equals nx.MaKP
                                 join dv in dv1 on nx.MaDV equals dv.MaDV
                                
                                 select new
                                 {
                                     nx.NgayNhap,
                                     nx.IDNhap,
                                     nx.PLoai,
                                     nx.SoCT,
                                     nx.MaKP,
                                     nx.GhiChu,
                                     nx.KieuDon,
                                     nx.MaKPnx,
                                     nx.MaBNhan,
                                     nx.MaBNhan1,
                                     nx.MaDV,
                                     nx.SoLo,
                                     nx.HanDung,
                                     nx.SoLuongN,
                                     nx.SoLuongX,
                                     dv.TenDV,
                                     dv.MaCC,
                                     kho.TenKP,
                                     dv.QCPC,
                                     dv.DonVi,
                                     dv.HamLuong,
                                     nx.TenBNhan,
                                     GChu = DungChung.Bien.MaBV == "27001" ? nx.TenBNhan : kho.TenKP,
                                 }).ToList();

                    #region gộp thuốc theo ngày
                    if (ckGopThuocTheoNgay.Checked)
                    {
                        var qtong1 = (from a in qtong
                                      join kp in _lKP on a.MaKPnx equals kp.MaKP into kq from kq1 in kq.DefaultIfEmpty()
                                      select new
                                      {
                                          NgayNhap = a.NgayNhap.Value.Date,
                                          a.IDNhap,
                                          a.PLoai,
                                          a.SoCT,
                                          a.MaKP,
                                          a.GhiChu,
                                          a.KieuDon,
                                          a.MaKPnx,
                                          a.MaBNhan,
                                          a.MaDV,
                                          a.SoLo,
                                          a.HanDung,
                                          a.SoLuongN,
                                          a.SoLuongX,
                                          a.TenDV,
                                          a.MaCC,
                                          a.TenKP,
                                          a.QCPC,
                                          a.DonVi,
                                          a.HamLuong,
                                          TenKPNX =  kq1 == null ? "" : kq1.TenKP,//_lKP.Where(p => p.MaKP == a.MaKPnx).Select(p => p.TenKP).FirstOrDefault(),
                                          GChu = DungChung.Bien.MaBV == "27001" ? ((a.PLoai == 2 && a.KieuDon == 0) ? a.TenBNhan : (kq1 == null ? "" : kq1.TenKP)) : a.GChu, // _lKP.Where(p => p.MaKP == a.MaKP).Select(p => p.TenKP).FirstOrDefault(),
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
                                      t.HamLuong
                                  } into kq
                                  select new
                                  {
                                      Ngaythang = kq.Key.NgayNhap,
                                      TenDV = kq.Key.TenDV + " - " + kq.Key.HamLuong,
                                      kq.Key.MaDV,
                                      QCPC = string.Join(";", kq.Where(p => p.QCPC != null && p.QCPC.Trim() != "").Select(p => p.QCPC).Distinct()),
                                      kq.Key.DonVi,
                                      SCT = string.Join(";", kq.Where(p => p.SoCT != null && p.SoCT.Trim() != "").Select(p => p.SoCT).Distinct()),
                                      SoLo = kq.Key.SoLo,
                                      HanDung = kq.Key.HanDung,
                                      SoLo_HanDung = kq.Key.SoLo + " - " + (kq.Key.HanDung == null ? kq.Key.HanDung.ToString() : (kq.Key.HanDung.Value.Day + "/" + kq.Key.HanDung.Value.Month + "/" + kq.Key.HanDung.Value.Year)),
                                      Soluongton = kq.Sum(p => p.SoLuongN) - kq.Sum(p => p.SoLuongX),
                                      SLNhap = kq.Sum(p => p.SoLuongN),
                                      SLXuat = kq.Sum(p => p.SoLuongX),
                                      Ton = kq.Sum(p => p.SoLuongN) - kq.Sum(p => p.SoLuongX),
                                      Phanloai = kq.Key.PLoai,
                                      GChu = (string.Join(";", kq.Where(p => p.GChu != null && p.GChu.Trim() != "").Select(p => p.GChu).Distinct())),
                                      Ghichu = DungChung.Bien.MaBV == "27001" ? (string.Join(";", kq.Where(p => p.GChu != null && p.GChu.Trim() != "").Select(p => p.GChu).Distinct())) : (string.Join(";", kq.Where(p => p.TenKPNX != null && p.TenKPNX.Trim() != "").Select(p => p.TenKPNX).Distinct())),
                                  }).ToList();
                      //  var q = q2.OrderBy(p => p.Ngaythang).ToList();
                        if (q2.Count > 0)
                        {
                            List<BaoCao.rep_NXT_GAYNG_HTT.lds> _lds = new List<BaoCao.rep_NXT_GAYNG_HTT.lds>();
                            var nd = (from nx in data.NhapDs.Where(p => p.NgayNhap < ngaytu)
                                      where nx.PLoai == 1 || nx.PLoai == 2 || nx.PLoai == 3
                                      join nxct in data.NhapDcts on nx.IDNhap equals nxct.IDNhap
                                      select new
                                      {
                                          PLoai = nx.PLoai ?? 0,
                                          SLN = nxct.SoLuongN,
                                          SLX = nxct.SoLuongX,
                                          MaKP = nx.MaKP ?? 0,
                                          MaDV = nxct.MaDV ?? 0,
                                      }).ToList();
                            _lds = (from kho in _kpChon
                                    join nx in nd on kho.MaKP equals nx.MaKP
                                    select new BaoCao.rep_NXT_GAYNG_HTT.lds
                                    {
                                        PLoai = nx.PLoai,
                                        SLN = nx.SLN,
                                        SLX = nx.SLX,
                                        MaKP = nx.MaKP,
                                        MaDV = nx.MaDV,
                                    }).ToList();
                            BaoCao.rep_NXT_GAYNG_HTT rep = new BaoCao.rep_NXT_GAYNG_HTT(_lds);
                            rep.PhanTrang.Value = chkPhanTrang.Checked;
                            if (IdTieuNhom != -1)
                            {
                                rep.TieuNhom.Value = lupTieuNhom.Text.ToUpper();
                            }
                            rep.CQCQ.Value = DungChung.Bien.TenCQCQ.ToUpper();
                            rep.TenCQ.Value = DungChung.Bien.TenCQ.ToUpper();
                            rep.DiaChi.Value = DungChung.Bien.DiaChi.ToUpper();
                            rep.SDT.Value = DungChung.Bien.SDTCQ.ToUpper();
                            //rep.TenDV.Value = lupTenDV.Text;
                            rep.TenHH.Value = lupTenDV.Text;
                            rep.ngaytu.Value = ngaytu;
                            rep.Madv.Value = _Maduoc;
                            rep.Ngaythang.Value = "(Bắt đầu sử dụng từ " + ngaytu.Day + "/" + ngaytu.Month + "/" + ngaytu.Year + " đến " + ngayden.Day + "/" + ngayden.Month + "/" + ngayden.Year + ")";
                            string khochon = "";
                            foreach(var item in _kpChon)
                            {
                                khochon = khochon + item.TenKP + "; ";
                            }
                            rep.Khoaphong.Value = khochon;
                            var a = (from dv in data.DichVus.Where(p => p.MaDV == _Maduoc) select new { dv.HamLuong, dv.DonVi, dv.MaDV, dv.TenDV }).ToList();
                            rep.Nhacc.Value = lupnhathau.Text;
                            string Ncc = lupnhathau.EditValue == null ? "" : lupnhathau.EditValue.ToString();
                            if (DungChung.Bien.MaBV == "27001")
                                rep.DataSource = q2.OrderBy(p => p.Ngaythang).ThenBy(p => p.TenDV).ToList();
                            else
                                rep.DataSource = q2.OrderByDescending(p => p.TenDV).ToList();
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

                        var q = (from t in qtong
                                 join kp in _lKP on t.MaKPnx equals kp.MaKP into kq
                                 from kq1 in kq.DefaultIfEmpty()
                                 select new
                                 {
                                     t.MaDV,
                                     t.TenDV,
                                     t.MaCC,
                                     t.NgayNhap,
                                     t.IDNhap,
                                     t.SoLo,
                                     t.HanDung,
                                     t.SoCT,
                                     t.PLoai,
                                     t.MaKP,
                                     // t.GhiChu,
                                     t.TenKP,
                                     t.QCPC,
                                     t.DonVi,
                                     t.KieuDon,
                                     t.MaKPnx,
                                     t.MaBNhan,
                                     t.HamLuong,
                                     t.SoLuongN,
                                     t.SoLuongX,
                                     t.GChu,
                                     GhiChu = DungChung.Bien.MaBV == "27001" && t.PLoai == 2 && t.KieuDon == 0 ? t.GChu : (kq1 == null ? "" : kq1.TenKP),
                                 }).ToList();


                        var q2 = (from t in q //qtong
                                  group new { t } by new
                                  {
                                      t.MaDV,
                                      t.TenDV,
                                      t.MaCC,
                                      t.NgayNhap,
                                      t.IDNhap,
                                      t.SoLo,
                                      t.HanDung,
                                      t.SoCT,
                                      t.PLoai,
                                      t.MaKP,
                                      t.GhiChu,
                                      t.TenKP,
                                      t.QCPC,
                                      t.DonVi,
                                      t.KieuDon,
                                      t.MaKPnx,
                                      t.MaBNhan,
                                      t.HamLuong,
                                      t.GChu
                                      
                                      //t.SoLuongN,
                                      //t.SoLuongX,
                                  } into kq
                                  select new
                                  {
                                      Ngaythang = kq.Key.NgayNhap,
                                      TenDV = kq.Key.TenDV + " - " + kq.Key.HamLuong,
                                      kq.Key.MaDV,
                                      kq.Key.QCPC,
                                      kq.Key.DonVi,
                                      SCT = kq.Key.SoCT,
                                      SoLo = kq.Key.SoLo,
                                      HanDung = kq.Key.HanDung,
                                      SoLo_HanDung = kq.Key.SoLo + " - " + (kq.Key.HanDung == null ? kq.Key.HanDung.ToString() : (kq.Key.HanDung.Value.Day + "/" + kq.Key.HanDung.Value.Month + "/" + kq.Key.HanDung.Value.Year)),
                                      MaKP = kq.Key.MaKP,
                                      Soluongton = kq.Sum(p => p.t.SoLuongN) - kq.Sum(p => p.t.SoLuongX),
                                      SLNhap = kq.Sum(p => p.t.SoLuongN),
                                      SLXuat = kq.Sum(p => p.t.SoLuongX),
                                      Ton = kq.Sum(p => p.t.SoLuongN) - kq.Sum(p => p.t.SoLuongX),
                                      Phanloai = kq.Key.PLoai,
                                      GChu = kq.Key.GChu, //_lKP.Where(p => p.MaKP == kq.Key.MaKP).Select(p => p.TenKP).FirstOrDefault(),
                                      Ghichu = kq.Key.GhiChu//_lKP.Where(p => p.MaKP == kq.Key.MaKPnx).Select(p => p.TenKP).FirstOrDefault(),
                                  }).ToList();
                      //  var q = q2.Where(p => p.Ngaythang >= ngaytu && p.Ngaythang <= ngayden).OrderBy(p => p.Ngaythang).ToList();
                        if (q2.Count > 0)
                        {
                            List<BaoCao.rep_NXT_GAYNG_HTT.lds> _lds = new List<BaoCao.rep_NXT_GAYNG_HTT.lds>();
                            var nd = (from nx in data.NhapDs.Where(p => p.NgayNhap < ngaytu)
                                      where nx.PLoai == 1 || nx.PLoai == 2 || nx.PLoai == 3
                                      join nxct in data.NhapDcts on nx.IDNhap equals nxct.IDNhap
                                      select new
                                      {
                                          PLoai = nx.PLoai ?? 0,
                                          SLN = nxct.SoLuongN,
                                          SLX = nxct.SoLuongX,
                                          MaKP = nx.MaKP ?? 0,
                                          MaDV = nxct.MaDV ?? 0,
                                      }).ToList();
                            _lds = (from kho in _kpChon
                                    join nx in nd on kho.MaKP equals nx.MaKP
                                    select new BaoCao.rep_NXT_GAYNG_HTT.lds
                               {
                                   PLoai = nx.PLoai ,
                                   SLN = nx.SLN,
                                   SLX = nx.SLX,
                                   MaKP = nx.MaKP,
                                   MaDV = nx.MaDV ,
                               }).ToList();
                            BaoCao.rep_NXT_GAYNG_HTT rep = new BaoCao.rep_NXT_GAYNG_HTT(_lds);
                            rep.PhanTrang.Value = chkPhanTrang.Checked;
                            if (IdTieuNhom != -1)
                            {
                                rep.TieuNhom.Value = lupTieuNhom.Text.ToUpper();
                            }
                            rep.CQCQ.Value = DungChung.Bien.TenCQCQ.ToUpper();
                            rep.TenCQ.Value = DungChung.Bien.TenCQ.ToUpper();
                            rep.DiaChi.Value = DungChung.Bien.DiaChi.ToUpper();
                            rep.SDT.Value = DungChung.Bien.SDTCQ.ToUpper();
                            rep.TenHH.Value = lupTenDV.Text;
                            rep.ngaytu.Value = ngaytu;
                            rep.Madv.Value = _Maduoc;
                            rep.Ngaythang.Value = "(Bắt đầu sử dụng từ " + ngaytu.Day + "/" + ngaytu.Month + "/" + ngaytu.Year + " đến " + ngayden.Day + "/" + ngayden.Month + "/" + ngayden.Year + ")";
                            string khochon = "";
                            foreach (var item in _kpChon)
                            {
                                khochon = khochon + item.TenKP + "; ";
                            }
                            rep.Khoaphong.Value = khochon;
                            var a = (from dv in data.DichVus.Where(p => p.MaDV == _Maduoc) select new { dv.HamLuong, dv.DonVi, dv.MaDV, dv.TenDV }).ToList();
                            rep.Nhacc.Value = lupnhathau.Text;
                            string Ncc = lupnhathau.EditValue == null ? "" : lupnhathau.EditValue.ToString();
                            if (DungChung.Bien.MaBV == "27001")
                                rep.DataSource = q2.OrderBy(p => p.Ngaythang).ThenBy(p => p.TenDV).ToList();
                            else
                                rep.DataSource = q2.OrderBy(p => p.TenDV).ToList();
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
                #endregion
                #region mẫu 1
                else
                {
                    var nx1 = (from nx in data.NhapDs.Where(p => p.PLoai == 1 || p.PLoai == 2 || p.PLoai == 3)
                               join nxct in data.NhapDcts on nx.IDNhap equals nxct.IDNhap
                               select new
                               {
                                   nx.NgayNhap,
                                   nx.IDNhap,
                                   nx.PLoai,
                                   nx.SoCT,
                                   nx.MaKP,
                                   nx.GhiChu,
                                   nx.KieuDon,
                                   nx.MaKPnx,
                                   MaBNhan = nx.MaBNhan,
                                   MaBNhan1 = nxct.MaBNhan,
                                   nxct.MaDV,
                                   nxct.SoLo,
                                   nxct.HanDung,
                                   SoLuongN = nx.PLoai == 1 ? nxct.SoLuongN : 0,
                                   SoLuongX = (nx.PLoai == 2 || nx.PLoai == 3) ? nxct.SoLuongX : 0,

                               }).ToList();
                    var qtong = (from kho in _kpChon
                                 join nx in nx1 on kho.MaKP equals nx.MaKP
                                 join dv in dv1 on nx.MaDV equals dv.MaDV
                                 select new
                                 {
                                     nx.NgayNhap,
                                     nx.IDNhap,
                                     nx.PLoai,
                                     nx.SoCT,
                                     nx.MaKP,
                                     nx.GhiChu,
                                     nx.KieuDon,
                                     nx.MaKPnx,
                                     nx.MaBNhan,
                                     nx.MaBNhan1,
                                     nx.MaDV,
                                     nx.SoLo,
                                     nx.HanDung,
                                     nx.SoLuongN,
                                     nx.SoLuongX,
                                     dv.TenDV,
                                     dv.MaCC,
                                     kho.TenKP,
                                     dv.QCPC,
                                     dv.DonVi,
                                     dv.HamLuong,
                                 }).ToList();
                    int x =0;
                    var qtong2 = (from a in qtong
                                  group a by new { a.MaDV, a.TenDV, a.HamLuong, a.QCPC, a.DonVi} into kq
                                  select new {
                                      kq.Key.MaDV,
                                      TenDV = kq.Key.TenDV + ", " + kq.Key.QCPC + ", " + kq.Key.HamLuong,
                                      kq.Key.DonVi,
                                      SLTonDK = kq.Where(p => p.NgayNhap < ngaytu).Sum(p => p.SoLuongN) - kq.Where(p => p.NgayNhap < ngaytu).Sum(p => p.SoLuongX),
                                      SLTonCK = kq.Where(p => p.NgayNhap <= ngayden).Sum(p => p.SoLuongN) - kq.Where(p => p.NgayNhap <= ngayden).Sum(p => p.SoLuongX),
                                      SLNhap = kq.Where(p => p.NgayNhap <= ngayden && p.NgayNhap >= ngaytu && p.PLoai == 1).Where(p => (DungChung.Bien.MaBV == "12001" && sokho > 1) ? p.KieuDon == 1 : x == 0).Sum(p => p.SoLuongN),
                                      SLXuat = (DungChung.Bien.MaBV == "12001" && sokho > 1) ? kq.Where(p => p.NgayNhap <= ngayden && p.NgayNhap >= ngaytu && p.PLoai == 2).Where(p => p.KieuDon != 2).Sum(p => p.SoLuongX) - kq.Where(p => p.NgayNhap <= ngayden && p.NgayNhap >= ngaytu && p.PLoai == 1).Where(p => p.KieuDon == 2).Sum(p => p.SoLuongN) : kq.Where(p => p.NgayNhap <= ngayden && p.NgayNhap >= ngaytu && p.PLoai == 2).Sum(p => p.SoLuongX),
                                      //SLXuat = kq.Where(p => p.NgayNhap <= ngayden && p.NgayNhap >= ngaytu && p.PLoai == 2).Where(p => (DungChung.Bien.MaBV == "12001" && sokho > 1) ? (p.KieuDon != 2 && p.MaBNhan1 != null) : x == 0).Sum(p => p.SoLuongX),
                                      SLHuHao = kq.Where(p => p.NgayNhap <= ngayden && p.NgayNhap >= ngaytu && p.PLoai == 3).Sum(p => p.SoLuongX),
                                      SLTong = kq.Where(p => p.NgayNhap < ngaytu).Sum(p => p.SoLuongN) - kq.Where(p => p.NgayNhap < ngaytu).Sum(p => p.SoLuongX) + (kq.Where(p => p.NgayNhap <= ngayden && p.NgayNhap >= ngaytu && p.PLoai == 1).Where(p => (DungChung.Bien.MaBV == "12001" && sokho > 1) ? p.KieuDon == 1 : x == 0).Sum(p => p.SoLuongN))
                                  }).ToList();
                    qtong2 = qtong2.Where(p => p.SLHuHao != 0 || p.SLNhap != 0 || p.SLTonCK != 0 || p.SLTonDK != 0 || p.SLTong != 0 || p.SLXuat != 0).ToList();
                    BaoCao.rep_BCNXT_GayNghien_HTT_PhongXa rep = new BaoCao.rep_BCNXT_GayNghien_HTT_PhongXa();
                    rep.ngaythang.Text = "(Từ ngày " + ngaytu.Day + "/" + ngaytu.Month + "/" + ngaytu.Year + " đến ngày " + ngayden.Day + "/" + ngayden.Month + "/" + ngayden.Year + ")";
                    rep.ngaythang2.Text = DungChung.Bien.DiaDanh + ", ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;
                    //if (DungChung.Bien.MaBV == "27001")
                    //    rep.DataSource = qtong2.OrderBy(p => p.Ngaythang).ThenBy(p => p.TenDV).ToList();
                    //else
                    rep.DataSource = qtong2.OrderBy(p => p.TenDV).ToList();
                    rep.BindingData();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                }
            #endregion
            }
        }

        private void lupKho_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void radMau_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(radMau.SelectedIndex == 0)
            {
                ckGopThuocTheoNgay.Enabled = true;
                chkPhanTrang.Enabled = true;
            }
            else
            {
                ckGopThuocTheoNgay.Enabled = false;
                chkPhanTrang.Enabled = false ;
                ckGopThuocTheoNgay.Checked = false;
                chkPhanTrang.Checked = false;
            }
        }

        private void cklKP_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            int check = 0;
            for (int i = 0; i < cklKP.ItemCount; i++)
            {
                if (cklKP.GetItemChecked(i))
                    check++;
            }
        }
    }
}