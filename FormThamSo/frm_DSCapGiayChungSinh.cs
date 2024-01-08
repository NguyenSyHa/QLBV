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
    public partial class frm_DSCapGiayChungSinh : DevExpress.XtraEditors.XtraForm
    {
        public frm_DSCapGiayChungSinh()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        class KhoaPhongs
        {
            public int makp { get; set; }
            public string tenkp { get; set; }
        }
        List<KhoaPhongs> kphong = new List<KhoaPhongs>();
        private void frm_DSCapGiayChungSinh_Load(object sender, EventArgs e)
        {
            dtpTuNgay.DateTime = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy" + " " + "00:00:00"));
            dtpDenNgay.DateTime = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy" + " " + "23:59:59"));
            checkedListBoxControlDoiTuong.DataSource = _data.DTBNs.ToList();
            kphong = (from Kp in _data.KPhongs.Where(p => p.PLoai == "Phòng khám" || p.PLoai == "Lâm sàng") select new KhoaPhongs { makp = Kp.MaKP, tenkp = Kp.TenKP }).ToList();
            kphong.Add(new KhoaPhongs { makp = 0, tenkp = "Tất cả" });
            lupKhoaPhong.Properties.DataSource = kphong.OrderBy(p => p.makp).ToList();
            lupKhoaPhong.EditValue = 0;
            checkedListBoxControlDoiTuong.CheckAll();
        }
        class DsNhanGiayChungSinh
        {
            //"STT", "MA_CT", "MA_CSKCB", "MA_THE", "SO_SERI", "MA_SOBHXH_ME", "HO_TEN_ME", "NGAY_SINH", "DIA_CHI", "CMND", "NGAY_CAP_CMND", "NOI_CAP_CMND", "DAN_TOC", "HO_TEN_CHA", "NGAY_SINHCON", "NOI_SINH_CON", "TEN_CON", "SO_CON", "GIOI_TINH_CON", "CAN_NANG_CON", "TINH_TRANG_CON", "GHI_CHU", "NGUOI_DO_DE", "NGUOI_GHI_PHIEU", "NGUOI_DAI_DIEN", "NGAY_CT", "SINHCON_PHAUTHUAT", "SINHCON_DUOI32TUAN", "SO", "QUYEN_SO"
            public byte IDDTBN { get; set; }
            public string MCS { get; set; }
            public string SThe { get; set; }
            public string SThe10 { get; set; }
            public string TenBNhan { get; set; }
            public string NamSinh { get; set; }
            public string DChi { get; set; }
            public string SoKSinh { get; set; }
            public DateTime? NgayCap { get; set; }
            public string MaDT { get; set; }
            public string TenCon { get; set; }
            public string TenBo { get; set; }
            public DateTime? ThoiGianSinh { get; set; }
            public string ThoiGianSinh2 { get; set; }
            public string DiaDiemSinh { get; set; }
            public int? SoCon { get; set; }
            public string GhiChu { get; set; }
            public string NguoiDoDe { get; set; }
            public string QuyenSo { get; set; }
            public string So { get; set; }
            public int? GioiTinhCon { get; set; }
            public int? CanNangCon { get; set; }
            public string NguoiGhiPhieu { get; set; }
            public string NgayChungTu { get; set; }
            public string NGuoiDaiDien { get; set; }
            public string NoiCapCMT { get; set; }
            public string Quyen { get; set; }
            public int SinhCon_Duoi32Tuan { get; set; }
            public int SinhCon_PhauThuat { get; set; }
            
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            List<DsNhanGiayChungSinh> ListBn = new List<DsNhanGiayChungSinh>();
            DateTime TuNgay = dtpTuNgay.DateTime;
            DateTime NgayDen = dtpDenNgay.DateTime;
            int Makp = Convert.ToInt32(lupKhoaPhong.EditValue);
            var checkItem = checkedListBoxControlDoiTuong.CheckedItems;
            List<byte> idDTBNs = new List<byte>();
            foreach (var item in checkItem)
            {
                DTBN dtbn = (DTBN)item;
                if (dtbn != null)
                    idDTBNs.Add(dtbn.IDDTBN);
            }
            ListBn = (from a in _data.BenhNhans.Where(p => (Makp == 0 || Makp == null) ? true : p.MaKP == Makp)
                      join b in _data.TTboXungs on a.MaBNhan equals b.MaBNhan
                      join d in _data.TheoDoiThaiSans.Where(p => p.ThoiGianSinh >= TuNgay && p.ThoiGianSinh <= NgayDen) on a.MaBNhan equals d.MaBNhan
                      select new DsNhanGiayChungSinh
                      {
                          IDDTBN = a.IDDTBN,
                          MCS = a.MaKCB,
                          SThe = a.SThe == "" ? "" : a.SThe,
                          SThe10 = a.SThe == "" ? "" : a.SThe.Substring(5, 10),
                          TenBNhan = a.TenBNhan,
                          NamSinh = a.NgaySinh + "/" + a.ThangSinh + "/" + a.NamSinh,
                          DChi = a.DChi,
                          SoKSinh = b.SoKSinh,
                          NgayCap = (b.NgayCapCMT == null ? null : b.NgayCapCMT),
                          MaDT = b.MaDT == "" ? "" : _data.DanTocs.Where(p => p.MaDT == b.MaDT).FirstOrDefault().MaDanToc,
                          TenBo = d.TenBo,
                          TenCon = d.Ten1,
                          DiaDiemSinh = d.DiaDiemSinh,
                          ThoiGianSinh = d.ThoiGianSinh,
                          CanNangCon = d.CanNang1,
                          SoCon = d.SoCon,
                          
                          GhiChu = d.GhiChu,
                          NguoiDoDe = d.NguoiDoDe,
                          QuyenSo = d.QuyenSo,
                          So = d.SoChungSinh,
                          GioiTinhCon = d.GioiTinhCon1,
                          NguoiGhiPhieu = d.NguoiGhiPhieu,
                          NoiCapCMT = b.NoiCapCMT,
                          Quyen = d.QuyenSo,
                          SinhCon_Duoi32Tuan = d.SinhCon_Duoi32Tuan == null ? 0 : (d.SinhCon_Duoi32Tuan == true ? 1 : 0),
                          SinhCon_PhauThuat = d.SinhCon_PhauThuat == null ? 0 : (d.SinhCon_PhauThuat == true ? 1 : 0)
                      }).Where(o => idDTBNs.Contains(o.IDDTBN)).ToList();

            if (ListBn.Count > 0)
            {
                List<DsNhanGiayChungSinh> listrep = new List<DsNhanGiayChungSinh>();
                foreach (var item in ListBn)
                {
                    DsNhanGiayChungSinh themoi = new DsNhanGiayChungSinh();
                    themoi.MCS = item.MCS; //MA_CSKCB
                    themoi.SThe = item.SThe; //MA_SOBHXH_ME
                    themoi.SThe10 = item.SThe10; //MA_THE10
                    themoi.TenBNhan = item.TenBNhan; //HO_TEN_ME
                    themoi.NamSinh = item.NamSinh; //NGAY_SINH
                    themoi.DChi = item.DChi; //DIA_CHI
                    themoi.SoKSinh = item.SoKSinh; //CMND
                    themoi.NgayCap = item.NgayCap; //NGAY_CAP_CMND
                    themoi.NoiCapCMT = item.NoiCapCMT; //NOI_CAP_CMND
                    themoi.TenBo = item.TenBo; //HO_TEN_CHA
                    themoi.TenCon = item.TenCon; //TEN_CON
                    themoi.ThoiGianSinh2 = DungChung.Ham.NgaySangChu(Convert.ToDateTime(item.ThoiGianSinh), 12); //NGAY_SINHCON
                    themoi.DiaDiemSinh = item.DiaDiemSinh; //NOI_SINH_CON
                    themoi.TenCon = item.TenCon; //HO_TEN_CON
                    themoi.GioiTinhCon = item.GioiTinhCon; //GIOI_TINHCON
                    themoi.SoCon = item.SoCon; //SO_CON
                    themoi.CanNangCon = item.CanNangCon; //CAN_NANG_CON
                    themoi.GhiChu = item.GhiChu; //GHI_CHU
                    themoi.MaDT = item.MaDT == null ? "" : (_data.DanTocs.Where(p => p.MaDT == item.MaDT).FirstOrDefault().TenDT == null ? " " : _data.DanTocs.Where(p => p.MaDT == item.MaDT).FirstOrDefault().TenDT); //DAN_TOC
                    themoi.NguoiDoDe = item.NguoiDoDe; //NGUOI_DO_DE
                    themoi.NguoiGhiPhieu = item.NguoiGhiPhieu; //NGUOI_GHI_PHIEU
                    themoi.NgayChungTu = DungChung.Ham.NgaySangChu(DateTime.Now, 12); //NGAY_CT
                    themoi.NGuoiDaiDien = DungChung.Bien.GiamDoc; //NGUOI_DAI_DIEN
                    themoi.SinhCon_Duoi32Tuan = item.SinhCon_Duoi32Tuan; //SINHCON_DUOI32TUAN
                    themoi.SinhCon_PhauThuat = item.SinhCon_PhauThuat; //SINHCON_PHAUTHUAT
                    themoi.So = item.So; //SO
                    themoi.Quyen = item.Quyen; //QUYEN_SO
                    listrep.Add(themoi);
                }
                DungChung.Ham.Print(DungChung.PrintConfig.Rep_DS_GiayChungSinh_ID551, listrep.ToList(), new Dictionary<string, object>(), false);
                if (chkXuatexcel.Checked == true)
                {
                    string[] _arr = new string[] { "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@" };
                    int[] _arrWidth = new int[] { };
                    string[] _tieude = { "STT", "MA_CT", "MA_CSKCB", "MA_THE", "SO_SERI", "MA_SOBHXH_ME", "HO_TEN_ME", "NGAY_SINH", "DIA_CHI", "CMND", "NGAY_CAP_CMND", "NOI_CAP_CMND", "DAN_TOC", "HO_TEN_CHA", "NGAY_SINHCON", "NOI_SINH_CON", "TEN_CON", "SO_CON", "GIOI_TINH_CON", "CAN_NANG_CON", "TINH_TRANG_CON", "GHI_CHU", "NGUOI_DO_DE", "NGUOI_GHI_PHIEU", "NGUOI_DAI_DIEN", "NGAY_CT", "SINHCON_PHAUTHUAT", "SINHCON_DUOI32TUAN", "SO", "QUYEN_SO" };
                    DungChung.Bien.MangHaiChieu = new Object[ListBn.ToList().Count + 3, 30];
                    for (int i = 0; i < _tieude.Length; i++)
                    {
                        DungChung.Bien.MangHaiChieu[0, i] = _tieude[i];
                    }

                    int num = 1;
                    foreach (var r in ListBn.ToList())
                    {
                        DungChung.Bien.MangHaiChieu[num, 0] = num;
                        DungChung.Bien.MangHaiChieu[num, 1] = "";
                        DungChung.Bien.MangHaiChieu[num, 2] = r.MCS;
                        DungChung.Bien.MangHaiChieu[num, 3] = r.SThe;
                        DungChung.Bien.MangHaiChieu[num, 4] = "";
                        DungChung.Bien.MangHaiChieu[num, 5] = r.SThe == "" ? "" : r.SThe.Substring(5, 10);
                        DungChung.Bien.MangHaiChieu[num, 6] = r.TenBNhan;
                        DungChung.Bien.MangHaiChieu[num, 7] = r.NamSinh;
                        DungChung.Bien.MangHaiChieu[num, 8] = r.DChi;
                        DungChung.Bien.MangHaiChieu[num, 9] = r.SoKSinh;
                        DungChung.Bien.MangHaiChieu[num, 10] = (r.NgayCap == null ? "" : DungChung.Ham.NgaySangChu(r.NgayCap.Value, 12));
                        DungChung.Bien.MangHaiChieu[num, 11] = r.NoiCapCMT;
                        DungChung.Bien.MangHaiChieu[num, 12] = r.MaDT;
                        DungChung.Bien.MangHaiChieu[num, 13] = r.TenBo;
                        DungChung.Bien.MangHaiChieu[num, 14] = DungChung.Ham.NgaySangChu(Convert.ToDateTime(r.ThoiGianSinh), 11);
                        DungChung.Bien.MangHaiChieu[num, 15] = r.DiaDiemSinh;
                        DungChung.Bien.MangHaiChieu[num, 16] = r.TenCon;
                        DungChung.Bien.MangHaiChieu[num, 17] = r.SoCon;
                        DungChung.Bien.MangHaiChieu[num, 18] = r.GioiTinhCon == 0 ? 2 : 1;
                        DungChung.Bien.MangHaiChieu[num, 19] = r.CanNangCon;
                        DungChung.Bien.MangHaiChieu[num, 20] = r.GhiChu;
                        DungChung.Bien.MangHaiChieu[num, 21] = "";
                        DungChung.Bien.MangHaiChieu[num, 22] = r.NguoiDoDe;
                        DungChung.Bien.MangHaiChieu[num, 23] = r.NguoiGhiPhieu;
                        DungChung.Bien.MangHaiChieu[num, 24] = DungChung.Bien.GiamDoc;
                        DungChung.Bien.MangHaiChieu[num, 25] = DungChung.Ham.NgaySangChu(DateTime.Now, 12);
                        DungChung.Bien.MangHaiChieu[num, 26] = r.SinhCon_PhauThuat;
                        DungChung.Bien.MangHaiChieu[num, 27] = r.SinhCon_Duoi32Tuan;
                        DungChung.Bien.MangHaiChieu[num, 28] = r.So;
                        DungChung.Bien.MangHaiChieu[num, 29] = r.Quyen;
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
                        filepatd = "C:\\DS_CapGiayChungSinh" + DateTime.Now + ".xls";
                    }

                    DungChung.Ham.xuatExcel(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "Sheet1", filepatd, true);
                    XtraMessageBox.Show("Xuất File thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                XtraMessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void groupControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void linkLabelSelectAll_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            checkedListBoxControlDoiTuong.CheckAll();
        }

        private void linkLabelUnselectAll_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            checkedListBoxControlDoiTuong.UnCheckAll();
        }


    }
}