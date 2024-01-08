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
    public partial class frm_ThongBaoTienTamUng : DevExpress.XtraEditors.XtraForm
    {
        public frm_ThongBaoTienTamUng(int mabn)
        {
            InitializeComponent();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            int maBN = Convert.ToInt32(txtMaBN.Text);
            KiemTraTienTamUng(maBN);
        }

        /// <summary>
        /// kiểm tra số tiền tạm ứng của bệnh nhân, nếu số tiền tạm ứng nhỏ hơn số tiền bệnh nhân phải trả thì thông báo
        /// </summary>
        /// <param name="mabn">mã bệnh nhân</param>
        private void KiemTraTienTamUng(int mabn)
        {
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            double tt = (from dt in data.DThuocs
                         join dtct in data.DThuoccts.Where(p => p.ThanhToan == 0) on dt.IDDon equals dtct.IDDon
                         select new { dtct.ThanhTien, dt.MaBNhan }).Where(p => p.MaBNhan == mabn).ToList().Sum(p => p.ThanhTien);
            double tu = Convert.ToInt32(data.TamUngs.Where(p => p.MaBNhan == mabn).Sum(p => p.SoTien));
            if (tu < tt)
            {
                double kq = tt - tu;
                if (kq > 500000)
                {
                    double tien = Math.Abs(500000 - kq);
                    MessageBox.Show("Số tiền tạm ứng nhỏ hơn số tiền bệnh nhân phải trả\nBệnh nhân phải trả thêm: " + tien, "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void frm_ThongBaoTienTamUng_Load(object sender, EventArgs e)
        {

        }
    }
}