using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
//using System.Web.UI.WebControls;

namespace QLBV.FormThamSo
{
    public partial class frm_DSSDBuong_Giuong : DevExpress.XtraEditors.XtraForm
    {
        int _makp = 0;
        string _nam = Convert.ToString(DateTime.Now.Year);
        string _buong = "";
        string _giuong = "";
        string _buong2 = "";
        public frm_DSSDBuong_Giuong()
        {
            InitializeComponent();
        }
        public frm_DSSDBuong_Giuong(int makp1,string nam,string buong)
        {
            _makp = makp1;
            _nam = nam;
            _buong = buong;
            InitializeComponent();
        }
        public frm_DSSDBuong_Giuong(int makp1)
        {
            _makp = makp1;
            InitializeComponent();
        }
        private class Buong 
        {
            private string buongg;

            public string Buongg
            {
                get { return buongg; }
                set { buongg = value; }
            }
        }

        private class Buong1
        {
            private string buongg1;

            public string Buongg1
            {
                get { return buongg1; }
                set { buongg1 = value; }
            }
            private string buongg3;

            public string Buongg3
            {
                get { return buongg3; }
                set { buongg3 = value; }
            }
            private string buongg2;

            public string Buongg2
            {
                get { return buongg2; }
                set { buongg2 = value; }
            }
        }

        private class Giuong
        {
            private string giuongg1;

            public string Giuongg1
            {
                get { return giuongg1; }
                set { giuongg1 = value; }
            }
            private string giuongg2;

            public string Giuongg2
            {
                get { return giuongg2; }
                set { giuongg2 = value; }
            }
            private string giuongg3;

            public string Giuongg3
            {
                get { return giuongg3; }
                set { giuongg3 = value; }
            }
            private string giuongg4;

            public string Giuongg4
            {
                get { return giuongg4; }
                set { giuongg4 = value; }
            }
            private string giuongg5;

            public string Giuongg5
            {
                get { return giuongg5; }
                set { giuongg5 = value; }
            }
            private string giuongg6;

            public string Giuongg6
            {
                get { return giuongg6; }
                set { giuongg6 = value; }
            }
            private string giuongg7;

            public string Giuongg7
            {
                get { return giuongg7; }
                set { giuongg7 = value; }
            }
            private string giuongg8;

            public string Giuongg8
            {
                get { return giuongg8; }
                set { giuongg8 = value; }
            }
        }

        private class bnkb
        {
            int? maBNhan;

            public int? MaBNhan
            {
                get { return maBNhan; }
                set { maBNhan = value; }
            }
            string tenBNhan;

            public string TenBNhan
            {
                get { return tenBNhan; }
                set { tenBNhan = value; }
            }
            string buong;

            public string Buong
            {
                get { return buong; }
                set { buong = value; }
            }
            string giuong;

            public string Giuong
            {
                get { return giuong; }
                set { giuong = value; }
            }
        }

        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        List<QLBV.FormThamSo.frm_NhapBuongGiuongKeKoach.dsBuongGiuong> _da = new List<QLBV.FormThamSo.frm_NhapBuongGiuongKeKoach.dsBuongGiuong>();
        List<QLBV.FormThamSo.frm_NhapBuongGiuongKeKoach.dsBuongGiuong> _da1 = new List<QLBV.FormThamSo.frm_NhapBuongGiuongKeKoach.dsBuongGiuong>();
        private void frm_DSSDBuong_Giuong_Load(object sender, EventArgs e)
        {
            lupngayden.DateTime = System.DateTime.Now;
            lupNgaytu.DateTime = System.DateTime.Now.AddMonths(-1);
            List<KPhong> _lkp = new List<KPhong>();
            _lkp = data.KPhongs.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.LamSang || p.PLoai == DungChung.Bien.st_PhanLoaiKP.PhongKham).ToList();
            lupKhoaphong.Properties.DataSource = _lkp;
            if(_makp != 0)
            lupKhoaphong.EditValue = _makp;
            addrow();
            _buong = "";
        }
        List<Giuong> _giuong1 = new List<Giuong>();

        private void addrow()
        {
            DateTime ngayden = DungChung.Ham.NgayDen(lupngayden.DateTime);
            DateTime ngaytu = DungChung.Ham.NgayTu(lupNgaytu.DateTime);
            int makp = 0;
            if (lupKhoaphong.EditValue != null)
            {
                makp = Convert.ToInt32(lupKhoaphong.EditValue);
            }
            string nam = lupngayden.DateTime.Year.ToString();
            _da = QLBV.FormThamSo.frm_NhapBuongGiuongKeKoach.getBuongGiuong(data, makp, nam);

            string buong2 = Convert.ToString(lupBuong.EditValue);
            if (buong2 != "" && buong2 != "Tất cả")
                _da = _da.Where(p => p.buong == buong2).ToList();
            int sob = _da.GroupBy(p => p.buong).Count();
            var buong = _da.GroupBy(p => p.buong).ToList();
            var giuong = _da.GroupBy(p => p.giuongTT).ToList();
            var dsa1 = (from b in data.BNKBs.Where(p => p.NgayKham <= ngayden)
                        join a in data.BenhNhans.Where(p => p.NoiTru == 1) on b.MaBNhan equals a.MaBNhan
                        join c in data.RaViens on b.MaBNhan equals c.MaBNhan into k
                        from k1 in k.DefaultIfEmpty()
                        select new
                        {
                            a.TenBNhan,
                            a.MaBNhan,
                            b.IDKB,
                            k1.NgayRa
                        }).ToList();
            var dsa = (from a in dsa1.Where(p => p.NgayRa > ngayden || p.NgayRa == null)
                       group a by new { a.TenBNhan, a.MaBNhan } into kq
                       select new
                       {
                           kq.Key.TenBNhan,
                           kq.Key.MaBNhan,
                           IDKB = kq.Max(p => p.IDKB)
                       }).ToList();
            var ds1 = (from a in dsa
                      join b in data.BNKBs.Where(p => p.MaKP == makp).Where(p => p.PhuongAn != 3) on a.IDKB equals b.IDKB
                      select new
                      {
                          a.MaBNhan,
                          a.TenBNhan,
                          b.Buong,
                          b.Giuong,
                      }).ToList();
            List<bnkb> ds = new List<bnkb>();
            foreach(var item in ds1)
            {
                bnkb them = new bnkb();
                them.MaBNhan = item.MaBNhan;
                them.TenBNhan = item.TenBNhan;
                string[] arr = item.Buong.Split(';');
                if(arr.Count()>0)
                them.Buong = arr[arr.Count() - 1];
                string[] arr1 = item.Giuong.Split(';');
                if (arr1.Count() > 0)
                them.Giuong = arr1[arr1.Count() - 1];
                ds.Add(them);
            }
            var da = (from a in _da.Where(p => p.buong != "" && p.giuongTT != "")
                      join b in ds on new { buong = a.buong, giuongTT = a.giuongTT } equals new { buong = b.Buong, giuongTT = b.Giuong } into k
                      from k1 in k.DefaultIfEmpty()
                      select new
                      {
                          a.buong,
                          a.giuongTT,
                          MaBNhan = (k1 != null) ? Convert.ToString(k1.MaBNhan) : null,
                          TenBN = (k1 != null) ? k1.TenBNhan : null
                      }).ToList();
            //foreach (var item in _da)
            //{
            //    Giuong moi = new Giuong();
            //    moi.Giuongg = item.giuongTT + " _ " + item.buong;
            //    _giuong1.Add(moi);
            //}
            da = (from a in da group a by new { a.buong, a.giuongTT, a.MaBNhan, a.TenBN } into kq select new { kq.Key.buong, kq.Key.giuongTT, kq.Key.MaBNhan, kq.Key.TenBN }).ToList();
            List<Buong1> ds_new = new List<Buong1>();
            List<Buong1> ds_new1 = new List<Buong1>();
            ds_new.Clear();
            ds_new1.Clear();
            foreach (var item2 in buong)
            {
                foreach (var item1 in da.Where(p => p.buong == item2.Key.ToString()).OrderBy(p => p.buong).ThenBy(p => p.giuongTT))
                {
                    Buong1 them = new Buong1();
                    them.Buongg1 = item1.buong;
                    them.Buongg2 = item1.giuongTT;
                    if (item1.MaBNhan == null)
                    {

                        them.Buongg3 = " ";
                        ds_new.Add(them);
                        ds_new1 = ds_new;
                    }
                    else
                    {
                        if (ds_new.Where(p => p.Buongg1 == item1.buong && p.Buongg2 == item1.giuongTT).Count() > 0)
                        {
                            Buong1 sua = ds_new.Single(p => p.Buongg1 == item1.buong && p.Buongg2 == item1.giuongTT);
                            sua.Buongg3 += item1.MaBNhan + "_" + item1.TenBN +  ".\n  ";
                        }
                        else
                        {
                            them.Buongg3 += item1.MaBNhan + "_" + item1.TenBN + ".\n  ";
                            ds_new.Add(them);
                        }
                        ds_new1 = ds_new;
                    }
                }
            }
            int n = 0, n1 = 0;
            string a1 = "", a2 = "", a3 = "", a4 = "", a5 = "", a6 = "", a7 = "", a8 = "", a9 = "", a10 = "";
            _giuong1.Clear();
            foreach (var item2 in buong)
            {
                foreach (var item1 in ds_new.Where(p => p.Buongg1 == item2.Key.ToString()))
                {
                    if (n < 8)
                    {
                        if (n == 0)
                        {
                            a1 = item1.Buongg2 + " _ " + item1.Buongg1 + ".\n." + item1.Buongg3;
                            n1++;
                        }
                        if (n == 1)
                        {
                            a2 = item1.Buongg2 + " _ " + item1.Buongg1 + ".\n." + item1.Buongg3;
                        }
                        if (n == 2)
                        {
                            a3 = item1.Buongg2 + " _ " + item1.Buongg1 + ".\n." + item1.Buongg3;
                        }
                        if (n == 3)
                        {
                            a4 = item1.Buongg2 + " _ " + item1.Buongg1 + ".\n." + item1.Buongg3;
                        }
                        if (n == 4)
                        {
                            a5 = item1.Buongg2 + " _ " + item1.Buongg1 + ".\n." + item1.Buongg3;
                        }
                        if (n == 5)
                        {
                            a6 = item1.Buongg2 + " _ " + item1.Buongg1 + ".\n." + item1.Buongg3;
                        }
                        if (n == 6)
                        {
                            a7 = item1.Buongg2 + " _ " + item1.Buongg1 + ".\n." + item1.Buongg3;
                        }
                        if (n == 7)
                        {
                            a8 = item1.Buongg2 + " _ " + item1.Buongg1 + ".\n." + item1.Buongg3;
                            n = 0;
                            Giuong moi = new Giuong();
                            moi.Giuongg1 = a1;
                            moi.Giuongg2 = a2;
                            moi.Giuongg3 = a3;
                            moi.Giuongg4 = a4;
                            moi.Giuongg5 = a5;
                            moi.Giuongg6 = a6;
                            moi.Giuongg7 = a7;
                            moi.Giuongg8 = a8;
                            _giuong1.Add(moi);
                            a1 = ""; a2 = ""; a3 = ""; a4 = ""; a5 = ""; a6 = ""; a7 = ""; a8 = "";
                        }
                    }
                    if (n > 0 && n1 == 0)
                        n++;
                    if (n == 0 && n1 == 1)
                    {
                        n++;
                        n1 = 0;
                    }
                }
            }
            if (n > 0)
            {
                Giuong moi1 = new Giuong();
                moi1.Giuongg1 = a1;
                moi1.Giuongg2 = a2;
                moi1.Giuongg3 = a3;
                moi1.Giuongg4 = a4;
                moi1.Giuongg5 = a5;
                moi1.Giuongg6 = a6;
                moi1.Giuongg7 = a7;
                moi1.Giuongg8 = a8;
                _giuong1.Add(moi1);
            }
            gridControl2.DataSource = _giuong1.ToList();
        }

        private void gridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if(e.RowHandle >= 0)
            {
                if (e.Column.FieldName == "Giuongg1")
                {
                    if (gridView2.GetRowCellValue(e.RowHandle, "Giuongg1") != null)
                    {
                        String celVL = gridView2.GetRowCellValue(e.RowHandle, "Giuongg1").ToString();
                        if (celVL.Split('.').Count() - 1 > 2)
                        {
                            e.Appearance.BackColor = Color.FromArgb(255, 224, 192);
                        }
                        if (celVL.Split('.').Count() - 1 == 2)
                            e.Appearance.BackColor = Color.FromArgb(181, 181, 181);
                    }
                }
                if (e.Column.FieldName == "Giuongg2")
                {
                    if(gridView2.GetRowCellValue(e.RowHandle, "Giuongg2") != null)
                    { 
                        String celVL = gridView2.GetRowCellValue(e.RowHandle, "Giuongg2").ToString();
                        if (celVL.Split('.').Count() - 1 > 2)
                        {
                            e.Appearance.BackColor = Color.FromArgb(255, 224, 192);
                        }
                        if (celVL.Split('.').Count() - 1 == 2)
                            e.Appearance.BackColor = Color.FromArgb(181, 181, 181);
                    }
                }
                if (e.Column.FieldName == "Giuongg3")
                {
                    if (gridView2.GetRowCellValue(e.RowHandle, "Giuongg3") != null)
                    {
                        String celVL = gridView2.GetRowCellValue(e.RowHandle, "Giuongg3").ToString();
                        if (celVL.Split('.').Count() - 1 > 2)
                        {
                            e.Appearance.BackColor = Color.FromArgb(255, 224, 192);
                        }
                        if (celVL.Split('.').Count() - 1 == 2)
                            e.Appearance.BackColor = Color.FromArgb(181, 181, 181);
                    }
                }
                if (e.Column.FieldName == "Giuongg4")
                {
                    if (gridView2.GetRowCellValue(e.RowHandle, "Giuongg4") != null)
                    {
                        String celVL = gridView2.GetRowCellValue(e.RowHandle, "Giuongg4").ToString();
                        if (celVL.Split('.').Count() - 1 > 2)
                        {
                            e.Appearance.BackColor = Color.FromArgb(255, 224, 192);
                        }
                        if (celVL.Split('.').Count() - 1 == 2)
                            e.Appearance.BackColor = Color.FromArgb(181, 181, 181);
                    }
                }
                if (e.Column.FieldName == "Giuongg5")
                {
                    if (gridView2.GetRowCellValue(e.RowHandle, "Giuongg5") != null)
                    {
                        String celVL = gridView2.GetRowCellValue(e.RowHandle, "Giuongg5").ToString();
                        if (celVL.Split('.').Count() - 1 > 2)
                        {
                            e.Appearance.BackColor = Color.FromArgb(255, 224, 192);
                        }
                        if (celVL.Split('.').Count() - 1 == 2)
                            e.Appearance.BackColor = Color.FromArgb(181, 181, 181);
                    }
                }
                if (e.Column.FieldName == "Giuongg6")
                {
                    if (gridView2.GetRowCellValue(e.RowHandle, "Giuongg6") != null)
                    {
                        String celVL = gridView2.GetRowCellValue(e.RowHandle, "Giuongg6").ToString();
                        if (celVL.Split('.').Count() - 1 > 2)
                        {
                            e.Appearance.BackColor = Color.FromArgb(255, 224, 192);
                        }
                        if (celVL.Split('.').Count() - 1 == 2)
                            e.Appearance.BackColor = Color.FromArgb(181, 181, 181);
                    }
                }
                if (e.Column.FieldName == "Giuongg7")
                {
                    if (gridView2.GetRowCellValue(e.RowHandle, "Giuongg7") != null)
                    {
                        String celVL = gridView2.GetRowCellValue(e.RowHandle, "Giuongg7").ToString();
                        if (celVL.Split('.').Count() - 1 > 2)
                        {
                            e.Appearance.BackColor = Color.FromArgb(255, 224, 192);
                        }
                        if (celVL.Split('.').Count() - 1 == 2)
                            e.Appearance.BackColor = Color.FromArgb(181, 181, 181);
                    }
                }
                if (e.Column.FieldName == "Giuongg8")
                {
                    if (gridView2.GetRowCellValue(e.RowHandle, "Giuongg8") != null)
                    {
                        String celVL = gridView2.GetRowCellValue(e.RowHandle, "Giuongg8").ToString();
                        if (celVL.Split('.').Count() - 1 > 2)
                        {
                            e.Appearance.BackColor = Color.FromArgb(255, 224, 192);
                        }
                        if (celVL.Split('.').Count() - 1 == 2)
                            e.Appearance.BackColor = Color.FromArgb(181, 181, 181);
                    }
                }
            }
        }

        private void lupKhoaphong_EditValueChanged(object sender, EventArgs e)
        {
            int makp = 0;
            string nam = lupngayden.DateTime.Year.ToString();
            if (lupKhoaphong.EditValue != null)
            {
                makp = Convert.ToInt32(lupKhoaphong.EditValue);
            }
            _da1 = QLBV.FormThamSo.frm_NhapBuongGiuongKeKoach.getBuongGiuong(data, makp, nam);
            if(makp != 0)
            {
                var mb = (from a in _da1 group a by a.buong into kq select new { buongg = kq.Key }).ToList();
                List<Buong> buong1 = new List<Buong>();
                Buong moi = new Buong();
                    moi.Buongg = "Tất cả";
                    buong1.Add(moi);
                foreach(var item in mb)
                {
                    Buong moi1 = new Buong();
                    moi1.Buongg = item.buongg;
                    buong1.Add(moi1);
                }
                bindingSource2.DataSource = buong1;
                lupBuong.Properties.DataSource = bindingSource2;
            }
            if (_buong != "")
                lupBuong.EditValue = _buong;
        }
        string ass = "";
        private void gridView1_FocusedColumnChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs e)
        {
            if(e != null && e.FocusedColumn != null)
            {
                ass= e.FocusedColumn.ToString();                   
            }
        }

        private void gridView2_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            string[] arr = gridView2.GetFocusedValue().ToString().Split('.');
            string[] arr1 = arr[0].ToString().Split('_');
            if (arr1.Length > 1)
            {
                _giuong = arr1[0];
                _buong2 = arr1[1];
            }
            else
            {
                _giuong = "";
                _buong2 = "";
            }
        }

        private void gridControl2_DoubleClick(object sender, EventArgs e)
        {
            if (_giuong == "" || _buong2 == "")
            {
                MessageBox.Show("Chưa chọn giường!");
            }
            else
            {
                FormNhap.usDieuTri._giuong = _giuong;
                FormNhap.usDieuTri._buong = _buong2;
                this.Close();
            }
        }

        private void lupBuong_EditValueChanged(object sender, EventArgs e)
        {
            addrow();
        }
    }
}