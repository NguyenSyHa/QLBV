using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;

namespace QLBV.FormThamSo
{
    public partial class frm_THChungTuMuaThuoc : DevExpress.XtraEditors.XtraForm
    {
        public frm_THChungTuMuaThuoc()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities _data;
        List<KPhong> _lKho = new List<KPhong>();
        List<NhaCC> _lNCC = new List<NhaCC>();
        private void frm_THChungTuMuaThuoc_Load(object sender, EventArgs e)
        {
            _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            dtTuNgay.EditValue = DateTime.Now.AddMonths(-1);
            dtDenNgay.EditValue = DateTime.Now;
            _lKho = _data.KPhongs.OrderBy(p => p.TenKP).Where(p => p.PLoai == "Khoa dược").ToList();
            _lKho.Insert(0, new KPhong { TenKP = "Tất cả", MaKP = 0 });
            lupKho.Properties.DataSource = _lKho;
            _lNCC = _data.NhaCCs.OrderBy(p => p.TenCC).ToList();
            _lNCC.Insert(0, new NhaCC { TenCC = "Tất cả", MaCC = "" });
            lupNcc.Properties.DataSource = _lNCC;
        }


        private void btnIn_Click(object sender, EventArgs e)
        {
            _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            string maNCC = "";
            int maKho = 0;
            if (lupNcc.EditValue != null)
                maNCC = lupNcc.EditValue.ToString();
            if (lupKho.EditValue != null)
                maKho = Convert.ToInt32(lupKho.EditValue);
            var dichvu = _data.DichVus.Where(p => p.PLoai == 1).ToList();
            if (checkValidate())
            {
                DateTime tungay = DungChung.Ham.NgayTu(dtTuNgay.DateTime);
                DateTime denngay = DungChung.Ham.NgayDen(dtDenNgay.DateTime);
                if (rgchonmau.SelectedIndex == 0)
                {
                    var q = (from dct in _data.NhapDcts
                             join d in _data.NhapDs on dct.IDNhap equals d.IDNhap
                             join n in _data.NhaCCs on d.MaCC equals n.MaCC
                             join k in _data.KPhongs on d.MaKP equals k.MaKP
                             select new
                             {
                                 dct.MaDV,
                                 d.NgayNhap,
                                 d.SoCT,
                                 dct.SoLuongN,
                                 dct.ThanhTienN,
                                 dct.ThanhTienDY,
                                 dct.DonGia,
                                 n.MaCC,
                                 n.TenCC,
                                 k.MaKP,
                                 d.GhiChu
                             })
                             .Where(p => p.NgayNhap < denngay).Where(p => p.NgayNhap > tungay).Where(p => maNCC == "" ? true : p.MaCC == maNCC).Where(p => maKho == 0 ? true : p.MaKP == maKho).ToList();
                    var ds = (from n in q
                              join dv in dichvu on n.MaDV equals dv.MaDV
                              group n by new { dv.DongY, n.NgayNhap, n.SoCT, n.TenCC, n.GhiChu } into kq
                              select new
                              {
                                  NgayNhap = kq.Key.NgayNhap,
                                  SoCT = kq.Key.SoCT,
                                  TenCC = kq.Key.TenCC,
                                  TT = kq.Key.DongY == 1 ? kq.Sum(p => p.ThanhTienDY) : kq.Sum(p => p.ThanhTienN),
                                  GhiChu = kq.Key.GhiChu
                              }).OrderBy(p => p.TenCC).ThenBy(p => p.NgayNhap).ToList();
                    frmIn frm = new frmIn();
                    BaoCao.rep_THChungTuMuaThuoc rep = new BaoCao.rep_THChungTuMuaThuoc();
                    rep.TitLe.Value = ("Bảng tổng hợp chứng từ mua thuốc").ToUpper();
                    rep.Ngay.Value = "Từ ngày:  " + dtTuNgay.Text + "  đến ngày:  " + dtDenNgay.Text;
                    rep.TenCQ.Value = DungChung.Bien.TenCQ.ToUpper();
                    rep.TenKhoa.Value = ("Khoa dược").ToUpper();
                    if (lupKho.Text == "Tất cả")
                        rep.TenKho.Value = "";
                    else
                        rep.TenKho.Value = (lupKho.Text).ToUpper();
                    rep.DataSource = ds;
                    rep.BindingData();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    this.Hide();
                    frm.ShowDialog();
                    this.Show();
                }
                else
                {
                    var _lnd = (from nd in _data.NhapDs.Where(p => p.NgayNhap < denngay).Where(p => p.NgayNhap > tungay).Where(p => maKho == 0 ? true : p.MaKP == maKho)
                                join ndct in _data.NhapDcts on nd.IDNhap equals ndct.IDNhap
                                select new { nd, ndct }).ToList();
                    var _lncc = _data.NhaCCs.Where(p => maNCC == "" ? true : p.MaCC == maNCC).ToList();
                    var _ldv = _data.DichVus.Where(p => p.PLoai == 1).ToList();
                    var _ldsnhap = (from nd in _lnd
                                    join ncc in _lncc on nd.nd.MaCC equals ncc.MaCC
                                    join dv in _ldv on nd.ndct.MaDV equals dv.MaDV
                                    select new
                                    {
                                        nd.nd.SoCT,
                                        nd.nd.NgayNhap,
                                        nd.nd.MaCC,
                                        nd.nd.MaKP,
                                        ncc.TenCC,
                                        dv.TenDV,
                                        nd.ndct.SoLuongN,
                                        nd.ndct.ThanhTienN,
                                        nd.ndct.MaDV,
                                        dv.DongY,
                                        nd.ndct.DonVi,
                                        nd.ndct.DonGia,
                                        nd.ndct.ThanhTienDY,
                                        nd.ndct.SoLo,
                                        nd.ndct.HanDung,

                                    }).ToList();
                    //.Where(p => p.NgayNhap < denngay).Where(p => p.NgayNhap > tungay)
                    //.Where(p => maKho == 0 ? true : p.MaKP == maKho)
                    //.Where(p => maNCC == "" ? true : p.MaCC == maNCC)
                    var _lkq = (from k in _ldsnhap
                                group k by new { k.SoCT, NgayNhap = k.NgayNhap.Value.Date, k.MaCC, k.TenCC, k.DongY, k.TenDV, k.MaDV, k.DonGia, k.DonVi, k.SoLo, k.HanDung } into kq
                                select new
                                {
                                    kq.Key.MaCC,
                                    MaDV = kq.Key.MaDV,
                                    NgayNhap = kq.Key.NgayNhap,
                                    SoCT = kq.Key.SoCT,
                                    TenCC = kq.Key.TenCC,
                                    TenDV = kq.Key.TenDV,
                                    DonGia = kq.Key.DonGia,
                                    DonVi = kq.Key.DonVi,
                                    Solo = kq.Key.SoLo,
                                    handung = kq.Key.HanDung,
                                    TT = kq.Key.DongY == 1 ? kq.Sum(p => p.ThanhTienDY) : kq.Sum(p => p.ThanhTienN),
                                    soluong = kq.Sum(p => p.SoLuongN)
                                }).OrderBy(p => p.NgayNhap).ThenBy(p => p.SoCT).ToList();
                    frmIn frm = new frmIn();
                    BaoCao.rep_Thchungtumuathuocchitiet rep = new BaoCao.rep_Thchungtumuathuocchitiet();
                    rep.tieude.Text = ("Bảng tổng hợp chứng từ mua thuốc").ToUpper();
                    rep.colNTN.Text = "Từ ngày:  " + dtTuNgay.Text + "  đến ngày:  " + dtDenNgay.Text;
                    rep.col_tencqcq.Text = DungChung.Bien.TenCQ.ToUpper();
                    rep.col_tenbv.Text = ("Khoa dược").ToUpper();
                    if (lupKho.Text == "Tất cả")
                        rep.col_tenkhoa.Text = "";
                    else
                        rep.col_tenkhoa.Text = (lupKho.Text).ToUpper();
                    rep.DataSource = _lkq;
                    rep.databinding();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                }
            }

        }

        private bool checkValidate()
        {
            if (dtTuNgay.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn từ ngày");
                dtTuNgay.Focus();
                return false;
            }
            else if (dtDenNgay.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn đến ngày");
                dtDenNgay.Focus();
                return false;
            }
            else if (lupKho.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn khoa phòng");
                lupKho.Focus();
                return false;
            }

            else return true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void labelControl5_Click(object sender, EventArgs e)
        {

        }

    }
}