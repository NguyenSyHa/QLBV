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
    public partial class frm_BC_CTNhapThuoc_VTYT_30009 : DevExpress.XtraEditors.XtraForm
    {
        public frm_BC_CTNhapThuoc_VTYT_30009()
        {
            InitializeComponent();
        }

        #region class kho dược
        private class KhoDuoc
        {
            public bool Chon { get; set; }
            public int MaKP { get; set; }
            public string TenKP { get; set; }
        }
        #endregion
        List<KhoDuoc> _lKhoDuoc = new List<KhoDuoc>();
        private void frm_BC_CTNhapThuoc_VTYT_30009_Load(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            date_TuNgay.DateTime = DateTime.Now;
            date_DenNgay.DateTime = DateTime.Now;
            var qkho = (from kp in data.KPhongs.Where(p => p.PLoai.Equals("Khoa dược")) select new { kp.MaKP, kp.TenKP });
            List<NhomDV> _lnhom = new List<NhomDV>();
            _lnhom = data.NhomDVs.Where(p => p.Status == 1).ToList();
            _lnhom.Add(new NhomDV { IDNhom = -1, TenNhom = " Tất cả" });
            lupNhomDV.Properties.DataSource = _lnhom.OrderBy(p => p.TenNhom).ToList();
            lupNhomDV.EditValue = -1;
            if (qkho.Count() > 0)
            {
                KhoDuoc moi = new KhoDuoc();
                moi.Chon = true;
                moi.MaKP = 0;
                moi.TenKP = "Tất cả";
                _lKhoDuoc.Add(moi);
                foreach (var item in qkho)
                {
                    KhoDuoc moi1 = new KhoDuoc();
                    moi1.Chon = true;
                    moi1.MaKP = item.MaKP;
                    moi1.TenKP = item.TenKP;
                    _lKhoDuoc.Add(moi1);
                }
                grcKhoaphong.DataSource = _lKhoDuoc.ToList();
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            DateTime tungay = DungChung.Ham.NgayTu(date_TuNgay.DateTime);
            DateTime denngay = DungChung.Ham.NgayDen(date_DenNgay.DateTime);
            List<KhoDuoc> dskhochon = new List<KhoDuoc>();
            dskhochon = _lKhoDuoc.Where(p => p.Chon == true && p.MaKP > 0).ToList();
            int nhom = -1;
            if (lupNhomDV.EditValue != null)
                nhom = Convert.ToInt32(lupNhomDV.EditValue);
            int tnhom = -1;
            if (lupTieuNhom.EditValue != null)
                tnhom = Convert.ToInt32(lupTieuNhom.EditValue);
            #region select
            var qNhapd = (from nd in data.NhapDs.Where(p => p.PLoai == 1).Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay)
                          join ndct in data.NhapDcts on nd.IDNhap equals ndct.IDNhap
                          select new
                          {
                              nd.PLoai,
                              nd.IDNhap,
                              nd.KieuDon,
                              nd.NgayNhap,
                              nd.MaKP,
                              ndct.MaDV,
                              ndct.DonGia,
                              ndct.SoLuongN,
                              ndct.ThanhTienN
                          }).ToList();
            var qdv = (from dv in data.DichVus
                       join ndv in data.NhomDVs on dv.IDNhom equals ndv.IDNhom
                       join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                       select new
                       {
                           dv.MaTam,
                           ndv.IDNhom,
                           tn.IdTieuNhom,
                           dv.TenDV,
                           dv.DonVi,
                           dv.MaDV,
                       }).Where(p => nhom == -1 || p.IDNhom == nhom).Where(p => tnhom == -1 || p.IdTieuNhom == tnhom).ToList();
            var q1 = (from kp in dskhochon
                      join dt in qNhapd on kp.MaKP equals dt.MaKP
                      join dv in qdv on dt.MaDV equals dv.MaDV
                      group new { dt, dv } by new { dv.DonVi, dt.DonGia, dv.MaDV, dv.TenDV, dv.MaTam } into kq
                      select new
                      {
                          kq.Key.MaTam,
                          kq.Key.TenDV,
                          kq.Key.DonVi,
                          kq.Key.DonGia,
                          SLNhapHD = kq.Where(p => p.dt.KieuDon == 1).Sum(p => p.dt.SoLuongN), // nhập theo hóa đơn
                          TTNhapHD = kq.Where(p => p.dt.KieuDon == 1).Sum(p => p.dt.ThanhTienN),

                          SLNhapTra = kq.Where(p => p.dt.KieuDon == 2).Sum(p => p.dt.SoLuongN), // nhập trả dược
                          TTNhapTra = kq.Where(p => p.dt.KieuDon == 2).Sum(p => p.dt.ThanhTienN),

                          SLNhapCK = kq.Where(p => p.dt.KieuDon == 0).Sum(p => p.dt.SoLuongN), // nhập chuyển kho
                          TTNhapCK = kq.Where(p => p.dt.KieuDon == 0).Sum(p => p.dt.ThanhTienN),

                          SLNhapKhac = kq.Where(p => p.dt.KieuDon != 0 && p.dt.KieuDon != 1 && p.dt.KieuDon != 2).Sum(p => p.dt.SoLuongN), // nhập khác
                          TTNhapKhac = kq.Where(p => p.dt.KieuDon != 0 && p.dt.KieuDon != 1 && p.dt.KieuDon != 2).Sum(p => p.dt.ThanhTienN),

                          SLTongNhap = kq.Sum(p => p.dt.SoLuongN), // tổng nhập
                          TTTongNhap = kq.Sum(p => p.dt.ThanhTienN),
                      }).ToList();
            #endregion
            BaoCao.Rep_BC_CTNhapThuoc_VTYT_30009 rep = new BaoCao.Rep_BC_CTNhapThuoc_VTYT_30009();
            frmIn frm = new frmIn();
            rep.DataSource = q1.OrderBy(p => p.TenDV);
            rep.lblTuNgay.Text = "( Từ ngày " + tungay.ToString("dd/MM/yyyy") + " đến ngày " + denngay.ToString("dd/MM/yyyy") + " )";
            rep.BindingData();
            rep.CreateDocument();
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();
        }

        private void grvKhoaphong_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "colChon")
            {
                if (grvKhoaphong.GetFocusedRowCellValue("TenKP") != null)
                {
                    string Ten = grvKhoaphong.GetFocusedRowCellValue("TenKP").ToString();

                    if (Ten == "Tất cả")
                    {
                        if (_lKhoDuoc.First().Chon == true)
                        {
                            foreach (var a in _lKhoDuoc)
                            {
                                a.Chon = false;
                            }
                        }
                        else
                        {
                            foreach (var a in _lKhoDuoc)
                            {
                                a.Chon = true;
                            }
                        }
                        grcKhoaphong.DataSource = "";
                        grcKhoaphong.DataSource = _lKhoDuoc.ToList();
                    }
                }
            }
        }

        private void lupNhomDV_EditValueChanged(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            List<TieuNhomDV> _ltnhom = new List<TieuNhomDV>();
            int id = -1;
            if (lupNhomDV.EditValue != null)
                id = Convert.ToInt32(lupNhomDV.EditValue);
            _ltnhom = data.TieuNhomDVs.Where(p => p.Status == 1).ToList();
            if (id >= 0)
                _ltnhom = _ltnhom.Where(p => p.IDNhom == id).ToList();
            _ltnhom.Add(new TieuNhomDV { IdTieuNhom = -1, TenTN = " Tất cả" });
            lupTieuNhom.Properties.DataSource = _ltnhom.OrderBy(p => p.TenTN).ToList();
            lupTieuNhom.EditValue = -1;
        }
    }
}