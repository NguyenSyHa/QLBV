using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace QLBV.TraCuu
{
    public partial class frm_BC_PhieuCDTH : DevExpress.XtraEditors.XtraForm
    {
        //int _mabn = 10863;
        List<int> _lIdCLS = new List<int>();
        //List<int> _lIdBN = new List<int>();
        QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        List<DichVu> _lDichvu = new List<DichVu>();
        public frm_BC_PhieuCDTH()
        {
            InitializeComponent();
        }
        public frm_BC_PhieuCDTH(List<int> lIdCLD)
        {
            InitializeComponent();
            _lIdCLS = lIdCLD;
        }
        private class DichVu
        {
            public bool Chon { get; set; }
            public string TenDV { get; set; }
            public double DonGia { get; set; }
            public double SoLuong { get; set; }
            public double ThanhTien { get; set; }
            public int MaDV { get; set; }


        }
        private void btnIn_Click(object sender, EventArgs e)
        {
            xuatBC();
        }
        public void xuatBC()
        {

            var query = (from bn in _data.BenhNhans
                         join cls in _data.CLS.Where(p => _lIdCLS.Contains(p.IdCLS)) on bn.MaBNhan equals cls.MaBNhan
                         //join cd in _data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                         //join dv in _data.DichVus on cd.MaDV equals dv.MaDV
                         join kp in _data.KPhongs on bn.MaKP equals kp.MaKP
                         join kb in _data.BNKBs on bn.MaBNhan equals kb.MaBNhan
                         //where bn.MaBNhan == _mabn
                         select new
                         {
                             bn.MaBNhan,
                             bn.TenBNhan,
                             bn.Tuoi,
                             bn.DChi,
                             bn.GTinh,
                             bn.SThe,
                             kp.TenKP,
                             kp.MaKP
                             //kb.Buong,
                             //kb.Giuong,
                             //kb.ChanDoan
                             //cls.IdCLS


                         }).ToList();
            BaoCao.repPhieuChiDinhTongHop rep = new BaoCao.repPhieuChiDinhTongHop();
            if (query.Count() > 0)
            {
                rep.TenBN.Value = query.FirstOrDefault().TenBNhan;
                rep.Tuoi.Value = query.FirstOrDefault().Tuoi;
                rep.DiaChi.Value = query.FirstOrDefault().DChi;
                rep.Khoa.Value = query.FirstOrDefault().TenKP;

                string[] arrThongTinBNKB = new string[5] { "", "", "", "", "" };
                arrThongTinBNKB = DungChung.Ham.laythongtinBNKB(_data, query.FirstOrDefault().MaBNhan, query.FirstOrDefault().MaKP, true);
                rep.ChanDoan.Value = arrThongTinBNKB[1];
                rep.Buong.Value = arrThongTinBNKB[2];
                rep.Giuong.Value = arrThongTinBNKB[3];
                rep.Khoa.Value = arrThongTinBNKB[4];

                if (query.FirstOrDefault().GTinh == 1)
                {
                    rep.Nu.Value = "/";
                }
                else
                {
                    rep.Nam.Value = "/";
                }
                if (query.First().SThe.Length >= 15)
                {
                    rep.SThe1.Value = query.First().SThe.Substring(0, 3);
                    rep.SThe2.Value = query.First().SThe.Substring(3, 2);
                    rep.SThe3.Value = query.First().SThe.Substring(5, 2);
                    rep.SThe4.Value = query.First().SThe.Substring(7, 3);
                    rep.SThe5.Value = query.First().SThe.Substring(10, 5);
                }
            }
            List<DichVu> dsdvtt = new List<DichVu>();
            dsdvtt = (_lDichvu.Where(p => p.Chon == true && p.MaDV > 0)).ToList();//danh sách dv thanh toán
            double tongtien = 0; ;
            foreach (var i in dsdvtt)
            {
                if (i.TenDV.Contains("tiền công khám") || i.TenDV.Contains("tiền công")
                || i.TenDV.Contains("công khám") || i.TenDV.Contains("tiền khám"))
                {
                    int idDonct = i.MaDV;
                    var donthuocct = (from dtct in _data.DThuoccts.Where(p => p.IDDonct == idDonct)
                                      select dtct).ToList();
                    if (donthuocct.Count() > 0)
                        donthuocct.FirstOrDefault().Status = 1;
                    _data.SaveChanges();


                }
                tongtien += i.ThanhTien;
            }
            rep.txtTongTien.Text = tongtien.ToString();
            frmIn frm = new frmIn();
            rep.DataSource = dsdvtt;
            rep.BindingData();
            rep.CreateDocument();
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();

        }

        private void frm_BC_PhieuCDTH_Load(object sender, EventArgs e)
        {
            //  _lIdCLS.Add(311280);
            //_lIdCLS.Add(311276);
            //_lIdCLS.Add(63);
            _lDichvu.Clear();

            var dsdv = (from bn in _data.BenhNhans
                        join cls in _data.CLS.Where(p => _lIdCLS.Contains(p.IdCLS)).Where(p => p.Status == 0) on bn.MaBNhan equals cls.MaBNhan
                        join cd in _data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                        join dv in _data.DichVus on cd.MaDV equals dv.MaDV
                        select new
                        {
                            dv.TenDV,
                            dv.DonGia,
                            dv.DonGia2,
                            dv.DonGiaBHYT,
                            cd.TrongBH,
                            SoLuong = dv.SLuong == null || dv.SLuong == 0 ? 1 : dv.SLuong.Value,
                            dv.MaDV,
                            cls.NgayThang,
                            bn.MaBNhan
                        }).ToList();
            if (dsdv.Count() > 0)
            {
                DichVu dv = new DichVu();
                dv.TenDV = "Chọn tất cả";
                dv.Chon = true;
                dv.MaDV = 0;
                //dv.DonGia = 0;
                //dv.SoLuong = 0;
                _lDichvu.Add(dv);
                foreach (var i in dsdv)
                {
                    DichVu dvs = new DichVu();
                    dvs.TenDV = i.TenDV;
                    if (i.TrongBH == 1)
                    {
                        if (i.NgayThang >= DungChung.Bien.ngayGiaMoi)
                        {
                            dvs.DonGia = i.DonGiaBHYT;
                        }
                        else
                        {
                            dvs.DonGia = i.DonGia;
                        }
                    }
                    else
                    {
                        dvs.DonGia = i.DonGia2;
                    }
                    dvs.Chon = true;
                    dvs.SoLuong = Convert.ToDouble(i.SoLuong);
                    dvs.ThanhTien = dvs.SoLuong * dvs.DonGia;
                    dvs.MaDV = i.MaDV;
                    //_lIdBN.Add(i.MaBNhan);
                    _lDichvu.Add(dvs);
                }
            }
            var qidbn = (from cls in _data.CLS.Where(p => _lIdCLS.Contains(p.IdCLS))
                         select cls.MaBNhan).ToList();
            int idbn = 0;
            if (qidbn.Count() > 0)
            {
                idbn = Convert.ToInt32(qidbn.FirstOrDefault());
            }
            var tiencongkham = (from bn in _data.BenhNhans.Where(p => p.MaBNhan == idbn)
                                join dt in _data.DThuocs on bn.MaBNhan equals dt.MaBNhan
                                join dtct in _data.DThuoccts.Where(p => p.Status == 0 || p.Status == null) on dt.IDDon equals dtct.IDDon
                                join dichvu in _data.DichVus on dtct.MaDV equals dichvu.MaDV
                                join tndv in _data.TieuNhomDVs on dichvu.IDNhom equals tndv.IDNhom
                                join ndv in _data.NhomDVs.Where(p => p.TenNhomCT == "Khám bệnh") on tndv.IDNhom equals ndv.IDNhom
                                select new
                                {
                                    dichvu.MaDV,
                                    tndv.TenTN,
                                    dtct.DonGia,
                                    dtct.SoLuong,
                                    ThanhTien = dtct.DonGia * dtct.SoLuong,
                                    dtct.IDDonct
                                }).ToList();


            if (tiencongkham.Count() > 0)
            {
                foreach (var i in tiencongkham)
                {
                    DichVu dvs = new DichVu();
                    dvs.TenDV = i.TenTN;
                    dvs.DonGia = i.DonGia;
                    dvs.SoLuong = i.SoLuong;
                    dvs.ThanhTien = i.ThanhTien;
                    dvs.MaDV = i.IDDonct;
                    dvs.Chon = true;
                    _lDichvu.Add(dvs);

                }
            }
            grcTHDV.DataSource = _lDichvu.ToList();

        }



        private void grvCDTH_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "colChon")
            {
                string ten = grvCDTH.GetFocusedRowCellValue("TenDV").ToString();
                if (ten == "Chọn tất cả")
                {
                    if (_lDichvu.First().Chon == true)
                    {
                        foreach (var dv in _lDichvu)
                        {
                            dv.Chon = false;
                        }
                    }
                    else
                    {
                        foreach (var dv in _lDichvu)
                        {
                            dv.Chon = true;
                        }
                    }
                    grcTHDV.DataSource = _lDichvu.ToList();
                }

            }
        }
    }
}