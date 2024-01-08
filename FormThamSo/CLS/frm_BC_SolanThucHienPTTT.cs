using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace QLBV.FormThamSo
{
    public partial class frm_BC_SolanThucHienPTTT : DevExpress.XtraEditors.XtraForm
    {

        #region Kho
        private class Kho
        {
            public bool Check { get; set; }
            public int MaKP { get; set; }
            public string TenKP { get; set; }
        }
        #endregion
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        List<Kho> _lKho = new List<Kho>(100);
        List<DSKP> _DSKP;
        public frm_BC_SolanThucHienPTTT()
        {
            InitializeComponent();

        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public class DSKP
        {
            private string TenKP;
            private int MaKP;
            public string tenkp
            { set { TenKP = value; } get { return TenKP; } }
            public int makp
            { set { MaKP = value; } get { return MaKP; } }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            DateTime tungay = DungChung.Ham.NgayTu(date_tungay.DateTime);
            DateTime denngay = DungChung.Ham.NgayDen(date_ngayden.DateTime);

            for (int i = 0; i < grvkhoaphong.RowCount; i++)
            {
                bool check = false;
                int id = Convert.ToInt16(grvkhoaphong.GetRowCellValue(i, makp));
                if (grvkhoaphong.GetRowCellValue(i, "Check") != null && grvkhoaphong.GetRowCellValue(i, "Check").ToString().ToLower() == "true" && id >= 0)
                {

                    check = true;

                }
                else
                {
                    check = false;
                }
                foreach (var item in _lKho)
                {
                    if (item.MaKP == id)
                    {
                        item.Check = check;
                        break;
                    }
                }
            }
            _DSKP = new List<DSKP>(100);
            for (int i = 0; i < 100; i++)
            {
                _DSKP.Add(new DSKP { makp = -1, tenkp = "" });
            }
            int j = 0;
         

            var dvu = (from dv in data.DichVus
                       join tnhom in data.TieuNhomDVs.Where(p => p.TenRG.ToLower().Contains("phẫu thuật") || p.TenRG.ToLower().Contains("thủ thuật")) on dv.IdTieuNhom equals tnhom.IdTieuNhom
                       select new { dv.MaDV, dv.TenDV, tnhom.TenRG }).ToList();

            var cls = (from a in data.DThuocs
                       join b in data.DThuoccts.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay) on a.IDDon equals b.IDDon
                       join c in data.KPhongs on b.MaKP equals c.MaKP
                      select new
                      {
                          MaBNhan = a.MaBNhan??0,
                          MaKP = b.MaKP??0,
                          b.MaDV,
                          c.PLoai,
                          b.SoLuong
                      }).ToList();

            var dv1 = (from a in cls
                      join b in dvu on a.MaDV equals b.MaDV
                      select new {
                      a.MaBNhan,
                      a.MaKP,
                      b.TenDV,
                      a.PLoai,
                      a.SoLuong
                      }).ToList();
            _lKho = _lKho.Where(p => p.MaKP > 0).OrderByDescending(p => p.Check).ToList();
            foreach (var item in _lKho)
            {
                if (item.Check && item.MaKP >= 0)
                {
                    _DSKP[j].tenkp = item.TenKP;
                    _DSKP[j].makp = item.MaKP;
                    j++;
                }
            }
            for (int i = 0; i < 50; i++) // thêm vào list Kho các kp null
            {
                Kho themmoi = new Kho();
                themmoi.TenKP = "";
                themmoi.MaKP = i + 6969;
                themmoi.Check = false;
                _lKho.Add(themmoi);
            }
            List<int> lplxuat = _lKho.Where(p => p.Check == true).Select(p => p.MaKP).ToList();
            List<NoiDung> solan0 = new List<NoiDung>();
            foreach (var item in lplxuat)
            {
                var nd = (from a in dv1.Where(p => p.MaKP == item)
                            group new { a } by new { a.TenDV, a.MaKP } into kq
                            select new NoiDung
                            {
                                TenDV = kq.Key.TenDV,
                                //MaKP = kq.Key.MaKP,
                                nd1 = kq.Where(p => p.a.MaKP == _lKho.Skip(0).Take(1).First().MaKP).Sum(p => p.a.SoLuong),
                                nd2 = kq.Where(p => p.a.MaKP == _lKho.Skip(1).Take(1).First().MaKP).Sum(p => p.a.SoLuong),
                                nd3 = kq.Where(p => p.a.MaKP == _lKho.Skip(2).Take(1).First().MaKP).Sum(p => p.a.SoLuong),
                                nd4 = kq.Where(p => p.a.MaKP == _lKho.Skip(3).Take(1).First().MaKP).Sum(p => p.a.SoLuong),
                                nd5 = kq.Where(p => p.a.MaKP == _lKho.Skip(4).Take(1).First().MaKP).Sum(p => p.a.SoLuong),
                                nd6 = kq.Where(p => p.a.MaKP == _lKho.Skip(5).Take(1).First().MaKP).Sum(p => p.a.SoLuong),
                                nd7 = kq.Where(p => p.a.MaKP == _lKho.Skip(6).Take(1).First().MaKP).Sum(p => p.a.SoLuong),
                                nd8 = kq.Where(p => p.a.MaKP == _lKho.Skip(7).Take(1).First().MaKP).Sum(p => p.a.SoLuong),
                                nd9 = kq.Where(p => p.a.MaKP == _lKho.Skip(8).Take(1).First().MaKP).Sum(p => p.a.SoLuong),
                                nd10 = kq.Where(p => p.a.MaKP == _lKho.Skip(9).Take(1).First().MaKP).Sum(p => p.a.SoLuong),
                                nd11 = kq.Where(p => p.a.MaKP == _lKho.Skip(10).Take(1).First().MaKP).Sum(p => p.a.SoLuong),
                                nd12 = kq.Where(p => p.a.MaKP == _lKho.Skip(11).Take(1).First().MaKP).Sum(p => p.a.SoLuong),
                                nd13 = kq.Where(p => p.a.MaKP == _lKho.Skip(12).Take(1).First().MaKP).Sum(p => p.a.SoLuong),
                                nd14 = kq.Where(p => p.a.MaKP == _lKho.Skip(13).Take(1).First().MaKP).Sum(p => p.a.SoLuong),
                                nd15 = kq.Where(p => p.a.MaKP == _lKho.Skip(14).Take(1).First().MaKP).Sum(p => p.a.SoLuong),
                                nd16 = kq.Where(p => p.a.MaKP == _lKho.Skip(15).Take(1).First().MaKP).Sum(p => p.a.SoLuong),
                                nd17 = kq.Where(p => p.a.MaKP == _lKho.Skip(16).Take(1).First().MaKP).Sum(p => p.a.SoLuong),
                                nd18 = kq.Where(p => p.a.MaKP == _lKho.Skip(17).Take(1).First().MaKP).Sum(p => p.a.SoLuong),
                                nd19 = kq.Where(p => p.a.MaKP == _lKho.Skip(18).Take(1).First().MaKP).Sum(p => p.a.SoLuong),
                                nd20 = kq.Where(p => p.a.MaKP == _lKho.Skip(19).Take(1).First().MaKP).Sum(p => p.a.SoLuong),
                                nd21 = kq.Where(p => p.a.MaKP == _lKho.Skip(20).Take(1).First().MaKP).Sum(p => p.a.SoLuong),
                                nd22 = kq.Where(p => p.a.MaKP == _lKho.Skip(21).Take(1).First().MaKP).Sum(p => p.a.SoLuong),
                                nd23 = kq.Where(p => p.a.MaKP == _lKho.Skip(22).Take(1).First().MaKP).Sum(p => p.a.SoLuong),
                                nd24 = kq.Where(p => p.a.MaKP == _lKho.Skip(23).Take(1).First().MaKP).Sum(p => p.a.SoLuong),
                                nd25 = kq.Where(p => p.a.MaKP == _lKho.Skip(24).Take(1).First().MaKP).Sum(p => p.a.SoLuong),
                                nd26 = kq.Where(p => p.a.MaKP == _lKho.Skip(25).Take(1).First().MaKP).Sum(p => p.a.SoLuong),
                                nd27 = kq.Where(p => p.a.MaKP == _lKho.Skip(26).Take(1).First().MaKP).Sum(p => p.a.SoLuong),
                                nd28 = kq.Where(p => p.a.MaKP == _lKho.Skip(27).Take(1).First().MaKP).Sum(p => p.a.SoLuong),
                                nd29 = kq.Where(p => p.a.MaKP == _lKho.Skip(28).Take(1).First().MaKP).Sum(p => p.a.SoLuong),
                                nd30 = kq.Where(p => p.a.MaKP == _lKho.Skip(29).Take(1).First().MaKP).Sum(p => p.a.SoLuong),
                                nd31 = kq.Where(p => p.a.MaKP == _lKho.Skip(30).Take(1).First().MaKP).Sum(p => p.a.SoLuong),
                                nd32 = kq.Where(p => p.a.MaKP == _lKho.Skip(31).Take(1).First().MaKP).Sum(p => p.a.SoLuong),
                                nd33 = kq.Where(p => p.a.MaKP == _lKho.Skip(32).Take(1).First().MaKP).Sum(p => p.a.SoLuong),
                                nd34 = kq.Where(p => p.a.MaKP == _lKho.Skip(33).Take(1).First().MaKP).Sum(p => p.a.SoLuong),
                                nd35 = kq.Where(p => p.a.MaKP == _lKho.Skip(34).Take(1).First().MaKP).Sum(p => p.a.SoLuong),
                                nd36 = kq.Where(p => p.a.MaKP == _lKho.Skip(35).Take(1).First().MaKP).Sum(p => p.a.SoLuong),
                                nd37 = kq.Where(p => p.a.MaKP == _lKho.Skip(36).Take(1).First().MaKP).Sum(p => p.a.SoLuong),
                                nd38 = kq.Where(p => p.a.MaKP == _lKho.Skip(37).Take(1).First().MaKP).Sum(p => p.a.SoLuong),
                                nd39 = kq.Where(p => p.a.MaKP == _lKho.Skip(38).Take(1).First().MaKP).Sum(p => p.a.SoLuong),
                                nd40 = kq.Where(p => p.a.MaKP == _lKho.Skip(39).Take(1).First().MaKP).Sum(p => p.a.SoLuong),
                                tc = 0.0
                            }).OrderBy(p => p.TenDV).ToList();
                foreach (var i in nd)
                {
                    NoiDung list = new NoiDung();
                    list.TenDV = i.TenDV;
                    //list.MaKP = i.MaKP;
                    list.nd1 = i.nd1;
                    list.nd2 = i.nd2;
                    list.nd3 = i.nd3;
                    list.nd4 = i.nd4;
                    list.nd5 = i.nd5;
                    list.nd6 = i.nd6;
                    list.nd7 = i.nd7;
                    list.nd8 = i.nd8;
                    list.nd9 = i.nd9;
                    list.nd10 = i.nd10;
                    list.nd11 = i.nd11;
                    list.nd12 = i.nd12;
                    list.nd13 = i.nd13;
                    list.nd14 = i.nd14;
                    list.nd15 = i.nd15;
                    list.nd16 = i.nd16;
                    list.nd17 = i.nd17;
                    list.nd18 = i.nd18;
                    list.nd19 = i.nd19;
                    list.nd20 = i.nd20;
                    list.nd21 = i.nd21;
                    list.nd22 = i.nd22;
                    list.nd23 = i.nd23;
                    list.nd24 = i.nd24;
                    list.nd25 = i.nd25;
                    list.nd26 = i.nd26;
                    list.nd27 = i.nd27;
                    list.nd28 = i.nd28;
                    list.nd29 = i.nd29;
                    list.nd30 = i.nd30;
                    list.nd31 = i.nd31;
                    list.nd32 = i.nd32;
                    list.nd33 = i.nd33;
                    list.nd34 = i.nd34;
                    list.nd35 = i.nd35;
                    list.nd36 = i.nd36;
                    list.nd37 = i.nd27;
                    list.nd38 = i.nd28;
                    list.nd39 = i.nd29;
                    list.nd40 = i.nd30;
                    list.tc = i.tc;
                    solan0.Add(list);
                }

            }
            var solan1 = (from a in dv1
                            group new { a } by new
                            {
                                a.TenDV,
                                a.MaKP
                            } into kq
                            select new NoiDung
                            {
                                TenDV = kq.Key.TenDV,
                                //MaKP = kq.Key.MaKP,
                                nd1 = 0.0,
                                nd2 = 0.0,
                                nd3 = 0.0,
                                nd4 = 0.0,
                                nd5 = 0.0,
                                nd6 = 0.0,
                                nd7 = 0.0,
                                nd8 = 0.0,
                                nd9 = 0.0,
                                nd10 = 0.0,
                                nd11 = 0.0,
                                nd12 = 0.0,
                                nd13 = 0.0,
                                nd14 = 0.0,
                                nd15 = 0.0,
                                nd16 = 0.0,
                                nd17 = 0.0,
                                nd18 = 0.0,
                                nd19 = 0.0,
                                nd20 = 0.0,
                                nd21 = 0.0,
                                nd22 = 0.0,
                                nd23 = 0.0,
                                nd24 = 0.0,
                                nd25 = 0.0,
                                nd26 = 0.0,
                                nd27 = 0.0,
                                nd28 = 0.0,
                                nd29 = 0.0,
                                nd30 = 0.0,
                                nd31 = 0.0,
                                nd32 = 0.0,
                                nd33 = 0.0,
                                nd34 = 0.0,
                                nd35 = 0.0,
                                nd36 = 0.0,
                                nd37 = 0.0,
                                nd38 = 0.0,
                                nd39 = 0.0,
                                nd40 = 0.0,
                                tc = kq.Where(p => p.a.PLoai.Contains("Lâm sàng") || p.a.PLoai.Contains("Phòng khám")).Sum(p => p.a.SoLuong)
                            }).OrderBy(p => p.TenDV).ToList();
            List<NoiDung> list1 = new List<NoiDung>();
            foreach (var i in solan1)
            {
                NoiDung list = new NoiDung();
                list.TenDV = i.TenDV;
                //list.MaKP = i.MaKP;
                list.nd1 = i.nd1;
                list.nd2 = i.nd2;
                list.nd3 = i.nd3;
                list.nd4 = i.nd4;
                list.nd5 = i.nd5;
                list.nd6 = i.nd6;
                list.nd7 = i.nd7;
                list.nd8 = i.nd8;
                list.nd9 = i.nd9;
                list.nd10 = i.nd10;
                list.nd11 = i.nd11;
                list.nd12 = i.nd12;
                list.nd13 = i.nd13;
                list.nd14 = i.nd14;
                list.nd15 = i.nd15;
                list.nd16 = i.nd16;
                list.nd17 = i.nd17;
                list.nd18 = i.nd18;
                list.nd19 = i.nd19;
                list.nd20 = i.nd20;
                list.nd21 = i.nd21;
                list.nd22 = i.nd22;
                list.nd23 = i.nd23;
                list.nd24 = i.nd24;
                list.nd25 = i.nd25;
                list.nd26 = i.nd26;
                list.nd27 = i.nd27;
                list.nd28 = i.nd28;
                list.nd29 = i.nd29;
                list.nd30 = i.nd30;
                list.nd31 = i.nd31;
                list.nd32 = i.nd32;
                list.nd33 = i.nd33;
                list.nd34 = i.nd34;
                list.nd35 = i.nd35;
                list.nd36 = i.nd36;
                list.nd37 = i.nd27;
                list.nd38 = i.nd28;
                list.nd39 = i.nd29;
                list.nd40 = i.nd30;
                list.tc = i.tc;
                list1.Add(list);
            }
            var solan = solan0.Concat(list1).ToList();
            var solan2 = (from a in solan
                            group a by new { a.TenDV } into kq
                            select new
                            {
                                kq.Key.TenDV,
                                nd1 = (kq.Sum(p => p.nd1) > 0) ? kq.Sum(p => p.nd1).ToString() : "",
                                nd2 = (kq.Sum(p => p.nd2) > 0) ? kq.Sum(p => p.nd2).ToString() : "",
                                nd3 = (kq.Sum(p => p.nd3) > 0) ? kq.Sum(p => p.nd3).ToString() : "",
                                nd4 = (kq.Sum(p => p.nd4) > 0) ? kq.Sum(p => p.nd4).ToString() : "",
                                nd5 = (kq.Sum(p => p.nd5) > 0) ? kq.Sum(p => p.nd5).ToString() : "",
                                nd6 = (kq.Sum(p => p.nd6) > 0) ? kq.Sum(p => p.nd6).ToString() : "",
                                nd7 = (kq.Sum(p => p.nd7) > 0) ? kq.Sum(p => p.nd7).ToString() : "",
                                nd8 = (kq.Sum(p => p.nd8) > 0) ? kq.Sum(p => p.nd8).ToString() : "",
                                nd9 = (kq.Sum(p => p.nd9) > 0) ? kq.Sum(p => p.nd9).ToString() : "",
                                nd10 = (kq.Sum(p => p.nd10) > 0) ? kq.Sum(p => p.nd10).ToString() : "",
                                nd11 = (kq.Sum(p => p.nd11) > 0) ? kq.Sum(p => p.nd11).ToString() : "",
                                nd12 = (kq.Sum(p => p.nd12) > 0) ? kq.Sum(p => p.nd12).ToString() : "",
                                nd13 = (kq.Sum(p => p.nd13) > 0) ? kq.Sum(p => p.nd13).ToString() : "",
                                nd14 = (kq.Sum(p => p.nd14) > 0) ? kq.Sum(p => p.nd14).ToString() : "",
                                nd15 = (kq.Sum(p => p.nd15) > 0) ? kq.Sum(p => p.nd15).ToString() : "",
                                nd16 = (kq.Sum(p => p.nd16) > 0) ? kq.Sum(p => p.nd16).ToString() : "",
                                nd17 = (kq.Sum(p => p.nd17) > 0) ? kq.Sum(p => p.nd17).ToString() : "",
                                nd18 = (kq.Sum(p => p.nd18) > 0) ? kq.Sum(p => p.nd18).ToString() : "",
                                nd19 = (kq.Sum(p => p.nd19) > 0) ? kq.Sum(p => p.nd19).ToString() : "",
                                nd20 = (kq.Sum(p => p.nd20) > 0) ? kq.Sum(p => p.nd20).ToString() : "",
                                nd21 = (kq.Sum(p => p.nd21) > 0) ? kq.Sum(p => p.nd21).ToString() : "",
                                nd22 = (kq.Sum(p => p.nd22) > 0) ? kq.Sum(p => p.nd22).ToString() : "",
                                nd23 = (kq.Sum(p => p.nd23) > 0) ? kq.Sum(p => p.nd23).ToString() : "",
                                nd24 = (kq.Sum(p => p.nd24) > 0) ? kq.Sum(p => p.nd24).ToString() : "",
                                nd25 = (kq.Sum(p => p.nd25) > 0) ? kq.Sum(p => p.nd25).ToString() : "",
                                nd26 = (kq.Sum(p => p.nd26) > 0) ? kq.Sum(p => p.nd26).ToString() : "",
                                nd27 = (kq.Sum(p => p.nd27) > 0) ? kq.Sum(p => p.nd27).ToString() : "",
                                nd28 = (kq.Sum(p => p.nd28) > 0) ? kq.Sum(p => p.nd28).ToString() : "",
                                nd29 = (kq.Sum(p => p.nd29) > 0) ? kq.Sum(p => p.nd29).ToString() : "",
                                nd30 = (kq.Sum(p => p.nd30) > 0) ? kq.Sum(p => p.nd30).ToString() : "",
                                nd31 = (kq.Sum(p => p.nd31) > 0) ? kq.Sum(p => p.nd31).ToString() : "",
                                nd32 = (kq.Sum(p => p.nd32) > 0) ? kq.Sum(p => p.nd32).ToString() : "",
                                nd33 = (kq.Sum(p => p.nd33) > 0) ? kq.Sum(p => p.nd33).ToString() : "",
                                nd34 = (kq.Sum(p => p.nd34) > 0) ? kq.Sum(p => p.nd34).ToString() : "",
                                nd35 = (kq.Sum(p => p.nd35) > 0) ? kq.Sum(p => p.nd35).ToString() : "",
                                nd36 = (kq.Sum(p => p.nd36) > 0) ? kq.Sum(p => p.nd36).ToString() : "",
                                nd37 = (kq.Sum(p => p.nd27) > 0) ? kq.Sum(p => p.nd27).ToString() : "",
                                nd38 = (kq.Sum(p => p.nd28) > 0) ? kq.Sum(p => p.nd28).ToString() : "",
                                nd39 = (kq.Sum(p => p.nd29) > 0) ? kq.Sum(p => p.nd29).ToString() : "",
                                nd40 = (kq.Sum(p => p.nd30) > 0) ? kq.Sum(p => p.nd30).ToString() : "",
                                tc = (kq.Sum(p => p.tc) > 0) ? kq.Sum(p => p.tc).ToString() : ""
                            }).ToList();
            for (int i = 0; i < _lKho.Where(p => p.Check && p.MaKP >= 0).ToList().Count; i += 7)
            {
                BaoCao.rep_BC_SolanThucHienPTTT rep = new BaoCao.rep_BC_SolanThucHienPTTT(_DSKP);
                frmIn frm = new frmIn();
                rep.Tungay.Value = date_tungay.Text;
                rep.Denngay.Value = date_ngayden.Text;
                rep.DataSource = solan2;
                rep.BindingData(i, _lKho.Where(p => p.Check && p.MaKP >= 0).ToList().Count);
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
            }
        }

        private void frm_BC_SolanThucHienPTTT_Load(object sender, EventArgs e)
        {
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            date_tungay.DateTime = DateTime.Now;
            date_ngayden.DateTime = DateTime.Now;
            listKP(true);
        }
        public void listKP(bool Check)
        {
            _lKho.Clear();
            var kd = from khoa in data.KPhongs.Where(p => p.PLoai == "Lâm sàng" || p.PLoai == "Phòng khám") select new { khoa.TenKP, khoa.MaKP };
            if (kd.Count() > 0)
            {
                Kho themmoi1 = new Kho();
                themmoi1.TenKP = "Chọn tất cả";
                themmoi1.MaKP = 0;
                themmoi1.Check = Check;
                _lKho.Add(themmoi1);
                foreach (var a in kd)
                {
                    Kho themmoi = new Kho();
                    themmoi.TenKP = a.TenKP;
                    themmoi.MaKP = a.MaKP;
                    themmoi.Check = Check;
                    _lKho.Add(themmoi);
                }
                grckhoaphong.DataSource = _lKho.ToList();
            }
        }
        private void grvKho_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            bool Check = true;
            if (e.Column.Name == "colChon")
            {
                if (grvkhoaphong.GetFocusedRowCellValue("TenKP") != null)
                {
                    string Ten = grvkhoaphong.GetFocusedRowCellValue("TenKP").ToString();

                    if (Ten == "Chọn tất cả")
                    {
                        if (_lKho.First().Check == true)
                        {
                            foreach (var a in _lKho)
                            {
                                a.Check = false;
                                Check = false;
                            }
                        }
                        else
                        {
                            foreach (var a in _lKho)
                            {
                                a.Check = true;
                                Check = true;
                            }
                        }
                        grckhoaphong.DataSource = "";
                        listKP(Check);
                    }
                }
            }
        }
        public class NoiDung
        {
            public string TenDV { get; set; }
            public int MaKP { get; set; }
            public double nd1 { get; set; }
            public double nd2 { get; set; }
            public double nd3 { get; set; }
            public double nd4 { get; set; }
            public double nd5 { get; set; }
            public double nd6 { get; set; }
            public double nd7 { get; set; }
            public double nd8 { get; set; }
            public double nd9 { get; set; }
            public double nd10 { get; set; }
            public double nd11{ get; set; }
            public double nd12 { get; set; }
            public double nd13 { get; set; }
            public double nd14 { get; set; }
            public double nd15 { get; set; }
            public double nd16 { get; set; }
            public double nd17 { get; set; }
            public double nd18 { get; set; }
            public double nd19 { get; set; }
            public double nd20 { get; set; }
            public double nd21 { get; set; }
            public double nd22 { get; set; }
            public double nd23 { get; set; }
            public double nd24 { get; set; }
            public double nd25 { get; set; }
            public double nd26 { get; set; }
            public double nd27 { get; set; }
            public double nd28 { get; set; }
            public double nd29 { get; set; }
            public double nd30 { get; set; }
            public double nd31 { get; set; }
            public double nd32 { get; set; }
            public double nd33 { get; set; }
            public double nd34 { get; set; }
            public double nd35 { get; set; }
            public double nd36 { get; set; }
            public double nd37 { get; set; }
            public double nd38 { get; set; }
            public double nd39 { get; set; }
            public double nd40 { get; set; }
            public double tc { get; set; }
        }
    }
}