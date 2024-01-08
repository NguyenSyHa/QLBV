using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
namespace QLBV.ChucNang
{
    public partial class frm_Update_MaKP_VP : DevExpress.XtraEditors.XtraForm
    {
        public frm_Update_MaKP_VP()
        {
            InitializeComponent();
        }
        int _mabn = 0, _makpkd = 0, _madv = 0;
        public frm_Update_MaKP_VP(int mabn, int makpkd, int madv)
        {
            InitializeComponent();
            _mabn = mabn;
            _makpkd = makpkd;
            _madv = madv;
        }
        private void chl_KhoaPhong_SelectedIndexChanged(object sender, EventArgs e)
        {
            int k = 0;
            k = chl_KhoaPhong.SelectedIndex;
            for (int i = 0; i < chl_KhoaPhong.ItemCount; i++)
            {
                if (i != k)
                {
                    chl_KhoaPhong.SetItemChecked(i, false);
                }
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities _dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            int _makp = 0;
            int _idkb = 0;
            for (int i = 0; i < chl_KhoaPhong.ItemCount; i++)
            {
                if (chl_KhoaPhong.GetItemChecked(i))
                {
                    _idkb =Convert.ToInt32( chl_KhoaPhong.GetItemValue(i));
                    break;
                }
            }
            var bnkb = _dataContext.BNKBs.Where(p => p.IDKB == _idkb).ToList();
            if (bnkb.Count > 0)
                _makp = bnkb.First().MaKP == null ? 0 : Convert.ToInt32(bnkb.First().MaKP) ;
            if (_makp == 0)
            {
                var sua = (from dt in _dataContext.DThuocs
                           join dtct in _dataContext.DThuoccts on dt.IDDon equals dtct.IDDon
                           where (dt.MaBNhan == _mabn && (dtct.MaKP == _makpkd || dtct.MaKP ==null) && dtct.MaDV == _madv)
                           select dtct).ToList();
                int j = 0;
                foreach (var a in sua)
                {
                    a.MaKP = _makp;
                    a.IDKB = _idkb;
                    _dataContext.SaveChanges();
                    j++;
                }
                if (j > 0)
                {
                    MessageBox.Show("Cập nhật thành công!");
                    this.Dispose();
                }
            }
            else {
                MessageBox.Show("Khoa phòng không hợp lệ");
            }
        }

        private void frm_Update_MaKP_VP_Load(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities _dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var dskp = (from kp in _dataContext.KPhongs
                        join bnkb in _dataContext.BNKBs.Where(p => p.MaBNhan == _mabn) on kp.MaKP equals bnkb.MaKP
                        select new { kp.MaKP, kp.TenKP, bnkb.IDKB }).Distinct().ToList();
            chl_KhoaPhong.DataSource = dskp;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}