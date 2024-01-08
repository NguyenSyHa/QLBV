using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.IO;

namespace QLBV.FormThamSo
{
    public partial class frm_KetQuaSieuAmThai35w_12345 : DevExpress.XtraEditors.XtraForm
    {
        public frm_KetQuaSieuAmThai35w_12345(int idcls, int makp)
        {
            InitializeComponent();
            _idcls = idcls;
            _maKP = makp;
        }

        int _idcls = 0, _maKP = 0;

        int giaodien = 0;

        public class Ketqua
        {
            public int stt { get; set; }
            public string tendvct { get; set; }
            public string ketqua { get; set; }
        }

        public class BienKQ
        {
            public string Soluongthai { get; set; }
            public string CuDongThai { get; set; }
            public string NgoiThai { get; set; }
            public string DKLuongDinh_BPD { get; set; }
            public string DKChamTranDau_OFD { get; set; }
            public string ChuViDau_HC { get; set; }
            public string DKTruocSauBung_APTD { get; set; }
            public string DKNgangBung_TTD { get; set; }
            public string ChuViBung_AC { get; set; }
            public string ChieuDaiXuongDui_FL { get; set; }
            public string TuoiThai1 { get; set; }
            public string TuoiThai2 { get; set; }
            public string DuKienSinh { get; set; }
            public string TrongLuongThai { get; set; }

            public string HamMat { get; set; }
            public string TuChi { get; set; }
            public string XuongSo { get; set; }
            public string CotSongHeXuong { get; set; }
            public string CacNaoThat { get; set; }
            public string TimThai { get; set; }
            public string TanSoTim { get; set; }
            public string OBung1 { get; set; }
            public string OBung2 { get; set; }
            public string OBung3 { get; set; }
            public string OBung4 { get; set; }
            public string OBung5 { get; set; }
            public string OBung6 { get; set; }
            public string Nguc { get; set; }


            public string RauThai { get; set; }
            public string RauThai2 { get; set; }
            public string RauThai3 { get; set; }
            public string RauThai4 { get; set; }
            public string DayRon { get; set; }
            public string NuocOi { get; set; }

            public string DopplerTrai { get; set; }
            public string DopplerPhai { get; set; }
            public string DopplerRon { get; set; }
            public string DopplerNaoGiua { get; set; }

        }
        public List<Ketqua> KQThai3540Tuan()
        {
            List<Ketqua> kq = new List<Ketqua>();
            kq.Add(new Ketqua { stt = 1, tendvct = "Số lượng thai", ketqua = " Một thai" });
            kq.Add(new Ketqua { stt = 2, tendvct = "Cử động thai", ketqua = " Tốt" });
            kq.Add(new Ketqua { stt = 3, tendvct = "Ngôi thai", ketqua = " Ngôi đầu" });
            kq.Add(new Ketqua { stt = 4, tendvct = "ĐK lưỡng đỉnh(BPD)", ketqua = "  mm" });
            kq.Add(new Ketqua { stt = 5, tendvct = "ĐK chẩm trán", ketqua = "  mm" });
            kq.Add(new Ketqua { stt = 6, tendvct = "Chu vi đầu(HC)", ketqua = "  mm" });
            kq.Add(new Ketqua { stt = 7, tendvct = "ĐK trước sau bụng(APTD)", ketqua = "  mm" });
            kq.Add(new Ketqua { stt = 8, tendvct = "ĐK ngang bụng(TTD)", ketqua = "  mm" });
            kq.Add(new Ketqua { stt = 9, tendvct = "Chu vi vòng bụng(AC)", ketqua = "  mm" });
            kq.Add(new Ketqua { stt = 10, tendvct = "Chiều dài xương đùi(FL)", ketqua = "  mm" });
            kq.Add(new Ketqua { stt = 11, tendvct = "Tuổi thai (Theo siêu âm lúc 11-14 tuần)", ketqua = "  tuần  ngày" });
            kq.Add(new Ketqua { stt = 12, tendvct = "Tuổi thai dựa theo số đo hiện tại", ketqua = " ~ tuần  ngày  (± 7 ngày)" });
            kq.Add(new Ketqua { stt = 13, tendvct = "Dự kiến sinh", ketqua = "  /  /20    (± 7 ngày)" });
            kq.Add(new Ketqua { stt = 14, tendvct = "Trọng lượng thai ước tính", ketqua = "  gr(±  gr)" });
            kq.Add(new Ketqua { stt = 15, tendvct = "1. Hàm mặt", ketqua = " Bình thường" });
            kq.Add(new Ketqua { stt = 16, tendvct = "2. Tứ chi", ketqua = " Mỗi chi ba đoạn, vận động bình thường, không bị khoèo" });
            kq.Add(new Ketqua { stt = 17, tendvct = "3. Xương sọ", ketqua = " Phát triển bình thường" });
            kq.Add(new Ketqua { stt = 18, tendvct = "4. Cột sống - hệ xương", ketqua = " Hiện tại không thấy bất thường." });

            kq.Add(new Ketqua { stt = 19, tendvct = "5. Các não thất", ketqua = " Không giãn, không thấy bất thường." });
            kq.Add(new Ketqua { stt = 20, tendvct = "6. Tim thai", ketqua = " Không to. Cấu trúc 4 buồng. Không thấy tràn dịch màng ngoài tim" });
            kq.Add(new Ketqua { stt = 21, tendvct = "Tần số tim", ketqua = "  ck/phút" });
            kq.Add(new Ketqua { stt = 22, tendvct = "7. Ổ bụng", ketqua = " Thành bụng kín." });
            kq.Add(new Ketqua { stt = 23, tendvct = "Ổ bụng", ketqua = " Cơ hoành bình thường" });
            kq.Add(new Ketqua { stt = 24, tendvct = "Ổ bụng", ketqua = " Dạ dày bình thường" });
            kq.Add(new Ketqua { stt = 25, tendvct = "Ổ bụng", ketqua = " Không thấy bất thường vùng gan." });
            kq.Add(new Ketqua { stt = 26, tendvct = "Ổ bụng", ketqua = " Thận hai bên bình thường" });
            kq.Add(new Ketqua { stt = 27, tendvct = "Ổ bụng", ketqua = " Bàng quang nằm đúng vị trí bình thường, có nước tiểu" });
            kq.Add(new Ketqua { stt = 28, tendvct = "8. Ngực", ketqua = " Phổi hai bên kích thước bình thường, âm vang đều, không thấy nang tuyến, không thấy tràn dịch màng phổi" });
            kq.Add(new Ketqua { stt = 29, tendvct = "Rau thai", ketqua = " Vị trí bám: Mặt sau tử cung." });
            kq.Add(new Ketqua { stt = 30, tendvct = "Rau thai", ketqua = " Dày: ~23mm. Không thấy tụ dịch sau rau." });
            kq.Add(new Ketqua { stt = 31, tendvct = "Rau thai", ketqua = " Mép bánh rau: Bình thường." });
            kq.Add(new Ketqua { stt = 32, tendvct = "Dây rốn", ketqua = " 2 động mạch, 1 tĩnh mạch." });
            kq.Add(new Ketqua { stt = 33, tendvct = "Nước ối", ketqua = " Dịch ối trong. Chỉ số ối(AFI): mm" });
            kq.Add(new Ketqua { stt = 34, tendvct = "Doppler ĐM tử cung trái", ketqua = " Bình thường" });
            kq.Add(new Ketqua { stt = 35, tendvct = "Doppler ĐM tử cung phải", ketqua = " Bình thường" });
            kq.Add(new Ketqua { stt = 36, tendvct = "Doppler ĐM rốn", ketqua = " Bình thường" });
            kq.Add(new Ketqua { stt = 37, tendvct = "Doppler DM não giữa", ketqua = " Bình thường" });
            return kq;
        }
        int mabn = 0, madvcd = 0, _idcd = 0;
        bool _tamthu = true;
        string _LoaiThangThaiNhi = "";
        void LoadKQMau()
        {
            QLBV_Database.QLBVEntities db = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

            var kqcls = (from cls in db.CLS.Where(p => p.IdCLS == _idcls)
                         join cd in db.ChiDinhs on cls.IdCLS equals cd.IdCLS
                         join clsct in db.CLScts on cd.IDCD equals clsct.IDCD
                         select new { cls.NgayTH, cd.MaCBth, cd.MaDV, cd.Status, cd.IDCD, clsct.KetQua, cls.MaBNhan, cd.KetLuan, cd.MaMay, clsct.DuongDan, cd.LoiDan, clsct.DuongDan2 }).ToList();
            madvcd = kqcls.First().MaDV ?? 0;
            var tendv = db.DichVus.Where(p => p.MaDV == madvcd).Select(p => p.TenDV).FirstOrDefault();
            if (tendv != null)
            {
                txttendv.Text = tendv;
                grcketqua.DataSource = null;
                //if (tendv.Contains("Siêu âm thai"))
                //{
                List<Ketqua> kq = KQThai3540Tuan();
                grcketqua.DataSource = kq;
                //}
                if (mmKLSieuam.Text.Trim() == "" || TTLuu == 0)
                {
                    mmKLSieuam.Text = "• Trong tử cung có hình ảnh một thai ~ thai   tuần   ngày (± 7 ngày)." + Environment.NewLine + "• Hiện tại không phát hiện bất thường trên siêu âm " + Environment.NewLine + "• Hẹn kiểm tra lại sau.....tuần.";

                }
                if (mmLoidanSieuAm.Text.Trim() == "" || TTLuu == 0)
                {
                    mmLoidanSieuAm.Text = "- Siêu âm không ảnh tới sức khỏe của Mẹ và Thai nhi. Khi mang thai nên uống 2-2,5 lít nước mỗi ngày" + Environment.NewLine + "- Siêu âm có thể phát hiện 75-80% các dị tật thường gặp ở thai nhi." + Environment.NewLine + "Thai phụ cần đặc biệt chú ý tới cử động của thai:" + Environment.NewLine + "* Theo dõi trong 30 phút sẽ có ít nhất 04 lần thai cử động-đạp (khi thai ngủ sẽ có thể ít hơn) là bình thường" + Environment.NewLine + "* Nếu trong 4 giờ(h) theo dõi liên tục mà có ít hơn 10 lần thai cử động thì cần đi khám ngay.";
                }
            }
        }

        private void frm_KetQuaSieuAmThai35w_12345_Load(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities db = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

            string _makp = ";" + _maKP.ToString() + ";";
            var c = (from cb in db.CanBoes.Where(p => p.Status == 1).Where(p => p.MaKPsd.Contains(_makp)).Where(p => p.CapBac.ToLower().Contains("bs") || p.CapBac.ToLower().Contains("bác sĩ") || p.CapBac.ToLower().Contains("bác sỹ") || p.CapBac.ToLower().Contains("ys") || p.CapBac.ToLower().Contains("y sĩ") || p.CapBac.ToLower().Contains("y sỹ") || p.CapBac.ToLower().Contains("giáo sư") || p.CapBac.ToLower().Contains("gs") || p.CapBac.ToLower().Contains("tiến sĩ") || p.CapBac.ToLower().Contains("ts"))
                     select new
                     {
                         cb.MaCB,
                         cb.TenCB,
                         cb.MaKPsd
                     }).ToList();
            LupCanBo.Properties.DataSource = c.ToList();
            var madv = (from ts in db.TaiSans.Where(p => p.MaKP == _maKP) select new { ts.MaDV }).ToList();
            var mamay = (from m in madv join dv in db.DichVus on m.MaDV equals dv.MaDV select new { dv.MaQD, dv.TenDV }).ToList();
            lupMaMay.Properties.DataSource = mamay;
            if (mamay.Count > 0)
            {
                lupMaMay.Properties.DataSource = mamay;
                lupMaMay.EditValue = mamay.First().MaQD;
            }
            var kqcls = (from cls in db.CLS.Where(p => p.IdCLS == _idcls)
                         join cd in db.ChiDinhs on cls.IdCLS equals cd.IdCLS
                         join clsct in db.CLScts on cd.IDCD equals clsct.IDCD
                         select new { cls.NgayTH, cls.MaCBth, cd.MaDV, cd.Status, cd.IDCD, clsct.KetQua, cls.MaBNhan, cd.KetLuan, cd.MaMay, clsct.DuongDan, cd.LoiDan, clsct.DuongDan2 }).ToList();

            if (kqcls.Count > 0)
            {
                _idcd = kqcls.First().IDCD;
                if (kqcls.First().Status == 0)
                {
                    TTLuu = 0;
                    LoadKQMau();
                    EnabledControl(false);

                }
                else
                {
                    EnabledControl(true);
                    TTLuu = 0;
                    if (kqcls.First().KetQua != null && kqcls.First().KetQua.Contains(";"))
                    {
                        LoadKetQua(kqcls.First().KetQua);
                    }
                }
                if (kqcls.First().DuongDan != null && File.Exists(kqcls.First().DuongDan))
                {
                    _fileanh = kqcls.First().DuongDan;
                    ptSieuam.Image = Image.FromFile(_fileanh);
                }
                else
                    ptSieuam.Image = null;


                if (!string.IsNullOrWhiteSpace(kqcls.First().KetLuan))
                {
                    mmKLSieuam.Text = kqcls.First().KetLuan;
                }
                if (kqcls.First().MaBNhan != null)
                {
                    mabn = kqcls.First().MaBNhan ?? 0;
                    var bn = db.BenhNhans.Where(p => p.MaBNhan == mabn).FirstOrDefault();
                    if (bn != null)
                    {
                        txttenbn.Text = bn.TenBNhan;
                        txtmabn.Text = bn.MaBNhan.ToString();
                    }
                }
                if (kqcls.First().MaDV != null)
                {
                    madvcd = kqcls.First().MaDV ?? 0;
                    var tendv = db.DichVus.Where(p => p.MaDV == madvcd).Select(p => p.TenDV).FirstOrDefault();
                    if (tendv != null)
                        txttendv.Text = tendv;
                }
                if (kqcls.First().LoiDan != null)
                {
                    mmLoidanSieuAm.Text = kqcls.First().LoiDan;
                }
                if (kqcls.First().NgayTH != null)
                {
                    lupNgayTH.DateTime = Convert.ToDateTime(kqcls.First().NgayTH);
                }
                else
                    lupNgayTH.DateTime = DateTime.Now;
                if (kqcls.First().MaCBth != null && kqcls.First().MaCBth.ToString() != "")
                {
                    LupCanBo.EditValue = kqcls.First().MaCBth;
                }
                else
                {
                    if (kqcls.First().Status == 1)
                        LupCanBo.EditValue = "";
                    else LupCanBo.EditValue = DungChung.Bien.MaCB;
                }
                if (!string.IsNullOrEmpty(kqcls.First().MaMay))
                    lupMaMay.EditValue = kqcls.First().MaMay;
                if (!DungChung.Ham._checkTamThu(db, String.IsNullOrEmpty(txtmabn.Text) ? 0 : Convert.ToInt32(txtmabn.Text), _idcls))
                {
                    _tamthu = false;
                }
            }

        }
        int TTLuu = -1;
        void LoadKetQua(string KetQua)
        {
            #region 12345

            string[] arrkq = KetQua.Split(';');
            int stt = 1;
            List<Ketqua> kq = new List<Ketqua>();
            foreach (var item in arrkq)
            {
                Ketqua moi = new Ketqua();
                if (item.Contains(":"))
                {
                    string[] ar = item.Split(':');
                    if (item.Contains("Rau thai") && ar.Length > 2)
                    {
                        moi.stt = stt;
                        moi.tendvct = ar[0];

                        moi.ketqua = ar[1] + ": " + ar[2];

                    }
                    else
                    {
                        moi.stt = stt;
                        moi.tendvct = ar[0];
                        moi.ketqua = ar[1];
                    }
                }
                else
                {
                    moi.stt = stt;
                    moi.tendvct = item;
                }
                kq.Add(moi);
            }
            grcketqua.DataSource = null;
            grcketqua.DataSource = kq;
        }

            #endregion
        string _fileanh = "", _fileanh2 = "";
        Boolean suaanhSA = true;
        private void sbtChonanhSA1_Click(object sender, EventArgs e)
        {
            bool tontai = true;
            if (ptSieuam.Image == null)
            {
                tontai = false;
            }
            if (tontai)
            {
                DialogResult _dresult = MessageBox.Show("Bạn có muốn chọn lại ảnh!", "Hỏi lưu", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (_dresult == DialogResult.Yes)
                {
                    string fileName = string.Empty;
                    OpenFileDialog op = new OpenFileDialog();
                    op.Multiselect = false;

                    op.Filter = "JPEG (*.jpg)|*.jpg|BMP(*.bmp)| *.bmp";
                    if (op.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        fileName = op.FileName;
                        _fileanh = fileName;
                        if (!string.IsNullOrEmpty(_fileanh))
                            ptSieuam.Image = Image.FromFile(_fileanh);
                        else
                            ptSieuam.Image = null;
                        suaanhSA = true;
                    }
                }
            }
            else
            {
                string fileName = string.Empty;
                OpenFileDialog op = new OpenFileDialog();
                op.Multiselect = false;
                op.Filter = "JPEG (*.jpg)|*.jpg|BMP(*.bmp)| *.bmp";
                if (op.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    fileName = op.FileName;
                    _fileanh = fileName;
                    if (!string.IsNullOrEmpty(_fileanh))
                        ptSieuam.Image = Image.FromFile(_fileanh);
                    else
                        ptSieuam.Image = null;
                    suaanhSA = true;
                }
            }

        }

        public string LuuKetQua()
        {
            string kq = "";
            for (int i = 0; i < grvketqua.DataRowCount; i++)
            {
                kq += grvketqua.GetRowCellValue(i, coltendvct).ToString() + (grvketqua.GetRowCellValue(i, colketqua) != null ? (":" + grvketqua.GetRowCellValue(i, colketqua).ToString()) : "") + ";";
            }
            return kq;


        }
        private void sbtXoaanhSA1_Click(object sender, EventArgs e)
        {
            _fileanh = "";
            ptSieuam.Image = null;
            suaanhSA = true;
        }
        public string layTenFileAnh(string fileAnhTMH, string tenfileanh)
        {
            if (!string.IsNullOrEmpty(fileAnhTMH))
            {
                if (!File.Exists(tenfileanh))
                {
                    File.Copy(fileAnhTMH, tenfileanh);

                }
                else
                {
                    for (int i = 0; i < 100; i++)
                    {
                        string a = "";
                        string ex = Path.GetExtension(tenfileanh);
                        a = tenfileanh.Replace(ex, i + ex);
                        if (!File.Exists(a))
                        {
                            File.Copy(fileAnhTMH, a);
                            tenfileanh = a;
                            break;
                        }
                    }
                }
            }
            return tenfileanh;
        }
        private void btnLuu_Click(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities db = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            if (_tamthu == false)
            {
                MessageBox.Show("Bệnh nhân chưa nộp tiền dịch vụ, bạn không thể lưu");
            }
            if (_tamthu && KT())
            {
                db = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                bool tieptuc = true;
                if (string.IsNullOrEmpty(mmKLSieuam.Text) || LupCanBo.EditValue == null || LupCanBo.EditValue == null)
                {
                    DialogResult _dresult3 = MessageBox.Show("Bạn chưa nhập đầy đủ kết quả, bạn có muốn lưu dữ liệu không?", "Hỏi lưu", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (_dresult3 != DialogResult.Yes)
                    {
                        tieptuc = false;
                    }

                }
                List<CL> cls = db.CLS.Where(p => p.IdCLS == _idcls).ToList();
                List<ChiDinh> cd = db.ChiDinhs.Where(p => p.IdCLS == _idcls).ToList();
                List<CLSct> clsct = db.CLScts.Where(p => p.IDCD == _idcd).ToList();
                if (tieptuc)
                {

                    if (LupCanBo.EditValue != null)
                        cls.First().MaCBth = LupCanBo.EditValue.ToString();
                    else
                        cls.First().MaCBth = "";
                    cls.First().Status = 1;
                    foreach (var item in cd)
                    {
                        item.KetLuan = mmKLSieuam.Text;
                        item.LoiDan = mmLoidanSieuAm.Text;
                        item.TamThu = 1;
                        item.Status = 1;
                        item.NgoaiGioHC = Convert.ToInt32(FormThamSo.frm_UpdateLetet.CheckNGioHC(lupNgayTH.DateTime));
                    }
                    string _tenfileanh = "", _tenfileanh2 = "";
                    //fileanh1
                    if (!string.IsNullOrEmpty(_fileanh))
                    {
                        _tenfileanh = DungChung.Bien.DuongDan + "\\";
                        _tenfileanh += mabn + _idcls + ".jpg";
                        try
                        {
                            if (!string.IsNullOrEmpty(_fileanh))
                            {
                                if (!File.Exists(_tenfileanh))
                                {
                                    File.Copy(_fileanh, _tenfileanh);
                                }
                                else
                                {
                                    DialogResult _dresult1 = MessageBox.Show("tệp đã có, bạn có muốn lưu đè", "Hỏi lưu", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                    if (_dresult1 == DialogResult.Yes)
                                    {
                                        _tenfileanh = layTenFileAnh(_fileanh, _tenfileanh);
                                    }
                                }
                            }
                        }
                        catch
                        {
                            MessageBox.Show("không lưu được ảnh");
                        }
                    }
                    //fileanh2
                    if (!string.IsNullOrEmpty(_fileanh2))
                    {
                        _tenfileanh2 = DungChung.Bien.DuongDan + "\\";
                        _tenfileanh2 += mabn + _idcls + ".jpg";
                        try
                        {
                            if (!string.IsNullOrEmpty(_fileanh2))
                            {
                                if (!File.Exists(_tenfileanh2))
                                {
                                    File.Copy(_fileanh2, _tenfileanh2);
                                }
                                else
                                {
                                    DialogResult _dresult1 = MessageBox.Show("tệp đã có, bạn có muốn lưu đè", "Hỏi lưu", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                    if (_dresult1 == DialogResult.Yes)
                                    {
                                        _tenfileanh2 = layTenFileAnh(_fileanh2, _tenfileanh2);
                                    }
                                }
                            }
                        }
                        catch
                        {
                            MessageBox.Show("không lưu được ảnh");
                        }
                    }
                    foreach (var a in clsct)
                    {
                        a.KetQua = LuuKetQua();
                        if (suaanhSA)
                        {
                            a.DuongDan = _tenfileanh;
                            a.DuongDan2 = _tenfileanh2;
                        }
                    }
                }
                string kl = "";
                foreach (var b in cd)
                {
                    int ID = b.IDCD;
                    var suacd = db.ChiDinhs.Single(p => p.IDCD == ID);
                    suacd.KetLuan = b.KetLuan;
                    kl = b.KetLuan;
                    suacd.LoiDan = b.LoiDan;
                    suacd.GhiChu = b.GhiChu;
                    // suacd.SoPhieu = b.SoPhieu;
                    suacd.NgoaiGioHC = Convert.ToInt32(FormThamSo.frm_UpdateLetet.CheckNGioHC(lupNgayTH.DateTime));
                    suacd.Status = 1;
                    suacd.TamThu = b.TamThu;
                    suacd.NgayTH = lupNgayTH.DateTime;
                    if (lupMaMay.EditValue != null)
                        suacd.MaMay = lupMaMay.EditValue.ToString();
                    if (LupCanBo.EditValue != null)
                        suacd.MaCBth = LupCanBo.EditValue.ToString();
                    db.SaveChanges();
                }
                foreach (var c in clsct)
                {
                    var suaclsct = db.CLScts.Single(p => p.Id == c.Id);
                    suaclsct.DuongDan = c.DuongDan;
                    suaclsct.DuongDan2 = c.DuongDan2;
                    suaclsct.KetQua = c.KetQua;
                    //suaclsct.MaCB = c.MaCB;
                    //suaclsct.Ngaythang = c.Ngaythang;
                    suaclsct.SoPhieu = c.SoPhieu;
                    if ((!String.IsNullOrEmpty(c.KetQua) && c.KetQua.Length > 0) || !string.IsNullOrEmpty(kl))
                    {
                        suaclsct.Status = 1;
                    }
                    else
                    {
                        suaclsct.Status = c.Status;
                    }
                    suaclsct.STTHT = c.STTHT;
                    db.SaveChanges();
                }
                int makp = 0;
                foreach (var a in cls)
                {
                    var suacls = db.CLS.Single(p => p.IdCLS == a.IdCLS);
                    makp = a.MaKP == null ? 0 : a.MaKP.Value;
                    suacls.MaCBth = a.MaCBth;
                    suacls.NgayTH = lupNgayTH.DateTime;
                    var ktstatuscd = db.ChiDinhs.Where(p => p.IdCLS == a.IdCLS).Where(p => p.Status == 0 || p.Status == null).ToList();
                    if (ktstatuscd.Count > 0)
                        suacls.Status = 0;
                    else
                    {
                        suacls.Status = 1;
                        BenhNhan sua = db.BenhNhans.Where(p => p.MaBNhan == mabn).FirstOrDefault();
                        if (sua != null)
                        {
                            var b = db.BNKBs.Where(p => p.MaBNhan == mabn).ToList();
                            var vienphi = db.VienPhis.Where(p => p.MaBNhan == mabn).ToList();
                            if (b.Count > 0 && vienphi.Count <= 0)
                            {
                                sua.Status = 5;
                            }
                        }
                    }
                    db.SaveChanges();
                }
                //int IdCLS = Convert.ToInt32(grvketqua.GetFocusedRowCellValue("id").ToString());
                var cdinh = (from cd1 in db.ChiDinhs.Where(p => p.IDCD == _idcd && p.Status == 1)
                             join dv in db.DichVus on cd1.MaDV equals dv.MaDV
                             select new { cd1.MaDV, cd1.SoPhieu, cd1.DonGia, cd1.IDCD, dv.DonVi, cd1.TrongBH, cd1.XHH, cd1.LoaiDV }).ToList();
                int iddthuoc = 0;
                //string _mabn = grvBenhnhan.GetFocusedRowCellValue("MaBNhan").ToString();
                int _idkb = 0;
                var bnkb = db.BNKBs.Where(p => p.MaBNhan == mabn && p.MaKP == makp).OrderByDescending(p => p.IDKB).ToList();
                if (bnkb.Count > 0)
                    _idkb = bnkb.First().IDKB;// so sánh ngày khám bệnh và ngày thực hiện, để lấy đúng IDKB
                var ktdthuoc = db.DThuocs.Where(p => p.MaBNhan == mabn).Where(p => p.PLDV == 2).ToList();
                if (ktdthuoc.Count > 0)
                    iddthuoc = ktdthuoc.First().IDDon;
                if (iddthuoc > 0)
                {
                    foreach (var cd2 in cdinh)
                    {
                        var kt = (from dt in db.DThuoccts.Where(p => p.IDCD == cd2.IDCD) select dt).ToList();
                        if (kt.Count <= 0)
                        {
                            double _dongia = DungChung.Ham._getGiaDM(db, cd2.MaDV == null ? 0 : cd2.MaDV.Value, cd2.TrongBH == null ? 1 : cd2.TrongBH.Value, string.IsNullOrEmpty(txtmabn.Text) ? 0 : Convert.ToInt32(txtmabn.Text), lupNgayTH.DateTime);
                            DThuocct moi = new DThuocct();
                            moi.MaDV = cd2.MaDV;
                            moi.IDKB = _idkb;
                            moi.IDDon = iddthuoc;
                            moi.DonVi = cd2.DonVi;
                            moi.TrongBH = cd.First().TrongBH == null ? 0 : cd.First().TrongBH.Value;
                            moi.IDCD = cd2.IDCD;
                            moi.DonGia = cd2.DonGia == 0 ? _dongia : cd2.DonGia;
                            moi.XHH = cd2.XHH;
                            moi.LoaiDV = cd2.LoaiDV;
                            if (LupCanBo.EditValue != null)
                                moi.MaCB = LupCanBo.EditValue.ToString();
                            else
                                moi.MaCB = "";
                            moi.MaKP = makp;
                            moi.ThanhTien = cd2.DonGia == 0 ? _dongia : cd2.DonGia;
                            moi.NgayNhap = lupNgayTH.DateTime;
                            moi.SoLuong = 1;
                            moi.Status = 0;
                            if (cd2.SoPhieu != null && cd2.SoPhieu > 0)
                                moi.ThanhToan = 1;
                            moi.TyLeTT = 100;
                            moi.IDCLS = _idcls;
                            db.DThuoccts.Add(moi);
                            db.SaveChanges();
                            var CheckGiaPhuThu = db.DichVus.Where(p => p.MaDV == cd2.MaDV).FirstOrDefault();
                            var sss = db.BenhNhans.Where(p => p.MaBNhan == mabn).Where(p => p.DTuong == "BHYT").ToList();
                            if (CheckGiaPhuThu != null && CheckGiaPhuThu.GiaPhuThu > 0 && sss.Count > 0 && moi.TrongBH == 1)
                            {
                                double s = CheckGiaPhuThu.GiaPhuThu;
                                DungChung.Ham._InsertPhuThu(db, moi.IDDonct, s);
                            }
                        }
                        else
                        {
                            foreach (var dt in kt)
                            {
                                dt.NgayNhap = lupNgayTH.DateTime;
                                dt.IDCLS = _idcls;
                            }
                            db.SaveChanges();
                        }
                    }
                }
                else
                {
                    DThuoc dthuoccd = new DThuoc();
                    dthuoccd.NgayKe = lupNgayTH.DateTime; // cần phải lấy theo ngày tháng nhập kết quả CLS
                    dthuoccd.MaBNhan = mabn;
                    dthuoccd.MaKP = cls.First().MaKP;
                    dthuoccd.MaCB = cls.First().MaCB;
                    dthuoccd.PLDV = 2;
                    dthuoccd.KieuDon = -1;
                    db.DThuocs.Add(dthuoccd);
                    if (db.SaveChanges() >= 0)
                    {
                        int maxid = dthuoccd.IDDon;
                        foreach (var cd3 in cdinh)
                        {
                            double _dongia = DungChung.Ham._getGiaDM(db, cd3.MaDV == null ? 0 : cd3.MaDV.Value, cd3.TrongBH == null ? 1 : cd3.TrongBH.Value, string.IsNullOrEmpty(txtmabn.Text) ? 0 : Convert.ToInt32(txtmabn.Text), lupNgayTH.DateTime);
                            DThuocct moi = new DThuocct();
                            moi.MaDV = cd3.MaDV;
                            moi.IDDon = maxid;
                            moi.IDKB = _idkb;
                            moi.TrongBH = cd.First().TrongBH == null ? 0 : cd.First().TrongBH.Value;
                            if (LupCanBo.EditValue != null)
                                moi.MaCB = LupCanBo.EditValue.ToString();
                            else
                                moi.MaCB = "";
                            moi.NgayNhap = lupNgayTH.DateTime;
                            moi.MaKP = makp;
                            moi.IDCD = cd3.IDCD;
                            moi.DonVi = cd3.DonVi;
                            moi.XHH = cd3.XHH;
                            moi.DonGia = cd3.DonGia == 0 ? _dongia : cd3.DonGia;
                            moi.ThanhTien = cd3.DonGia == 0 ? _dongia : cd3.DonGia;
                            moi.SoLuong = 1;
                            moi.Status = 0;
                            moi.LoaiDV = cd3.LoaiDV;
                            if (cd3.SoPhieu != null && cd3.SoPhieu > 0)
                                moi.ThanhToan = 1;
                            moi.TyLeTT = 100;
                            moi.IDCLS = _idcls;
                            db.DThuoccts.Add(moi);
                            db.SaveChanges();
                            var CheckGiaPhuThu = db.DichVus.Where(p => p.MaDV == cd3.MaDV).FirstOrDefault();
                            var sss = db.BenhNhans.Where(p => p.MaBNhan == mabn).Where(p => p.DTuong == "BHYT").ToList();
                            if (CheckGiaPhuThu != null && CheckGiaPhuThu.GiaPhuThu > 0 && sss.Count > 0 && moi.TrongBH == 1)
                            {
                                double s = CheckGiaPhuThu.GiaPhuThu;
                                DungChung.Ham._InsertPhuThu(db, moi.IDDonct, s);
                            }
                        }
                    }
                }
                EnabledControl(true);
                //trangthaiLuu = 0;
                suaanhSA = false;
                _fileanh = "";
                _fileanh2 = "";

            }
        }
        private bool KT()
        {
            QLBV_Database.QLBVEntities db = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

            int ot;
            int _int_maBN = 0;

            if (Int32.TryParse(txtmabn.Text, out ot))
                _int_maBN = Convert.ToInt32(txtmabn.Text);
            if (lupNgayTH.DateTime != null)
            {
                var _NgayCD = db.CLS.Where(p => p.IdCLS == _idcls).Select(p => p.NgayThang).FirstOrDefault();
                DateTime _NgayTH = lupNgayTH.DateTime;
                if (_NgayCD != null)
                {
                    if (_NgayTH < _NgayCD)
                    {

                        MessageBox.Show("Ngày Thực hiện không được < ngày chỉ định", "Thông báo", MessageBoxButtons.OK);
                        lupNgayTH.Focus();
                        return false;
                    }
                    else
                    {
                        if (_NgayTH > DateTime.Now)
                        {

                            MessageBox.Show("Ngày Thực hiện không được > ngày hiện tại", "Thông báo", MessageBoxButtons.OK);
                            lupNgayTH.Focus();
                            return false;
                        }
                    }
                }
            }
            if (db.VienPhis.Where(p => p.MaBNhan == _int_maBN).Count() > 0)
            {
                MessageBox.Show("Bệnh Nhân đã thanh toán. Không thể lưu!.");
                return false;
            }
            return true;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtmabn.Text))
            {
                QLBV_Database.QLBVEntities db = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                var vp = (from vpct in db.VienPhis.Where(p => p.MaBNhan == mabn) select new { vpct.idVPhi }).ToList();
                var rv = db.RaViens.Where(p => p.MaBNhan == mabn).ToList();
                if (vp.Count > 0)
                { MessageBox.Show("Bệnh nhân đã thanh toán không thể xoá!"); }
                else if (rv.Count > 0)
                {
                    MessageBox.Show("Bệnh nhân đã ra viện không thể sửa!");
                }
                else
                {
                    DialogResult dia = MessageBox.Show("Bạn muốn xóa kết quả?", "Hỏi xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                    if (dia == DialogResult.Yes)
                    {
                        List<CL> cls = db.CLS.Where(p => p.IdCLS == _idcls).ToList();
                        List<ChiDinh> cd = db.ChiDinhs.Where(p => p.IdCLS == _idcls).ToList();
                        List<CLSct> clsct = db.CLScts.Where(p => p.IDCD == _idcd).ToList();
                        int _maCK = 0;
                        var ck = (from nhom in db.NhomDVs.Where(p => p.TenNhomCT.Contains("Khám bệnh"))
                                  join dvu in db.DichVus.Where(p => p.PLoai == 2).Where(p => p.Status == 1) on nhom.IDNhom equals dvu.IDNhom
                                  select new { dvu.DonGia, dvu.MaDV, dvu.DonVi, dvu.TrongDM }).OrderByDescending(p => p.DonGia).ToList();
                        if (ck.Count > 0)
                            _maCK = ck.First().MaDV;
                        foreach (var b in cd)
                        {
                            int ID = b.IDCD;
                            var iddt = db.DThuoccts.Where(p => p.IDCD == ID && p.MaDV != _maCK).ToList();
                            if (iddt.Count > 0)
                            {
                                foreach (var item in iddt)
                                {
                                    int iddtct = item.IDDonct;
                                    var ktchiphi = db.DThuoccts.Where(p => p.AttachIDDonct == iddtct).ToList();
                                    if (ktchiphi.Count > 0)
                                    {
                                        MessageBox.Show("dịch vụ đã có chi phí đính kèm, bạn không thế xóa");
                                        return;
                                    }
                                    var xoa = db.DThuoccts.Single(p => p.IDDonct == iddtct);
                                    db.DThuoccts.Remove(xoa);
                                    db.SaveChanges();
                                }
                            }

                            var suacd = db.ChiDinhs.Single(p => p.IDCD == ID);
                            suacd.NgayTH = null;
                            suacd.KetLuan = "";
                            suacd.LoiDan = "";
                            suacd.MoTa = "";
                            //suacd.SoPhieu = 0;
                            suacd.Status = 0;
                            //suacd.TamThu = 1;
                            db.SaveChanges();
                        }
                        foreach (var c in clsct)
                        {
                            var suaclsct = db.CLScts.Single(p => p.Id == c.Id);
                            suaclsct.DuongDan = "";
                            suaclsct.DuongDan2 = "";
                            suaclsct.KetQua = "";
                            //suaclsct.MaCB = "";
                            //suaclsct.Ngaythang = null;
                            suaclsct.SoPhieu = 0;
                            suaclsct.Status = 0;
                            suaclsct.STTHT = 0;
                            db.SaveChanges();
                        }
                        foreach (var a in cls)
                        {
                            var suacls = db.CLS.Single(p => p.IdCLS == a.IdCLS);
                            suacls.MaCBth = "";
                            suacls.Status = 0;
                            suacls.NgayTH = null;
                            db.SaveChanges();
                        }
                        MessageBox.Show("Xoá thành công!");
                        frm_KetQuaSieuAmThai35w_12345_Load(null, null);
                        ptSieuam.Image = null;

                    }
                }
            }
            else
            {
                MessageBox.Show("Không thể xóa! Bạn chưa chọn bệnh nhân.");
            }
        }
        //int trangthaiLuu=0;
        string[] arrDuongDan = new string[7];
        string strDD = "";
        private void EnabledControl(bool T)//status=1: true
        {
            btnLuu.Enabled = !T;
            btnSua.Enabled = T;
            btnXoa.Enabled = T;

            sbtChonanhSA1.Enabled = !T;

            sbtXoaanhSA1.Enabled = !T;

            lupNgayTH.Properties.ReadOnly = T;
            mmKLSieuam.Enabled = !T;
            mmLoidanSieuAm.Enabled = !T;
            LupCanBo.Properties.ReadOnly = T;
            lupMaMay.Properties.ReadOnly = T;
            this.grvketqua.OptionsBehavior.ReadOnly = T;
        }
        private void btnSua_Click(object sender, EventArgs e)
        {
            //trangthaiLuu = 1;
            TTLuu = 2;

            QLBV_Database.QLBVEntities db = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var vp = (from vpct in db.VienPhis.Where(p => p.MaBNhan == mabn) select new { vpct.idVPhi }).ToList();
            var rv = db.RaViens.Where(p => p.MaBNhan == mabn).ToList();
            if (vp.Count > 0)
            {
                MessageBox.Show("Bệnh nhân đã thanh toán không thể sửa!");
            }
            else
                if (rv.Count > 0)
                {
                    MessageBox.Show("Bệnh nhân đã ra viện không thể sửa!");
                }
                else
                {
                    EnabledControl(false);
                    mmKLSieuam.Properties.ReadOnly = false;
                    mmLoidanSieuAm.Properties.ReadOnly = false;
                }
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities db = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

            List<Ketqua> kq = new List<Ketqua>();
            List<BienKQ> _listBienKQ = new List<BienKQ>();
            var kqcls = (from cls in db.CLS.Where(p => p.IdCLS == _idcls)
                         join cd in db.ChiDinhs on cls.IdCLS equals cd.IdCLS
                         join clsct in db.CLScts on cd.IDCD equals clsct.IDCD
                         select new { cls.NgayTH, cd.MaCBth, cd.MaDV, cd.Status, cd.IDCD, clsct.KetQua, cls.MaBNhan, cd.KetLuan, cd.MaMay, clsct.DuongDan, cd.LoiDan, clsct.DuongDan2 }).ToList();
            if (kqcls.First().KetQua != null && kqcls.First().KetQua.Contains(";"))
            {
                #region Sieu am 01 thai

                string[] arrkq = kqcls.First().KetQua.ToString().Split(';');
                int stt = 1;
                kq.Clear();
                BienKQ _bienmoi = new BienKQ();
                int t = 0;
                foreach (var item in arrkq)
                {
                    if (item.Contains("Tim thai"))
                    {
                        _bienmoi.TimThai = item;
                    }

                    if (item.Contains("Ngôi thai"))
                    {
                        _bienmoi.NgoiThai = item;
                    }
                    if (item.Contains("Cử động thai"))
                    {
                        _bienmoi.CuDongThai = "•  " + item;
                    }
                    if (item.Contains("Số lượng thai"))
                    {
                        _bienmoi.Soluongthai = "•  " + item;
                    }
                    if (item.Contains("Trọng lượng thai ước tính"))
                    {
                        _bienmoi.TrongLuongThai = item;
                    }

                    if (item.Contains("FL"))
                    {
                        _bienmoi.ChieuDaiXuongDui_FL = item;
                    }

                    if (item.Contains("Chu vi vòng bụng"))
                    {
                        _bienmoi.ChuViBung_AC = item;
                    }
                    if (item.Contains("Chu vi đầu"))
                    {
                        _bienmoi.ChuViDau_HC = item;
                    }
                    if (item.Contains("Cột sống - hệ xương"))
                    {
                        _bienmoi.CotSongHeXuong = item;
                    }
                    if (item.Contains("Xương sọ"))
                    {
                        _bienmoi.XuongSo = item;
                    }
                    if (item.Contains("Tứ chi"))
                    {
                        _bienmoi.TuChi = item;
                    }
                    if (item.Contains("Hàm mặt"))
                    {
                        _bienmoi.HamMat = item;
                    }
                    if (item.Contains("Ngực"))
                    {
                        _bienmoi.Nguc = item;
                    }

                    if (item.Contains("Dự kiến sinh"))
                    {
                        _bienmoi.DuKienSinh = item;
                    }
                    if (item.Contains("ĐK chẩm trán"))
                    {
                        _bienmoi.DKChamTranDau_OFD = item;
                    }
                    if (item.Contains("BPD"))
                    {
                        _bienmoi.DKLuongDinh_BPD = item;
                    }
                    if (item.Contains("APTD"))
                    {
                        _bienmoi.DKTruocSauBung_APTD = item;
                    }
                    if (item.Contains("TTD"))
                    {
                        _bienmoi.DKNgangBung_TTD = item;
                    }
                    if (item.Contains("Các não thất"))
                    {
                        _bienmoi.CacNaoThat = item;
                    }
                    if (item.Contains("Tần số tim"))
                    {
                        _bienmoi.TanSoTim = item;
                    }

                    if (item.Contains("Tuổi thai (Theo siêu âm lúc 11-14 tuần)"))
                    {
                        _bienmoi.TuoiThai1 = item;
                    }
                    if (item.Contains("Tuổi thai dựa theo số đo hiện tại"))
                    {
                        _bienmoi.TuoiThai2 = item;
                    }

                    if (item.Contains("Rau thai"))
                    {
                        string[] ar = item.Split(':');
                        if (ar.Count() > 2)
                        {
                            t++;
                            string s = ar[1] + ":" + ar[2];
                            _bienmoi.RauThai += s + "\r\n";
                            if (t == 1)
                                _bienmoi.RauThai2 = ar[1] + ":" + ar[2];
                            if (t == 2)
                                _bienmoi.RauThai3 = ar[1] + ": " + ar[2];
                            if (t == 3)
                                _bienmoi.RauThai4 = ar[1] + ": " + ar[2];
                        }
                        else
                        {
                            string s = ar[1];
                            _bienmoi.RauThai += s + "\r\n";
                        }

                    }
                    if (item.Contains("Ổ bụng"))
                    {
                        string[] ar = item.Split(':');
                        if (ar.Count() >= 2)
                        {
                            t++;
                            if (t == 1)
                                _bienmoi.OBung1 = ar[0] + ":" + ar[1];
                            if (t == 2)
                                _bienmoi.OBung2 = "•  " + ar[1];
                            if (t == 3)
                                _bienmoi.OBung3 = "•  " + ar[1];
                            if (t == 4)
                                _bienmoi.OBung4 = "•  " + ar[1];
                            if (t == 5)
                                _bienmoi.OBung5 = "•  " + ar[1];
                            if (t == 6)
                                _bienmoi.OBung6 = "•  " + ar[1];
                        }
                    }
                    if (item.Contains("Nước ối"))
                    {
                        string[] ar = item.Split(':');

                        _bienmoi.NuocOi = item;
                    }

                    if (item.Contains("Doppler ĐM tử cung trái"))
                    {
                        _bienmoi.DopplerTrai = item;
                    }

                    if (item.Contains("Doppler ĐM tử cung phải"))
                    {
                        _bienmoi.DopplerPhai = item;
                    }
                    if (item.Contains("Doppler ĐM rốn"))
                    {
                        _bienmoi.DopplerRon = item;
                    }
                    if (item.Contains("Doppler DM não giữa"))
                    {
                        _bienmoi.DopplerNaoGiua = item;
                    }
                    if (item.Contains("Dây rốn"))
                    {
                        _bienmoi.DayRon = item;
                    }
                }
                _listBienKQ.Add(_bienmoi);
                #endregion

            }
            CLS.InPhieu.PhieuSieuAmMau35Tuan_12345(_idcd, _listBienKQ);

            //else
            //  CLS.InPhieu.PhieuSieuAmMau4D(_idcd, rgSoThai.SelectedIndex);
        }
    }
}