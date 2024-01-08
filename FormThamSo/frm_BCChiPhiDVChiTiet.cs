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
    public partial class frm_BCChiPhiDVChiTiet : DevExpress.XtraEditors.XtraForm
    {
        public frm_BCChiPhiDVChiTiet()
        {
            InitializeComponent();
        }
        public class KhoaPhong
        {
            public bool _check;
            public int _maKP;
            public string _kp;
            public int MaKP { get { return _maKP; } set { _maKP = value; } }
            public bool Check { get { return _check; } set { _check = value; } }
            public string TenKP { get { return _kp; } set { _kp = value; } }
        }
        QLBV_Database.QLBVEntities data;
        private void frm_BCChiPhiDVChiTiet_Load(object sender, EventArgs e)
        {
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            lupDenNgay.DateTime = System.DateTime.Now;
            lupTuNgay.DateTime = System.DateTime.Now;
            cboDoituong.SelectedIndex = 0;
            var q = (from k in data.KPhongs
                     join rv in data.RaViens on k.MaKP equals rv.MaKP
                     select new KhoaPhong()
                     {
                         Check = false,
                         MaKP = k.MaKP,
                         TenKP = k.TenKP
                     }).Distinct().ToList();
            if (DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297")
            {
                q = (from k in data.KPhongs.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.CanLamSang || p.PLoai == DungChung.Bien.st_PhanLoaiKP.LamSang || p.PLoai == DungChung.Bien.st_PhanLoaiKP.PhongKham)
                     select new KhoaPhong()
                     {
                         Check = false,
                         MaKP = k.MaKP,
                         TenKP = k.TenKP
                     }).Distinct().ToList();
            }
            List<KhoaPhong> _lKP2 = new List<KhoaPhong>(q.Count + 1);
            _lKP2.Add(new KhoaPhong { Check = false, MaKP = 0, TenKP = "Tất cả" });
            _lKP2.InsertRange(1, q);
            grcKhoaPhong.DataSource = _lKP2;
            for (int i = 0; i < grvKhoaPhong.RowCount; i++)
            {
                grvKhoaPhong.SetRowCellValue(i, colCheckGrvKP, true);
            }

            var _listNhomDV = data.NhomDVs.Where(p => p.Status > 0).ToList();
            cklNhomDV.DisplayMember = "TenNhomCT";
            cklNhomDV.ValueMember = "IDNhom";
            cklNhomDV.DataSource = _listNhomDV;
            _listTieuNhom = data.TieuNhomDVs.Where(p => p.Status == 1).ToList();
            _listDV = data.DichVus.ToList();
            _listKP = data.KPhongs.ToList();
        }
        List<TieuNhomDV> _listTieuNhom = new List<TieuNhomDV>();
        List<DichVu> _listDV = new List<DichVu>();
        List<KPhong> _listKP = new List<KPhong>();
        List<DichVu> _ldv = new List<DichVu>();
        private void cklNhomDV_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            List<int> _idNhomDV = new List<int>();
            for (int i = 0; i < cklNhomDV.ItemCount; i++)
            {
                if (cklNhomDV.GetItemCheckState(i) == CheckState.Checked)
                    _idNhomDV.Add(Convert.ToInt32(cklNhomDV.GetItemValue(i)));
            }
            var _ltn = (from nh in _idNhomDV
                        join tn in _listTieuNhom on nh equals tn.IDNhom
                        select tn).ToList();
            ckcTieuNhomDV.DisplayMember = "TenTN";
            ckcTieuNhomDV.ValueMember = "IdTieuNhom";
            ckcTieuNhomDV.DataSource = _ltn;
            ckcTieuNhomDV_ItemCheck(null, null);
        }

        private void ckcTieuNhomDV_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            List<int> _idTieuNhomDV = new List<int>();
            for (int i = 0; i < ckcTieuNhomDV.ItemCount; i++)
            {
                if (ckcTieuNhomDV.GetItemCheckState(i) == CheckState.Checked)
                    _idTieuNhomDV.Add(Convert.ToInt32(ckcTieuNhomDV.GetItemValue(i)));
            }
            _ldv = (from nh in _idTieuNhomDV
                    join tn in _listDV on nh equals tn.IdTieuNhom
                    select tn).ToList();
            ckl_DichVu.DisplayMember = "TenDV";
            ckl_DichVu.ValueMember = "MaDV";
            ckl_DichVu.DataSource = _ldv;
        }

        private void hyp_Chon_OpenLink(object sender, DevExpress.XtraEditors.Controls.OpenLinkEventArgs e)
        {
            ckl_DichVu.CheckAll();
        }

        private void hyp_HuyChon_OpenLink(object sender, DevExpress.XtraEditors.Controls.OpenLinkEventArgs e)
        {
            ckl_DichVu.UnCheckAll();
        }

        private void txtTimDV_EditValueChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtTimDV.Text))
            {
                try
                {
                    int Madv = Convert.ToInt32(txtTimDV.Text);
                    ckl_DichVu.DataSource = _ldv.Where(p => p.MaDV == Madv).ToList();
                }
                catch
                {
                    string Tendv = txtTimDV.Text.ToLower().Trim();
                    ckl_DichVu.DataSource = _ldv.Where(p => p.TenDV.ToLower().Contains(Tendv)).ToList();
                }
            }
            else
                ckl_DichVu.DataSource = _ldv;
        }
        public class _BC
        {
            public int mabn { get; set; }
            public string tenbn { get; set; }
            public int soluong { get; set; }
            public double dongia { get; set; }
            public double thanhtien { get; set; }
            public int madv { get; set; }
            public string tendv { get; set; }
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            List<int> _lMaKhoa = new List<int>();
            int kp = 0;
            for (int i = 0; i < grvKhoaPhong.RowCount; i++)
            {
                if (grvKhoaPhong.GetRowCellValue(i, colCheckGrvKP) != null)
                {
                    if (grvKhoaPhong.GetRowCellValue(i, colCheckGrvKP).ToString() == "True")
                    {
                        int mKhoa = grvKhoaPhong.GetRowCellValue(i, colmaKP) == null ? -1 : Convert.ToInt32(grvKhoaPhong.GetRowCellValue(i, colmaKP));

                        if (mKhoa == 0)
                        {
                            kp = 0;

                            break;
                        }
                        else
                            _lMaKhoa.Add(mKhoa);
                    }
                    else
                    {
                        kp = -1;
                    }
                }
            }
            List<int> _lDichVu = new List<int>();
            for (int i = 0; i < ckl_DichVu.ItemCount; i++)
            {
                if (ckl_DichVu.GetItemChecked(i))
                    _lDichVu.Add(Convert.ToInt32(ckl_DichVu.GetItemValue(i)));
            }

            List<NhomDV> _lnhom = new List<NhomDV>();
            List<int> _idNhomDV = new List<int>();
            for (int i = 0; i < cklNhomDV.ItemCount; i++)
            {
                if (cklNhomDV.GetItemCheckState(i) == CheckState.Checked)
                    _idNhomDV.Add(Convert.ToInt32(cklNhomDV.GetItemValue(i)));
            }
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            _lnhom = (from nhom in data.NhomDVs join id in _idNhomDV on nhom.IDNhom equals id select nhom).ToList();
            var qdv = (from nhom in _lnhom
                       join tn in data.TieuNhomDVs on nhom.IDNhom equals tn.IDNhom
                       join dv in data.DichVus on tn.IdTieuNhom equals dv.IdTieuNhom
                       select new
                       {
                           nhom,
                           tn.TenTN,
                           dv
                       }).ToList();
            DateTime tungay = DungChung.Ham.NgayTu(lupTuNgay.DateTime);
            DateTime denngay = DungChung.Ham.NgayDen(lupDenNgay.DateTime);

            int _dtuong = cboDoituong.SelectedIndex;
            int _noitru = radNoiTru.SelectedIndex;


            List<_BC> lkq = new List<_BC>();
            var cd = (from a1 in data.ChiDinhs
                      join b in data.CLS on a1.IdCLS equals b.IdCLS
                      select new { a1.IDCD, a1.SoPhieu, a1.MaDV, b.MaKPth, b.MaBNhan }).ToList();
            var qtuct = (from bn in data.BenhNhans.Where(p => _dtuong == 0 ? true : (_dtuong == 1 ? p.DTuong == "BHYT" : (_dtuong == 2 ? p.DTuong == "Dịch vụ" : p.DTuong == "KSK"))).Where(p => _noitru == 2 || p.NoiTru == _noitru)
                         join tu in data.TamUngs.Where(p => p.NgayThu >= tungay && p.NgayThu <= denngay).Where(p => p.PhanLoai == 3) on bn.MaBNhan equals tu.MaBNhan
                         join tuct in data.TamUngcts.Where(p => p.Status == 0) on tu.IDTamUng equals tuct.IDTamUng// select new {bn, tuct})                            
                         select new
                         {
                             MaBNhan = bn.MaBNhan,
                             TenBNhan = bn.TenBNhan,
                             NgayThu = tu.NgayThu,
                             MaKP = tu.MaKP,// phòng khám hoặc khoa phòng điều trị ( không phải phòng thu viện phí)
                             TrongBH = tuct.TrongBH,
                             DonGia = tuct.DonGia,
                             MaDV = tuct.MaDV,
                             SoLuong = tuct.SoLuong,
                             ThanhTien = tuct.SoTien,
                             IDTamUng = tu.IDTamUng,
                             IDGoiDV = tu.IDGoiDV,
                             TenDV = "",
                             IDTamUngct = tuct.IDTamUngct
                         }).ToList();
            var test = (from bn in data.BenhNhans.Where(p => _dtuong == 0 ? true : (_dtuong == 1 ? p.DTuong == "BHYT" : (_dtuong == 2 ? p.DTuong == "Dịch vụ" : p.DTuong == "KSK"))).Where(p => _noitru == 2 || p.NoiTru == _noitru)
                        join tu in data.TamUngs.Where(p => p.NgayThu >= tungay && p.NgayThu <= denngay).Where(p => p.PhanLoai == 3) on bn.MaBNhan equals tu.MaBNhan
                        join goi in data.DmGoiDVs on tu.IDGoiDV equals goi.IDGoi
                        select new
                        {
                            MaBNhan = bn.MaBNhan,
                              TenBNhan=bn.TenBNhan,
                            NgayThu = tu.NgayThu,
                            MaKP = tu.MaKP,// phòng khám hoặc khoa phòng điều trị ( không phải phòng thu viện phí)
                            TrongBH = 0,
                            DonGia = goi.DonGia,
                            MaDV = 0,
                            SoLuong = 1.00,
                            ThanhTien = tu.SoTien ?? 0,
                            IDTamUng = tu.IDTamUng,
                            IDGoiDV = tu.IDGoiDV,
                            TenDV = goi.TenGoi,
                            IDTamUngct = 0
                        }).ToList();
            qtuct = (from a1 in qtuct.Where(p => p.IDGoiDV == 0)
                     join b in cd on new { IDTamUng = a1.IDTamUng, MaDV = a1.MaDV } equals new { IDTamUng = b.SoPhieu ?? 0, MaDV = b.MaDV ?? 0 } into k
                     from k1 in k.DefaultIfEmpty()
                     select new
                     {
                         MaBNhan = a1.MaBNhan,
                         TenBNhan=a1.TenBNhan,
                         NgayThu = a1.NgayThu,
                         MaKP = k1 != null ? k1.MaKPth : a1.MaKP,// Khoa phòng thực hiện
                         TrongBH = a1.TrongBH,
                         DonGia = a1.DonGia,
                         MaDV = a1.MaDV,
                         SoLuong = a1.SoLuong,
                         ThanhTien = a1.ThanhTien,
                         IDTamUng = a1.IDTamUng,
                         IDGoiDV = a1.IDGoiDV,
                         TenDV = a1.TenDV,
                         IDTamUngct = a1.IDTamUngct
                     }).Distinct().ToList();
            qtuct.AddRange(test);

            var qtk = (from tu in qtuct.Where(p => p.TenDV == "")
                       join nhom in qdv on tu.MaDV equals nhom.dv.MaDV
                       where (kp == 0 ? true : _lMaKhoa.Contains(tu.MaKP == null ? 0 : tu.MaKP.Value))
                       select new
                       {
                           MaBNhan = tu.MaBNhan,
                           TenBNhan=tu.TenBNhan,
                           NgayThu = tu.NgayThu,
                           MaKP = tu.MaKP,// phòng khám hoặc khoa phòng điều trị ( không phải phòng thu viện phí)
                           TrongBH = tu.TrongBH,
                           DonGia = tu.DonGia,
                           MaDV = tu.MaDV,
                           SoLuong = tu.SoLuong,
                           ThanhTien = tu.ThanhTien,
                           TenDV = nhom.dv.TenDV,
                       }).ToList();
            var qtk1 = (from tu in qtuct.Where(p => p.TenDV != "")
                        where (kp == 0 ? true : _lMaKhoa.Contains(tu.MaKP == null ? 0 : tu.MaKP.Value))
                        // where (lupDoituong.Text ==   "BHYT" ? ((tu.TrongBH == trongBH) ) : true)                          
                        select new
                        {
                            MaBNhan = tu.MaBNhan,
                            TenBNhan=tu.TenBNhan,
                            NgayThu = tu.NgayThu,
                            MaKP = tu.MaKP,// phòng khám hoặc khoa phòng điều trị ( không phải phòng thu viện phí)
                            TrongBH = tu.TrongBH,
                            DonGia = tu.DonGia,
                            MaDV = tu.MaDV,
                            SoLuong = tu.SoLuong,
                            ThanhTien = tu.ThanhTien,
                            TenDV = tu.TenDV,
                        }).ToList();
            qtk.AddRange(qtk1);
            var kqq = (from a in qtk
                       join dv in _lDichVu on a.MaDV equals dv
                       group a by new { a.MaBNhan, a.TenDV, a.TenBNhan, a.DonGia, a.MaDV } into kq
                       select new
                       {
                           kq.Key.MaBNhan,
                           kq.Key.TenBNhan,
                           kq.Key.MaDV,
                           kq.Key.TenDV,
                           kq.Key.DonGia,
                           ThanhTien = kq.Sum(p => p.ThanhTien),
                           SoLuong = kq.Sum(p => p.SoLuong)
                       }).OrderBy(p => p.TenDV).ThenBy(p => p.TenBNhan).ToList();
            if(kqq.Count()>0)
            {
                frmIn frm = new frmIn();
                BaoCao.rep_BCThuThangDVChiTiet rep = new BaoCao.rep_BCThuThangDVChiTiet();

                rep.TuNgayDenNgay.Value = "Từ ngày  " + lupTuNgay.DateTime.ToString().Substring(0, 10) + "  đến ngày  " + lupDenNgay.DateTime.ToString().Substring(0, 10);
                rep.txtTieuDe.Text = "BÁO CÁO CHI PHÍ DỊCH VỤ THU THẲNG CHI TIẾT";
                //if (!string.IsNullOrEmpty(macc))
                //    rep.NhaCC.Value = lupNhaCC.Text;
                rep.DataSource = kqq;

                rep.BindingData();
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Không có dữ liệu");
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void grvKhoaPhong_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "colCheckGrvKP")
            {

                if (e.RowHandle == 0)
                {
                    if (grvKhoaPhong.GetFocusedRowCellValue(colCheckGrvKP) != null)
                    {
                        if (grvKhoaPhong.GetRowCellValue(0, colCheckGrvKP).ToString() == "False")
                        {
                            for (int i = 0; i < grvKhoaPhong.RowCount; i++)
                            {
                                grvKhoaPhong.SetRowCellValue(i, "Check", true);
                            }
                        }
                        else
                        {
                            for (int i = 0; i < grvKhoaPhong.RowCount; i++)
                            {
                                grvKhoaPhong.SetRowCellValue(i, "Check", false);
                            }
                        }

                    }
                }
                else
                {
                    for (int i = 0; i < grvKhoaPhong.RowCount; i++)
                    {
                        if (grvKhoaPhong.GetRowCellValue(i, colCheckGrvKP) != null && grvKhoaPhong.GetRowCellValue(i, colCheckGrvKP).ToString() == "True")
                        {
                            grvKhoaPhong.SetRowCellValue(0, colCheckGrvKP, false);
                            break;
                        }
                        else
                        {

                        }
                    }

                }

            }
        }
    }
}