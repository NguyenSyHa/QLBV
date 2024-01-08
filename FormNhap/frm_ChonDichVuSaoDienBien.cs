using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QLBV.FormNhap
{
    public partial class frm_ChonDichVuSaoDienBien : Form
    {
        public delegate void DelegateChonDichVu(List<ChonDichVu> data);
        DelegateChonDichVu dlgChonDichVu;
        int idDB;
        int solan;
        public frm_ChonDichVuSaoDienBien(int _idDB, DelegateChonDichVu _dlg, int _solan)
        {
            InitializeComponent();
            this.idDB = _idDB;
            dlgChonDichVu = _dlg;
            this.solan = _solan;
        }

        private void frm_ChonDichVuSaoDienBien_Load(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var dichvu = (from cls in dataContext.CLS.Where(o => o.IDDienBien == idDB)
                          join cd in dataContext.ChiDinhs on cls.IdCLS equals cd.IdCLS
                          join dv in dataContext.DichVus.Where(o => o.IDNhom == 8) on cd.MaDV equals dv.MaDV
                          select new ChonDichVu { SoLan = this.solan, IDCLS = cls.IdCLS, MaDV = dv.MaDV, NgayThang = cls.NgayThang, TenDV = dv.TenDV, IDCD = cd.IDCD }).Distinct().ToList();
            gridControlDichVu.DataSource = dichvu;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            List<ChonDichVu> listDV = new List<ChonDichVu>();
            var selectDV = gridViewDichVu.GetSelectedRows();
            if (selectDV.Count() <= 0)
            {
                MessageBox.Show("Chưa chọn dịch vụ");
                return;
            }
            else
            {
                foreach (var item in selectDV)
                {
                    var dvu = (ChonDichVu)gridViewDichVu.GetRow(item);
                    if (dvu != null)
                        listDV.Add(dvu);
                }
                if (dlgChonDichVu != null)
                    dlgChonDichVu(listDV);
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public class ChonDichVu
        {
            public int SoLan { get; set; }
            public int IDCLS { get; set; }
            public int IDCD { get; set; }
            public DateTime? NgayThang { get; set; }
            public int MaDV { get; set; }
            public string TenDV { get; set; }
        }
    }
}
