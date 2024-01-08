using System;
using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using COMExcel = Microsoft.Office.Interop.Excel;

namespace QLBV.FormThamSo
{
    public partial class frmTsBCNXT : DevExpress.XtraEditors.XtraForm
    {
        public frmTsBCNXT()
        {
            InitializeComponent();
        }
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
            if ((radMauIn.SelectedIndex == 1 || radMauIn.SelectedIndex == 2) && checkEdit1.Checked == false)
            {
                if (cbo_cot1.EditValue == null && DungChung.Bien.MaBV != "01049")
                {
                    MessageBox.Show("Bạn chưa chọn cột 1");
                    cbo_cot1.Focus();
                    return false;
                }
                if (cbo_cot2.EditValue == null && DungChung.Bien.MaBV != "01049")
                {
                    MessageBox.Show("Bạn chưa chọn cột 2");
                    cbo_cot2.Focus();
                    return false;
                }
                if (cbo_cot3.EditValue == null && (DungChung.Bien.MaBV == "26007" || (DungChung.Bien.MaBV == "30004" && check30004.Checked == true)) && radLoaiXuat.SelectedIndex == 0 && radMauIn.SelectedIndex == 1)
                {
                    MessageBox.Show("Bạn chưa chọn cột 3");
                    cbo_cot3.Focus();
                    return false;
                }
                if (cbo_cot4.EditValue == null && DungChung.Bien.MaBV == "30004" && check30004.Checked == true && radLoaiXuat.SelectedIndex == 0 && radMauIn.SelectedIndex == 1)
                {
                    MessageBox.Show("Bạn chưa chọn cột4");
                    cbo_cot4.Focus();
                    return false;
                }
            }
            if ((cbo_cot1.EditValue != null && cbo_cot2.EditValue != null && cbo_cot3.EditValue != null && cbo_cot4.EditValue != null && DungChung.Bien.MaBV == "30004" && check30004.Checked == true) && checkEdit1.Checked == false)
            {
                if (Convert.ToInt32(cbo_cot1.EditValue) == Convert.ToInt32(cbo_cot2.EditValue) || Convert.ToInt32(cbo_cot1.EditValue) == Convert.ToInt32(cbo_cot3.EditValue)
                    || Convert.ToInt32(cbo_cot1.EditValue) == Convert.ToInt32(cbo_cot4.EditValue) || Convert.ToInt32(cbo_cot2.EditValue) == Convert.ToInt32(cbo_cot3.EditValue)
                    || Convert.ToInt32(cbo_cot2.EditValue) == Convert.ToInt32(cbo_cot4.EditValue) || Convert.ToInt32(cbo_cot3.EditValue) == Convert.ToInt32(cbo_cot4.EditValue))
                {
                    MessageBox.Show("Các cột không được giống nhau");
                    cbo_cot1.Focus();
                    return false;
                }
                else return true;
            }
            if ((cbo_cot1.EditValue != null && cbo_cot2.EditValue != null && cbo_cot3.EditValue != null && DungChung.Bien.MaBV == "26007") && checkEdit1.Checked == false)
            {
                if (Convert.ToInt32(cbo_cot2.EditValue) == Convert.ToInt32(cbo_cot1.EditValue) || Convert.ToInt32(cbo_cot2.EditValue) == Convert.ToInt32(cbo_cot3.EditValue) || Convert.ToInt32(cbo_cot3.EditValue) == Convert.ToInt32(cbo_cot1.EditValue))
                {
                    MessageBox.Show("Các cột không được giống nhau");
                    cbo_cot1.Focus();
                    return false;
                }
                else return true;
            }
            else if ((cbo_cot1.EditValue != null && cbo_cot2.EditValue != null) && checkEdit1.Checked == false)
            {
                if (Convert.ToInt32(cbo_cot2.EditValue) == Convert.ToInt32(cbo_cot1.EditValue))
                {
                    MessageBox.Show("Các cột không được giống nhau");
                    cbo_cot1.Focus();
                    return false;
                }
                else return true;
            }
            //if (lupKho.EditValue == null)
            //{
            //    MessageBox.Show("Bạn chưa chọn khoa phòng để in báo cáo");
            //    lupKho.Focus();
            //    return false;
            //}
            else return true;
        }
        private void btnInBC_Click(object sender, EventArgs e)
        {
            #region Bệnh viên 24009
            if (DungChung.Bien.MaBV == "24009" && checkEdit1.Checked == true)
            {
                string _maCQCQ = "";
                DateTime tungay = System.DateTime.Now.Date;
                DateTime denngay = System.DateTime.Now.Date;
                int idnhom = -1, idtieunhom = -1;
                string _tenkho = "", _tenNCC = "";
                List<KPhong> _kpChon = new List<KPhong>();
                List<cot> _cotchon = new List<cot>();
                QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                var qCQCQ = data.BenhViens.Where(panelControl1 => panelControl1.MaBV == DungChung.Bien.MaBV).FirstOrDefault();
                if (qCQCQ != null)
                    _maCQCQ = qCQCQ.MaChuQuan;
                if (KTtaoBcNXT())
                {
                    for (int i = 0; i < cklKP.ItemCount; i++)
                    {
                        if (cklKP.GetItemChecked(i))
                            _kpChon.Add(new KPhong { TenKP = cklKP.GetItemText(i), MaKP = cklKP.GetItemValue(i) == null ? 0 : Convert.ToInt32(cklKP.GetItemValue(i)) });
                    }
                    for (int i = 0; i < cklCot.ItemCount; i++)
                    {
                        if (cklCot.GetItemChecked(i))
                            _cotchon.Add(new cot { TenCot = cklCot.GetItemText(i), MaCot = Convert.ToString(cklCot.GetItemValue(i)) });
                    }
                    string _nhacc = "";
                    if (lupNhaCC.EditValue != null)
                        _nhacc = lupNhaCC.EditValue.ToString();
                    tungay = DungChung.Ham.NgayTu(dateTuNgay.DateTime);//dateTuNgay.DateTime;//
                    denngay = DungChung.Ham.NgayDen(dateDenNgay.DateTime);//dateDenNgay.DateTime;//


                    if (lupNhom.EditValue != null)
                        idnhom = Convert.ToInt32(lupNhom.EditValue);
                    if (lupTieuNhom.EditValue != null)
                        idtieunhom = Convert.ToInt32(lupTieuNhom.EditValue);
                    foreach (var item in _kpChon)
                    {
                        _tenkho += item.TenKP + "; ";
                    }
                    // kiểm tra lại
                    var qtenncc = (from nhapd in data.NhapDs
                                   join nhacc in data.NhaCCs on nhapd.MaCC equals nhacc.MaCC
                                   where (nhacc.MaCC == _nhacc)
                                   select new { nhacc.TenCC }).ToList();
                    if (qtenncc.Count > 0)
                    {
                        _tenNCC = qtenncc.First().TenCC;
                    }
                    if (_kpChon.Count > 0)
                    {
                        var qnxt2 = (from nhapd in data.NhapDs//.Where(p=> (chk_mauToanVien.Checked || count==0) ? !(kpXP.Contains(p.MaKPnx??0)) : true)
                                     join nhapdct in data.NhapDcts on nhapd.IDNhap equals nhapdct.IDNhap
                                     where ((nhapd.PLoai == 1) || (nhapd.PLoai == 2) || (nhapd.PLoai == 3))
                                     //group new{nhapd,nhapdct} by new {}
                                     select new { nhapd.TraDuoc_KieuDon, nhapd.XuatTD, nhapd.MaKP, nhapd.MaKPnx, nhapdct.MaDV, nhapdct.IDDTBN, nhapd.PLoai, nhapd.KieuDon, nhapd.NgayNhap, nhapdct.DonGia, SoLuongN = nhapd.PLoai == 1 ? nhapdct.SoLuongN : 0, SoLuongX = (nhapd.PLoai == 2 || nhapd.PLoai == 3) ? nhapdct.SoLuongX : 0, ThanhTienN = nhapd.PLoai == 1 ? nhapdct.ThanhTienN : 0, ThanhTienX = (nhapd.PLoai == 2 || nhapd.PLoai == 3) ? nhapdct.ThanhTienX : 0 }).OrderByDescending(p => p.NgayNhap).ToList();
                        var dichvu = (from dv in data.DichVus
                                      join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                                      join nhomdv in data.NhomDVs on tn.IDNhom equals nhomdv.IDNhom
                                      where (idnhom == -1 ? true : nhomdv.IDNhom == idnhom) && (idtieunhom == -1 ? true : tn.IdTieuNhom == idtieunhom)
                                      select new { dv.NhaSX, dv.MaDV, TenDV = (DungChung.Bien.MaBV == "12122" || DungChung.Bien.MaBV == "30009") ? (dv.TenDV + " " + dv.HamLuong) : ((DungChung.Bien.MaBV == "24009" || _maCQCQ == "24009") ? (dv.TenRG ?? "") : dv.TenDV), tn.TenTN, nhomdv.TenNhom, tn.STT, dv.MaCC, dv.DonVi, dv.HamLuong, dv.TenHC, dv.MaTam, dv.SoDK, dv.NuocSX, dv.DuongD, dv.NhomThau, dv.MaQD, dv.GiaNhap }).ToList(); //Nước sx = 1? trong nước, 0: nước ngoài

                        var qnxt = (from a in qnxt2
                                    join dv in dichvu on a.MaDV equals dv.MaDV
                                    join kp in _kpChon//.Where(p=> chk_mauToanVien.Checked? kpBV.Contains(p.MaKP) : true) 
                                    on a.MaKP equals kp.MaKP
                                    join ncc in data.NhaCCs on dv.MaCC equals ncc.MaCC into k
                                    from k1 in k.DefaultIfEmpty()
                                    group new { a, _tenNCC, k1 } by new { dv.TenNhom, dv.HamLuong, DuongDung = dv.DuongD, HangSX = dv.NhaSX, dv.STT, dv.MaCC, dv.TenTN, dv.NuocSX, dv.TenDV, dv.DonVi, a.DonGia, a.MaDV, dv.MaTam, dv.SoDK, dv.NhomThau, dv.MaQD, dv.TenHC, TenCC = k1 != null ? k1.TenCC : "", dv.GiaNhap } into kq
                                    select new
                                    {
                                        kq.Key.TenCC,
                                        kq.Key.TenNhom,
                                        kq.Key.NuocSX,
                                        kq.Key.STT,
                                        kq.Key.TenTN,
                                        kq.Key.MaCC,
                                        kq.Key.HangSX,
                                        kq.Key.DuongDung,
                                        kq.Key.HamLuong,
                                        MaDV = kq.Key.MaDV,
                                        MaTam = kq.Key.MaTam,
                                        TenNhomDuoc = kq.Key.TenNhom,
                                        TenHamLuong = kq.Key.TenDV,
                                        DonVi = kq.Key.DonVi,
                                        DonGia = ckcGiaNhap.Checked ? kq.Key.GiaNhap : kq.Key.DonGia,
                                        TenHC = kq.Key.TenHC,
                                        kq.Key.NhomThau,
                                        kq.Key.SoDK,
                                        kq.Key.MaQD,
                                        NhaThau = kq.Key.TenCC,
                                        TonDKSL = kq.Where(p => p.a.NgayNhap < tungay).Sum(p => p.a.SoLuongN) - kq.Where(p => p.a.NgayNhap < tungay).Sum(p => p.a.SoLuongX),
                                        TonDKTT = ckcGiaNhap.Checked 
                                                    ? (kq.Key.GiaNhap == null ? 0 : kq.Key.GiaNhap * kq.Where(p => p.a.NgayNhap < tungay).Sum(p => p.a.SoLuongN) - kq.Where(p => p.a.NgayNhap < tungay).Sum(p => p.a.SoLuongX)) 
                                                    : (kq.Where(p => p.a.NgayNhap < tungay).Sum(p => p.a.ThanhTienN) - kq.Where(p => p.a.NgayNhap < tungay).Sum(p => p.a.ThanhTienX)),
                                        NhapTKSL = kq.Where(p => p.a.NgayNhap >= tungay).Where(p => p.a.NgayNhap <= denngay && p.a.PLoai == 1 && p.a.KieuDon != 2).Sum(p => p.a.SoLuongN),
                                        NhapTKTT = ckcGiaNhap.Checked 
                                                ? kq.Key.GiaNhap == null ? 0 : kq.Key.GiaNhap * kq.Where(p => p.a.NgayNhap >= tungay).Where(p => p.a.NgayNhap <= denngay && p.a.PLoai == 1 && p.a.KieuDon != 2).Sum(p => p.a.SoLuongN)
                                                : kq.Where(p => p.a.NgayNhap >= tungay).Where(p => p.a.NgayNhap <= denngay && p.a.PLoai == 1 && p.a.KieuDon != 2).Sum(p => p.a.ThanhTienN),
                                        NhapHD = kq.Where(p => p.a.NgayNhap >= tungay).Where(p => p.a.NgayNhap <= denngay && p.a.PLoai == 1 && p.a.KieuDon == 1).Sum(p => p.a.SoLuongN),
                                        NhapHDTT = kq.Where(p => p.a.NgayNhap >= tungay).Where(p => p.a.NgayNhap <= denngay && p.a.PLoai == 1 && p.a.KieuDon == 1).Sum(p => p.a.ThanhTienN),
                                        TongXuatTKSL = (((kq.Where(p => p.a.NgayNhap >= tungay).Where(p => p.a.NgayNhap <= denngay).Sum(p => p.a.SoLuongX) - kq.Where(p => p.a.NgayNhap >= tungay && p.a.NgayNhap <= denngay && p.a.PLoai == 1 && p.a.KieuDon == 2).Sum(p => p.a.SoLuongN)) > 0 || DungChung.Bien.MaBV == "27023" || DungChung.Bien.MaBV == "30005")) ? // || DungChung.Bien.MaBV == "30005" 
                                                       (kq.Where(p => p.a.NgayNhap >= tungay).Where(p => p.a.NgayNhap <= denngay).Sum(p => p.a.SoLuongX) - kq.Where(p => p.a.NgayNhap >= tungay && p.a.NgayNhap <= denngay && p.a.PLoai == 1 && p.a.KieuDon == 2).Sum(p => p.a.SoLuongN)) : 0,
                                        TongXuatTKTT = ckcGiaNhap.Checked 
                                                        ?(kq.Key.GiaNhap == null ? 0 : kq.Key.GiaNhap * kq.Where(p => p.a.NgayNhap >= tungay).Where(p => p.a.NgayNhap <= denngay).Sum(p => p.a.SoLuongX) - kq.Where(p => p.a.NgayNhap >= tungay && p.a.NgayNhap <= denngay && p.a.PLoai == 1 && p.a.KieuDon == 2).Sum(p => p.a.SoLuongN)) 
                                                        :(((kq.Where(p => p.a.NgayNhap >= tungay).Where(p => p.a.NgayNhap <= denngay).Sum(p => p.a.ThanhTienX) - kq.Where(p => p.a.NgayNhap >= tungay && p.a.NgayNhap <= denngay && p.a.PLoai == 1 && p.a.KieuDon == 2).Sum(p => p.a.ThanhTienN)) > 0 || DungChung.Bien.MaBV == "27023" || DungChung.Bien.MaBV == "30005")) ?
                                                         (kq.Where(p => p.a.NgayNhap >= tungay).Where(p => p.a.NgayNhap <= denngay).Sum(p => p.a.ThanhTienX) - kq.Where(p => p.a.NgayNhap >= tungay && p.a.NgayNhap <= denngay && p.a.PLoai == 1 && p.a.KieuDon == 2).Sum(p => p.a.ThanhTienN)) : 0,
                                        TonCKSL = kq.Where(p => p.a.NgayNhap <= denngay).Sum(p => p.a.SoLuongN) - kq.Where(p => p.a.NgayNhap <= denngay).Sum(p => p.a.SoLuongX),
                                        TonCKTT = ckcGiaNhap.Checked 
                                        ? kq.Key.GiaNhap == null ? 0 : kq.Key.GiaNhap * kq.Where(p => p.a.NgayNhap <= denngay).Sum(p => p.a.SoLuongN) - kq.Where(p => p.a.NgayNhap <= denngay).Sum(p => p.a.SoLuongX)
                                        : kq.Where(p => p.a.NgayNhap <= denngay).Sum(p => p.a.ThanhTienN) - kq.Where(p => p.a.NgayNhap <= denngay).Sum(p => p.a.ThanhTienX),
                                    }).Where(p => p.TonDKSL != 0 || p.TonCKSL != 0 || p.NhapTKSL != 0 || p.TongXuatTKSL != 0).OrderBy(p => p.TenHamLuong).ToList();
                        string msg = "";
                        for (int i = 0; i < qnxt.Count; i++)
                        {
                            if (qnxt[i].TonCKSL == 0 && qnxt[i].TonCKTT != 0)
                            {
                                double tonCK = Math.Round((double)qnxt[i].TonCKTT, 1);
                                double TT = 0;
                                if(qnxt[i].DonGia != null)
                                    TT = Math.Round((qnxt[i].TonCKSL * (double)qnxt[i].DonGia), 1);
                                if (tonCK != TT)
                                {
                                    msg += "- Thuốc: " + qnxt[i].TenHamLuong + ", MaDV: " + qnxt[i].MaDV + ", có số lượng cuối kỳ = " + qnxt[i].TonCKSL + ", đơn giá: " + qnxt[i].DonGia + " nhưng thành tiền cuối kỳ = " + qnxt[i].TonCKTT + "\n";
                                }
                            }
                        }
                        if (!string.IsNullOrEmpty(msg))
                            MessageBox.Show(msg, "Thông báo những thuốc sai tiền cuối kỳ");
                        var _sq = (from a in qnxt
                                   select new
                                   {
                                       a.TenCC,
                                       _tenNCC,
                                       a.NuocSX,
                                       a.STT,
                                       a.DonGia,
                                       a.DonVi,
                                       a.MaCC,
                                       a.DuongDung,
                                       a.HamLuong,
                                       a.HangSX,
                                       a.MaDV,
                                       a.TenTN,
                                       a.MaTam,
                                       a.TenHC,
                                       a.NhomThau,
                                       a.SoDK,
                                       a.MaQD,
                                       a.NhaThau,
                                       a.TenNhom,
                                       NhapTKSL = Math.Round(a.NhapTKSL, DungChung.Bien.LamTronSo),
                                       NhapTKTT = Math.Round((double)a.NhapTKTT, DungChung.Bien.LamTronSo),
                                       a.TenHamLuong,
                                       a.TenNhomDuoc,
                                       NhapHD_SL = Math.Round(a.NhapHD, DungChung.Bien.LamTronSo),
                                       NhapHD_TT = Math.Round(a.NhapHDTT, DungChung.Bien.LamTronSo),
                                       TongXuatTKSL = Math.Round(a.TongXuatTKSL, DungChung.Bien.LamTronSo),
                                       TongXuatTKTT = Math.Round((double)a.TongXuatTKTT, DungChung.Bien.LamTronSo),
                                       TonCKSL = Math.Round(a.TonCKSL, DungChung.Bien.LamTronSo),
                                       TonCKTT = Math.Round((double)a.TonCKTT, DungChung.Bien.LamTronSo),
                                       TonDKSL = Math.Round(a.TonDKSL, DungChung.Bien.LamTronSo),
                                       TonDKTT = a.TonDKTT == null ? 0 : Math.Round((double)a.TonDKTT, DungChung.Bien.LamTronSo),
                                       XuatTKTongSL = Math.Round(a.TongXuatTKSL, DungChung.Bien.LamTronSo),
                                       xuatTKTongTT = Math.Round((double)a.TongXuatTKTT, DungChung.Bien.LamTronSo),
                                   }).OrderBy(p => (chkHienthi.Checked ? p.TenNhomDuoc : p.TenHamLuong)).ThenBy(p => p.STT).ThenBy(p => p.TenHamLuong).ToList();

                        var q = _sq.Where(p => (string.IsNullOrEmpty(_nhacc)) ? true : p.MaCC == _nhacc).ToList();
                        #region mẫu rút gọn
                        string[] arrr = { "", "", "", "", "", "", "", "", "", "", "", "", "" };
                        int y = 0;
                        foreach (var item in _cotchon)
                        {
                            if (item.MaCot != "0")
                            {
                                arrr[y] = item.MaCot;
                                y++;
                            }
                        }
                        BaoCao.rep_NXT_24009 rep = new BaoCao.rep_NXT_24009(chkHienthi.Checked, ckTrongNgoaiNuoc.Checked, arrr);
                        //frmIn frm2 = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "Báo cáo NXT", "C:\\TsBCNXT_RG.xls", true, this.Name);
                        int x = 0;
                        foreach (var item in _cotchon.Where(p => p.MaCot != "0").ToList())
                        {

                            if (x == 0)
                            {
                                rep.celll5.Text = item.TenCot;
                            }
                            if (x == 1)
                            {
                                rep.celll6.Text = item.TenCot;
                            }
                            if (x == 2)
                            {
                                rep.celll7.Text = item.TenCot;
                            }
                            if (x == 3)
                            {
                                rep.celll8.Text = item.TenCot;
                            }
                            if (x == 4)
                            {
                                rep.celll9.Text = item.TenCot;
                            }
                            if (x == 5)
                            {
                                rep.celll10.Text = item.TenCot;
                            }
                            if (x == 6)
                            {
                                rep.celll11.Text = item.TenCot;
                            }
                            if (x == 7)
                            {
                                rep.celll12.Text = item.TenCot;
                            }
                            if (x == 8)
                            {
                                rep.celll13.Text = item.TenCot;
                            }
                            if (x == 9)
                            {
                                rep.celll14.Text = item.TenCot;
                            }
                            if (x == 10)
                            {
                                rep.celll15.Text = item.TenCot;
                            }
                            if (x == 11)
                            {
                                rep.celll16.Text = item.TenCot;
                            }
                            if (x == 12)
                            {
                                rep.celll17.Text = item.TenCot;
                            }
                            x++;
                        }

                        frmIn frm2 = new frmIn();
                        rep.TuNgay.Value = dateTuNgay.Text;
                        rep.DenNgay.Value = dateDenNgay.Text;
                        rep.Kho.Value = _tenkho;
                        rep.DataSource = q;
                        double test = q.Sum(p => p.TonCKTT);
                        rep.NhaCC.Value = _tenNCC;
                        rep.BindingData();
                        rep.CreateDocument();
                        frm2.prcIN.PrintingSystem = rep.PrintingSystem;
                        frm2.ShowDialog();
                    }
                    #endregion
                }
            }
            #endregion
            else
            {
                bool check01049 = (DungChung.Bien.MaBV == "01049" && radMauIn.SelectedIndex == 1);
                string _maCQCQ = "";
                DateTime tungay = System.DateTime.Now.Date;
                DateTime denngay = System.DateTime.Now.Date;
                int idnhom = -1, idtieunhom = -1;
                int _cot1 = -1, _cot2 = -1, _cot3 = -1, _cot4 = -1;
                string _tenc1 = "", _tenc2 = "", _tenc3 = "", _tenc4 = "";
                string _tenkho = "", _tenNCC = "";
                int aa = -1;
                if (check01049)
                {
                    _tenc1 = "Xuất ngoại trú";
                    _tenc2 = "Xuất khoa phòng";
                }
                else
                {
                    _tenc1 = cbo_cot1.Properties.GetDisplayText(cbo_cot1.EditValue);
                    _tenc2 = cbo_cot2.Properties.GetDisplayText(cbo_cot2.EditValue);
                    _cot1 = Convert.ToInt32(cbo_cot1.EditValue);
                    _cot2 = Convert.ToInt32(cbo_cot2.EditValue);
                }

                _tenc3 = cbo_cot3.Properties.GetDisplayText(cbo_cot3.EditValue);
                _tenc4 = cbo_cot4.Properties.GetDisplayText(cbo_cot4.EditValue);
                _cot3 = Convert.ToInt32(cbo_cot3.EditValue);
                _cot4 = Convert.ToInt32(cbo_cot4.EditValue);
                List<KPhong> _kpChon = new List<KPhong>();
                QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                var qCQCQ = data.BenhViens.Where(panelControl1 => panelControl1.MaBV == DungChung.Bien.MaBV).FirstOrDefault();
                if (qCQCQ != null)
                    _maCQCQ = qCQCQ.MaChuQuan;

                var qDTBN = data.DTBNs.ToList();
                int idDTBH = -2;
                int idDTDV = -2;
                var qDTBH = qDTBN.Where(p => p.DTBN1 != null && p.DTBN1.Trim() == "BHYT").FirstOrDefault();
                var qDTDV = qDTBN.Where(p => p.DTBN1 != null && p.DTBN1.Trim().ToLower() == "dịch vụ").FirstOrDefault();
                if (qDTBH != null)
                    idDTBH = qDTBH.IDDTBN;
                if (qDTDV != null)
                    idDTDV = qDTDV.IDDTBN;

                if (KTtaoBcNXT())
                {
                    for (int i = 0; i < cklKP.ItemCount; i++)
                    {
                        if (cklKP.GetItemChecked(i))
                            _kpChon.Add(new KPhong { TenKP = cklKP.GetItemText(i), MaKP = cklKP.GetItemValue(i) == null ? 0 : Convert.ToInt32(cklKP.GetItemValue(i)) });
                    }
                    // maau toanf trung tâm chỉ lấy những khoa phòng có MABV sử dụng là mã bệnh viện
                    // List<int> kpBV = data.KPhongs.Where(p => p.MaBVsd == DungChung.Bien.MaBV).Select(p=>p.MaKP).ToList();
                    string _nhacc = "";
                    if (lupNhaCC.EditValue != null)
                        _nhacc = lupNhaCC.EditValue.ToString();
                    tungay = DungChung.Bien.MaBV == "14017" ? dateTuNgay.DateTime : DungChung.Ham.NgayTu(dateTuNgay.DateTime);//dateTuNgay.DateTime;
                    denngay = DungChung.Bien.MaBV == "14017" ? dateDenNgay.DateTime : DungChung.Ham.NgayDen(dateDenNgay.DateTime);//dateDenNgay.DateTime;


                    if (lupNhom.EditValue != null)
                        idnhom = Convert.ToInt32(lupNhom.EditValue);
                    if (lupTieuNhom.EditValue != null)
                        idtieunhom = Convert.ToInt32(lupTieuNhom.EditValue);
                    foreach (var item in _kpChon)
                    {
                        _tenkho += item.TenKP + "; ";
                    }
                    // kiểm tra lại
                    var qtenncc = (from nhapd in data.NhapDs
                                   join nhacc in data.NhaCCs on nhapd.MaCC equals nhacc.MaCC
                                   where (nhacc.MaCC == _nhacc)
                                   select new { nhacc.TenCC }).ToList();
                    if (qtenncc.Count > 0)
                    {
                        _tenNCC = qtenncc.First().TenCC;
                    }
                    if (_kpChon.Count > 0)
                    {
                        bool checkHuHaoDY = (DungChung.Bien.MaBV == "30004" && chkTruHuHaoDY30004.Checked);
                        bool check04007 = (DungChung.Bien.MaBV == "04007" && chkMauNhieuKho.Checked);

                        bool ChiTinhVAT = ckcDonGiaVat.Checked;
                        bool InFull = checkInDM.Checked;
                        var qnxt3 = (from nhapd in data.NhapDs.Where(p => DungChung.Bien.MaBV == "24012" ? p.Status != -3 || p.Status == null : true)
                                     join nhapdct in data.NhapDcts on nhapd.IDNhap equals nhapdct.IDNhap
                                     where ((nhapd.PLoai == 1 && (check04007 ? nhapd.KieuDon == 1 : true)) || (nhapd.PLoai == 2) || (nhapd.PLoai == 3) || (check04007 ? nhapd.PLoai == 5 : true))
                                     select new
                                     {
                                         _tenNCC,
                                         nhapdct.SoLo,
                                         nhapdct.HanDung,
                                         nhapdct.MaBNhan,
                                         nhapd.TraDuoc_KieuDon,
                                         nhapd.XuatTD,
                                         nhapd.MaKP,
                                         nhapd.MaKPnx,
                                         nhapdct.MaDV,
                                         DonGiaCT = DungChung.Bien.MaBV == "30372" && !InFull ? 0 : nhapdct.DonGiaCT,
                                         nhapdct.TyLeCK,
                                         nhapdct.VAT,
                                         nhapdct.IDDTBN,
                                         nhapd.PLoai,
                                         nhapd.KieuDon,
                                         nhapd.NgayNhap,
                                         DonGia = DungChung.Bien.MaBV == "30372" && !InFull ? 0 : nhapdct.DonGia,
                                         GiaNhap = DungChung.Bien.MaBV == "30372" && !InFull ? 0 : (nhapd.PLoai == 1 ? nhapdct.DonGia : 0),
                                         SoLuongN = nhapd.PLoai == 1 ? nhapdct.SoLuongN : 0,
                                         SoLuongX = (nhapd.PLoai == 2 || nhapd.PLoai == 3) ? nhapdct.SoLuongX : 0,
                                         ThanhTienN = DungChung.Bien.MaBV == "30372" && !InFull ? 0 : (nhapd.PLoai == 1 ? nhapdct.ThanhTienN : 0),
                                         ThanhTienX = DungChung.Bien.MaBV == "30372" && !InFull ? 0 : ((nhapd.PLoai == 2 || nhapd.PLoai == 3) ? nhapdct.ThanhTienX : 0),
                                         SoLuongSD = nhapd.PLoai == 5 ? nhapdct.SoLuongSD : 0,
                                         ThanhTienSD = DungChung.Bien.MaBV == "30372" && !InFull ? 0 : (nhapd.PLoai == 5 ? nhapdct.ThanhTienSD : 0),
                                         nhapdct.SoLuongDY,
                                         ThanhTienDY = DungChung.Bien.MaBV == "30372" && !InFull ? 0 : nhapdct.ThanhTienDY
                                     }).OrderByDescending(p => p.NgayNhap).ToList();
                        var qnxt2 = (from nhapd in data.NhapDs
                                     join nhapdct in data.NhapDcts on nhapd.IDNhap equals nhapdct.IDNhap
                                     where ((nhapd.PLoai == 1 && (check04007 ? nhapd.KieuDon == 1 : true)) || (nhapd.PLoai == 2) || (nhapd.PLoai == 3) || (check04007 ? nhapd.PLoai == 5 : true))
                                     select new
                                     {
                                         _tenNCC,
                                         nhapdct.MaBNhan,
                                         nhapd.TraDuoc_KieuDon,
                                         nhapd.XuatTD,
                                         nhapd.MaKP,
                                         nhapd.MaKPnx,
                                         nhapdct.MaDV,
                                         DonGiaCT = DungChung.Bien.MaBV == "30372" && !InFull ? 0 : nhapdct.DonGiaCT,
                                         nhapdct.TyLeCK,
                                         nhapdct.VAT,
                                         nhapdct.IDDTBN,
                                         nhapd.PLoai,
                                         nhapd.KieuDon,
                                         nhapd.NgayNhap,
                                         DonGia = DungChung.Bien.MaBV == "30372" && !InFull ? 0 : nhapdct.DonGia,
                                         GiaNhap = DungChung.Bien.MaBV == "30372" && !InFull ? 0 : (nhapd.PLoai == 1 ? nhapdct.DonGia : 0),
                                         SoLuongN = nhapd.PLoai == 1 ? nhapdct.SoLuongN : 0,
                                         SoLuongX = (nhapd.PLoai == 2 || nhapd.PLoai == 3) ? nhapdct.SoLuongX : 0,
                                         ThanhTienN = DungChung.Bien.MaBV == "30372" && !InFull ? 0 : (nhapd.PLoai == 1 ? nhapdct.ThanhTienN : 0),
                                         ThanhTienX = DungChung.Bien.MaBV == "30372" && !InFull ? 0 : ((nhapd.PLoai == 2 || nhapd.PLoai == 3) ? nhapdct.ThanhTienX : 0),
                                         SoLuongSD = nhapd.PLoai == 5 ? nhapdct.SoLuongSD : 0,
                                         ThanhTienSD = DungChung.Bien.MaBV == "30372" && !InFull ? 0 : (nhapd.PLoai == 5 ? nhapdct.ThanhTienSD : 0),
                                         nhapdct.SoLuongDY,
                                         ThanhTienDY = DungChung.Bien.MaBV == "30372" && !InFull ? 0 : nhapdct.ThanhTienDY
                                     }).OrderByDescending(p => p.NgayNhap).ToList();


                        bool checkNcc = !string.IsNullOrWhiteSpace(_nhacc);
                        var dichvu = (from dv in data.DichVus.Where(o => checkNcc ? o.MaCC == _nhacc : true)
                                      join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                                      join nhomdv in data.NhomDVs on tn.IDNhom equals nhomdv.IDNhom
                                      where (idnhom == -1 ? true : nhomdv.IDNhom == idnhom) && (idtieunhom == -1 ? true : tn.IdTieuNhom == idtieunhom)
                                      select new
                                      {
                                          _tenNCC,
                                          dv.DuongD,
                                          dv.NuocSX,
                                          dv.NhaSX,
                                          dv.MaQD,
                                          dv.QCPC,
                                          dv.MaNhom,
                                          dv.NhomThau,
                                          dv.SoQD,
                                          dv.NgayQD,
                                          dv.MaDV,
                                          TenDV = (DungChung.Bien.MaBV == "12122"
                                      //|| DungChung.Bien.MaBV == "30009"
                                      ) ? (dv.TenDV + " " + dv.HamLuong) : ((DungChung.Bien.MaBV == "24009" || _maCQCQ == "24009") ? (dv.TenRG ?? "") : dv.TenDV),
                                          dv.TenHC,
                                          tn.TenTN,
                                          nhomdv.TenNhom,
                                          tn.STT,
                                          dv.MaCC,
                                          dv.DonVi,
                                          dv.HamLuong,
                                          dv.MaTam,
                                          dv.IDNhom,
                                          dv.SoDK,
                                          dv.GiaNhap,
                                          NoiSX = ckTrongNgoaiNuoc.Checked ? ((dv.NuocSX.ToLower().Contains("việt nam") || dv.NuocSX.ToLower().Contains("v.nam") || dv.NuocSX.ToLower().Contains("vn") || dv.NuocSX.ToLower().Contains("v.n")) ? 1 : 0) : 1,
                                      }).ToList(); //Nơi sx = 1? trong nước, 0: nước ngoài

                        #region 1111111111
                        var qnxt1 = (from a in qnxt3
                                     join dv in dichvu on a.MaDV equals dv.MaDV
                                     join kp in _kpChon//.Where(p=> chk_mauToanVien.Checked? kpBV.Contains(p.MaKP) : true) 
                                     on a.MaKP equals kp.MaKP
                                     group a by new {a.SoLo, a.HanDung, dv.STT, dv.MaCC, dv.TenTN, dv.SoQD, dv.NuocSX, dv.TenNhom, dv.TenDV, dv.DonVi, a.DonGia, a.MaDV, dv.MaTam, dv.TenHC/**,a.DonGiaCT*/ } into kq
                                     select new
                                     {
                                         _tenNCC,
                                         kq.Key.TenNhom,
                                         kq.Key.NuocSX,
                                         kq.Key.STT,
                                         kq.Key.TenTN,
                                         kq.Key.MaCC,
                                         kq.Key.SoQD,
                                         MaDV = kq.Key.MaDV,
                                         MaTam = kq.Key.MaTam,
                                         solo = kq.Key.SoLo,
                                         handung = kq.Key.HanDung,
                                         TenNhomDuoc = kq.Key.TenNhom,
                                         TenHamLuong = kq.Key.TenDV,
                                         TenHC = kq.Key.TenHC,
                                         DonVi = kq.Key.DonVi,
                                         DonGia = kq.Key.DonGia,
                                         GiaNhap = kq.Key.DonGia,
                                         //DonGiaCT= kq.Key.DonGiaCT,

                                         //01049 xuất ngoại trú
                                         XuatNgoaiTru01049SL = kq.Where(o => o.NgayNhap >= tungay && o.NgayNhap <= denngay && o.PLoai == 2 && o.KieuDon == 0).Sum(o => o.SoLuongX),
                                         XuatNgoaiTru01049TT = kq.Where(o => o.NgayNhap >= tungay && o.NgayNhap <= denngay && o.PLoai == 2 && o.KieuDon == 0).Sum(o => o.ThanhTienX),
                                         //01049 xuất khoa phòng
                                         XuatKhoaPhong01049SL = kq.Where(o => o.NgayNhap >= tungay && o.NgayNhap <= denngay && o.PLoai == 2 && (o.KieuDon == 1 || o.KieuDon == 4 || o.KieuDon == 5 || o.KieuDon == 6 || o.KieuDon == 7)).Sum(o => o.SoLuongX),
                                         XuatKhoaPhong01049TT = kq.Where(o => o.NgayNhap >= tungay && o.NgayNhap <= denngay && o.PLoai == 2 && (o.KieuDon == 1 || o.KieuDon == 4 || o.KieuDon == 5 || o.KieuDon == 6 || o.KieuDon == 7)).Sum(o => o.ThanhTienX),
                                         //01049 xuất khác
                                         XuatKhac01049SL = kq.Where(o => o.NgayNhap >= tungay && o.NgayNhap <= denngay && o.PLoai == 2 && (o.KieuDon == 2 || o.KieuDon == 3 || o.KieuDon == 8 || o.KieuDon == 9 || o.KieuDon == 10 || o.KieuDon == 11)).Sum(o => o.SoLuongX),
                                         XuatKhac01049TT = kq.Where(o => o.NgayNhap >= tungay && o.NgayNhap <= denngay && o.PLoai == 2 && (o.KieuDon == 2 || o.KieuDon == 3 || o.KieuDon == 8 || o.KieuDon == 9 || o.KieuDon == 10 || o.KieuDon == 11)).Sum(o => o.ThanhTienX),
                                         //30004 hư hao đông y
                                         HuHaoDongY30004SL = kq.Where(o => o.NgayNhap >= tungay && o.NgayNhap <= denngay && o.PLoai == 2).Sum(o => o.SoLuongDY),
                                         HuHaoDongY30004TT = kq.Where(o => o.NgayNhap >= tungay && o.NgayNhap <= denngay && o.PLoai == 2).Sum(o => o.ThanhTienDY),
                                         //SoLo=kq.Key.SoLo,
                                         TonDKSL04007 = kq.Where(p => p.NgayNhap < tungay).Where(p => p.PLoai == 1 && p.KieuDon == 1).Sum(p => p.SoLuongN) - (kq.Where(p => p.NgayNhap < tungay).Where(p => (p.PLoai == 2 && (p.KieuDon != 2 && p.KieuDon != 3))).Sum(p => p.SoLuongX)
                                         + (kq.Where(p => p.NgayNhap < tungay).Where(p => (p.PLoai == 5)).Sum(p => p.SoLuongSD))),
                                         TonDKTT04007 = kq.Where(p => p.NgayNhap < tungay).Where(p => p.PLoai == 1 && p.KieuDon != 2).Sum(p => p.ThanhTienN) - (kq.Where(p => p.NgayNhap < tungay).Where(p => (p.PLoai == 2 && (p.KieuDon != 2 && p.KieuDon != 3))).Sum(p => p.ThanhTienX)
                                         + (kq.Where(p => p.NgayNhap < tungay).Where(p => (p.PLoai == 5)).Sum(p => p.ThanhTienSD))),
                                         TonDKSL = kq.Where(p => p.NgayNhap < tungay).Sum(p => p.SoLuongN) - kq.Where(p => p.NgayNhap < tungay).Sum(p => p.SoLuongX),
                                         TonDKTT = kq.Where(p => p.NgayNhap < tungay).Sum(p => p.ThanhTienN) - kq.Where(p => p.NgayNhap < tungay).Sum(p => p.ThanhTienX),
                                         //Tồn đầu kỳ 30004 - hư hao đông y
                                         TonDKSL30004 = kq.Where(p => p.NgayNhap < tungay).Sum(p => p.SoLuongN) - kq.Where(p => p.NgayNhap < tungay).Sum(p => p.SoLuongX) - kq.Where(o => o.NgayNhap < tungay && o.PLoai == 2).Sum(o => o.SoLuongDY),
                                         TonDKTT30004 = kq.Where(p => p.NgayNhap < tungay).Sum(p => p.ThanhTienN) - kq.Where(p => p.NgayNhap < tungay).Sum(p => p.ThanhTienX) - kq.Where(o => o.NgayNhap < tungay && o.PLoai == 2).Sum(o => o.ThanhTienDY),
                                         //TonDKTTTong = kq.Where(p => p.NgayNhap < tungay).Sum(p => p.ThanhTienN)-kq.Where(p => p.NgayNhap < tungay).Sum(p => p.ThanhTienX),

                                         //nhập trong kỳ (trừ nhập trả)

                                         NhapTKSL = check04007 ? kq.Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay && p.PLoai == 1 && p.KieuDon == 1).Sum(p => p.SoLuongN) : check01049 ? kq.Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay && p.PLoai == 1).Sum(p => p.SoLuongN) : kq.Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay && p.PLoai == 1 && p.KieuDon != 2).Sum(p => p.SoLuongN),

                                         NhapTKTT = check04007 ? kq.Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay && p.PLoai == 1 && p.KieuDon == 1).Sum(p => p.ThanhTienN) : kq.Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay && p.PLoai == 1 && p.KieuDon != 2).Sum(p => p.ThanhTienN),

                                         //nhập từ tủ trực
                                         NhapTTTSL = check04007 ? ((kq.Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay && p.PLoai == 1 && p.KieuDon == 1).Sum(p => p.SoLuongN))) : (kq.Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay && p.PLoai == 1 && p.KieuDon == 2 && p.TraDuoc_KieuDon == 6).Sum(p => p.SoLuongN)),
                                         NhapTTTTT = check04007 ? (kq.Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay && p.PLoai == 1 && p.KieuDon == 1).Sum(p => p.ThanhTienN)) : kq.Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay && p.PLoai == 1 && p.KieuDon == 2 && p.TraDuoc_KieuDon == 6).Sum(p => p.ThanhTienN),

                                         //Nhập theo Hóa đơn
                                         NhapHD = kq.Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay && p.PLoai == 1 && p.KieuDon == 1).Sum(p => p.SoLuongN),
                                         NhapHDTT = kq.Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay && p.PLoai == 1 && p.KieuDon == 1).Sum(p => p.ThanhTienN),

                                         //Nhập khác ( # nhập hóa đơn ) - bv 30002
                                         Nhap_Khac = kq.Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay && p.PLoai == 1 && p.KieuDon != 1).Sum(p => p.SoLuongN),
                                         Nhap_KhacTT = kq.Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay && p.PLoai == 1 && p.KieuDon != 1).Sum(p => p.ThanhTienN),

                                         //Nhập chuyển kho
                                         NhapCK = kq.Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay && p.PLoai == 1 && p.KieuDon == 0).Sum(p => p.SoLuongN),
                                         NhapCKTT = kq.Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay && p.PLoai == 1 && p.KieuDon == 0).Sum(p => p.ThanhTienN),

                                         //  nhập nội bộ(chuyển kho)
                                         NhapNB_SL = kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.PLoai == 1 && p.KieuDon == 0).Sum(p => p.SoLuongN),
                                         NhapNB_TT = kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.PLoai == 1 && p.KieuDon == 0).Sum(p => p.ThanhTienN),

                                         //nhập trả lại
                                         NhapTra_SL = kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.PLoai == 1 && p.KieuDon == 2).Sum(p => p.SoLuongN),
                                         NhapTra_TT = kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.PLoai == 1 && p.KieuDon == 2).Sum(p => p.ThanhTienN),
                                         #region 30002
                                         //xuất dịch vụ
                                         XuatDVSL = DungChung.Bien.MaBV == "30002" ? ((((kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.IDDTBN == idDTDV).Sum(p => p.SoLuongX) - kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.IDDTBN == idDTDV && p.PLoai == 1 && p.KieuDon == 2).Sum(p => p.SoLuongN)) > 0)) ?
                                                        (kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.IDDTBN == idDTDV).Sum(p => p.SoLuongX) - kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.IDDTBN == idDTDV && p.PLoai == 1 && p.KieuDon == 2).Sum(p => p.SoLuongN)) : 0) : 0,
                                         XuatDVTT = DungChung.Bien.MaBV == "30002" ? ((((kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.IDDTBN == idDTDV).Sum(p => p.ThanhTienX) - kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.IDDTBN == idDTDV && p.PLoai == 1 && p.KieuDon == 2).Sum(p => p.ThanhTienN)) > 0)) ?
                                        (kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.IDDTBN == idDTDV).Sum(p => p.ThanhTienX) - kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.IDDTBN == idDTDV && p.PLoai == 1 && p.KieuDon == 2).Sum(p => p.ThanhTienN)) : 0) : 0,
                                         //xuất bảo hiểm
                                         XuatBHSL = DungChung.Bien.MaBV == "30002" ? ((((kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.IDDTBN == idDTBH).Sum(p => p.SoLuongX) - kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.IDDTBN == idDTBH && p.PLoai == 1 && p.KieuDon == 2).Sum(p => p.SoLuongN)
                                         + kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.IDDTBN != idDTBH && p.IDDTBN != idDTDV && p.PLoai == 2 && (p.KieuDon == 1 || p.KieuDon == 5 || p.KieuDon == 7 || p.KieuDon == 11)).Sum(p => p.SoLuongX)
                                         ) > 0)) ?
                                                        (kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.IDDTBN == idDTBH).Sum(p => p.SoLuongX) - kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.IDDTBN == idDTBH && p.PLoai == 1 && p.KieuDon == 2).Sum(p => p.SoLuongN)
                                                        + kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.IDDTBN != idDTBH && p.IDDTBN != idDTDV && p.PLoai == 2 && (p.KieuDon == 1 || p.KieuDon == 5 || p.KieuDon == 7 || p.KieuDon == 11)).Sum(p => p.SoLuongX))
                                                        : 0) : 0,
                                         XuatBHTT = DungChung.Bien.MaBV == "30002" ? ((((kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.IDDTBN == idDTBH).Sum(p => p.ThanhTienX) - kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.IDDTBN == idDTBH && p.PLoai == 1 && p.KieuDon == 2).Sum(p => p.ThanhTienN)
                                         + kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.IDDTBN != idDTBH && p.IDDTBN != idDTDV && p.PLoai == 2 && (p.KieuDon == 1 || p.KieuDon == 5 || p.KieuDon == 7 || p.KieuDon == 11)).Sum(p => p.ThanhTienX)
                                         ) > 0)) ?
                                        (kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.IDDTBN == idDTBH).Sum(p => p.ThanhTienX) - kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.IDDTBN == idDTBH && p.PLoai == 1 && p.KieuDon == 2).Sum(p => p.ThanhTienN)
                                        + kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.IDDTBN != idDTBH && p.IDDTBN != idDTDV && p.PLoai == 2 && (p.KieuDon == 1 || p.KieuDon == 5 || p.KieuDon == 7 || p.KieuDon == 11)).Sum(p => p.ThanhTienX)
                                        ) : 0) : 0,
                                         //xuất khác
                                         XuatKhac30002SL = DungChung.Bien.MaBV == "30002" ? ((((kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.IDDTBN != idDTBH && p.IDDTBN != idDTDV).Sum(p => p.SoLuongX) - kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.IDDTBN != idDTBH && p.IDDTBN != idDTDV && p.PLoai == 1 && p.KieuDon == 2 && p.TraDuoc_KieuDon != 6).Sum(p => p.SoLuongN)
                                         - kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.IDDTBN != idDTBH && p.IDDTBN != idDTDV && p.PLoai == 2 && (p.KieuDon == 1 || p.KieuDon == 5 || p.KieuDon == 7 || p.KieuDon == 11)).Sum(p => p.SoLuongX)
                                         ) > 0)) ?
                                                        (kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.IDDTBN != idDTBH && p.IDDTBN != idDTDV).Sum(p => p.SoLuongX) - kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.IDDTBN != idDTBH && p.IDDTBN != idDTDV && p.PLoai == 1 && p.KieuDon == 2 && p.KieuDon == 2 && p.TraDuoc_KieuDon != 6).Sum(p => p.SoLuongN)
                                                        - kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.IDDTBN != idDTBH && p.IDDTBN != idDTDV && p.PLoai == 2 && (p.KieuDon == 1 || p.KieuDon == 5 || p.KieuDon == 7 || p.KieuDon == 11)).Sum(p => p.SoLuongX)
                                                        ) : 0) : 0,
                                         XuatKhac30002TT = DungChung.Bien.MaBV == "30002" ? ((((kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.IDDTBN != idDTBH && p.IDDTBN != idDTDV).Sum(p => p.ThanhTienX) - kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.IDDTBN != idDTBH && p.IDDTBN != idDTDV && p.PLoai == 1 && p.KieuDon == 2 && p.TraDuoc_KieuDon != 6).Sum(p => p.ThanhTienN)
                                         - kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.IDDTBN != idDTBH && p.IDDTBN != idDTDV && p.PLoai == 2 && (p.KieuDon == 1 || p.KieuDon == 5 || p.KieuDon == 7 || p.KieuDon == 11)).Sum(p => p.ThanhTienX)
                                         ) > 0)) ?
                                        (kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.IDDTBN != idDTBH && p.IDDTBN != idDTDV).Sum(p => p.ThanhTienX) - kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.IDDTBN != idDTBH && p.IDDTBN != idDTDV && p.PLoai == 1 && p.KieuDon == 2 && p.TraDuoc_KieuDon != 6).Sum(p => p.ThanhTienN)
                                        - kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.IDDTBN != idDTBH && p.IDDTBN != idDTDV && p.PLoai == 2 && (p.KieuDon == 1 || p.KieuDon == 5 || p.KieuDon == 7 || p.KieuDon == 11)).Sum(p => p.ThanhTienX)
                                        ) : 0) : 0,
                                         #endregion
                                         //xuất trong kỳ(không tính nhập trả dược - dùng cho mẫu BC nhập trả dược)
                                         TongXuatTKSL = check04007 ? (kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay).Where(p => (p.PLoai == 2 && (p.KieuDon != 2 && p.KieuDon != 3))).Sum(p => p.SoLuongX)
                                         + (kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay).Where(p => (p.PLoai == 5)).Sum(p => p.SoLuongSD)))
                                         : (((DungChung.Bien.MaBV == "27022" || DungChung.Bien.MaBV == "30005" || DungChung.Bien.MaBV == "30007") && radMauIn.SelectedIndex == 4) ? kq.Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.SoLuongX) : ((((kq.Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.SoLuongX) - kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.PLoai == 1 && p.KieuDon == 2).Sum(p => p.SoLuongN)) > 0 || DungChung.Bien.MaBV == "27023")) ? // || DungChung.Bien.MaBV == "30005" 
                                                        (kq.Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.SoLuongX) - kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.PLoai == 1 && p.KieuDon == 2).Sum(p => p.SoLuongN)) : 0)),
                                         TongXuatTKTT = check04007 ? (kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay).Where(p => (p.PLoai == 2 && (p.KieuDon != 2 && p.KieuDon != 3))).Sum(p => p.ThanhTienX)
                                         + (kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay).Where(p => (p.PLoai == 5)).Sum(p => p.ThanhTienSD)))
                                         : (((DungChung.Bien.MaBV == "27022" || DungChung.Bien.MaBV == "30005" || DungChung.Bien.MaBV == "30007") && radMauIn.SelectedIndex == 4) ? kq.Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.ThanhTienX) : ((((kq.Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.ThanhTienX) - kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.PLoai == 1 && p.KieuDon == 2).Sum(p => p.ThanhTienN)) > 0 || DungChung.Bien.MaBV == "27023")) ?
                                                          (kq.Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.ThanhTienX) - kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.PLoai == 1 && p.KieuDon == 2).Sum(p => p.ThanhTienN)) : 0)),

                                         TongXuatTKSL1 = ((DungChung.Bien.MaBV == "26007" || DungChung.Bien.MaBV == "30005" || DungChung.Bien.MaBV == "30004" || DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "56789")) ? // || DungChung.Bien.MaBV == "30005" 
                                                        (kq.Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.SoLuongX)) : 0,
                                         TongXuatTKTT1 = ((DungChung.Bien.MaBV == "26007" || DungChung.Bien.MaBV == "30005" || DungChung.Bien.MaBV == "30004" || DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "56789")) ?
                                                          (kq.Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.ThanhTienX)) : 0,

                                         //  xuất nội bộ(chuyển kho)
                                         XuatNB_SL = kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.KieuDon == 2).Sum(p => p.SoLuongX),
                                         XuatNB_TT = kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.KieuDon == 2).Sum(p => p.ThanhTienX),

                                         XuatNoiTruSL = (radLoaiXuat.SelectedIndex == 1) ? (kq.Where(p => p.IDDTBN == _cot1 && p.PLoai == 2).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.SoLuongX)) :
                                                         (kq.Where(p => p.KieuDon == _cot1 && p.PLoai == 2).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.SoLuongX)),
                                         xuatNoiTruTT = (radLoaiXuat.SelectedIndex == 1) ? (kq.Where(p => p.IDDTBN == _cot1 && p.PLoai == 2).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.ThanhTienX)) :
                                                         (kq.Where(p => p.KieuDon == _cot1 && p.PLoai == 2).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.ThanhTienX)),

                                         XuatNoiTruSL1 = kq.Where(p => p.KieuDon == _cot1 && p.PLoai == 2).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.SoLuongX) - kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.PLoai == 1 && p.TraDuoc_KieuDon == _cot1 && p.KieuDon == 2).Sum(p => p.SoLuongN),
                                         xuatNoiTruTT1 = kq.Where(p => p.KieuDon == _cot1 && p.PLoai == 2).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.ThanhTienX) - kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.PLoai == 1 && p.TraDuoc_KieuDon == _cot1 && p.KieuDon == 2).Sum(p => p.ThanhTienN),
                                         // xuatNoiTruTTTong = kq.Where(p => p.KieuDon == 0).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.ThanhTienX),

                                         XuatNgoaiTruSL = (radLoaiXuat.SelectedIndex == 1) ? (kq.Where(p => p.IDDTBN == _cot2 && p.PLoai == 2).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.SoLuongX)) :
                                                           (kq.Where(p => p.KieuDon == _cot2 && p.PLoai == 2).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.SoLuongX)),
                                         xuatNgoaiTruTT = (radLoaiXuat.SelectedIndex == 1) ? (kq.Where(p => p.IDDTBN == _cot2 && p.PLoai == 2).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.ThanhTienX)) :
                                                           (kq.Where(p => p.KieuDon == _cot2 && p.PLoai == 2).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.ThanhTienX)),

                                         XuatNgoaiTruSL1 = kq.Where(p => p.KieuDon == _cot2 && p.PLoai == 2).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.SoLuongX) - kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.PLoai == 1 && p.TraDuoc_KieuDon == _cot2 && p.KieuDon == 2).Sum(p => p.SoLuongN),
                                         xuatNgoaiTruTT1 = kq.Where(p => p.KieuDon == _cot2 && p.PLoai == 2).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.ThanhTienX) - kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.PLoai == 1 && p.TraDuoc_KieuDon == _cot2 && p.KieuDon == 2).Sum(p => p.ThanhTienN),

                                         XuatC3SL = (radLoaiXuat.SelectedIndex == 1) ? (kq.Where(p => p.IDDTBN == _cot3 && p.PLoai == 2).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.SoLuongX)) :
                                         (kq.Where(p => p.KieuDon == _cot3 && p.PLoai == 2).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.SoLuongX)),
                                         xuatC3TT = (radLoaiXuat.SelectedIndex == 1) ? (kq.Where(p => p.IDDTBN == _cot3 && p.PLoai == 2).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.ThanhTienX)) :
                                                           (kq.Where(p => p.KieuDon == _cot3 && p.PLoai == 2).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.ThanhTienX)),

                                         XuatC4SL = (radLoaiXuat.SelectedIndex == 1) ? (kq.Where(p => p.IDDTBN == _cot4 && p.PLoai == 2).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.SoLuongX)) :
                                         (kq.Where(p => p.KieuDon == _cot4 && p.PLoai == 2).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.SoLuongX)),
                                         xuatC4TT = (radLoaiXuat.SelectedIndex == 1) ? (kq.Where(p => p.IDDTBN == _cot4 && p.PLoai == 2).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.ThanhTienX)) :
                                         (kq.Where(p => p.KieuDon == _cot4 && p.PLoai == 2).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.ThanhTienX)),

                                         // xuatNgoaiTruTTTong = kq.Where(p => p.KieuDon == 1).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.ThanhTienX),
                                         XuatKhacSL = (radLoaiXuat.SelectedIndex == 1) ? kq.Where(p => p.IDDTBN != _cot1 && p.IDDTBN != _cot2 && p.IDDTBN != _cot3 && p.PLoai == 2 && (radMauIn.SelectedIndex == 3 ? p.PLoai == 2 : true)).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.SoLuongX) :
                                                       kq.Where(p => p.KieuDon != _cot1 && p.KieuDon != _cot2 && p.KieuDon != _cot3 && p.PLoai == 2 && (radMauIn.SelectedIndex == 3 ? p.PLoai == 2 : true)).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.SoLuongX),
                                         xuatKhacTT = (radLoaiXuat.SelectedIndex == 1) ? kq.Where(p => p.IDDTBN != _cot1 && p.IDDTBN != _cot2 && p.PLoai == 2 && (radMauIn.SelectedIndex == 3 ? p.PLoai == 2 : true)).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.ThanhTienX) :
                                                       kq.Where(p => p.KieuDon != _cot1 && p.KieuDon != _cot2 && p.KieuDon != _cot3 && p.PLoai == 2 && (radMauIn.SelectedIndex == 3 ? p.PLoai == 2 : true)).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.ThanhTienX),

                                         XuatKhacSL1 = kq.Where(p => p.KieuDon != _cot1 && p.KieuDon != _cot2 && p.PLoai == 2 && (radMauIn.SelectedIndex == 3 ? p.PLoai == 2 : true)).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.SoLuongX) - kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.PLoai == 1 && p.TraDuoc_KieuDon != _cot1 && p.TraDuoc_KieuDon != _cot2 && p.KieuDon == 2).Sum(p => p.SoLuongN),
                                         xuatKhacTT1 = kq.Where(p => p.KieuDon != _cot1 && p.KieuDon != _cot2 && p.PLoai == 2 && (radMauIn.SelectedIndex == 3 ? p.PLoai == 2 : true)).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.ThanhTienX) - kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.PLoai == 1 && p.TraDuoc_KieuDon != _cot1 && p.TraDuoc_KieuDon != _cot2 && p.KieuDon == 2).Sum(p => p.ThanhTienN),

                                         XuatKhacSL2 = (radLoaiXuat.SelectedIndex == 1) ? kq.Where(p => p.IDDTBN != _cot1 && p.IDDTBN != _cot2 && p.PLoai == 2 && (radMauIn.SelectedIndex == 3 ? p.PLoai == 2 : true)).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.SoLuongX) :
                                                      kq.Where(p => p.KieuDon != _cot1 && p.KieuDon != _cot2 && p.KieuDon != _cot3 && p.KieuDon != _cot4 && p.PLoai == 2 && (radMauIn.SelectedIndex == 3 ? p.PLoai == 2 : true)).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.SoLuongX),
                                         xuatKhacTT2 = (radLoaiXuat.SelectedIndex == 1) ? kq.Where(p => p.IDDTBN != _cot1 && p.IDDTBN != _cot2 && p.PLoai == 2 && (radMauIn.SelectedIndex == 3 ? p.PLoai == 2 : true)).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.ThanhTienX) :
                                                      kq.Where(p => p.KieuDon != _cot1 && p.KieuDon != _cot2 && p.KieuDon != _cot3 && p.KieuDon != _cot4 && p.PLoai == 2 && (radMauIn.SelectedIndex == 3 ? p.PLoai == 2 : true)).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.ThanhTienX),

                                         //Xuất khác dùng để xuất excel BV Chí Linh
                                         xuatKhacExcel = kq.Where(p => p.PLoai == 2 && p.KieuDon == 9).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.SoLuongX),

                                         XuatHuHaoSL = kq.Where(p => p.KieuDon != _cot1 && p.KieuDon != _cot2 && p.KieuDon != _cot3 && p.PLoai == 3).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.SoLuongX),
                                         XuatHuHaoTT = kq.Where(p => p.KieuDon != _cot1 && p.KieuDon != _cot2 && p.KieuDon != _cot3 && p.PLoai == 3).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.ThanhTienX),

                                         TonCKSL = kq.Where(p => p.NgayNhap <= denngay).Sum(p => p.SoLuongN) - kq.Where(p => p.NgayNhap <= denngay).Sum(p => p.SoLuongX),
                                         TonCKTT = kq.Where(p => p.NgayNhap <= denngay).Sum(p => p.ThanhTienN) - kq.Where(p => p.NgayNhap <= denngay).Sum(p => p.ThanhTienX),
                                         TonCKSL1 = kq.Where(p => p.NgayNhap <= denngay).Sum(p => p.SoLuongN) + kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.PLoai == 1 && p.KieuDon == 2).Sum(p => p.SoLuongN) - kq.Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.SoLuongX),
                                         TonCKTT1 = kq.Where(p => p.NgayNhap <= denngay).Sum(p => p.ThanhTienN) + kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.PLoai == 1 && p.KieuDon == 2).Sum(p => p.ThanhTienN) - kq.Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.ThanhTienX),

                                     }).OrderBy(p => p.TenHamLuong).ToList();
                        var qnxt = (from a in qnxt2
                                    join dv in dichvu on a.MaDV equals dv.MaDV
                                    join kp in _kpChon//.Where(p=> chk_mauToanVien.Checked? kpBV.Contains(p.MaKP) : true) 
                                    on a.MaKP equals kp.MaKP
                                    group a by new { dv.STT, dv.MaCC, dv.TenTN, dv.SoQD, dv.NuocSX, dv.TenNhom, dv.TenDV, dv.DonVi, a.DonGia, a.MaDV, dv.MaTam, dv.TenHC, dv.NhomThau,dv.HamLuong, dv.IDNhom, dv.GiaNhap} into kq
                                    select new
                                    {
                                        _tenNCC,
                                        kq.Key.TenNhom,
                                        kq.Key.NuocSX,
                                        kq.Key.STT,
                                        kq.Key.TenTN,
                                        kq.Key.MaCC,
                                        kq.Key.SoQD,
                                        MaDV = kq.Key.MaDV,
                                        MaTam = kq.Key.MaTam,
                                        TenNhomDuoc = kq.Key.TenNhom,
                                        TenHamLuong = kq.Key.TenDV,
                                        TenHC = kq.Key.TenHC,
                                        NhomThau = kq.Key.NhomThau,
                                        HamLuong = kq.Key.HamLuong,
                                        DonVi = kq.Key.DonVi,
                                        DonGia = ckcGiaNhap.Checked ? kq.Key.GiaNhap : kq.Key.DonGia,
                                        GiaNhap = kq.Key.DonGia,
                                        IDNhom = kq.Key.IDNhom,
                                        //DonGiaCT= kq.Key.DonGiaCT,

                                        //01049 xuất ngoại trú
                                        XuatNgoaiTru01049SL = kq.Where(o => o.NgayNhap >= tungay && o.NgayNhap <= denngay && o.PLoai == 2 && o.KieuDon == 0).Sum(o => o.SoLuongX),
                                        XuatNgoaiTru01049TT = kq.Where(o => o.NgayNhap >= tungay && o.NgayNhap <= denngay && o.PLoai == 2 && o.KieuDon == 0).Sum(o => o.ThanhTienX),
                                        //01049 xuất khoa phòng
                                        XuatKhoaPhong01049SL = kq.Where(o => o.NgayNhap >= tungay && o.NgayNhap <= denngay && o.PLoai == 2 && (o.KieuDon == 1 || o.KieuDon == 4 || o.KieuDon == 5 || o.KieuDon == 6 || o.KieuDon == 7)).Sum(o => o.SoLuongX),
                                        XuatKhoaPhong01049TT = kq.Where(o => o.NgayNhap >= tungay && o.NgayNhap <= denngay && o.PLoai == 2 && (o.KieuDon == 1 || o.KieuDon == 4 || o.KieuDon == 5 || o.KieuDon == 6 || o.KieuDon == 7)).Sum(o => o.ThanhTienX),
                                        //01049 xuất khác
                                        XuatKhac01049SL = kq.Where(o => o.NgayNhap >= tungay && o.NgayNhap <= denngay && o.PLoai == 2 && (o.KieuDon == 2 || o.KieuDon == 3 || o.KieuDon == 8 || o.KieuDon == 9 || o.KieuDon == 10 || o.KieuDon == 11)).Sum(o => o.SoLuongX),
                                        XuatKhac01049TT = kq.Where(o => o.NgayNhap >= tungay && o.NgayNhap <= denngay && o.PLoai == 2 && (o.KieuDon == 2 || o.KieuDon == 3 || o.KieuDon == 8 || o.KieuDon == 9 || o.KieuDon == 10 || o.KieuDon == 11)).Sum(o => o.ThanhTienX),
                                        //30004 hư hao đông y
                                        HuHaoDongY30004SL = kq.Where(o => o.NgayNhap >= tungay && o.NgayNhap <= denngay && o.PLoai == 2).Sum(o => o.SoLuongDY),
                                        HuHaoDongY30004TT = kq.Where(o => o.NgayNhap >= tungay && o.NgayNhap <= denngay && o.PLoai == 2).Sum(o => o.ThanhTienDY),
                                        //SoLo=kq.Key.SoLo,
                                        TonDKSL04007 = kq.Where(p => p.NgayNhap < tungay).Where(p => p.PLoai == 1 && p.KieuDon == 1).Sum(p => p.SoLuongN) - (kq.Where(p => p.NgayNhap < tungay).Where(p => (p.PLoai == 2 && (p.KieuDon != 2 && p.KieuDon != 3))).Sum(p => p.SoLuongX)
                                        + (kq.Where(p => p.NgayNhap < tungay).Where(p => (p.PLoai == 5)).Sum(p => p.SoLuongSD))),
                                        TonDKTT04007 = kq.Where(p => p.NgayNhap < tungay).Where(p => p.PLoai == 1 && p.KieuDon != 2).Sum(p => p.ThanhTienN) - (kq.Where(p => p.NgayNhap < tungay).Where(p => (p.PLoai == 2 && (p.KieuDon != 2 && p.KieuDon != 3))).Sum(p => p.ThanhTienX)
                                        + (kq.Where(p => p.NgayNhap < tungay).Where(p => (p.PLoai == 5)).Sum(p => p.ThanhTienSD))),
                                        TonDKSL = kq.Where(p => p.NgayNhap < tungay).Sum(p => p.SoLuongN) - kq.Where(p => p.NgayNhap < tungay).Sum(p => p.SoLuongX),
                                        TonDKTT = ckcGiaNhap.Checked 
                                                    ? (kq.Key.GiaNhap == null ? 0 : (kq.Key.GiaNhap * (kq.Where(p => p.NgayNhap < tungay).Sum(p => p.SoLuongN) - kq.Where(p => p.NgayNhap < tungay).Sum(p => p.SoLuongX))))
                                                    : kq.Where(p => p.NgayNhap < tungay).Sum(p => p.ThanhTienN) - kq.Where(p => p.NgayNhap < tungay).Sum(p => p.ThanhTienX),
                                        //Tồn đầu kỳ 30004 - hư hao đông y
                                        TonDKSL30004 = kq.Where(p => p.NgayNhap < tungay).Sum(p => p.SoLuongN) - kq.Where(p => p.NgayNhap < tungay).Sum(p => p.SoLuongX) - kq.Where(o => o.NgayNhap < tungay && o.PLoai == 2).Sum(o => o.SoLuongDY),
                                        TonDKTT30004 = kq.Where(p => p.NgayNhap < tungay).Sum(p => p.ThanhTienN) - kq.Where(p => p.NgayNhap < tungay).Sum(p => p.ThanhTienX) - kq.Where(o => o.NgayNhap < tungay && o.PLoai == 2).Sum(o => o.ThanhTienDY),
                                        //TonDKTTTong = kq.Where(p => p.NgayNhap < tungay).Sum(p => p.ThanhTienN)-kq.Where(p => p.NgayNhap < tungay).Sum(p => p.ThanhTienX),

                                        //nhập trong kỳ (trừ nhập trả)

                                        NhapTKSL = check04007 ? kq.Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay && p.PLoai == 1 && p.KieuDon == 1).Sum(p => p.SoLuongN) : check01049 ? kq.Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay && p.PLoai == 1).Sum(p => p.SoLuongN) : kq.Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay && p.PLoai == 1 && p.KieuDon != 2).Sum(p => p.SoLuongN),

                                        NhapTKTT = ckcGiaNhap.Checked
                                        ? (kq.Key.GiaNhap == null ? 0 : (kq.Key.GiaNhap * (kq.Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay && p.PLoai == 1 && p.KieuDon != 2).Sum(p => p.SoLuongN))))
                                        : (check04007 ? kq.Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay && p.PLoai == 1 && p.KieuDon == 1).Sum(p => p.ThanhTienN) : kq.Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay && p.PLoai == 1 && p.KieuDon != 2).Sum(p => p.ThanhTienN)),

                                        //nhập từ tủ trực
                                        NhapTTTSL = check04007 ? ((kq.Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay && p.PLoai == 1 && p.KieuDon == 1).Sum(p => p.SoLuongN))) : (kq.Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay && p.PLoai == 1 && p.KieuDon == 2 && p.TraDuoc_KieuDon == 6).Sum(p => p.SoLuongN)),
                                        NhapTTTTT = check04007 ? (kq.Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay && p.PLoai == 1 && p.KieuDon == 1).Sum(p => p.ThanhTienN)) : kq.Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay && p.PLoai == 1 && p.KieuDon == 2 && p.TraDuoc_KieuDon == 6).Sum(p => p.ThanhTienN),

                                        //Nhập theo Hóa đơn
                                        NhapHD = kq.Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay && p.PLoai == 1 && p.KieuDon == 1).Sum(p => p.SoLuongN),
                                        NhapHDTT = kq.Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay && p.PLoai == 1 && p.KieuDon == 1).Sum(p => p.ThanhTienN),

                                        //Nhập khác ( # nhập hóa đơn ) - bv 30002
                                        Nhap_Khac = kq.Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay && p.PLoai == 1 && p.KieuDon != 1).Sum(p => p.SoLuongN),
                                        Nhap_KhacTT = kq.Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay && p.PLoai == 1 && p.KieuDon != 1).Sum(p => p.ThanhTienN),

                                        //Nhập chuyển kho
                                        NhapCK = kq.Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay && p.PLoai == 1 && p.KieuDon == 0).Sum(p => p.SoLuongN),
                                        NhapCKTT = kq.Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay && p.PLoai == 1 && p.KieuDon == 0).Sum(p => p.ThanhTienN),

                                        //  nhập nội bộ(chuyển kho)
                                        NhapNB_SL = kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.PLoai == 1 && p.KieuDon == 0).Sum(p => p.SoLuongN),
                                        NhapNB_TT = kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.PLoai == 1 && p.KieuDon == 0).Sum(p => p.ThanhTienN),

                                        //nhập trả lại
                                        NhapTra_SL = kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.PLoai == 1 && p.KieuDon == 2).Sum(p => p.SoLuongN),
                                        NhapTra_TT = kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.PLoai == 1 && p.KieuDon == 2).Sum(p => p.ThanhTienN),
                                        #region 30002
                                        //xuất dịch vụ
                                        XuatDVSL = DungChung.Bien.MaBV == "30002" ? ((((kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.IDDTBN == idDTDV).Sum(p => p.SoLuongX) - kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.IDDTBN == idDTDV && p.PLoai == 1 && p.KieuDon == 2).Sum(p => p.SoLuongN)) > 0)) ?
                                                       (kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.IDDTBN == idDTDV).Sum(p => p.SoLuongX) - kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.IDDTBN == idDTDV && p.PLoai == 1 && p.KieuDon == 2).Sum(p => p.SoLuongN)) : 0) : 0,
                                        XuatDVTT = DungChung.Bien.MaBV == "30002" ? ((((kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.IDDTBN == idDTDV).Sum(p => p.ThanhTienX) - kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.IDDTBN == idDTDV && p.PLoai == 1 && p.KieuDon == 2).Sum(p => p.ThanhTienN)) > 0)) ?
                                       (kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.IDDTBN == idDTDV).Sum(p => p.ThanhTienX) - kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.IDDTBN == idDTDV && p.PLoai == 1 && p.KieuDon == 2).Sum(p => p.ThanhTienN)) : 0) : 0,
                                        //xuất bảo hiểm
                                        XuatBHSL = DungChung.Bien.MaBV == "30002" ? ((((kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.IDDTBN == idDTBH).Sum(p => p.SoLuongX) - kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.IDDTBN == idDTBH && p.PLoai == 1 && p.KieuDon == 2).Sum(p => p.SoLuongN)
                                        + kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.IDDTBN != idDTBH && p.IDDTBN != idDTDV && p.PLoai == 2 && (p.KieuDon == 1 || p.KieuDon == 5 || p.KieuDon == 7 || p.KieuDon == 11)).Sum(p => p.SoLuongX)
                                        ) > 0)) ?
                                                       (kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.IDDTBN == idDTBH).Sum(p => p.SoLuongX) - kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.IDDTBN == idDTBH && p.PLoai == 1 && p.KieuDon == 2).Sum(p => p.SoLuongN)
                                                       + kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.IDDTBN != idDTBH && p.IDDTBN != idDTDV && p.PLoai == 2 && (p.KieuDon == 1 || p.KieuDon == 5 || p.KieuDon == 7 || p.KieuDon == 11)).Sum(p => p.SoLuongX))
                                                       : 0) : 0,
                                        XuatBHTT = DungChung.Bien.MaBV == "30002" ? ((((kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.IDDTBN == idDTBH).Sum(p => p.ThanhTienX) - kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.IDDTBN == idDTBH && p.PLoai == 1 && p.KieuDon == 2).Sum(p => p.ThanhTienN)
                                        + kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.IDDTBN != idDTBH && p.IDDTBN != idDTDV && p.PLoai == 2 && (p.KieuDon == 1 || p.KieuDon == 5 || p.KieuDon == 7 || p.KieuDon == 11)).Sum(p => p.ThanhTienX)
                                        ) > 0)) ?
                                       (kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.IDDTBN == idDTBH).Sum(p => p.ThanhTienX) - kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.IDDTBN == idDTBH && p.PLoai == 1 && p.KieuDon == 2).Sum(p => p.ThanhTienN)
                                       + kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.IDDTBN != idDTBH && p.IDDTBN != idDTDV && p.PLoai == 2 && (p.KieuDon == 1 || p.KieuDon == 5 || p.KieuDon == 7 || p.KieuDon == 11)).Sum(p => p.ThanhTienX)
                                       ) : 0) : 0,
                                        //xuất khác
                                        XuatKhac30002SL = DungChung.Bien.MaBV == "30002" ? ((((kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.IDDTBN != idDTBH && p.IDDTBN != idDTDV).Sum(p => p.SoLuongX) - kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.IDDTBN != idDTBH && p.IDDTBN != idDTDV && p.PLoai == 1 && p.KieuDon == 2 && p.TraDuoc_KieuDon != 6).Sum(p => p.SoLuongN)
                                        - kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.IDDTBN != idDTBH && p.IDDTBN != idDTDV && p.PLoai == 2 && (p.KieuDon == 1 || p.KieuDon == 5 || p.KieuDon == 7 || p.KieuDon == 11)).Sum(p => p.SoLuongX)
                                        ) > 0)) ?
                                                       (kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.IDDTBN != idDTBH && p.IDDTBN != idDTDV).Sum(p => p.SoLuongX) - kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.IDDTBN != idDTBH && p.IDDTBN != idDTDV && p.PLoai == 1 && p.KieuDon == 2 && p.KieuDon == 2 && p.TraDuoc_KieuDon != 6).Sum(p => p.SoLuongN)
                                                       - kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.IDDTBN != idDTBH && p.IDDTBN != idDTDV && p.PLoai == 2 && (p.KieuDon == 1 || p.KieuDon == 5 || p.KieuDon == 7 || p.KieuDon == 11)).Sum(p => p.SoLuongX)
                                                       ) : 0) : 0,
                                        XuatKhac30002TT = DungChung.Bien.MaBV == "30002" ? ((((kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.IDDTBN != idDTBH && p.IDDTBN != idDTDV).Sum(p => p.ThanhTienX) - kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.IDDTBN != idDTBH && p.IDDTBN != idDTDV && p.PLoai == 1 && p.KieuDon == 2 && p.TraDuoc_KieuDon != 6).Sum(p => p.ThanhTienN)
                                        - kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.IDDTBN != idDTBH && p.IDDTBN != idDTDV && p.PLoai == 2 && (p.KieuDon == 1 || p.KieuDon == 5 || p.KieuDon == 7 || p.KieuDon == 11)).Sum(p => p.ThanhTienX)
                                        ) > 0)) ?
                                       (kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.IDDTBN != idDTBH && p.IDDTBN != idDTDV).Sum(p => p.ThanhTienX) - kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.IDDTBN != idDTBH && p.IDDTBN != idDTDV && p.PLoai == 1 && p.KieuDon == 2 && p.TraDuoc_KieuDon != 6).Sum(p => p.ThanhTienN)
                                       - kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.IDDTBN != idDTBH && p.IDDTBN != idDTDV && p.PLoai == 2 && (p.KieuDon == 1 || p.KieuDon == 5 || p.KieuDon == 7 || p.KieuDon == 11)).Sum(p => p.ThanhTienX)
                                       ) : 0) : 0,
                                        #endregion
                                        //xuất trong kỳ(không tính nhập trả dược - dùng cho mẫu BC nhập trả dược)
                                        TongXuatTKSL = check04007 ? (kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay).Where(p => (p.PLoai == 2 && (p.KieuDon != 2 && p.KieuDon != 3))).Sum(p => p.SoLuongX)
                                        + (kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay).Where(p => (p.PLoai == 5)).Sum(p => p.SoLuongSD)))
                                        : (((DungChung.Bien.MaBV == "27022" || DungChung.Bien.MaBV == "30005" || DungChung.Bien.MaBV == "30007") && radMauIn.SelectedIndex == 4) ? kq.Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.SoLuongX) : ((((kq.Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.SoLuongX) - kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.PLoai == 1 && p.KieuDon == 2).Sum(p => p.SoLuongN)) > 0 || DungChung.Bien.MaBV == "27023")) ? // || DungChung.Bien.MaBV == "30005" 
                                                       (kq.Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.SoLuongX) - kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.PLoai == 1 && p.KieuDon == 2).Sum(p => p.SoLuongN)) : 0)),
                                        TongXuatTKTT = ckcGiaNhap.Checked 
                                        ? kq.Key.GiaNhap == null ? 0 : (kq.Key.GiaNhap * ((kq.Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.SoLuongX) - kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.PLoai == 1 && p.KieuDon == 2).Sum(p => p.SoLuongN))))
                                        : check04007 ? (kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay).Where(p => (p.PLoai == 2 && (p.KieuDon != 2 && p.KieuDon != 3))).Sum(p => p.ThanhTienX)
                                        + (kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay).Where(p => (p.PLoai == 5)).Sum(p => p.ThanhTienSD)))
                                        : (((DungChung.Bien.MaBV == "27022" || DungChung.Bien.MaBV == "30005" || DungChung.Bien.MaBV == "30007") && radMauIn.SelectedIndex == 4) ? kq.Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.ThanhTienX) : ((((kq.Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.ThanhTienX) - kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.PLoai == 1 && p.KieuDon == 2).Sum(p => p.ThanhTienN)) > 0 || DungChung.Bien.MaBV == "27023")) ?
                                                         (kq.Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.ThanhTienX) - kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.PLoai == 1 && p.KieuDon == 2).Sum(p => p.ThanhTienN)) : 0)),

                                        TongXuatTKSL1 = ((DungChung.Bien.MaBV == "26007" || DungChung.Bien.MaBV == "30005" || DungChung.Bien.MaBV == "30004" || DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "56789")) ? // || DungChung.Bien.MaBV == "30005" 
                                                       (kq.Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.SoLuongX)) : 0,
                                        TongXuatTKTT1 = ((DungChung.Bien.MaBV == "26007" || DungChung.Bien.MaBV == "30005" || DungChung.Bien.MaBV == "30004" || DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "56789")) ?
                                                         (kq.Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.ThanhTienX)) : 0,

                                        //  xuất nội bộ(chuyển kho)
                                        XuatNB_SL = kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.KieuDon == 2).Sum(p => p.SoLuongX),
                                        XuatNB_TT = kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.KieuDon == 2).Sum(p => p.ThanhTienX),

                                        XuatNoiTruSL = (radLoaiXuat.SelectedIndex == 1) ? (kq.Where(p => p.IDDTBN == _cot1 && p.PLoai == 2).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.SoLuongX)) :
                                                        (kq.Where(p => p.KieuDon == _cot1 && p.PLoai == 2).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.SoLuongX)),
                                        xuatNoiTruTT = (radLoaiXuat.SelectedIndex == 1) ? (kq.Where(p => p.IDDTBN == _cot1 && p.PLoai == 2).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.ThanhTienX)) :
                                                        (kq.Where(p => p.KieuDon == _cot1 && p.PLoai == 2).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.ThanhTienX)),

                                        XuatNoiTruSL1 = kq.Where(p => p.KieuDon == _cot1 && p.PLoai == 2).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.SoLuongX) - kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.PLoai == 1 && p.TraDuoc_KieuDon == _cot1 && p.KieuDon == 2).Sum(p => p.SoLuongN),
                                        xuatNoiTruTT1 = kq.Where(p => p.KieuDon == _cot1 && p.PLoai == 2).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.ThanhTienX) - kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.PLoai == 1 && p.TraDuoc_KieuDon == _cot1 && p.KieuDon == 2).Sum(p => p.ThanhTienN),
                                        // xuatNoiTruTTTong = kq.Where(p => p.KieuDon == 0).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.ThanhTienX),

                                        XuatNgoaiTruSL = (radLoaiXuat.SelectedIndex == 1) ? (kq.Where(p => p.IDDTBN == _cot2 && p.PLoai == 2).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.SoLuongX)) :
                                                          (kq.Where(p => p.KieuDon == _cot2 && p.PLoai == 2).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.SoLuongX)),
                                        xuatNgoaiTruTT = (radLoaiXuat.SelectedIndex == 1) ? (kq.Where(p => p.IDDTBN == _cot2 && p.PLoai == 2).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.ThanhTienX)) :
                                                          (kq.Where(p => p.KieuDon == _cot2 && p.PLoai == 2).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.ThanhTienX)),

                                        XuatNgoaiTruSL1 = kq.Where(p => p.KieuDon == _cot2 && p.PLoai == 2).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.SoLuongX) - kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.PLoai == 1 && p.TraDuoc_KieuDon == _cot2 && p.KieuDon == 2).Sum(p => p.SoLuongN),
                                        xuatNgoaiTruTT1 = kq.Where(p => p.KieuDon == _cot2 && p.PLoai == 2).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.ThanhTienX) - kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.PLoai == 1 && p.TraDuoc_KieuDon == _cot2 && p.KieuDon == 2).Sum(p => p.ThanhTienN),

                                        XuatC3SL = (radLoaiXuat.SelectedIndex == 1) ? (kq.Where(p => p.IDDTBN == _cot3 && p.PLoai == 2).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.SoLuongX)) :
                                        (kq.Where(p => p.KieuDon == _cot3 && p.PLoai == 2).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.SoLuongX)),
                                        xuatC3TT = (radLoaiXuat.SelectedIndex == 1) ? (kq.Where(p => p.IDDTBN == _cot3 && p.PLoai == 2).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.ThanhTienX)) :
                                                          (kq.Where(p => p.KieuDon == _cot3 && p.PLoai == 2).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.ThanhTienX)),

                                        XuatC4SL = (radLoaiXuat.SelectedIndex == 1) ? (kq.Where(p => p.IDDTBN == _cot4 && p.PLoai == 2).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.SoLuongX)) :
                                        (kq.Where(p => p.KieuDon == _cot4 && p.PLoai == 2).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.SoLuongX)),
                                        xuatC4TT = (radLoaiXuat.SelectedIndex == 1) ? (kq.Where(p => p.IDDTBN == _cot4 && p.PLoai == 2).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.ThanhTienX)) :
                                        (kq.Where(p => p.KieuDon == _cot4 && p.PLoai == 2).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.ThanhTienX)),

                                        // xuatNgoaiTruTTTong = kq.Where(p => p.KieuDon == 1).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.ThanhTienX),
                                        XuatKhacSL = (radLoaiXuat.SelectedIndex == 1) ? kq.Where(p => p.IDDTBN != _cot1 && p.IDDTBN != _cot2 && p.IDDTBN != _cot3 && p.PLoai == 2 && (radMauIn.SelectedIndex == 3 ? p.PLoai == 2 : true)).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.SoLuongX) :
                                                      kq.Where(p => p.KieuDon != _cot1 && p.KieuDon != _cot2 && p.KieuDon != _cot3 && p.PLoai == 2 && (radMauIn.SelectedIndex == 3 ? p.PLoai == 2 : true)).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.SoLuongX),
                                        xuatKhacTT = (radLoaiXuat.SelectedIndex == 1) ? kq.Where(p => p.IDDTBN != _cot1 && p.IDDTBN != _cot2 && p.PLoai == 2 && (radMauIn.SelectedIndex == 3 ? p.PLoai == 2 : true)).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.ThanhTienX) :
                                                      kq.Where(p => p.KieuDon != _cot1 && p.KieuDon != _cot2 && p.KieuDon != _cot3 && p.PLoai == 2 && (radMauIn.SelectedIndex == 3 ? p.PLoai == 2 : true)).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.ThanhTienX),

                                        XuatKhacSL1 = kq.Where(p => p.KieuDon != _cot1 && p.KieuDon != _cot2 && p.PLoai == 2 && (radMauIn.SelectedIndex == 3 ? p.PLoai == 2 : true)).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.SoLuongX) - kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.PLoai == 1 && p.TraDuoc_KieuDon != _cot1 && p.TraDuoc_KieuDon != _cot2 && p.KieuDon == 2).Sum(p => p.SoLuongN),
                                        xuatKhacTT1 = kq.Where(p => p.KieuDon != _cot1 && p.KieuDon != _cot2 && p.PLoai == 2 && (radMauIn.SelectedIndex == 3 ? p.PLoai == 2 : true)).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.ThanhTienX) - kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.PLoai == 1 && p.TraDuoc_KieuDon != _cot1 && p.TraDuoc_KieuDon != _cot2 && p.KieuDon == 2).Sum(p => p.ThanhTienN),

                                        XuatKhacSL2 = (radLoaiXuat.SelectedIndex == 1) ? kq.Where(p => p.IDDTBN != _cot1 && p.IDDTBN != _cot2 && p.PLoai == 2 && (radMauIn.SelectedIndex == 3 ? p.PLoai == 2 : true)).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.SoLuongX) :
                                                     kq.Where(p => p.KieuDon != _cot1 && p.KieuDon != _cot2 && p.KieuDon != _cot3 && p.KieuDon != _cot4 && p.PLoai == 2 && (radMauIn.SelectedIndex == 3 ? p.PLoai == 2 : true)).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.SoLuongX),
                                        xuatKhacTT2 = (radLoaiXuat.SelectedIndex == 1) ? kq.Where(p => p.IDDTBN != _cot1 && p.IDDTBN != _cot2 && p.PLoai == 2 && (radMauIn.SelectedIndex == 3 ? p.PLoai == 2 : true)).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.ThanhTienX) :
                                                     kq.Where(p => p.KieuDon != _cot1 && p.KieuDon != _cot2 && p.KieuDon != _cot3 && p.KieuDon != _cot4 && p.PLoai == 2 && (radMauIn.SelectedIndex == 3 ? p.PLoai == 2 : true)).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.ThanhTienX),

                                        //Xuất khác dùng để xuất excel BV Chí Linh
                                        xuatKhacExcel = kq.Where(p => p.PLoai == 2 && p.KieuDon == 9).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.SoLuongX),

                                        XuatHuHaoSL = kq.Where(p => p.KieuDon != _cot1 && p.KieuDon != _cot2 && p.KieuDon != _cot3 && p.PLoai == 3).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.SoLuongX),
                                        XuatHuHaoTT = kq.Where(p => p.KieuDon != _cot1 && p.KieuDon != _cot2 && p.KieuDon != _cot3 && p.PLoai == 3).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.ThanhTienX),

                                        TonCKSL = kq.Where(p => p.NgayNhap <= denngay).Sum(p => p.SoLuongN) - kq.Where(p => p.NgayNhap <= denngay).Sum(p => p.SoLuongX),
                                        TonCKTT = ckcGiaNhap.Checked 
                                                    ? (kq.Key.GiaNhap == null ? 0 : (kq.Key.GiaNhap * (kq.Where(p => p.NgayNhap <= denngay).Sum(p => p.SoLuongN) - kq.Where(p => p.NgayNhap <= denngay).Sum(p => p.SoLuongX))))
                                                    : kq.Where(p => p.NgayNhap <= denngay).Sum(p => p.ThanhTienN) - kq.Where(p => p.NgayNhap <= denngay).Sum(p => p.ThanhTienX),
                                        TonCKSL1 = kq.Where(p => p.NgayNhap <= denngay).Sum(p => p.SoLuongN) + kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.PLoai == 1 && p.KieuDon == 2).Sum(p => p.SoLuongN) - kq.Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.SoLuongX),
                                        TonCKTT1 = kq.Where(p => p.NgayNhap <= denngay).Sum(p => p.ThanhTienN) + kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.PLoai == 1 && p.KieuDon == 2).Sum(p => p.ThanhTienN) - kq.Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.ThanhTienX),

                                    }).OrderBy(p => p.TenHamLuong).ToList();
                        #endregion

                        string msg = "";
                        for (int i = 0; i < qnxt.Count; i++)
                        {
                            if (qnxt[i].TonCKSL == 0 && qnxt[i].TonCKTT != 0)
                            {
                                double tonCK = Math.Round((double)qnxt[i].TonCKTT, 1);
                                double TT = 0;
                                if (qnxt[i].DonGia != null)
                                    TT = Math.Round((qnxt[i].TonCKSL * (double)qnxt[i].DonGia), 1);
                                if (tonCK != TT)
                                {
                                    msg += "- Thuốc: " + qnxt[i].TenHamLuong + ", MaDV: " + qnxt[i].MaDV + ", có số lượng cuối kỳ = " + qnxt[i].TonCKSL + ", đơn giá: " + qnxt[i].DonGia + " nhưng thành tiền cuối kỳ = " + qnxt[i].TonCKTT + "\n";
                                }
                            }
                        }

                        if (!string.IsNullOrEmpty(msg))
                            MessageBox.Show(msg, "Thông báo những thuốc sai tiền cuối kỳ");

                        #region list tinh tong
                        var _sq = (from a in qnxt
                                   select new
                                   {
                                       _tenNCC,
                                       a.IDNhom,
                                       a.TenNhom,
                                       a.NuocSX,
                                       a.STT,
                                       a.DonGia,
                                       a.DonVi,
                                       a.GiaNhap,
                                       //a.DonGiaCT,
                                       a.MaCC,
                                       a.MaDV,
                                       a.TenTN,
                                       a.MaTam,
                                       a.SoQD,
                                       NhapTKSL = check04007 ? a.NhapTKSL : ((_kpChon.Count > 1 || radMauIn.SelectedIndex == 3) ? (a.NhapTKSL - a.NhapNB_SL) : a.NhapTKSL),
                                       NhapTKTT = check04007 ? a.NhapTKTT : (_kpChon.Count > 1 || radMauIn.SelectedIndex == 3) ? (a.NhapTKTT - a.NhapNB_TT) : a.NhapTKTT,
                                       NhapTTTSL = ((_kpChon.Count > 1 || radMauIn.SelectedIndex == 3) ? (a.NhapTTTSL - a.NhapNB_SL) : a.NhapTTTSL),
                                       NhapTTTTT = (_kpChon.Count > 1 || radMauIn.SelectedIndex == 3) ? (a.NhapTTTTT - a.NhapNB_TT) : a.NhapTTTTT,
                                       TenHamLuong = !checkHC.Checked ? a.TenHamLuong : (a.TenHamLuong + ((a.TenHC != null && a.TenHC != "") ? (" [" + a.TenHC + "]") : "")),
                                       a.TenNhomDuoc,
                                       a.TenHC,
                                       a.NhomThau,
                                       a.HamLuong,
                                       NhapNB_SL = a.NhapNB_SL,
                                       NhapNB_TT = a.NhapNB_TT,
                                       //Nhap_SL = a.Nhap_SL,
                                       //Nhap_TT = a.Nhap_TT,
                                       NhapHD_SL = a.NhapHD,
                                       NhapHD_TT = a.NhapHDTT,
                                       NhapKhac_SL = a.Nhap_Khac,
                                       NhapKhac_TT = a.Nhap_KhacTT,
                                       NhapCK_SL = a.NhapCK,
                                       NhapCK_TT = a.NhapCKTT,
                                       NhapTra_SL = a.NhapTra_SL,
                                       NhapTra_TT = a.NhapTra_TT,
                                       TongCotXuatSL = a.XuatBHSL + a.XuatDVSL + a.XuatKhac30002SL,
                                       TongCotXuatTT = a.XuatBHTT + a.XuatDVTT + a.XuatKhac30002TT,
                                       TongXuatTKSL = a.TongXuatTKSL,
                                       TongXuatTKTT = a.TongXuatTKTT,
                                       TongXuatTKSL1 = a.TongXuatTKSL1,
                                       TongXuatTKTT1 = a.TongXuatTKTT1,
                                       XuatNB_SL = a.XuatNB_SL,
                                       XuatNB_TT = a.XuatNB_TT,
                                       XuatDVSL = a.XuatDVSL,
                                       XuatDVTT = a.XuatDVTT,
                                       XuatBHSL = a.XuatBHSL,
                                       XuatBHTT = a.XuatBHTT,
                                       XuatKhac30002SL = a.XuatKhac30002SL,
                                       XuatKhac30002TT = a.XuatKhac30002TT,
                                       TonCKSL = check04007 ? (a.TonDKSL04007 + a.NhapTKSL - a.TongXuatTKSL) : (checkHuHaoDY ? (a.TonCKSL - a.HuHaoDongY30004SL) : a.TonCKSL),
                                       TonCKTT = check04007 ? (a.TonDKTT04007 + a.NhapTKTT - a.TongXuatTKTT) : (checkHuHaoDY ? (a.TonCKTT - a.HuHaoDongY30004TT) : a.TonCKTT),
                                       TonCKSL1 = a.TonCKSL1,
                                       TonCKTT1 = a.TonCKTT1,
                                       TonDKSL = check04007 ? a.TonDKSL04007 : (checkHuHaoDY ? a.TonDKSL30004 : a.TonDKSL),
                                       TonDKTT = check04007 ? a.TonDKTT04007 : (checkHuHaoDY ? a.TonDKTT30004 : a.TonDKTT),
                                       XuatKhacSL = check01049 ? a.XuatKhac01049SL : a.XuatKhacSL,
                                       xuatKhacTT = check01049 ? a.XuatKhac01049TT : a.xuatKhacTT,
                                       XuatC3SL = a.XuatC3SL,
                                       xuatC3TT = a.xuatC3TT,
                                       XuatC4SL = a.XuatC4SL,
                                       xuatC4TT = a.xuatC4TT,
                                       XuatHuHaoSL = a.XuatHuHaoSL,
                                       XuatHuHaoTT = a.XuatHuHaoTT,
                                       XuatNoiTruSL = check01049 ? a.XuatNgoaiTru01049SL : a.XuatNoiTruSL,
                                       xuatNoiTruTT = check01049 ? a.XuatNgoaiTru01049TT : a.xuatNoiTruTT,
                                       XuatNgoaiTruSL = check01049 ? a.XuatKhoaPhong01049SL : a.XuatNgoaiTruSL,
                                       xuatNgoaiTruTT = check01049 ? a.XuatKhoaPhong01049TT : a.xuatNgoaiTruTT,
                                       XuatKhacExcel = a.xuatKhacExcel,
                                       XuatTKTongSL = check04007 ? a.TongXuatTKSL : (_kpChon.Count > 1 || radMauIn.SelectedIndex == 3) ? (a.TongXuatTKSL - a.XuatNB_SL) : (checkHuHaoDY ? (a.TongXuatTKSL + a.HuHaoDongY30004SL) : a.TongXuatTKSL), // dùng trong mẫu nhiều kho (tách riêng phần xuất xội bộ)
                                       xuatTKTongTT = check04007 ? a.TongXuatTKTT : (_kpChon.Count > 1 || radMauIn.SelectedIndex == 3) ? (a.TongXuatTKTT - a.XuatNB_TT) : (checkHuHaoDY ? (a.TongXuatTKTT + a.HuHaoDongY30004TT) : a.TongXuatTKTT),
                                       XuatTKTongSL1 = a.TongXuatTKSL1,
                                       xuatTKTongTT1 = a.TongXuatTKTT1,
                                       XuatNoiTruSL1 = a.XuatNoiTruSL1,
                                       XuatNoiTruTT1 = a.xuatNoiTruTT1,
                                       XuatKhacSL1 = a.XuatKhacSL1,
                                       xuatKhacTT1 = a.xuatKhacTT1,
                                       XuatKhacSL2 = a.XuatKhacSL2,
                                       xuatKhacTT2 = a.xuatKhacTT2,
                                       XuatNgoaiTruSL1 = a.XuatNgoaiTruSL1,
                                       XuatNgoaiTruTT1 = a.xuatNgoaiTruTT1
                                   }).Where(p => p.TonDKSL != 0 || p.TonCKSL != 0 || p.NhapTKSL != 0 || p.TongXuatTKSL != 0 || (DungChung.Bien.MaBV == "30002" && (p.NhapTTTSL != 0 || p.XuatDVSL != 0 || p.XuatBHSL != 0 || p.XuatKhac30002SL != 0))).ToList().OrderBy(p => (chkHienthi.Checked ? p.TenNhomDuoc : p.TenHamLuong)).ThenBy(p => p.STT).ThenBy(p => p.TenHamLuong).ToList();
                        var _sq24012 = (from a in qnxt1
                                        select new
                                        {
                                            _tenNCC,
                                            a.solo,
                                            a.handung,
                                            a.TenNhom,
                                            a.NuocSX,
                                            a.STT,
                                            a.DonGia,
                                            a.DonVi,
                                            a.GiaNhap,
                                            //a.DonGiaCT,
                                            a.MaCC,
                                            a.MaDV,
                                            a.TenTN,
                                            a.MaTam,
                                            a.SoQD,
                                            NhapTKSL = check04007 ? a.NhapTKSL : ((_kpChon.Count > 1 || radMauIn.SelectedIndex == 3) ? (a.NhapTKSL - a.NhapNB_SL) : a.NhapTKSL),
                                            NhapTKTT = check04007 ? a.NhapTKTT : (_kpChon.Count > 1 || radMauIn.SelectedIndex == 3) ? (a.NhapTKTT - a.NhapNB_TT) : a.NhapTKTT,
                                            NhapTTTSL = ((_kpChon.Count > 1 || radMauIn.SelectedIndex == 3) ? (a.NhapTTTSL - a.NhapNB_SL) : a.NhapTTTSL),
                                            NhapTTTTT = (_kpChon.Count > 1 || radMauIn.SelectedIndex == 3) ? (a.NhapTTTTT - a.NhapNB_TT) : a.NhapTTTTT,
                                            TenHamLuong = !checkHC.Checked ? a.TenHamLuong : (a.TenHamLuong + ((a.TenHC != null && a.TenHC != "") ? (" [" + a.TenHC + "]") : "")),
                                            a.TenNhomDuoc,
                                            a.TenHC,
                                            NhapNB_SL = a.NhapNB_SL,
                                            NhapNB_TT = a.NhapNB_TT,
                                            //Nhap_SL = a.Nhap_SL,
                                            //Nhap_TT = a.Nhap_TT,
                                            NhapHD_SL = a.NhapHD,
                                            NhapHD_TT = a.NhapHDTT,
                                            NhapKhac_SL = a.Nhap_Khac,
                                            NhapKhac_TT = a.Nhap_KhacTT,
                                            NhapCK_SL = a.NhapCK,
                                            NhapCK_TT = a.NhapCKTT,
                                            NhapTra_SL = a.NhapTra_SL,
                                            NhapTra_TT = a.NhapTra_TT,
                                            TongCotXuatSL = a.XuatBHSL + a.XuatDVSL + a.XuatKhac30002SL,
                                            TongCotXuatTT = a.XuatBHTT + a.XuatDVTT + a.XuatKhac30002TT,
                                            TongXuatTKSL = a.TongXuatTKSL,
                                            TongXuatTKTT = a.TongXuatTKTT,
                                            TongXuatTKSL1 = a.TongXuatTKSL1,
                                            TongXuatTKTT1 = a.TongXuatTKTT1,
                                            XuatNB_SL = a.XuatNB_SL,
                                            XuatNB_TT = a.XuatNB_TT,
                                            XuatDVSL = a.XuatDVSL,
                                            XuatDVTT = a.XuatDVTT,
                                            XuatBHSL = a.XuatBHSL,
                                            XuatBHTT = a.XuatBHTT,
                                            XuatKhac30002SL = a.XuatKhac30002SL,
                                            XuatKhac30002TT = a.XuatKhac30002TT,
                                            TonCKSL = check04007 ? (a.TonDKSL04007 + a.NhapTKSL - a.TongXuatTKSL) : (checkHuHaoDY ? (a.TonCKSL - a.HuHaoDongY30004SL) : a.TonCKSL),
                                            TonCKTT = check04007 ? (a.TonDKTT04007 + a.NhapTKTT - a.TongXuatTKTT) : (checkHuHaoDY ? (a.TonCKTT - a.HuHaoDongY30004TT) : a.TonCKTT),
                                            TonCKSL1 = a.TonCKSL1,
                                            TonCKTT1 = a.TonCKTT1,
                                            TonDKSL = check04007 ? a.TonDKSL04007 : (checkHuHaoDY ? a.TonDKSL30004 : a.TonDKSL),
                                            TonDKTT = check04007 ? a.TonDKTT04007 : (checkHuHaoDY ? a.TonDKTT30004 : a.TonDKTT),
                                            XuatKhacSL = check01049 ? a.XuatKhac01049SL : a.XuatKhacSL,
                                            xuatKhacTT = check01049 ? a.XuatKhac01049TT : a.xuatKhacTT,
                                            XuatC3SL = a.XuatC3SL,
                                            xuatC3TT = a.xuatC3TT,
                                            XuatC4SL = a.XuatC4SL,
                                            xuatC4TT = a.xuatC4TT,
                                            XuatHuHaoSL = a.XuatHuHaoSL,
                                            XuatHuHaoTT = a.XuatHuHaoTT,
                                            XuatNoiTruSL = check01049 ? a.XuatNgoaiTru01049SL : a.XuatNoiTruSL,
                                            xuatNoiTruTT = check01049 ? a.XuatNgoaiTru01049TT : a.xuatNoiTruTT,
                                            XuatNgoaiTruSL = check01049 ? a.XuatKhoaPhong01049SL : a.XuatNgoaiTruSL,
                                            xuatNgoaiTruTT = check01049 ? a.XuatKhoaPhong01049TT : a.xuatNgoaiTruTT,
                                            XuatKhacExcel = a.xuatKhacExcel,
                                            XuatTKTongSL = check04007 ? a.TongXuatTKSL : (_kpChon.Count > 1 || radMauIn.SelectedIndex == 3) ? (a.TongXuatTKSL - a.XuatNB_SL) : (checkHuHaoDY ? (a.TongXuatTKSL + a.HuHaoDongY30004SL) : a.TongXuatTKSL), // dùng trong mẫu nhiều kho (tách riêng phần xuất xội bộ)
                                            xuatTKTongTT = check04007 ? a.TongXuatTKTT : (_kpChon.Count > 1 || radMauIn.SelectedIndex == 3) ? (a.TongXuatTKTT - a.XuatNB_TT) : (checkHuHaoDY ? (a.TongXuatTKTT + a.HuHaoDongY30004TT) : a.TongXuatTKTT),
                                            XuatTKTongSL1 = a.TongXuatTKSL1,
                                            xuatTKTongTT1 = a.TongXuatTKTT1,
                                            XuatNoiTruSL1 = a.XuatNoiTruSL1,
                                            XuatNoiTruTT1 = a.xuatNoiTruTT1,
                                            XuatKhacSL1 = a.XuatKhacSL1,
                                            xuatKhacTT1 = a.xuatKhacTT1,
                                            XuatKhacSL2 = a.XuatKhacSL2,
                                            xuatKhacTT2 = a.xuatKhacTT2,
                                            XuatNgoaiTruSL1 = a.XuatNgoaiTruSL1,
                                            XuatNgoaiTruTT1 = a.xuatNgoaiTruTT1
                                        }).Where(p => p.TonDKSL != 0 || p.TonCKSL != 0 || p.NhapTKSL != 0 || p.TongXuatTKSL != 0 || (DungChung.Bien.MaBV == "30002" && (p.NhapTTTSL != 0 || p.XuatDVSL != 0 || p.XuatBHSL != 0 || p.XuatKhac30002SL != 0))).ToList().OrderBy(p => (chkHienthi.Checked ? p.TenNhomDuoc : p.TenHamLuong)).ThenBy(p => p.STT).ThenBy(p => p.TenHamLuong).ToList();
                        var _sq20001 = (from a in _sq
                                        join dv in data.DichVus on a.MaDV equals dv.MaDV
                                        select new
                                        {
                                            _tenNCC,
                                            DonGia = a.DonGia * 100 / (100 + dv.TyLeBQ + dv.TyLeSP),
                                            a.TenNhom,
                                            a.DonVi,
                                            a.MaCC,
                                            a.MaDV,
                                            a.MaTam,
                                            a.NhapCK_SL,
                                            a.NhapCK_TT,
                                            a.NhapHD_SL,
                                            a.NhapHD_TT,
                                            a.NhapKhac_SL,
                                            a.NhapKhac_TT,
                                            a.NhapNB_SL,
                                            a.NhapNB_TT,
                                            a.NhapTKSL,
                                            NhapTKTT = a.DonGia * 100 / (100 + dv.TyLeBQ + dv.TyLeSP) * a.NhapTKSL,
                                            a.NhapTra_SL,
                                            a.NhapTra_TT,
                                            a.NhapTTTSL,
                                            a.NhapTTTTT,
                                            a.NuocSX,
                                            a.SoQD,
                                            a.STT,
                                            a.TenHamLuong,
                                            a.TenHC,
                                            a.TenNhomDuoc,
                                            a.TenTN,
                                            a.TonCKSL,
                                            a.TonCKSL1,
                                            TonCKTT = a.DonGia * 100 / (100 + dv.TyLeBQ + dv.TyLeSP) * a.TonCKSL,
                                            a.TonCKTT1,
                                            a.TonDKSL,
                                            TonDKTT = a.DonGia * 100 / (100 + dv.TyLeBQ + dv.TyLeSP) * a.TonDKSL,
                                            a.TongCotXuatSL,
                                            a.TongCotXuatTT,
                                            a.TongXuatTKSL,
                                            a.TongXuatTKSL1,
                                            a.TongXuatTKTT,
                                            a.TongXuatTKTT1,
                                            a.XuatBHSL,
                                            a.XuatBHTT,
                                            a.XuatC3SL,
                                            a.xuatC3TT,
                                            a.XuatC4SL,
                                            a.xuatC4TT,
                                            a.XuatDVSL,
                                            a.XuatDVTT,
                                            a.XuatHuHaoSL,
                                            a.XuatHuHaoTT,
                                            a.XuatKhac30002SL,
                                            a.XuatKhac30002TT,
                                            a.XuatKhacExcel,
                                            a.XuatKhacSL,
                                            a.XuatKhacSL1,
                                            a.XuatKhacSL2,
                                            a.xuatKhacTT,
                                            a.xuatKhacTT1,
                                            a.xuatKhacTT2,
                                            a.XuatNB_SL,
                                            a.XuatNB_TT,
                                            a.XuatNgoaiTruSL,
                                            a.XuatNgoaiTruSL1,
                                            a.xuatNgoaiTruTT,
                                            a.XuatNgoaiTruTT1,
                                            a.XuatNoiTruSL,
                                            a.XuatNoiTruSL1,
                                            a.xuatNoiTruTT,
                                            a.XuatNoiTruTT1,
                                            a.XuatTKTongSL,
                                            a.XuatTKTongSL1,
                                            XuatTKTongTT = a.DonGia * 100 / (100 + dv.TyLeBQ + dv.TyLeSP) * a.XuatTKTongSL,
                                            a.xuatTKTongTT1,
                                            dv.TenDV,
                                        });
                        var q4 = _sq20001.Where(p => (string.IsNullOrEmpty(_nhacc)) ? true : p.MaCC == _nhacc).ToList();
                        #endregion
                        var q24012 = _sq24012.Where(p => (string.IsNullOrEmpty(_nhacc)) ? true : p.MaCC == _nhacc).ToList();
                        var q = _sq.Where(p => (string.IsNullOrEmpty(_nhacc)) ? true : p.MaCC == _nhacc).ToList();
                        if (DungChung.Bien.MaBV == "56789" && chkXuatTrongKy.Checked)
                        {
                            q = q.Where(o => o.XuatTKTongSL > 0 && o.xuatTKTongTT > 0).ToList();
                        }
                        #region xuất excel BV đa khoa Chí Linh
                        if (ckbBVChiLinh.Checked)
                        {
                            var q30003 = (from a in q
                                          join b in dichvu on a.MaDV equals b.MaDV
                                          join c in data.DichVuExes on a.MaDV equals c.MaDV into qCL
                                          from q1 in qCL.DefaultIfEmpty()
                                          group new { a, b } by new { a.MaCC, b.TenNhom, b.DuongD, b.NuocSX, b.NhaSX, b.MaQD, b.QCPC, b.MaNhom, b.NhomThau, b.SoQD, b.NgayQD, a.TenHamLuong, a.DonGia, a.DonVi, a.MaDV, a.TenTN, b.TenDV, b.HamLuong, b.TenHC, b.MaTam, b.SoDK, MaATC = q1 == null ? "" : q1.MaATC } into kq
                                          select new
                                          {
                                              _tenNCC,
                                              kq.Key.TenNhom,
                                              TenTN = kq.Key.TenTN,
                                              MaDV = kq.Key.MaDV,
                                              TenHamLuong = kq.Key.TenHamLuong,
                                              kq.Key.MaCC,
                                              kq.Key.DonGia,
                                              kq.Key.DonVi,
                                              kq.Key.HamLuong,
                                              kq.Key.MaATC,
                                              kq.Key.TenHC,
                                              MaTam = kq.Key.MaTam,
                                              kq.Key.SoDK,
                                              kq.Key.NuocSX,
                                              kq.Key.DuongD,
                                              kq.Key.TenDV,
                                              TonDKSL = kq.Sum(p => p.a.TonDKSL),
                                              NhapTKSL = kq.Sum(p => p.a.NhapTKSL),
                                              NhapKhac = kq.Sum(p => p.a.NhapTra_SL),
                                              XuatTKTongSL = kq.Sum(p => p.a.TongXuatTKSL),
                                              XuatKhacSL = kq.Sum(p => p.a.XuatKhacExcel),
                                              TonCKSL = kq.Sum(p => p.a.TonCKSL),
                                              NhaSX = kq.Key.NhaSX,
                                              NhapNB_SL = kq.Sum(p => p.a.NhapNB_SL),
                                              XuatNB_SL = kq.Sum(p => p.a.XuatNB_SL),
                                              XuatTK_SL = kq.Sum(p => p.a.XuatTKTongSL),
                                              kq.Key.MaQD,
                                              kq.Key.QCPC,
                                              kq.Key.MaNhom,
                                              kq.Key.NhomThau,
                                              kq.Key.SoQD,
                                              kq.Key.NgayQD,
                                          }).ToList();

                            string[] _arr = new string[] { "0", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@" };
                            string[] _arr1 = new string[] { "0", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@" };
                            int[] _arrWidth = new int[] { };
                            // int[] _arrWidth = new int[] { }; có thể gọi khi truyền kiểu fomatAutowith = true
                            int num = 1;
                            DungChung.Bien.MangHaiChieu = new Object[q.Count + 1, 20];


                            if (DungChung.Bien.MaBV == "30009")
                            {


                                DungChung.Bien.MangHaiChieu = new Object[q.Count + 1, 29];
                                string[] _tieude = { "STT", "Mã ATC", "Mã Nội Bộ", "Tên Biệt Dược", "Tên Hoạt Chất", "Hàm Lượng", "Tên tiểu nhóm", "Nhóm", "Số ĐK", "Đường Dùng", "Đơn Vị", "Nước SX", "Nhà SX", "Mã QĐ BYT", "QCPC", "Nhà Thầu", "Gói Thầu", "Nhóm Thầu", "Số QĐ", "Năm Công Bố", "Đơn Giá", "Tồn ĐK", "Nhập", "Nhập Khác", "Xuất", "Xuất Khác", "Tồn CK" };
                                foreach (var r in q30003)
                                {
                                    DungChung.Bien.MangHaiChieu[num, 0] = num;
                                    DungChung.Bien.MangHaiChieu[num, 1] = r.MaATC;
                                    DungChung.Bien.MangHaiChieu[num, 2] = r.MaTam;
                                    DungChung.Bien.MangHaiChieu[num, 3] = r.TenDV;
                                    DungChung.Bien.MangHaiChieu[num, 4] = r.TenHC;
                                    DungChung.Bien.MangHaiChieu[num, 5] = r.HamLuong;
                                    DungChung.Bien.MangHaiChieu[num, 6] = r.TenTN;
                                    DungChung.Bien.MangHaiChieu[num, 7] = r.TenNhom;
                                    DungChung.Bien.MangHaiChieu[num, 8] = r.SoDK;
                                    DungChung.Bien.MangHaiChieu[num, 9] = r.DuongD;
                                    DungChung.Bien.MangHaiChieu[num, 10] = r.DonVi;
                                    DungChung.Bien.MangHaiChieu[num, 11] = r.NuocSX;
                                    DungChung.Bien.MangHaiChieu[num, 12] = r.NhaSX;
                                    DungChung.Bien.MangHaiChieu[num, 13] = r.MaQD;
                                    DungChung.Bien.MangHaiChieu[num, 14] = r.QCPC;
                                    var dichvu1 = (from a in data.DichVus.Where(p => p.MaDV == r.MaDV)
                                                   join nhacc in data.NhaCCs on r.MaCC equals nhacc.MaCC
                                                   select new { nhacc.MaCC, nhacc.TenCC });
                                    if (dichvu1.Count() > 0)
                                    {
                                        DungChung.Bien.MangHaiChieu[num, 15] = dichvu1.First().TenCC;
                                    }
                                    else
                                    {
                                        DungChung.Bien.MangHaiChieu[num, 15] = "";
                                    }
                                    DungChung.Bien.MangHaiChieu[num, 16] = r.MaNhom;
                                    DungChung.Bien.MangHaiChieu[num, 17] = r.NhomThau;
                                    DungChung.Bien.MangHaiChieu[num, 18] = r.SoQD;
                                    DungChung.Bien.MangHaiChieu[num, 19] = r.NgayQD;
                                    DungChung.Bien.MangHaiChieu[num, 20] = r.DonGia;
                                    DungChung.Bien.MangHaiChieu[num, 21] = r.TonDKSL;
                                    DungChung.Bien.MangHaiChieu[num, 22] = r.NhapTKSL;
                                    DungChung.Bien.MangHaiChieu[num, 23] = r.NhapNB_SL;
                                    DungChung.Bien.MangHaiChieu[num, 24] = r.XuatTK_SL;
                                    DungChung.Bien.MangHaiChieu[num, 25] = r.XuatNB_SL;
                                    DungChung.Bien.MangHaiChieu[num, 26] = r.TonCKSL;
                                    num++;
                                }
                                //string[] _tieude = { "STT", "Tên Hoạt Chất", "Mã ATC", "Mã Nội Bộ", "Tên Biệt Dược", "Số ĐK", "Nhà SX", "Nước SX", "Nồng Độ", "Đơn Vị", "Đường Dùng", "Đơn Giá", "Tồn ĐK", "Nhập", "Nhập Chuyển kho", "Xuất", "Xuất chuyển kho", "Tồn CK" };
                                //foreach (var r in q30003)
                                //{
                                //    DungChung.Bien.MangHaiChieu[num, 0] = num;
                                //    DungChung.Bien.MangHaiChieu[num, 1] = r.TenHC;
                                //    DungChung.Bien.MangHaiChieu[num, 2] = r.TenTN;
                                //    DungChung.Bien.MangHaiChieu[num, 3] = r.MaATC;
                                //    DungChung.Bien.MangHaiChieu[num, 4] = r.MaTam;
                                //    DungChung.Bien.MangHaiChieu[num, 5] = r.TenHamLuong;
                                //    DungChung.Bien.MangHaiChieu[num, 6] = r.SoDK;
                                //    DungChung.Bien.MangHaiChieu[num, 7] = r.NhaSX;
                                //    DungChung.Bien.MangHaiChieu[num, 8] = r.NuocSX;
                                //    DungChung.Bien.MangHaiChieu[num, 9] = r.HamLuong;
                                //    DungChung.Bien.MangHaiChieu[num, 10] = r.DonVi;
                                //    DungChung.Bien.MangHaiChieu[num, 11] = r.DuongD;
                                //    DungChung.Bien.MangHaiChieu[num, 12] = r.DonGia;
                                //    DungChung.Bien.MangHaiChieu[num, 13] = r.TonDKSL;
                                //    DungChung.Bien.MangHaiChieu[num, 14] = r.NhapTKSL;
                                //    DungChung.Bien.MangHaiChieu[num, 15] = r.NhapNB_SL;
                                //    DungChung.Bien.MangHaiChieu[num, 16] = r.XuatTK_SL;
                                //    DungChung.Bien.MangHaiChieu[num, 17] = r.XuatNB_SL;
                                //    DungChung.Bien.MangHaiChieu[num, 18] = r.TonCKSL;
                                //    num++;
                                //}
                                for (int i = 0; i < _tieude.Length; i++)
                                {
                                    DungChung.Bien.MangHaiChieu[0, i] = _tieude[i].ToUpper();
                                }
                            }
                            else if (DungChung.Bien.MaBV == "27023")
                            {
                                string[] _tieude = { "STT", "Tên Hoạt Chất", "Mã ATC", "Mã Nội Bộ", "Mã QĐ BYT", "Tên Biệt Dược", "Số ĐK", "Nhà SX", "Nước SX", "Nồng Độ", "Đơn Vị", "Đường Dùng", "Đơn Giá", "Tồn ĐK", "Nhập", "Nhập Khác", "Xuất", "Xuất Khác", "Tồn CK" };
                                foreach (var r in q30003)
                                {
                                    DungChung.Bien.MangHaiChieu[num, 0] = num;
                                    DungChung.Bien.MangHaiChieu[num, 1] = r.TenHC;
                                    DungChung.Bien.MangHaiChieu[num, 2] = r.MaATC;
                                    DungChung.Bien.MangHaiChieu[num, 3] = r.MaTam;
                                    DungChung.Bien.MangHaiChieu[num, 4] = r.MaQD;
                                    DungChung.Bien.MangHaiChieu[num, 5] = r.TenHamLuong;
                                    DungChung.Bien.MangHaiChieu[num, 6] = r.SoDK;
                                    DungChung.Bien.MangHaiChieu[num, 7] = r.NhaSX;
                                    DungChung.Bien.MangHaiChieu[num, 8] = r.NuocSX;
                                    DungChung.Bien.MangHaiChieu[num, 9] = r.HamLuong;
                                    DungChung.Bien.MangHaiChieu[num, 10] = r.DonVi;
                                    DungChung.Bien.MangHaiChieu[num, 11] = r.DuongD;
                                    DungChung.Bien.MangHaiChieu[num, 12] = r.DonGia;
                                    DungChung.Bien.MangHaiChieu[num, 13] = r.TonDKSL;
                                    DungChung.Bien.MangHaiChieu[num, 14] = r.NhapTKSL;
                                    DungChung.Bien.MangHaiChieu[num, 15] = r.NhapKhac;
                                    DungChung.Bien.MangHaiChieu[num, 16] = r.XuatTKTongSL;
                                    DungChung.Bien.MangHaiChieu[num, 17] = r.XuatKhacSL;
                                    DungChung.Bien.MangHaiChieu[num, 18] = r.TonCKSL;
                                    num++;
                                }

                                for (int i = 0; i < _tieude.Length; i++)
                                {
                                    DungChung.Bien.MangHaiChieu[0, i] = _tieude[i].ToUpper();
                                }
                            }
                            else
                            {
                                string[] _tieude = { "STT", "Tên Hoạt Chất", "Mã ATC", "Mã Nội Bộ", "Tên Biệt Dược", "Số ĐK", "Nhà SX", "Nước SX", "Nồng Độ", "Đơn Vị", "Đường Dùng", "Đơn Giá", "Tồn ĐK", "Nhập", "Nhập Khác", "Xuất", "Xuất Khác", "Tồn CK" };
                                foreach (var r in q30003)
                                {
                                    DungChung.Bien.MangHaiChieu[num, 0] = num;
                                    DungChung.Bien.MangHaiChieu[num, 1] = r.TenHC;
                                    DungChung.Bien.MangHaiChieu[num, 2] = r.MaATC;
                                    DungChung.Bien.MangHaiChieu[num, 3] = r.MaTam;
                                    DungChung.Bien.MangHaiChieu[num, 4] = r.TenHamLuong;
                                    DungChung.Bien.MangHaiChieu[num, 5] = r.SoDK;
                                    DungChung.Bien.MangHaiChieu[num, 6] = r.NhaSX;
                                    DungChung.Bien.MangHaiChieu[num, 7] = r.NuocSX;
                                    DungChung.Bien.MangHaiChieu[num, 8] = r.HamLuong;
                                    DungChung.Bien.MangHaiChieu[num, 9] = r.DonVi;
                                    DungChung.Bien.MangHaiChieu[num, 10] = r.DuongD;
                                    DungChung.Bien.MangHaiChieu[num, 11] = r.DonGia;
                                    DungChung.Bien.MangHaiChieu[num, 12] = r.TonDKSL;
                                    DungChung.Bien.MangHaiChieu[num, 13] = r.NhapTKSL;
                                    DungChung.Bien.MangHaiChieu[num, 14] = r.NhapKhac;
                                    DungChung.Bien.MangHaiChieu[num, 15] = r.XuatTKTongSL;
                                    DungChung.Bien.MangHaiChieu[num, 16] = r.XuatKhacSL;
                                    DungChung.Bien.MangHaiChieu[num, 17] = r.TonCKSL;
                                    num++;
                                }

                                for (int i = 0; i < _tieude.Length; i++)
                                {
                                    DungChung.Bien.MangHaiChieu[0, i] = _tieude[i].ToUpper();
                                }
                            }
                            if (DungChung.Bien.MaBV == "30009")
                            {
                                QLBV_Library.QLBV_Ham.xuatExcelArr(DungChung.Bien.MangHaiChieu, _arr1, _arrWidth, "Báo cáo NXT TTYT Huyện Thanh Hà", "C:\\TsBCNXT_TTYTHTH_30003.xls", true);
                            }
                            else
                            {
                                QLBV_Library.QLBV_Ham.xuatExcelArr(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "Báo cáo NXT BV ĐK Chí Linh", "C:\\TsBCNXT_BVDKCL_30003.xls", true);
                            }

                            return;
                        }
                        #endregion
                        #region mẫu rút gọn BV Lao Phổi Lai Châu
                        if (ckbBVLaoPhoi.Checked)
                        {
                            if (DungChung.Bien.MaBV == "12122")
                            {
                                var q12122 = (from a in q
                                              join b in dichvu on a.MaDV equals b.MaDV
                                              group new { a, b } by new { a.TenHamLuong, a.NuocSX, a.TenNhomDuoc, a.TenTN, a.STT, a.DonGia, a.DonVi, a.MaDV, b.HamLuong, a.MaTam } into kq
                                              select new
                                              {
                                                  MaTam = kq.Key.MaTam,
                                                  MaDV = kq.Key.MaDV,
                                                  kq.Key.NuocSX,
                                                  TenHamLuong = kq.Key.TenHamLuong,
                                                  kq.Key.TenNhomDuoc,
                                                  kq.Key.TenTN,
                                                  kq.Key.STT,
                                                  kq.Key.DonGia,
                                                  kq.Key.DonVi,
                                                  kq.Key.HamLuong,
                                                  TonDKSL = kq.Sum(p => p.a.TonDKSL),
                                                  TonDKTT = kq.Sum(p => p.a.TonDKTT),
                                                  NhapTKSL = kq.Sum(p => p.a.NhapTKSL),
                                                  NhapTKTT = kq.Sum(p => p.a.NhapTKTT),
                                                  XuatTKTongSL = kq.Sum(p => p.a.TongXuatTKSL),// dung sửa 130417
                                                  xuatTKTongTT = kq.Sum(p => p.a.TongXuatTKSL),// dung sửa 130417
                                                  TonCKSL = kq.Sum(p => p.a.TonCKSL),
                                                  TonCKTT = kq.Sum(p => p.a.TonCKTT)
                                              }).ToList();
                                string[] _arr = new string[] { "0", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@" };
                                int[] _arrWidth = new int[] { };
                                // int[] _arrWidth = new int[] { }; có thể gọi khi truyền kiểu fomatAutowith = true
                                int num = 1;
                                DungChung.Bien.MangHaiChieu = new Object[q.Count + 1, 20];
                                string[] _tieude = { "STT", "TenThuoc", "Hàm Lượng", "DVT", "DonGia", "Tồn ĐK - SL", "Tồn ĐK - TT", "Nhập TK - Sl", "Nhập TK - TT", "Tổng xuất TK - SL", "Tổng xuất TK - TT", "Tồn CK - SL", "Tồn CK - TT" };
                                for (int i = 0; i < _tieude.Length; i++)
                                {
                                    DungChung.Bien.MangHaiChieu[0, i] = _tieude[i];
                                }

                                //for (int i = 0; i <= 17; i++) {
                                //    DungChung.Bien.MangHaiChieu[0, i] = _arr[0,i];
                                //}
                                foreach (var r in q12122)
                                {
                                    DungChung.Bien.MangHaiChieu[num, 0] = num;
                                    DungChung.Bien.MangHaiChieu[num, 1] = r.TenHamLuong;
                                    DungChung.Bien.MangHaiChieu[num, 2] = r.HamLuong;
                                    DungChung.Bien.MangHaiChieu[num, 3] = r.DonVi;
                                    DungChung.Bien.MangHaiChieu[num, 4] = r.DonGia;
                                    DungChung.Bien.MangHaiChieu[num, 5] = r.TonDKSL;
                                    DungChung.Bien.MangHaiChieu[num, 6] = r.TonDKTT;
                                    DungChung.Bien.MangHaiChieu[num, 7] = r.NhapTKSL;
                                    DungChung.Bien.MangHaiChieu[num, 8] = r.NhapTKTT;
                                    DungChung.Bien.MangHaiChieu[num, 9] = r.XuatTKTongSL;
                                    DungChung.Bien.MangHaiChieu[num, 10] = r.xuatTKTongTT;
                                    DungChung.Bien.MangHaiChieu[num, 11] = r.TonCKSL;
                                    DungChung.Bien.MangHaiChieu[num, 12] = r.TonCKTT;
                                    num++;
                                }
                                BaoCao.repBcNXTrutgon_12122 rep = new BaoCao.repBcNXTrutgon_12122(chkHienthi.Checked, ckTrongNgoaiNuoc.Checked);
                                frmIn frm2 = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "Báo cáo NXT BV Lao Phổi Lai Châu", "C:\\TsBCNXT_RG_12122.xls", true, this.Name);
                                rep.TuNgay.Value = dateTuNgay.Text;
                                rep.DenNgay.Value = dateDenNgay.Text;
                                rep.Kho.Value = _tenkho;
                                rep.DataSource = q12122;
                                rep.NhaCC.Value = _tenNCC;
                                rep.BindingData();
                                rep.CreateDocument();
                                frm2.prcIN.PrintingSystem = rep.PrintingSystem;
                                frm2.ShowDialog();
                                return;
                            }
                            else if (DungChung.Bien.MaBV == "30002")
                            {
                                string[] _arr = new string[] { "0", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@" };
                                int[] _arrWidth = new int[] { };
                                // int[] _arrWidth = new int[] { }; có thể gọi khi truyền kiểu fomatAutowith = true
                                int num = 1;
                                DungChung.Bien.MangHaiChieu = new Object[q.Count + 1, _arr.Length];
                                string[] _tieude = { "STT", "TenThuoc", "DVT", "DonGia", "Tồn ĐK - SL", "Tồn ĐK - TT", "Nhập TK - Sl", "Nhập TK - TT", "Xuất DV-SL", "Xuất DV-TT", "Xuất BH-SL", "Xuất BH-TT", "Xuất khác-SL", "Xuất khác-TT", "Tồn CK - SL", "Tồn CK - TT" };
                                for (int i = 0; i < _tieude.Length; i++)
                                {
                                    DungChung.Bien.MangHaiChieu[0, i] = _tieude[i];
                                }

                                //for (int i = 0; i <= 17; i++) {
                                //    DungChung.Bien.MangHaiChieu[0, i] = _arr[0,i];
                                //}
                                foreach (var r in q)
                                {
                                    DungChung.Bien.MangHaiChieu[num, 0] = num;
                                    DungChung.Bien.MangHaiChieu[num, 1] = r.TenHamLuong;
                                    DungChung.Bien.MangHaiChieu[num, 2] = r.DonVi;
                                    DungChung.Bien.MangHaiChieu[num, 3] = r.DonGia;
                                    DungChung.Bien.MangHaiChieu[num, 4] = r.TonDKSL;
                                    DungChung.Bien.MangHaiChieu[num, 5] = r.TonDKTT;
                                    DungChung.Bien.MangHaiChieu[num, 6] = r.NhapTKSL;
                                    DungChung.Bien.MangHaiChieu[num, 7] = r.NhapTKTT;
                                    DungChung.Bien.MangHaiChieu[num, 8] = r.XuatDVSL;
                                    DungChung.Bien.MangHaiChieu[num, 9] = r.XuatDVTT;
                                    DungChung.Bien.MangHaiChieu[num, 10] = r.XuatBHSL;
                                    DungChung.Bien.MangHaiChieu[num, 11] = r.XuatBHTT;
                                    DungChung.Bien.MangHaiChieu[num, 12] = r.XuatKhac30002SL;
                                    DungChung.Bien.MangHaiChieu[num, 13] = r.XuatKhac30002TT;
                                    DungChung.Bien.MangHaiChieu[num, 14] = r.TonCKSL;
                                    DungChung.Bien.MangHaiChieu[num, 15] = r.TonCKTT;

                                    num++;
                                }
                                BaoCao.repBcNXTrutgon_30002 rep = new BaoCao.repBcNXTrutgon_30002(chkHienthi.Checked, ckTrongNgoaiNuoc.Checked);
                                frmIn frm2 = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "Báo cáo NXT", "C:\\TsBCNXT_RG30002.xls", true, this.Name);
                                rep.TuNgay.Value = dateTuNgay.Text;
                                rep.DenNgay.Value = dateDenNgay.Text;
                                rep.Kho.Value = _tenkho;
                                rep.DataSource = q;
                                double test = (double)q.Sum(p => p.TonCKTT);
                                rep.NhaCC.Value = _tenNCC;
                                rep.BindingData();
                                rep.CreateDocument();
                                frm2.prcIN.PrintingSystem = rep.PrintingSystem;
                                frm2.ShowDialog();
                            }
                        }
                        #endregion
                        #region mẫu toàn viện
                        if (chk_mauToanVien.Checked)
                        {
                            int maKhoTong = lup_KhoTong.EditValue == null ? 0 : Convert.ToInt32(lup_KhoTong.EditValue);
                            List<int> maKhoXa = data.KPhongs.Where(p => p.TenKP.ToLower().Trim() == "kho xã").Select(p => p.MaKP).ToList();
                            var qnxt_kt = (from a in qnxt2
                                           join dv in dichvu on a.MaDV equals dv.MaDV

                                           join kp in _kpChon on a.MaKP equals kp.MaKP
                                           group a by new { dv.STT, dv.MaCC, dv.TenTN, dv.TenNhom, dv.TenDV, dv.DonVi, a.DonGia, a.MaDV, dv.MaTam } into kq
                                           select new
                                           {
                                               MaTam = kq.Key.MaTam,
                                               kq.Key.STT,
                                               kq.Key.TenTN,
                                               kq.Key.MaCC,
                                               MaDV = kq.Key.MaDV,
                                               TenNhomDuoc = kq.Key.TenNhom,
                                               TenHamLuong = kq.Key.TenDV,
                                               DonVi = kq.Key.DonVi,
                                               DonGia = kq.Key.DonGia,
                                               //SoLo=kq.Key.SoLo,

                                               TonDKSL = kq.Where(p => p.NgayNhap < tungay).Sum(p => p.SoLuongN) - kq.Where(p => p.NgayNhap < tungay).Sum(p => p.SoLuongX),
                                               TonDKTT = kq.Where(p => p.NgayNhap < tungay).Sum(p => p.ThanhTienN) - kq.Where(p => p.NgayNhap < tungay).Sum(p => p.ThanhTienX),
                                               //TonDKTTTong = kq.Where(p => p.NgayNhap < tungay).Sum(p => p.ThanhTienN)-kq.Where(p => p.NgayNhap < tungay).Sum(p => p.ThanhTienX),

                                               //nhập theo hóa đơn
                                               NhapTKSL = kq.Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Where(p => p.PLoai == 1 && p.KieuDon == 1).Where(p => p.MaKP == maKhoTong).Sum(p => p.SoLuongN),
                                               NhapTKTT = kq.Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Where(p => p.PLoai == 1 && p.KieuDon == 1).Where(p => p.MaKP == maKhoTong).Sum(p => p.ThanhTienN),

                                               //  nhập nội bộ(chuyển kho)
                                               NhapNB_SL = kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.PLoai == 1 && p.KieuDon == 0).Where(p => p.MaKP == maKhoTong).Sum(p => p.SoLuongN),
                                               NhapNB_TT = kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.PLoai == 1 && p.KieuDon == 0).Where(p => p.MaKP == maKhoTong).Sum(p => p.ThanhTienN),

                                               //  xuất nội bộ(chuyển kho về kho tổng)
                                               XuatNB_SL = kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.PLoai == 2 && p.KieuDon == 2 && maKhoXa.Contains(p.MaKPnx ?? 0)).Sum(p => p.SoLuongX),
                                               XuatNB_TT = kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.PLoai == 2 && p.KieuDon == 2 && maKhoXa.Contains(p.MaKPnx ?? 0)).Sum(p => p.ThanhTienX),

                                               XuatNoiTruSL = kq.Where(p => (p.KieuDon == 1 || p.KieuDon == 9) && p.PLoai == 2).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.SoLuongX) - kq.Where(p => p.KieuDon == 2 && p.PLoai == 1 && (p.TraDuoc_KieuDon == 1 || p.TraDuoc_KieuDon == 9)).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.SoLuongN),// liễu yêu cầu thêm kiểu đơn = xuất khác ngày 18052017
                                               xuatNoiTruTT = kq.Where(p => (p.KieuDon == 1 || p.KieuDon == 9) && p.PLoai == 2).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.ThanhTienX) - kq.Where(p => p.KieuDon == 2 && p.PLoai == 1 && (p.TraDuoc_KieuDon == 1 || p.TraDuoc_KieuDon == 9)).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.ThanhTienN),
                                               // xuatNoiTruTTTong = kq.Where(p => p.KieuDon == 0).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.ThanhTienX),

                                               XuatNgoaiTruSL = kq.Where(p => p.KieuDon == 0 && p.PLoai == 2).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.SoLuongX) - kq.Where(p => p.KieuDon == 2 && p.PLoai == 1 && p.TraDuoc_KieuDon == 0).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.SoLuongN),
                                               xuatNgoaiTruTT = kq.Where(p => p.KieuDon == 0 && p.PLoai == 2).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.ThanhTienX) - kq.Where(p => p.KieuDon == 2 && p.PLoai == 1 && p.TraDuoc_KieuDon == 0).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.SoLuongN),
                                               // xuatNgoaiTruTTTong = kq.Where(p => p.KieuDon == 1).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.ThanhTienX),
                                               //xuất cận lâm sàng
                                               XuatKhacSL = kq.Where(p => p.PLoai == 2 && (p.KieuDon == 5)).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.SoLuongX),// lieunt yêu cầu xuất CLS + Phòng khám
                                               xuatKhacTT = kq.Where(p => p.PLoai == 2 && (p.KieuDon == 5)).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.ThanhTienX),// lieunt yêu cầu

                                               XuatHuHaoSL = kq.Where(p => p.KieuDon != _cot1 && p.KieuDon != _cot2 && p.KieuDon != _cot3 && p.PLoai == 3).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.SoLuongX),
                                               XuatHuHaoTT = kq.Where(p => p.KieuDon != _cot1 && p.KieuDon != _cot2 && p.KieuDon != _cot3 && p.PLoai == 3).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.ThanhTienX),

                                               XuatTKTongSL = kq.Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.SoLuongX),
                                               xuatTKTongTT = kq.Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.ThanhTienX),
                                               //  xuatTKTongTTTong = kq.Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.ThanhTienX),

                                               TonCKSL = kq.Where(p => p.NgayNhap <= denngay).Sum(p => p.SoLuongN) - kq.Where(p => p.NgayNhap <= denngay).Sum(p => p.SoLuongX),
                                               TonCKTT = kq.Where(p => p.NgayNhap <= denngay).Sum(p => p.ThanhTienN) - kq.Where(p => p.NgayNhap <= denngay).Sum(p => p.ThanhTienX),
                                               // TonCKTTTong = kq.Where(p => p.nhapd.NgayNhap < denngay).Sum(p => p.nhapdct.ThanhTienN) - kq.Where(p => p.nhapd.NgayNhap < denngay).Sum(p => p.nhapdct.ThanhTienX)

                                           }).Where(p => p.TonDKSL != 0 || p.TonCKSL != 0 || p.NhapTKSL != 0 || p.XuatTKTongSL != 0).ToList().OrderBy(p => p.TenNhomDuoc).ThenBy(p => p.TenTN).ThenBy(p => p.TenHamLuong).ToList();
                            string[] _arr = new string[] { "@", "@", "@", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" };
                            int[] _arrWidth = new int[] { };
                            // int[] _arrWidth = new int[] { }; có thể gọi khi truyền kiểu fomatAutowith = true
                            int num = 1;
                            DungChung.Bien.MangHaiChieu = new Object[qnxt_kt.Count + 1, 20];
                            string[] _tieude = { "STT", "TenThuoc", "DVT", "DonGia", "SL tồn ĐK", "TT Tồn ĐK", "SL Nhập TK", "TT nhập TK", "SL Nhập CK", "TT nhập CK", "SL Xuất ngoại trú", "TT xuất ngoại trú", "SL Xuất Nội trú", "TT Xuất Nội trú", "SL Xuất CLS", "TT Xuất CLS", "SL Xuất kho xã", "TT Xuất kho xã", "Tồn CK - SL", "Tồn CK - TT" };
                            for (int i = 0; i < _tieude.Length; i++)
                            {
                                DungChung.Bien.MangHaiChieu[0, i] = _tieude[i];
                            }

                            //for (int i = 0; i <= 17; i++) {
                            //    DungChung.Bien.MangHaiChieu[0, i] = _arr[0,i];
                            //}
                            foreach (var r in qnxt_kt)
                            {
                                DungChung.Bien.MangHaiChieu[num, 0] = num;
                                DungChung.Bien.MangHaiChieu[num, 1] = r.TenHamLuong;
                                DungChung.Bien.MangHaiChieu[num, 2] = r.DonVi;
                                DungChung.Bien.MangHaiChieu[num, 3] = r.DonGia;
                                DungChung.Bien.MangHaiChieu[num, 4] = r.TonDKSL;
                                DungChung.Bien.MangHaiChieu[num, 5] = r.TonDKTT;
                                DungChung.Bien.MangHaiChieu[num, 6] = r.NhapTKSL;
                                DungChung.Bien.MangHaiChieu[num, 7] = r.NhapTKTT;
                                DungChung.Bien.MangHaiChieu[num, 8] = r.NhapNB_SL;
                                DungChung.Bien.MangHaiChieu[num, 9] = r.NhapNB_TT;
                                DungChung.Bien.MangHaiChieu[num, 10] = r.XuatNgoaiTruSL;
                                DungChung.Bien.MangHaiChieu[num, 11] = r.xuatNgoaiTruTT;
                                DungChung.Bien.MangHaiChieu[num, 12] = r.XuatNoiTruSL;
                                DungChung.Bien.MangHaiChieu[num, 13] = r.xuatNoiTruTT;
                                DungChung.Bien.MangHaiChieu[num, 14] = r.XuatKhacSL;
                                DungChung.Bien.MangHaiChieu[num, 15] = r.xuatKhacTT;
                                DungChung.Bien.MangHaiChieu[num, 16] = r.XuatNB_SL;
                                DungChung.Bien.MangHaiChieu[num, 17] = r.XuatNB_TT;
                                DungChung.Bien.MangHaiChieu[num, 18] = r.TonCKSL;
                                DungChung.Bien.MangHaiChieu[num, 19] = r.TonCKTT;

                                num++;
                            }
                            BaoCao.RepBcNXT_12001 rep = new BaoCao.RepBcNXT_12001(chkHienthi.Checked, ckTrongNgoaiNuoc.Checked);
                            frmIn frm2 = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "Báo cáo NXT", "C:\\TsBCNXT_ChuyenKho.xls", true, this.Name);
                            rep.TuNgay.Value = dateTuNgay.Text;
                            rep.DenNgay.Value = dateDenNgay.Text;
                            rep.DataSource = qnxt_kt;
                            rep.BindingData();
                            rep.CreateDocument();
                            frm2.prcIN.PrintingSystem = rep.PrintingSystem;
                            frm2.ShowDialog();
                            return;
                        }
                        #endregion
                        #region chọn nhiều kho
                        if (_kpChon.Count > 1 || radMauIn.SelectedIndex == 3)
                        {
                            string[] _arr = new string[] { "0", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", };
                            int[] _arrWidth = new int[] { };
                            // int[] _arrWidth = new int[] { }; có thể gọi khi truyền kiểu fomatAutowith = true
                            int num = 1;
                            DungChung.Bien.MangHaiChieu = new Object[q.Count + 1, 20];
                            string[] _tieude = { "STT", "TenThuoc", "DVT", "DonGia", "Tồn ĐK - SL", "Tồn ĐK - TT", "Nhập TK - Sl", "Nhập TK - TT", "Nhập CK TK - Sl", "Nhập CK TK - TT", "Xuất TK - SL", "Xuất TK - TT", "Xuất CK TK - SL", "Xuất CK TK - TT", "Tồn CK - SL", "Tồn CK - TT" };
                            for (int i = 0; i < _tieude.Length; i++)
                            {
                                DungChung.Bien.MangHaiChieu[0, i] = _tieude[i];
                            }

                            //for (int i = 0; i <= 17; i++) {
                            //    DungChung.Bien.MangHaiChieu[0, i] = _arr[0,i];
                            //}
                            foreach (var r in q)
                            {
                                DungChung.Bien.MangHaiChieu[num, 0] = num;
                                DungChung.Bien.MangHaiChieu[num, 1] = r.TenHamLuong;
                                if (DungChung.Bien.MaBV == "30004")
                                    DungChung.Bien.MangHaiChieu[num, 2] = r.DonVi;
                                DungChung.Bien.MangHaiChieu[num, 3] = r.DonGia;
                                DungChung.Bien.MangHaiChieu[num, 4] = r.TonDKSL;
                                DungChung.Bien.MangHaiChieu[num, 5] = r.TonDKTT;
                                DungChung.Bien.MangHaiChieu[num, 6] = r.NhapTKSL;
                                DungChung.Bien.MangHaiChieu[num, 7] = r.NhapTKTT;
                                DungChung.Bien.MangHaiChieu[num, 8] = r.NhapNB_SL;
                                DungChung.Bien.MangHaiChieu[num, 9] = r.NhapNB_TT;
                                DungChung.Bien.MangHaiChieu[num, 10] = r.XuatTKTongSL;
                                DungChung.Bien.MangHaiChieu[num, 11] = r.xuatTKTongTT;
                                DungChung.Bien.MangHaiChieu[num, 12] = r.XuatNB_SL;
                                DungChung.Bien.MangHaiChieu[num, 13] = r.XuatNB_TT;
                                DungChung.Bien.MangHaiChieu[num, 14] = r.TonCKSL;
                                DungChung.Bien.MangHaiChieu[num, 15] = r.TonCKTT;
                                DungChung.Bien.MangHaiChieu[num, 16] = r.TenHC;
                                DungChung.Bien.MangHaiChieu[num, 17] = r.HamLuong;
                                num++;
                            }
                            if (ckthuongtin.Checked)
                            {

                                BaoCao.repBcNXT_CK_01830 rep = new BaoCao.repBcNXT_CK_01830(chkHienthi.Checked, ckTrongNgoaiNuoc.Checked);
                                frmIn frm2 = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "Báo cáo NXT", "C:\\TsBCNXT_ChuyenKho.xls", true, this.Name);

                                rep.TuNgay.Value = dateTuNgay.Text;
                                rep.DenNgay.Value = dateDenNgay.Text;
                                rep.Kho.Value = _tenkho;
                                rep.DataSource = q;
                                rep.NhaCC.Value = _tenNCC;
                                rep.BindingData();
                                rep.CreateDocument();
                                frm2.prcIN.PrintingSystem = rep.PrintingSystem;
                                frm2.ShowDialog();
                                return;
                            }
                            else
                            {
                                BaoCao.repBcNXT_CK rep = new BaoCao.repBcNXT_CK(chkHienthi.Checked, ckTrongNgoaiNuoc.Checked);
                                frmIn frm2 = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "Báo cáo NXT", "C:\\TsBCNXT_ChuyenKho.xls", true, this.Name);

                                rep.TuNgay.Value = dateTuNgay.Text;
                                rep.DenNgay.Value = dateDenNgay.Text;
                                rep.Kho.Value = _tenkho;
                                rep.DataSource = q;
                                if (DungChung.Bien.MaBV == "24012")
                                {
                                    rep.DataSource = q24012;
                                }
                                rep.NhaCC.Value = _tenNCC;
                                rep.BindingData();
                                rep.CreateDocument();
                                frm2.prcIN.PrintingSystem = rep.PrintingSystem;
                                frm2.ShowDialog();
                                return;
                            }

                        }
                        #endregion
                        #region mẫu hư hao
                        if (radMauIn.SelectedIndex == 2)
                        {
                            string[] _arr = new string[] { "0", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@" };
                            int[] _arrWidth = new int[] { };
                            // int[] _arrWidth = new int[] { }; có thể gọi khi truyền kiểu fomatAutowith = true

                            DungChung.Bien.MangHaiChieu = new Object[q.Count + 1, 22];
                            int num = 1;
                            string[] _tieude = { "STT", "TenThuoc", "DVT", "DonGia", "Tồn ĐK - SL", "Tồn ĐK - TT", "Nhập TK - Sl", "Nhập TK - TT", "Nhập TL - SL", "Nhập TT - SL", _tenc1 + " - SL", _tenc1 + " - TT", _tenc2 + " - SL", _tenc2 + " - TT", "Xuất khác TK - SL", "Xuất khác TK - TT", "Xuất hư hao TK - SL", "Xuất hư hao TK - TT", "Tổng xuất TK - SL", "Tổng xuất TK - TT", "Tồn CK - SL", "Tồn CK - TT" };
                            for (int i = 0; i < _tieude.Length; i++)
                            {
                                DungChung.Bien.MangHaiChieu[0, i] = _tieude[i];
                            }


                            foreach (var r in q)
                            {
                                DungChung.Bien.MangHaiChieu[num, 0] = num;
                                DungChung.Bien.MangHaiChieu[num, 1] = r.TenHamLuong;
                                DungChung.Bien.MangHaiChieu[num, 2] = r.DonVi;
                                DungChung.Bien.MangHaiChieu[num, 3] = r.DonGia;
                                DungChung.Bien.MangHaiChieu[num, 4] = r.TonDKSL;
                                DungChung.Bien.MangHaiChieu[num, 5] = r.TonDKTT;
                                DungChung.Bien.MangHaiChieu[num, 6] = r.NhapTKSL;
                                DungChung.Bien.MangHaiChieu[num, 7] = r.NhapTKTT;
                                DungChung.Bien.MangHaiChieu[num, 8] = r.NhapTra_SL;
                                DungChung.Bien.MangHaiChieu[num, 9] = r.NhapTra_TT;
                                DungChung.Bien.MangHaiChieu[num, 10] = r.XuatNoiTruSL;
                                DungChung.Bien.MangHaiChieu[num, 11] = r.xuatNoiTruTT;
                                DungChung.Bien.MangHaiChieu[num, 12] = r.XuatNgoaiTruSL;
                                DungChung.Bien.MangHaiChieu[num, 13] = r.xuatNgoaiTruTT;
                                DungChung.Bien.MangHaiChieu[num, 14] = r.XuatKhacSL;
                                DungChung.Bien.MangHaiChieu[num, 15] = r.xuatKhacTT;
                                DungChung.Bien.MangHaiChieu[num, 16] = r.XuatHuHaoSL;
                                DungChung.Bien.MangHaiChieu[num, 17] = r.XuatHuHaoTT;
                                DungChung.Bien.MangHaiChieu[num, 18] = r.XuatTKTongSL1;
                                DungChung.Bien.MangHaiChieu[num, 19] = r.xuatTKTongTT1;
                                DungChung.Bien.MangHaiChieu[num, 20] = r.TonCKSL;
                                DungChung.Bien.MangHaiChieu[num, 21] = r.TonCKTT;
                                //if (DungChung.Bien.MaBV == "30005")
                                //{
                                //    DungChung.Bien.MangHaiChieu[num, 8] = r.XuatNoiTruSL1;
                                //    DungChung.Bien.MangHaiChieu[num, 9] = r.XuatNoiTruTT1;
                                //    DungChung.Bien.MangHaiChieu[num, 10] = r.XuatNgoaiTruSL1;
                                //    DungChung.Bien.MangHaiChieu[num, 11] = r.XuatNgoaiTruTT1;
                                //    DungChung.Bien.MangHaiChieu[num, 12] = r.XuatKhacSL1;
                                //    DungChung.Bien.MangHaiChieu[num, 13] = r.xuatKhacTT1;
                                //}// bỏ trừ nhập trả lại
                                num++;
                            }
                            if (ckthuongtin.Checked)
                            {

                                BaoCao.RepBcNXT_HH_01830 rep = new BaoCao.RepBcNXT_HH_01830(chkHienthi.Checked, ckTrongNgoaiNuoc.Checked);
                                frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "Báo cáo NXT_HH", "C:\\TsBCNXT.xls", true, this.Name);

                                rep.TuNgay.Value = dateTuNgay.Text;
                                rep.DenNgay.Value = dateDenNgay.Text;
                                rep.Kho.Value = _tenkho;
                                rep._tenc1.Value = _tenc1;
                                rep._tenc2.Value = _tenc2;
                                rep.DataSource = q;
                                rep.NhaCC.Value = _tenNCC;
                                rep.BindingData();
                                rep.CreateDocument();
                                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                frm.ShowDialog();
                            }
                            else if (DungChung.Bien.MaBV == "30005")
                            {
                                BaoCao.RepBcNXT_HH_30005 rep = new BaoCao.RepBcNXT_HH_30005(chkHienthi.Checked, ckTrongNgoaiNuoc.Checked);
                                frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "Báo cáo NXT_HH", "C:\\TsBCNXT.xls", true, this.Name);
                                rep.TuNgay.Value = dateTuNgay.Text;
                                rep.DenNgay.Value = dateDenNgay.Text;
                                rep.Kho.Value = _tenkho;
                                rep._tenc1.Value = _tenc1;
                                rep._tenc2.Value = _tenc2;
                                rep.DataSource = q;
                                rep.NhaCC.Value = _tenNCC;
                                rep.BindingData();
                                rep.CreateDocument();
                                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                frm.ShowDialog();
                            }
                            else
                            {
                                BaoCao.RepBcNXT_HH rep = new BaoCao.RepBcNXT_HH(chkHienthi.Checked, ckTrongNgoaiNuoc.Checked);
                                frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "Báo cáo NXT_HH", "C:\\TsBCNXT.xls", true, this.Name);
                                rep.TuNgay.Value = dateTuNgay.Text;
                                rep.DenNgay.Value = dateDenNgay.Text;
                                rep.Kho.Value = _tenkho;
                                rep._tenc1.Value = _tenc1;
                                rep._tenc2.Value = _tenc2;
                                rep.DataSource = q;
                                if (DungChung.Bien.MaBV == "24012")
                                {
                                    rep.DataSource = q24012;
                                }
                                rep.NhaCC.Value = _tenNCC;
                                rep.BindingData();
                                rep.CreateDocument();
                                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                frm.ShowDialog();
                            }
                        }
                        #endregion
                        #region mẫu chi tiết 30004
                        else if (radMauIn.SelectedIndex == 1 && DungChung.Bien.MaBV == "30004" && check30004.Checked == true)
                        {

                            string[] _arr = new string[] { "0", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@" };
                            int[] _arrWidth = new int[] { };
                            // int[] _arrWidth = new int[] { }; có thể gọi khi truyền kiểu fomatAutowith = true
                            int num = 1;
                            DungChung.Bien.MangHaiChieu = new Object[q.Count + 1, 25];
                            aa = 0;
                            string[] _tieude = { "STT", "TenThuoc", "DVT", "DonGia", "Tồn ĐK - SL", "Tồn ĐK - TT", "Nhập HD - Sl", "Nhập HD - TT", "Nhập TL - Sl", "Nhập TL - TT", _tenc1 + " - SL", _tenc1 + " - TT", _tenc2 + " - SL", _tenc2 + " - TT", _tenc3 + " - SL", _tenc3 + " - TT", _tenc4 + " - SL", _tenc4 + " - TT", "Xuất khác TK - SL", "Xuất khác TK - TT", "Tổng xuất TK - SL", "Tổng xuất TK - TT", "Tồn CK - SL", "Tồn CK - TT" };
                            for (int i = 0; i < _tieude.Length; i++)
                            {
                                DungChung.Bien.MangHaiChieu[0, i] = _tieude[i];
                            }

                            //for (int i = 0; i <= 17; i++) {
                            //    DungChung.Bien.MangHaiChieu[0, i] = _arr[0,i];
                            //}
                            foreach (var r in q)
                            {
                                DungChung.Bien.MangHaiChieu[num, 0] = num;
                                DungChung.Bien.MangHaiChieu[num, 1] = r.MaDV;
                                DungChung.Bien.MangHaiChieu[num, 2] = r.TenHamLuong;
                                DungChung.Bien.MangHaiChieu[num, 3] = r.DonVi;
                                DungChung.Bien.MangHaiChieu[num, 4] = r.DonGia;
                                DungChung.Bien.MangHaiChieu[num, 5] = r.TonDKSL;
                                DungChung.Bien.MangHaiChieu[num, 6] = r.TonDKTT;
                                DungChung.Bien.MangHaiChieu[num, 7] = r.NhapHD_SL;
                                DungChung.Bien.MangHaiChieu[num, 8] = r.NhapHD_TT;
                                DungChung.Bien.MangHaiChieu[num, 9] = r.NhapTra_SL;
                                DungChung.Bien.MangHaiChieu[num, 10] = r.NhapTra_TT;
                                DungChung.Bien.MangHaiChieu[num, 11] = r.XuatNoiTruSL;
                                DungChung.Bien.MangHaiChieu[num, 12] = r.xuatNoiTruTT;
                                DungChung.Bien.MangHaiChieu[num, 13] = r.XuatNgoaiTruSL;
                                DungChung.Bien.MangHaiChieu[num, 14] = r.xuatNgoaiTruTT;
                                DungChung.Bien.MangHaiChieu[num, 15] = r.XuatC3SL;
                                DungChung.Bien.MangHaiChieu[num, 16] = r.xuatC3TT;
                                DungChung.Bien.MangHaiChieu[num, 17] = r.XuatC4SL;
                                DungChung.Bien.MangHaiChieu[num, 18] = r.xuatC4TT;
                                DungChung.Bien.MangHaiChieu[num, 19] = r.XuatKhacSL2;
                                DungChung.Bien.MangHaiChieu[num, 20] = r.xuatKhacTT2;
                                DungChung.Bien.MangHaiChieu[num, 21] = r.XuatTKTongSL;
                                DungChung.Bien.MangHaiChieu[num, 22] = r.xuatTKTongTT;
                                DungChung.Bien.MangHaiChieu[num, 23] = r.TonCKSL;
                                DungChung.Bien.MangHaiChieu[num, 24] = r.TonCKTT;
                                num++;
                            }


                            bool _nhieukho = false;
                            if (_kpChon.Count > 1)
                                _nhieukho = true;
                            BaoCao.RepBcNXT_30004 rep = new BaoCao.RepBcNXT_30004(chkHienthi.Checked, ckTrongNgoaiNuoc.Checked, aa);
                            frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "Báo cáo NXT", "C:\\TsBCNXT.xls", true, this.Name);
                            rep.TuNgay.Value = dateTuNgay.Text;
                            rep.DenNgay.Value = dateDenNgay.Text;
                            rep.Kho.Value = _tenkho;
                            rep._tenc1.Value = _tenc1;
                            rep._tenc2.Value = _tenc2;
                            rep._tenc3.Value = _tenc3;
                            rep._tenc4.Value = _tenc4;
                            //rep.nhap.Value = "Nhập trả lại";
                            rep.DataSource = q;
                            rep.NhaCC.Value = _tenNCC;
                            rep.BindingData();
                            rep.CreateDocument();
                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                            frm.ShowDialog();
                        }
                        #endregion
                        #region mẫu chi tiết 26007

                        else if (radMauIn.SelectedIndex == 1 && mau26007.SelectedIndex == 1)
                        {

                            string[] _arr = new string[] { "0", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@" };
                            int[] _arrWidth = new int[] { };
                            // int[] _arrWidth = new int[] { }; có thể gọi khi truyền kiểu fomatAutowith = true
                            int num = 1;
                            DungChung.Bien.MangHaiChieu = new Object[q.Count + 1, 21];
                            #region Nhập chuyển kho
                            if (kho26007.SelectedIndex == 1)
                            {
                                aa = 1;
                                string[] _tieude = { "STT", "TenThuoc", "DVT", "DonGia", "Tồn ĐK - SL", "Tồn ĐK - TT", "Nhập HD - Sl", "Nhập HD - TT", "Nhập CK - Sl", "Nhập CK - TT", _tenc1 + " - SL", _tenc1 + " - TT", _tenc2 + " - SL", _tenc2 + " - TT", "Xuất khác TK - SL", "Xuất khác TK - TT", "Tổng xuất TK - SL", "Tổng xuất TK - TT", "Tồn CK - SL", "Tồn CK - TT" };
                                for (int i = 0; i < _tieude.Length; i++)
                                {
                                    DungChung.Bien.MangHaiChieu[0, i] = _tieude[i];
                                }

                                //for (int i = 0; i <= 17; i++) {
                                //    DungChung.Bien.MangHaiChieu[0, i] = _arr[0,i];
                                //}
                                foreach (var r in q)
                                {
                                    DungChung.Bien.MangHaiChieu[num, 0] = num;
                                    DungChung.Bien.MangHaiChieu[num, 1] = r.MaDV;
                                    DungChung.Bien.MangHaiChieu[num, 2] = r.TenHamLuong;
                                    DungChung.Bien.MangHaiChieu[num, 3] = r.DonVi;
                                    DungChung.Bien.MangHaiChieu[num, 4] = r.DonGia;
                                    DungChung.Bien.MangHaiChieu[num, 5] = r.TonDKSL;
                                    DungChung.Bien.MangHaiChieu[num, 6] = r.TonDKTT;
                                    DungChung.Bien.MangHaiChieu[num, 7] = r.NhapHD_SL;
                                    DungChung.Bien.MangHaiChieu[num, 8] = r.NhapHD_TT;
                                    DungChung.Bien.MangHaiChieu[num, 9] = r.NhapCK_SL;
                                    DungChung.Bien.MangHaiChieu[num, 10] = r.NhapCK_TT;
                                    DungChung.Bien.MangHaiChieu[num, 11] = r.XuatNoiTruSL;
                                    DungChung.Bien.MangHaiChieu[num, 12] = r.xuatNoiTruTT;
                                    DungChung.Bien.MangHaiChieu[num, 13] = r.XuatNgoaiTruSL;
                                    DungChung.Bien.MangHaiChieu[num, 14] = r.xuatNgoaiTruTT;
                                    DungChung.Bien.MangHaiChieu[num, 15] = r.XuatKhacSL;
                                    DungChung.Bien.MangHaiChieu[num, 16] = r.xuatKhacTT;
                                    DungChung.Bien.MangHaiChieu[num, 17] = r.XuatTKTongSL;
                                    DungChung.Bien.MangHaiChieu[num, 18] = r.xuatTKTongTT;
                                    DungChung.Bien.MangHaiChieu[num, 19] = r.TonCKSL;
                                    DungChung.Bien.MangHaiChieu[num, 20] = r.TonCKTT;
                                    num++;
                                }


                                bool _nhieukho = false;
                                if (_kpChon.Count > 1)
                                    _nhieukho = true;
                                BaoCao.RepBcNXT rep = new BaoCao.RepBcNXT(chkHienthi.Checked, ckTrongNgoaiNuoc.Checked, aa);
                                if (DungChung.Bien.MaBV == "24297" && ckcGiaNhap.Checked)
                                {
                                    rep.xrLabel11.Text = "Đơn giá nhập";
                                }
                                frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "Báo cáo NXT", "C:\\TsBCNXT.xls", true, this.Name);
                                rep.TuNgay.Value = dateTuNgay.Text;
                                rep.DenNgay.Value = dateDenNgay.Text;
                                rep.Kho.Value = _tenkho;
                                rep._tenc1.Value = _tenc1;
                                rep._tenc2.Value = _tenc2;
                                //rep._tenc3.Value = _tenc3;
                                //rep.nhap.Value = "Nhập chuyển kho";
                                rep.DataSource = q;
                                rep.NhaCC.Value = _tenNCC;
                                rep.BindingData();
                                rep.CreateDocument();
                                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                frm.ShowDialog();
                            }
                            #endregion
                            #region Nhập trả lại
                            if (kho26007.SelectedIndex == 0)
                            {
                                aa = 0;
                                string[] _tieude = { "STT", "TenThuoc", "DVT", "DonGia", "Tồn ĐK - SL", "Tồn ĐK - TT", "Nhập HD - Sl", "Nhập HD - TT", "Nhập TL - Sl", "Nhập TL - TT", _tenc1 + " - SL", _tenc1 + " - TT", _tenc2 + " - SL", _tenc2 + " - TT", "Xuất khác TK - SL", "Xuất khác TK - TT", "Tổng xuất TK - SL", "Tổng xuất TK - TT", "Tồn CK - SL", "Tồn CK - TT" };
                                for (int i = 0; i < _tieude.Length; i++)
                                {
                                    DungChung.Bien.MangHaiChieu[0, i] = _tieude[i];
                                }

                                //for (int i = 0; i <= 17; i++) {
                                //    DungChung.Bien.MangHaiChieu[0, i] = _arr[0,i];
                                //}
                                foreach (var r in q)
                                {
                                    DungChung.Bien.MangHaiChieu[num, 0] = num;
                                    DungChung.Bien.MangHaiChieu[num, 1] = r.MaDV;
                                    DungChung.Bien.MangHaiChieu[num, 2] = r.TenHamLuong;
                                    DungChung.Bien.MangHaiChieu[num, 3] = r.DonVi;
                                    DungChung.Bien.MangHaiChieu[num, 4] = r.DonGia;
                                    DungChung.Bien.MangHaiChieu[num, 5] = r.TonDKSL;
                                    DungChung.Bien.MangHaiChieu[num, 6] = r.TonDKTT;
                                    DungChung.Bien.MangHaiChieu[num, 7] = r.NhapHD_SL;
                                    DungChung.Bien.MangHaiChieu[num, 8] = r.NhapHD_TT;
                                    DungChung.Bien.MangHaiChieu[num, 9] = r.NhapTra_SL;
                                    DungChung.Bien.MangHaiChieu[num, 10] = r.NhapTra_TT;
                                    DungChung.Bien.MangHaiChieu[num, 11] = r.XuatNoiTruSL;
                                    DungChung.Bien.MangHaiChieu[num, 12] = r.xuatNoiTruTT;
                                    DungChung.Bien.MangHaiChieu[num, 13] = r.XuatNgoaiTruSL;
                                    DungChung.Bien.MangHaiChieu[num, 14] = r.xuatNgoaiTruTT;
                                    DungChung.Bien.MangHaiChieu[num, 15] = r.XuatKhacSL;
                                    DungChung.Bien.MangHaiChieu[num, 16] = r.xuatKhacTT;
                                    DungChung.Bien.MangHaiChieu[num, 17] = r.XuatTKTongSL;
                                    DungChung.Bien.MangHaiChieu[num, 18] = r.xuatTKTongTT;
                                    DungChung.Bien.MangHaiChieu[num, 19] = r.TonCKSL;
                                    DungChung.Bien.MangHaiChieu[num, 20] = r.TonCKTT;
                                    num++;
                                }


                                bool _nhieukho = false;
                                if (_kpChon.Count > 1)
                                    _nhieukho = true;
                                BaoCao.RepBcNXT rep = new BaoCao.RepBcNXT(chkHienthi.Checked, ckTrongNgoaiNuoc.Checked, aa);
                                frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "Báo cáo NXT", "C:\\TsBCNXT.xls", true, this.Name);
                                rep.TuNgay.Value = dateTuNgay.Text;
                                rep.DenNgay.Value = dateDenNgay.Text;
                                rep.Kho.Value = _tenkho;
                                rep._tenc1.Value = _tenc1;
                                rep._tenc2.Value = _tenc2;
                                //rep._tenc3.Value = _tenc3;
                                //rep.nhap.Value = "Nhập trả lại";
                                rep.DataSource = q;
                                rep.NhaCC.Value = _tenNCC;
                                rep.BindingData();
                                rep.CreateDocument();
                                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                frm.ShowDialog();
                            }
                            #endregion
                        }
                        else if (radMauIn.SelectedIndex == 1 && ckHienThiMaDV.Checked == false)
                        {
                            if (DungChung.Bien.MaBV == "30002")
                            {
                                string[] _arr = new string[] { "0", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@" };
                                int[] _arrWidth = new int[] { };
                                // int[] _arrWidth = new int[] { }; có thể gọi khi truyền kiểu fomatAutowith = true
                                int num = 1;
                                DungChung.Bien.MangHaiChieu = new Object[q.Count + 1, 21];
                                string[] _tieude = { "STT", "TenThuoc", "DVT", "DonGia", "GiaNhap", "Tồn ĐK - SL", "Tồn ĐK - TT", "Nhập HD - Sl", "Nhập HD - TT", "Nhập Khác - Sl", "Nhập Khác - TT", _tenc1 + " - SL", _tenc1 + " - TT", _tenc2 + " - SL", _tenc2 + " - TT", "Xuất khác TK - SL", "Xuất khác TK - TT", "Tổng xuất TK - SL", "Tổng xuất TK - TT", "Tồn CK - SL", "Tồn CK - TT" };
                                for (int i = 0; i < _tieude.Length; i++)
                                {
                                    DungChung.Bien.MangHaiChieu[0, i] = _tieude[i];
                                }

                                //for (int i = 0; i <= 17; i++) {
                                //    DungChung.Bien.MangHaiChieu[0, i] = _arr[0,i];
                                //}
                                foreach (var r in q)
                                {
                                    if (DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297")
                                    {
                                        DungChung.Bien.MangHaiChieu[num, 0] = num;
                                        DungChung.Bien.MangHaiChieu[num, 1] = r.MaDV;
                                        DungChung.Bien.MangHaiChieu[num, 2] = r.TenHamLuong;
                                        DungChung.Bien.MangHaiChieu[num, 3] = r.DonVi;
                                        DungChung.Bien.MangHaiChieu[num, 4] = r.DonGia;
                                        DungChung.Bien.MangHaiChieu[num, 5] = r.GiaNhap;
                                        DungChung.Bien.MangHaiChieu[num, 6] = r.TonDKSL;
                                        DungChung.Bien.MangHaiChieu[num, 7] = r.TonDKTT;
                                        DungChung.Bien.MangHaiChieu[num, 8] = r.NhapHD_SL;
                                        DungChung.Bien.MangHaiChieu[num, 9] = r.NhapHD_TT;
                                        DungChung.Bien.MangHaiChieu[num, 10] = r.NhapKhac_SL;
                                        DungChung.Bien.MangHaiChieu[num, 11] = r.NhapKhac_TT;
                                        DungChung.Bien.MangHaiChieu[num, 12] = r.XuatNoiTruSL;
                                        DungChung.Bien.MangHaiChieu[num, 13] = r.xuatNoiTruTT;
                                        DungChung.Bien.MangHaiChieu[num, 14] = r.XuatNgoaiTruSL;
                                        DungChung.Bien.MangHaiChieu[num, 15] = r.xuatNgoaiTruTT;
                                        DungChung.Bien.MangHaiChieu[num, 16] = r.XuatKhacSL;
                                        DungChung.Bien.MangHaiChieu[num, 17] = r.xuatKhacTT;
                                        DungChung.Bien.MangHaiChieu[num, 18] = r.XuatTKTongSL;
                                        DungChung.Bien.MangHaiChieu[num, 19] = r.xuatTKTongTT;
                                        DungChung.Bien.MangHaiChieu[num, 20] = r.TonCKSL;
                                        DungChung.Bien.MangHaiChieu[num, 21] = r.TonCKTT;
                                        num++;
                                    }
                                    else
                                    {
                                        DungChung.Bien.MangHaiChieu[num, 0] = num;
                                        DungChung.Bien.MangHaiChieu[num, 1] = r.MaDV;
                                        DungChung.Bien.MangHaiChieu[num, 2] = r.TenHamLuong;
                                        DungChung.Bien.MangHaiChieu[num, 3] = r.DonVi;
                                        DungChung.Bien.MangHaiChieu[num, 4] = r.DonGia;
                                        DungChung.Bien.MangHaiChieu[num, 6] = r.TonDKSL;
                                        DungChung.Bien.MangHaiChieu[num, 7] = r.TonDKTT;
                                        DungChung.Bien.MangHaiChieu[num, 8] = r.NhapHD_SL;
                                        DungChung.Bien.MangHaiChieu[num, 9] = r.NhapHD_TT;
                                        DungChung.Bien.MangHaiChieu[num, 10] = r.NhapKhac_SL;
                                        DungChung.Bien.MangHaiChieu[num, 11] = r.NhapKhac_TT;
                                        DungChung.Bien.MangHaiChieu[num, 12] = r.XuatNoiTruSL;
                                        DungChung.Bien.MangHaiChieu[num, 13] = r.xuatNoiTruTT;
                                        DungChung.Bien.MangHaiChieu[num, 14] = r.XuatNgoaiTruSL;
                                        DungChung.Bien.MangHaiChieu[num, 15] = r.xuatNgoaiTruTT;
                                        DungChung.Bien.MangHaiChieu[num, 16] = r.XuatKhacSL;
                                        DungChung.Bien.MangHaiChieu[num, 17] = r.xuatKhacTT;
                                        DungChung.Bien.MangHaiChieu[num, 18] = r.XuatTKTongSL;
                                        DungChung.Bien.MangHaiChieu[num, 19] = r.xuatTKTongTT;
                                        DungChung.Bien.MangHaiChieu[num, 20] = r.TonCKSL;
                                        DungChung.Bien.MangHaiChieu[num, 21] = r.TonCKTT;
                                      
                                        num++;
                                    }
                                }


                                bool _nhieukho = false;
                                if (_kpChon.Count > 1)
                                    _nhieukho = true;
                                BaoCao.RepBcNXT_30002 rep = new BaoCao.RepBcNXT_30002(chkHienthi.Checked, ckTrongNgoaiNuoc.Checked, aa);
                                frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "Báo cáo NXT", "C:\\TsBCNXT.xls", true, this.Name);
                                rep.TuNgay.Value = dateTuNgay.Text;
                                rep.DenNgay.Value = dateDenNgay.Text;
                                rep.Kho.Value = _tenkho;
                                rep._tenc1.Value = _tenc1;
                                rep._tenc2.Value = _tenc2;
                                rep.nhap.Value = "Nhập khác";
                                //rep._tenc3.Value = _tenc3;
                                //rep.nhap.Value = "Nhập trả lại";
                                rep.DataSource = q;
                                rep.NhaCC.Value = _tenNCC;
                                rep.BindingData();
                                rep.CreateDocument();
                                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                frm.ShowDialog();
                            }
                            else
                            {
                                string[] _arr = new string[] { "0", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@" };
                                int[] _arrWidth = new int[] { };
                                // int[] _arrWidth = new int[] { }; có thể gọi khi truyền kiểu fomatAutowith = true
                                int num = 1;
                                DungChung.Bien.MangHaiChieu = new Object[q.Count + 1, 20];
                                string[] _tieude = { "STT", "TenThuoc", "DVT", "DonGia", "Tồn ĐK - SL", "Tồn ĐK - TT", "Nhập TK - Sl", "Nhập TK - TT", _tenc1 + " - SL", _tenc1 + " - TT", _tenc2 + " - SL", _tenc2 + " - TT", "Xuất khác TK - SL", "Xuất khác TK - TT", "Tổng xuất TK - SL", "Tổng xuất TK - TT", "Tồn CK - SL", "Tồn CK - TT" };
                                for (int i = 0; i < _tieude.Length; i++)
                                {
                                    DungChung.Bien.MangHaiChieu[0, i] = _tieude[i];
                                }

                                //for (int i = 0; i <= 17; i++) {
                                //    DungChung.Bien.MangHaiChieu[0, i] = _arr[0,i];
                                //}
                                foreach (var r in q)
                                {
                                    DungChung.Bien.MangHaiChieu[num, 0] = num;
                                    DungChung.Bien.MangHaiChieu[num, 1] = r.TenHamLuong;
                                    DungChung.Bien.MangHaiChieu[num, 2] = r.DonVi;
                                    DungChung.Bien.MangHaiChieu[num, 3] = r.DonGia;
                                    DungChung.Bien.MangHaiChieu[num, 4] = r.TonDKSL;
                                    DungChung.Bien.MangHaiChieu[num, 5] = r.TonDKTT;
                                    DungChung.Bien.MangHaiChieu[num, 6] = r.NhapTKSL;
                                    DungChung.Bien.MangHaiChieu[num, 7] = r.NhapTKTT;
                                    DungChung.Bien.MangHaiChieu[num, 8] = r.XuatNoiTruSL;
                                    DungChung.Bien.MangHaiChieu[num, 9] = r.xuatNoiTruTT;
                                    DungChung.Bien.MangHaiChieu[num, 10] = r.XuatNgoaiTruSL;
                                    DungChung.Bien.MangHaiChieu[num, 11] = r.xuatNgoaiTruTT;
                                    DungChung.Bien.MangHaiChieu[num, 12] = r.XuatKhacSL;
                                    DungChung.Bien.MangHaiChieu[num, 13] = r.xuatKhacTT;
                                    DungChung.Bien.MangHaiChieu[num, 14] = r.XuatTKTongSL;
                                    DungChung.Bien.MangHaiChieu[num, 15] = r.xuatTKTongTT;
                                    DungChung.Bien.MangHaiChieu[num, 16] = r.TonCKSL;
                                    DungChung.Bien.MangHaiChieu[num, 17] = r.TonCKTT; 
                                    
                                    num++;
                                }
                                bool _nhieukho = false;
                                if (_kpChon.Count > 1)
                                    _nhieukho = true;
                                         
                                BaoCao.RepBcNXT_01830 rep = new BaoCao.RepBcNXT_01830(chkHienthi.Checked, ckTrongNgoaiNuoc.Checked);
                                if(DungChung.Bien.MaBV == "24297" && ckcGiaNhap.Checked)
                                {
                                    rep.xrLabel11.Text = "Đơn giá nhập";
                                }
                                else if (DungChung.Bien.MaBV == "27023" && cklKP.SelectedValue.ToString() == "98")
                                {
                                    rep.xrTableCell1.Text = "TP. TCHC-CTXH";
                                    rep.colKhoaDuoc.Text = "Nguyễn Xuân Sanh";
                                }
                                frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "Báo cáo NXT", "C:\\TsBCNXT.xls", true, this.Name);
                                rep.TuNgay.Value = dateTuNgay.Text;
                                rep.DenNgay.Value = dateDenNgay.Text;
                                rep.Kho.Value = _tenkho;
                                rep._tenc1.Value = _tenc1;
                                rep._tenc2.Value = _tenc2;
                                rep.DataSource = q;
                                if (DungChung.Bien.MaBV == "24012")
                                {
                                    rep.DataSource = q24012;
                                }
                                rep.Kho.Value = _tenkho;
                                rep.NhaCC.Value = _tenNCC;
                                rep.BindingData();
                                rep.CreateDocument();
                                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                frm.ShowDialog();
                            }
                        }
                        #endregion
                        #region mẫu 4 cột tt22

                        else if (radMauIn.SelectedIndex == 1 && ckHienThiMaDV.Checked == true)
                        {
                            string[] _arr = new string[] { "0", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@" };
                            int[] _arrWidth = new int[] { };
                            // int[] _arrWidth = new int[] { }; có thể gọi khi truyền kiểu fomatAutowith = true
                            int num = 1;
                            DungChung.Bien.MangHaiChieu = new Object[q.Count + 1, 20];
                            string[] _tieude = { "STT", "MaDV", "TenThuoc", "DVT", "DonGia", "Tồn ĐK - SL", "Tồn ĐK - TT", "Nhập TK - Sl", "Nhập TK - TT", _tenc1 + " - SL", _tenc1 + " - TT", _tenc2 + " - SL", _tenc2 + " - TT", "Xuất khác TK - SL", "Xuất khác TK - TT", "Tổng xuất TK - SL", "Tổng xuất TK - TT", "Tồn CK - SL", "Tồn CK - TT" };
                            for (int i = 0; i < _tieude.Length; i++)
                            {
                                DungChung.Bien.MangHaiChieu[0, i] = _tieude[i];
                            }

                            //for (int i = 0; i <= 17; i++) {
                            //    DungChung.Bien.MangHaiChieu[0, i] = _arr[0,i];
                            //}
                            foreach (var r in q)
                            {
                                DungChung.Bien.MangHaiChieu[num, 0] = num;
                                DungChung.Bien.MangHaiChieu[num, 1] = r.MaDV;
                                DungChung.Bien.MangHaiChieu[num, 2] = r.TenHamLuong;
                                DungChung.Bien.MangHaiChieu[num, 3] = r.DonVi;
                                DungChung.Bien.MangHaiChieu[num, 4] = r.DonGia;
                                DungChung.Bien.MangHaiChieu[num, 5] = r.TonDKSL;
                                DungChung.Bien.MangHaiChieu[num, 6] = r.TonDKTT;
                                DungChung.Bien.MangHaiChieu[num, 7] = r.NhapTKSL;
                                DungChung.Bien.MangHaiChieu[num, 8] = r.NhapTKTT;
                                DungChung.Bien.MangHaiChieu[num, 9] = r.XuatNoiTruSL;
                                DungChung.Bien.MangHaiChieu[num, 10] = r.xuatNoiTruTT;
                                DungChung.Bien.MangHaiChieu[num, 11] = r.XuatNgoaiTruSL;
                                DungChung.Bien.MangHaiChieu[num, 12] = r.xuatNgoaiTruTT;
                                DungChung.Bien.MangHaiChieu[num, 13] = r.XuatKhacSL;
                                DungChung.Bien.MangHaiChieu[num, 14] = r.xuatKhacTT;
                                DungChung.Bien.MangHaiChieu[num, 15] = r.XuatTKTongSL;
                                DungChung.Bien.MangHaiChieu[num, 16] = r.xuatTKTongTT;
                                DungChung.Bien.MangHaiChieu[num, 17] = r.TonCKSL;
                                DungChung.Bien.MangHaiChieu[num, 18] = r.TonCKTT;
                                
                                num++;
                            }


                            bool _nhieukho = false;
                            if (_kpChon.Count > 1)
                                _nhieukho = true;
                            BaoCao.RepBcNXT_MaDV rep = new BaoCao.RepBcNXT_MaDV(chkHienthi.Checked, ckTrongNgoaiNuoc.Checked);
                            frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "Báo cáo NXT", "C:\\TsBCNXT.xls", true, this.Name);
                            rep.TuNgay.Value = dateTuNgay.Text;
                            rep.DenNgay.Value = dateDenNgay.Text;
                            rep.Kho.Value = _tenkho;
                            rep._tenc1.Value = _tenc1;
                            rep._tenc2.Value = _tenc2;
                            rep.DataSource = q;
                            rep.Kho.Value = _tenkho;
                            rep.NhaCC.Value = _tenNCC;
                            rep.BindingData();
                            rep.CreateDocument();
                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                            frm.ShowDialog();
                        }
                        else if (radMauIn.SelectedIndex == 1 && ckHienThiMaDV.Checked == false)
                        {
                            string[] _arr = new string[] { "0", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@" };
                            int[] _arrWidth = new int[] { };
                            // int[] _arrWidth = new int[] { }; có thể gọi khi truyền kiểu fomatAutowith = true
                            int num = 1;
                            DungChung.Bien.MangHaiChieu = new Object[q.Count + 1, 20];
                            string[] _tieude = { "STT", "TenThuoc", "DVT", "DonGia", "Tồn ĐK - SL", "Tồn ĐK - TT", "Nhập TK - Sl", "Nhập TK - TT", _tenc1 + " - SL", _tenc1 + " - TT", _tenc2 + " - SL", _tenc2 + " - TT", "Xuất khác TK - SL", "Xuất khác TK - TT", "Tổng xuất TK - SL", "Tổng xuất TK - TT", "Tồn CK - SL", "Tồn CK - TT" };
                            for (int i = 0; i < _tieude.Length; i++)
                            {
                                DungChung.Bien.MangHaiChieu[0, i] = _tieude[i];
                            }

                            //for (int i = 0; i <= 17; i++) {
                            //    DungChung.Bien.MangHaiChieu[0, i] = _arr[0,i];
                            //}
                            foreach (var r in q)
                            {
                                DungChung.Bien.MangHaiChieu[num, 0] = num;
                                DungChung.Bien.MangHaiChieu[num, 1] = r.TenHamLuong;
                                DungChung.Bien.MangHaiChieu[num, 2] = r.DonVi;
                                DungChung.Bien.MangHaiChieu[num, 3] = r.DonGia;
                                DungChung.Bien.MangHaiChieu[num, 4] = r.TonDKSL;
                                DungChung.Bien.MangHaiChieu[num, 5] = r.TonDKTT;
                                DungChung.Bien.MangHaiChieu[num, 6] = r.NhapTKSL;
                                DungChung.Bien.MangHaiChieu[num, 7] = r.NhapTKTT;
                                DungChung.Bien.MangHaiChieu[num, 8] = r.XuatNoiTruSL;
                                DungChung.Bien.MangHaiChieu[num, 9] = r.xuatNoiTruTT;
                                DungChung.Bien.MangHaiChieu[num, 10] = r.XuatNgoaiTruSL;
                                DungChung.Bien.MangHaiChieu[num, 11] = r.xuatNgoaiTruTT;
                                DungChung.Bien.MangHaiChieu[num, 12] = r.XuatKhacSL;
                                DungChung.Bien.MangHaiChieu[num, 13] = r.xuatKhacTT;
                                DungChung.Bien.MangHaiChieu[num, 14] = r.XuatTKTongSL;
                                DungChung.Bien.MangHaiChieu[num, 15] = r.xuatTKTongTT;
                                DungChung.Bien.MangHaiChieu[num, 16] = r.TonCKSL;
                                DungChung.Bien.MangHaiChieu[num, 17] = r.TonCKTT;
                                DungChung.Bien.MangHaiChieu[num, 18] = r.TenHC;
                                DungChung.Bien.MangHaiChieu[num, 19] = r.HamLuong;

                                num++;
                            }
                            bool _nhieukho = false;
                            if (_kpChon.Count > 1)
                                _nhieukho = true;
                            BaoCao.RepBcNXT_01830 rep = new BaoCao.RepBcNXT_01830(chkHienthi.Checked, ckTrongNgoaiNuoc.Checked);
                            frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "Báo cáo NXT", "C:\\TsBCNXT.xls", true, this.Name);
                            rep.TuNgay.Value = dateTuNgay.Text;
                            rep.DenNgay.Value = dateDenNgay.Text;
                            rep.Kho.Value = _tenkho;
                            rep._tenc1.Value = _tenc1;
                            rep._tenc2.Value = _tenc2;
                            rep.DataSource = q;
                            rep.Kho.Value = _tenkho;
                            rep.NhaCC.Value = _tenNCC;
                            rep.BindingData();
                            rep.CreateDocument();
                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                            frm.ShowDialog();
                        }
                        #endregion
                        //mẫu rút gọn
                        #region mẫu rút gọn _ 26007
                        if (mau26007.SelectedIndex == 0 && DungChung.Bien.MaBV == "26007")
                        {
                            string[] _arr = new string[] { "0", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@" };
                            int[] _arrWidth = new int[] { };
                            // int[] _arrWidth = new int[] { }; có thể gọi khi truyền kiểu fomatAutowith = true
                            int num = 1;
                            DungChung.Bien.MangHaiChieu = new Object[q.Count + 1, 20];
                            string[] _tieude = { "STT", "TenThuoc", "DVT", "DonGia", "Tồn ĐK - SL", "Tồn ĐK - TT", "Nhập HD - SL", "Nhập HD - TT", "Nhập CK - SL", "Nhập CK - TT", "Tổng xuất TK - SL", "Tổng xuất TK - TT", "Tồn CK - SL", "Tồn CK - TT" };
                            for (int i = 0; i < _tieude.Length; i++)
                            {
                                DungChung.Bien.MangHaiChieu[0, i] = _tieude[i];
                            }

                            //for (int i = 0; i <= 17; i++) {
                            //    DungChung.Bien.MangHaiChieu[0, i] = _arr[0,i];
                            //}
                            foreach (var r in q)
                            {
                                DungChung.Bien.MangHaiChieu[num, 0] = num;
                                DungChung.Bien.MangHaiChieu[num, 1] = r.TenHamLuong;
                                DungChung.Bien.MangHaiChieu[num, 2] = r.DonVi;
                                DungChung.Bien.MangHaiChieu[num, 3] = r.DonGia;
                                DungChung.Bien.MangHaiChieu[num, 4] = r.TonDKSL;
                                DungChung.Bien.MangHaiChieu[num, 5] = r.TonDKTT;
                                DungChung.Bien.MangHaiChieu[num, 6] = r.NhapHD_SL;
                                DungChung.Bien.MangHaiChieu[num, 7] = r.NhapHD_TT;
                                DungChung.Bien.MangHaiChieu[num, 8] = r.NhapCK_SL;
                                DungChung.Bien.MangHaiChieu[num, 9] = r.NhapCK_TT;
                                DungChung.Bien.MangHaiChieu[num, 10] = r.XuatTKTongSL;
                                DungChung.Bien.MangHaiChieu[num, 11] = r.xuatTKTongTT;
                                DungChung.Bien.MangHaiChieu[num, 12] = r.TonCKSL;
                                DungChung.Bien.MangHaiChieu[num, 13] = r.TonCKTT;
                                num++;
                            }
                            BaoCao.repBcNXTrutgon_26007 rep = new BaoCao.repBcNXTrutgon_26007(chkHienthi.Checked, ckTrongNgoaiNuoc.Checked);
                            frmIn frm2 = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "Báo cáo NXT", "C:\\TsBCNXT_RG.xls", true, this.Name);
                            rep.TuNgay.Value = dateTuNgay.Text;
                            rep.DenNgay.Value = dateDenNgay.Text;
                            rep.Kho.Value = _tenkho;
                            rep.DataSource = q;
                            rep.NhaCC.Value = _tenNCC;
                            rep.BindingData();
                            rep.CreateDocument();
                            frm2.prcIN.PrintingSystem = rep.PrintingSystem;
                            frm2.ShowDialog();
                        }
                        #endregion
                        #region mẫu rút gọn
                        else
                        if (radMauIn.SelectedIndex == 0)
                        {
                            string[] _arr = new string[] { "0", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@" };
                            int[] _arrWidth = new int[] { };
                            // int[] _arrWidth = new int[] { }; có thể gọi khi truyền kiểu fomatAutowith = true
                            int num = 1;
                            DungChung.Bien.MangHaiChieu = new Object[q.Count + 1, 20];
                            string[] _tieude = { "STT", "TenThuoc", "DVT", "DonGia", "Tồn ĐK - SL", "Tồn ĐK - TT", "Nhập TK - Sl", "Nhập TK - TT", "Tổng xuất TK - SL", "Tổng xuất TK - TT", "Tồn CK - SL", "Tồn CK - TT" };
                            for (int i = 0; i < _tieude.Length; i++)
                            {
                                DungChung.Bien.MangHaiChieu[0, i] = _tieude[i];
                            }

                            //for (int i = 0; i <= 17; i++) {
                            //    DungChung.Bien.MangHaiChieu[0, i] = _arr[0,i];
                            //}
                            foreach (var r in q)
                            {
                                DungChung.Bien.MangHaiChieu[num, 0] = num;
                                DungChung.Bien.MangHaiChieu[num, 1] = r.MaTam;
                                DungChung.Bien.MangHaiChieu[num, 2] = r.TenHamLuong;
                                DungChung.Bien.MangHaiChieu[num, 3] = r.DonVi;
                                DungChung.Bien.MangHaiChieu[num, 4] = r.DonGia;
                                DungChung.Bien.MangHaiChieu[num, 5] = r.TonDKSL;
                                DungChung.Bien.MangHaiChieu[num, 6] = r.TonDKTT;
                                DungChung.Bien.MangHaiChieu[num, 7] = r.NhapTKSL;
                                DungChung.Bien.MangHaiChieu[num, 8] = r.NhapTKTT;
                                DungChung.Bien.MangHaiChieu[num, 9] = r.XuatTKTongSL;
                                DungChung.Bien.MangHaiChieu[num, 10] = r.xuatTKTongTT;
                                DungChung.Bien.MangHaiChieu[num, 11] = r.TonCKSL;
                                DungChung.Bien.MangHaiChieu[num, 12] = r.TonCKTT;
                                DungChung.Bien.MangHaiChieu[num, 13] = r.TenHC;
                                DungChung.Bien.MangHaiChieu[num, 14] = r.HamLuong;

                                num++;
                            }
                            if (ckthuongtin.Checked)
                            {

                                BaoCao.repBcNXTrutgon_01830 rep = new BaoCao.repBcNXTrutgon_01830(chkHienthi.Checked, ckTrongNgoaiNuoc.Checked);
                                frmIn frm2 = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "Báo cáo NXT", "C:\\TsBCNXT_RG.xls", true, this.Name);
                                rep.TuNgay.Value = dateTuNgay.Text;
                                rep.DenNgay.Value = dateDenNgay.Text;
                                rep.Kho.Value = _tenkho;
                                rep.DataSource = q;
                                rep.NhaCC.Value = _tenNCC;
                                rep.BindingData();
                                rep.CreateDocument();
                                frm2.prcIN.PrintingSystem = rep.PrintingSystem;
                                frm2.ShowDialog();
                            }
                            else
                            {
                                string macqcq = data.BenhViens.Where(p => p.MaBV == DungChung.Bien.MaBV).FirstOrDefault().MaChuQuan;
                                if (DungChung.Bien.MaBV == "24009" || macqcq == "24009")
                                {
                                    BaoCao.repBcNXTrutgon_24009 rep = new BaoCao.repBcNXTrutgon_24009(chkHienthi.Checked, ckTrongNgoaiNuoc.Checked);
                                    frmIn frm2 = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "Báo cáo NXT", "C:\\TsBCNXT_RG.xls", true, this.Name);
                                    rep.TuNgay.Value = dateTuNgay.Text;
                                    rep.DenNgay.Value = dateDenNgay.Text;
                                    rep.Kho.Value = _tenkho;
                                    rep.DataSource = q;
                                    double test = (double)q.Sum(p => p.TonCKTT);
                                    rep.NhaCC.Value = _tenNCC;
                                    rep.BindingData();
                                    rep.CreateDocument();
                                    frm2.prcIN.PrintingSystem = rep.PrintingSystem;
                                    frm2.ShowDialog();
                                }
                                else if (DungChung.Bien.MaBV != "30002" || (DungChung.Bien.MaBV == "30002" && ckbBVLaoPhoi.Checked == false))
                                {
                                    if (ckcHTSoQD.Checked == true)
                                    {
                                        BaoCao.repBcNXTrutgon_SoQD rep = new BaoCao.repBcNXTrutgon_SoQD(chkHienthi.Checked, ckTrongNgoaiNuoc.Checked);
                                        frmIn frm2 = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "Báo cáo NXT", "C:\\TsBCNXT_RG.xls", true, this.Name);
                                        rep.TuNgay.Value = dateTuNgay.Text;
                                        rep.DenNgay.Value = dateDenNgay.Text;
                                        rep.Kho.Value = _tenkho;
                                        rep.DataSource = q;
                                        double test = (double)q.Sum(p => p.TonCKTT);
                                        rep.NhaCC.Value = _tenNCC;
                                        rep.BindingData();
                                        rep.CreateDocument();
                                        frm2.prcIN.PrintingSystem = rep.PrintingSystem;
                                        frm2.ShowDialog();
                                    }
                                   
                                    else
                                    {

                                        BaoCao.repBcNXTrutgon repRG = new BaoCao.repBcNXTrutgon(chkHienthi.Checked, ckTrongNgoaiNuoc.Checked);
                                        if (DungChung.Bien.MaBV == "27023" && cklKP.SelectedValue.ToString() == "98")
                                        {
                                            repRG.xrTableCell1.Text = "TP. TCHC-CTXH";
                                            repRG.colKhoaDuoc.Text = "Nguyễn Xuân Sanh";
                                        }
                                        frmIn frm2 = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "Báo cáo NXT", "C:\\TsBCNXT_RG.xls", true, this.Name);
                                        repRG.Kho.Value = _tenkho;
                                        repRG.DataSource = q;
                                        if (DungChung.Bien.MaBV == "24012")
                                        {
                                            repRG.DataSource = q24012;
                                        }
                                        else if (DungChung.Bien.MaBV == "24297" && ckcGiaNhap.Checked)
                                        {
                                            repRG.xrLabel11.Text = "Đơn giá nhập";
                                        }
                                        else if (DungChung.Bien.MaBV == "27022")
                                        {
                                            List<Q1> q1 = new List<Q1>();
                                            foreach (var item in q)
                                            {
                                                Q1 q2 = new Q1();
                                                q2.TenHamLuong = item.TenHamLuong;
                                                q2.HamLuong = item.HamLuong;
                                                q2.NhomThau = item.NhomThau;
                                                q2.DonVi = item.DonVi;
                                                q2.DonGia = (double)item.DonGia;
                                                q2.TonDKSL = item.TonDKSL;
                                                q2.TonDKTT = (double)item.TonDKTT;
                                                q2.NhapTKSL = item.NhapTKSL;
                                                q2.XuatTKTongSL = item.XuatTKTongSL;
                                                q2.TonCKSL = item.TonCKSL;
                                                q2.NhapTKTT = (double)item.NhapTKTT;
                                                q2.XuatTKTongTT = (double)item.xuatTKTongTT;
                                                q2.TonCKTT = (double)item.TonCKTT;
                                                q2.TenNhomDuoc = item.TenNhomDuoc;
                                                q2.NuocSX = item.NuocSX;
                                                q2.TenTN = item.TenTN;
                                                q2.STT = item.STT;
                                                if (item.IDNhom == 9 || item.IDNhom == 10)
                                                {
                                                    q2.TenHC = item.TenHC;
                                                }
                                                q1.Add(q2);
                                            }

                                            repRG.DataSource = q1;
                                        }
                                        repRG.TuNgayDenNgay.Value = "Từ ngày " + dateTuNgay.Text + " đến ngày " + dateDenNgay.Text;
                                        repRG.NhaCC.Value = _tenNCC;
                                        repRG.BindingData();
                                        repRG.CreateDocument();
                                        frm2.prcIN.PrintingSystem = repRG.PrintingSystem;
                                        frm2.ShowDialog();
                                    }
                                }
                            }
                        }
                        #endregion

                        else
                        #region mẫu rút gọn (trả dược)
                                if (radMauIn.SelectedIndex == 4)
                        {
                            string[] _arr = new string[] { "0", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@" };
                            int[] _arrWidth = new int[] { };
                            // int[] _arrWidth = new int[] { }; có thể gọi khi truyền kiểu fomatAutowith = true
                            int num = 1;
                            DungChung.Bien.MangHaiChieu = new Object[q.Count + 1, 20];
                            string[] _tieude = { "STT", "TenThuoc", "DVT", "DonGia", "Tồn ĐK - SL", "Tồn ĐK - TT", "Nhập TK - SL", "Nhập TK - TT", "Nhập trả lại - SL", "Nhập trả lại - TT", "Tổng xuất TK - SL", "Tổng xuất TK - TT", "Tồn CK - SL", "Tồn CK - TT" };
                            for (int i = 0; i < _tieude.Length; i++)
                            {
                                DungChung.Bien.MangHaiChieu[0, i] = _tieude[i];
                            }

                            foreach (var r in q)
                            {
                                DungChung.Bien.MangHaiChieu[num, 0] = num;
                                DungChung.Bien.MangHaiChieu[num, 1] = r.TenHamLuong;
                                DungChung.Bien.MangHaiChieu[num, 2] = r.DonVi;
                                DungChung.Bien.MangHaiChieu[num, 3] = r.DonGia;
                                DungChung.Bien.MangHaiChieu[num, 4] = r.TonDKSL;
                                DungChung.Bien.MangHaiChieu[num, 5] = r.TonDKTT;
                                DungChung.Bien.MangHaiChieu[num, 6] = r.NhapTKSL;
                                DungChung.Bien.MangHaiChieu[num, 7] = r.NhapTKTT;
                                DungChung.Bien.MangHaiChieu[num, 8] = r.NhapTra_SL;
                                DungChung.Bien.MangHaiChieu[num, 9] = r.NhapTra_TT;
                                DungChung.Bien.MangHaiChieu[num, 10] = r.TongXuatTKSL;
                                DungChung.Bien.MangHaiChieu[num, 11] = r.TongXuatTKTT;
                                DungChung.Bien.MangHaiChieu[num, 12] = r.TonCKSL;
                                DungChung.Bien.MangHaiChieu[num, 13] = r.TonCKTT;
                                num++;
                            }
                            if (ckthuongtin.Checked)
                            {


                                BaoCao.repBcNXTrutgon_Nhaptra_01830 rep = new BaoCao.repBcNXTrutgon_Nhaptra_01830(chkHienthi.Checked, ckTrongNgoaiNuoc.Checked);
                                frmIn frm2 = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "Báo cáo NXT", "C:\\TsBCNXT_NhapTra.xls", true, this.Name);
                                rep.TuNgay.Value = dateTuNgay.Text;
                                rep.DenNgay.Value = dateDenNgay.Text;
                                rep.Kho.Value = _tenkho;
                                rep.DataSource = q;
                                rep.NhaCC.Value = _tenNCC;
                                rep.BindingData();
                                rep.CreateDocument();
                                frm2.prcIN.PrintingSystem = rep.PrintingSystem;
                                frm2.ShowDialog();
                            }
                            else
                            {

                                BaoCao.repBcNXTrutgon_Nhaptra rep = new BaoCao.repBcNXTrutgon_Nhaptra(chkHienthi.Checked, ckTrongNgoaiNuoc.Checked);
                                frmIn frm2 = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "Báo cáo NXT", "C:\\TsBCNXT_NhapTra.xls", true, this.Name);
                                rep.TuNgay.Value = dateTuNgay.Text;
                                rep.DenNgay.Value = dateDenNgay.Text;
                                rep.Kho.Value = _tenkho;
                                rep.DataSource = q;
                                if (DungChung.Bien.MaBV == "24012")
                                {
                                    rep.DataSource = q24012;
                                }
                                rep.NhaCC.Value = _tenNCC;
                                rep.BindingData();
                                rep.CreateDocument();
                                frm2.prcIN.PrintingSystem = rep.PrintingSystem;
                                frm2.ShowDialog();
                            }
                        }
                        #endregion
                        else if (radMauIn.SelectedIndex == 5)
                        {
                            string[] _arr = new string[] { "0", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@" };
                            int[] _arrWidth = new int[] { };
                            // int[] _arrWidth = new int[] { }; có thể gọi khi truyền kiểu fomatAutowith = true
                            int num = 1;
                            DungChung.Bien.MangHaiChieu = new Object[q4.Count + 1, 20];
                            string[] _tieude = { "STT", "TenThuoc", "DVT", "DonGia", "Tồn ĐK - SL", "Tồn ĐK - TT", "Nhập TK - SL", "Nhập TK - TT", "Nhập trả lại - SL", "Nhập trả lại - TT", "Tổng xuất TK - SL", "Tổng xuất TK - TT", "Tồn CK - SL", "Tồn CK - TT" };
                            for (int i = 0; i < _tieude.Length; i++)
                            {
                                DungChung.Bien.MangHaiChieu[0, i] = _tieude[i];
                            }

                            foreach (var r in q4)
                            {
                                DungChung.Bien.MangHaiChieu[num, 0] = num;
                                DungChung.Bien.MangHaiChieu[num, 1] = r.TenHamLuong;
                                DungChung.Bien.MangHaiChieu[num, 2] = r.DonVi;
                                DungChung.Bien.MangHaiChieu[num, 3] = r.DonGia;
                                DungChung.Bien.MangHaiChieu[num, 4] = r.TonDKSL;
                                DungChung.Bien.MangHaiChieu[num, 5] = r.TonDKTT;
                                DungChung.Bien.MangHaiChieu[num, 6] = r.NhapTKSL;
                                DungChung.Bien.MangHaiChieu[num, 7] = r.NhapTKTT;
                                DungChung.Bien.MangHaiChieu[num, 8] = r.NhapTra_SL;
                                DungChung.Bien.MangHaiChieu[num, 9] = r.NhapTra_TT;
                                DungChung.Bien.MangHaiChieu[num, 10] = r.TongXuatTKSL;
                                DungChung.Bien.MangHaiChieu[num, 11] = r.TongXuatTKTT;
                                DungChung.Bien.MangHaiChieu[num, 12] = r.TonCKSL;
                                DungChung.Bien.MangHaiChieu[num, 13] = r.TonCKTT;
                                num++;
                            }
                            #region Không tính hư hao đông y 20001
                            if (ckcHTSoQD.Checked == true)
                            {
                                BaoCao.repBcNXTrutgon_SoQD rep = new BaoCao.repBcNXTrutgon_SoQD(chkHienthi.Checked, ckTrongNgoaiNuoc.Checked);
                                frmIn frm2 = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "Báo cáo NXT", "C:\\TsBCNXT_RG.xls", true, this.Name);
                                rep.TuNgay.Value = dateTuNgay.Text;
                                rep.DenNgay.Value = dateDenNgay.Text;
                                rep.Kho.Value = _tenkho;
                                rep.DataSource = q4;
                                rep.NhaCC.Value = _tenNCC;
                                rep.BindingData();
                                rep.CreateDocument();
                                frm2.prcIN.PrintingSystem = rep.PrintingSystem;
                                frm2.ShowDialog();
                            }

                            else
                            {

                                BaoCao.repBcNXTrutgon_01071 rep = new BaoCao.repBcNXTrutgon_01071(chkHienthi.Checked, ckTrongNgoaiNuoc.Checked);
                                frmIn frm2 = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "Báo cáo NXT", "C:\\TsBCNXT_RG.xls", true, this.Name);
                                rep.TuNgay.Value = dateTuNgay.Text;
                                rep.DenNgay.Value = dateDenNgay.Text;
                                rep.Kho.Value = _tenkho;
                                rep.DataSource = q4;
                                rep.NhaCC.Value = _tenNCC;
                                rep.BindingData();
                                rep.CreateDocument();
                                frm2.prcIN.PrintingSystem = rep.PrintingSystem;
                                frm2.ShowDialog();
                            }
                            #endregion

                        }
                    }
                    else
                    {
                        MessageBox.Show("Bạn chưa chọn kho");

                    }
                }
            }
        }
        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public class cot
        {
            private string tenCot;

            public string TenCot
            {
                get { return tenCot; }
                set { tenCot = value; }
            }
            private string maCot;

            public string MaCot
            {
                get { return maCot; }
                set { maCot = value; }
            }
        }
        List<cot> _lCot = new List<cot>();
        private void frmTsBCNXT_Load(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            if (DungChung.Bien.MaBV == "04007")
            {
                chkMauNhieuKho.Visible = true;
                ckbBVChiLinh.Enabled = false;
                ckTrongNgoaiNuoc.Enabled = false;
                ckHienThiMaDV.Enabled = false;
                checkEdit1.Enabled = false;
                cbo_cot1.Enabled = false;
                cbo_cot2.Enabled = false;
            }
            if (DungChung.Bien.MaBV == "14017")//
            {
                this.dateTuNgay.Properties.DisplayFormat.FormatString = "g";
                this.dateTuNgay.Properties.EditFormat.FormatString = "g";
                this.dateTuNgay.Properties.Mask.EditMask = "g";

                this.dateDenNgay.Properties.DisplayFormat.FormatString = "g";
                this.dateDenNgay.Properties.EditFormat.FormatString = "g";
                this.dateDenNgay.Properties.Mask.EditMask = "g";
            }
            if (DungChung.Bien.MaBV == "30004")//BV nam sách
            {
                bv30004.Visible = true;
            }
            else
            {
                this.Size = new Size(this.Width, this.Height - bv30004.Height);
            }
            if (DungChung.Bien.MaBV.Equals("12122") || DungChung.Bien.MaBV == "30002")//BV Lao Phổi Lai Châu
            {
                if (DungChung.Bien.MaBV == "12122")
                {
                    gct12122.Text = "12122";
                    ckbBVLaoPhoi.Text = "Mẫu rút gọn (BV Lao Phổi Lai Châu)";
                }
                else if (DungChung.Bien.MaBV == "30002")
                {
                    gct12122.Text = "30002";
                    ckbBVLaoPhoi.Text = "Mẫu rút gọn 30002";
                }
                gct12122.Visible = true;
            }
            else
            {
                gct12122.Visible = false;
                ckbBVLaoPhoi.Checked = false;
                this.Size = new Size(this.Width, this.Height - gct12122.Height);
            }
            if (DungChung.Bien.MaBV.Equals("26007"))//Trung tâm y tế huyện Bình Xuyên
            {
                gct26007.Visible = true;
            }
            else
            {
                gct26007.Visible = false;
                this.Size = new Size(this.Width, this.Height - gct26007.Height);
            }
            if (DungChung.Bien.MaBV.Equals("12001"))// TYT xã Mỹ Nhơn
            {
                gc12001.Visible = true;
            }
            else
            {
                gc12001.Visible = false;
                this.Size = new Size(this.Width, this.Height - gc12001.Height);
            }

            if (DungChung.Bien.MaBV.Equals("01830"))//BV đa khoa Thường Tín
            {
                grc01830.Visible = true;
            }
            else
            {
                grc01830.Visible = false;
                this.Size = new Size(this.Width, this.Height - grc01830.Height);
            }
            if (DungChung.Bien.MaBV.Equals("56789"))
            {
                gc56789.Visible = true;
            }
            else
            {
                gc56789.Visible = false;
                this.Size = new Size(this.Width, this.Height - gc56789.Height);
            }
            if (DungChung.Bien.MaBV == "30009")
            {
                ckbBVChiLinh.Text = "Xuất Excel TTYT Huyện Thanh Hà";

            }
            if (DungChung.Bien.MaBV == "24009")
            {
                bv24009.Visible = true;
                _lCot.Add(new cot { MaCot = "0", TenCot = "Tất cả" });
                _lCot.Add(new cot { MaCot = "HamLuong", TenCot = "Hàm Lượng" });
                _lCot.Add(new cot { MaCot = "DonGia", TenCot = "Đơn Giá" });
                _lCot.Add(new cot { MaCot = "DuongDung", TenCot = "Đường dùng" });
                _lCot.Add(new cot { MaCot = "SoDK", TenCot = "Số ĐK" });
                _lCot.Add(new cot { MaCot = "HangSX", TenCot = "Hãng sản xuất" });
                _lCot.Add(new cot { MaCot = "NuocSX", TenCot = "Nước SX" });
                _lCot.Add(new cot { MaCot = "NhaThau", TenCot = "Nhà thầu" });
                _lCot.Add(new cot { MaCot = "MaQD", TenCot = "Mã QĐ" });
                _lCot.Add(new cot { MaCot = "NhomThau", TenCot = "Nhóm thầu" });
                _lCot.Add(new cot { MaCot = "TonDKSL", TenCot = "Tồn ĐK" });
                _lCot.Add(new cot { MaCot = "NhapTKSL", TenCot = "Nhập" });
                _lCot.Add(new cot { MaCot = "XuatTKTongSL", TenCot = "Xuất" });
                _lCot.Add(new cot { MaCot = "TonCKSL", TenCot = "Tồn CK" });

                cklCot.DataSource = _lCot.ToList();
                for (int i = 0; i < cklCot.ItemCount; i++)
                {
                    if (cklCot.GetItemValue(i) != null)
                        cklCot.SetItemChecked(i, true);
                    else
                        cklCot.SetItemChecked(i, false);
                }
            }
            else
            {
                bv24009.Visible = false;
                this.Size = new Size(this.Width, this.Height - bv24009.Height);
            }
            //if (DungChung.Bien.MaBV == "27022")
            //    ckcDonGiaVat.Visible = true;
            radMauIn_SelectedIndexChanged(null, null);
            cbo_cot1.Properties.DataSource = ReturnList(true);
            cbo_cot2.Properties.DataSource = ReturnList(true);
            cbo_cot3.Properties.DataSource = ReturnList(true);
            cbo_cot4.Properties.DataSource = ReturnList(true);
            var qcc = from CC in data.NhaCCs select new { CC.MaCC, CC.TenCC };
            lupNhaCC.Properties.DataSource = qcc.ToList();
            dateDenNgay.DateTime = System.DateTime.Now;
            dateTuNgay.DateTime = System.DateTime.Now;
            List<NhomDV> _lnhom = new List<NhomDV>();
            _lnhom = data.NhomDVs.Where(p => p.Status == 1).ToList();
            _lnhom.Add(new NhomDV { IDNhom = -1, TenNhom = " Tất cả" });
            lupNhom.Properties.DataSource = _lnhom.OrderBy(p => p.TenNhom).ToList();
            var dskp = (from kp in data.KPhongs.Where(p => p.PLoai == "Khoa dược")
                        select new
                        {
                            MaKP = kp.MaKP,
                            TenKP = kp.TenKP
                        }).OrderBy(p => p.TenKP).ToList();
            lup_KhoTong.Properties.DataSource = dskp;
            cklKP.DataSource = dskp;
            for (int i = 0; i < cklKP.ItemCount; i++)
            {
                if (cklKP.GetItemValue(i) != null && Convert.ToInt32(cklKP.GetItemValue(i)) == DungChung.Bien.MaKP)
                    cklKP.SetItemChecked(i, true);
                else
                    cklKP.SetItemChecked(i, false);
            }
            if (radMauIn.SelectedIndex == 0)
            {
                radLoaiXuat.Enabled = false;
                cbo_cot1.Properties.ReadOnly = true;
                cbo_cot2.Properties.ReadOnly = true;
            }
            if (DungChung.Bien.MaBV == "26007")
            {
                label9.Visible = true;
                cbo_cot3.Visible = true;
            }
            else
            {
                label9.Visible = false;
                cbo_cot3.Visible = false;
            }
            if (radMauIn.SelectedIndex == 5)
            {
                radLoaiXuat.Enabled = false;
                cbo_cot1.Properties.ReadOnly = true;
                cbo_cot2.Properties.ReadOnly = true;
            }

            if(DungChung.Bien.MaBV == "24297")
            {
                ckcGiaNhap.Visible = true;
            }

            this.Height = this.Height + 50;
            frmTsBCNXT_ResizeEnd(null, null);

        }

        private void lupKho_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void lupNhom_EditValueChanged(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            List<TieuNhomDV> _ltnhom = new List<TieuNhomDV>();
            int id = -1;
            if (lupNhom.EditValue != null)
                id = Convert.ToInt32(lupNhom.EditValue);
            _ltnhom = data.TieuNhomDVs.Where(p => p.Status == 1).ToList();
            _ltnhom.Add(new TieuNhomDV { IdTieuNhom = -1, TenTN = " Tất cả" });
            if (id >= 0)
                _ltnhom = _ltnhom.Where(p => p.IDNhom == id).ToList();
            lupTieuNhom.Properties.DataSource = _ltnhom.OrderBy(p => p.TenTN).ToList();
        }

        private void cklKP_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        #region function ReturnList
        private List<NoiDung> ReturnList(bool tt)
        {
            //true: Load loại xuất theo kiểu đơn, false: loại xuất theo đối tượng bệnh nhân
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            List<NoiDung> _lNoiDung = new List<NoiDung>();
            if (tt)
            {
                _lNoiDung.Clear();
                List<DungChung.Bien.c_PhanLoaiXuat> list = DungChung.Bien.c_PhanLoaiXuat._setPhanLoaiXuat();
                foreach (var a in list)
                {
                    _lNoiDung.Add(new NoiDung { Id = a.Id, Ten = a.PhanLoai });
                }
                //_lNoiDung.Add(new NoiDung { Id = 0, Ten = "Xuất ngoại trú" });
                //_lNoiDung.Add(new NoiDung { Id = 1, Ten = "Xuất nội trú" });
                //_lNoiDung.Add(new NoiDung { Id = 2, Ten = "Xuất nội bộ" });
                //_lNoiDung.Add(new NoiDung { Id = 3, Ten = "Xuất ngoài BV" });
                //_lNoiDung.Add(new NoiDung { Id = 4, Ten = "Xuất nhân dân" });
                //_lNoiDung.Add(new NoiDung { Id = 5, Ten = "Xuất Cận Lâm Sàng" });
                //_lNoiDung.Add(new NoiDung { Id = 6, Ten = "Xuất tủ trực" });
                //_lNoiDung.Add(new NoiDung { Id = 7, Ten = "Xuất phòng khám" });
                //_lNoiDung.Add(new NoiDung { Id = 8, Ten = "Xuất kiểm nghiệm" });
                //_lNoiDung.Add(new NoiDung { Id = 9, Ten = "Xuất khác" });
                return _lNoiDung.ToList();
            }
            else
            {
                _lNoiDung.Clear();
                NoiDung moi = new NoiDung();
                var dtbn = data.DTBNs.ToList();
                foreach (var item in dtbn)
                {
                    moi = new NoiDung();
                    moi.Id = item.IDDTBN;
                    moi.Ten = item.DTBN1;
                    _lNoiDung.Add(moi);
                }
                return _lNoiDung.ToList();
            }
        }
        #endregion

        private void radMauIn_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (DungChung.Bien.MaBV == "12122")
            {
                ckbBVLaoPhoi.Enabled = true;
            }
            else
            {
                ckbBVLaoPhoi.Checked = false;
                ckbBVLaoPhoi.Enabled = false;
            }
            if (radMauIn.SelectedIndex == 0)
            {
                ckcHTSoQD.Visible = true;
                cbo_cot1.Properties.ReadOnly = true;
                cbo_cot2.Properties.ReadOnly = true;
                radLoaiXuat.Enabled = false;
                if (DungChung.Bien.MaBV == "27022")
                    ckcDonGiaVat.Visible = true;
                if (DungChung.Bien.MaBV == "30372")
                    checkInDM.Visible = true;
                else
                    ckcDonGiaVat.Visible = false;

                if (DungChung.Bien.MaBV == "30002")
                {
                    ckbBVLaoPhoi.Enabled = true;
                }
            }
            else if (radMauIn.SelectedIndex == 5)
            {
                ckcHTSoQD.Visible = true;
            }
            else
            {
                ckcDonGiaVat.Visible = false;
                ckcHTSoQD.Visible = false;
            }
            if (radMauIn.SelectedIndex == 1)
            {
                if (DungChung.Bien.MaBV == "01049")
                {
                    cbo_cot1.Properties.ReadOnly = true;
                    cbo_cot2.Properties.ReadOnly = true;
                }
                else
                {
                    cbo_cot1.Properties.ReadOnly = false;
                    cbo_cot2.Properties.ReadOnly = false;
                }
                radLoaiXuat.Enabled = true;
                radLoaiXuat.SelectedIndex = 0;
                cbo_cot1.Properties.DataSource = ReturnList(true);
                cbo_cot2.Properties.DataSource = ReturnList(true);
                ckHienThiMaDV.Checked = false;
            }
            else
            {
                cbo_cot1.Properties.ReadOnly = false;
                cbo_cot2.Properties.ReadOnly = false;
                radLoaiXuat.Enabled = false;
                cbo_cot1.Properties.DataSource = ReturnList(true);
                cbo_cot2.Properties.DataSource = ReturnList(true);

            }

            if(DungChung.Bien.MaBV == "24297" && radMauIn.SelectedIndex == 0 || radMauIn.SelectedIndex == 1)
            {
                ckcGiaNhap.Visible = true;
            }
            else
            {
                ckcGiaNhap.Visible = false;
                ckcGiaNhap.Checked = false;
            }

        }

        private void cklKP_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            int check = 0;
            for (int i = 0; i < cklKP.ItemCount; i++)
            {
                if (cklKP.GetItemChecked(i))
                    check++;
            }
            if (check > 1)
            {
                radMauIn.Properties.ReadOnly = true;
                radMauIn.SelectedIndex = -1;
            }
            else
            {
                radMauIn.Properties.ReadOnly = false;
                radMauIn.SelectedIndex = 0;
            }
        }

        #region class NoiDung
        public class NoiDung
        {
            public int Id { get; set; }
            public string Ten { get; set; }
        }
        #endregion

        private void radLoaiXuat_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (radLoaiXuat.SelectedIndex == 0)
            {
                cbo_cot1.Properties.DataSource = ReturnList(true);
                cbo_cot2.Properties.DataSource = ReturnList(true);
            }
            if (radLoaiXuat.SelectedIndex == 1)
            {
                cbo_cot1.Properties.DataSource = ReturnList(false);
                cbo_cot2.Properties.DataSource = ReturnList(false);
            }
        }

        private void groupControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cklCot_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            int check = 0;
            for (int i = 0; i < cklCot.ItemCount; i++)
            {
                if (cklCot.GetItemChecked(i))
                    check++;
            }
            if (e.Index == 0)
            {
                if (e.State == CheckState.Checked)
                    cklCot.CheckAll();
                else
                    cklCot.UnCheckAll();
            }
        }

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEdit1.Checked == true)
            {
                cklCot.Enabled = true;
            }
            else
                cklCot.Enabled = false;
        }

        private void check30004_CheckedChanged(object sender, EventArgs e)
        {
            if (DungChung.Bien.MaBV == "30004" && check30004.Checked == true)
            {
                label9.Visible = true;
                cbo_cot3.Visible = true;
                cbo_cot4.Visible = true;
                label10.Visible = true;
            }
            else
            {
                label9.Visible = false;
                cbo_cot3.Visible = false;
                cbo_cot4.Visible = false;
                label10.Visible = false;
            }
        }

        private void frmTsBCNXT_ResizeEnd(object sender, EventArgs e)
        {
            Screen myScreen = Screen.FromControl(this);
            Rectangle area = myScreen.WorkingArea;

            this.Top = (area.Height - this.Height) / 2;
            this.Left = (area.Width - this.Width) / 2;
        }

        private void ckthuongtin_CheckedChanged(object sender, EventArgs e)
        {

        }
        private class Q1
        {
            public byte? STT { get; set; }
            public string NuocSX { get; set; }
            public string TenTN { get; set; }
            public string TenNhomDuoc { get; set; }
            public string TenHamLuong { get; set; }
            public string HamLuong { get; set; }
            public string NhomThau { get; set; }
            public string TenHC { get; set; }
            public string DonVi { get; set; }
            public double? DonGia { get; set; }
            public double TonDKSL { get; set; }
            public double TonDKTT { get; set; }
            public double NhapTKSL { get; set; }
            public double XuatTKTongSL { get; set; }
            public double TonCKSL { get; set; }
            public double NhapTKTT { get; set; }
            public double XuatTKTongTT { get; set; }
            public double TonCKTT { get; set; }
        }
    }
}