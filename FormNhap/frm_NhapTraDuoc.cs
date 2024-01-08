using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using DevExpress.XtraEditors.DXErrorProvider;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Localization;
using DevExpress.XtraEditors.Controls;
using QLBV.FormThamSo;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid;
using DevExpress.XtraEditors.Repository;
using DevExpress.Utils.Win;
using DevExpress.XtraEditors.Popup;

namespace QLBV.FormNhap
{   
    public partial class frm_NhapTraDuoc : DevExpress.XtraEditors.XtraForm
    {
        public frm_NhapTraDuoc()
        {
            // DevExpress.XtraGrid.Localization.GridLocalizer.Active = new MyGridLocalizer();
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities data;
        List<KPhong> _lKPhong = new List<KPhong>();
        List<KPhong> _lNoiTraDuoc = new List<KPhong>();
        List<NhapDct> _listDonCT = new List<NhapDct>();
        private int _idNhap = 0;
        private int rowNhapct = -1;      
        private void frm_HoaDonThuoc_Load(object sender, EventArgs e)
        {
            //  DungChung.Bien.PLoaiKP = "Admin";
            lblIDDon.Text = "0";
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            dtsTuNgay.EditValue = DateTime.Now.AddDays(-7);
            dtsDenNgay.EditValue = DateTime.Now;
            var ncc = data.NhaCCs.OrderBy(p => p.TenCC).ToList();
            lupGrNCC.DataSource = ncc;
            _lKPhong = (from k in data.KPhongs select k).Where(p => p.PLoai == "Khoa dược").OrderBy(p => p.TenKP).ToList();
            lupGrKPhong.DataSource = _lKPhong;
            lupKhoNhap.Properties.DataSource = _lKPhong;
            lupKhoNhap.EditValue = DungChung.Bien.MaKP;
            _lNoiTraDuoc = data.KPhongs.Where(p => p.TrongBV == 0).OrderBy(p => p.TenKP).ToList();
            lupNoiTraDuoc.Properties.DataSource = _lNoiTraDuoc;
            lupGrNoiTraDuoc.DataSource = _lNoiTraDuoc;
            //hiển thị danh sách cán bộ
            var cb = (from c in data.CanBoes
                      join kp in data.KPhongs on c.MaKP equals kp.MaKP
                      where kp.PLoai== ("Khoa dược")
                      select new { c.MaCB, c.TenCB, kp.MaKP }).ToList();
            lupGrCanBo.DataSource = cb;
            _lDichVu = (from d in data.DichVus
                        where d.PLoai == 1
                        select d).ToList();
            lupMaDV.DataSource = _lDichVu;
            binNhapDct.DataSource = _listDonCT;
            grc_NhapDct.DataSource = binNhapDct;
            grv_NhapDct.OptionsView.NewItemRowPosition = NewItemRowPosition.Bottom;
            setEditTable(false);
            setEditNhapdct(false);  
            _strMaCB = DungChung.Bien.MaCB;
           
        }
        List<DichVu> _lDichVu = new List<DichVu>();

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
        private void loadDSDon()
        {

            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            string soCT = txt_sSoCT.Text.Trim();
            int IDNhap = 0;
            bool result = Int32.TryParse(soCT, out IDNhap);
            if (result)
                IDNhap = Convert.ToInt32(soCT);
            int maKP =0, maKPnx = 0;
            if (lupKhoNhap.EditValue != null)
                maKP = Convert.ToInt32( lupKhoNhap.EditValue);
            if (lupNoiTraDuoc.EditValue != null)
                maKPnx = Convert.ToInt32( lupNoiTraDuoc.EditValue);
            DateTime tungay = DungChung.Ham.NgayTu(dtsTuNgay.DateTime);
            DateTime denngay = DungChung.Ham.NgayDen(dtsDenNgay.DateTime);
            List<NhapD> _lNhpD = new List<NhapD>();
            _lNhpD = (from n in data.NhapDs
                      where (maKP== 0 || n.MaKP== (maKP)) && (maKPnx== 0|| n.MaKPnx== maKPnx)
                      select n).Where(p => p.NgayNhap > tungay).Where(p => p.NgayNhap < denngay).Where(p => p.KieuDon == 10 && p.PLoai == 2).Where(p => p.SoCT.Contains(soCT) || p.IDNhap == IDNhap).ToList();
            binNhapD.DataSource = _lNhpD.OrderByDescending(p => p.IDNhap).ToList();
            grc_NhapD.DataSource = binNhapD;

        }
        private void txt_sSoCT_EditValueChanged(object sender, EventArgs e)
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
        private void lupKhoNhap_EditValueChanged(object sender, EventArgs e)
        {
            loadDSDon();
        }
        private void lupNoiTraDuoc_EditValueChanged(object sender, EventArgs e)
        {
            loadDSDon();
        }
        bool count_themmoi = false;
        private void grv_NhapD_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (count_themmoi && _idtoInsert >0)
            {               
                for (int i = 0; i < grv_NhapD.RowCount; i++)
                {
                    if ((grv_NhapD.GetRowCellValue(i, "IDNhap") != null && Convert.ToInt32(grv_NhapD.GetRowCellValue(i, "IDNhap")) == _idtoInsert))
                    {
                        grv_NhapD.FocusedRowHandle = i;
                        _bolsua = true;
                        break;
                    }
                }
            }
            else
            {
                if (grv_NhapD.FocusedRowHandle >= 0)
                {
                    _strMaCB = "";
                    _bolsua = false;
                    rowNhapD = grv_NhapD.FocusedRowHandle;
                    grv_NhapD.Appearance.Row.BackColor = Color.White;
                    grv_NhapDct.Appearance.Row.BackColor = Color.White;
                    _idNhap = -1;
                    if (grv_NhapD.GetRowCellValue(rowNhapD, "IDNhap") != null)
                    {
                        _idNhap = Convert.ToInt32(grv_NhapD.GetRowCellValue(rowNhapD, "IDNhap").ToString());
                    }
                    if (_idNhap > 0)
                        lblIDDon.Text = _idNhap.ToString();
                    if (grv_NhapD.GetRowCellValue(rowNhapD, colCanBo) != null)
                        _strMaCB = grv_NhapD.GetRowCellValue(rowNhapD, colCanBo).ToString().Trim();

                    setEditTable(false);
                    if (!checkQuyenChung(rowNhapD, 1, _strMaCB))
                    {
                        grv_NhapD.Appearance.FocusedRow.BackColor = Color.White;
                    }
                    else
                    {
                        grv_NhapD.Appearance.FocusedRow.BackColor = Color.Aquamarine;
                    }
                    _idtoInsert = 0;
                }
                else
                {
                    lblIDDon.Text = "0";
                    if (grv_NhapD.FocusedRowHandle < 0)
                    {
                        if (!checkQuyenChung(rowNhapD, 0, _strMaCB))
                        {
                            setEditTable(false);
                           // MessageBox.Show("Bạn không có quyền thêm mới");
                        }
                        else
                        {
                            grv_NhapD.Appearance.FocusedRow.BackColor = Color.Yellow;
                            setEditTable(true);
                        }
                    }
                    grv_NhapD.FocusedColumn = colSoCT;
                }
            }            
            count_themmoi = false;
        }
        private void setEditTable(bool rs)
        {
            grv_NhapD.Columns["SoCT"].OptionsColumn.AllowEdit = rs;
            grv_NhapD.Columns["NgayNhap"].OptionsColumn.AllowEdit = rs;
            if (DungChung.Bien.PLoaiKP == "Admin")
            {
                grv_NhapD.Columns["MaKP"].OptionsColumn.AllowEdit = rs;
                grv_NhapD.Columns["MaCB"].OptionsColumn.AllowEdit = rs;
            }
            else
            {
                grv_NhapD.Columns["MaKP"].OptionsColumn.AllowEdit = false;
                grv_NhapD.Columns["MaCB"].OptionsColumn.AllowEdit = false;
            }
            grv_NhapD.Columns["MaKPnx"].OptionsColumn.AllowEdit = rs;
            grv_NhapD.Columns["TenNguoiCC"].OptionsColumn.AllowEdit = rs;
            grv_NhapD.Columns["GhiChu"].OptionsColumn.AllowEdit = rs;
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
                        if (checkQuyenChung(row, 2, _strMaCB) || count_themmoi)
                        {
                            DialogResult dialogResult = MessageBox.Show("Bạn có muốn xóa đơn chi tiết này không ?", "Xác nhận xóa", MessageBoxButtons.YesNo);
                            if (dialogResult == DialogResult.Yes)
                            {
                                var donct = data.NhapDcts.Single(p => p.IDNhapct == idct);
                                data.NhapDcts.Remove(donct);
                                data.SaveChanges();
                                GridView view = sender as GridView;
                                view.DeleteRow(row);
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
            else if (e.Column.Name == "colSua")
            {
                if (row >= 0)
                {
                    if (!checkQuyenChung(row, 1, _strMaCB))
                    {
                        MessageBox.Show("Bạn không có quyền sửa đơn chi tiết trong đơn này");
                    }
                    else
                    {

                        grv_NhapDct.Appearance.FocusedRow.BackColor = Color.Yellow;
                        setEditNhapdct(true);
                    }
                }
                else if(grv_NhapDct.IsNewItemRow(grv_NhapDct.FocusedRowHandle))
                {
                    if (!checkQuyenChung(row, 0, _strMaCB))
                    {
                        grv_NhapDct.Appearance.FocusedRow.BackColor = Color.White;
                       // MessageBox.Show("Banj khoong co quyen them moi");
                    }
                    else
                    {

                        grv_NhapDct.Appearance.FocusedRow.BackColor = Color.Yellow;
                        setEditNhapdct(true);
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
                if (maDV >0)
                {
                    var nhapdct_dg = (from dct in data.NhapDcts
                                      join d in data.NhapDs on dct.IDNhap equals d.IDNhap
                                      where dct.MaDV == maDV && (d.PLoai == 1 || (d.PLoai == 2 && d.KieuDon == 10))
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
        string _strMaCB = DungChung.Bien.MaCB;
        private void btn_XoaDon2_Click(object sender, EventArgs e)
        {
            _idNhap = Convert.ToInt32(lblIDDon.Text);
            if (grv_NhapD.GetFocusedRowCellValue("IDNhap") != null)
            {
                if (checkQuyenChung(rowNhapD, 2, _strMaCB))
                {
                    if (_idNhap > 0)
                    {
                        var dsct = data.NhapDcts.Where(p => p.IDNhap == _idNhap).ToList();
                        if (dsct.Count <= 0)
                        {
                            DialogResult dialogResult = MessageBox.Show("Bạn có muốn xóa đơn này không ?", "Xác nhận xóa", MessageBoxButtons.YesNo);
                            if (dialogResult == DialogResult.Yes)
                            {
                                var xoa = data.NhapDs.Single(p => p.IDNhap == _idNhap);
                                data.NhapDs.Remove(xoa);
                                data.SaveChanges();                               
                                loadDSDon();
                                lblIDDon.Text = grv_NhapD.GetRowCellValue(grv_NhapD.FocusedRowHandle, colIDDon) != null ? (grv_NhapD.GetRowCellValue(grv_NhapD.FocusedRowHandle, colIDDon).ToString()) : "0";
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
                                lblIDDon.Text = grv_NhapD.GetRowCellValue(grv_NhapD.FocusedRowHandle, colIDDon) != null ? (grv_NhapD.GetRowCellValue(grv_NhapD.FocusedRowHandle, colIDDon).ToString()) : "0";
                            }
                        }
                    }
                    else
                        loadDSDon();
                }
                else
                {
                    MessageBox.Show("Bạn không có quyền xóa đơn này");
                }
            }
        }
        private void frm_HoaDonThuoc_KeyUp(object sender, KeyEventArgs e)
        {           
                if (e.KeyCode == Keys.Escape)
                    this.Dispose();               
        }
        int rowNhapD = 0;
        int _idtoInsert = 0;
        private void grv_NhapD_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            string _soCT = "", _maCC = "", _nguoiCC = "", _ghichu = "", _canbo = "";
            int _maKP = 0, _maKPNX = 0;
            DateTime _ngaynhap = DateTime.Now;
            rowNhapD = grv_NhapD.FocusedRowHandle;
            _idtoInsert = 0;
            if (grv_NhapD.GetRowCellValue(rowNhapD, "SoCT") != null)
                _soCT = grv_NhapD.GetRowCellValue(rowNhapD, "SoCT").ToString();
            if (grv_NhapD.GetRowCellValue(rowNhapD, "NgayNhap") != null)
                _ngaynhap = Convert.ToDateTime(grv_NhapD.GetRowCellValue(rowNhapD, "NgayNhap"));
            if (grv_NhapD.GetRowCellValue(rowNhapD, "MaKP") != null)
                _maKP = Convert.ToInt32( grv_NhapD.GetRowCellValue(rowNhapD, "MaKP"));
            if (grv_NhapD.GetRowCellValue(rowNhapD, "MaKPnx") != null)
                _maKPNX = Convert.ToInt32( grv_NhapD.GetRowCellValue(rowNhapD, "MaKPnx"));
            if (grv_NhapD.GetRowCellValue(rowNhapD, "MaCC") != null)
                _maCC = grv_NhapD.GetRowCellValue(rowNhapD, "MaCC").ToString();
            if (grv_NhapD.GetRowCellValue(rowNhapD, "TenNguoiCC") != null)
                _nguoiCC = grv_NhapD.GetRowCellValue(rowNhapD, "TenNguoiCC").ToString();
            if (grv_NhapD.GetRowCellValue(rowNhapD, "GhiChu") != null)
                _ghichu = grv_NhapD.GetRowCellValue(rowNhapD, "GhiChu").ToString();
            if (grv_NhapD.GetRowCellValue(rowNhapD, "MaCB") != null)
                _canbo = grv_NhapD.GetRowCellValue(rowNhapD, "MaCB").ToString();
            if (checkNhapD(sender, e))
            {
                if (e.RowHandle < 0)
                {
                    NhapD don = new NhapD();
                    don.SoCT = _soCT;
                    don.NgayNhap = _ngaynhap;
                    don.MaKPnx = _maKPNX;
                    don.TenNguoiCC = _nguoiCC;
                    don.MaKP = _maKP;
                    don.MaCB = _canbo;
                    don.GhiChu = _ghichu;
                    don.PLoai = 2;
                    don.KieuDon = 10;
                    data.NhapDs.Add(don);
                    if (data.SaveChanges() > 0)
                    {
                        var idNhap = (from d in data.NhapDs
                                      orderby d.IDNhap descending
                                      where d.SoCT== (_soCT)
                                      select d.IDNhap
                                       ).ToList();
                        if (idNhap.Count > 0)
                        {
                            _idNhap = idNhap.First();
                            lblIDDon.Text = _idNhap.ToString();
                            _idtoInsert = _idNhap;
                            count_themmoi = true;
                        }
                    }
                    loadDSDon();
                }
                else
                {
                    #region sửa
                    var suaNhapD = data.NhapDs.Where(p => p.IDNhap == _idNhap).ToList();
                    if (suaNhapD.Count > 0)
                    {
                        suaNhapD.First().SoCT = _soCT;
                        suaNhapD.First().NgayNhap = _ngaynhap;
                        suaNhapD.First().MaKPnx = _maKPNX;
                        suaNhapD.First().TenNguoiCC = _nguoiCC;
                        suaNhapD.First().MaKP = _maKP;
                        suaNhapD.First().MaCB = _canbo;
                        suaNhapD.First().GhiChu = _ghichu;
                        data.SaveChanges();
                    }
                    #endregion
                }

            }
            #region tạm bỏ
            //if (e.RowHandle < 0)//thêm mới
            //{
            //    NhapD don = new NhapD();
            //    don.SoCT = _soCT;
            //    don.NgayNhap = _ngaynhap;
            //    don.MaCC = _maCC;
            //    don.TenNguoiCC = _nguoiCC;
            //    don.MaKP = _maKP;
            //    don.MaCB = _canbo;
            //    don.GhiChu = _ghichu;
            //    don.PLoai = 2;
            //    don.KieuDon = 10;
            //    data.NhapDs.Add(don);
            //    if (data.SaveChanges() > 0)
            //    {

            //        //lấy ra idDon vừa nhập
            //        var idNhap = (from d in data.NhapDs
            //                      orderby d.IDNhap descending
            //                      where d.SoCT== (_soCT)
            //                      select d.IDNhap
            //                       ).ToList();
            //        if (idNhap.Count > 0)
            //            _idNhap =  idNhap.First();
            //        MessageBox.Show("Thêm mới thành công");
            //    }

            //}
            //else // sửa
            //{
            //    #region sửa
            //    var suaNhapD = data.NhapDs.Single(p => p.IDNhap == _idNhap);
            //    suaNhapD.SoCT = _soCT;
            //    suaNhapD.NgayNhap = _ngaynhap;
            //    suaNhapD.MaCC = _maCC;
            //    suaNhapD.TenNguoiCC = _nguoiCC;
            //    suaNhapD.MaKP = _maKP;
            //    suaNhapD.MaCB = _canbo;
            //    suaNhapD.GhiChu = _ghichu;
            //    if (data.SaveChanges() > 0)
            //    {
            //        loadDSDon();
            //    }
            //    #endregion
            //}
            #endregion tạm bỏ
        }

        private bool checkNhapD(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            GridView view = sender as GridView;
            GridColumn colKPhong = view.Columns["MaKP"];
            GridColumn colKPhongnx = view.Columns["MaKPnx"];
            GridColumn colCanBo = view.Columns["MaCB"];
            GridColumn colNguoiTra = view.Columns["TenNguoiCC"];
            object valKPhong = rowNhapD < 0 ? view.GetRowCellDisplayText(e.RowHandle, colKPhong) : view.GetRowCellValue(e.RowHandle, colKPhong);

            object valKPhongnx = view.GetRowCellDisplayText(e.RowHandle, colKPhongnx);
            object valNguoiTra = view.GetRowCellDisplayText(e.RowHandle, colNguoiTra);
            object valCanBo = rowNhapD < 0 ? view.GetRowCellDisplayText(e.RowHandle, colCanBo) : view.GetRowCellValue(e.RowHandle, colCanBo);

            if (valKPhong == null || valKPhong.ToString() == string.Empty)
            {
                e.Valid = false;
                view.SetColumnError(colKPhong, "Bạn chưa chọn kho nhập", ErrorType.Warning);
                return false;
            }
            else if (valKPhongnx == null || valKPhongnx.ToString() == string.Empty)
            {
                e.Valid = false;
                view.SetColumnError(colKPhongnx, "Bạn chưa chọn kho trả dược", ErrorType.Warning);
                return false;
            }
            //else if (valNguoiTra == null || valNguoiTra.ToString() == string.Empty)
            //{
            //    e.Valid = false;
            //    view.SetColumnError(colNguoiCC, "Bạn chưa nhập người trả dược", ErrorType.Warning);
            //    return false;
            //}
            else if (DungChung.Bien.PLoaiKP != "Admin" && (rowNhapD < 0 ? (valCanBo == null || valCanBo.ToString() == string.Empty) : (valCanBo == null || valCanBo.ToString() != DungChung.Bien.MaCB))) //DungChung.Bien.PLoaiKP != "Admin" && 
            {
                e.Valid = false;
                view.SetColumnError(colCanBo, "Tên cán bộ không hợp lệ", ErrorType.Warning);
                return false;
            }
            else
            {

                return true;
            }
        }
        private void grv_NhapD_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            grv_NhapD.SetRowCellValue(e.RowHandle, colNgayNhap, DateTime.Now.Date);
            grv_NhapD.SetRowCellValue(e.RowHandle, colCanBo, DungChung.Bien.MaCB);
            grv_NhapD.SetRowCellValue(e.RowHandle, colKPhong, DungChung.Bien.MaKP);
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {          
            if (rowNhapD >= 0)
            {
                if (!checkQuyenChung(rowNhapD, 1, _strMaCB))
                {
                    grv_NhapD.Appearance.FocusedRow.BackColor = Color.White;
                    MessageBox.Show("Bạn không có quyền sửa đơn này");
                }
                else
                {
                    grv_NhapD.Appearance.FocusedRow.BackColor = Color.Yellow;
                    setEditTable(true);
                }
                _bolsua = true;
                grv_NhapDct_FocusedRowChanged(null, null);
            }
        }
        private void setEditNhapdct(bool rs)
        {
            int row = grv_NhapDct.FocusedRowHandle;
            grv_NhapDct.Columns["SoLo"].OptionsColumn.AllowEdit = rs;
            grv_NhapDct.Columns["SoDangKy"].OptionsColumn.AllowEdit = rs;
            grv_NhapDct.Columns["HanDung"].OptionsColumn.AllowEdit = rs;
            grv_NhapDct.Columns["DonGia"].OptionsColumn.AllowEdit = rs;
            grv_NhapDct.Columns["SoLuongX"].OptionsColumn.AllowEdit = rs;
            grv_NhapDct.Columns["ThanhTienX"].OptionsColumn.AllowEdit = rs;
            if (rs && row < 0)
            {
                grv_NhapDct.Columns["DonVi"].OptionsColumn.AllowEdit = true;
                grv_NhapDct.Columns["MaDV"].OptionsColumn.AllowEdit = true;
            }
            else
            {
                grv_NhapDct.Columns["DonVi"].OptionsColumn.AllowEdit = false;
                grv_NhapDct.Columns["MaDV"].OptionsColumn.AllowEdit = false;
            }
        }
        private bool _bolsua = false;
        private void grv_NhapDct_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            rowNhapct = grv_NhapDct.FocusedRowHandle;
            grv_NhapDct.Appearance.Row.BackColor = Color.White;

            if (rowNhapct < 0)
            {
               
                if (!checkQuyenChung(rowNhapct, 0, _strMaCB))
                {                   
                    grv_NhapDct.Appearance.FocusedRow.BackColor = Color.White;
                    setEditNhapdct(false);
                }
                else
                {                    
                    grv_NhapDct.Appearance.FocusedRow.BackColor = Color.Aquamarine;
                }
            }
            else
            {
                setEditNhapdct(false);
                if (!checkQuyenChung(rowNhapct, 1, _strMaCB))
                {
                    grv_NhapDct.Appearance.FocusedRow.BackColor = Color.White;
                }
                else
                {
                    grv_NhapDct.Appearance.FocusedRow.BackColor = Color.Aquamarine;
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
                          where d.MaDV== maDV
                          select new { d.DonVi, d.DonGia }).ToList();
                var nhapdct_dg = (from dct in data.NhapDcts
                                  join d in data.NhapDs on dct.IDNhap equals d.IDNhap
                                  where dct.MaDV== maDV && (d.PLoai == 1 || (d.PLoai == 2 && d.KieuDon == 10))
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
                view.SetColumnError(colMaDV, "Bạn chưa chọn loại thuốc", ErrorType.Warning);
                e.ErrorText = "Bạn muốn sửa lại dữ liệu cho đúng ?";

                return false;
            }
            else if (valDonGia == null || valDonGia.ToString() == string.Empty)
            {
                e.Valid = false;
                view.SetColumnError(colDonGia, "Bạn chưa nhập đơn giá", ErrorType.Warning);
                e.ErrorText = "Bạn muốn sửa lại dữ liệu cho đúng ?";
                view.SetColumnError(colMaDV, "", ErrorType.None);

                return false;
            }

            else if (valSoLuong == null || valSoLuong.ToString() == string.Empty)
            {
                e.Valid = false;
                view.SetColumnError(colSoLuong, "Bạn chưa nhập số lượng", ErrorType.Warning);
                e.ErrorText = "Bạn muốn sửa lại dữ liệu cho đúng ?";
                view.SetColumnError(colMaDV, "", ErrorType.None);
                view.SetColumnError(colDonGia, "", ErrorType.None);


                return false;
            }

            else if (Convert.ToDouble((valSoLuong).ToString()) <= 0)
            {
                e.Valid = false;
                view.SetColumnError(colSoLuong, "số lượng phải lớn hơn 0", ErrorType.Warning);
                e.ErrorText = "Bạn muốn sửa lại dữ liệu cho đúng ?";
                view.SetColumnError(colMaDV, "", ErrorType.None);
                view.SetColumnError(colDonGia, "", ErrorType.None);

                return false;
            }
            else if (Convert.ToDouble((valDonGia).ToString()) <= 0)
            {
                e.Valid = false;
                view.SetColumnError(colDonGia, "đơn giá phải lơn hơn 0", ErrorType.Warning);//
                e.ErrorText = "Bạn muốn sửa lại dữ liệu cho đúng ?";
                view.SetColumnError(colMaDV, "", ErrorType.None);
                return false;
            }
            else 
            {               
                return true;
            }

        }
        private void grv_NhapDct_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {

            int i = grv_NhapDct.FocusedRowHandle;
            _idNhap = Convert.ToInt32(lblIDDon.Text);
            try
            {
                if (_idNhap > 0)
                {
                    var don = data.NhapDs.Where(p => p.IDNhap == _idNhap).ToList();
                    if (don.Count > 0)
                    {
                        if (checkNhapDCT_setColumnError(sender, e))
                        {
                            #region thêm mới
                            if (i < 0)
                            {
                                if (grv_NhapDct.GetRowCellValue(i, "MaDV") != null && grv_NhapDct.GetRowCellValue(i, "DonGia") != null && grv_NhapDct.GetRowCellValue(i, "SoLuongX") != null)
                                {
                                    int maDV = 0;
                                    string solo = "", donvi = "";
                                    float dongia = 0, soluongn = 0, thanhtienn = 0;
                                    if (grv_NhapDct.GetRowCellValue(i, "MaDV") != null)
                                        maDV = Convert.ToInt32( grv_NhapDct.GetRowCellValue(i, "MaDV"));
                                    if (grv_NhapDct.GetRowCellValue(i, "SoLo") != null)
                                        solo = grv_NhapDct.GetRowCellValue(i, "SoLo").ToString();
                                    var dvi = (from d in _lDichVu where d.MaDV== maDV select d.DonVi).ToList();
                                    if (dvi.Count() > 0)
                                        donvi = dvi.First().ToString();
                                    if (grv_NhapDct.GetRowCellValue(i, "DonGia") != null)
                                        dongia = (float)Convert.ToDouble(grv_NhapDct.GetRowCellValue(i, "DonGia"));
                                    if (grv_NhapDct.GetRowCellValue(i, "SoLuongX") != null)
                                        soluongn = (float)Convert.ToDouble(grv_NhapDct.GetRowCellValue(i, "SoLuongX"));
                                    if (grv_NhapDct.GetRowCellValue(i, "SoLuongX") != null && grv_NhapDct.GetRowCellValue(i, "DonGia") != null && grv_NhapDct.GetRowCellValue(i, "SoLuongX").ToString() != "" && grv_NhapDct.GetRowCellValue(i, "DonGia").ToString() != "")
                                        thanhtienn = (float)Convert.ToDouble(grv_NhapDct.GetRowCellValue(i, "DonGia")) * (float)Convert.ToDouble(grv_NhapDct.GetRowCellValue(i, "SoLuongX"));
                                    NhapDct nhapct = new NhapDct();
                                    nhapct.IDNhap = _idNhap;
                                    nhapct.MaDV = maDV;
                                    nhapct.SoLo = solo;
                                    if (grv_NhapDct.GetRowCellValue(i, "SoDangKy") != null)
                                        nhapct.SoDangKy = grv_NhapDct.GetRowCellValue(i, "SoDangKy").ToString();
                                    if (grv_NhapDct.GetRowCellValue(i, "HanDung") != null)
                                        nhapct.HanDung = Convert.ToDateTime(grv_NhapDct.GetRowCellValue(i, "HanDung").ToString());
                                    if (grv_NhapDct.GetRowCellValue(i, "VAT") != null)
                                        nhapct.VAT = Convert.ToInt32(grv_NhapDct.GetRowCellValue(i, "VAT").ToString());
                                    nhapct.DonVi = donvi;
                                    nhapct.DonGia = dongia;
                                    if (grv_NhapDct.GetRowCellValue(i, "DonGiaCT") != null)
                                        nhapct.DonGiaCT = (float)Convert.ToDouble(grv_NhapDct.GetRowCellValue(i, "DonGiaCT"));
                                    nhapct.SoLuongX = soluongn >= 0 ? (-soluongn) : soluongn;
                                    nhapct.ThanhTienX = thanhtienn >= 0 ? thanhtienn : (-thanhtienn);
                                    nhapct.SoLuongSD = 0;
                                    nhapct.ThanhTienSD = 0;
                                    nhapct.SoLuongN = 0;
                                    nhapct.ThanhTienN = 0;
                                    nhapct.SoLuongDY = 0;
                                    nhapct.ThanhTienDY = 0;
                                    nhapct.SoLuongKK = 0;
                                    nhapct.ThanhTienKK = 0;
                                    data.NhapDcts.Add(nhapct);
                                    if (data.SaveChanges() >= 0)
                                    {
                                        loadNhapDct(Convert.ToInt32(lblIDDon.Text));
                                    }
                                }
                            }
                            #endregion hết thêm mới
                            #region Sửa đơn chi tiết
                            else
                            {
                                int idct = 0;
                                if (grv_NhapDct.GetRowCellValue(i, colIDNhapct) != null)
                                {
                                    idct = Convert.ToInt32(grv_NhapDct.GetRowCellValue(i, colIDNhapct).ToString());
                                }
                                if (idct >= 0)
                                {
                                    if (grv_NhapDct.GetRowCellValue(i, "MaDV") != null && grv_NhapDct.GetRowCellValue(i, "DonGia") != null && grv_NhapDct.GetRowCellValue(i, "SoLuongX") != null)
                                    {
                                        int maDV = 0;
                                        string solo = "", donvi = "";
                                        float dongia = 0, soluongn = 0, thanhtienn = 0;
                                        if (grv_NhapDct.GetRowCellValue(i, "MaDV") != null)
                                            maDV = Convert.ToInt32( grv_NhapDct.GetRowCellValue(i, "MaDV"));
                                        if (grv_NhapDct.GetRowCellValue(i, "SoLo") != null)
                                            solo = grv_NhapDct.GetRowCellValue(i, "SoLo").ToString();
                                        var dvi = (from d in _lDichVu where d.MaDV == maDV select d.DonVi).ToList();
                                        if (dvi.Count() > 0)
                                            donvi = dvi.First().ToString();
                                        if (grv_NhapDct.GetRowCellValue(i, "DonGia") != null)
                                            dongia = (float)Convert.ToDouble(grv_NhapDct.GetRowCellValue(i, "DonGia"));
                                        if (grv_NhapDct.GetRowCellValue(i, "SoLuongX") != null)
                                            soluongn = (float)Convert.ToDouble(grv_NhapDct.GetRowCellValue(i, "SoLuongX"));
                                        if (grv_NhapDct.GetRowCellValue(i, "SoLuongX") != null && grv_NhapDct.GetRowCellValue(i, "DonGia") != null && grv_NhapDct.GetRowCellValue(i, "SoLuongX").ToString() != "" && grv_NhapDct.GetRowCellValue(i, "DonGia").ToString() != "")
                                            thanhtienn = (float)Convert.ToDouble(grv_NhapDct.GetRowCellValue(i, "DonGia")) * (float)Convert.ToDouble(grv_NhapDct.GetRowCellValue(i, "SoLuongX"));

                                        var sua = data.NhapDcts.Single(p => p.IDNhapct == idct);
                                        sua.MaDV = maDV;
                                        sua.SoLo = solo;
                                        if (grv_NhapDct.GetRowCellValue(i, "SoDangKy") != null)
                                            sua.SoDangKy = grv_NhapDct.GetRowCellValue(i, "SoDangKy").ToString();
                                        if (grv_NhapDct.GetRowCellValue(i, "HanDung") != null)
                                            sua.HanDung = Convert.ToDateTime(grv_NhapDct.GetRowCellValue(i, "HanDung").ToString());
                                        if (grv_NhapDct.GetRowCellValue(i, "VAT") != null)
                                            sua.VAT = Convert.ToInt32(grv_NhapDct.GetRowCellValue(i, "VAT").ToString());
                                        sua.DonVi = donvi;
                                        sua.DonGia = dongia;
                                        if (grv_NhapDct.GetRowCellValue(i, "DonGiaCT") != null)
                                            sua.DonGiaCT = (float)Convert.ToDouble(grv_NhapDct.GetRowCellValue(i, "DonGiaCT"));
                                        sua.SoLuongX = soluongn > 0 ? -soluongn : soluongn;
                                        sua.ThanhTienX = thanhtienn > 0 ? thanhtienn : -thanhtienn;
                                        if (data.SaveChanges() >= 0)
                                            loadNhapDct(_idNhap);
                                    }
                                }
                            }
                            #endregion hết Sửa đơn chi tiết
                        }

                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy đơn");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void PassData(int maDV)
        {
            if (grv_NhapDct.IsNewItemRow(grv_NhapDct.FocusedRowHandle) && grv_NhapDct.GetRow(grv_NhapDct.FocusedRowHandle) == null)
            {
                grv_NhapDct.AddNewRow();
            }
            grv_NhapDct.SetFocusedRowCellValue("MaDV", maDV);
        }

        private void grv_NhapDct_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = ExceptionMode.NoAction;
        }

        private void grv_NhapD_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = ExceptionMode.NoAction;
            e.WindowCaption = "Lỗi nhập dữ liệu";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="grv">Thứ tự gridview: 0- Grv_NhapD; 1- Grv_NhapDct </param>
        /// <param name="row">row- Index của row cần tác động</param>
        /// <param name="chucnang">chức năng thêm mới hoặc sửa, xóa, xem</param>
        /// <param name="maCB">mã Cán Bộ thêm đơn khi sửa đơn, khi thêm mới mã cán bộ  = Đungchung.Bien.MaCB</param>
        /// <returns></returns>
        //private bool checkQuyenChung(int grv, int row, int chucnang, string maCB)
        //{
        //    if (DungChung.Bien.PLoaiKP == "Admin")
        //    {
        //        //setEditTable(true);
        //        //setEditNhapdct(true);
        //        //grv_NhapD.Appearance.Row.BackColor = Color.White;
        //        //grv_NhapD.Appearance.FocusedRow.BackColor = Color.White;
        //        //grv_NhapD.Appearance.FocusedRow.BackColor = Color.Yellow;
        //        //grv_NhapDct.Appearance.Row.BackColor = Color.White;
        //        //grv_NhapDct.Appearance.FocusedRow.BackColor = Color.White;
        //        //grv_NhapDct.Appearance.FocusedRow.BackColor = Color.Yellow;
        //        return true;
        //    }
        //    else
        //    {
        //        if (grv == 0)// nhập, sửa đơn
        //        {
        //            if (DungChung.Ham.checkQuyen(this.Name)[chucnang])
        //            {
        //                if (chucnang == 0 || (chucnang == 1 && DungChung.Bien.MaCB == maCB))
        //                {
        //                //    grv_NhapD.Appearance.Row.BackColor = Color.White;
        //                //    grv_NhapD.Appearance.FocusedRow.BackColor = Color.White;
        //                //    grv_NhapD.Appearance.FocusedRow.BackColor = chucnang == 0 ? Color.Yellow : Color.SandyBrown;
        //                //    setEditTable(true);
        //                }
        //                else
        //                {
        //                    //grv_NhapD.Appearance.Row.BackColor = Color.White;
        //                    //grv_NhapD.Appearance.FocusedRow.BackColor = Color.White;

        //                }

        //                return true;
        //            }
        //            else
        //            //{       
        //            //        grv_NhapD.Appearance.Row.BackColor = Color.White;
        //            //        grv_NhapD.Appearance.FocusedRow.BackColor = Color.White;
        //            //        setEditTable(false);
        //                    return false;
        //            }
        //        }
        //        else if (grv == 1) // nhập chi tiết
        //        {
        //            if (DungChung.Ham.checkQuyen(this.Name)[chucnang])
        //            {
        //                //if (chucnang == 0 || (chucnang == 1 && DungChung.Bien.MaCB == maCB))
        //                //{
        //                //    grv_NhapDct.Appearance.Row.BackColor = Color.White;
        //                //    grv_NhapDct.Appearance.FocusedRow.BackColor = Color.White;
        //                //    grv_NhapDct.Appearance.FocusedRow.BackColor = chucnang == 0 ? Color.Yellow : Color.SandyBrown;
        //                //    setEditNhapdct(true);
        //                //}
        //                //else
        //                //{
        //                //    grv_NhapDct.Appearance.Row.BackColor = Color.White;
        //                //    grv_NhapDct.Appearance.FocusedRow.BackColor = Color.White;
        //                //}
        //                return true;
        //            }
        //            else
        //            {
        //                //grv_NhapDct.Appearance.Row.BackColor = Color.White;
        //                //grv_NhapDct.Appearance.FocusedRow.BackColor = Color.White;
        //                //setEditNhapdct(false);
        //                return false;
        //            }
        //        }
        //        else
        //        {
        //            //grv_NhapD.Appearance.Row.BackColor = Color.White;
        //            //grv_NhapD.Appearance.FocusedRow.BackColor = Color.White;
        //            //grv_NhapDct.Appearance.Row.BackColor = Color.White;
        //            //grv_NhapDct.Appearance.FocusedRow.BackColor = Color.White;
        //            //setEditTable(false);
        //            //setEditNhapdct(false);
        //            return false;
        //        }
        //    }
        private bool checkQuyenChung(int row, int chucnang, string maCB)
        {
            if (DungChung.Bien.PLoaiKP == "Admin")
            {
                return true;
            }
            else
            {
                if (DungChung.Ham.checkQuyen(this.Name)[chucnang])
                {
                    if (((chucnang == 0 || chucnang == 1) && DungChung.Bien.MaCB == maCB) || (chucnang != 1 && chucnang != 0))
                        return true;
                    else
                        return false;
                }
                else
                    return false;
            }

        }

        private void grv_NhapDct_Click(object sender, EventArgs e)
        {
            grv_NhapDct_FocusedRowChanged(null, null);
        }

        private void lblIDDon_TextChanged(object sender, EventArgs e)
          {
              loadNhapDct(Convert.ToInt32(lblIDDon.Text));
          }

        private void grc_NhapD_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab || e.KeyCode == Keys.Enter)
            {               
                GridControl grid = sender as GridControl;
                GridView view = grid.FocusedView as GridView;
                int row = view.FocusedRowHandle;
               // if (view.IsNewItemRow(view.FocusedRowHandle) && view.GetRow(view.FocusedRowHandle) != null)  
                if (!view.IsNewItemRow(row))
                {
                    if (e.KeyCode == Keys.Enter && view.FocusedColumn.VisibleIndex == 1)
                    {
                        btnEdit_Click(null, null);
                    }
                }
                else if (view.IsNewItemRow(row) && view.GetRow(view.FocusedRowHandle) != null && (view.FocusedColumn.VisibleIndex == view.VisibleColumns.Count - 1))
                {

                    lupNoiTraDuoc.Text = "";
                }
            }
            else if (e.KeyCode == Keys.Down)
            {
                if (grv_NhapD.FocusedColumn == colNoiTraDuoc && _bolsua)
                {
                    grv_NhapD.OptionsNavigation.EnterMoveNextColumn = false;

                    GridView view = (sender as GridControl).FocusedView as GridView;
                    if (view.ActiveEditor is LookUpEdit)
                    {
                        LookUpEdit edit = view.ActiveEditor as LookUpEdit;
                        edit.ShowPopup();
                    }
                }
            }
        }

        private void grc_NhapDct_ProcessGridKey(object sender, KeyEventArgs e)
        {
            GridControl grid = sender as GridControl;
            GridView view = grid.FocusedView as GridView;
            if (e.KeyCode == Keys.F9)
            {
                
                if (view.FocusedColumn.VisibleIndex == 2 && view.IsNewItemRow(view.FocusedRowHandle) && _bolsua)
                {
                    //int makp = 0, makho = 0;
                    //if (lupKhoaKhamkb.EditValue != null)
                    //    makp = Convert.ToInt32(lupKhoaKhamkb.EditValue);
                    //if (lupKhoXuat.EditValue != null)
                    //    makho = Convert.ToInt32(lupKhoXuat.EditValue);
                    //frm_DsMaDV frm = new frm_DsMaDV();
                    //frm.passMaDV = new frm_DsMaDV.PassMaDV(PassData);
                    //frm.ShowDialog();               
                }
            }          
                if (e.KeyCode == Keys.Enter && view.FocusedColumn.VisibleIndex == 1 && !view.IsNewItemRow(view.FocusedRowHandle))
                {
                    if (!checkQuyenChung(view.FocusedRowHandle, 1, _strMaCB))
                    {
                        MessageBox.Show("Bạn không có quyền sửa đơn chi tiết trong đơn này");
                    }
                    else
                    {
                        grv_NhapDct.Appearance.FocusedRow.BackColor = Color.Yellow;
                        setEditNhapdct(true);
                    }
                }
            
        }

        private void grv_NhapD_FocusedColumnChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs e)
        {
            //if ( grv_NhapD.FocusedColumn == colNoiTraDuoc && _bolsua)   
            //{               
            //    grv_NhapD.OptionsNavigation.EnterMoveNextColumn = false;               
            //}
            //else
            //    grv_NhapD.OptionsNavigation.EnterMoveNextColumn = true;
           
               
            
        }

        private void grc_NhapD_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                lupNoiTraDuoc.Text = "";
               
            }
        }

        private void txt_sSoCT_Leave(object sender, EventArgs e)
        {
           // lupNoiTraDuoc.Text = "  ";
        }

     
       
    }
    //public class MyGridLocalizer : GridLocalizer
    //{
    //    public override string GetLocalizedString(GridStringId id)
    //    {
    //        if (id == GridStringId.ColumnViewExceptionMessage) return string.Empty;
    //        return base.GetLocalizedString(id);
    //    }
    //}
}