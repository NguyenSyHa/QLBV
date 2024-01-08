using QLBV.Providers.Business.SyncMedicine;
using QLBV.DungChung;
using QLBV.Models.Business.TT27;
using QLBV.Utilities.Commons;
using QLBV_Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace QLBV.Forms.Circulars27
{
    public class Circulars27
    {
        private static SyncMedicineProvider _syncMedicineProvider;

        static string _baseURlCirculars27;
        public static string BaseURlCirculars27
        {
            get
            {
                if (string.IsNullOrEmpty(_baseURlCirculars27))
                {
                    _baseURlCirculars27 = System.Configuration.ConfigurationManager.AppSettings["Circulars27"];
                }

                return _baseURlCirculars27;
            }
        }

        public static string SendPrescriptions(QLBVEntities dataContext, BenhNhan bNhankb, int makp, int idDon, List<DThuocct> dThuoccts, List<ChanDoan> chanDoans)
        {
            var baseURl = BaseURlCirculars27;

            if (_syncMedicineProvider == null)
                _syncMedicineProvider = new SyncMedicineProvider();

            string message = string.Empty;

            if (bNhankb == null)
                return "Bạn chưa chọn bệnh nhân";

            // Lấy thông tin đăng nhập bệnh viện
            var heThong = dataContext.HTHONGs.FirstOrDefault(f => f.MaBV == Bien.MaBV);
            if (heThong == null)
                return "Thông tin đăng nhập bệnh viện không đúng"; ;

            // Lấy thông tin đăng nhập Bác sĩ
            var canBo = dataContext.CanBoes.FirstOrDefault(f => f.MaCB == Bien.MaCB);
            if (canBo == null)
                return "Thông tin đăng nhập Bác sĩ không đúng";

            var bNhankbEx = dataContext.TTboXungs.FirstOrDefault(f => f.MaBNhan == bNhankb.MaBNhan);
            var bNKB = dataContext.BNKBs.FirstOrDefault(w => w.MaBNhan == bNhankb.MaBNhan && w.MaKP == makp);

            #region Đăng nhập lấy Token 

            var resultApi = new BaseApiResponse<TokenResult, ErrorResult>();
            string Mabv = DungChung.Bien.MaBV;

            if (string.IsNullOrEmpty(AppConfig.TokenTT27_Doctor))
            {
                var dangNhapCSKCB = new DangNhapCSKCB()
                {
                    //ma_lien_thong_co_so_kham_chua_benh = "0130009",
                    //password = "123456qQ@"
                    ma_lien_thong_co_so_kham_chua_benh = heThong.MaLienThongCSKCB,
                    password = QLBV.Utilities.Commons.Security.Decrypt(heThong.MKDonThuocLienThong)
                };
                //if (Mabv.Substring(0, 2) == "24")
                //{
                //    resultApi = Task.Run(async () => await Utilities.Commons.AppApi.PostAsync<TokenResult, ErrorResult, DangNhapCSKCB>(AppConfig.BaseUrlTT27 = @"https://api.guidonthuoc.vn", "/api/auth/dang-nhap-co-so-kham-chua-benh", dangNhapCSKCB)).Result;
                //    //resultApi = Utilities.Commons.AppApi.PostAsync<TokenResult, ErrorResult, DangNhapCSKCB>(AppConfig.BaseUrlTT27 = @"https://api.guidonthuoc.vn", "/api/auth/dang-nhap-co-so-kham-chua-benh", dangNhapCSKCB);
                //}
                //else
                //{
                //resultApi = Utilities.Commons.AppApi.PostAsync<TokenResult, ErrorResult, DangNhapCSKCB>(AppConfig.BaseUrlTT27 = @"https://api.donthuocquocgia.vn", "/api/auth/dang-nhap-co-so-kham-chua-benh", dangNhapCSKCB);
                resultApi = Task.Run(async () => await Utilities.Commons.AppApi.PostAsync<TokenResult, ErrorResult, DangNhapCSKCB>(AppConfig.BaseUrlTT27 = baseURl, "/api/auth/dang-nhap-co-so-kham-chua-benh", dangNhapCSKCB)).Result;
                //}
                if (!resultApi.IsSuccess)
                {
                    if (resultApi.ContentFailure.error != null)
                    {
                        var sErro = string.Join(", ", resultApi.ContentFailure.error.ma_lien_thong_bac_si);
                        MessageBox.Show(sErro, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        return sErro;
                    }

                    return resultApi.Message;
                }
                if (resultApi != null && resultApi.ContentSuccess != null)
                {
                    AppConfig.TokenTT27 = resultApi.ContentSuccess.token;
                    AppConfig.TokenTypeTT27 = resultApi.ContentSuccess.token_type;
                }
            }

            #endregion

            #region Thêm mới bác sỹ

            var themBacSi = new DangNhapBacSi()
            {
                ma_lien_thong_bac_si = canBo.MaDinhDanh,
                ma_lien_thong_co_so_kham_chua_benh = heThong.MaLienThongCSKCB,
                password = QLBV.Utilities.Commons.Security.Decrypt(canBo.MKBacSi)
            };
            //if (Mabv.Substring(0, 2) == "24")
            //{
            //    var headers = new Dictionary<string, string>()
            //    {
            //        { "Authorization", AppConfig.TokenTT27 }
            //    };

            //    //resultApi = Utilities.Commons.AppApi.PostAsync<TokenResult, ErrorResult, DangNhapBacSi>(AppConfig.BaseUrlTT27 = @"https://api.guidonthuoc.vn", "/api/v1/them-bac-si", themBacSi, headers).Result;
            //    resultApi = Task.Run(async () => await Utilities.Commons.AppApi.PostAsync<TokenResult, ErrorResult, DangNhapBacSi>(AppConfig.BaseUrlTT27 = @"https://api.guidonthuoc.vn", "/api/v1/them-bac-si", themBacSi, headers)).Result;
            //}
            //else
            //{
            var headers = new Dictionary<string, string>()
                {
                    { "Authorization", AppConfig.TokenTT27 }
                };

            //resultApi = Utilities.Commons.AppApi.PostAsync<TokenResult, ErrorResult, DangNhapBacSi>(AppConfig.BaseUrlTT27 = @"https://api.donthuocquocgia.vn", "/api/v1/them-bac-si", themBacSi, headers).Result;
            resultApi = Task.Run(async () => await Utilities.Commons.AppApi.PostAsync<TokenResult, ErrorResult, DangNhapBacSi>(AppConfig.BaseUrlTT27 = baseURl, "/api/v1/them-bac-si", themBacSi, headers)).Result;
            //}

            if (!resultApi.IsSuccess)
            {
                if (resultApi.ContentFailure.error != null)
                {
                    var sErro = string.Join(", ", resultApi.ContentFailure.error.ma_lien_thong_bac_si);
                    MessageBox.Show(sErro, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return sErro;
                }

                return resultApi.Message;
            }

            #endregion

            #region Đăng nhập bác sĩ

            if (string.IsNullOrEmpty(AppConfig.TokenTT27_Doctor))
            {
                var dangNhapBacSi = new DangNhapBacSi()
                {
                    //ma_lien_thong_bac_si = "01001471HD-CCHN",
                    //ma_lien_thong_co_so_kham_chua_benh = "0130009",
                    //password = "123456Ht@"
                    ma_lien_thong_bac_si = canBo.MaDinhDanh,
                    ma_lien_thong_co_so_kham_chua_benh = heThong.MaLienThongCSKCB,
                    password = QLBV.Utilities.Commons.Security.Decrypt(canBo.MKBacSi)
                };
                //if (Mabv.Substring(0, 2) == "24")
                //{
                //    //resultApi = Utilities.Commons.AppApi.PostAsync<TokenResult, DangNhapBacSi>(AppConfig.BaseUrlTT27 = @"https://api.guidonthuoc.vn", "/api/auth/dang-nhap-bac-si", dangNhapBacSi);
                //    resultApi = Task.Run(async () => await Utilities.Commons.AppApi.PostAsync<TokenResult, ErrorResult, DangNhapBacSi>(AppConfig.BaseUrlTT27 = @"https://api.guidonthuoc.vn", "/api/auth/dang-nhap-bac-si", dangNhapBacSi)).Result;
                //}
                //else
                //{
                //resultApi = Utilities.Commons.AppApi.PostAsync<TokenResult, DangNhapBacSi>(AppConfig.BaseUrlTT27 = @"https://api.donthuocquocgia.vn", "/api/auth/dang-nhap-bac-si", dangNhapBacSi);
                resultApi = Task.Run(async () => await Utilities.Commons.AppApi.PostAsync<TokenResult, ErrorResult, DangNhapBacSi>(AppConfig.BaseUrlTT27 = baseURl, "/api/auth/dang-nhap-bac-si", dangNhapBacSi)).Result;
                //}
                if (!resultApi.IsSuccess)
                {
                    if (resultApi.ContentFailure.error != null)
                    {
                        var sErro = string.Join(", ", resultApi.ContentFailure.error.ma_lien_thong_bac_si);
                        MessageBox.Show(sErro, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        return sErro;
                    }

                    return resultApi.Message;
                }
                if (resultApi != null && resultApi.ContentSuccess != null)
                {
                    AppConfig.TokenTT27_Doctor = resultApi.ContentSuccess.token;
                    AppConfig.TokenTypeTT27_Doctor = resultApi.ContentSuccess.token_type;
                }
            }

            #endregion

            #region Đồng bộ đơn thuốc
            string maDonThuoc = string.Empty;
            var dthuocSelected = dataContext.DThuocs.FirstOrDefault(f => f.IDDon == idDon);
            if (dthuocSelected != null)
            {
                if (dThuoccts != null && dThuoccts.Count > 0)
                {
                    var maDVs = dThuoccts.Select(s => s.MaDV).ToList();
                    var dichVus = dataContext.DichVus.Where(w => maDVs.Contains(w.MaDV)).ToList();
                    var tieuNhomIds = dichVus.Select(s => s.IdTieuNhom).ToList();
                    var tieuNhoms = dataContext.TieuNhomDVs.Where(w => tieuNhomIds.Contains(w.IdTieuNhom)).ToList();

                    var donThuoc = new DonThuoc();
                    donThuoc.ho_ten_benh_nhan = bNhankb.TenBNhan;
                    donThuoc.dia_chi = bNhankb.DChi;

                    int year = 1;
                    int month = 1;
                    int day = 1;

                    try
                    {
                        if (!string.IsNullOrEmpty(bNhankb.NamSinh))
                        {
                            year = int.Parse(bNhankb.NamSinh);
                        }

                        if (!string.IsNullOrEmpty(bNhankb.ThangSinh))
                        {
                            month = int.Parse(bNhankb.ThangSinh);
                        }

                        if (!string.IsNullOrEmpty(bNhankb.NgaySinh))
                        {
                            day = int.Parse(bNhankb.NgaySinh);
                        }

                        donThuoc.ngay_sinh_benh_nhan = (new DateTime(year, month, day)).ToString("dd/MM/yyyy");
                    }
                    catch
                    {
                        donThuoc.ngay_sinh_benh_nhan = (new DateTime(year, month, day)).ToString("dd/MM/yyyy");
                    }

                    if (bNhankbEx != null && !string.IsNullOrEmpty(bNhankbEx.SoKSinh) && bNhankbEx.SoKSinh.Length >= 12)
                    {
                        try
                        {
                            donThuoc.ma_dinh_danh_cong_dan = bNhankbEx.SoKSinh.Substring(bNhankbEx.SoKSinh.Length - 12);
                        }
                        catch
                        {
                            donThuoc.ma_dinh_danh_cong_dan = null;
                        }
                    }

                    try
                    {
                        if (!string.IsNullOrEmpty(bNhankbEx.CanNang_ChieuCao) && !string.IsNullOrWhiteSpace(bNhankbEx.CanNang_ChieuCao))
                        {
                            var canNang_ChieuCao = bNhankbEx.CanNang_ChieuCao;
                            var canNangChieuCao = bNhankbEx.CanNang_ChieuCao.Split(new char[] { ',', ';', ':' }, StringSplitOptions.RemoveEmptyEntries).Select(s => s.Trim()).ToList();

                            if (canNangChieuCao != null && canNangChieuCao.Count > 0)
                            {
                                var canNang = canNangChieuCao.FirstOrDefault();

                                var canNang1 = decimal.Parse(canNang.Replace(",", "."));
                                var canNang2 = decimal.Parse(canNang.Replace(".", ","));

                                donThuoc.can_nang = Math.Min(canNang1, canNang2);
                            }
                        }
                    }
                    catch
                    {
                        donThuoc.can_nang = 0;
                    }

                    donThuoc.gioi_tinh = bNhankb.GTinh.GetValueOrDefault() == 0 ? 3 : (bNhankb.GTinh.GetValueOrDefault() == 1 ? 2 : 1);

                    if (bNhankb != null && !string.IsNullOrEmpty(bNhankb.SThe) && bNhankb.SThe.Length > 10)
                    {
                        try
                        {
                            donThuoc.ma_so_the_bao_hiem_y_te = bNhankb.SThe.Substring(bNhankb.SThe.Length - 10);
                        }
                        catch
                        {
                            donThuoc.ma_so_the_bao_hiem_y_te = null;
                        }
                    }

                    donThuoc.ma_dinh_danh_y_te = donThuoc.ma_so_the_bao_hiem_y_te;

                    donThuoc.thong_tin_nguoi_giam_ho = bNhankbEx == null ? null : bNhankbEx.NThan + ", " + bNhankbEx.DThoaiNT + ", " + bNhankbEx.DCNguoiThan;

                    donThuoc.chan_doan = chanDoans;

                    donThuoc.luu_y = dthuocSelected.GhiChu;

                    if (bNhankb.NoiTru != null)
                        donThuoc.hinh_thuc_dieu_tri = bNhankb.NoiTru.Value == 1 ? 1 : 2;
                    else
                        donThuoc.hinh_thuc_dieu_tri = 2;

                    foreach (var item in dThuoccts)
                    {
                        var loai_don_thuoc = "c";

                        var dichVu = dichVus.Find(f => f.MaDV == item.MaDV);
                        var tieuNhom = tieuNhoms.Find(f => f.IdTieuNhom == dichVu.IdTieuNhom);

                        if (tieuNhom != null)
                        {
                            var tenRG = tieuNhom.TenRG.ToLower();
                            if (tenRG.Contains("hướng tâm thần")
                                || tenRG.Contains("tiền chất")
                                || tenRG.Contains("hướng thần"))
                            {
                                loai_don_thuoc = "h";
                            }
                            else if (tenRG.Contains("gây nghiện"))
                            {
                                loai_don_thuoc = "n";
                            }
                            else
                            {
                                loai_don_thuoc = "c";
                            }
                        }

                        var donThuocCT = new ThongTinDonThuoc()
                        {
                            ma_thuoc = string.IsNullOrEmpty(dichVu.MaQD) ? "." : dichVu.MaQD,
                            biet_duoc = string.IsNullOrEmpty(dichVu.TenHC) ? "." : dichVu.TenHC,
                            ten_thuoc = string.IsNullOrEmpty(dichVu.TenDV) ? "." : dichVu.TenDV,
                            don_vi_tinh = string.IsNullOrEmpty(dichVu.DonVi) ? "." : dichVu.DonVi,
                            so_luong = item.SoLuong,
                            cach_dung = string.IsNullOrEmpty(dichVu.DuongD) ? "." : dichVu.DuongD,
                            loai_don_thuoc = loai_don_thuoc,
                            loi_dan = item.GhiChu,
                        };

                        donThuoc.thong_tin_don_thuoc.Add(donThuocCT);
                    }

                    if (bNhankbEx != null && bNhankbEx.DThoai != null)
                        donThuoc.so_dien_thoai_nguoi_kham_benh = bNhankbEx.DThoai;
                    else
                        donThuoc.so_dien_thoai_nguoi_kham_benh = "0000000000";

                    if (bNKB.NgayHen != null)
                    {
                        try
                        {
                            donThuoc.ngay_tai_kham = (DateTime.Today - bNKB.NgayHen.Value).Days;
                        }
                        catch
                        {
                            donThuoc.ngay_tai_kham = null;
                        }
                    }

                    donThuoc.ngay_gio_ke_don = dthuocSelected.NgayKe == null ? null : dthuocSelected.NgayKe.Value.ToString("yyyy-MM-dd HH:mm:ss");
                    donThuoc.dot_dung_thuoc = new DotDungThuoc()
                    {
                        dot = 1,
                        tu_ngay = dthuocSelected.NgayKe == null ? DateTime.Now.ToString("dd/MM/yyyy") : dthuocSelected.NgayKe.Value.ToString("dd/MM/yyyy"),
                        den_ngay = dthuocSelected.NgayKe == null ? DateTime.Now.ToString("dd/MM/yyyy") : dthuocSelected.NgayKe.Value.AddDays(1).ToString("dd/MM/yyyy")
                    };

                    // Gửi dữ liệu đơn thuốc
                    var cDonThuoc = donThuoc.Clone();
                    var hDonThuoc = donThuoc.Clone();
                    var nDonThuoc = donThuoc.Clone();

                    cDonThuoc.thong_tin_don_thuoc = cDonThuoc.thong_tin_don_thuoc.Where(w => w.loai_don_thuoc != "h" && w.loai_don_thuoc != "n").ToList();
                    if (cDonThuoc.thong_tin_don_thuoc != null && cDonThuoc.thong_tin_don_thuoc.Count > 0)
                    {
                        cDonThuoc.loai_don_thuoc = cDonThuoc.thong_tin_don_thuoc.FirstOrDefault().loai_don_thuoc;
                        cDonThuoc.ma_don_thuoc = Bien.MaBV + Helpers.RandomString(7) + "-" + "c";
                        cDonThuoc.loi_dan = string.Join(", ", cDonThuoc.thong_tin_don_thuoc.Select(s => s.loi_dan).ToList());
                        //if (Mabv.Substring(0, 2) == "24")
                        //{
                        //    message = Utilities.Commons.AppApi.PostAsync<object, Models.Business.TT27.DonThuoc>(AppConfig.BaseUrlTT27 = baseURl, @"/api/v1/gui-don-thuoc", cDonThuoc, AppConfig.TokenTT27_Doctor).Message;
                        //}
                        //else
                        //{
                        message = Utilities.Commons.AppApi.PostAsync<object, Models.Business.TT27.DonThuoc>(AppConfig.BaseUrlTT27 = baseURl, @"/api/v1/gui-don-thuoc", cDonThuoc, AppConfig.TokenTT27_Doctor).Message;
                        //}
                    }

                    hDonThuoc.thong_tin_don_thuoc = hDonThuoc.thong_tin_don_thuoc.Where(w => w.loai_don_thuoc == "h").ToList();
                    if (hDonThuoc.thong_tin_don_thuoc != null && hDonThuoc.thong_tin_don_thuoc.Count > 0)
                    {
                        hDonThuoc.loai_don_thuoc = hDonThuoc.thong_tin_don_thuoc.FirstOrDefault().loai_don_thuoc;
                        hDonThuoc.ma_don_thuoc = Bien.MaBV + Helpers.RandomString(7) + "-" + hDonThuoc.thong_tin_don_thuoc.FirstOrDefault().loai_don_thuoc;
                        hDonThuoc.loi_dan = string.Join(", ", hDonThuoc.thong_tin_don_thuoc.Select(s => s.loi_dan).ToList());
                        //if (Mabv.Substring(0, 2) == "24")
                        //{
                        //    message = Utilities.Commons.AppApi.PostAsync<object, Models.Business.TT27.DonThuoc>(AppConfig.BaseUrlTT27 = @"https://api.guidonthuoc.vn", @"/api/v1/gui-don-thuoc", hDonThuoc, AppConfig.TokenTT27_Doctor).Message;
                        //}
                        //else
                        //{
                        message = Utilities.Commons.AppApi.PostAsync<object, Models.Business.TT27.DonThuoc>(AppConfig.BaseUrlTT27 = baseURl, @"/api/v1/gui-don-thuoc", hDonThuoc, AppConfig.TokenTT27_Doctor).Message;
                        //}
                    }

                    nDonThuoc.thong_tin_don_thuoc = nDonThuoc.thong_tin_don_thuoc.Where(w => w.loai_don_thuoc == "n").ToList();
                    if (nDonThuoc.thong_tin_don_thuoc != null && nDonThuoc.thong_tin_don_thuoc.Count > 0)
                    {
                        nDonThuoc.loai_don_thuoc = nDonThuoc.thong_tin_don_thuoc.FirstOrDefault().loai_don_thuoc;
                        nDonThuoc.ma_don_thuoc = Bien.MaBV + Helpers.RandomString(7) + "-" + nDonThuoc.thong_tin_don_thuoc.FirstOrDefault().loai_don_thuoc;
                        nDonThuoc.loi_dan = string.Join(", ", nDonThuoc.thong_tin_don_thuoc.Select(s => s.loi_dan).ToList());
                        //if (Mabv.Substring(0, 2) == "24")
                        //{
                        //    message = Utilities.Commons.AppApi.PostAsync<object, Models.Business.TT27.DonThuoc>(AppConfig.BaseUrlTT27 = @"https://api.guidonthuoc.vn", @"/api/v1/gui-don-thuoc", nDonThuoc, AppConfig.TokenTT27_Doctor).Message;
                        //}
                        //else
                        //{
                        message = Utilities.Commons.AppApi.PostAsync<object, Models.Business.TT27.DonThuoc>(AppConfig.BaseUrlTT27 = baseURl, @"/api/v1/gui-don-thuoc", nDonThuoc, AppConfig.TokenTT27_Doctor).Message;
                        //}
                    }

                    #region Lưu đơn thuốc vào db

                    if (cDonThuoc.thong_tin_don_thuoc != null && cDonThuoc.thong_tin_don_thuoc.Count > 0)
                    {
                        maDonThuoc = cDonThuoc.ma_don_thuoc;
                    }

                    if (hDonThuoc.thong_tin_don_thuoc != null && hDonThuoc.thong_tin_don_thuoc.Count > 0)
                    {
                        maDonThuoc = hDonThuoc.ma_don_thuoc;
                    }

                    if (nDonThuoc.thong_tin_don_thuoc != null && nDonThuoc.thong_tin_don_thuoc.Count > 0)
                    {
                        maDonThuoc = nDonThuoc.ma_don_thuoc;
                    }
                    #endregion Lưu đơn thuốc vào db

                    if (!string.IsNullOrEmpty(message))
                    {
                        MessageBox.Show(message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        if (DungChung.Bien.MaBV == "27001")
                        {
                            _syncMedicineProvider.UpdateSyncedStatus(dthuocSelected.IDDon);
                        }
                        else
                        {
                            _syncMedicineProvider.UpdateSyncedStatus(dthuocSelected.IDDon);
                            MessageBox.Show("Gửi đơn thuốc thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.None);
                        }
                    }
                }
            }

            #endregion

            var dbThuocGui = dataContext.DThuocs.Where(x => x.IDDon == idDon).FirstOrDefault();
            dbThuocGui.MaDTQG = maDonThuoc;
            dataContext.SaveChanges();


            return message;
        }
    }
}
