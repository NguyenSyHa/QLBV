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
    public partial class frm_SoThuThuat_12121 : DevExpress.XtraEditors.XtraForm
    {
        public frm_SoThuThuat_12121()
        {
            InitializeComponent();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void btnOK_Click(object sender, EventArgs e)
        {
            DateTime tungay = DungChung.Ham.NgayTu(dtTuNgay.DateTime);
            DateTime denngay = DungChung.Ham.NgayDen(dtDenNgay.DateTime);
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

            List<KPhong> _lKhoaP = new List<KPhong>();
            _lKhoaP = _Kphong.Where(p => p.makp != 0).Where(p => p.chon == true).ToList();
            if (denngay >= tungay)
            {
                var qbn = (from bn in data.BenhNhans
                           join dtbn in data.DTBNs on bn.IDDTBN equals dtbn.IDDTBN
                           //join rv in data.RaViens  on bn.MaBNhan equals rv.MaBNhan into kq
                           //from kq1 in kq.DefaultIfEmpty()
                           select new
                           {
                               bn.TenBNhan,
                               bn.MaBNhan,
                               bn.Tuoi,
                               bn.MaDTuong,
                               bn.GTinh,
                               dtbn.DTBN1,
                               //NgayRa = (kq1 == null || (kq1 != null && kq1.NgayRa > denngay)) ? null : kq1.NgayRa
                           }).ToList();

                List<TH> qcls = (from cls in data.CLS.Where(p=>p.NgayTH <= denngay && p.NgayTH >= tungay)
                            join cd in data.ChiDinhs.Where(p=>p.Status == 1)
                            on cls.IdCLS equals cd.IdCLS
                            join dv in data.DichVus on cd.MaDV equals dv.MaDV
                            join tn in data.TieuNhomDVs.Where(p=>p.TenRG == "Thủ thuật") on dv.IdTieuNhom equals tn.IdTieuNhom
                            select new TH {                            
                           MaBNhan =  cls.MaBNhan??0,MaKP = cls.MaKP??0,NgayTH = cls.NgayTH, 
                           MaDV =  cd.MaDV??0, TenDV = dv.TenDV ?? "", TenDVlower = dv.TenDV == null ? "" : dv.TenDV.ToLower()                       
                            }).ToList();

                List<TH> qtaipk = (from dt in data.DThuocs.Where(p => p.NgayKe >= tungay && p.NgayKe <= denngay)                                
                                   join dtct in data.DThuoccts.Where(p=>p.IDCD == null || p.IDCD <=0) on dt.IDDon equals dtct.IDDon                                 
                                   join dv in data.DichVus on dtct.MaDV equals dv.MaDV
                                   join tn in data.TieuNhomDVs.Where(p => p.TenRG == "Thủ thuật") on dv.IdTieuNhom equals tn.IdTieuNhom
                                   select new TH
                                   {
                                       MaBNhan = dt.MaBNhan??0,
                                       MaKP = dt.MaKP ?? 0,
                                       NgayTH = dt.NgayKe,
                                       MaDV = dv.MaDV,
                                       TenDV = dv.TenDV ?? "",
                                       TenDVlower = dv.TenDV == null ? "" : dv.TenDV.ToLower()
                                   }).ToList();
                List<TH> tong = new List<TH>();
                if (ckCochiDinhCLs.Checked)
                    tong.AddRange(qcls);
                if (ckKoChiDinh.Checked)
                    tong.AddRange(qtaipk);
               
                List<BNKB> qkb = data.BNKBs.Where(p => p.NgayKham <= denngay).ToList();
#region Fix cứng theo tên dịch vụ
                // lấy tất cả
               var q1 = (from bn in qbn
                         join cls in tong on bn.MaBNhan equals cls.MaBNhan
                         //join bnkb in data.BNKBs on new { cls.MaBNhan, cls.MaKP } equals new { bnkb.MaBNhan, bnkb.MaKP }
                         join kp in _lKhoaP on cls.MaKP equals kp.makp
                         select new
                             {
                                 NgayThang = cls.NgayTH.Value.Date,
                                 bn.TenBNhan,
                                // bnkb.ChanDoan,
                                // bnkb.IDKB,
                                 Tuoi = bn.Tuoi == 0 ? null : bn.Tuoi,
                                 bn.GTinh,                                 
                                 bn.MaDTuong,
                                 bn.MaBNhan,
                                 bn.DTBN1,
                                 cls.TenDV,
                                 cls.MaDV,
                                 cls.MaKP,// mã khoa chỉ định CLS
                                 DienCham = cls.TenDVlower.Contains("điện châm") || cls.TenDVlower.Contains("điện mãng châm") ? 1 : 0,
                                 ThuyCham = cls.TenDVlower.Contains("thủy châm") ? 1 : 0,
                                 Cuu = cls.TenDVlower.StartsWith("cứu") ? 1 : 0,
                                 Giac = cls.TenDVlower.StartsWith("giác") ? 1 : 0,
                                 TaTay = cls.TenDVlower.StartsWith("tạ tay") ? 1 : 0, // ko có trong danh mục
                                 TapVanDong = cls.TenDVlower.StartsWith("tập vận động") ? 1 : 0,// có trong DM mới nhưng 1 số DV chỉ có tên trong tên thông tư 37
                                 Xoabop = cls.TenDVlower.Contains("xoa bóp bấm huyệt")? 1 : 0,
                                 XongHoiThuoc = cls.TenDVlower.Contains("xông hơi thuốc")? 1 : 0,
                                 ThuyTriLieu = cls.TenDVlower.StartsWith("thủy trị liệu") ? 1 : 0,
                                 SongXungKich = cls.TenDVlower.Contains("sóng xung kích") ? 1 : 0,
                                 SongNgan = cls.TenDVlower.Contains("sóng ngắn") || cls.TenDVlower.Contains("sóng cực ngắn") ? 1 : 0, // tên thông tư 37 (thiếu vi sóng, đèn led)
                                 HongNgoai = cls.TenDVlower.Contains("hồng ngoại") ? 1 : 0,// trùng với sóng ngắn // tên thông tư 37
                                 DienTuTruong = (cls.TenDVlower.Contains("điện từ trường") || cls.TenDVlower == "điều trị bằng từ trường"  || cls.TenDVlower == "điều trị bằng điện trường cao áp" || cls.TenDVlower == "điều trị bằng ion tĩnh điện" || cls.TenDVlower == "điều trị bằng tĩnh điện trường") ? 1 : 0,// tên thông tư 37
                                 KeoGianCotSong = cls.TenDVlower.Contains("kéo giãn cột sống") ? 1 : 0,
                                 TapXeDap = cls.TenDVlower == "tập với xe đạp tập" ? 1 : 0,
                                 TapRongRoc = (cls.TenDVlower == "tập với ròng rọc" || cls.TenDVlower == "tập với hệ thống ròng rọc") ? 1 : 0,
                                 Xoabopcucbo = cls.TenDVlower == "xoa bóp cục bộ bằng tay" || cls.TenDVlower=="kỹ thuật xoa bóp vùng" ? 1 : 0, // tên thông tư 37
                                 DienViDongGiamDau = cls.TenDVlower.Contains("điện vi dòng") ? 1 : 0, //điện vi dòng giảm đau- tên thông tư 37,
                                 CayChi = cls.TenDVlower.Contains("cấy chỉ") ? 1 : 0
                             }).ToList();

                // Lấy tổng số lượng theo từng dịch vụ
               var q2 = (from bn in q1
                         group bn by new { bn.NgayThang, bn.TenBNhan,  bn.Tuoi, bn.GTinh, bn.MaDTuong, bn.MaBNhan, bn.DTBN1, bn.MaKP } into kq
                         select new {
                             kq.Key.NgayThang, kq.Key.TenBNhan,
                             ChanDoan = getchanDoan(kq.Key.MaBNhan, kq.Key.MaKP, kq.Key.NgayThang, qkb) ,
                             kq.Key.Tuoi, kq.Key.GTinh, kq.Key.MaDTuong, kq.Key.MaBNhan, kq.Key.DTBN1,
                             TuoiNam = kq.Key.GTinh == 1 ? kq.Key.Tuoi : null,
                             TuoiNu = (kq.Key.GTinh == 0 || kq.Key.GTinh == null) ? kq.Key.Tuoi : null,
                             BHYT = kq.Key.DTBN1 == "BHYT" ? "x" : "",
                             NguoiNgheo = (kq.Key.MaDTuong != null && kq.Key.MaDTuong.ToString().ToLower() == "hn") ? "x" : "",
                             DtuongVP = (kq.Key.DTBN1 != "BHYT" && kq.Key.DTBN1 != "KSK") ? "x" : "",
                             KSK = kq.Key.DTBN1 == "KSK" ? "x" : "",
                             DienCham = kq.Where(p=>p.DienCham == 1).Count(),
                             ThuyCham = kq.Where(p=>p.ThuyCham == 1).Count(),
                             Cuu = kq.Where(p => p.Cuu == 1).Count(),
                             Giac = kq.Where(p => p.Giac == 1).Count(),
                             TaTay = kq.Where(p => p.TaTay == 1).Count(),
                             TapVanDong = kq.Where(p => p.TapVanDong == 1).Count(),
                             Xoabop = kq.Where(p => p.Xoabop == 1).Count(),
                             XongHoiThuoc = kq.Where(p => p.XongHoiThuoc == 1).Count(),
                             ThuyTriLieu = kq.Where(p => p.ThuyTriLieu == 1).Count(),
                             SongXungKich = kq.Where(p => p.SongXungKich == 1).Count(),
                             SongNgan = kq.Where(p => p.SongNgan == 1).Count(),// tên thông tư 37 (thiếu vi sóng, đèn led)
                             HongNgoai = kq.Where(p => p.HongNgoai == 1).Count(),// trùng với sóng ngắn // tên thông tư 37
                             DienTuTruong = kq.Where(p => p.DienTuTruong == 1).Count(),// tên thông tư 37
                             KeoGianCotSong = kq.Where(p => p.KeoGianCotSong == 1).Count(),
                             TapXeDap = kq.Where(p => p.TapXeDap == 1).Count(),
                             TapRongRoc = kq.Where(p => p.TapRongRoc == 1).Count(),                             
                             Xoabopcucbo = kq.Where(p => p.Xoabopcucbo == 1).Count(), // tên thông tư 37
                             DienViDongGiamDau = kq.Where(p => p.DienViDongGiamDau == 1).Count(), //điện vi dòng giảm đau- tên thông tư 37,
                             CayChi = kq.Where(p => p.CayChi == 1).Count(),
                         }).OrderBy(p=>p.NgayThang).ThenBy(p=>p.TenBNhan).ToList();
               if (q2.Count > 0)
               {
                   frmIn frm = new frmIn();
                   BaoCao.rep_SoThuThuat_12121_1 rep = new BaoCao.rep_SoThuThuat_12121_1();
                   rep.lbl_thoigian.Text = "Từ ngày " + tungay.ToString("dd/MM/yyyy") + " đến ngày " + denngay.ToString("dd/MM/yyyy");
                   rep.DataSource = q2;
                   rep.BindingData();
                   rep.CreateDocument();
                   frm.prcIN.PrintingSystem = rep.PrintingSystem;
                   frm.ShowDialog();

                   frmIn frm2 = new frmIn();
                   BaoCao.rep_SoThuThuat_12121_2 rep2 = new BaoCao.rep_SoThuThuat_12121_2();
                   rep2.lbl_thoigian.Text = "Từ ngày " + tungay.ToString("dd/MM/yyyy") + " đến ngày " + denngay.ToString("dd/MM/yyyy");
                   rep2.DataSource = q2;
                   rep2.BindingData();
                   rep2.CreateDocument();
                   frm2.prcIN.PrintingSystem = rep2.PrintingSystem;
                   frm2.ShowDialog();
               }else
                   MessageBox.Show("Không có dữ liệu");
#endregion

            }
            else
            {
                MessageBox.Show("Ngày đến phải lớn hơn ngày từ");
            }
        }

        /// <summary>
        /// Lấy chẩn đoán của bệnh nhân tại khoa phòng MaKP
        /// Chẩn đoán là chẩn đoán cuối cùng tại khoa phòng đó, thời gian khám phải nhỏ hơn hoặc bằng thời gian chỉ định
        /// </summary>
        /// <param name="MaBNhan"></param>
        /// <param name="MaKP">mã khoa phòng chỉ định</param>
        /// <param name="ngayTH">ngày thực hiện dịch vụ CLS</param>
        /// <returns></returns>
        private string getchanDoan(int MaBNhan, int MaKP, DateTime ngayTH, List<BNKB> qkb)
        {            
            var q = qkb.Where(p => p.MaBNhan == MaBNhan && p.MaKP == MaKP
                && p.NgayKham != null && p.NgayKham.Value.Date <= ngayTH
                )
                .OrderByDescending(p => p.IDKB)
                .FirstOrDefault();
            if (q != null)
                return q.ChanDoan;
            else return "";
        }

        private class TH
        { 
            public int MaBNhan { set; get; }
            public int MaKP { set; get; }
            public DateTime? NgayTH { set; get; }
           
            public int MaDV { set; get; }
            public string  TenDV { set; get; }
            public string TenDVlower { set; get; }
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
        private void frm_SoThuThuat_12121_Load(object sender, EventArgs e)
        {
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            dtTuNgay.DateTime = DateTime.Now;
            dtDenNgay.DateTime = DateTime.Now;

            var kphong = (from kp in data.KPhongs.Where(p => p.PLoai == "Lâm sàng" || p.PLoai == "Cận lâm sàng" || p.PLoai == "Phòng khám")
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
    }
}