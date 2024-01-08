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
    public partial class Frm_BCSKSCanbo : DevExpress.XtraEditors.XtraForm
    {
        public Frm_BCSKSCanbo()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities _Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private class BC
        {
            private string  hoten, capbac, chucvu, songayom, songaynamvien, chandoan, mabenh, chuyenvien, ghichu;
            private int tuoi;
            private int mbn;
            public int _MaBN
            {
                set { mbn = value; }
                get { return mbn; }
            }
            public string _Hoten
            {
                set { hoten = value; }
                get { return hoten; }
            }
            public string _Capbac
            {
                set { capbac = value; }
                get { return capbac; }
            }
            public string _Chucvu
            {
                set { chucvu = value; }
                get { return chucvu; }
            }
            public string _Songayom
            {
                set { songayom = value; }
                get { return songayom; }
            }
            public string _Songaynamvien
            {
                set { songaynamvien = value; }
                get { return songaynamvien; }
            }
            public string _Chandoan
            {
                set { chandoan = value; }
                get { return chandoan; }
            }
            public string _Mabenh
            {
                set { mabenh = value; }
                get { return mabenh; }
            }
            public string _Chuyenvien
            {
                set { chuyenvien = value; }
                get { return chuyenvien; }
            }
            public string _Ghichu
            {
                set { ghichu = value; }
                get { return ghichu; }
            }
            public int _Tuoi
            {
                set { tuoi = value; }
                get { return tuoi; }
            }
        }
        private class CapBac
        {
            private string capbac;
            private bool chon;
            private int id;
            public int _ID
            {
                set { id = value; }
                get { return id; }
            }
            public string _CapBac
            {
                set { capbac = value; }
                get { return capbac; }
            }
            public bool _Chon
            {
                set { chon = value; }
                get { return chon; }
            }
        }
        List<BC> _BC = new List<BC>();
        List<CapBac> _CapBac = new List<CapBac>();
        private void Frm_BCSKSCanbo_Load(object sender, EventArgs e)
        {
            Ngayden.DateTime = System.DateTime.Now;
            Ngaytu.DateTime = System.DateTime.Now;
            var q3 = (from cb in _Data.CapBacs select cb).ToList();
            if (q3.Count > 0)
            {
                CapBac themmoi1 = new CapBac();
                themmoi1._CapBac = "Tất cả";
                themmoi1._Chon = false;
                themmoi1._ID = -1;
                _CapBac.Add(themmoi1);
                foreach (var a in q3)
                {
                    CapBac themmoi = new CapBac();
                    themmoi._CapBac = a.Ten_CB;
                    themmoi._ID = a.ID_CB;
                    themmoi._Chon = false;
                    _CapBac.Add(themmoi);
                }
            }
            grcCapBac.DataSource = _CapBac;
            var q4 = (from dt in _Data.DTBNs select dt).ToList();
            if (q4.Count > 0)
            {
                lupDTuong.Properties.DataSource = q4.OrderBy(p => p.IDDTBN);
            }
        }

        private void sbtTaoBC_Click(object sender, EventArgs e)
        {
            DateTime NgayT = DungChung.Ham.NgayTu(Ngaytu.DateTime);
            DateTime NgayD = DungChung.Ham.NgayDen(Ngayden.DateTime);
            _BC.Clear();
            var q5 = (from bn in _Data.BenhNhans
                     join ttbs in _Data.TTboXungs on bn.MaBNhan equals ttbs.MaBNhan
                     join cv in _Data.ChucVus on ttbs.ID_CV equals cv.ID_CV
                     where (bn.NNhap >= NgayT && bn.NNhap <= NgayD)
                     select new
                     {
                         bn.MaBNhan,
                         bn.TenBNhan,
                         bn.Tuoi,
                         ttbs.ID_CB,
                         cv.Ten_CV,
                         bn.IDDTBN,
                     }).ToList();
            if (lupDTuong.EditValue != null)
            {
                int IDDT=Convert.ToInt32(lupDTuong.EditValue);
                var q6 = (q5.Where(p => p.IDDTBN == IDDT)).ToList();
                var q = (from ds in q6
                            join kp in _CapBac.Where(p=>p._Chon==true) on ds.ID_CB equals kp._ID
                            group new { ds, kp } by new { ds.ID_CB, ds.MaBNhan, ds.Ten_CV, ds.TenBNhan, ds.Tuoi, kp._CapBac } into kq
                            select new
                            {
                                kq.Key._CapBac,
                                kq.Key.MaBNhan,
                                kq.Key.Ten_CV,
                                kq.Key.TenBNhan,
                                kq.Key.Tuoi
                            }).ToList();

                if (q.Count > 0)
                {
                    foreach (var a in q)
                    {
                        BC themmoi = new BC();
                        themmoi._Capbac = a._CapBac;
                        themmoi._Chucvu = a.Ten_CV;
                        themmoi._Hoten = a.TenBNhan;
                        themmoi._MaBN = a.MaBNhan;
                        themmoi._Tuoi = a.Tuoi.Value;
                        _BC.Add(themmoi);
                    }
                    foreach (var b in _BC)
                    {
                        int _mbn = b._MaBN;
                        var q1 = (from kb in _Data.BNKBs.Where(p => p.MaBNhan == _mbn)
                                  select kb).ToList();
                        if (q1.Count > 0)
                        {
                            b._Chandoan = q1.Last().ChanDoan;
                            if (q1.Last().NgayNghi != null)
                            {
                                b._Songayom = q1.Last().NgayNghi.Value.ToString();
                            }
                            if (q1.Last().PhuongAn == 2)
                            {
                                b._Chuyenvien = "X";
                            }
                            else
                            {
                                b._Chuyenvien = "";
                            }
                            b._Mabenh = q1.Last().MaICD;
                        }
                        var q2 = (from rv in _Data.RaViens.Where(p => p.MaBNhan == _mbn)
                                  select rv).ToList();
                        if (q2.Count > 0)
                        {
                            var q3 = (_Data.BenhNhans.Where(p => p.MaBNhan == _mbn)).ToList();
                            if (q3.First().NoiTru == 0)
                            {
                                b._Songaynamvien = "";
                            }
                            else
                            {
                                b._Songaynamvien = q2.Sum(p => p.SoNgaydt).Value.ToString();
                            }
                        }
                    }

                    BaoCao.Rep_BCSKCanbo rep = new BaoCao.Rep_BCSKCanbo();
                    frmIn frm = new frmIn();
                    rep.ngaythang.Value = DungChung.Bien.TenCQ + ", " + DungChung.Ham.NgaySangChu(System.DateTime.Now);
                    rep.tungay.Value = "Từ ngày: " + Ngaytu.Text + " đến ngày: " + Ngayden.Text;
                    rep.DataSource = _BC;
                    rep.BindingData();
                    rep.CreateDocument();
                    //rep.DataMember = "Table";
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                }
            }
        }

        private void sbtHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void grvCapBac_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "colChon")
            {

                if (grvCapBac.GetFocusedRowCellValue("_CapBac") != null)
                {
                    string Ten = grvCapBac.GetFocusedRowCellValue("_CapBac").ToString();
                    if (Ten == "Tất cả")
                    {
                        if (_CapBac.First()._Chon == true)
                        {
                            foreach (var a in _CapBac)
                            {
                                a._Chon = false;
                            }
                        }
                        else
                        {
                            foreach (var a in _CapBac)
                            {
                                a._Chon = true;
                            }
                        }
                        grcCapBac.DataSource = "";
                        grcCapBac.DataSource = _CapBac.ToList();
                    }
                }
            }
        }
    }
}