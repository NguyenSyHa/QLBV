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
    public partial class frm_BC_ChiSoCongTacKCB_12122 : DevExpress.XtraEditors.XtraForm
    {
        public frm_BC_ChiSoCongTacKCB_12122()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {

            DateTime tungay = DungChung.Ham.NgayTu(lupTuNgay.DateTime);
            DateTime denngay = DungChung.Ham.NgayDen(lupDenNgay.DateTime);
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

            List<KPhong> _lLamSang = data.KPhongs.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.LamSang).Where(p=>p.Status != 0).OrderBy(p => p.Status).ToList();
            BaoCao.rep_BC_ChiSoCongTacKCB_12122 rep = new BaoCao.rep_BC_ChiSoCongTacKCB_12122();
            frmIn frm = new frmIn();
            int makp1 = 0; int makp2 = 0; int makp3 = 0; int makp4 = 0; int makp5 = 0;
            if (_lLamSang.Count > 0)
            {
                makp1 = _lLamSang.First().MaKP;
                rep.celTit1.Text = _lLamSang.First().TenKP;
            }
            if (_lLamSang.Count > 1)
            {
                makp2 = _lLamSang.Skip(1).First().MaKP;
                rep.celTit2.Text = _lLamSang.Skip(1).First().TenKP;
            }
            if (_lLamSang.Count > 2)
            {
                makp3 = _lLamSang.Skip(2).First().MaKP;
                rep.celTit3.Text = _lLamSang.Skip(2).First().TenKP;
            }
            if (_lLamSang.Count > 3)
            {
                makp4 = _lLamSang.Skip(3).First().MaKP;
                rep.celTit4.Text = _lLamSang.Skip(3).First().TenKP;
            }
            if (_lLamSang.Count > 4)
            {
                makp5 = _lLamSang.Skip(4).First().MaKP;
                rep.celTit5.Text = _lLamSang.Skip(4).First().TenKP;
            }
            List<BC> _lBC = new List<BC>();
           
            #region mẫu 1
            if (rdMau.SelectedIndex == 0)
            {
                #region Tổng số bệnh nhân đến viện 
                var qbnkb = (from bn in data.BenhNhans.Where(p => p.NNhap <= denngay).Where(p=>p.MaKCB == DungChung.Bien.MaBV)
                         join kb in data.BNKBs on bn.MaBNhan equals kb.MaBNhan
                         join rv in data.RaViens on bn.MaBNhan equals rv.MaBNhan into kq
                         from kq1 in kq.DefaultIfEmpty()
                          where kq1 == null ||                 
                              (kq1 != null && (bn.NNhap >= tungay ||( bn.NNhap < tungay && kq1.NgayRa >= tungay) ))
                         select new
                         {
                             bn.MaBNhan,
                             bn.NoiTru,
                             bn.Tuoi,
                             bn.GTinh,
                             bn.DTNT,
                             bn.SThe,
                             bn.MaDTuong, 
                             kb.MaKP,                              
                             kb.NgayKham,
                             kb.IDKB,
                             kb.MaKPdt,
                             bn.NNhap,
                            // MaKPrv =  ( kq1 == null || (kq1 != null && kq1.NgayRa >denngay)) ? 0 : kq1.MaKP, // chưa ra viện hoặc ra viện sau denngay
                             SoNgaydt = ( kq1 == null || (kq1 != null && kq1.NgayRa >denngay)) ? 0:  kq1.SoNgaydt,
                             bnRaVien = ( kq1 == null || (kq1 != null && kq1.NgayRa >denngay)) ? 0 : 1
                         }).ToList();

                // lấy ID khám bệnh lớn nhất của bệnh nhân trước denngay
                var qIDKB = (from bn in qbnkb
                          group bn by new
                          {
                              bn.MaBNhan,                             
                          } into kq
                          select new
                              {
                                  kq.Key.MaBNhan,
                                  IDKB = kq.Max(p => p.IDKB),
                              }).ToList();

                List<KPhong> lkp = data.KPhongs.ToList();

                var qall1 = (from bn in qbnkb
                             join kb in qIDKB on bn.IDKB equals kb.IDKB
                             join kp in lkp on bn.MaKP equals kp.MaKP
                             select new {

                                 bn.MaBNhan,
                                 bn.NoiTru,
                                 bn.Tuoi,
                                 bn.GTinh,
                                 bn.DTNT,
                                 bn.SThe,
                                 bn.MaDTuong,
                                 bn.MaKP,
                                 bn.NgayKham,
                                 bn.IDKB,  
                                 bn.NNhap,
                                 bn.MaKPdt,
                                 kp.PLoai,
                                 bn.bnRaVien,
                                 SoNgaydt = bn.bnRaVien == 1 ? bn.SoNgaydt : (denngay - bn.NNhap.Value).Days,
                                  
                             }).ToList();

                var qkp = (from a in qall1
                           select new
                           {
                               a.MaBNhan,
                               a.NoiTru,
                               a.Tuoi,
                               a.GTinh,
                               a.DTNT,
                               a.SThe,
                               a.MaDTuong,
                               a.SoNgaydt, 
                               a.NNhap,
                               a.bnRaVien,
                               PKham = a.PLoai == DungChung.Bien.st_PhanLoaiKP.PhongKham ? 1 : 0,
                               KP1 = a.MaKP == makp1 ? 1 : 0,
                               KP2 = a.MaKP == makp2 ? 1 : 0,
                               KP3 = a.MaKP == makp3 ? 1 : 0,
                               KP4 = a.MaKP == makp4 ? 1 : 0,
                               KP5 = a.MaKP == makp5 ? 1 : 0
                           }).ToList();

              

#endregion
                #region Khám bệnh
                var q1 = qkp.Where(p=>p.NNhap >= tungay).ToList();

                BC moi = new BC();
                moi.stt = 1;
                moi.sttHT = "1";
                moi.noidung = "Tổng số lượt khám chữa bệnh";
                moi.donvi = "Lượt";      
                moi.Phongkham = q1.Where(p => p.PKham == 1).Count();
                moi.Khoa1 = q1.Where(p => p.KP1 == 1).Count();
                moi.Khoa2 = q1.Where(p => p.KP2 == 1).Count();
                moi.Khoa3 = q1.Where(p => p.KP3 == 1).Count();
                moi.Khoa4 = q1.Where(p => p.KP4 == 1).Count();
                moi.Khoa5 = q1.Where(p => p.KP5 == 1).Count();
                _lBC.Add(moi);

                moi = new BC();
                moi.stt = 2;
                moi.sttHT = "";
                moi.noidung = "Bệnh nhân nữ";
                moi.donvi = "Lượt";
                moi.Phongkham = q1.Where(p => p.GTinh == 0).Where(p => p.PKham == 1).Count();
                moi.Khoa1 = q1.Where(p => p.GTinh == 0).Where(p => p.KP1 == 1).Count();
                moi.Khoa2 = q1.Where(p => p.GTinh == 0).Where(p => p.KP2 == 1).Count();
                moi.Khoa3 = q1.Where(p => p.GTinh == 0).Where(p => p.KP3 == 1).Count();
                moi.Khoa4 = q1.Where(p => p.GTinh == 0).Where(p => p.KP4 == 1).Count();
                moi.Khoa5 = q1.Where(p => p.GTinh == 0).Where(p => p.KP5 == 1).Count();
                _lBC.Add(moi);

                moi = new BC();
                moi.stt = 3;
                moi.sttHT = "";
                moi.noidung = "Trẻ em < 6 tuổi";
                moi.donvi = "Lượt";
                moi.Phongkham = q1.Where(p => p.Tuoi == null || p.Tuoi < 6).Where(p => p.PKham == 1).Count();
                moi.Khoa1 = q1.Where(p => p.Tuoi == null || p.Tuoi < 6).Where(p => p.KP1 == 1).Count();
                moi.Khoa2 = q1.Where(p => p.Tuoi == null || p.Tuoi < 6).Where(p => p.KP2 == 1).Count();
                moi.Khoa3 = q1.Where(p => p.Tuoi == null || p.Tuoi < 6).Where(p => p.KP3 == 1).Count();
                moi.Khoa4 = q1.Where(p => p.Tuoi == null || p.Tuoi < 6).Where(p => p.KP4 == 1).Count();
                moi.Khoa5 = q1.Where(p => p.Tuoi == null || p.Tuoi < 6).Where(p => p.KP5 == 1).Count();
                _lBC.Add(moi);

                moi = new BC();
                moi.stt = 4;
                moi.sttHT = "";
                moi.noidung = "Trẻ em < 15 tuổi";
                moi.donvi = "Lượt";
                moi.Phongkham = q1.Where(p => p.Tuoi == null || p.Tuoi < 15).Where(p => p.PKham == 1).Count();
                moi.Khoa1 = q1.Where(p => p.Tuoi == null || p.Tuoi < 15).Where(p => p.KP1 == 1).Count();
                moi.Khoa2 = q1.Where(p => p.Tuoi == null || p.Tuoi < 15).Where(p => p.KP2 == 1).Count();
                moi.Khoa3 = q1.Where(p => p.Tuoi == null || p.Tuoi < 15).Where(p => p.KP3 == 1).Count();
                moi.Khoa4 = q1.Where(p => p.Tuoi == null || p.Tuoi < 15).Where(p => p.KP4 == 1).Count();
                moi.Khoa5 = q1.Where(p => p.Tuoi == null || p.Tuoi < 15).Where(p => p.KP5 == 1).Count();
                _lBC.Add(moi);

                moi = new BC();
                moi.stt = 5;
                moi.sttHT = "";
                moi.noidung = "Hộ nghèo";
                moi.donvi = "Lượt";
                moi.Phongkham = q1.Where(p => p.MaDTuong == "HN").Where(p => p.PKham == 1).Count();
                moi.Khoa1 = q1.Where(p => p.MaDTuong == "HN").Where(p => p.KP1 == 1).Count();
                moi.Khoa2 = q1.Where(p => p.MaDTuong == "HN").Where(p => p.KP2 == 1).Count();
                moi.Khoa3 = q1.Where(p => p.MaDTuong == "HN").Where(p => p.KP3 == 1).Count();
                moi.Khoa4 = q1.Where(p => p.MaDTuong == "HN").Where(p => p.KP4 == 1).Count();
                moi.Khoa5 = q1.Where(p => p.MaDTuong == "HN").Where(p => p.KP5 == 1).Count();
                _lBC.Add(moi);


                var qbhKhac = q1.Where(p => p.SThe != null && p.SThe != "" && p.Tuoi >= 6).Where(p => p.MaDTuong != "HN").ToList();
                moi = new BC();
                moi.stt = 6;
                moi.sttHT = "";
                moi.noidung = "Bảo hiểm khác";
                moi.donvi = "Lượt";
                moi.Phongkham = qbhKhac.Where(p => p.PKham == 1).Count();
                moi.Khoa1 = qbhKhac.Where(p => p.KP1 == 1).Count();
                moi.Khoa2 = qbhKhac.Where(p => p.KP2 == 1).Count();
                moi.Khoa3 = qbhKhac.Where(p => p.KP3 == 1).Count();
                moi.Khoa4 = qbhKhac.Where(p => p.KP4 == 1).Count();
                moi.Khoa5 = qbhKhac.Where(p => p.KP5 == 1).Count();
                _lBC.Add(moi);

                moi = new BC();
                moi.stt = 7;
                moi.sttHT = "";
                moi.noidung = "Viện phí";
                moi.donvi = "Lượt";
                moi.Phongkham = q1.Where(p => p.SThe == null || p.SThe == "").Where(p => p.PKham == 1).Count();
                moi.Khoa1 = q1.Where(p => p.SThe == null || p.SThe == "").Where(p => p.KP1 == 1).Count();
                moi.Khoa2 = q1.Where(p => p.SThe == null || p.SThe == "").Where(p => p.KP2 == 1).Count();
                moi.Khoa3 = q1.Where(p => p.SThe == null || p.SThe == "").Where(p => p.KP3 == 1).Count();
                moi.Khoa4 = q1.Where(p => p.SThe == null || p.SThe == "").Where(p => p.KP4 == 1).Count();
                moi.Khoa5 = q1.Where(p => p.SThe == null || p.SThe == "").Where(p => p.KP5 == 1).Count();
                _lBC.Add(moi);



                #endregion

                #region điều trị nội trú
               // var q2 = qkp.Where(p => p.NoiTru == 1).ToList();
                var q0 = (from bn in qIDKB
                          join bnhan in data.BenhNhans on bn.MaBNhan equals bnhan.MaBNhan 
                          join bnkb in qbnkb on bn.IDKB equals bnkb.IDKB
                          join vv in data.VaoViens on bn.MaBNhan equals vv.MaBNhan
                          join kp in data.KPhongs on bnkb.MaKP equals kp.MaKP select new {kp.PLoai, kp.MaKP, bn.MaBNhan,bnhan.GTinh,bnhan.NoiTru,bnhan.DTNT, bnhan.Tuoi, bnhan.MaDTuong, bnhan.SThe }).ToList();
                var q2 = q0.Where(p => p.NoiTru == 1).ToList();
                moi = new BC();
                moi.stt = 8;
                moi.sttHT = "2";
                moi.noidung = "Tổng số bệnh nhân điều trị nội trú";
                moi.donvi = "Bệnh nhân";
                moi.Phongkham = q2.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.PhongKham).Count();
                moi.Khoa1 = q2.Where(p => p.MaKP == makp1).Count();
                moi.Khoa2 = q2.Where(p => p.MaKP == makp2).Count();
                moi.Khoa3 = q2.Where(p => p.MaKP == makp3).Count();
                moi.Khoa4 = q2.Where(p => p.MaKP == makp4).Count();
                moi.Khoa5 = q2.Where(p => p.MaKP == makp5).Count();
                _lBC.Add(moi);

                moi = new BC();
                moi.stt = 9;
                moi.sttHT = "";
                moi.noidung = "Bệnh nhân nữ";
                moi.donvi = "Bệnh nhân";
                moi.Phongkham = q2.Where(p => p.GTinh == 0).Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.PhongKham).Count();
                moi.Khoa1 = q2.Where(p => p.GTinh == 0).Where(p => p.MaKP == makp1).Count();
                moi.Khoa2 = q2.Where(p => p.GTinh == 0).Where(p => p.MaKP == makp2).Count();
                moi.Khoa3 = q2.Where(p => p.GTinh == 0).Where(p => p.MaKP == makp3).Count();
                moi.Khoa4 = q2.Where(p => p.GTinh == 0).Where(p => p.MaKP == makp4).Count();
                moi.Khoa5 = q2.Where(p => p.GTinh == 0).Where(p => p.MaKP == makp5).Count();
                _lBC.Add(moi);

                moi = new BC();
                moi.stt = 10;
                moi.sttHT = "";
                moi.noidung = "Trẻ em < 6 tuổi";
                moi.donvi = "Bệnh nhân";
                moi.Phongkham = q2.Where(p => p.Tuoi == null || p.Tuoi < 6).Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.PhongKham).Count();
                moi.Khoa1 = q2.Where(p => p.Tuoi == null || p.Tuoi < 6).Where(p => p.MaKP == makp1).Count();
                moi.Khoa2 = q2.Where(p => p.Tuoi == null || p.Tuoi < 6).Where(p => p.MaKP == makp2).Count();
                moi.Khoa3 = q2.Where(p => p.Tuoi == null || p.Tuoi < 6).Where(p => p.MaKP == makp3).Count();
                moi.Khoa4 = q2.Where(p => p.Tuoi == null || p.Tuoi < 6).Where(p => p.MaKP == makp4).Count();
                moi.Khoa5 = q2.Where(p => p.Tuoi == null || p.Tuoi < 6).Where(p => p.MaKP == makp5).Count();
                _lBC.Add(moi);

                moi = new BC();
                moi.stt = 11;
                moi.sttHT = "";
                moi.noidung = "Trẻ em < 15 tuổi";
                moi.donvi = "Bệnh nhân";
                moi.Phongkham = q2.Where(p => p.Tuoi == null || p.Tuoi < 15).Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.PhongKham).Count();
                moi.Khoa1 = q2.Where(p => p.Tuoi == null || p.Tuoi < 15).Where(p => p.MaKP == makp1).Count();
                moi.Khoa2 = q2.Where(p => p.Tuoi == null || p.Tuoi < 15).Where(p => p.MaKP == makp2).Count();
                moi.Khoa3 = q2.Where(p => p.Tuoi == null || p.Tuoi < 15).Where(p => p.MaKP == makp3).Count();
                moi.Khoa4 = q2.Where(p => p.Tuoi == null || p.Tuoi < 15).Where(p => p.MaKP == makp4).Count();
                moi.Khoa5 = q2.Where(p => p.Tuoi == null || p.Tuoi < 15).Where(p => p.MaKP == makp5).Count();
                _lBC.Add(moi);

                moi = new BC();
                moi.stt = 12;
                moi.sttHT = "";
                moi.noidung = "Hộ nghèo";
                moi.donvi = "Bệnh nhân";
                moi.Phongkham = q2.Where(p => p.MaDTuong == "HN").Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.PhongKham).Count();
                moi.Khoa1 = q2.Where(p => p.MaDTuong == "HN").Where(p => p.MaKP == makp1).Count();
                moi.Khoa2 = q2.Where(p => p.MaDTuong == "HN").Where(p => p.MaKP == makp2).Count();
                moi.Khoa3 = q2.Where(p => p.MaDTuong == "HN").Where(p => p.MaKP == makp3).Count();
                moi.Khoa4 = q2.Where(p => p.MaDTuong == "HN").Where(p => p.MaKP == makp4).Count();
                moi.Khoa5 = q2.Where(p => p.MaDTuong == "HN").Where(p => p.MaKP == makp5).Count();
                _lBC.Add(moi);


                var qbhKhac2 = q2.Where(p => p.SThe != null && p.SThe != "" && p.Tuoi >= 6).Where(p => p.MaDTuong != "HN").ToList();
                moi = new BC();
                moi.stt = 13;
                moi.sttHT = "";
                moi.noidung = "Bảo hiểm khác";
                moi.donvi = "Bệnh nhân";
                moi.Phongkham = qbhKhac2.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.PhongKham).Count();
                moi.Khoa1 = qbhKhac2.Where(p => p.MaKP == makp1).Count();
                moi.Khoa2 = qbhKhac2.Where(p => p.MaKP == makp2).Count();
                moi.Khoa3 = qbhKhac2.Where(p => p.MaKP == makp3).Count();
                moi.Khoa4 = qbhKhac2.Where(p => p.MaKP == makp4).Count();
                moi.Khoa5 = qbhKhac2.Where(p => p.MaKP == makp5).Count();
                _lBC.Add(moi);

                var qvp = q2.Where(p => p.SThe == null || p.SThe == "").ToList();
                moi = new BC();
                moi.stt = 14;
                moi.sttHT = "";
                moi.noidung = "Viện phí";
                moi.donvi = "Bệnh nhân";
                moi.Phongkham = qvp.Where(p => p.SThe == null || p.SThe == "").Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.PhongKham).Count();
                moi.Khoa1 = qvp.Where(p => p.SThe == null || p.SThe == "").Where(p => p.MaKP == makp1).Count();
                moi.Khoa2 = qvp.Where(p => p.SThe == null || p.SThe == "").Where(p => p.MaKP == makp2).Count();
                moi.Khoa3 = qvp.Where(p => p.SThe == null || p.SThe == "").Where(p => p.MaKP == makp3).Count();
                moi.Khoa4 = qvp.Where(p => p.SThe == null || p.SThe == "").Where(p => p.MaKP == makp4).Count();
                moi.Khoa5 = qvp.Where(p => p.SThe == null || p.SThe == "").Where(p => p.MaKP == makp5).Count();
                _lBC.Add(moi);



                #endregion

                #region điều trị ngoại trú
                var q3 = q0.Where(p => p.NoiTru == 0 && p.DTNT == true).ToList();

                moi = new BC();
                moi.stt = 15;
                moi.sttHT = "3";
                moi.noidung = "Tổng số bệnh nhân điều trị ngoại trú";
                moi.donvi = "Bệnh nhân";
                moi.Phongkham = q3.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.PhongKham).Count();
                moi.Khoa1 = q3.Where(p => p.MaKP == makp1).Count();
                moi.Khoa2 = q3.Where(p => p.MaKP == makp2).Count();
                moi.Khoa3 = q3.Where(p => p.MaKP == makp3).Count();
                moi.Khoa4 = q3.Where(p => p.MaKP == makp4).Count();
                moi.Khoa5 = q3.Where(p => p.MaKP == makp5).Count();
                _lBC.Add(moi);

                #endregion

                #region tổng số bệnh nhân kê đơn
                var qdthuoc = (from bn in data.BenhNhans.Where(p=>p.NoiTru== 0 && p.DTNT ==false).Where(p => p.NNhap >= tungay && p.NNhap <= denngay)
                               join dt in data.DThuocs.Where(p => p.PLDV == 1) on bn.MaBNhan equals dt.MaBNhan
                               join kp in data.KPhongs on dt.MaKP equals kp.MaKP
                               select new
                               {
                                   bn.NoiTru,
                                   bn.Tuoi,
                                   bn.GTinh,
                                   bn.DTNT,
                                   bn.SThe,
                                   bn.MaDTuong,
                                   kp.PLoai,
                                   dt.MaKP,
                                   kp.TenKP,
                               }).ToList();
                var qdthuoc2 = (from a in qdthuoc
                                select new
                                {
                                    a.NoiTru,
                                    a.Tuoi,
                                    a.GTinh,
                                    a.DTNT,
                                    a.SThe,
                                    a.MaDTuong,
                                    PKham = a.PLoai == DungChung.Bien.st_PhanLoaiKP.PhongKham ? 1 : 0,
                                    KP1 = a.MaKP == makp1 ? 1 : 0,
                                    KP2 = a.MaKP == makp2 ? 1 : 0,
                                    KP3 = a.MaKP == makp3 ? 1 : 0,
                                    KP4 = a.MaKP == makp4 ? 1 : 0,
                                    KP5 = a.MaKP == makp5 ? 1 : 0
                                }).ToList();
                var qdthuoc3 = (from a in qdthuoc2
                                group a by new { a.NoiTru, a.Tuoi, a.GTinh, a.DTNT, a.SThe, a.MaDTuong } into
                                    kq
                                select new
                                {
                                    kq.Key.NoiTru,
                                    kq.Key.Tuoi,
                                    kq.Key.GTinh,
                                    kq.Key.DTNT,
                                    kq.Key.SThe,
                                    kq.Key.MaDTuong,
                                    PKham = kq.Max(p => p.PKham),
                                    KP1 = kq.Max(p => p.KP1),
                                    KP2 = kq.Max(p => p.KP2),
                                    KP3 = kq.Max(p => p.KP3),
                                    KP4 = kq.Max(p => p.KP4),
                                    KP5 = kq.Max(p => p.KP5),

                                }).ToList();



                moi = new BC();
                moi.stt = 16;
                moi.sttHT = "4";
                moi.noidung = "Tổng số bệnh nhân kê đơn";
                moi.donvi = "Lượt";
                moi.Phongkham = qdthuoc3.Where(p => p.PKham == 1).Count();
                moi.Khoa1 = qdthuoc3.Where(p => p.KP1 == 1).Count();
                moi.Khoa2 = qdthuoc3.Where(p => p.KP2 == 1).Count();
                moi.Khoa3 = qdthuoc3.Where(p => p.KP3 == 1).Count();
                moi.Khoa4 = qdthuoc3.Where(p => p.KP4 == 1).Count();
                moi.Khoa5 = qdthuoc3.Where(p => p.KP5 == 1).Count();
                _lBC.Add(moi);

                moi = new BC();
                moi.stt = 17;
                moi.sttHT = "";
                moi.noidung = "Bệnh nhân nữ";
                moi.donvi = "Lượt";
                moi.Phongkham = qdthuoc3.Where(p => p.GTinh == 0).Where(p => p.PKham == 1).Count();
                moi.Khoa1 = qdthuoc3.Where(p => p.GTinh == 0).Where(p => p.KP1 == 1).Count();
                moi.Khoa2 = qdthuoc3.Where(p => p.GTinh == 0).Where(p => p.KP2 == 1).Count();
                moi.Khoa3 = qdthuoc3.Where(p => p.GTinh == 0).Where(p => p.KP3 == 1).Count();
                moi.Khoa4 = qdthuoc3.Where(p => p.GTinh == 0).Where(p => p.KP4 == 1).Count();
                moi.Khoa5 = qdthuoc3.Where(p => p.GTinh == 0).Where(p => p.KP5 == 1).Count();
                _lBC.Add(moi);

                moi = new BC();
                moi.stt = 18;
                moi.sttHT = "";
                moi.noidung = "Trẻ em < 6 tuổi";
                moi.donvi = "Lượt";
                moi.Phongkham = qdthuoc3.Where(p => p.Tuoi == null || p.Tuoi < 6).Where(p => p.PKham == 1).Count();
                moi.Khoa1 = qdthuoc3.Where(p => p.Tuoi == null || p.Tuoi < 6).Where(p => p.KP1 == 1).Count();
                moi.Khoa2 = qdthuoc3.Where(p => p.Tuoi == null || p.Tuoi < 6).Where(p => p.KP2 == 1).Count();
                moi.Khoa3 = qdthuoc3.Where(p => p.Tuoi == null || p.Tuoi < 6).Where(p => p.KP3 == 1).Count();
                moi.Khoa4 = qdthuoc3.Where(p => p.Tuoi == null || p.Tuoi < 6).Where(p => p.KP4 == 1).Count();
                moi.Khoa5 = qdthuoc3.Where(p => p.Tuoi == null || p.Tuoi < 6).Where(p => p.KP5 == 1).Count();
                _lBC.Add(moi);

                moi = new BC();
                moi.stt = 19;
                moi.sttHT = "";
                moi.noidung = "Trẻ em < 15 tuổi";
                moi.donvi = "Lượt";
                moi.Phongkham = qdthuoc3.Where(p => p.Tuoi == null || p.Tuoi < 15).Where(p => p.PKham == 1).Count();
                moi.Khoa1 = qdthuoc3.Where(p => p.Tuoi == null || p.Tuoi < 15).Where(p => p.KP1 == 1).Count();
                moi.Khoa2 = qdthuoc3.Where(p => p.Tuoi == null || p.Tuoi < 15).Where(p => p.KP2 == 1).Count();
                moi.Khoa3 = qdthuoc3.Where(p => p.Tuoi == null || p.Tuoi < 15).Where(p => p.KP3 == 1).Count();
                moi.Khoa4 = qdthuoc3.Where(p => p.Tuoi == null || p.Tuoi < 15).Where(p => p.KP4 == 1).Count();
                moi.Khoa5 = qdthuoc3.Where(p => p.Tuoi == null || p.Tuoi < 15).Where(p => p.KP5 == 1).Count();
                _lBC.Add(moi);

                moi = new BC();
                moi.stt = 20;
                moi.sttHT = "";
                moi.noidung = "Hộ nghèo";
                moi.donvi = "Lượt";
                moi.Phongkham = qdthuoc3.Where(p => p.MaDTuong == "HN").Where(p => p.PKham == 1).Count();
                moi.Khoa1 = qdthuoc3.Where(p => p.MaDTuong == "HN").Where(p => p.KP1 == 1).Count();
                moi.Khoa2 = qdthuoc3.Where(p => p.MaDTuong == "HN").Where(p => p.KP2 == 1).Count();
                moi.Khoa3 = qdthuoc3.Where(p => p.MaDTuong == "HN").Where(p => p.KP3 == 1).Count();
                moi.Khoa4 = qdthuoc3.Where(p => p.MaDTuong == "HN").Where(p => p.KP4 == 1).Count();
                moi.Khoa5 = qdthuoc3.Where(p => p.MaDTuong == "HN").Where(p => p.KP5 == 1).Count();
                _lBC.Add(moi);


                var qbhKhac4 = qdthuoc3.Where(p => p.SThe != null && p.SThe != "" && p.Tuoi >= 6).Where(p => p.MaDTuong != "HN").ToList();
                moi = new BC();
                moi.stt = 21;
                moi.sttHT = "";
                moi.noidung = "Bảo hiểm khác";
                moi.donvi = "Lượt";
                moi.Phongkham = qbhKhac4.Where(p => p.PKham == 1).Count();
                moi.Khoa1 = qbhKhac4.Where(p => p.KP1 == 1).Count();
                moi.Khoa2 = qbhKhac4.Where(p => p.KP2 == 1).Count();
                moi.Khoa3 = qbhKhac4.Where(p => p.KP3 == 1).Count();
                moi.Khoa4 = qbhKhac4.Where(p => p.KP4 == 1).Count();
                moi.Khoa5 = qbhKhac4.Where(p => p.KP5 == 1).Count();
                _lBC.Add(moi);


                moi = new BC();
                moi.stt = 22;
                moi.sttHT = "";
                moi.noidung = "Viện phí";
                moi.donvi = "Lượt";
                moi.Phongkham = qdthuoc3.Where(p => p.SThe == null || p.SThe == "").Where(p => p.PKham == 1).Count();
                moi.Khoa1 = qdthuoc3.Where(p => p.SThe == null || p.SThe == "").Where(p => p.KP1 == 1).Count();
                moi.Khoa2 = qdthuoc3.Where(p => p.SThe == null || p.SThe == "").Where(p => p.KP2 == 1).Count();
                moi.Khoa3 = qdthuoc3.Where(p => p.SThe == null || p.SThe == "").Where(p => p.KP3 == 1).Count();
                moi.Khoa4 = qdthuoc3.Where(p => p.SThe == null || p.SThe == "").Where(p => p.KP4 == 1).Count();
                moi.Khoa5 = qdthuoc3.Where(p => p.SThe == null || p.SThe == "").Where(p => p.KP5 == 1).Count();
                _lBC.Add(moi);
                #endregion

                var TSNgayDtri = (from bn in qbnkb.Where(p => p.NoiTru == 1 || (p.NoiTru == 0 && p.DTNT == true))
                                  join kb in qIDKB on bn.IDKB equals kb.IDKB
                                  join vv in data.VaoViens on bn.MaBNhan equals vv.MaBNhan
                                  join kp in lkp on bn.MaKP equals kp.MaKP
                                  select new
                                  {
                                      bn.MaBNhan,
                                      SoNgaydt = bn.bnRaVien == 1 ? bn.SoNgaydt : (denngay - bn.NNhap.Value).Days,
                                      bn.NoiTru,
                                      bn.Tuoi,
                                      bn.GTinh,
                                      bn.DTNT,
                                      bn.SThe,
                                      bn.MaDTuong,
                                      bn.MaKP,
                                      bn.NgayKham,
                                      bn.IDKB,
                                      bn.NNhap,
                                      bn.MaKPdt,
                                      kp.PLoai,
                                      bn.bnRaVien,
                                     

                                  }).OrderBy(p=>p.MaKP).ToList();

             
                #region tổng số ngày điều trị
                moi = new BC();
                moi.stt = 23;
                moi.sttHT = "5";
                moi.noidung = "Tổng số ngày điều trị";
                moi.donvi = "Ngày";
                moi.Phongkham = TSNgayDtri.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.PhongKham).Sum(p => p.SoNgaydt ?? 0);
                moi.Khoa1 = TSNgayDtri.Where(p => p.MaKP == makp1).Sum(p => p.SoNgaydt ?? 0);
                moi.Khoa2 = TSNgayDtri.Where(p => p.MaKP == makp2).Sum(p => p.SoNgaydt ?? 0);
                moi.Khoa3 = TSNgayDtri.Where(p => p.MaKP == makp3).Sum(p => p.SoNgaydt ?? 0);
                moi.Khoa4 = TSNgayDtri.Where(p => p.MaKP == makp4).Sum(p => p.SoNgaydt ?? 0);
                moi.Khoa5 = TSNgayDtri.Where(p => p.MaKP == makp5).Sum(p => p.SoNgaydt ?? 0);
                _lBC.Add(moi);
                #endregion

                #region TS bệnh nhân ra viện
                 var bnravien = (from bn in qIDKB join rv in data.RaViens on bn.MaBNhan equals rv.MaBNhan
                                 join bnhan in data.BenhNhans.Where(p => p.NoiTru == 1 || (p.NoiTru == 0 && p.DTNT == true)) on bn.MaBNhan equals bnhan.MaBNhan
                                 join vv in data.VaoViens on bn.MaBNhan equals vv.MaBNhan
                                 join kp in data.KPhongs on rv.MaKP equals kp.MaKP
                                    select new {bn.MaBNhan, rv.MaKP, bnhan.NoiTru,bnhan.DTNT, kp.PLoai
                                    }).ToList();
                                    //qkp.Where(p => p.NoiTru == 1 || (p.NoiTru == 0 && p.DTNT == true)).ToList();
                moi = new BC();
                moi.stt = 24;
                moi.sttHT = "6";
                moi.noidung = "Tổng số bệnh nhân ra viện";
                moi.donvi = "Ngày";
                moi.Phongkham = bnravien.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.PhongKham).Count();
                moi.Khoa1 = bnravien.Where(p => p.MaKP == makp1).Count();
                moi.Khoa2 = bnravien.Where(p => p.MaKP == makp2).Count();
                moi.Khoa3 = bnravien.Where(p => p.MaKP == makp3).Count();
                moi.Khoa4 = bnravien.Where(p => p.MaKP == makp4).Count();
                moi.Khoa5 = bnravien.Where(p => p.MaKP == makp5).Count();
                _lBC.Add(moi);
                #endregion


                rep.celNgayThang.Text = "Từ ngày " + lupTuNgay.DateTime.ToString("dd/MM/yyyy") + " đến ngày " + lupDenNgay.DateTime.ToString("dd/MM/yyyy");
                rep.DataSource = _lBC.OrderBy(p => p.stt).ToList();
                rep.Bindingdata();
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
            }
            #endregion

            #region mẫu 2 --tìm theo dịch vụ ; khoa phòng là khoa phòng chỉ định
            else
            {
                var qdv = (from dv in data.DichVus join tn in data.TieuNhomDVs.Where(p => p.IDNhom == 1 || p.IDNhom == 2 || p.IDNhom == 3) on dv.IdTieuNhom equals tn.IdTieuNhom select new { tn.IDNhom, dv.TenDV, dv.MaDV, tn.TenTN}).ToList();
                var q = (from bn in data.BenhNhans.Where(p => p.NNhap >= tungay && p.NNhap <= denngay)
                         join cls in data.CLS on bn.MaBNhan equals cls.MaBNhan
                         join cd in data.ChiDinhs.Where(p=>p.Status == 1) on cls.IdCLS equals cd.IdCLS                       
                         join kp in data.KPhongs on cls.MaKP equals kp.MaKP                         
                         select new
                         {
                             kp.MaKP,
                             kp.TenKP,
                             cd.MaDV,
                             kp.PLoai,
                            
                         }).ToList();
                _lBC = (from bn in q join dv in qdv on bn.MaDV equals dv.MaDV group new {dv, bn} by new {dv.TenTN, dv.IDNhom} into kq select new BC { 
                idNhom=  kq.Key.IDNhom??0,
                noidung = kq.Key.TenTN,
                Phongkham = kq.Where(p => p.bn.PLoai == DungChung.Bien.st_PhanLoaiKP.PhongKham).Count(),
                Khoa1 = kq.Where(p => p.bn.MaKP == makp1).Count(),
                Khoa2 = kq.Where(p => p.bn.MaKP == makp2).Count(),
                Khoa3 = kq.Where(p => p.bn.MaKP == makp3).Count(),
                Khoa4 = kq.Where(p => p.bn.MaKP == makp4).Count(),
                Khoa5 = kq.Where(p => p.bn.MaKP == makp5).Count(),
                }).OrderBy(p=>p.idNhom).ThenBy(p=>p.noidung).ToList();

                int count = 1;
                foreach (BC a in _lBC)
                {
                    a.sttHT = count.ToString();
                    a.donvi = "Lượt";
                    count++;
                }
                
                rep.celNgayThang.Text = "Từ ngày " + lupTuNgay.DateTime.ToString("dd/MM/yyyy") + " đến ngày " + lupDenNgay.DateTime.ToString("dd/MM/yyyy");
                rep.DataSource = _lBC;
                rep.Bindingdata();
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();

            }
            #endregion


        }
        public class BC
        {
            public int stt { set; get; }
            public int idNhom { set; get; }           
            public string sttHT { set; get; }
            public string noidung { set; get; }
            public string donvi { set; get; }
            public int Phongkham { set; get; }
            public int Khoa1 { set; get; }
            public int Khoa2 { set; get; }
            public int Khoa3 { set; get; }
            public int Khoa4 { set; get; }
            public int Khoa5 { set; get; }

        }

        private void frm_BC_ChiSoCongTacKCB_12122_Load(object sender, EventArgs e)
        {
            lupTuNgay.DateTime = DateTime.Now;
            lupDenNgay.DateTime = DateTime.Now;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}