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
    public partial class frm_BC_XNNuoiCayViKhuanLao : DevExpress.XtraEditors.XtraForm
    {
        public frm_BC_XNNuoiCayViKhuanLao()
        {
            InitializeComponent();
        }

        private QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

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

        private void frm_BC_XNNuoiCayViKhuanLao_Load(object sender, EventArgs e)
        {
            dateTuNgay.DateTime = DateTime.Now;
            dateDenNgay.DateTime = DateTime.Now;
            var kphong = (from kp in data.KPhongs.Where(p => p.PLoai == "Lâm sàng" || p.PLoai == "Phòng khám")
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

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool KTtaoBc()
        {

            if (dateTuNgay.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn ngày bắt đầu in báo cáo");
                dateTuNgay.Focus();
                return false;
            }
            if (dateDenNgay.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn ngày kết thúc in báo cáo");
                dateDenNgay.Focus();
                return false;
            }
            if (DungChung.Ham.NgayDen(dateDenNgay.DateTime) < DungChung.Ham.NgayTu(dateTuNgay.DateTime))
            {
                MessageBox.Show("Ngày đến không thể nhỏ hơn ngày từ.");
                dateDenNgay.Focus();
                return false;
            }
            else return true;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (KTtaoBc())
            {
                List<KPhong> _lKhoaP = new List<KPhong>();
                _lKhoaP = _Kphong.Where(p => p.makp > 0).Where(p => p.chon == true).ToList();
                List<Content> _lContent = new List<Content>();
                Content moi = new Content();
                DateTime tungay = DungChung.Ham.NgayTu(dateTuNgay.DateTime);
                DateTime denngay = DungChung.Ham.NgayTu(dateDenNgay.DateTime);

                List<int> _ckbLoaiXN = new List<int>();
                for (int i = 0; i < checkedListBoxControl1.Items.Count; i++)
                {
                    if (checkedListBoxControl1.GetItemChecked(i))
                    {
                        _ckbLoaiXN.Add(int.Parse(checkedListBoxControl1.Items[i].Value.ToString()));
                    }
                }

                List<int> _ckbDTuongLao = new List<int>();
                for (int i = 0; i < ckbDTLao.Items.Count; i++)
                {
                    if (ckbDTLao.GetItemChecked(i))
                    {
                        _ckbDTuongLao.Add(int.Parse(ckbDTLao.Items[i].Value.ToString()));
                    }
                }

                var qCLS = (from cls in data.CLS
                            join cd in data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                            join clsct in data.CLScts on cd.IDCD equals clsct.IDCD
                            join dv in data.DichVus on cd.MaDV equals dv.MaDV
                            join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                            select new
                            {
                                cls.IdCLS,
                                cls.MaBNhan,
                                cls.MaKP,
                                cls.MaKPth,
                                cls.NgayTH,
                                cls.STT,
                                cls.BenhPham,
                                cls.TrangThaiBP,
                                cls.ThoiGianLayMau,
                                cls.ThoiGianNhanMau,
                                clsct.Id,
                                clsct.KetQua,
                                tn.TenRG
                            }).Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNDom).ToList();

                var qBN = (from bn in data.BenhNhans
                           join ttbx in data.TTboXungs on bn.MaBNhan equals ttbx.MaBNhan
                           join xn in _ckbLoaiXN on ttbx.ChanDoanLao equals xn
                           join dt in _ckbDTuongLao on ttbx.DTuongLao equals dt
                           select new
                           {
                               bn.MaBNhan,
                               bn.TenBNhan,
                               bn.Tuoi,
                               bn.GTinh,
                               bn.DChi,
                               ttbx.ThangTheoDoi,
                               ttbx.ChanDoanLao,
                               ttbx.DTuongLao
                           }).ToList();

                var query = (from a in qCLS
                             join b in qBN on a.MaBNhan equals b.MaBNhan
                             join kp in _lKhoaP on a.MaKP equals kp.makp
                             select new
                             {
                                 a.IdCLS,
                                 a.MaBNhan,
                                 a.MaKP,
                                 a.MaKPth,
                                 a.NgayTH,
                                 a.STT,
                                 a.BenhPham,
                                 a.TrangThaiBP,
                                 a.ThoiGianLayMau,
                                 a.ThoiGianNhanMau,
                                 a.Id,
                                 a.KetQua,
                                 b.TenBNhan,
                                 b.Tuoi,
                                 b.GTinh,
                                 b.DChi,
                                 b.ThangTheoDoi,
                                 b.ChanDoanLao,
                                 b.DTuongLao,
                                 TenKP = kp.tenkp
                             }).ToList();

                foreach (var item in query)
                {
                    moi = new Content();
                    moi.SoXN = item.Id;
                    moi.NgayLayMau = String.Format("{0:dd/MM/yyyy}", item.ThoiGianLayMau);
                    moi.NgayNhanMau = String.Format("{0:dd/MM/yyyy}", item.ThoiGianNhanMau);
                    moi.NgayCay = String.Format("{0:dd/MM/yyyy}", item.NgayTH);
                    moi.HoTenBN = item.TenBNhan;
                    moi.TuoiNam = (item.GTinh == 1) ? item.Tuoi : null;
                    moi.TuoiNu = (item.GTinh == 0) ? item.Tuoi : null;
                    moi.DiaChi = item.DChi;
                    moi.DViYeuCau = item.TenKP;
                    moi.BPDom = (item.BenhPham != null) ? ((item.BenhPham.Contains("NB") || item.BenhPham.Contains("NM") || item.BenhPham.Contains("M")) ? item.BenhPham : "") : null;
                    moi.BPKhac = (item.BenhPham == null) ? null : ((!item.BenhPham.Contains("NB") && !item.BenhPham.Contains("NM") && !item.BenhPham.Contains("M")) ? item.BenhPham : "");
                    moi.TrangThaiBP = item.TrangThaiBP;
                    moi.LyDoPhatHien = (item.ThangTheoDoi == 0) ? "x" : "";
                    moi.TheoDoiThangThu = (item.ThangTheoDoi > 0) ? item.ThangTheoDoi.ToString() : "";
                    moi.KQSoi = item.KetQua;
                    _lContent.Add(moi);
                }
                BaoCao.rep_BC_XNNuoiCayViKhuanLao rep = new BaoCao.rep_BC_XNNuoiCayViKhuanLao();
                frmIn frm = new frmIn();
                rep.lblThoiGian.Text = "(Từ ngày " + dateTuNgay.DateTime.ToString("dd/MM/yyyy") + " đến ngày " + dateDenNgay.DateTime.ToString("dd/MM/yyyy") + ")";
                rep.DataSource = _lContent;
                rep.BindingData();
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
            }
        }
        public class Content
        {
            public int SoXN { get; set; }
            public string NgayLayMau { get; set; }
            public string NgayNhanMau { get; set; }
            public string NgayCay { get; set; }
            public string HoTenBN { get; set; }
            public int? TuoiNam { get; set; }
            public int? TuoiNu { get; set; }
            public string DiaChi { get; set; }
            public string DViYeuCau { get; set; }
            public string BPDom { get; set; }
            public string BPKhac { get; set; }
            public string TrangThaiBP { get; set; }
            public string LyDoPhatHien { get; set; }
            public string TheoDoiThangThu { get; set; }
            public string KQSoi { get; set; }
            public string KQ_LJ1 { get; set; }
            public string KQ_LJ2 { get; set; }
            public string KQ_MGIT { get; set; }
            public string KQ_NgayBaoDuong { get; set; }
            public string Niacin { get; set; }
            public string SoiCF { get; set; }
            public string TbcIDSD { get; set; }
            public string KetLuan { get; set; }
            public string NgayChuyenChungKSD { get; set; }
            public string NgayTraKQ { get; set; }
            public string XNVien { get; set; }
            public string GhiChu { get; set; }
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