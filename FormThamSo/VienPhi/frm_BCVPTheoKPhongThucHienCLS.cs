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
    public partial class frm_BCVPTheoKPhongThucHienCLS : DevExpress.XtraEditors.XtraForm
    {
        public frm_BCVPTheoKPhongThucHienCLS()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private class c_KPhong
        {
            private string TenKP;
            private int MaKP;
            private string PLoai;
            private string ChuyenKhoa;
            private bool Chon;
            public string tenkp
            { set { TenKP = value; } get { return TenKP; } }
            public int makp
            { set { MaKP = value; } get { return MaKP; } }
            public string ploai
            { set { PLoai = value; } get { return PLoai; } }
            public string chuyenkhoa
            { set { ChuyenKhoa = value; } get { return ChuyenKhoa; } }
            public bool chon
            { set { Chon = value; } get { return Chon; } }
        }
        List<c_KPhong> _Kphong = new List<c_KPhong>();
        List<KPhong> _lKPhong = new List<KPhong>();
        private void frm_BCVPTheoKPhongThucHienCLS_Load(object sender, EventArgs e)
        {
            lutungay.Focus();
            lutungay.DateTime = System.DateTime.Now;
            _Kphong.Clear();
            lupdenngay.DateTime = System.DateTime.Now;
            var kphong = (from kp in data.KPhongs
                          where (kp.PLoai == "Cận lâm sàng")
                          select new { kp.TenKP, kp.MaKP, kp.PLoai, kp.ChuyenKhoa }).ToList();
            if (kphong.Count > 0)
            {
                c_KPhong themmoi1 = new c_KPhong();
                themmoi1.tenkp = "Chọn tất cả";
                themmoi1.makp = 0;
                themmoi1.ploai = "";
                themmoi1.chuyenkhoa = "";
                themmoi1.chon = true;
                _Kphong.Add(themmoi1);
                foreach (var a in kphong)
                {
                    c_KPhong themmoi = new c_KPhong();
                    themmoi.tenkp = a.TenKP;
                    themmoi.makp = a.MaKP;
                    themmoi.ploai = a.PLoai;
                    themmoi.chuyenkhoa = a.ChuyenKhoa;
                    themmoi.chon = true;
                    _Kphong.Add(themmoi);
                }
                cklKP.DataSource = _Kphong.ToList();
            }
            cklKP.CheckAll();
            var _ltndv = data.TieuNhomDVs.Where(p => p.IDNhom == 1 || p.IDNhom == 2 || p.IDNhom == 3).Select(p => p.TenRG).ToList();
            lupnhomdv.Properties.DataSource = _ltndv;
        }

        private void cklKP_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            if (Convert.ToInt32(cklKP.SelectedValue) == 0)
            {
                if (cklKP.GetItemChecked(cklKP.SelectedIndex))
                {
                    cklKP.CheckAll();

                }
                else
                {
                    cklKP.UnCheckAll();
                }
            }

            for (int i = 0; i < cklKP.ItemCount; i++)
            {
                int makp = Convert.ToInt32(cklKP.GetItemValue(i));
                if (cklKP.GetItemChecked(i))
                {

                    foreach (var item in _Kphong)
                    {
                        if (item.makp == makp && item.makp != 0)
                        {
                            item.chon = true;
                            //break;
                        }
                    }
                }
                else
                {
                    foreach (var item in _Kphong)
                    {
                        if (item.makp == makp || item.makp == 0)
                        {
                            item.chon = false;
                            // break;
                        }
                    }
                }
            }
        }

        private void btnInBC_Click(object sender, EventArgs e)
        {
            DateTime _ngayTu = DungChung.Ham.NgayTu(lutungay.DateTime);
            DateTime _ngayDen = DungChung.Ham.NgayDen(lupdenngay.DateTime);

            for (int i = 0; i < cklKP.ItemCount; i++)
            {
                if (cklKP.GetItemChecked(i))
                {
                    int makp = Convert.ToInt32(cklKP.GetItemValue(i));
                    foreach (var item in _Kphong)
                    {
                        if (item.makp == makp && item.makp != 0)
                        {
                            item.chon = true;
                            break;
                        }
                    }
                }
            }
            
            string tieunhom = "";
            if (lupnhomdv.EditValue != null)
            {
                tieunhom = lupnhomdv.EditValue.ToString();

                string dtuong = "";
                if (cbodtuong.EditValue != null)
                {
                    dtuong = cbodtuong.EditValue.ToString();
                }
                var kp11 = _Kphong.Where(p => p.chon == true).ToList();
                var _bn = data.BenhNhans.Where(p => cbo_NoiTru.SelectedIndex == 2 ? true : p.NoiTru == cbo_NoiTru.SelectedIndex).Where(p => dtuong == "Tất cả" ? true : p.DTuong == dtuong).ToList();
                var _ldv = (from dv in data.DichVus
                            join tn in data.TieuNhomDVs.Where(p => p.TenRG == tieunhom) on dv.IdTieuNhom equals tn.IdTieuNhom
                            select new { dv.MaDV, dv.TenDV }).ToList();
                var _lcls = (from cls in data.CLS.Where(p => p.NgayTH >= _ngayTu && p.NgayTH <= _ngayDen).Where(p => p.Status == 1)
                             join cd in data.ChiDinhs.Where(p => ckxhh.Checked ? p.XHH == 1 : p.XHH == 0).Where(p => ckctheoyc.Checked ? p.LoaiDV == 1 : p.LoaiDV == 0) on cls.IdCLS equals cd.IdCLS
                             select new { cls.MaBNhan, cls.MaKPth, cls.NgayTH, cd.DonGia, cd.MaDV }).ToList();
                var _lkq1 = (from a in _lcls
                             join b in _bn on a.MaBNhan equals b.MaBNhan
                             select new { a.MaBNhan, a.MaKPth, a.NgayTH, a.DonGia, a.MaDV, b.TenBNhan, b.SThe }).ToList();
                var _kq = (from a in _lkq1
                           join d in _ldv on a.MaDV equals d.MaDV
                           join kp in kp11 on a.MaKPth equals kp.makp
                           group new { a, d } by new { a.MaBNhan, a.TenBNhan, a.SThe, a.MaDV, d.TenDV, a.NgayTH, a.DonGia } into kq
                           select new
                           {
                               kq.Key.MaBNhan,
                               kq.Key.TenBNhan,
                               kq.Key.SThe,
                               kq.Key.TenDV,
                               kq.Key.MaDV,
                               kq.Key.DonGia,
                               kq.Key.NgayTH,
                           }).ToList().OrderBy(p => p.TenBNhan);
                if (_kq.Count() > 0)
                {
                    frmIn frm1 = new frmIn();
                    BaoCao.rep_BCVPTHeoKphongCLS rep1 = new BaoCao.rep_BCVPTHeoKphongCLS();
                    rep1.TENCQ.Value = DungChung.Bien.TenCQ.ToUpper();
                    rep1.CQCQ.Value = DungChung.Bien.NguoiLapBieu;
                    rep1.TENTN.Value = tieunhom.ToUpper();
                    if (kp11.Count() == 1)
                    {
                        rep1.Khoa.Value = kp11.First().tenkp.ToString();
                    }
                    else
                    {
                        rep1.Khoa.Value = "";
                    }
                    rep1.NGAYTHANG.Value = "Từ ngày " + _ngayTu.ToShortDateString() + " đến ngày " + _ngayDen.ToShortDateString();
                    rep1.CQ.Value = DungChung.Bien.TenCQCQ.ToUpper();
                    rep1.DataSource = _kq;
                    rep1.bindingdata();
                    rep1.CreateDocument();
                    frm1.prcIN.PrintingSystem = rep1.PrintingSystem;
                    frm1.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Không có dữ liệu !", "Thông báo", MessageBoxButtons.OK);
                    lutungay.Focus();
                }
            }
            else
            {
                MessageBox.Show("Bạn chưa chọn nhóm Dịch vụ", "Thông báo", MessageBoxButtons.OK);
                lupnhomdv.Focus();
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}