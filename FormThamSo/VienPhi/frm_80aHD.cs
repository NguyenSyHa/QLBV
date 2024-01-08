using System;using QLBV_Database;
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
    public partial class frm_80aHD : DevExpress.XtraEditors.XtraForm
    {
        public frm_80aHD()
        {
            InitializeComponent();
            rdFont.Enabled = false;
        }
        private int Font = 0;
        private static char[] arrTCVN = {'µ','¸','¶','·','¹','¨', '»', '¾', '¼', '½', 'Æ','©', 'Ç', 'Ê', 'È', 'É', 'Ë', 
                                         '®', 'Ì', 'Ð', 'Î', 'Ï', 'Ñ','ª', 'Ò', 'Õ', 'Ó', 'Ô', 'Ö','×', 'Ý', 'Ø', 'Ü', 'Þ', 
                                         'ß', 'ã', 'á', 'â', 'ä','«', 'å', 'è', 'æ', 'ç', 'é','¬', 'ê', 'í', 'ë', 'ì', 'î','ï',
                                         'ó', 'ñ', 'ò', 'ô','­', 'õ', 'ø', 'ö', '÷', 'ù','ú', 'ý', 'û', 'ü', 'þ','¡', '¢', '§', '£', '¤', '¥', '¦'
                                        };
        private static char[] arrUnicode = {'à', 'á', 'ả', 'ã', 'ạ','ă', 'ằ', 'ắ', 'ẳ', 'ẵ', 'ặ','â', 'ầ', 'ấ', 'ẩ', 'ẫ', 'ậ','đ', 'è', 'é', 'ẻ', 'ẽ', 'ẹ', 
                                           'ê', 'ề', 'ế', 'ể', 'ễ', 'ệ','ì', 'í', 'ỉ', 'ĩ', 'ị','ò', 'ó', 'ỏ', 'õ', 'ọ','ô', 'ồ', 'ố', 'ổ', 'ỗ', 'ộ', 
                                          'ơ', 'ờ', 'ớ', 'ở', 'ỡ', 'ợ','ù', 'ú', 'ủ', 'ũ', 'ụ','ư', 'ừ', 'ứ', 'ử', 'ữ', 'ự','ỳ', 'ý', 'ỷ', 'ỹ', 'ỵ','Ă', 'Â', 'Đ', 'Ê', 'Ô', 'Ơ', 'Ư'
                                        };
        private static Char[] Converter;

        private String convert(String str)
        {
            bool tt = false;
            Converter = new char[str.Length];
            Char[] arrStr = str.ToCharArray();
            for (int i = 0; i < arrStr.Length; i++)
            {
                for (int j = 0; j < arrUnicode.Length; j++)
                {
                    if (arrStr[i]== (arrUnicode[j]))
                    {
                        Converter[i] = arrTCVN[j];
                        tt = true;
                        break;
                    }
                }
                if (tt == false)
                {
                    Converter[i] = arrStr[i];
                }
                tt = false;
            }
            return new String(Converter);
        }
        private String convertFont(String str) 
        {
            String result = "";
            if (Font == 0)
            {
                result = convert(str);
            }
            else
                result = str;
            return result;
        }
        int idThuoc = -1, idMau = -1, idXN = -1, idCDHA = -1, idTTPT = -1, idCongKham = -1, idDVKTC = -1,
            idVTYT = -1, idNgayGiuong = -1, idChiPhiVC = -1, idVTTT = -1, idThuocUngThuCTG = -1, idHoaChat = -1;
        private void setIDNhom()
        {

            QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var tenNhom = _data.NhomDVs.Select(a => new { a.IDNhom, a.TenNhomCT }).ToList();
            foreach (var item in tenNhom)
            {
                switch (item.TenNhomCT)
                {
                    case  "Thuốc trong danh mục BHYT" :
                        idThuoc = item.IDNhom;
                        break;
                    case "Máu và chế phẩm của máu":
                        idMau = item.IDNhom;
                        break;
                    case "Xét nghiệm":
                        idXN = item.IDNhom;
                        break;
                    case "Chẩn đoán hình ảnh":
                        idCDHA = item.IDNhom;
                        break;
                    case "Thủ thuật, phẫu thuật":
                        idTTPT = item.IDNhom;
                        break;
                    case "Khám bệnh":
                        idCongKham = item.IDNhom;
                        break;
                    case "DVKT thanh toán theo tỷ lệ":
                        idDVKTC = item.IDNhom;
                        break;
                    case"Vật tư y tế trong danh mục BHYT":
                        idVTYT = item.IDNhom;
                        break;
                    case "Giường điều trị nội trú":
                        idNgayGiuong = item.IDNhom;
                        break;
                    case "Vận chuyển":
                        idChiPhiVC = item.IDNhom;
                        break;
                    case"VTYT thanh toán theo tỷ lệ":
                        idVTTT = item.IDNhom;
                        break;
                    case "Thuốc điều trị ung thư, chống thải ghép ngoài danh mục":
                        idThuocUngThuCTG = item.IDNhom;
                        break;
                    case "Nhóm hóa chất":
                        idHoaChat = item.IDNhom;
                        break;

                }

            }
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            setIDNhom();
            QLBV_Database.QLBVEntities _dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            string DoiTuongKham = "BHYT";
            if (chkNhandan.Checked == true)
                DoiTuongKham = "Dịch vụ";
            if (kt())
            {
                if (radTimKiem.SelectedIndex == 0)
                {
                    if (cbosx.SelectedIndex == 0 || cbosx.SelectedIndex == 1)
                    {
                        DateTime ngaytu = System.DateTime.Now.Date;
                        DateTime ngayden = System.DateTime.Now.Date;
                        ngaytu = DungChung.Ham.NgayTu(lupNgaytu.DateTime);
                        ngayden = DungChung.Ham.NgayDen(lupngayden.DateTime);
                        var q = (from bn in _dataContext.BenhNhans.Where(p => p.DTuong == DoiTuongKham).Where(p => p.NoiTru == 1)
                                 join vp in _dataContext.VienPhis on bn.MaBNhan equals vp.MaBNhan
                                 join vpct in _dataContext.VienPhicts.Where(p => p.TrongBH == 1) on vp.idVPhi equals vpct.idVPhi
                                 join dv in _dataContext.DichVus on vpct.MaDV equals dv.MaDV
                                 join vv in _dataContext.VaoViens on bn.MaBNhan equals vv.MaBNhan
                                 join rv in _dataContext.RaViens.Where(p => p.NgayRa >= ngaytu).Where(p => p.NgayRa <= ngayden) on bn.MaBNhan equals rv.MaBNhan
                                 group new { bn, rv, dv, vpct, vp } by new { vp.NgayTT, bn.NoiTinh, vp.MaBNhan, rv.MaKP, vv.NgayVao, bn.TenBNhan, bn.NamSinh, bn.GTinh, bn.SThe, bn.DChi, bn.MaCS, bn.Tuyen, bn.HanBHTu, bn.HanBHDen, rv.NgayRa, rv.SoNgaydt, rv.MaICD, } into kq
                                 select new
                                 {
                                     dt = kq.Key.SThe.Substring(0, 2),
                                     //kq.Key.MaBNhan,
                                     NoiTinh = kq.Key.NoiTinh,
                                     Tuyen = kq.Key.Tuyen,
                                     Makp = kq.Key.MaKP,
                                     MaBNhan = kq.Key.MaBNhan,
                                     TenBNhan = kq.Key.TenBNhan,
                                     NSinh = kq.Key.NamSinh,
                                     SThe = kq.Key.SThe,
                                     Nam = kq.Key.GTinh,
                                     GTinh = kq.Key.GTinh,
                                     MaCS = kq.Key.MaCS,
                                     MaICD = kq.Key.MaICD,
                                     Ngaykham = kq.Key.NgayVao,
                                     Ngayvao = kq.Key.NgayVao,
                                     Ngayra2 = kq.Key.NgayRa,
                                     Ngayra = kq.Key.NgayRa,
                                     Songay = kq.Key.SoNgaydt,
                                     BHtu = kq.Key.HanBHTu,
                                     BHden = kq.Key.HanBHDen,
                                     Diachi = kq.Key.DChi,
                                     Mabn = kq.Key.MaBNhan,
                                     Thuoc = kq.Where(p => p.dv.IDNhom == idThuoc).Sum(p => p.vpct.ThanhTien),
                                     CDHA = kq.Where(p => p.dv.IDNhom == idCDHA).Sum(p => p.vpct.ThanhTien),
                                     Congkham = kq.Where(p => p.dv.IDNhom == idNgayGiuong).Sum(p => p.vpct.ThanhTien),
                                     Xetnghiem = kq.Where(p => p.dv.IDNhom == idXN).Sum(p => p.vpct.ThanhTien),
                                     Mau = kq.Where(p => p.dv.IDNhom == idMau).Sum(p => p.vpct.ThanhTien),
                                     TTPT = kq.Where(p => p.dv.IDNhom == idTTPT).Sum(p => p.vpct.ThanhTien),
                                     VTYT = kq.Where(p => p.dv.IDNhom == idVTYT).Sum(p => p.vpct.ThanhTien),
                                     DVKTC = kq.Where(p => p.dv.IDNhom == idDVKTC).Sum(p => p.vpct.ThanhTien),
                                     ThuocKTCG = kq.Where(p => p.dv.IDNhom == idThuocUngThuCTG).Sum(p => p.vpct.ThanhTien),
                                     CPVanchuyen = kq.Where(p => p.dv.IDNhom == idChiPhiVC).Sum(p => p.vpct.ThanhTien),
                                     ThanhTien = kq.Sum(p => p.vpct.ThanhTien),
                                     Tongchi = kq.Sum(p => p.vpct.ThanhTien),
                                     Tongcong = kq.Sum(p => p.vpct.ThanhTien),
                                     Nguoibenhchitra = kq.Sum(p => p.vpct.TienBN),
                                     TongcongBHYT = kq.Sum(p => p.vpct.TienBH),

                                 }).ToList().Select(x => new
                                 {
                                     dt = x.SThe.Substring(0, 2),
                                    // x.MaBNhan,
                                     NoiTinh = x.NoiTinh,
                                     Tuyen = x.Tuyen,
                                     Makp = x.Makp,
                                     MaBNhan = x.MaBNhan,
                                     TenBNhan = x.TenBNhan,
                                     NSinh = x.NSinh,
                                     SThe = x.SThe,
                                     Nam = x.GTinh,
                                     GTinh = x.GTinh,
                                     MaCS = x.MaCS,
                                     MaICD = x.MaICD,
                                     Ngaykham = x.Ngayvao,
                                     Ngayvao = x.Ngayvao,
                                     Ngayra = x.Ngayra,
                                     Ngayra2 = x.Ngayra.Value.Date,
                                     Songay = x.Songay,
                                     //TenKP = kq.Key.TenKP,
                                     BHtu = x.BHtu,
                                     BHden = x.BHden,
                                     Diachi = x.Diachi,
                                     Mabn = x.MaBNhan,
                                     // Ngaykham = kq.Key.NgayKham.Value.Day,
                                     Thuoc = x.Thuoc,
                                     x.CDHA,
                                     x.Congkham,
                                     x.Xetnghiem,
                                     x.Mau,
                                     x.TTPT,
                                     x.VTYT,
                                     x.DVKTC,
                                     x.ThuocKTCG,
                                     x.CPVanchuyen,
                                     x.ThanhTien,
                                     x.Tongchi,
                                     x.Tongcong,
                                     x.Nguoibenhchitra,
                                     x.TongcongBHYT,
                                 }).ToList();
                        if (string.IsNullOrEmpty(lupKhoaphong.Text))
                        {
                            frmIn frm = new frmIn();
                            BaoCao.rep80aHD rep = new BaoCao.rep80aHD();
                            if (cbosx.SelectedIndex == 0)
                            {
                                q = q.OrderBy(p => p.Mabn).ThenBy(p => p.NoiTinh).ThenBy(p => p.Tuyen).ToList();
                                rep.DataSource = q.ToList();
                            }
                            else
                            {
                                if (cbosx.SelectedIndex == 1)
                                {
                                    q = q.OrderBy(p => p.NoiTinh).ThenBy(p => p.Tuyen).ThenBy(p => p.Ngayra2).ThenBy(p => p.dt).ToList();
                                    rep.DataSource = q;
                                }
                                else
                                {
                                    // cần join đến bảng đối tượng
                                    q = q.OrderBy(p => p.NoiTinh).ThenBy(p => p.dt).ThenBy(p => p.Ngayra).ToList();
                                    rep.DataSource = q.ToList();
                                }
                            }
                            rep.Ngaythang.Value = theoquy();
                            rep.MaCS.Value = DungChung.Bien.MaBV;
                            rep.TenCQ.Value = DungChung.Bien.TenCQ.ToUpper();
                            double st = 0;
                            st = q.Sum(a => a.TongcongBHYT);
                            rep.Sotien.Value = QLBV_Library.QLBV_Ham.DocTienBangChu(st, " đồng.");
                            rep.BindingData();
                            rep.CreateDocument();
                            // rep.DataMember = "Table";
                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                            frm.ShowDialog();
                            #region xuat Excel
                            if (Xuatex.Checked)
                            {

                                COMExcel.Application exApp = new COMExcel.Application();
                                COMExcel.Workbook exQLBV = exApp.Workbooks.Add(
                                          COMExcel.XlWBATemplate.xlWBATWorksheet);
                                COMExcel.Worksheet exSheet = (COMExcel.Worksheet)exQLBV.Worksheets[1];
                                //exSheet.Activate();
                                exSheet.Name = "NoiTru";// gán tên sheet
                                int i = 1;
                                string[] _arr = new string[44] { "stt", "hoten", "namsinh", "gioitinh", "mathe", "ma_dkbd", "mabenh", "ngay_vao", "ngay_ra", "ngaydtr", "t_tongchi", "t_xn", "t_cdha", "t_thuoc", "t_mau", "t_pttt", "t_vtytth", "t_vtyttt", "t_dvktc", "t_ktg", "t_kham", "t_vchuyen", "t_bnct", "t_bhtt", "t_ngoaids", "lydo_vv", "benhkhac", "noikcb", "khoa", "thang_qt", "nam_qt", "gt_tu", "gt_den", "diachi", "giamdinh", "t_xuattoan", "lydo_xt", "t_datuyen", "t_vuottran", "loaikcb", "noi_ttoan", "sophieu", "ma_khoa", "keysl" };
                                int k = 0;
                                foreach (var b in _arr)
                                {
                                    k++;
                                    COMExcel.Range r = (COMExcel.Range)exSheet.Cells[1, k];
                                    r.Value2 = b.ToString();
                                    r.Columns.AutoFit();
                                }
                                foreach (var a in q)
                                {
                                    i++;
                                    COMExcel.Range r1 = (COMExcel.Range)exSheet.Cells[i, 1];
                                    r1.NumberFormat = "0";
                                    r1.Value2 = i - 1;
                                    r1.Columns.AutoFit();
                                    COMExcel.Range r2 = (COMExcel.Range)exSheet.Cells[i, 2];
                                    r2.NumberFormat = "@";
                                    if (Font == 0)
                                    {
                                        r2.Value2 = convert(a.TenBNhan);
                                    }
                                    else
                                        r2.Value2 = a.TenBNhan;
                                    r2.Columns.AutoFit();
                                    COMExcel.Range r3 = (COMExcel.Range)exSheet.Cells[i, 3];
                                    r3.NumberFormat = "0";
                                    r3.Value2 = a.NSinh;
                                    r3.Columns.AutoFit();
                                    COMExcel.Range r4 = (COMExcel.Range)exSheet.Cells[i, 4];
                                    r4.NumberFormat = "0";
                                    r4.Value2 = a.GTinh;
                                    r4.Columns.AutoFit();
                                    COMExcel.Range r5 = (COMExcel.Range)exSheet.Cells[i, 5];
                                    r5.NumberFormat = "@";
                                    r5.Value2 = convertFont(a.SThe);
                                    r5.Columns.AutoFit();
                                    COMExcel.Range r6 = (COMExcel.Range)exSheet.Cells[i, 6];
                                    r6.NumberFormat = "@";
                                    r6.Value2 = convertFont(a.MaCS);
                                    r6.Columns.AutoFit();
                                    COMExcel.Range r7 = (COMExcel.Range)exSheet.Cells[i, 7];
                                    r7.NumberFormat = "@";
                                    r7.Value2 = convertFont(a.MaICD);
                                    r7.Columns.AutoFit();
                                    COMExcel.Range r8 = (COMExcel.Range)exSheet.Cells[i, 8];
                                    r8.NumberFormat = "@";
                                    if (a.Ngayvao != null)
                                    {
                                        r8.Value2 = convertFont(a.Ngayvao.ToString().Substring(0, 10));
                                    }

                                    r8.Columns.AutoFit();
                                    COMExcel.Range r9 = (COMExcel.Range)exSheet.Cells[i, 9];
                                    r9.NumberFormat = "@";
                                    if (a.Ngayra != null)
                                    {
                                        r9.Value2 = convertFont(a.Ngayra.ToString().Substring(0, 10));
                                    }

                                    r9.Columns.AutoFit();
                                    COMExcel.Range r10 = (COMExcel.Range)exSheet.Cells[i, 10];
                                    r10.NumberFormat = "0";
                                    r10.Value2 = a.Songay;
                                    r10.Columns.AutoFit();
                                    COMExcel.Range r11 = (COMExcel.Range)exSheet.Cells[i, 11];
                                    r11.NumberFormat = "0";
                                    r11.Value2 = a.Tongcong;
                                    r11.Columns.AutoFit();
                                    COMExcel.Range r12 = (COMExcel.Range)exSheet.Cells[i, 12];
                                    r12.NumberFormat = "0";
                                    r12.Value2 = a.Xetnghiem;
                                    r12.Columns.AutoFit();
                                    COMExcel.Range r13 = (COMExcel.Range)exSheet.Cells[i, 13];
                                    r13.NumberFormat = "0";
                                    r13.Value2 = a.CDHA;
                                    r13.Columns.AutoFit();
                                    COMExcel.Range r14 = (COMExcel.Range)exSheet.Cells[i, 14];
                                    r14.NumberFormat = "0";
                                    r14.Value2 = a.Thuoc;
                                    r14.Columns.AutoFit();
                                    COMExcel.Range r15 = (COMExcel.Range)exSheet.Cells[i, 15];
                                    r15.NumberFormat = "0";
                                    r15.Value2 = a.Mau;
                                    r15.Columns.AutoFit();
                                    COMExcel.Range r16 = (COMExcel.Range)exSheet.Cells[i, 16];
                                    r16.NumberFormat = "0";
                                    r16.Value2 = a.TTPT;
                                    r16.Columns.AutoFit();
                                    COMExcel.Range r17 = (COMExcel.Range)exSheet.Cells[i, 17];
                                    r17.NumberFormat = "0";
                                    r17.Value2 = a.VTYT;
                                    r17.Columns.AutoFit();
                                    COMExcel.Range r18 = (COMExcel.Range)exSheet.Cells[i, 18];
                                    r18.NumberFormat = "0";
                                    r18.Value2 = 0;
                                    r18.Columns.AutoFit();
                                    COMExcel.Range r19 = (COMExcel.Range)exSheet.Cells[i, 19];
                                    r19.NumberFormat = "0";
                                    r19.Value2 = a.DVKTC;
                                    r19.Columns.AutoFit();
                                    COMExcel.Range r20 = (COMExcel.Range)exSheet.Cells[i, 20];
                                    r20.NumberFormat = "0";
                                    r20.Value2 = a.ThuocKTCG;
                                    r20.Columns.AutoFit();
                                    COMExcel.Range r21 = (COMExcel.Range)exSheet.Cells[i, 21];
                                    r21.NumberFormat = "0";
                                    r21.Value2 = a.Congkham;
                                    r21.Columns.AutoFit();
                                    COMExcel.Range r22 = (COMExcel.Range)exSheet.Cells[i, 22];
                                    r22.NumberFormat = "0";
                                    r22.Value2 = a.CPVanchuyen;
                                    r22.Columns.AutoFit();
                                    COMExcel.Range r23 = (COMExcel.Range)exSheet.Cells[i, 23];
                                    r23.NumberFormat = "0";
                                    r23.Value2 = a.Nguoibenhchitra;
                                    r23.Columns.AutoFit();
                                    COMExcel.Range r24 = (COMExcel.Range)exSheet.Cells[i, 24];
                                    r24.NumberFormat = "0";
                                    r24.Value2 = a.TongcongBHYT;
                                    r24.Columns.AutoFit();
                                    COMExcel.Range r25 = (COMExcel.Range)exSheet.Cells[i, 25];
                                    r25.NumberFormat = "0";
                                    r25.Value2 = a.CPVanchuyen;
                                    r25.Columns.AutoFit();
                                    COMExcel.Range r26 = (COMExcel.Range)exSheet.Cells[i, 26];
                                    r26.NumberFormat = "@";
                                    if (a.Tuyen != null && a.Tuyen == 1)
                                    {
                                        r26.Value2 = 1;
                                    }
                                    else
                                        r26.Value2 = 0;

                                    r26.Columns.AutoFit();
                                    COMExcel.Range r27 = (COMExcel.Range)exSheet.Cells[i, 27];
                                    r27.NumberFormat = "@";
                                    r27.Value2 = "";
                                    r27.Columns.AutoFit();
                                    COMExcel.Range r28 = (COMExcel.Range)exSheet.Cells[i, 28];
                                    r28.NumberFormat = "@";
                                    r28.Value2 = convertFont(DungChung.Bien.MaBV);
                                    r28.Columns.AutoFit();
                                    COMExcel.Range r29 = (COMExcel.Range)exSheet.Cells[i, 29];
                                    r29.NumberFormat = "@";
                                    var K = (from kh in _dataContext.KPhongs.Where(p => p.MaKP == a.Makp) select new { kh.TenKP }).ToList();
                                    if (K.Count > 0)
                                    {
                                        r29.Value2 = convertFont(K.First().TenKP);
                                    }
                                    r29.Columns.AutoFit();
                                    COMExcel.Range r30 = (COMExcel.Range)exSheet.Cells[i, 30];
                                    r30.NumberFormat = "0";
                                    if (a.Ngayra != null)
                                    {
                                        r30.Value2 = a.Ngayra.Value.Month;
                                        r30.Columns.AutoFit();
                                    }
                                    COMExcel.Range r31 = (COMExcel.Range)exSheet.Cells[i, 31];
                                    r31.NumberFormat = "0";
                                    if (a.Ngayra != null)
                                    {
                                        r31.Value2 = a.Ngayra.Value.Year;
                                        r31.Columns.AutoFit();
                                    }
                                    COMExcel.Range r32 = (COMExcel.Range)exSheet.Cells[i, 32];
                                    r32.NumberFormat = "@";
                                    if (a.BHtu != null)
                                    {
                                        r32.Value2 = convertFont(a.BHtu.ToString().Substring(0, 10));
                                        r32.Columns.AutoFit();
                                    }
                                    COMExcel.Range r33 = (COMExcel.Range)exSheet.Cells[i, 33];
                                    r33.NumberFormat = "@";
                                    if (a.BHden != null)
                                    {
                                        r33.Value2 = convertFont(a.BHden.ToString().Substring(0, 10));
                                        r33.Columns.AutoFit();
                                    }
                                    COMExcel.Range r34 = (COMExcel.Range)exSheet.Cells[i, 34];
                                    r34.NumberFormat = "@";
                                    r34.Value2 = convertFont(a.Diachi);
                                    r34.Columns.AutoFit();
                                    COMExcel.Range r35 = (COMExcel.Range)exSheet.Cells[i, 35];
                                    r35.Value2 = "";
                                    r35.Columns.AutoFit();
                                    COMExcel.Range r36 = (COMExcel.Range)exSheet.Cells[i, 36];
                                    r36.NumberFormat = "@";
                                    r36.Value2 = 0;
                                    r36.Columns.AutoFit();
                                    COMExcel.Range r37 = (COMExcel.Range)exSheet.Cells[i, 37];
                                    r37.NumberFormat = "@";
                                    r37.Value2 = "";
                                    r37.Columns.AutoFit();
                                    COMExcel.Range r38 = (COMExcel.Range)exSheet.Cells[i, 38];
                                    r38.NumberFormat = "@";
                                    r38.Value2 = 0;
                                    r38.Columns.AutoFit();
                                    COMExcel.Range r39 = (COMExcel.Range)exSheet.Cells[i, 39];
                                    r39.NumberFormat = "0";
                                    r39.Value2 = 0;
                                    r39.Columns.AutoFit();
                                    COMExcel.Range r40 = (COMExcel.Range)exSheet.Cells[i, 40];
                                    r40.NumberFormat = "@";
                                    r40.Value2 = convertFont("NOI");
                                    r40.Columns.AutoFit();
                                    COMExcel.Range r41 = (COMExcel.Range)exSheet.Cells[i, 41];
                                    r41.NumberFormat = "@";
                                    r41.Value2 = convertFont("CSKCB");
                                    r41.Columns.AutoFit();
                                    COMExcel.Range r42 = (COMExcel.Range)exSheet.Cells[i, 42];
                                    r42.NumberFormat = "@";
                                    r42.Value2 = a.Mabn.Value;
                                    r42.Columns.AutoFit();
                                    COMExcel.Range r43 = (COMExcel.Range)exSheet.Cells[i, 43];
                                    r43.NumberFormat = "@";
                                    var kp = (from k1 in _dataContext.KPhongs.Where(p => p.MaKP == a.Makp) select new { k1.TenKP }).ToList();
                                    if (kp.Count > 0)
                                    {
                                        r43.Value2 = convertFont(kp.First().TenKP);
                                    }
                                    else
                                    {
                                        r43.Value2 = a.Makp;
                                    }
                                    r43.Columns.AutoFit();
                                    COMExcel.Range r44 = (COMExcel.Range)exSheet.Cells[i, 44];
                                    r44.Value2 = "";
                                    r44.Columns.AutoFit();
                                }
                                exApp.Visible = true;//Ẩn hiện chương trình
                                exQLBV.SaveAs("C:\\Bieu80.xls", COMExcel.XlFileFormat.xlWorkbookNormal,
                                                null, null, false, false,
                                                COMExcel.XlSaveAsAccessMode.xlExclusive,
                                                false, false, false, false, false);
                                //exQLBV.Close(false, false, false);
                                //exApp.Quit(); // thoát ứng dụng
                                System.Runtime.InteropServices.Marshal.ReleaseComObject(exQLBV);
                                System.Runtime.InteropServices.Marshal.ReleaseComObject(exApp);
                            }
                            #endregion
                        }
                        else
                        {
                            int _MaKP = Convert.ToInt32(lupKhoaphong.EditValue);
                            frmIn frm = new frmIn();
                            BaoCao.rep80aHD rep = new BaoCao.rep80aHD();
                            q = q.ToList().Where(p => p.Makp == _MaKP).ToList();
                            rep.DataSource = q.ToList();
                            rep.Ngaythang.Value = theoquy();
                            double st = 
                            st = q.Where(p => p.Makp == _MaKP).Sum(p => p.TongcongBHYT);
                            rep.Sotien.Value = QLBV_Library.QLBV_Ham.DocTienBangChu(st, " đồng.");
                            rep.paramKhoaPhong.Value = lupKhoaphong.Text.ToUpper();
                            rep.BindingData();
                            rep.CreateDocument();
                            //rep.DataMember = "Table";
                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                            frm.ShowDialog();
                            #region xuat Excel
                            if (Xuatex.Checked)
                            {
                                COMExcel.Application exApp = new COMExcel.Application();
                                COMExcel.Workbook exQLBV = exApp.Workbooks.Add(
                                          COMExcel.XlWBATemplate.xlWBATWorksheet);
                                COMExcel.Worksheet exSheet = (COMExcel.Worksheet)exQLBV.Worksheets[1];
                                //exSheet.Activate();
                                exSheet.Name = "NoiTru";// gán tên sheet
                                int i = 1;
                                string[] _arr = new string[44] { "stt", "hoten", "namsinh", "gioitinh", "mathe", "ma_dkbd", "mabenh", "ngay_vao", "ngay_ra", "ngaydtr", "t_tongchi", "t_xn", "t_cdha", "t_thuoc", "t_mau", "t_pttt", "t_vtytth", "t_vtyttt", "t_dvktc", "t_ktg", "t_kham", "t_vchuyen", "t_bnct", "t_bhtt", "t_ngoaids", "lydo_vv", "benhkhac", "noikcb", "khoa", "thang_qt", "nam_qt", "gt_tu", "gt_den", "diachi", "giamdinh", "t_xuattoan", "lydo_xt", "t_datuyen", "t_vuottran", "loaikcb", "noi_ttoan", "sophieu", "ma_khoa", "keysl" };
                                int k = 0;
                                foreach (var b in _arr)
                                {
                                    k++;
                                    COMExcel.Range r = (COMExcel.Range)exSheet.Cells[1, k];
                                    r.Value2 = b.ToString();
                                    r.Columns.AutoFit();
                                }
                                foreach (var a in q)
                                {
                                    i++;
                                    COMExcel.Range r1 = (COMExcel.Range)exSheet.Cells[i, 1];
                                    r1.NumberFormat = "0";
                                    r1.Value2 = i - 1;
                                    r1.Columns.AutoFit();
                                    COMExcel.Range r2 = (COMExcel.Range)exSheet.Cells[i, 2];
                                    r2.NumberFormat = "@";
                                    r2.Value2 = convertFont(a.TenBNhan);
                                    r2.Columns.AutoFit();
                                    COMExcel.Range r3 = (COMExcel.Range)exSheet.Cells[i, 3];
                                    r3.NumberFormat = "0";
                                    r3.Value2 = a.NSinh;
                                    r3.Columns.AutoFit();
                                    COMExcel.Range r4 = (COMExcel.Range)exSheet.Cells[i, 4];
                                    r4.NumberFormat = "0";
                                    r4.Value2 = a.GTinh;
                                    r4.Columns.AutoFit();
                                    COMExcel.Range r5 = (COMExcel.Range)exSheet.Cells[i, 5];
                                    r5.NumberFormat = "@";
                                    r5.Value2 = convertFont(a.SThe);
                                    r5.Columns.AutoFit();
                                    COMExcel.Range r6 = (COMExcel.Range)exSheet.Cells[i, 6];
                                    r6.NumberFormat = "@";
                                    r6.Value2 = convertFont(a.MaCS);
                                    r6.Columns.AutoFit();
                                    COMExcel.Range r7 = (COMExcel.Range)exSheet.Cells[i, 7];
                                    r7.NumberFormat = "@";
                                    r7.Value2 = convertFont(a.MaICD);
                                    r7.Columns.AutoFit();
                                    COMExcel.Range r8 = (COMExcel.Range)exSheet.Cells[i, 8];
                                    r8.NumberFormat = "@";
                                    if (a.Ngayvao != null)
                                        r8.Value2 = convertFont(a.Ngayvao.ToString().Substring(0, 10));
                                    r8.Columns.AutoFit();
                                    COMExcel.Range r9 = (COMExcel.Range)exSheet.Cells[i, 9];
                                    r9.NumberFormat = "@";
                                    if (a.Ngayra != null)
                                        r9.Value2 = convertFont(a.Ngayra.ToString().Substring(0, 10));
                                    r9.Columns.AutoFit();
                                    COMExcel.Range r10 = (COMExcel.Range)exSheet.Cells[i, 10];
                                    r10.NumberFormat = "0";
                                    r10.Value2 = a.Songay;
                                    r10.Columns.AutoFit();
                                    COMExcel.Range r11 = (COMExcel.Range)exSheet.Cells[i, 11];
                                    r11.NumberFormat = "0";
                                    r11.Value2 = a.Tongcong;
                                    r11.Columns.AutoFit();
                                    COMExcel.Range r12 = (COMExcel.Range)exSheet.Cells[i, 12];
                                    r12.NumberFormat = "0";
                                    r12.Value2 = a.Xetnghiem;
                                    r12.Columns.AutoFit();
                                    COMExcel.Range r13 = (COMExcel.Range)exSheet.Cells[i, 13];
                                    r13.NumberFormat = "0";
                                    r13.Value2 = a.CDHA;
                                    r13.Columns.AutoFit();
                                    COMExcel.Range r14 = (COMExcel.Range)exSheet.Cells[i, 14];
                                    r14.NumberFormat = "0";
                                    r14.Value2 = a.Thuoc;
                                    r14.Columns.AutoFit();
                                    COMExcel.Range r15 = (COMExcel.Range)exSheet.Cells[i, 15];
                                    r15.NumberFormat = "0";
                                    r15.Value2 = a.Mau;
                                    r15.Columns.AutoFit();
                                    COMExcel.Range r16 = (COMExcel.Range)exSheet.Cells[i, 16];
                                    r16.NumberFormat = "0";
                                    r16.Value2 = a.TTPT;
                                    r16.Columns.AutoFit();
                                    COMExcel.Range r17 = (COMExcel.Range)exSheet.Cells[i, 17];
                                    r17.NumberFormat = "0";
                                    r17.Value2 = a.VTYT;
                                    r17.Columns.AutoFit();
                                    COMExcel.Range r18 = (COMExcel.Range)exSheet.Cells[i, 18];
                                    r18.NumberFormat = "0";
                                    r18.Value2 = 0;
                                    r18.Columns.AutoFit();
                                    COMExcel.Range r19 = (COMExcel.Range)exSheet.Cells[i, 19];
                                    r19.NumberFormat = "0";
                                    r19.Value2 = a.DVKTC;
                                    r19.Columns.AutoFit();
                                    COMExcel.Range r20 = (COMExcel.Range)exSheet.Cells[i, 20];
                                    r20.NumberFormat = "0";
                                    r20.Value2 = a.ThuocKTCG;
                                    r20.Columns.AutoFit();
                                    COMExcel.Range r21 = (COMExcel.Range)exSheet.Cells[i, 21];
                                    r21.NumberFormat = "0";
                                    r21.Value2 = a.Congkham;
                                    r21.Columns.AutoFit();
                                    COMExcel.Range r22 = (COMExcel.Range)exSheet.Cells[i, 22];
                                    r22.NumberFormat = "0";
                                    r22.Value2 = a.CPVanchuyen;
                                    r22.Columns.AutoFit();
                                    COMExcel.Range r23 = (COMExcel.Range)exSheet.Cells[i, 23];
                                    r23.NumberFormat = "0";
                                    r23.Value2 = a.Nguoibenhchitra;
                                    r23.Columns.AutoFit();
                                    COMExcel.Range r24 = (COMExcel.Range)exSheet.Cells[i, 24];
                                    r24.NumberFormat = "0";
                                    r24.Value2 = a.TongcongBHYT;
                                    r24.Columns.AutoFit();
                                    COMExcel.Range r25 = (COMExcel.Range)exSheet.Cells[i, 25];
                                    r25.NumberFormat = "0";
                                    r25.Value2 = a.CPVanchuyen;
                                    r25.Columns.AutoFit();
                                    COMExcel.Range r26 = (COMExcel.Range)exSheet.Cells[i, 26];
                                    r26.NumberFormat = "@";
                                    if (a.Tuyen != null && a.Tuyen == 1)
                                    {
                                        r26.Value2 = 1;
                                    }
                                    else
                                        r26.Value2 = 0;
                                    r26.Columns.AutoFit();
                                    COMExcel.Range r27 = (COMExcel.Range)exSheet.Cells[i, 27];
                                    r27.NumberFormat = "@";
                                    r27.Value2 = "";
                                    r27.Columns.AutoFit();
                                    COMExcel.Range r28 = (COMExcel.Range)exSheet.Cells[i, 28];
                                    r28.NumberFormat = "@";
                                    r28.Value2 = convertFont(DungChung.Bien.MaBV);
                                    r28.Columns.AutoFit();
                                    COMExcel.Range r29 = (COMExcel.Range)exSheet.Cells[i, 29];
                                    r29.NumberFormat = "@";
                                    var K = (from kh in _dataContext.KPhongs.Where(p => p.MaKP == a.Makp) select new { kh.TenKP }).ToList();
                                    if (K.Count > 0)
                                    {
                                        r29.Value2 = convertFont(K.First().TenKP);
                                    }
                                    //r29.Value2 = a.Makp;
                                    r29.Columns.AutoFit();
                                    COMExcel.Range r30 = (COMExcel.Range)exSheet.Cells[i, 30];
                                    r30.NumberFormat = "0";
                                    if (a.Ngayra != null)
                                    {
                                        r30.Value2 = a.Ngayra.Value.Month;
                                        r30.Columns.AutoFit();
                                    }
                                    COMExcel.Range r31 = (COMExcel.Range)exSheet.Cells[i, 31];
                                    r31.NumberFormat = "0";
                                    if (a.Ngayra != null)
                                    {
                                        r31.Value2 = a.Ngayra.Value.Year;
                                        r31.Columns.AutoFit();
                                    }
                                    COMExcel.Range r32 = (COMExcel.Range)exSheet.Cells[i, 32];
                                    r32.NumberFormat = "@";
                                    if (a.BHtu != null)
                                    {
                                        r32.Value2 = convertFont(a.BHtu.ToString().Substring(0, 10));
                                        r32.Columns.AutoFit();
                                    }
                                    COMExcel.Range r33 = (COMExcel.Range)exSheet.Cells[i, 33];
                                    r33.NumberFormat = "@";
                                    if (a.BHden != null)
                                    {
                                        r33.Value2 = convertFont(a.BHden.ToString().Substring(0, 10));
                                        r33.Columns.AutoFit();
                                    }
                                    COMExcel.Range r34 = (COMExcel.Range)exSheet.Cells[i, 34];
                                    r34.NumberFormat = "@";
                                    r34.Value2 = convertFont(a.Diachi);
                                    r34.Columns.AutoFit();
                                    COMExcel.Range r35 = (COMExcel.Range)exSheet.Cells[i, 35];
                                    r35.Value2 = "";
                                    r35.Columns.AutoFit();
                                    COMExcel.Range r36 = (COMExcel.Range)exSheet.Cells[i, 36];
                                    r36.NumberFormat = "@";
                                    r36.Value2 = 0;
                                    r36.Columns.AutoFit();
                                    COMExcel.Range r37 = (COMExcel.Range)exSheet.Cells[i, 37];
                                    r37.Value2 = "";
                                    r37.Columns.AutoFit();
                                    COMExcel.Range r38 = (COMExcel.Range)exSheet.Cells[i, 38];
                                    r38.NumberFormat = "@";
                                    r38.Value2 = 0;
                                    r38.Columns.AutoFit();
                                    COMExcel.Range r39 = (COMExcel.Range)exSheet.Cells[i, 39];
                                    r39.NumberFormat = "0";
                                    r39.Value2 = 0;
                                    r39.Columns.AutoFit();
                                    COMExcel.Range r40 = (COMExcel.Range)exSheet.Cells[i, 40];
                                    r40.NumberFormat = "@";
                                    r40.Value2 = convertFont("NOI");
                                    r40.Columns.AutoFit();
                                    COMExcel.Range r41 = (COMExcel.Range)exSheet.Cells[i, 41];
                                    r41.NumberFormat = "@";
                                    r41.Value2 = convertFont("CSKCB");
                                    r41.Columns.AutoFit();
                                    COMExcel.Range r42 = (COMExcel.Range)exSheet.Cells[i, 42];
                                    r42.NumberFormat = "@";
                                    r42.Value2 = a.Mabn.Value;
                                    r42.Columns.AutoFit();
                                    COMExcel.Range r43 = (COMExcel.Range)exSheet.Cells[i, 43];
                                    r43.NumberFormat = "@";
                                    var kp = (from k1 in _dataContext.KPhongs.Where(p => p.MaKP == a.Makp) select new { k1.TenKP }).ToList();
                                    if (kp.Count > 0)
                                    { r43.Value2 = convertFont(kp.First().TenKP); }
                                    else
                                    { r43.Value2 = a.Makp; }
                                    r43.Columns.AutoFit();
                                    COMExcel.Range r44 = (COMExcel.Range)exSheet.Cells[i, 44];
                                    r44.Value2 = "";
                                    r44.Columns.AutoFit();
                                }
                                exApp.Visible = true;//Ẩn hiện chương trình
                                exQLBV.SaveAs("C:\\Bieu79.xls", COMExcel.XlFileFormat.xlWorkbookNormal,
                                                null, null, false, false,
                                                COMExcel.XlSaveAsAccessMode.xlExclusive,
                                                false, false, false, false, false);
                                //exQLBV.Close(false, false, false);
                                //exApp.Quit(); // thoát ứng dụng
                                System.Runtime.InteropServices.Marshal.ReleaseComObject(exQLBV);
                                System.Runtime.InteropServices.Marshal.ReleaseComObject(exApp);
                            }
                            #endregion
                        }
                    }
                    else
                    {
                        DateTime ngaytu = System.DateTime.Now.Date;
                        DateTime ngayden = System.DateTime.Now.Date;
                        ngaytu = DungChung.Ham.NgayTu(lupNgaytu.DateTime);
                        ngayden = DungChung.Ham.NgayDen(lupngayden.DateTime);
                        var q = (from bn in _dataContext.BenhNhans.Where(p => p.DTuong == DoiTuongKham).Where(p => p.NoiTru == 1).Where(p => p.SThe.Length > 2)
                                 join dt in _dataContext.DTuongs on bn.SThe.Substring(0, 2) equals dt.MaDTuong
                                 join vp in _dataContext.VienPhis on bn.MaBNhan equals vp.MaBNhan
                                 join vpct in _dataContext.VienPhicts.Where(p => p.TrongBH == 1) on vp.idVPhi equals vpct.idVPhi
                                 join dv in _dataContext.DichVus on vpct.MaDV equals dv.MaDV
                                 join vv in _dataContext.VaoViens on bn.MaBNhan equals vv.MaBNhan
                                 join rv in _dataContext.RaViens.Where(p => p.NgayRa >= ngaytu).Where(p => p.NgayRa <= ngayden) on bn.MaBNhan equals rv.MaBNhan
                                 group new { bn, rv, dv, vpct, vp } by new { dt.Nhom, vp.NgayTT, bn.NoiTinh, vp.MaBNhan, rv.MaKP, vv.NgayVao, bn.TenBNhan, bn.NamSinh, bn.GTinh, bn.SThe, bn.DChi, bn.MaCS, bn.Tuyen, bn.HanBHTu, bn.HanBHDen, rv.NgayRa, rv.SoNgaydt, rv.MaICD, } into kq
                                 select new
                                 {
                                     dt = kq.Key.SThe.Substring(0, 2),
                                     Nhom = kq.Key.Nhom,
                                    // kq.Key.MaBNhan,
                                     kq.Key.NgayTT,
                                     NoiTinh = kq.Key.NoiTinh,
                                     Tuyen = kq.Key.Tuyen,
                                     Makp = kq.Key.MaKP,
                                     MaBNhan = kq.Key.MaBNhan,
                                     TenBNhan = kq.Key.TenBNhan,
                                     NSinh = kq.Key.NamSinh,
                                     SThe = kq.Key.SThe,
                                     Nam = kq.Key.GTinh,
                                     GTinh = kq.Key.GTinh,
                                     MaCS = kq.Key.MaCS,
                                     MaICD = kq.Key.MaICD,
                                     Ngaykham = kq.Key.NgayVao,
                                     Ngayvao = kq.Key.NgayVao,
                                     Ngayra = kq.Key.NgayRa,
                                     Songay = kq.Key.SoNgaydt,
                                     //TenKP = kq.Key.TenKP,
                                     BHtu = kq.Key.HanBHTu,
                                     BHden = kq.Key.HanBHDen,
                                     Diachi = kq.Key.DChi,
                                     Mabn = kq.Key.MaBNhan,
                                     // Ngaykham = kq.Key.NgayKham.Value.Day,
                                     Thuoc = kq.Where(p => p.dv.IDNhom == idThuoc).Sum(p => p.vpct.ThanhTien),
                                     CDHA = kq.Where(p => p.dv.IDNhom == idCDHA).Sum(p => p.vpct.ThanhTien),
                                     Congkham = kq.Where(p => p.dv.IDNhom == idNgayGiuong).Sum(p => p.vpct.ThanhTien),
                                     Xetnghiem = kq.Where(p => p.dv.IDNhom == idXN).Sum(p => p.vpct.ThanhTien),
                                     Mau = kq.Where(p => p.dv.IDNhom == idMau).Sum(p => p.vpct.ThanhTien),
                                     TTPT = kq.Where(p => p.dv.IDNhom == idTTPT).Sum(p => p.vpct.ThanhTien),
                                     VTYT = kq.Where(p => p.dv.IDNhom == idVTYT).Sum(p => p.vpct.ThanhTien),
                                     DVKTC = kq.Where(p => p.dv.IDNhom == idDVKTC).Sum(p => p.vpct.ThanhTien),
                                     ThuocKTCG = kq.Where(p => p.dv.IDNhom == idThuocUngThuCTG).Sum(p => p.vpct.ThanhTien),
                                     CPVanchuyen = kq.Where(p => p.dv.IDNhom == idChiPhiVC).Sum(p => p.vpct.ThanhTien),
                                     ThanhTien = kq.Sum(p => p.vpct.ThanhTien),
                                     Tongchi = kq.Sum(p => p.vpct.ThanhTien),
                                     Tongcong = kq.Sum(p => p.vpct.ThanhTien),
                                     Nguoibenhchitra = kq.Sum(p => p.vpct.TienBN),
                                     TongcongBHYT = kq.Sum(p => p.vpct.TienBH),

                                 }).ToList();
                        if (string.IsNullOrEmpty(lupKhoaphong.Text))
                        {
                            frmIn frm = new frmIn();
                            BaoCao.rep80aHD rep = new BaoCao.rep80aHD();
                            //q = q.OrderBy(p => p.Ngayra).OrderBy(p => p.SThe).OrderBy(p => p.Nhom).OrderBy(p => p.NoiTinh).OrderBy(p => p.Tuyen).ToList();

                            q = q.OrderBy(p => p.NoiTinh).ThenBy(p => p.Tuyen).ThenBy(p => p.Nhom).ThenBy(p => p.dt).ThenBy(p => p.Ngayra).ToList();
                            rep.DataSource = q.ToList();

                            rep.Ngaythang.Value = theoquy();
                            rep.MaCS.Value = DungChung.Bien.MaBV;
                            rep.TenCQ.Value = DungChung.Bien.TenCQ.ToUpper();
                            double st = 0;
                            st = q.Sum(p => p.TongcongBHYT);
                            rep.Sotien.Value = QLBV_Library.QLBV_Ham.DocTienBangChu(st, " đồng.");
                            rep.BindingData();
                            rep.CreateDocument();
                            // rep.DataMember = "Table";
                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                            frm.ShowDialog();
                            #region xuat Excel
                            if (Xuatex.Checked)
                            {
                                COMExcel.Application exApp = new COMExcel.Application();
                                COMExcel.Workbook exQLBV = exApp.Workbooks.Add(
                                          COMExcel.XlWBATemplate.xlWBATWorksheet);
                                COMExcel.Worksheet exSheet = (COMExcel.Worksheet)exQLBV.Worksheets[1];
                                //exSheet.Activate();
                                exSheet.Name = "NoiTru";// gán tên sheet
                                int i = 1;
                                string[] _arr = new string[44] { "stt", "hoten", "namsinh", "gioitinh", "mathe", "ma_dkbd", "mabenh", "ngay_vao", "ngay_ra", "ngaydtr", "t_tongchi", "t_xn", "t_cdha", "t_thuoc", "t_mau", "t_pttt", "t_vtytth", "t_vtyttt", "t_dvktc", "t_ktg", "t_kham", "t_vchuyen", "t_bnct", "t_bhtt", "t_ngoaids", "lydo_vv", "benhkhac", "noikcb", "khoa", "thang_qt", "nam_qt", "gt_tu", "gt_den", "diachi", "giamdinh", "t_xuattoan", "lydo_xt", "t_datuyen", "t_vuottran", "loaikcb", "noi_ttoan", "sophieu", "ma_khoa", "keysl" };
                                int k = 0;
                                foreach (var b in _arr)
                                {
                                    k++;
                                    COMExcel.Range r = (COMExcel.Range)exSheet.Cells[1, k];
                                    r.Value2 = b.ToString();
                                    r.Columns.AutoFit();
                                }
                                foreach (var a in q)
                                {
                                    i++;
                                    COMExcel.Range r1 = (COMExcel.Range)exSheet.Cells[i, 1];
                                    r1.NumberFormat = "0";
                                    r1.Value2 = i - 1;
                                    r1.Columns.AutoFit();
                                    COMExcel.Range r2 = (COMExcel.Range)exSheet.Cells[i, 2];
                                    r2.NumberFormat = "@";
                                    r2.Value2 = convertFont(a.TenBNhan);
                                    r2.Columns.AutoFit();
                                    COMExcel.Range r3 = (COMExcel.Range)exSheet.Cells[i, 3];
                                    r3.NumberFormat = "0";
                                    r3.Value2 = a.NSinh;
                                    r3.Columns.AutoFit();
                                    COMExcel.Range r4 = (COMExcel.Range)exSheet.Cells[i, 4];
                                    r4.NumberFormat = "0";
                                    r4.Value2 = a.GTinh;
                                    r4.Columns.AutoFit();
                                    COMExcel.Range r5 = (COMExcel.Range)exSheet.Cells[i, 5];
                                    r5.NumberFormat = "@";
                                    r5.Value2 = convertFont(a.SThe);
                                    r5.Columns.AutoFit();
                                    COMExcel.Range r6 = (COMExcel.Range)exSheet.Cells[i, 6];
                                    r6.NumberFormat = "@";
                                    r6.Value2 = convertFont(a.MaCS);
                                    r6.Columns.AutoFit();
                                    COMExcel.Range r7 = (COMExcel.Range)exSheet.Cells[i, 7];
                                    r7.NumberFormat = "@";
                                    r7.Value2 = convertFont(a.MaICD);
                                    r7.Columns.AutoFit();
                                    COMExcel.Range r8 = (COMExcel.Range)exSheet.Cells[i, 8];
                                    r8.NumberFormat = "@";
                                    if (a.Ngayvao != null)
                                        r8.Value2 = convertFont(a.Ngayvao.ToString().Substring(0, 10));
                                    r8.Columns.AutoFit();
                                    COMExcel.Range r9 = (COMExcel.Range)exSheet.Cells[i, 9];
                                    r9.NumberFormat = "@";
                                    if (a.Ngayra != null)
                                        r9.Value2 = convertFont(a.Ngayra.ToString().Substring(0, 10));
                                    r9.Columns.AutoFit();
                                    COMExcel.Range r10 = (COMExcel.Range)exSheet.Cells[i, 10];
                                    r10.NumberFormat = "0";
                                    r10.Value2 = a.Songay;
                                    r10.Columns.AutoFit();
                                    COMExcel.Range r11 = (COMExcel.Range)exSheet.Cells[i, 11];
                                    r11.NumberFormat = "0";
                                    r11.Value2 = a.Tongcong;
                                    r11.Columns.AutoFit();
                                    COMExcel.Range r12 = (COMExcel.Range)exSheet.Cells[i, 12];
                                    r12.NumberFormat = "0";
                                    r12.Value2 = a.Xetnghiem;
                                    r12.Columns.AutoFit();
                                    COMExcel.Range r13 = (COMExcel.Range)exSheet.Cells[i, 13];
                                    r13.NumberFormat = "0";
                                    r13.Value2 = a.CDHA;
                                    r13.Columns.AutoFit();
                                    COMExcel.Range r14 = (COMExcel.Range)exSheet.Cells[i, 14];
                                    r14.NumberFormat = "0";
                                    r14.Value2 = a.Thuoc;
                                    r14.Columns.AutoFit();
                                    COMExcel.Range r15 = (COMExcel.Range)exSheet.Cells[i, 15];
                                    r15.NumberFormat = "0";
                                    r15.Value2 = a.Mau;
                                    r15.Columns.AutoFit();
                                    COMExcel.Range r16 = (COMExcel.Range)exSheet.Cells[i, 16];
                                    r16.NumberFormat = "0";
                                    r16.Value2 = a.TTPT;
                                    r16.Columns.AutoFit();
                                    COMExcel.Range r17 = (COMExcel.Range)exSheet.Cells[i, 17];
                                    r17.NumberFormat = "0";
                                    r17.Value2 = a.VTYT;
                                    r17.Columns.AutoFit();
                                    COMExcel.Range r18 = (COMExcel.Range)exSheet.Cells[i, 18];
                                    r18.NumberFormat = "0";
                                    r18.Value2 = 0;
                                    r18.Columns.AutoFit();
                                    COMExcel.Range r19 = (COMExcel.Range)exSheet.Cells[i, 19];
                                    r19.NumberFormat = "0";
                                    r19.Value2 = a.DVKTC;
                                    r19.Columns.AutoFit();
                                    COMExcel.Range r20 = (COMExcel.Range)exSheet.Cells[i, 20];
                                    r20.NumberFormat = "0";
                                    r20.Value2 = a.ThuocKTCG;
                                    r20.Columns.AutoFit();
                                    COMExcel.Range r21 = (COMExcel.Range)exSheet.Cells[i, 21];
                                    r21.NumberFormat = "0";
                                    r21.Value2 = a.Congkham;
                                    r21.Columns.AutoFit();
                                    COMExcel.Range r22 = (COMExcel.Range)exSheet.Cells[i, 22];
                                    r22.NumberFormat = "0";
                                    r22.Value2 = a.CPVanchuyen;
                                    r22.Columns.AutoFit();
                                    COMExcel.Range r23 = (COMExcel.Range)exSheet.Cells[i, 23];
                                    r23.NumberFormat = "0";
                                    r23.Value2 = a.Nguoibenhchitra;
                                    r23.Columns.AutoFit();
                                    COMExcel.Range r24 = (COMExcel.Range)exSheet.Cells[i, 24];
                                    r24.NumberFormat = "0";
                                    r24.Value2 = a.TongcongBHYT;
                                    r24.Columns.AutoFit();
                                    COMExcel.Range r25 = (COMExcel.Range)exSheet.Cells[i, 25];
                                    r25.NumberFormat = "0";
                                    r25.Value2 = a.CPVanchuyen;
                                    r25.Columns.AutoFit();
                                    COMExcel.Range r26 = (COMExcel.Range)exSheet.Cells[i, 26];
                                    r26.NumberFormat = "@";
                                    if (a.Tuyen != null && a.Tuyen == 1)
                                    {
                                        r26.Value2 = 1;
                                    }
                                    else
                                        r26.Value2 = 0;

                                    r26.Columns.AutoFit();
                                    COMExcel.Range r27 = (COMExcel.Range)exSheet.Cells[i, 27];
                                    r27.NumberFormat = "@";
                                    r27.Value2 = "";
                                    r27.Columns.AutoFit();
                                    COMExcel.Range r28 = (COMExcel.Range)exSheet.Cells[i, 28];
                                    r28.NumberFormat = "@";
                                    r28.Value2 = convertFont(DungChung.Bien.MaBV);
                                    r28.Columns.AutoFit();
                                    COMExcel.Range r29 = (COMExcel.Range)exSheet.Cells[i, 29];
                                    r29.NumberFormat = "@";
                                    var K = (from kh in _dataContext.KPhongs.Where(p => p.MaKP == a.Makp) select new { kh.TenKP }).ToList();
                                    if (K.Count > 0)
                                    {
                                        r29.Value2 = convertFont(K.First().TenKP);
                                    }
                                    r29.Columns.AutoFit();
                                    COMExcel.Range r30 = (COMExcel.Range)exSheet.Cells[i, 30];
                                    r30.NumberFormat = "0";
                                    if (a.Ngayra != null)
                                    {
                                        r30.Value2 = a.Ngayra.Value.Month;
                                        r30.Columns.AutoFit();
                                    }
                                    COMExcel.Range r31 = (COMExcel.Range)exSheet.Cells[i, 31];
                                    r31.NumberFormat = "0";
                                    if (a.Ngayra != null)
                                    {
                                        r31.Value2 = a.Ngayra.Value.Year;
                                        r31.Columns.AutoFit();
                                    }
                                    COMExcel.Range r32 = (COMExcel.Range)exSheet.Cells[i, 32];
                                    r32.NumberFormat = "@";
                                    if (a.BHtu != null)
                                    {
                                        r32.Value2 = convertFont(a.BHtu.ToString().Substring(0, 10));
                                        r32.Columns.AutoFit();
                                    }
                                    COMExcel.Range r33 = (COMExcel.Range)exSheet.Cells[i, 33];
                                    r33.NumberFormat = "@";
                                    if (a.BHden != null)
                                    {
                                        r33.Value2 = convertFont(a.BHden.ToString().Substring(0, 10));
                                        r33.Columns.AutoFit();
                                    }
                                    COMExcel.Range r34 = (COMExcel.Range)exSheet.Cells[i, 34];
                                    r34.NumberFormat = "@";
                                    r34.Value2 = convertFont(a.Diachi);
                                    r34.Columns.AutoFit();
                                    COMExcel.Range r35 = (COMExcel.Range)exSheet.Cells[i, 35];
                                    r35.Value2 = "";
                                    r35.Columns.AutoFit();
                                    COMExcel.Range r36 = (COMExcel.Range)exSheet.Cells[i, 36];
                                    r36.NumberFormat = "@";
                                    r36.Value2 = 0;
                                    r36.Columns.AutoFit();
                                    COMExcel.Range r37 = (COMExcel.Range)exSheet.Cells[i, 37];
                                    r37.Value2 = "";
                                    r37.Columns.AutoFit();
                                    COMExcel.Range r38 = (COMExcel.Range)exSheet.Cells[i, 38];
                                    r38.NumberFormat = "@";
                                    r38.Value2 = 0;
                                    r38.Columns.AutoFit();
                                    COMExcel.Range r39 = (COMExcel.Range)exSheet.Cells[i, 39];
                                    r39.NumberFormat = "0";
                                    r39.Value2 = 0;
                                    r39.Columns.AutoFit();
                                    COMExcel.Range r40 = (COMExcel.Range)exSheet.Cells[i, 40];
                                    r40.NumberFormat = "@";
                                    r40.Value2 = convertFont("NOI");
                                    r40.Columns.AutoFit();
                                    COMExcel.Range r41 = (COMExcel.Range)exSheet.Cells[i, 41];
                                    r41.NumberFormat = "@";
                                    r41.Value2 = convertFont("CSKCB");
                                    r41.Columns.AutoFit();
                                    COMExcel.Range r42 = (COMExcel.Range)exSheet.Cells[i, 42];
                                    r42.NumberFormat = "@";
                                    r42.Value2 = a.Mabn.Value;
                                    r42.Columns.AutoFit();
                                    COMExcel.Range r43 = (COMExcel.Range)exSheet.Cells[i, 43];
                                    r43.NumberFormat = "@";
                                    var kp = (from k1 in _dataContext.KPhongs.Where(p => p.MaKP == a.Makp) select new { k1.TenKP }).ToList();
                                    if (kp.Count > 0)
                                    { r43.Value2 = convertFont(kp.First().TenKP); }
                                    else
                                    { r43.Value2 = a.Makp; }
                                    r43.Columns.AutoFit();
                                    COMExcel.Range r44 = (COMExcel.Range)exSheet.Cells[i, 44];
                                    r43.NumberFormat = "@";
                                    r44.Value2 = "";
                                    r44.Columns.AutoFit();
                                }
                                exApp.Visible = true;//Ẩn hiện chương trình
                                exQLBV.SaveAs("C:\\Bieu80.xls", COMExcel.XlFileFormat.xlWorkbookNormal,
                                                null, null, false, false,
                                                COMExcel.XlSaveAsAccessMode.xlExclusive,
                                                false, false, false, false, false);
                                //exQLBV.Close(false, false, false);
                                //exApp.Quit(); // thoát ứng dụng
                                System.Runtime.InteropServices.Marshal.ReleaseComObject(exQLBV);
                                System.Runtime.InteropServices.Marshal.ReleaseComObject(exApp);
                            }
                            #endregion
                        }
                        else
                        {
                            int _MaKP =  lupKhoaphong.EditValue == null ? 0 : Convert.ToInt32( lupKhoaphong.EditValue);
                            frmIn frm = new frmIn();
                            BaoCao.rep80aHD rep = new BaoCao.rep80aHD();
                            q = q.ToList().Where(p => p.Makp == _MaKP).OrderBy(p => p.Nhom).ThenBy(p => p.dt).ThenBy(p => p.Ngayra).ToList();
                            rep.DataSource = q;
                            rep.Ngaythang.Value = theoquy();
                            rep.MaCS.Value = DungChung.Bien.MaBV;
                            rep.TenCQ.Value = DungChung.Bien.TenCQ.ToUpper();
                            rep.paramKhoaPhong.Value = lupKhoaphong.Text.ToUpper();
                            double st = 0;
                            st = q.Sum(p => p.TongcongBHYT);
                            rep.Sotien.Value = QLBV_Library.QLBV_Ham.DocTienBangChu(st, " đồng.");
                            rep.BindingData();
                            rep.CreateDocument();
                            //rep.DataMember = "Table";
                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                            frm.ShowDialog();
                            #region xuat Excel
                            if (Xuatex.Checked)
                            {
                                COMExcel.Application exApp = new COMExcel.Application();
                                COMExcel.Workbook exQLBV = exApp.Workbooks.Add(
                                          COMExcel.XlWBATemplate.xlWBATWorksheet);
                                COMExcel.Worksheet exSheet = (COMExcel.Worksheet)exQLBV.Worksheets[1];
                                //exSheet.Activate();
                                exSheet.Name = "NoiTru";// gán tên sheet
                                int i = 1;
                                string[] _arr = new string[44] { "stt", "hoten", "namsinh", "gioitinh", "mathe", "ma_dkbd", "mabenh", "ngay_vao", "ngay_ra", "ngaydtr", "t_tongchi", "t_xn", "t_cdha", "t_thuoc", "t_mau", "t_pttt", "t_vtytth", "t_vtyttt", "t_dvktc", "t_ktg", "t_kham", "t_vchuyen", "t_bnct", "t_bhtt", "t_ngoaids", "lydo_vv", "benhkhac", "noikcb", "khoa", "thang_qt", "nam_qt", "gt_tu", "gt_den", "diachi", "giamdinh", "t_xuattoan", "lydo_xt", "t_datuyen", "t_vuottran", "loaikcb", "noi_ttoan", "sophieu", "ma_khoa", "keysl" };
                                int k = 0;
                                foreach (var b in _arr)
                                {
                                    k++;
                                    COMExcel.Range r = (COMExcel.Range)exSheet.Cells[1, k];
                                    r.Value2 = b.ToString();
                                    r.Columns.AutoFit();
                                }
                                foreach (var a in q)
                                {
                                    i++;
                                    COMExcel.Range r1 = (COMExcel.Range)exSheet.Cells[i, 1];
                                    r1.NumberFormat = "0";
                                    r1.Value2 = i - 1;
                                    r1.Columns.AutoFit();
                                    COMExcel.Range r2 = (COMExcel.Range)exSheet.Cells[i, 2];
                                    r2.NumberFormat = "@";
                                    r2.Value2 = a.TenBNhan;
                                    r2.Columns.AutoFit();
                                    COMExcel.Range r3 = (COMExcel.Range)exSheet.Cells[i, 3];
                                    r3.NumberFormat = "0";
                                    r3.Value2 = a.NSinh;
                                    r3.Columns.AutoFit();
                                    COMExcel.Range r4 = (COMExcel.Range)exSheet.Cells[i, 4];
                                    r4.NumberFormat = "0";
                                    r4.Value2 = a.GTinh;
                                    r4.Columns.AutoFit();
                                    COMExcel.Range r5 = (COMExcel.Range)exSheet.Cells[i, 5];
                                    r5.NumberFormat = "@";
                                    r5.Value2 = a.SThe;
                                    r5.Columns.AutoFit();
                                    COMExcel.Range r6 = (COMExcel.Range)exSheet.Cells[i, 6];
                                    r6.NumberFormat = "@";
                                    r6.Value2 = a.MaCS;
                                    r6.Columns.AutoFit();
                                    COMExcel.Range r7 = (COMExcel.Range)exSheet.Cells[i, 7];
                                    r7.NumberFormat = "@";
                                    r7.Value2 = a.MaICD;
                                    r7.Columns.AutoFit();
                                    COMExcel.Range r8 = (COMExcel.Range)exSheet.Cells[i, 8];
                                    r8.NumberFormat = "@";
                                    if (a.Ngayvao != null)
                                        r8.Value2 = a.Ngayvao.ToString().Substring(0, 10);
                                    r8.Columns.AutoFit();
                                    COMExcel.Range r9 = (COMExcel.Range)exSheet.Cells[i, 9];
                                    r9.NumberFormat = "@";
                                    if (a.Ngayra != null)
                                        r9.Value2 = a.Ngayra.ToString().Substring(0, 10);
                                    r9.Columns.AutoFit();
                                    COMExcel.Range r10 = (COMExcel.Range)exSheet.Cells[i, 10];
                                    r10.NumberFormat = "0";
                                    r10.Value2 = a.Songay;
                                    r10.Columns.AutoFit();
                                    COMExcel.Range r11 = (COMExcel.Range)exSheet.Cells[i, 11];
                                    r11.NumberFormat = "0";
                                    r11.Value2 = a.Tongcong;
                                    r11.Columns.AutoFit();
                                    COMExcel.Range r12 = (COMExcel.Range)exSheet.Cells[i, 12];
                                    r12.NumberFormat = "0";
                                    r12.Value2 = a.Xetnghiem;
                                    r12.Columns.AutoFit();
                                    COMExcel.Range r13 = (COMExcel.Range)exSheet.Cells[i, 13];
                                    r13.NumberFormat = "0";
                                    r13.Value2 = a.CDHA;
                                    r13.Columns.AutoFit();
                                    COMExcel.Range r14 = (COMExcel.Range)exSheet.Cells[i, 14];
                                    r14.NumberFormat = "0";
                                    r14.Value2 = a.Thuoc;
                                    r14.Columns.AutoFit();
                                    COMExcel.Range r15 = (COMExcel.Range)exSheet.Cells[i, 15];
                                    r15.NumberFormat = "0";
                                    r15.Value2 = a.Mau;
                                    r15.Columns.AutoFit();
                                    COMExcel.Range r16 = (COMExcel.Range)exSheet.Cells[i, 16];
                                    r16.NumberFormat = "0";
                                    r16.Value2 = a.TTPT;
                                    r16.Columns.AutoFit();
                                    COMExcel.Range r17 = (COMExcel.Range)exSheet.Cells[i, 17];
                                    r17.NumberFormat = "0";
                                    r17.Value2 = a.VTYT;
                                    r17.Columns.AutoFit();
                                    COMExcel.Range r18 = (COMExcel.Range)exSheet.Cells[i, 18];
                                    r18.NumberFormat = "0";
                                    r18.Value2 = 0;
                                    r18.Columns.AutoFit();
                                    COMExcel.Range r19 = (COMExcel.Range)exSheet.Cells[i, 19];
                                    r19.NumberFormat = "0";
                                    r19.Value2 = a.DVKTC;
                                    r19.Columns.AutoFit();
                                    COMExcel.Range r20 = (COMExcel.Range)exSheet.Cells[i, 20];
                                    r20.NumberFormat = "0";
                                    r20.Value2 = a.ThuocKTCG;
                                    r20.Columns.AutoFit();
                                    COMExcel.Range r21 = (COMExcel.Range)exSheet.Cells[i, 21];
                                    r21.NumberFormat = "0";
                                    r21.Value2 = a.Congkham;
                                    r21.Columns.AutoFit();
                                    COMExcel.Range r22 = (COMExcel.Range)exSheet.Cells[i, 22];
                                    r22.NumberFormat = "0";
                                    r22.Value2 = a.CPVanchuyen;
                                    r22.Columns.AutoFit();
                                    COMExcel.Range r23 = (COMExcel.Range)exSheet.Cells[i, 23];
                                    r23.NumberFormat = "0";
                                    r23.Value2 = a.Nguoibenhchitra;
                                    r23.Columns.AutoFit();
                                    COMExcel.Range r24 = (COMExcel.Range)exSheet.Cells[i, 24];
                                    r24.NumberFormat = "0";
                                    r24.Value2 = a.TongcongBHYT;
                                    r24.Columns.AutoFit();
                                    COMExcel.Range r25 = (COMExcel.Range)exSheet.Cells[i, 25];
                                    r25.NumberFormat = "0";
                                    r25.Value2 = a.CPVanchuyen;
                                    r25.Columns.AutoFit();
                                    COMExcel.Range r26 = (COMExcel.Range)exSheet.Cells[i, 26];
                                    r26.NumberFormat = "@";
                                    if (a.Tuyen != null && a.Tuyen == 1)
                                    {
                                        r26.Value2 = 1;
                                    }
                                    else
                                        r26.Value2 = 0;
                                    r26.Columns.AutoFit();
                                    COMExcel.Range r27 = (COMExcel.Range)exSheet.Cells[i, 27];
                                    r27.NumberFormat = "@";
                                    r27.Value2 = "";
                                    r27.Columns.AutoFit();
                                    COMExcel.Range r28 = (COMExcel.Range)exSheet.Cells[i, 28];
                                    r28.NumberFormat = "@";
                                    r28.Value2 = DungChung.Bien.MaBV;
                                    r28.Columns.AutoFit();
                                    COMExcel.Range r29 = (COMExcel.Range)exSheet.Cells[i, 29];
                                    r29.NumberFormat = "@";
                                    var K = (from kh in _dataContext.KPhongs.Where(p => p.MaKP == a.Makp) select new { kh.TenKP }).ToList();
                                    if (K.Count > 0)
                                    {
                                        r29.Value2 = K.First().TenKP;
                                    }
                                    //r29.Value2 = a.Makp;
                                    r29.Columns.AutoFit();
                                    COMExcel.Range r30 = (COMExcel.Range)exSheet.Cells[i, 30];
                                    r30.NumberFormat = "0";
                                    if (a.Ngayra != null)
                                    {
                                        r30.Value2 = a.Ngayra.Value.Month;
                                        r30.Columns.AutoFit();
                                    }
                                    COMExcel.Range r31 = (COMExcel.Range)exSheet.Cells[i, 31];
                                    r31.NumberFormat = "0";
                                    if (a.Ngayra != null)
                                    {
                                        r31.Value2 = a.Ngayra.Value.Year;
                                        r31.Columns.AutoFit();
                                    }
                                    COMExcel.Range r32 = (COMExcel.Range)exSheet.Cells[i, 32];
                                    r32.NumberFormat = "@";
                                    if (a.BHtu != null)
                                    {
                                        r32.Value2 = a.BHtu.ToString().Substring(0, 10);
                                        r32.Columns.AutoFit();
                                    }
                                    COMExcel.Range r33 = (COMExcel.Range)exSheet.Cells[i, 33];
                                    r33.NumberFormat = "@";
                                    if (a.BHden != null)
                                    {
                                        r33.Value2 = a.BHden.ToString().Substring(0, 10);
                                        r33.Columns.AutoFit();
                                    }
                                    COMExcel.Range r34 = (COMExcel.Range)exSheet.Cells[i, 34];
                                    r34.NumberFormat = "@";
                                    r34.Value2 = a.Diachi;
                                    r34.Columns.AutoFit();
                                    COMExcel.Range r35 = (COMExcel.Range)exSheet.Cells[i, 35];
                                    r35.Value2 = "";
                                    r35.Columns.AutoFit();
                                    COMExcel.Range r36 = (COMExcel.Range)exSheet.Cells[i, 36];
                                    r36.NumberFormat = "@";
                                    r36.Value2 = 0;
                                    r36.Columns.AutoFit();
                                    COMExcel.Range r37 = (COMExcel.Range)exSheet.Cells[i, 37];
                                    r37.Value2 = "";
                                    r37.Columns.AutoFit();
                                    COMExcel.Range r38 = (COMExcel.Range)exSheet.Cells[i, 38];
                                    r38.NumberFormat = "@";
                                    r38.Value2 = 0;
                                    r38.Columns.AutoFit();
                                    COMExcel.Range r39 = (COMExcel.Range)exSheet.Cells[i, 39];
                                    r39.NumberFormat = "0";
                                    r39.Value2 = 0;
                                    r39.Columns.AutoFit();
                                    COMExcel.Range r40 = (COMExcel.Range)exSheet.Cells[i, 40];
                                    r40.NumberFormat = "@";
                                    r40.Value2 = "NOI";
                                    r40.Columns.AutoFit();
                                    COMExcel.Range r41 = (COMExcel.Range)exSheet.Cells[i, 41];
                                    r41.NumberFormat = "@";
                                    r41.Value2 = "CSKCB";
                                    r41.Columns.AutoFit();
                                    COMExcel.Range r42 = (COMExcel.Range)exSheet.Cells[i, 42];
                                    r42.NumberFormat = "@";
                                    r42.Value2 = a.Mabn;
                                    r42.Columns.AutoFit();
                                    COMExcel.Range r43 = (COMExcel.Range)exSheet.Cells[i, 43];
                                    r43.NumberFormat = "@";
                                    var kp = (from k1 in _dataContext.KPhongs.Where(p => p.MaKP == a.Makp) select new { k1.TenKP }).ToList();
                                    if (kp.Count > 0)
                                    { r43.Value2 = kp.First().TenKP; }
                                    else
                                    { r43.Value2 = a.Makp; }
                                    r43.Columns.AutoFit();
                                    COMExcel.Range r44 = (COMExcel.Range)exSheet.Cells[i, 44];
                                    r44.Value2 = "";
                                    r44.Columns.AutoFit();
                                }
                                exApp.Visible = true;//Ẩn hiện chương trình
                                exQLBV.SaveAs("C:\\Bieu79.xls", COMExcel.XlFileFormat.xlWorkbookNormal,
                                                null, null, false, false,
                                                COMExcel.XlSaveAsAccessMode.xlExclusive,
                                                false, false, false, false, false);
                                //exQLBV.Close(false, false, false);
                                //exApp.Quit(); // thoát ứng dụng
                                System.Runtime.InteropServices.Marshal.ReleaseComObject(exQLBV);
                                System.Runtime.InteropServices.Marshal.ReleaseComObject(exApp);
                            }
                            #endregion
                        }
                    }
                }
                // ngày tt
                else {
                    if (cbosx.SelectedIndex == 0 || cbosx.SelectedIndex == 1)
                    {
                        DateTime ngaytu = System.DateTime.Now.Date;
                        DateTime ngayden = System.DateTime.Now.Date;
                        ngaytu = DungChung.Ham.NgayTu(lupNgaytu.DateTime);
                        ngayden = DungChung.Ham.NgayDen(lupngayden.DateTime);
                        var q = (from bn in _dataContext.BenhNhans.Where(p => p.DTuong == DoiTuongKham).Where(p => p.NoiTru == 1)
                                 join vp in _dataContext.VienPhis.Where(p => p.NgayTT >= ngaytu).Where(p => p.NgayTT <= ngayden) on bn.MaBNhan equals vp.MaBNhan
                                 join vpct in _dataContext.VienPhicts.Where(p => p.TrongBH == 1) on vp.idVPhi equals vpct.idVPhi
                                 join dv in _dataContext.DichVus on vpct.MaDV equals dv.MaDV
                                 join vv in _dataContext.VaoViens on bn.MaBNhan equals vv.MaBNhan
                                 join rv in _dataContext.RaViens on bn.MaBNhan equals rv.MaBNhan
                                 group new { bn, rv, dv, vpct, vp } by new { vp.NgayTT, bn.NoiTinh, vp.MaBNhan, rv.MaKP, vv.NgayVao, bn.TenBNhan, bn.NamSinh, bn.GTinh, bn.SThe, bn.DChi, bn.MaCS, bn.Tuyen, bn.HanBHTu, bn.HanBHDen, rv.NgayRa, rv.SoNgaydt, rv.MaICD, } into kq
                                 select new
                                 {
                                     dt = kq.Key.SThe.Substring(0, 2),
                                    // kq.Key.MaBNhan,
                                     NoiTinh = kq.Key.NoiTinh,
                                     Tuyen = kq.Key.Tuyen,
                                     Makp = kq.Key.MaKP,
                                     MaBNhan = kq.Key.MaBNhan,
                                     TenBNhan = kq.Key.TenBNhan,
                                     NSinh = kq.Key.NamSinh,
                                     SThe = kq.Key.SThe,
                                     Nam = kq.Key.GTinh,
                                     GTinh = kq.Key.GTinh,
                                     MaCS = kq.Key.MaCS,
                                     MaICD = kq.Key.MaICD,
                                     Ngaykham = kq.Key.NgayVao,
                                     Ngayvao = kq.Key.NgayVao,
                                     Ngaytt = kq.Key.NgayTT,
                                     Ngayra = kq.Key.NgayRa,
                                     Songay = kq.Key.SoNgaydt,
                                     //TenKP = kq.Key.TenKP,
                                     BHtu = kq.Key.HanBHTu,
                                     BHden = kq.Key.HanBHDen,
                                     Diachi = kq.Key.DChi,
                                    // Mabn = kq.Key.MaBNhan,
                                     // Ngaykham = kq.Key.NgayKham.Value.Day,
                                     Thuoc = kq.Where(p => p.dv.IDNhom == idThuoc).Sum(p => p.vpct.ThanhTien),
                                     CDHA = kq.Where(p => p.dv.IDNhom == idCDHA).Sum(p => p.vpct.ThanhTien),
                                     Congkham = kq.Where(p => p.dv.IDNhom == idNgayGiuong).Sum(p => p.vpct.ThanhTien),
                                     Xetnghiem = kq.Where(p => p.dv.IDNhom == idXN).Sum(p => p.vpct.ThanhTien),
                                     Mau = kq.Where(p => p.dv.IDNhom == idMau).Sum(p => p.vpct.ThanhTien),
                                     TTPT = kq.Where(p => p.dv.IDNhom == idTTPT).Sum(p => p.vpct.ThanhTien),
                                     VTYT = kq.Where(p => p.dv.IDNhom == idVTYT).Sum(p => p.vpct.ThanhTien),
                                     DVKTC = kq.Where(p => p.dv.IDNhom == idDVKTC).Sum(p => p.vpct.ThanhTien),
                                     ThuocKTCG = kq.Where(p => p.dv.IDNhom == idThuocUngThuCTG).Sum(p => p.vpct.ThanhTien),
                                     CPVanchuyen = kq.Where(p => p.dv.IDNhom == idChiPhiVC).Sum(p => p.vpct.ThanhTien),
                                     ThanhTien = kq.Sum(p => p.vpct.ThanhTien),
                                     Tongchi = kq.Sum(p => p.vpct.ThanhTien),
                                     Tongcong = kq.Sum(p => p.vpct.ThanhTien),
                                     Nguoibenhchitra = kq.Sum(p => p.vpct.TienBN),
                                     TongcongBHYT = kq.Sum(p => p.vpct.TienBH),

                                 }).ToList().Select(x => new
                                 {
                                     dt = x.SThe.Substring(0, 2),
                                    // x.MaBNhan,
                                     NoiTinh = x.NoiTinh,
                                     Tuyen = x.Tuyen,
                                     Makp = x.Makp,
                                     MaBNhan = x.MaBNhan,
                                     TenBNhan = x.TenBNhan,
                                     NSinh = x.NSinh,
                                     SThe = x.SThe,
                                     Nam = x.GTinh,
                                     GTinh = x.GTinh,
                                     MaCS = x.MaCS,
                                     MaICD = x.MaICD,
                                     Ngaykham = x.Ngayvao,
                                     Ngayvao = x.Ngayvao,
                                     Ngayra = x.Ngayra,
                                     Ngaytt = x.Ngaytt.Value.Date,
                                     Songay = x.Songay,
                                     //TenKP = kq.Key.TenKP,
                                     BHtu = x.BHtu,
                                     BHden = x.BHden,
                                     Diachi = x.Diachi,
                                     Mabn = x.MaBNhan,
                                     // Ngaykham = kq.Key.NgayKham.Value.Day,
                                     Thuoc = x.Thuoc,
                                     x.CDHA,
                                     x.Congkham,
                                     x.Xetnghiem,
                                     x.Mau,
                                     x.TTPT,
                                     x.VTYT,
                                     x.DVKTC,
                                     x.ThuocKTCG,
                                     x.CPVanchuyen,
                                     x.ThanhTien,
                                     x.Tongchi,
                                     x.Tongcong,
                                     x.Nguoibenhchitra,
                                     x.TongcongBHYT,
                                 }).ToList();
                        if (string.IsNullOrEmpty(lupKhoaphong.Text))
                        {
                            frmIn frm = new frmIn();
                            BaoCao.rep80aHD rep = new BaoCao.rep80aHD();
                            if (cbosx.SelectedIndex == 0)
                            {
                                q = q.OrderBy(p => p.MaBNhan).ThenBy(p => p.NoiTinh).ThenBy(p => p.Tuyen).ToList();
                                rep.DataSource = q.ToList();
                            }
                            else
                            {
                                if (cbosx.SelectedIndex == 1)
                                {
                                    q = q.OrderBy(p => p.NoiTinh).ThenBy(p => p.Tuyen).ThenBy(p => p.Ngaytt).ThenBy(p => p.dt).ToList();
                                    rep.DataSource = q;
                                }
                                else
                                {
                                    // cần join đến bảng đối tượng
                                    q = q.OrderBy(p => p.NoiTinh).ThenBy(p => p.dt).ThenBy(p => p.Ngaytt).ToList();
                                    rep.DataSource = q.ToList();
                                }
                            }
                            rep.Ngaythang.Value = theoquy();
                            rep.MaCS.Value = DungChung.Bien.MaBV;
                            rep.TenCQ.Value = DungChung.Bien.TenCQ.ToUpper();
                            double st = 0;
                            st = q.Sum(p => p.TongcongBHYT);
                            rep.Sotien.Value = QLBV_Library.QLBV_Ham.DocTienBangChu(st, " đồng.");
                            rep.BindingData();
                            rep.CreateDocument();
                            // rep.DataMember = "Table";
                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                            frm.ShowDialog();
                            #region xuat Excel
                            if (Xuatex.Checked)
                            {

                                COMExcel.Application exApp = new COMExcel.Application();
                                COMExcel.Workbook exQLBV = exApp.Workbooks.Add(
                                          COMExcel.XlWBATemplate.xlWBATWorksheet);
                                COMExcel.Worksheet exSheet = (COMExcel.Worksheet)exQLBV.Worksheets[1];
                                //exSheet.Activate();
                                exSheet.Name = "NoiTru";// gán tên sheet
                                int i = 1;
                                string[] _arr = new string[44] { "stt", "hoten", "namsinh", "gioitinh", "mathe", "ma_dkbd", "mabenh", "ngay_vao", "ngay_ra", "ngaydtr", "t_tongchi", "t_xn", "t_cdha", "t_thuoc", "t_mau", "t_pttt", "t_vtytth", "t_vtyttt", "t_dvktc", "t_ktg", "t_kham", "t_vchuyen", "t_bnct", "t_bhtt", "t_ngoaids", "lydo_vv", "benhkhac", "noikcb", "khoa", "thang_qt", "nam_qt", "gt_tu", "gt_den", "diachi", "giamdinh", "t_xuattoan", "lydo_xt", "t_datuyen", "t_vuottran", "loaikcb", "noi_ttoan", "sophieu", "ma_khoa", "keysl" };
                                int k = 0;
                                foreach (var b in _arr)
                                {
                                    k++;
                                    COMExcel.Range r = (COMExcel.Range)exSheet.Cells[1, k];
                                    r.Value2 = b.ToString();
                                    r.Columns.AutoFit();
                                }
                                foreach (var a in q)
                                {
                                    i++;
                                    COMExcel.Range r1 = (COMExcel.Range)exSheet.Cells[i, 1];
                                    r1.NumberFormat = "0";
                                    r1.Value2 = i - 1;
                                    r1.Columns.AutoFit();
                                    COMExcel.Range r2 = (COMExcel.Range)exSheet.Cells[i, 2];
                                    r2.NumberFormat = "@";
                                    if (Font == 0)
                                    {
                                        r2.Value2 = convert(a.TenBNhan);
                                    }
                                    else
                                        r2.Value2 = a.TenBNhan;
                                    r2.Columns.AutoFit();
                                    COMExcel.Range r3 = (COMExcel.Range)exSheet.Cells[i, 3];
                                    r3.NumberFormat = "0";
                                    r3.Value2 = a.NSinh;
                                    r3.Columns.AutoFit();
                                    COMExcel.Range r4 = (COMExcel.Range)exSheet.Cells[i, 4];
                                    r4.NumberFormat = "0";
                                    r4.Value2 = a.GTinh;
                                    r4.Columns.AutoFit();
                                    COMExcel.Range r5 = (COMExcel.Range)exSheet.Cells[i, 5];
                                    r5.NumberFormat = "@";
                                    r5.Value2 = convertFont(a.SThe);
                                    r5.Columns.AutoFit();
                                    COMExcel.Range r6 = (COMExcel.Range)exSheet.Cells[i, 6];
                                    r6.NumberFormat = "@";
                                    r6.Value2 = convertFont(a.MaCS);
                                    r6.Columns.AutoFit();
                                    COMExcel.Range r7 = (COMExcel.Range)exSheet.Cells[i, 7];
                                    r7.NumberFormat = "@";
                                    r7.Value2 = convertFont(a.MaICD);
                                    r7.Columns.AutoFit();
                                    COMExcel.Range r8 = (COMExcel.Range)exSheet.Cells[i, 8];
                                    r8.NumberFormat = "@";
                                    if (a.Ngayvao != null)
                                    {
                                        r8.Value2 = convertFont(a.Ngayvao.ToString().Substring(0, 10));
                                    }

                                    r8.Columns.AutoFit();
                                    COMExcel.Range r9 = (COMExcel.Range)exSheet.Cells[i, 9];
                                    r9.NumberFormat = "@";
                                    if (a.Ngayra != null)
                                    {
                                        r9.Value2 = convertFont(a.Ngayra.ToString().Substring(0, 10));
                                    }

                                    r9.Columns.AutoFit();
                                    COMExcel.Range r10 = (COMExcel.Range)exSheet.Cells[i, 10];
                                    r10.NumberFormat = "0";
                                    r10.Value2 = a.Songay;
                                    r10.Columns.AutoFit();
                                    COMExcel.Range r11 = (COMExcel.Range)exSheet.Cells[i, 11];
                                    r11.NumberFormat = "0";
                                    r11.Value2 = a.Tongcong;
                                    r11.Columns.AutoFit();
                                    COMExcel.Range r12 = (COMExcel.Range)exSheet.Cells[i, 12];
                                    r12.NumberFormat = "0";
                                    r12.Value2 = a.Xetnghiem;
                                    r12.Columns.AutoFit();
                                    COMExcel.Range r13 = (COMExcel.Range)exSheet.Cells[i, 13];
                                    r13.NumberFormat = "0";
                                    r13.Value2 = a.CDHA;
                                    r13.Columns.AutoFit();
                                    COMExcel.Range r14 = (COMExcel.Range)exSheet.Cells[i, 14];
                                    r14.NumberFormat = "0";
                                    r14.Value2 = a.Thuoc;
                                    r14.Columns.AutoFit();
                                    COMExcel.Range r15 = (COMExcel.Range)exSheet.Cells[i, 15];
                                    r15.NumberFormat = "0";
                                    r15.Value2 = a.Mau;
                                    r15.Columns.AutoFit();
                                    COMExcel.Range r16 = (COMExcel.Range)exSheet.Cells[i, 16];
                                    r16.NumberFormat = "0";
                                    r16.Value2 = a.TTPT;
                                    r16.Columns.AutoFit();
                                    COMExcel.Range r17 = (COMExcel.Range)exSheet.Cells[i, 17];
                                    r17.NumberFormat = "0";
                                    r17.Value2 = a.VTYT;
                                    r17.Columns.AutoFit();
                                    COMExcel.Range r18 = (COMExcel.Range)exSheet.Cells[i, 18];
                                    r18.NumberFormat = "0";
                                    r18.Value2 = 0;
                                    r18.Columns.AutoFit();
                                    COMExcel.Range r19 = (COMExcel.Range)exSheet.Cells[i, 19];
                                    r19.NumberFormat = "0";
                                    r19.Value2 = a.DVKTC;
                                    r19.Columns.AutoFit();
                                    COMExcel.Range r20 = (COMExcel.Range)exSheet.Cells[i, 20];
                                    r20.NumberFormat = "0";
                                    r20.Value2 = a.ThuocKTCG;
                                    r20.Columns.AutoFit();
                                    COMExcel.Range r21 = (COMExcel.Range)exSheet.Cells[i, 21];
                                    r21.NumberFormat = "0";
                                    r21.Value2 = a.Congkham;
                                    r21.Columns.AutoFit();
                                    COMExcel.Range r22 = (COMExcel.Range)exSheet.Cells[i, 22];
                                    r22.NumberFormat = "0";
                                    r22.Value2 = a.CPVanchuyen;
                                    r22.Columns.AutoFit();
                                    COMExcel.Range r23 = (COMExcel.Range)exSheet.Cells[i, 23];
                                    r23.NumberFormat = "0";
                                    r23.Value2 = a.Nguoibenhchitra;
                                    r23.Columns.AutoFit();
                                    COMExcel.Range r24 = (COMExcel.Range)exSheet.Cells[i, 24];
                                    r24.NumberFormat = "0";
                                    r24.Value2 = a.TongcongBHYT;
                                    r24.Columns.AutoFit();
                                    COMExcel.Range r25 = (COMExcel.Range)exSheet.Cells[i, 25];
                                    r25.NumberFormat = "0";
                                    r25.Value2 = a.CPVanchuyen;
                                    r25.Columns.AutoFit();
                                    COMExcel.Range r26 = (COMExcel.Range)exSheet.Cells[i, 26];
                                    r26.NumberFormat = "@";
                                    if (a.Tuyen != null && a.Tuyen == 1)
                                    {
                                        r26.Value2 = 1;
                                    }
                                    else
                                        r26.Value2 = 0;

                                    r26.Columns.AutoFit();
                                    COMExcel.Range r27 = (COMExcel.Range)exSheet.Cells[i, 27];
                                    r27.NumberFormat = "@";
                                    r27.Value2 = "";
                                    r27.Columns.AutoFit();
                                    COMExcel.Range r28 = (COMExcel.Range)exSheet.Cells[i, 28];
                                    r28.NumberFormat = "@";
                                    r28.Value2 = convertFont(DungChung.Bien.MaBV);
                                    r28.Columns.AutoFit();
                                    COMExcel.Range r29 = (COMExcel.Range)exSheet.Cells[i, 29];
                                    r29.NumberFormat = "@";
                                    var K = (from kh in _dataContext.KPhongs.Where(p => p.MaKP == a.Makp) select new { kh.TenKP }).ToList();
                                    if (K.Count > 0)
                                    {
                                        r29.Value2 = convertFont(K.First().TenKP);
                                    }
                                    r29.Columns.AutoFit();
                                    COMExcel.Range r30 = (COMExcel.Range)exSheet.Cells[i, 30];
                                    r30.NumberFormat = "0";
                                    if (a.Ngayra != null)
                                    {
                                        r30.Value2 = a.Ngayra.Value.Month;
                                        r30.Columns.AutoFit();
                                    }
                                    COMExcel.Range r31 = (COMExcel.Range)exSheet.Cells[i, 31];
                                    r31.NumberFormat = "0";
                                    if (a.Ngayra != null)
                                    {
                                        r31.Value2 = a.Ngayra.Value.Year;
                                        r31.Columns.AutoFit();
                                    }
                                    COMExcel.Range r32 = (COMExcel.Range)exSheet.Cells[i, 32];
                                    r32.NumberFormat = "@";
                                    if (a.BHtu != null)
                                    {
                                        r32.Value2 = convertFont(a.BHtu.ToString().Substring(0, 10));
                                        r32.Columns.AutoFit();
                                    }
                                    COMExcel.Range r33 = (COMExcel.Range)exSheet.Cells[i, 33];
                                    r33.NumberFormat = "@";
                                    if (a.BHden != null)
                                    {
                                        r33.Value2 = convertFont(a.BHden.ToString().Substring(0, 10));
                                        r33.Columns.AutoFit();
                                    }
                                    COMExcel.Range r34 = (COMExcel.Range)exSheet.Cells[i, 34];
                                    r34.NumberFormat = "@";
                                    r34.Value2 = convertFont(a.Diachi);
                                    r34.Columns.AutoFit();
                                    COMExcel.Range r35 = (COMExcel.Range)exSheet.Cells[i, 35];
                                    r35.Value2 = "";
                                    r35.Columns.AutoFit();
                                    COMExcel.Range r36 = (COMExcel.Range)exSheet.Cells[i, 36];
                                    r36.NumberFormat = "@";
                                    r36.Value2 = 0;
                                    r36.Columns.AutoFit();
                                    COMExcel.Range r37 = (COMExcel.Range)exSheet.Cells[i, 37];
                                    r37.NumberFormat = "@";
                                    r37.Value2 = "";
                                    r37.Columns.AutoFit();
                                    COMExcel.Range r38 = (COMExcel.Range)exSheet.Cells[i, 38];
                                    r38.NumberFormat = "@";
                                    r38.Value2 = 0;
                                    r38.Columns.AutoFit();
                                    COMExcel.Range r39 = (COMExcel.Range)exSheet.Cells[i, 39];
                                    r39.NumberFormat = "0";
                                    r39.Value2 = 0;
                                    r39.Columns.AutoFit();
                                    COMExcel.Range r40 = (COMExcel.Range)exSheet.Cells[i, 40];
                                    r40.NumberFormat = "@";
                                    r40.Value2 = convertFont("NOI");
                                    r40.Columns.AutoFit();
                                    COMExcel.Range r41 = (COMExcel.Range)exSheet.Cells[i, 41];
                                    r41.NumberFormat = "@";
                                    r41.Value2 = convertFont("CSKCB");
                                    r41.Columns.AutoFit();
                                    COMExcel.Range r42 = (COMExcel.Range)exSheet.Cells[i, 42];
                                    r42.NumberFormat = "@";
                                    r42.Value2 = a.Mabn.Value;
                                    r42.Columns.AutoFit();
                                    COMExcel.Range r43 = (COMExcel.Range)exSheet.Cells[i, 43];
                                    r43.NumberFormat = "@";
                                    var kp = (from k1 in _dataContext.KPhongs.Where(p => p.MaKP == a.Makp) select new { k1.TenKP }).ToList();
                                    if (kp.Count > 0)
                                    {
                                        r43.Value2 = convertFont(kp.First().TenKP);
                                    }
                                    else
                                    {
                                        r43.Value2 = a.Makp;
                                    }
                                    r43.Columns.AutoFit();
                                    COMExcel.Range r44 = (COMExcel.Range)exSheet.Cells[i, 44];
                                    r44.Value2 = "";
                                    r44.Columns.AutoFit();
                                }
                                exApp.Visible = true;//Ẩn hiện chương trình
                                exQLBV.SaveAs("C:\\Bieu80.xls", COMExcel.XlFileFormat.xlWorkbookNormal,
                                                null, null, false, false,
                                                COMExcel.XlSaveAsAccessMode.xlExclusive,
                                                false, false, false, false, false);
                                //exQLBV.Close(false, false, false);
                                //exApp.Quit(); // thoát ứng dụng
                                System.Runtime.InteropServices.Marshal.ReleaseComObject(exQLBV);
                                System.Runtime.InteropServices.Marshal.ReleaseComObject(exApp);
                            }
                            #endregion
                        }
                        else
                        {
                            int _MaKP =lupKhoaphong.EditValue == null ? 0: Convert.ToInt32( lupKhoaphong.EditValue);
                            frmIn frm = new frmIn();
                            BaoCao.rep80aHD rep = new BaoCao.rep80aHD();
                            q = q.ToList().Where(p => p.Makp == _MaKP).ToList();
                            rep.DataSource = q.ToList();
                            rep.Ngaythang.Value = theoquy();
                            rep.MaCS.Value = DungChung.Bien.MaBV;
                            rep.TenCQ.Value = DungChung.Bien.TenCQ.ToUpper();
                            rep.paramKhoaPhong.Value = lupKhoaphong.Text.ToUpper();
                            double st = 0;
                            st = q.Where(a => a.Makp == _MaKP).Sum(a => a.TongcongBHYT);
                            rep.Sotien.Value = QLBV_Library.QLBV_Ham.DocTienBangChu(st, " đồng.");
                            rep.BindingData();
                            rep.CreateDocument();
                            //rep.DataMember = "Table";
                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                            frm.ShowDialog();
                            #region xuat Excel
                            if (Xuatex.Checked)
                            {
                                COMExcel.Application exApp = new COMExcel.Application();
                                COMExcel.Workbook exQLBV = exApp.Workbooks.Add(
                                          COMExcel.XlWBATemplate.xlWBATWorksheet);
                                COMExcel.Worksheet exSheet = (COMExcel.Worksheet)exQLBV.Worksheets[1];
                                //exSheet.Activate();
                                exSheet.Name = "NoiTru";// gán tên sheet
                                int i = 1;
                                string[] _arr = new string[44] { "stt", "hoten", "namsinh", "gioitinh", "mathe", "ma_dkbd", "mabenh", "ngay_vao", "ngay_ra", "ngaydtr", "t_tongchi", "t_xn", "t_cdha", "t_thuoc", "t_mau", "t_pttt", "t_vtytth", "t_vtyttt", "t_dvktc", "t_ktg", "t_kham", "t_vchuyen", "t_bnct", "t_bhtt", "t_ngoaids", "lydo_vv", "benhkhac", "noikcb", "khoa", "thang_qt", "nam_qt", "gt_tu", "gt_den", "diachi", "giamdinh", "t_xuattoan", "lydo_xt", "t_datuyen", "t_vuottran", "loaikcb", "noi_ttoan", "sophieu", "ma_khoa", "keysl" };
                                int k = 0;
                                foreach (var b in _arr)
                                {
                                    k++;
                                    COMExcel.Range r = (COMExcel.Range)exSheet.Cells[1, k];
                                    r.Value2 = b.ToString();
                                    r.Columns.AutoFit();
                                }
                                foreach (var a in q)
                                {
                                    i++;
                                    COMExcel.Range r1 = (COMExcel.Range)exSheet.Cells[i, 1];
                                    r1.NumberFormat = "0";
                                    r1.Value2 = i - 1;
                                    r1.Columns.AutoFit();
                                    COMExcel.Range r2 = (COMExcel.Range)exSheet.Cells[i, 2];
                                    r2.NumberFormat = "@";
                                    r2.Value2 = convertFont(a.TenBNhan);
                                    r2.Columns.AutoFit();
                                    COMExcel.Range r3 = (COMExcel.Range)exSheet.Cells[i, 3];
                                    r3.NumberFormat = "0";
                                    r3.Value2 = a.NSinh;
                                    r3.Columns.AutoFit();
                                    COMExcel.Range r4 = (COMExcel.Range)exSheet.Cells[i, 4];
                                    r4.NumberFormat = "0";
                                    r4.Value2 = a.GTinh;
                                    r4.Columns.AutoFit();
                                    COMExcel.Range r5 = (COMExcel.Range)exSheet.Cells[i, 5];
                                    r5.NumberFormat = "@";
                                    r5.Value2 = convertFont(a.SThe);
                                    r5.Columns.AutoFit();
                                    COMExcel.Range r6 = (COMExcel.Range)exSheet.Cells[i, 6];
                                    r6.NumberFormat = "@";
                                    r6.Value2 = convertFont(a.MaCS);
                                    r6.Columns.AutoFit();
                                    COMExcel.Range r7 = (COMExcel.Range)exSheet.Cells[i, 7];
                                    r7.NumberFormat = "@";
                                    r7.Value2 = convertFont(a.MaICD);
                                    r7.Columns.AutoFit();
                                    COMExcel.Range r8 = (COMExcel.Range)exSheet.Cells[i, 8];
                                    r8.NumberFormat = "@";
                                    if (a.Ngayvao != null)
                                        r8.Value2 = convertFont(a.Ngayvao.ToString().Substring(0, 10));
                                    r8.Columns.AutoFit();
                                    COMExcel.Range r9 = (COMExcel.Range)exSheet.Cells[i, 9];
                                    r9.NumberFormat = "@";
                                    if (a.Ngayra != null)
                                        r9.Value2 = convertFont(a.Ngayra.ToString().Substring(0, 10));
                                    r9.Columns.AutoFit();
                                    COMExcel.Range r10 = (COMExcel.Range)exSheet.Cells[i, 10];
                                    r10.NumberFormat = "0";
                                    r10.Value2 = a.Songay;
                                    r10.Columns.AutoFit();
                                    COMExcel.Range r11 = (COMExcel.Range)exSheet.Cells[i, 11];
                                    r11.NumberFormat = "0";
                                    r11.Value2 = a.Tongcong;
                                    r11.Columns.AutoFit();
                                    COMExcel.Range r12 = (COMExcel.Range)exSheet.Cells[i, 12];
                                    r12.NumberFormat = "0";
                                    r12.Value2 = a.Xetnghiem;
                                    r12.Columns.AutoFit();
                                    COMExcel.Range r13 = (COMExcel.Range)exSheet.Cells[i, 13];
                                    r13.NumberFormat = "0";
                                    r13.Value2 = a.CDHA;
                                    r13.Columns.AutoFit();
                                    COMExcel.Range r14 = (COMExcel.Range)exSheet.Cells[i, 14];
                                    r14.NumberFormat = "0";
                                    r14.Value2 = a.Thuoc;
                                    r14.Columns.AutoFit();
                                    COMExcel.Range r15 = (COMExcel.Range)exSheet.Cells[i, 15];
                                    r15.NumberFormat = "0";
                                    r15.Value2 = a.Mau;
                                    r15.Columns.AutoFit();
                                    COMExcel.Range r16 = (COMExcel.Range)exSheet.Cells[i, 16];
                                    r16.NumberFormat = "0";
                                    r16.Value2 = a.TTPT;
                                    r16.Columns.AutoFit();
                                    COMExcel.Range r17 = (COMExcel.Range)exSheet.Cells[i, 17];
                                    r17.NumberFormat = "0";
                                    r17.Value2 = a.VTYT;
                                    r17.Columns.AutoFit();
                                    COMExcel.Range r18 = (COMExcel.Range)exSheet.Cells[i, 18];
                                    r18.NumberFormat = "0";
                                    r18.Value2 = 0;
                                    r18.Columns.AutoFit();
                                    COMExcel.Range r19 = (COMExcel.Range)exSheet.Cells[i, 19];
                                    r19.NumberFormat = "0";
                                    r19.Value2 = a.DVKTC;
                                    r19.Columns.AutoFit();
                                    COMExcel.Range r20 = (COMExcel.Range)exSheet.Cells[i, 20];
                                    r20.NumberFormat = "0";
                                    r20.Value2 = a.ThuocKTCG;
                                    r20.Columns.AutoFit();
                                    COMExcel.Range r21 = (COMExcel.Range)exSheet.Cells[i, 21];
                                    r21.NumberFormat = "0";
                                    r21.Value2 = a.Congkham;
                                    r21.Columns.AutoFit();
                                    COMExcel.Range r22 = (COMExcel.Range)exSheet.Cells[i, 22];
                                    r22.NumberFormat = "0";
                                    r22.Value2 = a.CPVanchuyen;
                                    r22.Columns.AutoFit();
                                    COMExcel.Range r23 = (COMExcel.Range)exSheet.Cells[i, 23];
                                    r23.NumberFormat = "0";
                                    r23.Value2 = a.Nguoibenhchitra;
                                    r23.Columns.AutoFit();
                                    COMExcel.Range r24 = (COMExcel.Range)exSheet.Cells[i, 24];
                                    r24.NumberFormat = "0";
                                    r24.Value2 = a.TongcongBHYT;
                                    r24.Columns.AutoFit();
                                    COMExcel.Range r25 = (COMExcel.Range)exSheet.Cells[i, 25];
                                    r25.NumberFormat = "0";
                                    r25.Value2 = a.CPVanchuyen;
                                    r25.Columns.AutoFit();
                                    COMExcel.Range r26 = (COMExcel.Range)exSheet.Cells[i, 26];
                                    r26.NumberFormat = "@";
                                    if (a.Tuyen != null && a.Tuyen == 1)
                                    {
                                        r26.Value2 = 1;
                                    }
                                    else
                                        r26.Value2 = 0;
                                    r26.Columns.AutoFit();
                                    COMExcel.Range r27 = (COMExcel.Range)exSheet.Cells[i, 27];
                                    r27.NumberFormat = "@";
                                    r27.Value2 = "";
                                    r27.Columns.AutoFit();
                                    COMExcel.Range r28 = (COMExcel.Range)exSheet.Cells[i, 28];
                                    r28.NumberFormat = "@";
                                    r28.Value2 = convertFont(DungChung.Bien.MaBV);
                                    r28.Columns.AutoFit();
                                    COMExcel.Range r29 = (COMExcel.Range)exSheet.Cells[i, 29];
                                    r29.NumberFormat = "@";
                                    var K = (from kh in _dataContext.KPhongs.Where(p => p.MaKP == a.Makp) select new { kh.TenKP }).ToList();
                                    if (K.Count > 0)
                                    {
                                        r29.Value2 = convertFont(K.First().TenKP);
                                    }
                                    //r29.Value2 = a.Makp;
                                    r29.Columns.AutoFit();
                                    COMExcel.Range r30 = (COMExcel.Range)exSheet.Cells[i, 30];
                                    r30.NumberFormat = "0";
                                    if (a.Ngayra != null)
                                    {
                                        r30.Value2 = a.Ngayra.Value.Month;
                                        r30.Columns.AutoFit();
                                    }
                                    COMExcel.Range r31 = (COMExcel.Range)exSheet.Cells[i, 31];
                                    r31.NumberFormat = "0";
                                    if (a.Ngayra != null)
                                    {
                                        r31.Value2 = a.Ngayra.Value.Year;
                                        r31.Columns.AutoFit();
                                    }
                                    COMExcel.Range r32 = (COMExcel.Range)exSheet.Cells[i, 32];
                                    r32.NumberFormat = "@";
                                    if (a.BHtu != null)
                                    {
                                        r32.Value2 = convertFont(a.BHtu.ToString().Substring(0, 10));
                                        r32.Columns.AutoFit();
                                    }
                                    COMExcel.Range r33 = (COMExcel.Range)exSheet.Cells[i, 33];
                                    r33.NumberFormat = "@";
                                    if (a.BHden != null)
                                    {
                                        r33.Value2 = convertFont(a.BHden.ToString().Substring(0, 10));
                                        r33.Columns.AutoFit();
                                    }
                                    COMExcel.Range r34 = (COMExcel.Range)exSheet.Cells[i, 34];
                                    r34.NumberFormat = "@";
                                    r34.Value2 = convertFont(a.Diachi);
                                    r34.Columns.AutoFit();
                                    COMExcel.Range r35 = (COMExcel.Range)exSheet.Cells[i, 35];
                                    r35.Value2 = "";
                                    r35.Columns.AutoFit();
                                    COMExcel.Range r36 = (COMExcel.Range)exSheet.Cells[i, 36];
                                    r36.NumberFormat = "@";
                                    r36.Value2 = 0;
                                    r36.Columns.AutoFit();
                                    COMExcel.Range r37 = (COMExcel.Range)exSheet.Cells[i, 37];
                                    r37.Value2 = "";
                                    r37.Columns.AutoFit();
                                    COMExcel.Range r38 = (COMExcel.Range)exSheet.Cells[i, 38];
                                    r38.NumberFormat = "@";
                                    r38.Value2 = 0;
                                    r38.Columns.AutoFit();
                                    COMExcel.Range r39 = (COMExcel.Range)exSheet.Cells[i, 39];
                                    r39.NumberFormat = "0";
                                    r39.Value2 = 0;
                                    r39.Columns.AutoFit();
                                    COMExcel.Range r40 = (COMExcel.Range)exSheet.Cells[i, 40];
                                    r40.NumberFormat = "@";
                                    r40.Value2 = convertFont("NOI");
                                    r40.Columns.AutoFit();
                                    COMExcel.Range r41 = (COMExcel.Range)exSheet.Cells[i, 41];
                                    r41.NumberFormat = "@";
                                    r41.Value2 = convertFont("CSKCB");
                                    r41.Columns.AutoFit();
                                    COMExcel.Range r42 = (COMExcel.Range)exSheet.Cells[i, 42];
                                    r42.NumberFormat = "@";
                                    r42.Value2 = a.Mabn.Value;
                                    r42.Columns.AutoFit();
                                    COMExcel.Range r43 = (COMExcel.Range)exSheet.Cells[i, 43];
                                    r43.NumberFormat = "@";
                                    var kp = (from k1 in _dataContext.KPhongs.Where(p => p.MaKP == a.Makp) select new { k1.TenKP }).ToList();
                                    if (kp.Count > 0)
                                    { r43.Value2 = convertFont(kp.First().TenKP); }
                                    else
                                    { r43.Value2 = a.Makp; }
                                    r43.Columns.AutoFit();
                                    COMExcel.Range r44 = (COMExcel.Range)exSheet.Cells[i, 44];
                                    r44.Value2 = "";
                                    r44.Columns.AutoFit();
                                }
                                exApp.Visible = true;//Ẩn hiện chương trình
                                exQLBV.SaveAs("C:\\Bieu79.xls", COMExcel.XlFileFormat.xlWorkbookNormal,
                                                null, null, false, false,
                                                COMExcel.XlSaveAsAccessMode.xlExclusive,
                                                false, false, false, false, false);
                                //exQLBV.Close(false, false, false);
                                //exApp.Quit(); // thoát ứng dụng
                                System.Runtime.InteropServices.Marshal.ReleaseComObject(exQLBV);
                                System.Runtime.InteropServices.Marshal.ReleaseComObject(exApp);
                            }
                            #endregion
                        }
                    }
                    else
                    {
                        DateTime ngaytu = System.DateTime.Now.Date;
                        DateTime ngayden = System.DateTime.Now.Date;
                        ngaytu = DungChung.Ham.NgayTu(lupNgaytu.DateTime);
                        ngayden = DungChung.Ham.NgayDen(lupngayden.DateTime);
                        var q = (from bn in _dataContext.BenhNhans.Where(p => p.DTuong == DoiTuongKham).Where(p => p.NoiTru == 1).Where(p => p.SThe.Length > 2)
                                 join dt in _dataContext.DTuongs on bn.SThe.Substring(0, 2) equals dt.MaDTuong
                                 join vp in _dataContext.VienPhis.Where(p => p.NgayTT >= ngaytu).Where(p => p.NgayTT <= ngayden) on bn.MaBNhan equals vp.MaBNhan
                                 join vpct in _dataContext.VienPhicts.Where(p => p.TrongBH == 1) on vp.idVPhi equals vpct.idVPhi
                                 join dv in _dataContext.DichVus on vpct.MaDV equals dv.MaDV
                                 join vv in _dataContext.VaoViens on bn.MaBNhan equals vv.MaBNhan
                                 join rv in _dataContext.RaViens on bn.MaBNhan equals rv.MaBNhan
                                 group new { bn, rv, dv, vpct, vp } by new { dt.Nhom, vp.NgayTT, bn.NoiTinh, vp.MaBNhan, rv.MaKP, vv.NgayVao, bn.TenBNhan, bn.NamSinh, bn.GTinh, bn.SThe, bn.DChi, bn.MaCS, bn.Tuyen, bn.HanBHTu, bn.HanBHDen, rv.NgayRa, rv.SoNgaydt, rv.MaICD, } into kq
                                 select new
                                 {
                                     dt = kq.Key.SThe.Substring(0, 2),
                                     Nhom = kq.Key.Nhom,
                                    
                                     kq.Key.NgayTT,
                                     NoiTinh = kq.Key.NoiTinh,
                                     Tuyen = kq.Key.Tuyen,
                                     Makp = kq.Key.MaKP,
                                     MaBNhan = kq.Key.MaBNhan,
                                     TenBNhan = kq.Key.TenBNhan,
                                     NSinh = kq.Key.NamSinh,
                                     SThe = kq.Key.SThe,
                                     Nam = kq.Key.GTinh,
                                     GTinh = kq.Key.GTinh,
                                     MaCS = kq.Key.MaCS,
                                     MaICD = kq.Key.MaICD,
                                     Ngaykham = kq.Key.NgayVao,
                                     Ngayvao = kq.Key.NgayVao,
                                     Ngayra = kq.Key.NgayRa,
                                     Songay = kq.Key.SoNgaydt,
                                     //TenKP = kq.Key.TenKP,
                                     BHtu = kq.Key.HanBHTu,
                                     BHden = kq.Key.HanBHDen,
                                     Diachi = kq.Key.DChi,
                                     Mabn = kq.Key.MaBNhan,
                                     // Ngaykham = kq.Key.NgayKham.Value.Day,
                                     Thuoc = kq.Where(p => p.dv.IDNhom == idThuoc).Sum(p => p.vpct.ThanhTien),
                                     CDHA = kq.Where(p => p.dv.IDNhom == idCDHA).Sum(p => p.vpct.ThanhTien),
                                     Congkham = kq.Where(p => p.dv.IDNhom == idNgayGiuong).Sum(p => p.vpct.ThanhTien),
                                     Xetnghiem = kq.Where(p => p.dv.IDNhom == idXN).Sum(p => p.vpct.ThanhTien),
                                     Mau = kq.Where(p => p.dv.IDNhom == idMau).Sum(p => p.vpct.ThanhTien),
                                     TTPT = kq.Where(p => p.dv.IDNhom == idTTPT).Sum(p => p.vpct.ThanhTien),
                                     VTYT = kq.Where(p => p.dv.IDNhom == idVTYT).Sum(p => p.vpct.ThanhTien),
                                     DVKTC = kq.Where(p => p.dv.IDNhom == idDVKTC).Sum(p => p.vpct.ThanhTien),
                                     ThuocKTCG = kq.Where(p => p.dv.IDNhom == idThuocUngThuCTG).Sum(p => p.vpct.ThanhTien),
                                     CPVanchuyen = kq.Where(p => p.dv.IDNhom == idChiPhiVC).Sum(p => p.vpct.ThanhTien),
                                     ThanhTien = kq.Sum(p => p.vpct.ThanhTien),
                                     Tongchi = kq.Sum(p => p.vpct.ThanhTien),
                                     Tongcong = kq.Sum(p => p.vpct.ThanhTien),
                                     Nguoibenhchitra = kq.Sum(p => p.vpct.TienBN),
                                     TongcongBHYT = kq.Sum(p => p.vpct.TienBH),

                                 }).ToList();
                        if (string.IsNullOrEmpty(lupKhoaphong.Text))
                        {
                            frmIn frm = new frmIn();
                            BaoCao.rep80aHD rep = new BaoCao.rep80aHD();
                            //q = q.OrderBy(p => p.Ngayra).OrderBy(p => p.SThe).OrderBy(p => p.Nhom).OrderBy(p => p.NoiTinh).OrderBy(p => p.Tuyen).ToList();

                            q = q.OrderBy(p => p.NoiTinh).ThenBy(p => p.Tuyen).ThenBy(p => p.Nhom).ThenBy(p => p.dt).ThenBy(p => p.Ngayra).ToList();
                            rep.DataSource = q.ToList();

                            rep.Ngaythang.Value = theoquy();
                            rep.MaCS.Value = DungChung.Bien.MaBV;
                            rep.TenCQ.Value = DungChung.Bien.TenCQ.ToUpper();
                            double st = 0;
                            st = q.Sum(p => p.TongcongBHYT);
                            rep.Sotien.Value = QLBV_Library.QLBV_Ham.DocTienBangChu(st, " đồng.");
                            rep.BindingData();
                            rep.CreateDocument();
                            // rep.DataMember = "Table";
                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                            frm.ShowDialog();
                            #region xuat Excel
                            if (Xuatex.Checked)
                            {
                                COMExcel.Application exApp = new COMExcel.Application();
                                COMExcel.Workbook exQLBV = exApp.Workbooks.Add(
                                          COMExcel.XlWBATemplate.xlWBATWorksheet);
                                COMExcel.Worksheet exSheet = (COMExcel.Worksheet)exQLBV.Worksheets[1];
                                //exSheet.Activate();
                                exSheet.Name = "NoiTru";// gán tên sheet
                                int i = 1;
                                string[] _arr = new string[44] { "stt", "hoten", "namsinh", "gioitinh", "mathe", "ma_dkbd", "mabenh", "ngay_vao", "ngay_ra", "ngaydtr", "t_tongchi", "t_xn", "t_cdha", "t_thuoc", "t_mau", "t_pttt", "t_vtytth", "t_vtyttt", "t_dvktc", "t_ktg", "t_kham", "t_vchuyen", "t_bnct", "t_bhtt", "t_ngoaids", "lydo_vv", "benhkhac", "noikcb", "khoa", "thang_qt", "nam_qt", "gt_tu", "gt_den", "diachi", "giamdinh", "t_xuattoan", "lydo_xt", "t_datuyen", "t_vuottran", "loaikcb", "noi_ttoan", "sophieu", "ma_khoa", "keysl" };
                                int k = 0;
                                foreach (var b in _arr)
                                {
                                    k++;
                                    COMExcel.Range r = (COMExcel.Range)exSheet.Cells[1, k];
                                    r.Value2 = b.ToString();
                                    r.Columns.AutoFit();
                                }
                                foreach (var a in q)
                                {
                                    i++;
                                    COMExcel.Range r1 = (COMExcel.Range)exSheet.Cells[i, 1];
                                    r1.NumberFormat = "0";
                                    r1.Value2 = i - 1;
                                    r1.Columns.AutoFit();
                                    COMExcel.Range r2 = (COMExcel.Range)exSheet.Cells[i, 2];
                                    r2.NumberFormat = "@";
                                    r2.Value2 = convertFont(a.TenBNhan);
                                    r2.Columns.AutoFit();
                                    COMExcel.Range r3 = (COMExcel.Range)exSheet.Cells[i, 3];
                                    r3.NumberFormat = "0";
                                    r3.Value2 = a.NSinh;
                                    r3.Columns.AutoFit();
                                    COMExcel.Range r4 = (COMExcel.Range)exSheet.Cells[i, 4];
                                    r4.NumberFormat = "0";
                                    r4.Value2 = a.GTinh;
                                    r4.Columns.AutoFit();
                                    COMExcel.Range r5 = (COMExcel.Range)exSheet.Cells[i, 5];
                                    r5.NumberFormat = "@";
                                    r5.Value2 = convertFont(a.SThe);
                                    r5.Columns.AutoFit();
                                    COMExcel.Range r6 = (COMExcel.Range)exSheet.Cells[i, 6];
                                    r6.NumberFormat = "@";
                                    r6.Value2 = convertFont(a.MaCS);
                                    r6.Columns.AutoFit();
                                    COMExcel.Range r7 = (COMExcel.Range)exSheet.Cells[i, 7];
                                    r7.NumberFormat = "@";
                                    r7.Value2 = convertFont(a.MaICD);
                                    r7.Columns.AutoFit();
                                    COMExcel.Range r8 = (COMExcel.Range)exSheet.Cells[i, 8];
                                    r8.NumberFormat = "@";
                                    if (a.Ngayvao != null)
                                        r8.Value2 = convertFont(a.Ngayvao.ToString().Substring(0, 10));
                                    r8.Columns.AutoFit();
                                    COMExcel.Range r9 = (COMExcel.Range)exSheet.Cells[i, 9];
                                    r9.NumberFormat = "@";
                                    if (a.Ngayra != null)
                                        r9.Value2 = convertFont(a.Ngayra.ToString().Substring(0, 10));
                                    r9.Columns.AutoFit();
                                    COMExcel.Range r10 = (COMExcel.Range)exSheet.Cells[i, 10];
                                    r10.NumberFormat = "0";
                                    r10.Value2 = a.Songay;
                                    r10.Columns.AutoFit();
                                    COMExcel.Range r11 = (COMExcel.Range)exSheet.Cells[i, 11];
                                    r11.NumberFormat = "0";
                                    r11.Value2 = a.Tongcong;
                                    r11.Columns.AutoFit();
                                    COMExcel.Range r12 = (COMExcel.Range)exSheet.Cells[i, 12];
                                    r12.NumberFormat = "0";
                                    r12.Value2 = a.Xetnghiem;
                                    r12.Columns.AutoFit();
                                    COMExcel.Range r13 = (COMExcel.Range)exSheet.Cells[i, 13];
                                    r13.NumberFormat = "0";
                                    r13.Value2 = a.CDHA;
                                    r13.Columns.AutoFit();
                                    COMExcel.Range r14 = (COMExcel.Range)exSheet.Cells[i, 14];
                                    r14.NumberFormat = "0";
                                    r14.Value2 = a.Thuoc;
                                    r14.Columns.AutoFit();
                                    COMExcel.Range r15 = (COMExcel.Range)exSheet.Cells[i, 15];
                                    r15.NumberFormat = "0";
                                    r15.Value2 = a.Mau;
                                    r15.Columns.AutoFit();
                                    COMExcel.Range r16 = (COMExcel.Range)exSheet.Cells[i, 16];
                                    r16.NumberFormat = "0";
                                    r16.Value2 = a.TTPT;
                                    r16.Columns.AutoFit();
                                    COMExcel.Range r17 = (COMExcel.Range)exSheet.Cells[i, 17];
                                    r17.NumberFormat = "0";
                                    r17.Value2 = a.VTYT;
                                    r17.Columns.AutoFit();
                                    COMExcel.Range r18 = (COMExcel.Range)exSheet.Cells[i, 18];
                                    r18.NumberFormat = "0";
                                    r18.Value2 = 0;
                                    r18.Columns.AutoFit();
                                    COMExcel.Range r19 = (COMExcel.Range)exSheet.Cells[i, 19];
                                    r19.NumberFormat = "0";
                                    r19.Value2 = a.DVKTC;
                                    r19.Columns.AutoFit();
                                    COMExcel.Range r20 = (COMExcel.Range)exSheet.Cells[i, 20];
                                    r20.NumberFormat = "0";
                                    r20.Value2 = a.ThuocKTCG;
                                    r20.Columns.AutoFit();
                                    COMExcel.Range r21 = (COMExcel.Range)exSheet.Cells[i, 21];
                                    r21.NumberFormat = "0";
                                    r21.Value2 = a.Congkham;
                                    r21.Columns.AutoFit();
                                    COMExcel.Range r22 = (COMExcel.Range)exSheet.Cells[i, 22];
                                    r22.NumberFormat = "0";
                                    r22.Value2 = a.CPVanchuyen;
                                    r22.Columns.AutoFit();
                                    COMExcel.Range r23 = (COMExcel.Range)exSheet.Cells[i, 23];
                                    r23.NumberFormat = "0";
                                    r23.Value2 = a.Nguoibenhchitra;
                                    r23.Columns.AutoFit();
                                    COMExcel.Range r24 = (COMExcel.Range)exSheet.Cells[i, 24];
                                    r24.NumberFormat = "0";
                                    r24.Value2 = a.TongcongBHYT;
                                    r24.Columns.AutoFit();
                                    COMExcel.Range r25 = (COMExcel.Range)exSheet.Cells[i, 25];
                                    r25.NumberFormat = "0";
                                    r25.Value2 = a.CPVanchuyen;
                                    r25.Columns.AutoFit();
                                    COMExcel.Range r26 = (COMExcel.Range)exSheet.Cells[i, 26];
                                    r26.NumberFormat = "@";
                                    if (a.Tuyen != null && a.Tuyen == 1)
                                    {
                                        r26.Value2 = 1;
                                    }
                                    else
                                        r26.Value2 = 0;

                                    r26.Columns.AutoFit();
                                    COMExcel.Range r27 = (COMExcel.Range)exSheet.Cells[i, 27];
                                    r27.NumberFormat = "@";
                                    r27.Value2 = "";
                                    r27.Columns.AutoFit();
                                    COMExcel.Range r28 = (COMExcel.Range)exSheet.Cells[i, 28];
                                    r28.NumberFormat = "@";
                                    r28.Value2 = convertFont(DungChung.Bien.MaBV);
                                    r28.Columns.AutoFit();
                                    COMExcel.Range r29 = (COMExcel.Range)exSheet.Cells[i, 29];
                                    r29.NumberFormat = "@";
                                    var K = (from kh in _dataContext.KPhongs.Where(p => p.MaKP == a.Makp) select new { kh.TenKP }).ToList();
                                    if (K.Count > 0)
                                    {
                                        r29.Value2 = convertFont(K.First().TenKP);
                                    }
                                    r29.Columns.AutoFit();
                                    COMExcel.Range r30 = (COMExcel.Range)exSheet.Cells[i, 30];
                                    r30.NumberFormat = "0";
                                    if (a.Ngayra != null)
                                    {
                                        r30.Value2 = a.Ngayra.Value.Month;
                                        r30.Columns.AutoFit();
                                    }
                                    COMExcel.Range r31 = (COMExcel.Range)exSheet.Cells[i, 31];
                                    r31.NumberFormat = "0";
                                    if (a.Ngayra != null)
                                    {
                                        r31.Value2 = a.Ngayra.Value.Year;
                                        r31.Columns.AutoFit();
                                    }
                                    COMExcel.Range r32 = (COMExcel.Range)exSheet.Cells[i, 32];
                                    r32.NumberFormat = "@";
                                    if (a.BHtu != null)
                                    {
                                        r32.Value2 = convertFont(a.BHtu.ToString().Substring(0, 10));
                                        r32.Columns.AutoFit();
                                    }
                                    COMExcel.Range r33 = (COMExcel.Range)exSheet.Cells[i, 33];
                                    r33.NumberFormat = "@";
                                    if (a.BHden != null)
                                    {
                                        r33.Value2 = convertFont(a.BHden.ToString().Substring(0, 10));
                                        r33.Columns.AutoFit();
                                    }
                                    COMExcel.Range r34 = (COMExcel.Range)exSheet.Cells[i, 34];
                                    r34.NumberFormat = "@";
                                    r34.Value2 = convertFont(a.Diachi);
                                    r34.Columns.AutoFit();
                                    COMExcel.Range r35 = (COMExcel.Range)exSheet.Cells[i, 35];
                                    r35.Value2 = "";
                                    r35.Columns.AutoFit();
                                    COMExcel.Range r36 = (COMExcel.Range)exSheet.Cells[i, 36];
                                    r36.NumberFormat = "@";
                                    r36.Value2 = 0;
                                    r36.Columns.AutoFit();
                                    COMExcel.Range r37 = (COMExcel.Range)exSheet.Cells[i, 37];
                                    r37.Value2 = "";
                                    r37.Columns.AutoFit();
                                    COMExcel.Range r38 = (COMExcel.Range)exSheet.Cells[i, 38];
                                    r38.NumberFormat = "@";
                                    r38.Value2 = 0;
                                    r38.Columns.AutoFit();
                                    COMExcel.Range r39 = (COMExcel.Range)exSheet.Cells[i, 39];
                                    r39.NumberFormat = "0";
                                    r39.Value2 = 0;
                                    r39.Columns.AutoFit();
                                    COMExcel.Range r40 = (COMExcel.Range)exSheet.Cells[i, 40];
                                    r40.NumberFormat = "@";
                                    r40.Value2 = convertFont("NOI");
                                    r40.Columns.AutoFit();
                                    COMExcel.Range r41 = (COMExcel.Range)exSheet.Cells[i, 41];
                                    r41.NumberFormat = "@";
                                    r41.Value2 = convertFont("CSKCB");
                                    r41.Columns.AutoFit();
                                    COMExcel.Range r42 = (COMExcel.Range)exSheet.Cells[i, 42];
                                    r42.NumberFormat = "@";
                                    r42.Value2 = a.Mabn.Value;
                                    r42.Columns.AutoFit();
                                    COMExcel.Range r43 = (COMExcel.Range)exSheet.Cells[i, 43];
                                    r43.NumberFormat = "@";
                                    var kp = (from k1 in _dataContext.KPhongs.Where(p => p.MaKP == a.Makp) select new { k1.TenKP }).ToList();
                                    if (kp.Count > 0)
                                    { r43.Value2 = convertFont(kp.First().TenKP); }
                                    else
                                    { r43.Value2 = a.Makp; }
                                    r43.Columns.AutoFit();
                                    COMExcel.Range r44 = (COMExcel.Range)exSheet.Cells[i, 44];
                                    r43.NumberFormat = "@";
                                    r44.Value2 = "";
                                    r44.Columns.AutoFit();
                                }
                                exApp.Visible = true;//Ẩn hiện chương trình
                                exQLBV.SaveAs("C:\\Bieu80.xls", COMExcel.XlFileFormat.xlWorkbookNormal,
                                                null, null, false, false,
                                                COMExcel.XlSaveAsAccessMode.xlExclusive,
                                                false, false, false, false, false);
                                //exQLBV.Close(false, false, false);
                                //exApp.Quit(); // thoát ứng dụng
                                System.Runtime.InteropServices.Marshal.ReleaseComObject(exQLBV);
                                System.Runtime.InteropServices.Marshal.ReleaseComObject(exApp);
                            }
                            #endregion
                        }
                        else
                        {
                            int _MaKP =  lupKhoaphong.EditValue == null?0: Convert.ToInt32( lupKhoaphong.EditValue);
                            frmIn frm = new frmIn();
                            BaoCao.rep80aHD rep = new BaoCao.rep80aHD();
                            q = q.ToList().Where(p => p.Makp == _MaKP).OrderBy(p => p.Nhom).ThenBy(p => p.dt).ThenBy(p => p.Ngayra).ToList();
                            rep.DataSource = q;
                            rep.Ngaythang.Value = theoquy();
                            rep.MaCS.Value = DungChung.Bien.MaBV;
                            rep.TenCQ.Value = DungChung.Bien.TenCQ.ToUpper();
                            rep.paramKhoaPhong.Value = lupKhoaphong.Text.ToUpper();
                            double st = 0;
                            st = q.Where(p => p.Makp == _MaKP).Sum(p => p.TongcongBHYT);
                            rep.Sotien.Value = QLBV_Library.QLBV_Ham.DocTienBangChu(st, " đồng.");
                            rep.BindingData();
                            rep.CreateDocument();
                            //rep.DataMember = "Table";
                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                            frm.ShowDialog();
                            #region xuat Excel
                            if (Xuatex.Checked)
                            {
                                COMExcel.Application exApp = new COMExcel.Application();
                                COMExcel.Workbook exQLBV = exApp.Workbooks.Add(
                                          COMExcel.XlWBATemplate.xlWBATWorksheet);
                                COMExcel.Worksheet exSheet = (COMExcel.Worksheet)exQLBV.Worksheets[1];
                                //exSheet.Activate();
                                exSheet.Name = "NoiTru";// gán tên sheet
                                int i = 1;
                                string[] _arr = new string[44] { "stt", "hoten", "namsinh", "gioitinh", "mathe", "ma_dkbd", "mabenh", "ngay_vao", "ngay_ra", "ngaydtr", "t_tongchi", "t_xn", "t_cdha", "t_thuoc", "t_mau", "t_pttt", "t_vtytth", "t_vtyttt", "t_dvktc", "t_ktg", "t_kham", "t_vchuyen", "t_bnct", "t_bhtt", "t_ngoaids", "lydo_vv", "benhkhac", "noikcb", "khoa", "thang_qt", "nam_qt", "gt_tu", "gt_den", "diachi", "giamdinh", "t_xuattoan", "lydo_xt", "t_datuyen", "t_vuottran", "loaikcb", "noi_ttoan", "sophieu", "ma_khoa", "keysl" };
                                int k = 0;
                                foreach (var b in _arr)
                                {
                                    k++;
                                    COMExcel.Range r = (COMExcel.Range)exSheet.Cells[1, k];
                                    r.Value2 = b.ToString();
                                    r.Columns.AutoFit();
                                }
                                foreach (var a in q)
                                {
                                    i++;
                                    COMExcel.Range r1 = (COMExcel.Range)exSheet.Cells[i, 1];
                                    r1.NumberFormat = "0";
                                    r1.Value2 = i - 1;
                                    r1.Columns.AutoFit();
                                    COMExcel.Range r2 = (COMExcel.Range)exSheet.Cells[i, 2];
                                    r2.NumberFormat = "@";
                                    r2.Value2 = a.TenBNhan;
                                    r2.Columns.AutoFit();
                                    COMExcel.Range r3 = (COMExcel.Range)exSheet.Cells[i, 3];
                                    r3.NumberFormat = "0";
                                    r3.Value2 = a.NSinh;
                                    r3.Columns.AutoFit();
                                    COMExcel.Range r4 = (COMExcel.Range)exSheet.Cells[i, 4];
                                    r4.NumberFormat = "0";
                                    r4.Value2 = a.GTinh;
                                    r4.Columns.AutoFit();
                                    COMExcel.Range r5 = (COMExcel.Range)exSheet.Cells[i, 5];
                                    r5.NumberFormat = "@";
                                    r5.Value2 = a.SThe;
                                    r5.Columns.AutoFit();
                                    COMExcel.Range r6 = (COMExcel.Range)exSheet.Cells[i, 6];
                                    r6.NumberFormat = "@";
                                    r6.Value2 = a.MaCS;
                                    r6.Columns.AutoFit();
                                    COMExcel.Range r7 = (COMExcel.Range)exSheet.Cells[i, 7];
                                    r7.NumberFormat = "@";
                                    r7.Value2 = a.MaICD;
                                    r7.Columns.AutoFit();
                                    COMExcel.Range r8 = (COMExcel.Range)exSheet.Cells[i, 8];
                                    r8.NumberFormat = "@";
                                    if (a.Ngayvao != null)
                                        r8.Value2 = a.Ngayvao.ToString().Substring(0, 10);
                                    r8.Columns.AutoFit();
                                    COMExcel.Range r9 = (COMExcel.Range)exSheet.Cells[i, 9];
                                    r9.NumberFormat = "@";
                                    if (a.Ngayra != null)
                                        r9.Value2 = a.Ngayra.ToString().Substring(0, 10);
                                    r9.Columns.AutoFit();
                                    COMExcel.Range r10 = (COMExcel.Range)exSheet.Cells[i, 10];
                                    r10.NumberFormat = "0";
                                    r10.Value2 = a.Songay;
                                    r10.Columns.AutoFit();
                                    COMExcel.Range r11 = (COMExcel.Range)exSheet.Cells[i, 11];
                                    r11.NumberFormat = "0";
                                    r11.Value2 = a.Tongcong;
                                    r11.Columns.AutoFit();
                                    COMExcel.Range r12 = (COMExcel.Range)exSheet.Cells[i, 12];
                                    r12.NumberFormat = "0";
                                    r12.Value2 = a.Xetnghiem;
                                    r12.Columns.AutoFit();
                                    COMExcel.Range r13 = (COMExcel.Range)exSheet.Cells[i, 13];
                                    r13.NumberFormat = "0";
                                    r13.Value2 = a.CDHA;
                                    r13.Columns.AutoFit();
                                    COMExcel.Range r14 = (COMExcel.Range)exSheet.Cells[i, 14];
                                    r14.NumberFormat = "0";
                                    r14.Value2 = a.Thuoc;
                                    r14.Columns.AutoFit();
                                    COMExcel.Range r15 = (COMExcel.Range)exSheet.Cells[i, 15];
                                    r15.NumberFormat = "0";
                                    r15.Value2 = a.Mau;
                                    r15.Columns.AutoFit();
                                    COMExcel.Range r16 = (COMExcel.Range)exSheet.Cells[i, 16];
                                    r16.NumberFormat = "0";
                                    r16.Value2 = a.TTPT;
                                    r16.Columns.AutoFit();
                                    COMExcel.Range r17 = (COMExcel.Range)exSheet.Cells[i, 17];
                                    r17.NumberFormat = "0";
                                    r17.Value2 = a.VTYT;
                                    r17.Columns.AutoFit();
                                    COMExcel.Range r18 = (COMExcel.Range)exSheet.Cells[i, 18];
                                    r18.NumberFormat = "0";
                                    r18.Value2 = 0;
                                    r18.Columns.AutoFit();
                                    COMExcel.Range r19 = (COMExcel.Range)exSheet.Cells[i, 19];
                                    r19.NumberFormat = "0";
                                    r19.Value2 = a.DVKTC;
                                    r19.Columns.AutoFit();
                                    COMExcel.Range r20 = (COMExcel.Range)exSheet.Cells[i, 20];
                                    r20.NumberFormat = "0";
                                    r20.Value2 = a.ThuocKTCG;
                                    r20.Columns.AutoFit();
                                    COMExcel.Range r21 = (COMExcel.Range)exSheet.Cells[i, 21];
                                    r21.NumberFormat = "0";
                                    r21.Value2 = a.Congkham;
                                    r21.Columns.AutoFit();
                                    COMExcel.Range r22 = (COMExcel.Range)exSheet.Cells[i, 22];
                                    r22.NumberFormat = "0";
                                    r22.Value2 = a.CPVanchuyen;
                                    r22.Columns.AutoFit();
                                    COMExcel.Range r23 = (COMExcel.Range)exSheet.Cells[i, 23];
                                    r23.NumberFormat = "0";
                                    r23.Value2 = a.Nguoibenhchitra;
                                    r23.Columns.AutoFit();
                                    COMExcel.Range r24 = (COMExcel.Range)exSheet.Cells[i, 24];
                                    r24.NumberFormat = "0";
                                    r24.Value2 = a.TongcongBHYT;
                                    r24.Columns.AutoFit();
                                    COMExcel.Range r25 = (COMExcel.Range)exSheet.Cells[i, 25];
                                    r25.NumberFormat = "0";
                                    r25.Value2 = a.CPVanchuyen;
                                    r25.Columns.AutoFit();
                                    COMExcel.Range r26 = (COMExcel.Range)exSheet.Cells[i, 26];
                                    r26.NumberFormat = "@";
                                    if (a.Tuyen != null && a.Tuyen == 1)
                                    {
                                        r26.Value2 = 1;
                                    }
                                    else
                                        r26.Value2 = 0;
                                    r26.Columns.AutoFit();
                                    COMExcel.Range r27 = (COMExcel.Range)exSheet.Cells[i, 27];
                                    r27.NumberFormat = "@";
                                    r27.Value2 = "";
                                    r27.Columns.AutoFit();
                                    COMExcel.Range r28 = (COMExcel.Range)exSheet.Cells[i, 28];
                                    r28.NumberFormat = "@";
                                    r28.Value2 = DungChung.Bien.MaBV;
                                    r28.Columns.AutoFit();
                                    COMExcel.Range r29 = (COMExcel.Range)exSheet.Cells[i, 29];
                                    r29.NumberFormat = "@";
                                    var K = (from kh in _dataContext.KPhongs.Where(p => p.MaKP == a.Makp) select new { kh.TenKP }).ToList();
                                    if (K.Count > 0)
                                    {
                                        r29.Value2 = K.First().TenKP;
                                    }
                                    //r29.Value2 = a.Makp;
                                    r29.Columns.AutoFit();
                                    COMExcel.Range r30 = (COMExcel.Range)exSheet.Cells[i, 30];
                                    r30.NumberFormat = "0";
                                    if (a.Ngayra != null)
                                    {
                                        r30.Value2 = a.Ngayra.Value.Month;
                                        r30.Columns.AutoFit();
                                    }
                                    COMExcel.Range r31 = (COMExcel.Range)exSheet.Cells[i, 31];
                                    r31.NumberFormat = "0";
                                    if (a.Ngayra != null)
                                    {
                                        r31.Value2 = a.Ngayra.Value.Year;
                                        r31.Columns.AutoFit();
                                    }
                                    COMExcel.Range r32 = (COMExcel.Range)exSheet.Cells[i, 32];
                                    r32.NumberFormat = "@";
                                    if (a.BHtu != null)
                                    {
                                        r32.Value2 = a.BHtu.ToString().Substring(0, 10);
                                        r32.Columns.AutoFit();
                                    }
                                    COMExcel.Range r33 = (COMExcel.Range)exSheet.Cells[i, 33];
                                    r33.NumberFormat = "@";
                                    if (a.BHden != null)
                                    {
                                        r33.Value2 = a.BHden.ToString().Substring(0, 10);
                                        r33.Columns.AutoFit();
                                    }
                                    COMExcel.Range r34 = (COMExcel.Range)exSheet.Cells[i, 34];
                                    r34.NumberFormat = "@";
                                    r34.Value2 = a.Diachi;
                                    r34.Columns.AutoFit();
                                    COMExcel.Range r35 = (COMExcel.Range)exSheet.Cells[i, 35];
                                    r35.Value2 = "";
                                    r35.Columns.AutoFit();
                                    COMExcel.Range r36 = (COMExcel.Range)exSheet.Cells[i, 36];
                                    r36.NumberFormat = "@";
                                    r36.Value2 = 0;
                                    r36.Columns.AutoFit();
                                    COMExcel.Range r37 = (COMExcel.Range)exSheet.Cells[i, 37];
                                    r37.Value2 = "";
                                    r37.Columns.AutoFit();
                                    COMExcel.Range r38 = (COMExcel.Range)exSheet.Cells[i, 38];
                                    r38.NumberFormat = "@";
                                    r38.Value2 = 0;
                                    r38.Columns.AutoFit();
                                    COMExcel.Range r39 = (COMExcel.Range)exSheet.Cells[i, 39];
                                    r39.NumberFormat = "0";
                                    r39.Value2 = 0;
                                    r39.Columns.AutoFit();
                                    COMExcel.Range r40 = (COMExcel.Range)exSheet.Cells[i, 40];
                                    r40.NumberFormat = "@";
                                    r40.Value2 = "NOI";
                                    r40.Columns.AutoFit();
                                    COMExcel.Range r41 = (COMExcel.Range)exSheet.Cells[i, 41];
                                    r41.NumberFormat = "@";
                                    r41.Value2 = "CSKCB";
                                    r41.Columns.AutoFit();
                                    COMExcel.Range r42 = (COMExcel.Range)exSheet.Cells[i, 42];
                                    r42.NumberFormat = "@";
                                    r42.Value2 = a.Mabn;
                                    r42.Columns.AutoFit();
                                    COMExcel.Range r43 = (COMExcel.Range)exSheet.Cells[i, 43];
                                    r43.NumberFormat = "@";
                                    var kp = (from k1 in _dataContext.KPhongs.Where(p => p.MaKP == a.Makp) select new { k1.TenKP }).ToList();
                                    if (kp.Count > 0)
                                    { r43.Value2 = kp.First().TenKP; }
                                    else
                                    { r43.Value2 = a.Makp; }
                                    r43.Columns.AutoFit();
                                    COMExcel.Range r44 = (COMExcel.Range)exSheet.Cells[i, 44];
                                    r44.Value2 = "";
                                    r44.Columns.AutoFit();
                                }
                                exApp.Visible = true;//Ẩn hiện chương trình
                                exQLBV.SaveAs("C:\\Bieu79.xls", COMExcel.XlFileFormat.xlWorkbookNormal,
                                                null, null, false, false,
                                                COMExcel.XlSaveAsAccessMode.xlExclusive,
                                                false, false, false, false, false);
                                //exQLBV.Close(false, false, false);
                                //exApp.Quit(); // thoát ứng dụng
                                System.Runtime.InteropServices.Marshal.ReleaseComObject(exQLBV);
                                System.Runtime.InteropServices.Marshal.ReleaseComObject(exApp);
                            }
                            #endregion
                        }
                    }
                }

            }
        }

        private void lupKhoaphong_EditValueChanged(object sender, EventArgs e)
        {
            
        }

        private void frm_80ct_Load(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities _dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var q = from kp in _dataContext.KPhongs.Where(p => p.PLoai== ("Lâm sàng"))
                    select new { kp.TenKP, kp.MaKP };
            lupKhoaphong.Properties.DataSource = q.ToList();
            DateTime ngaytu = System.DateTime.Now.Date;
            DateTime ngayden = System.DateTime.Now.Date;
            lupNgaytu.EditValue = ngaytu;
            lupngayden.EditValue = ngayden;
            lupNgaytu.Focus();

        }
        private bool kt()
        {
            if (lupNgaytu.Text == "" || lupngayden.Text == "")
            {
                MessageBox.Show("Bạn hãy chọn ngày tháng");
                lupNgaytu.Focus();
                return false;
            }
            return true;
        }
        private string theoquy()
        {
            string quy = "";

            if (ckBC.Checked == true)
            {
                switch (timquy(lupNgaytu.DateTime.Month))
                {
                    case 1:
                        quy = "Quý I";
                        break;
                    case 2:
                        quy = "Quý II";
                        break;
                    case 3:
                        quy = "Quý III";
                        break;
                    case 4:
                        quy = "Quý IV";
                        break;
                }

            }
            else
            {
                quy = "Từ ngày " + lupNgaytu.DateTime.ToString().Substring(0, 10) + "  đến ngày " + lupngayden.DateTime.ToString().Substring(0, 10);
            }
            return quy;
        }
        private int timquy(int month)
        {
            if (month >= 1 && month <= 3)
            {
                return (1);
            }
            if (month > 3 && month <= 6)
            {
                return (2);
            }
            if (month > 6 && month <= 9)
            {
                return (3);
            }
            else { return (4); }
        }
        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Xuatex_Properties_CheckedChanged(object sender, EventArgs e)
        {
            if (Xuatex.Checked)
            {
                rdFont.Enabled = true;
            }
            else
                rdFont.Enabled = false;
        }

        private void rdFont_Properties_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdFont.SelectedIndex == 0)
            {
                Font = 0;
            }
            else Font = 1;
        }
    }
}
