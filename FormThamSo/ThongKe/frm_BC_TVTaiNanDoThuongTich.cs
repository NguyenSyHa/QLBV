using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using QLBV.BaoCao;

namespace QLBV.FormThamSo
{
    public partial class frm_BC_TVTaiNanDoThuongTich : DevExpress.XtraEditors.XtraForm
    {
        public frm_BC_TVTaiNanDoThuongTich()
        {
            InitializeComponent();
        }

        public class thangBC
        {
            public int Value { get; set; }
            public string Text { get; set; }
        }

        public class MyObject
        {
            public int Value { set; get; }
        }

        private void frm_BC_TVTaiNanDoThuongTich_Load(object sender, EventArgs e)
        {
            MinimizeBox = false;
            MaximizeBox = false;

            List<thangBC> _listThang = new List<thangBC>();
            _listThang.Add(new thangBC { Value = 3, Text = "3 tháng" });
            _listThang.Add(new thangBC { Value = 6, Text = "6 tháng" });
            _listThang.Add(new thangBC { Value = 9, Text = "9 tháng" });
            _listThang.Add(new thangBC { Value = 12, Text = "12 tháng" });

            cbbThang.DataSource = _listThang;
            cbbThang.DisplayMember = "Text";
            cbbThang.ValueMember = "Value";
            lupTuNgay.DateTime = DateTime.Now;
            lupDenNgay.DateTime = DateTime.Now;
            int namHT = DateTime.Now.Year;
            List<MyObject> _list = new List<MyObject>();
            for (int i = namHT - 10; i <= namHT; i++)
            {
                MyObject obj = new MyObject();
                obj.Value = i;
                _list.Add(obj);
            }
            cbbNam.DisplayMember = "Value";
            cbbNam.ValueMember = "Value";
            cbbNam.DataSource = _list;
            cbbNam.SelectedValue = namHT;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            int _thang = (int)cbbThang.SelectedValue;
            int _nam = (int)cbbNam.SelectedValue;
            DateTime tungay = new DateTime();
            DateTime denngay = new DateTime();
            if (rdTheoNgay.Checked)
            {
                tungay = DungChung.Ham.NgayTu(lupTuNgay.DateTime);
                denngay = DungChung.Ham.NgayDen(lupDenNgay.DateTime);
            }
            else
            {
                switch (_thang)
                {
                    case 3:
                        tungay = new DateTime(_nam, 1, 1);
                        denngay = new DateTime(_nam, 4, 1);
                        tungay = DungChung.Ham.NgayTu(tungay);
                        denngay = DungChung.Ham.NgayDen(denngay.AddDays(-1));
                        break;
                    case 6:
                        tungay = new DateTime(_nam, 1, 1);
                        denngay = new DateTime(_nam, 7, 1);
                        tungay = DungChung.Ham.NgayTu(tungay);
                        denngay = DungChung.Ham.NgayDen(denngay.AddDays(-1));
                        break;
                    case 9:
                        tungay = new DateTime(_nam, 1, 1);
                        denngay = new DateTime(_nam, 10, 1);
                        tungay = DungChung.Ham.NgayTu(tungay);
                        denngay = DungChung.Ham.NgayDen(denngay.AddDays(-1));
                        break;
                    case 12:
                        tungay = new DateTime(_nam, 1, 1);
                        denngay = new DateTime(_nam + 1, 1, 1);
                        tungay = DungChung.Ham.NgayTu(tungay);
                        denngay = DungChung.Ham.NgayDen(denngay.AddDays(-1));
                        break;
                }
            }


            List<QLBV.BaoCao.Rep_BC_TVTaiNanDoThuongTich.TongBV> list1 = new List<QLBV.BaoCao.Rep_BC_TVTaiNanDoThuongTich.TongBV>();
            List<QLBV.BaoCao.Rep_BC_TVTaiNanDoThuongTich.TongBNChuyen> list2 = new List<QLBV.BaoCao.Rep_BC_TVTaiNanDoThuongTich.TongBNChuyen>();
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            #region Tổng bệnh nhân tai nạn
            var qbnkb = (from bn in data.BenhNhans.Where(p => p.NNhap >= tungay && p.NNhap <= denngay)
                         join bnkb in data.BNKBs on bn.MaBNhan equals bnkb.MaBNhan
                         group new { bn, bnkb } by new { bn.MaBNhan } into kq
                         select new { kq.Key.MaBNhan, IDKB = kq.Max(p => p.bnkb.IDKB) }).ToList();

            var benhNhanTN = (from bnkb in data.BNKBs
                              join
                                  bn in data.BenhNhans.Where(p => p.NNhap >= tungay && p.NNhap <= denngay) on bnkb.MaBNhan equals bn.MaBNhan
                              join kp in data.KPhongs on bnkb.MaKP equals kp.MaKP
                              join rv in data.RaViens on bn.MaBNhan equals rv.MaBNhan into kq
                              from kq1 in kq.DefaultIfEmpty()
                              select new
                              {
                                  bnkb.MaKP,
                                  bnkb.IDKB,
                                  kp.TenKP,
                                  kq1.KetQua,
                                  bnkb.MaBNhan,
                                  bn.ChuyenKhoa,
                                  bn.NNhap,
                                  kq1.MaBVC
                              })
                                .Where(p => p.ChuyenKhoa == "Tai nạn giao thông" || p.ChuyenKhoa == "Đường bộ" || p.ChuyenKhoa == "Đường sắt"
                                       || p.ChuyenKhoa == "Đường sông" || p.ChuyenKhoa == "Tai nạn lao động" || p.ChuyenKhoa == "Tai nạn sinh hoạt"
                                       || p.ChuyenKhoa == "Đánh nhau" || p.ChuyenKhoa == "Tự tử" || p.ChuyenKhoa == "Ngộ độc" || p.ChuyenKhoa == "Đuối nước" || p.ChuyenKhoa == "Tai nạn dưới nước"
                                       || p.ChuyenKhoa == "Bạo lực gia đình" || p.ChuyenKhoa == "Khác").ToList();

            var query1 = (from n in benhNhanTN
                          join bnkb in qbnkb on n.IDKB equals bnkb.IDKB
                          group n by new { n.MaKP, n.TenKP } into kq
                          select new
                          {
                              TenKP = kq.Key.TenKP,
                              MaKP = kq.Key.MaKP,
                              //MaBNhan = kq.Key.MaBNhan,
                              TongBNTN = kq.Count(),
                              TongBNTV = kq.Where(p => p.KetQua == "Tử vong").Count(),
                              TNGT_T = kq.Where(p => p.ChuyenKhoa == "Tai nạn giao thông" || p.ChuyenKhoa == "Đường bộ" || p.ChuyenKhoa == "Đường sắt" || p.ChuyenKhoa == "Đường sông").Count(),
                              TNGT_TV_T = kq.Where(p => p.ChuyenKhoa == "Tai nạn giao thông" || p.ChuyenKhoa == "Đường bộ" || p.ChuyenKhoa == "Đường sắt" || p.ChuyenKhoa == "Đường sông").Where(p => p.KetQua == "Tử vong").Count(),
                              DuoiNuoc_T = kq.Where(p => p.ChuyenKhoa == "Đuối nước" || p.ChuyenKhoa == "Tai nạn dưới nước").Count(),
                              DuoiNuoc_TV_T = kq.Where(p => p.ChuyenKhoa == "Đuối nước" || p.ChuyenKhoa == "Tai nạn dưới nước").Where(p => p.KetQua == "Tử vong").Count(),
                              NgoDocTP_T = kq.Where(p => p.ChuyenKhoa == "Ngộ độc").Count(),
                              NgoDocTP_TV_T = kq.Where(p => p.ChuyenKhoa == "Ngộ độc").Where(p => p.KetQua == "Tử vong").Count(),
                              TuTu_T = kq.Where(p => p.ChuyenKhoa == "Tự tử").Count(),
                              TuTu_TV_T = kq.Where(p => p.ChuyenKhoa == "Tự tử").Where(p => p.KetQua == "Tử vong").Count(),
                              TNLD_T = kq.Where(p => p.ChuyenKhoa == "Tai nạn lao động").Count(),
                              TNLD_TV_T = kq.Where(p => p.ChuyenKhoa == "Tai nạn lao động").Where(p => p.KetQua == "Tử vong").Count(),
                              BLXD_T = kq.Where(p => p.ChuyenKhoa == "Bạo lực gia đình" || p.ChuyenKhoa == "Đánh nhau").Count(),
                              BLXD_TV_T = kq.Where(p => p.ChuyenKhoa == "Bạo lực gia đình" || p.ChuyenKhoa == "Đánh nhau").Where(p => p.KetQua == "Tử vong").Count(),
                              TaiNanKhac_T = kq.Where(p => p.ChuyenKhoa == "Khác" || p.ChuyenKhoa == "Tai nạn sinh hoạt").Count(),
                              TaiNanKhac_TV_T = kq.Where(p => p.ChuyenKhoa == "Khác" || p.ChuyenKhoa == "Tai nạn sinh hoạt").Where(p => p.KetQua == "Tử vong").Count()
                          }).ToList();

            foreach (var item in query1)
            {
                QLBV.BaoCao.Rep_BC_TVTaiNanDoThuongTich.TongBV obj = new QLBV.BaoCao.Rep_BC_TVTaiNanDoThuongTich.TongBV();
                obj.TenCS = item.TenKP;
                obj.MaKP = Convert.ToInt32(item.MaKP);
                //obj.MaBN = Convert.ToInt32(item.MaBNhan);
                obj.TN_T = item.TongBNTN;
                obj.TV_T = item.TongBNTV;
                obj.TNGT_T = item.TNGT_T;
                obj.TNGT_TV_T = item.TNGT_TV_T;
                obj.DuoiNuoc_T = item.DuoiNuoc_T;
                obj.DuoiNuoc_TV_T = item.DuoiNuoc_TV_T;
                obj.NGTP_T = item.NgoDocTP_T;
                obj.NGTP_TV_T = item.NgoDocTP_TV_T;
                obj.TuTu_T = item.TuTu_T;
                obj.TuTu_TV_T = item.TuTu_TV_T;
                obj.TNLD_T = item.TNLD_T;
                obj.TNLD_TV_T = item.TNLD_TV_T;
                obj.BLXD_T = item.BLXD_T;
                obj.BLXD_TV_T = item.BLXD_TV_T;
                obj.TNKhac_T = item.TaiNanKhac_T;
                obj.TNKhac_TV_T = item.TaiNanKhac_TV_T;
                list1.Add(obj);
            }
            #endregion
            #region Tổng bệnh nhân chuyển viện do tai nạn
            var BNChuyenDoTN = (from //bnkb in data.BNKBs join 
                                    bn in data.BenhNhans// on bnkb.MaBNhan equals bn.MaBNhan                               
                                join rv in data.RaViens on bn.MaBNhan equals rv.MaBNhan
                                join kp in data.KPhongs on rv.MaKP equals kp.MaKP
                                select new
                                {
                                    rv.NgayRa,
                                    kp.TenKP,
                                    rv.KetQua,
                                    rv.MaBNhan,
                                    bn.ChuyenKhoa,
                                    rv.MaBVC,
                                    rv.MaKP
                                }).Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay && p.MaBVC != null)
                                .Where(p => p.ChuyenKhoa == "Tai nạn giao thông" || p.ChuyenKhoa == "Đường bộ" || p.ChuyenKhoa == "Đường sắt"
                                       || p.ChuyenKhoa == "Đường sông" || p.ChuyenKhoa == "Tai nạn lao động" || p.ChuyenKhoa == "Tai nạn sinh hoạt"
                                       || p.ChuyenKhoa == "Đánh nhau" || p.ChuyenKhoa == "Tự tử" || p.ChuyenKhoa == "Ngộ độc" || p.ChuyenKhoa == "Đuối nước" || p.ChuyenKhoa == "Tai nạn dưới nước"
                                       || p.ChuyenKhoa == "Bạo lực gia đình" || p.ChuyenKhoa == "Khác").ToList();
            var query2 = (from n in BNChuyenDoTN
                          group n by new { n.MaKP, n.TenKP } into kq
                          select new
                          {
                              TenKP = kq.Key.TenKP,
                              MaKP = kq.Key.MaKP,
                              //MaBNhan = kq.Key.MaBNhan,
                              TongBNTN = kq.Count(),
                              TongBNTV = kq.Where(p => p.KetQua == "Tử vong").Count(),
                              TNGT_T = kq.Where(p => p.ChuyenKhoa == "Tai nạn giao thông" || p.ChuyenKhoa == "Đường bộ" || p.ChuyenKhoa == "Đường sắt" || p.ChuyenKhoa == "Đường sông" ).Count(),
                              TNGT_TV_T = kq.Where(p => p.ChuyenKhoa == "Tai nạn giao thông" || p.ChuyenKhoa == "Đường bộ" || p.ChuyenKhoa == "Đường sắt" || p.ChuyenKhoa == "Đường sông").Where(p => p.KetQua == "Tử vong").Count(),
                              DuoiNuoc_T = kq.Where(p => p.ChuyenKhoa == "Đuối nước" || p.ChuyenKhoa == "Tai nạn dưới nước").Count(),
                              DuoiNuoc_TV_T = kq.Where(p => p.ChuyenKhoa == "Đuối nước" || p.ChuyenKhoa == "Tai nạn dưới nước").Where(p => p.KetQua == "Tử vong").Count(),
                              NgoDocTP_T = kq.Where(p => p.ChuyenKhoa == "Ngộ độc").Count(),
                              NgoDocTP_TV_T = kq.Where(p => p.ChuyenKhoa == "Ngộ độc").Where(p => p.KetQua == "Tử vong").Count(),
                              TuTu_T = kq.Where(p => p.ChuyenKhoa == "Tự tử").Count(),
                              TuTu_TV_T = kq.Where(p => p.ChuyenKhoa == "Tự tử").Where(p => p.KetQua == "Tử vong").Count(),
                              TNLD_T = kq.Where(p => p.ChuyenKhoa == "Tai nạn lao động").Count(),
                              TNLD_TV_T = kq.Where(p => p.ChuyenKhoa == "Tai nạn lao động").Where(p => p.KetQua == "Tử vong").Count(),
                              BLXD_T = kq.Where(p => p.ChuyenKhoa == "Bạo lực gia đình" || p.ChuyenKhoa == "Đánh nhau").Count(),
                              BLXD_TV_T = kq.Where(p => p.ChuyenKhoa == "Bạo lực gia đình" || p.ChuyenKhoa == "Đánh nhau").Where(p => p.KetQua == "Tử vong").Count(),
                              TaiNanKhac_T = kq.Where(p => p.ChuyenKhoa == "Khác" || p.ChuyenKhoa == "Tai nạn sinh hoạt").Count(),
                              TaiNanKhac_TV_T = kq.Where(p => p.ChuyenKhoa == "Khác" || p.ChuyenKhoa == "Tai nạn sinh hoạt").Where(p => p.KetQua == "Tử vong").Count()
                          }).ToList();

            foreach (var item in query2)
            {
                QLBV.BaoCao.Rep_BC_TVTaiNanDoThuongTich.TongBNChuyen obj = new QLBV.BaoCao.Rep_BC_TVTaiNanDoThuongTich.TongBNChuyen();
                obj.TenCS_BNC = item.TenKP;
                obj.MaKP_BNC = Convert.ToInt32(item.MaKP);
                //obj.MaBN_BNC = Convert.ToInt32(item.MaBNhan);
                obj.TN_T_BNC = item.TongBNTN;
                obj.TV_T_BNC = item.TongBNTV;
                obj.TNGT_T_BNC = item.TNGT_T;
                obj.TNGT_TV_T_BNC = item.TNGT_TV_T;
                obj.DuoiNuoc_T_BNC = item.DuoiNuoc_T;
                obj.DuoiNuoc_TV_T_BNC = item.DuoiNuoc_TV_T;
                obj.NGTP_T_BNC = item.NgoDocTP_T;
                obj.NGTP_TV_T_BNC = item.NgoDocTP_TV_T;
                obj.TuTu_T_BNC = item.TuTu_T;
                obj.TuTu_TV_T_BNC = item.TuTu_TV_T;
                obj.TNLD_T_BNC = item.TNLD_T;
                obj.TNLD_TV_T_BNC = item.TNLD_TV_T;
                obj.BLXD_T_BNC = item.BLXD_T;
                obj.BLXD_TV_T_BNC = item.BLXD_TV_T;
                obj.TNKhac_T_BNC = item.TaiNanKhac_T;
                obj.TNKhac_TV_T_BNC = item.TaiNanKhac_TV_T;
                list2.Add(obj);
            }
            #endregion
            #region xuat Excel

            //string[] _arr = new string[] { "0", "@", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" };
            string[] _arr = new string[] { "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@" };
            string[] _tieude = { "STT", "Tên cơ sở y tế", "Tổng số", "", "TN GT", "", "Đuối nước", "", "Ngộ độc TP", "", "Tự tử", "", "Tai nạn LĐ", "", "Bạo lực, xung đột", "", "Tai nạn LĐ khác", "" };
            string[] _tieude1 = { "", "", "M", "C", "M", "C", "M", "C", "M", "C", "M", "C", "M", "C", "M", "C", "M", "C" };

            string[] _tieude2 = { "(1)", "(2)", "(3)", "(4)", "(5)", "(6)", "(7)", "(8)", "(9)", "(10)", "(11)", "(12)", "(13)", "(14)", "(15)", "(16)", "(17)", "(18)" };
            int[] _arrWidth = new int[] { };
            if (list1.Count + list2.Count > 0)
            {
                DungChung.Bien.MangHaiChieu = new Object[list1.Count + list2.Count + 15, 18];

                DungChung.Bien.MangHaiChieu[0, 0] = DungChung.Bien.TenCQ.ToUpper(); ;
                DungChung.Bien.MangHaiChieu[1, 0] = DungChung.Bien.DiaChi;
                DungChung.Bien.MangHaiChieu[3, 4] = ("TÌNH HÌNH MẮC VÀ TỬ VONG DO TAI NẠN THƯƠNG TÍCH").ToUpper();
                DungChung.Bien.MangHaiChieu[4, 4] = "Từ ngày " + tungay.ToShortDateString() + " đến ngày " + denngay.ToShortDateString();


                for (int i = 0; i < _tieude.Length; i++)
                {
                    DungChung.Bien.MangHaiChieu[6, i] = _tieude[i];
                }
                for (int i = 0; i < _tieude1.Length; i++)
                {
                    DungChung.Bien.MangHaiChieu[7, i] = _tieude1[i];
                }
                for (int i = 0; i < _tieude2.Length; i++)
                {
                    DungChung.Bien.MangHaiChieu[8, i] = _tieude2[i];
                }
                DungChung.Bien.MangHaiChieu[9, 1] = "Tổng bệnh viện";
                int num = 10;
                DungChung.Bien.MangHaiChieu[10 + list1.Count, 1] = "Tổng chuyển viện";
                DungChung.Bien.MangHaiChieu[num + list1.Count + list2.Count + 2, 1] = "Người lập biểu";
                DungChung.Bien.MangHaiChieu[num + list1.Count + list2.Count + 4, 1] = DungChung.Bien.NguoiLapBieu;

                DungChung.Bien.MangHaiChieu[num + list1.Count + list2.Count + 2, 10] = "Giám đốc";
                DungChung.Bien.MangHaiChieu[num + list1.Count + list2.Count + 4, 10] = DungChung.Bien.GiamDoc;


                foreach (var r in list1)
                {
                    DungChung.Bien.MangHaiChieu[num, 0] = num - 9;
                    DungChung.Bien.MangHaiChieu[num, 1] = r.TenCS;
                    DungChung.Bien.MangHaiChieu[num, 2] = r.TN_T;
                    DungChung.Bien.MangHaiChieu[num, 3] = r.TV_T;
                    DungChung.Bien.MangHaiChieu[num, 4] = r.TNGT_T;
                    DungChung.Bien.MangHaiChieu[num, 5] = r.TNGT_TV_T; ;
                    DungChung.Bien.MangHaiChieu[num, 6] = r.DuoiNuoc_T;
                    DungChung.Bien.MangHaiChieu[num, 7] = r.DuoiNuoc_TV_T;
                    DungChung.Bien.MangHaiChieu[num, 8] = r.NGTP_T;

                    DungChung.Bien.MangHaiChieu[num, 9] = r.NGTP_TV_T;
                    DungChung.Bien.MangHaiChieu[num, 10] = r.TuTu_T;
                    DungChung.Bien.MangHaiChieu[num, 11] = r.TuTu_TV_T;
                    DungChung.Bien.MangHaiChieu[num, 12] = r.TNLD_T;
                    DungChung.Bien.MangHaiChieu[num, 13] = r.TNLD_TV_T; ;
                    DungChung.Bien.MangHaiChieu[num, 14] = r.BLXD_T;
                    DungChung.Bien.MangHaiChieu[num, 15] = r.BLXD_TV_T;
                    DungChung.Bien.MangHaiChieu[num, 16] = r.TNKhac_T;
                    DungChung.Bien.MangHaiChieu[num, 17] = r.TNKhac_TV_T;
                    num++;

                }
                num++;
                foreach (var r in list2)
                {
                    DungChung.Bien.MangHaiChieu[num, 0] = num - 10;
                    DungChung.Bien.MangHaiChieu[num, 1] = r.TenCS_BNC;
                    DungChung.Bien.MangHaiChieu[num, 2] = r.TN_T_BNC;
                    DungChung.Bien.MangHaiChieu[num, 3] = r.TV_T_BNC;
                    DungChung.Bien.MangHaiChieu[num, 4] = r.TNGT_T_BNC;
                    DungChung.Bien.MangHaiChieu[num, 5] = r.TNGT_TV_T_BNC; ;
                    DungChung.Bien.MangHaiChieu[num, 6] = r.DuoiNuoc_T_BNC;
                    DungChung.Bien.MangHaiChieu[num, 7] = r.DuoiNuoc_TV_T_BNC;
                    DungChung.Bien.MangHaiChieu[num, 8] = r.NGTP_T_BNC;

                    DungChung.Bien.MangHaiChieu[num, 9] = r.NGTP_TV_T_BNC;
                    DungChung.Bien.MangHaiChieu[num, 10] = r.TuTu_T_BNC;
                    DungChung.Bien.MangHaiChieu[num, 11] = r.TuTu_TV_T_BNC;
                    DungChung.Bien.MangHaiChieu[num, 12] = r.TNLD_T_BNC;
                    DungChung.Bien.MangHaiChieu[num, 13] = r.TNLD_TV_T_BNC; ;
                    DungChung.Bien.MangHaiChieu[num, 14] = r.BLXD_T_BNC;
                    DungChung.Bien.MangHaiChieu[num, 15] = r.BLXD_TV_T_BNC;
                    DungChung.Bien.MangHaiChieu[num, 16] = r.TNKhac_T_BNC;
                    DungChung.Bien.MangHaiChieu[num, 17] = r.TNKhac_TV_T_BNC;
                    num++;

                }
            }
            #endregion

            Rep_BC_TVTaiNanDoThuongTich rep = new Rep_BC_TVTaiNanDoThuongTich(list1, list2, tungay, denngay);
          //  QLBV_Library.QLBV_Ham.xuatExcelArr(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "BC_TNThuongTich", "C:\\BC Tu vong_Tai nan thuong tich.xls", true);
            frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "BC_TNThuongTich", "C:\\BC Tu vong_Tai nan thuong tich.xls", true, this.Name);
            rep.CreateDocument();
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();
        }
    }
}