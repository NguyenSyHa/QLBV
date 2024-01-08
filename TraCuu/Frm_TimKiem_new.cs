using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLBV.TraCuu
{
    public partial class Frm_TimKiem_new : Form
    {
        string id = "";
        public Frm_TimKiem_new(string ma)
        {
            InitializeComponent();
            id = ma;
        }
        QLBV_Database.QLBVEntities _Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        public delegate void _getstring(string chuoi1);
        public _getstring GetData;
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private class ICD
        {
            private string TenBenh;
            private string MaICD;
            private bool Chon;
            public string tenbenh
            { set { TenBenh = value; } get { return TenBenh; } }
            public string maicd
            { set { MaICD = value; } get { return MaICD; } }
            public bool chon
            { set { Chon = value; } get { return Chon; } }
        }
        public class MaBenh
        {
            private string maicd;
            private string tenbenh;
            public string MaICD
            {
                set { maicd = value; }
                get { return maicd; }
            }
            public string TenBenh
            {
                set { tenbenh = value; }
                get { return tenbenh; }
            }
        }
        class LSICD
        {
            public string ICD { get; set; }
        }
        class LSICDs
        {
            public string ICD { get; set; }
        }
        List<LSICDs> _lsts = new List<LSICDs>();
        List<LSICD> _lst = new List<LSICD>();
        List<ICD> _Icd = new List<ICD>();
        List<MaBenh> _MaBenh = new List<MaBenh>();
        private void grvICD_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "Chọn")
            {
                if (grvICD.GetFocusedRowCellValue("tenbenh") != null)
                {
                    string Ten = grvICD.GetFocusedRowCellValue("tenbenh").ToString();
                }
            }
        }

        private void Frm_TimKiem_new_Load(object sender, EventArgs e)
        {
            var icd = (from a in _Data.ICD10
                       select new { a.TenICD, a.MaICD }).ToList();
            txtMaicd.Text = id;
            if (icd.Count > 0)
            {
                foreach (var a in icd)
                {
                    ICD themmoi = new ICD();
                    themmoi.tenbenh = a.TenICD;
                    themmoi.maicd = a.MaICD;
                    _lst.Clear();
                    List<string> iCD = txtMaicd.Text.Split(';').ToList();
                    foreach (var ab in iCD)
                    {
                        LSICD item = new LSICD();
                        item.ICD = ab.ToString();
                        _lst.Add(item);

                        if (themmoi.maicd == ab.ToString())
                        {
                            themmoi.chon = true;
                        }
                    }
                    _Icd.Add(themmoi);
                    }
                    grcICD.DataSource = _Icd.OrderByDescending(p => p.chon).ThenBy(p => p.tenbenh).ToList();
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string chuoi = "";
            string tenbenh = "";
            String strSearch = "";
            grcICD.DataSource = null;
            grcICD.DataSource = _Icd.Where(p => p.maicd.ToLower().Contains(strSearch) || p.tenbenh.ToLower().Contains(strSearch)).ToList();
            _MaBenh.Clear();
            for (int k = 0; k < grvICD.DataRowCount; k++)
            {
                if (grvICD.GetRowCellValue(k, Chọn).ToString().ToLower() == "true")
                {
                    _MaBenh.Add(new MaBenh { MaICD = Convert.ToString(grvICD.GetRowCellValue(k, MaICD) == null ? "" : grvICD.GetRowCellValue(k, MaICD)) });
                }
            }
            foreach (var item in _MaBenh)
            {
                chuoi += item.MaICD.ToString() + ";";
                tenbenh += item.TenBenh + ";";
            }
            if (!string.IsNullOrEmpty(chuoi))
            {
                chuoi = chuoi.Remove(chuoi.Length - 1);
            }
            GetData(chuoi);
            this.Dispose();
        }
        private void grvICD_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                String TenBenh = grvICD.GetFocusedRowCellValue("tenbenh") != null ? grvICD.GetFocusedRowCellValue("tenbenh").ToString() : "";
                String MaICD = grvICD.GetFocusedRowCellValue("maicd") != null ? grvICD.GetFocusedRowCellValue("maicd").ToString() : "";
                txtMaICDKQ.Text = MaICD;
                txtTenBenhKQ.Text = TenBenh;
            }
            catch (Exception)
            {
            }
        }

        private void txtTimKiem_KeyUp(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            String strSearch = txtTimKiem.Text.ToLower();
            grcICD.DataSource = null;
            grcICD.DataSource = _Icd.Where(p => p.maicd.ToLower().Contains(strSearch) || p.tenbenh.ToLower().Contains(strSearch)).ToList();
        }

        private void btnclear_Click(object sender, EventArgs e)
        {
            GetData(null);
            this.Dispose();
        }
    }
}
