using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
namespace QLBV.BHYT
{
    public partial class frm_DuyetBN : DevExpress.XtraEditors.XtraForm
    {
        public frm_DuyetBN()
        {
            InitializeComponent();
        }
        string _tenbn = "";
        int  _mabn = 0;
        bool _duyet = true;
        public frm_DuyetBN(int ma, string ten,bool duyet)
        {
            InitializeComponent();
            _tenbn = ten;
            _mabn = ma;
            _duyet = duyet;
        }
        int idvp = 0;
        public class DuyetCP
        {
            private string maQD;

            public string MaQD
            {
                get { return maQD; }
                set { maQD = value; }
            }
            public int madv;
            public string tendv;
            public double dongia;
            public string donvi;
            public double soluong;
            public double thanhtien;
            public double tienbn;
            public double tienbh;
            public double tienduyet;
            public double tienchenhBN;
            public bool duyet;
            public int mabnhan;
            public double soluongd;
            public double SoLuongD
            {
                set { soluongd = value; }
                get { return soluongd; }
            }
            public double TienDuyet
            {
                set { tienduyet = value; }
                get { return tienduyet; }
            }
            public double TienChenhBN
            {
                set { tienchenhBN = value; }
                get { return tienchenhBN; }
            }
            public int MaBNhan
            {
                set { mabnhan = value; }
                get { return mabnhan; }
            }
            public double TienBH
            {
                set { tienbh = value; }
                get { return tienbh; }
            }
            public double TienBN
            {
                set { tienbn = value; }
                get { return tienbn; }
            }
            public int MaDV
            {
                set { madv = value; }
                get { return madv; }
            }
            public string TenDV
            {
                set { tendv = value; }
                get { return tendv; }
            }
            public double DonGia
            {
                set { dongia = value; }
                get { return dongia; }
            }
            public string DonVi
            {
                set { donvi = value; }
                get { return donvi; }
            }
            public double SoLuong
            {
                set { soluong = value; }
                get { return soluong; }
            }
            public double ThanhTien
            {
                set { thanhtien = value; }
                get { return thanhtien; }
            }
            public bool Duyet
            {
                set { duyet = value; }
                get { return duyet; }
            }
        }
        QLBV_Database.QLBVEntities _dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        List<DuyetCP> _lduyet = new List<DuyetCP>();
        int _HangBV = 0;
        double _pttt = 0;
        double _tyle = 1;
        string _muc = "";
        int _tuyen = -1;
        private void frm_DuyetBN_Load(object sender, EventArgs e)
        {
            if (!_duyet)
            {
                panelBottom.Visible = _duyet;
                colDuyet.VisibleIndex = -1;
                colSoLuongD.VisibleIndex = -1;
                colTienChenh.VisibleIndex = -1;
                colTienChenhBN.VisibleIndex = -1;
                txtLydo.Visible = _duyet;
                labelControl5.Visible = _duyet;
            }
            //1 tính % thanh toán cho BN
            _HangBV = DungChung.Ham.hangBV(DungChung.Bien.MaBV);

            //ket thúc 1
            this.Text = "Duyệt chi phí bệnh nhân: " + _tenbn;
            chkDuyetBN.Text = "Duyệt BN: " + _tenbn;
            chkDuyetBN.Checked = true;
            var dv = _dataContext.DichVus.ToList();
            lupMaDVtt.DataSource = dv.ToList();
            var ktra = _dataContext.VienPhis.Where(p => p.MaBNhan== _mabn).ToList();
            if (ktra.Count > 0)
            {
                if (ktra.First().Duyet == 1 || ktra.First().Duyet == 2)
                {
                    //colDuyet.Visible = false;
                }
            }
            var ttbn = (from bn in _dataContext.BenhNhans.Where(p => p.MaBNhan== _mabn)
                        join ravien in _dataContext.RaViens on bn.MaBNhan equals ravien.MaBNhan
                        select new { bn.SThe, bn.NamSinh, bn.Tuyen, ravien.NgayRa, ravien.MaICD, ravien.ChanDoan }).ToList();
            if (ttbn.Count > 0)
            {
                if (ttbn.First().SThe != null)
                {
                    if (ttbn.First().SThe.Length > 2)
                        _muc = ttbn.First().SThe.Substring(2, 1);
                    else
                        _muc = "1";
                }
                _tuyen = ttbn.First().Tuyen.Value;
                _pttt = DungChung.Ham._PtramTT(_dataContext, _muc);
                if (_tuyen == 1)
                {
                    _tyle = 1;
                }
                else
                {
                    switch (_HangBV)
                    {
                        case 1:
                            _tyle = 0.3;
                            break;
                        case 2:
                            _tyle = 0.6;
                            break;
                        case 3:
                            _tyle = 0.7;
                            break;
                        case 4:
                            _tyle = 1;
                            break;
                    }

                }

                txtTenBN.Text = _tenbn;
                txtMaICD.Text = ttbn.First().MaICD;
                txtChanDoan.Text = ttbn.First().ChanDoan;
                dtNgayRa.DateTime = ttbn.First().NgayRa.Value;
            }
            double _tongtien = 0;
            var kt = _dataContext.VienPhis.Where(p => p.MaBNhan == _mabn).Select(p => p.Duyet).ToList();
            //var vienphi = (from kd in _dataContext.VienPhis.Where(p => p.MaBNhan == _mabn)
            //               join kdct in _dataContext.VienPhicts.Where(p => p.TrongBH == 1) on kd.idVPhi equals kdct.idVPhi
            //               group new { kdct, kd } by new {kd.LyDo, kdct.Duyet, kdct.MaDV, kdct.DonGia, kdct.DonVi, kd.idVPhi } into kq
            //               select new { kq.Key.idVPhi,kq.Key.LyDo, kq.Key.Duyet, MaDV = kq.Key.MaDV, DonGia = kq.Key.DonGia, DonVi = kq.Key.DonVi, SoLuong = kq.Sum(p => p.kdct.SoLuong), ThanhTien = kq.Sum(p => p.kdct.ThanhTien), TienBN = kq.Sum(p => p.kdct.TienBN), TienBH = kq.Sum(p => p.kdct.TienBH), SoLuongD = kq.Sum(p => p.kdct.SoLuongD), TienChenh = kq.Sum(p => p.kdct.TienChenh) }).ToList();
            var vienphi = (from kd in _dataContext.VienPhis.Where(p => p.MaBNhan == _mabn)
                           join kdct in _dataContext.VienPhicts.Where(p => p.TrongBH == 1) on kd.idVPhi equals kdct.idVPhi
                           join dvu in _dataContext.DichVus on kdct.MaDV equals dvu.MaDV
                           group new { kdct, kd,dvu } by new {dvu.MaQD, kd.LyDo, kdct.Duyet, kdct.TienChenhBN, kdct.MaDV, kdct.DonGia, kdct.DonVi, kd.idVPhi, duyet = kd.Duyet } into kq
                           select new {kq.Key.MaQD, kq.Key.idVPhi, kq.Key.LyDo, kq.Key.TienChenhBN, kq.Key.Duyet, kq.Key.duyet, MaDV = kq.Key.MaDV, DonGia = kq.Key.DonGia, DonVi = kq.Key.DonVi, SoLuong = kq.Sum(p => p.kdct.SoLuong), ThanhTien = kq.Sum(p => p.kdct.ThanhTien), TienBN = kq.Sum(p => p.kdct.TienBN), TienBH = kq.Sum(p => p.kdct.TienBH), SoLuongD = kq.Sum(p => p.kdct.SoLuongD), TienChenh = kq.Sum(p => p.kdct.TienChenh) }).ToList();
            if (vienphi.Count > 0)
            {
                idvp = vienphi.First().idVPhi;
                _tongtien = vienphi.Sum(p => p.ThanhTien);
                if (_tongtien <= DungChung.Bien.GHanTT100)
                {
                    if (_tuyen == 1)
                        _pttt = 100;

                }
                else
                {
                    if (_tuyen == 2) // trái tuyến
                        _pttt = Math.Round(_pttt * _tyle, 1);
                }
                txtLydo.Text = vienphi.First().LyDo;
            }
            foreach (var vp in vienphi)
            {
                DuyetCP duyet = new DuyetCP();
                duyet.MaQD = vp.MaQD;
                duyet.DonGia = vp.DonGia;
                duyet.DonVi = vp.DonVi;
                duyet.Duyet = true;
                duyet.MaDV = vp.MaDV == null ?0 : vp.MaDV.Value;
                duyet.SoLuong = vp.SoLuong;
                if (vp.ThanhTien != null)
                    duyet.ThanhTien = vp.ThanhTien;
                else
                    duyet.thanhtien = 0;
                if (vp.TienBH != null)
                    duyet.TienBH = vp.TienBH;
                else
                    duyet.TienBH = 0;
                if (vp.TienBN != null)
                    duyet.TienBN = vp.TienBN;
                else
                    duyet.TienBN = 0;
                if (vp.TienChenh != null)
                    duyet.TienDuyet = vp.TienChenh;
                else duyet.TienDuyet = 0;
                if (vp.TienChenhBN != null)
                    duyet.TienChenhBN = vp.TienChenhBN;
                else duyet.TienChenhBN = 0;
                if (vp.SoLuongD != null)
                    duyet.SoLuongD = vp.SoLuongD;
                else
                    duyet.SoLuongD = 0;
                if (vp.duyet != null)
                {
                    if (vp.duyet.Value == 1 || vp.duyet.Value == 2)
                    {
                        duyet.Duyet = true;
                        chkDuyetBN.Text = " BN: " + _tenbn + " đã được duyệt";
                        //chkDuyetBN.Enabled = false;
                    }
                    else
                        if (vp.duyet.Value == 3)
                        {
                            duyet.Duyet = false;
                            chkDuyetBN.Text = " BN: " + _tenbn + " đã xuất toán";
                            //chkDuyetBN.Enabled = false;
                        }
                }
                duyet.MaBNhan = _mabn;
                _lduyet.Add(duyet);
                //duyet.TenDV=vp
            }

            binSDuyet.DataSource = _lduyet;
            grcThanhToan.DataSource = binSDuyet;
            #region kiểm tra duyệt hay chưa
            //if (kt.Count > 0)
            //{
            //    if (kt.First() != null)
            //    {
            //        if (kt.First().Value == 1 || kt.First().Value == 2)
            //        {
            //            var vienphi = (from kd in _dataContext.VienPhis.Where(p => p.MaBNhan == _mabn)
            //                           join kdct in _dataContext.VienPhicts.Where(p => p.TrongBH == 1) on kd.idVPhi equals kdct.idVPhi
            //                           group new { kdct, kd } by new { kdct.Duyet, kdct.MaDV, kdct.DonGia, kdct.DonVi, kd.idVPhi } into kq
            //                           select new { kq.Key.idVPhi, kq.Key.Duyet, MaDV = kq.Key.MaDV, DonGia = kq.Key.DonGia, DonVi = kq.Key.DonVi, SoLuong = kq.Sum(p => p.kdct.SoLuong), ThanhTien = kq.Sum(p => p.kdct.ThanhTien), TienBN = kq.Sum(p => p.kdct.TienBN), TienBH = kq.Sum(p => p.kdct.TienBH), SoLuongD = kq.Sum(p => p.kdct.SoLuongD), TienChenh = kq.Sum(p => p.kdct.TienChenh) }).ToList();
            //            if (vienphi.Count > 0)
            //            {
            //                idvp = vienphi.First().idVPhi;
            //                if (_tongtien < DungChung.Bien.GHanTT100) {
            //                    _pttt = 100;
            //                }
            //            }
            //            foreach (var vp in vienphi)
            //            {
            //                DuyetCP duyet = new DuyetCP();
            //                duyet.DonGia = vp.DonGia.Value;
            //                duyet.DonVi = vp.DonVi;
            //                duyet.Duyet = true;
            //                duyet.MaDV = vp.MaDV;
            //                duyet.SoLuong = vp.SoLuong.Value;
            //                duyet.ThanhTien = vp.ThanhTien.Value;
            //                duyet.TienBH = vp.TienBH.Value;
            //                duyet.TienBN = vp.TienBN.Value;
            //                if(vp.TienChenh!=null)
            //                duyet.TienDuyet = vp.TienChenh.Value;
            //                if(vp.SoLuongD!=null)
            //                duyet.SoLuongD = vp.SoLuongD.Value;
            //                if (vp.Duyet != null)
            //                {
            //                    if (vp.Duyet.Value == 1)
            //                        duyet.Duyet = true;
            //                    else
            //                        if (vp.Duyet.Value == 2)
            //                            duyet.Duyet = false;
            //                }
            //                duyet.MaBNhan = _mabn;
            //                _lduyet.Add(duyet);
            //                //duyet.TenDV=vp
            //            }

            //            binSDuyet.DataSource = _lduyet;
            //            grcThanhToan.DataSource = binSDuyet;
            //        }
            //        else
            //        {
            //            var vienphi = (from kd in _dataContext.VienPhis.Where(p => p.MaBNhan == _mabn)
            //                           join kdct in _dataContext.VienPhicts.Where(p => p.TrongBH == 1) on kd.idVPhi equals kdct.idVPhi
            //                           group new { kdct, kd } by new { kdct.Duyet, kdct.MaDV, kdct.DonGia, kdct.DonVi, kd.idVPhi } into kq
            //                           select new { kq.Key.idVPhi, kq.Key.Duyet, MaDV = kq.Key.MaDV, DonGia = kq.Key.DonGia, DonVi = kq.Key.DonVi, SoLuong = kq.Sum(p => p.kdct.SoLuong), ThanhTien = kq.Sum(p => p.kdct.ThanhTien), TienBN = kq.Sum(p => p.kdct.TienBN), TienBH = kq.Sum(p => p.kdct.TienBH), SoLuongD = kq.Sum(p => p.kdct.SoLuong), TienChenh = kq.Sum(p => p.kdct.ThanhTien) }).ToList();
            //            if (vienphi.Count > 0)
            //                idvp = vienphi.First().idVPhi;
            //            foreach (var vp in vienphi)
            //            {
            //                DuyetCP duyet = new DuyetCP();
            //                duyet.DonGia = vp.DonGia.Value;
            //                duyet.DonVi = vp.DonVi;
            //                duyet.Duyet = true;
            //                duyet.MaDV = vp.MaDV;
            //                duyet.SoLuong = vp.SoLuong.Value;
            //                duyet.ThanhTien = vp.ThanhTien.Value;
            //                duyet.TienBH = vp.TienBH.Value;
            //                duyet.TienBN = vp.TienBN.Value;
            //                duyet.TienDuyet = vp.TienChenh.Value;
            //                duyet.SoLuongD = vp.SoLuongD.Value;
            //                if (vp.Duyet != null)
            //                {
            //                    if (vp.Duyet.Value == 1)
            //                        duyet.Duyet = true;
            //                    else
            //                        if (vp.Duyet.Value == 2)
            //                            duyet.Duyet = false;
            //                }
            //                duyet.MaBNhan = _mabn;
            //                _lduyet.Add(duyet);
            //                //duyet.TenDV=vp
            //            }

            //            binSDuyet.DataSource = _lduyet;
            //            grcThanhToan.DataSource = binSDuyet;
            //        }
            //    }
            //    else
            //    {
            //        var vienphi = (from kd in _dataContext.VienPhis.Where(p => p.MaBNhan == _mabn)
            //                       join kdct in _dataContext.VienPhicts.Where(p => p.TrongBH == 1) on kd.idVPhi equals kdct.idVPhi
            //                       group new { kdct, kd } by new { kdct.Duyet, kdct.MaDV, kdct.DonGia, kdct.DonVi, kd.idVPhi } into kq
            //                       select new { kq.Key.idVPhi, kq.Key.Duyet, MaDV = kq.Key.MaDV, DonGia = kq.Key.DonGia, DonVi = kq.Key.DonVi, SoLuong = kq.Sum(p => p.kdct.SoLuong), ThanhTien = kq.Sum(p => p.kdct.ThanhTien), TienBN = kq.Sum(p => p.kdct.TienBN), TienBH = kq.Sum(p => p.kdct.TienBH), SoLuongD = kq.Sum(p => p.kdct.SoLuong), TienChenh = kq.Sum(p => p.kdct.ThanhTien) }).ToList();
            //        if (vienphi.Count > 0)
            //            idvp = vienphi.First().idVPhi;
            //        foreach (var vp in vienphi)
            //        {
            //            DuyetCP duyet = new DuyetCP();
            //            duyet.DonGia = vp.DonGia.Value;
            //            duyet.DonVi = vp.DonVi;
            //            duyet.Duyet = true;
            //            duyet.MaDV = vp.MaDV;
            //            duyet.SoLuong = vp.SoLuong.Value;
            //            duyet.ThanhTien = vp.ThanhTien.Value;
            //            duyet.TienBH = vp.TienBH.Value;
            //            duyet.TienBN = vp.TienBN.Value;
            //            duyet.TienDuyet = vp.TienChenh.Value;
            //            duyet.SoLuongD = vp.SoLuongD.Value;
            //            if (vp.Duyet != null)
            //            {
            //                if (vp.Duyet.Value == 1)
            //                    duyet.Duyet = true;
            //                else
            //                    if (vp.Duyet.Value == 2)
            //                        duyet.Duyet = false;
            //            }
            //            duyet.MaBNhan = _mabn;
            //            _lduyet.Add(duyet);
            //            //duyet.TenDV=vp
            //        }

            //        binSDuyet.DataSource = _lduyet;
            //        grcThanhToan.DataSource = binSDuyet;
            //    }

            //}
            #endregion
        }
        //List<TDinh> _lTDinh = new List<TDinh>();
        //List<TDinhct> _lTDinhct = new List<TDinhct>();
        private void btnDuyet_Click(object sender, EventArgs e)
        {
            //
            int dchinh = 0;
            for (int i = 0; i < grvThanhToan.RowCount; i++)
            {
                if (grvThanhToan.GetRowCellValue(i, colSoLuongD) != null && grvThanhToan.GetRowCellValue(i, colSoLuongD).ToString() != "")
                {
                    if (Convert.ToInt32(grvThanhToan.GetRowCellValue(i, colSoLuongD)) > 0)
                    {
                        dchinh = 2;
                        break;
                    }
                    if (grvThanhToan.GetRowCellValue(i, colDuyet) != null)
                        if (grvThanhToan.GetRowCellValue(i, colDuyet).ToString() == "False")
                        {
                            dchinh = 2;
                            break;
                        }
                }
            }
            //
            DialogResult _result = MessageBox.Show("Duyệt chi phí BN:" + _tenbn, "Duyệt chi phí", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (_result == DialogResult.Yes)
            {
                int madv = 0;
                if (chkDuyetBN.Checked)
                {
                    for (int i = 0; i < grvThanhToan.RowCount; i++) //update _lduyet
                    {
                        if (grvThanhToan.GetRowCellValue(i, colDuyet) != null)
                        {
                            if (grvThanhToan.GetRowCellValue(i, colDuyet).ToString() == "False" || grvThanhToan.GetRowCellValue(i, colDuyet).ToString() == "false")
                            {
                                if (grvThanhToan.GetRowCellValue(i, colMaDV) != null)
                                {
                                    madv = Convert.ToInt32( grvThanhToan.GetRowCellValue(i, colMaDV));
                                }
                                foreach (var j in _lduyet)
                                {
                                    if (j.MaDV == madv)
                                    {
                                        j.Duyet = false;
                                    }
                                }
                            }
                        }
                    }
                    var vp = _dataContext.VienPhis.Single(p => p.idVPhi == idvp);
                    if (dchinh == 2)
                        vp.Duyet = 2;// duyệt có điều chỉnh CP
                    else
                        vp.Duyet = 1; // duyệt toàn phần
                    vp.LyDo = txtLydo.Text;
                    if (_dataContext.SaveChanges() >= 0)
                    {
                        var vpct = _dataContext.VienPhicts.Where(p => p.idVPhi == idvp).ToList();
                        foreach (var ct in vpct)
                        {
                            var update = _dataContext.VienPhicts.Single(p => p.idVPhict == ct.idVPhict);
                            int br = 0;
                            foreach (var li in _lduyet)
                            {
                                if (update.MaDV == li.MaDV)
                                {
                                    update.TienChenh = li.TienDuyet;
                                    update.SoLuongD = li.SoLuongD;
                                    update.TienChenhBN = li.TienChenhBN;

                                    if (li.Duyet == true)
                                    {
                                        if (li.SoLuongD > 0)
                                            update.Duyet = 2;// duyệt một phần
                                        else
                                            update.Duyet = 1; // duyệt toàn phần
                                    }
                                    else
                                    {
                                        update.TienChenh = li.TienBH;
                                        update.SoLuongD = li.SoLuong;
                                        update.Duyet = 3; // không duyệt 
                                    }
                                    br = 1;
                                }
                                if (br == 1)
                                    break;
                            }
                            _dataContext.SaveChanges();
                        }
                    }
                }
                else
                {
                    var vp = _dataContext.VienPhis.Single(p => p.idVPhi == idvp);
                    vp.Duyet = 3; // không duyệt
                    _dataContext.SaveChanges();
                    var vpct = _dataContext.VienPhicts.Where(p => p.idVPhi == idvp).ToList();
                    foreach (var a in vpct)
                    {
                        var update = _dataContext.VienPhicts.Single(p => p.idVPhict == a.idVPhict);
                        update.Duyet = 3;
                        _dataContext.SaveChanges();
                    }
                }
                this.Dispose();
            }
            //var ttbn=(from bn in _dataContext.BenhNhans.Where(p=>p.MaBNhan== (_mabn)) join rvien in _dataContext.RaViens on bn.MaBNhan equals rvien.MaBNhan
            //       select new{bn.TenBNhan,bn.MaBNhan,bn.GTinh,bn.NoiTinh,bn.NoiTru,bn.NamSinh,bn.NNhap,bn.MaCS,bn.Tuoi,bn.Tuyen,bn.SThe,rvien.NgayRa,rvien.SoNgaydt,rvien.MaICD}).ToList();
            //if (ttbn.Count > 0)
            //{
            //    TDinh _moi = new TDinh();
            //    _moi.MaBNhan = ttbn.First().MaBNhan;
            //    _moi.GTinh = ttbn.First().GTinh;
            //    _moi.MaCS = ttbn.First().MaCS;
            //    _moi.NoiTinh = ttbn.First().NoiTinh;
            //    _moi.NgayKham = ttbn.First().NNhap;
            //    _moi.NgayRa = ttbn.First().NgayRa;
            //    _moi.SoNgayDT = ttbn.First().SoNgaydt;
            //    _moi.SThe = ttbn.First().SThe;
            //    _moi.TenBNhan = ttbn.First().TenBNhan;
            //    _moi.Tuoi = ttbn.First().Tuoi;
            //    _moi.Tuyen = ttbn.First().Tuyen;
            //    _moi.Status = 1;// 1 là duyệt, 0 là ko duyệt
            //    _dataContext.TDinhs.Add(_moi);
            //    if (_dataContext.SaveChanges() >= 0) {
            //        foreach (var d in _lduyet)
            //        {

            //            TDinhct _moict = new TDinhct();
            //            _moict.DonGia = d.DonGia;
            //            _moict.DonVi = d.DonVi;
            //            _moict.MaBNhan = _mabn;
            //            _moict.MaDV = d.MaDV;
            //            _moict.SoLuong = d.SoLuong;
            //            _moict.TienBH = d.TienBH;
            //            _moict.TienBN = d.TienBN;
            //            _moict.ThanhTien = d.ThanhTien;
            //            if (d.duyet == true)
            //                _moict.Duyet = 1;
            //            else
            //                _moict.Duyet = 0;


            //        }
            //    }
            //}

        }

        private void chkDuyetBN_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDuyetBN.Checked == false)
            {
                grvThanhToan.OptionsBehavior.ReadOnly = true;
                for (int i = 0; i < grvThanhToan.RowCount; i++)
                {
                    grvThanhToan.SetRowCellValue(i, colDuyet, false);
                }
            }
            else
            {
                grvThanhToan.OptionsBehavior.ReadOnly = false;
                for (int i = 0; i < grvThanhToan.RowCount; i++)
                {
                    grvThanhToan.SetRowCellValue(i, colDuyet, true);
                }
            }
        }

        private void grvThanhToan_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "colSoLuongD")
            {
                double dongia = 0;
                double soluong = 0;
                double thanhtien = 0;
                double Tongso = 0;
                if (grvThanhToan.GetFocusedRowCellValue(colSoLuongD) != null && grvThanhToan.GetFocusedRowCellValue(colSoLuongD).ToString() != "")
                {
                    if (grvThanhToan.GetFocusedRowCellValue(colSoLuong) != null && grvThanhToan.GetFocusedRowCellValue(colSoLuong).ToString() != "")
                    {
                        Tongso = Convert.ToDouble(grvThanhToan.GetFocusedRowCellValue(colSoLuong));
                    }
                    soluong = Convert.ToDouble(grvThanhToan.GetFocusedRowCellValue(colSoLuongD));
                    if (soluong > Tongso || soluong < 0)
                    {
                        MessageBox.Show("số lượng không hợp lệ");
                        grvThanhToan.SetFocusedRowCellValue(colSoLuongD, 0);
                        grvThanhToan.FocusedColumn = grvThanhToan.VisibleColumns[7];// set focus
                    }
                    else
                    {
                        if (soluong == Tongso)
                            grvThanhToan.SetFocusedRowCellValue(colDuyet, false);
                        else
                            grvThanhToan.SetFocusedRowCellValue(colDuyet, true);
                        if (grvThanhToan.GetFocusedRowCellValue(colDonGia) != null && grvThanhToan.GetFocusedRowCellValue(colDonGia).ToString() != "")
                        {
                            dongia = Convert.ToDouble(grvThanhToan.GetFocusedRowCellValue(colDonGia));
                            thanhtien = Math.Round(soluong * dongia * _pttt * 0.01, DungChung.Bien.LamTronSo);
                            grvThanhToan.SetFocusedRowCellValue(colTienChenh, thanhtien);
                            double tongtien = Math.Round(soluong * dongia, DungChung.Bien.LamTronSo);
                            grvThanhToan.SetFocusedRowCellValue(colTienChenhBN, tongtien - thanhtien);
                        }
                    }
                }
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

    }

}