using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace QLBV.ChucNang
{
    public partial class frm_DSTamUng_TCTT_ct : DevExpress.XtraEditors.XtraForm
    {
        public frm_DSTamUng_TCTT_ct()
        {
            InitializeComponent();
        }
        int _id=0;
        public frm_DSTamUng_TCTT_ct(int id)
        {
            InitializeComponent();
            this._id = id;
        }
        List<TamUng> _ltung = new List<TamUng>();
        QLBV_Database.QLBVEntities _dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void frm_DSTamUng_TCTT_ct_Load(object sender, EventArgs e)
        {
             _dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            _ltung = _dataContext.TamUngs.Where(p => p.IDTamUng == _id).ToList();
            if (_ltung.Count > 0 && _ltung.First().Status == true)
                chk_Khoa.Checked = true;
            else
                chk_Khoa.Checked = false;
            lup_TenDVtu.DataSource = _dataContext.DichVus.ToList();
            List<TamUngct> _ltuct = _dataContext.TamUngcts.Where(p => p.IDTamUng == _id).Where(p => DungChung.Bien.MaBV == "30372" ? p.Status == 0 : true).ToList();
            grcTamUngct.DataSource = _ltuct;
        }
        public void _getValue(bool a)
        {
            _ktmatkhau = a;
        }
        public bool _ktmatkhau = false;
        private void btnLuu_Click(object sender, EventArgs e)
        {
                _ktmatkhau = false;
                                ChucNang.frm_CheckPass frm = new ChucNang.frm_CheckPass();
                                frm.ok = new ChucNang.frm_CheckPass._getdata(_getValue);
                                frm.ShowDialog();
                                if (_ktmatkhau)
                                {
                                    _dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                                    _ltung = _dataContext.TamUngs.Where(p => p.IDTamUng == _id).ToList();
                                    foreach (var item in _ltung)
                                    {

                                        item.Status = chk_Khoa.Checked;
                                        _dataContext.SaveChanges();
                                    }

                                }
        }
    }
}