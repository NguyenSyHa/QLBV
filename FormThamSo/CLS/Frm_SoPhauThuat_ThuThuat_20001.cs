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
    public partial class Frm_SoPhauThuat_ThuThuat_20001 : DevExpress.XtraEditors.XtraForm
    {
        public Frm_SoPhauThuat_ThuThuat_20001()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        string _ptt = "";
        private bool KTtaoBc()
        {
            if (lupTuNgay.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn ngày bắt đầu in báo cáo");
                lupTuNgay.Focus();
                return false;
            }
            if (lupDenNgay.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn đến ngày kết thúc in báo cáo");
                lupDenNgay.Focus();
                return false;
            }

            return true;
        }
        private class KPhong
        {
            private string TenKP;
            private int MaKP;
            private bool Chon;
            public string tenkp
            { set { TenKP = value; } get { return TenKP; } }
            public int makp
            { set { MaKP = value; } get { return MaKP; } }
            public bool chon
            { set { Chon = value; } get { return Chon; } }
        }
        private class DichVu
        {
            private string TenDV;
            private int MaDV;
            private bool Chon;
            public string tendv
            { set { TenDV = value; } get { return TenDV; } }
            public int madv
            { set { MaDV = value; } get { return MaDV; } }
            public bool chon
            { set { Chon = value; } get { return Chon; } }
        }

        private class MaDT
        {
            private string DTBN;
            private bool Chon;
            public string dtbn
            { set { DTBN = value; } get { return DTBN; } }
            public bool chon
            { set { Chon = value; } get { return Chon; } }
        }

        List<MaDT> _lmadt = new List<MaDT>();
        List<KPhong> _Kphong = new List<KPhong>();
        //List<DV> _ldv = new List<DV>();
        List<DichVu> _lmadv = new List<DichVu>();
                     void loaddv(){
                         _lmadv.Clear();
                         grcDichVu.DataSource = null;
                         var qdvdt1 = (from dv in data.DichVus
                                       join tn in data.TieuNhomDVs.Where(p => radioGroup1.SelectedIndex == 2 ? p.IDNhom == 8 : p.TenRG == _ptt) on dv.IdTieuNhom equals tn.IdTieuNhom
                                       select new { dv.MaDV, dv.TenDV, tn.TenRG }).ToList();
                         var qdvdt = (from dv in qdvdt1 group dv by new { dv.MaDV, dv.TenDV } into kq select new { kq.Key.MaDV, kq.Key.TenDV }).ToList();

                         if (qdvdt.Count > 0)
                         {
                             DichVu themmoi1 = new DichVu();
                             themmoi1.tendv = "A.Chọn tất cả";
                             themmoi1.madv = 0;
                             themmoi1.chon = true;
                             _lmadv.Add(themmoi1);

                             foreach (var a in qdvdt)
                             {
                                 DichVu them = new DichVu();
                                 them.madv = a.MaDV;
                                 them.tendv = a.TenDV;
                                 them.chon = true;
                                 _lmadv.Add(them);
                             }
                         }
                         grcDichVu.DataSource = _lmadv.OrderBy(p => p.tendv).ToList();
                     }
        private void Frm_SoPhauThuat_Load(object sender, EventArgs e)
        {
            _Kphong.Clear();
            lupTuNgay.Focus();
            lupTuNgay.DateTime = System.DateTime.Now;
            lupDenNgay.DateTime = System.DateTime.Now;
            radioGroup1.SelectedIndex = 2;
            if (radioGroup1.SelectedIndex == 0) { _ptt = "Phẫu thuật"; }
            if (radioGroup1.SelectedIndex == 1) { _ptt = "Thủ thuật"; }
            var kphong = (from kp in data.KPhongs
                          where (kp.PLoai == "Lâm sàng" || kp.PLoai == "Phòng khám")
                          select new { kp.TenKP, kp.MaKP }).ToList();
            if (kphong.Count > 0)
            {
                KPhong themmoi1 = new KPhong();
                themmoi1.tenkp = "Chọn tất cả";
                themmoi1.makp = 0;
                themmoi1.chon = true;
                _Kphong.Add(themmoi1);
                foreach (var a in kphong)
                {
                    KPhong themmoi = new KPhong();
                    themmoi.tenkp = a.TenKP;
                    themmoi.makp = a.MaKP == null ? 0 : Convert.ToInt32(a.MaKP);
                    themmoi.chon = true;
                    _Kphong.Add(themmoi);
                }
                grcKhoaphong.DataSource = _Kphong.ToList();
            }
            loaddv();
            _lmadt.Clear();
            var dtuong = (from dt in data.DTBNs group dt by new { dt.DTBN1 } into kq select new { kq.Key.DTBN1 }).ToList();
            if (dtuong.Count > 0)
            {
                MaDT themmoi1 = new MaDT();
                themmoi1.dtbn = "Chọn tất cả";
                themmoi1.chon = true;
                _lmadt.Add(themmoi1);
                foreach (var a in dtuong)
                {
                    MaDT themmoi = new MaDT();
                    themmoi.dtbn = a.DTBN1;
                    themmoi.chon = true;
                    _lmadt.Add(themmoi);
                }
                grcDTuong.DataSource = _lmadt.ToList();
            }


        }
        private class BN
        {

            private int MaBNhan;
            private string Nam;
            private string Nu;

            private string DiaChi;
            private int GTinh;
            private int NoiTru;

            private int MaKP;
            private string NoiGui;
            private int MaDV;
            private string Loai;
            private string PPVC;
            private string KetLuan;
            private string MaBSTH;
            private string BSTH;
            private string BSGM;
            public int Tuoi { set; get; }
            private int PL;
            public string TenBNhan
            { set; get; }
            public int mabnhan
            { set { MaBNhan = value; } get { return MaBNhan; } }
            public string nam
            { set { Nam = value; } get { return Nam; } }
            public string nu
            { set { Nu = value; } get { return Nu; } }
            public string BHYT
            { set; get; }
            public string diachi
            { set { DiaChi = value; } get { return DiaChi; } }
            public int noitru
            { set { NoiTru = value; } get { return NoiTru; } }
            public int gtinh
            { set { GTinh = value; } get { return GTinh; } }
            public string ChanDoan
            { set; get; }
            public int makp
            { set { MaKP = value; } get { return MaKP; } }
            public string noigui
            { set { NoiGui = value; } get { return NoiGui; } }
            public int madv
            { set { MaDV = value; } get { return MaDV; } }
            public string YeuCau
            { set; get; }
            public string BuongGiuong
            { set; get; }
            public string ppvc
            { set { PPVC = value; } get { return PPVC; } }
            public string ketluan
            { set { KetLuan = value; } get { return KetLuan; } }
            public string TTVChinh
            { set { MaBSTH = value; } get { return MaBSTH; } }
            public string TTVPhu
            { set { BSTH = value; } get { return BSTH; } }
            public string TTVPhu2
            { set { BSGM = value; } get { return BSGM; } }
            public DateTime NgayTHtu
            { set; get; }
            public DateTime NgayTHden
            { set; get; }
            public int SoLan { set; get; }
            public string TenDV { set; get; }
            public DateTime? NgayTH { set; get; }
            public int IDCD { set; get; }
            public string DSCBth { set; get; }
            

        }
        List<BN> _BNhan = new List<BN>();
        List<KP> _lKhoaP = new List<KP>();
        public class KP
        {
            private int makp;
            public int MaKP
            {
                set { makp = value; }
                get { return makp; }
            }
        }

        private void btnInBC_Click(object sender, EventArgs e)
        {
            DateTime tungay = System.DateTime.Now.Date;
            DateTime denngay = System.DateTime.Now.Date;
            List<KPhong> _lkp = new List<KPhong>();
            List<MaDT> _ldt = new List<MaDT>();

            if (KTtaoBc())
            {
                _BNhan.Clear(); _lKhoaP.Clear();

                tungay = DungChung.Ham.NgayTu(lupTuNgay.DateTime);
                denngay = DungChung.Ham.NgayDen(lupDenNgay.DateTime);
                data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                var cb = data.CanBoes.ToList();
                _lkp = _Kphong.Where(p => p.chon == true).ToList();
                //_lkp.Add(new KPhong { makp = 0, tenkp = "" });
                
                _ldt = _lmadt.Where(p => p.chon == true).ToList();
                _ldt.Add(new MaDT { dtbn = "" });
                int loaitimkiem = cboLoaiTimKiem.SelectedIndex;
                #region Tất cả BN thực hiện
           
                DateTime ngaykhamden = denngay.AddMonths(2);
                DateTime ngaykhamtu = tungay.AddMonths(-3);
                var bnkb = (from bn in data.BNKBs.Where(p => p.NgayKham >= ngaykhamtu && p.NgayKham <= ngaykhamden) select new { bn.MaBNhan, bn.MaKP, Buong = bn.Buong + bn.Giuong }).Distinct().ToList();
                var qdv = (from tn in data.TieuNhomDVs.Where(p => radioGroup1.SelectedIndex == 2 ? p.IDNhom == 8 : p.TenRG == _ptt)
                           join dv in data.DichVus on tn.IdTieuNhom equals dv.IdTieuNhom
                           select new { dv.TenDV, dv.MaDV, tn.TenRG, dv.Loai }).ToList();

                List<BN> _qtong = new List<BN>();
                if (cboLoaiTimKiem.SelectedIndex == 0)
                {
                      _qtong = (from cls in data.CLS
                             join cd in data.ChiDinhs.Where(p => p.Status == 1).Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay) on cls.IdCLS equals cd.IdCLS
                             join bn in data.BenhNhans.Where(p=>radBN.SelectedIndex==2?true: p.NoiTru==radBN.SelectedIndex) on cls.MaBNhan equals bn.MaBNhan
                             select new  BN
                             {TTVChinh=cd.MaCBth, ChanDoan= cls.MaICD,IDCD= cd.IDCD,Tuoi=bn.Tuoi??0, YeuCau = cd.ChiDinh1,mabnhan= bn.MaBNhan,TenBNhan= bn.TenBNhan, gtinh= bn.GTinh??0,BHYT= bn.DTuong, noitru= bn.NoiTru??0,madv= cd.MaDV??0, makp= cls.MaKP??0, NgayTH= cd.NgayTH,DSCBth= cd.DSCBTH }
                       ).ToList();
                }
                else
                {
                    _qtong = (from cls in data.CLS
                          join cd in data.ChiDinhs.Where(p => p.Status == 1) on cls.IdCLS equals cd.IdCLS
                              join bn in data.BenhNhans.Where(p => radBN.SelectedIndex == 2 ? true : p.NoiTru == radBN.SelectedIndex) on cls.MaBNhan equals bn.MaBNhan
                          join vp in data.VienPhis.Where(p=>p.NgayTT>=tungay && p.NgayTT<=denngay) on bn.MaBNhan equals vp.MaBNhan
                              select new BN { TTVChinh = cd.MaCBth, ChanDoan = cls.MaICD, IDCD = cd.IDCD, Tuoi = bn.Tuoi ?? 0, YeuCau = cd.ChiDinh1, mabnhan = bn.MaBNhan, TenBNhan = bn.TenBNhan, gtinh = bn.GTinh ?? 0, BHYT = bn.DTuong, noitru = bn.NoiTru ?? 0, madv = cd.MaDV ?? 0, makp = cls.MaKP ?? 0, NgayTH = cd.NgayTH, DSCBth = cd.DSCBTH }
                      ).ToList();
                }
           

                var qcls = (from cls in _qtong
                            join dv in qdv on cls.madv equals dv.MaDV
                            join dvchon in _lmadv.Where(p => p.chon == true) on dv.MaDV equals dvchon.madv
                            select new { cls.ChanDoan,cls.Tuoi,cls.IDCD, cls.YeuCau, cls.mabnhan, cls.TenBNhan, cls.gtinh, cls.BHYT,  cls.noitru, cls.madv,   cls.makp, cls.TTVChinh, cls.NgayTH, cls.DSCBth, dv.TenDV, dv.Loai, dv.TenRG }
                           ).ToList();
                var qso1 = (from kp in _lkp
                            join q in qcls on kp.makp equals q.makp
                            select new {q.Tuoi, q.ChanDoan, q.IDCD, q.YeuCau, q.mabnhan, q.TenBNhan,  q.gtinh,  q.BHYT,  q.noitru, q.madv, q.TenDV, q.Loai,  kp.makp, kp.tenkp, q.TTVChinh, q.NgayTH, q.DSCBth,  q.TenRG }).ToList();
                var qso = (from q in qso1
                           join dt in _ldt.Where(p => p.dtbn != "Chọn tất cả")
                               on q.BHYT equals dt.dtbn
                           join kb in bnkb on new { q.mabnhan, q.makp } equals new { mabnhan = kb.MaBNhan ?? 0, makp = kb.MaKP ?? 0 }
                           group q by new {q.Tuoi, kb.Buong, q.ChanDoan, q.mabnhan, q.TenBNhan, q.gtinh,  q.BHYT, q.noitru, q.madv, q.TenDV, q.Loai, q.makp, q.tenkp, q.TTVChinh, q.DSCBth, q.YeuCau } into kq
                           select new
                           {
                               kq.Key.Tuoi,
                               kq.Key.Buong,
                               kq.Key.TTVChinh,
                               kq.Key.ChanDoan,
                               MaBNhan = kq.Key.mabnhan,
                               TenBNhan = kq.Key.TenBNhan,
                               GTinh = kq.Key.gtinh,
                               DTuong = kq.Key.BHYT,
                               NoiTru = kq.Key.noitru,
                               MaDV = kq.Key.madv,
                               TenDV = kq.Key.TenDV,
                             
                               YeuCau = kq.Key.YeuCau,
                               Loai = kq.Key.Loai,
                               MakP = kq.Key.makp,
                               TenKP = kq.Key.tenkp,
                               BSTH = kq.Key.TTVChinh,
                               NgayTHtu = kq.Select(p => p.NgayTH).Min(),
                               NgayTHden = kq.Select(p => p.NgayTH).Max(),
                               soLan = kq.Select(p => p.IDCD).Count(),
                               DSCBth = kq.Key.DSCBth,
                           }).OrderBy(p => p.NgayTHtu).ToList();
                if (qso.Count() > 0)
                {
                    foreach (var a in qso)
                    {
                        BN themmoi = new BN();
                        themmoi.mabnhan = a.MaBNhan;
                        themmoi.TenBNhan = a.TenBNhan;
                        themmoi.gtinh = Convert.ToInt32(a.GTinh);
                        if (a.GTinh == 1) { themmoi.nam = a.Tuoi.ToString(); } else { themmoi.nam = ""; }
                        if (a.GTinh == 0) { themmoi.nu = a.Tuoi.ToString(); } else { themmoi.nu = ""; }

                        if (a.DTuong == "BHYT")
                        {
                            themmoi.BHYT = "X";
                        }
                        else
                            themmoi.BHYT = "";
                        themmoi.ChanDoan = a.ChanDoan;
                        themmoi.YeuCau = a.YeuCau;
                        themmoi.SoLan = a.soLan;
                        themmoi.BuongGiuong = a.Buong;
                        themmoi.madv = a.MaDV;
                        themmoi.TenDV = a.TenDV;
                        themmoi.makp = a.MakP;
                        themmoi.NgayTHtu = a.NgayTHtu ?? DateTime.Now;
                        themmoi.NgayTHden = a.NgayTHden ?? DateTime.Now;
                        string mabsth = a.TTVChinh ?? "";
                        themmoi.TTVChinh = cb.Where(p => p.MaCB == mabsth).Select(p => p.TenCB).FirstOrDefault();
                        if (a.DSCBth != null)
                        {
                            string _dscb = a.DSCBth;
                            string[] gm = new string[5];
                            gm = QLBV_Library.QLBV_Ham.LayChuoi(';', _dscb);
                            if (gm.Length > 0)
                                themmoi.TTVPhu = gm[0];
                            if (gm.Length > 4)
                                themmoi.TTVPhu2 = gm[4];
                        }

                        _BNhan.Add(themmoi);
                    }
                }


            }
                #endregion


            BaoCao.Rep_SoPhauThuat_ThuThuat_A4_20001 rep = new BaoCao.Rep_SoPhauThuat_ThuThuat_A4_20001();
            int  khoa = _lkp.Where(p => p.makp != 0).Select(p => p.tenkp).ToList().Count;
            if (khoa==1)
            rep.Khoa.Value = string.Join(";", _lkp.Where(p=>p.makp!=0).Select(p => p.tenkp).ToArray());
            rep.TuNgay.Value = tungay.ToString().Substring(0, 10);
            rep.DenNgay.Value = denngay.ToString().Substring(0, 10);
            rep.TenSo.Value = ("SỔ TỔNG HỢP").ToUpper();
            rep.DataSource = _BNhan.OrderBy(p => p.NgayTHtu).ThenBy(p => p.TenBNhan).ToList();
            //rep.Nam.Value = _BN.Where(p => p.gtinh == 1).Select(p => p.mabnhan).Count();
            //rep.Nu.Value = _BN.Where(p => p.gtinh == 0).Select(p => p.mabnhan).Count();
            //rep.BHYT.Value = _BN.Where(p => p.bhyt == "X").Select(p => p.mabnhan).Count();
            #region xuat Excel

            //string[] _arr = new string[] {  "0", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@" } ;
            //string[] _tieude = { "STT", "Họ tên người bệnh", "Tuổi nam", "Tuổi nữ", "Địa chỉ", "BHYT", "CĐ trước PTTT", "CĐ sau PTTT", "Phương pháp PTTT", "Phương pháp vô cảm", "Ngày giờ PTTT", "Loại PTTT", "Bác sỹ PTTT", "Bác sỹ gây mê, tê", "Ghi chú" };
            //DungChung.Bien.MangHaiChieu = new Object[_BN.Count + 2, 15];
            //for (int i = 0; i < _tieude.Length; i++)
            //{
            //    DungChung.Bien.MangHaiChieu[0, i] = _tieude[i];
            //}
            //int[] _arrWidth = new int[] { };
            //int num = 0;
            //foreach (var r in _BN)
            //{
            //    DungChung.Bien.MangHaiChieu[num, 0] = num + 1;
            //    DungChung.Bien.MangHaiChieu[num, 1] = r.tenbnhan;
            //    DungChung.Bien.MangHaiChieu[num, 2] = r.nam;
            //    DungChung.Bien.MangHaiChieu[num, 3] = r.nu;
            //    DungChung.Bien.MangHaiChieu[num, 4] = r.diachi;
            //    DungChung.Bien.MangHaiChieu[num, 5] = r.bhyt;
            //    DungChung.Bien.MangHaiChieu[num, 6] = r.chandoan;
            //    DungChung.Bien.MangHaiChieu[num, 7] = r.ketluan;
            //    DungChung.Bien.MangHaiChieu[num, 8] = r.yeucau;
            //    DungChung.Bien.MangHaiChieu[num, 9] = r.ppvc;
            //    DungChung.Bien.MangHaiChieu[num, 10] = r.ngayth;
            //    DungChung.Bien.MangHaiChieu[num, 11] = r.loai;
            //    DungChung.Bien.MangHaiChieu[num, 12] = r.bsth;
            //    DungChung.Bien.MangHaiChieu[num, 13] = r.bsgm;
            //    DungChung.Bien.MangHaiChieu[num, 14] = r.noigui;

            //    num++;

            //}
            ////frmIn frm = new frmIn();

            //frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "Báo cáo Phẫu thuật / Thủ thuật", "C:\\BcPTTT.xls", true, this.Name);
            #endregion
            rep.BindingData();
            rep.CreateDocument();
            frmIn frm = new frmIn();
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();






        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void grvKhoaphong_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "col_Chon")
            {
                if (grvKhoaphong.GetFocusedRowCellValue("tenkp") != null)
                {
                    string Ten = grvKhoaphong.GetFocusedRowCellValue("tenkp").ToString();

                    if (Ten == "Chọn tất cả")
                    {
                        if (_Kphong.First().chon == true)
                        {
                            foreach (var a in _Kphong)
                            {
                                a.chon = false;
                            }
                        }
                        else
                        {
                            foreach (var a in _Kphong)
                            {
                                a.chon = true;
                            }
                        }
                        grcKhoaphong.DataSource = "";
                        grcKhoaphong.DataSource = _Kphong.ToList();
                    }
                    //else
                    //{
                        //if (grvKhoaphong.GetFocusedRowCellValue(col_Chon) != null && grvKhoaphong.GetFocusedRowCellValue(col_Chon).ToString().ToLower() == "false")
                        //{
                        //    foreach (var a in _Kphong)
                        //    {
                        //        if (a.tenkp == grvKhoaphong.GetFocusedRowCellValue(TenKP).ToString())
                        //        {
                        //            a.chon = true;
                        //        }
                        //    }
                        //}
                        //else
                        //{
                        //    foreach (var a in _Kphong)
                        //    {
                        //        if (a.tenkp == grvKhoaphong.GetFocusedRowCellValue(TenKP).ToString())
                        //        {
                        //            a.chon = false;
                        //        }
                        //    }
                        //}
                    //}

                }


            }


        }

        private void grvDTuong_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {

            if (e.Column.Name == "Chon")
            {
                if (grvDTuong.GetFocusedRowCellValue("dtbn") != null)
                {
                    string Madt = grvDTuong.GetFocusedRowCellValue("dtbn").ToString();

                    if (Madt == "Chọn tất cả")
                    {
                        if (_lmadt.First().chon == true)
                        {
                            foreach (var a in _lmadt)
                            {
                                a.chon = false;
                            }
                        }
                        else
                        {
                            foreach (var a in _lmadt)
                            {
                                a.chon = true;
                            }
                        }
                        grcDTuong.DataSource = "";
                        grcDTuong.DataSource = _lmadt.ToList();
                    }
                }
            }

        }


        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (radioGroup1.SelectedIndex == 0) { _ptt = "Phẫu thuật"; }
            else { _ptt = "Thủ thuật"; }
            loaddv();
        }

        private void radioGroup1_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {


        }

        private void radioGroup1_Properties_SelectedIndexChanged(object sender, EventArgs e)
        {
         
        }

        private void grvDichVu_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "colChon")
            {
                if (grvDichVu.GetFocusedRowCellValue("tendv") != null)
                {
                    string Ten = grvDichVu.GetFocusedRowCellValue("tendv").ToString();

                    if (Ten == "A.Chọn tất cả")
                    {
                        if (_lmadv.First().chon == true)
                        {
                            foreach (var a in _lmadv)
                            {
                                a.chon = false;
                            }
                        }
                        else
                        {
                            foreach (var a in _lmadv)
                            {
                                a.chon = true;
                            }
                        }
                        grcDichVu.DataSource = "";
                        grcDichVu.DataSource = _lmadv.ToList();
                    }
                    else
                    {
                        //if (grvDichVu.GetFocusedRowCellValue(colChon) != null && grvDichVu.GetFocusedRowCellValue(colChon).ToString().ToLower() == "false")
                        //{
                        //    foreach (var a in _lmadv)
                        //    {
                        //        if (a.tendv == grvDichVu.GetFocusedRowCellValue(colTenDV).ToString())
                        //        {
                        //            a.chon = true;
                        //        }
                        //    }
                        //}
                        //else
                        //{
                        //    foreach (var a in _lmadv)
                        //    {
                        //        if (a.tendv == grvDichVu.GetFocusedRowCellValue(colTenDV).ToString())
                        //        {
                        //            a.chon = false;
                        //        }
                        //    }
                        //}
                    }
                }
            }
        }
    }
}
