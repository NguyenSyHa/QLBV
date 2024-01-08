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
    public partial class frm_DSBenhNhanCC_01071 : DevExpress.XtraEditors.XtraForm
    {
        public frm_DSBenhNhanCC_01071()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void frm_DSBenhNhanCC_01071_Load(object sender, EventArgs e)
        {
            var _lKP = _data.KPhongs.Where(p => p.PLoai == "Phòng khám").ToList();
            lupKPhong.Properties.DataSource = _lKP;
            luptungay.Focus();
            luptungay.DateTime = System.DateTime.Today;
            DateTime denngay = luptungay.DateTime.AddHours(23).AddMinutes(59).AddSeconds(59);
            lupdenngay.DateTime = denngay;
            cboDtuong.SelectedIndex = 2;
            rdhtthanhtoan.SelectedIndex = 2;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            DateTime TuNgay = luptungay.DateTime;
            DateTime NgayDen = lupdenngay.DateTime;
            int _MaKP = 0;
            string Dtuong = cboDtuong.Text;
            int HTThanhToan = rdhtthanhtoan.SelectedIndex;
            _MaKP = Convert.ToInt32(lupKPhong.EditValue);
            var _lidkb = (from bn in _data.BenhNhans.Where(p => p.CapCuu == 1).Where(p => Dtuong == "Cả hai" ? true : p.DTuong == Dtuong)
                          join kq in _data.BNKBs.Where(p => p.NgayKham >= TuNgay && p.NgayKham <= NgayDen).Where(p => p.MaKP == _MaKP) on bn.MaBNhan equals kq.MaBNhan
                          join ttbx in _data.TTboXungs.Where(p => HTThanhToan == 2 ? true : p.HTThanhToan == HTThanhToan) on bn.MaBNhan equals ttbx.MaBNhan
                          group new { kq, bn } by new { kq.MaKP, kq.MaBNhan, MakpKham = bn.MaKP } into ks
                          select new
                          {
                              ks.Key.MaBNhan,
                              ks.Key.MaKP,
                              ks.Key.MakpKham,
                              IDKB = ks.Key.MaKP == ks.Key.MakpKham ? ks.Min(p => p.kq.IDKB) : ks.Max(p => p.kq.IDKB)
                          }).ToList();
            var _lTTBN1 = (from bn in _data.BenhNhans.Where(p => p.CapCuu == 1)
                           join kq in _data.BNKBs.Where(p => p.NgayKham >= TuNgay && p.NgayKham <= NgayDen).Where(p => p.MaKP == _MaKP) on bn.MaBNhan equals kq.MaBNhan
                           join vp in _data.VienPhis.Where(p => p.NgayTT >= TuNgay && p.NgayTT <= NgayDen) on bn.MaBNhan equals vp.MaBNhan into k
                           from k1 in k.DefaultIfEmpty()
                           select new
                           {
                               kq.IDKB,
                               bn.MaBNhan,
                               bn.TenBNhan,
                               bn.DTuong,
                               bn.NNhap,
                               //Tuoi = DungChung.Ham.TuoitheoThang(_data, bn.MaBNhan, DungChung.Bien.formatAge),
                               bn.TChung,
                               bn.DChi,
                               kq.MaKPdt,
                               kq.PhuongAn,
                               GTinh = bn.GTinh == 1 ? "Nam" : "Nữ",
                               MaICD = kq == null ? "" : kq.MaICD,
                               ChanDoan = kq == null ? "" : kq.ChanDoan,
                               KetQua = kq.PhuongAn == 2 ? "Chuyển viện" : (kq.PhuongAn == 3 ? "Chuyển phòng khám" : ((kq.PhuongAn == 1) ? "Vào viện":(k1 != null ? "Về nhà" : ((kq.PhuongAn == 1) ? "Vào viện" : (kq.PhuongAn == 3 ? "Chuyển phòng khám" : "Bệnh nhân lưu")))))
                           }).ToList();
            var _lkpchuyen = (from a in _lTTBN1.Where(p => p.PhuongAn == 3)
                              join b in _data.KPhongs on a.MaKPdt equals b.MaKP
                              group new { a, b } by new { a.MaBNhan, a.MaKPdt, b.TenKP } into kq
                              select new
                              {
                                  kq.Key.MaBNhan,
                                  kq.Key.MaKPdt,
                                  kq.Key.TenKP
                              });
            var vvv = (from a in _data.VaoViens
                       join b in _data.KPhongs on a.MaKP equals b.MaKP
                       select new { a.MaBNhan, b.TenKP }).ToList();
            var _lTTBN = (from a in _lTTBN1
                          join b in vvv on a.MaBNhan equals b.MaBNhan into k
                          from k1 in k.DefaultIfEmpty()
                          join kb in _lidkb on a.IDKB equals kb.IDKB
                          select new {
                              a.MaBNhan,
                              a.TenBNhan,
                              a.DTuong,
                              a.NNhap,
                              //a.Tuoi,
                              Tuoi = DungChung.Ham.TuoitheoThang(_data, a.MaBNhan, DungChung.Bien.formatAge),
                              a.TChung,
                              a.DChi,
                              a.GTinh,
                              a.MaICD,
                              a.ChanDoan,
                              a.KetQua,
                              KhoaVV = (k1 != null) ? k1.TenKP : ""
                          }).ToList();
            _lTTBN = (from a in _lTTBN
                      join s in _lkpchuyen on a.MaBNhan equals s.MaBNhan into k
                      from k1 in k.DefaultIfEmpty()
                      select new
                      {
                          a.MaBNhan,
                          a.TenBNhan,
                          a.DTuong,
                          a.NNhap,
                          //a.Tuoi,
                          a.Tuoi,
                          a.TChung,
                          a.DChi,
                          a.GTinh,
                          a.MaICD,
                          a.ChanDoan,
                          a.KetQua,
                          KhoaVV = k1 != null ? k1.TenKP : a.KhoaVV
                      }).ToList();

            if (_lTTBN.Count > 0)
            {
                frmIn frm = new frmIn();
                BaoCao.rep_DSBenhNhanCapCuu_01071 rep = new BaoCao.rep_DSBenhNhanCapCuu_01071();
                rep.CQCQ.Value = DungChung.Bien.TenCQCQ.ToUpper();
                rep.CQ.Value = DungChung.Bien.TenCQ.ToUpper();
                rep.TIEUDE.Value = "Danh sách bệnh nhân cấp cứu " + lupKPhong.Text;
                rep.GDKY.Value = DungChung.Bien.GiamDoc;
                rep.NGAYTHANG.Value = "Từ ngày: " + TuNgay.ToString() + " đến ngày: " + NgayDen.ToString();
                rep.DataSource = _lTTBN;
                rep.Databinding();
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Không có dữ liệu", "Thông báo", MessageBoxButtons.OK);
                luptungay.Focus();
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}