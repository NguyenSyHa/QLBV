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
    public partial class Frm_BcThuVP_VY : DevExpress.XtraEditors.XtraForm
    {
        public Frm_BcThuVP_VY()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities _Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private bool KT()
        {
            if (lupNgaytu.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn ngày bắt đầu in báo cáo");
                lupNgaytu.Focus();
                return false;
            }
            if (lupNgaytu.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn ngày kết thúc in báo cáo");
                lupNgayden.Focus();
                return false;
            }
            else return true;
        }
        private class KP
        {
            private int MaKP;
            private string TenKP;
            public int makp
            { set { MaKP = value; } get { return MaKP; } }
            public string tenkp
            { set { TenKP = value; } get { return TenKP; } }
        }
      
        private class DMT
        {
            private int MaKP;
            private string TenTN;
            private int idTN;
            private double ST1;
            private double ST2;
            private double ST3;
            private double ST4;
            private double ST5;
            private double ST6;
            private double ST7;
            private double ST8;
            private double ST9;
            private double ST10;
            private double ST11;
            private double ST12;
            private double ST13;
            private double Tong;
            public int makp
            { set { MaKP = value; } get { return MaKP; } }
            public int idtn
            { set { idTN = value; } get { return idTN; } }
            public string tentn
            { set { TenTN = value; } get { return TenTN; } }
            public double st1
            { set { ST1 = value; } get { return ST1; } }
            public double st2
            { set { ST2 = value; } get { return ST2; } }
            public double st3
            { set { ST3 = value; } get { return ST3; } }
            public double st4
            { set { ST4 = value; } get { return ST4; } }
            public double st5
            { set { ST5 = value; } get { return ST5; } }
            public double st6
            { set { ST6 = value; } get { return ST6; } }
            public double st7
            { set { ST7 = value; } get { return ST7; } }
            public double st8
            { set { ST8 = value; } get { return ST8; } }
            public double st9
            { set { ST9 = value; } get { return ST9; } }
            public double st10
            { set { ST10 = value; } get { return ST10; } }
            public double st11
            { set { ST11 = value; } get { return ST11; } }
            public double st12
            { set { ST12 = value; } get { return ST12; } }
            public double st13
            { set { ST13 = value; } get { return ST13; } }
            public double tong
            { set { Tong = value; } get { return Tong; } }
        }
        private void Frm_BcThuVP_VY_Load(object sender, EventArgs e)
        {
            lupNgaytu.Focus();
            lupNgaytu.DateTime = System.DateTime.Now;
            lupNgayden.DateTime = System.DateTime.Now;
        }
        List<KP> _KP=new List<KP>();
        List<DMT> _DMT=new List<DMT>();
        private void btnOK_Click(object sender, EventArgs e)
        {
            _KP.Clear();
            _DMT.Clear();
            DateTime tungay = DungChung.Ham.NgayTu(lupNgaytu.DateTime);
            DateTime denngay = DungChung.Ham.NgayDen(lupNgayden.DateTime);
            if (KT())
            {

                var qkp = (from kp in _Data.KPhongs.Where(p => p.PLoai == "Lâm sàng").Where(p => p.TenKP != "Phòng mổ") select new { kp.MaKP, kp.TenKP }).ToList();
                if (qkp.Count() > 0)
                {
                    foreach (var a in qkp)
                    {
                        {
                            KP themmoi = new KP();
                            themmoi.makp = a.MaKP;
                            themmoi.tenkp = a.TenKP;
                            _KP.Add(themmoi);
                           
                            
                        }

                    }

                }


               
            //var qdmt = (from vp in _Data.VienPhis.Where(p => p.NgayTT >= tungay).Where(p => p.NgayTT <= denngay)
            //               join vpct in _Data.VienPhicts on vp.idVPhi equals vpct.idVPhi
            //               join dv in _Data.DichVus on vpct.MaDV equals dv.MaDV
            //               join tn in _Data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
            //               join bn in _Data.BenhNhans on vp.MaBNhan equals bn.MaBNhan
            //               group new { tn,bn} by new {tn.IdTieuNhom, tn.TenTN,bn.NoiTru } into kq
            //               select new {
            //                           idTN=kq.Key.IdTieuNhom,
            //                           TenTN=kq.Key.TenTN,
            //                           NoiTru=kq.Key.NoiTru,
            //                      //     MaKP=kq.Key.MaKP,
            //                            }).ToList();
                if (radNT.SelectedIndex == 0)
                    {
                     var qdmt = (from bn in _Data.BenhNhans.Where(p=>p.NoiTru==1)
                                join vp in _Data.VienPhis on bn.MaBNhan equals vp.MaBNhan
                                join vpct in _Data.VienPhicts on vp.idVPhi equals vpct.idVPhi
                                join dv in _Data.DichVus on vpct.MaDV equals dv.MaDV
                                join tn in _Data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                                    where (vp.NgayTT >= tungay && vp.NgayTT <= denngay)
                                group new { tn,bn} by new {tn.IdTieuNhom, tn.TenTN } into kq
                                    select new {
                                       idTN=kq.Key.IdTieuNhom,
                                       TenTN=kq.Key.TenTN,
                                  //     MaKP=kq.Key.MaKP,
                                        }).ToList();
                    if(qdmt.Count>0)
                    {
                        foreach (var a in qdmt)
                        {
                            DMT themmoi = new DMT();
                            // themmoi.makp = a.MaKP;
                            themmoi.tentn = a.TenTN;
                            themmoi.idtn = Convert.ToInt32(a.idTN);
                            _DMT.Add(themmoi);
                        }
                    }
                }
                    if (radNT.SelectedIndex == 1)
                    {
                        var qdmt = (from bn in _Data.BenhNhans
                                join vp in _Data.VienPhis on bn.MaBNhan equals vp.MaBNhan
                                join vpct in _Data.VienPhicts on vp.idVPhi equals vpct.idVPhi
                                join dv in _Data.DichVus on vpct.MaDV equals dv.MaDV
                                join tn in _Data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                                    where (vp.NgayTT >= tungay && vp.NgayTT <= denngay)
                                group new { tn,bn} by new {tn.IdTieuNhom, tn.TenTN } into kq
                                    select new {
                                       idTN=kq.Key.IdTieuNhom,
                                       TenTN=kq.Key.TenTN,
                                  //     MaKP=kq.Key.MaKP,
                                        }).ToList();
                        if(qdmt.Count>0)
                        {
                            foreach (var a in qdmt)
                            {
                                DMT themmoi = new DMT();
                                // themmoi.makp = a.MaKP;
                                themmoi.tentn = a.TenTN;
                                themmoi.idtn = Convert.ToInt32(a.idTN);
                                _DMT.Add(themmoi);
                            }
                        }
                    
                    }
                //if (radNT.SelectedIndex == 0)
                //{
                    var qtien =  (from bn in _Data.BenhNhans.Where(p => p.NoiTru == 1)
                                join vp in _Data.VienPhis on bn.MaBNhan equals vp.MaBNhan
                                join vpct in _Data.VienPhicts on vp.idVPhi equals vpct.idVPhi
                                join dv in _Data.DichVus on vpct.MaDV equals dv.MaDV
                                join tn in _Data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                                where (vp.NgayTT >= tungay && vp.NgayTT <= denngay)
                                group new { vpct, tn, bn } by new { tn.IdTieuNhom, vpct.ThanhTien, bn.MaKP } into kq
                                 select new
                                 {
                                     idTN = kq.Key.IdTieuNhom,
                                     MaKP = kq.Key.MaKP,
                                     ThanhTien = kq.Sum(p => p.vpct.ThanhTien),
                                 }).ToList();
                    //var qtien = (from vp in _Data.VienPhis.Where(p => p.NgayTT >= tungay).Where(p => p.NgayTT <= denngay)
                    //             join vpct in _Data.VienPhicts on vp.idVPhi equals vpct.idVPhi
                    //             join dv in _Data.DichVus on vpct.MaDV equals dv.MaDV
                    //             join tn in _Data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                    //             join bn in _Data.BenhNhans.Where(p => p.NoiTru == 1) on vp.MaBNhan equals bn.MaBNhan
                    //             group new { vpct, tn, bn } by new { tn.IdTieuNhom, vpct.ThanhTien, bn.MaKP } into kq
                    //             select new
                    //             {
                    //                 idTN = kq.Key.IdTieuNhom,
                    //                 MaKP = kq.Key.MaKP,
                    //                 ThanhTien = kq.Sum(p => p.vpct.ThanhTien),
                    //             }).ToList();

                    if (qtien.Count > 0)
                    {
                        foreach (var a in _DMT)
                        {
                            foreach (var b in qtien)
                            {
                                if (a.idtn == b.idTN)
                                {
                                    if (b.ThanhTien != null && b.ThanhTien > 0)
                                    {
                                        for (int i = 0; i < _KP.Count; i++)
                                        {
                                            if (b.MaKP == _KP.Skip(i).First().makp)
                                            {
                                                switch (i)
                                                {
                                                    case 0:
                                                        a.st1 = Convert.ToDouble(b.ThanhTien);

                                                        break;
                                                    case 1:
                                                        a.st2 = Convert.ToDouble(b.ThanhTien);
                                                        break;
                                                    case 2:
                                                        a.st3 = Convert.ToDouble(b.ThanhTien);
                                                        break;
                                                    case 3:
                                                        a.st4 = Convert.ToDouble(b.ThanhTien);
                                                        break;
                                                    case 4:
                                                        a.st5 = Convert.ToDouble(b.ThanhTien);
                                                        break;
                                                    case 5:
                                                        a.st6 = Convert.ToDouble(b.ThanhTien);
                                                        break;
                                                    case 6:
                                                        a.st7 = Convert.ToDouble(b.ThanhTien);
                                                        break;
                                                    case 7:
                                                        a.st8 = Convert.ToDouble(b.ThanhTien);
                                                        break;
                                                    case 8:
                                                        a.st9 = Convert.ToDouble(b.ThanhTien);
                                                        break;
                                                    case 9:
                                                        a.st10 = Convert.ToDouble(b.ThanhTien);
                                                        break;
                                                    case 10:
                                                        a.st11 = Convert.ToDouble(b.ThanhTien);
                                                        break;
                                                    case 11:
                                                        a.st12 = Convert.ToDouble(b.ThanhTien);
                                                        break;
                                                    //case 12:
                                                    //    a.st13 = Convert.ToDouble(b.ThanhTien);
                                                    //    break;
                                                }
                                            }
                                        }
                                    }
                                    // a.tong = b.ThanhTien.ToString();
                                }
                            }

                        }
                    }
                //}
                if (radNT.SelectedIndex == 1)
                {
                    //KP themmoi1 = new KP();
                    //themmoi1.makp = "NT";
                    //themmoi1.tenkp = "Khoa khám bệnh";
                    //_KP.Add(themmoi1);

                    var qngt = (from bn in _Data.BenhNhans
                                 join vp in _Data.VienPhis on bn.MaBNhan equals vp.MaBNhan
                                 join vpct in _Data.VienPhicts on vp.idVPhi equals vpct.idVPhi
                                 join dv in _Data.DichVus on vpct.MaDV equals dv.MaDV
                                 join tn in _Data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                                 where (vp.NgayTT >= tungay && vp.NgayTT <= denngay)
                                 group new { vpct, tn, bn } by new { tn.IdTieuNhom, vpct.ThanhTien } into kq
                                 select new
                                 {
                                     idTN = kq.Key.IdTieuNhom,
                                     ThanhTien = kq.Sum(p => p.vpct.ThanhTien),
                                 }).ToList();
                    //var qngt = (from vp in _Data.VienPhis
                    //            join vpct in _Data.VienPhicts on vp.idVPhi equals vpct.idVPhi
                    //            join dv in _Data.DichVus on vpct.MaDV equals dv.MaDV
                    //            join tn in _Data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                    //            join bn in _Data.BenhNhans on vp.MaBNhan equals bn.MaBNhan
                    //            where(vp.NgayTT >= tungay && vp.NgayTT <= denngay)
                    //            group new { vpct, tn, bn } by new { tn.IdTieuNhom, vpct.ThanhTien } into kq
                    //            select new
                    //            {         
                    //                idTN = kq.Key.IdTieuNhom,
                    //                ThanhTien = kq.Sum(p => p.vpct.ThanhTien),
                    //            }).ToList();
                    if (qngt.Count > 0)
                    {
                        foreach (var a in _DMT)
                        {
                            foreach (var b in qngt)
                            {
                                if (a.idtn == b.idTN)
                                {
                                    if (b.ThanhTien != null && b.ThanhTien > 0)
                                    {
                                        for (int i = 0; i < _KP.Count; i++)
                                        {
                                            a.st13 = Convert.ToDouble(b.ThanhTien);
                                        }
                                    }
                                }
                            }

                        }
                    }
                    var qsk = (from tu in _Data.TamUngs.Where(p => p.NgayThu >= tungay).Where(p => p.NgayThu <= denngay)
                               join bn in _Data.BenhNhans.Where(p => p.DTuong.Contains("KSK")) on tu.MaBNhan equals bn.MaBNhan
                               group new { tu, bn } by new { tu.SoTien } into kq
                               select new { SoTien = kq.Sum(p => p.tu.SoTien) }).ToList();
                    if (qsk.Count > 0)
                    {
                        DMT themmoi = new DMT();
                        themmoi.idtn = 0;
                        themmoi.tentn = "Khám sức khỏe";
                        _DMT.Add(themmoi);

                        foreach (var a in _DMT)
                        {
                            foreach (var b in qsk)
                            {
                                if (a.idtn == 0)
                                {
                                    a.st13 = Convert.ToDouble(b.SoTien);
                                }
                            }
                        }
                    }
                }

                    
                foreach (var a in _DMT)
                {
                    a.tong = a.st1 + a.st2 + a.st3 + a.st4 + a.st5 + a.st6 + a.st7 + a.st8 + a.st9+a.st10+a.st11+a.st12+a.st13;
                }
                        BaoCao.Rep_BcThuVP_VY rep = new BaoCao.Rep_BcThuVP_VY();
                        frmIn frm = new frmIn();
                        for (int i = 0; i < _KP.Count; i++)
                        {
                            switch (i)
                            {
                                case 0:
                                    if (_KP.Skip(i).First().tenkp != null)
                                    { rep.TenKP1.Value = _KP.Skip(i).First().tenkp; }
                                   { rep.MaKP1.Value = _KP.Skip(i).First().makp; }
                                    break;
                                case 1:
                                    if (_KP.Skip(i).First().tenkp != null)
                                    { rep.TenKP2.Value = _KP.Skip(i).First().tenkp; }
                                  { rep.MaKP2.Value = _KP.Skip(i).First().makp; }
                                              break;
                                case 2:
                                     if (_KP.Skip(i).First().tenkp != null)
                                    { rep.TenKP3.Value = _KP.Skip(i).First().tenkp; }
                                    { rep.MaKP3.Value = _KP.Skip(i).First().makp; }
                                             break;
                                case 3:
                                    if (_KP.Skip(i).First().tenkp != null)
                                    { rep.TenKP4.Value = _KP.Skip(i).First().tenkp; }
                                        { rep.MaKP4.Value = _KP.Skip(i).First().makp; }
                                         break;
                                case 4:
                                    if (_KP.Skip(i).First().tenkp != null)
                                    { rep.TenKP5.Value = _KP.Skip(i).First().tenkp; }
                                   { rep.MaKP5.Value = _KP.Skip(i).First().makp; }
                                    break;
                                case 5:
                                    if (_KP.Skip(i).First().tenkp != null)
                                    { rep.TenKP6.Value = _KP.Skip(i).First().tenkp; }
                                  { rep.MaKP6.Value = _KP.Skip(i).First().makp; }
                                              break;
                                case 6:
                                    if (_KP.Skip(i).First().tenkp != null)
                                    { rep.TenKP7.Value = _KP.Skip(i).First().tenkp; }
                                                   break;
                                case 7:
                                    if (_KP.Skip(i).First().tenkp != null)
                                    { rep.TenKP8.Value = _KP.Skip(i).First().tenkp; }
                                  { rep.MaKP8.Value = _KP.Skip(i).First().makp; }
                                              break;
                                case 8:
                                    if (_KP.Skip(i).First().tenkp != null)
                                    { rep.TenKP9.Value = _KP.Skip(i).First().tenkp; }
                                      break;
                                case 9:
                                      if (_KP.Skip(i).First().tenkp != null)
                                      { rep.TenKP10.Value = _KP.Skip(i).First().tenkp; }
                                      break;
                                case 10:
                                      if (_KP.Skip(i).First().tenkp != null)
                                      { rep.TenKP11.Value = _KP.Skip(i).First().tenkp; }
                                      break;
                                case 11:
                                      if (_KP.Skip(i).First().tenkp != null)
                                      { rep.TenKP12.Value = _KP.Skip(i).First().tenkp; }
                                      break;
                                case 12:
                                      if (_KP.Skip(i).First().tenkp != null)
                                      { rep.TenKP13.Value = _KP.Skip(i).First().tenkp; }
                                      break;
                            }
                        }
    
                       rep.TuNgayDenNgay.Value = " Từ ngày " + lupNgaytu.Text.Substring(0, 10) + " đến ngày " +lupNgayden.Text.Substring(0, 10);
                       rep.TuNgay.Value = lupNgaytu.Text.Substring(0, 10);
                       rep.DenNgay.Value =  lupNgayden.Text.Substring(0, 10);
                       if (radNT.SelectedIndex == 1)
                       {
                           rep.NNT.Value = 1;
                           rep.TenKP13.Value = "Khoa khám bệnh";
                       }
                       rep.DataSource = _DMT;
                        rep.BindingData();
                        rep.CreateDocument();
                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                        frm.ShowDialog();
              }
            
        }


        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}