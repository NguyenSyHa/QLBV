using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QLBV.FormThamSo
{
    public partial class frm_BC_KCB_30003 : Form
    {
        public frm_BC_KCB_30003()
        {
            InitializeComponent();
        }

        public class BaoCao
        {
            public int TongBN { get; set; }
            public int TongCV { get; set; }
            public int TongXNCTM { get; set; }
            public int TongXNSHNT { get; set; }
            public int TongXNMienDich { get; set; }
            public int TongXNHSM1 { get; set; }
            public int TongXNHSM2 { get; set; }
            public int TongXNDM { get; set; }
            public int TongXNKhac { get; set; }
            public int TongSA { get; set; }
            public int TongXQ { get; set; }
            public int TongDT { get; set; }
            public string Tenkp { get; set; }
            public string TenLoai { get; set; }
            public string TenLoaiBN { get; set; }
            public string PLoai { get; set; }
            
        }

        public class KhoaPhong
        {
            public int MaKP { get; set; }
            public string TenKP { get; set; }
            public string PLoai { get; set; }
            public bool Check { get; set; }
        }

        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        List<frm_BC_KCB_30003.KhoaPhong> _lKPsd = new List<frm_BC_KCB_30003.KhoaPhong>();
        private void frm_BC_KCB_30003_Load(object sender, EventArgs e)
        {
            txtTuNgay.DateTime = DateTime.Now;
            txtDenNgay.DateTime = DateTime.Now;
            _lKPsd = (from kp in data.KPhongs.Where(p => p.PLoai == "Lâm sàng" || p.PLoai == "Phòng khám")
                      select new KhoaPhong()
                      {
                          Check = false,
                          MaKP = kp.MaKP,
                          TenKP = kp.TenKP,
                          PLoai = kp.PLoai
                      }).Distinct().OrderBy(p => p.TenKP).ToList();
            _lKPsd.Add(new KhoaPhong { TenKP = "Tất cả", Check = false, MaKP = 999 });
            grcKhoaPhong.DataSource = _lKPsd.OrderByDescending(p => p.MaKP).ToList();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DungChung.Ham.CallProcessWaitingForm(TaoBaoCao);
        }

        private void TaoBaoCao()
        {
            List<BaoCao> rep = new List<BaoCao>();
            DateTime TuNgay = DungChung.Ham.NgayTu(txtTuNgay.DateTime);
            DateTime DenNgay = DungChung.Ham.NgayDen(txtDenNgay.DateTime);

            var qkb = (from a in data.BNKBs.Where(p => p.NgayKham >= TuNgay && p.NgayKham <= DenNgay)
                       select new
                       {
                           a.IDKB, 
                           a.PhuongAn,
                           
                           a.MaKP
                       }).ToList();

            var qxn = (from a in data.CLS.Where(p => p.NgayTH >= TuNgay && p.NgayTH <= DenNgay) 
                       join b in data.ChiDinhs on a.IdCLS equals b.IdCLS into a0
                       from a1 in a0.DefaultIfEmpty()
                       join c in data.DichVus on a1.MaDV equals c.MaDV
                       select new
                       {
                           c.IdTieuNhom,
                           
                           a.MaKP,
                           a.IdCLS
                       }).ToList();

            
            //var qKB = (from kb in q2
                       //group kb by new {kb.TenKP, kb._tenloaibn, kb._tenloai, kb.PLoai, kb._maBV, kb._maBN, kb._maKP} into kq
                       //select new
                       //{
                           
                       //}).ToList();
            var qkp = (from a in _lKPsd.Where(p => p.Check == true).Where(p => p.MaKP != 999) 
                      select new {
                          a.MaKP,
                          a.TenKP,
                          a.PLoai,
                          //PLoai = a.PLoai == "Lâm sàng" ? "II. KHOA" : "I. PHÒNG KHÁM",
                          //TenLoaiBN = a.PLoai == "Lâm sàng" ? "TỔNG SỐ BN VÀO ĐIỀU TRỊ" : "TỔNG SỐ BN KHÁM BỆNH",
                          //TenLoai = a.PLoai == "Lâm sàng" ? "KHOA" : "PHÒNG KHÁM",
                      }).OrderBy(p => p.MaKP).ToList();

            

            //var kq = (from a in qkp
                        //join b in qxn on a.MaKP equals b.MaKP into b0 
                        //from b1 in b0.DefaultIfEmpty()
                        //join c in qkb on a.MaKP equals c.MaKP into c0
                        //from c1 in c0.DefaultIfEmpty()
                        //select new
                        //{
                            //a.MaKP,
                            //c1.IDKB,
                            //c1.PhuongAn,
                            //b1.MaDV,
                            //b1.IdTieuNhom,
                            //b1.IdCLS,
                            //tongbnkb = qkb.Where(p => p.MaKP == a.MaKP).Select(p => p.IDKB).Count(),
                            //tongbncv = qkb.Where(p => p.MaKP == a.MaKP).Where(p => p.PhuongAn == 2).Select(p => p.IDKB).Count(),
                            //TongXNCTM = qxn.Where(p => p.IdTieuNhom == 2).Where(p => p.MaKP == a.MaKP).Select(p => p.IdCLS).Count(),
                            //TongXNHSM1 = qxn.Where(p => p.MaDV == 11590).Where(p => p.MaKP == a.MaKP).Select(p => p.IdCLS).Count(),
                            //TongXNHSM2 = qxn.Where(p => p.MaDV == 4272).Where(p => p.MaKP == a.MaKP).Select(p => p.IdCLS).Count(),
                            //TongXNSHNT = qxn.Where(p => p.IdTieuNhom == 6).Where(p => p.MaKP == a.MaKP).Select(p => p.IdCLS).Count(),
                            //TongXNKhac = qxn.Where(p => p.IdTieuNhom == 7).Where(p => p.MaKP == a.MaKP).Select(p => p.IdCLS).Count(),
                            //TongXNMienDich = qxn.Where(p => p.IdTieuNhom == 118).Where(p => p.MaKP == a.MaKP).Select(p => p.IdCLS).Count(),
                            //TongXNDM = qxn.Where(p => p.IdTieuNhom == 111).Where(p => p.MaKP == a.MaKP).Select(p => p.IdCLS).Count(),
                            //TongDT = qxn.Where(p => p.IdTieuNhom == 15).Where(p => p.MaKP == a.MaKP).Select(p => p.IdCLS).Count(),
                            //TongSA = qxn.Where(p => p.IdTieuNhom == 4).Where(p => p.MaKP == a.MaKP).Select(p => p.IdCLS).Count(),
                            //TongXQ = qxn.Where(p => p.IdTieuNhom == 5).Where(p => p.MaKP == a.MaKP).Select(p => p.IdCLS).Count(),
                        //}).ToList();


            if (qkp.Count > 0)
            {
                foreach (var x in qkp)
                {
                    BaoCao bc = new BaoCao();
                    bc.Tenkp = x.TenKP;
                    bc.TenLoai = x.PLoai == "Lâm sàng" ? "KHOA" : "PHÒNG KHÁM";
                    bc.TenLoaiBN = x.PLoai == "Lâm sàng" ? "TỔNG SỐ BN VÀO ĐIỀU TRỊ" : "TỔNG SỐ BN KHÁM BỆNH";
                    bc.PLoai = x.PLoai == "Lâm sàng" ? "II. KHOA" : "I. PHÒNG KHÁM";
                    bc.TongBN = qkb.Where(p => p.MaKP == x.MaKP).Select(p => p.IDKB).Count();
                    bc.TongCV = qkb.Where(p => p.MaKP == x.MaKP).Where(p => p.PhuongAn == 2).Select(p => p.IDKB).Distinct().Count();
                    bc.TongDT = qxn.Where(p => p.IdTieuNhom == 15).Where(p => p.MaKP == x.MaKP).Select(p => p.IdCLS).Distinct().Count();
                    bc.TongSA = qxn.Where(p => p.IdTieuNhom == 4).Where(p => p.MaKP == x.MaKP).Select(p => p.IdCLS).Distinct().Count();
                    bc.TongXNCTM = qxn.Where(p => p.IdTieuNhom == 2).Where(p => p.MaKP == x.MaKP).Select(p => p.IdCLS).Distinct().Count();
                    bc.TongXNDM = qxn.Where(p => p.IdTieuNhom == 111).Where(p => p.MaKP == x.MaKP).Select(p => p.IdCLS).Distinct().Count();
                    bc.TongXNHSM1 = qxn.Where(p => p.IdTieuNhom == 1).Where(p => p.MaKP == x.MaKP).Select(p => p.IdCLS).Distinct().Count();
                    
                    bc.TongXNKhac = qxn.Where(p => p.IdTieuNhom == 7).Where(p => p.MaKP == x.MaKP).Select(p => p.IdCLS).Distinct().Count();
                    bc.TongXNMienDich = qxn.Where(p => p.IdTieuNhom == 118).Where(p => p.MaKP == x.MaKP).Select(p => p.IdCLS).Distinct().Count();
                    bc.TongXNSHNT = qxn.Where(p => p.IdTieuNhom == 6).Where(p => p.MaKP == x.MaKP).Select(p => p.IdCLS).Distinct().Count();
                    bc.TongXQ = qxn.Where(p => p.IdTieuNhom == 5).Where(p => p.MaKP == x.MaKP).Select(p => p.IdCLS).Distinct().Count();

                    rep.Add(bc);
                }


                Dictionary<string, object> _dic = new Dictionary<string, object>();
                _dic.Add("Ngay", "Từ ngày " + TuNgay.ToString("dd/MM/yyyy") + " đến ngày " + DenNgay.ToString("dd/MM/yyyy"));

                DungChung.Ham.Print(DungChung.PrintConfig.rep_BC_KCB_30003, rep, _dic, false);

            
            }
            else
            {
                MessageBox.Show("Bạn chưa chọn khoa phòng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
                    
                  
        }
        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
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
