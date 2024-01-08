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

    public partial class Frm_BcHoatDongDTri_24009 : DevExpress.XtraEditors.XtraForm
    {
        public Frm_BcHoatDongDTri_24009()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

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
                MessageBox.Show("Bạn chưa chọn ngày kết thúc in báo cáo");
                lupDenNgay.Focus();
                return false;
            }
            else return true;
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

        List<KPhong> _Kphong = new List<KPhong>();

        private void Frm_BcHoatDongDtri_TH04_Load(object sender, EventArgs e)
        {
            lupTuNgay.Focus();
            lupTuNgay.DateTime = System.DateTime.Now;
            lupDenNgay.DateTime = System.DateTime.Now;
     
            var kphong = (from kp in data.KPhongs.Where(p => p.PLoai == "Lâm sàng")
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
        public class DTri
        {
            private int makp;

            public int MaKP
            {
                get { return makp; }
                set { makp = value; }
            }
            private string khoa;

            public string Khoa
            {
                get { return khoa; }
                set { khoa = value; }
            }
            private int c1;

            public int C1
            {
                get { return c1; }
                set { c1 = value; }
            }
            private int c2;

            public int C2
            {
                get { return c2; }
                set { c2 = value; }
            }
            private int c3;

            public int C3
            {
                get { return c3; }
                set { c3 = value; }
            }
            private int c4;

            public int C4
            {
                get { return c4; }
                set { c4 = value; }
            }
            private int c5;

            public int C5
            {
                get { return c5; }
                set { c5 = value; }
            }
            private int c6;

            public int C6
            {
                get { return c6; }
                set { c6 = value; }
            }
            private int c7;

            public int C7
            {
                get { return c7; }
                set { c7 = value; }
            }
            private int c8;

            public int C8
            {
                get { return c8; }
                set { c8 = value; }
            }
            private int c9;

            public int C9
            {
                get { return c9; }
                set { c9 = value; }
            }
            private int c10;

            public int C10
            {
                get { return c10; }
                set { c10 = value; }
            }
            private int c11;

            public int C11
            {
                get { return c11; }
                set { c11 = value; }
            }
            public int? gtinh { set; get; }

        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            DateTime tungay = System.DateTime.Now.Date;
            DateTime denngay = System.DateTime.Now.Date;
            var kp = data.KPhongs.ToList();
            List<DTri> _lDT = new List<DTri>();
            List<DTri> _lDTbc = new List<DTri>();
            List<KPhong> _lKhoaP = new List<KPhong>();

            if (KTtaoBc())
            {
                
                tungay = DungChung.Ham.NgayTu(lupTuNgay.DateTime);
                denngay = DungChung.Ham.NgayDen(lupDenNgay.DateTime);
                //frmIn frm = new frmIn();

                #region Hiển thị thời gian
                int nam = Convert.ToInt32(tungay.Year);
                int thang = Convert.ToInt32(tungay.Month);
                string _ntn = "";
                if (txtNgayThang.Text == "")
                { _ntn = "Từ ngày " + lupTuNgay.Text + " Đến ngày " + lupDenNgay.Text; }
                else
                {
                    _ntn = txtNgayThang.Text;
                }

                #endregion
                _lKhoaP.Clear();
                _lKhoaP = _Kphong.Where(p => p.makp != 0).Where(p => p.chon == true).ToList();

                //#region lấy bệnh nhân vào viện, khám bệnh, ra viện trong khoảng thời gian đó
       
                var q11 = (from bn in data.BenhNhans.Where(p=>p.NoiTru==1)
                           join rv in data.RaViens on bn.MaBNhan equals rv.MaBNhan into kq
                           join vv in data.VaoViens on bn.MaBNhan equals vv.MaBNhan 
                           from kq1 in kq.DefaultIfEmpty() where (
                           ( vv.NgayVao < tungay && kq1 != null && kq1.NgayRa > denngay) ||
                           (vv.NgayVao >= tungay && vv.NgayVao <= denngay) ||
                           (kq1 != null && (kq1.NgayRa >= tungay && kq1.NgayRa <= denngay)))
                           select new
                           {
                               bn.CapCuu,
                               bn.MaBNhan,
                               bn.TuyenDuoi,
                               bn.Tuoi,
                               bn.NNhap,
                               bn.NgaySinh,
                               bn.ThangSinh,
                               bn.NamSinh,
                               bn.GTinh,
                               bn.SThe,
                               bn.NoiTru,
                               MaCSKCB = bn.TuyenDuoi == 0 ? DungChung.Bien.MaBV : (bn.ChuyenKhoa == null ? "" : bn.ChuyenKhoa),
                               NgayRa = kq1 != null ? kq1.NgayRa : null,
                               MaCK = kq1 != null ? kq1.MaKP : 99,
                               khoa = vv.MaKP ,
                               SoNgaydt = kq1 != null ? kq1.SoNgaydt : 0,
                               KetQua = kq1 != null ? kq1.KetQua : "",
                               bn.DTuong,
                               vvien =vv.MaBNhan 
                           }
                                        ).ToList();
                var q0 = (from bn in q11
                          select new
                          {
                              bn.CapCuu,
                              bn.MaBNhan,
                              bn.TuyenDuoi,
                              bn.Tuoi,
                              bn.NNhap,
                              bn.NgaySinh,
                              bn.ThangSinh,
                              bn.NamSinh,
                              bn.GTinh,
                              bn.SThe,
                              bn.NoiTru,
                              bn.MaCSKCB,
                              bn.NgayRa,
                              MaCK = bn.MaCK == 99 ? bn.khoa : bn.MaCK,
                             SoNgaydt= bn.SoNgaydt??0,
                              bn.KetQua,
                              bn.DTuong,
                              bn.vvien
                          }
                         ).ToList();
         
              
                    var q2 = (from bn in q0
                              join ck in _lKhoaP on bn.MaCK equals ck.makp into kq
                              from kq1 in kq.DefaultIfEmpty()
                              select new
                              {
                                  bn.CapCuu,
                                  bn.MaBNhan,
                                  bn.TuyenDuoi,
                                  bn.Tuoi,
                                  bn.GTinh,
                                  bn.SThe,
                                  bn.NoiTru,
                                  bn.MaCSKCB,
                                  bn.NgayRa,
                                  bn.MaCK,
                                  bn.SoNgaydt,
                                  bn.DTuong,
                                  TenKP=kq1!=null? kq1.tenkp:"",
                                  bn.vvien,bn.KetQua

                              }).ToList();
                    var q3 = q2.Distinct().ToList();
                    var q = (from bn in q3
                             group bn by new {  bn.TenKP } into kq
                             select new
                             {
                                 GTinh=0,
                                 Khoa = kq.Key.TenKP,
                                 C3 = kq.Where(p => p.NoiTru == 1 && p.vvien > 0).Count(),
                                 C4 = kq.Where(p => p.NoiTru == 1 && p.vvien > 0).Where(p => p.Tuoi < 15).Count(),
                                 C5 = kq.Where(p => p.NoiTru == 1 && p.vvien > 0).Where(p => p.CapCuu ==1).Count(),
                                 C6 = kq.Where(p => p.NoiTru == 1 && p.vvien > 0).Sum(p => p.SoNgaydt),
                                 C7 = kq.Where(p => p.NoiTru == 1 && p.vvien > 0 && p.KetQua == "Tử vong").Count(),
                                 C8 = kq.Where(p => p.NoiTru == 1 && p.vvien > 0 && p.KetQua == "Tử vong").Where(p => p.Tuoi < 15).Count(),
                                 C9 = kq.Where(p => p.NoiTru == 1 && p.vvien > 0 && p.KetQua == "Tử vong").Where(p => p.SoNgaydt <=1).Count(),
                                 C10 = kq.Where(p => p.NoiTru == 1 && p.vvien > 0 && p.DTuong == "BHYT").Count(),
                                 C11 = kq.Where(p => p.NoiTru == 1 && p.vvien > 0 && p.NgayRa==null).Count(),
                             }).OrderBy(p => p.Khoa).ToList();
                    var qtong = (from bn in q
                                 group bn by bn.GTinh into kq
                                 select new
                                 {
                                     
                                     GTinh = 0,
                                     Khoa = "Tổng số bệnh nhân vào viện",
                                     C3 = kq.Sum(p=>p.C3),
                                     C4 = kq.Sum(p => p.C4),
                                     C5 = kq.Sum(p => p.C5),
                                     C6 = kq.Sum(p => p.C6),
                                     C7 = kq.Sum(p => p.C7),
                                     C8 = kq.Sum(p => p.C8),
                                     C9 = kq.Sum(p => p.C9),
                                     C10 = kq.Sum(p => p.C10),
                                     C11 = kq.Sum(p => p.C11),
                                 }).ToList();
                    var qt = (from bn in q3
                              where bn.GTinh==0
                             group bn by new { bn.GTinh } into kq
                             select new
                             {
                                 GTinh = kq.Key.GTinh??0,
                                 Khoa = "Trong đó nữ",
                                 C3 = kq.Where(p => p.NoiTru == 1 && p.vvien > 0 && p.GTinh==0).Count(),
                                 C4 = kq.Where(p => p.NoiTru == 1 && p.vvien > 0 && p.GTinh == 0).Where(p => p.Tuoi < 15).Count(),
                                 C5 = kq.Where(p => p.NoiTru == 1 && p.vvien > 0 && p.GTinh == 0).Where(p => p.CapCuu == 1).Count(),
                                 C6 = kq.Where(p => p.NoiTru == 1 && p.vvien > 0 && p.GTinh == 0).Sum(p => p.SoNgaydt),
                                 C7 = kq.Where(p => p.NoiTru == 1 && p.vvien > 0 && p.GTinh == 0 && p.KetQua == "Tử vong").Count(),
                                 C8 = kq.Where(p => p.NoiTru == 1 && p.vvien > 0 && p.GTinh == 0 && p.KetQua == "Tử vong").Where(p => p.Tuoi < 15).Count(),
                                 C9 = kq.Where(p => p.NoiTru == 1 && p.vvien > 0 && p.GTinh == 0 && p.KetQua == "Tử vong").Where(p => p.SoNgaydt <= 1).Count(),
                                 C10 = kq.Where(p => p.NoiTru == 1 && p.vvien > 0 && p.GTinh == 0 && p.DTuong == "BHYT").Count(),
                                 C11 = kq.Where(p => p.NoiTru == 1 && p.vvien > 0 && p.GTinh == 0 && p.NgayRa == null).Count(),
                             }).ToList();
                    var tong = qtong.Union(qt).Union(q);
                    BaoCao.Rep_BcHoatDongDTri_24009 rep = new BaoCao.Rep_BcHoatDongDTri_24009();
                    if (txtNgayThang.Text.Trim() != "")
                        rep.TuNgay.Value = txtNgayThang.Text;
                    else
                        rep.TuNgay.Value = "Từ ngày " + lupTuNgay.DateTime.ToShortDateString() + " đến ngày " + lupDenNgay.DateTime.ToShortDateString();
                    frmIn frm = new frmIn();
           

               // _ldtri.Insert(0, nu);

                #region xuat Excel

                //string[] _arr = new string[] { "0", "@", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" };
                //string[] _tieude = { "STT", "Khoa", "Số giường bệnh", "Số người bệnh đầu kỳ", "TS người bệnh điều trị nội trú", "TS người bệnh ĐTNT là TE <15 tuổi", "TS người bệnh ĐTNT cấp cứu", "Số ngày ĐTNT", "TS người bệnh tử vong", "Số người bệnh tử vong là TE<15 tuổi", "Số người bệnh tử vong trước 24 giờ", "Người bệnh có thẻ", "Người bệnh còn lại cuối kỳ" };
                //int[] _arrWidth = new int[] { };// { 5, 15, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 };

                //DungChung.Bien.MangHaiChieu = new Object[_ldtri.Count + 18, 13];
                //DungChung.Bien.MangHaiChieu[0, 0] = DungChung.Bien.TenCQCQ.ToUpper();
                //DungChung.Bien.MangHaiChieu[1, 0] = DungChung.Bien.TenCQ.ToUpper();
                //DungChung.Bien.MangHaiChieu[2, 2] = ("BÁO CÁO HOẠT ĐỘNG  ĐIỀU TRỊ NỘI TRÚ").ToUpper();
                //DungChung.Bien.MangHaiChieu[3, 2] = _ntn;

                //DungChung.Bien.MangHaiChieu[_ldtri.Count() + 5, 7] = "Ngày ...... tháng ..... năm .....";
                //DungChung.Bien.MangHaiChieu[_ldtri.Count() + 6, 1] = ("Người lập biểu").ToUpper();
                //DungChung.Bien.MangHaiChieu[_ldtri.Count() + 10, 1] = DungChung.Bien.NguoiLapBieu;
                //DungChung.Bien.MangHaiChieu[_ldtri.Count() + 6, 4] = ("TRƯỞNG PHÒNG KHTH").ToUpper();
                //DungChung.Bien.MangHaiChieu[_ldtri.Count() + 10, 4] = "";
                //DungChung.Bien.MangHaiChieu[_ldtri.Count() + 6, 7] = ("Giám đốc").ToUpper();
                //DungChung.Bien.MangHaiChieu[_ldtri.Count() + 10, 7] = DungChung.Bien.GiamDoc;
                //for (int i = 0; i < _tieude.Length; i++)
                //{
                //    DungChung.Bien.MangHaiChieu[4, i] = _tieude[i];
                //}
                //int num = 5;
                //foreach (var r in _ldtri)
                //{
                //    DungChung.Bien.MangHaiChieu[num, 0] = num - 4;
                //    DungChung.Bien.MangHaiChieu[num, 1] = r.Khoa;
                //    DungChung.Bien.MangHaiChieu[num, 2] = r.C1;
                //    DungChung.Bien.MangHaiChieu[num, 3] = r.C2;
                //    DungChung.Bien.MangHaiChieu[num, 4] = r.C3;
                //    DungChung.Bien.MangHaiChieu[num, 5] = r.C4;
                //    DungChung.Bien.MangHaiChieu[num, 6] = r.C5;
                //    DungChung.Bien.MangHaiChieu[num, 7] = r.C6;
                //    DungChung.Bien.MangHaiChieu[num, 8] = r.C7;
                //    DungChung.Bien.MangHaiChieu[num, 9] = r.C8;
                //    DungChung.Bien.MangHaiChieu[num, 10] = "";
                //    DungChung.Bien.MangHaiChieu[num, 11] = r.C10;
                //    DungChung.Bien.MangHaiChieu[num, 12] = r.C11;

                //    num++;

                //}

                #endregion
             //   frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "Báo cáo hoạt động điều trị", "C:\\BcHDDT.xls", true, this.Name);
                // frmIn frm = new frmIn();



                    rep.DataSource = tong;
                rep.BindingData();
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();

            }

        }
        /// <summary>
        /// Lấy số ngày điều trị tại 1 khoa
        /// </summary>
        /// <param name="ngaykham"></param>
        /// <param name="ngayra"></param>
        /// <param name="idkb"></param>
        /// <param name="maBNhan"></param>
        /// <returns></returns>
        private int songaydt(DateTime? ngaykham, DateTime? ngayra, int idkb, int maBNhan, List<BNKB> allBNKB)
        {
            var bnkb = (from a in allBNKB.Where(p => p.MaBNhan == maBNhan) join b in _Kphong on a.MaKP equals b.makp select a).OrderBy(p => p.IDKB).ToList();
            int sngaydt = 0;

            if (ngaykham == null)
                return 0;
            else
            {
                int num = 1;
                DateTime _ngayra = DungChung.Ham.NgayTu(ngayra ?? DateTime.Now);
                foreach (var a in bnkb)
                {
                    if (a.IDKB == idkb)
                    {
                        DateTime _ngaykham = DungChung.Ham.NgayTu(ngaykham.Value);

                        if (num == bnkb.Count())//id khám bệnh là id lớn nhất
                        {
                            sngaydt = (int)(_ngayra - _ngaykham).TotalDays;
                            break;
                        }
                        else
                        {
                            DateTime _ngaykhamTiep = bnkb.Skip(num).First().NgayKham.Value;
                            sngaydt = (int)(_ngayra - _ngaykham).TotalDays;
                            break;
                        }
                    }
                    num++;
                }
                if (num == 1 && ngaykham.Value.Hour < 20)//lần khám đầu tiên
                    sngaydt = sngaydt + 1;
                return sngaydt;
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
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
    }
}