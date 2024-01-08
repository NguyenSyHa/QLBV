using System;
using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QLBV.FormNhap
{
    public partial class frm_Machine : Form
    {
        public frm_Machine()
        {
            InitializeComponent();
        }

        int TTLuu;
        int _id;

        private void enableControl(bool t)
        {
            txtMa.Properties.ReadOnly = !t;
            lupTS.Properties.ReadOnly = !t;
            lupDVCT.Properties.ReadOnly = !t;
            txtTen.Properties.ReadOnly = !t;
            btnMoi.Enabled = !t;
            btnSua.Enabled = !t;
            btnXoa.Enabled = !t;
            btnHuy.Enabled = t;
            btnLuu.Enabled = t;
            grcMap.Enabled = !t;
        }

        private void resetControl()
        {
            txtMa.Text = "";
            lupTS.Text = "";
            lupDVCT.Text = "";
            txtTen.Text = "";
            txtTenTriSo.Text = "";
        }

        private bool KTLuu()
        {
            if (string.IsNullOrEmpty(lupDVCT.Text))
            {
                MessageBox.Show("Bạn chưa nhập mã dịch vụ chi tiết");
                lupDVCT.Focus();
                return false;
            }

            return true;
        }

        QLBV_Database.QLBVEntities dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        List<LIS_MACHINE_MAP> map = new List<LIS_MACHINE_MAP>();

        private void frm_Machine_Load(object sender, EventArgs e)
        {
            map = dataContext.LIS_MACHINE_MAP.OrderBy(p => p.LIS_MACHINE_MAP_ID).ToList();
            var map1 = (from lmm in dataContext.LIS_MACHINE_MAP
                        join dvct in dataContext.DichVucts on lmm.MaDVct equals dvct.MaDVct
                        select new { lmm.LIS_MACHINE_MAP_ID, lmm.TAISAN_ID, lmm.MaDVct, dvct.TenDVct, lmm.MAP_VALUE, lmm.TenTriSoMay }).ToList();
            grcMap.DataSource = map1;

            var _ts = (from ts in dataContext.TaiSans
                       select new
                       {
                           ts.IDTS,
                           ts.GhiChu,
                           ts.TinhTrang
                       }).ToList();
            lupTS.Properties.DataSource = _ts;

            var _dvct = (from dvct in dataContext.DichVucts
                         select new
                         {
                             dvct.MaDVct,
                             dvct.TenDVct
                         }).ToList();
            lupDVCT.Properties.DataSource = _dvct;

            enableControl(false);
        }

        private void grvMap_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (grvMap.GetFocusedRowCellValue(colMa) != null)
            {
                int ma = Convert.ToInt32(grvMap.GetFocusedRowCellValue(colMa));
                txtMa.Text = ma.ToString();

                var _map = dataContext.LIS_MACHINE_MAP.Where(p => p.LIS_MACHINE_MAP_ID == ma).ToList();

                if (grvMap.GetFocusedRowCellValue(colTS) != null && grvMap.GetFocusedRowCellValue(colTS).ToString() != "")
                {
                    lupTS.EditValue = grvMap.GetFocusedRowCellValue(colTS).ToString();
                }
                else
                {
                    lupTS.EditValue = "";
                }

                if (grvMap.GetFocusedRowCellValue(colDVCT) != null && grvMap.GetFocusedRowCellValue(colDVCT).ToString() != "")
                {
                    lupDVCT.EditValue = grvMap.GetFocusedRowCellValue(colDVCT).ToString();
                }
                else
                {
                    lupDVCT.EditValue = "";
                }

                if (grvMap.GetFocusedRowCellValue(colValue) != null && grvMap.GetFocusedRowCellValue(colValue).ToString() != "")
                {
                    txtTen.Text = grvMap.GetFocusedRowCellValue(colValue).ToString();
                }
                else
                {
                    txtTen.Text = "";
                }

                if (grvMap.GetFocusedRowCellValue(colTenTriSo) != null && grvMap.GetFocusedRowCellValue(colTenTriSo).ToString() != "")
                {
                    txtTenTriSo.Text = grvMap.GetFocusedRowCellValue(colTenTriSo).ToString();
                }
                else
                {
                    txtTenTriSo.Text = "";
                }
            }
            else
            {
                txtMa.Text = "";
            }
        }

        private void btnMoi_Click(object sender, EventArgs e)
        {
            resetControl();
            enableControl(true);
            TTLuu = 1;
            txtMa.ReadOnly = true;
            txtTen.Focus();

        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (KTLuu())
            {
                switch (TTLuu)
                {
                    case 1:
                        LIS_MACHINE_MAP _map = new LIS_MACHINE_MAP();
                        _map.TAISAN_ID = lupTS.Text == null || lupTS.Text == "" ? 0 : Convert.ToInt32(lupTS.Text);
                        _map.MaDVct = lupDVCT.Text;
                        _map.MAP_VALUE = txtTen.Text;
                        _map.TenTriSoMay = txtTenTriSo.Text;
                        dataContext.LIS_MACHINE_MAP.Add(_map);
                        dataContext.SaveChanges();
                        MessageBox.Show("Lưu thành công!");
                        enableControl(false);
                        resetControl();
                        break;

                    case 2:
                        if (!string.IsNullOrEmpty(txtMa.Text))
                        {
                            _id = Convert.ToInt32(txtMa.Text);
                            LIS_MACHINE_MAP sua = dataContext.LIS_MACHINE_MAP.Single(p => p.LIS_MACHINE_MAP_ID == _id);
                            sua.TAISAN_ID = Convert.ToInt32(lupTS.Text);
                            sua.MaDVct = lupDVCT.Text;
                            sua.MAP_VALUE = txtTen.Text;
                            sua.TenTriSoMay = txtTenTriSo.Text;
                            dataContext.SaveChanges();
                            MessageBox.Show("Lưu thành công!");
                            enableControl(false);
                            resetControl();
                        }
                        break;
                }
                var map1 = (from lmm in dataContext.LIS_MACHINE_MAP
                            join dvct in dataContext.DichVucts on lmm.MaDVct equals dvct.MaDVct
                            select new { lmm.LIS_MACHINE_MAP_ID, lmm.TAISAN_ID, lmm.MaDVct, dvct.TenDVct, lmm.MAP_VALUE, lmm.TenTriSoMay }).ToList();
                grcMap.DataSource = map1;
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            enableControl(true);
            txtMa.ReadOnly = true;
            TTLuu = 2;
            txtTen.Focus();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            int ID = 0;
            if (grvMap.GetFocusedRowCellValue(colMa) != null)
            {
                ID = (int)grvMap.GetFocusedRowCellValue(colMa);
                DialogResult dia = MessageBox.Show("Bạn có chắc chắn muốn xóa?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (dia == DialogResult.Yes)
                {
                    var xoa = dataContext.LIS_MACHINE_MAP.Single(p => p.LIS_MACHINE_MAP_ID == ID);
                    dataContext.LIS_MACHINE_MAP.Remove(xoa);
                    dataContext.SaveChanges();
                    btnXoa.Enabled = true;
                }
                map = dataContext.LIS_MACHINE_MAP.OrderBy(p => p.LIS_MACHINE_MAP_ID).ToList();
                grcMap.DataSource = map;
                enableControl(false);
                resetControl();
            }


        }

        private void txtTimKiem_EditValueChanged(object sender, EventArgs e)
        {
            string tk = "";
            if (!string.IsNullOrEmpty(txtTimKiem.Text))
            {
                tk = txtTimKiem.Text;
            }
            grcMap.DataSource = map.Where(p => p.MAP_VALUE.Contains(tk));
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            resetControl();
            enableControl(false);
        }
    }
}
