using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;

namespace QLBV.FormNhap
{
    public partial class us_SuDung : DevExpress.XtraEditors.XtraUserControl
    {
        public us_SuDung()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        List<NhapD> _lNhapDuoc = new List<NhapD>();
        List<NhapDct> _lNhapDct = new List<NhapDct>();
        private void Enablebutton(bool Status)
        {
            btnLuu.Enabled = !Status;
            btnMoi.Enabled = Status;
            btnSua.Enabled = Status;
            btnXoa.Enabled = Status;
            grvNhapCT.OptionsBehavior.Editable = !Status;
            gcTimKiem.Enabled = Status;
        }
        private void EnableControl(bool status)
        {
            dtNgayNhap.Properties.ReadOnly = !status;
            txtSoCT.Properties.ReadOnly = !status;
            lupMaKP.Properties.ReadOnly = !status;
            txtGhiChu.Properties.ReadOnly = !status;
            lupMaXP.Properties.ReadOnly = !status;
            radHinhthucx.Properties.ReadOnly = !status;
            grcNhap.Enabled = !status;
        }
        private void ResetControl()
        {
            dtNgayNhap.EditValue = System.DateTime.Now;
            txtIDNhap.Text = "";
            txtSoCT.Text = "";
            txtGhiChu.Text = "";
            lupMaXP.EditValue = "";
            lupMaKP.EditValue = 0;
        }
        #region KT
        //Kiem tra trước khi lưu
        private bool KT()
        {
            if (dtNgayNhap.EditValue == null || dtNgayNhap.EditValue.ToString() == "")
            {
                MessageBox.Show("Bạn chưa chọn Ngày xuất!");
                dtNgayNhap.Focus();
                return false;
            }
            if (lupMaKP.EditValue == null || lupMaKP.EditValue.ToString() == "")
            {
                MessageBox.Show("Bạn chưa chon kho SD!");
                lupMaKP.Focus();
                return false;
            }
            if (lupMaKP.EditValue == null || lupMaKP.EditValue.ToString() == "")
            {
                MessageBox.Show("Bạn chưa chon nơi báo cáo!");
                lupMaKP.Focus();
                return false;
            }
            if (radHinhthucx.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn hình thức sử dụng!");
                lupMaKP.Focus();
                return false;
            }
            return true;
        }
        #endregion
        DateTime _dttu = System.DateTime.Now;
        DateTime _dtden = System.DateTime.Now;
        int _makho = 0;
        string _soPhieu = "";
        int _mkpnx = 0;
        int TTLuu = 0;
        #region hàm tìm kiếm
        private void TimKiem()
        {
            _dttu = DungChung.Ham.NgayTu(dtTimTuNgay.DateTime);
            _dtden = DungChung.Ham.NgayDen(dtTimDenNgay.DateTime);
            if (lupTimMaKPnx.EditValue != null && lupTimMaKPnx.EditValue.ToString() != "")
                _mkpnx = Convert.ToInt32(lupTimMaKPnx.EditValue);
            if (lupTimMaKP.EditValue != null && lupTimMaKP.EditValue.ToString() != "")
                _makho = Convert.ToInt32(lupTimMaKP.EditValue);
            if (!string.IsNullOrEmpty(txtTimKiem.Text) && txtTimKiem.Text != "Số phiếu|Số CT")
                _soPhieu = txtTimKiem.Text;
            if (_mkpnx > 0)
            {
                _lNhapDuoc = (from nd in _data.NhapDs.Where(p => p.PLoai == 5)
                              where (nd.NgayNhap >= _dttu && nd.NgayNhap <= _dtden)
                              where (nd.SoCT.Contains(_soPhieu))
                              where (nd.MaKP == _makho)
                              where (nd.MaKPnx == _mkpnx)
                              select nd).OrderByDescending(p => p.NgayNhap).OrderByDescending(p => p.IDNhap).ToList();
            }
            else
            {
                _lNhapDuoc = (from nd in _data.NhapDs.Where(p => p.PLoai == 5)
                              where (nd.NgayNhap >= _dttu && nd.NgayNhap <= _dtden)
                              where (nd.SoCT.Contains(_soPhieu))
                              where (nd.MaKP == _makho)
                              select nd).OrderByDescending(p => p.NgayNhap).OrderByDescending(p => p.IDNhap).ToList();
            }
            grcNhap.DataSource = _lNhapDuoc.ToList();
        }
        #endregion
        private void usXuatDuoc_Load(object sender, EventArgs e)
        {
            Enablebutton(true);
            EnableControl(false);
            dtTimDenNgay.DateTime = System.DateTime.Now;
            dtTimTuNgay.DateTime = System.DateTime.Now;
            dtNgayNhap.EditValue = System.DateTime.Now;
            if (DungChung.Bien.MaBV == "24012")
            {
                colSoLo.Visible = true;
                colHanDung.Visible = true;
            }
            if (DungChung.Bien.PLoaiKP == DungChung.Bien.st_PhanLoaiKP.Admin || DungChung.Bien.MaBV == "30009")
            {
                lupTimMaKP.Enabled = true;
            }
            else
            {
                lupTimMaKP.Enabled = false;
            }
            var q = (from KhoaKham in _data.KPhongs.Where(p => p.PLoai == ("Khoa dược")).Where(p => p.Status == 1).OrderBy(p => p.TenKP) select new { KhoaKham.TenKP, KhoaKham.MaKP }).ToList();
            if (q.Count > 0)
            {
                lupMaKP.Properties.DataSource = q.ToList();
                lupTimMaKP.Properties.DataSource = q.ToList();
                lupMaKPds.DataSource = q.ToList();
            }
            var qi = (from KhoaKham in _data.KPhongs.Where(p => p.Status == 1)
                      where (KhoaKham.PLoai.Contains("Lâm sàng") || KhoaKham.PLoai.Contains("khu vực") || KhoaKham.PLoai.Contains("Xã phường") || KhoaKham.PLoai.Contains("Khoa dược"))
                      select new { KhoaKham.TenKP, KhoaKham.MaKP, KhoaKham.PLoai }).OrderBy(p => p.TenKP).ToList();
            lupTimMaKPnx.Properties.DataSource = qi.ToList();
            lupMaXP.Properties.DataSource = qi;
            lupTimMaKP.EditValue = DungChung.Bien.MaKP;
            TimKiem();
            int idct = 0;
            if (!string.IsNullOrEmpty(txtIDNhap.Text))
            {
                idct = Convert.ToInt32(txtIDNhap.Text);
            }
            _lNhapDct = _data.NhapDcts.Where(p => p.IDNhap == idct).ToList();
            binNhapDuocct.DataSource = _lNhapDct.ToList();
            grcNhapCT.DataSource = binNhapDuocct;

        }

        private void btnMoi_Click(object sender, EventArgs e)
        {
            bool _sua = true;
            _sua = DungChung.Ham.checkQuyen(this.Name)[0];
            if (_sua)
            {
                Enablebutton(false);
                EnableControl(true);
                ResetControl();
                txtSoCT.Focus();
                lupMaKP.EditValue = DungChung.Bien.MaKP;
                _lNhapDct = _data.NhapDcts.Where(p => p.IDNhap == 0).ToList();
                binNhapDuocct.DataSource = _lNhapDct;
                grcNhapCT.DataSource = binNhapDuocct;
                TTLuu = 1;
            }
        }

        private void grvNhapCT_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            DateTime _ngaynhap = System.DateTime.Now;
            _ngaynhap = dtNgayNhap.DateTime;
            _ngaynhap = DungChung.Ham.NgayDen(_ngaynhap);
            int madv = 0;
            int makho = 0;
            int makpx = 0;
            if (grvNhapCT.GetFocusedRowCellValue(colMaDV) != null)
                madv = Convert.ToInt32(grvNhapCT.GetFocusedRowCellValue(colMaDV));
            if (lupMaKP.EditValue != null)
                makho = Convert.ToInt32(lupMaKP.EditValue);
            if (lupMaXP.EditValue != null)
                makpx = Convert.ToInt32(lupMaXP.EditValue);
            double _slTonChuaLuu = 0, _slTonDaLuu = 0;
            switch (e.Column.Name)
            {
                case "colMaDV":

                    //grvNhapCT.SetFocusedRowCellValue(colDonGia, "");
                    //for (int i = 0; i < cboDonGia.Items.Count; i++)
                    //{
                    //    cboDonGia.Items.RemoveAt(i);
                    //}
                    cboDonGia.Items.Clear();
                    var gia = (from nduoc in _data.NhapDs.Where(p => p.PLoai == 2).Where(p => p.MaKP == makho && p.MaKPnx == makpx && p.NgayNhap <= _ngaynhap)
                               join nhapduoc in _data.NhapDcts.Where(p => p.MaDV == madv) on nduoc.IDNhap equals nhapduoc.IDNhap
                               group new { nhapduoc } by new { nhapduoc.DonGia } into kq
                               select new { kq.Key.DonGia }).ToList();
                    string gias = "0";
                    if (gia.Count > 0)
                    {
                        gias = gia.First().DonGia.ToString();
                        foreach (var g in gia)
                        {
                            cboDonGia.Items.Add(g.DonGia);
                        }
                    }

                    // kết thúc
                    grvNhapCT.SetFocusedRowCellValue(colDonVi, DungChung.Ham._getDonVi(_data, madv));
                    //grvNhapCT.SetFocusedRowCellValue(colDonGia, DungChung.Ham._getGia(_data, madv, makho));
                    grvNhapCT.SetFocusedRowCellValue(colDonGia, gias);
                    grvNhapCT.SetFocusedRowCellValue(colMaCC, DungChung.Bien._maCC);
                    //  grvNhapCT.ViewCaption = "Số lượng tồn: " + DungChung.Bien.SoLuongTon.ToString();
                    grvNhapCT.SetFocusedRowCellValue(colSoLuong, 0);
                    break;
                case "colDonGia":
                    if (grvNhapCT.GetFocusedRowCellValue(colSoLuong) != null && grvNhapCT.GetFocusedRowCellValue(colSoLuong).ToString() != "")
                    {
                        double a = Convert.ToDouble(grvNhapCT.GetFocusedRowCellValue(colSoLuong).ToString());
                        if (grvNhapCT.GetFocusedRowCellValue(colDonGia) != null && grvNhapCT.GetFocusedRowCellValue(colDonGia).ToString() != "")
                        {
                            double b = Convert.ToDouble(grvNhapCT.GetFocusedRowCellValue(colDonGia));
                            grvNhapCT.SetFocusedRowCellValue(colThanhTien, a * b);
                            var macc = (from nd in _data.NhapDs.Where(p => p.PLoai == 1).Where(p => p.MaKP == makho)
                                        join ndct in _data.NhapDcts.Where(p => p.MaDV == madv).Where(p => p.DonGia == b) on nd.IDNhap equals ndct.IDNhap
                                        select ndct.MaCC).ToList();
                            if (macc.Count > 0)
                            {
                                grvNhapCT.SetFocusedRowCellValue(colMaCC, macc.First());
                            }
                        }
                    }

                    break;
                case "colSoLuong":
                    if (grvNhapCT.GetFocusedRowCellValue(colSoLuong) != null && grvNhapCT.GetFocusedRowCellValue(colSoLuong).ToString() != "")
                    {
                        double a = Convert.ToDouble(grvNhapCT.GetFocusedRowCellValue(colSoLuong));
                        double _dongia = 0;
                        if (grvNhapCT.GetFocusedRowCellValue(colDonGia) != null && grvNhapCT.GetFocusedRowCellValue(colDonGia).ToString() != "")
                            _dongia = Convert.ToDouble(grvNhapCT.GetFocusedRowCellValue(colDonGia));
                        if (a >= 0)
                        {
                            int _madv = grvNhapCT.GetFocusedRowCellValue(colMaDV) == null ? 0 : Convert.ToInt32(grvNhapCT.GetFocusedRowCellValue(colMaDV));
                            for (int i = 0; i <= grvNhapCT.RowCount; i++)
                            {
                                if (grvNhapCT.GetRowCellValue(i, colMaDV) != null && grvNhapCT.GetRowCellValue(i, colDonGia) != null)
                                    if (grvNhapCT.GetRowCellValue(i, colSoLuong) != null)
                                    {
                                        if (_madv == Convert.ToInt32(grvNhapCT.GetRowCellValue(i, colMaDV)) && _dongia == Convert.ToDouble(grvNhapCT.GetRowCellValue(i, colDonGia)))
                                        {
                                            if (grvNhapCT.GetRowCellValue(i, colIDNhapct) != null && Convert.ToInt32(grvNhapCT.GetRowCellValue(i, colIDNhapct)) <= 0)
                                            {
                                                _slTonChuaLuu += Convert.ToDouble(grvNhapCT.GetRowCellValue(i, colSoLuong));
                                            }
                                            else
                                            {
                                                int id = Convert.ToInt32(grvNhapCT.GetRowCellValue(i, colIDNhapct));
                                                var layton = _data.NhapDcts.Where(p => p.IDNhapct == id).Select(p => p.SoLuongSD).ToList();
                                                _slTonDaLuu += layton.Sum(p => p);
                                            }
                                        }
                                    }
                            }
                            double ton = 1000000, dongia = 0;
                            if (grvNhapCT.GetFocusedRowCellValue(colDonGia) != null && grvNhapCT.GetFocusedRowCellValue(colDonGia).ToString() != "")
                            {
                                dongia = Convert.ToDouble(grvNhapCT.GetFocusedRowCellValue(colDonGia));
                            }
                            // kiểm tra tồn
                            var ktton = (from nduoc in _data.NhapDs.Where(p => p.PLoai == 2 || p.PLoai == 5).Where(p => p.MaKP == makho && p.MaKPnx == makpx && p.NgayNhap <= _ngaynhap)
                                         join nhapduoc in _data.NhapDcts.Where(p => p.MaDV == madv).Where(p => p.DonGia == dongia) on nduoc.IDNhap equals nhapduoc.IDNhap
                                         group new { nhapduoc } by new { nhapduoc.DonGia } into kq
                                         select new { kq.Key.DonGia, soluong = kq.Sum(p => p.nhapduoc.SoLuongX) - kq.Sum(p => p.nhapduoc.SoLuongSD) }).ToList();
                            switch (TTLuu)
                            {
                                case 1: // khi tao don moi

                                    if (ktton.Count > 0)
                                        ton = ktton.First().soluong;
                                    ton = ton - _slTonChuaLuu;
                                    if (ton < 0)
                                        ton = 0;
                                    if (a <= ton)
                                    {
                                        if (grvNhapCT.GetFocusedRowCellValue(colDonGia) != null && grvNhapCT.GetFocusedRowCellValue(colDonGia).ToString() != "")
                                        {
                                            double b = Convert.ToDouble(grvNhapCT.GetFocusedRowCellValue(colDonGia));
                                            grvNhapCT.SetFocusedRowCellValue(colThanhTien, a * b);

                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Số lượng trong kho không đủ: " + ton.ToString());
                                        grvNhapCT.SetFocusedRowCellValue(colThanhTien, 0);
                                        grvNhapCT.SetFocusedRowCellValue(colSoLuong, 0);


                                        grvNhapCT.ViewCaption = "Số lượng tồn: " + DungChung.Bien.SoLuongTon.ToString();
                                    }
                                    break;
                                case 2:

                                    if (ktton.Count > 0)
                                        ton = ktton.First().soluong;
                                    if (grvNhapCT.GetFocusedRowCellValue(colIDNhapct) != null && Convert.ToInt32(grvNhapCT.GetFocusedRowCellValue(colIDNhapct)) <= 0)
                                    {
                                        ton = ton - _slTonChuaLuu;
                                    }
                                    else
                                    {
                                        ton = ton + _slTonDaLuu;
                                    }
                                    if (a <= ton)
                                    {
                                        if (grvNhapCT.GetFocusedRowCellValue(colDonGia) != null && grvNhapCT.GetFocusedRowCellValue(colDonGia).ToString() != "")
                                        {
                                            double b = Convert.ToDouble(grvNhapCT.GetFocusedRowCellValue(colDonGia));
                                            grvNhapCT.SetFocusedRowCellValue(colThanhTien, a * b);

                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Số lượng trong kho không đủ");
                                        grvNhapCT.SetFocusedRowCellValue(colThanhTien, 0);
                                        grvNhapCT.SetFocusedRowCellValue(colSoLuong, 0);


                                        grvNhapCT.ViewCaption = "Số lượng tồn: " + DungChung.Bien.SoLuongTon.ToString();
                                    }
                                    break;
                            }

                        }
                        else
                        {
                            MessageBox.Show("Số lượng phải > 0!");
                            grvNhapCT.SetFocusedRowCellValue(colSoLuong, 0);
                            grvNhapCT.SetFocusedRowCellValue(colThanhTien, 0);

                        }
                    }
                    break;


            }
        }

        private void grvNhapCT_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column == STT)
            {
                e.DisplayText = Convert.ToString(e.RowHandle + 1);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            bool _sua = true;
            _sua = DungChung.Ham.checkQuyen(this.Name)[1];
            if (_sua)
            {
                Enablebutton(false);
                EnableControl(true);
                txtSoCT.Focus();
                TTLuu = 2;
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            bool _sua = true;
            _sua = DungChung.Ham.checkQuyen(this.Name)[2];
            if (_sua)
            {
                if (!string.IsNullOrEmpty(txtIDNhap.Text))
                {
                    int id = Int32.Parse(txtIDNhap.Text);
                    var kt = _data.NhapDs.Where(p => p.IDNhap == id).ToList();
                    if (kt.Count > 0 && kt.First().Status != 1)
                    {
                        DialogResult _result;
                        _result = MessageBox.Show("Bạn muốn xóa chứng từ số: " + txtSoCT.Text, "xóa chứng từ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (_result == DialogResult.Yes)
                        {

                            var xoact = _data.NhapDcts.Where(p => p.IDNhap == id).ToList();
                            foreach (var xoa in xoact)
                            {
                                var _xoa = _data.NhapDcts.Single(p => p.IDNhapct == (xoa.IDNhapct));
                                _data.NhapDcts.Remove(_xoa);

                            }
                            var xoac = _data.NhapDs.Single(p => p.IDNhap == (id));
                            _data.NhapDs.Remove(xoac);
                            _data.SaveChanges();
                            TimKiem();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Chứng từ đã bị khóa, bạn không thể xóa!");
                    }
                }
                else
                {
                    MessageBox.Show("Không có chứng từ để xóa!");
                }
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {

            QLBV_Database.QLBVEntities DataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            // luu bang NhapD
            if (KT())
            {
                switch (TTLuu)
                {
                    case 1:
                        TTLuu = 0;
                        NhapD nhap = new NhapD();
                        nhap.PLoai = 5;
                        nhap.NgayNhap = dtNgayNhap.DateTime;
                        nhap.MaKP = lupMaKP.EditValue == null ? 0 : Convert.ToInt32(lupMaKP.EditValue);
                        nhap.MaKPnx = lupMaXP.EditValue == null ? 0 : Convert.ToInt32(lupMaXP.EditValue);
                        nhap.KieuDon = radHinhthucx.SelectedIndex;
                        if (!string.IsNullOrEmpty(txtGhiChu.Text))
                            nhap.GhiChu = txtGhiChu.Text;
                        if (!string.IsNullOrEmpty(txtSoCT.Text))
                            nhap.SoCT = txtSoCT.Text;
                        else
                            nhap.SoCT = "";
                        DataContext.NhapDs.Add(nhap);
                        string thuockluu = "các thuốc không được lưu:\n";
                        int _ttthuockluu = 0;
                        if (DataContext.SaveChanges() >= 0)
                        {

                            //Luu bang NhapDct
                            // lấy ID max trong bang NhapD
                            int idnhap = 0;
                            var que = (from IDMax in DataContext.NhapDs orderby IDMax.IDNhap descending select IDMax.IDNhap).ToList();
                            if (que.Count > 0)
                            {
                                idnhap = que.First();

                                for (int i = 0; i < grvNhapCT.DataRowCount; i++)
                                {
                                    if (grvNhapCT.GetRowCellValue(i, colMaDV) != null)
                                    {
                                        if (grvNhapCT.GetRowCellValue(i, colSoLuong) != null && grvNhapCT.GetRowCellValue(i, colSoLuong).ToString() != "")
                                        {
                                            NhapDct nhapdct = new NhapDct();
                                            nhapdct.IDNhap = idnhap;
                                            nhapdct.MaDV = Convert.ToInt32(grvNhapCT.GetRowCellValue(i, colMaDV));
                                            nhapdct.SoLo = "";
                                            nhapdct.DonVi = grvNhapCT.GetRowCellValue(i, colDonVi).ToString().Trim();
                                            nhapdct.DonGia = double.Parse(grvNhapCT.GetRowCellValue(i, colDonGia).ToString());
                                            nhapdct.SoLuongSD = double.Parse(grvNhapCT.GetRowCellValue(i, colSoLuong).ToString());
                                            nhapdct.ThanhTienSD = double.Parse(grvNhapCT.GetRowCellValue(i, colThanhTien).ToString());
                                            nhapdct.SoLuongN = 0;
                                            nhapdct.ThanhTienN = 0;
                                            nhapdct.SoLuongX = 0;
                                            nhapdct.ThanhTienX = 0;
                                            nhapdct.SoLuongKK = 0;
                                            nhapdct.ThanhTienKK = 0;
                                            if (grvNhapCT.GetRowCellValue(i, colSoLo) != null && grvNhapCT.GetRowCellValue(i, colSoLo).ToString() != "")
                                                nhapdct.SoLo = grvNhapCT.GetRowCellValue(i, colSoLo).ToString();
                                            if (grvNhapCT.GetRowCellValue(i, colSoDangKy) != null && grvNhapCT.GetRowCellValue(i, colSoDangKy).ToString() != "")
                                                nhapdct.SoDangKy = grvNhapCT.GetRowCellValue(i, colSoDangKy).ToString();
                                            if (grvNhapCT.GetRowCellValue(i, colHanDung) != null && grvNhapCT.GetRowCellValue(i, colHanDung).ToString() != "")
                                                nhapdct.HanDung = Convert.ToDateTime(grvNhapCT.GetRowCellValue(i, colHanDung));
                                            if (grvNhapCT.GetRowCellValue(i, colMaCC) != null && grvNhapCT.GetRowCellValue(i, colMaCC).ToString() != "")
                                                nhapdct.MaCC = grvNhapCT.GetRowCellValue(i, colMaCC).ToString();
                                            else
                                                nhapdct.MaCC = "";
                                            _data.NhapDcts.Add(nhapdct);
                                            _data.SaveChanges();
                                        }
                                        else
                                        {
                                            thuockluu += grvNhapCT.GetRowCellValue(i, colMaDV).ToString() + ", ";
                                            _ttthuockluu = 1;
                                        }
                                    }

                                }
                            }

                        }
                        if (_ttthuockluu == 1)
                            MessageBox.Show(thuockluu);
                        Enablebutton(true);
                        EnableControl(false);
                        TimKiem();
                        break;
                    case 2:

                        if (!string.IsNullOrEmpty(txtIDNhap.Text))
                        {
                            TTLuu = 0;
                            int id = Convert.ToInt32(txtIDNhap.Text);
                            NhapD nhaps = _data.NhapDs.Single(p => p.IDNhap == id);
                            nhaps.NgayNhap = dtNgayNhap.DateTime;
                            nhaps.MaKP = lupMaKP.EditValue == null ? 0 : Convert.ToInt32(lupMaKP.EditValue);
                            nhaps.MaKPnx = lupMaXP.EditValue == null ? 0 : Convert.ToInt32(lupMaXP.EditValue);
                            nhaps.KieuDon = radHinhthucx.SelectedIndex;
                            if (!string.IsNullOrEmpty(txtGhiChu.Text))
                                nhaps.GhiChu = txtGhiChu.Text;
                            if (!string.IsNullOrEmpty(txtSoCT.Text))
                                nhaps.SoCT = txtSoCT.Text;
                            string thuockluus = "các thuốc không được lưu:\n";
                            int _ttthuockluus = 0;
                            DataContext.SaveChanges();
                            //Luu bang NhapDct
                            // lấy ID max trong bang NhapD

                            for (int i = 0; i < grvNhapCT.DataRowCount; i++)
                            {
                                if (grvNhapCT.GetRowCellValue(i, colMaDV) != null)
                                {
                                    if (grvNhapCT.GetRowCellValue(i, colDonGia) != null && grvNhapCT.GetRowCellValue(i, colDonGia).ToString() != "")
                                    {
                                        if (grvNhapCT.GetRowCellValue(i, colSoLuong) != null && grvNhapCT.GetRowCellValue(i, colSoLuong).ToString() != "")
                                        {
                                            int idct = 0;
                                            if (grvNhapCT.GetRowCellValue(i, colIDNhapct) != null && grvNhapCT.GetRowCellValue(i, colIDNhapct).ToString() != "")
                                            {
                                                idct = Convert.ToInt32(grvNhapCT.GetRowCellValue(i, colIDNhapct).ToString());
                                                if (idct <= 0) // them row moi
                                                {
                                                    NhapDct nhapdct = new NhapDct();
                                                    nhapdct.IDNhap = id;
                                                    nhapdct.MaDV = Convert.ToInt32(grvNhapCT.GetRowCellValue(i, colMaDV));
                                                    nhapdct.DonVi = grvNhapCT.GetRowCellValue(i, colDonVi).ToString().Trim();
                                                    nhapdct.SoLo = "";
                                                    nhapdct.DonGia = double.Parse(grvNhapCT.GetRowCellValue(i, colDonGia).ToString());
                                                    nhapdct.SoLuongSD = double.Parse(grvNhapCT.GetRowCellValue(i, colSoLuong).ToString());
                                                    nhapdct.ThanhTienSD = double.Parse(grvNhapCT.GetRowCellValue(i, colThanhTien).ToString());
                                                    if (grvNhapCT.GetRowCellValue(i, colMaCC) != null && grvNhapCT.GetRowCellValue(i, colMaCC).ToString() != "")
                                                        nhapdct.MaCC = grvNhapCT.GetRowCellValue(i, colMaCC).ToString();
                                                    else
                                                        nhapdct.MaCC = "";
                                                    nhapdct.SoLuongN = 0;
                                                    nhapdct.ThanhTienN = 0;
                                                    nhapdct.SoLuongX = 0;
                                                    nhapdct.ThanhTienX = 0;
                                                    nhapdct.SoLuongKK = 0;
                                                    nhapdct.ThanhTienKK = 0;
                                                    if (grvNhapCT.GetRowCellValue(i, colSoLo) != null && grvNhapCT.GetRowCellValue(i, colSoLo).ToString() != "")
                                                        nhapdct.SoLo = grvNhapCT.GetRowCellValue(i, colSoLo).ToString();
                                                    if (grvNhapCT.GetRowCellValue(i, colSoDangKy) != null && grvNhapCT.GetRowCellValue(i, colSoDangKy).ToString() != "")
                                                        nhapdct.SoDangKy = grvNhapCT.GetRowCellValue(i, colSoDangKy).ToString();
                                                    if (grvNhapCT.GetRowCellValue(i, colHanDung) != null && grvNhapCT.GetRowCellValue(i, colHanDung).ToString() != "")
                                                        nhapdct.HanDung = Convert.ToDateTime(grvNhapCT.GetRowCellValue(i, colHanDung));
                                                    _data.NhapDcts.Add(nhapdct);
                                                    _data.SaveChanges();
                                                }
                                                else
                                                {
                                                    NhapDct nhapdcts = _data.NhapDcts.Single(p => p.IDNhapct == idct);
                                                    nhapdcts.MaDV = Convert.ToInt32(grvNhapCT.GetRowCellValue(i, colMaDV));
                                                    nhapdcts.DonVi = grvNhapCT.GetRowCellValue(i, colDonVi).ToString().Trim();
                                                    nhapdcts.DonGia = double.Parse(grvNhapCT.GetRowCellValue(i, colDonGia).ToString());
                                                    nhapdcts.SoLuongSD = double.Parse(grvNhapCT.GetRowCellValue(i, colSoLuong).ToString());
                                                    nhapdcts.ThanhTienSD = double.Parse(grvNhapCT.GetRowCellValue(i, colThanhTien).ToString());
                                                    if (grvNhapCT.GetRowCellValue(i, colMaCC) != null && grvNhapCT.GetRowCellValue(i, colMaCC).ToString() != "")
                                                        nhapdcts.MaCC = grvNhapCT.GetRowCellValue(i, colMaCC).ToString();
                                                    else
                                                        nhapdcts.MaCC = "";
                                                    if (grvNhapCT.GetRowCellValue(i, colSoLo) != null && grvNhapCT.GetRowCellValue(i, colSoLo).ToString() != "")
                                                        nhapdcts.SoLo = grvNhapCT.GetRowCellValue(i, colSoLo).ToString();
                                                    if (grvNhapCT.GetRowCellValue(i, colSoDangKy) != null && grvNhapCT.GetRowCellValue(i, colSoDangKy).ToString() != "")
                                                        nhapdcts.SoDangKy = grvNhapCT.GetRowCellValue(i, colSoDangKy).ToString();
                                                    if (grvNhapCT.GetRowCellValue(i, colHanDung) != null && grvNhapCT.GetRowCellValue(i, colHanDung).ToString() != "")
                                                        nhapdcts.HanDung = Convert.ToDateTime(grvNhapCT.GetRowCellValue(i, colHanDung));
                                                    _data.SaveChanges();
                                                }
                                            }
                                        }
                                        else
                                        {
                                            thuockluus += grvNhapCT.GetRowCellValue(i, colMaDV).ToString() + ", ";
                                            _ttthuockluus = 1;
                                        }
                                    }
                                    else
                                    {
                                        thuockluus += grvNhapCT.GetRowCellValue(i, colMaDV).ToString() + ", ";
                                        _ttthuockluus = 1;
                                    }
                                }
                            }
                            if (_ttthuockluus == 1)
                                MessageBox.Show(thuockluus);
                            Enablebutton(true);
                            EnableControl(false);
                            TimKiem();
                        }

                        break;
                }
                TTLuu = 0;
            }
        }

        private void txtTimKiem_Leave(object sender, EventArgs e)
        {
            TimKiem();
        }

        private void dtTimTuNgay_Leave(object sender, EventArgs e)
        {
            TimKiem();
        }

        private void dtTimDenNgay_Leave(object sender, EventArgs e)
        {
            TimKiem();
        }

        private void lupTimMaKP_EditValueChanged(object sender, EventArgs e)
        {
            TimKiem();
        }

        private void lupTimMaKPnx_EditValueChanged(object sender, EventArgs e)
        {
            TimKiem();
        }
        private void grvNhap_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            int id = 0;
            if (grvNhap.GetFocusedRowCellValue(colIDNhaps) != null && grvNhap.GetFocusedRowCellValue(colIDNhaps).ToString() != "")
            {
                txtIDNhap.Text = grvNhap.GetFocusedRowCellValue(colIDNhaps).ToString();
                id = Convert.ToInt32(grvNhap.GetFocusedRowCellValue(colIDNhaps));
                txtSoCT.Text = _lNhapDuoc.Where(p => p.IDNhap == id).First().SoCT;
                lupMaKP.EditValue = _lNhapDuoc.Where(p => p.IDNhap == id).First().MaKP;
                lupMaXP.EditValue = _lNhapDuoc.Where(p => p.IDNhap == id).First().MaKPnx;
                txtGhiChu.Text = _lNhapDuoc.Where(p => p.IDNhap == id).First().GhiChu;
                dtNgayNhap.DateTime = _lNhapDuoc.Where(p => p.IDNhap == id).First().NgayNhap.Value;
                //int kieudon = -1;
                if (_lNhapDuoc.Where(p => p.IDNhap == id).First().KieuDon != null)
                    radHinhthucx.SelectedIndex = _lNhapDuoc.Where(p => p.IDNhap == id).First().KieuDon.Value;
                else
                    radHinhthucx.SelectedIndex = -1;
                _lNhapDct = _data.NhapDcts.Where(p => p.IDNhap == id).ToList();
                binNhapDuocct.DataSource = _lNhapDct;
                grcNhapCT.DataSource = binNhapDuocct;
            }
            else
            {
                radHinhthucx.SelectedIndex = -1;
                txtIDNhap.Text = "";
                lupMaXP.EditValue = 0;
                lupMaKP.EditValue = 0;
                txtGhiChu.Text = "";
                txtSoCT.Text = "";
                _lNhapDct = _data.NhapDcts.Where(p => p.IDNhap == id).ToList();
                binNhapDuocct.DataSource = _lNhapDct;
                grcNhapCT.DataSource = binNhapDuocct;
            }
        }

        private void grvNhap_DataSourceChanged(object sender, EventArgs e)
        {
            int id = 0;
            if (grvNhap.GetFocusedRowCellValue(colIDNhaps) != null && grvNhap.GetFocusedRowCellValue(colIDNhaps).ToString() != "")
            {
                txtIDNhap.Text = grvNhap.GetFocusedRowCellValue(colIDNhaps).ToString();
                id = Convert.ToInt32(grvNhap.GetFocusedRowCellValue(colIDNhaps));
                txtSoCT.Text = _lNhapDuoc.Where(p => p.IDNhap == id).First().SoCT;
                lupMaKP.EditValue = _lNhapDuoc.Where(p => p.IDNhap == id).First().MaKP;
                lupMaXP.EditValue = _lNhapDuoc.Where(p => p.IDNhap == id).First().MaKPnx;
                txtGhiChu.Text = _lNhapDuoc.Where(p => p.IDNhap == id).First().GhiChu;
                dtNgayNhap.DateTime = _lNhapDuoc.Where(p => p.IDNhap == id).First().NgayNhap.Value;
                _lNhapDct = _data.NhapDcts.Where(p => p.IDNhap == id).ToList();
                binNhapDuocct.DataSource = _lNhapDct;
                grcNhapCT.DataSource = binNhapDuocct;
            }
            else
            {
                txtIDNhap.Text = "";
                lupMaXP.EditValue = 0;
                lupMaKP.EditValue = 0;
                txtGhiChu.Text = "";
                txtSoCT.Text = "";
                dtNgayNhap.DateTime = System.DateTime.Now;
                _lNhapDct = _data.NhapDcts.Where(p => p.IDNhap == id).ToList();
                binNhapDuocct.DataSource = _lNhapDct;
                grcNhapCT.DataSource = binNhapDuocct;
            }
        }

        private void grvNhapCT_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Column.Name == "colXoaCT")
            {
                if (!string.IsNullOrEmpty(txtIDNhap.Text))
                {
                    int id = Int32.Parse(txtIDNhap.Text);
                    var kt = _data.NhapDs.Where(p => p.IDNhap == id).ToList();
                    if (kt.Count > 0 && kt.First().Status != 1)
                    {
                        if (grvNhapCT.GetFocusedRowCellValue(colIDNhapct) != null && grvNhapCT.GetFocusedRowCellValue(colIDNhapct).ToString() != "")
                        {
                            if (grvNhapCT.GetFocusedRowCellDisplayText(colMaDV) != null)
                            {
                                string tenthuoc = grvNhapCT.GetFocusedRowCellDisplayText(colMaDV).ToString();
                                if (MessageBox.Show("Bạn muốn xóa thuốc: " + tenthuoc, "Xóa chi tiết", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                {
                                    int idct = Convert.ToInt32(grvNhapCT.GetFocusedRowCellValue(colIDNhapct));
                                    var _xoact = _data.NhapDcts.Single(p => p.IDNhapct == (idct));
                                    _data.NhapDcts.Remove(_xoact);
                                    _data.SaveChanges();
                                    _lNhapDct = _data.NhapDcts.Where(p => p.IDNhap == id).ToList();
                                    binNhapDuocct.DataSource = _lNhapDct;
                                    grcNhapCT.DataSource = binNhapDuocct;
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Chứng từ đã bị khóa, bạn không thể xóa!");
                    }
                }
                else
                {
                    MessageBox.Show("Không có chứng từ để xóa!");
                }
            }
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            int id = 0;
            if (!string.IsNullOrEmpty(txtIDNhap.Text))
                id = Convert.ToInt32(txtIDNhap.Text);
            FormThamSo.frmTsBcSuDungThuoc frm106 = new FormThamSo.frmTsBcSuDungThuoc();
            frm106.ShowDialog();

        }

        private void labelControl10_Click(object sender, EventArgs e)
        {

        }

        private void grvNhapCT_FocusedRowChanged_1(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            int madv = 0;
            int makho = 0;
            int makpx = 0;
            if (lupMaXP.EditValue != null)
                makpx = Convert.ToInt32(lupMaXP.EditValue);
            if (grvNhapCT.GetFocusedRowCellValue(colMaDV) != null)
            {
                madv = Convert.ToInt32(grvNhapCT.GetFocusedRowCellValue(colMaDV));
                if (lupMaKP.EditValue != null)
                    makho = Convert.ToInt32(lupMaKP.EditValue);
                int cgia = 0;
                cgia = cboDonGia.Items.Count;
                cboDonGia.Items.Clear();
                var gia = (from nhapduoc in _data.NhapDcts.Where(p => p.MaDV == madv)
                           join nduoc in _data.NhapDs.Where(p => p.PLoai == 2).Where(p => p.MaKP == makho && p.MaKPnx == makpx) on nhapduoc.IDNhap equals nduoc.IDNhap
                           group new { nhapduoc } by new { nhapduoc.DonGia } into kq
                           select new { kq.Key.DonGia }).ToList();
                if (gia.Count > 0)
                {
                    foreach (var g in gia)
                    {
                        cboDonGia.Items.Add(g.DonGia);
                    }
                }
            }
        }

        private void lupMaKP_EditValueChanged(object sender, EventArgs e)
        {
            int makho = 0;
            int makpx = 0;
            if (lupMaXP.EditValue != null)
                makpx = Convert.ToInt32(lupMaXP.EditValue);
            if (lupMaKP.EditValue != null)
                makho = Convert.ToInt32(lupMaKP.EditValue);
            if (TTLuu == 1 || TTLuu == 2)
            {
                var Ton = (from nd in _data.NhapDs
                           join ndct in _data.NhapDcts on nd.IDNhap equals ndct.IDNhap
                           join dv in _data.DichVus on ndct.MaDV equals dv.MaDV
                           where (nd.PLoai == 2 && nd.MaKP == makho && nd.MaKPnx == makpx || nd.PLoai == 5 && nd.MaKP == makho && nd.MaKPnx == makpx)
                           group new { ndct, dv } by new { ndct.MaDV, dv.TenDV, ndct.DonGia,ndct.DonVi}  into kq
                           select new
                           {
                               kq.Key.TenDV,
                               soluongton = kq.Sum(p => p.ndct.SoLuongX) - kq.Sum(p => p.ndct.SoLuongSD),
                               kq.Key.MaDV,
                               kq.Key.DonGia,
                               kq.Key.DonVi,
                           }).OrderBy(p => p.TenDV).ToList();
                var TonDuoc = (from t in Ton.Where(p => p.DonGia > 0) select t).ToList();

                lupMaDuoc.DataSource = TonDuoc.ToList();
            }
            else
            {
                lupMaDuoc.DataSource = _data.DichVus.Where(p => p.PLoai == 1).ToList();
            }

        }

        private void btnTDtaoSD_Click(object sender, EventArgs e)
        {
            ChucNang.frm_KKTuDong frm = new ChucNang.frm_KKTuDong(2);
            frm.FormClosed += new FormClosedEventHandler(this.usXuatDuoc_Load);
            frm.ShowDialog();
        }

        private void cboBCSD_SelectedIndexChanged(object sender, EventArgs e)
        {
            //switch (cboBCSD.SelectedIndex)
            //{
            //    case 1:
            //        FormThamSo.frmTsBcSuDungThuoc frm106 = new FormThamSo.frmTsBcSuDungThuoc();
            //        frm106.ShowDialog();
            //        break;
            //    case 2:
            //        FormThamSo.frmTsBcSuDungHoaChat frm107 = new FormThamSo.frmTsBcSuDungHoaChat();
            //        frm107.ShowDialog();
            //        break;
            //    case 3:
            //        FormThamSo.frmTsBcSuDungVTYTTieuHao frm108 = new FormThamSo.frmTsBcSuDungVTYTTieuHao();
            //        frm108.ShowDialog();
            //        break;
            //}
        }

        private void lupMaXP_EditValueChanged(object sender, EventArgs e)
        {
            lupMaKP_EditValueChanged(null, null);
        }

        private void lupMaKP_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if (TTLuu == 2 || TTLuu == 1)
            {
                if (grvNhapCT.RowCount > 0)
                {
                    if (grvNhapCT.RowCount == 1)
                    {
                        if (grvNhapCT.GetRowCellValue(1, colMaDV) != null && grvNhapCT.GetRowCellValue(1, colMaDV).ToString() != "")
                        {
                            MessageBox.Show("Chứng từ đã có thuốc, bạn không thể sửa kho");
                            e.Cancel = true;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Chứng từ  có thuốc, bạn không thể sửa kho");
                        e.Cancel = true;
                    }
                }
            }
        }

        private void lupMaXP_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if (TTLuu == 2 || TTLuu == 1)
            {
                if (grvNhapCT.RowCount > 0)
                {
                    if (grvNhapCT.RowCount == 1)
                    {
                        if (grvNhapCT.GetRowCellValue(1, colMaDV) != null && grvNhapCT.GetRowCellValue(1, colMaDV).ToString() != "")
                        {
                            MessageBox.Show("Chứng từ đã có thuốc, bạn không thể sửa đơn vị báo cáo");
                            e.Cancel = true;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Chứng từ  có thuốc, bạn không thể sửa đơn vị báo cáo");
                        e.Cancel = true;
                    }
                }
            }
        }

        private void dtNgayNhap_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void dtNgayNhap_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if (TTLuu == 2 || TTLuu == 1)
            {
                if (grvNhapCT.RowCount > 0)
                {
                    if (grvNhapCT.RowCount == 1)
                    {
                        if (grvNhapCT.GetRowCellValue(1, colMaDV) != null)
                        {
                            MessageBox.Show("Chứng từ đã có thuốc, bạn không thể sửa ngày nhập");
                            e.Cancel = true;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Chứng từ  có thuốc, bạn không thể sửa ngày nhập");
                        e.Cancel = true;
                    }
                }
            }
        }
        int i = 0;
        private void grvNhapCT_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
        }

        private void grcNhapCT_Click(object sender, EventArgs e)
        {

        }


    }
}
