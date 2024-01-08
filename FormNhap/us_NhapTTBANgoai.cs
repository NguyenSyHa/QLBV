using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace QLBV.FormNhap
{
    public partial class us_NhapTTBANgoai : DevExpress.XtraEditors.XtraUserControl
    {
        public us_NhapTTBANgoai(int mabn,string ChuyenKhoa)
        {
            InitializeComponent();
            _mabn = mabn;
            _ChuyenKhoa = ChuyenKhoa;
        }
        int _mabn = 0;
        string _ChuyenKhoa = "";
        public delegate void Close();
        public Close close;
        private void panelControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void labelControl1_Click(object sender, EventArgs e)
        {

        }
        QLBV_Database.QLBVEntities _data;
        int TTLuu = 0;
        private void us_NhapTTBANgoai_Load(object sender, EventArgs e)
        {
            _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var _ttbn = _data.BenhNhans.Where(p => p.MaBNhan == _mabn).FirstOrDefault();
            //var _ttbx = _data.TTboXungs.Where(p => p.MaBNhan == _mabn).FirstOrDefault();
            var _hsba = _data.HSBenhAns.Where(p => p.MaBNhan == _mabn && p.ChuyenKhoa == _ChuyenKhoa).FirstOrDefault();
            var _vv = _data.VaoViens.Where(p => p.MaBNhan == _mabn).FirstOrDefault();
            if (_vv != null)
            {
                txtLyDoVV.Text = _vv.LyDo;
                txtMach.Text = _vv.Mach;
                txtNhietDo.Text = _vv.NhietDo;
                txtHuyetAp.Text = _vv.HuyetAp;
                txtNhipTho.Text = _vv.NhipTho;
                txtCanNang.Text = _vv.CanNang;
                txtChieuCao.Text = _vv.ChieuCao;
                txtNhomMau.Text = _vv.NhomMau;
                nhommaurh.Text = _vv.HeMau;
            }
            if (_hsba != null)
            {
                TTLuu = 1;
                txtQTBenhLy.Text = _hsba.QTBenhLy;
                txtTienSuBN.Text = _hsba.TienSuBN;
                txtTienSuGD.Text = _hsba.TienSuGD;
                txtBenhTT.Text = _hsba.ToanThan;
                txtBenhNgoaiK.Text = _hsba.BenhNgoai;
                if (!string.IsNullOrEmpty(_hsba.DDLienQuan) && _hsba.DDLienQuan.Contains("|") && _hsba.DDLienQuan.Contains(";"))
                {
                    try
                    {
                        string[] arr = _hsba.DDLienQuan.Split('|');
                        if (!string.IsNullOrEmpty(arr[0]) && arr[0].Contains(";"))
                        {
                            string[] arr1 = arr[0].Split(';');
                            ckc01.Checked = Convert.ToBoolean(arr1[0]);
                            txtdd01.Text = arr1[1];
                        }
                        if (!string.IsNullOrEmpty(arr[1]) && arr[1].Contains(";"))
                        {
                            string[] arr1 = arr[1].Split(';');
                            ckc02.Checked = Convert.ToBoolean(arr1[0]);
                            txtdd02.Text = arr1[1];
                        }
                        if (!string.IsNullOrEmpty(arr[2]) && arr[2].Contains(";"))
                        {
                            string[] arr1 = arr[2].Split(';');
                            ckc03.Checked = Convert.ToBoolean(arr1[0]);
                            txtdd03.Text = arr1[1];
                        }
                        if (!string.IsNullOrEmpty(arr[3]) && arr[3].Contains(";"))
                        {
                            string[] arr1 = arr[3].Split(';');
                            ckc04.Checked = Convert.ToBoolean(arr1[0]);
                            txtdd04.Text = arr1[1];
                        }
                        if (!string.IsNullOrEmpty(arr[4]) && arr[4].Contains(";"))
                        {
                            string[] arr1 = arr[4].Split(';');
                            ckc05.Checked = Convert.ToBoolean(arr1[0]);
                            txtdd05.Text = arr1[1];
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi load đặc điểm liên quan: " + ex.ToString());
                    }
                }
                if (!string.IsNullOrEmpty(_hsba.CoQuan) && _hsba.CoQuan.Contains(";"))
                {
                    try
                    {
                        string[] arr = _hsba.CoQuan.Split(';');
                        if (!string.IsNullOrEmpty(arr[0]))
                            cb01.SelectedIndex = Convert.ToInt32(arr[0]) - 1;

                        if (!string.IsNullOrEmpty(arr[1]))
                            cb02.SelectedIndex = Convert.ToInt32(arr[1]) - 1;

                        if (!string.IsNullOrEmpty(arr[2]))
                            cb03.SelectedIndex = Convert.ToInt32(arr[2]) - 1;

                        if (!string.IsNullOrEmpty(arr[3]))
                            cb04.SelectedIndex = Convert.ToInt32(arr[3]) - 1;

                        if (!string.IsNullOrEmpty(arr[4]))
                            cb05.SelectedIndex = Convert.ToInt32(arr[4]) - 1;

                        if (!string.IsNullOrEmpty(arr[5]))
                            cb06.SelectedIndex = Convert.ToInt32(arr[5]) - 1;

                        if (!string.IsNullOrEmpty(arr[6]))
                            cb07.SelectedIndex = Convert.ToInt32(arr[6]) - 1;

                        if (!string.IsNullOrEmpty(arr[7]))
                            cb08.SelectedIndex = Convert.ToInt32(arr[7]) - 1;

                        if (!string.IsNullOrEmpty(arr[8]))
                            cb09.SelectedIndex = Convert.ToInt32(arr[8]) - 1;

                        if (!string.IsNullOrEmpty(arr[9]))
                            cb10.SelectedIndex = Convert.ToInt32(arr[9]) - 1;
                        if (!string.IsNullOrEmpty(arr[10]))
                            cb11.SelectedIndex = Convert.ToInt32(arr[10]) - 1;

                        if (!string.IsNullOrEmpty(arr[11]))
                            cb12.Text = arr[11];
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi cơ quan: " + ex.ToString());
                    }
                }
            }

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            close();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var suavv = _data.VaoViens.Where(p => p.MaBNhan == _mabn).FirstOrDefault();
            if (suavv != null)
            {
                suavv.LyDo = txtLyDoVV.Text;
                suavv.Mach = txtMach.Text;
                suavv.NhietDo = txtNhietDo.Text;
                suavv.HuyetAp = txtHuyetAp.Text;
                suavv.NhipTho = txtNhipTho.Text;
                suavv.CanNang = txtCanNang.Text;
                suavv.ChieuCao = txtChieuCao.Text;
                suavv.NhomMau = txtNhomMau.Text;
                suavv.HeMau = nhommaurh.Text;
            }
            if (TTLuu == 1)
            {
                var suahsba = _data.HSBenhAns.Where(p => p.MaBNhan == _mabn && p.ChuyenKhoa == _ChuyenKhoa).FirstOrDefault();
                suahsba.QTBenhLy = txtQTBenhLy.Text;
                suahsba.TienSuBN = txtTienSuBN.Text;
                suahsba.TienSuGD = txtTienSuGD.Text;
                suahsba.ToanThan = txtBenhTT.Text;
                suahsba.BenhNgoai = txtBenhNgoaiK.Text;
                suahsba.DDLienQuan = Convert.ToInt32(ckc01.Checked) + ";" + txtdd01.Text + "|" + Convert.ToInt32(ckc02.Checked) + ";" + txtdd02.Text + "|" + Convert.ToInt32(ckc03.Checked) + ";" + txtdd03.Text + "|" + Convert.ToInt32(ckc04.Checked) + ";" + txtdd04.Text + "|" + Convert.ToInt32(ckc05.Checked) + ";" + txtdd05.Text;
                suahsba.CoQuan = cb01.SelectedIndex + 1 + ";" + cb02.SelectedIndex + 1 + ";" + cb02.SelectedIndex + 1 + ";" + cb04.SelectedIndex + 1 + ";" + cb05.SelectedIndex + 1 + ";" + cb06.SelectedIndex + 1 + ";" + cb07.SelectedIndex + 1 + ";" + cb08.SelectedIndex + 1 + ";" + cb09.SelectedIndex + 1 + ";" + cb10.SelectedIndex + 1 + ";" + cb11.SelectedIndex + 1 + ";" + cb12.SelectedIndex + 1;
                _data.SaveChanges();
            }
            else
            {
                HSBenhAn moi = new HSBenhAn();
                moi.MaBNhan = _mabn;
                moi.ChuyenKhoa = _ChuyenKhoa;
                moi.QTBenhLy = txtQTBenhLy.Text;
                moi.TienSuBN = txtTienSuBN.Text;
                moi.TienSuGD = txtTienSuGD.Text;
                moi.ToanThan = txtBenhTT.Text;
                moi.BenhNgoai = txtBenhNgoaiK.Text;
                moi.DDLienQuan = Convert.ToInt32(ckc01.Checked) + ";" + txtdd01.Text + "|" + Convert.ToInt32(ckc02.Checked) + ";" + txtdd02.Text + "|" + Convert.ToInt32(ckc03.Checked) + ";" + txtdd03.Text + "|" + Convert.ToInt32(ckc04.Checked) + ";" + txtdd04.Text + "|" + Convert.ToInt32(ckc05.Checked) + ";" + txtdd05.Text;
                moi.CoQuan = cb01.SelectedIndex + 1 + ";" + cb02.SelectedIndex + 1 + ";" + cb02.SelectedIndex + 1 + ";" + cb04.SelectedIndex + 1 + ";" + cb05.SelectedIndex + 1 + ";" + cb06.SelectedIndex + 1 + ";" + cb07.SelectedIndex + 1 + ";" + cb08.SelectedIndex + 1 + ";" + cb09.SelectedIndex + 1 + ";" + cb10.SelectedIndex + 1 + ";" + cb11.SelectedIndex + 1 + ";" + cb12.SelectedIndex + 1;
                _data.HSBenhAns.Add(moi);
                _data.SaveChanges();
            }
        }

        private void ckc01_CheckedChanged(object sender, EventArgs e)
        {
            if (ckc01.Checked)
                txtdd01.Enabled = true;
            else
                txtdd01.Enabled = false;
        }

        private void ckc02_CheckedChanged(object sender, EventArgs e)
        {
            if (ckc02.Checked)
                txtdd02.Enabled = true;
            else
                txtdd02.Enabled = false;
        }

        private void ckc03_CheckedChanged(object sender, EventArgs e)
        {
            if (ckc03.Checked)
                txtdd03.Enabled = true;
            else
                txtdd03.Enabled = false;
        }

        private void ckc04_CheckedChanged(object sender, EventArgs e)
        {
            if (ckc04.Checked)
                txtdd04.Enabled = true;
            else
                txtdd04.Enabled = false;
        }

        private void ckc05_CheckedChanged(object sender, EventArgs e)
        {
            if (ckc05.Checked)
                txtdd05.Enabled = true;
            else
                txtdd05.Enabled = false;
        }
    }
}
