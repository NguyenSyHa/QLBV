using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;

namespace QLBV.FormThamSo
{
    public partial class Frm_SoThuChiTU_BG : DevExpress.XtraEditors.XtraForm
    {
        public Frm_SoThuChiTU_BG()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

        private bool KTtaoBc()
        {
            if (dateTuNgay.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn ngày bắt đầu in báo cáo");
                dateTuNgay.Focus();
                return false;
            }
            if (dateDenNgay.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn đến ngày kết thúc in báo cáo");
                dateDenNgay.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtTonDK.Text))
            {
                MessageBox.Show("Bạn chưa nhập tồn đầu kỳ");
                txtTonDK.Focus();
                return false;
            }

            else return true;
        }
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
        private void Frm_BcKSK_TKy_Load(object sender, EventArgs e)
        {
            dateTuNgay.DateTime = System.DateTime.Today;
            DateTime tungay = dateTuNgay.DateTime.AddDays(1).AddHours(23).AddMinutes(59);
            dateDenNgay.DateTime = tungay;
            dateTuNgay.Focus();
            var kphong = (from tu in data.TamUngs
                          join kp in data.KPhongs on tu.MaKP equals kp.MaKP
                          group kp by new { kp.MaKP, kp.TenKP } into kq
                          select new { kq.Key.TenKP, kq.Key.MaKP }).ToList();
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

        private void btnInBC_Click(object sender, EventArgs e)
        {
            DateTime tungay = dateTuNgay.DateTime;
            DateTime denngay = dateDenNgay.DateTime;
            List<KPhong> _lKhoaP = new List<KPhong>();
            if (KTtaoBc())
            {
                //tungay = DungChung.Ham.NgayTu(dateTuNgay.DateTime);//dateTuNgay.DateTime;
                //denngay = DungChung.Ham.NgayDen(dateDenNgay.DateTime);//dateDenNgay.DateTime;

                frmIn frm = new frmIn();
                BaoCao.Rep_SoThuChiTU_BG rep = new BaoCao.Rep_SoThuChiTU_BG();
                rep.NTN.Value = "Từ ngày " + dateTuNgay.Text + " Đến ngày " + dateDenNgay.Text;
                _lKhoaP = _Kphong.Where(p => p.chon == true).ToList();
                //  _lKhoaP.Add(new KPhong { makp = "", tenkp = "" });
                var qtu = (from tu in data.TamUngs.Where(p => p.PhanLoai == 0 || p.PhanLoai == 4)
                           join bn in data.BenhNhans on tu.MaBNhan equals bn.MaBNhan
                           select new { tu.SoHD, bn.MaBNhan, bn.TenBNhan, bn.DChi, tu.NgayThu, tu.IDTamUng, tu.PhanLoai, tu.SoTien, tu.MaKP, KPBN = bn.MaKP }).ToList();
                //var qtt = (from tt in qtu.Where(p => p.PhanLoai == 0)
                //           join tu in data.TamUngs.Where(p => p.PhanLoai == 4) on tt.MaBNhan equals tu.MaBNhan
                //           select tu).Distinct().ToList();
                #region tạm bỏ
                //var q = (from k in _lKhoaP
                //         join tu in qtu on k.makp equals tu.MaKP
                //         group new { tu, k } by new { tu.MaBNhan, tu.TenBNhan, tu.DChi, k.tenkp, tu.NgayThu, tu.IDTamUng, tu.PhanLoai } into kq
                //         select new
                //         {
                //             // NTN = kq.Key.NgayThu,
                //             TenBN = kq.Key.TenBNhan,
                //             DChi = kq.Key.DChi,
                //             TenKP = kq.Key.tenkp,
                //             NgayThu = kq.Key.NgayThu,
                //             MSThu = (kq.Key.PhanLoai == 0) ? kq.Key.IDTamUng : 0,
                //             MSChi = 0,//(kq.Key.PhanLoai == 1 || kq.Key.PhanLoai == 2) ? kq.Key.IDTamUng : 0,
                //             PhanLoai = kq.Key.PhanLoai,
                //             TienThu = kq.Where(p => p.tu.PhanLoai == 0).Where(p => p.tu.NgayThu >= tungay && p.tu.NgayThu <= denngay).Sum(p => p.tu.SoTien),
                //             TienChi = (from k in qtt
                //                        where k.MaBNhan == kq.Key.MaBNhan
                //                        select k.SoTien).Sum(),
                //             //TienChi = (from k in kq.Where(l => l.tu.PhanLoai == 0) join k2 in kq.Where(p => p.tu.PhanLoai == 0).Where(p => p.tu.NgayThu >= tungay && p.tu.NgayThu <= denngay) on k.tu.MaBNhan equals k2.tu.MaBNhan select k.tu.SoTien).Sum(),

                //             TonDK = (kq.Where(p => p.tu.NgayThu < tungay).Where(p => p.tu.PhanLoai == 0).Sum(p => p.tu.SoTien) == null ? 0 : kq.Where(p => p.tu.NgayThu < tungay).Where(p => p.tu.PhanLoai == 0).Sum(p => p.tu.SoTien)) - (kq.Where(p => p.tu.NgayThu < tungay).Where(p => p.tu.PhanLoai == 2).Sum(p => p.tu.SoTien) == null ? 0 : kq.Where(p => p.tu.NgayThu < tungay).Where(p => p.tu.PhanLoai == 2).Sum(p => p.tu.SoTien))
                //         }).Where(p => p.PhanLoai == 0 || p.PhanLoai == 1 || p.PhanLoai == 2).ToList().Select(p => new
                //         {
                //             // NTN = p.NTN.ToString().Substring(0, 5),
                //             p.TenBN,
                //             p.DChi,
                //             NgayThu = new DateTime(Convert.ToInt32(Convert.ToDateTime(p.NgayThu).Year), Convert.ToInt16(Convert.ToDateTime(p.NgayThu).Month), Convert.ToInt32(Convert.ToDateTime(p.NgayThu).Day)),
                //             p.TenKP,
                //             p.MSThu,
                //             p.MSChi,
                //             p.TienChi,
                //             p.TienThu,
                //             Ton = (p.TienThu == null ? 0 : p.TienThu) - p.TienChi,
                //             p.TonDK

                //         }
                //     ).OrderBy(p => p.NgayThu).ThenBy(p => p.TenBN).ToList();
                #endregion

                var q = (from tu in qtu
                         where (_lKhoaP.Where(p => p.makp == 0 && p.chon).Count() > 0) ? true : (_lKhoaP.Where(p => p.makp == tu.MaKP).Count() > 0)
                         group tu by new {tu.SoHD, tu.MaBNhan, tu.TenBNhan, tu.DChi, tu.MaKP, tu.NgayThu, tu.IDTamUng, tu.PhanLoai } into kq
                         select new
                         {
                             // NTN = kq.Key.NgayThu,
                             TenBN = kq.Key.TenBNhan,
                             DChi = kq.Key.DChi,
                             TenKP = _lKhoaP.Where(p => p.makp == kq.Key.MaKP).Count() > 0 ? (_lKhoaP.Where(p => p.makp == kq.Key.MaKP).ToList().First().tenkp) : "",
                             NgayThu = kq.Key.NgayThu,
                             MSThu = (kq.Key.PhanLoai == 0) ? kq.Key.SoHD : "0",
                             MSChi =(kq.Key.PhanLoai == 4) ? kq.Key.SoHD : "0",//(kq.Key.PhanLoai == 1 || kq.Key.PhanLoai == 2) ? kq.Key.IDTamUng : 0,
                             PhanLoai = kq.Key.PhanLoai,
                             TienThu = kq.Where(p => p.PhanLoai == 0).Where(p => p.NgayThu >= tungay && p.NgayThu <= denngay).Sum(p => p.SoTien),
                             //TienChi = kq.Where(p => p.PhanLoai == 4).Where(p => p.NgayThu >= tungay && p.NgayThu <= denngay).Sum(p => p.SoTien),
                             TienChi = kq.Where(p => p.PhanLoai == 4).Where(p => p.NgayThu >= tungay && p.NgayThu <= denngay).Sum(p => p.SoTien),
                             //TienChi = (from k in kq.Where(l => l.tu.PhanLoai == 0) join k2 in kq.Where(p => p.tu.PhanLoai == 0).Where(p => p.tu.NgayThu >= tungay && p.tu.NgayThu <= denngay) on k.tu.MaBNhan equals k2.tu.MaBNhan select k.tu.SoTien).Sum(),

                             TonDK = (kq.Where(p => p.NgayThu < tungay).Where(p => p.PhanLoai == 0).Sum(p => p.SoTien) == null ? 0 : kq.Where(p => p.NgayThu < tungay).Where(p => p.PhanLoai == 0).Sum(p => p.SoTien)) - (kq.Where(p => p.NgayThu < tungay).Where(p => p.PhanLoai == 4).Sum(p => p.SoTien) == null ? 0 : kq.Where(p => p.NgayThu < tungay).Where(p => p.PhanLoai == 4).Sum(p => p.SoTien))
                         }).ToList().Select(p => new
                    {
                        // NTN = p.NTN.ToString().Substring(0, 5),
                        p.TenBN,
                        p.DChi,
                        NgayThu = p.NgayThu.Value.Date,//new DateTime(Convert.ToInt32(Convert.ToDateTime(p.NgayThu).Year), Convert.ToInt16(Convert.ToDateTime(p.NgayThu).Month), Convert.ToInt32(Convert.ToDateTime(p.NgayThu).Day)),
                        p.TenKP,
                        p.MSThu,
                        p.MSChi,
                        p.TienChi,
                        p.TienThu,
                        Ton = (p.TienThu == null ? 0 : p.TienThu) - p.TienChi,
                        p.TonDK,
                        p.PhanLoai

                    }
                     ).OrderBy(p => p.NgayThu).ToList();
                double tonDK = 0;
                tonDK = Convert.ToDouble(txtTonDK.Text);
                rep.TonDK.Value = tonDK.ToString("#,##.00");
                var q1 = (from qn in q
                          where (qn.NgayThu >= tungay && qn.NgayThu <= denngay)
                          group qn by new { qn.NgayThu, qn.TenBN, qn.DChi, qn.TonDK } into kq
                          select new
                          {
                              NgayThu = kq.Key.NgayThu, // kq.Key.NgayThu.Value.Day + "/" + kq.Key.NgayThu.Value.Month + "/" + kq.Key.NgayThu.Value.Year,
                              kq.Key.TenBN,
                              kq.Key.DChi,
                              TenKP = String.Join(",", kq.Select(p => p.TenKP).Distinct()),
                              MSThu = String.Join(",", kq.Where(p=>p.PhanLoai==0).Select(p => p.MSThu)),
                              MSChi = String.Join(",", kq.Where(p => p.PhanLoai == 4).Select(p => p.MSChi)),
                              TienThu = kq.Sum(p => p.TienThu),
                              TienChi = kq.Sum(p => p.TienChi),
                          }
                        ).Where(p => p.TienChi != 0 || p.TienThu != 0).OrderBy(p=>p.NgayThu).ToList();
                rep.DataSource = q1;
                rep.BindingData();
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
            }
        }
        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void grvKhoaphong_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {

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

        private void grcKhoaphong_Click(object sender, EventArgs e)
        {

        }
    }
}