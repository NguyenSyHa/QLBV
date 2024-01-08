using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.IO;
using System.Security.Cryptography;

namespace QLBV
{
    public partial class frm_KichHoat : DevExpress.XtraEditors.XtraForm
    {
        public frm_KichHoat()
        {
            InitializeComponent();
        }
  

        #region Kiểm tra active
        private bool ktActive()
        {
            // lấy số seri
            string output1 =QLBV_Library.QLBV_Ham.HardDiskSeriesNumber();
            string output2 = QLBV_Library.QLBV_Ham.ConvertStringToSecureCode(output1);
            output2 = output2 + "@1";
            string licensecode = QLBV_Library.QLBV_Ham.ConvertStringToSecureCode(output2);
            MessageBox.Show(output2);
            //

            return true;
        }
        #endregion
        int _ktra = 0;
        string licensecode = "";
        string _serial;
        private void LuuSerial(string seria)
        {
            FileStream fs = new FileStream("C:\\VSS\\Serial.txt", FileMode.Create);
            string ngaydk = System.DateTime.Now.Date.ToString();
            StreamWriter writeFile = new StreamWriter(fs, Encoding.UTF8);//dùng streamwriter để ghi file
                writeFile.WriteLine(seria);
                writeFile.WriteLine(ngaydk);
                writeFile.WriteLine("Ten BV");
                writeFile.WriteLine("Ma BV");
                writeFile.Flush();
        }
        private void txtk1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtk1.Text.Length == 5) {
                txtk2.Focus();
            }
        }

        private void txtk2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtk2.Text.Length == 5)
            {
                txtk3.Focus();
            }
        }

        private void txtk3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtk3.Text.Length == 5)
            {
                txtk4.Focus();
            }
        }

        private void txtk4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtk4.Text.Length == 5)
            {
                txtk5.Focus();
            }
        }

        private void frm_KichHoat_Load(object sender, EventArgs e)
        {
            try
            {
                string output1 =QLBV_Library.QLBV_Ham.HardDiskSeriesNumber();
                //string output2 = ConvertStringToSecureCode(output1);
             
                txtSoMay.Text = output1;
                output1 = txtSoMay.Text.Trim();
                string output2 = output1 + "_@1";
                string output3 =QLBV_Library.QLBV_Ham.ConvertStringToSecureCode(output2);
                if (!string.IsNullOrEmpty(output3) && output3.Length > 25)
                    licensecode = output3.Substring(0, 25);
            }
            catch (Exception ex) {
                MessageBox.Show("lỗi kiểm tra mã kích hoạt"+ex.Message);
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                _serial = txtk1.Text.Trim() + txtk2.Text.Trim() + txtk3.Text.Trim() + txtk4.Text.Trim() + txtk5.Text.Trim();
                if (licensecode.ToUpper() == _serial)
                {
                    LuuSerial(_serial);
                    MessageBox.Show("Đăng ký thành công!\n Hãy đăng nhập lại");
                    //frmDangNhap frm = new frmDangNhap();
                    //this.Hide();
                    //frm.ShowDialog();
                    this.Dispose();
                }
                else
                    MessageBox.Show("Số PM chưa đúng");
            }catch(Exception){
                MessageBox.Show("lỗi lưu mã kích hoạt");
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}