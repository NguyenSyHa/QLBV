using System;
using QLBV_Database;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace QLBV.FormThamSo
{
    public partial class frm_DSBNThucHienTT_PT : DevExpress.XtraEditors.XtraForm
    {
        public frm_DSBNThucHienTT_PT()
        {
            InitializeComponent();

        }
        List<KP> _lkp = new List<KP>(); 
        private void frm_DSBNThucHienTT_PT_Load(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            _lkp.Clear();
            dateTuNgay.DateTime = System.DateTime.Now;
            dateDenNgay.DateTime = System.DateTime.Now;
            var kdnx = (from khoa in data.KPhongs.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.PhongKham || p.PLoai == DungChung.Bien.st_PhanLoaiKP.LamSang || p.PLoai == DungChung.Bien.st_PhanLoaiKP.HanhChinh) select new { khoa.TenKP, khoa.MaKP }).OrderBy(p => p.TenKP).ToList();
            if (kdnx.Count() > 0)
            {
                KP themmoi1 = new KP();
                themmoi1.TenKP = "Chọn tất cả";
                themmoi1.MaKP = 0;
                themmoi1.Check = true;
                _lkp.Add(themmoi1);
                foreach (var a in kdnx)
                { 
                    KP themmoi = new KP();
                    themmoi.TenKP = a.TenKP;
                    themmoi.MaKP = a.MaKP;
                    themmoi.Check = true;
                    _lkp.Add(themmoi);
                }
                grcKPhong.DataSource = _lkp.ToList();
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnInBC_Click(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            DateTime tungay = DungChung.Ham.NgayTu(dateTuNgay.DateTime);
            DateTime denngay = DungChung.Ham.NgayDen(dateDenNgay.DateTime); 
            List<int> kp = new List<int>();
            if (tungay > denngay)
            {
                MessageBox.Show("Ngày từ phải nhỏ hơn ngày đến", "Thông báo");
                return;
            }
            var ListCB = (from cb in data.CanBoes select new { cb.MaCB, cb.TenCB }).ToList();
            kp = _lkp.Where(p => p.MaKP > 0 && p.Check == true).Select(p => p.MaKP).ToList();
            var tenkp = _lkp.Where(p => p.MaKP > 0 && p.Check == true).Select(p => p.TenKP).ToList();
            string name = tenkp.First();
            string TenRG = "";
            string MaIn = "";
            if (tenkp.Count > 1)
            {
                for (int i = 1; i < tenkp.Count; i++)
                {
                    name += ", " + tenkp[i] ;
                }
            }
            Dictionary<string, object> _dic = new Dictionary<string, object>();
            _dic.Add("TenCQCQ", DungChung.Bien.TenCQCQ);
            _dic.Add("TenCQ", DungChung.Bien.TenCQ);
            _dic.Add("TenKP", name);
            string ngaythang = "Từ ngày " + tungay.Day.ToString() + " tháng " + tungay.Month.ToString() + " năm " + tungay.Year.ToString() + ", đến ngày " + denngay.Day.ToString() + " tháng " + denngay.Month.ToString() + " năm " + denngay.Year.ToString();
            _dic.Add("Ngaythang", ngaythang);
            string TenBaoCao = "";
            if ((DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049") && radChon.SelectedIndex == 0)
            {
                TenBaoCao = "DANH SÁCH BỆNH NHÂN LÀM THỦ THUẬT";
            }
            else
            {
                TenBaoCao = "DANH SÁCH BỆNH NHÂN LÀM PHẪU THUẬT";
            }
            _dic.Add("TenBaoCao", TenBaoCao);
            if (radChon.SelectedIndex == 0) // thủ thuật
            {
                TenRG = "Thủ thuật";
                MaIn = DungChung.PrintConfig.Rep_DSBNThuThuat;
            }
            else // phẫu thuật
            {
                TenRG = "Phẫu thuật";
                MaIn = DungChung.PrintConfig.Rep_DSBNPhauThuat;
            }
            List<DSBaoCao> report = new List<DSBaoCao>();
            var q = (from cls in data.CLS
                     join cd in data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                     join dv in data.DichVus on cd.MaDV equals dv.MaDV
                     join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                     join bn in data.BenhNhans on cls.MaBNhan equals bn.MaBNhan
                     where (cls.NgayTH > tungay && cls.NgayTH < denngay && tn.TenRG == TenRG && kp.Contains(cls.MaKP ?? 0))
                     
                     select new DSBaoCao
                     {
                         MaBNhan = bn.MaBNhan,
                         TenBNhan = bn.TenBNhan,
                         Tuoi = bn.Tuoi,
                         ChanDoan = cls.ChanDoan,
                         TenDV = dv.TenDV,
                         NgayTH = cls.NgayTH,
                         MaCBth = cls.MaCBth,
                         DSCBTH = cls.DSCBTH,
                         DungCu = cd.TenDungCu,
                         Loai = dv.Loai,
                         DonGia = cd.DonGia
                     }).OrderBy(p => p.NgayTH).ToList();
            foreach (var item in q)
            {
                string CBChinh = ListCB.Where(p => p.MaCB == item.MaCBth).Select(p => p.TenCB).FirstOrDefault().ToString();
                string PTV1 = "";
                string PTV2 = "";
                string PTV3 = "";
                string GMChinh = "";
                string GMPhu1 = "";
                string GMPhu2 = "";
                string GMPhu3 = "";
                string GiupViec = "";
                string[] dscb = new string[10];
                dscb = item.DSCBTH != null ? item.DSCBTH.Split(';'): new string[10]; // trường dscbth gồm họ tên cb th
                PTV1 =  dscb[0];
                GMChinh = dscb[1];
                GMPhu1 = dscb[2];
                GiupViec = dscb[3];
                PTV2 = dscb[4];
                PTV3 = dscb[5];
                GMPhu2 = dscb[6];
                GMPhu3 = dscb[7];

                DSBaoCao bc = new DSBaoCao();
                bc.MaBNhan = item.MaBNhan;
                bc.TenBNhan = item.TenBNhan;
                bc.Tuoi = item.Tuoi;
                bc.ChanDoan = item.ChanDoan;
                bc.TenDV = item.TenDV;
                bc.NgayTHStr = DungChung.Ham.NgaySangChu(Convert.ToDateTime(item.NgayTH), 7);
                bc.CanBoTH = CBChinh;
                bc.CanBoTTPhu = PTV1 + "\n" + PTV2 + "\n" + PTV3;
                bc.CanBoPhu1 = PTV1;
                bc.CanBoPhu2 = PTV2;
                bc.CanBoGMChinh = GMChinh;
                bc.CanBoGMPhu = GMPhu1 + "\n" + GMPhu2 + "\n" + GMPhu3;
                bc.CanBoGiupViec = GiupViec;
                bc.DungCu = item.DungCu;
                bc.ChayNgoai = "";
                bc.Loai = item.Loai;
                bc.DonGia = item.DonGia;
                report.Add(bc);
            }

            DungChung.Ham.Print(MaIn, report, _dic, false);
        }

        private void radChon_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (radChon.SelectedIndex == 0) // in thủ thuật
            {

            }
            else // in phẫu thuật
            {

            }
        }

        private void dateTuNgay_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void dateDenNgay_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void grvKPhong_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "colChon")
            {
                if (grvKPhong.GetFocusedRowCellValue("TenKP") != null)
                {
                    string Ten = grvKPhong.GetFocusedRowCellValue("TenKP").ToString();

                    if (Ten == "Chọn tất cả")
                    {
                        if (_lkp.First().Check == true)
                        {
                            foreach (var a in _lkp)
                            {
                                a.Check = false;
                            }
                        }
                        else
                        {
                            foreach (var a in _lkp)
                            {
                                a.Check = true;
                            }
                        }
                        grcKPhong.DataSource = "";
                        grcKPhong.DataSource = _lkp.ToList();
                    }
                }
            }
        }
    }
    public class KP
    {
        public bool Check { get; set; }
        public int MaKP { get; set; }
        public string TenKP { get; set; }
    }
    
    public class DSBaoCao
    {
        public int MaBNhan { get; set; }
        public string TenBNhan { get; set; }
        public int? Tuoi { get; set; }
        public string ChanDoan { get; set; }
        public string TenDV { get; set; }
        public DateTime? NgayTH { get; set; }
        public string NgayTHStr { get; set; }
        public string MaCBth { get; set; }
        public string DSCBTH { get; set; }
        public string CanBoTH { get; set; }
        public string CanBoTTPhu { get; set; }
        public string CanBoPhu1 { get; set; }
        public string CanBoPhu2 { get; set; }
        public string CanBoGMChinh { get; set; }
        public string CanBoGMPhu { get; set; }
        public string DungCu { get; set; }
        public string ChayNgoai { get; set; }
        public string CanBoGiupViec { get; set; }
        public int? Loai { get; set; }
        public double DonGia { get; set; }
    }
}