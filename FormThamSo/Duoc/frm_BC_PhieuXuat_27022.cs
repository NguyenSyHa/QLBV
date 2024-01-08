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
    public partial class frm_BC_PhieuXuat_27022 : DevExpress.XtraEditors.XtraForm
    {
        public frm_BC_PhieuXuat_27022()
        {
            InitializeComponent();
        }
        #region class Khoa
        private class Khoa
        {
            public bool Chon { get; set; }
            public int MaKP { get; set; }
            public string TenKP { get; set; }
        }
        private class TieuNhom
        {
            public bool Chon { get; set; }
            public int IdTN { get; set; }
            public int IdNhom { get; set; }
            public string TenTN { get; set; }
        }
        #endregion
        #region class DSDuoc
        private class DSDuoc
        {
            public string TenKP { get; set; }
            public int MaDV { get; set; }
            public string MaTam { get; set; }
            public string TenDV { get; set; }
            public string DonVi { get; set; }
            public string SoDK { get; set; }
            public double DonGia { get; set; }
            public double? SoLuongX { get; set; }
            public double? ThanhTienX { get; set; }
            public int IDNhom { get; set; }
            public int IDTieuNhom { get; set; }
            public string TenNhom { get; set; }
            public string TenTieuNhom { get; set; }
            public int MaKP { get; set; }
        }
        #endregion
        List<Khoa> _lKhoa = new List<Khoa>();
        List<TieuNhom> _lTieuNhom = new List<TieuNhom>();
        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public static int _radNTIndex = 0;
        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDSKP();
        }
        private void LoadTieuNhom(){
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            _lTieuNhom.Clear();
            var TieuNhom = (from tn in data.TieuNhomDVs select tn).ToList();
            //var TN = (from tnhom in TieuNhom.Where(p => p.IDNhom == 4 || p.IDNhom == 5 || p.IDNhom == 6 || p.IDNhom == 7 || p.IDNhom == 10).OrderBy(p => p.TenTN) select{tnhom.}
            var TN =(from tnhom in TieuNhom.Where(p=>p.IDNhom==4 || p.IDNhom==5 ||p.IDNhom==6||p.IDNhom==7||p.IDNhom==10).OrderBy(p=>p.TenTN) select new{tnhom.TenTN,tnhom.IdTieuNhom}).ToList();
            if (TN.Count>0)
            {
                TieuNhom addAll = new TieuNhom();
                addAll.TenTN="Chọn tất cả";
                addAll.IdTN=0;
                addAll.Chon=true;
                _lTieuNhom.Add(addAll);
                foreach(var item in TN)
                {
                    TieuNhom add = new TieuNhom();
                    add.TenTN=item.TenTN;
                    add.IdTN=item.IdTieuNhom;
                    add.Chon=true;
                    _lTieuNhom.Add(add);     
                }
                
                grcTieunhom.DataSource=_lTieuNhom.ToList();
            }
        }
        private void LoadDSKP()
        {
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            _lKhoa.Clear();

            var qkp = (from k in data.KPhongs select k).ToList();
            int Index = radioGroup1.SelectedIndex;
            var kphong = (from kp in qkp.Where(p => (Index == 0 ? (p.PLoai.Equals("Phòng khám") || p.TenKP.ToLower().Contains("khoa khám bệnh")) : Index == 1 ? (p.PLoai.Equals("Lâm sàng") && !p.TenKP.ToLower().Contains("khoa khám bệnh")) : (p.PLoai.Equals("Phòng khám") || p.PLoai.Equals("Lâm sàng") || p.PLoai.Equals("Cận lâm sàng") || p.PLoai.Equals("Tủ trực"))))
                          select new { kp.TenKP, kp.MaKP }).ToList();
            if (kphong.Count > 0)
            {
                Khoa themmoi1 = new Khoa();
                themmoi1.TenKP = "Chọn tất cả";
                themmoi1.MaKP = 0;
                themmoi1.Chon = true;
                _lKhoa.Add(themmoi1);
                foreach (var a in kphong)
                {
                    Khoa themmoi = new Khoa();
                    themmoi.TenKP = a.TenKP;
                    themmoi.MaKP = a.MaKP;
                    themmoi.Chon = true;
                    _lKhoa.Add(themmoi);
                }
                grcKhoaphong.DataSource = _lKhoa.ToList();
            }
        }
        private void frm_BC_PhieuXuat_27022_Load(object sender, EventArgs e)
        {  QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            dateTuNgay.DateTime = DateTime.Now;
            dateDenNgay.DateTime = DateTime.Now;
            LoadDSKP();
            LoadTieuNhom();
            List<TieuNhomDV> tnhom = data.TieuNhomDVs.Where(p => p.IDNhom == 4 || p.IDNhom == 5 || p.IDNhom == 6 || p.IDNhom == 7 || p.IDNhom == 10).OrderBy(p => p.TenTN).ToList();
            tnhom.Insert(0, new TieuNhomDV { IdTieuNhom = 0, TenTN = "Tất cả", TenRG = "Tất cả" });
            lupTieuNhom.Properties.DataSource = tnhom;
            lupTieuNhom.EditValue = 0;
        }

        List<DSDuoc> _lduoc = new List<DSDuoc>();
        private void btnOK_Click(object sender, EventArgs e)
        {
            int PPX = radioGroup1.SelectedIndex;
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            DateTime tungay = DungChung.Ham.NgayTu(dateTuNgay.DateTime);
            _lduoc.Clear();
            DateTime denngay = DungChung.Ham.NgayDen(dateDenNgay.DateTime);
            List<Khoa> khoa = new List<Khoa>();
            string strkhoa = string.Empty;
            khoa = _lKhoa.Where(p => p.Chon == true && p.MaKP > 0).ToList();
            List<TieuNhom> tieunhom = new List<TieuNhom>();
            if(DungChung.Bien.MaBV=="27022")
            {
                tieunhom = _lTieuNhom.Where(p => p.Chon == true && p.IdTN > 0).ToList();
            }
            int khotong = 0;
            int idtieunhom = 0;
            if (lupTieuNhom.EditValue != null)
            {
                idtieunhom = Convert.ToInt32(lupTieuNhom.EditValue);
            }
            if (DungChung.Bien.MaBV.Equals("27021"))
            {
                var qkho = data.KPhongs.Where(p => p.TenKP.ToLower().Contains("kho tổng")).ToList();
                khotong = qkho.Count > 0 ? qkho.FirstOrDefault().MaKP : 0;
            }
            foreach (var item in khoa)
            {
                strkhoa += item.TenKP + ";";
            }
            List<int> _lMaKP = _lKhoa.Where(p => p.Chon == true).Select(p => p.MaKP).ToList();
            strkhoa = strkhoa.Substring(0, strkhoa.Length - 1);
            BaoCao.rep_BC_PhieuXuat_27022 rep = new BaoCao.rep_BC_PhieuXuat_27022(PPX);

            var nhom = (from dv in data.DichVus
                        join tn in data.TieuNhomDVs.Where(p => idtieunhom == 0 ? true : p.IdTieuNhom == idtieunhom) on dv.IdTieuNhom equals tn.IdTieuNhom
                        join ndv in data.NhomDVs.Where(p => p.Status == 1) on tn.IDNhom equals ndv.IDNhom
                        select new
                        {
                            dv.MaDV,
                            dv.MaTam,
                            TenDV = dv.TenDV,
                            dv.DonVi,
                            dv.SoDK,
                            tn.IdTieuNhom,
                            tn.TenTN,
                            ndv.IDNhom,
                            ndv.TenNhom,
                        }).ToList();
            //var nhom = (from dv in data.DichVus
            //            join tn in tieunhom on dv.IdTieuNhom equals tn.IdTN
            //            join ndv in data.NhomDVs.Where(p => p.Status == 1) on tn.IdNhom equals ndv.IDNhom
            //            select new
            //            {
            //                dv.MaDV,
            //                dv.MaTam,
            //                TenDV = dv.TenDV,
            //                dv.DonVi,
            //                dv.SoDK,
            //                tn.IdTN,
            //                tn.TenTN,
            //                ndv.IDNhom,
            //                ndv.TenNhom,
            //            }).ToList();
            var qnhap1 = (from nd in data.NhapDs.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay)
                         join ndct in data.NhapDcts on nd.IDNhap equals ndct.IDNhap
                         select new
                         {
                             ndct.IDNhapct,
                             nd.MaKPnx,
                             MaKP = nd.MaKP ?? 0,
                             nd.SoCT,
                             ndct.MaDV,
                             nd.KieuDon,
                             nd.PLoai,
                             nd.TraDuoc_KieuDon,
                             nd.NgayNhap,
                             ndct.DonGia,
                             SoLuongN = nd.PLoai == 1 ? ndct.SoLuongN : 0,
                             SoLuongX = (nd.PLoai == 2|| nd.PLoai == 3) ? ndct.SoLuongX : 0,
                             ThanhTienN = nd.PLoai == 1 ? ndct.ThanhTienN : 0,
                             ThanhTienX = (nd.PLoai == 2 || nd.PLoai == 3) ? ndct.ThanhTienX : 0,
                         }).ToList();
            var qnhap = (from nd in qnhap1
                         join dv in nhom on nd.MaDV equals dv.MaDV
                         select new
                         {
                             nd.IDNhapct,
                             nd.MaKPnx,
                             MaKP = nd.MaKP,
                             nd.SoCT,
                             nd.MaDV,
                             nd.KieuDon,
                             nd.PLoai,
                             nd.TraDuoc_KieuDon,
                             dv.MaTam,
                             TenDV = dv.TenDV,
                             dv.DonVi,
                             dv.SoDK,
                             dv.IdTieuNhom,
                             dv.TenTN,
                             dv.IDNhom,
                             dv.TenNhom,
                             nd.NgayNhap,
                             nd.DonGia,
                             nd.SoLuongX,
                             nd.ThanhTienX,
                             nd.SoLuongN,
                             nd.ThanhTienN
                         }).ToList();
            if (rdgTimKiem.SelectedIndex == 0)//theo khoa kê
            {
                _lduoc = (from n in qnhap.Where(p => khotong == 0 || p.MaKP != khotong)
                          join kp in khoa on n.MaKPnx equals kp.MaKP into k
                          from k1 in k.DefaultIfEmpty()
                          group n by new { MaKP = k1 == null ? 0 : k1.MaKP, TenKP = k1 == null ? "" : k1.TenKP, n.MaDV, n.MaTam, n.TenDV, n.DonVi, n.SoDK, n.DonGia,
                          n.IDNhom,n.TenNhom,n.IdTieuNhom,n.TenTN
                          } into kq
                          select new DSDuoc
                          {
                              MaKP = kq.Key.MaKP,
                              TenKP = kq.Key.TenKP,
                              MaDV = kq.Key.MaDV ?? 0,
                              MaTam = kq.Key.MaTam,
                              TenDV = kq.Key.TenDV,
                              DonVi = kq.Key.DonVi,
                              SoDK = kq.Key.SoDK,
                              DonGia = kq.Key.DonGia,
                              TenNhom=kq.Key.TenNhom,
                              IDNhom=kq.Key.IDNhom,
                              IDTieuNhom=kq.Key.IdTieuNhom,
                              TenTieuNhom=kq.Key.TenTN,
                              SoLuongX = (radioGroup1.SelectedIndex == 0) ? kq.Where(p => p.PLoai == 2 && (p.KieuDon == 0 || p.KieuDon == 4)).Sum(p => p.SoLuongX) - kq.Where(p => p.PLoai == 1 && p.KieuDon == 2 && (p.TraDuoc_KieuDon == 0 || p.TraDuoc_KieuDon == 4)).Sum(p => p.SoLuongN) :
                                         ((radioGroup1.SelectedIndex == 1) ? kq.Where(p => p.PLoai == 2 && p.KieuDon == 1).Sum(p => p.SoLuongX) - kq.Where(p => p.PLoai == 1 && p.KieuDon == 2 &&p.TraDuoc_KieuDon == 1).Sum(p => p.SoLuongN) :
                                         (kq.Where(p => p.PLoai == 2).Sum(p => p.SoLuongX) - kq.Where(p => p.KieuDon == 2).Sum(p => p.SoLuongX) - kq.Where(p => p.PLoai == 1 && p.KieuDon == 2).Sum(p => p.SoLuongN))),

                              ThanhTienX = (radioGroup1.SelectedIndex == 0) ? kq.Where(p => p.PLoai == 2 && (p.KieuDon == 0 || p.KieuDon == 4)).Sum(p => p.ThanhTienX) - kq.Where(p => p.PLoai == 1 && p.KieuDon == 2 && (p.TraDuoc_KieuDon == 0 || p.TraDuoc_KieuDon == 4)).Sum(p => p.ThanhTienN) :
                                           ((radioGroup1.SelectedIndex == 1) ? kq.Where(p => p.PLoai == 2 && p.KieuDon == 1).Sum(p => p.ThanhTienX) - kq.Where(p => p.PLoai == 1 && p.KieuDon == 2 && p.TraDuoc_KieuDon == 1).Sum(p => p.ThanhTienN) :
                                           (kq.Where(p => p.PLoai == 2).Sum(p => p.ThanhTienX) - kq.Where(p => p.KieuDon == 2).Sum(p => p.ThanhTienX) - kq.Where(p => p.PLoai == 1 && p.KieuDon == 2).Sum(p => p.ThanhTienN)))
                          }).Where(p=> _lMaKP.Contains(p.MaKP)).ToList();
            }
            if (rdgTimKiem.SelectedIndex == 1)//theo kho xuất
            {
                _lduoc = (from n in qnhap//.Where(p=>rdgTimKiem.SelectedIndex == 0 ? true : _lMaKP.Contains(p.MaKP))
                          join kp in khoa on n.MaKP equals kp.MaKP
                          group n by new { kp.MaKP, kp.TenKP, n.MaDV, n.MaTam, n.TenDV, n.DonVi, n.SoDK, n.DonGia,n.IDNhom, n.TenNhom, n.IdTieuNhom, n.TenTN } into kq
                          select new DSDuoc
                          {
                              TenNhom = kq.Key.TenNhom,
                              IDNhom = kq.Key.IDNhom,
                              IDTieuNhom = kq.Key.IdTieuNhom,
                              TenTieuNhom = kq.Key.TenTN,
                              TenKP = kq.Key.TenKP,
                              MaDV = kq.Key.MaDV ?? 0,
                              MaTam = kq.Key.MaTam,
                              TenDV = kq.Key.TenDV,
                              DonVi = kq.Key.DonVi,
                              SoDK = kq.Key.SoDK,
                              DonGia = kq.Key.DonGia,
                              SoLuongX = (khoa.Count > 1) ? (kq.Where(p => p.PLoai == 2).Sum(p => p.SoLuongX) - kq.Where(p => p.KieuDon == 2).Sum(p => p.SoLuongX) - kq.Where(p => p.PLoai == 1 && p.KieuDon == 2).Sum(p => p.SoLuongN)) :
                                         ((kq.Where(p => p.PLoai == 2).Sum(p => p.SoLuongX) - kq.Where(p => p.PLoai == 1 && p.KieuDon == 2).Sum(p => p.SoLuongN))),

                              ThanhTienX = (khoa.Count > 1) ? (kq.Where(p => p.PLoai == 2).Sum(p => p.ThanhTienX) - kq.Where(p => p.KieuDon == 2).Sum(p => p.ThanhTienX) - kq.Where(p => p.PLoai == 1 && p.KieuDon == 2).Sum(p => p.ThanhTienN)):
                                           ((kq.Where(p => p.PLoai == 2).Sum(p => p.ThanhTienX) - kq.Where(p => p.PLoai == 1 && p.KieuDon == 2).Sum(p => p.ThanhTienN)))
                          }).ToList();
            }
            double TT = 0;
            if (_lduoc.Count > 0)
            {
                string tong = String.Format("{0:##,##0}", _lduoc.Sum(p => p.ThanhTienX));
                TT = Convert.ToDouble(tong);
            }
            rep.txtsotien.Text = "Tổng số tiền (bằng chữ): " + DungChung.Ham.DocTienBangChu(TT, " đồng!");
            rep.Ngaythang.Value = "Từ ngày " + tungay.ToString("dd/MM/yyyy") + " đến ngày " + denngay.ToString("dd/MM/yyyy");
            rep.Noidung.Value = radioGroup1.SelectedIndex == 0 ? "Xuất ngoại trú và điều trị ngoại trú" : (radioGroup1.SelectedIndex == 1 ? "Xuất nội trú" : "Xuất nội trú, ngoại trú và điều trị ngoại trú");
            rep.Khoxuat.Value = strkhoa;
            rep.Ngay.Value = "Ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;
            frmIn frm = new frmIn();
            rep.DataSource = _lduoc.Where(p => p.SoLuongX != 0).OrderBy(p => p.TenDV).ToList();
            rep.BindingData();
            rep.CreateDocument();
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();
        }

        private void grvKhoaphong_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "colChon")
            {
                if (grvKhoaphong.GetFocusedRowCellValue("TenKP") != null)
                {
                    string Ten = grvKhoaphong.GetFocusedRowCellValue("TenKP").ToString();

                    if (Ten.Equals("Chọn tất cả"))
                    {
                        if (_lKhoa.First().Chon == true)
                        {
                            foreach (var a in _lKhoa)
                            {
                                a.Chon = false;
                            }
                        }
                        else
                        {
                            foreach (var a in _lKhoa)
                            {
                                a.Chon = true;
                            }
                        }
                        grcKhoaphong.DataSource = "";
                        grcKhoaphong.DataSource = _lKhoa.ToList();
                    }
                }
            }
        }

        private void grvTieunhom_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "colChonTN")
            {
                if (grvTieunhom.GetFocusedRowCellValue("TenTN") != null)
                {
                    string Ten = grvTieunhom.GetFocusedRowCellValue("TenTN").ToString();

                    if (Ten.Equals("Chọn tất cả"))
                    {
                        if (_lTieuNhom.First().Chon == true)
                        {
                            foreach (var a in _lTieuNhom)
                            {
                                a.Chon = false;
                            }
                        }
                        else
                        {
                            foreach (var a in _lTieuNhom)
                            {
                                a.Chon = true;
                            }
                        }
                        grcTieunhom.DataSource = "";
                        grcTieunhom.DataSource = _lTieuNhom.ToList();
                    }
                }
            }
        }

        private void rdgTimKiem_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdgTimKiem.SelectedIndex == 1)
            {
                QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                radioGroup1.Enabled = false;
                _lKhoa.Clear();
                var kphong = (from kp in data.KPhongs.Where(p => p.PLoai.Equals("Khoa dược"))
                              select new { kp.TenKP, kp.MaKP }).ToList();
                if (kphong.Count > 0)
                {
                    Khoa themmoi1 = new Khoa();
                    themmoi1.TenKP = "Chọn tất cả";
                    themmoi1.MaKP = 0;
                    themmoi1.Chon = true;
                    _lKhoa.Add(themmoi1);
                    foreach (var a in kphong)
                    {
                        Khoa themmoi = new Khoa();
                        themmoi.TenKP = a.TenKP;
                        themmoi.MaKP = a.MaKP;
                        themmoi.Chon = true;
                        _lKhoa.Add(themmoi);
                    }
                    grcKhoaphong.DataSource = _lKhoa.ToList();
                }
            }
            else
            {
                radioGroup1.Enabled = true;
                radioGroup1_SelectedIndexChanged(sender, e);
            }
        }

        private void grcKhoaphong_Click(object sender, EventArgs e)
        {

        }
        
    }
}