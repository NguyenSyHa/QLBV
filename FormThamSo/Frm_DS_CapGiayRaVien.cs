using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;



namespace QLBV.FormThamSo
{
    public partial class Frm_DS_CapGiayRaVien : DevExpress.XtraEditors.XtraForm
    {
        public Frm_DS_CapGiayRaVien()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        List<KhoaPhong> KhoaPhongs = new List<KhoaPhong>();
        private void Frm_DS_CapGiayRaVien_Load(object sender, EventArgs e)
        {
            dtpDenNgay.DateTime = dtpTuNgay.DateTime = DateTime.Now;
            KhoaPhongs = (from kps in data.KPhongs.Where(p => p.PLoai == "Lâm sàng" || p.PLoai == "Phòng khám") select new KhoaPhong { MaKP = kps.MaKP, TenKP = kps.TenKP }).ToList();
            KhoaPhongs.Add(new KhoaPhong { MaKP = 0, TenKP = "Tất cả" });
            lupKhoaPhong.Properties.DataSource = KhoaPhongs.OrderBy(p => p.MaKP).ToList();
            lupLayTheoNgay.SelectedIndex = 0;
            cboDoiTuong.SelectedIndex = 0;
            lupKhoaPhong.EditValue = 0;

        }
        private class KhoaPhong
        {
            public int MaKP { get; set; }
            public string TenKP { get; set; }
        }
        private class DSbn
        {
            public string SoLT { get; set; }
            public string MaCS { get; set; }
            public string MaQD { get; set; }
            public string SThe { get; set; }
            public string MaThe { get; set; }
            public string DChi { get; set; }
            public string GTinhs { get; set; }
            public string NgaySinhS { get; set; }
            public string NgaySinh { get; set; }
            public string TenBNhan { get; set; }
            public string ThangSinh { get; set; }
            public string NamSinh { get; set; }
            public string TenDT { get; set; }
            public int? GTinh { get; set; }
            public string TenNN { get; set; }
            public string NgayVao { get; set; }
            public string NgayRa { get; set; }
            public string MaICD { get; set; }
            public string PPDT { get; set; }
            public string LoiDan { get; set; }
            public string GiamDoc { get; set; }
            public string MaTK { get; set; }
            public string TruongKhoa { get; set; }
            public string HoTenBo { get; set; }
            public string HoTenMe { get; set; }
            public string HoTenBoMe { get; set; }
            public string NguoiDaiDien { get; set; }
            public string NgayCT { get; set; }

        }
        List<DSbn> DSbns = new List<DSbn>();

        private void btnTaoBaoCao_Click(object sender, EventArgs e)
        {
            DSbns.Clear();
            int MaKP = 0;
            if (lupKhoaPhong.EditValue != "")
            {
                MaKP = Convert.ToInt32(lupKhoaPhong.EditValue);

            }

            var BenhNhan = (from bn in data.BenhNhans.Where(p => (cboDoiTuong.SelectedIndex == 0 ? true : p.DTuong.Contains(cboDoiTuong.Text)))
                            join ttbx in data.TTboXungs on bn.MaBNhan equals ttbx.MaBNhan
                            join kp in data.KPhongs.Where(p => (MaKP == 0 ? true : p.MaKP == MaKP)) on bn.MaKP equals kp.MaKP
                            join rv in data.RaViens on bn.MaBNhan equals rv.MaBNhan
                            select new { bn, ttbx, kp, rv }).ToList();

            DateTime TuNgay = DungChung.Ham.NgayTu(dtpTuNgay.DateTime);
            DateTime DenNgay = DungChung.Ham.NgayDen(dtpDenNgay.DateTime);
            int IndexValue = lupLayTheoNgay.SelectedIndex;
            if (IndexValue == 0)
            {
                DSbns = (from bn in BenhNhan.Where(p => p.bn.NoiTru == 1 || p.bn.DTNT == true)
                         join vp in data.VienPhis.Where(p => p.NgayTT >= TuNgay && p.NgayTT <= DenNgay) on bn.bn.MaBNhan equals vp.MaBNhan
                         select new DSbn
                         {
                             SoLT = bn.rv.SoLT, //MA_CT
                             NgaySinhS = bn.bn.NgaySinh + "/" + bn.bn.ThangSinh + "/" + bn.bn.NamSinh, //
                             MaThe = bn.bn.SThe == "" ? "" : bn.bn.SThe.Substring(5, 10),
                             GTinhs = bn.bn.GTinh == 1 ? "Nam" : "Nữ",
                             MaCS = bn.bn.MaKCB, //MA_CS
                             MaQD = bn.kp.MaQD,
                             TenBNhan = bn.bn.TenBNhan,
                             SThe = bn.bn.SThe,
                             DChi = bn.bn.DChi,
                             NgaySinh = bn.bn.NgaySinh,
                             ThangSinh = bn.bn.ThangSinh,
                             NamSinh = bn.bn.NamSinh,
                             GTinh = bn.bn.GTinh,
                             TenNN = bn.ttbx.MaNN == null ? "" : data.DmNNs.FirstOrDefault(p => p.MaNN == bn.ttbx.MaNN).TenNN,
                             NgayVao = DungChung.Ham.NgaySangChu(Convert.ToDateTime(bn.rv.NgayVao), 11),
                             NgayRa = DungChung.Ham.NgaySangChu(Convert.ToDateTime(bn.rv.NgayRa), 11),
                             MaICD = bn.rv.ChanDoan,
                             PPDT = bn.rv.PPDTr,
                             LoiDan = bn.rv.LoiDan,
                             TenDT = bn.ttbx.MaDT,
                             MaTK = bn.kp.MaTK,
                             HoTenBoMe = bn.ttbx.HoTenBoMe,
                             TruongKhoa = bn.kp.MaTK == null ? "" : data.CanBoes.Where(p => p.MaCB == bn.kp.MaTK).FirstOrDefault().TenCB,
                             
                             HoTenBo = bn.ttbx.HoTenBoMe == null ? "" : bn.ttbx.HoTenBoMe.Split(';')[0], //HO_TEN_BO
                             HoTenMe = bn.ttbx.HoTenBoMe == null ? "" : bn.ttbx.HoTenBoMe.Split(';')[1], //HO_TEN_ME
                             NguoiDaiDien = DungChung.Bien.GiamDoc, //NGUOI_DAI_DIEN
                             NgayCT = DungChung.Ham.NgaySangChu(Convert.ToDateTime(bn.rv.NgayRa), 12), //NGAY_CT
                         }).ToList();
            }
            else
            {
                DSbns = (from bn in BenhNhan.Where(p => p.rv.NgayRa >= TuNgay && p.rv.NgayRa <= DenNgay && (p.bn.NoiTru == 1 || p.bn.DTNT == true))
                         select new DSbn
                         {
                             SoLT = bn.rv.SoLT, //MA_CT
                             NgaySinhS = bn.bn.NgaySinh + "/" + bn.bn.ThangSinh + "/" + bn.bn.NamSinh, //NGAY_SINH
                             MaThe = bn.bn.SThe == "" ? "" : bn.bn.SThe.Substring(5, 10), 
                             GTinhs = bn.bn.GTinh == 1 ? "Nam" : "Nữ",
                             MaCS = bn.bn.MaKCB, //MA_CSKCB
                             MaQD = bn.kp.MaQD, //MA_KHOA
                             TenBNhan = bn.bn.TenBNhan, //HO_TEN
                             SThe = bn.bn.SThe, //MA_SOBHXH + MA_THE
                             DChi = bn.bn.DChi, //DIA_CHI
                             NgaySinh = bn.bn.NgaySinh,
                             ThangSinh = bn.bn.ThangSinh,
                             NamSinh = bn.bn.NamSinh,
                             GTinh = bn.bn.GTinh, //GIOI_TINH
                             TenNN = bn.ttbx.MaNN == null ? "" : data.DmNNs.FirstOrDefault(p => p.MaNN == bn.ttbx.MaNN).TenNN, //NGHE_NGHIEP
                             NgayVao = DungChung.Ham.NgaySangChu(Convert.ToDateTime(bn.rv.NgayVao), 11), //NGAY_VAO
                             NgayRa = DungChung.Ham.NgaySangChu(Convert.ToDateTime(bn.rv.NgayRa), 11), //NGAY_RA
                             MaICD = bn.rv.ChanDoan, //CHAN_DOAN
                             PPDT = bn.rv.PPDTr, //PP_DIEUTRI
                             LoiDan = bn.rv.LoiDan, //GHI_CHU
                             TenDT = bn.ttbx.MaDT, //DAN_TOC
                             MaTK = bn.kp.MaTK, //MA_TRUONGKHOA
                             TruongKhoa = bn.kp.MaTK == null ? "" : data.CanBoes.Where(p => p.MaCB == bn.kp.MaTK).FirstOrDefault().TenCB, //TEN_TRUONGKHOA
                             HoTenBoMe = bn.ttbx.HoTenBoMe,
                             HoTenBo = bn.ttbx.HoTenBoMe == null ? "" : bn.ttbx.HoTenBoMe.Split(';')[0], //HO_TEN_BO
                             HoTenMe = bn.ttbx.HoTenBoMe == null ? "" : bn.ttbx.HoTenBoMe.Split(';')[1], //HO_TEN_ME
                             NguoiDaiDien = DungChung.Bien.GiamDoc, //NGUOI_DAI_DIEN
                             NgayCT = DungChung.Ham.NgaySangChu(Convert.ToDateTime(bn.rv.NgayRa), 12), //NGAY_CT
                         }).ToList();
            }

            if (DSbns.Count > 0)
            {
                
                DungChung.Ham.Print(DungChung.PrintConfig.Rep_DSGiayRaVien_ID552, DSbns.ToList(), new Dictionary<string, object>(), false);
                if (chkXuatEX.Checked == true)
                {
                   
                    string[] _arr = new string[] { "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@" };
                    int[] _arrWidth = new int[] { };
                    string[] _tieude = { "STT", "MA_CT", "MA_CSKCB", "SO_SERI", "MA_KHOA", "MA_SOBHXH", "MA_THE", "HO_TEN", "DIA_CHI", "NGAY_SINH", "DAN_TOC", "GIOI_TINH", "NGHE_NGHIEP", "NGAY_VAO", "NGAY_RA", "TUOI_THAI", "CHAN_DOAN", "PP_DIEUTRI", "GHI_CHU", "NGUOI_DAI_DIEN", "MA_TRUONGKHOA", "NGAY_CT", "TEN_TRUONGKHOA", "HO_TEN_CHA", "HO_TEN_ME", "TEKT" };
                    DungChung.Bien.MangHaiChieu = new Object[DSbns.ToList().Count + 1, 26];
                    for (int i = 0; i < _tieude.Length; i++)
                    {
                        DungChung.Bien.MangHaiChieu[0, i] = _tieude[i];
                    }

                    int num = 1;               
                    foreach (var r in DSbns.ToList())
                    {
                        string[] HoTenBoMe = new string[] { "", "" };
                        if (r.HoTenBoMe!= null)
                        {
                            HoTenBoMe = r.HoTenBoMe.Split(';');
                            if (HoTenBoMe.Count() == 1)
                            {
                                HoTenBoMe = new string[] { HoTenBoMe[0], "" };
                            }
                            else
                            {
                                HoTenBoMe = r.HoTenBoMe.Split(';');
                            }

                        }
                        DungChung.Bien.MangHaiChieu[num, 0] = num;
                        DungChung.Bien.MangHaiChieu[num, 1] = r.SoLT;
                        DungChung.Bien.MangHaiChieu[num, 2] = r.MaCS;
                        DungChung.Bien.MangHaiChieu[num, 3] = "";
                        DungChung.Bien.MangHaiChieu[num, 4] = r.MaQD;
                        DungChung.Bien.MangHaiChieu[num, 5] = r.SThe == "" ? "" : r.SThe.Substring(5, 10);
                        DungChung.Bien.MangHaiChieu[num, 6] = r.SThe;
                        DungChung.Bien.MangHaiChieu[num, 7] = r.TenBNhan;
                        DungChung.Bien.MangHaiChieu[num, 8] = r.DChi;
                        DungChung.Bien.MangHaiChieu[num, 9] = r.NgaySinh + "/" + r.ThangSinh + "/" + r.NamSinh;
                        DungChung.Bien.MangHaiChieu[num, 10] = r.TenDT == null ? "" : (data.DanTocs.Where(p => p.MaDT == r.TenDT).Count() > 0 ? data.DanTocs.Where(p => p.MaDT == r.TenDT).First().MaDanToc : "");
                        DungChung.Bien.MangHaiChieu[num, 11] = r.GTinh == 0 ? 2 : 1; // nếu giới tính (0-nữ = 2) (1 nam)
                        DungChung.Bien.MangHaiChieu[num, 12] = r.TenNN;
                        DungChung.Bien.MangHaiChieu[num, 13] = r.NgayVao;
                        DungChung.Bien.MangHaiChieu[num, 14] = r.NgayRa;
                        DungChung.Bien.MangHaiChieu[num, 15] = "";
                        DungChung.Bien.MangHaiChieu[num, 16] = r.MaICD;
                        DungChung.Bien.MangHaiChieu[num, 17] = r.PPDT;
                        DungChung.Bien.MangHaiChieu[num, 18] = r.LoiDan;
                        DungChung.Bien.MangHaiChieu[num, 19] = DungChung.Bien.GiamDoc;
                        DungChung.Bien.MangHaiChieu[num, 20] = r.MaTK == null ? "" : data.CanBoes.Where(p => p.MaCB == r.MaTK).FirstOrDefault().MaCCHN;
                        DungChung.Bien.MangHaiChieu[num, 21] = DungChung.Ham.NgaySangChu(Convert.ToDateTime(r.NgayRa), 12);
                        DungChung.Bien.MangHaiChieu[num, 22] = r.MaTK == null ? "" : data.CanBoes.Where(p => p.MaCB == r.MaTK).FirstOrDefault().TenCB;
                        DungChung.Bien.MangHaiChieu[num, 23] = HoTenBoMe[0].ToString();
                        DungChung.Bien.MangHaiChieu[num, 24] = HoTenBoMe[1].ToString();
                        DungChung.Bien.MangHaiChieu[num, 25] = "";
                        num++;
                    }
                    SaveFileDialog op = new SaveFileDialog();
                    string filepatd;
                    if (op.ShowDialog() == DialogResult.OK)
                    {
                        filepatd = op.FileName + ".xls";
                    }
                    else
                    {
                        filepatd = "C:\\DS_CapGiayRaVien" + DateTime.Now + ".xls";
                    }

                    DungChung.Ham.xuatExcel(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "Sheet1", filepatd, true);
                    XtraMessageBox.Show("Xuất File thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                XtraMessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

    }
}
