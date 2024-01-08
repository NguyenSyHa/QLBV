using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using System.IO;
using QLBV.FormThamSo;

namespace QLBV.FormNhap
{
    public partial class US_TaiSan : DevExpress.XtraEditors.XtraUserControl
    {
        public US_TaiSan()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities _Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void DSKP()
        {
            var q1 = (from kp in _Data.KPhongs.Where(p => p.Status == 1)
                      select new { kp.MaKP, kp.TenKP, kp.PLoai, kp.DChi }).ToList();
            if (q1.Count > 0)
            {
                grcKhoaPhong.DataSource = "";
                grcKhoaPhong.DataSource = q1.ToList();
            }
            LupTenDV.DataSource = (from a in _Data.DichVus.Where(p => p.PLoai == 4) select new { a.MaDV, a.TenDV }).ToList();
        }

        private void US_TaiSan_Load(object sender, EventArgs e)
        {
            DSKP();
        }

        private void grvKhoaPhong_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            LoadDS();
        }
        private void LoadDS()
        {
            int _makp = 0;
            if (grvKhoaPhong.GetFocusedRowCellValue("MaKP") != null)
                _makp = (int)grvKhoaPhong.GetFocusedRowCellValue("MaKP");


            if (_makp != 0)
            {
                var ts = (from a in _Data.DichVus
                          join b in _Data.TaiSans.Where(p => p.MaKP == _makp) on a.MaDV equals b.MaDV
                          select new { b.IDTS, a.MaDV, a.TenDV, b.SoLuong, b.TinhTrang, b.GhiChu }).ToList();
                grcChiTietTS.DataSource = ts.ToList();
            }
            else
            {
                grcChiTietTS.DataSource = null;
            }
        }
        private void grvChiTietTS_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {

        }

        private void btnMoi_Click(object sender, EventArgs e)
        {
            frm_DMTSCT frm = new frm_DMTSCT(1);
            frm.ShowDialog();
            LoadDS();
        }

        private void grvChiTietTS_DataSourceChanged(object sender, EventArgs e)
        {
            grvChiTietTS_FocusedRowChanged(null, null);
        }

        private void grvChiTietTS_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {

        }

        private void grvKhoaPhong_DataSourceChanged(object sender, EventArgs e)
        {
            grvKhoaPhong_FocusedRowChanged(null, null);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {

        }

        private void btnXoaTS_Click(object sender, EventArgs e)
        {
            // frm_DMTSCT frm = null;
            //if (btnXoaTS.Buttons[]. == 0)
            //{
            //    frm = new frm_DMTSCT(1);
            //}
            //else
            //{
            //    frm = new frm_DMTSCT(2);

            //}
            //frm.ShowDialog();
        }

        private void btnXoaTS_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            int MaTS = (int)grvChiTietTS.GetFocusedRowCellValue(colIDTS);
            int madv = (int)grvChiTietTS.GetFocusedRowCellValue(colid);
            if (e.Button.Index == 0)
            {
                frm_DMTSCT frm = new frm_DMTSCT(2, MaTS, madv);
                frm.ShowDialog();
                LoadDS();
            }
            else
            {

                DialogResult r = XtraMessageBox.Show("Bạn muốn xóa tài sản " + MaTS + "", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (r == DialogResult.OK)
                {
                    if (MaTS != null)
                    {
                        TaiSan ts = new TaiSan();
                        var rs = _Data.TaiSans.Where(p => p.IDTS == MaTS).Single();
                        _Data.TaiSans.Remove(rs);
                        _Data.SaveChanges();
                        LoadDS();

                    }
                }

            }

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            FormNhap.Frm_SuDungTS frm = new FormNhap.Frm_SuDungTS();
            frm.ShowDialog();
        }
    }
}
