using System;
using QLBV_Database;
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
    public partial class frm_checkThuocThang : DevExpress.XtraEditors.XtraForm
    {
        ThuocThang thuocThang;
        DateTime _ngaybdke = new DateTime();
        DateTime _ngaycuoike = new DateTime();
        public frm_checkThuocThang(ThuocThang _data)
        {
            this.thuocThang = _data;
            InitializeComponent();
        }
        public frm_checkThuocThang(ThuocThang _data, DateTime ngaybdke, DateTime ngaycuoike)
        {
            this.thuocThang = _data;
            InitializeComponent();
        }
        public delegate void getString(ThuocThang data);
        public getString getdata;

        private void btnInBC_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cbSoThang.Text.Trim()))
            {
                MessageBox.Show("Chưa nhập số thang!");
                return;
            }
            else if (lupNgaytu.DateTime.ToString().Contains("01/01/0001") || lupngayden.DateTime.ToString().Contains("01/01/0001"))
            {
                MessageBox.Show("Chưa nhập đủ ngày kê!");
                return;
            }
            else if (lupNgaytu.DateTime > lupngayden.DateTime)
            {
                MessageBox.Show("Nhập sai ngày kê!");
                return;
            }
            else
            {
                ThuocThang thuocThang = new ThuocThang();
                if (!string.IsNullOrEmpty(memoGhichu.Text))
                    thuocThang.GhiChu = memoGhichu.Text;
                if (!string.IsNullOrEmpty(mmCahSac.Text))
                    thuocThang.CachSac = mmCahSac.Text;
                if (!string.IsNullOrEmpty(mmCachUong.Text))
                    thuocThang.CachUong = mmCachUong.Text;
                double st = Convert.ToDouble(cbSoThang.Text);
                thuocThang.SoThang = st;
                thuocThang.TuNgay = lupNgaytu.DateTime;
                thuocThang.DenNgay = lupngayden.DateTime;
                if (getdata != null)
                    getdata(thuocThang);
                this.Dispose();
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            //getdata(1, "", "");
            this.Close();
        }

        private void frm_checkThuocThang_Load(object sender, EventArgs e)
        {
            if (thuocThang.SoThang > 0)
                cbSoThang.Text = thuocThang.SoThang.ToString();
            if (thuocThang.DenNgay > DateTime.MinValue)
                lupngayden.DateTime = thuocThang.DenNgay;
            if (thuocThang.TuNgay > DateTime.MinValue)
                lupNgaytu.DateTime = thuocThang.TuNgay;
            memoGhichu.Text = thuocThang.GhiChu;
            if (DungChung.Bien.MaBV == "24272")
            {
                if (!string.IsNullOrEmpty(thuocThang.CachSac))
                {
                    mmCahSac.Text = thuocThang.CachSac;
                }
                else
                    mmCahSac.Text = "Sắc 01 ngày 01 thang";
                if (!string.IsNullOrEmpty(thuocThang.CachUong))
                {
                    mmCachUong.Text = thuocThang.CachUong;
                }
                else
                {
                    mmCachUong.Text = "Uống ngày 3 lần (S-T-C) sau ăn";
                }
            }
            else
            {
                mmCahSac.Text = thuocThang.CachSac;
                mmCachUong.Text = thuocThang.CachUong;
            }
            if (DungChung.Bien.MaBV == "27001")
            {

                memoGhichu.Text = "Kiêng thịt chó, cá mè, tôm cua, đỗ xanh, măng, cà muối, rau rút, mật ong.";
                mmCahSac.Text = "1 thang sắc 2 ngày, ngày sắc 2 lần.";
                mmCachUong.Text = "Uống sau ăn 60'.";
            }


        }

        private void cbSoThang_SelectedIndexChanged(object sender, EventArgs e)
        {
            double sothang = 1;
            if (double.TryParse(cbSoThang.Text, out sothang))
                lupngayden.DateTime = lupNgaytu.DateTime.AddDays(sothang - 1);
        }

        private void lupNgaytu_EditValueChanged(object sender, EventArgs e)
        {
            double sothang = 1;
            if (double.TryParse(cbSoThang.Text, out sothang))
                lupngayden.DateTime = lupNgaytu.DateTime.AddDays(sothang - 1);
        }

        private void cbSoThang_Properties_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void cbSoThang_KeyPress(object sender, KeyPressEventArgs e)
        {
        }
    }

    public class ThuocThang
    {
        public double SoThang { get; set; }
        public DateTime TuNgay { get; set; }
        public DateTime DenNgay { get; set; }
        public string GhiChu { get; set; }
        public string CachSac { get; set; }
        public string CachUong { get; set; }
    }
}