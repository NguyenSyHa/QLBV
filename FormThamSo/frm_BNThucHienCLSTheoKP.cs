using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using QLBV.BaoCao;

namespace QLBV.FormThamSo
{
    public partial class frm_BNThucHienCLSTheoKP : DevExpress.XtraEditors.XtraForm
    {
        public frm_BNThucHienCLSTheoKP()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void frm_BNThucHienCLSTheoKP_Load(object sender, EventArgs e)
        {
            dtpTuNgay.DateTime = System.DateTime.Now;
            dtpDenNgay.DateTime = System.DateTime.Now;
            cboKhoaPhong.Properties.DataSource = KhoaPhong().ToList();
            cboKhoaPhong.Properties.DisplayMember = "TenKP";
            cboKhoaPhong.EditValue = "Tất cả";
            DoiTuongBenhNhan();
            cboThongKe.SelectedIndex = 0;
            if (DungChung.Bien.MaBV == "24272")
            {
                chkHienThiGio.Visible = true;
            }


        }
        class Kphong
        {
            public int MaKP { get; set; }
            public string TenKP { get; set; }
        }
        class BenhNhan
        {
            public string TenBNhan { get; set; }
            public DateTime NgaySinh { get; set; }
            public string DoiTuong { get; set; }
            public string TenDichVu { get; set; }
            public double DonGia { get; set; }
            public int? SoLuong { get; set; }
            public double ThanhTien { get; set; }
            public DateTime? NgayChiDinh { get; set; }
            public DateTime? NgayThongKe { get; set; }
            public DateTime? NgayThanhToan { get; set; }
        }
        class DTs
        {

            public string tendoituong { get; set; }
        }
        private List<Kphong> KhoaPhong()
        {
            List<Kphong> khoaphong = new List<Kphong>();
            var Kps = (from kp in data.KPhongs.Where(p => p.PLoai.Contains("Cận lâm sàng"))
                       select new
                       {
                           kp.MaKP,
                           kp.TenKP,
                       });
            Kphong kp1 = new Kphong();
            kp1.MaKP = 0;
            kp1.TenKP = "Tất cả";
            khoaphong.Add(kp1);
            foreach (var item in Kps)
            {
                Kphong kp = new Kphong();
                kp.MaKP = item.MaKP;
                kp.TenKP = item.TenKP;
                khoaphong.Add(kp);
            }
            return khoaphong;
        }
        private void DoiTuongBenhNhan()
        {
            List<DTs> Dt = new List<DTs>();

            var dt = data.DTBNs.Where(p => p.Status == 1).Select(o => o.DTBN1).ToList();
            DTs dttc = new DTs();

            dttc.tendoituong = "Tất cả";
            Dt.Add(dttc);
            foreach (var item in dt)
            {
                DTs dttm = new DTs();

                dttm.tendoituong = item;
                Dt.Add(dttm);
            }
            cboDoiTuong.Properties.DataSource = Dt.ToList();
            cboDoiTuong.Properties.DisplayMember = "tendoituong";
            cboDoiTuong.EditValue = "Tất cả";
        }
        private void btnInBaoCao_Click(object sender, EventArgs e)
        {
            DateTime TuNgay = DungChung.Ham.NgayTu(dtpTuNgay.DateTime);
            DateTime DenNgay = DungChung.Ham.NgayDen(dtpDenNgay.DateTime);
            int ThongKe = 0;
            if (cboThongKe.Text == "Tất cả bệnh nhân đã thực hiện(Lấy theo ngày thực hiện)")
            {
                ThongKe = 0;
            }
            if (cboThongKe.Text == "Chỉ bệnh nhân đã thanh toán( Lấy theo ngày thanh toán)")
            {
                ThongKe = 1;
            }

            int NoiTru = 0;
            if (radioGroup1.SelectedIndex == 0)
            {
                NoiTru = 3;
            }
            else
            {
                if (radioGroup1.SelectedIndex == 1)
                {
                    NoiTru = 1;
                }
                else
                {
                    if (radioGroup1.SelectedIndex == 2)
                    {
                        NoiTru = 0;
                    }
                }
            }

            int MaKP = 0;
            if (cboKhoaPhong.EditValue != null)
            {
                MaKP = Convert.ToInt32(cboKhoaPhong.EditValue);

            }
            string DoiTuong = cboDoiTuong.Text;


            InBC(MaKP, NoiTru, ThongKe, DoiTuong, TuNgay, DenNgay);
        }

        class TTBN
        {
            public int MabNhan { get; set; }
            public string TenBNhan { get; set; }
            public string NgaySinh { get; set; }
            public string DoiTuong { get; set; }
            public string TenDichVu { get; set; }
            public double DonGia { get; set; }
            public int SoLuong { get; set; }
            public double ThanhTien { get; set; }
            public DateTime? NgayThucHien { get; set; }
            public DateTime? NgayChiDinh { get; set; }
            public DateTime? NgayThanhToan { get; set; }
            public int? IDCD { get; set; }
        }
        void InBC(int Mkp, int NT, int KieuThongKe, string DoiTuong, DateTime TuNgay, DateTime DenNgay)
        {
           
            rep_BNThucHienCLSTheoNgay rep = new rep_BNThucHienCLSTheoNgay(chkHienThiGio.Checked);
            rep.colTuNgay.Text = dtpTuNgay.Text;
            rep.colDenNgay.Text = dtpDenNgay.Text;
            List<TTBN> BenhNhan = new List<TTBN>();
            if (KieuThongKe == 0)
            {
                var cls1 = (from bn in data.BenhNhans.Where(p => (NT == 3 ? true : p.NoiTru == NT)).Where(o => (DoiTuong == "Tất cả" ? true : o.DTuong == DoiTuong))
                            join cls in data.CLS.Where(o => o.NgayTH >= TuNgay && o.NgayTH <= DenNgay) on bn.MaBNhan equals cls.MaBNhan
                            join cd in data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                            join kPhong in data.KPhongs.Where(p => (Mkp == 0 ? p.PLoai.Contains("Cận Lâm Sàng") : p.MaKP == Mkp)) on cls.MaKPth equals kPhong.MaKP
                            join dv in data.DichVus on cd.MaDV equals dv.MaDV
                            select new
                            {
                                bn.MaBNhan,
                                bn.TenBNhan,
                                bn.NgaySinh,
                                bn.ThangSinh,
                                bn.NamSinh,
                                bn.DTuong,
                                cls.NgayThang,
                                cls.NgayTH,
                                cd.MaDV,
                                cd.DonGia,
                                cls.MaKP,
                                cd.IDCD,
                                cd.IdCLS,
                                dv.TenDV,
                            }).ToList();



                foreach (var item in cls1)
                {
                    TTBN bn = new TTBN();
                    bn.MabNhan = item.MaBNhan;
                    bn.TenBNhan = item.TenBNhan;
                    bn.NgaySinh = (string.IsNullOrEmpty(item.NgaySinh.Trim()) ? "" : (item.NgaySinh + "/")) + (string.IsNullOrEmpty(item.ThangSinh.Trim()) ? "" : (item.ThangSinh + "/")) + item.NamSinh;
                    bn.DoiTuong = item.DTuong;
                    bn.TenDichVu = item.TenDV;
                    bn.DonGia = item.DonGia;
                    bn.SoLuong = 1;
                    bn.NgayThucHien = item.NgayTH;
                    bn.NgayChiDinh = item.NgayThang;
                    bn.ThanhTien = item.DonGia;
                    bn.IDCD = item.MaDV;
                    BenhNhan.Add(bn);
                }
            }
            if (KieuThongKe == 1)
            {
                var cls1 = (from bn in data.BenhNhans.Where(p => (NT == 3 ? true : p.NoiTru == NT)).Where(o => (DoiTuong == "Tất cả" ? true : o.DTuong == DoiTuong))
                            join cls in data.CLS.Where(o => o.NgayTH != null) on bn.MaBNhan equals cls.MaBNhan
                            join cd in data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                            join kPhong in data.KPhongs.Where(p => (Mkp == 0 ? p.PLoai.Contains("Cận Lâm Sàng") : p.MaKP == Mkp)) on cls.MaKPth equals kPhong.MaKP
                            join vp in data.VienPhis.Where(p => p.NgayTT >= TuNgay && p.NgayTT <= DenNgay) on bn.MaBNhan equals vp.MaBNhan
                            join dv in data.DichVus on cd.MaDV equals dv.MaDV
                            select new
                            {
                                bn.MaBNhan,
                                bn.TenBNhan,
                                bn.NgaySinh,
                                bn.ThangSinh,
                                bn.NamSinh,
                                bn.DTuong,
                                cls.NgayThang,
                                cls.NgayTH,
                                cd.MaDV,
                                cd.DonGia,
                                cls.MaKP,
                                dv.TenDV,
                                vp.NgayTT
                            }).ToList();



                foreach (var item in cls1)
                {
                    TTBN bn = new TTBN();
                    bn.MabNhan = item.MaBNhan;
                    bn.TenBNhan = item.TenBNhan;
                    bn.NgaySinh = (string.IsNullOrEmpty(item.NgaySinh) ? "" : (item.NgaySinh + "/")) + (string.IsNullOrEmpty(item.ThangSinh) ? "" : (item.ThangSinh + "/")) + "/" + item.NamSinh;
                    bn.DoiTuong = item.DTuong;
                    bn.TenDichVu = item.TenDV;
                    bn.DonGia = item.DonGia;
                    bn.SoLuong = 1;
                    bn.NgayThucHien = item.NgayTH;
                    bn.NgayChiDinh = item.NgayThang;
                    bn.NgayThanhToan = item.NgayTT;
                    bn.ThanhTien = item.DonGia;
                    bn.IDCD = item.MaDV;
                    BenhNhan.Add(bn);
                }
            }

            if (BenhNhan.Count > 0)
            {
                rep.ThanhTien.Value = BenhNhan.Sum(p => p.ThanhTien).ToString("#,##");
                rep.SoLuong.Value = BenhNhan.Sum(p => p.SoLuong).ToString("#,##");
                rep.DataSource = BenhNhan.ToList();
                rep.databind();
                rep.CreateDocument();
                frmIn frm = new frmIn();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();

            }
            else
            {
                XtraMessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}