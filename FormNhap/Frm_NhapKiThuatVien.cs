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
    public partial class Frm_NhapKiThuatVien : DevExpress.XtraEditors.XtraForm
    {
        int madv;
        int _int_maBN;
        int IDCD;
        QLBV_Database.QLBVEntities dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        public Frm_NhapKiThuatVien(int _madv, int _intMaBN, int _IDCD)
        {
            InitializeComponent();
            madv = _madv;
            _int_maBN = _intMaBN;
            IDCD = _IDCD;

        }
        ChiDinh nxrv;
        private void Frm_NhapKiThuatVien_Load(object sender, EventArgs e)
        {
          
            nxrv = (from cls in dataContext.CLS.Where(o => o.MaBNhan == _int_maBN)
                    join cd in dataContext.ChiDinhs.Where(o => o.MaDV == madv) on cls.IdCLS equals cd.IdCLS
                    select cd).OrderBy(o => o.IDCD).FirstOrDefault();
            DsBacSyThucHien();
            txtNhanXetRaVien.Text = nxrv != null ? nxrv.NXRaVien : "";
        }
        private class dsbs
        {
            public string TenCB { get; set; }
            public bool Chon { get; set; }
        }
        List<dsbs> _listBS = new List<dsbs>();
       

        private void DsBacSyThucHien()
        {
            string[] DSCapBac = new string[] { "KTV", "Điều dưỡng", "Cao đẳng điều dưỡng", "Điều dưỡng CKI", "Kỹ thuật viên trung cấp", "Cử nhân kỹ thuật viên", "Cao đẳng điều dưỡng", "Điều dưỡng trung cấp", "Cao đẳng kỹ thuật viên", "Cử nhân điều dưỡng" };
            int MaKP = dataContext.BNKBs.Where(p=>p.MaBNhan == _int_maBN).Max(p => p.MaKP).Value;
            _listBS = (from kp in dataContext.KPhongs.Where(p=>p.MaKP == MaKP)
                       join cb in dataContext.CanBoes.Where(p => DSCapBac.Contains(p.ChucVu)) on kp.MaKP equals cb.MaKP
                       group new { cb } by new { cb.TenCB } into k
                       select new dsbs { TenCB = k.Key.TenCB, Chon = false }).ToList();
            grcBSThucHien.DataSource = _listBS;
        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            if (nxrv != null)
            {
               
                var cdUpdate = dataContext.ChiDinhs.FirstOrDefault(o => o.IDCD == nxrv.IDCD);
                if (cdUpdate != null)
                {
                    cdUpdate.NXRaVien = txtNhanXetRaVien.Text;
                    dataContext.SaveChanges();
                }

            }
            List<string> cb = new List<string>();
            foreach (var item in _listBS.Where(p => p.Chon == true))
            {
                string HoTenCB = item.TenCB.TrimEnd();
                if (HoTenCB != null)
                {
                    string[] TenCB = HoTenCB.Split(' ');
                    if (TenCB.Length > 0)
                    {
                        cb.Add(TenCB[TenCB.Length - 1]);

                    }

                }

            }
            string DSCanBoTH = string.Join(", ", cb);
            int KetQuaDT = rgKetQuaDT.SelectedIndex;
            QLBV.CLS.InPhieu.InPhieuDieuTri_14018(madv, _int_maBN, QLBV.CLS.InPhieu.TypePhieuDieuTri14018.DieuTri, null, IDCD, txtCanBoThucHien.Text, txtNhanXetRaVien.Text, KetQuaDT, DSCanBoTH);
            this.Close();
        }
    }
}