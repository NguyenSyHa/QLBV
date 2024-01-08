using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using QLBV.BaoCao;

namespace QLBV.FormThamSo
{
    public partial class frm_BCTaiNanGiaoThong_CapCuu : DevExpress.XtraEditors.XtraForm
    {
        public frm_BCTaiNanGiaoThong_CapCuu()
        {
            InitializeComponent();
        }

        private void btnInBC_Click(object sender, EventArgs e)
        {

            DateTime tungay = DungChung.Ham.NgayTu(lupTuNgay.DateTime);
            DateTime denngay = DungChung.Ham.NgayDen(lupDenNgay.DateTime);
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            List<RaVien> rv = data.RaViens.ToList();
            frmIn frm = new frmIn();

            int ckNgay = 0;// bệnh nhân chuyển đến tìm theo ngày : 0: Cả hai, 1: ngày vào viện; 2: ngày ra viện
            if (ckNgayVaoVien.Checked && ckNgayRaVien.Checked)
                ckNgay = 0;
            else if (ckNgayVaoVien.Checked)
                ckNgay = 1;
            else if (ckNgayRaVien.Checked)
                ckNgay = 2;
            else
                ckNgay = -1;

            List<int> _listBN = new List<int>();
            //            List<int> _listAll = new List<int>();
            List<int> _listBNVao = new List<int>();
            List<int> _listBNRa = new List<int>();
            //--------------------------------------
            //danh sách bệnh nhân trong bản bệnh nhân
            // List<BenhNhan> _listAllBenhNhan = data.BenhNhans.ToList();
            List<BenhNhan> _listAllCapCuu = data.BenhNhans.Where(p => p.CapCuu == 1).ToList();

            List<BenhNhan> _listAll = new List<BenhNhan>();//List Bệnh nhân cấp cứu
            List<BenhNhan> _listAll1 = new List<BenhNhan>();//bao gồm cả bệnh nhân không cấp cứu
           
            // List<BenhNhan> _listBenhNhan = new List<BenhNhan>();// danh sách tất cả bệnh nhân đã tìm theo ngày
            List<BenhNhan> _listBenhNhanchuyenDen = new List<BenhNhan>();// danh sách bệnh nhân chuyển đến đã tìm theo ngày

            List<BenhNhan> _listBenhNhanCTSoNao = new List<BenhNhan>();
            //--------------------------------------
            _listBNVao = data.BenhNhans.Where(p => p.NNhap >= tungay && p.NNhap <= denngay).Select(p => p.MaBNhan).ToList();// không cần so sánh với MaBV trong bảng BenhNhan != null && "" (ở trên đã có)
            _listBNRa = data.RaViens.Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay).Select(p => p.MaBNhan).ToList();
            // _listBN = _listBNVao;
            _listBN.AddRange(_listBNVao);
            _listBN.AddRange(_listBNRa);
            _listBN = _listBN.Distinct().ToList();

            if (ckNgay == 0)
            {                
                _listAll = _listAllCapCuu.Where(p => _listBN.Contains(p.MaBNhan)).ToList();
                _listAll1 = data.BenhNhans.Where(p => _listBN.Contains(p.MaBNhan)).ToList();
            }
            else if (ckNgay == 1)
            {
                _listAll = _listAllCapCuu.Where(p => _listBNVao.Contains(p.MaBNhan)).ToList();  //(from a in _listBNVao join b in _listAllchuyenDen on a equals b.MaBNhan select b).Distinct().ToList();
                _listAll1 = data.BenhNhans.Where(p => _listBNVao.Contains(p.MaBNhan)).ToList();
            }
            else if (ckNgay == 2)
            {
                _listAll = _listAllCapCuu.Where(p => _listBNRa.Contains(p.MaBNhan)).ToList();  //(from a in _listBNRa join b in _listAllchuyenDen on a equals b.MaBNhan select b).Distinct().ToList();
                _listAll1 = data.BenhNhans.Where(p => _listBNRa.Contains(p.MaBNhan)).ToList();
            }
            _listBenhNhanchuyenDen = _listAll1.Where(p => p.ChuyenKhoa == "Tai nạn giao thông" || p.ChuyenKhoa == "Đường bộ" || p.ChuyenKhoa == "Đường sắt").ToList();

            _listBenhNhanCTSoNao = (from a in _listBenhNhanchuyenDen
                                    join b in data.RaViens.Where(p => p.MaICD.ToLower().Contains("s06")) on a.MaBNhan equals b.MaBNhan
                                    select a).ToList();
            #region getData
            clsTaiNanGT moi1 = new clsTaiNanGT();
            moi1.stt = 1;
            moi1.Nam = _listAll.Where(p => p.GTinh == 1).Count();
            moi1.Nu = _listAll.Where(p => p.GTinh == 0).Count();
            moi1.SL4 = _listAll.Where(p => p.Tuoi == null || p.Tuoi <= 4).Count();
            moi1.SL14 = _listAll.Where(p => p.Tuoi >= 5 && p.Tuoi <= 14).Count();
            moi1.SL19 = _listAll.Where(p => p.Tuoi >= 15 && p.Tuoi <= 19).Count();
            moi1.SL59 = _listAll.Where(p => p.Tuoi >= 20 && p.Tuoi <= 59).Count();
            moi1.SL60 = _listAll.Where(p => p.Tuoi >= 60).Count();
            clsTaiNanGT moi2 = new clsTaiNanGT();
            moi2.stt = 2;
            moi2.Nam = _listBenhNhanchuyenDen.Where(p => p.GTinh == 1).Count();
            moi2.Nu = _listBenhNhanchuyenDen.Where(p => p.GTinh == 0).Count();
            moi2.SL4 = _listBenhNhanchuyenDen.Where(p => p.Tuoi == null || p.Tuoi <= 4).Count();
            moi2.SL14 = _listBenhNhanchuyenDen.Where(p => p.Tuoi >= 5 && p.Tuoi <= 14).Count();
            moi2.SL19 = _listBenhNhanchuyenDen.Where(p => p.Tuoi >= 15 && p.Tuoi <= 19).Count();
            moi2.SL59 = _listBenhNhanchuyenDen.Where(p => p.Tuoi >= 20 && p.Tuoi <= 59).Count();
            moi2.SL60 = _listBenhNhanchuyenDen.Where(p => p.Tuoi >= 60).Count();
            clsTaiNanGT moi3 = new clsTaiNanGT();
            _listAll1 = (from a in  _listBenhNhanchuyenDen  join b in rv.Where(p=>p.KetQua == "Tử vong") on a.MaBNhan equals b.MaBNhan select a ).ToList();
            moi3.stt = 3;
            moi3.Nam = _listAll1.Where(p => p.GTinh == 1).Count();
            moi3.Nu = _listAll1.Where(p => p.GTinh == 0).Count();
            moi3.SL4 = _listAll1.Where(p => p.Tuoi == null || p.Tuoi <= 4).Count();
            moi3.SL14 = _listAll1.Where(p => p.Tuoi >= 5 && p.Tuoi <= 14).Count();
            moi3.SL19 = _listAll1.Where(p => p.Tuoi >= 15 && p.Tuoi <= 19).Count();
            moi3.SL59 = _listAll1.Where(p => p.Tuoi >= 20 && p.Tuoi <= 59).Count();
            moi3.SL60 = _listAll1.Where(p => p.Tuoi >= 60).Count();
            clsTaiNanGT moi4 = new clsTaiNanGT();
            _listAll1 = (from a in _listBenhNhanchuyenDen join b in rv.Where(p => p.Status == 1) on a.MaBNhan equals b.MaBNhan select a).ToList();
            moi4.stt = 3;
            moi4.Nam = _listAll1.Where(p => p.GTinh == 1).Count();
            moi4.Nu = _listAll1.Where(p => p.GTinh == 0).Count();
            moi4.SL4 = _listAll1.Where(p => p.Tuoi == null || p.Tuoi <= 4).Count();
            moi4.SL14 = _listAll1.Where(p => p.Tuoi >= 5 && p.Tuoi <= 14).Count();
            moi4.SL19 = _listAll1.Where(p => p.Tuoi >= 15 && p.Tuoi <= 19).Count();
            moi4.SL59 = _listAll1.Where(p => p.Tuoi >= 20 && p.Tuoi <= 59).Count();
            moi4.SL60 = _listAll1.Where(p => p.Tuoi >= 60).Count();
            clsTaiNanGT moi5 = new clsTaiNanGT();
            moi5.stt = 5;
            moi5.Nam = _listBenhNhanCTSoNao.Where(p => p.GTinh == 1).Count();
            moi5.Nu = _listBenhNhanCTSoNao.Where(p => p.GTinh == 0).Count();
            moi5.SL4 = _listBenhNhanCTSoNao.Where(p => p.Tuoi == null || p.Tuoi <= 4).Count();
            moi5.SL14 = _listBenhNhanCTSoNao.Where(p => p.Tuoi >= 5 && p.Tuoi <= 14).Count();
            moi5.SL19 = _listBenhNhanCTSoNao.Where(p => p.Tuoi >= 15 && p.Tuoi <= 19).Count();
            moi5.SL59 = _listBenhNhanCTSoNao.Where(p => p.Tuoi >= 20 && p.Tuoi <= 59).Count();
            moi5.SL60 = _listBenhNhanCTSoNao.Where(p => p.Tuoi >= 60).Count();
            #endregion

            List<clsTaiNanGT> _l = new List<clsTaiNanGT>();
            _l.Add(moi1);
            _l.Add(moi2);
            _l.Add(moi3);
            _l.Add(moi4);
            _l.Add(moi5);
            rep_BCTaiNanGiaoThong_CapCuu rep = new rep_BCTaiNanGiaoThong_CapCuu(_l);
            if (txtTieudeQuy.Text != "")
                rep.lblBaocaoQuy.Text = txtTieudeQuy.Text;    
            string thoigian = "Từ ngày " + lupTuNgay.DateTime.ToShortDateString() + " đến ngày " + lupDenNgay.DateTime.ToShortDateString();
            rep.lab_thoigian.Text = thoigian;

                   
            if(DungChung.Bien.MaBV == "24009")
            { rep.lab_kinhgui.Text = "Kính gửi: - Sở Y tế Bắc Giang"; }
            rep.CreateDocument();
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();
           
        }
        public class clsTaiNanGT
        {
            public int stt { set; get; }
            public int Nam { set; get; }
            public int Nu { set; get; }
            public int SL4 { set; get; }
            public int SL14 { set; get; }
            public int SL19 { set; get; }
            public int SL59 { set; get; }
            public int SL60 { set; get; }
        }

        private void frm_BCTaiNanGiaoThong_CapCuu_Load(object sender, EventArgs e)
        {
            lupTuNgay.DateTime = DateTime.Now;
            lupDenNgay.DateTime = DateTime.Now;
            ckNgayRaVien.Checked = true;
            ckNgayVaoVien.Checked = true;
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}