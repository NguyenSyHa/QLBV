using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraEditors.DXErrorProvider;
using DevExpress.XtraGrid;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Localization;
using QLBV.FormThamSo;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace QLBV.FormNhap
{
    public partial class frm_TraDuoc : DevExpress.XtraEditors.XtraForm
    {
        public frm_TraDuoc()
        {
            DevExpress.XtraGrid.Localization.GridLocalizer.Active = new MyGridLocalizer();
            InitializeComponent();
        }

        QLBV_Database.QLBVEntities data;

        List<KPhong> _lKPhong = new List<KPhong>();
        List<KPhong> _lNoiTraDuoc = new List<KPhong>();
        List<NhapDct> listDonCT = new List<NhapDct>();
        List<DichVu> _lDichVu = new List<DichVu>();
        private int _idNhap = 0;
        private int _idNhapct = 0;
        private void frm_NhapDuoc_Load(object sender, EventArgs e)
        {
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            dtNgayNhap.EditValue = DateTime.Now;
            dt_TK_TuNgay.EditValue = DateTime.Now;
            dt_TK_DenNgay.EditValue = DateTime.Now;

            var qKP = (from k in data.KPhongs select k).Where(p => p.PLoai == "Khoa dược").OrderBy(p => p.TenKP).ToList();
            lupMaKP.Properties.DataSource = qKP;
            lupMaKP.EditValue = DungChung.Bien.MaKP;

            lupGrKhoNhap.DataSource = qKP;

            var qNoiTra = data.KPhongs.Where(p => p.TrongBV == 0).OrderBy(p => p.TenKP).ToList();
            lupNoiTraDuoc.Properties.DataSource = qNoiTra;

            lupGrNoiTraDuoc.DataSource = qNoiTra;

            _lKPhong = (from k in data.KPhongs select k).Where(p => p.PLoai == "Khoa dược").OrderBy(p => p.TenKP).ToList();
            _lKPhong.Insert(0, new KPhong { MaKP = 0, TenKP = "Tất cả" });
            lupTK_KhoNhap.Properties.DataSource = _lKPhong;
            lupTK_KhoNhap.EditValue = DungChung.Bien.MaKP;

            _lNoiTraDuoc = data.KPhongs.Where(p => p.TrongBV == 0).OrderBy(p => p.TenKP).ToList();
            _lNoiTraDuoc.Insert(0, new KPhong { MaKP = 0, TenKP = "Tất cả" });
            lupTKNoiTra.Properties.DataSource = _lNoiTraDuoc;

            _lDichVu = (from d in data.DichVus
                        where d.PLoai == 1
                        select d).ToList();
            lupMaDV.DataSource = _lDichVu;
            binNhapDct.DataSource = _listDonCT;
            grc_NhapDct.DataSource = binNhapDct;

            if (DungChung.Bien.PLoaiKP == "Admin")
            {
                lupMaKP.Properties.ReadOnly = false;
                lupMaCB.Properties.ReadOnly = false;
            }
            else
            {
                lupMaKP.Properties.ReadOnly = true;
                lupMaCB.Properties.ReadOnly = true;
            }
            btnThemMoi.Enabled = true;
            simpleButton2.Enabled = false;
            btnReset.Enabled = false;
            btnLuu.Enabled = false;
            btn_luuChiTiet.Enabled = false;
            // btnThemMoi_Click(null, null);
        }
        private void loadDSDon()
        {
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            string soCT = txt_TK_SoCT.Text.Trim();
            int IDNhap = 0;
            bool result = Int32.TryParse(soCT, out IDNhap);
            if (result)
                IDNhap = Convert.ToInt32(soCT);
            int maKP = 0, maKPnx = 0;
            if (lupTK_KhoNhap.EditValue != null)
                maKP = Convert.ToInt32( lupTK_KhoNhap.EditValue);
            if (lupTKNoiTra.EditValue != null)
                maKPnx = Convert.ToInt32( lupTKNoiTra.EditValue);
            DateTime tungay = DungChung.Ham.NgayTu(dt_TK_TuNgay.DateTime);
            DateTime denngay = DungChung.Ham.NgayDen(dt_TK_DenNgay.DateTime);
            List<NhapD> _lNhpD = new List<NhapD>();
            _lNhpD = (from n in data.NhapDs
                      where (maKP== 0 || n.MaKP== (maKP)) && (maKPnx==0 || n.MaKPnx== (maKPnx))
                      select n).Where(p => p.NgayNhap > tungay).Where(p => p.NgayNhap < denngay).Where(p => p.KieuDon == 10 && p.PLoai == 2).Where(p => p.SoCT.Contains(soCT) || p.IDNhap == IDNhap).ToList();
            grc_NhapD.DataSource = _lNhpD.OrderByDescending(p => p.IDNhap).ToList();
        }
        List<NhapDct> _listDonCT = new List<NhapDct>();
        private void loadNhapDct(int idCT)
        {
            _listDonCT = data.NhapDcts.Where(p => p.IDNhap == idCT).ToList();
            foreach (NhapDct nhapdct in _listDonCT)
            {
                nhapdct.SoLuongX = nhapdct.SoLuongX >= 0 ? nhapdct.SoLuongX : (-nhapdct.SoLuongX);
            }
            binNhapDct.DataSource = _listDonCT.OrderBy(p => p.IDNhapct).ToList();
            grc_NhapDct.DataSource = binNhapDct;
        }
        private void lupMaKP_EditValueChanged(object sender, EventArgs e)
        {
            int maKPhong = 0;
            if (lupMaKP.EditValue != null)
                maKPhong = Convert.ToInt32( lupMaKP.EditValue);
            var cb = (from c in data.CanBoes
                      join kp in data.KPhongs on c.MaKP equals kp.MaKP
                      where kp.PLoai== ("Khoa dược")
                      select new { c.MaCB, c.TenCB, kp.MaKP }).Where(p => p.MaKP == maKPhong).ToList();
            lupMaCB.Properties.DataSource = cb;
            lupMaCB.EditValue = DungChung.Bien.MaCB;
            suaCuongDon = true;
        }
        private void setFocusNhapD()
        {
            for (int i = 0; i < grv_NhapD.RowCount; i++)
            {
                if ((grv_NhapD.GetRowCellValue(i, "IDNhap") != null && Convert.ToInt32(grv_NhapD.GetRowCellValue(i, "IDNhap")) == _idNhap))
                {
                    grv_NhapD.FocusedRowHandle = i;
                    break;
                }
            }
        }
        private bool InsUpDon()
        {
            bool rs = false;
            string soCT = "";
            int kp = 0, kpnx = 0;
            string nguoicc = "", canbo = "";
            soCT = txtSoCT.Text;
            nguoicc = txtTenNguoiCC.Text;
            DateTime ngaynhap = dtNgayNhap.DateTime;
            if (lupNoiTraDuoc.EditValue != null)
                kpnx = Convert.ToInt32( lupNoiTraDuoc.EditValue);
            if (lupMaKP.EditValue != null)
                kp =Convert.ToInt32( lupMaKP.EditValue);
            if (lupMaCB.EditValue != null)
                canbo = lupMaCB.EditValue.ToString();

            //thêm mới đơn
            if (_idNhap == 0)
            {
                NhapD don = new NhapD();
                don.SoCT = soCT;
                don.NgayNhap = ngaynhap;
                don.TenNguoiCC = nguoicc;
                don.MaKP = kp;
                don.MaKPnx = kpnx;
                don.MaCB = canbo;
                don.GhiChu = txtGhiChu.Text;
                don.PLoai = 2;
                don.KieuDon = 10;
                data.NhapDs.Add(don);

                if (data.SaveChanges() >= 0)
                {
                    rs = true;
                    loadDSDon();
                    //lấy ra idDon vừa nhập
                    var idNhap = (from d in data.NhapDs
                                  orderby d.IDNhap descending
                                  where d.SoCT== (soCT)
                                  select d.IDNhap
                                   ).ToList();
                    if (idNhap.Count > 0)
                    {
                        _idNhap = Convert.ToInt32(idNhap.First().ToString());

                        //setFocusNhapD();
                    }
                }
            }
            else
            {
                var sua = data.NhapDs.Single(p => p.IDNhap == _idNhap);
                sua.SoCT = soCT;
                sua.NgayNhap = ngaynhap;
                sua.TenNguoiCC = nguoicc;
                sua.MaKP = kp;
                sua.MaKPnx = kpnx;
                sua.MaCB = canbo;
                sua.GhiChu = txtGhiChu.Text;
                if (data.SaveChanges() >= 0)
                {
                    rs = true;                    
                }
            }
            return rs;
        }
        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (checkValidate())
            {
                if (InsUpDon())
                {
                    MessageBox.Show("Lưu thành công");
                    btnLuu.Enabled = false;
                    btnReset.Enabled = false ;
                    btnThemMoi.Enabled = true;
                    simpleButton2.Enabled = false;
                    btn_luuChiTiet.Enabled = false;
                }
                if (lblIDDon.Text == "0")
                    setFocusNhapD();
            }
           
        }
        private bool checkValidate()
        {
            if (lupMaKP.EditValue == null || lupMaKP.EditValue.ToString() == "")
            {
                MessageBox.Show("Bạn chưa chọn khoa phòng");
                lupMaKP.Focus();
                return false;
            }
            else if (lupMaCB.EditValue == null || lupMaCB.EditValue.ToString() == "")
            {
                MessageBox.Show("Bạn chưa chọn cán bộ nhập dược");
                lupMaCB.Focus();
                return false;
            }
            else if (dtNgayNhap.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn ngày nhập dược");
                dtNgayNhap.Focus();
                return false;
            }
            else
                return true;
        }
        private bool checkExist()
        {
            string soCT = txtSoCT.Text;
            if (soCT != "")
            {
                var ct = (from c in data.NhapDs where c.SoCT== (soCT) select c).ToList();
                if (ct.Count > 0)
                    return true;
                else return false;
            }
            else return false;

        }
        //private bool checkNhapDCT(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        //{
        //        GridView view = sender as GridView;           
        //        GridColumn colDonGia = view.Columns["DonGia"];
        //        GridColumn colSoLuongN = view.Columns["SoLuongN"];
        //        GridColumn colThanhTienN = view.Columns["ThanhTienN"];
        //        GridColumn colMaDV = view.Columns["MaDV"]; 
        //        object valMaDV = view.GetRowCellValue(e.RowHandle, colMaDV);
        //        object valSoLuongN = view.GetRowCellValue(e.RowHandle, colSoLuongN);
        //        object valDonGia = view.GetRowCellValue(e.RowHandle, colDonGia);
        //        object valDonGiaCT = view.GetRowCellValue(e.RowHandle, colDonGiaCT);
        //        object valThanhTienN = view.GetRowCellValue(e.RowHandle, colThanhTienN);
        //        object valVAT = view.GetRowCellValue(e.RowHandle, colVAT);


        //        if (valMaDV == null || valMaDV.ToString() == string.Empty)
        //        {
        //            e.Valid = false;
        //            view.SetColumnError(colMaDV, "Bạn chưa chọn loại thuốc");
        //            return false;
        //        }
        //        else if (valDonGia == null || valDonGia.ToString() == string.Empty)
        //        {
        //            e.Valid = false;
        //            view.SetColumnError(colDonGia, "Bạn chưa nhập đơn giá");
        //            view.SetColumnError(colMaDV, "", ErrorType.None);
        //            return false;
        //        }
        //        else if (valDonGiaCT != null && valDonGiaCT.ToString() != string.Empty && Convert.ToDouble((valDonGiaCT).ToString()) < 0)
        //        {

        //            e.Valid = false;
        //            view.SetColumnError(colDonGiaCT, "Đơn giá trước thuế phải là số dương");
        //            view.SetColumnError(colMaDV, "", ErrorType.None);
        //            view.SetColumnError(colDonGia, "", ErrorType.None);

        //            return false;
        //        }
        //        else if (valVAT != null && valVAT.ToString() != string.Empty && Convert.ToInt32((valVAT).ToString()) < 0)
        //        {

        //            e.Valid = false;
        //            view.SetColumnError(colVAT, "VAT phải là số dương");
        //            view.SetColumnError(colMaDV, "", ErrorType.None);
        //            view.SetColumnError(colDonGia, "", ErrorType.None);
        //            view.SetColumnError(colDonGiaCT, "", ErrorType.None);
        //            return false;
        //        }
        //        else if (valSoLuongN == null || valSoLuongN.ToString() == string.Empty)
        //        {
        //            e.Valid = false;
        //            view.SetColumnError(colSoLuongN, "Bạn chưa nhập số lượng");
        //            view.SetColumnError(colMaDV, "", ErrorType.None);
        //            view.SetColumnError(colDonGia, "", ErrorType.None);
        //            view.SetColumnError(colDonGiaCT, "", ErrorType.None);
        //            view.SetColumnError(colVAT, "", ErrorType.None);
        //            return false;
        //        }
        //        else if (valThanhTienN == null || valThanhTienN.ToString() == string.Empty)
        //        {
        //            e.Valid = false;
        //            view.SetColumnError(colThanhTienN, "Bạn chưa nhập thành tiền");
        //            view.SetColumnError(colMaDV, "", ErrorType.None);
        //            view.SetColumnError(colDonGia, "", ErrorType.None);
        //            view.SetColumnError(colDonGiaCT, "", ErrorType.None);
        //            view.SetColumnError(colVAT, "", ErrorType.None);
        //            view.SetColumnError(colSoLuongN, "", ErrorType.None);
        //            return false;
        //        }
        //        else if (Convert.ToDouble((valSoLuongN).ToString()) < 0)
        //        {
        //            e.Valid = false;
        //            view.SetColumnError(colSoLuongN, "số lượng phải là số dương");
        //            view.SetColumnError(colMaDV, "", ErrorType.None);
        //            view.SetColumnError(colDonGia, "", ErrorType.None);
        //            view.SetColumnError(colDonGiaCT, "", ErrorType.None);
        //            view.SetColumnError(colVAT, "", ErrorType.None);
        //            return false;
        //        }
        //        else if (Convert.ToDouble((valDonGia).ToString()) < 0)
        //        {
        //            e.Valid = false;
        //            view.SetColumnError(colDonGia, "đơn giá phải là số dương");
        //            view.SetColumnError(colMaDV, "", ErrorType.None);
        //            view.SetColumnError(colDonGiaCT, "", ErrorType.None);

        //            return false;
        //        }              
        //        else return true;
        //}
        //private void grv_NhapDct_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        //{
        //    int row = grv_NhapDct.FocusedRowHandle;            
        //        if (checkNhapDCT(sender, e))
        //        {
        //            int madv = "", solo = "", donvi = "";
        //            float dongia = 0, soluongn = 0, thanhtienn = 0;
        //            if (grv_NhapDct.GetRowCellValue(row, "MaDV") != null)
        //                maDV = grv_NhapDct.GetRowCellValue(row, "MaDV").ToString();
        //            if (grv_NhapDct.GetRowCellValue(row, "SoLo") != null)
        //                solo = grv_NhapDct.GetRowCellValue(row, "SoLo").ToString();
        //            var dvi = (from d in data.DichVus where d.MaDV== (maDV) select d.DonVi).ToList();
        //            if (dvi.Count() > 0)
        //                donvi = dvi.First().ToString();
        //            if (grv_NhapDct.GetRowCellValue(row, "DonGia") != null)
        //                dongia = (float)Convert.ToDouble(grv_NhapDct.GetRowCellValue(row, "DonGia"));
        //            if (grv_NhapDct.GetRowCellValue(row, "SoLuongN") != null)
        //                soluongn = (float)Convert.ToDouble(grv_NhapDct.GetRowCellValue(row, "SoLuongN"));
        //            if (grv_NhapDct.GetRowCellValue(row, "ThanhTienN") != null)
        //                thanhtienn = (float)Convert.ToDouble(grv_NhapDct.GetRowCellValue(row, "ThanhTienN"));
        //            if (e.RowHandle < 0)//thêm mới
        //            {
        //                NhapDct nhapct = new NhapDct();
        //                nhapct.IDNhap = _idNhap;
        //                nhapct.MaDV = maDV;
        //                nhapct.SoLo = solo;
        //                if (grv_NhapDct.GetRowCellValue(row, "SoDangKy") != null)
        //                    nhapct.SoDangKy = grv_NhapDct.GetRowCellValue(row, "SoDangKy").ToString();
        //                if (grv_NhapDct.GetRowCellValue(row, "HanDung") != null)
        //                    nhapct.HanDung = Convert.ToDateTime(grv_NhapDct.GetRowCellValue(row, "HanDung").ToString());
        //                if (grv_NhapDct.GetRowCellValue(row, "VAT") != null)
        //                    nhapct.VAT = Convert.ToInt32(grv_NhapDct.GetRowCellValue(row, "VAT").ToString());
        //                nhapct.DonVi = donvi;
        //                nhapct.DonGia = dongia;
        //                if (grv_NhapDct.GetRowCellValue(row, "DonGiaCT") != null)
        //                    nhapct.DonGiaCT = (float)Convert.ToDouble(grv_NhapDct.GetRowCellValue(row, "DonGiaCT"));
        //                nhapct.SoLuongN = soluongn;
        //                nhapct.ThanhTienN = thanhtienn;
        //                data.NhapDcts.Add(nhapct);
        //                data.SaveChanges();

        //            }
        //            else // sửa
        //            {
        //                #region sửa
        //                var sua = data.NhapDcts.Single(p => p.IDNhapct == _idNhapct);
        //                sua.MaDV = maDV;
        //                sua.SoLo = solo;
        //                if (grv_NhapDct.GetRowCellValue(row, "SoDangKy") != null)
        //                    sua.SoDangKy = grv_NhapDct.GetRowCellValue(row, "SoDangKy").ToString();
        //                if (grv_NhapDct.GetRowCellValue(row, "HanDung") != null)
        //                    sua.HanDung = Convert.ToDateTime(grv_NhapDct.GetRowCellValue(row, "HanDung").ToString());
        //                sua.DonVi = donvi;
        //                if (grv_NhapDct.GetRowCellValue(row, "VAT") != null)
        //                    sua.VAT = Convert.ToInt32(grv_NhapDct.GetRowCellValue(row, "VAT").ToString());
        //                sua.DonGia = dongia;
        //                if (grv_NhapDct.GetRowCellValue(row, "DonGiaCT") != null)
        //                    sua.DonGiaCT = (float)Convert.ToDouble(grv_NhapDct.GetRowCellValue(row, "DonGiaCT"));
        //                sua.SoLuongN = soluongn;
        //                sua.ThanhTienN = thanhtienn;
        //                data.SaveChanges();


        //                #endregion
        //            }
        //            loadNhapDct(_idNhap);
        //        } 
        //  //  form_close = false;
        //}       
        //private void grv_NhapDct_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        //{
        //    int row = grv_NhapDct.FocusedRowHandle;
        //    if (grv_NhapDct.GetRowCellValue(row, "IDNhapct") != null)
        //        _idNhapct = Convert.ToInt32(grv_NhapDct.GetRowCellValue(row, "IDNhapct").ToString());
        //    else
        //        _idNhapct = 0;
        //}
        private void hplkXoa_Click(object sender, EventArgs e)
        {

        }
        private void btnReset_Click(object sender, EventArgs e)
        {
            // _idNhap = 0;//TH form thêm mới
            if (_idNhap == 0)
            {
                txtSoCT.Text = "";
                txtTenNguoiCC.Text = "";
                txtGhiChu.Text = "";
                lupMaKP.EditValue = 0;
                //grv_NhapDct.OptionsView.NewItemRowPosition = NewItemRowPosition.None;
            }
            else
            {
                grv_NhapD_FocusedRowChanged(null, null);
                //grv_NhapDct.OptionsView.NewItemRowPosition = NewItemRowPosition.Bottom;
            }


        }
        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_xoa_Click(object sender, EventArgs e)
        {
            if (grv_NhapDct.GetFocusedRowCellValue("IDNhapct") == null)
            {
                this.binNhapDct.CancelEdit();
            }
            else
            {
                if (_idNhapct > 0)
                {
                    DialogResult dialogResult = MessageBox.Show("Xác nhận xóa ?", "Xác nhận xóa", MessageBoxButtons.YesNo);

                    if (dialogResult == DialogResult.Yes)
                    {

                        var xoa = data.NhapDcts.Single(p => p.IDNhapct == _idNhapct);
                        data.NhapDcts.Remove(xoa);
                        data.SaveChanges();
                        loadNhapDct(_idNhap);


                    }
                }
            }
        }
        //private void grv_NhapDct_LayoutUpgrade(object sender, DevExpress.Utils.LayoutUpgadeEventArgs e)
        //{
        //    GridView view = (GridView)sender;
        //    //if (_idNhap == 0)
        //    //    view.OptionsView.NewItemRowPosition = NewItemRowPosition.None;
        //    //else
        //    //  view.OptionsView.NewItemRowPosition = NewItemRowPosition.Bottom;
        //}
        private void lupMaDV_EditValueChanged(object sender, EventArgs e)
        {
            int maDV = 0;
            string donvi = "";
            string dongia = "";
            int row = grv_NhapDct.FocusedRowHandle;
            if (grv_NhapDct.GetRowCellValue(row, "MaDV") != null)
                maDV = Convert.ToInt32( grv_NhapDct.GetRowCellValue(row, "MaDV"));
            var dv = (from d in data.DichVus
                      where d.MaDV == maDV
                      select new { d.DonVi, d.DonGia }).ToList();
            if (dv.Count > 0)
            {
                if (dv.First().DonVi != null)
                {
                    donvi = dv.First().DonVi.ToString();
                    grv_NhapDct.SetRowCellValue(row, "colDonVi1", donvi);
                }
                if (dv.First().DonGia != null)
                {
                    dongia = dv.First().DonGia.ToString();
                    grv_NhapDct.SetRowCellValue(row, "colDonGia", dongia);
                }
            }

        }
        //private void grv_NhapDct_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        //{
        //    if (e.Column.Name == "colTenDV")
        //    {
        //       int madv = e.Value.ToString();
        //       string donvi = "";
        //       string dongia = "";
        //       var dv = (from d in data.DichVus
        //                 where d.MaDV== (maDV)
        //                 select new { d.DonVi, d.DonGia }).ToList();
        //       if (dv.Count > 0)
        //       {
        //           donvi = dv.First().DonVi.ToString();
        //           dongia = dv.First().DonGia.ToString();  
        //           grv_NhapDct.SetFocusedRowCellValue(colDonGia, dongia);
        //           grv_NhapDct.SetFocusedRowCellValue(colDonVi, donvi);
        //       }
        //    }
        //    else if (e.Column.Name == "colDonGia")
        //    {
        //        if (grv_NhapDct.GetFocusedRowCellValue(colSoLuongN) != null && grv_NhapDct.GetFocusedRowCellValue(colSoLuongN).ToString() != "" && (e.Value.ToString() != "") && (e.Value != null))
        //        {
        //            double thanhtien = Convert.ToDouble(grv_NhapDct.GetFocusedRowCellValue(colSoLuongN).ToString()) * Convert.ToDouble(e.Value.ToString()) ;
        //            grv_NhapDct.SetFocusedRowCellValue(colThanhTienN, thanhtien);
        //        }

        //    }
        //    else if (e.Column.Name == "colSoLuongN")
        //    {
        //        if (grv_NhapDct.GetFocusedRowCellValue(colDonGia) != null && grv_NhapDct.GetFocusedRowCellValue(colDonGia).ToString() != "" && (e.Value.ToString() != "") && (e.Value != null))
        //        {
        //            double thanhtien = Convert.ToDouble(grv_NhapDct.GetFocusedRowCellValue(colDonGia).ToString()) * Convert.ToDouble(e.Value.ToString());
        //            grv_NhapDct.SetFocusedRowCellValue(colThanhTienN, thanhtien);
        //        }

        //    }
        //    else if (e.Column.Name == "colDonGiaCT")
        //    {
        //        if (grv_NhapDct.GetFocusedRowCellValue(colVAT) != null && Convert.ToDouble(e.Value) != 0 && (e.Value != null))
        //        {                    
        //                double dongia = (Convert.ToDouble(e.Value) * (100 + Convert.ToInt32(grv_NhapDct.GetFocusedRowCellValue(colVAT)))) / 100;
        //                grv_NhapDct.SetFocusedRowCellValue(colDonGia, dongia);                    
        //        }

        //    }
        //    else if (e.Column.Name == "colVAT")
        //    {
        //        if (grv_NhapDct.GetFocusedRowCellValue(colDonGiaCT) != null && (e.Value != null))
        //        {
        //            double dongia = (Convert.ToDouble(grv_NhapDct.GetFocusedRowCellValue(colDonGiaCT)) * (100 +Convert.ToInt32(e.Value)))/100;
        //            grv_NhapDct.SetFocusedRowCellValue(colDonGia, dongia);
        //        }

        //    }


        //}
        private void grv_NhapD_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (suadon)
            {
                DialogResult dialogResult = MessageBox.Show("Đơn chưa được lưu. Bạn có muốn lưu đơn?", "Xác nhận lưu", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    btn_luuChiTiet_Click(null, null);
                }
                else
                {
                    int row = grv_NhapD.FocusedRowHandle;
                    if (grv_NhapD.GetRowCellValue(row, "IDNhap") != null)
                    {
                        _idNhap = Convert.ToInt32(grv_NhapD.GetRowCellValue(row, "IDNhap").ToString());
                        lblIDDon.Text = _idNhap.ToString();
                    }
                    else
                    {
                        _idNhap = 0;
                        lblIDDon.Text = "0";
                       
                    }
                }
            }
            else
            {
                int row = grv_NhapD.FocusedRowHandle;
                if (grv_NhapD.GetRowCellValue(row, "IDNhap") != null)
                {
                    _idNhap = Convert.ToInt32(grv_NhapD.GetRowCellValue(row, "IDNhap").ToString());
                    lblIDDon.Text = _idNhap.ToString();
                }
                else
                {
                    _idNhap = 0;
                    lblIDDon.Text = "0";
                   
                }
            }
            suaCuongDon = false;
        }
        private void FillDataNhapD(int _idNhap)
        {
            if (_idNhap > 0)
            {
                var don = data.NhapDs.Single(p => p.IDNhap == _idNhap);
                txtSoCT.Text = don.SoCT;
                txtTenNguoiCC.Text = don.TenNguoiCC;
                txtGhiChu.Text = don.GhiChu;
                //  lupMaCC.EditValue = don.MaCC;
                lupMaKP.EditValue = don.MaKP;
                lupNoiTraDuoc.EditValue = don.MaKPnx;
                dtNgayNhap.EditValue = don.NgayNhap;
            }
            else
            {
                txtSoCT.Text = "";
                txtTenNguoiCC.Text = "";
                txtGhiChu.Text = "";
                lupMaKP.EditValue = DungChung.Bien.MaKP;
                lupNoiTraDuoc.EditValue = null;
                lupMaCB.EditValue = DungChung.Bien.MaCB;
                dtNgayNhap.EditValue = DateTime.Now;
            }

        }
        private void txt_sSoCT_EditValueChanged(object sender, EventArgs e)
        {
            loadDSDon();
        }
        private void txtsNCC_EditValueChanged(object sender, EventArgs e)
        {
            loadDSDon();
        }
        private void dtsTuNgay_EditValueChanged(object sender, EventArgs e)
        {
            loadDSDon();
        }
        private void dtsDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            loadDSDon();
        }
        private void grv_NhapD_Click(object sender, EventArgs e)
        {

        }
        private void btn_xoadon_Click(object sender, EventArgs e)
        {
            if (grv_NhapD.GetFocusedRowCellValue("IDNhap") != null)
            {

                if (_idNhap > 0)
                {
                    var dsct = data.NhapDcts.Where(p => p.IDNhap == _idNhap).ToList();
                    if (dsct.Count <= 0)
                    {
                        DialogResult dialogResult = MessageBox.Show("Xác nhận xóa ?", "Xác nhận xóa", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            var xoa = data.NhapDs.Single(p => p.IDNhap == _idNhap);
                            data.NhapDs.Remove(xoa);
                            data.SaveChanges();
                            loadDSDon();
                            //loadNhapDct(_idNhap);
                        }
                    }
                    else
                    {
                        string ms = "Đơn " + _idNhap.ToString() + " có " + dsct.Count.ToString() + " thuốc chi tiết. Bạn có chắc chắn xóa?";
                        DialogResult dialogResult = MessageBox.Show(ms, "Xác nhận xóa", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            var valXoact = data.NhapDcts.Where(p => p.IDNhap == _idNhap).ToList();
                            foreach (var n in valXoact)
                                data.NhapDcts.Remove(n);
                            var xoa = data.NhapDs.Single(p => p.IDNhap == _idNhap);
                            data.NhapDs.Remove(xoa);
                            data.SaveChanges();                            
                            loadDSDon();
                            loadNhapDct(_idNhap);
                        }
                    }
                }
            }
        }
        private void btnThemMoi_Click(object sender, EventArgs e)
        {
            _idNhap = 0;//TH form thêm mới  
            lblIDDon.Text = "0";
            txtSoCT.Text = "";
            txtTenNguoiCC.Text = "";
            txtGhiChu.Text = "";
            lupMaKP.EditValue = DungChung.Bien.MaKP;
            lupMaCB.EditValue = DungChung.Bien.MaCB;
            lupNoiTraDuoc.EditValue = null;
            btnLuu.Enabled = true;
            btnReset.Enabled = true;
            btnThemMoi.Enabled = false;
            simpleButton2.Enabled = false;
            btn_luuChiTiet.Enabled = true;

            // loadNhapDct(_idNhap);
        }

        private void lupTK_KhoNhap_EditValueChanged(object sender, EventArgs e)
        {
            loadDSDon();
        }

        private void lupTKNoiTra_EditValueChanged(object sender, EventArgs e)
        {
            loadDSDon();
        }

        private void grc_NhapDct1_Click(object sender, EventArgs e)
        {

        }
        private bool checkNhapDCT_setColumnError(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            GridView view = sender as GridView;
            GridColumn colDonGia = view.Columns["DonGia"];
            GridColumn colSoLuong = view.Columns["SoLuongX"];
            GridColumn colMaDV = view.Columns["MaDV"];
            object valMaDV = view.GetRowCellValue(e.RowHandle, colMaDV);
            object valSoLuong = view.GetRowCellValue(e.RowHandle, colSoLuong);
            object valDonGia = view.GetRowCellValue(e.RowHandle, colDonGia);
            if (valMaDV == null || valMaDV.ToString() == string.Empty)
            {
                e.Valid = false;
                view.SetColumnError(colMaDV, "Bạn chưa chọn loại thuốc");
                e.ErrorText = "Bạn muốn sửa lại dữ liệu cho đúng ?";

                return false;
            }
            else if (valDonGia == null || valDonGia.ToString() == string.Empty)
            {
                e.Valid = false;
                view.SetColumnError(colDonGia, "Bạn chưa nhập đơn giá");
                e.ErrorText = "Bạn muốn sửa lại dữ liệu cho đúng ?";
                view.SetColumnError(colMaDV, "", ErrorType.None);

                return false;
            }

            else if (valSoLuong == null || valSoLuong.ToString() == string.Empty)
            {
                e.Valid = false;
                view.SetColumnError(colSoLuong, "Bạn chưa nhập số lượng");
                e.ErrorText = "Bạn muốn sửa lại dữ liệu cho đúng ?";
                view.SetColumnError(colMaDV, "", ErrorType.None);
                view.SetColumnError(colDonGia, "", ErrorType.None);
                return false;
            }

            else if (Convert.ToDouble((valSoLuong).ToString()) <= 0)
            {
                e.Valid = false;
                view.SetColumnError(colSoLuong, "số lượng phải lớn hơn 0");
                e.ErrorText = "Bạn muốn sửa lại dữ liệu cho đúng ?";
                view.SetColumnError(colMaDV, "", ErrorType.None);
                view.SetColumnError(colDonGia, "", ErrorType.None);

                return false;
            }
            else if (Convert.ToDouble((valDonGia).ToString()) <= 0)
            {
                e.Valid = false;
                view.SetColumnError(colDonGia, "đơn giá phải lơn hơn 0");//
                e.ErrorText = "Bạn muốn sửa lại dữ liệu cho đúng ?";
                view.SetColumnError(colMaDV, "", ErrorType.None);
                return false;
            }
            else
            {
                return true;
            }
        }
        bool suadon = false;// xác định có sửa đơn chi tiết hay ko
        bool suaCuongDon = false; // biến xác định thay đổi thông tin đơn
        private void grv_NhapDct_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            if (grv_NhapDct.GetRow(grv_NhapDct.FocusedRowHandle) != null && checkNhapDCT_setColumnError(sender, e))
                suadon = true;
        }

        private void btn_LuuDct_Click(object sender, EventArgs e)
        {
            if (_idNhap > 0)
            {
                listDonCT = new List<NhapDct>();
                #region kiểm tra
                int Err_count = 0;
                for (int i = 0; i < grv_NhapDct.RowCount; i++)
                {
                    int _maDV = 0;
                       string _solo = "", _donvi = "", _sodangky = "";
                    float _dongia = 0, _soluong = 0, _thanhtien = 0;
                    string _handung = "";

                    if (grv_NhapDct.GetRowCellValue(i, "MaDV") == null || grv_NhapDct.GetRowCellValue(i, "MaDV").ToString() != "")
                    {
                        Err_count++;
                        continue;
                    }
                    else if (grv_NhapDct.GetRowCellValue(i, "DonGia") == null || grv_NhapDct.GetRowCellValue(i, "DonGia").ToString() != "")
                    {
                        Err_count++;
                        continue;
                    }
                    else if (grv_NhapDct.GetRowCellValue(i, "SoLuongX") == null || grv_NhapDct.GetRowCellValue(i, "SoLuongX").ToString() != "")
                    {
                        Err_count++;
                        continue;
                    }
                    else
                    {
                        _maDV = Convert.ToInt32( grv_NhapDct.GetRowCellValue(i, "MaDV"));
                        _dongia = (float)Convert.ToDouble(grv_NhapDct.GetRowCellValue(i, "DonGia"));
                        _soluong = (float)Convert.ToDouble(grv_NhapDct.GetRowCellValue(i, "SoLuongX"));
                        _thanhtien = (float)Convert.ToDouble(grv_NhapDct.GetRowCellValue(i, "DonGia")) * (float)Convert.ToDouble(grv_NhapDct.GetRowCellValue(i, "SoLuongX"));
                        if (grv_NhapDct.GetRowCellValue(i, "SoLo") != null)
                            _solo = grv_NhapDct.GetRowCellValue(i, "SoLo").ToString();
                        var dvi = (from d in _lDichVu where d.MaDV== (_maDV) select d.DonVi).ToList();
                        if (dvi.Count() > 0)
                            _donvi = dvi.First().ToString();
                        if (grv_NhapDct.GetRowCellValue(i, "SoDangKy") != null)
                            _sodangky = grv_NhapDct.GetRowCellValue(i, "SoDangKy").ToString();
                        if (grv_NhapDct.GetRowCellValue(i, "HanDung") != null)
                            _handung = (grv_NhapDct.GetRowCellValue(i, "HanDung").ToString());

                        NhapDct nhapct = new NhapDct();
                        nhapct.IDNhap = _idNhap;
                        nhapct.MaDV = _maDV;
                        nhapct.SoLo = _solo;
                        if (_handung != "")
                            nhapct.HanDung = Convert.ToDateTime(_handung);
                        nhapct.SoDangKy = _sodangky;
                        nhapct.DonVi = _donvi;
                        nhapct.DonGia = _dongia;
                        nhapct.SoLuongX = _soluong >= 0 ? (-_soluong) : _soluong;
                        nhapct.ThanhTienX = _thanhtien >= 0 ? _thanhtien : (-_thanhtien);
                        nhapct.SoLuongSD = 0;
                        nhapct.ThanhTienSD = 0;
                        nhapct.SoLuongN = 0;
                        nhapct.ThanhTienN = 0;
                        nhapct.SoLuongDY = 0;
                        nhapct.ThanhTienDY = 0;
                        nhapct.SoLuongKK = 0;
                        nhapct.ThanhTienKK = 0;
                        _listDonCT.Add(nhapct);
                    }

                }

                #endregion kiểm tra
                if (Err_count > 0)
                {
                    DialogResult dialogResult = MessageBox.Show("Có " + Err_count.ToString() + " đơn chi tiết chưa nhập đúng định dạng. Bạn có chắc chắn lưu ?", "Xác nhận thay đổi", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                        var toRemove = data.NhapDcts.Where(p => p.IDNhap == _idNhap);
                        foreach (var item in toRemove)
                            data.NhapDcts.Remove(item);
                        foreach (var item in _listDonCT)
                            data.NhapDcts.Add(item);
                        if (data.SaveChanges() >= 0)
                        {
                            MessageBox.Show("Lưu thành công !");
                            loadNhapDct(_idNhap);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Bạn chưa chọn đơn để thêm mới hoặc sửa đơn chi tiết");
            }
            suadon = false;
        }

        private void grv_NhapDct_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            int row = e.RowHandle;

            if (e.Column.Name == "colXoa")
            {
                int idct = 0;

                if (grv_NhapDct.GetRowCellValue(row, colIDNhapct) != null)
                {
                    idct = Convert.ToInt32(grv_NhapDct.GetRowCellValue(row, colIDNhapct).ToString());
                    if (idct > 0)
                    {
                        if (true)
                        {
                            DialogResult dialogResult = MessageBox.Show("Bạn có muốn xóa đơn chi tiết này không ?", "Xác nhận xóa", MessageBoxButtons.YesNo);
                            if (dialogResult == DialogResult.Yes)
                            {
                                //var donct = data.NhapDcts.Single(p => p.IDNhapct == idct);
                                //data.Remove(donct);
                                // data.SaveChanges();
                                GridView view = sender as GridView;
                                view.DeleteRow(row);
                                suadon = true;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Bạn không có quyền xóa đơn chi tiết trong đơn này");
                        }
                    }
                    else
                    {
                        GridView view = sender as GridView;
                        view.DeleteRow(row);
                    }
                }
            }
            else if (e.Column.Name == "colDonGia")
            {
                int maDV = 0;
                double dongia = 0;

                if (grv_NhapDct.GetRowCellValue(row, colTenDV) != null)
                    maDV = Convert.ToInt32( grv_NhapDct.GetRowCellValue(row, colTenDV));
                if (grv_NhapDct.GetRowCellValue(row, colDonGia) != null)
                    dongia = Convert.ToDouble(grv_NhapDct.GetRowCellValue(row, colDonGia));
                if (maDV != 0)
                {
                    var nhapdct_dg = (from dct in data.NhapDcts
                                      join d in data.NhapDs on dct.IDNhap equals d.IDNhap
                                      where dct.MaDV== maDV && (d.PLoai == 1 || (d.PLoai == 2 && d.KieuDon == 10))
                                      select new { dct.DonGia }).Distinct().ToList();
                    cboDonGia.Items.Clear();
                    if (nhapdct_dg.Count > 0)
                    {
                        foreach (var n in nhapdct_dg)
                        {
                            if (n.DonGia != null)
                                cboDonGia.Items.Add(n.DonGia);

                        }
                        if (dongia != 0)
                            grv_NhapDct.SetRowCellValue(row, colDonGia, dongia);
                        else
                        {
                            if (cboDonGia.Items.Count > 0)
                            {
                                grv_NhapDct.SetRowCellValue(row, colDonGia, cboDonGia.Items[0]);
                            }
                        }

                    }

                }
            }
        }

        private void grv_NhapDct_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            double dongia1 = 0, soluong = 0;
            int maDV = 0;
            string donvi = "";
            string dongia = "";
            if (e.Column.Name == "colTenDV")
            {
                if (e.Value != null)
                    maDV = Convert.ToInt32( e.Value);
                var dv = (from d in _lDichVu
                          where d.MaDV==maDV
                          select new { d.DonVi, d.DonGia }).ToList();
                var nhapdct_dg = (from dct in data.NhapDcts
                                  join d in data.NhapDs on dct.IDNhap equals d.IDNhap
                                  where dct.MaDV==maDV && (d.PLoai == 1 || (d.PLoai == 2 && d.KieuDon == 10))
                                  select new { dct.DonGia }).Distinct().ToList();
                if (dv.Count > 0)
                {
                    if (dv.First().DonVi != null)
                        donvi = dv.First().DonVi.ToString();
                    cboDonGia.Items.Clear();
                    if (nhapdct_dg.Count > 0)
                    {
                        foreach (var n in nhapdct_dg)
                        {
                            if (n.DonGia != null)
                                cboDonGia.Items.Add(n.DonGia);
                        }

                        if (cboDonGia.Items.Count > 0)
                        {
                            grv_NhapDct.SetRowCellValue(e.RowHandle, colDonGia, cboDonGia.Items[0]);
                        }

                    }

                    grv_NhapDct.SetFocusedRowCellValue(colDonVi1, donvi);
                    grv_NhapDct.SetFocusedRowCellValue(colSoLuong, null);
                }
            }
            else if (e.Column.Name == "colDonGia")
            {
                try
                {
                    if (e.Value != null)
                    {
                        dongia1 = Convert.ToDouble(e.Value);
                        if (grv_NhapDct.GetRowCellValue(e.RowHandle, colSoLuong) != null)
                        {
                            soluong = Convert.ToDouble(grv_NhapDct.GetRowCellValue(e.RowHandle, colSoLuong));
                            grv_NhapDct.SetRowCellValue(e.RowHandle, colThanhTien, soluong * dongia1);
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }
            else if (e.Column.Name == "colSoLuong")
            {
                try
                {
                    if (e.Value != null)
                    {
                        soluong = Convert.ToDouble(e.Value);
                        if (grv_NhapDct.GetRowCellValue(e.RowHandle, colDonGia) != null)
                        {
                            dongia1 = Convert.ToDouble(grv_NhapDct.GetRowCellValue(e.RowHandle, colDonGia));
                            grv_NhapDct.SetRowCellValue(e.RowHandle, colThanhTien, soluong * dongia1);
                        }
                    }
                    else
                    {
                        grv_NhapDct.SetRowCellValue(e.RowHandle, colThanhTien, null);
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }

        private void grc_NhapDct_ProcessGridKey(object sender, KeyEventArgs e)
        {
            GridControl grid = sender as GridControl;
            GridView view = grid.FocusedView as GridView;
            if (e.KeyCode == Keys.F9)
            {

                if (view.FocusedColumn.VisibleIndex == 1 && view.IsNewItemRow(view.FocusedRowHandle))
                {
                    //frm_DsMaDV frm = new frm_DsMaDV();
                    //frm.passMaDV = new frm_DsMaDV.PassMaDV(PassData);
                    //frm.ShowDialog();
                }
            }
            else if (e.KeyCode == Keys.Tab)
            {
                if (view.IsEditing)
                    view.CloseEditor();
                grid.SelectNextControl(grid, e.Modifiers == Keys.None, false, false, true);
                e.Handled = true;
                btn_luuChiTiet.Focus();
            }

        }
        private void PassData(int madv)
        {
            if (grv_NhapDct.IsNewItemRow(grv_NhapDct.FocusedRowHandle) && grv_NhapDct.GetRow(grv_NhapDct.FocusedRowHandle) == null)
            {
                grv_NhapDct.AddNewRow();
            }
            grv_NhapDct.SetFocusedRowCellValue("MaDV", madv);
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void InsDetails()
        {
            _listDonCT = new List<NhapDct>();
            #region kiểm tra
            int Err_count = 0;
            for (int i = 0; i < grv_NhapDct.RowCount; i++)
            {
                if (grv_NhapDct.GetRow(i) != null)
                {
                    int _maDV = 0;
                    string _solo = "", _donvi = "", _sodangky = "";
                    float _dongia = 0, _soluong = 0, _thanhtien = 0;
                    string _handung = "";

                    if (grv_NhapDct.GetRowCellValue(i, "MaDV") == null)
                    {
                        Err_count++;
                        continue;
                    }
                    else if (grv_NhapDct.GetRowCellValue(i, "DonGia") == null || grv_NhapDct.GetRowCellValue(i, "DonGia").ToString() == "")
                    {
                        Err_count++;
                        continue;
                    }
                    else if (grv_NhapDct.GetRowCellValue(i, "SoLuongX") == null || grv_NhapDct.GetRowCellValue(i, "SoLuongX").ToString() == "")
                    {
                        Err_count++;
                        continue;
                    }
                    else
                    {
                        _maDV = Convert.ToInt32( grv_NhapDct.GetRowCellValue(i, "MaDV"));
                        _dongia = (float)Convert.ToDouble(grv_NhapDct.GetRowCellValue(i, "DonGia"));
                        _soluong = (float)Convert.ToDouble(grv_NhapDct.GetRowCellValue(i, "SoLuongX"));
                        _thanhtien = (float)Convert.ToDouble(grv_NhapDct.GetRowCellValue(i, "DonGia")) * (float)Convert.ToDouble(grv_NhapDct.GetRowCellValue(i, "SoLuongX"));
                        if (grv_NhapDct.GetRowCellValue(i, "SoLo") != null)
                            _solo = grv_NhapDct.GetRowCellValue(i, "SoLo").ToString();
                        var dvi = (from d in _lDichVu where d.MaDV== (_maDV) select d.DonVi).ToList();
                        if (dvi.Count() > 0)
                            _donvi = dvi.First().ToString();
                        if (grv_NhapDct.GetRowCellValue(i, "SoDangKy") != null)
                            _sodangky = grv_NhapDct.GetRowCellValue(i, "SoDangKy").ToString();
                        if (grv_NhapDct.GetRowCellValue(i, "HanDung") != null)
                            _handung = (grv_NhapDct.GetRowCellValue(i, "HanDung").ToString());

                        NhapDct nhapct = new NhapDct();
                        nhapct.IDNhap = _idNhap;
                        nhapct.MaDV = _maDV;
                        nhapct.SoLo = _solo;
                        if (_handung != "")
                            nhapct.HanDung = Convert.ToDateTime(_handung);
                        nhapct.SoDangKy = _sodangky;
                        nhapct.DonVi = _donvi;
                        nhapct.DonGia = _dongia;
                        nhapct.SoLuongX = _soluong >= 0 ? (-_soluong) : _soluong;
                        nhapct.ThanhTienX = _thanhtien >= 0 ? _thanhtien : (-_thanhtien);
                        nhapct.SoLuongSD = 0;
                        nhapct.ThanhTienSD = 0;
                        nhapct.SoLuongN = 0;
                        nhapct.ThanhTienN = 0;
                        nhapct.SoLuongDY = 0;
                        nhapct.ThanhTienDY = 0;
                        nhapct.SoLuongKK = 0;
                        nhapct.ThanhTienKK = 0;
                        _listDonCT.Add(nhapct);
                    }
                }
            }

            #endregion kiểm tra

            if (Err_count > 0)
            {
                DialogResult dialogResult = MessageBox.Show("Có " + Err_count.ToString() + " đơn chi tiết chưa nhập đúng định dạng. Bạn có chắc chắn lưu ?", "Xác nhận thay đổi", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                    var toRemove = data.NhapDcts.Where(p => p.IDNhap == _idNhap);
                    foreach (var item in toRemove)
                        data.NhapDcts.Remove(item);
                    foreach (var item in _listDonCT)
                        data.NhapDcts.Add(item);
                    if (data.SaveChanges() >= 0)
                    {
                        MessageBox.Show("Lưu thành công !");
                        loadNhapDct(_idNhap);
                    }
                }
            }
            else
            {
                data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                var toRemove = data.NhapDcts.Where(p => p.IDNhap == _idNhap);
                foreach (var item in toRemove)
                    data.NhapDcts.Remove(item);
                foreach (var item in _listDonCT)
                    data.NhapDcts.Add(item);
                if (data.SaveChanges() >= 0)
                {
                    MessageBox.Show("Lưu thành công !");
                    loadNhapDct(_idNhap);
                }
            }
        }
        private void btn_luuChiTiet_Click(object sender, EventArgs e)
        {

            if (_idNhap >= 0)
            {
                if (suaCuongDon)
                {
                    DialogResult dialogResult = MessageBox.Show(" Bạn chưa lưu đơn, bạn có muốn lưu lại đơn này không ?", "Thêm đơn", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        if (checkValidate())
                        {
                            if (!InsUpDon())
                                MessageBox.Show("Lỗi lưu đơn");
                            else
                            {
                                InsDetails();
                            }
                        }
                    }
                }
                else
                    if (checkValidate())
                        InsDetails();
            }
            else
            {
                MessageBox.Show("Bạn chưa chọn đơn để thêm mới hoặc sửa đơn chi tiết");
            }
            suadon = false;
            suaCuongDon = false;
            loadDSDon();
        }

        private void lblIDDon_TextChanged(object sender, EventArgs e)
        {
            loadNhapDct(Convert.ToInt32(lblIDDon.Text));
            FillDataNhapD(Convert.ToInt32(lblIDDon.Text));
            if (Convert.ToInt32(lblIDDon.Text) > 0)
            {
                btnLuu.Enabled = false;
                btnReset.Enabled = false;
                btnThemMoi.Enabled = true;
                simpleButton2.Enabled = true;
                btn_luuChiTiet.Enabled = false;
            }
            else
            {
                btnLuu.Enabled = false;
                btnReset.Enabled = false;
                btnThemMoi.Enabled = true;
                simpleButton2.Enabled = false;
                btn_luuChiTiet.Enabled = false;
            }
        }

        private void frm_NhapDuoc_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                this.Dispose();
        }

        private void grc_NhapD_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {

                GridControl grid = sender as GridControl;
                GridView view = grid.FocusedView as GridView;
                //if ((e.Modifiers == Keys.None && view.IsLastRow && view.FocusedColumn.VisibleIndex == view.VisibleColumns.Count - 1)
                //    || (e.Modifiers == Keys.Shift && view.IsFirstRow && view.FocusedColumn.VisibleIndex == 0))
                // {
                if (view.IsEditing)
                    view.CloseEditor();
                grid.SelectNextControl(grid, e.Modifiers == Keys.None, false, false, true);
                e.Handled = true;
                txt_TK_SoCT.Focus();
                // }

            }
        }

        private void txtSoCT_TextChanged(object sender, EventArgs e)
        {
            suaCuongDon = true;
        }

        private void dtNgayNhap_EditValueChanged(object sender, EventArgs e)
        {
            suaCuongDon = true;
        }

        private void lupMaCB_EditValueChanged(object sender, EventArgs e)
        {
            suaCuongDon = true;
        }

        private void lupNoiTraDuoc_EditValueChanged(object sender, EventArgs e)
        {
            suaCuongDon = true;
        }

        private void txtTenNguoiCC_EditValueChanged(object sender, EventArgs e)
        {
            suaCuongDon = true;
        }

        private void txtGhiChu_TextChanged(object sender, EventArgs e)
        {
            suaCuongDon = true;
        }

        //sửa đơn
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            btnLuu.Enabled = true;
            btnReset.Enabled = true;
            btnThemMoi.Enabled = false;
            simpleButton2.Enabled = false;
            btn_luuChiTiet.Enabled = true;
        }





        //private void grv_NhapDct_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        //{
        //    e.ExceptionMode = ExceptionMode.DisplayError;
        //}
    }
    public class MyGridLocalizer : GridLocalizer
    {
        public override string GetLocalizedString(GridStringId id)
        {
            if (id == GridStringId.ColumnViewExceptionMessage) return string.Empty;
            return base.GetLocalizedString(id);
        }
    }
}