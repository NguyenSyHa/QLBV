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
    public partial class frm_BCNXTNew_30002 : DevExpress.XtraEditors.XtraForm
    {
        public frm_BCNXTNew_30002()
        {
            InitializeComponent();
        }

        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        QLBV_Database.QLBVEntities data;
        List<KPhong> _lkp = new List<KPhong>();
        private void frm_BCNXTNew_30002_Load(object sender, EventArgs e)
        {
            _lkp.Clear();
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            detungay.DateTime = DateTime.Now;
            dedenngay.DateTime = DateTime.Now;

            _lkp = data.KPhongs.Where(p => p.Status == 1).Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.KhoaDuoc).ToList();
            lupkhotong.Properties.DataSource = _lkp;
            cklKP.DataSource = _lkp;
            var ld = (from a in data.NhomDVs.Where(p => p.Status == 1) select new { a.IDNhom, a.TenNhom }).ToList();
            ld.Insert(0, new { IDNhom = 0, TenNhom = "Tất cả" });
            lupLoaiDuoc.Properties.DataSource = ld.ToList();
            lupLoaiDuoc.EditValue = 0;
        }

        private void lupkhotong_EditValueChanged(object sender, EventArgs e)
        {
            //_lkp.Clear();
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            int makp = 0;
            if(lupkhotong.EditValue!=null)
            {
                makp = Convert.ToInt32(lupkhotong.EditValue);
                _lkp = data.KPhongs.Where(p => p.Status == 1).Where(p => p.MaKP != makp).Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.KhoaDuoc).ToList();
                cklKP.DataSource = _lkp;
            }
        }

        private void btntaobc_Click(object sender, EventArgs e)
        {
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            int makhotong = 0, _ld = -1;
            if (lupkhotong.EditValue != null)
                makhotong = Convert.ToInt32(lupkhotong.EditValue);
            else
            {
                MessageBox.Show("Chưa chọn kho tổng");
                return;
            }
            if (lupLoaiDuoc.EditValue != null)
                _ld = lupLoaiDuoc.EditValue == null ? 0 : Convert.ToInt32(lupLoaiDuoc.EditValue);
            int[] khoxa = new int[50];
            for (int i = 0; i < 50; i++)
                khoxa[i] = -1;
            for (int i = 0; i < cklKP.ItemCount; i++)
            {
                if (cklKP.GetItemCheckState(i) == CheckState.Checked)
                    khoxa[i] = Convert.ToInt32(cklKP.GetItemValue(i));
            }
            DateTime _Tungay = DungChung.Ham.NgayTu(detungay.DateTime);
            DateTime _Denngay = DungChung.Ham.NgayDen(dedenngay.DateTime);
            var _lnhapd = (from nd in data.NhapDs
                           join ndct in data.NhapDcts on nd.IDNhap equals ndct.IDNhap
                           select new { nd.IDNhap, nd.PLoai, nd.KieuDon, nd.MaKP, nd.MaKPnx, nd.Status, ndct.IDNhapct, ndct.MaDV, ndct.SoLuongN, ndct.SoLuongX, ndct.ThanhTienN, ndct.ThanhTienX, ndct.DonGia, nd.NgayNhap }).ToList();

            var _ldichvu = data.DichVus.Where(p => _ld == 0 || p.IDNhom == _ld).Where(p => p.PLoai == 1).ToList();

            var _ketqua = (from dv in _ldichvu
                           join nd in _lnhapd on dv.MaDV equals nd.MaDV
                           group new { nd, dv } by new { nd.MaDV, dv.TenDV, dv.DonVi, nd.DonGia } into kq
                           select new
                           {
                               kq.Key.MaDV,
                               kq.Key.TenDV,
                               kq.Key.DonGia,
                               kq.Key.DonVi,
                               TonDauKySL = kq.Where(p=>p.nd.NgayNhap<_Tungay).Where(p=>p.nd.PLoai==1&&p.nd.MaKP==makhotong).Sum(p=>p.nd.SoLuongN) - kq.Where(p=>p.nd.NgayNhap<_Tungay).Where(p=>p.nd.PLoai==2&&p.nd.MaKP==makhotong).Sum(p=>p.nd.SoLuongX) +kq.Where(p=>p.nd.NgayNhap<_Tungay).Where(p=>p.nd.PLoai==1&& khoxa.Contains(p.nd.MaKP??0)).Sum(p=>p.nd.SoLuongN)-kq.Where(p=>p.nd.NgayNhap<_Tungay).Where(p=>p.nd.PLoai==2&& khoxa.Contains(p.nd.MaKP??0)).Sum(p=>p.nd.SoLuongX),

                               TonDauKyTT = kq.Where(p => p.nd.NgayNhap < _Tungay).Where(p => p.nd.PLoai == 1 && p.nd.MaKP == makhotong).Sum(p => p.nd.ThanhTienN) - kq.Where(p => p.nd.NgayNhap < _Tungay).Where(p => p.nd.PLoai == 2 && p.nd.MaKP == makhotong).Sum(p => p.nd.ThanhTienX) + kq.Where(p => p.nd.NgayNhap < _Tungay).Where(p => p.nd.PLoai == 1 && khoxa.Contains(p.nd.MaKP ?? 0)).Sum(p => p.nd.ThanhTienN) - kq.Where(p => p.nd.NgayNhap < _Tungay).Where(p => p.nd.PLoai == 2 && khoxa.Contains(p.nd.MaKP ?? 0)).Sum(p => p.nd.ThanhTienX),

                               NhapHDKTongSL = kq.Where(p => p.nd.NgayNhap >= _Tungay && p.nd.NgayNhap <= _Denngay).Where(p => p.nd.MaKP == makhotong).Where(p => p.nd.PLoai == 1 && p.nd.KieuDon == 1).Sum(p => p.nd.SoLuongN),

                               NhapHDKTongTT = kq.Where(p => p.nd.NgayNhap >= _Tungay && p.nd.NgayNhap <= _Denngay).Where(p => p.nd.MaKP == makhotong).Where(p => p.nd.PLoai == 1 && p.nd.KieuDon == 1).Sum(p => p.nd.ThanhTienN),

                               NhapCKVeKTongSL = kq.Where(p => p.nd.NgayNhap >= _Tungay && p.nd.NgayNhap <= _Denngay).Where(p => p.nd.MaKP == makhotong).Where(p => p.nd.PLoai == 1 && p.nd.KieuDon == 0).Sum(p => p.nd.SoLuongN),

                               NhapCKVeKTongTT = kq.Where(p => p.nd.NgayNhap >= _Tungay && p.nd.NgayNhap <= _Denngay).Where(p => p.nd.MaKP == makhotong).Where(p => p.nd.PLoai == 1 && p.nd.KieuDon == 0).Sum(p => p.nd.ThanhTienN),

                               XuatSDSL = kq.Where(p => p.nd.NgayNhap >= _Tungay && p.nd.NgayNhap <= _Denngay).Where(p => p.nd.MaKP == makhotong || khoxa.Contains(p.nd.MaKP ?? 0)).Where(p => p.nd.PLoai == 5).Sum(p => p.nd.SoLuongX),
                               XuatSDTT = kq.Where(p => p.nd.NgayNhap >= _Tungay && p.nd.NgayNhap <= _Denngay).Where(p => p.nd.MaKP == makhotong || khoxa.Contains(p.nd.MaKP ?? 0)).Where(p => p.nd.PLoai == 5).Sum(p => p.nd.ThanhTienX),

                               XuatChuyenKhoSL = kq.Where(p => p.nd.NgayNhap >= _Tungay && p.nd.NgayNhap <= _Denngay).Where(p => p.nd.PLoai == 2 && p.nd.KieuDon == 2).Where(p => p.nd.MaKP == makhotong && !khoxa.Contains(p.nd.MaKPnx ?? 0)).Sum(p => p.nd.SoLuongX) + kq.Where(p => p.nd.NgayNhap >= _Tungay && p.nd.NgayNhap <= _Denngay).Where(p => p.nd.PLoai == 2 && p.nd.KieuDon == 2).Where(p => khoxa.Contains(p.nd.MaKP ?? 0)).Sum(p => p.nd.SoLuongX),
                               XuatChuyenKhoTT = kq.Where(p => p.nd.NgayNhap >= _Tungay && p.nd.NgayNhap <= _Denngay).Where(p => p.nd.PLoai == 2 && p.nd.KieuDon == 2).Where(p => p.nd.MaKP == makhotong && !khoxa.Contains(p.nd.MaKPnx ?? 0)).Sum(p => p.nd.ThanhTienX) + kq.Where(p => p.nd.NgayNhap >= _Tungay && p.nd.NgayNhap <= _Denngay).Where(p => p.nd.PLoai == 2 && p.nd.KieuDon == 2).Where(p => khoxa.Contains(p.nd.MaKP ?? 0)).Sum(p => p.nd.ThanhTienX),

                               TonCuoiKySL = kq.Where(p => p.nd.NgayNhap < _Denngay).Where(p => p.nd.PLoai == 1 && p.nd.MaKP == makhotong).Sum(p => p.nd.SoLuongN) - kq.Where(p => p.nd.NgayNhap < _Denngay).Where(p => p.nd.PLoai == 2 && p.nd.MaKP == makhotong).Sum(p => p.nd.SoLuongX) + kq.Where(p => p.nd.NgayNhap < _Denngay).Where(p => p.nd.PLoai == 1 && khoxa.Contains(p.nd.MaKP ?? 0)).Sum(p => p.nd.SoLuongN) - kq.Where(p => p.nd.NgayNhap < _Denngay).Where(p => p.nd.PLoai == 2 && khoxa.Contains(p.nd.MaKP ?? 0)).Sum(p => p.nd.SoLuongX),

                               TonCuoiKyTT = kq.Where(p => p.nd.NgayNhap < _Denngay).Where(p => p.nd.PLoai == 1 && p.nd.MaKP == makhotong).Sum(p => p.nd.ThanhTienN) - kq.Where(p => p.nd.NgayNhap < _Denngay).Where(p => p.nd.PLoai == 2 && p.nd.MaKP == makhotong).Sum(p => p.nd.ThanhTienX) + kq.Where(p => p.nd.NgayNhap < _Denngay).Where(p => p.nd.PLoai == 1 && khoxa.Contains(p.nd.MaKP ?? 0)).Sum(p => p.nd.ThanhTienN) - kq.Where(p => p.nd.NgayNhap < _Denngay).Where(p => p.nd.PLoai == 2 && khoxa.Contains(p.nd.MaKP ?? 0)).Sum(p => p.nd.ThanhTienX),
                           }).OrderBy(p => p.TenDV).ToList();

            frmIn frm = new frmIn();
            BaoCao.Rep_BCNXTNew_30002 rep = new BaoCao.Rep_BCNXTNew_30002();

            rep.TENCQ.Value = DungChung.Bien.TenCQ.ToUpper();
            rep.TENKHO.Value = lupkhotong.Text.ToUpper();
            rep.TIEUDE.Value = "BÁO CÁO NHẬP - XUẤT - TỒN TỔNG " + lupkhotong.Text.ToUpper();
            rep.NGAYTHANG.Value = "Từ ngày " + _Tungay.ToShortDateString() + " đến ngày " + _Denngay.ToShortDateString();
            var tenkho = (from kp in khoxa
                          join p in data.KPhongs on kp equals p.MaKP
                          select new { p.TenKP }).ToList();
            string tenkhonew = "";
            foreach (var item in tenkho)
            {
                tenkhonew += item.TenKP + ";";
            }
            rep.KHO.Value = tenkhonew;
            rep.DataSource = _ketqua;
            rep.databinding();
            rep.CreateDocument();
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();
        }

        private void btnthoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}