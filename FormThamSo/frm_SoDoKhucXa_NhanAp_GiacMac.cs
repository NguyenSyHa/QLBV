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
    public partial class frm_SoDoKhucXa_NhanAp_GiacMac : DevExpress.XtraEditors.XtraForm
    {
        public frm_SoDoKhucXa_NhanAp_GiacMac()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities _data;
        private void labelControl2_Click(object sender, EventArgs e)
        {

        }

        private void labelControl4_Click(object sender, EventArgs e)
        {

        }
        List<DichVu> _ldvall = new List<DichVu>();
        private void frm_SoDoKhucXa_NhanAp_GiacMac_Load(object sender, EventArgs e)
        {
            deTuNgay.DateTime = DateTime.Now;
            deDenNgay.DateTime = DateTime.Now;
            _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var dskp = _data.KPhongs.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.PhongKham || p.PLoai == DungChung.Bien.st_PhanLoaiKP.LamSang).ToList();
            clb_KhoaPhong.DataSource = dskp.OrderBy(p => p.TenKP).ToList();
            clb_KhoaPhong.UnCheckAll();
            _ldvall = _data.DichVus.Where(p => p.Status == 1).Where(p => p.TenDV.ToLower().Contains("đo khúc xạ máy") || p.TenDV.ToLower().Contains("đo nhãn áp") || p.TenDV.ToLower().Contains("đo khúc xạ giác mạc javal")).ToList();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnTaoSo_Click(object sender, EventArgs e)
        {
            if (clb_KhoaPhong.CheckedItemsCount <= 0)
            {
                MessageBox.Show("Chưa chọn khoa phòng");
                return;
            }
            int _Loai = rgChonSo.SelectedIndex;
            List<int> _lDichVu = new List<int>();
            List<BaoCaocs> _lkq = new List<BaoCaocs>();
            for (int i = 0; i < ckl_DichVu.ItemCount; i++)
            {
                if (ckl_DichVu.GetItemChecked(i))
                    _lDichVu.Add(Convert.ToInt32(ckl_DichVu.GetItemValue(i)));
            }
            DateTime _tungay = DungChung.Ham.NgayTu(deTuNgay.DateTime);
            DateTime _denngay = DungChung.Ham.NgayDen(deDenNgay.DateTime);
            List<int> checkMaKPhong = new List<int>();
            List<KPhong> checkKPhong = new List<KPhong>();
            foreach (var item in clb_KhoaPhong.CheckedItems)
            {
                var row = (KPhong)item;
                if (row != null)
                {
                    checkMaKPhong.Add(row.MaKP);
                    checkKPhong.Add(row);
                }
            }

            var _lcb = _data.CanBoes.ToList();
            var _lkp = _data.KPhongs.ToList();
            var q1 = (from cls in _data.CLS.Where(p => checkMaKPhong.Contains(p.MaKP ?? 0)).Where(p => p.Status == 1).Where(p => p.NgayTH >= _tungay && p.NgayTH <= _denngay)
                      join cd in _data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                      join clsct in _data.CLScts on cd.IDCD equals clsct.IDCD
                      join dv in _data.DichVus on cd.MaDV equals dv.MaDV
                      join bn in _data.BenhNhans on cls.MaBNhan equals bn.MaBNhan
                      select new { cls.MaBNhan, cls.MaKP, cls.MaCBth, cls.NgayTH, bn.TenBNhan, bn.Tuoi, bn.GTinh, bn.DTuong, cls.ChanDoan, cd.MaDV, clsct.KetQua, bn.DChi, dv.TenDV }).ToList();

            var q2 = (from a in q1
                      join b in _lDichVu on a.MaDV equals b
                      join cb in _lcb on a.MaCBth equals cb.MaCB
                      join kp in _lkp on a.MaKP equals kp.MaKP
                      select new BaoCaocs
                      {
                          TenBNhan = a.TenBNhan,
                          Nam = a.GTinh == 1 ? a.Tuoi : null,
                          Nu = a.GTinh == 1 ? null : a.Tuoi,
                          DChi = a.DChi,
                          BHYT = a.DTuong == "BHYT" ? "x" : "",
                          Khac = a.DTuong == "BHYT" ? "" : "x",
                          ChanDoan = a.ChanDoan,
                          TenDV = a.TenDV,
                          KetQua = a.KetQua,
                          TenCB = cb.TenCB,
                          NgayTH = Convert.ToDateTime(a.NgayTH),
                          TenKP = kp.TenKP
                      }).OrderBy(p => p.NgayTH).ToList();
            _lkq.AddRange(q2);
            if (ckcDVkoCD.Checked)
            {
                var q3 = (from dt in _data.DThuocs.Where(p => p.PLDV == 2)
                          join dtct in _data.DThuoccts.Where(p => p.NgayNhap >= _tungay && p.NgayNhap <= _denngay).Where(p => p.IDCD == null || p.IDCD < 0).Where(p => checkMaKPhong.Contains(p.MaKP ?? 0)) on dt.IDDon equals dtct.IDDon
                          join dv in _data.DichVus on dtct.MaDV equals dv.MaDV
                          join bn in _data.BenhNhans on dt.MaBNhan equals bn.MaBNhan
                          join bnkb in _data.BNKBs on dt.MaBNhan equals bnkb.MaBNhan
                          where dt.MaKP == bnkb.MaKP
                          select new { dt.MaBNhan, dt.MaKP, dt.MaCB, dtct.NgayNhap, bn.TenBNhan, bn.Tuoi, bn.GTinh, bn.DTuong, bnkb.ChanDoan, bnkb.BenhKhac, dtct.MaDV, bn.DChi, dv.TenDV }).ToList();
                var q4 = (from a in q3
                          join b in _lDichVu on a.MaDV equals b
                          join cb in _lcb on a.MaCB equals cb.MaCB
                          join kp in _lkp on a.MaKP equals kp.MaKP
                          select new BaoCaocs
                          {
                              TenBNhan = a.TenBNhan,
                              Nam = a.GTinh == 1 ? a.Tuoi : null,
                              Nu = a.GTinh == 1 ? null : a.Tuoi,
                              DChi = a.DChi,
                              BHYT = a.DTuong == "BHYT" ? "x" : "",
                              Khac = a.DTuong == "BHYT" ? "" : "x",
                              ChanDoan = DungChung.Ham.FreshString(a.ChanDoan + ";" + a.BenhKhac),
                              TenDV = a.TenDV,
                              KetQua = "",
                              TenCB = cb.TenCB,
                              NgayTH = Convert.ToDateTime(a.NgayNhap),
                              TenKP = kp.TenKP
                          }).OrderBy(p => p.NgayTH).ToList();
                _lkq.AddRange(q4);
            }
            if (_lkq.Count() > 0)
            {
                frmIn frm = new frmIn();
                BaoCao.Rep_SoDoKhucXa_NhanAp_GiacMac rep = new BaoCao.Rep_SoDoKhucXa_NhanAp_GiacMac();
                rep.SoYTe.Value = DungChung.Bien.TenCQCQ;
                rep.TieuDe.Value = _Loai == 0 ? "SỔ ĐO KHÚC XẠ MÁY" : (_Loai == 1 ? "SỔ ĐO NHÃN ÁP" : "SỔ ĐO KHÚC XẠ\nGIÁC MẠC JAVAL");
                rep.TenCQ.Value = DungChung.Bien.TenCQ;
                rep.KPhong.Value = string.Join(", ", checkKPhong.Select(o => o.TenKP));
                rep.YeuCau.Value = _Loai == 0 ? "Yêu cầu(đo khúc xạ máy)" : (_Loai == 1 ? "Yêu cầu(đo nhãn áp)" : "Yêu cầu(đo khúc xạ giác mạc Javal)");
                rep.KetQua.Value = _Loai == 1 ? "Kết quả mmHg" : "Kết quả";
                rep.NgaThang.Value = "Từ ngày " + _tungay.ToShortDateString() + " đến ngày " + _denngay.ToShortDateString();
                rep.DataSource = _lkq.ToList();
                rep.DataBinDing();
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Không có dữ liệu");
            }
        }
        private class BaoCaocs
        {
            public string TenBNhan { get; set; }
            public int? Nam { get; set; }
            public int? Nu { get; set; }
            public string DChi { get; set; }
            public string BHYT { get; set; }
            public string Khac { get; set; }
            public string ChanDoan { get; set; }
            public string TenDV { get; set; }
            public string KetQua { get; set; }
            public string TenCB { get; set; }
            public DateTime NgayTH { get; set; }
            public string TenKP { get; set; }
        }
        List<DichVu> _ldvchon = new List<DichVu>();
        private void rgChonSo_SelectedIndexChanged(object sender, EventArgs e)
        {
            int LoaiSo = rgChonSo.SelectedIndex;
            switch (LoaiSo)
            {
                case 0:
                    _ldvchon = _ldvall.Where(p => p.TenDV.ToLower().Contains("đo khúc xạ máy")).ToList();
                    ckl_DichVu.DisplayMember = "TenDV";
                    ckl_DichVu.ValueMember = "MaDV";
                    ckl_DichVu.DataSource = _ldvchon;
                    ckl_DichVu.CheckAll();
                    break;
                case 1:
                    _ldvchon = _ldvall.Where(p => p.TenDV.ToLower().Contains("đo nhãn áp")).ToList();
                    ckl_DichVu.DisplayMember = "TenDV";
                    ckl_DichVu.ValueMember = "MaDV";
                    ckl_DichVu.DataSource = _ldvchon;
                    ckl_DichVu.CheckAll();
                    break;
                case 2:
                    _ldvchon = _ldvall.Where(p => p.TenDV.ToLower().Contains("đo khúc xạ giác mạc javal")).ToList();
                    ckl_DichVu.DisplayMember = "TenDV";
                    ckl_DichVu.ValueMember = "MaDV";
                    ckl_DichVu.DataSource = _ldvchon;
                    ckl_DichVu.CheckAll();
                    break;
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (linkLabel1.Text == "Chọn tất cả")
            {
                clb_KhoaPhong.CheckAll();
                linkLabel1.Text = "Bỏ chọn tất cả";
            }
            else
            {
                clb_KhoaPhong.UnCheckAll();
                linkLabel1.Text = "Chọn tất cả";
            }

        }
    }
}