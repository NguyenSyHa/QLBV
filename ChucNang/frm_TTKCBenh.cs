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
    public partial class frm_TTKCBenh : DevExpress.XtraEditors.XtraForm
    {
        public frm_TTKCBenh()
        {
            InitializeComponent();
        }
        int _mbn = 0;
        public frm_TTKCBenh(int ma)
        {
            InitializeComponent();
            _mbn = ma;
        }
        QLBV_Database.QLBVEntities _data=new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void frm_TTKCBenh_Load(object sender, EventArgs e)
        {
            var bn = _data.BenhNhans.Where(p => p.MaBNhan==_mbn).ToList();
            grcTTHC.DataSource = bn;
            var bnkb = _data.BNKBs.Where(p => p.MaBNhan==_mbn).OrderBy(p => p.IDKB).ToList();
            grcBNKB.DataSource = bnkb;
            var ravien = (from rv in _data.RaViens.Where(p => p.MaBNhan== (_mbn)).OrderBy(p => p.NgayRa) select new { 
                rv.NgayVao,
            rv.ChanDoan, rv.MaICD, rv.NgayRa, rv.SoNgaydt, rv.KetQua,Status= rv.Status== 1 ?"Chuyển viện": "Ra viện",rv.SoRaVien,rv.SoGT,rv.SoChuyenVien,rv.MaKP,rv.MaBVC
            }).ToList();
            grcRaVien.DataSource = ravien;
            var Cb = _data.CanBoes.ToList();
            lupMaCB.DataSource = Cb;
            lupMaCBTT.DataSource = Cb;
            var kp = _data.KPhongs.ToList();
            lupKhoaPhong.DataSource = kp;
            lupMaKPRV.DataSource = kp;
            lupMaKPTT.DataSource = kp;
            grcThanhToan.DataSource = _data.VienPhis.Where(p => p.MaBNhan ==_mbn).ToList();
        }
    }
}