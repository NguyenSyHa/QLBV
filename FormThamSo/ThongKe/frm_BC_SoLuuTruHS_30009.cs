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
    public partial class frm_BC_SoLuuTruHS_30009 : DevExpress.XtraEditors.XtraForm
    {
        public frm_BC_SoLuuTruHS_30009()
        {
            InitializeComponent();
        }

        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        List<Khoa> _lKhoa = new List<Khoa>();
        private void frm_BC_SoLuuTruHS_30009_Load(object sender, EventArgs e)
        {
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            lupTuNgay.DateTime = DateTime.Now;
            lupDenNgay.DateTime = DateTime.Now;
            var kp = (from n in data.KPhongs.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.LamSang || p.PLoai == DungChung.Bien.st_PhanLoaiKP.PhongKham)
                      select new { n.TenKP, n.MaKP }).ToList();
            if (DungChung.Bien.MaBV.Equals("12121"))
            {
                radMauBC.SelectedIndex = 1;
            }
            else
            {
                radMauBC.SelectedIndex = 0;
            }
            if (kp.Count() > 0)
            {
                Khoa themmoi1 = new Khoa();
                themmoi1.TenKP = "Chọn tất cả";
                themmoi1.MaKP = 0;
                themmoi1.Chon = true;
                _lKhoa.Add(themmoi1);
                foreach (var a in kp)
                {
                    Khoa themmoi = new Khoa();
                    themmoi.TenKP = a.TenKP;
                    themmoi.MaKP = a.MaKP;
                    themmoi.Chon = true;
                    _lKhoa.Add(themmoi);
                }
                grcKhoaphong.DataSource = _lKhoa.ToList();
            }
            //lupKho.Properties.DataSource = kp;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public bool KiemTraBC()
        {
            if (lupTuNgay.Text.Trim() == "")
            {
                MessageBox.Show("Chưa chọn ngày bắt đầu.");
                lupTuNgay.Focus();
                return false;
            }
            if (lupDenNgay.Text.Trim() == "")
            {
                MessageBox.Show("Chưa chọn ngày kết thúc.");
                lupDenNgay.Focus();
                return false;
            }
            if (Convert.ToDateTime(lupDenNgay.Text.Trim()) < Convert.ToDateTime(lupTuNgay.Text.Trim()))
            {
                MessageBox.Show("Ngày kết thúc phải lớn hơn hoặc bằng ngày bát đầu in báo cáo.");
                lupDenNgay.Focus();
                return false;
            }
            else return true;
        }

        private void btnInBC_Click(object sender, EventArgs e)
        {
                DateTime tungay = DungChung.Ham.NgayTu(lupTuNgay.DateTime);
            DateTime denngay = DungChung.Ham.NgayDen(lupDenNgay.DateTime);
            if (KiemTraBC())
            {
                List<Khoa> dsKhoa = new List<Khoa>();
                data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                dsKhoa = _lKhoa.Where(p => p.MaKP > 0).Where(p => p.Chon == true).ToList();
                //int maKhoa = Convert.ToInt32(lupKho.EditValue);
                string tenBV = data.BenhViens.SingleOrDefault(p => p.MaBV == DungChung.Bien.MaBV).TenBV;
                string khoa = string.Empty;
                int noitru = radNoiNgoaiTru.SelectedIndex;
                foreach (var item in dsKhoa)
                {
                    khoa += item.TenKP + ";";
                }
                khoa = khoa.Remove(khoa.Length - 1, 1);
                //khoa = lupKho.Properties.GetDisplayText(lupKho.EditValue);
                List<SoLuutruHS> _lHS = new List<SoLuutruHS>();
                SoLuutruHS hs = new SoLuutruHS();
                #region select
                var qbn = (from bn in data.BenhNhans.Where(p=> noitru == 2 ? true : p.NoiTru == noitru).Where(p=>p.MaKCB == DungChung.Bien.MaBV)
                           join bnkb in data.BNKBs on bn.MaBNhan equals bnkb.MaBNhan
                           join bv in data.BenhViens on bn.MaBV equals bv.MaBV into kqBV
                           from kq3 in kqBV.DefaultIfEmpty()
                           select new
                           {
                               bn.TenBNhan,
                               bn.GTinh,
                               bn.DTuong,
                               bn.Tuoi,
                               kq3.TenBV,
                               bn.DChi,
                               bn.CDNoiGT,
                               bnkb.ChanDoan,
                               bnkb.MaBNhan,
                               bnkb.MaKP
                           }).ToList();
                var qttbx = (from ttbx in data.TTboXungs
                             join h in data.DmHuyens on ttbx.MaHuyen equals h.MaHuyen into kqhuyen
                             from kqhuyen1 in kqhuyen.DefaultIfEmpty()
                             join dt in data.DanTocs on ttbx.MaDT equals dt.MaDT into kqDToc
                             from kq in kqDToc.DefaultIfEmpty()
                             join nn in data.DmNNs on ttbx.MaNN equals nn.MaNN into kqNN
                             from kq1 in kqNN.DefaultIfEmpty()
                             select new
                             {
                                 ttbx.MaBNhan,
                                 kq1.TenNN,
                                 CVChuc = kq1 != null ? (kq1.CongVienChuc == 1 ? "X" : "") : "",
                                 ThanhThi = kqhuyen1 != null ? (kqhuyen1.ThanhThi == true ? "X" : "") : "",
                                 NongThon = kqhuyen1 != null ? (kqhuyen1.ThanhThi == false ? "X" : "") : "",
                                 kq.TenDT,
                                 ttbx.NgoaiKieu,
                                 ttbx.NThan,
                             }).ToList();
                var query = (from bn in qbn
                             join tt in qttbx on bn.MaBNhan equals tt.MaBNhan
                             select new
                             {
                                 bn.TenBNhan,
                                 bn.GTinh,
                                 bn.DTuong,
                                 bn.Tuoi,
                                 tt.TenNN,
                                 CVChuc = tt.CVChuc,
                                 ThanhThi = tt.ThanhThi,
                                 NongThon = tt.NongThon,
                                 tt.TenDT,
                                 bn.TenBV,
                                 bn.DChi,
                                 tt.NgoaiKieu,
                                 bn.CDNoiGT,
                                 bn.ChanDoan,
                                 bn.MaBNhan,
                                 tt.NThan,
                                 bn.MaKP
                             }).ToList();
                int x1 = 0;
                var qBN = (from a in query
                           join vv in data.VaoViens on a.MaBNhan equals vv.MaBNhan
                           join rv in data.RaViens.Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay).Where(p =>DungChung.Bien.MaBV != "12001" ? ( p.SoLT != null && p.SoLT.Trim() != "") :  x1==0)
                                                  .Where(p => p.KetQua != "Tử vong") on vv.MaBNhan equals rv.MaBNhan
                           join kp in dsKhoa on rv.MaKP equals kp.MaKP
                           group new { a, vv, rv, kp } by new
                           {
                               a.MaBNhan,
                               a.TenBNhan,
                               a.GTinh,
                               a.DTuong,
                               a.Tuoi,
                               TenNN = a.TenNN != null ? a.TenNN : "",
                               TenDT = a.TenDT != null ? a.TenDT : "",
                               a.TenBV,
                               a.DChi,
                               a.NgoaiKieu,
                               a.CDNoiGT,
                               a.CVChuc,
                               a.ThanhThi,
                               a.NongThon,
                               rv.MaYTe,
                               SoLT = rv.SoLT != null ? rv.SoLT : "",
                               kp.TenKP,
                               vv.NgayVao,
                               vv.SoVV,
                               rv.Status,
                               ChanDoanDT = rv.ChanDoan,
                               rv.KetQua,
                               rv.SoNgaydt,
                               a.NThan,
                               rv.NgayRa
                           } into q
                           select new
                           {
                               q.Key.MaBNhan,
                               q.Key.TenBNhan,
                               q.Key.SoVV,
                               q.Key.CVChuc,
                               q.Key.ThanhThi,
                               q.Key.NongThon,
                               TuoiNam = q.Key.GTinh == 1 ? q.Key.Tuoi : null,
                               TuoiNu = q.Key.GTinh == 0 ? q.Key.Tuoi : null,
                               BHYT = (q.Key.DTuong.Contains("BHYT")) ? "X" : "",
                               TreEmDuoi6 = q.Key.Tuoi < 6 ? "X" : "",
                               TreEmDuoi15 = (q.Key.Tuoi >= 6 && q.Key.Tuoi <= 15) ? "X" : "",
                               TreEmDuoi1 = q.Key.Tuoi < 1 ? "X" : "",
                               TreEm1Den15 = (q.Key.Tuoi >= 1 && q.Key.Tuoi <= 15) ? "X" : "",
                               q.Key.TenNN,
                               q.Key.TenDT,
                               q.Key.NgoaiKieu,
                               q.Key.DChi,
                               NoiGT = q.Key.TenBV,
                               CDNoiGT = q.Key.CDNoiGT,
                               q.Key.NThan,
                               MaYTe = q.Key.MaYTe,
                               SoLT = q.Key.SoLT.Trim(),
                               KhoaDT = q.Key.TenKP,
                               NgayRa = q.Key.NgayRa,
                               NgayVV = q.Key.NgayVao,
                               NgayCV = q.Key.Status == 1 ? q.Key.NgayRa : null,
                               NgayRV = q.Key.Status != 1 ? q.Key.NgayRa : null,
                               CDKhoaDT = q.Key.ChanDoanDT,
                               Khoi = q.Key.KetQua.Equals("Khỏi") ? "X" : "",
                               DoGiam = q.Key.KetQua.Equals("Đỡ|Giảm") ? "X" : "",
                               KhongTDoi = q.Key.KetQua.Equals("Không T.đổi") ? "X" : "",
                               NangHon = q.Key.KetQua.Equals("Nặng hơn") ? "X" : "",
                               q.Key.SoNgaydt
                           }).OrderBy(p => p.SoLT).ToList();
                #region add to list
                var kkb = data.KPhongs.Where(p => p.PLoai == "Phòng khám").ToList();
                foreach (var item in qBN)
                {
                    hs = new SoLuutruHS();
                    if (!string.IsNullOrEmpty(item.MaYTe))
                    {
                        string maYT = item.MaYTe.Trim();
                        if (item.MaYTe.Contains("/"))
                        {
                            string temp = string.Empty;
                            int lastIndexItem = maYT.LastIndexOf("/");
                            int len = maYT.Length - 1 - lastIndexItem;
                            temp = maYT.Substring(lastIndexItem + 1, len);
                            hs.MaYTe = temp;
                            //if (CheckIsNumber(temp))
                            //    hs.MaYTe = temp; //Convert.ToInt32(temp);
                        }
                        else
                        {
                            if (maYT != "")
                            {
                                hs.MaYTe = maYT;
                                //if (CheckIsNumber(maYT))
                                //    hs.MaYTe = Convert.ToDouble(maYT);
                            }
                        }
                    }
                    hs.CVChuc = item.CVChuc;
                    hs.ThanhThi = item.ThanhThi;
                    hs.NongThon = item.NongThon;
                    hs.SoLT = item.SoLT.Trim();
                    hs.SoVV = item.SoVV;
                    hs.KhoaDT = item.KhoaDT;
                    hs.MaBNhan = item.MaBNhan;
                    hs.TenBNhan = item.TenBNhan;
                    hs.TuoiNam = item.TuoiNam.ToString();
                    hs.TuoiNu = item.TuoiNu.ToString();
                    hs.BHYT = item.BHYT;
                    hs.TreEmDuoi6 = item.TreEmDuoi6;
                    hs.TreEmDuoi15 = item.TreEmDuoi15;
                    hs.TenNN = item.TenNN;
                    hs.TenDT = item.TenDT;
                    hs.NgoaiKieu = item.NgoaiKieu;
                    hs.DChi = item.DChi;
                    hs.NoiGT = item.NoiGT;
                    hs.NgayRa = item.NgayRa;
                    hs.NgayVV = item.NgayVV;
                    hs.NgayRV = item.NgayRV;
                    hs.NgayCV = item.NgayCV;
                    hs.CDNoiGT = item.CDNoiGT;
                    var cd = query.Where(p => p.MaBNhan == item.MaBNhan).ToList();
                    foreach (var cdKB in cd)
                    {
                        var cdkb = kkb.Where(p => p.MaKP == cdKB.MaKP).ToList();
                        if (cdkb.Count > 0)
                            hs.CDKhoaKB += cdKB.ChanDoan + "; ";
                    }
                    hs.CDKhoaDT = item.CDKhoaDT;
                    hs.Khoi = item.Khoi;
                    hs.DoGiam = item.DoGiam;
                    hs.NangHon = item.NangHon;
                    hs.KhongTDoi = item.KhongTDoi;
                    hs.NThan = item.NThan;
                    hs.SoNgaydt = Convert.ToInt32(item.SoNgaydt);
                    _lHS.Add(hs);
                }
                #endregion
                #endregion

                if (radMauBC.SelectedIndex == 0)//mẫu dùng chung
                {
                    string[] _arr = new string[] { "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@" };
                    int[] _arrWidth = new int[] {   7,   9,  12,  25,   8,   8,   7,  13,  15,  12,   6,   9,  31,  13,  20,  20,  20,  22,  25,  21,   4,   8,   8,  13,  10,  11};
                    int num = 1;
                    DungChung.Bien.MangHaiChieu = new Object[_lHS.Count + 1, 26];
                    string[] _tieude = { "Mã Y tế", "Số lưu trữ", "Khoa điều trị", "Họ tên người bệnh", "Tuổi Nam", "Tuổi Nữ", "Có BHYT", "Trẻ em < 6 tuổi", "Trẻ em 6 - 15 tuổi", "Nghề nghiệp", "Dân tộc", "Ngoại kiều", "Địa chỉ", "Nơi giới thiệu", "Ngày giờ Vào viện", "Ngày giờ Chuyển viện", "Ngày giờ Ra viện", "Chẩn đoán nơi giới thiệu", "Chẩn đoán khoa Khám bệnh", "Chẩn đoán khoa điều trị", "Khỏi", "Đỡ| Giảm", "Nặng hơn", "Không thay đổi", "Người thân", "Ngày điều trị" };
                    for (int i = 0; i < _tieude.Length; i++)
                    {
                        DungChung.Bien.MangHaiChieu[0, i] = _tieude[i];
                    }


                    foreach (var r in _lHS.OrderBy(p => p.NgayRa))
                    {
                        DungChung.Bien.MangHaiChieu[num, 0] = r.MaYTe;
                        DungChung.Bien.MangHaiChieu[num, 1] = r.SoLT;
                        DungChung.Bien.MangHaiChieu[num, 2] = r.KhoaDT;
                        DungChung.Bien.MangHaiChieu[num, 3] = r.TenBNhan;
                        DungChung.Bien.MangHaiChieu[num, 4] = (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" && r.TuoiNam != "") ? DungChung.Ham.TuoitheoThang(data, Convert.ToInt32(r.MaBNhan), "12-00") : r.TuoiNam;
                        DungChung.Bien.MangHaiChieu[num, 5] = (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" && r.TuoiNu != "") ? DungChung.Ham.TuoitheoThang(data, Convert.ToInt32(r.MaBNhan), "12-00") : r.TuoiNu;
                        DungChung.Bien.MangHaiChieu[num, 6] = r.BHYT;
                        DungChung.Bien.MangHaiChieu[num, 7] = r.TreEmDuoi6;
                        DungChung.Bien.MangHaiChieu[num, 8] = r.TreEmDuoi15;
                        DungChung.Bien.MangHaiChieu[num, 9] = r.TenNN;
                        DungChung.Bien.MangHaiChieu[num, 10] = r.TenDT;
                        DungChung.Bien.MangHaiChieu[num, 11] = r.NgoaiKieu;
                        DungChung.Bien.MangHaiChieu[num, 12] = r.DChi;
                        DungChung.Bien.MangHaiChieu[num, 13] = r.NoiGT;
                        DungChung.Bien.MangHaiChieu[num, 14] = r.NgayVV;
                        DungChung.Bien.MangHaiChieu[num, 15] = r.NgayCV;
                        DungChung.Bien.MangHaiChieu[num, 16] = r.NgayRV;
                        DungChung.Bien.MangHaiChieu[num, 17] = r.CDNoiGT;
                        DungChung.Bien.MangHaiChieu[num, 18] = r.CDKhoaKB;
                        DungChung.Bien.MangHaiChieu[num, 19] = r.CDKhoaDT;
                        DungChung.Bien.MangHaiChieu[num, 20] = r.Khoi;
                        DungChung.Bien.MangHaiChieu[num, 21] = r.DoGiam;
                        DungChung.Bien.MangHaiChieu[num, 22] = r.NangHon;
                        DungChung.Bien.MangHaiChieu[num, 23] = r.KhongTDoi;
                        DungChung.Bien.MangHaiChieu[num, 24] = r.NThan;
                        DungChung.Bien.MangHaiChieu[num, 25] = r.SoNgaydt;
                        num++;
                    }

                    BaoCao.Rep_BC_SoLuuTruHS_30009 rep = new BaoCao.Rep_BC_SoLuuTruHS_30009();
                    frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "Báo cáo SoLuuTruHS_30009", "C:\\BC_SoLuuTruHS_30009.xls", true, this.Name);
                    rep.lblBenhVien.Text = "BỆNH VIỆN: " + tenBV.ToUpper();
                    rep.lblKhoa.Text = "KHOA: " + khoa.ToUpper();
                    rep.lblTuNgayDenNgay.Text = "(Từ ngày " + tungay.ToString("dd/MM/yyyy") + " đến ngày " + denngay.ToString("dd/MM/yyyy") + " )";
                    rep.DataSource = _lHS.OrderBy(p => p.NgayRa).ToList();// sắp xếp theo ngày ra viện c.liễu yc 24-10
                    rep.BindingData();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                }
                if (radMauBC.SelectedIndex == 1)//BV YHCT Lai Châu
                {
                    string[] _arr = new string[] { "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@" };
                    int[] _arrWidth = new int[] { 9, 10, 25, 8, 8, 17, 17, 17, 18, 16, 16, 12, 12, 13, 20, 20, 20, 23, 25, 21, 28, 4, 8, 8, 13 };
                    int num = 1;
                    DungChung.Bien.MangHaiChieu = new Object[_lHS.Count + 1, 26];
                    string[] _tieude = { "Số lưu trữ", "Số vào viện", "Họ tên người bệnh", "Tuổi Nam", "Tuổi Nữ", "Công viên chức", "Có BHYT", "Nhân dân Thành thị", "Nhân dân nông thôn", "Trẻ em < 12 tháng", "Trẻ em 1 - 15 tuổi", "Nghề nghiệp", "Địa chỉ", "Nơi giới thiệu", "Ngày giờ Vào viện", "Ngày giờ Chuyển viện", "Ngày giờ Ra viện", "Chẩn đoán của tuyến dưới", "Chẩn đoán khoa Khám bệnh", "Chẩn đoán khoa điều trị", "Chẩn đoán khoa giải phẫu bệnh", "Khỏi", "Đỡ| Giảm", "Nặng hơn", "Không thay đổi" };
                    for (int i = 0; i < _tieude.Length; i++)
                    {
                        DungChung.Bien.MangHaiChieu[0, i] = _tieude[i];
                    }


                    foreach (var r in _lHS.OrderBy(p => p.NgayRa))
                    {
                        DungChung.Bien.MangHaiChieu[num, 0] = r.SoLT;
                        DungChung.Bien.MangHaiChieu[num, 1] = r.SoVV;
                        DungChung.Bien.MangHaiChieu[num, 2] = r.TenBNhan;
                        DungChung.Bien.MangHaiChieu[num, 4] = (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" && r.TuoiNam != "") ? DungChung.Ham.TuoitheoThang(data, Convert.ToInt32(r.MaBNhan), "12-00") : r.TuoiNam;
                        DungChung.Bien.MangHaiChieu[num, 5] = (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" && r.TuoiNu != "") ? DungChung.Ham.TuoitheoThang(data, Convert.ToInt32(r.MaBNhan), "12-00") : r.TuoiNu;
                        DungChung.Bien.MangHaiChieu[num, 5] = r.CVChuc;
                        DungChung.Bien.MangHaiChieu[num, 6] = r.BHYT;   
                        DungChung.Bien.MangHaiChieu[num, 7] = r.ThanhThi;
                        DungChung.Bien.MangHaiChieu[num, 8] = r.NongThon;
                        DungChung.Bien.MangHaiChieu[num, 9] = r.TreEmDuoi1;
                        DungChung.Bien.MangHaiChieu[num, 10] = r.TreEm1Den15;
                        DungChung.Bien.MangHaiChieu[num, 11] = r.TenNN;
                        DungChung.Bien.MangHaiChieu[num, 12] = r.DChi;
                        DungChung.Bien.MangHaiChieu[num, 13] = r.NoiGT;
                        DungChung.Bien.MangHaiChieu[num, 14] = r.NgayVV;
                        DungChung.Bien.MangHaiChieu[num, 15] = r.NgayCV;
                        DungChung.Bien.MangHaiChieu[num, 16] = r.NgayRV;
                        DungChung.Bien.MangHaiChieu[num, 17] = r.CDNoiGT;
                        DungChung.Bien.MangHaiChieu[num, 18] = r.CDKhoaKB;
                        DungChung.Bien.MangHaiChieu[num, 19] = r.CDKhoaDT;
                        DungChung.Bien.MangHaiChieu[num, 20] = null;
                        DungChung.Bien.MangHaiChieu[num, 21] = r.Khoi;
                        DungChung.Bien.MangHaiChieu[num, 22] = r.DoGiam;
                        DungChung.Bien.MangHaiChieu[num, 23] = r.NangHon;
                        DungChung.Bien.MangHaiChieu[num, 24] = r.KhongTDoi;

                        num++;
                    }

                    BaoCao.Rep_BC_SoLuuTruHS_12121 rep = new BaoCao.Rep_BC_SoLuuTruHS_12121();
                    frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "Báo cáo SoLuuTruHS_12121", "C:\\BC_SoLuuTruHS_12121.xls", true, this.Name);
                    rep.lblBenhVien.Text = "BỆNH VIỆN: " + tenBV.ToUpper();
                    rep.lblKhoa.Text = "KHOA: " + khoa.ToUpper();
                    rep.lblTuNgayDenNgay.Text = "(Từ ngày " + tungay.ToString("dd/MM/yyyy") + " đến ngày " + denngay.ToString("dd/MM/yyyy") + " )";
                    rep.DataSource = _lHS.OrderBy(p => p.NgayRa).ToList();// sắp xếp theo ngày ra viện c.liễu yc 24-10
                    rep.BindingData();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                }
            }
        }
        #region function kiểm tra chuỗi
        private bool CheckIsNumber(string str)
        {
            bool kt = false;
            for (int i = 0; i < str.Length; i++)
            {
                if (char.IsNumber(str[i]))
                    kt = true;
                else
                {
                    kt = false;
                    break;
                }
            }
            return kt;
        }
        #endregion
        #region class SoLuuTruHS
        public class SoLuutruHS
        {
            public string CVChuc { get; set; }
            public string ThanhThi { get; set; }
            public string NongThon { get; set; }
            public string MaYTe { get; set; }
            public string SoLT { get; set; }
            public string SoVV { get; set; }
            public string KhoaDT { get; set; }
            public int? MaBNhan { get; set; }
            public string TenBNhan { get; set; }
            public string TuoiNam { get; set; }
            public string TuoiNu { get; set; }
            public string BHYT { get; set; }
            public string TreEmDuoi6 { get; set; }
            public string TreEmDuoi15 { get; set; }
            public string TreEmDuoi1 { get; set; }
            public string TreEm1Den15 { get; set; }
            public string TenNN { get; set; }
            public string TenDT { get; set; }
            public string NgoaiKieu { get; set; }
            public string DChi { get; set; }
            public string NoiGT { get; set; }
            public DateTime? NgayVV { get; set; }
            public DateTime? NgayRV { get; set; }
            public DateTime? NgayCV { get; set; }
            public DateTime? NgayRa { get; set; }
            public string CDNoiGT { get; set; }
            public string CDKhoaKB { get; set; }
            public string CDKhoaDT { get; set; }
            public string Khoi { get; set; }
            public string DoGiam { get; set; }
            public string NangHon { get; set; }
            public string KhongTDoi { get; set; }
            public string NThan { get; set; }
            public int SoNgaydt { get; set; }
        }
        #endregion
        #region class Khoa
        private class Khoa
        {
            private string tenKP;
            private int maKP;
            private bool chon;
            public string TenKP
            { set { tenKP = value; } get { return tenKP; } }
            public int MaKP
            { set { maKP = value; } get { return maKP; } }
            public bool Chon
            { set { chon = value; } get { return chon; } }
        }
        #endregion

        private void grvKhoaphong_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "colChon")
            {
                if (grvKhoaphong.GetFocusedRowCellValue("TenKP") != null)
                {
                    string Ten = grvKhoaphong.GetFocusedRowCellValue("TenKP").ToString();

                    if (Ten == "Chọn tất cả")
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
    }
}