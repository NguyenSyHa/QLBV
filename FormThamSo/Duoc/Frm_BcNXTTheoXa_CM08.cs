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
    public partial class Frm_BcNXTTheoXa_CM08 : DevExpress.XtraEditors.XtraForm
    {
        public Frm_BcNXTTheoXa_CM08()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

        private bool KTtaoBcNXT()
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
            //if (lupKho.EditValue == null)
            //{
            //    MessageBox.Show("Bạn chưa chọn xã");
            //    lupKho.Focus();
            //    return false;
            //}
            return true;
        }
        List<NhomDV> _lnhom = new List<NhomDV>();
        private void Frm_BcNXTTheoXa_CM08_Load(object sender, EventArgs e)
        {
            dateTuNgay.DateTime = System.DateTime.Now;
            dateDenNgay.DateTime = System.DateTime.Now;
            var q = (from TK in data.KPhongs.Where(p => p.PLoai.Contains("Xã phường")) select new { TK.MaKP, TK.TenKP }).ToList();
            lupXaPhuong.Properties.DataSource = q.ToList();
            var kho = (from TK in data.KPhongs.Where(p => p.PLoai.Contains("Khoa dược")) select new { TK.MaKP, TK.TenKP }).ToList();
            lupMaKho.Properties.DataSource = kho.ToList();
            //cmbPL.EditValue = "Thuốc";
            var pldv = (from nhom in data.NhomDVs.Where(p => p.Status == 1)
                        select new
                        {
                            IDNhom = nhom.IDNhom,
                            TenNhomCT = nhom.TenNhomCT,
                            nhom.TenNhom,
                        })
                       .OrderBy(n => n.TenNhom).ToList();
            NhomDV moi2 = new NhomDV();
            moi2.TenNhom = " Tất cả";
            moi2.IDNhom = 0;
            moi2.TenNhomCT = "Tất cả";
            _lnhom.Add(moi2);
            //_lnhom.Add(new NhomDV { IDNhom = 0, TenNhomCT = "Tất cả", TenNhom = "Tất cả" });
            //_lnhom.InsertRange(1,
            foreach (var a in pldv)
            {
                NhomDV moi = new NhomDV();
                moi.TenNhom = a.TenNhom;
                moi.IDNhom = a.IDNhom;
                moi.TenNhomCT = a.TenNhomCT;
                _lnhom.Add(moi);
            }
            lupPLDV.Properties.DataSource = _lnhom;

        }

        private void btnInBC_Click(object sender, EventArgs e)
        {
            DateTime tungay = System.DateTime.Now.Date;
            DateTime denngay = System.DateTime.Now.Date;
            int _makho = 0;
            if (lupMaKho.EditValue != null)
                _makho = Convert.ToInt32(lupMaKho.EditValue);

            if (KTtaoBcNXT())
            {

                tungay = DungChung.Ham.NgayTu(dateTuNgay.DateTime);//dateTuNgay.DateTime;
                denngay = DungChung.Ham.NgayDen(dateDenNgay.DateTime);//dateDenNgay.DateTime;


                BaoCao.Rep_BcNXTTheoXa_CM08 rep = new BaoCao.Rep_BcNXTTheoXa_CM08(chkHienThi.Checked);

                rep.TuNgay.Value = dateTuNgay.Text;
                rep.DenNgay.Value = dateDenNgay.Text;
                rep.TuNgayDenNgay.Value = "Từ ngày " + dateTuNgay.Text + " Đến ngày " + dateDenNgay.Text;

                int _xp = 0;
                if (lupXaPhuong.EditValue != null)
                    _xp = Convert.ToInt32(lupXaPhuong.EditValue);
                var qtenxp = (from xp in data.KPhongs.Where(p => p.MaKP == _xp)
                              select new { xp.TenKP }).ToList();
                if (qtenxp.Count > 0)
                {
                    rep.XaPhuong.Value = (qtenxp.First().TenKP);
                }
                int id = 0;
                if (lupPLDV.EditValue != null)
                    id = Convert.ToInt32(lupPLDV.EditValue);
                string pl = "";
                if (id > 0)
                    pl = lupPLDV.Text;
                rep.PhanLoai.Value = ("báo cáo nhập xuất tồn " + pl).ToUpper();
                List<KPhong> _lkp = data.KPhongs.Where(p => p.PLoai == "Xã phường").ToList();
                _lkp = _lkp.Where(p => (_xp == 0 ? true : p.MaKP == _xp)).ToList();

                var qnxt1 = (from nhapd in data.NhapDs.Where(p => p.MaKP == (_makho))
                             join nhapdct in data.NhapDcts on nhapd.IDNhap equals nhapdct.IDNhap
                             where (nhapd.PLoai == 1 || nhapd.PLoai == 2 || nhapd.PLoai == 5)
                             select new { nhapd.MaKPnx, nhapd.MaKP, nhapd.NgayNhap, nhapdct.DonGia, nhapd.PLoai, nhapd.KieuDon, nhapdct.SoLuongSD, nhapdct.ThanhTienSD,
                                          SoLuongN = nhapd.PLoai == 1 ? nhapdct.SoLuongN : 0,
                                          SoLuongX = (nhapd.PLoai == 2) ? nhapdct.SoLuongX : 0,
                                          ThanhTienN = nhapd.PLoai == 1 ? nhapdct.ThanhTienN : 0,
                                          ThanhTienX = (nhapd.PLoai == 2) ? nhapdct.ThanhTienX : 0,
                                  nhapdct.MaDV }
                                ).ToList();
                var tnhom = (from dv in data.DichVus
                             join tieunhomdv in data.TieuNhomDVs on dv.IdTieuNhom equals tieunhomdv.IdTieuNhom
                             join nhomdv in data.NhomDVs.Where(p => p.Status == 1) on dv.IDNhom equals nhomdv.IDNhom
                             select new { nhomdv.TenNhomCT, tieunhomdv.TenTN, dv.TenDV, dv.NuocSX, dv.DonVi, dv.MaDV, nhomdv.IDNhom }).ToList();
                var qnxt = (from nhapdct in qnxt1
                            join dv in tnhom on nhapdct.MaDV equals dv.MaDV
                            join kp in _lkp on nhapdct.MaKPnx equals kp.MaKP
                            group new { dv, nhapdct } by new { dv.IDNhom, dv.TenNhomCT, dv.TenTN, dv.TenDV, dv.NuocSX, dv.DonVi, nhapdct.DonGia } into kq
                            select new
                                {
                                    kq.Key.IDNhom,
                                    TenNhomDV = kq.Key.TenNhomCT,
                                    TieuNhomDV = kq.Key.TenTN,
                                    TenDV = kq.Key.TenDV,
                                    DVT = kq.Key.DonVi,
                                    DonGia = kq.Key.DonGia,
                                    NuocSX = kq.Key.NuocSX,

                                    TonDKSL = kq.Where(p => p.nhapdct.NgayNhap < tungay).Where(p => p.nhapdct.PLoai == 2 && p.nhapdct.KieuDon == 3).Sum(p => p.nhapdct.SoLuongX) -
                                              kq.Where(p => p.nhapdct.NgayNhap < tungay).Where(p => p.nhapdct.PLoai == 1 && p.nhapdct.KieuDon == 2).Sum(p => p.nhapdct.SoLuongN) -
                                              kq.Where(p => p.nhapdct.NgayNhap < tungay).Where(p => p.nhapdct.PLoai == 5).Sum(p => p.nhapdct.SoLuongSD),
                                    TonDKTT = kq.Where(p => p.nhapdct.NgayNhap < tungay).Where(p => p.nhapdct.PLoai == 2 && p.nhapdct.KieuDon == 3).Sum(p => p.nhapdct.ThanhTienX) -
                                              kq.Where(p => p.nhapdct.NgayNhap < tungay).Where(p => p.nhapdct.PLoai == 1 && p.nhapdct.KieuDon == 2).Sum(p => p.nhapdct.ThanhTienN) -
                                              kq.Where(p => p.nhapdct.NgayNhap < tungay).Where(p => p.nhapdct.PLoai == 5).Sum(p => p.nhapdct.ThanhTienSD),

                                    NhapTKSL = kq.Where(p => p.nhapdct.NgayNhap >= tungay).Where(p => p.nhapdct.NgayNhap <= denngay).Where(p => p.nhapdct.PLoai == 2 && p.nhapdct.KieuDon == 3).Sum(p => p.nhapdct.SoLuongX),
                                    NhapTKTT = kq.Where(p => p.nhapdct.NgayNhap >= tungay).Where(p => p.nhapdct.NgayNhap <= denngay).Where(p => p.nhapdct.PLoai == 2 && p.nhapdct.KieuDon == 3).Sum(p => p.nhapdct.ThanhTienX),

                                    TongNhapSL = kq.Where(p => p.nhapdct.NgayNhap < tungay).Where(p => p.nhapdct.PLoai == 2 && p.nhapdct.KieuDon == 3).Sum(p => p.nhapdct.SoLuongX) -
                                              kq.Where(p => p.nhapdct.NgayNhap < tungay).Where(p => p.nhapdct.PLoai == 1 && p.nhapdct.KieuDon == 2).Sum(p => p.nhapdct.SoLuongN) -
                                              kq.Where(p => p.nhapdct.NgayNhap < tungay).Where(p => p.nhapdct.PLoai == 5).Sum(p => p.nhapdct.SoLuongSD) +
                                              kq.Where(p => p.nhapdct.NgayNhap >= tungay).Where(p => p.nhapdct.NgayNhap <= denngay).Where(p => p.nhapdct.PLoai == 2 && p.nhapdct.KieuDon == 3).Sum(p => p.nhapdct.SoLuongX),
                                    TongNhapTT = kq.Where(p => p.nhapdct.NgayNhap < tungay).Where(p => p.nhapdct.PLoai == 2 && p.nhapdct.KieuDon == 3).Sum(p => p.nhapdct.ThanhTienX) -
                                              kq.Where(p => p.nhapdct.NgayNhap < tungay).Where(p => p.nhapdct.PLoai == 1 && p.nhapdct.KieuDon == 2).Sum(p => p.nhapdct.ThanhTienN) -
                                              kq.Where(p => p.nhapdct.NgayNhap < tungay).Where(p => p.nhapdct.PLoai == 5).Sum(p => p.nhapdct.ThanhTienSD) +
                                              kq.Where(p => p.nhapdct.NgayNhap >= tungay).Where(p => p.nhapdct.NgayNhap <= denngay).Where(p => p.nhapdct.PLoai == 2 && p.nhapdct.KieuDon == 3).Sum(p => p.nhapdct.ThanhTienX),

                                    //TongNhapSL = kq.Where(p => p.nhapdct.NgayNhap < tungay).Sum(p => p.nhapdct.SoLuongX) - kq.Where(p => p.nhapdct.NgayNhap < tungay).Sum(p => p.nhapdct.SoLuongSD) +
                                    //             kq.Where(p => p.nhapdct.NgayNhap >= tungay).Where(p => p.nhapdct.NgayNhap <= denngay).Sum(p => p.nhapdct.SoLuongX),
                                    //TongNhapTT = kq.Where(p => p.nhapdct.NgayNhap < tungay).Sum(p => p.nhapdct.ThanhTienX) - kq.Where(p => p.nhapdct.NgayNhap < tungay).Sum(p => p.nhapdct.ThanhTienSD) +
                                    //             kq.Where(p => p.nhapdct.NgayNhap >= tungay).Where(p => p.nhapdct.NgayNhap <= denngay).Sum(p => p.nhapdct.ThanhTienX), 
                                    TongTraSL = kq.Where(p => p.nhapdct.NgayNhap >= tungay).Where(p => p.nhapdct.NgayNhap <= denngay).Where(p => p.nhapdct.PLoai == 1 && p.nhapdct.KieuDon == 2).Sum(p => p.nhapdct.SoLuongN),
                                    TongTraTT = kq.Where(p => p.nhapdct.NgayNhap >= tungay).Where(p => p.nhapdct.NgayNhap <= denngay).Where(p => p.nhapdct.PLoai == 1 && p.nhapdct.KieuDon == 2).Sum(p => p.nhapdct.ThanhTienN),

                                    SDTKSL = kq.Where(p => p.nhapdct.NgayNhap >= tungay).Where(p => p.nhapdct.NgayNhap <= denngay).Sum(p => p.nhapdct.SoLuongSD),
                                    SDTKTT = kq.Where(p => p.nhapdct.NgayNhap >= tungay).Where(p => p.nhapdct.NgayNhap <= denngay).Sum(p => p.nhapdct.ThanhTienSD),

                                    TonCKSL = kq.Where(p => p.nhapdct.NgayNhap <= denngay).Where(p => p.nhapdct.PLoai == 2 && p.nhapdct.KieuDon == 3).Sum(p => p.nhapdct.SoLuongX) -
                                              kq.Where(p => p.nhapdct.NgayNhap <= denngay).Where(p => p.nhapdct.PLoai == 1 && p.nhapdct.KieuDon == 2).Sum(p => p.nhapdct.SoLuongN) -
                                              kq.Where(p => p.nhapdct.NgayNhap <= denngay).Where(p => p.nhapdct.PLoai == 5).Sum(p => p.nhapdct.SoLuongSD),
                                    TonCKTT = kq.Where(p => p.nhapdct.NgayNhap <= denngay).Where(p => p.nhapdct.PLoai == 2 && p.nhapdct.KieuDon == 3).Sum(p => p.nhapdct.ThanhTienX) -
                                              kq.Where(p => p.nhapdct.NgayNhap <= denngay).Where(p => p.nhapdct.PLoai == 1 && p.nhapdct.KieuDon == 2).Sum(p => p.nhapdct.ThanhTienN) -
                                              kq.Where(p => p.nhapdct.NgayNhap <= denngay).Where(p => p.nhapdct.PLoai == 5).Sum(p => p.nhapdct.ThanhTienSD)

                                }).Where(p => p.TonDKSL != 0 || p.TonCKSL != 0 || (p.NhapTKSL != 0 || p.SDTKSL != 0)).OrderBy(p => p.TieuNhomDV).ThenBy(p => p.TenDV).ToList();
                double TT = 0;
                qnxt = qnxt.Where(p => (id == 0 ? true : p.IDNhom == id)).OrderBy(p => p.TieuNhomDV).ThenBy(p => p.TenDV).ToList();
                if (qnxt.Count > 0)
                    TT = qnxt.Sum(p => p.TonCKTT);
                rep.TongTien.Value = DungChung.Ham.DocTienBangChu(TT, " đồng.");
                int[] _arrWidth = new int[] { };
                string[] _arr = new string[] { "0", "@", "@", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" };
                DungChung.Bien.MangHaiChieu = new Object[qnxt.Count + 1, 15];
                int num = 1;
                string[] _tieude = { "STT", "TenThuoc", "DVT", "DonGia", "Tồn ĐK - SL", "Tồn ĐK - TT", "Nhập TK - Sl", "Nhập TK - TT", "Tổng nhập - SL", "Tổng nhập - TT", "Sử dụng TK - SL", "Sử dụng TK - TT", "Tồn CK - SL", "Tồn CK - TT" };
                for (int i = 0; i < _tieude.Length; i++)
                {
                    DungChung.Bien.MangHaiChieu[0, i] = _tieude[i];
                }


                foreach (var r in qnxt)
                {
                    DungChung.Bien.MangHaiChieu[num, 0] = num;
                    DungChung.Bien.MangHaiChieu[num, 1] = r.TenDV;
                    DungChung.Bien.MangHaiChieu[num, 2] = r.DVT;
                    DungChung.Bien.MangHaiChieu[num, 3] = r.DonGia;
                    DungChung.Bien.MangHaiChieu[num, 4] = r.TonDKSL;
                    DungChung.Bien.MangHaiChieu[num, 5] = r.TonDKTT;
                    DungChung.Bien.MangHaiChieu[num, 6] = r.NhapTKSL;
                    DungChung.Bien.MangHaiChieu[num, 7] = r.NhapTKTT;
                    DungChung.Bien.MangHaiChieu[num, 8] = r.TongNhapSL;
                    DungChung.Bien.MangHaiChieu[num, 9] = r.TongNhapTT;
                    DungChung.Bien.MangHaiChieu[num, 10] = r.SDTKSL;
                    DungChung.Bien.MangHaiChieu[num, 11] = r.SDTKTT;
                    DungChung.Bien.MangHaiChieu[num, 12] = r.TonCKSL;
                    DungChung.Bien.MangHaiChieu[num, 13] = r.TonCKTT;
                    num++;
                }
                frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "Báo cáo NXT_SD", "C:\\TsBCNXT_SD.xls", true, this.Name);

                rep.DataSource = qnxt;
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
    }
}