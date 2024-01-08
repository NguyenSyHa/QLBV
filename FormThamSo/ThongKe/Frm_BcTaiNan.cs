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
    public partial class Frm_BcTaiNan : DevExpress.XtraEditors.XtraForm
    {
        public Frm_BcTaiNan()
        {
            InitializeComponent();
        }

        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private bool KTtaoBc()
        {
            if (lupTuNgay.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn ngày bắt đầu in báo cáo");
                lupTuNgay.Focus();
                return false;
            }
            if (lupDenNgay.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn đến ngày kết thúc in báo cáo");
                lupDenNgay.Focus();
                return false;
            }

            return true;
        }
        private class KPhong
        {
            private string TenKP;
            private int MaKP;
            private bool Chon;
            public string tenkp
            { set { TenKP = value; } get { return TenKP; } }
            public int makp
            { set { MaKP = value; } get { return MaKP; } }
            public bool chon
            { set { Chon = value; } get { return Chon; } }
        }
        List<KPhong> _Kphong = new List<KPhong>();
   
        private void Frm_BcTaiNan_Load(object sender, EventArgs e)
        {
            _Kphong.Clear();
            lupTuNgay.Focus();
            lupTuNgay.DateTime = System.DateTime.Now;
            lupDenNgay.DateTime = System.DateTime.Now;
            var kphong = (from kp in data.KPhongs
                          where (kp.PLoai == "Lâm sàng" || kp.PLoai == "Phòng khám")
                          select new { kp.TenKP, kp.MaKP }).ToList();
            if (kphong.Count > 0)
            {
                KPhong themmoi1 = new KPhong();
                themmoi1.tenkp = "Chọn tất cả";
                themmoi1.makp =0;
                themmoi1.chon = true;
                _Kphong.Add(themmoi1);
                foreach (var a in kphong)
                {
                    KPhong themmoi = new KPhong();
                    themmoi.tenkp = a.TenKP;
                    themmoi.makp = a.MaKP;
                    themmoi.chon = true;
                    _Kphong.Add(themmoi);
                }
                grcKhoaphong.DataSource = _Kphong.ToList();
            }
         
        }

        private void btnInBC_Click(object sender, EventArgs e)
        {
            DateTime tungay = System.DateTime.Now.Date;
            DateTime denngay = System.DateTime.Now.Date;

            if (KTtaoBc())
            {
                tungay = DungChung.Ham.NgayTu(lupTuNgay.DateTime);
                denngay = DungChung.Ham.NgayDen(lupDenNgay.DateTime);
                List<int> _lMaKP = new List<int>();


                for (int i = 0; i < _Kphong.Count; i++)
                {
                    if (_Kphong.Skip(i).First().chon == true)
                    {
                        _lMaKP.Add(_Kphong.Skip(i).First().makp);                        
                    }
                }
                List<QLBV.DungChung.Bien.c_TaiNan> _listTaiNan = DungChung.Bien.getDMTaiNan().Where(p=> p.Id != 0).ToList();

                //danh sách bệnh nhân đã ra viện              
                var q0 = (from bn in data.BenhNhans.Where(p=>p.NNhap >= tungay && p.NNhap <= denngay)
                          join rv in data.RaViens on bn.MaBNhan equals rv.MaBNhan
                          join vv in data.VaoViens on bn.MaBNhan equals vv.MaBNhan into kq
                          from kq1 in kq.DefaultIfEmpty()
                          select new { bn.MaBNhan, bn.Tuoi, bn.GTinh, bn.ChuyenKhoa, rv.KetQua, rv.MaKP, rv.Status, bnvv = kq1 == null ? 0 : kq1.MaBNhan }).ToList();
              
              
                var q = (from a in q0 join b in _lMaKP on a.MaKP equals b select new { a.MaBNhan, a.Tuoi, a.GTinh, a.ChuyenKhoa, a.KetQua, a.Status, a.bnvv}).ToList();
                foreach (QLBV.DungChung.Bien.c_TaiNan a in _listTaiNan)
                {
                    var q2 = q.Where(p => p.ChuyenKhoa == a.Tenloai).ToList();
                    a.Tenloai = a.IdParent == 1 ? ("+ " + a.Tenloai) : a.Tenloai;
                    a.Tongso = q2.Count();
                    a.Nam = q2.Where(p => p.GTinh == 1).Count();
                    a.Nu = q2.Where(p => p.GTinh == 0).Count();
                    a.T0 = q2.Where(p => p.Tuoi < 5).Count();
                    a.T5 = q2.Where(p => p.Tuoi >= 5 && p.Tuoi < 15).Count();
                    a.T15 =  q2.Where(p => p.Tuoi >= 15 && p.Tuoi < 20).Count();
                    a.T20 = q2.Where(p => p.Tuoi >= 20 && p.Tuoi < 60).Count();
                    a.T60 = q2.Where(p => p.Tuoi >= 60).Count();
                    a.Vaovien = q2.Where(p=>p.bnvv != 0).Count();
                    a.Chuyenvien = q2.Where(p=>p.Status==1).Count();
                    a.Tuvong = q2.Where(p =>p.KetQua == "Tử vong").Count();                              
                }
                List<int> _idParent = _listTaiNan.Select(p=>p.IdParent).ToList();
                foreach (QLBV.DungChung.Bien.c_TaiNan a in _listTaiNan)
                {
                    List<QLBV.DungChung.Bien.c_TaiNan> _l2 = _listTaiNan.Where(p => p.IdParent == a.Id).ToList();
                    if (_idParent.Contains(a.Id))
                    {
                        a.Tongso = _l2.Sum(p => p.Tongso);
                        a.Nam = _l2.Sum(p => p.Nam);
                        a.Nu = _l2.Sum(p => p.Nu);
                        a.T0 = _l2.Sum(p => p.T0);
                        a.T5 = _l2.Sum(p => p.T5);
                        a.T15 = _l2.Sum(p => p.T15);
                        a.T20 = _l2.Sum(p => p.T20);
                        a.T60 = _l2.Sum(p => p.T60);
                        a.Vaovien = _l2.Sum(p => p.Vaovien);
                        a.Chuyenvien = _l2.Sum(p => p.Chuyenvien);
                        a.Tuvong = _l2.Sum(p => p.Tuvong);
                    }                    
                }

                foreach (QLBV.DungChung.Bien.c_TaiNan a in _listTaiNan)
                {
                    a.Tongso = a.Tongso == 0 ? null : a.Tongso;
                    a.Nam = a.Nam == 0 ? null : a.Nam;
                    a.Nu = a.Nu == 0 ? null : a.Nu;
                    a.T0 = a.T0 == 0 ? null : a.T0;
                    a.T5 = a.T5 == 0 ? null : a.T5;
                    a.T15 = a.T15 == 0 ? null : a.T15;
                    a.T20 = a.T20 == 0 ? null : a.T20;
                    a.T60 = a.T60 == 0 ? null : a.T60;
                    a.Vaovien = a.Vaovien == 0 ? null : a.Vaovien;
                    a.Chuyenvien = a.Chuyenvien == 0 ? null : a.Chuyenvien;
                    a.Tuvong = a.Tuvong == 0 ? null : a.Tuvong;
                }
                frmIn frm = new frmIn();
                BaoCao.Rep_BcTaiNan rep = new BaoCao.Rep_BcTaiNan();
               
                    rep.NTN.Value = "Từ ngày " + tungay.ToString().Substring(0, 10) + " đến ngày " + denngay.ToString().Substring(0, 10);
               
                rep.DataSource = _listTaiNan.OrderBy(p=>p.Id);
                rep.databinding();
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void labelControl4_Click(object sender, EventArgs e)
        {

        }

        private void labelControl14_Click(object sender, EventArgs e)
        {

        }

        private void grvKhoaphong_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "Chọn")
            {
                if (grvKhoaphong.GetFocusedRowCellValue("tenkp") != null)
                {
                    string Ten = grvKhoaphong.GetFocusedRowCellValue("tenkp").ToString();

                    if (Ten == "Chọn tất cả")
                    {
                        if (_Kphong.First().chon == true)
                        {
                            foreach (var a in _Kphong)
                            {
                                a.chon = false;
                            }
                        }
                        else
                        {
                            foreach (var a in _Kphong)
                            {
                                a.chon = true;
                            }
                        }
                        grcKhoaphong.DataSource = "";
                        grcKhoaphong.DataSource = _Kphong.ToList();
                    }
                }
            }
        }
    }
}