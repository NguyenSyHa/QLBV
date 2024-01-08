using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraWaitForm;
using DevExpress.XtraSplashScreen;

namespace QLBV.FormThamSo
{
    public partial class frm_DuyetPhieuThu_12345 : DevExpress.XtraEditors.XtraForm
    {
        public frm_DuyetPhieuThu_12345()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void frm_DuyetPhieuThu_12345_Load(object sender, EventArgs e)
        {
            dtpTuNgay.DateTime = DateTime.Now;
            dtpDenNgay.DateTime = DateTime.Now;
            cboTrangThai.SelectedIndex = 2;
            GetValueTU();

        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            DungChung.Ham.CallProcessWaitingForm(GetValueTU, "Đang tìm kiếm...", "Đang thực hiện! xin chờ trong giây lát.");
        }
        private void GetValueTU()
        {

            DateTime tungay = DungChung.Ham.NgayTu(dtpTuNgay.DateTime);
            DateTime denngay = DungChung.Ham.NgayDen(dtpDenNgay.DateTime);

            var bntu = (from bn in data.BenhNhans
                        join tu in data.TamUngs.Where(p => p.NgayThu >= tungay && p.NgayThu <= denngay && p.PhanLoai == 3) on bn.MaBNhan equals tu.MaBNhan
                        group new { bn } by new { bn.MaBNhan, bn.TenBNhan, bn.Tuoi, bn.DChi } into kq
                        select new { MaBNhan = kq.Key.MaBNhan, TenBNhan = kq.Key.TenBNhan, Tuoi = kq.Key.Tuoi, DChi = kq.Key.DChi }).ToList();
            grcBenhNhan.DataSource = bntu.ToList();
        }

        private class Tamung
        {
            public int IDTamUng { get; set; }
            public DateTime? NgayThu { get; set; }
            public string TenCBT { get; set; }
            public double? SoTien { get; set; }
            public double TienChenh { get; set; }
            public string LyDo { get; set; }
            public bool DuyetPhieu { get; set; }
            public string NguoiDuyet { get; set; }

        }
        private void grvBenhNhan_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            LoadDS();
            LoadPhieu();
        }
        private void LoadDS()
        {
            List<Tamung> tamung = new List<Tamung>();
            int MaBn = Convert.ToInt32(grvBenhNhan.GetFocusedRowCellValue(colMaBenhNhan));
            int tt = cboTrangThai.SelectedIndex;
            tamung = (from tu1 in data.TamUngs.Where(p => p.MaBNhan == MaBn && p.PhanLoai == 3)
                      join cb in data.CanBoes on tu1.MaCB equals cb.MaCB
                      select new Tamung
                      {
                          IDTamUng = tu1.IDTamUng,
                          NgayThu = tu1.NgayThu,
                          TenCBT = cb.TenCB,
                          SoTien = tu1.SoTien,
                          TienChenh = tu1.TienChenh,
                          LyDo = tu1.LyDo,
                          DuyetPhieu = tu1.DuyetPhieuThu ?? false,
                          NguoiDuyet = tu1.NguoiKiemDuyet ?? "",
                      }).ToList();
            grcPhieu.DataSource = tt == 2 ? tamung.ToList() : tamung.Where(p => p.DuyetPhieu == (tt == 1)).ToList();
        }
        private void DuyetPhieu(int IDTamung, bool status, string TenDN)
        {
            TamUng t = data.TamUngs.Where(p => p.IDTamUng == IDTamung).Single();
            t.DuyetPhieuThu = status;
            t.NguoiKiemDuyet = TenDN;
            data.SaveChanges();
        }
        private void grvPhieu_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {

        }

        private void grvPhieu_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column == colTTduyet)
            {
                var idtamung = Convert.ToInt32(grvPhieu.GetFocusedRowCellValue(colSoPhieu));
                var check = (bool)grvPhieu.GetFocusedRowCellValue(colTTduyet);
                if (!check)
                {
                    DialogResult r = XtraMessageBox.Show("Duyệt phiếu: " + idtamung + " ?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (r == DialogResult.OK)
                    {
                        DuyetPhieu(idtamung, true, DungChung.Bien.TenDN);
                    }
                }
                else
                {
                    if (data.ADMINs.Where(p => p.TenDN == DungChung.Bien.TenDN).First().CapDo == 9)
                    {
                        DialogResult r = XtraMessageBox.Show("Bỏ duyệt phiếu: " + idtamung + " ?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                        if (r == DialogResult.OK)
                        {
                            DuyetPhieu(idtamung, false, DungChung.Bien.TenDN);
                        }
                    }
                    else
                    {
                        XtraMessageBox.Show("Bạn chưa đủ quyền để hủy phiếu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        LoadDS();
                        return;
                    }

                }

            }
            LoadDS();
        }

        private void grvPhieu_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            LoadPhieu();
        }
        private void LoadPhieu()
        {
            int IDPhieu = Convert.ToInt32(grvPhieu.GetFocusedRowCellValue(colSoPhieu));
            var tamungct = (from tuct in data.TamUngcts.Where(p => p.IDTamUng == IDPhieu && p.Status != 1)
                            join dv in data.DichVus on tuct.MaDV equals dv.MaDV
                            select new
                            {
                                dv.TenDV,
                                dv.DonVi,
                                TrongBH = tuct.TrongBH == 0 ? "Ngoài  DM" : tuct.TrongBH == 1 ? "Trong DM" : tuct.TrongBH == 2 ? "Không thanh toán" : "Phụ thu",
                                tuct.DonGia,
                                tuct.SoLuong,
                                tuct.ThanhTien
                            }).ToList();
            grcDichVu.DataSource = tamungct.ToList();
        }

        private void cboTrangThai_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDS();
            LoadPhieu();
        }
    }
}