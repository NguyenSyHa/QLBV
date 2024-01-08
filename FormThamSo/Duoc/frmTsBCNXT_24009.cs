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
    public partial class frmTsBCNXT_24009 : DevExpress.XtraEditors.XtraForm
    {
        public frmTsBCNXT_24009()
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
            DateTime tungay = System.DateTime.Now.Date;
            DateTime denngay = System.DateTime.Now.Date;          
          //  string _tenkho = "";
            List<KPhong> _kpChon = new List<KPhong>();
            if (KTtaoBcNXT())
            {
                for (int i = 0; i < cklKP.ItemCount; i++)
                {
                    if (cklKP.GetItemChecked(i))
                    {
                        _kpChon.Add(new KPhong { TenKP = cklKP.GetItemText(i), MaKP = cklKP.GetItemValue(i) == null ? 0 : Convert.ToInt32(cklKP.GetItemValue(i)) });

                    }
                }

                string _nhacc = "";
                if (lupNhaCC.EditValue != null)
                    _nhacc = lupNhaCC.EditValue.ToString();
                tungay = DungChung.Ham.NgayTu(dateTuNgay.DateTime);//dateTuNgay.DateTime;
                denngay = DungChung.Ham.NgayDen(dateDenNgay.DateTime);//dateDenNgay.DateTime;
                int mauin = radMauIn.SelectedIndex + 1;
                QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);               
                //foreach (var item in _kpChon)
                //{
                //    _tenkho += item.TenKP + "; ";
                //}
                List<int> _ListIdTN = new List<int>();
                for (int i = 0; i < cklTieuNhom.ItemCount; i++)
                {
                    if (cklTieuNhom.GetItemCheckState(i) == CheckState.Checked)
                        _ListIdTN.Add(Convert.ToInt32(cklTieuNhom.GetItemValue(i)));
                }


                if (_kpChon.Count > 0)
                {
                    var qnxt2 = (from nhapd in data.NhapDs
                                 join nhapdct in data.NhapDcts on nhapd.IDNhap equals nhapdct.IDNhap
                                 where ((nhapd.PLoai == 1) || (nhapd.PLoai == 2))
                                 select new { nhapd.XuatTD, nhapd.MaKP, nhapdct.MaDV, nhapd.PLoai, nhapd.KieuDon, nhapd.NgayNhap, nhapdct.DonGia, nhapdct.SoLuongN, nhapdct.SoLuongX, nhapdct.ThanhTienN, nhapdct.ThanhTienX }).ToList();

                    var dichvu = (from dv in data.DichVus
                                  join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                                  select new
                                  {
                                      dv.MaDV,
                                      dv.TenDV,
                                      dv.TenHC,
                                      dv.QCPC,
                                      dv.DuongD,
                                      tn.TenTN,
                                      dv.SoTTqd,
                                      dv.NguonGoc,
                                      tn.STT,
                                      dv.MaCC,
                                      dv.DonVi,
                                      dv.NhaSX,
                                      tn.IdTieuNhom
                                  }).ToList();
                    var qcc = data.NhaCCs.ToList();
                    var qdv = (from dv in dichvu
                               join ncc in qcc on dv.MaCC equals ncc.MaCC into kq  from kq1 in kq.DefaultIfEmpty()
                               join tn in _ListIdTN on dv.IdTieuNhom equals tn
                               select new
                               {
                                   dv.MaDV,
                                   dv.TenDV,
                                   dv.TenHC,
                                   dv.QCPC,
                                   dv.DuongD,
                                   dv.TenTN,
                                   dv.IdTieuNhom,
                                   dv.NguonGoc,
                                   dv.SoTTqd,
                                   dv.STT,
                                   dv.MaCC,
                                   dv.DonVi,
                                   dv.NhaSX,
                                   TenCC = kq1 == null ? "" :  (kq1.TenCC == null ? "" : kq1.TenCC)
                               }).ToList();
                    // bổ sung hình thức mua
                    var qbs = (from dv in qdv
                               join bs in data.DichVuExes on dv.MaDV equals bs.MaDV into kq
                               from kq1 in kq.DefaultIfEmpty()
                               select new
                               {
                                   dv.MaDV,
                                   dv.TenDV,
                                   dv.TenHC,
                                   dv.QCPC,
                                   dv.DuongD,
                                   dv.TenTN,
                                   dv.IdTieuNhom,
                                   dv.NguonGoc,
                                   dv.SoTTqd,
                                   dv.STT,
                                   dv.MaCC,
                                   dv.DonVi,
                                   dv.NhaSX,
                                   dv.TenCC ,
                                   DauThauSYT = kq1 ==null? "" : (kq1.HinhThucMua == 1? "x" : ""),
                                   DauThauDonVi = kq1 == null ? "" : (kq1.HinhThucMua == 2 ? "x" : ""),
                                   MuaKhac = kq1 == null ? "" : (kq1.HinhThucMua == 3 ? "x" : ""),
                               }).ToList();

                    var qnxt = (from a in qnxt2
                                join dv in qbs on a.MaDV equals dv.MaDV
                                join kp in _kpChon on a.MaKP equals kp.MaKP
                                group a by new
                                {
                                    dv.SoTTqd,
                                    dv.STT,
                                    dv.MaCC,
                                    dv.TenCC,
                                    dv.TenHC,
                                    dv.QCPC,
                                    dv.DuongD,
                                    dv.TenTN,
                                    dv.IdTieuNhom,
                                    dv.TenDV,
                                    dv.DonVi,
                                    dv.NhaSX,
                                    dv.NguonGoc,
                                    dv.DauThauSYT,
                                    dv.DauThauDonVi,
                                    dv.MuaKhac,
                                    a.DonGia,
                                    a.MaDV,

                                } into kq
                                select new
                                        {
                                            kq.Key.STT,
                                            kq.Key.SoTTqd,// số thứu tự thông tư
                                            kq.Key.TenTN,
                                            kq.Key.IdTieuNhom,
                                            kq.Key.MaCC,
                                            MaDV = kq.Key.MaDV,
                                            TenThuoc = kq.Key.TenHC,// tên thuốc, thành phần thuốc
                                            TenThuongMai = kq.Key.TenDV,// Tên thương mại của dịch vụ
                                            DDung = (kq.Key.DuongD == null || kq.Key.DuongD == "") ? kq.Key.QCPC : kq.Key.DuongD, // dạng bào chế, hoặc đường dùng
                                            kq.Key.NhaSX, //nhà sản xuất
                                            NCC = kq.Key.TenCC,// tên nhà cung cấp
                                            DonVi = kq.Key.DonVi, // đơn vị tính
                                            DonGia = kq.Key.DonGia,
                                            KQdauthau = kq.Key.DauThauSYT,// đấu thầu theo kq của SYT
                                            Dauthau_dvitc = kq.Key.DauThauDonVi, // đấu thầu rộng rãi do đơn vị tự tổ chức
                                            hthuckhac = kq.Key.MuaKhac,// hình thức muakhacs
                                            cot2 = mauin == 3? kq.Key.TenHC: (mauin == 4? kq.Key.SoTTqd : kq.Key.TenDV),
                                           // cot3 =kq.Key.TenDV,
                                            cot4 = mauin== 4? kq.Key.TenHC : kq.Key.QCPC,
                                            cot5 = mauin == 4? kq.Key.NguonGoc : kq.Key.NhaSX, // nguồn gốc                                            
                                            TonDKSL = kq.Where(p => p.NgayNhap < tungay).Sum(p => p.SoLuongN) - kq.Where(p => p.NgayNhap < tungay).Sum(p => p.SoLuongX),
                                            TonDKTT = kq.Where(p => p.NgayNhap < tungay).Sum(p => p.ThanhTienN) - kq.Where(p => p.NgayNhap < tungay).Sum(p => p.ThanhTienX),

                                            //nhập trong kỳ
                                            NhapTKSL = kq.Where(p => p.PLoai == 1 && p.KieuDon == -1).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.SoLuongN), // phân loại = 1; nhập dược; kiểu đơn = 1: nhập theo hóa đơn
                                            NhapTKTT = kq.Where(p => p.PLoai == 1 && p.KieuDon == -1).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.ThanhTienN),

                                            XuatNoiTruSL = kq.Where(p => p.KieuDon == 1 && p.PLoai == 2).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.SoLuongX),  //Phân loại = 2 là xuất dược
                                            xuatNoiTruTT = kq.Where(p => p.KieuDon == 1 && p.PLoai == 2).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.ThanhTienX),// nội trú <==>  lâm sàng

                                            XuatNgoaiTruSL = kq.Where(p => (mauin == 6 ? p.KieuDon == 5 : p.KieuDon == 0) && p.PLoai == 2).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.SoLuongX),// mẫu 6 là CLS ( Kiểu đơn = 5)
                                            xuatNgoaiTruTT = kq.Where(p =>(mauin == 6 ? p.KieuDon == 5 : p.KieuDon == 0) && p.PLoai == 2).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.ThanhTienX),

                                            XuatXaSL = kq.Where(p => p.KieuDon == 3 && p.PLoai == 2).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.SoLuongX),  //Phân loại = 3 là xuất xã -- sai
                                            XuatXaTT = kq.Where(p => p.KieuDon == 3 && p.PLoai == 2).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.ThanhTienX),  

                                            TonCKSL = kq. Where(p => p.NgayNhap < denngay).Sum(p => p.SoLuongN) - kq.Where(p => p.NgayNhap < denngay).Sum(p => p.SoLuongX),
                                            TonCKTT = kq.Where(p => p.NgayNhap < denngay).Sum(p => p.ThanhTienN) - kq.Where(p => p.NgayNhap < denngay).Sum(p => p.ThanhTienX),

                                            dukienSL = "",
                                        }).Where(p => p.TonDKSL != 0 || p.TonCKSL != 0 || p.NhapTKSL != 0 || p.XuatNoiTruSL != 0 || p.XuatNgoaiTruSL != 0).ToList();


                    //
                    //Tìm theo điều kiện NCC                   
                    var qnxt3 = qnxt.Where(p => (string.IsNullOrEmpty(_nhacc) || _nhacc == "") ? true : p.MaCC == _nhacc).OrderBy(p => p.TenTN).ThenBy(p => p.TenThuongMai).ToList();

                    //
                    //Lấy ra danh sách tiểu nhóm dv của những dịch vụ đã select được trên qnxt3                     
                    var qtn = (from a in qnxt3 group a by new { a.IdTieuNhom, a.TenTN, a.STT } into kq select new { kq.Key.IdTieuNhom, kq.Key.TenTN, kq.Key.STT }).OrderBy(p => p.TenTN).ToList();

                    //
                    //Insert STT của nhóm từ 1 đến hết                   
                    int sttTN = 1;  // số thứ tự tiểu nhóm tự tăng sau khi sắp xếp tiểu nhóm theo tên
                    var qtn1 = (from a in qtn select new { a.STT, a.IdTieuNhom, a.TenTN, STTTN = sttTN++ }).ToList();//STTTN là số thứ tự tiểu nhóm tự tăng sau khi sắp xếp tiểu nhóm theo tên    

                    //
                    //Lấy ra STT la mã của nhóm dịch vụ                    
                    List<STTLaMa> qLaMa = getListSTTLaMa();
                    var qtn2 = (from a in qtn1 join b in qLaMa on a.STTTN equals b.Stt select new { a.TenTN, a.IdTieuNhom, a.STT, a.STTTN, b.SttLaMa }).ToList();

                    //
                    //Lấy ra dữ liệu đổ vào báo cáo                    
                    var q = (from a in qnxt3
                             join b in qtn2 on a.IdTieuNhom equals b.IdTieuNhom
                             select new
                             {
                                 a.SoTTqd,
                                 a.STT,
                                 a.TenTN,
                                 a.IdTieuNhom,
                                 a.MaCC,
                                 a.MaDV,
                                 a.TenThuoc,
                                 a.TenThuongMai,
                                 a.DDung,
                                 a.NhaSX,
                                 a.NCC,
                                 a.DonVi,
                                 a.DonGia,
                                 a.KQdauthau,
                                 a.Dauthau_dvitc,
                                 a.hthuckhac,
                                 a.TonDKSL,
                                 a.TonDKTT,
                                 a.NhapTKSL,
                                 a.NhapTKTT,
                                 a.XuatNoiTruSL,
                                 a.xuatNoiTruTT,
                                 a.XuatNgoaiTruSL,
                                 a.xuatNgoaiTruTT,
                                 a.XuatXaSL,
                                 a.XuatXaTT,
                                 a.TonCKSL,
                                 a.TonCKTT,
                                 a.dukienSL,
                                 a.cot2,
                                 a.cot4,
                                 a.cot5,
                                 b.STTTN,
                                 b.SttLaMa
                             }).OrderBy(p => p.STTTN).ThenBy(p => p.TenThuongMai).ToList();

                    if (_kpChon.Count > 0)
                    {
                        string[] _arr = new string[] { "0", "@", "@", "@", "@", "@", "@", "@", "0", "@", "@", "@", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" };
                        int[] _arrWidth = new int[] { };
                        int num = 1;
                        DungChung.Bien.MangHaiChieu = new Object[q.Count + 2, 23];
                        string[] _tieude1, _tieude2;
                        _tieude1 = new string[] { "STT", "STT TT", "Tên thương mại", "Dạng bào chế, đường dùng", "Xuất xứ (Hãng SX, nước SX)", "Doanh nghiệp cung ứng", "Đơn vị tính", "Đơn giá (có VAT)", "Hình thức mua", "", "", "Tồn đầu kỳ", "", "Mua trong kỳ", "", "Sử dụng nội trú", "", "Sử dụng ngoại trú", "", "Tồn cuối kỳ", "", "Số lượng dự kiến kỳ sau" };
                        _tieude2 = new string[] { "", "", "", "", "", "", "", "", "", "Theo kết quả đấu thầu của Sở Y Tế", "Đấu thầu rộng rãi do đơn vị tự tổ chức", "Theo các hình thức khác", "Số lượng", "Thành tiền", "Số lượng", "Thành tiền", "Số lượng", "Thành tiền", "Số lượng", "Thành tiền", "Số lượng", "Thành tiền", "" };
                        for (int i = 0; i < _tieude1.Length; i++)
                        {
                            DungChung.Bien.MangHaiChieu[0, i] = _tieude1[i];
                        }
                        for (int i = 0; i < _tieude2.Length; i++)
                        {
                            DungChung.Bien.MangHaiChieu[0, 1] = _tieude2[i];
                        }
                        foreach (var r in q)
                        {

                            DungChung.Bien.MangHaiChieu[num, 0] = num;
                            DungChung.Bien.MangHaiChieu[num, 1] = r.STT;
                            DungChung.Bien.MangHaiChieu[num, 2] = r.TenThuoc;
                            DungChung.Bien.MangHaiChieu[num, 3] = r.TenThuongMai;
                            DungChung.Bien.MangHaiChieu[num, 4] = r.DDung;
                            DungChung.Bien.MangHaiChieu[num, 5] = r.NhaSX;
                            DungChung.Bien.MangHaiChieu[num, 6] = r.NCC;
                            DungChung.Bien.MangHaiChieu[num, 7] = r.DonVi;
                            DungChung.Bien.MangHaiChieu[num, 8] = r.DonGia;
                            DungChung.Bien.MangHaiChieu[num, 9] = r.KQdauthau;// hình thức mua theo kết quả đấu thầu của sở y tế
                            DungChung.Bien.MangHaiChieu[num, 10] = r.Dauthau_dvitc;// hình thức mua là đấu thầu rộng rãi do đơn vị tự tổ chức
                            DungChung.Bien.MangHaiChieu[num, 11] = r.hthuckhac;// Các hình thức mua khác
                            DungChung.Bien.MangHaiChieu[num, 12] = r.TonDKSL;
                            DungChung.Bien.MangHaiChieu[num, 13] = r.TonDKTT;
                            DungChung.Bien.MangHaiChieu[num, 14] = r.NhapTKSL;
                            DungChung.Bien.MangHaiChieu[num, 15] = r.NhapTKTT;
                            DungChung.Bien.MangHaiChieu[num, 16] = r.XuatNoiTruSL;
                            DungChung.Bien.MangHaiChieu[num, 17] = r.xuatNoiTruTT;
                            DungChung.Bien.MangHaiChieu[num, 18] = r.XuatNgoaiTruSL;
                            DungChung.Bien.MangHaiChieu[num, 19] = r.xuatNgoaiTruTT;
                            DungChung.Bien.MangHaiChieu[num, 20] = r.TonCKSL;
                            DungChung.Bien.MangHaiChieu[num, 21] = r.TonCKTT;
                            DungChung.Bien.MangHaiChieu[num, 22] = r.dukienSL;// số lượng dự kiến kỳ sau
                            num++;
                        }
                       

                        //
                        // Mẫu 23 cột
                        //
                        if (mauin == 1 || mauin == 2)
                        {
                            BaoCao.rep_6NXT_23_24009 rep = new BaoCao.rep_6NXT_23_24009();
                            frmIn frm2 = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "BCNXT", "C:\\TsBCNXT.xls", true, this.Name);
                            rep.lab_thoigian.Text = "Từ ngày " + dateTuNgay.Text + " đến này " + dateDenNgay.Text;
                            if (denngay.Month == 1 || denngay.Month == 2 || denngay.Month == 3)
                                rep.celQuyNam.Text = "QÚY I NĂM " + denngay.Year;
                            else if (denngay.Month == 4 || denngay.Month == 5 || denngay.Month == 6)
                                rep.celQuyNam.Text = "QÚY II NĂM " + denngay.Year;
                            else if (denngay.Month == 7 || denngay.Month == 8 || denngay.Month == 9)
                                rep.celQuyNam.Text = "QÚY III NĂM " + denngay.Year;
                            else if (denngay.Month == 10 || denngay.Month == 11 || denngay.Month == 12)
                                rep.celQuyNam.Text = "QÚY IV NĂM " + denngay.Year;
                            if (DungChung.Bien.MaBV == "24009")
                            {
                                rep.lab_kinhgui.Text = "Kính gửi: Phòng Nghiệp vụ Dược - Sở Y tế Bắc Giang";
                                rep.cel_diadanh.Text = "Bắc Giang, ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;
                            }
                            else
                            {
                                rep.lab_kinhgui.Text = "";
                                rep.cel_diadanh.Text = "ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;
                            }
                            
                           
                            #region mẫu 1
                            if (mauin == 1)
                            {
                                rep.lab_Tieude.Text = "BÁO CÁO NHẬP - XUẤT - TỒN THUỐC CHẾ PHẨM Y HỌC CỔ TUYỀN";
                                rep.lab_Mauso.Text = "Mẫu số 3";
                               
                            }
                            #endregion mẫu 1
                            else
                            {
                                #region mẫu 2
                                rep.lab_Tieude.Text = "BÁO CÁO NHẬP - XUẤT - TỒN THUỐC TÂN DƯỢC";
                                rep.lab_Mauso.Text = "Mẫu số 2";
                                //
                                // tiêu đề cột
                                //
                                rep.celTit12.Text = "STT thuốc TT40";
                                rep.celTit13.Text = "Tên hoạt chất";
                                rep.celTit14.Text = "Tên thương mại";
                                rep.celTit15.Text = "Đường dùng, dạng dùng, hàm lượng, dạng BC";
                                rep.celTit16.Text = "Xuất xứ (Hãng SX, nước SX)";
                                rep.celTit17.Text = "Doanh nghiệp cung ứng";

                                #endregion mẫu 2
                            }

                            rep.DataSource = q;
                            rep.BindingData();
                            rep.CreateDocument();
                            frm2.prcIN.PrintingSystem = rep.PrintingSystem;
                            frm2.ShowDialog();
                        }

                        //
                        // Mẫu 22 cột
                        //
                        else
                        {
                            BaoCao.rep_6NXT_22_24009 rep = new BaoCao.rep_6NXT_22_24009();
                            frmIn frm2 = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "BCNXT", "C:\\TsBCNXT.xls", true, this.Name);
                            rep.lab_thoigian.Text = "Từ ngày " + dateTuNgay.Text + " đến này " + dateDenNgay.Text;
                            if (denngay.Month == 1 || denngay.Month == 2 || denngay.Month == 3)
                                rep.celQuyNam.Text = "QÚY I NĂM " + denngay.Year;
                            else if (denngay.Month == 4 || denngay.Month == 5 || denngay.Month == 6)
                                rep.celQuyNam.Text = "QÚY II NĂM " + denngay.Year;
                            else if (denngay.Month == 7 || denngay.Month == 8 || denngay.Month == 9)
                                rep.celQuyNam.Text = "QÚY III NĂM " + denngay.Year;
                            else if (denngay.Month == 10 || denngay.Month == 11 || denngay.Month == 12)
                                rep.celQuyNam.Text = "QÚY IV NĂM " + denngay.Year;
                            if (DungChung.Bien.MaBV == "24009")
                            {
                                rep.lab_kinhgui.Text = "Kính gửi: Phòng Nghiệp vụ Dược - Sở Y tế Bắc Giang";
                                rep.cel_diadanh.Text = "Bắc Giang, ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;
                            }
                            else
                            {
                                rep.lab_kinhgui.Text = "";
                                rep.cel_diadanh.Text = "ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;
                            }

                            if(mauin == 3)
                            {
                                #region mẫu 3
                                rep.lab_Tieude.Text = "BÁO CÁO NHẬP - XUẤT - TỒN SINH PHẨM Y TẾ";
                                rep.lab_Mauso.Text = "Mẫu số 3";
                                //
                                // tiêu đề cột
                                //
                                rep.celTit2.Text = "Tên sinh phẩm y tế";
                                rep.celTit3.Text = "Tên thương mại (nếu có)";
                                rep.celTit4.Text = "Quy cách đóng gói";
                                rep.celTit5.Text = "Xuất xứ (Hãng SX, nước SX)";
                                rep.celTit6.Text = "Doanh nghiệp cung ứng";
                                #endregion mẫu 3
                            }
                            else if (mauin == 4)
                            {
                                #region mẫu 4
                                rep.lab_Tieude.Text = "BÁO CÁO NHẬP - XUẤT - TỒN VỊ THUỐC YHCT";
                                rep.lab_Mauso.Text = "Mẫu số 4";
                                //
                                // tiêu đề cột
                                //
                                rep.celTit2.Text = "STT TT05";
                                rep.celTit3.Text = "Tên vị thuốc";
                                rep.celTit4.Text = "Tên khoa học";
                                rep.celTit5.Text = "Nguồn gốc";
                                rep.celTit6.Text = "Doanh nghiệp cung ứng";
                                #endregion mẫu 4
                            }
                            else if (mauin == 5)
                            {
                                #region mẫu 5
                                rep.lab_Tieude.Text = "BÁO CÁO NHẬP - XUẤT - TỒN VẬT TƯ Y TẾ";
                                rep.lab_Mauso.Text = "Mẫu số 6";
                                //
                                // tiêu đề cột
                                //
                                rep.celTit2.Text = "Tên vật tư";
                                rep.celTit3.Text = "Tên thương mại (nếu có)";
                                rep.celTit4.Text = "Tiêu chuẩn kỹ thuật";
                                rep.celTit5.Text = "Xuất xứ (Hãng SX, nước SX)";
                                rep.celTit6.Text = "Doanh nghiệp cung ứng";
                                #endregion mẫu 5
                            }
                            else if (mauin == 6)
                            {
                                #region mẫu 6
                                rep.lab_Tieude.Text = "BÁO CÁO NHẬP - XUẤT - TỒN HÓA CHẤT";
                                rep.lab_Mauso.Text = "Mẫu số 7";
                                //
                                // tiêu đề cột
                                //
                                rep.celTit2.Text = "Tên hóa chất";
                                rep.celTit3.Text = "Tên thương mại (nếu có)";
                                rep.celTit4.Text = "Tiêu chuẩn kỹ thuật";
                                rep.celTit5.Text = "Xuất xứ (Hãng SX, nước SX)";
                                rep.celTit6.Text = "Doanh nghiệp cung ứng";
                                rep.lblSD_1.Text = "Sử dụng trong lâm sàng";
                                rep.lblSD_2.Text = "Sử dụng trong cận lâm sàng";
                                #endregion mẫu 6
                            }
                            rep.DataSource = q;
                            rep.BindingData();
                            rep.CreateDocument();
                            frm2.prcIN.PrintingSystem = rep.PrintingSystem;
                            frm2.ShowDialog();
                        }
                    }
                    else
                    { MessageBox.Show("Bạn chưa chọn kho"); }
                }
               
            }
        }
        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmTsBCNXT_Load(object sender, EventArgs e)
        {
            try
            {
                QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                List<NhaCC> qcc = data.NhaCCs.ToList();
                qcc.Insert(0, new NhaCC { MaCC = "", TenCC = "Tất cả" });
                lupNhaCC.Properties.DataSource = qcc.ToList();
                dateDenNgay.DateTime = System.DateTime.Now;
                dateTuNgay.DateTime = System.DateTime.Now;
                var qtn = (from nhom in data.NhomDVs.Where(p => p.Status == 1)
                           join tn in data.TieuNhomDVs on nhom.IDNhom equals tn.IDNhom
                           select new { tn.TenTN, tn.IdTieuNhom, tn.TenRG }).OrderBy(p => p.TenTN).ToList();
                List<TieuNhomDV> _ltn = new List<TieuNhomDV>();
                foreach (var a in qtn)
                {
                    TieuNhomDV moi = new TieuNhomDV();
                    moi.TenTN = a.TenTN;
                    moi.TenRG = a.TenRG;
                    moi.IdTieuNhom = a.IdTieuNhom;
                    _ltn.Add(moi);
                }
                _ltn.Insert(0, new TieuNhomDV { IdTieuNhom = 0, TenRG = "Tất cả", TenTN = "Tất cả" });
                cklTieuNhom.DataSource = _ltn;
                for (int i = 0; i < cklTieuNhom.ItemCount; i++)
                {
                    cklTieuNhom.SetItemChecked(i, true);
                }

                List<KPhong> dskp = data.KPhongs.Where(p => p.PLoai == "Khoa dược").OrderBy(p => p.TenKP).ToList();
                dskp.Insert(0, new KPhong { MaKP = 0, TenKP = "Tất cả" });
                //var dskp = (from kp in data.KPhongs.Where(p => p.PLoai == "Khoa dược")
                //            select new
                //            {
                //                MaKP = kp.MaKP,
                //                TenKP = kp.TenKP
                //            }).OrderBy(p => p.TenKP).ToList();
                cklKP.DataSource = dskp;
                for (int i = 0; i < cklKP.ItemCount; i++)
                {
                    cklKP.SetItemChecked(i, true);
                }
            }
            catch (Exception)
            {
            }
        }

        private void cklKP_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            //int check = 0;
            //for (int i = 0; i < cklKP.ItemCount; i++)
            //{
            //    if (cklKP.GetItemChecked(i))
            //        check++;
            //}
            //if (check > 1)
            //{
            //    radMauIn.Properties.ReadOnly = true;
            //    radMauIn_hg.SelectedIndex = -1;
            //}
            //else
            //{
            //    radMauIn_hg.Properties.ReadOnly = false;
            //    radMauIn_hg.SelectedIndex = 0;
            //}
            if (e.Index == 0)
            {
                if (e.State == CheckState.Checked)
                {
                    for (int i = 1; i < cklKP.ItemCount; i++)
                    {
                        cklKP.SetItemChecked(i, true);
                    }
                }
                else
                {
                    for (int i = 1; i < cklKP.ItemCount; i++)
                    {
                        cklKP.SetItemChecked(i, false);
                    }
                }
            }
        }



        private void cklTieuNhom_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            if (e.Index == 0)
            {
                if (e.State == CheckState.Checked)
                {
                    for (int i = 1; i < cklTieuNhom.ItemCount; i++)
                    {
                        cklTieuNhom.SetItemChecked(i, true);
                    }
                }
                else
                {
                    for (int i = 1; i < cklTieuNhom.ItemCount; i++)
                    {
                        cklTieuNhom.SetItemChecked(i, false);
                    }
                }
            }
        }


        private List<STTLaMa> getListSTTLaMa()
        {
            List<STTLaMa> _listSTTLaMa = new List<STTLaMa> { };
            _listSTTLaMa.Add(new STTLaMa { Stt = 1, SttLaMa = "I" });
            _listSTTLaMa.Add(new STTLaMa { Stt = 2, SttLaMa = "II" });
            _listSTTLaMa.Add(new STTLaMa { Stt = 3, SttLaMa = "III" });
            _listSTTLaMa.Add(new STTLaMa { Stt = 4, SttLaMa = "IV" });
            _listSTTLaMa.Add(new STTLaMa { Stt = 5, SttLaMa = "V" });
            _listSTTLaMa.Add(new STTLaMa { Stt = 6, SttLaMa = "VI" });
            _listSTTLaMa.Add(new STTLaMa { Stt = 7, SttLaMa = "VII" });
            _listSTTLaMa.Add(new STTLaMa { Stt = 8, SttLaMa = "VIII" });
            _listSTTLaMa.Add(new STTLaMa { Stt = 9, SttLaMa = "IX" });
            _listSTTLaMa.Add(new STTLaMa { Stt = 10, SttLaMa = "X" });
            _listSTTLaMa.Add(new STTLaMa { Stt = 11, SttLaMa = "XI" });
            _listSTTLaMa.Add(new STTLaMa { Stt = 12, SttLaMa = "XII" });
            _listSTTLaMa.Add(new STTLaMa { Stt = 13, SttLaMa = "XIII" });
            _listSTTLaMa.Add(new STTLaMa { Stt = 14, SttLaMa = "XIV" });
            _listSTTLaMa.Add(new STTLaMa { Stt = 15, SttLaMa = "XV" });
            _listSTTLaMa.Add(new STTLaMa { Stt = 16, SttLaMa = "XVI" });
            _listSTTLaMa.Add(new STTLaMa { Stt = 17, SttLaMa = "XVII" });
            _listSTTLaMa.Add(new STTLaMa { Stt = 18, SttLaMa = "XVIII" });
            _listSTTLaMa.Add(new STTLaMa { Stt = 19, SttLaMa = "XIX" });
            _listSTTLaMa.Add(new STTLaMa { Stt = 20, SttLaMa = "XX" });
            _listSTTLaMa.Add(new STTLaMa { Stt = 21, SttLaMa = "XXI" });
            _listSTTLaMa.Add(new STTLaMa { Stt = 22, SttLaMa = "XXII" });
            _listSTTLaMa.Add(new STTLaMa { Stt = 23, SttLaMa = "XXIII" });
            _listSTTLaMa.Add(new STTLaMa { Stt = 24, SttLaMa = "XXIV" });
            _listSTTLaMa.Add(new STTLaMa { Stt = 25, SttLaMa = "XXV" });
            _listSTTLaMa.Add(new STTLaMa { Stt = 26, SttLaMa = "XXVI" });
            _listSTTLaMa.Add(new STTLaMa { Stt = 27, SttLaMa = "XXVII" });
            _listSTTLaMa.Add(new STTLaMa { Stt = 28, SttLaMa = "XXIII" });
            _listSTTLaMa.Add(new STTLaMa { Stt = 29, SttLaMa = "XXIX" });
            _listSTTLaMa.Add(new STTLaMa { Stt = 30, SttLaMa = "XXX" });
            _listSTTLaMa.Add(new STTLaMa { Stt = 31, SttLaMa = "XXXI" });
            _listSTTLaMa.Add(new STTLaMa { Stt = 32, SttLaMa = "XXXII" });
            _listSTTLaMa.Add(new STTLaMa { Stt = 33, SttLaMa = "XXXIII" });
            _listSTTLaMa.Add(new STTLaMa { Stt = 34, SttLaMa = "XXXIV" });
            _listSTTLaMa.Add(new STTLaMa { Stt = 35, SttLaMa = "XXXV" });
            _listSTTLaMa.Add(new STTLaMa { Stt = 36, SttLaMa = "XXXVI" });
            _listSTTLaMa.Add(new STTLaMa { Stt = 37, SttLaMa = "XXXVII" });
            _listSTTLaMa.Add(new STTLaMa { Stt = 38, SttLaMa = "XXXVIII" });
            _listSTTLaMa.Add(new STTLaMa { Stt = 39, SttLaMa = "XXXIX" });
            _listSTTLaMa.Add(new STTLaMa { Stt = 40, SttLaMa = "XL" });
            _listSTTLaMa.Add(new STTLaMa { Stt = 41, SttLaMa = "XLI" });
            _listSTTLaMa.Add(new STTLaMa { Stt = 42, SttLaMa = "XLII" });
            _listSTTLaMa.Add(new STTLaMa { Stt = 43, SttLaMa = "XLIII" });
            _listSTTLaMa.Add(new STTLaMa { Stt = 44, SttLaMa = "XLIV" });
            _listSTTLaMa.Add(new STTLaMa { Stt = 45, SttLaMa = "XLV" });
            _listSTTLaMa.Add(new STTLaMa { Stt = 46, SttLaMa = "XLVI" });
            _listSTTLaMa.Add(new STTLaMa { Stt = 47, SttLaMa = "XLVII" });
            _listSTTLaMa.Add(new STTLaMa { Stt = 48, SttLaMa = "XLIII" });
            _listSTTLaMa.Add(new STTLaMa { Stt = 49, SttLaMa = "XLIX" });
            _listSTTLaMa.Add(new STTLaMa { Stt = 50, SttLaMa = "L" });
            return _listSTTLaMa;
        }
        class STTLaMa
        {
            private int stt;

            public int Stt
            {
                get { return stt; }
                set { stt = value; }
            }
            private string sttLaMa;

            public string SttLaMa
            {
                get { return sttLaMa; }
                set { sttLaMa = value; }
            }
        }
    }
}