using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;

namespace QLBV.FormThamSo
{
    public partial class frm_NhapBuongGiuongKeKoach : DevExpress.XtraEditors.XtraForm
    {

        public frm_NhapBuongGiuongKeKoach()
        {
            InitializeComponent();
        }
        public int _makp = -1;
        public frm_NhapBuongGiuongKeKoach(int makp)
        {
            _makp = makp;
            InitializeComponent();
        }
        public class dsBuongGiuong
        {
            public string buong
            { set; get; }
            public string giuongKH { set; get; }
            public string giuongTT { set; get; }
            public int makp { set; get; }
        }
        public class buongg
        {
            public string buong1
            { set; get; }
        }
        public class giuongg
        {
            public string giuong1
            { set; get; }
            public string mabuong
            { set; get; }
        }
        private static string kh1 = "";
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        List<dsBuongGiuong> _da = new List<dsBuongGiuong>();
        List<buongg> _buong = new List<buongg>();
        List<giuongg> _giuong = new List<giuongg>();
        List<dsBuongGiuong> _da1 = new List<dsBuongGiuong>();
        List<dsBuongGiuong> _da2 = new List<dsBuongGiuong>();
        List<dsBuongGiuong> _da3 = new List<dsBuongGiuong>();
        public static List<dsBuongGiuong> getBuongGiuong(QLBV_Database.QLBVEntities data, int MaKP, string nam)
        {

            List<dsBuongGiuong> _da = new List<dsBuongGiuong>();
            _da.Clear();
            var kp1 = data.KPhongs.Where(p => p.MaKP == MaKP && p.BuongGiuong != null && p.BuongGiuong.Contains(nam) && p.SoGiuongKH != null && p.SoGiuongKH.Contains(nam)).Select(p => new { p.BuongGiuong, p.SoGiuongKH }).FirstOrDefault();
            kh1 = "";
            if (kp1 != null)
            {
                string[] Nam = kp1.BuongGiuong.Split('|');
                string[] kh = kp1.SoGiuongKH.Split('|');
                foreach (var item4 in kh)
                {
                    if (item4.Contains(nam))
                    {
                        var a1 = item4.IndexOf(':') + 1;
                        kh1 = item4.Substring(a1, item4.Length - a1);
                    }
                }
                foreach (var item in Nam)
                {
                    if (item.Contains(nam))
                    {
                        var a = item.IndexOf(':') + 1;
                        string b = item.Substring(a, item.Length - a);
                        string[] buong = b.Split(';');
                        foreach (var item2 in buong)
                        {
                            if (!string.IsNullOrEmpty(item2))
                            {
                                string[] Buong = item2.Split('{');
                                for (int j = 0; j < Buong.Count(); j++)
                                {
                                    if (j % 2 == 0)
                                    {
                                        //buongg b1 = new buongg();
                                        //b1.buong1 = Buong[j];
                                        //_buong.Add(b1);

                                        string[] cc = Buong[j + 1].Split(',');
                                        for (int x = 0; x < cc.Count(); x++)
                                        {
                                            if (cc[x].Contains('}'))
                                            {
                                                dsBuongGiuong g2 = new dsBuongGiuong();
                                                g2.buong = Buong[j];
                                                g2.giuongTT = cc[x].Substring(0, cc[x].Length - 1);
                                                g2.giuongKH = kh1;
                                                g2.makp = MaKP;
                                                _da.Add(g2);
                                            }
                                            else
                                            {
                                                dsBuongGiuong g1 = new dsBuongGiuong();
                                                g1.buong = Buong[j];
                                                g1.giuongTT = cc[x];
                                                g1.giuongKH = kh1;
                                                g1.makp = MaKP;
                                                _da.Add(g1);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            _da = _da.Where(p => p.giuongTT != "" ).ToList();
            _da = _da.Where(p => p.buong != "").ToList();
            return _da;
        }
        private void frm_NhapBuongGiuongKeKoach_Load(object sender, EventArgs e)
        {
            _da.Clear();
            _da1.Clear();
            _da = getBuongGiuong(data, _makp, cbbNam.Text);
            _da1 = _da;
            var kp1 = data.KPhongs.Where(p => p.MaKP == _makp).ToList();
            KPhong1.Text = kp1.FirstOrDefault().TenKP;
            if (_da != null)
                celGiuong.Text = kh1;
            else celGiuong.Text = null;

            var b2 = (from a in _da group a by new { a.buong } into kq select new { kq.Key.buong }).ToList();
            List<dsBuongGiuong> _buong1 = (from a in b2 select new dsBuongGiuong { buong = a.buong }).ToList();
            bindingSource1.DataSource = _buong1.ToList();
            grb.DataSource = bindingSource1;
            if (_buong1.Count == 0)
            {
                grb.Enabled = true;
                grG.Enabled = true;
                celGiuong.Enabled = true;
            }
            else
            {
                grb.Enabled = false;
                grG.Enabled = false;
                celGiuong.Enabled = false;
            }

        }
        private bool kt()
        {
            if (cbbNam.Text == "")
            {
                MessageBox.Show("Cần điền năm");
                cbbNam.Focus();
                return false;
            }
            if (celGiuong.Text == "")
            {
                MessageBox.Show("Cần số giường kế hoạch");
                celGiuong.Focus();
                return false;
            }
            int j = 0;
            string[] dem = new string[2000];
            foreach (var a1 in _da1)
            {
                int i = 0;
                foreach (var b1 in _da)
                {
                    if (a1.buong == b1.buong && a1.giuongTT == b1.giuongTT)
                        i++;
                    if (b1.giuongTT == "" || b1.buong == "")
                        i--;
                }
                dem[j] = Convert.ToString(i);
                j++;
            }
            for (int i = 0; i < j; i++)
            {
                if (Convert.ToInt32(dem[i]) > 1)
                {
                    MessageBox.Show("Tên giường không giống nhau", "Thông báo!");
                    return false;
                }

            }
            var ax = (from a in _da
                      join b in _buong on a.buong equals b.buong1
                      select new { a }).ToList();
            if(ax.Count() == 0 && _buong.Where(p => p.buong1 != "").Count() > 0)
            {
                MessageBox.Show("Có buồng chưa có có giường", "Thông báo!");
                return false;
            }
            return true;
        }
        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (kt())
            {
                var kp1 = data.KPhongs.Where(p => p.MaKP == _makp && p.BuongGiuong != null && p.BuongGiuong.Contains(cbbNam.Text) && p.SoGiuongKH != null && p.SoGiuongKH.Contains(cbbNam.Text)).Select(p => new { p.BuongGiuong, p.SoGiuongKH }).FirstOrDefault();
                string c1 = null, c2 = null;
                if (kp1 != null)
                {
                    string[] Nam = kp1.BuongGiuong.Split('|');
                    string[] kh = kp1.SoGiuongKH.Split('|');
                    foreach (var item4 in kh)
                    {
                        if (item4.Contains(cbbNam.Text) || item4 == "")
                        {
                        }
                        else
                            c2 = c2 + item4 + "|";
                    }

                    foreach (var item in Nam)
                    {
                        if (item.Contains(cbbNam.Text) || item == "")
                        {
                        }
                        else
                            c1 = c1 + item + "|";
                    }
                    KPhong d = data.KPhongs.Single(p => p.MaKP == _makp);
                    d.SoGiuongKH = c2;
                }
                else
                {
                    var kp11 = data.KPhongs.Where(p => p.MaKP == _makp).Select(p => new { p.BuongGiuong, p.SoGiuongKH }).FirstOrDefault();
                    c1 = kp11.BuongGiuong + "|";
                    c2 = kp11.SoGiuongKH + "|";
                    KPhong d = data.KPhongs.Single(p => p.MaKP == _makp);
                    d.SoGiuongKH = c2;
                }
                var kp = data.KPhongs.Where(p => p.MaKP == _makp).ToList();
                KPhong ds = data.KPhongs.Single(p => p.MaKP == _makp);
                ds.SoGiuongKH = kp.First().SoGiuongKH + cbbNam.Text + ":" + celGiuong.Text + "|";
                string buonga = "";
                int ii = 0;
                _da = _da.Where(p => p.giuongTT != "").ToList();
                _da = _da.Where(p => p.buong != "").ToList();
                foreach (var bb in (from a in _da group a by new { a.buong } into kq select new { kq.Key.buong }).ToList())
                {
                    buonga = buonga + bb.buong + "{";
                    foreach (var aa in _da.Where(p => p.buong == bb.buong).ToList())
                    {
                        int cout = _da.Where(p => p.buong == bb.buong).Count();
                        if (bb.buong == aa.buong)
                        {
                            if (ii < cout - 1)
                            {
                                buonga = buonga + aa.giuongTT + ",";
                                ii++;
                            }
                            else
                            {
                                buonga = buonga + aa.giuongTT + "};";
                                ii = 0;
                            }

                        }
                    }
                }
                if(buonga == "" )
                {
                    buonga = c1;
                }
                else
                buonga = c1 + cbbNam.Text + ":" + buonga;
                ds.BuongGiuong = buonga.Substring(0, buonga.Length - 1) + "|";
                if (data.SaveChanges() >= 0)
                {
                    MessageBox.Show("Lưu thành công");
                    frm_NhapBuongGiuongKeKoach_Load(sender, e);
                }
            }
        }

        private void grbuong_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            GridView view = sender as GridView;
            buongg ds = new buongg();
            ds.buong1 = grbuong.GetRowCellValue(e.RowHandle, buong).ToString();
            if(e.RowHandle < 0)
            _buong.Add(ds);
        }

        private void cbbNam_SelectedIndexChanged(object sender, EventArgs e)
        {
            frm_NhapBuongGiuongKeKoach_Load(sender, e);
        }
        private void grGiuong_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            GridView view = sender as GridView;
            dsBuongGiuong ds = new dsBuongGiuong();
            ds.giuongTT = grGiuong.GetRowCellValue(e.RowHandle, giuong).ToString();
            ds.buong = grbuong.GetFocusedRowCellValue(buong).ToString();
            if (_da.Where(p => p.buong == grbuong.GetFocusedRowCellValue(buong).ToString() && p.giuongTT == grGiuong.GetRowCellValue(e.RowHandle, giuong).ToString() && (p.buong != "" && p.giuongTT != "")).Count() > 1)
            {
                MessageBox.Show("Tên giường không giống nhau", "Thông báo!");
            }
            else if (e.RowHandle < 0 && _da.Where(p => p.buong == grbuong.GetFocusedRowCellValue(buong).ToString() && p.giuongTT == grGiuong.GetRowCellValue(e.RowHandle, giuong).ToString()).Count() == 0)
                _da.Add(ds);
            else if (e.RowHandle < 0 && _da.Where(p => p.buong == grbuong.GetFocusedRowCellValue(buong).ToString() && p.giuongTT == grGiuong.GetRowCellValue(e.RowHandle, giuong).ToString()).Count() > 0)
                MessageBox.Show("Tên giường không giống nhau", "Thông báo!");
        }

        private void grbuong_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            string b3 = "";
            if (grbuong.GetFocusedRowCellValue(buong) != null)
            {
                b3 = grbuong.GetFocusedRowCellValue(buong).ToString();
            }
            bindingSource2.DataSource = _da.Where(p => b3 == "" ? false : p.buong == b3).ToList();
            grG.DataSource = bindingSource2;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            grb.Enabled = true;
            grG.Enabled = true;
            celGiuong.Enabled = true;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if(namtu.Text != "" && dennam.Text != "")
            {
                if (namtu.Text != dennam.Text)
                {
                    var kp1 = data.KPhongs.Where(p => p.MaKP == _makp && p.BuongGiuong != null && p.BuongGiuong.Contains(dennam.Text) && p.SoGiuongKH != null && p.SoGiuongKH.Contains(dennam.Text)).Select(p => new { p.BuongGiuong, p.SoGiuongKH }).FirstOrDefault();
                    _da2.Clear();
                    _da3.Clear();
                    _da2 = getBuongGiuong(data, _makp, namtu.Text);
                    if(_da2.Count() > 0)
                    {
                        _da3 = _da2;
                        string c1 = null, c2 = null;
                        if (kp1 != null)
                        {
                            string[] Nam = kp1.BuongGiuong.Split('|');
                            string[] kh = kp1.SoGiuongKH.Split('|');
                            foreach (var item4 in kh)
                            {
                                if (item4.Contains(dennam.Text) || item4 == "")
                                {
                                }
                                else
                                    c2 = c2 + item4 + "|";
                            }

                            foreach (var item in Nam)
                            {
                                if (item.Contains(dennam.Text) || item == "")
                                {
                                }
                                else
                                    c1 = c1 + item + "|";
                            }
                            KPhong d = data.KPhongs.Single(p => p.MaKP == _makp);
                            d.SoGiuongKH = c2;
                        }
                        else
                        {
                            var kp11 = data.KPhongs.Where(p => p.MaKP == _makp).Select(p => new { p.BuongGiuong, p.SoGiuongKH }).FirstOrDefault();
                            c1 = kp11.BuongGiuong + "|";
                            c2 = kp11.SoGiuongKH + "|";
                            KPhong d = data.KPhongs.Single(p => p.MaKP == _makp);
                            d.SoGiuongKH = c2;
                        }
                        var kp = data.KPhongs.Where(p => p.MaKP == _makp).ToList();
                        KPhong ds = data.KPhongs.Single(p => p.MaKP == _makp);
                        ds.SoGiuongKH = kp.First().SoGiuongKH + dennam.Text + ":" + _da3.First().giuongKH + "|";
                        string buonga = "";
                        int ii = 0;
                        _da3 = _da3.Where(p => p.giuongTT != "").ToList();
                        _da3 = _da3.Where(p => p.buong != "").ToList();
                        foreach (var bb in (from a in _da group a by new { a.buong } into kq select new { kq.Key.buong }).ToList())
                        {
                            buonga = buonga + bb.buong + "{";
                            foreach (var aa in _da3.Where(p => p.buong == bb.buong).ToList())
                            {
                                int cout = _da3.Where(p => p.buong == bb.buong).Count();
                                if (bb.buong == aa.buong)
                                {
                                    if (ii < cout - 1)
                                    {
                                        buonga = buonga + aa.giuongTT + ",";
                                        ii++;
                                    }
                                    else
                                    {
                                        buonga = buonga + aa.giuongTT + "};";
                                        ii = 0;
                                    }

                                }
                            }
                        }
                        buonga = c1 + dennam.Text + ":" + buonga;
                        ds.BuongGiuong = buonga.Substring(0, buonga.Length - 1) + "|";
                        if (data.SaveChanges() >= 0)
                        {
                            MessageBox.Show("UpDate thành công");
                            frm_NhapBuongGiuongKeKoach_Load(sender, e);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Năm cũ không có dữ liệu!");
                    }
                }
                else
                {
                    MessageBox.Show("Không thể Update cùng năm!");
                }
            }
            else
            {
                var kp2 = data.KPhongs.Where(p => p.PLoai == "Lâm sàng" && (p.BuongGiuong != "" && p.BuongGiuong != null)).ToList();
                foreach (var item1 in kp2)
                {
                    var kt = data.KPhongs.Where(p => p.PLoai == "Lâm sàng" && (p.BuongGiuong != "" && p.BuongGiuong != null) && p.MaKP == item1.MaKP).Single();
                    string buongs = kt.BuongGiuong, giuongs = Convert.ToString(kt.PPXuat);
                    if (!kt.BuongGiuong.Contains(":"))
                    {
                        KPhong d = data.KPhongs.Where(p => p.PLoai == "Lâm sàng" && (p.BuongGiuong != "" && p.BuongGiuong != null) && p.MaKP == item1.MaKP).Single();
                        d.BuongGiuong = System.DateTime.Now.Year.ToString() + ":" + buongs;
                        d.SoGiuongKH = System.DateTime.Now.Year.ToString() + ":" + giuongs;
                        if (data.SaveChanges() >= 0)
                        {
                        }
                    }
                }
                MessageBox.Show("UpDate thành công");
            }
        }
        string buongcu = "";
        private void grbuong_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(buongcu))
            {
                foreach (var item in _da)
                {
                    if (buongcu == item.buong)
                    {
                        item.buong = e.Value.ToString();
                    }
                }
                foreach(var item in _buong)
                {
                    if(buongcu == item.buong1)
                    {
                        item.buong1 = e.Value.ToString();
                    }
                }
            }
        }

        private void grbuong_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                buongcu = "";
                buongcu = grbuong.GetFocusedRowCellValue("buong").ToString();
            }
        }
    }
}