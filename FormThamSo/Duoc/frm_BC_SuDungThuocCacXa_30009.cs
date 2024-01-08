using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace QLBV.FormThamSo
{
    public partial class frm_BC_SuDungThuocCacXa_30009 : DevExpress.XtraEditors.XtraForm
    {
        public frm_BC_SuDungThuocCacXa_30009()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

        private void frm_BC_SuDungThuocCacXa_Load(object sender, EventArgs e)
        {
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            lupDenNgay.DateTime = DateTime.Now;
            lupTuNgay.DateTime = DateTime.Now;
            DSKPhong();
            DSXa();
            DSDTBN();
        }

        #region DS đối tượng bệnh nhân
        private void DSDTBN()
        {
            var dtbn = data.DTBNs.ToList();
            lupDoituong.Properties.DataSource = dtbn;
        }
        #endregion
        #region function DSKPhong
        List<KhoaPhong> _lKPhong = new List<KhoaPhong>();
        private void DSKPhong()
        {
            KhoaPhong themmoi = new KhoaPhong();
            var dsKho = data.KPhongs.Where(p => p.PLoai == "Khoa dược").ToList();
            if (dsKho.Count > 0)
            {
                KhoaPhong themmoi1 = new KhoaPhong();
                themmoi1.TenKP = "Chọn tất cả";
                themmoi1.MaKP = 0;
                themmoi1.Chon = true;
                _lKPhong.Add(themmoi1);
                foreach (var a in dsKho)
                {
                    themmoi = new KhoaPhong();
                    themmoi.TenKP = a.TenKP;
                    themmoi.MaKP = a.MaKP;
                    themmoi.Chon = true;
                    _lKPhong.Add(themmoi);
                }
                grcKhoaphong.DataSource = _lKPhong.ToList();
            }
        }
        #endregion
        #region function DSXa
        List<Xa> _lXa = new List<Xa>();
        private void DSXa()
        {
            Xa themmoi = new Xa();
            var dsXa = data.KPhongs.Where(p => p.PLoai == "Xã phường").ToList();
            if (dsXa.Count > 0)
            {
                Xa themmoi1 = new Xa();
                themmoi1.Ten = "Chọn tất cả";
                themmoi1.IdXa = 0;
                themmoi1.Chon = true;
                _lXa.Add(themmoi1);
                foreach (var a in dsXa)
                {
                    themmoi = new Xa();
                    themmoi.Ten = a.TenKP;
                    themmoi.IdXa = a.MaKP;
                    themmoi.Chon = true;
                    _lXa.Add(themmoi);
                }
                grcDSXa.DataSource = _lXa.ToList();
            }
        }
        #endregion

        #region class Xa
        public class Xa
        {
            public int IdXa { get; set; }
            public string Ten { get; set; }
            public bool Chon { get; set; }
        }
        #endregion
        #region class KhoaPhong
        private class KhoaPhong
        {
            public string TenKP { get; set; }
            public int MaKP { get; set; }
            public bool Chon { get; set; }
        }
        #endregion

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            DateTime tungay = DungChung.Ham.NgayTu(lupTuNgay.DateTime);
            DateTime denngay = DungChung.Ham.NgayDen(lupDenNgay.DateTime);
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            //lấy ds kho được chọn
            List<KhoaPhong> _lKhoChon = new List<KhoaPhong>();
            _lKhoChon = _lKPhong.Where(p => p.MaKP > 0).Where(p => p.Chon == true).ToList();
            //lấy ds xã được chọn
            List<Xa> _lXaChon = new List<Xa>();
            _lXaChon = _lXa.Where(p => p.IdXa > 0).Where(p => p.Chon == true).ToList();
            //đối tượng BN
            int _dtbn = 0;
            if (lupDoituong.EditValue != null)
            {
                _dtbn = Convert.ToInt32(lupDoituong.EditValue.ToString());
            }

            #region select
            var qdv = (from dv in data.DichVus.Where(p => p.PLoai == 1)
                       join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                       select new
                       {
                           dv.TenDV,
                           dv.DonVi,
                           tn.TenTN,
                           dv.MaDV,
                           dv.TenHC,
                           dv.DuongD,
                           dv.SoDK,
                           dv.MaQD,
                           dv.SoTTqd,
                           dv.SoQD,
                           dv.HamLuong
                       }).ToList();
            var qNhapd = (from kp in _lKhoChon
                          join nd in data.NhapDs.Where(p => p.PLoai == 5).Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay) on kp.MaKP equals nd.MaKP
                          join ndct in data.NhapDcts on nd.IDNhap equals ndct.IDNhap
                          join dtBN in data.DTBNs on ndct.IDDTBN equals dtBN.IDDTBN into kq
                          from kq1 in kq.DefaultIfEmpty()
                          select new
                          {
                              nd.IDNhap,
                              ndct.IDDTBN,
                              DTBN = kq1 == null ? "" : kq1.DTBN1,
                              nd.Mien,
                              nd.PLoai,
                              nd.KieuDon,
                              nd.XuatTD,
                              nd.NgayNhap,
                              nd.MaKPnx,
                              ndct.MaDV,
                              ndct.SoLuongSD,
                              ndct.ThanhTienSD,
                              ndct.DonGia
                          }).Where(p => p.IDDTBN == _dtbn).ToList();
            var qXuatD = (from x in _lXaChon
                          join nd in qNhapd on x.IdXa equals nd.MaKPnx
                          join dv in qdv on nd.MaDV equals dv.MaDV
                          group new { x, nd, dv } by new { x.Ten, dv.TenTN, dv.TenDV, dv.DonVi, nd.DonGia, dv.SoTTqd, dv.SoQD, dv.SoDK, dv.DuongD, dv.TenHC, dv.HamLuong } into kq
                          select new
                          {
                              TenTYT = kq.Key.Ten,
                              kq.Key.TenTN,
                              kq.Key.TenDV,
                              kq.Key.DonVi,
                              kq.Key.DonGia,
                              kq.Key.SoTTqd,
                              kq.Key.SoQD,
                              kq.Key.SoDK,
                              kq.Key.DuongD,
                              kq.Key.TenHC,
                              kq.Key.HamLuong,
                              SoLuong = kq.Sum(p => p.nd.SoLuongSD),
                              ThanhTien = kq.Sum(p => p.nd.ThanhTienSD)
                          }).ToList();
            #endregion
            BaoCao.Rep_BC_SuDungThuocCuaCacXa_30009 rep = new BaoCao.Rep_BC_SuDungThuocCuaCacXa_30009();
            frmIn frm = new frmIn();
            rep.DataSource = qXuatD.OrderBy(p => p.TenDV);
            rep.lbl_ThoiGian.Text = "(Thời gian bắt đầu từ " + tungay.ToString("dd/MM/yyyy") + " đến ngày " + denngay.ToString("dd/MM/yyyy") + " )";
            rep.lbl_NgayThang.Text = "Ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;
            rep.BindingData();
            rep.CreateDocument();
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();
        }

        private void grvKhoaphong_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "Chọn")
            {
                if (grvKhoaphong.GetFocusedRowCellValue("TenKP") != null)
                {
                    string Ten = grvKhoaphong.GetFocusedRowCellValue("TenKP").ToString();

                    if (Ten == "Chọn tất cả")
                    {
                        if (_lKPhong.First().Chon == true)
                        {
                            foreach (var a in _lKPhong)
                            {
                                a.Chon = false;
                            }
                        }
                        else
                        {
                            foreach (var a in _lKPhong)
                            {
                                a.Chon = true;
                            }
                        }
                        grcKhoaphong.DataSource = "";
                        grcKhoaphong.DataSource = _lKPhong.ToList();
                    }
                }
            }
        }

        private void grvDSXa_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "colChon")
            {
                if (grvDSXa.GetFocusedRowCellValue("Ten") != null)
                {
                    string Ten = grvDSXa.GetFocusedRowCellValue("Ten").ToString();

                    if (Ten == "Chọn tất cả")
                    {
                        if (_lXa.First().Chon == true)
                        {
                            foreach (var a in _lXa)
                            {
                                a.Chon = false;
                            }
                        }
                        else
                        {
                            foreach (var a in _lXa)
                            {
                                a.Chon = true;
                            }
                        }
                        grcDSXa.DataSource = "";
                        grcDSXa.DataSource = _lXa.ToList();
                    }
                }
            }
        }
    }
}