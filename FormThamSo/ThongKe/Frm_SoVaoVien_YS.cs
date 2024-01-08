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
    public partial class Frm_SoVaoVien_YS : DevExpress.XtraEditors.XtraForm
    {
        public Frm_SoVaoVien_YS()
        {
            InitializeComponent();
        }
        private class benhnhan
        {
            private int MaBN;
            private int Phuongan;
            private int IDKB;
            private int Tuoi;
            private string Sothe;
            private string DTuong;
            private int MaKP;
            private string NTT;
            private string Diachi;
            private string CDNoiGT;
            private string NoiGT;
            private string CD;
            private string TenBS;
            private string TenBN;
            private int GT;
            private int CT;
            private string ngayvao, ngayra;
            private int songaydt;string ntuoi;
            public int ct
            { set { CT = value; } get { return CT; } }
            public int gt
            { set { GT = value; } get { return GT; } }
            public string tenbn
            { set { TenBN = value; } get { return TenBN; } }
            public string tenbs
            { set { TenBS = value; } get { return TenBS; } }
            public string cd
            { set { CD = value; } get { return CD; } }
            public string noigt
            { set { NoiGT = value; } get { return NoiGT; } }
            public string cdnoigt
            { set { CDNoiGT = value; } get { return CDNoiGT; } }
            public string diachi
            { set { Diachi = value; } get { return Diachi; } }
            public int maBN
            { get { return MaBN; } set { MaBN = value; } }
            public int phuongan
            { get { return Phuongan; } set { Phuongan = value; } }
            public int idkb
            { get { return IDKB; } set { IDKB = value; } }
            public int tuoi
            { get { return Tuoi; } set { Tuoi = value; } }
            public string sothe
            { get { return Sothe; } set { Sothe = value; } }
            public string dtuong
            { get { return DTuong; } set { DTuong = value; } }
            public int makp
            { get { return MaKP; } set { MaKP = value; } }
            public string ntt
            { get { return NTT; } set { NTT = value; } }
            public string NgayVao
            { get { return ngayvao; } set { ngayvao = value; } }
            public string NgayRa
            { get { return ngayra; } set { ngayra = value; } }
            public int SoNgayDT
            { get { return songaydt; } set { songaydt = value; } }
            public string NhomTuoi
            { get { return ntuoi; } set { ntuoi = value; } }
        }
        List<benhnhan> _benhnhan = new List<benhnhan>();

        private void Frm_Sokhambenh_Load(object sender, EventArgs e)
        {
            lupNgaytu.Focus();
            lupNgaytu.DateTime = System.DateTime.Now;
            lupNgayden.DateTime = System.DateTime.Now;
            var q = from TK in _Data.KPhongs where (TK.PLoai.Contains("Lâm sàng")) select new { TK.TenKP, TK.MaKP };
            lupKhoa.Properties.DataSource = q.ToList();
        }
        QLBV_Database.QLBVEntities _Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private bool KTtaoBc()
        {
            if (lupNgaytu.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn ngày bắt đầu in báo cáo");
                lupNgaytu.Focus();
                return false;
            }
            if (lupNgayden.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn đến ngày kết thúc in báo cáo");
                lupNgayden.Focus();
                return false;
            }
            if (lupKhoa.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn khoa");
                lupKhoa.Focus();
                return false;
            }
            return true;
        }
        List<benhnhan> _lBN = new List<benhnhan>();
            
        private void sbtTao_Click(object sender, EventArgs e)
        {
            _benhnhan.Clear();
            DateTime Ngaytu = DungChung.Ham.NgayTu(lupNgaytu.DateTime);
            DateTime Ngayden = DungChung.Ham.NgayDen(lupNgayden.DateTime);
            if (KTtaoBc())
            {
                int _makhoa = 0;
                if (lupKhoa.EditValue != null)
                    _makhoa = Convert.ToInt32(lupKhoa.EditValue);
                var idvv = (from vv in _Data.VaoViens.Where(p => p.MaKP == _makhoa).Where(p => p.NgayVao >= Ngaytu).Where(p => p.NgayVao <= Ngayden) 
                             join kb in _Data.BNKBs on vv.MaBNhan equals kb.MaBNhan
                           group kb by kb.MaBNhan into kq select new {MaBNhan= kq.Key == null? 0 : kq.Key.Value, IDKB = kq.Max(p => p.IDKB) }).ToList();
                var idrv = (from rv in _Data.RaViens.Where(p => p.MaKP == _makhoa).Where(p => p.NgayRa >= Ngaytu && p.NgayRa <= Ngayden)
                            //   where !(from tv1 in TV1 select tv1.Key).Contains(rv.MaBNhan)
                            group rv by rv.MaBNhan into kq
                            select new {MaBNhan= kq.Key, IDKB = kq.Max(p => p.IdRaVien) }).ToList();
                var qid1 = idvv.Union(idrv).Select(a => new { a.MaBNhan }).ToList();
                var qid = (from id in qid1 group id by id.MaBNhan into kq select new { kq.Key }).ToList();

                if (qid.Count > 0)
                {
                    foreach (var a in qid)
                    {
                        benhnhan themmoi = new benhnhan();
                        themmoi.maBN = a.Key;
                       // themmoi.idkb=a.IDKB;
                        _benhnhan.Add(themmoi);
                       
                    }
                }
               // var idrv = (from rv in _Data.RaViens.Where(p => p.MaKP == _makhoa).Where(p => p.NgayRa >= Ngaytu && p.NgayRa <= Ngayden)
                           //   where !(from tv1 in TV1 select tv1.Key).Contains(rv.MaBNhan)
                           //group rv by rv.MaBNhan into kq
                           //select new { kq.Key, IDKB = kq.Max(p => p.IdRaVien) }).ToList();
                //if (idrv.Count > 0 && _benhnhan.Count > 0)
                //{
                //    foreach (var a1 in _benhnhan)
                //    {
                //        foreach (var a2 in idrv)
                //        {
                //            if (a2.Key != a1.maBN)
                //            {
                //                benhnhan themmoi2 = new benhnhan();
                //                themmoi2.maBN = a2.Key;
                //                themmoi2.idkb = a2.IDKB;
                //                _lBN.Add(themmoi2);
                //            }
                //        }
                //    }

                //    _lBN.AddRange(_benhnhan);
        
                //}
                //if (idrv.Count > 0 && _benhnhan.Count == 0)
                //{

                //    foreach (var a2 in idrv)
                //        {
                //                benhnhan themmoi2 = new benhnhan();
                //                themmoi2.maBN = a2.Key;
                //                themmoi2.idkb = a2.IDKB;
                //                _benhnhan.Add(themmoi2);
                            
                //        }
                //    }
               
                //_lBN.Count();
                var qvv = (from id1 in idvv
                           join bn in _Data.BenhNhans on id1.MaBNhan equals bn.MaBNhan
                           join vv in _Data.VaoViens on bn.MaBNhan equals vv.MaBNhan
                             select new { bn.MaBNhan, bn.SThe, bn.DTuong,NhomTuoi= bn.Tuoi>15 ?"Nhóm tuổi > 15":"Nhóm tuổi <=15",bn.Tuoi, bn.DChi, bn.CDNoiGT, bn.MaBV, bn.TenBNhan, bn.GTinh,vv.ChanDoan,NgayVao=vv.NgayVao}).ToList();// NgayVao=vv.NgayVao, NgayRa=vv.NgayVao,songaydt=vv.idVaoVien}).ToList();
                var qrv1 = (from id2 in idrv
                          join kb in _Data.BNKBs on id2.MaBNhan equals kb.MaBNhan
                          group kb by kb.MaBNhan into kq select new {kq.Key,IDKB = kq.Max(p=>p.IDKB)}).ToList();

                var qrv = (from q1 in qrv1
                            join   bn in _Data.BenhNhans on q1.Key equals bn.MaBNhan
                           join rv in _Data.RaViens.Where(p => p.MaKP == _makhoa) on bn.MaBNhan equals rv.MaBNhan  //.Where(p=>p.NgayRa>=Ngaytu&&p.NgayRa<=Ngayden)
                               select new { bn.MaBNhan, bn.SThe, bn.DTuong, NhomTuoi = bn.Tuoi > 15 ? "Nhóm tuổi > 15" : "Nhóm tuổi <=15", bn.Tuoi, bn.DChi, bn.CDNoiGT, bn.MaBV, bn.TenBNhan, bn.GTinh,rv.ChanDoan,NgayRa = rv.NgayRa,songaydt = rv.SoNgaydt}).ToList();//,NgayVao=rv.NgayRa, NgayRa = rv.NgayRa, songaydt = rv.SoNgaydt }).ToList();
                //var q = (qvv.Union(qrv).Select(a=>new{}).ToList();

                    if (qvv.Count > 0)
                    {
                        foreach (var a1 in qvv)
                        {
                            foreach (var a2 in _benhnhan)
                            {
                                if (a1.MaBNhan == a2.maBN)
                                {
                                    //if (a1.IDKB == a2.idkb)
                                    //{
                                        a2.tenbn = a1.TenBNhan;
                                        a2.gt = Convert.ToInt32(a1.GTinh);
                                        if (a1.MaBV != null)
                                        {
                                            string Ma = a1.MaBV.ToString();
                                            var gt = (from bv in _Data.BenhViens.Where(p => p.MaBV == Ma) select new { bv.TenBV }).ToList();
                                            if (gt.Count > 0 && gt.First().TenBV != null)
                                            {
                                                a2.noigt = gt.First().TenBV;
                                            }
                                        }
                                        a2.cdnoigt = a1.CDNoiGT;
                                        a2.diachi = a1.DChi;
                                        a2.sothe = a1.SThe;
                                        a2.tuoi = Convert.ToInt32(a1.Tuoi);
                                        var id1 = (_Data.BenhNhans.Where(p => p.MaBNhan == a2.maBN)).ToList();
                                        if (id1.Count > 0 && id1.First().NoiTru != null)
                                        {
                                            a2.phuongan = id1.First().NoiTru.Value;
                                        }
                                        var RV = (from v in _Data.RaViens.Where(p => p.MaBNhan == a2.maBN) select new { v.Status }).ToList();
                                        if (RV.Count > 0 && RV.First().Status != null)
                                        {
                                            a2.ct = RV.First().Status.Value;
                                        }
                                        else
                                        { a2.ct = 2; }

                                        a2.cd = a1.ChanDoan;
                                        if (a2.idkb != null)
                                        {
                                            var id = (from kb in _Data.BNKBs.Where(p => p.IDKB == a2.idkb) select new { kb.MaCB }).ToList();
                                            if (id.Count > 0)
                                            {
                                                string Ma = id.First().MaCB;
                                                var cb = (from bs in _Data.CanBoes.Where(p => p.MaCB == Ma) select new { bs.TenCB }).ToList();
                                                if (cb.Count > 0 && cb.First().TenCB != null)
                                                {
                                                    a2.tenbs = cb.First().TenCB;
                                                }
                                            }
                                            a2.dtuong = a1.DTuong;
                                            if (a1.NgayVao != null)
                                            {
                                                a2.NgayVao = a1.NgayVao.ToString().Substring(0, 5);
                                            }
                                            //else { a2.NgayVao = ""; }
                                            a2.NhomTuoi = a1.NhomTuoi;
                                        }
                                    //}
                                }
                            }
                        }
                    }
                    if (qrv.Count > 0)
                    {
                        foreach (var a1 in qrv)
                        {
                            foreach (var a2 in _benhnhan)
                            {
                                if (a1.MaBNhan == a2.maBN)
                                {
                                    //if (a1.IDKB == a2.idkb)
                                    //{
                                        a2.tenbn = a1.TenBNhan;
                                        a2.gt = Convert.ToInt32(a1.GTinh);
                                        if (a1.MaBV != null)
                                        {
                                            string Ma = a1.MaBV.ToString();
                                            var gt = (from bv in _Data.BenhViens.Where(p => p.MaBV == Ma) select new { bv.TenBV }).ToList();
                                            if (gt.Count > 0 && gt.First().TenBV != null)
                                            {
                                                a2.noigt = gt.First().TenBV;
                                            }
                                        }
                                        a2.cdnoigt = a1.CDNoiGT;
                                        a2.diachi = a1.DChi;
                                        a2.sothe = a1.SThe;
                                        a2.tuoi = Convert.ToInt32(a1.Tuoi);
                                        var id1 = (_Data.BenhNhans.Where(p => p.MaBNhan == a2.maBN)).ToList();
                                        if (id1.Count > 0 && id1.First().NoiTru != null)
                                        {
                                            a2.phuongan = id1.First().NoiTru.Value;
                                        }
                                        var RV = (from v in _Data.RaViens.Where(p => p.MaBNhan == a2.maBN) select new { v.Status }).ToList();
                                        if (RV.Count > 0 && RV.First().Status != null)
                                        {
                                            a2.ct = RV.First().Status.Value;
                                        }
                                        else
                                        { a2.ct = 2; }
                                        a2.cd = a1.ChanDoan;
                                        if (a2.idkb != null)
                                        {
                                            var id = (from kb in _Data.BNKBs.Where(p => p.IDKB == a2.idkb) select new { kb.MaCB }).ToList();
                                            if (id.Count > 0)
                                            {
                                                string Ma = id.First().MaCB;
                                                var cb = (from bs in _Data.CanBoes.Where(p => p.MaCB == Ma) select new { bs.TenCB }).ToList();
                                                if (cb.Count > 0 && cb.First().TenCB != null)
                                                {
                                                    a2.tenbs = cb.First().TenCB;
                                                }
                                            }
                                            a2.dtuong = a1.DTuong;
                                            a2.NgayRa = a1.NgayRa.ToString().Substring(0, 5);
                                            a2.SoNgayDT = Convert.ToInt32(a1.songaydt);
                                            a2.NhomTuoi = a1.NhomTuoi;
                                        }
                                    }
                                //}
                            }
                        }
                    }
                   //888888888 
                    //    benhnhan themmoi = new benhnhan();
                    //    themmoi.maBN = a.Key;
                    //    themmoi.idkb = a.IDKB;
                    //    if (q.First().GTinh != null)
                    //    {
                    //        themmoi.gt = Convert.ToInt32(q.First().GTinh);
                    //    }
                    //    if (q.First().TenBNhan != null)
                    //    {
                    //        themmoi.tenbn = q.First().TenBNhan;
                    //    }
                    //    if (q.First().MaBV != null)
                    //    {
                    //        string Ma = q.First().MaBV;
                    //        var gt = (from bv in _Data.BenhViens.Where(p => p.MaBV == Ma) select new { bv.TenBV }).ToList();
                    //        if (gt.Count > 0 && gt.First().TenBV != null)
                    //        {
                    //            themmoi.noigt = gt.First().TenBV;
                    //        }
                    //    }
                    //    if (q.First().CDNoiGT != null)
                    //    { themmoi.cdnoigt = q.First().CDNoiGT; }
                    //    if (q.First().DChi != null)
                    //    { themmoi.diachi = q.First().DChi; }
                    //    if (q.First().SThe != null)
                    //    { themmoi.sothe = q.First().SThe; }
                    //    if (q.First().Tuoi != null)
                    //    { themmoi.tuoi = q.First().Tuoi.Value; }
                    //    var id1 = (_Data.BenhNhans.Where(p => p.MaBNhan == a.Key)).ToList();
                    //    if (id1.Count > 0 && id1.First().NoiTru != null)
                    //    {
                    //        themmoi.phuongan = id1.First().NoiTru.Value;
                    //    }
                    //    var RV =(from v in _Data.RaViens.Where(p=>p.MaBNhan==a.Key) select new {v.Status}).ToList();
                    //    if (RV.Count > 0 && RV.First().Status != null)
                    //    {
                    //        themmoi.ct = RV.First().Status.Value;
                    //    }
                    //    else
                    //    { themmoi.ct = 2; }
                    //    var id = (_Data.BNKBs.Where(p => p.IDKB == a.IDKB)).ToList();
                    //    if (id.Count > 0 && id.First().ChanDoan != null)
                    //    {
                    //        themmoi.cd = id.First().ChanDoan;
                    //    }
                    //    if (id.Count > 0 && id.First().MaCB != null)
                    //    {
                    //        string Ma = id.First().MaCB;
                    //        var cb = (from bs in _Data.CanBoes.Where(p => p.MaCB == Ma) select new { bs.TenCB }).ToList();
                    //        if (cb.Count > 0 && cb.First().TenCB != null)
                    //        {
                    //            themmoi.tenbs = cb.First().TenCB;
                    //        }
                    //    }
                    //    if (q.First().DTuong != null)
                    //    { themmoi.dtuong = q.First().DTuong; }
                    //    if (q.First().NgayVao!=null)
                    //    themmoi.NgayVao = q.First().NgayVao.Value;
                    //    if (q.First().NgayRa != null)
                    //    themmoi.NgayRa = q.First().NgayRa.Value;
                    //    if (q.First().songaydt != null)
                    //    themmoi.SoNgayDT = q.First().songaydt.Value;
                    //    themmoi.NhomTuoi = q.First().NhomTuoi;
                    //    _benhnhan.Add(themmoi);
                    //}
                //}
                    if (_benhnhan.Count > 0)
                {
                    frmIn frm = new frmIn();
                    BaoCao.Rep_SoVaoVien_ys rep = new BaoCao.Rep_SoVaoVien_ys();
                    var qkp = (from kp in _Data.KPhongs.Where(p => p.MaKP == _makhoa) select new { kp.TenKP }).ToList();
                    if (qkp.Count > 0)
                    {
                        rep.Khoa.Value = qkp.First().TenKP.ToUpper();
                    }
                    rep.DataSource = _benhnhan.OrderBy(p => p.NgayRa).ToList();
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
    }
}