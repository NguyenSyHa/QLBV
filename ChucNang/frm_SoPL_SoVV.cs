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
    public partial class frm_SoPL_SoVV : DevExpress.XtraEditors.XtraForm
    {
        public frm_SoPL_SoVV()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        public class MyObject
        {
            public int Value { set; get; }
            public string Text { set; get; }
        }
        bool Them = false, Sua = false, Xoa = false;
        private void frm_SoPL_SoVV_Load(object sender, EventArgs e)
        {
            List<KPhong> qkp = data.KPhongs.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.LamSang || p.PLoai == DungChung.Bien.st_PhanLoaiKP.PhongKham).ToList();
            qkp.Insert(0, new KPhong { MaKP = 0, TenKP = "" });
            lupKP.Properties.DataSource = qkp;
            lupKPtimKiem.Properties.DataSource = qkp;
            lupKPHT.DataSource = qkp;
            dtNgayThang.DateTime = DateTime.Now.Date;
            string[] phanloai = new string[] { "", "Số phiếu lĩnh", "Số vào viện", "Số Khám bệnh", "Số bệnh án", "Số chuyển viện", "Số TT thanh toán trong ngày", "Số lưu trữ", "Mã y tế", "Số ra viện", "Số nghỉ ốm", "Số chứng từ nghỉ ốm" };
            lupPLoai.Properties.Items.AddRange(phanloai);
            lupPLoai.SelectedIndex = 2;

            List<MyObject> noitru = new List<MyObject>();
            noitru.Add(new MyObject { Value = -1, Text = "" });
            noitru.Add(new MyObject { Value = 0, Text = "Ngoại trú" });
            noitru.Add(new MyObject { Value = 1, Text = "Nội trú" });
            lupNoiNgoaiTru.DataSource = noitru;
            dtNgayThang.DateTime = DateTime.Now;


            TimKiem();
            //if (DungChung.Bien.CapDo == 9)
            if (DungChung.Bien.PLoaiKP == DungChung.Bien.st_PhanLoaiKP.Admin)
                groupControl1.Enabled = true;
            else
                groupControl1.Enabled = false;
            var perm = data.Permissions.Where(p => p.TenDN == DungChung.Bien.TenDN).ToList();
            if (perm.Where(p => p.ID == 9051).ToList().Count > 0 && perm.Where(p => p.ID == 9051).First().C_New == true)
            {
                Them = true;
            }
            if (perm.Where(p => p.ID == 9051).ToList().Count > 0 && perm.Where(p => p.ID == 9051).First().C_Edit == true)
            {
                Sua = true;
            }
            if (perm.Where(p => p.ID == 9051).ToList().Count > 0 && perm.Where(p => p.ID == 9051).First().C_Delete == true)
            {
                Xoa = true;
            }
            if (Them)
                btnNew.Enabled = true;
            btnSave.Enabled = false;
            if (Xoa)
                btnDelete.Enabled = true;
            if (Sua)
                btnSua.Enabled = true;
            lupKP.Enabled = false;
            txtSoVV.Enabled = false;
            dtNgayThang.Enabled = false;
        }
        class Content
        {
            public string TenKP { set; get; }
            public int SoVV { set; get; }
            public DateTime? NgayThang { set; get; }

            public int MaKP { get; set; }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            trangthai = 0;
            txtSoVV.Text = "";
            btnSave.Enabled = true;
            if (Them)
                btnNew.Enabled = false;
            if (Sua)
                btnSua.Enabled = false;
            if (Xoa)
                btnDelete.Enabled = false; ;
            lupKP.Enabled = true;
            txtSoVV.Enabled = true;
            dtNgayThang.Enabled = true;
        }

        int trangthai = -1;// 0: thêm mới; 1: sửa
        SoPL suasoPL = new SoPL();
        private void grv_SoVV_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {

            int makp = -1; int sovv = -1; DateTime ngaythang = DateTime.Now; int noitru = 2;
            if (grv_SoVV.GetFocusedRowCellValue(colKhoaPhong) != null)
            {
                makp = Convert.ToInt32(grv_SoVV.GetFocusedRowCellValue(colKhoaPhong));
                lupKP.EditValue = makp;
            }

            if (grv_SoVV.GetFocusedRowCellValue(colSoVV) != null)
            {
                sovv = Convert.ToInt32(grv_SoVV.GetFocusedRowCellValue(colSoVV));
                txtSoVV.Text = sovv.ToString();
            }
            else
                txtSoVV.Text = "";
            if (grv_SoVV.GetFocusedRowCellValue(colNgayThang) != null)
            {
                ngaythang = Convert.ToDateTime(grv_SoVV.GetFocusedRowCellValue(colNgayThang));
                dtNgayThang.DateTime = ngaythang;
            }
            else
            {
                dtNgayThang.DateTime = DateTime.Now.Date;
            }

            if (grv_SoVV.GetFocusedRowCellValue(colNoiTru) != null)
            {
                noitru = Convert.ToInt32(grv_SoVV.GetFocusedRowCellValue(colNoiTru));
                if (noitru == -1)
                    rdNoiNgoaiTru.SelectedIndex = 2;
                else
                    rdNoiNgoaiTru.SelectedIndex = noitru;
            }
            else
            {
                dtNgayThang.DateTime = DateTime.Now.Date;
            }
            suasoPL = data.SoPLs.Where(p => p.PhanLoai == lupPLoai.SelectedIndex).Where(p => p.SoPL1 == sovv && p.MaKP == makp && p.NgayNhap == dtNgayThang.DateTime.Date).FirstOrDefault();
            if (!string.IsNullOrEmpty(txtSoVV.Text))
            {
                if (Sua)
                    btnSua.Enabled = true;
                if (Xoa)
                    btnDelete.Enabled = true;
                lupKP.Enabled = false;
            }
            else
            {
                if (Sua)
                    btnSua.Enabled = false;
                if (Xoa)
                    btnDelete.Enabled = false; ;
            }
            if (trangthai != 0 && trangthai != 1)
                btnSave.Enabled = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            if (trangthai == 0)// thêm mới
            {
                SoPL moi = new SoPL();
                moi.PhanLoai = lupPLoai.SelectedIndex;
                moi.NgayNhap = dtNgayThang.DateTime.Date;
                if (lupKP.EditValue != null)
                    moi.MaKP = Convert.ToInt32(lupKP.EditValue);
                if (!String.IsNullOrEmpty(txtSoVV.Text))
                    moi.SoPL1 = Convert.ToInt32(txtSoVV.Text);
                moi.PhanLoai = lupPLoai.SelectedIndex;
                moi.NoiTru = rdNoiNgoaiTru.SelectedIndex == 2 ? -1 : rdNoiNgoaiTru.SelectedIndex;
                moi.Status = 1;
                data.SoPLs.Add(moi);
                try
                {
                    if (data.SaveChanges() >= 0)
                        MessageBox.Show("Thêm mới thành công");
                    else
                        MessageBox.Show("Số đã tồn tại");
                }
                catch (Exception)
                {
                    MessageBox.Show("Số đã tồn tại");
                }
            }
            else if (trangthai == 1)
            {
                int sovv = -1;
                if (!String.IsNullOrEmpty(txtSoVV.Text))
                    sovv = Convert.ToInt32(txtSoVV.Text);
                DateTime ngay = dtNgayThang.DateTime.Date;
                SoPL sua = data.SoPLs.Where(p => p.MaKP == suasoPL.MaKP && p.SoPL1 == suasoPL.SoPL1 && p.PhanLoai == lupPLoai.SelectedIndex && p.NgayNhap == suasoPL.NgayNhap).FirstOrDefault();
                if (sua != null)
                {
                    SoPL sua_new = data.SoPLs.Where(p => p.MaKP == suasoPL.MaKP && p.SoPL1 == sovv && p.PhanLoai == lupPLoai.SelectedIndex && p.NgayNhap == ngay).FirstOrDefault();
                    if ((sovv != suasoPL.SoPL1 || ngay != suasoPL.NgayNhap || suasoPL.PhanLoai != lupPLoai.SelectedIndex) && sua_new != null)
                    {
                        MessageBox.Show("Số đã tồn tại");
                        txtSoVV.Focus();
                    }
                    else
                    {
                        sua.PhanLoai = lupPLoai.SelectedIndex;
                        sua.SoPL1 = sovv;
                        sua.NgayNhap = ngay;
                        sua.NoiTru = rdNoiNgoaiTru.SelectedIndex == 2 ? -1 : rdNoiNgoaiTru.SelectedIndex;
                        sua.Status = 1;
                        data.SaveChanges();
                        MessageBox.Show("Sửa thành công");
                    }
                }
            }
            if (Them)
                btnNew.Enabled = true;
            btnSave.Enabled = false;
            lupKP.Enabled = false;
            txtSoVV.Enabled = false;
            dtNgayThang.Enabled = false;
            trangthai = -1;
            suasoPL = new SoPL();
            TimKiem();
        }
        List<SoPL> _lsopl = new List<SoPL>();
        private void TimKiem()
        {
            _lsopl.Clear();
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            int makp = 0; int ploai = 1;
            if (lupKPtimKiem.EditValue != null)
                makp = Convert.ToInt32(lupKPtimKiem.EditValue);
            ploai = lupPLoai.SelectedIndex;
            _lsopl = (from sovv in data.SoPLs.Where(p => p.PhanLoai == ploai).Where(p => p.MaKP == makp)
                      select sovv).OrderBy(p => p.MaKP).ToList();
            grc_SoVV.DataSource = null;
            grc_SoVV.DataSource = _lsopl;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int makp = 0; int sopl = -1; int ploai = lupPLoai.SelectedIndex;
         //   DateTime ngay = dtNgayThang.DateTime.Date;
            if (lupKP.EditValue != null)
                makp = Convert.ToInt32(lupKP.EditValue);
            if (!String.IsNullOrEmpty(txtSoVV.Text))
                sopl = Convert.ToInt32(txtSoVV.Text);
            SoPL sua = data.SoPLs.Where(p => p.MaKP == makp && p.SoPL1 == sopl && p.PhanLoai == ploai ).FirstOrDefault();
            if (sua != null)
            {

                DialogResult _result = MessageBox.Show("Bạn muốn xóa " + lupPLoai.Text + " " + txtSoVV.Text, "hỏi xóa!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (_result == DialogResult.Yes)
                {
                    data.SoPLs.Remove(sua);
                    data.SaveChanges();
                    TimKiem();
                }
            } 
            if (Them)
                btnNew.Enabled = true; ;
            trangthai = -1;

        }

        private void lupKPtimKiem_EditValueChanged(object sender, EventArgs e)
        {
            TimKiem();
            if (lupKPtimKiem.EditValue != null)
            {
                lupKP.EditValue = Convert.ToInt32(lupKPtimKiem.EditValue);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            trangthai = 1;

            txtSoVV.Enabled = true;
            dtNgayThang.Enabled = true;
            btnSave.Enabled = true;
            if (Xoa)
                btnDelete.Enabled = true;
            //int sovv = 0; int makp = 0;
            //suasoPL = new SoPL();
            //if (!string.IsNullOrEmpty(txtSoVV.Text))
            //    sovv = Convert.ToInt32(txtSoVV.Text);
            //if (lupKP.EditValue != null)
            //    makp = Convert.ToInt32(lupKP.EditValue);


            //suasoPL = data.SoPLs.Where(p => p.PhanLoai == lupKPtimKiem.se).Where(p => p.SoPL1 == sovv && p.MaKP == makp && p.NgayNhap == dtNgayThang.DateTime.Date ).FirstOrDefault();

        }




        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void grv_SoVV_Click(object sender, EventArgs e)
        {

        }

        private void grv_SoVV_DataSourceChanged(object sender, EventArgs e)
        {
            grv_SoVV_FocusedRowChanged(null, null);
        }

        private void lupPLoai_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            TimKiem();
        }
    }
}