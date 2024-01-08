using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
namespace QLBV.FormThamSo
{
    public partial class frm_UpdateDVct : DevExpress.XtraEditors.XtraForm
    {
        public frm_UpdateDVct()
        {
            InitializeComponent();
        }

        private void btnUpdatePhauThuat_Click(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            List<DichVu> ldv = new List<DichVu>();
            String tenRG = cboTenRG.Text;
            String ma = "";
            if (tenRG == "Siêu âm")
            {
                ma = "sa";
            }
            else if (tenRG == "X-Quang")
            {
                ma = "xq";
            }
            else if (tenRG == "X-Quang CT")
            {
                ma = "xqct";
            }
            else if (tenRG == "Điện Tim")
            {
                ma = "dt";
            }
            else if (tenRG == "Nội soi")
            {
                ma = "ns";
            }
            else if (tenRG == "Nội soi Tai-Mũi-Họng")
            {
                ma = "nstmh";
            }
            else if (tenRG == "Phẫu thuật")
            {
                ma = "pt";
            }
            else if (tenRG == "Thủ thuật")
            {
                ma = "tt";
            }
            else
            {
                ma = "dvct";
            }
            ldv = (from dv in dataContext.DichVus.Where(p => p.Status == 1)
                   join tndv in dataContext.TieuNhomDVs.Where(p => p.TenRG == tenRG) on dv.IdTieuNhom equals tndv.IdTieuNhom
                   select dv).ToList();
            var dvct = dataContext.DichVucts.ToList();
            int i = 1;
            int dem = 0, tongso = 0, tontai = 0;
            tongso = ldv.Count;
            foreach (var item in ldv)
            {
                if (dvct.Where(p => p.MaDV == item.MaDV).ToList().Count == 0)
                {

                    DichVuct dvMoi = new DichVuct();
                    for (; i < 10000; i++)
                    {
                        string _madv = (ma + i).ToString().ToLower();
                        if (dvct.Where(p => p.MaDVct.ToLower() == (_madv)).ToList().Count == 0)
                        {
                            dvMoi.MaDVct = _madv;
                            dem++;
                            break;
                        }
                    }
                    dvMoi.TenDVct = item.TenDV;
                    dvMoi.MaDV = item.MaDV;
                    dvMoi.STT = 1;
                    dataContext.DichVucts.Add(dvMoi);
                    dataContext.SaveChanges();
                    i++;
                }
                else
                {
                    tontai++;
                }
            }

            MessageBox.Show("Update thành công " + dem + "/" + tongso + " bản ghi, đã tồn tại: " + tontai + " bản ghi");

        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}