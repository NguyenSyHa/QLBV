using QLBV.Models.Dictionaries.KPhongs;
using QLBV.Providers.Business.Datacommunication;
using QLBV.Providers.StoredProcedure;
using QLBV_Database;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Linq;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLBV.FormNhap
{
    public partial class Frm_DoiKPNhanh_24012 : Form
    {
        private ExcuteStoredProcedureProvider _excuteStoredProcedureProvider;

        public Frm_DoiKPNhanh_24012()
        {
            _excuteStoredProcedureProvider = new ExcuteStoredProcedureProvider();
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        List<KPhong> _lKPall = new List<KPhong>();
        private void Frm_DoiKPNhanh_24012_Load(object sender, EventArgs e)
        {
            string spName = "sp_RoomList";
            Dictionary<string, string> para = new Dictionary<string, string>()
            {
                {"@maCB",DungChung.Bien.MaCB },
                {"@viewKP","5" }//5: là hiển thị tất cả các KP theo mã cán bộ
            };
            var lstKP =_excuteStoredProcedureProvider.ExcuteStoredProcedure<KPhongModel>(spName, para);
            lupMaKP.Properties.DataSource = lstKP;
        }
        private void btnLuuKb_Click(object sender, EventArgs e)
        {
            CanBo canbo = dataContext.CanBoes.Single(p => p.MaCB == DungChung.Bien.MaCB);
            canbo.MaKP = lupMaKP.EditValue == null ? 0 : Convert.ToInt32(lupMaKP.EditValue);
            dataContext.SaveChanges();
            if (dataContext.SaveChanges() >= 0)
            {
                MessageBox.Show("Bấm ok để khởi động lại ứng dụng");
                try
                {
                    Process.Start(Application.StartupPath + "\\QLBV.exe");
                    Process.GetCurrentProcess().Kill();
                }
                catch
                { }
            }
            else
                MessageBox.Show("Lưu thất bại!!");
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
