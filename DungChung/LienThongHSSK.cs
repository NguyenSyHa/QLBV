using Newtonsoft.Json;
using RestSharp;
using System;using QLBV_Database;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace QLBV.DungChung
{
    public class LienThongHSSK
    {

        #region Upload
        public static string GetTokenUpload(int maBNhan, ref List<MessageHSSK> listMessage)
        {
            string token = "";
            var client = new RestClient(DungChung.Bien.UrlUploadHSSK + "api/authenticate");
            client.Timeout = 10000;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", string.Format("{{\r\n    \"username\": \"{0}\",\r\n    \"password\": \"{1}\"\r\n}}", DungChung.Bien.xmlFilePath_LIS[38], DungChung.Bien.xmlFilePath_LIS[39]), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                listMessage.Add(new MessageHSSK(maBNhan, "Không kết nối được api gettoken Upload"));
                return "";
            }
            var content = response.Content;
            try
            {
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    TokenUpload tk = JsonConvert.DeserializeObject<TokenUpload>(content);
                    token = tk.id_token;
                }
                else
                {
                    listMessage.Add(new MessageHSSK(maBNhan, "Lỗi kết nối lấy token Upload hồ sơ"));
                }
            }
            catch (Exception ex)
            {
                listMessage.Add(new MessageHSSK(maBNhan, "Lỗi lấy token Upload"));
            }
            return token;
        }

        private static KQUploadHS UpLoadHSSK(string xmlFilePath, string token, int maBNhan, ref List<MessageHSSK> listMessage)
        {
            var client = new RestClient(DungChung.Bien.UrlUploadHSSK + "api/upload");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "multipart/form-data");
            request.AddHeader("Authorization", string.Format("Bearer {0}", token));
            request.AddFile("file", xmlFilePath);
            IRestResponse response = client.Execute(request);
            var content = response.Content;
            if (response.StatusCode == HttpStatusCode.OK)
            {
                KQUploadHS kq = JsonConvert.DeserializeObject<KQUploadHS>(content);
                if (kq.status != "200")
                {
                    listMessage.Add(new MessageHSSK(maBNhan, "Gửi hồ sơ sức khỏe thất bại. Kết quả trả về: " + kq.status + "- " + kq.message));
                }
                return kq;
            }
            else
            {
                listMessage.Add(new MessageHSSK(maBNhan, "Lỗi kết nối gửi HSSK"));
                return new KQUploadHS();
            }
        }
        #endregion

        #region PID
        public KQPhienLamViecPID CallApi_TokenPID(int maBNhan, ref List<MessageHSSK> listMessage)
        {
            var client = new RestClient(DungChung.Bien.UrlPID + "Services/PID/thongTinBenhNhan/Login");
            client.Timeout = 10000;
            var request = new RestRequest(Method.GET);
            request.AddHeader("u", DungChung.Bien.xmlFilePath_LIS[36]);
            request.AddHeader("p", DungChung.Bien.xmlFilePath_LIS[37]);
            IRestResponse response = client.Execute(request);
            string str3 = response.Content;
            if (response.StatusCode != HttpStatusCode.OK)
            {
                listMessage.Add(new MessageHSSK(maBNhan, "Không kết nối được api gettoken PID"));
                return new KQPhienLamViecPID();
            }
            try
            {
                KQPhienLamViecPID dinh = JsonConvert.DeserializeObject<KQPhienLamViecPID>(str3);
                if (dinh.StatusCode == "200")
                {
                    return dinh;
                }
                else
                {
                    listMessage.Add(new MessageHSSK(maBNhan, dinh.Comment));
                    return dinh;
                }
            }
            catch (Exception ex)
            {
                listMessage.Add(new MessageHSSK(maBNhan, "Lỗi khi gửi"));
                return new KQPhienLamViecPID();
            }
            return new KQPhienLamViecPID();
        }

        public KQPID GetPID(string token, LayDinhDanh pid, int maBNhan, ref List<MessageHSSK> listMessage)
        {
            var client = new RestClient(DungChung.Bien.UrlPID + "Services/PID/thongTinBenhNhan/GetPID");
            client.Timeout = 10000;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("token", token);
            request.AddParameter("application/json", string.Format("{{\r\n    \"fULLNAME\": \"{0}\",\r\n    \"sEX\": \"{1}\",\r\n    \"dATEOFBIRTH\": \"{2}\",\r\n    \"pLACEOFBIRTH\": \"{3}\",\r\n    \"hOMETOWN\": \"{4}\",\r\n    \"hEALTHINSURANCEIDCARD\": \"{5}\",\r\n    \"iDENTIICATIONIDCARD\": \"{6}\",\r\n    \"pHONE\": \"{7}\",\r\n    \"cURENTPROVICECODE\": \"{8}\",\r\n    \"cURENTDISTRICTCODE\": \"{9}\",\r\n    \"cURENTCOMMUNECODE\": \"{10}\",\r\n    \"nAMEOFCONSERVATOR\": \"{11}\",\r\n    \"rELATIONWITHCONSERVATOR\": \"{12}\",\r\n    \"iDOFCONSERVATOR\": \"{13}\",\r\n    \"pHONEOFCONSERVATOR\": \"{14}\",\r\n    \"pROVINCIALBIRTHREGISTRATIONCODE\": \"{15}\"\r\n}}", pid.fULLNAME, pid.sEX, pid.dATEOFBIRTH, pid.pLACEOFBIRTH, pid.hOMETOWN, pid.hEALTHINSURANCEIDCARD, pid.iDENTIICATIONIDCARD, pid.pHONE, pid.cURENTPROVICECODE, pid.cURENTDISTRICTCODE, pid.cURENTCOMMUNECODE, pid.nAMEOFCONSERVATOR, pid.rELATIONWITHCONSERVATOR, pid.iDOFCONSERVATOR, pid.pHONEOFCONSERVATOR, pid.pROVINCIALBIRTHREGISTRATIONCODE), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                listMessage.Add(new MessageHSSK(maBNhan, "Không kết nối được api get PID"));
            }
            var content = response.Content;
            string str3 = content;
            try
            {
                KQPID dinh = JsonConvert.DeserializeObject<KQPID>(str3);
                if (dinh.StatusCode == "200")
                {
                    return dinh;
                }
                else
                {
                    listMessage.Add(new MessageHSSK(maBNhan, dinh.Comment));
                    return dinh;
                }
            }
            catch (Exception ex)
            {
                listMessage.Add(new MessageHSSK(maBNhan, "Lỗi khi gửi"));
            }
            return new KQPID();
        }
        #endregion

        public XElement GetThongTinchung(QLBV_Database.QLBVEntities _data, int maBNhan)
        {
            ThongTinChung moi = new ThongTinChung();
            var qbn = _data.BenhNhans.Where(p => p.MaBNhan == maBNhan).FirstOrDefault();
            var qttbx = _data.TTboXungs.Where(p => p.MaBNhan == maBNhan).FirstOrDefault();
            if (qbn != null)
            {
                moi.maThe = qbn.SThe;
                moi.hoTen = qbn.TenBNhan;
                moi.gioiTinh = qbn.GTinh == 0 ? "2" : (qbn.GTinh == 1 ? "1" : "");
                string ngaysinh = string.IsNullOrEmpty(qbn.NgaySinh) ? "00" : (qbn.NgaySinh.Length == 1 ? ("0" + qbn.NgaySinh.ToString()) : (qbn.NgaySinh.Length == 2 ? qbn.NgaySinh : "00"));
                string thangsinh = string.IsNullOrEmpty(qbn.ThangSinh) ? "00" : (qbn.ThangSinh.Length == 1 ? ("0" + qbn.ThangSinh.ToString()) : (qbn.ThangSinh.Length == 2 ? qbn.ThangSinh : "00"));
                moi.ngaySinh = qbn.NamSinh.ToString() + (!string.IsNullOrWhiteSpace(thangsinh) ? ("-" + thangsinh) : "") + (!string.IsNullOrWhiteSpace(thangsinh) ? ("-" + ngaysinh) : "");
                moi.diaChiChiTietHienTai = qbn.DChi;
                if (qttbx != null)
                {
                    var danToc = _data.DanTocs.FirstOrDefault(o => o.MaDT == qttbx.MaDT);
                    if (danToc != null)
                    {
                        moi.maDanToc = danToc.MaDanToc;
                    }
                    else
                        moi.maDanToc = qttbx.MaDT;
                    moi.maQuocTich = "VN";
                    if (qttbx.NgoaiKieu != null)
                    {
                        if (qttbx.NgoaiKieu.Trim().ToLower() == "việt nam" || qttbx.NgoaiKieu.Trim().ToLower() == "vn")
                            moi.maQuocTich = "VN";
                        else
                            moi.maQuocTich = qttbx.NgoaiKieu.Trim();
                    }

                    moi.maNgheNghiep = qttbx.MaNN;
                    moi.soCmnd = qttbx.SoKSinh;
                    if (qttbx.MaTinhKhaiSinh != null)
                        moi.maTinhKhaiSinh = qttbx.MaTinhKhaiSinh.Trim();
                    moi.maTinhHienTai = qttbx.MaTinh;
                    moi.maHuyenHienTai = qttbx.MaHuyen;
                    moi.maXaHienTai = qttbx.MaXa;
                    moi.dienThoaiDd = qttbx.DThoai;
                    if (qttbx.QuanHeNhanThan != null && qttbx.QuanHeNhanThan.Trim() != "" && qttbx.QuanHeNhanThan.Trim() != "0" && qttbx.NThan != null && qttbx.NThan.Trim() != "")
                    {
                        moi.quanHeNguoiChamSoc = qttbx.QuanHeNhanThan.Trim();
                        moi.nguoiChamSocChinh = qttbx.NThan.Trim();
                        moi.dienThoaiDiDDongNCSC = qttbx.DThoaiNT;
                    }

                    moi.diaChiChiTietThuongTru = qttbx.HKTT;
                    if (qttbx.NgayCapCMT != null && qttbx.NgayCapCMT.Value != DateTime.MinValue)
                        moi.ngayCap = qttbx.NgayCapCMT.Value.ToString("yyyy-MM-dd");

                }
            }
            #region xuất xml
            try
            {
                if (moi != null)
                {
                    var xEle1 = new XElement("ThongTinChung",
                                                   new XElement("maThe", moi.maThe),
                                                   new XElement("hoTen", moi.hoTen),
                                                   new XElement("gioiTinh", moi.gioiTinh),
                                                   new XElement("nhomMauHeAbo", moi.nhomMauHeAbo),
                                                   new XElement("nhomMauHeRh", moi.nhomMauHeRh),
                                                   new XElement("ngaySinh", moi.ngaySinh),
                                                   new XElement("maTinhKhaiSinh", moi.maTinhKhaiSinh),
                                                   new XElement("maDanToc", moi.maDanToc),
                                                   new XElement("maQuocTich", moi.maQuocTich),
                                                   new XElement("maTonGiao", moi.maTonGiao),
                                                   new XElement("maNgheNghiep", moi.maNgheNghiep),
                                                   new XElement("soCmnd", moi.soCmnd),
                                                   new XElement("ngayCap", moi.ngayCap),
                                                   new XElement("noiCap", moi.noiCap),
                                                   new XElement("diaChiChiTietThuongTru", moi.diaChiChiTietThuongTru),
                                                   new XElement("maTinhThuongTru", moi.maTinhThuongTru),
                                                   new XElement("maHuyenThuongTru", moi.maHuyenThuongTru),
                                                   new XElement("maXaThuongTru", moi.maXaThuongTru),
                                                   new XElement("maThonXomThuongTru", moi.maThonXomThuongTru),
                                                   new XElement("diaChiChiTietHienTai", moi.diaChiChiTietHienTai),
                                                   new XElement("maTinhHienTai", moi.maTinhHienTai),
                                                   new XElement("maHuyenHienTai", moi.maHuyenHienTai),
                                                   new XElement("maXaHienTai", moi.maXaHienTai),
                                                   new XElement("maThonXomHienTai", moi.maThonXomHienTai),
                                                   new XElement("dienThoaiNR", moi.dienThoaiNR),
                                                   new XElement("dienThoaiDd", moi.dienThoaiDd),
                                                   new XElement("email", moi.email),
                                                   new XElement("hoTenBo", moi.hoTenBo),
                                                   new XElement("hoTenMe", moi.hoTenMe),
                                                   new XElement("maYTeBo", moi.maYTeBo),
                                                   new XElement("maYTeMe", moi.maYTeMe),
                                                   new XElement("nguoiChamSocChinh", moi.nguoiChamSocChinh),
                                                   new XElement("quanHeNguoiChamSoc", moi.quanHeNguoiChamSoc),
                                                   new XElement("dienThoaiCoDinhNCSC", moi.dienThoaiCoDinhNCSC),
                                                    new XElement("dienThoaiDiDDongNCSC", moi.dienThoaiDiDDongNCSC));

                    return xEle1;
                }
                else return null;
            }
            catch (Exception ex)
            {
                return null;
            }
            #endregion
        }

        public XElement GetDichVuCLS(QLBV_Database.QLBVEntities _data, int maBNhan)
        {
            List<DichVuCLS> list = new List<DichVuCLS>();
            list = (from cls in _data.CLS.Where(p => p.MaBNhan == maBNhan)
                    join cd in _data.ChiDinhs.Where(p => p.Status == 1) on cls.IdCLS equals cd.IdCLS
                    join clsct in _data.CLScts on cd.IDCD equals clsct.IDCD
                    join dvct in _data.DichVucts on clsct.MaDVct equals dvct.MaDVct
                    join dv in _data.DichVus on dvct.MaDV equals dv.MaDV
                    select new DichVuCLS
                    {
                        maDichVu = dvct.MaDVct,
                        maNhom = dv.IDNhom,
                        tenDichVu = dvct.TenDVct,
                        ketQua = clsct.KetQua
                    }).ToList();
            #region get XMl
            try
            {
                if (list.Count > 0)
                {
                    var xEle1 = new XElement("CanLamSang",
                                             from item2 in list
                                             select
                                                       new XElement("DichVu",
                                                          new XElement("maDichVu", item2.maDichVu),
                                                          new XElement("maNhom", item2.maNhom),
                                                          new XElement("tenDichVu", item2.tenDichVu),
                                                          new XElement("ketQua", item2.ketQua)
                                                          ));
                    return xEle1;
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                return null;
            }
            #endregion

        }
        public XElement GetThuoc(QLBV_Database.QLBVEntities _data, int maBNhan)
        {
            List<Thuoc> list = new List<Thuoc>();

            list = (from vp in _data.VienPhis.Where(p => p.MaBNhan == maBNhan)
                    join vpct in _data.VienPhicts.Where(p => p.TrongBH != 2) on vp.idVPhi equals vpct.idVPhi
                    join dv in _data.DichVus on vpct.MaDV equals dv.MaDV
                    where (dv.IDNhom == 4 || dv.IDNhom == 5 || dv.IDNhom == 6)
                    select new Thuoc
                    {
                        MaDV = dv.MaDV,
                        tenThuoc = dv.TenDV,
                        duongDung = dv.MaDuongDung,
                        soLuong = vpct.SoLuong,
                        soDangKy = dv.SoDK,
                        thanhTien = Math.Round(vpct.ThanhTien, 4),
                        tienBntt = Math.Round(vpct.TienBN, 4),
                        tienBhtt = Math.Round(vpct.TienBH, 4),
                        tienBncct = Math.Round(vpct.TBNCTT, 4)
                    }
                             ).ToList();
            var qdt = (from dt in _data.DThuocs.Where(p => p.MaBNhan == maBNhan)
                       join dtct in _data.DThuoccts on dt.IDDon equals dtct.IDDon
                       select new { dtct.MaDV, LieuDung = (dtct.SoLan ?? "") + " " + (dtct.MoiLan ?? "") + " " + (dtct.Luong ?? "") + " " + (dtct.DviUong ?? "") }).ToList();
            foreach (Thuoc thuoc in list)
            {
                var lieudung = qdt.Where(p => p.MaDV == thuoc.MaDV).FirstOrDefault();
                if (lieudung != null)
                    thuoc.lieuDung = lieudung.LieuDung;
            }
            string fomat = "{0:0.####}";
            #region get XMl
            try
            {
                if (list.Count > 0)
                {
                    var xEle1 = new XElement("DonThuocHoSoKhamChuaBenh",
                                             from item2 in list
                                             select
                                                       new XElement("Thuoc",
                                                          new XElement("tenThuoc", item2.tenThuoc),
                                                          new XElement("duongDung", item2.duongDung),
                                                          new XElement("soLuong", item2.soLuong),
                                                          new XElement("lieuDung", item2.lieuDung),
                                                           new XElement("soDangKy", item2.soDangKy),
                                                          new XElement("thanhTien", string.Format(fomat, item2.thanhTien)),
                                                          new XElement("tienNguonKhac", string.Format(fomat, item2.tienNguonKhac)),
                                                           new XElement("tienBntt", string.Format(fomat, item2.tienBntt)),
                                                          new XElement("tienBhtt", string.Format(fomat, item2.tienBhtt)),
                                                          new XElement("tienBncct", string.Format(fomat, item2.tienBncct)),
                                                          new XElement("tienNgoaids", string.Format(fomat, item2.tienNgoaids))

                                                          ));
                    return xEle1;
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                return null;
            }
            #endregion
        }

        public XElement GetDvkt(QLBV_Database.QLBVEntities _data, int maBNhan)
        {
            List<Dvkt> list = new List<Dvkt>();

            list = (from vp in _data.VienPhis.Where(p => p.MaBNhan == maBNhan)
                    join vpct in _data.VienPhicts.Where(p => p.TrongBH != 2) on vp.idVPhi equals vpct.idVPhi
                    join dv in _data.DichVus on vpct.MaDV equals dv.MaDV
                    where (dv.PLoai == 2)
                    select new Dvkt
                    {
                        MaDV = dv.MaDV,
                        maDichVu = dv.MaQD,
                        tenDichVu = dv.TenDV,
                        soLuong = vpct.SoLuong,
                        maNhom = dv.IDNhom,
                        donViTinh = vpct.DonVi,
                        thanhTien = Math.Round(vpct.ThanhTien, 4),
                        tienBntt = Math.Round(vpct.TienBN, 4),
                        tienBhtt = Math.Round(vpct.TienBH, 4),
                        tienBncct = Math.Round(vpct.TBNCTT, 4)
                    }).ToList();
            string fomat = "{0:0.####}";
            #region get XMl
            try
            {
                if (list.Count > 0)
                {
                    var xEle1 = new XElement("DvktHoSoKhamChuaBenh",
                                             from item2 in list
                                             select
                                                       new XElement("Dvkt",
                                                          new XElement("maDichVu", item2.maDichVu),
                                                          new XElement("tenDichVu", item2.tenDichVu),
                                                          new XElement("maNhom", item2.maNhom),
                                                          new XElement("donViTinh", item2.donViTinh),
                                                           new XElement("soLuong", item2.soLuong),
                                                          new XElement("thanhTien", string.Format(fomat, item2.thanhTien)),
                                                          new XElement("tienNguonKhac", string.Format(fomat, item2.tienNguonKhac)),
                                                           new XElement("tienBntt", string.Format(fomat, item2.tienBntt)),
                                                          new XElement("tienBhtt", string.Format(fomat, item2.tienBhtt)),
                                                          new XElement("tienBncct", string.Format(fomat, item2.tienBncct)),
                                                          new XElement("tienNgoaids", string.Format(fomat, item2.tienNgoaids))

                                                          ));
                    return xEle1;
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                return null;
            }
            #endregion
        }



        public ThongTinChung GetThongTinchungObj(QLBV_Database.QLBVEntities _data, int maBNhan)
        {
            ThongTinChung moi = new ThongTinChung();
            var qbn = _data.BenhNhans.Where(p => p.MaBNhan == maBNhan).FirstOrDefault();
            var qttbx = _data.TTboXungs.Where(p => p.MaBNhan == maBNhan).FirstOrDefault();
            if (qbn != null)
            {
                moi.maThe = qbn.SThe;
                moi.hoTen = qbn.TenBNhan;
                moi.gioiTinh = qbn.GTinh == 0 ? "2" : (qbn.GTinh == 1 ? "1" : "");
                string ngaysinh = string.IsNullOrEmpty(qbn.NgaySinh) ? "00" : (qbn.NgaySinh.Length == 1 ? ("0" + qbn.NgaySinh.ToString()) : (qbn.NgaySinh.Length == 2 ? qbn.NgaySinh : "00"));
                string thangsinh = string.IsNullOrEmpty(qbn.ThangSinh) ? "00" : (qbn.ThangSinh.Length == 1 ? ("0" + qbn.ThangSinh.ToString()) : (qbn.ThangSinh.Length == 2 ? qbn.ThangSinh : "00"));
                moi.ngaySinh = qbn.NamSinh.ToString() + "-" + thangsinh + "-" + ngaysinh;
                moi.diaChiChiTietHienTai = qbn.DChi;
                if (qttbx != null)
                {
                    moi.maDanToc = qttbx.MaDT;
                    moi.maQuocTich = qttbx.NgoaiKieu;
                    moi.maNgheNghiep = qttbx.MaNN;
                    moi.soCmnd = qttbx.SoKSinh;
                    moi.maTinhHienTai = qttbx.MaTinh;
                    moi.maHuyenHienTai = qttbx.MaHuyen;
                    moi.maXaHienTai = qttbx.MaXa;
                    moi.dienThoaiDd = qttbx.DThoai;
                    moi.nguoiChamSocChinh = qttbx.NThan;
                    moi.dienThoaiDiDDongNCSC = qttbx.DThoaiNT;
                    moi.diaChiChiTietThuongTru = qttbx.HKTT;
                    if (qttbx.MaTinhKhaiSinh != null)
                        moi.maTinhKhaiSinh = qttbx.MaTinhKhaiSinh.Trim();
                }
            }


            return moi;
        }

        public List<DichVuCLS> GetDichVuCLSObj(QLBV_Database.QLBVEntities _data, int maBNhan)
        {
            List<DichVuCLS> list = new List<DichVuCLS>();
            list = (from cls in _data.CLS.Where(p => p.MaBNhan == maBNhan)
                    join cd in _data.ChiDinhs.Where(p => p.Status == 1) on cls.IdCLS equals cd.IdCLS
                    join clsct in _data.CLScts on cd.IDCD equals clsct.IDCD
                    join dvct in _data.DichVucts on clsct.MaDVct equals dvct.MaDVct
                    join dv in _data.DichVus on dvct.MaDV equals dv.MaDV
                    select new DichVuCLS
                    {
                        maDichVu = dvct.MaDVct,
                        maNhom = dv.IDNhom,
                        tenDichVu = dvct.TenDVct,
                        ketQua = clsct.KetQua
                    }).ToList();
            return list;

        }
        public List<Thuoc> GetThuocObj(QLBV_Database.QLBVEntities _data, int maBNhan)
        {
            List<Thuoc> list = new List<Thuoc>();

            list = (from vp in _data.VienPhis.Where(p => p.MaBNhan == maBNhan)
                    join vpct in _data.VienPhicts.Where(p => p.TrongBH != 2) on vp.idVPhi equals vpct.idVPhi
                    join dv in _data.DichVus on vpct.MaDV equals dv.MaDV
                    where (dv.IDNhom == 4 || dv.IDNhom == 5 || dv.IDNhom == 6)
                    select new Thuoc
                    {
                        MaDV = dv.MaDV,
                        tenThuoc = dv.TenDV,
                        duongDung = dv.MaDuongDung,
                        soLuong = vpct.SoLuong,
                        soDangKy = dv.SoDK,
                        thanhTien = Math.Round(vpct.ThanhTien, 4),
                        tienBntt = Math.Round(vpct.TienBN, 4),
                        tienBhtt = Math.Round(vpct.TienBH, 4),
                        tienBncct = Math.Round(vpct.TBNCTT, 4)
                    }
                             ).ToList();
            var qdt = (from dt in _data.DThuocs.Where(p => p.MaBNhan == maBNhan)
                       join dtct in _data.DThuoccts on dt.IDDon equals dtct.IDDon
                       select new { dtct.MaDV, LieuDung = (dtct.SoLan ?? "") + " " + (dtct.MoiLan ?? "") + " " + (dtct.Luong ?? "") + " " + (dtct.DviUong ?? "") }).ToList();
            foreach (Thuoc thuoc in list)
            {
                var lieudung = qdt.Where(p => p.MaDV == thuoc.MaDV).FirstOrDefault();
                if (lieudung != null)
                    thuoc.lieuDung = lieudung.LieuDung;
            }
            return list;
        }

        public List<Dvkt> GetDvktObj(QLBV_Database.QLBVEntities _data, int maBNhan)
        {
            List<Dvkt> list = new List<Dvkt>();

            list = (from vp in _data.VienPhis.Where(p => p.MaBNhan == maBNhan)
                    join vpct in _data.VienPhicts.Where(p => p.TrongBH != 2) on vp.idVPhi equals vpct.idVPhi
                    join dv in _data.DichVus on vpct.MaDV equals dv.MaDV
                    where (dv.PLoai == 2)
                    select new Dvkt
                    {
                        MaDV = dv.MaDV,
                        maDichVu = dv.MaQD,
                        tenDichVu = dv.TenDV,
                        soLuong = vpct.SoLuong,
                        maNhom = dv.IDNhom,
                        donViTinh = vpct.DonVi,
                        thanhTien = Math.Round(vpct.ThanhTien, 4),
                        tienBntt = Math.Round(vpct.TienBN, 4),
                        tienBhtt = Math.Round(vpct.TienBH, 4),
                        tienBncct = Math.Round(vpct.TBNCTT, 4)
                    }).ToList();

            return list;
        }

        public bool GetHoSoKhamChuaBenh(QLBV_Database.QLBVEntities _data, int maBNhan, string path, ref List<MessageHSSK> listMessage)
        {
            bool rt = false;
            bool check = true;
            HoSoKhamChuaBenh moi = new HoSoKhamChuaBenh();
            var qbn = _data.BenhNhans.Where(p => p.MaBNhan == maBNhan).FirstOrDefault();
            var qttbx = _data.TTboXungs.Where(p => p.MaBNhan == maBNhan).FirstOrDefault();
            var qbnkb = _data.BNKBs.Where(p => p.MaBNhan == maBNhan).OrderBy(p => p.IDKB).ToList();
            var qvaovien = _data.VaoViens.Where(p => p.MaBNhan == maBNhan).FirstOrDefault();
            var qravien = _data.RaViens.Where(p => p.MaBNhan == maBNhan).FirstOrDefault();
            #region
            if (qbn != null)
            {
                moi.ngayKham = qbn.NNhap.Value.ToString("yyyy-MM-dd");
                if (qbnkb.Count > 0)
                {
                    moi.ngayBatDau = qbnkb.First().NgayKham.Value.ToString("yyyy-MM-dd");
                    moi.ngayKetThuc = qbnkb.Last().NgayKham.Value.ToString("yyyy-MM-dd");
                    moi.bacSiKham = DungChung.Ham._getTenCB(_data, qbnkb.First().MaCB);
                    moi.lyDoKham = qbnkb.First().ChanDoan;
                    if (qbnkb.Last().NgayHen != null)
                        moi.henLichKham = qbnkb.Last().NgayHen.Value.ToString("yyyy-MM-dd");

                    #region kiểm tra chiều cao, cân nặng, nhịp thở
                    string[] machNDHA = new string[] { };
                    string[] arrcannang = new string[] { };

                    if (qttbx != null)
                    {
                        if (!string.IsNullOrEmpty(qttbx.Mach_NDo_HAp) && qttbx.Mach_NDo_HAp.Contains(';'))
                        {
                            machNDHA = qttbx.Mach_NDo_HAp.Split(';');
                        }
                        if (!string.IsNullOrEmpty(qttbx.CanNang_ChieuCao) && qttbx.CanNang_ChieuCao.Contains(';'))
                        {
                            arrcannang = qttbx.CanNang_ChieuCao.Split(';');
                        }
                    }
                    if (qvaovien != null && !string.IsNullOrEmpty(qvaovien.Mach))
                    {
                        moi.mach = qvaovien.Mach;
                    }
                    else if (qttbx != null)
                    {
                        if (machNDHA != null && machNDHA.Length > 0)
                            moi.mach = machNDHA[0];
                    }

                    if (qvaovien != null && !string.IsNullOrEmpty(qvaovien.NhietDo))
                    {
                        moi.nhietDo = qvaovien.NhietDo;
                    }
                    else if (qttbx != null)
                    {
                        if (machNDHA != null && machNDHA.Length > 1)
                            moi.nhietDo = machNDHA[1];
                    }

                    if (qvaovien != null && !string.IsNullOrEmpty(qvaovien.HuyetAp))
                    {
                        int slashIndex = qvaovien.HuyetAp.IndexOf('/');
                        if (slashIndex >= 0)
                        {
                            moi.huyetApTD = qvaovien.HuyetAp.Substring(0, slashIndex);
                            moi.huyetApTT = qvaovien.HuyetAp.Substring(slashIndex + 1, qvaovien.HuyetAp.Length - slashIndex - 1);
                        }
                        else
                            moi.huyetApTT = qvaovien.HuyetAp;
                    }
                    else if (qttbx != null)
                    {
                        if (machNDHA != null && machNDHA.Length > 2)
                        {
                            int slashIndex = machNDHA[2].IndexOf('/');
                            if (slashIndex >= 0)
                            {
                                moi.huyetApTD = machNDHA[2].Substring(0, slashIndex);
                                moi.huyetApTT = machNDHA[2].Substring(slashIndex + 1, machNDHA[2].Length - slashIndex - 1);
                            }
                            else
                                moi.huyetApTT = machNDHA[2];
                        }

                    }


                    if (qvaovien != null && !string.IsNullOrEmpty(qvaovien.NhipTho))
                    {
                        moi.nhipTho = qvaovien.NhipTho;
                    }
                    else if (qttbx != null)
                    {
                        if (machNDHA != null && machNDHA.Length > 3)
                            moi.nhipTho = machNDHA[3];
                    }

                    if (qvaovien != null && !string.IsNullOrEmpty(qvaovien.CanNang))
                    {
                        moi.canNang = qvaovien.CanNang;
                    }
                    else if (qttbx != null)
                    {
                        if (arrcannang != null && arrcannang.Length > 0)
                            moi.canNang = arrcannang[0];
                    }

                    if (qvaovien != null && !string.IsNullOrEmpty(qvaovien.ChieuCao))
                    {
                        moi.chieuCao = qvaovien.ChieuCao;
                    }
                    else if (qttbx != null)
                    {
                        if (arrcannang != null && arrcannang.Length > 1)
                            moi.chieuCao = arrcannang[1];
                    }
                    if (qttbx == null)
                    {
                        listMessage.Add(new MessageHSSK(maBNhan, "Bệnh nhân chưa có thông tin bổ sung"));
                        check = false;
                    }
                    if (string.IsNullOrWhiteSpace(qttbx.MaTinhKhaiSinh))
                    {
                        listMessage.Add(new MessageHSSK(maBNhan, "Chưa có thông tin tỉnh khai sinh"));
                        check = false;
                    }
                    if (string.IsNullOrWhiteSpace(qttbx.MaTinh))
                    {
                        listMessage.Add(new MessageHSSK(maBNhan, "Chưa có thông tin tỉnh hiện tại"));
                        check = false;
                    }
                    if (string.IsNullOrWhiteSpace(qttbx.MaHuyen))
                    {
                        listMessage.Add(new MessageHSSK(maBNhan, "Chưa có thông tin huyện hiện tại"));
                        check = false;
                    }
                    if (string.IsNullOrWhiteSpace(qttbx.MaXa))
                    {
                        listMessage.Add(new MessageHSSK(maBNhan, "Chưa có thông tin xã hiện tại"));
                        check = false;
                    }
                    if ((string.IsNullOrWhiteSpace(qttbx.NThan)) && DungChung.Ham.CalculateAgeByDate(qbn.NgaySinh, qbn.ThangSinh, qbn.NamSinh) < 1)
                    {
                        listMessage.Add(new MessageHSSK(maBNhan, "Bệnh nhân dưới 1 tuổi chưa nhập thông tin người thân"));
                        check = false;
                    }
                    if ((string.IsNullOrWhiteSpace(qttbx.QuanHeNhanThan) || (qttbx.QuanHeNhanThan != null && qttbx.QuanHeNhanThan.Trim() == "0")) && DungChung.Ham.CalculateAgeByDate(qbn.NgaySinh, qbn.ThangSinh, qbn.NamSinh) < 1)
                    {
                        listMessage.Add(new MessageHSSK(maBNhan, "Bệnh nhân dưới 1 tuổi chưa nhập thông tin quan hệ nhân thân"));
                        check = false;
                    }
                    if (qttbx.NgayCapCMT != null && qttbx.NgayCapCMT != DateTime.MinValue && qttbx.NgayCapCMT.Value.Year <= 1800)
                    {
                        listMessage.Add(new MessageHSSK(maBNhan, "Năm cấp CMT không được nhỏ hơn 1800"));
                        check = false;
                    }
                    #endregion
                    #region khám
                    int makp = qbnkb.First().MaKP.Value;
                    var qkp = _data.KPhongs.Where(p => p.MaKP == makp).FirstOrDefault();
                    if (qkp != null)
                    {
                        if (qkp.MaQD == "K13")
                        {
                            moi.khamDa = "Khám da liễu";
                        }
                        else if (qkp.MaQD == "K01" || qkp.MaQD == "K03" || qkp.MaQD == "K19")
                        {
                            moi.khamToanThanKhac = "Khám toàn thân khác";
                        }
                        else if (qkp.MaQD == "K04")
                        {
                            moi.khamTimMach = "Khám tim mạch";
                        }
                        else if (qkp.MaQD == "K50")
                        {
                            moi.khamHoHap = "Khám hô hấp";
                        }
                        else if (qkp.MaQD == "K05" || qkp.MaQD == "K22")
                        {
                            moi.khamTieuHoa = "Khám tiêu hóa";
                        }
                        else if (qkp.MaQD == "K07" || qkp.MaQD == "K23")
                        {
                            moi.khamTietNieu = "Khám tiết niệu";
                        }
                        else if (qkp.MaQD == "K06")
                        {
                            moi.khamCoXuongKhop = "Khám cơ xương khớp";
                        }
                        else if (qkp.MaQD == "K08")
                        {
                            moi.khamNoiTiet = "Khám nội tiết";
                        }
                        else if (qkp.MaQD == "K14" || qkp.MaQD == "K20")
                        {
                            moi.khamThanKinh = "Khám thần kinh";
                        }
                        else if (qkp.MaQD == "K15")
                        {
                            moi.khamTamThan = "Khám tâm thần";
                        }
                        else if (qkp.MaQD == "K29")
                        {
                            moi.khamRHM = "Khám răng hàm mặt";
                        }
                        else if (qkp.MaQD == "K28")
                        {
                            moi.khamTMH = "Khám tai mũi họng";
                        }
                        else if (qkp.MaQD == "K31")
                        {
                            moi.khamVanDong = "Khám vận động";
                        }
                        else if (qkp.MaQD == "K45")
                        {
                            moi.khamDinhDuong = "Khám dinh dưỡng";
                        }
                        else if (qkp.MaQD == "K30")
                        {
                            moi.khamMat = "Khám mắt";
                        }
                        else
                        {
                            moi.khamKhac = "Khám khác";
                        }

                    }
                    #endregion
                    #region CLS
                    if (qravien != null && !string.IsNullOrEmpty(qravien.MaICD))
                    {

                        List<string> maICD = qravien.MaICD.Split(';').ToList();
                        if (maICD.Count > 0)
                        {
                            var icd = maICD.First();
                            var icd10 = _data.ICD10.FirstOrDefault(o => o.MaICD == icd);
                            if (icd != null)
                                moi.chanDoanKetLuan = maICD.First();//qravien.MaICD;
                            else
                            {
                                listMessage.Add(new MessageHSSK(maBNhan, "Bệnh nhân có mã ICD ra viện không nằm trong danh mục"));
                                check = false;
                            }
                        }
                        moi.benhTheoDoi = DungChung.Ham.FreshString(qravien.ChanDoan);
                    }

                    var qvp = (from vp in _data.VienPhis.Where(p => p.MaBNhan == maBNhan)
                               join vpct in _data.VienPhicts.Where(p => p.TrongBH != 2) on vp.idVPhi equals vpct.idVPhi
                               join dv in _data.DichVus on vpct.MaDV equals dv.MaDV
                               join tn in _data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                               join n in _data.NhomDVs on tn.IDNhom equals n.IDNhom
                               select new { vpct.ThanhTien, vpct.TienBH, vpct.TienBN, vpct.TBNCTT, vpct.TBNTT, dv.TenDV, tn.TenRG, n.TenNhomCT, n.IDNhom }).ToList();
                    var qxnhuyethoc = qvp.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).ToList();
                    if (qxnhuyethoc.Count > 0)
                        moi.xnHuyetHoc = DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc;

                    var qxnSinhhoamau = qvp.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau).ToList();
                    if (qxnSinhhoamau.Count > 0)
                        moi.xnSinhHoaMau = DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau;

                    var qXQuang = qvp.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang).ToList();
                    if (qXQuang.Count > 0)
                        moi.xQuang = DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang;

                    var qSieuAm = qvp.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm).ToList();
                    if (qSieuAm.Count > 0)
                        moi.sieuAm = DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm;

                    var qXNKhac = qvp.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNKhac).ToList();
                    if (qXNKhac.Count > 0)
                        moi.xnKhac = DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNKhac;
                    #endregion
                    #region chi phí
                    var qThuoc = qvp.Where(p => p.IDNhom == 4 || p.IDNhom == 5 || p.IDNhom == 6).ToList();
                    if (qThuoc.Count > 0)
                        moi.tienThuoc = Math.Round(qThuoc.Sum(p => p.ThanhTien), 4);

                    var qVTYT = qvp.Where(p => p.IDNhom == 10 || p.IDNhom == 11).ToList();
                    if (qVTYT.Count > 0)
                        moi.tienVtyt = Math.Round(qVTYT.Sum(p => p.ThanhTien), 4);
                    moi.tienTong = Math.Round(qvp.Sum(p => p.ThanhTien), 4);
                    moi.tienBntt = Math.Round(qvp.Sum(p => p.TienBN), 4);
                    moi.tienBhtt = Math.Round(qvp.Sum(p => p.TienBH), 4);
                    moi.tienXn = Math.Round(qvp.Where(p => p.IDNhom == 1).Sum(p => p.ThanhTien), 4);
                    moi.tienCdha = Math.Round(qvp.Where(p => p.IDNhom == 2).Sum(p => p.ThanhTien), 4);
                    moi.tienTdcn = Math.Round(qvp.Where(p => p.IDNhom == 3).Sum(p => p.ThanhTien), 4);
                    moi.tienThuocBhyt = Math.Round(qvp.Where(p => p.IDNhom == 4).Sum(p => p.ThanhTien), 4);
                    moi.tienThuocUngthu = Math.Round(qvp.Where(p => p.IDNhom == 5).Sum(p => p.ThanhTien), 4);
                    moi.tienThuocTttl = Math.Round(qvp.Where(p => p.IDNhom == 6).Sum(p => p.ThanhTien), 4);
                    moi.tienMau = Math.Round(qvp.Where(p => p.IDNhom == 7).Sum(p => p.ThanhTien), 4);
                    moi.tienPttt = Math.Round(qvp.Where(p => p.IDNhom == 8).Sum(p => p.ThanhTien), 4);
                    moi.tienDvkt = Math.Round(qvp.Where(p => p.IDNhom == 9).Sum(p => p.ThanhTien), 4);
                    moi.tienVanChuyen = Math.Round(qvp.Where(p => p.IDNhom == 12).Sum(p => p.ThanhTien), 4);
                    moi.tienKham = Math.Round(qvp.Where(p => p.IDNhom == 13).Sum(p => p.ThanhTien), 4);
                    moi.tienGiuongNgoai = Math.Round(qvp.Where(p => p.IDNhom == 14).Sum(p => p.ThanhTien), 4);
                    moi.tienGiuongNoi = Math.Round(qvp.Where(p => p.IDNhom == 15).Sum(p => p.ThanhTien), 4);
                    moi.tienVtytTyle = Math.Round(qvp.Where(p => p.IDNhom == 11).Sum(p => p.ThanhTien), 4);
                    moi.tienVtytBhyt = Math.Round(qvp.Where(p => p.IDNhom == 10).Sum(p => p.ThanhTien), 4);
                    #endregion
                    #region kiểm tra bác sỹ khám, lý do khám
                    if (moi.bacSiKham == null || moi.bacSiKham == "")
                    {
                        listMessage.Add(new MessageHSSK(maBNhan, "Chưa có thông tin bác sỹ khám"));
                        check = false;
                    }
                    else if (moi.lyDoKham == null || moi.lyDoKham == "")
                    {
                        listMessage.Add(new MessageHSSK(maBNhan, "Chưa có lý do khám"));
                        check = false;
                    }
                    #endregion
                }
                else
                {
                    listMessage.Add(new MessageHSSK(maBNhan, "Bệnh nhân chưa có thông tin khám bệnh"));
                    check = false;
                }
            }
            else
            {
                listMessage.Add(new MessageHSSK(maBNhan, "Không tìm thấy bệnh nhân"));
                check = false;
            }

            if (!check)
                return rt;

            string strpid = "";
            if (GetPID(_data, maBNhan, ref strpid, ref listMessage) && strpid.Length == 13)
            {
                #region xuất xml HSSK
                try
                {
                    var xEleCLS = GetDichVuCLS(_data, maBNhan);
                    var xEleDichvu = GetDvkt(_data, maBNhan);
                    var xEleThuoc = GetThuoc(_data, maBNhan);
                    XElement xEleThongTinChung = GetThongTinchung(_data, maBNhan);
                    string fomat = "{0:0.####}";
                    if (moi != null)
                    {
                        var xEle1 = new XElement("DuLieuLienThongYTeCoSo",
                                        new XElement("MaCSKCB", DungChung.Bien.MaBV),
                                        new XElement("danhSachHoSoYTe",
                                            new XElement("HoSoYTe",
                                                new XElement("maYTeCaNhan", strpid), //qbn.PID),
                                                xEleThongTinChung,
                                                new XElement("DanhSachHoSoKhamChuaBenh",
                                                    new XElement("HoSoKhamChuaBenh",
                                                       new XElement("ngayKham", moi.ngayKham),
                                                       new XElement("ngayBatDau", moi.ngayBatDau),
                                                       new XElement("ngayKetThuc", moi.ngayKetThuc),
                                                       new XElement("bacSiKham", moi.bacSiKham),
                                                       new XElement("lyDoKham", moi.lyDoKham),
                                                       new XElement("benhSu", moi.benhSu),
                                                       new XElement("mach", moi.mach),
                                                       new XElement("nhietDo", moi.nhietDo),
                                                       new XElement("huyetApTT", moi.huyetApTT),
                                                       new XElement("huyetApTD", moi.huyetApTD),
                                                       new XElement("nhipTho", moi.nhipTho),
                                                       new XElement("chieuCao", moi.chieuCao),
                                                       new XElement("canNang", moi.canNang),
                                                       new XElement("chiSoBmi", moi.chiSoBmi),
                                                       new XElement("vongBung", moi.vongBung),
                                                       new XElement("khamDa", moi.khamDa),
                                                       new XElement("khamNiemMac", moi.khamNiemMac),
                                                       new XElement("khamToanThanKhac", moi.khamToanThanKhac),
                                                       new XElement("khamTimMach", moi.khamTimMach),
                                                       new XElement("khamHoHap", moi.khamHoHap),
                                                       new XElement("khamTieuHoa", moi.khamTieuHoa),
                                                       new XElement("khamTietNieu", moi.khamTietNieu),
                                                       new XElement("khamCoXuongKhop", moi.khamCoXuongKhop),
                                                       new XElement("khamNoiTiet", moi.khamNoiTiet),
                                                       new XElement("khamThanKinh", moi.khamThanKinh),
                                                       new XElement("tuVan", moi.tuVan),
                                                       new XElement("chanDoanKetLuan", moi.chanDoanKetLuan),
                                                       new XElement("henLichKham", moi.henLichKham),
                                                       new XElement("benhTheoDoi", moi.benhTheoDoi),
                                                       new XElement("khamTamThan", moi.khamTamThan),
                                                       new XElement("khamTMH", moi.khamTMH),
                                                       new XElement("khamRHM", moi.khamRHM),
                                                       new XElement("khamVanDong", moi.khamVanDong),
                                                       new XElement("khamDinhDuong", moi.khamDinhDuong),
                                                       new XElement("khamKhac", moi.khamKhac),
                                                       new XElement("khamDanhGia", moi.khamDanhGia),
                                                       new XElement("khamMat", moi.khamMat),
                                                       new XElement("xnHuyetHoc", moi.xnHuyetHoc),
                                                       new XElement("xnSinhHoaMau", moi.xnSinhHoaMau),
                                                       new XElement("xQuang", moi.xQuang),
                                                       new XElement("sieuAm", moi.sieuAm),
                                                       new XElement("xnKhac", moi.xnKhac),
                                                       new XElement("tienThuoc", string.Format(fomat, moi.tienThuoc)),
                                                       new XElement("tienVtyt", string.Format(fomat, moi.tienVtyt)),
                                                       new XElement("tienTong", string.Format(fomat, moi.tienTong)),
                                                       new XElement("tienBntt", string.Format(fomat, moi.tienBntt)),
                                                       new XElement("tienBhtt", string.Format(fomat, moi.tienBhtt)),
                                                       new XElement("tienNguonkhac", string.Format(fomat, moi.tienNguonkhac)),
                                                       new XElement("tienNgoaids", string.Format(fomat, moi.tienNgoaids)),
                                                       new XElement("tienXn", string.Format(fomat, moi.tienXn)),
                                                       new XElement("tienCdha", string.Format(fomat, moi.tienCdha)),
                                                       new XElement("tienTdcn", string.Format(fomat, moi.tienTdcn)),
                                                       new XElement("tienThuocBhyt", string.Format(fomat, moi.tienThuocBhyt)),
                                                       new XElement("tienThuocUngthu", string.Format(fomat, moi.tienThuocUngthu)),
                                                       new XElement("tienThuocTttl", string.Format(fomat, moi.tienThuocTttl)),
                                                       new XElement("tienMau", string.Format(fomat, moi.tienMau)),
                                                       new XElement("tienVanChuyen", string.Format(fomat, moi.tienVanChuyen)),
                                                       new XElement("tienKham", string.Format(fomat, moi.tienKham)),
                                                       new XElement("tienGiuongNgoai", string.Format(fomat, moi.tienGiuongNgoai)),
                                                       new XElement("tienGiuongNoi", string.Format(fomat, moi.tienGiuongNoi)),
                                                       new XElement("tienVtytTyle", string.Format(fomat, moi.tienVtytTyle)),
                                                       new XElement("tienVtytBhyt", string.Format(fomat, moi.tienVtytBhyt)),
                                                       xEleCLS,
                                                       xEleThuoc,
                                                       xEleDichvu
                                                        )))));


                        try
                        {
                            xEle1.Save(path + "\\hssk_" + maBNhan.ToString() + ".xml");
                            string token = "";
                            token = LienThongHSSK.GetTokenUpload(maBNhan, ref listMessage);
                            if (!String.IsNullOrEmpty(token))
                            {
                                KQUploadHS kq = LienThongHSSK.UpLoadHSSK(path + "\\hssk_" + maBNhan.ToString() + ".xml", token, maBNhan, ref listMessage);
                                if (kq != null && kq.status == "200")
                                {
                                    BenhNhan bn = _data.BenhNhans.FirstOrDefault(o => o.MaBNhan == maBNhan);
                                    bn.PID = strpid;
                                    RaVien qrv = _data.RaViens.FirstOrDefault(p => p.MaBNhan == maBNhan);
                                    if (!string.IsNullOrEmpty(kq.maGiaoDich))
                                    {
                                        qrv.maGiaoDichHSSK = kq.maGiaoDich;
                                        _data.SaveChanges();
                                        rt = true;
                                    }
                                    else
                                    {
                                        listMessage.Add(new MessageHSSK(maBNhan, "Không trả về mã giao dịch"));
                                        rt = false;
                                    }
                                }
                                else
                                {
                                    listMessage.Add(new MessageHSSK(maBNhan, "Gửi file bị lỗi" + kq != null ? (": " + kq.message) : ""));
                                    rt = false;
                                }
                            }
                            else
                            {
                                listMessage.Add(new MessageHSSK(maBNhan, "Không lấy được token upload hồ sơ"));
                                rt = false;
                            }

                        }
                        catch (Exception ex)
                        {
                            listMessage.Add(new MessageHSSK(maBNhan, "Lỗi lưu file XML kiểm tra lại đường dẫn lưu file: " + path));
                            rt = false;
                        }
                    }
                    else
                        rt = false;
                }
                catch (Exception ex)
                {
                    rt = false;
                }
                //return rt;
                #endregion
            }
            return rt;
            #endregion

        }
        public class MessageHSSK
        {
            public int MaBNhan { get; set; }
            public string Message { get; set; }
            public MessageHSSK(int _MaBNhan, string _Message)
            {
                this.MaBNhan = _MaBNhan;
                this.Message = _Message;
            }
        }
        public bool GetPID(QLBV_Database.QLBVEntities DataContext, int _mabn, ref string strpid, ref List<MessageHSSK> listMessage)
        {
            bool rs = false;
            LayDinhDanh pid = new LayDinhDanh();
            if (_mabn > 0)
                pid.MaBNhan = _mabn;
            //else
            //    pid.MaBNhan = _mabnhan;

            var qbn = DataContext.BenhNhans.Where(p => p.MaBNhan == pid.MaBNhan).FirstOrDefault();
            if (qbn == null)
            {
                listMessage.Add(new MessageHSSK(_mabn, "Bạn chưa lưu bệnh nhân"));
            }
            else
            {
                if (!string.IsNullOrEmpty(qbn.PID))
                {
                    listMessage.Add(new MessageHSSK(_mabn, "Bệnh nhân đã có mã định danh cá nhân"));
                    strpid = qbn.PID;
                    rs = true;
                }
                else
                {
                    var qttbx = DataContext.TTboXungs.Where(p => p.MaBNhan == pid.MaBNhan).FirstOrDefault();
                    bool kt = true;
                    string mss = "Mã bệnh nhân: " + _mabn + "\n";
                    //else if (qttbx.NoiCapCMT == null || qttbx.NoiCapCMT == "")
                    //{
                    //    kt = false;
                    //    MessageBox.Show("Bệnh nhân chưa có nơi cấp CMT/ thẻ căn cước");
                    //}
                    if (kt)
                    {
                        pid.fULLNAME = qbn.TenBNhan;
                        pid.sEX = qbn.GTinh == 1 ? "1" : "2";
                        pid.dATEOFBIRTH = qbn.NamSinh + (qbn.ThangSinh.Length == 1 ? ("0" + qbn.ThangSinh.Trim()) : qbn.ThangSinh.Trim()) + (qbn.NgaySinh.Trim().Length == 1 ? ("0" + qbn.NgaySinh.Trim()) : qbn.NgaySinh.Trim());
                        pid.hOMETOWN = qbn.DChi;
                        pid.hEALTHINSURANCEIDCARD = qbn.SThe;
                        pid.pLACEOFBIRTH = string.IsNullOrEmpty(qttbx.DchiKhaiSinh) ? qbn.DChi : qttbx.DchiKhaiSinh;
                        pid.iDENTIICATIONIDCARD = qttbx.SoKSinh;// số cmnd
                        pid.pHONE = qttbx.DThoai;
                        pid.cURENTPROVICECODE = qttbx.MaTinh == null ? "" : qttbx.MaTinh;
                        pid.cURENTDISTRICTCODE = qttbx.MaHuyen == null ? "" : qttbx.MaHuyen;
                        pid.cURENTCOMMUNECODE = qttbx.MaXa;

                        List<string> lQHThannhan = new List<string>();
                        for (int i = 1; i < 18; i++)
                        {
                            if (i != 14 && i != 15)
                            {
                                lQHThannhan.Add(i.ToString());
                            }
                        }

                        if (qttbx.QuanHeNhanThan != null && lQHThannhan.Contains(qttbx.QuanHeNhanThan.Trim()) && qttbx.QuanHeNhanThan.Trim() != "0" && !string.IsNullOrEmpty(qttbx.NThan))
                        {
                            pid.rELATIONWITHCONSERVATOR = qttbx.QuanHeNhanThan.Trim();
                            pid.nAMEOFCONSERVATOR = qttbx.NThan;
                            pid.pHONEOFCONSERVATOR = qttbx.DThoaiNT;
                        }

                        pid.iDOFCONSERVATOR = "";

                        pid.pROVINCIALBIRTHREGISTRATIONCODE = qttbx.MaTinhKhaiSinh == null ? "" : ("0" + qttbx.MaTinhKhaiSinh.Trim());


                        if (kt)
                        {
                            #region create file xml yc pid - không dùng đến
                            StringBuilder sb = new StringBuilder();
                            sb.Append("{" + Environment.NewLine);
                            string text = "\"fULLNAME\":\"";
                            sb.Append(text);
                            text = pid.fULLNAME + "\"" + Environment.NewLine;
                            sb.Append(text);

                            text = "\"sEX\":\"";
                            sb.Append(text);
                            text = pid.sEX + "\"" + Environment.NewLine;
                            sb.Append(text);

                            text = "\"dATEOFBIRTH\":\"";
                            sb.Append(text);
                            text = pid.dATEOFBIRTH + "\"" + Environment.NewLine;
                            sb.Append(text);

                            text = "\"pLACEOFBIRTH\":\"";
                            sb.Append(text);
                            text = pid.pLACEOFBIRTH + "\"" + Environment.NewLine;
                            sb.Append(text);

                            text = "\"hOMETOWN\":\"";
                            sb.Append(text);
                            text = pid.hOMETOWN + "\"" + Environment.NewLine;
                            sb.Append(text);

                            text = "\"hEALTHINSURANCEIDCARD\":\"";
                            sb.Append(text);
                            text = pid.hEALTHINSURANCEIDCARD + "\"" + Environment.NewLine;
                            sb.Append(text);

                            text = "\"iDENTIICATIONIDCARD\":\"";
                            sb.Append(text);
                            text = pid.iDENTIICATIONIDCARD + "\"" + Environment.NewLine;
                            sb.Append(text);

                            text = "\"pHONE\":\"";
                            sb.Append(text);
                            text = pid.pHONE + "\"" + Environment.NewLine;
                            sb.Append(text);

                            text = "\"cURENTPROVICECODE\":\"";
                            sb.Append(text);
                            text = pid.cURENTPROVICECODE + "\"" + Environment.NewLine;
                            sb.Append(text);

                            text = "\"cURENTDISTRICTCODE\":\"";
                            sb.Append(text);
                            text = pid.cURENTDISTRICTCODE + "\"" + Environment.NewLine;
                            sb.Append(text);

                            text = "\"cURENTCOMMUNECODE\":\"";
                            sb.Append(text);
                            text = pid.cURENTCOMMUNECODE + "\"" + Environment.NewLine;
                            sb.Append(text);

                            text = "\"nAMEOFCONSERVATOR\":\"";
                            sb.Append(text);
                            text = pid.nAMEOFCONSERVATOR + "\"" + Environment.NewLine;
                            sb.Append(text);

                            text = "\"rELATIONWITHCONSERVATOR\":\"";
                            sb.Append(text);
                            text = pid.rELATIONWITHCONSERVATOR + "\"" + Environment.NewLine;
                            sb.Append(text);

                            text = "\"iDOFCONSERVATOR\":\"";
                            sb.Append(text);
                            text = pid.iDOFCONSERVATOR + "\"" + Environment.NewLine;
                            sb.Append(text);

                            text = "\"pHONEOFCONSERVATOR\":\"";
                            sb.Append(text);
                            text = pid.pHONEOFCONSERVATOR + "\"" + Environment.NewLine;
                            sb.Append(text);

                            text = "\"pROVINCIALBIRTHREGISTRATIONCODE\":\"";
                            sb.Append(text);
                            text = pid.pROVINCIALBIRTHREGISTRATIONCODE + "\"" + Environment.NewLine;
                            sb.Append(text);

                            sb.Append("}");

                            string filepath = DungChung.Bien.xmlFilePath_LIS[34] + "\\" + pid.MaBNhan.ToString() + ".xml";
                            try
                            {
                                File.WriteAllText(filepath, sb.ToString());
                                // MessageBox.Show("xuất file thành công");
                            }
                            catch (Exception ex)
                            {
                                listMessage.Add(new MessageHSSK(_mabn, "Lỗi xuất file xml PID. Kiểm tra đường dẫn : " + filepath));
                            }

                            #endregion


                            #region ok
                            //string urlGetPID = @"http://203.190.173.122:8090/";
                            // string ms = "";

                            if (!string.IsNullOrEmpty(DungChung.Bien.xmlFilePath_LIS[36]) && !string.IsNullOrEmpty(DungChung.Bien.xmlFilePath_LIS[37]))
                            {
                                LienThongHSSK hssk = new LienThongHSSK();
                                KQPhienLamViecPID kq = hssk.CallApi_TokenPID(_mabn, ref listMessage);
                                if (kq.StatusCode == "200")
                                {
                                    // ms = "ok";
                                    #region get pid
                                    try
                                    {
                                        KQPID kqpid = hssk.GetPID(kq.Token, pid, _mabn, ref listMessage);

                                        if (kqpid.PID != null && kqpid.PID.Length == 13)
                                        {
                                            pid.PID = kqpid.PID;
                                            rs = true;
                                            strpid = kqpid.PID;
                                            //   MessageBox.Show("Lấy thành công mã PID cho bệnh nhân " + bn.TenBNhan + " : " + pid.PID);

                                        }
                                        else if (kqpid != null)
                                        {
                                            listMessage.Add(new MessageHSSK(_mabn, "Lỗi trả về trong quá trình lấy PID: " + kqpid.StatusCode + " - " + kqpid.Comment));
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        listMessage.Add(new MessageHSSK(_mabn, ex.Message));
                                    }
                                    #endregion

                                }
                                else
                                {
                                    listMessage.Add(new MessageHSSK(_mabn, "Lỗi trả về trong quá trình lấy PID: " + kq.StatusCode + " - " + kq.Comment));
                                }
                            }
                            else
                            {
                                listMessage.Add(new MessageHSSK(_mabn, "Bạn chưa thiết lập tên đăng nhập, mật khẩu"));
                            }
                            #endregion ok

                        }
                    }
                    else
                        listMessage.Add(new MessageHSSK(_mabn, mss));
                }
            }
            return rs;
        }

    }
    public class KQPhienLamViecPID
    {
        public string StatusCode { set; get; }
        public string Token { set; get; }
        public string Comment { set; get; }
    }
    public class ApiToken
    {
        public string u { set; get; }
        public string p { set; get; }
    }
    public class KQPID
    {
        public string StatusCode { set; get; }
        public string PID { set; get; }
        public string Comment { set; get; }

    }

    public class KQUploadHS
    {
        public string maGiaoDich { set; get; }
        public string message { set; get; }
        public string status { set; get; }
    }
    public class ThongTinChung
    {
        public string maThe { set; get; }
        public string hoTen { set; get; }
        public string gioiTinh { set; get; }
        public string nhomMauHeAbo { set; get; }
        public string nhomMauHeRh { set; get; }
        public string ngaySinh { set; get; }
        public string maTinhKhaiSinh { set; get; }
        public string maDanToc { set; get; }
        public string maQuocTich { set; get; }
        public string maTonGiao { set; get; }
        public string maNgheNghiep { set; get; }
        public string soCmnd { set; get; }
        public string ngayCap { set; get; }
        public string noiCap { set; get; }
        public string diaChiChiTietThuongTru { set; get; }
        public string maTinhThuongTru { set; get; }
        public string maHuyenThuongTru { set; get; }
        public string maXaThuongTru { set; get; }
        public string maThonXomThuongTru { set; get; }
        public string diaChiChiTietHienTai { set; get; }
        public string maTinhHienTai { set; get; }
        public string maHuyenHienTai { set; get; }
        public string maXaHienTai { set; get; }
        public string maThonXomHienTai { set; get; }
        public string dienThoaiNR { set; get; }
        public string dienThoaiDd { set; get; }
        public string email { set; get; }
        public string hoTenBo { set; get; }
        public string hoTenMe { set; get; }
        public string maYTeBo { set; get; }
        public string maYTeMe { set; get; }
        public string nguoiChamSocChinh { set; get; }
        public string quanHeNguoiChamSoc { set; get; }
        public string dienThoaiCoDinhNCSC { set; get; }
        public string dienThoaiDiDDongNCSC { set; get; }

    }

    public class HoSoKhamChuaBenh
    {
        public string ngayKham { set; get; }
        public string ngayBatDau { set; get; }
        public string ngayKetThuc { set; get; }
        public string bacSiKham { set; get; }
        public string lyDoKham { set; get; }
        public string benhSu { set; get; }
        public string mach { set; get; }
        public string nhietDo { set; get; }
        public string huyetApTT { set; get; }
        public string huyetApTD { set; get; }
        public string nhipTho { set; get; }
        public string chieuCao { set; get; }
        public string canNang { set; get; }
        public string chiSoBmi { set; get; }
        public string vongBung { set; get; }
        public string khamDa { set; get; }
        public string khamNiemMac { set; get; }
        public string khamToanThanKhac { set; get; }
        public string khamTimMach { set; get; }
        public string khamHoHap { set; get; }
        public string khamTieuHoa { set; get; }
        public string khamTietNieu { set; get; }
        public string khamCoXuongKhop { set; get; }
        public string khamNoiTiet { set; get; }
        public string khamThanKinh { set; get; }
        public string tuVan { set; get; }
        public string chanDoanKetLuan { set; get; }
        public string henLichKham { set; get; }
        public string benhTheoDoi { set; get; }
        public string khamTamThan { set; get; }
        public string khamTMH { set; get; }
        public string khamRHM { set; get; }
        public string khamVanDong { set; get; }
        public string khamDinhDuong { set; get; }
        public string khamKhac { set; get; }
        public string khamDanhGia { set; get; }
        public string khamMat { set; get; }
        public string xnHuyetHoc { set; get; }
        public string xnSinhHoaMau { set; get; }
        public string xQuang { set; get; }
        public string sieuAm { set; get; }
        public string xnKhac { set; get; }
        public double? tienThuoc { set; get; }
        public double? tienVtyt { set; get; }
        public double? tienTong { set; get; }
        public double? tienBntt { set; get; }
        public double? tienBhtt { set; get; }
        public double? tienNguonkhac { set; get; }
        public double? tienNgoaids { set; get; }
        public double? tienXn { set; get; }
        public double? tienCdha { set; get; }
        public double? tienTdcn { set; get; }
        public double? tienThuocBhyt { set; get; }
        public double? tienThuocUngthu { set; get; }
        public double? tienThuocTttl { set; get; }
        public double? tienMau { set; get; }
        public double? tienPttt { set; get; }
        public double? tienDvkt { set; get; }
        public double? tienVanChuyen { set; get; }
        public double? tienKham { set; get; }
        public double? tienGiuongNgoai { set; get; }
        public double? tienGiuongNoi { set; get; }
        public double? tienVtytTyle { set; get; }
        public double? tienVtytBhyt { set; get; }
        public CanLamSang CanLamSang { set; get; }
        public DonThuocHoSoKhamChuaBenh DonThuocHoSoKhamChuaBenh { set; get; }
        public DvktHoSoKhamChuaBenh DvktHoSoKhamChuaBenh { set; get; }

    }
    public class CanLamSang
    {
        public List<DichVuCLS> DichVu { set; get; }
    }
    public class DonThuocHoSoKhamChuaBenh
    {
        public List<Thuoc> Thuoc { set; get; }
    }
    public class DvktHoSoKhamChuaBenh
    {
        public List<Dvkt> Dvkt { set; get; }
    }
    public class DichVuCLS
    {
        //  public string idGoc { set; get; }
        public string maDichVu { set; get; }
        public int? maNhom { set; get; }
        public string tenDichVu { set; get; }
        public string ketQua { set; get; }

    }
    public class Thuoc
    {
        public string tenThuoc { set; get; }
        public string duongDung { set; get; }
        public double? soLuong { set; get; }
        public string lieuDung { set; get; }
        public string soDangKy { set; get; }
        public double? thanhTien { set; get; }
        public double? tienNguonKhac { set; get; }
        public double? tienBntt { set; get; }
        public double? tienBhtt { set; get; }
        public double? tienBncct { set; get; }
        public double? tienNgoaids { set; get; }

        public int MaDV { get; set; }
    }

    public class Dvkt
    {
        public string maDichVu { set; get; }
        public string tenDichVu { set; get; }
        public int? maNhom { set; get; }
        public string donViTinh { set; get; }
        public double? soLuong { set; get; }
        public double? thanhTien { set; get; }
        public double? tienNguonKhac { set; get; }
        public double? tienBntt { set; get; }
        public double? tienBhtt { set; get; }
        public double? tienBncct { set; get; }
        public double? tienNgoaids { set; get; }

        public int MaDV { get; set; }
    }

    public class TokenUpload
    {
        public string StatusCode { set; get; }
        public string id_token { set; get; }
    }

    public class DuLieuLienThongYTeCoSo
    {
        public string MaCSKCB { set; get; }
        public danhSachHoSoYTe danhSachHoSoYTe { set; get; }
    }
    public class danhSachHoSoYTe
    {
        public HoSoYTe HoSoYTe { set; get; }
    }
    public class HoSoYTe
    {
        public string maYTeCaNhan { set; get; }
        public ThongTinChung ThongTinChung { set; get; }
        public DanhSachHoSoKhamChuaBenh DanhSachHoSoKhamChuaBenh { set; get; }
        //public string ThongTinKhamThai { set; get; }
        //public string TinhTrangDe { set; get; }
        //public string ThongTinKeHoachHoaGiaDinh { set; get; }
        //public string ThongTinPhaThai { set; get; }
        //public string ThongTinBenhSotRet { set; get; }
        //public string ThongTinBenhTamThanTuKy { set; get; }
        //public string ThongTinBenhLao { set; get; }
        //public string ThongTinBenhHIV { set; get; }
        //public string ThongTinBenhKhongLayNhiem { set; get; }
        //public string ThongTinTuVong { set; get; }
    }
    public class DanhSachHoSoKhamChuaBenh
    {
        public HoSoKhamChuaBenh HoSoKhamChuaBenh { set; get; }
    }
}
