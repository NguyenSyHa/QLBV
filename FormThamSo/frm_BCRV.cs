using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using QLBV.DungChung;
using QLBV.FormThamSo;

namespace QLBV.FormThamSo
{
    public partial class frm_BCRV : DevExpress.XtraEditors.XtraForm
    {
        public frm_BCRV()
        {
            InitializeComponent();
        }
        List<KPhong> lkp = new List<KPhong>();
        //QLBV_Database.QLBVEntities data;
        private void btnTAORV_Click(object sender, EventArgs e)
        {

            QLBV_Database.QLBVEntities db = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            DateTime _tungay = QLBV.DungChung.Ham.NgayTu(detungay.DateTime);
            DateTime _denngay = QLBV.DungChung.Ham.NgayDen(dedenngay.DateTime);
            List<int> _lkp = new List<int>();
            for (int i = 0; i < cklKhoaPhong.ItemCount; i++)
            {
                if (cklKhoaPhong.GetItemChecked(i))
                    _lkp.Add(Convert.ToInt32(cklKhoaPhong.GetItemValue(i)));
            }
            var _ldv = db.DichVus.ToList();
            var _kq1 = (from a in db.RaViens.Where(p => p.NgayRa >= _tungay && p.NgayRa <= _denngay)
                        join bn in db.BenhNhans.Where(p => p.DTuong == "BHYT") on a.MaBNhan equals bn.MaBNhan
                        join vp in db.VienPhis on a.MaBNhan equals vp.MaBNhan
                        join vpct in db.VienPhicts on vp.idVPhi equals vpct.idVPhi
                        join kp in db.KPhongs on a.MaKP equals kp.MaKP
                        select new
                        {
                            a.MaBNhan,
                            MaKP = kp.PLoai == "Phòng khám" ? 1000 : kp.MaKP,
                            TenKP = kp.PLoai == "Phòng khám" ? "Ngoại trú" : kp.TenKP,
                            vpct.idVPhict,
                            vpct.MaDV,
                            vpct.ThanhTien,
                            vpct.TienBH,
                            vpct.TienBN
                        }).ToList();
            //var test = _kq1.Where(p => p.MaKP == 25).Select(p=>p.MaBNhan).Distinct().ToList();
            var _ketqua = (from a in _kq1
                           join dv in _ldv on a.MaDV equals dv.MaDV
                           join kp in _lkp on a.MaKP equals kp
                          group new { a, dv } by new { a.MaKP, a.TenKP } into kq
                           select new
                           {
                               kq.Key.MaKP,
                               kq.Key.TenKP,
                               SoBN = kq.Select(p => p.a.MaBNhan).Distinct().Count(),
                               ThuocThuongTong = kq.Where(p => p.dv.PLoai == 1).Where(p => p.dv.DongY == 0).Sum(p => p.a.ThanhTien),
                               DongYTong = kq.Where(p => p.dv.PLoai == 1).Where(p => p.dv.DongY == 1).Sum(p => p.a.ThanhTien),
                               Tong = kq.Where(p => p.dv.PLoai == 1).Sum(p => p.a.ThanhTien),
                               ThuocThuongBH = kq.Where(p => p.dv.PLoai == 1).Sum(p => p.a.TienBH),
                               ThuocThuongBN = kq.Where(p => p.dv.PLoai == 1).Sum(p => p.a.TienBN),
                           }).ToList();
          
            frmIn frm = new frmIn();
            BaoCao.REP_BCRAVIENBNBHYT rep = new BaoCao.REP_BCRAVIENBNBHYT();
            rep.TCQ.Value = DungChung.Bien.TenCQ.ToUpper();
            rep.CQCQ.Value = DungChung.Bien.TenCQCQ.ToUpper();
            rep.NT.Value = "Từ ngày " + _tungay.ToShortDateString() + " đến ngày " + _denngay.ToShortDateString();
            rep.NK.Value = "Ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;
            rep.DataSource = _ketqua.OrderBy(p=>p.MaKP);
            rep.SonOC();
            rep.CreateDocument();
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();
        }

        private void frm_BCRV_Load(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities db = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            lkp = db.KPhongs.Where(p => p.PLoai == "Lâm sàng").OrderBy(p => p.TenKP).ToList();
            lkp.Insert(0, new KPhong { MaKP = 1000, TenKP = "Ngoại trú" });
            lkp.Insert(0, new KPhong { MaKP = 0, TenKP = "Tất cả", PLoai = "Tất cả" });
            cklKhoaPhong.DataSource = lkp;
            cklKhoaPhong.CheckAll();
            detungay.DateTime = DateTime.Now;
            dedenngay.DateTime = DateTime.Now;
        }

        private void btnhuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cklKhoaPhong_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            if (e.Index == 0)
            {
                if (cklKhoaPhong.GetItemChecked(0) == true)
                    cklKhoaPhong.CheckAll();
                else
                    cklKhoaPhong.UnCheckAll();
            }
        }

    }
}