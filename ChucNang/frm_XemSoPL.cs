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

    public partial class frm_XemSoPL : DevExpress.XtraEditors.XtraForm
    {
        public frm_XemSoPL()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities _dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        #region Tìm kiếm
        void TimKiem()
        {
            int _makpxd = 0;
            int _TimBoPhan = 0;
            int status = -1;
            bool ktPhongKham = false; //nếu là phòng khám chỉ tìm những phiếu klinhx !=0
            status = chkStatus.SelectedIndex;
            
            if (lupTimMaKP.EditValue != null)
            {
                _makpxd = Convert.ToInt32(lupTimMaKP.EditValue);
            }
            int _sopl = -1;
            if (lupTimKP.EditValue != null)
                _TimBoPhan = Convert.ToInt32(lupTimKP.EditValue);
            bool _tutruc = false;

            var qkp = _dataContext.KPhongs.Where(p => p.MaKP == (_TimBoPhan)).ToList();
            var kttt = qkp.Where(p => p.PLoai == "Tủ trực").ToList();
            if (kttt.Count > 0)
                _tutruc = true;
            if (qkp.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.PhongKham).Count() > 0)
                ktPhongKham = true;
            if (!string.IsNullOrEmpty(txtSoPL.Text))
            {
                _sopl = Convert.ToInt32(txtSoPL.Text);
                if (_tutruc)
                {

                    var bpkd1 = (from kd in _dataContext.DThuocs.Where(p => p.PLDV == 1)//
                                 join dtct in _dataContext.DThuoccts.Where(p => p.SoPL > 0).Where(p => p.Status == status) on kd.IDDon equals dtct.IDDon
                                 where (dtct.SoPL == _sopl)
                                 // where (kd.MaKXuat== (_TimBoPhan))
                                 group new { kd, dtct } by new { dtct.SoPL, kd.MaKP, kd.KieuDon } into kq
                                 select new { kq.Key.SoPL, kq.Key.MaKP, TuNgay = kq.Min(p => p.kd.NgayKe), DenNgay = kq.Max(p => p.kd.NgayKe), kq.Key.KieuDon }).Distinct().OrderBy(p => p.SoPL).ToList();

                    var kph = _dataContext.KPhongs.ToList();
                    var bpkd = (from dt in bpkd1 join kp in kph on dt.MaKP equals kp.MaKP select new { dt.SoPL, dt.MaKP, kp.TenKP, dt.TuNgay, dt.DenNgay, dt.KieuDon }).OrderBy(p => p.SoPL).ToList();
                    grcBenhNhankd.DataSource = bpkd.ToList();
                }
                else
                {

                    var bpkd1 = (from kd in _dataContext.DThuocs.Where(p => p.PLDV == 1)//
                                 join dtct in _dataContext.DThuoccts.Where(p => p.SoPL > 0).Where(p => p.Status == status).Where(p => ktPhongKham ? p.SoPL != 0 : true) on kd.IDDon equals dtct.IDDon
                                 where (dtct.SoPL == _sopl)
                                 where (dtct.MaKXuat == (_makpxd))
                                 group new { kd, dtct } by new { dtct.SoPL, kd.MaKP, kd.KieuDon } into kq
                                 select new { kq.Key.SoPL, kq.Key.MaKP, TuNgay = kq.Min(p => p.kd.NgayKe), DenNgay = kq.Max(p => p.kd.NgayKe), kq.Key.KieuDon }).Distinct().OrderBy(p => p.SoPL).ToList();

                    var kph = _dataContext.KPhongs.ToList();
                    var bpkd = (from dt in bpkd1 join kp in kph on dt.MaKP equals kp.MaKP select new { dt.SoPL, dt.MaKP, kp.TenKP, dt.TuNgay, dt.DenNgay, dt.KieuDon }).OrderBy(p => p.SoPL).ToList();
                    grcBenhNhankd.DataSource = bpkd.ToList();
                }

            }
            else
            {
                if (_TimBoPhan > 0)
                {
                    if (_tutruc)
                    {

                        var bpkd1 = (from kd in _dataContext.DThuocs.Where(p => p.PLDV == 1)
                                     join dtct in _dataContext.DThuoccts.Where(p => p.Status == status).Where(p => p.SoPL > 0) on kd.IDDon equals dtct.IDDon
                                     where (dtct.MaKXuat == (_TimBoPhan))
                                     group new { kd, dtct } by new { dtct.SoPL, kd.MaKP, kd.KieuDon } into kq
                                     select new { kq.Key.SoPL, kq.Key.MaKP, TuNgay = kq.Min(p => p.kd.NgayKe), DenNgay = kq.Max(p => p.kd.NgayKe), kq.Key.KieuDon }).Distinct().OrderBy(p => p.SoPL).ToList();

                        var kph = _dataContext.KPhongs.ToList();
                        var bpkd = (from dt in bpkd1 join kp in kph on dt.MaKP equals kp.MaKP select new { dt.SoPL, dt.MaKP, kp.TenKP, dt.TuNgay, dt.DenNgay, dt.KieuDon }).OrderBy(p => p.SoPL).ToList();
                        grcBenhNhankd.DataSource = bpkd.ToList();
                    }
                    else
                    {

                        var bpkd1 = (from kd in _dataContext.DThuocs.Where(p => p.PLDV == 1)
                                     join dtct in _dataContext.DThuoccts.Where(p => p.Status == status).Where(p => p.SoPL > 0) on kd.IDDon equals dtct.IDDon
                                     where (kd.MaKP == (_TimBoPhan))
                                     where (dtct.MaKXuat == (_makpxd))
                                     group new { kd, dtct } by new { dtct.SoPL, kd.MaKP, kd.KieuDon } into kq
                                     select new { kq.Key.SoPL, kq.Key.MaKP, TuNgay = kq.Min(p => p.kd.NgayKe), DenNgay = kq.Max(p => p.kd.NgayKe), kq.Key.KieuDon }).Distinct().OrderBy(p => p.SoPL).ToList();

                        var kph = _dataContext.KPhongs.ToList();
                        var bpkd = (from dt in bpkd1 join kp in kph on dt.MaKP equals kp.MaKP select new { dt.SoPL, dt.MaKP, kp.TenKP, dt.TuNgay, dt.DenNgay, dt.KieuDon }).OrderBy(p => p.SoPL).ToList();
                        grcBenhNhankd.DataSource = bpkd.ToList();
                    }
                }
                else
                {
                    if (_tutruc)
                    {
                        var bpkd1 = (from kd in _dataContext.DThuocs.Where(p => p.PLDV == 1)
                                     join dtct in _dataContext.DThuoccts.Where(p => p.Status == status).Where(p => p.SoPL > 0) on kd.IDDon equals dtct.IDDon
                                     group new { kd, dtct } by new { dtct.SoPL, kd.MaKP, kd.KieuDon } into kq
                                     select new { kq.Key.SoPL, kq.Key.MaKP, TuNgay = kq.Min(p => p.kd.NgayKe), DenNgay = kq.Max(p => p.kd.NgayKe), kq.Key.KieuDon }).Distinct().OrderBy(p => p.SoPL).ToList();

                        var kph = _dataContext.KPhongs.ToList();
                        var bpkd = (from dt in bpkd1 join kp in kph on dt.MaKP equals kp.MaKP select new { dt.SoPL, dt.MaKP, kp.TenKP, dt.TuNgay, dt.DenNgay, dt.KieuDon }).OrderBy(p => p.SoPL).ToList();
                        grcBenhNhankd.DataSource = bpkd.ToList();
                    }
                    else
                    {
                        var bpkd1 = (from kd in _dataContext.DThuocs.Where(p => p.PLDV == 1)
                                     join dtct in _dataContext.DThuoccts.Where(p => p.Status == status).Where(p => p.SoPL > 0) on kd.IDDon equals dtct.IDDon
                                     where (dtct.MaKXuat == (_makpxd))
                                     group new { kd, dtct } by new { dtct.SoPL, kd.MaKP, kd.KieuDon } into kq
                                     select new { kq.Key.SoPL, kq.Key.MaKP, TuNgay = kq.Min(p => p.kd.NgayKe), DenNgay = kq.Max(p => p.kd.NgayKe), kq.Key.KieuDon }).Distinct().OrderBy(p => p.SoPL).ToList();

                        var kph = _dataContext.KPhongs.ToList();
                        var bpkd = (from dt in bpkd1 join kp in kph on dt.MaKP equals kp.MaKP select new { dt.SoPL, dt.MaKP, kp.TenKP, dt.TuNgay, dt.DenNgay, dt.KieuDon }).OrderBy(p => p.SoPL).ToList();
                        grcBenhNhankd.DataSource = bpkd.ToList();
                    }
                }
            }
        }
        #endregion
        private class Thuocthang
        {

            private string tendv;
            private string donvi;
            private double ngay1;
            private double ngay2;
            private double ngay3;
            private double ngay4;
            private double ngay5;
            private double thanhtien;
            private double thanhTien;

            public double ThanhTien
            {
                get { return thanhTien; }
                set { thanhTien = value; }
            }
            private int madv;
            private int sTT;

            public int STT
            {
                get { return sTT; }
                set { sTT = value; }
            }
            public int MaDV
            { set { madv = value; } get { return madv; } }
            public string TenDV
            { set { tendv = value; } get { return tendv; } }
            public string DonVi
            { set { donvi = value; } get { return donvi; } }
            public double SoLuong
            { set { ngay1 = value; } get { return ngay1; } }
            public double Ngay2
            { set { ngay2 = value; } get { return ngay2; } }
            public double Ngay3
            { set { ngay3 = value; } get { return ngay3; } }
            public double Ngay4
            { set { ngay4 = value; } get { return ngay4; } }
            public double Ngay5
            { set { ngay5 = value; } get { return ngay5; } }
            public double DonGia
            { set { thanhtien = value; } get { return thanhtien; } }
        }
        string _maCQCQ = "";
        private void frm_XemSoPL_Load(object sender, EventArgs e)
        {
            if (DungChung.Bien.MaBV != "24012")
                btnXemPLHuy.Visible = false;
            var qCQCQ = _dataContext.BenhViens.Where(panelControl1 => panelControl1.MaBV == DungChung.Bien.MaBV).FirstOrDefault();
            if (qCQCQ != null)
                _maCQCQ = qCQCQ.MaChuQuan;
            lupTimKP.EditValue = DungChung.Bien.MaKP;
            //if (DungChung.Bien.CapDo == 9 || DungChung.Bien.CapDo == 8)
            if (DungChung.Bien.PLoaiKP == DungChung.Bien.st_PhanLoaiKP.Admin)
            {
                lupTimKP.Properties.ReadOnly = false;
            }
            var tendv = (from dv in _dataContext.DichVus select new { dv.TenDV, dv.MaDV }).ToList();
            lupMaDV.DataSource = tendv.ToList();
            var kp = from kphong in _dataContext.KPhongs where (kphong.PLoai.Contains("Khoa dược") || kphong.PLoai.Contains("Lâm sàng") || kphong.PLoai.Contains("Tủ trực") || (kphong.PLoai == DungChung.Bien.st_PhanLoaiKP.PhongKham && DungChung.Bien.MaBV == "27021")) select kphong;
            lupTimMaKP.Properties.DataSource = kp.Where(p => p.PLoai.Contains("Khoa dược") || p.PLoai.Contains("Tủ trực")).ToList();
            lupTimKP.Properties.DataSource = kp.OrderBy(p => p.PLoai).ToList();
            //lupTimKPhong.Properties.DataSource = kp.Where(p => p.PLoai.Contains("Lâm sàng")).ToList();
            lupTimMaKP.EditValue = DungChung.Bien.MaKP;
            List<kieudon> _lkd = new List<kieudon>();
            kieudon kd = new kieudon();
            kd.index = 0;
            kd.ten = "Hàng ngày";
            _lkd.Add(kd);
            kieudon kd1 = new kieudon();
            kd1.index = 1;
            kd1.ten = "Bổ xung";
            _lkd.Add(kd1);
            kieudon kd2 = new kieudon();
            kd2.index = 2;
            kd2.ten = "Trả thuốc";
            _lkd.Add(kd2);
            lupKieuDon.DataSource = _lkd.ToList();
            TimKiem();
        }
        public class kieudon
        {
            public int index;
            public string ten;
            public int Index
            {
                set { index = value; }
                get { return index; }
            }
            public string Ten
            {
                set { ten = value; }
                get { return ten; }
            }
        }
        int sopl = 0;
        int _makp = 0;
        int _makp1 = 0;
        int makho = 0;
        private void grvBenhNhankd_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (grvBenhNhankd.GetFocusedRowCellValue(colSoPL) != null && grvBenhNhankd.GetFocusedRowCellValue(colSoPL).ToString() != "")
            {
                if (grvBenhNhankd.GetFocusedRowCellValue(colMaKP) != null)
                    _makp = Convert.ToInt32(grvBenhNhankd.GetFocusedRowCellValue(colMaKP));
                _makp1 = Convert.ToInt32(grvBenhNhankd.GetFocusedRowCellValue(colMaKP));
                makho = Convert.ToInt32(lupTimMaKP.EditValue);
                //if (DungChung.Bien.MaBV != "19048")
                //    _makp = 0;
                sopl = Convert.ToInt32(grvBenhNhankd.GetFocusedRowCellValue(colSoPL));
                txtSoPLinh.Text = sopl.ToString();
                var q = (from kd in _dataContext.DThuocs.Where(p => (_makp == 0 ? true : p.MaKP == _makp))
                         join kdct in _dataContext.DThuoccts.Where(p => p.Status >= 0 || p.Status == null).Where(p => p.SoPL == sopl).Where(p=>p.MaKXuat==makho) on kd.IDDon equals kdct.IDDon
                         join dv in _dataContext.DichVus on kdct.MaDV equals dv.MaDV
                         join tndv in _dataContext.TieuNhomDVs on dv.IdTieuNhom equals tndv.IdTieuNhom
                         group new { kdct } by new { kdct.DonGia, kdct.DonVi, dv.TenDV, dv.MaDV, dv.TenRG } into kq
                         select new
                         {

                             TenDV = (DungChung.Bien.MaBV == "24009" || _maCQCQ == "24009") ? kq.Key.TenRG : kq.Key.TenDV,
                             kq.Key.MaDV,
                             kq.Key.DonVi,
                             SoLuong = kq.Sum(p => p.kdct.SoLuong),
                             ThanhTien = kq.Sum(p => p.kdct.ThanhTien),
                             kq.Key.DonGia
                         }).ToList();
                int x = 1;
                List<Thuocthang> ds = new List<Thuocthang>();
                foreach (var item in q.OrderBy(p => p.MaDV))
                {
                    Thuocthang moi = new Thuocthang();
                    moi.STT = x++;
                    moi.TenDV = item.TenDV;
                    moi.MaDV = item.MaDV;
                    moi.DonVi = item.DonVi;
                    moi.SoLuong = item.SoLuong;
                    moi.ThanhTien = item.ThanhTien;
                    moi.DonGia = item.DonGia;
                    ds.Add(moi);
                }
                if (DungChung.Bien.MaBV == "30002")
                    grcDonThuocct.DataSource = ds.OrderBy(p => p.MaDV).ToList();
                else
                    grcDonThuocct.DataSource = ds.ToList();
            }
            else
            {
                _makp = 0;
                txtSoPLinh.Text = "";
                sopl = 0;
                grcDonThuocct.DataSource = null;
            }
        }

        private void grvBenhNhankd_DataSourceChanged(object sender, EventArgs e)
        {

            grvBenhNhankd_FocusedRowChanged(null, null);
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void lupTimMaKP_EditValueChanged(object sender, EventArgs e)
        {
            TimKiem();
        }

        private void txtSoPL_EditValueChanged(object sender, EventArgs e)
        {
            TimKiem();
        }

        private void chkStatus_CheckedChanged(object    sender, EventArgs e)
        {
            TimKiem();
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSoPLinh.Text))
            {
                string[] _ds = new string[2] { "", "" };
                int _soPL3 = Convert.ToInt32(txtSoPLinh.Text);
                _ds[0] = _soPL3.ToString();
                _ds[1] = _makp.ToString();
                if (_soPL3 > 0)
                {
                    if (DungChung.Bien.keNhieuKho)
                    {
                        var ktkpl = (from dt in _dataContext.DThuocs.Where(p => p.MaKP == _makp1)//.Where(p => p.SoPL == _soPL3)
                                     join dtct in _dataContext.DThuoccts.Where(p => p.SoPL == _soPL3) on dt.IDDon equals dtct.IDDon
                                     group dt by new { dt.KieuDon, dt.IDDon, dt.MaKP } into kq
                                     select new { kq.Key.KieuDon, kq.Key.IDDon, kq.Key.MaKP }).ToList();
                        if (ktkpl.Count > 0 && ktkpl.First().KieuDon != null && (ktkpl.First().KieuDon == 4 || ktkpl.First().KieuDon == 3) && ktkpl.First().MaKP == _makp1)
                        {
                            int iddon = ktkpl.First().IDDon;
                            FormNhap.frmPhieuLinh_New.InPhieu(_soPL3, _makp, 2);
                        }
                        else
                        {
                            FormNhap.frm_Check_moi frm = new FormNhap.frm_Check_moi();
                            if (DungChung.Bien.MaBV == "30002")
                            {
                                frm.InPhieu(_ds, 3);
                            }
                            else frm.InPhieu(_ds, 2);
                        }
                    }
                    else
                    {
                        var ktkpl = (from dt in _dataContext.DThuocs.Where(p => p.MaKP == _makp1)
                                     join dtct in _dataContext.DThuoccts.Where(p => p.SoPL == _soPL3) on dt.IDDon equals dtct.IDDon
                                     join dv in _dataContext.DichVus on dtct.MaDV equals dv.MaDV
                                     select new { dt.KieuDon, dt.IDDon, dt.MaKP, dv.PLoai }).ToList();
                        if (ktkpl.Count > 0 && ktkpl.First().KieuDon != null && (ktkpl.First().KieuDon == 4 || ktkpl.First().KieuDon == 3) && ktkpl.First().MaKP == _makp1)
                        {
                            int iddon = ktkpl.First().IDDon;
                            FormNhap.frm_Check_moi frm = new frm_Check_moi();
                            string[] pl1 = new string[2] { "", "" };
                            pl1[0] = _soPL3.ToString();
                            pl1[1] = _makp1.ToString();

                            if (DungChung.Bien.MaBV == "30002" || (DungChung.Bien.MaBV == "27022" && ktkpl.First().PLoai != 5))
                            {
                                frm.InPhieu(pl1, 2);
                            }
                            else frm.InPhieu(pl1, 3);
                        }
                        else
                        {
                            FormNhap.frm_Check_moi frm = new FormNhap.frm_Check_moi();
                            frm.InPhieu(_ds, 2);
                        }
                    }

                }

            }
        }
        private void btnHuy_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSoPLinh.Text))
            {
                int sopl = Convert.ToInt32(txtSoPLinh.Text);

                var kt0 = _dataContext.DThuoccts.Where(p => p.SoPL == sopl).ToList();
                if (kt0.Count > 0)
                {
                    int makhoake = kt0.First().MaKP ?? 0;
                    var kt = (from dt in _dataContext.DThuocs
                              join dtct in _dataContext.DThuoccts.Where(p => p.SoPL == sopl && (_makp == 0 ? true : p.MaKP == _makp)) on dt.IDDon equals dtct.IDDon
                              join kp in _dataContext.KPhongs on dt.MaKP equals kp.MaKP
                              select new { dt.MaBNhan, dt.IDDon, dtct.Status, kp.PLoai, dt.MaBNhanChiTiet }).ToList();
                    if (kt.Count > 0)
                    {
                        if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "24012" || DungChung.Bien.MaBV == "24389")
                        {
                            var ploai = kt.First().PLoai != null ? kt.First().PLoai : "";
                            var mabnhanchitiet = kt.First().MaBNhanChiTiet ?? kt.First().MaBNhanChiTiet;
                            if (ploai.ToString() == "Tủ trực" && !string.IsNullOrEmpty(mabnhanchitiet.ToString()))
                            {
                                MessageBox.Show("Không thể hủy đơn lĩnh bù tủ trực!", "Thông báo");
                                return;
                            }
                        }
                        bool ktrahuy = true;
                        if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049")
                        {
                            string mess = "";
                            List<int> _lmabn = kt.Select(p => p.MaBNhan ?? 0).Distinct().ToList();
                            foreach (var item in _lmabn)
                            {
                                var ktrarv = _dataContext.RaViens.Where(p => p.MaBNhan == item).ToList();
                                if (ktrarv.Count > 0)
                                {
                                    var tenbn = _dataContext.BenhNhans.Where(p => p.MaBNhan == item).FirstOrDefault();
                                    mess += tenbn.TenBNhan + ",\n";
                                }
                            }
                            if (!string.IsNullOrEmpty(mess))
                            {
                                ktrahuy = false;
                                MessageBox.Show("Phiếu lĩnh có bệnh nhân:\n" + mess + "đã ra viện, không thể hủy!", "Thông báo", MessageBoxButtons.OK);
                            }
                        }

                        if (kt.First().Status == 0)
                        {
                            if (ktrahuy == true)
                            {
                                var qtutruc = _dataContext.KPhongs.Where(p => p.MaKP == makhoake && p.PLoai == DungChung.Bien.st_PhanLoaiKP.TuTruc).FirstOrDefault();
                                if (DungChung.Bien.MaBV == "24272")
                                {
                                    DialogResult _result = MessageBox.Show("Bạn muốn hủy phiếu lĩnh", "Hỏi hủy", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                    if (_result == DialogResult.Yes)
                                    {
                                        if (qtutruc != null)
                                        {
                                            var qdttt = _dataContext.DThuoccts.Where(p => p.SoPL == sopl && p.Status == 0).ToList();
                                            foreach (var i in qdttt)
                                            {
                                                var sua = _dataContext.DThuoccts.FirstOrDefault(p => p.IDDonct == i.IDDonct);
                                                sua.SoPL = 0;
                                                _dataContext.SaveChanges();
                                            }

                                            if (qdttt.Count > 0)
                                            {
                                                var qSoPL = _dataContext.SoPLs.Where(p => p.SoPL1 == sopl && p.PhanLoai == 1).FirstOrDefault();
                                                if (qSoPL != null)
                                                    qSoPL.Status = -1;
                                                _dataContext.SaveChanges();
                                            }
                                            this.frm_XemSoPL_Load(null, null);
                                        }
                                        else
                                        {
                                            var idd = _dataContext.DThuoccts.Where(p => p.SoPL == sopl).Where(p => p.Status != 1 && p.Status != -1).ToList();// không cần thiết phải != -1 ()vì ko lĩnh ko có số PL

                                            foreach (var i in idd)
                                            {
                                                var sua = _dataContext.DThuoccts.Single(p => p.IDDonct == i.IDDonct);
                                                sua.SoPL = 0;
                                                _dataContext.SaveChanges();
                                            }
                                            var _lSoPL = _dataContext.SoPLs.Where(p => p.SoPL1 == sopl && p.PhanLoai == 1).FirstOrDefault();
                                            if (_lSoPL != null)
                                            {
                                                _lSoPL.Status = -1;
                                            }
                                            _dataContext.SaveChanges();
                                            this.frm_XemSoPL_Load(null, null);
                                        }
                                    }
                                }
                                else
                                {
                                    if (qtutruc != null)
                                    {
                                        FormNhap.frm_Check frm = new frm_Check(sopl, 9, makhoake, "");
                                        frm.FormClosed += new FormClosedEventHandler(this.frm_XemSoPL_Load);
                                        frm.ShowDialog();
                                    }

                                    else
                                    {
                                        FormNhap.frm_Check frm = new frm_Check(sopl, 5);
                                        frm.FormClosed += new FormClosedEventHandler(this.frm_XemSoPL_Load);
                                        frm.ShowDialog();
                                    }
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Phiếu lĩnh đã được xuất dược, Bạn không thể sửa");
                        }
                    }
                }
            }
        }

        private void chkStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            TimKiem();
        }

        private void lupTimKP_EditValueChanged(object sender, EventArgs e)
        {
            TimKiem();
        }


        private void grvDonThuocct_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Column.Name == "colXemBN")
            {
                int madv = 0;
                if (grvDonThuocct.GetFocusedRowCellValue(colMaDV) != null)
                {
                    madv = Convert.ToInt32(grvDonThuocct.GetFocusedRowCellValue(colMaDV));
                    double dongia = 0;
                    if (grvDonThuocct.GetFocusedRowCellValue(colDonGia) != null)
                        dongia = Convert.ToDouble(grvDonThuocct.GetFocusedRowCellValue(colDonGia));
                    FormThamSo.frm_dsBNlinhthuoc frm = new FormThamSo.frm_dsBNlinhthuoc(_makp, sopl, madv, 1, dongia);
                    frm.ShowDialog();
                }
            }
        }

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTuTruc.Checked)
            {
                int _makptt = 0;
                var _kttt = _dataContext.KPhongs.Where(p => p.NhomKP == (DungChung.Bien.MaKP)).Select(p => p.MaKP).ToList();
                if (_kttt.Count > 0)
                    _makptt = Convert.ToInt32(_kttt.First());
                lupTimKP.EditValue = _makptt;
            }
            else
            {
                lupTimKP.EditValue = DungChung.Bien.MaKP;
            }
        }

        private void grvDonThuocct_DataSourceChanged(object sender, EventArgs e)
        {
        }

        private void btnInPhieuDY_Click(object sender, EventArgs e)
        {
            if (DungChung.Bien.MaBV == "14017")
            {
                if (!string.IsNullOrEmpty(txtSoPLinh.Text))
                {
                    string[] _ds = new string[2] { "", "" };
                    int _soPL3 = Convert.ToInt32(txtSoPLinh.Text);
                    _ds[0] = _soPL3.ToString();
                    _ds[1] = _makp.ToString();
                    if (_soPL3 > 0)
                    {
                        if (DungChung.Bien.keNhieuKho)
                        {
                            var ktkpl = (from dt in _dataContext.DThuocs.Where(p => p.MaKP == _makp1)//.Where(p => p.SoPL == _soPL3)
                                         join dtct in _dataContext.DThuoccts.Where(p => p.SoPL == _soPL3) on dt.IDDon equals dtct.IDDon
                                         group dt by new { dt.KieuDon, dt.IDDon, dt.MaKP } into kq
                                         select new { kq.Key.KieuDon, kq.Key.IDDon, kq.Key.MaKP }).ToList();
                            if (ktkpl.Count > 0 && ktkpl.First().KieuDon != null && (ktkpl.First().KieuDon == 4 || ktkpl.First().KieuDon == 3) && ktkpl.First().MaKP == _makp1)
                            {
                                int iddon = ktkpl.First().IDDon;
                                FormNhap.frmPhieuLinh_New.InPhieu(_soPL3, _makp, 2);
                            }
                            else
                            {
                                FormNhap.frm_Check_moi frm = new FormNhap.frm_Check_moi();
                                if (DungChung.Bien.MaBV == "30002")
                                {
                                    frm.InPhieu(_ds, 3);
                                }
                                else frm.InPhieu_14007(_ds, 2);
                            }
                        }
                        else
                        {
                            var ktkpl = (from dt in _dataContext.DThuocs.Where(p => p.MaKP == _makp1)
                                         join dtct in _dataContext.DThuoccts.Where(p => p.SoPL == _soPL3) on dt.IDDon equals dtct.IDDon
                                         join dv in _dataContext.DichVus on dtct.MaDV equals dv.MaDV
                                         select new { dt.KieuDon, dt.IDDon, dt.MaKP, dv.PLoai }).ToList();
                            if (ktkpl.Count > 0 && ktkpl.First().KieuDon != null && (ktkpl.First().KieuDon == 4 || ktkpl.First().KieuDon == 3) && ktkpl.First().MaKP == _makp1)
                            {
                                int iddon = ktkpl.First().IDDon;
                                FormNhap.frm_Check_moi frm = new frm_Check_moi();
                                string[] pl1 = new string[2] { "", "" };
                                pl1[0] = _soPL3.ToString();
                                pl1[1] = _makp1.ToString();

                                if (DungChung.Bien.MaBV == "30002" || (DungChung.Bien.MaBV == "27022" && ktkpl.First().PLoai != 5))
                                {
                                    frm.InPhieu(pl1, 2);
                                }
                                else frm.InPhieu(pl1, 3);
                            }
                            else
                            {
                                FormNhap.frm_Check_moi frm = new FormNhap.frm_Check_moi();
                                frm.InPhieu_14007(_ds, 2);
                            }
                        }

                    }

                }
            }
            else
            {
                if (!string.IsNullOrEmpty(txtSoPLinh.Text))
                {
                    int _soPL3 = Convert.ToInt32(txtSoPLinh.Text);
                    if (DungChung.Bien.MaBV == "20001")
                        frmPhieulinh._InPhieuThuocDY_20001(_soPL3);
                    else
                        frmPhieulinh._InPhieuThuocDY(_soPL3);
                }
            }
        }
        private void grvDonThuocct_BeforePrintRow(object sender, DevExpress.XtraGrid.Views.Printing.CancelPrintRowEventArgs e)
        {

        }

        private void btnXemPLHuy_Click(object sender, EventArgs e)
        {
            frm_XemSoPLHuy frm = new frm_XemSoPLHuy();
            frm.ShowDialog();
        }
    }
}