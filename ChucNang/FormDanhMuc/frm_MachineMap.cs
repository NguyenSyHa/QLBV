using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace QLBV.ChucNang.FormDanhMuc
{
    public partial class frm_MachineMap : DevExpress.XtraEditors.XtraForm
    {
        public frm_MachineMap()
        {
            InitializeComponent();
        }

        int TTLuu = 0;
        int _id;
        

        private void enableControl(bool t)
        {
            txtMachineID.Properties.ReadOnly = !t;
            lupMaDVct.Properties.ReadOnly = !t;
            
            lupTSID.Properties.ReadOnly = !t;
            grcMachine.Enabled = !t;
            btnMoi.Enabled = !t;
            btnLuu.Enabled = t;
            btnSua.Enabled = !t;
            btnXoa.Enabled = !t;
            btnHuy.Enabled = t;
        }

        private void resetControl()
        {
            txtMachineID.Text = "";
            lupTSID.Text = "";
            txtGT.Text = "";
            lupMaDVct.Text = "";
            
        }

        private bool KTLuu()
        {
            //if (string.IsNullOrEmpty(lupTSID.Text))
            //{
                //MessageBox.Show("Bạn chưa nhập mã tài sản");
                //lupTSID.Focus();
                //return false;
            //}

            if (string.IsNullOrEmpty(lupMaDVct.Text))
            {
                MessageBox.Show("Bạn chưa nhập mã dịch vụ chi tiết");
                lupMaDVct.Focus();
                return false;
            }
            
            return true;
        }

        QLBV_Database.QLBVEntities dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        List<LIS_MACHINE_MAP> _machine = new List<LIS_MACHINE_MAP>();
        
        

        private void frm_MachineMap_Load(object sender, EventArgs e)
        {
            var _ts = (from ts in dataContext.TaiSans select new {
                ts.IDTS
            }).ToList();
            lupTSID.Properties.DataSource = _ts;

            var _dvct = (from dvct in dataContext.DichVucts select new {
                dvct.MaDVct,
                dvct.TenDVct
            }).ToList();
            lupMaDVct.Properties.DataSource = _dvct;

            _machine = dataContext.LIS_MACHINE_MAP.OrderBy(p => p.LIS_MACHINE_MAP_ID).ToList();
            grcMachine.DataSource = _machine;
            enableControl(false);           
           
        }

        private void grvMachine_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (grvMachine.GetFocusedRowCellValue(IDMachine) != null )
            {
                int idmay = Convert.ToInt32(grvMachine.GetFocusedRowCellValue(IDMachine));
                txtMachineID.Text = idmay.ToString();
                var a = _machine.Where(p => p.LIS_MACHINE_MAP_ID == idmay).ToList();
                if (grvMachine.GetFocusedRowCellValue(IDTaiSan) != null || grvMachine.GetFocusedRowCellValue(IDTaiSan).ToString() != "")
                {
                    lupTSID.EditValue = a.First().TAISAN_ID.ToString();
                }
                else
                {
                    lupTSID.EditValue = "";
                }

                if (grvMachine.GetFocusedRowCellValue(IDDVct) != null || grvMachine.GetFocusedRowCellValue(IDDVct).ToString() != "")
                {
                    lupMaDVct.EditValue = a.First().MaDVct.ToString();
                }
                else
                {
                    lupMaDVct.EditValue = "";
                }

                if (grvMachine.GetFocusedRowCellValue(MAP_VALUE) != null || grvMachine.GetFocusedRowCellValue(MAP_VALUE).ToString() != "")
                {
                    txtGT.Text = a.First().MAP_VALUE.ToString();
                }
                else
                {
                    txtGT.Text = "";
                }
            }
            else
            {
                txtMachineID.Text = "";
            }
        }

        private void btnMoi_Click(object sender, EventArgs e)
        {
            enableControl(true);
            resetControl();
            TTLuu = 1;
            txtMachineID.Enabled = false;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (KTLuu())
            {
                switch (TTLuu)
                {
                    case 1:
                       
                        var ma = dataContext.LIS_MACHINE_MAP.Where(p => p.LIS_MACHINE_MAP_ID == _id).ToList();
                        if (ma.Count > 0)
                        {
                            MessageBox.Show("Mã máy đã có, vui lòng nhập mã khác");
                        }
                        else
                        {
                            LIS_MACHINE_MAP mac = new LIS_MACHINE_MAP();
                            
                            mac.TAISAN_ID = Convert.ToInt32(lupTSID.EditValue);
                            mac.MaDVct = lupMaDVct.EditValue.ToString();
                            mac.MAP_VALUE = txtGT.Text;
                            
                            dataContext.LIS_MACHINE_MAP.Add(mac);
                            dataContext.SaveChanges();
                            MessageBox.Show("Lưu thành công!");
                            enableControl(false);
                            resetControl();
                            
                        }
                        break;
                    case 2:
                        if (!string.IsNullOrEmpty(txtMachineID.Text))
                        {
                            _id = Convert.ToInt32(txtMachineID.Text);
                            LIS_MACHINE_MAP macsua = dataContext.LIS_MACHINE_MAP.Single(p => p.LIS_MACHINE_MAP_ID == _id);
                            macsua.TAISAN_ID = Convert.ToInt32(lupTSID.EditValue);
                            macsua.MaDVct = lupMaDVct.EditValue.ToString();
                            macsua.MAP_VALUE = txtGT.Text;
                            
                            dataContext.SaveChanges();
                            MessageBox.Show("Lưu thành công!");
                            enableControl(false);
                            resetControl();
                        }
                        break;
                }
                _machine = dataContext.LIS_MACHINE_MAP.OrderBy(p => p.LIS_MACHINE_MAP_ID).ToList();
                grcMachine.DataSource = _machine.ToList();
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            enableControl(true);
            txtMachineID.Enabled = false;
            TTLuu = 2;
            
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            _id = Convert.ToInt32(txtMachineID.Text);
            DataRow dr = grvMachine.GetFocusedDataRow();
            DialogResult dia = MessageBox.Show("Bạn có chắc chắn muốn xóa mẫu đã chọn?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (dia == DialogResult.Yes)
            {

                var xoa = dataContext.LIS_MACHINE_MAP.Single(p => p.LIS_MACHINE_MAP_ID == _id);
                dataContext.LIS_MACHINE_MAP.Remove(xoa);
                dataContext.SaveChanges();
                btnXoa.Enabled = true;

            }
            _machine = dataContext.LIS_MACHINE_MAP.OrderBy(p => p.LIS_MACHINE_MAP_ID).ToList();
            grcMachine.DataSource = _machine.ToList();
            enableControl(false);
            resetControl();
            
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            enableControl(false);
            resetControl();
        }

        private void textEdit1_EditValueChanged(object sender, EventArgs e)
        {
            string tk = "";
            if (!string.IsNullOrEmpty(textEdit1.Text))
            {
                tk = textEdit1.Text;
            }
            grcMachine.DataSource = _machine.Where(p => p.MAP_VALUE.Contains(tk));
        }

        

        
    }
}