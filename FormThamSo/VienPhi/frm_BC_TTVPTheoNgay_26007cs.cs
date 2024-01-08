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
    public partial class frm_BC_TTVPTheoNgay_26007cs : DevExpress.XtraEditors.XtraForm
    {
        QLBV_Database.QLBVEntities db = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        List<CanBo> _lCanBo = new List<CanBo>();

        public frm_BC_TTVPTheoNgay_26007cs()
        {
            InitializeComponent();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frm_BC_TTVPTheoNgay_26007cs_Load(object sender, EventArgs e)
        {
            var qcb = (from tcb in db.CanBoes
                       select new { MaCB = tcb.MaCB, TenCB = tcb.TenCB }).ToList();
            foreach (var a in qcb)
            {
                CanBo cb0 = new CanBo();
                cb0.MaCB = a.MaCB;
                cb0.TenCB = a.TenCB;
                _lCanBo.Add(cb0);
            }
            _lCanBo = _lCanBo.OrderBy(p => p.TenCB).ToList();
            CanBo cb = new CanBo();
            cb.TenCB = "Tất cả";
            cb.MaCB = "0";
            _lCanBo.Insert(0, cb);
            lupTenCB.Properties.DataSource = _lCanBo;
            luptungay.DateTime = System.DateTime.Now;
            lupdenngay.DateTime = System.DateTime.Now;

        }

        private class BN_TTTU
        {
            private double thanhtien;
            private string tenBN, namsinh, diachi, quyenhd, tenDV, ngaythu;
            private int sohd;
            public string TenBNhan
            { set { tenBN = value; } get { return tenBN; } }
            public string NamSinh
            { set { namsinh = value; } get { return namsinh; } }
            public string DChi
            { set { diachi = value; } get { return diachi; } }
            public int SoHD
            { set { sohd = value; } get { return sohd; } }
            public string QuyenHD
            { set { quyenhd = value; } get { return quyenhd; } }
            public string TenDV
            { set { tenDV = value; } get { return tenDV; } }
            public string NgayThu
            { set { ngaythu = value; } get { return ngaythu; } }
            public double ThanhTien
            { set { thanhtien = value; } get { return thanhtien; } }

        }

        List<BN_TTTU> _TTVP = new List<BN_TTTU>();

        private void btnOK_Click(object sender, EventArgs e)
        {
            _TTVP.Clear();
            DateTime ngaythu1 = new DateTime();
            ngaythu1 = DungChung.Ham.NgayTu(luptungay.DateTime);
            DateTime ngaythu2 = new DateTime();
            ngaythu2 = DungChung.Ham.NgayDen(lupdenngay.DateTime);

            var lstTamUng1 = (from a in db.TamUngs.Where(p => p.NgayThu >= ngaythu1 && p.NgayThu <= ngaythu2).Where(p => p.PhanLoai == 3)
                              join b in db.TamUngcts on a.IDTamUng equals b.IDTamUng
                              join c in db.DichVus on b.MaDV equals c.MaDV
                              select new
                              {
                                  MaBNhan = a.MaBNhan ?? 0,
                                  SoHD = a.IDTamUng,
                                  ThanhTien = b.ThanhTien,
                                  TenDV = (c.TenDV != null) ? c.TenDV : "",
                                  NgayThu = a.NgayThu
                              }).ToList();

            var lsttamung = (from a in db.TamUngs.Where(p => p.NgayThu >= ngaythu1 && p.NgayThu <= ngaythu2).Where(p => p.PhanLoai == 1)
                             join c in db.RaViens on a.MaBNhan equals c.MaBNhan into k
                             from kq in k.DefaultIfEmpty() 
                             join d in db.KPhongs on a.MaKP equals d.MaKP
                             select new
                             {
                                 MaBNhan = a.MaBNhan ?? 0,
                                 SoHD = a.IDTamUng,
                                 ThanhTien = (kq.MaBNhan != null) ? a.TienChenh : (a.SoTien??0.00),
                                 TenDV = ("TT viện phí điều trị " + d.TenKP),
                                 a.NgayThu
                             }).ToList();

            //var lsttamung2 = (from a in db.TamUngs.Where(p => p.NgayThu >= ngaythu1 && p.NgayThu <= ngaythu2).Where(p => p.PhanLoai == 1)
            //                      join b in db.BenhNhans.Where(p => p.DTuong == "KSK") on a.MaBNhan equals b.MaBNhan
            //                      join c in db.KPhongs on a.MaKP equals c.MaKP
            //                          select new
            //                          {
            //                              MaBNhan = a.MaBNhan ?? 0,
            //                              SoHD = a.IDTamUng,
            //                              ThanhTien = a.SoTien,
            //                              TenDV = "TT viện phí điều trị " + c.TenKP,
            //                              a.NgayThu
            //                          }).ToList();
            //cột số tiền=tổng số tiền bệnh nhân phải thanh toán- thu thẳng-tạm ứng p.PhanLoai == 

            //var lstVienphi = (from a in db.VienPhis.Where(p => p.NgayDuyet >= ngaythu1 && p.NgayDuyet <= ngaythu2 ) 
            //                  join b in db.VienPhicts.Where(p => p.ThanhToan == 0) on a.idVPhi equals b.idVPhi
            //                  join c in db.KPhongs on b.MaKP equals c.MaKP
            //                      group new {a,b,c} by new {
            //                      a.MaBNhan,
            //                      SoHD = (b.IDTamUng != null) ? b.IDTamUng : 0,
            //                      TenDV = ("TT viện phí điều trị " + c.TenKP)
            //                      } into kq1
            //                      select new
            //                      {
            //                          MaBNhan = kq1.Key.MaBNhan ?? 0,
            //                          SoHD = kq1.Key.SoHD??0,
            //                          ThanhTien = kq1.Sum(p => p.b.ThanhTien),
            //                          TenDV = kq1.Key.TenDV
            //                      }).ToList();
            var union = lsttamung.Concat(lstTamUng1).ToList();

            var _tamung = (from a in union
                           group a by new { a.MaBNhan, a.SoHD, a.NgayThu } into kq
                           select new
                           {
                               kq.Key.MaBNhan,
                               kq.Key.SoHD,
                               TenDV = String.Join(";", kq.Where(p => p.TenDV != null).Select(p => p.TenDV)),
                               ThanhTien = kq.Where(p => p.ThanhTien != 0).Sum(p => p.ThanhTien),
                               kq.Key.NgayThu
                           }).OrderBy(p => p.SoHD).Distinct().ToList();

            var _bn = (from a in db.BenhNhans.Where(p => p.DTuong != "BHYT")
                       select new
                       {
                           a.MaBNhan,
                           a.TenBNhan,
                           a.NamSinh,
                           a.DChi,
                           a.DTuong,
                           a.TChung
                       }).ToList();

            var BN_TU = (from a in _bn
                         join b in _tamung on a.MaBNhan equals b.MaBNhan
                         select new
                         {
                             a.TenBNhan,
                             a.NamSinh,
                             a.DChi,
                             b.SoHD,
                             b.ThanhTien,
                             TenDV = (a.DTuong == "KSK" ? ((b.TenDV.Contains("TT viện phí điều trị ")) ? a.TChung : (b.TenDV + "( " + a.TChung + " )")) : b.TenDV ),
                             b.NgayThu
                         }).OrderBy(p => p.NgayThu).ToList();

            if (BN_TU.Count() > 0)
            {
                foreach (var a in BN_TU)
                {
                    BN_TTTU themmoi = new BN_TTTU();
                    themmoi.TenBNhan = a.TenBNhan;
                    themmoi.NamSinh = a.NamSinh;
                    themmoi.DChi = a.DChi;
                    themmoi.SoHD = a.SoHD;
                    themmoi.ThanhTien = a.ThanhTien;
                    themmoi.NgayThu = a.NgayThu.Value.Day + "/" + a.NgayThu.Value.Month + "/" + a.NgayThu.Value.Year;
                    string[] arr = a.TenDV.Split(';');
                    string TenDV = "";
                    if (arr.Count() > 1)
                    {
                        for (int j = 0; j < arr.Count(); j++)
                        {
                            for (int z = j + 1; z < arr.Count(); z++)
                            {
                                if (arr[j] == arr[z] && arr[z] != null)
                                {
                                    arr[z] = null;
                                }
                            }
                            if (arr[j] != null)
                                TenDV += arr[j] + " + ";
                        }
                        TenDV = TenDV.Remove((TenDV.Length - 2));
                    }
                    else TenDV = a.TenDV;
                    themmoi.TenDV = TenDV;
                    _TTVP.Add(themmoi);
                }
            }

            BaoCao.rep_BC_TTVPTheonNgay_BNDV_26007cs rep = new BaoCao.rep_BC_TTVPTheonNgay_BNDV_26007cs();
            frmIn frm = new frmIn();
            rep.DataSource = _TTVP;
            rep.lblTieuDeBC.Text = ("BẢNG KÊ THU VIỆN PHÍ NGÀY (Nhân dân)");
            rep.ngaythang.Value = "Từ ngày " + ngaythu1.ToShortDateString() + " đến ngày " + ngaythu2.ToShortDateString();
            //rep.gr_ngay.Text = ngaythu1.Day + "/" + ngaythu1.Month + "/" + ngaythu1.Year;
            rep.cleeNgay.Text = DungChung.Ham.NgaySangChu(ngaythu1, 1);
            double TTong = BN_TU.Sum(p => p.ThanhTien);
            TTong = Math.Round(TTong, 0);
            rep.cellSoBangChu.Text = "( " + DungChung.Ham.DocTienBangChu(TTong, " đồng") + " )";
            if (lupTenCB.Text != "Chọn tên cán bộ" && lupTenCB.Text != "Tất cả")
                rep.cleeKTT.Text = lupTenCB.Text;
            else rep.cleeKTT.Text = "";
            //rep.xrTableCell17.Text = "Quyển hóa đơn ký hiệu " + " Từ số " + " đến số " + " Số tiền";
            rep.BindingData();
            rep.CreateDocument();
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}