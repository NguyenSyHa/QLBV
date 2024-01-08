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
    public partial class frm_BC_TheoDoiTTKCB_30009 : DevExpress.XtraEditors.XtraForm
    {
        public frm_BC_TheoDoiTTKCB_30009()
        {
            InitializeComponent();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        List<Khoa> _lKhoa = new List<Khoa>();
        private void frm_BC_TheoDoiTTKCB_30009_Load(object sender, EventArgs e)
        {
            dtTuNgay.DateTime = DateTime.Now;
            dtDenNgay.DateTime = DateTime.Now;
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var kphong = data.KPhongs.Where(p => p.PLoai == "Lâm sàng").ToList();
            if (kphong.Count > 0)
            {
                Khoa themmoi1 = new Khoa();
                themmoi1.TenKP = "Chọn tất cả";
                themmoi1.MaKP = 0;
                themmoi1.Chon = true;
                _lKhoa.Add(themmoi1);
                foreach (var a in kphong)
                {
                    Khoa themmoi = new Khoa();
                    themmoi.TenKP = a.TenKP;
                    themmoi.MaKP = a.MaKP;
                    themmoi.Chon = true;
                    _lKhoa.Add(themmoi);
                }
                grcKhoaphong.DataSource = _lKhoa.ToList();
            }
        }

        private void btnInBC_Click(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            DateTime tungay = DungChung.Ham.NgayTu(dtTuNgay.DateTime);
            DateTime denngay = DungChung.Ham.NgayDen(dtDenNgay.DateTime);
            List<Khoa> dskhoa = new List<Khoa>();
            dskhoa = _lKhoa.Where(p => p.Chon == true && p.MaKP > 0).ToList();
            var qtu = (from tu in data.TamUngs.Where(p => p.PhanLoai == 0)
                       group tu by new { tu.MaBNhan } into kq
                       select new
                       {
                           kq.Key.MaBNhan,
                           SotienTU = kq.Sum(p => p.SoTien)
                       }).ToList();

            var qvienphi = (from vp in data.VienPhis.Where(p => p.NgayTT >= tungay && p.NgayTT <= denngay)
                            join vpct in data.VienPhicts.Where(p => p.ThanhToan == 0) on vp.idVPhi equals vpct.idVPhi
                            join bn in data.BenhNhans.Where(p => p.NoiTru == 1) on vp.MaBNhan equals bn.MaBNhan
                            group new { vp, vpct } by new { vp.MaBNhan } into kq
                            select new { kq.Key.MaBNhan, ThanhToan = kq.Sum(p => p.vpct.TienBN) }).ToList();

            var qbn = (from tu in data.TamUngs.Where(p => p.PhanLoai == 1 || p.PhanLoai == 2)
                       join bn in data.BenhNhans.Where(p => p.DTuong.ToLower().Contains("dịch vụ")) on tu.MaBNhan equals bn.MaBNhan
                       join vv in data.VaoViens on bn.MaBNhan equals vv.MaBNhan
                       join rv in data.RaViens on vv.MaBNhan equals rv.MaBNhan
                       select new
                       {
                           bn.MaBNhan,
                           bn.TenBNhan,
                           bn.NoiTru,
                           bn.Tuoi,
                           GTinh = bn.GTinh == 0 ? "Nữ" : "Nam",
                           bn.DChi,
                           tu.SoHD,
                           NgayVao = vv.NgayVao,
                           rv.NgayRa,
                           rv.MaKP,
                           TienTra = tu.PhanLoai == 2 ? tu.TienChenh : 0,
                           ThuThem = (tu.PhanLoai == 1) ? tu.TienChenh : 0
                       }).ToList();
            var test = DungChung.Ham.TuoitheoThang(data, 239222, "12-30");
            var query = (from bn in qbn
                         join t in qtu on bn.MaBNhan equals t.MaBNhan into k
                         from k1 in k.DefaultIfEmpty()
                         join vp in qvienphi on bn.MaBNhan equals vp.MaBNhan
                         join kp in dskhoa on bn.MaKP equals kp.MaKP
                         group new { k1, bn, vp, kp } by new { bn.MaBNhan, bn.TenBNhan, bn.Tuoi, bn.GTinh, bn.NgayRa, bn.NgayVao, kp.TenKP, MaBNTU = k1 == null ? null : k1.MaBNhan } into kq
                         select new
                         {
                             kq.Key.TenKP,
                             kq.Key.MaBNhan,
                             kq.Key.TenBNhan,
                             Tuoi = (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049") ? DungChung.Ham.TuoitheoThang(data, kq.Key.MaBNhan, "12-30") : kq.Key.Tuoi.ToString(),
                             kq.Key.GTinh,
                             kq.Key.NgayVao,
                             kq.Key.NgayRa,
                             TienUng = kq.Key.MaBNTU == null ? null : kq.Sum(p => p.k1.SotienTU),
                             TT = kq.Sum(p => p.vp.ThanhToan),
                             TienTra = kq.Sum(p => p.bn.TienTra) == 0 ? "" : String.Format("{0:##,##0}", kq.Sum(p => p.bn.TienTra)),
                             ThuThem = kq.Sum(p => p.bn.ThuThem) == 0 ? "" : String.Format("{0:##,##0}", kq.Sum(p => p.bn.ThuThem))
                         }).OrderBy(p => p.TenKP).ThenBy(p => p.TenBNhan).ToList();

            BaoCao.rep_BC_TheoDoiTTKCB_30009 rep1 = new BaoCao.rep_BC_TheoDoiTTKCB_30009();
            frmIn frm = new frmIn();
            rep1.lblSoBL.Text = "Từ ngày " + tungay.ToString("dd/MM/yyyy") + " đến ngày " + denngay.ToString("dd/MM/yyyy");
            rep1.DataSource = query.ToList();
            rep1.BindingData();
            rep1.CreateDocument();
            frm.prcIN.PrintingSystem = rep1.PrintingSystem;
            frm.ShowDialog();
        }

        #region class Khoa
        private class Khoa
        {
            public bool Chon { get; set; }
            public int MaKP { get; set; }
            public string TenKP { get; set; }
        }
        #endregion

        private void grvKhoaphong_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "colchon")
            {
                if (grvKhoaphong.GetFocusedRowCellValue("TenKP") != null)
                {
                    string Ten = grvKhoaphong.GetFocusedRowCellValue("TenKP").ToString();

                    if (Ten == "Chọn tất cả")
                    {
                        if (_lKhoa.First().Chon == true)
                        {
                            foreach (var a in _lKhoa)
                            {
                                a.Chon = false;
                            }
                        }
                        else
                        {
                            foreach (var a in _lKhoa)
                            {
                                a.Chon = true;
                            }
                        }
                        grcKhoaphong.DataSource = "";
                        grcKhoaphong.DataSource = _lKhoa.ToList();
                    }
                }
            }
        }
    }
}