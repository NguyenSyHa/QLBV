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
    public partial class Frm_TKChiPhiTheoDV : DevExpress.XtraEditors.XtraForm
    {
        public Frm_TKChiPhiTheoDV()
        {
            InitializeComponent();
        }

        private void rdFont_Properties_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        class MyObject
        {
            public string value { set; get; }
            public string Text { set; get; }
        }

        public class KhoaPhong
        {
            public bool _check;
            public int _maKP;
            public string _kp;

            public int MaKP { get { return _maKP; } set { _maKP = value; } }
            public bool Check { get { return _check; } set { _check = value; } }
            public string TenKP { get { return _kp; } set { _kp = value; } }

            public string PLoai { get; set; }
        }
        class BangKe
        {
            public string TenDV { get; set; }
            public double DonGia { get; set; }
            public double SLKP1 { get; set; }
            public double SLKP2 { get; set; }
            public double SLKP3 { get; set; }
            public double SLKP4 { get; set; }
            public double SLKP5 { get; set; }
            public double SLKP6 { get; set; }
            public double SLKP7 { get; set; }
            public double SLKP8 { get; set; }
            public double SLKP9 { get; set; }
            public double SLKP10 { get; set; }
            public double SLKP11 { get; set; }
            public double SLKP12 { get; set; }
            public double SLKP13 { get; set; }
            public double SLKP14 { get; set; }
            public double SLKP15 { get; set; }
            public double Tong { get; set; }
            public double ThanhTien { get; set; }
        }
        List<TieuNhomDV> _listTieuNhom = new List<TieuNhomDV>();
        List<DichVu> _listDV = new List<DichVu>();
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void Frm_TKChiPhiTheoDV_Load(object sender, EventArgs e)
        {
            ckc_BNCCT.Checked = true;
            ckTLBH.Checked = true;
            ckTLBN.Checked = true;
            lupDenNgay.DateTime = System.DateTime.Now;
            lupTuNgay.DateTime = System.DateTime.Now;
            rdHTThanhToan.SelectedIndex = 2;
            rdCKhoan.SelectedIndex = 2;
            List<MyObject> lMaDtuong = new List<MyObject>();
            lMaDtuong = data.DTuongs.Where(p => p.Status == 1).Select(p => new MyObject { value = p.MaDTuong, Text = p.MaDTuong }).OrderBy(p => p.Text).ToList();
            //lMaDtuong = data.BenhNhans.Select(p => new MyObject { value = p.MaDTuong == null ? "" : p.MaDTuong.Trim().ToUpper(), Text = p.MaDTuong == null ? "" : p.MaDTuong.Trim().ToUpper() }).Distinct().OrderBy(p => p.Text).ToList();
            lMaDtuong.Insert(0, new MyObject { value = "", Text = "Tất cả" });
            //lMaDtuong.Insert(0, new MyObject { value = "", Text = "Tất cả" });
            if (lMaDtuong.Count <= 0)
                MessageBox.Show("Danh mục Đối Tượng chưa được thiết lập!");
            cklMaDTuong.DataSource = lMaDtuong;
            cklMaDTuong.CheckAll();

            // load danh sách khoa phòng
            var q3 = (from k in data.KPhongs
                      join rv in data.RaViens on k.MaKP equals rv.MaKP
                      select k).ToList();
            var q2 = (from k in q3
                      select k).Distinct().ToList();
            var q = (from k in q2
                     select new KhoaPhong()
                     {
                         Check = false,
                         MaKP = k.MaKP,
                         TenKP = k.TenKP,
                         PLoai = k.PLoai
                     }).Distinct().OrderBy(p => p.PLoai).ToList();
            List<KhoaPhong> _lKP2 = new List<KhoaPhong>(q.Count + 1);
            _lKP2.Add(new KhoaPhong { Check = false, MaKP = 0, TenKP = "Tất cả" });
            _lKP2.InsertRange(1, q);
            grcKhoaPhong.DataSource = _lKP2;
            for (int i = 0; i < grvKhoaPhong.RowCount; i++)
            {
                grvKhoaPhong.SetRowCellValue(i, colCheckGrvKP, true);
            }

            //List<DTBN> _lDTBN = new List<DTBN>();
            //_lDTBN = data.DTBNs.Where(p => p.Status == 1).ToList();
            //_lDTBN.Add(new DTBN { IDDTBN = 0, DTBN1 = "Tất cả" });
            //lupDoituong.Properties.DataSource = _lDTBN.ToList();
            lupDoituong.SelectedIndex = 0;
            radNoiTru_SelectedIndexChanged(sender, e);
            radio_DTNT.SelectedIndex = 2;
            radXP_SelectedIndexChanged(sender, e);

            var _listNhomDV = data.NhomDVs.Where(p => p.Status > 0).ToList();
            cklNhomDV.DisplayMember = "TenNhomCT";
            cklNhomDV.ValueMember = "IDNhom";
            cklNhomDV.DataSource = _listNhomDV;
            _listTieuNhom = data.TieuNhomDVs.Where(p => p.Status == 1).ToList();
            _listDV = data.DichVus.ToList();
            _listKP = data.KPhongs.ToList();
        }

        private void radNoiTru_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (radNoiTru.SelectedIndex == 0)
            {
                radio_DTNT.Properties.ReadOnly = false;
            }
            else
            {
                radio_DTNT.SelectedIndex = 2;
                radio_DTNT.Properties.ReadOnly = true;
            }
        }
        public class CSKCB
        {
            public bool _check1;
            public string _maKP1;
            public string _kp1;

            public string MaKP { get { return _maKP1; } set { _maKP1 = value; } }
            public bool Check { get { return _check1; } set { _check1 = value; } }
            public string TenKP { get { return _kp1; } set { _kp1 = value; } }
        }
        List<CSKCB> _lCSKCB = new List<CSKCB>();
        private void radXP_SelectedIndexChanged(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities _dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            _lCSKCB.Clear();
            if (radXP.SelectedIndex == 0)
            {
                _lCSKCB.Add(new CSKCB { Check = true, MaKP = DungChung.Bien.MaBV, TenKP = DungChung.Bien.TenCQ });


            }
            if (radXP.SelectedIndex == 1)
            {
                //DungChung.Bien.mack_theoHangBV
                _lCSKCB = (from kp in _listKP
                           where kp.PLoai == DungChung.Bien.st_PhanLoaiKP.XaPhuong //|| (kp.PLoai == DungChung.Bien.st_PhanLoaiKP.PhongKham && kp.TrongBV == 0)
                           select new CSKCB()
                           {
                               Check = false,
                               MaKP = kp.MaBVsd,
                               TenKP = kp.TenKP
                           }).Distinct().OrderBy(p => p.TenKP).ToList();

            }
            if (radXP.SelectedIndex == 2)
            {
                _lCSKCB = (from kp in _listKP
                           where kp.PLoai == DungChung.Bien.st_PhanLoaiKP.PKKhuVuc
                           select new CSKCB()
                           {
                               Check = false,
                               MaKP = kp.MaBVsd,
                               TenKP = kp.TenKP
                           }).Distinct().OrderBy(p => p.TenKP).ToList();


            }
            if (radXP.SelectedIndex == 3)
            {
                _lCSKCB = (from kp in _dataContext.BenhViens.Where(p => p.Connect)
                           select new CSKCB()
                           {
                               Check = false,
                               MaKP = kp.MaBV,
                               TenKP = kp.TenBV
                           }).Distinct().OrderBy(p => p.TenKP).ToList();

            }
            _lCSKCB.Insert(0, new CSKCB { Check = false, MaKP = "0", TenKP = "Tất cả" });
            cklKP.DataSource = _lCSKCB;
            cklKP.CheckAll();
        }

        private void grvKhoaPhong_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "colCheckGrvKP")
            {

                if (e.RowHandle == 0)
                {
                    if (grvKhoaPhong.GetFocusedRowCellValue(colCheckGrvKP) != null)
                    {
                        if (grvKhoaPhong.GetRowCellValue(0, colCheckGrvKP).ToString() == "False")
                        {
                            for (int i = 0; i < grvKhoaPhong.RowCount; i++)
                            {
                                grvKhoaPhong.SetRowCellValue(i, "Check", true);
                            }
                        }
                        else
                        {
                            for (int i = 0; i < grvKhoaPhong.RowCount; i++)
                            {
                                grvKhoaPhong.SetRowCellValue(i, "Check", false);
                            }
                        }

                    }
                }
                else
                {
                    for (int i = 0; i < grvKhoaPhong.RowCount; i++)
                    {
                        if (grvKhoaPhong.GetRowCellValue(i, colCheckGrvKP) != null && grvKhoaPhong.GetRowCellValue(i, colCheckGrvKP).ToString() == "True")
                        {
                            grvKhoaPhong.SetRowCellValue(0, colCheckGrvKP, false);
                            break;
                        }
                        else
                        {

                        }
                    }

                }

            }
        }

        private void rdHTThanhToan_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdHTThanhToan.SelectedIndex == 1)
            {
                rdCKhoan.Enabled = true;
                rdCKhoan.SelectedIndex = 2;
            }
            else
                rdCKhoan.Enabled = false;
        }

        private void lupDoituong_EditValueChanged(object sender, EventArgs e)
        {
            if (lupDoituong.Text == ("BHYT") || lupDoituong.Text == ("Tất cả"))
            {
                rdTrongBH.Enabled = true;
                radXP.Enabled = true;
                cboNoiTinh.Enabled = true;
                cklMaDTuong.Enabled = true;
            }
            else
            {
                cboNoiTinh.Enabled = false;
                cboNoiTinh.SelectedIndex = 0;
                rdTrongBH.Enabled = false;
                rdTrongBH.SelectedIndex = 0;
                cklMaDTuong.Enabled = false;
            }
            cklMaDTuong.CheckAll();
        }

        private void cklNhomDV_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ckcTieuNhomDV_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void hyp_Chon_OpenLink(object sender, DevExpress.XtraEditors.Controls.OpenLinkEventArgs e)
        {
            ckl_DichVu.CheckAll();
        }

        private void hyp_HuyChon_OpenLink(object sender, DevExpress.XtraEditors.Controls.OpenLinkEventArgs e)
        {
            ckl_DichVu.UnCheckAll();
        }

        private void cklNhomDV_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            List<int> _idNhomDV = new List<int>();
            for (int i = 0; i < cklNhomDV.ItemCount; i++)
            {
                if (cklNhomDV.GetItemCheckState(i) == CheckState.Checked)
                    _idNhomDV.Add(Convert.ToInt32(cklNhomDV.GetItemValue(i)));
            }
            var _ltn = (from nh in _idNhomDV
                        join tn in _listTieuNhom on nh equals tn.IDNhom
                        select tn).ToList();
            ckcTieuNhomDV.DisplayMember = "TenTN";
            ckcTieuNhomDV.ValueMember = "IdTieuNhom";
            ckcTieuNhomDV.DataSource = _ltn;
            ckcTieuNhomDV_ItemCheck(null, null);
        }
        List<DichVu> _ldv = new List<DichVu>();
        private void ckcTieuNhomDV_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            List<int> _idTieuNhomDV = new List<int>();
            for (int i = 0; i < ckcTieuNhomDV.ItemCount; i++)
            {
                if (ckcTieuNhomDV.GetItemCheckState(i) == CheckState.Checked)
                    _idTieuNhomDV.Add(Convert.ToInt32(ckcTieuNhomDV.GetItemValue(i)));
            }
            _ldv = (from nh in _idTieuNhomDV
                    join tn in _listDV on nh equals tn.IdTieuNhom
                    select tn).ToList();
            ckl_DichVu.DisplayMember = "TenDV";
            ckl_DichVu.ValueMember = "MaDV";
            ckl_DichVu.DataSource = _ldv;
        }
        private bool KTtaoBcMau20()
        {

            if (lupTuNgay.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn ngày bắt đầu in báo cáo");
                lupTuNgay.Focus();
                return false;
            }
            if (lupDenNgay.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn ngày kết thúc in báo cáo");
                lupDenNgay.Focus();
                return false;
            }
            else return true;
        }
        List<KPhong> _listKP = new List<KPhong>();
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            List<string> _dsCSKCB = new List<string>();
            for (int i = 0; i < cklKP.ItemCount; i++)
            {
                if (cklKP.GetItemChecked(i))
                {
                    _dsCSKCB.Add(cklKP.GetItemValue(i).ToString());
                }
            }
            _dsCSKCB = _dsCSKCB.Distinct().ToList();

            #region Lấy danh sách khoa phòng
            List<int> _lMaKhoa = new List<int>();
            int kp = 0;
            for (int i = 0; i < grvKhoaPhong.RowCount; i++)
            {
                if (grvKhoaPhong.GetRowCellValue(i, colCheckGrvKP) != null)
                {
                    if (grvKhoaPhong.GetRowCellValue(i, colCheckGrvKP).ToString() == "True")
                    {
                        int mKhoa = grvKhoaPhong.GetRowCellValue(i, colmaKP) == null ? -1 : Convert.ToInt32(grvKhoaPhong.GetRowCellValue(i, colmaKP));

                        if (mKhoa == 0)
                        {
                            kp = 0;
                            if (radBaoCao.SelectedIndex != 2)
                                break;
                        }
                        else
                            _lMaKhoa.Add(mKhoa);
                    }
                    else
                    {
                        kp = -1;
                    }
                }
            }
            #endregion lấy danh sách khoa phòng

            #region Biến
            int _makp = -1;

            DateTime tungay, denngay;
            int trongBH = 5;


            int _idDtuong = -1;
            _idDtuong = lupDoituong.SelectedIndex;
            List<string> lmaDtuong = new List<string>();
            for (int i = 0; i < cklMaDTuong.ItemCount; i++)
            {
                if (cklMaDTuong.GetItemChecked(i))
                    lmaDtuong.Add(cklMaDTuong.GetItemValue(i).ToString());
            }
            lmaDtuong.Add("DV");
            List<int> _lDichVu = new List<int>();
            for (int i = 0; i < ckl_DichVu.ItemCount; i++)
            {
                if (ckl_DichVu.GetItemChecked(i))
                    _lDichVu.Add(Convert.ToInt32(ckl_DichVu.GetItemValue(i)));
            }
            int xp = radXP.SelectedIndex;
            int _intduyet = 2;
            if (rad_Duyet.SelectedIndex != null)
                _intduyet = rad_Duyet.SelectedIndex;
            int _CP_BH = -1;
            if ((ckc_BNCCT.Checked && ckTLBH.Checked && ckTLBN.Checked) || (!ckc_BNCCT.Checked && !ckTLBH.Checked && !ckTLBN.Checked))
                _CP_BH = 0;

            if (KTtaoBcMau20())
            {
                trongBH = rdTrongBH.SelectedIndex;
                tungay = DungChung.Ham.NgayTu(lupTuNgay.DateTime);
                denngay = DungChung.Ham.NgayDen(lupDenNgay.DateTime);
                int _ngaytt = radTimKiem.SelectedIndex;
                List<string> _ltamung = new List<string>();
                int HTThanhToan = rdHTThanhToan.SelectedIndex;
                int ChuyenKhoan = rdCKhoan.SelectedIndex;
                if (radBaoCao.SelectedIndex == 0)
                {
                    var q = (from rv in data.RaViens
                             join bn in data.BenhNhans
                             on rv.MaBNhan equals bn.MaBNhan
                             join ttbx in data.TTboXungs.Where(p => HTThanhToan == 2 ? true : p.HTThanhToan == HTThanhToan) on bn.MaBNhan equals ttbx.MaBNhan
                             join vp in data.VienPhis on bn.MaBNhan equals vp.MaBNhan
                             join vpct in data.VienPhicts//.Where(p => (DungChung.Bien.MaBV == "30002" && _mauso == 21 && radioThuChi.SelectedIndex == 0) ? (qtu.Where(p=>p.MaBNhan == )) : true)
                             on vp.idVPhi equals vpct.idVPhi
                             where (bn.IDDTBN == _idDtuong || _idDtuong == 0)
                             where ((radTimKiem.SelectedIndex == 4) ? true : (_ngaytt == 2 ? (vp.NgayDuyet >= tungay && vp.NgayDuyet <= denngay) : (_ngaytt == 1 ? (vp.NgayTT >= tungay && vp.NgayTT <= denngay) : (_ngaytt == 3 ? (vp.NgayDuyetCP >= tungay && vp.NgayDuyetCP <= denngay) : (rv.NgayRa >= tungay && rv.NgayRa <= denngay)))))
                             where (cboNoiTinh.SelectedIndex == 0 ? true : bn.NoiTinh == cboNoiTinh.SelectedIndex)
                             where (radio_DTNT.SelectedIndex == 2 ? true : (radio_DTNT.SelectedIndex == 1 ? bn.DTNT : bn.DTNT == false))
                             where (radNoiTru.SelectedIndex == 2 ? true : bn.NoiTru == radNoiTru.SelectedIndex)
                             //  where((DungChung.Bien.MaBV == "30002" && _mauso == 21 && radioThuChi.SelectedIndex == 0) ? (qtu.Where(p=>p.MaBNhan == vp.MaBNhan).Where(p=>p.MaDV == vpct.MaDV.Value && vpct.ThanhToan == 1).Count() >0) : true)
                             select new
                             {

                                 bn.MaKCB,
                                 bn.DTNT,
                                 bn.MaBNhan,
                                 bn.DTuong,
                                 MaDTuong = bn.MaDTuong == null ? "" : (bn.MaDTuong.Trim() == "" ? "DV" : bn.MaDTuong.Trim().ToUpper()),
                                 bn.IDDTBN,
                                 bn.NoiTru,
                                 bn.TuyenDuoi,
                                 vpct.TrongBH,
                                 bn.TenBNhan,
                                 bn.SThe,
                                 MaKP = vpct.MaKP == null ? 0 : vpct.MaKP,
                                 rv.NgayRa,
                                 rv.MaICD,
                                 vp.NgayTT,
                                 vpct.DonGia,
                                 vpct.MaDV,
                                 vpct.SoLuong,
                                 vpct.ThanhTien,
                                 vpct.TienBH,
                                 vpct.TienBN,
                                 vpct.IDTamUng,
                                 vp.NgayDuyet,
                                 vpct.TyLeTT,
                                 vpct.XHH,
                                 vpct.LoaiDV,
                                 vpct.TBNCTT,
                                 vpct.TBNTT
                             }).ToList();
                    var q2 = (from dv in _lDichVu
                              join vp in q.Where(p => trongBH == 3 ? true : p.TrongBH == trongBH) on dv equals vp.MaDV
                              join dt in lmaDtuong on vp.MaDTuong equals dt
                              join dv1 in _listDV on dv equals dv1.MaDV
                              join tn in _listTieuNhom on dv1.IdTieuNhom equals tn.IdTieuNhom
                              select new
                              {
                                  dv1.TenDV,
                                  vp.MaDV,
                                  vp.MaBNhan,
                                  vp.TenBNhan,
                                  vp.SThe,
                                  vp.MaKCB,
                                  NgayTT = vp.NgayTT.Value.ToShortDateString(),
                                  vp.MaKP,
                                  vp.DonGia,
                                  vp.TyLeTT,
                                  vp.ThanhTien,
                                  vp.SoLuong,
                                  vp.TienBH,
                                  vp.TienBN,
                                  tn.IdTieuNhom,
                                  tn.TenTN,
                                  vp.TBNCTT,
                                  vp.TBNTT
                              }).Distinct().ToList();
                    var q3 = (from vp in q2
                              join k in _listKP.Where(p => kp == 0 ? true : _lMaKhoa.Contains(p.MaKP)) on vp.MaKP equals k.MaKP
                              group new { vp, k } by new
                              {
                                  vp.TenDV,
                                  vp.TenBNhan,
                                  vp.MaBNhan,
                                  vp.SThe,
                                  vp.NgayTT,
                                  vp.MaKP,
                                  vp.DonGia,
                                  vp.MaDV,
                                  vp.MaKCB,
                                  k.TenKP,
                                  vp.TyLeTT,
                                  vp.IdTieuNhom,
                                  vp.TenTN
                              } into kq
                              select new
                              {
                                  kq.Key.TenDV,
                                  kq.Key.MaDV,
                                  kq.Key.MaBNhan,
                                  kq.Key.TenBNhan,
                                  kq.Key.SThe,
                                  kq.Key.MaKCB,
                                  kq.Key.NgayTT,
                                  kq.Key.MaKP,
                                  kq.Key.DonGia,
                                  kq.Key.TyLeTT,
                                  kq.Key.TenTN,
                                  kq.Key.IdTieuNhom,
                                  ThanhTien = kq.Sum(p => _CP_BH == 0 ? p.vp.ThanhTien : ((ckTLBH.Checked ? p.vp.TienBH : 0) + (ckTLBN.Checked ? p.vp.TBNTT : 0) + (ckc_BNCCT.Checked ? p.vp.TBNCTT : 0))),// _CP_BH == 0 ? kq.Sum(p => p.vp.ThanhTien) : (_CP_BH == 2 ? kq.Sum(p => p.vp.TienBH) : kq.Sum(p => p.vp.TienBN)),
                                  SoLuong = kq.Sum(p => p.vp.SoLuong),
                                  kq.Key.TenKP,
                                  TienBH = kq.Sum(p => (_CP_BH == 0 ? p.vp.TienBH : (ckTLBH.Checked ? p.vp.TienBH : 0))),//kq.Sum(p => (_CP_BH == 0 ? p.vp.TienBN : (ckTLBN.Checked ? p.vp.TBNTT : 0) + (ckc_BNCCT.Checked ? p.vp.TBNCTT : 0))),//p.vp.TienBH),
                                  TienBN = kq.Sum(p => (_CP_BH == 0 ? p.vp.TienBN : (ckTLBN.Checked ? p.vp.TBNTT : 0) + (ckc_BNCCT.Checked ? p.vp.TBNCTT : 0)))//kq.Sum(p => (_CP_BH == 0 ? p.vp.TienBH : (ckTLBH.Checked ? p.vp.TienBH : 0)))//p.vp.TienBN)
                              }).ToList();
                    if (q3.Count > 0)
                    {
                        //if (DungChung.Bien.MaBV != "26007" && DungChung.Bien.MaBV != "26007")
                        //{
                        //    frmIn frm = new frmIn();
                        //    BaoCao.Rep_TKChiPhiTheoDV rep = new BaoCao.Rep_TKChiPhiTheoDV();
                        //    rep.CQCQ.Value = DungChung.Bien.TenCQCQ.ToUpper();
                        //    rep.TenDV.Value = DungChung.Bien.TenCQ.ToUpper();
                        //    rep.Ngaythang.Value = "Từ ngày " + tungay.ToShortDateString() + " đến ngày " + denngay.ToShortDateString();
                        //    rep.TieuDe.Value = "THỐNG KÊ CHI TIẾT CHI PHÍ THANH TOÁN" + (_idDtuong == 0 ? "" : (_idDtuong == 1 ? " BHYT" : (_idDtuong == 2 ? " DỊCH VỤ" : " KHÁM SỨC KHỎE")));
                        //    rep.DataSource = q3.ToList();
                        //    rep.BindingData();
                        //    rep.CreateDocument();
                        //    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                        //    frm.ShowDialog();

                        //    var qq = (from a in q3
                        //              group a by new { a.IdTieuNhom, a.TenTN } into kq
                        //              select new
                        //              {
                        //                  kq.Key.IdTieuNhom,
                        //                  kq.Key.TenTN,
                        //                  SoLuongT = kq.Sum(p => p.SoLuong),
                        //                  TTT = kq.Sum(p => p.ThanhTien)
                        //              }).ToList();
                        //    frmIn frm1 = new frmIn();
                        //    BaoCao.Rep_TKChiPhiTheoDV_THTN rep1 = new BaoCao.Rep_TKChiPhiTheoDV_THTN();
                        //    rep1.CQCQ.Value = DungChung.Bien.TenCQCQ.ToUpper();
                        //    rep1.TenBV.Value = DungChung.Bien.TenCQ.ToUpper();
                        //    rep1.NgayThang.Value = "Từ ngày " + tungay.ToShortDateString() + " đến ngày " + denngay.ToShortDateString();
                        //    rep1.TieuDe.Value = "THỐNG KÊ TỔNG HỢP CHI PHÍ THANH TOÁN" + (_idDtuong == 0 ? "" : (_idDtuong == 1 ? " BHYT" : (_idDtuong == 2 ? " DỊCH VỤ" : " KHÁM SỨC KHỎE")));
                        //    rep1.DataSource = qq.ToList();
                        //    rep1.BinDingdata();
                        //    rep1.CreateDocument();
                        //    frm1.prcIN.PrintingSystem = rep1.PrintingSystem;
                        //    frm1.ShowDialog();

                        //}
                        //else
                        //{
                        frmIn frm = new frmIn();
                        BaoCao.Rep_TKChiPhiTheoDV_26007 rep = new BaoCao.Rep_TKChiPhiTheoDV_26007();
                        rep.CQCQ.Value = DungChung.Bien.TenCQCQ.ToUpper();
                        rep.TenDV.Value = DungChung.Bien.TenCQ.ToUpper();
                        rep.Ngaythang.Value = "Từ ngày " + tungay.ToShortDateString() + " đến ngày " + denngay.ToShortDateString();
                        rep.TieuDe.Value = "THỐNG KÊ CHI TIẾT CHI PHÍ THANH TOÁN" + (_idDtuong == 0 ? "" : (_idDtuong == 1 ? " BHYT" : (_idDtuong == 2 ? " DỊCH VỤ" : " KHÁM SỨC KHỎE")));
                        rep.DataSource = q3.ToList();
                        rep.BindingData();
                        rep.CreateDocument();
                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                        frm.ShowDialog();
                        //}
                    }
                    else
                    {
                        MessageBox.Show("Không có dữ liệu!");
                    }
                }
                else if (radBaoCao.SelectedIndex == 1)
                {
                    var q = (from rv in data.RaViens
                             join bn in data.BenhNhans on rv.MaBNhan equals bn.MaBNhan//.Where(p=>p.MaBNhan==195887)
                             join ttbx in data.TTboXungs.Where(p => HTThanhToan == 2 ? true : p.HTThanhToan == HTThanhToan) on bn.MaBNhan equals ttbx.MaBNhan
                             join vp in data.VienPhis on bn.MaBNhan equals vp.MaBNhan
                             join dt in data.DThuocs on bn.MaBNhan equals dt.MaBNhan
                             join dtct in data.DThuoccts.Where(p => trongBH == 3 ? true : p.TrongBH == trongBH) on dt.IDDon equals dtct.IDDon//.Where(p=>p.MaDV==754)
                             join cd in data.ChiDinhs on dtct.IDCD equals cd.IDCD into kq
                             from k in kq.DefaultIfEmpty()
                             where (bn.IDDTBN == _idDtuong || _idDtuong == 0)
                             where ((radTimKiem.SelectedIndex == 4) ? true : (_ngaytt == 2 ? (vp.NgayDuyet >= tungay && vp.NgayDuyet <= denngay) : (_ngaytt == 1 ? (vp.NgayTT >= tungay && vp.NgayTT <= denngay) : (_ngaytt == 3 ? (vp.NgayDuyetCP >= tungay && vp.NgayDuyetCP <= denngay) : (rv.NgayRa >= tungay && rv.NgayRa <= denngay)))))
                             // where ((radTimKiem.SelectedIndex == 4) ? true : (_ngaytt == 2 ? (vp.NgayDuyet >= tungay && vp.NgayDuyet <= denngay) : (_ngaytt == 1 ? (vp.NgayTT >= tungay && vp.NgayTT <= denngay) : (_ngaytt == 3 ? (vp.NgayDuyetCP >= tungay && vp.NgayDuyetCP <= denngay) : (rv.NgayRa >= tungay && rv.NgayRa <= denngay)))))
                             where (cboNoiTinh.SelectedIndex == 0 ? true : bn.NoiTinh == cboNoiTinh.SelectedIndex)
                             where (radio_DTNT.SelectedIndex == 2 ? true : (radio_DTNT.SelectedIndex == 1 ? bn.DTNT : bn.DTNT == false))
                             where (radNoiTru.SelectedIndex == 2 ? true : bn.NoiTru == radNoiTru.SelectedIndex)
                             //  where((DungChung.Bien.MaBV == "30002" && _mauso == 21 && radioThuChi.SelectedIndex == 0) ? (qtu.Where(p=>p.MaBNhan == vp.MaBNhan).Where(p=>p.MaDV == vpct.MaDV.Value && vpct.ThanhToan == 1).Count() >0) : true)
                             select new
                             {

                                 bn.MaKCB,
                                 bn.DTNT,
                                 bn.MaBNhan,
                                 bn.DTuong,
                                 MaDTuong = bn.MaDTuong == null ? "" : (bn.MaDTuong.Trim() == "" ? "DV" : bn.MaDTuong.Trim().ToUpper()),
                                 bn.IDDTBN,
                                 bn.NoiTru,
                                 bn.TuyenDuoi,
                                 dtct.TrongBH,
                                 bn.TenBNhan,
                                 bn.SThe,
                                 MaKP = dtct.MaKP == null ? 0 : dtct.MaKP,
                                 rv.NgayRa,
                                 rv.MaICD,
                                 vp.NgayTT,
                                 dtct.DonGia,
                                 dtct.MaDV,
                                 dtct.SoLuong,
                                 dtct.ThanhTien,
                                 dtct.TienBH,
                                 dtct.TienBN,
                                 PTVPhu = k != null ? k.DSCBTH : null,
                                 vp.NgayDuyet,
                                 dtct.TyLeTT,
                                 dtct.XHH,
                                 dtct.LoaiDV,
                                 NgayTH = k != null ? k.NgayTH : dtct.NgayNhap,
                                 MaCBth = dtct.MaCB,
                             }).ToList();
                    var q2 = (from dv in _lDichVu
                              join vp in q on dv equals vp.MaDV
                              join dt in lmaDtuong on vp.MaDTuong equals dt
                              join dv1 in _listDV on dv equals dv1.MaDV
                              join cb in data.CanBoes on vp.MaCBth equals cb.MaCB
                              select new
                              {
                                  dv1.TenDV,
                                  vp.MaDV,
                                  vp.MaBNhan,
                                  vp.TenBNhan,
                                  vp.SThe,
                                  vp.MaKCB,
                                  NgayTT = vp.NgayTT.Value.ToShortDateString(),
                                  vp.MaKP,
                                  vp.DonGia,
                                  vp.TyLeTT,
                                  vp.ThanhTien,
                                  vp.SoLuong,
                                  NgayTH = vp.NgayTH.Value.ToShortDateString(),
                                  cb.TenCB,
                                  dv1.Loai,
                                  vp.PTVPhu,
                                  dv1.MaQD
                              }).ToList();
                    var q3 = (from vp in q2
                              join k in _listKP.Where(p => kp == 0 ? true : _lMaKhoa.Contains(p.MaKP)) on vp.MaKP equals k.MaKP
                              group new { vp, k } by new
                              {
                                  vp.TenDV,
                                  vp.TenBNhan,
                                  vp.MaBNhan,
                                  vp.SThe,
                                  vp.NgayTT,
                                  vp.MaKP,
                                  vp.DonGia,
                                  vp.MaDV,
                                  vp.MaKCB,
                                  k.TenKP,
                                  vp.TyLeTT,
                                  vp.NgayTH,
                                  vp.TenCB,
                                  vp.Loai,
                                  vp.PTVPhu,
                                  vp.MaQD
                              } into kq
                              select new
                              {
                                  kq.Key.TenDV,
                                  kq.Key.MaDV,
                                  kq.Key.MaBNhan,
                                  kq.Key.TenBNhan,
                                  kq.Key.SThe,
                                  kq.Key.MaKCB,
                                  kq.Key.NgayTT,
                                  kq.Key.MaKP,
                                  kq.Key.DonGia,
                                  kq.Key.TyLeTT,
                                  kq.Key.NgayTH,
                                  kq.Key.TenCB,
                                  Loai = kq.Key.Loai == 0 ? "ĐB" : (kq.Key.Loai == 1 ? "I" : (kq.Key.Loai == 2 ? "II" : (kq.Key.Loai == 3 ? "III" : kq.Key.Loai.ToString()))),
                                  ThanhTien = kq.Sum(p => p.vp.ThanhTien),
                                  SoLuong = kq.Sum(p => p.vp.SoLuong),
                                  kq.Key.TenKP,
                                  kq.Key.PTVPhu,
                                  kq.Key.MaQD
                              }).ToList();
                    if (q3.Count > 0)
                    {
                        if (DungChung.Bien.MaBV == "26007")
                        {
                            frmIn frm = new frmIn();
                            BaoCao.rep_DSBNPTTT_26007 rep = new BaoCao.rep_DSBNPTTT_26007();
                            rep.Ngay.Text = "Từ ngày " + tungay.ToShortDateString() + " đến ngày " + denngay.ToShortDateString();
                            rep.DataSource = q3.ToList();
                            rep.count.Value = q3.Count;
                            rep.BindingData();
                            rep.CreateDocument();
                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                            frm.ShowDialog();
                        }
                        else
                        {
                            frmIn frm = new frmIn();
                            BaoCao.rep_DSBNPTTT rep = new BaoCao.rep_DSBNPTTT();
                            rep.Ngay.Text = "Từ ngày " + tungay.ToShortDateString() + " đến ngày " + denngay.ToShortDateString();
                            rep.DataSource = q3.ToList();
                            rep.BindingData();
                            rep.CreateDocument();
                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                            frm.ShowDialog();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Không có dữ liệu!");
                    }
                }
                else
                {
                    var q = (from rv in data.RaViens
                             join bn in data.BenhNhans on rv.MaBNhan equals bn.MaBNhan
                             join ttbx in data.TTboXungs.Where(p => HTThanhToan == 2 ? true : p.HTThanhToan == HTThanhToan) on bn.MaBNhan equals ttbx.MaBNhan
                             join vp in data.VienPhis on bn.MaBNhan equals vp.MaBNhan
                             join vpct in data.VienPhicts on vp.idVPhi equals vpct.idVPhi
                             where (bn.IDDTBN == _idDtuong || _idDtuong == 0)
                             where ((radTimKiem.SelectedIndex == 4) ? true : (_ngaytt == 2 ? (vp.NgayDuyet >= tungay && vp.NgayDuyet <= denngay) : (_ngaytt == 1 ? (vp.NgayTT >= tungay && vp.NgayTT <= denngay) : (_ngaytt == 3 ? (vp.NgayDuyetCP >= tungay && vp.NgayDuyetCP <= denngay) : (rv.NgayRa >= tungay && rv.NgayRa <= denngay)))))
                             where (cboNoiTinh.SelectedIndex == 0 ? true : bn.NoiTinh == cboNoiTinh.SelectedIndex)
                             where (radio_DTNT.SelectedIndex == 2 ? true : (radio_DTNT.SelectedIndex == 1 ? bn.DTNT : bn.DTNT == false))
                             where (radNoiTru.SelectedIndex == 2 ? true : bn.NoiTru == radNoiTru.SelectedIndex)
                             select new
                             {

                                 bn.MaKCB,
                                 bn.DTNT,
                                 bn.MaBNhan,
                                 bn.DTuong,
                                 MaDTuong = bn.MaDTuong == null ? "" : (bn.MaDTuong.Trim() == "" ? "DV" : bn.MaDTuong.Trim().ToUpper()),
                                 bn.IDDTBN,
                                 bn.NoiTru,
                                 bn.TuyenDuoi,
                                 vpct.TrongBH,
                                 bn.TenBNhan,
                                 bn.SThe,
                                 MaKP = vpct.MaKP == null ? 0 : vpct.MaKP,
                                 rv.NgayRa,
                                 rv.MaICD,
                                 vp.NgayTT,
                                 vpct.DonGia,
                                 vpct.MaDV,
                                 vpct.SoLuong,
                                 vpct.ThanhTien,
                                 vpct.TienBH,
                                 vpct.TienBN,
                                 //vpct.IDTamUng,
                                 vp.NgayDuyet,
                                 vpct.TyLeTT,
                                 vpct.XHH,
                                 vpct.LoaiDV
                             }).ToList();
                    var q2 = (from dv in _lDichVu
                              join vp in q.Where(p => trongBH == 3 ? true : p.TrongBH == trongBH) on dv equals vp.MaDV
                              join dt in lmaDtuong on vp.MaDTuong equals dt
                              join dv1 in _listDV on dv equals dv1.MaDV
                              select new
                              {
                                  dv1.TenDV,
                                  vp.MaDV,
                                  vp.MaBNhan,
                                  vp.TenBNhan,
                                  vp.SThe,
                                  vp.MaKCB,
                                  NgayTT = vp.NgayTT.Value.ToShortDateString(),
                                  vp.MaKP,
                                  vp.DonGia,
                                  vp.TyLeTT,
                                  vp.ThanhTien,
                                  vp.SoLuong,
                              }).Distinct().ToList();
                    var q3 = (from vp in q2
                              join k in _listKP.Where(p => kp == 0 ? true : _lMaKhoa.Contains(p.MaKP)) on vp.MaKP equals k.MaKP
                              group new { vp, k } by new
                              {
                                  vp.TenDV,
                                  vp.TenBNhan,
                                  vp.MaBNhan,
                                  vp.SThe,
                                  vp.NgayTT,
                                  vp.MaKP,
                                  vp.DonGia,
                                  vp.MaDV,
                                  vp.MaKCB,
                                  k.TenKP,
                                  vp.TyLeTT,
                              } into kq
                              select new
                              {
                                  kq.Key.TenDV,
                                  kq.Key.MaDV,
                                  kq.Key.MaBNhan,
                                  kq.Key.TenBNhan,
                                  kq.Key.SThe,
                                  kq.Key.MaKCB,
                                  kq.Key.NgayTT,
                                  kq.Key.MaKP,
                                  kq.Key.DonGia,
                                  kq.Key.TyLeTT,
                                  ThanhTien = kq.Sum(p => p.vp.ThanhTien),
                                  SoLuong = kq.Sum(p => p.vp.SoLuong),
                                  kq.Key.TenKP
                              }).ToList();

                    if (q3.Count > 0)
                    {
                        var kp1 = (from a in _listKP.Where(p => _lMaKhoa.Contains(p.MaKP))
                                   select new { a.TenKP, a.MaKP }).OrderByDescending(p => p.TenKP).ToList();
                        int x1 = 0, x2 = 0, x3 = 0, x4 = 0, x5 = 0, x6 = 0, x7 = 0, x8 = 0, x9 = 0, x10 = 0, x11 = 0, x12 = 0, x13 = 0, x14 = 0, x15 = 0;
                        string xx1 = "", xx2 = "", xx3 = "", xx4 = "", xx5 = "", xx6 = "", xx7 = "", xx8 = "", xx9 = "", xx10 = "", xx11 = "", xx12 = "", xx13 = "", xx14 = "", xx15 = "";
                        if (kp1.Count() > 0)
                        {
                            x1 = kp1.Count >= 1 ? kp1.Skip(0).FirstOrDefault().MaKP : 0;
                            x2 = kp1.Count >= 2 ? kp1.Skip(1).FirstOrDefault().MaKP : 0;
                            x3 = kp1.Count >= 3 ? kp1.Skip(2).FirstOrDefault().MaKP : 0;
                            x4 = kp1.Count >= 4 ? kp1.Skip(3).FirstOrDefault().MaKP : 0;
                            x5 = kp1.Count >= 5 ? kp1.Skip(4).FirstOrDefault().MaKP : 0;
                            x6 = kp1.Count >= 6 ? kp1.Skip(5).FirstOrDefault().MaKP : 0;
                            x7 = kp1.Count >= 7 ? kp1.Skip(6).FirstOrDefault().MaKP : 0;
                            x8 = kp1.Count >= 8 ? kp1.Skip(7).FirstOrDefault().MaKP : 0;
                            x9 = kp1.Count >= 9 ? kp1.Skip(8).FirstOrDefault().MaKP : 0;
                            x10 = kp1.Count >= 10 ? kp1.Skip(9).FirstOrDefault().MaKP : 0;
                            x11 = kp1.Count >= 11 ? kp1.Skip(10).FirstOrDefault().MaKP : 0;
                            x12 = kp1.Count >= 12 ? kp1.Skip(11).FirstOrDefault().MaKP : 0;
                            x13 = kp1.Count >= 13 ? kp1.Skip(12).FirstOrDefault().MaKP : 0;
                            x14 = kp1.Count >= 14 ? kp1.Skip(13).FirstOrDefault().MaKP : 0;
                            x15 = kp1.Count >= 15 ? kp1.Skip(14).FirstOrDefault().MaKP : 0;

                            xx1 = kp1.Count >= 1 ? kp1.Skip(0).FirstOrDefault().TenKP : "";
                            xx2 = kp1.Count >= 2 ? kp1.Skip(1).FirstOrDefault().TenKP : "";
                            xx3 = kp1.Count >= 3 ? kp1.Skip(2).FirstOrDefault().TenKP : "";
                            xx4 = kp1.Count >= 4 ? kp1.Skip(3).FirstOrDefault().TenKP : "";
                            xx5 = kp1.Count >= 5 ? kp1.Skip(4).FirstOrDefault().TenKP : "";
                            xx6 = kp1.Count >= 6 ? kp1.Skip(5).FirstOrDefault().TenKP : "";
                            xx7 = kp1.Count >= 7 ? kp1.Skip(6).FirstOrDefault().TenKP : "";
                            xx8 = kp1.Count >= 8 ? kp1.Skip(7).FirstOrDefault().TenKP : "";
                            xx9 = kp1.Count >= 9 ? kp1.Skip(8).FirstOrDefault().TenKP : "";
                            xx10 = kp1.Count >= 10 ? kp1.Skip(9).FirstOrDefault().TenKP : "";
                            xx11 = kp1.Count >= 11 ? kp1.Skip(10).FirstOrDefault().TenKP : "";
                            xx12 = kp1.Count >= 12 ? kp1.Skip(11).FirstOrDefault().TenKP : "";
                            xx13 = kp1.Count >= 13 ? kp1.Skip(12).FirstOrDefault().TenKP : "";
                            xx14 = kp1.Count >= 14 ? kp1.Skip(13).FirstOrDefault().TenKP : "";
                            xx15 = kp1.Count >= 15 ? kp1.Skip(14).FirstOrDefault().TenKP : "";

                            List<BangKe> ds = new List<BangKe>();
                            var q4 = (from a in q3
                                      group a by new
                                      {
                                          a.TenDV,
                                          a.MaDV,
                                          a.DonGia
                                      } into kq
                                      select new BangKe
                                      {
                                          TenDV = kq.Key.TenDV,
                                          DonGia = kq.Key.DonGia,
                                          SLKP1 = kq.Where(p => p.MaKP == x1).Sum(p => p.SoLuong),
                                          SLKP2 = kq.Where(p => p.MaKP == x2).Sum(p => p.SoLuong),
                                          SLKP3 = kq.Where(p => p.MaKP == x3).Sum(p => p.SoLuong),
                                          SLKP4 = kq.Where(p => p.MaKP == x4).Sum(p => p.SoLuong),
                                          SLKP5 = kq.Where(p => p.MaKP == x5).Sum(p => p.SoLuong),
                                          SLKP6 = kq.Where(p => p.MaKP == x6).Sum(p => p.SoLuong),
                                          SLKP7 = kq.Where(p => p.MaKP == x7).Sum(p => p.SoLuong),
                                          SLKP8 = kq.Where(p => p.MaKP == x8).Sum(p => p.SoLuong),
                                          SLKP9 = kq.Where(p => p.MaKP == x9).Sum(p => p.SoLuong),
                                          SLKP10 = kq.Where(p => p.MaKP == x10).Sum(p => p.SoLuong),
                                          SLKP11 = kq.Where(p => p.MaKP == x11).Sum(p => p.SoLuong),
                                          SLKP12 = kq.Where(p => p.MaKP == x12).Sum(p => p.SoLuong),
                                          SLKP13 = kq.Where(p => p.MaKP == x13).Sum(p => p.SoLuong),
                                          SLKP14 = kq.Where(p => p.MaKP == x14).Sum(p => p.SoLuong),
                                          SLKP15 = kq.Where(p => p.MaKP == x15).Sum(p => p.SoLuong),
                                          ThanhTien = kq.Where(p => p.MaKP == x1 || p.MaKP == x2 || p.MaKP == x3 || p.MaKP == x4 || p.MaKP == x5 || p.MaKP == x6 || p.MaKP == x7 || p.MaKP == x8 || p.MaKP == x9 || p.MaKP == x10 || p.MaKP == x11 || p.MaKP == x12 || p.MaKP == x13 || p.MaKP == x14 || p.MaKP == x15).Sum(p => p.ThanhTien),
                                          Tong = kq.Where(p => p.MaKP == x1 || p.MaKP == x2 || p.MaKP == x3 || p.MaKP == x4 || p.MaKP == x5 || p.MaKP == x6 || p.MaKP == x7 || p.MaKP == x8 || p.MaKP == x9 || p.MaKP == x10 || p.MaKP == x11 || p.MaKP == x12 || p.MaKP == x13 || p.MaKP == x14 || p.MaKP == x15).Sum(p => p.SoLuong),
                                      }).ToList();

                            frmIn frm = new frmIn();
                            BaoCao.rep_BangKeChiTetDVKP rep = new BaoCao.rep_BangKeChiTetDVKP();
                            rep.Khoa1.Value = xx1;
                            rep.Khoa2.Value = xx2;
                            rep.Khoa3.Value = xx3;
                            rep.Khoa4.Value = xx4;
                            rep.Khoa5.Value = xx5;
                            rep.Khoa6.Value = xx6;
                            rep.Khoa7.Value = xx7;
                            rep.Khoa8.Value = xx8;
                            rep.Khoa9.Value = xx9;
                            rep.Khoa10.Value = xx10;
                            rep.Khoa11.Value = xx11;
                            rep.Khoa12.Value = xx12;
                            rep.Khoa13.Value = xx13;
                            rep.Khoa14.Value = xx14;
                            rep.Khoa15.Value = xx15;
                            rep.Ngay.Text = "Từ ngày " + tungay.ToShortDateString() + " đến ngày " + denngay.ToShortDateString();
                            rep.NgayThang.Value = "Ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;
                            rep.DataSource = q4.OrderBy(p => p.TenDV).ToList();
                            rep.BindingData();
                            rep.CreateDocument();
                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                            frm.ShowDialog();
                        }
                        if (kp1.Count() > 15)
                        {
                            x1 = kp1.Count >= 16 ? kp1.Skip(15).FirstOrDefault().MaKP : 0;
                            x2 = kp1.Count >= 17 ? kp1.Skip(16).FirstOrDefault().MaKP : 0;
                            x3 = kp1.Count >= 18 ? kp1.Skip(17).FirstOrDefault().MaKP : 0;
                            x4 = kp1.Count >= 19 ? kp1.Skip(18).FirstOrDefault().MaKP : 0;
                            x5 = kp1.Count >= 20 ? kp1.Skip(19).FirstOrDefault().MaKP : 0;
                            x6 = kp1.Count >= 21 ? kp1.Skip(20).FirstOrDefault().MaKP : 0;
                            x7 = kp1.Count >= 22 ? kp1.Skip(21).FirstOrDefault().MaKP : 0;
                            x8 = kp1.Count >= 23 ? kp1.Skip(22).FirstOrDefault().MaKP : 0;
                            x9 = kp1.Count >= 24 ? kp1.Skip(23).FirstOrDefault().MaKP : 0;
                            x10 = kp1.Count >= 25 ? kp1.Skip(24).FirstOrDefault().MaKP : 0;
                            x11 = kp1.Count >= 26 ? kp1.Skip(25).FirstOrDefault().MaKP : 0;
                            x12 = kp1.Count >= 27 ? kp1.Skip(26).FirstOrDefault().MaKP : 0;
                            x13 = kp1.Count >= 28 ? kp1.Skip(27).FirstOrDefault().MaKP : 0;
                            x14 = kp1.Count >= 29 ? kp1.Skip(28).FirstOrDefault().MaKP : 0;
                            x15 = kp1.Count >= 30 ? kp1.Skip(29).FirstOrDefault().MaKP : 0;

                            xx1 = kp1.Count >= 16 ? kp1.Skip(15).FirstOrDefault().TenKP : "";
                            xx2 = kp1.Count >= 17 ? kp1.Skip(16).FirstOrDefault().TenKP : "";
                            xx3 = kp1.Count >= 18 ? kp1.Skip(17).FirstOrDefault().TenKP : "";
                            xx4 = kp1.Count >= 19 ? kp1.Skip(18).FirstOrDefault().TenKP : "";
                            xx5 = kp1.Count >= 20 ? kp1.Skip(19).FirstOrDefault().TenKP : "";
                            xx6 = kp1.Count >= 21 ? kp1.Skip(20).FirstOrDefault().TenKP : "";
                            xx7 = kp1.Count >= 22 ? kp1.Skip(21).FirstOrDefault().TenKP : "";
                            xx8 = kp1.Count >= 23 ? kp1.Skip(22).FirstOrDefault().TenKP : "";
                            xx9 = kp1.Count >= 24 ? kp1.Skip(23).FirstOrDefault().TenKP : "";
                            xx10 = kp1.Count >= 25 ? kp1.Skip(24).FirstOrDefault().TenKP : "";
                            xx11 = kp1.Count >= 26 ? kp1.Skip(25).FirstOrDefault().TenKP : "";
                            xx12 = kp1.Count >= 27 ? kp1.Skip(26).FirstOrDefault().TenKP : "";
                            xx13 = kp1.Count >= 28 ? kp1.Skip(27).FirstOrDefault().TenKP : "";
                            xx14 = kp1.Count >= 29 ? kp1.Skip(28).FirstOrDefault().TenKP : "";
                            xx15 = kp1.Count >= 30 ? kp1.Skip(29).FirstOrDefault().TenKP : "";

                            List<BangKe> ds = new List<BangKe>();
                            var q4 = (from a in q3
                                      group a by new
                                      {
                                          a.TenDV,
                                          a.MaDV,
                                          a.DonGia
                                      } into kq
                                      select new BangKe
                                      {
                                          TenDV = kq.Key.TenDV,
                                          DonGia = kq.Key.DonGia,
                                          SLKP1 = kq.Where(p => p.MaKP == x1).Sum(p => p.SoLuong),
                                          SLKP2 = kq.Where(p => p.MaKP == x2).Sum(p => p.SoLuong),
                                          SLKP3 = kq.Where(p => p.MaKP == x3).Sum(p => p.SoLuong),
                                          SLKP4 = kq.Where(p => p.MaKP == x4).Sum(p => p.SoLuong),
                                          SLKP5 = kq.Where(p => p.MaKP == x5).Sum(p => p.SoLuong),
                                          SLKP6 = kq.Where(p => p.MaKP == x6).Sum(p => p.SoLuong),
                                          SLKP7 = kq.Where(p => p.MaKP == x7).Sum(p => p.SoLuong),
                                          SLKP8 = kq.Where(p => p.MaKP == x8).Sum(p => p.SoLuong),
                                          SLKP9 = kq.Where(p => p.MaKP == x9).Sum(p => p.SoLuong),
                                          SLKP10 = kq.Where(p => p.MaKP == x10).Sum(p => p.SoLuong),
                                          SLKP11 = kq.Where(p => p.MaKP == x11).Sum(p => p.SoLuong),
                                          SLKP12 = kq.Where(p => p.MaKP == x12).Sum(p => p.SoLuong),
                                          SLKP13 = kq.Where(p => p.MaKP == x13).Sum(p => p.SoLuong),
                                          SLKP14 = kq.Where(p => p.MaKP == x14).Sum(p => p.SoLuong),
                                          SLKP15 = kq.Where(p => p.MaKP == x15).Sum(p => p.SoLuong),
                                          ThanhTien = kq.Where(p => p.MaKP == x1 || p.MaKP == x2 || p.MaKP == x3 || p.MaKP == x4 || p.MaKP == x5 || p.MaKP == x6 || p.MaKP == x7 || p.MaKP == x8 || p.MaKP == x9 || p.MaKP == x10 || p.MaKP == x11 || p.MaKP == x12 || p.MaKP == x13 || p.MaKP == x14 || p.MaKP == x15).Sum(p => p.ThanhTien),
                                          Tong = kq.Where(p => p.MaKP == x1 || p.MaKP == x2 || p.MaKP == x3 || p.MaKP == x4 || p.MaKP == x5 || p.MaKP == x6 || p.MaKP == x7 || p.MaKP == x8 || p.MaKP == x9 || p.MaKP == x10 || p.MaKP == x11 || p.MaKP == x12 || p.MaKP == x13 || p.MaKP == x14 || p.MaKP == x15).Sum(p => p.SoLuong),
                                      }).ToList();

                            frmIn frm = new frmIn();
                            BaoCao.rep_BangKeChiTetDVKP rep = new BaoCao.rep_BangKeChiTetDVKP();
                            rep.Khoa1.Value = xx1;
                            rep.Khoa2.Value = xx2;
                            rep.Khoa3.Value = xx3;
                            rep.Khoa4.Value = xx4;
                            rep.Khoa5.Value = xx5;
                            rep.Khoa6.Value = xx6;
                            rep.Khoa7.Value = xx7;
                            rep.Khoa8.Value = xx8;
                            rep.Khoa9.Value = xx9;
                            rep.Khoa10.Value = xx10;
                            rep.Khoa11.Value = xx11;
                            rep.Khoa12.Value = xx12;
                            rep.Khoa13.Value = xx13;
                            rep.Khoa14.Value = xx14;
                            rep.Khoa15.Value = xx15;
                            rep.Ngay.Text = "Từ ngày " + tungay.ToShortDateString() + " đến ngày " + denngay.ToShortDateString();
                            rep.NgayThang.Value = "Ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;
                            rep.DataSource = q4.OrderBy(p => p.TenDV).ToList();
                            rep.BindingData();
                            rep.CreateDocument();
                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                            frm.ShowDialog();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Không có dữ liệu!");
                    }
                }

            }
            #endregion biến
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cklMaDTuong_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            if (e.Index == 0)
            {
                if (cklMaDTuong.GetItemChecked(0) == true)
                    cklMaDTuong.CheckAll();
                else
                    cklMaDTuong.UnCheckAll();
            }
        }

        private void txtTimDV_Leave(object sender, EventArgs e)
        {

        }

        private void txtTimDV_EditValueChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtTimDV.Text))
            {
                try
                {
                    int Madv = Convert.ToInt32(txtTimDV.Text);
                    ckl_DichVu.DataSource = _ldv.Where(p => p.MaDV == Madv).ToList();
                }
                catch
                {
                    string Tendv = txtTimDV.Text.ToLower().Trim();
                    ckl_DichVu.DataSource = _ldv.Where(p => p.TenDV.ToLower().Contains(Tendv)).ToList();
                }
            }
            else
                ckl_DichVu.DataSource = _ldv;
        }

        private void ckBHTT_CheckedChanged(object sender, EventArgs e)
        {
            //if (ckBHTT.Checked)
            //    ckBNTT.Checked = false;
        }

        private void ckBNTT_CheckedChanged(object sender, EventArgs e)
        {
            //if (ckBNTT.Checked)
            //    ckBHTT.Checked = false;
        }

        private void llblSelectAll_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ckcTieuNhomDV.CheckAll();
            ckcTieuNhomDV_ItemCheck(null, null);
        }
    }
}