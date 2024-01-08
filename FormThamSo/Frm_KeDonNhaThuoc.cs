using System;
using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraEditors.DXErrorProvider;
using DevExpress.XtraEditors.Controls;
using QLBV.DungChung;
using DevExpress.XtraGrid.Views.Base;

namespace QLBV.FormNhap
{
    public partial class Frm_KeDonNhaThuoc : DevExpress.XtraEditors.XtraUserControl
    {
        public class MyObject
        {
            public int Value { set; get; }
            public string Text { set; get; }
        }
        public Frm_KeDonNhaThuoc()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities _Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        List<BenhNhan> _BenhNhan = new List<BenhNhan>();
        List<VienPhict> _VienPhict = new List<VienPhict>();
        List<CP> _DMChiPhi = new List<CP>();

        List<DichVu> _lDichVu = new List<DichVu>();
        List<KPhong> _lkp = new List<KPhong>();
        List<KPhong> lquayThuoc = new List<KPhong>();
        int Trangthai = -1;
        bool load = false;
        int _idDTBN = -1;

        private void Frm_KeDonNhaThuoc_Load(object sender, EventArgs e)
        {

            _Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var qDTBN = _Data.DTBNs.Where(p => p.DTBN1.ToLower() == "dịch vụ").FirstOrDefault();
            if (qDTBN != null)
                _idDTBN = qDTBN.IDDTBN;

            _lbv = (from bv in _Data.BenhViens select bv).ToList();
            UNREADONLY(true);
            _lDichVu = _Data.DichVus.Where(p => p.Status == 1).OrderBy(p => p.TenDV).ToList();
            txtNgayden.DateTime = System.DateTime.Now;
            txtNgaytu.DateTime = System.DateTime.Now;

            // List<int> makp = (from kp in _Data.KPhongs.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.KhoaDuoc) select kp.MaKP).ToList();
            lupCB.Properties.DataSource = _Data.CanBoes.OrderBy(p => p.TenCB).ToList();// thiếu điều kiện khoa phòng sử dụng phải có quầy thuốc được chọn
            lupCB.EditValue = DungChung.Bien.MaCB;
            lquayThuoc = _Data.KPhongs.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.KhoaDuoc && (DungChung.Bien.MaBV != "24272" ? p.ChuyenKhoa == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NhaThuoc : true)).ToList();
            lupKP.Properties.DataSource = lquayThuoc;
            lup_TK_QuayThuoc.Properties.DataSource = lquayThuoc;
            if (lquayThuoc.Count == 1)
            {
                lupKP.ItemIndex = 0;
                lup_TK_QuayThuoc.ItemIndex = 0;
            }
            else
            {
                lupKP.EditValue = Bien.MaKho > 0 ? Bien.MaKho : Bien.MaKP;
                lup_TK_QuayThuoc.EditValue = Bien.MaKho > 0 ? Bien.MaKho : Bien.MaKP;
            }
            _lGiaUT = _Data.GiaUTs.ToList();
            // QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            List<MyObject> lMyObject = new List<MyObject>();
            lMyObject.Add(new MyObject { Text = "Tất cả", Value = 0 });
            lMyObject.Add(new MyObject { Text = "Chưa thanh toán", Value = 1 });
            lMyObject.Add(new MyObject { Text = "Đã thanh toán", Value = 2 });
            lMyObject.Add(new MyObject { Text = "Đã xuất dược", Value = 3 });
            lupTrangThai.Properties.DataSource = lMyObject;
            lupTrangThai.EditValue = 1;

            // EnableControl(true);
            SBTLuu.Enabled = false;
            SBTXoa.Enabled = false;
            SBTHuy.Enabled = false;
            SBTSua.Enabled = false;
            SBTThemmoi.Enabled = true;
            GrvChiphi.OptionsBehavior.ReadOnly = true;
            GrcBenhNhan.Enabled = true;

            load = true;
            DSBN();
        }
        private void DSBN()
        {
            if (load)
            {
                DateTime _Ngaytu = DungChung.Ham.NgayTu(txtNgaytu.DateTime);
                DateTime _Ngayden = DungChung.Ham.NgayDen(txtNgayden.DateTime);
                string TENBN = "";
                if (!string.IsNullOrEmpty(txtTenBN.Text))
                {
                    TENBN = txtTenBN.Text.ToLower();
                }
                int status = -1;
                if (lupTrangThai.EditValue != null)
                    status = Convert.ToInt32(lupTrangThai.EditValue);

                // List<int> lkp = (from kp in _Data.KPhongs.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.QuayThuoc) select kp.MaKP).ToList();
                int makp = 0;
                if (lup_TK_QuayThuoc.EditValue != null)
                    makp = Convert.ToInt32(lup_TK_QuayThuoc.EditValue);
                var DSBN = (from bn in _Data.BenhNhans.Where(p => p.TuyenDuoi == 3)
                            join nd in _Data.NhapDs on bn.MaBNhan equals nd.MaBNhan into kq
                            from kq1 in kq.DefaultIfEmpty()
                                //status = 0: tất cả , =1: chưa thanh toán; =2: đã thanh toán; =3: đã xuất dược và ra viện
                            where (status == 0 ? true : (status == 1 ? (bn.Status != 2 && bn.Status != 3) : (status == 2 ? ((bn.Status == 3 || bn.Status == 2) && kq1 == null) : (status == 3 ? kq1 != null : false))))
                            where (bn.NNhap >= _Ngaytu && bn.NNhap <= _Ngayden)
                            where bn.MaKP == makp
                            where bn.NoiTru == 0 && bn.DTNT == false && bn.DTuong == "Dịch vụ"
                            where (bn.TenBNhan.ToLower().Contains(TENBN))
                            select bn).OrderBy(p => p.MaBNhan).ToList();


                GrcBenhNhan.DataSource = "";
                GrcBenhNhan.DataSource = DSBN.OrderBy(p => p.NNhap).ToList();

                //if (status == 0 || status == 1)// chưa thanh toán
                //{
                //    btnThanhToan.Enabled = true;
                //    btnXoaTT.Enabled = false;
                //    btnPhieuThu.Enabled = false;
                //    btnIn.Enabled = false;
                //    SBTXoa.Enabled = true;
                //    SBTSua.Enabled = true;


                //}
                //else if (status == 2)// đã thanh toán, chưa xuất dược
                //{
                //    btnThanhToan.Enabled = false;
                //    btnXoaTT.Enabled = true;
                //    btnPhieuThu.Enabled = true;
                //    btnIn.Enabled = true;
                //    SBTXoa.Enabled = false;
                //    SBTSua.Enabled = false;
                //}
                //else if (status == 3)// đã xuất dược
                //{
                //    btnThanhToan.Enabled = false;
                //    btnXoaTT.Enabled = false;
                //    btnPhieuThu.Enabled = true;
                //    btnIn.Enabled = false;
                //    SBTXoa.Enabled = false;
                //    SBTSua.Enabled = false;
                //}

                //if (GrvBenhNhan.FocusedRowHandle < 0)
                //{
                //    btnThanhToan.Enabled = false;
                //    btnXoaTT.Enabled = false;
                //    btnPhieuThu.Enabled = false;
                //    btnIn.Enabled = false;
                //    SBTXoa.Enabled = false;
                //    SBTSua.Enabled = false;
                //}
            }
        }
        private void Xoatrang()//xoá trắng thông tin hành chính BN
        {
            LupNgayNhap.Text = "";
            txtNhapTBN.Text = "";
            radNamNu.SelectedIndex = -1;
            txtNgaySinh.Text = "";
            txtThangSinh.Text = "";
            txtNamSinh.Text = "";
            txtTuoi.Text = "";
            txtDiaChi.Text = "";

            if (lquayThuoc.Count == 1)
            {
                lupKP.ItemIndex = 0;
                lup_TK_QuayThuoc.ItemIndex = 0;
            }
            else
            {
                lupKP.EditValue = Bien.MaKho > 0 ? Bien.MaKho : Bien.MaKP;
                //lup_TK_QuayThuoc.EditValue = Bien.MaKho > 0 ? Bien.MaKho : Bien.MaKP;
            }
            GrcChiphi.DataSource = null;

        }
        private void UNREADONLY(bool T)
        {
            LupNgayNhap.Properties.ReadOnly = T;
            txtNhapTBN.Properties.ReadOnly = T;
            radNamNu.Properties.ReadOnly = T;
            txtNgaySinh.Properties.ReadOnly = T;
            txtThangSinh.Properties.ReadOnly = T;
            txtNamSinh.Properties.ReadOnly = T;
            txtTuoi.Properties.ReadOnly = T;
            txtDiaChi.Properties.ReadOnly = T;
            lupCB.Properties.ReadOnly = T;
            lupKP.Properties.ReadOnly = T;
            DonGia.OptionsColumn.AllowEdit = !T;



        }
        private void EnableControl(bool t)
        {

            SBTLuu.Enabled = !t;
            SBTXoa.Enabled = t;
            SBTHuy.Enabled = !t;
            SBTSua.Enabled = t;
            SBTThemmoi.Enabled = t;
            GrvChiphi.OptionsBehavior.ReadOnly = t;
            GrcBenhNhan.Enabled = t;

        }

        private bool KTTTHC()// kiểm tra thông tin hành chính
        {
            if (string.IsNullOrEmpty(LupNgayNhap.Text))
            {
                MessageBox.Show("Bạn chưa chọn ngày!");
                LupNgayNhap.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtNhapTBN.Text))
            {
                MessageBox.Show("Bạn chưa nhập tên bệnh nhân!");
                txtNhapTBN.Focus();
                return false;
            }
            if (radNamNu.SelectedIndex == -1)
            {
                MessageBox.Show("Chưa nhập giới tính");
                radNamNu.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtTuoi.Text))
            {
                MessageBox.Show("Bạn chưa nhập Tuổi!");
                txtNamSinh.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(lupCB.Text))
            {
                MessageBox.Show("Bạn chưa nhập tên cán bộ!");
                lupCB.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(lupKP.Text))
            {
                MessageBox.Show("Bạn chưa chọn quầy thuốc!");
                lupKP.Focus();
                return false;
            }

            return true;



        }

        private void txtNgaytu_EditValueChanged(object sender, EventArgs e)
        {
            DSBN();
        }

        private void txtNgayden_EditValueChanged(object sender, EventArgs e)
        {
            DSBN();
        }

        private void txtTenBN_EditValueChanged(object sender, EventArgs e)
        {
            DSBN();
        }
        List<GiaUT> _lGiaUT = new List<GiaUT>();
        private void GrvBenhNhan_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (GrvBenhNhan.GetFocusedRowCellValue(MaBN) != null)
            {
                Xoatrang();
                int _Mabn = Convert.ToInt32(GrvBenhNhan.GetFocusedRowCellValue(MaBN));
                txtMaBNhan.Text = _Mabn.ToString();
                txtTenBenhNhan.Text = GrvBenhNhan.GetFocusedRowCellDisplayText(TenBN).ToString();
                var DSBN = (from bn in _Data.BenhNhans.Where(p => p.MaBNhan == _Mabn)
                            select bn).ToList();
                if (DSBN.Count > 0)
                {
                    LupNgayNhap.DateTime = DSBN.First().NNhap.Value;
                    txtNhapTBN.Text = DSBN.First().TenBNhan;
                    txtNgaySinh.Text = DSBN.First().NgaySinh ?? "" ;
                    txtThangSinh.Text = DSBN.First().ThangSinh ?? "";
                    txtNamSinh.Text = DSBN.First().NamSinh;
                    if (DSBN.First().GTinh == 0)
                    {
                        radNamNu.SelectedIndex = 1;
                    }
                    else
                    {
                        radNamNu.SelectedIndex = 0;
                    }

                    txtDiaChi.Text = DSBN.First().DChi;

                    lupKP.EditValue = DSBN.First().MaKP;

                    lupCB.EditValue = DSBN.First().MaCB;

                }
                _DMChiPhi.Clear();
                //lupTenchiphi.DataSource = "";
                //lupTenchiphi.DataSource = _lDichVu;
                // var dt1 = _Data.VienPhis.Where(p => p.MaBNhan == _Mabn).ToList();
                var VPCT = (from dt in _Data.DThuocs.Where(p => p.MaBNhan == _Mabn)
                            join dtct in _Data.DThuoccts on dt.IDDon equals dtct.IDDon
                            join DV in _Data.DichVus on dtct.MaDV equals DV.MaDV
                            select new CP { MaDV = DV.MaDV, DonVi = dtct.DonVi, Gia = dtct.DonGia, SoLuong = dtct.SoLuong, ThanhTien = dtct.ThanhTien, GhiChu = dtct.GhiChu, IDDonct = dtct.IDDonct }).ToList();
                Bds.DataSource = VPCT.OrderBy(p => p.IDDonct).ToList();
                GrcChiphi.DataSource = Bds;

                var qvp = _Data.VienPhis.Where(p => p.MaBNhan == _Mabn).FirstOrDefault();
                var qnd = _Data.NhapDs.Where(p => p.MaBNhan == _Mabn).FirstOrDefault();

                #region
                //if (status == 0 || status == 1)// chưa thanh toán
                //{
                //    btnThanhToan.Enabled = true;
                //    btnXoaTT.Enabled = false;
                //    btnPhieuThu.Enabled = false;
                //    btnIn.Enabled = false;
                //    SBTXoa.Enabled = true;
                //    SBTSua.Enabled = true;


                //}
                //else if (status == 2)// đã thanh toán, chưa xuất dược
                //{
                //    btnThanhToan.Enabled = false;
                //    btnXoaTT.Enabled = true;
                //    btnPhieuThu.Enabled = true;
                //    btnIn.Enabled = true;
                //    SBTXoa.Enabled = false;
                //    SBTSua.Enabled = false;
                //}
                //else if (status == 3)// đã xuất dược
                //{
                //    btnThanhToan.Enabled = false;
                //    btnXoaTT.Enabled = false;
                //    btnPhieuThu.Enabled = true;
                //    btnIn.Enabled = false;
                //    SBTXoa.Enabled = false;
                //    SBTSua.Enabled = false;
                //}

                //if (GrvBenhNhan.FocusedRowHandle < 0)
                //{
                //    btnThanhToan.Enabled = false;
                //    btnXoaTT.Enabled = false;
                //    btnPhieuThu.Enabled = false;
                //    btnIn.Enabled = false;
                //    SBTXoa.Enabled = false;
                //    SBTSua.Enabled = false;
                //}
                #endregion
                if (qvp != null)
                {
                    SBTLuu.Enabled = false;
                    SBTXoa.Enabled = false;
                    SBTHuy.Enabled = false;
                    SBTSua.Enabled = false;
                    SBTThemmoi.Enabled = true;
                    GrvChiphi.OptionsBehavior.ReadOnly = true;
                    GrcBenhNhan.Enabled = true;
                    btnThanhToan.Enabled = false;
                    //btnPhieuThu.Enabled = true;
                    //btnIn.Enabled = false;
                    if (qnd != null)
                    {
                        btnXoaTT.Enabled = false;
                        btnIn.Enabled = true;
                        btnPhieuThu.Enabled = true;
                    }
                    else
                    {
                        btnXoaTT.Enabled = true;
                        btnIn.Enabled = true;
                        btnPhieuThu.Enabled = true;
                    }
                }
                else if (VPCT.Count > 0)
                {
                    EnableControl(true);
                    btnThanhToan.Enabled = true;
                    btnXoaTT.Enabled = false;
                    btnIn.Enabled = false;
                    btnPhieuThu.Enabled = false;
                }
            }
            else
            {
                SBTLuu.Enabled = false;
                SBTXoa.Enabled = false;
                SBTHuy.Enabled = false;
                SBTSua.Enabled = false;
                SBTThemmoi.Enabled = true;
                GrvChiphi.OptionsBehavior.ReadOnly = true;
                GrcBenhNhan.Enabled = true;
                txtTenBenhNhan.Text = "";
                txtMaBNhan.Text = "";
                btnThanhToan.Enabled = false;
                btnXoaTT.Enabled = false;
                btnIn.Enabled = false;
                btnPhieuThu.Enabled = false;
                Xoatrang();
            }
        }

        private void SBTThemmoi_Click(object sender, EventArgs e)
        {
            Xoatrang();
            LupNgayNhap.DateTime = System.DateTime.Now;

            UNREADONLY(false);
            _DMChiPhi.Clear();


            Trangthai = 0;
            Bds.DataSource = _DMChiPhi.ToList();
            GrcChiphi.DataSource = Bds;
            //lupTenchiphi.DataSource = "";
            //lupTenchiphi.DataSource = _lDichVu;

            EnableControl(false);
            LupNgayNhap.Focus();
            if (lquayThuoc.Count == 1)
            {
                lupKP.ItemIndex = 0;
                lup_TK_QuayThuoc.ItemIndex = 0;
            }
            else
            {
                lupKP.EditValue = Bien.MaKho > 0 ? Bien.MaKho : Bien.MaKP;
                lup_TK_QuayThuoc.EditValue = Bien.MaKho > 0 ? Bien.MaKho : Bien.MaKP;
            }

        }


        private void SBTLuu_Click(object sender, EventArgs e)
        {
            _DMChiPhi.Clear();
            //MessageBox.Show(GrvChiphi.RowCount.ToString());


            if (KTTTHC())
            {
                if(GrvChiphi.RowCount > 0 && GrvChiphi.GetRowCellValue(0, TenCP) != null && Convert.ToInt32(GrvChiphi.GetRowCellValue(0, TenCP)) > 0)
                {

                    if (Trangthai == 0)// TT=0 là thêm mới, TT=1 là sửa
                    {
                        #region thêm , sửa bảng bệnh nhân
                        //thêm mới bảng bệnh nhân
                        BenhNhan themmoi = new BenhNhan();
                        themmoi.NNhap = LupNgayNhap.DateTime;
                        themmoi.TenBNhan = txtNhapTBN.Text;
                        themmoi.IDDTBN = Convert.ToByte(_idDTBN);
                        themmoi.TuyenDuoi = 3;
                        if (radNamNu.SelectedIndex == 0)
                            themmoi.GTinh = 1;
                        else if (radNamNu.SelectedIndex == 1)
                            themmoi.GTinh = 0;
                        if (txtNgaySinh.Text != null)
                        {
                            themmoi.NgaySinh = txtNgaySinh.Text.Length == 1 ? "0" + txtNgaySinh.Text : txtNgaySinh.Text;
                        }
                        if (txtThangSinh.Text != null)
                        {
                            themmoi.ThangSinh = txtThangSinh.Text.Length == 1 ? "0" + txtThangSinh.Text : txtThangSinh.Text;
                        }
                        themmoi.SoTT = 0;
                        if (!string.IsNullOrEmpty(txtNamSinh.Text))
                            themmoi.NamSinh = txtNamSinh.Text;
                        if (!string.IsNullOrEmpty(txtTuoi.Text))
                            themmoi.Tuoi = Convert.ToInt32(txtTuoi.Text);
                        themmoi.DChi = txtDiaChi.Text;
                        themmoi.DTNT = false;
                        themmoi.NoiTru = 0;
                        themmoi.DTuong = "Dịch vụ";
                        themmoi.MaCB = lupCB.EditValue.ToString();
                        themmoi.MaKCB = DungChung.Bien.MaBV;
                        themmoi.SoDK = 0;

                        if (lupKP.EditValue != null)
                            themmoi.MaKP = Convert.ToInt32(lupKP.EditValue);
                        themmoi.Status = 1;// ra đã thanh toán ?


                        _Data.BenhNhans.Add(themmoi);
                        _Data.SaveChanges();


                        #endregion end thêm mới bảng bệnh nhân

                        #region//thêm mới đơn thuốc
                        DThuoc themmoi3 = new DThuoc();
                        themmoi3.MaBNhan = themmoi.MaBNhan;
                        themmoi3.NgayKe = LupNgayNhap.DateTime;
                        themmoi3.KieuDon = 6;
                        themmoi3.PLDV = 1;
                        if (lupKP.EditValue != null)
                        {
                            themmoi3.MaKP = Convert.ToInt32(lupKP.EditValue);
                            themmoi3.MaKXuat = Convert.ToInt32(lupKP.EditValue);
                        }

                        _Data.DThuocs.Add(themmoi3);
                        _Data.SaveChanges();
                        #endregion // end thêm mới dthuoc
                        #region // thêm mới dthuocct
                        for (int i = 0; i < GrvChiphi.RowCount; i++)
                        {
                            if (GrvChiphi.GetRowCellValue(i, MaDV) != null && GrvChiphi.GetRowCellValue(i, MaDV).ToString() != "")
                            {
                                if (GrvChiphi.GetRowCellValue(i, SoLuong) != null && GrvChiphi.GetRowCellValue(i, SoLuong).ToString() != "" && GrvChiphi.GetRowCellValue(i, DonGia) != null && GrvChiphi.GetRowCellValue(i, DonGia).ToString() != "")
                                {
                                    DThuocct themmoi4 = new DThuocct();

                                    if (lupKP.EditValue != null)
                                    {
                                        themmoi4.MaKP = Convert.ToInt32(lupKP.EditValue);
                                        themmoi4.MaKXuat = Convert.ToInt32(lupKP.EditValue);
                                    }
                                    themmoi4.Status = 0;
                                    themmoi4.IDDon = themmoi3.IDDon;
                                    themmoi4.MaDV = GrvChiphi.GetRowCellValue(i, MaDV) == null ? 0 : Convert.ToInt32(GrvChiphi.GetRowCellValue(i, MaDV));
                                    themmoi4.DonVi = GrvChiphi.GetRowCellValue(i, DonVi).ToString();
                                    themmoi4.DonGia = Convert.ToDouble(GrvChiphi.GetRowCellValue(i, DonGia).ToString());
                                    themmoi4.SoLuong = Convert.ToDouble(GrvChiphi.GetRowCellValue(i, SoLuong).ToString());
                                    themmoi4.ThanhTien = Convert.ToDouble(GrvChiphi.GetRowCellValue(i, ThanhTien).ToString());
                                    if (GrvChiphi.GetRowCellValue(i, colGhiChu) != null)
                                        themmoi4.GhiChu = GrvChiphi.GetRowCellValue(i, colGhiChu).ToString();
                                    _Data.DThuoccts.Add(themmoi4);
                                    _Data.SaveChanges();
                                }
                            }
                        }


                        DSBN();
                        Trangthai = -1;
                        UNREADONLY(true);
                        EnableControl(true);

                        #endregion end thêm mới VienPhict
                        #region//thêm mới nhập dược
                        //NhapD nd = new NhapD();
                        //nd.MaBNhan = themmoi.MaBNhan;
                        //nd.NgayNhap = LupNgayNhap.DateTime;
                        //nd.PLoai = 2;
                        //nd.KieuDon = 12;
                        //if (lupKP.EditValue != null)
                        //{
                        //    nd.MaKP = Convert.ToInt32(lupKP.EditValue);

                        //}

                        //nd.MaCB = DungChung.Bien.MaCB;
                        //_Data.NhapDs.Add(nd);
                        //_Data.SaveChanges();
                        #endregion // end thêm mới nhập dược
                        #region // thêm mới nhập dược chi tiết
                        //for (int i = 0; i < GrvChiphi.RowCount; i++)
                        //{
                        //    if (GrvChiphi.GetRowCellValue(i, MaDV) != null && GrvChiphi.GetRowCellValue(i, MaDV).ToString() != "")
                        //    {
                        //        if (GrvChiphi.GetRowCellValue(i, SoLuong) != null && GrvChiphi.GetRowCellValue(i, SoLuong).ToString() != "" && GrvChiphi.GetRowCellValue(i, DonGia) != null && GrvChiphi.GetRowCellValue(i, DonGia).ToString() != "")
                        //        {
                        //            NhapDct themmoi4 = new NhapDct();


                        //            themmoi4.IDNhap = nd.IDNhap;
                        //            themmoi4.MaDV = GrvChiphi.GetRowCellValue(i, MaDV) == null ? 0 : Convert.ToInt32(GrvChiphi.GetRowCellValue(i, MaDV));
                        //            themmoi4.DonVi = GrvChiphi.GetRowCellValue(i, DonVi).ToString();
                        //            themmoi4.DonGia = Convert.ToDouble(GrvChiphi.GetRowCellValue(i, DonGia).ToString());
                        //            themmoi4.SoLuongX = Convert.ToDouble(GrvChiphi.GetRowCellValue(i, SoLuong).ToString());
                        //            themmoi4.ThanhTienX = Convert.ToDouble(GrvChiphi.GetRowCellValue(i, ThanhTien).ToString());

                        //            _Data.NhapDcts.Add(themmoi4);
                        //            _Data.SaveChanges();
                        //        }
                        //    }
                        //}


                        DSBN();
                        Trangthai = -1;
                        UNREADONLY(true);
                        EnableControl(true);

                        #endregion end thêm mới VienPhict
                        #region//thêm mới viện phí
                        //VienPhi themmoivp = new VienPhi();
                        //themmoivp.MaBNhan = themmoi.MaBNhan;
                        //themmoivp.NgayTT = LupNgayNhap.DateTime.AddMinutes(15);
                        //themmoivp.NgayDuyetCP = LupNgayNhap.DateTime.AddMinutes(15);
                        //themmoivp.NgayRa = LupNgayNhap.DateTime.AddMinutes(10);
                        //if (lupKP.EditValue != null)
                        //    themmoivp.MaKP = Convert.ToInt32(lupKP.EditValue);
                        //_Data.VienPhis.Add(themmoivp);
                        //_Data.SaveChanges();
                        #endregion // end thêm mới Vien phi
                        #region // thêm mới VienPhict
                        //for (int i = 0; i < GrvChiphi.RowCount; i++)
                        //{
                        //    if (GrvChiphi.GetRowCellValue(i, MaDV) != null && GrvChiphi.GetRowCellValue(i, MaDV).ToString() != "")
                        //    {
                        //        if (GrvChiphi.GetRowCellValue(i, SoLuong) != null && GrvChiphi.GetRowCellValue(i, SoLuong).ToString() != "" && GrvChiphi.GetRowCellValue(i, DonGia) != null && GrvChiphi.GetRowCellValue(i, DonGia).ToString() != "")
                        //        {
                        //            VienPhict themmoi4 = new VienPhict();

                        //            if (lupKP.EditValue != null)
                        //                themmoi4.MaKP = Convert.ToInt32(lupKP.EditValue);
                        //            themmoi4.idVPhi = themmoivp.idVPhi;
                        //            themmoi4.MaDV = GrvChiphi.GetRowCellValue(i, MaDV) == null ? 0 : Convert.ToInt32(GrvChiphi.GetRowCellValue(i, MaDV));
                        //            themmoi4.DonVi = GrvChiphi.GetRowCellValue(i, DonVi).ToString();
                        //            themmoi4.DonGia = Convert.ToDouble(GrvChiphi.GetRowCellValue(i, DonGia).ToString());
                        //            themmoi4.SoLuong = Convert.ToDouble(GrvChiphi.GetRowCellValue(i, SoLuong).ToString());
                        //            themmoi4.ThanhTien = Convert.ToDouble(GrvChiphi.GetRowCellValue(i, ThanhTien).ToString());
                        //            themmoi4.Duyet = 0;
                        //            themmoi4.NgayChiPhi = LupNgayNhap.DateTime.AddMinutes(15);

                        //            _Data.VienPhicts.Add(themmoi4);
                        //            _Data.SaveChanges();
                        //        }
                        //    }
                        //}
                        #endregion end thêm mới VienPhict

                        MessageBox.Show("Thêm mới thành công");
                        DSBN();
                        //Trangthai = -1;
                        //UNREADONLY(true);
                        //EnableControl(true);


                    }
                    else
                    {
                        int _MaBN = 0;

                        if (GrvBenhNhan.GetFocusedRowCellValue(MaBN) != null)
                        {
                            _MaBN = Convert.ToInt32(GrvBenhNhan.GetFocusedRowCellValue(MaBN));
                            var qbn = _Data.BenhNhans.Single(p => p.MaBNhan == _MaBN);
                            var qnd = _Data.NhapDs.Where(p => p.MaBNhan == _MaBN).ToList();
                            if (qnd.Count > 0)
                            {
                                MessageBox.Show("Bệnh nhân đã được xuất dược, bạn không thể sửa");
                                return;
                            }
                            else if (qbn.Status == 2 || qbn.Status == 3)
                            {
                                MessageBox.Show("Bệnh nhân đã được thanh toán, bạn không thể sửa");
                                return;
                            }
                            #region sửa thông tin hành chính bệnh nhân
                            if (GrvBenhNhan.GetFocusedRowCellValue(MaBN) != null)
                            {
                                _MaBN = Convert.ToInt32(GrvBenhNhan.GetFocusedRowCellValue(MaBN));
                                BenhNhan sua = _Data.BenhNhans.Single(p => p.MaBNhan == _MaBN);
                                sua.NNhap = LupNgayNhap.DateTime;
                                sua.TenBNhan = txtNhapTBN.Text;
                                sua.TuyenDuoi = 3;
                                if (radNamNu.SelectedIndex == 0)
                                    sua.GTinh = 1;
                                else if (radNamNu.SelectedIndex == 1)
                                    sua.GTinh = 0;


                                if (txtNgaySinh.Text != null)
                                {
                                    sua.NgaySinh = txtNgaySinh.Text.Length == 1 ? "0" + txtNgaySinh.Text : txtNgaySinh.Text;
                                }
                                if (txtThangSinh.Text != null)
                                {
                                    sua.ThangSinh = txtThangSinh.Text.Length == 1 ? "0" + txtThangSinh.Text : txtThangSinh.Text;
                                }
                                if (!string.IsNullOrEmpty(txtNamSinh.Text))
                                    sua.NamSinh = txtNamSinh.Text;
                                if (!string.IsNullOrEmpty(txtTuoi.Text))
                                    sua.Tuoi = Convert.ToInt32(txtTuoi.Text);
                                sua.DChi = txtDiaChi.Text;
                                sua.MaCB = lupCB.EditValue.ToString();

                                if (lupKP.EditValue != null)
                                    sua.MaKP = Convert.ToInt32(lupKP.EditValue);
                                _Data.SaveChanges();
                                #endregion // end sua bệnh nhân

                                #region // sửa thông tin Ra vien
                                //var ktrv = _Data.RaViens.Where(p => p.MaBNhan == _MaBN).ToList();
                                //if (ktrv.Count > 0)
                                //{
                                //    int id = ktrv.First().MaBNhan;
                                //    var sua2 = _Data.RaViens.Single(p => p.MaBNhan == id);
                                //    sua2.ChanDoan = lupChandoan.Text;
                                //    sua2.MaBNhan = _MaBN;
                                //    sua2.MaICD = lupMaICD.EditValue.ToString();

                                //    if (lupKP.EditValue != null)
                                //        sua2.MaKP = Convert.ToInt32(lupKP.EditValue);
                                //    sua2.NgayRa = LupNgayNhap.DateTime.AddMinutes(10);
                                //    sua2.NgayVao = LupNgayNhap.DateTime;
                                //    _Data.SaveChanges();
                                //}
                                //else
                                //{
                                //    RaVien themmoi2 = new RaVien();
                                //    themmoi2.ChanDoan = lupChandoan.Text;
                                //    themmoi2.MaBNhan = _MaBN;
                                //    themmoi2.MaICD = lupMaICD.EditValue.ToString();

                                //    if (lupKP.EditValue != null)
                                //        themmoi2.MaKP = Convert.ToInt32(lupKP.EditValue);
                                //    themmoi2.NgayRa = LupNgayNhap.DateTime.AddMinutes(10);
                                //    themmoi2.NgayVao = LupNgayNhap.DateTime;

                                //    _Data.RaViens.Add(themmoi2);
                                //    _Data.SaveChanges();
                                //}
                                #endregion  //end sửa thông tin ra viện

                                #region   // sửa thông tin dthuoc
                                int _Iddt = -1;
                                var ktdt = _Data.DThuocs.Where(p => p.MaBNhan == (_MaBN)).ToList();
                                if (ktdt.Count > 0)
                                {
                                    _Iddt = ktdt.First().IDDon;
                                    int id = -1;
                                    id = ktdt.First().IDDon;
                                    var sua3 = _Data.DThuocs.Single(p => p.IDDon == id);
                                    sua3.NgayKe = LupNgayNhap.DateTime;
                                    if (lupKP.EditValue != null)
                                        sua3.MaKP = Convert.ToInt32(lupKP.EditValue);
                                    _Data.SaveChanges();
                                }
                                else
                                {
                                    DThuoc themmoi3 = new DThuoc();
                                    themmoi3.MaBNhan = _MaBN;
                                    themmoi3.NgayKe = LupNgayNhap.DateTime;
                                    themmoi3.KieuDon = 6;
                                    if (lupKP.EditValue != null)
                                        themmoi3.MaKP = Convert.ToInt32(lupKP.EditValue);
                                    _Data.DThuocs.Add(themmoi3);
                                    _Data.SaveChanges();
                                }
                                #endregion  // end sửa thông tin viện phí
                                #region  // sửa thông tin dthuocct
                                //var VP = (from V in _Data.DThuocs.Where(p => p.MaBNhan == _MaBN) select new { V.IDDon }).ToList();
                                //if (VP.Count > 0)
                                //{
                                //    _Iddt = VP.Last().IDDon;
                                //}
                                if (_Iddt > 0)
                                {
                                    for (int i = 0; i < GrvChiphi.RowCount; i++)
                                    {
                                        int ID = 0;
                                        if (GrvChiphi.GetRowCellValue(i, IDDonct) != null)
                                            ID = Convert.ToInt32(GrvChiphi.GetRowCellValue(i, IDDonct));
                                        if (ID <= 0)
                                        {
                                            if (GrvChiphi.GetRowCellValue(i, MaDV) != null && GrvChiphi.GetRowCellValue(i, MaDV).ToString() != "")
                                            {
                                                if (GrvChiphi.GetRowCellValue(i, SoLuong) != null && GrvChiphi.GetRowCellValue(i, SoLuong).ToString() != "" && GrvChiphi.GetRowCellValue(i, DonGia) != null && GrvChiphi.GetRowCellValue(i, DonGia).ToString() != "" && GrvChiphi.GetRowCellValue(i, DonVi) != null)
                                                {
                                                    DThuocct themmoi4 = new DThuocct();
                                                    themmoi4.IDDon = _Iddt;
                                                    if (lupKP.EditValue != null)
                                                        themmoi4.MaKP = Convert.ToInt32(lupKP.EditValue);
                                                    themmoi4.MaDV = GrvChiphi.GetRowCellValue(i, MaDV) == null ? 0 : Convert.ToInt32(GrvChiphi.GetRowCellValue(i, MaDV));
                                                    themmoi4.DonVi = GrvChiphi.GetRowCellValue(i, DonVi).ToString();
                                                    themmoi4.DonGia = Convert.ToDouble(GrvChiphi.GetRowCellValue(i, DonGia).ToString());
                                                    themmoi4.SoLuong = Convert.ToDouble(GrvChiphi.GetRowCellValue(i, SoLuong).ToString());
                                                    themmoi4.ThanhTien = Convert.ToDouble(GrvChiphi.GetRowCellValue(i, ThanhTien).ToString());
                                                    if (GrvChiphi.GetRowCellValue(i, colGhiChu) != null)
                                                        themmoi4.GhiChu = GrvChiphi.GetRowCellValue(i, colGhiChu).ToString();

                                                    _Data.DThuoccts.Add(themmoi4);
                                                    _Data.SaveChanges();
                                                }
                                            }
                                        }
                                        else
                                        {
                                            {
                                                if (GrvChiphi.GetRowCellValue(i, SoLuong) != null && GrvChiphi.GetRowCellValue(i, SoLuong).ToString() != "" && GrvChiphi.GetRowCellValue(i, DonGia) != null && GrvChiphi.GetRowCellValue(i, DonGia).ToString() != "")
                                                {
                                                    var themmoi4 = _Data.DThuoccts.Single(p => p.IDDonct == ID);

                                                    themmoi4.IDDon = _Iddt;
                                                    if (lupKP.EditValue != null)
                                                        themmoi4.MaKP = Convert.ToInt32(lupKP.EditValue);

                                                    themmoi4.MaDV = GrvChiphi.GetRowCellValue(i, MaDV) == null ? 0 : Convert.ToInt32(GrvChiphi.GetRowCellValue(i, MaDV));
                                                    themmoi4.DonVi = GrvChiphi.GetRowCellValue(i, DonVi).ToString();
                                                    themmoi4.DonGia = Convert.ToDouble(GrvChiphi.GetRowCellValue(i, DonGia).ToString());
                                                    themmoi4.SoLuong = Convert.ToDouble(GrvChiphi.GetRowCellValue(i, SoLuong).ToString());
                                                    themmoi4.ThanhTien = Convert.ToDouble(GrvChiphi.GetRowCellValue(i, ThanhTien).ToString());
                                                    if (GrvChiphi.GetRowCellValue(i, colGhiChu) != null)
                                                        themmoi4.GhiChu = GrvChiphi.GetRowCellValue(i, colGhiChu).ToString();

                                                    _Data.SaveChanges();
                                                }
                                            }
                                        }
                                    }
                                }
                                DSBN();

                                #endregion // end sửa thông tin VPct

                                #region   // sửa thông tin nhập dược
                                //int _idnd = -1;
                                //var ktnd = _Data.NhapDs.Where(p => p.MaBNhan == (_MaBN)).ToList();
                                //if (ktnd.Count > 0)
                                //{
                                //    _idnd = ktnd.First().IDNhap;

                                //    var sua3 = _Data.VienPhis.Single(p => p.idVPhi == _idnd);
                                //    sua3.MaBNhan = _MaBN;
                                //    sua3.NgayNhap = LupNgayNhap.DateTime;
                                //    sua3.MaCB = DungChung.Bien.MaCB;
                                //    if (lupKP.EditValue != null)
                                //        sua3.MaKP = Convert.ToInt32(lupKP.EditValue);
                                //    _Data.SaveChanges();
                                //}
                                //else
                                //{
                                //    NhapD themmoi3 = new NhapD();
                                //    themmoi3.MaBNhan = _MaBN;
                                //    themmoi3.NgayNhap = LupNgayNhap.DateTime;
                                //    themmoi3.PLoai = 2;
                                //    themmoi3.KieuDon = 12;
                                //    if (lupKP.EditValue != null)
                                //        themmoi3.MaKP = Convert.ToInt32(lupKP.EditValue);
                                //    _Data.NhapDs.Add(themmoi3);
                                //    _Data.SaveChanges();

                                //}
                                #endregion  // end sửa thông tin nhập dược
                                #region  // sửa thông tin nhập dược ct
                                //delete nhập dược ct
                                //List<NhapDct> _lndct = _Data.NhapDcts.Where(p => p.IDNhap == _idnd).ToList();
                                //foreach (NhapDct nd in _lndct)
                                //{
                                //    _Data.NhapDcts.Remove(nd);
                                //}
                                //_Data.SaveChanges();
                                //for (int i = 0; i < GrvChiphi.RowCount; i++)
                                //{
                                //    if (GrvChiphi.GetRowCellValue(i, MaDV) != null && GrvChiphi.GetRowCellValue(i, MaDV).ToString() != "")
                                //    {
                                //        if (GrvChiphi.GetRowCellValue(i, SoLuong) != null && GrvChiphi.GetRowCellValue(i, SoLuong).ToString() != "" && GrvChiphi.GetRowCellValue(i, DonGia) != null && GrvChiphi.GetRowCellValue(i, DonGia).ToString() != "" && GrvChiphi.GetRowCellValue(i, DonVi) != null)
                                //        {
                                //            NhapDct themmoi4 = new NhapDct();
                                //            themmoi4.IDNhap = _idnd;

                                //            themmoi4.MaDV = GrvChiphi.GetRowCellValue(i, MaDV) == null ? 0 : Convert.ToInt32(GrvChiphi.GetRowCellValue(i, MaDV));
                                //            themmoi4.DonVi = GrvChiphi.GetRowCellValue(i, DonVi).ToString();
                                //            themmoi4.DonGia = Convert.ToDouble(GrvChiphi.GetRowCellValue(i, DonGia).ToString());
                                //            themmoi4.SoLuongX = Convert.ToDouble(GrvChiphi.GetRowCellValue(i, SoLuong).ToString());
                                //            themmoi4.ThanhTienX = Convert.ToDouble(GrvChiphi.GetRowCellValue(i, ThanhTien).ToString());
                                //            _Data.NhapDcts.Add(themmoi4);
                                //            _Data.SaveChanges();
                                //        }
                                //    }

                                //}

                                //DSBN();

                                #endregion // end sửa thông tin VPct
                                #region   // sửa thông tin VienPhi
                                //int _IdVP = -1;
                                //var ktvp = _Data.VienPhis.Where(p => p.MaBNhan == (_MaBN)).ToList();
                                //if (ktvp.Count > 0)
                                //{
                                //    _IdVP = ktvp.First().idVPhi;
                                //    int id = -1;
                                //    id = ktvp.First().idVPhi;
                                //    var sua3 = _Data.VienPhis.Single(p => p.idVPhi == id);
                                //    sua3.MaBNhan = _MaBN;
                                //    sua3.NgayTT = LupNgayNhap.DateTime.AddMinutes(15);
                                //    sua3.NgayRa = LupNgayNhap.DateTime.AddMinutes(10);

                                //    if (lupKP.EditValue != null)
                                //        sua3.MaKP = Convert.ToInt32(lupKP.EditValue);
                                //    _Data.SaveChanges();
                                //}
                                //else
                                //{
                                //    VienPhi themmoi3 = new VienPhi();
                                //    themmoi3.MaBNhan = _MaBN;
                                //    themmoi3.NgayTT = LupNgayNhap.DateTime.AddMinutes(15);
                                //    themmoi3.NgayDuyetCP = LupNgayNhap.DateTime.AddMinutes(15);
                                //    themmoi3.NgayRa = LupNgayNhap.DateTime.AddMinutes(10);

                                //    if (lupKP.EditValue != null)
                                //        themmoi3.MaKP = Convert.ToInt32(lupKP.EditValue);
                                //    _Data.VienPhis.Add(themmoi3);
                                //    _Data.SaveChanges();

                                //}
                                #endregion  // end sửa thông tin viện phí
                                #region  // sửa thông tin VienPhict
                                ////delete vpct
                                //List<VienPhict> _lvpct = _Data.VienPhicts.Where(p => p.idVPhi == _IdVP).ToList();
                                //foreach (VienPhict vp in _lvpct)
                                //{
                                //    _Data.VienPhicts.Remove(vp);
                                //}
                                //_Data.SaveChanges();
                                //for (int i = 0; i < GrvChiphi.RowCount; i++)
                                //{
                                //    if (GrvChiphi.GetRowCellValue(i, MaDV) != null && GrvChiphi.GetRowCellValue(i, MaDV).ToString() != "")
                                //    {
                                //        if (GrvChiphi.GetRowCellValue(i, SoLuong) != null && GrvChiphi.GetRowCellValue(i, SoLuong).ToString() != "" && GrvChiphi.GetRowCellValue(i, DonGia) != null && GrvChiphi.GetRowCellValue(i, DonGia).ToString() != "" && GrvChiphi.GetRowCellValue(i, DonVi) != null)
                                //        {
                                //            VienPhict themmoi4 = new VienPhict();
                                //            themmoi4.idVPhi = _IdVP;
                                //            if (lupKP.EditValue != null)
                                //                themmoi4.MaKP = Convert.ToInt32(lupKP.EditValue);
                                //            themmoi4.MaDV = GrvChiphi.GetRowCellValue(i, MaDV) == null ? 0 : Convert.ToInt32(GrvChiphi.GetRowCellValue(i, MaDV));
                                //            themmoi4.DonVi = GrvChiphi.GetRowCellValue(i, DonVi).ToString();
                                //            themmoi4.DonGia = Convert.ToDouble(GrvChiphi.GetRowCellValue(i, DonGia).ToString());
                                //            themmoi4.SoLuong = Convert.ToDouble(GrvChiphi.GetRowCellValue(i, SoLuong).ToString());
                                //            themmoi4.ThanhTien = Convert.ToDouble(GrvChiphi.GetRowCellValue(i, ThanhTien).ToString());
                                //            themmoi4.NgayChiPhi = LupNgayNhap.DateTime.AddMinutes(15);
                                //            _Data.VienPhicts.Add(themmoi4);
                                //            _Data.SaveChanges();
                                //        }
                                //    }

                                //}

                                //DSBN();

                                #endregion // end sửa thông tin VPct
                                MessageBox.Show("Sửa thành công bệnh nhân");
                            }

                        }
                        UNREADONLY(true);
                        EnableControl(true);
                        Trangthai = -1;

                    }
                }
                else
                {
                    MessageBox.Show("Chưa chọn thuốc");
                    return;
                }
            }
        }

        private void GrvChiphi_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            //if (e.Column.Name == "Dongia")
            //{
            //    MessageBox.Show("tt");
            //}
        }

        private void GrvChiphi_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            //Bds.DataSource = _DMChiPhi.ToList();
            //GrcChiphi.DataSource = Bds;
            if (e.Column.Name == "tendv")
            {
                //string _TenDV = "";
                //_TenDV=GrvChiphi.GetFocusedRowCellValue(tendv).ToString();
                //var Donvi = (from Dichvu in _Data.DichVus.Where(p => p.TenDV == _TenDV)
                //             select new { Dichvu.DonVi, Dichvu.TenDV }).ToList();
                //if(Donvi.Count>0)
                //{
                //    foreach (var a in Donvi)
                //    {
                //        DMChiPhi themmoi = new DMChiPhi();
                //        themmoi.tendv = a.TenDV;
                //        themmoi.donvi = a.DonVi;
                //        _DMChiPhi.Add(themmoi);
                //    }
                //    GrcChiphi.DataSource = _DMChiPhi;

            }
            //MessageBox.Show(_TenDV);
        }







        private void txtNamsinh_EditValueChanged(object sender, EventArgs e)
        {
            if (txtNamSinh.Text != null && txtNamSinh.Text.Length == 4)
            {
                int HT = Convert.ToInt32(System.DateTime.Now.ToString().Substring(6, 4));
                int NS = Convert.ToInt32(txtNamSinh.Text.Trim());
                int T = HT - NS;
                if (T < 0 || T > 100)
                {
                    MessageBox.Show("Năm sinh không chính xác");
                }
                else
                {
                    txtTuoi.Text = T.ToString();
                }
            }
        }

        private void SBTSua_Click(object sender, EventArgs e)
        {
            Trangthai = 1;
            UNREADONLY(false);
            //Bds.DataSource = _DMChiPhi.ToList();
            //GrcChiphi.DataSource = Bds;
            EnableControl(false);

        }

        private void SBTXoa_Click(object sender, EventArgs e)
        {
            int _MaBN = 0;
            if (GrvBenhNhan.GetFocusedRowCellValue(MaBN) != null)
            {
                int ID = 0;
                _MaBN = Convert.ToInt32(GrvBenhNhan.GetFocusedRowCellValue(MaBN));
                //Xoá Benh nhan
                var xoa = _Data.BenhNhans.Single(p => p.MaBNhan == _MaBN);
                var qnd = _Data.NhapDs.Where(p => p.MaBNhan == _MaBN).ToList();
                if (qnd.Count > 0)
                {
                    MessageBox.Show("Bệnh nhân đã được xuất dược, bạn không thể xóa");
                }
                else if (xoa.Status == 2 || xoa.Status == 3)
                {
                    MessageBox.Show("Bệnh nhân đã thanh toán, bạn không thể xóa");
                }
                else
                {
                    DialogResult _result = MessageBox.Show("Bạn có muốn xóa bệnh nhân '" + xoa.TenBNhan + "' không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (_result == DialogResult.Yes)
                    {
                        _Data.BenhNhans.Remove(xoa);
                        //end xoá benh nhan
                        #region xóa DThuoc
                        var ktdt = _Data.DThuocs.Where(p => p.MaBNhan == _MaBN).ToList();
                        if (ktdt.Count > 0)
                        {
                            ID = ktdt.First().IDDon;

                            // xoá dtct
                            var ktdtct = _Data.DThuoccts.Where(p => p.IDDon == ID).ToList();
                            foreach (var d in ktdtct)
                            {
                                int idct = d.IDDonct;
                                var xoa4 = _Data.DThuoccts.Single(p => p.IDDonct == idct);
                                _Data.DThuoccts.Remove(xoa4);
                            }

                            var xoa3 = _Data.DThuocs.Single(p => p.IDDon == ID);
                            _Data.DThuocs.Remove(xoa3);
                        }
                        #endregion
                        #region xoá ra viện
                        //var ktrv = _Data.RaViens.Where(p => p.MaBNhan == _MaBN).ToList();
                        //foreach (var item in ktrv)
                        //{

                        //    _Data.RaViens.Remove(item);
                        //}
                        #endregion end xoá ra viện
                        #region xoá Vienphi
                        //var ktvp = _Data.VienPhis.Where(p => p.MaBNhan == _MaBN).ToList();
                        //if (ktvp.Count > 0)
                        //{
                        //    ID = ktvp.First().idVPhi;
                        //    // end xoá vien phi
                        //    // xoá viện phí ct
                        //    var vpct = _Data.VienPhicts.Where(p => p.idVPhi == ID).ToList();
                        //    foreach (var d in vpct)
                        //    {
                        //        int idct = d.idVPhict;
                        //        var xoa4 = _Data.VienPhicts.Single(p => p.idVPhict == idct);
                        //        _Data.VienPhicts.Remove(xoa4);
                        //    }

                        //    var xoa3 = _Data.VienPhis.Single(p => p.idVPhi == ID);
                        //    _Data.VienPhis.Remove(xoa3);
                        //}
                        #endregion end xoá viện phí
                        #region xóa nhập dược
                        //var ktnd = _Data.NhapDs.Where(p => p.MaBNhan == _MaBN).ToList();
                        //if (ktnd.Count > 0)
                        //{
                        //    ID = ktnd.First().IDNhap;

                        //    // xoá nhập dược ct
                        //    var ktndct = _Data.NhapDcts.Where(p => p.IDNhap == ID).ToList();
                        //    foreach (var d in ktndct)
                        //    {
                        //        int idct = d.IDNhapct;
                        //        var xoa4 = _Data.NhapDcts.Single(p => p.IDNhapct == idct);
                        //        _Data.NhapDcts.Remove(xoa4);
                        //    }

                        //    var xoa3 = _Data.NhapDs.Single(p => p.IDNhap == ID);
                        //    _Data.NhapDs.Remove(xoa3);
                        //}
                        #endregion end xoá nhập dược
                        _Data.SaveChanges();

                        MessageBox.Show("Xóa thành công");
                        DSBN();
                    }
                }

            }
        }
        List<Ham.giaSoLoHSD> dsgia = new List<Ham.giaSoLoHSD>();
        // List<Ham.giaSoLoHSD> allGia = new List<Ham.giaSoLoHSD>();

        private void GrvChiphi_CellValueChanged_1(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            double _SLuong = 0;
            int iddtct = 0;
            int _MaDV = 0;
            double soluong_old = 0, _DGia = 0;
            _Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            if (GrvChiphi.GetFocusedRowCellValue(TenCP) != null)
            {
                _MaDV = Convert.ToInt32(GrvChiphi.GetFocusedRowCellValue(TenCP));
            }

            if (GrvChiphi.GetFocusedRowCellValue("IDDonct") != null && Convert.ToInt32(GrvChiphi.GetFocusedRowCellValue("IDDonct")) > 0)
            {
                iddtct = Convert.ToInt32(GrvChiphi.GetFocusedRowCellValue("IDDonct"));
            }

            if (iddtct > 0)
            {
                var qsl = _Data.DThuoccts.Where(p => p.IDDonct == iddtct).Where(p => p.MaDV == _MaDV).FirstOrDefault();
                if (qsl != null)
                    soluong_old = qsl.SoLuong;
            }
            int MaKP = 0; double ton = 0;
            if (lupKP.EditValue != null)
            {
                MaKP = Convert.ToInt32(lupKP.EditValue);
            }
            if (e.Column.Name == "TenCP")
            {
                if (_MaDV > 0)
                {
                    dsgia = new List<Ham.giaSoLoHSD>();
                    #region lấy ra đơn vị
                    var KT = (from dv in _lDichVu.Where(p => p.MaDV == _MaDV)
                              select new { dv.PLoai, dv.DonVi, dv.TrongDM }).OrderByDescending(p => p.PLoai).ToList();
                    //double dongia = 0;

                    //var dongiaUT = (from dg in _lGiaUT.Where(p => p.MaDV == _MaDV) select dg.DonGia).FirstOrDefault();
                    //if (dongiaUT != null)
                    //    dongia = dongiaUT.Value;

                    #endregion
                    groupControl4.Text = "Tồn: 0";
                    if (KT.Count > 0)
                    {
                        dsgia = Ham._getDSGia(_Data, _MaDV, MaKP);
                        // allGia.AddRange(dsgia);
                        // lupDonGia.DataSource = allGia;
                        GrvChiphi.SetFocusedRowCellValue("DonVi", KT.First().DonVi);
                        GrvChiphi.SetFocusedRowCellValue(DonGia, 0);
                        GrvChiphi.SetFocusedRowCellValue(SoLuong, 0);

                        if (iddtct <= 0)
                        {
                            GrvChiphi.SetFocusedRowCellValue("IDDonct", -1);

                        }
                        if (GrvChiphi.GetFocusedRowCellValue("IDDon") != null && Convert.ToInt32(GrvChiphi.GetFocusedRowCellValue("IDDon")) > 0)
                        {

                        }
                        else
                        {
                            GrvChiphi.SetFocusedRowCellValue("IDDon", -1);

                        }
                    }



                }
            }

            if (e.Column.Name == "DonGia")
            {
                if (GrvChiphi.GetFocusedRowCellValue(DonGia) != null)
                {
                    _DGia = Convert.ToDouble(GrvChiphi.GetFocusedRowCellValue(DonGia));
                }
                if (GrvChiphi.GetFocusedRowCellValue(SoLuong) != null && GrvChiphi.GetFocusedRowCellValue(DonGia) != null)
                {
                    _SLuong = Convert.ToInt32(GrvChiphi.GetFocusedRowCellValue(SoLuong));
                }
                double _Thanhtien = 0;
                if (_SLuong >= 0)
                {
                    _Thanhtien = _DGia * _SLuong;
                    ton = DungChung.Ham._checkTon_KD(_Data, _MaDV, MaKP, _DGia, 0, "") + soluong_old;
                    if (ton - _SLuong >= 0)
                    {

                        GrvChiphi.SetFocusedRowCellValue(ThanhTien, _Thanhtien);

                        groupControl4.Text = "Tồn: " + (ton - _SLuong);
                    }
                    else
                    {
                        if (_SLuong > 0)
                            GrvChiphi.SetFocusedRowCellValue(SoLuong, 0);
                        if (_Thanhtien > 0)
                            GrvChiphi.SetFocusedRowCellValue(ThanhTien, 0);

                        groupControl4.Text = "Tồn: " + ton;
                    }
                }
                else
                {
                    MessageBox.Show("Số lượng phải >0");
                    GrvChiphi.FocusedColumn = GrvChiphi.VisibleColumns[2];

                }
            }
            if (e.Column.Name == "SoLuong")
            {
                double _tyle = 0;
                double _Thanhtien = 0;

                _tyle = _tyle / 100;
                if (GrvChiphi.GetFocusedRowCellValue(DonGia) != null)
                {
                    _DGia = Convert.ToDouble(GrvChiphi.GetFocusedRowCellValue(DonGia));
                }
                if (GrvChiphi.GetFocusedRowCellValue(SoLuong) != null && GrvChiphi.GetFocusedRowCellValue(DonGia) != null)
                {
                    _SLuong = Convert.ToInt32(GrvChiphi.GetFocusedRowCellValue(SoLuong));
                }
                if (_SLuong >= 0)
                {

                    _Thanhtien = _DGia * _SLuong;
                    ton = DungChung.Ham._checkTon_KD(_Data, _MaDV, MaKP, _DGia, 0, "") + soluong_old;
                    if (ton - _SLuong >= 0)
                    {
                        GrvChiphi.SetFocusedRowCellValue(ThanhTien, _Thanhtien);

                        groupControl4.Text = "Tồn: " + (ton - _SLuong);
                    }
                    else
                    {
                        if (_SLuong > 0)
                            GrvChiphi.SetFocusedRowCellValue(SoLuong, 0);
                        if (_Thanhtien > 0)
                            GrvChiphi.SetFocusedRowCellValue(ThanhTien, 0);

                        groupControl4.Text = "Tồn: " + ton;

                    }
                }
                else
                {
                    MessageBox.Show("Số lượng phải >0");
                    GrvChiphi.FocusedColumn = GrvChiphi.VisibleColumns[2];

                }
            }

        }


        private void HoTen_Leave(object sender, EventArgs e)
        {
            txtNhapTBN.Text = DungChung.Ham.ToFirstUpper(txtNhapTBN.Text.Trim());
        }


        private void SBTHuy_Click(object sender, EventArgs e)
        {
            this.Frm_KeDonNhaThuoc_Load(sender, e);
            EnableControl(true);
            Trangthai = -1;
        }
        double _ptthanhtoan = 0;




        List<BenhVien> _lbv = new List<BenhVien>();



        private void GrvChiphi_RowCellClick_1(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Column.Name == "colXoa")
            {
                DialogResult _result = MessageBox.Show("Xóa chi phí: " + GrvChiphi.GetFocusedRowCellDisplayText(TenCP), "xóa chi tiết!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (_result == DialogResult.Yes)
                {
                    int xoaid = 0;
                    if (GrvChiphi.GetFocusedRowCellValue(IDDonct) != null && GrvChiphi.GetFocusedRowCellValue(IDDonct).ToString() != "")
                        xoaid = Convert.ToInt32(GrvChiphi.GetFocusedRowCellValue(IDDonct));
                    if (xoaid > 0)
                    {
                        var xoa = _Data.DThuoccts.Single(p => p.IDDonct == xoaid);
                        _Data.DThuoccts.Remove(xoa);
                        _Data.SaveChanges();
                        GrvChiphi.DeleteSelectedRows();
                    }
                    else
                    {
                        GrvChiphi.DeleteSelectedRows();
                    }
                }
            }
        }

        private void GrvBenhNhan_DataSourceChanged(object sender, EventArgs e)
        {
            //GrvBenhNhan_FocusedRowChanged(null, null);
        }




        int _maubk = 0;
        private void btnIn_Click(object sender, EventArgs e)
        {
            //int _mabn = 0, ot = 0;
            //    if (Int32.TryParse(txtMaBNhan.Text, out ot))
            //        _mabn = Convert.ToInt32(txtMaBNhan.Text);
            int _mabn = 0;
            int makp = 0;
            if (lupKP.EditValue != null)
                makp = Convert.ToInt32(lupKP.EditValue);
            if (GrvBenhNhan.GetFocusedRowCellValue(MaBN) != null)
            {
                _mabn = Convert.ToInt32(GrvBenhNhan.GetFocusedRowCellValue(MaBN));
                _Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                BaoCao.rep_KeDonNhaThuoc rep = new BaoCao.rep_KeDonNhaThuoc();
                frmIn frm = new frmIn();
                if (Bien.MaBV == "24272")
                {
                    rep.celTenBV24272.Text = Bien.TenCQ.ToUpper();
                    rep.celTenKP24272.Text = lupKP.Text;
                    rep.celTen24272.Text = txtNhapTBN.Text;
                    rep.celNamSinh24272.Text = txtNamSinh.Text;
                    rep.celGTinh24272.Text = radNamNu.SelectedIndex == 0 ? "Nam" : (radNamNu.SelectedIndex == 1 ? "Nữ" : "");
                    rep.celDiaChi24272.Text = txtDiaChi.Text;
                    var dthuoc = _Data.DThuocs.FirstOrDefault(p => p.MaBNhan == _mabn);
                    if (dthuoc != null)
                    {
                        var date = dthuoc.NgayKe.Value;
                        rep.celSophieu.Text = "PXDV" + date.ToString("dd") + date.ToString("MM") + date.ToString("yy") + dthuoc.MaBNhan.Value.ToString("D4");
                    }


                    var qvienphi = (from vp in _Data.DThuocs.Where(p => p.MaBNhan == _mabn)
                                    join vpct in _Data.DThuoccts on vp.IDDon equals vpct.IDDon
                                    join dv in _Data.DichVus on vpct.MaDV equals dv.MaDV
                                    select new { dv.TenDV, vpct.DonGia, vpct.SoLuong, vpct.DonVi, vpct.ThanhTien, vpct.Luong, vpct.MoiLan, vpct.GhiChu}
                                       ).OrderBy(p => p.TenDV).Distinct().ToList();
                    double tt = 0;
                    if (qvienphi.Count > 0)
                        tt = qvienphi.Sum(p => p.ThanhTien);
                    double lamtron = 0;
                    lamtron = Math.Round(tt);
                    rep.celTongTien24272.Text = tt.ToString("###,###.##");
                    rep.celSoTien24272.Text = tt.ToString("###,###.##") + " VNĐ";
                    rep.celBangChu24272.Text = DungChung.Ham.DocTienBangChu(lamtron, " đồng");
                    string datenhap = LupNgayNhap.EditValue.ToString();
                    string dater = string.Empty;
                    if (datenhap.Length > 0)
                    {
                        DateTime _date = Convert.ToDateTime(datenhap);
                        DungChung.Bien.FormatDate = 1;
                        dater = DungChung.Ham.NgaySangChu(_date, DungChung.Bien.FormatDate);
                    }
                    rep.celNgay.Text = dater;

                    rep.DataSource = qvienphi.ToList();
                }
                else
                {
                    rep.celMaBN.Text = rep.celSophieu.Text = _mabn.ToString();
                    rep.celTenBV.Text = DungChung.Bien.TenCQ.ToUpper();
                    rep.celTenKP.Text = lupKP.Text;
                    rep.celTen.Text = txtNhapTBN.Text;
                    rep.celTuoi.Text = txtTuoi.Text;
                    rep.celGTinh.Text = radNamNu.SelectedIndex == 0 ? "Nam" : (radNamNu.SelectedIndex == 1 ? "Nữ" : "");
                    rep.celDiaChi.Text = txtDiaChi.Text;
                    rep.celCanBo.Text = lupCB.Text;

                    rep.celDiaDanh.Text = DungChung.Bien.DiaDanh + ", ngày " + DateTime.Now.Day.ToString("D2") + " tháng " + DateTime.Now.Month.ToString("D2") + " năm " + DateTime.Now.Year;
                    var qvienphi = (from vp in _Data.DThuocs.Where(p => p.MaBNhan == _mabn)
                                    join vpct in _Data.DThuoccts on vp.IDDon equals vpct.IDDon
                                    join dv in _Data.DichVus on vpct.MaDV equals dv.MaDV
                                    select new { dv.TenDV, dv.DonGia, vpct.SoLuong, vpct.DonVi, vpct.ThanhTien, vpct.Luong, vpct.MoiLan, vpct.GhiChu }
                                       ).OrderBy(p => p.TenDV).ToList();
                    double tt = 0;
                    if (qvienphi.Count > 0)
                        tt = qvienphi.Sum(p => p.ThanhTien);
                    double lamtron = 0;
                    lamtron = Math.Round(tt);
                    rep.celSoTien.Text = tt.ToString("###,###.##") + " đồng";
                    rep.celBangChu.Text = DungChung.Ham.DocTienBangChu(lamtron, " đồng");

                    rep.DataSource = qvienphi.ToList();
                }

                rep.BindingData();
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();

            }
        }

        private void lup_TK_QuayThuoc_EditValueChanged(object sender, EventArgs e)
        {
            DSBN();
        }

        private void lupKP_EditValueChanged(object sender, EventArgs e)
        {
            _Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            int makp = 0;
            if (lupKP.EditValue != null && !string.IsNullOrEmpty(lupKP.Text))
                makp = Convert.ToInt32(lupKP.EditValue);
            if (makp > 0)
            {
                //load danh sách thuốc
                var qthuoc0 = (from nd in _Data.NhapDs.Where(p => p.MaKP == makp).Where(p => p.PLoai == 1)
                               join ndct in _Data.NhapDcts on nd.IDNhap equals ndct.IDNhap
                               select new { ndct.MaDV, ndct.DonVi, ndct.DonGia }).ToList();
                var qthuoc = (from nd in qthuoc0
                              group nd by new { nd.MaDV, nd.DonVi } into kq
                              select new { kq.Key.MaDV, kq.Key.DonVi, }).ToList();
                var qdvu = _Data.DichVus.ToList();
                var qkq = (from nd in qthuoc
                           join dv in qdvu on nd.MaDV equals dv.MaDV
                           select new { nd.MaDV, nd.DonVi, dv.TenDV }).ToList();
                lupTenchiphi.DataSource = qkq;

                var qdgia = (from gia in qthuoc0 group gia by gia.DonGia into kq select new { Gia = kq.Key }).ToList();
                lupDonGia.DataSource = qdgia;
            }
            else
                lupTenchiphi.DataSource = null;
            //------

        }
        string _errMessage = "";
        private void GrvChiphi_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {

            GridView view = sender as GridView;
            string fieldName = string.Empty;

            double dongia = 0;
            object valMaDV = view.GetRowCellValue(e.RowHandle, TenCP);
            object valSL = view.GetRowCellValue(e.RowHandle, SoLuong);
            if (view.GetRowCellValue(e.RowHandle, DonGia) != null)
                dongia = Convert.ToDouble(view.GetRowCellValue(e.RowHandle, DonGia));
            if (valMaDV == null || string.IsNullOrEmpty(valMaDV.ToString()))
            {
                e.Valid = false;
                view.SetColumnError(TenCP, "Bạn chưa chọn loại thuốc, vật tư!");
            }
            if (valSL == null || Convert.ToDouble(valSL) <= 0)
            {
                e.Valid = false;
                //Set errors with specific descriptions for the columns
                view.SetColumnError(SoLuong, "Số lượng phải > 0");
            }

            int _MaDV = Convert.ToInt32(valMaDV);

            //kiểm tra thuốc đã được kê chưa
            for (int i = 0; i < view.RowCount; i++)
            {
                if (i != e.RowHandle)
                {
                    if (view.GetRowCellValue(i, TenCP) != null && view.GetRowCellValue(i, DonGia) != null)
                    {
                        if (dongia > 0 && Convert.ToInt32(view.GetRowCellValue(i, TenCP)) == _MaDV && Convert.ToInt32(view.GetRowCellValue(i, DonGia)) == dongia)
                        {
                            e.Valid = false;
                            view.SetColumnError(TenCP, "Thuốc, vật tư đã được kê!");
                            return;
                        }
                    }
                }
            }

            if (e.Valid)
                view.ClearColumnErrors();

        }
        private void GrvChiphi_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = ExceptionMode.NoAction;
            //DialogResult _result = MessageBox.Show(_errMessage, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Question);
            //if (_result == DialogResult.OK)
            //{
            //    GrvChiphi.FocusedColumn = GrvChiphi.VisibleColumns[2];
            //}
            //else
            //{
            //    GrvChiphi.SetFocusedRowCellValue(TenCP, 0);
            //}
            // MessageBox.Show(_errMessage);
        }


        private void GrvChiphi_ShownEditor(object sender, EventArgs e)
        {
            GridView view = (GridView)sender;
            if (view.FocusedColumn.FieldName == "Gia" && view.ActiveEditor is LookUpEdit)
            {
                if (dsgia.Count == 0)
                    dsgia.Add(new Ham.giaSoLoHSD { Gia = 0, SoLuong = 0 });
                LookUpEdit edit = (LookUpEdit)view.ActiveEditor;
                edit.Properties.DataSource = dsgia;
                edit.Properties.ValueMember = "Gia";
                edit.Properties.DisplayMember = "Gia";
                view.SetRowCellValue(view.FocusedRowHandle, DonGia, dsgia.First().Gia);
            }
        }


        public class CP
        {
            public int MaDV { set; get; }
            public string DonVi { set; get; }
            public double Gia { get; set; }
            public double SoLuong { get; set; }
            public double ThanhTien { get; set; }
            public string GhiChu { get; set; }
            public int IDDonct { get; set; }
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            int _mabn = 0;
            int makp = 0;
            if (lupKP.EditValue != null)
                makp = Convert.ToInt32(lupKP.EditValue);
            _Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            #region xóa tt
            //if (GrvBenhNhan.GetFocusedRowCellValue(MaBN) != null)
            //{
            //    _mabn = Convert.ToInt32(GrvBenhNhan.GetFocusedRowCellValue(MaBN));
            //VienPhi vp = data.VienPhis.Where(p => p.MaBNhan == _mabn).FirstOrDefault();
            //if (vp != null)
            //{
            //    var qvpct = data.VienPhicts.Where(p => p.idVPhi == vp.idVPhi).ToList();
            //    foreach(VienPhict vpct in qvpct)
            //    {
            //        //data.VienPhicts.Where(p => p.idVPhict == vpct.idVPhict).FirstOrDefault();
            //        data.VienPhicts.Remove(vpct);
            //        data.SaveChanges();
            //    }
            //    data.VienPhis.Remove(vp);
            //    data.SaveChanges();
            //}
            #endregion

            //thêm thanh toán mới
            if (GrvBenhNhan.GetFocusedRowCellValue(MaBN) != null)
            {
                _mabn = Convert.ToInt32(GrvBenhNhan.GetFocusedRowCellValue(MaBN));
                BenhNhan qbn = _Data.BenhNhans.Single(p => p.MaBNhan == _mabn);
                var qnd = _Data.NhapDs.Where(p => p.MaBNhan == _mabn).ToList();
                var qdt = _Data.DThuocs.FirstOrDefault(p => p.MaBNhan == _mabn);
                if (qbn != null)
                {

                    if (qnd.Count > 0)
                    {
                        MessageBox.Show("Bệnh nhân đã xuất dược, bạn không thể thanh toán tiếp");
                        return;
                    }
                    else if (qbn.Status == 2 || qbn.Status == 3)
                    {
                        MessageBox.Show("Bệnh nhân đã thanh toán, bạn không thể thanh toán tiếp");
                        return;
                    }
                    //#region Tạo đơn xuất trên bảng NhapD

                    //int mabn = 0;
                    //if (GrvBenhNhan.GetFocusedRowCellValue(MaBN) != null && GrvBenhNhan.GetFocusedRowCellValue(MaBN).ToString() != "")
                    //{
                    //    mabn = Convert.ToInt32(GrvBenhNhan.GetFocusedRowCellValue(MaBN));
                    //}

                    //NhapD nd = new NhapD();
                    //nd.MaBNhan = mabn;
                    //nd.NgayNhap = LupNgayNhap.DateTime;
                    //nd.SoCT = qdt.IDDon.ToString();
                    //nd.PLoai = 2;
                    //nd.KieuDon = 0;
                    //if (lupKP.EditValue != null)
                    //{
                    //    nd.MaKP = nd.MaKPnx = Convert.ToInt32(lupKP.EditValue);
                    //}
                    //nd.MaCB = DungChung.Bien.MaCB;
                    //nd.XuatTD = 1;
                    //nd.SoPL = qdt.IDDon;
                    //nd.IDDTBN = 0;
                    //nd.Mien = 0;
                    //nd.TraDuoc_KieuDon = 0;

                    //_Data.NhapDs.Add(nd);

                    //#endregion
                    //#region Tạo đơn xuất trên bảng NhapDct
                    //if (_Data.SaveChanges() > 0)
                    //{
                    //    for (int i = 0; i < GrvChiphi.RowCount; i++)
                    //    {
                    //        if (GrvChiphi.GetRowCellValue(i, MaDV) != null && GrvChiphi.GetRowCellValue(i, MaDV).ToString() != "")
                    //        {
                    //            if (GrvChiphi.GetRowCellValue(i, SoLuong) != null && GrvChiphi.GetRowCellValue(i, SoLuong).ToString() != "" && GrvChiphi.GetRowCellValue(i, DonGia) != null && GrvChiphi.GetRowCellValue(i, DonGia).ToString() != "")
                    //            {
                    //                NhapDct themmoi = new NhapDct();


                    //                themmoi.IDNhap = nd.IDNhap;
                    //                themmoi.MaDV = GrvChiphi.GetRowCellValue(i, MaDV) == null ? 0 : Convert.ToInt32(GrvChiphi.GetRowCellValue(i, MaDV));
                    //                themmoi.DonVi = GrvChiphi.GetRowCellValue(i, DonVi).ToString();
                    //                themmoi.DonGia = Convert.ToDouble(GrvChiphi.GetRowCellValue(i, DonGia).ToString());
                    //                themmoi.SoLuongX = Convert.ToDouble(GrvChiphi.GetRowCellValue(i, SoLuong).ToString());
                    //                themmoi.ThanhTienX = Convert.ToDouble(GrvChiphi.GetRowCellValue(i, ThanhTien).ToString());
                    //                themmoi.MaBNhan = mabn;

                    //                _Data.NhapDcts.Add(themmoi);
                    //                _Data.SaveChanges();
                    //            }
                    //        }
                    //    }
                    //}

                    //#endregion
                    #region//thêm mới viện phí
                    VienPhi themmoivp = new VienPhi();
                    themmoivp.MaBNhan = _mabn;
                    themmoivp.NgayTT = LupNgayNhap.DateTime.AddMinutes(15);
                    themmoivp.NgayThu = LupNgayNhap.DateTime.AddMinutes(15);
                    themmoivp.NgayDuyetCP = LupNgayNhap.DateTime.AddMinutes(15);
                    themmoivp.NgayRa = LupNgayNhap.DateTime.AddMinutes(10);
                    if (lupKP.EditValue != null)
                        themmoivp.MaKP = Convert.ToInt32(lupKP.EditValue);
                    _Data.VienPhis.Add(themmoivp);
                    _Data.SaveChanges();
                    #endregion // end thêm mới Vien phi
                    #region // thêm mới VienPhict
                    for (int i = 0; i < GrvChiphi.RowCount; i++)
                    {
                        if (GrvChiphi.GetRowCellValue(i, MaDV) != null && GrvChiphi.GetRowCellValue(i, MaDV).ToString() != "")
                        {
                            if (GrvChiphi.GetRowCellValue(i, SoLuong) != null && GrvChiphi.GetRowCellValue(i, SoLuong).ToString() != "" && GrvChiphi.GetRowCellValue(i, DonGia) != null && GrvChiphi.GetRowCellValue(i, DonGia).ToString() != "")
                            {
                                _Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                                VienPhict themmoi4 = new VienPhict();

                                if (lupKP.EditValue != null)
                                    themmoi4.MaKP = Convert.ToInt32(lupKP.EditValue);
                                themmoi4.idVPhi = themmoivp.idVPhi;
                                themmoi4.MaDV = GrvChiphi.GetRowCellValue(i, MaDV) == null ? 0 : Convert.ToInt32(GrvChiphi.GetRowCellValue(i, MaDV));
                                themmoi4.DonVi = GrvChiphi.GetRowCellValue(i, DonVi).ToString();
                                themmoi4.DonGia = Convert.ToDouble(GrvChiphi.GetRowCellValue(i, DonGia).ToString());
                                themmoi4.SoLuong = Convert.ToDouble(GrvChiphi.GetRowCellValue(i, SoLuong).ToString());
                                themmoi4.ThanhTien = Convert.ToDouble(GrvChiphi.GetRowCellValue(i, ThanhTien).ToString());
                                themmoi4.Duyet = 0;
                                themmoi4.NgayChiPhi = LupNgayNhap.DateTime.AddMinutes(15);
                                themmoi4.NgayYLenh = LupNgayNhap.DateTime.AddMinutes(15);

                                _Data.VienPhicts.Add(themmoi4);
                                _Data.SaveChanges();
                            }
                        }
                    }
                    #endregion end thêm mới VienPhict
                    BenhNhan benhnhan = _Data.BenhNhans.Single(p => p.MaBNhan == _mabn);
                    benhnhan.Status = 3;
                    _Data.SaveChanges();
                    #region//thêm mới ra viện
                    RaVien themmoi2 = new RaVien();
                    themmoi2.MaBNhan = _mabn;
                    if (lupKP.EditValue != null)
                        themmoi2.MaKP = Convert.ToInt32(lupKP.EditValue);

                    themmoi2.NgayRa = LupNgayNhap.DateTime.AddMinutes(10);
                    themmoi2.NgayVao = LupNgayNhap.DateTime;
                    themmoi2.SoNgaydt = 1;
                    themmoi2.Status = 2;
                    //themmoi2.SoNgaydt=
                    _Data.RaViens.Add(themmoi2);
                    _Data.SaveChanges();
                    #endregion // end thêm mới ra viện

                    #region thêm vào bảng tạm ứng
                    //  _Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                    var a = QLBV.FormNhap.us_KhamSucKhoe.QuyenSoBL._getQuyen_SoBL(1, "");
                    TamUng newTamUng = new TamUng();
                    newTamUng.MaBNhan = _mabn;
                    newTamUng.PhanLoai = 1;
                    newTamUng.MaCB = DungChung.Bien.MaCB;
                    if (lupCB.EditValue != null)
                        newTamUng.MaCB = lupCB.EditValue.ToString();

                    var qvpct = _Data.VienPhicts.Where(p => p.idVPhi == themmoivp.idVPhi).ToList();
                    if (qvpct.Count > 0)
                    {
                        newTamUng.SoTien = qvpct.Sum(p => p.ThanhTien);
                        newTamUng.TienChenh = newTamUng.SoTien.Value;
                    }
                    newTamUng.NgayThu = LupNgayNhap.DateTime.AddMinutes(15);
                    newTamUng.QuyenHD = a.FirstOrDefault().Quyen;
                    newTamUng.SoHD = a.FirstOrDefault().So != null ? (a.FirstOrDefault().So + 1).ToString() : "";
                    _Data.TamUngs.Add(newTamUng);
                    _Data.SaveChanges();
                    #endregion
                    MessageBox.Show("Thanh toán thành công");
                    if (DungChung.Bien.MaBV == "30303")
                        usTamThu_TToan._InPhieuThu_01071(newTamUng.IDTamUng, 1, false);
                    else
                        InPhieuThu(newTamUng);
                    btnIn_Click(null, null);
                    DSBN();
                    Trangthai = -1;
                    UNREADONLY(true);
                    EnableControl(true);
                }
            }
        }

        private void InPhieuThu(TamUng moi)
        {
            BaoCao.rep_PhieuThuChi_TT107 rep = new BaoCao.rep_PhieuThuChi_TT107();
            rep.TieuDe.Value = "PHIẾU THU";
            rep.xrTableCell11.Text = "Họ và tên người nộp tiền: ";
            rep.NguoiNop.Value = "NGƯỜI NỘP";
            rep.NguoiNhan.Value = "THỦ QUỸ";
            rep.xrTableCell56.Text = txtNhapTBN.Text;
            rep.xrTableCell2.Text = "Mẫu số: C40-BB";
            rep.xrTableCell72.Text = "Mẫu số: C40-BB";
            rep.So.Value = "Số: " + moi.SoHD;
            rep.QuyenSo.Value = "Quyển số: " + moi.QuyenHD;
            rep.No.Value = "Nợ:";
            rep.Co.Value = "Có:";
            rep.SubBand1.Visible = true;
            rep.SubBand2.Visible = false;
            rep.clMaBNhan.Text = "Mã BN: " + txtMaBNhan.Text;
            rep.HoTen.Value = txtNhapTBN.Text;
            rep.DChi.Value = txtDiaChi.Text;
            rep.NoiDung.Value = moi.LyDo;
            string[] ar = DungChung.Bien.FormatString[1].Split(';');
            rep.SoTien.Value = (moi.SoTien.Value).ToString("###,###.00");
            rep.SoThanhChu.Value = DungChung.Ham.DocTienBangChu(moi.SoTien.Value, " đồng");
            rep.NguoiLap.Value = lupCB.Text;
            rep.NgayThang.Value = "Ngày " + moi.NgayThu.Value.Day + " tháng " + moi.NgayThu.Value.Month + " năm " + moi.NgayThu.Value.Year;
            rep.CreateDocument();
            frmIn frm = new frmIn();
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();
        }

        private void lupTrangThai_EditValueChanged(object sender, EventArgs e)
        {
            DSBN();

        }

        private void btnXoaTT_Click(object sender, EventArgs e)
        {
            int _mabn = 0;
            _Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            if (GrvBenhNhan.GetFocusedRowCellValue(MaBN) != null)
            {
                _mabn = Convert.ToInt32(GrvBenhNhan.GetFocusedRowCellValue(MaBN));
                BenhNhan qbn = _Data.BenhNhans.Single(p => p.MaBNhan == _mabn);
                if (qbn != null)
                {
                    #region xóa tt
                    if (qbn.Status == 2 || qbn.Status == 3)// bệnh nhân đã ra viện, tương đương với đã thanh toán 
                    {
                        _mabn = Convert.ToInt32(GrvBenhNhan.GetFocusedRowCellValue(MaBN));
                        VienPhi vp = _Data.VienPhis.Where(p => p.MaBNhan == _mabn).FirstOrDefault();
                        if (vp != null)
                        {
                            DialogResult _result = MessageBox.Show("Bạn có muốn xóa thanh toán của bệnh nhân '" + qbn.TenBNhan + "' không ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (_result == DialogResult.Yes)
                            {

                                //var qnd = _Data.NhapDs.FirstOrDefault(p => p.MaBNhan == _mabn);
                                //if (qnd != null) // bệnh nhân đã xuất dược
                                //{
                                //    //MessageBox.Show("Bệnh nhân đã xuất dược, bạn không thể xóa thanh toán");
                                //    //return;

                                //    //xóa nhapd
                                //    _Data.NhapDs.Remove(qnd);

                                //    //xóa nahpdct
                                //    var xoact = _Data.NhapDcts.Where(p => p.IDNhap == qnd.IDNhap).ToList();
                                //    foreach (var item in xoact)
                                //    {
                                //        _Data.NhapDcts.Remove(item);
                                //    }

                                //    _Data.SaveChanges();
                                //}

                                var qvpct = _Data.VienPhicts.Where(p => p.idVPhi == vp.idVPhi).ToList();
                                foreach (VienPhict vpct in qvpct)
                                {
                                    VienPhict xoa = _Data.VienPhicts.Single(p => p.idVPhict == vpct.idVPhict);
                                    _Data.VienPhicts.Remove(vpct);
                                    _Data.SaveChanges();
                                }
                                _Data.VienPhis.Remove(vp);
                                _Data.SaveChanges();
                                BenhNhan benhnhan = _Data.BenhNhans.Single(p => p.MaBNhan == _mabn);
                                benhnhan.Status = 1;
                                _Data.SaveChanges();

                                var tu = _Data.TamUngs.Where(p => p.MaBNhan == _mabn).ToList();
                                foreach (var a in tu)
                                {
                                    TamUng t = _Data.TamUngs.Single(p => p.IDTamUng == a.IDTamUng);
                                    _Data.TamUngs.Remove(t);
                                }
                                RaVien rv = _Data.RaViens.Where(p => p.MaBNhan == _mabn).FirstOrDefault();
                                if (rv != null)
                                    _Data.RaViens.Remove(rv);
                                _Data.SaveChanges();
                                MessageBox.Show("Xóa thanh toán thành công");
                                DSBN();
                            }
                        }
                        else
                            MessageBox.Show("Bệnh nhân chưa thanh toán, bạn không thể xóa");
                    }
                    #endregion
                }


            }

        }

        private void btnPhieuThu_Click(object sender, EventArgs e)
        {
            int _MaBN = 0;
            TamUng tamung = null;
            if (GrvBenhNhan.GetFocusedRowCellValue(MaBN) != null)
            {
                int ID = 0;
                _MaBN = Convert.ToInt32(GrvBenhNhan.GetFocusedRowCellValue(MaBN));
                tamung = _Data.TamUngs.Where(p => p.MaBNhan == _MaBN && p.PhanLoai == 1).FirstOrDefault();

            }
            if (tamung != null)
            {
                if (DungChung.Bien.MaBV == "30303")
                    usTamThu_TToan._InPhieuThu_01071(tamung.IDTamUng, 1, false);
                else
                    InPhieuThu(tamung);
            }
            else
                MessageBox.Show("Bệnh nhân chưa thanh toán");
        }
        private void GrvChiphi_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (e.RowHandle >= 0)
                e.Info.DisplayText = (e.RowHandle +1).ToString();
            if (e.RowHandle > 9)
                GrvChiphi.IndicatorWidth = 35;
        }

        private void GrvChiphi_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        { 
            double _SLuong = 0;
            int MaKP = 0;
            int _MaDV = 0;
            double _DGia = 0;

            if (GrvChiphi.GetFocusedRowCellValue(DonGia) != null)
            {
                _DGia = Convert.ToDouble(GrvChiphi.GetFocusedRowCellValue(DonGia));
            }
            if (GrvChiphi.GetFocusedRowCellValue(SoLuong) != null && GrvChiphi.GetFocusedRowCellValue(DonGia) != null)
            {
                _SLuong = Convert.ToInt32(GrvChiphi.GetFocusedRowCellValue(SoLuong));
            }
            if (GrvChiphi.GetFocusedRowCellValue(MaDV) != null)
            {
                _MaDV = Convert.ToInt32(GrvChiphi.GetFocusedRowCellValue(MaDV));
            }
            if (lupKP.EditValue != null)
            {
                MaKP = Convert.ToInt32(lupKP.EditValue);
            }

            var ton = DungChung.Ham._checkTon_KD(_Data, _MaDV, MaKP, _DGia, 0, "");
            if (Trangthai == 0)
            {
                groupControl4.Text = "Tồn: " + (ton);
            }
        }
    }
}

