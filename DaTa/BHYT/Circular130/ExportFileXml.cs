using QLBV.Models.Business.HealthInsuranceAppraisals.Circular130;
using QLBV.Providers.StoredProcedure;
using QLBV.Utilities.Commons;
using QLBV_Database;
using QLBV_Database.Common;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static QLBV.DungChung.cls_KetNoi_BHXH;

namespace QLBV.DaTa.BHYT.Circular130
{
    public class ExportFileCircular130
    {
        private readonly ExcuteStoredProcedureProvider _excuteStoredProcedureProvider;
        private readonly QLBVEntities _dataContext;

        public ExportFileCircular130()
        {
            if (_excuteStoredProcedureProvider == null)
                _excuteStoredProcedureProvider = new ExcuteStoredProcedureProvider();

            if (_dataContext == null)
                _dataContext = EntityDbContext.DbContext;
        }

        public bool ExportXml(int patientId)
        {
            var isExport = true;

            try
            {
                BenhNhan benhNhan = _dataContext.BenhNhans.FirstOrDefault(p => p.MaBNhan == patientId && p.SThe != null && p.SThe.Length == 15);
                VienPhi vienPhi = _dataContext.VienPhis.FirstOrDefault(p => p.MaBNhan == patientId);

                Circular130XmlGeneral xmlGeneral = new Circular130XmlGeneral()
                {
                    HospitalInfo = new HospitalInfo()
                    {
                        HospitalCode = DungChung.Bien.MaBV
                    },
                    ProfileInfo = new ProfileInfo()
                    {
                        CreteTime = DateTime.Today.ToString("yyyyMMdd"),
                        Quantity = "1",
                        FileInfos = new List<Models.Business.HealthInsuranceAppraisals.Circular130.FileInfo>()
                        {
                            new Models.Business.HealthInsuranceAppraisals.Circular130.FileInfo()
                            {
                                FileInfoDetails = GetXml(patientId)
                            }
                        }
                    }
                };

                var xmls = XMLHelper.SerializeObject(xmlGeneral, "GIAMDINHHS");
                string fileName = string.Format("{0}\\{1}_{2}_{3}.xml", DungChung.Bien.xmlFilePath_LIS[7], DateTime.Now.ToString("yyyyMMddHHmmss"), benhNhan.SThe, vienPhi.NgayTT.Value.ToString("yyyy_MM"));
                File.WriteAllText(fileName, xmls);
            }
            catch (Exception ex)
            {
                isExport = false;
            }

            return isExport;
        }

        public List<FileInfoDetail> GetXml(int patientId)
        {
            var para = new Dictionary<string, string>()
            {
                { "@mabn", patientId.ToString() },
            };

            var queryResult = _excuteStoredProcedureProvider.ExcuteStoredProcedureQueryMultiple("[dbo].[sp_NewXML]", para);

            var xml01 = queryResult.Read<Circular130Xml01>().FirstOrDefault();
            var xml02 = new Circular130Xml02()
            {
                CHI_TIET_THUOC = queryResult.Read<Circular130Xml02Detail>().ToList()
            };
            var xml03 = new Circular130Xml03()
            {
                CHI_TIET_DVKT = queryResult.Read<Circular130Xml03Detail>().ToList()
            };
            var xml04 = new Circular130Xml04()
            {
                CHI_TIET_CLS = queryResult.Read<Circular130Xml04Detail>().ToList()
            };
            var xml05 = new Circular130Xml05()
            {
                CHI_TIET_DIEN_BIEN_CLS = queryResult.Read<Circular130Xml05Detail>().ToList()
            };
            var xml06 = new Circular130Xml06()
            {
                CHAM_SOC_HIV = queryResult.Read<Circular130Xml06Detail>().ToList()
            };
            var xml07 = queryResult.Read<Circular130Xml07>().FirstOrDefault();
            var xml08 = queryResult.Read<Circular130Xml08>().FirstOrDefault();
            var xml09 = queryResult.Read<Circular130Xml09>().FirstOrDefault();
            var xml10 = queryResult.Read<Circular130Xml10>().FirstOrDefault();
            var xml11 = queryResult.Read<Circular130Xml11>().FirstOrDefault();
            var xml12 = queryResult.Read<Circular130Xml12>().FirstOrDefault();

            var results = new List<FileInfoDetail>();

            //if (xml01 != null)
            {
                var xmlData = new FileInfoDetail()
                {
                    Type = "XML1",
                    Content = xml01 == null ? null : Security.Base64Encode(XMLHelper.SerializeObject(xml01))
                };
                results.Add(xmlData);
            }
            //if (xml02 != null && xml02.CHI_TIET_THUOC != null && xml02.CHI_TIET_THUOC.Count > 0)
            {
                var xmlData = new FileInfoDetail()
                {
                    Type = "XML2",
                    Content = xml02.CHI_TIET_THUOC == null || xml02.CHI_TIET_THUOC.Count == 0 ? null : Security.Base64Encode(XMLHelper.SerializeObject(xml02))
                };
                results.Add(xmlData);
            }
            //if (xml03 != null && xml03.CHI_TIET_DVKT != null && xml03.CHI_TIET_DVKT.Count > 0)
            {
                var xmlData = new FileInfoDetail()
                {
                    Type = "XML3",
                    Content = xml03.CHI_TIET_DVKT == null || xml03.CHI_TIET_DVKT.Count == 0 ? null : Security.Base64Encode(XMLHelper.SerializeObject(xml03))
                };
                results.Add(xmlData);
            }
            //if (xml04 != null && xml04.CHI_TIET_CLS != null && xml04.CHI_TIET_CLS.Count > 0)
            {
                var xmlData = new FileInfoDetail()
                {
                    Type = "XML4",
                    Content = xml04.CHI_TIET_CLS == null || xml04.CHI_TIET_CLS.Count == 0 ? null : Security.Base64Encode(XMLHelper.SerializeObject(xml04))
                };
                results.Add(xmlData);
            }
            //if (xml05 != null && xml05.CHI_TIET_DIEN_BIEN_CLS != null && xml05.CHI_TIET_DIEN_BIEN_CLS.Count > 0)
            {
                var xmlData = new FileInfoDetail()
                {
                    Type = "XML5",
                    Content = xml05.CHI_TIET_DIEN_BIEN_CLS == null ? null : Security.Base64Encode(XMLHelper.SerializeObject(xml05))
                };
                results.Add(xmlData);
            }
            //if (xml06 != null && xml06.CHAM_SOC_HIV != null && xml06.CHAM_SOC_HIV.Count > 0)
            {
                var xmlData = new FileInfoDetail()
                {
                    Type = "XML6",
                    Content = xml06.CHAM_SOC_HIV == null || xml06.CHAM_SOC_HIV.Count == 0 ? null : Security.Base64Encode(XMLHelper.SerializeObject(xml06))
                };
                results.Add(xmlData);
            }
            //if (xml07 != null)
            {
                var xmlData = new FileInfoDetail()
                {
                    Type = "XML7",
                    Content = xml07 == null ? null : Security.Base64Encode(XMLHelper.SerializeObject(xml07))
                };
                results.Add(xmlData);
            }
            //if (xml08 != null)
            {
                var xmlData = new FileInfoDetail()
                {
                    Type = "XML8",
                    Content = xml08 == null ? null : Security.Base64Encode(XMLHelper.SerializeObject(xml08))
                };
                results.Add(xmlData);
            }
            //if (xml09 != null)
            {
                var xmlData = new FileInfoDetail()
                {
                    Type = "XML9",
                    Content = xml09 == null ? null : Security.Base64Encode(XMLHelper.SerializeObject(xml09))
                };
                results.Add(xmlData);
            }
            //if (xml10 != null)
            {
                var xmlData = new FileInfoDetail()
                {
                    Type = "XML10",
                    Content = xml10 == null ? null : Security.Base64Encode(XMLHelper.SerializeObject(xml10))
                };
                results.Add(xmlData);
            }
            //if (xml11 != null)
            {
                var xmlData = new FileInfoDetail()
                {
                    Type = "XML11",
                    Content = xml11 == null ? null : Security.Base64Encode(XMLHelper.SerializeObject(xml11))
                };
                results.Add(xmlData);
            }
            //if (xml12 != null)
            {
                var xmlData = new FileInfoDetail()
                {
                    Type = "XML12",
                    Content = xml12 == null ? null : Security.Base64Encode(XMLHelper.SerializeObject(xml12))
                };
                results.Add(xmlData);
            }

            return results;
        }

        private bool UpdateMaGiaoDich(int maBn, string magiaodich)
        {
            try
            {
                var vp = _dataContext.VienPhis.Where(p => p.MaBNhan == maBn).ToList();
                foreach (var item in vp)
                {
                    item.MaGD_BHXH = magiaodich;
                    item.NgayGuiBHXH = DateTime.Now;
                    _dataContext.SaveChanges();
                }

                return false;
            }
            catch
            {
                return true;
            }
        }

        private int CheckThongTuyen(string maNoiKcb, string maDkKcb)
        {
            int ttuyen = 1;

            var bv = _dataContext.BenhViens.Select(s => new
            {
                s.MaBV,
                s.HangBV,
                s.TuyenBV,
                s.MaHuyen
            });

            var bvkham = bv.FirstOrDefault(p => p.MaBV == maNoiKcb);
            var bvdk = bv.FirstOrDefault(p => p.MaBV == maDkKcb);

            if (bvdk != null && bvkham != null)
            {
                if (bvkham.TuyenBV.Trim().ToUpper() == "C" && (bvdk.TuyenBV.Trim().ToUpper() == "C" || bvdk.TuyenBV.Trim().ToUpper() == "D"))
                {
                    if (bvkham.MaHuyen != bvdk.MaHuyen)
                    {
                        ttuyen = 4;
                    }
                }
            }

            return ttuyen;
        }

        public double GetMucHuong(int hangBV, string mathe, int tuyen, int noingoaitru, DateTime ngayTT)
        {
            double muctt = 0;
            string mamuc = "";

            if (mathe.Length > 2 && ngayTT != null)
            {
                mamuc = mathe.Substring(2, 1);
                var qmuc = _dataContext.MucTTs.Where(p => p.MaMuc == mamuc).ToList();
                if (qmuc.Count > 0 && qmuc.First().PTTT != null)
                {
                    if (tuyen == 1) // đúng tuyến
                    {
                        muctt = Convert.ToDouble(qmuc.First().PTTT.ToString());
                    }
                    else // trái tuyến
                    {
                        double tylevuottuyen = 0;

                        if (noingoaitru == 0)
                        {
                            if (ngayTT >= new DateTime(2015, 1, 1) && ngayTT < new DateTime(2016, 1, 1))
                            {
                                switch (hangBV)
                                {
                                    case 3:
                                        tylevuottuyen = 0.7;
                                        break;
                                }
                            }
                            else if (ngayTT >= new DateTime(2016, 1, 1))
                            {
                                if (hangBV == 4 || hangBV == 3)
                                    tylevuottuyen = 1;
                            }
                        }
                        else if (noingoaitru == 1) // nội trú
                        {
                            if (ngayTT >= new DateTime(2015, 1, 1) && ngayTT < new DateTime(2016, 1, 1))
                                switch (hangBV)
                                {
                                    case 3:
                                        tylevuottuyen = 0.7;
                                        break;
                                    case 2:
                                        tylevuottuyen = 0.6;
                                        break;
                                    case 1:
                                        tylevuottuyen = 0.4;
                                        break;
                                }
                            else if (ngayTT >= new DateTime(2016, 1, 1) && ngayTT < new DateTime(2021, 1, 1))
                                switch (hangBV)
                                {
                                    case 4:
                                        tylevuottuyen = 1;
                                        break;
                                    case 3:
                                        tylevuottuyen = 1;
                                        break;
                                    case 2:
                                        tylevuottuyen = 0.6;
                                        break;
                                    case 1:
                                        tylevuottuyen = 0.4;
                                        break;
                                }
                            else if (ngayTT >= new DateTime(2021, 1, 1))
                            {
                                switch (hangBV)
                                {
                                    case 4:
                                        tylevuottuyen = 1;
                                        break;
                                    case 3:
                                        tylevuottuyen = 1;
                                        break;
                                    case 2:
                                        tylevuottuyen = 1;
                                        break;
                                    case 1:
                                        tylevuottuyen = 0.4;
                                        break;
                                }
                            }
                        }
                        muctt = Convert.ToDouble(qmuc.First().PTTT) * tylevuottuyen;
                    }
                }
            }

            return muctt;
        }

        private string LayLieuDung(int mabn, int MaDV)
        {
            string lieuDung = "";
            var qdt = (from dt in _dataContext.DThuocs.Where(p => p.MaBNhan == mabn).Where(p => p.PLDV == 1)
                       join dtct in _dataContext.DThuoccts.Where(p => p.MaDV == MaDV).Where(p => p.Luong != null && p.SoLan != null) on dt.IDDon equals dtct.IDDon
                       select dtct).FirstOrDefault();
            if (qdt != null)
                lieuDung = qdt.Luong + qdt.DviUong + "/lần" + "*" + qdt.SoLan + "lần/ngày";

            return lieuDung;
        }

        private string LayMaKhoaTheoQD(int? maKp)
        {
            if (maKp == null)
                return "";
            else
            {
                var kp = _dataContext.KPhongs.Where(p => p.MaKP == maKp).Select(p => p.MaQD).FirstOrDefault();
                if (kp == null)
                    return "";
                else if (kp.Length < 2)
                    return "";
                else
                    return kp;
            }
        }

        private string LayMaBacSy(int maBN, int? maDV, int? maKP)
        {
            var qdtct = (from dthuoc in _dataContext.DThuocs.Where(p => p.MaBNhan == maBN)
                         join dtct in _dataContext.DThuoccts.Where(p => p.MaDV == maDV) on dthuoc.IDDon equals dtct.IDDon
                         join cb in _dataContext.CanBoes on dthuoc.MaCB equals cb.MaCB
                         select new { dthuoc.MaKP, cb.MaCCHN, cb.MaCB }).ToList();

            if (qdtct.Count == 0)
                return "";

            else
            {
                var qdtct1 = (from dt in qdtct.Where(p => p.MaKP == maKP) select dt).FirstOrDefault();// chỗ này cần lấy theo mã số chứng chỉ hành nghề
                if (qdtct1 != null)
                {
                    if (string.IsNullOrEmpty(qdtct1.MaCCHN))
                        return "";
                    else
                        return qdtct1.MaCCHN.ToString();
                }
                else
                {
                    if (string.IsNullOrEmpty(qdtct.First().MaCCHN))
                        return "";
                    else
                        return qdtct.First().MaCCHN.ToString();
                }
            }
        }
    }
}
