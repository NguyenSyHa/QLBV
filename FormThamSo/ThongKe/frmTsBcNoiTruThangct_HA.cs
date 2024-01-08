using System;
using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using System.Text.RegularExpressions;

namespace QLBV.FormThamSo
{
    public partial class frmTsBcNoiTruThangct_HA : DevExpress.XtraEditors.XtraForm
    {
        public frmTsBcNoiTruThangct_HA()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

        private bool KTtaoBc()
        {
            if (dateTuNgay.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn ngày bắt đầu in báo cáo");
                dateTuNgay.Focus();
                return false;
            }
            if (dateDenNgay.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn đến ngày kết thúc in báo cáo");
                dateDenNgay.Focus();
                return false;
            }
           else return true;
        }
        private class BN
        {
            private string MaICD;

            public string MaICD1
            {
                get { return MaICD; }
                set { MaICD = value; }
            }
            private string TenICD;

            public string TenICD1
            {
                get { return TenICD; }
                set { TenICD = value; }
            }
            private string TongSoLK;

            public string TongSoLK1
            {
                get { return TongSoLK; }
                set { TongSoLK = value; }
            }
            private string TongSoLKTE;

            public string TongSoLKTE1
            {
                get { return TongSoLKTE; }
                set { TongSoLKTE = value; }
            }
            private string TongSoLKTE5;

            public string TongSoLKTE51
            {
                get { return TongSoLKTE5; }
                set { TongSoLKTE5 = value; }
            }
            private string TongSoLKNam;

            public string TongSoLKNam1
            {
                get { return TongSoLKNam; }
                set { TongSoLKNam = value; }
            }
            private string TongSoLKNu;

            public string TongSoLKNu1
            {
                get { return TongSoLKNu; }
                set { TongSoLKNu = value; }
            }

            private string TongTien;
            public string TongTien1
            {
                get { return TongTien; }
                set { TongTien = value; }
            }
        }
        List<BN> _lbn = new List<BN>();
        private void btnInBC_Click(object sender, EventArgs e)
        {

            DateTime tungay = System.DateTime.Now.Date;
            DateTime denngay = System.DateTime.Now.Date;
            if (KTtaoBc())
            {
                _lbn.Clear();
                tungay = DungChung.Ham.NgayTu(dateTuNgay.DateTime);
                denngay = DungChung.Ham.NgayDen(dateDenNgay.DateTime);

                frmIn frm = new frmIn();
                string _dt1 = ""; string _dt2 = "";
                if (radDT.SelectedIndex == 0) { _dt1 = "BHYT"; _dt2 = ""; }
                if (radDT.SelectedIndex == 1) { _dt1 = ""; _dt2 = "Dịch vụ"; }
                if (radDT.SelectedIndex == 2) { _dt1 = "BHYT"; _dt2 = "Dịch vụ"; }
                int noitru = -1, ngoaitru = -1; string _tenbc = "",_khoa="";
                if (radBN.SelectedIndex == 2)
                {
                    _tenbc = "THỐNG KÊ BỆNH NHÂN KHÁM BỆNH THEO MÃ BỆNH ICD";
                    _khoa = "";
                    ngoaitru = 0; noitru = 1; }
                if (radBN.SelectedIndex == 1)
                {
                    _tenbc = "THỐNG KÊ BỆNH NHÂN NỘI TRÚ KHÁM BỆNH THEO MÃ BỆNH ICD";
                    _khoa = "Khoa: Điều trị";
                    noitru = 1; }
                if (radBN.SelectedIndex == 0)
                {
                    _tenbc = "THỐNG KÊ BỆNH NHÂN NGOẠI TRÚ KHÁM BỆNH THEO MÃ BỆNH ICD";
                    _khoa = "Khoa: Khám bệnh";
                    ngoaitru = 0;
                }
              
                List<KPhong> _lKhoaP = new List<KPhong>();
                _lKhoaP = _Kphong.Where(p => p.makp >0).Where(p => p.chon == true).ToList();
                if (iCD.Where(p => p.chon == true).ToList().Count <= 0)
                {
                    MessageBox.Show("Bạn chưa chọn icd!");
                    return;
                }
                else if (iCD.Where(p => p.chon == true && p.maICD == "0").ToList().Count > 0)
                {
                    List<string> _icdchon = iCD.Where(p => p.chon == true).Select(p => p.maICD).Distinct().ToList();
                    if (InTK.Checked == true)
                    {
                        var q1 = (from kp in _lKhoaP
                                  join rv in data.RaViens.Where(p => p.MaICD.Length >= 3).Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay) on kp.makp equals rv.MaKP
                                  join bn in data.BenhNhans.Where(p => p.DTuong == _dt1 || p.DTuong == _dt2).Where(p => p.NoiTru == noitru || p.NoiTru == ngoaitru) on rv.MaBNhan equals bn.MaBNhan
                                  join icd in data.ICD10 on rv.MaICD equals icd.MaICD
                                  join a1 in _icdchon on icd.MaICD equals a1
                                  select new { rv.KetQua, rv.MaBNhan, bn.Tuoi, bn.GTinh, rv.MaICD, TenICD = DungChung.Bien.MaBV == "26007" ? rv.ChanDoan:  icd.TenICD }).ToList();

                        var qicd = (from q in q1
                                    group new { q } by new { q.MaICD, q.TenICD } into kq
                                    select new
                                    {
                                        MaICD = kq.Key.MaICD,
                                        TenICD = DungChung.Ham.FreshString(kq.Key.TenICD),
                                        TongSoLK = kq.Select(p => p.q.MaBNhan).Count(),
                                        TongSoLKTE = kq.Where(p => p.q.Tuoi <= 15).Select(p => p.q.MaBNhan).Distinct().Count(),
                                        TongSoLKTE5 = kq.Where(p => p.q.Tuoi <= 5).Select(p => p.q.MaBNhan).Distinct().Count(),
                                        TongSoLKNam = kq.Where(p => p.q.GTinh == 1).Select(p => p.q.MaBNhan).Distinct().Count(),
                                        TongSoLKNu = kq.Where(p => p.q.GTinh == 0).Select(p => p.q.MaBNhan).Distinct().Count(),
                                        TongTien = kq.Select(p => p.q.MaBNhan).Count() * 34500,
                                    }).ToList();


                        if (qicd.Count > 0)
                        {
                            foreach (var a in qicd)
                            {
                                BN them = new BN();
                                them.MaICD1 = a.MaICD;
                                them.TenICD1 = a.TenICD;
                                them.TongSoLK1 = a.TongSoLK == 0 ? null : a.TongSoLK.ToString();
                                them.TongSoLKTE1 = a.TongSoLKTE == 0 ? null : a.TongSoLKTE.ToString();
                                them.TongSoLKTE51 = a.TongSoLKTE == 0 ? null : a.TongSoLKTE.ToString();
                                them.TongSoLKNam1 = a.TongSoLKNam == 0 ? null : a.TongSoLKNam.ToString();
                                them.TongSoLKNu1 = a.TongSoLKNu == 0 ? null : a.TongSoLKNu.ToString();
                                them.TongTien1 = a.TongTien == 0 ? null : a.TongTien.ToString();
                                _lbn.Add(them);
                            }
                        }
                    }
                    else
                    {
                        if (radioGroup1.SelectedIndex == 0)//Lươt KB || Ko thống kê BN chuyển PK
                        {

                            var id = (from kb in data.BNKBs.Where(p => p.NgayKham >= tungay).Where(p => p.NgayKham <= denngay)
                                      group kb by kb.MaBNhan into kq
                                      select new { kq.Key, IDKB = kq.Max(p => p.IDKB) }).ToList();
                            var idkp = (from i in id
                                        join kb in data.BNKBs on i.IDKB equals kb.IDKB
                                        select new { i.IDKB, kb.MaKP }).ToList();
                            var qicd = (from kp in _lKhoaP
                                        join q in idkp on kp.makp equals q.MaKP
                                        join bnkb in data.BNKBs.Where(p => p.NgayKham >= tungay && p.NgayKham <= denngay) on q.IDKB equals bnkb.IDKB
                                        join bn in data.BenhNhans.Where(p => p.DTuong == _dt1 || p.DTuong == _dt2).Where(p => p.NoiTru == noitru || p.NoiTru == ngoaitru) on bnkb.MaBNhan equals bn.MaBNhan
                                        join icd in data.ICD10 on bnkb.MaICD equals icd.MaICD
                                        join a1 in _icdchon on icd.MaICD equals a1
                                        group new { bn, bnkb } by new { bnkb.MaICD,TenICD =  DungChung.Bien.MaBV == "26007" ? (bnkb.ChanDoan +" " + bnkb.BenhKhac) :  icd.TenICD } into kq
                                        select new
                                        {

                                            MaICD = kq.Key.MaICD,
                                            TenICD = DungChung.Ham.FreshString(kq.Key.TenICD),
                                            TongSoLK = kq.Select(p => p.bn.MaBNhan).Count(),
                                            TongSoLKTE = kq.Where(p => p.bn.Tuoi <= 15).Select(p => p.bn.MaBNhan).Distinct().Count(),
                                            TongSoLKTE5 = kq.Where(p => p.bn.Tuoi <= 5).Select(p => p.bn.MaBNhan).Distinct().Count(),
                                            TongSoLKNam = kq.Where(p => p.bn.GTinh == 1).Select(p => p.bn.MaBNhan).Distinct().Count(),
                                            TongSoLKNu = kq.Where(p => p.bn.GTinh == 0).Select(p => p.bn.MaBNhan).Distinct().Count(),
                                            TongTien = kq.Select(p => p.bn.MaBNhan).Count() * 34500,
                                        }).ToList();


                            if (qicd.Count > 0)
                            {
                                foreach (var a in qicd)
                                {
                                    BN them = new BN();
                                    them.MaICD1 = a.MaICD;
                                    them.TenICD1 = a.TenICD;
                                    them.TongSoLK1 = a.TongSoLK == 0 ? null : a.TongSoLK.ToString();
                                    them.TongSoLKTE1 = a.TongSoLKTE == 0 ? null : a.TongSoLKTE.ToString();
                                    them.TongSoLKTE51 = a.TongSoLKTE == 0 ? null : a.TongSoLKTE.ToString();
                                    them.TongSoLKNam1 = a.TongSoLKNam == 0 ? null : a.TongSoLKNam.ToString();
                                    them.TongSoLKNu1 = a.TongSoLKNu == 0 ? null : a.TongSoLKNu.ToString();
                                    them.TongTien1 = a.TongTien == 0 ? null : a.TongTien.ToString();
                                    _lbn.Add(them);
                                }
                            }
                        }

                        else //TK BN chuyển PK
                        {
                            var qicd = (from kp in _lKhoaP
                                        join bnkb in data.BNKBs.Where(p => p.NgayKham >= tungay && p.NgayKham <= denngay) on kp.makp equals bnkb.MaKP
                                        join bn in data.BenhNhans.Where(p => p.DTuong == _dt1 || p.DTuong == _dt2).Where(p => p.NoiTru == noitru || p.NoiTru == ngoaitru) on bnkb.MaBNhan equals bn.MaBNhan
                                        join icd in data.ICD10 on bnkb.MaICD equals icd.MaICD
                                        join a1 in _icdchon on icd.MaICD equals a1
                                        group new { bn, bnkb } by new { bnkb.MaICD,TenICD =  DungChung.Bien.MaBV == "26007" ? (bnkb.ChanDoan + " " +bnkb.BenhKhac) :  icd.TenICD } into kq
                                        select new
                                        {

                                            MaICD = kq.Key.MaICD,
                                            TenICD = DungChung.Ham.FreshString(kq.Key.TenICD),
                                            TongSoLK = kq.Select(p => p.bn.MaBNhan).Count(),
                                            TongSoLKTE = kq.Where(p => p.bn.Tuoi <= 15).Select(p => p.bn.MaBNhan).Distinct().Count(),
                                            TongSoLKTE5 = kq.Where(p => p.bn.Tuoi <= 5).Select(p => p.bn.MaBNhan).Distinct().Count(),
                                            TongSoLKNam = kq.Where(p => p.bn.GTinh == 1).Select(p => p.bn.MaBNhan).Distinct().Count(),
                                            TongSoLKNu = kq.Where(p => p.bn.GTinh == 0).Select(p => p.bn.MaBNhan).Distinct().Count(),
                                            TongTien = kq.Select(p => p.bn.MaBNhan).Count() * 34500,
                                        }).ToList();


                            if (qicd.Count > 0)
                            {
                                foreach (var a in qicd)
                                {
                                    BN them = new BN();
                                    them.MaICD1 = a.MaICD;
                                    them.TenICD1 = a.TenICD;
                                    them.TongSoLK1 = a.TongSoLK == 0 ? null : a.TongSoLK.ToString();
                                    them.TongSoLKTE1 = a.TongSoLKTE == 0 ? null : a.TongSoLKTE.ToString();
                                    them.TongSoLKTE51 = a.TongSoLKTE == 0 ? null : a.TongSoLKTE.ToString();
                                    them.TongSoLKNam1 = a.TongSoLKNam == 0 ? null : a.TongSoLKNam.ToString();
                                    them.TongSoLKNu1 = a.TongSoLKNu == 0 ? null : a.TongSoLKNu.ToString();
                                    them.TongTien1 = a.TongTien == 0 ? null : a.TongTien.ToString();
                                    _lbn.Add(them);
                                }
                            }
                        }

                    }
                }
                else //chọn ICD
                {
                    List<string> _icdchon = iCD.Where(p => p.chon == true).Select(p => p.maICD).Distinct().ToList();
                    if (InTK.Checked == true)
                    {
                        var q1 = (from kp in _lKhoaP
                                  join rv in data.RaViens.Where(p => p.MaICD.Length >= 3).Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay) on kp.makp equals rv.MaKP
                                  join bn in data.BenhNhans.Where(p => p.DTuong == _dt1 || p.DTuong == _dt2).Where(p => p.NoiTru == noitru || p.NoiTru == ngoaitru) on rv.MaBNhan equals bn.MaBNhan
                                  join icd in data.ICD10 on rv.MaICD equals icd.MaICD
                                  join a1 in _icdchon on icd.MaICD equals a1
                                  select new { rv.KetQua, rv.MaBNhan, bn.Tuoi, bn.GTinh, rv.MaICD,TenICD =  (DungChung.Bien.MaBV == "26007" )? (rv.ChanDoan): icd.TenICD }).ToList();

                        var qicd = (from q in q1
                                    group new { q } by new { q.MaICD, q.TenICD } into kq
                                    select new
                                    {

                                        MaICD = kq.Key.MaICD,
                                        TenICD = DungChung.Ham.FreshString(kq.Key.TenICD),
                                        TongSoLK = kq.Select(p => p.q.MaBNhan).Count(),
                                        TongSoLKTE = kq.Where(p => p.q.Tuoi <= 15).Select(p => p.q.MaBNhan).Distinct().Count(),
                                        TongSoLKTE5 = kq.Where(p => p.q.Tuoi <= 5).Select(p => p.q.MaBNhan).Distinct().Count(),
                                        TongSoLKNam = kq.Where(p => p.q.GTinh == 1).Select(p => p.q.MaBNhan).Distinct().Count(),
                                        TongSoLKNu = kq.Where(p => p.q.GTinh == 0).Select(p => p.q.MaBNhan).Distinct().Count(),
                                        TongTien = kq.Select(p => p.q.MaBNhan).Count() * 34500,

                                    }).ToList();


                        if (qicd.Count > 0)
                        {
                            foreach (var a in qicd)
                            {
                                BN them = new BN();
                                them.MaICD1 = a.MaICD;
                                them.TenICD1 = a.TenICD;
                                them.TongSoLK1 = a.TongSoLK == 0 ? null : a.TongSoLK.ToString();
                                them.TongSoLKTE1 = a.TongSoLKTE == 0 ? null : a.TongSoLKTE.ToString();
                                them.TongSoLKTE51 = a.TongSoLKTE == 0 ? null : a.TongSoLKTE.ToString();
                                them.TongSoLKNam1 = a.TongSoLKNam == 0 ? null : a.TongSoLKNam.ToString();
                                them.TongSoLKNu1 = a.TongSoLKNu == 0 ? null : a.TongSoLKNu.ToString();
                                them.TongTien1 = a.TongTien == 0 ? null : a.TongTien.ToString();
                                _lbn.Add(them);
                            }
                        }
                    }
                    else
                    {
                        if (radioGroup1.SelectedIndex == 0)//Lươt KB || Ko thống kê BN chuyển PK
                        {
                            var id = (from kb in data.BNKBs.Where(p => p.NgayKham >= tungay && p.NgayKham <= denngay)
                                      group kb by kb.MaBNhan into kq
                                      select new { kq.Key, IDKB = kq.Max(p => p.IDKB) }).ToList();
                            var idkp = (from i in id
                                        join kb in data.BNKBs on i.IDKB equals kb.IDKB
                                        select new { i.IDKB, kb.MaKP }).ToList();
                            var qicd = (from kp in _lKhoaP
                                        join q in idkp on kp.makp equals q.MaKP
                                        join bnkb in data.BNKBs.Where(p => p.NgayKham >= tungay && p.NgayKham <= denngay) on q.IDKB equals bnkb.IDKB
                                        join bn in data.BenhNhans.Where(p => p.DTuong == _dt1 || p.DTuong == _dt2).Where(p => p.NoiTru == noitru || p.NoiTru == ngoaitru) on bnkb.MaBNhan equals bn.MaBNhan
                                        join icd in data.ICD10 on bnkb.MaICD equals icd.MaICD
                                        join a1 in _icdchon on icd.MaICD equals a1
                                        group new { bn, bnkb } by new { bnkb.MaICD,TenICD = DungChung.Bien.MaBV == "26007" ? (bnkb.ChanDoan+ " " + bnkb.BenhKhac): icd.TenICD } into kq
                                        select new
                                        {

                                            MaICD = kq.Key.MaICD,
                                            TenICD = DungChung.Ham.FreshString(kq.Key.TenICD),
                                            TongSoLK = kq.Select(p => p.bn.MaBNhan).Count(),
                                            TongSoLKTE = kq.Where(p => p.bn.Tuoi <= 15).Select(p => p.bn.MaBNhan).Distinct().Count() ,
                                            TongSoLKTE5 = kq.Where(p => p.bn.Tuoi <= 5).Select(p => p.bn.MaBNhan).Distinct().Count(),
                                            TongSoLKNam = kq.Where(p => p.bn.GTinh == 1).Select(p => p.bn.MaBNhan).Distinct().Count(),
                                            TongSoLKNu = kq.Where(p => p.bn.GTinh == 0).Select(p => p.bn.MaBNhan).Distinct().Count(),
                                            TongTien = kq.Select(p => p.bn.MaBNhan).Count() * 34500,

                                        }).ToList();


                            if (qicd.Count > 0)
                            {
                                foreach (var a in qicd)
                                {
                                    BN them = new BN();
                                    them.MaICD1 = a.MaICD;
                                    them.TenICD1 = a.TenICD;
                                    them.TongSoLK1 = a.TongSoLK == 0 ? null : a.TongSoLK.ToString();
                                    them.TongSoLKTE1 = a.TongSoLKTE == 0 ? null : a.TongSoLKTE.ToString();
                                    them.TongSoLKTE51 = a.TongSoLKTE == 0 ? null : a.TongSoLKTE.ToString();
                                    them.TongSoLKNam1 = a.TongSoLKNam == 0 ? null : a.TongSoLKNam.ToString();
                                    them.TongSoLKNu1 = a.TongSoLKNu == 0 ? null : a.TongSoLKNu.ToString();
                                    them.TongTien1 = a.TongTien == 0 ? null : a.TongTien.ToString();
                                    _lbn.Add(them);
                                }

                            }
                        }

                        else //TK BN chuyển PK
                        {
                            var qicd = (from kp in _lKhoaP
                                        join bnkb in data.BNKBs.Where(p => p.NgayKham >= tungay && p.NgayKham <= denngay) on kp.makp equals bnkb.MaKP
                                        join bn in data.BenhNhans.Where(p => p.DTuong == _dt1 || p.DTuong == _dt2).Where(p => p.NoiTru == noitru || p.NoiTru == ngoaitru) on bnkb.MaBNhan equals bn.MaBNhan
                                        join icd in data.ICD10 on bnkb.MaICD equals icd.MaICD
                                        join a1 in _icdchon on icd.MaICD equals a1
                                        group new { bn, bnkb } by new { bnkb.MaICD, TenICD = DungChung.Bien.MaBV == "26007" ? (bnkb.ChanDoan + " " + bnkb.BenhKhac) :icd.TenICD } into kq
                                        select new
                                        {

                                            MaICD = kq.Key.MaICD,
                                            TenICD = DungChung.Ham.FreshString(kq.Key.TenICD),
                                            TongSoLK = kq.Select(p => p.bn.MaBNhan).Count(),
                                            TongSoLKTE = kq.Where(p => p.bn.Tuoi <= 15).Select(p => p.bn.MaBNhan).Distinct().Count(),
                                            TongSoLKTE5 = kq.Where(p => p.bn.Tuoi <= 5).Select(p => p.bn.MaBNhan).Distinct().Count(),
                                            TongSoLKNam = kq.Where(p => p.bn.GTinh == 1).Select(p => p.bn.MaBNhan).Distinct().Count(),
                                            TongSoLKNu = kq.Where(p => p.bn.GTinh == 0).Select(p => p.bn.MaBNhan).Distinct().Count(),
                                            TongTien = kq.Select(p => p.bn.MaBNhan).Count() * 34500,

                                        }).ToList();


                            if (qicd.Count > 0)
                            {
                                foreach (var a in qicd)
                                {
                                    BN them = new BN();
                                    them.MaICD1 = a.MaICD;
                                    them.TenICD1 = a.TenICD;
                                    them.TongSoLK1 = a.TongSoLK == 0 ? null : a.TongSoLK.ToString();
                                    them.TongSoLKTE1 = a.TongSoLKTE == 0 ? null : a.TongSoLKTE.ToString();
                                    them.TongSoLKTE51 = a.TongSoLKTE == 0 ? null : a.TongSoLKTE.ToString();
                                    them.TongSoLKNam1 = a.TongSoLKNam == 0 ? null : a.TongSoLKNam.ToString();
                                    them.TongSoLKNu1 = a.TongSoLKNu == 0 ? null : a.TongSoLKNu.ToString();
                                    them.TongTien1 = a.TongTien == 0 ? null : a.TongTien.ToString();
                                    _lbn.Add(them);
                                }
                            }
                        }

                    }
                }
                if (DungChung.Bien.MaBV == "08104")
                {
                    BaoCao.repBcNoiTruThangct_CATQ rep = new BaoCao.repBcNoiTruThangct_CATQ();
                    rep.TuNgay.Value = "Từ ngày " + tungay.ToString().Substring(0, 10) + " đến ngày " + denngay.ToString().Substring(0, 10);
                    rep.TenBC.Value = _tenbc;
                    rep.Khoa.Value = _khoa;
                    rep.DataSource = _lbn.OrderBy(p => (radSX.SelectedIndex == 0 ? p.TenICD1 : p.MaICD1)).ToList();
                    rep.BindingData();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                }
                else
                {
                    BaoCao.repBcNoiTruThangct_HA rep = new BaoCao.repBcNoiTruThangct_HA();
                    rep.TuNgay.Value = "Từ ngày " + tungay.ToString().Substring(0, 10) + " đến ngày " + denngay.ToString().Substring(0, 10);
                    rep.TenBC.Value = _tenbc;
                    rep.Khoa.Value = _khoa;
                    rep.DataSource = _lbn.OrderBy(p => (radSX.SelectedIndex==0? p.TenICD1:p.MaICD1)).ToList();
                    rep.BindingData();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                }
            }

    }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
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
        private class ICD
        {
            private string TenICD;
            private string MaICD;
            private bool Chon;
            public string tenICD
            { set { TenICD = value; } get { return TenICD; } }
            public string maICD
            { set { MaICD = value; } get { return MaICD; } }
            public bool chon
            { set { Chon = value; } get { return Chon; } }
        }
        List<KPhong> _Kphong = new List<KPhong>();
        List<ICD> iCD = new List<ICD>();
        private void frmTsBcNoiTruThangct_HA_Load(object sender, EventArgs e)
        {

            dateTuNgay.DateTime = System.DateTime.Now;
            dateDenNgay.DateTime = System.DateTime.Now;
            iCD.Clear();
            var icD = (from icd in data.ICD10
                          select icd).ToList();
            if (icD.Count > 0)
            {
                ICD themmoi1 = new ICD();
                themmoi1.tenICD = "Chọn tất cả";
                themmoi1.maICD = "0";
                themmoi1.chon = true;
                iCD.Add(themmoi1);
                foreach (var a in icD)
                {
                    ICD themmoi = new ICD();
                    themmoi.tenICD = a.TenICD;
                    themmoi.maICD = a.MaICD;
                    themmoi.chon = true;
                    iCD.Add(themmoi);
                }
                GrcTenBenh.DataSource = iCD.ToList();
            }
            _Kphong.Clear();
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
                    themmoi.makp = a.MaKP;
                    themmoi.chon = true;
                    _Kphong.Add(themmoi);
                }
                grcKhoaphong.DataSource = _Kphong.ToList();
            }
        }

        private void grvKhoaphong_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "Chọn")
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
                }
            }
        }

        private void GrvICD_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "Chon")
            {
                if (GrvICD.GetFocusedRowCellValue("tenICD") != null)
                {
                    string Ten = GrvICD.GetFocusedRowCellValue("tenICD").ToString();

                    if (Ten == "Chọn tất cả")
                    {
                        if (iCD.First().chon == true)
                        {
                            foreach (var a in iCD)
                            {
                                a.chon = false;
                            }
                        }
                        else
                        {
                            foreach (var a in iCD)
                            {
                                a.chon = true;
                            }
                        }
                        GrcTenBenh.DataSource = "";
                        GrcTenBenh.DataSource = iCD.ToList();
                    }
                }
            }
        }

        private void dateDenNgay_EditValueChanged(object sender, EventArgs e)
        {

        }
        public bool IsNumber(string pText)
        {
            Regex regex = new Regex(@"^[-+]?[0-9]*\.?[0-9]+$");
            return regex.IsMatch(pText);
        }
        private void txtTimkiemMaICD_TextChanged(object sender, EventArgs e)
        {
            List<ICD> _listICD = new List<ICD>();

            string _maicd = "";
            if (!string.IsNullOrEmpty(txtTimkiemMaICD.Text))
                _maicd = txtTimkiemMaICD.Text;

            string[] _getmaicd = _maicd.Split(',');
            if (txtTimkiemMaICD.Text != "")
            {
                for (int i = 0; i < _getmaicd.Count(); i++)
                {
                    if (_getmaicd[i] != "")
                    {
                        string s = _getmaicd[i].ToUpper();
                        GrcTenBenh.DataSource = iCD.Where(p => _maicd == "" || p.maICD.Contains(s)).OrderBy(p => p.maICD).ThenBy(p => p.tenICD).ToList();
                    }
                }
            }
            else GrcTenBenh.DataSource = iCD;
        }
    }
}