using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using System.IO;

namespace QLBV.FormThamSo
{
    public partial class frmTsSoKiemNhap : DevExpress.XtraEditors.XtraForm
    {
        public frmTsSoKiemNhap()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

        private bool KTtaoSoKiemNhap()
        {
            for (int i = 1; i < grvChungTu.RowCount; i++)
            {
                if (grvChungTu.GetRowCellValue(i, "Check") != null && grvChungTu.GetRowCellValue(i, "Check").ToString() == "True")
                    return true;
            }
            return false;
        }


        private void btnInSo_Click(object sender, EventArgs e)
        {
            DateTime _ngaynhap = System.DateTime.Now.Date;
            //List<Kho> dskho = new List<Kho>();
            //dskho = _lKho.Where(p => p.Chon == true && p.MaKP > 0).ToList();
            DateTime tungay = DungChung.Ham.NgayTu(lupNgayBDSD.DateTime);
            DateTime denngay = DungChung.Ham.NgayDen(lupNgayHS.DateTime);
            if (KTtaoSoKiemNhap())
            {

                frmIn frm = new frmIn();
                BaoCao.repSoKiemNhap rep = new BaoCao.repSoKiemNhap();
                //  QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

                rep.NgayBDSD.Value = lupNgayBDSD.Text;
                rep.NgayHS.Value = lupNgayHS.Text;
                rep.LyDo.Value = txtLyDoNhap.Text;
                rep.TV1.Value = lupTV1.Text;
                rep.TV2.Value = lupTV2.Text;
                rep.TV3.Value = lupTV3.Text;
                rep.TV4.Value = lupTV4.Text;
                rep.TV5.Value = lupTV5.Text;
                rep.CD1.Value = txtChucDanh1.Text;
                rep.CD2.Value = txtChucDanh2.Text;
                rep.CD3.Value = txtChucDanh3.Text;
                rep.CD4.Value = txtChucDanh4.Text;
                rep.CD5.Value = txtChucDanh5.Text;

                if(txtChucDanh1.Text == "Chủ tịch")
                    rep.ChuTich1.Value = lupTV1.Text;
                else if (txtChucDanh1.Text == "Thư ký")
                    rep.ThuKy1.Value = lupTV1.Text;
                else
                    rep.TVM1.Value = lupTV1.Text;
                //2
                if (txtChucDanh2.Text == "Chủ tịch")
                {
                    if (rep.ChuTich1.Value != null)
                        rep.ChuTich2.Value = lupTV2.Text;
                    else
                        rep.ChuTich1.Value = lupTV2.Text;
                }
                else if (txtChucDanh2.Text == "Thư ký" )
                {
                    if (rep.ThuKy1.Value != null)
                        rep.ThuKy2.Value = lupTV2.Text;
                    else
                        rep.ThuKy1.Value = lupTV2.Text;
                }
                else
                    rep.TVM2.Value = lupTV2.Text;
                //3
                if (txtChucDanh3.Text == "Chủ tịch")
                {
                    if (rep.ChuTich1.Value != null)
                        rep.ChuTich2.Value = lupTV3.Text;
                    else
                        rep.ChuTich1.Value = lupTV3.Text;
                }
                else if (txtChucDanh3.Text == "Thư ký")
                {
                    if (rep.ThuKy1.Value != null)
                        rep.ThuKy2.Value = lupTV3.Text;
                    else
                        rep.ThuKy1.Value = lupTV3.Text;
                }
                else
                    rep.TVM3.Value = lupTV3.Text;
                //4
                if (txtChucDanh4.Text == "Chủ tịch")
                {
                    if (rep.ChuTich1.Value != null)
                        rep.ChuTich2.Value = lupTV4.Text;
                    else
                        rep.ChuTich1.Value = lupTV4.Text;
                }
                else if (txtChucDanh4.Text == "Thư ký")
                {
                    if (rep.ThuKy1.Value != null)
                        rep.ThuKy2.Value = lupTV4.Text;
                    else
                        rep.ThuKy1.Value = lupTV4.Text;
                }
                else
                    rep.TVM4.Value = lupTV4.Text;
                //5
                if (txtChucDanh5.Text == "Chủ tịch")
                {
                    if (rep.ChuTich1.Value != null)
                        rep.ChuTich2.Value = lupTV5.Text;
                    else
                        rep.ChuTich1.Value = lupTV5.Text;
                }
                else if (txtChucDanh5.Text == "Thư ký")
                {
                    if (rep.ThuKy1.Value != null)
                        rep.ThuKy2.Value = lupTV5.Text;
                    else
                        rep.ThuKy1.Value = lupTV5.Text;
                }
                else
                    rep.TVM5.Value = lupTV5.Text;

                List<int> lSoCT = new List<int>();

                for (int i = 0; i < grvChungTu.RowCount; i++)
                {
                    if (grvChungTu.GetRowCellValue(i, "Check") != null && grvChungTu.GetRowCellValue(i, "Check").ToString() == "True")
                        lSoCT.Add(Convert.ToInt32(grvChungTu.GetRowCellValue(i, "IDNhap")));
                }
                List<int> lkp = new List<int>();

                for (int i = 0; i < grvKhoaphong.RowCount; i++)
                {
                    if (grvKhoaphong.GetRowCellValue(i, "Chon") != null && grvKhoaphong.GetRowCellValue(i, "Chon").ToString() == "True")
                        lkp.Add(Convert.ToInt32(grvKhoaphong.GetRowCellValue(i, "MaKP")));
                }
                var qnn = (from nn in data.NhapDs.Where(p => p.PLoai == 1).Where(p => lSoCT.Contains(p.IDNhap)) select new { nn.NgayNhap }).ToList();
                if (qnn.Count > 0)
                {
                    rep.NgayNhap.Value = qnn.First().NgayNhap.ToString().Substring(0, 10);
                }
                var _ldv = (from dv in data.DichVus.Where(p => p.PLoai == 1)
                            join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                            join nhomdv in data.NhomDVs on tn.IDNhom equals nhomdv.IDNhom
                            select new { dv.MaDV, nhomdv.TenNhom, nhomdv.IDNhom, dv.NuocSX, dv.TenDV }).ToList();
                var _lnd = (from nhapd in data.NhapDs.Where(p => lSoCT.Contains(p.IDNhap))
                            join nhapdct in data.NhapDcts on nhapd.IDNhap equals nhapdct.IDNhap
                            where ((ckNhapHD.Checked && DungChung.Bien.MaBV.Equals("04016")) ? (nhapd.PLoai == 1 && nhapd.KieuDon == 1 && (nhapd.NgayNhap >= tungay && nhapd.NgayNhap <= denngay)) : nhapd.PLoai == 1)
                            select new { nhapd, nhapdct }).ToList();
                var q = (from k in lkp
                         join nd in _lnd on k equals nd.nhapd.MaKP
                         join dv in _ldv on nd.nhapdct.MaDV equals dv.MaDV
                         group new { nd, dv } by new { dv.TenNhom, nd.nhapd.IDNhap, nd.nhapd.SoCT, dv.TenDV, dv.NuocSX, nd.nhapdct.SoLo, nd.nhapdct.HanDung, nd.nhapdct.DonVi, nd.nhapdct.DonGia } into kq
                         select new
                         {
                             IDNhap = kq.Key.IDNhap,
                             TenNhomDuoc = kq.Key.TenNhom,
                             SoCT = kq.Key.SoCT,
                             TenDuoc = kq.Key.TenDV,
                             DonVi = kq.Key.DonVi,
                             SoKS = kq.Key.SoLo,
                             NuocSX = kq.Key.NuocSX,
                             HanDung = kq.Key.HanDung,
                             DonGia = kq.Key.DonGia,
                             SoLuong = kq.Where(p=>p.nd.nhapd.PLoai == 1).Sum(p => p.nd.nhapdct.SoLuongN),
                             ThanhTien =  kq.Where(p=>p.nd.nhapd.PLoai == 1).Sum(p => p.nd.nhapdct.ThanhTienN)
                         }).OrderBy(p => p.IDNhap).ToList();
                //var q = (from k in dskho
                //         join nhapd in data.NhapDs.Where(p => lSoCT.Contains(p.IDNhap)) on k.MaKP equals nhapd.MaKP
                //         join nhapdct in data.NhapDcts on nhapd.IDNhap equals nhapdct.IDNhap
                //         join dv in data.DichVus on nhapdct.MaDV equals dv.MaDV
                //         join nhomdv in data.NhomDVs on dv.IDNhom equals nhomdv.IDNhom
                //         where ((ckNhapHD.Checked && DungChung.Bien.MaBV.Equals("04016")) ? (nhapd.PLoai == 1 && nhapd.KieuDon == 1 && (nhapd.NgayNhap >= tungay && nhapd.NgayNhap <= denngay)) : nhapd.PLoai == 1)
                //         group new { nhomdv, nhapdct, dv, nhapd }
                //         by new { nhomdv.TenNhom, nhapd.IDNhap, nhapd.SoCT, dv.TenDV, dv.NuocSX, nhapdct.SoLo, nhapdct.HanDung, dv.DonVi, nhapdct.DonGia } into kq
                //         select new
                //         {
                //             IDNhap = kq.Key.IDNhap,
                //             TenNhomDuoc = kq.Key.TenNhom,
                //             SoCT = kq.Key.SoCT,
                //             TenDuoc = kq.Key.TenDV,
                //             DonVi = kq.Key.DonVi,
                //             SoKS = kq.Key.SoLo,
                //             NuocSX = kq.Key.NuocSX,
                //             HanDung = kq.Key.HanDung,
                //             DonGia = kq.Key.DonGia,
                //             SoLuong = kq.Sum(p => p.nhapdct.SoLuongN),
                //             ThanhTien = kq.Sum(p => p.nhapdct.ThanhTienN)

                //         }).OrderBy(p=>p.IDNhap).ToList();
                if (q.Count > 0)
                {
                    rep.DataSource = q.ToList();
                    rep.BindingData();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                }
                //    else 
                //        MessageBox.Show("Không có dữ liệu");
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public class NhapDuoc
        {
            public int IDNhap { set; get; }
            public string SoCT { set; get; }
            public bool Check { set; get; }
            public DateTime? NgayNhap { get; set; }
        }

        private class Kho
        {
            public bool Chon { get; set; }
            public int MaKP { get; set; }
            public string TenKP { get; set; }
        }

        List<Kho> _lKho = new List<Kho>();
        private void frmTsSoKiemNhap_Load(object sender, EventArgs e)
        {
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var kho = (from k in data.KPhongs
                       where (DungChung.Bien.MaBV.Equals("04016") ? (k.TenKP.ToLower().Contains("kho chính") || k.TenKP.ToLower().Contains("kho đông y") || k.TenKP.ToLower().Contains("kho xã")) :
                       k.PLoai == "Khoa dược")
                       select new { k.MaKP, k.TenKP }).ToList();
            deTuNgay.DateTime = DateTime.Now;
            deDenNgay.DateTime = DateTime.Now;
            if (kho.Count > 0)
            {
                Kho moi = new Kho();
                moi.TenKP = "Chọn tất cả";
                moi.MaKP = 0;
                moi.Chon = true;
                _lKho.Add(moi);
                foreach (var a in kho)
                {
                    Kho themmoi = new Kho();
                    themmoi.TenKP = a.TenKP;
                    themmoi.MaKP = a.MaKP;
                    themmoi.Chon = true;
                    _lKho.Add(themmoi);
                }
                grcKhoaphong.DataSource = _lKho.ToList();
            }
           

            var qtencb = (from cb in data.CanBoes
                          select new { cb.MaCB, cb.TenCB }).ToList();
            if (qtencb.Count > 0)
            {
                lupTV1.Properties.DataSource = qtencb.ToList();
                lupTV2.Properties.DataSource = qtencb.ToList();
                lupTV3.Properties.DataSource = qtencb.ToList();
                lupTV4.Properties.DataSource = qtencb.ToList();
                lupTV5.Properties.DataSource = qtencb.ToList();
            }
          
        }

        void LoadDSChungTu()
        {
            DateTime _TuNgay = DateTime.Now;
            DateTime _DenNgay = DateTime.Now;
            _TuNgay = DungChung.Ham.NgayTu(deTuNgay.DateTime);
            _DenNgay = DungChung.Ham.NgayDen(deDenNgay.DateTime);

            bool NhapHD = ckNhapHD.Checked;

            //var khochon = _lKho.Where(p => p.Chon == true && p.MaKP > 0).ToList();
           

            var q = (from nd in data.NhapDs.Where(p => p.PLoai == 1).Where(p => p.NgayNhap >= _TuNgay && p.NgayNhap <= _DenNgay).Where(p => NhapHD == true ? p.KieuDon == 1 : true)
                     select new
                     {
                         SoCT = nd.SoCT,
                         IDNhap = nd.IDNhap,
                         NgayNhap = nd.NgayNhap,
                         MaKP = nd.MaKP
                     }).OrderByDescending(p => p.NgayNhap).ToList();
            List<int> lkp = new List<int>();

            for (int i = 0; i < grvKhoaphong.RowCount; i++)
            {
                if (grvKhoaphong.GetRowCellValue(i, "Chon") != null && grvKhoaphong.GetRowCellValue(i, "Chon").ToString() == "True")
                    lkp.Add(Convert.ToInt32(grvKhoaphong.GetRowCellValue(i, "MaKP")));
            }
            
            
            
            
            //if (makps > 0)
            //{
            //    if (b == false)
            //        lkp.Add(makps);
            //    else
            //        lkp.Remove(makps);
            //}
            List<NhapDuoc> lnhap = (from a in q
                                    join kp in lkp on a.MaKP equals kp
                                    select new NhapDuoc { SoCT = a.SoCT, IDNhap = a.IDNhap, NgayNhap = a.NgayNhap }).OrderByDescending(p => p.NgayNhap).OrderByDescending(p => p.IDNhap).ToList();

            lnhap.Insert(0, new NhapDuoc { IDNhap = 0, SoCT = "Tất cả" });
            grcChungTu.DataSource = null;
            grcChungTu.DataSource = lnhap;
            for (int i = 0; i < grvChungTu.RowCount; i++)
            {
                grvChungTu.SetRowCellValue(i, colCheckGrvKP, true);
            }
        }
        int makps = 0;
        bool b = false;
        private void grvChungTu_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "colCheckGrvKP")
            {

                if (e.RowHandle == 0)
                {
                    if (grvChungTu.GetFocusedRowCellValue(colCheckGrvKP) != null)
                    {
                        if (grvChungTu.GetRowCellValue(0, colCheckGrvKP).ToString() == "False")
                        {
                            for (int i = 0; i < grvChungTu.RowCount; i++)
                            {
                                grvChungTu.SetRowCellValue(i, "Check", true);
                            }
                        }
                        else
                        {
                            for (int i = 0; i < grvChungTu.RowCount; i++)
                            {
                                grvChungTu.SetRowCellValue(i, "Check", false);
                            }
                        }

                    }
                }
                else
                {
                    for (int i = 0; i < grvChungTu.RowCount; i++)
                    {
                        if (grvChungTu.GetRowCellValue(i, colCheckGrvKP) != null && grvChungTu.GetRowCellValue(i, colCheckGrvKP).ToString() == "True")
                        {
                            grvChungTu.SetRowCellValue(0, colCheckGrvKP, false);
                            break;
                        }
                    }

                }
                
            }
        }

        private void grvKhoaphong_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            
        }

        private void dateEdit2_EditValueChanged(object sender, EventArgs e)
        {
            LoadDSChungTu();
        }

        private void ckNhapHD_CheckedChanged(object sender, EventArgs e)
        {
            LoadDSChungTu();
        }

        private void deTuNgay_EditValueChanged(object sender, EventArgs e)
        {
            LoadDSChungTu();
        }

        private void grvKhoaphong_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            //if (e.Column.Name == "colchon")
            //{
            //    LoadDSChungTu();
            //}
        }

        private void grvKhoaphong_CellValueChanging_1(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "colchon")
            {
                if (grvKhoaphong.GetFocusedRowCellValue("TenKP") != null)
                {
                    string Ten = grvKhoaphong.GetFocusedRowCellValue("TenKP").ToString();

                    if (Ten == "Chọn tất cả")
                    {
                        if (_lKho.First().Chon == true)
                        {
                            foreach (var a in _lKho)
                            {
                                a.Chon = false;
                            }
                        }
                        else
                        {
                            foreach (var a in _lKho)
                            {
                                a.Chon = true;
                            }
                        }
                        grcKhoaphong.DataSource = "";
                        grcKhoaphong.DataSource = _lKho.ToList();
                    }
                    else
                    {
                        int _MaKP = grvKhoaphong.GetFocusedRowCellValue(MaKP) == null ? 0 : Convert.ToInt32(grvKhoaphong.GetFocusedRowCellValue(MaKP));
                        if (_lKho.Where(p => p.MaKP == _MaKP).Count() > 0)
                        {
                            Kho sua = _lKho.Where(p => p.MaKP == _MaKP).First();
                            if (e.Column == colchon) // Nếu click vào cột Chọn  
                            {
                                if (sua.Chon == false)
                                {
                                    sua.Chon = true;
                                }
                                else if (sua.Chon == true)
                                {
                                    sua.Chon = false;
                                }
                            }

                        }
                    }
                }
                //var test = _lThuocTon.Where(p => p.chon == true).ToList();
                //grvthuocton.FocusedColumn = colSluongT;
            }
            
            LoadDSChungTu();
            //if (e.Column.Name == "colchon")
            //{

            //    if (e.RowHandle == 0)
            //    {
            //        if (grvKhoaphong.GetFocusedRowCellValue(colchon) != null)
            //        {
            //            if (grvKhoaphong.GetRowCellValue(0, colchon).ToString() == "False")
            //            {
            //                for (int i = 0; i < grvKhoaphong.RowCount; i++)
            //                {
            //                    grvKhoaphong.SetRowCellValue(i, "Chon", true);
            //                }
            //            }
            //            else
            //            {
            //                for (int i = 0; i < grvKhoaphong.RowCount; i++)
            //                {
            //                    grvKhoaphong.SetRowCellValue(i, "Chon", false);
            //                }
            //            }

            //        }
            //    }
            //    else
            //    {
            //        for (int i = 0; i < grvKhoaphong.RowCount; i++)
            //        {
            //            if (grvKhoaphong.GetRowCellValue(i, colchon) != null && grvKhoaphong.GetRowCellValue(i, colchon).ToString() == "True")
            //            {
            //                grvKhoaphong.SetRowCellValue(0, colchon, false);
            //                break;
            //            }
            //        }
            //    }
            //    if(grvKhoaphong.GetFocusedRowCellValue(MaKP)!=null)
            //    {
            //        makps = Convert.ToInt32(grvKhoaphong.GetFocusedRowCellValue(MaKP));
            //        b = Convert.ToBoolean(grvKhoaphong.GetFocusedRowCellValue(colchon));
            //        deTuNgay_EditValueChanged(null, null);
            //    }
            //}
            //List<int> lkp = new List<int>();

            //for (int i = 0; i < grvKhoaphong.RowCount; i++)
            //{
            //    if (grvKhoaphong.GetRowCellValue(i, "Chon") != null && grvKhoaphong.GetRowCellValue(i, "Chon").ToString() == "True")
            //        lkp.Add(Convert.ToInt32(grvKhoaphong.GetRowCellValue(i, "MaKP")));
            //}
        }

        private void grvKhoaphong_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            //if (e.Column.Name == "colchon")
            //{
            //    LoadDSChungTu();
            //}

        }
    }
}