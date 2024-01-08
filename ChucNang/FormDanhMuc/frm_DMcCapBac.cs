using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace QLBV.FormThamSo
{
    public partial class frm_DMcCapBac : DevExpress.XtraEditors.XtraForm
    {
        public frm_DMcCapBac()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        int x = -1, y = 0;
        private class cb
        {
            private int iD_CB;
            private string ten_CB1, moTa, status;
            public string Status
            {
                get { return status; }
                set { status = value; }
            }

            public string MoTa
            {
                get { return moTa; }
                set { moTa = value; }
            }

            public string Ten_CB1
            {
                get { return ten_CB1; }
                set { ten_CB1 = value; }
            }

            public int ID_CB
            {
                get { return iD_CB; }
                set { iD_CB = value; }
            }
        }
        List<cb> _cv = new List<cb>();
        private void frm_DMChucVu_Load(object sender, EventArgs e)
        {
            _cv.Clear();
            TenCV.Enabled = false;
            MoTa1.Enabled = false;
            checkSuDung.Enabled = false;
            var ds = _data.CapBacs.ToList();
            foreach(var item in ds)
            {
                cb moi = new cb();
                moi.ID_CB = item.ID_CB;
                moi.Ten_CB1 = item.Ten_CB;
                moi.MoTa = item.MoTa;
                if (item.Status == 0)
                    moi.Status = "Không sử dụng";
                else
                    moi.Status = "Sử dụng";
                _cv.Add(moi);
            }
            gridControl1.DataSource = _cv.ToList();
            gridView1_FocusedRowChanged(null, null);
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (TenCV.Text != "" && TenCV.Text != null)
            {
                if (y == 0)
                {
                    CapBac moi = new CapBac();
                    moi.Ten_CB = TenCV.Text;
                    moi.MoTa = MoTa1.Text;
                    moi.Status = (checkSuDung.Checked == false ? Convert.ToByte(0) : Convert.ToByte(1));
                    _data.CapBacs.Add(moi);
                    if (_data.SaveChanges() >= 0)
                    {
                        MessageBox.Show("Lưu thành công");
                        simpleButton2.Enabled = true;
                        simpleButton1.Enabled = false;
                    }
                }
                else
                {
                    CapBac moi = _data.CapBacs.Single(p => p.ID_CB == x);
                    moi.Ten_CB = TenCV.Text;
                    moi.MoTa = MoTa1.Text;
                    moi.Status = (checkSuDung.Checked == false ? Convert.ToByte(0) : Convert.ToByte(1));
                    if (_data.SaveChanges() >= 0)
                    {
                        MessageBox.Show("Lưu thành công");
                        simpleButton2.Enabled = true;
                        simpleButton1.Enabled = false;
                    }

                }
                frm_DMChucVu_Load(sender, e);
                TenCV.Enabled = false;
                MoTa1.Enabled = false;
                checkSuDung.Enabled = false;
                TenCV.Text = "";
                MoTa1.Text = "";
                checkSuDung.Checked = false;
                gridControl1.Enabled = true;
                frm_DMChucVu_Load(sender, e);
            }
            else
            {
                MessageBox.Show("Tên cấp bậc không được để trống!");
            }
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            TenCV.Enabled = true;
            MoTa1.Enabled = true;
            checkSuDung.Enabled = true;
            simpleButton1.Enabled = true;
            TenCV.Text = "";
            MoTa1.Text = "";
            checkSuDung.Checked = true;
            gridControl1.Enabled = false;
            y = 0;
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            TenCV.Enabled = false;
            MoTa1.Enabled = false;
            checkSuDung.Enabled = false;
            TenCV.Text = "";
            MoTa1.Text = "";
            checkSuDung.Checked = false;
            frm_DMChucVu_Load(sender, e);
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            TenCV.Enabled = true;
            MoTa1.Enabled = true;
            checkSuDung.Enabled = true;
            gridControl1.Enabled = false;
            simpleButton1.Enabled = true;
            y = 1;
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gridView1.GetFocusedRowCellValue(ID_CB) != null)
            {
                simpleButton2.Enabled = true;
                x = Convert.ToInt32(gridView1.GetFocusedRowCellValue(ID_CB).ToString());
                var laydl = _data.CapBacs.Where(p => p.ID_CB == x).ToList();
                TenCV.Text = laydl.First().Ten_CB;
                MoTa1.Text = laydl.First().MoTa;
                checkSuDung.Checked = (laydl.First().Status == 0 ? false : true);
            }

        }

        private void bnt_xoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn xóa không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                CapBac moi = _data.CapBacs.Single(p => p.ID_CB == x);
                _data.CapBacs.Remove(moi);
                if (_data.SaveChanges() >= 0)
                {
                    MessageBox.Show("Xóa thành công");
                }
                x = -1;
                frm_DMChucVu_Load(sender, e);
            }
            
        }
    }
}