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
using DevExpress.XtraEditors.Controls;

namespace QLBV.FormNhap
{
    public partial class frm_Ravien : DevExpress.XtraEditors.XtraForm
    {
        //ngày 07/11_ Đoài yc thêm req_1601_12122_ số lưu trữ duyệt bệnh ánh:(cho phép tăng tự động, cho phép hủy khi số lưu trữ đang là số lưu trữ cuối cùng)
        int _mabn = 0;
        int idKb = 0;

        public frm_Ravien(int mabn)
        {

            InitializeComponent();
            _mabn = mabn;
        }
        public frm_Ravien(int mabn, int _idKb)
        {

            InitializeComponent();
            _mabn = mabn;
            idKb = _idKb;
        }
        private void Enablebutton(bool Status)
        {
            btnLuu.Enabled = Status;
            btnInPhieu.Enabled = Status;
            //btnSua.Enabled = Status;
            btnThoat.Enabled = Status;
        }

        List<RaVien> _ravien = new List<RaVien>();

        //QLBVEntities DataContect = new QLBVEntities(DungChung.Bien.StrCon);
        QLBVEntities _data = new QLBVEntities(DungChung.Bien.StrCon);
        bool _vaovien = false;
        bool _cotheluu = true;
        int _noitr = -1;
        class mayte
        {
            string mabv; string _mayte;
            public string MaBV
            {
                set { mabv = value; }
                get { return mabv; }
            }
            public string MaYTe
            {
                set { _mayte = value; }
                get { return _mayte; }
            }
        }
        private string _TaoMaYTe(string _mabv)
        {
            string _mayte = "";
            List<mayte> _lMaYTe = new List<mayte>();
            _lMaYTe.Add(new mayte { MaBV = "30003", MaYTe = "107/057/" + System.DateTime.Now.ToString("yy") });
            //if (DungChung.Bien.MaBV == "27021" && _mabn > 0)
            //{
            //    var a = _data.VaoViens.Where(panelControl1 => panelControl1.MaBNhan == _mabn).FirstOrDefault();
            //    if (a != null)
            //    {
            //        string nam = a.NgayVao.Value.Year.ToString().Substring(2, 2);
            //        mayte moi = new mayte();
            //        moi.MaBV = "27021";
            //        moi.MaYTe = "273.021." + nam + "." + (a.SoVV == null? "000000" : Convert.ToInt32(a.SoVV).ToString("D6") );
            //        _lMaYTe.Add(moi);
            //    } 
            //}

            if (DungChung.Bien.MaBV == "27021" && _mabn > 0)
            {
                var a = _data.BenhNhans.Where(p => p.MaBNhan == _mabn).FirstOrDefault();
                if (a != null)
                {
                    string nam = a.NNhap.Value.Year.ToString().Substring(2, 2);
                    mayte moi = new mayte();
                    moi.MaBV = "27021";
                    moi.MaYTe = "223.4.9." + nam + "." + (a.SoKB == null ? "000001" : a.SoKB.ToString());
                    _lMaYTe.Add(moi);
                }
            }
            if (_lMaYTe.Where(p => p.MaBV == _mabv).Count() > 0)
                _mayte = _lMaYTe.Where(p => p.MaBV == _mabv).First().MaYTe;

            return _mayte;
        }
        private void _setPPDT()
        {
            var a = _data.RaViens.Where(p => p.PPDTr != null).Select(p => p.PPDTr).Distinct().OrderBy(p => p).ToList();
            txtPPDTr.Properties.Items.AddRange(a);
        }
        private void _setLDBS()
        {
            var a = _data.RaViens.Where(p => p.LoiDan != null).Select(p => p.LoiDan).Distinct().OrderBy(p => p).ToList();
            txtLoiDan.Properties.Items.AddRange(a);
        }

        int _soLT = 0;
        void setSoHS()
        {
            int noingoaitru = -1;
            _soLT = 0;
            if (DungChung.Bien.MaBV == "27021")
            {
                var qbn = _data.BenhNhans.Where(p => p.MaBNhan == _mabn).FirstOrDefault();
                if (qbn != null)
                    noingoaitru = qbn.NoiTru ?? 0;
            }
            if (DungChung.Bien.PP_SoLT == 1)
            {
                int makp = 0;
                if (lupKPDT.EditValue != null && lupKPDT.EditValue.ToString() != "")
                {
                    makp = Convert.ToInt32(lupKPDT.EditValue);
                    _soLT = DungChung.Ham.GetSoPL(7, makp, noingoaitru);
                }
                else
                {
                    MessageBox.Show("chưa chọn khoa khám bệnh, không lấy được số khám bệnh");
                }
            }
            else if (DungChung.Bien.PP_SoLT == 2)
            {
                _soLT = DungChung.Ham.GetSoPL(7, 0, noingoaitru);
            }
            if (_soLT > 0)
            {
                if (DungChung.Bien.MaBV == "20001")
                    txtSoLT.Text = getTenVietTatKP() + _soLT.ToString();

                else if (DungChung.Bien.MaBV == "27021")
                {
                    if (noingoaitru == 0)
                        txtSoLT.Text = "NgoaiT" + _soLT.ToString();
                    else if (noingoaitru == 1)
                        txtSoLT.Text = "NT" + _soLT.ToString();
                }
                else
                    txtSoLT.Text = _soLT.ToString();
            }
        }

        private string getTenVietTatKP()
        {
            string kp = lupKPDT.Text;
            if (string.IsNullOrEmpty(lupKPDT.Text))
                return "";
            else
            {
                string rs = "";
                List<string> tenkp = lupKPDT.Text.Split(' ').ToList();
                foreach (string a in tenkp)
                {
                    if (a.Length > 0)
                        rs = rs + a.Substring(0, 1).ToUpper();
                }
                return rs;
            }
        }

        private void GetSoLT01071()
        {
            string kq = "";
            int _makp = 0;
            if (lupKPDT.EditValue != null)
                _makp = Convert.ToInt32(lupKPDT.EditValue);
            DateTime ngayvao = txtNgayRa.DateTime;
            int nam = ngayvao.Year, manam = 0;
            switch (nam)
            {
                case 2016:
                    manam = 16;
                    break;
                case 2017:
                    manam = 17;
                    break;
                case 2018:
                    manam = 18;
                    break;
                case 2019:
                    manam = 19;
                    break;
                case 2020:
                    manam = 20;
                    break;
                case 2021:
                    manam = 21;
                    break;
                case 2022:
                    manam = 22;
                    break;

            }
            var Makp9324 = _data.KPhongs.Where(p => p.MaKP == _makp).Select(p => p.MaQD).FirstOrDefault();
            if (Makp9324.Trim() != "" && Makp9324.Trim() != "0")
            {
                kq = manam + Makp9324;
            }
            if (kq != "")
            {
                int noingoaitru = -1;
                string _soLT = "";
                if (DungChung.Bien.PP_SoLT == 1)
                {
                    int makp = 0;
                    if (lupKPDT.EditValue != null && lupKPDT.EditValue.ToString() != "")
                    {
                        makp = Convert.ToInt32(lupKPDT.EditValue);
                        _soLT01071 = DungChung.Ham.GetSoPL(7, makp, noingoaitru);
                        _soLT = _soLT01071.ToString("D5");
                    }
                    else
                    {
                        MessageBox.Show("chưa chọn khoa khám bệnh, không lấy được số khám bệnh");
                    }
                }
                else if (DungChung.Bien.PP_SoLT == 2)
                {
                    _soLT01071 = DungChung.Ham.GetSoPL(7, 0, noingoaitru);
                    _soLT = _soLT01071.ToString("D5");
                }
                if (_soLT != "")
                {
                    kq += _soLT.ToString();
                    txtSoLT.Text = kq.ToString();
                }
            }
            else
            {
                MessageBox.Show("Khoa điều trị chưa có mã 9324, không lấy được số lưu trữ");
            }

        }

        //void setDBsoLT()
        //{
        //    if (DungChung.Bien.MaBV == "01830" && radNoiNgoaiTru.SelectedIndex == 0)
        //        return;
        //    if (DungChung.Bien.PP_SoVV == 1 || DungChung.Bien.PP_SoVV == 2)
        //    {
        //        int rs, sovaovien = 0, makpvv = 0;
        //        int noingoaitru = -1;
        //        if (DungChung.Bien.MaBV == "27021")
        //            noingoaitru = radNoiNgoaiTru.SelectedIndex;
        //        if (DungChung.Bien.PP_SoVV == 1 && lupKhoaDT.EditValue != null && lupKhoaDT.EditValue.ToString() != "")
        //            makpvv = Convert.ToInt32(lupKhoaDT.EditValue);
        //        if (DungChung.Bien.PP_SoVV == 1 || DungChung.Bien.PP_SoVV == 2)
        //        {
        //            if (Int32.TryParse(txtSoVV.Text, out rs))
        //            {
        //                sovaovien = Convert.ToInt32(txtSoVV.Text);
        //            }
        //            if (!DungChung.Ham.checkSoPL(makpvv, sovaovien, 2, noingoaitru))
        //            {
        //                DungChung.Ham.SetSoPL(makpvv, sovaovien, 2, noingoaitru);
        //            }
        //            else
        //            {
        //                DungChung.Ham.SetSoPL(makpvv, sovaovien, 2, noingoaitru);
        //                setSoVV();
        //            }

        //        }
        //    }

        //}
        bool TTxoa = false;// trạng thái xóa
        public static double _soHdt = 0;
        /// <summary>
        /// Trả về số ngày điều trị 
        /// </summary>
        /// <param name="tungay"></param>
        /// <param name="denngay"></param>
        /// <returns></returns>
        /// 
        public static int getDaysOfStay(int maBNhan, DateTime tungay, DateTime denngay)
        {
            int rs = 0;
            DateTime ngayYLenhCuoi = denngay;
            if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "56789")
            {
                QLBVEntities data = new QLBVEntities(DungChung.Bien.StrCon);
                var qdt0 = (from dt in data.DThuocs.Where(p => p.MaBNhan == maBNhan).Where(p => p.PLDV == 1) join dtct in data.DThuoccts on dt.IDDon equals dtct.IDDon select new { NgayNhap = dtct.NgayNhap.Value, dtct.MaDV, dtct.SoLuong, dtct.MaKXuat }).OrderByDescending(p => p.NgayNhap).ToList();

                var qdt = (from dt in qdt0
                           group dt by new { NgayNhap = dt.NgayNhap.Date, dt.MaDV, dt.MaKXuat } into kq
                           from kq1 in kq.DefaultIfEmpty()
                           select new
                           {
                               kq.Key.NgayNhap,
                               kq.Key.MaDV,
                               kq.Key.MaKXuat,
                               SoLuong = kq.Sum(p => p.SoLuong)
                           }).Where(p => p.SoLuong > 0).OrderByDescending(p => p.NgayNhap).FirstOrDefault();

                var qdv = (from dt in data.DThuocs.Where(p => p.MaBNhan == maBNhan).Where(p => p.PLDV == 2) join dtct in data.DThuoccts on dt.IDDon equals dtct.IDDon select dtct).OrderByDescending(p => p.NgayNhap).FirstOrDefault();
                var qcd = data.CLS.Where(p => p.MaBNhan == maBNhan).OrderByDescending(p => p.NgayThang).FirstOrDefault();
                List<DateTime> lngay = new List<DateTime>();
                if (qdt != null)
                    lngay.Add(qdt.NgayNhap);
                if (qdv != null)
                    lngay.Add(qdv.NgayNhap.Value);
                if (qcd != null)
                    lngay.Add(qcd.NgayThang.Value);

                if (lngay.Count() == 0)
                    return 0;
                else
                {
                    ngayYLenhCuoi = lngay.OrderByDescending(p => p).First();
                }
            }

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

                if (rs > 1 && denngay.Date > ngayYLenhCuoi.Date)
                {
                    rs = rs - (denngay.Date - ngayYLenhCuoi.Date).Days;
                    if (rs < 0)
                        rs = 0;
                }
            }

            return rs;
        }
        class c_ICD
        {
            string maICD;

            public string MaICD
            {
                get { return maICD; }
                set { maICD = value; }
            }
            string tenICD;

            public string TenICD
            {
                get { return tenICD; }
                set { tenICD = value; }
            }
        }
        List<c_ICD> lICD = new List<c_ICD>();
        List<KPhong> _lkp = new List<KPhong>();
        BenhNhan BNrv = new BenhNhan();
        List<ICD10> _licd10 = new List<ICD10>();
        private void frm_Ravien_Load(object sender, EventArgs e)
        {
            if(DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049")
            {
                panelControl2.Visible = true;
            }    
            if (DungChung.Bien.MaBV == "30009")
            {
                panel2.Visible = true;
                labelControl22.Text = "Bệnh khác:";
                labelControl24.Text = "Mã ICD:";
            }
            else
            {
                panel1.Location = new Point(6, 114);
                groupControl2.Size = new Size(932, 206);
                groupControl2.Location = new Point(0, 287);
            }
            _data = new QLBVEntities(DungChung.Bien.StrCon);
            _licd10 = _data.ICD10.ToList();
            BNrv = _data.BenhNhans.Where(p => p.MaBNhan == _mabn).FirstOrDefault();
            var qvv = _data.VaoViens.Where(p => p.MaBNhan == _mabn).FirstOrDefault();
            #region 26007
            if (DungChung.Bien.MaBV == "26007")
            {
                cboBenhManTinh.Visible = true;
                labelControl23.Visible = true;

                if (BNrv != null && BNrv.SThe != null && BNrv.SThe != "")
                {
                    string Sthe = BNrv.SThe.Trim();
                    var KtraBenhMT = _data.People.Where(p => p.SThe.Contains(Sthe)).ToList();
                    if (KtraBenhMT.Count > 0 && KtraBenhMT.First().GhiChu != null)
                    {
                        string BenhMT = KtraBenhMT.First().GhiChu;
                        if (BenhMT.Contains("THA"))
                        {
                            cboBenhManTinh.SelectedIndex = 1;
                        }
                        if (BenhMT.Contains("TD"))
                        {
                            cboBenhManTinh.SelectedIndex = 2;
                        }
                    }
                }
            }
            #endregion
            DateTime ngaymoi = new DateTime(2019, 1, 1);
            if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "14017")
            {

                if (qvv != null && qvv.NgayVao != null && qvv.NgayVao >= ngaymoi)
                {
                    txtMaYT.Enabled = false;
                    txtSoLT.Enabled = false;
                }
                else
                {
                    txtMaYT.Text = "";
                    txtSoLT.Text = "";
                    txtMaYT.Enabled = true;
                    txtSoLT.Enabled = true;
                }
            }
            txtSoNgaydt.Properties.ReadOnly = false;
            if (DungChung.Bien.MaBV == "14018")
            {
                txtPPDTr.Properties.Items.Add("Các PP VLTL- PHCN – Thuốc – Nâng cao thể trạng");
                txtPPDTr.Properties.Items.Add("Các PP VLTL- PHCN");
                txtPPDTr.Properties.Items.Add("Các PP VLTL- PHCN – Thuốc điều trị nội khoa");
                txtLoiDan.Text = "Tập luyện theo hướng dẫn";
            }
            else
            {
                _setPPDT();
            }
            _setLDBS();
            var _lKhamBenh = _data.BNKBs.Where(p => p.MaBNhan == _mabn).OrderBy(p => p.IDKB).ToList();
            cboKetQua.Properties.Items.Clear();
            cboKetQua.Properties.Items.AddRange(DungChung.Bien._ketQuaDT);
            txtMaYT.Text = _TaoMaYTe(DungChung.Bien.MaBV);

            if (DungChung.Bien.MaBV == "08204")
                txtSoNgaydt.Properties.ReadOnly = true;
            var nn = _data.DmNNs.ToList();
            lupNgheNghiep.Properties.DataSource = nn;

            var vaovien = _data.VaoViens.Where(p => p.MaBNhan == _mabn).ToList();
            if (BNrv.NoiTru == 1)
            {
                if (vaovien.Count > 0)
                {
                    txtNgayVao.DateTime = vaovien.First().NgayVao.Value;
                    //if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049")
                    //{
                    //    txtSoLT.Text = vaovien.First().SoBA;
                    //    txtSoLT.Enabled = false;
                    //}
                    _vaovien = true;
                }
                else
                {
                    _vaovien = false;
                }
            }
            else
            {
                _vaovien = true;
                if (vaovien.Count > 0)
                {
                    txtNgayVao.DateTime = vaovien.First().NgayVao.Value;
                    //if (DungChung.Bien.MaBV == "01071")
                    //    txtSoLT.Text = vaovien.First().SoBA;
                }
                else
                {
                    var bnkb = _data.BNKBs.Where(p => p.MaBNhan == _mabn).OrderBy(p => p.IDKB).ToList();
                    if (bnkb.Count > 0)
                        txtNgayVao.DateTime = bnkb.First().NgayKham.Value;
                    else
                        txtNgayVao.DateTime = BNrv.NNhap.Value;
                }
            }

            _lkp = (from kb in _data.BNKBs.Where(p => p.MaBNhan == _mabn)
                    join kp in _data.KPhongs on kb.MaKP equals kp.MaKP
                    select kp).Distinct().ToList();
            lupKPDT.Properties.DataSource = _lkp.ToList();
            int mbn = _mabn;
            txtNgayRa.DateTime = System.DateTime.Now;

            if (BNrv != null)
            {
                _noitr = BNrv.NoiTru.Value;
                var dtoc = (from tt in _data.TTboXungs.Where(p => p.MaBNhan == _mabn) join dt in _data.DanTocs on tt.MaDT equals dt.MaDT select new { dt.TenDT, tt.MaNN }).ToList();
                if (dtoc.Count > 0)
                {
                    txtTenDT.Text = dtoc.First().TenDT;
                    lupNgheNghiep.EditValue = dtoc.First().MaNN;
                }
                dateEdit1.DateTime = BNrv.NNhap ?? DateTime.Now;
                txtTenBNhan.Text = BNrv.TenBNhan;
                txtMaBNhan.Text = BNrv.MaBNhan.ToString();
                txtTuoi.Text = BNrv.Tuoi.ToString();

                if (BNrv.GTinh == 1)
                {
                    txtGTinh.Text = "Nam";
                }
                else txtGTinh.Text = "Nữ";




                if (DungChung.Bien.MaBV == "14017")
                {
                    labelControl35.Text = "Bệnh khác:";
                    labelControl34.Text = "Mã ICD:";
                    labelControl32.Visible = labelControl31.Visible = labelControl22.Visible = labelControl24.Visible = false;
                    txtBenhKhac2.Visible = txtBenhKhac3.Visible = txtBenhKhac4.Visible = LupICD2.Visible = LupICD3.Visible = LupICD4.Visible = false;
                    lICD = (from a in _data.ICD10.Where(p => p.MaYHCT != null && p.MaYHCT != "") select new c_ICD { MaICD = a.MaYHCT, TenICD = a.TenYHCT + "[" + a.TenICD + "]" }).OrderBy(p => p.MaICD).ToList();
                    txtPPDTr.Text = "Nội khoa";
                    txtLoiDan.Text = "Uống thuốc theo toa";
                }
                else
                {
                    txtBenhKhac1.Visible = false;
                    lICD = (from a in _data.ICD10 select new c_ICD { MaICD = a.MaICD, TenICD = a.TenICD }).OrderBy(p => p.MaICD).ToList();
                }
                txtMaICD.Properties.DataSource = lICD;
                LupICD2.Properties.DataSource = lICD;
                LupICD3.Properties.DataSource = lICD;
                LupICD4.Properties.DataSource = lICD;
                LupICD44.Properties.DataSource = lICD;
                LupICD55.Properties.DataSource = lICD;
                txtSThe.Text = BNrv.SThe;
                txtHanBHTu.Text = BNrv.HanBHTu.ToString();
                txtHanBHDen.Text = BNrv.HanBHDen.ToString();
                txtDChi.Text = BNrv.DChi;

                _songayGiuong = _getSoNgayGiuong(new QLBVEntities(DungChung.Bien.StrCon), _mabn);
                //_songayGiuong = double.Parse((txtNgayRa.DateTime.Date - txtNgayVao.DateTime.Date).Days.ToString());

                txtSoNgayGiuong.Text = _songayGiuong.ToString("##,###.##");
            }

            _ravien = _data.RaViens.Where(p => p.MaBNhan == _mabn).Where(p => p.Status == 2 || p.Status == 3 || p.Status == 4).ToList();
            #region sửa
            if (_ravien.Count > 0)
            {

                //// string[] _MaICDarr = DungChung.Ham.getMaICDarrFull(_data, _mabn, DungChung.Bien.GetICD, 0);
                txtNgayRa.DateTime = _ravien.First().NgayRa.Value;
                txtPPDTr.Text = _ravien.First().PPDTr;
                txtLoiDan.Text = _ravien.First().LoiDan;
                if (_ravien.First().SoNgaydt != null)
                {
                    _soHdt = 24 * _ravien.First().SoNgaydt.Value;
                    txtSoNgaydt.Text = _ravien.First().SoNgaydt.Value.ToString();
                }
                cboKetQua.Text = _ravien.First().KetQua;

                if (DungChung.Bien.MaBV == "24012" || DungChung.Bien.MaBV == "24272")
                {
                    string benhchinh = "";
                    string benhphu2 = "";
                    string benhphu3 = "";
                    string benhphu4 = "";

                    string maICD = "";
                    string maICD2 = "";
                    string maICD3 = "";
                    string maICD4 = "";
                    string[] icd = DungChung.Ham.FreshString(_ravien.First().MaICD.Trim()).Split(';');

                    string[] tenbenh = DungChung.Ham.FreshString(_ravien.First().ChanDoan.Trim()).Split(';');

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
                    txtBenhKhac1.Text = txtBenhKhac2.Text = benhphu2.Trim();
                    LupICD3.EditValue = maICD3.Trim();
                    txtBenhKhac3.Text = benhphu3.Trim();
                    LupICD4.EditValue = maICD4;
                    LupICD4.Text = maICD4;
                    txtBenhKhac4.Text = benhphu4;

                }
                else if (DungChung.Bien.MaBV == "30009")
                {
                    string benhchinh = "";
                    string benhphu2 = "";
                    string benhphu3 = "";
                    string benhphu4 = "";
                    string benhphu44 = "";
                    string benhphu55 = "";
                    string maICD = "";
                    string maICD2 = "";
                    string maICD3 = "";
                    string maICD4 = "";
                    string maICD44 = "";
                    string maICD55 = "";
                    string[] icd = _ravien.First().MaICD.Trim().Split(';');

                    string[] tenbenh = _ravien.First().ChanDoan.Trim().Split(';');

                    foreach (var ICD in icd)
                    {
                        if (string.IsNullOrEmpty(maICD))
                            maICD += ICD;
                        else if (string.IsNullOrEmpty(maICD2))
                            maICD2 += ICD;
                        else if (string.IsNullOrEmpty(maICD3))
                            maICD3 += ICD;
                        else if (string.IsNullOrEmpty(maICD4))
                            maICD4 += ICD + ";";
                        else if (string.IsNullOrEmpty(maICD44))
                            maICD44 += ICD;
                        else if (string.IsNullOrEmpty(maICD55))
                            maICD55 += ICD;
                    }
                    foreach (var item in tenbenh)
                    {
                        if (string.IsNullOrEmpty(benhchinh))
                            benhchinh += item;
                        else if (string.IsNullOrEmpty(benhphu2))
                            benhphu2 += item;
                        else if (string.IsNullOrEmpty(benhphu3))
                            benhphu3 += item;
                        else if (string.IsNullOrEmpty(benhphu4))
                            benhphu4 += item + ";";
                        else if (string.IsNullOrEmpty(benhphu44))
                            benhphu44 += item;
                        else if (string.IsNullOrEmpty(benhphu55))
                            benhphu55 += item;
                    }

                    txtMaICD.Text = maICD.Trim();
                    txtChanDoan.Text = benhchinh.Trim();
                    LupICD2.Text = maICD2.Trim();
                    LupICD2.EditValue = maICD2.Trim();
                    txtBenhKhac1.Text = txtBenhKhac2.Text = benhphu2.Trim();
                    LupICD3.EditValue = maICD3.Trim();
                    txtBenhKhac3.Text = benhphu3.Trim();
                    LupICD4.EditValue = maICD4;
                    LupICD4.Text = maICD4;
                    txtBenhKhac4.Text = benhphu4;


                    LupICD44.EditValue = maICD44.Trim();
                    txtBenhKhac44.Text = benhphu44.Trim();
                    LupICD55.EditValue = maICD55.Trim();
                    txtBenhKhac55.Text = benhphu55.Trim();
                }
                else
                {


                    string[] icd = _ravien.First().MaICD.Split(';');
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
                        LupICD4.Text = DungChung.Ham.FreshString(string.Join(";", icd4));
                    }
                    string[] benhkhac = _ravien.First().ChanDoan.Split(';');
                    if (benhkhac.Length > 3)
                    {
                        txtBenhKhac4.EditValue = DungChung.Ham.FreshString(string.Join(";", benhkhac.Skip(3)));
                    }



                }
                if (_ravien.First().Status == 2)

                    radHinhThuc.SelectedIndex = 0;

                else
                    if (_ravien.First().Status == 3)
                    radHinhThuc.SelectedIndex = 1;
                else
                        if (_ravien.First().Status == 4)
                    radHinhThuc.SelectedIndex = 2;

                lupKPDT.EditValue = _ravien.First().MaKP;
                if (_ravien.First().SoLT != null)
                    txtSoLT.Text = _ravien.First().SoLT;
                getSoLT_Edit();
                txtMaYT.Text = _ravien.First().MaYTe;
                btnXoa.Enabled = true;
                btnLuu.Enabled = false;
            }
            #endregion
            #region thêm mới
            else
            {
                //
                try
                {
                    string[] _MaICDarr_ = DungChung.Ham.getMaICDarrFull(_data, _mabn, DungChung.Bien.GetICD, 0);
                    int _makp = Convert.ToInt16(_MaICDarr_[6]);
                    lupKPDT.EditValue = _makp;
                    var tenkp = (from kp in _data.KPhongs.Where(p => p.MaKP == _makp)
                                 select new
                                 {
                                     kp.TenKP
                                 }).ToList();

                    lupKPDT.Text = tenkp.First().TenKP;
                }
                catch
                {

                }
                if (DungChung.Bien.MaBV == "24012")
                {
                    _soHdt = 0;
                }
                else
                {
                    _soHdt = Convert.ToDouble(txt_GioDT.Text);
                }
                if (DungChung.Bien.MaBV == "14017")
                {

                    var query = _data.BNKBs.Where(p => p.IDKB == (idKb)).ToList();
                    if (query.Count > 0)
                    {
                        string[] _MaICDarr = DungChung.Ham.getMaICDarrFull_SL(_data, _mabn, DungChung.Bien.GetICD, idKb);
                        txtMaICD.EditValue = query.First().MaICD.Trim();
                        txtChanDoan.EditValue = query.First().ChanDoan;
                        string[] ICDBenhkhac = query.First().MaICD2.Split(';');
                        string bk = "";
                        if (ICDBenhkhac.Count() > 0)
                        {
                            for (int i = 0; i < ICDBenhkhac.Count(); i++)
                            {
                                bk += ICDBenhkhac[i].Trim() + ";";
                            }
                        }
                        lupKhac.EditValue = bk;
                        txtBenhKhac1.EditValue = query.First().BenhKhac;

                        if (DungChung.Bien.MaBV != "30007")
                        {
                            string[] benhkhac = new string[4] { "", "", "", "" };
                            if (!string.IsNullOrEmpty(query.First().BenhKhac))
                            {
                                string[] icd2 = query.First().BenhKhac.Split(';');
                                for (int i = 0; i < icd2.Length; i++)
                                {
                                    if (icd2[i] != null)
                                    {
                                        if (i == 2)
                                            benhkhac[i] += icd2[i];
                                        else if (i > 2)
                                        {
                                            if (icd2[i].Length > 2)
                                                benhkhac[2] += ";" + icd2[i];
                                        }

                                        else
                                            benhkhac[i] = icd2[i];
                                    }
                                }
                            }
                            if (!string.IsNullOrEmpty(benhkhac[0]) && benhkhac[0].Contains("(Bệnh phụ)"))
                            {
                                txtBenhKhac2.EditValue = benhkhac[0];
                            }
                            else
                            {
                                txtBenhKhac2.EditValue = benhkhac[0];
                            }

                            if (!string.IsNullOrEmpty(benhkhac[1]) && benhkhac[1].Contains("(Bệnh phụ)"))
                            {
                                txtBenhKhac3.EditValue = benhkhac[1];
                            }
                            else
                            {
                                txtBenhKhac3.EditValue = benhkhac[1];
                            }

                            if (!string.IsNullOrEmpty(benhkhac[2]) && benhkhac[2].Contains("(Bệnh phụ)"))
                            {
                                txtBenhKhac4.EditValue = benhkhac[2];
                            }

                            else
                            {
                                txtBenhKhac4.EditValue = benhkhac[2];
                            }
                        }
                    }
                }

                else if (DungChung.Bien.MaBV == "24012" || DungChung.Bien.MaBV == "24272")
                {
                    DungChung.Bien.ChanDoan ttchandoan = DungChung.Ham.getCDRV_24012(_data, _mabn, DungChung.Bien.GetICD, 0);
                    txtMaICD.Text = ttchandoan.MaICD.Trim();
                    txtChanDoan.Text = ttchandoan.BenhChinh.Trim();
                    LupICD2.Text = ttchandoan.MaICD2.Trim();
                    LupICD2.EditValue = ttchandoan.MaICD2.Trim();
                    txtBenhKhac1.Text = txtBenhKhac2.Text = ttchandoan.BenhPhu2.Trim();
                    LupICD3.EditValue = ttchandoan.MaICD3.Trim();
                    txtBenhKhac3.Text = ttchandoan.BenhPhu3.Trim();
                    LupICD4.EditValue = ttchandoan.MaICD4;
                    LupICD4.Text = ttchandoan.MaICD4;
                    txtBenhKhac4.Text = ttchandoan.BenhPhu4;
                }

                else if (DungChung.Bien.MaBV == "24297")
                {
                    string[] _MaICDarr = DungChung.Ham.getMaICDarrFull(_data, _mabn, DungChung.Bien.GetICD, 0);
                    string[] icd = _MaICDarr[0].Split(';');
                    string[] tenBenh = DungChung.Ham.FreshString(_MaICDarr[1]).Split(';');
                    string tenBenhkhac = "";
                    if (tenBenh.Count() > 3)
                        tenBenhkhac = DungChung.Ham.FreshString(string.Join(";", tenBenh.Skip(3)));
                    if (icd.Length > 0)
                    {
                        txtMaICD.Text = icd[0].Trim();
                        txtChanDoan.Text = tenBenh[0].Trim();
                    }
                    if (icd.Length > 1)
                    {
                        LupICD2.EditValue = icd[1].Trim();
                        txtBenhKhac1.Text = txtBenhKhac2.Text = tenBenh[1].Trim();
                    }
                    if (icd.Length > 2)
                    {
                        LupICD3.EditValue = icd[2].Trim();
                        txtBenhKhac3.Text = tenBenh[2].Trim();
                    }
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
                        LupICD4.Text = DungChung.Ham.FreshString(string.Join(";", icd4));
                    }
                    //LupICD4.EditValue = DungChung.Ham.FreshString(string.Join(";", icd.Skip(3))).Trim();
                    if (_MaICDarr.Length >= 8)
                        txtBenhKhac4.Text = DungChung.Ham.FreshString(_MaICDarr[7]);
                    if (DungChung.Bien.MaBV == "24012")
                        txtBenhKhac4.Text = tenBenhkhac;
                }
                else
                {
                    string[] _MaICDarr = DungChung.Ham.getMaICDarrFull(_data, _mabn, DungChung.Bien.GetICD, 0);

                    string[] icd = _MaICDarr[0].Split(';');
                    if (icd.Length > 0)
                        txtMaICD.Text = icd[0].Trim();
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
                        LupICD4.Text = DungChung.Ham.FreshString(string.Join(";", icd4));
                    }
                    if (DungChung.Bien.MaBV == "30009")
                    {
                        if (icd.Length > 4)
                            LupICD44.EditValue = icd[4].Trim();
                        if (icd.Length > 5)
                            LupICD55.EditValue = icd[5].Trim();
                    }
                    //LupICD4.EditValue = DungChung.Ham.FreshString(string.Join(";", icd.Skip(3))).Trim();
                    if (_MaICDarr.Length >= 8)
                        txtBenhKhac4.Text = DungChung.Ham.FreshString(_MaICDarr[7]);
                }
                btnXoa.Enabled = false;
                btnLuu.Enabled = true;

                if ((DungChung.Bien.MaBV == "27021" || DungChung.Bien.MaBV == "20001" || DungChung.Bien.MaBV == "12122") && TTxoa == false)
                {
                    setSoHS();
                }
                if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "14017")
                {
                    if (_noitr == 1)
                    {
                        //  var qvv = _data.VaoViens.Where(p => p.MaBNhan == _mabn).FirstOrDefault();
                        if (qvv != null && qvv.NgayVao != null && qvv.NgayVao >= ngaymoi)
                        {
                            if (qvv.SoBA != null)
                                txtSoLT.Text = qvv.SoBA;
                            if (qvv.SoVV != null)
                            {
                                if (qvv.SoVV.Length == 6)
                                    txtMaYT.Text = DungChung.Bien.MaTinh + DungChung.Bien.MaBV.Substring(2, 3) + DateTime.Now.Year.ToString().Substring(2, 2) + qvv.SoVV;
                                else if (qvv.SoVV.Length == 8)
                                    txtMaYT.Text = DungChung.Bien.MaTinh + DungChung.Bien.MaBV.Substring(2, 3) + qvv.SoVV;
                                else
                                    txtMaYT.Text = qvv.SoVV;
                            }
                        }
                    }
                }
            }
            #endregion
            var donthuoc = (from dt in _data.DThuocs.Where(p => p.MaBNhan == _mabn && p.PLDV == 1 && (p.KieuDon == 0 || p.KieuDon == 1))
                            join dtct in _data.DThuoccts on dt.IDDon equals dtct.IDDon
                            join kp in _data.KPhongs on dt.MaKXuat equals kp.MaKP
                            select new { dtct.SoPL, dtct.Status, kp.PLoai }).ToList();
            foreach (var a in donthuoc)
                if (a.Status == null || a.Status == 0)
                {
                    if (DungChung.Bien.MaTinh == "24")
                    {
                        if (DungChung.Bien.MaBV == "24012" && a.PLoai == "Tủ trực")
                        {
                            _cotheluu = true;
                        }
                        else
                        {
                            if (DungChung.Bien.MaBV == "24272")
                            {
                                _cotheluu = true;
                            }
                            else
                            {
                                if (a.Status == null || a.Status == 0)
                                {
                                    _cotheluu = false;
                                    MessageBox.Show("Bệnh nhân có đơn thuốc chưa lĩnh, bạn không thể làm thủ tục ra viện");
                                    break;
                                }
                                if (a.SoPL != null && a.SoPL > 0)
                                {
                                    _cotheluu = true;
                                }
                                else
                                {
                                    _cotheluu = false;
                                    MessageBox.Show("Bệnh nhân có thuốc đã kê đơn nhưng chưa có phiếu lĩnh thuốc hoặc chưa hủy đơn \n Bạn không thể làm thủ tục ra viện");
                                    break;
                                }
                            }
                            
                        }
                    }
                    else
                    {
                        _cotheluu = true;
                    }
                }
            TTxoa = false;
            if (DungChung.Bien.MaBV == "30002")
            {
                txtSoNgaydt.Enabled = true;
            }

            if (DungChung.Bien.MaBV != "34019")
            {
                labelControl1.Text = "Số ngày ĐT:";
                labelControl11.Text = "T.Số ngày giường ĐT:";
            }
        }
        List<BNKB> _lbnKB = new List<BNKB>();
        public static string CheckDonChuaLinh(int mabn)
        {
            QLBVEntities _data = new QLBVEntities(DungChung.Bien.StrCon);
            string DSDon = "";
            var kt = (from dt in _data.DThuocs.Where(p => p.MaBNhan == mabn)
                      join dtct in _data.DThuoccts on dt.IDDon equals dtct.IDDon
                      join kp in _data.KPhongs.Where(p => p.PLoai != DungChung.Bien.st_PhanLoaiKP.TuTruc) on dt.MaKXuat equals kp.MaKP
                      where (dtct.Status == 0 && dtct.SoPL > 0 && dtct.Status != -1)
                      select new { dt.IDDon, dtct.SoPL, dt.NgayKe, kp.TenKP }).Distinct().ToList();
            foreach (var item in kt)
                DSDon += item.IDDon + " đơn ngày: " + item.NgayKe + " kho kê: " + item.TenKP + ", số PL: " + item.SoPL + "\n";
            return DSDon;
        }
        public static string CheckDonChuaLenPLinh(int mabn)
        {
            QLBVEntities _data = new QLBVEntities(DungChung.Bien.StrCon);
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
        public bool CheckChiPhiPhongKham(int mabn)
        {
            QLBVEntities _dataContext = new QLBVEntities(DungChung.Bien.StrCon);
            var kt3 = (from dt in _dataContext.DThuocs.Where(p => p.MaBNhan == _mabn)
                       join bn in _dataContext.BenhNhans.Where(p => p.NoiTru == 1) on dt.MaBNhan equals bn.MaBNhan
                       join dtct in _dataContext.DThuoccts on dt.IDDon equals dtct.IDDon
                       join kp in _dataContext.KPhongs on dtct.MaKP equals kp.MaKP
                       join dv in _dataContext.DichVus on dtct.MaDV equals dv.MaDV
                       where kp.PLoai != DungChung.Bien.st_PhanLoaiKP.LamSang
                       select new { dv.TenDV, kp.TenKP }).ToList();
            string mb = "";
            int i = 0;
            foreach (var item in kt3)
            {
                if (i == 0)
                    mb = "Các chi phí của phòng khám chưa được đẩy vào khoa điều trị, xem lại mẫu 13 \n";
                mb += item.TenDV + ": " + item.TenKP + "\n";

            }
            if (!string.IsNullOrEmpty(mb))
            {
                MessageBox.Show(mb);
                return false;
            }
            return true;
        }
        private bool KTLuu()
        {
             
            if (DungChung.Bien.MaBV == "30007" || DungChung.Bien.MaBV == "30010")
                if (!CheckChiPhiPhongKham(_mabn))
                {
                    //return false;
                }
            if (DungChung.Ham.KTraTT(_data, _mabn))
            {
                MessageBox.Show("Bệnh nhân đã thanh toán bạn không thể lưu!");
                return false;
            }
            if (string.IsNullOrEmpty(lupKPDT.Text))
            {
                MessageBox.Show("Khoa|Phòng điều trị không hợp lệ!");
                lupKPDT.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtSoNgaydt.Text))
            {
                MessageBox.Show("Số ngày điều trị không hợp lệ!");
                txtSoNgaydt.Focus();
                return false;
            }
            else
            {
                if (_soHdt >= 4)
                {
                    if (Convert.ToInt32(txtSoNgaydt.Text) < 1)
                    {
                        MessageBox.Show("Số ngày điều trị không hợp lệ!");
                        txtSoNgaydt.Focus();
                        return false;
                    }
                }
                if (DungChung.Bien.MaBV != "01071" && DungChung.Bien.MaBV != "01049" || DungChung.Bien.MaBV != "12345" && DungChung.Bien.MaBV != "56789")
                {
                    if (songaydt < Convert.ToInt32(txtSoNgaydt.Text))
                    {
                        BenhNhan qbn = _data.BenhNhans.Single(p => p.MaBNhan == _mabn);
                        if (qbn.DTuong == "BHYT")
                        {
                            MessageBox.Show("Số ngày điều trị lớn hơn so với thực tế");
                            return false;
                        }

                    }
                    else if (songaydt != Convert.ToInt32(txtSoNgaydt.Text))
                    {
                        DialogResult a = MessageBox.Show("Số ngày điều trị không khớp với thực tế, bạn có muốn lưu ?", "Cảnh báo", MessageBoxButtons.OKCancel);
                        if (a == DialogResult.Cancel)
                        {
                            txtSoNgaydt.Focus();
                            return false;
                        }
                    }
                }
            }
            if (string.IsNullOrEmpty(cboKetQua.Text) && radHinhThuc.SelectedIndex == 0)
            {
                MessageBox.Show("Kết quả điều trị không hợp lệ!");
                cboKetQua.Focus();
                return false;
            }
            if (DungChung.Bien.MaBV == "14017" && cboKetQua.SelectedIndex == 4)
            {
                DialogResult a = MessageBox.Show("Xác nhận bệnh nhân tử vong ?", "Cảnh báo", MessageBoxButtons.OKCancel);
                if (a == DialogResult.Cancel)
                {
                    cboKetQua.Focus();
                    return false;
                }
            }
            if (string.IsNullOrEmpty(txtNgayVao.Text))
            {
                MessageBox.Show("Ngày vào không hợp lệ!");
                txtNgayVao.Focus();
                return false;
            }
            _lbnKB = _data.BNKBs.Where(p => p.MaBNhan == _mabn).OrderByDescending(p => p.IDKB).ToList();
            int _makp = 0;
            string giuong = "";
            if (_lbnKB.Count > 0)
            {
                _makp = _lbnKB.First().MaKP == null ? 0 : _lbnKB.First().MaKP.Value;
                // giuong = _lbnKB.First().Giuong;               
                giuong = string.Join("", _lbnKB.Where(p => p.Giuong != null && p.Giuong != "").Select(p => p.Giuong).ToArray());

                if (DungChung.Bien.MaBV == "27023")
                {
                    int idkbvv = _lbnKB.Where(p => p.PhuongAn == 1).Select(p => p.IDKB).FirstOrDefault();
                    var ktsongdt = _lbnKB.Where(p => p.IDKB > idkbvv).Where(p => p.SoNgaydt == null).ToList();
                    if (ktsongdt.Count > 0)
                    {
                        MessageBox.Show("khoa phòng điều trị chưa có số ngày điều trị!");
                        return false;
                    }
                }
            }

            if (DungChung.Bien.PLoaiKP != "Admin")
            {

                if (DungChung.Bien.listKPHoatDong.Where(p => p == _makp).Count() == 0)
                {
                    MessageBox.Show("Mã Khoa Phòng không hợp lệ");
                    return false;
                }
            }
            if (DungChung.Bien.MaBV == "30303")  // cho phép bệnh nhân ngoại trú đc  ra viện khi ko ko nhập buồng giường
            {
                if (BNrv.DTuong == "BHYT" && !string.IsNullOrEmpty(giuong) && giuong.Length > 30 && (BNrv.NoiTru == 1))
                {
                    MessageBox.Show("Tổng ký tự buồng giường không được > 30 ký tự");
                    return false;
                }
            }
            else
            {
                if (BNrv.DTuong == "BHYT" && string.IsNullOrEmpty(giuong) && (BNrv.NoiTru == 1))
                {
                    MessageBox.Show("Bạn chưa nhập mã giường điều trị của bệnh nhân!");
                    return false;
                }
                if (BNrv.DTuong == "BHYT" && !string.IsNullOrEmpty(giuong) && giuong.Length > 30 && (DungChung.Bien.MaBV == "30003" || (BNrv.NoiTru == 1)))//BNrv.NoiTru == 1 &&
                {
                    MessageBox.Show("Tổng ký tự buồng giường không được > 30 ký tự");
                    return false;
                }
            }

            if (string.IsNullOrEmpty(lupKPDT.Text))
            {
                MessageBox.Show("Khoa|phòng điều trị không hợp lệ");
                lupKPDT.Focus();
                return false;
            }
            else
            {
                int kp = lupKPDT.EditValue == null ? 0 : Convert.ToInt32(lupKPDT.EditValue);
                if (_lbnKB.Count > 0 && kp != _lbnKB.First().MaKP)
                {
                    DialogResult _result = MessageBox.Show("Kiểm tra lại khoa phòng điều trị?", "Khoa|phòng điều trị", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (_result == DialogResult.Yes)
                    {
                        lupKPDT.Focus();
                        return false;
                    }
                }
            }
            int _songaydt = Convert.ToInt32(txtSoNgaydt.Text);
            if (_noitr == 1)
            {
                //14018 không tính ngày giường cuối cùng nên check >1 HIS 626
                if ((_songaydt - _songayGiuong > (DungChung.Bien.MaBV == "14018" || DungChung.Bien.MaBV == "14017" ? 1 : 0)))//_noitr == 1 && 
                {
                    if (_soHdt >= 4)
                    {
                        if (_songayGiuong > 0)
                        {

                            DialogResult rs = MessageBox.Show("Bệnh nhân có tiền ngày giường chưa khớp với số ngày điều trị, bạn có muốn cập nhật chi phí ngày giường không ?", "Tiền ngày giường", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (rs == DialogResult.Yes)
                            {

                                frm_CapNhatNgayGiuong frm = new frm_CapNhatNgayGiuong(_mabn, txtNgayRa.DateTime, 2);
                                frm.ShowDialog();
                                _songayGiuong = _getSoNgayGiuong(new QLBVEntities(DungChung.Bien.StrCon), _mabn);
                                txtSoNgayGiuong.Text = _songayGiuong.ToString("##,###.##");
                                return false;
                                //MessageBox.Show("chức năng đang nâng cấp");
                            }
                        }
                        else
                        {
                            DialogResult rs = MessageBox.Show("Bệnh nhân chưa có tiền ngày giường, không thể ra viện, \nbạn có muốn cập nhật chi phí ngày giường không ?", "Tiền ngày giường", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (rs == DialogResult.Yes)
                            {

                                frm_CapNhatNgayGiuong frm = new frm_CapNhatNgayGiuong(_mabn, txtNgayRa.DateTime, 2);
                                frm.ShowDialog();
                                _songayGiuong = _getSoNgayGiuong(new QLBVEntities(DungChung.Bien.StrCon), _mabn);
                                txtSoNgayGiuong.Text = _songayGiuong.ToString("##,###.##");
                                return false;
                                //MessageBox.Show("chức năng đang nâng cấp");
                            }
                            else
                                return false;

                        }
                    }
                }

                if (_songayGiuong > Convert.ToDouble(txtSoNgaydt.Text) && (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "56789" || DungChung.Bien.MaBV == "26062"))
                {
                    MessageBox.Show("Số ngày giường không được lớn hơn số ngày điều trị");
                    frm_CapNhatNgayGiuong frm = new frm_CapNhatNgayGiuong(_mabn, txtNgayRa.DateTime, 2);
                    frm.ShowDialog();
                    _songayGiuong = _getSoNgayGiuong(new QLBVEntities(DungChung.Bien.StrCon), _mabn);
                    txtSoNgayGiuong.Text = _songayGiuong.ToString("##,###.##");
                    return false;
                }
            }
            string tencd = "";
            tencd = DungChung.Ham.KTChiDinh(_data, _mabn);
            if (!string.IsNullOrEmpty(tencd))
            {
                if (DungChung.Bien.MaBV != "30003" && DungChung.Bien.MaBV != "01830" && DungChung.Bien.MaBV != "01071" && DungChung.Bien.MaBV != "01049" && DungChung.Bien.MaBV != "12345" && DungChung.Bien.MaBV != "56789")
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
            //kiểm tra đơn chưa lên phiếu lĩnh
            if (DungChung.Bien.MaBV == "30002" || DungChung.Bien.MaBV == "12122" || DungChung.Bien.MaBV == "01830" || DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049")
            {
                string dsdonchualenpl = CheckDonChuaLenPLinh(_mabn);
                if (!string.IsNullOrEmpty(dsdonchualenpl))
                {
                    if (DungChung.Bien.MaBV == "01830")// quynv yc 17/07/2
                    {
                        var bn = _data.BenhNhans.Where(p => p.MaBNhan == _mabn).Select(p => p.CapCuu).FirstOrDefault();
                        if (bn != null && bn.Value == 1)
                        {
                            DialogResult _result = MessageBox.Show("Bệnh nhân có các đơn thuốc chưa tạo phiếu lĩnh :\n " + dsdonchualenpl + "Bạn vẫn muốn làm thủ tục ra viện?", "Hỏi ra viện", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (_result == DialogResult.No)
                                return false;
                        }
                        else
                        {
                            MessageBox.Show("Bệnh nhân có các đơn thuốc chưa tạo phiếu lĩnh :\n " + dsdonchualenpl + "Bạn không thể làm thủ tục ra viện", "Hỏi ra viện");
                            return false;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Bệnh nhân có các đơn thuốc chưa tạo phiếu lĩnh :\n " + dsdonchualenpl + "Bạn không thể làm thủ tục ra viện?", "Thông báo");
                        return false;
                    }

                }
            }
            // kiểm tra đơn chưa lĩnh
            string dsdoncl = CheckDonChuaLinh(_mabn);
            if (!string.IsNullOrEmpty(dsdoncl))
            {
                if (DungChung.Bien.MaBV == "12122" || DungChung.Bien.MaBV == "01830") // quynv yc 19/01/2017
                {
                    if (DungChung.Bien.MaBV == "01830")// quynv yc 17/07/2017
                    {
                        var bn = _data.BenhNhans.Where(p => p.MaBNhan == _mabn).Select(p => p.CapCuu).FirstOrDefault();
                        if (bn != null && bn.Value == 1)
                        {
                            DialogResult _result = MessageBox.Show("Bệnh nhân có các đơn thuốc chưa lĩnh :\n " + dsdoncl + "Bạn vẫn muốn làm thủ tục ra viện?", "Hỏi ra viện", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (_result == DialogResult.No)
                                return false;
                        }
                        else
                        {
                            MessageBox.Show("Bệnh nhân có các đơn thuốc chưa lĩnh :\n " + dsdoncl + "Bạn không thể làm thủ tục ra viện", "Hỏi ra viện");
                            return false;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Bệnh nhân có các đơn thuốc chưa lĩnh :\n " + dsdoncl + "Bạn không thể làm thủ tục ra viện", "Hỏi ra viện");
                        return false;
                    }

                }
                else
                {
                    if (DungChung.Bien.MaBV == "01071")
                    {
                        MessageBox.Show("Bệnh nhân có các đơn thuốc chưa lĩnh :\n " + dsdoncl + "Bạn không thể làm thủ tục ra viện", "Hỏi ra viện");
                        return false;
                    }
                    else
                    {
                        DialogResult _result = MessageBox.Show("Bệnh nhân có các đơn thuốc chưa lĩnh :\n " + dsdoncl + "Bạn vẫn muốn làm thủ tục ra viện?", "Hỏi ra viện", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (_result == DialogResult.No)
                            return false;
                    }

                }

            }



            // ngày ra viện phải lớn hơn hoặc bằng ngày kê đơn cuối cùng, 
            // 03/01/17 kiểm tra thêm điều kiện nếu đơn ngày y lệnh cuối cùng đã được trả dược thì không cần kiểm tra

            //var qdthuoc = (from dt in _data.DThuocs.Where(p => p.MaBNhan == _mabn) join dtct in _data.DThuoccts on dt.IDDon equals dtct.IDDon select dtct.NgayNhap).OrderByDescending(p => p).FirstOrDefault();
            if (checkNgayKeNgayRa(_mabn, txtNgayRa.DateTime) == false)
            {
                txtNgayRa.Focus();
                return false;
            }
            if (checkNgayKhamNgayRa(_mabn, txtNgayRa.DateTime) == false)
            {
                //MessageBox.Show("Ngày ra  phải lớn hơn ngày y lệnh cuối cùng: ");
                return false;
            }
            if (DungChung.Bien.MaBV == "01071")
            {
                QLBVEntities data = new QLBVEntities(DungChung.Bien.StrCon);
                var vv = data.VaoViens.Where(p => p.MaBNhan == _mabn).FirstOrDefault();
                if (vv != null && vv.NgayVao.Value.Hour >= 20)
                {
                    DateTime ngay1 = DungChung.Ham.NgayTu(vv.NgayVao.Value);
                    DateTime ngay2 = DungChung.Ham.NgayDen(vv.NgayVao.Value);
                    var ktng = (from dt in data.DThuocs.Where(p => p.MaBNhan == _mabn && p.PLDV == 2)
                                join dtct in data.DThuoccts.Where(p => p.NgayNhap >= ngay1 && p.NgayNhap <= ngay2) on dt.IDDon equals dtct.IDDon
                                join dv in data.DichVus on dtct.MaDV equals dv.MaDV
                                join tn in data.TieuNhomDVs.Where(p => p.IDNhom == 14 || p.IDNhom == 15) on dv.IdTieuNhom equals tn.IdTieuNhom
                                select new { dt }).ToList();
                    if (ktng.Count > 0)
                    {
                        MessageBox.Show("Bệnh nhân vào viện sau 20h không được tính tiền giường ngày " + ngay1.ToShortDateString());
                        return false;
                    }
                }
            }

            var bnhan = _data.BenhNhans.FirstOrDefault(p => p.MaBNhan == _mabn);
            if (bnhan != null && !CheckXuatDuoc20001(bnhan.CapCuu ?? 0, txtNgayRa.DateTime, (bnhan.NoiTru == 1 || bnhan.DTNT), _mabn))
            {
                MessageBox.Show("Bệnh nhân chưa xuất dược không thể ra viện!");
                return false;
            }

            if (bnhan != null && !CheckPL20001(bnhan.CapCuu ?? 0, txtNgayRa.DateTime, (bnhan.NoiTru == 1 || bnhan.DTNT), _mabn))
            {
                MessageBox.Show("Bệnh nhân chưa tạo phiếu lĩnh không thể ra viện!");
                return false;
            }
            //var qdienbien=_data.DienBiens.Where(p=>p.MaBNhan==_mabn).Select(p=>p.NgayNhap).OrderByDescending(p=>p).
            //var _ldthuocct=from dt in _data.DThuocs.Where(p=>p.MaBNhan==_mabn&&p.PLDV==1)
            //               join dtct in _data.DThuoccts on dt.IDDon equals dtct.IDDon


            return true;
        }

        private bool CheckXuatDuoc20001(int capCuu, DateTime ngayRa, bool dtntOrngTru, int? maBnhan)
        {
            bool rs = true;

            if (DungChung.Bien.MaBV == "20001" && dtntOrngTru && !DungChung.Ham.CheckNGioHC(ngayRa) && (capCuu == 0 || capCuu == 1))
            {
                _data = new QLBVEntities(DungChung.Bien.StrCon);
                var xd = (from dt in _data.DThuocs.Where(o => o.MaBNhan == maBnhan)
                          join dtct in _data.DThuoccts.Where(o => (o.Status != 1 && o.Status != 2) || o.SoPL == null || o.SoPL <= 0) on dt.IDDon equals dtct.IDDon
                          join dv in _data.DichVus.Where(o => o.PLoai == 1) on dtct.MaDV equals dv.MaDV
                          select dtct).ToList();
                if (xd.Count > 0)
                    return false;
            }

            return rs;
        }

        private bool CheckPL20001(int capCuu, DateTime ngayRa, bool dtntOrngTru, int? maBnhan)
        {
            bool rs = true;

            if (DungChung.Bien.MaBV == "20001" && dtntOrngTru && DungChung.Ham.CheckNGioHC(ngayRa) && (capCuu == 0 || capCuu == 1))
            {
                _data = new QLBVEntities(DungChung.Bien.StrCon);
                var pl = (from dt in _data.DThuocs.Where(o => o.MaBNhan == maBnhan)
                          join dtct in _data.DThuoccts.Where(o => o.SoPL == 0 || o.SoPL == null) on dt.IDDon equals dtct.IDDon
                          join dv in _data.DichVus.Where(o => o.PLoai == 1) on dtct.MaDV equals dv.MaDV
                          select dt).ToList();
                if (pl.Count > 0)
                    return false;
            }

            return rs;
        }

        private bool checkNgayKhamNgayRa(int _mabn, DateTime ngayra)
        {
            QLBVEntities data = new QLBVEntities(DungChung.Bien.StrCon);

            var qbnkb = (from kb in data.BNKBs.Where(p => p.MaBNhan == _mabn && p.NgayKham > ngayra) select kb).ToList();
            if (qbnkb.Count > 0)
            {
                MessageBox.Show("Bệnh nhân có ngày khám lớn hơn ngày ra viện");
                return false;
            }
            var ttbn = data.BenhNhans.Where(p => p.MaBNhan == _mabn).FirstOrDefault();
            if (ttbn != null && ttbn.DTuong == "BHYT")
            {
                DateTime hanBHtu = ttbn.HanBHTu.Value;

                if (hanBHtu > ttbn.NNhap)
                {
                    MessageBox.Show("Bệnh nhân có ngày nhập nhỏ hơn giới hạn thẻ từ: " + hanBHtu.ToShortDateString());
                    return false;
                }
            }
            return true;

        }
        public bool checkNgayKeNgayRa(int maBNhan, DateTime ngayra)
        {
            bool kt = true;
            string _loi = "";
            string _loi2 = "";
            QLBVEntities data = new QLBVEntities(DungChung.Bien.StrCon);
            //var ttbn = data.BenhNhans.Where(p => p.MaBNhan == maBNhan).FirstOrDefault();
            //if (ttbn != null && ttbn.DTuong == "BHYT")
            //{
            //    DateTime hanBHtu = ttbn.HanBHTu.Value;
            //    var ktradthuoc = (from dt in data.DThuocs.Where(p => p.MaBNhan == maBNhan)
            //                      join dtct in data.DThuoccts.Where(p => p.NgayNhap < hanBHtu) on dt.IDDon equals dtct.IDDon
            //                      select new
            //                      {
            //                          dtct.MaDV,
            //                      }).ToList();
            //    if (ktradthuoc.Count > 0)
            //    {
            //        kt = false;
            //        MessageBox.Show("Bệnh nhân có dịch vụ nhỏ hơn giới hạn thẻ từ: " + hanBHtu.ToShortDateString());
            //    }
            //}

            var _ldv = data.DichVus.ToList();
            var qdthuoc = (from dt in data.DThuocs.Where(p => p.MaBNhan == maBNhan) //.Where(p => p.NgayKe >= ngayra) sửa lại để check cả dịch vụ nhập Đức 05-09
                           join dtct in data.DThuoccts.Where(p => p.NgayNhap >= ngayra) on dt.IDDon equals dtct.IDDon
                           group new { dt, dtct } by new { dt.MaBNhan, dtct.MaDV }
                               into kq
                           select new { kq.Key.MaDV, SoLuong = kq.Sum(p => p.dtct.SoLuong) }).Where(p => p.SoLuong > 0).ToList();
            if (qdthuoc.Count > 0 && kt)
            {
                var q1 = (from dtt in qdthuoc
                          join dv in _ldv on dtt.MaDV equals dv.MaDV
                          select new { dv.TenDV }).ToList(); ;
                _loi = string.Join(";", q1.Select(p => p.TenDV).ToArray());
                kt = false;
                MessageBox.Show("Bệnh nhân có các y lệnh: " + _loi + "\nChưa có đơn trả thuốc vì ngày nhập lớn hơn ngày ra viện");
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
                    var qdienbien = _data.DienBiens.Where(p => p.MaBNhan == _mabn).Where(p => p.NgayNhap > ngaykemax).Select(p => p.NgayNhap).OrderByDescending(p => p).ToList();
                    if (qdienbien.Count > 0)
                    {
                        MessageBox.Show("Bệnh nhân có diễn biến bệnh lớn hơn ngày ra viện");
                        kt = false;
                    }
                }
                else
                {
                    var qdienbien = _data.DienBiens.Where(p => p.MaBNhan == _mabn).Where(p => p.NgayNhap > ngayra).Select(p => p.NgayNhap).OrderByDescending(p => p).ToList();
                    if (qdienbien.Count > 0)
                    {
                        MessageBox.Show("Bệnh nhân có diễn biến bệnh lớn hơn ngày ra viện");
                        kt = false;
                    }
                }

            }
            return kt;
        }
        double _songayGiuong = 0;
        public static double _getSoNgayGiuong(QLBVEntities _data, int _mabn)
        {
            try
            {
                double _ngayGiuong = 0;
                var ngayg = (from dt in _data.DThuocs.Where(p => p.MaBNhan == _mabn)
                             join dthuoct in _data.DThuoccts on dt.IDDon equals dthuoct.IDDon
                             join dv in _data.DichVus on dthuoct.MaDV equals dv.MaDV
                             join nhom in _data.NhomDVs.Where(p => p.TenNhomCT == "Giường điều trị nội trú" || p.TenNhomCT == "Giường điều trị ngoại trú") on dv.IDNhom equals nhom.IDNhom
                             select new { SoLuong = dthuoct.SoLuong == null ? 0 : dthuoct.SoLuong }).ToList();
                _ngayGiuong = ngayg.Sum(p => p.SoLuong);
                return Math.Round(_ngayGiuong, 1);
            }
            catch (Exception)
            {
                return 1;
            }
        }

        private bool KiemTraTTBX()
        {
            bool kq = true;
            var dt = _data.TTboXungs.FirstOrDefault(p => p.MaBNhan == _mabn);
            if (dt != null && string.IsNullOrWhiteSpace(dt.MaDT))
            {
                XtraMessageBox.Show("Bạn chưa nhập thông tin dân tộc cho bệnh nhân!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                kq = false;
            }
            return kq;
        }

        private bool KiemTraTE()
        {
            bool kq = true;
            var bn = _data.BenhNhans.FirstOrDefault(p => p.MaBNhan == _mabn);
            int? tuoi = bn.Tuoi;
            if (tuoi < 16)
            {
                var dt = _data.TTboXungs.FirstOrDefault(p => p.MaBNhan == _mabn);
                if (dt != null && string.IsNullOrWhiteSpace(dt.NThan))
                {
                    XtraMessageBox.Show("Bạn chưa nhập thông tin cha mẹ cho bệnh nhân dưới 16 tuổi!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    kq = false;
                }
            }
            return kq;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {

            //QLBVEntities _data = new QLBVEntities(DungChung.Bien.StrCon);
            // luu bang RaVien
            _data = new QLBVEntities(DungChung.Bien.StrCon);
            var bn1 = _data.DienBiens.Where(p => p.MaBNhan == _mabn).ToList();
            if (bn1.Count > 0)
            {
                if (DungChung.Bien.MaBV != "24272")
                {
                    foreach (var item in bn1)
                    {
                        if (item.Ploai == 0 && (item.DienBien1 == null || item.DienBien1 == ""))
                        {
                            XtraMessageBox.Show("Nhập đầy đủ diễn biến bệnh trước khi ra viện!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                }
                //if (bn1.Where(p => p.DienBien1 == "").ToList().Count > 0)
                //{
                //    XtraMessageBox.Show("Nhập đầy đủ diễn biến bệnh trước khi ra viện!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    return;
                //}
            }
            var ktravv = _data.VaoViens.Where(p => p.MaBNhan == _mabn).ToList();
            if (ktravv.Count <= 0)
                _vaovien = false;
            if (_vaovien)
            {
                if (KTLuu() && ((DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049") ? (KiemTraTE() && KiemTraTTBX()) : true))
                {
                    if (!string.IsNullOrEmpty(txtMaBNhan.Text))
                    {
                        string[] icd = new string[6] { "", "", "", "", "", "" };
                        string[] benhkhac = new string[6] { "", "", "", "", "", "" };
                        _mabn = Convert.ToInt32(txtMaBNhan.Text);
                        var kt = _data.RaViens.Where(p => p.MaBNhan == _mabn).ToList();
                        var bnkb = _data.BNKBs.Where(p => p.MaBNhan == _mabn).OrderByDescending(p => p.IDKB).FirstOrDefault();
                        var bn = _data.BenhNhans.FirstOrDefault(o => o.MaBNhan == _mabn);
                        var bn2 = _data.DienBiens.Where(p => p.MaBNhan == _mabn).ToList();
                        if (bn1.Count > 0 && DungChung.Bien.MaBV != "24272")
                        {
                            if (bn1.Where(p => p.DienBien1 == "" && p.Ploai == 0).ToList().Count > 0)
                            {
                                XtraMessageBox.Show("Nhập đầy đủ diễn biến bệnh trước khi ra viện!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                        }
                        if (kt.Count > 0)
                        {
                            if (kt.Where(p => p.Status == 1).ToList().Count > 0)
                            {
                                MessageBox.Show("Bệnh nhân đã được nhập chuyển viện, bạn không thể lưu");
                            }
                            else
                            {
                                if (bn != null && bn.NoThe)
                                {
                                    MessageBox.Show("Bệnh nhân nợ thẻ BHYT, bạn không thể lưu");
                                    return;
                                }
                                //sửa
                                //if (_cotheluu)
                                //{
                                int id = kt.First().IdRaVien;
                                RaVien nhaprv = _data.RaViens.Single(p => p.IdRaVien == (id));
                                nhaprv.PPDTr = txtPPDTr.Text;
                                nhaprv.LoiDan = txtLoiDan.Text;
                                nhaprv.NgayRa = txtNgayRa.DateTime;
                                nhaprv.NgayVao = txtNgayVao.DateTime;
                                nhaprv.SoLT = (txtSoLT.Text != "" && txtSoLT.Text != null) ? txtSoLT.Text.Trim() : null;
                                nhaprv.MaYTe = (txtMaYT.Text != "" && txtMaYT.Text != null) ? txtMaYT.Text.Trim() : null;
                                nhaprv.MaBNhan = _mabn;

                                int makp = 0;
                                if (lupKPDT.EditValue != null)
                                {
                                    nhaprv.MaKP = Convert.ToInt32(lupKPDT.EditValue);
                                    makp = Convert.ToInt32(lupKPDT.EditValue);
                                }
                                else
                                {
                                    nhaprv.MaKP = 0;
                                }
                                int MaCK = _lbnKB.Count > 0 ? _lbnKB.First().MaCK : -1;

                                nhaprv.MaCK = MaCK;
                                if (!string.IsNullOrEmpty(txtMaICD.Text))
                                    icd[0] = txtMaICD.EditValue.ToString();
                                if (LupICD2.EditValue != null)
                                    icd[1] = LupICD2.EditValue.ToString();
                                if (LupICD3.EditValue != null)
                                    icd[2] = LupICD3.EditValue.ToString();
                                if (LupICD4.EditValue != null)
                                    icd[3] = LupICD4.EditValue.ToString();
                                if (DungChung.Bien.MaBV == "30009")
                                {
                                    if (LupICD44.EditValue != null)
                                        icd[4] = LupICD44.EditValue.ToString();
                                    if (LupICD4.EditValue != null)
                                        icd[5] = LupICD55.EditValue.ToString();
                                }

                                string maicd = string.Join(";", icd);
                                nhaprv.MaICD = (DungChung.Bien.MaBV == "14017" ? txtMaICD.EditValue.ToString() + ";" + lupKhac.EditValue.ToString().Trim() : maicd).Replace(" ", "");
                                if (DungChung.Bien.MaBV == "20001")
                                {
                                    nhaprv.MaYHCT = DungChung.Ham.GetMaYHCT(DungChung.Ham.FreshString(nhaprv.MaICD), _licd10)[0];
                                    nhaprv.ChanDoanYHCT = DungChung.Ham.GetMaYHCT(DungChung.Ham.FreshString(nhaprv.MaICD), _licd10)[1];
                                }
                                string _benhphu = "";
                                benhkhac[0] = txtChanDoan.Text.Trim() + _benhphu;
                                benhkhac[1] = txtBenhKhac2.Text.Trim() + _benhphu;
                                benhkhac[2] = txtBenhKhac3.Text.Trim() + _benhphu;
                                benhkhac[3] = txtBenhKhac4.Text.Trim() + _benhphu;
                                if (DungChung.Bien.MaBV == "30009")
                                {
                                    benhkhac[4] = txtBenhKhac44.Text.Trim() + _benhphu;
                                    benhkhac[5] = txtBenhKhac55.Text.Trim() + _benhphu;
                                }

                                maicd = string.Join(";", benhkhac);
                                nhaprv.ChanDoan = DungChung.Bien.MaBV == "14017" ? txtChanDoan.Text.Trim() + _benhphu + ";" + txtBenhKhac1.Text.Trim() + _benhphu : maicd;
                                if (radHinhThuc.SelectedIndex == 0)
                                    nhaprv.Status = 2;
                                else
                                    if (radHinhThuc.SelectedIndex == 1)
                                    nhaprv.Status = 3;
                                else
                                    nhaprv.Status = 4;
                                if (!string.IsNullOrEmpty(txtSoNgaydt.Text))
                                    nhaprv.SoNgaydt = Convert.ToInt32(txtSoNgaydt.Text);
                                nhaprv.KetQua = cboKetQua.Text;
                                bnkb.PhuongAn = 0;
                                bnkb.NgayNghi = txtNgayRa.DateTime;
                                _data.SaveChanges();
                                DungChung.Ham._setStatus(_mabn, 2);
                                //  MessageBox.Show("Lưu thành công!");
                                if (DungChung.Bien.MaBV == "26007")
                                {
                                    var Ktrabn = _data.BenhNhans.Where(p => p.MaBNhan == _mabn).Where(p => p.DTuong == "BHYT").ToList();
                                    if (Ktrabn.Count > 0)
                                    {
                                        string sthe = Ktrabn.First().SThe.Trim();
                                        List<Person> q1 = _data.People.Where(p => p.SThe.Contains(sthe)).ToList();
                                        Person p1 = q1.First();
                                        p1.GhiChu = cboBenhManTinh.SelectedIndex == 1 ? "THA" : (cboBenhManTinh.SelectedIndex == 2 ? "TD" : null);
                                        _data.SaveChanges();
                                    }
                                }
                                if (DungChung.Bien.MaBV == "01071")
                                {
                                    var par = (from bnct in _data.BenhNhans.Where(o => o.MaBNhan == _mabn)
                                               join dt in _data.DThuocs.Where(o => o.PLDV == 1) on bnct.MaBNhan equals dt.MaBNhan
                                               join dtct in _data.DThuoccts.Where(o => o.Status == 0) on dt.IDDon equals dtct.IDDon
                                               select new { trangthai = dtct.Status }).ToList();
                                    if (par.Count() > 0)
                                    {
                                        MessageBox.Show("Bệnh nhân có các đơn thuốc chưa tạo phiếu lĩnh, không thể làm thủ tục ra viện", "Hỏi ra viện");
                                        return;

                                    }
                                    else
                                    {
                                        frm_Ravien_Load(sender, e);
                                    }
                                }
                                else
                                {
                                    frm_Ravien_Load(sender, e);
                                }

                                //}
                            }
                        }
                        else
                        {

                            if (_cotheluu)
                            {
                                if (bn != null && bn.NoThe)
                                {
                                    MessageBox.Show("Bệnh nhân nợ thẻ BHYT, bạn không thể lưu");
                                    return;
                                }
                                if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "56789")
                                    setDBSoLT01071();
                                RaVien nhaprv = new RaVien();
                                nhaprv.MaBNhan = _mabn;
                                nhaprv.PPDTr = txtPPDTr.Text;
                                nhaprv.LoiDan = txtLoiDan.Text;
                                nhaprv.NgayRa = txtNgayRa.DateTime;
                                nhaprv.NgayVao = txtNgayVao.DateTime;
                                nhaprv.ChanDoan = txtChanDoan.Text;
                                nhaprv.MaICD = txtMaICD.Text;
                                if (radHinhThuc.SelectedIndex == 0)
                                    nhaprv.Status = 2;
                                else
                                    if (radHinhThuc.SelectedIndex == 1)
                                    nhaprv.Status = 3;

                                else
                                    nhaprv.Status = 4;
                                int makp = 0;
                                if (lupKPDT.EditValue != null)
                                {
                                    nhaprv.MaKP = Convert.ToInt32(lupKPDT.EditValue);
                                    makp = Convert.ToInt32(lupKPDT.EditValue);
                                }
                                else
                                {
                                    nhaprv.MaKP = 0;
                                }
                                int MaCK = _lbnKB.Count > 0 ? _lbnKB.First().MaCK : -1;
                                nhaprv.MaCK = MaCK;
                                if (!string.IsNullOrEmpty(txtMaICD.Text))
                                    icd[0] = txtMaICD.EditValue.ToString();
                                if (LupICD2.EditValue != null)
                                    icd[1] = LupICD2.EditValue.ToString();
                                if (LupICD3.EditValue != null)
                                    icd[2] = LupICD3.EditValue.ToString();
                                if (LupICD4.EditValue != null)
                                    icd[3] = LupICD4.EditValue.ToString();
                                if (DungChung.Bien.MaBV == "30009")
                                {
                                    if (LupICD44.EditValue != null)
                                        icd[4] = LupICD44.EditValue.ToString();
                                    if (LupICD55.EditValue != null)
                                        icd[5] = LupICD55.EditValue.ToString();
                                }
                                string maicd = string.Join(";", icd);
                                nhaprv.MaICD = (DungChung.Bien.MaBV == "14017" ? txtMaICD.EditValue.ToString().Trim() + ";" + lupKhac.EditValue.ToString().Trim() : maicd).Replace(" ","");
                                if (DungChung.Bien.MaBV == "20001")
                                {
                                    nhaprv.MaYHCT = DungChung.Ham.GetMaYHCT(DungChung.Ham.FreshString(nhaprv.MaICD), _licd10)[0];
                                    nhaprv.ChanDoanYHCT = DungChung.Ham.GetMaYHCT(DungChung.Ham.FreshString(nhaprv.MaICD), _licd10)[1];
                                }
                                string _benhphu = "";
                                benhkhac[0] = txtChanDoan.Text.Trim() + _benhphu;
                                benhkhac[1] = txtBenhKhac2.Text.Trim() + _benhphu;
                                benhkhac[2] = txtBenhKhac3.Text.Trim() + _benhphu;
                                benhkhac[3] = txtBenhKhac4.Text.Trim() + _benhphu;
                                if (DungChung.Bien.MaBV == "30009")
                                {
                                    benhkhac[4] = txtBenhKhac44.Text.Trim() + _benhphu;
                                    benhkhac[5] = txtBenhKhac55.Text.Trim() + _benhphu;
                                }
                                maicd = string.Join(";", benhkhac);
                                nhaprv.ChanDoan = DungChung.Bien.MaBV == "14017" ? txtChanDoan.Text.Trim() + _benhphu + ";" + txtBenhKhac1.Text.Trim() + _benhphu : maicd;
                                nhaprv.SoLT = (txtSoLT.Text != "" && txtSoLT.Text != null) ? txtSoLT.Text.Trim() : null;
                                if (DungChung.Bien.MaBV == "27021" || DungChung.Bien.MaBV == "20001" || DungChung.Bien.MaBV == "12122")
                                    setDSSoHS();

                                nhaprv.MaYTe = (txtMaYT.Text != "" && txtMaYT.Text != null) ? txtMaYT.Text.Trim() : null;
                                if (!string.IsNullOrEmpty(txtSoNgaydt.Text))
                                    nhaprv.SoNgaydt = Convert.ToInt32(txtSoNgaydt.Text);
                                nhaprv.KetQua = cboKetQua.Text;
                                _data.RaViens.Add(nhaprv);
                                bnkb.PhuongAn = 0;
                                bnkb.NgayNghi = txtNgayRa.DateTime;
                                _data.SaveChanges();
                                DungChung.Ham._setStatus(_mabn, 2);
                                // MessageBox.Show("Lưu thành công!");
                                if (DungChung.Bien.MaBV == "26007")
                                {
                                    var Ktrabn = _data.BenhNhans.Where(p => p.MaBNhan == _mabn).Where(p => p.DTuong == "BHYT").ToList();
                                    if (Ktrabn.Count > 0)
                                    {
                                        string sthe = Ktrabn.First().SThe.Trim();
                                        List<Person> q1 = _data.People.Where(p => p.SThe.Contains(sthe)).ToList();
                                        Person p1 = q1.First();
                                        p1.GhiChu = cboBenhManTinh.SelectedIndex == 1 ? "THA" : (cboBenhManTinh.SelectedIndex == 2 ? "TD" : null);
                                        _data.SaveChanges();
                                    }
                                }
                                if (DungChung.Bien.MaBV == "01071")
                                {
                                    var par = (from bnct in _data.BenhNhans.Where(o => o.MaBNhan == _mabn)
                                               join dt in _data.DThuocs.Where(o => o.PLDV == 1) on bnct.MaBNhan equals dt.MaBNhan
                                               join dtct in _data.DThuoccts.Where(o => o.Status == 0) on dt.IDDon equals dtct.IDDon
                                               select new { trangthai = dtct.Status }).ToList();
                                    if (par.Count() > 0)
                                    {
                                        MessageBox.Show("Bệnh nhân có các đơn thuốc chưa tạo phiếu lĩnh, không thể làm thủ tục ra viện", "Hỏi ra viện");
                                        return;

                                    }
                                    else
                                    {
                                        frm_Ravien_Load(sender, e);
                                    }
                                }
                                else
                                {
                                    frm_Ravien_Load(sender, e);
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Bệnh nhân chưa khám bệnh vào viện");
            }
        }
        private void getSoLT_Edit()
        {
            string solt = txtSoLT.Text;
            int ot;

            if (!string.IsNullOrEmpty(txtSoLT.Text))
            {
                if (Int32.TryParse(txtSoLT.Text, out ot))
                    _soLT = Convert.ToInt32(txtSoLT.Text);
                else
                {
                    for (int i = 0; i < solt.Length; i++)
                    {
                        string tem = solt.Substring(solt.Length - i - 1, i + 1);
                        if (Int32.TryParse(tem, out ot))
                            _soLT = Convert.ToInt32(tem);
                        else
                            break;
                    }
                }

            }
        }
        int _soLT01071 = 0;
        void setDBSoLT01071()
        {
            if (DungChung.Bien.PP_SoLT == 1 || DungChung.Bien.PP_SoLT == 2)
            {
                int rs, so = 0, makp = 0;
                if (DungChung.Bien.PP_SoLT == 1 && lupKPDT.EditValue != null && lupKPDT.EditValue.ToString() != "")
                    makp = Convert.ToInt32(lupKPDT.EditValue);
                //if (Int32.TryParse(txtSoLT.Text, out rs))
                //{
                //    so = Convert.ToInt32(txtSoLT.Text);
                //}
                so = _soLT01071;
                if (!DungChung.Ham.checkSoPL(makp, so, 7, -1))
                {
                    DungChung.Ham.SetSoPL(makp, so, 7, -1);
                }
                else
                {
                    DungChung.Ham.SetSoPL(makp, 0, 7, -1);
                    GetSoLT01071();
                }
                // }
            }
        }
        private void setDSSoHS()
        {
            int noingoaitru = -1;
            if (DungChung.Bien.MaBV == "27021")
            {
                var qbn = _data.BenhNhans.Where(p => p.MaBNhan == _mabn).FirstOrDefault();
                if (qbn != null)
                    noingoaitru = qbn.NoiTru ?? 0;
            }

            if (DungChung.Bien.PP_SoLT == 1 || DungChung.Bien.PP_SoLT == 2)
            {
                int rs, so = 0, makp = 0;
                if (DungChung.Bien.PP_SoLT == 1 && lupKPDT.EditValue != null && lupKPDT.EditValue.ToString() != "")
                    makp = Convert.ToInt32(lupKPDT.EditValue);
                //if (Int32.TryParse(txtSoLT.Text, out rs))
                //{
                //    so = Convert.ToInt32(txtSoLT.Text);
                //}
                so = _soLT;
                if (!DungChung.Ham.checkSoPL(makp, so, 7, noingoaitru))
                {
                    DungChung.Ham.SetSoPL(makp, so, 7, noingoaitru);
                }
                else
                {
                    DungChung.Ham.SetSoPL(makp, 0, 7, noingoaitru);

                    setSoHS();
                }
                // }
            }
        }
        #region giay ra vien
        public bool InGiayRaVienTT18(int mbn)
        {
            _data = new QLBVEntities(DungChung.Bien.StrCon);
            frmIn frm = new frmIn();
            BaoCao.repGiayRaVien rep = new BaoCao.repGiayRaVien();
            rep.SubBand1.Visible = false;
            rep.SubBand2.Visible = false;
            rep.SubBand3.Visible = false;
            rep.sub_RaVienTT18.Visible = true;
            rep.colTD_GDBV.Text = "Trưởng khoa";
            
            var par = (from bn in _data.BenhNhans
                       join vv in _data.VaoViens on bn.MaBNhan equals vv.MaBNhan
                       join rv in _data.RaViens on bn.MaBNhan equals rv.MaBNhan
                       join ttbx in _data.TTboXungs on bn.MaBNhan equals ttbx.MaBNhan into kq
                       from kq2 in kq.DefaultIfEmpty()
                       where (bn.MaBNhan == mbn)
                       select new { bn, rv, kq2, vv }).ToList();
            if (par.Count > 0)
            {
                foreach (var item in par)
                {
                    if (DungChung.Bien.MaBV == "14017")
                    {
                        if (!string.IsNullOrEmpty(item.rv.ChanDoan))
                        {
                            var spChanDoan = item.rv.ChanDoan.Split(';');
                            if (spChanDoan != null && spChanDoan.Count() > 0)
                            {
                                if (!string.IsNullOrEmpty(item.rv.MaICD))
                                {
                                    var spMaIcd = item.rv.MaICD.Split(';').ToArray();
                                    if (spMaIcd != null && spMaIcd.Count() > 0)
                                    {
                                        if (spChanDoan.Count() <= spMaIcd.Count())
                                        {
                                            for (int i = 0; i < spChanDoan.Count(); i++)
                                            {
                                                if (!string.IsNullOrEmpty(spMaIcd[i]))
                                                    spChanDoan[i] = " [" + spMaIcd[i] + "] " + spChanDoan[i];
                                            }
                                        }
                                        else
                                        {
                                            for (int i = 0; i < spMaIcd.Count(); i++)
                                            {
                                                if (!string.IsNullOrEmpty(spMaIcd[i]))
                                                    spChanDoan[i] = " [" + spMaIcd[i] + "] " + spChanDoan[i];
                                            }
                                        }
                                    }
                                }
                            }

                            item.rv.ChanDoan = string.Join(";", spChanDoan);
                        }
                        else if (DungChung.Bien.MaBV == "30003")
                            if (!string.IsNullOrEmpty(item.rv.ChanDoan))
                            {
                                var spChanDoan = item.rv.ChanDoan.Split(';');
                                if (spChanDoan != null && spChanDoan.Count() > 0)
                                {
                                    if (!string.IsNullOrEmpty(item.rv.MaICD))
                                    {
                                        var spMaIcd = item.rv.MaICD.Split(';').Skip(0).Take(3).ToArray();
                                        if (spMaIcd != null && spMaIcd.Count() > 0)
                                        {
                                            if (spChanDoan.Count() <= spMaIcd.Count())
                                            {
                                                for (int i = 0; i < spChanDoan.Count(); i++)
                                                {
                                                    if (!string.IsNullOrEmpty(spMaIcd[i]))
                                                        spChanDoan[i] = spChanDoan[i] + " (" + spMaIcd[i] + ")";
                                                }
                                            }
                                            else
                                            {
                                                for (int i = 0; i < spMaIcd.Count(); i++)
                                                {
                                                    if (!string.IsNullOrEmpty(spMaIcd[i]))
                                                        spChanDoan[i] = spChanDoan[i] + " (" + spMaIcd[i] + ")";
                                                }
                                            }
                                        }
                                    }
                                }
                                item.rv.ChanDoan = string.Join(";", spChanDoan);
                            }
                    }
                }

                var par1 = (from p in par
                            select new {p.bn.NgaySinh, p.bn.ThangSinh, p.bn.NamSinh, p.rv.MaKP, p.rv.ChanDoan, p.rv.MaICD, NoiLV = p.kq2 == null ? "" : p.kq2.NoiLV, p.vv.SoBA, p.vv.SoVV, p.bn.TenBNhan, p.bn.Tuoi, p.bn.DTuong, p.bn.GTinh, MaNN = p.kq2 == null ? "" : p.kq2.MaNN, p.bn.HanBHTu, p.bn.HanBHDen, p.bn.SThe, p.bn.DChi, p.vv.NgayVao, p.rv.NgayRa, p.rv.PPDTr, p.rv.LoiDan }
                              );

                var dtoc = (from tt in _data.TTboXungs.Where(p => p.MaBNhan == mbn) join dt in _data.DanTocs on tt.MaDT equals dt.MaDT select dt.TenDT).ToList();
                if (dtoc.Count > 0)
                    rep.Parameters["TenDT"].Value = dtoc.First();
                rep.Parameters["TenBNhan"].Value = par1.First().TenBNhan.ToUpper();
                rep.Parameters["TenCQCQ"].Value = DungChung.Bien.TenCQCQ;
                rep.Parameters["TenCQ"].Value = DungChung.Bien.TenCQ;
                rep.Parameters["Tuoi"].Value = par1.First().Tuoi;
                rep.Parameters["GTinh"].Value = par1.First().GTinh == 1 ? "Nam" : "Nữ";
                string mann = "";
                if (par1.First().MaNN != null)
                    mann = par1.First().MaNN;
                var nn = _data.DmNNs.Where(p => p.MaNN == (mann)).FirstOrDefault();
                string _NN = "";
                _NN = nn == null ? "" : nn.TenNN;
                if (!string.IsNullOrEmpty(par1.First().NoiLV))
                {
                    if (DungChung.Bien.MaBV != "24009")
                        _NN += " - " + par1.First().NoiLV;
                }
                rep.Parameters["NgheNghiep"].Value = _NN;
                rep.Parameters["BHYT"].Value = par1.First().SThe;
                rep.Parameters["DChi"].Value = par1.First().DChi;
                rep.Parameters["PPDTr"].Value = par1.First().PPDTr;
                rep.Parameters["LoiDan"].Value = par1.First().LoiDan;
                rep.Parameters["NgaySinh"].Value = par1.First().NgaySinh;
                rep.Parameters["ThangSinh"].Value = par1.First().ThangSinh;
                rep.Parameters["NamSinh"].Value = par1.First().NamSinh;
                //if (par1.First().DTuong == "BHYT")
                //{
                //rep.HanBHTu.Value = par.First().HanBHTu;
                //rep.HanBHDen.Value = par.First().HanBHDen;
                //rep.SThe.Value = par1.First().SThe;

                //}

                if (par1.First().NgayVao != null)
                {
                    rep.Parameters["NgayVao"].Value = par1.First().NgayVao.Value.Hour + " giờ " + par1.First().NgayVao.Value.Minute + " phút, Ngày " + par1.First().NgayVao.Value.Day + "  tháng  " + par1.First().NgayVao.Value.Month + "  năm  " + par1.First().NgayVao.Value.Year;
                }
                if (par1.First().NgayRa != null)
                {
                    rep.Parameters["NgayRa"].Value = par1.First().NgayRa.Value.Hour + " giờ " + par1.First().NgayRa.Value.Minute + " phút, Ngày " + par1.First().NgayRa.Value.Day + "  tháng  " + par1.First().NgayRa.Value.Month + "  năm  " + par1.First().NgayRa.Value.Year;
                }
                //if (DungChung.Bien.MaBV == "08204")
                //{
                //    if (par1.First().SoBA != null)
                //        rep.SoVV.Value = "Số vào viện: " + par1.First().SoBA;
                //}

                rep.Parameters["SoLuu"].Value = (txtSoLT.Text != "" && txtSoLT.Text != null) ? txtSoLT.Text : null;
                rep.Parameters["MaYTe"].Value = (txtMaYT.Text != "" && txtMaYT.Text != null) ? txtMaYT.Text : null;
                rep.Parameters["ChanDoan"].Value = DungChung.Ham.FreshString_WithCode(par1.First().ChanDoan, par1.First().MaICD);
                int _makp = 0;
                if (par1.First().MaKP != null)
                    _makp = Convert.ToInt32(par1.First().MaKP);
                //thêm tên trưởng khoa
                rep.Parameters["HoTenTruongKhoa"].Value = DungChung.Bien.TruongKhoaLS;
                var kp = _data.KPhongs.Where(p => p.MaKP == _makp).FirstOrDefault();
                rep.Parameters["Khoa"].Value = kp.TenKP;
                rep.Parameters["NgayThangDT"].Value = (DungChung.Bien.MaBV == "12001" || DungChung.Bien.MaBV == "24012" || DungChung.Bien.MaBV == "24389" || DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "56789" || DungChung.Bien.MaBV == "14017") ? ("Ngày " + par1.First().NgayRa.Value.Day + "  tháng  " + par1.First().NgayRa.Value.Month + "  năm  " + par1.First().NgayRa.Value.Year) : "Ngày ..... tháng ..... năm 20...";
                rep.Parameters["NgayThangGD"].Value = (DungChung.Bien.MaBV == "12001" || DungChung.Bien.MaBV == "24012" || DungChung.Bien.MaBV == "24389" || DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "56789" || DungChung.Bien.MaBV == "14017") ? ("Ngày " + par1.First().NgayRa.Value.Day + "  tháng  " + par1.First().NgayRa.Value.Month + "  năm  " + par1.First().NgayRa.Value.Year) : "Ngày ..... tháng ..... năm 20...";

                rep.Parameters["GiamDoc"].Value = DungChung.Bien.MaBV == "14018" ? "" : DungChung.Bien.GiamDoc;
            }
            rep.CreateDocument();
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();
            return true;
        }
        public bool InGiayRaVien(int mbn)
        {
            //try
            //{
            _data = new QLBVEntities(DungChung.Bien.StrCon);
            frmIn frm = new frmIn();
            if (DungChung.Bien.MaBV == "20001" || DungChung.Bien.MaBV == "30007" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "24012" || DungChung.Bien.MaBV == "24389")
            {
                BaoCao.repGiayRaVien_12121 rep = new BaoCao.repGiayRaVien_12121();

                var par = (from bn in _data.BenhNhans.Where(o => o.MaBNhan == mbn)
                           join vv in _data.VaoViens on bn.MaBNhan equals vv.MaBNhan
                           join rv in _data.RaViens on bn.MaBNhan equals rv.MaBNhan
                           join ttbx in _data.TTboXungs on bn.MaBNhan equals ttbx.MaBNhan into kq
                           from kq2 in kq.DefaultIfEmpty()
                           where (bn.MaBNhan == mbn)
                           select new { rv.MaKP, rv.ChanDoan, rv.ChanDoanYHCT, bn.NgaySinh, bn.ThangSinh, bn.NamSinh, rv.MaICD, NoiLV = kq2 == null ? "" : kq2.NoiLV, vv.SoBA, vv.SoVV, bn.TenBNhan, bn.Tuoi, bn.DTuong, bn.GTinh, MaNN = kq2 == null ? "" : kq2.MaNN, bn.HanBHTu, bn.HanBHDen, bn.SThe, bn.DChi, vv.NgayVao, rv.NgayRa, rv.PPDTr, rv.LoiDan }).ToList();
                if (par.Count > 0)
                {
                    var dtoc = (from tt in _data.TTboXungs.Where(p => p.MaBNhan == mbn) join dt in _data.DanTocs on tt.MaDT equals dt.MaDT select dt.TenDT).ToList();
                    if (dtoc.Count > 0)
                        rep.TenDT.Value = dtoc.First();
                    rep.TenBNhan.Value = par.First().TenBNhan.ToUpper();
                    rep.Tuoi.Value = DungChung.Ham.TuoitheoThang(_data, mbn, DungChung.Bien.formatAge);
                    rep.GTinh.Value = par.First().GTinh == 1 ? "Nam" : "Nữ";
                    string mann = "";
                    if (par.First().MaNN != null)
                        mann = par.First().MaNN;
                    var nn = _data.DmNNs.Where(p => p.MaNN == (mann)).FirstOrDefault();
                    string _NN = "";
                    _NN = nn == null ? "" : nn.TenNN;
                    if (!string.IsNullOrEmpty(par.First().NoiLV))
                    {
                        if (DungChung.Bien.MaBV != "24009" && DungChung.Bien.MaBV != "20001")
                            _NN += " - " + par.First().NoiLV;
                    }
                    rep.NgheNghiep.Value = _NN;

                    rep.DChi.Value = par.First().DChi;

                    rep.PPDTr.Value = par.First().PPDTr;
                    rep.LoiDan.Value = par.First().LoiDan;
                    if (par.First().DTuong == "BHYT")
                    {
                        rep.HanBHTu.Value = par.First().HanBHTu;
                        //rep.HanBHDen.Value = par.First().HanBHDen;
                        rep.SThe.Value = par.First().SThe;
                        if (par.First().SThe.Contains("TE") && DungChung.Bien.MaBV == "30007")
                        {
                            rep.Tuoi.Value = DungChung.Ham.TuoitheoThang(_data, mbn, DungChung.Bien.formatAge) + "(" + par.First().NgaySinh + "/" + par.First().ThangSinh + "/" + par.First().NamSinh + ")";
                        }
                        else
                            rep.Tuoi.Value = DungChung.Ham.TuoitheoThang(_data, mbn, DungChung.Bien.formatAge);
                    }
                    else
                    {
                        rep.Tuoi.Value = DungChung.Ham.TuoitheoThang(_data, mbn, DungChung.Bien.formatAge);
                    }

                    if (par.First().NgayVao != null)
                    {
                        rep.NgayVao.Value = par.First().NgayVao.Value.Hour + " giờ " + par.First().NgayVao.Value.Minute + " phút, Ngày " + par.First().NgayVao.Value.Day + "  tháng  " + par.First().NgayVao.Value.Month + "  năm  " + par.First().NgayVao.Value.Year;
                    }
                    //rep.NgayVao.Value = par.First().NgayVao;
                    if (par.First().NgayRa != null)
                    {
                        rep.NgayRa.Value = par.First().NgayRa.Value.Hour + " giờ " + par.First().NgayRa.Value.Minute + " phút, Ngày " + par.First().NgayRa.Value.Day + "  tháng  " + par.First().NgayRa.Value.Month + "  năm  " + par.First().NgayRa.Value.Year;
                    }
                    if (DungChung.Bien.MaBV == "08204")
                    {
                        if (par.First().SoBA != null)
                            rep.SoVV.Value = "Số vào viện: " + par.First().SoBA;
                    }

                    rep.SoLuu.Value = (txtSoLT.Text != "" && txtSoLT.Text != null) ? txtSoLT.Text : null;
                    rep.MaYTe.Value = (txtMaYT.Text != "" && txtMaYT.Text != null) ? txtMaYT.Text : null;
                    if (DungChung.Bien.MaBV == "20001" || DungChung.Bien.MaBV == "24297")
                    {
                        var icd = _data.ICD10.Where(o => true).ToList();
                        rep.ChanDoan.Value = DungChung.Ham.GhepChuoiChanDoanYHCT(icd, "", par.First().MaICD);
                    }

                    else
                        rep.ChanDoan.Value = DungChung.Ham.FreshString(DungChung.Ham.GetICDstr(par.First().ChanDoan));
                    int _makp = 0;
                    if (par.First().MaKP != null)
                        _makp = Convert.ToInt32(par.First().MaKP);
                    //thêm tên trưởng khoa
                    //if (DungChung.Bien.MaBV == "26007")
                    //{
                    //    var qcb = _data.CanBoes.Where(p => p.MaKP == _makp).Where(p => p.ChucVu == "Trưởng khoa" || p.ChucVu == "trưởng khoa").FirstOrDefault();
                    //    if (qcb != null)
                    //        rep.HoTenTruongKhoa.Value = "Họ tên: " + qcb.TenCB;
                    //    else
                    //    {
                    //        rep.HoTenTruongKhoa.Value = "Họ tên:.......................";
                    //    }
                    //}
                    //else

                    var kp = _data.KPhongs.Where(p => p.MaKP == _makp).FirstOrDefault();
                    rep.Khoa.Value = kp.TenKP;
                    rep.NgayThangDT.Value = DungChung.Bien.MaBV == "12121" || DungChung.Bien.MaBV == "24012" || DungChung.Bien.MaBV == "24389" || DungChung.Bien.MaBV == "14017" ? ("Ngày " + par.First().NgayRa.Value.Day + "  Tháng  " + par.First().NgayRa.Value.Month + "  Năm  " + par.First().NgayRa.Value.Year) : "Ngày ..... tháng ..... năm 20...";
                    rep.NgayThangGD.Value = DungChung.Bien.MaBV == "12121" || DungChung.Bien.MaBV == "24012" || DungChung.Bien.MaBV == "24389" || DungChung.Bien.MaBV == "14017" ? ("Ngày " + par.First().NgayRa.Value.Day + "  Tháng  " + par.First().NgayRa.Value.Month + "  Năm  " + par.First().NgayRa.Value.Year) : "Ngày ..... tháng ..... năm 20...";
                    rep.GiamDoc.Value = DungChung.Bien.GiamDoc;
                    rep.HoTenTruongKhoa.Value = string.IsNullOrEmpty(DungChung.Bien.TruongKhoaLS) ? DungChung.Bien.MaBV == "24297" ? "" : "Họ tên:................." : "Họ tên: " + DungChung.Bien.TruongKhoaLS;

                }
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
                return true;
            }
            else
            {
                BaoCao.repGiayRaVien rep = new BaoCao.repGiayRaVien();
                if (DungChung.Bien.MaBV == "27023")
                {
                    rep.SubBand1.Visible = true;
                }
                else
                {
                    rep.SubBand2.Visible = true;
                }
                if (DungChung.Bien.MaBV == "24009")
                {
                    rep.SubBand3.Visible = true;
                    rep.SubBand2.Visible = false;
                    rep.SubBand1.Visible = false;
                }
                else
                {
                    rep.SubBand3.Visible = false;
                }
                var par = (from bn in _data.BenhNhans
                           join vv in _data.VaoViens on bn.MaBNhan equals vv.MaBNhan
                           join rv in _data.RaViens on bn.MaBNhan equals rv.MaBNhan
                           join ttbx in _data.TTboXungs on bn.MaBNhan equals ttbx.MaBNhan into kq
                           from kq2 in kq.DefaultIfEmpty()
                           where (bn.MaBNhan == mbn)
                           select new { bn, rv, kq2, vv }).ToList();
                if (par.Count > 0)
                {
                    foreach (var item in par)
                    {
                        if (DungChung.Bien.MaBV == "14017")
                        {
                            if (!string.IsNullOrEmpty(item.rv.ChanDoan))
                            {
                                var spChanDoan = item.rv.ChanDoan.Split(';');
                                if (spChanDoan != null && spChanDoan.Count() > 0)
                                {
                                    if (!string.IsNullOrEmpty(item.rv.MaICD))
                                    {
                                        var spMaIcd = item.rv.MaICD.Split(';').ToArray();
                                        if (spMaIcd != null && spMaIcd.Count() > 0)
                                        {
                                            if (spChanDoan.Count() <= spMaIcd.Count())
                                            {
                                                for (int i = 0; i < spChanDoan.Count(); i++)
                                                {
                                                    if (!string.IsNullOrEmpty(spMaIcd[i]))
                                                        spChanDoan[i] = " [" + spMaIcd[i] + "] " + spChanDoan[i];
                                                }
                                            }
                                            else
                                            {
                                                for (int i = 0; i < spMaIcd.Count(); i++)
                                                {
                                                    if (!string.IsNullOrEmpty(spMaIcd[i]))
                                                        spChanDoan[i] = " [" + spMaIcd[i] + "] " + spChanDoan[i];
                                                }
                                            }
                                        }
                                    }
                                }

                                item.rv.ChanDoan = string.Join(";", spChanDoan);
                            }
                            else if (DungChung.Bien.MaBV == "30003")
                                if (!string.IsNullOrEmpty(item.rv.ChanDoan))
                                {
                                    var spChanDoan = item.rv.ChanDoan.Split(';');
                                    if (spChanDoan != null && spChanDoan.Count() > 0)
                                    {
                                        if (!string.IsNullOrEmpty(item.rv.MaICD))
                                        {
                                            var spMaIcd = item.rv.MaICD.Split(';').Skip(0).Take(3).ToArray();
                                            if (spMaIcd != null && spMaIcd.Count() > 0)
                                            {
                                                if (spChanDoan.Count() <= spMaIcd.Count())
                                                {
                                                    for (int i = 0; i < spChanDoan.Count(); i++)
                                                    {
                                                        if (!string.IsNullOrEmpty(spMaIcd[i]))
                                                            spChanDoan[i] = spChanDoan[i] + " (" + spMaIcd[i] + ")";
                                                    }
                                                }
                                                else
                                                {
                                                    for (int i = 0; i < spMaIcd.Count(); i++)
                                                    {
                                                        if (!string.IsNullOrEmpty(spMaIcd[i]))
                                                            spChanDoan[i] = spChanDoan[i] + " (" + spMaIcd[i] + ")";
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    item.rv.ChanDoan = string.Join(";", spChanDoan);
                                }
                        }
                    }

                    var par1 = (from p in par
                                select new { p.bn.NamSinh, p.rv.MaKP, p.rv.ChanDoan, p.rv.MaICD, NoiLV = p.kq2 == null ? "" : p.kq2.NoiLV, p.vv.SoBA, p.vv.SoVV, p.bn.TenBNhan, p.bn.Tuoi, p.bn.DTuong, p.bn.GTinh, MaNN = p.kq2 == null ? "" : p.kq2.MaNN, p.bn.HanBHTu, p.bn.HanBHDen, p.bn.SThe, p.bn.DChi, p.vv.NgayVao, p.rv.NgayRa, p.rv.PPDTr, p.rv.LoiDan }
                                  );

                    var dtoc = (from tt in _data.TTboXungs.Where(p => p.MaBNhan == mbn) join dt in _data.DanTocs on tt.MaDT equals dt.MaDT select dt.TenDT).ToList();
                    if (dtoc.Count > 0)
                        rep.TenDT.Value = dtoc.First();
                    rep.TenBNhan.Value = par1.First().TenBNhan.ToUpper();
                    if (DungChung.Bien.MaBV == "14018")
                    {
                        rep.Tuoi.Value = DungChung.Ham.CalculateAge(par.First().bn.NgaySinh, par.First().bn.ThangSinh, par.First().bn.NamSinh, " tháng tuổi.");
                    }
                    else
                        rep.Tuoi.Value = (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "56789") ? par1.First().NamSinh : DungChung.Ham.TuoitheoThang(_data, mbn, DungChung.Bien.formatAge);
                    rep.GTinh.Value = par1.First().GTinh == 1 ? "Nam" : "Nữ";
                    string mann = "";
                    if (par1.First().MaNN != null)
                        mann = par1.First().MaNN;
                    var nn = _data.DmNNs.Where(p => p.MaNN == (mann)).FirstOrDefault();
                    string _NN = "";
                    _NN = nn == null ? "" : nn.TenNN;
                    if (!string.IsNullOrEmpty(par1.First().NoiLV))
                    {
                        if (DungChung.Bien.MaBV != "24009")
                            _NN += " - " + par1.First().NoiLV;
                    }
                    rep.NgheNghiep.Value = _NN;
                    rep.BHYT.Value = par1.First().SThe;
                    rep.DChi.Value = par1.First().DChi;

                    rep.PPDTr.Value = par1.First().PPDTr;
                    rep.LoiDan.Value = par1.First().LoiDan;
                    if (par1.First().DTuong == "BHYT")
                    {
                        //rep.HanBHTu.Value = par.First().HanBHTu;
                        //rep.HanBHDen.Value = par.First().HanBHDen;
                        rep.SThe.Value = par1.First().SThe;

                    }

                    if (par1.First().NgayVao != null)
                    {
                        rep.NgayVao.Value = par1.First().NgayVao.Value.Hour + " giờ " + par1.First().NgayVao.Value.Minute + " phút, Ngày " + par1.First().NgayVao.Value.Day + "  tháng  " + par1.First().NgayVao.Value.Month + "  năm  " + par1.First().NgayVao.Value.Year;
                    }
                    //rep.NgayVao.Value = par.First().NgayVao;
                    if (par1.First().NgayRa != null)
                    {
                        rep.NgayRa.Value = par1.First().NgayRa.Value.Hour + " giờ " + par1.First().NgayRa.Value.Minute + " phút, Ngày " + par1.First().NgayRa.Value.Day + "  tháng  " + par1.First().NgayRa.Value.Month + "  năm  " + par1.First().NgayRa.Value.Year;
                    }
                    if (DungChung.Bien.MaBV == "08204")
                    {
                        if (par1.First().SoBA != null)
                            rep.SoVV.Value = "Số vào viện: " + par1.First().SoBA;
                    }

                    rep.SoLuu.Value = (txtSoLT.Text != "" && txtSoLT.Text != null) ? txtSoLT.Text : null;
                    rep.MaYTe.Value = (txtMaYT.Text != "" && txtMaYT.Text != null) ? txtMaYT.Text : null;
                    if (DungChung.Bien.MaBV == "30003" || DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049")
                    {
                        rep.ChanDoan.Value = DungChung.Ham.FreshString_WithCode(par1.First().ChanDoan, par1.First().MaICD);
                    }
                    else
                        rep.ChanDoan.Value = DungChung.Ham.FreshString(DungChung.Ham.GetICDstr(par1.First().ChanDoan));
                    int _makp = 0;
                    if (par1.First().MaKP != null)
                        _makp = Convert.ToInt32(par1.First().MaKP);
                    //thêm tên trưởng khoa
                    if (DungChung.Bien.MaBV == "26007")
                    {
                        var qcb = _data.CanBoes.Where(p => p.MaKP == _makp).Where(p => p.ChucVu == "Trưởng khoa" || p.ChucVu == "trưởng khoa").FirstOrDefault();
                        if (qcb != null)
                            rep.HoTenTruongKhoa.Value = qcb.TenCB;
                        else
                        {
                            rep.HoTenTruongKhoa.Value = "";
                        }
                    }
                    else if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049")
                    {
                        rep.HoTenTruongKhoa.Value = DungChung.Bien.TruongKhoaLS;
                    }
                    else if (DungChung.Bien.MaBV == "14018")
                    {
                        rep.HoTenTruongKhoa.Value = "";
                    }
                    else rep.HoTenTruongKhoa.Value = "";

                    var kp = _data.KPhongs.Where(p => p.MaKP == _makp).FirstOrDefault();
                    rep.Khoa.Value = kp.TenKP;
                    if (DungChung.Bien.MaBV == "27023")
                    {
                        //string x = "KHOA: " + kp.TenKP.Remove(0, 4);
                        rep.Khoa.Value = kp.TenKP.ToUpper();
                    }
                    rep.NgayThangDT.Value = (DungChung.Bien.MaBV == "12001" || DungChung.Bien.MaBV == "24012" || DungChung.Bien.MaBV == "24389" || DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "56789" || DungChung.Bien.MaBV == "14017") ? ("Ngày " + par1.First().NgayRa.Value.Day + "  tháng  " + par1.First().NgayRa.Value.Month + "  năm  " + par1.First().NgayRa.Value.Year) : "Ngày ..... tháng ..... năm 20...";
                    rep.NgayThangGD.Value = (DungChung.Bien.MaBV == "12001" || DungChung.Bien.MaBV == "24012" || DungChung.Bien.MaBV == "24389" || DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "56789" || DungChung.Bien.MaBV == "14017") ? ("Ngày " + par1.First().NgayRa.Value.Day + "  tháng  " + par1.First().NgayRa.Value.Month + "  năm  " + par1.First().NgayRa.Value.Year) : "Ngày ..... tháng ..... năm 20...";

                    rep.GiamDoc.Value = DungChung.Bien.MaBV == "14018" ? "" : DungChung.Bien.GiamDoc;
                }
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
                return true;
                //}
                //catch (Exception)
                //{
                //    return false;
                //}
            }
        }
        #endregion
        private void btnInPhieu_Click(object sender, EventArgs e)
        {
            //duc da o day
            int rs;

            int mbn = 0;
            if (Int32.TryParse(txtMaBNhan.Text, out rs))
                mbn = Convert.ToInt32(txtMaBNhan.Text);
            if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049")
            {
                if (comboBox1.SelectedIndex == 0)
                {
                    InGiayRaVien(mbn);
                }
                if (comboBox1.SelectedIndex == 1)
                {
                    InGiayRaVienTT18(mbn);
                }
                if(comboBox1.SelectedIndex == -1)
                {
                    MessageBox.Show("Chưa chọn mẫu in");
                }    
            }
            else
                InGiayRaVien(mbn);
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void txtPPDTr_EditValueChanged(object sender, EventArgs e)
        {
            if (_ravien.Count > 0)
            {
                if (txtPPDTr.Text != _ravien.First().PPDTr)
                    btnLuu.Enabled = true;
                else
                    btnLuu.Enabled = false;
            }
            else btnLuu.Enabled = true;
        }

        private void txtLoiDan_EditValueChanged(object sender, EventArgs e)
        {
            if (_ravien.Count > 0)
            {
                if (txtLoiDan.Text != _ravien.First().LoiDan)
                    btnLuu.Enabled = true;
                else
                    btnLuu.Enabled = false;
            }
            else btnLuu.Enabled = true;
        }
        int songaydt = 0;
        private void txtNgayRa_Leave(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi! " + ex.Message);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            int makp = 0;

            if (lupKPDT.EditValue != null)
                makp = Convert.ToInt32(lupKPDT.EditValue);
            try
            {
                DialogResult _result = MessageBox.Show("Bạn muốn xóa thông tin ra viện của BN?", "Hỏi xóa!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (_result == DialogResult.Yes)
                {
                    #region 12122
                    bool ktXoa = true;
                    int noingoaitru = -1;
                    if (DungChung.Bien.MaBV == "12122")// update lại bảng số phiếu lĩnh khi số lưu trữ là số cuối cùng
                    {
                        int Soltht = 0;

                        if (DungChung.Bien.PP_SoLT == 1)
                        {

                            if (makp > 0)
                            {
                                makp = Convert.ToInt32(lupKPDT.EditValue);
                                Soltht = DungChung.Ham.GetSoPL(7, makp, noingoaitru);
                            }
                        }
                        else if (DungChung.Bien.PP_SoLT == 2)
                        {
                            Soltht = DungChung.Ham.GetSoPL(7, 0, noingoaitru);
                        }

                        if (Soltht == _soLT + 1)
                        {
                            SoPL qsopl = new SoPL();
                            qsopl.SoPL1 = _soLT - 1;
                            qsopl.MaKP = (DungChung.Bien.PP_SoLT == 2) ? 0 : makp;
                            qsopl.PhanLoai = 7;
                            qsopl.NoiTru = noingoaitru;
                            qsopl.NgayNhap = DateTime.Now;
                            qsopl.Status = 1;
                            _data.SoPLs.Add(qsopl);
                            _data.SaveChanges();
                            SoPL qsoplXoa = _data.SoPLs.Where(p => (DungChung.Bien.PP_SoLT == 2) ? true : p.MaKP == makp).Where(p => p.PhanLoai == 7).Where(p => p.NoiTru == noingoaitru).OrderByDescending(p => p.SoPL1).FirstOrDefault();
                            if (qsoplXoa != null)
                            {
                                _data.SoPLs.Remove(qsoplXoa);
                                _data.SaveChanges();
                            }
                        }
                        else
                        {
                            DialogResult dl = MessageBox.Show("Số lưu trữ không phải là số cuối cùng (" + (Soltht - 1) + "), bạn có chắc chắn muốn xóa ra viện không ?", "Hỏi xóa!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (dl == DialogResult.Yes)
                            {
                                DungChung.Ham.UpdateHSHuy(_mabn, makp, txtSoLT.Text.Trim(), 7, noingoaitru);
                            }
                            else
                            {
                                ktXoa = false;
                            }
                        }
                    }
                    else
                        DungChung.Ham.UpdateHSHuy(_mabn, makp, txtSoLT.Text.Trim(), 7, noingoaitru);
                    #endregion
                    if (ktXoa)
                    {
                        #region xóa
                        if (!DungChung.Ham.KTraTT(_data, _mabn))
                        {
                            if (DungChung.Bien.MaBV == "27021")
                            {
                                var qbn = _data.BenhNhans.Where(p => p.MaBNhan == _mabn).FirstOrDefault();
                                if (qbn != null)
                                    noingoaitru = qbn.NoiTru ?? 0;
                            }
                            var ktrv = _data.RaViens.Where(p => p.MaBNhan == _mabn).Where(p => p.Status == 2 || p.Status == 3 || p.Status == 4).ToList();
                            if (ktrv.Count > 0)
                            {
                                int id = ktrv.First().MaBNhan;
                                var bnkb = _data.BNKBs.Where(p => p.MaBNhan == _mabn).OrderByDescending(p => p.IDKB).FirstOrDefault();
                                var xoa = _data.RaViens.Single(p => p.MaBNhan == id);



                                _data.RaViens.Remove(xoa);
                                bnkb.PhuongAn = 4;
                                bnkb.NgayNghi = null;
                                _data.SaveChanges();
                                //dung290516
                                //DungChung.Ham._setStatus(_mabn, 1);
                                var qcls = _data.CLS.Where(p => p.MaBNhan == _mabn).ToList();
                                if (qcls.Count == 0)
                                    DungChung.Ham._setStatus(_mabn, 1);// bệnh nhân đã khám
                                else
                                    DungChung.Ham._setStatus(_mabn, 5);// bệnh nhân đã có kqCLS

                                //-----------
                                if (DungChung.Bien.MaBV == "26007")
                                {
                                    var qbn = _data.BenhNhans.Where(p => p.MaBNhan == _mabn).FirstOrDefault();
                                    if (qbn != null && qbn.DTuong == "BHYT")
                                    {
                                        string sthe = qbn.SThe.Trim().ToString();
                                        var Ktramant = _data.People.Where(p => p.SThe.Contains(sthe)).FirstOrDefault();
                                        if (Ktramant != null && Ktramant.GhiChu != null)
                                        {
                                            Ktramant.GhiChu = null;
                                            _data.SaveChanges();
                                        }
                                    }
                                }

                                TTxoa = true;
                                frm_Ravien_Load(sender, e);

                            }
                        }
                        else
                        {
                            MessageBox.Show("BN đã thanh toán, bạn không được phép xóa");
                        }
                        #endregion
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.ToString());
            }
        }

        private void cboKetQua_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                QLBVEntities data = new QLBVEntities(DungChung.Bien.StrCon);
                if (!string.IsNullOrEmpty(txtNgayRa.Text) && _ravien.Count <= 0)
                {
                    if ((txtNgayRa.DateTime.Date - txtNgayVao.DateTime.Date).Days < 0)
                    {
                        MessageBox.Show("Ngày ra viện phải >= ngày vào viện!");
                        txtNgayRa.Focus();
                    }
                    //else if (string.IsNullOrEmpty(cboKetQua.Text))
                    //{
                    //    MessageBox.Show("Bạn chưa chọn kết quả điều trị!, chọn kết quả để lấy số ngày điều trị!");
                    //    cboKetQua.Focus();
                    //}
                    else
                    {
                        if (_ravien.Count > 0)
                        {
                            if (txtNgayRa.DateTime != _ravien.First().NgayRa)
                            {
                                btnLuu.Enabled = true;
                            }
                            else
                                btnLuu.Enabled = false;
                        }
                        else
                        {
                            btnLuu.Enabled = true;
                            if (DungChung.Bien.MaBV == "20001" || DungChung.Bien.MaBV == "30002" || DungChung.Bien.MaBV == "24012" || DungChung.Bien.MaBV == "24009" || DungChung.Bien.MaBV == "24272")
                            {
                                songaydt = _GetSoNgayDTri(data, _mabn, txtNgayVao.DateTime, txtNgayRa.DateTime, cboKetQua.Text, radHinhThuc.SelectedIndex); //getDaysOfStay(_mabn, txtNgayVao.DateTime, txtNgayRa.DateTime);
                                txtSoNgaydt.Text = songaydt.ToString();
                            }
                        }

                    }
                }
                if (cboKetQua.SelectedIndex == 4)
                {
                    labelPPDT.Text = "Nguyên nhân tử vong:";
                }
                else
                {
                    labelPPDT.Text = " Phương pháp điều trị:";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi! " + ex.Message);
            }

        }
        void _setHinhThuc(bool ravien)
        {
            if (ravien)
            {
                labelTGRaVien.Text = "Ra viện lúc:";
            }
            else
            {

            }
        }
        public static int _GetSoNgayDTri(QLBVEntities _data, int _mabn, DateTime _NgayVao, DateTime _NgayRa, string _KetQua, int RV)
        {
            //DateTime ngayYLenhCuoi = _NgayRa;
            //if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049")
            //{
            //    QLBVEntities data = new QLBVEntities(DungChung.Bien.StrCon);
            //    var qdt0 = (from dt in data.DThuocs.Where(p => p.MaBNhan == _mabn).Where(p => p.PLDV == 1) join dtct in data.DThuoccts on dt.IDDon equals dtct.IDDon select new { NgayNhap = dtct.NgayNhap.Value, dtct.MaDV, dtct.SoLuong, dtct.MaKXuat }).OrderByDescending(p => p.NgayNhap).ToList();

            //    var qdt = (from dt in qdt0
            //               group dt by new { NgayNhap = dt.NgayNhap.Date, dt.MaDV, dt.MaKXuat } into kq
            //               from kq1 in kq.DefaultIfEmpty()
            //               select new
            //               {
            //                   kq.Key.NgayNhap,
            //                   kq.Key.MaDV,
            //                   kq.Key.MaKXuat,
            //                   SoLuong = kq.Sum(p => p.SoLuong)
            //               }).Where(p => p.SoLuong > 0).OrderByDescending(p => p.NgayNhap).FirstOrDefault();

            //    var qdv = (from dt in data.DThuocs.Where(p => p.MaBNhan == _mabn).Where(p => p.PLDV == 2) join dtct in data.DThuoccts on dt.IDDon equals dtct.IDDon select dtct).OrderByDescending(p => p.NgayNhap).FirstOrDefault();
            //    var qcd = data.CLS.Where(p => p.MaBNhan == _mabn).OrderByDescending(p => p.NgayThang).FirstOrDefault();
            //    List<DateTime> lngay = new List<DateTime>();
            //    if (qdt != null)
            //        lngay.Add(qdt.NgayNhap);
            //    if (qdv != null)
            //        lngay.Add(qdv.NgayNhap.Value);
            //    if (qcd != null)
            //        lngay.Add(qcd.NgayThang.Value);

            //    if (lngay.Count() == 0)
            //        return 0;
            //    else
            //    {
            //        ngayYLenhCuoi = lngay.OrderByDescending(p => p).First();
            //    }
            //}
            string Dtuong = "";
            if (DungChung.Bien.MaBV == "30004")
            {
                var bn = _data.BenhNhans.Where(p => p.MaBNhan == _mabn).ToList();
                if (bn.Count() > 0)
                {
                    Dtuong = bn.First().DTuong;
                }
            }
            int rs = 0;
            TimeSpan ts = _NgayRa - _NgayVao;
            int day = (_NgayRa.Date - _NgayVao.Date).Days;

            _soHdt = ts.TotalHours;
            if (ts.TotalHours >= 4)
            {//20001 tính theo cách cũ đức 17/06/2019 liễu y/c
                if (_NgayVao >= Convert.ToDateTime("15/07/2018 00:00:00") || DungChung.Bien.MaBV == "20001") // vào viện trước ngày 15/7/2018 vẫn tính theo cách cũ_ namnt
                {
                    if (DungChung.Bien.MaBV == "30004" && Dtuong == "Dịch vụ")
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
                    else
                    {
                        if ((_KetQua.ToLower().Contains("đỡ|giảm") || _KetQua.ToLower().Contains("khỏi")) && RV == 0 && DungChung.Bien.MaBV != "24272")
                        {
                            if (day == 0)
                            {
                                rs = 1;
                            }
                            else
                            {
                                if (DungChung.Bien.MaBV == "14018")
                                    rs = day + 1;
                                else if (DungChung.Bien.MaBV == "14017" && _NgayRa.Hour > 12)
                                    rs = day + 1;
                                else
                                    rs = day;
                            }

                            //if (rs > 1 && _NgayRa.Date > ngayYLenhCuoi.Date)
                            //{
                            //    rs = rs - (_NgayRa.Date - ngayYLenhCuoi.Date).Days;
                            //    if (rs < 0)
                            //        rs = 0;
                            //}
                        }
                        else
                        {
                            if (day == 1)
                            {
                                if (DungChung.Bien.MaBV == "30005")
                                {
                                    rs = day;
                                }
                                else
                                {
                                    if (ts.TotalMilliseconds <= 28800000)
                                        rs = 1;
                                    else if (ts.TotalMilliseconds > 28800000)
                                        rs = 2;
                                }

                            }
                            else if (day == 0 || day > 0)
                            {
                                if (DungChung.Bien.MaBV == "30005")
                                    rs = day;
                                else if (DungChung.Bien.MaBV == "14017" && _NgayRa.Hour < 12)
                                    rs = day;
                                else
                                    rs = day + 1;
                            }

                            //if (rs > 1 && _NgayRa.Date > ngayYLenhCuoi.Date)
                            //{
                            //    rs = rs - (_NgayRa.Date - ngayYLenhCuoi.Date).Days;
                            //    if (rs < 0)
                            //        rs = 0;
                            //}
                        }
                    }
                }
                else
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

                    //if (rs > 1 && _NgayRa.Date > ngayYLenhCuoi.Date)
                    //{
                    //    rs = rs - (_NgayRa.Date - ngayYLenhCuoi.Date).Days;
                    //    if (rs < 0)
                    //        rs = 0;
                    //}
                }
            }
            return rs;
        }
        private void radHinhThuc_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboKetQua.Text = "";
            txtSoNgaydt.Text = "";
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Căn cứ thông tư  số 28/2014/TT-BYT ngày 14/8/2014 của Bộ Y tế về chỉ tiêu số 20: " +
            "Số ngày điều trị trung bình của 1 đợt điều trị nội trú, mục 4 quy định về số ngày điều trị nội trú được tính cụ thể như sau:" +
"1. Cách tính\n" +
"- Số ngàyđiều trị nội trú = (ngày ra viện - ngày vào viện) + 1 \n" +
"- Trong trường hợp người bệnh vào viện đêm hôm trước và ra viện vào sáng hôm sau (từ 4 tiếng đến dưới 8 tiếng) chỉ được tính một ngày.\n" +
"- Trong trường hợp người bệnh chuyển khoa trong cùng một bệnh viện và cùng một ngày mỗi khoa chỉ được tính 1/2 ngày.");
        }

        private void txtNgayRa_EditValueChanged(object sender, EventArgs e)
        {
            QLBVEntities data = new QLBVEntities(DungChung.Bien.StrCon);
            if (!string.IsNullOrEmpty(txtNgayRa.Text) && _ravien.Count <= 0)
                if ((txtNgayRa.DateTime.Date - txtNgayVao.DateTime.Date).Days < 0)
                {
                    MessageBox.Show("Ngày ra viện phải >= ngày vào viện!");
                    txtNgayRa.Focus();
                }
                //else if (string.IsNullOrEmpty(cboKetQua.Text))
                //{
                //    MessageBox.Show("Bạn chưa chọn kết quả điều trị!, chọn kết quả để lấy số ngày điều trị!");
                //    cboKetQua.Focus();
                //}
                else
                {
                    if (_ravien.Count > 0)
                    {
                        if (txtNgayRa.DateTime != _ravien.First().NgayRa)
                        {
                            btnLuu.Enabled = true;
                        }
                        else
                            btnLuu.Enabled = false;
                    }
                    else
                    {
                        btnLuu.Enabled = true;
                        songaydt = _GetSoNgayDTri(data, _mabn, txtNgayVao.DateTime, txtNgayRa.DateTime, cboKetQua.Text, radHinhThuc.SelectedIndex); //getDaysOfStay(_mabn, txtNgayVao.DateTime, txtNgayRa.DateTime);
                        //if (DungChung.Bien.MaTinh == "30")
                        //{
                        //    if (songaydt > 1 && BNrv.NoiTru == 0 && BNrv.DTNT == true)
                        //        songaydt = songaydt - 1;
                        //}
                        txtSoNgaydt.Text = songaydt.ToString();
                        txt_GioDT.Text = Convert.ToString(_soHdt);
                    }

                }
        }

        private void txtSoNgaydt_EditValueChanged(object sender, EventArgs e)
        {
            if (_ravien.Count > 0)
            {
                if (txtSoNgaydt.Text != (_ravien.First().SoNgaydt != null ? _ravien.First().SoNgaydt.ToString() : "0"))
                    btnLuu.Enabled = true;
                else
                    btnLuu.Enabled = false;
            }
            else btnLuu.Enabled = true;
        }

        private void cboKetQua_EditValueChanged(object sender, EventArgs e)
        {
            txtNgayRa_EditValueChanged(null, null);
        }

        private void txtSoLT_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (_ravien.Count > 0)
                {
                    if (txtSoLT.Text != _ravien.First().SoLT)
                        btnLuu.Enabled = true;
                    else
                        btnLuu.Enabled = false;
                }
                else btnLuu.Enabled = true;
            }
            catch
            {
            }
        }

        private void txtMaYT_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (_ravien.Count > 0)
                {
                    if (txtMaYT.Text != _ravien.First().MaYTe)
                        btnLuu.Enabled = true;
                    else
                        btnLuu.Enabled = false;
                }
                else btnLuu.Enabled = true;
            }
            catch
            {

            }
        }

        private void lupKPDT_EditValueChanged(object sender, EventArgs e)
        {
            if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "56789")
            {
                var kt = _data.RaViens.Where(p => p.MaBNhan == _mabn).ToList();
                if (kt.Count == 0)
                    if (DungChung.Bien.PP_SoLT != 0)
                        GetSoLT01071();
            }
        }

        private void LupICD2_EditValueChanged(object sender, EventArgs e)
        {
            var idcd = lICD.FirstOrDefault(p => p.MaICD == LupICD2.Text);
            if (idcd != null)
            {
                txtBenhKhac2.EditValue = DungChung.Bien.MaBV == "30002" ? lICD.FirstOrDefault(p => p.MaICD == LupICD2.EditValue.ToString()).TenICD + string.Format("({0})", LupICD2.EditValue.ToString()) : lICD.FirstOrDefault(p => p.MaICD == LupICD2.EditValue.ToString()).TenICD;

            }
            else
            {
                LupICD2.Properties.Buttons[1].Visible = false;
            }

        }

        private void txtBenhKhac2_EditValueChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBenhKhac2.Text))
            {
                LupICD2.EditValue = null;
            }
        }

        private void LupICD3_EditValueChanged(object sender, EventArgs e)
        {
            var idcd = lICD.FirstOrDefault(p => p.MaICD == LupICD3.Text);
            if (idcd != null)
                txtBenhKhac3.EditValue = DungChung.Bien.MaBV == "30002" ? lICD.FirstOrDefault(p => p.MaICD == LupICD3.EditValue.ToString()).TenICD + string.Format("({0})", LupICD3.EditValue.ToString()) : lICD.Where(p => p.MaICD == LupICD3.EditValue.ToString()).Select(p => p.TenICD).FirstOrDefault();
            else
                LupICD3.Properties.Buttons[1].Visible = false;
        }

        private void LupICD4_EditValueChanged(object sender, EventArgs e)
        {
            if (DungChung.Bien.MaBV == "14017")
            {
                var idcd = lICD.FirstOrDefault(p => p.MaICD == LupICD4.Text);
                if (idcd != null)
                {
                    txtBenhKhac4.EditValue = DungChung.Bien.MaBV == "30002" ? lICD.FirstOrDefault(p => p.MaICD == LupICD4.EditValue.ToString()).TenICD + string.Format("({0})", LupICD4.EditValue.ToString()) : lICD.FirstOrDefault(p => p.MaICD == LupICD4.EditValue.ToString()).TenICD;
                }
                else
                {
                    LupICD4.Properties.Buttons[1].Visible = false;
                }
            }
            else
            {
                var idcd = lICD.FirstOrDefault(p => p.MaICD == LupICD4.Text);
                if (idcd != null)
                {
                    txtBenhKhac4.EditValue = DungChung.Bien.MaBV == "30002" ? lICD.FirstOrDefault(p => p.MaICD == LupICD4.EditValue.ToString()).TenICD + string.Format("({0})", LupICD4.EditValue.ToString()) : lICD.FirstOrDefault(p => p.MaICD == LupICD4.EditValue.ToString()).TenICD;
                }
                else
                {
                    LupICD4.Properties.Buttons[1].Visible = false;
                }
            }
        }

        private void frm_Ravien_FormClosing(object sender, FormClosingEventArgs e)
        {
            _data = new QLBVEntities(DungChung.Bien.StrCon);
            var kt = _data.RaViens.Where(p => p.MaBNhan == _mabn).Where(p => p.Status == 2).ToList();
            if (kt.Count <= 0)
            {
                var update = _data.BNKBs.Where(p => p.MaBNhan == _mabn && p.PhuongAn == 0).ToList();
                foreach (var a in update)
                {
                    a.PhuongAn = 4;
                    a.NgayNghi = null;
                    a.MaKPdt = 0;
                    _data.SaveChanges();
                }
            }
        }

        private void txtMaICD_EditValueChanged(object sender, EventArgs e)
        {
            var idcd = lICD.FirstOrDefault(p => p.MaICD == txtMaICD.Text);
            if (idcd != null)
                txtChanDoan.EditValue = DungChung.Bien.MaBV == "30002" ? lICD.FirstOrDefault(p => p.MaICD == txtMaICD.EditValue.ToString()).TenICD + string.Format("({0})", txtMaICD.EditValue.ToString()) : lICD.Where(p => p.MaICD == txtMaICD.EditValue.ToString()).Select(p => p.TenICD).FirstOrDefault();
            else
                txtMaICD.Properties.Buttons[1].Visible = false;

        }

        private void btnHenKham_Click(object sender, EventArgs e)
        {
            try
            {
                int rs;
                int _int_maBN = 0;
                if (Int32.TryParse(txtMaBNhan.Text, out rs))
                    _int_maBN = Convert.ToInt32(txtMaBNhan.Text);
                if (idKb > 0)
                {
                    QLBVEntities _data = new QLBVEntities(DungChung.Bien.StrCon);
                    int id = idKb;
                    var idkb = _data.BNKBs.Where(p => p.MaBNhan == _int_maBN).OrderByDescending(p => p.IDKB).ToList();
                    if (idkb.Count > 0)
                    {
                        if (id >= idkb.First().IDKB)
                        {
                            FormThamSo.frm_HenKham frm = new FormThamSo.frm_HenKham(id);
                            frm.ShowDialog();
                        }
                        else
                        {
                            MessageBox.Show("Chỉ Khoa khám bệnh cuối cùng của BN mới có quyền hẹn tái khám");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DungChung.WriteLog.Warn(ex);
            }
        }

        private void txtMaICD_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                txtMaICD.EditValue = null;
                txtChanDoan.EditValue = null;
            }
        }

        private void LupICD2_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                LupICD2.EditValue = null;
                txtBenhKhac2.Text = null;
            }
        }

        private void LupICD3_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                LupICD3.EditValue = null;
                txtBenhKhac3.Text = null;
            }
        }

        private void LupICD4_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                LupICD4.EditValue = null;
                txtBenhKhac4.Text = null;
            }
        }

        private void txtChanDoan_EditValueChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtChanDoan.Text))
            {
                txtMaICD.EditValue = null;
            }
        }

        private void txtBenhKhac3_EditValueChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBenhKhac3.Text))
            {
                LupICD3.EditValue = null;
            }
        }

        private void txtBenhKhac4_EditValueChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBenhKhac4.Text))
            {
                LupICD4.EditValue = null;
            }
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

        public void getICDKhac(string _maicd)
        {
            lupKhac.EditValue = _maicd;

            //if (DungChung.Bien.MaBV != "30007" && DungChung.Bien.MaBV != "30340")
            //{
            string tenICD = lICD.Where(p => p.MaICD == _maicd).Select(p => p.TenICD).FirstOrDefault();
            if (string.IsNullOrEmpty(txtBenhKhac1.Text) && tenICD != null)
            {
                txtBenhKhac1.Text = tenICD;

            }
        }

        private void LupICD2_Leave(object sender, EventArgs e)
        {
            if (LupICD2.Text != null && !string.IsNullOrWhiteSpace(LupICD2.Text))
            {
                LupICD2.Properties.Buttons[1].Visible = true;
                txtBenhKhac2.EditValue = DungChung.Bien.MaBV == "30002" ? lICD.FirstOrDefault(p => p.MaICD == LupICD2.EditValue.ToString()).TenICD + string.Format("({0})", LupICD2.EditValue.ToString()) : lICD.FirstOrDefault(p => p.MaICD == LupICD2.EditValue.ToString()).TenICD;
            }
            else
            {

                txtBenhKhac2.EditValue = null;
            }
        }

        private void LupICD3_Leave(object sender, EventArgs e)
        {
            if (LupICD3.Text != null && !string.IsNullOrWhiteSpace(LupICD3.Text))
            {
                LupICD3.Properties.Buttons[1].Visible = true;
                txtBenhKhac3.EditValue = DungChung.Bien.MaBV == "30002" ? lICD.FirstOrDefault(p => p.MaICD == LupICD3.EditValue.ToString()).TenICD + string.Format("({0})", LupICD3.EditValue.ToString()) : lICD.FirstOrDefault(p => p.MaICD == LupICD3.EditValue.ToString()).TenICD;
            }
            else
            {

                txtBenhKhac3.EditValue = null;
            }
        }

        private void LupICD4_Leave(object sender, EventArgs e)
        {
            if (LupICD4.EditValue != null && !string.IsNullOrWhiteSpace(LupICD4.EditValue.ToString()))
            {
                LupICD4.Properties.Buttons[1].Visible = true;
                if (lICD.FirstOrDefault(p => p.MaICD == LupICD4.EditValue.ToString()) != null)
                    txtBenhKhac4.EditValue = DungChung.Bien.MaBV == "30002" ? lICD.FirstOrDefault(p => p.MaICD == LupICD4.EditValue.ToString()).TenICD + string.Format("({0})", LupICD4.EditValue.ToString()) : lICD.FirstOrDefault(p => p.MaICD == LupICD4.EditValue.ToString()).TenICD;
            }
            else
            {
                txtBenhKhac4.EditValue = null;
            }

        }

        private void txtMaICD_Leave(object sender, EventArgs e)
        {
            if (txtMaICD.EditValue != null && !string.IsNullOrWhiteSpace(txtMaICD.EditValue.ToString()))
            {
                txtMaICD.Properties.Buttons[1].Visible = true;
                txtChanDoan.EditValue = DungChung.Bien.MaBV == "30002" ? lICD.FirstOrDefault(p => p.MaICD == txtMaICD.EditValue.ToString()).TenICD + string.Format("({0})", txtMaICD.EditValue.ToString()) : lICD.Where(p => p.MaICD == txtMaICD.EditValue.ToString()).Select(p => p.TenICD).FirstOrDefault();
            }
            else
            {

                txtChanDoan.EditValue = null;
            }
        }

        private void lupKhac_EditValueChanged(object sender, EventArgs e)
        {
            btnLuu.Enabled = true;

        }

        private void lupKhac_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.F9)
            {
                FormThamSo.frm_TimKiem frm = new FormThamSo.frm_TimKiem();
                frm.GetData = new FormThamSo.frm_TimKiem._getstring(getICDKhac);
                frm.ShowDialog();

            }
        }

        private void txtBenhKhac1_EditValueChanged(object sender, EventArgs e)
        {
            btnLuu.Enabled = true;
        }

        private void txtBenhKhac44_EditValueChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBenhKhac44.Text))
            {
                LupICD44.EditValue = null;
            }
        }

        private void txtBenhKhac55_EditValueChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBenhKhac55.Text))
            {
                LupICD55.EditValue = null;
            }
        }

        private void LupICD44_EditValueChanged(object sender, EventArgs e)
        {
            var idcd = lICD.FirstOrDefault(p => p.MaICD == LupICD44.Text);
            if (idcd != null)
                txtBenhKhac44.EditValue = lICD.Where(p => p.MaICD == LupICD44.EditValue.ToString()).Select(p => p.TenICD).FirstOrDefault();
            else
                LupICD44.Properties.Buttons[1].Visible = false;
        }

        private void LupICD55_EditValueChanged(object sender, EventArgs e)
        {
            var idcd = lICD.FirstOrDefault(p => p.MaICD == LupICD55.Text);
            if (idcd != null)
                txtBenhKhac55.EditValue = lICD.Where(p => p.MaICD == LupICD55.EditValue.ToString()).Select(p => p.TenICD).FirstOrDefault();
            else
                LupICD55.Properties.Buttons[1].Visible = false;
        }

        private void LupICD44_Leave(object sender, EventArgs e)
        {
            if (LupICD44.Text != null && !string.IsNullOrWhiteSpace(LupICD44.Text))
            {
                LupICD44.Properties.Buttons[1].Visible = true;
                txtBenhKhac44.EditValue = lICD.FirstOrDefault(p => p.MaICD == LupICD44.EditValue.ToString()).TenICD;
            }
            else
            {

                txtBenhKhac44.EditValue = null;
            }
        }

        private void LupICD55_Leave(object sender, EventArgs e)
        {
            if (LupICD55.Text != null && !string.IsNullOrWhiteSpace(LupICD55.Text))
            {
                LupICD55.Properties.Buttons[1].Visible = true;
                txtBenhKhac55.EditValue = lICD.FirstOrDefault(p => p.MaICD == LupICD55.EditValue.ToString()).TenICD;
            }
            else
            {

                txtBenhKhac55.EditValue = null;
            }
        }

        private void lookUpEdit1_Properties_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                LupICD44.EditValue = null;
                txtBenhKhac44.Text = null;
            }
        }

        private void lookUpEdit2_Properties_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                LupICD3.EditValue = null;
                txtBenhKhac3.Text = null;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
    }
}