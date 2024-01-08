using System;
using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using QLBV.BaoCao;
using DevExpress.XtraEditors.Controls;

namespace QLBV.FormNhap
{
    public partial class frm_CVienNoiTru : DevExpress.XtraEditors.XtraForm
    {
        int _mabn = 0;
        string _capbac = "";
        QLBV_Database.QLBVEntities DataContect = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        public frm_CVienNoiTru()
        {
            InitializeComponent();
        }
        public frm_CVienNoiTru(int mabn)
        {
            _mabn = mabn;
            InitializeComponent();
        }

        List<RaVien> _chuyenvien = new List<RaVien>();

        void setSoCV(int socvnew)
        {
            int soCV = 0;
            int makp = 0;
            if (DungChung.Bien.PP_SoCV == 1)
            {
                if (lupKhoaPhong.EditValue != null && lupKhoaPhong.EditValue.ToString() != "")
                {
                    makp = Convert.ToInt32(lupKhoaPhong.EditValue);
                    soCV = DungChung.Ham.GetSoPL(5, makp, -1);
                }
                else
                {
                    MessageBox.Show("chưa chọn khoa điều trị, không lấy được số bệnh án");

                }
            }
            else if (DungChung.Bien.PP_SoCV == 2)
            {
                soCV = DungChung.Ham.GetSoPL(5, makp, -1);
            }
            soCV += socvnew;
            txtSo.Text = soCV.ToString();

        }
        public int laySoChuyenVien(DateTime ngaychuyenvien, int ngay2)
        {
            int day = ngaychuyenvien.Day;
            DataContect = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            DateTime ngayBD;
            DateTime ngayKT;
            if (day > ngay2)
            {
                ngayBD = Convert.ToDateTime(ngay2 + "/" + ngaychuyenvien.Month + "/" + ngaychuyenvien.Year);
            }
            else
            {
                ngayBD = Convert.ToDateTime(ngay2 + "/" + (ngaychuyenvien.Month - 1) + "/" + ngaychuyenvien.Year);
            }
            ngayBD = DungChung.Ham.NgayTu(ngayBD);
            ngayKT = ngayBD.AddMonths(1);
            ngayKT = DungChung.Ham.NgayTu(ngayKT);

            var q = DataContect.RaViens.ToList();
            var ngay = (from a in q
                        where a.NgayRa >= ngayBD && a.NgayRa <= ngayKT
                        select a).ToList();
            int sotrave = 0;
            if (ngay.Count > 0)
            {
                sotrave = ngay.Count;
            }

            return sotrave + 1;
        }
        bool _daco = false;
        //private int getSoChuyenVien()
        //{
        //    QLBV_Database.QLBVEntities DataContect = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        //    int _sochuyenvien = 1;
        //    // thay đổi lại cách lấy số vào viện
        //    var hthong = DataContect.HTHONGs.Where(p => p.MaBV == DungChung.Bien.MaBV).Select(p => p.SoChuyenVien).FirstOrDefault();
        //    if (hthong != null)
        //    {
        //        if (hthong == 0)
        //            return 0;
        //        _sochuyenvien += hthong;
        //    }


        //    return _sochuyenvien;

        //}
        public static double _soHdt = 0;
        public static int getDaysOfStay(DateTime tungay, DateTime denngay)
        {
            int rs = 0;

            TimeSpan ts = denngay - tungay;
            int day = (denngay.Date - tungay.Date).Days;
            _soHdt = ts.TotalHours;
            if (ts.TotalHours >= 4)
            {

                if (day == 1)
                {

                    if (ts.TotalMilliseconds <= 28800000)
                        rs = 1;
                    else if (ts.TotalMilliseconds > 28800000)
                        rs = 2;
                }
                else if (day == 0 || day > 0)
                {
                    rs = day + 1;
                }
            }
            return rs;
        }
        int songaydt = 0, _noitru = -1; bool DTNT = false;
        string _Dtuong = "";
        public class c_ICD
        {

            public string TenICD { set; get; }
            public string MaICD { set; get; }
        }
        List<c_ICD> lICD = new List<c_ICD>();
        List<ICD10> _licd10 = new List<ICD10>();
        private void frm_CVienNoiTru_Load(object sender, EventArgs e)
        {

            DataContect = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            _licd10 = DataContect.ICD10.ToList();
            if (DungChung.Bien.MaBV == "08204")
                txtSoNgaydt.Properties.ReadOnly = true;
            if (DungChung.Bien.MaBV == "01830")
                txtSo.Properties.ReadOnly = true;
            if (DungChung.Bien.MaBV == "30350")
            {
                txtChanDoan.Enabled = true;
                txtMaICD.Enabled = true;
            }
            else
            {
                txtChanDoan.Enabled = false;
                txtMaICD.Enabled = false;
            }
            var nn = DataContect.DmNNs.ToList();
            txtNgheNghiep.Properties.DataSource = nn;
            lICD = ((from ICD in DataContect.ICD10 select new c_ICD { MaICD = ICD.MaICD, TenICD = ICD.TenICD }).OrderBy(p => p.TenICD)).ToList();
            txtMaICD.Properties.DataSource = lICD;
            LupICD2.Properties.DataSource = lICD;
            LupICD3.Properties.DataSource = lICD;
            //txtChanDoan.Properties.Items.AddRange(lICD.Select(p => p.TenICD).ToArray());
            txtChanDoan.Properties.Items.AddRange(lICD);
            txtBenhKhac2.Properties.DataSource = lICD;
            txtBenhKhac3.Properties.DataSource = lICD;
            string[] arrICD = lICD.Where(p => p.TenICD != null).Select(p => p.TenICD).ToArray();
            string[] arrMaICD = lICD.Where(p => p.TenICD != null).Select(p => p.MaICD).ToArray();
            LupICD4.Properties.Items.AddRange(arrMaICD);
            txtBenhKhac4.Properties.Items.AddRange(arrICD);
            //LupICD4.Properties.DataSource = lICD;
            txtMaBNhan.Text = _mabn.ToString();
            var dt = DataContect.DanTocs.ToList();
            lupTenDT.Properties.DataSource = dt;
            var qbv_24012 = (from TK in DataContect.BenhViens.Where(p => p.status == 3 || p.status == 2 || p.status == 1) select new { TK.TenBV, TK.MaBV }).OrderBy(p => p.TenBV).ToList();
            var qbv = (from TK in DataContect.BenhViens.Where(p => p.status == 3 || p.status == 2) select new { TK.TenBV, TK.MaBV }).OrderBy(p => p.TenBV).ToList();
            if(DungChung.Bien.MaBV == "24012")
            {
                btnLuuTamThoi.Visible = true;
                lupBVC.Properties.DataSource = qbv_24012.ToList();
                lupMaBVchuyen.Properties.DataSource = qbv_24012.ToList();
            }
            else
            {
                lupBVC.Properties.DataSource = qbv.ToList();
                lupMaBVchuyen.Properties.DataSource = qbv.ToList();
            }
            var Kp = (from kb in DataContect.BNKBs.Where(p => p.MaBNhan == _mabn)
                      join kp in DataContect.KPhongs on kb.MaKP equals kp.MaKP
                      select kp).Distinct().ToList();
            lupKhoaPhong.Properties.DataSource = Kp;
            var cv = (from bn in DataContect.BenhNhans
                      where (bn.MaBNhan == _mabn)
                      join bnkb in DataContect.BNKBs on bn.MaBNhan equals bnkb.MaBNhan
                      join ttbs in DataContect.TTboXungs on bn.MaBNhan equals ttbs.MaBNhan
                      select new { bnkb.NgayNghi, bnkb.IDKB, bn.DTuong, bn.TenBNhan, bnkb.NgayKham, bn.Tuoi, bn.NoiTru, bn.DTNT, bn.MaBNhan, bn.GTinh, bn.SThe, bn.HanBHTu, bn.HanBHDen, bn.DChi, ttbs.MaNN, ttbs.MaDT }).OrderByDescending(p => p.IDKB).ToList();
            if (cv.Count > 0)
            {
                _Dtuong = cv.First().DTuong;
                DTNT = cv.First().DTNT;
                _noitru = cv.First().NoiTru ?? 0;
                txtNgayVao.DateTime = cv.First().NgayKham.Value;
                txtTenBNhan.Text = cv.First().TenBNhan;
                txtMaBNhan.Text = cv.First().MaBNhan.ToString();
                txtTuoi.Text = cv.First().Tuoi.ToString();
                lupTenDT.EditValue = cv.First().MaDT;
                if (cv.First().GTinh == 1)
                {
                    txtGTinh.Text = "Nam";
                }
                else txtGTinh.Text = "Nữ";

                txtNgheNghiep.EditValue = cv.First().MaNN;
                txtSThe.Text = cv.First().SThe;
                txtHanBHTu.Text = cv.First().HanBHTu.ToString();
                txtHanBHDen.Text = cv.First().HanBHDen.ToString();
                txtDChi.Text = cv.First().DChi;
                songaydt = getDaysOfStay(txtNgayVao.DateTime, txtNgayCV.DateTime);
                txtSoNgaydt.Text = songaydt.ToString();
            }
            var vv = DataContect.VaoViens.Where(p => p.MaBNhan == _mabn).ToList();
            if (vv.Count > 0)
                txtNgayVao.DateTime = vv.First().NgayVao.Value;
            //_chuyenvien = DataContect.RaViens.ToList();
            var crv = DataContect.RaViens.Where(p => p.MaBNhan == _mabn).Where(p => p.Status == 1).ToList();
            if (crv.Count > 0)
            {
                _daco = true;
                lupKhoaPhong.EditValue = crv.First().MaKP;
                lupBSChuyen.EditValue = crv.First().MaBS;
                txtSo.Text = crv.First().SoChuyenVien.ToString();
                txtNgayCV.DateTime = crv.First().NgayRa.Value;
                if (DungChung.Bien.MaBV == "24297")
                {
                    txtNgayCV.DateTime = System.DateTime.Now;
                }
                if (crv.First().NgayVao != null)
                    txtNgayVao.DateTime = crv.First().NgayVao.Value;
                if (crv.First().SoNgaydt != null)
                {
                    _soHdt = 24 * crv.First().SoNgaydt.Value;
                    txtSoNgaydt.Text = crv.First().SoNgaydt.ToString();
                }

                cboLyDoCV.Text = crv.First().LyDoC;
                txtTinhTrang.Text = crv.First().TinhTrangC;
                txtPhuongTienVC.Text = crv.First().HinhThucC;
                txtHuongDT.Text = crv.First().PPDTr;
                txtDauHieuLS.Text = crv.First().LoiDan;
                if (crv.First().SoGT != null)
                    txtSoGT.Text = crv.First().SoGT.ToString();
                memoKQCLS.Text = crv.First().KetQua;
                memoThuocSD.Text = crv.First().ThuocSD;
                //txtGGTSo.Text = crv.First().SoGT.ToString();
                lupBVC.EditValue = crv.First().MaBVC;
                lupNguoiDD.EditValue = crv.First().MaCB;
                if (DungChung.Bien.MaBV == "24012")
                {
                    string benhchinh = "";
                    string benhphu2 = "";
                    string benhphu3 = "";
                    string benhphu4 = "";
                    string maICD = "";
                    string maICD2 = "";
                    string maICD3 = "";
                    string maICD4 = "";
                    string[] icd = DungChung.Ham.FreshString(crv.First().MaICD.Trim()).Split(';');

                    string[] tenbenh = DungChung.Ham.FreshString(crv.First().ChanDoan.Trim()).Split(';');

                    foreach (var ICD in icd)
                    {
                        if (string.IsNullOrEmpty(maICD))
                            maICD += ICD;
                        else if (string.IsNullOrEmpty(maICD2))
                            maICD2 += ICD;
                        else if (string.IsNullOrEmpty(maICD3))
                            maICD3 += ICD;
                        else
                            maICD4 += ICD + ";";
                    }
                    foreach (var item in tenbenh)
                    {
                        if (string.IsNullOrEmpty(benhchinh))
                            benhchinh += item;
                        else if (string.IsNullOrEmpty(benhphu2))
                            benhphu2 += item;
                        else if (string.IsNullOrEmpty(benhphu3))
                            benhphu3 += item;
                        else
                            benhphu4 += item + ";";
                    }

                    txtMaICD.Text = maICD.Trim();
                    txtChanDoan.Text = benhchinh.Trim();
                    LupICD2.Text = maICD2.Trim();
                    LupICD2.EditValue = maICD2.Trim();
                    txtBenhKhac2.Text = benhphu2.Trim();
                    LupICD3.EditValue = maICD3.Trim();
                    txtBenhKhac3.Text = benhphu3.Trim();
                    LupICD4.EditValue = maICD4;
                    LupICD4.Text = maICD4;
                    txtBenhKhac4.Text = benhphu4;

                }
                else
                {
                    string[] icd = crv.First().MaICD.Split(';');
                    if (icd.Length > 0)
                        txtMaICD.EditValue = icd[0].Trim();
                    if (icd.Length > 1)
                        LupICD2.EditValue = icd[1].Trim();
                    if (icd.Length > 2)
                        LupICD3.EditValue = icd[2].Trim();
                    if (icd.Length > 3)
                    {
                        string icdKhac = DungChung.Ham.FreshString(string.Join(";", icd.Skip(3)));
                        string[] arricd4 = DungChung.Ham.FreshString(icdKhac).Split(';');
                        string icd4 = "";
                        if (arricd4.Count() > 0)
                        {
                            for (int i = 0; i < arricd4.Count(); i++)
                            {
                                icd4 += arricd4[i].Trim() + ";";
                            }
                        }
                        LupICD4.EditValue = DungChung.Ham.FreshString(string.Join(";", icd4));
                        //LupICD4.EditValue = DungChung.Ham.FreshString(string.Join(";", icd.Skip(3)));
                    }

                    //string[] benhkhac = DungChung.Ham.getICD(crv.First().ChanDoan);
                    //txtChanDoan.EditValue = benhkhac[0];
                    //txtBenhKhac2.EditValue = benhkhac[1];
                    //txtBenhKhac3.EditValue = benhkhac[2];

                    string[] benhkhac = crv.First().ChanDoan.Split(';');
                    if (benhkhac.Length > 3)
                    {
                        txtBenhKhac4.EditValue = DungChung.Ham.FreshString(string.Join(";", benhkhac.Skip(3)));
                    }
                    if (benhkhac.Length > 0)
                    {
                        txtChanDoan.Text = benhkhac[0];
                    }
                }


                if (crv.First().HanC != null)
                    dt_HanC.DateTime = crv.First().HanC.Value;
                txtChanDoanCV.Text = crv.First().ChanDoanCV;
                enableText(false);
            }
            else
            {
                string[] listStr = {};
                string str = "";
                var kb = DataContect.BNKBs.Where(p => p.MaBNhan == _mabn).OrderByDescending(p => p.IDKB).ToList();
                if (kb.Count > 0)
                {
                    str = kb.First().TTChuyenVienTamThoi;
                }
                if (str == "" || str == null)
                {
                    _soHdt = 0;
                    string[] _MaICDarr = DungChung.Ham.getMaICDarrFull(DataContect, _mabn, DungChung.Bien.GetICD, 0);
                    try
                    {
                        int _makp = Convert.ToInt16(_MaICDarr[6]);
                        lupKhoaPhong.EditValue = _makp;
                    }
                    catch
                    {
                    }
                    if (cv.First().NgayNghi != null)
                        txtNgayCV.DateTime = cv.First().NgayNghi.Value;
                    else
                        txtNgayCV.DateTime = System.DateTime.Now;
                    //string[] icd = DungChung.Ham.getICD(_MaICDarr[0]);
                    //txtMaICD.EditValue = icd[0];
                    //LupICD2.EditValue = icd[1];
                    //LupICD3.EditValue = icd[2];
                    //LupICD4.EditValue = icd[3];

                    if (DungChung.Bien.MaBV == "24012")
                    {
                        DungChung.Bien.ChanDoan ttchandoan = DungChung.Ham.getCDRV_24012(DataContect, _mabn, DungChung.Bien.GetICD, 0);
                        txtMaICD.Text = ttchandoan.MaICD.Trim();
                        txtChanDoan.Text = ttchandoan.BenhChinh.Trim();
                        LupICD2.Text = ttchandoan.MaICD2.Trim();
                        LupICD2.EditValue = ttchandoan.MaICD2.Trim();
                        txtBenhKhac2.Text = ttchandoan.BenhPhu2.Trim();
                        LupICD3.EditValue = ttchandoan.MaICD3.Trim();
                        txtBenhKhac3.Text = ttchandoan.BenhPhu3.Trim();
                        LupICD4.EditValue = ttchandoan.MaICD4;
                        LupICD4.Text = ttchandoan.MaICD4;
                        txtBenhKhac4.Text = ttchandoan.BenhPhu4;
                    }
                    else
                    {
                        string[] icd = _MaICDarr[0].Split(';');
                        if (icd.Length > 0)
                            txtMaICD.EditValue = icd[0].Trim();
                        if (icd.Length > 1)
                            LupICD2.EditValue = icd[1].Trim();
                        if (icd.Length > 2)
                            LupICD3.EditValue = icd[2].Trim();
                        if (icd.Length > 3)
                        {
                            string icdKhac = DungChung.Ham.FreshString(string.Join(";", icd.Skip(3)));
                            string[] arricd4 = DungChung.Ham.FreshString(icdKhac).Split(';');
                            string icd4 = "";
                            if (arricd4.Count() > 0)
                            {
                                for (int i = 0; i < arricd4.Count(); i++)
                                {
                                    icd4 += arricd4[i].Trim() + ";";
                                }
                            }
                            LupICD4.EditValue = DungChung.Ham.FreshString(string.Join(";", icd4));
                            //LupICD4.Text = DungChung.Ham.FreshString(string.Join(";", icd4));

                            //LupICD4.EditValue = DungChung.Ham.FreshString(string.Join(";", icd.Skip(3)));
                        }


                        txtBenhKhac4.Text = DungChung.Ham.FreshString(_MaICDarr[7]);
                    }




                    //Dung sửa ngày 2809_his1040
                    if (DungChung.Bien.MaBV == "30350")
                    {
                        txtChanDoanCV.Text = "";
                        if (txtChanDoan.Text != null && txtChanDoan.Text != "")
                            txtChanDoanCV.Text += txtChanDoan.Text;
                        if (txtBenhKhac2.Text != null && txtBenhKhac2.Text != "")
                            txtChanDoanCV.Text += "; " + txtBenhKhac2.Text;
                        if (txtBenhKhac3.Text != null && txtBenhKhac3.Text != "")
                            txtChanDoanCV.Text += "; " + txtBenhKhac3.Text;
                        if (txtBenhKhac4.Text != null && txtBenhKhac4.Text != "")
                            txtChanDoanCV.Text += "; " + txtBenhKhac4.Text;
                    }
                    else
                    {
                        if (DungChung.Bien.MaBV == "20001")
                        {
                            string cdyhct = DungChung.Ham.GetMaYHCT(DungChung.Ham.FreshString(DungChung.Ham.GetICDstr(_MaICDarr[0])), _licd10)[1];
                            txtChanDoanCV.Text = DungChung.Ham._GetCHuoiChanDoanYHCT(DungChung.Ham.FreshString(DungChung.Ham.GetICDstr(_MaICDarr[1])), cdyhct); //cdyhct + "[" + DungChung.Ham.FreshString(DungChung.Ham.GetICDstr(_MaICDarr[1])) + "]";
                        }
                        else
                            txtChanDoanCV.Text = DungChung.Ham.FreshString(DungChung.Ham.GetICDstr(_MaICDarr[1]));
                    }

                    //if (txtChanDoan.Text != null && txtChanDoan.Text != "")
                    //    txtChanDoanCV.Text += txtChanDoan.Text;
                    //if (txtBenhKhac2.Text != null && txtBenhKhac2.Text != "")
                    //    txtChanDoanCV.Text += "; " + txtBenhKhac2.Text;
                    //if (txtBenhKhac3.Text != null && txtBenhKhac3.Text != "")
                    //    txtChanDoanCV.Text += "; " + txtBenhKhac3.Text;
                    //if (txtBenhKhac4.Text != null && txtBenhKhac4.Text != "")
                    //    txtChanDoanCV.Text += "; " + txtBenhKhac4.Text;


                    txtDauHieuLS.Text = "";
                    lupBVC.EditValue = "";
                    lupNguoiDD.EditValue = "";
                    cboLyDoCV.SelectedIndex = 0;
                    txtTinhTrang.Text = "";
                    txtPhuongTienVC.Text = "";

                    dt_HanC.DateTime = (System.DateTime.Now.AddDays(5));
                    // txtSoNgaydt.Text = "0";
                    txtHuongDT.Text = "";
                    txtSoGT.Text = "";
                    if (DungChung.Bien.MaBV == "30007")
                        txtSo.Text = _mabn.ToString();
                    else
                        setSoCV(0);
                    var max = DataContect.RaViens.Where(p => p.Status == 1).ToList().Max(p => p.SoGT);
                    if (max != null)
                        txtSoGT.Text = (max + 1).ToString();
                    else
                        txtSoGT.Text = "1";
                    if (DungChung.Bien.MaBV == "30009")
                        txtSoGT.Text = "";
                    string cacxn = "";

                    // Lấy KQ CLS
                    var CLS_tong = (from cls in DataContect.CLS.Where(p => p.MaBNhan == _mabn)
                                    join cd in DataContect.ChiDinhs.Where(p => p.Status == 1) on cls.IdCLS equals cd.IdCLS
                                    join clsct in DataContect.CLScts on cd.IDCD equals clsct.IDCD
                                    select new { cls.MaBNhan, cd.MaDV, cd.KetLuan, clsct.MaDVct, clsct.KetQua, cd.MoTa }).ToList();
                    var dv_tong = (from dvct in DataContect.DichVucts
                                   join dv in DataContect.DichVus on dvct.MaDV equals dv.MaDV
                                   join tn in DataContect.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                                   join nhom in DataContect.NhomDVs on tn.IDNhom equals nhom.IDNhom
                                   select new { nhom.TenNhomCT, nhom.TenNhom, tn.TenTN, dv.MaDV, dvct.MaDVct, dvct.TenDVct, dv.TenDV }).ToList();
                    //
                    cacxn = "";
                    var CLS3 = (from cls in CLS_tong
                                join dv in dv_tong on cls.MaDV equals dv.MaDV
                                where (dv.TenNhomCT.Contains("Chẩn đoán hình ảnh"))
                                select new { dv.MaDV, cls.MoTa, cls.KetLuan, dv.TenDV, dv.TenTN }).Distinct().ToList();
                    var CLS4 = (from cls in CLS_tong
                                join dv in dv_tong on cls.MaDVct equals dv.MaDVct
                                where (dv.TenNhomCT.Contains("Xét nghiệm")) 
                                select new { cls.MaDVct, cls.KetQua, dv.TenDVct }).ToList();
                    if (DungChung.Bien.MaBV == "24297")
                    {
                        CLS3 = (from cls in CLS_tong
                                join dv in dv_tong on cls.MaDV equals dv.MaDV
                                where (dv.TenNhomCT.Contains("Chẩn đoán hình ảnh")) || (dv.TenTN.ToLower().Contains("Điện tim".ToLower()))
                                select new { dv.MaDV, cls.MoTa, cls.KetLuan, dv.TenDV, dv.TenTN }).Distinct().ToList();
                        CLS4 = (from cls in CLS_tong
                                join dv in dv_tong on cls.MaDVct equals dv.MaDVct
                                where (dv.TenNhomCT.Contains("Xét nghiệm"))// || (dv.TenTN.ToLower().Contains("Điện tim".ToLower()))
                                select new { cls.MaDVct, cls.KetQua, dv.TenDVct }).ToList();
                    }

                    var CLS_KQ = (from cls in CLS_tong
                                  join dv in dv_tong on cls.MaDV equals dv.MaDV
                                  where (dv.TenNhomCT.Contains("Chẩn đoán hình ảnh")) || (dv.TenTN.ToLower().Contains("Điện tim".ToLower()))
                                  select new { cls, dv })
                                  .Select(x => new 
                                  { 
                                      x.dv.TenTN,
                                      KetQua = DataContect.DichVucts.Where(c => c.MaDVct == x.cls.MaDVct).Select(p => p.TenDVct).FirstOrDefault() + ": " + x.cls.KetQua,
                                      MaDVct = DataContect.DichVucts.Where(c => c.MaDVct == x.cls.MaDVct).Select(p => p.MaDVct).FirstOrDefault(),
                                  }).Distinct().ToList();
                    string kq_cls = string.Empty;
                    string kq_clscdha = string.Empty;
                    foreach (var item in CLS_KQ)
                    {
                        if (item.TenTN == "Điện tim")
                        {
                            kq_cls += item.KetQua + ", \n";
                        }
                    }

                    if (DungChung.Bien.MaBV == "24297")
                    {
                        foreach (var i in CLS3)
                        {
                            //if (i.TenNhom.Contains("xét nghiệm") || i.TenNhom.Contains("CĐHA"))
                            if (i.TenTN == "Điện tim")
                            {
                                cacxn += i.TenDV + ": \n- mô tả: \n" + i.MoTa + "; \n- kết luận: " + i.KetLuan + "| \n\n";
                            }
                            else
                            {
                                cacxn += i.TenDV +  ";- kết luận: " + i.KetLuan + "| \n\n";
                            }
                            
                        }
                    }
                    else
                    {
                        foreach (var i in CLS3)
                        {
                            //if (i.TenNhom.Contains("xét nghiệm") || i.TenNhom.Contains("CĐHA"))
                            cacxn += i.TenDV + ": " + i.KetLuan + ", ";
                        }
                    }
                    foreach (var i in CLS4)
                    {
                        if (!string.IsNullOrEmpty(i.KetQua))
                            cacxn += i.TenDVct + ": " + i.KetQua + ", ";
                    }

                    if (string.IsNullOrEmpty(cacxn))
                    {
                        var CLS_tongdt = (from dt2 in DataContect.DThuocs.Where(p => p.MaBNhan == _mabn && p.PLDV == 2)
                                          join dtct in DataContect.DThuoccts on dt2.IDDon equals dtct.IDDon
                                          select new { dtct.MaDV }).ToList();
                        var CLS = (from cls in CLS_tongdt
                                   join dv in dv_tong on cls.MaDV equals dv.MaDV
                                   where (dv.TenNhomCT.Contains("Chẩn đoán hình ảnh") || dv.TenNhomCT.Contains("Xét nghiệm"))
                                   select new { dv.TenDV, dv.TenNhomCT }).Distinct().ToList();
                        if (DungChung.Bien.MaBV == "24297")
                        {
                            CLS = (from cls in CLS_tongdt
                                   join dv in dv_tong on cls.MaDV equals dv.MaDV
                                   where (dv.TenNhomCT.Contains("Chẩn đoán hình ảnh") || dv.TenNhomCT.Contains("Xét nghiệm") || (dv.TenTN.ToLower().Contains("Điện tim".ToLower())))
                                   select new { dv.TenDV, dv.TenNhomCT }).Distinct().ToList();
                        }
                        if (CLS.Count > 0)
                        {
                            foreach (var i in CLS)
                            {
                                //if (i.TenNhom.Contains("xét nghiệm") || i.TenNhom.Contains("CĐHA"))
                                cacxn += i.TenDV + ", ";
                            }
                        }
                    }
                    //
                    memoKQCLS.Text = cacxn;

                    string thuocddung = "";
                    var DT_tong = (from dt2 in DataContect.DThuocs.Where(p => p.MaBNhan == _mabn && p.PLDV == 1)
                                   join dtct in DataContect.DThuoccts on dt2.IDDon equals dtct.IDDon
                                   select new { dtct.MaDV }).ToList();
                    var dv_tong2 = (from dv in DataContect.DichVus
                                    join tn in DataContect.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                                    join nhom in DataContect.NhomDVs.Where(p => p.Status == 1) on tn.IDNhom equals nhom.IDNhom
                                    select new { nhom.TenNhomCT, dv.MaDV, dv.TenDV }).ToList();
                    var dthuoc = (from cls in DT_tong
                                  join dv in dv_tong2 on cls.MaDV equals dv.MaDV
                                  where (dv.TenNhomCT == ("Thuốc trong danh mục BHYT") || dv.TenNhomCT == ("Máu và chế phẩm của máu") || dv.TenNhomCT == "Thuốc thanh toán theo tỷ lệ")
                                  group new { dv, cls } by new { dv.TenDV, dv.TenNhomCT } into kq
                                  select new { kq.Key.TenDV, kq.Key.TenNhomCT }).ToList();
                    foreach (var i in dthuoc)
                    {
                        thuocddung += i.TenDV + ", ";
                    }
                    memoThuocSD.Text = thuocddung;
                    enableText(true);


                    if (lupBSChuyen.EditValue != null)
                        _capbac = lupBSChuyen.Properties.GetKeyValueByDisplayText(lupBSChuyen.Text).ToString();

                }
                else//Chuyenvientamthoi
                {

                    listStr = str.Split('|');
                    _soHdt = 0;
                    try
                    {
                        int _makp = Convert.ToInt16(listStr[0]);
                        lupKhoaPhong.EditValue = _makp;
                    }
                    catch
                    {
                    }

                    //if (listStr[7] != null)
                    //    txtNgayCV.DateTime = Convert.ToDateTime(listStr[7]);
                    //else
                    //{
                    //    if (cv.First().NgayNghi != null)
                    //        txtNgayCV.DateTime = cv.First().NgayNghi.Value;
                    //    else
                    //        txtNgayCV.DateTime = System.DateTime.Now;
                    //}
                    if (cv.First().NgayNghi != null)
                        txtNgayCV.DateTime = cv.First().NgayNghi.Value;
                    else
                        txtNgayCV.DateTime = System.DateTime.Now;
                    if (DungChung.Bien.MaBV == "24297")
                    {
                        txtNgayCV.DateTime = System.DateTime.Now;
                    }
                    DungChung.Bien.ChanDoan ttchandoan = DungChung.Ham.getCDRV_24012(DataContect, _mabn, DungChung.Bien.GetICD, 0);
                    txtChanDoan.Text = listStr[2];
                    txtMaICD.EditValue = listStr[21];
                    LupICD2.EditValue = listStr[22];
                    txtBenhKhac2.Text = listStr[3];
                    LupICD3.EditValue = listStr[23];
                    txtBenhKhac3.Text = listStr[4];
                    LupICD4.EditValue = listStr[24];
                    txtBenhKhac4.Text = listStr[5];

                    txtChanDoanCV.Text = listStr[6];
                    lupMaBVchuyen.EditValue = listStr[10];
                    txtDauHieuLS.Text = listStr[11];
                    lupBSChuyen.EditValue = listStr[1];
                    lupNguoiDD.EditValue = listStr[19];
                    cboLyDoCV.SelectedIndex = listStr[15] != ""? Convert.ToInt32(listStr[15]) : 0;
                    txtTinhTrang.Text = listStr[14];
                    txtPhuongTienVC.Text = listStr[17];

                    dt_HanC.DateTime = listStr[20] != "" ? Convert.ToDateTime(listStr[20]) : System.DateTime.Now;
                    // txtSoNgaydt.Text = "0";
                    txtHuongDT.Text = listStr[16];
                    txtSoGT.Text = listStr[18];

                    memoKQCLS.Text = listStr[12];

                    memoThuocSD.Text = listStr[13];
                }
            }

            if (btnLuu.Enabled == false)
            {
                btnInGiayChuyenBenhNhanDTYHCT.Enabled = true;
            }
            else
            {
                btnInGiayChuyenBenhNhanDTYHCT.Enabled = false;
            }

            if ((DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049") && (DungChung.Bien.PP_SoCV == 1 || (DungChung.Bien.PP_SoCV == 2)))
            {
                txtSo.Properties.ReadOnly = true;
            }
            else
                txtSo.Properties.ReadOnly = false;

        }

        #region in phieu
        private void btnInPhieu_Click(object sender, EventArgs e)
        {
            int rs;
            int _int_maBN = 0;
            if (Int32.TryParse(txtMaBNhan.Text, out rs))
                _int_maBN = Convert.ToInt32(txtMaBNhan.Text);
            DataContect = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            frmIn frm = new frmIn();
            var ravien = DataContect.RaViens.Where(p => p.MaBNhan == _mabn).Where(p => p.Status == 1).ToList();
            bool daco = false;
            if (ravien.Count > 0)
                daco = true;
            if (daco)
            {
                if (DungChung.Bien.MaBV == "04011")
                {
                    BaoCao.repGiayCVNoiTru_HL rep = new BaoCao.repGiayCVNoiTru_HL();
                    // string mabn = txtMaBNhan.Text;
                    var par = DataContect.BenhNhans.Where(p => p.MaBNhan == _int_maBN).ToList();
                    if (par.Count > 0)
                    {
                        // thông tin hành chính
                        var tuyenbv = DataContect.BenhViens.Where(p => p.MaBV == (DungChung.Bien.MaBV)).Select(p => p.TuyenBV).ToList();
                        if (tuyenbv.Count > 0)
                        {
                            string tbv = "";
                            if (tuyenbv.First() != null)
                            {
                                tbv = tuyenbv.First().ToString().Trim();
                                switch (tbv)
                                {
                                    case "A":
                                        rep.TuyenBV.Value = "(Tuyến 01)";
                                        break;
                                    case "B":
                                        rep.TuyenBV.Value = "(Tuyến 02)";
                                        break;
                                    case "C":
                                        rep.TuyenBV.Value = "(Tuyến 03)";
                                        break;
                                    case "D":
                                        rep.TuyenBV.Value = "(Tuyến 04)";
                                        break;
                                }
                            }
                        }
                        rep.HoTenBN.Value = par.First().TenBNhan.ToUpper();
                        rep.DiaChi.Value = par.First().DChi;

                        rep.TuoiBN.Value = DungChung.Ham.TuoitheoThang(DataContect, _mabn, DungChung.Bien.formatAge);

                        rep.Ngaythang.Value = "Ngày ..... tháng ..... năm 20...";

                        if (par.First().GTinh != null)
                        {
                            if (par.First().GTinh.ToString() == "1")
                                rep.GioiTinh.Value = "Nam";
                            else
                                rep.GioiTinh.Value = "Nữ";
                        }
                        rep.Macs.Value = par.First().MaCS;
                        rep.SHBHYT.Value = par.First().SThe;
                        rep.GiatriBHYT.Value = par.First().HanBHTu;
                        rep.GiatriBHYTden.Value = par.First().HanBHDen;
                        var ttbxug = (from tt in DataContect.TTboXungs.Where(p => p.MaBNhan == _mabn)
                                      select new { tt.NgoaiKieu, tt.MaNN, tt.NoiLV, tt.MaDT }).ToList();
                        string _madt = "";
                        if (ttbxug.Count > 0)
                        {
                            rep.NgoaiKieu.Value = ttbxug.First().NgoaiKieu;
                            string mann = "";
                            if (ttbxug.First().MaNN != null)
                                mann = ttbxug.First().MaNN;
                            var nn = DataContect.DmNNs.Where(p => p.MaNN == (mann)).ToList();
                            if (nn.Count > 0)
                                rep.NgheNghiep.Value = nn.First().TenNN;
                            rep.NoiLV.Value = ttbxug.First().NoiLV;
                            _madt = ttbxug.First().MaDT;

                        }
                        var dt = DataContect.DanTocs.Where(p => p.MaDT == _madt).ToList();
                        if (dt.Count > 0)
                            rep.DanToc.Value = dt.First().TenDT;
                        else
                        {
                            MessageBox.Show("Bạn chưa nhập dân tộc cho bệnh nhân");
                        }
                        if (par.First().MaBV != null && par.First().MaBV.ToString() != "")
                        {
                            string mabv = par.First().MaBV;
                            var bv = DataContect.BenhViens.Where(p => p.MaBV == (mabv)).ToList();
                            if (bv.Count > 0)
                                rep.BVtuyentruoc.Value = bv.First().TenBV;
                            string tbvt = "";
                            if (bv.First().TuyenBV != null)
                                tbvt = bv.First().TuyenBV.ToString().Trim();
                            switch (tbvt)
                            {
                                case "A":
                                    rep.TuyenTruoc.Value = "(Tuyến 01)";
                                    break;
                                case "B":
                                    rep.TuyenTruoc.Value = "(Tuyến 02)";
                                    break;
                                case "C":
                                    rep.TuyenTruoc.Value = "(Tuyến 03)";
                                    break;
                                case "D":
                                    rep.TuyenTruoc.Value = "(Tuyến 04)";
                                    break;
                            }
                        }



                        if (par.First().NoiTru == 1)
                        {
                            var tt = (from vv in DataContect.VaoViens.Where(p => p.MaBNhan == _mabn) select vv).ToList();
                            if (tt.Count > 0)
                            {
                                rep.Ngaytu.Value = tt.First().NgayVao;
                            }
                        }
                        else
                        {
                            rep.Ngaytu.Value = par.First().NNhap;

                        }
                        if (ravien.Count > 0)
                        {
                            rep.ChuanD.Value = "- Chẩn đoán:\n " + ravien.First().ChanDoanCV;
                            //

                            string mabvc = "";
                            if (ravien.First().MaBVC != null)
                            {
                                mabvc = ravien.First().MaBVC;
                                var bvc = DataContect.BenhViens.Where(p => p.MaBV == (mabvc)).Select(p => p.TenBV).ToList();
                                if (bvc.Count > 0)
                                    rep.TenBvchuyen.Value = "Kính gửi: " + bvc.First();
                            }
                            if (par.First().NoiTru == 1)
                                rep.SoHS.Value = ravien.First().IdRaVien;
                            rep.Dauhieuls.Value = ravien.First().LoiDan;
                            rep.ngayden.Value = ravien.First().NgayRa;
                            if (ravien.First().LyDoC != null)
                            {
                                string lydo = ravien.First().LyDoC.ToLower();
                                if (lydo.Contains("đúng tuyến"))
                                {
                                    rep.Lydochuyen.Value = "O";
                                    rep.Lydochuyen2.Value = "";
                                }

                                else
                                {
                                    rep.Lydochuyen.Value = "";
                                    rep.Lydochuyen2.Value = "O";
                                }
                            }
                            rep.HuongDT.Value = ravien.First().PPDTr;
                            rep.SoLuu.Value = ravien.First().SoChuyenVien;
                            if (ravien.First().NgayRa != null && ravien.First().NgayRa.ToString() != "")
                                rep.NgayChuyen.Value = ravien.First().NgayRa.Value.Hour + " giờ " + ravien.First().NgayRa.Value.Minute + " phút, ngày " + ravien.First().NgayRa.Value.Day + " tháng " + ravien.First().NgayRa.Value.Month + " năm " + ravien.First().NgayRa.Value.Year;
                            rep.PhuongTien.Value = ravien.First().HinhThucC;
                            // tên người hộ tống
                            string macb = "";
                            if (ravien.First().MaCB != null)
                            {
                                macb = ravien.First().MaCB;
                                var cb = DataContect.CanBoes.Where(p => p.MaCB == (macb)).ToList();
                                if (cb.Count > 0 && cb.First().TenCB != null && cb.First().TenCB.Trim() != "")
                                {
                                    rep.HoTenNC.Value = cb.First().CapBac + "  " + cb.First().TenCB;
                                }
                            }
                            rep.CacXN.Value = "- Kết quả xét nghiệm, cận lâm sàng:\n " + ravien.First().KetQua;
                        }
                        rep.ThuocDD.Value = "- Phương pháp, thủ thuật, kỹ thuật, thuốc đã sử dụng trong điều trị:\n " + ravien.First().ThuocSD;

                        //

                        rep.CreateDocument();
                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                        frm.ShowDialog();
                    }
                }
                else if (DateTime.Now > Convert.ToDateTime("01/12/2018 00:00:00"))
                {
                    BaoCao.repGiayCVNoiTru_TT146 rep = new BaoCao.repGiayCVNoiTru_TT146();

                    if (Int32.TryParse(txtMaBNhan.Text, out rs))
                        _int_maBN = Convert.ToInt32(txtMaBNhan.Text);
                    var par = DataContect.BenhNhans.Where(p => p.MaBNhan == _int_maBN).ToList();
                    DateTime dt7 = Convert.ToDateTime(txtNgayCV.Text);
                    if (DungChung.Bien.MaBV == "30003")
                    {
                        rep.Ngaythang.Value = "Ngày " + dt7.Day.ToString() + " tháng " + dt7.Month.ToString() + " năm " + dt7.Year.ToString();
                    }
                    else
                    {
                        rep.Ngaythang.Value = "Ngày ..... tháng ..... năm 20...";
                    }
                    if (par.Count > 0)
                    {
                        // thông tin hành chính
                        var tuyenbv = DataContect.BenhViens.Where(p => p.MaBV == (DungChung.Bien.MaBV)).Select(p => p.TuyenBV).ToList();
                        if (tuyenbv.Count > 0)
                        {
                            string tbv = "";
                            if (tuyenbv.First() != null)
                            {
                                tbv = tuyenbv.First().ToString().Trim();
                                switch (tbv)
                                {
                                    case "A":
                                        rep.TuyenBV.Value = "(Tuyến 01)";
                                        break;
                                    case "B":
                                        rep.TuyenBV.Value = "(Tuyến 02)";
                                        break;
                                    case "C":
                                        rep.TuyenBV.Value = "(Tuyến 03)";
                                        break;
                                    case "D":
                                        rep.TuyenBV.Value = "(Tuyến 04)";
                                        break;
                                }
                            }
                        }
                        rep.HoTenBN.Value = par.First().TenBNhan.ToUpper();
                        rep.DiaChi.Value = par.First().DChi;
                        rep.TuoiBN.Value = DungChung.Bien.MaBV == "14018" ? DungChung.Ham.TinhTuoiBenhNhan(DataContect, _int_maBN) : DungChung.Ham.TuoitheoThang(DataContect, _mabn, DungChung.Bien.formatAge);

                        if (par.First().GTinh != null)
                        {
                            if (par.First().GTinh.ToString() == "1")
                                rep.GioiTinh.Value = "Nam";
                            else
                                rep.GioiTinh.Value = "Nữ";
                        }
                        rep.Macs.Value = par.First().MaCS;
                        rep.SHBHYT.Value = par.First().SThe;
                        rep.GiatriBHYT.Value = par.First().HanBHTu;
                        rep.GiatriBHYTden.Value = par.First().HanBHDen;
                        var ttbxug = (from tt in DataContect.TTboXungs.Where(p => p.MaBNhan == _mabn)
                                      join dt in DataContect.DanTocs on tt.MaDT equals dt.MaDT
                                      select new { dt.TenDT, tt.NgoaiKieu, tt.MaNN, tt.NoiLV }).ToList();
                        if (ttbxug.Count > 0)
                        {
                            rep.NgoaiKieu.Value = ttbxug.First().NgoaiKieu;
                            string mann = "";
                            if (ttbxug.First().MaNN != null)
                                mann = ttbxug.First().MaNN;
                            var nn = DataContect.DmNNs.Where(p => p.MaNN == (mann)).ToList();
                            if (nn.Count > 0)
                                rep.NgheNghiep.Value = nn.First().TenNN;
                            rep.NoiLV.Value = ttbxug.First().NoiLV;
                            rep.DanToc.Value = ttbxug.First().TenDT;
                        }
                        else
                        {
                            rep.NgoaiKieu.Value = "Việt Nam";
                        }
                        if (par.First().MaBV != null && par.First().MaBV.ToString() != "")
                        {
                            string tbvt = "";
                            string mabv = par.First().MaBV;
                            var bv = DataContect.BenhViens.Where(p => p.MaBV == (mabv)).ToList();
                            if (bv.Count > 0)
                            {
                                rep.BVtuyentruoc.Value = bv.First().TenBV;

                                if (bv.First().TuyenBV != null)
                                    tbvt = bv.First().TuyenBV.ToString().Trim();
                            }
                            switch (tbvt)
                            {
                                case "A":
                                    rep.TuyenTruoc.Value = "(Tuyến 01)";
                                    break;
                                case "B":
                                    rep.TuyenTruoc.Value = "(Tuyến 02)";
                                    break;
                                case "C":
                                    rep.TuyenTruoc.Value = "(Tuyến 03)";
                                    break;
                                case "D":
                                    rep.TuyenTruoc.Value = "(Tuyến 04)";
                                    break;
                            }
                        }
                        var kb = DataContect.BNKBs.Where(p => p.MaBNhan == _mabn).OrderByDescending(p => p.IDKB).ToList();
                        string mabsdt = "";
                        if (!string.IsNullOrEmpty(ravien.First().MaBS))
                        {
                            mabsdt = ravien.First().MaBS;
                        }
                        else
                        {

                            if (kb.Count > 0)
                            {

                                mabsdt = kb.First().MaCB;

                            }
                        }
                        var cb = DataContect.CanBoes.Where(p => p.MaCB == mabsdt).FirstOrDefault();
                        var _capbaccb = DataContect.CanBoes.Where(p => p.MaCB == _capbac).FirstOrDefault();

                        if (cb != null)
                            rep.HoTenBS.Value = DungChung.Bien.MaBV == "30012" ? "Bác sĩ: " + cb.TenCB : (DungChung.Bien.MaBV == "30002") ? _capbaccb.CapBac + ": " + cb.TenCB : cb.TenCB;
                        if (par.First().NoiTru == 1)
                        {
                            var tt = (from vv in DataContect.VaoViens.Where(p => p.MaBNhan == _mabn) select vv).ToList();
                            if (tt.Count > 0)
                            {
                                rep.Ngaytu.Value = tt.First().NgayVao;
                            }
                        }
                        else
                        {
                            if (kb.Count > 0)
                            {
                                rep.Ngaytu.Value = kb.First().NgayKham;
                            }
                        }

                        if (ravien.Count > 0)
                        {
                            string mabvc = "";
                            if (ravien.First().MaBVC != null)
                            {
                                mabvc = ravien.First().MaBVC;
                                var bvc = DataContect.BenhViens.Where(p => p.MaBV == (mabvc)).Select(p => p.TenBV).ToList();
                                if (bvc.Count > 0)
                                    rep.TenBvchuyen.Value = "Kính gửi: " + bvc.First();
                            }

                            if (par.First().NoiTru == 1)
                                rep.SoHS.Value = ravien.First().IdRaVien;
                            rep.Dauhieuls.Value = ravien.First().LoiDan;//in đậm cho BV tam đường
                            rep.ngayden.Value = ravien.First().NgayRa;
                            rep.TinhTrangBN.Value = ravien.First().TinhTrangC;
                            if (ravien.First().LyDoC != null)
                            {
                                string lydo = ravien.First().LyDoC.ToLower();
                                if (lydo.Contains("đúng tuyến"))
                                {
                                    rep.txt2.Visible = true;
                                    rep.txt1.Visible = false;
                                }

                                else
                                {
                                    rep.txt2.Visible = false;
                                    rep.txt1.Visible = true;
                                }
                            }
                            if (DungChung.Bien.MaBV == "24297")
                            {
                                var icd = DataContect.ICD10.Where(o => true).ToList();
                                rep.ChuanD.Value = "- Chẩn đoán: " + DungChung.Ham.GhepChuoiChanDoanYHCT(icd, "", ravien.First().MaICD);
                            }
                            else
                            {
                                rep.ChuanD.Value = (DungChung.Bien.MaBV == "12001" ? "" : "- Chẩn đoán: ") + ravien.First().ChanDoanCV;//in đậm cho BV tam đường
                            }


                            rep.HuongDT.Value = ravien.First().PPDTr;//in đậm cho BV tam đường
                            if (DungChung.Bien.MaBV == "24009")
                            {
                                rep.SoLuu.Value = "..........";
                            }
                            else
                                rep.SoLuu.Value = ravien.First().SoChuyenVien;
                            //rep.SoLuu.Value = (DungChung.Bien.MaBV == "30003" || ravien.First().SoChuyenVien == null) ? "" : (ravien.First().SoChuyenVien.Value.ToString());
                            if (ravien.First().NgayRa != null && ravien.First().NgayRa.ToString() != "")
                                rep.NgayChuyen.Value = ravien.First().NgayRa.Value.Hour + " giờ " + ravien.First().NgayRa.Value.Minute + " phút, ngày " + ravien.First().NgayRa.Value.Day + " tháng " + ravien.First().NgayRa.Value.Month + " năm " + ravien.First().NgayRa.Value.Year;
                            rep.PhuongTien.Value = ravien.First().HinhThucC;
                            var kp = (from rv in ravien join kphong in DataContect.KPhongs on rv.MaKP equals kphong.MaKP select kphong).FirstOrDefault();
                            rep.p_TenKP.Value = kp.TenKP;

                            // tên người hộ tống
                            string macb = "";
                            if (ravien.First().MaCB != null)
                            {
                                macb = ravien.First().MaCB;
                                var cb1 = DataContect.CanBoes.Where(p => p.MaCB == (macb)).ToList();
                                if (cb1.Count > 0 && cb1.First().TenCB != null && cb1.First().TenCB.Trim() != "")
                                {
                                    rep.HoTenNC.Value = cb1.First().CapBac + "  " + cb1.First().TenCB;
                                }
                                else
                                {
                                    rep.HoTenNC.Value = lupNguoiDD.Text;
                                }
                            }
                            rep.CacXN.Value = (DungChung.Bien.MaBV == "12001" ? "" : "- Kết quả xét nghiệm, cận lâm sàng: ") + ravien.First().KetQua;//in đậm cho BV tam đường
                        }

                        rep.ThuocDD.Value = "- Phương pháp, thủ thuật, kỹ thuật, thuốc đã sử dụng trong điều trị: " + ravien.First().ThuocSD;
                        rep.SoHS.Value = _mabn;
                    }
                    //

                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                }

                else
                {

                    BaoCao.repGiayCVNoiTru rep = new BaoCao.repGiayCVNoiTru();

                    if (Int32.TryParse(txtMaBNhan.Text, out rs))
                        _int_maBN = Convert.ToInt32(txtMaBNhan.Text);
                    var par = DataContect.BenhNhans.Where(p => p.MaBNhan == _int_maBN).ToList();
                    if (par.Count > 0)
                    {
                        // thông tin hành chính
                        var tuyenbv = DataContect.BenhViens.Where(p => p.MaBV == (DungChung.Bien.MaBV)).Select(p => p.TuyenBV).ToList();
                        if (tuyenbv.Count > 0)
                        {
                            string tbv = "";
                            if (tuyenbv.First() != null)
                            {
                                tbv = tuyenbv.First().ToString().Trim();
                                switch (tbv)
                                {
                                    case "A":
                                        rep.TuyenBV.Value = "(Tuyến 01)";
                                        break;
                                    case "B":
                                        rep.TuyenBV.Value = "(Tuyến 02)";
                                        break;
                                    case "C":
                                        rep.TuyenBV.Value = "(Tuyến 03)";
                                        break;
                                    case "D":
                                        rep.TuyenBV.Value = "(Tuyến 04)";
                                        break;
                                }
                            }
                        }
                        rep.HoTenBN.Value = par.First().TenBNhan.ToUpper();
                        rep.DiaChi.Value = par.First().DChi;
                        rep.TuoiBN.Value = DungChung.Ham.TuoitheoThang(DataContect, _mabn, DungChung.Bien.formatAge);
                        rep.Ngaythang.Value = "Ngày ..... tháng ..... năm 20...";
                        if (par.First().GTinh != null)
                        {
                            if (par.First().GTinh.ToString() == "1")
                                rep.GioiTinh.Value = "Nam";
                            else
                                rep.GioiTinh.Value = "Nữ";
                        }
                        rep.Macs.Value = par.First().MaCS;
                        rep.SHBHYT.Value = par.First().SThe;
                        rep.GiatriBHYT.Value = par.First().HanBHTu;
                        rep.GiatriBHYTden.Value = par.First().HanBHDen;
                        var ttbxug = (from tt in DataContect.TTboXungs.Where(p => p.MaBNhan == _mabn)
                                      join dt in DataContect.DanTocs on tt.MaDT equals dt.MaDT
                                      select new { dt.TenDT, tt.NgoaiKieu, tt.MaNN, tt.NoiLV }).ToList();
                        if (ttbxug.Count > 0)
                        {
                            rep.NgoaiKieu.Value = ttbxug.First().NgoaiKieu;
                            string mann = "";
                            if (ttbxug.First().MaNN != null)
                                mann = ttbxug.First().MaNN;
                            var nn = DataContect.DmNNs.Where(p => p.MaNN == (mann)).ToList();
                            if (nn.Count > 0)
                                rep.NgheNghiep.Value = nn.First().TenNN;
                            rep.NoiLV.Value = ttbxug.First().NoiLV;
                            rep.DanToc.Value = ttbxug.First().TenDT;
                        }
                        else
                        {
                            rep.NgoaiKieu.Value = "Việt Nam";
                        }
                        if (par.First().MaBV != null && par.First().MaBV.ToString() != "")
                        {
                            string tbvt = "";
                            string mabv = par.First().MaBV;
                            var bv = DataContect.BenhViens.Where(p => p.MaBV == (mabv)).ToList();
                            if (bv.Count > 0)
                            {
                                rep.BVtuyentruoc.Value = bv.First().TenBV;

                                if (bv.First().TuyenBV != null)
                                    tbvt = bv.First().TuyenBV.ToString().Trim();
                            }
                            switch (tbvt)
                            {
                                case "A":
                                    rep.TuyenTruoc.Value = "(Tuyến 01)";
                                    break;
                                case "B":
                                    rep.TuyenTruoc.Value = "(Tuyến 02)";
                                    break;
                                case "C":
                                    rep.TuyenTruoc.Value = "(Tuyến 03)";
                                    break;
                                case "D":
                                    rep.TuyenTruoc.Value = "(Tuyến 04)";
                                    break;
                            }
                        }
                        var kb = DataContect.BNKBs.Where(p => p.MaBNhan == _mabn).OrderByDescending(p => p.IDKB).ToList();
                        string mabsdt = "";
                        if (!string.IsNullOrEmpty(ravien.First().MaBS))
                        {
                            mabsdt = ravien.First().MaBS;
                        }
                        else
                        {

                            if (kb.Count > 0)
                            {

                                mabsdt = kb.First().MaCB;

                            }
                        }
                        var cb = DataContect.CanBoes.Where(p => p.MaCB == mabsdt).FirstOrDefault();
                        var _capbaccb = DataContect.CanBoes.Where(p => p.MaCB == _capbac).FirstOrDefault();
                        if (cb != null)
                            rep.HoTenBS.Value = (DungChung.Bien.MaBV == "30002") ? _capbaccb.CapBac + ": " + cb.TenCB : cb.TenCB;
                        if (par.First().NoiTru == 1)
                        {
                            var tt = (from vv in DataContect.VaoViens.Where(p => p.MaBNhan == _mabn) select vv).ToList();
                            if (tt.Count > 0)
                            {
                                rep.Ngaytu.Value = tt.First().NgayVao;
                            }
                        }
                        else
                        {
                            if (kb.Count > 0)
                            {
                                rep.Ngaytu.Value = kb.First().NgayKham;
                            }
                        }

                        if (ravien.Count > 0)
                        {
                            string mabvc = "";
                            if (ravien.First().MaBVC != null)
                            {
                                mabvc = ravien.First().MaBVC;
                                var bvc = DataContect.BenhViens.Where(p => p.MaBV == (mabvc)).Select(p => p.TenBV).ToList();
                                if (bvc.Count > 0)
                                    rep.TenBvchuyen.Value = "Kính gửi: " + bvc.First();
                            }

                            if (par.First().NoiTru == 1)
                                rep.SoHS.Value = ravien.First().IdRaVien;
                            rep.Dauhieuls.Value = (DungChung.Bien.MaBV == "12001" ? "" : "- Dấu hiệu lâm sàng : ") + ravien.First().LoiDan;//in đậm cho BV tam đường
                            rep.ngayden.Value = ravien.First().NgayRa;
                            rep.TinhTCV.Value = "- Tình trạng người bệnh lúc chuyển viện: " + ravien.First().TinhTrangC;
                            if (ravien.First().LyDoC != null)
                            {
                                string lydo = ravien.First().LyDoC.ToLower();
                                if (lydo.Contains("đúng tuyến"))
                                {
                                    rep.Lydochuyen.Value = "O";//in đậm cho BV tam đường
                                    rep.Lydochuyen2.Value = "";
                                }

                                else
                                {
                                    rep.Lydochuyen.Value = "";
                                    rep.Lydochuyen2.Value = "O";
                                }
                            }
                            rep.ChuanD.Value = (DungChung.Bien.MaBV == "12001" ? "" : "- Chẩn đoán: ") + ravien.First().ChanDoanCV;//in đậm cho BV tam đường
                            rep.HuongDT.Value = "- Hướng điều trị: " + ravien.First().PPDTr;//in đậm cho BV tam đường
                            rep.SoLuu.Value = ravien.First().SoChuyenVien;
                            if (ravien.First().NgayRa != null && ravien.First().NgayRa.ToString() != "")
                                rep.NgayChuyen.Value = ravien.First().NgayRa.Value.Hour + " giờ " + ravien.First().NgayRa.Value.Minute + " phút, ngày " + ravien.First().NgayRa.Value.Day + " tháng " + ravien.First().NgayRa.Value.Month + " năm " + ravien.First().NgayRa.Value.Year;
                            rep.PhuongTien.Value = ravien.First().HinhThucC;
                            var kp = (from rv in ravien join kphong in DataContect.KPhongs on rv.MaKP equals kphong.MaKP select kphong).FirstOrDefault();
                            rep.p_TenKP.Value = kp.TenKP;

                            // tên người hộ tống
                            string macb = "";
                            if (ravien.First().MaCB != null)
                            {
                                macb = ravien.First().MaCB;
                                var cb1 = DataContect.CanBoes.Where(p => p.MaCB == (macb)).ToList();
                                if (cb1.Count > 0 && cb1.First().TenCB != null && cb1.First().TenCB.Trim() != "")
                                {
                                    rep.HoTenNC.Value = cb1.First().CapBac + "  " + cb1.First().TenCB;
                                }
                                else
                                {
                                    rep.HoTenNC.Value = lupNguoiDD.Text;
                                }
                            }
                            rep.CacXN.Value = (DungChung.Bien.MaBV == "12001" ? "" : "- Kết quả xét nghiệm, cận lâm sàng: ") + ravien.First().KetQua;//in đậm cho BV tam đường
                        }

                        rep.ThuocDD.Value = "- Phương pháp, thủ thuật, kỹ thuật, thuốc đã sử dụng trong điều trị: " + ravien.First().ThuocSD;
                    }
                    //

                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("Bạn chưa lưu dữ liệu");
            }
        }
        #endregion
        private bool KTLuu()
        {
            if (string.IsNullOrEmpty(lupBSChuyen.Text))
            {
                MessageBox.Show("Bạn chưa chọn bác sĩ ra y lệnh chuyển bệnh nhân");
                lupBSChuyen.Focus();
                return false;
            }
            if (lupBVC.Text == null || lupBVC.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn nơi chuyển đến");
                lupBVC.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtNgayCV.Text))
            {
                MessageBox.Show("Bạn chưa nhập ngày chuyển");
                txtNgayCV.Focus();
                return false;
            }
            var ngayDV = DataContect.BenhNhans.Where(p => p.MaBNhan == _mabn).Select(p => p.NNhap).Max();
            var benhnhan = DataContect.BenhNhans.Where(p => p.MaBNhan == _mabn).FirstOrDefault();
            if (ngayDV != null && (ngayDV.Value.Date - txtNgayCV.DateTime.Date).Days > 0)
            {
                MessageBox.Show("Ngày chuyển viện không được nhỏ hơn ngày nhập");
                txtNgayCV.Focus();
                return false;
            }
            if (benhnhan != null && (benhnhan.NoiTru == 1 || (benhnhan.NoiTru == 0 && benhnhan.DTNT == true)) && (txtNgayVao.DateTime > txtNgayCV.DateTime))
            {
                MessageBox.Show("Ngày chuyển viện không được nhỏ hơn ngày vào viện");
                txtNgayCV.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtSoNgaydt.Text))
            {
                MessageBox.Show("Bạn chưa nhập số ngày điều trị");
                txtSoNgaydt.Focus();
                return false;
            }
            //else
            //{
            //    double _SoNgayGiuong = 0;
            //    //frm_Ravien frm = new frm_Ravien();
            //    _SoNgayGiuong = frm_Ravien._getSoNgayGiuong(DataContect, _mabn);
            //    if(_SoNgayGiuong<=0)
            //}

            if (string.IsNullOrEmpty(lupKhoaPhong.Text))
            {
                MessageBox.Show("Khoa phòng Điều trị không hợp lệ!");
                lupKhoaPhong.Focus();
                return false;
            }

            string tencd = "";
            tencd = DungChung.Ham.KTChiDinh(DataContect, _mabn);
            if (!string.IsNullOrEmpty(tencd))
            {
                if (DungChung.Bien.MaBV != "30003" && DungChung.Bien.MaBV != "01830" && DungChung.Bien.MaBV != "01071" && DungChung.Bien.MaBV != "01049" && DungChung.Bien.MaBV != "12345" && DungChung.Bien.MaBV != "24297" && DungChung.Bien.MaBV != "56789")
                {
                    DialogResult _result = MessageBox.Show("Bệnh nhân có các chỉ định CLS chưa được thực hiện:\n " + tencd + "Bạn vẫn muốn làm thủ tục ra viện?", "Hỏi ra viện", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (_result == DialogResult.No)
                        return false;

                }
                else
                {
                    MessageBox.Show("Bệnh nhân có các chỉ định CLS chưa được thực hiện:\n " + tencd + "Bạn không thể lưu ra viện", "Hỏi ra viện");
                    return false;
                }
            }
            //if (!string.IsNullOrEmpty(txtSoNgaydt.Text))
            //{
            //    if (songaydt != Convert.ToInt32(txtSoNgaydt.Text))
            //    {
            //        DialogResult a = MessageBox.Show("Số ngày điều trị không khớp với thực tế, bạn có muốn lưu ?", "Cảnh báo", MessageBoxButtons.OKCancel);
            //        if (a == DialogResult.Cancel)
            //        {
            //            txtSoNgaydt.Focus();
            //            return false;
            //        }
            //    }
            //}
            if (string.IsNullOrEmpty(dt_HanC.Text))
            {
                MessageBox.Show("Bạn chưa nhập ngày hết hạn chuyển");
                dt_HanC.Focus();
                return false;
            }
            if ((dt_HanC.DateTime.Date - txtNgayCV.DateTime.Date).Days < 0)
            {
                MessageBox.Show("Hạn chuyển phải >= ngày chuyển");
            }
            if (!checkNgayKeNgayRa(_mabn, txtNgayCV.DateTime))
            {
                return false;
            }

            if (!checkNgayKhamNgayRa(_mabn, txtNgayCV.DateTime))
            {
                txtNgayCV.Focus();
                return false;
            }
            var bnkb = DataContect.BNKBs.Where(p => p.MaBNhan == _mabn).OrderByDescending(p => p.IDKB).ToList();
            int _makp = 0;
            string giuong = "";

            if (bnkb.Count > 0)
            {
                _makp = bnkb.First().MaKP == null ? 0 : bnkb.First().MaKP.Value;
                // giuong = bnkb.First().Giuong;
                giuong = string.Join("", bnkb.Where(p => p.Giuong != null && p.Giuong != "").Select(p => p.Giuong).ToArray());
            }
            //if (benhnhan.NoiTru == 1 && benhnhan.DTuong == "BHYT" && string.IsNullOrEmpty(giuong))
            //{
            //    MessageBox.Show("Bạn chưa nhập mã giường điều trị của bệnh nhân!");
            //    return false;
            //}
            if (benhnhan.NoiTru == 1 && benhnhan.DTuong == "BHYT" && !string.IsNullOrEmpty(giuong) && giuong.Length > 12)
            {
                MessageBox.Show("Tổng ký tự buồng giường không được > 12 ký tự");
                return false;
            }
            if (benhnhan.NoiTru == 1 && benhnhan.DTuong == "BHYT" && string.IsNullOrEmpty(giuong))
            {
                MessageBox.Show("Bạn chưa nhập mã giường điều trị của bệnh nhân!");
                return false;
            }
            double SoNgayGiuong = 0;
            SoNgayGiuong = frm_Ravien._getSoNgayGiuong(DataContect, _mabn);
            if (SoNgayGiuong == 0 && (_noitru == 1 || DTNT == true))
            {
                DialogResult Result = MessageBox.Show("Bệnh nhân chưa có tiền ngày giường, bạn có muốn cập nhật tiền ngày giường ?", "Cập nhật", MessageBoxButtons.YesNo);
                if (Result == DialogResult.Yes)
                {
                    frm_CapNhatNgayGiuong frm = new frm_CapNhatNgayGiuong(_mabn, txtNgayCV.DateTime, 2);
                    frm.ShowDialog();
                    return false;
                }
            }
            if (DungChung.Bien.MaBV == "30002" || DungChung.Bien.MaBV == "12122" || DungChung.Bien.MaBV == "01830")
            {
                string dsdonchualenpl = CheckDonChuaLenPLinh(_mabn);
                if (!string.IsNullOrEmpty(dsdonchualenpl))
                {
                    if (DungChung.Bien.MaBV == "01830")// quynv yc 17/07/2
                    {
                        var bn = DataContect.BenhNhans.Where(p => p.MaBNhan == _mabn).Select(p => p.CapCuu).FirstOrDefault();
                        if (bn != null && bn.Value == 1)
                        {
                            DialogResult _result = MessageBox.Show("Bệnh nhân có các đơn thuốc chưa tạo phiếu lĩnh :\n " + dsdonchualenpl + "Bạn vẫn muốn làm thủ tục chuyển viện?", "Hỏi chuyển viện", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (_result == DialogResult.No)
                                return false;
                        }
                        else
                        {
                            MessageBox.Show("Bệnh nhân có các đơn thuốc chưa tạo phiếu lĩnh :\n " + dsdonchualenpl + "Bạn không thể làm thủ tục chuyển viện", "Hỏi chuyển viện");

                            return false;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Bệnh nhân có các đơn thuốc chưa tạo phiếu lĩnh :\n " + dsdonchualenpl + "Bạn không thể làm thủ tục chuyển viện?", "Thông báo");
                        return false;
                    }

                }
            }
            return true;
        }
        public static string CheckDonChuaLenPLinh(int mabn)
        {
            QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            string DSDon = "";
            var kt = (from dt in _data.DThuocs.Where(p => p.MaBNhan == mabn)
                      join dtct in _data.DThuoccts on dt.IDDon equals dtct.IDDon
                      join kp in _data.KPhongs.Where(p => p.PLoai != DungChung.Bien.st_PhanLoaiKP.TuTruc) on dt.MaKXuat equals kp.MaKP
                      where (dtct.Status == 0 && (dtct.SoPL == 0 || dtct.SoPL == null) && dtct.Status != -1)
                      select new { dt.IDDon, dt.NgayKe, kp.TenKP }).Distinct().ToList();
            foreach (var item in kt)
                DSDon += item.IDDon + " đơn ngày: " + item.NgayKe + " kho kê: " + item.TenKP + "\n";
            return DSDon;
        }
        private bool checkNgayKhamNgayRa(int _mabn, DateTime ngayra)
        {
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

            var qbnkb = (from kb in data.BNKBs.Where(p => p.MaBNhan == _mabn && p.NgayKham > ngayra) select kb).ToList();
            if (qbnkb.Count > 0)
            {
                MessageBox.Show("Bệnh nhân có ngày khám lớn hơn ngày chuyển viện");
                return false;
            }
            return true;


        }
        public bool checkNgayKeNgayRa(int maBNhan, DateTime ngayra)
        {
            bool kt = true;
            string _loi = "";
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var _ldv = data.DichVus.ToList();
            var qdthuoc = (from dt in data.DThuocs.Where(p => p.MaBNhan == maBNhan) //.Where(p => p.NgayKe >= ngayra) sửa lại để check cả dịch vụ nhập Đức 05-09
                           join dtct in data.DThuoccts.Where(p => p.NgayNhap >= ngayra) on dt.IDDon equals dtct.IDDon
                           group new { dt, dtct } by new { dt.MaBNhan, dtct.MaDV }
                               into kq
                           select new { kq.Key.MaDV, SoLuong = kq.Sum(p => p.dtct.SoLuong) }).Where(p => p.SoLuong > 0).ToList();
            if (qdthuoc.Count > 0)
            {
                var q1 = (from dtt in qdthuoc
                          join dv in _ldv on dtt.MaDV equals dv.MaDV
                          select new { dv.TenDV }).ToList(); ;
                _loi = string.Join(";", q1.Select(p => p.TenDV).ToArray());
                kt = false;
                MessageBox.Show("Bệnh nhân có các y lệnh: " + _loi + "\nCó ngày nhập lớn hơn ngày ra viện");
            }
            if (kt)
            {
                var _ldontra = data.DThuocs.Where(p => p.MaBNhan == maBNhan).Where(p => p.PLDV == 1 && p.KieuDon == 2).Where(p => p.NgayKe >= ngayra).Select(p => new { p.NgayKe, p.IDDon }).ToList();
                foreach (var item in _ldontra)
                {
                    DateTime ngaykenew = Convert.ToDateTime(item.NgayKe);
                    var qdthuocnew = (from dt in data.DThuocs.Where(p => p.PLDV == 1).Where(p => p.MaBNhan == maBNhan)
                                      join dtct in data.DThuoccts.Where(p => p.NgayNhap >= ngaykenew.Date) on dt.IDDon equals dtct.IDDon
                                      group new { dt, dtct } by new { dt.MaBNhan, dtct.MaDV }
                                          into kq
                                      select new { kq.Key.MaDV, SoLuong = kq.Sum(p => p.dtct.SoLuong) }).Where(p => p.SoLuong > 0).ToList();
                    if (qdthuocnew.Count > 0)
                    {
                        var q1 = (from dtt in qdthuoc
                                  join dv in _ldv on dtt.MaDV equals dv.MaDV
                                  select new { dv.TenDV }).ToList(); ;
                        _loi += ";" + string.Join(";", q1.Select(p => p.TenDV).ToArray());

                        kt = false;
                    }
                }
                if (!kt)
                {
                    kt = false;
                    MessageBox.Show("Bệnh nhân có các y lệnh: " + _loi + "\nCó ngày nhập lớn hơn ngày ra viện");
                }
                //trả thuốc ko hết vẫn cho ra trc đơn trả và sau đơn lĩnh về a hùng 20-07
                //var _lq = (from dt in data.DThuocs.Where(p => p.MaBNhan == maBNhan).Where(p=>p.PLDV==2)
                //           join dtct in data.DThuoccts.Where(p => p.NgayNhap >= ngayra).Where(p => p.SoLuong > 0) on dt.IDDon equals dtct.IDDon
                //           select dtct).ToList();
                //if (_lq.Count > 0)
                //    kt = false;
            }
            if (kt)//kiểm tra thêm thời gian diễn biến 26-06 đoài
            {
                var _ldontra = data.DThuocs.Where(p => p.MaBNhan == maBNhan).Where(p => p.PLDV == 1 && p.KieuDon == 2).Where(p => p.NgayKe >= ngayra).Select(p => new { p.NgayKe, p.IDDon }).ToList();
                if (_ldontra.Count > 0)
                {
                    DateTime ngaykemax = Convert.ToDateTime(_ldontra.Max(p => p.NgayKe));
                    var qdienbien = data.DienBiens.Where(p => p.MaBNhan == _mabn).Where(p => p.NgayNhap > ngaykemax).Select(p => p.NgayNhap).OrderByDescending(p => p).ToList();
                    if (qdienbien.Count > 0)
                    {
                        MessageBox.Show("Bệnh nhân có diễn biến bệnh lớn hơn ngày ra viện");
                        kt = false;
                    }
                }
                else
                {
                    var qdienbien = data.DienBiens.Where(p => p.MaBNhan == _mabn).Where(p => p.NgayNhap > ngayra).Select(p => p.NgayNhap).OrderByDescending(p => p).ToList();
                    if (qdienbien.Count > 0)
                    {
                        MessageBox.Show("Bệnh nhân có diễn biến bệnh lớn hơn ngày ra viện");
                        kt = false;
                    }
                }

            }
            if (kt)//kiểm tra thêm thời gian hội chẩn 26-06 đoài
            {
                var qdienbien = data.BBHCs.Where(p => p.MaBNhan == _mabn).Where(p => p.NgayHC > ngayra).Select(p => p.NgayHC).OrderByDescending(p => p).ToList();
                if (qdienbien.Count > 0)
                {
                    MessageBox.Show("Bệnh nhân có hội chẩn lớn hơn ngày ra viện");
                    kt = false;
                }
            }
            return kt;
        }
        //public bool checkNgayKeNgayRa(int maBNhan, DateTime ngayra)
        //{
        //    bool kt = true;
        //    QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        //    var q1 = data.DThuocs.Where(p => p.MaBNhan == maBNhan).ToList();
        //    var q2 = (from dt in data.DThuocs.Where(p => p.MaBNhan == maBNhan)
        //              join dtct in data.DThuoccts on dt.IDDon equals dtct.IDDon
        //              select dtct).ToList();
        //    var qdthuoc = (from dtct in q2.Where(p => p.NgayNhap >= ngayra).Where(p => p.SoLuong > 0)
        //                   select dtct).ToList();
        //    if (qdthuoc.Count > 0)
        //        kt = false;
        //    if (kt == false)
        //    {
        //        List<int> _liddon = qdthuoc.Select(p => p.IDDon ?? 0).Distinct().ToList();
        //        var _ldontra = data.DThuocs.Where(p => p.MaBNhan == maBNhan).Where(p => p.PLDV == 1 && p.KieuDon == 2).Where(p => p.NgayKe >= ngayra).Select(p => new { p.NgayKe, p.IDDon }).ToList();

        //        foreach (var item in _ldontra)
        //        {
        //            DateTime ngaykenew = Convert.ToDateTime(item.NgayKe);
        //            var qdthuocnew = (from dt in data.DThuocs.Where(p => p.MaBNhan == maBNhan)
        //                              join dtct in data.DThuoccts.Where(p => p.NgayNhap >= ngaykenew.Date) on dt.IDDon equals dtct.IDDon
        //                              group new { dt, dtct } by new { dt.MaBNhan, dtct.MaDV }
        //                                  into kq
        //                                  select new { kq.Key.MaDV, SoLuong = kq.Sum(p => p.dtct.SoLuong) }).Where(p => p.SoLuong > 0).ToList();
        //            if (qdthuocnew.Count > 0)
        //                kt = false;
        //        }
        //    }

        //    if (kt)//kiểm tra thêm thời gian diễn biến 26-06 đoài
        //    {
        //        var qdienbien = data.DienBiens.Where(p => p.MaBNhan == _mabn).Where(p => p.NgayNhap > ngayra).Select(p => p.NgayNhap).OrderByDescending(p => p).ToList();
        //        if (qdienbien.Count > 0)
        //            kt = false;
        //    }
        //    if (kt)//kiểm tra thêm thời gian hội chẩn 26-06 đoài
        //    {
        //        var qdienbien = data.BBHCs.Where(p => p.MaBNhan == _mabn).Where(p => p.NgayHC > ngayra).Select(p => p.NgayHC).OrderByDescending(p => p).ToList();
        //        if (qdienbien.Count > 0)
        //            kt = false;
        //    }
        //    return kt;
        //}

        private void btnLuu_Click(object sender, EventArgs e)
        {
            //try
            //{
            DateTime a = System.DateTime.Now;
            songaydt = getDaysOfStay(txtNgayVao.DateTime, a);
            int rs;
            int _int_maBN = 0;
            if (Int32.TryParse(txtMaBNhan.Text, out rs))
                _int_maBN = Convert.ToInt32(txtMaBNhan.Text);
            DataContect = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

            // luu bang RaVien
            if (_int_maBN > 0)
            {
                var bn1 = DataContect.DienBiens.Where(p => p.MaBNhan == _mabn).ToList();
                if (bn1.Count > 0)
                {
                    foreach (var item in bn1)
                    {
                        if (item.Ploai == 0 && (item.DienBien1 == null || item.DienBien1 == ""))
                        {
                            XtraMessageBox.Show("Nhập đầy đủ diễn biến bệnh trước khi chuyển viện!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                }
                if (KTLuu())
                {
                    string[] icd = new string[5] { "", "", "", "", "" };
                    string[] benhkhac = new string[5] { "", "", "", "", "" };
                    int makp = 0;
                    _mabn = _int_maBN;
                    var bnkb = DataContect.BNKBs.Where(p => p.MaBNhan == _mabn).OrderByDescending(p => p.IDKB).ToList();
                    var kt = DataContect.RaViens.Where(p => p.MaBNhan == _mabn).ToList();
                    if (kt.Count > 0)
                    {
                        //sửa
                        if (kt.Where(p => p.Status == 2).ToList().Count > 0)
                        {
                            MessageBox.Show("Bệnh nhân đã được nhập ra viện, bạn không thể lưu!");
                        }
                        else
                        {
                            int id = kt.First().IdRaVien;
                            RaVien nhapcv = DataContect.RaViens.Single(p => p.IdRaVien == (id));
                            if (!string.IsNullOrEmpty(txtSo.Text))
                            {
                                nhapcv.SoChuyenVien = int.Parse(txtSo.Text);
                            }
                            if (lupKhoaPhong.EditValue != null && lupKhoaPhong.EditValue.ToString() != "")
                            {
                                makp = Convert.ToInt32(lupKhoaPhong.EditValue);
                            }
                            else
                            {
                                makp = String.IsNullOrEmpty(DungChung.Ham.getMaICDarr(DataContect, _mabn, DungChung.Bien.GetICD, 0)[2]) ? 0 : Convert.ToInt32(DungChung.Ham.getMaICDarr(DataContect, _mabn, DungChung.Bien.GetICD, 0)[2]);


                            }
                            nhapcv.MaKP = makp;
                            if (!string.IsNullOrEmpty(txtMaICD.Text))
                                icd[0] = txtMaICD.EditValue.ToString();
                            if (LupICD2.EditValue != null)
                                icd[1] = LupICD2.EditValue.ToString();
                            if (LupICD3.EditValue != null)
                                icd[2] = LupICD3.EditValue.ToString();
                            if (LupICD4.EditValue != null)
                                icd[3] = LupICD4.EditValue.ToString();
                            string maicd = string.Join(";", icd);
                            nhapcv.MaICD = maicd;
                            benhkhac[0] = txtChanDoan.Text.Trim();
                            benhkhac[1] = txtBenhKhac2.Text.Trim();
                            benhkhac[2] = txtBenhKhac3.Text.Trim();
                            benhkhac[3] = txtBenhKhac4.Text.Trim();
                            maicd = string.Join(";", benhkhac);
                            nhapcv.ChanDoan = maicd;
                            nhapcv.ChanDoanCV = txtChanDoanCV.Text;
                            if (DungChung.Bien.MaBV == "20001")
                            {
                                nhapcv.ChanDoanYHCT = DungChung.Ham.GetMaYHCT(nhapcv.MaICD, _licd10)[1];
                                nhapcv.MaYHCT = DungChung.Ham.GetMaYHCT(nhapcv.MaICD, _licd10)[0];
                            }
                            nhapcv.MaBS = lupBSChuyen.EditValue.ToString();
                            nhapcv.SoNgaydt = Convert.ToInt32(txtSoNgaydt.Text);
                            nhapcv.MaBNhan = _int_maBN;
                            nhapcv.LyDoC = cboLyDoCV.Text;
                            nhapcv.HinhThucC = txtPhuongTienVC.Text;
                            nhapcv.TinhTrangC = txtTinhTrang.Text;
                            nhapcv.LoiDan = txtDauHieuLS.Text;
                            nhapcv.PPDTr = txtHuongDT.Text;
                            nhapcv.Status = 1;
                            if (lupBVC.EditValue != null)
                            {
                                nhapcv.MaBVC = lupBVC.EditValue.ToString();
                            }
                            if (lupNguoiDD.EditValue != null)
                            {
                                nhapcv.MaCB = lupNguoiDD.EditValue.ToString();
                            }
                            if (!string.IsNullOrEmpty(txtNgayCV.Text))
                            {
                                nhapcv.NgayRa = txtNgayCV.DateTime;
                            }
                            if (!string.IsNullOrEmpty(txtNgayVao.Text))
                            {
                                nhapcv.NgayVao = txtNgayVao.DateTime;
                            }
                            if (!string.IsNullOrEmpty(txtSoGT.Text))
                                nhapcv.SoGT = Convert.ToInt32(txtSoGT.Text);
                            nhapcv.KetQua = memoKQCLS.Text;
                            nhapcv.ThuocSD = memoThuocSD.Text;
                            nhapcv.HanC = dt_HanC.DateTime;
                            int MaCK = bnkb.Where(p => p.MaKP == makp).Select(p => p.MaCK).ToList().Count > 0 ? bnkb.Where(p => p.MaKP == makp).Select(p => p.MaCK).ToList().First() : -1;
                            nhapcv.MaCK = MaCK;
                            var idkb = bnkb.Where(p => p.PhuongAn == 2).Select(p => p.IDKB).FirstOrDefault();
                            if (DataContect.SaveChanges() >= 0)
                            {
                                if (idkb != 0)
                                {
                                    BNKB sua = DataContect.BNKBs.Single(p => p.IDKB == idkb);
                                    sua.NgayNghi = txtNgayCV.DateTime;
                                    sua.TTChuyenVienTamThoi = null;
                                    DataContect.SaveChanges();
                                }
                                else
                                {
                                    int IDkb = bnkb.First().IDKB;
                                    BNKB sua = DataContect.BNKBs.Single(p => p.IDKB == IDkb);
                                    sua.NgayNghi = txtNgayCV.DateTime;
                                    sua.PhuongAn = 2;
                                    sua.TTChuyenVienTamThoi = null;
                                    DataContect.SaveChanges();
                                }
                            }
                            btnInPhieu_Click(sender, e);
                            enableText(false);
                            DungChung.Ham._setStatus(_int_maBN, 2);
                        }
                    }
                    else
                    {
                        RaVien nhapcv = new RaVien();

                        DataContect = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                        if (DungChung.Bien.MaBV != "30007")
                        {
                            if (DungChung.Bien.PP_SoCV == 1 || DungChung.Bien.PP_SoCV == 2)
                            {
                                //int r, socv = 0, makpvv = 0;
                                //if (Int32.TryParse(txtSo.Text, out r))
                                //{
                                //    socv = Convert.ToInt32(txtSo.Text);
                                //    DungChung.Ham.SetSoPL(makpvv, socv, 5, -1);
                                //}
                                int r, socv = 0, makpvv = 0;
                                if (DungChung.Bien.PP_SoCV == 1 && lupKhoaPhong.EditValue != null && lupKhoaPhong.EditValue.ToString() != "")
                                    makpvv = Convert.ToInt32(lupKhoaPhong.EditValue);
                                bool ktra = false;
                                if (DungChung.Bien.PP_SoCV == 1 || DungChung.Bien.PP_SoCV == 2)
                                    ktra = true; int socvnew = 0;
                                while (ktra)
                                {
                                    if (Int32.TryParse(txtSo.Text, out r))
                                    {
                                        socv = Convert.ToInt32(txtSo.Text);
                                    }

                                    if (!DungChung.Ham.checkSoPL(makpvv, socv, 5, -1))
                                    {
                                        ktra = false;
                                        DungChung.Ham.SetSoPL(makpvv, socv, 5, -1);
                                    }
                                    else
                                    {
                                        //DungChung.Ham.SetSoPL(makpvv, 0, 5, -1);
                                        socvnew++;
                                        setSoCV(socvnew);
                                        //ktra = false;
                                    }
                                }
                            }
                        }
                        if (!string.IsNullOrEmpty(txtSo.Text))
                        {
                            nhapcv.SoChuyenVien = int.Parse(txtSo.Text);
                        }
                        nhapcv.MaBS = lupBSChuyen.EditValue.ToString();
                        nhapcv.MaBNhan = _int_maBN;
                        nhapcv.LyDoC = cboLyDoCV.Text;
                        nhapcv.HinhThucC = txtPhuongTienVC.Text;
                        nhapcv.TinhTrangC = txtTinhTrang.Text;
                        nhapcv.LoiDan = txtDauHieuLS.Text;
                        nhapcv.PPDTr = txtHuongDT.Text;
                        nhapcv.Status = 1;
                        int songay = 1;
                        if (!string.IsNullOrEmpty(txtSoNgaydt.Text))
                            songay = Convert.ToInt32(txtSoNgaydt.Text);
                        if (songay < 1)
                        {
                            var qbn = DataContect.BenhNhans.Where(p => p.MaBNhan == _mabn).Where(p => p.NoiTru == 1 || p.DTNT == true).FirstOrDefault();
                            if (qbn != null)
                                songay = 1;

                        }
                        nhapcv.SoNgaydt = songay;
                        if (lupKhoaPhong.EditValue != null && lupKhoaPhong.EditValue.ToString() != "")
                        {
                            makp = Convert.ToInt32(lupKhoaPhong.EditValue);
                        }
                        else
                        {
                            makp = String.IsNullOrEmpty(DungChung.Ham.getMaICDarr(DataContect, _mabn, DungChung.Bien.GetICD, 0)[2]) ? 0 : Convert.ToInt32(DungChung.Ham.getMaICDarr(DataContect, _mabn, DungChung.Bien.GetICD, 0)[2]);


                        }
                        nhapcv.MaKP = makp;
                        if (!string.IsNullOrEmpty(txtMaICD.Text))
                            icd[0] = txtMaICD.EditValue.ToString();
                        if (LupICD2.EditValue != null)
                            icd[1] = LupICD2.EditValue.ToString();
                        if (LupICD3.EditValue != null)
                            icd[2] = LupICD3.EditValue.ToString();
                        if (LupICD4.EditValue != null)
                            icd[3] = LupICD4.EditValue.ToString();
                        string maicd = string.Join(";", icd);
                        nhapcv.MaICD = maicd;
                        benhkhac[0] = txtChanDoan.Text.Trim();
                        benhkhac[1] = txtBenhKhac2.Text.Trim();
                        benhkhac[2] = txtBenhKhac3.Text.Trim();
                        benhkhac[3] = txtBenhKhac4.Text.Trim();
                        maicd = string.Join(";", benhkhac);
                        nhapcv.ChanDoan = maicd;
                        nhapcv.ChanDoanCV = txtChanDoanCV.Text;
                        if (DungChung.Bien.MaBV == "20001")
                        {
                            nhapcv.ChanDoanYHCT = DungChung.Ham.GetMaYHCT(nhapcv.MaICD, _licd10)[1];
                            nhapcv.MaYHCT = DungChung.Ham.GetMaYHCT(nhapcv.MaICD, _licd10)[0];
                        }
                        if (lupBVC.EditValue != null)
                        {
                            nhapcv.MaBVC = lupBVC.EditValue.ToString();
                        }
                        if (lupNguoiDD.EditValue != null)
                        {
                            nhapcv.MaCB = lupNguoiDD.EditValue.ToString();
                        }
                        if (!string.IsNullOrEmpty(txtNgayCV.Text))
                        {
                            nhapcv.NgayRa = txtNgayCV.DateTime;
                        }
                        if (!string.IsNullOrEmpty(txtNgayVao.Text))
                        {
                            nhapcv.NgayVao = txtNgayVao.DateTime;
                        }
                        if (!string.IsNullOrEmpty(txtSoGT.Text))
                            nhapcv.SoGT = Convert.ToInt32(txtSoGT.Text);
                        nhapcv.KetQua = memoKQCLS.Text;
                        nhapcv.ThuocSD = memoThuocSD.Text;
                        nhapcv.HanC = dt_HanC.DateTime;
                        int MaCK = bnkb.Where(p => p.MaKP == makp).Select(p => p.MaCK).ToList().Count > 0 ? bnkb.Where(p => p.MaKP == makp).Select(p => p.MaCK).ToList().First() : -1;
                        nhapcv.MaCK = MaCK;
                        DataContect.RaViens.Add(nhapcv);
                        var idkb = bnkb.Where(p => p.PhuongAn == 2).Select(p => p.IDKB).FirstOrDefault();
                        if (DataContect.SaveChanges() >= 0)
                        {
                            if (idkb != 0)
                            {
                                BNKB sua = DataContect.BNKBs.Single(p => p.IDKB == idkb);
                                sua.NgayNghi = txtNgayCV.DateTime;
                                sua.TTChuyenVienTamThoi = null;
                                DataContect.SaveChanges();
                            }
                            else
                            {
                                int IDkb = bnkb.First().IDKB;
                                BNKB sua = DataContect.BNKBs.Single(p => p.IDKB == IDkb);
                                sua.NgayNghi = txtNgayCV.DateTime;
                                sua.PhuongAn = 2;
                                sua.TTChuyenVienTamThoi = null;
                                DataContect.SaveChanges();
                            }
                        }
                        DungChung.Ham._setStatus(_int_maBN, 2);
                        if (DungChung.Bien.MaBV == "01049" && _Dtuong == "BHYT")
                        {
                            if (lupMaBVchuyen.EditValue != null && lupMaBVchuyen.EditValue.ToString() == "01071")
                            {
                                DialogResult Result = MessageBox.Show("Bệnh viện chuyển đến là cơ sở I \nbạn có muốn update chi phí về:không thanh toán ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                if (Result == DialogResult.Yes)
                                {
                                    UpdateChiPhi01049(DataContect, _int_maBN, 0);
                                }
                            }
                        }
                        frm_CVienNoiTru_Load(sender, e);
                        btnInPhieu_Click(sender, e);
                        enableText(false);
                    }
                }
            }
            //}
            //catch (Exception)
            //{
            //    MessageBox.Show("Lỗi lưu chuyển viện");
            //}
            btnInGiayChuyenBenhNhanDTYHCT.Enabled = true;
        }

        private void updateSoChuyenVien()
        {
            throw new NotImplementedException();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtNgayCV_EditValueChanged(object sender, EventArgs e)
        {
            if (DungChung.Bien.MaBV == "33080" && _daco == false)
            {
                txtSo.Text = laySoChuyenVien(txtNgayCV.DateTime, 21).ToString();
            }

            if ((txtNgayCV.DateTime - txtNgayVao.DateTime).Days < 0)
            {
                MessageBox.Show("Ngày ra viện phải >= ngày vào viện!");
                txtNgayCV.Focus();
            }
            else
            {
                {

                    txtSoNgaydt.Text = frm_Ravien._GetSoNgayDTri(DataContect, _mabn, txtNgayVao.DateTime, txtNgayCV.DateTime, "nặng hơn", 3).ToString();

                }
            }
            if ((txtNgayCV.DateTime - txtNgayVao.DateTime).Days < 0)
            {
                MessageBox.Show("Ngày ra viện phải >= ngày vào viện!");
                txtNgayCV.Focus();
            }
            else
            {
                {

                    txtSoNgaydt.Text = frm_Ravien._GetSoNgayDTri(DataContect, _mabn, txtNgayVao.DateTime, txtNgayCV.DateTime, "nặng hơn", 3).ToString();

                }
            }
        }
        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult _result = MessageBox.Show("Bạn muốn xóa thông tin chuyển viện của BN?", "Hỏi xóa!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (_result == DialogResult.Yes)
                {
                    if (!DungChung.Ham.KTraTT(DataContect, _mabn))
                    {
                        var ktcv = DataContect.RaViens.Where(p => p.MaBNhan == _mabn).Where(p => p.Status == 1).ToList();
                        if (ktcv.Count > 0)
                        {
                            int id = ktcv.First().IdRaVien;
                            var xoa = DataContect.RaViens.Single(p => p.IdRaVien == id);

                            //#region update SochuyenVien
                            //List<HTHONG> _lhethong = DataContect.HTHONGs.Where(p => p.MaBV == DungChung.Bien.MaBV).ToList();
                            //if (getSoChuyenVien() > 0 && (getSoChuyenVien() - 1).ToString() == txtSo.Text)
                            //{

                            //    foreach (var ht in _lhethong)
                            //    {
                            //        ht.SoChuyenVien = ht.SoChuyenVien - 1;
                            //    }
                            //    DataContect.SaveChanges();
                            //}
                            //#endregion
                            DataContect.RaViens.Remove(xoa);
                            DataContect.SaveChanges();
                            var q = DataContect.BNKBs.Where(p => p.MaBNhan == _mabn && p.PhuongAn == 2).ToList().FirstOrDefault();
                            if (q != null)
                            {
                                q.PhuongAn = 4;
                                q.NgayNghi = null;

                                DataContect.SaveChanges();
                            }
                            DungChung.Ham._setStatus(String.IsNullOrEmpty(txtMaBNhan.Text) ? 0 : Convert.ToInt32(txtMaBNhan.Text), 1);
                            //insert vào hồ sơ hủy
                            if (DungChung.Bien.PP_SoCV == 1 || DungChung.Bien.PP_SoCV == 2)
                            {

                                int makprv = 0;
                                if (DungChung.Bien.PP_SoVV == 1 && lupKhoaPhong.EditValue != null && lupKhoaPhong.EditValue.ToString() != "")
                                    makprv = Convert.ToInt32(lupKhoaPhong.EditValue);

                                DungChung.Ham.UpdateHSHuy(_mabn, makprv, txtSo.Text, 5, -1);
                                if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049")
                                {
                                    DungChung.Ham.SoPLDeleteChuyenVien(makprv, int.Parse(txtSo.Text), 5, -1);
                                }

                            }
                            if (DungChung.Bien.MaBV == "01049" && _Dtuong == "BHYT")
                            {
                                if (lupMaBVchuyen.EditValue != null && lupMaBVchuyen.EditValue.ToString() == "01071")
                                {
                                    DialogResult Result = MessageBox.Show("Bệnh viện chuyển đến là cơ sở I, Xóa chuyển viện \nbạn có muốn update chi phí về: Trong danh mục ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                    if (Result == DialogResult.Yes)
                                    {
                                        UpdateChiPhi01049(DataContect, _mabn, 1);
                                    }
                                }
                            }
                            frm_CVienNoiTru_Load(sender, e);
                        }
                        enableText(true);
                    }
                    else
                    {
                        MessageBox.Show("BN đã thanh toán, bạn không được phép xóa");
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Lỗi xóa thông tin chuyển viện");
            }
        }

        private void txtSoGT_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSoGT.Text))
            {
                int sogt = Convert.ToInt32(txtSoGT.Text);
                var ktcv = DataContect.RaViens.Where(p => p.Status == 1).Where(p => p.SoGT == sogt).ToList();
                if (ktcv.Count > 0)
                    MessageBox.Show("Số lệnh điều xe đã có, bạn hãy nhập số khác");
            }
        }

        private void txtNgayCV_Leave(object sender, EventArgs e)
        {
        }
        private void enableText(bool T)
        {
            if (DungChung.Bien.MaBV == "30350")
            {
                txtChanDoan.Properties.ReadOnly = !T;
                txtMaICD.Properties.ReadOnly = !T;
            }
            txtBenhKhac2.Properties.ReadOnly = !T;
            txtBenhKhac3.Properties.ReadOnly = !T;
            txtBenhKhac4.Properties.ReadOnly = !T;

            //txtMaICD.Properties.ReadOnly = !T;
            LupICD2.Properties.ReadOnly = !T;
            LupICD3.Properties.ReadOnly = !T;
            LupICD4.Properties.ReadOnly = !T;
            txtChanDoanCV.Properties.ReadOnly = !T;
            txtNgayCV.Properties.ReadOnly = !T;
            txtSoNgaydt.Properties.ReadOnly = !T;
            if (DungChung.Bien.MaBV != "01830" && DungChung.Bien.MaBV != "01071" && DungChung.Bien.MaBV != "01049")
                txtSo.Properties.ReadOnly = !T;
            lupBVC.Properties.ReadOnly = !T;
            txtDauHieuLS.Properties.ReadOnly = !T;
            memoKQCLS.ReadOnly = !T;
            memoThuocSD.Properties.ReadOnly = !T;
            txtTinhTrang.Properties.ReadOnly = !T;
            cboLyDoCV.Properties.ReadOnly = !T;
            txtHuongDT.Properties.ReadOnly = !T;
            txtPhuongTienVC.Properties.ReadOnly = !T;
            lupNguoiDD.Properties.ReadOnly = !T;
            txtSoGT.Properties.ReadOnly = !T;
            dt_HanC.Properties.ReadOnly = !T;
            btnInPhieu.Enabled = !T;
            btnLuu.Enabled = T;
            btnXoa.Enabled = true;
            btnSua.Enabled = !T;
            btnThoat.Enabled = true;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            var ktTT = DataContect.VienPhis.Where(p => p.MaBNhan == _mabn).ToList();
            if (ktTT.Count > 0)
            {
                MessageBox.Show("Không thể sửa! Bệnh nhân đã thanh toán. ");
            }
            else
                enableText(true);
        }

        private void lupBVC_DoubleClick(object sender, EventArgs e)
        {
            MessageBox.Show("Để hiện thị được danh sách bệnh viện, trong danh mục bệnh viện trường 'status'=3");
        }

        private void txtPhuongTienVC_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void lupKhoaPhong_EditValueChanged(object sender, EventArgs e)
        {
            var kt = DataContect.RaViens.Where(p => p.MaBNhan == _mabn).Where(p => p.Status == 1).ToList();
            if (kt.Count == 0)
            {
                setSoCV(0);
            }
            if (lupKhoaPhong.EditValue != null)
            {
                List<CanBo> qcb = new List<CanBo>();
                int makp = Convert.ToInt32(lupKhoaPhong.EditValue);
                string _makp = makp.ToString();
                qcb = DataContect.CanBoes.Where(p => p.Status == 1 && p.MaKPsd.Contains(_makp)).ToList();
                lupBSChuyen.Properties.DataSource = qcb.ToList();
                qcb.Insert(0, new CanBo { TenCB = "Người nhà", MaCB = "0", CapBac = " " });
                lupNguoiDD.Properties.DataSource = qcb.ToList();
            }
            else
                lupBSChuyen.Properties.DataSource = null;
        }

        private void frm_CVienNoiTru_FormClosing(object sender, FormClosingEventArgs e)
        {
            DataContect = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var kt = DataContect.RaViens.Where(p => p.MaBNhan == _mabn).Where(p => p.Status == 1).ToList();
            if (kt.Count <= 0)
            {
                var update = DataContect.BNKBs.Where(p => p.MaBNhan == _mabn && p.PhuongAn == 2).ToList();
                foreach (var a in update)
                {
                    a.PhuongAn = 4;
                    a.NgayNghi = null;
                    a.MaKPdt = 0;
                    DataContect.SaveChanges();
                }
            }
        }

        private void lupBVC_EditValueChanged(object sender, EventArgs e)
        {
            lupMaBVchuyen.EditValue = lupBVC.EditValue;
        }

        private void lupMaBVchuyen_EditValueChanged(object sender, EventArgs e)
        {
            lupBVC.EditValue = lupMaBVchuyen.EditValue;

        }
        private void UpdateChiPhi01049(QLBV_Database.QLBVEntities _data, int MaBN, int _status)
        {
            bool kt = true;
            var vp = _data.VienPhis.Where(p => p.MaBNhan == MaBN).ToList();
            if (vp.Count > 0)
            {
                kt = false;
            }
            if (kt)
            {
                int dem = 0;
                List<DThuocct> _ldtct = (from dt in _data.DThuocs.Where(p => p.MaBNhan == _mabn)
                                         join dtct in _data.DThuoccts.Where(p => _status == 0 ? p.TrongBH == 1 : p.TrongBH == 2) on dt.IDDon equals dtct.IDDon
                                         select dtct).ToList();
                foreach (var item in _ldtct)
                {
                    item.TrongBH = _status == 0 ? 2 : 1;
                    dem++;
                }
                _data.SaveChanges();
                if (dem > 0)
                {
                    MessageBox.Show("Đã cập nhật " + dem.ToString() + " dịch vụ");
                }
                else
                {
                    MessageBox.Show("Không có dịch vụ để cập nhật");
                }
            }
            else
            {
                MessageBox.Show("Bệnh nhân đã thanh toán không thể cập nhật");
            }
        }
        private void txtHuongDT_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void txtMaICD_EditValueChanged(object sender, EventArgs e)
        {
            if (txtMaICD.EditValue != null && txtMaICD.EditValue.ToString() != "")
            {
                txtMaICD.Properties.Buttons[1].Visible = true;
                txtChanDoan.EditValue = lICD.Where(p => p.MaICD == txtMaICD.EditValue.ToString()).Select(p => p.TenICD).FirstOrDefault();
            }
            else
            {
                txtMaICD.Properties.Buttons[1].Visible = false;
                txtChanDoan.EditValue = "";
            }
        }
        private void txtMaICD_Changed(object sender, EventArgs e)
        {

        }
        private void LupICD2_EditValueChanged(object sender, EventArgs e)
        {
            if (LupICD2.EditValue != null && LupICD2.EditValue.ToString() != "")
            {
                LupICD2.Properties.Buttons[1].Visible = true;
                txtBenhKhac2.EditValue = lICD.Where(p => p.MaICD == LupICD2.EditValue.ToString()).Select(p => p.TenICD).FirstOrDefault();
            }
            else
            {
                LupICD2.Properties.Buttons[1].Visible = false;
                txtBenhKhac2.EditValue = "";
            }
        }

        private void LupICD3_EditValueChanged(object sender, EventArgs e)
        {
            if (LupICD3.EditValue != null && LupICD3.EditValue.ToString() != "")
            {
                LupICD3.Properties.Buttons[1].Visible = true;
                txtBenhKhac3.EditValue = lICD.Where(p => p.MaICD == LupICD3.EditValue.ToString()).Select(p => p.TenICD).FirstOrDefault();
            }
            else
            {
                LupICD3.Properties.Buttons[1].Visible = false;
                txtBenhKhac3.EditValue = "";
            }
        }
        public void getICD4(string _maicd)
        {
            string a = "";
            if (!string.IsNullOrEmpty(LupICD4.Text.Trim()))
                a = ";";
            LupICD4.Text += a + _maicd;
            string[] iCD = LupICD4.Text.Split(';');
            string[] tenICD = lICD.Where(p => iCD.Contains(p.MaICD)).Select(p => p.TenICD).ToArray();

            txtBenhKhac4.Text = string.Join(";", tenICD);

        }
        private void LupICD4_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void LupICD4_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.F9)
            {
                FormThamSo.frm_TimKiem frm = new FormThamSo.frm_TimKiem();
                frm.GetData = new FormThamSo.frm_TimKiem._getstring(getICD4);
                frm.ShowDialog();

            }
        }

        private void txtBenhKhac4_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.F9)
            {
                FormThamSo.frm_TimKiem frm = new FormThamSo.frm_TimKiem();
                frm.GetData = new FormThamSo.frm_TimKiem._getstring(getICD4);
                frm.ShowDialog();
            }
        }

        private void txtChanDoan_Changed(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(txtChanDoan.Text))
            {
                string chandoan = txtChanDoan.Text;
                var qmaicd = (from idcd in lICD.Where(p => p.TenICD == chandoan) select idcd.MaICD).FirstOrDefault();

                if (qmaicd != null)
                    txtMaICD.EditValue = qmaicd;
                else
                {
                    if (DungChung.Bien.MaBV != "30350")
                        txtMaICD.EditValue = "";
                }
            }
            else
            {
                txtMaICD.EditValue = "";
            }

            if (DungChung.Bien.MaBV == "30350")
            {
                txtChanDoanCV.Text = "";
                if (txtChanDoan.Text != null && txtChanDoan.Text != "")
                    txtChanDoanCV.Text += txtChanDoan.Text;
                if (txtBenhKhac2.Text != null && txtBenhKhac2.Text != "")
                    txtChanDoanCV.Text += "; " + txtBenhKhac2.Text;
                if (txtBenhKhac3.Text != null && txtBenhKhac3.Text != "")
                    txtChanDoanCV.Text += "; " + txtBenhKhac3.Text;
                if (txtBenhKhac4.Text != null && txtBenhKhac4.Text != "")
                    txtChanDoanCV.Text += "; " + txtBenhKhac4.Text;
            }
        }

        private void txtChanDoan_Leave(object sender, EventArgs e)
        {
            txtChanDoan_Changed(null, null);

        }

        private void txtMaICD_Leave(object sender, EventArgs e)
        {
            txtMaICD_Changed(null, null);
        }

        private void txtBenhKhac2_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtChanDoan.Text))
            {
                if (txtBenhKhac2.EditValue.ToString() == "0")
                {
                    txtBenhKhac2.EditValue = "";
                    LupICD2.EditValue = "";
                }
                else
                    LupICD2.EditValue = lICD.Where(p => p.TenICD == txtBenhKhac2.EditValue.ToString()).Select(p => p.MaICD).FirstOrDefault();

            }
            else
            {
                LupICD2.EditValue = "";
            }
        }

        private void txtBenhKhac3_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtBenhKhac3.Text))
            {
                if (txtBenhKhac3.EditValue.ToString() == "0")
                {
                    txtBenhKhac3.EditValue = "";
                    LupICD3.EditValue = "";
                }
                else
                    LupICD3.EditValue = lICD.Where(p => p.TenICD == txtBenhKhac3.EditValue.ToString()).Select(p => p.MaICD).FirstOrDefault();

            }
            else
            {
                LupICD3.EditValue = "";
            }
        }

        private void LupICD4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnInGiayChuyenBenhNhanDTYHCT_Click(object sender, EventArgs e)
        {
            InGiayChuyenBenhNhanDieuTriYHCT();
        }
        public static DateTime NgayVao, GioVao; public static string GTinh;
        void InGiayChuyenBenhNhanDieuTriYHCT()
        {
            frmIn frm = new frmIn();
            var benhNhanChuyenVien = (from bn in DataContect.BenhNhans.Where(p => p.MaBNhan == _mabn)
                                      join bnkb in DataContect.BNKBs.Where(p => p.PhuongAn == 2) on bn.MaBNhan equals bnkb.MaBNhan
                                      join TTBX in DataContect.TTboXungs on bnkb.MaBNhan equals TTBX.MaBNhan
                                      join Vv in DataContect.VaoViens on bn.MaBNhan equals Vv.MaBNhan
                                      join cb in DataContect.CanBoes on bnkb.MaCB equals cb.MaCB
                                      select new
                                      {
                                          TenBN = bn.TenBNhan,
                                          Tuoi = bn.Tuoi,
                                          GioiTinh = bn.GTinh,
                                          SoVaoVien = Vv.SoVV,
                                          SoTheBH = bn.SThe,
                                          diaChi = bn.DChi,
                                          NgayGioVaoKhoa = Vv.NgayVao,
                                          buong = bnkb.Buong,
                                          giuong = bnkb.Giuong,
                                          mach = Vv.Mach,
                                          nhietDo = Vv.NhietDo,
                                          huyetAp = Vv.HuyetAp,
                                          nhipTho = Vv.NhipTho,
                                          canNang = Vv.CanNang,
                                          trieuchung = bn.TChung,
                                          chanDoan = bnkb.ChanDoan,
                                          bacSiDieuTri = cb.TenCB,
                                      }).ToList();

            if (benhNhanChuyenVien.Count > 0)
            {
                string NgayGioVao = benhNhanChuyenVien.Select(p => p.NgayGioVaoKhoa).First().ToString();
                string[] Time = NgayGioVao.Split(' ');
                NgayVao = Convert.ToDateTime(Time[0].ToString());
                GioVao = Convert.ToDateTime(Time[1].ToString());
                string Gtinh = benhNhanChuyenVien.Select(p => p.GioiTinh).First().ToString();
                if (Gtinh == "1")
                {
                    GTinh = "Nam";
                }
                else
                {
                    GTinh = "Nữ";
                }
                rep_GiayChuyenBenhNhanDieuTriYHocCoTruyen rep = new rep_GiayChuyenBenhNhanDieuTriYHocCoTruyen();
                rep.lblNgayThang.Text = DungChung.Ham.NgaySangChu(DateTime.Now);
                rep.DataSource = benhNhanChuyenVien.ToList();
                rep.databindind();
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
            }
        }

        private void lupBSChuyen_Validated(object sender, EventArgs e)
        {
            if (lupBSChuyen.EditValue != null)
                _capbac = lupBSChuyen.Properties.GetKeyValueByDisplayText(lupBSChuyen.Text).ToString();
        }

        private void txtChanDoan_EditValueChanged(object sender, EventArgs e)
        {
            if (txtChanDoan.EditValue != null && txtChanDoan.EditValue.ToString() != "")
            {
                txtChanDoan.Properties.Buttons[1].Visible = true;
            }
            else
            {
                txtChanDoan.Properties.Buttons[1].Visible = false;
                txtMaICD.EditValue = "";
            }
        }

        private void txtBenhKhac2_EditValueChanged(object sender, EventArgs e)
        {
            if (txtBenhKhac2.EditValue != null && txtBenhKhac2.EditValue.ToString() != "")
            {
                txtBenhKhac2.Properties.Buttons[1].Visible = true;
            }
            else
            {
                txtBenhKhac2.Properties.Buttons[1].Visible = false;
                LupICD2.EditValue = "";
            }
        }

        private void txtBenhKhac3_EditValueChanged(object sender, EventArgs e)
        {
            if (txtBenhKhac3.EditValue != null && txtBenhKhac3.EditValue.ToString() != "")
            {
                txtBenhKhac3.Properties.Buttons[1].Visible = true;
            }
            else
            {
                txtBenhKhac3.Properties.Buttons[1].Visible = false;
                LupICD3.EditValue = "";
            }
        }

        private void txtBenhKhac4_EditValueChanged(object sender, EventArgs e)
        {
            if (txtBenhKhac4.EditValue != null && txtBenhKhac4.EditValue.ToString() != "")
            {
                txtBenhKhac4.Properties.Buttons[1].Visible = true;
            }
            else
            {
                txtBenhKhac4.Properties.Buttons[1].Visible = false;
            }
        }

        private void LupICD4_EditValueChanged(object sender, EventArgs e)
        {
            if (LupICD4.EditValue != null && LupICD4.EditValue.ToString() != "")
            {
                LupICD4.Properties.Buttons[1].Visible = true;
            }
            else
            {
                LupICD4.Properties.Buttons[1].Visible = false;
            }
        }

        private void txtChanDoan_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                txtChanDoan.EditValue = "";
            }
        }

        private void txtBenhKhac2_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                txtBenhKhac2.EditValue = "";
            }
        }

        private void txtBenhKhac3_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                txtBenhKhac3.EditValue = "";
            }
        }

        private void txtBenhKhac4_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                txtBenhKhac4.EditValue = "";
            }
        }

        private void txtMaICD_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                txtMaICD.EditValue = "";
            }
        }

        private void LupICD2_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                LupICD2.EditValue = "";
            }
        }

        private void btnLuuTamThoi_Click(object sender, EventArgs e)
        {
            TTRaVienTamThoi rvtt = new TTRaVienTamThoi();
            rvtt.KPDieutri = lupKhoaPhong.EditValue != null ? lupKhoaPhong.EditValue.ToString() : "";
            rvtt.BSDieuTri = lupBSChuyen.EditValue != null ? lupBSChuyen.EditValue.ToString() : "";
            rvtt.ChanDoan = txtChanDoan.EditValue != null ? txtChanDoan.EditValue.ToString() : "";
            rvtt.BenhKhac2 = txtBenhKhac2.EditValue != null ? txtBenhKhac2.EditValue.ToString() : "";
            rvtt.BenhKhac3 = txtBenhKhac3.EditValue != null ? txtBenhKhac3.EditValue.ToString() : "";
            rvtt.BenhKhac4 = txtBenhKhac4.EditValue != null ? txtBenhKhac4.EditValue.ToString() : "";
            rvtt.ChanDoanCV = txtChanDoanCV.EditValue != null ? txtChanDoanCV.EditValue.ToString() : "";
            rvtt.NgayCV = txtNgayCV.EditValue != null ? txtNgayCV.EditValue.ToString() : "";
            rvtt.SoNgayDT = txtSoNgaydt.EditValue != null ? txtSoNgaydt.EditValue.ToString() : "";
            rvtt.SoChuyenTuyen = txtSo.EditValue != null ? txtSo.EditValue.ToString() : "";
            rvtt.CoQuan = lupMaBVchuyen.EditValue != null ? lupMaBVchuyen.EditValue.ToString() : "";
            rvtt.DauHieuLS = txtDauHieuLS.EditValue != null ? txtDauHieuLS.EditValue.ToString() : "";
            rvtt.KetQuaKLS = memoKQCLS.Text != null ? memoKQCLS.Text : "";
            rvtt.ThuocSD = memoThuocSD.EditValue != null ? memoThuocSD.EditValue.ToString() : "";
            rvtt.TinhTrang = txtTinhTrang.EditValue != null ? txtTinhTrang.EditValue.ToString() : "";
            rvtt.LyDoCV = cboLyDoCV.SelectedIndex != null ? cboLyDoCV.SelectedIndex : 0;
            rvtt.HuongDT = txtHuongDT.EditValue != null ? txtHuongDT.EditValue.ToString() : "";
            rvtt.PhuongTienVC = txtPhuongTienVC.EditValue != null ? txtPhuongTienVC.EditValue.ToString() : "";
            rvtt.SoGT = txtSoGT.EditValue != null ? txtSoGT.EditValue.ToString() : "";
            rvtt.NguoiDD = lupNguoiDD.EditValue != null ? lupNguoiDD.EditValue.ToString() : "";
            rvtt.HanC = dt_HanC.EditValue != null ? dt_HanC.EditValue.ToString() : "";

            rvtt.Ma1 = txtMaICD.EditValue != null ? txtMaICD.EditValue.ToString() : "";
            rvtt.Ma2 = LupICD2.EditValue != null ? LupICD2.EditValue.ToString() : "";
            rvtt.Ma3 = LupICD3.EditValue != null ? LupICD3.EditValue.ToString() : "";
            rvtt.Ma4 = LupICD4.EditValue != null ? LupICD4.EditValue.ToString() : "";

            int rs;
            int _int_maBN = 0;
            if (Int32.TryParse(txtMaBNhan.Text, out rs))
                _int_maBN = Convert.ToInt32(txtMaBNhan.Text);
            DataContect = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var bnkb = DataContect.BNKBs.Where(p => p.MaBNhan == _mabn).OrderByDescending(p => p.IDKB).ToList();
            string summ = "";
            summ = rvtt.KPDieutri + "|" + rvtt.BSDieuTri + "|" + rvtt.ChanDoan + "|" + rvtt.BenhKhac2 + "|" + rvtt.BenhKhac3 + "|" + rvtt.BenhKhac4 + "|" + rvtt.ChanDoanCV + "|" + rvtt.NgayCV + "|" + rvtt.SoNgayDT + "|" + rvtt.SoChuyenTuyen + "|" + rvtt.CoQuan + "|" + rvtt.DauHieuLS + "|" + rvtt.KetQuaKLS + "|" + rvtt.ThuocSD + "|" + rvtt.TinhTrang + "|" + rvtt.LyDoCV + "|" + rvtt.HuongDT + "|" + rvtt.PhuongTienVC + "|" + rvtt.SoGT + "|" + rvtt.NguoiDD + "|" + rvtt.HanC + "|"+ rvtt.Ma1 + "|" + rvtt.Ma2 + "|"+ rvtt.Ma3 + "|" + rvtt.Ma4;

            if (bnkb.Count > 0)
            {
                frmIn frm = new frmIn();
                bnkb.First().TTChuyenVienTamThoi = summ;
                if (DataContect.SaveChanges() > 0)
                {
                    MessageBox.Show("Lưu thông tin tạm thành công!", "Thông báo!!");
                    BaoCao.repGiayCVNoiTru_TT146 rep = new BaoCao.repGiayCVNoiTru_TT146();

                    if (Int32.TryParse(txtMaBNhan.Text, out rs))
                        _int_maBN = Convert.ToInt32(txtMaBNhan.Text);
                    var par = DataContect.BenhNhans.Where(p => p.MaBNhan == _int_maBN).ToList();
                    DateTime dt7 = Convert.ToDateTime(txtNgayCV.Text);
                    if (DungChung.Bien.MaBV == "30003")
                    {
                        rep.Ngaythang.Value = "Ngày " + dt7.Day.ToString() + " tháng " + dt7.Month.ToString() + " năm " + dt7.Year.ToString();
                    }
                    else
                    {
                        rep.Ngaythang.Value = "Ngày ..... tháng ..... năm 20...";
                    }
                    if (par.Count > 0)
                    {
                        // thông tin hành chính
                        var tuyenbv = DataContect.BenhViens.Where(p => p.MaBV == (DungChung.Bien.MaBV)).Select(p => p.TuyenBV).ToList();
                        if (tuyenbv.Count > 0)
                        {
                            string tbv = "";
                            if (tuyenbv.First() != null)
                            {
                                tbv = tuyenbv.First().ToString().Trim();
                                switch (tbv)
                                {
                                    case "A":
                                        rep.TuyenBV.Value = "(Tuyến 01)";
                                        break;
                                    case "B":
                                        rep.TuyenBV.Value = "(Tuyến 02)";
                                        break;
                                    case "C":
                                        rep.TuyenBV.Value = "(Tuyến 03)";
                                        break;
                                    case "D":
                                        rep.TuyenBV.Value = "(Tuyến 04)";
                                        break;
                                }
                            }
                        }
                        rep.HoTenBN.Value = par.First().TenBNhan.ToUpper();
                        rep.DiaChi.Value = par.First().DChi;
                        rep.TuoiBN.Value = DungChung.Bien.MaBV == "14018" ? DungChung.Ham.TinhTuoiBenhNhan(DataContect, _int_maBN) : DungChung.Ham.TuoitheoThang(DataContect, _mabn, DungChung.Bien.formatAge);

                        if (par.First().GTinh != null)
                        {
                            if (par.First().GTinh.ToString() == "1")
                                rep.GioiTinh.Value = "Nam";
                            else
                                rep.GioiTinh.Value = "Nữ";
                        }
                        rep.Macs.Value = par.First().MaCS;
                        rep.SHBHYT.Value = par.First().SThe;
                        rep.GiatriBHYT.Value = par.First().HanBHTu;
                        rep.GiatriBHYTden.Value = par.First().HanBHDen;
                        var ttbxug = (from tt in DataContect.TTboXungs.Where(p => p.MaBNhan == _mabn)
                                        join dt in DataContect.DanTocs on tt.MaDT equals dt.MaDT
                                        select new { dt.TenDT, tt.NgoaiKieu, tt.MaNN, tt.NoiLV }).ToList();
                        if (ttbxug.Count > 0)
                        {
                            rep.NgoaiKieu.Value = ttbxug.First().NgoaiKieu;
                            string mann = "";
                            if (ttbxug.First().MaNN != null)
                                mann = ttbxug.First().MaNN;
                            var nn = DataContect.DmNNs.Where(p => p.MaNN == (mann)).ToList();
                            if (nn.Count > 0)
                                rep.NgheNghiep.Value = nn.First().TenNN;
                            rep.NoiLV.Value = ttbxug.First().NoiLV;
                            rep.DanToc.Value = ttbxug.First().TenDT;
                        }
                        else
                        {
                            rep.NgoaiKieu.Value = "Việt Nam";
                        }
                        if (par.First().MaBV != null && par.First().MaBV.ToString() != "")
                        {
                            string tbvt = "";
                            string mabv = par.First().MaBV;
                            var bv = DataContect.BenhViens.Where(p => p.MaBV == (mabv)).ToList();
                            if (bv.Count > 0)
                            {
                                rep.BVtuyentruoc.Value = bv.First().TenBV;

                                if (bv.First().TuyenBV != null)
                                    tbvt = bv.First().TuyenBV.ToString().Trim();
                            }
                            switch (tbvt)
                            {
                                case "A":
                                    rep.TuyenTruoc.Value = "(Tuyến 01)";
                                    break;
                                case "B":
                                    rep.TuyenTruoc.Value = "(Tuyến 02)";
                                    break;
                                case "C":
                                    rep.TuyenTruoc.Value = "(Tuyến 03)";
                                    break;
                                case "D":
                                    rep.TuyenTruoc.Value = "(Tuyến 04)";
                                    break;
                            }
                        }
                        string[] listStr = {"",""};
                        var kb = DataContect.BNKBs.Where(p => p.MaBNhan == _mabn).OrderByDescending(p => p.IDKB).ToList();
                        if (kb.Count > 0)
                        {
                            string str = kb.First().TTChuyenVienTamThoi;
                            listStr = str.Split('|');
                        }
                        string mabsdt = "";
                        if (listStr.Length > 0)
                        {
                            mabsdt = listStr[1];
                        }
                        else
                        {
                            if (kb.Count > 0)
                            {
                                mabsdt = kb.First().MaCB;
                            }
                        }
                        var cb = DataContect.CanBoes.Where(p => p.MaCB == mabsdt).FirstOrDefault();
                        var _capbaccb = DataContect.CanBoes.Where(p => p.MaCB == _capbac).FirstOrDefault();

                        if (cb != null)
                            rep.HoTenBS.Value = cb.TenCB;
                        if (par.First().NoiTru == 1)
                        {
                            var tt = (from vv in DataContect.VaoViens.Where(p => p.MaBNhan == _mabn) select vv).ToList();
                            if (tt.Count > 0)
                            {
                                rep.Ngaytu.Value = tt.First().NgayVao;
                            }
                        }
                        else
                        {
                            if (kb.Count > 0)
                            {
                                rep.Ngaytu.Value = kb.First().NgayKham;
                            }
                        }

                        if (listStr.Length > 0)
                        {
                            string mabvc = "";
                            if (listStr[1] != null)
                            {
                                mabvc = listStr[10];
                                var bvc = DataContect.BenhViens.Where(p => p.MaBV == (mabvc)).Select(p => p.TenBV).ToList();
                                if (bvc.Count > 0)
                                    rep.TenBvchuyen.Value = "Kính gửi: " + bvc.First();
                            }

                            if (par.First().NoiTru == 1)
                                rep.SoHS.Value = listStr[9];
                            rep.Dauhieuls.Value = listStr[11];//in đậm cho BV tam đường
                            if (listStr[7] != null)
                            {
                                rep.ngayden.Value = listStr[7].Substring(0, 10);
                            }
                            rep.TinhTrangBN.Value = listStr[14];
                            if (listStr[15] != null)
                            {
                                string lydo = listStr[15].ToLower();
                                if (lydo.Contains("đúng tuyến"))
                                {
                                    rep.txt2.Visible = true;
                                    rep.txt1.Visible = false;
                                }

                                else
                                {
                                    rep.txt2.Visible = false;
                                    rep.txt1.Visible = true;
                                }
                            }
                            rep.ChuanD.Value = "- Chẩn đoán: " + listStr[6];


                            rep.HuongDT.Value = listStr[16];//in đậm cho BV tam đường
                            rep.SoLuu.Value = listStr[9];
                            if (listStr[7] != null)
                                rep.NgayChuyen.Value = Convert.ToDateTime(listStr[7]).Hour + " giờ " + Convert.ToDateTime(listStr[7]).Minute + " phút, ngày " + Convert.ToDateTime(listStr[7]).Day + " tháng " + Convert.ToDateTime(listStr[7]).Month + " năm " + Convert.ToDateTime(listStr[7]).Year;
                            rep.PhuongTien.Value = listStr[17];
                            int makp = Convert.ToInt32(listStr[0]);
                            var kp = DataContect.KPhongs.Where(p => p.MaKP == makp).ToList();
                            if (kp.Count > 0)
                            {
                                rep.p_TenKP.Value = kp.First().TenKP;
                            }

                            if (listStr[15] != null)
                            {
                                string lydo = listStr[15];
                                if (lydo == "0")
                                {
                                    rep.txt2.Visible = true;
                                    rep.txt1.Visible = false;
                                }
                                else
                                {
                                    rep.txt2.Visible = false;
                                    rep.txt1.Visible = true;
                                }
                            }
                            // tên người hộ tống
                            string macb = "";
                            if (listStr[19] != null)
                            {
                                macb = listStr[19];
                                var cb1 = DataContect.CanBoes.Where(p => p.MaCB == (macb)).ToList();
                                if (cb1.Count > 0 && cb1.First().TenCB != null && cb1.First().TenCB.Trim() != "")
                                {
                                    rep.HoTenNC.Value = cb1.First().CapBac + "  " + cb1.First().TenCB;
                                }
                                else
                                {
                                    rep.HoTenNC.Value = lupNguoiDD.Text;
                                }
                            }
                            rep.CacXN.Value = "- Kết quả xét nghiệm, cận lâm sàng: " + listStr[12];//in đậm cho BV tam đường
                        }

                        rep.ThuocDD.Value = "- Phương pháp, thủ thuật, kỹ thuật, thuốc đã sử dụng trong điều trị: " + listStr[13];
                        rep.SoHS.Value = _mabn;
                    }
                    //

                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                }
            }
        }
        private class TTRaVienTamThoi
        {
            public string KPDieutri { get; set; }
            public string BSDieuTri { get; set; }
            public string ChanDoan { get; set; }
            public string BenhKhac2 { get; set; }
            public string BenhKhac3 { get; set; }
            public string BenhKhac4 { get; set; }
            public string ChanDoanCV { get; set; }
            public string NgayCV { get; set; }
            public string SoNgayDT { get; set; }
            public string SoChuyenTuyen { get; set; }
            public string CoQuan { get; set; }
            public string DauHieuLS { get; set; }
            public string KetQuaKLS { get; set; }
            public string ThuocSD { get; set; }
            public string TinhTrang { get; set; }
            public int? LyDoCV { get; set; }
            public string HuongDT { get; set; }
            public string PhuongTienVC { get; set; }
            public string SoGT { get; set; }
            public string NguoiDD { get; set; }
            public string HanC { get; set; }
            public string Ma1 { get; set; }
            public string Ma2 { get; set; }
            public string Ma3 { get; set; }
            public string Ma4 { get; set; }
        }
        private void LupICD3_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                LupICD3.EditValue = "";
            }
        }

        private void LupICD4_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                LupICD4.EditValue = "";
            }
        }
    }
}