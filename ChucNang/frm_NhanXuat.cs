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
    public partial class frm_NhanXuat : DevExpress.XtraEditors.XtraForm
    {
        public frm_NhanXuat()
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
            var khp = _data.KPhongs.OrderBy(p => p.PLoai).ToList();
            lupTimKhoX.Properties.DataSource = khp.Where(p => p.PLoai == ("Khoa dược")).ToList();
            if (DungChung.Bien.PLoaiKP == DungChung.Bien.st_PhanLoaiKP.Admin)
            {
                //lupKhoNhap.Properties.DataSource = khp.Where(p => p.PLoai == ("Khoa dược")).ToList();
                var noinhan = (from noin in khp where (noin.PLoai == ("Khoa dược") || noin.PLoai == ("Lâm sàng")) select noin).ToList();
                lupTimNoiNhan.Properties.DataSource = noinhan.ToList();
              
                lupKhoNhap.Properties.DataSource = khp.Where(p => p.PLoai == ("Khoa dược")).ToList();
                //lupKhoNhap.Properties.ReadOnly = false;
                //lupTimNoiNhan.Properties.ReadOnly = false;
            }
            else
            {
                var q = (from a in khp.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.KhoaDuoc || p.PLoai == ("Lâm sàng"))
                         join b in DungChung.Bien.listKPHoatDong on a.MaKP equals b
                         select a).ToList();
                lupTimNoiNhan.Properties.DataSource = q;
                var q2 = (from a in khp.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.KhoaDuoc)
                         join b in DungChung.Bien.listKPHoatDong on a.MaKP equals b
                         select a).ToList();
                //lupTimKhoX.Properties.DataSource = q2;
                lupKhoNhap.Properties.DataSource = q2;
                lupKhoNhap.EditValue = DungChung.Bien.MaKP;
            }
           
            cboNhap.SelectedIndex = 0;
            lupHinhthucx.Properties.DataSource = DungChung.Bien.c_PhanLoaiXuat._setPhanLoaiXuat();
            lupHinhthucx.EditValue = 2;
            lupTimNoiNhan.EditValue = DungChung.Bien.MaKP;
            dtTimTuNgay.DateTime = System.DateTime.Now;
            dtTimDenNgay.DateTime = System.DateTime.Now;
            var _tenduoc = _data.DichVus.Where(p => p.PLoai == 1).ToList();
            lupMaDuoc.DataSource = _tenduoc;
           
            TimKiem();
        }
        private void TimKiem()
        {
            int _makx = 0;
            int _manNhan = 0;
            int _sophieu = 0;
            _dttu = DungChung.Ham.NgayTu(dtTimTuNgay.DateTime);
            _dtden = DungChung.Ham.NgayDen(dtTimDenNgay.DateTime);
            if (lupTimKhoX.EditValue != null && lupTimKhoX.EditValue.ToString() != "")
                _makx = Convert.ToInt32(lupTimKhoX.EditValue);
            if (lupTimNoiNhan.EditValue != null && lupTimNoiNhan.EditValue.ToString() != "")
                _manNhan = Convert.ToInt32(lupTimNoiNhan.EditValue);
            if (!string.IsNullOrEmpty(txtTimKiem.Text) && txtTimKiem.Text != "Số phiếu|Số CT")
                _sophieu = Convert.ToInt32(txtTimKiem.Text);
            if (_sophieu > 0)
            {
                _nhap = _data.NhapDs.Where(p => p.IDNhap == _sophieu).ToList();
            }
            else
            {
                _nhap = (from nd in _data.NhapDs.Where(p => p.PLoai == 2)
                         where (nd.NgayNhap >= _dttu && nd.NgayNhap <= _dtden)
                         where (nd.MaKP == (_makx))
                         where (nd.MaKPnx == (_manNhan))
                         select nd).OrderByDescending(p => p.NgayNhap).OrderByDescending(p => p.IDNhap).ToList();
            }
            grcNhap.DataSource = _nhap.ToList();

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
        /// <summary>
        /// _id: ID chứng từ
        /// </summary>
        /// <param name="_id"></param>
        /// <param name="dt"></param>
        /// <param name="ghichu"></param>
        /// <param name="ploainhap">0: nhập từ kho khác trong bệnh viện; 1: nhập trả dược</param>
        /// <param name="traduoc_kieudon">theo phân loại xuất</param>
        /// <returns></returns>
        public static bool nhanCTXuat(int _id, DateTime dt, string ghichu, int ploainhap, int traduoc_kieudon)
        {
            try
            {
                int makp = 0, makhoxuat = 0;
                QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                List<NhapD> _lnhap = _data.NhapDs.Where(p => p.IDNhap == _id).ToList();
                List<NhapDct> _lnhapct = new List<NhapDct>();

                //var IDctnhap1 = _data.NhapDs.Where(p => p.IDNhap == _id).Select(p => p.XuatTD).FirstOrDefault();
                _lnhapct = _data.NhapDcts.Where(p => p.IDNhap == _id).ToList();
                var ktxuattd = _data.NhapDs.Where(p => p.XuatTD == _id).ToList();
                if (ktxuattd.Count > 0 && _lnhap.Count > 0)
                {
                    MessageBox.Show("Chứng từ đã được sử dụng");
                    return false;
                }

                makp = _lnhap.First().MaKPnx ?? 0;
                makhoxuat = _lnhap.First().MaKP ?? 0;
                //using (System.Data.Entity.DbContextTransaction trans = _data.Database.BeginTransaction())
                //{
                //    try
                //    {
                        NhapD nd = new NhapD();
                        
                        nd.XuatTD = _id;
                        nd.NgayNhap = dt;
                        nd.SoCT = _id.ToString();
                        nd.PLoai = 1;
                        if (ploainhap == 0)// nhập từ kho khác trong bệnh viện
                            nd.KieuDon = 0;
                        else if (ploainhap == 1)// nhập trả dược
                        {
                            nd.KieuDon = 2;
                            nd.TraDuoc_KieuDon = traduoc_kieudon;
                        }
                        nd.MaCC = "";
                        nd.MaKP = makp;
                        nd.GhiChu = ghichu;
                        nd.MaCB = DungChung.Bien.MaCB;
                        //nd.MaCC = makhoxuat.ToString();
                        nd.MaKPnx = makhoxuat;
                        _data.NhapDs.Add(nd);
                        if (_data.SaveChanges() >= 0)
                        {

                            //Luu bang NhapDct
                            // lấy ID max trong bang NhapD
                            int idnhap = 0;
                            idnhap = nd.IDNhap;
                            if (idnhap > 0)
                            {

                                foreach (var a in _lnhapct)
                                {

                                    NhapDct ndct = new NhapDct();
                                    ndct.MaDV = a.MaDV;
                                    ndct.IDNhap = idnhap;
                                    ndct.DonVi = a.DonVi;
                                    ndct.DonGia = a.DonGia;
                                    ndct.DonGiaCT = a.DonGiaCT;
                                    if (!string.IsNullOrEmpty(a.MaCC))
                                        ndct.MaCC = a.MaCC;
                                    else
                                        ndct.MaCC = "";
                                    ndct.SoLo = a.SoLo;
                                    ndct.HanDung = a.HanDung;
                                    ndct.SoDangKy = a.SoDangKy;
                                    ndct.SoLuongN = a.SoLuongX;
                                    ndct.ThanhTienN = a.ThanhTienX;
                                    ndct.SoLuongX = 0;
                                    ndct.ThanhTienX = 0;
                                    ndct.SoLuongSD = 0;
                                    ndct.ThanhTienDY = a.ThanhTienDY;
                                    ndct.SoLuongDY = a.SoLuongDY;
                                    ndct.ThanhTienSD = 0;
                                    ndct.SoLuongKK = 0;
                                    ndct.ThanhTienKK = 0;
                                    _data.NhapDcts.Add(ndct);

                                }
                                _data.SaveChanges();
                            }


                        }
                //        trans.Commit();
                        return true;
                //        //
                //    }
                //    catch (Exception ex)
                //    {
                //        trans.Rollback();
                //        MessageBox.Show("Lỗi nhận xuất: " + ex.Message);
                //        return false;
                //    }
                //}

                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
                return false;
            }
        }
        private void btnNhanCT_Click(object sender, EventArgs e)
        {
            if (KTra())
            {
                if (_lnhapct.Count > 0)
                {
                    int _id = 0;
                    _id = _lnhapct.First().IDNhap.Value;
                    int traduoc_kieudon = 2;// trả dược cho xuất nội bộ
                    if (lupHinhthucx.EditValue != null)
                        traduoc_kieudon = Convert.ToInt32(lupHinhthucx.EditValue);
                    DialogResult _result = MessageBox.Show("bạn muốn nhận chứng từ số :" + txtID.Text, "Nhận CT", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (_result == DialogResult.Yes)
                    {
                        // FormNhap.frm_NhanXuat.nhanCTXuat(_id, dtNgayNhap.DateTime, txtGhiChu.Text.Trim());
                        FormNhap.frm_NhanXuat.nhanCTXuat(_id, dtNgayNhap.DateTime, txtGhiChu.Text.Trim(), cboNhap.SelectedIndex, traduoc_kieudon);
                        this.Dispose();
                    }
                }
            }
        }
    }
}