using System;using QLBV_Database;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml;
using System.IO;

namespace QLBV.DungChung
{
    class cls_KetNoiXP_SA
    {
        #region biến

        //private string _mabv; // Lưu trữ mã bệnh viện
        //private string _malk = "";// mã liên kết trong 1 file XML
        //private int _makp = 0;// mã kho tương ứng của BV xã
        public List<Err> _lstErr1TH = new List<Err>();// danh sách lỗi trong thẻ tổng hợp của 1 file xml
        public List<Err> _listErr1Don = new List<Err>();// danh sách lỗi trong thẻ chitiet của 1 file xml
        public List<Err> _listErrAll = new List<Err>();// danh sách lỗi nhiều file xml
        public List<FileInfo> _lAllFileInfo = new List<FileInfo>();// danh sách tất cả các file
        public List<FileInfo> _lFileErr = new List<FileInfo>();// danh sách những file lỗi
        public List<TongHop> lstTongHop = new List<TongHop>();
        private string _messageError;   // Lưu trữ thông báo lỗi        
        List<TongHop> lstTongHop_NotFound = new List<TongHop>();
        List<NhapDct> lstNhapDCT = new List<NhapDct>();
        // List<Err> lstErr1 = new List<Err>();
        QLBV_Database.QLBVEntities db = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        #endregion

        #region class
        public class Duoc
        {
            public string MA_LK { get; set; }
            public string STT { get; set; }
            public string MA_QD { get; set; }
            public string MA_DV { get; set; }
            public string SO_QD { get; set; }
            public string HAN_DUNG { get; set; }
            public string LOSX { get; set; }
            public string NCC { get; set; }
            public string TEN_QD { get; set; }
            public string TEN_DV { get; set; }
            public string DON_GIA { get; set; }
            public string SO_LUONG { get; set; }
            public string THANH_TIEN { get; set; }

        }
        public class TongHop
        {
            public string MA_BV { get; set; }
            public string TEN_BV { get; set; }
            public string MA_LK { get; set; }
            public string NGAY_CT { get; set; }
            public string NGUOIGIAO { get; set; }
            public string MA_KHO_XUAT { get; set; }
            public string TEN_KHO_XUAT { get; set; }
            public string PHAN_LOAI_XUAT { get; set; }
            public string DOI_TUONG { get; set; }
            public string TRANG_THAI { get; set; }
            public string PL_GUINHAN { get; set; }
            public string NHOM_KIEUSO { get; set; }

        }
        public class Err
        {
            private string ma_lk;

            public string Ma_lk
            {
                get { return ma_lk; }
                set { ma_lk = value; }
            }
            private string field_err;

            public string Field_err
            {
                get { return field_err; }
                set { field_err = value; }
            }
            private string mss;

            public string Mss
            {
                get { return mss; }
                set { mss = value; }
            }
            private string maDV;


            public string MaDV
            {
                get { return maDV; }
                set { maDV = value; }
            }
            public string tenfile { set; get; }
        }
        public class ChitietThuoc
        {
            private string stt;
            private string MA_QD;

        }
        #endregion

        #region funtion
        /// <summary>
        /// Xuất ra danh sách các file xml
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="path"></param>
        /// /// <param name="Trang_Thai"> = 0 : xuất dược; 1 : hủy xuất</param>
        /// <returns></returns>


        /// <summary>
        /// Đọc 1 file XML đổ vào CSDL
        /// </summary>
        /// <param name="path">Đường dẫn 1 file XML</param>
        /// <param name="viewOrAdd">true: add, false: view</param>
        /// <returns></returns>
        public bool GetDichVu(string pathFile, bool viewOrAdd)
        {
            //var path = @"D:\Long project\MVC\MVC_QLBV\XML\InputXml\InputXml\Mau_NXT_XP_2.xml";
            bool rs = false;
            db = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var qdv = db.DichVus.ToList();
            Err err = new Err();
            List<Duoc> lstDuoc = new List<Duoc>();
            XDocument doc = new XDocument();
            doc = XDocument.Load(pathFile);

            int idNhap = 0;
            byte dtuong = 0;
            if (doc.Element("NXT") != null)
            {
                if (doc.Element("NXT").Element("TONGHOP") != null)
                {
                    XElement ndTongHop = doc.Element("NXT").Element("TONGHOP");
                    #region đổ ra object TongHop
                    TongHop th = new TongHop();
                    if (ndTongHop.Element("MA_BV") != null)
                        th.MA_BV = ndTongHop.Element("MA_BV").Value.ToString();
                    if (ndTongHop.Element("TEN_BV") != null)
                        th.TEN_BV = ndTongHop.Element("TEN_BV").Value.ToString();
                    if (ndTongHop.Element("MA_LK") != null)
                        th.MA_LK = ndTongHop.Element("MA_LK").Value.ToString();
                    if (ndTongHop.Element("NGAY_CT") != null)
                        th.NGAY_CT = ndTongHop.Element("NGAY_CT").Value.ToString();
                    if (ndTongHop.Element("NGUOIGIAO") != null)
                        th.NGUOIGIAO = ndTongHop.Element("NGUOIGIAO").Value.ToString();
                    if (ndTongHop.Element("MA_KHO_XUAT") != null)
                        th.MA_KHO_XUAT = ndTongHop.Element("MA_KHO_XUAT").Value.ToString();
                    if (ndTongHop.Element("TEN_KHO_XUAT") != null)
                        th.TEN_KHO_XUAT = ndTongHop.Element("TEN_KHO_XUAT").Value.ToString();
                    if (ndTongHop.Element("PHAN_LOAI_XUAT") != null)
                        th.PHAN_LOAI_XUAT = ndTongHop.Element("PHAN_LOAI_XUAT").Value.ToString();
                    if (ndTongHop.Element("DOI_TUONG") != null)
                        th.DOI_TUONG = ndTongHop.Element("DOI_TUONG").Value.ToString();
                    if (ndTongHop.Element("TRANG_THAI") != null)
                        th.TRANG_THAI = ndTongHop.Element("TRANG_THAI").Value.ToString();
                    if (ndTongHop.Element("PL_GUINHAN") != null)
                        th.PL_GUINHAN = ndTongHop.Element("PL_GUINHAN").Value.ToString();
                    #endregion
                    #region đổ ra danh sách Duoc
                    if (doc.Element("NXT").Element("CHITIET") != null)
                    {
                        lstDuoc = (from nd in doc.Element("NXT").Element("CHITIET").Descendants("DICHVU")
                                   select new Duoc
                                   {
                                       MA_LK = th.MA_LK,
                                       STT = nd.Element("STT") == null ? "" : nd.Element("STT").Value,
                                       MA_QD = nd.Element("MA_QD") == null ? "" : nd.Element("MA_QD").Value,
                                       MA_DV = nd.Element("MA_DV") == null ? "" : nd.Element("MA_DV").Value,
                                       SO_QD = nd.Element("SO_QD") == null ? "" : nd.Element("SO_QD").Value,
                                       HAN_DUNG = nd.Element("HAN_DUNG") == null ? "" : nd.Element("HAN_DUNG").Value,
                                       LOSX = nd.Element("LOSX") == null ? "" : nd.Element("LOSX").Value,
                                       NCC = nd.Element("NCC") == null ? "" : nd.Element("NCC").Value,
                                       TEN_QD = nd.Element("TEN_QD") == null ? "" : nd.Element("TEN_QD").Value,
                                       TEN_DV = nd.Element("TEN_DV") == null ? "" : nd.Element("TEN_DV").Value,
                                       DON_GIA = nd.Element("DON_GIA") == null ? "" : nd.Element("DON_GIA").Value,
                                       SO_LUONG = nd.Element("SO_LUONG") == null ? "" : nd.Element("SO_LUONG").Value,
                                       THANH_TIEN = nd.Element("THANH_TIEN") == null ? "" : nd.Element("THANH_TIEN").Value
                                   }
                                   ).ToList();
                    }
                    #endregion

                    if (kt_TongHop(th, pathFile) && kt_ChiTiet(lstDuoc, qdv, th.MA_LK, pathFile))
                    {
                        if (viewOrAdd)
                        {
                            AddToDB(th, lstDuoc, db, qdv);
                        }
                        rs = true;
                    }

                }

            }


            return rs;
        }
        private bool CheckedAndDel(int makp, string malk)
        {
            bool rs = false;
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var q = data.NhapDs.Where(p => p.SoCT == malk && p.MaKP == makp).ToList();
            if (q.Count == 0)
                rs = true;
            foreach (var a in q)
            {
                int idNhap = a.IDNhap;
                var qnhapct = data.NhapDcts.Where(p => p.IDNhap == idNhap).ToList();
                foreach (var b in qnhapct)
                {
                    data.NhapDcts.Remove(b);
                }
                data.NhapDs.Remove(a);
                data.SaveChanges();
                rs = true;
            }
            return rs;

        }
        public bool AddToDB(TongHop th, List<Duoc> lstDuoc, QLBV_Database.QLBVEntities db, List<DichVu> qdv)
        {
            db = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            int makp = getMaKP(th.MA_BV);
            int idNhap = 0;
            bool rs = false;
            //  var qdv = db.DichVus.ToList();
            if (CheckedAndDel(makp, th.MA_LK))
            {
                #region thêm nhapD
                NhapD nhapDuoc = new NhapD();
                if (ktDateTime(th.NGAY_CT, "yyyyMMddHHmm"))
                    nhapDuoc.NgayNhap = DateTime.ParseExact(th.NGAY_CT, "yyyyMMddHHmm", null);
                nhapDuoc.MaKP = makp;
                nhapDuoc.TenNguoiCC = th.NGUOIGIAO;
                byte dtuong = (byte)int.Parse(th.DOI_TUONG);
           
                nhapDuoc.Status = 3;// nhận tuyến dưới ( lưu thông tin tuyến dưới xuất dược)
                nhapDuoc.KieuDon = Convert.ToInt32(th.PHAN_LOAI_XUAT);
                nhapDuoc.PLoai = 2;// xuất dược
                nhapDuoc.SoCT = th.MA_LK;
                // ma_lk = int.Parse(ndTongHop.Element("MA_LK").Value.Trim());// Tạm nhập IDNhap vào trường MaBNhan   
                // nhapDuoc.MaBNhan = ma_lk;

                db.NhapDs.Add(nhapDuoc);
                int count = db.SaveChanges();
                var nhapd_new = db.NhapDs.Where(p => p.MaKP == makp && p.PLoai == 2 && p.Status == 3 && p.SoCT == th.MA_LK).OrderByDescending(p => p.IDNhap).FirstOrDefault();
                if (nhapd_new != null)
                    idNhap = nhapd_new.IDNhap;

                #endregion
                #region thêm nhapdct
                if (idNhap > 0)
                {
                    foreach (var a in lstDuoc)
                    {
                        DichVu dv = qdv.Where(p => p.MaQD == a.MA_QD).FirstOrDefault();//.Where(p => p.SoQD == a.SO_QD).Where(p => p.TenHC == a.TEN_QD).FirstOrDefault();
                        NhapDct nhapdct = new NhapDct();
                        nhapdct.IDNhap = idNhap;
                        nhapdct.MaDV = dv.MaDV; //?
                        nhapdct.DonVi = dv.DonVi;
                        nhapdct.DonGia = Convert.ToDouble(a.DON_GIA);
                        nhapdct.SoLuongX = Convert.ToDouble(a.SO_LUONG);
                        nhapdct.ThanhTienX = Convert.ToDouble(a.THANH_TIEN);
                        nhapdct.IDDTBN = dtuong; // dùng riêng cho Tam Đường: Đối tượng: 1- BHYT, 2- trẻ em, 3-Đối tượng 139, 4- Khác
                        if (ktDateTime(a.HAN_DUNG, "yyyyMMdd"))
                            nhapdct.HanDung = DateTime.ParseExact(a.HAN_DUNG, "yyyyMMdd", null);
                        db.NhapDcts.Add(nhapdct);
                    }
                    db.SaveChanges();
                    rs = true;
                }
                #endregion
            }
            return rs;
        }
        #endregion
        #region funtion
        /// <summary>
        /// Xuất ra danh sách các file xml
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="path"></param>
        /// /// <param name="Trang_Thai"> = 0 : xuất dược; 1 : hủy xuất</param>
        /// <returns></returns>
        public static bool ExportXML(int idnhap, string pathFolder, int Trang_Thai)
        {
            bool rs = false;

            QLBV_Database.QLBVEntities db = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            //var NhapD2 = db.NhapDs.Where(p => p.KieuDon == 3 && p.Status == 2).ToList();// kiểu đơn = 3: xuất ngoài BV; Status = 2: xuất tuyến dưới
            // var nhap = (from a in NhapD2 where a.NgayNhap.Value.Date == dt.Date select a).ToList();

            try
            {
                NhapD n = db.NhapDs.Single(p => p.IDNhap == idnhap);


                var nhap = (from a in db.NhapDcts.Where(p => p.IDNhap == idnhap)
                            join dv in db.DichVus on a.MaDV equals dv.MaDV
                            group new { a, dv } by new { a.MaBNhan, a.MaDV, a.DonGia, a.HanDung, a.SoLo, dv.MaQD, dv.MaCC, dv.SoQD, dv.TenDV, dv.TenHC } into kq
                            select new { kq.Key.MaBNhan, kq.Key.MaDV, SoLuongX = kq.Sum(p => p.a.SoLuongX), kq.Key.DonGia, ThanhTienX = kq.Sum(p => p.a.ThanhTienX), kq.Key.HanDung, kq.Key.SoLo, kq.Key.MaQD, kq.Key.MaCC, kq.Key.SoQD, kq.Key.TenDV, kq.Key.TenHC }).ToList();

                List<KPhong> lkp = db.KPhongs.ToList();

                if (n != null)
                {
                    try
                    {
                        var benhvien = lkp.Where(x => x.MaKP == n.MaKPnx).FirstOrDefault();
                        var khoxuat = lkp.Where(x => x.MaKP == n.MaKP).SingleOrDefault();
                        var canbo = db.CanBoes.ToList();
                        string tenFile = (benhvien == null ? "" : benhvien.MaKP.ToString()) + "_" + n.IDNhap + "_" + (n.NgayNhap == null ? "" : n.NgayNhap.Value.ToString("yyyyMMddHHmm")) + ".xml"; //dt.ToString("yyyyMMddHHmmss") + ".xml";
                        BenhNhan bn = db.BenhNhans.Where(x => x.MaBNhan == n.MaBNhan).SingleOrDefault();
                        string dtuong = "";
                        if (bn != null && bn.SThe != null)
                        {
                            if (bn.SThe != null && bn.SThe.Length == 15)
                            {
                                string maDT = bn.SThe.Substring(0, 2);

                                if (maDT == "TE")
                                    dtuong = "2";
                                else if (maDT == "DT" || maDT == "HN" || maDT == "DK")
                                    dtuong = "3";
                                else
                                    dtuong = "1";
                            }
                            else
                                dtuong = "4";
                        }
                        else
                            dtuong = "4";
                        int stt = 1;
                        var xEle1 = new XElement("NXT",
                           new XElement("TONGHOP",
                            // new XElement("MA_CSKCB", moi.ma_cskcb),
                                                  new XElement("MA_BV", benhvien == null ? "" : benhvien.MaQD.ToString()),
                                                  new XElement("TEN_BV", benhvien == null ? "" : benhvien.TenKP),
                                                  new XElement("MA_LK", n.IDNhap.ToString()),
                                                  new XElement("NGAY_CT", n.NgayNhap == null ? "" : n.NgayNhap.Value.ToString("yyyyMMddHHmm")),
                                                  new XElement("NGUOIGIAO", canbo.Where(p => p.MaCB == n.MaCB).Select(p => p.TenCB).FirstOrDefault()), //benhvien == null ? "" : benhvien.TenKP);
                                                  new XElement("MA_KHO_XUAT", n.MaKP == null ? "" : n.MaKP.ToString()),
                                                  new XElement("TEN_KHO_XUAT", khoxuat == null ? "" : khoxuat.TenKP),
                                                  new XElement("PHAN_LOAI_XUAT", n.KieuDon == null ? "-1" : n.KieuDon.ToString()),
                                                  new XElement("DOI_TUONG", dtuong),
                                                  new XElement("TRANG_THAI", Trang_Thai.ToString()),
                                                  new XElement("PL_GUINHAN", "1"),
                                                  new XElement("NHOM_KIEUSO", "1")),
                                              new XElement("CHITIET",
                                                  from item in nhap
                                                  select
                                                       new XElement("DICHVU",
                                                           new XElement("STT", stt++),
                                                           new XElement("MA_QD", item.MaQD),// lấy mã QD
                                                           new XElement("MA_DV", item.MaDV),
                                                           new XElement("SO_QD", item.SoQD),
                                                           new XElement("HAN_DUNG", item.HanDung == null ? "" : item.HanDung.Value.ToString("yyyyMMdd")),
                                                           new XElement("NCC", item.MaCC),
                                                           new XElement("TEN_QD", item.TenHC),
                                                           new XElement("TEN_DV", item.TenDV),
                                                           new XElement("DON_GIA", item.DonGia),
                                                           new XElement("SO_LUONG", item.SoLuongX),
                                                           new XElement("THANH_TIEN", item.ThanhTienX))));
                        xEle1.Save(pathFolder + "\\"+ tenFile);
                        rs = true;
                        if (Trang_Thai == 0)
                        {
                            n.Status = 2;

                        }
                        else
                        {
                            n.Status = 0;
                        }
                        db.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        return false;
                    }
                }
            }
            catch
            {
                return false;
            }

            return rs;
        }
        public int GetDSDichVu(string getFolder, string backFolder, bool viewOrAdd, List<string> lstStr)
        {
            int count = 0;
            if (getFolder != "")
            {
                _listErrAll = new List<Err>();
                _lAllFileInfo = new List<FileInfo>();
                _lFileErr = new List<FileInfo>();

                DirectoryInfo d = new DirectoryInfo(getFolder);//Assuming Test is your Folder
                FileInfo[] Files = d.GetFiles("*.xml"); //Getting Text files  
                _lAllFileInfo = (from a in Files.ToList() join b in lstStr on a.Name equals b select a).ToList();

                //_lAllFileInfo = Files.OrderBy(p => p.LastWriteTime).ToList();
                foreach (FileInfo file in _lAllFileInfo)
                {
                    if (GetDichVu(file.FullName, viewOrAdd)) // nếu là thêm mới và thêm mới thành công mới di chuyển file
                    {
                        if (viewOrAdd)
                        {
                            MoveFile(file.Name, getFolder, backFolder);
                            count++;


                        }
                    }
                    _listErrAll.AddRange(_lstErr1TH);
                    _listErrAll.AddRange(_listErr1Don);
                    if (_lstErr1TH.Count + _listErr1Don.Count > 0)
                        _lFileErr.Add(file);

                }

            }
            return count;
        }
        /// <summary>
        /// Đọc 1 file XML đổ vào CSDL
        /// </summary>
        /// <param name="path">Đường dẫn 1 file XML</param>
        /// <param name="viewOrAdd">true: add, false: view</param>
        /// <returns></returns>

        #endregion
        #region validate
        private bool kt_TongHop(TongHop th, string pathFile)
        {
            int ot;
            bool rs = true;
            _lstErr1TH = new List<Err>();
            //  _malk = "";
            if (String.IsNullOrEmpty(th.PL_GUINHAN))
            {
                rs = false;
            }
            else if (!Int32.TryParse(th.PL_GUINHAN, out ot) || int.Parse(th.PL_GUINHAN) != 0)//kiểm tra là file xuất dược do tuyến xã gửi lên
            {
                rs = false;

            }
            else if (String.IsNullOrEmpty(th.MA_BV))
            {
                Err newErr = new Err();
                newErr.Ma_lk = th.MA_LK;
                newErr.Field_err = "MA_BV";
                newErr.Mss = "MA_BV không được để trống";
                newErr.tenfile = pathFile;
                _lstErr1TH.Add(newErr);
                rs = false;
            }
            else if (!String.IsNullOrEmpty(th.MA_BV))
            {
                if (getMaKP(th.MA_BV) <= 0)
                {
                    Err newErr = new Err();
                    newErr.Ma_lk = th.MA_LK;
                    newErr.Field_err = "MA_BV";
                    newErr.Mss = "Không tìm thấy MaKP tương ứng với mã bệnh viện";
                    newErr.tenfile = pathFile;
                    _lstErr1TH.Add(newErr);
                    rs = false;
                }
            }
            else if (!String.IsNullOrEmpty(th.MA_LK))
            {
                Err newErr = new Err();
                newErr.Ma_lk = th.MA_LK;
                newErr.Field_err = "MA_LK";
                newErr.Mss = "MA_LK không được để trống";
                newErr.tenfile = pathFile;
                _lstErr1TH.Add(newErr);
                rs = false;
            }
            else
            {
                // _malk = th.Element("MA_LK").Value.ToString();

                if (!ktDateTime(th.NGAY_CT, "ddMMyyyyhhmm"))
                {
                    Err newErr = new Err();
                    newErr.Ma_lk = th.MA_LK;
                    newErr.Field_err = "NGAY_CT";
                    newErr.Mss = "NGAY_CT chưa nhập đúng định dạng";
                    newErr.tenfile = pathFile;
                    _lstErr1TH.Add(newErr);
                    rs = false;

                }
                else if (!Int32.TryParse(th.DOI_TUONG, out ot))
                {
                    Err newErr = new Err();
                    newErr.Ma_lk = th.MA_LK;
                    newErr.Field_err = "DOI_TUONG";
                    newErr.Mss = "DOI_TUONG không đúng định dạng";
                    newErr.tenfile = pathFile;
                    _lstErr1TH.Add(newErr);
                    rs = false;

                }
                else if (!Int32.TryParse(th.PHAN_LOAI_XUAT, out ot))
                {
                    Err newErr = new Err();
                    newErr.Ma_lk = th.MA_LK;
                    newErr.Field_err = "PHAN_LOAI_XUAT";
                    newErr.Mss = "PHAN_LOAI_XUAT không đúng định dạng";
                    newErr.tenfile = pathFile;
                    _lstErr1TH.Add(newErr);
                    rs = false;

                }
            }
            return rs;

        }
        private bool kt_ChiTiet(List<Duoc> lstDuoc, List<DichVu> qdv, string malk, string pathFile)
        {
            _listErr1Don = new List<Err>();
            Err er = new Err();
            bool rs = true;
            if (lstDuoc.Count <= 0)
            {
                er = new Err();
                er.Ma_lk = malk;
                er.Field_err = "";
                er.Mss = "File không có thuốc hoặc vật tư y tế";
                er.tenfile = pathFile;
                _listErr1Don.Add(er);
                return false;
            }
            foreach (var a in lstDuoc)
            {
                DichVu dv = qdv.Where(p => p.MaQD == a.MA_QD).FirstOrDefault();//.Where(p => p.SoQD == a.SO_QD).Where(p => p.TenHC == a.TEN_QD).FirstOrDefault();
                if (dv == null)
                {

                    er = new Err();
                    er.Ma_lk = malk;
                    er.Field_err = "MaQD";
                    er.Mss = "Không tìm được dịch vụ có mã tương đương";
                    er.tenfile = pathFile;
                    er.MaDV = a.MA_QD;
                    _listErr1Don.Add(er);
                    rs = false;
                }
                else
                {

                    if (string.IsNullOrEmpty(a.DON_GIA))
                    {
                        er = new Err();
                        er.Ma_lk = malk;
                        er.Field_err = "DON_GIA";
                        er.Mss = "Đơn giá không được để trống";
                        er.tenfile = pathFile;
                        er.MaDV = a.MA_QD;
                        _listErr1Don.Add(er);
                        rs = false;
                    }
                    else if (string.IsNullOrEmpty(a.SO_LUONG))
                    {
                        er = new Err();
                        er.Ma_lk = malk;
                        er.Field_err = "SO_LUONG";
                        er.Mss = "Số lượng không được để trống";
                        er.tenfile = pathFile;
                        er.MaDV = a.MA_QD;
                        _listErr1Don.Add(er);
                        rs = false;
                    }
                    else if (string.IsNullOrEmpty(a.THANH_TIEN))
                    {
                        er = new Err();
                        er.Ma_lk = malk;
                        er.Field_err = "THANH_TIEN";
                        er.Mss = "Đơn giá không được để trống";
                        er.tenfile = pathFile;
                        er.MaDV = a.MA_QD;
                        _listErr1Don.Add(er);
                        rs = false;
                    }

                    else
                    {
                        double otd;

                        if (!Double.TryParse(a.DON_GIA, out otd))
                        {
                            er = new Err();
                            er.Ma_lk = malk;
                            er.Field_err = "DON_GIA";
                            er.Mss = "Đơn giá phải là kiểu số";
                            er.tenfile = pathFile;
                            er.MaDV = a.MA_QD;
                            _listErr1Don.Add(er);
                            rs = false;

                        }
                        else if (!Double.TryParse(a.SO_LUONG, out otd))
                        {
                            er = new Err();
                            er.Ma_lk = malk;
                            er.Field_err = "SO_LUONG";
                            er.Mss = "Số lượng phải là kiểu số";
                            er.tenfile = pathFile;
                            er.MaDV = a.MA_QD;
                            _listErr1Don.Add(er);
                            rs = false;

                        }
                        else if (!Double.TryParse(a.THANH_TIEN, out otd))
                        {
                            er = new Err();
                            er.Ma_lk = malk;
                            er.Field_err = "THANH_TIEN";
                            er.Mss = "Đơn giá không phải là kiểu số";
                            er.tenfile = pathFile;
                            er.MaDV = a.MA_QD;
                            _listErr1Don.Add(er);
                            rs = false;
                        }

                        else if (Convert.ToDouble(a.DON_GIA) * Convert.ToDouble(a.SO_LUONG) != Convert.ToDouble(a.THANH_TIEN))
                        {
                            er = new Err();
                            er.Ma_lk = malk;
                            er.Field_err = "";
                            er.Mss = "Thành tiền không bằng đơn giá * số lượng";
                            er.tenfile = pathFile;
                            er.MaDV = a.MA_QD;
                            _listErr1Don.Add(er);
                            rs = false;
                        }
                        //else if (!String.IsNullOrEmpty(a.HAN_DUNG) && !ktDateTime(a.HAN_DUNG, "yyyyMMdd"))   // hạn dùng có thể để rỗng                 
                        //{
                        //    er = new Err();
                        //    er.Ma_lk = malk;
                        //    er.Field_err = "HAN_DUNG";
                        //    er.Mss = "Hạn dùng không đúng định dạng";
                        //    er.tenfile = pathFile;
                        //    er.MaDV = a.MA_QD;
                        //    _listErr1Don.Add(er);
                        //    rs = false;
                        //}
                    }
                }
            }

            return rs;

        }
        #endregion
        #region /hàm phụ trợ
        /// <summary>
        /// Hàm kiểm tra datetime có đúng định dạng không
        /// </summary>
        /// <param name="datestr">chuối string datetime</param>
        /// <param name="fomatType">kiểu fomat vd: "yyyyMMdd", "yyyyMMddhhmm"</param>
        /// <returns></returns>
        private bool ktDateTime(string datestr, string fomatType)
        {
            if (String.IsNullOrEmpty(datestr))
            {
                return false;
            }
            else
            {
                DateTime dt;
                if (DateTime.TryParseExact(datestr,
                                                      fomatType,
                                                      System.Globalization.CultureInfo.InvariantCulture,
                                                      System.Globalization.DateTimeStyles.None,
                                                      out dt))
                    return true;
                else return false;
            }
        }
        /// <summary>
        /// Trả về MAKP của kho dược xã
        /// </summary>
        /// <param name="maBV"></param>
        /// <returns></returns>
        private int getMaKP(string maBV)
        {
            QLBV_Database.QLBVEntities db = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var getMaKP = db.KPhongs.Where(m => m.MaQD == maBV).Where(p => p.TrongBV == 0).Where(p => p.PLoai == "Khoa dược").FirstOrDefault(); //coi kho dược của xã như 1 kho trong BV
            if (getMaKP != null)
                return getMaKP.MaKP;
            else
                return 0;
        }
        /// <summary>
        /// Hàm di chuyển file
        /// </summary>
        /// <param name="fileName">Tên file</param>
        /// <param name="frompath">Thư mục lấy file</param>
        /// <param name="despath">thư mục đích</param>
        private void MoveFile(string fileName, string frompath, string despath)
        {
            DirectoryInfo d = new DirectoryInfo(frompath);
            FileInfo[] Files = d.GetFiles("*.xml");
            foreach (FileInfo file in Files)
            {
                if (file.Name == fileName)
                {
                    if (System.IO.File.Exists(despath + "\\" + fileName))
                    {
                        System.IO.File.Delete(despath + "\\" + fileName);
                    }
                    File.Move(file.FullName, despath + "\\" + fileName);
                    break;

                }
            }


        }
        #endregion



    }
}
