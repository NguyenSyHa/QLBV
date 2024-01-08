using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Data.SqlClient;

namespace QLBV.FormThamSo
{
    public partial class Frm_KetNoi_CLS : DevExpress.XtraEditors.XtraForm
    {
        public Frm_KetNoi_CLS()
        {
            InitializeComponent();
        }

        private void btnKetNoi_Click(object sender, EventArgs e)
        {
            String chuoiketnoi = "";
            String server = txtMayChu.Text;
            String database = txtTenCSDL.Text;
            String tkhoan = txtTaiKhoan.Text;
            String mkhau = txtMatKhau.Text;
            String table = txtTenBang.Text;
            string _par1=txtMaDVct.Text.Trim();
            string _par2 = txtKQ.Text.Trim();
            string BarCode = txtBarCode.Text;
            DateTime _ngaytu;
            try
            {
                SqlConnection Conn = new SqlConnection(); // khởi tạo một đối tượng kết nối
                Conn.ConnectionString = @"Data Source=" + server + ";Initial Catalog=" + database + ";User ID=" + tkhoan + ";password=" + mkhau;
                Conn.Open();
                string sql = "select " +_par1+" , "+_par2+ " from " + table;
                SqlDataAdapter da = new SqlDataAdapter(sql, Conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                chuoiketnoi = server + "|" + database + "|" + tkhoan + "|" + mkhau + "|" + table + "|" + _par1 + "|" + _par2 + "|" + BarCode;
                MessageBox.Show("Kết nối thành công!");
                DungChung.Bien.thongtinketnoi = chuoiketnoi;
                QLBV_Library.QLBV_Ham.Write_update("Cuong.ConnCLS", chuoiketnoi);
                Conn.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Kết nối không thành công!");
            }
            
        }

        private void groupControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Frm_KetNoi_CLS_Load(object sender, EventArgs e)
        {
              String[] chuoiKN = new String[7] { "", "", "", "", "","","" };
                chuoiKN = DungChung.Bien.thongtinketnoi.Split('|');

                String server = "", database = "", tkhoan = "", mkhau = "", table = "", _par1 = "", _par2 = "", BarCode = "";
                if (chuoiKN.Length > 0 && chuoiKN[0] != null)
                    server = chuoiKN[0];
                if (chuoiKN.Length > 1 && chuoiKN[1] != null)
                    database = chuoiKN[1];
                if (chuoiKN.Length > 2 && chuoiKN[2] != null)
                    tkhoan = chuoiKN[2];
                if (chuoiKN.Length > 3 && chuoiKN[3] != null)
                    mkhau = chuoiKN[3];
                if (chuoiKN.Length > 4 && chuoiKN[4] != null)
                    table = chuoiKN[4];
                if (chuoiKN.Length > 5 && chuoiKN[5] != null)
                    _par1 = chuoiKN[5];
                if (chuoiKN.Length > 6 && chuoiKN[6] != null)
                    _par2 = chuoiKN[6];
                if (chuoiKN.Length > 7 && chuoiKN[7] != null)
                    BarCode = chuoiKN[7];

           txtMayChu.Text=server;
          txtTenCSDL.Text=database;
           txtTaiKhoan.Text=tkhoan;
         txtMatKhau.Text=mkhau;
          txtTenBang.Text=table;
          txtMaDVct.Text=_par1;
           txtKQ.Text=_par2;
           txtBarCode.Text = BarCode;
        }

    }
}