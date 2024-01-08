using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QLBV.FormNhap
{
    public partial class us_SanXuatThuoc : UserControl
    {
        public us_SanXuatThuoc()
        {
            InitializeComponent();
        }
        List<NVL> _lNVL = new List<NVL>();
        int trangthai = 0;
        bool load = false;
        private void us_SanXuatThuoc_Load(object sender, EventArgs e)
        {
            lupNgayLinh.DateTime = DateTime.Now;
            lupNgayNhapTP.DateTime = DateTime.Now;
            dtHandung.DateTime = DateTime.Now;
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            List<KPhong> lkp = data.KPhongs.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.KhoaDuoc).ToList();
            lupKho.Properties.DataSource = lkp;
            lupKho.EditValue = DungChung.Bien.MaKP;
            lupKhoTK.Properties.DataSource = lkp;
            lupKhoTK.EditValue = DungChung.Bien.MaKP;

            List<DichVu> _lThuoc = (from dv in data.DichVus
                                    join tn in data.TieuNhomDVs.Where(p => p.IDNhom == 4 || p.IDNhom == 5 || p.IDNhom == 6 || p.IDNhom == 10 || p.IDNhom == 11) on dv.IdTieuNhom equals tn.IdTieuNhom
                                    select dv).OrderBy(p => p.TenDV).ToList();

            _lThuoc.Insert(0, new DichVu { MaDV = 0, TenDV = "" });
            // lupTenThuoc.Properties.DataSource = _lThuoc;
            lupTenTP.Properties.DataSource = _lThuoc;
            lupTenDV.DataSource = _lThuoc;
            lupNgaytu.DateTime = DateTime.Now;
            lupngayden.DateTime = DateTime.Now;
            TimKiemSX();
            load = true;
        }

        private void TimKiemSX()
        {

            DateTime tungay = DungChung.Ham.NgayTu(lupNgaytu.DateTime);
            DateTime denngay = DungChung.Ham.NgayDen(lupngayden.DateTime);
            int makp = 0;
            if (lupKhoTK.EditValue != null)
            {
                makp = Convert.ToInt32(lupKhoTK.EditValue);

            }
            var q = (from nd in data.NhapDs.Where(p => p.PLoai == 1 && p.KieuDon == 4).Where(p => p.MaKP == makp).Where(p => rdNgay.SelectedIndex == 0 ? (p.NgayNhap >= tungay && p.NgayNhap <= denngay) : (p.NgayNhap_NVL >= tungay && p.NgayNhap_NVL <= denngay))
                     join ndct in data.NhapDcts on nd.IDNhap equals ndct.IDNhap
                     select new { nd.IDNhap, nd.IDSXThuoc, nd.NgayNhap, nd.NgayNhap_NVL, ndct.MaDV }
                         ).OrderBy(p => rdNgay.SelectedIndex == 0 ? p.NgayNhap : p.NgayNhap_NVL).ToList();
            grc_sxThuoc.DataSource = q;

        }
        List<DungChung.Ham.giaSoLoHSD> dsgia = new List<DungChung.Ham.giaSoLoHSD>();
        double slTon = 0;
        //   double slKe = 0;
        private void lupTenThuoc_EditValueChanged(object sender, EventArgs e)
        {
            // slKe = 0;
            #region TH thêm mới NVL
            //if (trangthai == 1)
            //{
            List<double> _lgia = new List<double>();
            cboDonGia.Properties.Items.Clear();
            cboDonGia.Text = "";
            lblSoLuongTon.Text = "0";
            if (lupKho.EditValue != null)
            {
                if (lupTenThuoc.EditValue != null)
                {
                    int madv = Convert.ToInt32(lupTenThuoc.EditValue);
                    int makp = Convert.ToInt32(lupKho.EditValue);
                    dsgia = DungChung.Ham._getDSGia(data, madv, makp);
                    grc_Tonct.DataSource = dsgia;
                    if (dsgia.Count > 0)
                    {
                        if (string.IsNullOrEmpty(txtSoLuong.Text) || Convert.ToDouble(txtSoLuong.Text) == 0)
                        {
                            txtSoLuong.Text = "0";
                        }
                        #region tính tồn
                        slTon = DungChung.Bien.SoLuongTon;
                        lblSoLuongTon.Text = slTon.ToString();
                        cboDonGia.Text = dsgia.First().Gia.ToString();
                        #endregion
                    }
                    else
                    {
                        lblSoLuongTon.Text = "0";
                    }
                    txtMa.Text = madv.ToString();

                }
                else
                    lblSoLuongTon.Text = "0";

            }
            // }
            #endregion


        }

        private void txtMa_EditValueChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtMa.Text))
                lupTenThuoc.EditValue = Convert.ToInt32(txtMa.Text);
        }
        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (KT())
            {
                data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                int maDV = Convert.ToInt32(txtMa.Text);
                var qdv = data.DichVus.Where(p => p.MaDV == maDV).FirstOrDefault();
                double soluongKe = Convert.ToDouble(txtSoLuong.Text);
                if (trangthai == 2) // sửa
                {
                    _lNVL = _lNVL.Where(p => p.MaDV != maDV).ToList();
                    int madv = Convert.ToInt32(lupTenThuoc.EditValue);
                    int makp = Convert.ToInt32(lupKho.EditValue);
                    dsgia = DungChung.Ham._getDSGia(data, madv, makp);
                }
                dsgia = dsgia.Where(p => p.Gia != 0 && p.SoLuong != 0).ToList();

                foreach (QLBV.DungChung.Ham.giaSoLoHSD gia in dsgia)
                {
                    if (soluongKe <= gia.SoLuong)
                    {
                        NVL moi = new NVL();
                        moi.MaTam = qdv.MaTam;
                        moi.DonVi = qdv.DonVi;
                        moi.MaDV = maDV;
                        moi.TenDV = qdv.TenDV;
                        moi.DonGia = gia.Gia;
                        moi.SoLuong = soluongKe;
                        _lNVL.Add(moi);
                        break;
                    }
                    else
                    {
                        NVL moi = new NVL();
                        moi.MaTam = qdv.MaTam;
                        moi.DonVi = qdv.DonVi;
                        moi.MaDV = maDV;
                        moi.TenDV = qdv.TenDV;
                        moi.DonGia = gia.Gia;
                        moi.SoLuong = gia.SoLuong;
                        soluongKe = soluongKe - gia.SoLuong;
                        _lNVL.Add(moi);
                    }
                }
                grvNVL.Columns.Clear();
                grcNVL.DataSource = null;
                bindingSource1.DataSource = _lNVL;
                grcNVL.DataSource = bindingSource1;
                trangthai = 0;
                txtMa.Enabled = false;
                txtSoLuong.Enabled = false;
                cboDonGia.Enabled = false;
                lupTenThuoc.Enabled = false;
                btnLuu.Enabled = false;
                btnNew.Enabled = true;
                txtMaTP.Enabled = true;
                lupTenTP.Enabled = true;
                txtSoLuongTP.Enabled = true;
              
                lupNgayNhapTP.Enabled = true;
                dtHandung.Enabled = true;
                txtSoLo.Enabled = true;
                radChange.Enabled = true;
                txtChange.Enabled = true;


            }
        }


        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private bool KT()
        {
            DateTime ot;
            if (lupKho.EditValue == null)
            {
                MessageBox.Show("Chưa chọn kho sản xuất");
                lupKho.Focus();
                return false;
            }
            else if (string.IsNullOrEmpty(txtMa.Text))
            {
                MessageBox.Show("Chưa chọn vật liệu");
                txtMa.Focus();
                return false;
            }
            else if (string.IsNullOrEmpty(txtSoLuong.Text))
            {
                MessageBox.Show("Chưa nhập số lượng");
                txtSoLuong.Focus();
                return false;
            }
            else if (string.IsNullOrEmpty(cboDonGia.Text))
            {
                MessageBox.Show("Chưa có đơn giá");
                cboDonGia.Focus();
                return false;
            }

            else if (!DateTime.TryParse(lupNgayLinh.Text, out ot))
            {
                MessageBox.Show("Ngày lĩnh không đúng định dạng");
                lupNgayLinh.Focus();
                return false;
            }
            else
            {

                double sl = Convert.ToDouble(txtSoLuong.Text);
                if (slTon < sl || slTon == 0)
                {
                    MessageBox.Show("Số lượng tồn phải lớn hơn 0 và lớn hơn hoặc bằng số lượng kê");
                    txtSoLuong.Focus();
                    return false;
                }
                else
                {
                    int madv = Convert.ToInt32(txtMa.Text);

                    var q = (from dv in data.DichVus.Where(p => p.MaDV == madv) join tn in data.TieuNhomDVs.Where(p => p.IDNhom == 4 || p.IDNhom == 5 || p.IDNhom == 6 || p.IDNhom == 10 || p.IDNhom == 11) on dv.IdTieuNhom equals tn.IdTieuNhom select dv).ToList();
                    if (q.Count == 0)
                    {
                        MessageBox.Show("Mã thuốc chưa đúng");
                        txtMa.Focus();
                        return false;
                    }
                    else
                    {
                        if (trangthai == 1 && _lNVL.Where(p => p.MaDV == madv).Count() > 0)
                        {
                            MessageBox.Show("Mã thuốc đã có");
                            txtMa.Focus();
                            return false;
                        }
                        else
                            return true;
                    }
                }
            }


        }
        public class NVL
        {
            public int MaDV { set; get; }
            public string MaTam { set; get; }
            public string TenDV { set; get; }
            public double SoLuong { set; get; }
            public string DonVi { set; get; }
            public double DonGia { set; get; }

        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            resetNVL();

            lupNgayLinh.DateTime = DateTime.Now;
            //txtSoLuong.Text = "";
            //cboDonGia.Text = "";          
            //txtMa.Text = "";
            //lupTenThuoc.EditValue = 0;
            lupKho.Enabled = true;
            lupNgayLinh.Enabled = true;
            txtMa.Enabled = true;
            txtSoLuong.Enabled = true;
            // cboDonGia.Enabled = true;
            lupTenThuoc.Enabled = true;
            btnLuu.Enabled = true;
            trangthai = 1;
            btnLuu.Text = "Thêm";
        }

        private void grvNVL_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (trangthaiSX == 1 && grvNVL.RowCount > 0)
            {
                trangthai = 2;
                lupKho.Enabled = false;
                lupNgayLinh.Enabled = true;
                lupTenThuoc.Enabled = false;
                txtMa.Enabled = false;
                txtSoLuong.Enabled = true;
                btnLuu.Enabled = true;
                btnXoa.Enabled = true;
                btnLuu.Text = "Lưu";

                int madv = 0;
                if (grvNVL.GetRowCellValue(grvNVL.FocusedRowHandle, colMaDV) != null)
                {
                    madv = Convert.ToInt32(grvNVL.GetRowCellValue(grvNVL.FocusedRowHandle, colMaDV));
                    lupTenThuoc.EditValue = madv;
                    double soluong = 0;
                    for (int i = 0; i < grvNVL.RowCount; i++)
                    {
                        if (grvNVL.GetRowCellValue(grvNVL.FocusedRowHandle, colSoLuong) != null)
                        {
                            int dv = Convert.ToInt32(grvNVL.GetRowCellValue(i, colMaDV));
                            if (madv == dv)
                                soluong = soluong + Convert.ToDouble(grvNVL.GetRowCellValue(i, colSoLuong));
                        }
                    }
                    txtSoLuong.Text = soluong.ToString();
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (trangthaiSX == 1)
            {
                if (lupTenThuoc.EditValue != null)
                {
                    int maNVL = Convert.ToInt32(lupTenThuoc.EditValue);
                    List<NVL> xoa = _lNVL.Where(p => p.MaDV == maNVL).ToList();
                    if (xoa.Count > 0)
                    {
                        DialogResult dialogResult = MessageBox.Show("Xóa NVL: " + lupTenThuoc.Text + " ?", "Xác nhận xóa", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            foreach (NVL x in xoa)
                            {
                                _lNVL.Remove(x);
                            }
                            trangthai = 0;
                            resetNVL();
                            lupKho.Enabled = false;
                            lupNgayLinh.Enabled = false;
                            txtMa.Enabled = false;
                            txtSoLuong.Enabled = false;
                            cboDonGia.Enabled = false;
                            lupTenThuoc.Enabled = false;
                            btnLuu.Enabled = false;
                            grcNVL.DataSource = null;
                            grcNVL.DataSource = _lNVL;

                        }
                    }


                }

            }

        }


        private void txtSoLuongTP_EditValueChanged(object sender, EventArgs e)
        {
            getgia();
            //change = false;
            //txtTangTongTien.Text = tangtongtien;
            //txtTangGiaSX.Text = tanggia;
            //change = true;
        }

        private void txtMaTP_EditValueChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtMa.Text))
                lupTenThuoc.EditValue = Convert.ToInt32(txtMa.Text);
        }

        private void lupTenTP_EditValueChanged(object sender, EventArgs e)
        {

            if (lupTenTP.EditValue != null)
            {
                int madv = Convert.ToInt32(lupTenTP.EditValue);
                txtMaTP.Text = madv.ToString();
            }

        }

        private void btnGhi_Click(object sender, EventArgs e)
        {
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            if (validate())
            {
                int maDV = Convert.ToInt32(txtMaTP.Text);
                double soluong = Convert.ToDouble(txtSoLuongTP.Text);
                double dongia = Convert.ToDouble(lblDonGiaTP.Text);
                int tanggia = 0;
                int ot;
                if (int.TryParse(lblTangGiaSx.Text, out ot))
                {
                    tanggia = Convert.ToInt32(lblTangGiaSx.Text);
                }
                #region tạo phiếu xuất nguyên vật liệu
                NhapD nduoc2 = new NhapD();
                nduoc2.MaKP = Convert.ToInt32(lupKho.EditValue);
                nduoc2.PLoai = 2;
                nduoc2.KieuDon = 10;
                nduoc2.NgayNhap = lupNgayNhapTP.DateTime;
                nduoc2.NgayNhap_NVL = lupNgayLinh.DateTime;
                nduoc2.TangGiaSX = tanggia;
                nduoc2.LoaiTang = radChange.SelectedIndex;
                data.NhapDs.Add(nduoc2);
                data.SaveChanges();

                foreach (var a in _lNVL)
                {
                    NhapDct ndct2 = new NhapDct();
                    ndct2.IDNhap = nduoc2.IDNhap;
                    ndct2.MaDV = a.MaDV;
                    ndct2.DonGia = a.DonGia;
                    ndct2.DonVi = a.DonVi;
                    ndct2.SoLuongX = a.SoLuong;
                    ndct2.ThanhTienX = a.DonGia * a.SoLuong;
                    ndct2.VAT = 0;
                    data.NhapDcts.Add(ndct2);
                    data.SaveChanges();
                }
                #endregion
                #region tạo phiếu nhập thành phẩm
                NhapD nduoc = new NhapD();
                nduoc.MaKP = Convert.ToInt32(lupKho.EditValue);
                nduoc.PLoai = 1;
                nduoc.KieuDon = 4;
                nduoc.NgayNhap = lupNgayNhapTP.DateTime;
                nduoc.NgayNhap_NVL = lupNgayLinh.DateTime;
                nduoc.IDSXThuoc = nduoc2.IDNhap;
                nduoc.TangGiaSX = tanggia;
                nduoc.LoaiTang = radChange.SelectedIndex;

                data.NhapDs.Add(nduoc);
                data.SaveChanges();

                NhapDct ndct = new NhapDct();
                ndct.IDNhap = nduoc.IDNhap;
                ndct.MaDV = maDV;
                ndct.DonGia = dongia;
                var qdv = data.DichVus.Where(p => p.MaDV == maDV).FirstOrDefault();
                if (qdv != null)
                    ndct.DonVi = qdv.DonVi;
                ndct.SoLuongN = soluong;
                ndct.ThanhTienN = soluong * dongia;
                ndct.SoLo = txtSoLo.Text;
                ndct.HanDung = dtHandung.DateTime;
                ndct.VAT = 0;
                ndct.DonGiaCT = dongia;
                data.NhapDcts.Add(ndct);
                data.SaveChanges();
                #endregion
                TimKiemSX();
                btnGhi.Enabled = false;
                trangthaiSX = 0;


            }

        }

        private bool validate()
        {
            DateTime ot;
            if (lupKho.EditValue == null || Convert.ToInt32(lupKho.EditValue) <= 0)
            {
                MessageBox.Show("Chưa chọn kho sản xuất");
                lupKho.Focus();
                return false;
            }
            else if (string.IsNullOrEmpty(txtMaTP.Text))
            {
                MessageBox.Show("Chưa chọn mã thành phẩm");
                txtMaTP.Focus();
                return false;
            }
            else if (string.IsNullOrEmpty(txtSoLuongTP.Text))
            {
                MessageBox.Show("Chưa nhập số lượng thành phẩm");
                txtSoLuongTP.Focus();
                return false;
            }
           
            else if (!DateTime.TryParse(lupNgayNhapTP.Text, out ot))
            {
                MessageBox.Show("Ngày nhập thanh phẩm không đúng định dạng");
                lupNgayNhapTP.Focus();
                return false;
            }
            else if (!DateTime.TryParse(dtHandung.Text, out ot))
            {
                MessageBox.Show("Ngày nhập thanh phẩm không đúng định dạng");
                dtHandung.Focus();
                return false;
            }
            else
            {
                int madv = Convert.ToInt32(txtMaTP.Text);

                var q = (from dv in data.DichVus.Where(p => p.MaDV == madv) join tn in data.TieuNhomDVs.Where(p => p.IDNhom == 4 || p.IDNhom == 5 || p.IDNhom == 6 || p.IDNhom == 10 || p.IDNhom == 11) on dv.IdTieuNhom equals tn.IdTieuNhom select dv).ToList();
                if (q.Count == 0)
                {
                    MessageBox.Show("Mã thuốc chưa đúng");
                    txtMa.Focus();
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }


        string tangtongtien = "0";
        string tanggia = "0";
        private void getgia()
        {
            string change =  "0";
            if(!string.IsNullOrEmpty(txtChange.Text))
                change = txtChange.Text;

            if (!string.IsNullOrEmpty(txtSoLuongTP.Text) && _lNVL.Count > 0 && Convert.ToDouble(txtSoLuongTP.Text) != 0)
            {
                double thanhtienT = 0;
                double soluong = Convert.ToDouble(txtSoLuongTP.Text);
                foreach (var a in _lNVL)
                {
                    double thanhtien = a.SoLuong * a.DonGia;
                    thanhtienT = thanhtienT + thanhtien;
                }
                if (radChange.SelectedIndex == 1)// đơn giá thành phẩm
                {
                    double dongia = Convert.ToDouble(change);
                    lblTongTienTP.Text =Math.Round( (dongia * soluong),0).ToString();
                    lblTangTongTien.Text = Math.Round( ( (dongia * soluong) - thanhtienT), 0).ToString();
                    lblDonGiaTP.Text = change;
                    lblTangGiaSx.Text =(   Math.Round(  ((double)((dongia * soluong) - thanhtienT) / thanhtienT ),3)     ).ToString();
                }
                else if (radChange.SelectedIndex == 2)// tăng giá sx
                {
                    double tanggiasx = Convert.ToDouble(change);
                    double dongiaTP = Math.Round(( (double)(thanhtienT  * (100 + tanggiasx)) / (soluong * 100)), 3);
                    lblDonGiaTP.Text = Math.Round(dongiaTP, 3).ToString();
                    lblTongTienTP.Text = Math.Round( (dongiaTP * soluong),0).ToString();
                    lblTangTongTien.Text = Math.Round( ( dongiaTP * soluong - thanhtienT), 0).ToString();                  
                    lblTangGiaSx.Text = change;
                }
                else if (radChange.SelectedIndex == 0)// tăng tổng tiền
                {
                    double tangtongtien = Convert.ToDouble(change);
                    double dongiaTP =Math.Round(( (double)(thanhtienT + tangtongtien) / (soluong)),3);                  
                    lblDonGiaTP.Text = Math.Round(dongiaTP, 3).ToString();
                    lblTongTienTP.Text = Math.Round((thanhtienT + tangtongtien),0).ToString();
                    lblTangTongTien.Text =  txtChange.Text;
                    lblTangGiaSx.Text = Math.Round(((double)(tangtongtien * 100) / thanhtienT), 3).ToString();
                }
            }

            //if (!string.IsNullOrEmpty(txtSoLuongTP.Text) && _lNVL.Count > 0 && (!string.IsNullOrEmpty(txtTangTongTien.Text) && (Convert.ToDouble(txtTangTongTien.Text) != 0)))
            //{
            //    double thanhtienT = 0;
            //    double tangtongtien = Convert.ToDouble(txtTangTongTien.Text);
            //    double soluong = Convert.ToDouble(txtSoLuongTP.Text);

            //    foreach (var a in _lNVL)
            //    {
            //        double thanhtien = a.SoLuong * a.DonGia;
            //        thanhtienT = thanhtienT + thanhtien;
            //    }
            //    // thanhtienT = thanhtienT + tangtongtien;

            //    txtDonGiaTP.Text = Math.Round(((double)(thanhtienT + tangtongtien) / soluong), 3).ToString();
            //    tanggia = ((double)tangtongtien *100 / thanhtienT).ToString();
            //}


        }
        private void txtTangGiaSX_EditValueChanged(object sender, EventArgs e)
        {
            //getgia();
            //change = false;
            //txtTangTongTien.Text = tangtongtien;
            //txtTangGiaSX.Text = tanggia;
            //change = true;
        }

        private void txtDonGiaTP_TextChanged(object sender, EventArgs e)
        {
            if (trangthaiSX == 1)
                btnGhi.Enabled = true;
        }

        private void lupKhoTK_EditValueChanged(object sender, EventArgs e)
        {
            if (load)
                TimKiemSX();
        }

        private void lupNgaytu_EditValueChanged(object sender, EventArgs e)
        {
            if (load)
                TimKiemSX();
        }

        private void lupngayden_EditValueChanged(object sender, EventArgs e)
        {
            if (load)
                TimKiemSX();
        }

        int trangthaiSX = 0;// sản xuất thuốc: 0: Mặc định; 1: thêm; 2: sửa
        int _idnhap = 0;
        int _idnhapsx = 0;
        string _tenTP = "";
        private void grv_sxThuoc_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (preRow == -1)
            {
                int row = grv_sxThuoc.FocusedRowHandle;
                refocus = true;
                if (grv_sxThuoc.GetRowCellValue(row, colIDNhap) != null && grv_sxThuoc.GetRowCellValue(row, colIDNhapSX) != null)
                {
                    _idnhap = Convert.ToInt32(grv_sxThuoc.GetRowCellValue(row, colIDNhap));
                    _idnhapsx = Convert.ToInt32(grv_sxThuoc.GetRowCellValue(row, colIDNhapSX));
                    loadChiTiet(_idnhap, _idnhapsx);
                    _tenTP = lupTenTP.Text;
                    txtMa.Enabled = false;
                    txtSoLuong.Enabled = false;
                    cboDonGia.Enabled = false;
                    lupTenThuoc.Enabled = false;
                    txtMaTP.Enabled = false;
                    lupTenTP.Enabled = false;
                    txtSoLuongTP.Enabled = false;                   
                    lupNgayNhapTP.Enabled = false;
                    dtHandung.Enabled = false;
                    txtSoLo.Enabled = false;
                    radChange.Enabled = false;
                    txtChange.Enabled = false;
                    lupKho.Enabled = false;
                    lupNgayLinh.Enabled = false;
                    trangthaiSX = 0;
                }
            }
            else
            {
                refocus = false;
                grv_sxThuoc.FocusedRowHandle = preRow;
                refocus = true;
            }
        }
        bool refocus = true;//false: Không hỏi load lai; true; hoi load lại
        private void resetNVL()
        {
            txtSoLuong.Text = "";
            cboDonGia.Text = "";
            txtMa.Text = "";
            lupTenThuoc.EditValue = 0;
        }
        private void resetTP()
        {
            txtMaTP.Text = "";
            lupTenTP.EditValue = 0;
            txtSoLuongTP.Text = "";
            lblTangGiaSx.Text = "0";
            lblDonGiaTP.Text = "0";
            lblTangTongTien.Text = "0";
            lblTongTienTP.Text = "0";
            txtChange.Text = "0";
            radChange.SelectedIndex = 0;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="idnhap">id nhập thành phẩm</param>
        /// <param name="idnhapsx">id xuấtn nguyeen vật liệu</param>
        /// 

        private void loadChiTiet(int idnhap, int idnhapsx)
        {
            // load NVL
            resetNVL();
            lupNgayLinh.DateTime = DateTime.Now;
            double thanhtienNVL = 0;

            NhapD xuat = data.NhapDs.Where(p => p.IDNhap == idnhapsx).FirstOrDefault();
            if (xuat != null)
            {
                if (xuat.NgayNhap_NVL != null)
                    lupNgayLinh.DateTime = xuat.NgayNhap_NVL.Value;
                lupKho.EditValue = xuat.MaKP;

                var qNVL = (from ndct in data.NhapDcts.Where(p => p.IDNhap == idnhapsx)
                            join dv in data.DichVus on ndct.MaDV equals dv.MaDV
                            select new NVL { MaDV = ndct.MaDV ?? 0, TenDV = dv.TenDV, MaTam = dv.MaTam, DonVi = ndct.DonVi, DonGia = ndct.DonGia, SoLuong = ndct.SoLuongX}).ToList();
                bindingSource1.DataSource = qNVL;
                grcNVL.DataSource = bindingSource1;
                if(qNVL.Count>0)
                {
                    thanhtienNVL = qNVL.Sum(p => p.DonGia * p.SoLuong);
                }
            }
            // load thành phẩm
            NhapDct nhapct = data.NhapDcts.Where(p => p.IDNhap == idnhap).FirstOrDefault();
            NhapD nhap = data.NhapDs.Where(p => p.IDNhap == idnhap).FirstOrDefault();
            if (nhapct != null && nhap != null)
            {
                lupTenTP.EditValue = nhapct.MaDV;
                txtSoLuongTP.Text = nhapct.SoLuongN.ToString();
                lblTangGiaSx.Text = (nhap.TangGiaSX ?? 0).ToString();
                lblDonGiaTP.Text = nhapct.DonGia.ToString();
                lupNgayNhapTP.EditValue = nhap.NgayNhap.Value;
                if (nhapct.HanDung != null)
                    dtHandung.EditValue = nhapct.HanDung.Value;
                txtSoLo.Text = nhapct.SoLo;
                double thanhtienN = data.NhapDcts.Where(p => p.IDNhap == idnhap).Sum(p=>p.ThanhTienN);
                string tangtongtien = Math.Round(thanhtienN - thanhtienNVL, 0).ToString();
                lblTongTienTP.Text = Math.Round(thanhtienN, 0).ToString();
                lblTangTongTien.Text = tangtongtien;
                if(nhap.LoaiTang == 0)// tăng tổng tiền
                {
                   
                    radChange.SelectedIndex = 0;
                    txtChange.Text = tangtongtien;
                }
                else if(nhap.LoaiTang == 1) // nhập thẳng giá thành phẩm
                {
                    radChange.SelectedIndex = 1;
                    txtChange.Text = nhapct.DonGia.ToString();
                   
                }
                else// tăng giá sx
                {
                    radChange.SelectedIndex = 2;
                    txtChange.Text = nhap.TangGiaSX == null ? "0" : nhap.TangGiaSX.ToString();
                   
                }
            }
            btnGhi.Enabled = false;
            btnNew.Enabled = false;
            btnLuu.Enabled = false;
            btnXoa.Enabled = false;
        }
        //  bool taomoiSX = true;
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            preRow = -1;
            lupKho.EditValue = DungChung.Bien.MaKP;
            bindingSource1.DataSource = null;
            grcNVL.DataSource = null;
            btnNew_Click(null, null);
            _idnhap = 0;
            _idnhapsx = 0;
            resetTP();
            lupNgayNhapTP.DateTime = DateTime.Now;
            lupNgayLinh.DateTime = DateTime.Now;
            dtHandung.DateTime = DateTime.Now;
            trangthaiSX = 1;
            _lNVL.Clear();
        }

        private void rdNgay_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void lupKho_EditValueChanged(object sender, EventArgs e)
        {
            if (lupKho.EditValue != null)
            {
                int makp = Convert.ToInt32(lupKho.EditValue);
                if (makp > 0)
                {
                    data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                    List<DichVu> _lThuoc = (from nd in data.NhapDs.Where(p => p.PLoai == 1).Where(p => p.MaKP == makp)
                                            join ndct in data.NhapDcts on nd.IDNhap equals ndct.IDNhap
                                            join dv in data.DichVus on ndct.MaDV equals dv.MaDV
                                            join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                                            select dv).Distinct().ToList();

                    _lThuoc.Insert(0, new DichVu { MaDV = 0, TenDV = "" });
                    lupTenThuoc.Properties.DataSource = _lThuoc;
                }
            }
        }



        private void grvNVL_DataSourceChanged(object sender, EventArgs e)
        {
            grvNVL_FocusedRowChanged(null, null);
        }

        private void txtMa_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void txtMaTP_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void txtSoLuong_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar) || e.KeyChar == '.')
                e.Handled = true;
        }

        private void cboDonGia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar) || e.KeyChar == '.')
                e.Handled = true;
        }

        private void txtSoLuongTP_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar) || e.KeyChar == '.')
                e.Handled = true;
        }

        private void txtDonGiaTP_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if ((!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar) ) || e.KeyChar == '.')
            //    e.Handled = true;
        }

        private void lupNgayNhapTP_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtTangGiaSX_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsControl(e.KeyChar) || char.IsDigit(e.KeyChar) || e.KeyChar == '.'))
            {
                e.Handled = true;
            }
        }

        private void rdNgay_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (load)
                TimKiemSX();
        }

        private void btnXoaSX_Click(object sender, EventArgs e)
        {
            if (_idnhap > 0 && _idnhapsx > 0)
            {

                DialogResult dialogResult = MessageBox.Show("Xóa thông tin sản xuất thuốc: " + _tenTP + " ?", "Xác nhận xóa", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    var qndct = data.NhapDcts.Where(p => p.IDNhap == _idnhap || p.IDNhap == _idnhapsx).ToList();
                    foreach (NhapDct ct in qndct)
                    {
                        data.NhapDcts.Remove(ct);
                    }
                    data.SaveChanges();

                    var qnd = data.NhapDs.Where(p => p.IDNhap == _idnhap || p.IDNhap == _idnhapsx).ToList();
                    foreach (NhapD ct in qnd)
                    {
                        data.NhapDs.Remove(ct);
                    }
                    data.SaveChanges();
                }
            }
            TimKiemSX();
        }

        private void grv_sxThuoc_DataSourceChanged(object sender, EventArgs e)
        {
            if (grv_sxThuoc.RowCount == 1)
                grv_sxThuoc.FocusedRowHandle = 0;
        }

        private void grv_sxThuoc_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (grv_sxThuoc.RowCount == 1)
                grv_sxThuoc.FocusedRowHandle = 0;
        }

        int preRow = -1;
        private void grv_sxThuoc_BeforeLeaveRow(object sender, DevExpress.XtraGrid.Views.Base.RowAllowEventArgs e)
        {

            if ((trangthaiSX == 1 || trangthaiSX == 2) && refocus)
            {
                DialogResult dialogResult = MessageBox.Show("Dữ liệu chưa được lưu, bạn có chắc chắn muốn xem đơn khác không ?", "Xác nhận", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.No)
                {
                    preRow = grv_sxThuoc.FocusedRowHandle;
                }
                else
                {
                    preRow = -1;
                }
            }

        }

        private void btnSuaSX_Click(object sender, EventArgs e)
        {
            preRow = -1;
            trangthaiSX = 2;
        }
        static double tonthuoc = 0;
        private void grvNVL_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {

        }

        private void btnSua_Click(object sender, EventArgs e)
        {

        }

        private void txtSoLuong_TextChanged(object sender, EventArgs e)
        {

        }
        private void grvNVL_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            grvNVL_FocusedRowChanged(null, null);
        }

        private void txtTangTongTien_EditValueChanged(object sender, EventArgs e)
        {
            // getgia();

        }

        private void txtChange_TextChanged(object sender, EventArgs e)
        {
            getgia();
        }

        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //getgia();
            if(radChange.SelectedIndex == 0 || radChange.SelectedIndex == 2)
            {
                txtChange.Text = "0";
            }
            else if (radChange.SelectedIndex == 1)
            {


                if (!string.IsNullOrEmpty(txtSoLuongTP.Text) && _lNVL.Count > 0 && Convert.ToDouble(txtSoLuongTP.Text) != 0)
                {
                    double thanhtienT = 0;
                    double soluong = Convert.ToDouble(txtSoLuongTP.Text);
                    foreach (var a in _lNVL)
                    {
                        double thanhtien = a.SoLuong * a.DonGia;
                        thanhtienT = thanhtienT + thanhtien;
                    }
                    txtChange.Text = (Math.Round((double)thanhtienT / soluong, 3)).ToString(); ;
                        
                }
            }
        }

        private void txtSoLuongTP_TextChanged(object sender, EventArgs e)
        {
            if (trangthaiSX == 1)
                btnGhi.Enabled = true;
        }
    }
}
