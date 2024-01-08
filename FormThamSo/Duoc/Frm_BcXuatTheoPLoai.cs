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
    public partial class Frm_BcXuatTheoPLoai : DevExpress.XtraEditors.XtraForm
    {
        public Frm_BcXuatTheoPLoai()
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
            if (lupKho.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn kho");
                lupKho.Focus();
                return false;
            }
            if (chkNgT.Checked == false && chkNT.Checked == false && chkNB.Checked == false && chkNgoaiBV.Checked == false && chkND.Checked == false && chkCLS.Checked == false && chkTT.Checked == false)
            {
                MessageBox.Show("Bạn chưa chọn phân loại xuất");
                return false;
            }

            else return true;
        }
        private void frmTsBcNXTXuat_BG_Load(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var q = from TK in data.KPhongs.Where(p => p.PLoai.Equals("Khoa dược")) select new { TK.TenKP, TK.MaKP };
            lupKho.Properties.DataSource = q.ToList();

            var qcc = from CC in data.NhaCCs select new { CC.MaCC, CC.TenCC };
            lupNhaCC.Properties.DataSource = qcc.ToList();
            dateTuNgay.DateTime = System.DateTime.Now;
            dateDenNgay.DateTime = System.DateTime.Now;
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public class DSKho
        {
            private string TenKhoa;

            public string TenKhoa1
            {
                get { return TenKhoa; }
                set { TenKhoa = value; }
            }
            private int MaKhoa;

            public int MaKhoa1
            {
                get { return MaKhoa; }
                set { MaKhoa = value; }
            }
            private int KieuDon;

            public int KieuDon1
            {
                get { return KieuDon; }
                set { KieuDon = value; }
            }

        }

        private void btnInBC_Click(object sender, EventArgs e)
        {
            DateTime tungay = DungChung.Ham.NgayTu(dateTuNgay.DateTime);
            DateTime denngay = DungChung.Ham.NgayDen(dateDenNgay.DateTime);
            List<DSKho> _dsk = new List<DSKho>();
            frmIn frm = new frmIn();

            if (KTtaoBcNXT())
            {
                _dsk.Clear();
                QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

                int _kho = 0;
                if (lupKho.EditValue != null)
                { _kho =Convert.ToInt32( lupKho.EditValue); }
                string _nhacc = "";
                if (lupNhaCC.EditValue != null)
                { _nhacc = lupNhaCC.EditValue.ToString(); }
                var qtenkho = (from kp in data.KPhongs
                               join nhapd in data.NhapDs on kp.MaKP equals nhapd.MaKP
                               where (nhapd.MaKP == _kho)
                               select new { kp.TenKP }).ToList();
                var qtenncc = (from nhapd in data.NhapDs
                               join nhacc in data.NhaCCs on nhapd.MaCC equals nhacc.MaCC
                               where (nhacc.MaCC == _nhacc)
                               select new { nhacc.TenCC }).ToList();
                #region Lấy tên kho
                var dsk1 = (from nd in data.NhapDs.Where(p => p.MaKP == _kho)
                            join kp in data.KPhongs.Where(p => p.PLoai == "Khoa dược" || p.PLoai == "Lâm sàng") on nd.MaKPnx equals kp.MaKP
                            select new { nd.MaKPnx, kp.TenKP, kp.PLoai }).ToList();
                if (chkNT.Checked == true)
                {
                    var dsk = (from ds in dsk1.Where(p => p.PLoai == "Lâm sàng")
                               group ds by new { ds.MaKPnx, ds.TenKP } into kq
                               select new { kq.Key.MaKPnx, kq.Key.TenKP }).ToList();
                    if (dsk.Count > 0)
                    {
                        foreach (var b in dsk)
                        {
                            DSKho them = new DSKho();
                            them.MaKhoa1 = b.MaKPnx==null?-1: b.MaKPnx.Value;
                            them.TenKhoa1 = b.TenKP;
                            them.KieuDon1 = 1;
                            _dsk.Add(them);
                        }

                    }
                }
                if (chkNB.Checked == true)
                {
                    var dsk = (from ds in dsk1.Where(p => p.PLoai == "Khoa dược")
                               group ds by new { ds.MaKPnx, ds.TenKP } into kq
                               select new { kq.Key.MaKPnx, kq.Key.TenKP }).ToList();
                    if (dsk.Count > 0)
                    {
                        foreach (var b in dsk)
                        {
                            DSKho them = new DSKho();
                            them.MaKhoa1 = b.MaKPnx == null ? -1 : b.MaKPnx.Value;
                            them.TenKhoa1 = b.TenKP;
                            them.KieuDon1 = 2;
                            _dsk.Add(them);
                        }

                    }
                }
                if (chkNgT.Checked == true)
                {
                    DSKho them = new DSKho();
                    them.MaKhoa1 = 100;
                    them.TenKhoa1 = "Xuất ngoại trú";
                    them.KieuDon1 = 0;
                    _dsk.Add(them);

                }
                if (chkNgoaiBV.Checked == true)
                {
                    DSKho them = new DSKho();
                    them.MaKhoa1 = 103;
                    them.TenKhoa1 = "Xuất xã phường";
                    them.KieuDon1 = 3;
                    _dsk.Add(them);
                }
                if (chkND.Checked == true)
                {
                    DSKho them = new DSKho();
                    them.MaKhoa1 = 104;
                    them.TenKhoa1 = "Xuất nhân dân";
                    them.KieuDon1 = 4;
                    _dsk.Add(them);
                }
                if (chkCLS.Checked == true)
                {
                    DSKho them = new DSKho();
                    them.MaKhoa1 = 105;
                    them.TenKhoa1 = "Xuất Cận Lâm sàng";
                    them.KieuDon1 = 5;
                    _dsk.Add(them);
                }
                if (chkTT.Checked == true)
                {
                    DSKho them = new DSKho();
                    them.MaKhoa1 = 106;
                    them.TenKhoa1 = "Xuất Tủ trực";
                    _dsk.Add(them);
                    them.KieuDon1 = 6;
                }

                var dskho = (from d in _dsk select new { d.MaKhoa1, d.TenKhoa1, d.KieuDon1 }).ToList();
                if (dskho.Count > 17)
                {
                    MessageBox.Show("Dữ liệu vượt giới hạn, báo cáo chỉ hiển thị tối đa 17 khoa phòng!");
                }
                int[] arrKP = new int[20];
                for (int i = 0; i < 20; i++)
                {
                    if (i < dskho.Count)
                        arrKP[i] = dskho.Skip(i).Take(1).First().MaKhoa1;
                    else
                        arrKP[i] = -1;
                }
                int[] arrKD = new int[20];
                for (int i = 0; i < 20; i++)
                {
                    if (i < dskho.Count)
                        arrKD[i] = dskho.Skip(i).Take(1).First().KieuDon1;
                    else
                        arrKD[i] = -1;
                }
                int colcount = dskho.Count + 1;
                if (dskho.Count >= 17)
                    colcount = 18;
                #endregion
                #region lấy dữ liệu
                var qnxt = (from nd in data.NhapDs
                            join ndct in data.NhapDcts on nd.IDNhap equals ndct.IDNhap
                            join dv in data.DichVus on ndct.MaDV equals dv.MaDV
                            select new { nd.NgayNhap, nd.MaKP, nd.PLoai, nd.KieuDon, nd.MaKPnx, dv.MaDV, dv.TenDV, ndct.DonVi, ndct.DonGia, ndct.SoLuongX, ndct.ThanhTienX, dv.MaCC }).ToList();
                var nx = (from nd in qnxt.Where(p => p.PLoai == 2).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Where(p => p.MaKP==_kho)
                          group nd by new { nd.MaDV, nd.TenDV, nd.DonVi, nd.DonGia, nd.KieuDon, nd.MaCC } into kq
                          select new
                          {
                              MaCC = kq.Key.MaCC,
                              MaDV = kq.Key.MaDV,
                              TenDV = kq.Key.TenDV,
                              DonVi = kq.Key.DonVi,
                              DonGia = kq.Key.DonGia,
                              SL1 = kq.Key.KieuDon == 1 || kq.Key.KieuDon == 2 ? kq.Where(p => p.KieuDon == arrKD[0] && p.MaKPnx == arrKP[0]).Sum(p => p.SoLuongX) : kq.Where(p => p.KieuDon == arrKD[0]).Sum(p => p.SoLuongX),
                              TT1 = kq.Key.KieuDon == 1 || kq.Key.KieuDon == 2 ? kq.Where(p => p.KieuDon == arrKD[0] && p.MaKPnx == arrKP[0]).Sum(p => p.ThanhTienX) : kq.Where(p => p.KieuDon == arrKD[0]).Sum(p => p.ThanhTienX),
                              SL2 = kq.Key.KieuDon == 1 || kq.Key.KieuDon == 2 ? kq.Where(p => p.KieuDon == arrKD[1] && p.MaKPnx == arrKP[1]).Sum(p => p.SoLuongX) : kq.Where(p => p.KieuDon == arrKD[1]).Sum(p => p.SoLuongX),
                              TT2 = kq.Key.KieuDon == 1 || kq.Key.KieuDon == 2 ? kq.Where(p => p.KieuDon == arrKD[1] && p.MaKPnx == arrKP[1]).Sum(p => p.ThanhTienX) : kq.Where(p => p.KieuDon == arrKD[1]).Sum(p => p.ThanhTienX),
                              SL3 = kq.Key.KieuDon == 1 || kq.Key.KieuDon == 2 ? kq.Where(p => p.KieuDon == arrKD[2] && p.MaKPnx == arrKP[2]).Sum(p => p.SoLuongX) : kq.Where(p => p.KieuDon == arrKD[2]).Sum(p => p.SoLuongX),
                              TT3 = kq.Key.KieuDon == 1 || kq.Key.KieuDon == 2 ? kq.Where(p => p.KieuDon == arrKD[2] && p.MaKPnx == arrKP[2]).Sum(p => p.ThanhTienX) : kq.Where(p => p.KieuDon == arrKD[2]).Sum(p => p.ThanhTienX),
                              SL4 = kq.Key.KieuDon == 1 || kq.Key.KieuDon == 2 ? kq.Where(p => p.KieuDon == arrKD[3] && p.MaKPnx == arrKP[3]).Sum(p => p.SoLuongX) : kq.Where(p => p.KieuDon == arrKD[3]).Sum(p => p.SoLuongX),
                              TT4 = kq.Key.KieuDon == 1 || kq.Key.KieuDon == 2 ? kq.Where(p => p.KieuDon == arrKD[3] && p.MaKPnx == arrKP[3]).Sum(p => p.ThanhTienX) : kq.Where(p => p.KieuDon == arrKD[3]).Sum(p => p.ThanhTienX),
                              SL5 = kq.Key.KieuDon == 1 || kq.Key.KieuDon == 2 ? kq.Where(p => p.KieuDon == arrKD[4] && p.MaKPnx == arrKP[4]).Sum(p => p.SoLuongX) : kq.Where(p => p.KieuDon == arrKD[4]).Sum(p => p.SoLuongX),
                              TT5 = kq.Key.KieuDon == 1 || kq.Key.KieuDon == 2 ? kq.Where(p => p.KieuDon == arrKD[4] && p.MaKPnx == arrKP[4]).Sum(p => p.ThanhTienX) : kq.Where(p => p.KieuDon == arrKD[4]).Sum(p => p.ThanhTienX),
                              SL6 = kq.Key.KieuDon == 1 || kq.Key.KieuDon == 2 ? kq.Where(p => p.KieuDon == arrKD[5] && p.MaKPnx == arrKP[5]).Sum(p => p.SoLuongX) : kq.Where(p => p.KieuDon == arrKD[5]).Sum(p => p.SoLuongX),
                              TT6 = kq.Key.KieuDon == 1 || kq.Key.KieuDon == 2 ? kq.Where(p => p.KieuDon == arrKD[5] && p.MaKPnx == arrKP[5]).Sum(p => p.ThanhTienX) : kq.Where(p => p.KieuDon == arrKD[5]).Sum(p => p.ThanhTienX),
                              SL7 = kq.Key.KieuDon == 1 || kq.Key.KieuDon == 2 ? kq.Where(p => p.KieuDon == arrKD[6] && p.MaKPnx == arrKP[6]).Sum(p => p.SoLuongX) : kq.Where(p => p.KieuDon == arrKD[6]).Sum(p => p.SoLuongX),
                              TT7 = kq.Key.KieuDon == 1 || kq.Key.KieuDon == 2 ? kq.Where(p => p.KieuDon == arrKD[6] && p.MaKPnx == arrKP[6]).Sum(p => p.ThanhTienX) : kq.Where(p => p.KieuDon == arrKD[6]).Sum(p => p.ThanhTienX),
                              SL8 = kq.Key.KieuDon == 1 || kq.Key.KieuDon == 2 ? kq.Where(p => p.KieuDon == arrKD[7] && p.MaKPnx == arrKP[7]).Sum(p => p.SoLuongX) : kq.Where(p => p.KieuDon == arrKD[7]).Sum(p => p.SoLuongX),
                              TT8 = kq.Key.KieuDon == 1 || kq.Key.KieuDon == 2 ? kq.Where(p => p.KieuDon == arrKD[7] && p.MaKPnx == arrKP[7]).Sum(p => p.ThanhTienX) : kq.Where(p => p.KieuDon == arrKD[7]).Sum(p => p.ThanhTienX),
                              SL9 = kq.Key.KieuDon == 1 || kq.Key.KieuDon == 2 ? kq.Where(p => p.KieuDon == arrKD[8] && p.MaKPnx == arrKP[8]).Sum(p => p.SoLuongX) : kq.Where(p => p.KieuDon == arrKD[8]).Sum(p => p.SoLuongX),
                              SL10 = kq.Key.KieuDon == 1 || kq.Key.KieuDon == 2 ? kq.Where(p => p.KieuDon == arrKD[9] && p.MaKPnx == arrKP[9]).Sum(p => p.SoLuongX) : kq.Where(p => p.KieuDon == arrKD[9]).Sum(p => p.SoLuongX),
                              SL11 = kq.Key.KieuDon == 1 || kq.Key.KieuDon == 2 ? kq.Where(p => p.KieuDon == arrKD[10] && p.MaKPnx == arrKP[10]).Sum(p => p.SoLuongX) : kq.Where(p => p.KieuDon == arrKD[10]).Sum(p => p.SoLuongX),
                              SL12 = kq.Key.KieuDon == 1 || kq.Key.KieuDon == 2 ? kq.Where(p => p.KieuDon == arrKD[11] && p.MaKPnx == arrKP[11]).Sum(p => p.SoLuongX) : kq.Where(p => p.KieuDon == arrKD[11]).Sum(p => p.SoLuongX),
                              SL13 = kq.Key.KieuDon == 1 || kq.Key.KieuDon == 2 ? kq.Where(p => p.KieuDon == arrKD[12] && p.MaKPnx == arrKP[12]).Sum(p => p.SoLuongX) : kq.Where(p => p.KieuDon == arrKD[12]).Sum(p => p.SoLuongX),
                              SL14 = kq.Key.KieuDon == 1 || kq.Key.KieuDon == 2 ? kq.Where(p => p.KieuDon == arrKD[13] && p.MaKPnx == arrKP[13]).Sum(p => p.SoLuongX) : kq.Where(p => p.KieuDon == arrKD[13]).Sum(p => p.SoLuongX),
                              SL15 = kq.Key.KieuDon == 1 || kq.Key.KieuDon == 2 ? kq.Where(p => p.KieuDon == arrKD[14] && p.MaKPnx == arrKP[14]).Sum(p => p.SoLuongX) : kq.Where(p => p.KieuDon == arrKD[14]).Sum(p => p.SoLuongX),
                              SL16 = kq.Key.KieuDon == 1 || kq.Key.KieuDon == 2 ? kq.Where(p => p.KieuDon == arrKD[15] && p.MaKPnx == arrKP[15]).Sum(p => p.SoLuongX) : kq.Where(p => p.KieuDon == arrKD[15]).Sum(p => p.SoLuongX),
                              SL17 = kq.Key.KieuDon == 1 || kq.Key.KieuDon == 2 ? kq.Where(p => p.KieuDon == arrKD[16] && p.MaKPnx == arrKP[16]).Sum(p => p.SoLuongX) : kq.Where(p => p.KieuDon == arrKD[16]).Sum(p => p.SoLuongX),
                              SL18 = kq.Key.KieuDon == 1 || kq.Key.KieuDon == 2 ? kq.Where(p => p.KieuDon == arrKD[17] && p.MaKPnx == arrKP[17]).Sum(p => p.SoLuongX) : kq.Where(p => p.KieuDon == arrKD[17]).Sum(p => p.SoLuongX),
                          }).ToList();
                var NXT = (from a in nx
                           select new
                           {
                               a.MaCC,
                               a.MaDV,
                               a.TenDV,
                               a.DonVi,
                               a.DonGia,
                               SL1 = a.SL1,
                               TT1 = a.TT1,
                               SL2 = a.SL2,
                               TT2 = a.TT2,
                               SL3 = a.SL3,
                               TT3 = a.TT3,
                               SL4 = a.SL4,
                               TT4 = a.TT4,
                               SL5 = a.SL5,
                               TT5 = a.TT5,
                               SL6 = a.SL6,
                               TT6 = a.TT6,
                               SL7 = a.SL7,
                               TT7 = a.TT7,
                               SL8 = a.SL8,
                               TT8 = a.TT8,
                               SL9 = a.SL9,
                               SL10 = a.SL10,
                               SL11 = a.SL11,
                               SL12 = a.SL12,
                               SL13 = a.SL13,
                               SL14 = a.SL14,
                               SL15 = a.SL15,
                               SL16 = a.SL16,
                               SL17 = a.SL17,
                               SL18 = a.SL18,

                               SL = a.SL1 + a.SL2 + a.SL3 + a.SL4 + a.SL5 + a.SL6 + a.SL7 + a.SL8 + a.SL9 + a.SL10 + a.SL11 + a.SL12 + a.SL13 + a.SL14 + a.SL15 + a.SL16 + a.SL17 + a.SL18,
                               SLT = a.SL1 + a.SL2 + a.SL3 + a.SL4 + a.SL5 + a.SL6 + a.SL7 + a.SL8 ,
                               TT = a.TT1 + a.TT2 + a.TT3 + a.TT4 + a.TT5 + a.TT6 + a.TT7 + a.TT8 ,
                           }).Where(p => p.TT != null).ToList();
                #endregion
                #region In Báo cáo
                if (chkInTT.Checked == false)
                {
                    BaoCao.Rep_BcXuatTheoPLoai rep = new BaoCao.Rep_BcXuatTheoPLoai();
                    rep.TuNgayDenNgay.Value = "Từ ngày " + tungay.ToString().Substring(0, 10) + " đến ngày " + denngay.ToString().Substring(0, 10);
                    if (qtenkho.Count > 0)
                    { rep.Kho.Value = "Kho: " + qtenkho.First().TenKP; }
                    if (qtenncc.Count > 0)
                    { rep.NhaCC.Value = "Nhà cung cấp: " + qtenncc.First().TenCC; }
                    for (int i = 1; i < colcount; i++)
                    {
                        switch (i)
                        {
                            case 1:
                                rep.Ten1.Value = dskho[i - 1].TenKhoa1.ToString();
                                break;
                            case 2:
                                rep.Ten2.Value = dskho[i - 1].TenKhoa1.ToString();
                                break;
                            case 3:
                                rep.Ten3.Value = dskho[i - 1].TenKhoa1.ToString();
                                break;
                            case 4:
                                rep.Ten4.Value = dskho[i - 1].TenKhoa1.ToString();
                                break;
                            case 5:
                                rep.Ten5.Value = dskho[i - 1].TenKhoa1.ToString();
                                break;
                            case 6:
                                rep.Ten6.Value = dskho[i - 1].TenKhoa1.ToString();
                                break;
                            case 7:
                                rep.Ten7.Value = dskho[i - 1].TenKhoa1.ToString();
                                break;
                            case 8:
                                rep.Ten8.Value = dskho[i - 1].TenKhoa1.ToString();
                                break;
                            case 9:
                                rep.Ten9.Value = dskho[i - 1].TenKhoa1.ToString();
                                break;
                            case 10:
                                rep.Ten10.Value = dskho[i - 1].TenKhoa1.ToString();
                                break;
                            case 11:
                                rep.Ten11.Value = dskho[i - 1].TenKhoa1.ToString();
                                break;
                            case 12:
                                rep.Ten12.Value = dskho[i - 1].TenKhoa1.ToString();
                                break;
                            case 13:
                                rep.Ten13.Value = dskho[i - 1].TenKhoa1.ToString();
                                break;
                            case 14:
                                rep.Ten14.Value = dskho[i - 1].TenKhoa1.ToString();
                                break;
                            case 15:
                                rep.Ten15.Value = dskho[i - 1].TenKhoa1.ToString();
                                break;
                            case 16:
                                rep.Ten16.Value = dskho[i - 1].TenKhoa1.ToString();
                                break;
                            case 17:
                                rep.Ten17.Value = dskho[i - 1].TenKhoa1.ToString();
                                break;
                            case 18:
                                rep.Ten18.Value = dskho[i - 1].TenKhoa1.ToString();
                                break;
                        }
                    }
                    if (!string.IsNullOrEmpty(_nhacc))
                    { rep.DataSource = NXT.Where(p => p.MaCC == _nhacc); }
                    else
                    {
                        rep.DataSource = NXT;
                    }
                    rep.BindingData();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();

                }

                else
                {
                    

                    if (dskho.Count > 8)
                    {
                        BaoCao.Rep_BCXuatTheoPLoai_A3 rep = new BaoCao.Rep_BCXuatTheoPLoai_A3();
                        rep.TuNgayDenNgay.Value = "Từ ngày " + tungay.ToString().Substring(0, 10) + " đến ngày " + denngay.ToString().Substring(0, 10);
                        if (qtenkho.Count > 0)
                        { rep.Kho.Value = "Kho: " + qtenkho.First().TenKP; }
                        if (qtenncc.Count > 0)
                        { rep.NhaCC.Value = "Nhà cung cấp: " + qtenncc.First().TenCC; }
                        //MessageBox.Show("Dữ liệu vượt giới hạn, báo cáo chỉ hiển thị tối đa 8 khoa phòng!");
                        for (int i = 1; i < colcount; i++)
                        {
                            switch (i)
                            {
                                case 1:
                                    rep.T1.Value = dskho[i - 1].TenKhoa1.ToString();
                                    break;
                                case 2:
                                    rep.T2.Value = dskho[i - 1].TenKhoa1.ToString();
                                    break;
                                case 3:
                                    rep.T3.Value = dskho[i - 1].TenKhoa1.ToString();
                                    break;
                                case 4:
                                    rep.T4.Value = dskho[i - 1].TenKhoa1.ToString();
                                    break;
                                case 5:
                                    rep.T5.Value = dskho[i - 1].TenKhoa1.ToString();
                                    break;
                                case 6:
                                    rep.T6.Value = dskho[i - 1].TenKhoa1.ToString();
                                    break;
                                case 7:
                                    rep.T7.Value = dskho[i - 1].TenKhoa1.ToString();
                                    break;
                                case 8:
                                    rep.T8.Value = dskho[i - 1].TenKhoa1.ToString();
                                    break;
                                case 9:
                                    rep.T9.Value = dskho[i - 1].TenKhoa1.ToString();
                                    break;
                                case 10:
                                    rep.T10.Value = dskho[i - 1].TenKhoa1.ToString();
                                    break;
                                case 11:
                                    rep.T11.Value = dskho[i - 1].TenKhoa1.ToString();
                                    break;
                                case 12:
                                    rep.T12.Value = dskho[i - 1].TenKhoa1.ToString();
                                    break;
                                case 13:
                                    rep.T13.Value = dskho[i - 1].TenKhoa1.ToString();
                                    break;
                            }
                        }
                        if (!string.IsNullOrEmpty(_nhacc))
                        { rep.DataSource = NXT.Where(p => p.MaCC == _nhacc); }
                        else
                        { rep.DataSource = NXT; }
                        rep.BindingData();
                        rep.CreateDocument();
                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                        frm.ShowDialog();
                    }
                    else
                    {
                        BaoCao.Rep_BcXuatTheoPLoai_BX rep = new BaoCao.Rep_BcXuatTheoPLoai_BX();
                        rep.TuNgayDenNgay.Value = "Từ ngày " + tungay.ToString().Substring(0, 10) + " đến ngày " + denngay.ToString().Substring(0, 10);
                        if (qtenkho.Count > 0)
                        { rep.Kho.Value = "Kho: " + qtenkho.First().TenKP; }
                        if (qtenncc.Count > 0)
                        { rep.NhaCC.Value = "Nhà cung cấp: " + qtenncc.First().TenCC; }
                        for (int i = 1; i < colcount; i++)
                        {
                            switch (i)
                            {
                                case 1:
                                    rep.Ten1.Value = dskho[i - 1].TenKhoa1.ToString();
                                    break;
                                case 2:
                                    rep.Ten2.Value = dskho[i - 1].TenKhoa1.ToString();
                                    break;
                                case 3:
                                    rep.Ten3.Value = dskho[i - 1].TenKhoa1.ToString();
                                    break;
                                case 4:
                                    rep.Ten4.Value = dskho[i - 1].TenKhoa1.ToString();
                                    break;
                                case 5:
                                    rep.Ten5.Value = dskho[i - 1].TenKhoa1.ToString();
                                    break;
                                case 6:
                                    rep.Ten6.Value = dskho[i - 1].TenKhoa1.ToString();
                                    break;
                                case 7:
                                    rep.Ten7.Value = dskho[i - 1].TenKhoa1.ToString();
                                    break;
                                case 8:
                                    rep.Ten8.Value = dskho[i - 1].TenKhoa1.ToString();
                                    break;
                            }
                        }
                        if (!string.IsNullOrEmpty(_nhacc))
                        { rep.DataSource = NXT.Where(p => p.MaCC == _nhacc); }
                        else
                        { rep.DataSource = NXT; }
                        rep.BindingData();
                        rep.CreateDocument();
                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                        frm.ShowDialog();
                    }

                }
                #endregion
            }
        }

        private void chkPL_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}