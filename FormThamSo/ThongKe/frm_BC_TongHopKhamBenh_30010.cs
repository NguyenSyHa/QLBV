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
    public partial class frm_BC_TongHopKhamBenh_30010 : DevExpress.XtraEditors.XtraForm
    {
        public frm_BC_TongHopKhamBenh_30010()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        List<KPhong> _Kphong = new List<KPhong>();

        private void frm_BC_TongHopKhamBenh_30010_Load(object sender, EventArgs e)
        {
            date_TuNgay.DateTime = DateTime.Now;
            date_DenNgay.DateTime = DateTime.Now;
            var kphong = (from kp in data.KPhongs.Where(p => p.PLoai == "Lâm sàng" || p.PLoai == "Phòng khám")
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

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            DateTime tungay = DungChung.Ham.NgayTu(date_TuNgay.DateTime);
            DateTime denngay = DungChung.Ham.NgayDen(date_DenNgay.DateTime);
            List<KPhong> _lKhoaP = new List<KPhong>();
            _lKhoaP = _Kphong.Where(p => p.makp > 0).Where(p => p.chon == true).ToList();
            List<KhamBenh> _listKB = new List<KhamBenh>();
            KhamBenh moi = new KhamBenh();

            var qKhamBenh = (from kp in _lKhoaP
                             join bnkb in data.BNKBs on kp.makp equals bnkb.MaKP
                             join bn in data.BenhNhans on bnkb.MaBNhan equals bn.MaBNhan
                             join dtbn in data.DTBNs on bn.IDDTBN equals dtbn.IDDTBN
                             join ttbx in data.TTboXungs on bnkb.MaBNhan equals ttbx.MaBNhan
                             select new { bnkb, bn, dtbn, ttbx, kp }).Where(p => p.bnkb.NgayKham >= tungay && p.bnkb.NgayKham <= denngay).ToList();

            var bnVaoVien = (from kb in data.BNKBs
                             join bn in data.BenhNhans on kb.MaBNhan equals bn.MaBNhan
                             join vv in data.VaoViens on kb.MaBNhan equals vv.MaBNhan
                             join ttbx in data.TTboXungs on bn.MaBNhan equals ttbx.MaBNhan
                             join k in data.KPhongs on vv.MaKP equals k.MaKP
                             group new { kb, vv, k } by new { bn.Tuoi, bn.MaBNhan, ttbx.NgoaiKieu, bn.DTuong, bn.NoiTru, bn.DTNT, kb.MaKP, k.TenKP, vv.NgayVao, kb.NgayKham } into kq
                             select new
                             {
                                 kq.Key.Tuoi,
                                 kq.Key.MaBNhan,
                                 kq.Key.NgoaiKieu,
                                 kq.Key.NoiTru,
                                 kq.Key.DTNT,
                                 kq.Key.MaKP,
                                 kq.Key.NgayVao,
                                 kq.Key.DTuong,
                                 tenkp = kq.Key.TenKP,
                                 kq.Key.NgayKham
                             }).Where(p => p.NgayKham >= tungay && p.NgayKham <= denngay).ToList();
            var qVaoVien = (from k in _lKhoaP
                            join vv in bnVaoVien on k.makp equals vv.MaKP
                            select vv).ToList();
            //status: 1-chuyển viện, 2-ra viện, 3-trốn viện, 4-xin ra viện
            var ravien = (from bnkb in data.BNKBs
                          join bn in data.BenhNhans on bnkb.MaBNhan equals bn.MaBNhan
                          join rv in data.RaViens on bn.MaBNhan equals rv.MaBNhan
                          select new { rv.MaBNhan, rv.NgayRa, rv.Status, rv.MaKP }).Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay).Where(p => p.Status == 1).ToList();
            var qRaVien = (from kp in _lKhoaP
                           join rv in ravien on kp.makp equals rv.MaKP
                           select new { rv }).ToList();
            for (DateTime i = tungay; i <= denngay; i = i.AddDays(1.0))
            {
                moi = new KhamBenh();
                moi.Ngay = String.Format("{0:dd/MM/yyyy}", i);
                moi.BHYT_TongKB = qKhamBenh.Where(p => (p.bn.DTuong == "BHYT") && String.Format("{0:dd/MM/yyyy}", p.bnkb.NgayKham).Equals(moi.Ngay) &&
                                                  p.bn.Tuoi > 15 && (p.bn.NoiTru == 0 && p.bn.DTNT == false)).Select(p => p.bnkb.MaBNhan).Distinct().Count();
                moi.BHYT_KB_BNTren60 = qKhamBenh.Where(p => (p.bn.DTuong == "BHYT") && String.Format("{0:dd/MM/yyyy}", p.bnkb.NgayKham).Equals(moi.Ngay) &&
                                                       p.bn.Tuoi >= 60 && (p.bn.NoiTru == 0 && p.bn.DTNT == false)).Select(p => p.bnkb.MaBNhan).Distinct().Count();
                moi.BHYT_TongVV = qVaoVien.Where(p => (p.DTuong == "BHYT") && String.Format("{0:dd/MM/yyyy}", p.NgayKham).Equals(moi.Ngay) &&
                                                 p.Tuoi > 15 && (!p.tenkp.Contains("HSCC") || !p.tenkp.ToLower().Contains("hồi sức cấp cứu")) && (p.NoiTru == 1 || p.DTNT == true)).Select(p => p.MaBNhan).Distinct().Count();
                moi.BHYT_VV_BNTren60 = qVaoVien.Where(p => (p.DTuong == "BHYT") && String.Format("{0:dd/MM/yyyy}", p.NgayKham).Equals(moi.Ngay) &&
                                                      p.Tuoi >= 60 && (p.NoiTru == 1 || p.DTNT == true) && (!p.tenkp.Contains("HSCC") || !p.tenkp.ToLower().Contains("hồi sức cấp cứu"))).Select(p => p.MaBNhan).Distinct().Count();

                moi.ND_TongKB = qKhamBenh.Where(p => (p.dtbn.DTBN1 != "BHYT") && String.Format("{0:dd/MM/yyyy}", p.bnkb.NgayKham).Equals(moi.Ngay) &&
                                                p.bn.Tuoi > 15 && (p.bn.NoiTru == 0 && p.bn.DTNT == false)).Select(p => p.bnkb.MaBNhan).Distinct().Count();
                moi.ND_KB_BNTren60 = qKhamBenh.Where(p => (p.dtbn.DTBN1 != "BHYT") && String.Format("{0:dd/MM/yyyy}", p.bnkb.NgayKham).Equals(moi.Ngay) &&
                                                     p.bn.Tuoi >= 60 && (p.bn.NoiTru == 0 && p.bn.DTNT == false)).Select(p => p.bnkb.MaBNhan).Distinct().Count();
                moi.ND_TongVV = qVaoVien.Where(p => (p.DTuong != "BHYT") && String.Format("{0:dd/MM/yyyy}", p.NgayKham).Equals(moi.Ngay) &&
                                                     p.Tuoi > 15 && (!p.tenkp.Contains("HSCC") || !p.tenkp.ToLower().Contains("hồi sức cấp cứu")) && (p.NoiTru == 1 || p.DTNT == true)).Select(p => p.MaBNhan).Distinct().Count();
                moi.ND_VV_BNTren60 = qVaoVien.Where(p => (p.DTuong != "BHYT") && String.Format("{0:dd/MM/yyyy}", p.NgayKham).Equals(moi.Ngay) &&
                                                    p.Tuoi >= 60 && (!p.tenkp.Contains("HSCC") || !p.tenkp.ToLower().Contains("hồi sức cấp cứu")) && (p.NoiTru == 1 || p.DTNT == true)).Select(p => p.MaBNhan).Distinct().Count();

                moi.TEDuoi15_BHYT_TongKB = qKhamBenh.Where(p => (p.dtbn.DTBN1 == "BHYT") && String.Format("{0:dd/MM/yyyy}", p.bnkb.NgayKham).Equals(moi.Ngay) &&
                                                           p.bn.Tuoi > 6 && p.bn.Tuoi <= 15 && (p.bn.NoiTru == 0 && p.bn.DTNT == false)).Select(p => p.bnkb.MaBNhan).Distinct().Count();
                moi.TEDuoi15_BHYT_VV = qVaoVien.Where(p => (p.DTuong == "BHYT") && String.Format("{0:dd/MM/yyyy}", p.NgayKham).Equals(moi.Ngay) &&
                                                            p.Tuoi > 6 && p.Tuoi <= 15 && (!p.tenkp.Contains("HSCC") || !p.tenkp.ToLower().Contains("hồi sức cấp cứu")) && (p.NoiTru == 1 || p.DTNT == true)).Select(p => p.MaBNhan).Distinct().Count();
                moi.TEDuoi15_ND_TongKB = qKhamBenh.Where(p => (p.dtbn.DTBN1 != "BHYT") && String.Format("{0:dd/MM/yyyy}", p.bnkb.NgayKham).Equals(moi.Ngay) &&
                                                               p.bn.Tuoi > 6 && p.bn.Tuoi <= 15 && (p.bn.NoiTru == 0 && p.bn.DTNT == false)).Select(p => p.bnkb.MaBNhan).Distinct().Count();
                moi.TEDuoi15_ND_VV = qVaoVien.Where(p => (p.DTuong != "BHYT") && String.Format("{0:dd/MM/yyyy}", p.NgayKham).Equals(moi.Ngay) &&
                                                    p.Tuoi > 6 && p.Tuoi <= 15 && (!p.tenkp.Contains("HSCC") || !p.tenkp.ToLower().Contains("hồi sức cấp cứu")) && (p.NoiTru == 1 || p.DTNT == true)).Select(p => p.MaBNhan).Distinct().Count();
                moi.TEDuoi6_TongKB = qKhamBenh.Where(p => String.Format("{0:dd/MM/yyyy}", p.bnkb.NgayKham).Equals(moi.Ngay) &&
                                                     p.bn.Tuoi <= 6 && (p.bn.NoiTru == 0 && p.bn.DTNT == false)).Select(p => p.bnkb.MaBNhan).Distinct().Count();
                moi.TEDuoi6_TongVV = qVaoVien.Where(p => String.Format("{0:dd/MM/yyyy}", p.NgayKham).Equals(moi.Ngay) && p.Tuoi <= 6 &&
                                                    (!p.tenkp.Contains("HSCC") || !p.tenkp.ToLower().Contains("hồi sức cấp cứu")) && (p.NoiTru == 1 || p.DTNT == true)).Select(p => p.MaBNhan).Distinct().Count();

                moi.TongKB = qKhamBenh.Where(p => p.bn.NoiTru == 0 && p.bn.DTNT == false).Where(p => String.Format("{0:dd/MM/yyyy}", p.bnkb.NgayKham).Equals(moi.Ngay)).Select(p => p.bnkb.MaBNhan).Distinct().Count();
                moi.KB_NguoiNuocNgoai = qKhamBenh.Where(p => p.bn.NoiTru == 0 && p.bn.DTNT == false).Where(p => String.Format("{0:dd/MM/yyyy}", p.bnkb.NgayKham).Equals(moi.Ngay) &&
                                                       (p.ttbx.NgoaiKieu != null && p.ttbx.NgoaiKieu != "Việt Nam")).Select(p => p.bnkb.MaBNhan).Distinct().Count();
                moi.KB_BNTren60 = qKhamBenh.Where(p => p.bn.NoiTru == 0 && p.bn.DTNT == false).Where(p => String.Format("{0:dd/MM/yyyy}", p.bnkb.NgayKham).Equals(moi.Ngay) && p.bn.Tuoi >= 60)
                                           .Select(p => p.bnkb.MaBNhan).Distinct().Count();

                moi.TongVV = qVaoVien.Where(p => String.Format("{0:dd/MM/yyyy}", p.NgayVao).Equals(moi.Ngay) && (p.NoiTru == 1 || p.DTNT == true)).Select(p => p.MaBNhan).Distinct().Count();
                moi.VV_NguoiNuocNgoai = qVaoVien.Where(p => String.Format("{0:dd/MM/yyyy}", p.NgayVao).Equals(moi.Ngay) &&
                                                      (p.NgoaiKieu != null && p.NgoaiKieu != "Việt Nam") && (p.NoiTru == 1 || p.DTNT == true)).Select(p => p.MaBNhan).Distinct().Count();
                moi.VV_BNTren60 = qVaoVien.Where(p => String.Format("{0:dd/MM/yyyy}", p.NgayKham).Equals(moi.Ngay) && (p.Tuoi >= 60) && (p.NoiTru == 1 || p.DTNT == true)).GroupBy(p => p.MaBNhan).Count();

                moi.TongChuyenVien = qRaVien.Where(p => String.Format("{0:dd/MM/yyyy}", p.rv.NgayRa).Equals(moi.Ngay) && (p.rv.Status == 1)).Select(p => p.rv.MaBNhan).Distinct().Count();
                moi.VV_CapCuu = qVaoVien.Where(p => (p.tenkp.Contains("HSCC") || p.tenkp.ToLower().Contains("hồi sức cấp cứu")) && String.Format("{0:dd/MM/yyyy}", p.NgayKham).Equals(moi.Ngay) && (p.NoiTru == 1 || p.DTNT == true)).Select(p => p.MaBNhan).Distinct().Count();
                _listKB.Add(moi);
            }

            BaoCao.Rep_BC_TongHopKhamBenh_30010 rep = new BaoCao.Rep_BC_TongHopKhamBenh_30010();
            frmIn frm = new frmIn();
            rep.DataSource = _listKB.ToList();
            rep.lblThoiGian.Text = "( Từ ngày " + tungay.ToString("dd/MM/yyyy") + " đến ngày " + denngay.ToString("dd/MM/yyyy") + " )";
            rep.BindingData();
            rep.CreateDocument();
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();
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

        #region class KhamBenh
        public class KhamBenh
        {
            public string Ngay { get; set; }
            public int? BHYT_TongKB { get; set; }
            public int? BHYT_KB_BNTren60 { get; set; }
            public int? BHYT_TongVV { get; set; }
            public int? BHYT_VV_BNTren60 { get; set; }
            public int? ND_TongKB { get; set; }
            public int? ND_KB_BNTren60 { get; set; }
            public int? ND_TongVV { get; set; }
            public int? ND_VV_BNTren60 { get; set; }
            public int? TEDuoi15_BHYT_TongKB { get; set; }
            public int? TEDuoi15_BHYT_VV { get; set; }
            public int? TEDuoi15_ND_TongKB { get; set; }
            public int? TEDuoi15_ND_VV { get; set; }
            public int? TEDuoi6_TongKB { get; set; }
            public int? TEDuoi6_TongVV { get; set; }
            public int? TongKB { get; set; }
            public int? KB_NguoiNuocNgoai { get; set; }
            public int? KB_BNTren60 { get; set; }
            public int? TongVV { get; set; }
            public int? VV_NguoiNuocNgoai { get; set; }
            public int? VV_BNTren60 { get; set; }
            public int? TongChuyenVien { get; set; }
            public int? VV_CapCuu { get; set; }
        }
        #endregion

        #region class KPhong
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
        #endregion
    }
}