using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;

namespace QLBV.FormThamSo
{
    public partial class frm_TheoDoiSinhDe : DevExpress.XtraEditors.XtraForm
    {
        int _mabn = 0;
        int _tt = 0;//1:lưu mới, 2:sửa
        public frm_TheoDoiSinhDe()
        {
            InitializeComponent();
        }

        public frm_TheoDoiSinhDe(int mabn)
        {
            _mabn = mabn;
            InitializeComponent();
        }

        #region EnableControl
        private void EnableControl(bool b)
        {
            txtLanDe.Properties.ReadOnly = b;
            txtSoCon.Properties.ReadOnly = b;
            lupBV.Properties.ReadOnly = b;
            dateNgaySinh.Properties.ReadOnly = b;
            lupCBTH.Properties.ReadOnly = b;
            txtCanNang.Properties.ReadOnly = b;
            txtChieuCao.Properties.ReadOnly = b;
            lupTienSuNaoThai.Properties.ReadOnly = b;
            lupThaiChet.Properties.ReadOnly = b;
            lupTaiBien.Properties.ReadOnly = b;
        }
        #endregion
        #region ResetControl
        private void ResetControl()
        {
            txtSoCon.Text = "";
            lupBV.EditValue = null;
            dateNgaySinh.DateTime = DateTime.Now;
            lupCBTH.EditValue = null;
            txtCanNang.Text = "";
            txtChieuCao.Text = "";
            lupDBCuocDe.EditValue = null;
            lupTienSuNaoThai.EditValue = null;
            lupThaiChet.EditValue = null;
            lupTaiBien.EditValue = null;
        }
        #endregion
        #region class tai biến sản khoa
        private class TaiBien
        {
            public int Stt { get; set; }
            public string Ten { get; set; }
        }
        #endregion
        #region class diễn biến cuộc đẻ
        private class DienBien
        {
            public int Stt { get; set; }
            public string Ten { get; set; }
        }
        #endregion
        #region class tiền sử nạo thai
        private class TSNaoThai
        {
            public int Stt { get; set; }
            public string Ten { get; set; }
        }
        #endregion
        #region class thai chết
        private class ThaiChet
        {
            public int Stt { get; set; }
            public string Ten { get; set; }
        }
        #endregion
        #region binding gridview
        private void BindingGridview()
        {
            if (_mabn > 0)
            {
                QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                var qbn = (from tt in data.TTTHs.Where(p => p.MaBNhan == _mabn)
                           join bv in data.BenhViens on tt.MaBV equals bv.MaBV into a
                           from q1 in a.DefaultIfEmpty()
                           join cb in data.CanBoes on tt.MaCB equals cb.MaCB into c
                           from kq1 in c.DefaultIfEmpty()
                           select new
                           {
                               tt.IDTTTH,
                               tt.SoLC,
                               tt.SoLD,
                               tt.CanNangCon,
                               tt.CCaoCon,
                               TenCB = kq1 == null ? "" : kq1.TenCB,
                               NgaySinh = tt.NgaySinh,
                               GioiTinh = tt.GioiTinh == 0 ? "Nam" : "Nữ",
                               DBien = tt.DBien == -1 ? "" : (tt.DBien == 0 ? "Đẻ thường" : (tt.DBien == 1 ? "Đẻ khó" : (tt.DBien == 2 ? "Mổ đẻ" : "Chết"))),
                               TaiBien = tt.TaiBien == 0 ? "Băng huyết" : (tt.TaiBien == 1 ? "Vỡ tử cung" : (tt.TaiBien == 2 ? "Sản giật" : (tt.TaiBien == 3 ? "Nhiễm trùng" : (tt.TaiBien == 4 ? "Uốn ván" : "")))),
                               Thaichet = tt.Thaichet == 0 ? "Chết lưu" : (tt.Thaichet == 1 ? "Chết trong khi đẻ" : (tt.Thaichet == 2 ? "Chết dưới 7 ngày" : (tt.Thaichet == 3 ? "Chết sau đẻ 28 ngày" : ""))),
                           }).ToList();
                grcNhapCT.DataSource = qbn.ToList();
            }
        }
        #endregion

        private void groupControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void frm_TheoDoiSinhDe_Load(object sender, EventArgs e)
        {
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            txtMaBNhan.Text = _mabn.ToString();
            EnableControl(true);
            #region danh sách tai biến sản khoa
            List<TaiBien> lTaiBien = new List<TaiBien>();
            lTaiBien.Add(new TaiBien { Stt = 0, Ten = "Băng huyết" });
            lTaiBien.Add(new TaiBien { Stt = 1, Ten = "Vỡ tử cung" });
            lTaiBien.Add(new TaiBien { Stt = 2, Ten = "Sản giật" });
            lTaiBien.Add(new TaiBien { Stt = 3, Ten = "Nhiễm trùng" });
            lTaiBien.Add(new TaiBien { Stt = 4, Ten = "Uốn ván" });
            lupTaiBien.Properties.DataSource = lTaiBien.ToList();
            #endregion
            #region danh sách diễn biến cuộc đẻ
            List<DienBien> lDienBien = new List<DienBien>();
            lDienBien.Add(new DienBien { Stt = 0, Ten = "Đẻ thường" });
            lDienBien.Add(new DienBien { Stt = 1, Ten = "Đẻ khó" });
            lDienBien.Add(new DienBien { Stt = 2, Ten = "Mổ đẻ" });
            lDienBien.Add(new DienBien { Stt = 3, Ten = "Chết" });
            lupDBCuocDe.Properties.DataSource = lDienBien.ToList();
            #endregion
            #region danh sách thai chết
            List<ThaiChet> lThaiChet = new List<ThaiChet>();
            lThaiChet.Add(new ThaiChet { Stt = 0, Ten = "Chết lưu" });
            lThaiChet.Add(new ThaiChet { Stt = 1, Ten = "Chết trong khi đẻ" });
            lThaiChet.Add(new ThaiChet { Stt = 2, Ten = "Chết dưới 7 ngày" });
            lThaiChet.Add(new ThaiChet { Stt = 3, Ten = "Chết sau đẻ 28 ngày" });
            lupThaiChet.Properties.DataSource = lThaiChet.ToList();
            #endregion
            #region danh sách tiền sử nạo thai
            List<TSNaoThai> lTSNaoThai = new List<TSNaoThai>();
            lTSNaoThai.Add(new TSNaoThai { Stt = 0, Ten = "Đẻ" });
            lTSNaoThai.Add(new TSNaoThai { Stt = 1, Ten = "Nạo thai" });
            lTSNaoThai.Add(new TSNaoThai { Stt = 2, Ten = "Hút thai" });
            lupTienSuNaoThai.Properties.DataSource = lTSNaoThai.ToList();
            #endregion
            #region danh sách cán bộ
            var qcb = (from cb in data.CanBoes select new { cb.MaCB, cb.TenCB }).Distinct().ToList();
            lupCBTH.Properties.DataSource = qcb.OrderBy(p => p.TenCB).ToList();
            #endregion
            #region danh sách bệnh viện
            var qbv = (from bv in data.BenhViens select new { bv.MaBV, bv.TenBV }).ToList();
            lupBV.Properties.DataSource = qbv.OrderBy(p => p.TenBV).ToList();
            #endregion
            BindingGridview();
            if (_mabn > 0)
            {
                var q = (from bn in data.BenhNhans.Where(p => p.MaBNhan == _mabn)
                         select new { bn.TenBNhan, bn.Tuoi }).ToList();
                if (q.Count > 0)
                {
                    txtTenBNhan.Text = q.First().TenBNhan;
                    txtTuoi.Text = q.First().Tuoi.ToString();
                }
                var qttth = (from a in data.TTTHs.Where(p => p.MaBNhan == _mabn) select a).ToList();
                if (qttth.Count > 0)
                {
                    btnSua.Enabled = true;
                    btnXoa.Enabled = true;
                }
                else
                {
                    btnSua.Enabled = false;
                    btnXoa.Enabled = false;
                }
            }
        }

        private void txtLanDe_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void txtSoCon_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void txtCanNang_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void txtChieuCao_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void grvNhapCT_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (grvNhapCT.GetFocusedRowCellValue(colID) != null && grvNhapCT.GetFocusedRowCellValue(colID).ToString() != "")
            {
                data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                int id = Convert.ToInt32(grvNhapCT.GetFocusedRowCellValue(colID));
                txtID.Text = id.ToString();
                var qttth = (from a in data.TTTHs.Where(p => p.IDTTTH == id) select a).First();
                if (qttth != null)
                {
                    txtLanDe.Text = qttth.SoLD.ToString();
                    txtSoCon.Text = qttth.SoLC.ToString();
                    dateNgaySinh.DateTime = Convert.ToDateTime(qttth.NgaySinh);
                    if (qttth.GioiTinh == 0)
                        rdgGTinh.SelectedIndex = 0;
                    else
                        rdgGTinh.SelectedIndex = 1;
                    txtCanNang.Text = qttth.CanNangCon.Value.ToString();
                    txtChieuCao.Text = qttth.CCaoCon.Value.ToString();
                    lupThaiChet.EditValue = (qttth.Thaichet == -1) ? null : qttth.Thaichet;
                    lupTienSuNaoThai.EditValue = (qttth.Ploai == -1) ? null : qttth.Ploai;
                    lupBV.EditValue = (qttth.MaBV == null) ? "" : qttth.MaBV;
                    lupCBTH.EditValue = (qttth.MaCB == null) ? "" : qttth.MaCB;
                    lupDBCuocDe.EditValue = (qttth.DBien == -1) ? null : qttth.DBien;
                    lupTaiBien.EditValue = (qttth.TaiBien == -1) ? null : qttth.TaiBien.Value.ToString();
                }
            }
        }

        private void btnMoi_Click(object sender, EventArgs e)
        {
            ResetControl();
            EnableControl(false);
            if (_mabn > 0)
            {
                var qttth = (from a in data.TTTHs.Where(p => p.MaBNhan == _mabn) select a).ToList();
                if (qttth.Count > 0)
                {
                    txtLanDe.Text = (qttth.Count() + 1).ToString();
                }
            }
            _tt = 1;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtID.Text))
            {
                data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                int id = int.Parse(txtID.Text);
                DialogResult _reuslt = MessageBox.Show("Bạn thực sự muốn xóa chi tiết đẻ của BN: " + txtTenBNhan.Text, "Xóa bản ghi", MessageBoxButtons.YesNo, MessageBoxIcon.Question); if (_reuslt == DialogResult.Yes)
                {
                    var q = (from tt in data.TTTHs.Where(p => p.IDTTTH == id) select tt).FirstOrDefault();
                    data.TTTHs.Remove(q);
                    if (data.SaveChanges() >= 0)
                    {
                        MessageBox.Show("Xóa thành công");
                        frm_TheoDoiSinhDe_Load(sender, e);
                    }
                }
            }
        }

        #region kiểm tra
        private bool KiemTra()
        {
            bool kt = true;
            if (string.IsNullOrEmpty(txtSoCon.Text))
            {
                MessageBox.Show("Bạn phải nhập số con.");
                txtSoCon.Focus();
                kt = false;
            }
            if (string.IsNullOrEmpty(txtLanDe.Text))
            {
                MessageBox.Show("Bạn phải nhập số lần đẻ.");
                txtLanDe.Focus();
                kt = false;
            }
            if (string.IsNullOrEmpty(txtCanNang.Text))
            {
                MessageBox.Show("Bạn phải nhập số cân nặng (gram).");
                txtCanNang.Focus();
                kt = false;
            }
            if (string.IsNullOrEmpty(txtChieuCao.Text))
            {
                MessageBox.Show("Bạn phải nhập chiều cao (cm).");
                txtChieuCao.Focus();
                kt = false;
            }
            return kt;
        }
        #endregion

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtMaBNhan.Text))
            {
                if (KiemTra())
                {
                    switch (_tt)
                    {
                        case 1:
                            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                            TTTH tt = new TTTH();
                            tt.MaBNhan = Convert.ToInt32(txtMaBNhan.Text);
                            tt.SoLC = Convert.ToInt32(txtSoCon.Text);
                            tt.SoLD = Convert.ToInt32(txtLanDe.Text);
                            tt.NgaySinh = Convert.ToDateTime(dateNgaySinh.DateTime);
                            tt.GioiTinh = Convert.ToInt32(rdgGTinh.SelectedIndex);
                            tt.CanNangCon = Convert.ToDouble(txtCanNang.Text);
                            tt.CCaoCon = Convert.ToDouble(txtChieuCao.Text);
                            tt.Thaichet = lupThaiChet.EditValue == null ? -1 : Convert.ToInt32(lupThaiChet.EditValue);
                            tt.MaBV = lupBV.EditValue == null ? null : lupBV.EditValue.ToString();
                            tt.MaCB = lupCBTH.EditValue == null ? null : lupCBTH.EditValue.ToString();
                            tt.DBien = lupDBCuocDe.EditValue == null ? -1 : Convert.ToInt32(lupDBCuocDe.EditValue);
                            tt.TaiBien = lupTaiBien.EditValue == null ? -1 : Convert.ToInt32(lupTaiBien.EditValue);
                            tt.Ploai = lupTienSuNaoThai.EditValue == null ? -1 : Convert.ToInt32(lupTienSuNaoThai.EditValue);
                            data.TTTHs.Add(tt);
                            if (data.SaveChanges() >= 0)
                            {
                                MessageBox.Show("Tạo mới thành công");
                                frm_TheoDoiSinhDe_Load(sender, e);
                            }
                            break;
                        case 2:
                            if (!string.IsNullOrEmpty(txtID.Text))
                            {
                                int id = Convert.ToInt32(txtID.Text);
                                TTTH sua = data.TTTHs.Single(p => p.IDTTTH == id);
                                sua.MaBNhan = Convert.ToInt32(txtMaBNhan.Text);
                                sua.SoLC = Convert.ToInt32(txtSoCon.Text);
                                sua.SoLD = Convert.ToInt32(txtLanDe.Text);
                                sua.NgaySinh = Convert.ToDateTime(dateNgaySinh.DateTime);
                                sua.GioiTinh = Convert.ToInt32(rdgGTinh.SelectedIndex);
                                sua.CanNangCon = Convert.ToDouble(txtCanNang.Text);
                                sua.CCaoCon = Convert.ToDouble(txtChieuCao.Text);
                                sua.Thaichet = lupThaiChet.EditValue == null ? -1 : Convert.ToInt32(lupThaiChet.EditValue);
                                sua.MaBV = lupBV.EditValue == null ? null : lupBV.EditValue.ToString();
                                sua.MaCB = lupCBTH.EditValue == null ? null : lupCBTH.EditValue.ToString();
                                sua.DBien = lupDBCuocDe.EditValue == null ? -1 : Convert.ToInt32(lupDBCuocDe.EditValue);
                                sua.TaiBien = lupTaiBien.EditValue == null ? -1 : Convert.ToInt32(lupTaiBien.EditValue);
                                sua.Ploai = lupTienSuNaoThai.EditValue == null ? -1 : Convert.ToInt32(lupTienSuNaoThai.EditValue);
                                if (data.SaveChanges() >= 0)
                                {
                                    _tt = 0;
                                    MessageBox.Show("Sửa thành công");
                                    frm_TheoDoiSinhDe_Load(sender, e);
                                }
                            }
                            else
                                MessageBox.Show("CHưa chọn bản ghi nào để sửa.");
                            break;
                            btnLuu.Enabled = false;
                    }
                }
            }
            else
                MessageBox.Show("Luu không thành công, vì chưa chọn bệnh nhân");
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtID.Text))
            {
                EnableControl(false);
                _tt = 2;
            }
            else
                MessageBox.Show("Không có bản ghi nào để sửa");
        }
    }
}