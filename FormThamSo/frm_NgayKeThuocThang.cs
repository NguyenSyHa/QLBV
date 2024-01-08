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
    public partial class frm_NgayKeThuocThang : DevExpress.XtraEditors.XtraForm
    {
        int _IDDonThuoc = 0;
        public frm_NgayKeThuocThang(int iddon)
        {
            InitializeComponent();
            _IDDonThuoc = iddon;
        }

        QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void frm_NgayKeThuocThang_Load(object sender, EventArgs e)
        {
            var _Ghichu = (from dt in _data.DThuocs.Where(p => p.IDDon == _IDDonThuoc)
                           select new { dt.NgayKe, dt.GhiChu }).Distinct().ToList();
            if (_Ghichu.First().GhiChu != null) //số tháng >1
            {
                var ghiChuSp = _Ghichu.First().GhiChu.Split(';');
                if (ghiChuSp.Count() >= 3)
                {
                    Lupngaytu.DateTime = Convert.ToDateTime(ghiChuSp[1]);
                    lupngayden.DateTime = Convert.ToDateTime(ghiChuSp[2]);
                }
            }
            else
            {
                Lupngaytu.DateTime = _Ghichu.First().NgayKe.Value;
                lupngayden.DateTime = _Ghichu.First().NgayKe.Value;
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string GhiChu = "";
            DThuoc _lDThuoc = _data.DThuocs.Where(p => p.IDDon == _IDDonThuoc).FirstOrDefault();
            bool Ktra = true;
            if (Lupngaytu.DateTime > lupngayden.DateTime)
            {
                Ktra = false;
                MessageBox.Show("Ngày từ không được lớn hơn ngày đến", "Thông báo", MessageBoxButtons.OK);
                Lupngaytu.Focus();
            }
            if (Ktra)
            {
                if (_lDThuoc != null)
                {
                    var ghiChuSp = _lDThuoc.GhiChu.Split(';');
                    if (ghiChuSp.Count() >= 3)
                    {
                        ghiChuSp[1] = Lupngaytu.DateTime.ToString("dd/MM/yyyy");
                        ghiChuSp[2] = lupngayden.DateTime.ToString("dd/MM/yyyy");
                        _lDThuoc.GhiChu = string.Join(";", ghiChuSp);
                        _data.SaveChanges();
                    }
                    QLBV.FormNhap.frm_Check_moi._InPhieuThuocDY_TT01(_IDDonThuoc);
                }
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Lupngaytu_EditValueChanged(object sender, EventArgs e)
        {
            //if (Lupngaytu.DateTime > lupngayden.DateTime)
            //{
            //    MessageBox.Show("Thông báo", "Ngày từ không được lớn hơn ngày đến", MessageBoxButtons.OK);
            //    //Lupngaytu.DateTime = lupngayden.DateTime;
            //}
        }

        private void lupngayden_EditValueChanged(object sender, EventArgs e)
        {
            if (lupngayden.DateTime < Lupngaytu.DateTime)
            {
                MessageBox.Show("Ngày đến không được nhỏ hơn ngày từ", "Thông báo", MessageBoxButtons.OK);
                lupngayden.Focus();
                //lupngayden.DateTime = Lupngaytu.DateTime;
            }
        }
    }
}