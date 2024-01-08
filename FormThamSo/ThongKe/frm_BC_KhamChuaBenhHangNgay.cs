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
    public partial class frm_BC_KhamChuaBenhHangNgay : DevExpress.XtraEditors.XtraForm
    {
        public frm_BC_KhamChuaBenhHangNgay()
        {
            InitializeComponent();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frm_BC_KhamChuaBenhHangNgay_Load(object sender, EventArgs e)
        {
            MinimizeBox = false;
            MaximizeBox = false;
            dateTuNgay.DateTime = DateTime.Now;
            dateDenNgay.DateTime = DateTime.Now;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            List<KhamChuaBenh> _listContent = new List<KhamChuaBenh>();
            KhamChuaBenh kcb = new KhamChuaBenh();
            DateTime _tungay = DungChung.Ham.NgayTu(dateTuNgay.DateTime);
            DateTime _denngay = DungChung.Ham.NgayDen(dateDenNgay.DateTime);
            if (_denngay >= _tungay)
            {
                #region số lượt khám bệnh
                var _luotkham = (from bnkb in data.BNKBs
                                 join bn in data.BenhNhans on bnkb.MaBNhan equals bn.MaBNhan
                                 join kp in data.KPhongs.Where(p=>p.PLoai == DungChung.Bien.st_PhanLoaiKP.PhongKham) 
                                 on bnkb.MaKP equals kp.MaKP 
                                 select new
                                 {
                                     bnkb.NgayKham,
                                     bnkb.MaBNhan,
                                     bn.CapCuu,   
                                     bn.ChuyenKhoa,
                                     bnkb.PhuongAn,
                                     bn.NoiTru
                                 }).Where(p => p.NgayKham >= _tungay && p.NgayKham <= _denngay).ToList();

                kcb = new KhamChuaBenh();
                kcb.Stt = 1;
                kcb.TenNhom = "Số lượt khám bệnh";
                kcb.TenChiTiet = "";
                kcb.DonVi = "lượt";
                kcb.SoLuong = _luotkham.Count;
                kcb.GhiChu = "";
                _listContent.Add(kcb);
                #endregion
                #region Khám cấp cứu
                kcb = new KhamChuaBenh();
                kcb.Stt = 2;
                kcb.TenNhom = "Khám cấp cứu";
                kcb.TenChiTiet = "";
                kcb.DonVi = "lượt";
                kcb.SoLuong = _luotkham.Where(p => p.CapCuu == 1).Count();
                kcb.GhiChu = "";
                _listContent.Add(kcb);
                #endregion
                #region Tai nạn
                var _bnTaiNan = _luotkham.Where(p => p.ChuyenKhoa == "Tai nạn giao thông" || p.ChuyenKhoa == "Đường bộ" || p.ChuyenKhoa == "Đường sắt"
                                           || p.ChuyenKhoa == "Đường sông" || p.ChuyenKhoa == "Tai nạn lao động" || p.ChuyenKhoa == "Tai nạn sinh hoạt"
                                           || p.ChuyenKhoa == "Đánh nhau" || p.ChuyenKhoa == "Tự tử" || p.ChuyenKhoa == "Ngộ độc" || p.ChuyenKhoa == "Đuối nước"
                                           || p.ChuyenKhoa == "Bạo lực gia đình" || p.ChuyenKhoa == "Khác").ToList();

                kcb = new KhamChuaBenh();
                kcb.Stt = 3;
                kcb.TenNhom = "Số người tai nạn: Trong đó";
                kcb.TenChiTiet = "";
                kcb.DonVi = "Người";
                kcb.SoLuong = _bnTaiNan.GroupBy(p => p.MaBNhan).Count();
                kcb.GhiChu = "";
                _listContent.Add(kcb);

                kcb = new KhamChuaBenh();
                kcb.Stt = 3;
                kcb.TenNhom = "Số người tai nạn: Trong đó";
                kcb.TenChiTiet = "Tai nạn giao thông";
                kcb.DonVi = "Người";
                kcb.SoLuong = _bnTaiNan.Where(p => p.ChuyenKhoa == "Tai nạn giao thông" || p.ChuyenKhoa == "Đường bộ" || p.ChuyenKhoa == "Đường sắt"
                                           || p.ChuyenKhoa == "Đường sông").GroupBy(p => p.MaBNhan).Count();
                kcb.GhiChu = "";
                _listContent.Add(kcb);

                kcb = new KhamChuaBenh();
                kcb.Stt = 3;
                kcb.TenNhom = "Số người tai nạn: Trong đó";
                kcb.TenChiTiet = "Tai nạn lao động";
                kcb.DonVi = "Người";
                kcb.SoLuong = _bnTaiNan.Where(p => p.ChuyenKhoa == "Tai nạn lao động").GroupBy(p => p.MaBNhan).Count();
                kcb.GhiChu = "";
                _listContent.Add(kcb);

                kcb = new KhamChuaBenh();
                kcb.Stt = 3;
                kcb.TenNhom = "Số người tai nạn: Trong đó";
                kcb.TenChiTiet = "Tai nạn sinh hoạt";
                kcb.DonVi = "Người";
                kcb.SoLuong = _bnTaiNan.Where(p => p.ChuyenKhoa == "Tai nạn sinh hoạt").GroupBy(p => p.MaBNhan).Count();
                kcb.GhiChu = "";
                _listContent.Add(kcb);

                kcb = new KhamChuaBenh();
                kcb.Stt = 3;
                kcb.TenNhom = "Số người tai nạn: Trong đó";
                kcb.TenChiTiet = "Tai nạn khác";
                kcb.DonVi = "Người";
                kcb.SoLuong = _bnTaiNan.Where(p => p.ChuyenKhoa != "Tai nạn giao thông" && p.ChuyenKhoa != "Đường bộ" && p.ChuyenKhoa != "Đường sắt"
                                           && p.ChuyenKhoa != "Đường sông" && p.ChuyenKhoa != "Tai nạn lao động" && p.ChuyenKhoa != "Tai nạn sinh hoạt").GroupBy(p => p.MaBNhan).Count();
                kcb.GhiChu = "";
                _listContent.Add(kcb);
                #endregion
                #region số người vào viện
                var _bnVaoVien = (from vv in data.VaoViens
                                  join bn in data.BenhNhans
                                  on vv.MaBNhan equals bn.MaBNhan
                                  select new
                                  {
                                      vv.NgayVao,
                                      vv.MaBNhan, 
                                      bn.NoiTru
                                  }).Where(p => p.NgayVao >= _tungay && p.NgayVao <= _denngay).ToList();

                kcb = new KhamChuaBenh();
                kcb.Stt = 4;
                kcb.TenNhom = "Số người vào viện";
                kcb.TenChiTiet = "";
                kcb.DonVi = "Người";
                kcb.SoLuong = _bnVaoVien.Count;
                kcb.GhiChu = "";
                _listContent.Add(kcb);
                #endregion
                #region số người bệnh hiện có
                var q5 = (from bn in data.BenhNhans.Where(p=>p.NoiTru == 1 || (p.NoiTru == 0 && p.DTNT == true) )
                          join vv in data.VaoViens.Where(p => p.NgayVao <= _denngay) on bn.MaBNhan equals vv.MaBNhan
                          join rv in data.RaViens on bn.MaBNhan equals rv.MaBNhan into kq
                          from kq1 in kq.DefaultIfEmpty()
                          where (kq1 == null || (kq1 != null && kq1.NgayRa >= _tungay)) //_tungay
                          select new { bn.MaBNhan }).ToList();

                kcb = new KhamChuaBenh();
                kcb.Stt = 5;
                kcb.TenNhom = "Số người bệnh hiện có";
                kcb.TenChiTiet = "";
                kcb.DonVi = "Người";
                kcb.SoLuong = q5.Count;
                kcb.GhiChu = "";
                _listContent.Add(kcb);
                #endregion
                #region Tổng số ca đẻ
                var bnpttt = (from bn in data.BenhNhans
                            join dt in data.DThuocs on bn.MaBNhan equals dt.MaBNhan
                            join dtct in data.DThuoccts//.Where(p=>p.IDCD == null) 
                              on dt.IDDon equals dtct.IDDon                           
                            join dv in data.DichVus on dtct.MaDV equals dv.MaDV
                            join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                            select new
                            {
                               dt.NgayKe,
                               dtct.NgayNhap,
                               TenDV = dv.TenDV.ToLower(),
                               tn.TenRG,
                            }).Where(p => p.NgayNhap >= _tungay && p.NgayNhap <= _denngay).ToList();
                              //.Where(p => p.TenDV.Contains("Đỡ đẻ") || p.TenDV.Contains("Phẫu thuật lấy thai")).ToList();
                var bnpttt2 = (from bn in data.BenhNhans
                            join cls in data.CLS on bn.MaBNhan equals cls.MaBNhan
                            join cd in data.ChiDinhs.Where(p=>p.Status == 1) on cls.IdCLS equals cd.IDCD
                            join dv in data.DichVus on cd.MaDV equals dv.MaDV
                            join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                            select new
                            {
                                cls.NgayTH,
                                TenDV =  dv.TenDV.ToLower(),
                                tn.TenRG
                            }).Where(p => p.NgayTH >= _tungay && p.NgayTH <= _denngay).ToList();
                                  //            .Where(p => p.TenDV.Contains("Đỡ đẻ") || p.TenDV.Contains("Phẫu thuật lấy thai")).ToList();

                kcb = new KhamChuaBenh();
                kcb.Stt = 6;
                kcb.TenNhom = "Tổng số ca đẻ";
                kcb.TenChiTiet = "";
                kcb.DonVi = "Ca";
                kcb.SoLuong = bnpttt.Where(p => p.TenDV.Contains("đỡ đẻ") || p.TenDV.Contains("phẫu thuật lấy thai")).Count();// +bnpttt2.Where(p => p.TenDV.Contains("đỡ đẻ") || p.TenDV.Contains("phẫu thuật lấy thai")).Count();
                kcb.GhiChu = "";
                _listContent.Add(kcb);

                kcb = new KhamChuaBenh();
                kcb.Stt = 6;
                kcb.TenNhom = "Tổng số ca đẻ";
                kcb.TenChiTiet = "Đẻ thường";
                kcb.DonVi = "Ca";
                kcb.SoLuong = bnpttt.Where(p => p.TenDV.Contains("đỡ đẻ")).Count();// +bnpttt2.Where(p => p.TenDV.Contains("đỡ đẻ")).Count();
                kcb.GhiChu = "";
                _listContent.Add(kcb);

                kcb = new KhamChuaBenh();
                kcb.Stt = 6;
                kcb.TenNhom = "Tổng số ca đẻ";
                kcb.TenChiTiet = "Đẻ mổ";
                kcb.DonVi = "Ca";
                kcb.SoLuong = bnpttt.Where(p => p.TenDV.Contains("phẫu thuật lấy thai")).Count();// +bnpttt2.Where(p => p.TenDV.Contains("phẫu thuật lấy thai")).Count();
                kcb.GhiChu = "";
                _listContent.Add(kcb);
                #endregion
                #region Tổng số phẫu thuật
                //var q7 = (from cd in data.ChiDinhs
                //          join dv in data.DichVus on cd.MaDV equals dv.MaDV
                //          join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                //          join dtct in data.DThuoccts on cd.IDCD equals dtct.IDCD
                //          join cls in data.CLS on cd.IdCLS equals cls.IdCLS
                //          select new { cd.Status, tn.TenRG, dv.Loai, dtct.IDCD, cls.NgayTH, cls.MaBNhan }).Where(p => p.Status == 1 && p.IDCD != null).ToList();

                kcb = new KhamChuaBenh();
                kcb.Stt = 7;
                kcb.TenNhom = "Tổng số phẫu thuật";
                kcb.TenChiTiet = "";
                kcb.DonVi = "Ca";
                kcb.SoLuong = bnpttt.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.PhauThuat).Count();// +bnpttt2.Where(p => p.TenRG == "Phẫu thuật").Count();
                kcb.GhiChu = "";
                _listContent.Add(kcb);

                kcb = new KhamChuaBenh();
                kcb.Stt = 7;
                kcb.TenNhom = "Tổng số phẫu thuật";
                kcb.TenChiTiet = "Tổng số phẫu thuật nội soi";
                kcb.DonVi = "Ca";
                kcb.SoLuong = bnpttt.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.PhauThuat).Where(p => p.TenDV.Contains("nội soi")).Count();// +bnpttt2.Where(p => p.TenRG == "Phẫu thuật").Where(p => p.TenDV.Contains("nội soi")).Count();
                kcb.GhiChu = "";
                _listContent.Add(kcb);
                #endregion

                #region Tổng số ca tử vong
                var q8 = data.RaViens.Where(p => p.NgayRa >= _tungay && p.NgayRa <= _denngay && p.KetQua == "Tử vong").ToList();
                kcb = new KhamChuaBenh();
                kcb.Stt = 8;
                kcb.TenNhom = "Tổng số ca tử vong";
                kcb.TenChiTiet = "";
                kcb.DonVi = "Ca";
                kcb.SoLuong = q8.Count();
                kcb.GhiChu = "";
                _listContent.Add(kcb);
                #endregion
                #region Tình hình dịch bệnh
                kcb = new KhamChuaBenh();
                kcb.Stt = 9;
                kcb.TenNhom = "Tình hình dịch bệnh";
                kcb.TenChiTiet = "";
                kcb.DonVi = "Ca";
                //kcb.SoLuong = q8.Count();
                kcb.GhiChu = "";
                _listContent.Add(kcb);

                kcb = new KhamChuaBenh();
                kcb.Stt = 9;
                kcb.TenNhom = "Tình hình dịch bệnh";
                kcb.TenChiTiet = "Sốt xuất huyết";
                kcb.DonVi = "Ca";
                //kcb.SoLuong = q8.Count();
                kcb.GhiChu = "";
                _listContent.Add(kcb);

                kcb = new KhamChuaBenh();
                kcb.Stt = 9;
                kcb.TenNhom = "Tình hình dịch bệnh";
                kcb.TenChiTiet = "Tay chân miệng";
                kcb.DonVi = "Ca";
                //kcb.SoLuong = q8.Count();
                kcb.GhiChu = "";
                _listContent.Add(kcb);
                #endregion
                #region Các trường hợp đặc biệt
                kcb = new KhamChuaBenh();
                kcb.Stt = 10;
                kcb.TenNhom = "Các trường hợp đặc biệt";
                kcb.TenChiTiet = "";
                kcb.DonVi = "Ca";
                //kcb.SoLuong = q8.Count();
                kcb.GhiChu = "";
                _listContent.Add(kcb);
                #endregion

                BaoCao.Rep_BC_KhamChuaBenhHangNgay rep = new BaoCao.Rep_BC_KhamChuaBenhHangNgay();
                frmIn frm = new frmIn();
                rep.lbl_Title.Text = "(Từ ngày " + _tungay.ToString("dd/MM/yyyy") + " đến ngày " + _denngay.ToString("dd/MM/yyyy") + ")";
                rep.DataSource = _listContent.OrderBy(p => p.Stt);
                rep.BindingData();
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Đến ngày không thể nhỏ hơn Từ ngày.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }

    public class KhamChuaBenh
    {
        public int Stt { get; set; }
        public string TenNhom { get; set; }
        public string TenChiTiet { get; set; }
        public string DonVi { get; set; }
        public int? SoLuong { get; set; }
        public string GhiChu { get; set; }
    }
}