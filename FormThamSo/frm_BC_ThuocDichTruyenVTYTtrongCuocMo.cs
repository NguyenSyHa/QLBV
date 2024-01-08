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
    public partial class frm_BC_ThuocDichTruyenVTYTtrongCuocMo : DevExpress.XtraEditors.XtraForm
    {
        public frm_BC_ThuocDichTruyenVTYTtrongCuocMo()
        {
            InitializeComponent();
        }

        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

        private class KPhong
        {
            private string TenKP;
            private int MaKP;
            private bool Chon;
            public string tenkp
            { set { TenKP = value; } get { return TenKP; } }
            public int makp
            { set { MaKP = value; } get { return MaKP; } }
            public bool chon
            { set { Chon = value; } get { return Chon; } }
        }

        List<KPhong> _Kphong = new List<KPhong>();

        private void frm_BC_ThuocDichTruyenVTYTtrongCuocMo_Load(object sender, EventArgs e)
        {
            lupTuNgay.Focus();
            lupTuNgay.DateTime = System.DateTime.Now;
            lupDenNgay.DateTime = System.DateTime.Now;

            var kphong = (from kp in data.KPhongs.Where(p => p.PLoai == "Lâm sàng" || p.PLoai == DungChung.Bien.st_PhanLoaiKP.PhongKham)
                          select new { kp.TenKP, kp.MaKP, kp.PLoai }).OrderBy(p=>p.PLoai).ToList();
            if (kphong.Count > 0)
            {
                KPhong themmoi1 = new KPhong();
                themmoi1.tenkp = "Chọn tất cả";
                themmoi1.makp = 0;
                themmoi1.chon = true;
                _Kphong.Add(themmoi1);
                foreach (var a in kphong)
                {
                    KPhong themmoi = new KPhong();
                    themmoi.tenkp = a.TenKP;
                    themmoi.makp = a.MaKP;
                    themmoi.chon = true;
                    _Kphong.Add(themmoi);
                }
                grcKhoaphong.DataSource = _Kphong.ToList();
            }
        }

        private void grvKhoaphong_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "Chọn")
            {
                if (grvKhoaphong.GetFocusedRowCellValue("tenkp") != null)
                {
                    string Ten = grvKhoaphong.GetFocusedRowCellValue("tenkp").ToString();

                    if (Ten == "Chọn tất cả")
                    {
                        if (_Kphong.First().chon == true)
                        {
                            foreach (var a in _Kphong)
                            {
                                a.chon = false;
                            }
                        }
                        else
                        {
                            foreach (var a in _Kphong)
                            {
                                a.chon = true;
                            }
                        }
                        grcKhoaphong.DataSource = "";
                        grcKhoaphong.DataSource = _Kphong.ToList();
                    }
                }
            }
        }

        private bool KTtaoBc()
        {

            if (lupTuNgay.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn ngày bắt đầu in báo cáo");
                lupTuNgay.Focus();
                return false;
            }
            if (lupDenNgay.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn ngày kết thúc in báo cáo");
                lupDenNgay.Focus();
                return false;
            }
            if (DungChung.Ham.NgayDen(lupDenNgay.DateTime) < DungChung.Ham.NgayTu(lupTuNgay.DateTime))
            {
                MessageBox.Show("Ngày đến không thể nhỏ hơn ngày từ.");
                lupDenNgay.Focus();
                return false;
            }
            //if (string.IsNullOrEmpty(txtTieuDe.Text.Trim()))
            //{
            //    MessageBox.Show("Chưa nhập tiêu đề báo cáo");
            //    txtTieuDe.Focus();
            //    return false;
            //}
            else return true;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Lấy theo ngày thanh toán theo yêu cầu của khách hàng
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (KTtaoBc())
            {               
                DateTime tungay = DungChung.Ham.NgayTu(lupTuNgay.DateTime);
                DateTime denngay = DungChung.Ham.NgayDen(lupDenNgay.DateTime);
                List<KPhong> _lKhoaP = new List<KPhong>();
                _lKhoaP = _Kphong.Where(p => p.makp > 0).Where(p => p.chon == true).ToList();
                var qdv = (from dv in data.DichVus
                           join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                           join n in data.NhomDVs on tn.IDNhom equals n.IDNhom
                           select new { dv.MaDV, dv.PLoai, tn.TenRG, n.IDNhom }).ToList();

                // chỉ lấy nhưng đơn thuốc cho những bệnh nhân phẫu thuật
                var dsbn = (from bn in data.BenhNhans
                            join vp in data.VienPhis.Where(p=>p.NgayTT >= tungay && p.NgayTT <= denngay)on bn.MaBNhan equals vp.MaBNhan
                            join dt in data.DThuocs  on bn.MaBNhan equals dt.MaBNhan
                            join dtct in data.DThuoccts on dt.IDDon equals dtct.IDDon  
                            join kb in data.BNKBs on dtct.IDKB equals kb.IDKB into kq from kq1 in kq.DefaultIfEmpty()
                            select new { bn.TenBNhan, dtct.DonGia, dtct.DonVi, dtct.TrongBH, dtct.SoLuong, dtct.ThanhTien, bn.MaBNhan, dtct.MaDV, dtct.IDCD, dt.MaKP, dt.NgayKe, dtct.IDKB, ChanDoan = kq1 == null ? "" : kq1.ChanDoan, BenhKhac = kq1 == null ? "" : kq1.BenhKhac }).ToList();
                List<Content> q1 = new List<Content>();
                if (rdChon.SelectedIndex == 0)
                {
                    var dsbn1 = (from bn in dsbn
                                 join dv in qdv.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.PhauThuat) on bn.MaDV equals dv.MaDV
                                 select new { bn.MaBNhan, bn.IDCD, bn.MaKP, ChanDoan = bn.ChanDoan + bn.BenhKhac }).Distinct().ToList();

                    // những dịch vụ không thực hiện tại phòng khám
                    var qcls = (from cls in data.CLS.Where(p => p.Status == 1)
                                join cd in data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                                select new { cls.MaKPth, cd.IDCD }).ToList();

                    var dsbn2 = (from dt in dsbn1
                                 join cls in qcls on dt.IDCD equals cls.IDCD into kq
                                 from kq1 in kq.DefaultIfEmpty()
                                 select new
                                 {
                                     dt.MaBNhan,
                                     ChanDoan = DungChung.Ham.FreshString( dt.ChanDoan),
                                     MaKP = kq1 == null ? dt.MaKP : kq1.MaKPth,
                                 }).ToList();

                    var dsbn3 = (from bn in dsbn2 
                                 join kp in _lKhoaP on bn.MaKP equals kp.makp
                                 group new { bn, kp} by new {bn.MaBNhan} into kq 
                                 select new { 
                                 kq.Key.MaBNhan,
                                 tenkp = string.Join(",", kq.Select(p => p.kp.tenkp).Distinct().ToArray()),
                                 ChanDoan = string.Join(",", kq.Where(p => p.bn.ChanDoan != null && p.bn.ChanDoan != "").Select(p => p.bn.ChanDoan).Distinct().ToArray()),
                                 }).ToList();


                    // lấy ra thuốc, VTYT, dịch truyền
                    var qdthuoc1 = (from bnnhan in dsbn3
                                   // join kp in _lKhoaP on bnnhan.MaKP equals kp.makp
                                    join dt in dsbn on bnnhan.MaBNhan equals dt.MaBNhan
                                    // into kq from kq1 in kq.DefaultIfEmpty()                              
                                    select new
                                    {
                                        dt.MaBNhan,
                                        //bnnhan.MaKP,
                                        dt.NgayKe,
                                        dt.MaDV,
                                        dt.TrongBH,
                                        dt.SoLuong,
                                        dt.DonGia,
                                        dt.DonVi,
                                        dt.ThanhTien,
                                        dt.TenBNhan,
                                        ChanDoan = bnnhan.ChanDoan,
                                        bnnhan.tenkp,
                                        //kb.IDKB
                                    }).Where(p => p.TrongBH == 2).ToList();

                    q1 = (from dt in qdthuoc1
                          join dv in qdv on dt.MaDV equals dv.MaDV
                          group new { dt, dv } by new {dt.tenkp, dt.TenBNhan, dt.MaBNhan, dt.ChanDoan } into kq
                          select new Content
                          {
                              khoa = kq.Key.tenkp,
                              MaBNhan = kq.Key.MaBNhan,
                              benhnhan = kq.Key.TenBNhan,
                              chandoan = kq.Key.ChanDoan,
                              TienThuoc = kq.Where(p => p.dv.IDNhom == 4 || p.dv.IDNhom == 5 || p.dv.IDNhom == 6).Where(p => p.dv.TenRG != DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuocThuong_DT).Sum(p => p.dt.ThanhTien),
                              TienDTruyen = kq.Where(p => p.dv.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuocThuong_DT).Sum(p => p.dt.ThanhTien),
                              TienVTYT = kq.Where(p => p.dv.IDNhom == 10 || p.dv.IDNhom == 11).Sum(p => p.dt.ThanhTien),
                          }).ToList();
                }
                else // lấy tất cả chi phí ngoài danh mục 
                {
                    var dsbn1 = (from bn in dsbn
                                 join dv in qdv on bn.MaDV equals dv.MaDV
                                 select new { bn.MaBNhan,bn.MaKP, ChanDoan = bn.ChanDoan + bn.BenhKhac }).Distinct().ToList();


                    // lấy ra thuốc, VTYT, dịch truyền
                    var qdthuoc1 = (from bnnhan in dsbn1
                                    join dt in dsbn on bnnhan.MaBNhan equals dt.MaBNhan
                                    // into kq from kq1 in kq.DefaultIfEmpty()                              
                                    select new
                                    {
                                        dt.MaBNhan,
                                        bnnhan.MaKP,
                                        dt.NgayKe,
                                        dt.MaDV,
                                        dt.TrongBH,
                                        dt.SoLuong,
                                        dt.DonGia,
                                        dt.DonVi,
                                        dt.ThanhTien,
                                        dt.TenBNhan,
                                        ChanDoan = bnnhan.ChanDoan,
                                        //kb.IDKB
                                    }).Where(p => p.TrongBH == 2).ToList();
                    q1 = (from dt in qdthuoc1
                          join dv in qdv on dt.MaDV equals dv.MaDV
                          join kp in _lKhoaP on dt.MaKP equals kp.makp
                          group new { dt, dv, kp } by new { dt.MaKP, kp.tenkp, dt.TenBNhan, dt.MaBNhan, dt.ChanDoan } into kq
                          select new Content
                          {
                              khoa = kq.Key.tenkp,
                              MaBNhan = kq.Key.MaBNhan,
                              benhnhan = kq.Key.TenBNhan,
                              chandoan = kq.Key.ChanDoan,
                              TienThuoc = kq.Where(p => p.dv.IDNhom == 4 || p.dv.IDNhom == 5 || p.dv.IDNhom == 6).Where(p => p.dv.TenRG != DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuocThuong_DT).Sum(p => p.dt.ThanhTien),
                              TienDTruyen = kq.Where(p => p.dv.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuocThuong_DT).Sum(p => p.dt.ThanhTien),
                              TienVTYT = kq.Where(p => p.dv.IDNhom == 10 || p.dv.IDNhom == 11).Sum(p => p.dt.ThanhTien),
                          }).ToList();
                }

                foreach (var item in q1)
                {
                    item.TTien = item.TienVTYT + item.TienThuoc + item.TienDTruyen;                  
                }

                BaoCao.Rep_BC_ThuocDichTruyenVTYTtrongCuocMo rep = new BaoCao.Rep_BC_ThuocDichTruyenVTYTtrongCuocMo();
                frmIn frm = new frmIn();
                rep.lblTuNgayDenNgay.Text = "(Báo cáo từ ngày " + tungay.ToString("dd/MM/yyyy") + " đến ngày " + denngay.ToString("dd/MM/yyyy") + ")";
                if (string.IsNullOrEmpty(txtTieuDe.Text.Trim()))
                {
                    rep.lblTieuDe.Text = "THUỐC, DỊCH TRUYỀN, VTYT TRONG CUỘC MỔ";              
                }
                else
                rep.lblTieuDe.Text = txtTieuDe.Text.ToUpper();
                rep.DataSource = q1.OrderBy(p => p.MaBNhan).ThenBy(p => p.benhnhan).ToList();
                rep.BindingData();
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
            }
        }
        
    }
    public class Content
    {
        public int MaBNhan { get; set; }
        public string khoa { get; set; }
        public string benhnhan { get; set; }
        public string chandoan { get; set; }
        public double? TienThuoc { get; set; }
        public double? TienDTruyen { get; set; }
        public double? TienVTYT { get; set; }
        public double? TTien { get; set; }
    }
}