using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QLBV.ChucNang
{
    public partial class frm_ChooseRoom : Form
    {
        public frm_ChooseRoom()
        {
            InitializeComponent();
        }

        private void frm_ChooseRoom_Load(object sender, EventArgs e)
        {
            LoadDataCombo();
        }

        private void LoadDataCombo()
        {
            QLBV_Database.QLBVEntities dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var kpsd = (from adm in dataContext.ADMINs.Where(o => o.TenDN == DungChung.Bien.TenDN)
                        join cb in dataContext.CanBoes on adm.MaCB equals cb.MaCB
                        select new { cb.MaKPsd, cb.MaKP }).FirstOrDefault();
            if (kpsd != null)
            {
                List<int> maKPsd = new List<int>();
                var spkpsd = kpsd.MaKPsd.Split(';').Distinct().Where(o => !string.IsNullOrWhiteSpace(o)).Select(o => Convert.ToInt32(o)).ToList();
                maKPsd.AddRange(spkpsd);
                maKPsd.Add(kpsd.MaKP ?? 0);
                gridControlChooseRoom.DataSource = dataContext.KPhongs.Where(o => maKPsd.Contains(o.MaKP)).ToList();
            }
        }

        private void repositoryItemButtonEdit_Choose_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var row = (KPhong)gridViewChooseRoom.GetFocusedRow();
            if (row != null)
            {
                QLBV_Database.QLBVEntities dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                var loginCheck = dataContext.LOGIN_LOG.Where(o => o.LOGIN_NAME == DungChung.Bien.TenDN && o.ID != DungChung.Bien.ID_LOGIN_LOG && o.STATUS).ToList();
                if (loginCheck.Count > 0)
                {
                    if (loginCheck.Exists(o => o.MaKP != row.MaKP))
                    {
                        var kpht = loginCheck.FirstOrDefault().MaKP;
                        var khoaPhong = dataContext.KPhongs.FirstOrDefault(p => p.MaKP == kpht);
                        if (khoaPhong != null)
                        {
                            MessageBox.Show(string.Format("Tài khoản đang làm việc tại phòng: {0}. Bạn không thể chọn phòng khác", khoaPhong.TenKP));
                            return;
                        }
                    }
                }

                var loginUpdate = dataContext.LOGIN_LOG.FirstOrDefault(o => o.ID == DungChung.Bien.ID_LOGIN_LOG);
                if(loginUpdate!=null)
                {
                    loginUpdate.MaKP = row.MaKP;
                    dataContext.SaveChanges();
                }
            }
        }
    }
}
