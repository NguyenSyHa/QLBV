using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace QLBV.FormDanhMuc
{
    public partial class us_ThongBao : DevExpress.XtraEditors.XtraUserControl
    {
        public us_ThongBao()
        {
            InitializeComponent();
        }

        private void labelControl6_Click(object sender, EventArgs e)
        {

        }
        QLBV_Database.QLBVEntities data;
        List<CanBo> _lCB = new List<CanBo>();
        bool New = true, Edit = true, Delete = true;
        private void us_ThongBao_Load(object sender, EventArgs e)
        {
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            dtNgayNhap.DateTime = DateTime.Now;
            _lCB = data.CanBoes.Where(p => p.Status == 1).ToList();
            lupCanBo.Properties.DataSource = _lCB.OrderBy(p => p.TenCB);
            cboStatus.SelectedIndex = 1;
            cboTTTK.SelectedIndex = 2;
            cboBPhanTK.SelectedIndex = 8;
            dtTuNgay.DateTime = DateTime.Now;
            dtDenNgay.DateTime = DateTime.Now;
            EnableControl(false);
            var KtraPer = data.Permissions.Where(p => p.ID == 9050).FirstOrDefault();
            if (KtraPer != null)
            {
                New = KtraPer.C_New;
                Edit = KtraPer.C_Edit;
                Delete = KtraPer.C_Delete;
            }
            if (!New)
                btnThemMoi.Enabled = false;
            if (!Edit)
                btnSua.Enabled = false;
            if (!Delete)
                btnXoa.Enabled = false;

        }
        public class NDThongBao
        {
            public int ID { get; set; }
            public string TenCB { get; set; }
            public DateTime NgayNhap { get; set; }
            public string NoiDung { get; set; }
            public string PhanLoai { get; set; }
            public int Status { get; set; }
        }
        List<ThongBao> _lTB = new List<ThongBao>();
        List<NDThongBao> _lKetqua = new List<NDThongBao>();
        void LoadDSThongBao()
        {
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            _lKetqua.Clear();
            DateTime _tungay = DungChung.Ham.NgayTu(dtTuNgay.DateTime);
            DateTime _denngay = DungChung.Ham.NgayDen(dtDenNgay.DateTime);
            int status = cboTTTK.SelectedIndex;
            string ploai = cboBPhanTK.Text;
            _lTB = data.ThongBaos.Where(p => p.NgayNhap >= _tungay && p.NgayNhap <= _denngay).Where(p => status == 2 ? true : p.Status == status).Where(p => ploai == "Tất cả" ? true : p.PLoai == ploai).ToList();
            foreach (var item in _lTB)
            {
                NDThongBao moi = new NDThongBao();
                moi.ID = item.ID;
                var tencb = _lCB.Where(p => p.MaCB == item.MaCB).Select(p => p.TenCB).FirstOrDefault();
                if (tencb != null)
                    moi.TenCB = tencb;
                else
                    moi.TenCB = "";
                moi.NgayNhap = Convert.ToDateTime(item.NgayNhap);
                moi.NoiDung = item.NDung;
                moi.PhanLoai = item.PLoai;
                moi.Status = item.Status;
                _lKetqua.Add(moi);
            }
            grcDSThongBao.DataSource = null;
            grcDSThongBao.DataSource = _lKetqua;
        }
        private void EnableControl(bool T)
        {
            dtNgayNhap.Properties.ReadOnly = !T;
            mmNDung.Properties.ReadOnly = !T;
            cboPhanLoai.Properties.ReadOnly = !T;
            cboStatus.Properties.ReadOnly = !T;
            lupCanBo.Properties.ReadOnly = !T;
            btnLuu.Enabled = T;
            if (New)
                btnThemMoi.Enabled = !T;
            if (Edit)
                btnSua.Enabled = !T;
            if (Delete)
                btnXoa.Enabled = !T;
            grcDSThongBao.Enabled = !T;
        }
        private void resetcontrol()
        {
            cboStatus.SelectedIndex = 1;
            mmNDung.Text = "";
            cboPhanLoai.SelectedIndex = 0;
            dtNgayNhap.DateTime = DateTime.Now;
            lupCanBo.EditValue = null;
        }
        private void grvDSThongBao_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {

            if (grvDSThongBao.GetFocusedRowCellValue(colID) != null)
            {
                int ID = Convert.ToInt32(grvDSThongBao.GetFocusedRowCellValue(colID));
                txtID.Text = ID.ToString();
                ThongBao TB = _lTB.Where(p => p.ID == ID).FirstOrDefault();
                if (TB != null)
                {
                    dtNgayNhap.DateTime = Convert.ToDateTime(TB.NgayNhap);
                    lupCanBo.EditValue = TB.MaCB;
                    cboPhanLoai.Text = TB.PLoai;
                    cboStatus.SelectedIndex = TB.Status;
                    mmNDung.Text = TB.NDung;
                }
            }
        }
        int TTLuu = 0;//1 themmoi, 2 sua
        private void simpleButton4_Click(object sender, EventArgs e)
        {
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            if (TTLuu == 1)
            {
                ThongBao moi = new ThongBao();
                moi.NDung = mmNDung.Text;
                moi.MaCB = lupCanBo.EditValue != null ? lupCanBo.EditValue.ToString() : "";
                moi.Status = cboStatus.SelectedIndex;
                moi.PLoai = cboPhanLoai.Text;
                moi.NgayNhap = dtNgayNhap.DateTime;
                data.ThongBaos.Add(moi);
                data.SaveChanges();
                LoadDSThongBao();
                EnableControl(false);
            }
            if (TTLuu == 2)
            {
                int ID = Convert.ToInt32(txtID.Text);
                ThongBao sua = data.ThongBaos.Where(p => p.ID == ID).FirstOrDefault();
                sua.NDung = mmNDung.Text;
                sua.MaCB = lupCanBo.EditValue != null ? lupCanBo.EditValue.ToString() : "";
                sua.Status = cboStatus.SelectedIndex;
                sua.PLoai = cboPhanLoai.Text;
                sua.NgayNhap = dtNgayNhap.DateTime;
                data.SaveChanges();
                LoadDSThongBao();
                EnableControl(false);
            }
        }

        private void btnThemMoi_Click(object sender, EventArgs e)
        {
            resetcontrol();
            EnableControl(true);
            TTLuu = 1;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            EnableControl(true);
            TTLuu = 2;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            DialogResult Result = MessageBox.Show("Bạn muốn xóa thông báo này ?", "Hỏi xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(Result==DialogResult.Yes)
            {
                int ID = Convert.ToInt32(txtID.Text);
                ThongBao xoa = data.ThongBaos.Where(p => p.ID == ID).FirstOrDefault();
                data.ThongBaos.Remove(xoa);
                data.SaveChanges();
                LoadDSThongBao();
                EnableControl(false);
            }
        }

        private void dtTuNgay_EditValueChanged(object sender, EventArgs e)
        {
            LoadDSThongBao();
        }

        private void dtDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            LoadDSThongBao();
        }

        private void cboBPhanTK_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDSThongBao();
        }

        private void cboTTTK_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDSThongBao();
        }
        //void TimKiem()
        //{

        //}
    }
}
