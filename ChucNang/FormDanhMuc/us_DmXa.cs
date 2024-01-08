using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;

namespace QLBV.FormDanhMuc
{
    public partial class us_DmXa : DevExpress.XtraEditors.XtraUserControl
    {
        public us_DmXa()
        {
            InitializeComponent();
        }
        int TTLuu = 0;
        //int TTXoa = 0;
        string _maxa = "";
        private void enableControl(bool T)
        {
            txtMaXa.Properties.ReadOnly = !T;
            txtTenXa.Properties.ReadOnly = !T;
            lupMaTinh.Properties.ReadOnly = !T;
            lupMaHuyen.Properties.ReadOnly = !T;
            btnLuu.Enabled = T;
            btnMoi.Enabled = !T;
            btnSua.Enabled = !T;
            btnXoa.Enabled = !T;
            grcDmXa.Enabled = !T;
        }
        private void resetControl()
        {
            txtMaXa.Text = "";
            txtTenXa.Text = "";
            lupMaTinh.Text = "";
            lupMaHuyen.Text = "";
        }
        #region
        private bool KTLuu()
        {
            if (string.IsNullOrEmpty(txtMaXa.Text))
            {
                MessageBox.Show("Bạn chưa nhập mã xã");
                txtMaXa.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtTenXa.Text))
            {
                MessageBox.Show("Bạn chưa nhập tên xã");
                txtTenXa.Focus();
                return false;
            }

            return true;
        }

        #endregion
        QLBV_Database.QLBVEntities dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        List<DmXa> _lXa = new List<DmXa>();
        private void us_DmXa_Load(object sender, EventArgs e)
        {
            var qt = from dmt in dataContext.DmTinhs select new { dmt.TenTinh, dmt.MaTinh };
            lupMaTinh.Properties.DataSource = qt;
            _lXa = dataContext.DmXas.OrderBy(p => p.TenXa).ToList();
    
            //_lXa = (from dmx in dataContext.DmXas
            //        join dmt in dataContext.DmTinhs on dmx.MaTinh equals dmt.MaTinh
            //        join dmh in dataContext.DmHuyens on dmx.MaHuyen equals dmh.MaHuyen
            //        group new { dmx, dmt, dmh } by new { dmx.MaXa, dmx.TenXa, dmh.TenHuyen, dmt.TenTinh } into kq
            //        select new
            //        {
            //            MaXa = kq.Key.MaXa,
            //            TenXa = kq.Key.TenXa,
            //            MaTinh = kq.Key.TenHuyen,
            //            MaHuyen = kq.Key.TenTinh
            //        });
            grcDmXa.DataSource = _lXa;
            enableControl(false);
        }
        private void btnMoi_Click(object sender, EventArgs e)
        {
            enableControl(true);
            resetControl();
            TTLuu = 1;
            txtMaXa.Focus();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            enableControl(true);
            txtMaXa.Enabled = false;
            TTLuu = 2;
            txtTenXa.Focus();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            _maxa = txtMaXa.Text;
            var ma = dataContext.TTboXungs.Where(p => p.MaXa== (_maxa)).ToList();
            if (ma.Count > 0)
            {
                MessageBox.Show("Mã xã đã được sử dụng, Bạn không thể xóa.");
            }
            else
            {
                DataRow dr = grvDmXa.GetFocusedDataRow();
                DialogResult dia = MessageBox.Show("Bạn có chắc chắn muốn xóa xã đã chọn?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (dia == DialogResult.Yes)
                {
                    //grcDanToc.ControlRemoved();
                    var xoa = dataContext.DmXas.Single(p => p.MaXa== (_maxa));
                    dataContext.DmXas.Remove(xoa);
                    dataContext.SaveChanges();
                    btnXoa.Enabled = true;

                }
                _lXa = dataContext.DmXas.OrderBy(p => p.TenXa).ToList();
                grcDmXa.DataSource = _lXa.ToList();
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (KTLuu())
            {
                switch (TTLuu)
                {
                    case 1:
                        _maxa = txtMaXa.Text.Trim();
                        var ma = dataContext.DmXas.Where(p => p.MaXa== (_maxa)).ToList();
                        if (ma.Count > 0)
                        {
                            MessageBox.Show("Mã xã đã có, vui lòng nhập mã khác");
                        }
                        else
                        {
                            DmXa xa = new DmXa();
                            xa.MaXa = txtMaXa.Text;
                            xa.TenXa = txtTenXa.Text;
                            xa.MaTinh = lupMaTinh.Text;
                            xa.MaHuyen = lupMaHuyen.Text;
                            dataContext.DmXas.Add(xa);
                            dataContext.SaveChanges();
                            enableControl(false);
                            MessageBox.Show("Lưu thành công!");
                        }
                        break;
                    case 2:
                        if (!string.IsNullOrEmpty(txtMaXa.Text))
                        {
                            string maxa = txtMaXa.Text;
                            DmXa xasua = dataContext.DmXas.Single(p => p.MaXa== (maxa));
                            xasua.TenXa = txtTenXa.Text;
                            xasua.MaTinh = lupMaTinh.Text;
                            xasua.MaHuyen = lupMaHuyen.Text;
                            dataContext.SaveChanges();
                            MessageBox.Show("Lưu thành công!");
                            enableControl(false);
                        }
                        break;
                }
                _lXa = dataContext.DmXas.OrderBy(p => p.TenXa).ToList();
                grcDmXa.DataSource = _lXa.ToList();
            }
        }

        private void groupControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtTimKiem_EditValueChanged(object sender, EventArgs e)
        {
            string tk = "";
            if (!string.IsNullOrEmpty(txtTimKiem.Text))
            {
                tk = txtTimKiem.Text;
            }
            grcDmXa.DataSource = _lXa.Where(p => p.TenXa.Contains(tk));
        }

        private void grvDmXa_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (grvDmXa.GetFocusedRowCellValue(colMaXa) != null && grvDmXa.GetFocusedRowCellValue(colMaXa).ToString() != "")
            {
                txtMaXa.Text = grvDmXa.GetFocusedRowCellValue(colMaXa).ToString();
                if (grvDmXa.GetFocusedRowCellValue(colTenXa) != null && grvDmXa.GetFocusedRowCellValue(colTenXa).ToString() != "")
                {
                    txtTenXa.Text = grvDmXa.GetFocusedRowCellValue(colTenXa).ToString();
                }
                else
                {
                    txtTenXa.Text = "";
                }

                if (grvDmXa.GetFocusedRowCellValue(colTinh) != null && grvDmXa.GetFocusedRowCellValue(colTinh).ToString() != "")
                {
                    lupMaTinh.Text = grvDmXa.GetFocusedRowCellValue(colTinh).ToString();
                }
                else
                {
                    lupMaTinh.Text = "";
                }
                if (grvDmXa.GetFocusedRowCellValue(colHuyen) != null && grvDmXa.GetFocusedRowCellValue(colHuyen).ToString() != "")
                {
                    lupMaHuyen.Text = grvDmXa.GetFocusedRowCellValue(colHuyen).ToString();
                }
                else
                {
                    lupMaHuyen.Text = "";
                }
            }
            else
            {
                txtMaXa.Text = "";
            }
        
        }

        private void lupMaTinh_EditValueChanged(object sender, EventArgs e)
        {
            string matinh = "";

            if (lupMaTinh.EditValue != null && lupMaTinh.EditValue.ToString() != "")
            {
                matinh = lupMaTinh.Text;
                var qh = from dmt in dataContext.DmHuyens.Where(p => p.MaTinh == matinh) select new { dmt.TenHuyen, dmt.MaHuyen };
                lupMaHuyen.Properties.DataSource = qh;
            }
        }

      
    }
}
