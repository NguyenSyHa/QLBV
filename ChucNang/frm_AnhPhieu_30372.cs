using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.IO;

namespace QLBV.ChucNang
{
    public partial class frm_AnhPhieu_30372 : DevExpress.XtraEditors.XtraForm
    {
        public frm_AnhPhieu_30372()
        {
            InitializeComponent();

        }


        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        string[] arrDuongDan = new string[8];

        public int IDCD;
        public int idcls;
        public int _mabn;
        private void ChonAnh(PictureEdit pt, int i)
        {

            bool tontai = true;
            switch (i)
            {
                case 1:
                    if (ptAnh1.Image == null)
                        tontai = false;
                    break;
                case 2:
                    if (ptAnh2.Image == null)
                        tontai = false;
                    break;
                case 3:
                    if (ptAnh3.Image == null)
                        tontai = false;
                    break;
                case 4:
                    if (ptAnh4.Image == null)
                        tontai = false;
                    break;
                case 5:
                    if (ptAnh5.Image == null)
                        tontai = false;
                    break;
                case 6:
                    if (ptAnh6.Image == null)
                        tontai = false;
                    break;
                case 7:
                    if (ptAnh7.Image == null)
                        tontai = false;
                    break;
                case 8:
                    if (ptAnh8.Image == null)
                        tontai = false;
                    break;
                default:
                    break;
            }
            if (tontai)
            {
                DialogResult _dresult = MessageBox.Show("Bạn có muốn chọn lại ảnh!", "Hỏi lưu", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (_dresult == DialogResult.Yes)
                {
                  
                        string fileName = string.Empty;
                        OpenFileDialog op = new OpenFileDialog();
                        op.Multiselect = false;
                        op.Filter = "JPEG (*.jpg)|*.jpg|BMP(*.bmp)| *.bmp";
                        int IdCLS = idcls;
                        if (trangthaiLuu() == 0)//Nếu là thêm mới ảnh.
                        {
                            if (op.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                            {
                                fileName = op.FileName;
                                string ex = Path.GetExtension(op.FileName);
                                if (!string.IsNullOrEmpty(fileName))
                                    pt.Image = Image.FromFile(fileName);
                                else
                                    pt.Image = null;
                                string _tenfileanh = DungChung.Bien.DuongDan;
                                // _tenfileanh += _mabn + "_" + IdCLS + "_" + i + ".jpg";
                                _tenfileanh += _mabn + "_" + IdCLS + "_" + i + ex;
                                arrDuongDan[i - 1] = layTenFileAnh(fileName, _tenfileanh);
                            }
                        }
                        if (trangthaiLuu() == 1) // Nếu là sửa ảnh
                        {
                            if (op.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                            {
                                fileName = op.FileName;
                                string ex = Path.GetExtension(op.FileName);
                                if (!string.IsNullOrEmpty(fileName))
                                    pt.Image = Image.FromFile(fileName);
                                else
                                    pt.Image = null;

                                string _tenfileanh = DungChung.Bien.DuongDan;
                                // _tenfileanh += _mabn + "_" + IdCLS + "_" + i + ".jpg";
                                _tenfileanh += _mabn + "_" + IdCLS + "_" + i + ex;
                                arrDuongDan[i - 1] = layTenFileAnh(fileName, _tenfileanh);
                            }
                        }
                    
                }
            }
            else
            {


                string fileName = string.Empty;
                OpenFileDialog op = new OpenFileDialog();
                op.Multiselect = false;
                op.Filter = "JPEG (*.jpg)|*.jpg|BMP(*.bmp)| *.bmp";
                int IdCLS = idcls;

                if (trangthaiLuu() == 0)//Nếu là thêm mới ảnh.
                {
                    if (op.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        fileName = op.FileName;
                        string ex = Path.GetExtension(op.FileName);
                        if (!string.IsNullOrEmpty(fileName))
                            pt.Image = Image.FromFile(fileName);
                        else
                            pt.Image = null;
                        string _tenfileanh = DungChung.Bien.DuongDan;
                        //  _tenfileanh += _mabn + "_" + IdCLS + "_" + i + ".jpg";
                        _tenfileanh += _mabn + "_" + IdCLS + "_" + i + ex;
                        arrDuongDan[i - 1] = layTenFileAnh(fileName, _tenfileanh);
                    }
                }


                if (trangthaiLuu() == 1) // Nếu là sửa ảnh
                {
                    try
                    {
                       
                        if (op.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            fileName = op.FileName;
                            string ex = Path.GetExtension(op.FileName);
                            if (!string.IsNullOrEmpty(fileName))
                                pt.Image = Image.FromFile(fileName);
                            else
                                pt.Image = null;

                            string _tenfileanh = DungChung.Bien.DuongDan;
                            // _tenfileanh += _mabn + "_" + IdCLS + "_" + i + ".jpg";
                            _tenfileanh += _mabn + "_" + IdCLS + "_" + i + ex;
                            arrDuongDan[i - 1] = layTenFileAnh(fileName, _tenfileanh);
                        }
                    }
                    catch 
                    {

                        if (op.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            fileName = op.FileName;
                            string ex = Path.GetExtension(op.FileName);
                            if (!string.IsNullOrEmpty(fileName))
                                pt.Image = Image.FromFile(fileName);
                            else
                                pt.Image = null;

                            string _tenfileanh = DungChung.Bien.DuongDan;
                            // _tenfileanh += _mabn + "_" + IdCLS + "_" + i + ".jpg";
                            _tenfileanh += _mabn + "_" + IdCLS + "_" + i + ex;
                            arrDuongDan[i - 1] = layTenFileAnh(fileName, _tenfileanh);
                        }
                    }
                  
                }
            }
        }

        public string layTenFileAnh(string fileAnhTMH, string tenfileanh)
        {
            if (!string.IsNullOrEmpty(fileAnhTMH))
            {
                if (!File.Exists(tenfileanh))
                {
                    File.Copy(fileAnhTMH, tenfileanh);

                }
                else
                {
                    for (int i = 0; i < 100; i++)
                    {
                        string a = "";
                        string ex = Path.GetExtension(tenfileanh);
                        a = tenfileanh.Replace(ex, i + ex);
                        //if(DungChung.Bien.MaBV== "30012")
                        //    a = tenfileanh.Replace(".bmp", i + ".bmp");
                        //else
                        //    a = tenfileanh.Replace(".jpg", i + ".jpg");
                        if (!File.Exists(a))
                        {
                            File.Copy(fileAnhTMH, a);
                            tenfileanh = a;
                            break;
                        }
                    }
                }
            }
            return tenfileanh;
        }
        private void xoaAnh(PictureEdit pt, int i)
        {
            i = i - 1;
            if (trangthaiLuu() == 0)
            {
                pt.Image = null;
            }
            if (trangthaiLuu() == 1)
            {
                arrDuongDan[i] = "";
                pt.Image = null;
            }

        }


        #region sự kiện thêm ảnh
        private void btnChonAnh1_Click(object sender, EventArgs e)
        {
            ChonAnh(ptAnh1, 1);
        }

        private void btnChonAnh2_Click(object sender, EventArgs e)
        {
            ChonAnh(ptAnh2, 2);
        }

        private void btnChonAnh3_Click(object sender, EventArgs e)
        {
            ChonAnh(ptAnh3, 3);
        }

        private void btnChonAnh4_Click(object sender, EventArgs e)
        {
            ChonAnh(ptAnh4, 4);
        }

        private void btnChonAnh5_Click(object sender, EventArgs e)
        {
            ChonAnh(ptAnh5, 5);
        }

        private void btnChonAnh6_Click(object sender, EventArgs e)
        {
            ChonAnh(ptAnh6, 6);
        }

        private void btnChonAnh7_Click(object sender, EventArgs e)
        {
            ChonAnh(ptAnh7, 7);
        }

        private void btnChonAnh8_Click(object sender, EventArgs e)
        {
            ChonAnh(ptAnh8, 8);
        }
        #endregion

        #region Sự kiện xóa ảnh
        private void btnXoaAnh1_Click(object sender, EventArgs e)
        {
            xoaAnh(ptAnh1, 1);
        }

        private void btnXoaAnh2_Click(object sender, EventArgs e)
        {
            xoaAnh(ptAnh2, 2);
        }

        private void btnXoaAnh3_Click(object sender, EventArgs e)
        {
            xoaAnh(ptAnh3, 3);
        }

        private void btnXoaAnh4_Click(object sender, EventArgs e)
        {
            xoaAnh(ptAnh4, 4);
        }

        private void btnXoaAnh5_Click(object sender, EventArgs e)
        {
            xoaAnh(ptAnh5, 5);
        }

        private void btnXoaAnh6_Click(object sender, EventArgs e)
        {
            xoaAnh(ptAnh6, 6);
        }

        private void btnXoaAnh7_Click(object sender, EventArgs e)
        {
            xoaAnh(ptAnh7, 7);
        }

        private void btnXoaAnh8_Click(object sender, EventArgs e)
        {
            xoaAnh(ptAnh8, 8);
        }


        #endregion
        public void binding(int idcls, int mabn, int IDCD)
        {
            this.idcls = idcls;
            this._mabn = mabn;
            this.IDCD = IDCD;

        }
        void CheckAnh(bool T)
        {
            btnChonAnh1.Enabled = T;
            btnChonAnh2.Enabled = T;
            btnChonAnh3.Enabled = T;
            btnChonAnh4.Enabled = T;
            btnChonAnh5.Enabled = T;
            btnChonAnh6.Enabled = T;
            btnChonAnh7.Enabled = T;
            btnChonAnh8.Enabled = T;
            btnXoaAnh1.Enabled = T;
            btnXoaAnh2.Enabled = T;
            btnXoaAnh3.Enabled = T;
            btnXoaAnh4.Enabled = T;
            btnXoaAnh5.Enabled = T;
            btnXoaAnh6.Enabled = T;
            btnXoaAnh7.Enabled = T;
            btnXoaAnh8.Enabled = T;


        }
        private void btnLuuAnh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            CLSct Anh = data.CLScts.Where(p => p.IDCD == IDCD).Single();        
            Anh.DuongDan2 = QLBV_Library.QLBV_Ham.LuuChuoi('|', arrDuongDan);
            int a = data.SaveChanges();
            if (a > 0)
            {             
                CheckAnh(false);
                btnSua.Enabled = true;
                btnLuuAnh.Enabled = false;
                XtraMessageBox.Show("Đã lưu thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }
        private int trangthaiLuu()
        {
            int ketqua = 0;
            var a = data.CLScts.Where(p => p.IDCD == IDCD).First().DuongDan2;
            if (a != "" || a != null)
            {
                ketqua = 1;


            }
            return ketqua;
        }

        private void frm_AnhPhieu_30372_Load(object sender, EventArgs e)
        {
            if (trangthaiLuu() == 0)
            {
                CheckAnh(true);
            }
            else
            {
                CheckAnh(false);
            }
           
           
            if (data.CLScts.Where(p => p.IDCD == IDCD).First().DuongDan2 != "" || data.CLScts.Where(p => p.IDCD == IDCD).First().DuongDan2 != null)
            {
                btnLuuAnh.Enabled = false ;
                if (data.CLScts.Where(p => p.IDCD == IDCD).First().DuongDan2 != null)
                {
                    arrDuongDan = data.CLScts.Where(p => p.IDCD == IDCD).First().DuongDan2.Split('|');
                    for (int i = 0; i < arrDuongDan.Length; i++)
                    {
                        switch (i)
                        {
                            case 0:
                                if (string.IsNullOrEmpty(arrDuongDan[i]) && arrDuongDan[i].Length <= 3)
                                {
                                    ptAnh1.Image = null;
                                }
                                else
                                {
                                    if (File.Exists(arrDuongDan[i]))
                                      
                                        ptAnh1.Image = Image.FromFile(arrDuongDan[i]);
                                    else
                                        ptAnh1.Image = null;
                                 
                                }
                                break;

                            case 1:
                                if (string.IsNullOrEmpty(arrDuongDan[i]) && arrDuongDan[i].Length <= 3)
                                {
                                    ptAnh2.Image = null;
                                }
                                else
                                {
                                    if (File.Exists(arrDuongDan[i]))
                                        ptAnh2.Image = Image.FromFile(arrDuongDan[i]);
                                    else
                                        ptAnh2.Image = null;           
                                }
                                break;
                            case 2:
                                if (string.IsNullOrEmpty(arrDuongDan[i]) && arrDuongDan[i].Length <= 3)
                                {
                                    ptAnh3.Image = null;
                                }
                                else
                                {
                                    if (File.Exists(arrDuongDan[i]))
                                        ptAnh3.Image = Image.FromFile(arrDuongDan[i]);
                                    else
                                        ptAnh3.Image = null;
                                }
                                break;
                            case 3:
                                if (string.IsNullOrEmpty(arrDuongDan[i]) && arrDuongDan[i].Length <= 3)
                                {
                                    ptAnh4.Image = null;
                                }
                                else
                                {
                                    if (File.Exists(arrDuongDan[i]))
                                        ptAnh4.Image = Image.FromFile(arrDuongDan[i]);
                                    else
                                        ptAnh4.Image = null;
                                }
                                break;
                            case 4:
                                if (string.IsNullOrEmpty(arrDuongDan[i]) && arrDuongDan[i].Length <= 3)
                                {
                                    ptAnh5.Image = null;
                                }
                                else
                                {
                                    if (File.Exists(arrDuongDan[i]))
                                        ptAnh5.Image = Image.FromFile(arrDuongDan[i]);
                                    else
                                        ptAnh5.Image = null;
                                }
                                break;
                            case 5:
                                if (string.IsNullOrEmpty(arrDuongDan[i]) && arrDuongDan[i].Length <= 3)
                                {
                                    ptAnh6.Image = null;
                                }
                                else
                                {
                                    if (File.Exists(arrDuongDan[i]))
                                        ptAnh6.Image = Image.FromFile(arrDuongDan[i]);
                                    else
                                        ptAnh6.Image = null;
                                }
                                break;
                            case 6:
                                if (string.IsNullOrEmpty(arrDuongDan[i]) && arrDuongDan[i].Length <= 3)
                                {
                                    ptAnh7.Image = null;
                                }
                                else
                                {
                                    if (File.Exists(arrDuongDan[i]))
                                        ptAnh7.Image = Image.FromFile(arrDuongDan[i]);
                                    else
                                        ptAnh7.Image = null;
                                }
                                break;
                            case 7:
                                if (string.IsNullOrEmpty(arrDuongDan[i]) && arrDuongDan[i].Length <= 3)
                                {
                                    ptAnh8.Image = null;
                                }
                                else
                                {
                                    if (File.Exists(arrDuongDan[i]))
                                        ptAnh8.Image = Image.FromFile(arrDuongDan[i]);
                                    else
                                        ptAnh8.Image = null;
                                }
                                break;

                        }                     
                    }
                }
           


            }
        }
        private void gpAnh(PictureEdit pt)
        {
            if (pt != null)
            {
                pt.Dispose();
            }
        }
        private void btnSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void btnSua1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            btnLuuAnh.Enabled = true;
            btnSua1.Enabled = false;
            CheckAnh(true);
        }

        private void frm_AnhPhieu_30372_FormClosed(object sender, FormClosedEventArgs e)
        {
            gpAnh(ptAnh1);
            gpAnh(ptAnh2);
            gpAnh(ptAnh3);
            gpAnh(ptAnh4);
            gpAnh(ptAnh5);
            gpAnh(ptAnh6);
            gpAnh(ptAnh7);
            gpAnh(ptAnh8);

        }
    }
}