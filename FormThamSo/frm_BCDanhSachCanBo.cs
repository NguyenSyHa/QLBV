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
    public partial class frm_BCDanhSachCanBo : DevExpress.XtraEditors.XtraForm
    {
        public frm_BCDanhSachCanBo()
        {
            InitializeComponent();
        }
        private class KPhongc
        {
            private string TenKP;
            private int MaKP;
            private bool Chon;
            public string tenkp
            { set { TenKP = value; } get { return TenKP; } }
            public int makp
            { set { MaKP = value; } get { return MaKP; } }
            public bool chon
            { set { Chon = value; } get { return Chon; } }
            public int stt { get; set; }
        }
        QLBV_Database.QLBVEntities _data;
        List<KPhong> _lkpall = new List<KPhong>();
        List<CanBo> _lcb = new List<CanBo>();
        List<KPhongc> _Kphong = new List<KPhongc>();
        private void frm_BCDanhSachCanBo_Load(object sender, EventArgs e)
        {
            _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            _lkpall = _data.KPhongs.Where(p => p.Status == 1).ToList();
            _lcb = _data.CanBoes.Where(p => p.Status == 1).ToList();
            var kphong = (from kp in _lkpall
                          where (kp.Status == 1)
                          select new { kp.TenKP, kp.MaKP, kp.STTHT }).ToList();
            if (kphong.Count > 0)
            {
                KPhongc themmoi1 = new KPhongc();
                themmoi1.tenkp = "Chọn tất cả";
                themmoi1.makp = 0;
                themmoi1.chon = true;
                themmoi1.stt = 0;
                _Kphong.Add(themmoi1);
                foreach (var a in kphong)
                {
                    KPhongc themmoi = new KPhongc();
                    themmoi.tenkp = a.TenKP;
                    themmoi.makp = a.MaKP;
                    themmoi.chon = true;
                    themmoi.stt = a.STTHT ?? 0;
                    _Kphong.Add(themmoi);
                }
                grcKhoaphong.DataSource = _Kphong.ToList();
            }
        }

        private void grvKhoaphong_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "Chọn")
            {
                if (grvKhoaphong.GetFocusedRowCellValue("tenkp") != null)
                {
                    string Ten = grvKhoaphong.GetFocusedRowCellValue("tenkp").ToString();

                    if (Ten == "Chọn tất cả")
                    {
                        if (_Kphong.First().chon == true)
                        {
                            foreach (var a in _Kphong)
                            {
                                a.chon = false;
                            }
                        }
                        else
                        {
                            foreach (var a in _Kphong)
                            {
                                a.chon = true;
                            }
                        }
                        grcKhoaphong.DataSource = "";
                        grcKhoaphong.DataSource = _Kphong.ToList();
                    }
                }
            }
        }

        private void btnInBC_Click(object sender, EventArgs e)
        {
            bool TaoBC = true;
            List<KPhongc> _lKhoaP = new List<KPhongc>();
            _lKhoaP = _Kphong.Where(p => p.chon == true).Where(p => p.makp > 0).ToList();
            if (_lKhoaP.Count() == 0)
            {
                TaoBC = false;
                MessageBox.Show("Chưa chọn Khoa|Phòng");
            }
            if(TaoBC)
            {
                var kq1 = (from cb in _lcb
                           join kp in _lKhoaP on cb.MaKPBC equals kp.makp
                           select new
                           {
                               cb.TenCB,
                               GTinh = cb.GioiTinh == 1 ? "Nam" : "Nữ",
                               NgaySinh = cb.NamSinh,//(cb.NgaySinh != null && cb.NamSinh != null && cb.ThangSinh != null) ? (cb.NgaySinh + "/" + cb.ThangSinh + "/" + cb.NamSinh) : (cb.NamSinh != null ? cb.NamSinh.ToString() : ""),
                               cb.BangCap,
                               cb.MaCCHN,
                               cb.NgayCapCCHN,
                               cb.HSLuong,
                               cb.NgayTangLuong,
                               cb.MaNgach,
                               cb.TinHoc,
                               cb.NgoaiNgu,
                               kp.makp,
                               cb.ChucVu,
                               tenkp = kp.tenkp.ToUpper(),
                               SL = 1,
                               BienChe = cb.BienChe == 0 ? "Hợp đồng" : "Biên chế",
                               cb.STTHT,
                               kp.stt
                           }).OrderBy(p => p.stt).ThenBy(p => p.STTHT).ToList();
                if (kq1.Count() > 0)
                {
                    string dskp = "";
                    if (_lKhoaP.Count == 1)
                    {
                        dskp = _lKhoaP.First().tenkp;
                    }
                    else
                    {
                        foreach (var item in _lKhoaP)
                        {
                            dskp += item.tenkp + "\n";
                        }
                    }
                    BaoCao.Rep_BCDanhSachCanBo rep = new BaoCao.Rep_BCDanhSachCanBo();
                    frmIn frm = new frmIn();
                    //rep.lblThoiGian.Text = dskp;
                    rep.DataSource = kq1;
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
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}