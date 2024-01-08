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
    public partial class frm_THXuatDuoc : DevExpress.XtraEditors.XtraForm

    {
        public frm_THXuatDuoc()
        {
            InitializeComponent();
        }
        private bool KTtaoBcNXT()
        {
            if (dateTuNgay.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn ngày bắt đầu in báo cáo");
                dateTuNgay.Focus();
                return false;
            }
            if (dateDenNgay.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn đến ngày kết thúc in báo cáo");
                dateDenNgay.Focus();
                return false;
            }

            else return true;
        }
        private void btnInBC_Click(object sender, EventArgs e)
        {
            DateTime tungay = System.DateTime.Now.Date;
            DateTime denngay = System.DateTime.Now.Date;
            int _kho = 0;
            if (lupKho.EditValue != null)
                _kho = Convert.ToInt32(lupKho.EditValue);
            string _nhacc = "";
            if (lupNhaCC.EditValue != null)
                _nhacc = lupNhaCC.EditValue.ToString();
            tungay = DungChung.Ham.NgayTu(dateTuNgay.DateTime);//dateTuNgay.DateTime;
            denngay = DungChung.Ham.NgayDen(dateDenNgay.DateTime);//dateDenNgay.DateTime;
            List<KPhong> _lkp = data.KPhongs.ToList();
            _lkp.Insert(0, new KPhong { MaKP = 0, TenKP = "" });
            if (KTtaoBcNXT())
            {
                List<PPXD> _lPPX = new List<PPXD>();
                for (int i = 0; i < grvPPXuat.RowCount; i++)
                {

                    if (grvPPXuat.GetRowCellValue(i, "chon") != null && (grvPPXuat.GetRowCellValue(i, "chon").ToString().ToLower() == "true"))
                    {

                        if (grvPPXuat.GetRowCellValue(i, colKieuDon) != null)
                        {
                            _lPPX.Add(new PPXD { chon = true, mappxd = Convert.ToInt32(grvPPXuat.GetRowCellValue(i, colKieuDon)) });
                        }
                    }
                }
                var q2 = (from nd in data.NhapDs.Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Where(p => p.MaKP == (_kho)).Where(p => p.PLoai == 2)
                          join ndct in data.NhapDcts on nd.IDNhap equals ndct.IDNhap
                          join dv in data.DichVus.Where(p => _nhacc == "" || p.MaCC == _nhacc) on ndct.MaDV equals dv.MaDV
                          select new { nd, ndct, dv.TenDV, dv.DonVi, MaKPnx = (nd.MaKPnx != null && nd.MaKPnx != 0 && nd.MaKPnx != -1) ? nd.MaKPnx : 0 }).ToList();
               
                if (chkHienTong.Checked)
                {

                    data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                    frmIn frm = new frmIn();
                    BaoCao.rep_THXuatDuoc rep = new BaoCao.rep_THXuatDuoc(chkChiTiet.Checked, chkHienTong.Checked);
                    rep.TieuDe.Value = "BÁO CÁO XUẤT THUỐC, VẬT TƯ, DỤNG CỤ";

                    rep.Ngay.Value = "Từ ngày:" + tungay.ToString().Substring(0, 10) + " đến ngày: " + denngay.ToString().Substring(0, 10);
            
                    var q = (from pp in _lPPX.Where(p => p.chon == true)
                             join nd in q2
                              on pp.mappxd equals nd.nd.KieuDon
                              join kp in _lkp on nd.nd.MaKPnx equals kp.MaKP
                             group nd by new { nd.TenDV, nd.DonVi, nd.ndct.DonGia, nd.ndct.MaDV, nd.nd.MaKPnx, kp.TenKP } into kq
                             select new { kq.Key.TenDV, kq.Key.MaDV, kq.Key.MaKPnx, kq.Key.TenKP, kq.Key.DonVi, kq.Key.DonGia, SoLuongX = kq.Where(p => p.nd.PLoai == 2 || p.nd.PLoai == 3).Sum(p => p.ndct.SoLuongX), ThanhTienX = kq.Where(p => p.nd.PLoai == 2 || p.nd.PLoai == 3).Sum(p => p.ndct.ThanhTienX) }).ToList();
                    rep.DataSource = q.ToList();
                    rep.BindingData();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                }
                else
                {
                    data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                    frmIn frm = new frmIn();
                    BaoCao.rep_THXuatDuoc rep = new BaoCao.rep_THXuatDuoc(chkChiTiet.Checked);
                    rep.TieuDe.Value = "BẢNG KÊ HÀNG XUẤT";
                    rep.Ngay.Value = "Từ ngày:" + tungay.ToString().Substring(0, 10) + " đến ngày: " + denngay.ToString().Substring(0, 10);
                    var q = (from pp in _lPPX.Where(p => p.chon == true)
                             join nd in q2
                               on pp.mappxd equals nd.nd.KieuDon
                             join kp in _lkp on nd.nd.MaKPnx equals kp.MaKP
                             group nd by new { nd.TenDV, nd.DonVi, nd.ndct.DonGia, nd.ndct.MaDV, nd.nd.SoCT, nd.nd.MaKPnx, nd.nd.NgayNhap, kp.TenKP, nd.nd.IDNhap } into kq
                             select new { kq.Key.TenDV, kq.Key.MaDV, kq.Key.IDNhap, kq.Key.SoCT, kq.Key.TenKP, kq.Key.NgayNhap, kq.Key.DonVi, kq.Key.DonGia, SoLuongX = kq.Where(p => p.nd.PLoai == 2 || p.nd.PLoai == 3).Sum(p => p.ndct.SoLuongX), ThanhTienX = kq.Where(p => p.nd.PLoai == 2 || p.nd.PLoai == 3).Sum(p => p.ndct.ThanhTienX) }).ToList();
                    rep.DataSource = DungChung.Bien.MaBV == "12122" ? q.OrderBy(p => p.IDNhap).ThenBy(p => p.SoCT).ToList() : q.OrderBy(p => p.NgayNhap).ToList();
                    rep.BindingData();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                }

            } //
        }
        List<PPXD> _PPXD = new List<PPXD>();
        List<NCC> _lNCC = new List<NCC>();
        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        QLBV_Database.QLBVEntities data;
        private void frmTsBcNXTXuat_Load(object sender, EventArgs e)
        {

            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var q = from TK in data.KPhongs.Where(p => p.PLoai == ("Khoa dược")) select new { TK.TenKP, TK.MaKP };
            lupKho.Properties.DataSource = q.ToList();
            var qcc = (from CC in data.NhaCCs select new { CC.MaCC, CC.TenCC }).OrderBy(p => p.TenCC).ToList();
            if (qcc.Count > 0)
            {
                NCC ncc1 = new NCC();
                ncc1.MaCC = "";
                ncc1.TenCC = "Chọn tất cả";
                _lNCC.Add(ncc1);
                foreach (var item in qcc)
                {
                    NCC ncc = new NCC();
                    ncc.MaCC = item.MaCC;
                    ncc.TenCC = item.TenCC;
                    _lNCC.Add(ncc);
                }
            }
            lupNhaCC.Properties.DataSource = _lNCC.ToList();
            lupNhaCC.EditValue = "";
            dateTuNgay.DateTime = System.DateTime.Now;
            dateDenNgay.DateTime = System.DateTime.Now;
            addDataPPXuat();
        }
        private class NCC
        {
            private string maCC;

            public string MaCC
            {
                get { return maCC; }
                set { maCC = value; }
            }
            private string tenCC;

            public string TenCC
            {
                get { return tenCC; }
                set { tenCC = value; }
            }
        }
        private class PPXD
        {
            private int MaPPXD;
            private string TenPPXD;
            private bool Chon;
            public int mappxd { set { MaPPXD = value; } get { return MaPPXD; } }
            public string tenppxd { set { TenPPXD = value; } get { return TenPPXD; } }
            public bool chon { set { Chon = value; } get { return Chon; } }
        }
        public void addDataPPXuat()
        {
       
            List<DungChung.Bien.c_PhanLoaiXuat> list = DungChung.Bien.c_PhanLoaiXuat._setPhanLoaiXuat();
            foreach (var a in list)
            {
                _PPXD.Add(new PPXD { mappxd = a.Id, tenppxd = a.PhanLoai, chon = a.Check });
            }
            grcPPXuat.DataSource = _PPXD.ToList();
        }

    }
}