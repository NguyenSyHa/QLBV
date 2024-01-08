using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using QLBV.Models.Dictionaries.Thuoc;
using QLBV.Providers.Business.Medicines;
using QLBV_Database;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QLBV.Forms.Medicines
{
    public partial class Frm_KhoaThuoc : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly MedicinesProvider _medicinesProvider;

        int maKho = 0;
        int maDV = 0;

        IList<MedicineInventoryModel> medicinesByRoom = new List<MedicineInventoryModel>();

        public Frm_KhoaThuoc()
        {
            InitializeComponent();
            _medicinesProvider = new MedicinesProvider();
        }

        private void Frm_KhoaThuoc_Load(object sender, EventArgs e)
        {
            lupKhoDuoc.Properties.DataSource = _medicinesProvider.GetListKhoaPhong(DungChung.Bien.MaCB, 0);
        }

        private void lupKhoDuoc_EditValueChanged(object sender, EventArgs e)
        {
            if(lupKhoDuoc.EditValue != null)
            {
                maKho = Convert.ToInt32(lupKhoDuoc.EditValue);

                medicinesByRoom = _medicinesProvider.GetLupMaDuoc(maKho, -1,0).Distinct(new MedicineInventoryModelComparer1()).ToList();
                grcDMDuoc.DataSource = medicinesByRoom;
            }
        }

        private void grvDMDuoc_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (grvDMDuoc.GetRowCellValue(e.RowHandle, colMaDV) != null)
                maDV = Convert.ToInt32(grvDMDuoc.GetRowCellValue(e.RowHandle, colMaDV));

            LoadThuoc();
        }

        private void btnKhoa_Click(object sender, EventArgs e)
        {
            var medicines = GetListSelected(grvDonThuocct);

            if(medicines.Count > 0)
            {
                _medicinesProvider.LockMedicine(medicines, true);
                LoadThuoc();
            }
        }

        private void btnMoKhoa_Click(object sender, EventArgs e)
        {
            var medicines = GetListSelected(grvDonThuocctLocked);

            if (medicines.Count > 0)
            {
                _medicinesProvider.LockMedicine(medicines, false);
                LoadThuoc();
            }
        }

        private List<MedicineInventoryModel> GetListSelected(GridView gridView)
        {
            List<MedicineInventoryModel> medModels = new List<MedicineInventoryModel>();
            if (gridView.SelectedRowsCount > 0)
            {
                for (int i = 0; i < gridView.RowCount; i++)
                {
                    if (gridView.IsRowSelected(i))
                    {
                        medModels.Add((MedicineInventoryModel)gridView.GetRow(i));
                    }
                }
            }

            return medModels;
        }
        private void LoadThuoc()
        {
            grcDonThuocct.DataSource = _medicinesProvider.GetLupMaDuoc(maKho, -1,0).Where(p => p.MaDV == maDV && p.Lock == false).ToList();
            grcDonThuocctLocked.DataSource = _medicinesProvider.GetLupMaDuoc(maKho, -1,0).Where(p => p.MaDV == maDV && p.Lock == true).ToList();
        }

        private void txtTenThuoc_TextChanged(object sender, EventArgs e)
        {
            if (medicinesByRoom.Count > 0)
            {
                if (txtTenThuoc.Text != "")
                    grcDMDuoc.DataSource = medicinesByRoom.Where(p => p.TenDV.ToLower().Contains(txtTenThuoc.Text.ToLower()) || p.MaDV.ToString() == txtTenThuoc.Text).ToList();
                else
                    grcDMDuoc.DataSource = medicinesByRoom;
            }
        }
    }

}
