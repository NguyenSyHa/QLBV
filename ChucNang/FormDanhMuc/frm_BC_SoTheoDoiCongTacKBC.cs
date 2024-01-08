using System;
using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace QLBV.ChucNang.FormDanhMuc
{
    public partial class frm_BC_SoTheoDoiCongTacKBC : DevExpress.XtraEditors.XtraForm
    {
        public frm_BC_SoTheoDoiCongTacKBC()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void btnTaoBaoCao_Click(object sender, EventArgs e)
        {
            List<KPhongc> _lKhoaP = new List<KPhongc>();
            _lKhoaP = _Kphong.Where(p => p.chon == true).ToList();
            if (_lKhoaP.Count() == 0)
            {
                MessageBox.Show("Chưa chọn khoa phòng chỉ định");
            }
            TaoBaoCao(dtTuNgay.DateTime, dtDenNgay.DateTime, _lKhoaP);
        }

        private class BenhNhan
        {
            public int MaBNhan { get; set; }
            public string TenBenhNhan { get; set; }
            public string MaQD { get; set; }
            public string TenKP { get; set; }
            public string TenDV { get; set; }
            public string NgayTh { get; set; }
            public int SoLuong { get; set; }
        }
        private class KPhongc
        {
            public string MaQD { get; set; }
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
        List<KPhong> _lkpall = new List<KPhong>();
        List<KPhongc> _Kphong = new List<KPhongc>();
        private void TaoBaoCao(DateTime TuNgay, DateTime DenNgay, List<KPhongc> kphong)
        {

            List<BenhNhan> benhnhan = new List<BenhNhan>();
            var a = (from bn in data.BenhNhans
                     join cls in data.CLS.Where(p => p.NgayTH >= TuNgay && p.NgayTH <= DenNgay) on bn.MaBNhan equals cls.MaBNhan
                     join kp in data.KPhongs on cls.MaKPth equals kp.MaKP
                     select new { cls.IdCLS, kp.MaQD, kp.MaKP, kp.TenKP, bn.TenBNhan, bn.MaBNhan, cls.NgayTH });
            var kq1 = (from a1 in a
                       join cd in data.ChiDinhs on a1.IdCLS equals cd.IdCLS
                       join dv in data.DichVus.Where(p => p.TenDV.Contains("Sắc thuốc thang")) on cd.MaDV equals dv.MaDV
                       group new { a1 } by new { a1.MaQD,a1.MaKP, a1.TenKP, dv.TenDV, a1.MaBNhan, a1.TenBNhan, a1.NgayTH } into kq
                       select new
                       {
                           MaBNhan = kq.Key.MaBNhan,
                           TenBenhNhan = kq.Key.TenBNhan,
                           MaKP = kq.Key.MaKP,
                           MaQD = kq.Key.MaQD,
                           TenKP = kq.Key.TenKP,
                           TenDV = kq.Key.TenDV,
                           NgayTh = kq.Key.NgayTH,
                           SoLuong = kq.Select(p => p.a1.MaBNhan).Count(),
                       }).ToList();

            var benhNhan = (from q in kq1
                            join _kp in kphong on q.MaKP equals _kp.makp
                            select new BenhNhan
                            {
                                MaBNhan = q.MaBNhan,
                                TenBenhNhan = q.TenBenhNhan,
                                MaQD = q.MaQD,
                                TenKP = q.TenKP,
                                TenDV = q.TenDV,
                                NgayTh = q.NgayTh.Value.ToString("dd/MM/yyyy"),
                                SoLuong = q.SoLuong,
                            }).ToList();


            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("TuNgay", TuNgay.ToString("dd/MM/yyyy"));
            dic.Add("DenNgay", DenNgay.ToString("dd/MM/yyyy"));
            dic.Add("TuNgayDenNgay", string.Format("THỜI GIAN BÁO CÁO TỪ NGÀY {0} ĐẾN NGÀY {1}", TuNgay.ToString("dd/MM/yyyy"), DenNgay.ToString("dd/MM/yyyy")));

            if (benhNhan.Count() > 0)
            {
                DungChung.Ham.Print(DungChung.PrintConfig.rep_BC_congtacsacthuoctrongCSKCB, benhNhan, dic, false);
            }
            else
            {
                XtraMessageBox.Show("Không có dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void frm_BC_SoTheoDoiCongTacKBC_Load(object sender, EventArgs e)
        {
            dtTuNgay.DateTime = DungChung.Ham.NgayTu(System.DateTime.Now);
            dtDenNgay.DateTime = DungChung.Ham.NgayDen(System.DateTime.Now);
            _lkpall = data.KPhongs.Where(p => p.Status == 1).ToList();
            var kphong = (from kp in _lkpall
                          where (kp.PLoai == "Lâm sàng" || kp.PLoai == "Phòng khám" || kp.PLoai == DungChung.Bien.st_PhanLoaiKP.HanhChinh)
                          select new { kp.TenKP, kp.MaKP }).ToList();
            if (kphong.Count > 0)
            {
                KPhongc themmoi1 = new KPhongc();
                themmoi1.tenkp = "Chọn tất cả";
                themmoi1.makp = 0;
                themmoi1.chon = true;
                _Kphong.Add(themmoi1);
                foreach (var a in kphong)
                {
                    KPhongc themmoi = new KPhongc();
                    themmoi.tenkp = a.TenKP;
                    themmoi.makp = a.MaKP;
                    themmoi.chon = true;
                    _Kphong.Add(themmoi);
                }
                dsKP.DataSource = _Kphong.ToList();
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void grvdsKP_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "Chọn")
            {
                if (grvdsKP.GetFocusedRowCellValue("tenkp") != null)
                {
                    string Ten = grvdsKP.GetFocusedRowCellValue("tenkp").ToString();

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
                        dsKP.DataSource = "";
                        dsKP.DataSource = _Kphong.ToList();

                    }
                }
            }
        }
    }
}