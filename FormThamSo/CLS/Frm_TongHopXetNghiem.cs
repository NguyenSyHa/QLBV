﻿using System;using QLBV_Database;
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
    public partial class Frm_TongHopXetNghiem : DevExpress.XtraEditors.XtraForm
    {
        public Frm_TongHopXetNghiem()
        {
            InitializeComponent();
        }

        private void ButHuy_Click(object sender, EventArgs e)
        {
            this.Close();
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
        

        private class Baocao
        {
            private string TenBN;
            private string Gioitinh;
            private string Tuoi;
            private string Diachi;
            private string Chuandoan;
            private string Noigui;
            private string Yeucau;
            private string KQ1;
            private string KQ2;
            private string KQ3;
            private string KQ4;
            private string KQ5;
            private string KQ6;
            private string KQ7;
            private string KQ8;
            private string KQ9;
            private string KQ10;
            private string KQ11;
            private string KQ12;
            private string KQ13;
            private string KQ14;
            private string KQ15;
            private string KQ16;
            private string KQ17;
            private string KQ18;
            private string KQ19;
            private string KQ20;
            private string KQ21;
            private string KQ22;
            private string KQ23;
            private string KQ24;
            private string KQ25;
            private string KQ26;
            private string KQ27;
            private string KQ28;
            private string KQ29;
            private string KQ30;
            private string BHYT;
            private string MaDVct;
            private string Ngaythang;
            public string ngaythang
            {
                set { Ngaythang = value; }
                get { return Ngaythang; }
            }
            public string bhyt
            {
                set { BHYT = value; }
                get { return BHYT; }
            }
            public string tenBN
            {
                set { TenBN = value; }
                get { return TenBN; }
            }
            public string gioitinh
            {
                set { Gioitinh = value; }
                get { return Gioitinh; }
            }
            public string tuoi
            {
                set { Tuoi = value; }
                get { return Tuoi; }
            }
            public string diachi
            {
                set { Diachi = value; }
                get { return Diachi; }
            }
            public string chuandoan
            {
                set { Chuandoan = value; }
                get { return Chuandoan; }
            }
            public string noigui
            {
                set { Noigui = value; }
                get { return Noigui; }
            }
            public string madvct
            {
                set { MaDVct = value; }
                get { return MaDVct; }
            }
            public string kq1
            {
                set { KQ1 = value; }
                get { return KQ1; }
            }
            public string kq2
            {
                set { KQ2 = value; }
                get { return KQ2; }
            }
            public string kq3
            {
                set { KQ3 = value; }
                get { return KQ3; }
            }
            public string kq4
            {
                set { KQ4 = value; }
                get { return KQ4; }
            }
            public string kq5
            {
                set { KQ5 = value; }
                get { return KQ5; }
            }
            public string kq6
            {
                set { KQ6 = value; }
                get { return KQ6; }
            }
            public string kq7
            {
                set { KQ7 = value; }
                get { return KQ7; }
            }
            public string kq8
            {
                set { KQ8 = value; }
                get { return KQ8; }
            }
            public string kq9
            {
                set { KQ9 = value; }
                get { return KQ9; }
            }
            public string kq10
            {
                set { KQ10 = value; }
                get { return KQ10; }
            }
            public string kq11
            {
                set { KQ11 = value; }
                get { return KQ11; }
            }
            public string kq12
            {
                set { KQ12 = value; }
                get { return KQ12; }
            }
            public string kq13
            {
                set { KQ13 = value; }
                get { return KQ13; }
            }
            public string kq14
            {
                set { KQ14 = value; }
                get { return KQ14; }
            }
            public string kq15
            {
                set { KQ15 = value; }
                get { return KQ15; }
            }
            public string kq16
            {
                set { KQ16 = value; }
                get { return KQ16; }
            }
            public string kq17
            {
                set { KQ17 = value; }
                get { return KQ17; }
            }
            public string kq18
            {
                set { KQ18 = value; }
                get { return KQ18; }
            }
            public string kq19
            {
                set { KQ19 = value; }
                get { return KQ19; }
            }
            public string kq20
            {
                set { KQ20 = value; }
                get { return KQ20; }
            }
            public string kq21
            {
                set { KQ21 = value; }
                get { return KQ21; }
            }
            public string kq22
            {
                set { KQ22 = value; }
                get { return KQ22; }
            }
            public string kq23
            {
                set { KQ23 = value; }
                get { return KQ23; }
            }
            public string kq24
            {
                set { KQ24 = value; }
                get { return KQ24; }
            }
            public string kq25
            {
                set { KQ25 = value; }
                get { return KQ25; }
            }
            public string kq26
            {
                set { KQ26 = value; }
                get { return KQ26; }
            }
            public string kq27
            {
                set { KQ27 = value; }
                get { return KQ27; }
            }
            public string kq28
            {
                set { KQ28 = value; }
                get { return KQ28; }
            }
            public string kq29
            {
                set { KQ29 = value; }
                get { return KQ29; }
            }
            public string kq30
            {
                set { KQ30 = value; }
                get { return KQ30; }
            }
        }
        List<Baocao> _Baocao = new List<Baocao>();
        private void butTaoBC_Click(object sender, EventArgs e)
        {

            DateTime NT = DungChung.Ham.NgayTu(LupNgaytu.DateTime);
            DateTime ND = DungChung.Ham.NgayDen(LupNgayden.DateTime);
            //string _MaKP1 = "";
            //string _MaKP2 = "";
            //string _MaKP3 = "";
            //string _MaKP4 = "";
            //string _MaKP5 = "";
            //string _MaKP6 = "";
            //string _MaKP7 = "";
            //string _MaKP8 = "";
            //string _MaKP9 = "";
            //string _MaKP10 = "";
            //string _MaKP11 = "";
            //string _MaKP12 = "";
            //string _MaKP13 = "";
            //string _MaKP14 = "";
            //string _MaKP15 = "";
            //string _MaKP16 = "";
            //string _MaKP17 = "";
            //string _MaKP18 = "";
            //string _MaKP19 = "";
            //string _MaKP20 = "";
            //string _MaKP21 = "";
            //string _MaKP22 = "";
            //string _MaKP23 = "";
            //string _MaKP24 = "";
            //string _MaKP25 = "";
            //string _MaKP26 = "";
            //string _MaKP27 = "";
            //string _MaKP28 = "";
            //for (int i = 0; i < _Kphong.Count; i++)
            //{
            //    if (_Kphong.Skip(i).First().chon == true)
            //    {
            //        switch (i)
            //        {
            //            case 0:
            //                _MaKP1 = _Kphong.Skip(i).First().makp;
            //                break;
            //            case 1:
            //                _MaKP2 = _Kphong.Skip(i).First().makp;
            //                break;
            //            case 2:
            //                _MaKP3 = _Kphong.Skip(i).First().makp;
            //                break;
            //            case 3:
            //                _MaKP4 = _Kphong.Skip(i).First().makp;
            //                break;
            //            case 4:
            //                _MaKP5 = _Kphong.Skip(i).First().makp;
            //                break;
            //            case 5:
            //                _MaKP6 = _Kphong.Skip(i).First().makp;
            //                break;
            //            case 6:
            //                _MaKP7 = _Kphong.Skip(i).First().makp;
            //                break;
            //            case 7:
            //                _MaKP8 = _Kphong.Skip(i).First().makp;
            //                break;
            //            case 8:
            //                _MaKP9 = _Kphong.Skip(i).First().makp;
            //                break;
            //            case 9:
            //                _MaKP10 = _Kphong.Skip(i).First().makp;
            //                break;
            //            case 10:
            //                _MaKP11 = _Kphong.Skip(i).First().makp;
            //                break;
            //            case 11:
            //                _MaKP12 = _Kphong.Skip(i).First().makp;
            //                break;
            //            case 12:
            //                _MaKP13 = _Kphong.Skip(i).First().makp;
            //                break;
            //            case 13:
            //                _MaKP14 = _Kphong.Skip(i).First().makp;
            //                break;
            //            case 14:
            //                _MaKP15 = _Kphong.Skip(i).First().makp;
            //                break;
            //            case 15:
            //                _MaKP16 = _Kphong.Skip(i).First().makp;
            //                break;
            //            case 16:
            //                _MaKP17 = _Kphong.Skip(i).First().makp;
            //                break;
            //            case 17:
            //                _MaKP18 = _Kphong.Skip(i).First().makp;
            //                break;
            //            case 18:
            //                _MaKP19 = _Kphong.Skip(i).First().makp;
            //                break;
            //            case 19:
            //                _MaKP20 = _Kphong.Skip(i).First().makp;
            //                break;
            //            case 20:
            //                _MaKP21 = _Kphong.Skip(i).First().makp;
            //                break;
            //            case 21:
            //                _MaKP22 = _Kphong.Skip(i).First().makp;
            //                break;
            //            case 22:
            //                _MaKP23 = _Kphong.Skip(i).First().makp;
            //                break;
            //            case 23:
            //                _MaKP24 = _Kphong.Skip(i).First().makp;
            //                break;
            //            case 24:
            //                _MaKP25 = _Kphong.Skip(i).First().makp;
            //                break;
            //            case 25:
            //                _MaKP26 = _Kphong.Skip(i).First().makp;
            //                break;
            //            case 26:
            //                _MaKP27 = _Kphong.Skip(i).First().makp;
            //                break;
            //            case 27:
            //                _MaKP28 = _Kphong.Skip(i).First().makp;
            //                break;
            //        }
            //    }
            //}
            string _tenrg = "";
            _tenrg = cmbInBC.Text;
            if (cboTT.SelectedIndex == 1)// đã thanh toán, lấy theo ngày thanh toán
            {

                var canlamsang5 = (from cls in _Data.CLS
                                  where (from vp in _Data.VienPhis select vp.MaBNhan).Contains(cls.MaBNhan)
                                  join cd in _Data.ChiDinhs.Where(p=>p.Status==1) on cls.IdCLS equals cd.IdCLS
                                  join clsct in _Data.CLScts on cd.IDCD equals clsct.IDCD
                                  join dv in _Data.DichVus on cd.MaDV equals dv.MaDV
                                  join tn in _Data.TieuNhomDVs.Where(p => p.TenRG.ToLower().Contains(_tenrg.ToLower())) on dv.IdTieuNhom equals tn.IdTieuNhom
                                  join VP in _Data.VienPhis.Where(p => p.NgayTT >= NT).Where(p => p.NgayTT <= ND) on cls.MaBNhan equals VP.MaBNhan
                                  //join kp in _Kphong.Where(p=>p.chon=true) on cls.MaKP equals kp.makp
                                  //where (cls.MaKP == _MaKP1 || cls.MaKP == _MaKP10 || cls.MaKP == _MaKP11 || cls.MaKP == _MaKP12 || cls.MaKP == _MaKP13 || cls.MaKP == _MaKP14 || cls.MaKP == _MaKP15 || cls.MaKP == _MaKP2 || cls.MaKP == _MaKP3 || cls.MaKP == _MaKP4 || cls.MaKP == _MaKP5 || cls.MaKP == _MaKP6 || cls.MaKP == _MaKP7 || cls.MaKP == _MaKP8 || cls.MaKP == _MaKP9 || cls.MaKP == _MaKP16 || cls.MaKP == _MaKP17 || cls.MaKP == _MaKP18 || cls.MaKP == _MaKP19 || cls.MaKP == _MaKP20 || cls.MaKP == _MaKP21 || cls.MaKP == _MaKP22 || cls.MaKP == _MaKP23 || cls.MaKP == _MaKP24 || cls.MaKP == _MaKP25 || cls.MaKP == _MaKP26|| cls.MaKP == _MaKP27 || cls.MaKP == _MaKP28)
                                  group new { cls } by new { cls.IdCLS, cls.MaKP } into kq
                                  select new { IDCLS = kq.Key.IdCLS, MaKP=kq.Key.MaKP }).ToList();
                var canlamsang= (from ls in canlamsang5
                                join kp in _Kphong.Where(p=>p.chon==true) on ls.MaKP equals kp.makp
                                group new { ls } by new { ls.IDCLS } into kq
                                  select new { IDCLS = kq.Key.IDCLS }).ToList();
                //
                string[] arr = new string[50];
                for (int i = 0; i < 50; i++)
                {
                    arr[i] = "";
                }
                    var madvct = (from clsct in _Data.CLScts
                             join dvct in _Data.DichVucts on clsct.MaDVct equals dvct.MaDVct
                             join dv in _Data.DichVus on dvct.MaDV equals dv.MaDV
                                  join tn in _Data.TieuNhomDVs.Where(p => p.TenRG.ToLower().Contains(_tenrg.ToLower())) on dv.IdTieuNhom equals tn.IdTieuNhom
                             group new { clsct,dvct } by new { clsct.MaDVct,dvct.MaDV} into kq
                             select new { kq.Key.MaDVct,kq.Key.MaDV, }).OrderBy(p=>p.MaDV).ToList();
                    int k = 0;
                foreach (var c in madvct)
                {
                    
                    arr[k] = c.MaDVct;
                    k++;
                }
                //
                if (canlamsang.Count > 0)
                {
                    foreach (var a in canlamsang)
                    {
                        var benhnhan5 = (from bn in _Data.BenhNhans
                                        join cls in _Data.CLS.Where(p => p.IdCLS == a.IDCLS) on bn.MaBNhan equals cls.MaBNhan
                                        //join kp1 in _Kphong.Where(p => p.chon = true) on cls.MaKP equals kp1.makp
                                        //where (cls.MaKP == _MaKP1 || cls.MaKP == _MaKP10 || cls.MaKP == _MaKP11 || cls.MaKP == _MaKP12 || cls.MaKP == _MaKP13 || cls.MaKP == _MaKP14 || cls.MaKP == _MaKP15 || cls.MaKP == _MaKP2 || cls.MaKP == _MaKP3 || cls.MaKP == _MaKP4 || cls.MaKP == _MaKP5 || cls.MaKP == _MaKP6 || cls.MaKP == _MaKP7 || cls.MaKP == _MaKP8 || cls.MaKP == _MaKP9 || cls.MaKP == _MaKP16 || cls.MaKP == _MaKP17 || cls.MaKP == _MaKP18 || cls.MaKP == _MaKP19 || cls.MaKP == _MaKP20 || cls.MaKP == _MaKP21 || cls.MaKP == _MaKP22 || cls.MaKP == _MaKP23 || cls.MaKP == _MaKP24 || cls.MaKP == _MaKP25 || cls.MaKP == _MaKP26 || cls.MaKP == _MaKP27 || cls.MaKP == _MaKP28)
                                        join kp in _Data.KPhongs on bn.MaKP equals kp.MaKP
                                        select new { bn.TenBNhan, bn.GTinh, bn.Tuoi, bn.DChi, bn.DTuong, kp.TenKP, bn.MaBNhan, cls.MaKP }).ToList();

                        var benhnhan = (from bn in benhnhan5 
                                      join kp in _Kphong.Where(p=>p.chon==true) on bn.MaKP equals kp.makp
                                      select new { bn.TenBNhan, bn.GTinh, bn.Tuoi, bn.DChi, bn.DTuong, bn.TenKP, bn.MaBNhan, bn.MaKP }).ToList();
                        if (benhnhan.Count > 0)
                        {
                            var hoasinh = (from chidinh in _Data.ChiDinhs.Where(p => p.IdCLS == a.IDCLS)
                                           join clsct in _Data.CLScts.Where(p => p.Status == 1) on chidinh.IDCD equals clsct.IDCD
                                           join dvct in _Data.DichVucts on clsct.MaDVct equals dvct.MaDVct
                                           select new { dvct.TenDVct, dvct.STT,dvct.MaDVct, clsct.KetQua}).OrderBy(p => p.STT).ToList();
                            if (hoasinh.Count > 0)
                            {
                                Baocao themmoi = new Baocao();
                                if (benhnhan.First().TenBNhan != null)
                                {
                                    themmoi.tenBN = benhnhan.First().TenBNhan;
                                }
                                if (benhnhan.First().GTinh != null)
                                {
                                    themmoi.gioitinh = benhnhan.First().GTinh.ToString();
                                }
                                if (benhnhan.First().Tuoi != null)
                                {
                                    themmoi.tuoi = benhnhan.First().Tuoi.ToString();
                                }
                                if (benhnhan.First().DChi != null)
                                {
                                    themmoi.diachi = benhnhan.First().DChi;
                                }
                                if (benhnhan.First().DTuong != null && benhnhan.First().DTuong == "BHYT")
                                {
                                    themmoi.bhyt = "X";
                                }
                                if (benhnhan.First().TenKP != null)
                                {
                                    themmoi.noigui = benhnhan.First().TenKP;
                                }
                                if (benhnhan.First().MaBNhan != null && benhnhan.First().MaKP != null)
                                {
                                    int Mabn = benhnhan.First().MaBNhan;
                                    int MaKP = benhnhan.First().MaKP.Value;
                                    

                                    var bnkb5 = (from bn in _Data.BNKBs.Where(p => p.MaBNhan == Mabn).Where(p => p.MaKP == MaKP)
                                                 join cls in _Data.CLS on bn.MaBNhan equals cls.MaBNhan
                                                 //join kp in _Kphong.Where(p => p.chon = true) on cls.MaKP equals kp.makp
                                                 //where (cls.MaKP == _MaKP1 || cls.MaKP == _MaKP10 || cls.MaKP == _MaKP11 || cls.MaKP == _MaKP12 || cls.MaKP == _MaKP13 || cls.MaKP == _MaKP14 || cls.MaKP == _MaKP15 || cls.MaKP == _MaKP2 || cls.MaKP == _MaKP3 || cls.MaKP == _MaKP4 || cls.MaKP == _MaKP5 || cls.MaKP == _MaKP6 || cls.MaKP == _MaKP7 || cls.MaKP == _MaKP8 || cls.MaKP == _MaKP9 || cls.MaKP == _MaKP16 || cls.MaKP == _MaKP17 || cls.MaKP == _MaKP18 || cls.MaKP == _MaKP19 || cls.MaKP == _MaKP20 || cls.MaKP == _MaKP21 || cls.MaKP == _MaKP22 || cls.MaKP == _MaKP23 || cls.MaKP == _MaKP24 || cls.MaKP == _MaKP25 || cls.MaKP == _MaKP26 || cls.MaKP == _MaKP27 || cls.MaKP == _MaKP28)
                                                 select new { bn.ChanDoan, bn.IDKB, bn.MaKP }).OrderBy(p => p.IDKB).ToList();

                                    var bnkb = (from bn in bnkb5
                                                join kp in _Kphong.Where(p => p.chon = true) on bn.MaKP equals kp.makp
                                                select new { bn.ChanDoan, bn.IDKB }).OrderBy(p => p.IDKB).ToList();
                                    if (bnkb.Count > 0 && bnkb.First().ChanDoan != null)
                                    {
                                        themmoi.chuandoan = bnkb.First().ChanDoan;
                                    }
                                }
                                #region cach2
                                foreach (var b in hoasinh) {
                                    for (int i = 0; i < 50;i++ )
                                    {
                                        if (arr[i] == b.MaDVct)
                                        {
                                            string STT =i.ToString();
                                            switch (STT)
                                            {
                                                case "1":
                                                    themmoi.kq1 = b.KetQua;
                                                    break;
                                                case "2":
                                                    themmoi.kq2 = b.KetQua;
                                                    break;
                                                case "3":
                                                    themmoi.kq3 = b.KetQua;
                                                    break;
                                                case "4":
                                                    themmoi.kq4 = b.KetQua;
                                                    break;
                                                case "5":
                                                    themmoi.kq5 = b.KetQua;
                                                    break;
                                                case "6":
                                                    themmoi.kq6 = b.KetQua;
                                                    break;
                                                case "7":
                                                    themmoi.kq7 = b.KetQua;
                                                    break;
                                                case "8":
                                                    themmoi.kq8 = b.KetQua;
                                                    break;
                                                case "9":
                                                    themmoi.kq9 = b.KetQua;
                                                    break;
                                                case "10":
                                                    themmoi.kq10 = b.KetQua;
                                                    break;
                                                case "11":
                                                    themmoi.kq11 = b.KetQua;
                                                    break;
                                                case "12":
                                                    themmoi.kq12 = b.KetQua;
                                                    break;
                                                case "13":
                                                    themmoi.kq13 = b.KetQua;
                                                    break;
                                                case "14":
                                                    themmoi.kq14 = b.KetQua;
                                                    break;
                                                case "15":
                                                    themmoi.kq15 = b.KetQua;
                                                    break;
                                                case "16":
                                                    themmoi.kq16 = b.KetQua;
                                                    break;
                                                case "17":
                                                    themmoi.kq17 = b.KetQua;
                                                    break;
                                                case "18":
                                                    themmoi.kq18 = b.KetQua;
                                                    break;
                                                case "19":
                                                    themmoi.kq19 = b.KetQua;
                                                    break;
                                                case "20":
                                                    themmoi.kq20 = b.KetQua;
                                                    break;
                                                case "21":
                                                    themmoi.kq21 = b.KetQua;
                                                    break;
                                                case "22":
                                                    themmoi.kq22 = b.KetQua;
                                                    break;
                                                case "23":
                                                    themmoi.kq23 = b.KetQua;
                                                    break;
                                                case "24":
                                                    themmoi.kq24 = b.KetQua;
                                                    break;
                                                case "25":
                                                    themmoi.kq25 = b.KetQua;
                                                    break;
                                                case "26":
                                                    themmoi.kq26 = b.KetQua;
                                                    break;
                                                case "27":
                                                    themmoi.kq27 = b.KetQua;
                                                    break;
                                                case "28":
                                                    themmoi.kq28 = b.KetQua;
                                                    break;
                                                case "29":
                                                    themmoi.kq29 = b.KetQua;
                                                    break;
                                                case "30":
                                                    themmoi.kq30 = b.KetQua;
                                                    break;
                                            }
                                        }
                                    }
                                }
                                #endregion
                                
                                _Baocao.Add(themmoi);
                            }
                        }
                    }
                    if (_Baocao.Count > 0)
                    {
                        BaoCao.Rep_TongHopHoaSinhMau rep = new BaoCao.Rep_TongHopHoaSinhMau(arr);
                        frmIn frm = new frmIn();
                        rep.DataSource = _Baocao.OrderBy(p => p.ngaythang);
                        rep.BindingData();
                        rep.CreateDocument();
                        _Baocao.Clear();
                        //rep.DataMember = "Table";
                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                        frm.ShowDialog();
                    }
                }
            }
            if (cboTT.SelectedIndex == 0)// đã thực hiện
            {
                var canlamsang5 = (from cls in _Data.CLS.Where(p => p.NgayTH >= NT).Where(p => p.NgayTH <= ND)
                                  join cd in _Data.ChiDinhs.Where(p=>p.Status==1) on cls.IdCLS equals cd.IdCLS
                                  join clsct in _Data.CLScts.Where(p=>p.Status==1) on cd.IDCD equals clsct.IDCD
                                  join dv in _Data.DichVus on cd.MaDV equals dv.MaDV
                                  join tn in _Data.TieuNhomDVs.Where(p => p.TenRG.ToLower().Contains(_tenrg.ToLower())) on dv.IdTieuNhom equals tn.IdTieuNhom
                                  //join kp in _Kphong.Where(p => p.chon = true) on cls.MaKP equals kp.makp
                                  //where (cls.MaKP == _MaKP1 || cls.MaKP == _MaKP10 || cls.MaKP == _MaKP11 || cls.MaKP == _MaKP12 || cls.MaKP == _MaKP13 || cls.MaKP == _MaKP14 || cls.MaKP == _MaKP15 || cls.MaKP == _MaKP2 || cls.MaKP == _MaKP3 || cls.MaKP == _MaKP4 || cls.MaKP == _MaKP5 || cls.MaKP == _MaKP6 || cls.MaKP == _MaKP7 || cls.MaKP == _MaKP8 || cls.MaKP == _MaKP9 || cls.MaKP == _MaKP16 || cls.MaKP == _MaKP17 || cls.MaKP == _MaKP18 || cls.MaKP == _MaKP19 || cls.MaKP == _MaKP20 || cls.MaKP == _MaKP21 || cls.MaKP == _MaKP22 || cls.MaKP == _MaKP23 || cls.MaKP == _MaKP24 || cls.MaKP == _MaKP25 || cls.MaKP == _MaKP26 || cls.MaKP == _MaKP27 || cls.MaKP == _MaKP28)
                                  group new { cls } by new { cls.IdCLS, cls.MaBNhan, cls.MaKP } into kq
                                  select new { IDCLS = kq.Key.IdCLS, MaBNhan=kq.Key.MaBNhan, kq.Key.MaKP }).ToList();
                var canlamsang= (from cl in canlamsang5
                                join  kp in _Kphong.Where(p => p.chon = true) on cl.MaKP equals kp.makp
                                group new { cl } by new { cl.IDCLS, cl.MaBNhan } into kq
                                  select new { IDCLS = kq.Key.IDCLS, MaBNhan=kq.Key.MaBNhan }).ToList();
                string[] arr = new string[50];
                for (int i = 0; i < 50; i++)
                {
                    arr[i] = "";
                }
                var madvct = (from clsct in _Data.CLScts
                              join dvct in _Data.DichVucts on clsct.MaDVct equals dvct.MaDVct
                              join dv in _Data.DichVus on dvct.MaDV equals dv.MaDV
                              join tn in _Data.TieuNhomDVs.Where(p => p.TenRG.ToLower().Contains(_tenrg.ToLower())) on dv.IdTieuNhom equals tn.IdTieuNhom
                              group new { clsct, dvct } by new { clsct.MaDVct, dvct.MaDV } into kq
                              select new { kq.Key.MaDVct, kq.Key.MaDV, }).OrderBy(p => p.MaDV).ToList();
                int k = 0;
                foreach (var c in madvct)
                {

                    arr[k] = c.MaDVct;
                    k++;
                }
                if (canlamsang.Count > 0)
                {
                        foreach (var a in canlamsang)
                        {
                            var benhnhan5 = (from bn in _Data.BenhNhans
                                            join cls in _Data.CLS.Where(p => p.IdCLS == a.IDCLS) on bn.MaBNhan equals cls.MaBNhan
                                            //join kp1 in _Kphong.Where(p => p.chon = true) on cls.MaKP equals kp1.makp
                                            //where (cls.MaKP == _MaKP1 || cls.MaKP == _MaKP10 || cls.MaKP == _MaKP11 || cls.MaKP == _MaKP12 || cls.MaKP == _MaKP13 || cls.MaKP == _MaKP14 || cls.MaKP == _MaKP15 || cls.MaKP == _MaKP2 || cls.MaKP == _MaKP3 || cls.MaKP == _MaKP4 || cls.MaKP == _MaKP5 || cls.MaKP == _MaKP6 || cls.MaKP == _MaKP7 || cls.MaKP == _MaKP8 || cls.MaKP == _MaKP9 || cls.MaKP == _MaKP16 || cls.MaKP == _MaKP17 || cls.MaKP == _MaKP18 || cls.MaKP == _MaKP19 || cls.MaKP == _MaKP20 || cls.MaKP == _MaKP21 || cls.MaKP == _MaKP22 || cls.MaKP == _MaKP23 || cls.MaKP == _MaKP24 || cls.MaKP == _MaKP25 || cls.MaKP == _MaKP26 || cls.MaKP == _MaKP27 || cls.MaKP == _MaKP28)
                                            join kp in _Data.KPhongs on bn.MaKP equals kp.MaKP
                                            select new { bn.TenBNhan, bn.GTinh, bn.Tuoi, bn.DChi, bn.DTuong, kp.TenKP, bn.MaBNhan, cls.MaKP }).ToList();

                            var benhnhan= (from bn in benhnhan5
                                          join kp1 in _Kphong.Where(p => p.chon = true) on bn.MaKP equals kp1.makp
                                          select new { bn.TenBNhan, bn.GTinh, bn.Tuoi, bn.DChi, bn.DTuong, bn.TenKP, bn.MaBNhan, bn.MaKP }).ToList();
                            if (benhnhan.Count > 0)
                            {
                                var hoasinh = (from chidinh in _Data.ChiDinhs.Where(p => p.IdCLS == a.IDCLS)
                                               join clsct in _Data.CLScts.Where(p => p.Status == 1) on chidinh.IDCD equals clsct.IDCD
                                               join dvct in _Data.DichVucts on clsct.MaDVct equals dvct.MaDVct
                                               select new { dvct.TenDVct, dvct.STT,dvct.MaDVct, clsct.KetQua}).OrderBy(p => p.STT).ToList();
                                if (hoasinh.Count > 0)
                                {
                                    Baocao themmoi = new Baocao();
                                    if (benhnhan.First().TenBNhan != null)
                                    {
                                        themmoi.tenBN = benhnhan.First().TenBNhan;
                                    }
                                    if (benhnhan.First().GTinh != null)
                                    {
                                        themmoi.gioitinh = benhnhan.First().GTinh.ToString();
                                    }
                                    if (benhnhan.First().Tuoi != null)
                                    {
                                        themmoi.tuoi = benhnhan.First().Tuoi.ToString();
                                    }
                                    if (benhnhan.First().DChi != null)
                                    {
                                        themmoi.diachi = benhnhan.First().DChi;
                                    }
                                    if (benhnhan.First().DTuong != null && benhnhan.First().DTuong == "BHYT")
                                    {
                                        themmoi.bhyt = "X";
                                    }
                                    if (benhnhan.First().TenKP != null)
                                    {
                                        themmoi.noigui = benhnhan.First().TenKP;
                                    }
                                    if (benhnhan.First().MaBNhan != null && benhnhan.First().MaKP != null)
                                    {
                                        int Mabn = benhnhan.First().MaBNhan;
                                        int MaKP = benhnhan.First().MaKP.Value;
                                        var bnkb5 = (from bn in _Data.BNKBs.Where(p => p.MaBNhan == Mabn).Where(p => p.MaKP == MaKP)
                                                    join cls in _Data.CLS on bn.MaBNhan equals cls.MaBNhan
                                                    //join kp in _Kphong.Where(p => p.chon = true) on cls.MaKP equals kp.makp
                                                    //where (cls.MaKP == _MaKP1 || cls.MaKP == _MaKP10 || cls.MaKP == _MaKP11 || cls.MaKP == _MaKP12 || cls.MaKP == _MaKP13 || cls.MaKP == _MaKP14 || cls.MaKP == _MaKP15 || cls.MaKP == _MaKP2 || cls.MaKP == _MaKP3 || cls.MaKP == _MaKP4 || cls.MaKP == _MaKP5 || cls.MaKP == _MaKP6 || cls.MaKP == _MaKP7 || cls.MaKP == _MaKP8 || cls.MaKP == _MaKP9 || cls.MaKP == _MaKP16 || cls.MaKP == _MaKP17 || cls.MaKP == _MaKP18 || cls.MaKP == _MaKP19 || cls.MaKP == _MaKP20 || cls.MaKP == _MaKP21 || cls.MaKP == _MaKP22 || cls.MaKP == _MaKP23 || cls.MaKP == _MaKP24 || cls.MaKP == _MaKP25 || cls.MaKP == _MaKP26 || cls.MaKP == _MaKP27 || cls.MaKP == _MaKP28)
                                                    select new { bn.ChanDoan, bn.IDKB, bn.MaKP }).OrderBy(p => p.IDKB).ToList();

                                        var bnkb= (from bn in bnkb5
                                                  join kp in _Kphong.Where(p=>p.chon==true) on bn.MaKP equals kp.makp
                                                  select new { bn.ChanDoan, bn.IDKB }).OrderBy(p => p.IDKB).ToList();
                                        if (bnkb.Count > 0 && bnkb.First().ChanDoan != null)
                                        {
                                            themmoi.chuandoan = bnkb.First().ChanDoan;
                                        }
                                    }
                                    #region cach2
                                    foreach (var b in hoasinh)
                                    {
                                        for (int i = 0; i < 50; i++)
                                        {
                                            if (arr[i] == b.MaDVct)
                                            {
                                                string STT = i.ToString();
                                                switch (STT)
                                                {
                                                    case "1":
                                                        themmoi.kq1 = b.KetQua;
                                                        break;
                                                    case "2":
                                                        themmoi.kq2 = b.KetQua;
                                                        break;
                                                    case "3":
                                                        themmoi.kq3 = b.KetQua;
                                                        break;
                                                    case "4":
                                                        themmoi.kq4 = b.KetQua;
                                                        break;
                                                    case "5":
                                                        themmoi.kq5 = b.KetQua;
                                                        break;
                                                    case "6":
                                                        themmoi.kq6 = b.KetQua;
                                                        break;
                                                    case "7":
                                                        themmoi.kq7 = b.KetQua;
                                                        break;
                                                    case "8":
                                                        themmoi.kq8 = b.KetQua;
                                                        break;
                                                    case "9":
                                                        themmoi.kq9 = b.KetQua;
                                                        break;
                                                    case "10":
                                                        themmoi.kq10 = b.KetQua;
                                                        break;
                                                    case "11":
                                                        themmoi.kq11 = b.KetQua;
                                                        break;
                                                    case "12":
                                                        themmoi.kq12 = b.KetQua;
                                                        break;
                                                    case "13":
                                                        themmoi.kq13 = b.KetQua;
                                                        break;
                                                    case "14":
                                                        themmoi.kq14 = b.KetQua;
                                                        break;
                                                    case "15":
                                                        themmoi.kq15 = b.KetQua;
                                                        break;
                                                    case "16":
                                                        themmoi.kq16 = b.KetQua;
                                                        break;
                                                    case "17":
                                                        themmoi.kq17 = b.KetQua;
                                                        break;
                                                    case "18":
                                                        themmoi.kq18 = b.KetQua;
                                                        break;
                                                    case "19":
                                                        themmoi.kq19 = b.KetQua;
                                                        break;
                                                    case "20":
                                                        themmoi.kq20 = b.KetQua;
                                                        break;
                                                    case "21":
                                                        themmoi.kq21 = b.KetQua;
                                                        break;
                                                    case "22":
                                                        themmoi.kq22 = b.KetQua;
                                                        break;
                                                    case "23":
                                                        themmoi.kq23 = b.KetQua;
                                                        break;
                                                    case "24":
                                                        themmoi.kq24 = b.KetQua;
                                                        break;
                                                    case "25":
                                                        themmoi.kq25 = b.KetQua;
                                                        break;
                                                    case "26":
                                                        themmoi.kq26 = b.KetQua;
                                                        break;
                                                    case "27":
                                                        themmoi.kq27 = b.KetQua;
                                                        break;
                                                    case "28":
                                                        themmoi.kq28 = b.KetQua;
                                                        break;
                                                    case "29":
                                                        themmoi.kq29 = b.KetQua;
                                                        break;
                                                    case "30":
                                                        themmoi.kq30 = b.KetQua;
                                                        break;
                                                }
                                            }
                                        }
                                    }
                                    #endregion
                                    
                                    _Baocao.Add(themmoi);
                                }
                            }
                        }
                    if (_Baocao.Count > 0)
                    {
                        BaoCao.Rep_TongHopHoaSinhMau rep = new BaoCao.Rep_TongHopHoaSinhMau(arr);
                        frmIn frm = new frmIn();
                        rep.DataSource = _Baocao.OrderBy(p => p.ngaythang);
                        rep.BindingData();
                        rep.CreateDocument();
                        _Baocao.Clear();
                        //rep.DataMember = "Table";
                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                        frm.ShowDialog();
                    }
                }
            }
            //if (cboTT.SelectedIndex == 2)// cả hai
            //{

            //    var canlamsang = (from cls in _Data.CLS.Where(p => p.NgayTH >= NT).Where(p => p.NgayTH <= ND)
            //                      //where (from vp in _Data.VienPhis select vp.MaBNhan).Contains(cls.MaBNhan)
            //                      join cd in _Data.ChiDinhs on cls.IdCLS equals cd.IdCLS
            //                      join clsct in _Data.CLScts on cd.IDCD equals clsct.IDCD
            //                      join dv in _Data.DichVus on cd.MaDV equals dv.MaDV
            //                      join tn in _Data.TieuNhomDVs.Where(p => p.TenRG.ToLower().Contains(_tenrg.ToLower())) on dv.IdTieuNhom equals tn.IdTieuNhom
            //                      where (cls.MaKP == _MaKP1 || cls.MaKP == _MaKP10 || cls.MaKP == _MaKP11 || cls.MaKP == _MaKP12 || cls.MaKP == _MaKP13 || cls.MaKP == _MaKP14 || cls.MaKP == _MaKP15 || cls.MaKP == _MaKP2 || cls.MaKP == _MaKP3 || cls.MaKP == _MaKP4 || cls.MaKP == _MaKP5 || cls.MaKP == _MaKP6 || cls.MaKP == _MaKP7 || cls.MaKP == _MaKP8 || cls.MaKP == _MaKP9)
            //                      group new { cls } by new { cls.IdCLS } into kq
            //                      select new { IDCLS = kq.Key.IdCLS }).ToList();
            //    string[] arr = new string[50];
            //    for (int i = 0; i < 50; i++)
            //    {
            //        arr[i] = "";
            //    }
            //    var madvct = (from clsct in _Data.CLScts
            //                  join dvct in _Data.DichVucts on clsct.MaDVct equals dvct.MaDVct
            //                  join dv in _Data.DichVus on dvct.MaDV equals dv.MaDV
            //                  join tn in _Data.TieuNhomDVs.Where(p => p.TenRG.ToLower().Contains(_tenrg.ToLower())) on dv.IdTieuNhom equals tn.IdTieuNhom
            //                  group new { clsct, dvct } by new { clsct.MaDVct, dvct.MaDV } into kq
            //                  select new { kq.Key.MaDVct, kq.Key.MaDV, }).OrderBy(p => p.MaDV).ToList();
            //    int k = 0;
            //    foreach (var c in madvct)
            //    {

            //        arr[k] = c.MaDVct;
            //        k++;
            //    }
            //    if (canlamsang.Count > 0)
            //    {
            //        foreach (var a in canlamsang)
            //        {
            //            var benhnhan = (from bn in _Data.BenhNhans
            //                            join cls in _Data.CLS.Where(p => p.IdCLS == a.IDCLS) on bn.MaBNhan equals cls.MaBNhan
            //                            where (cls.MaKP == _MaKP1 || cls.MaKP == _MaKP10 || cls.MaKP == _MaKP11 || cls.MaKP == _MaKP12 || cls.MaKP == _MaKP13 || cls.MaKP == _MaKP14 || cls.MaKP == _MaKP15 || cls.MaKP == _MaKP2 || cls.MaKP == _MaKP3 || cls.MaKP == _MaKP4 || cls.MaKP == _MaKP5 || cls.MaKP == _MaKP6 || cls.MaKP == _MaKP7 || cls.MaKP == _MaKP8 || cls.MaKP == _MaKP9)
            //                            join kp in _Data.KPhongs on bn.MaKP equals kp.MaKP
            //                            select new { bn.TenBNhan, bn.GTinh, bn.Tuoi, bn.DChi, bn.DTuong, kp.TenKP, bn.MaBNhan, cls.MaKP }).ToList();

            //            if (benhnhan.Count > 0)
            //            {
            //                var hoasinh = (from chidinh in _Data.ChiDinhs.Where(p => p.IdCLS == a.IDCLS)
            //                               join clsct in _Data.CLScts.Where(p => p.Status == 1) on chidinh.IDCD equals clsct.IDCD
            //                               join dvct in _Data.DichVucts on clsct.MaDVct equals dvct.MaDVct
            //                               select new { dvct.TenDVct, dvct.STT,dvct.MaDVct, clsct.KetQua}).OrderBy(p => p.STT).ToList();
            //                if (hoasinh.Count > 0)
            //                {
            //                    Baocao themmoi = new Baocao();
            //                    if (benhnhan.First().TenBNhan != null)
            //                    {
            //                        themmoi.tenBN = benhnhan.First().TenBNhan;
            //                    }
            //                    if (benhnhan.First().GTinh != null)
            //                    {
            //                        themmoi.gioitinh = benhnhan.First().GTinh.ToString();
            //                    }
            //                    if (benhnhan.First().Tuoi != null)
            //                    {
            //                        themmoi.tuoi = benhnhan.First().Tuoi.ToString();
            //                    }
            //                    if (benhnhan.First().DChi != null)
            //                    {
            //                        themmoi.diachi = benhnhan.First().DChi;
            //                    }
            //                    if (benhnhan.First().DTuong != null && benhnhan.First().DTuong == "BHYT")
            //                    {
            //                        themmoi.bhyt = "X";
            //                    }
            //                    if (benhnhan.First().TenKP != null)
            //                    {
            //                        themmoi.noigui = benhnhan.First().TenKP;
            //                    }
            //                    if (benhnhan.First().MaBNhan != null && benhnhan.First().MaKP != null)
            //                    {
            //                        string Mabn = benhnhan.First().MaBNhan;
            //                        string MaKP = benhnhan.First().MaKP;
            //                        var bnkb = (from bn in _Data.BNKBs.Where(p => p.MaBNhan == Mabn).Where(p => p.MaKP == MaKP) select new { bn.ChanDoan }).ToList();
            //                        if (bnkb.Count > 0 && bnkb.First().ChanDoan != null)
            //                        {
            //                            themmoi.chuandoan = bnkb.First().ChanDoan;
            //                        }
            //                    }
            //                    #region cach2
            //                    foreach (var b in hoasinh)
            //                    {
            //                        for (int i = 0; i < 50; i++)
            //                        {
            //                            if (arr[i] == b.MaDVct)
            //                            {
            //                                string STT = i.ToString();
            //                                switch (STT)
            //                                {
            //                                    case "1":
            //                                        themmoi.kq1 = b.KetQua;
            //                                        break;
            //                                    case "2":
            //                                        themmoi.kq2 = b.KetQua;
            //                                        break;
            //                                    case "3":
            //                                        themmoi.kq3 = b.KetQua;
            //                                        break;
            //                                    case "4":
            //                                        themmoi.kq4 = b.KetQua;
            //                                        break;
            //                                    case "5":
            //                                        themmoi.kq5 = b.KetQua;
            //                                        break;
            //                                    case "6":
            //                                        themmoi.kq6 = b.KetQua;
            //                                        break;
            //                                    case "7":
            //                                        themmoi.kq7 = b.KetQua;
            //                                        break;
            //                                    case "8":
            //                                        themmoi.kq8 = b.KetQua;
            //                                        break;
            //                                    case "9":
            //                                        themmoi.kq9 = b.KetQua;
            //                                        break;
            //                                    case "10":
            //                                        themmoi.kq10 = b.KetQua;
            //                                        break;
            //                                    case "11":
            //                                        themmoi.kq11 = b.KetQua;
            //                                        break;
            //                                    case "12":
            //                                        themmoi.kq12 = b.KetQua;
            //                                        break;
            //                                    case "13":
            //                                        themmoi.kq13 = b.KetQua;
            //                                        break;
            //                                    case "14":
            //                                        themmoi.kq14 = b.KetQua;
            //                                        break;
            //                                    case "15":
            //                                        themmoi.kq15 = b.KetQua;
            //                                        break;
            //                                    case "16":
            //                                        themmoi.kq16 = b.KetQua;
            //                                        break;
            //                                    case "17":
            //                                        themmoi.kq17 = b.KetQua;
            //                                        break;
            //                                    case "18":
            //                                        themmoi.kq18 = b.KetQua;
            //                                        break;
            //                                    case "19":
            //                                        themmoi.kq19 = b.KetQua;
            //                                        break;
            //                                    case "20":
            //                                        themmoi.kq20 = b.KetQua;
            //                                        break;
            //                                    case "21":
            //                                        themmoi.kq21 = b.KetQua;
            //                                        break;
            //                                    case "22":
            //                                        themmoi.kq22 = b.KetQua;
            //                                        break;
            //                                    case "23":
            //                                        themmoi.kq23 = b.KetQua;
            //                                        break;
            //                                    case "24":
            //                                        themmoi.kq24 = b.KetQua;
            //                                        break;
            //                                    case "25":
            //                                        themmoi.kq25 = b.KetQua;
            //                                        break;
            //                                    case "26":
            //                                        themmoi.kq26 = b.KetQua;
            //                                        break;
            //                                    case "27":
            //                                        themmoi.kq27 = b.KetQua;
            //                                        break;
            //                                    case "28":
            //                                        themmoi.kq28 = b.KetQua;
            //                                        break;
            //                                    case "29":
            //                                        themmoi.kq29 = b.KetQua;
            //                                        break;
            //                                    case "30":
            //                                        themmoi.kq30 = b.KetQua;
            //                                        break;
            //                                }
            //                            }
            //                        }
            //                    }
            //                    #endregion
                                
            //                    _Baocao.Add(themmoi);
            //                }
            //            }
            //        }
            //        if (_Baocao.Count > 0)
            //        {
            //            BaoCao.Rep_TongHopHoaSinhMau rep = new BaoCao.Rep_TongHopHoaSinhMau(arr);
            //            frmIn frm = new frmIn();
            //            rep.DataSource = _Baocao.OrderBy(p => p.ngaythang);
            //            rep.BindingData();
            //            rep.CreateDocument();
            //            _Baocao.Clear();
            //            //rep.DataMember = "Table";
            //            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            //            frm.ShowDialog();
            //        }
            //    }
            //}

        }
        QLBV_Database.QLBVEntities _Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void Frm_repTonghopHoaSinh_Load(object sender, EventArgs e)
        {
            LupNgaytu.DateTime = System.DateTime.Now;
            LupNgayden.DateTime = System.DateTime.Now;
            _Kphong.Clear();
            var kphong = (from kp in _Data.KPhongs
                          where (kp.PLoai == "Lâm sàng" || kp.PLoai == "Phòng khám")
                          select new { kp.TenKP, kp.MaKP }).ToList();
            if (kphong.Count > 0)
            {
                KPhong themmoi1 = new KPhong();
                themmoi1.tenkp = "Chọn tất cả";
                themmoi1.makp = 0;
                themmoi1.chon = false;
                _Kphong.Add(themmoi1);
                foreach (var a in kphong)
                {
                    KPhong themmoi = new KPhong();
                    themmoi.tenkp = a.TenKP;
                    themmoi.makp = a.MaKP;
                    themmoi.chon = false;
                    _Kphong.Add(themmoi);
                }
                grcKhoaphong.DataSource = _Kphong.ToList();
            }
            cboTT.SelectedIndex = 0;
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