using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GridView = DevExpress.XtraGrid.Views.Grid.GridView;
using GridColumn = DevExpress.XtraGrid.Columns.GridColumn;

namespace QLBV.FormNhap
{
    public partial class us_HoTro : UserControl
    {
        QLBV_Database.QLBVEntities _data;
        List<HoTro> _lHoTro = new List<HoTro>();
        string action = "";
        string ploai = "";
        public us_HoTro()
        {
            InitializeComponent();
            _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        }

        private void us_HoTro_Load(object sender, EventArgs e)
        {
            var kp = _data.KPhongs.Where(p => p.MaKP == DungChung.Bien.MaKP).ToList();
            ploai = kp.First().PLoai;
            if (kp.Count > 0)
            {
                if (ploai == "Admin")
                {
                    pnDisplayTop.Enabled = false;
                    pnDisplayTop.Visible = true;
                    pnControl.Visible = true;
                    colStatus.Visible = true;
                }
                else
                {
                    pnDisplayTop.Visible = false;
                    pnControl.Visible = false;
                    colStatus.Visible = false;
                }
            }
            enableControl(true);
            loadSource();
        }
        private void loadSource()
        {
            if (ploai == "Admin")
            {
                _lHoTro = _data.HoTroes.OrderBy(p => p.SoTT).ToList();
            }
            else
            {
                _lHoTro = _data.HoTroes.Where(p => p.Status == true).OrderBy(p => p.SoTT).ToList();
            }
            grcCanBo.DataSource = _lHoTro;
        }
        private void grvCanBo_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (grvCanBo.GetSelectedRows().Count() > 0)
            {
                int indexRow = grvCanBo.GetSelectedRows()[0]; // Lấy row đang trỏ đến
                txtTenNV.Text = getValueGrid(grvCanBo, colTenNV, indexRow);
                txtPhongBan.Text = getValueGrid(grvCanBo, colPhongBan, indexRow);
                txtChucVu.Text = getValueGrid(grvCanBo, colChucVu, indexRow);
                txtSDT1.Text = getValueGrid(grvCanBo, colSDT1, indexRow);
                txtSDT2.Text = getValueGrid(grvCanBo, colSDT2, indexRow);
                txtDiaChi.Text = getValueGrid(grvCanBo, colDiaChi, indexRow);
                txtBietDanh.Text = getValueGrid(grvCanBo, colBietDanh, indexRow);
                txtSTT.Text = getValueGrid(grvCanBo, colSTT, indexRow);
                if (getValueGrid(grvCanBo, colStatus, indexRow).ToString() == "True")
                {
                    chkHienThi.Checked = true;
                }
                else chkHienThi.Checked = false;
            }
            else
            {
                refresh();
            }
        }
        private string getValueGrid(GridView grv, GridColumn column, int index)
        {
            if (grv.GetRowCellValue(index, column) != null)
            {
                return grv.GetRowCellValue(index, column).ToString();
            }
            return "";
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            action = "edit";
            enableControl(false);
            pnDisplayTop.Enabled = true;
        }
        private HoTro createHoTro(HoTro h, string TenNV, string ChucVu, string PhongBan, string SDT1, string SDT2, string DiaChi, string BietDanh, bool status, int STT)
        {
            h.TenNV = TenNV;
            h.ChucVu = ChucVu;
            h.PhongBan = PhongBan;
            h.SoDT1 = SDT1;
            h.SoDT2 = SDT2;
            h.DiaChi = DiaChi;
            h.BietDanh = BietDanh;
            h.SoTT = STT;
            h.Status = status;
            return h;
        }

        private void btnMoi_Click(object sender, EventArgs e)
        {
            action = "new";
            pnDisplayTop.Enabled = true;
            enableControl(false);
            refresh();
        }
        private bool kt()
        {
            if (txtTenNV.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập Tên Nhân Viên.");
                txtTenNV.Focus();
                return false;
            }
            if (txtSTT.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập Số TT.");
                txtSTT.Focus();
                return false;
            }
            if (txtChucVu.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập Chức vụ.");
                txtChucVu.Focus();
                return false;
            }
            return true;
        }
        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (kt())
            {
                if (action == "new")
                {
                    int STT = Convert.ToInt32(txtSTT.Text.Trim());
                    HoTro moi = new HoTro();
                    moi = createHoTro(moi, txtTenNV.Text.Trim(), txtChucVu.Text.Trim(), txtPhongBan.Text.Trim(), txtSDT1.Text.Trim(), txtSDT2.Text.Trim(), txtDiaChi.Text.Trim(), txtBietDanh.Text.Trim(), chkHienThi.Checked, STT);
                    _data.HoTroes.Add(moi);
                    _data.SaveChanges();
                    MessageBox.Show("Thêm thành công!");
                }
                if (action == "edit")
                {
                    if (grvCanBo.GetSelectedRows().Count() == 0)
                    {
                        MessageBox.Show("Bạn chưa chọn Cán bộ.!");
                    }
                    else
                    {
                        int indexRow = grvCanBo.GetSelectedRows()[0];
                        int STT = Convert.ToInt32(txtSTT.Text.Trim());
                        if (indexRow >= 0)
                        {
                            int id = Convert.ToInt32(getValueGrid(grvCanBo, colIDNV, indexRow));
                            var q = _data.HoTroes.Where(p => p.IDNV == id).ToList();
                            if (q.Count > 0)
                            {
                                HoTro sua = q.First();
                                sua = createHoTro(sua, txtTenNV.Text.Trim(), txtChucVu.Text.Trim(), txtPhongBan.Text.Trim(), txtSDT1.Text.Trim(), txtSDT2.Text.Trim(), txtDiaChi.Text.Trim(), txtBietDanh.Text.Trim(), chkHienThi.Checked, STT);
                                _data.SaveChanges();
                                MessageBox.Show("Sửa thành công!");
                            }
                        }
                    }
                }
                loadSource();
                enableControl(true);
            }
        }
        private void enableControl(bool T)
        {
            btnXoa.Enabled = T;
            btnSua.Enabled = T;
            btnMoi.Enabled = T;
            btnLuu.Enabled = !T;
            pnDisplayTop.Enabled = !T;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (grvCanBo.GetSelectedRows().Count() == 0)
            {
                MessageBox.Show("Bạn chưa chọn Cán bộ để xóa!");
            }
            else
            {
                int indexRow = grvCanBo.GetSelectedRows()[0];
                if (indexRow >= 0)
                {
                    int id = Convert.ToInt32(getValueGrid(grvCanBo, colIDNV, indexRow));
                    var q = _data.HoTroes.Where(p => p.IDNV == id).ToList();
                    if (q.Count > 0)
                    {
                        DialogResult _result = MessageBox.Show("Bạn có muốn xóa Cán Bộ này không?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (_result == DialogResult.Yes)
                        {
                            HoTro h = _data.HoTroes.Where(p => p.IDNV == id).First();
                            _data.HoTroes.Remove(h);
                            _data.SaveChanges();
                            loadSource();
                        }
                    }
                }
                enableControl(true);
            }

        }

        private void textEdit1_EditValueChanged(object sender, EventArgs e)
        {
            string str = textEdit1.Text;
            List<HoTro> _lHoTro = _data.HoTroes.Where(p => p.TenNV.Contains(str)).ToList();
            grcCanBo.DataSource = _lHoTro;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            enableControl(true);
            refresh();
        }
        private void refresh()
        {
            grvCanBo.SelectRow(-1);
            txtTenNV.Text = "";
            txtChucVu.Text = "";
            txtPhongBan.Text = "";
            txtBietDanh.Text = "";
            chkHienThi.Checked = false;
            txtDiaChi.Text = "";
            txtSDT1.Text = "";
            txtSDT2.Text = "";
            txtSTT.Text = "";
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var q = _data.HoTroes.Where(p=>p.Status == true).ToList().OrderBy(p=>p.SoTT).ThenBy(p=>p.PhongBan).ThenBy(p=>p.TenNV);
            BaoCao.rep_CanBoHoTro rep = new BaoCao.rep_CanBoHoTro();
            rep.DataSource = q;
            rep.DataBindding();
            rep.CreateDocument();
            frmIn frm = new frmIn();
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();
        }
    }
}
