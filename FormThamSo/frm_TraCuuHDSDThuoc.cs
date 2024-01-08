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
    public partial class frm_TraCuuHDSDThuoc : DevExpress.XtraEditors.XtraForm
    {
        public frm_TraCuuHDSDThuoc()
        {
            InitializeComponent();
        }
        int x = 0;
        public class dichvu
        {
            private int maDV;

            public int MaDV
            {
                get { return maDV; }
                set { maDV = value; }
            }
            private string tenDV;

            public string TenDV
            {
                get { return tenDV; }
                set { tenDV = value; }
            }
            private string donVi;

            public string DonVi
            {
                get { return donVi; }
                set { donVi = value; }
            }
            private string duongD;

            public string DuongD
            {
                get { return duongD; }
                set { duongD = value; }
            }
            private string hamLuong;

            public string HamLuong
            {
                get { return hamLuong; }
                set { hamLuong = value; }
            }
            private string duocLucHoc;

            public string DuocLucHoc
            {
                get { return duocLucHoc; }
                set { duocLucHoc = value; }
            }
            private string chiDinh;

            public string ChiDinh
            {
                get { return chiDinh; }
                set { chiDinh = value; }
            }
            private string chongChiDinh;

            public string ChongChiDinh
            {
                get { return chongChiDinh; }
                set { chongChiDinh = value; }
            }
            private string lDCD;

            public string LDCD
            {
                get { return lDCD; }
                set { lDCD = value; }
            }
            private string tuongTacThuoc;

            public string TuongTacThuoc
            {
                get { return tuongTacThuoc; }
                set { tuongTacThuoc = value; }
            }
            private string maDuongDung;

            public string MaDuongDung
            {
                get { return maDuongDung; }
                set { maDuongDung = value; }
            }
            private string maTam;

            public string MaTam
            {
                get { return maTam; }
                set { maTam = value; }
            }
            private string soDK;

            public string SoDK
            {
                get { return soDK; }
                set { soDK = value; }
            }
        }
        QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        List<dichvu> _ldichvu = new List<dichvu>();
        private void ktt(bool t)
        {
            txtTenThuoc.Enabled = !t;
            cbbDonVi.Enabled = !t;
            txtHamLuong.Enabled = !t;
            txtDuongDung.Enabled = !t;
            txtDuocLucHoc.Enabled = !t;
            txtChongChiDinh.Enabled = !t;
            txtTuongTacThuoc.Enabled = !t;
            txtChiDinh.Enabled = !t;
            txtLDCD.Enabled = !t;
            simpleButton3.Enabled = !t;
            simpleButton4.Enabled = !t;
            simpleButton1.Enabled = t;
            simpleButton2.Enabled = t;
        }
        void timkiem()
        {
            try
            {
                string _timkiem = "";
                if (!string.IsNullOrEmpty(txtTimKiem.Text) && txtTimKiem.Text != "Tìm mã/ Tên thuốc/ Số đăng ký")
                    _timkiem = txtTimKiem.Text;
                _ldichvu.Clear();
                var dv = _data.DichVus.Where(p => p.PLoai == 1).ToList();
                foreach (var item in dv)
                {
                    dichvu them = new dichvu();
                    them.MaDV = item.MaDV;
                    them.TenDV = item.TenDV;
                    them.HamLuong = item.HamLuong;
                    them.MaDuongDung = item.MaDuongDung;
                    them.DonVi = item.DonVi;
                    if (!string.IsNullOrWhiteSpace(item.GhiChu))
                    {
                        string[] arr = item.GhiChu.Split(';');
                        if (arr != null)
                        {
                            for (int i = 0; i < arr.Count(); i++)
                            {
                                if (i == 0)
                                    them.DuocLucHoc = arr[0];
                                if (i == 1)
                                    them.ChiDinh = arr[1];
                                if (i == 2)
                                    them.ChongChiDinh = arr[2];
                                if (i == 3)
                                    them.LDCD = arr[3];
                                if (i == 4)
                                    them.TuongTacThuoc = arr[4];
                            }
                        }
                    }
                    them.DuongD = item.DuongD;
                    them.SoDK = item.SoDK;
                    them.MaTam = item.MaTam;
                    _ldichvu.Add(them);
                }
                grcDichVu.DataSource = _ldichvu.Where(p => p.TenDV.ToLower().Contains(_timkiem.ToLower()) || p.SoDK == _timkiem || Convert.ToString(p.MaDV) == _timkiem).ToList();
                grvDichVu_FocusedRowChanged(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tìm kiếm: " + ex.Message);
            }
        }

        private void txtTimKiem_EditValueChanged(object sender, EventArgs e)
        {
            timkiem();
        }

        private void frm_TraCuuHDSDThuoc_Load(object sender, EventArgs e)
        {
            ktt(true);
            _ldichvu.Clear();
            var dv = _data.DichVus.Where(p => p.PLoai == 1).ToList();
            foreach (var item in dv)
            {
                dichvu them = new dichvu();
                them.MaDV = item.MaDV;
                them.TenDV = item.TenDV;
                them.HamLuong = item.HamLuong;
                them.DonVi = item.DonVi;
                if (!string.IsNullOrWhiteSpace(item.GhiChu))
                {
                    string[] arr = item.GhiChu.Split(';');
                    if (arr != null)
                    {
                        for (int i = 0; i < arr.Count(); i++)
                        {
                            if (i == 0)
                                them.DuocLucHoc = arr[0];
                            if (i == 1)
                                them.ChiDinh = arr[1];
                            if (i == 2)
                                them.ChongChiDinh = arr[2];
                            if (i == 3)
                                them.LDCD = arr[3];
                            if (i == 4)
                                them.TuongTacThuoc = arr[4];
                        }
                    }
                }
                them.DuongD = item.DuongD;
                them.SoDK = item.SoDK;
                them.MaTam = item.MaTam;
                _ldichvu.Add(them);
            }
            grcDichVu.DataSource = _ldichvu.ToList();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            ktt(false);
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            ktt(true);
            DichVu sua = _data.DichVus.Single(p => p.MaDV == x);
            sua.TenDV = txtTenThuoc.Text;
            sua.HamLuong = txtHamLuong.Text;
            sua.DonVi = cbbDonVi.Text;
            sua.GhiChu = txtDuocLucHoc.Text + ";" + txtChiDinh.Text + ";" + txtChongChiDinh.Text + ";" + txtLDCD.Text + ";" + txtTuongTacThuoc.Text;
            sua.DuongD = txtDuongDung.Text;
            if (_data.SaveChanges() >= 0)
            {
                MessageBox.Show("Sửa thành công!", "Thông báo");
            }
            frm_TraCuuHDSDThuoc_Load(sender, e);
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            ktt(true);
            frm_TraCuuHDSDThuoc_Load(sender, e);
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (x != 0)
            {
                DialogResult _reuslt = MessageBox.Show("Bạn thực sự muốn xóa Thuốc " + txtTenThuoc.Text, "Xóa thuốc", MessageBoxButtons.YesNo, MessageBoxIcon.Question); if (_reuslt == DialogResult.Yes)
                {
                    var xoa = _data.DichVus.Single(p => p.MaDV == x);
                    _data.DichVus.Remove(xoa);
                    _data.SaveChanges();
                    frm_TraCuuHDSDThuoc_Load(sender, e);
                }
            }
            else
                MessageBox.Show("Chưa chọn thuốc cần xóa!", "Thông báo!");
        }

        private void grvDichVu_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            x = 0;
            if (grvDichVu.GetFocusedRowCellValue(MaDV) != null)
            {
                x = Convert.ToInt32(grvDichVu.GetFocusedRowCellValue(MaDV).ToString());
                var laydl = _data.DichVus.Where(p => p.MaDV == x).ToList();
                labMaThuoc.Text = grvDichVu.GetFocusedRowCellValue(MaDV).ToString();
                txtTenThuoc.Text = laydl.First().TenDV;
                cbbDonVi.Text = laydl.First().DonVi;
                txtHamLuong.Text = laydl.First().HamLuong;
                txtDuongDung.Text = laydl.First().DuongD;

                txtDuocLucHoc.Text = "";
                txtChiDinh.Text = "";
                txtChongChiDinh.Text = "";
                txtLDCD.Text = "";
                txtTuongTacThuoc.Text = "";
                if (!string.IsNullOrWhiteSpace(laydl.First().GhiChu))
                {
                    string[] arr = laydl.First().GhiChu.Split(';');
                    if (arr != null)
                    {
                        for (int i = 0; i < arr.Count(); i++)
                        {
                            if (i == 0)
                                txtDuocLucHoc.Text = arr[0];
                            if (i == 1)
                                txtChiDinh.Text = arr[1];
                            if (i == 2)
                                txtChongChiDinh.Text = arr[2];
                            if (i == 3)
                                txtLDCD.Text = arr[3];
                            if (i == 4)
                                txtTuongTacThuoc.Text = arr[4];
                        }
                    }
                }
            }

        }
    }
}