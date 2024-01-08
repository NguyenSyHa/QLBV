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
    public partial class Frm_HuyCLS : DevExpress.XtraEditors.XtraForm
    {
        int _idCLS = 0;
        bool loaiCLSct;
        public Frm_HuyCLS()
        {
            InitializeComponent();
        }
        public Frm_HuyCLS(int id, bool CLSct)
        {
            InitializeComponent();
            _idCLS = id;
            loaiCLSct = CLSct;
        }
        public class DVCLS
        {
            private string tendv, lydo;

            private int madv;
            private bool huy;
            private int idcd;
            public int IDCD
            { get { return idcd; } set { idcd = value; } }
            public int MaDV
            { set { madv = value; } get { return madv; } }
            string maDVct;

            public string MaDVct
            {
                get { return maDVct; }
                set { maDVct = value; }
            }
            public string TenDV
            { set { tendv = value; } get { return tendv; } }
            public string LyDo
            { set { lydo = value; } get { return lydo; } }
            public bool Huy
            { set { huy = value; } get { return huy; } }
        }
        List<DVCLS> _DV = new List<DVCLS>();
        List<DVCLS> _DVct = new List<DVCLS>();
        QLBV_Database.QLBVEntities _Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void sbtThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #region loadDichVu
        void loadDichVu()
        {
            _DV.Clear();
            var a = (from cl in _Data.CLS.Where(p => p.IdCLS == _idCLS)
                     join cd in _Data.ChiDinhs.Where(p => p.Status == 0 || p.Status == -1 || p.Status == null) on cl.IdCLS equals cd.IdCLS
                     join dv in _Data.DichVus on cd.MaDV equals dv.MaDV
                     group new { cd, dv } by new { cd.MaDV, cd.KetLuan, cd.Status, dv.TenDV, cd.IDCD } into kq
                     select new { kq.Key.KetLuan, kq.Key.Status, kq.Key.TenDV, kq.Key.MaDV, kq.Key.IDCD }).ToList();
            if (a.Count > 0)
            {
                foreach (var c in a)
                {
                    DVCLS themmoi = new DVCLS();
                    themmoi.LyDo = c.KetLuan;
                    themmoi.TenDV = c.TenDV;
                    themmoi.MaDV = c.MaDV == null ? 0 : c.MaDV.Value;
                    themmoi.IDCD = c.IDCD;
                    if (c.Status == -1)
                    { themmoi.Huy = true; }
                    else
                    {
                        themmoi.Huy = false;
                    }
                    _DV.Add(themmoi);
                }
            }
            grcCLS.DataSource = _DV.OrderBy(p => p.TenDV);

        }
        #endregion
        #region loadDVct
        void loadDVct()
        {
            _DVct.Clear();
            var a = (from cl in _Data.CLS.Where(p => p.IdCLS == _idCLS)
                     join cd in _Data.ChiDinhs.Where(p => p.Status == 0 || p.Status == -1 || p.Status == null) on cl.IdCLS equals cd.IdCLS
                     join clct in _Data.CLScts.Where(p => p.Status == 0 || p.Status == -1 || p.Status == null) on cd.IDCD equals clct.IDCD
                     join dv in _Data.DichVucts on clct.MaDVct equals dv.MaDVct
                     select new { MaDVct = clct.MaDVct, KetLuan = clct.KetQua, clct.Status, TenDV = dv.TenDVct, IDCD = clct.Id }).ToList();
            if (a.Count > 0)
            {
                foreach (var c in a)
                {
                    DVCLS themmoi = new DVCLS();
                    themmoi.LyDo = c.KetLuan;
                    themmoi.TenDV = c.TenDV;
                    themmoi.MaDVct = c.MaDVct == null ? "" : c.MaDVct.ToString();
                    themmoi.IDCD = c.IDCD;
                    if (c.Status == -1)
                    { themmoi.Huy = true; }
                    else
                    {
                        themmoi.Huy = false;
                    }
                    _DVct.Add(themmoi);
                }
            }
            grcChiTiet.DataSource = _DVct.OrderBy(p => p.TenDV);
        }
        #endregion
        private void Frm_HuyCLS_Load(object sender, EventArgs e)
        {
            if (loaiCLSct)
            {
                loadDVct();
                xtraTabControl1.SelectedTabPageIndex = 1;
                ((Control)this.xtraDichVu).Enabled = false;
            }
            else
            {
                loadDichVu();
                xtraTabControl1.SelectedTabPageIndex = 0;
                ((Control)this.xtraDichVuct).Enabled = false;
            }

        }

        private void SbtLuu_Click(object sender, EventArgs e)
        {
            if (_DV.Count > 0)
            {
                foreach (var q in _DV)
                {
                    if (q.Huy == true)
                    {
                        var sua = _Data.ChiDinhs.Single(p => p.IDCD == q.IDCD);
                        sua.Status = -1;
                        sua.KetLuan = q.LyDo;
                        _Data.SaveChanges();

                    }
                    else
                    {
                        var sua = _Data.ChiDinhs.Single(p => p.IDCD == q.IDCD);
                        sua.Status = 0;
                        sua.KetLuan = "";
                        _Data.SaveChanges();
                    }

                }
                _Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                var KTcls = _Data.ChiDinhs.Where(p => p.IdCLS == _idCLS).ToList();
                int huy = 0, tong = 0;
                tong = KTcls.Count;
                huy = KTcls.Where(p => p.Status == -1).ToList().Count;
                if (tong == huy)
                {
                    var cls = _Data.CLS.Single(p => p.IdCLS == _idCLS);
                    cls.Status = 1;
                    _Data.SaveChanges();
                }
                MessageBox.Show("Thực hiện thành công!");
                this.Dispose();

            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (_DVct.Count > 0)
            {
                if (_DVct.Where(p => p.Huy == false).Count() > 0)
                {
                    foreach (var q in _DVct)
                    {
                        var sua = _Data.CLScts.Single(p => p.Id == q.IDCD);
                        if (q.Huy)
                        {

                            sua.Status = -1;
                            sua.KetQua = q.LyDo;
                            _Data.SaveChanges();
                        }
                        else
                        {
                            sua.Status = 0;
                            sua.KetQua = "";
                            _Data.SaveChanges();
                        }

                    }
                    //_Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                    //var KTclsct = _Data.CLScts.Where(p => p.IdCLS == _idCLS).ToList();
                    //var KTcls = _Data.ChiDinhs.Where(p => p.IdCLS == _idCLS).ToList();
                    //int huy = 0, tong = 0;
                    //tong = KTcls.Count;
                    //huy = KTcls.Where(p => p.Status == -1).ToList().Count;
                    //if (tong == huy)
                    //{
                    //    var cls = _Data.CLS.Single(p => p.IdCLS == _idCLS);
                    //    cls.Status = 1;
                    //    _Data.SaveChanges();
                    //}
                    MessageBox.Show("Thực hiện thành công!");
                    this.Dispose();
                }
                else
                    MessageBox.Show("Bạn không thể hủy tất cả các thông số");
            }
        }







        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}