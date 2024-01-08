using System;using QLBV_Database;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Linq;
using System.IO;
namespace QLBV.CLS
{
    class Connect_LIS
    {
        /// <summary>
        /// List Tên File chứa BarCode
        /// </summary>
       
        /// <summary>
        /// Message thông báo trả về
        /// </summary>
        public static string msg = "";

        #region clss
      
        #endregion
        public static bool XML_ChiDinhCLS(QLBV_Database.QLBVEntities data, string path, int idCLS)
        {

            bool rs = false;
            string sIDCLS = idCLS.ToString();
            if (idCLS > 0)
            {
                var q = (from cls in data.CLS.Where(p => p.IdCLS == idCLS)
                         // join bn in data.BenhNhans on cls.MaBNhan equals bn.MaBNhan
                         join cd in data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                         join clsct in data.CLScts on cd.IDCD equals clsct.IDCD
                         join dvct in data.DichVucts on clsct.MaDVct equals dvct.MaDVct
                         join dv in data.DichVus on dvct.MaDV equals dv.MaDV
                         join tnhom in data.TieuNhomDVs on dv.IdTieuNhom equals tnhom.IdTieuNhom
                         select new
                         {
                           STT_F=  dvct.STT_F,
                             PatientId = cls.MaBNhan,
                             MaYTe = "2015010101",
                           OrderId = sIDCLS,//"CD" + cls.NgayThang.Value.Year.ToString() + "." + cls.NgayThang.Value.Month.ToString() + cls.NgayThang.Value.Day.ToString() + (idCLS == null ? "" : idCLS.ToString()),
                             SampleId = cls.BarCode,
                             HoTen = "Vssoft",//bn.TenBNhan.ToUpper(),
                           GioChiDinh = cls.NgayThang==null?DateTime.Now: cls.NgayThang.Value,
                             GioiTinh = "?", //bn.GTinh == 1 ? "M" : ( bn.GTinh == 0 ? "F" : "?"),
                             NamSinh = "2014", //bn.NamSinh,
                             Address = "Vssoft",//bn.DChi,
                             MaDoiTuong = "Vssoft",//dt.DTBN1,
                             ObjectName = "Vssoft",
                             MaKhoaPhong = cls.MaKP,
                             LocationName = "Vssoft",
                             CapCuu = "false",
                             MaLoaiDV = tnhom.IdTieuNhom,
                             TenLoaiDV = tnhom.TenRG,
                             MaDV = dvct.MaDVct,
                             TenDichVu = dvct.TenDVct,
                             KetQua = "",
                             NormalRange = dvct.TSBT
                         }).OrderBy(p=>p.STT_F).ToList();
                if (q.Count > 0)
                {
                    if ((DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049") ? false : String.IsNullOrEmpty(q.First().SampleId))
                    {
                        System.Windows.Forms.MessageBox.Show("Bạn chưa nhập BarCode");
                        rs = false;
                    }
                    else
                    {
                        string maBN = q.First().PatientId == null ? "" : q.First().PatientId.ToString();
                        //try
                        //{
                            XNamespace xsi = XNamespace.Get("http://www.w3.org/2001/XMLSchema-instance");
                            XNamespace xsd = XNamespace.Get("http://www.w3.org/2001/XMLSchema");
                            var xEle = new XElement("PatientInfo",
                                new XAttribute(XNamespace.Xmlns + "xsi", xsi),
                                new XAttribute(XNamespace.Xmlns + "xsd", xsd),
                                new XElement("PatientId", maBN),
                                new XElement("MaYTe", q.First().MaYTe),
                                new XElement("OrderId", q.First().OrderId),
                                new XElement("SampleId", q.First().SampleId),
                                new XElement("HoTen", q.First().HoTen),
                                new XElement("GioChiDinh", q.First().GioChiDinh.ToString("yyyy-MM-dd HH:mm:ss")),
                                new XElement("GioiTinh", q.First().GioiTinh),
                                new XElement("NamSinh", q.First().NamSinh),
                                new XElement("Address", q.First().Address),
                                new XElement("MaDoiTuong", q.First().MaDoiTuong),
                                new XElement("ObjectName", q.First().ObjectName),
                                new XElement("MaKhoaPhong", q.First().MaKhoaPhong),
                                new XElement("CapCuu", q.First().CapCuu),
                                new XElement("ListTestResult",
                                    from item in q
                                    select new XElement("TestResult",
                                               new XElement("MaBenhNhan", maBN),
                                               new XElement("MaLoaiDV", item.MaLoaiDV),
                                               new XElement("TenLoaiDV", item.TenLoaiDV),
                                               new XElement("MaDV", item.MaDV),
                                               new XElement("TenDichVu", item.TenDichVu),
                                               new XElement("KetQua", item.KetQua),
                                               new XElement("NormalRange", item.NormalRange),
                                               new XElement("ListSubTestResult", "")))
                                          );
                            xEle.Save(path + "\\" + idCLS.ToString() + ".xml");
                            rs = true;
                        //}
                        //catch (Exception)
                        //{
                        //    rs = false;
                        //}
                    }
                }
            }
            return rs;
        }
        //-----------------------------------------------------------------------------------------      
       
        /// <summary>
        /// Khi xóa kết quả của 1 chỉ định CLS (1 ID CLS), các file kết quả của ID CLS đó sẽ được lấy lại từ thư mục backUp về thư mục chưa kết quả
        /// </summary>
        /// <param name="idCLS"></param>
        /// <param name="strFilePath">Nơi lưu các file kết quả - DungChung.Bien.xmlFilePath_LIS.Split(';')[1]</param>
        /// <param name="data"></param>
        /// <param name="backUpFolder">Nơi lưu file backUp DungChung.Bien.xmlFilePath_LIS.Split(';')[2]</param>
        public static bool getFileBackup(int idCLS, string strFilePath, QLBV_Database.QLBVEntities data, string backUpFolder)
        {
            bool rs = false;
            var barCode = data.CLS.Where(p => p.IdCLS == idCLS).Select(p => p.BarCode).ToList();
            if (barCode.Count > 0 && barCode.First().ToString() != null)
            {
                try
                {
                    DirectoryInfo d = new DirectoryInfo(backUpFolder);
                    FileInfo[] Files = d.GetFiles("*.xml");
                    string strSubFile = barCode.First().ToString() + "_" + idCLS.ToString();
                    foreach (FileInfo file in Files)
                    {
                        List<Library_CLS.Lis_His.clsChiDinhCLS> list_cls = new List<Library_CLS.Lis_His.clsChiDinhCLS>();
                        int index = file.Name.IndexOf('_');
                        int indexrs = file.Name.IndexOf('_', index + 1);
                        if ((strSubFile + ".xml")== (file.Name) || (indexrs > 1 && file.Name.Substring(0, indexrs) == strSubFile))// so sánh tên file có chưa mã Barcode + id chỉ định
                        {
                            if (System.IO.File.Exists(backUpFolder + "\\" + file.Name))
                            {
                                if (!backUpFile_ConnectList(backUpFolder, file.Name, strFilePath))
                                {
                                    System.Windows.Forms.MessageBox.Show("Lỗi lấy lại file");
                                    rs = false;
                                }
                                else
                                {
                                    rs = true;
                                }
                            }
                        }
                    }
                }


                catch (Exception)
                {
                    rs = false;
                }
            }

            return rs;
        }
        public static bool backUpFile_ConnectList(string filePath, string fileName, string DestinationFolder)
        {
            bool rs = false;
            try
            {
                if (Directory.Exists(DestinationFolder))
                {
                    File.Copy(filePath + "\\" + fileName, DestinationFolder + "\\" + fileName);
                    File.Delete(filePath + "\\" + fileName);
                }
                else
                {
                    DirectoryInfo di = Directory.CreateDirectory(DestinationFolder);
                    File.Copy(filePath + "\\" + fileName, DestinationFolder + "\\" + fileName);
                    File.Delete(filePath + "\\" + fileName);
                }
                rs = true;
            }
            catch (Exception)
            {
                rs = false;
            }

            return rs;

        }

    }
}
