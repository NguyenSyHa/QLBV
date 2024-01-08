using System;
using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;

namespace QLBV.TraCuu
{
    public partial class us_TCThuocKD : DevExpress.XtraEditors.XtraUserControl
    {
        int makho_nxt = 0;
        int madv_nxt = 0;
        double dongia_nxt = 0;
        public us_TCThuocKD()
        {
            InitializeComponent();
        }
        public us_TCThuocKD(int makho, int madv, double dongia)
        {
            InitializeComponent();
            makho_nxt = makho;
            madv_nxt = madv;
            dongia_nxt = dongia;
        }
        QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        DateTime _dttu = DungChung.Ham.NgayTu(System.DateTime.Now);
        DateTime _dtden = DungChung.Ham.NgayDen(System.DateTime.Now);
        class ctrangThai
        {
            string trangThai;

            public string TrangThai
            {
                get { return trangThai; }
                set { trangThai = value; }
            }
            int status;

            public int Status
            {
                get { return status; }
                set { status = value; }
            }
        }
        List<ctrangThai> _lTrangThai = new List<ctrangThai>();
        private void us_TCThuocKD_Load(object sender, EventArgs e)
        {
            dtTimTuNgay.DateTime = System.DateTime.Now.AddYears(-1);
            dtTimDenNgay.DateTime = System.DateTime.Now;
            var kp = _data.KPhongs.Where(p => p.PLoai.Contains("Khoa dược") || p.PLoai.Contains("Tủ trực")).ToList();
            lupKhoXuat.Properties.DataSource = kp.ToList();
            lupKPhongKe.DataSource = _data.KPhongs.ToList();
            var kphong = _data.KPhongs.Where(p => p.PLoai == "Lâm sàng" || p.PLoai == "Phòng khám" || p.PLoai == DungChung.Bien.st_PhanLoaiKP.CanLamSang || p.PLoai == DungChung.Bien.st_PhanLoaiKP.TuTruc).ToList();
            if (kphong.Count > 0)
            {
                KhoaKe themmoi1 = new KhoaKe();
                themmoi1.TenKP = "Chọn tất cả";
                themmoi1.MaKP = 0;
                themmoi1.Chon = true;
                _lKhoake.Add(themmoi1);
                foreach (var a in kphong)
                {
                    KhoaKe themmoi = new KhoaKe();
                    themmoi.TenKP = a.TenKP;
                    themmoi.MaKP = a.MaKP;
                    themmoi.Chon = true;
                    _lKhoake.Add(themmoi);
                }
                grcKhoaphong.DataSource = _lKhoake.ToList();
            }

            LupMaBN.DataSource = _data.BenhNhans.ToList();
            _lTrangThai.Add(new ctrangThai { Status = -1, TrangThai = "Không lĩnh" });
            _lTrangThai.Add(new ctrangThai { Status = 0, TrangThai = "Chưa xuất" });
            _lTrangThai.Add(new ctrangThai { Status = 1, TrangThai = "Đã xuất" });
            _lTrangThai.Add(new ctrangThai { Status = 2, TrangThai = "Hủy đơn" });
            lup_Status.DataSource = _lTrangThai.ToList();
            if(makho_nxt != 0 && madv_nxt != 0 && dongia_nxt != 0)
            {
                lupKhoXuat.EditValue = makho_nxt;
                lupMaDV.EditValue = madv_nxt;
                cboDonGia.EditValue = dongia_nxt;
                cboStatus.SelectedIndex = 1;
                TimKiem(true);
            }
        }
        int makho = 0;
        int madv = 0;
        private void lupKhoXuat_EditValueChanged(object sender, EventArgs e)
        {
            List<Thuoc> dsThuoc = new List<Thuoc>();
            Thuoc thuoc = new Thuoc();
            if (lupKhoXuat.EditValue != null)
                makho = Convert.ToInt32(lupKhoXuat.EditValue);
            var duoc2 = (from tenduoc in _data.DichVus.Where(p => p.PLoai == 1)
                         join nhapduoc in _data.NhapDcts on tenduoc.MaDV equals nhapduoc.MaDV

                         join nduoc in _data.NhapDs.Where(p => p.PLoai == 1).Where(p => p.MaKP == makho) on nhapduoc.IDNhap equals nduoc.IDNhap
                         select new { tenduoc, nhapduoc, nduoc }).ToList();
            var duoc = (from tenduoc in duoc2
                        group tenduoc by new { tenduoc.nduoc.MaKP, tenduoc.tenduoc.TenDV, tenduoc.tenduoc.MaDV, tenduoc.tenduoc.DonVi, tenduoc.tenduoc.MaTam } into kq
                        select new { kq.Key.TenDV, kq.Key.MaDV, kq.Key.DonVi, kq.Key.MaKP, kq.Key.MaTam }
                        ).OrderBy(p => p.TenDV).ToList();
            dsThuoc.Add(new Thuoc { TenDV = "Tất cả", MaDV = 0, DonVi = "", MaKP = null });
            foreach (var item in duoc)
            {
                thuoc = new Thuoc();
                thuoc.TenDV = item.TenDV;
                thuoc.MaDV = item.MaDV;
                thuoc.DonVi = item.DonVi;
                thuoc.MaKP = item.MaKP;
                thuoc.MaTam = item.MaTam;
                dsThuoc.Add(thuoc);
            }
            lupMaDV.Properties.DataSource = dsThuoc.ToList();
        }

        private void lupMaDV_EditValueChanged(object sender, EventArgs e)
        {
            cboDonGia.Text = "";
            for (int a = 0; a < cboDonGia.Properties.Items.Count; a++)
            {
                cboDonGia.Properties.Items.RemoveAt(a);
            }
            if (lupMaDV.EditValue != null)
            {
                madv = Convert.ToInt32(lupMaDV.EditValue);
                var gia2 = (from nhapduoc in _data.NhapDcts.Where(p => p.MaDV == madv)
                            join nduoc in _data.NhapDs.Where(p => p.PLoai == 1).Where(p => p.MaKP == makho) on nhapduoc.IDNhap equals nduoc.IDNhap
                            select nhapduoc.DonGia).ToList();
                var gia = gia2.Distinct().ToList();
                if (gia.Count > 0)
                {
                    foreach (var g in gia)
                    {
                        cboDonGia.Properties.Items.Add(g);
                    }
                }
            }
            else
            {
                madv = 0;
            }
        }

        #region class
        private class c_DanhSach
        {
            double _DonGia;

            public double DonGia
            {
                get { return _DonGia; }
                set { _DonGia = value; }
            }
            DateTime _NgayKe;

            public DateTime NgayKe
            {
                get { return _NgayKe; }
                set { _NgayKe = value; }
            }
            int _IDDon;

            public int IDDon
            {
                get { return _IDDon; }
                set { _IDDon = value; }
            }
            double _SoLuong;

            public double SoLuong
            {
                get { return _SoLuong; }
                set { _SoLuong = value; }
            }
            int _SoPL;

            public int SoPL
            {
                get { return _SoPL; }
                set { _SoPL = value; }
            }
            int _Status;

            public int Status
            {
                get { return _Status; }
                set { _Status = value; }
            }
            int _MaBNhan;

            public int MaBNhan
            {
                get { return _MaBNhan; }
                set { _MaBNhan = value; }
            }
            int _MaKP;

            public int MaKP
            {
                get { return _MaKP; }
                set { _MaKP = value; }
            }
            bool _Chon;

            public bool Chon
            {
                get { return _Chon; }
                set { _Chon = value; }
            }
            int _MaDV;

            public int MaDV
            {
                get { return _MaDV; }
                set { _MaDV = value; }
            }
            string _TenDV;

            public string TenDV
            {
                get { return _TenDV; }
                set { _TenDV = value; }
            }

            int _IDDonct;

            public int IDDonct
            {
                get { return _IDDonct; }
                set { _IDDonct = value; }
            }
        }

        private class KhoaKe
        {
            private string tenKP;

            public string TenKP
            {
                get { return tenKP; }
                set { tenKP = value; }
            }
            private int maKP;

            public int MaKP
            {
                get { return maKP; }
                set { maKP = value; }
            }

            private bool chon;

            public bool Chon
            {
                get { return chon; }
                set { chon = value; }
            }
        }

        private class Thuoc
        {
            private string tenDV;

            public string TenDV
            {
                get { return tenDV; }
                set { tenDV = value; }
            }
            private int maDV;

            public int MaDV
            {
                get { return maDV; }
                set { maDV = value; }
            }
            private string donVi;

            public string DonVi
            {
                get { return donVi; }
                set { donVi = value; }
            }
            private int? maKP;

            public int? MaKP
            {
                get { return maKP; }
                set { maKP = value; }
            }
            private string matam;
            public string MaTam
            {
                get { return matam; }
                set { matam = value; }
            }
        }
        #endregion
        List<KhoaKe> _lKhoake = new List<KhoaKe>();
        List<c_DanhSach> _lDanhsach = new List<c_DanhSach>();
        void TimKiem(bool TK)
        {
            _lDanhsach.Clear();
            grcTraCuu.DataSource = null;
            if (TK)
            {
                List<KhoaKe> _kpChon = new List<KhoaKe>();
                _kpChon = _lKhoake.Where(p => p.MaKP > 0).Where(p => p.Chon == true).ToList();

                int status = -1;
                _dttu = DungChung.Ham.NgayTu(dtTimTuNgay.DateTime);
                _dtden = DungChung.Ham.NgayDen(dtTimDenNgay.DateTime);
                double dongia = 0;
                if (lupMaDV.EditValue != null)
                {
                    madv = Convert.ToInt32(lupMaDV.EditValue);
                }
                if (!string.IsNullOrEmpty(cboDonGia.Text))
                {
                    dongia = Convert.ToDouble(cboDonGia.EditValue);
                }
                if (!string.IsNullOrEmpty(cboStatus.Text))
                { status = cboStatus.SelectedIndex; }
                if (status == 3)
                    status = -1;
                if (madv == 0)
                {
                    _lDanhsach = (from dt in _data.DThuocs.Where(p => p.MaKXuat == makho).Where(p => p.NgayKe >= _dttu).Where(p => p.NgayKe <= _dtden)
                                  join dtct in _data.DThuoccts on dt.IDDon equals dtct.IDDon
                                  join dv in _data.DichVus.Where(p => p.PLoai == 1) on dtct.MaDV equals dv.MaDV
                                  where (dtct.Status == status)
                                  select new c_DanhSach { TenDV = dv.TenDV, MaDV = madv, Chon = false, DonGia = dtct.DonGia == null ? 0 : dtct.DonGia, IDDonct = dtct.IDDonct, NgayKe = dt.NgayKe == null ? new DateTime() : dt.NgayKe.Value, IDDon = dtct.IDDon.Value, SoLuong = dtct.SoLuong, SoPL = dtct.SoPL == null ? 0 : dtct.SoPL, Status = dtct.Status == null ? 0 : dtct.Status.Value, MaBNhan = dt.MaBNhan == null ? 0 : dt.MaBNhan.Value, MaKP = dt.MaKP == null ? 0 : dt.MaKP.Value }).OrderBy(p => p.NgayKe).ToList();
                }
                else
                {
                    _lDanhsach = (from dt in _data.DThuocs.Where(p => p.MaKXuat == makho).Where(p => p.NgayKe >= _dttu).Where(p => p.NgayKe <= _dtden)
                                  join dtct in _data.DThuoccts.Where(p => p.DonGia == dongia) on dt.IDDon equals dtct.IDDon
                                  join dv in _data.DichVus.Where(p => p.PLoai == 1) on dtct.MaDV equals dv.MaDV
                                  where (dtct.Status == status)
                                  && (madv == 0 ? true : dtct.MaDV == madv)
                                  && (dongia == 0 ? true : dtct.DonGia == dongia)
                                  select new c_DanhSach { TenDV = dv.TenDV, MaDV = madv, Chon = false, DonGia = dtct.DonGia == null ? 0 : dtct.DonGia, IDDonct = dtct.IDDonct, NgayKe = dt.NgayKe == null ? new DateTime() : dt.NgayKe.Value, IDDon = dtct.IDDon.Value, SoLuong = dtct.SoLuong, SoPL = dtct.SoPL == null ? 0 : dtct.SoPL, Status = dtct.Status == null ? 0 : dtct.Status.Value, MaBNhan = dt.MaBNhan == null ? 0 : dt.MaBNhan.Value, MaKP = dt.MaKP == null ? 0 : dt.MaKP.Value }).OrderBy(p => p.NgayKe).ToList();
                }
                _lDanhsach = (from kp in _kpChon
                              join ds in _lDanhsach on kp.MaKP equals ds.MaKP
                              select ds).ToList();
            }
            grcTraCuu.DataSource = "";
            grcTraCuu.DataSource = _lDanhsach.OrderBy(p => p.TenDV);
        }
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            TimKiem(true);
        }
        private void _Chon(bool chon)
        {
            foreach (var a in _lDanhsach)
            {
                a.Chon = chon;
            }
            grcTraCuu.DataSource = null;
            grcTraCuu.DataSource = _lDanhsach;
        }
        private void loc()
        {
            grcTraCuu.DataSource = null;
            switch (rad_loc.SelectedIndex)
            {
                case 0:
                    grcTraCuu.DataSource = _lDanhsach.Where(p => p.Chon == false).ToList();
                    break;
                case 1:
                    grcTraCuu.DataSource = _lDanhsach.Where(p => p.Chon == true).ToList();
                    break;
                case 2:
                    grcTraCuu.DataSource = _lDanhsach;
                    break;
            }

        }
        bool _huy(int status, int _madv)
        {
            try
            {
                _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                List<int> dsach = (_lDanhsach.Where(p => p.Chon == true).Select(p => p.IDDonct)).Distinct().ToList();
                foreach (var a in dsach)
                {
                    var dthuoc = _data.DThuoccts.Where(p => p.IDDonct == a).Where(p => _madv == 0 ? true : p.MaDV == _madv).ToList();
                    foreach (var b in dthuoc)
                    {
                        b.Status = status;
                        _data.SaveChanges();
                    }
                }


                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public void _getValue(bool a)
        {
            _ktmatkhau = a;
        }
        public bool _ktmatkhau = false;
        private void btn_Huythuoc_Click(object sender, EventArgs e)
        {
            _ktmatkhau = false;

            bool _huyDon = DungChung.Ham.checkQuyen(this.Name)[0];
            if (!_huyDon)
            {
                MessageBox.Show("Chức năng bị giới hạn!");
                return;
            }
            ChucNang.frm_CheckPass frm = new ChucNang.frm_CheckPass();
            frm.ok = new ChucNang.frm_CheckPass._getdata(_getValue);
            frm.ShowDialog();
            int _status = -1;
            if (cboStatus.SelectedIndex == 0)
                _status = 2;
            if (cboStatus.SelectedIndex == 2)
                _status = 0;
            if (_ktmatkhau)
                if (_huy(_status, madv))
                {
                    MessageBox.Show(btn_Huythuoc.Text + " thành công!");
                }
                else
                    MessageBox.Show("Lỗi!");
        }

        private void hyp_Chon_OpenLink(object sender, DevExpress.XtraEditors.Controls.OpenLinkEventArgs e)
        {
            _Chon(true);
        }

        private void hyp_HuyChon_OpenLink(object sender, DevExpress.XtraEditors.Controls.OpenLinkEventArgs e)
        {
            _Chon(false);
        }

        private void rad_loc_SelectedIndexChanged(object sender, EventArgs e)
        {
            loc();
        }
        private void EnableControl(bool b)
        {
            lupKhoXuat.Properties.ReadOnly = b;
            lupMaDV.Properties.ReadOnly = b;

            cboDonGia.Properties.ReadOnly = b;
            cboStatus.Properties.ReadOnly = b;
            dtTimTuNgay.Properties.ReadOnly = b;
            dtTimDenNgay.Properties.ReadOnly = b;
        }
        private void chk_TimKiem_CheckedChanged(object sender, EventArgs e)
        {
            btn_Huythuoc.Enabled = chk_TimKiem.Checked;
            btnXuatExcel.Enabled = chk_TimKiem.Checked;
            EnableControl(chk_TimKiem.Checked);

            TimKiem(chk_TimKiem.Checked);
        }

        private void cboStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            btn_Huythuoc.Visible = true;
            if (cboStatus.SelectedIndex == 0)
            {
                btn_Huythuoc.Text = "Hủy thuốc";
            }
            if (cboStatus.SelectedIndex == 1 || cboStatus.SelectedIndex == 3)
            {
                btn_Huythuoc.Visible = false;
            }
            if (cboStatus.SelectedIndex == 2)
            {
                btn_Huythuoc.Text = "Bỏ hủy";
            }
        }

        private void cklKP_Leave(object sender, EventArgs e)
        {

        }

        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            if (_lDanhsach.Count > 0)
            {
                var xuatExcel = (from ds in _lDanhsach
                                 join kp in _data.KPhongs on ds.MaKP equals kp.MaKP
                                 join bn in _data.BenhNhans on ds.MaBNhan equals bn.MaBNhan
                                 select new
                                 {
                                     ds.IDDon,
                                     ds.TenDV,
                                     ds.NgayKe,
                                     kp.TenKP,
                                     ds.SoLuong,
                                     bn.TenBNhan,
                                     Noitru = "",
                                     ds.SoPL,
                                     TrangThai = ds.Status == -1 ? "Không lĩnh" : (ds.Status == 0 ? "Chưa lĩnh" : (ds.Status == 1 ? "Đã lĩnh" : (ds.Status == 2 ? "Hủy" : "")))
                                 }).OrderBy(o => o.NgayKe).ToList();

                string[] _arr = new string[] { "0", "@", "@", "dd/MM/yyyy HH:mm:ss", "@", "@", "@", "@", "@", "@" };
                string[] _tieude = { "Stt", "ID Đơn", "Tên thuốc", "Ngày kê", "Bộ phận kê", "Số lượng", "Bệnh nhân", "Nội|Ngoại trú", "Số phiếu lĩnh", "Trạng thái" };
                string _filePath = "C:\\" + "DanhSachThuocKeDon.xls";
                int[] _arrWidth = new int[] { };
                var qexcel = xuatExcel;
                DungChung.Bien.MangHaiChieu = new Object[qexcel.Count() + 1, 10];
                for (int i = 0; i < 10; i++)
                {
                    DungChung.Bien.MangHaiChieu[0, i] = _tieude[i];
                }
                int num = 1;
                foreach (var r in qexcel)
                {
                    DungChung.Bien.MangHaiChieu[num, 0] = num;
                    DungChung.Bien.MangHaiChieu[num, 1] = r.IDDon;
                    DungChung.Bien.MangHaiChieu[num, 2] = r.TenDV;
                    DungChung.Bien.MangHaiChieu[num, 3] = r.NgayKe;
                    DungChung.Bien.MangHaiChieu[num, 4] = r.TenKP;
                    DungChung.Bien.MangHaiChieu[num, 5] = r.SoLuong;
                    DungChung.Bien.MangHaiChieu[num, 6] = r.TenBNhan;
                    DungChung.Bien.MangHaiChieu[num, 7] = r.Noitru;
                    DungChung.Bien.MangHaiChieu[num, 8] = r.SoPL;
                    DungChung.Bien.MangHaiChieu[num, 9] = r.TrangThai;

                    num++;
                }

                QLBV_Library.QLBV_Ham.xuatExcelArr(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "DanhSachThuocKeDon", _filePath, true);
            }
        }

        private void grcKhoaphong_Click(object sender, EventArgs e)
        {

        }

        private void grvKhoaphong_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "Chọn")
            {
                if (grvKhoaphong.GetFocusedRowCellValue("TenKP") != null)
                {
                    string Ten = grvKhoaphong.GetFocusedRowCellValue("TenKP").ToString();

                    if (Ten == "Chọn tất cả")
                    {
                        if (_lKhoake.First().Chon == true)
                        {
                            foreach (var a in _lKhoake)
                            {
                                a.Chon = false;
                            }
                        }
                        else
                        {
                            foreach (var a in _lKhoake)
                            {
                                a.Chon = true;
                            }
                        }
                        grcKhoaphong.DataSource = "";
                        grcKhoaphong.DataSource = _lKhoake.ToList();
                    }
                }
            }
        }
    }
}
