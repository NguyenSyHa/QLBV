using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
namespace QLBV.TraCuu
{
    public partial class us_BNhuydon : DevExpress.XtraEditors.XtraUserControl
    {
        public us_BNhuydon()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities _dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        public bool _Xoact = false;
        List<DThuocct> _lDThuocct = new List<DThuocct>();
        DateTime _ngayke = System.DateTime.Now;
        DateTime _dttu = System.DateTime.Now;
        DateTime _dtden = System.DateTime.Now;
        List<BenhNhan> _lTKbn = new List<BenhNhan>();
        int _makp = DungChung.Bien.MaKP;
        #region Tìm kiếm
        void TimKiem()
        {
            int _status = cboStatus.SelectedIndex;
            _dttu = DungChung.Ham.NgayTu(dtTimTuNgay.DateTime);
            _dtden = DungChung.Ham.NgayDen(dtTimDenNgay.DateTime);
            string _TimBNhan = "";
            if (lupTimMaKP.EditValue != null && lupTimMaKP.EditValue.ToString() != "")
            {
                _makp = Convert.ToInt32(lupTimMaKP.EditValue.ToString());
            }
            else
                _makp = 0;

            if (!string.IsNullOrEmpty(txtTimKiem.Text) && txtTimKiem.Text != "Tìm tên|Mã BN")
                _TimBNhan = txtTimKiem.Text.Trim();
            int _timma = 0,outTim=0;
            if (int.TryParse(_TimBNhan, out outTim))
                _timma=Convert.ToInt32(_TimBNhan);
            //grcBenhNhankd.DataSource = _lTKbn.ToList();
           
                var bnkd = (from bn in _dataContext.BenhNhans.Where(p => p.NoiTru == 0)
                            join kd in _dataContext.DThuocs.Where(p => p.PLDV == 1) on bn.MaBNhan equals kd.MaBNhan
                            join dtct in _dataContext.DThuoccts.Where(p => p.Status == _status) on kd.IDDon equals dtct.IDDon
                            //where (from tt in _dataContext.VienPhis select tt.MaBNhan).Contains(kd.MaBNhan)
                            //where !(from xd in _dataContext.NhapDs select xd.MaBNhan).Contains(kd.MaBNhan)
                            where (kd.NgayKe >= _dttu && kd.NgayKe <= _dtden)
                            where (bn.MaBNhan == (_timma) || bn.TenBNhan.Contains(_TimBNhan))
                            where _makp == 0 || kd.MaKXuat == _makp
                            select new { kd.MaBNhan, bn.TenBNhan, kd.IDDon, kd.MaKXuat, kd.NgayKe }).OrderBy(p =>p.MaBNhan).ToList();
                grcBenhNhankd.DataSource = bnkd.ToList();
            
        }
        #endregion
        private void lupTimMaKP_EditValueChanged(object sender, EventArgs e)
        {
            TimKiem();
        }

        private void us_BNhuydon_Load(object sender, EventArgs e)
        {
            var tendv = (from dv in _dataContext.DichVus select new { dv.TenDV, dv.MaDV }).ToList();
            lupMaDV.DataSource = tendv.ToList();
            var kp = _dataContext.KPhongs.Where(p => p.PLoai.Contains("Khoa dược")).ToList();
            lupTimMaKP.Properties.DataSource = kp.ToList();
            lupTimMaKP.EditValue = DungChung.Bien.MaKP;
            dtTimDenNgay.DateTime = System.DateTime.Now;
            dtTimTuNgay.DateTime = System.DateTime.Now;
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

        private void grvBenhNhankd_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            int _idkd = 0;
            if (grvBenhNhankd.GetFocusedRowCellValue(colIDDon) != null && grvBenhNhankd.GetFocusedRowCellValue(colIDDon).ToString() != "")
            {
                _idkd = int.Parse(grvBenhNhankd.GetFocusedRowCellValue(colIDDon).ToString());

               
            }
            txtIDDon.Text = _idkd.ToString();
            if (grvBenhNhankd.GetFocusedRowCellValue(colMaBNhan) != null && grvBenhNhankd.GetFocusedRowCellValue(colMaBNhan).ToString() != "")
            {
                txtMaBNhan.Text = grvBenhNhankd.GetFocusedRowCellValue(colMaBNhan).ToString();
                txtTenBnhan.Text = grvBenhNhankd.GetFocusedRowCellValue(colTenBNhan).ToString();
            }
            else
                txtMaBNhan.Text = "";
            _lDThuocct = _dataContext.DThuoccts.Where(p => p.IDDon == _idkd).OrderBy(p => p.IDDonct).ToList();
            grcDonThuocct.DataSource = _lDThuocct.ToList();
        }

        private void btnXuatDuoc_Click(object sender, EventArgs e)
        {

            DialogResult _result;
            string _tenbn = "";
            if (!string.IsNullOrEmpty(txtMaBNhan.Text))
            {
                if (grvBenhNhankd.GetFocusedRowCellDisplayText(colTenBNhan) != null && grvBenhNhankd.GetFocusedRowCellDisplayText(colTenBNhan).ToString() != "")
                    _tenbn = grvBenhNhankd.GetFocusedRowCellDisplayText(colTenBNhan).ToString();
                _result = MessageBox.Show("Xuất dược cho bệnh nhân: " + _tenbn + " ?", "Xuất dược", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (_result == DialogResult.Yes)
                {
                    NhapD _xuat = new NhapD();
                    _xuat.MaBNhan = Convert.ToInt32( txtMaBNhan.Text);
                    _xuat.SoCT = txtIDDon.Text;
                    _xuat.PLoai = 2;
                    _xuat.KieuDon = 0;
                    _xuat.NgayNhap = _ngayke;
                    if (lupTimMaKP.EditValue != null)
                        _xuat.MaKP = Convert.ToInt32( lupTimMaKP.EditValue);
                    _dataContext.NhapDs.Add(_xuat);
                    if (_dataContext.SaveChanges() >= 0)
                    {
                        int iddt = Convert.ToInt32(txtIDDon.Text);
                        //var statusKD = _dataContext.DThuocs.Single(p => p.IDDon == iddt);
                        //statusKD.Status = 1;
                        //_dataContext.SaveChanges();
                        var maxid = _dataContext.NhapDs.Max(p => p.IDNhap);
                        int id = maxid;
                        foreach (var i in _lDThuocct)
                        {
                            NhapDct _xuatct = new NhapDct();
                            _xuatct.IDNhap = id;
                            _xuatct.MaDV = i.MaDV;
                            _xuatct.DonVi = i.DonVi;
                            _xuatct.DonGia = i.DonGia;
                            _xuatct.MaCC = i.MaCC;
                            _xuatct.SoLuongX = i.SoLuong;
                            _xuatct.ThanhTienX = i.ThanhTien;
                            _xuatct.SoLuongN = 0;
                            _xuatct.ThanhTienN = 0;
                            _xuatct.SoLuongSD = 0;
                            _xuatct.ThanhTienSD = 0;
                            _xuatct.SoLuongKK = 0;
                            _xuatct.ThanhTienKK = 0;
                            _xuatct.SoLo = i.SoLo;
                            _dataContext.NhapDcts.Add(_xuatct);
                            _dataContext.SaveChanges();
                        }
                    }
                    TimKiem();
                }

            }
        }

        private void cboStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            TimKiem();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {

        }
    }
}
