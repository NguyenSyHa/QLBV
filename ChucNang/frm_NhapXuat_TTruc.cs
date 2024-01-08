using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;

namespace QLBV.FormNhap
{
    public partial class frm_NhapXuat_TTruc : DevExpress.XtraEditors.XtraForm
    {
        public frm_NhapXuat_TTruc()
        {
            InitializeComponent();
        }
        List<NhapDct> _lnhapct = new List<NhapDct>();
        QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        DateTime _dttu = System.DateTime.Now;
        DateTime _dtden = System.DateTime.Now;
        private void grvNhap_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (grvNhap.GetFocusedRowCellValue(colIDNhaps) != null && Convert.ToInt32(grvNhap.GetFocusedRowCellValue(colIDNhaps)) > 0)
            {
                txtID.Text = grvNhap.GetFocusedRowCellValue(colIDNhaps).ToString();
                int id = Convert.ToInt32(txtID.Text);
                if (grvNhap.GetFocusedRowCellValue(colMaKPNX) != null && grvNhap.GetFocusedRowCellValue(colMaKPNX).ToString() != "")
                    lupKhoNhap.EditValue = Convert.ToInt32(grvNhap.GetFocusedRowCellValue(colMaKPNX));
                _lnhapct = _data.NhapDcts.Where(p => p.IDNhap == id).ToList();
                grcNhapCT.DataSource = _lnhapct;
            }
            else
            {
                txtID.Text = "";
                _lnhapct = _data.NhapDcts.Where(p => p.IDNhap == 0).ToList();
                grcNhapCT.DataSource = _lnhapct;
            }
        }
        List<NhapD> _nhap = new List<NhapD>();
        private void frm_NhanXuat_Load(object sender, EventArgs e)
        {
            //if (DungChung.Bien.CapDo == 9 || DungChung.Bien.CapDo == 8)
            if (DungChung.Bien.PLoaiKP == DungChung.Bien.st_PhanLoaiKP.Admin)
            {
                lupKhoNhap.Properties.ReadOnly = false;
                lupTimNoiNhan.Properties.ReadOnly = false;
            }
            //lupKhoNhap.EditValue = DungChung.Bien.MaKP;
            lupTimNoiNhan.EditValue = DungChung.Bien.MaKP;
            dtTimTuNgay.DateTime = System.DateTime.Now;
            dtTimDenNgay.DateTime = System.DateTime.Now;
            var khp = _data.KPhongs.OrderBy(p => p.PLoai).ToList();
            lupKhoNhap.Properties.DataSource = khp.Where(p => p.PLoai == ("Khoa dược")).ToList();
            var noinhan = (from noin in khp where (noin.PLoai == ("Khoa dược") && noin.TuTruc==true) select noin).ToList();
            lupTimNoiNhan.Properties.DataSource = noinhan.ToList();
            var _tenduoc = _data.DichVus.Where(p => p.PLoai == 1).ToList();
            lupMaDuoc.DataSource = _tenduoc;
            _nhap = _data.NhapDs.Where(p => p.PLoai == 2).ToList();
            grcNhap.DataSource = _nhap.ToList();
            TimKiem();
        }
        private void TimKiem()
        {
            int _makx = 0;
            int _manNhan = 0;
            int _sophieu = 0;
            _dttu = DungChung.Ham.NgayTu(dtTimTuNgay.DateTime);
            _dtden = DungChung.Ham.NgayDen(dtTimDenNgay.DateTime);
            if (lupTimNoiNhan.EditValue != null && lupTimNoiNhan.EditValue.ToString() != "")
                _manNhan = Convert.ToInt32(lupTimNoiNhan.EditValue);
            if (!string.IsNullOrEmpty(txtTimKiem.Text) && txtTimKiem.Text != "Số phiếu|Số CT")
                _sophieu = Convert.ToInt32(txtTimKiem.Text);
                _nhap = (from nd in _data.NhapDs.Where(p => p.PLoai == 2)
                         join kp in _data.KPhongs.Where(p=>p.TuTruc) on nd.MaKP equals kp.MaKP
                         where (nd.NgayNhap >= _dttu && nd.NgayNhap <= _dtden)
                         where (nd.MaKP == (_manNhan) )
                         select nd).OrderByDescending(p => p.NgayNhap).OrderByDescending(p => p.IDNhap).ToList();
      
            grcNhap.DataSource = _nhap.Where(p=>_sophieu>0? p.IDNhap==_sophieu: true).ToList();

        }

        private void txtTimKiem_EditValueChanged(object sender, EventArgs e)
        {
            TimKiem();
        }

        private void dtTimTuNgay_EditValueChanged(object sender, EventArgs e)
        {
            TimKiem();
        }

        private void dtTimDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            TimKiem();
        }

        private void lupTimKhoX_EditValueChanged(object sender, EventArgs e)
        {
            TimKiem();
        }

        private void lupTimNoiNhan_EditValueChanged(object sender, EventArgs e)
        {
            TimKiem();
        }

        private void grvNhap_DataSourceChanged(object sender, EventArgs e)
        {
            if (grvNhap.GetFocusedRowCellValue(colIDNhaps) != null && Convert.ToInt32(grvNhap.GetFocusedRowCellValue(colIDNhaps)) > 0)
            {
                txtID.Text = grvNhap.GetFocusedRowCellValue(colIDNhaps).ToString();
                int id = Convert.ToInt32(txtID.Text);
                _lnhapct = _data.NhapDcts.Where(p => p.IDNhap == id).ToList();
                grcNhapCT.DataSource = _lnhapct;
            }
            else
            {
                txtID.Text = "";
                _lnhapct = _data.NhapDcts.Where(p => p.IDNhap == 0).ToList();
                grcNhapCT.DataSource = _lnhapct;
            }
        }
        private bool KTra()
        {
            if (dtNgayNhap.EditValue == null || dtNgayNhap.EditValue.ToString() == "")
            {
                MessageBox.Show("Bạn chưa chọn ngày nhập");
                dtNgayNhap.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(lupKhoNhap.Text))
            {
                MessageBox.Show("Bạn chưa chọn kho nhập");
                lupKhoNhap.Focus();
                return false;
            }
            return true;
        }
       
        private void btnNhanCT_Click(object sender, EventArgs e)
        {
            if (KTra())
            {
                if (_lnhapct.Count > 0)
                {
                    int _id = 0;
                    _id = _lnhapct.First().IDNhap.Value;

                    DialogResult _result = MessageBox.Show("bạn muốn xuất chứng từ số :" + txtID.Text, "Nhận CT", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (_result == DialogResult.Yes)
                    {
                        int khoxuat = lupKhoNhap.EditValue == null ? 0 : Convert.ToInt32(lupKhoNhap.EditValue);
                        int noinhan = lupTimNoiNhan.EditValue == null ? 0 : Convert.ToInt32(lupTimNoiNhan.EditValue);
                        if (FormNhap.frm_NhanNhap.nhannhapthanhxuat(_id, dtNgayNhap.DateTime, khoxuat, noinhan, txtGhiChu.Text,2)) { 
                     _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                     int idnhap = _data.NhapDs.Where(p => p.XuatTD == _id).Select(p => p.IDNhap).FirstOrDefault();

                     FormNhap.frm_NhanXuat.nhanCTXuat(idnhap, dtNgayNhap.DateTime, txtGhiChu.Text.Trim(), 0,2);
                        }
                        this.Dispose();
                    }
                

                }
            }
        }

        private void rad_TrangThai_SelectedIndexChanged(object sender, EventArgs e)
        {
            TimKiem();
            if (rad_TrangThai.SelectedIndex == 0)
                btnNhanCT.Enabled = true;
            else
                btnNhanCT.Enabled = false;
        }
    }
}