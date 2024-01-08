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
    public partial class frm_DMTSCT : DevExpress.XtraEditors.XtraForm
    {
        public frm_DMTSCT()
        {
            InitializeComponent();
        }
        private int value; // 1 Thêm mới hoặc  2 sửa 
        private int IDTS; //ID tài sản
        private int _madv;
        public frm_DMTSCT(int Value, int idts = 0, int Madv = 0)
        {
            InitializeComponent();
            value = Value;
            IDTS = idts;
            _madv = Madv;
        }
        QLBV_Database.QLBVEntities _Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void DSKP()
        {
            txtIDTS.Text = IDTS.ToString();
            var q1 = (from kp in _Data.KPhongs.Where(p => p.Status == 1)
                      select new { kp.MaKP, kp.TenKP }).ToList();
            if (q1.Count > 0)
            {
                LupKhoaPhong.Properties.DataSource = "";
                LupKhoaPhong.Properties.DataSource = q1.ToList();
            }
            txtTenTaiSan.Properties.DataSource = (from a in _Data.DichVus.Where(p => p.PLoai == 4) select new { a.MaDV, a.TenDV }).ToList();
            txtTenTaiSan.EditValue = _madv;
            if (IDTS != 0)
            {
                var ts = _Data.TaiSans.Where(p => p.IDTS == IDTS).ToList();
                txtSoLuong.EditValue = ts.First().SoLuong;
                txtTinhTrang.EditValue = ts.First().TinhTrang;
                mmGhiChu.Text = ts.First().GhiChu;
                LupKhoaPhong.EditValue = ts.First().MaKP;

            }

        }
        private void frm_DMTSCT_Load(object sender, EventArgs e)
        {
            DSKP();
        }
        private bool CheckValueControl()
        {
            if (string.IsNullOrEmpty(txtTenTaiSan.Text))
            {
                XtraMessageBox.Show("Bạn chưa chọn tài sản", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtTenTaiSan.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(LupKhoaPhong.Text))
            {
                XtraMessageBox.Show("Khoa phòng không hợp lệ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LupKhoaPhong.Focus();
                return false;
            }
            else
            {
                return true;
            }

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (CheckValueControl())
            {
                if (value == 1)
                {
                    TaiSan ts = new TaiSan();
                    ts.MaDV = (int)txtTenTaiSan.EditValue;
                    ts.SoLuong = Convert.ToInt32(txtSoLuong.EditValue);
                    ts.MaKP = (int)LupKhoaPhong.EditValue;
                    ts.TinhTrang = txtTinhTrang.Text;
                    ts.GhiChu = mmGhiChu.Text;
                    _Data.TaiSans.Add(ts);
                }
                else
                {

                    TaiSan ts = _Data.TaiSans.Where(p => p.IDTS == IDTS).Single();
                    ts.MaDV = (int)txtTenTaiSan.EditValue;
                    ts.SoLuong = Convert.ToInt32(txtSoLuong.EditValue);
                    ts.MaKP = (int)LupKhoaPhong.EditValue;
                    ts.TinhTrang = txtTinhTrang.EditValue.ToString();
                    ts.GhiChu = mmGhiChu.Text;

                }
                if (_Data.SaveChanges() >= 0)
                {
                    XtraMessageBox.Show("Thao tác thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

        }
    }
}