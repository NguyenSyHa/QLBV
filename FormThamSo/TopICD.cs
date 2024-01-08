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
    public partial class TopICD : DevExpress.XtraEditors.XtraForm
    {
        public TopICD()
        {
            InitializeComponent();
        }
        public class micd
        {
            private string maICD;

            public string MaICD
            {
                get { return maICD; }
                set { maICD = value; }
            }
            private int soLan;

            public int SoLan
            {
                get { return soLan; }
                set { soLan = value; }
            }
            private string tenICD;

            public string TenICD
            {
                get { return tenICD; }
                set { tenICD = value; }
            }
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        List<micd> _ds1 = new List<micd>();
        List<micd> _ds = new List<micd>();
        private void TopICD_Load(object sender, EventArgs e)
        {
            LupNgaytu.DateTime = System.DateTime.Now.AddMonths(-1);
            LupNgayden.DateTime = System.DateTime.Now;
            timkiem();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            timkiem();
        }
        private void timkiem()
        {
            _ds.Clear();
            _ds1.Clear();
            DateTime tungay = DungChung.Ham.NgayTu(LupNgaytu.DateTime);
            DateTime denngay = DungChung.Ham.NgayTu(LupNgayden.DateTime);
            var ds = data.BNKBs.Where(p => p.NgayKham >= tungay && p.NgayKham <= denngay).ToList();
            foreach (var item in ds)
            {
                if (item.MaICD != null && item.MaICD != "")
                {
                    micd them = new micd();
                    them.MaICD = item.MaICD;
                    them.SoLan = 1;
                    _ds1.Add(them);
                }
                if (item.MaICD2 != null && item.MaICD2 != "")
                {
                    string[] arr = item.MaICD2.Split(';');
                    for (int i = 0; i < arr.Length; i++)
                    {
                        if (arr[i] != "")
                        {
                            micd them1 = new micd();
                            them1.MaICD = arr[i];
                            them1.SoLan = 1;
                            _ds1.Add(them1);
                        }
                    }
                }
            }
            var _ds2 = (from a in _ds1
                        join b in data.ICD10 on a.MaICD equals b.MaICD
                        group new { a, b } by new { a.MaICD, b.TenICD, b.TenWHO} into kq
                        select new { kq.Key, SoLan = kq.Sum(p => p.a.SoLan), kq.Key.TenICD }).OrderByDescending(p => p.SoLan).ToList();
            foreach (var item in _ds2)
            {
                micd them = new micd();
                them.MaICD = item.Key.MaICD;
                them.SoLan = item.SoLan;
                them.TenICD = item.TenICD;
                _ds.Add(them);
            }
            bindingSource1.DataSource = _ds.ToList();
            gridControl1.DataSource = bindingSource1;
        }
    }
}