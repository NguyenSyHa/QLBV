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
    public partial class Frm_DsXuatDuocTheoDT : DevExpress.XtraEditors.XtraForm
    {
        public Frm_DsXuatDuocTheoDT()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private bool KTtaoBc()
        {
            if (lupTuNgay.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn ngày bắt đầu in báo cáo");
                lupTuNgay.Focus();
                return false;
            }
            if (lupDenNgay.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn đến ngày kết thúc in báo cáo");
                lupDenNgay.Focus();
                return false;
            }

            return true;
        }
        private class KPhong
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
        }
       
        List<KPhong> _Kphong = new List<KPhong>();
        private void Frm_DsXuatDuocTheoDT_Load(object sender, EventArgs e)
        {
            _Kphong.Clear();
            lupTuNgay.DateTime = System.DateTime.Now;
            lupDenNgay.DateTime = System.DateTime.Now;
            var kphong = (from kp in Data.KPhongs
                          where (kp.PLoai == "Lâm sàng" || kp.PLoai == "Phòng khám")
                          select new { kp.TenKP, kp.MaKP }).ToList();
            if (kphong.Count > 0)
            {
                KPhong themmoi1 = new KPhong();
                themmoi1.tenkp = "Chọn tất cả";
                themmoi1.makp = 0;
                themmoi1.chon = true;
                _Kphong.Add(themmoi1);
                foreach (var a in kphong)
                {
                    KPhong themmoi = new KPhong();
                    themmoi.tenkp = a.TenKP;
                    themmoi.makp = a.MaKP;
                    themmoi.chon = true;
                    _Kphong.Add(themmoi);
                }
                grcKhoaphong.DataSource = _Kphong.ToList();
            }
        }
    
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            DateTime _ngaytu=DungChung.Ham.NgayTu(lupTuNgay.DateTime);
            DateTime _ngayden=DungChung.Ham.NgayDen(lupDenNgay.DateTime);
            List<KPhong> _lKhoaP = new List<KPhong>();
            if (KTtaoBc())
            {
                _lKhoaP.Clear();
                _lKhoaP = _Kphong.Where(p => p.chon == true).ToList();
                _lKhoaP.Add(new KPhong { makp = 0, tenkp = "" });
                int _bn1 = -1; int _bn2 = -1;
                {
                    if (radBN.SelectedIndex == 0) { _bn1 = 1; _bn2 = 0; }
                    if (radBN.SelectedIndex == 1) { _bn1 = 1; _bn2 = -1; }
                    if (radBN.SelectedIndex == 2) { _bn2 = 0; _bn1 = -1; }
                }
                var qd = (from nd in Data.NhapDs
                          join ndct in Data.NhapDcts on nd.IDNhap equals ndct.IDNhap
                          join bn in Data.BenhNhans on ndct.MaBNhan equals bn.MaBNhan
                          select new {bn.MaBNhan,bn.TenBNhan,bn.DTuong,bn.NoiTru,bn.MaDTuong,nd.NgayNhap,bn.MaKP,ndct.ThanhTienX }).ToList();
                var d1 = (from kp in _lKhoaP
                         join bn in qd.Where(p => p.NgayNhap >= _ngaytu && p.NgayNhap <= _ngayden).Where(p => p.NoiTru == _bn1 || p.NoiTru == _bn2).Where(p => p.DTuong == "BHYT") on kp.makp equals bn.MaKP
                         group bn by new { bn.TenBNhan, bn.MaBNhan, bn.NgayNhap } into kp
                         select new
                         {
                             NgayNhap = kp.Key.NgayNhap,
                             TenBN = kp.Key.TenBNhan,
                             MaBN = kp.Key.MaBNhan,
                             TE = kp.Where(p => p.MaDTuong == "TE").Sum(p => p.ThanhTienX),
                             GD = kp.Where(p => p.MaDTuong == "GD").Sum(p => p.ThanhTienX),
                             HS = kp.Where(p => p.MaDTuong == "HS").Sum(p => p.ThanhTienX),
                             HNDTDK = kp.Where(p => p.MaDTuong == "HN" || p.MaDTuong == "DT" || p.MaDTuong == "DK").Sum(p => p.ThanhTienX),
                             Khac = kp.Sum(p => p.ThanhTienX) - kp.Where(p => p.MaDTuong == "TE" || p.MaDTuong == "GD" || p.MaDTuong == "HS" || p.MaDTuong == "HN" || p.MaDTuong == "DT" || p.MaDTuong == "DK").Sum(p => p.ThanhTienX),
                             TT = kp.Sum(p => p.ThanhTienX),
                         }).ToList().Select(p => new { NgayNhap=p.NgayNhap.ToString().Substring(0,10),
                             p.TenBN,p.MaBN,p.TE,p.GD,p.HS,p.HNDTDK,p.Khac,p.TT
                         }).ToList();
                var d = (from a in d1
                         group a by new { a.NgayNhap, a.TenBN, a.MaBN, a.TE, a.GD, a.HS, a.HNDTDK, a.Khac, a.TT } into kq
                         select new {kq.Key.NgayNhap,kq.Key.MaBN,kq.Key.TenBN,kq.Key.TE,kq.Key.GD,kq.Key.HS,kq.Key.HNDTDK,kq.Key.Khac,kq.Key.TT }).ToList();
                var DT1 = (from dt in d
                           join vp in Data.VienPhis on dt.MaBN equals vp.MaBNhan
                           join vpct in Data.VienPhicts on vp.idVPhi equals vpct.idVPhi
                           join dv in Data.DichVus on vpct.MaDV equals dv.MaDV
                           select new {dv.PLoai,dt.NgayNhap, dt.MaBN, dt.TenBN, dt.TE, dt.GD, dt.HS, dt.HNDTDK, dt.Khac, dt.TT, vpct.TienBN }).ToList();
    
                var DT = (from dt in DT1.Where(p=>p.PLoai==1)
                          group dt by new { dt.MaBN, dt.TenBN,dt.NgayNhap } into kq
                          select new 
                              {
                                  NgayNhap=kq.Key.NgayNhap,
                                  TenBN=kq.Key.TenBN,
                                  TE =  kq.Sum(p=>p.TE),
                                  GD = kq.Sum(p => p.GD),
                                  HS =kq.Sum(p => p.HS),
                                  HNDTDK =  kq.Sum(p => p.HNDTDK),
                                  Khac =  kq.Sum(p => p.Khac),
                                  TT = kq.Sum(p => p.TT),
                                  BN =  kq.Sum(p => p.TienBN),
                                  TC = kq.Sum(p => p.TT) - kq.Sum(p => p.TienBN),
                              }).ToList();

                frmIn frm = new frmIn();
                BaoCao.Rep_DsXuatDuocTheoDT rep = new BaoCao.Rep_DsXuatDuocTheoDT();
                rep.Ngaythang.Value = "Từ ngày: " + lupTuNgay.Text.Substring(0, 10) + "  Đến ngày: " + lupDenNgay.Text.Substring(0, 10);
                rep.DataSource = DT.Where(p=>p.TT>0).OrderBy(p => p.NgayNhap);
                rep.BindingData();
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
              
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Dispose();
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
    }
}