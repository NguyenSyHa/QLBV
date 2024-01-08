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
    public partial class frm_SoLanSaoChiDinh : Form
    {
        int _idCLS;
        public delegate void ReloadForm();
        ReloadForm _dlgReload;
        public frm_SoLanSaoChiDinh(int idCLS, ReloadForm _dlgReload)
        {
            InitializeComponent();
            this._idCLS = idCLS;
            this._dlgReload = _dlgReload;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frm_SoLanSaoChiDinh_Load(object sender, EventArgs e)
        {

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (spSoLan.EditValue == null || spSoLan.Value <= 0)
            {
                MessageBox.Show("Số lần không hợp lệ");
                return;
            }

            var dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

            var cls = dataContext.CLS.FirstOrDefault(o => o.IdCLS == _idCLS);
            for (int i = 0; i < spSoLan.Value; i++)
            {
                int IdCLS = cls.IdCLS;
                CL moicls = new CL();
                moicls.MaBNhan = cls.MaBNhan;
                moicls.MaCB = cls.MaCB;
                moicls.MaKP = cls.MaKP;
                moicls.MaKPth = cls.MaKPth;
                moicls.NgayThang = cls.NgayThang;
                moicls.CapCuu = cls.CapCuu;
                moicls.Status = 0;
                moicls.ChanDoan = cls.ChanDoan;
                moicls.MaICD = cls.MaICD;
                dataContext.CLS.Add(moicls);
                dataContext.SaveChanges();
                int IdCLSNew = moicls.IdCLS;

                var chidinh = dataContext.ChiDinhs.Where(p => p.IdCLS == IdCLS).ToList();
                foreach (var items in chidinh)
                {
                    int idcd = items.IDCD;
                    ChiDinh themmoiCD = new ChiDinh();
                    themmoiCD.IdCLS = IdCLSNew;
                    themmoiCD.MaDV = items.MaDV;
                    themmoiCD.Status = 0;
                    themmoiCD.DonGia = items.DonGia;
                    themmoiCD.TrongBH = items.TrongBH;
                    themmoiCD.ChiDinh1 = items.ChiDinh1;
                    themmoiCD.XHH = 0;
                    dataContext.ChiDinhs.Add(themmoiCD);
                    if (dataContext.SaveChanges() >= 0)
                    {
                        int idCDnew = themmoiCD.IDCD;
                        var clsct = dataContext.CLScts.Where(p => p.IDCD == idcd).ToList();
                        foreach (var item2 in clsct)
                        {
                            CLSct themmoiCL = new CLSct();
                            themmoiCL.IDCD = idCDnew;
                            themmoiCL.MaDVct = item2.MaDVct;
                            themmoiCL.Status = 0;
                            dataContext.CLScts.Add(themmoiCL);
                            dataContext.SaveChanges();
                        }
                    }
                }

                if (DungChung.Bien.MaBV == "14018" || DungChung.Bien.MaBV == "14017")
                {
                    var ngayDB = moicls.NgayThang ?? DateTime.Now;
                    DungChung.Ham.Update_CLS_DienBienct(moicls.MaBNhan ?? 0, ngayDB.Date, moicls.MaKP);
                    dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                    var dienBien = dataContext.DienBiens.FirstOrDefault(o => o.MaBNhan == (moicls.MaBNhan ?? 0) && o.NgayNhap.Value.Day == ngayDB.Day && o.NgayNhap.Value.Month == ngayDB.Month && o.NgayNhap.Value.Year == ngayDB.Year && o.Loai == 1 && o.MaKP == moicls.MaKP);
                    if (dienBien != null)
                    {
                        dienBien.YLenh = "";
                        dienBien.MaKP = moicls.MaKP;
                        dataContext.SaveChanges();
                        DungChung.Ham.Update_DienBien_All(dienBien.ID, moicls.MaKP);
                    }
                    else
                    {
                        DienBien dienBienNew = new DienBien();
                        dienBienNew.NgayNhap = ngayDB;
                        dienBienNew.YLenh = "";
                        dienBienNew.DienBien1 = "";
                        dienBienNew.MaCB = DungChung.Bien.MaCB;
                        dienBienNew.Loai = 1;
                        dienBienNew.MaKP = moicls.MaKP;
                        dienBienNew.Ploai = 0;
                        dienBienNew.MaBNhan = moicls.MaBNhan;
                        dataContext.DienBiens.Add(dienBienNew);
                        dataContext.SaveChanges();

                        DungChung.Ham.Update_DienBien_All(dienBienNew.ID, moicls.MaKP);
                    }
                }
            }
            MessageBox.Show("Sao thành công");
            if (this._dlgReload != null)
                this._dlgReload();
            this.Close();
        }
    }
}
