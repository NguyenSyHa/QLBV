using System;
using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using System.Data.Entity;

namespace QLBV.FormNhap
{
    public partial class frm_TheKho : DevExpress.XtraEditors.XtraForm
    {
        public frm_TheKho()
        {
            InitializeComponent();
        }
        private bool ktcd()
        {
            if (lupTenDV.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn tên thuốc");
                lupTenDV.Focus();
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



        private void lupTenDV_EditValueChanged(object sender, EventArgs e)
        {
            int madv = 0;

            int makp = 0;
            if (lupTenDV.EditValue != null && lupKho.EditValue != null)
            {
                madv = Convert.ToInt32(lupTenDV.EditValue);

                makp = Convert.ToInt32(lupKho.EditValue);
            }
            if (madv > 0 && makp > 0)
            {
                List<clsDonGia> b0 = (from dv in data.DichVus.Where(p => p.MaDV == madv)
                                      join nxct in data.NhapDcts on dv.MaDV equals nxct.MaDV
                                      join nd in data.NhapDs.Where(p => p.MaKP == (makp)) on nxct.IDNhap equals nd.IDNhap
                                      select new clsDonGia { DonGia = nxct.DonGia }).Distinct().ToList();
                b0.Insert(0, new clsDonGia { DonGia = null });
                lupDonGia.Properties.DataSource = b0;

            }
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void frmThekho_Load(object sender, EventArgs e)
        {
            if (DungChung.Bien.MaBV == "24012")
            {
                ckngoaitru.Visible = true;
            }
            var D = from tk in data.KPhongs.Where(p => p.PLoai == ("Khoa dược")) select new { tk.TenKP, tk.MaKP };
            lupKho.Properties.DataSource = D.ToList();
            lupngaytu.DateTime = System.DateTime.Now;
            lupngay.DateTime = System.DateTime.Now;
            var c = (from ncc in data.NhaCCs select new { ncc.TenCC, ncc.MaCC }).ToList();
            if (c.Count > 0)
            {
                lupnhathau.Properties.DataSource = c;
            }
            radChon.SelectedIndex = 0;
            if (DungChung.Bien.MaBV == "27022")
            {
                checkEdit1.Visible = true;
                textEdit1.Visible = true;
            }
            else
            {
                checkEdit1.Visible = false;
                textEdit1.Visible = false;
            }
        }
        private class clsDonGia
        {
            public double? DonGia { set; get; }
        }
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (ktcd())
            {
                int _Maduoc = 0;
                double dongia = 0;
                if (lupDonGia.EditValue != null)
                    dongia = Convert.ToDouble(lupDonGia.EditValue);
                _Maduoc = lupTenDV.EditValue == null ? 0 : Convert.ToInt32(lupTenDV.EditValue);
                DateTime ngaytu = DungChung.Ham.NgayTu(lupngaytu.DateTime);
                DateTime ngayden = DungChung.Ham.NgayDen(lupngay.DateTime);
                List<KPhong> _lKP = data.KPhongs.ToList();
                List<NhaCC> _lCC = data.NhaCCs.ToList();
                var qtong = (
                    from nx in data.NhapDs
                    join nxct in data.NhapDcts.Where(p => p.MaDV == _Maduoc && (dongia == 0 || p.DonGia == dongia)) on nx.IDNhap equals nxct.IDNhap
                    join bn in data.BenhNhans.Where(p => p.NoiTru == 0 && p.DTNT == false) on nx.MaBNhan equals bn.MaBNhan into k
                    from k1 in k.DefaultIfEmpty()
                    where nx.PLoai == 1 || nx.PLoai == 2 || nx.PLoai == 3
                    select new
                    {
                        nx.MaKPnx,
                        nx.MaBNhan,
                        TenBNhan = k1 != null ? k1.TenBNhan : "",
                        nx.KieuDon,
                        SoLuongN = nx.PLoai == 1 ? nxct.SoLuongN : 0,
                        SoLuongX = (nx.PLoai == 2 || nx.PLoai == 3) ? nxct.SoLuongX : 0,
                        nx.NgayNhap,
                        nxct.MaDV,
                        nx.IDNhap,
                        nxct.SoLo,
                        nxct.HanDung,
                        nx.SoCT,
                        nxct.MaCC,
                        nx.PLoai,
                        nx.MaKP,
                        nx.GhiChu,
                        MaKPBN = k1 != null ? k1.MaKP : null
                    }).ToList();
                var q2 = (from nx in qtong
                          where ((nx.PLoai == 1 || nx.PLoai == 2 || nx.PLoai == 3) && nx.HanDung != null && nx.SoCT != "")
                          select new { nx.SoCT, nx.HanDung }).ToList().Select(p => new
                          {
                              p.SoCT,
                              HanDung = p.HanDung
                          }).ToList();


                List<DichVu> _ldv = data.DichVus.ToList();
                #region Bệnh viên khác 27022
                if (DungChung.Bien.MaBV != "27022" && DungChung.Bien.MaBV != "24012")
                {
                    if (radChon.SelectedIndex == 0)
                    {
                        frmIn frm = new frmIn();

                        var q = (from nx in qtong
                                 join dv in _ldv on nx.MaDV equals dv.MaDV
                                 //join Kh in data.KPhongs on nx.MaKP equals Kh.MaKP
                                 where (nx.NgayNhap <= ngayden && nx.NgayNhap >= ngaytu)
                                 group new { nx, dv } by new
                                 {
                                     nx.NgayNhap,
                                     dv.TenDV,
                                     nx.IDNhap,
                                     nx.SoLo,
                                     nx.HanDung,
                                     SoCT = nx.SoCT,
                                     nx.PLoai,
                                     nx.MaKP,
                                     nx.KieuDon,
                                     nx.MaCC,
                                     nx.MaKPnx,
                                     nx.GhiChu,
                                     nx.MaKPBN,
                                     nx.TenBNhan,
                                     nx.MaBNhan,
                                 } into kq
                                 select new
                                 {
                                     TenBNhan = kq.Key.TenBNhan,
                                     MaBNhan = kq.Key.MaBNhan,
                                     Ngaythang = kq.Key.NgayNhap,
                                     HanDung24012 = kq.Key.HanDung,
                                     SCTn = kq.Key.PLoai == 1 ? kq.Key.SoCT : "",
                                     SCTx = kq.Key.PLoai != 1 ? kq.Key.SoCT : "",
                                     SCT = kq.Key.SoCT,
                                     Solo = kq.Key.SoLo,
                                     MaKP = kq.Key.MaKP,
                                     Soluongton = kq.Sum(p => p.nx.SoLuongN) - kq.Sum(p => p.nx.SoLuongX),
                                     SLNhap = kq.Sum(p => p.nx.SoLuongN),
                                     SLXuat = kq.Sum(p => p.nx.SoLuongX),
                                     Ton = kq.Sum(p => p.nx.SoLuongN) - kq.Sum(p => p.nx.SoLuongX),
                                     Phanloai = kq.Key.PLoai,
                                     NCC = kq.Key.MaCC,
                                     GChu = kq.Key.MaKPBN == null ? (kq.Key.PLoai == 1 ? (kq.Key.KieuDon == 1 ?
                                     _lCC.Where(p => p.MaCC == kq.Key.MaCC).Select(p => p.TenCC).FirstOrDefault() + ("") :
                                     _lKP.Where(p => p.MaKP == kq.Key.MaKPnx).Select(p => p.TenKP).FirstOrDefault() + ("")) :
                                     (kq.Key.KieuDon == 0 ? (kq.Key.MaBNhan == null ? "" : (kq.Key.MaBNhan.ToString() + (""))) :
                                     _lKP.Where(p => p.MaKP == kq.Key.MaKPnx).Select(p => p.TenKP).FirstOrDefault())) : _lKP.Where(p => p.MaKP == kq.Key.MaKPBN).Select(p => p.TenKP).FirstOrDefault() + (""),

                                 }).ToList().Select(a => new
                                 {
                                     a.SCT,
                                     a.Ngaythang,
                                     Handung = DungChung.Bien.MaBV == "24012" ? a.HanDung24012 : (q2.Where(p => p.SoCT == a.SCTn || p.SoCT == a.SCTx).Select(p => p.HanDung).ToList().Count > 0 ? q2.Where(p => p.SoCT == a.SCTn || p.SoCT == a.SCTx).Select(p => p.HanDung).First() : null),
                                     Solo_HanDung = a.Solo + " - " + DungChung.Bien.MaBV == "24012" ? a.HanDung24012.ToString().Substring(0, 9) : ((q2.Where(p => p.SoCT == a.SCTn || p.SoCT == a.SCTx).Select(p => p.HanDung).ToList().Count > 0 ? q2.Where(p => p.SoCT == a.SCTn || p.SoCT == a.SCTx).Select(p => p.HanDung).FirstOrDefault().ToString().Substring(0, 9) : null)),
                                     a.SCTn,
                                     a.SCTx,
                                     a.Solo,
                                     a.MaKP,
                                     a.Soluongton,
                                     a.SLNhap,
                                     a.SLXuat,
                                     a.Ton,
                                     a.Phanloai,
                                     a.NCC,
                                     a.GChu,
                                     a.TenBNhan,
                                     a.MaBNhan,
                                 }).OrderBy(p => p.Ngaythang).ToList();
                        int _MaKP = 0;
                        if (lupKho.EditValue != null)
                            _MaKP = Convert.ToInt32(lupKho.EditValue);
                        var q1 = (from _nd in data.NhapDs.Where(p => p.NgayNhap < ngayden).Where(o => o.MaKP == _MaKP)
                                  join _ndct in data.NhapDcts.Where(p => p.MaDV == _Maduoc && (dongia == 0 || p.DonGia == dongia)) on _nd.IDNhap equals _ndct.IDNhap
                                  group new { _nd, _ndct } by new
                                  {
                                      _nd.NgayNhap,
                                      _ndct.SoLuongN,
                                      _ndct.SoLuongX
                                  } into kq
                                  select new
                                  {
                                      SLNhap = kq.Sum(p => p._ndct.SoLuongN),
                                      SLXuat = kq.Sum(p => p._ndct.SoLuongX)
                                  }).ToList();



                        if (q.Count > 0)
                        {
                            if (checkmaumoi.Checked == false)
                            {
                                BaoCao.rep_thekho rep = new BaoCao.rep_thekho(dongia);
                                rep.CQCQ.Value = DungChung.Bien.TenCQCQ.ToUpper();
                                rep.TenDV.Value = DungChung.Bien.TenCQ.ToUpper();
                                rep.TenHH.Value = lupTenDV.Text;
                                rep.ngaytu.Value = ngaytu;
                                rep.Madv.Value = _Maduoc;
                                rep.Ngaythang.Value = ngayden;
                                rep.Ngay.Value = "Từ ngày: " + ngaytu.ToString("dd/MM/yyyy") + " - Đến ngày: " + ngayden.ToString("dd/MM/yyyy");
                                rep.Khoaphong.Value = lupKho.EditValue.ToString();
                                double _tongnhap = 0;
                                double _tongxuat = 0;
                                if (q1.Count > 0)
                                {
                                    foreach (var item in q1)
                                    {
                                        _tongnhap += item.SLNhap;
                                        _tongxuat += item.SLXuat;
                                    }
                                    rep.LuyKeNhap.Value = _tongnhap;
                                    rep.LuyKeXuat.Value = _tongxuat;
                                }

                                rep.TonDau.Value = q.LastOrDefault().Soluongton;

                                var a = (from dv in data.DichVus.Where(p => p.MaDV == _Maduoc) select new { dv.HamLuong, dv.DonVi, dv.MaDV, dv.TenDV, dv.QCPC }).ToList();

                                if (a.First().HamLuong != null)
                                {
                                    rep.QPHHH.Value = a.First().HamLuong.ToString() + ", " + a.First().QCPC.ToString();
                                }
                                if (a.First().DonVi != null)
                                {
                                    rep.Donvi.Value = a.First().DonVi.ToString();
                                }
                                if (a.First().MaDV != 0)
                                {
                                    rep.Maso.Value = a.First().MaDV.ToString();
                                }
                                if (lupKho.Text == "")
                                {
                                    if (lupnhathau.Text == "")
                                    {
                                        rep.DataSource = q.ToList();
                                        rep.BindingData();
                                        rep.CreateDocument();
                                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                        frm.ShowDialog();
                                    }
                                    else
                                    {
                                        string Ncc = lupnhathau.EditValue.ToString();
                                        rep.Nhacc.Value = lupnhathau.Text;
                                        rep.DataSource = q.ToList().Where(p => p.NCC == Ncc);
                                        rep.BindingData();
                                        rep.CreateDocument();
                                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                        frm.ShowDialog();
                                    }
                                }
                                else
                                {
                                    if (lupnhathau.Text == "")
                                    {
                                        int _Kho = 0;
                                        _Kho = lupKho.EditValue == null ? 0 : Convert.ToInt32(lupKho.EditValue);
                                        rep.DataSource = (q.ToList().Where(p => p.MaKP == _Kho)).ToList();
                                        rep.BindingData();
                                        rep.CreateDocument();
                                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                        frm.ShowDialog();
                                    }
                                    else
                                    {
                                        int _Kho = 0;
                                        rep.Nhacc.Value = lupnhathau.Text;
                                        _Kho = lupKho.EditValue == null ? 0 : Convert.ToInt32(lupKho.EditValue);
                                        string Ncc = lupnhathau.EditValue.ToString();
                                        rep.DataSource = q.ToList().Where(p => p.MaKP == _Kho).Where(p => p.NCC == Ncc);
                                        rep.BindingData();
                                        rep.CreateDocument();
                                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                        frm.ShowDialog();
                                    }
                                }
                            }
                            else
                            {
                                BaoCao.rep_NXT_GAYNG_HTT rep = new BaoCao.rep_NXT_GAYNG_HTT();
                                rep.CQCQ.Value = DungChung.Bien.TenCQCQ.ToUpper();
                                rep.TenCQ.Value = DungChung.Bien.TenCQ.ToUpper();
                                rep.TenHH.Value = lupTenDV.Text;
                                rep.ngaytu.Value = ngaytu;
                                rep.Madv.Value = _Maduoc;
                                rep.Ngaythang.Value = ngayden;
                                rep.Khoaphong.Value = lupKho.EditValue.ToString();
                                string tenkttk = data.CanBoes.Where(p => p.MaCB == DungChung.Bien.MaCB).FirstOrDefault().TenCB;
                                rep.txtKTTK.Text = tenkttk;
                                var a = (from dv in data.DichVus.Where(p => p.MaDV == _Maduoc) select new { dv.HamLuong, dv.DonVi, dv.MaDV, dv.TenDV }).ToList();
                                if (a.First().HamLuong != null)
                                {
                                    rep.QPHHH.Value = a.First().HamLuong.ToString();
                                }
                                if (a.First().DonVi != null)
                                {
                                    rep.Donvi.Value = a.First().DonVi.ToString();
                                }
                                if (a.First().MaDV != null)
                                {
                                    rep.Maso.Value = a.First().MaDV.ToString();
                                }
                                if (lupKho.Text == "")
                                {
                                    if (lupnhathau.Text == "")
                                    {
                                        rep.DataSource = q.ToList();
                                        rep.BindingData();
                                        rep.CreateDocument();
                                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                        frm.ShowDialog();
                                    }
                                    else
                                    {
                                        string Ncc = lupnhathau.EditValue.ToString();
                                        rep.Nhacc.Value = lupnhathau.Text;
                                        rep.DataSource = q.ToList().Where(p => p.NCC == Ncc);
                                        rep.BindingData();
                                        rep.CreateDocument();
                                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                        frm.ShowDialog();
                                    }
                                }
                                else
                                {
                                    if (lupnhathau.Text == "")
                                    {
                                        int _Kho = 0;
                                        _Kho = lupKho.EditValue == null ? 0 : Convert.ToInt32(lupKho.EditValue);
                                        rep.DataSource = q.ToList().Where(p => p.MaKP == _Kho);
                                        rep.BindingData();
                                        rep.CreateDocument();
                                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                        frm.ShowDialog();
                                    }
                                    else
                                    {
                                        int _Kho = 0;
                                        rep.Nhacc.Value = lupnhathau.Text;
                                        _Kho = lupKho.EditValue == null ? 0 : Convert.ToInt32(lupKho.EditValue);
                                        string Ncc = lupnhathau.EditValue.ToString();
                                        rep.DataSource = q.ToList().Where(p => p.MaKP == _Kho).Where(p => p.NCC == Ncc);
                                        rep.BindingData();
                                        rep.CreateDocument();
                                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                        frm.ShowDialog();
                                    }
                                }
                            }
                        }
                        else
                        { MessageBox.Show("Không có dữ liệu"); }

                    }
                    else
                    {
                        #region Hiển thị theo tháng

                        frmIn frm = new frmIn();
                        var q = (from nx in qtong
                                 join dv in _ldv on nx.MaDV equals dv.MaDV
                                 //join Kh in data.KPhongs on nx.MaKP equals Kh.MaKP
                                 where (nx.NgayNhap <= ngayden && nx.NgayNhap >= ngaytu)
                                 group new { nx, dv } by new
                                 {
                                     nx.NgayNhap.Value.Month,
                                     dv.TenDV,
                                     HanDung = nx.HanDung,
                                     nx.SoLo,
                                     nx.PLoai,
                                     nx.MaKP,
                                     nx.MaCC,

                                 } into kq
                                 select new
                                 {
                                     Ngaythang = kq.Key.Month,
                                     Solo = kq.Key.SoLo,
                                     MaKP = kq.Key.MaKP,
                                     Soluongton = kq.Sum(p => p.nx.SoLuongN) - kq.Sum(p => p.nx.SoLuongX),
                                     SLNhap = kq.Sum(p => p.nx.SoLuongN),
                                     SLXuat = kq.Sum(p => p.nx.SoLuongX),
                                     Ton = kq.Sum(p => p.nx.SoLuongN) - kq.Sum(p => p.nx.SoLuongX),
                                     Phanloai = kq.Key.PLoai,
                                     NCC = kq.Key.MaCC,
                                     HanDung = kq.Key.HanDung,
                                     GChu = "",
                                 }).ToList().OrderBy(p => p.Ngaythang).ToList();

                        int _MaKP = 0;
                        if (lupKho.EditValue != null)
                            _MaKP = Convert.ToInt32(lupKho.EditValue);
                        var q10 = (from _nd in data.NhapDs.Where(p => p.NgayNhap < ngayden).Where(o => o.MaKP == _MaKP)
                                   join _ndct in data.NhapDcts.Where(p => p.MaDV == _Maduoc && (dongia == 0 || p.DonGia == dongia)) on _nd.IDNhap equals _ndct.IDNhap
                                   group new { _nd, _ndct } by new
                                   {
                                       _nd.NgayNhap,
                                       _ndct.SoLuongN,
                                       _ndct.SoLuongX
                                   } into kq
                                   select new
                                   {
                                       SLNhap = kq.Sum(p => p._ndct.SoLuongN),
                                       SLXuat = kq.Sum(p => p._ndct.SoLuongX)
                                   }).ToList();


                        if (DungChung.Bien.MaBV == "20001")
                        {
                            var q1 = (from nx in qtong
                                      join dv in _ldv on nx.MaDV equals dv.MaDV
                                      where (nx.NgayNhap <= ngayden && nx.NgayNhap >= ngaytu)
                                      group new { nx, dv } by new
                                      {
                                          nx.NgayNhap,
                                          dv.TenDV,
                                          nx.IDNhap,
                                          nx.SoLo,
                                          SoCT = nx.SoCT,
                                          nx.PLoai,
                                          nx.MaKP,
                                          nx.KieuDon,
                                          nx.MaBNhan,
                                          nx.MaCC,
                                          nx.MaKPnx,
                                          nx.GhiChu,
                                          nx.MaKPBN
                                      } into kq
                                      select new
                                      {
                                          Ngaythang = kq.Key.NgayNhap,
                                          SCTn = kq.Key.PLoai == 1 ? kq.Key.SoCT : "",
                                          SCTx = kq.Key.PLoai != 1 ? kq.Key.SoCT : "",
                                          MaKP = kq.Key.MaKP,
                                          Soluongton = kq.Sum(p => p.nx.SoLuongN) - kq.Sum(p => p.nx.SoLuongX),
                                          SLNhap = kq.Sum(p => p.nx.SoLuongN),
                                          SLXuat = kq.Sum(p => p.nx.SoLuongX),
                                          Ton = kq.Sum(p => p.nx.SoLuongN) - kq.Sum(p => p.nx.SoLuongX),
                                          Phanloai = kq.Key.PLoai,
                                          NCC = kq.Key.MaCC,
                                          GChu = kq.Key.MaKPBN == null ? (kq.Key.PLoai == 1 ? (kq.Key.KieuDon == 1 ?
                                          _lCC.Where(p => p.MaCC == kq.Key.MaCC).Select(p => p.TenCC).FirstOrDefault() :
                                          _lKP.Where(p => p.MaKP == kq.Key.MaKPnx).Select(p => p.TenKP).FirstOrDefault()) :
                                          (kq.Key.KieuDon == 0 ? (kq.Key.MaBNhan == null ? "" : kq.Key.MaBNhan.ToString()) :
                                          _lKP.Where(p => p.MaKP == kq.Key.MaKPnx).Select(p => p.TenKP).FirstOrDefault())) : _lKP.Where(p => p.MaKP == kq.Key.MaKPBN).Select(p => p.TenKP).FirstOrDefault(),
                                      }).ToList().Select(a => new { a.Ngaythang, Handung = q2.Where(p => p.SoCT == a.SCTn || p.SoCT == a.SCTx).Select(p => p.HanDung).ToList().Count > 0 ? q2.Where(p => p.SoCT == a.SCTn || p.SoCT == a.SCTx).Select(p => p.HanDung).First() : null, a.SCTn, a.SCTx, a.MaKP, a.Soluongton, a.SLNhap, a.SLXuat, a.Ton, a.Phanloai, a.NCC, a.GChu }).OrderBy(p => p.Ngaythang).ToList();
                            var q11 = (from a in q1
                                       group a
                                           by new
                                           {
                                               a.Ngaythang.Value.Month,
                                               Handung = a.Handung,
                                               a.Phanloai,
                                               a.MaKP,
                                               a.NCC,
                                               a.GChu
                                           } into kq
                                       select new
                                       {
                                           Ngaythang = kq.Key.Month,
                                           MaKP = kq.Key.MaKP,
                                           Soluongton = kq.Sum(p => p.Soluongton),
                                           SLNhap = kq.Sum(p => p.SLNhap),
                                           SLXuat = kq.Sum(p => p.SLXuat),
                                           Ton = kq.Sum(p => p.Ton),
                                           Phanloai = kq.Key.Phanloai,
                                           NCC = kq.Key.NCC,
                                           HanDung = kq.Key.Handung,
                                           GChu = kq.Key.GChu,
                                       }).ToList().OrderBy(p => p.Ngaythang).ToList();
                            if (q11.Count > 0)
                            {
                                BaoCao.rep_thekho rep = new BaoCao.rep_thekho(dongia);
                                rep.CQCQ.Value = DungChung.Bien.TenCQCQ.ToUpper();
                                rep.TenDV.Value = DungChung.Bien.TenCQ.ToUpper();
                                rep.TenHH.Value = lupTenDV.Text;
                                rep.ngaytu.Value = ngaytu;
                                rep.Madv.Value = _Maduoc;
                                rep.Ngaythang.Value = ngayden;
                                rep.Khoaphong.Value = lupKho.EditValue.ToString();
                                rep.Ngay.Value = "Từ ngày: " + ngaytu.ToString("dd/MM/yyyy") + " - Đến ngày: " + ngayden.ToString("dd/MM/yyyy");
                                var a = (from dv in data.DichVus.Where(p => p.MaDV == _Maduoc) select new { dv.HamLuong, dv.DonVi, dv.MaDV, dv.TenDV }).ToList();
                                if (a.First().HamLuong != null)
                                {
                                    rep.QPHHH.Value = a.First().HamLuong.ToString();
                                }
                                if (a.First().DonVi != null)
                                {
                                    rep.Donvi.Value = a.First().DonVi.ToString();
                                }
                                if (a.First().MaDV != null)
                                {
                                    rep.Maso.Value = a.First().MaDV.ToString();
                                }
                                if (lupKho.Text == "")
                                {
                                    if (lupnhathau.Text == "")
                                    {
                                        rep.DataSource = q11.ToList();
                                        rep.BindingData();
                                        rep.CreateDocument();
                                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                        frm.ShowDialog();
                                    }
                                    else
                                    {
                                        string Ncc = lupnhathau.EditValue.ToString();
                                        rep.Nhacc.Value = lupnhathau.Text;
                                        rep.DataSource = q11.ToList().Where(p => p.NCC == Ncc);
                                        rep.BindingData();
                                        rep.CreateDocument();
                                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                        frm.ShowDialog();
                                    }
                                }
                                else
                                {
                                    if (lupnhathau.Text == "")
                                    {
                                        int _Kho = 0;
                                        _Kho = lupKho.EditValue == null ? 0 : Convert.ToInt32(lupKho.EditValue);
                                        rep.DataSource = q11.ToList().Where(p => p.MaKP == _Kho);
                                        rep.BindingData();
                                        rep.CreateDocument();
                                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                        frm.ShowDialog();
                                    }
                                    else
                                    {
                                        int _Kho = 0;
                                        rep.Nhacc.Value = lupnhathau.Text;
                                        _Kho = lupKho.EditValue == null ? 0 : Convert.ToInt32(lupKho.EditValue);
                                        string Ncc = lupnhathau.EditValue.ToString();
                                        rep.DataSource = q11.ToList().Where(p => p.MaKP == _Kho).Where(p => p.NCC == Ncc);
                                        rep.BindingData();
                                        rep.CreateDocument();
                                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                        frm.ShowDialog();
                                    }
                                }
                            }
                            else
                            { MessageBox.Show("Không có dữ liệu"); }
                        }
                        else
                        {
                            if (q.Count > 0)
                            {
                                BaoCao.rep_thekho rep = new BaoCao.rep_thekho(dongia);
                                rep.CQCQ.Value = DungChung.Bien.TenCQCQ.ToUpper();
                                rep.TenDV.Value = DungChung.Bien.TenCQ.ToUpper();
                                rep.TenHH.Value = lupTenDV.Text;
                                rep.ngaytu.Value = ngaytu;
                                rep.Madv.Value = _Maduoc;
                                rep.Ngaythang.Value = ngayden;
                                rep.Khoaphong.Value = lupKho.EditValue.ToString();
                                rep.Ngay.Value = "Từ ngày: " + ngaytu.ToString("dd/MM/yyyy") + " - Đến ngày: " + ngayden.ToString("dd/MM/yyyy");
                                double _tongnhap = 0;
                                double _tongxuat = 0;
                                if (q10.Count > 0)
                                {
                                    foreach (var item in q10)
                                    {
                                        _tongnhap += item.SLNhap;
                                        _tongxuat += item.SLXuat;
                                    }
                                    rep.LuyKeNhap.Value = _tongnhap;
                                    rep.LuyKeXuat.Value = _tongxuat;
                                }
                                var a = (from dv in data.DichVus.Where(p => p.MaDV == _Maduoc) select new { dv.HamLuong, dv.DonVi, dv.MaDV, dv.TenDV }).ToList();
                                if (a.First().HamLuong != null)
                                {
                                    rep.QPHHH.Value = a.First().HamLuong.ToString();
                                }
                                if (a.First().DonVi != null)
                                {
                                    rep.Donvi.Value = a.First().DonVi.ToString();
                                }
                                if (a.First().MaDV != null)
                                {
                                    rep.Maso.Value = a.First().MaDV.ToString();
                                }
                                if (lupKho.Text == "")
                                {
                                    if (lupnhathau.Text == "")
                                    {
                                        rep.DataSource = q.ToList();
                                        rep.BindingData();
                                        rep.CreateDocument();
                                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                        frm.ShowDialog();
                                    }
                                    else
                                    {
                                        string Ncc = lupnhathau.EditValue.ToString();
                                        rep.Nhacc.Value = lupnhathau.Text;
                                        rep.DataSource = q.ToList().Where(p => p.NCC == Ncc);
                                        rep.BindingData();
                                        rep.CreateDocument();
                                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                        frm.ShowDialog();
                                    }
                                }
                                else
                                {
                                    if (lupnhathau.Text == "")
                                    {
                                        int _Kho = 0;
                                        _Kho = lupKho.EditValue == null ? 0 : Convert.ToInt32(lupKho.EditValue);
                                        rep.DataSource = q.ToList().Where(p => p.MaKP == _Kho);
                                        rep.BindingData();
                                        rep.CreateDocument();
                                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                        frm.ShowDialog();
                                    }
                                    else
                                    {
                                        int _Kho = 0;
                                        rep.Nhacc.Value = lupnhathau.Text;
                                        _Kho = lupKho.EditValue == null ? 0 : Convert.ToInt32(lupKho.EditValue);
                                        string Ncc = lupnhathau.EditValue.ToString();
                                        rep.DataSource = q.ToList().Where(p => p.MaKP == _Kho).Where(p => p.NCC == Ncc);
                                        rep.BindingData();
                                        rep.CreateDocument();
                                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                        frm.ShowDialog();
                                    }
                                }
                            }
                            else
                            { MessageBox.Show("Không có dữ liệu"); }
                        }

                        #endregion
                    }
                }
                #endregion
                #region BV 24012
                else if (DungChung.Bien.MaBV == "24012")
                {
                    if (radChon.SelectedIndex == 0)
                    {
                        frmIn frm = new frmIn();

                        var q = (from nx in qtong
                                 join dv in _ldv on nx.MaDV equals dv.MaDV
                                 //join Kh in data.KPhongs on nx.MaKP equals Kh.MaKP
                                 where (nx.NgayNhap <= ngayden && nx.NgayNhap >= ngaytu)
                                 group new { nx, dv } by new
                                 {
                                     nx.NgayNhap,
                                     dv.TenDV,
                                     nx.IDNhap,
                                     nx.SoLo,
                                     nx.HanDung,
                                     SoCT = nx.SoCT,
                                     nx.PLoai,
                                     nx.MaKP,
                                     nx.KieuDon,
                                     nx.MaCC,
                                     nx.MaKPnx,
                                     nx.GhiChu,
                                     nx.MaKPBN,
                                     nx.TenBNhan,
                                     nx.MaBNhan,
                                 } into kq
                                 select new
                                 {
                                     TenBNhan = kq.Key.TenBNhan,
                                     MaBNhan = kq.Key.MaBNhan,
                                     Ngaythang = kq.Key.NgayNhap,
                                     HanDung24012 = kq.Key.HanDung,
                                     SCTn = kq.Key.PLoai == 1 ? kq.Key.SoCT : "",
                                     SCTx = kq.Key.PLoai != 1 ? kq.Key.SoCT : "",
                                     SCT = kq.Key.SoCT,
                                     Solo = kq.Key.SoLo,
                                     MaKP = kq.Key.MaKP,
                                     Soluongton = kq.Sum(p => p.nx.SoLuongN) - kq.Sum(p => p.nx.SoLuongX),
                                     SLNhap = kq.Sum(p => p.nx.SoLuongN),
                                     SLXuat = kq.Sum(p => p.nx.SoLuongX),
                                     Ton = kq.Sum(p => p.nx.SoLuongN) - kq.Sum(p => p.nx.SoLuongX),
                                     Phanloai = kq.Key.PLoai,
                                     NCC = kq.Key.MaCC,
                                     GChu = kq.Key.MaKPBN == null ? (kq.Key.PLoai == 1 ? (kq.Key.KieuDon == 1 ?
                                     "Nhập từ " + _lCC.Where(p => p.MaCC == kq.Key.MaCC).Select(p => p.TenCC).FirstOrDefault() :
                                     "Nhập thuốc vào " + _lKP.Where(p => p.MaKP == kq.Key.MaKPnx).Select(p => p.TenKP).FirstOrDefault()) :
                                     (kq.Key.KieuDon == 0 ? (kq.Key.TenBNhan == null ? "" : "Xuất cho mã bệnh nhân: " + (kq.Key.MaBNhan)) :
                                     "Xuất thuốc cho " + _lKP.Where(p => p.MaKP == kq.Key.MaKPnx).Select(p => p.TenKP).FirstOrDefault())) : "Xuất thuốc cho " + _lKP.Where(p => p.MaKP == kq.Key.MaKPBN).Select(p => p.TenKP).FirstOrDefault(),
                                 }).ToList().Select(a => new
                                 {
                                     a.SCT,
                                     a.Ngaythang,
                                     Handung = DungChung.Bien.MaBV == "24012" ? a.HanDung24012 : (q2.Where(p => p.SoCT == a.SCTn || p.SoCT == a.SCTx).Select(p => p.HanDung).ToList().Count > 0 ? q2.Where(p => p.SoCT == a.SCTn || p.SoCT == a.SCTx).Select(p => p.HanDung).First() : null),
                                     Solo_HanDung = a.Solo + " - " + DungChung.Bien.MaBV == "24012" ? a.HanDung24012.ToString().Substring(0, 9) : ((q2.Where(p => p.SoCT == a.SCTn || p.SoCT == a.SCTx).Select(p => p.HanDung).ToList().Count > 0 ? q2.Where(p => p.SoCT == a.SCTn || p.SoCT == a.SCTx).Select(p => p.HanDung).FirstOrDefault().ToString().Substring(0, 9) : null)),
                                     a.SCTn,
                                     a.SCTx,
                                     a.Solo,
                                     a.MaKP,
                                     a.Soluongton,
                                     a.SLNhap,
                                     a.SLXuat,
                                     a.Ton,
                                     a.Phanloai,
                                     a.NCC,
                                     a.GChu,
                                     a.TenBNhan,
                                     a.MaBNhan,
                                 }).OrderBy(p => p.Ngaythang).ToList();
                        int _MaKP = 0;
                        if (lupKho.EditValue != null)
                            _MaKP = Convert.ToInt32(lupKho.EditValue);
                        var q1 = (from _nd in data.NhapDs.Where(p => p.NgayNhap < ngayden).Where(o => o.MaKP == _MaKP)
                                  join _ndct in data.NhapDcts.Where(p => p.MaDV == _Maduoc && (dongia == 0 || p.DonGia == dongia)) on _nd.IDNhap equals _ndct.IDNhap
                                  group new { _nd, _ndct } by new
                                  {
                                      _nd.NgayNhap,
                                      _ndct.SoLuongN,
                                      _ndct.SoLuongX
                                  } into kq
                                  select new
                                  {
                                      SLNhap = kq.Sum(p => p._ndct.SoLuongN),
                                      SLXuat = kq.Sum(p => p._ndct.SoLuongX)
                                  }).ToList();



                        if (checkmaumoi.Checked == false)
                        {
                            BaoCao.rep_thekho rep = new BaoCao.rep_thekho(dongia);
                            if (ckngoaitru.Checked)
                            {
                                rep.xrTable2.Visible = false;
                                rep.sub_24012.Visible = true;
                                rep.SubBand3.Visible = false;
                                rep.xrTable6.Visible = true;
                                rep.SubBand4.Visible = true;
                                rep.SubBand5.Visible = false;
                            }
                            rep.colTenKho24012.Visible = true;
                            rep.col_TenKho_24012.Visible = true;
                            int makp = Convert.ToInt32(lupKho.EditValue);
                            var kp = data.KPhongs.Where(p => p.MaKP == makp).Select(p => p.TenKP).FirstOrDefault();
                            rep.Parameters["TenKho"].Value = kp;
                            rep.CQCQ.Value = DungChung.Bien.TenCQCQ.ToUpper();
                            rep.TenDV.Value = DungChung.Bien.TenCQ.ToUpper();
                            rep.TenHH.Value = lupTenDV.Text;
                            rep.ngaytu.Value = ngaytu;
                            rep.Madv.Value = _Maduoc;
                            rep.Ngaythang.Value = ngayden;
                            rep.Ngay.Value = "Từ ngày: " + ngaytu.ToString("dd/MM/yyyy") + " - Đến ngày: " + ngayden.ToString("dd/MM/yyyy");
                            rep.Khoaphong.Value = lupKho.EditValue.ToString();
                            double _tongnhap = 0;
                            double _tongxuat = 0;
                            if (q1.Count > 0)
                            {
                                foreach (var item in q1)
                                {
                                    _tongnhap += item.SLNhap;
                                    _tongxuat += item.SLXuat;
                                }
                                rep.LuyKeNhap.Value = _tongnhap;
                                rep.LuyKeXuat.Value = _tongxuat;
                            }
                            if (q.Count() > 0)
                            {
                                rep.TonDau.Value = q.LastOrDefault().Soluongton;
                            }
                            else
                            {
                                rep.TonDau.Value = "0";
                            }    
                            var a = (from dv in data.DichVus.Where(p => p.MaDV == _Maduoc) select new { dv.HamLuong, dv.DonVi, dv.MaDV, dv.TenDV, dv.QCPC }).ToList();

                            if (a.First().HamLuong != null)
                            {
                                rep.QPHHH.Value = a.First().HamLuong.ToString() + ", " + a.First().QCPC.ToString();
                            }
                            if (a.First().DonVi != null)
                            {
                                rep.Donvi.Value = a.First().DonVi.ToString();
                            }
                            if (a.First().MaDV != 0)
                            {
                                rep.Maso.Value = a.First().MaDV.ToString();
                            }
                            if (lupKho.Text == "")
                            {
                                if (lupnhathau.Text == "")
                                {
                                    rep.DataSource = q.ToList();
                                    rep.BindingData();
                                    rep.CreateDocument();
                                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                    frm.ShowDialog();
                                }
                                else
                                {
                                    string Ncc = lupnhathau.EditValue.ToString();
                                    rep.Nhacc.Value = lupnhathau.Text;
                                    rep.DataSource = q.ToList().Where(p => p.NCC == Ncc);
                                    rep.BindingData();
                                    rep.CreateDocument();
                                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                    frm.ShowDialog();
                                }
                            }
                            else
                            {
                                if (lupnhathau.Text == "")
                                {
                                    int _Kho = 0;
                                    _Kho = lupKho.EditValue == null ? 0 : Convert.ToInt32(lupKho.EditValue);
                                    rep.DataSource = (q.ToList().Where(p => p.MaKP == _Kho)).ToList();
                                    rep.BindingData();
                                    rep.CreateDocument();
                                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                    frm.ShowDialog();
                                }
                                else
                                {
                                    int _Kho = 0;
                                    rep.Nhacc.Value = lupnhathau.Text;
                                    _Kho = lupKho.EditValue == null ? 0 : Convert.ToInt32(lupKho.EditValue);
                                    string Ncc = lupnhathau.EditValue.ToString();
                                    rep.DataSource = q.ToList().Where(p => p.MaKP == _Kho).Where(p => p.NCC == Ncc);
                                    rep.BindingData();
                                    rep.CreateDocument();
                                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                    frm.ShowDialog();
                                }
                            }
                        }
                        else
                        {
                            BaoCao.rep_NXT_GAYNG_HTT rep = new BaoCao.rep_NXT_GAYNG_HTT();
                            rep.CQCQ.Value = DungChung.Bien.TenCQCQ.ToUpper();
                            rep.TenCQ.Value = DungChung.Bien.TenCQ.ToUpper();
                            rep.TenHH.Value = lupTenDV.Text;
                            rep.ngaytu.Value = ngaytu;
                            rep.Madv.Value = _Maduoc;
                            rep.Ngaythang.Value = ngayden;
                            rep.Khoaphong.Value = lupKho.EditValue.ToString();
                            string tenkttk = data.CanBoes.Where(p => p.MaCB == DungChung.Bien.MaCB).FirstOrDefault().TenCB;
                            rep.txtKTTK.Text = tenkttk;
                            var a = (from dv in data.DichVus.Where(p => p.MaDV == _Maduoc) select new { dv.HamLuong, dv.DonVi, dv.MaDV, dv.TenDV }).ToList();
                            if (a.First().HamLuong != null)
                            {
                                rep.QPHHH.Value = a.First().HamLuong.ToString();
                            }
                            if (a.First().DonVi != null)
                            {
                                rep.Donvi.Value = a.First().DonVi.ToString();
                            }
                            if (a.First().MaDV != null)
                            {
                                rep.Maso.Value = a.First().MaDV.ToString();
                            }
                            if (lupKho.Text == "")
                            {
                                if (lupnhathau.Text == "")
                                {
                                    rep.DataSource = q.ToList();
                                    rep.BindingData();
                                    rep.CreateDocument();
                                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                    frm.ShowDialog();
                                }
                                else
                                {
                                    string Ncc = lupnhathau.EditValue.ToString();
                                    rep.Nhacc.Value = lupnhathau.Text;
                                    rep.DataSource = q.ToList().Where(p => p.NCC == Ncc);
                                    rep.BindingData();
                                    rep.CreateDocument();
                                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                    frm.ShowDialog();
                                }
                            }
                            else
                            {
                                if (lupnhathau.Text == "")
                                {
                                    int _Kho = 0;
                                    _Kho = lupKho.EditValue == null ? 0 : Convert.ToInt32(lupKho.EditValue);
                                    rep.DataSource = q.ToList().Where(p => p.MaKP == _Kho);
                                    rep.BindingData();
                                    rep.CreateDocument();
                                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                    frm.ShowDialog();
                                }
                                else
                                {
                                    int _Kho = 0;
                                    rep.Nhacc.Value = lupnhathau.Text;
                                    _Kho = lupKho.EditValue == null ? 0 : Convert.ToInt32(lupKho.EditValue);
                                    string Ncc = lupnhathau.EditValue.ToString();
                                    rep.DataSource = q.ToList().Where(p => p.MaKP == _Kho).Where(p => p.NCC == Ncc);
                                    rep.BindingData();
                                    rep.CreateDocument();
                                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                    frm.ShowDialog();
                                }
                            }
                        }
                    }
                    else
                    {
                        #region Hiển thị theo tháng

                        frmIn frm = new frmIn();
                        var q = (from nx in qtong
                                 join dv in _ldv on nx.MaDV equals dv.MaDV
                                 //join Kh in data.KPhongs on nx.MaKP equals Kh.MaKP
                                 where (nx.NgayNhap <= ngayden && nx.NgayNhap >= ngaytu)
                                 group new { nx, dv } by new
                                 {
                                     nx.NgayNhap.Value.Month,
                                     dv.TenDV,
                                     HanDung = nx.HanDung,
                                     nx.SoLo,
                                     nx.PLoai,
                                     nx.MaKP,
                                     nx.MaCC,

                                 } into kq
                                 select new
                                 {
                                     Ngaythang = kq.Key.Month,
                                     Solo = kq.Key.SoLo,
                                     MaKP = kq.Key.MaKP,
                                     Soluongton = kq.Sum(p => p.nx.SoLuongN) - kq.Sum(p => p.nx.SoLuongX),
                                     SLNhap = kq.Sum(p => p.nx.SoLuongN),
                                     SLXuat = kq.Sum(p => p.nx.SoLuongX),
                                     Ton = kq.Sum(p => p.nx.SoLuongN) - kq.Sum(p => p.nx.SoLuongX),
                                     Phanloai = kq.Key.PLoai,
                                     NCC = kq.Key.MaCC,
                                     HanDung = kq.Key.HanDung,
                                     GChu = "",
                                 }).ToList().OrderBy(p => p.Ngaythang).ToList();

                        int _MaKP = 0;
                        if (lupKho.EditValue != null)
                            _MaKP = Convert.ToInt32(lupKho.EditValue);
                        var q10 = (from _nd in data.NhapDs.Where(p => p.NgayNhap < ngayden).Where(o => o.MaKP == _MaKP)
                                   join _ndct in data.NhapDcts.Where(p => p.MaDV == _Maduoc && (dongia == 0 || p.DonGia == dongia)) on _nd.IDNhap equals _ndct.IDNhap
                                   group new { _nd, _ndct } by new
                                   {
                                       _nd.NgayNhap,
                                       _ndct.SoLuongN,
                                       _ndct.SoLuongX
                                   } into kq
                                   select new
                                   {
                                       SLNhap = kq.Sum(p => p._ndct.SoLuongN),
                                       SLXuat = kq.Sum(p => p._ndct.SoLuongX)
                                   }).ToList();
                                BaoCao.rep_thekho rep = new BaoCao.rep_thekho(dongia);
                                rep.CQCQ.Value = DungChung.Bien.TenCQCQ.ToUpper();
                                rep.TenDV.Value = DungChung.Bien.TenCQ.ToUpper();
                                rep.TenHH.Value = lupTenDV.Text;
                                rep.ngaytu.Value = ngaytu;
                                rep.Madv.Value = _Maduoc;
                                rep.Ngaythang.Value = ngayden;
                                rep.Khoaphong.Value = lupKho.EditValue.ToString();
                                rep.Ngay.Value = "Từ ngày: " + ngaytu.ToString("dd/MM/yyyy") + " - Đến ngày: " + ngayden.ToString("dd/MM/yyyy");
                                double _tongnhap = 0;
                                double _tongxuat = 0;
                                if (q10.Count > 0)
                                {
                                    foreach (var item in q10)
                                    {
                                        _tongnhap += item.SLNhap;
                                        _tongxuat += item.SLXuat;
                                    }
                                    rep.LuyKeNhap.Value = _tongnhap;
                                    rep.LuyKeXuat.Value = _tongxuat;
                                }
                                var a = (from dv in data.DichVus.Where(p => p.MaDV == _Maduoc) select new { dv.HamLuong, dv.DonVi, dv.MaDV, dv.TenDV }).ToList();
                                if (a.First().HamLuong != null)
                                {
                                    rep.QPHHH.Value = a.First().HamLuong.ToString();
                                }
                                if (a.First().DonVi != null)
                                {
                                    rep.Donvi.Value = a.First().DonVi.ToString();
                                }
                                if (a.First().MaDV != null)
                                {
                                    rep.Maso.Value = a.First().MaDV.ToString();
                                }
                                if (lupKho.Text == "")
                                {
                                    if (lupnhathau.Text == "")
                                    {
                                        rep.DataSource = q.ToList();
                                        rep.BindingData();
                                        rep.CreateDocument();
                                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                        frm.ShowDialog();
                                    }
                                    else
                                    {
                                        string Ncc = lupnhathau.EditValue.ToString();
                                        rep.Nhacc.Value = lupnhathau.Text;
                                        rep.DataSource = q.ToList().Where(p => p.NCC == Ncc);
                                        rep.BindingData();
                                        rep.CreateDocument();
                                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                        frm.ShowDialog();
                                    }
                                }
                                else
                                {
                                    if (lupnhathau.Text == "")
                                    {
                                        int _Kho = 0;
                                        _Kho = lupKho.EditValue == null ? 0 : Convert.ToInt32(lupKho.EditValue);
                                        rep.DataSource = q.ToList().Where(p => p.MaKP == _Kho);
                                        rep.BindingData();
                                        rep.CreateDocument();
                                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                        frm.ShowDialog();
                                    }
                                    else
                                    {
                                        int _Kho = 0;
                                        rep.Nhacc.Value = lupnhathau.Text;
                                        _Kho = lupKho.EditValue == null ? 0 : Convert.ToInt32(lupKho.EditValue);
                                        string Ncc = lupnhathau.EditValue.ToString();
                                        rep.DataSource = q.ToList().Where(p => p.MaKP == _Kho).Where(p => p.NCC == Ncc);
                                        rep.BindingData();
                                        rep.CreateDocument();
                                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                        frm.ShowDialog();
                                    }
                                }
                        #endregion
                    }
                }
                #endregion
                #region BV 27022
                else
                {
                    #region Bệnh viên 27022
                    if (checkEdit1.Checked == false)
                    {
                        if (radChon.SelectedIndex == 0)
                        {
                            frmIn frm = new frmIn();

                            var q = (from nx in qtong
                                     join dv in _ldv on nx.MaDV equals dv.MaDV
                                     //join Kh in data.KPhongs on nx.MaKP equals Kh.MaKP
                                     where (nx.NgayNhap <= ngayden && nx.NgayNhap >= ngaytu)
                                     group new { nx, dv } by new
                                     {
                                         nx.NgayNhap,
                                         dv.TenDV,
                                         nx.IDNhap,
                                         nx.SoLo,
                                         SoCT = nx.SoCT,
                                         nx.PLoai,
                                         nx.MaKP,
                                         nx.KieuDon,
                                         nx.MaBNhan,
                                         nx.MaCC,
                                         nx.MaKPnx,
                                         nx.GhiChu,
                                         nx.MaKPBN,
                                         nx.TenBNhan
                                     } into kq
                                     select new
                                     {
                                         Ngaythang = kq.Key.NgayNhap,
                                         //ToString().Substring(0,10),
                                         SCTn = kq.Key.PLoai == 1 ? kq.Key.SoCT : "",
                                         SCTx = kq.Key.PLoai != 1 ? kq.Key.SoCT : "",
                                         SCT = kq.Key.SoCT,
                                         Solo = kq.Key.SoLo,
                                         MaKP = kq.Key.MaKP,
                                         Soluongton = kq.Sum(p => p.nx.SoLuongN) - kq.Sum(p => p.nx.SoLuongX),
                                         SLNhap = kq.Sum(p => p.nx.SoLuongN),
                                         SLXuat = kq.Sum(p => p.nx.SoLuongX),
                                         Ton = kq.Sum(p => p.nx.SoLuongN) - kq.Sum(p => p.nx.SoLuongX),
                                         Phanloai = kq.Key.PLoai,
                                         NCC = kq.Key.MaCC,
                                         GChu = kq.Key.MaKPBN == null ? (kq.Key.PLoai == 1 ? (kq.Key.KieuDon == 1 ?
                                         _lCC.Where(p => p.MaCC == kq.Key.MaCC).Select(p => p.TenCC).FirstOrDefault() + (DungChung.Bien.MaBV == "27022" && kq.Key.MaBNhan != null ? (" - " + kq.Key.MaBNhan.ToString() + " - " + kq.Key.TenBNhan) : "") :
                                         _lKP.Where(p => p.MaKP == kq.Key.MaKPnx).Select(p => p.TenKP).FirstOrDefault() + (DungChung.Bien.MaBV == "27022" && kq.Key.MaBNhan != null ? (" - " + kq.Key.MaBNhan.ToString() + " - " + kq.Key.TenBNhan) : "")) :
                                         (kq.Key.KieuDon == 0 ? (kq.Key.MaBNhan == null ? "" : (kq.Key.MaBNhan.ToString() + (DungChung.Bien.MaBV == "27022" ? " - " + kq.Key.TenBNhan : ""))) :
                                         _lKP.Where(p => p.MaKP == kq.Key.MaKPnx).Select(p => p.TenKP).FirstOrDefault())) : _lKP.Where(p => p.MaKP == kq.Key.MaKPBN).Select(p => p.TenKP).FirstOrDefault() + (DungChung.Bien.MaBV == "27022" && kq.Key.MaBNhan != null ? (" - " + kq.Key.MaBNhan.ToString() + " - " + kq.Key.TenBNhan) : ""),
                                         //   Kho=kq.Key.

                                     }).ToList().Select(a => new
                                     {
                                         a.SCT,
                                         a.Ngaythang,
                                         Handung = q2.Where(p => p.SoCT == a.SCTn || p.SoCT == a.SCTx).Select(p => p.HanDung).ToList().Count > 0 ? q2.Where(p => p.SoCT == a.SCTn || p.SoCT == a.SCTx).Select(p => p.HanDung).First() : null,
                                         Solo_HanDung = a.Solo + " - " + (q2.Where(p => p.SoCT == a.SCTn || p.SoCT == a.SCTx).Select(p => p.HanDung).ToList().Count > 0 ? q2.Where(p => p.SoCT == a.SCTn || p.SoCT == a.SCTx).Select(p => p.HanDung).FirstOrDefault().ToString().Substring(0, 9) : null),
                                         a.SCTn,
                                         a.SCTx,
                                         a.Solo,
                                         a.MaKP,
                                         a.Soluongton,
                                         a.SLNhap,
                                         a.SLXuat,
                                         a.Ton,
                                         a.Phanloai,
                                         a.NCC,
                                         a.GChu
                                     }).OrderBy(p => p.Ngaythang).ToList();

                            /**
                             SELECT         SUM(NhapDct.SoLuongN) AS [Lũy kế xuất], SUM(NhapDct.SoLuongX) AS [Lũy kế nhập]
        FROM            NhapD INNER JOIN
                                 NhapDct ON NhapD.IDNhap = NhapDct.IDNhap
        WHERE        (NhapD.NgayNhap < CONVERT(DATETIME, '2020-03-11 00:00:00', 102)) AND (NhapD.MaKP = 6) AND (NhapDct.MaDV = 4231)
                             */
                            int _MaKP = 0;
                            if (lupKho.EditValue != null)
                                _MaKP = Convert.ToInt32(lupKho.EditValue);
                            var q1 = (from _nd in data.NhapDs.Where(p => p.NgayNhap < ngayden).Where(o => o.MaKP == _MaKP)
                                      join _ndct in data.NhapDcts.Where(p => p.MaDV == _Maduoc && (dongia == 0 || p.DonGia == dongia)) on _nd.IDNhap equals _ndct.IDNhap
                                      group new { _nd, _ndct } by new
                                      {
                                          _nd.NgayNhap,
                                          _ndct.SoLuongN,
                                          _ndct.SoLuongX
                                      } into kq
                                      select new
                                      {
                                          SLNhap = kq.Sum(p => p._ndct.SoLuongN),
                                          SLXuat = kq.Sum(p => p._ndct.SoLuongX)
                                      }).ToList();



                            if (q.Count > 0)
                            {
                                if (checkmaumoi.Checked == false)
                                {
                                    BaoCao.rep_thekho rep = new BaoCao.rep_thekho(dongia);
                                    rep.LB_HoatChat_27022.Visible = true;
                                    rep.txt_HoatChat_27022.Visible = true;
                                    rep.Ngay.Value = "Từ ngày: " + ngaytu.ToString("dd/MM/yyyy") + " - Đến ngày: " + ngayden.ToString("dd/MM/yyyy");
                                    rep.CQCQ.Value = DungChung.Bien.TenCQCQ.ToUpper();
                                    rep.TenDV.Value = DungChung.Bien.TenCQ.ToUpper();
                                    //rep.TenDV.Value = lupTenDV.Text;
                                    rep.TenHH.Value = lupTenDV.Text;
                                    rep.ngaytu.Value = ngaytu;
                                    rep.Madv.Value = _Maduoc;
                                    rep.Ngaythang.Value = ngayden;
                                    rep.Khoaphong.Value = lupKho.EditValue.ToString();
                                    //string tenkttk = data.CanBoes.Where(p => p.MaCB == DungChung.Bien.MaCB).FirstOrDefault().TenCB;
                                    //rep.txtKTTK.Text = tenkttk;
                                    double _tongnhap = 0;
                                    double _tongxuat = 0;
                                    if (q1.Count > 0)
                                    {
                                        foreach (var item in q1)
                                        {
                                            _tongnhap += item.SLNhap;
                                            _tongxuat += item.SLXuat;
                                        }
                                        rep.LuyKeNhap.Value = _tongnhap;
                                        rep.LuyKeXuat.Value = _tongxuat;
                                    }

                                    var a = (from dv in data.DichVus.Where(p => p.MaDV == _Maduoc) select new { dv.TenHC, dv.HamLuong, dv.DonVi, dv.MaDV, dv.TenDV }).ToList();

                                    if (a.First().HamLuong != null)
                                    {
                                        rep.QPHHH.Value = a.First().HamLuong.ToString();
                                    }
                                    if (a.First().TenHC != null)
                                    {
                                        rep.Parameters["HoatChat"].Value = a.First().TenHC.ToString();
                                    }
                                    if (a.First().DonVi != null)
                                    {
                                        rep.Donvi.Value = a.First().DonVi.ToString();
                                    }
                                    if (a.First().MaDV != null)
                                    {
                                        rep.Maso.Value = a.First().MaDV.ToString();
                                    }
                                    if (lupKho.Text == "")
                                    {
                                        if (lupnhathau.Text == "")
                                        {
                                            rep.DataSource = q.ToList();
                                            rep.BindingData();
                                            rep.CreateDocument();
                                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                            frm.ShowDialog();
                                        }
                                        else
                                        {
                                            string Ncc = lupnhathau.EditValue.ToString();
                                            rep.Nhacc.Value = lupnhathau.Text;
                                            rep.DataSource = q.ToList().Where(p => p.NCC == Ncc);
                                            rep.BindingData();
                                            rep.CreateDocument();
                                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                            frm.ShowDialog();
                                        }
                                    }
                                    else
                                    {
                                        if (lupnhathau.Text == "")
                                        {
                                            int _Kho = 0;
                                            _Kho = lupKho.EditValue == null ? 0 : Convert.ToInt32(lupKho.EditValue);
                                            rep.DataSource = (q.ToList().Where(p => p.MaKP == _Kho)).ToList();
                                            rep.BindingData();
                                            rep.CreateDocument();
                                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                            frm.ShowDialog();
                                        }
                                        else
                                        {
                                            int _Kho = 0;
                                            rep.Nhacc.Value = lupnhathau.Text;
                                            _Kho = lupKho.EditValue == null ? 0 : Convert.ToInt32(lupKho.EditValue);
                                            string Ncc = lupnhathau.EditValue.ToString();
                                            rep.DataSource = q.ToList().Where(p => p.MaKP == _Kho).Where(p => p.NCC == Ncc);
                                            rep.BindingData();
                                            rep.CreateDocument();
                                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                            frm.ShowDialog();
                                        }
                                    }
                                }
                                else
                                {
                                    BaoCao.rep_NXT_GAYNG_HTT rep = new BaoCao.rep_NXT_GAYNG_HTT();
                                    rep.CQCQ.Value = DungChung.Bien.TenCQCQ.ToUpper();
                                    rep.TenCQ.Value = DungChung.Bien.TenCQ.ToUpper();
                                    //rep.TenDV.Value = lupTenDV.Text;
                                    rep.TenHH.Value = lupTenDV.Text;
                                    rep.ngaytu.Value = ngaytu;
                                    rep.Madv.Value = _Maduoc;
                                    rep.Ngaythang.Value = ngayden;
                                    rep.Khoaphong.Value = lupKho.EditValue.ToString();
                                    string tenkttk = data.CanBoes.Where(p => p.MaCB == DungChung.Bien.MaCB).FirstOrDefault().TenCB;
                                    rep.txtKTTK.Text = tenkttk;
                                    var a = (from dv in data.DichVus.Where(p => p.MaDV == _Maduoc) select new { dv.HamLuong, dv.DonVi, dv.MaDV, dv.TenDV }).ToList();
                                    if (a.First().HamLuong != null)
                                    {
                                        rep.QPHHH.Value = a.First().HamLuong.ToString();
                                    }
                                    if (a.First().DonVi != null)
                                    {
                                        rep.Donvi.Value = a.First().DonVi.ToString();
                                    }
                                    if (a.First().MaDV != null)
                                    {
                                        rep.Maso.Value = a.First().MaDV.ToString();
                                    }
                                    if (lupKho.Text == "")
                                    {
                                        if (lupnhathau.Text == "")
                                        {
                                            rep.DataSource = q.ToList();
                                            rep.BindingData();
                                            rep.CreateDocument();
                                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                            frm.ShowDialog();
                                        }
                                        else
                                        {
                                            string Ncc = lupnhathau.EditValue.ToString();
                                            rep.Nhacc.Value = lupnhathau.Text;
                                            rep.DataSource = q.ToList().Where(p => p.NCC == Ncc);
                                            rep.BindingData();
                                            rep.CreateDocument();
                                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                            frm.ShowDialog();
                                        }
                                    }
                                    else
                                    {
                                        if (lupnhathau.Text == "")
                                        {
                                            int _Kho = 0;
                                            _Kho = lupKho.EditValue == null ? 0 : Convert.ToInt32(lupKho.EditValue);
                                            rep.DataSource = q.ToList().Where(p => p.MaKP == _Kho);
                                            rep.BindingData();
                                            rep.CreateDocument();
                                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                            frm.ShowDialog();
                                        }
                                        else
                                        {
                                            int _Kho = 0;
                                            rep.Nhacc.Value = lupnhathau.Text;
                                            _Kho = lupKho.EditValue == null ? 0 : Convert.ToInt32(lupKho.EditValue);
                                            string Ncc = lupnhathau.EditValue.ToString();
                                            rep.DataSource = q.ToList().Where(p => p.MaKP == _Kho).Where(p => p.NCC == Ncc);
                                            rep.BindingData();
                                            rep.CreateDocument();
                                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                            frm.ShowDialog();
                                        }
                                    }
                                }
                            }
                            else
                            { MessageBox.Show("Không có dữ liệu"); }

                        }
                        else
                        {
                            #region Hiển thị theo tháng

                            frmIn frm = new frmIn();
                            var q = (from nx in qtong
                                     join dv in _ldv on nx.MaDV equals dv.MaDV
                                     //join Kh in data.KPhongs on nx.MaKP equals Kh.MaKP
                                     where (nx.NgayNhap <= ngayden && nx.NgayNhap >= ngaytu)
                                     group new { nx, dv } by new
                                     {
                                         nx.NgayNhap.Value.Month,
                                         dv.TenDV,
                                         HanDung = nx.HanDung ?? Convert.ToDateTime("01/01/2010"),
                                         //nx.IDNhap,
                                         //nxct.SoLo,
                                         //nx.SoCT,
                                         nx.PLoai,
                                         nx.MaKP,
                                         //nx.GhiChu,
                                         // Kh.TenKP,
                                         nx.MaCC,

                                     } into kq
                                     select new
                                     {
                                         Ngaythang = kq.Key.Month,
                                         //ToString().Substring(0,10),
                                         //.Date.ToString().Substring(0,10),
                                         //SCT = kq.Key.SoCT,
                                         //Solo = kq.Key.SoLo,
                                         MaKP = kq.Key.MaKP,
                                         Soluongton = kq.Sum(p => p.nx.SoLuongN) - kq.Sum(p => p.nx.SoLuongX),
                                         SLNhap = kq.Sum(p => p.nx.SoLuongN),
                                         SLXuat = kq.Sum(p => p.nx.SoLuongX),
                                         Ton = kq.Sum(p => p.nx.SoLuongN) - kq.Sum(p => p.nx.SoLuongX),
                                         Phanloai = kq.Key.PLoai,
                                         NCC = kq.Key.MaCC,
                                         HanDung = kq.Key.HanDung,
                                         GChu = ""
                                         //GChu = kq.Key.GhiChu,
                                         //   Kho=kq.Key.

                                     }).ToList().OrderBy(p => p.Ngaythang).ToList();

                            int _MaKP = 0;
                            if (lupKho.EditValue != null)
                                _MaKP = Convert.ToInt32(lupKho.EditValue);
                            var q10 = (from _nd in data.NhapDs.Where(p => p.NgayNhap < ngayden).Where(o => o.MaKP == _MaKP)
                                       join _ndct in data.NhapDcts.Where(p => p.MaDV == _Maduoc && (dongia == 0 || p.DonGia == dongia)) on _nd.IDNhap equals _ndct.IDNhap
                                       group new { _nd, _ndct } by new
                                       {
                                           _nd.NgayNhap,
                                           _ndct.SoLuongN,
                                           _ndct.SoLuongX
                                       } into kq
                                       select new
                                       {
                                           SLNhap = kq.Sum(p => p._ndct.SoLuongN),
                                           SLXuat = kq.Sum(p => p._ndct.SoLuongX)
                                       }).ToList();


                            if (DungChung.Bien.MaBV == "20001")
                            {
                                var q1 = (from nx in qtong
                                          join dv in _ldv on nx.MaDV equals dv.MaDV
                                          //join Kh in data.KPhongs on nx.MaKP equals Kh.MaKP
                                          where (nx.NgayNhap <= ngayden && nx.NgayNhap >= ngaytu)
                                          group new { nx, dv } by new
                                          {
                                              nx.NgayNhap,
                                              dv.TenDV,
                                              nx.IDNhap,
                                              nx.SoLo,
                                              SoCT = nx.SoCT,
                                              nx.PLoai,
                                              nx.MaKP,
                                              nx.KieuDon,
                                              nx.MaBNhan,
                                              nx.MaCC,
                                              nx.MaKPnx,
                                              nx.GhiChu,
                                              nx.MaKPBN
                                          } into kq
                                          select new
                                          {
                                              Ngaythang = kq.Key.NgayNhap,
                                              //ToString().Substring(0,10),
                                              SCTn = kq.Key.PLoai == 1 ? kq.Key.SoCT : "",
                                              SCTx = kq.Key.PLoai != 1 ? kq.Key.SoCT : "",
                                              Solo = kq.Key.SoLo,
                                              MaKP = kq.Key.MaKP,
                                              Soluongton = kq.Sum(p => p.nx.SoLuongN) - kq.Sum(p => p.nx.SoLuongX),
                                              SLNhap = kq.Sum(p => p.nx.SoLuongN),
                                              SLXuat = kq.Sum(p => p.nx.SoLuongX),
                                              Ton = kq.Sum(p => p.nx.SoLuongN) - kq.Sum(p => p.nx.SoLuongX),
                                              Phanloai = kq.Key.PLoai,
                                              NCC = kq.Key.MaCC,
                                              GChu = kq.Key.MaKPBN == null ? (kq.Key.PLoai == 1 ? (kq.Key.KieuDon == 1 ?
                                              _lCC.Where(p => p.MaCC == kq.Key.MaCC).Select(p => p.TenCC).FirstOrDefault() :
                                              _lKP.Where(p => p.MaKP == kq.Key.MaKPnx).Select(p => p.TenKP).FirstOrDefault()) :
                                              (kq.Key.KieuDon == 0 ? (kq.Key.MaBNhan == null ? "" : kq.Key.MaBNhan.ToString()) :
                                              _lKP.Where(p => p.MaKP == kq.Key.MaKPnx).Select(p => p.TenKP).FirstOrDefault())) : _lKP.Where(p => p.MaKP == kq.Key.MaKPBN).Select(p => p.TenKP).FirstOrDefault(),
                                              //   Kho=kq.Key.

                                          }).ToList().Select(a => new { a.Ngaythang, Handung = q2.Where(p => p.SoCT == a.SCTn || p.SoCT == a.SCTx).Select(p => p.HanDung).ToList().Count > 0 ? q2.Where(p => p.SoCT == a.SCTn || p.SoCT == a.SCTx).Select(p => p.HanDung).First() : null, a.SCTn, a.SCTx, a.Solo, a.MaKP, a.Soluongton, a.SLNhap, a.SLXuat, a.Ton, a.Phanloai, a.NCC, a.GChu }).OrderBy(p => p.Ngaythang).ToList();
                                var q11 = (from a in q1
                                           group a
                                               by new
                                               {
                                                   a.Ngaythang.Value.Month,
                                                   Handung = a.Handung,
                                                   //nxct.SoLo,
                                                   //nx.SoCT,
                                                   a.Phanloai,
                                                   a.MaKP,
                                                   a.GChu,
                                                   // Kh.TenKP,
                                                   a.NCC,
                                               } into kq
                                           select new
                                           {
                                               Ngaythang = kq.Key.Month,

                                               //.Date.ToString().Substring(0,10),
                                               //SCT = kq.Key.SoCT,
                                               //Solo = kq.Key.SoLo,
                                               MaKP = kq.Key.MaKP,
                                               Soluongton = kq.Sum(p => p.Soluongton),
                                               SLNhap = kq.Sum(p => p.SLNhap),
                                               SLXuat = kq.Sum(p => p.SLXuat),
                                               Ton = kq.Sum(p => p.Ton),
                                               Phanloai = kq.Key.Phanloai,
                                               NCC = kq.Key.NCC,
                                               HanDung = kq.Key.Handung,
                                               GChu = kq.Key.GChu,
                                               //   Kho=kq.Key.

                                           }).ToList().OrderBy(p => p.Ngaythang).ToList();
                                if (q11.Count > 0)
                                {
                                    BaoCao.rep_thekho rep = new BaoCao.rep_thekho(dongia);
                                    rep.CQCQ.Value = DungChung.Bien.TenCQCQ.ToUpper();
                                    rep.TenDV.Value = DungChung.Bien.TenCQ.ToUpper();
                                    rep.LB_HoatChat_27022.Visible = true;
                                    rep.txt_HoatChat_27022.Visible = true;
                                    //rep.TenDV.Value = lupTenDV.Text;
                                    rep.TenHH.Value = lupTenDV.Text;
                                    rep.ngaytu.Value = ngaytu;
                                    rep.Madv.Value = _Maduoc;
                                    rep.Ngaythang.Value = ngayden;
                                    rep.Khoaphong.Value = lupKho.EditValue.ToString();
                                    rep.Ngay.Value = "Từ ngày: " + ngaytu.ToString("dd/MM/yyyy") + " - Đến ngày: " + ngayden.ToString("dd/MM/yyyy");
                                    var a = (from dv in data.DichVus.Where(p => p.MaDV == _Maduoc) select new {dv.TenHC, dv.HamLuong, dv.DonVi, dv.MaDV, dv.TenDV }).ToList();
                                    if (a.First().HamLuong != null)
                                    {
                                        rep.QPHHH.Value = a.First().HamLuong.ToString();
                                    }
                                    if (a.First().TenHC != null)
                                    {
                                        rep.Parameters["HoatChat"].Value = a.First().TenHC.ToString();
                                    }
                                    if (a.First().DonVi != null)
                                    {
                                        rep.Donvi.Value = a.First().DonVi.ToString();
                                    }
                                    if (a.First().MaDV != null)
                                    {
                                        rep.Maso.Value = a.First().MaDV.ToString();
                                    }
                                    if (lupKho.Text == "")
                                    {
                                        if (lupnhathau.Text == "")
                                        {
                                            rep.DataSource = q11.ToList();
                                            rep.BindingData();
                                            rep.CreateDocument();
                                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                            frm.ShowDialog();
                                        }
                                        else
                                        {
                                            string Ncc = lupnhathau.EditValue.ToString();
                                            rep.Nhacc.Value = lupnhathau.Text;
                                            rep.DataSource = q11.ToList().Where(p => p.NCC == Ncc);
                                            rep.BindingData();
                                            rep.CreateDocument();
                                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                            frm.ShowDialog();
                                        }
                                    }
                                    else
                                    {
                                        if (lupnhathau.Text == "")
                                        {
                                            int _Kho = 0;
                                            _Kho = lupKho.EditValue == null ? 0 : Convert.ToInt32(lupKho.EditValue);
                                            rep.DataSource = q11.ToList().Where(p => p.MaKP == _Kho);
                                            rep.BindingData();
                                            rep.CreateDocument();
                                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                            frm.ShowDialog();
                                        }
                                        else
                                        {
                                            int _Kho = 0;
                                            rep.Nhacc.Value = lupnhathau.Text;
                                            _Kho = lupKho.EditValue == null ? 0 : Convert.ToInt32(lupKho.EditValue);
                                            string Ncc = lupnhathau.EditValue.ToString();
                                            rep.DataSource = q11.ToList().Where(p => p.MaKP == _Kho).Where(p => p.NCC == Ncc);
                                            rep.BindingData();
                                            rep.CreateDocument();
                                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                            frm.ShowDialog();
                                        }
                                    }
                                }
                                else
                                { MessageBox.Show("Không có dữ liệu"); }
                            }
                            else
                            {
                                if (q.Count > 0)
                                {
                                    BaoCao.rep_thekho rep = new BaoCao.rep_thekho(dongia);
                                    rep.LB_HoatChat_27022.Visible = true;
                                    rep.txt_HoatChat_27022.Visible = true;
                                    rep.CQCQ.Value = DungChung.Bien.TenCQCQ.ToUpper();
                                    rep.TenDV.Value = DungChung.Bien.TenCQ.ToUpper();
                                    //rep.TenDV.Value = lupTenDV.Text;
                                    rep.TenHH.Value = lupTenDV.Text;
                                    rep.ngaytu.Value = ngaytu;
                                    rep.Madv.Value = _Maduoc;
                                    rep.Ngaythang.Value = ngayden;
                                    rep.Khoaphong.Value = lupKho.EditValue.ToString();
                                    rep.Ngay.Value = "Từ ngày: " + ngaytu.ToString("dd/MM/yyyy") + " - Đến ngày: " + ngayden.ToString("dd/MM/yyyy");
                                    double _tongnhap = 0;
                                    double _tongxuat = 0;
                                    if (q10.Count > 0)
                                    {
                                        foreach (var item in q10)
                                        {
                                            _tongnhap += item.SLNhap;
                                            _tongxuat += item.SLXuat;
                                        }
                                        rep.LuyKeNhap.Value = _tongnhap;
                                        rep.LuyKeXuat.Value = _tongxuat;
                                    }
                                    var a = (from dv in data.DichVus.Where(p => p.MaDV == _Maduoc) select new {dv.TenHC, dv.HamLuong, dv.DonVi, dv.MaDV, dv.TenDV }).ToList();
                                    if (a.First().HamLuong != null)
                                    {
                                        rep.QPHHH.Value = a.First().HamLuong.ToString();
                                    }
                                    if (a.First().TenHC != null)
                                    {
                                        rep.Parameters["HoatChat"].Value = a.First().TenHC.ToString();
                                    }
                                    if (a.First().DonVi != null)
                                    {
                                        rep.Donvi.Value = a.First().DonVi.ToString();
                                    }
                                    if (a.First().MaDV != null)
                                    {
                                        rep.Maso.Value = a.First().MaDV.ToString();
                                    }
                                    if (lupKho.Text == "")
                                    {
                                        if (lupnhathau.Text == "")
                                        {
                                            rep.DataSource = q.ToList();
                                            rep.BindingData();
                                            rep.CreateDocument();
                                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                            frm.ShowDialog();
                                        }
                                        else
                                        {
                                            string Ncc = lupnhathau.EditValue.ToString();
                                            rep.Nhacc.Value = lupnhathau.Text;
                                            rep.DataSource = q.ToList().Where(p => p.NCC == Ncc);
                                            rep.BindingData();
                                            rep.CreateDocument();
                                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                            frm.ShowDialog();
                                        }
                                    }
                                    else
                                    {
                                        if (lupnhathau.Text == "")
                                        {
                                            int _Kho = 0;
                                            _Kho = lupKho.EditValue == null ? 0 : Convert.ToInt32(lupKho.EditValue);
                                            rep.DataSource = q.ToList().Where(p => p.MaKP == _Kho);
                                            rep.BindingData();
                                            rep.CreateDocument();
                                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                            frm.ShowDialog();
                                        }
                                        else
                                        {
                                            int _Kho = 0;
                                            rep.Nhacc.Value = lupnhathau.Text;
                                            _Kho = lupKho.EditValue == null ? 0 : Convert.ToInt32(lupKho.EditValue);
                                            string Ncc = lupnhathau.EditValue.ToString();
                                            rep.DataSource = q.ToList().Where(p => p.MaKP == _Kho).Where(p => p.NCC == Ncc);
                                            rep.BindingData();
                                            rep.CreateDocument();
                                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                            frm.ShowDialog();
                                        }
                                    }
                                }
                                else
                                { MessageBox.Show("Không có dữ liệu"); }
                            }

                            #endregion
                        }
                    }
                    else
                    {
                        #region Hiển thị theo năm
                        if (checkEdit1.CheckState == CheckState.Checked)
                        {
                            if (textEdit1.Text == null || textEdit1.Text == "" || textEdit1.Text.Trim(' ') == " ")
                            {
                                MessageBox.Show("Bạn đã nhập chưa đúng năm. Vui lòng nhập lại");
                            }
                            else
                            {
                                int _year = Convert.ToInt32(textEdit1.Text);
                                if (_year <= 0)
                                {
                                    MessageBox.Show("Bạn đã nhập số năm âm. Vui lòng nhập lại");
                                }
                                else
                                {
                                    var _ngaytu = "01/01/" + _year + " 00:00:00";
                                    var _ngayden = "31/12/" + _year + " 23:59:59";
                                    if (radChon.SelectedIndex == 0)
                                    {
                                        frmIn frm = new frmIn();

                                        var q = (from nx in qtong
                                                 join dv in _ldv on nx.MaDV equals dv.MaDV
                                                 //join Kh in data.KPhongs on nx.MaKP equals Kh.MaKP
                                                 where (nx.NgayNhap <= ngayden && nx.NgayNhap >= ngaytu)
                                                 group new { nx, dv } by new
                                                 {
                                                     nx.NgayNhap,
                                                     dv.TenDV,
                                                     nx.IDNhap,
                                                     nx.SoLo,
                                                     SoCT = nx.SoCT,
                                                     nx.PLoai,
                                                     nx.MaKP,
                                                     nx.KieuDon,
                                                     nx.MaBNhan,
                                                     nx.MaCC,
                                                     nx.MaKPnx,
                                                     nx.GhiChu,
                                                     nx.MaKPBN,
                                                     nx.TenBNhan
                                                 } into kq
                                                 select new
                                                 {
                                                     Ngaythang = kq.Key.NgayNhap,
                                                     //ToString().Substring(0,10),
                                                     SCTn = kq.Key.PLoai == 1 ? kq.Key.SoCT : "",
                                                     SCTx = kq.Key.PLoai != 1 ? kq.Key.SoCT : "",
                                                     SCT = kq.Key.SoCT,
                                                     Solo = kq.Key.SoLo,
                                                     MaKP = kq.Key.MaKP,
                                                     Soluongton = kq.Sum(p => p.nx.SoLuongN) - kq.Sum(p => p.nx.SoLuongX),
                                                     SLNhap = kq.Sum(p => p.nx.SoLuongN),
                                                     SLXuat = kq.Sum(p => p.nx.SoLuongX),
                                                     Ton = kq.Sum(p => p.nx.SoLuongN) - kq.Sum(p => p.nx.SoLuongX),
                                                     Phanloai = kq.Key.PLoai,
                                                     NCC = kq.Key.MaCC,
                                                     GChu = kq.Key.MaKPBN == null ? (kq.Key.PLoai == 1 ? (kq.Key.KieuDon == 1 ?
                                                     _lCC.Where(p => p.MaCC == kq.Key.MaCC).Select(p => p.TenCC).FirstOrDefault() + (DungChung.Bien.MaBV == "27022" && kq.Key.MaBNhan != null ? (" - " + kq.Key.MaBNhan.ToString() + " - " + kq.Key.TenBNhan) : "") :
                                                     _lKP.Where(p => p.MaKP == kq.Key.MaKPnx).Select(p => p.TenKP).FirstOrDefault() + (DungChung.Bien.MaBV == "27022" && kq.Key.MaBNhan != null ? (" - " + kq.Key.MaBNhan.ToString() + " - " + kq.Key.TenBNhan) : "")) :
                                                     (kq.Key.KieuDon == 0 ? (kq.Key.MaBNhan == null ? "" : (kq.Key.MaBNhan.ToString() + (DungChung.Bien.MaBV == "27022" ? " - " + kq.Key.TenBNhan : ""))) :
                                                     _lKP.Where(p => p.MaKP == kq.Key.MaKPnx).Select(p => p.TenKP).FirstOrDefault())) : _lKP.Where(p => p.MaKP == kq.Key.MaKPBN).Select(p => p.TenKP).FirstOrDefault() + (DungChung.Bien.MaBV == "27022" && kq.Key.MaBNhan != null ? (" - " + kq.Key.MaBNhan.ToString() + " - " + kq.Key.TenBNhan) : ""),
                                                     //   Kho=kq.Key.

                                                 }).ToList().Select(a => new
                                                 {
                                                     a.SCT,
                                                     a.Ngaythang,
                                                     Handung = q2.Where(p => p.SoCT == a.SCTn || p.SoCT == a.SCTx).Select(p => p.HanDung).ToList().Count > 0 ? q2.Where(p => p.SoCT == a.SCTn || p.SoCT == a.SCTx).Select(p => p.HanDung).First() : null,
                                                     Solo_HanDung = a.Solo + " - " + (q2.Where(p => p.SoCT == a.SCTn || p.SoCT == a.SCTx).Select(p => p.HanDung).ToList().Count > 0 ? q2.Where(p => p.SoCT == a.SCTn || p.SoCT == a.SCTx).Select(p => p.HanDung).FirstOrDefault().ToString().Substring(0, 9) : null),
                                                     a.SCTn,
                                                     a.SCTx,
                                                     a.Solo,
                                                     a.MaKP,
                                                     a.Soluongton,
                                                     a.SLNhap,
                                                     a.SLXuat,
                                                     a.Ton,
                                                     a.Phanloai,
                                                     a.NCC,
                                                     a.GChu
                                                 }).OrderBy(p => p.Ngaythang).ToList();

                                        /**
                                         SELECT         SUM(NhapDct.SoLuongN) AS [Lũy kế xuất], SUM(NhapDct.SoLuongX) AS [Lũy kế nhập]
                    FROM            NhapD INNER JOIN
                                             NhapDct ON NhapD.IDNhap = NhapDct.IDNhap
                    WHERE        (NhapD.NgayNhap < CONVERT(DATETIME, '2020-03-11 00:00:00', 102)) AND (NhapD.MaKP = 6) AND (NhapDct.MaDV = 4231)
                                         */
                                        int _MaKP = 0;
                                        if (lupKho.EditValue != null)
                                            _MaKP = Convert.ToInt32(lupKho.EditValue);
                                        DateTime _ngaytu1 = Convert.ToDateTime(_ngaytu);
                                        var q1 = (from _nd in data.NhapDs.Where(p => p.NgayNhap < ngayden && p.NgayNhap >= _ngaytu1).Where(o => o.MaKP == _MaKP)
                                                  join _ndct in data.NhapDcts.Where(p => p.MaDV == _Maduoc && (dongia == 0 || p.DonGia == dongia)) on _nd.IDNhap equals _ndct.IDNhap
                                                  group new { _nd, _ndct } by new
                                                  {
                                                      _nd.NgayNhap,
                                                      _ndct.SoLuongN,
                                                      _ndct.SoLuongX
                                                  } into kq
                                                  select new
                                                  {
                                                      SLNhap = kq.Sum(p => p._ndct.SoLuongN),
                                                      SLXuat = kq.Sum(p => p._ndct.SoLuongX)
                                                  }).ToList();



                                        if (q.Count > 0)
                                        {
                                            if (checkmaumoi.Checked == false)
                                            {
                                                BaoCao.rep_thekho rep = new BaoCao.rep_thekho(dongia);
                                                rep.CQCQ.Value = DungChung.Bien.TenCQCQ.ToUpper();
                                                rep.TenDV.Value = DungChung.Bien.TenCQ.ToUpper();
                                                rep.LB_HoatChat_27022.Visible = true;
                                                rep.txt_HoatChat_27022.Visible = true;
                                                //rep.TenDV.Value = lupTenDV.Text;
                                                rep.TenHH.Value = lupTenDV.Text;
                                                rep.ngaytu.Value = ngaytu;
                                                rep.Madv.Value = _Maduoc;
                                                rep.Ngaythang.Value = ngayden;
                                                rep.Khoaphong.Value = lupKho.EditValue.ToString();
                                                rep.Ngay.Value = "Từ ngày: " + ngaytu.ToString("dd/MM/yyyy") + " - Đến ngày: " + ngayden.ToString("dd/MM/yyyy");
                                                //string tenkttk = data.CanBoes.Where(p => p.MaCB == DungChung.Bien.MaCB).FirstOrDefault().TenCB;
                                                //rep.txtKTTK.Text = tenkttk;
                                                double _tongnhap = 0;
                                                double _tongxuat = 0;
                                                if (q1.Count > 0)
                                                {
                                                    foreach (var item in q1)
                                                    {
                                                        _tongnhap += item.SLNhap;
                                                        _tongxuat += item.SLXuat;
                                                    }
                                                    rep.LuyKeNhap.Value = _tongnhap;
                                                    rep.LuyKeXuat.Value = _tongxuat;
                                                }

                                                var a = (from dv in data.DichVus.Where(p => p.MaDV == _Maduoc) select new { dv.TenHC, dv.HamLuong, dv.DonVi, dv.MaDV, dv.TenDV }).ToList();

                                                if (a.First().HamLuong != null)
                                                {
                                                    rep.QPHHH.Value = a.First().HamLuong.ToString();
                                                }
                                                if (a.First().DonVi != null)
                                                {
                                                    rep.Donvi.Value = a.First().DonVi.ToString();
                                                }
                                                if (a.First().TenHC != null)
                                                {
                                                    rep.Parameters["HoatChat"].Value = a.First().TenHC.ToString();
                                                }
                                                if (a.First().MaDV != null)
                                                {
                                                    rep.Maso.Value = a.First().MaDV.ToString();
                                                }
                                                if (lupKho.Text == "")
                                                {
                                                    if (lupnhathau.Text == "")
                                                    {
                                                        rep.DataSource = q.ToList();
                                                        rep.BindingData();
                                                        rep.CreateDocument();
                                                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                                        frm.ShowDialog();
                                                    }
                                                    else
                                                    {
                                                        string Ncc = lupnhathau.EditValue.ToString();
                                                        rep.Nhacc.Value = lupnhathau.Text;
                                                        rep.DataSource = q.ToList().Where(p => p.NCC == Ncc);
                                                        rep.BindingData();
                                                        rep.CreateDocument();
                                                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                                        frm.ShowDialog();
                                                    }
                                                }
                                                else
                                                {
                                                    if (lupnhathau.Text == "")
                                                    {
                                                        int _Kho = 0;
                                                        _Kho = lupKho.EditValue == null ? 0 : Convert.ToInt32(lupKho.EditValue);
                                                        rep.DataSource = (q.ToList().Where(p => p.MaKP == _Kho)).ToList();
                                                        rep.BindingData();
                                                        rep.CreateDocument();
                                                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                                        frm.ShowDialog();
                                                    }
                                                    else
                                                    {
                                                        int _Kho = 0;
                                                        rep.Nhacc.Value = lupnhathau.Text;
                                                        _Kho = lupKho.EditValue == null ? 0 : Convert.ToInt32(lupKho.EditValue);
                                                        string Ncc = lupnhathau.EditValue.ToString();
                                                        rep.DataSource = q.ToList().Where(p => p.MaKP == _Kho).Where(p => p.NCC == Ncc);
                                                        rep.BindingData();
                                                        rep.CreateDocument();
                                                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                                        frm.ShowDialog();
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                BaoCao.rep_NXT_GAYNG_HTT rep = new BaoCao.rep_NXT_GAYNG_HTT();
                                                rep.CQCQ.Value = DungChung.Bien.TenCQCQ.ToUpper();
                                                rep.TenCQ.Value = DungChung.Bien.TenCQ.ToUpper();
                                                //rep.TenDV.Value = lupTenDV.Text;
                                                rep.TenHH.Value = lupTenDV.Text;
                                                rep.ngaytu.Value = ngaytu;
                                                rep.Madv.Value = _Maduoc;
                                                rep.Ngaythang.Value = ngayden;
                                                rep.Khoaphong.Value = lupKho.EditValue.ToString();
                                                string tenkttk = data.CanBoes.Where(p => p.MaCB == DungChung.Bien.MaCB).FirstOrDefault().TenCB;
                                                rep.txtKTTK.Text = tenkttk;
                                                var a = (from dv in data.DichVus.Where(p => p.MaDV == _Maduoc) select new {dv.TenHC, dv.HamLuong, dv.DonVi, dv.MaDV, dv.TenDV }).ToList();
                                                if (a.First().HamLuong != null)
                                                {
                                                    rep.QPHHH.Value = a.First().HamLuong.ToString();
                                                }
                                                if (a.First().TenHC != null)
                                                {
                                                    rep.Parameters["HoatChat"].Value = a.First().TenHC.ToString();
                                                }
                                                if (a.First().DonVi != null)
                                                {
                                                    rep.Donvi.Value = a.First().DonVi.ToString();
                                                }
                                                if (a.First().MaDV != null)
                                                {
                                                    rep.Maso.Value = a.First().MaDV.ToString();
                                                }
                                                if (lupKho.Text == "")
                                                {
                                                    if (lupnhathau.Text == "")
                                                    {
                                                        rep.DataSource = q.ToList();
                                                        rep.BindingData();
                                                        rep.CreateDocument();
                                                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                                        frm.ShowDialog();
                                                    }
                                                    else
                                                    {
                                                        string Ncc = lupnhathau.EditValue.ToString();
                                                        rep.Nhacc.Value = lupnhathau.Text;
                                                        rep.DataSource = q.ToList().Where(p => p.NCC == Ncc);
                                                        rep.BindingData();
                                                        rep.CreateDocument();
                                                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                                        frm.ShowDialog();
                                                    }
                                                }
                                                else
                                                {
                                                    if (lupnhathau.Text == "")
                                                    {
                                                        int _Kho = 0;
                                                        _Kho = lupKho.EditValue == null ? 0 : Convert.ToInt32(lupKho.EditValue);
                                                        rep.DataSource = q.ToList().Where(p => p.MaKP == _Kho);
                                                        rep.BindingData();
                                                        rep.CreateDocument();
                                                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                                        frm.ShowDialog();
                                                    }
                                                    else
                                                    {
                                                        int _Kho = 0;
                                                        rep.Nhacc.Value = lupnhathau.Text;
                                                        _Kho = lupKho.EditValue == null ? 0 : Convert.ToInt32(lupKho.EditValue);
                                                        string Ncc = lupnhathau.EditValue.ToString();
                                                        rep.DataSource = q.ToList().Where(p => p.MaKP == _Kho).Where(p => p.NCC == Ncc);
                                                        rep.BindingData();
                                                        rep.CreateDocument();
                                                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                                        frm.ShowDialog();
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        { MessageBox.Show("Không có dữ liệu"); }

                                    }
                                    else
                                    {
                                        #region Hiển thị theo tháng

                                        frmIn frm = new frmIn();
                                        var q = (from nx in qtong
                                                 join dv in _ldv on nx.MaDV equals dv.MaDV
                                                 //join Kh in data.KPhongs on nx.MaKP equals Kh.MaKP
                                                 where (nx.NgayNhap <= ngayden && nx.NgayNhap >= ngaytu)
                                                 group new { nx, dv } by new
                                                 {
                                                     nx.NgayNhap.Value.Month,
                                                     dv.TenDV,
                                                     HanDung = nx.HanDung ?? Convert.ToDateTime("01/01/2010"),
                                                     //nx.IDNhap,
                                                     //nxct.SoLo,
                                                     //nx.SoCT,
                                                     nx.PLoai,
                                                     nx.MaKP,
                                                     //nx.GhiChu,
                                                     // Kh.TenKP,
                                                     nx.MaCC,

                                                 } into kq
                                                 select new
                                                 {
                                                     Ngaythang = kq.Key.Month,
                                                     //ToString().Substring(0,10),
                                                     //.Date.ToString().Substring(0,10),
                                                     //SCT = kq.Key.SoCT,
                                                     //Solo = kq.Key.SoLo,
                                                     MaKP = kq.Key.MaKP,
                                                     Soluongton = kq.Sum(p => p.nx.SoLuongN) - kq.Sum(p => p.nx.SoLuongX),
                                                     SLNhap = kq.Sum(p => p.nx.SoLuongN),
                                                     SLXuat = kq.Sum(p => p.nx.SoLuongX),
                                                     Ton = kq.Sum(p => p.nx.SoLuongN) - kq.Sum(p => p.nx.SoLuongX),
                                                     Phanloai = kq.Key.PLoai,
                                                     NCC = kq.Key.MaCC,
                                                     HanDung = kq.Key.HanDung,
                                                     GChu = ""
                                                     //GChu = kq.Key.GhiChu,
                                                     //   Kho=kq.Key.

                                                 }).ToList().OrderBy(p => p.Ngaythang).ToList();

                                        int _MaKP = 0;
                                        if (lupKho.EditValue != null)
                                            _MaKP = Convert.ToInt32(lupKho.EditValue);
                                        DateTime _ngaytu1 = Convert.ToDateTime(_ngaytu);
                                        var q10 = (from _nd in data.NhapDs.Where(p => p.NgayNhap < ngayden && p.NgayNhap >= _ngaytu1).Where(o => o.MaKP == _MaKP)
                                                   join _ndct in data.NhapDcts.Where(p => p.MaDV == _Maduoc && (dongia == 0 || p.DonGia == dongia)) on _nd.IDNhap equals _ndct.IDNhap
                                                   group new { _nd, _ndct } by new
                                                   {
                                                       _nd.NgayNhap,
                                                       _ndct.SoLuongN,
                                                       _ndct.SoLuongX
                                                   } into kq
                                                   select new
                                                   {
                                                       SLNhap = kq.Sum(p => p._ndct.SoLuongN),
                                                       SLXuat = kq.Sum(p => p._ndct.SoLuongX)
                                                   }).ToList();


                                        if (DungChung.Bien.MaBV == "20001")
                                        {
                                            var q1 = (from nx in qtong
                                                      join dv in _ldv on nx.MaDV equals dv.MaDV
                                                      //join Kh in data.KPhongs on nx.MaKP equals Kh.MaKP
                                                      where (nx.NgayNhap <= ngayden && nx.NgayNhap >= ngaytu)
                                                      group new { nx, dv } by new
                                                      {
                                                          nx.NgayNhap,
                                                          dv.TenDV,
                                                          nx.IDNhap,
                                                          nx.SoLo,
                                                          SoCT = nx.SoCT,
                                                          nx.PLoai,
                                                          nx.MaKP,
                                                          nx.KieuDon,
                                                          nx.MaBNhan,
                                                          nx.MaCC,
                                                          nx.MaKPnx,
                                                          nx.GhiChu,
                                                          nx.MaKPBN
                                                      } into kq
                                                      select new
                                                      {
                                                          Ngaythang = kq.Key.NgayNhap,
                                                          //ToString().Substring(0,10),
                                                          SCTn = kq.Key.PLoai == 1 ? kq.Key.SoCT : "",
                                                          SCTx = kq.Key.PLoai != 1 ? kq.Key.SoCT : "",
                                                          Solo = kq.Key.SoLo,
                                                          MaKP = kq.Key.MaKP,
                                                          Soluongton = kq.Sum(p => p.nx.SoLuongN) - kq.Sum(p => p.nx.SoLuongX),
                                                          SLNhap = kq.Sum(p => p.nx.SoLuongN),
                                                          SLXuat = kq.Sum(p => p.nx.SoLuongX),
                                                          Ton = kq.Sum(p => p.nx.SoLuongN) - kq.Sum(p => p.nx.SoLuongX),
                                                          Phanloai = kq.Key.PLoai,
                                                          NCC = kq.Key.MaCC,
                                                          GChu = kq.Key.MaKPBN == null ? (kq.Key.PLoai == 1 ? (kq.Key.KieuDon == 1 ?
                                                          _lCC.Where(p => p.MaCC == kq.Key.MaCC).Select(p => p.TenCC).FirstOrDefault() :
                                                          _lKP.Where(p => p.MaKP == kq.Key.MaKPnx).Select(p => p.TenKP).FirstOrDefault()) :
                                                          (kq.Key.KieuDon == 0 ? (kq.Key.MaBNhan == null ? "" : kq.Key.MaBNhan.ToString()) :
                                                          _lKP.Where(p => p.MaKP == kq.Key.MaKPnx).Select(p => p.TenKP).FirstOrDefault())) : _lKP.Where(p => p.MaKP == kq.Key.MaKPBN).Select(p => p.TenKP).FirstOrDefault(),
                                                          //   Kho=kq.Key.

                                                      }).ToList().Select(a => new { a.Ngaythang, Handung = q2.Where(p => p.SoCT == a.SCTn || p.SoCT == a.SCTx).Select(p => p.HanDung).ToList().Count > 0 ? q2.Where(p => p.SoCT == a.SCTn || p.SoCT == a.SCTx).Select(p => p.HanDung).First() : null, a.SCTn, a.SCTx, a.Solo, a.MaKP, a.Soluongton, a.SLNhap, a.SLXuat, a.Ton, a.Phanloai, a.NCC, a.GChu }).OrderBy(p => p.Ngaythang).ToList();
                                            var q11 = (from a in q1
                                                       group a
                                                           by new
                                                           {
                                                               a.Ngaythang.Value.Month,
                                                               Handung = a.Handung,
                                                               //nxct.SoLo,
                                                               //nx.SoCT,
                                                               a.Phanloai,
                                                               a.MaKP,
                                                               a.GChu,
                                                               // Kh.TenKP,
                                                               a.NCC,
                                                           } into kq
                                                       select new
                                                       {
                                                           Ngaythang = kq.Key.Month,

                                                           //.Date.ToString().Substring(0,10),
                                                           //SCT = kq.Key.SoCT,
                                                           //Solo = kq.Key.SoLo,
                                                           MaKP = kq.Key.MaKP,
                                                           Soluongton = kq.Sum(p => p.Soluongton),
                                                           SLNhap = kq.Sum(p => p.SLNhap),
                                                           SLXuat = kq.Sum(p => p.SLXuat),
                                                           Ton = kq.Sum(p => p.Ton),
                                                           Phanloai = kq.Key.Phanloai,
                                                           NCC = kq.Key.NCC,
                                                           HanDung = kq.Key.Handung,
                                                           GChu = kq.Key.GChu,
                                                           //   Kho=kq.Key.

                                                       }).ToList().OrderBy(p => p.Ngaythang).ToList();
                                            if (q11.Count > 0)
                                            {
                                                BaoCao.rep_thekho rep = new BaoCao.rep_thekho(dongia);
                                                rep.LB_HoatChat_27022.Visible = true;
                                                rep.txt_HoatChat_27022.Visible = true;
                                                rep.CQCQ.Value = DungChung.Bien.TenCQCQ.ToUpper();
                                                rep.TenDV.Value = DungChung.Bien.TenCQ.ToUpper();
                                                //rep.TenDV.Value = lupTenDV.Text;
                                                rep.TenHH.Value = lupTenDV.Text;
                                                rep.ngaytu.Value = ngaytu;
                                                rep.Madv.Value = _Maduoc;
                                                rep.Ngaythang.Value = ngayden;
                                                rep.Khoaphong.Value = lupKho.EditValue.ToString();
                                                rep.Ngay.Value = "Từ ngày: " + ngaytu.ToString("dd/MM/yyyy") + " - Đến ngày: " + ngayden.ToString("dd/MM/yyyy");
                                                var a = (from dv in data.DichVus.Where(p => p.MaDV == _Maduoc) select new {dv.TenHC, dv.HamLuong, dv.DonVi, dv.MaDV, dv.TenDV }).ToList();
                                                if (a.First().HamLuong != null)
                                                {
                                                    rep.QPHHH.Value = a.First().HamLuong.ToString();
                                                }
                                                if (a.First().DonVi != null)
                                                {
                                                    rep.Donvi.Value = a.First().DonVi.ToString();
                                                }
                                                if (a.First().MaDV != null)
                                                {
                                                    rep.Maso.Value = a.First().MaDV.ToString();
                                                }
                                                if (a.First().TenHC != null)
                                                {
                                                    rep.Parameters["HoatChat"].Value = a.First().TenHC.ToString();
                                                }
                                                if (lupKho.Text == "")
                                                {
                                                    if (lupnhathau.Text == "")
                                                    {
                                                        rep.DataSource = q11.ToList();
                                                        rep.BindingData();
                                                        rep.CreateDocument();
                                                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                                        frm.ShowDialog();
                                                    }
                                                    else
                                                    {
                                                        string Ncc = lupnhathau.EditValue.ToString();
                                                        rep.Nhacc.Value = lupnhathau.Text;
                                                        rep.DataSource = q11.ToList().Where(p => p.NCC == Ncc);
                                                        rep.BindingData();
                                                        rep.CreateDocument();
                                                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                                        frm.ShowDialog();
                                                    }
                                                }
                                                else
                                                {
                                                    if (lupnhathau.Text == "")
                                                    {
                                                        int _Kho = 0;
                                                        _Kho = lupKho.EditValue == null ? 0 : Convert.ToInt32(lupKho.EditValue);
                                                        rep.DataSource = q11.ToList().Where(p => p.MaKP == _Kho);
                                                        rep.BindingData();
                                                        rep.CreateDocument();
                                                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                                        frm.ShowDialog();
                                                    }
                                                    else
                                                    {
                                                        int _Kho = 0;
                                                        rep.Nhacc.Value = lupnhathau.Text;
                                                        _Kho = lupKho.EditValue == null ? 0 : Convert.ToInt32(lupKho.EditValue);
                                                        string Ncc = lupnhathau.EditValue.ToString();
                                                        rep.DataSource = q11.ToList().Where(p => p.MaKP == _Kho).Where(p => p.NCC == Ncc);
                                                        rep.BindingData();
                                                        rep.CreateDocument();
                                                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                                        frm.ShowDialog();
                                                    }
                                                }
                                            }
                                            else
                                            { MessageBox.Show("Không có dữ liệu"); }
                                        }
                                        else
                                        {
                                            if (q.Count > 0)
                                            {
                                                BaoCao.rep_thekho rep = new BaoCao.rep_thekho(dongia);
                                                rep.LB_HoatChat_27022.Visible = true;
                                                rep.txt_HoatChat_27022.Visible = true;
                                                rep.CQCQ.Value = DungChung.Bien.TenCQCQ.ToUpper();
                                                rep.TenDV.Value = DungChung.Bien.TenCQ.ToUpper();
                                                //rep.TenDV.Value = lupTenDV.Text;
                                                rep.TenHH.Value = lupTenDV.Text;
                                                rep.ngaytu.Value = ngaytu;
                                                rep.Madv.Value = _Maduoc;
                                                rep.Ngaythang.Value = ngayden;
                                                rep.Khoaphong.Value = lupKho.EditValue.ToString();
                                                double _tongnhap = 0;
                                                double _tongxuat = 0;
                                                if (q10.Count > 0)
                                                {
                                                    foreach (var item in q10)
                                                    {
                                                        _tongnhap += item.SLNhap;
                                                        _tongxuat += item.SLXuat;
                                                    }
                                                    rep.LuyKeNhap.Value = _tongnhap;
                                                    rep.LuyKeXuat.Value = _tongxuat;
                                                }
                                                var a = (from dv in data.DichVus.Where(p => p.MaDV == _Maduoc) select new {dv.TenHC, dv.HamLuong, dv.DonVi, dv.MaDV, dv.TenDV }).ToList();
                                                if (a.First().HamLuong != null)
                                                {
                                                    rep.QPHHH.Value = a.First().HamLuong.ToString();
                                                }
                                                if (a.First().DonVi != null)
                                                {
                                                    rep.Donvi.Value = a.First().DonVi.ToString();
                                                }
                                                if (a.First().MaDV != null)
                                                {
                                                    rep.Maso.Value = a.First().MaDV.ToString();
                                                }
                                                if (a.First().TenHC != null)
                                                {
                                                    rep.Parameters["HoatChat"].Value = a.First().TenHC.ToString();
                                                }
                                                if (lupKho.Text == "")
                                                {
                                                    if (lupnhathau.Text == "")
                                                    {
                                                        rep.DataSource = q.ToList();
                                                        rep.BindingData();
                                                        rep.CreateDocument();
                                                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                                        frm.ShowDialog();
                                                    }
                                                    else
                                                    {
                                                        string Ncc = lupnhathau.EditValue.ToString();
                                                        rep.Nhacc.Value = lupnhathau.Text;
                                                        rep.DataSource = q.ToList().Where(p => p.NCC == Ncc);
                                                        rep.BindingData();
                                                        rep.CreateDocument();
                                                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                                        frm.ShowDialog();
                                                    }
                                                }
                                                else
                                                {
                                                    if (lupnhathau.Text == "")
                                                    {
                                                        int _Kho = 0;
                                                        _Kho = lupKho.EditValue == null ? 0 : Convert.ToInt32(lupKho.EditValue);
                                                        rep.DataSource = q.ToList().Where(p => p.MaKP == _Kho);
                                                        rep.BindingData();
                                                        rep.CreateDocument();
                                                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                                        frm.ShowDialog();
                                                    }
                                                    else
                                                    {
                                                        int _Kho = 0;
                                                        rep.Nhacc.Value = lupnhathau.Text;
                                                        _Kho = lupKho.EditValue == null ? 0 : Convert.ToInt32(lupKho.EditValue);
                                                        string Ncc = lupnhathau.EditValue.ToString();
                                                        rep.DataSource = q.ToList().Where(p => p.MaKP == _Kho).Where(p => p.NCC == Ncc);
                                                        rep.BindingData();
                                                        rep.CreateDocument();
                                                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                                        frm.ShowDialog();
                                                    }
                                                }
                                            }
                                            else
                                            { MessageBox.Show("Không có dữ liệu"); }
                                        }
                                        #endregion
                                    }
                                }
                            }
                        }
                        #endregion
                    }
                    #endregion
                }
                #endregion
            }
        }



        private void lupKho_EditValueChanged(object sender, EventArgs e)
        {
            int _MaKP = 0;
            if (lupKho.EditValue != null)
                _MaKP = Convert.ToInt32(lupKho.EditValue);

            var q2 = (from nd in data.NhapDs.Where(p => p.MaKP == _MaKP && p.PLoai == 1)
                      join nxct in data.NhapDcts on nd.IDNhap equals nxct.IDNhap
                      join DV in data.DichVus on nxct.MaDV equals DV.MaDV
                      select new { DV.TenDV, DV.MaDV, DV.MaTam }).ToList();
            var q = (from nd in q2
                     group new { nd } by new { nd.TenDV, nd.MaDV, nd.MaTam } into kq
                     select new { TenDV = kq.Key.TenDV, MaDV = kq.Key.MaDV, MaNB = kq.Key.MaTam }).OrderBy(P => P.TenDV).ToList();
            lupTenDV.Properties.DataSource = q;
        }

        private void checkmaumoi_CheckedChanged(object sender, EventArgs e)
        {
            radChon.Enabled = false;
            radChon.SelectedIndex = 0;
        }

        private void checkEdit1_CheckStateChanged(object sender, EventArgs e)
        {
            if (DungChung.Bien.MaBV == "27022")
            {

                if (checkEdit1.CheckState == CheckState.Checked)
                {
                    //lupngaytu.Enabled = false;
                    //lupngay.Enabled = false;
                    textEdit1.Enabled = true;
                    textEdit1.Text = DateTime.Now.Year.ToString();
                }
                else
                {
                    //lupngaytu.Enabled = true;
                    //lupngay.Enabled = true;
                    textEdit1.Enabled = false;
                    textEdit1.Text = "";
                }
            }


        }

        private void textEdit1_Validated(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textEdit1.Text))
            {
                textEdit1.Focus();
                errorProvider1.SetError(textEdit1, "Bạn chưa nhập năm tính lũy kế. Vui lòng nhập lại");
            }
            else
            {
                int n = Convert.ToInt32(textEdit1.Text);
                if (n <= 0)
                    errorProvider1.SetError(textEdit1, "Bạn nhập năm âm. Vui lòng nhập lại !");
                else
                    errorProvider1.SetError(textEdit1, null);
            }
        }

        private void textEdit1_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(textEdit1.Text))
            {
                textEdit1.Focus();
                errorProvider1.SetError(textEdit1, "Bạn chưa nhập năm tính lũy kế. Vui lòng nhập lại");
            }
            else
            {
                int n = Convert.ToInt32(textEdit1.Text);
                if (n <= 0)
                    errorProvider1.SetError(textEdit1, "Bạn nhập năm âm. Vui lòng nhập lại !");
                else
                    errorProvider1.SetError(textEdit1, null);
            }
        }

        private void simpleButton1_Click_1(object sender, EventArgs e)
        {

        }

        private void simpleButton2_Click_1(object sender, EventArgs e)
        {

        }

        private void panelControl2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}