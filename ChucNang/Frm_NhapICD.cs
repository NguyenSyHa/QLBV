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
    public partial class Frm_NhapICD : DevExpress.XtraEditors.XtraForm
    {
        public Frm_NhapICD()
        {
            InitializeComponent();
        }
        public Frm_NhapICD(List<BenhNhan> BenhNhan1)
        {
            InitializeComponent();
            _BenhNhan = BenhNhan1;
        }
        private class BC
        {
            private string  _tenbn, _maicd, _chandoan;
            int _mabn;
            public int MaBN
            {
                set { _mabn = value; }
                get { return _mabn; }
            }
            public string TenBN
            {
                set { _tenbn = value; }
                get { return _tenbn; }
            }
            public string MaICD
            {
                set { _maicd = value; }
                get { return _maicd; }
            }
            public string ChanDoan
            {
                set { _chandoan = value; }
                get { return _chandoan; }
            }
        }
        QLBV_Database.QLBVEntities _Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        List<BenhNhan> _BenhNhan = new List<BenhNhan>();
        List<BC> _BC = new List<BC>();
        private void Frm_NhapICD_Load(object sender, EventArgs e)
        {
            _BC.Clear();
            var ICD = (from id in _Data.ICD10 select new { id.MaICD }).ToList();
            LupMaICD.DataSource = ICD.OrderBy(p => p.MaICD);
            foreach (var a in _BenhNhan)
            {
                BC themmoi = new BC();
                themmoi.MaBN = a.MaBNhan;
                themmoi.TenBN = a.TenBNhan;
                var kb = _Data.BNKBs.Where(p => p.MaBNhan == a.MaBNhan).ToList();
                if (kb.Count > 0)
                {
                    if (kb.Last().MaICD != null)
                    {
                        themmoi.MaICD = kb.Last().MaICD;
                    }
                    if (kb.Last().ChanDoan != null)
                    {
                        themmoi.ChanDoan = kb.Last().ChanDoan;
                    }
                }
                _BC.Add(themmoi);
            }
            grcBenhNhan.DataSource = _BC;
        }

        private void textEdit1_EditValueChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtTimkiem.Text))
            {
                int rs;
                int _int_maBN = 0;
                if (Int32.TryParse(txtTimkiem.Text, out rs))
                _int_maBN = Convert.ToInt32(txtTimkiem.Text);
                grcBenhNhan.DataSource = _BC.Where(p => p.MaBN== _int_maBN || p.TenBN.Contains(txtTimkiem.Text));
            }
            else
            {
                grcBenhNhan.DataSource = _BC;
            }
        }

        private void sbtThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void sbtLuu_Click(object sender, EventArgs e)
        {
            int b = 0;
            foreach (var a in _BC)
            {
                var tk = _Data.RaViens.Where(p => p.MaBNhan == a.MaBN).ToList();
                foreach (var item in  tk)
                {
                    item.MaICD = a.MaICD;
                    _Data.SaveChanges();
                    if (_Data.SaveChanges() >= 0)
                    {
                        b = 1;
                    }
                }
            }
            if (b == 1)
            {
                MessageBox.Show("Lưu thành công!");
            }
        }
    }
}