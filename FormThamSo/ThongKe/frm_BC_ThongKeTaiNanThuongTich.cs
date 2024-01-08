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
    public partial class frm_BC_ThongKeTaiNanThuongTich : DevExpress.XtraEditors.XtraForm
    {
        public frm_BC_ThongKeTaiNanThuongTich()
        {   
            InitializeComponent();
        }

        private void frm_BC_ThongKeTaiNanThuongTich_Load(object sender, EventArgs e)
        {
            dateTuNgay.DateTime = DateTime.Now;
            dateDenNgay.DateTime = DateTime.Now;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            DateTime _tungay = DungChung.Ham.NgayTu(dateTuNgay.DateTime);
            DateTime _denngay = DungChung.Ham.NgayDen(dateDenNgay.DateTime);
            List<clsThongKe> _listContent = new List<clsThongKe>();
            clsThongKe moi = new clsThongKe();
            if (_denngay >= _tungay)
            {
                List<QLBV.DungChung.Bien.c_TaiNan> _listTaiNan = DungChung.Bien.getDMTaiNan().Where(p => p.Id != 0).ToList();
                var query1 = (from bn in data.BenhNhans
                              join ttbx in data.TTboXungs on bn.MaBNhan equals ttbx.MaBNhan
                              join rv in data.RaViens on bn.MaBNhan equals rv.MaBNhan into kq
                              from kq1 in kq.DefaultIfEmpty()
                              select new
                              {
                                  MaICD = kq1 == null ? "" :( kq1.MaICD.Contains(";") == false ? kq1.MaICD : kq1.MaICD.Substring(0, kq1.MaICD.IndexOf(";"))),
                                  bn.NNhap,
                                  bn.MaBNhan,
                                  bn.Tuoi,
                                  bn.GTinh,
                                  bn.MaBV,
                                  bn.ChuyenKhoa,
                                  ttbx.MaNN,
                                  KetQua = kq1 == null ? "" : kq1.KetQua
                              }).Where(p => p.NNhap >= _tungay && p.NNhap <= _denngay).ToList();

                #region Lấy ICD theo bảng BNKB
                var KBenh = (from bn in data.BNKBs group bn by bn.MaBNhan into kq select new { kq.Key, MaICD = kq.Where(p => p.NgayKham == kq.Max(q => q.NgayKham)).Select(q => q.MaICD).FirstOrDefault() }).ToList();
                var bnkb = (from bn in query1
                            join kbenh in KBenh on bn.MaBNhan equals kbenh.Key
                            into kq
                            from kq1 in kq.DefaultIfEmpty()
                            select new
                            {
                                bn.NNhap,
                                bn.MaBNhan,
                                bn.Tuoi,
                                bn.GTinh,
                                bn.MaBV,
                                bn.ChuyenKhoa,
                                bn.MaNN,
                                bn.KetQua,
                                MaICD = kq1 == null ? "" : (kq1.MaICD.Contains(";") == false ? kq1.MaICD : kq1.MaICD.Substring(0, kq1.MaICD.IndexOf(";"))),
                            }).ToList();
                #endregion lấy ICD theo bảng BNKB
                var query = (from bn in bnkb join tn in _listTaiNan on bn.ChuyenKhoa equals tn.Tenloai select bn).ToList();

                #region Số người bị TNTT
                moi = new clsThongKe();
                moi.Stt = 1;
                moi.TenNhom = "Số người bị TNTT";
                moi.ChiTiet = "";
                moi.Tong_M = query.Count;
                moi.Tong_C = query.Where(p => p.KetQua == "Tử vong").Count();
                moi.TongNu_M = query.Where(p => p.GTinh == 0).Count();
                moi.TongNu_C = query.Where(p => p.GTinh == 0).Where(p => p.KetQua == "Tử vong").Count();

                moi.Duoi4Tuoi_M = query.Where(p => p.Tuoi <= 4).Count();
                moi.Duoi4Tuoi_C = query.Where(p => p.Tuoi <= 4).Where(p => p.KetQua == "Tử vong").Count();
                moi.NuDuoi4Tuoi_M = query.Where(p => p.Tuoi <= 4).Where(p => p.GTinh == 0).Count();
                moi.NuDuoi4Tuoi_C = query.Where(p => p.Tuoi <= 4).Where(p => p.GTinh == 0).Where(p => p.KetQua == "Tử vong").Count();

                moi.Duoi14Tuoi_M = query.Where(p => p.Tuoi > 4 && p.Tuoi <= 14).Count();
                moi.Duoi14Tuoi_C = query.Where(p => p.Tuoi > 4 && p.Tuoi <= 14).Where(p => p.KetQua == "Tử vong").Count();
                moi.NuDuoi14Tuoi_M = query.Where(p => p.Tuoi > 4 && p.Tuoi <= 14).Where(p => p.GTinh == 0).Count();
                moi.NuDuoi14Tuoi_C = query.Where(p => p.Tuoi > 4 && p.Tuoi <= 14).Where(p => p.GTinh == 0).Where(p => p.KetQua == "Tử vong").Count();

                moi.Duoi19Tuoi_M = query.Where(p => p.Tuoi > 14 && p.Tuoi <= 19).Count();
                moi.Duoi19Tuoi_C = query.Where(p => p.Tuoi > 14 && p.Tuoi <= 19).Where(p => p.KetQua == "Tử vong").Count();
                moi.NuDuoi19Tuoi_M = query.Where(p => p.Tuoi > 14 && p.Tuoi <= 19).Where(p => p.GTinh == 0).Count();
                moi.NuDuoi19Tuoi_C = query.Where(p => p.Tuoi > 14 && p.Tuoi <= 19).Where(p => p.GTinh == 0).Where(p => p.KetQua == "Tử vong").Count();

                moi.Duoi60Tuoi_M = query.Where(p => p.Tuoi > 19 && p.Tuoi <= 60).Count();
                moi.Duoi60Tuoi_C = query.Where(p => p.Tuoi > 19 && p.Tuoi <= 60).Where(p => p.KetQua == "Tử vong").Count();
                moi.NuDuoi60Tuoi_M = query.Where(p => p.Tuoi > 19 && p.Tuoi <= 60).Where(p => p.GTinh == 0).Count();
                moi.NuDuoi60Tuoi_C = query.Where(p => p.Tuoi > 19 && p.Tuoi <= 60).Where(p => p.GTinh == 0).Where(p => p.KetQua == "Tử vong").Count();

                moi.Tren60Tuoi_M = query.Where(p => p.Tuoi > 60).Count();
                moi.Tren60Tuoi_C = query.Where(p => p.Tuoi > 60).Where(p => p.KetQua == "Tử vong").Count();
                moi.NuTren60Tuoi_M = query.Where(p => p.Tuoi > 60).Where(p => p.GTinh == 0).Count();
                moi.NuTren60Tuoi_C = query.Where(p => p.Tuoi > 60).Where(p => p.GTinh == 0).Where(p => p.KetQua == "Tử vong").Count();

                _listContent.Add(moi);
                #endregion
                #region Nghề nghiệp
                moi = new clsThongKe();
                moi.Stt = 2;
                moi.TenNhom = "Nghề nghiệp";
                moi.ChiTiet = "";
                _listContent.Add(moi);

                var _listNN1 = (from n in query
                               join nn in data.DmNNs on n.MaNN equals nn.MaNN into a
                               from a1 in a.DefaultIfEmpty()
                              
                               select new
                               {
                                 n.MaNN,
                                 TenNN = a1 == null ? "Khác" : a1.TenNN

                               }).ToList();
                var _listNN = (from n in _listNN1                              
                               group n by new {n.MaNN, n.TenNN} into kq
                               select new
                               {
                                   kq.Key.MaNN,
                                   kq.Key.TenNN
                               }).ToList();
                foreach (var item in _listNN)
                {
                    moi = new clsThongKe();
                    moi.Stt = 2;
                    moi.TenNhom = "Nghề nghiệp";
                    moi.ChiTiet = item.TenNN;
                    moi.Tong_M = query.Where(p=>p.MaNN== item.MaNN).Count();
                    moi.Tong_C = query.Where(p=>p.MaNN== item.MaNN).Where(p => p.KetQua == "Tử vong").Count();
                    moi.TongNu_M = query.Where(p=>p.MaNN== item.MaNN).Where(p => p.GTinh == 0).Count();
                    moi.TongNu_C = query.Where(p=>p.MaNN== item.MaNN).Where(p => p.GTinh == 0).Where(p => p.KetQua == "Tử vong").Count();

                    moi.Duoi4Tuoi_M = query.Where(p=>p.MaNN== item.MaNN).Where(p => p.Tuoi <= 4).Count();
                    moi.Duoi4Tuoi_C = query.Where(p=>p.MaNN== item.MaNN).Where(p => p.Tuoi <= 4).Where(p => p.KetQua == "Tử vong").Count();
                    moi.NuDuoi4Tuoi_M = query.Where(p=>p.MaNN== item.MaNN).Where(p => p.Tuoi <= 4).Where(p => p.GTinh == 0).Count();
                    moi.NuDuoi4Tuoi_C = query.Where(p=>p.MaNN== item.MaNN).Where(p => p.Tuoi <= 4).Where(p => p.GTinh == 0).Where(p => p.KetQua == "Tử vong").Count();

                    moi.Duoi14Tuoi_M = query.Where(p=>p.MaNN== item.MaNN).Where(p => p.Tuoi > 4 && p.Tuoi <= 14).Count();
                    moi.Duoi14Tuoi_C = query.Where(p=>p.MaNN== item.MaNN).Where(p => p.Tuoi > 4 && p.Tuoi <= 14).Where(p => p.KetQua == "Tử vong").Count();
                    moi.NuDuoi14Tuoi_M = query.Where(p=>p.MaNN== item.MaNN).Where(p => p.Tuoi > 4 && p.Tuoi <= 14).Where(p => p.GTinh == 0).Count();
                    moi.NuDuoi14Tuoi_C = query.Where(p=>p.MaNN== item.MaNN).Where(p => p.Tuoi > 4 && p.Tuoi <= 14).Where(p => p.GTinh == 0).Where(p => p.KetQua == "Tử vong").Count();

                    moi.Duoi19Tuoi_M = query.Where(p=>p.MaNN== item.MaNN).Where(p => p.Tuoi > 14 && p.Tuoi <= 19).Count();
                    moi.Duoi19Tuoi_C = query.Where(p=>p.MaNN== item.MaNN).Where(p => p.Tuoi > 14 && p.Tuoi <= 19).Where(p => p.KetQua == "Tử vong").Count();
                    moi.NuDuoi19Tuoi_M = query.Where(p=>p.MaNN== item.MaNN).Where(p => p.Tuoi > 14 && p.Tuoi <= 19).Where(p => p.GTinh == 0).Count();
                    moi.NuDuoi19Tuoi_C = query.Where(p=>p.MaNN== item.MaNN).Where(p => p.Tuoi > 14 && p.Tuoi <= 19).Where(p => p.GTinh == 0).Where(p => p.KetQua == "Tử vong").Count();

                    moi.Duoi60Tuoi_M = query.Where(p=>p.MaNN== item.MaNN).Where(p => p.Tuoi > 19 && p.Tuoi <= 60).Count();
                    moi.Duoi60Tuoi_C = query.Where(p=>p.MaNN== item.MaNN).Where(p => p.Tuoi > 19 && p.Tuoi <= 60).Where(p => p.KetQua == "Tử vong").Count();
                    moi.NuDuoi60Tuoi_M = query.Where(p=>p.MaNN== item.MaNN).Where(p => p.Tuoi > 19 && p.Tuoi <= 60).Where(p => p.GTinh == 0).Count();
                    moi.NuDuoi60Tuoi_C = query.Where(p=>p.MaNN== item.MaNN).Where(p => p.Tuoi > 19 && p.Tuoi <= 60).Where(p => p.GTinh == 0).Where(p => p.KetQua == "Tử vong").Count();

                    moi.Tren60Tuoi_M = query.Where(p=>p.MaNN== item.MaNN).Where(p => p.Tuoi > 60).Count();
                    moi.Tren60Tuoi_C = query.Where(p=>p.MaNN== item.MaNN).Where(p => p.Tuoi > 60).Where(p => p.KetQua == "Tử vong").Count();
                    moi.NuTren60Tuoi_M = query.Where(p=>p.MaNN== item.MaNN).Where(p => p.Tuoi > 60).Where(p => p.GTinh == 0).Count();
                    moi.NuTren60Tuoi_C = query.Where(p=>p.MaNN== item.MaNN).Where(p => p.Tuoi > 60).Where(p => p.GTinh == 0).Where(p => p.KetQua == "Tử vong").Count();
                    _listContent.Add(moi);
                }
                #endregion
                #region Bộ phận bị thương
                moi = new clsThongKe();
                moi.Stt = 3;
                moi.TenNhom = "Bộ phận bị thương - theo ICD 10";
                moi.ChiTiet = "";
                _listContent.Add(moi);

                var q3_a = (from n in query
                            select new
                            {
                                n.MaICD,
                                n.KetQua,
                                n.GTinh,
                                n.Tuoi
                            }).Where(p => p.MaICD != null)
                            .Where(p => p.MaICD.Contains("S00") || p.MaICD.Contains("S01") || p.MaICD.Contains("S02") || p.MaICD.Contains("S03") ||
                                   p.MaICD.Contains("S04") || p.MaICD.Contains("S05") || p.MaICD.Contains("S06") || p.MaICD.Contains("S07") || p.MaICD.Contains("S08") ||
                                   p.MaICD.Contains("S09") || p.MaICD.Contains("S10") || p.MaICD.Contains("S11") || p.MaICD.Contains("S12") || p.MaICD.Contains("S13") ||
                                   p.MaICD.Contains("S14") || p.MaICD.Contains("S15") || p.MaICD.Contains("S16") || p.MaICD.Contains("S17") || p.MaICD.Contains("S18") || p.MaICD.Contains("S19")).ToList();
                moi = new clsThongKe();
                moi.Stt = 3;
                moi.TenNhom = "Bộ phận bị thương - theo ICD 10";
                moi.ChiTiet = "Đầu, mặt, cổ (S00-S19)";
                moi.Tong_M = q3_a.Count;
                moi.Tong_C = q3_a.Where(p => p.KetQua == "Tử vong").Count();
                moi.TongNu_M = q3_a.Where(p => p.GTinh == 0).Count();
                moi.TongNu_C = q3_a.Where(p => p.KetQua == "Tử vong").Where(p => p.GTinh == 0).Count();
                moi.Duoi4Tuoi_M = q3_a.Where(p => p.Tuoi <= 4).Count();
                moi.Duoi4Tuoi_C = q3_a.Where(p => p.Tuoi <= 4).Where(p => p.KetQua == "Tử vong").Count();
                moi.NuDuoi4Tuoi_M = q3_a.Where(p => p.Tuoi <= 4).Where(p => p.GTinh == 0).Count();
                moi.NuDuoi4Tuoi_C = q3_a.Where(p => p.Tuoi <= 4).Where(p => p.KetQua == "Tử vong").Where(p => p.GTinh == 0).Count();
                moi.Duoi14Tuoi_M = q3_a.Where(p => p.Tuoi > 4 && p.Tuoi <= 14).Count();
                moi.Duoi14Tuoi_C = q3_a.Where(p => p.Tuoi > 4 && p.Tuoi <= 14).Where(p => p.KetQua == "Tử vong").Count();
                moi.NuDuoi14Tuoi_M = q3_a.Where(p => p.Tuoi > 4 && p.Tuoi <= 14).Where(p => p.GTinh == 0).Count();
                moi.NuDuoi14Tuoi_C = q3_a.Where(p => p.Tuoi > 4 && p.Tuoi <= 14).Where(p => p.KetQua == "Tử vong").Where(p => p.GTinh == 0).Count();
                moi.Duoi19Tuoi_M = q3_a.Where(p => p.Tuoi > 14 && p.Tuoi <= 19).Count();
                moi.Duoi19Tuoi_C = q3_a.Where(p => p.Tuoi > 14 && p.Tuoi <= 19).Where(p => p.KetQua == "Tử vong").Count();
                moi.NuDuoi19Tuoi_M = q3_a.Where(p => p.Tuoi > 14 && p.Tuoi <= 19).Where(p => p.GTinh == 0).Count();
                moi.NuDuoi19Tuoi_C = q3_a.Where(p => p.Tuoi > 14 && p.Tuoi <= 19).Where(p => p.KetQua == "Tử vong").Where(p => p.GTinh == 0).Count();
                moi.Duoi60Tuoi_M = q3_a.Where(p => p.Tuoi > 19 && p.Tuoi <= 60).Count();
                moi.Duoi60Tuoi_C = q3_a.Where(p => p.Tuoi > 19 && p.Tuoi <= 60).Where(p => p.KetQua == "Tử vong").Count();
                moi.NuDuoi60Tuoi_M = q3_a.Where(p => p.Tuoi > 19 && p.Tuoi <= 60).Where(p => p.GTinh == 0).Count();
                moi.NuDuoi60Tuoi_C = q3_a.Where(p => p.Tuoi > 19 && p.Tuoi <= 60).Where(p => p.KetQua == "Tử vong").Where(p => p.GTinh == 0).Count();
                moi.Tren60Tuoi_M = q3_a.Where(p => p.Tuoi > 60).Count();
                moi.Tren60Tuoi_C = q3_a.Where(p => p.Tuoi > 60).Where(p => p.KetQua == "Tử vong").Count();
                moi.NuTren60Tuoi_M = q3_a.Where(p => p.Tuoi > 60).Where(p => p.GTinh == 0).Count();
                moi.NuTren60Tuoi_C = q3_a.Where(p => p.Tuoi > 60).Where(p => p.KetQua == "Tử vong").Count();
                _listContent.Add(moi);

                var q3_b = (from n in query
                            select new
                            {
                                n.MaICD,
                                n.KetQua,
                                n.GTinh,
                                n.Tuoi
                            }).Where(p => p.MaICD != null)
                            .Where(p => p.MaICD.Contains("S20") || p.MaICD.Contains("S21") || p.MaICD.Contains("S22") || p.MaICD.Contains("S23") ||
                                   p.MaICD.Contains("S24") || p.MaICD.Contains("S25") || p.MaICD.Contains("S26") || p.MaICD.Contains("S27") || p.MaICD.Contains("S28") ||
                                   p.MaICD.Contains("S29") || p.MaICD.Contains("S30") || p.MaICD.Contains("S31") || p.MaICD.Contains("S32") || p.MaICD.Contains("S33") ||
                                   p.MaICD.Contains("S34") || p.MaICD.Contains("S35") || p.MaICD.Contains("S36") || p.MaICD.Contains("S37") || p.MaICD.Contains("S38") || p.MaICD.Contains("S39")).ToList();

                moi = new clsThongKe();
                moi.Stt = 3;
                moi.TenNhom = "Bộ phận bị thương - theo ICD 10";
                moi.ChiTiet = "Thân mình (S20-S39)";
                moi.Tong_M = q3_b.Count;
                moi.Tong_C = q3_b.Where(p => p.KetQua == "Tử vong").Count();
                moi.TongNu_M = q3_b.Where(p => p.GTinh == 0).Count();
                moi.TongNu_C = q3_b.Where(p => p.KetQua == "Tử vong").Where(p => p.GTinh == 0).Count();
                moi.Duoi4Tuoi_M = q3_b.Where(p => p.Tuoi <= 4).Count();
                moi.Duoi4Tuoi_C = q3_b.Where(p => p.Tuoi <= 4).Where(p => p.KetQua == "Tử vong").Count();
                moi.NuDuoi4Tuoi_M = q3_b.Where(p => p.Tuoi <= 4).Where(p => p.GTinh == 0).Count();
                moi.NuDuoi4Tuoi_C = q3_b.Where(p => p.Tuoi <= 4).Where(p => p.KetQua == "Tử vong").Where(p => p.GTinh == 0).Count();
                moi.Duoi14Tuoi_M = q3_b.Where(p => p.Tuoi > 4 && p.Tuoi <= 14).Count();
                moi.Duoi14Tuoi_C = q3_b.Where(p => p.Tuoi > 4 && p.Tuoi <= 14).Where(p => p.KetQua == "Tử vong").Count();
                moi.NuDuoi14Tuoi_M = q3_b.Where(p => p.Tuoi > 4 && p.Tuoi <= 14).Where(p => p.GTinh == 0).Count();
                moi.NuDuoi14Tuoi_C = q3_b.Where(p => p.Tuoi > 4 && p.Tuoi <= 14).Where(p => p.KetQua == "Tử vong").Where(p => p.GTinh == 0).Count();
                moi.Duoi19Tuoi_M = q3_b.Where(p => p.Tuoi > 14 && p.Tuoi <= 19).Count();
                moi.Duoi19Tuoi_C = q3_b.Where(p => p.Tuoi > 14 && p.Tuoi <= 19).Where(p => p.KetQua == "Tử vong").Count();
                moi.NuDuoi19Tuoi_M = q3_b.Where(p => p.Tuoi > 14 && p.Tuoi <= 19).Where(p => p.GTinh == 0).Count();
                moi.NuDuoi19Tuoi_C = q3_b.Where(p => p.Tuoi > 14 && p.Tuoi <= 19).Where(p => p.KetQua == "Tử vong").Where(p => p.GTinh == 0).Count();
                moi.Duoi60Tuoi_M = q3_b.Where(p => p.Tuoi > 19 && p.Tuoi <= 60).Count();
                moi.Duoi60Tuoi_C = q3_b.Where(p => p.Tuoi > 19 && p.Tuoi <= 60).Where(p => p.KetQua == "Tử vong").Count();
                moi.NuDuoi60Tuoi_M = q3_b.Where(p => p.Tuoi > 19 && p.Tuoi <= 60).Where(p => p.GTinh == 0).Count();
                moi.NuDuoi60Tuoi_C = q3_b.Where(p => p.Tuoi > 19 && p.Tuoi <= 60).Where(p => p.KetQua == "Tử vong").Where(p => p.GTinh == 0).Count();
                moi.Tren60Tuoi_M = q3_b.Where(p => p.Tuoi > 60).Count();
                moi.Tren60Tuoi_C = q3_b.Where(p => p.Tuoi > 60).Where(p => p.KetQua == "Tử vong").Count();
                moi.NuTren60Tuoi_M = q3_b.Where(p => p.Tuoi > 60).Where(p => p.GTinh == 0).Count();
                moi.NuTren60Tuoi_C = q3_b.Where(p => p.Tuoi > 60).Where(p => p.KetQua == "Tử vong").Count();
                _listContent.Add(moi);

                var q3_c = (from n in query
                            select new
                            {
                                n.MaICD,
                                n.KetQua,
                                n.GTinh,
                                n.Tuoi
                            }).Where(p => p.MaICD != null)
                            .Where(p => p.MaICD.Contains("S40") || p.MaICD.Contains("S41") || p.MaICD.Contains("S42") || p.MaICD.Contains("S43") || p.MaICD.Contains("S44") ||
                                   p.MaICD.Contains("S45") || p.MaICD.Contains("S46") || p.MaICD.Contains("S47") || p.MaICD.Contains("S48") || p.MaICD.Contains("S49") ||
                                   p.MaICD.Contains("S50") || p.MaICD.Contains("S51") || p.MaICD.Contains("S52") || p.MaICD.Contains("S53") || p.MaICD.Contains("S54") ||
                                   p.MaICD.Contains("S55") || p.MaICD.Contains("S56") || p.MaICD.Contains("S57") || p.MaICD.Contains("S58") || p.MaICD.Contains("S59") ||
                                   p.MaICD.Contains("S60") || p.MaICD.Contains("S61") || p.MaICD.Contains("S62") || p.MaICD.Contains("S63") || p.MaICD.Contains("S64") ||
                                   p.MaICD.Contains("S65") || p.MaICD.Contains("S66") || p.MaICD.Contains("S67") || p.MaICD.Contains("S68") || p.MaICD.Contains("S69") ||
                                   p.MaICD.Contains("S70") || p.MaICD.Contains("S71") || p.MaICD.Contains("S72") || p.MaICD.Contains("S73") || p.MaICD.Contains("S74") ||
                                   p.MaICD.Contains("S75") || p.MaICD.Contains("S76") || p.MaICD.Contains("S77") || p.MaICD.Contains("S78") || p.MaICD.Contains("S79") ||
                                   p.MaICD.Contains("S80") || p.MaICD.Contains("S81") || p.MaICD.Contains("S82") || p.MaICD.Contains("S83") || p.MaICD.Contains("S84") ||
                                   p.MaICD.Contains("S85") || p.MaICD.Contains("S86") || p.MaICD.Contains("S87") || p.MaICD.Contains("S88") || p.MaICD.Contains("S89") ||
                                   p.MaICD.Contains("S90") || p.MaICD.Contains("S91") || p.MaICD.Contains("S92") || p.MaICD.Contains("S93") || p.MaICD.Contains("S94") ||
                                   p.MaICD.Contains("S95") || p.MaICD.Contains("S96") || p.MaICD.Contains("S97") || p.MaICD.Contains("S98") || p.MaICD.Contains("S99")).ToList();

                moi = new clsThongKe();
                moi.Stt = 3;
                moi.TenNhom = "Bộ phận bị thương - theo ICD 10";
                moi.ChiTiet = "Chi (S40-S99)";
                moi.Tong_M = q3_c.Count;
                moi.Tong_C = q3_c.Where(p => p.KetQua == "Tử vong").Count();
                moi.TongNu_M = q3_c.Where(p => p.GTinh == 0).Count();
                moi.TongNu_C = q3_c.Where(p => p.KetQua == "Tử vong").Where(p => p.GTinh == 0).Count();
                moi.Duoi4Tuoi_M = q3_c.Where(p => p.Tuoi <= 4).Count();
                moi.Duoi4Tuoi_C = q3_c.Where(p => p.Tuoi <= 4).Where(p => p.KetQua == "Tử vong").Count();
                moi.NuDuoi4Tuoi_M = q3_c.Where(p => p.Tuoi <= 4).Where(p => p.GTinh == 0).Count();
                moi.NuDuoi4Tuoi_C = q3_c.Where(p => p.Tuoi <= 4).Where(p => p.KetQua == "Tử vong").Where(p => p.GTinh == 0).Count();
                moi.Duoi14Tuoi_M = q3_c.Where(p => p.Tuoi > 4 && p.Tuoi <= 14).Count();
                moi.Duoi14Tuoi_C = q3_c.Where(p => p.Tuoi > 4 && p.Tuoi <= 14).Where(p => p.KetQua == "Tử vong").Count();
                moi.NuDuoi14Tuoi_M = q3_c.Where(p => p.Tuoi > 4 && p.Tuoi <= 14).Where(p => p.GTinh == 0).Count();
                moi.NuDuoi14Tuoi_C = q3_c.Where(p => p.Tuoi > 4 && p.Tuoi <= 14).Where(p => p.KetQua == "Tử vong").Where(p => p.GTinh == 0).Count();
                moi.Duoi19Tuoi_M = q3_c.Where(p => p.Tuoi > 14 && p.Tuoi <= 19).Count();
                moi.Duoi19Tuoi_C = q3_c.Where(p => p.Tuoi > 14 && p.Tuoi <= 19).Where(p => p.KetQua == "Tử vong").Count();
                moi.NuDuoi19Tuoi_M = q3_c.Where(p => p.Tuoi > 14 && p.Tuoi <= 19).Where(p => p.GTinh == 0).Count();
                moi.NuDuoi19Tuoi_C = q3_c.Where(p => p.Tuoi > 14 && p.Tuoi <= 19).Where(p => p.KetQua == "Tử vong").Where(p => p.GTinh == 0).Count();
                moi.Duoi60Tuoi_M = q3_c.Where(p => p.Tuoi > 19 && p.Tuoi <= 60).Count();
                moi.Duoi60Tuoi_C = q3_c.Where(p => p.Tuoi > 19 && p.Tuoi <= 60).Where(p => p.KetQua == "Tử vong").Count();
                moi.NuDuoi60Tuoi_M = q3_c.Where(p => p.Tuoi > 19 && p.Tuoi <= 60).Where(p => p.GTinh == 0).Count();
                moi.NuDuoi60Tuoi_C = q3_c.Where(p => p.Tuoi > 19 && p.Tuoi <= 60).Where(p => p.KetQua == "Tử vong").Where(p => p.GTinh == 0).Count();
                moi.Tren60Tuoi_M = q3_c.Where(p => p.Tuoi > 60).Count();
                moi.Tren60Tuoi_C = q3_c.Where(p => p.Tuoi > 60).Where(p => p.KetQua == "Tử vong").Count();
                moi.NuTren60Tuoi_M = q3_c.Where(p => p.Tuoi > 60).Where(p => p.GTinh == 0).Count();
                moi.NuTren60Tuoi_C = q3_c.Where(p => p.Tuoi > 60).Where(p => p.KetQua == "Tử vong").Count();
                _listContent.Add(moi);

                var q3_d = (from n in query
                            select new
                            {
                                n.MaICD,
                                n.KetQua,
                                n.GTinh,
                                n.Tuoi
                            }).Where(p => p.MaICD != null)
                            .Where(p => p.MaICD.Contains("T00") || p.MaICD.Contains("T01") || p.MaICD.Contains("T02") || p.MaICD.Contains("T03") ||
                                   p.MaICD.Contains("T04") || p.MaICD.Contains("T05") || p.MaICD.Contains("T06") || p.MaICD.Contains("T07")).ToList();

                moi = new clsThongKe();
                moi.Stt = 3;
                moi.TenNhom = "Bộ phận bị thương - theo ICD 10";
                moi.ChiTiet = "Đa chấn thương (T00-T07)";
                moi.Tong_M = q3_d.Count;
                moi.Tong_C = q3_d.Where(p => p.KetQua == "Tử vong").Count();
                moi.TongNu_M = q3_d.Where(p => p.GTinh == 0).Count();
                moi.TongNu_C = q3_d.Where(p => p.KetQua == "Tử vong").Where(p => p.GTinh == 0).Count();
                moi.Duoi4Tuoi_M = q3_d.Where(p => p.Tuoi <= 4).Count();
                moi.Duoi4Tuoi_C = q3_d.Where(p => p.Tuoi <= 4).Where(p => p.KetQua == "Tử vong").Count();
                moi.NuDuoi4Tuoi_M = q3_d.Where(p => p.Tuoi <= 4).Where(p => p.GTinh == 0).Count();
                moi.NuDuoi4Tuoi_C = q3_d.Where(p => p.Tuoi <= 4).Where(p => p.KetQua == "Tử vong").Where(p => p.GTinh == 0).Count();
                moi.Duoi14Tuoi_M = q3_d.Where(p => p.Tuoi > 4 && p.Tuoi <= 14).Count();
                moi.Duoi14Tuoi_C = q3_d.Where(p => p.Tuoi > 4 && p.Tuoi <= 14).Where(p => p.KetQua == "Tử vong").Count();
                moi.NuDuoi14Tuoi_M = q3_d.Where(p => p.Tuoi > 4 && p.Tuoi <= 14).Where(p => p.GTinh == 0).Count();
                moi.NuDuoi14Tuoi_C = q3_d.Where(p => p.Tuoi > 4 && p.Tuoi <= 14).Where(p => p.KetQua == "Tử vong").Where(p => p.GTinh == 0).Count();
                moi.Duoi19Tuoi_M = q3_d.Where(p => p.Tuoi > 14 && p.Tuoi <= 19).Count();
                moi.Duoi19Tuoi_C = q3_d.Where(p => p.Tuoi > 14 && p.Tuoi <= 19).Where(p => p.KetQua == "Tử vong").Count();
                moi.NuDuoi19Tuoi_M = q3_d.Where(p => p.Tuoi > 14 && p.Tuoi <= 19).Where(p => p.GTinh == 0).Count();
                moi.NuDuoi19Tuoi_C = q3_d.Where(p => p.Tuoi > 14 && p.Tuoi <= 19).Where(p => p.KetQua == "Tử vong").Where(p => p.GTinh == 0).Count();
                moi.Duoi60Tuoi_M = q3_d.Where(p => p.Tuoi > 19 && p.Tuoi <= 60).Count();
                moi.Duoi60Tuoi_C = q3_d.Where(p => p.Tuoi > 19 && p.Tuoi <= 60).Where(p => p.KetQua == "Tử vong").Count();
                moi.NuDuoi60Tuoi_M = q3_d.Where(p => p.Tuoi > 19 && p.Tuoi <= 60).Where(p => p.GTinh == 0).Count();
                moi.NuDuoi60Tuoi_C = q3_d.Where(p => p.Tuoi > 19 && p.Tuoi <= 60).Where(p => p.KetQua == "Tử vong").Where(p => p.GTinh == 0).Count();
                moi.Tren60Tuoi_M = q3_d.Where(p => p.Tuoi > 60).Count();
                moi.Tren60Tuoi_C = q3_d.Where(p => p.Tuoi > 60).Where(p => p.KetQua == "Tử vong").Count();
                moi.NuTren60Tuoi_M = q3_d.Where(p => p.Tuoi > 60).Where(p => p.GTinh == 0).Count();
                moi.NuTren60Tuoi_C = q3_d.Where(p => p.Tuoi > 60).Where(p => p.KetQua == "Tử vong").Count();
                _listContent.Add(moi);

                moi = new clsThongKe();
                moi.Stt = 3;
                moi.TenNhom = "Bộ phận bị thương - theo ICD 10";
                moi.ChiTiet = "Khác";
                _listContent.Add(moi);
                #endregion
                #region Nguyên nhân TNTT-theo ICD 10
                moi = new clsThongKe();
                moi.Stt = 4;
                moi.TenNhom = "Nguyên nhân TNTT - theo ICD 10";
                moi.ChiTiet = "";
                _listContent.Add(moi);

                #region Biến
                int Tong_M = 0;
                int Tong_C = 0;
                int TongNu_M = 0;
                int TongNu_C = 0;
                int Duoi4_M = 0;
                int Duoi4_C = 0;
                int NuDuoi4_M = 0;
                int NuDuoi4_C = 0;
                int Duoi14_M = 0;
                int Duoi14_C = 0;
                int NuDuoi14_M = 0;
                int NuDuoi14_C = 0;
                int Duoi19_M = 0;
                int Duoi19_C = 0;
                int NuDuoi19_M = 0;
                int NuDuoi19_C = 0;
                int Duoi60_M = 0;
                int Duoi60_C = 0;
                int NuDuoi60_M = 0;
                int NuDuoi60_C = 0;
                int Tren60_M = 0;
                int tren60_C = 0;
                int NuTren60_M = 0;
                int NuTren60_C = 0;
                #endregion

                for (int i = 0; i <= 99; i++)
                {
                    string icd = null;
                    if (i < 10)
                    {
                        icd = "V0" + i;
                    }
                    if (i >= 10)
                    {
                        icd = "V" + i;
                    }
                    Tong_M += query.Where(p => p.MaICD != null && p.MaICD.Contains(icd)).Count();
                    Tong_C += query.Where(p => p.MaICD != null && p.MaICD.Contains(icd)).Where(p => p.KetQua == "Tử vong").Count();
                    TongNu_M += query.Where(p => p.MaICD != null && p.MaICD.Contains(icd)).Where(p => p.GTinh == 0).Count();
                    TongNu_C += query.Where(p => p.MaICD != null && p.MaICD.Contains(icd)).Where(p => p.GTinh == 0).Where(p => p.KetQua == "Tử vong").Count();
                    Duoi4_M += query.Where(p => p.MaICD != null && p.MaICD.Contains(icd)).Where(p => p.Tuoi <= 4).Count();
                    Duoi4_C += query.Where(p => p.MaICD != null && p.MaICD.Contains(icd)).Where(p => p.Tuoi <= 4).Where(p => p.KetQua == "Tử vong").Count();
                    NuDuoi4_M += query.Where(p => p.MaICD != null && p.MaICD.Contains(icd)).Where(p => p.GTinh == 0).Where(p => p.Tuoi <= 4).Count();
                    NuDuoi4_C += query.Where(p => p.MaICD != null && p.MaICD.Contains(icd)).Where(p => p.GTinh == 0).Where(p => p.Tuoi <= 4).Where(p => p.KetQua == "Tử vong").Count();
                    Duoi14_M += query.Where(p => p.MaICD != null && p.MaICD.Contains(icd)).Where(p => p.Tuoi > 4 && p.Tuoi <= 14).Count();
                    Duoi14_C += query.Where(p => p.MaICD != null && p.MaICD.Contains(icd)).Where(p => p.Tuoi > 4 && p.Tuoi <= 14).Where(p => p.KetQua == "Tử vong").Count();
                    NuDuoi14_M += query.Where(p => p.MaICD != null && p.MaICD.Contains(icd)).Where(p => p.GTinh == 0).Where(p => p.Tuoi > 4 && p.Tuoi <= 14).Count();
                    NuDuoi14_C += query.Where(p => p.MaICD != null && p.MaICD.Contains(icd)).Where(p => p.GTinh == 0).Where(p => p.Tuoi > 4 && p.Tuoi <= 14).Where(p => p.KetQua == "Tử vong").Count();
                    Duoi19_M += query.Where(p => p.MaICD != null && p.MaICD.Contains(icd)).Where(p => p.Tuoi > 14 && p.Tuoi <= 19).Count();
                    Duoi19_C += query.Where(p => p.MaICD != null && p.MaICD.Contains(icd)).Where(p => p.Tuoi > 14 && p.Tuoi <= 19).Where(p => p.KetQua == "Tử vong").Count();
                    NuDuoi19_M += query.Where(p => p.MaICD != null && p.MaICD.Contains(icd)).Where(p => p.GTinh == 0).Where(p => p.Tuoi > 14 && p.Tuoi <= 19).Count();
                    NuDuoi19_C += query.Where(p => p.MaICD != null && p.MaICD.Contains(icd)).Where(p => p.GTinh == 0).Where(p => p.Tuoi > 14 && p.Tuoi <= 19).Where(p => p.KetQua == "Tử vong").Count();
                    Duoi60_M += query.Where(p => p.MaICD != null && p.MaICD.Contains(icd)).Where(p => p.Tuoi > 19 && p.Tuoi <= 60).Count();
                    Duoi60_C += query.Where(p => p.MaICD != null && p.MaICD.Contains(icd)).Where(p => p.Tuoi > 19 && p.Tuoi <= 60).Where(p => p.KetQua == "Tử vong").Count();
                    NuDuoi60_M += query.Where(p => p.MaICD != null && p.MaICD.Contains(icd)).Where(p => p.GTinh == 0).Where(p => p.Tuoi > 19 && p.Tuoi <= 60).Count();
                    NuDuoi60_C += query.Where(p => p.MaICD != null && p.MaICD.Contains(icd)).Where(p => p.GTinh == 0).Where(p => p.Tuoi > 19 && p.Tuoi <= 60).Where(p => p.KetQua == "Tử vong").Count();
                    Tren60_M += query.Where(p => p.MaICD != null && p.MaICD.Contains(icd)).Where(p => p.Tuoi > 60).ToList().Count;
                    tren60_C += query.Where(p => p.MaICD != null && p.MaICD.Contains(icd)).Where(p => p.Tuoi > 60).Where(p => p.KetQua == "Tử vong").Count();
                    NuTren60_M += query.Where(p => p.MaICD != null && p.MaICD.Contains(icd)).Where(p => p.GTinh == 0).Where(p => p.Tuoi > 60).Count();
                    NuTren60_C += query.Where(p => p.MaICD != null && p.MaICD.Contains(icd)).Where(p => p.GTinh == 0).Where(p => p.Tuoi > 60).Where(p => p.KetQua == "Tử vong").Count();
                }

                moi = new clsThongKe();
                moi.Stt = 4;
                moi.TenNhom = "Nguyên nhân TNTT - theo ICD 10";
                moi.ChiTiet = "Tai nạn giao thông (V01-V99)";
                moi.Tong_M = Tong_M;
                moi.Tong_C = Tong_C;
                moi.TongNu_M = TongNu_M;
                moi.TongNu_C = TongNu_C;
                moi.Duoi4Tuoi_M = Duoi4_M;
                moi.Duoi4Tuoi_C = Duoi4_C;
                moi.NuDuoi4Tuoi_M = NuDuoi4_M;
                moi.NuDuoi4Tuoi_C = NuDuoi4_C;
                moi.Duoi14Tuoi_M = Duoi14_M;
                moi.Duoi14Tuoi_C = Duoi14_C;
                moi.NuDuoi14Tuoi_M = NuDuoi14_M;
                moi.NuDuoi14Tuoi_C = NuDuoi14_C;
                moi.Duoi19Tuoi_M = Duoi19_M;
                moi.Duoi19Tuoi_C = Duoi19_C;
                moi.NuDuoi19Tuoi_M = NuDuoi19_M;
                moi.NuDuoi19Tuoi_C = NuDuoi19_C;
                moi.Duoi60Tuoi_M = Duoi60_M;
                moi.Duoi60Tuoi_C = Duoi60_C;
                moi.NuDuoi60Tuoi_M = NuDuoi60_M;
                moi.NuDuoi60Tuoi_C = NuDuoi60_C;
                moi.Tren60Tuoi_M = Tren60_M;
                moi.Tren60Tuoi_C = tren60_C;
                moi.NuTren60Tuoi_M = NuTren60_M;
                moi.NuTren60Tuoi_C = NuTren60_C;
                _listContent.Add(moi);

                var q4_b = (from n in query
                            select new
                            {
                                n.MaICD,
                                n.KetQua,
                                n.GTinh,
                                n.Tuoi
                            }).Where(p => p.MaICD != null)
                            .Where(p => p.MaICD.Contains("W20") || p.MaICD.Contains("W21") || p.MaICD.Contains("W22") || p.MaICD.Contains("W23") || p.MaICD.Contains("W39") ||
                                   p.MaICD.Contains("W24") || p.MaICD.Contains("W25") || p.MaICD.Contains("W26") || p.MaICD.Contains("W27") || p.MaICD.Contains("W28") ||
                                   p.MaICD.Contains("W29") || p.MaICD.Contains("W30") || p.MaICD.Contains("W31") || p.MaICD.Contains("W32") || p.MaICD.Contains("W33") ||
                                   p.MaICD.Contains("W34") || p.MaICD.Contains("W35") || p.MaICD.Contains("W36") || p.MaICD.Contains("W37") || p.MaICD.Contains("W38") ||
                                   p.MaICD.Contains("W39") || p.MaICD.Contains("W40") || p.MaICD.Contains("W41") || p.MaICD.Contains("W42") || p.MaICD.Contains("W43") || p.MaICD.Contains("W44") ||
                                   p.MaICD.Contains("W45") || p.MaICD.Contains("W46") || p.MaICD.Contains("W47") || p.MaICD.Contains("W48") || p.MaICD.Contains("W49")).ToList();

                moi = new clsThongKe();
                moi.Stt = 4;
                moi.TenNhom = "Nguyên nhân TNTT - theo ICD 10";
                moi.ChiTiet = "Tai nạn lao động (W20-W49)";
                moi.Tong_M = q4_b.Count;
                moi.Tong_C = q4_b.Where(p => p.KetQua == "Tử vong").Count();
                moi.TongNu_M = q4_b.Where(p => p.GTinh == 0).Count();
                moi.TongNu_C = q4_b.Where(p => p.KetQua == "Tử vong").Where(p => p.GTinh == 0).Count();
                moi.Duoi4Tuoi_M = q4_b.Where(p => p.Tuoi <= 4).Count();
                moi.Duoi4Tuoi_C = q4_b.Where(p => p.Tuoi <= 4).Where(p => p.KetQua == "Tử vong").Count();
                moi.NuDuoi4Tuoi_M = q4_b.Where(p => p.Tuoi <= 4).Where(p => p.GTinh == 0).Count();
                moi.NuDuoi4Tuoi_C = q4_b.Where(p => p.Tuoi <= 4).Where(p => p.KetQua == "Tử vong").Where(p => p.GTinh == 0).Count();
                moi.Duoi14Tuoi_M = q4_b.Where(p => p.Tuoi > 4 && p.Tuoi <= 14).Count();
                moi.Duoi14Tuoi_C = q4_b.Where(p => p.Tuoi > 4 && p.Tuoi <= 14).Where(p => p.KetQua == "Tử vong").Count();
                moi.NuDuoi14Tuoi_M = q4_b.Where(p => p.Tuoi > 4 && p.Tuoi <= 14).Where(p => p.GTinh == 0).Count();
                moi.NuDuoi14Tuoi_C = q4_b.Where(p => p.Tuoi > 4 && p.Tuoi <= 14).Where(p => p.KetQua == "Tử vong").Where(p => p.GTinh == 0).Count();
                moi.Duoi19Tuoi_M = q4_b.Where(p => p.Tuoi > 14 && p.Tuoi <= 19).Count();
                moi.Duoi19Tuoi_C = q4_b.Where(p => p.Tuoi > 14 && p.Tuoi <= 19).Where(p => p.KetQua == "Tử vong").Count();
                moi.NuDuoi19Tuoi_M = q4_b.Where(p => p.Tuoi > 14 && p.Tuoi <= 19).Where(p => p.GTinh == 0).Count();
                moi.NuDuoi19Tuoi_C = q4_b.Where(p => p.Tuoi > 14 && p.Tuoi <= 19).Where(p => p.KetQua == "Tử vong").Where(p => p.GTinh == 0).Count();
                moi.Duoi60Tuoi_M = q4_b.Where(p => p.Tuoi > 19 && p.Tuoi <= 60).Count();
                moi.Duoi60Tuoi_C = q4_b.Where(p => p.Tuoi > 19 && p.Tuoi <= 60).Where(p => p.KetQua == "Tử vong").Count();
                moi.NuDuoi60Tuoi_M = q4_b.Where(p => p.Tuoi > 19 && p.Tuoi <= 60).Where(p => p.GTinh == 0).Count();
                moi.NuDuoi60Tuoi_C = q4_b.Where(p => p.Tuoi > 19 && p.Tuoi <= 60).Where(p => p.KetQua == "Tử vong").Where(p => p.GTinh == 0).Count();
                moi.Tren60Tuoi_M = q4_b.Where(p => p.Tuoi > 60).Count();
                moi.Tren60Tuoi_C = q4_b.Where(p => p.Tuoi > 60).Where(p => p.KetQua == "Tử vong").Count();
                moi.NuTren60Tuoi_M = q4_b.Where(p => p.Tuoi > 60).Where(p => p.GTinh == 0).Count();
                moi.NuTren60Tuoi_C = q4_b.Where(p => p.Tuoi > 60).Where(p => p.GTinh == 0).Where(p => p.KetQua == "Tử vong").Count();
                _listContent.Add(moi);

                var q4_c = (from n in query
                            select new
                            {
                                n.MaICD,
                                n.KetQua,
                                n.GTinh,
                                n.Tuoi
                            }).Where(p => p.MaICD != null)
                            .Where(p => p.MaICD.Contains("W50") || p.MaICD.Contains("W51") || p.MaICD.Contains("W52") || p.MaICD.Contains("W53") || p.MaICD.Contains("W54") ||
                                   p.MaICD.Contains("W55") || p.MaICD.Contains("W56") || p.MaICD.Contains("W57") || p.MaICD.Contains("W58") || p.MaICD.Contains("W59") ||
                                   p.MaICD.Contains("W60") || p.MaICD.Contains("W61") || p.MaICD.Contains("W62") || p.MaICD.Contains("W63") || p.MaICD.Contains("W64")).ToList();

                moi = new clsThongKe();
                moi.Stt = 4;
                moi.TenNhom = "Nguyên nhân TNTT - theo ICD 10";
                moi.ChiTiet = "Súc vật, động vật: cắn, đốt, húc(W50-W64)";
                moi.Tong_M = q4_c.Count;
                moi.Tong_C = q4_c.Where(p => p.KetQua == "Tử vong").Count();
                moi.TongNu_M = q4_c.Where(p => p.GTinh == 0).Count();
                moi.TongNu_C = q4_c.Where(p => p.KetQua == "Tử vong").Where(p => p.GTinh == 0).Count();
                moi.Duoi4Tuoi_M = q4_c.Where(p => p.Tuoi <= 4).Count();
                moi.Duoi4Tuoi_C = q4_c.Where(p => p.Tuoi <= 4).Where(p => p.KetQua == "Tử vong").Count();
                moi.NuDuoi4Tuoi_M = q4_c.Where(p => p.Tuoi <= 4).Where(p => p.GTinh == 0).Count();
                moi.NuDuoi4Tuoi_C = q4_c.Where(p => p.Tuoi <= 4).Where(p => p.KetQua == "Tử vong").Where(p => p.GTinh == 0).Count();
                moi.Duoi14Tuoi_M = q4_c.Where(p => p.Tuoi > 4 && p.Tuoi <= 14).Count();
                moi.Duoi14Tuoi_C = q4_c.Where(p => p.Tuoi > 4 && p.Tuoi <= 14).Where(p => p.KetQua == "Tử vong").Count();
                moi.NuDuoi14Tuoi_M = q4_c.Where(p => p.Tuoi > 4 && p.Tuoi <= 14).Where(p => p.GTinh == 0).Count();
                moi.NuDuoi14Tuoi_C = q4_c.Where(p => p.Tuoi > 4 && p.Tuoi <= 14).Where(p => p.KetQua == "Tử vong").Where(p => p.GTinh == 0).Count();
                moi.Duoi19Tuoi_M = q4_c.Where(p => p.Tuoi > 14 && p.Tuoi <= 19).Count();
                moi.Duoi19Tuoi_C = q4_c.Where(p => p.Tuoi > 14 && p.Tuoi <= 19).Where(p => p.KetQua == "Tử vong").Count();
                moi.NuDuoi19Tuoi_M = q4_c.Where(p => p.Tuoi > 14 && p.Tuoi <= 19).Where(p => p.GTinh == 0).Count();
                moi.NuDuoi19Tuoi_C = q4_c.Where(p => p.Tuoi > 14 && p.Tuoi <= 19).Where(p => p.KetQua == "Tử vong").Where(p => p.GTinh == 0).Count();
                moi.Duoi60Tuoi_M = q4_c.Where(p => p.Tuoi > 19 && p.Tuoi <= 60).Count();
                moi.Duoi60Tuoi_C = q4_c.Where(p => p.Tuoi > 19 && p.Tuoi <= 60).Where(p => p.KetQua == "Tử vong").Count();
                moi.NuDuoi60Tuoi_M = q4_c.Where(p => p.Tuoi > 19 && p.Tuoi <= 60).Where(p => p.GTinh == 0).Count();
                moi.NuDuoi60Tuoi_C = q4_c.Where(p => p.Tuoi > 19 && p.Tuoi <= 60).Where(p => p.KetQua == "Tử vong").Where(p => p.GTinh == 0).Count();
                moi.Tren60Tuoi_M = q4_c.Where(p => p.Tuoi > 60).Count();
                moi.Tren60Tuoi_C = q4_c.Where(p => p.Tuoi > 60).Where(p => p.KetQua == "Tử vong").Count();
                moi.NuTren60Tuoi_M = q4_c.Where(p => p.Tuoi > 60).Where(p => p.GTinh == 0).Count();
                moi.NuTren60Tuoi_C = q4_c.Where(p => p.Tuoi > 60).Where(p => p.GTinh == 0).Where(p => p.KetQua == "Tử vong").Count();
                _listContent.Add(moi);

                var q4_d = (from n in query
                            select new
                            {
                                n.MaICD,
                                n.KetQua,
                                n.GTinh,
                                n.Tuoi
                            }).Where(p => p.MaICD != null)
                            .Where(p => p.MaICD.Contains("W00") || p.MaICD.Contains("W01") || p.MaICD.Contains("W02") || p.MaICD.Contains("W03") ||
                                   p.MaICD.Contains("W04") || p.MaICD.Contains("W05") || p.MaICD.Contains("W06") || p.MaICD.Contains("W07") || p.MaICD.Contains("W08") ||
                                   p.MaICD.Contains("W09") || p.MaICD.Contains("W10") || p.MaICD.Contains("W11") || p.MaICD.Contains("W12") || p.MaICD.Contains("W13") ||
                                   p.MaICD.Contains("W14") || p.MaICD.Contains("W15") || p.MaICD.Contains("W16") || p.MaICD.Contains("W17") || p.MaICD.Contains("W18") || p.MaICD.Contains("S19")).ToList();

                moi = new clsThongKe();
                moi.Stt = 4;
                moi.TenNhom = "Nguyên nhân TNTT - theo ICD 10";
                moi.ChiTiet = "Ngã(W01-W19)";
                moi.Tong_M = q4_d.Count;
                moi.Tong_C = q4_d.Where(p => p.KetQua == "Tử vong").Count();
                moi.TongNu_M = q4_d.Where(p => p.GTinh == 0).Count();
                moi.TongNu_C = q4_d.Where(p => p.KetQua == "Tử vong").Where(p => p.GTinh == 0).Count();
                moi.Duoi4Tuoi_M = q4_d.Where(p => p.Tuoi <= 4).Count();
                moi.Duoi4Tuoi_C = q4_d.Where(p => p.Tuoi <= 4).Where(p => p.KetQua == "Tử vong").Count();
                moi.NuDuoi4Tuoi_M = q4_d.Where(p => p.Tuoi <= 4).Where(p => p.GTinh == 0).Count();
                moi.NuDuoi4Tuoi_C = q4_d.Where(p => p.Tuoi <= 4).Where(p => p.KetQua == "Tử vong").Where(p => p.GTinh == 0).Count();
                moi.Duoi14Tuoi_M = q4_d.Where(p => p.Tuoi > 4 && p.Tuoi <= 14).Count();
                moi.Duoi14Tuoi_C = q4_d.Where(p => p.Tuoi > 4 && p.Tuoi <= 14).Where(p => p.KetQua == "Tử vong").Count();
                moi.NuDuoi14Tuoi_M = q4_d.Where(p => p.Tuoi > 4 && p.Tuoi <= 14).Where(p => p.GTinh == 0).Count();
                moi.NuDuoi14Tuoi_C = q4_d.Where(p => p.Tuoi > 4 && p.Tuoi <= 14).Where(p => p.KetQua == "Tử vong").Where(p => p.GTinh == 0).Count();
                moi.Duoi19Tuoi_M = q4_d.Where(p => p.Tuoi > 14 && p.Tuoi <= 19).Count();
                moi.Duoi19Tuoi_C = q4_d.Where(p => p.Tuoi > 14 && p.Tuoi <= 19).Where(p => p.KetQua == "Tử vong").Count();
                moi.NuDuoi19Tuoi_M = q4_d.Where(p => p.Tuoi > 14 && p.Tuoi <= 19).Where(p => p.GTinh == 0).Count();
                moi.NuDuoi19Tuoi_C = q4_d.Where(p => p.Tuoi > 14 && p.Tuoi <= 19).Where(p => p.KetQua == "Tử vong").Where(p => p.GTinh == 0).Count();
                moi.Duoi60Tuoi_M = q4_d.Where(p => p.Tuoi > 19 && p.Tuoi <= 60).Count();
                moi.Duoi60Tuoi_C = q4_d.Where(p => p.Tuoi > 19 && p.Tuoi <= 60).Where(p => p.KetQua == "Tử vong").Count();
                moi.NuDuoi60Tuoi_M = q4_d.Where(p => p.Tuoi > 19 && p.Tuoi <= 60).Where(p => p.GTinh == 0).Count();
                moi.NuDuoi60Tuoi_C = q4_d.Where(p => p.Tuoi > 19 && p.Tuoi <= 60).Where(p => p.KetQua == "Tử vong").Where(p => p.GTinh == 0).Count();
                moi.Tren60Tuoi_M = q4_d.Where(p => p.Tuoi > 60).Count();
                moi.Tren60Tuoi_C = q4_d.Where(p => p.Tuoi > 60).Where(p => p.KetQua == "Tử vong").Count();
                moi.NuTren60Tuoi_M = q4_d.Where(p => p.Tuoi > 60).Where(p => p.GTinh == 0).Count();
                moi.NuTren60Tuoi_C = q4_d.Where(p => p.Tuoi > 60).Where(p => p.GTinh == 0).Where(p => p.KetQua == "Tử vong").Count();
                _listContent.Add(moi);

                var q4_e = (from n in query
                            select new
                            {
                                n.MaICD,
                                n.KetQua,
                                n.GTinh,
                                n.Tuoi
                            }).Where(p => p.MaICD != null)
                            .Where(p => p.MaICD.Contains("W65") || p.MaICD.Contains("W66") || p.MaICD.Contains("W67") || p.MaICD.Contains("W68") ||
                                   p.MaICD.Contains("W69") || p.MaICD.Contains("W70") || p.MaICD.Contains("W71") || p.MaICD.Contains("W72") || p.MaICD.Contains("W73") ||
                                   p.MaICD.Contains("W74") || p.MaICD.Contains("W75") || p.MaICD.Contains("W76") || p.MaICD.Contains("W77") || p.MaICD.Contains("W78") ||
                                   p.MaICD.Contains("W79") || p.MaICD.Contains("W80") || p.MaICD.Contains("W81") || p.MaICD.Contains("W82") || p.MaICD.Contains("W83") || p.MaICD.Contains("W84")).ToList();

                moi = new clsThongKe();
                moi.Stt = 4;
                moi.TenNhom = "Nguyên nhân TNTT - theo ICD 10";
                moi.ChiTiet = "Đuối nước(W65-W84)";
                moi.Tong_M = q4_e.Count;
                moi.Tong_C = q4_e.Where(p => p.KetQua == "Tử vong").Count();
                moi.TongNu_M = q4_e.Where(p => p.GTinh == 0).Count();
                moi.TongNu_C = q4_e.Where(p => p.KetQua == "Tử vong").Where(p => p.GTinh == 0).Count();
                moi.Duoi4Tuoi_M = q4_e.Where(p => p.Tuoi <= 4).Count();
                moi.Duoi4Tuoi_C = q4_e.Where(p => p.Tuoi <= 4).Where(p => p.KetQua == "Tử vong").Count();
                moi.NuDuoi4Tuoi_M = q4_e.Where(p => p.Tuoi <= 4).Where(p => p.GTinh == 0).Count();
                moi.NuDuoi4Tuoi_C = q4_e.Where(p => p.Tuoi <= 4).Where(p => p.KetQua == "Tử vong").Where(p => p.GTinh == 0).Count();
                moi.Duoi14Tuoi_M = q4_e.Where(p => p.Tuoi > 4 && p.Tuoi <= 14).Count();
                moi.Duoi14Tuoi_C = q4_e.Where(p => p.Tuoi > 4 && p.Tuoi <= 14).Where(p => p.KetQua == "Tử vong").Count();
                moi.NuDuoi14Tuoi_M = q4_e.Where(p => p.Tuoi > 4 && p.Tuoi <= 14).Where(p => p.GTinh == 0).Count();
                moi.NuDuoi14Tuoi_C = q4_e.Where(p => p.Tuoi > 4 && p.Tuoi <= 14).Where(p => p.KetQua == "Tử vong").Where(p => p.GTinh == 0).Count();
                moi.Duoi19Tuoi_M = q4_e.Where(p => p.Tuoi > 14 && p.Tuoi <= 19).Count();
                moi.Duoi19Tuoi_C = q4_e.Where(p => p.Tuoi > 14 && p.Tuoi <= 19).Where(p => p.KetQua == "Tử vong").Count();
                moi.NuDuoi19Tuoi_M = q4_e.Where(p => p.Tuoi > 14 && p.Tuoi <= 19).Where(p => p.GTinh == 0).Count();
                moi.NuDuoi19Tuoi_C = q4_e.Where(p => p.Tuoi > 14 && p.Tuoi <= 19).Where(p => p.KetQua == "Tử vong").Where(p => p.GTinh == 0).Count();
                moi.Duoi60Tuoi_M = q4_e.Where(p => p.Tuoi > 19 && p.Tuoi <= 60).Count();
                moi.Duoi60Tuoi_C = q4_e.Where(p => p.Tuoi > 19 && p.Tuoi <= 60).Where(p => p.KetQua == "Tử vong").Count();
                moi.NuDuoi60Tuoi_M = q4_e.Where(p => p.Tuoi > 19 && p.Tuoi <= 60).Where(p => p.GTinh == 0).Count();
                moi.NuDuoi60Tuoi_C = q4_e.Where(p => p.Tuoi > 19 && p.Tuoi <= 60).Where(p => p.KetQua == "Tử vong").Where(p => p.GTinh == 0).Count();
                moi.Tren60Tuoi_M = q4_e.Where(p => p.Tuoi > 60).Count();
                moi.Tren60Tuoi_C = q4_e.Where(p => p.Tuoi > 60).Where(p => p.KetQua == "Tử vong").Count();
                moi.NuTren60Tuoi_M = q4_e.Where(p => p.Tuoi > 60).Where(p => p.GTinh == 0).Count();
                moi.NuTren60Tuoi_C = q4_e.Where(p => p.Tuoi > 60).Where(p => p.GTinh == 0).Where(p => p.KetQua == "Tử vong").Count();
                _listContent.Add(moi);

                var q4_f = (from n in query
                            select new
                            {
                                n.MaICD,
                                n.KetQua,
                                n.GTinh,
                                n.Tuoi
                            }).Where(p => p.MaICD != null)
                            .Where(p => p.MaICD.Contains("W85") || p.MaICD.Contains("W86") || p.MaICD.Contains("W87") || p.MaICD.Contains("W88") ||
                                   p.MaICD.Contains("W89") || p.MaICD.Contains("W90") || p.MaICD.Contains("W91") || p.MaICD.Contains("W92") || p.MaICD.Contains("W93") ||
                                   p.MaICD.Contains("W94") || p.MaICD.Contains("W95") || p.MaICD.Contains("W96") || p.MaICD.Contains("W97") || p.MaICD.Contains("W98") ||
                                   p.MaICD.Contains("W99") || p.MaICD.Contains("X00") || p.MaICD.Contains("X01") || p.MaICD.Contains("X02") || p.MaICD.Contains("X03") || p.MaICD.Contains("X04") ||
                                   p.MaICD.Contains("X05") || p.MaICD.Contains("X06") || p.MaICD.Contains("X07") || p.MaICD.Contains("X08") || p.MaICD.Contains("X09") || p.MaICD.Contains("X10") ||
                                   p.MaICD.Contains("X11") || p.MaICD.Contains("X12") || p.MaICD.Contains("X13") || p.MaICD.Contains("X14") || p.MaICD.Contains("X15") || p.MaICD.Contains("X16") ||
                                   p.MaICD.Contains("X17") || p.MaICD.Contains("X18") || p.MaICD.Contains("X19")).ToList();

                moi = new clsThongKe();
                moi.Stt = 4;
                moi.TenNhom = "Nguyên nhân TNTT - theo ICD 10";
                moi.ChiTiet = "Bỏng(W85-W99;X00-X19)";
                moi.Tong_M = q4_f.Count;
                moi.Tong_C = q4_f.Where(p => p.KetQua == "Tử vong").Count();
                moi.TongNu_M = q4_f.Where(p => p.GTinh == 0).Count();
                moi.TongNu_C = q4_f.Where(p => p.KetQua == "Tử vong").Where(p => p.GTinh == 0).Count();
                moi.Duoi4Tuoi_M = q4_f.Where(p => p.Tuoi <= 4).Count();
                moi.Duoi4Tuoi_C = q4_f.Where(p => p.Tuoi <= 4).Where(p => p.KetQua == "Tử vong").Count();
                moi.NuDuoi4Tuoi_M = q4_f.Where(p => p.Tuoi <= 4).Where(p => p.GTinh == 0).Count();
                moi.NuDuoi4Tuoi_C = q4_f.Where(p => p.Tuoi <= 4).Where(p => p.KetQua == "Tử vong").Where(p => p.GTinh == 0).Count();
                moi.Duoi14Tuoi_M = q4_f.Where(p => p.Tuoi > 4 && p.Tuoi <= 14).Count();
                moi.Duoi14Tuoi_C = q4_f.Where(p => p.Tuoi > 4 && p.Tuoi <= 14).Where(p => p.KetQua == "Tử vong").Count();
                moi.NuDuoi14Tuoi_M = q4_f.Where(p => p.Tuoi > 4 && p.Tuoi <= 14).Where(p => p.GTinh == 0).Count();
                moi.NuDuoi14Tuoi_C = q4_f.Where(p => p.Tuoi > 4 && p.Tuoi <= 14).Where(p => p.KetQua == "Tử vong").Where(p => p.GTinh == 0).Count();
                moi.Duoi19Tuoi_M = q4_f.Where(p => p.Tuoi > 14 && p.Tuoi <= 19).Count();
                moi.Duoi19Tuoi_C = q4_f.Where(p => p.Tuoi > 14 && p.Tuoi <= 19).Where(p => p.KetQua == "Tử vong").Count();
                moi.NuDuoi19Tuoi_M = q4_f.Where(p => p.Tuoi > 14 && p.Tuoi <= 19).Where(p => p.GTinh == 0).Count();
                moi.NuDuoi19Tuoi_C = q4_f.Where(p => p.Tuoi > 14 && p.Tuoi <= 19).Where(p => p.KetQua == "Tử vong").Where(p => p.GTinh == 0).Count();
                moi.Duoi60Tuoi_M = q4_f.Where(p => p.Tuoi > 19 && p.Tuoi <= 60).Count();
                moi.Duoi60Tuoi_C = q4_f.Where(p => p.Tuoi > 19 && p.Tuoi <= 60).Where(p => p.KetQua == "Tử vong").Count();
                moi.NuDuoi60Tuoi_M = q4_f.Where(p => p.Tuoi > 19 && p.Tuoi <= 60).Where(p => p.GTinh == 0).Count();
                moi.NuDuoi60Tuoi_C = q4_f.Where(p => p.Tuoi > 19 && p.Tuoi <= 60).Where(p => p.KetQua == "Tử vong").Where(p => p.GTinh == 0).Count();
                moi.Tren60Tuoi_M = q4_f.Where(p => p.Tuoi > 60).Count();
                moi.Tren60Tuoi_C = q4_f.Where(p => p.Tuoi > 60).Where(p => p.KetQua == "Tử vong").Count();
                moi.NuTren60Tuoi_M = q4_f.Where(p => p.Tuoi > 60).Where(p => p.GTinh == 0).Count();
                moi.NuTren60Tuoi_C = q4_f.Where(p => p.Tuoi > 60).Where(p => p.GTinh == 0).Where(p => p.KetQua == "Tử vong").Count();
                _listContent.Add(moi);

                var q4_g = (from n in query
                            select new
                            {
                                n.MaICD,
                                n.KetQua,
                                n.GTinh,
                                n.Tuoi
                            }).Where(p => p.MaICD != null)
                            .Where(p => p.MaICD.Contains("X25") || p.MaICD.Contains("X26") || p.MaICD.Contains("X27") || p.MaICD.Contains("X28") || p.MaICD.Contains("X29") || p.MaICD.Contains("X40") ||
                                   p.MaICD.Contains("X41") || p.MaICD.Contains("X42") || p.MaICD.Contains("X43") || p.MaICD.Contains("X44") || p.MaICD.Contains("X45") ||
                                   p.MaICD.Contains("X46") || p.MaICD.Contains("X47") || p.MaICD.Contains("X48") || p.MaICD.Contains("X49")).ToList();

                moi = new clsThongKe();
                moi.Stt = 4;
                moi.TenNhom = "Nguyên nhân TNTT - theo ICD 10";
                moi.ChiTiet = "Ngộ độc: hóa chất, thực phẩm động vật, thực vật có độc (X29-X25; X40-X49)";
                moi.Tong_M = q4_g.Count;
                moi.Tong_C = q4_g.Where(p => p.KetQua == "Tử vong").Count();
                moi.TongNu_M = q4_g.Where(p => p.GTinh == 0).Count();
                moi.TongNu_C = q4_g.Where(p => p.KetQua == "Tử vong").Where(p => p.GTinh == 0).Count();
                moi.Duoi4Tuoi_M = q4_g.Where(p => p.Tuoi <= 4).Count();
                moi.Duoi4Tuoi_C = q4_g.Where(p => p.Tuoi <= 4).Where(p => p.KetQua == "Tử vong").Count();
                moi.NuDuoi4Tuoi_M = q4_g.Where(p => p.Tuoi <= 4).Where(p => p.GTinh == 0).Count();
                moi.NuDuoi4Tuoi_C = q4_g.Where(p => p.Tuoi <= 4).Where(p => p.KetQua == "Tử vong").Where(p => p.GTinh == 0).Count();
                moi.Duoi14Tuoi_M = q4_g.Where(p => p.Tuoi > 4 && p.Tuoi <= 14).Count();
                moi.Duoi14Tuoi_C = q4_g.Where(p => p.Tuoi > 4 && p.Tuoi <= 14).Where(p => p.KetQua == "Tử vong").Count();
                moi.NuDuoi14Tuoi_M = q4_g.Where(p => p.Tuoi > 4 && p.Tuoi <= 14).Where(p => p.GTinh == 0).Count();
                moi.NuDuoi14Tuoi_C = q4_g.Where(p => p.Tuoi > 4 && p.Tuoi <= 14).Where(p => p.KetQua == "Tử vong").Where(p => p.GTinh == 0).Count();
                moi.Duoi19Tuoi_M = q4_g.Where(p => p.Tuoi > 14 && p.Tuoi <= 19).Count();
                moi.Duoi19Tuoi_C = q4_g.Where(p => p.Tuoi > 14 && p.Tuoi <= 19).Where(p => p.KetQua == "Tử vong").Count();
                moi.NuDuoi19Tuoi_M = q4_g.Where(p => p.Tuoi > 14 && p.Tuoi <= 19).Where(p => p.GTinh == 0).Count();
                moi.NuDuoi19Tuoi_C = q4_g.Where(p => p.Tuoi > 14 && p.Tuoi <= 19).Where(p => p.KetQua == "Tử vong").Where(p => p.GTinh == 0).Count();
                moi.Duoi60Tuoi_M = q4_g.Where(p => p.Tuoi > 19 && p.Tuoi <= 60).Count();
                moi.Duoi60Tuoi_C = q4_g.Where(p => p.Tuoi > 19 && p.Tuoi <= 60).Where(p => p.KetQua == "Tử vong").Count();
                moi.NuDuoi60Tuoi_M = q4_g.Where(p => p.Tuoi > 19 && p.Tuoi <= 60).Where(p => p.GTinh == 0).Count();
                moi.NuDuoi60Tuoi_C = q4_g.Where(p => p.Tuoi > 19 && p.Tuoi <= 60).Where(p => p.KetQua == "Tử vong").Where(p => p.GTinh == 0).Count();
                moi.Tren60Tuoi_M = q4_g.Where(p => p.Tuoi > 60).Count();
                moi.Tren60Tuoi_C = q4_g.Where(p => p.Tuoi > 60).Where(p => p.KetQua == "Tử vong").Count();
                moi.NuTren60Tuoi_M = q4_g.Where(p => p.Tuoi > 60).Where(p => p.GTinh == 0).Count();
                moi.NuTren60Tuoi_C = q4_g.Where(p => p.Tuoi > 60).Where(p => p.GTinh == 0).Where(p => p.KetQua == "Tử vong").Count();
                _listContent.Add(moi);

                var q4_h = (from n in query
                            select new
                            {
                                n.MaICD,
                                n.KetQua,
                                n.GTinh,
                                n.Tuoi
                            }).Where(p => p.MaICD != null)
                            .Where(p => p.MaICD.Contains("X65") || p.MaICD.Contains("X66") || p.MaICD.Contains("X67") || p.MaICD.Contains("X68") || p.MaICD.Contains("X60") || p.MaICD.Contains("X61") ||
                                   p.MaICD.Contains("X69") || p.MaICD.Contains("X70") || p.MaICD.Contains("X71") || p.MaICD.Contains("X72") || p.MaICD.Contains("X73") || p.MaICD.Contains("X62") ||
                                   p.MaICD.Contains("X74") || p.MaICD.Contains("X75") || p.MaICD.Contains("X76") || p.MaICD.Contains("X77") || p.MaICD.Contains("X78") || p.MaICD.Contains("X63") || p.MaICD.Contains("X64") ||
                                   p.MaICD.Contains("X79") || p.MaICD.Contains("X80") || p.MaICD.Contains("X81") || p.MaICD.Contains("X82") || p.MaICD.Contains("X83") || p.MaICD.Contains("X84")).ToList();

                moi = new clsThongKe();
                moi.Stt = 4;
                moi.TenNhom = "Nguyên nhân TNTT - theo ICD 10";
                moi.ChiTiet = "Tự tử(X60-X84)";
                moi.Tong_M = q4_h.Count;
                moi.Tong_C = q4_h.Where(p => p.KetQua == "Tử vong").Count();
                moi.TongNu_M = q4_h.Where(p => p.GTinh == 0).Count();
                moi.TongNu_C = q4_h.Where(p => p.KetQua == "Tử vong").Where(p => p.GTinh == 0).Count();
                moi.Duoi4Tuoi_M = q4_h.Where(p => p.Tuoi <= 4).Count();
                moi.Duoi4Tuoi_C = q4_h.Where(p => p.Tuoi <= 4).Where(p => p.KetQua == "Tử vong").Count();
                moi.NuDuoi4Tuoi_M = q4_h.Where(p => p.Tuoi <= 4).Where(p => p.GTinh == 0).Count();
                moi.NuDuoi4Tuoi_C = q4_h.Where(p => p.Tuoi <= 4).Where(p => p.KetQua == "Tử vong").Where(p => p.GTinh == 0).Count();
                moi.Duoi14Tuoi_M = q4_h.Where(p => p.Tuoi > 4 && p.Tuoi <= 14).Count();
                moi.Duoi14Tuoi_C = q4_h.Where(p => p.Tuoi > 4 && p.Tuoi <= 14).Where(p => p.KetQua == "Tử vong").Count();
                moi.NuDuoi14Tuoi_M = q4_h.Where(p => p.Tuoi > 4 && p.Tuoi <= 14).Where(p => p.GTinh == 0).Count();
                moi.NuDuoi14Tuoi_C = q4_h.Where(p => p.Tuoi > 4 && p.Tuoi <= 14).Where(p => p.KetQua == "Tử vong").Where(p => p.GTinh == 0).Count();
                moi.Duoi19Tuoi_M = q4_h.Where(p => p.Tuoi > 14 && p.Tuoi <= 19).Count();
                moi.Duoi19Tuoi_C = q4_h.Where(p => p.Tuoi > 14 && p.Tuoi <= 19).Where(p => p.KetQua == "Tử vong").Count();
                moi.NuDuoi19Tuoi_M = q4_h.Where(p => p.Tuoi > 14 && p.Tuoi <= 19).Where(p => p.GTinh == 0).Count();
                moi.NuDuoi19Tuoi_C = q4_h.Where(p => p.Tuoi > 14 && p.Tuoi <= 19).Where(p => p.KetQua == "Tử vong").Where(p => p.GTinh == 0).Count();
                moi.Duoi60Tuoi_M = q4_h.Where(p => p.Tuoi > 19 && p.Tuoi <= 60).Count();
                moi.Duoi60Tuoi_C = q4_h.Where(p => p.Tuoi > 19 && p.Tuoi <= 60).Where(p => p.KetQua == "Tử vong").Count();
                moi.NuDuoi60Tuoi_M = q4_h.Where(p => p.Tuoi > 19 && p.Tuoi <= 60).Where(p => p.GTinh == 0).Count();
                moi.NuDuoi60Tuoi_C = q4_h.Where(p => p.Tuoi > 19 && p.Tuoi <= 60).Where(p => p.KetQua == "Tử vong").Where(p => p.GTinh == 0).Count();
                moi.Tren60Tuoi_M = q4_h.Where(p => p.Tuoi > 60).Count();
                moi.Tren60Tuoi_C = q4_h.Where(p => p.Tuoi > 60).Where(p => p.KetQua == "Tử vong").Count();
                moi.NuTren60Tuoi_M = q4_h.Where(p => p.Tuoi > 60).Where(p => p.GTinh == 0).Count();
                moi.NuTren60Tuoi_C = q4_h.Where(p => p.Tuoi > 60).Where(p => p.GTinh == 0).Where(p => p.KetQua == "Tử vong").Count();
                _listContent.Add(moi);

                var q4_i = (from n in query
                            select new
                            {
                                n.MaICD,
                                n.KetQua,
                                n.GTinh,
                                n.Tuoi
                            }).Where(p => p.MaICD != null)
                            .Where(p => p.MaICD.Contains("X85") || p.MaICD.Contains("X86") || p.MaICD.Contains("X87") || p.MaICD.Contains("X88") || p.MaICD.Contains("X89") || p.MaICD.Contains("X90") ||
                                   p.MaICD.Contains("X91") || p.MaICD.Contains("X92") || p.MaICD.Contains("X93") || p.MaICD.Contains("X94") || p.MaICD.Contains("X95") || p.MaICD.Contains("X96") ||
                                   p.MaICD.Contains("X97") || p.MaICD.Contains("X98") || p.MaICD.Contains("X99") || p.MaICD.Contains("X01") || p.MaICD.Contains("X02") || p.MaICD.Contains("X03") || p.MaICD.Contains("X04") ||
                                   p.MaICD.Contains("X05") || p.MaICD.Contains("X06") || p.MaICD.Contains("X07") || p.MaICD.Contains("X08") || p.MaICD.Contains("X09")).ToList();

                moi = new clsThongKe();
                moi.Stt = 4;
                moi.TenNhom = "Nguyên nhân TNTT - theo ICD 10";
                moi.ChiTiet = "Bạo lực, xung đột(X85-Y09)";
                moi.Tong_M = q4_i.Count;
                moi.Tong_C = q4_i.Where(p => p.KetQua == "Tử vong").Count();
                moi.TongNu_M = q4_i.Where(p => p.GTinh == 0).Count();
                moi.TongNu_C = q4_i.Where(p => p.KetQua == "Tử vong").Where(p => p.GTinh == 0).Count();
                moi.Duoi4Tuoi_M = q4_i.Where(p => p.Tuoi <= 4).Count();
                moi.Duoi4Tuoi_C = q4_i.Where(p => p.Tuoi <= 4).Where(p => p.KetQua == "Tử vong").Count();
                moi.NuDuoi4Tuoi_M = q4_i.Where(p => p.Tuoi <= 4).Where(p => p.GTinh == 0).Count();
                moi.NuDuoi4Tuoi_C = q4_i.Where(p => p.Tuoi <= 4).Where(p => p.KetQua == "Tử vong").Where(p => p.GTinh == 0).Count();
                moi.Duoi14Tuoi_M = q4_i.Where(p => p.Tuoi > 4 && p.Tuoi <= 14).Count();
                moi.Duoi14Tuoi_C = q4_i.Where(p => p.Tuoi > 4 && p.Tuoi <= 14).Where(p => p.KetQua == "Tử vong").Count();
                moi.NuDuoi14Tuoi_M = q4_i.Where(p => p.Tuoi > 4 && p.Tuoi <= 14).Where(p => p.GTinh == 0).Count();
                moi.NuDuoi14Tuoi_C = q4_i.Where(p => p.Tuoi > 4 && p.Tuoi <= 14).Where(p => p.KetQua == "Tử vong").Where(p => p.GTinh == 0).Count();
                moi.Duoi19Tuoi_M = q4_i.Where(p => p.Tuoi > 14 && p.Tuoi <= 19).Count();
                moi.Duoi19Tuoi_C = q4_i.Where(p => p.Tuoi > 14 && p.Tuoi <= 19).Where(p => p.KetQua == "Tử vong").Count();
                moi.NuDuoi19Tuoi_M = q4_i.Where(p => p.Tuoi > 14 && p.Tuoi <= 19).Where(p => p.GTinh == 0).Count();
                moi.NuDuoi19Tuoi_C = q4_i.Where(p => p.Tuoi > 14 && p.Tuoi <= 19).Where(p => p.KetQua == "Tử vong").Where(p => p.GTinh == 0).Count();
                moi.Duoi60Tuoi_M = q4_i.Where(p => p.Tuoi > 19 && p.Tuoi <= 60).Count();
                moi.Duoi60Tuoi_C = q4_i.Where(p => p.Tuoi > 19 && p.Tuoi <= 60).Where(p => p.KetQua == "Tử vong").Count();
                moi.NuDuoi60Tuoi_M = q4_i.Where(p => p.Tuoi > 19 && p.Tuoi <= 60).Where(p => p.GTinh == 0).Count();
                moi.NuDuoi60Tuoi_C = q4_i.Where(p => p.Tuoi > 19 && p.Tuoi <= 60).Where(p => p.KetQua == "Tử vong").Where(p => p.GTinh == 0).Count();
                moi.Tren60Tuoi_M = q4_i.Where(p => p.Tuoi > 60).Count();
                moi.Tren60Tuoi_C = q4_i.Where(p => p.Tuoi > 60).Where(p => p.KetQua == "Tử vong").Count();
                moi.NuTren60Tuoi_M = q4_i.Where(p => p.Tuoi > 60).Where(p => p.GTinh == 0).Count();
                moi.NuTren60Tuoi_C = q4_i.Where(p => p.Tuoi > 60).Where(p => p.GTinh == 0).Where(p => p.KetQua == "Tử vong").Count();
                _listContent.Add(moi);

                moi = new clsThongKe();
                moi.Stt = 4;
                moi.TenNhom = "Nguyên nhân TNTT - theo ICD 10";
                moi.ChiTiet = "Khác";
                _listContent.Add(moi);
                #endregion
                #region Điều trị ban đầu sau tai nạn thương tích
                var query2 = (from n in query
                              join bv in data.BenhViens on n.MaBV equals bv.MaBV into kq
                              from kq1 in kq.DefaultIfEmpty()
                              select new
                              {
                                  n.MaBV,
                                  TuyenBV = kq1 != null ? kq1.TuyenBV : "",
                                  HangBV = kq1 != null ? kq1.HangBV.ToString() : "",
                                  n.KetQua,
                                  n.GTinh,
                                  n.Tuoi
                              }).ToList();

                moi = new clsThongKe();
                moi.Stt = 5;
                moi.TenNhom = "Điều trị ban đầu sau tai nạn thương tích";
                moi.ChiTiet = "";
                _listContent.Add(moi);

                moi = new clsThongKe();
                moi.Stt = 5;
                moi.TenNhom = "Điều trị ban đầu sau tai nạn thương tích";
                moi.ChiTiet = "Tự điều trị";
                //moi.Tong_M = query2.Count();
                //moi.Tong_C = query2.Where(p => p.KetQua == "Tử vong").Count();
                //moi.TongNu_M = query2.Where(p => p.GTinh == 0).Count();
                //moi.TongNu_C = query2.Where(p => p.GTinh == 0).Where(p => p.KetQua == "Tử vong").Count();
                //moi.Duoi4Tuoi_M = query2.Where(p => p.Tuoi <= 4).Count();
                //moi.Duoi4Tuoi_C = query2.Where(p => p.Tuoi <= 4).Where(p => p.KetQua == "Tử vong").Count();
                //moi.NuDuoi4Tuoi_M = query2.Where(p => p.Tuoi <= 4).Where(p => p.GTinh == 0).Count();
                //moi.NuDuoi4Tuoi_C = query2.Where(p => p.Tuoi <= 4).Where(p => p.GTinh == 0).Where(p => p.KetQua == "Tử vong").Count();
                //moi.Duoi14Tuoi_M = query2.Where(p => p.Tuoi > 4 && p.Tuoi <= 14).Count();
                //moi.Duoi14Tuoi_C = query2.Where(p => p.Tuoi > 4 && p.Tuoi <= 14).Where(p => p.KetQua == "Tử vong").Count();
                //moi.NuDuoi14Tuoi_M = query2.Where(p => p.Tuoi > 4 && p.Tuoi <= 14).Where(p => p.GTinh == 0).Count();
                //moi.NuDuoi14Tuoi_C = query2.Where(p => p.Tuoi > 4 && p.Tuoi <= 14).Where(p => p.GTinh == 0).Where(p => p.KetQua == "Tử vong").Count();
                //moi.Duoi19Tuoi_M = query2.Where(p => p.Tuoi > 14 && p.Tuoi <= 19).Count();
                //moi.Duoi19Tuoi_C = query2.Where(p => p.Tuoi > 14 && p.Tuoi <= 19).Where(p => p.KetQua == "Tử vong").Count();
                //moi.NuDuoi19Tuoi_M = query2.Where(p => p.Tuoi > 14 && p.Tuoi <= 19).Where(p => p.GTinh == 0).Count();
                //moi.NuDuoi19Tuoi_C = query2.Where(p => p.Tuoi > 14 && p.Tuoi <= 19).Where(p => p.GTinh == 0).Where(p => p.KetQua == "Tử vong").Count();
                //moi.Duoi60Tuoi_M = query2.Where(p => p.Tuoi > 19 && p.Tuoi <= 60).Count();
                //moi.Duoi60Tuoi_C = query2.Where(p => p.Tuoi > 19 && p.Tuoi <= 60).Where(p => p.KetQua == "Tử vong").Count();
                //moi.NuDuoi60Tuoi_M = query2.Where(p => p.Tuoi > 19 && p.Tuoi <= 60).Where(p => p.GTinh == 0).Count();
                //moi.NuDuoi60Tuoi_C = query2.Where(p => p.Tuoi > 19 && p.Tuoi <= 60).Where(p => p.GTinh == 0).Where(p => p.KetQua == "Tử vong").Count();
                //moi.Tren60Tuoi_M = query2.Where(p => p.Tuoi > 60).Count();
                //moi.Tren60Tuoi_C = query2.Where(p => p.Tuoi > 60).Where(p => p.KetQua == "Tử vong").Count();
                //moi.NuTren60Tuoi_M = query2.Where(p => p.Tuoi > 60).Where(p => p.GTinh == 0).Count();
                //moi.NuTren60Tuoi_C = query2.Where(p => p.Tuoi > 60).Where(p => p.GTinh == 0).Where(p => p.KetQua == "Tử vong").Count();
                _listContent.Add(moi);

                moi = new clsThongKe();
                moi.Stt = 5;
                moi.TenNhom = "Điều trị ban đầu sau tai nạn thương tích";
                moi.ChiTiet = "Đội sơ cứu của các Hội";
                _listContent.Add(moi);

                moi = new clsThongKe();
                moi.Stt = 5;
                moi.TenNhom = "Điều trị ban đầu sau tai nạn thương tích";
                moi.ChiTiet = "Cơ sở khám chữa bệnh tư nhân";
                _listContent.Add(moi);

                moi = new clsThongKe();
                moi.Stt = 5;
                moi.TenNhom = "Điều trị ban đầu sau tai nạn thương tích";
                moi.ChiTiet = "Trạm xá xã";
                moi.Tong_M = query2.Where(p => p.MaBV != null).Where(p => p.TuyenBV.Contains("D")).Count();
                moi.Tong_C = query2.Where(p => p.MaBV != null).Where(p => p.TuyenBV.Contains("D")).Where(p => p.KetQua == "Tử vong").Count();
                moi.TongNu_M = query2.Where(p => p.MaBV != null).Where(p => p.TuyenBV.Contains("D")).Where(p => p.GTinh == 0).Count();
                moi.TongNu_C = query2.Where(p => p.MaBV != null).Where(p => p.TuyenBV.Contains("D")).Where(p => p.GTinh == 0).Where(p => p.KetQua == "Tử vong").Count();
                moi.Duoi4Tuoi_M = query2.Where(p => p.MaBV != null).Where(p => p.TuyenBV.Contains("D")).Where(p => p.Tuoi <= 4).Count();
                moi.Duoi4Tuoi_C = query2.Where(p => p.MaBV != null).Where(p => p.TuyenBV.Contains("D")).Where(p => p.Tuoi <= 4).Where(p => p.KetQua == "Tử vong").Count();
                moi.NuDuoi4Tuoi_M = query2.Where(p => p.MaBV != null).Where(p => p.TuyenBV.Contains("D")).Where(p => p.Tuoi <= 4).Where(p => p.GTinh == 0).Count();
                moi.NuDuoi4Tuoi_C = query2.Where(p => p.MaBV != null).Where(p => p.TuyenBV.Contains("D")).Where(p => p.Tuoi <= 4).Where(p => p.GTinh == 0).Where(p => p.KetQua == "Tử vong").Count();
                moi.Duoi14Tuoi_M = query2.Where(p => p.MaBV != null).Where(p => p.TuyenBV.Contains("D")).Where(p => p.Tuoi > 4 && p.Tuoi <= 14).Count();
                moi.Duoi14Tuoi_C = query2.Where(p => p.MaBV != null).Where(p => p.TuyenBV.Contains("D")).Where(p => p.Tuoi > 4 && p.Tuoi <= 14).Where(p => p.KetQua == "Tử vong").Count();
                moi.NuDuoi14Tuoi_M = query2.Where(p => p.MaBV != null).Where(p => p.TuyenBV.Contains("D")).Where(p => p.Tuoi > 4 && p.Tuoi <= 14).Where(p => p.GTinh == 0).Count();
                moi.NuDuoi14Tuoi_C = query2.Where(p => p.MaBV != null).Where(p => p.TuyenBV.Contains("D")).Where(p => p.Tuoi > 4 && p.Tuoi <= 14).Where(p => p.GTinh == 0).Where(p => p.KetQua == "Tử vong").Count();
                moi.Duoi19Tuoi_M = query2.Where(p => p.MaBV != null).Where(p => p.TuyenBV.Contains("D")).Where(p => p.Tuoi > 14 && p.Tuoi <= 19).Count();
                moi.Duoi19Tuoi_C = query2.Where(p => p.MaBV != null).Where(p => p.TuyenBV.Contains("D")).Where(p => p.Tuoi > 14 && p.Tuoi <= 19).Where(p => p.KetQua == "Tử vong").Count();
                moi.NuDuoi19Tuoi_M = query2.Where(p => p.MaBV != null).Where(p => p.TuyenBV.Contains("D")).Where(p => p.Tuoi > 14 && p.Tuoi <= 19).Where(p => p.GTinh == 0).Count();
                moi.NuDuoi19Tuoi_C = query2.Where(p => p.MaBV != null).Where(p => p.TuyenBV.Contains("D")).Where(p => p.Tuoi > 14 && p.Tuoi <= 19).Where(p => p.GTinh == 0).Where(p => p.KetQua == "Tử vong").Count();
                moi.Duoi60Tuoi_M = query2.Where(p => p.MaBV != null).Where(p => p.TuyenBV.Contains("D")).Where(p => p.Tuoi > 19 && p.Tuoi <= 60).Count();
                moi.Duoi60Tuoi_C = query2.Where(p => p.MaBV != null).Where(p => p.TuyenBV.Contains("D")).Where(p => p.Tuoi > 19 && p.Tuoi <= 60).Where(p => p.KetQua == "Tử vong").Count();
                moi.NuDuoi60Tuoi_M = query2.Where(p => p.MaBV != null).Where(p => p.TuyenBV.Contains("D")).Where(p => p.Tuoi > 19 && p.Tuoi <= 60).Where(p => p.GTinh == 0).Count();
                moi.NuDuoi60Tuoi_C = query2.Where(p => p.MaBV != null).Where(p => p.TuyenBV.Contains("D")).Where(p => p.Tuoi > 19 && p.Tuoi <= 60).Where(p => p.GTinh == 0).Where(p => p.KetQua == "Tử vong").Count();
                moi.Tren60Tuoi_M = query2.Where(p => p.MaBV != null).Where(p => p.TuyenBV.Contains("D")).Where(p => p.Tuoi > 60).Count();
                moi.Tren60Tuoi_C = query2.Where(p => p.MaBV != null).Where(p => p.TuyenBV.Contains("D")).Where(p => p.Tuoi > 60).Where(p => p.KetQua == "Tử vong").Count();
                moi.NuTren60Tuoi_M = query2.Where(p => p.MaBV != null).Where(p => p.TuyenBV.Contains("D")).Where(p => p.Tuoi > 60).Where(p => p.GTinh == 0).Count();
                moi.NuTren60Tuoi_C = query2.Where(p => p.MaBV != null).Where(p => p.TuyenBV.Contains("D")).Where(p => p.Tuoi > 60).Where(p => p.GTinh == 0).Where(p => p.KetQua == "Tử vong").Count();
                _listContent.Add(moi);

                moi = new clsThongKe();
                moi.Stt = 5;
                moi.TenNhom = "Điều trị ban đầu sau tai nạn thương tích";
                moi.ChiTiet = "Trung tâm y tế bệnh viện Huyện";
                moi.Tong_M = query2.Where(p => p.MaBV != null).Where(p => p.TuyenBV.Contains("C")).Count();
                moi.Tong_C = query2.Where(p => p.MaBV != null).Where(p => p.TuyenBV.Contains("C")).Where(p => p.KetQua == "Tử vong").Count();
                moi.TongNu_M = query2.Where(p => p.MaBV != null).Where(p => p.TuyenBV.Contains("C")).Where(p => p.GTinh == 0).Count();
                moi.TongNu_C = query2.Where(p => p.MaBV != null).Where(p => p.TuyenBV.Contains("C")).Where(p => p.GTinh == 0).Where(p => p.KetQua == "Tử vong").Count();
                moi.Duoi4Tuoi_M = query2.Where(p => p.MaBV != null).Where(p => p.TuyenBV.Contains("C")).Where(p => p.Tuoi <= 4).Count();
                moi.Duoi4Tuoi_C = query2.Where(p => p.MaBV != null).Where(p => p.TuyenBV.Contains("C")).Where(p => p.Tuoi <= 4).Where(p => p.KetQua == "Tử vong").Count();
                moi.NuDuoi4Tuoi_M = query2.Where(p => p.MaBV != null).Where(p => p.TuyenBV.Contains("C")).Where(p => p.Tuoi <= 4).Where(p => p.GTinh == 0).Count();
                moi.NuDuoi4Tuoi_C = query2.Where(p => p.MaBV != null).Where(p => p.TuyenBV.Contains("C")).Where(p => p.Tuoi <= 4).Where(p => p.GTinh == 0).Where(p => p.KetQua == "Tử vong").Count();
                moi.Duoi14Tuoi_M = query2.Where(p => p.MaBV != null).Where(p => p.TuyenBV.Contains("C")).Where(p => p.Tuoi > 4 && p.Tuoi <= 14).Count();
                moi.Duoi14Tuoi_C = query2.Where(p => p.MaBV != null).Where(p => p.TuyenBV.Contains("C")).Where(p => p.Tuoi > 4 && p.Tuoi <= 14).Where(p => p.KetQua == "Tử vong").Count();
                moi.NuDuoi14Tuoi_M = query2.Where(p => p.MaBV != null).Where(p => p.TuyenBV.Contains("C")).Where(p => p.Tuoi > 4 && p.Tuoi <= 14).Where(p => p.GTinh == 0).Count();
                moi.NuDuoi14Tuoi_C = query2.Where(p => p.MaBV != null).Where(p => p.TuyenBV.Contains("C")).Where(p => p.Tuoi > 4 && p.Tuoi <= 14).Where(p => p.GTinh == 0).Where(p => p.KetQua == "Tử vong").Count();
                moi.Duoi19Tuoi_M = query2.Where(p => p.MaBV != null).Where(p => p.TuyenBV.Contains("C")).Where(p => p.Tuoi > 14 && p.Tuoi <= 19).Count();
                moi.Duoi19Tuoi_C = query2.Where(p => p.MaBV != null).Where(p => p.TuyenBV.Contains("C")).Where(p => p.Tuoi > 14 && p.Tuoi <= 19).Where(p => p.KetQua == "Tử vong").Count();
                moi.NuDuoi19Tuoi_M = query2.Where(p => p.MaBV != null).Where(p => p.TuyenBV.Contains("C")).Where(p => p.Tuoi > 14 && p.Tuoi <= 19).Where(p => p.GTinh == 0).Count();
                moi.NuDuoi19Tuoi_C = query2.Where(p => p.MaBV != null).Where(p => p.TuyenBV.Contains("C")).Where(p => p.Tuoi > 14 && p.Tuoi <= 19).Where(p => p.GTinh == 0).Where(p => p.KetQua == "Tử vong").Count();
                moi.Duoi60Tuoi_M = query2.Where(p => p.MaBV != null).Where(p => p.TuyenBV.Contains("C")).Where(p => p.Tuoi > 19 && p.Tuoi <= 60).Count();
                moi.Duoi60Tuoi_C = query2.Where(p => p.MaBV != null).Where(p => p.TuyenBV.Contains("C")).Where(p => p.Tuoi > 19 && p.Tuoi <= 60).Where(p => p.KetQua == "Tử vong").Count();
                moi.NuDuoi60Tuoi_M = query2.Where(p => p.MaBV != null).Where(p => p.TuyenBV.Contains("C")).Where(p => p.Tuoi > 19 && p.Tuoi <= 60).Where(p => p.GTinh == 0).Count();
                moi.NuDuoi60Tuoi_C = query2.Where(p => p.MaBV != null).Where(p => p.TuyenBV.Contains("C")).Where(p => p.Tuoi > 19 && p.Tuoi <= 60).Where(p => p.GTinh == 0).Where(p => p.KetQua == "Tử vong").Count();
                moi.Tren60Tuoi_M = query2.Where(p => p.MaBV != null).Where(p => p.TuyenBV.Contains("C")).Where(p => p.Tuoi > 60).Count();
                moi.Tren60Tuoi_C = query2.Where(p => p.MaBV != null).Where(p => p.TuyenBV.Contains("C")).Where(p => p.Tuoi > 60).Where(p => p.KetQua == "Tử vong").Count();
                moi.NuTren60Tuoi_M = query2.Where(p => p.MaBV != null).Where(p => p.TuyenBV.Contains("C")).Where(p => p.Tuoi > 60).Where(p => p.GTinh == 0).Count();
                moi.NuTren60Tuoi_C = query2.Where(p => p.MaBV != null).Where(p => p.TuyenBV.Contains("C")).Where(p => p.Tuoi > 60).Where(p => p.GTinh == 0).Where(p => p.KetQua == "Tử vong").Count();
                _listContent.Add(moi);

                moi = new clsThongKe();
                moi.Stt = 5;
                moi.TenNhom = "Điều trị ban đầu sau tai nạn thương tích";
                moi.ChiTiet = "Bệnh viện tỉnh";
                moi.Tong_M = query2.Where(p => p.MaBV != null).Where(p => p.TuyenBV.Contains("B")).Count();
                moi.Tong_C = query2.Where(p => p.MaBV != null).Where(p => p.TuyenBV.Contains("B")).Where(p => p.KetQua == "Tử vong").Count();
                moi.TongNu_M = query2.Where(p => p.MaBV != null).Where(p => p.TuyenBV.Contains("B")).Where(p => p.GTinh == 0).Count();
                moi.TongNu_C = query2.Where(p => p.MaBV != null).Where(p => p.TuyenBV.Contains("B")).Where(p => p.GTinh == 0).Where(p => p.KetQua == "Tử vong").Count();
                moi.Duoi4Tuoi_M = query2.Where(p => p.MaBV != null).Where(p => p.TuyenBV.Contains("B")).Where(p => p.Tuoi <= 4).Count();
                moi.Duoi4Tuoi_C = query2.Where(p => p.MaBV != null).Where(p => p.TuyenBV.Contains("B")).Where(p => p.Tuoi <= 4).Where(p => p.KetQua == "Tử vong").Count();
                moi.NuDuoi4Tuoi_M = query2.Where(p => p.MaBV != null).Where(p => p.TuyenBV.Contains("B")).Where(p => p.Tuoi <= 4).Where(p => p.GTinh == 0).Count();
                moi.NuDuoi4Tuoi_C = query2.Where(p => p.MaBV != null).Where(p => p.TuyenBV.Contains("B")).Where(p => p.Tuoi <= 4).Where(p => p.GTinh == 0).Where(p => p.KetQua == "Tử vong").Count();
                moi.Duoi14Tuoi_M = query2.Where(p => p.MaBV != null).Where(p => p.TuyenBV.Contains("B")).Where(p => p.Tuoi > 4 && p.Tuoi <= 14).Count();
                moi.Duoi14Tuoi_C = query2.Where(p => p.MaBV != null).Where(p => p.TuyenBV.Contains("B")).Where(p => p.Tuoi > 4 && p.Tuoi <= 14).Where(p => p.KetQua == "Tử vong").Count();
                moi.NuDuoi14Tuoi_M = query2.Where(p => p.MaBV != null).Where(p => p.TuyenBV.Contains("B")).Where(p => p.Tuoi > 4 && p.Tuoi <= 14).Where(p => p.GTinh == 0).Count();
                moi.NuDuoi14Tuoi_C = query2.Where(p => p.MaBV != null).Where(p => p.TuyenBV.Contains("B")).Where(p => p.Tuoi > 4 && p.Tuoi <= 14).Where(p => p.GTinh == 0).Where(p => p.KetQua == "Tử vong").Count();
                moi.Duoi19Tuoi_M = query2.Where(p => p.MaBV != null).Where(p => p.TuyenBV.Contains("B")).Where(p => p.Tuoi > 14 && p.Tuoi <= 19).Count();
                moi.Duoi19Tuoi_C = query2.Where(p => p.MaBV != null).Where(p => p.TuyenBV.Contains("B")).Where(p => p.Tuoi > 14 && p.Tuoi <= 19).Where(p => p.KetQua == "Tử vong").Count();
                moi.NuDuoi19Tuoi_M = query2.Where(p => p.MaBV != null).Where(p => p.TuyenBV.Contains("B")).Where(p => p.Tuoi > 14 && p.Tuoi <= 19).Where(p => p.GTinh == 0).Count();
                moi.NuDuoi19Tuoi_C = query2.Where(p => p.MaBV != null).Where(p => p.TuyenBV.Contains("B")).Where(p => p.Tuoi > 14 && p.Tuoi <= 19).Where(p => p.GTinh == 0).Where(p => p.KetQua == "Tử vong").Count();
                moi.Duoi60Tuoi_M = query2.Where(p => p.MaBV != null).Where(p => p.TuyenBV.Contains("B")).Where(p => p.Tuoi > 19 && p.Tuoi <= 60).Count();
                moi.Duoi60Tuoi_C = query2.Where(p => p.MaBV != null).Where(p => p.TuyenBV.Contains("B")).Where(p => p.Tuoi > 19 && p.Tuoi <= 60).Where(p => p.KetQua == "Tử vong").Count();
                moi.NuDuoi60Tuoi_M = query2.Where(p => p.MaBV != null).Where(p => p.TuyenBV.Contains("B")).Where(p => p.Tuoi > 19 && p.Tuoi <= 60).Where(p => p.GTinh == 0).Count();
                moi.NuDuoi60Tuoi_C = query2.Where(p => p.MaBV != null).Where(p => p.TuyenBV.Contains("B")).Where(p => p.Tuoi > 19 && p.Tuoi <= 60).Where(p => p.GTinh == 0).Where(p => p.KetQua == "Tử vong").Count();
                moi.Tren60Tuoi_M = query2.Where(p => p.MaBV != null).Where(p => p.TuyenBV.Contains("B")).Where(p => p.Tuoi > 60).Count();
                moi.Tren60Tuoi_C = query2.Where(p => p.MaBV != null).Where(p => p.TuyenBV.Contains("B")).Where(p => p.Tuoi > 60).Where(p => p.KetQua == "Tử vong").Count();
                moi.NuTren60Tuoi_M = query2.Where(p => p.MaBV != null).Where(p => p.TuyenBV.Contains("B")).Where(p => p.Tuoi > 60).Where(p => p.GTinh == 0).Count();
                moi.NuTren60Tuoi_C = query2.Where(p => p.MaBV != null).Where(p => p.TuyenBV.Contains("B")).Where(p => p.Tuoi > 60).Where(p => p.GTinh == 0).Where(p => p.KetQua == "Tử vong").Count();
                _listContent.Add(moi);

                moi = new clsThongKe();
                moi.Stt = 5;
                moi.TenNhom = "Điều trị ban đầu sau tai nạn thương tích";
                moi.ChiTiet = "Bệnh viện trung ương";
                moi.Tong_M = query2.Where(p => p.MaBV != null).Where(p => p.TuyenBV.Contains("A")).Count();
                moi.Tong_C = query2.Where(p => p.MaBV != null).Where(p => p.TuyenBV.Contains("A")).Where(p => p.KetQua == "Tử vong").Count();
                moi.TongNu_M = query2.Where(p => p.MaBV != null).Where(p => p.TuyenBV.Contains("A")).Where(p => p.GTinh == 0).Count();
                moi.TongNu_C = query2.Where(p => p.MaBV != null).Where(p => p.TuyenBV.Contains("A")).Where(p => p.GTinh == 0).Where(p => p.KetQua == "Tử vong").Count();
                moi.Duoi4Tuoi_M = query2.Where(p => p.MaBV != null).Where(p => p.TuyenBV.Contains("A")).Where(p => p.Tuoi <= 4).Count();
                moi.Duoi4Tuoi_C = query2.Where(p => p.MaBV != null).Where(p => p.TuyenBV.Contains("A")).Where(p => p.Tuoi <= 4).Where(p => p.KetQua == "Tử vong").Count();
                moi.NuDuoi4Tuoi_M = query2.Where(p => p.MaBV != null).Where(p => p.TuyenBV.Contains("A")).Where(p => p.Tuoi <= 4).Where(p => p.GTinh == 0).Count();
                moi.NuDuoi4Tuoi_C = query2.Where(p => p.MaBV != null).Where(p => p.TuyenBV.Contains("A")).Where(p => p.Tuoi <= 4).Where(p => p.GTinh == 0).Where(p => p.KetQua == "Tử vong").Count();
                moi.Duoi14Tuoi_M = query2.Where(p => p.MaBV != null).Where(p => p.TuyenBV.Contains("A")).Where(p => p.Tuoi > 4 && p.Tuoi <= 14).Count();
                moi.Duoi14Tuoi_C = query2.Where(p => p.MaBV != null).Where(p => p.TuyenBV.Contains("A")).Where(p => p.Tuoi > 4 && p.Tuoi <= 14).Where(p => p.KetQua == "Tử vong").Count();
                moi.NuDuoi14Tuoi_M = query2.Where(p => p.MaBV != null).Where(p => p.TuyenBV.Contains("A")).Where(p => p.Tuoi > 4 && p.Tuoi <= 14).Where(p => p.GTinh == 0).Count();
                moi.NuDuoi14Tuoi_C = query2.Where(p => p.MaBV != null).Where(p => p.TuyenBV.Contains("A")).Where(p => p.Tuoi > 4 && p.Tuoi <= 14).Where(p => p.GTinh == 0).Where(p => p.KetQua == "Tử vong").Count();
                moi.Duoi19Tuoi_M = query2.Where(p => p.MaBV != null).Where(p => p.TuyenBV.Contains("A")).Where(p => p.Tuoi > 14 && p.Tuoi <= 19).Count();
                moi.Duoi19Tuoi_C = query2.Where(p => p.MaBV != null).Where(p => p.TuyenBV.Contains("A")).Where(p => p.Tuoi > 14 && p.Tuoi <= 19).Where(p => p.KetQua == "Tử vong").Count();
                moi.NuDuoi19Tuoi_M = query2.Where(p => p.MaBV != null).Where(p => p.TuyenBV.Contains("A")).Where(p => p.Tuoi > 14 && p.Tuoi <= 19).Where(p => p.GTinh == 0).Count();
                moi.NuDuoi19Tuoi_C = query2.Where(p => p.MaBV != null).Where(p => p.TuyenBV.Contains("A")).Where(p => p.Tuoi > 14 && p.Tuoi <= 19).Where(p => p.GTinh == 0).Where(p => p.KetQua == "Tử vong").Count();
                moi.Duoi60Tuoi_M = query2.Where(p => p.MaBV != null).Where(p => p.TuyenBV.Contains("A")).Where(p => p.Tuoi > 19 && p.Tuoi <= 60).Count();
                moi.Duoi60Tuoi_C = query2.Where(p => p.MaBV != null).Where(p => p.TuyenBV.Contains("A")).Where(p => p.Tuoi > 19 && p.Tuoi <= 60).Where(p => p.KetQua == "Tử vong").Count();
                moi.NuDuoi60Tuoi_M = query2.Where(p => p.MaBV != null).Where(p => p.TuyenBV.Contains("A")).Where(p => p.Tuoi > 19 && p.Tuoi <= 60).Where(p => p.GTinh == 0).Count();
                moi.NuDuoi60Tuoi_C = query2.Where(p => p.MaBV != null).Where(p => p.TuyenBV.Contains("A")).Where(p => p.Tuoi > 19 && p.Tuoi <= 60).Where(p => p.GTinh == 0).Where(p => p.KetQua == "Tử vong").Count();
                moi.Tren60Tuoi_M = query2.Where(p => p.MaBV != null).Where(p => p.TuyenBV.Contains("A")).Where(p => p.Tuoi > 60).Count();
                moi.Tren60Tuoi_C = query2.Where(p => p.MaBV != null).Where(p => p.TuyenBV.Contains("A")).Where(p => p.Tuoi > 60).Where(p => p.KetQua == "Tử vong").Count();
                moi.NuTren60Tuoi_M = query2.Where(p => p.MaBV != null).Where(p => p.TuyenBV.Contains("A")).Where(p => p.Tuoi > 60).Where(p => p.GTinh == 0).Count();
                moi.NuTren60Tuoi_C = query2.Where(p => p.MaBV != null).Where(p => p.TuyenBV.Contains("A")).Where(p => p.Tuoi > 60).Where(p => p.GTinh == 0).Where(p => p.KetQua == "Tử vong").Count();
                _listContent.Add(moi);

                moi = new clsThongKe();
                moi.Stt = 5;
                moi.TenNhom = "Điều trị ban đầu sau tai nạn thương tích";
                moi.ChiTiet = "Khác";
                moi.Tong_M = query2.Where(p => p.MaBV == null).Count();
                moi.Tong_C = query2.Where(p => p.MaBV == null).Where(p => p.KetQua == "Tử vong").Count();
                moi.TongNu_M = query2.Where(p => p.MaBV == null).Where(p => p.GTinh == 0).Count();
                moi.TongNu_C = query2.Where(p => p.MaBV == null).Where(p => p.GTinh == 0).Where(p => p.KetQua == "Tử vong").Count();
                moi.Duoi4Tuoi_M = query2.Where(p => p.MaBV == null).Where(p => p.Tuoi <= 4).Count();
                moi.Duoi4Tuoi_C = query2.Where(p => p.MaBV == null).Where(p => p.Tuoi <= 4).Where(p => p.KetQua == "Tử vong").Count();
                moi.NuDuoi4Tuoi_M = query2.Where(p => p.MaBV == null).Where(p => p.Tuoi <= 4).Where(p => p.GTinh == 0).Count();
                moi.NuDuoi4Tuoi_C = query2.Where(p => p.MaBV == null).Where(p => p.Tuoi <= 4).Where(p => p.GTinh == 0).Where(p => p.KetQua == "Tử vong").Count();
                moi.Duoi14Tuoi_M = query2.Where(p => p.MaBV == null).Where(p => p.Tuoi > 4 && p.Tuoi <= 14).Count();
                moi.Duoi14Tuoi_C = query2.Where(p => p.MaBV == null).Where(p => p.Tuoi > 4 && p.Tuoi <= 14).Where(p => p.KetQua == "Tử vong").Count();
                moi.NuDuoi14Tuoi_M = query2.Where(p => p.MaBV == null).Where(p => p.Tuoi > 4 && p.Tuoi <= 14).Where(p => p.GTinh == 0).Count();
                moi.NuDuoi14Tuoi_C = query2.Where(p => p.MaBV == null).Where(p => p.Tuoi > 4 && p.Tuoi <= 14).Where(p => p.GTinh == 0).Where(p => p.KetQua == "Tử vong").Count();
                moi.Duoi19Tuoi_M = query2.Where(p => p.MaBV == null).Where(p => p.Tuoi > 14 && p.Tuoi <= 19).Count();
                moi.Duoi19Tuoi_C = query2.Where(p => p.MaBV == null).Where(p => p.Tuoi > 14 && p.Tuoi <= 19).Where(p => p.KetQua == "Tử vong").Count();
                moi.NuDuoi19Tuoi_M = query2.Where(p => p.MaBV == null).Where(p => p.Tuoi > 14 && p.Tuoi <= 19).Where(p => p.GTinh == 0).Count();
                moi.NuDuoi19Tuoi_C = query2.Where(p => p.MaBV == null).Where(p => p.Tuoi > 14 && p.Tuoi <= 19).Where(p => p.GTinh == 0).Where(p => p.KetQua == "Tử vong").Count();
                moi.Duoi60Tuoi_M = query2.Where(p => p.MaBV == null).Where(p => p.Tuoi > 19 && p.Tuoi <= 60).Count();
                moi.Duoi60Tuoi_C = query2.Where(p => p.MaBV == null).Where(p => p.Tuoi > 19 && p.Tuoi <= 60).Where(p => p.KetQua == "Tử vong").Count();
                moi.NuDuoi60Tuoi_M = query2.Where(p => p.MaBV == null).Where(p => p.Tuoi > 19 && p.Tuoi <= 60).Where(p => p.GTinh == 0).Count();
                moi.NuDuoi60Tuoi_C = query2.Where(p => p.MaBV == null).Where(p => p.Tuoi > 19 && p.Tuoi <= 60).Where(p => p.GTinh == 0).Where(p => p.KetQua == "Tử vong").Count();
                moi.Tren60Tuoi_M = query2.Where(p => p.MaBV == null).Where(p => p.Tuoi > 60).Count();
                moi.Tren60Tuoi_C = query2.Where(p => p.MaBV == null).Where(p => p.Tuoi > 60).Where(p => p.KetQua == "Tử vong").Count();
                moi.NuTren60Tuoi_M = query2.Where(p => p.MaBV == null).Where(p => p.Tuoi > 60).Where(p => p.GTinh == 0).Count();
                moi.NuTren60Tuoi_C = query2.Where(p => p.MaBV == null).Where(p => p.Tuoi > 60).Where(p => p.GTinh == 0).Where(p => p.KetQua == "Tử vong").Count();
                _listContent.Add(moi);
                #endregion

                BaoCao.Rep_BC_ThongKeTaiNanThuongTich rep = new BaoCao.Rep_BC_ThongKeTaiNanThuongTich();
                frmIn frm = new frmIn();
                rep.lblNgayKy.Text = "Ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;
                rep.lblTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
                rep.lblTenCQ.Text = DungChung.Bien.TenCQ;

                rep.lblTuNgayDenNgay.Text = "(Báo cáo từ ngày " + _tungay.ToString("dd/MM/yyyy") + " đến ngày " + _denngay.ToString("dd/MM/yyyy");
                rep.colGiamDoc.Text = DungChung.Bien.GiamDoc;
                rep.colNguoiLapBieu.Text = DungChung.Bien.NguoiLapBieu;
                rep.DataSource = _listContent;
                rep.BindingData();
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
                if (!string.IsNullOrEmpty(txtTieuDe.Text))
                {
                    rep.lblTieuDe.Text = txtTieuDe.Text.ToUpper();
                }
                else
                {
                    rep.lblTieuDe.Text = "BÁO CÁO THỐNG KÊ TAI NẠN, THƯƠNG TÍCH TỪ NGÀY " + _tungay.ToString("dd/MM/yyyy") + " đến ngày " + _denngay.ToString("dd/MM/yyyy");
                }
            }
            else
            {
                MessageBox.Show("Đến ngày không thể nhỏ hơn Từ ngày.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }

    public class clsThongKe
    {
        public int? Stt { get; set; }
        public string TenNhom { get; set; }
        public string ChiTiet { get; set; }
        public int? Tong_M { get; set; }
        public int? Tong_C { get; set; }
        public int? TongNu_M { get; set; }
        public int? TongNu_C { get; set; }
        public int? Duoi4Tuoi_M { get; set; }
        public int? Duoi4Tuoi_C { get; set; }
        public int? NuDuoi4Tuoi_M { get; set; }
        public int? NuDuoi4Tuoi_C { get; set; }
        public int? Duoi14Tuoi_M { get; set; }
        public int? Duoi14Tuoi_C { get; set; }
        public int? NuDuoi14Tuoi_M { get; set; }
        public int? NuDuoi14Tuoi_C { get; set; }
        public int? Duoi19Tuoi_M { get; set; }
        public int? Duoi19Tuoi_C { get; set; }
        public int? NuDuoi19Tuoi_M { get; set; }
        public int? NuDuoi19Tuoi_C { get; set; }
        public int? Duoi60Tuoi_M { get; set; }
        public int? Duoi60Tuoi_C { get; set; }
        public int? NuDuoi60Tuoi_M { get; set; }
        public int? NuDuoi60Tuoi_C { get; set; }
        public int? Tren60Tuoi_M { get; set; }
        public int? Tren60Tuoi_C { get; set; }
        public int? NuTren60Tuoi_M { get; set; }
        public int? NuTren60Tuoi_C { get; set; }
    }
}