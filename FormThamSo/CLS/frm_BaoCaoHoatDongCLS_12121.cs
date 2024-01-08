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
    public partial class frm_BaoCaoHoatDongCLS_12121 : DevExpress.XtraEditors.XtraForm
    {
        public frm_BaoCaoHoatDongCLS_12121()
        {
            InitializeComponent();
        }

        List<DichVu> _ldvAll = new List<DichVu>();
        List<TieuNhomDV> qtn = new List<TieuNhomDV>();
        bool load = false;
        private void frm_BangTHCongNo_12121_Load(object sender, EventArgs e)
        {
            lupNgaytu.DateTime = DateTime.Now;
            lupngayden.DateTime = DateTime.Now;

            memoEdit1.Text = "Mẫu 1: \n - Lấy theo số bệnh nhân thực hiện (ID nhóm = 2 hoặc 3) \n; - Các dịch vụ (các dòng) được tính theo tên rút gọn tiểu nhóm dịch vụ;\n - Bệnh nhân thực hiện dịch vụ thuộc 2 tiểu nhóm có tên RG khác nhau sẽ được tính mỗi loại dịch vụ là 2; \n - 1 bệnh nhân vào nhiều hơn 1 khoa sẽ được tính cho khoa phòng nào chỉ định, khoa phòng có phân loại là phòng khám sẽ tính là ngoại trú, lâm sàng tính là nội trú, \n - bệnh nhân viện phí là bệnh nhân không phải bệnh nhân bảo hiểm; \n Mẫu 2: Tính theo dịch vụ có ID nhóm = 1; \n Sắp xếp theo tiểu nhóm, dịch vụ";
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            DateTime tungay = DungChung.Ham.NgayTu(lupNgaytu.DateTime);
            DateTime denngay = DungChung.Ham.NgayDen(lupngayden.DateTime);
            List<int> lMaDV = new List<int>();
            #region mẫu 1
            if (rdMau.SelectedIndex == 0)
            {
                List<KPhong> lKp = data.KPhongs.Where(p => p.PLoai == "Lâm sàng").OrderBy(p => p.PLoai).ToList();
                int[] lMaKP = new int[] { 0, 0, 0, 0, 0, 0, 0, 0 ,0,0,0};
                BaoCao.rep_BCHoatDongCanLamSang_12121 rep = new BaoCao.rep_BCHoatDongCanLamSang_12121();
                frmIn frm = new frmIn();
                for (int i = 1; i < 11; i++)
                {
                    if (i <= lKp.Count)
                    {
                        lMaKP[i - 1] = lKp.Skip(i - 1).Select(p => p.MaKP).FirstOrDefault();
                        switch (i)
                        {
                            case 1:
                                rep.celTit1.Text = lKp.Skip(i - 1).Select(p => p.TenKP).FirstOrDefault();
                                break;
                            case 2:
                                rep.celTit2.Text = lKp.Skip(i - 1).Select(p => p.TenKP).FirstOrDefault();
                                break;
                            case 3:
                                rep.celTit3.Text = lKp.Skip(i - 1).Select(p => p.TenKP).FirstOrDefault();
                                break;
                            //case 4:
                            //    rep.celTit4.Text = lKp.Skip(i - 1).Select(p => p.TenKP).FirstOrDefault();
                            //    break;
                        }
                    }
                    else
                        break;
                }

                DateTime tungayKham = tungay.AddMonths(-3);
                // tìm khoa phòng cuối cùng bênh nhân đang khám hoặc điều trị (trước thời gian ngày đến)
                var qkp = (from bn in data.BenhNhans
                           join bnkb in data.BNKBs.Where(p => p.NgayKham <= denngay) on bn.MaBNhan equals bnkb.MaBNhan
                           group new { bn, bnkb } by new { bn.MaBNhan, bn.NoiTru,bnkb.IDKB } into kq
                           select new
                           {
                               IDKB=kq.Key.IDKB,
                               MaBNhan = kq.Key.MaBNhan,
                               MaKP = kq.Select(p => p.bnkb.MaKP).FirstOrDefault() //.Where(p => p.bnkb.NgayKham == kq.Max(q => q.bnkb.NgayKham))
                           }).OrderBy(p => p.MaBNhan).ToList();

                var qcls = (from cls in data.CLS.Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay)
                            join cd in data.ChiDinhs.Where(p => p.Status == 1) on cls.IdCLS equals cd.IdCLS
                            join bn in data.BenhNhans on cls.MaBNhan equals bn.MaBNhan
                            select new { bn.MaBNhan, bn.MaDTuong, bn.NoiTru, bn.Tuoi, cls.MaKP, cd.MaDV, bn.SThe, cd.IDCD,bn.DTuong }).ToList();
                var qdv = (from dv in data.DichVus.Where(p => p.PLoai == 2)
                           join tn in data.TieuNhomDVs.Where(p => p.IDNhom == 2 || p.IDNhom == 3) on dv.IdTieuNhom equals tn.IdTieuNhom
                           select new { tn.TenRG, dv.MaDV, Loai = (dv.Loai == null || dv.Loai == 0) ? 1 : dv.Loai.Value, tn.IDNhom }).ToList();
                //var qclsTH = (from cls in qcls
                //              join dv in qdv on cls.MaDV equals dv.MaDV
                //              join kp in qkp on new { MaBNhan = cls.MaBNhan, MaKP = cls.MaKP } equals new { MaBNhan = kp.MaBNhan, MaKP = kp.MaKP }
                //              group new { cls, dv, kp } by new { dv.TenRG, cls.MaBNhan, cls.Tuoi, cls.MaDTuong, cls.NoiTru, kp.MaKP } into kq
                //              select new { TenRG = kq.Key.TenRG.ToLower(), kq.Key.MaBNhan, kq.Key.Tuoi, MaDTuong = kq.Key.MaDTuong.ToLower(), kq.Key.MaKP, kq.Key.NoiTru }).ToList();
                var qclsTH = (from cls in qcls
                              join dv in qdv on cls.MaDV equals dv.MaDV
                              join kp in data.KPhongs on cls.MaKP equals kp.MaKP
                              group new { cls, dv, kp } by new { dv.TenRG, cls.MaBNhan, cls.Tuoi, cls.MaDTuong, cls.MaKP, kp.PLoai,cls.DTuong,cls.IDCD } into kq
                              select new { TenRG = kq.Key.TenRG.ToLower(), kq.Key.MaBNhan, kq.Key.Tuoi, MaDTuong = kq.Key.MaDTuong.ToLower().Trim(), kq.Key.MaKP,kq.Key.DTuong,kq.Key.PLoai,kq.Key.IDCD }).ToList();
                
                #region dòng 1: tổng số bệnh nhân vào khoa cls (ngày thực hiện)
                var kqclsTH = (from a in qclsTH
                               group a by new { a.MaBNhan, a.Tuoi, a.MaDTuong, a.MaKP, a.PLoai, a.DTuong,a.IDCD } into kq
                               select new { kq.Key.MaBNhan, kq.Key.Tuoi, kq.Key.MaDTuong, kq.Key.MaKP, kq.Key.DTuong, kq.Key.PLoai,kq.Key.IDCD }).ToList();
                if (kqclsTH.Count > 0)
                {
                    rep.cel11.Text = kqclsTH.Select(p => p.IDCD).Count().ToString();
                    //chỉ lấy những dịch vụ của bệnh nhân tại khoa đang điều trị

                    var qclsDtri = (from cls in kqclsTH
                                    join kp in qkp on new { MaBNhan = cls.MaBNhan, MaKP = cls.MaKP } equals new { MaBNhan = kp.MaBNhan, MaKP = kp.MaKP }
                                    select new { cls.MaBNhan, cls.MaKP, cls.IDCD }).Distinct().ToList();
                    if (lKp.Count > 0)
                    {
                        rep.cel12.Text = kqclsTH.Where(p => p.MaKP == lKp.Select(m => m.MaKP).FirstOrDefault()).Select(p => p.IDCD).Distinct().Count().ToString();
                    }
                    if (lKp.Count > 1)
                    {
                        rep.cel13.Text = kqclsTH.Where(p => p.MaKP == lKp.Skip(1).Select(m => m.MaKP).FirstOrDefault()).Select(p => p.IDCD).Distinct().Count().ToString();
                    }
                    if (lKp.Count > 2)
                    {
                        rep.cel14.Text = kqclsTH.Where(p => p.MaKP == lKp.Skip(2).Select(m => m.MaKP).FirstOrDefault()).Select(p => p.IDCD).Distinct().Count().ToString();
                    }
                    //if (lKp.Count > 3)
                    //{
                    //    rep.cel15.Text = kqclsTH.Where(p => p.MaKP == lKp.Skip(3).Select(m => m.MaKP).FirstOrDefault()).Select(p => p.IDCD).Distinct().Count().ToString();
                    //}
                    rep.cel16.Text = kqclsTH.Where(p => p.PLoai == "Lâm sàng").Where(p => p.MaDTuong != null && p.MaDTuong != "" && p.MaDTuong.ToLower().Trim() != "hn" && p.MaDTuong.ToLower().Trim() != "cn" && p.MaDTuong.ToLower().Trim() != "dt" && p.Tuoi > 6).Select(p => p.IDCD).Distinct().Count().ToString();

                    rep.cel17.Text = kqclsTH.Where(p => p.PLoai == "Lâm sàng").Where(p => p.MaDTuong.ToLower().Trim() == "hn" || p.MaDTuong.ToLower().Trim() == "cn" || p.MaDTuong.ToLower().Trim() == "dt").Select(p => p.IDCD).Distinct().Count().ToString();
                    rep.cel18.Text = kqclsTH.Where(p => p.PLoai == "Lâm sàng").Where(p => p.Tuoi <= 6).Where(p => p.DTuong == "BHYT").Select(p => p.IDCD).Distinct().Count().ToString();
                    rep.cel19.Text = kqclsTH.Where(p => p.PLoai == "Lâm sàng").Where(p => p.MaDTuong == null || p.MaDTuong.Trim() == "").Select(p => p.IDCD).Distinct().Count().ToString();
                    rep.cel110.Text = kqclsTH.Where(p => p.PLoai == "Phòng khám").Where(p => p.MaDTuong != null && p.MaDTuong != "" && p.MaDTuong.ToLower().Trim() != "hn" && p.MaDTuong.ToLower().Trim() != "cn" && p.MaDTuong.ToLower().Trim() != "dt" && p.Tuoi > 6).Select(p => p.IDCD).Distinct().Count().ToString();
                    rep.cel111.Text = kqclsTH.Where(p => p.PLoai == "Phòng khám").Where(p => p.MaDTuong.ToLower().Trim() == "hn" || p.MaDTuong.ToLower().Trim() == "cn" || p.MaDTuong.ToLower().Trim() == "dt").Select(p => p.IDCD).Distinct().Count().ToString();
                    rep.cel112.Text = kqclsTH.Where(p => p.PLoai == "Phòng khám").Where(p => p.Tuoi <= 6 && p.DTuong == "BHYT").Select(p => p.IDCD).Distinct().Count().ToString();
                    rep.cel113.Text = kqclsTH.Where(p => p.PLoai == "Phòng khám").Where(p => p.MaDTuong == null || p.MaDTuong.Trim() == "").Select(p => p.IDCD).Distinct().Count().ToString();
                }
                #endregion
                #region dòng 2: Siêu âm
                var qclsTH1 = qclsTH.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm.ToLower()).ToList();
                if (qclsTH1.Count > 0)
                {
                    rep.cel21.Text = qclsTH1.Select(p => p.IDCD).Distinct().Count().ToString();
                    var qclsDtri = (from cls in qclsTH1
                                    join kp in qkp on new { MaBNhan = cls.MaBNhan, MaKP = cls.MaKP } equals new { MaBNhan = kp.MaBNhan, MaKP = kp.MaKP }
                                    select new { cls.MaBNhan, cls.MaKP,cls.IDCD }).Distinct();
                    if (lKp.Count > 0)
                    {
                        rep.cel22.Text = qclsDtri.Where(p => p.MaKP == lKp.Select(m => m.MaKP).FirstOrDefault()).Select(p => p.IDCD).Distinct().Count().ToString();
                    }
                    if (lKp.Count > 1)
                    {
                        rep.cel23.Text = qclsDtri.Where(p => p.MaKP == lKp.Skip(1).Select(m => m.MaKP).FirstOrDefault()).Select(p => p.IDCD).Distinct().Count().ToString();
                    }
                    if (lKp.Count > 2)
                    {
                        rep.cel24.Text = qclsDtri.Where(p => p.MaKP == lKp.Skip(2).Select(m => m.MaKP).FirstOrDefault()).Select(p => p.IDCD).Distinct().Count().ToString();
                    }
                    //if (lKp.Count > 3)
                    //{
                    //    rep.cel25.Text = qclsDtri.Where(p => p.MaKP == lKp.Skip(3).Select(m => m.MaKP).FirstOrDefault()).Select(p => p.IDCD).Distinct().Count().ToString();
                    //}
                    if (lKp.Count > 0)
                    {
                        rep.cel22.Text = qclsTH1.Where(p => p.MaKP == lKp.Select(m => m.MaKP).FirstOrDefault()).Select(p => p.IDCD).Distinct().Count().ToString();
                    }
                    if (lKp.Count > 1)
                    {
                        rep.cel23.Text = qclsTH1.Where(p => p.MaKP == lKp.Skip(1).Select(m => m.MaKP).FirstOrDefault()).Select(p => p.IDCD).Distinct().Count().ToString();
                    }
                    if (lKp.Count > 2)
                    {
                        rep.cel24.Text = qclsTH1.Where(p => p.MaKP == lKp.Skip(2).Select(m => m.MaKP).FirstOrDefault()).Select(p => p.IDCD).Distinct().Count().ToString();
                    }
                    //if (lKp.Count > 3)
                    //{
                    //    rep.cel25.Text = qclsTH1.Where(p => p.MaKP == lKp.Skip(3).Select(m => m.MaKP).FirstOrDefault()).Select(p => p.IDCD).Distinct().Count().ToString();
                    //}
                    //rep.cel26.Text = qclsTH1.Where(p => p.NoiTru == 1).Where(p => p.MaDTuong != null && p.MaDTuong != "" && p.MaDTuong.ToLower().Trim() != "hn" && p.MaDTuong.ToLower().Trim() != "cn" && p.MaDTuong.ToLower().Trim() != "dt" && p.Tuoi >= 6).Select(p => p.MaBNhan).Distinct().Count().ToString();

                    //rep.cel27.Text = qclsTH1.Where(p => p.NoiTru == 1).Where(p => p.MaDTuong.ToLower() == "hn" || p.MaDTuong.ToLower() == "cn" || p.MaDTuong.ToLower() == "dt").Select(p => p.MaBNhan).Distinct().Count().ToString();

                    //rep.cel28.Text = qclsTH1.Where(p => p.NoiTru == 1).Where(p => p.Tuoi < 6).Select(p => p.MaBNhan).Distinct().Count().ToString();

                    //rep.cel29.Text = qclsTH1.Where(p => p.NoiTru == 1).Where(p => p.MaDTuong == null || p.MaDTuong.Trim() == "").Select(p => p.MaBNhan).Distinct().Count().ToString();

                    //rep.cel210.Text = qclsTH1.Where(p => p.NoiTru == 0).Where(p => p.MaDTuong != null && p.MaDTuong != "" && p.MaDTuong.ToLower().Trim() != "hn" && p.Tuoi >= 6).Select(p => p.MaBNhan).Distinct().Count().ToString();

                    //rep.cel211.Text = qclsTH1.Where(p => p.NoiTru == 0).Where(p => p.MaDTuong == "hn").Select(p => p.MaBNhan).Distinct().Count().ToString();

                    //rep.cel212.Text = qclsTH1.Where(p => p.NoiTru == 0).Where(p => p.Tuoi < 6).Select(p => p.MaBNhan).Distinct().Count().ToString();

                    //rep.cel213.Text = qclsTH1.Where(p => p.NoiTru == 0).Where(p => p.MaDTuong == null || p.MaDTuong.Trim() == "").Select(p => p.MaBNhan).Distinct().Count().ToString();
                    rep.cel26.Text = qclsTH1.Where(p => p.PLoai == "Lâm sàng").Where(p => p.MaDTuong != null && p.MaDTuong != "" && p.MaDTuong.ToLower().Trim() != "hn" && p.MaDTuong.ToLower().Trim() != "cn" && p.MaDTuong.ToLower().Trim() != "dt" && p.Tuoi > 6).Select(p => p.IDCD).Distinct().Count().ToString();

                    rep.cel27.Text = qclsTH1.Where(p => p.PLoai == "Lâm sàng").Where(p => p.MaDTuong.ToLower() == "hn" || p.MaDTuong.ToLower() == "cn" || p.MaDTuong.ToLower() == "dt").Select(p => p.IDCD).Distinct().Count().ToString();

                    rep.cel28.Text = qclsTH1.Where(p => p.PLoai == "Lâm sàng").Where(p => p.Tuoi <= 6).Where(p => p.DTuong == "BHYT").Select(p => p.IDCD).Distinct().Count().ToString();

                    rep.cel29.Text = qclsTH1.Where(p => p.PLoai == "Lâm sàng").Where(p => p.MaDTuong == null || p.MaDTuong.Trim() == "").Select(p => p.IDCD).Distinct().Count().ToString();

                    rep.cel210.Text = qclsTH1.Where(p => p.PLoai == "Phòng khám").Where(p => p.MaDTuong != null && p.MaDTuong != "" && p.MaDTuong.ToLower().Trim() != "hn" && p.MaDTuong.ToLower().Trim() != "cn" && p.MaDTuong.ToLower().Trim() != "dt" && p.Tuoi > 6).Select(p => p.IDCD).Distinct().Count().ToString();

                    rep.cel211.Text = qclsTH1.Where(p => p.PLoai == "Phòng khám").Where(p => p.MaDTuong == "hn" || p.MaDTuong.ToLower() == "cn" || p.MaDTuong.ToLower() == "dt").Select(p => p.IDCD).Distinct().Count().ToString();

                    rep.cel212.Text = qclsTH1.Where(p => p.PLoai == "Phòng khám").Where(p => p.Tuoi <= 6 && p.DTuong == "BHYT").Select(p => p.IDCD).Distinct().Count().ToString();

                    rep.cel213.Text = qclsTH1.Where(p => p.PLoai == "Phòng khám").Where(p => p.MaDTuong == null || p.MaDTuong.Trim() == "").Select(p => p.IDCD).Distinct().Count().ToString();
                }
                #endregion
                #region điện tâm đồ
                var qclsTH2 = qclsTH.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim.ToLower()).ToList();
                if (qclsTH2.Count > 0)
                {
                    rep.cel31.Text = qclsTH2.Select(p => p.IDCD).Distinct().Count().ToString();
                    var qclsDtri = (from cls in qclsTH2
                                    join kp in qkp on new { MaBNhan = cls.MaBNhan, MaKP = cls.MaKP } equals new { MaBNhan = kp.MaBNhan, MaKP = kp.MaKP }
                                    select new { cls.MaBNhan, cls.MaKP,cls.IDCD }).Distinct();
                    if (lKp.Count > 0)
                    {
                        rep.cel32.Text = qclsDtri.Where(p => p.MaKP == lKp.Select(m => m.MaKP).FirstOrDefault()).Select(p => p.IDCD).Distinct().Count().ToString();
                    }
                    if (lKp.Count > 1)
                    {
                        rep.cel33.Text = qclsDtri.Where(p => p.MaKP == lKp.Skip(1).Select(m => m.MaKP).FirstOrDefault()).Select(p => p.IDCD).Distinct().Count().ToString();
                    }
                    if (lKp.Count > 2)
                    {
                        rep.cel34.Text = qclsDtri.Where(p => p.MaKP == lKp.Skip(2).Select(m => m.MaKP).FirstOrDefault()).Select(p => p.IDCD).Distinct().Count().ToString();
                    }
                    //if (lKp.Count > 3)
                    //{
                    //    rep.cel35.Text = qclsDtri.Where(p => p.MaKP == lKp.Skip(3).Select(m => m.MaKP).FirstOrDefault()).Select(p => p.IDCD).Distinct().Count().ToString();
                    //}
                    //rep.cel36.Text = qclsTH2.Where(p => p.NoiTru == 1).Where(p => p.MaDTuong != null && p.MaDTuong != "" && p.MaDTuong.ToLower().Trim() != "hn" && p.Tuoi >= 6).Select(p => p.MaBNhan).Distinct().Count().ToString();
                    //rep.cel37.Text = qclsTH2.Where(p => p.NoiTru == 1).Where(p => p.MaDTuong == "hn").Select(p => p.MaBNhan).Distinct().Count().ToString();
                    //rep.cel38.Text = qclsTH2.Where(p => p.NoiTru == 1).Where(p => p.Tuoi < 6).Select(p => p.MaBNhan).Distinct().Count().ToString();
                    //rep.cel39.Text = qclsTH2.Where(p => p.NoiTru == 1).Where(p => p.MaDTuong == null || p.MaDTuong.Trim() == "").Select(p => p.MaBNhan).Distinct().Count().ToString();
                    //rep.cel310.Text = qclsTH2.Where(p => p.NoiTru == 0).Where(p => p.MaDTuong != null && p.MaDTuong != "" && p.MaDTuong.ToLower().Trim() != "hn" && p.Tuoi >= 6).Select(p => p.MaBNhan).Distinct().Count().ToString();
                    //rep.cel311.Text = qclsTH2.Where(p => p.NoiTru == 0).Where(p => p.MaDTuong == "hn").Select(p => p.MaBNhan).Distinct().Count().ToString();
                    //rep.cel312.Text = qclsTH2.Where(p => p.NoiTru == 0).Where(p => p.Tuoi < 6).Select(p => p.MaBNhan).Distinct().Count().ToString();
                    //rep.cel313.Text = qclsTH2.Where(p => p.NoiTru == 0).Where(p => p.MaDTuong == null || p.MaDTuong.Trim() == "").Select(p => p.MaBNhan).Distinct().Count().ToString();
                    rep.cel36.Text = qclsTH2.Where(p => p.PLoai == "Lâm sàng").Where(p => p.MaDTuong != null && p.MaDTuong != "" && p.MaDTuong.ToLower().Trim() != "hn" && p.MaDTuong.ToLower().Trim() != "cn" && p.MaDTuong.ToLower().Trim() != "dt" && p.Tuoi > 6).Select(p => p.IDCD).Distinct().Count().ToString();

                    rep.cel37.Text = qclsTH2.Where(p => p.PLoai == "Lâm sàng").Where(p => p.MaDTuong.ToLower() == "hn" || p.MaDTuong.ToLower() == "dt" || p.MaDTuong.ToLower() == "cn").Select(p => p.IDCD).Distinct().Count().ToString();

                    rep.cel38.Text = qclsTH2.Where(p => p.PLoai == "Lâm sàng").Where(p => p.Tuoi <= 6).Where(p => p.DTuong == "BHYT").Select(p => p.IDCD).Distinct().Count().ToString();

                    rep.cel39.Text = qclsTH2.Where(p => p.PLoai == "Lâm sàng").Where(p => p.MaDTuong == null || p.MaDTuong.Trim() == "").Select(p => p.IDCD).Distinct().Count().ToString();

                    rep.cel310.Text = qclsTH2.Where(p => p.PLoai == "Phòng khám").Where(p => p.MaDTuong != null && p.MaDTuong != "" && p.MaDTuong.ToLower().Trim() != "hn" && p.MaDTuong.ToLower().Trim() != "cn" && p.MaDTuong.ToLower().Trim() != "dt" && p.Tuoi > 6).Select(p => p.IDCD).Distinct().Count().ToString();

                    rep.cel311.Text = qclsTH2.Where(p => p.PLoai == "Phòng khám").Where(p => p.MaDTuong == "hn" || p.MaDTuong.ToLower() == "dt" || p.MaDTuong.ToLower() == "cn").Select(p => p.IDCD).Distinct().Count().ToString();

                    rep.cel312.Text = qclsTH2.Where(p => p.PLoai == "Phòng khám").Where(p => p.Tuoi <= 6 && p.DTuong == "BHYT").Select(p => p.IDCD).Distinct().Count().ToString();

                    rep.cel313.Text = qclsTH2.Where(p => p.PLoai == "Phòng khám").Where(p => p.MaDTuong == null || p.MaDTuong.Trim() == "").Select(p => p.IDCD).Distinct().Count().ToString();
                }
                #endregion
                #region đo mật độ xương
                var qclsTH3 = qclsTH.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DoMatDoXuong.ToLower()).ToList();
                if (qclsTH3.Count > 0)
                {
                    rep.cel41.Text = qclsTH3.Select(p => p.IDCD).Distinct().Count().ToString();
                    var qclsDtri = (from cls in qclsTH3
                                    join kp in qkp on new { MaBNhan = cls.MaBNhan, MaKP = cls.MaKP } equals new { MaBNhan = kp.MaBNhan, MaKP = kp.MaKP }
                                    select new { cls.MaBNhan, cls.MaKP,cls.IDCD }).Distinct();
                    if (lKp.Count > 0)
                    {
                        rep.cel42.Text = qclsDtri.Where(p => p.MaKP == lKp.Select(m => m.MaKP).FirstOrDefault()).Select(p => p.IDCD).Distinct().Count().ToString();
                    }
                    if (lKp.Count > 1)
                    {
                        rep.cel43.Text = qclsDtri.Where(p => p.MaKP == lKp.Skip(1).Select(m => m.MaKP).FirstOrDefault()).Select(p => p.IDCD).Distinct().Count().ToString();
                    }
                    if (lKp.Count > 2)
                    {
                        rep.cel44.Text = qclsDtri.Where(p => p.MaKP == lKp.Skip(2).Select(m => m.MaKP).FirstOrDefault()).Select(p => p.IDCD).Distinct().Count().ToString();
                    }
                    //if (lKp.Count > 3)
                    //{
                    //    rep.cel45.Text = qclsDtri.Where(p => p.MaKP == lKp.Skip(3).Select(m => m.MaKP).FirstOrDefault()).Select(p => p.IDCD).Distinct().Count().ToString();
                    //}
                    //rep.cel46.Text = qclsTH3.Where(p => p.NoiTru == 1).Where(p => p.MaDTuong != null && p.MaDTuong.Trim() != "" && p.MaDTuong.ToLower().Trim() != "hn" && p.Tuoi >= 6).Select(p => p.MaBNhan).Distinct().Count().ToString();
                    //rep.cel47.Text = qclsTH3.Where(p => p.NoiTru == 1).Where(p => p.MaDTuong == "hn").Select(p => p.MaBNhan).Distinct().Count().ToString();
                    //rep.cel48.Text = qclsTH3.Where(p => p.NoiTru == 1).Where(p => p.Tuoi < 6).Select(p => p.MaBNhan).Distinct().Count().ToString();
                    //rep.cel49.Text = qclsTH3.Where(p => p.NoiTru == 1).Where(p => p.MaDTuong == null || p.MaDTuong.Trim() == "").Select(p => p.MaBNhan).Distinct().Count().ToString();
                    //rep.cel410.Text = qclsTH3.Where(p => p.NoiTru == 0).Where(p => p.MaDTuong != null && p.MaDTuong.Trim() != "" && p.MaDTuong.ToLower().Trim() != "hn" && p.Tuoi >= 6).Select(p => p.MaBNhan).Distinct().Count().ToString();
                    //rep.cel411.Text = qclsTH3.Where(p => p.NoiTru == 0).Where(p => p.MaDTuong == "hn").Select(p => p.MaBNhan).Distinct().Count().ToString();
                    //rep.cel412.Text = qclsTH3.Where(p => p.NoiTru == 0).Where(p => p.Tuoi < 6).Select(p => p.MaBNhan).Distinct().Count().ToString();
                    //rep.cel413.Text = qclsTH3.Where(p => p.NoiTru == 0).Where(p => p.MaDTuong == null || p.MaDTuong.Trim() == "").Select(p => p.MaBNhan).Distinct().Count().ToString();
                    rep.cel46.Text = qclsTH3.Where(p => p.PLoai == "Lâm sàng").Where(p => p.MaDTuong != null && p.MaDTuong != "" && p.MaDTuong.ToLower().Trim() != "hn" && p.MaDTuong.ToLower().Trim() != "cn" && p.MaDTuong.ToLower().Trim() != "dt" && p.Tuoi > 6).Select(p => p.IDCD).Distinct().Count().ToString();

                    rep.cel47.Text = qclsTH3.Where(p => p.PLoai == "Lâm sàng").Where(p => p.MaDTuong.ToLower() == "hn" || p.MaDTuong.ToLower() == "cn" || p.MaDTuong.ToLower() == "dt").Select(p => p.IDCD).Distinct().Count().ToString();

                    rep.cel48.Text = qclsTH3.Where(p => p.PLoai == "Lâm sàng").Where(p => p.Tuoi <= 6).Where(p => p.DTuong == "BHYT").Select(p => p.IDCD).Distinct().Count().ToString();

                    rep.cel49.Text = qclsTH3.Where(p => p.PLoai == "Lâm sàng").Where(p => p.MaDTuong == null || p.MaDTuong.Trim() == "").Select(p => p.IDCD).Distinct().Count().ToString();

                    rep.cel410.Text = qclsTH3.Where(p => p.PLoai == "Phòng khám").Where(p => p.MaDTuong != null && p.MaDTuong != "" && p.MaDTuong.ToLower().Trim() != "hn" && p.MaDTuong.ToLower().Trim() != "cn" && p.MaDTuong.ToLower().Trim() != "dt" && p.Tuoi > 6).Select(p => p.IDCD).Distinct().Count().ToString();

                    rep.cel411.Text = qclsTH3.Where(p => p.PLoai == "Phòng khám").Where(p => p.MaDTuong == "hn" || p.MaDTuong.ToLower() == "cn" || p.MaDTuong.ToLower() == "dt").Select(p => p.IDCD).Distinct().Count().ToString();

                    rep.cel412.Text = qclsTH3.Where(p => p.PLoai == "Phòng khám").Where(p => p.Tuoi <= 6 && p.DTuong == "BHYT").Select(p => p.IDCD).Distinct().Count().ToString();

                    rep.cel413.Text = qclsTH3.Where(p => p.PLoai == "Phòng khám").Where(p => p.MaDTuong == null || p.MaDTuong.Trim() == "").Select(p => p.IDCD).Distinct().Count().ToString();
                }
                #endregion
                #region điện não đồ
                var qclsTH4 = qclsTH.Where(p => p.TenRG.Contains("điện não")).ToList();
                if (qclsTH4.Count > 0)
                {
                    rep.cel51.Text = qclsTH4.Select(p => p.IDCD).Distinct().Count().ToString();
                    var qclsDtri = (from cls in qclsTH4
                                    join kp in qkp on new { MaBNhan = cls.MaBNhan, MaKP = cls.MaKP } equals new { MaBNhan = kp.MaBNhan, MaKP = kp.MaKP }
                                    select new { cls.MaBNhan, cls.MaKP,cls.IDCD }).Distinct().ToList();
                    if (lKp.Count > 0)
                    {
                        rep.cel52.Text = qclsDtri.Where(p => p.MaKP == lKp.Select(m => m.MaKP).FirstOrDefault()).Select(p => p.IDCD).Distinct().Count().ToString();
                    }
                    if (lKp.Count > 1)
                    {
                        rep.cel53.Text = qclsDtri.Where(p => p.MaKP == lKp.Skip(1).Select(m => m.MaKP).FirstOrDefault()).Select(p => p.IDCD).Distinct().Count().ToString();
                    }
                    if (lKp.Count > 2)
                    {
                        rep.cel54.Text = qclsDtri.Where(p => p.MaKP == lKp.Skip(2).Select(m => m.MaKP).FirstOrDefault()).Select(p => p.IDCD).Distinct().Count().ToString();
                    }
                    //if (lKp.Count > 3)
                    //{
                    //    rep.cel55.Text = qclsDtri.Where(p => p.MaKP == lKp.Skip(3).Select(m => m.MaKP).FirstOrDefault()).Select(p => p.IDCD).Distinct().Count().ToString();
                    //}
                    //rep.cel56.Text = qclsTH4.Where(p => p.NoiTru == 1).Where(p => p.MaDTuong != null && p.MaDTuong != "" && p.MaDTuong.ToLower().Trim() != "hn" && p.Tuoi >= 6).Select(p => p.MaBNhan).Distinct().Count().ToString();
                    //rep.cel57.Text = qclsTH4.Where(p => p.NoiTru == 1).Where(p => p.MaDTuong == "hn").Select(p => p.MaBNhan).Distinct().Count().ToString();
                    //rep.cel58.Text = qclsTH4.Where(p => p.NoiTru == 1).Where(p => p.Tuoi < 6).Select(p => p.MaBNhan).Distinct().Count().ToString();
                    //rep.cel59.Text = qclsTH4.Where(p => p.NoiTru == 1).Where(p => p.MaDTuong == null || p.MaDTuong.Trim() == "").Select(p => p.MaBNhan).Distinct().Count().ToString();
                    //rep.cel510.Text = qclsTH4.Where(p => p.NoiTru == 0).Where(p => p.MaDTuong != null && p.MaDTuong != "" && p.MaDTuong.ToLower().Trim() != "hn" && p.Tuoi >= 6).Select(p => p.MaBNhan).Distinct().Count().ToString();
                    //rep.cel511.Text = qclsTH4.Where(p => p.NoiTru == 0).Where(p => p.MaDTuong == "hn").Select(p => p.MaBNhan).Distinct().Count().ToString();
                    //rep.cel512.Text = qclsTH4.Where(p => p.NoiTru == 0).Where(p => p.Tuoi < 6).Select(p => p.MaBNhan).Distinct().Count().ToString();
                    //rep.cel513.Text = qclsTH4.Where(p => p.NoiTru == 0).Where(p => p.MaDTuong == null || p.MaDTuong.Trim() == "").Select(p => p.MaBNhan).Distinct().Count().ToString();
                    rep.cel56.Text = qclsTH4.Where(p => p.PLoai == "Lâm sàng").Where(p => p.MaDTuong != null && p.MaDTuong != "" && p.MaDTuong.ToLower().Trim() != "hn" && p.MaDTuong.ToLower().Trim() != "cn" && p.MaDTuong.ToLower().Trim() != "dt" && p.Tuoi > 6).Select(p => p.IDCD).Distinct().Count().ToString();

                    rep.cel57.Text = qclsTH4.Where(p => p.PLoai == "Lâm sàng").Where(p => p.MaDTuong.ToLower() == "hn" || p.MaDTuong.ToLower() == "cn" || p.MaDTuong.ToLower() == "dt").Select(p => p.IDCD).Distinct().Count().ToString();

                    rep.cel58.Text = qclsTH4.Where(p => p.PLoai == "Lâm sàng").Where(p => p.Tuoi <= 6).Where(p => p.DTuong == "BHYT").Select(p => p.IDCD).Distinct().Count().ToString();

                    rep.cel59.Text = qclsTH4.Where(p => p.PLoai == "Lâm sàng").Where(p => p.MaDTuong == null || p.MaDTuong.Trim() == "").Select(p => p.IDCD).Distinct().Count().ToString();

                    rep.cel510.Text = qclsTH4.Where(p => p.PLoai == "Phòng khám").Where(p => p.MaDTuong != null && p.MaDTuong != "" && p.MaDTuong.ToLower().Trim() != "hn" && p.MaDTuong.ToLower().Trim() != "cn" && p.MaDTuong.ToLower().Trim() != "dt" && p.Tuoi > 6).Select(p => p.IDCD).Distinct().Count().ToString();

                    rep.cel511.Text = qclsTH4.Where(p => p.PLoai == "Phòng khám").Where(p => p.MaDTuong == "hn" || p.MaDTuong.ToLower() == "cn" || p.MaDTuong.ToLower() == "dt").Select(p => p.IDCD).Distinct().Count().ToString();

                    rep.cel512.Text = qclsTH4.Where(p => p.PLoai == "Phòng khám").Where(p => p.Tuoi <= 6 && p.DTuong == "BHYT").Select(p => p.IDCD).Distinct().Count().ToString();

                    rep.cel513.Text = qclsTH4.Where(p => p.PLoai == "Phòng khám").Where(p => p.MaDTuong == null || p.MaDTuong.Trim() == "").Select(p => p.IDCD).Distinct().Count().ToString();
                }
                #endregion
                #region x quang
                var qclsTH5 = qclsTH.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang.ToLower()).ToList();
                var qclsDtri5 = (from cls in qclsTH5
                                 join kp in qkp on new { MaBNhan = cls.MaBNhan, MaKP = cls.MaKP } equals new { MaBNhan = kp.MaBNhan, MaKP = kp.MaKP }
                                 select new { cls.MaBNhan, cls.MaKP,cls.IDCD }).ToList();
                if (qclsTH5.Count > 0)
                {
                    rep.cel61.Text = qclsTH5.Select(p => p.IDCD).Distinct().Count().ToString();

                    if (lKp.Count > 0)
                    {
                        rep.cel62.Text = qclsDtri5.Where(p => p.MaKP == lKp.Select(m => m.MaKP).FirstOrDefault()).Select(p => p.IDCD).Distinct().Count().ToString();
                    }
                    if (lKp.Count > 1)
                    {
                        rep.cel63.Text = qclsDtri5.Where(p => p.MaKP == lKp.Skip(1).Select(m => m.MaKP).FirstOrDefault()).Select(p => p.IDCD).Distinct().Count().ToString();
                    }
                    if (lKp.Count > 2)
                    {
                        rep.cel64.Text = qclsDtri5.Where(p => p.MaKP == lKp.Skip(2).Select(m => m.MaKP).FirstOrDefault()).Select(p => p.IDCD).Distinct().Count().ToString();
                    }
                    //if (lKp.Count > 3)
                    //{
                    //    rep.cel65.Text = qclsDtri5.Where(p => p.MaKP == lKp.Skip(3).Select(m => m.MaKP).FirstOrDefault()).Select(p => p.IDCD).Distinct().Count().ToString();
                    //}

                    //rep.cel66.Text = qclsTH5.Where(p => p.NoiTru == 1).Where(p => p.MaDTuong != null && p.MaDTuong != "" && p.MaDTuong.ToLower().Trim() != "hn" && p.Tuoi >= 6).Select(p => p.MaBNhan).Distinct().Count().ToString();
                    //rep.cel67.Text = qclsTH5.Where(p => p.NoiTru == 1).Where(p => p.MaDTuong == "hn").Select(p => p.MaBNhan).Distinct().Count().ToString();
                    //rep.cel68.Text = qclsTH5.Where(p => p.NoiTru == 1).Where(p => p.Tuoi < 6).Select(p => p.MaBNhan).Distinct().Count().ToString();
                    //rep.cel69.Text = qclsTH5.Where(p => p.NoiTru == 1).Where(p => p.MaDTuong == null || p.MaDTuong.Trim() == "").Select(p => p.MaBNhan).Distinct().Count().ToString();
                    //rep.cel610.Text = qclsTH5.Where(p => p.NoiTru == 0).Where(p => p.MaDTuong != null && p.MaDTuong != "" && p.MaDTuong.ToLower().Trim() != "hn" && p.Tuoi >= 6).Select(p => p.MaBNhan).Distinct().Count().ToString();
                    //rep.cel611.Text = qclsTH5.Where(p => p.NoiTru == 0).Where(p => p.MaDTuong == "hn").Select(p => p.MaBNhan).Distinct().Count().ToString();
                    //rep.cel612.Text = qclsTH5.Where(p => p.NoiTru == 0).Where(p => p.Tuoi < 6).Select(p => p.MaBNhan).Distinct().Count().ToString();
                    //rep.cel613.Text = qclsTH5.Where(p => p.NoiTru == 0).Where(p => p.MaDTuong == null || p.MaDTuong.Trim() == "").Select(p => p.MaBNhan).Distinct().Count().ToString();
                    rep.cel66.Text = qclsTH5.Where(p => p.PLoai == "Lâm sàng").Where(p => p.MaDTuong != null && p.MaDTuong != "" && p.MaDTuong.ToLower().Trim() != "hn" && p.MaDTuong.ToLower().Trim() != "cn" && p.MaDTuong.ToLower().Trim() != "dt" && p.Tuoi > 6).Select(p => p.IDCD).Distinct().Count().ToString();

                    rep.cel67.Text = qclsTH5.Where(p => p.PLoai == "Lâm sàng").Where(p => p.MaDTuong.ToLower() == "hn" || p.MaDTuong.ToLower() == "cn" || p.MaDTuong.ToLower() == "dt").Select(p => p.IDCD).Distinct().Count().ToString();

                    rep.cel68.Text = qclsTH5.Where(p => p.PLoai == "Lâm sàng").Where(p => p.Tuoi <= 6).Where(p => p.DTuong == "BHYT").Select(p => p.IDCD).Distinct().Count().ToString();

                    rep.cel69.Text = qclsTH5.Where(p => p.PLoai == "Lâm sàng").Where(p => p.MaDTuong == null || p.MaDTuong.Trim() == "").Select(p => p.IDCD).Distinct().Count().ToString();

                    rep.cel610.Text = qclsTH5.Where(p => p.PLoai == "Phòng khám").Where(p => p.MaDTuong != null && p.MaDTuong != "" && p.MaDTuong.ToLower().Trim() != "hn" && p.MaDTuong.ToLower().Trim() != "cn" && p.MaDTuong.ToLower().Trim() != "dt" && p.Tuoi >= 6).Select(p => p.IDCD).Distinct().Count().ToString();

                    rep.cel611.Text = qclsTH5.Where(p => p.PLoai == "Phòng khám").Where(p => p.MaDTuong == "hn" || p.MaDTuong.ToLower() == "cn" || p.MaDTuong.ToLower() == "dt").Select(p => p.IDCD).Distinct().Count().ToString();

                    rep.cel612.Text = qclsTH5.Where(p => p.PLoai == "Phòng khám").Where(p => p.Tuoi <= 6 && p.DTuong == "BHYT").Select(p => p.IDCD).Distinct().Count().ToString();

                    rep.cel613.Text = qclsTH5.Where(p => p.PLoai == "Phòng khám").Where(p => p.MaDTuong == null || p.MaDTuong.Trim() == "").Select(p => p.IDCD).Distinct().Count().ToString();
                }
                #endregion
                #region Tổng số lượt Xquang
                var qclsTH6 = (from cls in qcls
                              join dv in qdv on cls.MaDV equals dv.MaDV
                              join kp in data.KPhongs on cls.MaKP equals kp.MaKP
                              group new { cls, dv, kp } by new { dv.TenRG, dv.Loai, cls.MaBNhan, cls.Tuoi, cls.MaDTuong, cls.NoiTru, cls.MaKP, kp.PLoai, cls.DTuong,cls.IDCD } into kq
                              select new { TenRG = kq.Key.TenRG.ToLower(), kq.Key.Loai, kq.Key.MaBNhan, kq.Key.Tuoi, MaDTuong = kq.Key.MaDTuong.ToLower().Trim(), kq.Key.MaKP, kq.Key.NoiTru, kq.Key.DTuong, kq.Key.PLoai }).ToList();
                var qclsTH61 = qclsTH6.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang.ToLower()).ToList();
                var qclsDtri51 = (from cls in qclsTH61
                                 join kp in qkp on new { MaBNhan = cls.MaBNhan, MaKP = cls.MaKP } equals new { MaBNhan = kp.MaBNhan, MaKP = kp.MaKP }
                                 select new { cls.MaBNhan, cls.MaKP, cls.Loai }).ToList();
                if (qclsTH61.Count > 0)
                {
                    rep.cel71.Text = qclsTH61.Sum(p => p.Loai).ToString();
                    if (lKp.Count > 0)
                    {
                        rep.cel72.Text = qclsDtri51.Where(p => p.MaKP == lKp.Select(m => m.MaKP).FirstOrDefault()).Sum(p => p.Loai).ToString();
                    }
                    if (lKp.Count > 1)
                    {
                        rep.cel73.Text = qclsDtri51.Where(p => p.MaKP == lKp.Skip(1).Select(m => m.MaKP).FirstOrDefault()).Sum(p => p.Loai).ToString();
                    }
                    if (lKp.Count > 2)
                    {
                        rep.cel74.Text = qclsDtri51.Where(p => p.MaKP == lKp.Skip(2).Select(m => m.MaKP).FirstOrDefault()).Sum(p => p.Loai).ToString();
                    }
                    //if (lKp.Count > 3)
                    //{
                    //    rep.cel75.Text = qclsDtri51.Where(p => p.MaKP == lKp.Skip(3).Select(m => m.MaKP).FirstOrDefault()).Sum(p => p.Loai).ToString();
                    //}
                    rep.cel76.Text = qclsTH61.Where(p => p.PLoai == "Lâm sàng").Where(p => p.MaDTuong != null && p.MaDTuong != "" && p.MaDTuong.ToLower().Trim() != "hn" && p.MaDTuong.ToLower().Trim() != "cn" && p.MaDTuong.ToLower().Trim() != "dt" && p.Tuoi > 6).Sum(p => p.Loai).ToString();

                    rep.cel77.Text = qclsTH61.Where(p => p.PLoai == "Lâm sàng").Where(p => p.MaDTuong.ToLower() == "hn" || p.MaDTuong.ToLower() == "cn" || p.MaDTuong.ToLower() == "dt").Sum(p => p.Loai).ToString();

                    rep.cel78.Text = qclsTH61.Where(p => p.PLoai == "Lâm sàng").Where(p => p.Tuoi <= 6).Where(p => p.DTuong == "BHYT").Sum(p => p.Loai).ToString();

                    rep.cel79.Text = qclsTH61.Where(p => p.PLoai == "Lâm sàng").Where(p => p.MaDTuong == null || p.MaDTuong.Trim() == "").Sum(p => p.Loai).ToString();

                    rep.cel710.Text = qclsTH61.Where(p => p.PLoai == "Phòng khám").Where(p => p.MaDTuong != null && p.MaDTuong != "" && p.MaDTuong.ToLower().Trim() != "hn" && p.MaDTuong.ToLower().Trim() != "cn" && p.MaDTuong.ToLower().Trim() != "dt" && p.Tuoi > 6).Sum(p => p.Loai).ToString();

                    rep.cel711.Text = qclsTH61.Where(p => p.PLoai == "Phòng khám").Where(p => p.MaDTuong.ToLower() == "hn" || p.MaDTuong.ToLower() == "cn" || p.MaDTuong.ToLower() == "dt").Sum(p => p.Loai).ToString();

                    rep.cel712.Text = qclsTH61.Where(p => p.PLoai == "Phòng khám").Where(p => p.Tuoi < 6).Where(p => p.DTuong == "BHYT").Sum(p => p.Loai).ToString();

                    rep.cel713.Text = qclsTH61.Where(p => p.PLoai == "Phòng khám").Where(p => p.MaDTuong == null || p.MaDTuong.Trim() == "").Sum(p => p.Loai).ToString();
                }
                #endregion

                if (DungChung.Ham.NgayTu(denngay) == tungay)
                    rep.celNgayThang.Text = "Trong ngày";
                else
                    rep.celNgayThang.Text = "Từ ngày " + tungay.ToString("dd/MM/yyyy") + " đến ngày " + denngay.ToString("dd/MM/yyyy");
                //rep.DataSource = qTH2;
                //rep.BindingData();
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();

            }
            #endregion
            #region mẫu 2
            else
            {
                List<KPhong> lKp = data.KPhongs.Where(p => p.PLoai == "Lâm sàng").OrderBy(p => p.MaKP).ToList();
                int[] lMaKP = new int[] { 0, 0, 0, 0, 0, 0, 0, 0 };
                BaoCao.rep_BaoCaoCongTacKhoaCLS_12121 rep = new BaoCao.rep_BaoCaoCongTacKhoaCLS_12121();
                frmIn frm = new frmIn();
                for (int i = 0; i < lKp.Count; i++)
                {
                    if (i <= lKp.Count)
                    {
                        //lMaKP[i - 1] = lKp.Skip(i - 1).Select(p => p.MaKP).FirstOrDefault();
                        switch (i)
                        {
                            case 0:
                                rep.celTit1.Text = lKp[0].TenKP;
                                break;
                            case 1:
                                rep.celTit2.Text = lKp[1].TenKP;
                                break;
                            case 2:
                                rep.celTit3.Text = lKp[2].TenKP;
                                break;
                            //case 3:
                            //    rep.celTit4.Text = lKp[3].TenKP;
                            //    break;
                        }
                    }
                    else
                        break;
                }
                var qdv = (from tn in data.TieuNhomDVs.Where(p => p.IDNhom == 1)
                           join dv in data.DichVus on tn.IdTieuNhom equals dv.IdTieuNhom
                           select new { tn.TenRG, tn.TenTN, dv.TenDV, dv.MaDV }).ToList();

                var qcls = (from cls in data.CLS.Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay)
                            join cd in data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                            join bn in data.BenhNhans on cls.MaBNhan equals bn.MaBNhan
                            select new
                            {
                                bn.MaBNhan,
                                bn.SThe,
                                bn.MaDTuong,
                                bn.NoiTru,
                                bn.Tuoi,
                                cd.MaDV,
                                cls.MaKP
                            }).ToList();
                var qcls2 = (from dvu in qdv
                             join cls in qcls on dvu.MaDV equals cls.MaDV
                             group new { dvu, cls } by new { dvu.MaDV, dvu.TenDV, dvu.TenRG, dvu.TenTN, cls.MaBNhan, cls.SThe, cls.NoiTru, cls.MaDTuong, cls.Tuoi, cls.MaKP } into kq
                             select new { kq.Key.MaDV, kq.Key.TenDV, kq.Key.TenRG, kq.Key.TenTN, kq.Key.MaBNhan, kq.Key.SThe, kq.Key.NoiTru, MaDTuong = kq.Key.MaDTuong.ToLower().Trim(), kq.Key.Tuoi, kq.Key.MaKP }).ToList();
                var BC = (from bc in qcls2
                          group bc by new { bc.MaDV, bc.TenTN, bc.TenRG, bc.TenDV } into kq
                          select new
                          {
                              kq.Key.MaDV,
                              kq.Key.TenTN,
                              kq.Key.TenRG,
                              kq.Key.TenDV,
                              TongSo = kq.Count(),
                              Khoa1 = (lKp.Count > 0) ? kq.Where(p => p.MaKP == lKp[0].MaKP).Count() : 0,
                              Khoa2 = (lKp.Count > 1) ? kq.Where(p => p.MaKP == lKp[1].MaKP).Count() : 0,
                              Khoa3 = (lKp.Count > 2) ? kq.Where(p => p.MaKP == lKp[2].MaKP).Count() : 0,
                              //Khoa4 = (lKp.Count > 4) ? kq.Where(p => p.MaKP == lKp[3].MaKP).Count() : 0,

                              NT_BH = kq.Where(p => p.NoiTru == 1 && p.SThe != null && p.SThe != "" && p.MaDTuong != "hn" && p.Tuoi >= 6).Count(),
                              NT_HN = kq.Where(p => p.NoiTru == 1 && p.MaDTuong == "hn").Count(),
                              NT_TE = kq.Where(p => p.NoiTru == 1 && p.SThe != "" && p.SThe != null && (p.Tuoi < 6 || p.Tuoi == null)).Count(),
                              NT_VP = kq.Where(p => p.NoiTru == 1 && (p.SThe == null || p.SThe == "")).Count(),

                              NgT_BH = kq.Where(p => p.NoiTru == 0 && p.SThe != null && p.SThe != "" && p.MaDTuong != "hn" && p.Tuoi >= 6).Count(),
                              NgT_HN = kq.Where(p => p.NoiTru == 0 && p.MaDTuong == "hn").Count(),
                              NgT_TE = kq.Where(p => p.NoiTru == 0 && p.SThe != "" && p.SThe != null && (p.Tuoi < 6 || p.Tuoi == null)).Count(),
                              NgT_VP = kq.Where(p => p.NoiTru == 0 && (p.SThe == null || p.SThe == "")).OrderBy(p => p.TenTN).ThenBy(p => p.TenDV).Count(),

                          }).OrderBy(p => p.TenTN).ThenBy(p => p.TenDV).ToList();

                rep.celNgayThang.Text = "Từ ngày " + tungay.ToString("dd/MM/yyyy") + " đến ngày " + denngay.ToString("dd/MM/yyyy");
                rep.DataSource = BC;
                rep.BindingData();
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();

                //var listcb = from cb in db.DMCanBoes
                //             join ad in db.Admins on cb.MaCB equals ad.MaCB
                //             where ad.MaCB == macb & ad.Ploai > 0
                //             select new
                //             {
                //                 cb.MaCB,
                //                 cb.TenCB,
                //                 ad.TenDN,
                //             };


                //var rs = (from pq in db.PhanQuyens
                //          join l in listcb on pq.MaCB equals l.MaCB into kq
                //          from kq2 in kq.DefaultIfEmpty()
                //          select new
                //          {
                //              MaCB = kq2 != null ? kq2.MaCB : 0,
                //              TenCB = kq2 != null ? kq2.TenCB : "",
                //              TenDN = kq2 != null ? kq2.TenDN : "",
                //              IDPQ = pq != null ? pq.IDPQ : 0,
                //              MaDoiTuong = pq != null ? pq.MaDoiTuong : 0,
                //              Sua = pq.Sua,
                //              Them = pq.Them,
                //              Xem = pq.Xem,
                //              Xoa = pq.Xoa
                //          }).ToList();

            }
            #endregion

        }

        private void xtraTabPage1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}