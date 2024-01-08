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
    public partial class Frm_BcCongTacKCB_BLam : DevExpress.XtraEditors.XtraForm
    {
        public Frm_BcCongTacKCB_BLam()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        string _madt;
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
        private void Frm_BcKhoaKB_BLac_Load(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            lupTuNgay.Focus();
            lupTuNgay.DateTime = System.DateTime.Now;
            lupDenNgay.DateTime = System.DateTime.Now;
            List<DTBN> _lDTBN = new List<DTBN>();
            _lDTBN = data.DTBNs.Where(p => p.Status == 1).ToList();
            _lDTBN.Add(new DTBN { IDDTBN = 99, DTBN1 = "Tất cả" });
            lupDoituong.Properties.DataSource = _lDTBN;
            var kphong = (from kp in data.KPhongs.Where(p => p.PLoai == "Lâm sàng" || p.PLoai == "Phòng khám") select new { kp.TenKP, kp.MaKP }).ToList();
            
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
            radBN.SelectedIndex = 2;
        }

        private void btnInBC_Click(object sender, EventArgs e)
        {

            DateTime tungay = DungChung.Ham.NgayTu(lupTuNgay.DateTime);
            DateTime denngay = DungChung.Ham.NgayDen(lupDenNgay.DateTime);
            List<KPhong> _lKhoaP = new List<KPhong>();

            frmIn frm = new frmIn();
            BaoCao.Rep_BcCongTacKCB_BLam rep = new BaoCao.Rep_BcCongTacKCB_BLam();

            rep.DiaDanh.Value = DungChung.Bien.DiaDanh + ", ngày ..... tháng ..... năm .....";
            #region Hiển thị thời gian
            int nam = Convert.ToInt32(denngay.Year);
            int thang = Convert.ToInt32(denngay.Month);
            if (radIn.SelectedIndex == 0)
            { rep.NgayThang.Value = "Từ ngày " + lupTuNgay.Text + " Đến ngày " + lupDenNgay.Text; }
            if (radIn.SelectedIndex == 1)
            { rep.NgayThang.Value = "Tháng " + thang + " năm " + nam; }
            if (radIn.SelectedIndex == 2)
            {
                if (thang > 1 && thang <= 3) { rep.NgayThang.Value = "Quý I năm " + nam; }
                if (thang > 3 && thang <= 6) { rep.NgayThang.Value = "Quý II năm " + nam; }
                if (thang > 6 && thang <= 9) { rep.NgayThang.Value = "Quý III năm " + nam; }
                if (thang > 9 && thang <= 12) { rep.NgayThang.Value = "Quý IV năm " + nam; }
            }
            if (radIn.SelectedIndex == 3)
            {
                rep.NgayThang.Value = "Báo cáo 6 tháng/ năm " + nam;
            }
            if (radIn.SelectedIndex == 4)
            {
                rep.NgayThang.Value = "Báo cáo 9 tháng/ năm " + nam;
            }
            if (radIn.SelectedIndex == 5)
            { rep.NgayThang.Value = "Năm " + nam; }
            #endregion
            _lKhoaP = _Kphong.Where(p => p.chon == true).ToList();
            _lKhoaP.Add(new KPhong { makp = 0, tenkp = "" });
            int _idDTBN = 99;
            if (lupDoituong.EditValue != null)
                _idDTBN = Convert.ToInt32(lupDoituong.EditValue);
            if (KTtaoBc())
            {
                var qvp12 = (from vp in data.VienPhis.Where(p => p.NgayTT >= tungay && p.NgayTT <= denngay)
                            select new {vp.idVPhi,  vp.NgayTT, vp.MaBNhan}).ToList();
                var qvp13 = (from bn in data.BenhNhans.Where(p => (radBN.SelectedIndex == 2 ? true : p.NoiTru == radBN.SelectedIndex)).Where(p => (_idDTBN == 99 ? true : p.IDDTBN == _idDTBN)) 
                            join rv in data.RaViens on bn.MaBNhan equals rv.MaBNhan
                            select new {rv.KetQua, bn.MaBNhan,rv.MaKP, rv.MaICD, bn.IDDTBN, rv.MaCK,  bn.SThe, bn.NoiTru, bn.DTuong, bn.DTNT, bn.Tuoi, rv.Status, rv.SoNgaydt }).ToList();
              
                var qvp1 = (from vp in qvp12
                            join bn in qvp13 on vp.MaBNhan equals bn.MaBNhan
                            select new {bn.KetQua, vp.idVPhi,bn.MaKP, bn.IDDTBN, bn.MaCK,bn.MaICD, vp.NgayTT, vp.MaBNhan, bn.SThe, bn.NoiTru, bn.DTuong, bn.DTNT, bn.Tuoi, bn.Status, bn.SoNgaydt }).ToList();
                var qvpkb = (from vp in qvp1
                             join kp in  _lKhoaP on vp.MaKP equals kp.makp
                             group vp by new { } into kq
                             select new
                             {
                                 KB1 = kq.Select(p => p.MaBNhan).Count(),
                                 YHCT = kq.Where(p => p.MaCK == 7).Select(p => p.MaBNhan).Count(),
                                 YHCT_NT = kq.Where(p => p.MaCK == 7 && p.NoiTru == 1).Select(p => p.MaBNhan).Count(),
                                 YHCT_NgT = kq.Where(p => p.MaCK == 7 && p.NoiTru == 0 && p.DTNT == true).Select(p => p.MaBNhan).Count(),
                                 KB1b = kq.Where(p => p.Tuoi < 6).Select(p => p.MaBNhan).Count(),
                                 KB1c = kq.Where(p => p.DTuong == "BHYT" && p.Tuoi < 6).Select(p => p.MaBNhan).Count(),
                                 KB1d = kq.Where(p => p.DTuong == "Dịch vụ" && p.Tuoi < 6).Select(p => p.MaBNhan).Count(),
                                 KB1e = kq.Where(p => p.SThe != null && p.SThe.Contains("HN")).Select(p => p.MaBNhan).Count(),
                                 KB1f = kq.Where(p => p.SThe != null && p.SThe.Contains("HN") && p.Tuoi >= 15).Select(p => p.MaBNhan).Count(),
                                 KB1g = kq.Where(p => p.SThe != null && p.SThe.Contains("HN") && p.Tuoi < 15).Select(p => p.MaBNhan).Count(),
                                 KB1hbe = kq.Where(p => p.DTuong == "BHYT").Select(p => p.MaBNhan).Count(),
                                 KB1i = kq.Where(p => p.DTuong == "Dịch vụ").Select(p => p.MaBNhan).Count(),
                                 KB1k = kq.Where(p => p.Tuoi >= 60).Select(p => p.MaBNhan).Count(),
                                 KB1l = kq.Where(p => p.Tuoi >= 60 && p.DTuong == "BHYT").Select(p => p.MaBNhan).Count(),
                                 KB1m = kq.Where(p => p.Tuoi >= 60 && p.DTuong == "BHYT" && p.SThe.Substring(0, 2) == "DT").Select(p => p.MaBNhan).Count(),
                                 KB1p = kq.Where(p => p.Tuoi >= 60 && p.DTuong == "Dịch vụ").Select(p => p.MaBNhan).Count(),
                                 KSK = kq.Where(p => p.DTuong == "KSK").Select(p => p.MaBNhan).Count(),
                                 KB3 = kq.Where(p => p.NoiTru == 1).Select(p => p.MaBNhan).Count(),
                                 KB32 = kq.Where(p => p.NoiTru == 1 && p.Tuoi < 6).Select(p => p.MaBNhan).Count(),
                                 KB33 = kq.Where(p => p.DTuong == "BHYT" && p.NoiTru == 1 && p.Tuoi < 6).Select(p => p.MaBNhan).Count(),
                                 KB34 = kq.Where(p => p.DTuong == "Dịch vụ" && p.NoiTru == 1 && p.Tuoi < 6).Select(p => p.MaBNhan).Count(),
                                 KB35 = kq.Where(p => p.SThe != null && p.NoiTru == 1 && p.SThe.Contains("HN")).Select(p => p.MaBNhan).Count(),
                                 KB36 = kq.Where(p => p.SThe != null && p.NoiTru == 1 && p.SThe.Contains("HN") && p.Tuoi >= 15).Select(p => p.MaBNhan).Count(),
                                 KB37 = kq.Where(p => p.SThe != null && p.NoiTru == 1 && p.SThe.Contains("HN") && p.Tuoi < 15).Select(p => p.MaBNhan).Count(),
                                 KB38_32_35 = kq.Where(p => p.NoiTru == 1 && p.DTuong == "BHYT").Select(p => p.MaBNhan).Count(),
                                 KB39 = kq.Where(p => p.NoiTru == 1 && p.DTuong == "Dịch vụ").Select(p => p.MaBNhan).Count(),
                                 KB310 = kq.Where(p => p.NoiTru == 1 && p.Tuoi >= 60).Select(p => p.MaBNhan).Count(),
                                 KB311 = kq.Where(p => p.NoiTru == 1 && p.Tuoi >= 60 && p.DTuong == "BHYT").Select(p => p.MaBNhan).Count(),
                                 KB312 = kq.Where(p => p.NoiTru == 1 && p.Tuoi >= 60 && p.DTuong == "Dịch vụ").Select(p => p.MaBNhan).Count(),
                                 KB4 = kq.Where(p => p.NoiTru == 0 && p.DTNT == true).Select(p => p.MaBNhan).Count(),
                                 KB42 = kq.Where(p => p.NoiTru == 0 && p.DTNT == true && p.Tuoi < 6).Select(p => p.MaBNhan).Count(),
                                 KB43 = kq.Where(p => p.SThe != null && p.NoiTru == 0 && p.DTNT == true && p.SThe.Contains("HN")).Select(p => p.MaBNhan).Count(),
                                 KB44_42_43 = kq.Where(p => p.NoiTru == 0 && p.DTNT == true && p.DTuong == "BHYT").Select(p => p.MaBNhan).Count(),
                                 DTNT_TS = kq.Where(p => p.NoiTru == 1 && p.DTuong == "BHYT" && p.SThe.Contains("DT")).Select(p => p.MaBNhan).Count(),
                                 DTNT_TS_15l = kq.Where(p => p.NoiTru == 1 && p.Tuoi >= 15 && p.DTuong == "BHYT" && p.SThe.Contains("DT")).Select(p => p.MaBNhan).Count(),
                                 DTNT_TS_15n = kq.Where(p => p.NoiTru == 1 && p.Tuoi < 15 && p.DTuong == "BHYT" && p.SThe.Contains("DT")).Select(p => p.MaBNhan).Count(),
                                 DTNgT_TS = kq.Where(p => p.NoiTru == 0 && p.DTNT == true && p.DTuong == "BHYT" && p.SThe.Contains("DT")).Select(p => p.MaBNhan).Count(),
                                 DTNgT_TS_15l = kq.Where(p => p.NoiTru == 0 && p.DTNT == true && p.Tuoi >= 15 && p.DTuong == "BHYT" && p.SThe.Contains("DT")).Select(p => p.MaBNhan).Count(),
                                 DTNgT_TS_15n = kq.Where(p => p.NoiTru == 0 && p.DTNT == true && p.Tuoi < 15 && p.DTuong == "BHYT" && p.SThe.Contains("DT")).Select(p => p.MaBNhan).Count(),

                                 BNCTuyen_5 = kq.Where(p => p.Status==1).Select(p => p.MaBNhan).Count(),
                                 C16 = kq.Where(p =>  p.MaICD!=null && p.MaICD.ToUpper().Contains("J11") ).Select(p => p.MaBNhan).Count(),
                                 C17 = kq.Where(p => p.MaICD != null && p.MaICD.ToUpper().Contains("B02.6")).Select(p => p.MaBNhan).Count(),
                                 C18 = kq.Where(p => p.MaICD != null && p.MaICD.ToUpper().Contains("B01")).Select(p => p.MaBNhan).Count(),
                                 C19 = kq.Where(p => p.MaICD != null && p.MaICD.ToUpper().Contains("A03.9")).Select(p => p.MaBNhan).Count(),
                                 C20 = kq.Where(p => p.MaICD != null && p.MaICD.ToUpper().Contains("B05")).Select(p => p.MaBNhan).Count(),
                                 C21 = kq.Where(p => p.MaICD != null && p.MaICD.ToUpper().Contains("A09")).Select(p => p.MaBNhan).Count(),
                                 C22 = kq.Where(p => p.MaICD != null && p.MaICD.ToUpper().Contains("B06")).Select(p => p.MaBNhan).Count(),
                                 C23 = kq.Where(p => p.MaICD != null && p.MaICD.ToUpper().Contains("B34.0")).Select(p => p.MaBNhan).Count(),
                                 C24 = kq.Where(p => p.MaICD != null && p.MaICD.ToUpper().Contains("B19")).Select(p => p.MaBNhan).Count(),
                                 C25 = kq.Where(p => p.MaICD != null && p.MaICD.ToUpper().Contains("A22")).Select(p => p.MaBNhan).Count(),
                                 C26 = kq.Where(p => p.MaICD != null && p.MaICD.ToUpper().Contains("B08.8")).Select(p => p.MaBNhan).Count(),
                                 C27 = kq.Where(p => p.MaICD != null && p.MaICD.ToUpper().Contains("B54")).Select(p => p.MaBNhan).Count(),
                                 C271 = kq.Where(p => p.MaICD != null && p.MaICD.ToUpper().Contains("B54") && p.Tuoi < 1).Select(p => p.MaBNhan).Count(),
                                 C272 = kq.Where(p => p.MaICD != null && p.MaICD.ToUpper().Contains("B54") && p.Tuoi < 5).Select(p => p.MaBNhan).Count(),
                                 TNGT = kq.Where(p => p.MaICD != null && p.MaICD.ToUpper().Contains("V99")).Select(p => p.MaBNhan).Count(),
                                 TNGT_TuVong = kq.Where(p => p.MaICD != null && p.MaICD.ToUpper().Contains("V99") && p.KetQua == "Tử vong").Select(p => p.MaBNhan).Count(),
                                 TuVong = kq.Where(p => p.KetQua == "Tử vong").Select(p => p.MaBNhan).Count(),
                             }).ToList();
                if (qvpkb.Count > 0)
                {
                    rep.C14.Value =qvpkb.First().TNGT>0? qvpkb.First().TNGT.ToString():"";
                    rep.C16.Value = qvpkb.First().C16>0? qvpkb.First().C16.ToString():"";
                    rep.C17.Value = qvpkb.First().C17==0?"": qvpkb.First().C17.ToString();
                    rep.C18.Value = qvpkb.First().C18 == 0 ? "" : qvpkb.First().C18.ToString();
                    rep.C19.Value = qvpkb.First().C19 == 0 ? "" : qvpkb.First().C19.ToString();
                    rep.C20.Value = qvpkb.First().C20 == 0 ? "" : qvpkb.First().C20.ToString();
                    rep.C21.Value = qvpkb.First().C21 == 0 ? "" : qvpkb.First().C21.ToString();
                    rep.C22.Value = qvpkb.First().C22 == 0 ? "" : qvpkb.First().C22.ToString();
                    rep.C23.Value = qvpkb.First().C23 == 0 ? "" : qvpkb.First().C23.ToString();
                    rep.C24.Value = qvpkb.First().C24 == 0 ? "" : qvpkb.First().C24.ToString();
                    rep.C25.Value = qvpkb.First().C25 == 0 ? "" : qvpkb.First().C25.ToString();
                    rep.C26.Value = qvpkb.First().C26 == 0 ? "" : qvpkb.First().C26.ToString();
                    rep.C27.Value = qvpkb.First().C27 == 0 ? "" : qvpkb.First().C27.ToString();
                    rep.C27a.Value = qvpkb.First().C271 == 0 ? "" : qvpkb.First().C271.ToString();
                    rep.C27b.Value = qvpkb.First().C272 == 0 ? "" : qvpkb.First().C272.ToString();

                    rep.C1.Value = qvpkb.First().KB1 == 0 ? "" : qvpkb.First().KB1.ToString();
                    rep.C2.Value = qvpkb.First().KSK == 0 ? "" : qvpkb.First().KSK.ToString();
                    rep.C1b.Value = qvpkb.First().KB1b == 0 ? "" : qvpkb.First().KB1b.ToString();
                    rep.C1c.Value = qvpkb.First().KB1c == 0 ? "" : qvpkb.First().KB1c.ToString();
                    rep.C1d.Value = qvpkb.First().KB1d == 0 ? "" : qvpkb.First().KB1d.ToString();
                    rep.C1e.Value = qvpkb.First().KB1e == 0 ? "" : qvpkb.First().KB1e.ToString();
                    rep.C1f.Value = qvpkb.First().KB1f == 0 ? "" : qvpkb.First().KB1f.ToString();
                    rep.C1g.Value = qvpkb.First().KB1g == 0 ? "" : qvpkb.First().KB1g.ToString();
                    int c1h = (qvpkb.First().KB1hbe - qvpkb.First().KB1b - qvpkb.First().KB1e);
                    rep.C1h.Value =c1h==0?"": c1h.ToString();
                    rep.C1i.Value = qvpkb.First().KB1i == 0 ? "" : qvpkb.First().KB1i.ToString();
                    rep.C1k.Value = qvpkb.First().KB1k == 0 ? "" : qvpkb.First().KB1k.ToString();
                    rep.C1l.Value = qvpkb.First().KB1l == 0 ? "" : qvpkb.First().KB1l.ToString();
                    rep.C1m.Value = qvpkb.First().KB1m == 0 ? "" : qvpkb.First().KB1m.ToString();
                    rep.C1p.Value = qvpkb.First().KB1p == 0 ? "" : qvpkb.First().KB1p.ToString();
                    rep.C3.Value = qvpkb.First().KB3 == 0 ? "" : qvpkb.First().KB3.ToString();
                    rep.C32.Value = qvpkb.First().KB32 == 0 ? "" : qvpkb.First().KB32.ToString();
                    rep.C33.Value = qvpkb.First().KB33 == 0 ? "" : qvpkb.First().KB33.ToString();
                    rep.C34.Value = qvpkb.First().KB34 == 0 ? "" : qvpkb.First().KB34.ToString();
                    rep.C35.Value = qvpkb.First().KB35 == 0 ? "" : qvpkb.First().KB35.ToString();
                    rep.C36.Value = qvpkb.First().KB36 == 0 ? "" : qvpkb.First().KB36.ToString();
                    rep.C37.Value = qvpkb.First().KB37 == 0 ? "" : qvpkb.First().KB37.ToString();
                    int c38 = (qvpkb.First().KB38_32_35 - qvpkb.First().KB32 - qvpkb.First().KB35 - qvpkb.First().DTNT_TS);
                    rep.C38.Value =c38==0?"": c38.ToString();
                    rep.C39.Value = qvpkb.First().KB39 == 0 ? "" : qvpkb.First().KB39.ToString();
                    rep.C310.Value = qvpkb.First().KB310 == 0 ? "" : qvpkb.First().KB310.ToString();
                    rep.C311.Value = qvpkb.First().KB311 == 0 ? "" : qvpkb.First().KB311.ToString();
                    rep.C312.Value = qvpkb.First().KB312 == 0 ? "" : qvpkb.First().KB312.ToString();
                    rep.C4.Value = qvpkb.First().KB4 == 0 ? "" : qvpkb.First().KB4.ToString();
                    rep.C42.Value = qvpkb.First().KB42 == 0 ? "" : qvpkb.First().KB42.ToString();
                    rep.C5.Value = qvpkb.First().KB43 == 0 ? "" : qvpkb.First().KB43.ToString();
                    int c6 = (qvpkb.First().KB44_42_43 - qvpkb.First().KB42 - qvpkb.First().KB43 - qvpkb.First().DTNgT_TS);
                    rep.C6.Value =c6==0?"": c6.ToString();
                    rep.C7.Value = qvpkb.First().BNCTuyen_5 == 0 ? "" : qvpkb.First().BNCTuyen_5.ToString();
                    rep.cell_YHCT.Text = qvpkb.First().YHCT == 0 ? "" : qvpkb.First().YHCT.ToString();
                    rep.cell_YHCT_NT.Text = qvpkb.First().YHCT_NT == 0 ? "" : qvpkb.First().YHCT_NT.ToString();
                    rep.cell_YHCT_NgT.Text = qvpkb.First().YHCT_NgT == 0 ? "" : qvpkb.First().YHCT_NgT.ToString();
                    if(qvpkb.First().DTNT_TS > 0 )
                    rep.Cell_DT_NT_TS.Text =  qvpkb.First().DTNT_TS.ToString();
                    if (qvpkb.First().DTNT_TS_15l > 0)
                    rep.Cell_DT_NT_15l.Text = qvpkb.First().DTNT_TS_15l.ToString();
                    if (qvpkb.First().DTNT_TS_15n > 0)
                    rep.Cell_DT_NT_15n.Text =  qvpkb.First().DTNT_TS_15n.ToString();
                    if (qvpkb.First().DTNgT_TS > 0)
                    rep.Cell_DT_NgT_TS.Text =  qvpkb.First().DTNgT_TS.ToString();
                    if (qvpkb.First().DTNgT_TS_15l > 0)
                    rep.Cell_DT_NgT_15l.Text =  qvpkb.First().DTNgT_TS_15l.ToString();
                    if (qvpkb.First().DTNgT_TS_15n > 0)
                    rep.Cell_DT_NgT_15n.Text = qvpkb.First().DTNgT_TS_15n.ToString();
                    if (qvpkb.First().TuVong > 0)
                    rep.cell_TuVong.Text = qvpkb.First().TuVong.ToString();
                    if (qvpkb.First().TNGT_TuVong > 0)
                    rep.cell_TuVong_TNGT.Text = qvpkb.First().TNGT_TuVong.ToString(); 
                   
                }


                var qdt2 = (from dt in qvp1
                            join vp in data.VienPhicts on dt.idVPhi equals vp.idVPhi
                            select new { dt.IDDTBN, dt.MaBNhan, dt.DTuong, dt.NoiTru, dt.MaKP, vp.SoLuong, vp.MaDV }).ToList();
                var dvu = (from dv in data.DichVus
                           join tnhom in data.TieuNhomDVs on dv.IdTieuNhom equals tnhom.IdTieuNhom
                           select new { tnhom.TenRG, dv.MaDV }).ToList();
                var qdt1 = (from q2 in qdt2
                            join dv in dvu on q2.MaDV equals dv.MaDV
                            select new {q2.IDDTBN, q2.MaBNhan,  q2.DTuong, q2.NoiTru, q2.MaKP, dv.TenRG, q2.SoLuong }).ToList();
                var qvpcls = (from kp in _lKhoaP
                           join qd in qdt1 on kp.makp equals qd.MaKP
                              group qd by new { } into kq
                              select new
                              {
                                  CLS8 = kq.Where(p => p.TenRG.Contains("XN")).Sum(p => p.SoLuong),
                                  CLS9 = kq.Where(p => p.TenRG == "X-Quang").Sum(p => p.SoLuong),
                                  CLS10 = kq.Where(p => p.TenRG.Contains("Siêu âm")).Sum(p => p.SoLuong),
                                  CLS11 = kq.Where(p => p.TenRG == "Điện tim").Sum(p => p.SoLuong),
                                  CLS12 = kq.Where(p => p.TenRG == "Phẫu thuật").Sum(p => p.SoLuong),
                                  CLS13 = kq.Where(p => p.TenRG.Contains("Nội soi")).Sum(p => p.SoLuong),
                              }).ToList();
                if (qvpcls.Count > 0)
                {
                    rep.C8.Value = qvpcls.First().CLS8>0? qvpcls.First().CLS8.ToString():"";
                    rep.C9.Value = qvpcls.First().CLS9>0? qvpcls.First().CLS9.ToString():"";
                    rep.C10.Value =qvpcls.First().CLS10>0? qvpcls.First().CLS10.ToString():"";
                    rep.C11.Value =qvpcls.First().CLS11>0? qvpcls.First().CLS11.ToString():"";
                    rep.C12.Value =qvpcls.First().CLS12>0? qvpcls.First().CLS12.ToString():"";
                    rep.C13.Value = qvpcls.First().CLS13>0? qvpcls.First().CLS13.ToString():"";
                }

             
           
                
                   
             
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();

            }


        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void grvDTuong_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
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