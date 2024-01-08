using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Collections.Generic;


namespace QLBV.BaoCao
{
    public partial class rep_ChuyenTuyen : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_ChuyenTuyen()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        List<BenhVien> _listBenhVien = new List<BenhVien>();
        DateTime _tungay = new DateTime();
        DateTime _denngay = new DateTime();
        int mackhoa(string tenck, List<ChuyenKhoa> lck)
        {
            if (tenck == null)
                return 99;

            var ten = lck.Where(p => p.TenCK == tenck.ToString()).Select(p => p.MaCK).ToList();
            if (ten.Count > 0)
                return ten.First();
            else return 99;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tungay"></param>
        /// <param name="denngay"></param>
        /// <param name="ckNgay">0: cả ngày ra lẫn ngày vào; 1: Ngày vào; 2: ngày ra</param>
        public rep_ChuyenTuyen(DateTime tungay, DateTime denngay, int ckNgay)
        {
            InitializeComponent();
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            List<ChuyenKhoa> lck = data.ChuyenKhoas.ToList();
            _listBenhVien = data.BenhViens.ToList();
            _tungay = tungay;
            _denngay = denngay;
            #region chuyển tuyến đi
            //tổng số bệnh nhân đến khám ( cả chuyển tuyến và không chuyển tuyến)
            List<BNhan> qBNkb = (from bn in data.BenhNhans.Where(p => p.NoiTru == 0).Where(p=>p.NNhap >= tungay && p.NNhap <= denngay) 

                                 join kp in data.KPhongs on bn.MaKP equals kp.MaKP into kqKP from kq2 in kqKP.DefaultIfEmpty()
                                 join rv in data.RaViens on bn.MaBNhan equals rv.MaBNhan into kq from kq1 in kq.DefaultIfEmpty()
                                 select new BNhan { MaCK = kq2 == null ? -1 : kq2.MaCK, MaBNhan = bn.MaBNhan, SThe = bn.SThe, DTNT = bn.DTNT, LyDoC = kq1 ==null ? "" : kq1.LyDoC, MaBVC = kq1 == null? "" : kq1.MaBVC, Status = kq1 == null ? null : kq1.Status }).ToList();//, bn.NoiTru, bn.DTNT 
            //List<int> _lmabnRV = data.RaViens.Where(p => p.NgayRa <= tungay).Select(p => p.MaBNhan).ToList();

            //var qBNdt2 = (from bn in data.BenhNhans.Where(p => p.NoiTru == 1)
            //              join rv in data.RaViens.Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay) on bn.MaBNhan equals rv.MaBNhan
            //              select new { rv.MaCK, bn.NoiTru, bn.MaBNhan, bn.DTNT }).ToList();

            //var qBNdt3 = (from bn in data.BenhNhans.Where(p => p.NoiTru == 1)
            //              join vv in data.VaoViens.Where(p => p.NgayVao <= denngay) on bn.MaBNhan equals vv.MaBNhan
            //              select new { vv.ChuyenKhoa, bn.MaBNhan, vv.NgayVao }).ToList();

            //List<BNhan> qBNdt4 = (from a in qBNdt3.Where(p => p.NgayVao <= tungay) where !(from b in _lmabnRV select b).Contains(a.MaBNhan) select new BNhan { MaCK = mackhoa(a.ChuyenKhoa, lck), MaBNhan = a.MaBNhan }).ToList();
            //List<BNhan> qBNdt5 = (from a in qBNdt3.Where(p => p.NgayVao >= tungay) select new BNhan { MaCK = mackhoa(a.ChuyenKhoa, lck), MaBNhan = a.MaBNhan }).ToList();

            var qBNdt1 = (from bn in data.BenhNhans.Where(p => p.NoiTru == 1)
                          join vv in data.VaoViens on bn.MaBNhan equals vv.MaBNhan
                          join rv in data.RaViens on bn.MaBNhan equals rv.MaBNhan into kq
                          from kq1 in kq.DefaultIfEmpty()
                          where (vv.NgayVao >= tungay && vv.NgayVao <= denngay) ||
                          (kq1 != null && (kq1.NgayRa >= tungay && kq1.NgayRa <= denngay) || (vv.NgayVao < tungay && kq1.NgayRa > denngay))
                          select new { MaCK = kq1 == null ? -1 : kq1.MaCK, MaBNhan = bn.MaBNhan, vv.ChuyenKhoa, bn.SThe, bn.DTNT, kq1 }).ToList();

            List<BNhan> qBNdt = new List<BNhan>();// điều trị nội trú
            foreach (var a in qBNdt1)
            {
                BNhan moi = new BNhan();
                if (a.kq1 != null)
                {
                    moi.MaCK = a.MaCK??-1;
                    moi.MaBVC = a.kq1.MaBVC;
                    moi.LyDoC = a.kq1.LyDoC;
                    moi.Status = a.kq1.Status;
                }
                else
                {
                    moi.MaCK = mackhoa(a.ChuyenKhoa, lck);
                }
                moi.MaBNhan = a.MaBNhan;
                moi.SThe = a.SThe;
                moi.DTNT = a.DTNT;
                qBNdt.Add(moi);
            }

            List<BNhan> qTH = new List<BNhan>();// tổng hợp bệnh nhân
            qTH.AddRange(qBNkb);
            qTH.AddRange(qBNdt);


            foreach (BNhan bn in qTH)
            {
                bn.TenCK = lck.Where(p => p.MaCK == bn.MaCK).Select(p => p.TenCK).FirstOrDefault();
                //if (bn.TenCK == null || bn.TenCK == "")
                //    bn.TenCK = bn.MaCK.ToString();
            }

        

            #region kiểm tra MaBVC không tồn tại trong bảng BV
            List<string> _listMaBVC = qTH.Select(p => p.MaBVC).Distinct().ToList();
            List<string> _listMaBVC2 = (from a in qBNdt join rv in data.RaViens on a.MaBNhan equals rv.MaBNhan select rv.MaBVC).Distinct().ToList();
            _listMaBVC.AddRange(_listMaBVC2);
            _listMaBVC = _listMaBVC.Distinct().ToList();
            List<string> _listBv = data.BenhViens.Select(p => p.MaBV).ToList();

            List<string> qBVOuter = (from a in _listMaBVC
                                     join b in _listBv on a equals b
                                         into kq
                                     from kq1 in kq.DefaultIfEmpty()
                                     select new
                                     {
                                         MaBV = kq1 == null ? a : ""
                                     }).Where(p => p.MaBV != "").Select(p => p.MaBV).ToList();

            string ms = string.Join(",", qBVOuter.ToArray());
            System.Windows.Forms.MessageBox.Show("Mã Bv chuyển: '" + ms + "' chưa tồn tại trong bảng Bệnh Viện");
            #endregion
            _Tuyen = GetTuyen(DungChung.Bien.MaBV, _listBenhVien);
            List<Rp1> list1 = new List<Rp1>();
            #region đổ dữ liệu vào list
            foreach (var a in qTH)
            {
                Rp1 obj = new Rp1();
                obj.MaCK = a.MaCK??0;
                obj.TenCK = a.TenCK;
                obj.SThe = (a.SThe != null && a.SThe.Length == 15) ? 1 : 0;
                obj.Status = a.Status ?? -1;
                if ( a.MaBVC != null && a.MaBVC.Trim() != "")
                {
                    if (a.LyDoC == "Đủ điều kiện chuyển tuyến(đúng tuyến)")
                        obj.LydoChuyen4 = 1;
                    else if (a.LyDoC == "Không đủ điều kiện chuyển tuyến/chuyển tuyến theo yêu cầu người bệnh...(vượt tuyến)")
                        obj.LydoChuyen5 = 1;
                    int tuyen = GetTuyen(a.MaBVC, _listBenhVien);

                    //có thể lấy theo tên object
                    if (tuyen == 1)
                        obj.Tuyen1 = 1;
                    else if (tuyen == 2)
                        obj.Tuyen2 = 1;
                    else if (tuyen == 3)
                        obj.Tuyen3 = 1;
                    else if (tuyen == 4)
                        obj.Tuyen4 = 1;

                    //hình thức chuyển
                    int hinhthucchuyen = getHinhThucChuyen(tuyen, _Tuyen);
                    if (hinhthucchuyen == 0)
                        obj.HTchuyen1a = 1;
                    else if (hinhthucchuyen == 1)
                        obj.HTchuyen1b = 1;
                    else if (hinhthucchuyen == 2)
                        obj.HTchuyen2 = 1;
                    else if (hinhthucchuyen == 3)
                        obj.HTchuyen3 = 1;
                }
                list1.Add(obj);
            }
            #endregion
            #region group
            _list1 = (from a in list1
                      group a by new { a.MaCK, a.TenCK } into kq
                      select new Rp1
                      {
                          MaCK = kq.Key.MaCK,
                          TenCK = kq.Key.TenCK,
                          Ngoaitru = qBNkb.Where(p => p.MaCK == kq.Key.MaCK).Count(),
                          NoiTru = qBNdt.Where(p => p.MaCK == kq.Key.MaCK).Count(),
                          ChuyenDi = kq.Where(p=>p.Status == 1).Count(),
                          TyLe = (float)kq.Where(p => p.Status == 1).Count() * 100 / qTH.Where(p => p.MaCK == kq.Key.MaCK).Count(),
                          SThe = kq.Sum(p => p.SThe),
                          HTchuyen1a = kq.Sum(p => p.HTchuyen1a),
                          HTchuyen1b = kq.Sum(p => p.HTchuyen1b),
                          HTchuyen2 = kq.Sum(p => p.HTchuyen2),
                          HTchuyen3 = kq.Sum(p => p.HTchuyen3),
                          LydoChuyen4 = kq.Sum(p => p.LydoChuyen4),
                          LydoChuyen5 = kq.Sum(p => p.LydoChuyen5),
                          Tuyen1 = kq.Sum(p => p.Tuyen1),
                          Tuyen2 = kq.Sum(p => p.Tuyen2),
                          Tuyen3 = kq.Sum(p => p.Tuyen3),
                          Tuyen4 = kq.Sum(p => p.Tuyen4),
                      }).ToList();
            foreach (var a in _list1)
            {
                a.Ngoaitru = a.Ngoaitru == 0 ? null : a.Ngoaitru;
                a.NoiTru = a.NoiTru == 0 ? null : a.NoiTru;
                a.ChuyenDi = a.ChuyenDi == 0 ? null : a.ChuyenDi;
                a.TyLe = a.TyLe == 0 ? null : a.TyLe;
                a.SThe = a.SThe == 0 ? null : a.SThe;
                a.HTchuyen1a = a.HTchuyen1a == 0 ? null : a.HTchuyen1a;
                a.HTchuyen1b = a.HTchuyen1b == 0 ? null : a.HTchuyen1b;
                a.HTchuyen2 = a.HTchuyen2 == 0 ? null : a.HTchuyen2;

                a.HTchuyen3 = a.HTchuyen3 == 0 ? null : a.HTchuyen3;
                a.LydoChuyen4 = a.LydoChuyen4 == 0 ? null : a.LydoChuyen4;
                a.LydoChuyen5 = a.LydoChuyen5 == 0 ? null : a.LydoChuyen5;
                a.Tuyen1 = a.Tuyen1 == 0 ? null : a.Tuyen1;
                a.Tuyen2 = a.Tuyen2 == 0 ? null : a.Tuyen2;
                a.Tuyen3 = a.Tuyen3 == 0 ? null : a.Tuyen3;
                a.Tuyen4 = a.LydoChuyen5 == 0 ? null : a.Tuyen4;

            }
            #endregion
            #endregion
            #region chuyển tuyến đến
            //danh sách mã bệnh nhân-----------
            List<int> _listBN = new List<int>();
            //            List<int> _listAll = new List<int>();
            List<int> _listBNVao = new List<int>();
            List<int> _listBNRa = new List<int>();
            //--------------------------------------
            //danh sách bệnh nhân trong bản bệnh nhân
            // List<BenhNhan> _listAllBenhNhan = data.BenhNhans.ToList();
            List<BenhNhan> _listAllchuyenDen = data.BenhNhans.Where(p => p.MaBV != null && p.MaBV.Trim() != "").ToList();
            // List<BenhNhan> _listBenhNhan = new List<BenhNhan>();// danh sách tất cả bệnh nhân đã tìm theo ngày
            List<BenhNhan> _listBenhNhanchuyenDen = new List<BenhNhan>();// danh sách bệnh nhân chuyển đến đã tìm theo ngày
            //--------------------------------------
            _listBNVao = data.BenhNhans.Where(p => p.NNhap >= tungay && p.NNhap <= denngay).Select(p => p.MaBNhan).ToList();
            _listBNRa = data.RaViens.Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay).Select(p => p.MaBNhan).ToList();
            // _listBN = _listBNVao;
            _listBN.AddRange(_listBNVao);
            _listBN.AddRange(_listBNRa);
            _listBN = _listBN.Distinct().ToList();

            if (ckNgay == 0)
            {
                _listBenhNhanchuyenDen = _listAllchuyenDen.Where(p => _listBN.Contains(p.MaBNhan)).ToList();
            }
            else if (ckNgay == 1)
            {
                _listBenhNhanchuyenDen = _listAllchuyenDen.Where(p => _listBNVao.Contains(p.MaBNhan)).ToList();
            }
            else if (ckNgay == 2)
            {
                _listBenhNhanchuyenDen = _listAllchuyenDen.Where(p => _listBNRa.Contains(p.MaBNhan)).ToList();
            }
            #endregion
            #region đổ vào list
            List<rp2> list2 = new List<rp2>();
            foreach (var a in _listBenhNhanchuyenDen)
            {
                rp2 moi = new rp2();
                moi.MaCSKCB = a.MaBV;
                int tuyen = GetTuyen(a.MaBV, _listBenhVien);
                int hinhthucchuyen = getHinhThucChuyen(_Tuyen, tuyen);
                if (hinhthucchuyen == 0)
                    moi.HT1aSL = 1;
                else if (hinhthucchuyen == 1)
                    moi.HT1bSL = 1;
                else if (hinhthucchuyen == 2)
                    moi.HT2SL = 1;
                else if (hinhthucchuyen == 3)
                    moi.HT3SL = 1;
                moi.HT4SL = 1;// chưa có căn cứ
                moi.HT5SL = 0;// chưa có căn cứ
                moi.CdPH = 0;//?
                moi.CdKb = 0;//?
                list2.Add(moi);
            }
            #endregion
            #region group
            _list2 = (from a in list2
                      group a by new { a.MaCSKCB } into kq
                      select new rp2
                      {
                          tenCSKCB = getTenBVByMaBV(data, kq.Key.MaCSKCB),
                          TSBN = _listBenhNhanchuyenDen.Where(p => p.MaBV == kq.Key.MaCSKCB).Count(),
                          TSCoThe = _listBenhNhanchuyenDen.Where(p => p.MaBV == kq.Key.MaCSKCB).Where(p => p.SThe != null && p.SThe.Trim() != "").Count(),
                          HT1aSL = kq.Sum(p => p.HT1aSL),
                          HT1bSL = kq.Sum(p => p.HT1bSL),
                          HT2SL = kq.Sum(p => p.HT2SL),
                          HT3SL = kq.Sum(p => p.HT3SL),
                          HT4SL = kq.Sum(p => p.HT4SL),
                          HT5SL = kq.Sum(p => p.HT5SL),
                          CdPH = kq.Sum(p => p.CdPH),
                          CdKb = kq.Sum(p => p.CdKb),

                      }).ToList();
            //foreach(rp2 a in _list2)
            //{
            //    a.HT1aTL = (float)a.HT1aSL * 100 / a.TSBN;
            //    a.HT1bTL = (float)a.HT1bSL * 100 / a.TSBN;
            //    a.HT2TL = (float)a.HT2SL * 100 / a.TSBN;
            //    a.HT3TL = (float)a.HT3SL * 100 / a.TSBN;
            //    a.HT4TL = (float)a.HT4SL * 100 / a.TSBN;
            //    a.HT5TL = (float)a.HT5SL * 100 / a.TSBN;
            //    a.CdPHTL = (float)a.CdPH * 100 / a.TSBN;
            //    a.CdKbTL = (float)a.CdKb * 100 / a.TSBN;
            //}
            foreach (rp2 a in _list2)
            {
                a.TSBN = a.TSBN == 0 ? null : a.TSBN;
                a.TSCoThe = a.TSCoThe == 0 ? null : a.TSCoThe;
                a.HT1aSL = a.HT1aSL == 0 ? null : a.HT1aSL;
                a.HT1bSL = a.HT1bSL == 0 ? null : a.HT1bSL;
                a.HT2SL = a.HT2SL == 0 ? null : a.HT2SL;
                a.HT3SL = a.HT3SL == 0 ? null : a.HT3SL;
                a.HT4SL = a.HT4SL == 0 ? null : a.HT4SL;
                a.HT5SL = a.HT5SL == 0 ? null : a.HT5SL;
                a.CdPH = a.CdPH == 0 ? null : a.CdPH;
                a.CdKb = a.CdKb == 0 ? null : a.CdKb;
            }
            #endregion

        }

        public class BNhan
        {
            public int MaBNhan { set; get; }
            public int? MaCK { set; get; }
            public string TenCK { set; get; }
            public string SThe { set; get; }
            public string MaBVC { set; get; }
            public string LyDoC { set; get; }
            public int? Status { set; get; }
            public bool DTNT { set; get; }

        }
        List<Rp1> _list1 = new List<Rp1>();
        List<rp2> _list2 = new List<rp2>();
        public class Rp1
        {
            public int MaCK { set; get; }
            public string TenCK { set; get; }
            public int? Ngoaitru { set; get; }
            public int? NoiTru { set; get; }
            public int? ChuyenDi { set; get; }
            public double? TyLe { set; get; }
            public int? SThe { set; get; }
            public string MaBVC { set; get; }
            public string LyDoC { set; get; }
            public int Status { set; get; }
            public int? HTchuyen1a { set; get; }
            public int? HTchuyen1b { set; get; }
            public int? HTchuyen2 { set; get; }
            public int? HTchuyen3 { set; get; }
            public int? LydoChuyen4 { set; get; }
            public int? LydoChuyen5 { set; get; }
            public int? Tuyen1 { set; get; }
            public int? Tuyen2 { set; get; }
            public int? Tuyen3 { set; get; }
            public int? Tuyen4 { set; get; }

        }
        public class rp2
        {
            public string MaCSKCB { set; get; }
            public string tenCSKCB { set; get; }
            public int? TSBN { set; get; }
            public int? TSCoThe { set; get; }
            public int? HT1aSL { set; get; }
            public double? HT1aTL { set; get; }
            public int? HT1bSL { set; get; }
            public double? HT1bTL { set; get; }
            public int? HT2SL { set; get; }
            public double? HT2TL { set; get; }
            public int? HT3SL { set; get; }
            public double? HT3TL { set; get; }
            public int? HT4SL { set; get; }
            public double? HT4TL { set; get; }
            public int? HT5SL { set; get; }
            public double? HT5TL { set; get; }
            public int? CdPH { set; get; }
            public double? CdPHTL { set; get; }
            public int? CdKb { set; get; }
            public double? CdKbTL { set; get; }



        }
        private int TuyenByTuyen(string tuyen)
        {
            if (tuyen == null)
                return -1;
            else if (tuyen.Trim() == "A")
                return 1;
            else if (tuyen.Trim() == "B")
                return 2;
            else if (tuyen.Trim() == "C")
                return 3;
            else if (tuyen.Trim() == "D")
                return 4;
            else return -1;
        }
        private int GetTuyen(string maBV, List<BenhVien> lbv)
        {
            int tuyen = -1;
            var hangBV = lbv.Where(p => p.MaBV == maBV).Select(p => p.TuyenBV == null ? "" : p.TuyenBV.Trim()).FirstOrDefault();
            switch (hangBV)
            {
                case "A":
                    tuyen = 1;
                    break;
                case "B":
                    tuyen = 2;
                    break;
                case "C":
                    tuyen = 3;
                    break;
                case "D":
                    tuyen = 4;
                    break;
            }

            return tuyen;
        }
        private int _Tuyen;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="maBVC"></param>
        /// <returns></returns>
        private int getHinhThucChuyen(int tuyenden, int tuyendi)
        {
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

            // var q = data.BenhViens.Where(p => p.MaBV == DungChung.Bien.MaBV).Where(p => p.MaChuQuan == maBVC).ToList();
            if (tuyenden + 1 == tuyendi)
            {
                return 0;// hình thức 1a ( từ tuyến dưới chuyển lên tuyến trên liền kề)
            }
            else
            {
                if (tuyenden <= 0)
                    return -1;
                else if (tuyenden < tuyendi)
                {
                    return 1;// hình thức 1b (từ tuyến dưới chuyển lên tuyến trên không liền kề                    
                }
                else if (tuyenden > tuyendi)
                {
                    return 2;// hình thức 2: chuyển người bệnh từ tuyến trên về tuyến dưới
                }
                else
                    return 3;// chuyển người bệnh giữa các cơ sở cùng tuyến
            }
        }

        #region bỏ
        private int getHinhThucChuyen(string maBVC)
        {
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            int tuyenden = GetTuyen(maBVC, _listBenhVien);

            //  var q = data.BenhViens.Where(p => p.MaBV == DungChung.Bien.MaBV).Where(p => p.MaChuQuan == maBVC).ToList();
            if (tuyenden + 1 == _Tuyen)
            {
                return 0;// hình thức 1a ( từ tuyến dưới chuyển lên tuyến trên liền kề)
            }
            else
            {
                if (tuyenden <= 0)
                    return -1;
                else if (tuyenden < _Tuyen)
                {
                    return 1;// hình thức 1b (tuef tuyế dưới chuyển lên tuyến trên không liền kề                    
                }
                else if (tuyenden > _Tuyen)
                {
                    return 2;// hình thức 2: chuyển người bệnh từ tuyến trên về tuyến dưới
                }
                else
                    return 3;// chuyển người bệnh giữa các cơ sở cùng tuyến
            }
        }
        #endregion
        private void xrSubreport1_BeforePrint(object sender, CancelEventArgs e)
        {

            rep_congtacchuyentuyen_subChuyenDi repSub = (rep_congtacchuyentuyen_subChuyenDi)xrSubreport1.ReportSource;
            repSub.DataSource = _list1;
            repSub.dataBinding();
        }

        private void PageHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            lbl_TenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            lab_TenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
        }

        private void xrSubreport2_BeforePrint(object sender, CancelEventArgs e)
        {
            rep_Congtacchuyentuyenden_sub repSub = (rep_Congtacchuyentuyenden_sub)xrSubreport2.ReportSource;
            repSub.DataSource = _list2;
            repSub.dataBinding();
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            celNguoiLap.Text = DungChung.Bien.NguoiLapBieu;
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            lbl_TenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            lab_TenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            lab_thoigian.Text = "Từ ngày " + _tungay.Day + " tháng " + _tungay.Month + " năm " + _tungay.Year + " đến ngày " + _denngay.Day + " tháng " + _denngay.Month + " năm " + _denngay.Year;
            lbl_ngaythang.Text = "......., ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;
        }

        private string getTenBVByMaBV(QLBV_Database.QLBVEntities data, string maBV)
        {
            string tenbv = "";
            tenbv = data.BenhViens.Where(p => p.MaBV == maBV).Select(p => p.TenBV).FirstOrDefault();
            return tenbv;
        }

    }
}
