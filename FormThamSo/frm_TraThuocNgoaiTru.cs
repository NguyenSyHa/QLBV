using QLBV.DungChung;
using QLBV_Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace QLBV.FormThamSo
{
    public partial class frm_TraThuocNgoaiTru : Form
    {
        string _KP = "";
        string _KhoKe = "";
        int _int_maBN = -1000;
        int _iddon = -1000;
        int _iddonTra = -1000;
        int _maKP = -1000;
        double sothangSua = 1;
        List<DThuocct> _lDthuocct = new List<DThuocct>();
        public frm_TraThuocNgoaiTru(string kp, string khoke, int mabn, int makp)
        {
            InitializeComponent();
            _KP = kp;
            _KhoKe = khoke;
            _int_maBN = mabn;
            _maKP = makp;
        }

        private void frm_TraThuocNgoaiTru_Load(object sender, EventArgs e)
        {
            grcDonThuocct.DataSource = null;
            _iddon = 0;
            _iddonTra = 0;
            sothangSua = 1;
            lupNgayKe.EditValue = DateTime.Now;
            QLBVEntities DaTaContext = new QLBVEntities(DungChung.Bien.StrCon);
            lupBSKe.Properties.DataSource = DaTaContext.CanBoes.Where(p => p.Status == 1).ToList();
            _lDthuocct.Clear();
            var _donThuoc = (from dt in DaTaContext.DThuocs.Where(p => p.MaBNhan == _int_maBN).Where(p => p.PLDV == 1 && (p.KieuDon == -1 || p.KieuDon == 8))
                             join dtct in DaTaContext.DThuoccts on dt.IDDon equals dtct.IDDon
                             where (dtct.Status == 1) // hien thi don da linh
                             select new {dt.NgayKe, dt.IDDon, dtct }).OrderBy(p => p. IDDon).ToList();

            var MaDuoc = (from dtct in _donThuoc
                          join dv in DaTaContext.DichVus on dtct.dtct.MaDV equals dv.MaDV
                          select new
                          {
                              dv.MaDV,
                              dv.TenDV,
                              dtct.dtct.SoLuongct,
                          }).ToList();
            repositoryItemLookUpEdit2.DataSource = MaDuoc;
            if (_donThuoc.Count > 0)
            {
                _iddon = Convert.ToInt32(_donThuoc.First().IDDon);
                var _lDthuocct1 = (from dt in DaTaContext.DThuocs.Where(p => p.MaBNhan == _int_maBN).Where(p => p.PLDV == 1 && (p.KieuDon == -1 || p.KieuDon == 8))
                              join dtct in DaTaContext.DThuoccts on dt.IDDon equals dtct.IDDon
                              where (dtct.SoLuongct < 0)
                              select new {
                                  dt.NgayKe,
                                  dt.IDDon,
                                  dtct,
                              }).ToList();
                _lDthuocct = (from dtct in _lDthuocct1
                              select new DThuocct
                              {
                                  MaDV = dtct.dtct.MaDV,
                                  SoLuongct = dtct.dtct.SoLuongct,
                                  SoLuong = dtct.dtct.SoLuong,
                                  IDDonct = dtct.dtct.IDDonct,
                                  IDDon = dtct.dtct.IDDon,
                                  NgayNhap = dtct.dtct.NgayNhap,
                                  Status = dtct.dtct.Status,
                              }).ToList();
                if (_lDthuocct.Count > 0)
                {
                    _iddonTra = Convert.ToInt32(_lDthuocct.First().IDDon);
                    lupBSKe.EditValue = _lDthuocct1.First().dtct.MaCB;
                    lupNgayKe.EditValue = _lDthuocct1.First().NgayKe;

                    if (_lDthuocct.First().Status ==1)
                    {
                        colMaDV.OptionsColumn.AllowEdit = false;
                        colSoLuongct.OptionsColumn.AllowEdit = false;
                        colNgayKe.OptionsColumn.AllowEdit = false;
                    }
                    CheckReadonly(true);
                }
                else
                {
                    CheckReadonly(false);
                }
            }
            bindingSourceDTCT.DataSource = _lDthuocct;
            grcDonThuocct.DataSource = bindingSourceDTCT;
            txtKhoaPhong.Text = _KP;
            txtKhoTra.Text = _KhoKe;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        public void CheckReadonly(bool T)
        {
            lupNgayKe.ReadOnly = T;
            lupBSKe.ReadOnly = T;
            //grcDonThuocct.Enabled = !T;
            colMaDV.OptionsColumn.AllowEdit = !T;
            colSoLuongct.OptionsColumn.AllowEdit = !T;
            colNgayKe.OptionsColumn.AllowEdit = !T;
            btnSave.Enabled = !T;
            btnEdit.Enabled = T;
            btnInDon.Enabled = T;
        }

        private void grvDonThuocct_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            QLBVEntities DaTaContext = new QLBVEntities(DungChung.Bien.StrCon);
            if (grvDonThuocct.OptionsBehavior.Editable == true)
            {
                switch (e.Column.Name)
                {
                    case "colXoactdt":
                        int xoaid = 0;
                        if (!DungChung.Ham.KTraTT(DaTaContext, _int_maBN))
                        {
                            if (grvDonThuocct.GetFocusedRowCellValue(colIDDonct) != null && grvDonThuocct.GetFocusedRowCellValue(colIDDonct).ToString() != "")
                            {
                                xoaid = Convert.ToInt32(grvDonThuocct.GetFocusedRowCellValue(colIDDonct).ToString());
                                var dThuocct = DaTaContext.DThuoccts.FirstOrDefault(o => o.IDDonct == xoaid);
                                if (grvDonThuocct.GetFocusedRowCellValue(colMaDV) != null && grvDonThuocct.GetFocusedRowCellValue(colMaDV).ToString() != "")
                                {
                                    bool kt = true;
                                    if (grvDonThuocct.GetFocusedRowCellValue(colStatus).ToString() == "1")
                                    {
                                        kt = false;
                                    } 
                                    if (kt)
                                    {
                                        DialogResult _result = MessageBox.Show("Xóa thuốc: " + grvDonThuocct.GetFocusedRowCellDisplayText(colMaDV), "xóa chi tiết!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                        if (_result == DialogResult.Yes)
                                        {
                                            if (xoaid > 0)
                                            {
                                                var xoa = DaTaContext.DThuoccts.Single(p => p.IDDonct == xoaid);
                                                DaTaContext.DThuoccts.Remove(xoa);
                                                DaTaContext.SaveChanges();
                                                grvDonThuocct.DeleteSelectedRows();
                                            }
                                            else
                                            {
                                                grvDonThuocct.DeleteSelectedRows();
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Bệnh nhân đã thanh toán, Bạn không thể xóa!");
                        }
                        break;
                }
            }
        }

        private void grvDonThuocct_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            QLBVEntities DaTaContext = new QLBVEntities(DungChung.Bien.StrCon);
            int madv = 0;
            double qslSua = 0;
            btnSave.Enabled = true;
            madv = Convert.ToInt32(grvDonThuocct.GetRowCellValue(e.RowHandle, colMaDV));
            var qslSua1 = DaTaContext.DThuoccts.Where(p => p.IDDon == _iddon).Where(p => p.MaDV == madv).ToList();
            if (qslSua1.Count > 0)
            {
                qslSua = qslSua1.First().SoLuongct;
            }
            switch (e.Column.Name)
            {
                #region colMaDV
                case "colMaDV":
                    if (grvDonThuocct.GetRowCellValue(e.RowHandle, colMaDV) != null && grvDonThuocct.GetRowCellValue(e.RowHandle, colMaDV).ToString() != "")
                    {
                        List<DThuocct> listDT = new List<DThuocct>();
                        listDT = DaTaContext.DThuoccts.Where(p => p.MaDV == madv).Where(p => p.IDDon == _iddon).ToList();
                        if (listDT.Count > 0)
                        {
                            var a = listDT.First();
                            sothangSua = a.SoLuong / a.SoLuongct;
                            grvDonThuocct.SetRowCellValue(e.RowHandle, SoLuongCu, a.SoLuong );
                        }
                        grvDonThuocct.SetRowCellValue(e.RowHandle, colNgayKe, DateTime.Now);
                    }
                    break;

                #endregion

                #region colSoLuongct
                case "colSoLuongct":
                    if (grvDonThuocct.GetRowCellValue(e.RowHandle, colSoLuongct) != null && grvDonThuocct.GetRowCellValue(e.RowHandle, colSoLuongct).ToString() != "")
                    {
                        if (grvDonThuocct.GetRowCellValue(e.RowHandle, colSoLuongct).ToString() != "0")
                        {
                            double slct = Convert.ToDouble(grvDonThuocct.GetRowCellValue(e.RowHandle, colSoLuongct));
                            slct = slct > 0 ? -slct : slct;
                            double soluong = 0;
                            soluong = slct * sothangSua;
                            if (-Convert.ToDouble(grvDonThuocct.GetRowCellValue(e.RowHandle, colSoLuongct)) > qslSua)
                            {
                                MessageBox.Show(" Số lượng thuốc trả không được lớn hơn số lượng thuốc kê cho bệnh nhân! \n Số lượng đã kê cho bệnh nhân : "  + qslSua, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                grvDonThuocct.SetRowCellValue(e.RowHandle, colSoLuongct, "0");
                                grvDonThuocct.SetRowCellValue(e.RowHandle, colSoLuong, "0");
                                return;
                            }
                            grvDonThuocct.SetRowCellValue(e.RowHandle, colSoLuong, soluong);
                            if (Convert.ToDouble(grvDonThuocct.GetRowCellValue(e.RowHandle, colSoLuongct)) > 0)
                            {
                                grvDonThuocct.SetRowCellValue(e.RowHandle, colSoLuongct, slct);
                            }
                        }
                        else
                        {
                            grvDonThuocct.SetRowCellValue(e.RowHandle, colSoLuong, "0");
                        }
                    }
                    break;
                    #endregion
            }
        }

        private void grvDonThuocct_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            grvDonThuocct.SetRowCellValue(e.RowHandle, colNgayKe, DateTime.Now);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            QLBVEntities DaTaContext = new QLBVEntities(DungChung.Bien.StrCon);
            if (_iddonTra != 0) // da co don thuoc
            {
                var dt = DaTaContext.DThuocs.Where(p => p.IDDon == _iddonTra).Single();
                dt.MaCB = lupBSKe.EditValue.ToString();
                dt.NgayKe = lupNgayKe.DateTime;
                DaTaContext.SaveChanges();
                for (int i = 0; i < grvDonThuocct.RowCount - 1; i++)
                {
                    var iddct = grvDonThuocct.GetRowCellValue(i, colIDDonct);
                    if (!string.IsNullOrEmpty(iddct.ToString()) && Convert.ToDouble(iddct) != 0)
                    {
                        int madv = 0;
                        int iddonct = 0;
                        if (grvDonThuocct.GetRowCellValue(i, colMaDV) != null)
                        {
                            madv = Convert.ToInt32(grvDonThuocct.GetRowCellValue(i, colMaDV));
                        }

                        if (grvDonThuocct.GetRowCellValue(i, colIDDonct) != null)
                        {
                            iddonct = Convert.ToInt32(grvDonThuocct.GetRowCellValue(i, colIDDonct));
                        }
                        var edited = DaTaContext.DThuoccts.Where(p => p.IDDonct == iddonct).Single();
                        edited.NgayNhap = Convert.ToDateTime(grvDonThuocct.GetRowCellValue(i, colNgayKe));
                        edited.SoLuong = Convert.ToDouble(grvDonThuocct.GetRowCellValue(i, colSoLuong));
                        edited.SoLuongct = Convert.ToDouble(grvDonThuocct.GetRowCellValue(i, colSoLuongct));
                        edited.ThanhTien = Convert.ToDouble(grvDonThuocct.GetRowCellValue(i, colSoLuong)) * edited.DonGia * edited.TyLeTT / 100;
                        edited.MaCB = lupBSKe.EditValue.ToString();
                        DaTaContext.SaveChanges();
                    }
                    else
                    {
                        int madv = 0;
                        if (grvDonThuocct.GetRowCellValue(i, colMaDV) != null)
                        {
                            madv = Convert.ToInt32(grvDonThuocct.GetRowCellValue(i, colMaDV));
                        }
                        var dtct = DaTaContext.DThuoccts.Where(p => p.MaDV == madv).Where(p => p.IDDon == _iddon).Where(p => p.Status == 1).First();
                        if (dtct != null)
                        {
                            DThuocct dthuocct = new DThuocct();
                            dthuocct.MaCB = lupBSKe.EditValue.ToString();
                            dthuocct.IDKB = dtct.IDKB;
                            dthuocct.MaKP = dtct.MaKP;
                            dthuocct.IDDon = _iddonTra;
                            dthuocct.MaKXuat = dtct.MaKXuat;
                            dthuocct.NgayNhap = Convert.ToDateTime(grvDonThuocct.GetRowCellValue(i, colNgayKe));
                            dthuocct.MaDV = Convert.ToInt32(grvDonThuocct.GetRowCellValue(i, colMaDV));
                            dthuocct.DonVi = dtct.DonVi;
                            dthuocct.DonGia = dtct.DonGia;
                            dthuocct.SoLuong = Convert.ToDouble(grvDonThuocct.GetRowCellValue(i, colSoLuong));
                            dthuocct.SoLuongct = Convert.ToDouble(grvDonThuocct.GetRowCellValue(i, colSoLuongct));
                            dthuocct.Loai = dtct.Loai;
                            dthuocct.ThanhTien = Convert.ToDouble(grvDonThuocct.GetRowCellValue(i, colSoLuong)) * dtct.DonGia * dtct.TyLeTT / 100;
                            dthuocct.DuongD = dtct.DuongD;
                            dthuocct.SoLan = dtct.SoLan;
                            dthuocct.MoiLan = dtct.MoiLan;
                            dthuocct.Luong = dtct.Luong;
                            dthuocct.DviUong = dtct.DviUong;
                            dthuocct.MaCC = dtct.MaCC;
                            dthuocct.GhiChu = dtct.GhiChu;
                            dthuocct.TrongBH = dtct.TrongBH;
                            dthuocct.Status = 0;
                            dthuocct.TyLeTT = dtct.TyLeTT;
                            dthuocct.SoLo = dtct.SoLo;
                            dthuocct.HanDung = dtct.HanDung;
                            dthuocct.MienCT = dtct.MienCT;
                            DaTaContext.DThuoccts.Add(dthuocct);
                            DaTaContext.SaveChanges();
                        }
                    }
                }
            }
            else //tao moi
            {
                try
                {
                    var dt = DaTaContext.DThuocs.Where(p => p.IDDon == _iddon).Single();
                    var makp = dt.MaKP;
                    DThuoc a = new DThuoc();
                    a.MaBNhan = dt.MaBNhan;
                    a.MaKP = dt.MaKP;
                    a.MaCB = lupBSKe.EditValue.ToString();
                    a.MaKXuat = dt.MaKXuat;
                    a.PLDV = dt.PLDV;
                    a.NgayKe = lupNgayKe.DateTime;
                    a.KieuDon = dt.KieuDon;
                    DaTaContext.DThuocs.Add(a);
                    if (DaTaContext.SaveChanges() > 0)
                    {
                        _iddonTra = a.IDDon;
                        for (int i = 0; i < grvDonThuocct.RowCount - 1; i++)
                        {
                            int madv = 0;
                            if (grvDonThuocct.GetRowCellValue(i, colMaDV) != null)
                            {
                                madv = Convert.ToInt32(grvDonThuocct.GetRowCellValue(i, colMaDV));
                            }
                            var dtct = DaTaContext.DThuoccts.Where(p => p.MaDV == madv).Where(p => p.IDDon == _iddon).Where(p => p.Status == 1).First();
                            if (dtct != null)
                            {
                                DThuocct dthuocct = new DThuocct();
                                dthuocct.MaCB = lupBSKe.EditValue.ToString();
                                dthuocct.IDKB = dtct.IDKB;
                                dthuocct.MaKP = dtct.MaKP;
                                dthuocct.IDDon = _iddonTra;
                                dthuocct.MaKXuat = dtct.MaKXuat;
                                dthuocct.NgayNhap = Convert.ToDateTime(grvDonThuocct.GetRowCellValue(i, colNgayKe));
                                dthuocct.MaDV = Convert.ToInt32(grvDonThuocct.GetRowCellValue(i, colMaDV));
                                dthuocct.DonVi = dtct.DonVi;
                                dthuocct.DonGia = dtct.DonGia;
                                dthuocct.SoLuong = Convert.ToInt32(grvDonThuocct.GetRowCellValue(i, colSoLuong));
                                dthuocct.SoLuongct = Convert.ToInt32(grvDonThuocct.GetRowCellValue(i, colSoLuongct));
                                dthuocct.Loai = dtct.Loai;
                                dthuocct.ThanhTien = Convert.ToInt32(grvDonThuocct.GetRowCellValue(i, colSoLuong)) * dtct.DonGia * dtct.TyLeTT / 100;
                                dthuocct.DuongD = dtct.DuongD;
                                dthuocct.SoLan = dtct.SoLan;
                                dthuocct.MoiLan = dtct.MoiLan;
                                dthuocct.Luong = dtct.Luong;
                                dthuocct.DviUong = dtct.DviUong;
                                dthuocct.MaCC = dtct.MaCC;
                                dthuocct.GhiChu = dtct.GhiChu;
                                dthuocct.TrongBH = dtct.TrongBH;
                                dthuocct.Status = 0;
                                dthuocct.TyLeTT = dtct.TyLeTT;
                                dthuocct.SoLo = dtct.SoLo;
                                dthuocct.HanDung = dtct.HanDung;
                                dthuocct.MienCT = dtct.MienCT;
                                DaTaContext.DThuoccts.Add(dthuocct);
                                DaTaContext.SaveChanges();
                            }
                        }
                    }
                    else
                        MessageBox.Show("khong thanh cong");
                }
                catch (Exception)
                {
                    MessageBox.Show("Không thể lưu!");
                }
                
                

            }

            frm_TraThuocNgoaiTru_Load(null, null);
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            QLBVEntities DaTaContext = new QLBVEntities(DungChung.Bien.StrCon);
            if (_iddonTra != 0)
            {
                var listDTCT = DaTaContext.DThuoccts.Where(p => p.IDDon == _iddonTra).ToList();
                foreach (var item in listDTCT)
                {
                    if (item.Status == 1) // đơn trạng thái đã lĩnh k được xóa
                    {
                        MessageBox.Show("Đơn trả đã được lĩnh, không thể xóa!");
                        return;
                    }     
                }
                DialogResult dr = MessageBox.Show("Bạn muốn xóa đơn thuốc này?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    var dt = DaTaContext.DThuocs.Where(p => p.IDDon == _iddonTra ).Single();
                    DaTaContext.DThuocs.Remove(dt);
                    foreach (var item in listDTCT)
                    {
                        DaTaContext.DThuoccts.Remove(item);
                    }
                    DaTaContext.SaveChanges();
                    frm_TraThuocNgoaiTru_Load(null, null);
                }     
            }
            else
            {
                MessageBox.Show("Không hợp lệ!");
            }
        }

        private void grvDonThuocct_UnboundExpressionEditorCreated(object sender, DevExpress.XtraGrid.Views.Base.UnboundExpressionEditorEventArgs e)
        {

        }

        private void grvDonThuocct_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
           
        }

        private void grvDonThuocct_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {

        }

        private void grvDonThuocct_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {

        }

        private void btnInDon_Click(object sender, EventArgs e)
        {
            if (_iddonTra > 0)
                DungChung.Ham.InDon(_iddonTra, _int_maBN, _maKP, 0, true, 0, true);
            else
                MessageBox.Show("Không có dữ liệu!");
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            CheckReadonly(false);
        }
    }
}
