using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QLBV.FormDanhMuc;

namespace QLBV.FormThamSo
{
    public partial class frm_SoTheoDoiBNVV_14017 : Form
    {
        public frm_SoTheoDoiBNVV_14017()
        {
            InitializeComponent();

        }

        private class BaoCao
        {
            public string TenBN { get; set; }
            public int? Tuoi { get; set; }
            public string GTinh { get; set; }
            public string DChi { get; set; }
            public string BHYT { get; set; }
            public string NoiGT { get; set; }
            public string ChanDoanTuyenDuoi { get; set; }
            public string ChanDoanKB { get; set; }
            public string KhoaDT { get; set; }
            public string CDVV { get; set; }
            public string HinhThucDT { get; set; }
            public string NgayVV { get; set; }
        }

        public class KhoaPhong
        {
            public int MaKP { get; set; }
            public bool Check { get; set; }
            public string TenKP { get; set; }
        }

        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        List<frm_SoTheoDoiBNVV_14017.KhoaPhong> _lKPsd = new List<frm_SoTheoDoiBNVV_14017.KhoaPhong>();
        private void frm_SoTheoDoiBNVV_14017_Load(object sender, EventArgs e)
        {
            _lKPsd = (from kp in data.KPhongs.Where(p => p.PLoai == "Lâm sàng" || p.PLoai == "Cận lâm sàng" || p.PLoai == "Phòng khám")
                      select new KhoaPhong()
                      {
                          Check = false,
                          MaKP = kp.MaKP,
                          TenKP = kp.TenKP
                      }).Distinct().OrderBy(p => p.TenKP).ToList();
            _lKPsd.Add(new KhoaPhong { TenKP = "Tất cả", Check = false, MaKP = 99 });
            grcKhoaPhong.DataSource = _lKPsd.OrderByDescending(p => p.MaKP).ToList();
            txtDenNgay.DateTime = DateTime.Now;
            txtTuNgay.DateTime = DateTime.Now;

            rgHinhThuc.SelectedIndex = 2;
        }



        private void simpleButton1_Click(object sender, EventArgs e)
        {
            DungChung.Ham.CallProcessWaitingForm(TaoBaoCao);
        }
        private void TaoBaoCao()
        {
            List<BaoCao> rep = new List<BaoCao>();
            DateTime TuNgay = DungChung.Ham.NgayTu(txtTuNgay.DateTime);
            DateTime DenNgay = DungChung.Ham.NgayDen(txtDenNgay.DateTime);
            var q1 = (from bn in data.BenhNhans.Where(p => rgHinhThuc.SelectedIndex == 2 ? true : (rgHinhThuc.SelectedIndex == 1 ? (p.NoiTru == 1 && p.DTNT == false) : (p.NoiTru == 0 && p.DTNT == true)))
                      join bnkb in data.BNKBs on bn.MaBNhan equals bnkb.MaBNhan
                      join vv in data.VaoViens.Where(p => p.NgayVao >= TuNgay && p.NgayVao <= DenNgay) on bn.MaBNhan equals vv.MaBNhan
                      join bv in data.BenhViens on bn.MaBV equals bv.MaBV into bvs
                      from kq in bvs.DefaultIfEmpty()
                      select new
                      {
                          MaBN = bn.MaBNhan,
                          TenBN = bn.TenBNhan,
                          Tuoi = bn.Tuoi,
                          GTinh = bn.GTinh == 1 ? "Nam" : "Nữ",
                          DChi = bn.DChi,
                          BHYT = bn.SThe == null ? "" : bn.SThe,
                          NoiGT = bn.MaBV == null ? "" : kq == null ? "" : kq.TenBV,
                          ChanDoanTuyenDuoi = bn.CDNoiGT ?? "",
                          ChanDoanKB = bnkb.ChanDoan,
                          MaKPDT = bnkb.MaKPdt == bnkb.MaKP ? bnkb.MaKP : bnkb.MaKPdt,
                          CDVV = vv.TinhTrangVV,
                          HinhThucDT = bn.DTNT == true ? "Điều trị ngoại trú" : "Điều trị nội trú",
                          NgayVV = vv.NgayVao,

                      }).OrderBy(p => p.NgayVV).ToList();
            if (q1.Count > 0)
            {

                var q3 = (from bn in q1
                          //join cs in data.BenhViens on bn.NoiGT equals cs.MaBV
                          join kp in _lKPsd.Where(p => p.Check == true) on bn.MaKPDT equals kp.MaKP
                          group bn by new { bn.MaBN, bn.TenBN, bn.Tuoi, bn.GTinh, bn.DChi, bn.BHYT, bn.ChanDoanTuyenDuoi, bn.ChanDoanKB, bn.MaKPDT, bn.CDVV, bn.HinhThucDT, bn.NgayVV, kp.TenKP, bn.NoiGT } into kq
                          select new
                          {
                              MaBN = kq.Key.MaBN,
                              TenBN = kq.Key.TenBN,
                              Tuoi = kq.Key.Tuoi,
                              GTinh = kq.Key.GTinh,
                              DChi = kq.Key.DChi,
                              BHYT = kq.Key.BHYT,
                              NoiGT = kq.Key.NoiGT,
                              TuyenDuoi = kq.Key.ChanDoanTuyenDuoi,
                              KhoaKB = kq.Key.ChanDoanKB,
                              KhoaDT = kq.Key.TenKP,
                              CDVV = kq.Key.CDVV,
                              HinhThucDT = kq.Key.HinhThucDT,
                              NgayVV = kq.Key.NgayVV
                          }).OrderBy(p => p.NgayVV).ToList();

                foreach (var item in q3)
                {
                    BaoCao bc = new BaoCao();
                    bc.TenBN = item.TenBN;
                    bc.Tuoi = item.Tuoi;
                    bc.GTinh = item.GTinh;
                    bc.DChi = item.DChi;
                    bc.BHYT = item.BHYT;
                    bc.NoiGT = item.NoiGT;
                    bc.ChanDoanTuyenDuoi = item.TuyenDuoi;
                    bc.ChanDoanKB = item.KhoaKB;
                    bc.KhoaDT = item.KhoaDT;
                    bc.CDVV = item.CDVV;
                    bc.HinhThucDT = item.HinhThucDT;
                    bc.NgayVV = item.NgayVV == null ? "" : Convert.ToDateTime(item.NgayVV).ToString("dd/MM/yyyy");

                    rep.Add(bc);
                }
                if (rep.Count > 0)
                {
                    Dictionary<string, object> _dic = new Dictionary<string, object>();
                    _dic.Add("Ngay", "Từ ngày " + TuNgay.ToString("dd/MM/yyyy") + " đến ngày " + DenNgay.ToString("dd/MM/yyyy"));
                    DungChung.Ham.Print(DungChung.PrintConfig.rep_SoTheoDoiBNVV_14017, rep, _dic, false);
                }
                else
                {

                    MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }

        private void grvKhoaPhong_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {

        }

        private void grvKhoaPhong_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column == colChon)
            {
                if (grvKhoaPhong.GetFocusedRowCellValue(colKP) != null)
                {
                    string name = grvKhoaPhong.GetFocusedRowCellValue(colKP).ToString();
                    if (name == "Tất cả")
                    {
                        bool a = !(bool)grvKhoaPhong.GetFocusedRowCellValue(colChon);
                        if (a == true)
                        {
                            foreach (var item in _lKPsd)
                            {
                                item.Check = true;
                            }
                        }
                        else
                        {
                            foreach (var item in _lKPsd)
                            {
                                item.Check = false;
                            }
                        }
                        grcKhoaPhong.DataSource = null;
                        grcKhoaPhong.DataSource = _lKPsd.OrderByDescending(P => P.MaKP).ToList();
                    }
                }
            }
        }


    }
}
