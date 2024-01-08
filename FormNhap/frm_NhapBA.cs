using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace QLBV.FormNhap
{
    public partial class frm_NhapBA : DevExpress.XtraEditors.XtraForm
    {
        public frm_NhapBA(int mabn,string ChuyenKhoa)
        {
            InitializeComponent();
            _mabn = mabn;
            _ChuyenKhoa = ChuyenKhoa;
        }
        int _mabn = 0;
        string _ChuyenKhoa = "";
        void Thoat()
        {
            this.Close();
        }
        
        private void frm_NhapBA_Load(object sender, EventArgs e)
        {
            if (_ChuyenKhoa == "Ngoại")
            {
                QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                var ttbn = _data.BenhNhans.Where(p => p.MaBNhan == _mabn).FirstOrDefault();
                if (ttbn != null)
                {
                    groupControl1.Text = "Thông tin bệnh án: " + _ChuyenKhoa + " - " + ttbn.TenBNhan.ToUpper();
                    pdetail.Controls.Clear();
                    QLBV.FormNhap.us_NhapTTBANgoai frm = new us_NhapTTBANgoai(_mabn, _ChuyenKhoa);
                    frm.close = new us_NhapTTBANgoai.Close(Thoat);
                    frm.Dock = System.Windows.Forms.DockStyle.Fill;
                    pdetail.Controls.Add(frm);
                }
            }
            else
            {
                MessageBox.Show("Hiện tại mới chỉ có thể nhập HS bệnh án Ngoại khoa\ncác bệnh án khác vui lòng chờ cập nhật");
                this.Close();
            }
        }
    }
}