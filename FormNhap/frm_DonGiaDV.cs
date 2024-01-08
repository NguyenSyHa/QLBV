using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraEditors.DXErrorProvider;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraEditors.Repository;

namespace QLBV.FormNhap
{
    public partial class frm_DonGiaDV : DevExpress.XtraEditors.XtraForm
    {
        public frm_DonGiaDV()
        {
            InitializeComponent();
        }
        List<DichVu> lDV = new List<DichVu>();
        List<DonGiaDV> _lDongia = new List<DonGiaDV>();
        QLBV_Database.QLBVEntities data= new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        void loadList()
        {
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            lDV = data.DichVus.Where(p => p.PLoai == 1 && p.Status == 1).OrderBy(p => p.TenDV).ToList();
            _lDongia = data.DonGiaDVs.ToList();

        }
        private void frm_DonGiaDV_Load(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            loadList();
            slupTimThuocChuaChon.Properties.DataSource = lDV;
            radChon.SelectedIndex = 0;
            TimKiemThuoc();

        }
        int _maDV = 0;
        private void TimKiem()
        {
            _maDV = 0;
            if (grvThuocChuaChon.GetFocusedRowCellValue(colMaDV) != null)
                _maDV = Convert.ToInt32(grvThuocChuaChon.GetFocusedRowCellValue(colMaDV));
            List<DonGiaDV> qdv = (from dv in _lDongia.Where(p => p.MaDV == _maDV)
                                  select dv
                          ).ToList();
            bindingSource1.DataSource = qdv;
            grc_dongia.DataSource = bindingSource1;
        }

        private void lupTimTenDV_EditValueChanged(object sender, EventArgs e)
        {
            TimKiemThuoc();
        }

        DonGiaDV sua = new DonGiaDV();
        private void grv_dongia_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            if (checkDThuocCT_setColumnError(sender, e))
            {
                QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                if (sua != null)
                {
                    #region delete
                    DonGiaDV dgxoa = data.DonGiaDVs.Where(p => p.MaDV == _maDV).Where(p => p.DonGiaN == sua.DonGiaN && p.DonGiaX_BH == sua.DonGiaX_BH && p.DonGiaX_DV == sua.DonGiaX_DV  && p.DonGiaCT == sua.DonGiaCT ).FirstOrDefault();
                    if (dgxoa != null)
                    {
                        data.DonGiaDVs.Remove(dgxoa);
                        data.SaveChanges();
                    }
                    #endregion
                }
                #region insert
                double dongiaN = 0; double dongiaX_BH = 0; Double dongiaX_DV = 0; double dongiaCT = 0; double VAT = 0; bool status = false;
                string SoLo = "";
                DateTime? HanDung  = null;
                dongiaN = Convert.ToDouble(grv_dongia.GetRowCellValue(grv_dongia.FocusedRowHandle, colDongiaN));
                VAT = Convert.ToDouble(grv_dongia.GetRowCellValue(grv_dongia.FocusedRowHandle, colVAT));
                dongiaX_BH = Convert.ToDouble(grv_dongia.GetRowCellValue(grv_dongia.FocusedRowHandle, colDonGiaX_BH));
                dongiaX_DV = Convert.ToDouble(grv_dongia.GetRowCellValue(grv_dongia.FocusedRowHandle, colDonGiaX_DV));
                dongiaCT = Convert.ToDouble(grv_dongia.GetRowCellValue(grv_dongia.FocusedRowHandle, colDonGiaCT));
                status = Convert.ToBoolean(grv_dongia.GetRowCellValue(grv_dongia.FocusedRowHandle, colStatus));
                DateTime ngayhieuluc = Convert.ToDateTime(grv_dongia.GetRowCellValue(grv_dongia.FocusedRowHandle, colNgayHieuLuc));
                //SoLo = (grv_dongia.GetRowCellValue(grv_dongia.FocusedRowHandle, colSoLo)).ToString();
                //if(grv_dongia.GetRowCellValue(grv_dongia.FocusedRowHandle, colHanDung) != null)
                // HanDung = Convert.ToDateTime(grv_dongia.GetRowCellValue(grv_dongia.FocusedRowHandle, colHanDung));
                DonGiaDV dgdv = new DonGiaDV();
                dgdv.MaDV = _maDV;
                dgdv.VAT = VAT;
                dgdv.DonGiaN = dongiaN;
                dgdv.DonGiaCT = dongiaCT;
                dgdv.DonGiaX_BH = dongiaX_BH;
                dgdv.DonGiaX_DV = dongiaX_DV;
                dgdv.Status = status;
                dgdv.GiaBHGioiHanTT = dongiaN;
                dgdv.NgayHieuLuc = ngayhieuluc;
                dgdv.SoLo = SoLo;
                dgdv.HanDung = HanDung;
                data.DonGiaDVs.Add(dgdv);
                data.SaveChanges();
                loadList();
                TimKiemThuoc();
                #endregion
            }

        }
        private bool checkDThuocCT_setColumnError(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {

            GridView view = sender as GridView;
            GridColumn gia_n = view.Columns["DonGiaN"];
            GridColumn giax_BH = view.Columns["DonGiaX_BH"];
            GridColumn giax_Dv = view.Columns["DonGiaX_DV"];
            GridColumn status = view.Columns["Status"];
            object valN = view.GetRowCellValue(e.RowHandle, gia_n);
            object valX_BH = view.GetRowCellValue(e.RowHandle, giax_BH);
            object valX_DV = view.GetRowCellValue(e.RowHandle, giax_Dv);
            object valStatus = view.GetRowCellValue(e.RowHandle, status);

            if (valN == null || valN.ToString() == string.Empty)
            {
                e.Valid = false;
                view.SetColumnError(colDongiaN, "Bạn chưa nhập giá nhập", ErrorType.Default);
                return false;
            }
            else if (Convert.ToDouble((valN).ToString()) <= 0)
            {
                e.Valid = false;
                view.SetColumnError(colDongiaN, "đơn giá nhập phải lơn hơn 0", ErrorType.Default);
                return false;
            }
            else if (valX_BH == null || valX_BH.ToString() == string.Empty)
            {
                e.Valid = false;
                view.SetColumnError(colDonGiaX_BH, "Bạn chưa nhập giá xuất BH", ErrorType.Default);
                return false;
            }
            else if (Convert.ToDouble((valX_BH).ToString()) <= 0)
            {
                e.Valid = false;
                view.SetColumnError(colDonGiaX_BH, "đơn giá xuất BH phải lơn hơn 0", ErrorType.Default);
                return false;
            }
            else if (valX_DV == null || valX_DV.ToString() == string.Empty)
            {
                e.Valid = false;
                view.SetColumnError(colDonGiaX_DV, "Bạn chưa nhập giá xuất BH", ErrorType.Default);
                return false;
            }
            else if (Convert.ToDouble((valX_DV).ToString()) <= 0)
            {
                e.Valid = false;
                view.SetColumnError(colDonGiaX_DV, "đơn giá xuất BH phải lơn hơn 0", ErrorType.Default);
                return false;
            }

            else
            {
                return true;
            }

        }

        private void grv_dongia_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = ExceptionMode.NoAction;
        }

        private void grv_dongia_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (grv_dongia.FocusedRowHandle < 0)
            {
                sua = null;
                if (_maDV <= 0)
                {
                    grv_dongia.Columns["DonGiaN"].OptionsColumn.AllowEdit = false;
                    grv_dongia.Columns["DonGiaX_BH"].OptionsColumn.AllowEdit = false;
                    grv_dongia.Columns["DonGiaX_DV"].OptionsColumn.AllowEdit = false;
                    grv_dongia.Columns["DonGiaCT"].OptionsColumn.AllowEdit = false;
                }
                else
                {
                    grv_dongia.Columns["DonGiaN"].OptionsColumn.AllowEdit = true;
                    grv_dongia.Columns["DonGiaX_BH"].OptionsColumn.AllowEdit = true;
                    grv_dongia.Columns["DonGiaX_DV"].OptionsColumn.AllowEdit = true;
                    grv_dongia.Columns["DonGiaCT"].OptionsColumn.AllowEdit = true;
                }
            }
            else
            {
                grv_dongia.Columns["DonGiaN"].OptionsColumn.AllowEdit = false;
                grv_dongia.Columns["DonGiaCT"].OptionsColumn.AllowEdit = false;
                grv_dongia.Columns["DonGiaX_BH"].OptionsColumn.AllowEdit = false;
                grv_dongia.Columns["DonGiaX_DV"].OptionsColumn.AllowEdit = false;

                double dongiaN = 0; double dongiaX_BH = 0; Double dongiaX_DV = 0; double dongiaCT = 0; double VAT = 0; bool status = false;
                dongiaN = Convert.ToDouble(grv_dongia.GetRowCellValue(grv_dongia.FocusedRowHandle, colDongiaN));
                VAT = Convert.ToDouble(grv_dongia.GetRowCellValue(grv_dongia.FocusedRowHandle, colVAT));
                dongiaCT = Convert.ToDouble(grv_dongia.GetRowCellValue(grv_dongia.FocusedRowHandle, colDonGiaCT));
                dongiaX_BH = Convert.ToDouble(grv_dongia.GetRowCellValue(grv_dongia.FocusedRowHandle, colDonGiaX_BH));
                dongiaX_DV = Convert.ToDouble(grv_dongia.GetRowCellValue(grv_dongia.FocusedRowHandle, colDonGiaX_DV));
                status = Convert.ToBoolean(grv_dongia.GetRowCellValue(grv_dongia.FocusedRowHandle, colStatus));
                QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                sua = data.DonGiaDVs.Where(p => p.MaDV == _maDV).Where(p => p.DonGiaN == dongiaN && p.DonGiaX_BH == dongiaX_BH && p.DonGiaX_DV == dongiaX_DV && p.DonGiaCT == dongiaCT).FirstOrDefault();
            }
        }

        private void grv_dongia_Click(object sender, EventArgs e)
        {
            // grv_dongia_FocusedRowChanged(sender, null);
        }

        private void grv_dongia_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            //QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            //int row = e.RowHandle;

            //if (e.Column.Name == "colXoact")
            //{
            //    if (sua != null)
            //    {
            //        DialogResult dialogResult = MessageBox.Show("Bạn muốn xóa đơn giá này ?", "Xác nhận xóa", MessageBoxButtons.YesNo);
            //        if (dialogResult == DialogResult.Yes)
            //        {
            //            DonGiaDV dgxoa = data.DonGiaDVs.Where(p => p.MaDV == _maDV).Where(p => p.DonGiaN == sua.DonGiaN && p.DonGiaX_BH == sua.DonGiaX_BH && p.DonGiaX_DV == sua.DonGiaX_DV && p.Status == sua.Status).FirstOrDefault();
            //            if (dgxoa != null)
            //            {
            //                data.DonGiaDVs.Remove(dgxoa);
            //                data.SaveChanges();
            //            }
            //            TimKiem();
            //        }

            //    }
            //    else
            //        TimKiem();
            //}


        }

        private void grv_dongia_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            sua = null;
            grv_dongia.SetRowCellValue(e.RowHandle, colVAT, 5);
            grv_dongia.SetRowCellValue(e.RowHandle, colPTGia,0);
            grv_dongia.SetRowCellValue(e.RowHandle, colNgayHieuLuc, DateTime.Now);
        }

        private void grv_dongia_MouseDown(object sender, MouseEventArgs e)
        {
            //if ((Control.ModifierKeys & Keys.Control) != Keys.Control)
            //{
            //    GridView view = sender as GridView;
            //    GridHitInfo hi = view.CalcHitInfo(e.Location);
            //    if (hi.InRowCell)
            //    {
            //        view.FocusedRowHandle = hi.RowHandle;
            //        view.FocusedColumn = hi.Column;
            //        view.ShowEditor();
            //        if (view.ActiveEditor.Properties.GetType() == typeof(RepositoryItemImageComboBox))
            //            (e as DevExpress.Utils.DXMouseEventArgs).Handled = true;
            //    }
            //}
        }

        private void btnXoact_5_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

            if (sua != null)
            {
                DialogResult dialogResult = MessageBox.Show("Bạn muốn xóa đơn giá này ?", "Xác nhận xóa", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    DonGiaDV dgxoa = data.DonGiaDVs.Where(p => p.MaDV == _maDV).Where(p => p.DonGiaN == sua.DonGiaN && p.DonGiaX_BH == sua.DonGiaX_BH && p.DonGiaX_DV == sua.DonGiaX_DV && p.VAT == sua.VAT && p.Status == sua.Status && p.DonGiaCT == sua.DonGiaCT).FirstOrDefault();
                    if (dgxoa != null)
                    {
                        data.DonGiaDVs.Remove(dgxoa);
                        data.SaveChanges();
                    }
                    TimKiemThuoc();
                }

            }
            else
                TimKiemThuoc();

        }

        private void frm_DonGiaDV_FormClosing(object sender, FormClosingEventArgs e)
        {
            //grv_dongia.FocusedColumn = colDongiaN;
            //grv_dongia_ValidateRow(null, null);
        }

        private void grv_dongia_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            double dongiaCT = 0; double VAT = 0, trietkhau = 0; double dongiaN = 0; double dongiaX_BH = 0; double dongiaX_DV = 0;
            if (e.Column.Name == "colDonGiaCT")
            {
                if (e.Value != null)
                {
                    dongiaCT = Convert.ToDouble(e.Value);
                    if (grv_dongia.GetRowCellValue(e.RowHandle, colVAT) != null)
                    {
                        trietkhau = Convert.ToDouble(grv_dongia.GetRowCellValue(e.RowHandle, colPTGia));
                        trietkhau = (100 - trietkhau) / 100;
                        VAT = Convert.ToDouble(grv_dongia.GetRowCellValue(e.RowHandle, colVAT));
                        grv_dongia.SetRowCellValue(e.RowHandle, colDongiaN, trietkhau * dongiaCT * (1 + VAT / 100));
                        grv_dongia.SetRowCellValue(e.RowHandle, colDonGiaX_BH, trietkhau * dongiaCT * (1 + VAT / 100));
                        grv_dongia.SetRowCellValue(e.RowHandle, colDonGiaX_DV, trietkhau * dongiaCT * (1 + VAT / 100));
                    }
                }

            }
            else if (e.Column.Name == "colVAT")
            {
                if (e.Value != null)
                {
                    VAT = Convert.ToDouble(e.Value);
                    if (grv_dongia.GetRowCellValue(e.RowHandle, colDonGiaCT) != null)
                    {
                        trietkhau = Convert.ToDouble(grv_dongia.GetRowCellValue(e.RowHandle, colPTGia));
                        trietkhau = (100 - trietkhau) / 100;
                        dongiaCT = Convert.ToDouble(grv_dongia.GetRowCellValue(e.RowHandle, colDonGiaCT));
                        grv_dongia.SetRowCellValue(e.RowHandle, colDongiaN,trietkhau* dongiaCT * (1 + VAT / 100));
                        grv_dongia.SetRowCellValue(e.RowHandle, colDonGiaX_BH,trietkhau* dongiaCT * (1 + VAT / 100));
                        grv_dongia.SetRowCellValue(e.RowHandle, colDonGiaX_DV, trietkhau * dongiaCT * (1 + VAT / 100));
                    }
                }

            }
            else if (e.Column.Name == "colPTGia")
            {
                trietkhau = Convert.ToDouble(e.Value);
                trietkhau = (100 - trietkhau) / 100;
                if (grv_dongia.GetRowCellValue(e.RowHandle, colDonGiaCT) != null)
                {
                    VAT = Convert.ToDouble(grv_dongia.GetRowCellValue(e.RowHandle, colVAT));
                    dongiaCT = Convert.ToDouble(grv_dongia.GetRowCellValue(e.RowHandle, colDonGiaCT));
                    grv_dongia.SetRowCellValue(e.RowHandle, colDongiaN, trietkhau * dongiaCT * (1 + VAT / 100));
                    grv_dongia.SetRowCellValue(e.RowHandle, colDonGiaX_BH, trietkhau * dongiaCT * (1 + VAT / 100));
                    grv_dongia.SetRowCellValue(e.RowHandle, colDonGiaX_DV, trietkhau * dongiaCT * (1 + VAT / 100));
                }
            }


        }

        private void txtTimThuocDaChon_EditValueChanged(object sender, EventArgs e)
        {

            TimKiemThuoc();
        }
        void TimKiemThuoc()
        {
            int maDV = 0;
            string ten = txtTimThuocDaChon.Text.ToLower();
            int.TryParse(txtTimThuocDaChon.Text, out maDV);
            int chon = radChon.SelectedIndex;
            grcThuocChuaChon.DataSource = (from a in lDV
                                           join b in _lDongia on a.MaDV equals b.MaDV into c
                                           from kq in c.DefaultIfEmpty()
                                           where chon == 0 ? kq == null : kq != null
                                           where a.MaDV==maDV || a.TenDV.ToLower().Contains(ten) || (a.MaTam != null && a.MaTam.ToLower().Contains(ten) )
                                           select a
                              ).Distinct().ToList();
        }

        private void grvThuocChuaChon_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            TimKiem();
        }

        private void grvThuocChuaChon_DataSourceChanged(object sender, EventArgs e)
        {
            TimKiem();
        }

        private void radChon_SelectedIndexChanged(object sender, EventArgs e)
        {
            TimKiemThuoc();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var dsthuoc=(from nd in data.NhapDs.Where(p=>p.PLoai==1) join ndct in data.NhapDcts.Where(p=>p.MaDV!=null && p.DonGiaCT>0 && p.VAT>0) on nd.IDNhap equals ndct.IDNhap
                                               select new {ndct.MaDV, ndct.DonGiaCT, ndct.VAT,ndct.DonGia }).ToList().Distinct().ToList();
            int dem = 0;
            foreach (var item in dsthuoc)
            {
                var kt = _lDongia.Where(p => p.MaDV == item.MaDV && p.DonGiaCT == item.DonGiaCT && p.DonGiaN == item.DonGia).FirstOrDefault();
                if (kt == null)
                {
                    dem++;
                    DonGiaDV moi = new DonGiaDV();
                    moi.MaDV = item.MaDV??0;
                    moi.VAT = item.VAT;
                    moi.DonGiaCT = item.DonGiaCT;
                    moi.DonGiaN = item.DonGia;
                    moi.DonGiaX_BH = item.DonGia;
                    moi.DonGiaX_DV = item.DonGia;
                    moi.Status = true;
                    data.DonGiaDVs.Add(moi);
                    data.SaveChanges();
                }
            }
            MessageBox.Show("update thành công: "+dem+ " bản ghi");
            this.frm_DonGiaDV_Load(sender, e);
        }

    }
}