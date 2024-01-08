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

namespace QLBV.FormTraCuu
{
    public partial class Frm_TCThuocTTchuaXuatct : DevExpress.XtraEditors.XtraForm
    {
        public Frm_TCThuocTTchuaXuatct()
        {
            InitializeComponent();
        }
        int _madv = 0;
        DateTime _dttu; DateTime _dtden;
        List<string> txtKP = new List<string>();

        public Frm_TCThuocTTchuaXuatct(int madv, DateTime dttu, DateTime dtden)
        {
            InitializeComponent();
            _madv = madv;
            _dttu = dttu;
            _dtden = dtden;

            cboOptionBC.SelectedIndex = 0;
            grvKho.SelectAll();
            grvKhoaPhong.SelectAll();
        }

        private class NXTct
        {
            private int id;
            private int mabn;
            private string tenbn;
            private string tendv;
            private int sopl;
            private DateTime ngay;
            private string donvi;
            private double dongia;
            private string khoaphong;
            private double soluongkd;
            private double thanhtienkd;
            private double soluongxd;
            private double thanhtienxd;
            private double soluongtt;
            private double thanhtientt;
            private string solo;
            private string tenKphong;
            private string tenKXuat;
            private DateTime? handung;
            private DateTime ngayra;
            private DateTime ngayvao;

            public int ID
            { set { id = value; } get { return id; } }
            public int MaBN
            { set { mabn = value; } get { return mabn; } }
            public string TenBN
            { set { tenbn = value; } get { return tenbn; } }
            public string TenDV
            { set { tendv = value; } get { return tendv; } }
            public DateTime Ngay
            { set { ngay = value; } get { return ngay; } }
            public string TenKP
            { set { khoaphong = value; } get { return khoaphong; } }
            public string DonVi
            { set { donvi = value; } get { return donvi; } }
            public double DonGia
            { set { dongia = value; } get { return dongia; } }
            public double SoLuongKD
            { set { soluongkd = value; } get { return soluongkd; } }
            public double ThanhTienKD
            { set { thanhtienkd = value; } get { return thanhtienkd; } }
            public double SoLuongXD
            { set { soluongxd = value; } get { return soluongxd; } }
            public double ThanhTienXD
            { set { thanhtienxd = value; } get { return thanhtienxd; } }
            public double SoLuongTT
            { set { soluongtt = value; } get { return soluongtt; } }
            public double ThanhTienTT
            { set { thanhtientt = value; } get { return thanhtientt; } }
            public int SoPL
            { set { sopl = value; } get { return sopl; } }
            public string SoLo
            { set { solo = value; } get { return solo; } }
            public string TenKXuat
            { set { tenKXuat = value; } get { return tenKXuat; } }
            public DateTime? HanDung
            { set; get; }
            public DateTime NgayRa
            { set { ngayra = value; } get { return ngayra; } }
            public DateTime NgayVao
            { set { ngayvao = value; } get { return ngayvao; } }


        }
        List<NXTct> _NXTct = new List<NXTct>();
        private void Frm_TcNhapXuatTonct_Load(object sender, EventArgs e)
        {
            _NXTct.Clear();
            QLBV_Database.QLBVEntities DataContect = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

            var khoaPhong = (from kp in DataContect.KPhongs.Where(p => p.PLoai.Contains("Lâm sàng") || p.PLoai.Contains("Phòng khám")) select new { kp.TenKP }).ToList();
            gcKhoaPhong.DataSource = khoaPhong;
            var kho = (from kp in DataContect.KPhongs.Where(p => p.PLoai.Contains("Khoa dược") || p.PLoai.Contains("Tủ trực")) select new { kp.TenKP }).ToList();
            gcKho.DataSource = kho;

            var t = (from dv in DataContect.DichVus.Where(p => p.MaDV == _madv) select new { dv.TenDV }).ToList();
            if (t.Count > 0)
            {
                lblTitle.Text = ("Quá trình Kê đơn - Thanh toán - xuất thuốc:   ").ToUpper() + t.First().TenDV;
            }
            else lblTitle.Text = ("Quá trình Kê đơn - Thanh toán - xuất thuốc:   ").ToUpper();

            var qnct = (from dt in DataContect.DThuocs
                        join dtct in DataContect.DThuoccts.Where(p => p.MaDV == _madv) on dt.IDDon equals dtct.IDDon
                        join bn in DataContect.BenhNhans on dt.MaBNhan equals bn.MaBNhan
                        join dv in DataContect.DichVus on dtct.MaDV equals dv.MaDV
                        join kp in DataContect.KPhongs on dt.MaKP equals kp.MaKP
                        join rv in DataContect.RaViens on bn.MaBNhan equals rv.MaBNhan
                        select new { dt.IDDon, dt.NgayKe, kp.TenKP, dt.MaBNhan, dtct.SoPL, dv.TenDV, dtct.DonVi, dtct.DonGia, dtct.SoLuong, dtct.ThanhTien, dtct.Status, bn.TenBNhan, rv.NgayRa, rv.NgayVao, dtct.SoLo, dtct.HanDung, dtct.MaKXuat }).ToList();

            var qnct1 = (from p in qnct
                         join kp in DataContect.KPhongs on p.MaKXuat equals kp.MaKP
                         select new { p.IDDon, p.NgayKe, p.TenKP, p.MaBNhan, p.SoPL, p.TenDV, p.DonVi, p.DonGia, p.SoLuong, p.ThanhTien, p.Status, p.TenBNhan, p.NgayRa, p.NgayVao, p.SoLo, p.HanDung, p.MaKXuat, TenKXuat = kp.TenKP }).ToList();
            if (qnct1.Count > 0)
            {
                foreach (var a in qnct1)
                {
                    NXTct them = new NXTct();
                    them.ID = a.IDDon;
                    them.TenDV = a.TenDV;
                    them.TenKP = a.TenKP;
                    them.TenBN = a.TenBNhan;
                    them.MaBN = a.MaBNhan == null ? 0 : a.MaBNhan.Value;
                    them.Ngay = a.NgayKe.Value;
                    them.DonVi = a.DonVi;
                    them.SoPL = Convert.ToInt32(a.SoPL);
                    them.DonGia = Convert.ToDouble(a.DonGia);
                    them.SoLuongKD = Convert.ToInt32(a.SoLuong);
                    if (a.Status == 1)
                    {
                        them.SoLuongXD = Convert.ToInt32(a.SoLuong);
                    } else
                    {
                        them.SoLuongKD = 0;
                    }
                    them.ThanhTienKD = Convert.ToDouble(a.ThanhTien);
                    them.TenKXuat = a.TenKXuat;
                    them.NgayVao = a.NgayVao.Value;
                    them.NgayRa = a.NgayRa.Value;
                    them.HanDung = a.HanDung.HasValue ? (DateTime?)a.HanDung.Value : null;
                    them.SoLo = a.SoLo == null ? "0" : a.SoLo;
                    _NXTct.Add(them);
                }
            }

            //var qtt = (from vp in DataContect.VienPhis
            //           join vpct in DataContect.VienPhicts.Where(p => p.MaDV == _madv) on vp.idVPhi equals vpct.idVPhi
            //           join bn in DataContect.BenhNhans on vp.MaBNhan equals bn.MaBNhan
            //           join dv in DataContect.DichVus on vpct.MaDV equals dv.MaDV
            //           join kp in DataContect.KPhongs on vp.MaKP equals kp.MaKP
            //           join rv in DataContect.RaViens on bn.MaBNhan equals rv.MaBNhan
            //           select new { vp.idVPhi, dv.TenDV, kp.TenKP, vp.MaBNhan, vp.NgayTT, vpct.DonVi, vpct.DonGia, vpct.SoLuong, vpct.ThanhTien, bn.TenBNhan, rv.NgayRa, rv.NgayVao }).ToList();
            //if (qtt.Count > 0)
            //{
            //    foreach (var b in qtt)
            //    {
            //        NXTct them2 = new NXTct();
            //        them2.ID = b.idVPhi;
            //        them2.TenDV = b.TenDV;
            //        them2.TenKP = b.TenKP;
            //        them2.TenBN = b.TenBNhan;
            //        them2.MaBN = b.MaBNhan == null ? 0 : b.MaBNhan.Value;
            //        them2.Ngay = b.NgayTT.Value;
            //        them2.DonVi = b.DonVi;
            //        them2.DonGia = Convert.ToDouble(b.DonGia);
            //        them2.SoLuongTT = Convert.ToInt32(b.SoLuong);
            //        them2.ThanhTienTT = Convert.ToDouble(b.ThanhTien);
            //        them2.SoLo = "0";
            //        them2.NgayVao = b.NgayVao.Value;
            //        them2.NgayRa = b.NgayRa.Value;
            //        _NXTct.Add(them2);
            //    }

            //}
            //var qxct = (from dt in DataContect.DThuocs
            //            join dtct in DataContect.DThuoccts.Where(p => p.MaDV == _madv).Where(p => p.Status == 1) on dt.IDDon equals dtct.IDDon
            //            join bn in DataContect.BenhNhans on dt.MaBNhan equals bn.MaBNhan
            //            join dv in DataContect.DichVus on dtct.MaDV equals dv.MaDV
            //            join kp in DataContect.KPhongs on dt.MaKP equals kp.MaKP
            //            join rv in DataContect.RaViens on bn.MaBNhan equals rv.MaBNhan
            //            select new { dt.IDDon, dt.NgayKe, kp.TenKP, dt.MaBNhan, dtct.SoPL, dv.TenDV, dtct.DonVi, dtct.DonGia, dtct.SoLuong, dtct.ThanhTien, bn.TenBNhan, rv.NgayRa, rv.NgayVao, dtct.SoLo, dtct.HanDung, dtct.MaKXuat }).ToList();
            //var qxct1 = (from p in qxct
            //             join kp in DataContect.KPhongs on p.MaKXuat equals kp.MaKP
            //             select new { p.IDDon, p.NgayKe, p.TenKP, p.MaBNhan, p.SoPL, p.TenDV, p.DonVi, p.DonGia, p.SoLuong, p.ThanhTien, p.TenBNhan, p.NgayRa, p.NgayVao, p.SoLo, p.HanDung, p.MaKXuat, TenKXuat = kp.TenKP }).ToList();

            //if (qxct1.Count > 0)
            //{
            //    foreach (var b in qxct1)
            //    {
            //        NXTct them1 = new NXTct();
            //        them1.ID = b.IDDon;
            //        them1.TenDV = b.TenDV;
            //        them1.TenKP = b.TenKP;
            //        them1.TenBN = b.TenBNhan;
            //        them1.MaBN = b.MaBNhan == null ? 0 : b.MaBNhan.Value;
            //        them1.SoPL = Convert.ToInt32(b.SoPL);
            //        them1.Ngay = b.NgayKe.Value;
            //        them1.DonVi = b.DonVi;
            //        them1.DonGia = Convert.ToDouble(b.DonGia);
            //        them1.SoLuongXD = Convert.ToInt32(b.SoLuong);
            //        them1.ThanhTienXD = Convert.ToDouble(b.ThanhTien);
            //        them1.NgayVao = b.NgayVao.Value;
            //        them1.NgayRa = b.NgayRa.Value;
            //        them1.TenKXuat = b.TenKXuat;
            //        them1.HanDung = b.HanDung.HasValue ? (DateTime?)b.HanDung.Value : null;
            //        them1.SoLo = b.SoLo == null ? "0" : b.SoLo;
            //        _NXTct.Add(them1);
            //    }
            //}
            var newList = (from lnx in _NXTct.Where(p => p.Ngay >= _dttu && p.Ngay <= _dtden)
                           group lnx by new { lnx.ID, lnx.Ngay, lnx.TenKP, lnx.MaBN, lnx.TenBN, lnx.DonVi, lnx.DonGia, lnx.SoLuongKD, lnx.ThanhTienKD, lnx.SoLuongXD, lnx.SoPL, lnx.SoLo, lnx.HanDung, lnx.TenKXuat, lnx.NgayVao, lnx.NgayRa } into kq
                           select new { kq.Key.ID, kq.Key.Ngay, kq.Key.TenKP, kq.Key.MaBN, kq.Key.TenBN, kq.Key.DonVi, kq.Key.DonGia, kq.Key.SoLuongKD, kq.Key.ThanhTienKD, kq.Key.SoLuongXD, kq.Key.SoPL, kq.Key.SoLo, kq.Key.HanDung, kq.Key.TenKXuat, kq.Key.NgayVao, kq.Key.NgayRa, }).ToList();

            grcNhapCT.DataSource = newList.ToList();
        }

        private void grvNhapCT_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column == STT)
            {
                e.DisplayText = Convert.ToString(e.RowHandle + 1);
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            txtKP.Clear();
            List<string> dsTenKpChon = new List<string>();
            List<string> dsTenKhoChon = new List<string>();
            int[] index = grvKhoaPhong.GetSelectedRows();
            int[] index2 = grvKho.GetSelectedRows();
            if (index.Count() > 0)
            {
                for (int i = 0; i < index.Count(); i++)
                {
                    dsTenKpChon.Add(Convert.ToString(grvKhoaPhong.GetRowCellValue(index[i], colTenKP)));
                    txtKP.Add(Convert.ToString(grvKhoaPhong.GetRowCellValue(index[i], colTenKP)));
                }
            } else
            {
                MessageBox.Show("Bạn chưa chọn khoa phòng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            if (index.Count() > 0)
            {
                for (int j = 0; j < index2.Count(); j++)
                {
                    dsTenKhoChon.Add(Convert.ToString(grvKho.GetRowCellValue(index2[j], colTenKho)));
                }
            }
            else
            {
                MessageBox.Show("Bạn chưa chọn kho.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            var listSearch = (from a in _NXTct.Where(p => p.Ngay >= _dttu && p.Ngay <= _dtden)
                              where dsTenKpChon.Contains(a.TenKP) && dsTenKhoChon.Contains(a.TenKXuat)
                              select new { a.ID, a.Ngay, a.TenKP, a.MaBN, a.TenBN, a.DonVi, a.DonGia, a.SoLuongKD, a.ThanhTienKD, a.SoLuongXD, a.SoPL, a.SoLo, a.HanDung, a.TenKXuat, a.NgayVao, a.NgayRa }).ToList();

            grcNhapCT.DataSource = listSearch.ToList();

        }

        private void btnBieu20_Click(object sender, EventArgs e)
        {
            FormThamSo.frm_BC_TraCuuTT_Bieu20 frmBieu20 = new FormThamSo.frm_BC_TraCuuTT_Bieu20(_madv);
            frmBieu20.ShowDialog();
        }
         
        private void btnInBaoCao_Click(object sender, EventArgs e)
        {
            string tenBV, tenTD;
            tenBV = DungChung.Bien.TenBV;
            if(cboOptionBC.SelectedIndex == 0)
            {
                tenTD = "BÁO CÁO THÔNG TIN CHI TIẾT LỊCH SỬ SỬ DỤNG THUỐC THEO NGÀY XUẤT DƯỢC";
            }
            else
            {
                tenTD = "BÁO CÁO THÔNG TIN CHI TIẾT LỊCH SỬ SỬ DỤNG THUỐC THEO BIỂU 20";
            }
            BaoCao.repBCChiTietLSSDThuoc rep;
            rep = new BaoCao.repBCChiTietLSSDThuoc(tenBV, tenTD, txtKP);

            #region xuat Excel

            string[] _arr = new string[] { "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@" };
            int[] _arrWidth = new int[] { };
            string[] _tieude = { "STT", "STT theo DMT của BYT", "Tên hoạt chất", "Tên thuốc thành phẩm", "Đường dùng, dạng bào chế", "Hàm lượng nồng độ", "Số đăng ký hoặc số GPNK", "Đơn vị tính", "Số lượng ngoại trú", "Số lượng nội trú", "Đơn giá", "Thành tiền" };
            //DungChung.Bien.MangHaiChieu = new Object[_list.OrderBy(p => p.SapXep).ThenBy(p => p.Ten_thuoc).ToList().Count + 6, 12];
            //DungChung.Bien.MangHaiChieu[1, 0] = "Tên CSYT:"; DungChung.Bien.MangHaiChieu[1, 1] = DungChung.Bien.TenCQ.ToUpper(); DungChung.Bien.MangHaiChieu[1, 4] = "Mẫu số 20/BHYT";
            //DungChung.Bien.MangHaiChieu[2, 0] = "Mã CSYT:"; DungChung.Bien.MangHaiChieu[2, 1] = DungChung.Bien.MaBV;
            //DungChung.Bien.MangHaiChieu[3, 2] = "";
            //DungChung.Bien.MangHaiChieu[4, 2] = theoquy();
            //for (int i = 0; i < _tieude.Length; i++)
            //{
            //    DungChung.Bien.MangHaiChieu[5, i] = _tieude[i];
            //}

            //int num = 6;
            //foreach (var r in _list.OrderBy(p => p.SapXep).ThenBy(p => p.Ten_thuoc).ToList())
            //{
            //    DungChung.Bien.MangHaiChieu[num, 0] = r.SoTTqd;
            //    DungChung.Bien.MangHaiChieu[num, 1] = r.MaQD;
            //    DungChung.Bien.MangHaiChieu[num, 2] = r.TenHC;
            //    DungChung.Bien.MangHaiChieu[num, 3] = r.Ten_thuoc;
            //    DungChung.Bien.MangHaiChieu[num, 4] = r.Duong_dung;
            //    DungChung.Bien.MangHaiChieu[num, 5] = r.Ham_luong;
            //    DungChung.Bien.MangHaiChieu[num, 6] = r.So_dang_ky;
            //    DungChung.Bien.MangHaiChieu[num, 7] = r.Don_vi_tinh;
            //    DungChung.Bien.MangHaiChieu[num, 8] = r.SoluongNgT;
            //    DungChung.Bien.MangHaiChieu[num, 9] = r.SoluongNT;
            //    DungChung.Bien.MangHaiChieu[num, 10] = r.Don_gia;
            //    DungChung.Bien.MangHaiChieu[num, 11] = r.Thanh_tien;
            //    num++;

            //}

            frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "", "C:\\Biểu20.xls", true, this.Name);
            #endregion

            if (cboOptionBC.SelectedIndex == 0)
            {
                var newList = (from lnx in _NXTct.Where(p => p.Ngay >= _dttu && p.Ngay <= _dtden)
                               group lnx by new { lnx.ID, lnx.Ngay, lnx.TenKP, lnx.MaBN, lnx.TenBN, lnx.DonVi, lnx.DonGia, lnx.SoLuongKD, lnx.ThanhTienKD, lnx.SoLuongXD, lnx.SoPL, lnx.SoLo, lnx.HanDung, lnx.TenKXuat, lnx.NgayVao, lnx.NgayRa } into kq
                               select new { kq.Key.ID, kq.Key.Ngay, kq.Key.TenKP, kq.Key.MaBN, kq.Key.TenBN, kq.Key.DonVi, kq.Key.DonGia, kq.Key.SoLuongKD, kq.Key.ThanhTienKD, kq.Key.SoLuongXD, kq.Key.SoPL, kq.Key.SoLo, kq.Key.HanDung, kq.Key.TenKXuat, kq.Key.NgayVao, kq.Key.NgayRa, }).ToList();
                rep.DataSource = newList.ToList();
            } else
            {
                var newList = (from lnx in _NXTct.Where(p => p.NgayRa >= _dttu && p.NgayRa <= _dtden)
                               group lnx by new { lnx.ID, lnx.Ngay, lnx.TenKP, lnx.MaBN, lnx.TenBN, lnx.DonVi, lnx.DonGia, lnx.SoLuongKD, lnx.ThanhTienKD, lnx.SoLuongXD, lnx.SoPL, lnx.SoLo, lnx.HanDung, lnx.TenKXuat, lnx.NgayVao, lnx.NgayRa } into kq
                               select new { kq.Key.ID, kq.Key.Ngay, kq.Key.TenKP, kq.Key.MaBN, kq.Key.TenBN, kq.Key.DonVi, kq.Key.DonGia, kq.Key.SoLuongKD, kq.Key.ThanhTienKD, kq.Key.SoLuongXD, kq.Key.SoPL, kq.Key.SoLo, kq.Key.HanDung, kq.Key.TenKXuat, kq.Key.NgayVao, kq.Key.NgayRa, }).ToList();
                rep.DataSource = newList.ToList();
            }

            rep.BindingData();
            rep.CreateDocument();
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();
        }

        private void cboOptionBC_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboOptionBC.SelectedIndex == 0)
            {
                var newList = (from lnx in _NXTct.Where(p => p.Ngay >= _dttu && p.Ngay <= _dtden)
                               group lnx by new { lnx.ID, lnx.Ngay, lnx.TenKP, lnx.MaBN, lnx.TenBN, lnx.DonVi, lnx.DonGia, lnx.SoLuongKD, lnx.ThanhTienKD, lnx.SoLuongXD, lnx.SoPL, lnx.SoLo, lnx.HanDung, lnx.TenKXuat, lnx.NgayVao, lnx.NgayRa } into kq
                               select new { kq.Key.ID, kq.Key.Ngay, kq.Key.TenKP, kq.Key.MaBN, kq.Key.TenBN, kq.Key.DonVi, kq.Key.DonGia, kq.Key.SoLuongKD, kq.Key.ThanhTienKD, kq.Key.SoLuongXD, kq.Key.SoPL, kq.Key.SoLo, kq.Key.HanDung, kq.Key.TenKXuat, kq.Key.NgayVao, kq.Key.NgayRa, }).ToList();
                grcNhapCT.DataSource = newList.ToList();
            } else
            {
                var newList = (from lnx in _NXTct.Where(p => p.NgayRa >= _dttu && p.NgayRa <= _dtden)
                               group lnx by new { lnx.ID, lnx.Ngay, lnx.TenKP, lnx.MaBN, lnx.TenBN, lnx.DonVi, lnx.DonGia, lnx.SoLuongKD, lnx.ThanhTienKD, lnx.SoLuongXD, lnx.SoPL, lnx.SoLo, lnx.HanDung, lnx.TenKXuat, lnx.NgayVao, lnx.NgayRa } into kq
                               select new { kq.Key.ID, kq.Key.Ngay, kq.Key.TenKP, kq.Key.MaBN, kq.Key.TenBN, kq.Key.DonVi, kq.Key.DonGia, kq.Key.SoLuongKD, kq.Key.ThanhTienKD, kq.Key.SoLuongXD, kq.Key.SoPL, kq.Key.SoLo, kq.Key.HanDung, kq.Key.TenKXuat, kq.Key.NgayVao, kq.Key.NgayRa, }).ToList();
                grcNhapCT.DataSource = newList.ToList();
            }
            
        }
    }
}