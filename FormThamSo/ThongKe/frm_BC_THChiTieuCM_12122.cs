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
    public partial class frm_BC_THChiTieuCM_12122 : DevExpress.XtraEditors.XtraForm
    {
        public frm_BC_THChiTieuCM_12122()
        {
            InitializeComponent();
        }

        private void frm_BC_THChiTieuCM_12122_Load(object sender, EventArgs e)
        {
            lupTuNgay.DateTime = DateTime.Now;
            lupDenNgay.DateTime = DateTime.Now;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        List<NDChiTieu> _lchitieu = new List<NDChiTieu>();
        private void btnOK_Click(object sender, EventArgs e)
        {
            _lchitieu.Clear();
            NDChiTieu nd = new NDChiTieu();
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            DateTime tungay = DungChung.Ham.NgayTu(lupTuNgay.DateTime);
            DateTime denngay = DungChung.Ham.NgayDen(lupDenNgay.DateTime);

            #region ngày theo từng quý
            DateTime? tungayquy1 = null;
            DateTime? denngayquy1 = null;
            DateTime? tungayquy2 = null;
            DateTime? denngayquy2 = null;
            DateTime? tungayquy3 = null;
            DateTime? denngayquy3 = null;
            DateTime? tungayquy4 = null;
            DateTime? denngayquy4 = null;
            List<Ngay> ds = new List<Ngay>();
            ds = dsNgay(tungay, denngay);
            if (ds.Where(p => p.Quy == 1).Count() > 0)
            {
                tungayquy1 = ds.Where(p => p.Quy == 1).FirstOrDefault().Day;
                denngayquy1 = ds.Where(p => p.Quy == 1).LastOrDefault().Day;
                tungayquy1 = DungChung.Ham.NgayTu(tungayquy1.GetValueOrDefault());
                denngayquy1 = DungChung.Ham.NgayDen(denngayquy1.GetValueOrDefault());
            }

            if (ds.Where(p => p.Quy == 2).Count() > 0)
            {
                tungayquy2 = ds.Where(p => p.Quy == 2).FirstOrDefault().Day;
                denngayquy2 = ds.Where(p => p.Quy == 2).LastOrDefault().Day;
                tungayquy2 = DungChung.Ham.NgayTu(tungayquy2.GetValueOrDefault());
                denngayquy2 = DungChung.Ham.NgayDen(denngayquy2.GetValueOrDefault());
            }

            if (ds.Where(p => p.Quy == 3).Count() > 0)
            {
                tungayquy3 = ds.Where(p => p.Quy == 3).FirstOrDefault().Day;
                denngayquy3 = ds.Where(p => p.Quy == 3).LastOrDefault().Day;
                tungayquy3 = DungChung.Ham.NgayTu(tungayquy3.GetValueOrDefault());
                denngayquy3 = DungChung.Ham.NgayDen(denngayquy3.GetValueOrDefault());
            }

            if (ds.Where(p => p.Quy == 4).Count() > 0)
            {
                tungayquy4 = ds.Where(p => p.Quy == 4).FirstOrDefault().Day;
                denngayquy4 = ds.Where(p => p.Quy == 4).LastOrDefault().Day;
                tungayquy4 = DungChung.Ham.NgayTu(tungayquy4.GetValueOrDefault());
                denngayquy4 = DungChung.Ham.NgayDen(denngayquy4.GetValueOrDefault());
            }
            #endregion

            #region query
            string strtinh = "";
            var tinh = (from h in data.DmHuyens.Where(p => p.MaHuyen == DungChung.Bien.MaHuyen)
                        join t in data.DmTinhs on h.MaTinh equals t.MaTinh
                        select new { t.TenTinh }).ToList();

            strtinh = tinh.Count > 0 ? tinh.FirstOrDefault().TenTinh : "";

            var qcb = (from cb in data.CanBoes select new { cb.TenCB }).Distinct();
            var qkb = (from  bn in data.BenhNhans.Where(p=>p.NNhap >= tungay && p.NNhap <= denngay) select new { bn.MaDTuong, bn.DTuong, bn.Tuoi, bn.NoiTru, bn.NNhap }).ToList(); //(from  bn in data.BenhNhans.Where(p=>p.NNhap >= tungay && p.NNhap <= denngay)  join kb in data.BNKBs  on bn.MaBNhan equals kb.MaBNhan select new { kb.NgayKham, kb.MaBNhan, bn.MaDTuong, bn.DTuong, bn.Tuoi, bn.NoiTru, bn.NNhap }).ToList();// (from kb in data.BNKBs join bn in data.BenhNhans on kb.MaBNhan equals bn.MaBNhan select new { kb.NgayKham, kb.MaBNhan, bn.MaDTuong, bn.DTuong,bn.Tuoi, bn.NoiTru }).ToList();
            var qrv = (from bn in data.BenhNhans
                       join rv in data.RaViens.Where(p=>p.NgayRa >= tungay && p.NgayRa <= denngay) on bn.MaBNhan equals rv.MaBNhan
                       select new { bn.MaBNhan,bn.DTNT, bn.NoiTru, rv.NgayRa, rv.SoNgaydt }).ToList();
            var qdt = (from bn in data.BenhNhans.Where(p => p.NNhap >= tungay && p.NNhap <= denngay) 
                       join vv in data.VaoViens on bn.MaBNhan equals vv.MaBNhan
                       select new { bn.MaBNhan, vv.NgayVao,bn.NNhap, bn.NoiTru, bn.Tuoi, bn.MaDTuong, bn.DTuong, bn.DTNT }).ToList();
            #endregion

            #region TSCBCNV
            nd = new NDChiTieu();
            nd.Stt = 1;
            nd.ChiTieu = "TSCBCNV";
            nd.DonVi = "CB";
            nd.ChiTieuNam = 93;
            nd.THNam = qcb.Count();
            nd.TyLeNam = Math.Round((double)((qcb.Count() / 93) * 100), 2);
            _lchitieu.Add(nd);
            #endregion
            #region Giường bệnh
            //int tongGiuong = 0;
            //var khoa = data.KPhongs.Where(p => p.BuongGiuong != null && p.BuongGiuong != "").ToList();
            //if (khoa.Count > 0)
            //{
            //    foreach (var item in khoa)
            //    {
            //        tongGiuong += SoGiuong(item.BuongGiuong);
            //    }
            //}
            nd = new NDChiTieu();
            nd.Stt = 2;
            nd.ChiTieu = "Giường bệnh";
            nd.DonVi = "Giường";
            nd.ChiTieuNam = 60;
            nd.THNam = 60;// tongGiuong;
            nd.TyLeNam = 100;//Math.Round((double)((tongGiuong / 60) * 100), 2);
            _lchitieu.Add(nd);
            #endregion
            #region Tổng số lần khám bệnh
            nd = new NDChiTieu();
            nd.Stt = 3;
            nd.ChiTieu = "Tổng số lần khám bệnh";
            nd.DonVi = "Lần";
            nd.ChiTieuNam = 2800;
            if (tungay != null && denngay != null)
            {
                nd.THNam = qkb.Count();
                nd.TyLeNam = Math.Round((double)(((double)nd.THNam.GetValueOrDefault() / 2800) * 100), 2);
            }
            if (tungayquy1 != null && denngayquy1 != null)
            {
                nd.THQuy1 = qkb.Where(p => p.NNhap >= tungayquy1 && p.NNhap <= denngayquy1).Count();
                nd.TyLeQuy1 = Math.Round((double)(((double)nd.THQuy1.GetValueOrDefault() / 2800) * 100), 2);
            }
            if (tungayquy2 != null && denngayquy2 != null)
            {
                nd.THQuy2 = qkb.Where(p => p.NNhap >= tungayquy2 && p.NNhap <= denngayquy2).Count();
                nd.TyLeQuy2 = Math.Round((double)(((double)nd.THQuy2.GetValueOrDefault() / 2800) * 100), 2);
            }
            if (tungayquy3 != null && denngayquy3 != null)
            {
                nd.THQuy3 = qkb.Where(p => p.NNhap >= tungayquy3 && p.NNhap <= denngayquy3).Count();
                nd.TyLeQuy3 = Math.Round((double)(((double)nd.THQuy3.GetValueOrDefault() / 2800) * 100), 2);
            }
            if (tungayquy4 != null && denngayquy4 != null)
            {
                nd.THQuy4 = qkb.Where(p => p.NNhap >= tungayquy4 && p.NNhap <= denngayquy4).Distinct().Count();
                nd.TyLeQuy4 = Math.Round((double)(((double)nd.THQuy4.GetValueOrDefault() / 2800) * 100), 2);
            }
            _lchitieu.Add(nd);
            #endregion
            #region Ngày điều trị trung bình
            nd = new NDChiTieu();
            nd.Stt = 4;
            nd.ChiTieu = "Ngày điều trị trung bình";
            nd.DonVi = "Ngày";
            nd.ChiTieuNam = 16;
            var qngayDT = qrv.Where(p => p.NoiTru == 1 || (p.NoiTru == 0 && p.DTNT == true)).ToList();
            if (tungay != null && denngay != null)
            {               
                nd.THNam = (qngayDT.Select(p => p.MaBNhan).Distinct().Count() == 0) ? 0 :(double) qngayDT.Sum(p => p.SoNgaydt) / qngayDT.Select(p => p.MaBNhan).Distinct().Count();
                nd.THNam = Math.Round((double)nd.THNam.GetValueOrDefault(), 2);
                nd.TyLeNam = Math.Round((double)(((double)nd.THNam.GetValueOrDefault() / 16) * 100), 2);
            }
            if (tungayquy1 != null && denngayquy1 != null)
            {
               
                nd.THQuy1 = (qngayDT.Where(p => p.NgayRa >= tungayquy1 && p.NgayRa <= denngayquy1).Select(p => p.MaBNhan).Distinct().Count() == 0) ? 0 : (double)qngayDT.Where(p => p.NgayRa >= tungayquy1 && p.NgayRa <= denngayquy1).Sum(p => p.SoNgaydt) / qngayDT.Where(p => p.NgayRa >= tungayquy1 && p.NgayRa <= denngayquy1).Select(p => p.MaBNhan).Distinct().Count();
                nd.THQuy1 = Math.Round((double)nd.THQuy1.GetValueOrDefault(), 2);
                nd.TyLeQuy1 = Math.Round((double)(((double)nd.THQuy1.GetValueOrDefault() / 16) * 100), 2);
            }
            if (tungayquy2 != null && denngayquy2 != null)
            {
                nd.THQuy2 = (qngayDT.Where(p => p.NgayRa >= tungayquy2 && p.NgayRa <= denngayquy2).Select(p => p.MaBNhan).Distinct().Count() == 0) ? 0 : (double)qngayDT.Where(p => p.NgayRa >= tungayquy2 && p.NgayRa <= denngayquy2).Sum(p => p.SoNgaydt) / qngayDT.Where(p => p.NgayRa >= tungayquy2 && p.NgayRa <= denngayquy2).Select(p => p.MaBNhan).Distinct().Count();
                nd.THQuy2 = Math.Round((double)nd.THQuy2.GetValueOrDefault(), 2);
                nd.TyLeQuy2 = Math.Round((double)(((double)nd.THQuy2.GetValueOrDefault() / 16) * 100), 2);
            }
            if (tungayquy3 != null && denngayquy3 != null)
            {
                nd.THQuy3 = (qngayDT.Where(p => p.NgayRa >= tungayquy3 && p.NgayRa <= denngayquy3).Select(p => p.MaBNhan).Distinct().Count() == 0) ? 0 : (double)qngayDT.Where(p => p.NgayRa >= tungayquy3 && p.NgayRa <= denngayquy3).Sum(p => p.SoNgaydt) / qngayDT.Where(p => p.NgayRa >= tungayquy3 && p.NgayRa <= denngayquy3).Select(p => p.MaBNhan).Distinct().Count();
                nd.THQuy3 = Math.Round((double)nd.THQuy3.GetValueOrDefault(), 2);
                nd.TyLeQuy3 = Math.Round((double)(((double)nd.THQuy3.GetValueOrDefault() / 16) * 100), 2);
            }
            if (tungayquy4 != null && denngayquy4 != null)
            {
                nd.THQuy4 = (qngayDT.Where(p => p.NgayRa >= tungayquy4 && p.NgayRa <= denngayquy4).Select(p => p.MaBNhan).Distinct().Count() == 0) ? 0 : (double)qngayDT.Where(p => p.NgayRa >= tungayquy4 && p.NgayRa <= denngayquy4).Sum(p => p.SoNgaydt) / qngayDT.Where(p => p.NgayRa >= tungayquy4 && p.NgayRa <= denngayquy4).Select(p => p.MaBNhan).Distinct().Count();
                nd.THQuy4 = Math.Round((double)nd.THQuy4.GetValueOrDefault(), 2);
                nd.TyLeQuy4 = Math.Round((double)(((double)nd.THQuy4.GetValueOrDefault() / 16) * 100), 2);
            }
            _lchitieu.Add(nd);
            #endregion
            #region Tổng số ngày điều trị
            nd = new NDChiTieu();
            nd.Stt = 5;
            nd.ChiTieu = "Tổng số ngày điều trị";
            nd.DonVi = "Ngày";
            nd.ChiTieuNam = 15400;
            if (tungay != null && denngay != null)
            {
                nd.THNam = qngayDT.Sum(p => p.SoNgaydt);
                nd.TyLeNam = Math.Round((double)(((double)nd.THNam.GetValueOrDefault() / 15400) * 100), 2);
            }
            if (tungayquy1 != null && denngayquy1 != null)
            {
                nd.THQuy1 = qngayDT.Where(p => p.NgayRa >= tungayquy1 && p.NgayRa <= denngayquy1).Sum(p => p.SoNgaydt);
                nd.TyLeQuy1 = Math.Round((double)(((double)nd.THQuy1.GetValueOrDefault() / 15400) * 100), 2);
            }
            if (tungayquy2 != null && denngayquy2 != null)
            {
                nd.THQuy2 = qngayDT.Where(p => p.NgayRa >= tungayquy2 && p.NgayRa <= denngayquy2).Sum(p => p.SoNgaydt);
                nd.TyLeQuy2 = Math.Round((double)(((double)nd.THQuy2.GetValueOrDefault() / 15400) * 100), 2);
            }
            if (tungayquy3 != null && denngayquy3 != null)
            {
                nd.THQuy3 = qngayDT.Where(p => p.NgayRa >= tungayquy3 && p.NgayRa <= denngayquy3).Sum(p => p.SoNgaydt);
                nd.TyLeQuy3 = Math.Round((double)(((double)nd.THQuy3.GetValueOrDefault() / 15400) * 100), 2);
            }
            if (tungayquy4 != null && denngayquy4 != null)
            {
                nd.THQuy4 = qngayDT.Where(p => p.NgayRa >= tungayquy4 && p.NgayRa <= denngayquy4).Sum(p => p.SoNgaydt);
                nd.TyLeQuy4 = Math.Round((double)(((double)nd.THQuy4.GetValueOrDefault() / 15400) * 100), 2);
            }
            _lchitieu.Add(nd);
            #endregion
            #region Công suất sử dụng giường bệnh
            nd = new NDChiTieu();
            nd.Stt = 6;
            nd.ChiTieu = "Công suất sử dụng giường bệnh";
            nd.DonVi = "%";
            nd.ChiTieuNam = 80;
            //nd.THNam = qcb.Count();
            //nd.TyLeNam = Math.Round((double)((qcb.Count() / 93) * 100), 2);
            _lchitieu.Add(nd);
            #endregion
            #region Tổng số bệnh nhân đt nội trú
            nd = new NDChiTieu();
            nd.Stt = 7;
            nd.ChiTieu = "TS bệnh nhân điều trị nội trú";
            nd.DonVi = "BN";
            nd.ChiTieuNam = 1400;
            nd.THNam = qdt.Where(p =>  p.NoiTru == 1).Count();
            nd.TyLeNam = Math.Round((double)(((double)nd.THNam.GetValueOrDefault() / 1400) * 100), 2);
            if (tungayquy1 != null && denngayquy1 != null)
            {
                nd.THQuy1 = qdt.Where(p => p.NNhap >= tungayquy1 && p.NNhap <= denngayquy1 && p.NoiTru == 1).Count();
                nd.TyLeQuy1 = Math.Round((double)(((double)nd.THQuy1.GetValueOrDefault() / 1400) * 100), 2);
            }
            if (tungayquy2 != null && denngayquy2 != null)
            {
                nd.THQuy2 = qdt.Where(p => p.NNhap >= tungayquy2 && p.NNhap <= denngayquy2 && p.NoiTru == 1).Count();
                nd.TyLeQuy2 = Math.Round((double)(((double)nd.THQuy2.GetValueOrDefault() / 1400) * 100), 2);
            }
            if (tungayquy3 != null && denngayquy3 != null)
            {
                nd.THQuy3 = qdt.Where(p => p.NNhap >= tungayquy3 && p.NNhap <= denngayquy3 && p.NoiTru == 1).Count();
                nd.TyLeQuy3 = Math.Round((double)(((double)nd.THQuy3.GetValueOrDefault() / 1400) * 100), 2);
            }
            if (tungayquy4 != null && denngayquy4 != null)
            {
                nd.THQuy4 = qdt.Where(p => p.NNhap >= tungayquy4 && p.NNhap <= denngayquy4 && p.NoiTru == 1).Count();
                nd.TyLeQuy4 = Math.Round((double)(((double)nd.THQuy4.GetValueOrDefault() / 1400) * 100), 2);
            }
            _lchitieu.Add(nd);
            #endregion
            #region TE < 6
            nd = new NDChiTieu();
            nd.Stt = 8;
            nd.ChiTieu = "Trẻ em < 6";
            nd.DonVi = "BN";
            nd.ChiTieuNam = 140;
            nd.THNam = qdt.Where(p => p.DTuong == "BHYT"&& p.NoiTru == 1 && p.Tuoi < 6).Count();
            nd.TyLeNam = Math.Round((double)(((double)nd.THNam.GetValueOrDefault() / 140) * 100), 2);
            if (tungayquy1 != null && denngayquy1 != null)
            {
                nd.THQuy1 = qdt.Where(p => p.DTuong == "BHYT").Where(p => p.NNhap >= tungayquy1 && p.NNhap <= denngayquy1 && p.NoiTru == 1 && p.Tuoi < 6).Count();
                nd.TyLeQuy1 = Math.Round((double)(((double)nd.THQuy1.GetValueOrDefault() / 140) * 100), 2);
            }
            if (tungayquy2 != null && denngayquy2 != null)
            {
                nd.THQuy2 = qdt.Where(p => p.DTuong == "BHYT").Where(p => p.NNhap >= tungayquy2 && p.NNhap <= denngayquy2 && p.NoiTru == 1 && p.Tuoi < 6).Count();
                nd.TyLeQuy2 = Math.Round((double)(((double)nd.THQuy2.GetValueOrDefault() / 140) * 100), 2);
            }
            if (tungayquy3 != null && denngayquy3 != null)
            {
                nd.THQuy3 = qdt.Where(p => p.DTuong == "BHYT").Where(p => p.NNhap >= tungayquy3 && p.NNhap <= denngayquy3 && p.NoiTru == 1 && p.Tuoi < 6).Count();
                nd.TyLeQuy3 = Math.Round((double)(((double)nd.THQuy3.GetValueOrDefault() / 140) * 100), 2);
            }
            if (tungayquy4 != null && denngayquy4 != null)
            {
                nd.THQuy4 = qdt.Where(p => p.DTuong == "BHYT").Where(p => p.NNhap >= tungayquy4 && p.NNhap <= denngayquy4 && p.NoiTru == 1 && p.Tuoi < 6).Count();
                nd.TyLeQuy4 = Math.Round((double)(((double)nd.THQuy4.GetValueOrDefault() / 140) * 100), 2);
            }
            _lchitieu.Add(nd);
            #endregion
            #region Người nghèo
            nd = new NDChiTieu();
            nd.Stt = 9;
            nd.ChiTieu = "Người nghèo";
            nd.DonVi = "BN";
            nd.ChiTieuNam = 550;
            nd.THNam = qdt.Where(p =>  p.NoiTru == 1 && p.MaDTuong.ToLower().Contains("hn")).Count();
            nd.TyLeNam = Math.Round((double)(((double)nd.THNam.GetValueOrDefault() / 550) * 100), 2);
            if (tungayquy1 != null && denngayquy1 != null)
            {
                nd.THQuy1 = qdt.Where(p => p.NNhap >= tungayquy1 && p.NNhap <= denngayquy1 && p.NoiTru == 1 && p.MaDTuong.ToLower().Contains("hn")).Count();
                nd.TyLeQuy1 = Math.Round((double)(((double)nd.THQuy1.GetValueOrDefault() / 550) * 100), 2);
            }
            if (tungayquy2 != null && denngayquy2 != null)
            {
                nd.THQuy2 = qdt.Where(p => p.NNhap >= tungayquy2 && p.NNhap <= denngayquy2 && p.NoiTru == 1 && p.MaDTuong.ToLower().Contains("hn")).Count();
                nd.TyLeQuy2 = Math.Round((double)(((double)nd.THQuy2.GetValueOrDefault() / 550) * 100), 2);
            }
            if (tungayquy3 != null && denngayquy3 != null)
            {
                nd.THQuy3 = qdt.Where(p => p.NNhap >= tungayquy3 && p.NNhap <= denngayquy3 && p.NoiTru == 1 && p.MaDTuong.ToLower().Contains("hn")).Count();
                nd.TyLeQuy3 = Math.Round((double)(((double)nd.THQuy3.GetValueOrDefault() / 550) * 100), 2);
            }
            if (tungayquy4 != null && denngayquy4 != null)
            {
                nd.THQuy4 = qdt.Where(p => p.NNhap >= tungayquy4 && p.NNhap <= denngayquy4 && p.NoiTru == 1 && p.MaDTuong.ToLower().Contains("hn")).Count();
                nd.TyLeQuy4 = Math.Round((double)(((double)nd.THQuy4.GetValueOrDefault() / 550) * 100), 2);
            }
            _lchitieu.Add(nd);
            #endregion
            #region viện phí
            nd = new NDChiTieu();
            nd.Stt = 10;
            nd.ChiTieu = "Viện phí";
            nd.DonVi = "BN";
            //nd.ChiTieuNam = 550;
            nd.THNam = qdt.Where(p =>p.NoiTru == 1 && p.DTuong.ToLower().Contains("dịch vụ")).Count();
            //nd.TyLeNam = Math.Round((double)((nd.THNam.GetValueOrDefault() / 550) * 100), 2);
            if (tungayquy1 != null && denngayquy1 != null)
            {
                nd.THQuy1 = qdt.Where(p => p.NNhap >= tungayquy1 && p.NNhap <= denngayquy1 && p.NoiTru == 1 && p.DTuong.ToLower().Contains("dịch vụ")).Count();
                //nd.TyLeQuy1 = Math.Round((double)((nd.THQuy1.GetValueOrDefault() / 550) * 100), 2);
            }
            if (tungayquy2 != null && denngayquy2 != null)
            {
                nd.THQuy2 = qdt.Where(p => p.NNhap >= tungayquy2 && p.NNhap <= denngayquy2 && p.NoiTru == 1 && p.DTuong.ToLower().Contains("dịch vụ")).Count();
                //nd.TyLeQuy2 = Math.Round((double)((nd.THQuy2.GetValueOrDefault() / 550) * 100), 2);
            }
            if (tungayquy3 != null && denngayquy3 != null)
            {
                nd.THQuy3 = qdt.Where(p => p.NNhap >= tungayquy3 && p.NNhap <= denngayquy3 && p.NoiTru == 1 && p.DTuong.ToLower().Contains("dịch vụ")).Count();
                //nd.TyLeQuy3 = Math.Round((double)((nd.THQuy3.GetValueOrDefault() / 550) * 100), 2);
            }
            if (tungayquy4 != null && denngayquy4 != null)
            {
                nd.THQuy4 = qdt.Where(p => p.NNhap >= tungayquy4 && p.NNhap <= denngayquy4 && p.NoiTru == 1 && p.DTuong.ToLower().Contains("dịch vụ")).Count();
                //nd.TyLeQuy4 = Math.Round((double)((nd.THQuy4.GetValueOrDefault() / 550) * 100), 2);
            }
            _lchitieu.Add(nd);
            #endregion
            #region BH khác
            nd = new NDChiTieu();
            nd.Stt = 11;
            nd.ChiTieu = "Bảo hiểm khác";
            nd.DonVi = "BN";
            //nd.ChiTieuNam = 550;
            nd.THNam = qdt.Where(p=>p.DTuong == "BHYT").Where(p =>p.NoiTru == 1 &&  !p.MaDTuong.ToLower().Contains("hn") && p.Tuoi >= 6).Count();
            //nd.TyLeNam = Math.Round((double)((nd.THNam.GetValueOrDefault() / 550) * 100), 2);
            if (tungayquy1 != null && denngayquy1 != null)
            {
                nd.THQuy1 = qdt.Where(p => p.DTuong == "BHYT").Where(p => p.NNhap >= tungayquy1 && p.NNhap <= denngayquy1 && p.NoiTru == 1 && !p.MaDTuong.ToLower().Contains("hn") && p.Tuoi >= 6).Count();
                //nd.TyLeQuy1 = Math.Round((double)((nd.THQuy1.GetValueOrDefault() / 550) * 100), 2);
            }
            if (tungayquy2 != null && denngayquy2 != null)
            {
                nd.THQuy2 = qdt.Where(p => p.DTuong == "BHYT").Where(p => p.NNhap >= tungayquy2 && p.NNhap <= denngayquy2 && p.NoiTru == 1 && !p.MaDTuong.ToLower().Contains("hn") && p.Tuoi >= 6).Count();
                //nd.TyLeQuy2 = Math.Round((double)((nd.THQuy2.GetValueOrDefault() / 550) * 100), 2);
            }
            if (tungayquy3 != null && denngayquy3 != null)
            {
                nd.THQuy3 = qdt.Where(p => p.DTuong == "BHYT").Where(p => p.NNhap >= tungayquy3 && p.NNhap <= denngayquy3 && p.NoiTru == 1 && !p.MaDTuong.ToLower().Contains("hn") && p.Tuoi >= 6).Count();
                //nd.TyLeQuy3 = Math.Round((double)((nd.THQuy3.GetValueOrDefault() / 550) * 100), 2);
            }
            if (tungayquy4 != null && denngayquy4 != null)
            {
                nd.THQuy4 = qdt.Where(p => p.DTuong == "BHYT").Where(p => p.NNhap >= tungayquy4 && p.NNhap <= denngayquy4 && p.NoiTru == 1 && !p.MaDTuong.ToLower().Contains("hn") && p.Tuoi >= 6).Count();
                //nd.TyLeQuy4 = Math.Round((double)((nd.THQuy4.GetValueOrDefault() / 550) * 100), 2);
            }
            _lchitieu.Add(nd);
            #endregion
            #region Tổng số bệnh nhân đt ngoại trú
            nd = new NDChiTieu();
            nd.Stt = 12;
            nd.ChiTieu = "TS bệnh nhân điều trị ngoại trú + kê đơn";
            nd.DonVi = "BN";
            nd.ChiTieuNam = 1500;
            nd.THNam = qkb.Where(p => p.NoiTru == 0).Count();
            nd.TyLeNam = Math.Round((double)(((double)nd.THNam.GetValueOrDefault() / 1500) * 100), 2);
            if (tungayquy1 != null && denngayquy1 != null)
            {
                nd.THQuy1 = qkb.Where(p => p.NNhap >= tungayquy1 && p.NNhap <= denngayquy1 && p.NoiTru == 0).Count();
                nd.TyLeQuy1 = Math.Round((double)(((double)nd.THQuy1.GetValueOrDefault() / 1500) * 100), 2);
            }
            if (tungayquy2 != null && denngayquy2 != null)
            {
                nd.THQuy2 = qkb.Where(p => p.NNhap >= tungayquy2 && p.NNhap <= denngayquy2 && p.NoiTru == 0).Count();
                nd.TyLeQuy2 = Math.Round((double)(((double)nd.THQuy2.GetValueOrDefault() / 1500) * 100), 2);
            }
            if (tungayquy3 != null && denngayquy3 != null)
            {
                nd.THQuy3 = qkb.Where(p => p.NNhap >= tungayquy3 && p.NNhap <= denngayquy3 && p.NoiTru == 0).Count();
                nd.TyLeQuy3 = Math.Round((double)(((double)nd.THQuy3.GetValueOrDefault() / 1500) * 100), 2);
            }
            if (tungayquy4 != null && denngayquy4 != null)
            {
                nd.THQuy4 = qkb.Where(p => p.NNhap >= tungayquy4 && p.NNhap <= denngayquy4 && p.NoiTru == 0).Count();
                nd.TyLeQuy4 = Math.Round((double)(((double)nd.THQuy4.GetValueOrDefault() / 1500) * 100), 2);
            }
            _lchitieu.Add(nd);
            #endregion
            #region Người nghèo
            nd = new NDChiTieu();
            nd.Stt = 13;
            nd.ChiTieu = "Người nghèo";
            nd.DonVi = "BN";
            nd.ChiTieuNam = 400;
            nd.THNam = qkb.Where(p => p.NoiTru == 0 && p.MaDTuong.ToLower().Contains("hn")).Count(); 
            nd.TyLeNam = Math.Round((double)(((double)nd.THNam.GetValueOrDefault() / 400) * 100), 2);
            if (tungayquy1 != null && denngayquy1 != null)
            {
                nd.THQuy1 = qkb.Where(p => p.NNhap >= tungayquy1 && p.NNhap <= denngayquy1 && p.NoiTru == 0 && p.MaDTuong.ToLower().Contains("hn")).Count(); 
                nd.TyLeQuy1 = Math.Round((double)(((double)nd.THQuy1.GetValueOrDefault() / 400) * 100), 2);
            }
            if (tungayquy2 != null && denngayquy2 != null)
            {
                nd.THQuy2 = qkb.Where(p => p.NNhap >= tungayquy2 && p.NNhap <= denngayquy2 && p.NoiTru == 0 && p.MaDTuong.ToLower().Contains("hn")).Count(); 
                nd.TyLeQuy2 = Math.Round((double)(((double)nd.THQuy2.GetValueOrDefault() / 400) * 100), 2);
            }
            if (tungayquy3 != null && denngayquy3 != null)
            {
                nd.THQuy3 = qkb.Where(p => p.NNhap >= tungayquy3 && p.NNhap <= denngayquy3 && p.NoiTru == 0 && p.MaDTuong.ToLower().Contains("hn")).Count(); 
                nd.TyLeQuy3 = Math.Round((double)(((double)nd.THQuy3.GetValueOrDefault() / 400) * 100), 2);
            }
            if (tungayquy4 != null && denngayquy4 != null)
            {
                nd.THQuy4 = qkb.Where(p => p.NNhap >= tungayquy4 && p.NNhap <= denngayquy4 && p.NoiTru == 0 && p.MaDTuong.ToLower().Contains("hn")).Count(); 
                nd.TyLeQuy4 = Math.Round((double)(((double)nd.THQuy4.GetValueOrDefault() / 400) * 100), 2);
            }
            _lchitieu.Add(nd);
            #endregion
            #region TE < 6
            nd = new NDChiTieu();
            nd.Stt = 14;
            nd.ChiTieu = "Trẻ em < 6";
            nd.DonVi = "BN";
            nd.ChiTieuNam = 1100;
            nd.THNam = qkb.Where(p =>p.NoiTru == 0 && p.Tuoi < 6).Count(); 
            nd.TyLeNam = Math.Round((double)(((double)nd.THNam.GetValueOrDefault() / 1100) * 100), 2);
            if (tungayquy1 != null && denngayquy1 != null)
            {
                nd.THQuy1 = qkb.Where(p => p.NNhap >= tungayquy1 && p.NNhap <= denngayquy1 && p.NoiTru == 0 && p.Tuoi < 6).Count(); 
                nd.TyLeQuy1 = Math.Round((double)(((double)nd.THQuy1.GetValueOrDefault() / 1100) * 100), 2);
            }
            if (tungayquy2 != null && denngayquy2 != null)
            {
                nd.THQuy2 = qkb.Where(p => p.NNhap >= tungayquy2 && p.NNhap <= denngayquy2 && p.NoiTru == 0 && p.Tuoi < 6).Count(); 
                nd.TyLeQuy2 = Math.Round((double)(((double)nd.THQuy2.GetValueOrDefault() / 1100) * 100), 2);
            }
            if (tungayquy3 != null && denngayquy3 != null)
            {
                nd.THQuy3 = qkb.Where(p => p.NNhap >= tungayquy3 && p.NNhap <= denngayquy3 && p.NoiTru == 0 && p.Tuoi < 6).Count(); 
                nd.TyLeQuy3 = Math.Round((double)(((double)nd.THQuy3.GetValueOrDefault() / 1100) * 100), 2);
            }
            if (tungayquy4 != null && denngayquy4 != null)
            {
                nd.THQuy4 = qkb.Where(p => p.NNhap >= tungayquy4 && p.NNhap <= denngayquy4 && p.NoiTru == 0 && p.Tuoi < 6).Count(); 
                nd.TyLeQuy4 = Math.Round((double)(((double)nd.THQuy4.GetValueOrDefault() / 1100) * 100), 2);
            }
            _lchitieu.Add(nd);
            #endregion

            BaoCao.rep_BC_THChiTieuCM_12122 rep = new BaoCao.rep_BC_THChiTieuCM_12122();
            frmIn frm = new frmIn();
            rep.lblTuNgay.Text = "Từ ngày " + tungay.ToString("dd/MM/yyyy") + " đến ngày " + denngay.ToString("dd/MM/yyyy");
            rep.lblNgayThang.Text = strtinh.Trim() + ",ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;
            rep.DataSource = _lchitieu;
            rep.BindingData();
            rep.CreateDocument();
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();
        }

        #region số giường bệnh
        public int SoGiuong(string str)
        {
            int _sogiuong = 0;
            if (!string.IsNullOrEmpty(str))
            {
                List<string> buong = new List<string>();
                List<string> giuong = new List<string>();
                List<string> tam = new List<string>();
                buong = str.Split(';').ToList();
                foreach (var _buong in buong)
                {
                    tam = _buong.Split(',').ToList();
                    foreach (var _tam in tam)
                    {
                        giuong.Add(_tam);
                    }
                }
                _sogiuong = giuong.Count;
            }
            return _sogiuong;
        }
        #endregion
        #region class NDChiTieu
        private class NDChiTieu
        {
            public int Stt { get; set; }
            public string ChiTieu { get; set; }
            public string DonVi { get; set; }
            public double? ChiTieuNam { get; set; }
            public double? THNam { get; set; }
            public double? TyLeNam { get; set; }
            public double? THQuy1 { get; set; }
            public double? TyLeQuy1 { get; set; }
            public double? THQuy2 { get; set; }
            public double? TyLeQuy2 { get; set; }
            public double? THQuy3 { get; set; }
            public double? TyLeQuy3 { get; set; }
            public double? THQuy4 { get; set; }
            public double? TyLeQuy4 { get; set; }
        }
        #endregion
        #region class Ngay
        private class Ngay
        {
            public int Quy { get; set; }
            public DateTime Day { get; set; }
        }
        #endregion
        #region function CheckDate
        private List<Ngay> dsNgay(DateTime tungay, DateTime denngay)
        {
            List<Ngay> lngay = new List<Ngay>();
            for (DateTime i = tungay; i <= denngay; i = i.AddDays(1.0))
            {
                Ngay d = new Ngay();
                int m = i.Month;
                if (m >= 1 && m <= 3)
                {
                    d.Quy = 1;
                }
                if (m >= 4 && m <= 6)
                {
                    d.Quy = 2;
                }
                if (m >= 7 && m <= 9)
                {
                    d.Quy = 3;
                }
                if (m >= 9 && m <= 12)
                {
                    d.Quy = 4;
                }
                d.Day = i;
                lngay.Add(d);
            }
            return lngay;
        }
        #endregion
    }
}