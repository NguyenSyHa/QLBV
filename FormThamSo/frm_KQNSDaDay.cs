using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.IO;

namespace QLBV.FormThamSo
{
    public partial class frm_KQNSDaDay : DevExpress.XtraEditors.XtraForm
    {
        int _idcls = 0, _makp = 0;
        public frm_KQNSDaDay(int idcls, int makp)
        {
            InitializeComponent();
            _idcls = idcls;
            _makp = makp;
        }
        int trangthaiLuu = 0, _mabn = 0, _idcd = 0;
        string[] arrDuongDan = new string[7];
        QLBV_Database.QLBVEntities _Data;
        private void loadBSTH(int _maKP,QLBV_Database.QLBVEntities data)
        {
            string _makp = ";" + _maKP.ToString() + ";";
            var c = (from cb in data.CanBoes.Where(p => p.Status == 1).Where(p => p.MaKPsd.Contains(_makp)).Where(p => p.CapBac.ToLower().Contains("bs") || p.CapBac.ToLower().Contains("bác sĩ") || p.CapBac.ToLower().Contains("bác sỹ") || p.CapBac.ToLower().Contains("ys") || p.CapBac.ToLower().Contains("y sĩ") || p.CapBac.ToLower().Contains("y sỹ") || p.CapBac.ToLower().Contains("cn") || p.CapBac.ToLower().Contains("ktv") || p.CapBac.ToLower().Contains("kỹ thuật viên") || p.CapBac.ToLower().Contains("giáo sư") || p.CapBac.ToLower().Contains("gs") || p.CapBac.ToLower().Contains("tiến sĩ") || p.CapBac.ToLower().Contains("ts"))
                     select new
                     {
                         cb.MaCB,
                         cb.TenCB,
                         cb.MaKPsd
                     }).ToList();
            //if(DungChung.Bien.PLoaiKP=DungChung.Bien.st_PhanLoaiKP.Admin)
            LupCanBo.Properties.DataSource = c.ToList();
        }
        void getMaMay(int makp)
        {
            var madv = (from ts in _lTaiSan.Where(p => p.MaKP == makp) select new { ts.MaDV }).ToList();
            var mamay = (from m in madv join dv in _ldvu on m.MaDV equals dv.MaDV select new { dv.MaQD, dv.TenDV }).ToList();
            lupMaMay.Properties.DataSource = null;
            if (mamay.Count > 0)
            {
                lupMaMay.Properties.DataSource = mamay;
                lupMaMay.EditValue = mamay.First().MaQD;
            }
            //if(mamay)

        }
        List<TaiSan> _lTaiSan = new List<TaiSan>();
        List<DichVu> _ldvu = new List<DichVu>();
        string Duongdandasua = "";
        bool _tamthu = true;
        private void loadAnhNoiSoi()
        {
            ptNoisoi1.Image = null;
            ptNoisoi2.Image = null;
            ptNoisoi3.Image = null;
            ptNoisoi4.Image = null;
            ptNoisoi5.Image = null;
            ptNoisoi6.Image = null;
        }
        private void frm_KQNSDaDay_Load(object sender, EventArgs e)
        {
            _Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            _ldvu = _Data.DichVus.ToList();
            _lTaiSan = _Data.TaiSans.ToList();
            loadBSTH(_makp, _Data);
            getMaMay(_makp);
            lupNgayTH.DateTime = System.DateTime.Now;
            var _cls = (from cls in _Data.CLS.Where(p => p.IdCLS == _idcls)
                        join cd in _Data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                        join clsct in _Data.CLScts on cd.IDCD equals clsct.IDCD
                        select new { cls, cd, clsct }).ToList();
            if (_cls.Count() > 0)
            {
                _mabn = _cls.First().cls.MaBNhan ?? 0;
                _idcd = _cls.First().cd.IDCD;
                if (_cls.First().cd.Status == 0)//chưa làm
                {
                    mmKLNoisoi.Text = "";
                    mmKLNoisoi.Text = "";
                    mmLoidanNoisoi.Text = "";
                    mmKQNoisoi.Properties.ReadOnly = false;
                    mmKLNoisoi.Properties.ReadOnly = false;
                    mmLoidanNoisoi.Properties.ReadOnly = false;
                    loadAnhNoiSoi();
                    enablecontrol(false);
                }
                else
                {
                    enablecontrol(true);
                    if (_cls.First().clsct.KetQua != null)
                    {
                        mmKQNoisoi.Text = _cls.First().clsct.KetQua;
                    }
                    else
                        mmKQNoisoi.Text = "";
                    if (_cls.First().cd.KetLuan != null)
                    {
                        mmKLNoisoi.Text = _cls.First().cd.KetLuan;
                    }
                    else
                        mmKLNoisoi.Text = "";
                    if (_cls.First().cd.LoiDan != null)
                    {
                        mmLoidanNoisoi.Text = _cls.First().cd.LoiDan;
                    }
                    else
                        mmLoidanNoisoi.Text = "";
                    if (_cls.First().cls.MaCBth != null)
                    {
                        LupCanBo.EditValue = _cls.First().cls.MaCBth;
                    }
                    if (_cls.First().cls.NgayTH != null)
                    {
                        lupNgayTH.EditValue = _cls.First().cls.NgayTH;
                    }
                    if (_cls.First().clsct.DuongDan2 != null)
                    {
                        String strDD = _cls.First().clsct.DuongDan2;
                        Duongdandasua = strDD;
                        string[] arrDD = QLBV_Library.QLBV_Ham.LayChuoi('|', strDD);
                        for (int i = 0; i < arrDD.Length; i++)
                        {
                            switch (i)
                            {
                                case 0:
                                    if (string.IsNullOrEmpty(arrDD[i]) && arrDD[i].Length <= 3)
                                    {
                                        ptNoisoi1.Image = null;
                                    }
                                    else
                                    {
                                        if (File.Exists(arrDD[i]))
                                            ptNoisoi1.Image = Image.FromFile(arrDD[i]);
                                        else
                                            ptNoisoi1.Image = null;
                                    }
                                    break;
                                case 1:
                                    if (string.IsNullOrEmpty(arrDD[i]))
                                    {
                                        ptNoisoi2.Image = null;
                                    }
                                    else
                                    {
                                        if (File.Exists(arrDD[i]))
                                            ptNoisoi2.Image = Image.FromFile(arrDD[i]);
                                        else
                                            ptNoisoi2.Image = null;
                                    }
                                    break;
                                case 2:
                                    if (string.IsNullOrEmpty(arrDD[i]))
                                    {
                                        ptNoisoi3.Image = null;
                                    }
                                    else
                                    {
                                        if (File.Exists(arrDD[i]))
                                            ptNoisoi3.Image = Image.FromFile(arrDD[i]);
                                        else
                                            ptNoisoi3.Image = null;
                                    }
                                    break;
                                case 3:
                                    if (string.IsNullOrEmpty(arrDD[i]))
                                    {
                                        ptNoisoi4.Image = null;
                                    }
                                    else
                                    {
                                        if (File.Exists(arrDD[i]))
                                            ptNoisoi4.Image = Image.FromFile(arrDD[i]);
                                        else
                                            ptNoisoi4.Image = null;
                                    }
                                    break;
                                case 4:
                                    if (string.IsNullOrEmpty(arrDD[i]))
                                    {
                                        ptNoisoi5.Image = null;
                                    }
                                    else
                                    {
                                        if (File.Exists(arrDD[i]))
                                            ptNoisoi5.Image = Image.FromFile(arrDD[i]);
                                        else
                                            ptNoisoi5.Image = null;
                                    }
                                    break;
                                case 5:
                                    if (string.IsNullOrEmpty(arrDD[i]))
                                    {
                                        ptNoisoi6.Image = null;
                                    }
                                    else
                                    {
                                        if (File.Exists(arrDD[i]))
                                            ptNoisoi6.Image = Image.FromFile(arrDD[i]);
                                        else
                                            ptNoisoi6.Image = null;
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    mmKQNoisoi.Properties.ReadOnly = true;
                    mmKLNoisoi.Properties.ReadOnly = true;
                    mmLoidanNoisoi.Properties.ReadOnly = true;
                    btnSua.Enabled = true;
                    if (_tamthu == false)
                        btnSua.Enabled = false;
                    
                }
            }
        }
        public string layTenFileAnh(string fileAnhTMH, string tenfileanh)
        {
            if (!string.IsNullOrEmpty(fileAnhTMH))
            {
                if (!File.Exists(tenfileanh))
                {
                    File.Copy(fileAnhTMH, tenfileanh);

                }
                else
                {
                    for (int i = 0; i < 100; i++)
                    {
                        string a = "";
                        string ex = Path.GetExtension(tenfileanh);
                        a = tenfileanh.Replace(ex, i + ex);
                        //if(DungChung.Bien.MaBV== "30012")
                        //    a = tenfileanh.Replace(".bmp", i + ".bmp");
                        //else
                        //    a = tenfileanh.Replace(".jpg", i + ".jpg");
                        if (!File.Exists(a))
                        {
                            File.Copy(fileAnhTMH, a);
                            tenfileanh = a;
                            break;
                        }
                    }
                }
            }
            return tenfileanh;
        }
        private void chonAnhNoiSoi(PictureEdit pt, int i)
        {
            bool tontai = true;
            switch (i)
            {
                case 1:
                    if (ptNoisoi1.Image == null)
                        tontai = false;
                    break;
                case 2:
                    if (ptNoisoi2.Image == null)
                        tontai = false;
                    break;
                case 3:
                    if (ptNoisoi3.Image == null)
                        tontai = false;
                    break;
                case 4:
                    if (ptNoisoi4.Image == null)
                        tontai = false;
                    break;
                case 5:
                    if (ptNoisoi5.Image == null)
                        tontai = false;
                    break;
                case 6:
                    if (ptNoisoi6.Image == null)
                        tontai = false;
                    break;
                default:
                    break;
            }
            if (tontai)
            {
                DialogResult _dresult = MessageBox.Show("Bạn có muốn chọn lại ảnh!", "Hỏi lưu", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (_dresult == DialogResult.Yes)
                {
                    string fileName = string.Empty;
                    OpenFileDialog op = new OpenFileDialog();
                    op.Multiselect = false;
                    op.Filter = "JPEG (*.jpg)|*.jpg|BMP(*.bmp)| *.bmp";
                    if (trangthaiLuu == 0)//Nếu là thêm mới ảnh.
                    {
                        if (op.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            fileName = op.FileName;
                            string ex = Path.GetExtension(op.FileName);
                            if (!string.IsNullOrEmpty(fileName))
                                pt.Image = Image.FromFile(fileName);
                            else
                                pt.Image = null;
                            string _tenfileanh = DungChung.Bien.DuongDan;
                            // _tenfileanh += _mabn + "_" + IdCLS + "_" + i + ".jpg";
                            _tenfileanh += _mabn + "_" + _idcls + "_" + i + ex;
                            arrDuongDan[i - 1] = layTenFileAnh(fileName, _tenfileanh);
                        }
                    }
                    if (trangthaiLuu == 1) // Nếu là sửa ảnh
                    {
                        if (op.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            fileName = op.FileName;
                            string ex = Path.GetExtension(op.FileName);
                            if (!string.IsNullOrEmpty(fileName))
                                pt.Image = Image.FromFile(fileName);
                            else
                                pt.Image = null;

                            string _tenfileanh = DungChung.Bien.DuongDan;
                            // _tenfileanh += _mabn + "_" + IdCLS + "_" + i + ".jpg";
                            _tenfileanh += _mabn + "_" + _idcls + "_" + i + ex;
                            arrDuongDan[i - 1] = layTenFileAnh(fileName, _tenfileanh);
                        }
                    }
                }
            }
            else
            {
                string fileName = string.Empty;
                OpenFileDialog op = new OpenFileDialog();
                op.Multiselect = false;
                op.Filter = "JPEG (*.jpg)|*.jpg|BMP(*.bmp)| *.bmp";
                if (trangthaiLuu == 0)//Nếu là thêm mới ảnh.
                {
                    if (op.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        fileName = op.FileName;
                        string ex = Path.GetExtension(op.FileName);
                        if (!string.IsNullOrEmpty(fileName))
                            pt.Image = Image.FromFile(fileName);
                        else
                            pt.Image = null;
                        string _tenfileanh = DungChung.Bien.DuongDan;
                        //  _tenfileanh += _mabn + "_" + IdCLS + "_" + i + ".jpg";
                        _tenfileanh += _mabn + "_" + _idcls + "_" + i + ex;
                        arrDuongDan[i - 1] = layTenFileAnh(fileName, _tenfileanh);
                    }
                }
                if (trangthaiLuu == 1) // Nếu là sửa ảnh
                {
                    if (op.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        fileName = op.FileName;
                        string ex = Path.GetExtension(op.FileName);
                        if (!string.IsNullOrEmpty(fileName))
                            pt.Image = Image.FromFile(fileName);
                        else
                            pt.Image = null;

                        string _tenfileanh = DungChung.Bien.DuongDan;
                        // _tenfileanh += _mabn + "_" + IdCLS + "_" + i + ".jpg";
                        _tenfileanh += _mabn + "_" + _idcls + "_" + i + ex;
                        arrDuongDan[i - 1] = layTenFileAnh(fileName, _tenfileanh);
                    }
                }
            }
        }
        private void xoaAnh(PictureEdit pt, int i)
        {
            i = i - 1;
            if (trangthaiLuu == 0)
            {
                pt.Image = null;
            }
            if (trangthaiLuu == 1)
            {
                arrDuongDan[i] = "";
                pt.Image = null;
            }

        }

        private void btnChonAnhNS1_Click(object sender, EventArgs e)
        {
            chonAnhNoiSoi(ptNoisoi1, 1);
        }

        private void simpleButton9_Click(object sender, EventArgs e)
        {
            xoaAnh(ptNoisoi6, 6);
        }

        private void btnChonAnhNS2_Click(object sender, EventArgs e)
        {
            chonAnhNoiSoi(ptNoisoi2, 2);
        }

        private void btnChonAnhNS3_Click(object sender, EventArgs e)
        {
            chonAnhNoiSoi(ptNoisoi3, 3);
        }

        private void btnChonAnhNS4_Click(object sender, EventArgs e)
        {
            chonAnhNoiSoi(ptNoisoi4, 4);
        }

        private void btnChonAnhNS5_Click(object sender, EventArgs e)
        {
            chonAnhNoiSoi(ptNoisoi5, 5);
        }

        private void btnChonAnhNS6_Click(object sender, EventArgs e)
        {
            chonAnhNoiSoi(ptNoisoi6, 6);
        }

        private void btnXoaAnhNS2_Click(object sender, EventArgs e)
        {
            xoaAnh(ptNoisoi2, 2);
        }

        private void btnXoaAnhNS1_Click(object sender, EventArgs e)
        {
            xoaAnh(ptNoisoi1, 1);
        }

        private void btnXoaAnhNS3_Click(object sender, EventArgs e)
        {
            xoaAnh(ptNoisoi3, 3);
        }

        private void btnXoaAnhNS4_Click(object sender, EventArgs e)
        {
            xoaAnh(ptNoisoi4, 4);
        }

        private void btnXoaAnhNS5_Click(object sender, EventArgs e)
        {
            xoaAnh(ptNoisoi5, 5);
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            _Data=new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            bool luu = true;
            if (DungChung.Bien.MaBV != "24009" && (string.IsNullOrEmpty(mmKQNoisoi.Text) || string.IsNullOrEmpty(mmKLNoisoi.Text) || LupCanBo.EditValue == null))
            {
                DialogResult _dresult4 = MessageBox.Show("Bạn chưa nhập đầy đủ kết quả, bạn có muốn lưu dữ liệu không?", "Hỏi lưu", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (_dresult4 == DialogResult.Yes)
                {
                    luu = true;
                }
                else
                {
                    luu = false;
                }
            }
            bool KtraBNKSK = false;
            if (luu)
            {
                CL suacls = _Data.CLS.Where(p => p.IdCLS == _idcls).FirstOrDefault();
                ChiDinh cdsua = _Data.ChiDinhs.Where(p => p.IDCD == _idcd).FirstOrDefault();
                CLSct clsctsua = _Data.CLScts.Where(p => p.IDCD == _idcd).FirstOrDefault();
                cdsua.KetLuan = mmKLNoisoi.Text;
                cdsua.LoiDan = mmLoidanNoisoi.Text;
                cdsua.TamThu = 1;
                cdsua.Status = 1;
                cdsua.NgoaiGioHC = Convert.ToInt32(FormThamSo.frm_UpdateLetet.CheckNGioHC(lupNgayTH.DateTime));
                if (lupMaMay.EditValue != null)
                    cdsua.MaMay = lupMaMay.EditValue.ToString();

                if (LupCanBo.EditValue != null)
                    suacls.MaCBth = LupCanBo.EditValue.ToString();
                else
                    suacls.MaCBth = "";
                suacls.NgayTH = lupNgayTH.DateTime;
                suacls.Status = 1;

                clsctsua.KetQua = mmKQNoisoi.Text;
                clsctsua.DuongDan2 = QLBV_Library.QLBV_Ham.LuuChuoi('|', arrDuongDan);
                clsctsua.Status = 1;
                var ktstatuscd = _Data.ChiDinhs.Where(p => p.IdCLS == _idcls).Where(p => p.Status == 0 || p.Status == null).ToList();
                if (ktstatuscd.Count() == 0)
                {
                    BenhNhan sua = _Data.BenhNhans.Where(p => p.MaBNhan == _mabn).FirstOrDefault();
                    if (sua != null)
                    {
                        var b = _Data.BNKBs.Where(p => p.MaBNhan == _mabn).ToList();
                        var vienphi = _Data.VienPhis.Where(p => p.MaBNhan == _mabn).ToList();
                        if (b.Count > 0 && vienphi.Count <= 0)
                        {
                            sua.Status = 5;
                        }
                        if (sua.IDDTBN == 3 && (DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "56789" || DungChung.Bien.MaBV == "24297"))
                            KtraBNKSK = true;
                    }
                }
                _Data.SaveChanges();

                int iddthuoc = 0;
                int _idkb = 0;
                var bnkb = _Data.BNKBs.Where(p => p.MaBNhan == _mabn && p.MaKP == _makp).OrderByDescending(p => p.IDKB).ToList();
                if (bnkb.Count > 0)
                    _idkb = bnkb.First().IDKB;// so sánh ngày khám bệnh và ngày thực hiện, để lấy đúng IDKB
                var ktdthuoc = _Data.DThuocs.Where(p => p.MaBNhan == _mabn).Where(p => p.PLDV == 2).ToList();
                if (ktdthuoc.Count > 0)
                    iddthuoc = ktdthuoc.First().IDDon;
                List<int> dsIDGOiDV = new List<int>();//lấy danh sách những gói đã được thu thẳng trước đó
                if (KtraBNKSK == true)
                {
                    var _lThuTT = _Data.TamUngs.Where(p => p.IDGoiDV != null && p.PhanLoai == 3 && p.MaBNhan == _mabn).Select(p => p.IDGoiDV ?? 0).ToList();
                    dsIDGOiDV.AddRange(_lThuTT);
                }
                if (iddthuoc > 0)
                {
                    var kt = (from dt in _Data.DThuoccts.Where(p => p.IDCD == cdsua.IDCD) select dt).ToList();
                    if (kt.Count <= 0)
                    {
                        double _dongia = DungChung.Ham._getGiaDM(_Data, cdsua.MaDV == null ? 0 : cdsua.MaDV.Value, cdsua.TrongBH == null ? 1 : cdsua.TrongBH.Value, _mabn, lupNgayTH.DateTime);
                        DThuocct moi = new DThuocct();
                        moi.MaDV = cdsua.MaDV;
                        moi.IDKB = _idkb;
                        moi.IDDon = iddthuoc;
                        moi.DonVi = DungChung.Ham._getDonVi(_Data, cdsua.MaDV ?? 0);
                        moi.TrongBH = cdsua.TrongBH == null ? 0 : cdsua.TrongBH.Value;
                        moi.IDCD = cdsua.IDCD;
                        moi.DonGia = cdsua.DonGia == 0 ? _dongia : cdsua.DonGia;
                        moi.XHH = cdsua.XHH;
                        moi.LoaiDV = cdsua.LoaiDV;
                        if (LupCanBo.EditValue != null)
                            moi.MaCB = LupCanBo.EditValue.ToString();
                        else
                            moi.MaCB = "";
                        moi.MaKP = suacls.MaKP == null ? 0 : suacls.MaKP.Value;
                        moi.ThanhTien = cdsua.DonGia == 0 ? _dongia : cdsua.DonGia;
                        moi.NgayNhap = lupNgayTH.DateTime;
                        moi.SoLuong = 1;
                        moi.Status = 0;
                        if (KtraBNKSK == true && cdsua.IDGoi != null && dsIDGOiDV.Where(p => p == cdsua.IDGoi).Count() > 0)
                        {
                            moi.ThanhToan = 1;
                        }
                        else if (cdsua.SoPhieu != null && cdsua.SoPhieu > 0)
                            moi.ThanhToan = 1;
                        moi.TyLeTT = 100;
                        moi.IDCLS = _idcls;
                        _Data.DThuoccts.Add(moi);
                        _Data.SaveChanges();
                        var CheckGiaPhuThu = _Data.DichVus.Where(p => p.MaDV == cdsua.MaDV).FirstOrDefault();
                        var sss = _Data.BenhNhans.Where(p => p.MaBNhan == _mabn).Where(p => p.DTuong == "BHYT").ToList();
                        if (CheckGiaPhuThu != null && CheckGiaPhuThu.GiaPhuThu > 0 && sss.Count > 0 && moi.TrongBH == 1)
                        {
                            double s = CheckGiaPhuThu.GiaPhuThu;
                            DungChung.Ham._InsertPhuThu(_Data, moi.IDDonct, s);
                        }
                    }
                    else
                    {
                        foreach (var dt in kt)
                        {
                            dt.NgayNhap = lupNgayTH.DateTime;
                            dt.IDCLS = _idcls;
                        }
                        _Data.SaveChanges();
                    }
                }
                else
                {
                    DThuoc dthuoccd = new DThuoc();
                    dthuoccd.NgayKe = lupNgayTH.DateTime; // cần phải lấy theo ngày tháng nhập kết quả CLS
                    dthuoccd.MaBNhan = _mabn;
                    dthuoccd.MaKP = suacls.MaKP == null ? 0 : suacls.MaKP.Value;
                    dthuoccd.MaCB = suacls.MaCB;
                    dthuoccd.PLDV = 2;
                    dthuoccd.KieuDon = -1;
                    _Data.DThuocs.Add(dthuoccd);
                    if (_Data.SaveChanges() >= 0)
                    {
                        int maxid = dthuoccd.IDDon;
                        double _dongia = DungChung.Ham._getGiaDM(_Data, cdsua.MaDV == null ? 0 : cdsua.MaDV.Value, cdsua.TrongBH == null ? 1 : cdsua.TrongBH.Value, _mabn, lupNgayTH.DateTime);
                        DThuocct moi = new DThuocct();
                        moi.MaDV = cdsua.MaDV;
                        moi.IDDon = maxid;
                        moi.IDKB = _idkb;
                        moi.TrongBH = cdsua.TrongBH == null ? 0 : cdsua.TrongBH.Value;
                        if (LupCanBo.EditValue != null)
                            moi.MaCB = LupCanBo.EditValue.ToString();
                        else
                            moi.MaCB = "";
                        moi.NgayNhap = lupNgayTH.DateTime;
                        moi.MaKP = suacls.MaKP == null ? 0 : suacls.MaKP.Value;
                        moi.IDCD = cdsua.IDCD;
                        moi.DonVi = DungChung.Ham._getDonVi(_Data, cdsua.MaDV ?? 0);
                        moi.XHH = cdsua.XHH;
                        moi.DonGia = cdsua.DonGia == 0 ? _dongia : cdsua.DonGia;
                        moi.ThanhTien = cdsua.DonGia == 0 ? _dongia : cdsua.DonGia;
                        moi.SoLuong = 1;
                        moi.Status = 0;
                        moi.LoaiDV = cdsua.LoaiDV;
                        if (KtraBNKSK == true && cdsua.IDGoi != null && dsIDGOiDV.Where(p => p == cdsua.IDGoi).Count() > 0)
                        {
                            moi.ThanhToan = 1;
                        }
                        else if (cdsua.SoPhieu != null && cdsua.SoPhieu > 0)
                            moi.ThanhToan = 1;
                        moi.TyLeTT = 100;
                        moi.IDCLS = _idcls;
                        _Data.DThuoccts.Add(moi);
                        _Data.SaveChanges();
                        var CheckGiaPhuThu = _Data.DichVus.Where(p => p.MaDV == cdsua.MaDV).FirstOrDefault();
                        var sss = _Data.BenhNhans.Where(p => p.MaBNhan == _mabn).Where(p => p.DTuong == "BHYT").ToList();
                        if (CheckGiaPhuThu != null && CheckGiaPhuThu.GiaPhuThu > 0 && sss.Count > 0 && moi.TrongBH == 1)
                        {
                            double s = CheckGiaPhuThu.GiaPhuThu;
                            DungChung.Ham._InsertPhuThu(_Data, moi.IDDonct, s);
                        }
                    }
                }
                enablecontrol(true);
                mmKLNoisoi.Properties.ReadOnly = true;
                mmKQNoisoi.Properties.ReadOnly = true;
                mmLoidanNoisoi.Properties.ReadOnly = true;
            }
            //    foreach (var item in _Chidinh)
            //    {
            //        item.KetLuan = mmKLNoisoi.Text;
            //        item.LoiDan = mmLoidanNoisoi.Text;
            //        item.TamThu = 1;
            //        item.Status = 1;
            //        item.NgoaiGioHC = Convert.ToInt32(FormThamSo.frm_UpdateLetet.CheckNGioHC(lupNgayTH.DateTime));
            //    }
            //    if (LupCanBo.EditValue != null)
            //        _Cls.First().MaCBth = LupCanBo.EditValue.ToString();
            //    else
            //        _Cls.First().MaCBth = "";
            //    foreach (var a in _CLSct)
            //    {
            //        a.KetQua = mmKQNoisoi.Text;
            //        a.DuongDan2 = QLBV_Library.QLBV_Ham.LuuChuoi('|', arrDuongDan);
            //    }
            //}
        }
        private void enablecontrol(bool T)
        {
            btnLuu.Enabled = !T;
            btnSua.Enabled = T;
            btnXoa.Enabled = T;
            btnChonAnhNS1.Enabled = !T; 
            btnChonAnhNS2.Enabled = !T;
            btnChonAnhNS3.Enabled = !T;
            btnChonAnhNS4.Enabled = !T;
            btnChonAnhNS5.Enabled = !T;
            btnChonAnhNS6.Enabled = !T;
            btnXoaAnhNS1.Enabled = !T;
            btnXoaAnhNS2.Enabled = !T;
            btnXoaAnhNS3.Enabled = !T;
            btnXoaAnhNS4.Enabled = !T;
            btnXoaAnhNS5.Enabled = !T;
            btnXoaAnhNS6.Enabled = !T;
        }
        private void btnSua_Click(object sender, EventArgs e)
        {
            _Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
             trangthaiLuu = 1;
             var clsct = _Data.CLScts.Where(p => p.IDCD == _idcd).FirstOrDefault();
             if (clsct != null && clsct.DuongDan2 != null)
             {
                 string strDD = clsct.DuongDan2.ToString(); // Lấy đường dẫn ảnh để sửa ảnh.
                 arrDuongDan = QLBV_Library.QLBV_Ham.LayChuoi('|', strDD);
             }
            var vp = (from vpct in _Data.VienPhis.Where(p => p.MaBNhan == _mabn) select new { vpct.idVPhi }).ToList();

            DateTime ngay = Convert.ToDateTime("2018-04-01 00:00:00");
            if (vp.Count > 0)
            {
                MessageBox.Show("Bệnh nhân đã thanh toán không thể sửa!");
            }
            else
            {
                enablecontrol(false);
                mmKLNoisoi.Properties.ReadOnly = false;
                mmKQNoisoi.Properties.ReadOnly = false;
                mmLoidanNoisoi.Properties.ReadOnly = false;
            }
        }
        private void removeAllImage()
        {
            ptNoisoi1.Image = null;
            ptNoisoi2.Image = null;
            ptNoisoi3.Image = null;
            ptNoisoi4.Image = null;
            ptNoisoi5.Image = null;
            ptNoisoi6.Image = null;
        }
        private void btnXoa_Click(object sender, EventArgs e)
        {
            _Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var vp = (from vpct in _Data.VienPhis.Where(p => p.MaBNhan == _mabn) select new { vpct.idVPhi }).ToList();
            if (vp.Count > 0)
            { MessageBox.Show("Bệnh nhân đã thanh toán không thể xoá!"); }
            else
            {
                DialogResult dia = MessageBox.Show("Bạn muốn xóa kết quả?", "Hỏi xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (dia == DialogResult.Yes)
                {
                    int _maCK = 0;
                    var ck = (from nhom in _Data.NhomDVs.Where(p => p.TenNhomCT.Contains("Khám bệnh"))
                              join dvu in _Data.DichVus.Where(p => p.PLoai == 2).Where(p => p.Status == 1) on nhom.IDNhom equals dvu.IDNhom
                              select new { dvu.DonGia, dvu.MaDV, dvu.DonVi, dvu.TrongDM }).OrderByDescending(p => p.DonGia).ToList();
                    if (ck.Count > 0)
                        _maCK = ck.First().MaDV;
                    CL suacls = _Data.CLS.Where(p => p.IdCLS == _idcls).FirstOrDefault();
                    ChiDinh cdsua = _Data.ChiDinhs.Where(p => p.IDCD == _idcd).FirstOrDefault();
                    CLSct clsctsua = _Data.CLScts.Where(p => p.IDCD == _idcd).FirstOrDefault();

                    var iddt = _Data.DThuoccts.Where(p => p.IDCD == _idcd && p.MaDV != _maCK).ToList();
                    if (iddt.Count > 0)
                    {
                        foreach (var item in iddt)
                        {
                            int iddtct = item.IDDonct;
                            var ktchiphi = _Data.DThuoccts.Where(p => p.AttachIDDonct == iddtct).ToList();
                            if (ktchiphi.Count > 0)
                            {
                                MessageBox.Show("dịch vụ đã có chi phí đính kèm, bạn không thế xóa");
                                return;
                            }
                            var xoa = _Data.DThuoccts.Single(p => p.IDDonct == iddtct);
                            _Data.DThuoccts.Remove(xoa);
                            _Data.SaveChanges();
                        }
                    }

                    cdsua.NgayTH = null;
                    cdsua.KetLuan = "";
                    cdsua.LoiDan = "";
                    cdsua.MoTa = "";
                    cdsua.MaMay = "";
                    _Data.SaveChanges();

                    clsctsua.DuongDan = "";
                    clsctsua.DuongDan2 = "";
                    clsctsua.KetQua = "";
                        //suaclsct.MaCB = "";
                        //suaclsct.Ngaythang = null;
                    clsctsua.SoPhieu = 0;
                    clsctsua.Status = 0;
                    clsctsua.STTHT = 0;
                    _Data.SaveChanges();

                    suacls.MaCBth = "";
                    suacls.Status = 0;
                    suacls.NgayTH = null;
                    _Data.SaveChanges();

                    FRM_chidinh_Moi._setStatus(_mabn);
                    MessageBox.Show("Xoá thành công!");
                    enablecontrol(true);
                    removeAllImage();
                }
            }
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            _Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            CLS.InPhieu._inPhieu_CDHA_mau(_Data, "Nội soi", _mabn, _idcls, _idcd);
        }
    }
}