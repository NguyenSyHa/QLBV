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
    public partial class frm_phieutamthuthainguyen : DevExpress.XtraEditors.XtraForm
    {
        public frm_phieutamthuthainguyen()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

        public void haminbaocaonoptienvienphi()
        {
            string MaCanBo = "";
            if (cboCanBoDuyet.EditValue != null)
            {
                MaCanBo = Convert.ToString(cboCanBoDuyet.EditValue);
            }
            int idbn = 99;
            var idKsk = data.DTBNs.FirstOrDefault(o => o.DTBN1.ToUpper().Contains("KSK")).IDDTBN;
            if (lupDTBN.EditValue != null)
            {
                idbn = Convert.ToInt32(lupDTBN.EditValue);
            }
            List<BC> _lbc = new List<BC>();
            if (rdNgay.SelectedIndex == 0)
            {
                #region tìm theo ngày thanh toán
                DateTime tungay = DungChung.Ham.NgayTu(luptungay.DateTime);
                DateTime denngay = DungChung.Ham.NgayDen(lupdenngay.DateTime);

                int NoiTru = rgNoiTru.SelectedIndex;
                var bnnoptien = from benhnhan in data.BenhNhans.Where(p => NoiTru == 3 ? true : (NoiTru == 0 ? (p.NoiTru == 0 && p.DTNT == false) : NoiTru == 4 ? (p.NoiTru == 0) : (NoiTru == 1 ? p.NoiTru == 1 : (p.NoiTru == 0 && p.DTNT == true)))).Where(p => idbn == 99 || p.IDDTBN == idbn)
                                join tamthu in data.TamUngs on benhnhan.MaBNhan equals tamthu.MaBNhan
                                where (tamthu.NgayThu >= tungay && tamthu.NgayThu <= denngay)
                                select new { benhnhan.MaBNhan, benhnhan.TenBNhan, tamthu.NgayThu, tamthu.SoHD, tamthu.QuyenHD, tamthu.SoTien, tamthu.PhanLoai };
                var bnnoptiens = bnnoptien.ToList().Select(p => new { p.TenBNhan, p.MaBNhan, ngaythu = p.NgayThu.Value.Date, p.SoHD, p.SoTien, p.QuyenHD, p.PhanLoai });
                //  frmIn inbaocao = new frmIn();
                BaoCao.fep_phieutamthuthainguyen nbaocao = new BaoCao.fep_phieutamthuthainguyen(rgPhanLoai.SelectedIndex);
                int pl = rgPhanLoai.SelectedIndex;

                if (DungChung.Bien.MaBV == "30005")
                {
                    var ketqua = (from h in bnnoptiens.Where(p => pl == 0 ? p.PhanLoai == 0 : (pl == 1 ? p.PhanLoai == 1 : (p.PhanLoai != 3)))
                                  select new
                                  {
                                      h.ngaythu,
                                      h.TenBNhan,
                                      h.MaBNhan,
                                      SoTien = h.PhanLoai == 0 ? h.SoTien : null,
                                      SoHD = h.PhanLoai == 0 ? h.SoHD : null,
                                      QuyenHD = h.PhanLoai == 0 ? h.QuyenHD : null,
                                      sotien1 = (h.PhanLoai == 1 || h.PhanLoai == 2) ? h.SoTien : null,
                                      sohoadon = (h.PhanLoai == 1 || h.PhanLoai == 2) ? h.SoHD : null,
                                      soquyen = (h.PhanLoai == 1 || h.PhanLoai == 2) ? h.QuyenHD : null,
                                      traTamUng = (h.PhanLoai == 4) ? h.SoTien : null
                                  }).ToList();

                    var ketqua1 = (from h in ketqua
                                   group h by new { h.MaBNhan, h.TenBNhan } into kq
                                   select new
                                   {
                                       ngaythu = kq.Max(p => p.ngaythu),
                                       kq.Key.TenBNhan,
                                       kq.Key.MaBNhan,
                                       SoTien = kq.Where(p => p.SoTien != null).Sum(p => p.SoTien),
                                       SoHD = string.Join(",", kq.Where(p => p.SoHD != null && p.SoHD != "").Select(p => p.SoHD).ToList().Distinct()),//  h.PhanLoai == 0 ? h.SoHD : null,
                                       QuyenHD = string.Join(",", kq.Where(p => p.QuyenHD != null && p.QuyenHD != "").Select(p => p.QuyenHD).ToList().Distinct()),
                                       sotien1 = kq.Where(p => p.sotien1 != null).Sum(p => p.sotien1),
                                       sohoadon = string.Join(",", kq.Where(p => p.sohoadon != null && p.sohoadon != "").Select(p => p.sohoadon).ToList().Distinct()),
                                       soquyen = string.Join(",", kq.Where(p => p.soquyen != null && p.soquyen != "").Select(p => p.soquyen).ToList().Distinct()),

                                   }).ToList();

                    var ketqua2 = (from h in ketqua1
                                   select new
                                   {
                                       h.ngaythu,
                                       h.MaBNhan,
                                       h.TenBNhan,
                                       h.SoTien,
                                       h.SoHD,
                                       h.soquyen,
                                       h.sohoadon,
                                       h.sotien1,
                                       h.QuyenHD,
                                       thu = h.sotien1 - h.SoTien > 0 ? h.sotien1 - h.SoTien : 0,
                                       chi = h.sotien1 - h.SoTien < 0 ? h.SoTien - h.sotien1 : 0
                                   }).ToList();
                    #region xuất Excel

                    string[] _arr = new string[] { "0", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@" };
                    int[] _arrWidth = new int[] { };
                    // int[] _arrWidth = new int[] { }; có thể gọi khi truyền kiểu fomatAutowith = true
                    int num = 1;
                    DungChung.Bien.MangHaiChieu = new Object[ketqua2.Count + 1, 11];
                    string[] _tieude = { "STT", "Mã BN", "Tên BN", "Số quyển tạm ứng", "Số biên lai tạm ứng", "Số tiền tạm ứng", "Số quyển HĐ thanh toán", "Số biên lai hóa đơn thanh toán", "Số tiền thanh toán", "Số tiền thu", "Số tiền chi" };
                    for (int i = 0; i < _tieude.Length; i++)
                    {
                        DungChung.Bien.MangHaiChieu[0, i] = _tieude[i].ToUpper();
                    }

                    //for (int i = 0; i <= 17; i++) {
                    //    DungChung.Bien.MangHaiChieu[0, i] = _arr[0,i];
                    //}
                    foreach (var r in ketqua2)
                    {
                        DungChung.Bien.MangHaiChieu[num, 0] = num;
                        DungChung.Bien.MangHaiChieu[num, 1] = r.MaBNhan;
                        DungChung.Bien.MangHaiChieu[num, 2] = r.TenBNhan;
                        DungChung.Bien.MangHaiChieu[num, 3] = r.QuyenHD;
                        DungChung.Bien.MangHaiChieu[num, 4] = r.SoHD;
                        DungChung.Bien.MangHaiChieu[num, 5] = r.SoTien;
                        DungChung.Bien.MangHaiChieu[num, 6] = r.soquyen;
                        DungChung.Bien.MangHaiChieu[num, 7] = r.sohoadon;
                        DungChung.Bien.MangHaiChieu[num, 8] = r.sotien1;
                        DungChung.Bien.MangHaiChieu[num, 9] = r.thu;
                        DungChung.Bien.MangHaiChieu[num, 10] = r.chi;
                        num++;
                    }
                    //QLBV_Library.QLBV_Ham.xuatExcelArr(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "Báo cáo cham cong PTTT", "C:\\BC_ChamCongPTTT.xls", true);
                    #endregion
                    nbaocao.DataSource = ketqua2;
                    frmIn inbaocao = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "Tổng hợp thu chi", "C:\\BC_TonghopThuChi.xls", false, this.Name);
                    nbaocao.tungays1 = luptungay.DateTime.ToShortDateString();
                    nbaocao.demngays2 = lupdenngay.DateTime.ToShortDateString();
                    nbaocao.hamloatbaocaotamthu();
                    nbaocao.CreateDocument();
                    inbaocao.prcIN.PrintingSystem = nbaocao.PrintingSystem;
                    inbaocao.ShowDialog();
                }
                else
                {
                    var ketqua = (from h in bnnoptiens.Where(p => pl == 0 ? p.PhanLoai == 0 : (pl == 1 ? p.PhanLoai == 1 : (pl == 2 ? p.PhanLoai == 7 : (p.PhanLoai != 3))))
                                  select new
                                  {
                                      h.ngaythu,
                                      h.TenBNhan,
                                      h.MaBNhan,
                                      SoTien = (h.PhanLoai == 0 || h.PhanLoai == 7) ? h.SoTien : null,
                                      SoHD = (h.PhanLoai == 0 || h.PhanLoai == 7) ? h.SoHD : null,
                                      QuyenHD = (h.PhanLoai == 0 || h.PhanLoai == 7) ? h.QuyenHD : null,
                                      sotien1 = h.PhanLoai == 1 ? h.SoTien : null,
                                      sohoadon = h.PhanLoai == 1 ? h.SoHD : null,
                                      soquyen = h.PhanLoai == 1 ? h.QuyenHD : null,
                                      chi = h.PhanLoai == 2 ? h.SoTien : null
                                  }).ToList();
                    nbaocao.DataSource = ketqua.ToList();
                    nbaocao.tungays1 = luptungay.DateTime.ToShortDateString();
                    nbaocao.demngays2 = lupdenngay.DateTime.ToShortDateString();
                    nbaocao.hamloatbaocaotamthu();
                    nbaocao.CreateDocument();

                    #region xuất Excel

                    string[] _arr = new string[] { "0", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@" };
                    int[] _arrWidth = new int[] { };
                    // int[] _arrWidth = new int[] { }; có thể gọi khi truyền kiểu fomatAutowith = true
                    int num = 1;
                    DungChung.Bien.MangHaiChieu = new Object[ketqua.Count + 1, 11];
                    string[] _tieude = { "STT", "Mã BN", "Tên BN", "Số quyển tạm ứng", "Số biên lai tạm ứng", "Số tiền tạm ứng", "Số quyển HĐ thanh toán", "Số biên lai hóa đơn thanh toán", "Số tiền thanh toán", "Số tiền thu", "Số tiền chi" };
                    for (int i = 0; i < _tieude.Length; i++)
                    {
                        DungChung.Bien.MangHaiChieu[0, i] = _tieude[i].ToUpper();
                    }

                    //for (int i = 0; i <= 17; i++) {
                    //    DungChung.Bien.MangHaiChieu[0, i] = _arr[0,i];
                    //}
                    foreach (var r in ketqua)
                    {
                        DungChung.Bien.MangHaiChieu[num, 0] = num;
                        DungChung.Bien.MangHaiChieu[num, 1] = r.MaBNhan;
                        DungChung.Bien.MangHaiChieu[num, 2] = r.TenBNhan;
                        DungChung.Bien.MangHaiChieu[num, 3] = r.QuyenHD;
                        DungChung.Bien.MangHaiChieu[num, 4] = r.SoHD;
                        DungChung.Bien.MangHaiChieu[num, 5] = r.SoTien;
                        DungChung.Bien.MangHaiChieu[num, 6] = r.soquyen;
                        DungChung.Bien.MangHaiChieu[num, 7] = r.sohoadon;
                        DungChung.Bien.MangHaiChieu[num, 8] = r.sotien1;
                        DungChung.Bien.MangHaiChieu[num, 9] = r.sotien1;
                        DungChung.Bien.MangHaiChieu[num, 10] = r.chi;
                        num++;
                    }
                    //QLBV_Library.QLBV_Ham.xuatExcelArr(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "Báo cáo cham cong PTTT", "C:\\BC_ChamCongPTTT.xls", true);
                    #endregion
                    frmIn inbaocao = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "Tổng hợp thu chi", "C:\\BC_TonghopThuChi.xls", false, this.Name);
                    inbaocao.prcIN.PrintingSystem = nbaocao.PrintingSystem;
                    inbaocao.ShowDialog();
                }

                #endregion
            }
            else
            {
                #region tìm theo ngày duyệt

                var maCbo = _canbo.Select(o => o.MaCanBo);
                DateTime tungay = DungChung.Ham.NgayTu(luptungay.DateTime);
                DateTime denngay = DungChung.Ham.NgayDen(lupdenngay.DateTime);
                int NoiTru = rgNoiTru.SelectedIndex;
                var qbn = (from tu in data.TamUngs.Where(p => p.NgayThu >= tungay && p.NgayThu <= denngay && (p.PhanLoai == 1 || p.PhanLoai == 2)) select tu).ToList();
                var bnnoptienAll = (from benhnhan in data.BenhNhans.Where(p => NoiTru == 3 ? true : (NoiTru == 0 ? (p.NoiTru == 0 && p.DTNT == false) : NoiTru == 4 ? (p.NoiTru == 0) : (NoiTru == 1 ? p.NoiTru == 1 : (p.NoiTru == 0 && p.DTNT == true)))).Where(p => idbn == 99 || p.IDDTBN == idbn)
                                    join tamthu in data.TamUngs on benhnhan.MaBNhan equals tamthu.MaBNhan //code theo ngày duyệt lấy theo mã cán bộ 
                                    join cb in maCbo.Where(o => (MaCanBo == "tatca" || MaCanBo == "") ? true : (o == MaCanBo)) on tamthu.MaCB equals cb
                                    join ravien in data.RaViens on benhnhan.MaBNhan equals ravien.MaBNhan
                                    join kp in data.KPhongs on ravien.MaKP equals kp.MaKP
                                    where (tamthu.NgayThu <= denngay)
                                    select new { benhnhan.MaBNhan, benhnhan.TenBNhan, tamthu.NgayThu, tamthu.SoHD, tamthu.QuyenHD, tamthu.SoTien, tamthu.PhanLoai, kp.TenKP }).ToList();

                if (DungChung.Bien.MaBV == "30005" && idbn == 99)
                {
                    var ravien = data.RaViens.Where(o => true).Select(o => o.MaBNhan).ToList();
                    var q0 = (from benhnhan in data.BenhNhans.Where(p => NoiTru == 3 ? true : (NoiTru == 0 ? (p.NoiTru == 0 && p.DTNT == false) : NoiTru == 4 ? (p.NoiTru == 0) : (NoiTru == 1 ? p.NoiTru == 1 : (p.NoiTru == 0 && p.DTNT == true)))).Where(p => p.IDDTBN == idKsk)
                              join tamthu in data.TamUngs on benhnhan.MaBNhan equals tamthu.MaBNhan //code theo ngày duyệt lấy theo mã cán bộ 
                              join cb in maCbo.Where(o => (MaCanBo == "tatca" || MaCanBo == "") ? true : (o == MaCanBo)) on tamthu.MaCB equals cb
                              join kp in data.KPhongs on benhnhan.MaKP equals kp.MaKP
                              where (tamthu.NgayThu <= denngay)
                              select new { benhnhan.MaBNhan, benhnhan.TenBNhan, tamthu.NgayThu, tamthu.SoHD, tamthu.QuyenHD, tamthu.SoTien, tamthu.PhanLoai, TenKP = kp.TenKP }).ToList();
                    var bnKskNopTien = q0.Where(o => !ravien.Contains(o.MaBNhan)).ToList();

                    bnnoptienAll.AddRange(bnKskNopTien);
                }

                var bnnoptien = (from bn in qbn
                                 join tu in bnnoptienAll on bn.MaBNhan equals tu.MaBNhan
                                 select new { bn.MaBNhan, tu.TenBNhan, NgayThu = tu.NgayThu.Value.Date, tu.SoHD, tu.QuyenHD, tu.SoTien, tu.PhanLoai, tu.TenKP }).ToList();


                var bnnoptiens = bnnoptien.ToList().Select(p => new { p.TenBNhan, p.MaBNhan, ngaythu = p.NgayThu, p.SoHD, p.SoTien, p.QuyenHD, p.PhanLoai, p.TenKP });

                BaoCao.fep_phieutamthuthainguyen nbaocao = new BaoCao.fep_phieutamthuthainguyen();
                int pl = rgPhanLoai.SelectedIndex;

                if (DungChung.Bien.MaBV == "30005")
                {
                    var ketqua = (from h in bnnoptiens.Where(p => pl == 0 ? p.PhanLoai == 0 : (pl == 1 ? p.PhanLoai == 1 : (p.PhanLoai != 3)))
                                  select new BC
                                  {
                                      ngaythu = h.ngaythu,
                                      TenBNhan = h.TenBNhan,
                                      MaBNhan = h.MaBNhan,
                                      TenKP = h.TenKP,
                                      SoTien = h.PhanLoai == 0 ? h.SoTien : null,
                                      SoHD = h.PhanLoai == 0 ? h.SoHD : null,
                                      QuyenHD = h.PhanLoai == 0 ? h.QuyenHD : null,
                                      sotien1 = (h.PhanLoai == 1 || h.PhanLoai == 2) ? h.SoTien : null,
                                      sohoadon = (h.PhanLoai == 1 || h.PhanLoai == 2) ? h.SoHD : null,
                                      soquyen = (h.PhanLoai == 1 || h.PhanLoai == 2) ? h.QuyenHD : null

                                  }).ToList();

                    var ketqua1 = (from h in ketqua
                                   group h by new { h.MaBNhan, h.TenBNhan } into kq
                                   select new BC
                                   {
                                       ngaythu = kq.Max(p => p.ngaythu),
                                       TenBNhan = kq.Key.TenBNhan,
                                       MaBNhan = kq.Key.MaBNhan,
                                       TenKP = kq.Select(p => p.TenKP).First().ToString(),
                                       SoTien = kq.Where(p => p.SoTien != null).Sum(p => p.SoTien),
                                       SoHD = string.Join(",", kq.Where(p => p.SoHD != null && p.SoHD != "").Select(p => p.SoHD).ToList().Distinct()),//  h.PhanLoai == 0 ? h.SoHD : null,
                                       QuyenHD = string.Join(",", kq.Where(p => p.QuyenHD != null && p.QuyenHD != "").Select(p => p.QuyenHD).ToList().Distinct()),
                                       sotien1 = kq.Where(p => p.sotien1 != null).Sum(p => p.sotien1),
                                       sohoadon = string.Join(",", kq.Where(p => p.sohoadon != null && p.sohoadon != "").Select(p => p.sohoadon).ToList().Distinct()),
                                       soquyen = string.Join(",", kq.Where(p => p.soquyen != null && p.soquyen != "").Select(p => p.soquyen).ToList().Distinct()),

                                   }).ToList();

                    var ketqua2 = (from h in ketqua1
                                   select new BC
                                   {
                                       ngaythu = h.ngaythu,
                                       MaBNhan = h.MaBNhan,
                                       TenBNhan = h.TenBNhan,
                                       SoTien = h.SoTien,
                                       SoHD = h.SoHD,
                                       TenKP = h.TenKP,
                                       soquyen = h.soquyen,
                                       sohoadon = h.sohoadon,
                                       sotien1 = h.sotien1,
                                       QuyenHD = h.QuyenHD,
                                       thu = h.sotien1 - h.SoTien > 0 ? h.sotien1 - h.SoTien : 0,
                                       chi = h.sotien1 - h.SoTien < 0 ? h.SoTien - h.sotien1 : 0
                                   }).ToList();

                    nbaocao.DataSource = ketqua2.ToList();
                    #region xuất Excel

                    string[] _arr = new string[] { "0", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@" };
                    int[] _arrWidth = new int[] { };
                    // int[] _arrWidth = new int[] { }; có thể gọi khi truyền kiểu fomatAutowith = true
                    int num = 1;
                    DungChung.Bien.MangHaiChieu = new Object[ketqua2.Count + 1, 11];
                    string[] _tieude = { "STT", "Mã BN", "Tên BN", "Số quyển tạm ứng", "Số biên lai tạm ứng", "Số tiền tạm ứng", "Số quyển HĐ thanh toán", "Số biên lai hóa đơn thanh toán", "Số tiền thanh toán", "Số tiền thu", "Số tiền chi" };
                    for (int i = 0; i < _tieude.Length; i++)
                    {
                        DungChung.Bien.MangHaiChieu[0, i] = _tieude[i].ToUpper();
                    }

                    //for (int i = 0; i <= 17; i++) {
                    //    DungChung.Bien.MangHaiChieu[0, i] = _arr[0,i];
                    //}
                    foreach (var r in ketqua2)
                    {
                        DungChung.Bien.MangHaiChieu[num, 0] = num;
                        DungChung.Bien.MangHaiChieu[num, 1] = r.MaBNhan;
                        DungChung.Bien.MangHaiChieu[num, 2] = r.TenBNhan;
                        DungChung.Bien.MangHaiChieu[num, 3] = r.QuyenHD;
                        DungChung.Bien.MangHaiChieu[num, 4] = r.SoHD;
                        DungChung.Bien.MangHaiChieu[num, 5] = r.SoTien;
                        DungChung.Bien.MangHaiChieu[num, 6] = r.soquyen;
                        DungChung.Bien.MangHaiChieu[num, 7] = r.sohoadon;
                        DungChung.Bien.MangHaiChieu[num, 8] = r.sotien1;
                        DungChung.Bien.MangHaiChieu[num, 9] = r.thu;
                        DungChung.Bien.MangHaiChieu[num, 10] = r.chi;
                        num++;
                    }
                    //QLBV_Library.QLBV_Ham.xuatExcelArr(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "Báo cáo cham cong PTTT", "C:\\BC_ChamCongPTTT.xls", true);
                    #endregion
                    frmIn inbaocao = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "Tổng hợp thu chi", "C:\\BC_TonghopThuChi.xls", false, this.Name);
                    nbaocao.tungays1 = luptungay.DateTime.ToShortDateString();
                    nbaocao.demngays2 = lupdenngay.DateTime.ToShortDateString();
                    nbaocao.hamloatbaocaotamthu();
                    nbaocao.CreateDocument();
                    inbaocao.prcIN.PrintingSystem = nbaocao.PrintingSystem;
                    inbaocao.ShowDialog();
                }
                else
                {
                    var ketqua = (from h in bnnoptiens.Where(p => pl == 0 ? p.PhanLoai == 0 : (pl == 1 ? p.PhanLoai == 1 : (pl == 2 ? p.PhanLoai == 7 : (p.PhanLoai != 3))))
                                  select new
                                  {
                                      h.ngaythu,
                                      h.TenBNhan,
                                      h.MaBNhan,
                                      SoTien = (h.PhanLoai == 0 || h.PhanLoai == 7) ? h.SoTien : null,
                                      SoHD = (h.PhanLoai == 0 || h.PhanLoai == 7) ? h.SoHD : null,
                                      QuyenHD = (h.PhanLoai == 0 || h.PhanLoai == 7) ? h.QuyenHD : null,
                                      sotien1 = h.PhanLoai == 1 ? h.SoTien : null,
                                      sohoadon = h.PhanLoai == 1 ? h.SoHD : null,
                                      soquyen = h.PhanLoai == 1 ? h.QuyenHD : null,
                                      chi = h.PhanLoai == 2 ? h.SoTien : null
                                  }).ToList();
                    nbaocao.DataSource = ketqua.ToList();
                    nbaocao.tungays1 = luptungay.DateTime.ToShortDateString();
                    nbaocao.demngays2 = lupdenngay.DateTime.ToShortDateString();
                    nbaocao.hamloatbaocaotamthu();
                    nbaocao.CreateDocument();
                    #region xuất Excel

                    string[] _arr = new string[] { "0", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@" };
                    int[] _arrWidth = new int[] { };
                    // int[] _arrWidth = new int[] { }; có thể gọi khi truyền kiểu fomatAutowith = true
                    int num = 1;
                    DungChung.Bien.MangHaiChieu = new Object[ketqua.Count + 1, 11];
                    string[] _tieude = { "STT", "Mã BN", "Tên BN", "Số quyển tạm ứng", "Số biên lai tạm ứng", "Số tiền tạm ứng", "Số quyển HĐ thanh toán", "Số biên lai hóa đơn thanh toán", "Số tiền thanh toán", "Số tiền thu", "Số tiền chi" };
                    for (int i = 0; i < _tieude.Length; i++)
                    {
                        DungChung.Bien.MangHaiChieu[0, i] = _tieude[i].ToUpper();
                    }

                    //for (int i = 0; i <= 17; i++) {
                    //    DungChung.Bien.MangHaiChieu[0, i] = _arr[0,i];
                    //}
                    foreach (var r in ketqua)
                    {
                        DungChung.Bien.MangHaiChieu[num, 0] = num;
                        DungChung.Bien.MangHaiChieu[num, 1] = r.MaBNhan;
                        DungChung.Bien.MangHaiChieu[num, 2] = r.TenBNhan;
                        DungChung.Bien.MangHaiChieu[num, 3] = r.QuyenHD;
                        DungChung.Bien.MangHaiChieu[num, 4] = r.SoHD;
                        DungChung.Bien.MangHaiChieu[num, 5] = r.SoTien;
                        DungChung.Bien.MangHaiChieu[num, 6] = r.soquyen;
                        DungChung.Bien.MangHaiChieu[num, 7] = r.sohoadon;
                        DungChung.Bien.MangHaiChieu[num, 8] = r.sotien1;
                        DungChung.Bien.MangHaiChieu[num, 9] = r.sotien1;
                        DungChung.Bien.MangHaiChieu[num, 10] = r.chi;
                        num++;
                    }
                    //QLBV_Library.QLBV_Ham.xuatExcelArr(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "Báo cáo cham cong PTTT", "C:\\BC_ChamCongPTTT.xls", true);
                    #endregion
                    frmIn inbaocao = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "Tổng hợp thu chi", "C:\\BC_TonghopThuChi.xls", false, this.Name);
                    inbaocao.prcIN.PrintingSystem = nbaocao.PrintingSystem;
                    inbaocao.ShowDialog();
                }



                #endregion
            }

        }
        public class BC
        {

            public DateTime ngaythu { get; set; }

            public int? MaBNhan { get; set; }

            public string TenBNhan { get; set; }

            public double? SoTien { get; set; }

            public string SoHD { get; set; }

            public string soquyen { get; set; }

            public string sohoadon { get; set; }

            public double? sotien1 { get; set; }
            public string TenKP { get; set; }

            public string QuyenHD { get; set; }

            public double? thu { get; set; }

            public double? chi { get; set; }
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            haminbaocaonoptienvienphi();
        }

        private void frm_phieutamthuthainguyen_Load(object sender, EventArgs e)
        {
            if(DungChung.Bien.MaBV!="24012")
            {
                rgPhanLoai.Properties.Items.Remove(rgPhanLoai.Properties.Items[2]);
            } else
            {
                rgPhanLoai.Properties.Items[2].Description = "Tạm thu phòng nhu cầu";
            }
            luptungay.DateTime = DateTime.Now;
            lupdenngay.DateTime = DateTime.Now;
            rgNoiTru.SelectedIndex = 3;
            rgPhanLoai.SelectedIndex = 0;
            var qdtbn = data.DTBNs.ToList();
            qdtbn.Insert(0, new DTBN { IDDTBN = 99, DTBN1 = "Tất cả" });
            lupDTBN.Properties.DataSource = qdtbn;
            lupDTBN.EditValue = lupDTBN.Properties.GetKeyValueByDisplayText("Tất cả");

            if (rdNgay.SelectedIndex == 1)
            {
                cboCanBoDuyet.Enabled = true;

            }
            else
            {
                cboCanBoDuyet.Enabled = false;
            }
            DsCanBoDuyet();
            cboCanBoDuyet.Properties.DataSource = _canbo.ToList();
            cboCanBoDuyet.Properties.DisplayMember = "TenCanBo";
            cboCanBoDuyet.Properties.ValueMember = "MaCanBo";

        }
        class canbo
        {
            private string macanbo;
            private string tencanbo;
            public string MaCanBo
            { set { macanbo = value; } get { return macanbo; } }

            public string TenCanBo
            { set { tencanbo = value; } get { return tencanbo; } }
        }
        List<canbo> _canbo = new List<canbo>();
        void DsCanBoDuyet()
        {
            var CanBo = (from cb in data.TamUngs
                         join cbct in data.CanBoes on cb.MaCB equals cbct.MaCB
                         select new
                         {
                             cb.MaCB,
                             cbct.TenCB,
                         }
                       ).Distinct().ToList();
            _canbo.Add(new canbo { MaCanBo = "tatca", TenCanBo = "Tất cả" });
            foreach (var a in CanBo)
            {
                canbo cb = new canbo();
                cb.MaCanBo = a.MaCB;
                cb.TenCanBo = a.TenCB;
                _canbo.Add(cb);
            }


        }
        private void labelControl4_Click(object sender, EventArgs e)
        {

        }

        private void labelControl2_Click(object sender, EventArgs e)
        {

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rdNgay_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdNgay.SelectedIndex == 1)
            {
                cboCanBoDuyet.Enabled = true;
                //DsCanBoDuyet();
            }
            else
            {
                cboCanBoDuyet.Enabled = false;
            }
        }

        private void cboCanBoDuyet_EditValueChanged(object sender, EventArgs e)
        {

        }


    }
}