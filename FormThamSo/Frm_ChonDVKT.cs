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
    public partial class Frm_ChonDVKT : DevExpress.XtraEditors.XtraForm
    {
        int _MaBN = 0;
        int _MaKP = 0;
        string _TenKP = "";

        public Frm_ChonDVKT()
        {
            InitializeComponent();
        }

        public Frm_ChonDVKT(int _m, string tenKP, object maKP)
        {
            InitializeComponent();
            _MaBN = _m;
            _TenKP = tenKP;
            _MaKP = Convert.ToInt32(maKP);
        }

        private class DV
        {
            public string TenDV { get; set; }
            public int MaDV { get; set; }
            public int? IDNhom { get; set; }
            public bool Chon { get; set; }
            public string SapXep { get; set; }
            public string TenNhom { get; set; }
        }
        private class KPhong
        {
            public string TenKP { get; set; }
            public int MaKP { get; set; }
            public bool Chon { get; set; }
        }

        List<DV> _DichVu = new List<DV>();
        List<KPhong> _Kphong = new List<KPhong>();
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            hamin();
        }

        private void hamin()
        {
            QLBV_Database.QLBVEntities _db = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var cls = (from cl in _db.CLS
                       where (cl.MaBNhan == _MaBN)
                       join cd in _db.ChiDinhs on cl.IdCLS equals cd.IDCD
                       join dv in _db.DichVus on cd.MaDV equals dv.MaDV
                       join kp in _Kphong.Where(p => p.Chon == true).Where(p => p.MaKP != 0) on cl.MaKP equals kp.MaKP
                       where (dv.IDNhom == 1 || dv.IDNhom == 2 || dv.IDNhom == 3 || dv.IDNhom == 9)
                       select new { dv.MaDV, dv.TenDV, dv.IDNhom }).ToList();
            if (cls.Count > 0)
            {
                foreach (var a in cls)
                {
                    DV themmoi = new DV();
                    themmoi.TenDV = a.TenDV;
                    themmoi.MaDV = a.MaDV;
                    themmoi.IDNhom = a.IDNhom;
                    themmoi.Chon = false;
                    _DichVu.Add(themmoi);
                }
            }
            var thuoc = (from dt in _db.DThuocs // thuoc va vtyt
                         join dtct in _db.DThuoccts on dt.IDDon equals dtct.IDDon
                         join dv in _db.DichVus on dtct.MaDV equals dv.MaDV
                         join kp in _Kphong.Where(p => p.Chon == true).Where(p => p.MaKP != 0) on dt.MaKP equals kp.MaKP
                         where (dt.MaBNhan == _MaBN)
                         where (dv.IDNhom == 4 || dv.IDNhom == 5 || dv.IDNhom == 6 || dv.IDNhom == 10 || dv.IDNhom == 11)
                         select new { dv.MaDV, dv.TenDV, dv.IDNhom }).ToList();
            if (thuoc.Count > 0)
            {
                foreach (var a in thuoc)
                {
                    DV themmoi = new DV();
                    themmoi.TenDV = a.TenDV;
                    themmoi.MaDV = a.MaDV;
                    themmoi.IDNhom = a.IDNhom;
                    themmoi.Chon = false;
                    _DichVu.Add(themmoi);
                }
            }


            Dictionary<string, object> _dic = new Dictionary<string, object>();
            _dic.Add("TenCQCQ", DungChung.Bien.TenCQCQ.ToUpper());
            _dic.Add("TenCQ", DungChung.Bien.TenCQCQ.ToUpper());
            List<DV> _list = new List<DV>();
            List<DV> _Report = new List<DV>();
            _list = _DichVu.Where(p => p.Chon == true).ToList();


            var khoa = (from a in _db.BNKBs.Where(p => p.MaBNhan == _MaBN)
                        join b in _db.KPhongs on a.MaKP equals b.MaKP
                        select new { a, b }).ToList();

            if (khoa.Count > 0)
            {
                _dic.Add("Buong", string.Join("; ", khoa.Where(p => !string.IsNullOrEmpty(p.a.Buong)).Select(p => p.a.Buong).Distinct()));
                _dic.Add("Giuong", string.Join("; ", khoa.Where(p => !string.IsNullOrEmpty(p.a.Giuong)).Select(p => p.a.Giuong).Distinct()));
                _dic.Add("CD", string.Join("; ", DungChung.Ham.FreshString(string.Join("; ", khoa.Select(p => DungChung.Ham.FreshString(p.a.ChanDoan))) + "; " + string.Join("; ", khoa.Select(p => DungChung.Ham.FreshString(p.a.BenhKhac))))));

            }
            var vv = (from a in _db.BenhNhans.Where(p => p.MaBNhan == _MaBN)
                      join b in _db.VaoViens on a.MaBNhan equals b.MaBNhan
                      select new { a.MaBNhan, a.TenBNhan, a.NgaySinh, a.GTinh, b.NgayVao, a.ThangSinh, a.NamSinh, a.MaKP }).ToList();
            if (vv.Count > 0)
            {
                _dic.Add("TenBNhan", vv.First().TenBNhan.ToUpper());
                _dic.Add("NgSinh", vv.First().NgaySinh + "/" + vv.First().ThangSinh + "/" + vv.First().NamSinh);
                _dic.Add("NgVao", DungChung.Ham.NgaySangChu(Convert.ToDateTime(vv.First().NgayVao), 7));
                _dic.Add("GioiTinh", vv.First().GTinh == 1 ? "Nam" : "Nữ");
                _dic.Add("Khoa", _TenKP);
                DateTime day = Convert.ToDateTime(vv.First().NgayVao);
                for (int j = 0; j < 25; j++)// ds 25 cot ngay
                {
                    int v = j + 1;
                    DateTime day1 = day.AddDays(j);
                    _dic.Add("NgayVV_" + v, day1.Day + " / " + day1.Month);
                }

                var _ListXetNghiem = _list.Where(p => p.IDNhom == 1).ToList();
                string tenNhomXN = "Xét nghiệm";
                for (int i = 0; i < 12; i++) // ds 12 hang xet nghiem
                {
                    DV dv = new DV();
                    int v = i + 1;
                    if (_ListXetNghiem.Count > i)
                    {
                        _dic.Add("XetNghiem_" + v , _ListXetNghiem[i].TenDV);
                        dv.TenDV = _ListXetNghiem[i].TenDV;
                    }
                    else
                    {
                        _dic.Add("XetNghiem_" + v, "");
                        dv.TenDV = "";
                    }

                    dv.SapXep = "I";
                    dv.TenNhom = tenNhomXN;
                    _Report.Add(dv);
                }


                var _ListCDHA = _list.Where(p => p.IDNhom == 2).ToList();
                string tenNhomCDHA = "Chẩn đoán hình ảnh";
                for (int i = 0; i < 4; i++) // ds 4 hàng Chẩn đoán hình ảnh
                {
                    DV dv = new DV();
                    int v = i + 1;
                    if (_ListCDHA.Count > i)
                    {
                        _dic.Add("CDHA_" + v, _ListCDHA[i].TenDV);
                        dv.TenDV = _ListCDHA[i].TenDV;
                    }
                    else
                    {
                        _dic.Add("CDHA_" + v, "");
                        dv.TenDV = "";
                    }

                    dv.SapXep = "II";
                    dv.TenNhom = tenNhomCDHA;
                    _Report.Add(dv);
                }


                var _ListTDCN = _list.Where(p => p.IDNhom == 3).ToList();
                string tenNhomTDCN = "Thăm dò chức năng";
                for (int i = 0; i < 2; i++) // ds 2 hàng thăm dò chức năng
                {
                    DV dv = new DV();
                    int v = i + 1;
                    if (_ListTDCN.Count > i)
                    {
                        _dic.Add("TDCN_" + v, _ListTDCN[i].TenDV); 
                        dv.TenDV = _ListTDCN[i].TenDV;
                    }
                    else
                    {
                        _dic.Add("TDCN_" + v, "");
                        dv.TenDV = "";
                    }

                    dv.SapXep = "III";
                    dv.TenNhom = tenNhomTDCN;
                    _Report.Add(dv);
                }

                var _ListDVKT = _list.Where(p => p.IDNhom == 9).ToList();
                string tenNhomDVKT = "Dịch vị kỹ thuật";
                for (int i = 0; i < 10; i++) // ds 10 hàng DV kỹ thuật
                {
                    DV dv = new DV();
                    int v = i + 1;
                    if (_ListDVKT.Count > i)
                    {
                        _dic.Add("DVKT_" + v, _ListDVKT[i].TenDV);
                        dv.TenDV = _ListDVKT[i].TenDV;
                    }
                    else
                    {
                        _dic.Add("DVKT_" + v, "");
                        dv.TenDV = "";
                    }
                    dv.SapXep = "IV";
                    dv.TenNhom = tenNhomDVKT;
                    _Report.Add(dv);
                }

                var _ListThuoc = _list.Where(p => p.IDNhom == 4 || p.IDNhom == 5 || p.IDNhom == 6).ToList();
                string tenNhomThuoc = "Thuốc, dịch truyền(tên, nồng độ, hàm lượng)";
                for (int i = 0; i < 10; i++) // ds 10 hàng thuốc, dịch truyền
                {
                    DV dv = new DV();
                    int v = i + 1;
                    if (_ListThuoc.Count > i)
                    {
                        _dic.Add("Thuoc_" + v, _ListThuoc[i].TenDV);
                        dv.TenDV = _ListThuoc[i].TenDV;
                    }
                    else
                    {
                        _dic.Add("Thuoc_" + v, "");
                        dv.TenDV = "";
                    }

                    dv.SapXep = "V";
                    dv.TenNhom = tenNhomThuoc;
                    _Report.Add(dv);
                }

                var _ListVTYT = _list.Where(p => p.IDNhom == 10 || p.IDNhom == 11).ToList();
                string tenNhomVTYT = "VTYT (không có trong DVKT)";
                for (int i = 0; i < 4; i++) // ds 4 hàng vtyt
                {
                    DV dv = new DV();
                    int v = i + 1;
                    if (_ListVTYT.Count > i)
                    {
                        _dic.Add("VTYT_" + v, _ListVTYT[i].TenDV);
                        dv.TenDV = _ListVTYT[i].TenDV;
                    }
                    else
                    {
                        _dic.Add("VTYT_" + v, "");
                        dv.TenDV = "";
                    }

                    dv.SapXep = "VI";
                    dv.TenNhom = tenNhomVTYT;
                    _Report.Add(dv);
                }
            }

            //Binding List _Report   nhưng groupheaer của ReportDesigner ko nhận nên phải import từng hàng cái này phát triển sau... Rep_CongKhaiDVKT_24012_Update.repx
            DungChung.Ham.Print(DungChung.PrintConfig.Rep_CongKhaiDVKT_24012, null, _dic, false);
        }

        private void sbtHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Frm_ChonDVKT_Load(object sender, EventArgs e)
        {
            #region MyRegion
            //QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            //var cls = (from cl in data.CLS
            //           where (cl.MaKP == _MaKP)
            //           where (cl.MaBNhan == _MaBN)
            //           join cd in data.ChiDinhs on cl.IdCLS equals cd.IDCD
            //           join dv in data.DichVus on cd.MaDV equals dv.MaDV
            //           where (dv.IDNhom == 1 || dv.IDNhom == 2 || dv.IDNhom == 3 || dv.IDNhom == 9)
            //           select new { dv.MaDV, dv.TenDV, dv.IDNhom }).ToList();
            //if (cls.Count > 0)
            //{
            //    foreach (var a in cls)
            //    {
            //        DV themmoi = new DV();
            //        themmoi.TenDV = a.TenDV;
            //        themmoi.MaDV = a.MaDV;
            //        themmoi.IDNhom = a.IDNhom;
            //        themmoi.Chon = false;
            //        _DichVu.Add(themmoi);
            //    }
            //}
            //var thuoc = (from dt in data.DThuocs // thuoc va vtyt
            //             join dtct in data.DThuoccts on dt.IDDon equals dtct.IDDon
            //             join dv in data.DichVus on dtct.MaDV equals dv.MaDV
            //             where (dt.MaKP == _MaKP)
            //             where (dt.MaBNhan == _MaBN)
            //             where (dv.IDNhom == 4 || dv.IDNhom == 5 || dv.IDNhom == 6 || dv.IDNhom == 10 || dv.IDNhom == 11)
            //             select new { dv.MaDV, dv.TenDV, dv.IDNhom }).ToList();
            //if (thuoc.Count > 0)
            //{
            //    foreach (var a in thuoc)
            //    {
            //        DV themmoi = new DV();
            //        themmoi.TenDV = a.TenDV;
            //        themmoi.MaDV = a.MaDV;
            //        themmoi.IDNhom = a.IDNhom;
            //        themmoi.Chon = false;
            //        _DichVu.Add(themmoi);
            //    }
            //}

            //grcDV.DataSource = _DichVu.ToList();
            //if (_DichVu.Count > 0)
            //{
            //    sbtTBC.Enabled = true;
            //}
            #endregion
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

            var kphong = (from dt in data.DThuocs.Where(p => p.MaBNhan == _MaBN)
                          join dtct in data.DThuoccts on dt.IDDon equals dtct.IDDon
                          join kp in data.KPhongs on dtct.MaKP equals kp.MaKP
                          //where (kp.PLoai == "Lâm sàng" || kp.PLoai == "Phòng khám")
                          group new { kp } by new { kp.MaKP, kp.TenKP } into kq
                          select new { TenKP = kq.Key.TenKP, MaKP = kq.Key.MaKP }).ToList();
            if (kphong.Count > 0)
            {
                KPhong themmoi1 = new KPhong();
                themmoi1.TenKP = "Chọn tất cả";
                themmoi1.MaKP = 0;
                themmoi1.Chon = false;
                _Kphong.Add(themmoi1);
                foreach (var a in kphong)
                {
                    KPhong themmoi = new KPhong();
                    themmoi.TenKP = a.TenKP;
                    themmoi.MaKP = a.MaKP;
                    themmoi.Chon = false;
                    _Kphong.Add(themmoi);
                }
                grcDV.DataSource = _Kphong.ToList();
            }

        }

        private void grvDV_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "Chọn")
            {
                if (grvDV.GetFocusedRowCellValue("TenKP") != null)
                {
                    string Ten = grvDV.GetFocusedRowCellValue("TenKP").ToString();
                    if (Ten == "Chọn tất cả")
                    {
                        if (_Kphong.First().Chon == true)
                        {
                            foreach (var a in _Kphong)
                            {
                                a.Chon = false;
                            }
                        }
                        else
                        {
                            foreach (var a in _Kphong)
                            {
                                a.Chon = true;
                            }
                        }
                        grcDV.DataSource = "";
                        grcDV.DataSource = _Kphong.ToList();
                    }
                }
            }
        }
    }
}