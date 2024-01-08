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
    public partial class frm_UpdateLetet : DevExpress.XtraEditors.XtraForm
    {
        public frm_UpdateLetet()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void btnInBC_Click(object sender, EventArgs e)
        {
            
            DateTime ngay1 = DungChung.Ham.NgayTu(dtletet.DateTime);
            DateTime ngay2 = DungChung.Ham.NgayDen(dtletet.DateTime);
            if (CheckCuoiTuan(dtletet.DateTime) == false)
            {
                var update = data.ChiDinhs.Where(p => p.NgayTH >= ngay1 && p.NgayTH <= ngay2 && (p.NgoaiGioHC != 1 || p.NgoaiGioHC == null)).Select(p => p.IDCD).ToList();
                if (update.Count > 0)
                {
                    foreach (var item in update)
                    {
                        ChiDinh _cd = data.ChiDinhs.Single(p => p.IDCD == item);
                        _cd.NgoaiGioHC = 2;
                        data.SaveChanges();
                    }
                    MessageBox.Show("Cập nhật thành công!");
                }
                else MessageBox.Show("Không có dữ liệu cần cập nhật!");
            }
            else MessageBox.Show("Không có dữ liệu cần cập nhật!");
        }

        private void frm_UpdateLetet_Load(object sender, EventArgs e)
        {
            dtletet.DateTime = DateTime.Now;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            var update1 = data.ChiDinhs.Where(p =>( p.NgoaiGioHC != 2 || p.NgoaiGioHC == null )&& p.NgayTH != null).Select(p => new { p.IDCD, p.NgayTH}).ToList();
            if (update1.Count > 0)
            {
                foreach (var item in update1)
                {
                    ChiDinh _cd1 = data.ChiDinhs.Single(p => p.IDCD == item.IDCD);  
                    if (CheckCuoiTuan(_cd1.NgayTH.Value) == true || CheckNGioHC(_cd1.NgayTH.Value) == true)
                    { 
                        _cd1.NgoaiGioHC = 1;
                    }
                    else _cd1.NgoaiGioHC = 0;
                    
                    data.SaveChanges();
                }
                MessageBox.Show("Cập nhật thành công!");
            }
            else MessageBox.Show("Không có dữ liệu cần cập nhật!");
        }

        public static bool CheckNGioHC(DateTime dt)
        {
            DateTime dttu1 = new DateTime(dt.Year, dt.Month, dt.Day, DungChung.Bien.GioTu[0], DungChung.Bien.PhutTu[0], 0);
            DateTime dtden1 = new DateTime(dt.Year, dt.Month, dt.Day, DungChung.Bien.GioDen[0], DungChung.Bien.PhutDen[0], 0);
            DateTime dttu2 = new DateTime(dt.Year, dt.Month, dt.Day, DungChung.Bien.GioTu[1], DungChung.Bien.PhutTu[1], 0);
            DateTime dtden2 = new DateTime(dt.Year, dt.Month, dt.Day, DungChung.Bien.GioDen[1], DungChung.Bien.PhutDen[1], 0);
            if (dt >= dttu1 && dt <= dtden1)
                return false;
            if (dt >= dttu2 && dt <= dtden2)
                return false;
            return true;
        }
        public static bool CheckCuoiTuan(DateTime dt)
        {
            var thu1 = dt.DayOfWeek;
            int thu = Convert.ToInt32(thu1);
            if (thu == 0)
                return true;
            if (thu == 6)
                return true;
            return false;
        }
    }
}