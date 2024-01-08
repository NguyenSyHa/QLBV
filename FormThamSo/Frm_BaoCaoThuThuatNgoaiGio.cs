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
    public partial class Frm_BaoCaoThuThuatNgoaiGio : DevExpress.XtraEditors.XtraForm
    {
        public Frm_BaoCaoThuThuatNgoaiGio()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void btnTaoBaoCao_Click(object sender, EventArgs e)
        {

            DungChung.Ham.CallProcessWaitingForm(TaoBaoCao, "Đang tạo báo cáo!", "Vui lòng đợi trong ít phút!.");
        }
        private class baoCao
        {
            public DateTime? oderby { get; set; }
            public string NgayThang { get; set; }
            public string HoTenBenhNhan { get; set; }
            public string Tuoi { get; set; }
            public string DiaChi { get; set; }
            public DateTime? BatDau { get; set; }
            public DateTime? KetThuc { get; set; }
            public string DichVu { get; set; }
            public string TongGio { get; set; }
            public string PhauThuatVienChinh { get; set; }
            public string PhauThuatVienPhu { get; set; }
            public string GayMeChinh { get; set; }
            public string GayMePhu1 { get; set; }
            public string GayMePhu2 { get; set; }
        }
        private void TaoBaoCao()
        {
            dtpTuNgay.DateTime = DungChung.Ham.NgayTu(dtpTuNgay.DateTime);
            dtpDenNgay.DateTime = DungChung.Ham.NgayDen(dtpDenNgay.DateTime);
            int TinhTheo = radTinhTheo.SelectedIndex;
            var bncd = (from bn in data.BenhNhans
                        join cls in data.CLS on bn.MaBNhan equals cls.MaBNhan
                        join cd in data.ChiDinhs.Where(p => p.NgoaiGioHC != 0 && p.NgayTH >= dtpTuNgay.DateTime && p.NgayTH <= dtpDenNgay.DateTime) on cls.IdCLS equals cd.IdCLS
                        join cb in data.CanBoes on cd.MaCBth equals cb.MaCB
                        select new
                        {
                            bn,
                            cls,
                            cd,
                            cb
                        }).ToList();
            var rep = (from bn in bncd
                       join dv in _listDv.Where(p => p.Chon == true) on bn.cd.MaDV equals dv.MaDV
                       select new
                       {
                           NgayThang = bn.cd.NgayTH,
                           HoTenBenhNhan = bn.bn.TenBNhan,
                           Tuoi = bn.bn.Tuoi,
                           DiaChi = bn.bn.DChi,
                           TenDichVu = dv.TenDV,
                           BatDau = bn.cd.NgayBDTH == null ? bn.cls.NgayThang : bn.cd.NgayBDTH,
                           KetThuc = bn.cd.NgayTH,
                           PhauThuatVien = bn.cb.TenCB,
                           DSCBTH = bn.cd.DSCBTH,
                           mabn = bn.bn.MaBNhan
                       }).ToList();
            if (rep.Count > 0)
            {
                List<baoCao> bc = new List<baoCao>();
                foreach (var item in rep)
                {
                    baoCao r = new baoCao();
                    r.oderby = item.NgayThang;
                    r.NgayThang = Convert.ToDateTime(item.NgayThang).ToString("dd/MM/yyyy");
                    r.HoTenBenhNhan = item.HoTenBenhNhan;
                    r.Tuoi = item.Tuoi.ToString();
                    if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049")
                    {
                        r.Tuoi = DungChung.Ham.TuoitheoThang(data, item.mabn, "12-00");
                    }
                    r.DiaChi = item.DiaChi;
                    r.PhauThuatVienChinh = item.PhauThuatVien;
                    r.DichVu = item.TenDichVu;
                    r.BatDau = item.BatDau;
                    r.KetThuc = item.KetThuc;
                    TimeSpan time = (DateTime)item.KetThuc - (DateTime)item.BatDau;
                    r.TongGio = TinhTheo == 0 ? Math.Round(time.TotalHours) + " giờ" : Math.Round(time.TotalMinutes) + " phút";
                    if (item.DSCBTH != null)
                    {
                        string[] DSCB = item.DSCBTH.Split(';');
                        r.PhauThuatVienPhu = DSCB[0].ToString();
                        r.GayMeChinh = DSCB[1].ToString();
                        r.GayMePhu1 = DSCB[2].ToString();
                        r.GayMePhu2 = DSCB[6].ToString();
                    }
                    bc.Add(r);
                }
                DungChung.Ham.Print(DungChung.PrintConfig.Rep_BCThuThuatPhauThuatNgoaiGioHanhChinh, bc.OrderBy(p=>p.oderby).ToList(), new Dictionary<string, object>(), false);
            }
            else
            {
                XtraMessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }
        private class DV
        {
            public int MaDV { get; set; }
            public string TenDV { get; set; }
            public bool Chon { get; set; }
            public int? DangSuDung { get; set; }
        }
        List<DV> _listDv = new List<DV>();
        private void Frm_BaoCaoThuThuatNgoaiGio_Load(object sender, EventArgs e)
        {
            dtpTuNgay.DateTime = dtpDenNgay.DateTime = DateTime.Now;
            _listDv = (from dv in data.DichVus
                       join ndv in data.NhomDVs.Where(p => p.TenNhom.Contains("Thủ thuật, phẫu thuật") || p.IDNhom == 8) on dv.IDNhom equals ndv.IDNhom
                       select new DV
                       {
                           MaDV = dv.MaDV,
                           TenDV = dv.TenDV,
                           Chon = true,
                           DangSuDung = dv.Status,
                       }).ToList();
            _listDv.Insert(0, new DV { MaDV = 0, TenDV = "Tất cả", Chon = true, DangSuDung = 1 });
            grcDichVu.DataSource = _listDv.OrderBy(p => p.MaDV).ToList();
        }

        private void grvDichVu_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column == grvChon)
            {
                string TenDV = grvDichVu.GetFocusedRowCellValue(grvTenDichVu).ToString();
                if (TenDV == "Tất cả")
                {
                    bool Check = !(bool)grvDichVu.GetFocusedRowCellValue(grvChon);
                    if (Check == true)
                    {
                        foreach (var item in _listDv)
                        {
                            item.Chon = true;
                        }

                    }
                    else
                    {
                        foreach (var item in _listDv)
                        {
                            item.Chon = false;
                        }
                    }
                    grcDichVu.DataSource = null;
                    grcDichVu.DataSource = _listDv.ToList();
                }


            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void chkLayDichVuDangSuDung_CheckedChanged(object sender, EventArgs e)
        {
            if (chkLayDichVuDangSuDung.Checked == true)
            {              
                foreach (var item in _listDv)
                {
                    if (item.DangSuDung == 0)
                    {
                        item.Chon = false;
                    }
                }
                grcDichVu.DataSource = _listDv.Where(p => p.DangSuDung == 1).ToList();
            }
            else
            {
                foreach (var item in _listDv)
                {
                    if (item.DangSuDung == 0)
                    {
                        item.Chon = true;
                    }
                }
                grcDichVu.DataSource = _listDv.OrderBy(p => p.MaDV).ToList();
            }
        }



    }
}