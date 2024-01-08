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
    public partial class frm_KhamChuaBenh_BVYHCT_12121 : DevExpress.XtraEditors.XtraForm
    {
        public frm_KhamChuaBenh_BVYHCT_12121()
        {
            InitializeComponent();
        }

        private void frm_KhamChuaBenh_BVYHCT_Load(object sender, EventArgs e)
        {
            dateTuNgay.DateTime = DateTime.Now;
            dateDenNgay.DateTime = DateTime.Now;
            MinimizeBox = false;
            MaximizeBox = false;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Lấy tuyến bệnh viện thông qua hạng bệnh viện
        /// </summary>
        /// <param name="hangBv"></param>
        /// <returns></returns>
        public int getTuyenByHang(string hangBv)
        {
            int rs = 0;
            if (!String.IsNullOrEmpty(hangBv))
            {
                hangBv = hangBv.Trim();
                switch (hangBv)
                {
                    case "A":
                        rs = 1;
                        break;
                    case "B":
                        rs = 2;
                        break;
                    case "C":
                        rs = 3;
                        break;
                    case "D":
                        rs = 4;
                        break;
                }
            }
            return rs;

        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            List<DanhMuc> _listContent = new List<DanhMuc>();
            DanhMuc moi = new DanhMuc();
            DateTime tungay = DungChung.Ham.NgayTu(dateTuNgay.DateTime);
            DateTime denngay = DungChung.Ham.NgayDen(dateDenNgay.DateTime);
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            string hangBV = data.BenhViens.Where(p => p.MaBV == DungChung.Bien.MaBV).Select(p => p.TuyenBV).FirstOrDefault();
            int _tuyen = getTuyenByHang(hangBV);

            List<KPhong> _listKP = data.KPhongs.ToList();
            //var qcls = data.CLS.Select(p => p.MaBNhan).Distinct().ToList();

            var q1 = (from kp in data.KPhongs
                      join bnkb in data.BNKBs
                      on kp.MaKP equals bnkb.MaKP
                      select new { MaBNhan = bnkb.MaBNhan ?? 0 }).ToList();
            // List<int> lbn = q1.ToList();.
            var benhnhan = (from bn in data.BenhNhans.Where(p => p.NNhap >= tungay && p.NNhap <= denngay)                           
                            join ttbx in data.TTboXungs on bn.MaBNhan equals ttbx.MaBNhan into kq
                            from kq1 in kq.DefaultIfEmpty()
                            join vv in data.VaoViens on bn.MaBNhan equals vv.MaBNhan into kqvv
                                                from kq3 in kqvv.DefaultIfEmpty()
                            select new
                            {
                                bn.MaBNhan,
                                bn.Tuoi,
                                bn.GTinh,
                                bn.NoiTru,
                                bn.DTNT,
                                bn.SThe,
                                bn.MaDTuong,
                                bn.MaBV,                                
                                nn = kq1 == null ? "" : kq1.NgoaiKieu,
                                MaKPvv = kq3== null ? 0 : (kq3.MaKP??0),
                                cv = 0
                            }).ToList();

            var bncv = (from a in benhnhan 
                            join b in data.BNKBs.Where(p => p.PhuongAn == 2) on a.MaBNhan equals b.MaBNhan 
                            group a by a into kq
                            select kq).ToList();
            benhnhan = (from a in benhnhan
                        join b in bncv on a.MaBNhan equals b.Key.MaBNhan into k
                        from k1 in k.DefaultIfEmpty()
                        select new {
                            a.MaBNhan,
                            a.Tuoi,
                            a.GTinh,
                            a.NoiTru,
                            a.DTNT,
                            a.SThe,
                            a.MaDTuong,
                            a.MaBV,
                            a.nn ,
                            a.MaKPvv,
                            cv = k1 != null ? 2 : a.cv
                        }).ToList();

            var dtt = (from a in data.DThuocs
                       join b in data.DThuoccts.Where(p => p.NgayNhap >= tungay) on a.IDDon equals b.IDDon
                       group a by a.MaBNhan into kq
                       select new { MaBNhan = kq.Key, ketqua = kq.Count() }).ToList();

            var bnkedon = (from bn in benhnhan
                           join dt in dtt on bn.MaBNhan equals dt.MaBNhan 
                           group new {bn, dt} by new {bn.MaBNhan,
                               bn.Tuoi,                      
                               bn.GTinh,
                               bn.NoiTru,
                               bn.DTNT,
                               bn.SThe,
                               bn.MaDTuong,
                               bn.MaBV,
                               bn.nn,  
                               bn.MaKPvv,
                               dt.ketqua
                           } into ketqua
                           select new
                           {
                               ketqua.Key.MaBNhan,
                               Tuoi = ketqua.Key.Tuoi ?? 0,
                               ketqua.Key.GTinh,
                               ketqua.Key.NoiTru,
                               ketqua.Key.DTNT,
                               SThe = ketqua.Key.SThe??"",
                               ketqua.Key.MaDTuong,
                               ketqua.Key.MaBV,
                               nn = ketqua.Key.nn ?? "",
                               ketqua.Key.MaKPvv,
                               KeDon = (ketqua.Key.ketqua > 1 ? (ketqua.Count() > 0 ? 1 : 0) : (ketqua.Where(p => p.bn.cv == 2).Count() > 0 ? 2 : 0))
                               //(ketqua.Count() > 1 || ketqua.Where(p => p.bn.cv == 1).Count() > 0) ? 1 : 0
                           }).ToList();

            //var qkb = (from bnkb in q1
            //           join bn in benhnhan on bnkb.MaBNhan equals bn.MaBNhan                                
            //           //join cls in qcls on bn.MaBNhan equals cls into kqcls
            //           //          from kq2 in kqcls.DefaultIfEmpty()
            //           join vv in data.VaoViens on bnkb.MaBNhan equals vv.MaBNhan into kqvv
            //                     from kq3 in kqvv.DefaultIfEmpty()
            //           group new { bn,  kq3 } by new
            //           {
            //               bn.MaBNhan,
            //               bn.Tuoi,
            //               bn.GTinh,
            //               bn.NoiTru,
            //               bn.DTNT,
            //               bn.SThe,
            //               bn.MaDTuong,
            //               bn.MaBV,
            //               bn.nn,
            //               //kq2,
            //               kq3
            //           } into kqua
            //           select new
            //           {
            //               kqua.Key.MaBNhan,
            //               kqua.Key.Tuoi,
            //               kqua.Key.GTinh,
            //               kqua.Key.NoiTru,
            //               kqua.Key.DTNT,
            //               kqua.Key.SThe,
            //               kqua.Key.MaDTuong,
            //               nn = kqua.Key.nn,
            //               kqua.Key.MaBV,
            //               //kqua.Key.kq2,
            //               kqua.Key.kq3
            //           }).ToList();


            var qBHYT = bnkedon.Where(p =>p.SThe != "" && p.MaDTuong != "HN" && p.MaDTuong != "hn" && p.MaDTuong != "DT" && p.MaDTuong != "dt" && p.Tuoi > 6).ToList();// bn có thẻ
            var qHN = bnkedon.Where(p =>p.Tuoi > 6 && (  p.MaDTuong == "HN" || p.MaDTuong == "hn" || p.MaDTuong == "DT" || p.MaDTuong == "dt")).ToList();// hộ nghèo
            var qvp = bnkedon.Where(p =>p.SThe == "").ToList();// bệnh nhân viện phí
            var qNN = bnkedon.Where(p => p.nn != "" && p.nn.ToLower() != "việt nam").ToList(); // người nước ngoài

            #region I : tổng số lượt khám
            moi.SttHT = 1;
            moi.Stt = "I";
            moi.TenDanhMuc = "TỔNG SỐ LƯỢT KHÁM BỆNH: ";
            moi.TenKhoa = "";
            moi.TongSo = bnkedon.Count();
            moi.BN_BHYT_Duoi60 = qBHYT.Where(p => p.Tuoi < 60).Count();
            moi.BN_BHYT_Tren60 = qBHYT.Where(p => p.Tuoi >= 60 && p.Tuoi < 80).Count();
            moi.BN_BHYT_Tren80 = qBHYT.Where(p =>p.Tuoi >= 80).Count();
            moi.BN_TuVanKhongBH =qvp.Where(p => p.KeDon == 0).Where(p => p.NoiTru == 0 && p.DTNT == false).Count();
            moi.BN_BHNguoiNgheo_Duoi60 = qHN.Where(p =>p.Tuoi < 60).Count();
            moi.BN_BHNguoiNgheo_Tren60 = qHN.Where(p => p.Tuoi >= 60 && p.Tuoi < 80).Count();
            moi.BN_BHNguoiNgheo_Tren80 = qHN.Where(p => p.Tuoi >= 80).Count();
            moi.TreEm_Duoi6 = bnkedon.Where(p => p.Tuoi <= 6).Count();
            moi.BN_VienPhi_Duoi60 = qvp.Where(p =>p.Tuoi < 60).Count();
            moi.BN_VienPhi_Tren60 = qvp.Where(p => p.Tuoi >= 60 && p.Tuoi < 80).Count();
            moi.BN_VienPhi_Tren80 = qvp.Where(p =>p.Tuoi >= 80).Count();
            moi.BN_NuocNgoai_Duoi60 = qNN.Where(p => p.Tuoi < 60).Count();
            moi.BN_NuocNgoai_Tren60 = qNN.Where(p => p.Tuoi >= 60).Count();
            _listContent.Add(moi);
            #endregion

            #region 2 : tổng số kê đơn (bệnh nhân ngoại trú( khám, có chỉ định CLS)
            moi = new DanhMuc();
            moi.SttHT = 2;
            moi.Stt = "1";
            moi.TenDanhMuc = "Tổng số bệnh nhân kê đơn";
            moi.TenKhoa = "";
            moi.TongSo = bnkedon.Where(p=>p.KeDon == 1).Where(p => p.NoiTru == 0 && p.DTNT == false).Count();
            moi.BN_BHYT_Duoi60 = qBHYT.Where(p => p.KeDon == 1).Where(p => p.NoiTru == 0 && p.DTNT == false).Where(p => p.Tuoi < 60).Count();
            moi.BN_BHYT_Tren60 = qBHYT.Where(p => p.KeDon == 1).Where(p => p.NoiTru == 0 && p.DTNT == false).Where(p =>p.Tuoi >= 60 && p.Tuoi < 80).Count();
            moi.BN_BHYT_Tren80 = qBHYT.Where(p => p.KeDon == 1).Where(p => p.NoiTru == 0 && p.DTNT == false).Where(p =>p.Tuoi >= 80).Count();
            moi.BN_TuVanKhongBH = 0;
            moi.BN_BHNguoiNgheo_Duoi60 = qHN.Where(p => p.KeDon == 1).Where(p => p.NoiTru == 0 && p.DTNT == false).Where(p => p.Tuoi < 60).Count();
            moi.BN_BHNguoiNgheo_Tren60 = qHN.Where(p => p.KeDon == 1).Where(p => p.NoiTru == 0 && p.DTNT == false).Where(p => p.Tuoi >= 60 && p.Tuoi < 80).Count();
            moi.BN_BHNguoiNgheo_Tren80 = qHN.Where(p => p.KeDon == 1).Where(p => p.NoiTru == 0 && p.DTNT == false).Where(p =>p.Tuoi >= 80).Count();

            moi.TreEm_Duoi6 = bnkedon.Where(p => p.KeDon == 1).Where(p => p.NoiTru == 0 && p.DTNT == false).Where(p =>p.Tuoi <= 6).Count();
            moi.BN_VienPhi_Duoi60 = qvp.Where(p => p.KeDon == 1).Where(p => p.NoiTru == 0 && p.DTNT == false).Where(p => p.Tuoi < 60).Count();
            moi.BN_VienPhi_Tren60 = qvp.Where(p => p.KeDon == 1).Where(p => p.NoiTru == 0 && p.DTNT == false).Where(p => p.Tuoi >= 60 && p.Tuoi < 80).Count();
            moi.BN_VienPhi_Tren80 = qvp.Where(p => p.KeDon == 1).Where(p => p.NoiTru == 0 && p.DTNT == false).Where(p => p.Tuoi >= 80).Count();
            moi.BN_NuocNgoai_Duoi60 = qNN.Where(p => p.KeDon == 1).Where(p => p.NoiTru == 0 && p.DTNT == false).Where(p => p.Tuoi < 60).Count();
            moi.BN_NuocNgoai_Tren60 = qNN.Where(p => p.KeDon == 1).Where(p => p.NoiTru == 0 && p.DTNT == false).Where(p => p.Tuoi >= 60).Count();
            _listContent.Add(moi);
            #endregion

            #region 3 : tổng số điều trị ngoại trú
            moi = new DanhMuc();
            moi.SttHT = 3;
            moi.Stt = "2";
            moi.TenDanhMuc = "Tổng số bệnh nhân điều trị ngoại trú";
            moi.TenKhoa = "";
            moi.TongSo = bnkedon.Where(p => p.NoiTru == 0 && p.DTNT == true && p.MaKPvv > 0).Count();//  p.MaKPvv >0 => có trong bảng vào viện
            moi.BN_BHYT_Duoi60 = qBHYT.Where(p => p.NoiTru == 0 && p.DTNT == true && p.MaKPvv > 0).Where(p =>p.Tuoi < 60).Count();
            moi.BN_BHYT_Tren60 = qBHYT.Where(p => p.NoiTru == 0 && p.DTNT == true && p.MaKPvv > 0).Where(p => p.Tuoi >= 60 && p.Tuoi < 80).Count();
            moi.BN_BHYT_Tren80 = qBHYT.Where(p => p.NoiTru == 0 && p.DTNT == true && p.MaKPvv > 0).Where(p => p.Tuoi >= 80).Count();
            moi.BN_TuVanKhongBH = 0;
            moi.BN_BHNguoiNgheo_Duoi60 = qHN.Where(p => p.NoiTru == 0 && p.DTNT == true && p.MaKPvv > 0).Where(p =>p.Tuoi < 60).Count();
            moi.BN_BHNguoiNgheo_Tren60 = qHN.Where(p => p.NoiTru == 0 && p.DTNT == true && p.MaKPvv > 0).Where(p => p.Tuoi >= 60 && p.Tuoi < 80).Count();
            moi.BN_BHNguoiNgheo_Tren80 = qHN.Where(p => p.NoiTru == 0 && p.DTNT == true && p.MaKPvv > 0).Where(p =>p.Tuoi >= 80).Count();
            moi.TreEm_Duoi6 = bnkedon.Where(p => p.NoiTru == 0 && p.DTNT == true && p.MaKPvv > 0).Where(p =>p.Tuoi <=6).Count();
            moi.BN_VienPhi_Duoi60 = qvp.Where(p => p.NoiTru == 0 && p.DTNT == true && p.MaKPvv > 0).Where(p => p.Tuoi < 60).Count();
            moi.BN_VienPhi_Tren60 = qvp.Where(p => p.NoiTru == 0 && p.DTNT == true && p.MaKPvv > 0).Where(p => p.Tuoi >= 60 && p.Tuoi < 80).Count();
            moi.BN_VienPhi_Tren80 = qvp.Where(p => p.NoiTru == 0 && p.DTNT == true && p.MaKPvv > 0).Where(p => p.Tuoi >= 80).Count();
            moi.BN_NuocNgoai_Duoi60 = qNN.Where(p => p.NoiTru == 0 && p.DTNT == true && p.MaKPvv > 0).Where(p =>p.Tuoi < 60).Count();
            moi.BN_NuocNgoai_Tren60 = qNN.Where(p => p.NoiTru == 0 && p.DTNT == true && p.MaKPvv > 0).Where(p =>p.Tuoi >= 60).Count();
            _listContent.Add(moi);
            #endregion

            #region 4 : tổng số điều trị nội trú
            var qnoitru = bnkedon.Where(p => p.NoiTru == 1).ToList();
            var _lMakpdtri = qnoitru.Select(p => p.MaKPvv).Distinct();
            var _lKPDtri = (from makp in _lMakpdtri join kp in data.KPhongs on makp equals kp.MaKP select new { makp, kp.TenKP }).ToList();
            //  int stt = 4;
            foreach (var a in _lKPDtri)
            {
                var qNoiTruTheoKhoa = qnoitru.Where(p => p.MaKPvv == a.makp).ToList();
               
                moi = new DanhMuc();
                moi.SttHT = 4;
                moi.Stt = "3";
                moi.TenDanhMuc = "Tổng số bệnh nhân nội trú";
                moi.TenKhoa = "    - " + a.TenKP;
                moi.TongSo = qNoiTruTheoKhoa.Count();
                moi.BN_BHYT_Duoi60 = qNoiTruTheoKhoa.Where(p => p.SThe != "" && p.MaDTuong != "HN" && p.MaDTuong != "hn" && p.MaDTuong != "DT" && p.MaDTuong != "dt" && p.Tuoi >= 6).Where(p => p.Tuoi < 60).Count();
                moi.BN_BHYT_Tren60 = qNoiTruTheoKhoa.Where(p => p.SThe != "" && p.MaDTuong != "HN" && p.MaDTuong != "hn" && p.MaDTuong != "DT" && p.MaDTuong != "dt" && p.Tuoi >= 6).Where(p => p.Tuoi >= 60 && p.Tuoi < 80).Count();
                moi.BN_BHYT_Tren80 = qNoiTruTheoKhoa.Where(p => p.SThe != "" && p.MaDTuong != "HN" && p.MaDTuong != "hn" && p.MaDTuong != "DT" && p.MaDTuong != "dt" && p.Tuoi >= 6).Where(p => p.Tuoi >= 80).Count();
                moi.BN_TuVanKhongBH = 0;
                moi.BN_BHNguoiNgheo_Duoi60 = qNoiTruTheoKhoa.Where(p => p.Tuoi > 6 && (p.MaDTuong == "HN" || p.MaDTuong == "hn" || p.MaDTuong == "DT" || p.MaDTuong == "dt")).Where(p => p.Tuoi < 60).Count();
                moi.BN_BHNguoiNgheo_Tren60 = qNoiTruTheoKhoa.Where(p => p.Tuoi > 6 && (p.MaDTuong == "HN" || p.MaDTuong == "hn" || p.MaDTuong == "DT" || p.MaDTuong == "dt")).Where(p => p.Tuoi >= 60 && p.Tuoi < 80).Count();
                moi.BN_BHNguoiNgheo_Tren80 = qNoiTruTheoKhoa.Where(p => p.Tuoi > 6 && (p.MaDTuong == "HN" || p.MaDTuong == "hn" || p.MaDTuong == "DT" || p.MaDTuong == "dt")).Where(p => p.Tuoi >= 80).Count();
                moi.TreEm_Duoi6 = qNoiTruTheoKhoa.Where(p =>p.Tuoi <= 6).Count();
                moi.BN_VienPhi_Duoi60 = qNoiTruTheoKhoa.Where(p =>p.SThe == "").Where(p =>p.Tuoi < 60).Count();
                moi.BN_VienPhi_Tren60 = qNoiTruTheoKhoa.Where(p => p.SThe == "").Where(p => p.Tuoi >= 60 && p.Tuoi < 80).Count();
                moi.BN_VienPhi_Tren80 = qNoiTruTheoKhoa.Where(p =>p.SThe == "").Where(p =>p.Tuoi >= 80).Count();
                moi.BN_NuocNgoai_Duoi60 = qNoiTruTheoKhoa.Where(p =>p.nn != "" && p.nn.ToLower() != "việt nam").Where(p =>p.Tuoi < 60).Count();
                moi.BN_NuocNgoai_Tren60 = qNoiTruTheoKhoa.Where(p =>p.nn != "" && p.nn.ToLower() != "việt nam").Where(p =>p.Tuoi >= 60).Count();
                _listContent.Add(moi);

            }
            #endregion
            #region 5 : tổng số người bệnh được tư vấn
            moi = new DanhMuc();
            moi.SttHT = 5;
            moi.Stt = "4";
            moi.TenDanhMuc = "Tổng số người bệnh được tư vấn (không điều trị + cận lâm sàng) ";
            moi.TenKhoa = "";
            moi.TongSo = bnkedon.Where(p => p.KeDon == 0).Where(p=> p.NoiTru == 0 && p.DTNT == false).Count();
            moi.BN_BHYT_Duoi60 = qBHYT.Where(p => p.KeDon == 0).Where(p => p.NoiTru == 0 && p.DTNT == false).Where(p =>p.Tuoi < 60).Count();
            moi.BN_BHYT_Tren60 = qBHYT.Where(p => p.KeDon == 0).Where(p => p.NoiTru == 0 && p.DTNT == false).Where(p => p.Tuoi >= 60 && p.Tuoi < 80).Count();
            moi.BN_BHYT_Tren80 = qBHYT.Where(p => p.KeDon == 0).Where(p => p.NoiTru == 0 && p.DTNT == false).Where(p =>p.Tuoi >= 80).Count();
            moi.BN_TuVanKhongBH = qvp.Where(p => p.KeDon == 0).Where(p => p.NoiTru == 0 && p.DTNT == false).Count();
            moi.BN_BHNguoiNgheo_Duoi60 = qHN.Where(p => p.KeDon == 0).Where(p => p.NoiTru == 0 && p.DTNT == false).Where(p =>p.Tuoi < 60).Count();
            moi.BN_BHNguoiNgheo_Tren60 = qHN.Where(p => p.KeDon == 0).Where(p => p.NoiTru == 0 && p.DTNT == false).Where(p => p.Tuoi >= 60 && p.Tuoi < 80).Count();
            moi.BN_BHNguoiNgheo_Tren80 = qHN.Where(p => p.KeDon == 0).Where(p => p.NoiTru == 0 && p.DTNT == false).Where(p =>p.Tuoi >= 80).Count();
            moi.TreEm_Duoi6 = bnkedon.Where(p => p.KeDon == 0).Where(p => p.NoiTru == 0 && p.DTNT == false).Where(p =>p.Tuoi <= 6).Count();
            moi.BN_VienPhi_Duoi60 = qvp.Where(p => p.KeDon == 0).Where(p => p.NoiTru == 0 && p.DTNT == false).Where(p =>p.Tuoi < 60).Count();
            moi.BN_VienPhi_Tren60 = qvp.Where(p => p.KeDon == 0).Where(p => p.NoiTru == 0 && p.DTNT == false).Where(p => p.Tuoi >= 60 && p.Tuoi < 80).Count();
            moi.BN_VienPhi_Tren80 = qvp.Where(p => p.KeDon == 0).Where(p => p.NoiTru == 0 && p.DTNT == false).Where(p =>p.Tuoi >= 80).Count();
            moi.BN_NuocNgoai_Duoi60 = qNN.Where(p => p.KeDon == 0).Where(p => p.NoiTru == 0 && p.DTNT == false).Where(p =>p.Tuoi < 60).Count();
            moi.BN_NuocNgoai_Tren60 = qNN.Where(p => p.KeDon == 0).Where(p => p.NoiTru == 0 && p.DTNT == false).Where(p =>p.Tuoi >= 60).Count();
            _listContent.Add(moi);
            #endregion


            #region 6 : tổng số chuyển tuyến dưới lên
            var qchuyentuyenlen = (from a in bnkedon.Where(p => p.MaBV != null) join bv in data.BenhViens on a.MaBV equals bv.MaBV select new { a.MaBNhan, a.Tuoi, a.MaBV, a.MaDTuong, a.nn, a.NoiTru, a.SThe, a.GTinh, a.DTNT, bv.TuyenBV, Tuyen = getTuyenByHang(bv.TuyenBV) }).ToList();


            moi = new DanhMuc();
            moi.SttHT = 6;
            moi.Stt = "5";
            moi.TenDanhMuc = "Bệnh nhân chuyển tuyến dưới lên";
            moi.TenKhoa = "";
            moi.TongSo = qchuyentuyenlen.Where(p => p.Tuyen > _tuyen).Count();
            moi.BN_BHYT_Duoi60 = qchuyentuyenlen.Where(p => p.Tuyen > _tuyen).Where(p => p.SThe != "" && p.MaDTuong != "HN" && p.MaDTuong != "hn" && p.MaDTuong != "DT" && p.MaDTuong != "dt" && p.Tuoi >= 6).Where(p => p.Tuoi < 60).Count();
            moi.BN_BHYT_Tren60 = qchuyentuyenlen.Where(p => p.Tuyen > _tuyen).Where(p => p.SThe != "").Where(p => p.SThe != "" && p.MaDTuong != "HN" && p.MaDTuong != "hn" && p.MaDTuong != "DT" && p.MaDTuong != "dt" && p.Tuoi >= 60 && p.Tuoi < 80).Count();
            moi.BN_BHYT_Tren80 = qchuyentuyenlen.Where(p => p.Tuyen > _tuyen).Where(p => p.SThe != "" && p.MaDTuong != "HN" && p.MaDTuong != "hn" && p.MaDTuong != "DT" && p.MaDTuong != "dt" && p.Tuoi >= 6).Where(p => p.Tuoi >= 80).Count();
            moi.BN_TuVanKhongBH = 0;
            moi.BN_BHNguoiNgheo_Duoi60 = qchuyentuyenlen.Where(p => p.Tuyen > _tuyen).Where(p => p.Tuoi > 6 && (p.MaDTuong == "HN" || p.MaDTuong == "hn" || p.MaDTuong == "DT" || p.MaDTuong == "dt")).Where(p => p.Tuoi < 60).Count();
            moi.BN_BHNguoiNgheo_Tren60 = qchuyentuyenlen.Where(p => p.Tuyen > _tuyen).Where(p => p.Tuoi > 6 && (p.MaDTuong == "HN" || p.MaDTuong == "hn" || p.MaDTuong == "DT" || p.MaDTuong == "dt")).Where(p => p.Tuoi >= 60 && p.Tuoi < 80).Count();
            moi.BN_BHNguoiNgheo_Tren80 = qchuyentuyenlen.Where(p => p.Tuyen > _tuyen).Where(p => p.Tuoi > 6 && (p.MaDTuong == "HN" || p.MaDTuong == "hn" || p.MaDTuong == "DT" || p.MaDTuong == "dt")).Where(p => p.Tuoi >= 80).Count();
            moi.TreEm_Duoi6 = qchuyentuyenlen.Where(p => p.Tuyen > _tuyen).Where(p =>p.Tuoi <= 6).Count();
            moi.BN_VienPhi_Duoi60 = qchuyentuyenlen.Where(p => p.Tuyen > _tuyen).Where(p =>p.SThe == "").Where(p =>p.Tuoi < 60).Count();
            moi.BN_VienPhi_Tren60 = qchuyentuyenlen.Where(p => p.Tuyen > _tuyen).Where(p => p.SThe == "").Where(p => p.Tuoi >= 60 && p.Tuoi < 80).Count();
            moi.BN_VienPhi_Tren80 = qchuyentuyenlen.Where(p => p.Tuyen > _tuyen).Where(p =>p.SThe == "").Where(p =>p.Tuoi >= 80).Count();
            moi.BN_NuocNgoai_Duoi60 = qchuyentuyenlen.Where(p => p.Tuyen > _tuyen).Where(p =>p.nn != "" && p.nn.ToLower() != "việt nam").Where(p => p.Tuoi < 60).Count();
            moi.BN_NuocNgoai_Tren60 = qchuyentuyenlen.Where(p => p.Tuyen > _tuyen).Where(p =>p.nn != "" && p.nn.ToLower() != "việt nam").Where(p => p.Tuoi >= 60).Count();
            _listContent.Add(moi);

            #endregion

            #region 6 : tổng số chuyển lên tuyến trên
            var qchuyentuyendi = (from a in bnkedon
                                  join rv in data.RaViens on a.MaBNhan equals rv.MaBNhan
                                  join bv in data.BenhViens on rv.MaBVC equals bv.MaBV
                                  select new { a.MaBNhan, a.Tuoi, a.MaBV, a.MaDTuong, a.nn, a.NoiTru, a.SThe, a.GTinh, a.DTNT, bv.TuyenBV, Tuyen = getTuyenByHang(bv.TuyenBV) }).ToList();

            moi = new DanhMuc();
            moi.SttHT = 7;
            moi.Stt = "6";
            moi.TenDanhMuc = "Bệnh nhân chuyển tuyến trên";
            moi.TenKhoa = "";
            moi.TongSo = qchuyentuyendi.Where(p => p.Tuyen < _tuyen).Count();
            moi.BN_BHYT_Duoi60 = qchuyentuyendi.Where(p => p.Tuyen < _tuyen).Where(p => p.SThe != "" && p.MaDTuong != "HN" && p.MaDTuong != "hn" && p.MaDTuong != "DT" && p.MaDTuong != "dt" && p.Tuoi >= 6).Where(p => p.Tuoi < 60).Count();
            moi.BN_BHYT_Tren60 = qchuyentuyendi.Where(p => p.Tuyen < _tuyen).Where(p => p.SThe != "" && p.MaDTuong != "HN" && p.MaDTuong != "hn" && p.MaDTuong != "DT" && p.MaDTuong != "dt" && p.Tuoi >= 6).Where(p => p.Tuoi >= 60 && p.Tuoi < 80).Count();
            moi.BN_BHYT_Tren80 = qchuyentuyendi.Where(p => p.Tuyen < _tuyen).Where(p => p.SThe != "" && p.MaDTuong != "HN" && p.MaDTuong != "hn" && p.MaDTuong != "DT" && p.MaDTuong != "dt" && p.Tuoi >= 6).Where(p => p.Tuoi >= 80).Count();
            moi.BN_TuVanKhongBH = 0;
            moi.BN_BHNguoiNgheo_Duoi60 = qchuyentuyendi.Where(p => p.Tuyen < _tuyen).Where(p => p.Tuoi > 6 && (p.MaDTuong == "HN" || p.MaDTuong == "hn" || p.MaDTuong == "DT" || p.MaDTuong == "dt")).Where(p => p.Tuoi < 60).Count();
            moi.BN_BHNguoiNgheo_Tren60 = qchuyentuyendi.Where(p => p.Tuyen < _tuyen).Where(p => p.Tuoi > 6 && (p.MaDTuong == "HN" || p.MaDTuong == "hn" || p.MaDTuong == "DT" || p.MaDTuong == "dt")).Where(p => p.Tuoi >= 60 && p.Tuoi < 80).Count();
            moi.BN_BHNguoiNgheo_Tren80 = qchuyentuyendi.Where(p => p.Tuyen < _tuyen).Where(p => p.Tuoi > 6 && (p.MaDTuong == "HN" || p.MaDTuong == "hn" || p.MaDTuong == "DT" || p.MaDTuong == "dt")).Where(p => p.Tuoi >= 80).Count();
            moi.TreEm_Duoi6 = qchuyentuyendi.Where(p => p.Tuyen < _tuyen).Where(p =>p.Tuoi < 6).Count();
            moi.BN_VienPhi_Duoi60 = qchuyentuyendi.Where(p => p.Tuyen < _tuyen).Where(p =>p.SThe == "").Where(p =>p.Tuoi <= 60).Count();
            moi.BN_VienPhi_Tren60 = qchuyentuyendi.Where(p => p.Tuyen < _tuyen).Where(p => p.SThe == "").Where(p => p.Tuoi >= 60 && p.Tuoi < 80).Count();
            moi.BN_VienPhi_Tren80 = qchuyentuyendi.Where(p => p.Tuyen < _tuyen).Where(p => p.SThe == "").Where(p =>p.Tuoi >= 80).Count();
            moi.BN_NuocNgoai_Duoi60 = qchuyentuyendi.Where(p => p.Tuyen < _tuyen).Where(p =>  p.nn != "" && p.nn.ToLower() != "việt nam").Where(p =>p.Tuoi < 60).Count();
            moi.BN_NuocNgoai_Tren60 = qchuyentuyendi.Where(p => p.Tuyen < _tuyen).Where(p => p.nn != "" && p.nn.ToLower() != "việt nam").Where(p =>p.Tuoi >= 60).Count();
            _listContent.Add(moi);

            #endregion

            BaoCao.Rep_BC_KhamChuaBenh_BVYHCT_12121 rep = new BaoCao.Rep_BC_KhamChuaBenh_BVYHCT_12121();
            frmIn frm = new frmIn();
            rep.DataSource = _listContent.OrderBy(p => p.SttHT).ThenBy(p => p.TenKhoa).ToList();

            if (txtNgayThangHT.Text != "")
                rep.lbl_tungay.Text = txtNgayThangHT.Text;
            else
                rep.lbl_tungay.Text = "Từ ngày " + dateTuNgay.DateTime.ToString("dd/MM/yyyy") + " đến ngày " + dateDenNgay.DateTime.ToString("dd/MM/yyyy");

            rep.Bindingdata();
            rep.CreateDocument();
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();
        }

        class DanhMuc
        {
            public int SttHT { get; set; }
            public string Stt { get; set; }
            public string TenDanhMuc { get; set; }
            public string TenKhoa { get; set; }
            public int TongSo { get; set; }
            public int BN_BHYT_Duoi60 { get; set; }
            public int BN_BHYT_Tren60 { get; set; }
            public int BN_BHYT_Tren80 { get; set; }
            public int BN_TuVanKhongBH { get; set; }
            public int BN_BHNguoiNgheo_Duoi60 { get; set; }
            public int BN_BHNguoiNgheo_Tren60 { get; set; }
            public int BN_BHNguoiNgheo_Tren80 { get; set; }
            public int TreEm_Duoi6 { get; set; }
            public int BN_VienPhi_Duoi60 { get; set; }
            public int BN_VienPhi_Tren60 { get; set; }
            public int BN_VienPhi_Tren80 { get; set; }
            public int BN_NuocNgoai_Duoi60 { get; set; }
            public int BN_NuocNgoai_Tren60 { get; set; }
        }
    }
}