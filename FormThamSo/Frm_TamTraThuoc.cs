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
    public partial class Frm_TamTraThuoc : DevExpress.XtraEditors.XtraForm
    {
        public Frm_TamTraThuoc()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities _Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void Frm_TamTraThuoc_Load(object sender, EventArgs e)
        {
            LupNgayTu.DateTime = System.DateTime.Now;
            LupNgayDen.DateTime = System.DateTime.Now;
            var KP = (from kp1 in _Data.KPhongs.Where(p => p.PLoai.Contains("Lâm sàng")) select new { kp1.TenKP, kp1.MaKP }).ToList();
            if (KP.Count > 0)
            {
                LupKhoaPhong.Properties.DataSource = KP.OrderBy(p => p.TenKP).ToList();
            }
            //radM.SelectedIndex = 1;
            radNT.SelectedIndex = 2;
            chkBoxung.Checked = true;
            chkThuongxuyen.Checked = true;
            chkTrathuoc.Checked = true;
        }

        private void Huy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private class Tamtra
        {
            private int tuoi;
            private string TenBN;
            private string sl1;
            private string sl2;
            private string sl3;
            private string sl4;
            private string sl5;
            private string sl6;
            private string sl7;
            private string sl8;
            private string sl9;
            private string sl10;
            private string sl11;
            private string sl12;
            private string sl13;
            private string sl14;
            private string sl15;
            private string sl16;
            private string sl17;
            private string sl18;
            private string sl19;
            private string sl20;
            private string sl21;
            private string sl22;
            private string sl23;
            private string sl24;
            private string sl25;
            private string sl26;
            private string sl27;
            private string sl28;
            private string sl29;
            private string sl30;
            private string sl31;
            private string sl32;
            private string sl33;
            private string sl34;
            private string sl35;
            private string sl36;
            private string sl37;
            private string sl38;
            private string sl39;
            private string sl40;
            private string sl41;
            private string sl42;
            private string sl43;
            private string sl44;
            private string sl45;
            private string sl46;
            private string sl47;
            private string sl48;
            private string sl49;
            private string sl50;
            private string sl51;
            private string sl52;
            private string sl53;
            private string sl54;
            private string sl55;
            private string sl56;
            private string sl57;
            private string sl58;
            private string sl59;
            private string sl60;
            private string sl61;
            private string sl62;
            private string sl63;
            private string sl64;
            private string sl65;
            private string sl66;
            private string sl67;
            private string sl68;
            private string sl69;
            private string sl70;
            private string sl71;
            private string sl72;
            private string sl73;
            private string sl74;
            private string sl75;
            private string sl76;
            private string sl77;
            private string sl78;
            private string sl79;
            private string sl80;
            private string sl81;
            private string sl82;
            private string sl83;
            private string sl84;
            private string sl85;
            private string sl86;
            private string sl87;
            private string sl88;
            private string sl89;
            private string sl90;
            private string sl91;
            private string sl92;
            private string sl93;
            private string sl94;
            private string sl95;
            private string sl96;
            private string sl97;
            private string sl98;
            private string sl99;
            private int MaBN;
            private int SX;
            public int sx
            { set { SX = value; } get { return SX; } }
            public int mabn

            { set { MaBN = value; } get { return MaBN; } }
            public string tenbn
            {
                set { TenBN = value; }
                get { return TenBN; }
            }
            public int Tuoi
            {
                set { tuoi = value; }
                get { return tuoi; }
            }
            public string SL101
            { set { sl1 = value; } get { return sl1; } }
            public string SL201
            { set { sl2 = value; } get { return sl2; } }
            public string SL301
            { set { sl3 = value; } get { return sl3; } }
            public string SL401
            { set { sl4 = value; } get { return sl4; } }
            public string SL501
            { set { sl5 = value; } get { return sl5; } }
            public string SL601
            { set { sl6 = value; } get { return sl6; } }
            public string SL701
            { set { sl7 = value; } get { return sl7; } }
            public string SL801
            { set { sl8 = value; } get { return sl8; } }
            public string SL901
            { set { sl9 = value; } get { return sl9; } }
            public string SL1001
            { set { sl10 = value; } get { return sl10; } }
            public string SL1101
            { set { sl11 = value; } get { return sl11; } }
            public string SL1201
            {
                set { sl12 = value; }
                get { return sl12; }
            }
            public string SL1301
            { set { sl13 = value; } get { return sl13; } }
            public string SL1401
            { set { sl14 = value; } get { return sl14; } }
            public string SL1501
            {
                set { sl15 = value; }
                get { return sl15; }
            }
            public string SL1601
            {
                set { sl16 = value; }
                get { return sl16; }
            }
            public string SL1701
            {
                set { sl17 = value; }
                get { return sl17; }
            }
            public string SL1801
            {
                set { sl18 = value; }
                get { return sl18; }
            }
            public string SL1901
            {
                set { sl19 = value; }
                get { return sl19; }
            }
            public string SL2001
            {
                set { sl20 = value; }
                get { return sl20; }
            }
            public string SL2101
            {
                set { sl21 = value; }
                get { return sl21; }
            }
            public string SL2201
            {
                set { sl22 = value; }
                get { return sl22; }
            }
            public string SL2301
            {
                set { sl23 = value; }
                get { return sl23; }
            }
            public string SL2401
            {
                set { sl24 = value; }
                get { return sl24; }
            }
            public string SL2501
            {
                set { sl25 = value; }
                get { return sl25; }
            }
            public string SL2601
            {
                set { sl26 = value; }
                get { return sl26; }
            }
            public string SL2701
            {
                set { sl27 = value; }
                get { return sl27; }
            }
            public string SL2801
            {
                set { sl28 = value; }
                get { return sl28; }
            }
            public string SL2901
            {
                set { sl29 = value; }
                get { return sl29; }
            }
            public string SL3001
            {
                set { sl30 = value; }
                get { return sl30; }
            }
            public string SL3101
            {
                set { sl31 = value; }
                get { return sl31; }
            }
            public string SL3201
            {
                set { sl32 = value; }
                get { return sl32; }
            }
            public string SL3301
            {
                set { sl33 = value; }
                get { return sl33; }
            }
            public string SL3401
            {
                set { sl34 = value; }
                get { return sl34; }
            }
            public string SL3501
            {
                set { sl35 = value; }
                get { return sl35; }
            }
            public string SL3601
            {
                set { sl36 = value; }
                get { return sl36; }
            }
            public string SL3701
            {
                set { sl37 = value; }
                get { return sl37; }
            }
            public string SL3801
            {
                set { sl38 = value; }
                get { return sl38; }
            }
            public string SL3901
            {
                set { sl39 = value; }
                get { return sl39; }
            }
            public string SL4001
            {
                set { sl40 = value; }
                get { return sl40; }
            }
            public string SL4101
            {
                set { sl41 = value; }
                get { return sl41; }
            }
            public string SL4201
            {
                set { sl42 = value; }
                get { return sl42; }
            }
            public string SL4301
            {
                set { sl43 = value; }
                get { return sl43; }
            }
            public string SL4401
            {
                set { sl44 = value; }
                get { return sl44; }
            }
            public string SL4501
            {
                set { sl45 = value; }
                get { return sl45; }
            }
            public string SL4601
            {
                set { sl46 = value; }
                get { return sl46; }
            }
            public string SL4701
            {
                set { sl47 = value; }
                get { return sl47; }
            }
            public string SL4801
            {
                set { sl48 = value; }
                get { return sl48; }
            }
            public string SL4901
            {
                set { sl49 = value; }
                get { return sl49; }
            }
            public string SL5001
            {
                set { sl50 = value; }
                get { return sl50; }
            }
            public string SL5101
            {
                set { sl51 = value; }
                get { return sl51; }
            }
            public string SL5201
            {
                set { sl52 = value; }
                get { return sl52; }
            }
            public string SL5301
            {
                set { sl53 = value; }
                get { return sl53; }
            }
            public string SL5401
            {
                set { sl54 = value; }
                get { return sl54; }
            }
            public string SL5501
            {
                set { sl55 = value; }
                get { return sl55; }
            }
            public string SL5601
            {
                set { sl56 = value; }
                get { return sl56; }
            }
            public string SL5701
            {
                set { sl57 = value; }
                get { return sl57; }
            }
            public string SL5801
            {
                set { sl58 = value; }
                get { return sl58; }
            }
            public string SL5901
            {
                set { sl59 = value; }
                get { return sl59; }
            }
            public string SL6001
            {
                set { sl60 = value; }
                get { return sl60; }
            }
            public string SL6101
            {
                set { sl61 = value; }
                get { return sl61; }
            }
            public string SL6201
            {
                set { sl62 = value; }
                get { return sl62; }
            }
            public string SL6301
            {
                set { sl63 = value; }
                get { return sl63; }
            }
            public string SL6401
            {
                set { sl64 = value; }
                get { return sl64; }
            }
            public string SL6501
            {
                set { sl65 = value; }
                get { return sl65; }
            }
            public string SL6601
            {
                set { sl66 = value; }
                get { return sl66; }
            }
            public string SL6701
            {
                set { sl67 = value; }
                get { return sl67; }
            }
            public string SL6801
            {
                set { sl68 = value; }
                get { return sl68; }
            }
            public string SL6901
            {
                set { sl69 = value; }
                get { return sl69; }
            }
            public string SL7001
            {
                set { sl70 = value; }
                get { return sl70; }
            }
            public string SL7101
            {
                set { sl71 = value; }
                get { return sl71; }
            }
            public string SL7201
            {
                set { sl72 = value; }
                get { return sl72; }
            }
            public string SL7301
            {
                set { sl73 = value; }
                get { return sl73; }
            }
            public string SL7401
            {
                set { sl74 = value; }
                get { return sl74; }
            }
            public string SL7501
            {
                set { sl75 = value; }
                get { return sl75; }
            }
            public string SL7601
            {
                set { sl76 = value; }
                get { return sl76; }
            }
            public string SL7701
            {
                set { sl77 = value; }
                get { return sl77; }
            }
            public string SL7801
            {
                set { sl78 = value; }
                get { return sl78; }
            }
            public string SL7901
            {
                set { sl79 = value; }
                get { return sl79; }
            }
            public string SL8001
            {
                set { sl80 = value; }
                get { return sl80; }
            }
            public string SL8101
            {
                set { sl81 = value; }
                get { return sl81; }
            }
            public string SL8201
            {
                set { sl82 = value; }
                get { return sl82; }
            }
            public string SL8301
            {
                set { sl83 = value; }
                get { return sl83; }
            }
            public string SL8401
            {
                set { sl84 = value; }
                get { return sl84; }
            }
            public string SL8501
            {
                set { sl85 = value; }
                get { return sl85; }
            }
            public string SL8601
            {
                set { sl86 = value; }
                get { return sl86; }
            }
            public string SL8701
            {
                set { sl87 = value; }
                get { return sl87; }
            }
            public string SL8801
            {
                set { sl88 = value; }
                get { return sl88; }
            }
            public string SL8901
            {
                set { sl89 = value; }
                get { return sl89; }
            }
            public string SL9001
            {
                set { sl90 = value; }
                get { return sl90; }
            }
            public string SL9101
            {
                set { sl91 = value; }
                get { return sl91; }
            }
            public string SL9201
            {
                set { sl92 = value; }
                get { return sl92; }
            }
            public string SL9301
            {
                set { sl93 = value; }
                get { return sl93; }
            }
            public string SL9401
            {
                set { sl94 = value; }
                get { return sl94; }
            }
            public string SL9501
            {
                set { sl95 = value; }
                get { return sl95; }
            }
            public string SL9601
            {
                set { sl96 = value; }
                get { return sl96; }
            }
            public string SL9701
            {
                set { sl97 = value; }
                get { return sl97; }
            }
            public string SL9801
            {
                set { sl98 = value; }
                get { return sl98; }
            }
            public string SL9901
            {
                set { sl99 = value; }
                get { return sl99; }
            }

        }
        class DSDV
        {
            private string TenDV;
            private int MaDV;
            public string tendv
            { set { TenDV = value; } get { return TenDV; } }
            public int madv
            {
                set { MaDV = value; }
                get { return MaDV; }
            }
        }
        List<DSDV> _DSDV = new List<DSDV>();
        List<DSDV> _DSDVMoi = new List<DSDV>();
        List<Tamtra> _Tamtra = new List<Tamtra>();
        List<Tamtra> _Tamtramoi = new List<Tamtra>();
        private bool KT()
        {
            if (LupNgayTu.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập ngày tháng");
                LupNgayTu.Focus();
                return false;
            }
            if (LupNgayDen.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập ngày tháng");
                LupNgayDen.Focus();
                return false;
            }
            if (LupKhoaPhong.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn khoa phòng!");
                LupKhoaPhong.Focus();
                return false;
            }
            if (CboThuoc.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn trạng thái!");
                CboThuoc.Focus();
                return false;
            }
            return true;
        }
        private void Taoso_Click(object sender, EventArgs e)
        {
            if (KT())
            {
                int _a1 = -1;
                int _a2 = -1;
                int _a3 = -1;
                //int _a4 = -1;
                if (chkBoxung.Checked == true)
                { _a2 = 1; }
                if (chkThuongxuyen.Checked == true)
                { _a1 = 0; }
                if (chkTrathuoc.Checked == true)
                { _a3 = 2; }
                if (radNT.SelectedIndex == 0)
                {
                    int _Status1 = 0;
                    int _Status2 = 0;
                    int TT = CboThuoc.SelectedIndex;
                    switch (TT)
                    {
                        case 0:
                            _Status1 = 1;
                            _Status2 = 1;
                            break;
                        case 1:
                            _Status1 = 0;
                            _Status2 = 0;
                            break;
                        case 2:
                            _Status1 = 1;
                            _Status2 = 0;
                            break;
                    }
                    int _MaKP = 0;
                    _Tamtra.Clear();
                    _DSDV.Clear();
                    if (LupKhoaPhong.Text != null)
                    {
                        _MaKP = Convert.ToInt32( LupKhoaPhong.EditValue);
                    }
                    DateTime Ngaytu = DungChung.Ham.NgayTu(LupNgayTu.DateTime);
                    DateTime Ngayden = DungChung.Ham.NgayDen(LupNgayDen.DateTime);

                    if (chkThuoc.Checked == true)
                    {
                        var bn2 = (from bn1 in _Data.BenhNhans.Where(p => p.NoiTru == 1)
                                   join DT in _Data.DThuocs.Where(p => p.KieuDon == _a1 || p.KieuDon == _a2 || p.KieuDon == _a3).Where(p => p.MaKP == _MaKP) on bn1.MaBNhan equals DT.MaBNhan
                                   join DTct in _Data.DThuoccts on DT.IDDon equals DTct.IDDon
                                   join DV in _Data.DichVus.Where(P => P.PLoai == 1) on DTct.MaDV equals DV.MaDV
                                   join Ndv in _Data.NhomDVs on DV.IDNhom equals Ndv.IDNhom
                                   where (DT.NgayKe >= Ngaytu && DT.NgayKe <= Ngayden)
                                   where (DTct.Status == _Status1 || DTct.Status == _Status2)
                                   group new { DV, Ndv, DTct } by new { DV.MaDV, DV.TenDV, Ndv.TenNhomCT } into kq
                                   select new
                                   {
                                       TenDV = kq.Key.TenDV,
                                       MaDV = kq.Key.MaDV,
                                       TenN = kq.Key.TenNhomCT,
                                       SL = kq.Select(p => p.DTct.IDDonct).Count()
                                   }).OrderBy(p => p.TenDV).OrderBy(p => p.TenN).ToList();
                        if (bn2.Count > 0)
                        {
                            foreach (var a in bn2)
                            {
                                DSDV themmoi = new DSDV();
                                themmoi.tendv = a.TenDV;
                                themmoi.madv = a.MaDV;
                                _DSDV.Add(themmoi);
                            }
                        }

                        var MaBN = (from bn3 in _Data.BenhNhans.Where(p => p.NoiTru == 1)
                                    join DT in _Data.DThuocs.Where(p => p.KieuDon == _a1 || p.KieuDon == _a2 || p.KieuDon == _a3).Where(p => p.MaKP == _MaKP) on bn3.MaBNhan equals DT.MaBNhan
                                    join DTct in _Data.DThuoccts on DT.IDDon equals DTct.IDDon
                                    join dv in _Data.DichVus.Where(p => p.PLoai == 1) on DTct.MaDV equals dv.MaDV
                                    //join TN in _Data.NhomDVs.Where(p => p.TenNhomCT.Contains("Nhóm thuốc, hoá chất")) on dv.IDNhom equals TN.IDNhom
                                    where (DT.NgayKe >= Ngaytu && DT.NgayKe <= Ngayden)
                                    where (DTct.Status == _Status1 || DTct.Status == _Status2)
                                    group new { bn3 } by new { bn3.MaBNhan } into kq
                                    select new { MaBN = kq.Key.MaBNhan }).ToList();
                        if (MaBN.Count > 0)
                        {
                            foreach (var c in MaBN)
                            {
                                Tamtra themmoi = new Tamtra();
                                themmoi.mabn = c.MaBN;
                                _Tamtra.Add(themmoi);
                                _Tamtramoi.Add(themmoi);
                            }
                        }

                        var bn = (from bn1 in _Data.BenhNhans.Where(p => p.NoiTru == 1)
                                  join DT in _Data.DThuocs.Where(p => p.KieuDon == _a1 || p.KieuDon == _a2 || p.KieuDon == _a3).Where(p => p.MaKP == _MaKP) on bn1.MaBNhan equals DT.MaBNhan
                                  join DTct in _Data.DThuoccts on DT.IDDon equals DTct.IDDon
                                  join DV in _Data.DichVus.Where(p => p.PLoai == 1) on DTct.MaDV equals DV.MaDV
                                  where (DT.NgayKe >= Ngaytu && DT.NgayKe <= Ngayden)
                                  where (DTct.Status != null && (DTct.Status == _Status1 || DTct.Status == _Status2))
                                  group new { bn1, DTct } by new { DV.MaDV, bn1.TenBNhan, bn1.MaBNhan, bn1.Tuoi } into kq
                                  select new
                                  {
                                      kq.Key.Tuoi,
                                      Mabenhnhan = kq.Key.MaBNhan,
                                      MaDV = kq.Key.MaDV,
                                      SoLuong = kq.Sum(p => p.DTct.SoLuong),
                                      TenBN = kq.Key.TenBNhan,
                                  }).ToList();

                        if (bn.Count > 0)
                        {
                            #region tạo code báo cáo
                            foreach (var n in _Tamtra)
                            {
                                foreach (var m in bn)
                                {
                                    if (n.mabn == m.Mabenhnhan)
                                    {
                                        if (m.SoLuong != null && m.SoLuong != 0)
                                        {
                                            for (int i = 0; i < _DSDV.Count; i++)
                                            {
                                                if (m.MaDV == _DSDV.Skip(i).First().madv)
                                                {
                                                    switch (i)
                                                    {
                                                        case 0:
                                                            n.SL101 = m.SoLuong.ToString();
                                                            break;
                                                        case 1:
                                                            n.SL201 = m.SoLuong.ToString();
                                                            break;
                                                        case 2:
                                                            n.SL301 = m.SoLuong.ToString();
                                                            break;
                                                        case 3:
                                                            n.SL401 = m.SoLuong.ToString();
                                                            break;
                                                        case 4:
                                                            n.SL501 = m.SoLuong.ToString();
                                                            break;
                                                        case 5:
                                                            n.SL601 = m.SoLuong.ToString();
                                                            break;
                                                        case 6:
                                                            n.SL701 = m.SoLuong.ToString();
                                                            break;
                                                        case 7:
                                                            n.SL801 = m.SoLuong.ToString();
                                                            break;
                                                        case 8:
                                                            n.SL901 = m.SoLuong.ToString();
                                                            break;
                                                        case 9:
                                                            n.SL1001 = m.SoLuong.ToString();
                                                            break;
                                                        case 10:
                                                            n.SL1101 = m.SoLuong.ToString();
                                                            break;
                                                        case 11:
                                                            n.SL1201 = m.SoLuong.ToString();
                                                            break;
                                                        case 12:
                                                            n.SL1301 = m.SoLuong.ToString();
                                                            break;
                                                        case 13:
                                                            n.SL1401 = m.SoLuong.ToString();
                                                            break;
                                                        case 14:
                                                            n.SL1501 = m.SoLuong.ToString();
                                                            break;
                                                        case 15:
                                                            n.SL1601 = m.SoLuong.ToString();
                                                            break;
                                                        case 16:
                                                            n.SL1701 = m.SoLuong.ToString();
                                                            break;
                                                        case 17:
                                                            n.SL1801 = m.SoLuong.ToString();
                                                            break;
                                                        case 18:
                                                            n.SL1901 = m.SoLuong.ToString();
                                                            break;
                                                        case 19:
                                                            n.SL2001 = m.SoLuong.ToString();
                                                            break;
                                                        case 20:
                                                            n.SL2101 = m.SoLuong.ToString();
                                                            break;
                                                        case 21:
                                                            n.SL2201 = m.SoLuong.ToString();
                                                            break;
                                                        case 22:
                                                            n.SL2301 = m.SoLuong.ToString();
                                                            break;
                                                        case 23:
                                                            n.SL2401 = m.SoLuong.ToString();
                                                            break;
                                                        case 24:
                                                            n.SL2501 = m.SoLuong.ToString();
                                                            break;
                                                        case 25:
                                                            n.SL2601 = m.SoLuong.ToString();
                                                            break;
                                                        case 26:
                                                            n.SL2701 = m.SoLuong.ToString();
                                                            break;
                                                        case 27:
                                                            n.SL2801 = m.SoLuong.ToString();
                                                            break;
                                                        case 28:
                                                            n.SL2901 = m.SoLuong.ToString();
                                                            break;
                                                        case 29:
                                                            n.SL3001 = m.SoLuong.ToString();
                                                            break;
                                                        case 30:
                                                            n.SL3101 = m.SoLuong.ToString();
                                                            break;
                                                        case 31:
                                                            n.SL3201 = m.SoLuong.ToString();
                                                            break;
                                                        case 32:
                                                            n.SL3301 = m.SoLuong.ToString();
                                                            break;
                                                        case 33:
                                                            n.SL3401 = m.SoLuong.ToString();
                                                            break;
                                                        case 34:
                                                            n.SL3501 = m.SoLuong.ToString();
                                                            break;
                                                        case 35:
                                                            n.SL3601 = m.SoLuong.ToString();
                                                            break;
                                                        case 36:
                                                            n.SL3701 = m.SoLuong.ToString();
                                                            break;
                                                        case 37:
                                                            n.SL3801 = m.SoLuong.ToString();
                                                            break;
                                                        case 38:
                                                            n.SL3901 = m.SoLuong.ToString();
                                                            break;
                                                        case 39:
                                                            n.SL4001 = m.SoLuong.ToString();
                                                            break;
                                                        case 40:
                                                            n.SL4101 = m.SoLuong.ToString();
                                                            break;
                                                        case 41:
                                                            n.SL4201 = m.SoLuong.ToString();
                                                            break;
                                                        case 42:
                                                            n.SL4301 = m.SoLuong.ToString();
                                                            break;
                                                        case 43:
                                                            n.SL4401 = m.SoLuong.ToString();
                                                            break;
                                                        case 44:
                                                            n.SL4501 = m.SoLuong.ToString();
                                                            break;
                                                        case 45:
                                                            n.SL4601 = m.SoLuong.ToString();
                                                            break;
                                                        case 46:
                                                            n.SL4701 = m.SoLuong.ToString();
                                                            break;
                                                        case 47:
                                                            n.SL4801 = m.SoLuong.ToString();
                                                            break;
                                                        case 48:
                                                            n.SL4901 = m.SoLuong.ToString();
                                                            break;
                                                        case 49:
                                                            n.SL5001 = m.SoLuong.ToString();
                                                            break;
                                                        case 50:
                                                            n.SL5101 = m.SoLuong.ToString();
                                                            break;
                                                        case 51:
                                                            n.SL5201 = m.SoLuong.ToString();
                                                            break;
                                                        case 52:
                                                            n.SL5301 = m.SoLuong.ToString();
                                                            break;
                                                        case 53:
                                                            n.SL5401 = m.SoLuong.ToString();
                                                            break;
                                                        case 54:
                                                            n.SL5501 = m.SoLuong.ToString();
                                                            break;
                                                        case 55:
                                                            n.SL5601 = m.SoLuong.ToString();
                                                            break;
                                                        case 56:
                                                            n.SL5701 = m.SoLuong.ToString();
                                                            break;
                                                        case 57:
                                                            n.SL5801 = m.SoLuong.ToString();
                                                            break;
                                                        case 58:
                                                            n.SL5901 = m.SoLuong.ToString();
                                                            break;
                                                        case 59:
                                                            n.SL6001 = m.SoLuong.ToString();
                                                            break;
                                                        case 60:
                                                            n.SL6101 = m.SoLuong.ToString();
                                                            break;
                                                        case 61:
                                                            n.SL6201 = m.SoLuong.ToString();
                                                            break;
                                                        case 62:
                                                            n.SL6301 = m.SoLuong.ToString();
                                                            break;
                                                        case 63:
                                                            n.SL6401 = m.SoLuong.ToString();
                                                            break;
                                                        case 64:
                                                            n.SL6501 = m.SoLuong.ToString();
                                                            break;
                                                        case 65:
                                                            n.SL6601 = m.SoLuong.ToString();
                                                            break;
                                                        case 66:
                                                            n.SL6701 = m.SoLuong.ToString();
                                                            break;
                                                        case 67:
                                                            n.SL6801 = m.SoLuong.ToString();
                                                            break;
                                                        case 68:
                                                            n.SL6901 = m.SoLuong.ToString();
                                                            break;
                                                        case 69:
                                                            n.SL7001 = m.SoLuong.ToString();
                                                            break;
                                                        case 70:
                                                            n.SL7101 = m.SoLuong.ToString();
                                                            break;
                                                        case 71:
                                                            n.SL7201 = m.SoLuong.ToString();
                                                            break;
                                                        case 72:
                                                            n.SL7301 = m.SoLuong.ToString();
                                                            break;
                                                        case 73:
                                                            n.SL7401 = m.SoLuong.ToString();
                                                            break;
                                                        case 74:
                                                            n.SL7501 = m.SoLuong.ToString();
                                                            break;
                                                        case 75:
                                                            n.SL7601 = m.SoLuong.ToString();
                                                            break;
                                                        case 76:
                                                            n.SL7701 = m.SoLuong.ToString();
                                                            break;
                                                        case 77:
                                                            n.SL7801 = m.SoLuong.ToString();
                                                            break;
                                                        case 78:
                                                            n.SL7901 = m.SoLuong.ToString();
                                                            break;
                                                        case 79:
                                                            n.SL8001 = m.SoLuong.ToString();
                                                            break;
                                                        case 80:
                                                            n.SL8101 = m.SoLuong.ToString();
                                                            break;
                                                        case 81:
                                                            n.SL8201 = m.SoLuong.ToString();
                                                            break;
                                                        case 82:
                                                            n.SL8301 = m.SoLuong.ToString();
                                                            break;
                                                        case 83:
                                                            n.SL8401 = m.SoLuong.ToString();
                                                            break;
                                                        case 84:
                                                            n.SL8501 = m.SoLuong.ToString();
                                                            break;
                                                        case 85:
                                                            n.SL8601 = m.SoLuong.ToString();
                                                            break;
                                                        case 86:
                                                            n.SL8701 = m.SoLuong.ToString();
                                                            break;
                                                        case 87:
                                                            n.SL8801 = m.SoLuong.ToString();
                                                            break;
                                                        case 88:
                                                            n.SL8901 = m.SoLuong.ToString();
                                                            break;
                                                        case 89:
                                                            n.SL9001 = m.SoLuong.ToString();
                                                            break;
                                                        case 90:
                                                            n.SL9101 = m.SoLuong.ToString();
                                                            break;
                                                        case 91:
                                                            n.SL9201 = m.SoLuong.ToString();
                                                            break;
                                                        case 92:
                                                            n.SL9301 = m.SoLuong.ToString();
                                                            break;
                                                        case 93:
                                                            n.SL9401 = m.SoLuong.ToString();
                                                            break;
                                                        case 94:
                                                            n.SL9501 = m.SoLuong.ToString();
                                                            break;
                                                        case 95:
                                                            n.SL9601 = m.SoLuong.ToString();
                                                            break;
                                                        case 96:
                                                            n.SL9701 = m.SoLuong.ToString();
                                                            break;
                                                        case 97:
                                                            n.SL9801 = m.SoLuong.ToString();
                                                            break;
                                                        case 98:
                                                            n.SL9901 = m.SoLuong.ToString();
                                                            break;
                                                    }
                                                    n.tenbn = m.TenBN;
                                                    n.Tuoi = m.Tuoi.Value;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            #endregion tạo code báo cáo
                            if (_DSDV.Count >= 100)
                            {
                                DialogResult _result = MessageBox.Show("Mẫu A4 không đủ để hiển thị, bạn có muốn in mẫu A3 không?", "Hỏi in", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                if (_result == DialogResult.Yes)
                                {
                                    #region tạo báo cáo a3
                                    if (_DSDV.Count > 54)
                                    {
                                        BaoCao.Rep_TamTraThuocA3 rep = new BaoCao.Rep_TamTraThuocA3();
                                        BaoCao.Rep_TamTraThuocA302 rep1 = new BaoCao.Rep_TamTraThuocA302();
                                        MessageBox.Show("Mẫu báo cáo in thành 2 phần thoát phần 1 để lấy BC phần 2");
                                        frmIn frm = new frmIn();
                                        for (int i = 0; i < _DSDV.Count; i++)
                                        {
                                            switch (i)
                                            {
                                                case 0:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T1.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 1:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T2.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 2:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T3.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 3:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T4.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 4:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T5.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 5:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T6.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 6:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T7.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 7:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T8.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 8:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T9.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 9:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T10.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 10:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T11.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 11:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T12.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 12:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T13.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 13:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T14.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 14:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T15.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 15:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T16.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 16:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T17.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 17:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T18.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 18:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T19.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 19:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T20.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 20:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T21.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 21:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T22.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 22:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T23.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 23:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T24.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 24:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T25.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 25:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T26.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 26:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T27.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 27:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T28.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 28:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T29.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 29:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T30.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 30:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T31.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 31:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T32.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 32:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T33.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 33:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T34.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 34:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T35.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 35:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T36.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 36:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T37.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 37:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T38.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 38:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T39.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 39:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T40.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 40:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T41.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 41:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T42.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 42:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T43.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 43:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T44.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 44:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T45.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 45:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T46.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 46:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T47.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 47:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T48.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 48:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T49.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 49:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T50.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 50:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T51.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 51:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T52.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 52:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T53.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 53:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T54.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;

                                            }
                                        }


                                        rep.Ngaythang.Value = " Từ ngày: " + LupNgayTu.Text.Substring(0, 10) + " đến ngày: " + LupNgayDen.Text.Substring(0, 10);
                                        rep.KhoaPhong.Value = LupKhoaPhong.Text;
                                        rep.DataSource = _Tamtra;
                                        rep.BindingData();
                                        rep.CreateDocument();
                                        //rep.DataMember = "Table";
                                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                        frm.ShowDialog();
                                    }
                                    #endregion tạo báo cáo a3
                                }
                                else
                                {

                                    #region tạo báo cáo A4 1 trang
                                    if (_DSDV.Count <= 46)
                                    {
                                        BaoCao.Rep_TamTraThuoc rep = new BaoCao.Rep_TamTraThuoc();
                                        frmIn frm = new frmIn();
                                        for (int i = 0; i < _DSDV.Count; i++)
                                        {
                                            switch (i)
                                            {
                                                case 0:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T1.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 1:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T2.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 2:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T3.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 3:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T4.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 4:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T5.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 5:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T6.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 6:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T7.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 7:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T8.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 8:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T9.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 9:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T10.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 10:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T11.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 11:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T12.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 12:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T13.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 13:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T14.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 14:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T15.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 15:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T16.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 16:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T17.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 17:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T18.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 18:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T19.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 19:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T20.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 20:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T21.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 21:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T22.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 22:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T23.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 23:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T24.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 24:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T25.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 25:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T26.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 26:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T27.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 27:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T28.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 28:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T29.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 29:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T30.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 30:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T31.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 31:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T32.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 32:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T33.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 33:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T34.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 34:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T35.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 35:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T36.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 36:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T37.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 37:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T38.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 38:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T39.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 39:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T40.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 40:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T41.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 41:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T42.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 42:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T43.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 43:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T44.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 44:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T45.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 45:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T46.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;


                                            }
                                        }

                                        rep.Ngaythang.Value = " Từ ngày: " + LupNgayTu.Text.Substring(0, 10) + " đến ngày: " + LupNgayDen.Text.Substring(0, 10);
                                        rep.Khoaphong.Value = LupKhoaPhong.Text;
                                        rep.DataSource = _Tamtra;
                                        rep.BindingData();
                                        rep.CreateDocument();
                                        //rep.DataMember = "Table";
                                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                        frm.ShowDialog();

                                    }
                                    #endregion tạo báo cáo a4
                                    #region tạo báo cáo A4 2 trang
                                    if (_DSDV.Count > 46)
                                    {

                                        BaoCao.Rep_TamTraThuoc rep = new BaoCao.Rep_TamTraThuoc();
                                        frmIn frm = new frmIn();
                                        for (int i = 0; i < _DSDV.Count; i++)
                                        {
                                            switch (i)
                                            {
                                                case 0:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T1.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 1:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T2.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 2:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T3.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 3:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T4.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 4:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T5.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 5:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T6.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 6:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T7.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 7:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T8.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 8:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T9.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 9:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T10.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 10:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T11.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 11:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T12.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 12:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T13.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 13:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T14.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 14:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T15.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 15:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T16.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 16:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T17.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 17:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T18.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 18:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T19.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 19:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T20.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 20:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T21.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 21:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T22.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 22:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T23.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 23:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T24.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 24:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T25.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 25:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T26.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 26:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T27.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 27:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T28.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 28:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T29.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 29:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T30.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 30:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T31.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 31:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T32.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 32:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T33.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 33:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T34.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 34:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T35.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 35:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T36.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 36:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T37.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 37:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T38.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 38:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T39.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 39:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T40.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 40:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T41.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 41:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T42.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 42:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T43.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 43:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T44.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 44:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T45.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 45:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T46.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;


                                            }
                                        }

                                        rep.Ngaythang.Value = " Từ ngày: " + LupNgayTu.Text.Substring(0, 10) + " đến ngày: " + LupNgayDen.Text.Substring(0, 10);
                                        rep.Khoaphong.Value = LupKhoaPhong.Text;
                                        rep.DataSource = _Tamtra;
                                        rep.BindingData();
                                        rep.CreateDocument();
                                        //rep.DataMember = "Table";
                                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                        frm.ShowDialog();

                                        // tạo báo cáo thứ 2

                                        BaoCao.Rep_Tamtrathuoc02 rep1 = new BaoCao.Rep_Tamtrathuoc02();
                                        string a = "Kiểu đơn: ";
                                        if (chkBoxung.Checked == true)
                                        { a += "bổ xung"; }
                                        if (chkThuongxuyen.Checked == true)
                                        {
                                            if (string.IsNullOrEmpty(a))
                                            {
                                                a += " thường xuyên ";
                                            }
                                            else
                                            { a = a + " - " + " thường xuyên "; }
                                        }
                                        if (chkTrathuoc.Checked == true)
                                        {
                                            if (string.IsNullOrEmpty(a))
                                            {
                                                a += " trả thuốc ";
                                            }
                                            else
                                            { a = a + " - " + " trả thuốc "; }
                                        }
                                        rep1.Kieudon.Value = a;
                                        frmIn frm1 = new frmIn();
                                        for (int i = 46; i < _DSDV.Count; i++)
                                        {
                                            switch (i)
                                            {
                                                case 46:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T1.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 47:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T2.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 48:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T3.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 49:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T4.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 50:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T5.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 51:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T6.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 52:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T7.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 53:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T8.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 54:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T9.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 55:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T10.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 56:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T11.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 57:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T12.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 58:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T13.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 59:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T14.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 60:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T15.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 61:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T16.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 62:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T17.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 63:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T18.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 64:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T19.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 65:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T20.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 66:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T21.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 67:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T22.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 68:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T23.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 69:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T24.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 70:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T25.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 71:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T26.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 72:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T27.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 73:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T28.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 74:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T29.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 75:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T30.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 76:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T31.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 77:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T32.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 78:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T33.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 79:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T34.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 80:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T35.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 81:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T36.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 82:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T37.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 83:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T38.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 84:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T39.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 85:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T40.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 86:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T41.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 87:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T42.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 88:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T43.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 89:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T44.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 90:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T45.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 91:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T46.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 92:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T47.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 93:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T48.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 94:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T49.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 95:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T50.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 96:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T51.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 97:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T52.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 98:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T53.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;

                                            }
                                        }

                                        rep1.Ngaythang.Value = " Từ ngày: " + LupNgayTu.Text.Substring(0, 10) + " đến ngày: " + LupNgayDen.Text.Substring(0, 10);
                                        rep1.Khoaphong.Value = LupKhoaPhong.Text;
                                        rep.DataSource = _Tamtra;
                                        rep1.BindingData();
                                        rep1.CreateDocument();
                                        //rep.DataMember = "Table";
                                        frm1.prcIN.PrintingSystem = rep1.PrintingSystem;
                                        frm1.ShowDialog();
                                    }
                                    #endregion tạo báo cáo A4 2 trang
                                }
                            }
                            else
                            {
                                #region tạo báo cáo A4 1 trang
                                if (_DSDV.Count <= 46)
                                {
                                    BaoCao.Rep_TamTraThuoc rep = new BaoCao.Rep_TamTraThuoc();
                                    frmIn frm = new frmIn();
                                    for (int i = 0; i < _DSDV.Count; i++)
                                    {
                                        switch (i)
                                        {
                                            case 0:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T1.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 1:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T2.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 2:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T3.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 3:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T4.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 4:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T5.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 5:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T6.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 6:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T7.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 7:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T8.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 8:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T9.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 9:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T10.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 10:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T11.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 11:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T12.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 12:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T13.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 13:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T14.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 14:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T15.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 15:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T16.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 16:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T17.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 17:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T18.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 18:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T19.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 19:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T20.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 20:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T21.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 21:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T22.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 22:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T23.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 23:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T24.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 24:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T25.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 25:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T26.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 26:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T27.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 27:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T28.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 28:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T29.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 29:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T30.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 30:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T31.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 31:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T32.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 32:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T33.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 33:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T34.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 34:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T35.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 35:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T36.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 36:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T37.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 37:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T38.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 38:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T39.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 39:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T40.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 40:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T41.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 41:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T42.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 42:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T43.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 43:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T44.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 44:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T45.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 45:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T46.Value = _DSDV.Skip(i).First().tendv; }
                                                break;


                                        }
                                    }

                                    rep.Ngaythang.Value = " Từ ngày: " + LupNgayTu.Text.Substring(0, 10) + " đến ngày: " + LupNgayDen.Text.Substring(0, 10);
                                    rep.Khoaphong.Value = LupKhoaPhong.Text;
                                    rep.DataSource = _Tamtra;
                                    rep.BindingData();
                                    rep.CreateDocument();
                                    //rep.DataMember = "Table";
                                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                    frm.ShowDialog();

                                }
                                #endregion tạo báo cáo a4
                                #region tạo báo cáo A4 2 trang
                                if (_DSDV.Count > 46)
                                {

                                    BaoCao.Rep_TamTraThuoc rep = new BaoCao.Rep_TamTraThuoc();
                                    frmIn frm = new frmIn();
                                    for (int i = 0; i < _DSDV.Count; i++)
                                    {
                                        switch (i)
                                        {
                                            case 0:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T1.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 1:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T2.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 2:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T3.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 3:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T4.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 4:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T5.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 5:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T6.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 6:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T7.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 7:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T8.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 8:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T9.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 9:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T10.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 10:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T11.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 11:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T12.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 12:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T13.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 13:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T14.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 14:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T15.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 15:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T16.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 16:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T17.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 17:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T18.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 18:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T19.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 19:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T20.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 20:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T21.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 21:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T22.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 22:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T23.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 23:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T24.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 24:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T25.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 25:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T26.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 26:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T27.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 27:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T28.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 28:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T29.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 29:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T30.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 30:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T31.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 31:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T32.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 32:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T33.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 33:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T34.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 34:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T35.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 35:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T36.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 36:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T37.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 37:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T38.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 38:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T39.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 39:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T40.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 40:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T41.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 41:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T42.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 42:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T43.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 43:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T44.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 44:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T45.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 45:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T46.Value = _DSDV.Skip(i).First().tendv; }
                                                break;


                                        }
                                    }

                                    rep.Ngaythang.Value = " Từ ngày: " + LupNgayTu.Text.Substring(0, 10) + " đến ngày: " + LupNgayDen.Text.Substring(0, 10);
                                    rep.Khoaphong.Value = LupKhoaPhong.Text;
                                    rep.DataSource = _Tamtra;
                                    rep.BindingData();
                                    rep.CreateDocument();
                                    //rep.DataMember = "Table";
                                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                    frm.ShowDialog();

                                    // tạo báo cáo thứ 2

                                    BaoCao.Rep_Tamtrathuoc02 rep1 = new BaoCao.Rep_Tamtrathuoc02();
                                    string a = "Kiểu đơn: ";
                                    if (chkBoxung.Checked == true)
                                    { a += "bổ xung"; }
                                    if (chkThuongxuyen.Checked == true)
                                    {
                                        if (string.IsNullOrEmpty(a))
                                        {
                                            a += " thường xuyên ";
                                        }
                                        else
                                        { a = a + " - " + " thường xuyên "; }
                                    }
                                    if (chkTrathuoc.Checked == true)
                                    {
                                        if (string.IsNullOrEmpty(a))
                                        {
                                            a += " trả thuốc ";
                                        }
                                        else
                                        { a = a + " - " + " trả thuốc "; }
                                    }
                                    rep1.Kieudon.Value = a;
                                    frmIn frm1 = new frmIn();
                                    for (int i = 46; i < _DSDV.Count; i++)
                                    {
                                        switch (i)
                                        {
                                            case 46:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T1.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 47:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T2.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 48:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T3.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 49:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T4.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 50:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T5.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 51:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T6.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 52:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T7.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 53:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T8.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 54:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T9.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 55:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T10.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 56:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T11.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 57:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T12.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 58:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T13.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 59:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T14.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 60:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T15.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 61:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T16.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 62:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T17.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 63:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T18.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 64:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T19.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 65:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T20.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 66:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T21.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 67:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T22.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 68:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T23.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 69:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T24.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 70:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T25.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 71:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T26.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 72:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T27.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 73:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T28.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 74:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T29.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 75:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T30.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 76:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T31.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 77:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T32.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 78:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T33.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 79:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T34.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 80:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T35.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 81:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T36.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 82:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T37.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 83:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T38.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 84:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T39.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 85:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T40.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 86:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T41.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 87:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T42.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 88:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T43.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 89:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T44.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 90:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T45.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 91:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T46.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 92:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T47.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 93:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T48.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 94:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T49.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 95:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T50.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 96:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T51.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 97:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T52.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 98:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T53.Value = _DSDV.Skip(i).First().tendv; }
                                                break;

                                        }
                                    }

                                    rep1.Ngaythang.Value = " Từ ngày: " + LupNgayTu.Text.Substring(0, 10) + " đến ngày: " + LupNgayDen.Text.Substring(0, 10);
                                    rep1.Khoaphong.Value = LupKhoaPhong.Text;
                                    rep.DataSource = _Tamtra;
                                    rep1.BindingData();
                                    rep1.CreateDocument();
                                    //rep.DataMember = "Table";
                                    frm1.prcIN.PrintingSystem = rep1.PrintingSystem;
                                    frm1.ShowDialog();
                                }
                                #endregion tạo báo cáo A4 2 trang
                            }
                        }
                    }
                    else
                    {

                        var bn2 = (from bn1 in _Data.BenhNhans.Where(p => p.NoiTru == 1)
                                   join DT in _Data.DThuocs.Where(p => p.KieuDon == _a1 || p.KieuDon == _a2 || p.KieuDon == _a3).Where(p => p.MaKP == _MaKP) on bn1.MaBNhan equals DT.MaBNhan
                                   join DTct in _Data.DThuoccts.Where(p => p.TrongBH == 1) on DT.IDDon equals DTct.IDDon
                                   join DV in _Data.DichVus.Where(P => P.PLoai == 1) on DTct.MaDV equals DV.MaDV
                                   join Ndv in _Data.NhomDVs on DV.IDNhom equals Ndv.IDNhom
                                   where (DT.NgayKe >= Ngaytu && DT.NgayKe <= Ngayden)
                                   //where (DT.Status == _Status1)
                                   where (DTct.Status != null && (DTct.Status == _Status1 || DTct.Status == _Status2))
                                   group new { DV, Ndv } by new { DV.MaDV, DV.TenDV, Ndv.TenNhomCT } into kq
                                   select new
                                   {
                                       TenN = kq.Key.TenNhomCT,
                                       TenDV = kq.Key.TenDV,
                                       MaDV = kq.Key.MaDV,
                                   }).OrderBy(p => p.TenDV).OrderBy(p => p.TenN).ToList();
                        if (bn2.Count > 0)
                        {
                            foreach (var a in bn2)
                            {
                                DSDV themmoi = new DSDV();
                                themmoi.tendv = a.TenDV;
                                themmoi.madv = a.MaDV;
                                _DSDV.Add(themmoi);
                            }
                        }

                        var MaBN = (from bn3 in _Data.BenhNhans.Where(p => p.NoiTru == 1)
                                    join DT in _Data.DThuocs.Where(p => p.KieuDon == _a1 || p.KieuDon == _a2 || p.KieuDon == _a3).Where(p => p.MaKP == _MaKP) on bn3.MaBNhan equals DT.MaBNhan
                                    join DTct in _Data.DThuoccts.Where(p => p.TrongBH == 1) on DT.IDDon equals DTct.IDDon
                                    join dv in _Data.DichVus.Where(p => p.PLoai == 1) on DTct.MaDV equals dv.MaDV
                                    where (DT.NgayKe >= Ngaytu && DT.NgayKe <= Ngayden)
                                    //where (DT.Status == _Status1)
                                    where (DTct.Status == _Status1 || DTct.Status == _Status2)
                                    group new { bn3 } by new { bn3.MaBNhan } into kq
                                    select new { MaBN = kq.Key.MaBNhan }).ToList();
                        if (MaBN.Count > 0)
                        {
                            foreach (var c in MaBN)
                            {
                                Tamtra themmoi = new Tamtra();
                                themmoi.mabn = c.MaBN;
                                _Tamtra.Add(themmoi);
                            }
                        }
                        var bn = (from bn1 in _Data.BenhNhans.Where(p => p.NoiTru == 1)
                                  join DT in _Data.DThuocs.Where(p => p.KieuDon == _a1 || p.KieuDon == _a2 || p.KieuDon == _a3).Where(p => p.MaKP == _MaKP) on bn1.MaBNhan equals DT.MaBNhan
                                  join DTct in _Data.DThuoccts.Where(p => p.TrongBH == 1) on DT.IDDon equals DTct.IDDon
                                  join DV in _Data.DichVus.Where(p => p.PLoai == 1) on DTct.MaDV equals DV.MaDV
                                  where (DT.NgayKe >= Ngaytu && DT.NgayKe <= Ngayden)
                                  //where (DT.Status == _Status1)
                                  where (DTct.Status == _Status1 || DTct.Status == _Status2)
                                  group new { bn1, DTct } by new { DV.MaDV, bn1.TenBNhan, bn1.MaBNhan, bn1.Tuoi } into kq
                                  select new
                                  {
                                      kq.Key.Tuoi,
                                      Mabenhnhan = kq.Key.MaBNhan,
                                      MaDV = kq.Key.MaDV,
                                      SoLuong = kq.Sum(p => p.DTct.SoLuong),
                                      TenBN = kq.Key.TenBNhan,
                                  }).ToList();
                        if (bn.Count > 0)
                        {
                            #region tạo báo cáo
                            foreach (var n in _Tamtra)
                            {
                                foreach (var m in bn)
                                {
                                    if (n.mabn == m.Mabenhnhan)
                                    {
                                        if (m.SoLuong != null && m.SoLuong != 0)
                                        {
                                            for (int i = 0; i < _DSDV.Count; i++)
                                            {
                                                if (m.MaDV == _DSDV.Skip(i).First().madv)
                                                {
                                                    switch (i)
                                                    {
                                                        case 0:
                                                            n.SL101 = m.SoLuong.ToString();
                                                            break;
                                                        case 1:
                                                            n.SL201 = m.SoLuong.ToString();
                                                            break;
                                                        case 2:
                                                            n.SL301 = m.SoLuong.ToString();
                                                            break;
                                                        case 3:
                                                            n.SL401 = m.SoLuong.ToString();
                                                            break;
                                                        case 4:
                                                            n.SL501 = m.SoLuong.ToString();
                                                            break;
                                                        case 5:
                                                            n.SL601 = m.SoLuong.ToString();
                                                            break;
                                                        case 6:
                                                            n.SL701 = m.SoLuong.ToString();
                                                            break;
                                                        case 7:
                                                            n.SL801 = m.SoLuong.ToString();
                                                            break;
                                                        case 8:
                                                            n.SL901 = m.SoLuong.ToString();
                                                            break;
                                                        case 9:
                                                            n.SL1001 = m.SoLuong.ToString();
                                                            break;
                                                        case 10:
                                                            n.SL1101 = m.SoLuong.ToString();
                                                            break;
                                                        case 11:
                                                            n.SL1201 = m.SoLuong.ToString();
                                                            break;
                                                        case 12:
                                                            n.SL1301 = m.SoLuong.ToString();
                                                            break;
                                                        case 13:
                                                            n.SL1401 = m.SoLuong.ToString();
                                                            break;
                                                        case 14:
                                                            n.SL1501 = m.SoLuong.ToString();
                                                            break;
                                                        case 15:
                                                            n.SL1601 = m.SoLuong.ToString();
                                                            break;
                                                        case 16:
                                                            n.SL1701 = m.SoLuong.ToString();
                                                            break;
                                                        case 17:
                                                            n.SL1801 = m.SoLuong.ToString();
                                                            break;
                                                        case 18:
                                                            n.SL1901 = m.SoLuong.ToString();
                                                            break;
                                                        case 19:
                                                            n.SL2001 = m.SoLuong.ToString();
                                                            break;
                                                        case 20:
                                                            n.SL2101 = m.SoLuong.ToString();
                                                            break;
                                                        case 21:
                                                            n.SL2201 = m.SoLuong.ToString();
                                                            break;
                                                        case 22:
                                                            n.SL2301 = m.SoLuong.ToString();
                                                            break;
                                                        case 23:
                                                            n.SL2401 = m.SoLuong.ToString();
                                                            break;
                                                        case 24:
                                                            n.SL2501 = m.SoLuong.ToString();
                                                            break;
                                                        case 25:
                                                            n.SL2601 = m.SoLuong.ToString();
                                                            break;
                                                        case 26:
                                                            n.SL2701 = m.SoLuong.ToString();
                                                            break;
                                                        case 27:
                                                            n.SL2801 = m.SoLuong.ToString();
                                                            break;
                                                        case 28:
                                                            n.SL2901 = m.SoLuong.ToString();
                                                            break;
                                                        case 29:
                                                            n.SL3001 = m.SoLuong.ToString();
                                                            break;
                                                        case 30:
                                                            n.SL3101 = m.SoLuong.ToString();
                                                            break;
                                                        case 31:
                                                            n.SL3201 = m.SoLuong.ToString();
                                                            break;
                                                        case 32:
                                                            n.SL3301 = m.SoLuong.ToString();
                                                            break;
                                                        case 33:
                                                            n.SL3401 = m.SoLuong.ToString();
                                                            break;
                                                        case 34:
                                                            n.SL3501 = m.SoLuong.ToString();
                                                            break;
                                                        case 35:
                                                            n.SL3601 = m.SoLuong.ToString();
                                                            break;
                                                        case 36:
                                                            n.SL3701 = m.SoLuong.ToString();
                                                            break;
                                                        case 37:
                                                            n.SL3801 = m.SoLuong.ToString();
                                                            break;
                                                        case 38:
                                                            n.SL3901 = m.SoLuong.ToString();
                                                            break;
                                                        case 39:
                                                            n.SL4001 = m.SoLuong.ToString();
                                                            break;
                                                        case 40:
                                                            n.SL4101 = m.SoLuong.ToString();
                                                            break;
                                                        case 41:
                                                            n.SL4201 = m.SoLuong.ToString();
                                                            break;
                                                        case 42:
                                                            n.SL4301 = m.SoLuong.ToString();
                                                            break;
                                                        case 43:
                                                            n.SL4401 = m.SoLuong.ToString();
                                                            break;
                                                        case 44:
                                                            n.SL4501 = m.SoLuong.ToString();
                                                            break;
                                                        case 45:
                                                            n.SL4601 = m.SoLuong.ToString();
                                                            break;
                                                        case 46:
                                                            n.SL4701 = m.SoLuong.ToString();
                                                            break;
                                                        case 47:
                                                            n.SL4801 = m.SoLuong.ToString();
                                                            break;
                                                        case 48:
                                                            n.SL4901 = m.SoLuong.ToString();
                                                            break;
                                                        case 49:
                                                            n.SL5001 = m.SoLuong.ToString();
                                                            break;
                                                        case 50:
                                                            n.SL5101 = m.SoLuong.ToString();
                                                            break;
                                                        case 51:
                                                            n.SL5201 = m.SoLuong.ToString();
                                                            break;
                                                        case 52:
                                                            n.SL5301 = m.SoLuong.ToString();
                                                            break;
                                                        case 53:
                                                            n.SL5401 = m.SoLuong.ToString();
                                                            break;
                                                        case 54:
                                                            n.SL5501 = m.SoLuong.ToString();
                                                            break;
                                                        case 55:
                                                            n.SL5601 = m.SoLuong.ToString();
                                                            break;
                                                        case 56:
                                                            n.SL5701 = m.SoLuong.ToString();
                                                            break;
                                                        case 57:
                                                            n.SL5801 = m.SoLuong.ToString();
                                                            break;
                                                        case 58:
                                                            n.SL5901 = m.SoLuong.ToString();
                                                            break;
                                                        case 59:
                                                            n.SL6001 = m.SoLuong.ToString();
                                                            break;
                                                        case 60:
                                                            n.SL6101 = m.SoLuong.ToString();
                                                            break;
                                                        case 61:
                                                            n.SL6201 = m.SoLuong.ToString();
                                                            break;
                                                        case 62:
                                                            n.SL6301 = m.SoLuong.ToString();
                                                            break;
                                                        case 63:
                                                            n.SL6401 = m.SoLuong.ToString();
                                                            break;
                                                        case 64:
                                                            n.SL6501 = m.SoLuong.ToString();
                                                            break;
                                                        case 65:
                                                            n.SL6601 = m.SoLuong.ToString();
                                                            break;
                                                        case 66:
                                                            n.SL6701 = m.SoLuong.ToString();
                                                            break;
                                                        case 67:
                                                            n.SL6801 = m.SoLuong.ToString();
                                                            break;
                                                        case 68:
                                                            n.SL6901 = m.SoLuong.ToString();
                                                            break;
                                                        case 69:
                                                            n.SL7001 = m.SoLuong.ToString();
                                                            break;
                                                        case 70:
                                                            n.SL7101 = m.SoLuong.ToString();
                                                            break;
                                                        case 71:
                                                            n.SL7201 = m.SoLuong.ToString();
                                                            break;
                                                        case 72:
                                                            n.SL7301 = m.SoLuong.ToString();
                                                            break;
                                                        case 73:
                                                            n.SL7401 = m.SoLuong.ToString();
                                                            break;
                                                        case 74:
                                                            n.SL7501 = m.SoLuong.ToString();
                                                            break;
                                                        case 75:
                                                            n.SL7601 = m.SoLuong.ToString();
                                                            break;
                                                        case 76:
                                                            n.SL7701 = m.SoLuong.ToString();
                                                            break;
                                                        case 77:
                                                            n.SL7801 = m.SoLuong.ToString();
                                                            break;
                                                        case 78:
                                                            n.SL7901 = m.SoLuong.ToString();
                                                            break;
                                                        case 79:
                                                            n.SL8001 = m.SoLuong.ToString();
                                                            break;
                                                        case 80:
                                                            n.SL8101 = m.SoLuong.ToString();
                                                            break;
                                                        case 81:
                                                            n.SL8201 = m.SoLuong.ToString();
                                                            break;
                                                        case 82:
                                                            n.SL8301 = m.SoLuong.ToString();
                                                            break;
                                                        case 83:
                                                            n.SL8401 = m.SoLuong.ToString();
                                                            break;
                                                        case 84:
                                                            n.SL8501 = m.SoLuong.ToString();
                                                            break;
                                                        case 85:
                                                            n.SL8601 = m.SoLuong.ToString();
                                                            break;
                                                        case 86:
                                                            n.SL8701 = m.SoLuong.ToString();
                                                            break;
                                                        case 87:
                                                            n.SL8801 = m.SoLuong.ToString();
                                                            break;
                                                        case 88:
                                                            n.SL8901 = m.SoLuong.ToString();
                                                            break;
                                                        case 89:
                                                            n.SL9001 = m.SoLuong.ToString();
                                                            break;
                                                        case 90:
                                                            n.SL9101 = m.SoLuong.ToString();
                                                            break;
                                                        case 91:
                                                            n.SL9201 = m.SoLuong.ToString();
                                                            break;
                                                        case 92:
                                                            n.SL9301 = m.SoLuong.ToString();
                                                            break;
                                                        case 93:
                                                            n.SL9401 = m.SoLuong.ToString();
                                                            break;
                                                        case 94:
                                                            n.SL9501 = m.SoLuong.ToString();
                                                            break;
                                                        case 95:
                                                            n.SL9601 = m.SoLuong.ToString();
                                                            break;
                                                        case 96:
                                                            n.SL9701 = m.SoLuong.ToString();
                                                            break;
                                                        case 97:
                                                            n.SL9801 = m.SoLuong.ToString();
                                                            break;
                                                        case 98:
                                                            n.SL9901 = m.SoLuong.ToString();
                                                            break;
                                                    }
                                                    n.tenbn = m.TenBN;
                                                    n.Tuoi = m.Tuoi.Value;
                                                }
                                            }
                                        }
                                    }
                                }

                            }
                            #endregion tạo báo cáo
                            if (_DSDV.Count >= 100)
                            {
                                DialogResult _result = MessageBox.Show("Mẫu A4 không đủ để hiển thị, bạn có muốn in mẫu A3 không?", "Hỏi in", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                                if (_result == DialogResult.Yes)
                                #region tạo báo cáo A3
                                {

                                    BaoCao.Rep_TamTraThuocA3 rep = new BaoCao.Rep_TamTraThuocA3();
                                    frmIn frm = new frmIn();
                                    for (int i = 0; i < _DSDV.Count; i++)
                                    {
                                        switch (i)
                                        {
                                            case 0:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T1.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 1:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T2.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 2:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T3.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 3:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T4.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 4:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T5.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 5:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T6.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 6:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T7.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 7:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T8.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 8:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T9.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 9:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T10.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 10:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T11.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 11:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T12.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 12:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T13.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 13:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T14.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 14:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T15.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 15:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T16.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 16:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T17.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 17:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T18.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 18:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T19.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 19:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T20.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 20:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T21.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 21:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T22.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 22:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T23.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 23:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T24.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 24:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T25.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 25:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T26.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 26:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T27.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 27:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T28.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 28:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T29.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 29:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T30.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 30:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T31.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 31:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T32.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 32:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T33.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 33:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T34.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 34:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T35.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 35:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T36.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 36:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T37.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 37:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T38.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 38:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T39.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 39:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T40.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 40:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T41.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 41:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T42.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 42:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T43.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 43:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T44.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 44:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T45.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 45:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T46.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 46:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T47.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 47:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T48.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 48:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T49.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 49:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T50.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 50:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T51.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 51:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T52.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 52:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T53.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 53:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T54.Value = _DSDV.Skip(i).First().tendv; }
                                                break;

                                        }
                                    }

                                    rep.Ngaythang.Value = " Từ ngày: " + LupNgayTu.Text.Substring(0, 10) + " đến ngày: " + LupNgayDen.Text.Substring(0, 10);
                                    rep.KhoaPhong.Value = LupKhoaPhong.Text;
                                    rep.DataSource = _Tamtra;
                                    rep.BindingData();
                                    rep.CreateDocument();
                                    //rep.DataMember = "Table";
                                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                    frm.ShowDialog();
                                }
                                #endregion tạo báo cáo A3
                                else
                                {
                                    #region tạo báo cáo A 1 trang
                                    if (_DSDV.Count <= 46)
                                    {
                                        BaoCao.Rep_TamTraThuoc rep = new BaoCao.Rep_TamTraThuoc();
                                        frmIn frm = new frmIn();
                                        string a = "Kiểu đơn: ";
                                        if (chkBoxung.Checked == true)
                                        { a += " bổ xung "; }
                                        if (chkThuongxuyen.Checked == true)
                                        {
                                            if (string.IsNullOrEmpty(a))
                                            {
                                                a += " thường xuyên ";
                                            }
                                            else
                                            { a = a + " - " + " thường xuyên "; }
                                        }
                                        if (chkTrathuoc.Checked == true)
                                        {
                                            if (string.IsNullOrEmpty(a))
                                            {
                                                a += " trả thuốc ";
                                            }
                                            else
                                            { a = a + " - " + " trả thuốc "; }
                                        }
                                        rep.Kieudon.Value = a;
                                        for (int i = 0; i < _DSDV.Count; i++)
                                        {
                                            switch (i)
                                            {
                                                case 0:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T1.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 1:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T2.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 2:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T3.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 3:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T4.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 4:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T5.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 5:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T6.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 6:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T7.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 7:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T8.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 8:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T9.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 9:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T10.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 10:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T11.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 11:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T12.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 12:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T13.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 13:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T14.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 14:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T15.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 15:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T16.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 16:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T17.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 17:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T18.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 18:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T19.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 19:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T20.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 20:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T21.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 21:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T22.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 22:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T23.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 23:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T24.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 24:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T25.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 25:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T26.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 26:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T27.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 27:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T28.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 28:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T29.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 29:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T30.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 30:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T31.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 31:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T32.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 32:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T33.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 33:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T34.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 34:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T35.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 35:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T36.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 36:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T37.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 37:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T38.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 38:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T39.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 39:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T40.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 40:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T41.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 41:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T42.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 42:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T43.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 43:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T44.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 44:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T45.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 45:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T46.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;


                                            }
                                        }

                                        rep.Ngaythang.Value = " Từ ngày: " + LupNgayTu.Text.Substring(0, 10) + " đến ngày: " + LupNgayDen.Text.Substring(0, 10);
                                        rep.Khoaphong.Value = LupKhoaPhong.Text;
                                        rep.DataSource = _Tamtra;
                                        rep.BindingData();
                                        rep.CreateDocument();
                                        //rep.DataMember = "Table";
                                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                        frm.ShowDialog();

                                    }
                                    #endregion tạo báo cáo A4 1 trang
                                    #region tạo báo cáo A4 2 trang
                                    if (_DSDV.Count > 46)
                                    {

                                        BaoCao.Rep_TamTraThuoc rep = new BaoCao.Rep_TamTraThuoc();
                                        frmIn frm = new frmIn();
                                        string a = "Kiểu đơn: ";
                                        if (chkBoxung.Checked == true)
                                        { a += " bổ xung "; }
                                        if (chkThuongxuyen.Checked == true)
                                        {
                                            if (string.IsNullOrEmpty(a))
                                            {
                                                a += " thường xuyên ";
                                            }
                                            else
                                            { a = a + " - " + " thường xuyên "; }
                                        }
                                        if (chkTrathuoc.Checked == true)
                                        {
                                            if (string.IsNullOrEmpty(a))
                                            {
                                                a += " trả thuốc ";
                                            }
                                            else
                                            { a = a + " - " + " trả thuốc "; }
                                        }
                                        rep.Kieudon.Value = a;
                                        for (int i = 0; i < _DSDV.Count; i++)
                                        {
                                            switch (i)
                                            {
                                                case 0:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T1.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 1:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T2.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 2:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T3.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 3:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T4.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 4:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T5.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 5:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T6.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 6:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T7.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 7:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T8.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 8:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T9.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 9:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T10.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 10:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T11.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 11:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T12.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 12:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T13.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 13:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T14.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 14:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T15.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 15:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T16.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 16:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T17.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 17:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T18.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 18:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T19.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 19:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T20.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 20:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T21.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 21:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T22.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 22:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T23.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 23:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T24.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 24:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T25.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 25:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T26.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 26:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T27.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 27:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T28.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 28:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T29.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 29:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T30.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 30:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T31.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 31:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T32.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 32:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T33.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 33:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T34.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 34:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T35.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 35:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T36.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 36:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T37.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 37:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T38.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 38:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T39.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 39:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T40.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 40:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T41.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 41:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T42.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 42:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T43.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 43:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T44.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 44:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T45.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 45:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T46.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;


                                            }
                                        }

                                        rep.Ngaythang.Value = " Từ ngày: " + LupNgayTu.Text.Substring(0, 10) + " đến ngày: " + LupNgayDen.Text.Substring(0, 10);
                                        rep.Khoaphong.Value = LupKhoaPhong.Text;
                                        rep.DataSource = _Tamtra;
                                        rep.BindingData();
                                        rep.CreateDocument();
                                        //rep.DataMember = "Table";
                                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                        frm.ShowDialog();

                                        // tạo báo cáo thứ 2

                                        BaoCao.Rep_Tamtrathuoc02 rep1 = new BaoCao.Rep_Tamtrathuoc02();
                                        frmIn frm1 = new frmIn();
                                        a = "Kiểu đơn: ";
                                        if (chkBoxung.Checked == true)
                                        { a += " bổ xung "; }
                                        if (chkThuongxuyen.Checked == true)
                                        {
                                            if (string.IsNullOrEmpty(a))
                                            {
                                                a += " thường xuyên ";
                                            }
                                            else
                                            { a = a + " - " + " thường xuyên "; }
                                        }
                                        if (chkTrathuoc.Checked == true)
                                        {
                                            if (string.IsNullOrEmpty(a))
                                            {
                                                a += " trả thuốc ";
                                            }
                                            else
                                            { a = a + " - " + " trả thuốc "; }
                                        }
                                        rep.Kieudon.Value = a;
                                        for (int i = 46; i < _DSDV.Count; i++)
                                        {
                                            switch (i)
                                            {
                                                case 46:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T1.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 47:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T2.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 48:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T3.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 49:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T4.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 50:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T5.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 51:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T6.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 52:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T7.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 53:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T8.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 54:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T9.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 55:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T10.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 56:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T11.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 57:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T12.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 58:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T13.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 59:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T14.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 60:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T15.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 61:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T16.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 62:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T17.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 63:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T18.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 64:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T19.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 65:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T20.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 66:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T21.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 67:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T22.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 68:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T23.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 69:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T24.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 70:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T25.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 71:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T26.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 72:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T27.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 73:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T28.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 74:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T29.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 75:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T30.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 76:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T31.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 77:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T32.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 78:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T33.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 79:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T34.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 80:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T35.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 81:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T36.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 82:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T37.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 83:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T38.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 84:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T39.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 85:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T40.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 86:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T41.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 87:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T42.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 88:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T43.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 89:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T44.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 90:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T45.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 91:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T46.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 92:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T47.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 93:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T48.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 94:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T49.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 95:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T50.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 96:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T51.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 97:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T52.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 98:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T53.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;

                                            }
                                        }

                                        rep1.Ngaythang.Value = " Từ ngày: " + LupNgayTu.Text.Substring(0, 10) + " đến ngày: " + LupNgayDen.Text.Substring(0, 10);
                                        rep1.Khoaphong.Value = LupKhoaPhong.Text;
                                        rep1.DataSource = _Tamtra;
                                        rep1.BindingData();
                                        rep1.CreateDocument();
                                        //rep.DataMember = "Table";
                                        frm1.prcIN.PrintingSystem = rep1.PrintingSystem;
                                        frm1.ShowDialog();
                                    }
                                    #endregion tạo báo cáo A4 2 trang
                                }
                            }
                            else
                            {
                                #region tạo báo cáo A 1 trang
                                if (_DSDV.Count <= 46)
                                {
                                    BaoCao.Rep_TamTraThuoc rep = new BaoCao.Rep_TamTraThuoc();
                                    frmIn frm = new frmIn();
                                    string a = "Kiểu đơn: ";
                                    if (chkBoxung.Checked == true)
                                    { a += " bổ xung "; }
                                    if (chkThuongxuyen.Checked == true)
                                    {
                                        if (string.IsNullOrEmpty(a))
                                        {
                                            a += " thường xuyên ";
                                        }
                                        else
                                        { a = a + " - " + " thường xuyên "; }
                                    }
                                    if (chkTrathuoc.Checked == true)
                                    {
                                        if (string.IsNullOrEmpty(a))
                                        {
                                            a += " trả thuốc ";
                                        }
                                        else
                                        { a = a + " - " + " trả thuốc "; }
                                    }
                                    rep.Kieudon.Value = a;
                                    for (int i = 0; i < _DSDV.Count; i++)
                                    {
                                        switch (i)
                                        {
                                            case 0:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T1.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 1:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T2.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 2:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T3.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 3:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T4.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 4:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T5.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 5:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T6.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 6:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T7.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 7:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T8.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 8:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T9.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 9:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T10.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 10:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T11.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 11:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T12.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 12:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T13.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 13:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T14.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 14:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T15.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 15:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T16.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 16:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T17.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 17:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T18.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 18:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T19.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 19:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T20.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 20:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T21.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 21:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T22.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 22:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T23.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 23:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T24.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 24:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T25.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 25:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T26.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 26:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T27.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 27:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T28.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 28:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T29.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 29:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T30.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 30:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T31.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 31:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T32.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 32:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T33.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 33:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T34.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 34:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T35.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 35:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T36.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 36:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T37.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 37:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T38.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 38:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T39.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 39:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T40.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 40:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T41.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 41:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T42.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 42:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T43.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 43:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T44.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 44:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T45.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 45:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T46.Value = _DSDV.Skip(i).First().tendv; }
                                                break;


                                        }
                                    }

                                    rep.Ngaythang.Value = " Từ ngày: " + LupNgayTu.Text.Substring(0, 10) + " đến ngày: " + LupNgayDen.Text.Substring(0, 10);
                                    rep.Khoaphong.Value = LupKhoaPhong.Text;
                                    rep.DataSource = _Tamtra;
                                    rep.BindingData();
                                    rep.CreateDocument();
                                    //rep.DataMember = "Table";
                                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                    frm.ShowDialog();

                                }
                                #endregion tạo báo cáo A4 1 trang
                                #region tạo báo cáo A4 2 trang
                                if (_DSDV.Count > 46)
                                {

                                    BaoCao.Rep_TamTraThuoc rep = new BaoCao.Rep_TamTraThuoc();
                                    frmIn frm = new frmIn();
                                    string a = "Kiểu đơn: ";
                                    if (chkBoxung.Checked == true)
                                    { a += " bổ xung "; }
                                    if (chkThuongxuyen.Checked == true)
                                    {
                                        if (string.IsNullOrEmpty(a))
                                        {
                                            a += " thường xuyên ";
                                        }
                                        else
                                        { a = a + " - " + " thường xuyên "; }
                                    }
                                    if (chkTrathuoc.Checked == true)
                                    {
                                        if (string.IsNullOrEmpty(a))
                                        {
                                            a += " trả thuốc ";
                                        }
                                        else
                                        { a = a + " - " + " trả thuốc "; }
                                    }
                                    rep.Kieudon.Value = a;
                                    for (int i = 0; i < _DSDV.Count; i++)
                                    {
                                        switch (i)
                                        {
                                            case 0:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T1.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 1:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T2.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 2:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T3.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 3:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T4.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 4:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T5.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 5:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T6.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 6:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T7.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 7:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T8.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 8:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T9.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 9:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T10.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 10:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T11.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 11:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T12.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 12:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T13.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 13:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T14.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 14:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T15.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 15:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T16.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 16:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T17.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 17:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T18.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 18:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T19.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 19:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T20.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 20:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T21.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 21:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T22.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 22:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T23.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 23:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T24.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 24:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T25.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 25:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T26.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 26:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T27.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 27:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T28.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 28:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T29.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 29:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T30.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 30:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T31.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 31:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T32.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 32:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T33.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 33:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T34.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 34:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T35.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 35:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T36.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 36:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T37.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 37:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T38.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 38:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T39.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 39:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T40.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 40:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T41.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 41:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T42.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 42:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T43.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 43:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T44.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 44:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T45.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 45:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T46.Value = _DSDV.Skip(i).First().tendv; }
                                                break;


                                        }
                                    }

                                    rep.Ngaythang.Value = " Từ ngày: " + LupNgayTu.Text.Substring(0, 10) + " đến ngày: " + LupNgayDen.Text.Substring(0, 10);
                                    rep.Khoaphong.Value = LupKhoaPhong.Text;
                                    rep.DataSource = _Tamtra;
                                    rep.BindingData();
                                    rep.CreateDocument();
                                    //rep.DataMember = "Table";
                                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                    frm.ShowDialog();

                                    // tạo báo cáo thứ 2

                                    BaoCao.Rep_Tamtrathuoc02 rep1 = new BaoCao.Rep_Tamtrathuoc02();
                                    frmIn frm1 = new frmIn();
                                    a = "Kiểu đơn: ";
                                    if (chkBoxung.Checked == true)
                                    { a += " bổ xung "; }
                                    if (chkThuongxuyen.Checked == true)
                                    {
                                        if (string.IsNullOrEmpty(a))
                                        {
                                            a += " thường xuyên ";
                                        }
                                        else
                                        { a = a + " - " + " thường xuyên "; }
                                    }
                                    if (chkTrathuoc.Checked == true)
                                    {
                                        if (string.IsNullOrEmpty(a))
                                        {
                                            a += " trả thuốc ";
                                        }
                                        else
                                        { a = a + " - " + " trả thuốc "; }
                                    }
                                    rep.Kieudon.Value = a;
                                    for (int i = 46; i < _DSDV.Count; i++)
                                    {
                                        switch (i)
                                        {
                                            case 46:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T1.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 47:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T2.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 48:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T3.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 49:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T4.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 50:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T5.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 51:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T6.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 52:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T7.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 53:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T8.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 54:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T9.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 55:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T10.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 56:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T11.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 57:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T12.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 58:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T13.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 59:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T14.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 60:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T15.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 61:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T16.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 62:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T17.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 63:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T18.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 64:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T19.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 65:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T20.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 66:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T21.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 67:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T22.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 68:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T23.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 69:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T24.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 70:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T25.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 71:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T26.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 72:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T27.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 73:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T28.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 74:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T29.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 75:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T30.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 76:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T31.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 77:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T32.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 78:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T33.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 79:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T34.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 80:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T35.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 81:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T36.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 82:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T37.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 83:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T38.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 84:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T39.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 85:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T40.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 86:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T41.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 87:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T42.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 88:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T43.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 89:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T44.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 90:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T45.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 91:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T46.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 92:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T47.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 93:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T48.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 94:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T49.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 95:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T50.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 96:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T51.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 97:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T52.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 98:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T53.Value = _DSDV.Skip(i).First().tendv; }
                                                break;

                                        }
                                    }

                                    rep1.Ngaythang.Value = " Từ ngày: " + LupNgayTu.Text.Substring(0, 10) + " đến ngày: " + LupNgayDen.Text.Substring(0, 10);
                                    rep1.Khoaphong.Value = LupKhoaPhong.Text;
                                    rep1.DataSource = _Tamtra;
                                    rep1.BindingData();
                                    rep1.CreateDocument();
                                    //rep.DataMember = "Table";
                                    frm1.prcIN.PrintingSystem = rep1.PrintingSystem;
                                    frm1.ShowDialog();
                                }
                                #endregion tạo báo cáo A4 2 trang
                            }
                        }
                    }
                }
                if (radNT.SelectedIndex == 1)// điều trị ngoại trú
                {
                    int _Status1 = 0;
                    int _Status2 = 0;
                    int TT = CboThuoc.SelectedIndex;
                    switch (TT)
                    {
                        case 0:
                            _Status1 = 1;
                            _Status2 = 1;
                            break;
                        case 1:
                            _Status1 = 0;
                            _Status2 = 0;
                            break;
                        case 2:
                            _Status1 = 1;
                            _Status2 = 0;
                            break;
                    }
                    int _MaKP = 0;
                    _Tamtra.Clear();
                    _DSDV.Clear();
                    if (LupKhoaPhong.Text != null)
                    {
                        _MaKP = Convert.ToInt32( LupKhoaPhong.EditValue);
                    }
                    DateTime Ngaytu = DungChung.Ham.NgayTu(LupNgayTu.DateTime);
                    DateTime Ngayden = DungChung.Ham.NgayDen(LupNgayDen.DateTime);

                    if (chkThuoc.Checked == true)
                    {
                        var bn2 = (from bn1 in _Data.BenhNhans.Where(p => p.NoiTru == 0)
                                   join DT in _Data.DThuocs.Where(p => p.KieuDon == _a1 || p.KieuDon == _a2 || p.KieuDon == _a3).Where(p => p.MaKP == _MaKP) on bn1.MaBNhan equals DT.MaBNhan
                                   join DTct in _Data.DThuoccts on DT.IDDon equals DTct.IDDon
                                   join vv in _Data.VaoViens on bn1.MaBNhan equals vv.MaBNhan
                                   join DV in _Data.DichVus.Where(P => P.PLoai == 1) on DTct.MaDV equals DV.MaDV
                                   join Ndv in _Data.NhomDVs on DV.IDNhom equals Ndv.IDNhom
                                   where (DT.NgayKe >= Ngaytu && DT.NgayKe <= Ngayden)
                                   where (DTct.Status == _Status1 || DTct.Status == _Status2)
                                   group new { DV, Ndv } by new { DV.MaDV, DV.TenDV, Ndv.TenNhomCT } into kq
                                   select new
                                   {
                                       TenDV = kq.Key.TenDV,
                                       MaDV = kq.Key.MaDV,
                                       TenN = kq.Key.TenNhomCT
                                   }).OrderBy(p => p.TenDV).OrderBy(p => p.TenN).ToList();
                        if (bn2.Count > 0)
                        {
                            foreach (var a in bn2)
                            {
                                DSDV themmoi = new DSDV();
                                themmoi.tendv = a.TenDV;
                                themmoi.madv = a.MaDV;
                                _DSDV.Add(themmoi);
                            }
                        }

                        var MaBN = (from bn3 in _Data.BenhNhans.Where(p => p.NoiTru == 0)
                                    join DT in _Data.DThuocs.Where(p => p.KieuDon == _a1 || p.KieuDon == _a2 || p.KieuDon == _a3).Where(p => p.MaKP == _MaKP) on bn3.MaBNhan equals DT.MaBNhan
                                    join DTct in _Data.DThuoccts on DT.IDDon equals DTct.IDDon
                                    join dv in _Data.DichVus.Where(p => p.PLoai == 1) on DTct.MaDV equals dv.MaDV
                                    join vv in _Data.VaoViens on bn3.MaBNhan equals vv.MaBNhan
                                    //join TN in _Data.NhomDVs.Where(p => p.TenNhomCT.Contains("Nhóm thuốc, hoá chất")) on dv.IDNhom equals TN.IDNhom
                                    where (DT.NgayKe >= Ngaytu && DT.NgayKe <= Ngayden)
                                    where (DTct.Status == _Status1 || DTct.Status == _Status2)
                                    group new { bn3 } by new { bn3.MaBNhan } into kq
                                    select new { MaBN = kq.Key.MaBNhan }).ToList();
                        if (MaBN.Count > 0)
                        {
                            foreach (var c in MaBN)
                            {
                                Tamtra themmoi = new Tamtra();
                                themmoi.mabn = c.MaBN;
                                _Tamtra.Add(themmoi);
                            }
                        }

                        var bn = (from bn1 in _Data.BenhNhans.Where(p => p.NoiTru == 0)
                                  join DT in _Data.DThuocs.Where(p => p.KieuDon == _a1 || p.KieuDon == _a2 || p.KieuDon == _a3).Where(p => p.MaKP == _MaKP) on bn1.MaBNhan equals DT.MaBNhan
                                  join DTct in _Data.DThuoccts on DT.IDDon equals DTct.IDDon
                                  join DV in _Data.DichVus.Where(p => p.PLoai == 1) on DTct.MaDV equals DV.MaDV
                                  join vv in _Data.VaoViens on bn1.MaBNhan equals vv.MaBNhan
                                  where (DT.NgayKe >= Ngaytu && DT.NgayKe <= Ngayden)
                                  where (DTct.Status != null && (DTct.Status == _Status1 || DTct.Status == _Status2))
                                  group new { bn1, DTct } by new { DV.MaDV, bn1.TenBNhan, bn1.MaBNhan, bn1.Tuoi } into kq
                                  select new
                                  {
                                      kq.Key.Tuoi,
                                      Mabenhnhan = kq.Key.MaBNhan,
                                      MaDV = kq.Key.MaDV,
                                      SoLuong = kq.Sum(p => p.DTct.SoLuong),
                                      TenBN = kq.Key.TenBNhan,
                                  }).ToList();
                        if (bn.Count > 0)
                        {
                            #region tạo code báo cáo
                            foreach (var n in _Tamtra)
                            {
                                foreach (var m in bn)
                                {
                                    if (n.mabn == m.Mabenhnhan)
                                    {
                                        if (m.SoLuong != null && m.SoLuong != 0)
                                        {
                                            for (int i = 0; i < _DSDV.Count; i++)
                                            {
                                                if (m.MaDV == _DSDV.Skip(i).First().madv)
                                                {
                                                    switch (i)
                                                    {
                                                        case 0:
                                                            n.SL101 = m.SoLuong.ToString();
                                                            break;
                                                        case 1:
                                                            n.SL201 = m.SoLuong.ToString();
                                                            break;
                                                        case 2:
                                                            n.SL301 = m.SoLuong.ToString();
                                                            break;
                                                        case 3:
                                                            n.SL401 = m.SoLuong.ToString();
                                                            break;
                                                        case 4:
                                                            n.SL501 = m.SoLuong.ToString();
                                                            break;
                                                        case 5:
                                                            n.SL601 = m.SoLuong.ToString();
                                                            break;
                                                        case 6:
                                                            n.SL701 = m.SoLuong.ToString();
                                                            break;
                                                        case 7:
                                                            n.SL801 = m.SoLuong.ToString();
                                                            break;
                                                        case 8:
                                                            n.SL901 = m.SoLuong.ToString();
                                                            break;
                                                        case 9:
                                                            n.SL1001 = m.SoLuong.ToString();
                                                            break;
                                                        case 10:
                                                            n.SL1101 = m.SoLuong.ToString();
                                                            break;
                                                        case 11:
                                                            n.SL1201 = m.SoLuong.ToString();
                                                            break;
                                                        case 12:
                                                            n.SL1301 = m.SoLuong.ToString();
                                                            break;
                                                        case 13:
                                                            n.SL1401 = m.SoLuong.ToString();
                                                            break;
                                                        case 14:
                                                            n.SL1501 = m.SoLuong.ToString();
                                                            break;
                                                        case 15:
                                                            n.SL1601 = m.SoLuong.ToString();
                                                            break;
                                                        case 16:
                                                            n.SL1701 = m.SoLuong.ToString();
                                                            break;
                                                        case 17:
                                                            n.SL1801 = m.SoLuong.ToString();
                                                            break;
                                                        case 18:
                                                            n.SL1901 = m.SoLuong.ToString();
                                                            break;
                                                        case 19:
                                                            n.SL2001 = m.SoLuong.ToString();
                                                            break;
                                                        case 20:
                                                            n.SL2101 = m.SoLuong.ToString();
                                                            break;
                                                        case 21:
                                                            n.SL2201 = m.SoLuong.ToString();
                                                            break;
                                                        case 22:
                                                            n.SL2301 = m.SoLuong.ToString();
                                                            break;
                                                        case 23:
                                                            n.SL2401 = m.SoLuong.ToString();
                                                            break;
                                                        case 24:
                                                            n.SL2501 = m.SoLuong.ToString();
                                                            break;
                                                        case 25:
                                                            n.SL2601 = m.SoLuong.ToString();
                                                            break;
                                                        case 26:
                                                            n.SL2701 = m.SoLuong.ToString();
                                                            break;
                                                        case 27:
                                                            n.SL2801 = m.SoLuong.ToString();
                                                            break;
                                                        case 28:
                                                            n.SL2901 = m.SoLuong.ToString();
                                                            break;
                                                        case 29:
                                                            n.SL3001 = m.SoLuong.ToString();
                                                            break;
                                                        case 30:
                                                            n.SL3101 = m.SoLuong.ToString();
                                                            break;
                                                        case 31:
                                                            n.SL3201 = m.SoLuong.ToString();
                                                            break;
                                                        case 32:
                                                            n.SL3301 = m.SoLuong.ToString();
                                                            break;
                                                        case 33:
                                                            n.SL3401 = m.SoLuong.ToString();
                                                            break;
                                                        case 34:
                                                            n.SL3501 = m.SoLuong.ToString();
                                                            break;
                                                        case 35:
                                                            n.SL3601 = m.SoLuong.ToString();
                                                            break;
                                                        case 36:
                                                            n.SL3701 = m.SoLuong.ToString();
                                                            break;
                                                        case 37:
                                                            n.SL3801 = m.SoLuong.ToString();
                                                            break;
                                                        case 38:
                                                            n.SL3901 = m.SoLuong.ToString();
                                                            break;
                                                        case 39:
                                                            n.SL4001 = m.SoLuong.ToString();
                                                            break;
                                                        case 40:
                                                            n.SL4101 = m.SoLuong.ToString();
                                                            break;
                                                        case 41:
                                                            n.SL4201 = m.SoLuong.ToString();
                                                            break;
                                                        case 42:
                                                            n.SL4301 = m.SoLuong.ToString();
                                                            break;
                                                        case 43:
                                                            n.SL4401 = m.SoLuong.ToString();
                                                            break;
                                                        case 44:
                                                            n.SL4501 = m.SoLuong.ToString();
                                                            break;
                                                        case 45:
                                                            n.SL4601 = m.SoLuong.ToString();
                                                            break;
                                                        case 46:
                                                            n.SL4701 = m.SoLuong.ToString();
                                                            break;
                                                        case 47:
                                                            n.SL4801 = m.SoLuong.ToString();
                                                            break;
                                                        case 48:
                                                            n.SL4901 = m.SoLuong.ToString();
                                                            break;
                                                        case 49:
                                                            n.SL5001 = m.SoLuong.ToString();
                                                            break;
                                                        case 50:
                                                            n.SL5101 = m.SoLuong.ToString();
                                                            break;
                                                        case 51:
                                                            n.SL5201 = m.SoLuong.ToString();
                                                            break;
                                                        case 52:
                                                            n.SL5301 = m.SoLuong.ToString();
                                                            break;
                                                        case 53:
                                                            n.SL5401 = m.SoLuong.ToString();
                                                            break;
                                                        case 54:
                                                            n.SL5501 = m.SoLuong.ToString();
                                                            break;
                                                        case 55:
                                                            n.SL5601 = m.SoLuong.ToString();
                                                            break;
                                                        case 56:
                                                            n.SL5701 = m.SoLuong.ToString();
                                                            break;
                                                        case 57:
                                                            n.SL5801 = m.SoLuong.ToString();
                                                            break;
                                                        case 58:
                                                            n.SL5901 = m.SoLuong.ToString();
                                                            break;
                                                        case 59:
                                                            n.SL6001 = m.SoLuong.ToString();
                                                            break;
                                                        case 60:
                                                            n.SL6101 = m.SoLuong.ToString();
                                                            break;
                                                        case 61:
                                                            n.SL6201 = m.SoLuong.ToString();
                                                            break;
                                                        case 62:
                                                            n.SL6301 = m.SoLuong.ToString();
                                                            break;
                                                        case 63:
                                                            n.SL6401 = m.SoLuong.ToString();
                                                            break;
                                                        case 64:
                                                            n.SL6501 = m.SoLuong.ToString();
                                                            break;
                                                        case 65:
                                                            n.SL6601 = m.SoLuong.ToString();
                                                            break;
                                                        case 66:
                                                            n.SL6701 = m.SoLuong.ToString();
                                                            break;
                                                        case 67:
                                                            n.SL6801 = m.SoLuong.ToString();
                                                            break;
                                                        case 68:
                                                            n.SL6901 = m.SoLuong.ToString();
                                                            break;
                                                        case 69:
                                                            n.SL7001 = m.SoLuong.ToString();
                                                            break;
                                                        case 70:
                                                            n.SL7101 = m.SoLuong.ToString();
                                                            break;
                                                        case 71:
                                                            n.SL7201 = m.SoLuong.ToString();
                                                            break;
                                                        case 72:
                                                            n.SL7301 = m.SoLuong.ToString();
                                                            break;
                                                        case 73:
                                                            n.SL7401 = m.SoLuong.ToString();
                                                            break;
                                                        case 74:
                                                            n.SL7501 = m.SoLuong.ToString();
                                                            break;
                                                        case 75:
                                                            n.SL7601 = m.SoLuong.ToString();
                                                            break;
                                                        case 76:
                                                            n.SL7701 = m.SoLuong.ToString();
                                                            break;
                                                        case 77:
                                                            n.SL7801 = m.SoLuong.ToString();
                                                            break;
                                                        case 78:
                                                            n.SL7901 = m.SoLuong.ToString();
                                                            break;
                                                        case 79:
                                                            n.SL8001 = m.SoLuong.ToString();
                                                            break;
                                                        case 80:
                                                            n.SL8101 = m.SoLuong.ToString();
                                                            break;
                                                        case 81:
                                                            n.SL8201 = m.SoLuong.ToString();
                                                            break;
                                                        case 82:
                                                            n.SL8301 = m.SoLuong.ToString();
                                                            break;
                                                        case 83:
                                                            n.SL8401 = m.SoLuong.ToString();
                                                            break;
                                                        case 84:
                                                            n.SL8501 = m.SoLuong.ToString();
                                                            break;
                                                        case 85:
                                                            n.SL8601 = m.SoLuong.ToString();
                                                            break;
                                                        case 86:
                                                            n.SL8701 = m.SoLuong.ToString();
                                                            break;
                                                        case 87:
                                                            n.SL8801 = m.SoLuong.ToString();
                                                            break;
                                                        case 88:
                                                            n.SL8901 = m.SoLuong.ToString();
                                                            break;
                                                        case 89:
                                                            n.SL9001 = m.SoLuong.ToString();
                                                            break;
                                                        case 90:
                                                            n.SL9101 = m.SoLuong.ToString();
                                                            break;
                                                        case 91:
                                                            n.SL9201 = m.SoLuong.ToString();
                                                            break;
                                                        case 92:
                                                            n.SL9301 = m.SoLuong.ToString();
                                                            break;
                                                        case 93:
                                                            n.SL9401 = m.SoLuong.ToString();
                                                            break;
                                                        case 94:
                                                            n.SL9501 = m.SoLuong.ToString();
                                                            break;
                                                        case 95:
                                                            n.SL9601 = m.SoLuong.ToString();
                                                            break;
                                                        case 96:
                                                            n.SL9701 = m.SoLuong.ToString();
                                                            break;
                                                        case 97:
                                                            n.SL9801 = m.SoLuong.ToString();
                                                            break;
                                                        case 98:
                                                            n.SL9901 = m.SoLuong.ToString();
                                                            break;
                                                    }
                                                    n.tenbn = m.TenBN;
                                                    n.Tuoi = m.Tuoi.Value;
                                                }
                                            }
                                        }
                                    }
                                }

                            }
                            #endregion tạo code báo cáo

                            if (_DSDV.Count >= 100)
                            {
                                DialogResult _result = MessageBox.Show("Mẫu A4 không đủ để hiển thị, bạn có muốn in mẫu A3 không?", "Hỏi in", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                                if (_result == DialogResult.Yes)
                                {
                                    #region tạo báo cáo A3
                                    BaoCao.Rep_TamTraThuocA3 rep = new BaoCao.Rep_TamTraThuocA3();
                                    frmIn frm = new frmIn();
                                    for (int i = 0; i < _DSDV.Count; i++)
                                    {
                                        switch (i)
                                        {
                                            case 0:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T1.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 1:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T2.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 2:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T3.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 3:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T4.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 4:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T5.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 5:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T6.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 6:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T7.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 7:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T8.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 8:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T9.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 9:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T10.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 10:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T11.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 11:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T12.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 12:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T13.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 13:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T14.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 14:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T15.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 15:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T16.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 16:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T17.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 17:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T18.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 18:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T19.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 19:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T20.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 20:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T21.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 21:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T22.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 22:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T23.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 23:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T24.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 24:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T25.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 25:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T26.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 26:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T27.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 27:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T28.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 28:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T29.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 29:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T30.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 30:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T31.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 31:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T32.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 32:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T33.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 33:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T34.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 34:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T35.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 35:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T36.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 36:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T37.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 37:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T38.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 38:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T39.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 39:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T40.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 40:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T41.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 41:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T42.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 42:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T43.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 43:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T44.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 44:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T45.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 45:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T46.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 46:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T47.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 47:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T48.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 48:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T49.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 49:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T50.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 50:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T51.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 51:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T52.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 52:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T53.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 53:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T54.Value = _DSDV.Skip(i).First().tendv; }
                                                break;

                                        }
                                    }

                                    rep.Ngaythang.Value = " Từ ngày: " + LupNgayTu.Text.Substring(0, 10) + " đến ngày: " + LupNgayDen.Text.Substring(0, 10);
                                    rep.KhoaPhong.Value = LupKhoaPhong.Text;
                                    rep.DataSource = _Tamtra;
                                    rep.BindingData();
                                    rep.CreateDocument();
                                    //rep.DataMember = "Table";
                                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                    frm.ShowDialog();
                                    #endregion tạo báo cáo A3
                                }
                                else
                                {
                                    #region tạo báo cáo A4 1 trang
                                    if (_DSDV.Count <= 46)
                                    {
                                        BaoCao.Rep_TamTraThuoc rep = new BaoCao.Rep_TamTraThuoc();
                                        frmIn frm = new frmIn();
                                        string a = "Kiểu đơn: ";
                                        if (chkBoxung.Checked == true)
                                        { a += " bổ xung "; }
                                        if (chkThuongxuyen.Checked == true)
                                        {
                                            if (string.IsNullOrEmpty(a))
                                            {
                                                a += " thường xuyên ";
                                            }
                                            else
                                            { a = a + " - " + " thường xuyên "; }
                                        }
                                        if (chkTrathuoc.Checked == true)
                                        {
                                            if (string.IsNullOrEmpty(a))
                                            {
                                                a += "trả thuốc ";
                                            }
                                            else
                                            { a = a + " - " + " trả thuốc "; }
                                        }
                                        rep.Kieudon.Value = a;
                                        for (int i = 0; i < _DSDV.Count; i++)
                                        {
                                            switch (i)
                                            {
                                                case 0:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T1.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 1:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T2.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 2:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T3.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 3:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T4.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 4:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T5.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 5:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T6.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 6:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T7.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 7:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T8.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 8:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T9.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 9:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T10.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 10:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T11.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 11:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T12.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 12:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T13.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 13:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T14.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 14:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T15.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 15:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T16.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 16:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T17.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 17:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T18.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 18:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T19.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 19:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T20.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 20:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T21.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 21:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T22.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 22:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T23.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 23:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T24.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 24:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T25.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 25:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T26.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 26:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T27.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 27:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T28.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 28:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T29.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 29:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T30.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 30:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T31.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 31:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T32.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 32:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T33.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 33:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T34.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 34:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T35.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 35:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T36.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 36:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T37.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 37:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T38.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 38:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T39.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 39:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T40.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 40:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T41.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 41:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T42.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 42:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T43.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 43:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T44.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 44:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T45.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 45:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T46.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;


                                            }
                                        }

                                        rep.Ngaythang.Value = " Từ ngày: " + LupNgayTu.Text.Substring(0, 10) + " đến ngày: " + LupNgayDen.Text.Substring(0, 10);
                                        rep.Khoaphong.Value = LupKhoaPhong.Text;
                                        rep.DataSource = _Tamtra;
                                        rep.BindingData();
                                        rep.CreateDocument();
                                        //rep.DataMember = "Table";
                                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                        frm.ShowDialog();

                                    }
                                    #endregion tạo báo cáo A4 1 trang
                                    #region tạo báo cáo A4 2 trang
                                    if (_DSDV.Count > 46)
                                    {
                                        BaoCao.Rep_TamTraThuoc rep = new BaoCao.Rep_TamTraThuoc();
                                        frmIn frm = new frmIn();
                                        string a = "Kiểu đơn: ";
                                        if (chkBoxung.Checked == true)
                                        { a += " bổ xung "; }
                                        if (chkThuongxuyen.Checked == true)
                                        {
                                            if (string.IsNullOrEmpty(a))
                                            {
                                                a += "thường xuyên ";
                                            }
                                            else
                                            { a = a + " - " + " thường xuyên "; }
                                        }
                                        if (chkTrathuoc.Checked == true)
                                        {
                                            if (string.IsNullOrEmpty(a))
                                            {
                                                a += "trả thuốc ";
                                            }
                                            else
                                            { a = a + " - " + " trả thuốc "; }
                                        }
                                        rep.Kieudon.Value = a;
                                        for (int i = 0; i < _DSDV.Count; i++)
                                        {
                                            switch (i)
                                            {
                                                case 0:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T1.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 1:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T2.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 2:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T3.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 3:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T4.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 4:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T5.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 5:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T6.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 6:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T7.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 7:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T8.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 8:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T9.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 9:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T10.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 10:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T11.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 11:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T12.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 12:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T13.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 13:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T14.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 14:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T15.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 15:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T16.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 16:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T17.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 17:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T18.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 18:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T19.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 19:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T20.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 20:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T21.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 21:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T22.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 22:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T23.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 23:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T24.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 24:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T25.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 25:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T26.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 26:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T27.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 27:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T28.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 28:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T29.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 29:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T30.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 30:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T31.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 31:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T32.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 32:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T33.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 33:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T34.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 34:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T35.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 35:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T36.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 36:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T37.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 37:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T38.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 38:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T39.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 39:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T40.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 40:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T41.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 41:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T42.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 42:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T43.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 43:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T44.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 44:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T45.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 45:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T46.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;


                                            }
                                        }

                                        rep.Ngaythang.Value = " Từ ngày: " + LupNgayTu.Text.Substring(0, 10) + " đến ngày: " + LupNgayDen.Text.Substring(0, 10);
                                        rep.Khoaphong.Value = LupKhoaPhong.Text;
                                        rep.DataSource = _Tamtra;
                                        rep.BindingData();
                                        rep.CreateDocument();
                                        //rep.DataMember = "Table";
                                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                        frm.ShowDialog();

                                        // tạo báo cáo thứ 2

                                        BaoCao.Rep_Tamtrathuoc02 rep1 = new BaoCao.Rep_Tamtrathuoc02();
                                        frmIn frm1 = new frmIn();
                                        a = "Kiểu đơn: ";
                                        if (chkBoxung.Checked == true)
                                        { a += " bổ xung "; }
                                        if (chkThuongxuyen.Checked == true)
                                        {
                                            if (string.IsNullOrEmpty(a))
                                            {
                                                a += " thường xuyên ";
                                            }
                                            else
                                            { a = a + " - " + " thường xuyên "; }
                                        }
                                        if (chkTrathuoc.Checked == true)
                                        {
                                            if (string.IsNullOrEmpty(a))
                                            {
                                                a += " trả thuốc ";
                                            }
                                            else
                                            { a = a + " - " + " trả thuốc "; }
                                        }
                                        rep1.Kieudon.Value = a;
                                        for (int i = 46; i < _DSDV.Count; i++)
                                        {
                                            switch (i)
                                            {
                                                case 46:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T1.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 47:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T2.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 48:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T3.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 49:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T4.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 50:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T5.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 51:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T6.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 52:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T7.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 53:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T8.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 54:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T9.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 55:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T10.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 56:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T11.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 57:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T12.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 58:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T13.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 59:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T14.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 60:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T15.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 61:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T16.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 62:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T17.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 63:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T18.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 64:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T19.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 65:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T20.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 66:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T21.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 67:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T22.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 68:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T23.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 69:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T24.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 70:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T25.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 71:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T26.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 72:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T27.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 73:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T28.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 74:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T29.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 75:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T30.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 76:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T31.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 77:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T32.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 78:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T33.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 79:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T34.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 80:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T35.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 81:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T36.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 82:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T37.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 83:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T38.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 84:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T39.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 85:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T40.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 86:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T41.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 87:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T42.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 88:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T43.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 89:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T44.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 90:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T45.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 91:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T46.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 92:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T47.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 93:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T48.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 94:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T49.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 95:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T50.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 96:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T51.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 97:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T52.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 98:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T53.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;

                                            }
                                        }

                                        rep1.Ngaythang.Value = " Từ ngày: " + LupNgayTu.Text.Substring(0, 10) + " đến ngày: " + LupNgayDen.Text.Substring(0, 10);
                                        rep1.Khoaphong.Value = LupKhoaPhong.Text;
                                        rep1.DataSource = _Tamtra;
                                        rep1.BindingData();
                                        rep1.CreateDocument();
                                        //rep.DataMember = "Table";
                                        frm1.prcIN.PrintingSystem = rep1.PrintingSystem;
                                        frm1.ShowDialog();
                                    }
                                    #endregion tạo báo cáo A4 2 trang
                                }

                            }
                            else
                            {
                                #region tạo báo cáo A4 1 trang
                                if (_DSDV.Count <= 46)
                                {
                                    BaoCao.Rep_TamTraThuoc rep = new BaoCao.Rep_TamTraThuoc();
                                    frmIn frm = new frmIn();
                                    string a = "Kiểu đơn: ";
                                    if (chkBoxung.Checked == true)
                                    { a += " bổ xung "; }
                                    if (chkThuongxuyen.Checked == true)
                                    {
                                        if (string.IsNullOrEmpty(a))
                                        {
                                            a += " thường xuyên ";
                                        }
                                        else
                                        { a = a + " - " + " thường xuyên "; }
                                    }
                                    if (chkTrathuoc.Checked == true)
                                    {
                                        if (string.IsNullOrEmpty(a))
                                        {
                                            a += "trả thuốc ";
                                        }
                                        else
                                        { a = a + " - " + " trả thuốc "; }
                                    }
                                    rep.Kieudon.Value = a;
                                    for (int i = 0; i < _DSDV.Count; i++)
                                    {
                                        switch (i)
                                        {
                                            case 0:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T1.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 1:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T2.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 2:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T3.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 3:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T4.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 4:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T5.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 5:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T6.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 6:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T7.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 7:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T8.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 8:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T9.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 9:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T10.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 10:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T11.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 11:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T12.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 12:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T13.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 13:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T14.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 14:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T15.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 15:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T16.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 16:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T17.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 17:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T18.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 18:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T19.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 19:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T20.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 20:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T21.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 21:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T22.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 22:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T23.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 23:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T24.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 24:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T25.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 25:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T26.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 26:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T27.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 27:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T28.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 28:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T29.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 29:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T30.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 30:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T31.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 31:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T32.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 32:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T33.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 33:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T34.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 34:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T35.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 35:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T36.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 36:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T37.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 37:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T38.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 38:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T39.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 39:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T40.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 40:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T41.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 41:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T42.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 42:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T43.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 43:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T44.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 44:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T45.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 45:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T46.Value = _DSDV.Skip(i).First().tendv; }
                                                break;


                                        }
                                    }

                                    rep.Ngaythang.Value = " Từ ngày: " + LupNgayTu.Text.Substring(0, 10) + " đến ngày: " + LupNgayDen.Text.Substring(0, 10);
                                    rep.Khoaphong.Value = LupKhoaPhong.Text;
                                    rep.DataSource = _Tamtra;
                                    rep.BindingData();
                                    rep.CreateDocument();
                                    //rep.DataMember = "Table";
                                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                    frm.ShowDialog();

                                }
                                #endregion tạo báo cáo A4 1 trang
                                #region tạo báo cáo A4 2 trang
                                if (_DSDV.Count > 46)
                                {
                                    BaoCao.Rep_TamTraThuoc rep = new BaoCao.Rep_TamTraThuoc();
                                    frmIn frm = new frmIn();
                                    string a = "Kiểu đơn: ";
                                    if (chkBoxung.Checked == true)
                                    { a += " bổ xung "; }
                                    if (chkThuongxuyen.Checked == true)
                                    {
                                        if (string.IsNullOrEmpty(a))
                                        {
                                            a += "thường xuyên ";
                                        }
                                        else
                                        { a = a + " - " + " thường xuyên "; }
                                    }
                                    if (chkTrathuoc.Checked == true)
                                    {
                                        if (string.IsNullOrEmpty(a))
                                        {
                                            a += "trả thuốc ";
                                        }
                                        else
                                        { a = a + " - " + " trả thuốc "; }
                                    }
                                    rep.Kieudon.Value = a;
                                    for (int i = 0; i < _DSDV.Count; i++)
                                    {
                                        switch (i)
                                        {
                                            case 0:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T1.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 1:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T2.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 2:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T3.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 3:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T4.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 4:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T5.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 5:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T6.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 6:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T7.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 7:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T8.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 8:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T9.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 9:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T10.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 10:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T11.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 11:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T12.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 12:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T13.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 13:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T14.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 14:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T15.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 15:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T16.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 16:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T17.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 17:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T18.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 18:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T19.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 19:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T20.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 20:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T21.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 21:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T22.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 22:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T23.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 23:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T24.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 24:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T25.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 25:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T26.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 26:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T27.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 27:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T28.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 28:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T29.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 29:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T30.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 30:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T31.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 31:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T32.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 32:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T33.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 33:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T34.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 34:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T35.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 35:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T36.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 36:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T37.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 37:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T38.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 38:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T39.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 39:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T40.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 40:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T41.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 41:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T42.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 42:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T43.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 43:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T44.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 44:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T45.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 45:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T46.Value = _DSDV.Skip(i).First().tendv; }
                                                break;


                                        }
                                    }

                                    rep.Ngaythang.Value = " Từ ngày: " + LupNgayTu.Text.Substring(0, 10) + " đến ngày: " + LupNgayDen.Text.Substring(0, 10);
                                    rep.Khoaphong.Value = LupKhoaPhong.Text;
                                    rep.DataSource = _Tamtra;
                                    rep.BindingData();
                                    rep.CreateDocument();
                                    //rep.DataMember = "Table";
                                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                    frm.ShowDialog();

                                    // tạo báo cáo thứ 2

                                    BaoCao.Rep_Tamtrathuoc02 rep1 = new BaoCao.Rep_Tamtrathuoc02();
                                    frmIn frm1 = new frmIn();
                                    a = "Kiểu đơn: ";
                                    if (chkBoxung.Checked == true)
                                    { a += " bổ xung "; }
                                    if (chkThuongxuyen.Checked == true)
                                    {
                                        if (string.IsNullOrEmpty(a))
                                        {
                                            a += " thường xuyên ";
                                        }
                                        else
                                        { a = a + " - " + " thường xuyên "; }
                                    }
                                    if (chkTrathuoc.Checked == true)
                                    {
                                        if (string.IsNullOrEmpty(a))
                                        {
                                            a += " trả thuốc ";
                                        }
                                        else
                                        { a = a + " - " + " trả thuốc "; }
                                    }
                                    rep1.Kieudon.Value = a;
                                    for (int i = 46; i < _DSDV.Count; i++)
                                    {
                                        switch (i)
                                        {
                                            case 46:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T1.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 47:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T2.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 48:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T3.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 49:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T4.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 50:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T5.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 51:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T6.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 52:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T7.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 53:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T8.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 54:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T9.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 55:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T10.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 56:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T11.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 57:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T12.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 58:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T13.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 59:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T14.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 60:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T15.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 61:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T16.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 62:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T17.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 63:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T18.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 64:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T19.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 65:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T20.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 66:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T21.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 67:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T22.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 68:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T23.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 69:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T24.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 70:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T25.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 71:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T26.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 72:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T27.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 73:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T28.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 74:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T29.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 75:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T30.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 76:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T31.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 77:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T32.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 78:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T33.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 79:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T34.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 80:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T35.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 81:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T36.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 82:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T37.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 83:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T38.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 84:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T39.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 85:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T40.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 86:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T41.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 87:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T42.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 88:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T43.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 89:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T44.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 90:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T45.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 91:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T46.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 92:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T47.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 93:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T48.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 94:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T49.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 95:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T50.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 96:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T51.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 97:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T52.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 98:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T53.Value = _DSDV.Skip(i).First().tendv; }
                                                break;

                                        }
                                    }

                                    rep1.Ngaythang.Value = " Từ ngày: " + LupNgayTu.Text.Substring(0, 10) + " đến ngày: " + LupNgayDen.Text.Substring(0, 10);
                                    rep1.Khoaphong.Value = LupKhoaPhong.Text;
                                    rep1.DataSource = _Tamtra;
                                    rep1.BindingData();
                                    rep1.CreateDocument();
                                    //rep.DataMember = "Table";
                                    frm1.prcIN.PrintingSystem = rep1.PrintingSystem;
                                    frm1.ShowDialog();
                                }
                                #endregion tạo báo cáo A4 2 trang
                            }
                        }
                    }
                    else
                    {
                        var bn2 = (from bn1 in _Data.BenhNhans.Where(p => p.NoiTru == 0)
                                   join DT in _Data.DThuocs.Where(p => p.KieuDon == _a1 || p.KieuDon == _a2 || p.KieuDon == _a3).Where(p => p.MaKP == _MaKP) on bn1.MaBNhan equals DT.MaBNhan
                                   join DTct in _Data.DThuoccts.Where(p => p.TrongBH == 1) on DT.IDDon equals DTct.IDDon
                                   join DV in _Data.DichVus.Where(P => P.PLoai == 1) on DTct.MaDV equals DV.MaDV
                                   join vv in _Data.VaoViens on bn1.MaBNhan equals vv.MaBNhan
                                   join Ndv in _Data.NhomDVs on DV.IDNhom equals Ndv.IDNhom
                                   where (DT.NgayKe >= Ngaytu && DT.NgayKe <= Ngayden)
                                   //where (DT.Status == _Status1)
                                   where (DTct.Status != null && (DTct.Status == _Status1 || DTct.Status == _Status2))
                                   group new { DV, Ndv } by new { DV.MaDV, DV.TenDV, Ndv.TenNhomCT } into kq
                                   select new
                                   {
                                       TenN = kq.Key.TenNhomCT,
                                       TenDV = kq.Key.TenDV,
                                       MaDV = kq.Key.MaDV,
                                   }).OrderBy(p => p.TenDV).OrderBy(p => p.TenN).ToList();
                        if (bn2.Count > 0)
                        {
                            foreach (var a in bn2)
                            {
                                DSDV themmoi = new DSDV();
                                themmoi.tendv = a.TenDV;
                                themmoi.madv = a.MaDV;
                                _DSDV.Add(themmoi);
                            }
                        }

                        var MaBN = (from bn3 in _Data.BenhNhans.Where(p => p.NoiTru == 0)
                                    join DT in _Data.DThuocs.Where(p => p.KieuDon == _a1 || p.KieuDon == _a2 || p.KieuDon == _a3).Where(p => p.MaKP == _MaKP) on bn3.MaBNhan equals DT.MaBNhan
                                    join DTct in _Data.DThuoccts.Where(p => p.TrongBH == 1) on DT.IDDon equals DTct.IDDon
                                    join dv in _Data.DichVus.Where(p => p.PLoai == 1) on DTct.MaDV equals dv.MaDV
                                    join vv in _Data.VaoViens on bn3.MaBNhan equals vv.MaBNhan
                                    where (DT.NgayKe >= Ngaytu && DT.NgayKe <= Ngayden)
                                    //where (DT.Status == _Status1)
                                    where (DTct.Status == _Status1 || DTct.Status == _Status2)
                                    group new { bn3 } by new { bn3.MaBNhan } into kq
                                    select new { MaBN = kq.Key.MaBNhan }).ToList();
                        if (MaBN.Count > 0)
                        {
                            foreach (var c in MaBN)
                            {
                                Tamtra themmoi = new Tamtra();
                                themmoi.mabn = c.MaBN;
                                _Tamtra.Add(themmoi);
                            }
                        }
                        var bn = (from bn1 in _Data.BenhNhans.Where(p => p.NoiTru == 0)
                                  join DT in _Data.DThuocs.Where(p => p.KieuDon == _a1 || p.KieuDon == _a2 || p.KieuDon == _a3).Where(p => p.MaKP == _MaKP) on bn1.MaBNhan equals DT.MaBNhan
                                  join DTct in _Data.DThuoccts.Where(p => p.TrongBH == 1) on DT.IDDon equals DTct.IDDon
                                  join DV in _Data.DichVus.Where(p => p.PLoai == 1) on DTct.MaDV equals DV.MaDV
                                  join vv in _Data.VaoViens on bn1.MaBNhan equals vv.MaBNhan
                                  where (DT.NgayKe >= Ngaytu && DT.NgayKe <= Ngayden)
                                  //where (DT.Status == _Status1)
                                  where (DTct.Status == _Status1 || DTct.Status == _Status2)
                                  group new { bn1, DTct } by new { DV.MaDV, bn1.TenBNhan, bn1.MaBNhan, bn1.Tuoi } into kq
                                  select new
                                  {
                                      kq.Key.Tuoi,
                                      Mabenhnhan = kq.Key.MaBNhan,
                                      MaDV = kq.Key.MaDV,
                                      SoLuong = kq.Sum(p => p.DTct.SoLuong),
                                      TenBN = kq.Key.TenBNhan,
                                  }).ToList();
                        if (bn.Count > 0)
                        {
                            #region tạo code bao cao
                            foreach (var n in _Tamtra)
                            {
                                foreach (var m in bn)
                                {
                                    if (n.mabn == m.Mabenhnhan)
                                    {
                                        if (m.SoLuong != null && m.SoLuong != 0)
                                        {
                                            for (int i = 0; i < _DSDV.Count; i++)
                                            {
                                                if (m.MaDV == _DSDV.Skip(i).First().madv)
                                                {
                                                    switch (i)
                                                    {
                                                        case 0:
                                                            n.SL101 = m.SoLuong.ToString();
                                                            break;
                                                        case 1:
                                                            n.SL201 = m.SoLuong.ToString();
                                                            break;
                                                        case 2:
                                                            n.SL301 = m.SoLuong.ToString();
                                                            break;
                                                        case 3:
                                                            n.SL401 = m.SoLuong.ToString();
                                                            break;
                                                        case 4:
                                                            n.SL501 = m.SoLuong.ToString();
                                                            break;
                                                        case 5:
                                                            n.SL601 = m.SoLuong.ToString();
                                                            break;
                                                        case 6:
                                                            n.SL701 = m.SoLuong.ToString();
                                                            break;
                                                        case 7:
                                                            n.SL801 = m.SoLuong.ToString();
                                                            break;
                                                        case 8:
                                                            n.SL901 = m.SoLuong.ToString();
                                                            break;
                                                        case 9:
                                                            n.SL1001 = m.SoLuong.ToString();
                                                            break;
                                                        case 10:
                                                            n.SL1101 = m.SoLuong.ToString();
                                                            break;
                                                        case 11:
                                                            n.SL1201 = m.SoLuong.ToString();
                                                            break;
                                                        case 12:
                                                            n.SL1301 = m.SoLuong.ToString();
                                                            break;
                                                        case 13:
                                                            n.SL1401 = m.SoLuong.ToString();
                                                            break;
                                                        case 14:
                                                            n.SL1501 = m.SoLuong.ToString();
                                                            break;
                                                        case 15:
                                                            n.SL1601 = m.SoLuong.ToString();
                                                            break;
                                                        case 16:
                                                            n.SL1701 = m.SoLuong.ToString();
                                                            break;
                                                        case 17:
                                                            n.SL1801 = m.SoLuong.ToString();
                                                            break;
                                                        case 18:
                                                            n.SL1901 = m.SoLuong.ToString();
                                                            break;
                                                        case 19:
                                                            n.SL2001 = m.SoLuong.ToString();
                                                            break;
                                                        case 20:
                                                            n.SL2101 = m.SoLuong.ToString();
                                                            break;
                                                        case 21:
                                                            n.SL2201 = m.SoLuong.ToString();
                                                            break;
                                                        case 22:
                                                            n.SL2301 = m.SoLuong.ToString();
                                                            break;
                                                        case 23:
                                                            n.SL2401 = m.SoLuong.ToString();
                                                            break;
                                                        case 24:
                                                            n.SL2501 = m.SoLuong.ToString();
                                                            break;
                                                        case 25:
                                                            n.SL2601 = m.SoLuong.ToString();
                                                            break;
                                                        case 26:
                                                            n.SL2701 = m.SoLuong.ToString();
                                                            break;
                                                        case 27:
                                                            n.SL2801 = m.SoLuong.ToString();
                                                            break;
                                                        case 28:
                                                            n.SL2901 = m.SoLuong.ToString();
                                                            break;
                                                        case 29:
                                                            n.SL3001 = m.SoLuong.ToString();
                                                            break;
                                                        case 30:
                                                            n.SL3101 = m.SoLuong.ToString();
                                                            break;
                                                        case 31:
                                                            n.SL3201 = m.SoLuong.ToString();
                                                            break;
                                                        case 32:
                                                            n.SL3301 = m.SoLuong.ToString();
                                                            break;
                                                        case 33:
                                                            n.SL3401 = m.SoLuong.ToString();
                                                            break;
                                                        case 34:
                                                            n.SL3501 = m.SoLuong.ToString();
                                                            break;
                                                        case 35:
                                                            n.SL3601 = m.SoLuong.ToString();
                                                            break;
                                                        case 36:
                                                            n.SL3701 = m.SoLuong.ToString();
                                                            break;
                                                        case 37:
                                                            n.SL3801 = m.SoLuong.ToString();
                                                            break;
                                                        case 38:
                                                            n.SL3901 = m.SoLuong.ToString();
                                                            break;
                                                        case 39:
                                                            n.SL4001 = m.SoLuong.ToString();
                                                            break;
                                                        case 40:
                                                            n.SL4101 = m.SoLuong.ToString();
                                                            break;
                                                        case 41:
                                                            n.SL4201 = m.SoLuong.ToString();
                                                            break;
                                                        case 42:
                                                            n.SL4301 = m.SoLuong.ToString();
                                                            break;
                                                        case 43:
                                                            n.SL4401 = m.SoLuong.ToString();
                                                            break;
                                                        case 44:
                                                            n.SL4501 = m.SoLuong.ToString();
                                                            break;
                                                        case 45:
                                                            n.SL4601 = m.SoLuong.ToString();
                                                            break;
                                                        case 46:
                                                            n.SL4701 = m.SoLuong.ToString();
                                                            break;
                                                        case 47:
                                                            n.SL4801 = m.SoLuong.ToString();
                                                            break;
                                                        case 48:
                                                            n.SL4901 = m.SoLuong.ToString();
                                                            break;
                                                        case 49:
                                                            n.SL5001 = m.SoLuong.ToString();
                                                            break;
                                                        case 50:
                                                            n.SL5101 = m.SoLuong.ToString();
                                                            break;
                                                        case 51:
                                                            n.SL5201 = m.SoLuong.ToString();
                                                            break;
                                                        case 52:
                                                            n.SL5301 = m.SoLuong.ToString();
                                                            break;
                                                        case 53:
                                                            n.SL5401 = m.SoLuong.ToString();
                                                            break;
                                                        case 54:
                                                            n.SL5501 = m.SoLuong.ToString();
                                                            break;
                                                        case 55:
                                                            n.SL5601 = m.SoLuong.ToString();
                                                            break;
                                                        case 56:
                                                            n.SL5701 = m.SoLuong.ToString();
                                                            break;
                                                        case 57:
                                                            n.SL5801 = m.SoLuong.ToString();
                                                            break;
                                                        case 58:
                                                            n.SL5901 = m.SoLuong.ToString();
                                                            break;
                                                        case 59:
                                                            n.SL6001 = m.SoLuong.ToString();
                                                            break;
                                                        case 60:
                                                            n.SL6101 = m.SoLuong.ToString();
                                                            break;
                                                        case 61:
                                                            n.SL6201 = m.SoLuong.ToString();
                                                            break;
                                                        case 62:
                                                            n.SL6301 = m.SoLuong.ToString();
                                                            break;
                                                        case 63:
                                                            n.SL6401 = m.SoLuong.ToString();
                                                            break;
                                                        case 64:
                                                            n.SL6501 = m.SoLuong.ToString();
                                                            break;
                                                        case 65:
                                                            n.SL6601 = m.SoLuong.ToString();
                                                            break;
                                                        case 66:
                                                            n.SL6701 = m.SoLuong.ToString();
                                                            break;
                                                        case 67:
                                                            n.SL6801 = m.SoLuong.ToString();
                                                            break;
                                                        case 68:
                                                            n.SL6901 = m.SoLuong.ToString();
                                                            break;
                                                        case 69:
                                                            n.SL7001 = m.SoLuong.ToString();
                                                            break;
                                                        case 70:
                                                            n.SL7101 = m.SoLuong.ToString();
                                                            break;
                                                        case 71:
                                                            n.SL7201 = m.SoLuong.ToString();
                                                            break;
                                                        case 72:
                                                            n.SL7301 = m.SoLuong.ToString();
                                                            break;
                                                        case 73:
                                                            n.SL7401 = m.SoLuong.ToString();
                                                            break;
                                                        case 74:
                                                            n.SL7501 = m.SoLuong.ToString();
                                                            break;
                                                        case 75:
                                                            n.SL7601 = m.SoLuong.ToString();
                                                            break;
                                                        case 76:
                                                            n.SL7701 = m.SoLuong.ToString();
                                                            break;
                                                        case 77:
                                                            n.SL7801 = m.SoLuong.ToString();
                                                            break;
                                                        case 78:
                                                            n.SL7901 = m.SoLuong.ToString();
                                                            break;
                                                        case 79:
                                                            n.SL8001 = m.SoLuong.ToString();
                                                            break;
                                                        case 80:
                                                            n.SL8101 = m.SoLuong.ToString();
                                                            break;
                                                        case 81:
                                                            n.SL8201 = m.SoLuong.ToString();
                                                            break;
                                                        case 82:
                                                            n.SL8301 = m.SoLuong.ToString();
                                                            break;
                                                        case 83:
                                                            n.SL8401 = m.SoLuong.ToString();
                                                            break;
                                                        case 84:
                                                            n.SL8501 = m.SoLuong.ToString();
                                                            break;
                                                        case 85:
                                                            n.SL8601 = m.SoLuong.ToString();
                                                            break;
                                                        case 86:
                                                            n.SL8701 = m.SoLuong.ToString();
                                                            break;
                                                        case 87:
                                                            n.SL8801 = m.SoLuong.ToString();
                                                            break;
                                                        case 88:
                                                            n.SL8901 = m.SoLuong.ToString();
                                                            break;
                                                        case 89:
                                                            n.SL9001 = m.SoLuong.ToString();
                                                            break;
                                                        case 90:
                                                            n.SL9101 = m.SoLuong.ToString();
                                                            break;
                                                        case 91:
                                                            n.SL9201 = m.SoLuong.ToString();
                                                            break;
                                                        case 92:
                                                            n.SL9301 = m.SoLuong.ToString();
                                                            break;
                                                        case 93:
                                                            n.SL9401 = m.SoLuong.ToString();
                                                            break;
                                                        case 94:
                                                            n.SL9501 = m.SoLuong.ToString();
                                                            break;
                                                        case 95:
                                                            n.SL9601 = m.SoLuong.ToString();
                                                            break;
                                                        case 96:
                                                            n.SL9701 = m.SoLuong.ToString();
                                                            break;
                                                        case 97:
                                                            n.SL9801 = m.SoLuong.ToString();
                                                            break;
                                                        case 98:
                                                            n.SL9901 = m.SoLuong.ToString();
                                                            break;
                                                    }
                                                    n.tenbn = m.TenBN;
                                                    n.Tuoi = m.Tuoi.Value;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            #endregion tạo code bao cao
                            if (_DSDV.Count >= 100)
                            {
                                DialogResult _result = MessageBox.Show("Mẫu A4 không đủ để hiển thị, bạn có muốn in mẫu A3 không?", "Hỏi in", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                if (_result == DialogResult.Yes)
                                {
                                    #region tạo báo cáo A3
                                    BaoCao.Rep_TamTraThuocA3 rep = new BaoCao.Rep_TamTraThuocA3();
                                    frmIn frm = new frmIn();
                                    for (int i = 0; i < _DSDV.Count; i++)
                                    {
                                        switch (i)
                                        {
                                            case 0:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T1.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 1:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T2.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 2:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T3.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 3:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T4.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 4:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T5.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 5:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T6.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 6:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T7.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 7:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T8.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 8:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T9.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 9:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T10.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 10:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T11.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 11:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T12.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 12:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T13.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 13:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T14.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 14:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T15.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 15:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T16.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 16:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T17.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 17:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T18.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 18:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T19.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 19:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T20.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 20:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T21.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 21:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T22.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 22:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T23.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 23:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T24.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 24:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T25.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 25:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T26.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 26:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T27.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 27:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T28.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 28:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T29.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 29:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T30.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 30:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T31.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 31:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T32.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 32:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T33.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 33:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T34.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 34:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T35.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 35:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T36.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 36:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T37.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 37:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T38.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 38:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T39.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 39:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T40.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 40:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T41.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 41:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T42.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 42:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T43.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 43:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T44.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 44:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T45.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 45:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T46.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 46:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T47.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 47:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T48.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 48:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T49.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 49:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T50.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 50:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T51.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 51:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T52.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 52:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T53.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 53:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T54.Value = _DSDV.Skip(i).First().tendv; }
                                                break;

                                        }
                                    }

                                    rep.Ngaythang.Value = " Từ ngày: " + LupNgayTu.Text.Substring(0, 10) + " đến ngày: " + LupNgayDen.Text.Substring(0, 10);
                                    rep.KhoaPhong.Value = LupKhoaPhong.Text;
                                    rep.DataSource = _Tamtra;
                                    rep.BindingData();
                                    rep.CreateDocument();
                                    //rep.DataMember = "Table";
                                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                    frm.ShowDialog();
                                    #endregion tạo báo cáo A3
                                }
                                else
                                {
                                    if (_DSDV.Count <= 46)
                                    #region tạo báo cáo A4 1 trang
                                    {
                                        BaoCao.Rep_TamTraThuoc rep = new BaoCao.Rep_TamTraThuoc();
                                        frmIn frm = new frmIn();
                                        string a = "Kiểu đơn: ";
                                        if (chkBoxung.Checked == true)
                                        { a += "bổ xung"; }
                                        if (chkThuongxuyen.Checked == true)
                                        {
                                            if (string.IsNullOrEmpty(a))
                                            {
                                                a += "thường xuyên";
                                            }
                                            else
                                            { a = a + " - " + " thường xuyên"; }
                                        }
                                        if (chkTrathuoc.Checked == true)
                                        {
                                            if (string.IsNullOrEmpty(a))
                                            {
                                                a += "trả thuốc";
                                            }
                                            else
                                            { a = a + " - " + " trả thuốc"; }
                                        }
                                        rep.Kieudon.Value = a;
                                        for (int i = 0; i < _DSDV.Count; i++)
                                        {
                                            switch (i)
                                            {
                                                case 0:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T1.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 1:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T2.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 2:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T3.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 3:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T4.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 4:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T5.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 5:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T6.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 6:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T7.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 7:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T8.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 8:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T9.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 9:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T10.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 10:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T11.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 11:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T12.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 12:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T13.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 13:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T14.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 14:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T15.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 15:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T16.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 16:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T17.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 17:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T18.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 18:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T19.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 19:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T20.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 20:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T21.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 21:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T22.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 22:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T23.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 23:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T24.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 24:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T25.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 25:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T26.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 26:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T27.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 27:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T28.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 28:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T29.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 29:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T30.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 30:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T31.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 31:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T32.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 32:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T33.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 33:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T34.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 34:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T35.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 35:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T36.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 36:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T37.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 37:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T38.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 38:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T39.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 39:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T40.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 40:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T41.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 41:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T42.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 42:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T43.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 43:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T44.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 44:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T45.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 45:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T46.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;


                                            }
                                        }

                                        rep.Ngaythang.Value = " Từ ngày: " + LupNgayTu.Text.Substring(0, 10) + " đến ngày: " + LupNgayDen.Text.Substring(0, 10);
                                        rep.Khoaphong.Value = LupKhoaPhong.Text;
                                        rep.DataSource = _Tamtra;
                                        rep.BindingData();
                                        rep.CreateDocument();
                                        //rep.DataMember = "Table";
                                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                        frm.ShowDialog();
                                    }
                                    #endregion tạo báo cáo A4 1 trang
                                    if (_DSDV.Count > 46)
                                    #region tạo báo cáo A4 2 trang
                                    {
                                        BaoCao.Rep_TamTraThuoc rep = new BaoCao.Rep_TamTraThuoc();
                                        frmIn frm = new frmIn();
                                        string a = "Kiểu đơn: ";
                                        if (chkBoxung.Checked == true)
                                        { a += "bổ xung"; }
                                        if (chkThuongxuyen.Checked == true)
                                        {
                                            if (string.IsNullOrEmpty(a))
                                            {
                                                a += "thường xuyên";
                                            }
                                            else
                                            { a = a + " - " + " thường xuyên"; }
                                        }
                                        if (chkTrathuoc.Checked == true)
                                        {
                                            if (string.IsNullOrEmpty(a))
                                            {
                                                a += "trả thuốc";
                                            }
                                            else
                                            { a = a + " - " + " trả thuốc"; }
                                        }
                                        rep.Kieudon.Value = a;
                                        for (int i = 0; i < _DSDV.Count; i++)
                                        {
                                            switch (i)
                                            {
                                                case 0:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T1.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 1:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T2.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 2:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T3.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 3:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T4.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 4:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T5.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 5:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T6.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 6:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T7.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 7:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T8.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 8:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T9.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 9:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T10.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 10:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T11.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 11:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T12.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 12:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T13.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 13:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T14.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 14:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T15.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 15:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T16.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 16:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T17.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 17:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T18.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 18:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T19.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 19:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T20.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 20:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T21.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 21:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T22.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 22:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T23.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 23:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T24.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 24:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T25.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 25:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T26.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 26:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T27.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 27:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T28.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 28:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T29.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 29:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T30.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 30:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T31.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 31:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T32.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 32:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T33.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 33:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T34.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 34:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T35.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 35:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T36.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 36:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T37.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 37:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T38.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 38:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T39.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 39:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T40.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 40:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T41.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 41:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T42.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 42:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T43.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 43:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T44.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 44:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T45.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 45:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T46.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;


                                            }
                                        }

                                        rep.Ngaythang.Value = " Từ ngày: " + LupNgayTu.Text.Substring(0, 10) + " đến ngày: " + LupNgayDen.Text.Substring(0, 10);
                                        rep.Khoaphong.Value = LupKhoaPhong.Text;
                                        rep.DataSource = _Tamtra;
                                        rep.BindingData();
                                        rep.CreateDocument();
                                        //rep.DataMember = "Table";
                                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                        frm.ShowDialog();

                                        // tạo báo cáo thứ 2

                                        BaoCao.Rep_Tamtrathuoc02 rep1 = new BaoCao.Rep_Tamtrathuoc02();
                                        frmIn frm1 = new frmIn();
                                        a = "Kiểu đơn: ";
                                        if (chkBoxung.Checked == true)
                                        { a += " bổ xung "; }
                                        if (chkThuongxuyen.Checked == true)
                                        {
                                            if (string.IsNullOrEmpty(a))
                                            {
                                                a += " thường xuyên ";
                                            }
                                            else
                                            { a = a + " - " + " thường xuyên "; }
                                        }
                                        if (chkTrathuoc.Checked == true)
                                        {
                                            if (string.IsNullOrEmpty(a))
                                            {
                                                a += " trả thuốc ";
                                            }
                                            else
                                            { a = a + " - " + " trả thuốc "; }
                                        }
                                        rep1.Kieudon.Value = a;
                                        for (int i = 46; i < _DSDV.Count; i++)
                                        {
                                            switch (i)
                                            {
                                                case 46:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T1.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 47:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T2.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 48:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T3.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 49:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T4.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 50:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T5.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 51:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T6.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 52:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T7.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 53:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T8.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 54:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T9.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 55:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T10.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 56:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T11.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 57:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T12.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 58:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T13.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 59:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T14.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 60:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T15.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 61:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T16.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 62:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T17.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 63:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T18.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 64:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T19.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 65:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T20.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 66:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T21.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 67:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T22.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 68:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T23.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 69:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T24.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 70:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T25.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 71:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T26.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 72:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T27.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 73:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T28.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 74:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T29.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 75:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T30.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 76:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T31.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 77:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T32.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 78:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T33.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 79:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T34.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 80:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T35.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 81:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T36.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 82:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T37.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 83:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T38.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 84:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T39.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 85:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T40.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 86:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T41.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 87:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T42.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 88:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T43.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 89:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T44.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 90:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T45.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 91:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T46.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 92:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T47.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 93:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T48.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 94:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T49.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 95:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T50.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 96:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T51.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 97:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T52.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 98:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T53.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;

                                            }
                                        }

                                        rep1.Ngaythang.Value = " Từ ngày: " + LupNgayTu.Text.Substring(0, 10) + " đến ngày: " + LupNgayDen.Text.Substring(0, 10);
                                        rep1.Khoaphong.Value = LupKhoaPhong.Text;
                                        rep1.DataSource = _Tamtra;
                                        rep1.BindingData();
                                        rep1.CreateDocument();
                                        //rep.DataMember = "Table";
                                        frm1.prcIN.PrintingSystem = rep1.PrintingSystem;
                                        frm1.ShowDialog();
                                    }
                                    #endregion tạo báo cáo A4 2 trang
                                }

                            }
                            else
                            {
                                if (_DSDV.Count <= 46)
                                #region tạo báo cáo A4 1 trang
                                {
                                    BaoCao.Rep_TamTraThuoc rep = new BaoCao.Rep_TamTraThuoc();
                                    frmIn frm = new frmIn();
                                    string a = "Kiểu đơn: ";
                                    if (chkBoxung.Checked == true)
                                    { a += "bổ xung"; }
                                    if (chkThuongxuyen.Checked == true)
                                    {
                                        if (string.IsNullOrEmpty(a))
                                        {
                                            a += "thường xuyên";
                                        }
                                        else
                                        { a = a + " - " + " thường xuyên"; }
                                    }
                                    if (chkTrathuoc.Checked == true)
                                    {
                                        if (string.IsNullOrEmpty(a))
                                        {
                                            a += "trả thuốc";
                                        }
                                        else
                                        { a = a + " - " + " trả thuốc"; }
                                    }
                                    rep.Kieudon.Value = a;
                                    for (int i = 0; i < _DSDV.Count; i++)
                                    {
                                        switch (i)
                                        {
                                            case 0:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T1.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 1:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T2.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 2:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T3.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 3:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T4.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 4:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T5.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 5:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T6.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 6:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T7.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 7:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T8.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 8:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T9.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 9:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T10.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 10:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T11.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 11:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T12.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 12:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T13.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 13:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T14.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 14:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T15.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 15:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T16.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 16:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T17.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 17:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T18.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 18:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T19.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 19:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T20.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 20:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T21.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 21:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T22.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 22:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T23.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 23:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T24.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 24:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T25.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 25:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T26.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 26:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T27.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 27:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T28.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 28:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T29.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 29:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T30.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 30:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T31.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 31:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T32.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 32:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T33.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 33:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T34.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 34:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T35.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 35:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T36.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 36:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T37.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 37:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T38.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 38:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T39.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 39:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T40.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 40:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T41.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 41:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T42.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 42:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T43.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 43:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T44.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 44:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T45.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 45:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T46.Value = _DSDV.Skip(i).First().tendv; }
                                                break;


                                        }
                                    }

                                    rep.Ngaythang.Value = " Từ ngày: " + LupNgayTu.Text.Substring(0, 10) + " đến ngày: " + LupNgayDen.Text.Substring(0, 10);
                                    rep.Khoaphong.Value = LupKhoaPhong.Text;
                                    rep.DataSource = _Tamtra;
                                    rep.BindingData();
                                    rep.CreateDocument();
                                    //rep.DataMember = "Table";
                                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                    frm.ShowDialog();
                                }
                                #endregion tạo báo cáo A4 1 trang
                                if (_DSDV.Count > 46)
                                #region tạo báo cáo A4 2 trang
                                {
                                    BaoCao.Rep_TamTraThuoc rep = new BaoCao.Rep_TamTraThuoc();
                                    frmIn frm = new frmIn();
                                    string a = "Kiểu đơn: ";
                                    if (chkBoxung.Checked == true)
                                    { a += "bổ xung"; }
                                    if (chkThuongxuyen.Checked == true)
                                    {
                                        if (string.IsNullOrEmpty(a))
                                        {
                                            a += "thường xuyên";
                                        }
                                        else
                                        { a = a + " - " + " thường xuyên"; }
                                    }
                                    if (chkTrathuoc.Checked == true)
                                    {
                                        if (string.IsNullOrEmpty(a))
                                        {
                                            a += "trả thuốc";
                                        }
                                        else
                                        { a = a + " - " + " trả thuốc"; }
                                    }
                                    rep.Kieudon.Value = a;
                                    for (int i = 0; i < _DSDV.Count; i++)
                                    {
                                        switch (i)
                                        {
                                            case 0:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T1.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 1:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T2.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 2:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T3.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 3:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T4.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 4:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T5.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 5:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T6.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 6:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T7.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 7:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T8.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 8:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T9.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 9:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T10.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 10:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T11.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 11:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T12.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 12:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T13.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 13:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T14.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 14:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T15.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 15:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T16.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 16:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T17.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 17:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T18.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 18:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T19.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 19:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T20.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 20:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T21.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 21:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T22.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 22:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T23.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 23:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T24.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 24:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T25.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 25:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T26.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 26:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T27.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 27:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T28.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 28:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T29.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 29:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T30.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 30:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T31.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 31:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T32.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 32:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T33.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 33:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T34.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 34:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T35.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 35:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T36.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 36:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T37.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 37:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T38.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 38:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T39.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 39:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T40.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 40:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T41.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 41:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T42.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 42:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T43.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 43:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T44.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 44:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T45.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 45:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T46.Value = _DSDV.Skip(i).First().tendv; }
                                                break;


                                        }
                                    }

                                    rep.Ngaythang.Value = " Từ ngày: " + LupNgayTu.Text.Substring(0, 10) + " đến ngày: " + LupNgayDen.Text.Substring(0, 10);
                                    rep.Khoaphong.Value = LupKhoaPhong.Text;
                                    rep.DataSource = _Tamtra;
                                    rep.BindingData();
                                    rep.CreateDocument();
                                    //rep.DataMember = "Table";
                                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                    frm.ShowDialog();

                                    // tạo báo cáo thứ 2

                                    BaoCao.Rep_Tamtrathuoc02 rep1 = new BaoCao.Rep_Tamtrathuoc02();
                                    frmIn frm1 = new frmIn();
                                    a = "Kiểu đơn: ";
                                    if (chkBoxung.Checked == true)
                                    { a += " bổ xung "; }
                                    if (chkThuongxuyen.Checked == true)
                                    {
                                        if (string.IsNullOrEmpty(a))
                                        {
                                            a += " thường xuyên ";
                                        }
                                        else
                                        { a = a + " - " + " thường xuyên "; }
                                    }
                                    if (chkTrathuoc.Checked == true)
                                    {
                                        if (string.IsNullOrEmpty(a))
                                        {
                                            a += " trả thuốc ";
                                        }
                                        else
                                        { a = a + " - " + " trả thuốc "; }
                                    }
                                    rep1.Kieudon.Value = a;
                                    for (int i = 46; i < _DSDV.Count; i++)
                                    {
                                        switch (i)
                                        {
                                            case 46:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T1.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 47:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T2.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 48:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T3.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 49:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T4.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 50:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T5.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 51:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T6.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 52:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T7.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 53:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T8.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 54:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T9.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 55:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T10.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 56:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T11.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 57:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T12.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 58:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T13.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 59:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T14.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 60:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T15.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 61:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T16.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 62:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T17.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 63:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T18.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 64:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T19.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 65:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T20.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 66:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T21.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 67:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T22.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 68:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T23.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 69:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T24.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 70:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T25.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 71:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T26.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 72:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T27.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 73:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T28.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 74:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T29.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 75:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T30.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 76:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T31.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 77:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T32.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 78:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T33.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 79:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T34.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 80:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T35.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 81:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T36.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 82:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T37.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 83:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T38.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 84:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T39.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 85:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T40.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 86:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T41.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 87:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T42.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 88:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T43.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 89:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T44.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 90:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T45.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 91:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T46.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 92:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T47.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 93:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T48.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 94:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T49.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 95:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T50.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 96:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T51.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 97:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T52.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 98:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T53.Value = _DSDV.Skip(i).First().tendv; }
                                                break;

                                        }
                                    }

                                    rep1.Ngaythang.Value = " Từ ngày: " + LupNgayTu.Text.Substring(0, 10) + " đến ngày: " + LupNgayDen.Text.Substring(0, 10);
                                    rep1.Khoaphong.Value = LupKhoaPhong.Text;
                                    rep1.DataSource = _Tamtra;
                                    rep1.BindingData();
                                    rep1.CreateDocument();
                                    //rep.DataMember = "Table";
                                    frm1.prcIN.PrintingSystem = rep1.PrintingSystem;
                                    frm1.ShowDialog();
                                }
                                #endregion tạo báo cáo A4 2 trang
                            }
                        }
                    }
                }
                if (radNT.SelectedIndex == 2) //cả hai
                {
                    int _Status1 = 0;
                    int _Status2 = 0;
                    int TT = CboThuoc.SelectedIndex;
                    switch (TT)
                    {
                        case 0:
                            _Status1 = 1;
                            _Status2 = 1;
                            break;
                        case 1:
                            _Status1 = 0;
                            _Status2 = 0;
                            break;
                        case 2:
                            _Status1 = 1;
                            _Status2 = 0;
                            break;
                    }
                    int _MaKP = 0;
                    _Tamtra.Clear();
                    _DSDV.Clear();
                    if (LupKhoaPhong.Text != null)
                    {
                        _MaKP = Convert.ToInt32( LupKhoaPhong.EditValue);//ok
                    }
                    DateTime Ngaytu = DungChung.Ham.NgayTu(LupNgayTu.DateTime);
                    DateTime Ngayden = DungChung.Ham.NgayDen(LupNgayDen.DateTime);

                    if (chkThuoc.Checked == true)
                    {
                        var bn2 = (from bn1 in _Data.BenhNhans
                                   join DT in _Data.DThuocs.Where(p => p.KieuDon == _a1 || p.KieuDon == _a2 || p.KieuDon == _a3).Where(p => p.MaKP == _MaKP) on bn1.MaBNhan equals DT.MaBNhan
                                   join DTct in _Data.DThuoccts on DT.IDDon equals DTct.IDDon
                                   join DV in _Data.DichVus.Where(P => P.PLoai == 1) on DTct.MaDV equals DV.MaDV
                                   join Ndv in _Data.NhomDVs on DV.IDNhom equals Ndv.IDNhom
                                   join vv in _Data.VaoViens on bn1.MaBNhan equals vv.MaBNhan
                                   where (DT.NgayKe >= Ngaytu && DT.NgayKe <= Ngayden)
                                   where (DTct.Status == _Status1 || DTct.Status == _Status2)
                                   group new { DV, Ndv, DTct } by new { DV.MaDV, DV.TenDV, Ndv.TenNhomCT } into kq
                                   select new
                                   {
                                       TenDV = kq.Key.TenDV,
                                       MaDV = kq.Key.MaDV,
                                       TenN = kq.Key.TenNhomCT,
                                       SL = kq.Select(p => p.DTct.IDDonct).Count()
                                   }).OrderBy(p => p.TenDV).OrderBy(p => p.TenN).ToList();
                        if (bn2.Count > 0)
                        {
                            foreach (var a in bn2)
                            {
                                DSDV themmoi = new DSDV();
                                themmoi.tendv = a.TenDV;
                                themmoi.madv = a.MaDV;
                                _DSDV.Add(themmoi);
                            }
                        }

                        var MaBN = (from bn3 in _Data.BenhNhans
                                    join DT in _Data.DThuocs.Where(p => p.KieuDon == _a1 || p.KieuDon == _a2 || p.KieuDon == _a3).Where(p => p.MaKP == _MaKP) on bn3.MaBNhan equals DT.MaBNhan
                                    join DTct in _Data.DThuoccts on DT.IDDon equals DTct.IDDon
                                    join dv in _Data.DichVus.Where(p => p.PLoai == 1) on DTct.MaDV equals dv.MaDV
                                    join vv in _Data.VaoViens on bn3.MaBNhan equals vv.MaBNhan
                                    //join TN in _Data.NhomDVs.Where(p => p.TenNhomCT.Contains("Nhóm thuốc, hoá chất")) on dv.IDNhom equals TN.IDNhom
                                    where (DT.NgayKe >= Ngaytu && DT.NgayKe <= Ngayden)
                                    where (DTct.Status == _Status1 || DTct.Status == _Status2)
                                    group new { bn3 } by new { bn3.MaBNhan } into kq
                                    select new { MaBN = kq.Key.MaBNhan }).ToList();
                        if (MaBN.Count > 0)
                        {
                            foreach (var c in MaBN)
                            {
                                Tamtra themmoi = new Tamtra();
                                themmoi.mabn = c.MaBN;
                                _Tamtra.Add(themmoi);
                            }
                        }

                        var bn = (from bn1 in _Data.BenhNhans
                                  join DT in _Data.DThuocs.Where(p => p.KieuDon == _a1 || p.KieuDon == _a2 || p.KieuDon == _a3).Where(p => p.MaKP == _MaKP) on bn1.MaBNhan equals DT.MaBNhan
                                  join DTct in _Data.DThuoccts on DT.IDDon equals DTct.IDDon
                                  join DV in _Data.DichVus.Where(p => p.PLoai == 1) on DTct.MaDV equals DV.MaDV
                                  join vv in _Data.VaoViens on bn1.MaBNhan equals vv.MaBNhan
                                  where (DT.NgayKe >= Ngaytu && DT.NgayKe <= Ngayden)
                                  where (DTct.Status != null && (DTct.Status == _Status1 || DTct.Status == _Status2))
                                  group new { bn1, DTct } by new { DV.MaDV, bn1.TenBNhan, bn1.MaBNhan, bn1.Tuoi } into kq
                                  select new
                                  {
                                      kq.Key.Tuoi,
                                      Mabenhnhan = kq.Key.MaBNhan,
                                      MaDV = kq.Key.MaDV,
                                      SoLuong = kq.Sum(p => p.DTct.SoLuong),
                                      TenBN = kq.Key.TenBNhan,
                                  }).ToList();
                        if (bn.Count > 0)
                        {
                            #region tạo code báo cáo
                            foreach (var n in _Tamtra)
                            {
                                foreach (var m in bn)
                                {
                                    if (n.mabn == m.Mabenhnhan)
                                    {
                                        if (m.SoLuong != null && m.SoLuong != 0)
                                        {
                                            for (int i = 0; i < _DSDV.Count; i++)
                                            {
                                                if (m.MaDV == _DSDV.Skip(i).First().madv)
                                                {
                                                    switch (i)
                                                    {
                                                        case 0:
                                                            n.SL101 = m.SoLuong.ToString();
                                                            break;
                                                        case 1:
                                                            n.SL201 = m.SoLuong.ToString();
                                                            break;
                                                        case 2:
                                                            n.SL301 = m.SoLuong.ToString();
                                                            break;
                                                        case 3:
                                                            n.SL401 = m.SoLuong.ToString();
                                                            break;
                                                        case 4:
                                                            n.SL501 = m.SoLuong.ToString();
                                                            break;
                                                        case 5:
                                                            n.SL601 = m.SoLuong.ToString();
                                                            break;
                                                        case 6:
                                                            n.SL701 = m.SoLuong.ToString();
                                                            break;
                                                        case 7:
                                                            n.SL801 = m.SoLuong.ToString();
                                                            break;
                                                        case 8:
                                                            n.SL901 = m.SoLuong.ToString();
                                                            break;
                                                        case 9:
                                                            n.SL1001 = m.SoLuong.ToString();
                                                            break;
                                                        case 10:
                                                            n.SL1101 = m.SoLuong.ToString();
                                                            break;
                                                        case 11:
                                                            n.SL1201 = m.SoLuong.ToString();
                                                            break;
                                                        case 12:
                                                            n.SL1301 = m.SoLuong.ToString();
                                                            break;
                                                        case 13:
                                                            n.SL1401 = m.SoLuong.ToString();
                                                            break;
                                                        case 14:
                                                            n.SL1501 = m.SoLuong.ToString();
                                                            break;
                                                        case 15:
                                                            n.SL1601 = m.SoLuong.ToString();
                                                            break;
                                                        case 16:
                                                            n.SL1701 = m.SoLuong.ToString();
                                                            break;
                                                        case 17:
                                                            n.SL1801 = m.SoLuong.ToString();
                                                            break;
                                                        case 18:
                                                            n.SL1901 = m.SoLuong.ToString();
                                                            break;
                                                        case 19:
                                                            n.SL2001 = m.SoLuong.ToString();
                                                            break;
                                                        case 20:
                                                            n.SL2101 = m.SoLuong.ToString();
                                                            break;
                                                        case 21:
                                                            n.SL2201 = m.SoLuong.ToString();
                                                            break;
                                                        case 22:
                                                            n.SL2301 = m.SoLuong.ToString();
                                                            break;
                                                        case 23:
                                                            n.SL2401 = m.SoLuong.ToString();
                                                            break;
                                                        case 24:
                                                            n.SL2501 = m.SoLuong.ToString();
                                                            break;
                                                        case 25:
                                                            n.SL2601 = m.SoLuong.ToString();
                                                            break;
                                                        case 26:
                                                            n.SL2701 = m.SoLuong.ToString();
                                                            break;
                                                        case 27:
                                                            n.SL2801 = m.SoLuong.ToString();
                                                            break;
                                                        case 28:
                                                            n.SL2901 = m.SoLuong.ToString();
                                                            break;
                                                        case 29:
                                                            n.SL3001 = m.SoLuong.ToString();
                                                            break;
                                                        case 30:
                                                            n.SL3101 = m.SoLuong.ToString();
                                                            break;
                                                        case 31:
                                                            n.SL3201 = m.SoLuong.ToString();
                                                            break;
                                                        case 32:
                                                            n.SL3301 = m.SoLuong.ToString();
                                                            break;
                                                        case 33:
                                                            n.SL3401 = m.SoLuong.ToString();
                                                            break;
                                                        case 34:
                                                            n.SL3501 = m.SoLuong.ToString();
                                                            break;
                                                        case 35:
                                                            n.SL3601 = m.SoLuong.ToString();
                                                            break;
                                                        case 36:
                                                            n.SL3701 = m.SoLuong.ToString();
                                                            break;
                                                        case 37:
                                                            n.SL3801 = m.SoLuong.ToString();
                                                            break;
                                                        case 38:
                                                            n.SL3901 = m.SoLuong.ToString();
                                                            break;
                                                        case 39:
                                                            n.SL4001 = m.SoLuong.ToString();
                                                            break;
                                                        case 40:
                                                            n.SL4101 = m.SoLuong.ToString();
                                                            break;
                                                        case 41:
                                                            n.SL4201 = m.SoLuong.ToString();
                                                            break;
                                                        case 42:
                                                            n.SL4301 = m.SoLuong.ToString();
                                                            break;
                                                        case 43:
                                                            n.SL4401 = m.SoLuong.ToString();
                                                            break;
                                                        case 44:
                                                            n.SL4501 = m.SoLuong.ToString();
                                                            break;
                                                        case 45:
                                                            n.SL4601 = m.SoLuong.ToString();
                                                            break;
                                                        case 46:
                                                            n.SL4701 = m.SoLuong.ToString();
                                                            break;
                                                        case 47:
                                                            n.SL4801 = m.SoLuong.ToString();
                                                            break;
                                                        case 48:
                                                            n.SL4901 = m.SoLuong.ToString();
                                                            break;
                                                        case 49:
                                                            n.SL5001 = m.SoLuong.ToString();
                                                            break;
                                                        case 50:
                                                            n.SL5101 = m.SoLuong.ToString();
                                                            break;
                                                        case 51:
                                                            n.SL5201 = m.SoLuong.ToString();
                                                            break;
                                                        case 52:
                                                            n.SL5301 = m.SoLuong.ToString();
                                                            break;
                                                        case 53:
                                                            n.SL5401 = m.SoLuong.ToString();
                                                            break;
                                                        case 54:
                                                            n.SL5501 = m.SoLuong.ToString();
                                                            break;
                                                        case 55:
                                                            n.SL5601 = m.SoLuong.ToString();
                                                            break;
                                                        case 56:
                                                            n.SL5701 = m.SoLuong.ToString();
                                                            break;
                                                        case 57:
                                                            n.SL5801 = m.SoLuong.ToString();
                                                            break;
                                                        case 58:
                                                            n.SL5901 = m.SoLuong.ToString();
                                                            break;
                                                        case 59:
                                                            n.SL6001 = m.SoLuong.ToString();
                                                            break;
                                                        case 60:
                                                            n.SL6101 = m.SoLuong.ToString();
                                                            break;
                                                        case 61:
                                                            n.SL6201 = m.SoLuong.ToString();
                                                            break;
                                                        case 62:
                                                            n.SL6301 = m.SoLuong.ToString();
                                                            break;
                                                        case 63:
                                                            n.SL6401 = m.SoLuong.ToString();
                                                            break;
                                                        case 64:
                                                            n.SL6501 = m.SoLuong.ToString();
                                                            break;
                                                        case 65:
                                                            n.SL6601 = m.SoLuong.ToString();
                                                            break;
                                                        case 66:
                                                            n.SL6701 = m.SoLuong.ToString();
                                                            break;
                                                        case 67:
                                                            n.SL6801 = m.SoLuong.ToString();
                                                            break;
                                                        case 68:
                                                            n.SL6901 = m.SoLuong.ToString();
                                                            break;
                                                        case 69:
                                                            n.SL7001 = m.SoLuong.ToString();
                                                            break;
                                                        case 70:
                                                            n.SL7101 = m.SoLuong.ToString();
                                                            break;
                                                        case 71:
                                                            n.SL7201 = m.SoLuong.ToString();
                                                            break;
                                                        case 72:
                                                            n.SL7301 = m.SoLuong.ToString();
                                                            break;
                                                        case 73:
                                                            n.SL7401 = m.SoLuong.ToString();
                                                            break;
                                                        case 74:
                                                            n.SL7501 = m.SoLuong.ToString();
                                                            break;
                                                        case 75:
                                                            n.SL7601 = m.SoLuong.ToString();
                                                            break;
                                                        case 76:
                                                            n.SL7701 = m.SoLuong.ToString();
                                                            break;
                                                        case 77:
                                                            n.SL7801 = m.SoLuong.ToString();
                                                            break;
                                                        case 78:
                                                            n.SL7901 = m.SoLuong.ToString();
                                                            break;
                                                        case 79:
                                                            n.SL8001 = m.SoLuong.ToString();
                                                            break;
                                                        case 80:
                                                            n.SL8101 = m.SoLuong.ToString();
                                                            break;
                                                        case 81:
                                                            n.SL8201 = m.SoLuong.ToString();
                                                            break;
                                                        case 82:
                                                            n.SL8301 = m.SoLuong.ToString();
                                                            break;
                                                        case 83:
                                                            n.SL8401 = m.SoLuong.ToString();
                                                            break;
                                                        case 84:
                                                            n.SL8501 = m.SoLuong.ToString();
                                                            break;
                                                        case 85:
                                                            n.SL8601 = m.SoLuong.ToString();
                                                            break;
                                                        case 86:
                                                            n.SL8701 = m.SoLuong.ToString();
                                                            break;
                                                        case 87:
                                                            n.SL8801 = m.SoLuong.ToString();
                                                            break;
                                                        case 88:
                                                            n.SL8901 = m.SoLuong.ToString();
                                                            break;
                                                        case 89:
                                                            n.SL9001 = m.SoLuong.ToString();
                                                            break;
                                                        case 90:
                                                            n.SL9101 = m.SoLuong.ToString();
                                                            break;
                                                        case 91:
                                                            n.SL9201 = m.SoLuong.ToString();
                                                            break;
                                                        case 92:
                                                            n.SL9301 = m.SoLuong.ToString();
                                                            break;
                                                        case 93:
                                                            n.SL9401 = m.SoLuong.ToString();
                                                            break;
                                                        case 94:
                                                            n.SL9501 = m.SoLuong.ToString();
                                                            break;
                                                        case 95:
                                                            n.SL9601 = m.SoLuong.ToString();
                                                            break;
                                                        case 96:
                                                            n.SL9701 = m.SoLuong.ToString();
                                                            break;
                                                        case 97:
                                                            n.SL9801 = m.SoLuong.ToString();
                                                            break;
                                                        case 98:
                                                            n.SL9901 = m.SoLuong.ToString();
                                                            break;
                                                    }
                                                    n.tenbn = m.TenBN;
                                                    n.Tuoi = m.Tuoi.Value;
                                                }
                                            }
                                        }
                                    }
                                }

                            }
                            #endregion tạo code báo cáo

                            if (_DSDV.Count >= 100)
                            {
                                DialogResult _result = MessageBox.Show("Mẫu A4 không đủ để hiển thị, bạn có muốn in mẫu A3 không?", "Hỏi in", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                if (_result == DialogResult.Yes)
                                #region tạo báo cáo A3
                                {
                                    BaoCao.Rep_TamTraThuocA3 rep = new BaoCao.Rep_TamTraThuocA3();
                                    frmIn frm = new frmIn();
                                    for (int i = 0; i < _DSDV.Count; i++)
                                    {
                                        switch (i)
                                        {
                                            case 0:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T1.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 1:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T2.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 2:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T3.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 3:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T4.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 4:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T5.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 5:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T6.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 6:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T7.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 7:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T8.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 8:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T9.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 9:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T10.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 10:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T11.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 11:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T12.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 12:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T13.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 13:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T14.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 14:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T15.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 15:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T16.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 16:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T17.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 17:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T18.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 18:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T19.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 19:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T20.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 20:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T21.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 21:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T22.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 22:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T23.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 23:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T24.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 24:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T25.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 25:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T26.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 26:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T27.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 27:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T28.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 28:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T29.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 29:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T30.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 30:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T31.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 31:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T32.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 32:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T33.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 33:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T34.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 34:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T35.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 35:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T36.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 36:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T37.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 37:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T38.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 38:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T39.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 39:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T40.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 40:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T41.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 41:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T42.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 42:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T43.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 43:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T44.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 44:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T45.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 45:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T46.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 46:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T47.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 47:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T48.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 48:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T49.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 49:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T50.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 50:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T51.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 51:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T52.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 52:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T53.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 53:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T54.Value = _DSDV.Skip(i).First().tendv; }
                                                break;

                                        }
                                    }

                                    rep.Ngaythang.Value = " Từ ngày: " + LupNgayTu.Text.Substring(0, 10) + " đến ngày: " + LupNgayDen.Text.Substring(0, 10);
                                    rep.KhoaPhong.Value = LupKhoaPhong.Text;
                                    rep.DataSource = _Tamtra;
                                    rep.BindingData();
                                    rep.CreateDocument();
                                    //rep.DataMember = "Table";
                                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                    frm.ShowDialog();
                                }
                                #endregion tạo báo cáo A3
                                else
                                {
                                    if (_DSDV.Count <= 46)
                                    #region tạo báo cáo A4 1 trang
                                    {
                                        BaoCao.Rep_TamTraThuoc rep = new BaoCao.Rep_TamTraThuoc();
                                        frmIn frm = new frmIn();
                                        string a = "Kiểu đơn: ";
                                        if (chkBoxung.Checked == true)
                                        { a += "bổ xung"; }
                                        if (chkThuongxuyen.Checked == true)
                                        {
                                            if (string.IsNullOrEmpty(a))
                                            {
                                                a += "thường xuyên";
                                            }
                                            else
                                            { a = a + " - " + " thường xuyên"; }
                                        }
                                        if (chkTrathuoc.Checked == true)
                                        {
                                            if (string.IsNullOrEmpty(a))
                                            {
                                                a += "trả thuốc";
                                            }
                                            else
                                            { a = a + " - " + " trả thuốc"; }
                                        }
                                        rep.Kieudon.Value = a;
                                        for (int i = 0; i < _DSDV.Count; i++)
                                        {
                                            switch (i)
                                            {
                                                case 0:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T1.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 1:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T2.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 2:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T3.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 3:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T4.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 4:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T5.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 5:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T6.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 6:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T7.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 7:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T8.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 8:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T9.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 9:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T10.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 10:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T11.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 11:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T12.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 12:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T13.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 13:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T14.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 14:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T15.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 15:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T16.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 16:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T17.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 17:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T18.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 18:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T19.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 19:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T20.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 20:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T21.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 21:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T22.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 22:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T23.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 23:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T24.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 24:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T25.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 25:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T26.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 26:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T27.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 27:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T28.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 28:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T29.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 29:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T30.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 30:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T31.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 31:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T32.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 32:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T33.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 33:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T34.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 34:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T35.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 35:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T36.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 36:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T37.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 37:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T38.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 38:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T39.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 39:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T40.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 40:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T41.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 41:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T42.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 42:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T43.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 43:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T44.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 44:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T45.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 45:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T46.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;


                                            }
                                        }

                                        rep.Ngaythang.Value = " Từ ngày: " + LupNgayTu.Text.Substring(0, 10) + " đến ngày: " + LupNgayDen.Text.Substring(0, 10);
                                        rep.Khoaphong.Value = LupKhoaPhong.Text;
                                        rep.DataSource = _Tamtra;
                                        rep.BindingData();
                                        rep.CreateDocument();
                                        //rep.DataMember = "Table";
                                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                        frm.ShowDialog();

                                    }
                                    #endregion tạo báo cáo A4 1 trang
                                    if (_DSDV.Count > 46)
                                    #region tạo báo cáo A4 2 trang
                                    {
                                        BaoCao.Rep_TamTraThuoc rep = new BaoCao.Rep_TamTraThuoc();
                                        frmIn frm = new frmIn();
                                        string a = "Kiểu đơn: ";
                                        if (chkBoxung.Checked == true)
                                        { a += "bổ xung "; }
                                        if (chkThuongxuyen.Checked == true)
                                        {
                                            if (string.IsNullOrEmpty(a))
                                            {
                                                a += "thường xuyên ";
                                            }
                                            else
                                            { a = a + " - " + " thường xuyên "; }
                                        }
                                        if (chkTrathuoc.Checked == true)
                                        {
                                            if (string.IsNullOrEmpty(a))
                                            {
                                                a += "trả thuốc ";
                                            }
                                            else
                                            { a = a + " - " + " trả thuốc "; }
                                        }
                                        rep.Kieudon.Value = a;
                                        for (int i = 0; i < _DSDV.Count; i++)
                                        {
                                            switch (i)
                                            {
                                                case 0:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T1.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 1:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T2.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 2:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T3.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 3:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T4.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 4:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T5.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 5:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T6.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 6:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T7.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 7:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T8.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 8:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T9.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 9:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T10.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 10:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T11.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 11:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T12.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 12:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T13.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 13:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T14.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 14:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T15.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 15:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T16.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 16:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T17.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 17:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T18.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 18:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T19.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 19:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T20.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 20:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T21.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 21:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T22.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 22:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T23.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 23:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T24.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 24:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T25.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 25:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T26.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 26:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T27.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 27:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T28.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 28:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T29.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 29:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T30.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 30:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T31.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 31:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T32.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 32:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T33.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 33:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T34.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 34:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T35.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 35:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T36.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 36:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T37.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 37:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T38.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 38:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T39.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 39:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T40.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 40:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T41.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 41:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T42.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 42:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T43.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 43:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T44.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 44:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T45.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 45:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T46.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;


                                            }
                                        }

                                        rep.Ngaythang.Value = " Từ ngày: " + LupNgayTu.Text.Substring(0, 10) + " đến ngày: " + LupNgayDen.Text.Substring(0, 10);
                                        rep.Khoaphong.Value = LupKhoaPhong.Text;
                                        rep.DataSource = _Tamtra;
                                        rep.BindingData();
                                        rep.CreateDocument();
                                        //rep.DataMember = "Table";
                                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                        frm.ShowDialog();

                                        // tạo báo cáo thứ 2

                                        BaoCao.Rep_Tamtrathuoc02 rep1 = new BaoCao.Rep_Tamtrathuoc02();
                                        frmIn frm1 = new frmIn();
                                        a = "Kiểu đơn: ";
                                        if (chkBoxung.Checked == true)
                                        { a += " bổ xung "; }
                                        if (chkThuongxuyen.Checked == true)
                                        {
                                            if (string.IsNullOrEmpty(a))
                                            {
                                                a += " thường xuyên ";
                                            }
                                            else
                                            { a = a + " - " + " thường xuyên "; }
                                        }
                                        if (chkTrathuoc.Checked == true)
                                        {
                                            if (string.IsNullOrEmpty(a))
                                            {
                                                a += " trả thuốc ";
                                            }
                                            else
                                            { a = a + " - " + " trả thuốc "; }
                                        }
                                        rep1.Kieudon.Value = a;
                                        for (int i = 46; i < _DSDV.Count; i++)
                                        {
                                            switch (i)
                                            {
                                                case 46:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T1.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 47:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T2.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 48:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T3.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 49:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T4.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 50:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T5.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 51:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T6.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 52:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T7.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 53:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T8.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 54:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T9.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 55:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T10.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 56:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T11.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 57:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T12.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 58:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T13.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 59:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T14.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 60:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T15.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 61:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T16.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 62:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T17.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 63:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T18.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 64:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T19.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 65:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T20.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 66:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T21.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 67:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T22.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 68:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T23.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 69:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T24.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 70:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T25.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 71:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T26.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 72:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T27.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 73:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T28.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 74:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T29.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 75:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T30.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 76:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T31.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 77:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T32.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 78:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T33.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 79:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T34.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 80:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T35.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 81:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T36.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 82:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T37.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 83:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T38.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 84:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T39.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 85:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T40.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 86:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T41.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 87:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T42.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 88:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T43.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 89:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T44.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 90:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T45.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 91:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T46.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 92:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T47.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 93:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T48.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 94:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T49.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 95:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T50.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 96:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T51.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 97:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T52.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 98:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T53.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;

                                            }
                                        }

                                        rep1.Ngaythang.Value = " Từ ngày: " + LupNgayTu.Text.Substring(0, 10) + " đến ngày: " + LupNgayDen.Text.Substring(0, 10);
                                        rep1.Khoaphong.Value = LupKhoaPhong.Text;
                                        rep1.DataSource = _Tamtra;
                                        rep1.BindingData();
                                        rep1.CreateDocument();
                                        //rep.DataMember = "Table";
                                        frm1.prcIN.PrintingSystem = rep1.PrintingSystem;
                                        frm1.ShowDialog();
                                    }
                                    #endregion tạo báo cáo A4 2 trang
                                }
                            }
                            else
                            {
                                if (_DSDV.Count <= 46)
                                #region tạo báo cáo A4 1 trang
                                {
                                    BaoCao.Rep_TamTraThuoc rep = new BaoCao.Rep_TamTraThuoc();
                                    frmIn frm = new frmIn();
                                    string a = "Kiểu đơn: ";
                                    if (chkBoxung.Checked == true)
                                    { a += "bổ xung"; }
                                    if (chkThuongxuyen.Checked == true)
                                    {
                                        if (string.IsNullOrEmpty(a))
                                        {
                                            a += "thường xuyên";
                                        }
                                        else
                                        { a = a + " - " + " thường xuyên"; }
                                    }
                                    if (chkTrathuoc.Checked == true)
                                    {
                                        if (string.IsNullOrEmpty(a))
                                        {
                                            a += "trả thuốc";
                                        }
                                        else
                                        { a = a + " - " + " trả thuốc"; }
                                    }
                                    rep.Kieudon.Value = a;
                                    for (int i = 0; i < _DSDV.Count; i++)
                                    {
                                        switch (i)
                                        {
                                            case 0:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T1.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 1:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T2.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 2:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T3.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 3:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T4.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 4:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T5.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 5:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T6.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 6:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T7.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 7:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T8.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 8:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T9.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 9:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T10.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 10:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T11.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 11:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T12.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 12:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T13.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 13:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T14.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 14:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T15.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 15:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T16.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 16:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T17.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 17:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T18.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 18:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T19.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 19:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T20.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 20:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T21.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 21:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T22.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 22:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T23.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 23:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T24.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 24:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T25.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 25:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T26.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 26:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T27.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 27:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T28.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 28:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T29.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 29:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T30.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 30:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T31.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 31:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T32.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 32:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T33.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 33:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T34.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 34:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T35.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 35:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T36.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 36:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T37.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 37:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T38.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 38:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T39.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 39:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T40.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 40:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T41.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 41:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T42.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 42:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T43.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 43:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T44.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 44:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T45.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 45:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T46.Value = _DSDV.Skip(i).First().tendv; }
                                                break;


                                        }
                                    }

                                    rep.Ngaythang.Value = " Từ ngày: " + LupNgayTu.Text.Substring(0, 10) + " đến ngày: " + LupNgayDen.Text.Substring(0, 10);
                                    rep.Khoaphong.Value = LupKhoaPhong.Text;
                                    rep.DataSource = _Tamtra;
                                    rep.BindingData();
                                    rep.CreateDocument();
                                    //rep.DataMember = "Table";
                                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                    frm.ShowDialog();

                                }
                                #endregion tạo báo cáo A4 1 trang
                                if (_DSDV.Count > 46)
                                #region tạo báo cáo A4 2 trang
                                {
                                    BaoCao.Rep_TamTraThuoc rep = new BaoCao.Rep_TamTraThuoc();
                                    frmIn frm = new frmIn();
                                    string a = "Kiểu đơn: ";
                                    if (chkBoxung.Checked == true)
                                    { a += "bổ xung "; }
                                    if (chkThuongxuyen.Checked == true)
                                    {
                                        if (string.IsNullOrEmpty(a))
                                        {
                                            a += "thường xuyên ";
                                        }
                                        else
                                        { a = a + " - " + " thường xuyên "; }
                                    }
                                    if (chkTrathuoc.Checked == true)
                                    {
                                        if (string.IsNullOrEmpty(a))
                                        {
                                            a += "trả thuốc ";
                                        }
                                        else
                                        { a = a + " - " + " trả thuốc "; }
                                    }
                                    rep.Kieudon.Value = a;
                                    for (int i = 0; i < _DSDV.Count; i++)
                                    {
                                        switch (i)
                                        {
                                            case 0:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T1.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 1:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T2.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 2:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T3.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 3:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T4.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 4:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T5.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 5:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T6.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 6:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T7.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 7:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T8.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 8:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T9.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 9:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T10.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 10:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T11.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 11:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T12.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 12:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T13.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 13:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T14.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 14:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T15.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 15:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T16.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 16:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T17.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 17:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T18.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 18:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T19.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 19:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T20.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 20:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T21.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 21:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T22.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 22:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T23.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 23:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T24.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 24:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T25.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 25:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T26.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 26:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T27.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 27:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T28.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 28:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T29.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 29:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T30.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 30:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T31.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 31:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T32.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 32:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T33.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 33:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T34.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 34:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T35.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 35:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T36.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 36:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T37.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 37:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T38.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 38:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T39.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 39:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T40.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 40:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T41.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 41:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T42.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 42:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T43.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 43:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T44.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 44:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T45.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 45:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T46.Value = _DSDV.Skip(i).First().tendv; }
                                                break;


                                        }
                                    }

                                    rep.Ngaythang.Value = " Từ ngày: " + LupNgayTu.Text.Substring(0, 10) + " đến ngày: " + LupNgayDen.Text.Substring(0, 10);
                                    rep.Khoaphong.Value = LupKhoaPhong.Text;
                                    rep.DataSource = _Tamtra;
                                    rep.BindingData();
                                    rep.CreateDocument();
                                    //rep.DataMember = "Table";
                                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                    frm.ShowDialog();

                                    // tạo báo cáo thứ 2

                                    BaoCao.Rep_Tamtrathuoc02 rep1 = new BaoCao.Rep_Tamtrathuoc02();
                                    frmIn frm1 = new frmIn();
                                    a = "Kiểu đơn: ";
                                    if (chkBoxung.Checked == true)
                                    { a += " bổ xung "; }
                                    if (chkThuongxuyen.Checked == true)
                                    {
                                        if (string.IsNullOrEmpty(a))
                                        {
                                            a += " thường xuyên ";
                                        }
                                        else
                                        { a = a + " - " + " thường xuyên "; }
                                    }
                                    if (chkTrathuoc.Checked == true)
                                    {
                                        if (string.IsNullOrEmpty(a))
                                        {
                                            a += " trả thuốc ";
                                        }
                                        else
                                        { a = a + " - " + " trả thuốc "; }
                                    }
                                    rep1.Kieudon.Value = a;
                                    for (int i = 46; i < _DSDV.Count; i++)
                                    {
                                        switch (i)
                                        {
                                            case 46:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T1.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 47:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T2.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 48:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T3.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 49:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T4.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 50:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T5.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 51:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T6.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 52:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T7.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 53:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T8.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 54:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T9.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 55:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T10.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 56:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T11.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 57:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T12.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 58:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T13.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 59:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T14.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 60:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T15.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 61:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T16.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 62:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T17.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 63:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T18.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 64:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T19.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 65:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T20.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 66:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T21.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 67:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T22.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 68:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T23.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 69:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T24.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 70:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T25.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 71:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T26.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 72:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T27.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 73:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T28.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 74:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T29.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 75:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T30.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 76:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T31.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 77:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T32.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 78:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T33.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 79:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T34.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 80:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T35.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 81:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T36.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 82:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T37.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 83:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T38.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 84:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T39.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 85:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T40.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 86:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T41.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 87:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T42.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 88:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T43.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 89:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T44.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 90:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T45.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 91:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T46.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 92:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T47.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 93:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T48.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 94:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T49.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 95:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T50.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 96:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T51.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 97:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T52.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 98:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T53.Value = _DSDV.Skip(i).First().tendv; }
                                                break;

                                        }
                                    }

                                    rep1.Ngaythang.Value = " Từ ngày: " + LupNgayTu.Text.Substring(0, 10) + " đến ngày: " + LupNgayDen.Text.Substring(0, 10);
                                    rep1.Khoaphong.Value = LupKhoaPhong.Text;
                                    rep1.DataSource = _Tamtra;
                                    rep1.BindingData();
                                    rep1.CreateDocument();
                                    //rep.DataMember = "Table";
                                    frm1.prcIN.PrintingSystem = rep1.PrintingSystem;
                                    frm1.ShowDialog();
                                }
                                #endregion tạo báo cáo A4 2 trang
                            }
                        }
                    }
                    else
                    {
                        var bn2 = (from bn1 in _Data.BenhNhans
                                   join DT in _Data.DThuocs.Where(p => p.KieuDon == _a1 || p.KieuDon == _a2 || p.KieuDon == _a3).Where(p => p.MaKP == _MaKP) on bn1.MaBNhan equals DT.MaBNhan
                                   join DTct in _Data.DThuoccts.Where(p => p.TrongBH == 1) on DT.IDDon equals DTct.IDDon
                                   join DV in _Data.DichVus.Where(P => P.PLoai == 1) on DTct.MaDV equals DV.MaDV
                                   join Ndv in _Data.NhomDVs on DV.IDNhom equals Ndv.IDNhom
                                   join vv in _Data.VaoViens on bn1.MaBNhan equals vv.MaBNhan
                                   where (DT.NgayKe >= Ngaytu && DT.NgayKe <= Ngayden)
                                   //where (DT.Status == _Status1)
                                   where (DTct.Status != null && (DTct.Status == _Status1 || DTct.Status == _Status2))
                                   group new { DV, Ndv } by new { DV.MaDV, DV.TenDV, Ndv.TenNhomCT } into kq
                                   select new
                                   {
                                       TenN = kq.Key.TenNhomCT,
                                       TenDV = kq.Key.TenDV,
                                       MaDV = kq.Key.MaDV,
                                   }).OrderBy(p => p.TenDV).OrderBy(p => p.TenN).ToList();
                        if (bn2.Count > 0)
                        {
                            foreach (var a in bn2)
                            {
                                DSDV themmoi = new DSDV();
                                themmoi.tendv = a.TenDV;
                                themmoi.madv = a.MaDV;
                                _DSDV.Add(themmoi);
                            }
                        }

                        var MaBN = (from bn3 in _Data.BenhNhans
                                    join DT in _Data.DThuocs.Where(p => p.KieuDon == _a1 || p.KieuDon == _a2 || p.KieuDon == _a3).Where(p => p.MaKP == _MaKP) on bn3.MaBNhan equals DT.MaBNhan
                                    join DTct in _Data.DThuoccts.Where(p => p.TrongBH == 1) on DT.IDDon equals DTct.IDDon
                                    join dv in _Data.DichVus.Where(p => p.PLoai == 1) on DTct.MaDV equals dv.MaDV
                                    join vv in _Data.VaoViens on bn3.MaBNhan equals vv.MaBNhan
                                    where (DT.NgayKe >= Ngaytu && DT.NgayKe <= Ngayden)
                                    //where (DT.Status == _Status1)
                                    where (DTct.Status == _Status1 || DTct.Status == _Status2)
                                    group new { bn3 } by new { bn3.MaBNhan } into kq
                                    select new { MaBN = kq.Key.MaBNhan }).ToList();
                        if (MaBN.Count > 0)
                        {
                            foreach (var c in MaBN)
                            {
                                Tamtra themmoi = new Tamtra();
                                themmoi.mabn = c.MaBN;
                                _Tamtra.Add(themmoi);
                            }
                        }
                        var bn = (from bn1 in _Data.BenhNhans
                                  join DT in _Data.DThuocs.Where(p => p.KieuDon == _a1 || p.KieuDon == _a2 || p.KieuDon == _a3).Where(p => p.MaKP == _MaKP) on bn1.MaBNhan equals DT.MaBNhan
                                  join DTct in _Data.DThuoccts.Where(p => p.TrongBH == 1) on DT.IDDon equals DTct.IDDon
                                  join DV in _Data.DichVus.Where(p => p.PLoai == 1) on DTct.MaDV equals DV.MaDV
                                  join vv in _Data.VaoViens on bn1.MaBNhan equals vv.MaBNhan
                                  where (DT.NgayKe >= Ngaytu && DT.NgayKe <= Ngayden)
                                  //where (DT.Status == _Status1)
                                  where (DTct.Status == _Status1 || DTct.Status == _Status2)
                                  group new { bn1, DTct } by new { DV.MaDV, bn1.TenBNhan, bn1.MaBNhan, bn1.Tuoi } into kq
                                  select new
                                  {
                                      kq.Key.Tuoi,
                                      Mabenhnhan = kq.Key.MaBNhan,
                                      MaDV = kq.Key.MaDV,
                                      SoLuong = kq.Sum(p => p.DTct.SoLuong),
                                      TenBN = kq.Key.TenBNhan,
                                  }).ToList();
                        if (bn.Count > 0)
                        {
                            #region tạo code báo cáo
                            foreach (var n in _Tamtra)
                            {
                                foreach (var m in bn)
                                {
                                    if (n.mabn == m.Mabenhnhan)
                                    {
                                        if (m.SoLuong != null && m.SoLuong != 0)
                                        {
                                            for (int i = 0; i < _DSDV.Count; i++)
                                            {
                                                if (m.MaDV == _DSDV.Skip(i).First().madv)
                                                {
                                                    switch (i)
                                                    {
                                                        case 0:
                                                            n.SL101 = m.SoLuong.ToString();
                                                            break;
                                                        case 1:
                                                            n.SL201 = m.SoLuong.ToString();
                                                            break;
                                                        case 2:
                                                            n.SL301 = m.SoLuong.ToString();
                                                            break;
                                                        case 3:
                                                            n.SL401 = m.SoLuong.ToString();
                                                            break;
                                                        case 4:
                                                            n.SL501 = m.SoLuong.ToString();
                                                            break;
                                                        case 5:
                                                            n.SL601 = m.SoLuong.ToString();
                                                            break;
                                                        case 6:
                                                            n.SL701 = m.SoLuong.ToString();
                                                            break;
                                                        case 7:
                                                            n.SL801 = m.SoLuong.ToString();
                                                            break;
                                                        case 8:
                                                            n.SL901 = m.SoLuong.ToString();
                                                            break;
                                                        case 9:
                                                            n.SL1001 = m.SoLuong.ToString();
                                                            break;
                                                        case 10:
                                                            n.SL1101 = m.SoLuong.ToString();
                                                            break;
                                                        case 11:
                                                            n.SL1201 = m.SoLuong.ToString();
                                                            break;
                                                        case 12:
                                                            n.SL1301 = m.SoLuong.ToString();
                                                            break;
                                                        case 13:
                                                            n.SL1401 = m.SoLuong.ToString();
                                                            break;
                                                        case 14:
                                                            n.SL1501 = m.SoLuong.ToString();
                                                            break;
                                                        case 15:
                                                            n.SL1601 = m.SoLuong.ToString();
                                                            break;
                                                        case 16:
                                                            n.SL1701 = m.SoLuong.ToString();
                                                            break;
                                                        case 17:
                                                            n.SL1801 = m.SoLuong.ToString();
                                                            break;
                                                        case 18:
                                                            n.SL1901 = m.SoLuong.ToString();
                                                            break;
                                                        case 19:
                                                            n.SL2001 = m.SoLuong.ToString();
                                                            break;
                                                        case 20:
                                                            n.SL2101 = m.SoLuong.ToString();
                                                            break;
                                                        case 21:
                                                            n.SL2201 = m.SoLuong.ToString();
                                                            break;
                                                        case 22:
                                                            n.SL2301 = m.SoLuong.ToString();
                                                            break;
                                                        case 23:
                                                            n.SL2401 = m.SoLuong.ToString();
                                                            break;
                                                        case 24:
                                                            n.SL2501 = m.SoLuong.ToString();
                                                            break;
                                                        case 25:
                                                            n.SL2601 = m.SoLuong.ToString();
                                                            break;
                                                        case 26:
                                                            n.SL2701 = m.SoLuong.ToString();
                                                            break;
                                                        case 27:
                                                            n.SL2801 = m.SoLuong.ToString();
                                                            break;
                                                        case 28:
                                                            n.SL2901 = m.SoLuong.ToString();
                                                            break;
                                                        case 29:
                                                            n.SL3001 = m.SoLuong.ToString();
                                                            break;
                                                        case 30:
                                                            n.SL3101 = m.SoLuong.ToString();
                                                            break;
                                                        case 31:
                                                            n.SL3201 = m.SoLuong.ToString();
                                                            break;
                                                        case 32:
                                                            n.SL3301 = m.SoLuong.ToString();
                                                            break;
                                                        case 33:
                                                            n.SL3401 = m.SoLuong.ToString();
                                                            break;
                                                        case 34:
                                                            n.SL3501 = m.SoLuong.ToString();
                                                            break;
                                                        case 35:
                                                            n.SL3601 = m.SoLuong.ToString();
                                                            break;
                                                        case 36:
                                                            n.SL3701 = m.SoLuong.ToString();
                                                            break;
                                                        case 37:
                                                            n.SL3801 = m.SoLuong.ToString();
                                                            break;
                                                        case 38:
                                                            n.SL3901 = m.SoLuong.ToString();
                                                            break;
                                                        case 39:
                                                            n.SL4001 = m.SoLuong.ToString();
                                                            break;
                                                        case 40:
                                                            n.SL4101 = m.SoLuong.ToString();
                                                            break;
                                                        case 41:
                                                            n.SL4201 = m.SoLuong.ToString();
                                                            break;
                                                        case 42:
                                                            n.SL4301 = m.SoLuong.ToString();
                                                            break;
                                                        case 43:
                                                            n.SL4401 = m.SoLuong.ToString();
                                                            break;
                                                        case 44:
                                                            n.SL4501 = m.SoLuong.ToString();
                                                            break;
                                                        case 45:
                                                            n.SL4601 = m.SoLuong.ToString();
                                                            break;
                                                        case 46:
                                                            n.SL4701 = m.SoLuong.ToString();
                                                            break;
                                                        case 47:
                                                            n.SL4801 = m.SoLuong.ToString();
                                                            break;
                                                        case 48:
                                                            n.SL4901 = m.SoLuong.ToString();
                                                            break;
                                                        case 49:
                                                            n.SL5001 = m.SoLuong.ToString();
                                                            break;
                                                        case 50:
                                                            n.SL5101 = m.SoLuong.ToString();
                                                            break;
                                                        case 51:
                                                            n.SL5201 = m.SoLuong.ToString();
                                                            break;
                                                        case 52:
                                                            n.SL5301 = m.SoLuong.ToString();
                                                            break;
                                                        case 53:
                                                            n.SL5401 = m.SoLuong.ToString();
                                                            break;
                                                        case 54:
                                                            n.SL5501 = m.SoLuong.ToString();
                                                            break;
                                                        case 55:
                                                            n.SL5601 = m.SoLuong.ToString();
                                                            break;
                                                        case 56:
                                                            n.SL5701 = m.SoLuong.ToString();
                                                            break;
                                                        case 57:
                                                            n.SL5801 = m.SoLuong.ToString();
                                                            break;
                                                        case 58:
                                                            n.SL5901 = m.SoLuong.ToString();
                                                            break;
                                                        case 59:
                                                            n.SL6001 = m.SoLuong.ToString();
                                                            break;
                                                        case 60:
                                                            n.SL6101 = m.SoLuong.ToString();
                                                            break;
                                                        case 61:
                                                            n.SL6201 = m.SoLuong.ToString();
                                                            break;
                                                        case 62:
                                                            n.SL6301 = m.SoLuong.ToString();
                                                            break;
                                                        case 63:
                                                            n.SL6401 = m.SoLuong.ToString();
                                                            break;
                                                        case 64:
                                                            n.SL6501 = m.SoLuong.ToString();
                                                            break;
                                                        case 65:
                                                            n.SL6601 = m.SoLuong.ToString();
                                                            break;
                                                        case 66:
                                                            n.SL6701 = m.SoLuong.ToString();
                                                            break;
                                                        case 67:
                                                            n.SL6801 = m.SoLuong.ToString();
                                                            break;
                                                        case 68:
                                                            n.SL6901 = m.SoLuong.ToString();
                                                            break;
                                                        case 69:
                                                            n.SL7001 = m.SoLuong.ToString();
                                                            break;
                                                        case 70:
                                                            n.SL7101 = m.SoLuong.ToString();
                                                            break;
                                                        case 71:
                                                            n.SL7201 = m.SoLuong.ToString();
                                                            break;
                                                        case 72:
                                                            n.SL7301 = m.SoLuong.ToString();
                                                            break;
                                                        case 73:
                                                            n.SL7401 = m.SoLuong.ToString();
                                                            break;
                                                        case 74:
                                                            n.SL7501 = m.SoLuong.ToString();
                                                            break;
                                                        case 75:
                                                            n.SL7601 = m.SoLuong.ToString();
                                                            break;
                                                        case 76:
                                                            n.SL7701 = m.SoLuong.ToString();
                                                            break;
                                                        case 77:
                                                            n.SL7801 = m.SoLuong.ToString();
                                                            break;
                                                        case 78:
                                                            n.SL7901 = m.SoLuong.ToString();
                                                            break;
                                                        case 79:
                                                            n.SL8001 = m.SoLuong.ToString();
                                                            break;
                                                        case 80:
                                                            n.SL8101 = m.SoLuong.ToString();
                                                            break;
                                                        case 81:
                                                            n.SL8201 = m.SoLuong.ToString();
                                                            break;
                                                        case 82:
                                                            n.SL8301 = m.SoLuong.ToString();
                                                            break;
                                                        case 83:
                                                            n.SL8401 = m.SoLuong.ToString();
                                                            break;
                                                        case 84:
                                                            n.SL8501 = m.SoLuong.ToString();
                                                            break;
                                                        case 85:
                                                            n.SL8601 = m.SoLuong.ToString();
                                                            break;
                                                        case 86:
                                                            n.SL8701 = m.SoLuong.ToString();
                                                            break;
                                                        case 87:
                                                            n.SL8801 = m.SoLuong.ToString();
                                                            break;
                                                        case 88:
                                                            n.SL8901 = m.SoLuong.ToString();
                                                            break;
                                                        case 89:
                                                            n.SL9001 = m.SoLuong.ToString();
                                                            break;
                                                        case 90:
                                                            n.SL9101 = m.SoLuong.ToString();
                                                            break;
                                                        case 91:
                                                            n.SL9201 = m.SoLuong.ToString();
                                                            break;
                                                        case 92:
                                                            n.SL9301 = m.SoLuong.ToString();
                                                            break;
                                                        case 93:
                                                            n.SL9401 = m.SoLuong.ToString();
                                                            break;
                                                        case 94:
                                                            n.SL9501 = m.SoLuong.ToString();
                                                            break;
                                                        case 95:
                                                            n.SL9601 = m.SoLuong.ToString();
                                                            break;
                                                        case 96:
                                                            n.SL9701 = m.SoLuong.ToString();
                                                            break;
                                                        case 97:
                                                            n.SL9801 = m.SoLuong.ToString();
                                                            break;
                                                        case 98:
                                                            n.SL9901 = m.SoLuong.ToString();
                                                            break;
                                                    }
                                                    n.tenbn = m.TenBN;
                                                    n.Tuoi = m.Tuoi.Value;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            #endregion tạo code báo cáo
                            if (_DSDV.Count >= 100)
                            {
                                DialogResult _result = MessageBox.Show("Mẫu A4 không đủ để hiển thị, bạn có muốn in mẫu A3 không?", "Hỏi in", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                if (_result == DialogResult.Yes)
                                #region tạo báo cáo A3
                                {
                                    BaoCao.Rep_TamTraThuocA3 rep = new BaoCao.Rep_TamTraThuocA3();
                                    frmIn frm = new frmIn();
                                    for (int i = 0; i < _DSDV.Count; i++)
                                    {
                                        switch (i)
                                        {
                                            case 0:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T1.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 1:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T2.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 2:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T3.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 3:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T4.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 4:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T5.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 5:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T6.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 6:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T7.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 7:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T8.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 8:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T9.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 9:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T10.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 10:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T11.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 11:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T12.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 12:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T13.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 13:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T14.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 14:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T15.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 15:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T16.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 16:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T17.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 17:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T18.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 18:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T19.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 19:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T20.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 20:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T21.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 21:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T22.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 22:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T23.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 23:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T24.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 24:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T25.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 25:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T26.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 26:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T27.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 27:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T28.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 28:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T29.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 29:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T30.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 30:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T31.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 31:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T32.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 32:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T33.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 33:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T34.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 34:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T35.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 35:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T36.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 36:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T37.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 37:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T38.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 38:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T39.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 39:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T40.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 40:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T41.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 41:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T42.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 42:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T43.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 43:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T44.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 44:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T45.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 45:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T46.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 46:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T47.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 47:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T48.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 48:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T49.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 49:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T50.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 50:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T51.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 51:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T52.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 52:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T53.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 53:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T54.Value = _DSDV.Skip(i).First().tendv; }
                                                break;

                                        }
                                    }

                                    rep.Ngaythang.Value = " Từ ngày: " + LupNgayTu.Text.Substring(0, 10) + " đến ngày: " + LupNgayDen.Text.Substring(0, 10);
                                    rep.KhoaPhong.Value = LupKhoaPhong.Text;
                                    rep.DataSource = _Tamtra;
                                    rep.BindingData();
                                    rep.CreateDocument();
                                    //rep.DataMember = "Table";
                                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                    frm.ShowDialog();
                                }
                                #endregion tạo báo cáo A3
                                else
                                {
                                    if (_DSDV.Count <= 46)
                                    #region tạo báo cáo A4 1 trang
                                    {
                                        BaoCao.Rep_TamTraThuoc rep = new BaoCao.Rep_TamTraThuoc();
                                        frmIn frm = new frmIn();
                                        string a = "Kiểu đơn: ";
                                        if (chkBoxung.Checked == true)
                                        { a += "bổ xung"; }
                                        if (chkThuongxuyen.Checked == true)
                                        {
                                            if (string.IsNullOrEmpty(a))
                                            {
                                                a += "thường xuyên";
                                            }
                                            else
                                            { a = a + " - " + " thường xuyên"; }
                                        }
                                        if (chkTrathuoc.Checked == true)
                                        {
                                            if (string.IsNullOrEmpty(a))
                                            {
                                                a += "trả thuốc";
                                            }
                                            else
                                            { a = a + " - " + " trả thuốc"; }
                                        }
                                        rep.Kieudon.Value = a;
                                        for (int i = 0; i < _DSDV.Count; i++)
                                        {
                                            switch (i)
                                            {
                                                case 0:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T1.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 1:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T2.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 2:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T3.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 3:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T4.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 4:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T5.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 5:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T6.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 6:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T7.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 7:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T8.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 8:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T9.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 9:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T10.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 10:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T11.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 11:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T12.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 12:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T13.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 13:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T14.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 14:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T15.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 15:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T16.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 16:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T17.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 17:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T18.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 18:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T19.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 19:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T20.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 20:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T21.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 21:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T22.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 22:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T23.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 23:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T24.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 24:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T25.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 25:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T26.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 26:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T27.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 27:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T28.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 28:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T29.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 29:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T30.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 30:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T31.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 31:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T32.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 32:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T33.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 33:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T34.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 34:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T35.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 35:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T36.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 36:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T37.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 37:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T38.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 38:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T39.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 39:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T40.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 40:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T41.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 41:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T42.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 42:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T43.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 43:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T44.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 44:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T45.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 45:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T46.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;


                                            }
                                        }

                                        rep.Ngaythang.Value = " Từ ngày: " + LupNgayTu.Text.Substring(0, 10) + " đến ngày: " + LupNgayDen.Text.Substring(0, 10);
                                        rep.Khoaphong.Value = LupKhoaPhong.Text;
                                        rep.DataSource = _Tamtra;
                                        rep.BindingData();
                                        rep.CreateDocument();
                                        //rep.DataMember = "Table";
                                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                        frm.ShowDialog();

                                    }
                                    #endregion tạo báo cáo A4 1 trang
                                    if (_DSDV.Count > 46)
                                    #region tạo báo cáo A4 2 trang
                                    {
                                        BaoCao.Rep_TamTraThuoc rep = new BaoCao.Rep_TamTraThuoc();
                                        frmIn frm = new frmIn();
                                        string a = "Kiểu đơn: ";
                                        if (chkBoxung.Checked == true)
                                        { a += "bổ xung"; }
                                        if (chkThuongxuyen.Checked == true)
                                        {
                                            if (string.IsNullOrEmpty(a))
                                            {
                                                a += "thường xuyên";
                                            }
                                            else
                                            { a = a + " - " + " thường xuyên"; }
                                        }
                                        if (chkTrathuoc.Checked == true)
                                        {
                                            if (string.IsNullOrEmpty(a))
                                            {
                                                a += "trả thuốc";
                                            }
                                            else
                                            { a = a + " - " + " trả thuốc"; }
                                        }
                                        rep.Kieudon.Value = a;
                                        for (int i = 0; i < _DSDV.Count; i++)
                                        {
                                            switch (i)
                                            {
                                                case 0:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T1.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 1:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T2.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 2:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T3.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 3:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T4.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 4:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T5.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 5:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T6.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 6:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T7.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 7:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T8.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 8:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T9.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 9:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T10.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 10:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T11.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 11:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T12.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 12:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T13.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 13:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T14.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 14:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T15.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 15:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T16.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 16:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T17.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 17:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T18.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 18:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T19.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 19:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T20.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 20:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T21.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 21:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T22.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 22:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T23.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 23:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T24.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 24:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T25.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 25:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T26.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 26:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T27.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 27:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T28.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 28:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T29.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 29:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T30.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 30:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T31.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 31:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T32.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 32:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T33.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 33:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T34.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 34:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T35.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 35:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T36.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 36:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T37.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 37:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T38.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 38:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T39.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 39:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T40.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 40:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T41.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 41:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T42.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 42:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T43.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 43:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T44.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 44:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T45.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 45:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep.T46.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;


                                            }
                                        }

                                        rep.Ngaythang.Value = " Từ ngày: " + LupNgayTu.Text.Substring(0, 10) + " đến ngày: " + LupNgayDen.Text.Substring(0, 10);
                                        rep.Khoaphong.Value = LupKhoaPhong.Text;
                                        rep.DataSource = _Tamtra;
                                        rep.BindingData();
                                        rep.CreateDocument();
                                        //rep.DataMember = "Table";
                                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                        frm.ShowDialog();

                                        // tạo báo cáo thứ 2

                                        BaoCao.Rep_Tamtrathuoc02 rep1 = new BaoCao.Rep_Tamtrathuoc02();
                                        frmIn frm1 = new frmIn();
                                        a = "Kiểu đơn: ";
                                        if (chkBoxung.Checked == true)
                                        { a += " bổ xung "; }
                                        if (chkThuongxuyen.Checked == true)
                                        {
                                            if (string.IsNullOrEmpty(a))
                                            {
                                                a += "thường xuyên ";
                                            }
                                            else
                                            { a = a + " - " + " thường xuyên "; }
                                        }
                                        if (chkTrathuoc.Checked == true)
                                        {
                                            if (string.IsNullOrEmpty(a))
                                            {
                                                a += "trả thuốc ";
                                            }
                                            else
                                            { a = a + " - " + " trả thuốc "; }
                                        }
                                        rep1.Kieudon.Value = a;
                                        for (int i = 46; i < _DSDV.Count; i++)
                                        {
                                            switch (i)
                                            {
                                                case 46:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T1.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 47:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T2.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 48:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T3.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 49:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T4.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 50:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T5.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 51:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T6.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 52:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T7.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 53:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T8.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 54:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T9.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 55:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T10.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 56:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T11.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 57:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T12.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 58:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T13.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 59:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T14.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 60:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T15.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 61:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T16.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 62:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T17.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 63:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T18.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 64:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T19.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 65:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T20.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 66:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T21.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 67:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T22.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 68:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T23.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 69:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T24.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 70:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T25.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 71:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T26.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 72:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T27.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 73:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T28.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 74:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T29.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 75:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T30.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 76:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T31.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 77:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T32.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 78:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T33.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 79:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T34.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 80:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T35.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 81:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T36.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 82:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T37.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 83:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T38.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 84:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T39.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 85:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T40.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 86:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T41.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 87:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T42.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 88:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T43.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 89:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T44.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 90:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T45.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 91:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T46.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 92:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T47.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 93:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T48.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 94:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T49.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 95:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T50.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 96:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T51.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 97:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T52.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;
                                                case 98:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    { rep1.T53.Value = _DSDV.Skip(i).First().tendv; }
                                                    break;

                                            }
                                        }

                                        rep1.Ngaythang.Value = " Từ ngày: " + LupNgayTu.Text.Substring(0, 10) + " đến ngày: " + LupNgayDen.Text.Substring(0, 10);
                                        rep1.Khoaphong.Value = LupKhoaPhong.Text;
                                        rep1.DataSource = _Tamtra;
                                        rep1.BindingData();
                                        rep1.CreateDocument();
                                        //rep.DataMember = "Table";
                                        frm1.prcIN.PrintingSystem = rep1.PrintingSystem;
                                        frm1.ShowDialog();
                                    }
                                    #endregion tạo báo cáo A4 2 trang
                                }
                            }
                            else
                            {
                                if (_DSDV.Count <= 46)
                                #region tạo báo cáo A4 1 trang
                                {
                                    BaoCao.Rep_TamTraThuoc rep = new BaoCao.Rep_TamTraThuoc();
                                    frmIn frm = new frmIn();
                                    string a = "Kiểu đơn: ";
                                    if (chkBoxung.Checked == true)
                                    { a += "bổ xung"; }
                                    if (chkThuongxuyen.Checked == true)
                                    {
                                        if (string.IsNullOrEmpty(a))
                                        {
                                            a += "thường xuyên";
                                        }
                                        else
                                        { a = a + " - " + " thường xuyên"; }
                                    }
                                    if (chkTrathuoc.Checked == true)
                                    {
                                        if (string.IsNullOrEmpty(a))
                                        {
                                            a += "trả thuốc";
                                        }
                                        else
                                        { a = a + " - " + " trả thuốc"; }
                                    }
                                    rep.Kieudon.Value = a;
                                    for (int i = 0; i < _DSDV.Count; i++)
                                    {
                                        switch (i)
                                        {
                                            case 0:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T1.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 1:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T2.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 2:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T3.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 3:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T4.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 4:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T5.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 5:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T6.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 6:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T7.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 7:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T8.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 8:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T9.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 9:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T10.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 10:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T11.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 11:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T12.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 12:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T13.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 13:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T14.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 14:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T15.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 15:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T16.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 16:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T17.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 17:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T18.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 18:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T19.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 19:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T20.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 20:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T21.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 21:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T22.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 22:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T23.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 23:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T24.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 24:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T25.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 25:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T26.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 26:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T27.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 27:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T28.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 28:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T29.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 29:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T30.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 30:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T31.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 31:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T32.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 32:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T33.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 33:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T34.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 34:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T35.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 35:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T36.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 36:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T37.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 37:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T38.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 38:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T39.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 39:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T40.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 40:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T41.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 41:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T42.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 42:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T43.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 43:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T44.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 44:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T45.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 45:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T46.Value = _DSDV.Skip(i).First().tendv; }
                                                break;


                                        }
                                    }

                                    rep.Ngaythang.Value = " Từ ngày: " + LupNgayTu.Text.Substring(0, 10) + " đến ngày: " + LupNgayDen.Text.Substring(0, 10);
                                    rep.Khoaphong.Value = LupKhoaPhong.Text;
                                    rep.DataSource = _Tamtra;
                                    rep.BindingData();
                                    rep.CreateDocument();
                                    //rep.DataMember = "Table";
                                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                    frm.ShowDialog();

                                }
                                #endregion tạo báo cáo A4 1 trang
                                if (_DSDV.Count > 46)
                                #region tạo báo cáo A4 2 trang
                                {
                                    BaoCao.Rep_TamTraThuoc rep = new BaoCao.Rep_TamTraThuoc();
                                    frmIn frm = new frmIn();
                                    string a = "Kiểu đơn: ";
                                    if (chkBoxung.Checked == true)
                                    { a += "bổ xung"; }
                                    if (chkThuongxuyen.Checked == true)
                                    {
                                        if (string.IsNullOrEmpty(a))
                                        {
                                            a += "thường xuyên";
                                        }
                                        else
                                        { a = a + " - " + " thường xuyên"; }
                                    }
                                    if (chkTrathuoc.Checked == true)
                                    {
                                        if (string.IsNullOrEmpty(a))
                                        {
                                            a += "trả thuốc";
                                        }
                                        else
                                        { a = a + " - " + " trả thuốc"; }
                                    }
                                    rep.Kieudon.Value = a;
                                    for (int i = 0; i < _DSDV.Count; i++)
                                    {
                                        switch (i)
                                        {
                                            case 0:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T1.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 1:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T2.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 2:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T3.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 3:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T4.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 4:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T5.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 5:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T6.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 6:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T7.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 7:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T8.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 8:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T9.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 9:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T10.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 10:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T11.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 11:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T12.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 12:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T13.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 13:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T14.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 14:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T15.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 15:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T16.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 16:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T17.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 17:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T18.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 18:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T19.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 19:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T20.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 20:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T21.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 21:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T22.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 22:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T23.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 23:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T24.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 24:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T25.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 25:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T26.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 26:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T27.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 27:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T28.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 28:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T29.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 29:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T30.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 30:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T31.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 31:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T32.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 32:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T33.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 33:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T34.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 34:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T35.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 35:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T36.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 36:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T37.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 37:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T38.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 38:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T39.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 39:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T40.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 40:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T41.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 41:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T42.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 42:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T43.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 43:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T44.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 44:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T45.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 45:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep.T46.Value = _DSDV.Skip(i).First().tendv; }
                                                break;


                                        }
                                    }

                                    rep.Ngaythang.Value = " Từ ngày: " + LupNgayTu.Text.Substring(0, 10) + " đến ngày: " + LupNgayDen.Text.Substring(0, 10);
                                    rep.Khoaphong.Value = LupKhoaPhong.Text;
                                    rep.DataSource = _Tamtra;
                                    rep.BindingData();
                                    rep.CreateDocument();
                                    //rep.DataMember = "Table";
                                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                    frm.ShowDialog();

                                    // tạo báo cáo thứ 2

                                    BaoCao.Rep_Tamtrathuoc02 rep1 = new BaoCao.Rep_Tamtrathuoc02();
                                    frmIn frm1 = new frmIn();
                                    a = "Kiểu đơn: ";
                                    if (chkBoxung.Checked == true)
                                    { a += " bổ xung "; }
                                    if (chkThuongxuyen.Checked == true)
                                    {
                                        if (string.IsNullOrEmpty(a))
                                        {
                                            a += "thường xuyên ";
                                        }
                                        else
                                        { a = a + " - " + " thường xuyên "; }
                                    }
                                    if (chkTrathuoc.Checked == true)
                                    {
                                        if (string.IsNullOrEmpty(a))
                                        {
                                            a += "trả thuốc ";
                                        }
                                        else
                                        { a = a + " - " + " trả thuốc "; }
                                    }
                                    rep1.Kieudon.Value = a;
                                    for (int i = 46; i < _DSDV.Count; i++)
                                    {
                                        switch (i)
                                        {
                                            case 46:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T1.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 47:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T2.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 48:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T3.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 49:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T4.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 50:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T5.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 51:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T6.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 52:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T7.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 53:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T8.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 54:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T9.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 55:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T10.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 56:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T11.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 57:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T12.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 58:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T13.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 59:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T14.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 60:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T15.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 61:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T16.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 62:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T17.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 63:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T18.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 64:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T19.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 65:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T20.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 66:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T21.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 67:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T22.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 68:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T23.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 69:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T24.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 70:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T25.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 71:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T26.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 72:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T27.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 73:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T28.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 74:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T29.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 75:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T30.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 76:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T31.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 77:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T32.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 78:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T33.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 79:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T34.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 80:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T35.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 81:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T36.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 82:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T37.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 83:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T38.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 84:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T39.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 85:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T40.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 86:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T41.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 87:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T42.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 88:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T43.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 89:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T44.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 90:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T45.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 91:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T46.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 92:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T47.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 93:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T48.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 94:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T49.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 95:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T50.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 96:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T51.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 97:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T52.Value = _DSDV.Skip(i).First().tendv; }
                                                break;
                                            case 98:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                { rep1.T53.Value = _DSDV.Skip(i).First().tendv; }
                                                break;

                                        }
                                    }

                                    rep1.Ngaythang.Value = " Từ ngày: " + LupNgayTu.Text.Substring(0, 10) + " đến ngày: " + LupNgayDen.Text.Substring(0, 10);
                                    rep1.Khoaphong.Value = LupKhoaPhong.Text;
                                    rep1.DataSource = _Tamtra;
                                    rep1.BindingData();
                                    rep1.CreateDocument();
                                    //rep.DataMember = "Table";
                                    frm1.prcIN.PrintingSystem = rep1.PrintingSystem;
                                    frm1.ShowDialog();
                                }
                                #endregion tạo báo cáo A4 2 trang
                            }
                        }
                    }
                }
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            int a = radNT.SelectedIndex;
            MessageBox.Show(a.ToString());
        }
    }
}

