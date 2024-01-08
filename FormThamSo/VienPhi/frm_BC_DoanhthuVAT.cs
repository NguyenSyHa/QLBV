using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using COMExcel = Microsoft.Office.Interop.Excel;

namespace QLBV.FormThamSo
{
    public partial class frm_BC_DoanhthuVAT : DevExpress.XtraEditors.XtraForm
    {
        public frm_BC_DoanhthuVAT()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

        private bool KTtaoBcNXT()
        {
            if (dateTuNgay.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn ngày bắt đầu in báo cáo");
                dateTuNgay.Focus();
                return false;
            }
            if (dateDenNgay.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn đến ngày kết thúc in báo cáo");
                dateDenNgay.Focus();
                return false;
            }

            return true;
        }
        private class TieuNhom
        {
            private string TenTN;
            private int MaTN;
            private bool Chon;
            public string tenTN
            { set { TenTN = value; } get { return TenTN; } }
            public int maTN
            { set { MaTN = value; } get { return MaTN; } }
            public bool chon
            { set { Chon = value; } get { return Chon; } }
        }
        List<TieuNhom> _lTieunhom = new List<TieuNhom>();

        private void frm_new_Load(object sender, EventArgs e)
        {
            dateTuNgay.DateTime = System.DateTime.Now;
            dateDenNgay.DateTime = System.DateTime.Now;
            var qtn = (from tn in data.TieuNhomDVs.Where(p => p.IDNhom == 4 || p.IDNhom == 5 || p.IDNhom == 6 || p.IDNhom == 7 || p.IDNhom == 10 || p.IDNhom == 11)
                       select new
                       {
                           tn.TenTN,
                           tn.IdTieuNhom
                       }).Distinct().OrderBy(p => p.TenTN).ToList();
            if (qtn.Count() > 0)
            {
                TieuNhom themmoi1 = new TieuNhom();
                themmoi1.tenTN = "Chọn tất cả";
                themmoi1.maTN = 0;
                themmoi1.chon = true;
                _lTieunhom.Add(themmoi1);
                foreach (var a in qtn)
                {
                    TieuNhom themmoi = new TieuNhom();
                    themmoi.tenTN = a.TenTN;
                    themmoi.maTN = a.IdTieuNhom;
                    themmoi.chon = true;
                    _lTieunhom.Add(themmoi);
                }
                grcTieunhom.DataSource = _lTieunhom.ToList();
            }
            List<KPhong> q = data.KPhongs.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.KhoaDuoc).ToList();
            q.Insert(0, new KPhong { MaKP = 0, TenKP = "Tất cả" });
            lupMaKho.Properties.DataSource = q.ToList();

            List<KPhong> khoa = data.KPhongs.Where(p => p.TrongBV == 1).ToList();
            khoa.Insert(0, new KPhong { MaKP = 0, TenKP = "Tất cả" });
            lupKhoa.Properties.DataSource = khoa.ToList();

            lupMaKho.EditValue = lupMaKho.Properties.GetKeyValueByDisplayText("Tất cả");
            lupKhoa.EditValue = lupKhoa.Properties.GetKeyValueByDisplayText("Tất cả");
            if (DungChung.Bien.MaBV == "27022")
                ckcDonGiaVat.Visible = true;
        }

        private void btnInBC_Click(object sender, EventArgs e)
        {
            DateTime tungay = System.DateTime.Now.Date;
            DateTime denngay = System.DateTime.Now.Date;
            if (KTtaoBcNXT())
            {
                tungay = DungChung.Ham.NgayTu(dateTuNgay.DateTime);//dateTuNgay.DateTime;
                denngay = DungChung.Ham.NgayDen(dateDenNgay.DateTime);//dateDenNgay.DateTime;
                QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                if (tungay <= denngay)
                {
                    int maKP = 0;
                    if (lupMaKho.EditValue != null)
                        maKP = Convert.ToInt32(lupMaKho.EditValue);
                    //string maCC = "";
                    int maBPhan = 0;
                    if (lupKhoa.Enabled == true && lupKhoa.EditValue != null)
                        maBPhan = Convert.ToInt32(lupKhoa.EditValue);
                    List<TieuNhom> dstn = new List<TieuNhom>();
                    var qdv = (from dv in data.DichVus.Where(p => p.PLoai == 1)
                               select new
                               {
                                   dv.MaDV,
                                   dv.TenDV,
                                   dv.IdTieuNhom
                               }).ToList();
                    dstn = _lTieunhom.Where(p => p.chon == true && p.maTN > 0).ToList();
                    var qdvtn = (from dv in qdv
                                 join tn in dstn on dv.IdTieuNhom equals tn.maTN
                                 select new
                                 {
                                     dv.MaDV,
                                     dv.TenDV,
                                     tn.maTN
                                 }).ToList();
                    var list1 = (from nd in data.NhapDs.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && (maKP == 0 || p.MaKP == maKP) && (maBPhan == 0 || p.MaKPnx == maBPhan) && p.PLoai == 2 && ((DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297") ? (p.KieuDon != 2 && p.KieuDon != 7) : p.KieuDon != 2))
                                 join ndct in data.NhapDcts on nd.IDNhap equals ndct.IDNhap
                                 select new
                                 {
                                     ndct.MaDV,
                                     ndct.DonVi,
                                     ndct.DonGia,
                                     ndct.SoLuongX,
                                     nd.PLoai,
                                     nd.KieuDon,
                                     ndct.ThanhTienX
                                 }).ToList();
                    var q1 = (from a in list1
                              group new { a } by new { a.MaDV, a.DonVi, a.DonGia } into kq
                              select new
                              {
                                  kq.Key.MaDV,
                                  kq.Key.DonVi,
                                  kq.Key.DonGia,
                                  SoLuongX = kq.Sum(p => p.a.SoLuongX),
                                  ThanhTienX = kq.Sum(p => p.a.ThanhTienX)
                              }).ToList();

                    if (DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297")
                    {
                        var list2 = (from nd in data.NhapDs.Where(p => p.PLoai == 1 && (p.KieuDon == 3 || p.KieuDon == 1))// lấy theo nhập tồn (riêng bv 27183),  bv khác lấy nhập theo hóa đơn
                                     join ndct in data.NhapDcts on nd.IDNhap equals ndct.IDNhap
                                     join dv in data.DichVus.Where(o => o.GiaNhap != null && o.GiaNhap > 0) on ndct.MaDV equals dv.MaDV
                                     select new
                                     {
                                         ndct.MaDV,
                                         ndct.DonGia,
                                         ndct.DonVi,
                                         ndct.DonGiaCT,
                                         dv.GiaNhap,
                                         ndct.VAT,
                                         ndct.SoLuongN
                                     }).ToList();
                        var list3 = (from l in list2
                                     group l by new { l.MaDV, l.DonGia, l.DonVi, l.GiaNhap } into kq
                                     select new { kq.Key.GiaNhap, kq.Key.MaDV, kq.Key.DonGia, kq.Key.DonVi, DonGiaCT = kq.Key.GiaNhap ?? 0 }).Distinct().ToList();

                        var list = (from l1 in list3
                                    join l2 in q1 on new { l1.MaDV, l1.DonGia } equals new { l2.MaDV, l2.DonGia } into kq
                                    from kq1 in kq.DefaultIfEmpty()
                                    select new
                                    {
                                        l1,
                                        SoLuongX = kq1 != null ? kq1.SoLuongX : 0
                                    }).ToList();
                        if (DungChung.Bien.MaBV == "27022")
                        {
                            list = (from l1 in list3
                                    join l2 in q1 on new { l1.MaDV, l1.DonGia } equals new { l2.MaDV, l2.DonGia }
                                    select new
                                    {
                                        l1,
                                        SoLuongX = l2.SoLuongX
                                    }).ToList();
                        }

                        var qTH = (from l in list
                                   join dvtn in qdvtn on l.l1.MaDV equals dvtn.MaDV
                                   group new { l, dvtn } by new { dvtn.MaDV, dvtn.TenDV, l.l1.DonGia, l.l1.DonGiaCT, l.l1.DonVi } into kq
                                   select new
                                   {
                                       kq.Key.MaDV,
                                       kq.Key.TenDV,
                                       kq.Key.DonGia,
                                       kq.Key.DonVi,
                                       Dongiachuathue = Math.Round(kq.Key.DonGiaCT),
                                       Soluongxuat = kq.Sum(p => p.l.SoLuongX),
                                       Thanhtienthu = Convert.ToDouble((kq.Sum(p => p.l.SoLuongX) * kq.Key.DonGia)),
                                       Thanhtiennhap = kq.Sum(p => p.l.SoLuongX) * Math.Round(kq.Key.DonGiaCT),
                                       Doanhthu = Math.Round(kq.Sum(p => p.l.SoLuongX) * (kq.Key.DonGia - Math.Round(kq.Key.DonGiaCT)), 2)
                                   }).OrderBy(p => p.TenDV).ToList();

                        BaoCao.rep_BC_DoanhthuVAT rep = new BaoCao.rep_BC_DoanhthuVAT(ckcHTDoanhThu.Checked);
                        frmIn frm = new frmIn();
                        rep.DataSource = qTH.OrderBy(p => p.TenDV);
                        rep.TuNgayDenNgay.Text = "Từ ngày " + dateTuNgay.Text + " Đến ngày " + dateDenNgay.Text;
                        rep.BindingData();
                        rep.CreateDocument();
                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                        frm.ShowDialog();
                        if (Xuatex.Checked)
                        {
                            bool chonFont = false;
                            if (rdFont.SelectedIndex == 0)
                                chonFont = true;
                            else
                                chonFont = false;
                            string font = "TCVN3";
                            COMExcel.Application exApp = new COMExcel.Application();
                            COMExcel.Workbook exQLBV = exApp.Workbooks.Add(
                                      COMExcel.XlWBATemplate.xlWBATWorksheet);
                            COMExcel.Worksheet exSheet = (COMExcel.Worksheet)exQLBV.Worksheets[1];

                            exSheet.Name = "Danh sách bệnh nhân nghỉ ốm";

                            // mảng tên cột--------------------------------------------------------------------------------------------------------
                            string[] _arr = new string[] { "STT", "Ten_thuoc", "DVT", "Don_gia_binh_quan", "Gia_ban", "Tong_ban", "Tong_von_ban_dau", "Doanh_thu_ban_hang", "Loi_nhuan" };

                            // add tên cột vào sheet exSheet---------------------------------------------------------------------------------------
                            int k = 0;
                            foreach (var b in _arr)
                            {
                                k++;
                                COMExcel.Range r = (COMExcel.Range)exSheet.Cells[1, k];
                                r.Value2 = DungChung.Ham.convertFont(chonFont, b.ToString(), font);
                                r.Columns.AutoFit();
                                r.Cells.Font.Bold = true;
                            }
                            int row = qTH.Count;
                            int col = _arr.Length;
                            Object[,] _arr2 = new Object[row, col]; // tạo ra mảng count, col phần tử
                            if (row > 0)
                            {
                                int num = 0;
                                int stt = 1;
                                foreach (var item in qTH)
                                {
                                    _arr2[num, 0] = stt;
                                    _arr2[num, 1] = item.TenDV;
                                    _arr2[num, 2] = item.DonVi;
                                    _arr2[num, 3] = item.Dongiachuathue;
                                    _arr2[num, 4] = item.DonGia;
                                    _arr2[num, 5] = item.Soluongxuat;
                                    _arr2[num, 6] = item.Thanhtiennhap;
                                    _arr2[num, 7] = item.Thanhtienthu;
                                    _arr2[num, 8] = item.Doanhthu;
                                    stt++;
                                    num++;
                                }

                                //-------------------------------------------------------------------------------------------
                                exSheet.Range[exSheet.Cells[2, 1], exSheet.Cells[row + 1, col]].Value = _arr2;
                                exApp.Visible = true;
                                try
                                {

                                    exQLBV.SaveAs(txtFilePath.Text, COMExcel.XlFileFormat.xlWorkbookNormal,
                                                    null, null, false, false,
                                                    COMExcel.XlSaveAsAccessMode.xlExclusive,
                                                    false, false, false, false, false);
                                }
                                catch (Exception ex)
                                {

                                }
                                finally
                                {
                                    System.Runtime.InteropServices.Marshal.ReleaseComObject(exQLBV);
                                    System.Runtime.InteropServices.Marshal.ReleaseComObject(exApp);
                                }

                            }
                        }
                    }
                    else
                    {
                        if (ckcHTDoanhThu.Checked)
                        {
                            var qTH = (from l in q1
                                       join dvtn in qdvtn on l.MaDV equals dvtn.MaDV
                                       group new { l, dvtn } by new { dvtn.MaDV, dvtn.TenDV, l.DonGia, l.DonVi } into kq
                                       select new
                                       {
                                           kq.Key.MaDV,
                                           kq.Key.TenDV,
                                           kq.Key.DonGia,
                                           kq.Key.DonVi,
                                           Soluongxuat = kq.Sum(p => p.l.SoLuongX),
                                           Thanhtienthu = Convert.ToDouble((kq.Sum(p => p.l.SoLuongX) * kq.Key.DonGia))
                                       }).OrderBy(p => p.TenDV).ToList();
                            if (qTH.Count() > 0)
                            {
                                BaoCao.rep_BC_DoanhthuVAT rep = new BaoCao.rep_BC_DoanhthuVAT(ckcHTDoanhThu.Checked);
                                frmIn frm = new frmIn();
                                rep.DataSource = qTH.OrderBy(p => p.TenDV);
                                rep.TuNgayDenNgay.Text = "Từ ngày " + dateTuNgay.Text + " Đến ngày " + dateDenNgay.Text;
                                rep.BindingData();
                                rep.CreateDocument();
                                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                frm.ShowDialog();

                                if (Xuatex.Checked)
                                {
                                    bool chonFont = false;
                                    if (rdFont.SelectedIndex == 0)
                                        chonFont = true;
                                    else
                                        chonFont = false;
                                    string font = "TCVN3";
                                    COMExcel.Application exApp = new COMExcel.Application();
                                    COMExcel.Workbook exQLBV = exApp.Workbooks.Add(
                                              COMExcel.XlWBATemplate.xlWBATWorksheet);
                                    COMExcel.Worksheet exSheet = (COMExcel.Worksheet)exQLBV.Worksheets[1];

                                    exSheet.Name = "Danh sách bệnh nhân nghỉ ốm";

                                    // mảng tên cột--------------------------------------------------------------------------------------------------------
                                    string[] _arr = new string[] { "STT", "Ten_thuoc", "DVT", "Don_gia_binh_quan", "Gia_ban", "Tong_ban", "Tong_von_ban_dau", "Doanh_thu_ban_hang", "Loi_nhuan" };

                                    // add tên cột vào sheet exSheet---------------------------------------------------------------------------------------
                                    int k = 0;
                                    foreach (var b in _arr)
                                    {
                                        k++;
                                        COMExcel.Range r = (COMExcel.Range)exSheet.Cells[1, k];
                                        r.Value2 = DungChung.Ham.convertFont(chonFont, b.ToString(), font);
                                        r.Columns.AutoFit();
                                        r.Cells.Font.Bold = true;
                                    }
                                    int row = qTH.Count;
                                    int col = _arr.Length;
                                    Object[,] _arr2 = new Object[row, col]; // tạo ra mảng count, col phần tử
                                    if (row > 0)
                                    {
                                        int num = 0;
                                        int stt = 1;
                                        foreach (var item in qTH)
                                        {
                                            _arr2[num, 0] = stt;
                                            _arr2[num, 1] = item.TenDV;
                                            _arr2[num, 2] = item.DonVi;
                                            //_arr2[num, 3] = item.MA_BV;
                                            _arr2[num, 4] = item.DonGia;
                                            _arr2[num, 5] = item.Soluongxuat;
                                            _arr2[num, 7] = item.Thanhtienthu;
                                            stt++;
                                            num++;
                                        }

                                        //-------------------------------------------------------------------------------------------
                                        exSheet.Range[exSheet.Cells[2, 1], exSheet.Cells[row + 1, col]].Value = _arr2;
                                        exApp.Visible = true;
                                        try
                                        {

                                            exQLBV.SaveAs(txtFilePath.Text, COMExcel.XlFileFormat.xlWorkbookNormal,
                                                            null, null, false, false,
                                                            COMExcel.XlSaveAsAccessMode.xlExclusive,
                                                            false, false, false, false, false);
                                        }
                                        catch (Exception ex)
                                        {

                                        }
                                        finally
                                        {
                                            System.Runtime.InteropServices.Marshal.ReleaseComObject(exQLBV);
                                            System.Runtime.InteropServices.Marshal.ReleaseComObject(exApp);
                                        }

                                    }
                                }
                            }
                        }
                        else
                        {
                            var list2 = (from nd in data.NhapDs.Where(p => p.PLoai == 1 && (p.KieuDon == 3 || p.KieuDon == 1))// lấy theo nhập tồn (riêng bv 27183),  bv khác lấy nhập theo hóa đơn
                                         join ndct in data.NhapDcts on nd.IDNhap equals ndct.IDNhap
                                         select new
                                         {
                                             ndct.MaDV,
                                             ndct.DonGia,
                                             ndct.DonVi,
                                             ndct.DonGiaCT,
                                             ndct.VAT,
                                             ndct.SoLuongN
                                         }).ToList();
                            var list3 = (from l in list2
                                         group l by new { l.MaDV, l.DonGia, l.DonVi } into kq
                                         select new { kq.Key.MaDV, kq.Key.DonGia, kq.Key.DonVi, DonGiaCT = ckcDonGiaVat.Checked == true ? kq.Sum(p => (Math.Round(p.DonGiaCT * (100 + p.VAT) / 100, DungChung.Bien.LamTronSo, MidpointRounding.AwayFromZero)) * p.SoLuongN) / kq.Sum(p => p.SoLuongN) : kq.Sum(p => p.DonGiaCT * p.SoLuongN) / kq.Sum(p => p.SoLuongN) }).Distinct().ToList();

                            var list = (from l1 in list3
                                        join l2 in q1 on new { l1.MaDV, l1.DonGia } equals new { l2.MaDV, l2.DonGia } into kq
                                        from kq1 in kq.DefaultIfEmpty()
                                        select new
                                        {
                                            l1,
                                            SoLuongX = kq1 != null ? kq1.SoLuongX : 0
                                        }).ToList();
                            if (DungChung.Bien.MaBV == "27022")
                            {
                                list = (from l1 in list3
                                        join l2 in q1 on new { l1.MaDV, l1.DonGia } equals new { l2.MaDV, l2.DonGia }
                                        select new
                                        {
                                            l1,
                                            SoLuongX = l2.SoLuongX
                                        }).ToList();
                            }

                            var qTH = (from l in list

                                       join dvtn in qdvtn on l.l1.MaDV equals dvtn.MaDV
                                       group new { l, dvtn } by new { dvtn.MaDV, dvtn.TenDV, l.l1.DonGia, l.l1.DonGiaCT, l.l1.DonVi } into kq
                                       select new
                                       {
                                           kq.Key.MaDV,
                                           kq.Key.TenDV,
                                           kq.Key.DonGia,
                                           kq.Key.DonVi,
                                           Dongiachuathue = Math.Round(kq.Key.DonGiaCT),
                                           Soluongxuat = kq.Sum(p => p.l.SoLuongX),
                                           Thanhtienthu = Convert.ToDouble((kq.Sum(p => p.l.SoLuongX) * kq.Key.DonGia)),
                                           Thanhtiennhap = kq.Sum(p => p.l.SoLuongX) * Math.Round(kq.Key.DonGiaCT),
                                           Doanhthu = Math.Round(kq.Sum(p => p.l.SoLuongX) * (kq.Key.DonGia - Math.Round(kq.Key.DonGiaCT)), 2)

                                       }).OrderBy(p => p.TenDV).ToList();

                            BaoCao.rep_BC_DoanhthuVAT rep = new BaoCao.rep_BC_DoanhthuVAT(ckcHTDoanhThu.Checked);
                            frmIn frm = new frmIn();
                            rep.DataSource = qTH.OrderBy(p => p.TenDV);
                            rep.TuNgayDenNgay.Text = "Từ ngày " + dateTuNgay.Text + " Đến ngày " + dateDenNgay.Text;
                            rep.BindingData();
                            rep.CreateDocument();
                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                            frm.ShowDialog();
                            if (Xuatex.Checked)
                            {
                                bool chonFont = false;
                                if (rdFont.SelectedIndex == 0)
                                    chonFont = true;
                                else
                                    chonFont = false;
                                string font = "TCVN3";
                                COMExcel.Application exApp = new COMExcel.Application();
                                COMExcel.Workbook exQLBV = exApp.Workbooks.Add(
                                          COMExcel.XlWBATemplate.xlWBATWorksheet);
                                COMExcel.Worksheet exSheet = (COMExcel.Worksheet)exQLBV.Worksheets[1];

                                exSheet.Name = "Danh sách bệnh nhân nghỉ ốm";

                                // mảng tên cột--------------------------------------------------------------------------------------------------------
                                string[] _arr = new string[] { "STT", "Ten_thuoc", "DVT", "Don_gia_binh_quan", "Gia_ban", "Tong_ban", "Tong_von_ban_dau", "Doanh_thu_ban_hang", "Loi_nhuan" };

                                // add tên cột vào sheet exSheet---------------------------------------------------------------------------------------
                                int k = 0;
                                foreach (var b in _arr)
                                {
                                    k++;
                                    COMExcel.Range r = (COMExcel.Range)exSheet.Cells[1, k];
                                    r.Value2 = DungChung.Ham.convertFont(chonFont, b.ToString(), font);
                                    r.Columns.AutoFit();
                                    r.Cells.Font.Bold = true;
                                }
                                int row = qTH.Count;
                                int col = _arr.Length;
                                Object[,] _arr2 = new Object[row, col]; // tạo ra mảng count, col phần tử
                                if (row > 0)
                                {
                                    int num = 0;
                                    int stt = 1;
                                    foreach (var item in qTH)
                                    {
                                        _arr2[num, 0] = stt;
                                        _arr2[num, 1] = item.TenDV;
                                        _arr2[num, 2] = item.DonVi;
                                        _arr2[num, 3] = item.Dongiachuathue;
                                        _arr2[num, 4] = item.DonGia;
                                        _arr2[num, 5] = item.Soluongxuat;
                                        _arr2[num, 6] = item.Thanhtiennhap;
                                        _arr2[num, 7] = item.Thanhtienthu;
                                        _arr2[num, 8] = item.Doanhthu;
                                        stt++;
                                        num++;
                                    }

                                    //-------------------------------------------------------------------------------------------
                                    exSheet.Range[exSheet.Cells[2, 1], exSheet.Cells[row + 1, col]].Value = _arr2;
                                    exApp.Visible = true;
                                    try
                                    {

                                        exQLBV.SaveAs(txtFilePath.Text, COMExcel.XlFileFormat.xlWorkbookNormal,
                                                        null, null, false, false,
                                                        COMExcel.XlSaveAsAccessMode.xlExclusive,
                                                        false, false, false, false, false);
                                    }
                                    catch (Exception ex)
                                    {

                                    }
                                    finally
                                    {
                                        System.Runtime.InteropServices.Marshal.ReleaseComObject(exQLBV);
                                        System.Runtime.InteropServices.Marshal.ReleaseComObject(exApp);
                                    }

                                }
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Không thể chọn từ ngày lớn hơn đến ngày. Mời bạn chọn lại!!!");
                }
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void grvTieunhom_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "colChon")
            {
                if (grvTieunhom.GetFocusedRowCellValue("tenTN") != null)
                {
                    string Ten = grvTieunhom.GetFocusedRowCellValue("tenTN").ToString();

                    if (Ten == "Chọn tất cả")
                    {
                        if (_lTieunhom.First().chon == true)
                        {
                            foreach (var a in _lTieunhom)
                            {
                                a.chon = false;
                            }
                        }
                        else
                        {
                            foreach (var a in _lTieunhom)
                            {
                                a.chon = true;
                            }
                        }
                        grcTieunhom.DataSource = "";
                        grcTieunhom.DataSource = _lTieunhom.ToList();
                    }
                }
            }
        }
        SaveFileDialog dialog = new SaveFileDialog();
        private void btnChonFilePath_Click(object sender, EventArgs e)
        {
            DateTime _ngay = System.DateTime.Now.Date;
            dialog.InitialDirectory = "C:\\";
            dialog.Filter = "Excel files (*.xls or *.xlsx)|*.xls;*.xlsx";
            dialog.FilterIndex = 1;
            dialog.FileName = "BCDoanhThuVAT_" + DungChung.Bien.MaBV + "_" + _ngay.Year + "_" + _ngay.Month + ".xls";
            dialog.RestoreDirectory = true;
            dialog.CheckFileExists = false;
            dialog.CheckPathExists = false;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                txtFilePath.Text = dialog.FileName;
            }
        }

        private void Xuatex_CheckedChanged(object sender, EventArgs e)
        {
            DateTime _ngay = System.DateTime.Now.Date;
            // txtFilePath.Enabled = Xuatex.Checked;
            btnChonFilePath.Enabled = Xuatex.Checked;
            if (Xuatex.Checked)
                txtFilePath.Text = "C:\\BCDoanhThuVAT_" + DungChung.Bien.MaBV + "_" + _ngay.Year + "_" + _ngay.Month + ".xls";
            else
                txtFilePath.Text = "";
        }

        private void ckcHTDoanhThu_CheckedChanged(object sender, EventArgs e)
        {
            //if(ckcHTDoanhThu.Checked)
            //{
            //    panelControl1.Visible = true;
            //}
            //else
            //{
            //    Xuatex.Checked = false;
            //    panelControl1.Visible = false;
            //}
        }
    }
}