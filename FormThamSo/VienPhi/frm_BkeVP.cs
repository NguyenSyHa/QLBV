using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.FormThamSo
{
    public partial class frm_BkeVP : DevExpress.XtraEditors.XtraForm
    {
        public frm_BkeVP()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities data;
        List<KPhong> _lKPhong = new List<KPhong>();
        private void frm_BkeVienPhi_Load(object sender, EventArgs e)
        {
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            dtTungay.DateTime = System.DateTime.Now;
            dtDenNgay.DateTime = System.DateTime.Now;
            //DataTable tbDoiTuong = new DataTable();
            //tbDoiTuong.Columns.Add("TenDoiTuong", typeof(string));
            //tbDoiTuong.Columns.Add("MaDoiTuong", typeof(string));
            //tbDoiTuong.Rows.Add("Tất cả", "tc");
            //tbDoiTuong.Rows.Add("BHYT", "BHYT");
            //tbDoiTuong.Rows.Add("Dịch vụ", "Dịch vụ");
            //tbDoiTuong.Rows.Add("KSK", "KSK");
            //lupDoiTuong.Properties.DataSource = tbDoiTuong;
            //_lKPhong = data.KPhongs.OrderBy(p => p.TenKP).ToList();
            //_lKPhong.Insert(0, new KPhong { MaKP = "tc", TenKP = "Tất cả" });
            radioGroup1.SelectedIndex = 0;


        }
        public DataTable tb;
        private class BC
        {
            private int _idvp, _tuoi, _idnhom, _idtieunhom, _trongbh, _noitru, _phanloai,_mabn;
            private string _tentn,  _gt, _dchi, _dtuong, _tenbn;
            private double _soluong, _thanhtien, _tienbn, _sotien;
            private string _ngaythu;
            public string TenBN
            {
                set { _tenbn = value; }
                get { return _tenbn; }
            }
            public string NgayThu
            {
                set { _ngaythu = value; }
                get { return _ngaythu; }
            }
            public int idVPhi
            {
                set { _idvp = value; }
                get { return _idvp; }
            }
            public int Tuoi
            { set { _tuoi = value; }
                get { return _tuoi; }
            }
            public int IDNhom
            {
                set { _idnhom = value; }
                get { return _idnhom; }
            }
            public int IDTieuNhom
            {
                set { _idtieunhom = value; }
                get { return _idtieunhom; }
            }
            public int TrongBH
            {
                set { _trongbh = value; }
                get { return _trongbh; }
            }
            public int NoiTru
            {
                set { _noitru = value; }
                get { return _noitru; }
            }
            public int PhanLoai
            {
                set { _phanloai = value; }
                get { return _phanloai; }
            }
            public string TenTN
            {
                set { _tentn = value; }
                get { return _tentn; }
            }
            public int MaBNhan
            {
                set { _mabn = value; }
                get { return _mabn; }
            }
            public string GTinh
            {
                set { _gt = value; }
                get { return _gt; }
            }
            public string DChi
            {
                set { _dchi = value; }
                get { return _dchi; }
            }
            public string DTuong
            {
                set { _dtuong = value; }
                get { return _dtuong; }
            }
            public double SoLuong
            {
                set { _soluong = value; }
                get { return _soluong; }
            }
            public double ThanhTien
            {
                set { _thanhtien = value; }
                get { return _thanhtien; }
            }
            public double TienBN
            {
                set { _tienbn = value; }
                get { return _tienbn; }
            }
            public double SoTien
            {
                set { _sotien = value; }
                get { return _sotien; }
            }
        }
        private class Ten
        {
            private string _tentn;
            private int _idtieunhom, _idnhom, _stt;
            public int STT
            { set { _stt = value; } get { return _stt; } }
            public string TenTN
            {
                set { _tentn = value; }
                get { return _tentn; }
            }
            public int IDTieuNhom
            {
                set { _idtieunhom = value; }
                get { return _idtieunhom; }
            }
            public int IDNhom
            {
                set { _idnhom = value; }
                get { return _idnhom; }
            }
        }
        List<BC> _BC = new List<BC>();
        List<Ten> _Ten = new List<Ten>();
        private void btnInBC_Click(object sender, EventArgs e)
        {

            DateTime tungay = DungChung.Ham.NgayTu(dtTungay.DateTime);
            DateTime denngay = DungChung.Ham.NgayDen(dtDenNgay.DateTime);
            string doituong = "tc";
            string khoaphong = "tc";
            int noingoaitru = -1;
            _BC.Clear();
            _Ten.Clear();
            
            //if (lupDoiTuong.EditValue != null)
            //    doituong = lupDoiTuong.EditValue.ToString();
            if (radioGroup1.SelectedIndex == 0)// tất cả
                noingoaitru = -1;
            else if (radioGroup1.SelectedIndex == 1)// nội trú
                noingoaitru = 1;
            else if (radioGroup1.SelectedIndex == 2)// ngoại trú
                noingoaitru = 0;
            if (!chkIn.Checked)
            {
                frmIn frm = new frmIn();
                BaoCao.rep_BkeVP rep = new BaoCao.rep_BkeVP();
                data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

                #region add para baocao
                int idNhomthuoc = 0;
                var nt = data.NhomDVs.Where(p => p.TenNhom== ("Thuốc, Dịch truyền"));
                if (nt.Count() > 0)
                {
                    idNhomthuoc = nt.ToList().First().IDNhom;
                }

                rep.Ngay.Value = "Từ ngày: " + dtTungay.Text + "  đến ngày: " + dtDenNgay.Text;
                rep.TenCQ.Value = DungChung.Bien.TenCQ.ToUpper();
                rep.TieuDe.Value = ("Bảng kê thu viện phí").ToUpper();
                #endregion
                //int trongdm = 1, ngoaidm = 0;
                //if (cbo_trongDM.SelectedIndex != 2) {
                //    trongdm = cbo_trongDM.SelectedIndex;
                //    ngoaidm = cbo_trongDM.SelectedIndex;
                //}

                // lấy tiền thu viện phí (thu chi thanh toán)
                var all2 = (from bn in data.BenhNhans
                            join vp in data.VienPhis on bn.MaBNhan equals vp.MaBNhan
                            join vpct in data.VienPhicts on vp.idVPhi equals vpct.idVPhi
                            join dv in data.DichVus on vpct.MaDV equals dv.MaDV
                            join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                            join tu in data.TamUngs on vpct.IDTamUng equals tu.IDTamUng
                            //where (vpct.TrongBH == trongdm || vpct.TrongBH == ngoaidm)
                            orderby vp.MaBNhan, dv.IDNhom
                            select new
                            {
                                tn.STT,
                                vpct.idVPhict,
                                tn.TenTN,
                                bn.MaBNhan,
                                bn.TenBNhan,
                                bn.GTinh,
                                bn.Tuoi,
                                bn.DChi,
                                dv.IDNhom,
                                dv.IdTieuNhom,
                                vpct.SoLuong,
                                vpct.ThanhTien,
                                vpct.TienBN,
                                //vp.NgayTT,
                                bn.DTuong,
                                vpct.TrongBH,
                                bn.NoiTru,
                                tu.PhanLoai,
                                tu.NgayThu,
                                tu.SoTien, tu.IDTamUng,
                            }).ToList();
                var all = (from a in all2
                           select new
                           {
                               a.IDTamUng,
                               a.idVPhict,
                               a.TenTN,
                               a.MaBNhan,
                               a.TenBNhan,
                               a.GTinh,
                               a.Tuoi,
                               a.STT,
                               a.DChi,
                               a.IDNhom,
                               a.IdTieuNhom,
                               a.SoLuong,
                               a.ThanhTien,
                               a.TienBN,
                               //a.NgayTT,
                               a.DTuong,
                               a.TrongBH,
                               a.NoiTru,
                               a.PhanLoai,
                               a.NgayThu,
                               a.SoTien
                           }).Where(p => p.NgayThu >= tungay && p.NgayThu <= denngay)
                         .Where(p => p.PhanLoai == 1 || p.PhanLoai == 2)
                    //.Where(p => doituong == "tc" || p.DTuong == doituong)
                         .Where(p => noingoaitru == -1 || p.NoiTru == noingoaitru).OrderBy(p=>p.IDTamUng)
                         .ToList();

                //var tn1= from cd in data.ChiDinhs
                //        join dv in data.DichVus

                int stt = 1;
                // lấy tiền thu thẳng
                var all3 = (from bn in data.BenhNhans
                            join cls in data.CLS on bn.MaBNhan equals cls.MaBNhan
                            join cd in data.ChiDinhs.Where(p => p.SoPhieu != null) on cls.IdCLS equals cd.IdCLS
                            join dv in data.DichVus on cd.MaDV equals dv.MaDV
                            join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                            join tu in data.TamUngs.Where(p => p.PhanLoai == 3) on bn.MaBNhan equals tu.MaBNhan
                            //where!(from vp in data.VienPhis select vp.MaBNhan).Contains(bn.MaBNhan)
                            orderby bn.MaBNhan, dv.IDNhom
                            select new
                            {
                                tu.IDTamUng,
                                tn.STT,
                                cd.IDCD,
                                tn.TenTN,
                                bn.MaBNhan,
                                bn.TenBNhan,
                                bn.GTinh,
                                bn.Tuoi,
                                bn.DChi,
                                dv.IDNhom,
                                dv.IdTieuNhom,
                                SoLuong = 1,
                                dv.DonGia,
                                TienBN = dv.DonGia,
                                //vp.NgayTT,
                                bn.DTuong,
                                //vpct.TrongBH,
                                bn.NoiTru,
                                tu.PhanLoai,
                                tu.NgayThu,
                                tu.SoTien
                            }).ToList();
                var all4 = (from a in all3
                            select new
                            {
                                a.IDTamUng,
                                idVPhict = a.IDCD,
                                a.TenTN,
                                a.STT,
                                a.MaBNhan,
                                a.TenBNhan,
                                a.GTinh,
                                a.Tuoi,
                                a.DChi,
                                a.IDNhom,
                                a.IdTieuNhom,
                                a.SoLuong,
                                //a.ThanhTien,
                                ThanhTien = 0,
                                a.TienBN,
                                //a.NgayTT,
                                a.DTuong,
                                //a.TrongBH,
                                TrongBH = 1,
                                a.NoiTru,
                                a.PhanLoai,
                                a.NgayThu,
                                a.SoTien,
                            }).Where(p => p.PhanLoai == 3)
                           .Where(p => p.NgayThu >= tungay && p.NgayThu <= denngay)
                    //.Where(p => doituong == "tc" || p.DTuong == doituong)
                         .Where(p => noingoaitru == -1 || p.NoiTru == noingoaitru).OrderBy(p=>p.IDTamUng)
                         .ToList();

                // lấy tiền công khám
                var all5 = (from bn in data.BenhNhans
                            join dt in data.DThuocs on bn.MaBNhan equals dt.MaBNhan
                            join dtct in data.DThuoccts.Where(p => p.IDCD != null) on dt.IDDon equals dtct.IDDon
                            join dv in data.DichVus on dtct.MaDV equals dv.MaDV
                            join nhom in data.NhomDVs.Where(p => p.TenNhomCT== ("Khám bệnh")) on dv.IDNhom equals nhom.IDNhom
                            join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                            join tu in data.TamUngs.Where(p => p.PhanLoai == 3) on dtct.IDCD equals tu.IDTamUng
                            //where!(from vp in data.VienPhis select vp.MaBNhan).Contains(bn.MaBNhan)
                            orderby bn.MaBNhan, dv.IDNhom
                            select new
                            {
                                tu.IDTamUng,
                                tn.STT,
                                dtct.IDCD,
                                tn.TenTN,
                                bn.MaBNhan,
                                bn.TenBNhan,
                                bn.GTinh,
                                bn.Tuoi,
                                bn.DChi,
                                dv.IDNhom,
                                dv.IdTieuNhom,
                                SoLuong = 1,
                                dv.DonGia,
                                TienBN = dv.DonGia,
                                //vp.NgayTT,
                                bn.DTuong,
                                //vpct.TrongBH,
                                bn.NoiTru,
                                tu.PhanLoai,
                                tu.NgayThu,
                                tu.SoTien
                            }).ToList();
                var all6 = (from a in all5
                            select new
                            {
                                a.IDTamUng,
                                idVPhict = a.IDCD,
                                a.TenTN,
                                a.MaBNhan,
                                a.STT,
                                a.TenBNhan,
                                a.GTinh,
                                a.Tuoi,
                                a.DChi,
                                a.IDNhom,
                                a.IdTieuNhom,
                                a.SoLuong,
                                //a.ThanhTien,
                                ThanhTien = 0,
                                a.TienBN,
                                //a.NgayTT,
                                a.DTuong,
                                //a.TrongBH,
                                TrongBH = 1,
                                a.NoiTru,
                                a.PhanLoai,
                                a.NgayThu,
                                a.SoTien,
                            }).Where(p => p.PhanLoai == 3)
                           .Where(p => p.NgayThu >= tungay && p.NgayThu <= denngay)
                    //.Where(p => doituong == "tc" || p.DTuong == doituong)
                         .Where(p => noingoaitru == -1 || p.NoiTru == noingoaitru).OrderBy(p=>p.IDTamUng)
                         .ToList();
                var all7 = (from bn in data.BenhNhans.Where(p => p.DTuong== ("KSK"))
                            join tu in data.TamUngs on bn.MaBNhan equals tu.MaBNhan
                            where (tu.NgayThu >= tungay && tu.NgayThu <= denngay)
                            select new {tu.IDTamUng, bn.TenBNhan, bn.MaBNhan, tu.SoTien, bn.DTuong, tu.NgayThu, bn.DChi, bn.GTinh, bn.NoiTru, bn.Tuoi }).OrderBy(p=>p.IDTamUng).ToList();
                if (all7.Count > 0)
                {
                    foreach (var a7 in all7)
                    {
                        BC themmoi = new BC();
                        themmoi.DChi = a7.DChi;
                        themmoi.DTuong = a7.DTuong;
                        if (a7.GTinh == 1)
                        {
                            themmoi.GTinh = "Nam";
                        }
                        else
                        { themmoi.GTinh = "Nữ"; }
                        themmoi.IDNhom = 0;                       
                        themmoi.IDTieuNhom = 0;
                        themmoi.idVPhi = 0;
                        themmoi.MaBNhan = a7.MaBNhan ;
                        themmoi.NoiTru = a7.NoiTru.Value;
                        themmoi.NgayThu = a7.NgayThu.Value.Day + "/" + a7.NgayThu.Value.Month;
                        themmoi.PhanLoai = 1;
                        themmoi.SoLuong = 1;
                        themmoi.SoTien = a7.SoTien.Value;
                        themmoi.TenTN = "";
                        themmoi.TenBN = a7.TenBNhan;
                        themmoi.TienBN = 0;
                        themmoi.Tuoi = a7.Tuoi.Value;
                        themmoi.ThanhTien = 0;
                        themmoi.TrongBH = 1;
                        _BC.Add(themmoi);
                    }
                }
                // add chi phí thu chi thanh toán
                foreach (var c1 in all)
                {
                    BC themmoi = new BC();
                    themmoi.DChi = c1.DChi;
                    themmoi.DTuong = c1.DTuong;
                    if (c1.GTinh == 1)
                    {
                        themmoi.GTinh = "Nam";
                    }
                    else
                    { themmoi.GTinh = "Nữ"; }
                    themmoi.IDNhom = c1.IDNhom.Value;
                    themmoi.IDTieuNhom = c1.IdTieuNhom.Value;
                    themmoi.idVPhi = c1.idVPhict;
                    themmoi.MaBNhan = c1.MaBNhan;
                    themmoi.NoiTru = c1.NoiTru.Value;
                    themmoi.NgayThu = c1.NgayThu.Value.Day + "/" + c1.NgayThu.Value.Month;
                    themmoi.PhanLoai = c1.PhanLoai.Value;
                    themmoi.SoLuong = c1.SoLuong;
                    themmoi.SoTien = c1.SoTien.Value;
                    themmoi.TenTN = c1.TenTN;
                    themmoi.TenBN = c1.TenBNhan;
                    if (c1.TienBN != null)
                    {
                        themmoi.TienBN = c1.TienBN;
                    }
                    else
                    {
                        themmoi.TienBN = 0;
                    }
                    themmoi.Tuoi = c1.Tuoi.Value;
                    themmoi.ThanhTien = c1.ThanhTien;
                    themmoi.TrongBH = c1.TrongBH;
                    _BC.Add(themmoi);
                }
                // add chi phí thu trực tiếp
                foreach (var d in all4)
                {
                    BC themmoi = new BC();
                    themmoi.DChi = d.DChi;
                    themmoi.DTuong = d.DTuong;
                    if (d.GTinh == 1)
                    {
                        themmoi.GTinh = "Nam";
                    }
                    else
                    { themmoi.GTinh = "Nữ"; }
                    themmoi.IDNhom = d.IDNhom.Value;
                    themmoi.IDTieuNhom = d.IdTieuNhom.Value;
                    themmoi.idVPhi = d.idVPhict;
                    themmoi.MaBNhan = d.MaBNhan;
                    themmoi.NoiTru = d.NoiTru.Value;
                    themmoi.NgayThu = d.NgayThu.Value.Day + "/" + d.NgayThu.Value.Month;
                    themmoi.PhanLoai = d.PhanLoai.Value;
                    themmoi.SoLuong = d.SoLuong;
                    themmoi.SoTien = d.SoTien.Value;
                    themmoi.TenTN = d.TenTN;
                    themmoi.TenBN = d.TenBNhan;
                    if (d.TienBN != null)
                    {
                        themmoi.TienBN = d.TienBN;
                    }
                    else
                    { themmoi.TienBN = 0; }
                    themmoi.Tuoi = d.Tuoi.Value;
                    themmoi.ThanhTien = d.ThanhTien;
                    themmoi.TrongBH = d.TrongBH;
                    _BC.Add(themmoi);
                }
                // add công khám thu trực tiếp
                foreach (var c5 in all6)
                {
                    BC themmoi = new BC();
                    themmoi.DChi = c5.DChi;
                    themmoi.DTuong = c5.DTuong;
                    if (c5.GTinh == 1)
                    {
                        themmoi.GTinh = "Nam";
                    }
                    else
                    { themmoi.GTinh = "Nữ"; }
                    themmoi.IDNhom = c5.IDNhom.Value;
                    themmoi.IDTieuNhom = c5.IdTieuNhom.Value;
                    themmoi.idVPhi = c5.idVPhict.Value;
                    themmoi.MaBNhan = c5.MaBNhan;
                    themmoi.NoiTru = c5.NoiTru.Value;
                    themmoi.NgayThu = c5.NgayThu.Value.Day + "/" + c5.NgayThu.Value.Month;
                    themmoi.PhanLoai = c5.PhanLoai.Value;
                    themmoi.SoLuong = c5.SoLuong;
                    themmoi.SoTien = c5.SoTien.Value;
                    themmoi.TenTN = c5.TenTN;
                    themmoi.TenBN = c5.TenBNhan;
                    if (c5.TienBN != null)
                    {
                        themmoi.TienBN = c5.TienBN;
                    }
                    else
                    { themmoi.TienBN = 0; }
                    themmoi.Tuoi = c5.Tuoi.Value;
                    themmoi.ThanhTien = c5.ThanhTien;
                    themmoi.TrongBH = c5.TrongBH;
                    _BC.Add(themmoi);
                }
                var tnhom = (from a in all where a.IDNhom != idNhomthuoc select new { a.TenTN, a.IdTieuNhom, a.IDNhom, a.STT }).OrderBy(p => p.IDNhom).Distinct().ToList();
                var tnhom1 = (from a in all4 where a.IDNhom != idNhomthuoc select new { a.TenTN, a.IdTieuNhom, a.IDNhom, a.STT }).OrderBy(p => p.IDNhom).Distinct().ToList();
                foreach (var b1 in tnhom)
                {
                    Ten themmoi = new Ten();
                    themmoi.IDNhom = b1.IDNhom.Value;
                    if (b1.STT != null)
                    {
                        themmoi.STT = b1.STT.Value;
                    }
                    themmoi.IDTieuNhom = b1.IdTieuNhom.Value;
                    themmoi.TenTN = b1.TenTN;
                    _Ten.Add(themmoi);
                }
                foreach (var b2 in tnhom1)
                {
                    Ten themmoi = new Ten();
                    themmoi.IDNhom = b2.IDNhom.Value;
                    if (b2.STT != null)
                    {
                        themmoi.STT = b2.STT.Value;
                    }
                    themmoi.IDTieuNhom = b2.IdTieuNhom.Value;
                    themmoi.TenTN = b2.TenTN;
                    _Ten.Add(themmoi);
                }
                var dv3 = (from dv1 in data.DichVus
                           join nhom in data.NhomDVs.Where(p => p.TenNhomCT== ("Khám bệnh")) on dv1.IDNhom equals nhom.IDNhom
                           join tn5 in data.TieuNhomDVs on nhom.IDNhom equals tn5.IDNhom
                           select new { tn5.IdTieuNhom, tn5.TenTN, nhom.IDNhom }).ToList();
                if (dv3.Count > 0)
                {
                    _Ten.Add(new Ten { IDNhom = dv3.First().IDNhom, IDTieuNhom = dv3.First().IdTieuNhom, TenTN = dv3.First().TenTN , STT=1});
                }
                var q1 = _Ten.GroupBy(p => p.TenTN).Select(x => x.First()).OrderBy(p=>p.IDNhom).ThenBy(p=>p.TenTN).ToList();
                int k = -1;
                for (int i1 = 0; i1 < q1.Count(); i1++)
                {
                    if (q1.Skip(i1).First().TenTN.ToUpper().Contains(("Ngày Giường").ToUpper()))
                    {
                        k = i1;
                    }
                }
                if (k != -1)
                {
                    q1.RemoveAt(k);
                }
                if (q1.Count > 21)
                {
                    MessageBox.Show("Dữ liệu vượt giới hạn");
                }
                string[] arrTn = new string[20];
                for (int i = 0; i < 20; i++)
                {
                    if (i < q1.Count)
                        arrTn[i] = q1.Skip(i).Take(1).First().TenTN;
                    else
                        arrTn[i] = "";
                }
                int colcount = q1.Count;
                if (q1.Count > 17)
                    colcount = 18;

                for (int i = 0; i < colcount; i++)
                {
                    switch (i)
                    {
                        case 0:
                            rep.n1.Value = q1[i].TenTN.ToString();
                            break;
                        case 1:
                            rep.n2.Value = q1[i].TenTN.ToString();
                            break;
                        case 2:
                            rep.n3.Value = q1[i].TenTN.ToString();
                            break;
                        case 3:
                            rep.n4.Value = q1[i].TenTN.ToString();
                            break;
                        case 4:
                            rep.n5.Value = q1[i].TenTN.ToString();
                            break;
                        case 5:
                            rep.n6.Value = q1[i].TenTN.ToString();
                            break;
                        case 6:
                            rep.n7.Value = q1[i].TenTN.ToString();
                            break;
                        case 7:
                            rep.n8.Value = q1[i].TenTN.ToString();
                            break;
                        case 8:
                            rep.n9.Value = q1[i].TenTN.ToString();
                            break;
                        case 9:
                            rep.n10.Value = q1[i].TenTN.ToString();
                            break;
                        case 10:
                            rep.n11.Value = q1[i].TenTN.ToString();
                            break;
                        case 11:
                            rep.n12.Value = q1[i].TenTN.ToString();
                            break;
                        case 12:
                            rep.n13.Value = q1[i].TenTN.ToString();
                            break;
                        case 13:
                            rep.n14.Value = q1[i].TenTN.ToString();
                            break;
                        case 14:
                            rep.n15.Value = q1[i].TenTN.ToString();
                            break;
                        case 15:
                            rep.n16.Value = q1[i].TenTN.ToString();
                            break;
                        case 16:
                            rep.n17.Value = q1[i].TenTN.ToString();
                            break;
                        case 17:
                            rep.n18.Value = q1[i].TenTN.ToString();
                            break;
                        case 18:
                            rep.n19.Value = q1[i].TenTN.ToString();
                            break;
                        //case 20:
                        //    rep.n18.Value = tnhom[i].TenTN.ToString();
                        //    break;
                        //case 21:
                        //    rep.n18.Value = tnhom[i].TenTN.ToString();
                        //    break;
                        //case 22:
                        //    rep.n18.Value = tnhom[i].TenTN.ToString();
                        //    break;
                    }
                }
                var b = (from a in _BC
                         group a by new { a.TenBN, a.MaBNhan, a.GTinh, a.DChi, a.Tuoi, a.NgayThu } into kq
                         select new
                         {
                             //STT = stt++,
                             kq.Key.MaBNhan,
                             kq.Key.TenBN,
                             kq.Key.DChi,
                             kq.Key.NgayThu,
                             //TuoiNam = kq.Key.GTinh == 1 ? kq.Key.Tuoi : null,
                             TuoiNu = kq.Key.GTinh,
                             //tn1 = kq.Where(p => p.TenTN == idNhomthuoc).Sum(p => p.TienBN),
                             tn1 = kq.Where(p => p.TenTN == arrTn[0]).Where(p => p.PhanLoai == 3).Sum(p => p.TienBN),
                             tn2 = kq.Where(p => p.TenTN == arrTn[1]).Where(p => p.PhanLoai == 3).Sum(p => p.TienBN),
                             tn3 = kq.Where(p => p.TenTN == arrTn[2]).Where(p => p.PhanLoai == 3).Sum(p => p.TienBN),
                             tn4 = kq.Where(p => p.TenTN == arrTn[3]).Where(p => p.PhanLoai == 3).Sum(p => p.TienBN),
                             tn5 = kq.Where(p => p.TenTN == arrTn[4]).Where(p => p.PhanLoai == 3).Sum(p => p.TienBN),
                             tn6 = kq.Where(p => p.TenTN == arrTn[5]).Where(p => p.PhanLoai == 3).Sum(p => p.TienBN),
                             tn7 = kq.Where(p => p.TenTN == arrTn[6]).Where(p => p.PhanLoai == 3).Sum(p => p.TienBN),
                             tn8 = kq.Where(p => p.TenTN == arrTn[7]).Where(p => p.PhanLoai == 3).Sum(p => p.TienBN),
                             tn9 = kq.Where(p => p.TenTN == arrTn[8]).Where(p => p.PhanLoai == 3).Sum(p => p.TienBN),
                             tn10 = kq.Where(p => p.TenTN == arrTn[9]).Where(p => p.PhanLoai == 3).Sum(p => p.TienBN),
                             tn11 = kq.Where(p => p.TenTN == arrTn[10]).Where(p => p.PhanLoai == 3).Sum(p => p.TienBN),
                             tn12 = kq.Where(p => p.TenTN == arrTn[11]).Where(p => p.PhanLoai == 3).Sum(p => p.TienBN),
                             tn13 = kq.Where(p => p.TenTN == arrTn[12]).Where(p => p.PhanLoai == 3).Sum(p => p.TienBN),
                             tn14 = kq.Where(p => p.TenTN == arrTn[13]).Where(p => p.PhanLoai == 3).Sum(p => p.TienBN),
                             tn15 = kq.Where(p => p.TenTN.ToUpper().Contains(("sao lục").ToUpper())).Sum(p => p.TienBN),
                             tn16 = kq.Where(p => p.DTuong== ("BHYT")).Where(p => p.PhanLoai == 1 || p.PhanLoai == 2).Where(p=>p.TrongBH==1).Sum(p => p.TienBN),
                             tn17 = kq.Where(p => p.DTuong== ("KSK")).Sum(p => p.SoTien),
                             tn18 = kq.Where(p => p.TrongBH==0 && p.TenTN.ToUpper().Contains(("Ngày Giường").ToUpper())).Sum(p => p.TienBN),
                             tn19 = kq.Where(p => p.TrongBH==0 && !(p.TenTN.ToUpper().Contains(("Ngày Giường").ToUpper())) && p.PhanLoai != 3).Sum(p => p.TienBN),
                         }).ToList();
                var c = (from bc in b
                         select new
                         {
                             //bc.STT,
                             bc.MaBNhan,
                             bc.TenBN,
                             bc.DChi,
                             NgayThu = bc.NgayThu,
                             bc.TuoiNu,
                             tn1 = bc.tn1,
                             tn2 = bc.tn2,
                             tn3 = bc.tn3,
                             tn4 = bc.tn4,
                             tn5 = bc.tn5,
                             tn6 = bc.tn6,
                             tn7 = bc.tn7,
                             tn8 = bc.tn8,
                             tn9 = bc.tn9,
                             tn10 = bc.tn10,
                             tn11 = bc.tn11,
                             tn12 = bc.tn12,
                             tn13 = bc.tn13,
                             tn14 = bc.tn14,
                             tn15 = bc.tn15,
                             tn16 = bc.tn16,
                             tn17 = bc.tn17,
                             tn18 = bc.tn18,
                             tn19 = bc.tn19,
                             TongCong = bc.tn1 + bc.tn2 + bc.tn3 + bc.tn4 + bc.tn5 + bc.tn6 + bc.tn7 + bc.tn8 + bc.tn9 + bc.tn10 + bc.tn11 + bc.tn12
                             + bc.tn13 + bc.tn14 + bc.tn15 + bc.tn16 + bc.tn17 + bc.tn18 + bc.tn19
                         }).Where(p => p.TongCong != 0).ToList();

                rep.DataSource = c;
                rep.BindingData();
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                this.Hide();
                frm.ShowDialog();
                this.Show();
            }
            else
            {
                frmIn frm = new frmIn();
                BaoCao.rep_BkeVPTH rep = new BaoCao.rep_BkeVPTH();
                data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

                #region add para baocao
                int idNhomthuoc = 0;
                var nt = data.NhomDVs.Where(p => p.TenNhom== ("Thuốc, Dịch truyền"));
                if (nt.Count() > 0)
                {
                    idNhomthuoc = nt.ToList().First().IDNhom;
                }

                rep.Ngay.Value = "Từ ngày: " + dtTungay.Text + "  đến ngày: " + dtDenNgay.Text;
                rep.TenCQ.Value = DungChung.Bien.TenCQ.ToUpper();
                rep.TieuDe.Value = ("Bảng kê thu viện phí").ToUpper();
                #endregion
                //int trongdm = 1, ngoaidm = 0;
                //if (cbo_trongDM.SelectedIndex != 2) {
                //    trongdm = cbo_trongDM.SelectedIndex;
                //    ngoaidm = cbo_trongDM.SelectedIndex;
                //}

                // lấy tiền thu viện phí (thu chi thanh toán)
                var all2 = (from bn in data.BenhNhans
                            join vp in data.VienPhis on bn.MaBNhan equals vp.MaBNhan
                            join vpct in data.VienPhicts on vp.idVPhi equals vpct.idVPhi
                            join dv in data.DichVus on vpct.MaDV equals dv.MaDV
                            join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                            join tu in data.TamUngs on vpct.IDTamUng equals tu.IDTamUng
                            //where (vpct.TrongBH == trongdm || vpct.TrongBH == ngoaidm)
                            orderby vp.MaBNhan, dv.IDNhom
                            select new
                            {
                                tn.STT,
                                vpct.idVPhict,
                                tn.TenTN,
                                bn.MaBNhan,
                                bn.TenBNhan,
                                bn.GTinh,
                                bn.Tuoi,
                                bn.DChi,
                                dv.IDNhom,
                                dv.IdTieuNhom,
                                vpct.SoLuong,
                                vpct.ThanhTien,
                                vpct.TienBN,
                                //vp.NgayTT,
                                bn.DTuong,
                                vpct.TrongBH,
                                bn.NoiTru,
                                tu.PhanLoai,
                                tu.NgayThu,
                                tu.SoTien
                            }).ToList();
                var all = (from a in all2
                           select new
                           {
                               a.STT,
                               a.idVPhict,
                               a.TenTN,
                               a.MaBNhan,
                               a.TenBNhan,
                               a.GTinh,
                               a.Tuoi,
                               a.DChi,
                               a.IDNhom,
                               a.IdTieuNhom,
                               a.SoLuong,
                               a.ThanhTien,
                               a.TienBN,
                               //a.NgayTT,
                               a.DTuong,
                               a.TrongBH,
                               a.NoiTru,
                               a.PhanLoai,
                               a.NgayThu,
                               a.SoTien
                           }).Where(p => p.NgayThu >= tungay && p.NgayThu <= denngay)
                         .Where(p => p.PhanLoai == 1 || p.PhanLoai == 2)
                    //.Where(p => doituong == "tc" || p.DTuong == doituong)
                         .Where(p => noingoaitru == -1 || p.NoiTru == noingoaitru)
                         .ToList();

                //var tn1= from cd in data.ChiDinhs
                //        join dv in data.DichVus

                int stt = 1;
                // lấy tiền thu thẳng
                var all3 = (from bn in data.BenhNhans
                            join cls in data.CLS on bn.MaBNhan equals cls.MaBNhan
                            join cd in data.ChiDinhs.Where(p => p.SoPhieu != null) on cls.IdCLS equals cd.IdCLS
                            join dv in data.DichVus on cd.MaDV equals dv.MaDV
                            join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                            join tu in data.TamUngs.Where(p => p.PhanLoai == 3) on bn.MaBNhan equals tu.MaBNhan
                            //where!(from vp in data.VienPhis select vp.MaBNhan).Contains(bn.MaBNhan)
                            orderby bn.MaBNhan, dv.IDNhom
                            select new
                            {
                                tn.STT,
                                cd.IDCD,
                                tn.TenTN,
                                bn.MaBNhan,
                                bn.TenBNhan,
                                bn.GTinh,
                                bn.Tuoi,
                                bn.DChi,
                                dv.IDNhom,
                                dv.IdTieuNhom,
                                SoLuong = 1,
                                dv.DonGia,
                                TienBN = dv.DonGia,
                                //vp.NgayTT,
                                bn.DTuong,
                                //vpct.TrongBH,
                                bn.NoiTru,
                                tu.PhanLoai,
                                tu.NgayThu,
                                tu.SoTien
                            }).ToList();
                var all4 = (from a in all3
                            select new
                            {
                                a.STT,
                                idVPhict = a.IDCD,
                                a.TenTN,
                                a.MaBNhan,
                                a.TenBNhan,
                                a.GTinh,
                                a.Tuoi,
                                a.DChi,
                                a.IDNhom,
                                a.IdTieuNhom,
                                a.SoLuong,
                                //a.ThanhTien,
                                ThanhTien = 0,
                                a.TienBN,
                                //a.NgayTT,
                                a.DTuong,
                                //a.TrongBH,
                                TrongBH = 1,
                                a.NoiTru,
                                a.PhanLoai,
                                a.NgayThu,
                                a.SoTien,
                            }).Where(p => p.PhanLoai == 3)
                           .Where(p => p.NgayThu >= tungay && p.NgayThu <= denngay)
                    //.Where(p => doituong == "tc" || p.DTuong == doituong)
                         .Where(p => noingoaitru == -1 || p.NoiTru == noingoaitru)
                         .ToList();

                // lấy tiền công khám
                var all5 = (from bn in data.BenhNhans
                            join dt in data.DThuocs on bn.MaBNhan equals dt.MaBNhan
                            join dtct in data.DThuoccts.Where(p => p.IDCD != null) on dt.IDDon equals dtct.IDDon
                            join dv in data.DichVus on dtct.MaDV equals dv.MaDV
                            join nhom in data.NhomDVs.Where(p => p.TenNhomCT== ("Khám bệnh")) on dv.IDNhom equals nhom.IDNhom
                            join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                            join tu in data.TamUngs.Where(p => p.PhanLoai == 3) on dtct.IDCD equals tu.IDTamUng
                            //where!(from vp in data.VienPhis select vp.MaBNhan).Contains(bn.MaBNhan)
                            orderby bn.MaBNhan, dv.IDNhom
                            select new
                            {
                                tn.STT,
                                dtct.IDCD,
                                tn.TenTN,
                                bn.MaBNhan,
                                bn.TenBNhan,
                                bn.GTinh,
                                bn.Tuoi,
                                bn.DChi,
                                dv.IDNhom,
                                dv.IdTieuNhom,
                                SoLuong = 1,
                                dv.DonGia,
                                TienBN = dv.DonGia,
                                //vp.NgayTT,
                                bn.DTuong,
                                //vpct.TrongBH,
                                bn.NoiTru,
                                tu.PhanLoai,
                                tu.NgayThu,
                                tu.SoTien
                            }).ToList();
                var all6 = (from a in all5
                            select new
                            {
                                a.STT,
                                idVPhict = a.IDCD,
                                a.TenTN,
                                a.MaBNhan,
                                a.TenBNhan,
                                a.GTinh,
                                a.Tuoi,
                                a.DChi,
                                a.IDNhom,
                                a.IdTieuNhom,
                                a.SoLuong,
                                //a.ThanhTien,
                                ThanhTien = 0,
                                a.TienBN,
                                //a.NgayTT,
                                a.DTuong,
                                //a.TrongBH,
                                TrongBH = 1,
                                a.NoiTru,
                                a.PhanLoai,
                                a.NgayThu,
                                a.SoTien,
                            }).Where(p => p.PhanLoai == 3)
                           .Where(p => p.NgayThu >= tungay && p.NgayThu <= denngay)
                    //.Where(p => doituong == "tc" || p.DTuong == doituong)
                         .Where(p => noingoaitru == -1 || p.NoiTru == noingoaitru)
                         .ToList();
                // lấy tiền khám sức khoẻ
                var all7 = (from bn in data.BenhNhans.Where(p => p.DTuong== ("KSK"))
                            join tu in data.TamUngs on bn.MaBNhan equals tu.MaBNhan
                            where (tu.NgayThu>=tungay&&tu.NgayThu<=denngay)
                            select new { bn.TenBNhan, bn.MaBNhan, tu.SoTien, bn.DTuong, tu.NgayThu, bn.DChi, bn.GTinh, bn.NoiTru , bn.Tuoi}).ToList();
                if (all7.Count > 0)
                {
                    foreach (var a7 in all7)
                    {
                        BC themmoi = new BC();
                        themmoi.DChi = a7.DChi;
                        themmoi.DTuong = a7.DTuong;
                        if (a7.GTinh == 1)
                        {
                            themmoi.GTinh = "Nam";
                        }
                        else
                        { themmoi.GTinh = "Nữ"; }
                        themmoi.IDNhom = 0;
                        themmoi.IDTieuNhom = 0;
                        themmoi.idVPhi = 0;
                        themmoi.MaBNhan = a7.MaBNhan;
                        themmoi.NoiTru = a7.NoiTru.Value;
                        themmoi.NgayThu = a7.NgayThu.Value.Day + "/" + a7.NgayThu.Value.Month;
                        themmoi.PhanLoai = 1;
                        themmoi.SoLuong = 1;
                        themmoi.SoTien = a7.SoTien.Value;
                        themmoi.TenTN = "";
                        themmoi.TenBN = a7.TenBNhan;
                        themmoi.TienBN = 0;
                        themmoi.Tuoi = a7.Tuoi.Value;
                        themmoi.ThanhTien = 0;
                        themmoi.TrongBH = 1;
                        _BC.Add(themmoi);
                    }
                }

                // add chi phí thu chi thanh toán
                foreach (var c1 in all)
                {
                    BC themmoi = new BC();
                    themmoi.DChi = c1.DChi;
                    themmoi.DTuong = c1.DTuong;
                    if (c1.GTinh == 1)
                    {
                        themmoi.GTinh = "Nam";
                    }
                    else
                    { themmoi.GTinh = "Nữ"; }
                    themmoi.IDNhom = c1.IDNhom.Value;
                    themmoi.IDTieuNhom = c1.IdTieuNhom.Value;
                    themmoi.idVPhi = c1.idVPhict;
                    themmoi.MaBNhan = c1.MaBNhan;
                    themmoi.NoiTru = c1.NoiTru.Value;
                    themmoi.NgayThu = c1.NgayThu.Value.Day + "/" + c1.NgayThu.Value.Month;
                    themmoi.PhanLoai = c1.PhanLoai.Value;
                    themmoi.SoLuong = c1.SoLuong;
                    themmoi.SoTien = c1.SoTien.Value;
                    themmoi.TenTN = c1.TenTN;
                    themmoi.TenBN = c1.TenBNhan;
                    if (c1.TienBN != null)
                    {
                        themmoi.TienBN = c1.TienBN;
                    }
                    else
                    {
                        themmoi.TienBN = 0;
                    }
                    themmoi.Tuoi = c1.Tuoi.Value;
                    themmoi.ThanhTien = c1.ThanhTien;
                    themmoi.TrongBH = c1.TrongBH;
                    _BC.Add(themmoi);
                }
                // add chi phí thu trực tiếp
                foreach (var d in all4)
                {
                    BC themmoi = new BC();
                    themmoi.DChi = d.DChi;
                    themmoi.DTuong = d.DTuong;
                    if (d.GTinh == 1)
                    {
                        themmoi.GTinh = "Nam";
                    }
                    else
                    { themmoi.GTinh = "Nữ"; }
                    themmoi.IDNhom = d.IDNhom.Value;
                    themmoi.IDTieuNhom = d.IdTieuNhom.Value;
                    themmoi.idVPhi = d.idVPhict;
                    themmoi.MaBNhan = d.MaBNhan;
                    themmoi.NoiTru = d.NoiTru.Value;
                    themmoi.NgayThu = d.NgayThu.Value.Day + "/" + d.NgayThu.Value.Month;
                    themmoi.PhanLoai = d.PhanLoai.Value;
                    themmoi.SoLuong = d.SoLuong;
                    themmoi.SoTien = d.SoTien.Value;
                    themmoi.TenTN = d.TenTN;
                    themmoi.TenBN = d.TenBNhan;
                    if (d.TienBN != null)
                    {
                        themmoi.TienBN = d.TienBN;
                    }
                    else
                    { themmoi.TienBN = 0; }
                    themmoi.Tuoi = d.Tuoi.Value;
                    themmoi.ThanhTien = d.ThanhTien;
                    themmoi.TrongBH = d.TrongBH;
                    _BC.Add(themmoi);
                }
                // add công khám thu trực tiếp
                foreach (var c5 in all6)
                {
                    BC themmoi = new BC();
                    themmoi.DChi = c5.DChi;
                    themmoi.DTuong = c5.DTuong;
                    if (c5.GTinh == 1)
                    {
                        themmoi.GTinh = "Nam";
                    }
                    else
                    { themmoi.GTinh = "Nữ"; }
                    themmoi.IDNhom = c5.IDNhom.Value;
                    themmoi.IDTieuNhom = c5.IdTieuNhom.Value;
                    themmoi.idVPhi = c5.idVPhict.Value;
                    themmoi.MaBNhan = c5.MaBNhan;
                    themmoi.NoiTru = c5.NoiTru.Value;
                    themmoi.NgayThu = c5.NgayThu.Value.Day + "/" + c5.NgayThu.Value.Month;
                    themmoi.PhanLoai = c5.PhanLoai.Value;
                    themmoi.SoLuong = c5.SoLuong;
                    themmoi.SoTien = c5.SoTien.Value;
                    themmoi.TenTN = c5.TenTN;
                    themmoi.TenBN = c5.TenBNhan;
                    if (c5.TienBN != null)
                    {
                        themmoi.TienBN = c5.TienBN;
                    }
                    else
                    { themmoi.TienBN = 0; }
                    themmoi.Tuoi = c5.Tuoi.Value;
                    themmoi.ThanhTien = c5.ThanhTien;
                    themmoi.TrongBH = c5.TrongBH;
                    _BC.Add(themmoi);
                }
                var tnhom = (from a in all where a.IDNhom != idNhomthuoc select new { a.TenTN, a.IdTieuNhom, a.IDNhom,a.STT }).OrderBy(p => p.IDNhom).Distinct().ToList();
                var tnhom1 = (from a in all4 where a.IDNhom != idNhomthuoc select new { a.TenTN, a.IdTieuNhom, a.IDNhom , a.STT}).OrderBy(p => p.IDNhom).Distinct().ToList();
                foreach (var b1 in tnhom)
                {
                    Ten themmoi = new Ten();
                    themmoi.IDNhom = b1.IDNhom.Value;
                    if (b1.STT != null)
                    { themmoi.STT = b1.STT.Value; }
                    themmoi.IDTieuNhom = b1.IdTieuNhom.Value;
                    themmoi.TenTN = b1.TenTN;
                    _Ten.Add(themmoi);
                }
                foreach (var b2 in tnhom1)
                {
                    Ten themmoi = new Ten();
                    themmoi.IDNhom = b2.IDNhom.Value;
                    if (b2.STT != null)
                    { themmoi.STT = b2.STT.Value; }
                    themmoi.IDTieuNhom = b2.IdTieuNhom.Value;
                    themmoi.TenTN = b2.TenTN;
                    _Ten.Add(themmoi);
                }
                var dv3 = (from dv1 in data.DichVus
                           join nhom in data.NhomDVs.Where(p => p.TenNhomCT== ("Khám bệnh")) on dv1.IDNhom equals nhom.IDNhom
                           join tn5 in data.TieuNhomDVs on nhom.IDNhom equals tn5.IDNhom
                           select new { tn5.IdTieuNhom, tn5.TenTN, nhom.IDNhom }).ToList();
                if (dv3.Count > 0)
                {
                    _Ten.Add(new Ten { IDNhom = dv3.First().IDNhom, IDTieuNhom = dv3.First().IdTieuNhom, TenTN = dv3.First().TenTN, STT=1 });
                }
                var q1 = _Ten.GroupBy(p => p.TenTN).Select(x => x.First()).OrderBy(p=>p.STT).ToList();
                int k = -1;
                for (int i1 = 0; i1 < q1.Count(); i1++)
                {
                    if (q1.Skip(i1).First().TenTN.ToUpper().Contains(("Ngày Giường").ToUpper()))
                    {
                        k = i1;
                    }
                }
                if (k != -1)
                {
                    q1.RemoveAt(k);
                }
                if (q1.Count > 21)
                {
                    MessageBox.Show("Dữ liệu vượt giới hạn");
                }
                string[] arrTn = new string[20];
                for (int i = 0; i < 20; i++)
                {
                    if (i < q1.Count)
                        arrTn[i] = q1.Skip(i).Take(1).First().TenTN;
                    else
                        arrTn[i] = "";
                }
                int colcount = q1.Count;
                if (q1.Count > 17)
                    colcount = 18;

                for (int i = 0; i < colcount; i++)
                {
                    switch (i)
                    {
                        case 0:
                            rep.n1.Value = q1[i].TenTN.ToString();
                            break;
                        case 1:
                            rep.n2.Value = q1[i].TenTN.ToString();
                            break;
                        case 2:
                            rep.n3.Value = q1[i].TenTN.ToString();
                            break;
                        case 3:
                            rep.n4.Value = q1[i].TenTN.ToString();
                            break;
                        case 4:
                            rep.n5.Value = q1[i].TenTN.ToString();
                            break;
                        case 5:
                            rep.n6.Value = q1[i].TenTN.ToString();
                            break;
                        case 6:
                            rep.n7.Value = q1[i].TenTN.ToString();
                            break;
                        case 7:
                            rep.n8.Value = q1[i].TenTN.ToString();
                            break;
                        case 8:
                            rep.n9.Value = q1[i].TenTN.ToString();
                            break;
                        case 9:
                            rep.n10.Value = q1[i].TenTN.ToString();
                            break;
                        case 10:
                            rep.n11.Value = q1[i].TenTN.ToString();
                            break;
                        case 11:
                            rep.n12.Value = q1[i].TenTN.ToString();
                            break;
                        case 12:
                            rep.n13.Value = q1[i].TenTN.ToString();
                            break;
                        case 13:
                            rep.n14.Value = q1[i].TenTN.ToString();
                            break;
                        case 14:
                            rep.n15.Value = q1[i].TenTN.ToString();
                            break;
                        case 15:
                            rep.n16.Value = q1[i].TenTN.ToString();
                            break;
                        case 16:
                            rep.n17.Value = q1[i].TenTN.ToString();
                            break;
                        case 17:
                            rep.n18.Value = q1[i].TenTN.ToString();
                            break;
                        case 18:
                            rep.n19.Value = q1[i].TenTN.ToString();
                            break;
                        //case 20:
                        //    rep.n18.Value = tnhom[i].TenTN.ToString();
                        //    break;
                        //case 21:
                        //    rep.n18.Value = tnhom[i].TenTN.ToString();
                        //    break;
                        //case 22:
                        //    rep.n18.Value = tnhom[i].TenTN.ToString();
                        //    break;
                    }
                }
                var b = (from a in _BC
                         group a by new { a.NgayThu } into kq
                         select new
                         {
                             //STT = stt++,
                             //kq.Key.MaBNhan,
                             //kq.Key.TenBN,
                             //kq.Key.DChi,
                             kq.Key.NgayThu,
                             //TuoiNam = kq.Key.GTinh == 1 ? kq.Key.Tuoi : null,
                             //TuoiNu = kq.Key.GTinh,
                             //tn1 = kq.Where(p => p.TenTN == idNhomthuoc).Sum(p => p.TienBN),
                             tn1 = kq.Where(p => p.TenTN == arrTn[0]).Where(p => p.PhanLoai == 3).Sum(p => p.TienBN),
                             tn2 = kq.Where(p => p.TenTN == arrTn[1]).Where(p => p.PhanLoai == 3).Sum(p => p.TienBN),
                             tn3 = kq.Where(p => p.TenTN == arrTn[2]).Where(p => p.PhanLoai == 3).Sum(p => p.TienBN),
                             tn4 = kq.Where(p => p.TenTN == arrTn[3]).Where(p => p.PhanLoai == 3).Sum(p => p.TienBN),
                             tn5 = kq.Where(p => p.TenTN == arrTn[4]).Where(p => p.PhanLoai == 3).Sum(p => p.TienBN),
                             tn6 = kq.Where(p => p.TenTN == arrTn[5]).Where(p => p.PhanLoai == 3).Sum(p => p.TienBN),
                             tn7 = kq.Where(p => p.TenTN == arrTn[6]).Where(p => p.PhanLoai == 3).Sum(p => p.TienBN),
                             tn8 = kq.Where(p => p.TenTN == arrTn[7]).Where(p => p.PhanLoai == 3).Sum(p => p.TienBN),
                             tn9 = kq.Where(p => p.TenTN == arrTn[8]).Where(p => p.PhanLoai == 3).Sum(p => p.TienBN),
                             tn10 = kq.Where(p => p.TenTN == arrTn[9]).Where(p => p.PhanLoai == 3).Sum(p => p.TienBN),
                             tn11 = kq.Where(p => p.TenTN == arrTn[10]).Where(p => p.PhanLoai == 3).Sum(p => p.TienBN),
                             tn12 = kq.Where(p => p.TenTN == arrTn[11]).Where(p => p.PhanLoai == 3).Sum(p => p.TienBN),
                             tn13 = kq.Where(p => p.TenTN == arrTn[12]).Where(p => p.PhanLoai == 3).Sum(p => p.TienBN),
                             tn14 = kq.Where(p => p.TenTN == arrTn[13]).Where(p => p.PhanLoai == 3).Sum(p => p.TienBN),
                             tn15 = kq.Where(p => p.TenTN.ToUpper().Contains(("sao lục").ToUpper())).Sum(p => p.TienBN),
                             tn16 = kq.Where(p => p.DTuong== ("BHYT")).Where(p => p.PhanLoai == 1 || p.PhanLoai == 2).Where(p => p.TrongBH == 1).Sum(p => p.TienBN),
                             tn17 = kq.Where(p => p.DTuong== ("KSK")).Sum(p => p.SoTien),
                             tn18 = kq.Where(p => p.TrongBH==0 && p.TenTN.ToUpper().Contains(("Ngày Giường").ToUpper())).Sum(p => p.TienBN),
                             tn19 = kq.Where(p => p.TrongBH==0 && !(p.TenTN.ToUpper().Contains(("Ngày Giường").ToUpper())) && p.PhanLoai != 3).Sum(p => p.TienBN),
                         }).ToList();
                var c = (from bc in b
                         select new
                         {
                             //bc.STT,
                             //bc.MaBNhan,
                             //bc.TenBN,
                             //bc.DChi,
                             NgayThu = bc.NgayThu,
                             //bc.TuoiNu,
                             tn1 = bc.tn1,
                             tn2 = bc.tn2,
                             tn3 = bc.tn3,
                             tn4 = bc.tn4,
                             tn5 = bc.tn5,
                             tn6 = bc.tn6,
                             tn7 = bc.tn7,
                             tn8 = bc.tn8,
                             tn9 = bc.tn9,
                             tn10 = bc.tn10,
                             tn11 = bc.tn11,
                             tn12 = bc.tn12,
                             tn13 = bc.tn13,
                             tn14 = bc.tn14,
                             tn15 = bc.tn15,
                             tn16 = bc.tn16,
                             tn17 = bc.tn17,
                             tn18 = bc.tn18,
                             tn19 = bc.tn19,
                             TongCong = bc.tn1 + bc.tn2 + bc.tn3 + bc.tn4 + bc.tn5 + bc.tn6 + bc.tn7 + bc.tn8 + bc.tn9 + bc.tn10 + bc.tn11 + bc.tn12
                             + bc.tn13 + bc.tn14 + bc.tn15 + bc.tn16 + bc.tn17 + bc.tn18 + bc.tn19
                         }).Where(p => p.TongCong != 0).ToList();

                rep.DataSource = c;
                rep.BindingData();
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                this.Hide();
                frm.ShowDialog();
                this.Show();
            }

        }
        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void lupDoiTuong_EditValueChanged(object sender, EventArgs e)
        {
            //if (lupDoiTuong.Text != "BHYT")
            //{
            //    cbo_trongDM.SelectedIndex = 2;
            //    cbo_trongDM.Properties.ReadOnly = true;
            //}
            //else {
            //    cbo_trongDM.Properties.ReadOnly = false;
            //}
        }
    }
}