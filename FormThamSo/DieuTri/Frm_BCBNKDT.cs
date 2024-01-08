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
    public partial class Frm_BCBNKDT : DevExpress.XtraEditors.XtraForm
    {
        public Frm_BCBNKDT()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities _Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private class BC
        {
            private string tbn;
            private string sothe;
            private int tuoi;
            private int gtinh;
            private string chandoan;
            private string dchi;
            private DateTime ngayvao;
            private string ngayra;
            private int mbn;
            private string ngaydt;
            public string NgayDT
            { set { ngaydt = value; } get { return ngaydt; } }
            public int MaBN
            { set { mbn = value; } get { return mbn; } }
            public string TenBN
            { set { tbn = value; } get { return tbn; } }
            public string Sothe
            { set { sothe = value; } get { return sothe; } }
            public int Tuoi
            { set { tuoi = value; } get { return tuoi; } }
            public int Gtinh
            { set { gtinh = value; } get { return gtinh; } }
            public string ChanDoan
            { set { chandoan = value; } get { return chandoan; } }
            public string DChi
            { set { dchi = value; } get { return dchi; } }
            public DateTime NgayVao
            { set { ngayvao = value; } get { return ngayvao; } }
            public string NgayRa
            { set { ngayra = value; } get { return ngayra; } }
             
        }
        List<BC> _BC = new List<BC>();
        private void Frm_BCBNKDT_Load(object sender, EventArgs e)
        {
            Lupngaytu.DateTime = System.DateTime.Now;
            Lupngayden.DateTime = System.DateTime.Now;
            List<KPhong> a = (from kp in _Data.KPhongs.Where(p => p.PLoai.Contains("Lâm sàng")) select kp).ToList();
            a.Add(new KPhong { TenKP = " Tất cả", MaKP = 0 });
            if (a.Count > 0)
            {
                LupKP.Properties.DataSource = a.OrderBy(p=>p.TenKP);
            }
            if(DungChung.Bien.MaBV == "27021")
            rdNgayTK.SelectedIndex = 2;
            radRV.SelectedIndex = 1;
            radRV.Enabled = false;
        }

        private void sbtTaoBC_Click(object sender, EventArgs e)
        {
            int _Makp = 0;
            DateTime _Ngaytu = DungChung.Ham.NgayTu(Lupngaytu.DateTime);
            DateTime _Ngaden = DungChung.Ham.NgayDen(Lupngayden.DateTime);
            int N1 = -1;
            int N2 = -1;
            string BH = "";
            string DV = "";
            if (ckNoitru.Checked == true)
            { N1 = 1; }
            if (ckNgoaitru.Checked == true)
            { N2 = 0; }
            if (BH1.Checked == true)
            { BH = "BHYT"; }
            if (DV1.Checked == true)
            { DV = "Dịch vụ"; }
            if (!string.IsNullOrEmpty(LupKP.Text))
            { _Makp = LupKP.EditValue == null ? 0 :Convert.ToInt32( LupKP.EditValue); }
            if (true)
            {
                var bnkb=(from bk in _Data.BNKBs.Where(p=>_Makp==0?true: p.MaKPdt==_Makp)
                          join vv in _Data.VaoViens on bk.MaBNhan equals vv.MaBNhan                         
                          join rv in _Data.RaViens on vv.MaBNhan equals rv.MaBNhan into rv1
                          from a in rv1.DefaultIfEmpty()
                          where (rdNgayTK.SelectedIndex == 0 ? (a != null && a.NgayRa >= _Ngaytu && a.NgayRa <= _Ngaden) :(rdNgayTK.SelectedIndex == 1 ? ( vv.NgayVao <= _Ngaden && vv.NgayVao >= _Ngaytu) : true))
                          group bk by new{bk.MaBNhan} into kq select new {kq.Key.MaBNhan,IDKB=kq.Select(p=>p.IDKB).Max()} ).ToList();

                var c2 = (from bn in _Data.BenhNhans.Where(p => (p.NoiTru == N1 || p.NoiTru == N2) && (p.DTuong == BH || p.DTuong == DV))
                         join bk in _Data.BNKBs.Where(p=>_Makp==0?true: p.MaKPdt==_Makp) on bn.MaBNhan equals bk.MaBNhan
                         join vv in _Data.VaoViens on bn.MaBNhan equals vv.MaBNhan   
                         join rv in _Data.RaViens on bn.MaBNhan equals rv.MaBNhan into rv1 from a in rv1.DefaultIfEmpty()
                          where (rdNgayTK.SelectedIndex == 0 ? (a != null && a.NgayRa >= _Ngaytu && a.NgayRa <= _Ngaden) : (rdNgayTK.SelectedIndex == 1 ? (vv.NgayVao <= _Ngaden && vv.NgayVao >= _Ngaytu) : true))
                         select new { NgayRa = (DateTime?)a.NgayRa, bn.TenBNhan, bk.IDKB, bn.MaBNhan, bn.NNhap,
                             bn.Tuoi, bn.GTinh, bn.DChi, bn.DTuong, bn.SThe, bk.ChanDoan, vv.NgayVao, bk.MaICD }).OrderBy(p => p.NgayVao).ToList();

                if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "56789")
                {
                    var bnkb1 = (from a in _Data.BNKBs.Where(p => p.NgayKham <= _Ngaden)
                                 group a by new { a.MaBNhan } into kq
                                 select new { kq.Key.MaBNhan, IDKB = kq.Max(p => p.IDKB) }).ToList();
                    bnkb = (from a1 in bnkb1
                            join bk in _Data.BNKBs.Where(p => _Makp == 0 ? true : p.MaKP == _Makp) on a1.IDKB equals bk.IDKB
                            join vv in _Data.VaoViens on bk.MaBNhan equals vv.MaBNhan
                            join rv in _Data.RaViens on vv.MaBNhan equals rv.MaBNhan into rv1
                            from a in rv1.DefaultIfEmpty()
                            where (rdNgayTK.SelectedIndex == 0 ? (a != null && a.NgayRa >= _Ngaytu && a.NgayRa <= _Ngaden) : (rdNgayTK.SelectedIndex == 1 ? (vv.NgayVao <= _Ngaden && vv.NgayVao >= _Ngaytu) : (vv.NgayVao <= _Ngaden)))
                            group bk by new { bk.MaBNhan, a1.IDKB } into kq
                            select new { kq.Key.MaBNhan, kq.Key.IDKB }).ToList();

                   var c1 = (from bn in _Data.BenhNhans.Where(p => (p.NoiTru == N1 || p.NoiTru == N2) && (p.DTuong == BH || p.DTuong == DV))
                          join bk in _Data.BNKBs.Where(p => _Makp == 0 ? true : p.MaKP == _Makp) on bn.MaBNhan equals bk.MaBNhan
                          join vv in _Data.VaoViens on bn.MaBNhan equals vv.MaBNhan
                          join rv in _Data.RaViens on bn.MaBNhan equals rv.MaBNhan into rv1
                          from a in rv1.DefaultIfEmpty()
                             where (rdNgayTK.SelectedIndex == 0 ? (a != null && a.NgayRa >= _Ngaytu && a.NgayRa <= _Ngaden) : (rdNgayTK.SelectedIndex == 1 ? (vv.NgayVao <= _Ngaden && vv.NgayVao >= _Ngaytu) : (vv.NgayVao <= _Ngaden)))
                          select new
                          {
                              NgayRa = (DateTime?)a.NgayRa,
                              bn.TenBNhan,
                              bk.IDKB,
                              bn.MaBNhan,
                              bn.NNhap,
                              bn.Tuoi,
                              bn.GTinh,
                              bn.DChi,
                              bn.DTuong,
                              bn.SThe,
                              bk.ChanDoan,
                              vv.NgayVao,
                              bk.MaICD
                          }).OrderBy(p => p.NgayVao).ToList();

                   c2 = (from a in c1
                         join b in bnkb1 on a.IDKB equals b.IDKB
                         select new
                         {
                             a.NgayRa,
                             a.TenBNhan,
                             a.IDKB,
                             a.MaBNhan,
                             a.NNhap,
                             a.Tuoi,
                             a.GTinh,
                             a.DChi,
                             a.DTuong,
                             a.SThe,
                             a.ChanDoan,
                             a.NgayVao,
                             a.MaICD
                         }).ToList();
                }

                var c = (from a in c2.Where(p => radRV.SelectedIndex == 2 ? (p.NgayRa == null || (p.NgayRa >= _Ngaytu && p.NgayRa <= _Ngaden)) : (radRV.SelectedIndex == 1 ? (p.NgayRa >= _Ngaytu && p.NgayRa <= _Ngaden) : (p.NgayRa == null))).OrderBy(p => p.NgayVao).ThenBy(p => p.TenBNhan) 
                         join b in bnkb on a.IDKB equals b.IDKB 
                         select new { a.NgayRa, a.TenBNhan, a.MaBNhan, a.NNhap, a.Tuoi, a.GTinh, a.DChi, a.DTuong, a.SThe, a.ChanDoan, a.NgayVao }).ToList();
                _BC.Clear();
                foreach (var a in c)
                {
                    BC themmoi = new BC();
                        themmoi.ChanDoan = a.ChanDoan;
                    themmoi.DChi = a.DChi;
                    themmoi.Gtinh = a.GTinh.Value;
                    themmoi.MaBN = a.MaBNhan;
                    themmoi.NgayRa = a.NgayRa==null?"": a.NgayRa.Value.ToShortDateString();
                    themmoi.NgayVao = a.NgayVao == null ? DateTime.Now : a.NgayVao.Value;
                    themmoi.Sothe = a.SThe;
                    themmoi.TenBN = a.TenBNhan;
                    themmoi.Tuoi = a.Tuoi.Value;
                    _BC.Add(themmoi);
                }
                BaoCao.Rep_BCBNKDT rep = new BaoCao.Rep_BCBNKDT();
                frmIn frm = new frmIn();
                rep.CQCQ.Value = DungChung.Bien.TenCQCQ;
                rep.BV.Value = DungChung.Bien.TenCQ;
                rep.Ngaythang.Value = "Từ ngày " + Lupngaytu.DateTime.ToString().Substring(0, 10) + " đến ngày " + Lupngayden.DateTime.ToString().Substring(0, 10);
                rep.KP.Value = LupKP.Text;
                //rep.HT.Value = "Tất cả bệnh nhân chưa thanh toán và đã thanh toán";
                rep.DataSource = _BC;
                rep.BindingData();
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
                      
            }
        }

        private void sbtHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rdNgayTK_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(rdNgayTK.SelectedIndex == 0)
            {
                radRV.SelectedIndex = 1;
                radRV.Enabled = false;
            }
            else
            {
                radRV.Enabled = true;
                radRV.SelectedIndex = 0;
            }
        }
    }
}