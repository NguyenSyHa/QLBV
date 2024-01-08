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
using QLBV.FormNhap;
using System.IO;
using System.Data.Objects.SqlClient;

namespace QLBV.ChucNang
{
    public partial class frm_ThucHienPT : DevExpress.XtraEditors.XtraForm
    {
        public frm_ThucHienPT()
        {
            InitializeComponent();
        }
        int _mabn = 0;
        public frm_ThucHienPT(int mabn)
        {
            InitializeComponent();
            _mabn = mabn;
        }
        int _idcd = 0;
        Action reload;
        public frm_ThucHienPT(int mabn, int idcd, Action _reload)
        {
            InitializeComponent();
            _mabn = mabn;
            this._idcd = idcd;
            this.reload = _reload;


            var chidinh = _Data.ChiDinhs.FirstOrDefault(o => o.IDCD == _idcd);
            int idcls = chidinh.IdCLS ?? 0;
            var suacls1 = _Data.CLS.Where(p => p.IdCLS == idcls).FirstOrDefault();
            if (DungChung.Bien.MaBV == "27001")
            {
                lupNgayTH.DateTime = DateTime.Now;
            }
            else
            {
                lupNgayTH.DateTime = suacls1.NgayThang.Value.AddMinutes(10);
            }
            if (DungChung.Bien.MaBV != "24012")
            {
                Chon1.Visible = false;
            }
            if (DungChung.Bien.MaBV != "24272")
            {
                Chon1.Visible = false;
            }
            if (suacls1 != null)
            {
                DateTime ngaycd = suacls1.NgayThang.Value;
                if (DungChung.Bien.MaBV == "27001")
                {
                    lupNgayTH.DateTime = DateTime.Now;
                }
                else
                {
                    lupNgayTH.DateTime = suacls1.NgayThang.Value.AddMinutes(10);
                }
                if (_ThucHienTatCa)
                {

                    if (DungChung.Bien.MaBV == "27001")
                    {
                        lupNgayTH.DateTime = DateTime.Now;
                    }
                    else
                    {
                        lupNgayTH.DateTime = suacls1.NgayThang.Value.AddMinutes(10);
                    }
                    if (DungChung.Bien.MaBV == "24012")
                    {
                        dtpNgayBatDau.DateTime = Convert.ToDateTime(ngaycd.Day + "/" + ngaycd.Month + "/" + ngaycd.Year + " " + dtpNgayBatDau.DateTime.Hour + ":" + dtpNgayBatDau.DateTime.Minute);
                        lupNgayTH.DateTime = Convert.ToDateTime(ngaycd.Day + "/" + ngaycd.Month + "/" + ngaycd.Year + " " + lupNgayTH.DateTime.Hour + ":" + lupNgayTH.DateTime.Minute);
                        DateTime ngayth = lupNgayTH.DateTime;
                    }
                }
            }

        }
        private bool ktraLuu()
        {
            if (string.IsNullOrWhiteSpace(lupNgayTH.Text))
            {
                MessageBox.Show("Bạn chưa chọn ngày thực hiện");
                lupNgayTH.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(LupCanBo.Text))
            {
                MessageBox.Show("Bạn chưa chọn người thực hiện");
                LupCanBo.Focus();
                return false;
            }
            return true;
        }
        QLBV_Database.QLBVEntities _Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        int status = 0;
        public void _getValue(bool a)
        {
            _ktmatkhau = a;
        }
        bool _ktmatkhau = false;
        //  int FocusedRowHandle = 0;
        bool ThucHienTTPT(int _idcd, DateTime ngayth)
        {
            chiDinhADOs = (List<ChiDinhADO>)grcChiDinhPTTT.DataSource;
            var dataSource1 = chiDinhADOs;
            float _idCLS = Convert.ToInt32(grvChiDinhPTTT.GetFocusedRowCellValue("IdCLS"));
            var checkDatas = dataSource1.Count > 0 ? dataSource1.Where(o => o.Check).ToList() : null;
            if (DungChung.Bien.MaBV == "24012" && DungChung.Bien.Checkin == true)
            {
                string dscb = ""; int makp = 0;
                dscb = txtPTVPhu.Text.Trim() + ";" + txtGMChinh.Text.Trim() + ";" + txtGMPhu.Text.Trim() + ";" + txtGiupViec.Text.Trim() + ";" + txtPTVPhu2.Text.Trim() + ";" + txtPTVPhu3.Text.Trim() + ";" + txtGMPhu2.Text.Trim() + ";" + txtGMPhu3.Text.Trim();
                int tyleTT = 100;
                if (!string.IsNullOrEmpty(txtTyLeTT.Text))
                    tyleTT = Convert.ToInt16(txtTyLeTT.Text);

                if (tyleTT <= 0 || tyleTT > 100)
                {
                    MessageBox.Show("Tỷ lệ thanh toán không hợp lệ!");
                    return false;
                }

                if (checkDatas.Count > 0)
                {
                    foreach (var item in checkDatas)
                    {
                        var chidinh = _Data.ChiDinhs.Single(p => p.IDCD == item.IDCD);
                        int idcls = 0;
                        if (chidinh != null)
                        {
                            idcls = chidinh.IdCLS ?? 0;
                        }

                        var suacls = _Data.CLS.Single(p => p.IdCLS == item.IdCLS);
                        if (suacls != null && ngayth.Year < 2016)

                            ngayth = suacls.NgayThang.Value.AddMinutes(1);

                        if (suacls != null)
                        {
                            DateTime ngaycd = suacls.NgayThang.Value;
                            if (DungChung.Bien.MaBV == "27001")
                            {
                                suacls.NgayTH = DateTime.Now;
                            }
                            else
                            {
                                suacls.NgayTH = suacls.NgayThang.Value.AddMinutes(10);
                            }

                            if (_ThucHienTatCa || DungChung.Bien.MaBV == "24012")
                            {
                                suacls.NgayTH = suacls.NgayThang.Value.AddMinutes(10);
                                dtpNgayBatDau.DateTime = Convert.ToDateTime(ngaycd.Day + "/" + ngaycd.Month + "/" + ngaycd.Year + " " + dtpNgayBatDau.DateTime.Hour + ":" + dtpNgayBatDau.DateTime.Minute);

                                lupNgayTH.DateTime = Convert.ToDateTime(ngaycd.Day + "/" + ngaycd.Month + "/" + ngaycd.Year + " " + lupNgayTH.DateTime.Hour + ":" + lupNgayTH.DateTime.Minute);

                                ngayth = lupNgayTH.DateTime;

                            }
                            else
                            {
                                if (ngaycd > ngayth)
                                {
                                    MessageBox.Show("Ngày thực hiện không được nhỏ hơn ngày chỉ định?");
                                    return false;
                                }
                                if (ngaycd.ToShortDateString() != ngayth.ToShortDateString())
                                {
                                    DialogResult _result = MessageBox.Show("Ngày thực hiện khác ngày chỉ định, Tiếp tục thực hiện?", "Hỏi Lưu", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                    if (_result == DialogResult.No)
                                    {
                                        return false;
                                    }
                                }
                            }
                        }
                        if (dtpNgayBatDau.DateTime > lupNgayTH.DateTime)
                        {
                            XtraMessageBox.Show("Ngày bắt đầu đang lớn hơn so với ngày kết thúc xin kiểm tra lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return false;
                        }

                        if (chidinh != null)
                        {
                            int icd9 = 0;
                            if (lupMaICD.EditValue != null)
                                icd9 = Convert.ToInt32(lupMaICD.EditValue);
                            chidinh.Status = 1;
                            chidinh.NgayBDTH = dtpNgayBatDau.DateTime;
                            chidinh.NgayTH = ngayth;
                            chidinh.DSCBTH = dscb;
                            chidinh.MaCBth = LupCanBo.EditValue != null ? LupCanBo.EditValue.ToString() : "";
                            chidinh.ICD9 = icd9;
                            chidinh.KetLuan = memoKL.Text.Trim();
                            chidinh.LoiDan = memo_PPVC.Text.Trim();
                            chidinh.Mau_Lan_MTruongXN = txtTuThe.Text.Trim();
                            chidinh.GhiChu = memoTacDung.Text.Trim();
                            chidinh.MoTa = memoTienMe.Text.Trim();
                            chidinh.NgoaiGioHC = Convert.ToInt32(FormThamSo.frm_UpdateLetet.CheckNGioHC(lupNgayTH.DateTime));
                            _Data.SaveChanges();
                            var suaclsct = _Data.CLScts.Where(p => p.IDCD == chidinh.IDCD).ToList();
                            foreach (var b in suaclsct)
                            {
                                b.Status = 1;
                                b.KetQua = memoKetQua.Text.Trim();
                                b.DuongDan = filePath;
                                _Data.SaveChanges();
                            }
                            var qdtct = _Data.DThuoccts.Where(p => p.IDCD == _idcd).ToList();
                            foreach (var a in qdtct)
                            {
                                a.MaCB = LupCanBo.EditValue != null ? LupCanBo.EditValue.ToString() : "";
                                a.NgayNhap = ngayth;
                                a.DSCBTH = dscb;
                                _Data.SaveChanges();
                            }



                        }


                        var chidinhall = _Data.ChiDinhs.Where(p => p.IdCLS == idcls && p.Status == 0).ToList();
                        //var suacls = _Data.CLS.Single(p => p.IdCLS == idcls);
                        suacls.MaCBth = LupCanBo.EditValue != null ? LupCanBo.EditValue.ToString() : "";
                        makp = suacls.MaKP == null ? 0 : Convert.ToInt32(suacls.MaKP); // Lấy makp để gán vào makp trong bàng DThuocct
                                                                                       // kiểm tra Khoa phòng thực hiện Lâm sàng thì gán vào DTHuocct
                        var kp = _Data.KPhongs.Where(p => (p.PLoai == "Lâm sàng" || p.PLoai == "Phòng khám") && p.MaKP == suacls.MaKPth).ToList();
                        if (kp.Count > 0)
                            makp = suacls.MaKPth == null ? 0 : Convert.ToInt32(suacls.MaKPth);
                        suacls.NgayTH = ngayth;
                        if (chidinhall.Count <= 0)
                            suacls.Status = 1;
                        suacls.DSCBTH = dscb;

                        _Data.SaveChanges();
                        updateDT(_mabn, chidinh.IDCD, tyleTT, (LupCanBo.EditValue != null ? LupCanBo.EditValue.ToString() : ""), dscb);
                    }
                }
                else
                {
                    MessageBox.Show("Chưa chọn TTPT để thực hiện", "Thông báo");
                }
                return true;
            }
            else
            {
                string dscb = ""; int makp = 0;
                dscb = txtPTVPhu.Text.Trim() + ";" + txtGMChinh.Text.Trim() + ";" + txtGMPhu.Text.Trim() + ";" + txtGiupViec.Text.Trim() + ";" + txtPTVPhu2.Text.Trim() + ";" + txtPTVPhu3.Text.Trim() + ";" + txtGMPhu2.Text.Trim() + ";" + txtGMPhu3.Text.Trim();
                int tyleTT = 100;
                if (!string.IsNullOrEmpty(txtTyLeTT.Text))
                    tyleTT = Convert.ToInt16(txtTyLeTT.Text);

                if (tyleTT <= 0 || tyleTT > 100)
                {
                    MessageBox.Show("Tỷ lệ thanh toán không hợp lệ!");
                    return false;
                }
                var chidinh = _Data.ChiDinhs.Single(p => p.IDCD == _idcd);
                int idcls = 0;
                if (chidinh != null)
                {
                    idcls = chidinh.IdCLS ?? 0;

                }
                //if (DungChung.Ham._checkTamThu(_Data, _mabn, idcls))
                //{

                var suacls = _Data.CLS.Single(p => p.IdCLS == idcls);
                if (suacls != null && ngayth.Year < 2016)

                    ngayth = suacls.NgayThang.Value.AddMinutes(1);
                DateTime ngaybdth = new DateTime();
                if (dtpNgayBatDau.EditValue != null)
                {
                    ngaybdth = Convert.ToDateTime(dtpNgayBatDau.EditValue);
                }

                if (suacls != null)
                {
                    DateTime ngaycd = suacls.NgayThang.Value;
                    if (DungChung.Bien.MaBV == "27001")
                    {
                        suacls.NgayTH = suacls.NgayThang.Value;
                    }
                    else
                    {
                        suacls.NgayTH = suacls.NgayThang.Value.AddMinutes(10);
                    }

                    if (_ThucHienTatCa)
                    {
                        if (DungChung.Bien.MaBV == "27001")
                        {
                            suacls.NgayTH = DateTime.Now;
                        }
                        else
                        {
                            suacls.NgayTH = suacls.NgayThang.Value.AddMinutes(10);
                        }
                        if (DungChung.Bien.MaBV == "24012")
                        {
                            dtpNgayBatDau.DateTime = Convert.ToDateTime(ngaycd.Day + "/" + ngaycd.Month + "/" + ngaycd.Year + " " + dtpNgayBatDau.DateTime.Hour + ":" + dtpNgayBatDau.DateTime.Minute);

                            lupNgayTH.DateTime = Convert.ToDateTime(ngaycd.Day + "/" + ngaycd.Month + "/" + ngaycd.Year + " " + lupNgayTH.DateTime.Hour + ":" + lupNgayTH.DateTime.Minute);

                            ngayth = lupNgayTH.DateTime;
                        }
                        if (DungChung.Bien.MaBV == "24272")
                        {
                            if (ngaycd > ngayth)
                            {
                                MessageBox.Show("Ngày thực hiện không được nhỏ hơn ngày chỉ định!!");
                                return false;
                            }
                            if (ngaybdth < ngaycd)
                            {
                                MessageBox.Show("Ngày thực hiện không được nhỏ hơn ngày chỉ định!!");
                                return false;
                            }
                        }
                    }
                    else
                    {
                        if (DungChung.Bien.MaBV == "24272")
                        {
                            if (ngaycd > ngayth)
                            {
                                MessageBox.Show("Ngày thực hiện không được nhỏ hơn ngày chỉ định!!");
                                return false;
                            }

                            if (ngaybdth < ngaycd)
                            {
                                MessageBox.Show("Ngày thực hiện không được nhỏ hơn ngày chỉ định!!");
                                return false;
                            }
                        }
                        if (ngaycd.ToShortDateString() != ngayth.ToShortDateString())
                        {
                            DialogResult _result = MessageBox.Show("Ngày thực hiện khác ngày chỉ định, Tiếp tục thực hiện?", "Hỏi Lưu", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (_result == DialogResult.No)
                            {
                                return false;
                            }
                        }
                    }
                }

                if (dtpNgayBatDau.DateTime > lupNgayTH.DateTime)
                {
                    XtraMessageBox.Show("Ngày bắt đầu đang lớn hơn so với ngày kết thúc xin kiểm tra lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                if (chidinh != null)
                {
                    int icd9 = 0;
                    if (lupMaICD.EditValue != null)
                        icd9 = Convert.ToInt32(lupMaICD.EditValue);
                    chidinh.Status = 1;
                    chidinh.NgayBDTH = dtpNgayBatDau.DateTime;
                    chidinh.NgayTH = ngayth;
                    chidinh.DSCBTH = dscb;
                    chidinh.MaCBth = LupCanBo.EditValue != null ? LupCanBo.EditValue.ToString() : "";
                    chidinh.ICD9 = icd9;
                    chidinh.KetLuan = memoKL.Text.Trim();
                    chidinh.LoiDan = memo_PPVC.Text.Trim();
                    chidinh.Mau_Lan_MTruongXN = txtTuThe.Text.Trim();
                    chidinh.GhiChu = memoTacDung.Text.Trim();
                    chidinh.MoTa = memoTienMe.Text.Trim();
                    chidinh.NgoaiGioHC = Convert.ToInt32(FormThamSo.frm_UpdateLetet.CheckNGioHC(lupNgayTH.DateTime));
                    _Data.SaveChanges();
                    var suaclsct = _Data.CLScts.Where(p => p.IDCD == chidinh.IDCD).ToList();
                    foreach (var b in suaclsct)
                    {
                        b.Status = 1;
                        b.KetQua = memoKetQua.Text.Trim();
                        b.DuongDan = filePath;
                        _Data.SaveChanges();
                    }
                    var qdtct = _Data.DThuoccts.Where(p => p.IDCD == _idcd).ToList();
                    foreach (var a in qdtct)
                    {
                        a.MaCB = LupCanBo.EditValue != null ? LupCanBo.EditValue.ToString() : "";
                        a.NgayNhap = ngayth;
                        a.DSCBTH = dscb;
                        _Data.SaveChanges();
                    }



                }


                var chidinhall = _Data.ChiDinhs.Where(p => p.IdCLS == idcls && p.Status == 0).ToList();
                //var suacls = _Data.CLS.Single(p => p.IdCLS == idcls);
                suacls.MaCBth = LupCanBo.EditValue != null ? LupCanBo.EditValue.ToString() : "";
                makp = suacls.MaKP == null ? 0 : Convert.ToInt32(suacls.MaKP); // Lấy makp để gán vào makp trong bàng DThuocct
                                                                               // kiểm tra Khoa phòng thực hiện Lâm sàng thì gán vào DTHuocct
                var kp = _Data.KPhongs.Where(p => (p.PLoai == "Lâm sàng" || p.PLoai == "Phòng khám") && p.MaKP == suacls.MaKPth).ToList();
                if (kp.Count > 0)
                    makp = suacls.MaKPth == null ? 0 : Convert.ToInt32(suacls.MaKPth);
                suacls.NgayTH = ngayth;
                if (chidinhall.Count <= 0)
                    suacls.Status = 1;
                suacls.DSCBTH = dscb;

                _Data.SaveChanges();
                updateDT(_mabn, chidinh.IDCD, tyleTT, (LupCanBo.EditValue != null ? LupCanBo.EditValue.ToString() : ""), dscb);
                return true;
            }
            //}
            //else
            //{
            //    return false;
            //}
        }
        int TTluu = 0;


        private void btnPhauThuat_Click(object sender, EventArgs e)
        {
            try
            {
                DungChung.Bien.Checkin = true;
                chiDinhADOs = (List<ChiDinhADO>)grcChiDinhPTTT.DataSource;
                var dataSource1 = chiDinhADOs;
                if (dataSource1 != null)
                {
                    var checkDatas = dataSource1.Where(o => o.Check).ToList();
                }

                _Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

                if (rvien != null)
                {
                    MessageBox.Show("Bệnh nhân đã ra viện!");
                    return;
                }
                if ((lupMaICD.EditValue == null || Convert.ToInt32(lupMaICD.EditValue) == 0) && DungChung.Bien.MaBV == "30007")
                {
                    MessageBox.Show("Chưa nhập mã ICD9");
                    return;
                }
                string dscb = ""; int makp = 0;
                int tyleTT = 100;
                if (!string.IsNullOrEmpty(txtTyLeTT.Text))
                    tyleTT = Convert.ToInt16(txtTyLeTT.Text);
                if (status == 0)
                {
                    if (_idcd > 0)
                    {
                        if (!ktraLuu())
                            return;
                        BenhNhan benhNhan = _Data.BenhNhans.FirstOrDefault(p => p.MaBNhan == _mabn);
                        if (benhNhan != null && benhNhan.CapCuu != 1 && (benhNhan.IDDTBN != 1 || benhNhan.DTuong != "BHYT") && string.IsNullOrWhiteSpace(benhNhan.SThe) && (DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "24272") && !DungChung.Ham.Check_DuyetTamThu(_idcd))
                        {
                            MessageBox.Show("Dịch vụ chưa được duyệt tạm thu không thể thực hiện");
                            return;
                        }
                        if (DungChung.Bien.MaBV == "01071")
                        {
                            if (LupCanBo.Text == null || LupCanBo.Text == "")
                            {
                                int bsth = (_Data.BenhNhans.Where(p => p.MaBNhan == _mabn).Where(x => x.DTuong == "BHYT" || x.NoThe == true)).Count();
                                if (bsth > 0)
                                {
                                    MessageBox.Show("Bệnh nhân bảo hiểm nợ thẻ không thể thực hiện");
                                }
                                else
                                {
                                    ThucHienTTPT(_idcd, lupNgayTH.DateTime);
                                    var tongcp = (from dt in _Data.DThuocs.Where(p => p.MaBNhan == _mabn)
                                                  join dtct in _Data.DThuoccts.Where(p => p.TrongBH == 1) on dt.IDDon equals dtct.IDDon
                                                  select new { dtct }).ToList();
                                    double tongcptrbh = 0;
                                    tongcptrbh = tongcp.Sum(p => p.dtct.ThanhTien);
                                    if (tongcptrbh >= 10000000)
                                    {
                                        MessageBox.Show("Tổng chi phí điều trị trong danh mục của bệnh nhân đã vượt quá 10.000.000");
                                    }
                                }
                            }
                            else
                            {
                                ThucHienTTPT(_idcd, lupNgayTH.DateTime);
                                var tongcp = (from dt in _Data.DThuocs.Where(p => p.MaBNhan == _mabn)
                                              join dtct in _Data.DThuoccts.Where(p => p.TrongBH == 1) on dt.IDDon equals dtct.IDDon
                                              select new { dtct }).ToList();
                                double tongcptrbh = 0;
                                tongcptrbh = tongcp.Sum(p => p.dtct.ThanhTien);
                                if (tongcptrbh >= 10000000)
                                {
                                    MessageBox.Show("Tổng chi phí điều trị trong danh mục của bệnh nhân đã vướt quá 10.000.000");
                                }
                            }
                        }
                        else
                        {
                            if (DungChung.Bien.MaBV == "14018" || DungChung.Bien.MaBV == "14017")
                            {
                                var dataSource = (List<ChiDinhADO>)grcChiDinhPTTT.DataSource;
                                var checkDatas = dataSource.Where(o => o.Check).ToList();
                                if (checkDatas.Count > 0)
                                {
                                    foreach (var item in checkDatas)
                                    {
                                        ThucHienTTPT(item.IDCD ?? 0, lupNgayTH.DateTime);
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Chưa chọn chỉ định để thực hiện");
                                }
                            }
                            else
                            {
                                ThucHienTTPT(_idcd, lupNgayTH.DateTime);
                            }
                        }
                        frm_ThucHienPT_Load(sender, e);
                    }
                }
                else
                {
                    if (_idcd > 0)
                    {
                        var dt1 = dataSource1.Where(o => o.Check).ToList();
                        foreach (var item in dt1)
                        {
                            BenhNhan benhNhan = _Data.BenhNhans.FirstOrDefault(p => p.MaBNhan == _mabn);
                            if (benhNhan != null && benhNhan.CapCuu != 1 && (benhNhan.IDDTBN != 1 || benhNhan.DTuong != "BHYT") && string.IsNullOrWhiteSpace(benhNhan.SThe) && (DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "24272") && !DungChung.Ham.Check_DuyetTamThu(item.IDCD.Value))
                            {
                                MessageBox.Show("Dịch vụ chưa được duyệt tam thu không thể thực hiện");
                                continue;
                            }
                        }
                        
                        if (TTluu == 2)
                        {
                            ThucHienTTPT(_idcd, lupNgayTH.DateTime);
                            TTluu = 0;
                        }
                        else //hủy thực hiện PTTT
                        {
                            if (DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "56789")
                            {
                                DateTime ktrangay = lupNgayTH.DateTime;
                                if (ktrangay.ToShortDateString() != System.DateTime.Today.ToShortDateString())
                                {
                                    MessageBox.Show("Chỉ có thể xóa khi thực hiện trong ngày", "Thông báo", MessageBoxButtons.OK);
                                    return;
                                }
                                //if (DungChung.Bien.CapDo >= 8)
                                //if (DungChung.Bien.PLoaiKP == DungChung.Bien.st_PhanLoaiKP.Admin)
                                //{
                                //    DateTime ktrangay = lupNgayTH.DateTime;
                                //    if (ktrangay.ToShortDateString() != System.DateTime.Today.ToShortDateString())
                                //    {
                                //        MessageBox.Show("Chỉ có thể xóa khi thực hiện trong ngày", "Thông báo", MessageBoxButtons.OK);
                                //        return;
                                //    }



                                //}
                                //else
                                //{
                                //    MessageBox.Show("Chỉ quyền admin mới có thể hủy");
                                //    return;
                                //}
                            }
                            int dem = 0;
                            var dataSource2 = dataSource1.Where(p => p.Check).ToList();
                            if (DungChung.Bien.MaBV == "24012" && dataSource2.Count > 1)
                            {
                                var checkDatas = dataSource1.Where(o => o.Check).ToList();
                                if (checkDatas.Count > 0)
                                {
                                    DialogResult _result = MessageBox.Show("Bạn muốn hủy thủ thuật - phẫu thuật: ?", "Hỏi hủy", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                    if (_result == DialogResult.Yes)
                                    {
                                        _ktmatkhau = true;
                                    }
                                    foreach (var item1 in dataSource2)
                                    {
                                        var suadt = _Data.DThuoccts.Where(p => p.IDCD == item1.IDCD).ToList();
                                        foreach (var a in suadt)
                                        {
                                            var qhcvt = _Data.DThuoccts.Where(p => p.AttachIDDonct == a.IDDonct).ToList();
                                            if (qhcvt.Count > 0)
                                            {
                                                MessageBox.Show("Dịch vụ đã kê hóa chất vật tư y tế đính kèm, bạn không thể hủy");
                                                return;
                                            }
                                        }
                                        if (_ktmatkhau)
                                        {
                                            int idcls = 0;
                                            var chidinh = _Data.ChiDinhs.Single(p => p.IDCD == item1.IDCD);
                                            if (chidinh != null)
                                            {

                                                chidinh.Status = 0;
                                                chidinh.MaCBth = "";
                                                chidinh.DSCBTH = "";
                                                chidinh.NgayTH = null;
                                                chidinh.KetLuan = "";
                                                chidinh.LoiDan = "";
                                                chidinh.Mau_Lan_MTruongXN = "";
                                                chidinh.GhiChu = "";
                                                chidinh.MoTa = "";
                                                _Data.SaveChanges();
                                                var clsct = _Data.CLScts.Where(p => p.IDCD == chidinh.IDCD).ToList();
                                                foreach (var b in clsct)
                                                {
                                                    b.KetQua = "";
                                                    b.Status = 0;
                                                    b.DuongDan = "";
                                                    _Data.SaveChanges();
                                                }

                                                if (suadt.Count > 0)
                                                {
                                                    foreach (var item in suadt)
                                                    {
                                                        int id = item.IDDonct;
                                                        var xoa = _Data.DThuoccts.Single(p => p.IDDonct == id);
                                                        _Data.DThuoccts.Remove(xoa);
                                                        _Data.SaveChanges();
                                                    }
                                                }
                                                idcls = chidinh.IdCLS ?? 0;
                                            }
                                            var suacls = _Data.CLS.Single(p => p.IdCLS == idcls);
                                            suacls.MaCBth = "";
                                            suacls.NgayTH = null;
                                            suacls.Status = 0;
                                            _Data.SaveChanges();
                                            dem++;
                                        }
                                        else
                                        {
                                            return;
                                        }
                                    }
                                    MessageBox.Show("Đã hủy thành công " + dem + " TTPT");
                                }
                                else
                                {
                                    MessageBox.Show("Chưa chọn TTPT để hủy");
                                }
                            }
                            else
                            {
                                foreach (var item in dataSource2)
                                {
                                    var suadt = _Data.DThuoccts.Where(p => p.IDCD == item.IDCD).ToList();
                                    foreach (var a in suadt)
                                    {
                                        var qhcvt = _Data.DThuoccts.Where(p => p.AttachIDDonct == a.IDDonct).ToList();
                                        if (qhcvt.Count > 0)
                                        {
                                            MessageBox.Show("Dịch vụ đã kê hóa chất vật tư y tế đính kèm, bạn không thể hủy");
                                            return;
                                        }
                                    }
                                }
                                
                                DialogResult _result = MessageBox.Show("Bạn muốn hủy thủ thuật - phẫu thuật?", "Hỏi hủy", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                if (_result == DialogResult.Yes)
                                {
                                    if (DungChung.Bien.MaBV == "24272" || DungChung.Bien.MaBV == "20001" || DungChung.Bien.MaBV == "24012" || DungChung.Bien.MaBV == "24389" || DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049")
                                        _ktmatkhau = true;
                                    else
                                    {
                                        ChucNang.frm_CheckPass frm = new ChucNang.frm_CheckPass();
                                        frm.ok = new ChucNang.frm_CheckPass._getdata(_getValue);
                                        frm.ShowDialog();
                                    }

                                }
                                if (_ktmatkhau)
                                {
                                    int idcls = 0;
                                    foreach (var item in dataSource2)
                                    {
                                        var chidinh = _Data.ChiDinhs.Where(p => p.IDCD == item.IDCD).FirstOrDefault();
                                        if (chidinh != null)
                                        {
                                            chidinh.Status = 0;
                                            chidinh.MaCBth = "";
                                            chidinh.DSCBTH = "";
                                            chidinh.NgayTH = null;
                                            chidinh.KetLuan = "";
                                            chidinh.LoiDan = "";
                                            chidinh.Mau_Lan_MTruongXN = "";
                                            chidinh.GhiChu = "";
                                            chidinh.MoTa = "";
                                            //chidinh.IdCLS = item.IdCLS;
                                            _Data.SaveChanges();
                                            var clsct = _Data.CLScts.Where(p => p.IDCD == chidinh.IDCD).ToList();
                                            foreach (var b in clsct)
                                            {
                                                b.KetQua = "";
                                                b.Status = 0;
                                                b.DuongDan = "";
                                                _Data.SaveChanges();
                                            }
                                            var suadt1 = _Data.DThuoccts.Where(p => p.IDCD == item.IDCD).ToList();

                                            if (suadt1.Count > 0)
                                            {
                                                foreach (var item1 in suadt1)
                                                {
                                                    int id = item1.IDDonct;
                                                    var xoa = _Data.DThuoccts.Where(p => p.IDDonct == id).Select(x => x).FirstOrDefault();
                                                    _Data.DThuoccts.Remove(xoa);
                                                    _Data.SaveChanges();
                                                }
                                            }
                                            idcls = chidinh.IdCLS ?? 0;
                                        }
                                        var suacls = _Data.CLS.Where(p => p.IdCLS == idcls).Select(x => x).FirstOrDefault();
                                        suacls.MaCBth = "";
                                        suacls.NgayTH = null;
                                        suacls.Status = 0;
                                        _Data.SaveChanges();
                                    }

                                    foreach (var item in dataSource1)
                                    {
                                        if (item.Check == true)
                                        {
                                            item.Status = 0;
                                            item.LoiDan = "";
                                            item.KetQua = "";
                                            item.KetLuan = "";
                                            item.MaCBth = "";
                                            item.Check = false;
                                        }
                                    }
                                    
                                }
                                else
                                {
                                    return;
                                }
                            }
                        }

                    }
                    string macbth = LupCanBo.EditValue != null ? LupCanBo.EditValue.ToString() : "";
                    dscb = txtPTVPhu.Text.Trim() + ";" + txtGMChinh.Text.Trim() + ";" + txtGMPhu.Text.Trim() + ";" + txtGiupViec.Text.Trim() + ";" + txtGMPhu2.Text.Trim() + ";" + txtGMPhu3.Text.Trim();
                    updateDT(_mabn, _idcd, tyleTT, macbth, dscb);
                    //frm_ThucHienPT_Load(sender, e);
                    grcChiDinhPTTT.DataSource = dataSource1;
                    new DevExpress.XtraGrid.Selection.CheckMarksSelection(grvChiDinhPTTT);
                    if (reload != null)
                        reload();
                }
                
                // grvChiDinhPTTT.FocusedRowHandle = FocusedRowHandle;
            }
            finally
            {
                _ktmatkhau = false;
            }
        }
        int focus = 0;
        public static bool updateDT(int _mabn, int _idcd, int tyleTT, string macb, string dscb)
        {
            try
            {
                int makp = 0;
                QLBV_Database.QLBVEntities _Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                int _idkb = 0;

                int iddthuoc = 0;
                var cdinh = (from cl in _Data.CLS
                             join cd1 in _Data.ChiDinhs.Where(p => p.Status == 1 && p.IDCD == _idcd) on cl.IdCLS equals cd1.IdCLS
                             join dv in _Data.DichVus on cd1.MaDV equals dv.MaDV
                             select new { cl.NgayTH, cd1.SoPhieu, cl.MaKP, cl.MaCB, cd1.MaDV, dv.DonGia, dv.DonGia2, cd1.IDCD, dv.DonVi, cd1.TrongBH, cl.IdCLS }).ToList();
                if (cdinh.Count > 0)
                    makp = cdinh.First().MaKP.Value;
                var bnkb = _Data.BNKBs.Where(p => p.MaBNhan == _mabn && p.MaKP == makp).OrderByDescending(p => p.IDKB).ToList();
                if (bnkb.Count > 0)
                    _idkb = bnkb.First().IDKB;// so sánh ngày khám bệnh và ngày thực hiện, để lấy đúng IDKB
                var ktdthuoc = _Data.DThuocs.Where(p => p.MaBNhan == _mabn).Where(p => p.PLDV == 2).ToList();
                if (ktdthuoc.Count > 0)
                    iddthuoc = ktdthuoc.First().IDDon;
                if (iddthuoc > 0)
                {
                    int Tontai = 0;
                    foreach (var d in cdinh)
                    {
                        var kt = (from dt in _Data.DThuoccts.Where(p => p.IDCD == d.IDCD) select new { dt.IDDonct }).ToList();
                        if (kt.Count > 0)
                        { Tontai = Tontai + 1; }
                    }
                    if (Tontai == 0)
                    {

                        foreach (var cd2 in cdinh)
                        {
                            double _dongia = DungChung.Ham._getGiaDM(_Data, cd2.MaDV == null ? 0 : cd2.MaDV.Value, cd2.TrongBH == null ? 1 : cd2.TrongBH.Value, _mabn, cd2.NgayTH == null ? DateTime.Now : cd2.NgayTH.Value);
                            DThuocct moi = new DThuocct();
                            moi.MaDV = cd2.MaDV;
                            moi.IDDon = iddthuoc;
                            moi.IDKB = _idkb;
                            moi.DonVi = cd2.DonVi;
                            moi.TrongBH = cd2.TrongBH == null ? 0 : cd2.TrongBH.Value;
                            moi.IDCD = cd2.IDCD;
                            moi.DonGia = _dongia;
                            moi.MaCB = macb;

                            moi.DSCBTH = dscb;
                            moi.MaKP = makp;
                            moi.ThanhTien = _dongia * tyleTT / 100;
                            moi.NgayNhap = cd2.NgayTH;
                            moi.SoLuong = 1;
                            if (cd2.SoPhieu != null && cd2.SoPhieu > 0)
                                moi.ThanhToan = 1;
                            moi.TyLeTT = tyleTT;
                            moi.IDCLS = cd2.IdCLS;
                            _Data.DThuoccts.Add(moi);
                            _Data.SaveChanges();
                            var CheckGiaPhuThu = _Data.DichVus.Where(p => p.MaDV == cd2.MaDV).FirstOrDefault();
                            var sss = _Data.BenhNhans.Where(p => p.MaBNhan == _mabn).Where(p => p.DTuong == "BHYT").ToList();
                            if (CheckGiaPhuThu != null && CheckGiaPhuThu.GiaPhuThu > 0 && sss.Count > 0 && moi.TrongBH == 1)
                            {
                                double s = CheckGiaPhuThu.GiaPhuThu;
                                DungChung.Ham._InsertPhuThu(_Data, moi.IDDonct, s);
                            }
                        }
                    }
                }
                else
                {
                    DThuoc dthuoccd = new DThuoc();
                    dthuoccd.NgayKe = cdinh.First().NgayTH;
                    dthuoccd.MaBNhan = _mabn;
                    dthuoccd.MaKP = cdinh.First().MaKP;
                    dthuoccd.MaCB = cdinh.First().MaCB;
                    dthuoccd.PLDV = 2;
                    dthuoccd.KieuDon = -1;
                    _Data.DThuocs.Add(dthuoccd);
                    if (_Data.SaveChanges() >= 0)
                    {
                        var maxid = _Data.DThuocs.Where(p => p.MaBNhan == _mabn).ToList().Max(p => p.IDDon);
                        foreach (var cd3 in cdinh)
                        {
                            double _dongia = DungChung.Ham._getGiaDM(_Data, cd3.MaDV == null ? 0 : cd3.MaDV.Value, cd3.TrongBH == null ? 1 : cd3.TrongBH.Value, _mabn, cd3.NgayTH == null ? DateTime.Now : cd3.NgayTH.Value);
                            DThuocct moi = new DThuocct();
                            moi.MaDV = cd3.MaDV;
                            moi.IDDon = maxid;
                            moi.IDKB = _idkb;

                            moi.TrongBH = cd3.TrongBH == null ? 0 : cd3.TrongBH.Value;

                            moi.MaCB = macb;

                            moi.DSCBTH = dscb;
                            moi.IDKB = _idkb;
                            moi.NgayNhap = cd3.NgayTH;
                            moi.MaKP = makp;
                            moi.IDCD = cd3.IDCD;
                            moi.DonVi = cd3.DonVi;
                            moi.DonGia = _dongia;
                            moi.ThanhTien = _dongia * tyleTT / 100;
                            moi.SoLuong = 1;
                            if (cd3.SoPhieu != null && cd3.SoPhieu > 0)
                                moi.ThanhToan = 1;
                            moi.TyLeTT = tyleTT;
                            moi.IDCLS = cdinh.First().IdCLS;
                            _Data.DThuoccts.Add(moi);
                            _Data.SaveChanges();
                            var CheckGiaPhuThu = _Data.DichVus.Where(p => p.MaDV == cd3.MaDV).FirstOrDefault();
                            var sss = _Data.BenhNhans.Where(p => p.MaBNhan == _mabn).Where(p => p.DTuong == "BHYT").ToList();
                            if (CheckGiaPhuThu != null && CheckGiaPhuThu.GiaPhuThu > 0 && sss.Count > 0 && moi.TrongBH == 1)
                            {
                                double s = CheckGiaPhuThu.GiaPhuThu;
                                DungChung.Ham._InsertPhuThu(_Data, moi.IDDonct, s);
                            }
                        }
                    }
                }

                return true;
            }
            catch
            {
                return false;
            }

        }
        private void _EnableControl(bool b)
        {
            LupCanBo.Properties.ReadOnly = b;
            lupNgayTH.Properties.ReadOnly = b;
            txtGMChinh.Properties.ReadOnly = b;
            txtGMPhu.Properties.ReadOnly = b;
            txtGMPhu2.Properties.ReadOnly = b;
            txtGMPhu3.Properties.ReadOnly = b;
            txtGiupViec.Properties.ReadOnly = b;
            txtPTVPhu.Properties.ReadOnly = b;
            memoKL.Properties.ReadOnly = b;
            lupMaICD.Properties.ReadOnly = b;
            btnTimKiemICD.Enabled = !b;
            memo_PPVC.Properties.ReadOnly = b;
            memoKetQua.Properties.ReadOnly = b;
            txtPTVPhu2.Properties.ReadOnly = b;
            txtPTVPhu3.Properties.ReadOnly = b;
            txtTuThe.Properties.ReadOnly = b;
            memoTacDung.Properties.ReadOnly = b;
            memoTienMe.Properties.ReadOnly = b;
            dtpNgayBatDau.Properties.ReadOnly = b;

        }
        int makpth = 0;
        RaVien rvien = new RaVien();
        class statusTH
        {
            int statuss;

            public int StatusTH
            {
                get { return statuss; }
                set { statuss = value; }
            }
            string tenStatus;

            public string TenStatus
            {
                get { return tenStatus; }
                set { tenStatus = value; }
            }
        }
        bool loadsinger = false;
        int load = 0;
        private void frm_ThucHienPT_Load(object sender, EventArgs e)
        {
            List<statusTH> _lstatus = new List<frm_ThucHienPT.statusTH>();
            _lstatus.Clear();
            _lstatus.Add(new statusTH { StatusTH = 0, TenStatus = "Chưa thực hiện" });
            _lstatus.Add(new statusTH { StatusTH = 1, TenStatus = "Đã thực hiện" });

            if (DungChung.Bien.MaBV == "24012")
            {
                layoutControlItem2.Text = "Người thực hiện: ";
            }
            if (DungChung.Bien.MaBV == "24272")
            {
                layoutControlItem33.Text = "Ngày chỉ định:";
                layoutControlItem1.Text = "Ngày thực hiện:";
            }
            lupstatus.DataSource = _lstatus;
            _Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            rvien = _Data.RaViens.Where(p => p.MaBNhan == _mabn).FirstOrDefault();
            var _lkp = _Data.KPhongs.ToList();
            lupKPth.DataSource = _lkp;
            lupKPCD.DataSource = _lkp;
            string ChuyenKhoa = "";
            if (_lkp.Where(p => p.MaKP == DungChung.Bien.MaKP).ToList().Count > 0)
                ChuyenKhoa = _lkp.Where(p => p.MaKP == DungChung.Bien.MaKP).First().ChuyenKhoa;
            else
                ChuyenKhoa = "";
            //if (ChuyenKhoa == "Phẫu thuật")
            //    btnPhauThuat.Enabled = true;
            //else
            //    btnPhauThuat.Enabled = false;
            if (DungChung.Bien.MaBV.StartsWith("30"))
            {
                List<ICD9> _lICD9 = new List<ICD9>();
                _lICD9 = _Data.ICD9.ToList();
                _lICD9.Insert(0, new ICD9 { MaICD = "", TenPTTT = "", ID = 0 });
                lupMaICD.Properties.DataSource = _lICD9;
            }
            else
            {
                lupMaICD.Visible = false;
                btnTimKiemICD.Visible = false;
                memoKL.Dock = System.Windows.Forms.DockStyle.Fill;
            }

            if (_idcd == 0)
            {
                this.colNgayCD.GroupFormat.FormatString = "d";
                this.colNgayCD.GroupFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                this.colNgayCD.GroupInterval = DevExpress.XtraGrid.ColumnGroupInterval.Date;
                this.colNgayCD.VisibleIndex = 0;
            }
            else
            {
                loadsinger = true;

            }

          
            var cbo = (from kp in _Data.KPhongs.Where(p => p.PLoai == ("Lâm sàng") || p.PLoai == "Phòng khám")
                       join cb in _Data.CanBoes.Where(p => p.Status == 1).Where(p => DungChung.Bien.MaBV == "30010" ? true : p.CapBac.ToLower().Contains("bs") || p.CapBac.ToLower().Contains("bác sĩ") || p.CapBac.ToLower().Contains("bác sỹ") || p.CapBac.ToLower().Contains("ys") || p.CapBac.ToLower().Contains("y sĩ") || p.CapBac.ToLower().Contains("giáo sư") || p.CapBac.ToLower().Contains("gs") || p.CapBac.ToLower().Contains("tiến sĩ") || p.CapBac.ToLower().Contains("ts"))
                       on kp.MaKP equals cb.MaKP
                       select cb).ToList();
            var cbo_20001 = (from kp in _Data.KPhongs.Where(p => (DungChung.Bien.MaBV == "14018" || DungChung.Bien.MaBV == "14017" || DungChung.Bien.MaBV == "24272") ? true : (p.PLoai == ("Lâm sàng") || p.PLoai == "Phòng khám"))
                             join cb in _Data.CanBoes.Where(p => p.Status == 1).Where(p => p.MaCCHN != "" && p.MaCCHN != null)
                             on kp.MaKP equals cb.MaKP
                             select cb).ToList();
            if(DungChung.Bien.MaBV == "30372")
            {
                cbo_20001 = (from kp in _Data.KPhongs.Where(p => (DungChung.Bien.MaBV == "14018" || DungChung.Bien.MaBV == "14017" || DungChung.Bien.MaBV == "24272") ? true : (p.PLoai == ("Cận lâm sàng") || p.PLoai == ("Lâm sàng") || p.PLoai == "Phòng khám"))
                             join cb in _Data.CanBoes.Where(p => p.Status == 1).Where(p => p.MaCCHN != "" && p.MaCCHN != null)
                             on kp.MaKP equals cb.MaKP
                             where cb.MaKP == 65 || cb.MaKPsd.Contains("65")
                             select cb).ToList();
            }
            //var nthg = cls.First().NgayThang.Value;
            //DateTime ngaygio1 = Convert.ToDateTime(nthg.Day + "-" + nthg.Month + "-" + nthg.Year + " " + dtpNgayBatDau.DateTime.Hour + ":" + dtpNgayBatDau.DateTime.AddMinutes(10));

            bool checkTamThu = DungChung.Ham.Check_DuyetTamThu(_idcd);

            LupCanBo.Properties.DataSource = cbo_20001; //(DungChung.Bien.MaBV == "20001" || DungChung.Bien.MaBV == "26007" || DungChung.Bien.MaBV == "30007" || DungChung.Bien.MaBV == "30012") ? cbo_20001 : cbo;
            lupMaCBcd.DataSource = cbo_20001;
            //_Data.TTboXungs.Remove
            lupCBth.DataSource = cbo_20001;
            var cls = (from cl in _Data.CLS.Where(p => p.MaBNhan == _mabn)
                       join bn in _Data.BenhNhans on cl.MaBNhan equals bn.MaBNhan
                       join cd in _Data.ChiDinhs.Where(p => (_idcd > 0 && load == 0) ? p.IDCD == _idcd : true) on cl.IdCLS equals cd.IdCLS
                       join clsct in _Data.CLScts on cd.IDCD equals clsct.IDCD
                       join dv in _Data.DichVus.Where(o => ((DungChung.Bien.MaBV == "14018") ? o.IS_EXECUTE_CLS == true : true)) on cd.MaDV equals dv.MaDV
                       join tn in _Data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                       join nhom in _Data.NhomDVs//.Where(p => p.TenNhomCT == ("Thủ thuật, phẫu thuật"))
                       on tn.IDNhom equals nhom.IDNhom
                       where (tn.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat || tn.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.PhauThuat || ((DungChung.Bien.MaBV == "14018" || DungChung.Bien.MaBV == "14017") ? (tn.TenRG == "Điều trị vật lý" || tn.TenRG == "Điều trị vận động" || tn.TenRG == "Điều trị y học cổ truyền" || tn.TenRG == "Điều trị ngôn ngữ trị liệu") : false))
                       select new ChiDinhADO { Check = false, MaCB = cl.MaCB, NgayThang = cl.NgayThang, KetQua = clsct.KetQua, LoiDan = cd.LoiDan, DSCBTH = cd.DSCBTH, KetLuan = cd.KetLuan, IdCLS = cl.IdCLS, MaKP = cl.MaKP, MaKPth = cl.MaKPth, NgayTH = cd.NgayTH, IDCD = cd.IDCD, MaDV = cd.MaDV, MaCBth = cd.MaCBth == null ? dv.IsAutoExecute == true ? cl.MaCB : cd.MaCBth : cd.MaCBth, Status = cd.Status, TenDV = DungChung.Bien.MaBV == "24012" ? dv.TenRG : dv.TenDV, ChiDinh = cd.ChiDinh1, ICD9 = cd.ICD9 }
                         ).Distinct().ToList();
            load++;
            if (cls.Count > 0 && cls.First().MaKPth != null)
                makpth = cls.First().MaKPth.Value;
            if (DungChung.Bien.MaBV == "27001")
            {
                lupNgayTH.DateTime = DateTime.Now;
            }
            //lupNgayTH.DateTime = cls.Last().NgayThang.Value;
            //lupNgayTH.DateTime.AddMinutes(10);


            if (DungChung.Bien.MaBV == "27022")
            {
                cbo = (from kp in _Data.KPhongs.Where(p => p.PLoai == ("Lâm sàng") || p.PLoai == "Phòng khám")
                       join cb in _Data.CanBoes.Where(p => p.Status == 1).Where(p => p.MaCCHN != "" && p.MaCCHN != null)
                       on kp.MaKP equals cb.MaKP
                       select cb).ToList();

                var cbo_th_27002 = (from kp in _Data.KPhongs
                                    join cb in _Data.CanBoes.Where(p => p.Status == 1).Where(p => p.ChucVu == "DD")
                                    on kp.MaKP equals cb.MaKP
                                    select cb).ToList();

                foreach (var i in cbo_th_27002)
                {
                    txtGiupViec.Properties.Items.Add(i.TenCB);
                }
                //}
                foreach (var item in cbo)
                {
                    //txtPTVPhu.Properties.Items.Add(item.TenCB);
                    //txtGMChinh.Properties.Items.Add(item.TenCB);
                    //txtGMPhu.Properties.Items.Add(item.TenCB);
                    //txtGiupViec.Properties.Items.Add(item.TenCB);
                    //txtPTVPhu2.Properties.Items.Add(item.TenCB);
                    //txtPTVPhu3.Properties.Items.Add(item.TenCB);
                    //txtGMPhu2.Properties.Items.Add(item.TenCB);
                    //txtGMPhu3.Properties.Items.Add(item.TenCB);
                    txtPTVPhu.Properties.DataSource = cbo;
                    txtGMChinh.Properties.DataSource = cbo;
                    txtGMPhu.Properties.DataSource = cbo;
                    txtPTVPhu2.Properties.DataSource = cbo;
                    txtPTVPhu3.Properties.DataSource = cbo;
                    txtGMPhu2.Properties.DataSource = cbo;
                    txtGMPhu3.Properties.DataSource = cbo;
                }
            }
            else
            {
                var q = _Data.KPhongs.Where(p => p.PLoai == ("Lâm sàng") || p.PLoai == "Phòng khám").Select(p => SqlFunctions.StringConvert((double)p.MaKP).Trim()).ToList();
                var qq = _Data.CanBoes.Where(p => p.Status == 1).Select(p => p.MaKPsd).ToList();
                if (DungChung.Bien.MaBV == "30372")
                {
                    cbo = (from kp in _Data.KPhongs.Where(p => p.PLoai == ("Lâm sàng") || p.PLoai == "Phòng khám")
                           from cbs in _Data.CanBoes.Where(p => p.Status == 1)
                           where cbs.MaKP == 65 || cbs.MaKPsd.Contains("65")
                           select cbs).Distinct().ToList();
                }
                else
                {
                    cbo = (from kp in _Data.KPhongs.Where(p => p.PLoai == ("Lâm sàng") || p.PLoai == "Phòng khám")
                           from cbs in _Data.CanBoes.Where(p => p.Status == 1)
                           where cbs.MaKPsd.Contains(SqlFunctions.StringConvert((double)kp.MaKP).Trim())
                           select cbs).ToList();
                }
                txtPTVPhu.Properties.DataSource = cbo;
                txtGMChinh.Properties.DataSource = cbo;
                txtGMPhu.Properties.DataSource = cbo;
                txtPTVPhu2.Properties.DataSource = cbo;
                txtPTVPhu3.Properties.DataSource = cbo;
                txtGMPhu2.Properties.DataSource = cbo;
                txtGMPhu3.Properties.DataSource = cbo;
                foreach (var item in cbo)
                {
                    //txtPTVPhu.Properties.Items.Add(item.TenCB);
                    //txtGMChinh.Properties.Items.Add(item.TenCB);
                    //txtGMPhu.Properties.Items.Add(item.TenCB);
                    txtGiupViec.Properties.Items.Add(item.TenCB);
                    //txtPTVPhu2.Properties.Items.Add(item.TenCB);
                    //txtPTVPhu3.Properties.Items.Add(item.TenCB);
                    //txtGMPhu2.Properties.Items.Add(item.TenCB);
                    //txtGMPhu3.Properties.Items.Add(item.TenCB);
                }
            }


            chiDinhADOs = cls;
            grcChiDinhPTTT.DataSource = chiDinhADOs;
            new DevExpress.XtraGrid.Selection.CheckMarksSelection(grvChiDinhPTTT);

            grvChiDinhPTTT.FocusedRowHandle = focus;

            if (DungChung.Bien.MaBV == "24297")
            {
                layoutControlItem2.Text = "Người thực hiện:";
            }
        }

        private void btnInGiayCN_Click(object sender, EventArgs e)
        {

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            //assdsd(sd)
        }
        #region phiếu phẫu thuật trĩ 20001
        void _inphieuPTtri_20001(int _mabn, int _idCD)
        {
            QLBV_Database.QLBVEntities Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            frmIn frm = new frmIn();
            #region BV khác

            {
                BaoCao.Rep_PhieuPTTT_tri_20001 rep = new BaoCao.Rep_PhieuPTTT_tri_20001();
                var qbn = Data.BenhNhans.Where(p => p.MaBNhan == _mabn).Select(bn => new { bn.TenBNhan, bn.GTinh, bn.Tuoi, bn.DChi, bn.SThe }).ToList();
                rep.TenBN.Value = qbn.First().TenBNhan.ToUpper();
                rep.DChi.Value = qbn.First().DChi;
                rep.SThe.Value = qbn.First().SThe;
                if (DungChung.Bien.MaBV == "24012")
                {
                    rep.Tuoi.Value = Convert.ToInt32(DungChung.Ham.TuoitheoThang(Data, _mabn, DungChung.Bien.formatAge_24012));
                }
                else
                    rep.Tuoi.Value = Convert.ToInt32(qbn.First().Tuoi);
                if (qbn.First().GTinh == 1) { rep.Nu.Value = "/".ToUpper(); }
                if (qbn.First().GTinh == 0) { rep.Nam.Value = "/".ToUpper(); }

                var qvv = Data.VaoViens.Where(p => p.MaBNhan == _mabn).Select(p => new { p.NgayVao, p.SoVV }).ToList();
                if (qvv.Count() > 0)
                {
                    rep.colSo.Text = qvv.First().SoVV;
                    if (qvv.First().NgayVao.ToString() != null)
                    {
                        rep.VaoVienLuc.Value = qvv.First().NgayVao.Value.Hour + " giờ " + qvv.First().NgayVao.Value.Minute + " phút, ngày "
                                              + qvv.First().NgayVao.Value.Day + " tháng " + qvv.First().NgayVao.Value.Month + " năm " + qvv.First().NgayVao.Value.Year;
                    }
                }
                else
                {
                    var id = (from kb in Data.BNKBs.Where(p => p.MaBNhan == _mabn)
                              group kb by kb.MaBNhan into kq
                              select new { kq.Key, IDKB = kq.Min(p => p.IDKB) }).ToList();
                    var qkb = (from k in id
                               join kb in Data.BNKBs on k.IDKB equals kb.IDKB
                               select new { kb.NgayKham }).ToList();
                    if (qkb.Count() > 0)
                    {

                        if (qkb.First().NgayKham.ToString() != null)
                        {
                            rep.VaoVienLuc.Value = qkb.First().NgayKham.Value.Hour + " giờ " + qkb.First().NgayKham.Value.Minute + " phút, ngày "
                                                  + qkb.First().NgayKham.Value.Day + " tháng " + qkb.First().NgayKham.Value.Month + " năm " + qkb.First().NgayKham.Value.Year;
                        }
                    }
                }

                var bs = (from cls in Data.CLS
                          join cd in Data.ChiDinhs.Where(p => p.IDCD == _idCD) on cls.IdCLS equals cd.IdCLS
                          join clsct in Data.CLScts on cd.IDCD equals clsct.IDCD
                          join dv in Data.DichVus on cd.MaDV equals dv.MaDV
                          select new { cd.NgayTH, cls.MaKP, cls.DSCBTH, cd.MaCBth, dv.TenDV, dv.Loai, cd.LoiDan, cd.KetLuan, clsct.KetQua }).ToList();
                if (bs.Count > 0)
                {
                    rep.PPhapPTTT.Value = bs.First().TenDV;
                    rep.PPhapVoCam.Value = bs.First().LoiDan;
                    rep.LoaiPTTT.Value = "Loại " + bs.First().Loai;
                    rep.CDSauPTTT.Value = bs.First().KetQua;
                    string macb = bs.First().MaCBth;
                    var canbo = Data.CanBoes.Where(p => p.MaCB == macb).FirstOrDefault();
                    if (canbo != null)
                        rep.PTTTVien.Value = (canbo.ChucVu == null ? "" : (canbo.ChucVu + ". ")) + canbo.TenCB;
                    rep.TrinhTuPT.Value = bs.First().KetLuan;
                    int makp = bs.First().MaKP ?? 0;
                    var qcd = (from kb in Data.BNKBs.Where(p => p.MaBNhan == _mabn && p.MaKP == makp)
                               join kp in Data.KPhongs on kb.MaKP equals kp.MaKP
                               select new { kb.Buong, kb.Giuong, kb.ChanDoan, kb.BenhKhac, kp.TenKP }).ToList();
                    if (qcd.Count() > 0)
                    {
                        rep.Buong.Value = qcd.First().Buong;
                        rep.Giuong.Value = qcd.First().Giuong;
                        rep.Khoa.Value = qcd.First().TenKP;
                        rep.CDVaoKhoa.Value = qcd.First().TenKP;
                        if (qcd.First().BenhKhac != null)
                        {
                            rep.CDTruocPTTT.Value = DungChung.Ham.FreshString(qcd.First().ChanDoan) + " / " + DungChung.Ham.FreshString(qcd.First().BenhKhac);
                        }
                        else
                        {
                            rep.CDTruocPTTT.Value = DungChung.Ham.FreshString(qcd.First().ChanDoan);
                        }
                        if (bs.First().NgayTH != null)
                        {
                            rep.PTTTLuc.Value = bs.First().NgayTH.Value.Hour + " giờ " + bs.First().NgayTH.Value.Minute + " phút, ngày "
                                                          + bs.First().NgayTH.Value.Day + " tháng " + bs.First().NgayTH.Value.Month + " năm " + bs.First().NgayTH.Value.Year;
                        }
                    }

                    if (bs.First().DSCBTH != null)
                    {
                        string _dscb = bs.First().DSCBTH.ToString();
                        string[] a = new string[5];
                        a = QLBV_Library.QLBV_Ham.LayChuoi(';', _dscb);
                        rep.PTTTPhu.Value = a[0];
                        rep.BSGM.Value = a[1];
                    }
                }
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();

            }
            #endregion

        }
        #endregion
        #region phiếu phẫu thuật thủ thuật
        private void _InPhieu_TTPT(int _mabn, int _idCD)
        {
            QLBV_Database.QLBVEntities Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            frmIn frm = new frmIn();
            #region BV Thường Tín
            if (DungChung.Bien.MaBV == "01830")
            {
                BaoCao.Rep_PhieuPTTT_01830 rep = new BaoCao.Rep_PhieuPTTT_01830();
                var qbn = Data.BenhNhans.Where(p => p.MaBNhan == _mabn).Select(bn => new { bn.TenBNhan, bn.GTinh, bn.Tuoi, bn.DChi, bn.SThe }).ToList();
                rep.TenBN.Value = qbn.First().TenBNhan.ToUpper();
                rep.DChi.Value = qbn.First().DChi;
                rep.SThe.Value = qbn.First().SThe;
                rep.Tuoi.Value = Convert.ToInt32(qbn.First().Tuoi);
                if (qbn.First().GTinh == 1) { rep.Nu.Value = "/".ToUpper(); }
                if (qbn.First().GTinh == 0) { rep.Nam.Value = "/".ToUpper(); }

                var qvv = Data.VaoViens.Where(p => p.MaBNhan == _mabn).Select(p => new { p.NgayVao }).ToList();
                if (qvv.Count() > 0)
                {

                    if (qvv.First().NgayVao.ToString() != null)
                    {
                        rep.VaoVienLuc.Value = qvv.First().NgayVao.Value.Hour + " giờ " + qvv.First().NgayVao.Value.Minute + " phút, ngày "
                                              + qvv.First().NgayVao.Value.Day + " tháng " + qvv.First().NgayVao.Value.Month + " năm " + qvv.First().NgayVao.Value.Year;
                    }
                }
                else
                {
                    var id = (from kb in Data.BNKBs.Where(p => p.MaBNhan == _mabn)
                              group kb by kb.MaBNhan into kq
                              select new { kq.Key, IDKB = kq.Min(p => p.IDKB) }).ToList();
                    var qkb = (from k in id
                               join kb in Data.BNKBs on k.IDKB equals kb.IDKB
                               select new { kb.NgayKham }).ToList();
                    if (qkb.Count() > 0)
                    {

                        if (qkb.First().NgayKham.ToString() != null)
                        {
                            rep.VaoVienLuc.Value = qkb.First().NgayKham.Value.Hour + " giờ " + qkb.First().NgayKham.Value.Minute + " phút, ngày "
                                                  + qkb.First().NgayKham.Value.Day + " tháng " + qkb.First().NgayKham.Value.Month + " năm " + qkb.First().NgayKham.Value.Year;
                        }
                    }
                }

                var bs = (from cls in Data.CLS
                          join cb in Data.CanBoes on cls.MaCBth equals cb.MaCB
                          join cd in Data.ChiDinhs.Where(p => p.IDCD == _idCD) on cls.IdCLS equals cd.IdCLS
                          join clsct in Data.CLScts on cd.IDCD equals clsct.IDCD
                          join dv in Data.DichVus on cd.MaDV equals dv.MaDV
                          select new { cls.NgayTH, cls.DSCBTH, cls.MaKP, cb.TenCB, dv.TenDV, dv.Loai, cd.LoiDan, cd.KetLuan, clsct.KetQua }).ToList();
                if (bs.Count > 0)
                {
                    int? makp = bs.First().MaKP;
                    var qcd = (from kb in Data.BNKBs.Where(p => p.MaBNhan == _mabn && p.MaKP == makp)
                               join kp in Data.KPhongs on kb.MaKP equals kp.MaKP
                               select new { kb.Buong, kb.Giuong, kb.ChanDoan, kb.BenhKhac, kp.TenKP }).ToList();
                    if (qcd.Count > 0)
                    {
                        rep.Buong.Value = qcd.First().Buong;
                        rep.Giuong.Value = qcd.First().Giuong;
                        rep.Khoa.Value = qcd.First().TenKP;
                        if (qcd.First().BenhKhac != null)
                        {
                            rep.CDTruocPTTT.Value = DungChung.Ham.FreshString(qcd.First().ChanDoan) + " / " + DungChung.Ham.FreshString(qcd.First().BenhKhac);
                        }
                        else
                        {
                            rep.CDTruocPTTT.Value = DungChung.Ham.FreshString(qcd.First().ChanDoan);
                        }

                    }
                    if (bs.First().NgayTH != null)
                    {
                        rep.PTTTLuc.Value = bs.First().NgayTH.Value.Hour + " giờ " + bs.First().NgayTH.Value.Minute + " phút, ngày "
                                                      + bs.First().NgayTH.Value.Day + " tháng " + bs.First().NgayTH.Value.Month + " năm " + bs.First().NgayTH.Value.Year;
                    }
                    rep.PPhapPTTT.Value = bs.First().TenDV;
                    rep.PPhapVoCam.Value = bs.First().LoiDan;
                    rep.LoaiPTTT.Value = "Loại " + bs.First().Loai;
                    rep.CDSauPTTT.Value = bs.First().KetQua;
                    rep.PTTTVien.Value = bs.First().TenCB;
                    rep.TrinhTuPT.Value = bs.First().KetLuan;
                    if (bs.First().DSCBTH != null)
                    {
                        string _dscb = bs.First().DSCBTH.ToString();
                        string[] a = new string[5];
                        a = QLBV_Library.QLBV_Ham.LayChuoi(';', _dscb);
                        rep.PTTTPhu.Value = a[0];
                        rep.BSGM.Value = a[1];
                    }
                }
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
            }
            #endregion
            #region 30010
            else if (DungChung.Bien.MaBV == "30010")
            {
                BaoCao.Rep_PhieuPTTT_30010 rep = new BaoCao.Rep_PhieuPTTT_30010();
                var qbn = Data.BenhNhans.Where(p => p.MaBNhan == _mabn).Select(bn => new { bn.TenBNhan, bn.GTinh, bn.Tuoi, bn.DChi, bn.SThe }).ToList();
                rep.TenBN.Value = qbn.First().TenBNhan.ToUpper();
                rep.DChi.Value = qbn.First().DChi;
                rep.SThe.Value = qbn.First().SThe;
                rep.Tuoi.Value = Convert.ToInt32(qbn.First().Tuoi);
                if (qbn.First().GTinh == 1) { rep.Nu.Value = "/".ToUpper(); }
                if (qbn.First().GTinh == 0) { rep.Nam.Value = "/".ToUpper(); }

                var qvv = Data.VaoViens.Where(p => p.MaBNhan == _mabn).Select(p => new { p.NgayVao }).ToList();
                if (qvv.Count() > 0)
                {
                    if (qvv.First().NgayVao.ToString() != null)
                    {
                        rep.VaoVienLuc.Value = qvv.First().NgayVao.Value.Hour + " giờ " + qvv.First().NgayVao.Value.Minute + " phút, ngày "
                                              + qvv.First().NgayVao.Value.Day + " tháng " + qvv.First().NgayVao.Value.Month + " năm " + qvv.First().NgayVao.Value.Year;
                    }
                }
                else
                {
                    var id = (from kb in Data.BNKBs.Where(p => p.MaBNhan == _mabn)
                              group kb by kb.MaBNhan into kq
                              select new { kq.Key, IDKB = kq.Min(p => p.IDKB) }).ToList();
                    var qkb = (from k in id
                               join kb in Data.BNKBs on k.IDKB equals kb.IDKB
                               select new { kb.NgayKham }).ToList();
                    if (qkb.Count() > 0)
                    {

                        if (qkb.First().NgayKham.ToString() != null)
                        {
                            rep.VaoVienLuc.Value = qkb.First().NgayKham.Value.Hour + " giờ " + qkb.First().NgayKham.Value.Minute + " phút, ngày "
                                                  + qkb.First().NgayKham.Value.Day + " tháng " + qkb.First().NgayKham.Value.Month + " năm " + qkb.First().NgayKham.Value.Year;
                        }
                    }
                }

                var bs = (from cls in Data.CLS
                          join cb in Data.CanBoes on cls.MaCBth equals cb.MaCB
                          join cd in Data.ChiDinhs.Where(p => p.IdCLS == _idCD) on cls.IdCLS equals cd.IdCLS
                          join clsct in Data.CLScts on cd.IDCD equals clsct.IDCD
                          join dv in Data.DichVus on cd.MaDV equals dv.MaDV
                          select new { cls.NgayTH, cls.MaKP, cls.DSCBTH, cb.TenCB, cb.ChucVu, dv.TenDV, dv.Loai, cd.LoiDan, cd.KetLuan, clsct.KetQua }).ToList();
                if (bs.Count() > 0)
                {
                    rep.PPhapPTTT.Value = bs.First().TenDV;
                    rep.PPhapVoCam.Value = bs.First().LoiDan;
                    rep.LoaiPTTT.Value = "Loại " + bs.First().Loai;
                    rep.CDSauPTTT.Value = bs.First().KetQua;
                    rep.PTTTVien.Value = (bs.First().ChucVu == null ? "" : (bs.First().ChucVu + ". ")) + bs.First().TenCB;
                    rep.TrinhTuPT.Value = bs.First().KetLuan;
                    int _makp = Convert.ToInt32(bs.First().MaKP);
                    var qcd = (from kb in Data.BNKBs.Where(p => p.MaBNhan == _mabn && p.MaKP == _makp)
                               join kp in Data.KPhongs on kb.MaKP equals kp.MaKP
                               select new { kb.Buong, kb.Giuong, kb.ChanDoan, kb.BenhKhac, kp.TenKP }).ToList();
                    if (qcd.Count() > 0)
                    {
                        rep.Buong.Value = qcd.First().Buong;
                        rep.Giuong.Value = qcd.First().Giuong;
                        rep.Khoa.Value = qcd.First().TenKP;
                        rep.CDVaoKhoa.Value = qcd.First().TenKP;
                        if (qcd.First().BenhKhac != null)
                        {
                            rep.CDTruocPTTT.Value = DungChung.Ham.FreshString(qcd.First().ChanDoan) + " / " + DungChung.Ham.FreshString(qcd.First().BenhKhac);
                        }
                        else
                        {
                            rep.CDTruocPTTT.Value = DungChung.Ham.FreshString(qcd.First().ChanDoan);
                        }
                        if (bs.First().NgayTH != null)
                        {
                            rep.PTTTLuc.Value = bs.First().NgayTH.Value.Hour + " giờ " + bs.First().NgayTH.Value.Minute + " phút, ngày "
                                                          + bs.First().NgayTH.Value.Day + " tháng " + bs.First().NgayTH.Value.Month + " năm " + bs.First().NgayTH.Value.Year;
                        }
                    }

                    if (bs.First().DSCBTH != null)
                    {
                        string _dscb = bs.First().DSCBTH.ToString();
                        string[] a = new string[5];
                        a = QLBV_Library.QLBV_Ham.LayChuoi(';', _dscb);
                        rep.PTTTPhu.Value = a[0];
                        rep.BSGM.Value = a[1];
                    }
                }
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();

            }
            #endregion

            #region BV khác
            else
            {
                BaoCao.Rep_PhieuPTTT rep = new BaoCao.Rep_PhieuPTTT();
                var qbn = Data.BenhNhans.Where(p => p.MaBNhan == _mabn).Select(bn => new { bn.TenBNhan, bn.NamSinh, bn.GTinh, bn.Tuoi, bn.DChi, bn.SThe }).ToList();
                rep.TenBN.Value = qbn.First().TenBNhan.ToUpper();
                rep.DChi.Value = qbn.First().DChi;
                rep.SThe.Value = qbn.First().SThe;
                rep.NamSinh.Value = qbn.First().NamSinh;
                rep.Tuoi.Value = DungChung.Bien.MaBV == "14018" ? DungChung.Ham.TinhTuoiBenhNhan(Data, _mabn) : qbn.First().Tuoi.ToString();
                if (qbn.First().GTinh == 1) { rep.Nu.Value = "/".ToUpper(); }
                if (qbn.First().GTinh == 0) { rep.Nam.Value = "/".ToUpper(); }

                var qvv = Data.VaoViens.Where(p => p.MaBNhan == _mabn).Select(p => new { p.NgayVao }).ToList();
                if (qvv.Count() > 0)
                {
                    if (qvv.First().NgayVao.ToString() != null)
                    {
                        rep.VaoVienLuc.Value = qvv.First().NgayVao.Value.Hour + " giờ " + qvv.First().NgayVao.Value.Minute + " phút, ngày "
                                              + qvv.First().NgayVao.Value.Day + " tháng " + qvv.First().NgayVao.Value.Month + " năm " + qvv.First().NgayVao.Value.Year;
                    }
                }
                else
                {
                    var id = (from kb in Data.BNKBs.Where(p => p.MaBNhan == _mabn)
                              group kb by kb.MaBNhan into kq
                              select new { kq.Key, IDKB = kq.Min(p => p.IDKB) }).ToList();
                    var qkb = (from k in id
                               join kb in Data.BNKBs on k.IDKB equals kb.IDKB
                               select new { kb.NgayKham }).ToList();
                    if (qkb.Count() > 0)
                    {

                        if (qkb.First().NgayKham.ToString() != null)
                        {
                            rep.VaoVienLuc.Value = qkb.First().NgayKham.Value.Hour + " giờ " + qkb.First().NgayKham.Value.Minute + " phút, ngày "
                                                  + qkb.First().NgayKham.Value.Day + " tháng " + qkb.First().NgayKham.Value.Month + " năm " + qkb.First().NgayKham.Value.Year;
                        }
                    }
                }

                var bs = (from cls in Data.CLS
                          join cb in Data.CanBoes on cls.MaCBth equals cb.MaCB
                          join cd in Data.ChiDinhs.Where(p => p.IDCD == _idCD) on cls.IdCLS equals cd.IdCLS
                          join clsct in Data.CLScts on cd.IDCD equals clsct.IDCD
                          join dv in Data.DichVus on cd.MaDV equals dv.MaDV
                          select new { cd.NgayBDTH, cls.NgayTH, cls.MaKP, cls.DSCBTH, cb.TenCB, cb.ChucVu, cb.CapBac, dv.TenDV, dv.Loai, cd.LoiDan, cd.KetLuan, clsct.KetQua }).ToList();
                if (bs.Count() > 0)
                {
                    rep.PPhapPTTT.Value = bs.First().TenDV;
                    rep.PPhapVoCam.Value = bs.First().LoiDan;
                    rep.LoaiPTTT.Value = "Loại " + bs.First().Loai;
                    rep.CDSauPTTT.Value = bs.First().KetQua;
                    rep.PTTTVien.Value = DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" ? (bs.First().CapBac == null ? "" : (bs.First().CapBac + ". ")) + bs.First().TenCB : (bs.First().ChucVu == null ? "" : (bs.First().ChucVu + ". ")) + bs.First().TenCB;
                    rep.TrinhTuPT.Value = bs.First().KetLuan;
                    int makp = bs.First().MaKP ?? 0;
                    var qcd = (from kb in Data.BNKBs.Where(p => p.MaBNhan == _mabn && p.MaKP == makp)
                               join kp in Data.KPhongs on kb.MaKP equals kp.MaKP
                               select new { kb.Buong, kb.Giuong, kb.ChanDoan, kb.BenhKhac, kp.TenKP }).ToList();
                    if (qcd.Count() > 0)
                    {
                        rep.Buong.Value = qcd.First().Buong;
                        rep.Giuong.Value = qcd.First().Giuong;
                        rep.Khoa.Value = qcd.First().TenKP;
                        rep.CDVaoKhoa.Value = qcd.First().TenKP;
                        if (qcd.First().BenhKhac != null)
                        {
                            rep.CDTruocPTTT.Value = DungChung.Ham.FreshString(qcd.First().ChanDoan) + " / " + DungChung.Ham.FreshString(qcd.First().BenhKhac);
                        }
                        else
                        {
                            rep.CDTruocPTTT.Value = DungChung.Ham.FreshString(qcd.First().ChanDoan);
                        }
                        if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049")
                        {
                            if (bs.First().NgayBDTH != null)
                            {
                                rep.PTTTLuc.Value = bs.First().NgayBDTH.Value.Hour + " giờ " + bs.First().NgayBDTH.Value.Minute + " phút, ngày "
                                                              + bs.First().NgayBDTH.Value.Day + " tháng " + bs.First().NgayBDTH.Value.Month + " năm " + bs.First().NgayBDTH.Value.Year;
                            }
                        }
                        else
                        {
                            if (bs.First().NgayTH != null)
                            {
                                rep.PTTTLuc.Value = bs.First().NgayTH.Value.Hour + " giờ " + bs.First().NgayTH.Value.Minute + " phút, ngày "
                                                              + bs.First().NgayTH.Value.Day + " tháng " + bs.First().NgayTH.Value.Month + " năm " + bs.First().NgayTH.Value.Year;
                            }
                        }
                    }

                    if (bs.First().DSCBTH != null)
                    {
                        string _dscb = bs.First().DSCBTH.ToString();
                        string[] a = new string[5];
                        a = QLBV_Library.QLBV_Ham.LayChuoi(';', _dscb);
                        rep.PTTTPhu.Value = a[0];
                        rep.BSGM.Value = a[1];
                    }
                }
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();

            }
            #endregion


        }
        #endregion
        #region giấy chứng nhận phẫu thuật
        private void _InGiay_CNPT(int _mabn, int _idCD)
        {
            QLBV_Database.QLBVEntities Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            frmIn frm = new frmIn();
            BaoCao.Rep_GiayChungNhanPhauThuat_TK rep = new BaoCao.Rep_GiayChungNhanPhauThuat_TK();

            var bv = Data.BenhViens.Where(p => p.MaBV == DungChung.Bien.MaBV).Select(p => new { p.TenBV }).ToList();
            if (bv.Count > 0)
            { rep.TenBV.Value = bv.First().TenBV; }
            var qbn = (from bn in Data.BenhNhans.Where(p => p.MaBNhan == _mabn)
                       join vv in Data.VaoViens on bn.MaBNhan equals vv.MaBNhan
                       select new { bn.TenBNhan, bn.DChi, vv.NgayVao, vv.SoVV, vv.NhomMau, vv.HeMau }).ToList();
            if (qbn.Count > 0)
            {
                rep.TenBN.Value = qbn.First().TenBNhan.ToUpper();
                rep.DChi.Value = qbn.First().DChi;
                rep.VVNgay.Value = qbn.First().NgayVao;
                rep.NhomMau.Value = qbn.First().NhomMau;
                rep.YeuToRh.Value = qbn.First().HeMau;
                rep.SoLT.Value = qbn.First().SoVV;
            }
            var qrv = Data.RaViens.Where(p => p.MaBNhan == _mabn).Select(p => new { p.KetQua, p.NgayRa }).ToList();
            if (qrv.Count > 0)
            {
                if (qrv.First().NgayRa != null)
                {
                    rep.RVNgay.Value = qrv.First().NgayRa;
                }
            }
            if (qrv.Count() > 0)
            {
                if (qrv.First().KetQua != null)
                {
                    rep.TTRaVien.Value = qrv.First().KetQua;
                }
            }
            var qcd = (from kb in Data.BNKBs.Where(p => p.MaBNhan == _mabn)
                       join cls in Data.CLS on kb.MaKP equals cls.MaKP
                       join cd in Data.ChiDinhs.Where(p => p.IDCD == _idCD) on cls.IdCLS equals cd.IdCLS
                       join clsct in Data.CLScts on cd.IDCD equals clsct.IDCD
                       select new { kb.ChanDoan, kb.BenhKhac, cd.KetLuan, clsct.KetQua, cls.NgayTH }).Distinct().ToList();
            rep.CDSauPT.Value = qcd.First().KetQua;
            rep.NgayKy.Value = DungChung.Ham.NgaySangChu(qcd.First().NgayTH.Value, DungChung.Bien.FormatDate);
            if (qcd.First().BenhKhac != null)
            {
                rep.CDTruocPT.Value = qcd.First().ChanDoan + " / " + qcd.First().BenhKhac;
            }
            else
            {
                rep.CDTruocPT.Value = qcd.First().ChanDoan;
            }
            if (qcd.Count > 0 && !string.IsNullOrEmpty(qcd.First().KetLuan))
            {
                rep.CTPT.Value = qcd.First().KetLuan;
            }
            else
            {
                rep.CTPT.Value = "......................................................................"
                    + "......................................................................................"
                    + "......................................................................................"
                    + "......................................................................................"
                    + "......................................................................................"
                    + "......................................................................................"
                    + "......................................................................................"
                    + "......................................................................................"
                    + "......................................................................................"
                    + "......................................................................................"
                   + "......................................................................................"
                    + "......................................................................................";
            }

            rep.CreateDocument();
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();



        }
        #endregion
        #region Phiếu bó bột
        private void _InPhieu_BoBot(int _mabn, int _idCD)
        {
            QLBV_Database.QLBVEntities Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            frmIn frm = new frmIn();
            BaoCao.Rep_PhieuBoBot_CL rep = new BaoCao.Rep_PhieuBoBot_CL();
            var qbn = Data.BenhNhans.Where(p => p.MaBNhan == _mabn).Select(bn => new { bn.TenBNhan, bn.DChi, bn.Tuoi }).ToList();
            if (qbn.Count() > 0)
            {
                rep.TenBN.Value = qbn.First().TenBNhan.ToUpper();
                rep.DChi.Value = qbn.First().DChi;
                rep.Tuoi.Value = DungChung.Bien.MaBV == "14018" ? DungChung.Ham.TinhTuoiBenhNhan(Data, _mabn) : qbn.First().Tuoi.ToString();
            }
            var qkb = (from cls in Data.CLS
                       join cd in Data.ChiDinhs.Where(p => p.IDCD == _idCD) on cls.IdCLS equals cd.IdCLS
                       join kb in Data.BNKBs.Where(p => p.MaBNhan == _mabn) on cls.MaKP equals kb.MaKP
                       select new { kb.NgayKham, kb.ChanDoan }).ToList();
            if (qkb.Count() > 0)
            {
                rep.NgayKham.Value = qkb.First().NgayKham.ToString().Substring(0, 10);
                rep.ChanDoan.Value = qkb.First().ChanDoan;
            }
            var qrv = Data.RaViens.Where(p => p.MaBNhan == _mabn).Select(p => new { p.PPDTr }).ToList();
            if (qrv.Count > 0)
            {
                rep.PPDT.Value = qrv.First().PPDTr;
            }
            rep.CreateDocument();
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();

        }
        #endregion
        #region giấy cam đoan mổ
        private void InGiayCamDoanMo(int idCD)
        {
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            frmIn frm = new frmIn();
            BaoCao.rep_GiayCamDoanMo rep = new BaoCao.rep_GiayCamDoanMo();
            rep.celTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            rep.celDiaDanh.Text = DungChung.Bien.DiaDanh + ", ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;
            CL cls = (from cd in data.ChiDinhs.Where(p => p.IDCD == idCD) join cl in data.CLS on cd.IdCLS equals cl.IdCLS select cl).FirstOrDefault();
            if (cls != null)
            {
                if (cls.MaKPth != null)
                {
                    KPhong kp = data.KPhongs.Where(p => p.MaKP == cls.MaKPth).FirstOrDefault();
                    if (kp != null && kp.TenKP != null)
                        rep.celKhoa.Text = ("Khoa: " + kp.TenKP).ToUpper();

                }
                BenhNhan bn = data.BenhNhans.Where(p => p.MaBNhan == (cls.MaBNhan ?? 0)).FirstOrDefault();
                if (bn != null)
                {
                    rep.celHoTen1.Text = bn.TenBNhan;
                    rep.celHoTen2.Text = bn.TenBNhan;
                }


            }
            rep.CreateDocument();
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();
        }

        #endregion
        #region giấy cam đoan PT-TT 20001
        private void phieu_camdoan_TT_PT(int mabn)
        {
            QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            frmIn frm = new frmIn();
            BaoCao.rep_GiayCamDoanTT_PT rep = new BaoCao.rep_GiayCamDoanTT_PT();
            var benhnhan = _data.BenhNhans.Where(p => p.MaBNhan == mabn).ToList();
            var kphong = _data.KPhongs.ToList();
            var ttbx = _data.TTboXungs.Where(p => p.MaBNhan == mabn).ToList();
            var tenkp = (from bn in benhnhan
                         join kp in kphong on bn.MaKP equals kp.MaKP
                         select new { kp.TenKP }).FirstOrDefault();
            if (benhnhan.Count() > 0)
            {
                rep.NamSinh.Value = benhnhan.First().NamSinh;
                rep.tenbn.Value = benhnhan.First().TenBNhan.ToUpper();
                rep.tuoi.Value = DungChung.Bien.MaBV == "14018" ? DungChung.Ham.TinhTuoiBenhNhan(_data, mabn) : benhnhan.First().Tuoi.ToString();
                //rep.gtinh.Value = benhnhan.First().GTinh == 1 ? "N?" : "Nam";
                rep.diachi.Value = benhnhan.First().DChi;
                if (benhnhan.First().GTinh == 1)
                {
                    rep.gtinh.Value = "Nam";
                }
                else
                {
                    rep.gtinh.Value = "Nữ";
                }

            }
            if (ttbx.Count() > 0)
            {
                if (ttbx.First().MaNN != null)
                {
                    string mann = "";
                    mann = ttbx.First().MaNN;
                    var nn = _data.DmNNs.Where(p => p.MaNN == mann).Select(p => p.TenNN).FirstOrDefault();
                    if (nn != null)
                    {
                        rep.nghenghiep.Value = nn.ToString();
                    }
                }
            }
            if (tenkp != null)
            {
                rep.khoa.Value = tenkp.TenKP.ToUpper();
                rep.khoadt.Value = tenkp.TenKP.ToString();
            }
            var dantoc = (from tt in ttbx
                          join dt in _data.DanTocs on tt.MaDT equals dt.MaDT
                          select dt.TenDT).FirstOrDefault();
            if (dantoc != null)
            {
                rep.dantoc.Value = dantoc.ToString();
            }

            rep.cqcq.Value = DungChung.Bien.TenCQCQ.ToUpper();
            rep.tencq.Value = DungChung.Bien.TenCQ.ToUpper();
            rep.tencq1.Value = DungChung.Bien.TenCQ.ToString();
            rep.CreateDocument();

            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();
        }
        #endregion
        #region Phiếu tóm tắt thông qua mổ 20001
        private void phieu_ttthongquamo(int mabn)
        {
            QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            frmIn frm = new frmIn();
            BaoCao.rep_tomtatmo20001 rep = new BaoCao.rep_tomtatmo20001();
            BenhNhan benhnhan = _data.BenhNhans.Single(p => p.MaBNhan == mabn);
            var kphong = _data.KPhongs.ToList();
            var vaovien = _data.VaoViens.Where(p => p.MaBNhan == mabn).ToList();
            var bnkb = _data.BNKBs.Where(p => p.MaBNhan == mabn).ToList();
            var khambenh = (from kb in bnkb
                            join kp in kphong on kb.MaKP equals kp.MaKP
                            select new
                            {
                                kb.IDKB,
                                kb.ChanDoan,
                                kb.MaKP,
                                kb.MaICD,
                                kp.TenKP
                            }).OrderByDescending(p => p.IDKB).ToList();

            if (khambenh.Count() > 0 && benhnhan != null && vaovien.Count > 0)
            {
                rep.TENBN.Value = benhnhan.TenBNhan.ToUpper().ToString();
                rep.TUOI.Value = DungChung.Bien.MaBV == "14018" ? DungChung.Ham.TinhTuoiBenhNhan(_Data, _mabn) : benhnhan.Tuoi.ToString();
                rep.GTINH.Value = benhnhan.Tuoi == 1 ? "N?" : "Nam";
                DateTime ngayvao = Convert.ToDateTime(vaovien.First().NgayVao);
                rep.NGAYVV.Value = ngayvao.ToShortDateString();
                rep.CHANDOAN.Value = khambenh.First().MaICD.ToString() + " - " + khambenh.First().ChanDoan.ToString();
                rep.KHOA.Value = khambenh.First().TenKP.ToUpper();
            }
            rep.CQCQ.Value = DungChung.Bien.TenCQCQ.ToUpper();
            rep.TENBV.Value = DungChung.Bien.TenCQ.ToUpper();
            rep.CreateDocument();
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();
        }
        #endregion
        private void cboIn_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cboIn.SelectedIndex == 0)
            {
                _InGiay_CNPT(_mabn, _idcd);
            }
            if (cboIn.SelectedIndex == 1)
            {
                _InPhieu_TTPT(_mabn, _idcd);

            }
            if (cboIn.SelectedIndex == 2)
            {
                _InPhieu_BoBot(_mabn, _idcd);
            }
            if (cboIn.SelectedIndex == 3) // phiếu cam đoan
            {
                InGiayCamDoanMo(_idcd);
            }

            if (cboIn.SelectedIndex == 4) // phiếu thủ thuật
            {
                inphieuthuthuat_20001(_idcd);
            }
            if (cboIn.SelectedIndex == 5) // phiếu thủ thuật
            {
                inphieuthuthuat_26007(_mabn, _idcd);
            }
            if (cboIn.SelectedIndex == 6) // phiếu thủ thuật trĩ 20001
            {
                _inphieuPTtri_20001(_mabn, _idcd);
            }
            if (cboIn.SelectedIndex == 7)
            {
                phieu_camdoan_TT_PT(_mabn);
            }
            if (cboIn.SelectedIndex == 8)
            {
                phieu_ttthongquamo(_mabn);
            }
            if (cboIn.SelectedIndex == 9)
            {
                // phieu_ttthongquamo(_mabn);
                inphieu(_mabn, _idcd);

            }
            if (cboIn.SelectedIndex == 10)
            {
                // phieu_ttthongquamo(_mabn);
                _inPhieu_Glocom(_Data, _mabn, _idcd);

            }
            if (cboIn.SelectedIndex == 11)
            {
                // phieu_ttthongquamo(_mabn);
                _inPhieu_Mong(_Data, _mabn, _idcd);

            }
            if (cboIn.SelectedIndex == 12)
            {
                // phieu_ttthongquamo(_mabn);
                _inPhieu_MoQuam(_Data, _mabn, _idcd);

            }
            if (cboIn.SelectedIndex == 13)
            {
                // phieu_ttthongquamo(_mabn);
                _inPhieu_ThuyTinh(_Data, _mabn, _idcd);

            }
            if (cboIn.SelectedIndex == 14)
            {
                // phieu_ttthongquamo(_mabn);
                _inPhieu_TTT_CatBe(_Data, _mabn, _idcd);

            }
            cboIn.SelectedIndex = -1;
        }
        private void inphieu(int _mabn, int _idCD)
        {
            QLBV_Database.QLBVEntities Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            frmIn frm = new frmIn();
            BaoCao.Rep_PhieuGayMeHoiSuc rep = new BaoCao.Rep_PhieuGayMeHoiSuc();
            var qbn = Data.BenhNhans.Where(p => p.MaBNhan == _mabn).Select(bn => new { bn.TenBNhan, bn.GTinh, bn.Tuoi, bn.DChi, bn.SThe }).ToList();
            rep.TenBN.Value = qbn.First().TenBNhan.ToUpper();
            rep.Tuoi.Value = DungChung.Bien.MaBV == "14018" ? DungChung.Ham.TinhTuoiBenhNhan(Data, _mabn) : qbn.First().Tuoi.ToString();
            if (qbn.First().GTinh == 1) { rep.Nu.Value = "/".ToUpper(); }
            if (qbn.First().GTinh == 0) { rep.Nam.Value = "/".ToUpper(); }

            var bs = (from cls in Data.CLS
                      join cd in Data.ChiDinhs.Where(p => p.IDCD == _idCD) on cls.IdCLS equals cd.IdCLS
                      join clsct in Data.CLScts on cd.IDCD equals clsct.IDCD
                      join dv in Data.DichVus on cd.MaDV equals dv.MaDV
                      select new { cls.NgayTH, cls.MaKP, cd.DSCBTH, cd.MaCBth, dv.TenDV, dv.Loai, cd.LoiDan, cd.KetLuan, clsct.KetQua, cd.GhiChu, cd.Mau_Lan_MTruongXN, cls.ChanDoan, cd.MoTa }).ToList();
            var qvv = Data.VaoViens.Where(p => p.MaBNhan == _mabn).Select(p => new { p.CanNang, p.ChieuCao, p.NhomMau, p.SoVV }).ToList();
            if (qvv.Count() > 0)
            {
                if (qvv.First().ChieuCao != null)
                {
                    rep.ChieuCao.Value = qvv.First().ChieuCao + "cm";
                }
                else rep.ChieuCao.Value = "  cm";
                rep.CanNang.Value = qvv.First().CanNang;
                rep.NhomMau.Value = qvv.First().NhomMau;
                rep.SoVV.Value = qvv.First().SoVV;
            }
            if (bs.Count() > 0)
            {
                rep.PPhapPTTT.Value = bs.First().TenDV;
                rep.PPhapVoCam.Value = bs.First().LoiDan;
                string macb = bs.First().MaCBth;
                var cb = (from a in Data.CanBoes.Where(p => p.MaCB == macb) select new { a.TenCB, a.ChucVu }).ToList();
                rep.PTTTVien.Value = (cb.First().ChucVu == null ? "" : (cb.First().ChucVu + ". ")) + cb.First().TenCB;
                rep.TrinhTuPT.Value = bs.First().KetLuan;
                rep.TacDung.Value = bs.First().GhiChu;
                rep.TuThe.Value = bs.First().Mau_Lan_MTruongXN;
                rep.CDTruocPTTT.Value = bs.First().ChanDoan;
                rep.TienMe.Value = bs.First().MoTa;
                if (bs.First().NgayTH != null)
                {
                    rep.NgayTH.Value = "- Ngày " + bs.First().NgayTH.Value.Day + " tháng " + bs.First().NgayTH.Value.Month + " năm " + bs.First().NgayTH.Value.Year;
                    rep.NgayTH1.Value = "Ngày " + bs.First().NgayTH.Value.Day + " tháng " + bs.First().NgayTH.Value.Month + " năm " + bs.First().NgayTH.Value.Year;
                }
                else
                {
                    rep.NgayTH.Value = "- Ngày ........ tháng ........ năm ........";
                    rep.NgayTH1.Value = "Ngày ........ tháng ........ năm ........";
                }
                int _makp = Convert.ToInt32(bs.First().MaKP);
                var qcd = (from kb in Data.BNKBs.Where(p => p.MaBNhan == _mabn && p.MaKP == _makp)
                           join kp in Data.KPhongs on kb.MaKP equals kp.MaKP
                           select new { kb.Buong, kb.Giuong, kp.TenKP }).ToList();
                if (qcd.Count() > 0)
                {
                    rep.Buong.Value = qcd.First().Buong;
                    rep.Giuong.Value = qcd.First().Giuong;
                    rep.Khoa.Value = qcd.First().TenKP;

                }

                if (bs.First().DSCBTH != null)
                {
                    string _dscb = bs.First().DSCBTH.ToString();
                    string[] a = new string[7];
                    a = QLBV_Library.QLBV_Ham.LayChuoi(';', _dscb);
                    rep.BSGM.Value = "Họ tên: " + a[1];
                    if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "56789")
                        rep.BSGM1.Value = a[1] + "; " + a[2] + "; " + a[6] + "; " + a[7];
                    else
                        rep.BSGM1.Value = a[1];
                }
            }
            rep.CreateDocument();
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();

        }
        private void inphieuthuthuat_26007(int _mabn, int _idCD)
        {
            #region BV huyện Bình Xuyên
            frmIn frm = new frmIn();
            QLBV_Database.QLBVEntities Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            BaoCao.Rep_PhieuPTTT_27006 rep = new BaoCao.Rep_PhieuPTTT_27006();

            var qbn = Data.BenhNhans.Where(p => p.MaBNhan == _mabn).Select(bn => new { bn.TenBNhan, bn.GTinh, bn.Tuoi, bn.DChi, bn.SThe }).ToList();
            rep.TenBN.Value = qbn.First().TenBNhan.ToUpper();
            rep.DChi.Value = qbn.First().DChi;
            rep.SThe.Value = qbn.First().SThe;
            rep.Tuoi.Value = DungChung.Bien.MaBV == "14018" ? DungChung.Ham.TinhTuoiBenhNhan(Data, _mabn) : qbn.First().Tuoi.ToString();
            if (qbn.First().GTinh == 1) { rep.Nu.Value = "/".ToUpper(); }
            if (qbn.First().GTinh == 0) { rep.Nam.Value = "/".ToUpper(); }

            var qvv = Data.VaoViens.Where(p => p.MaBNhan == _mabn).Select(p => new { p.NgayVao }).ToList();
            if (qvv.Count() > 0)
            {

                if (qvv.First().NgayVao.ToString() != null)
                {
                    rep.VaoVienLuc.Value = qvv.First().NgayVao.Value.Hour + " giờ " + qvv.First().NgayVao.Value.Minute + " phút, ngày "
                                          + qvv.First().NgayVao.Value.Day + " tháng " + qvv.First().NgayVao.Value.Month + " năm " + qvv.First().NgayVao.Value.Year;
                }
            }
            else
            {
                var id = (from kb in Data.BNKBs.Where(p => p.MaBNhan == _mabn)
                          group kb by kb.MaBNhan into kq
                          select new { kq.Key, IDKB = kq.Min(p => p.IDKB) }).ToList();
                var qkb = (from k in id
                           join kb in Data.BNKBs on k.IDKB equals kb.IDKB
                           select new { kb.NgayKham }).ToList();
                if (qkb.Count() > 0)
                {

                    if (qkb.First().NgayKham.ToString() != null)
                    {
                        rep.VaoVienLuc.Value = qkb.First().NgayKham.Value.Hour + " giờ " + qkb.First().NgayKham.Value.Minute + " phút, ngày "
                                              + qkb.First().NgayKham.Value.Day + " tháng " + qkb.First().NgayKham.Value.Month + " năm " + qkb.First().NgayKham.Value.Year;
                    }
                }
            }

            var bs = (from cls in Data.CLS
                      join cb in Data.CanBoes on cls.MaCBth equals cb.MaCB
                      join cd in Data.ChiDinhs.Where(p => p.IDCD == _idCD) on cls.IdCLS equals cd.IdCLS
                      join clsct in Data.CLScts on cd.IDCD equals clsct.IDCD
                      join dv in Data.DichVus on cd.MaDV equals dv.MaDV
                      select new { clsct.DuongDan, cb.ChucVu, cls.NgayTH, cls.DSCBTH, cls.MaKP, cb.TenCB, dv.TenDV, dv.Loai, cd.LoiDan, cd.KetLuan, clsct.KetQua }).ToList();
            if (bs.Count > 0)
            {
                int makp = bs.First().MaKP ?? 0;
                var qcd = (from kb in Data.BNKBs.Where(p => p.MaBNhan == _mabn && p.MaKP == makp)
                           join kp in Data.KPhongs on kb.MaKP equals kp.MaKP
                           select new { kb.Buong, kb.Giuong, kb.ChanDoan, kb.BenhKhac, kp.TenKP }).ToList();
                if (qcd.Count > 0)
                {
                    rep.Buong.Value = qcd.First().Buong;
                    rep.Giuong.Value = qcd.First().Giuong;
                    rep.Khoa.Value = qcd.First().TenKP;
                    if (qcd.First().BenhKhac != null)
                    {
                        rep.CDTruocPTTT.Value = DungChung.Ham.FreshString(qcd.First().ChanDoan) + " / " + DungChung.Ham.FreshString(qcd.First().BenhKhac);
                    }
                    else
                    {
                        rep.CDTruocPTTT.Value = DungChung.Ham.FreshString(qcd.First().ChanDoan);
                    }

                }
                if (bs.First().NgayTH != null)
                {
                    rep.PTTTLuc.Value = bs.First().NgayTH.Value.Hour + " giờ " + bs.First().NgayTH.Value.Minute + " phút, ngày "
                                                  + bs.First().NgayTH.Value.Day + " tháng " + bs.First().NgayTH.Value.Month + " năm " + bs.First().NgayTH.Value.Year;
                }
                rep.PPhapPTTT.Value = bs.First().TenDV;
                rep.PPhapVoCam.Value = bs.First().LoiDan;
                rep.LoaiPTTT.Value = "Loại " + bs.First().Loai;
                rep.CDSauPTTT.Value = "- Sau phẫu thuật/ thủ thuật:  " + bs.First().KetQua;
                var vv = (from vv1 in Data.VaoViens.Where(p => p.MaBNhan == _mabn) select new { vv1.SoVV });
                if (vv.Count() > 0)
                    rep.vaovien.Value = vv.First().SoVV;
                else rep.vaovien.Value = "";
                rep.TrinhTuPT.Value = bs.First().KetLuan;
                rep.PTTTVien.Value = (bs.First().ChucVu == null ? "" : (bs.First().ChucVu + ". ")) + bs.First().TenCB;
                rep.TrinhTuPT.Value = bs.First().KetLuan;
                rep.HinhAnh.Value = (bs.First().DuongDan == null ? "" : bs.First().DuongDan);
                if (bs.First().DSCBTH != null)
                {
                    string _dscb = bs.First().DSCBTH.ToString();
                    string[] a = new string[5];
                    a = QLBV_Library.QLBV_Ham.LayChuoi(';', _dscb);
                    rep.PTTTPhu.Value = a[0] + ", " + a[3] + ", " + a[4] + ", " + a[5];
                    rep.BSGM.Value = a[1];
                    rep.BSGMP.Value = a[2];
                }
            }
            rep.CreateDocument();
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();

            #endregion
        }
        static void inphieuthuthuat_20001(int _idcld)
        {
            string ketluan = "";
            QLBV_Database.QLBVEntities _db = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            BaoCao.REP_PTT_20001 rep = new BaoCao.REP_PTT_20001();

            var kq2 = (from
                         bn in _db.BenhNhans
                       join cls in _db.CLS on bn.MaBNhan equals cls.MaBNhan
                       join kb in _db.BNKBs on new { cls.MaKP, bn.MaBNhan } equals new { kb.MaKP, MaBNhan = kb.MaBNhan ?? 0 }
                       //join kp in _db.KPhongs on cls.MaKP equals kp.MaKP
                       join cd in _db.ChiDinhs.Where(p => p.IDCD == _idcld) on cls.IdCLS equals cd.IdCLS
                       join dv in _db.DichVus on cd.MaDV equals dv.MaDV
                       //join cb in _db.CanBoes on cls.MaCBth equals cb.MaCB
                       join clsct in _db.CLScts on cd.IDCD equals clsct.IDCD
                       select new
                       {
                           cls.MaKP,
                           cd.MaCBth,
                           cd.MaDV,
                           cd.ChiDinh1,
                           cls.MaBNhan,
                           bn.TenBNhan,
                           bn.Tuoi,
                           GTinh = bn.GTinh == 1 ? "Nam" : "Nữ",
                           bn.DTuong,
                           //kp.TenKP,
                           Buong = kb.Buong == null ? "" : kb.Buong,
                           Giuong = kb.Giuong == null ? "" : kb.Giuong,
                           kb.ChanDoan,
                           kb.BenhKhac,
                           kb.MaICD,
                           kb.MaICD2,
                           dv.TenDV,
                           dv.Loai,
                           //cb.TenCB,
                           cd.DSCBTH,
                           cd.KetLuan
                       }).ToList();
            var kq = kq2.FirstOrDefault();

            if (kq != null)
            {

                var svv = (from vv in _db.VaoViens.Where(p => p.MaBNhan == kq.MaBNhan)
                           select vv.SoVV).FirstOrDefault();
                if (svv != null)
                {
                    rep.lab_sovv.Text = svv.ToString();
                }
                var solan = (from cls in _db.CLS.Where(p => p.MaBNhan == kq.MaBNhan)
                             join cd in _db.ChiDinhs.Where(p => p.MaDV == kq.MaDV && p.Status == 1) on cls.IdCLS equals cd.IdCLS
                             select new
                             {
                                 cd.KetLuan,
                                 cls.NgayThang,
                                 cls.IdCLS
                             }).Distinct().OrderBy(p => p.NgayThang).ToList();


                if (solan.Count > 0)
                {
                    rep.ngaytu.Value = solan.First().NgayThang.Value.ToShortDateString();
                    rep.ngayden.Value = solan.Last().NgayThang.Value.ToShortDateString();
                    List<string> kl = solan.Select(p => p.KetLuan).ToList().Distinct().ToList();
                    string[] a = kl.ToArray();
                    ketluan = string.Join(";", a);
                }

                rep.solan.Value = solan.Count;
            }
            frmIn frm = new frmIn();




            if (kq != null)
            {
                string tencbth = kq.DSCBTH;
                if (tencbth != null)
                {
                    string[] arrTenCB = tencbth.Split(';');
                    if (arrTenCB.Count() > 0)
                        rep.PTV1.Value = arrTenCB[0];
                    if (arrTenCB.Count() > 1)
                        rep.PTV2.Value = arrTenCB[4];

                }
                string benhkhac = kq.BenhKhac;
                string icdkhac = kq.MaICD2;
                string chandoan = "";
                if (benhkhac != null && icdkhac != null)
                {
                    string[] arricdkhac = icdkhac.Split(';');
                    string[] arrbenhkhac = benhkhac.Split(';');
                    for (int i = 0; i < arricdkhac.Length; i++)
                    {
                        if (arricdkhac[i] != "")
                            chandoan += arricdkhac[i] + "_" + arrbenhkhac[i] + ",";
                    }
                }
                rep.Tenbn.Value = kq.TenBNhan.ToUpper();
                rep.Tuoi.Value = DungChung.Bien.MaBV == "14018" ? DungChung.Ham.TinhTuoiBenhNhan(_db, Convert.ToInt32(kq.MaBNhan)) : kq.Tuoi.ToString();
                rep.Gtinh.Value = kq.GTinh.ToString();
                var kp = _db.KPhongs.Where(p => p.MaKP == kq.MaKP).FirstOrDefault();
                if (kp != null)
                    rep.Khoa.Value = kp.TenKP.ToString();
                if (kq.DTuong != null)
                    rep.Dtuong.Value = kq.DTuong.ToString();
                if (kq.ChanDoan != null)
                    rep.Chandoan.Value = "- Chẩn đoán: " + kq.MaICD.ToString() + "_" + kq.ChanDoan.ToString() + "," + chandoan.ToString();
                if (kq.Buong != null || kq.Giuong != null)
                    rep.Buong.Value = kq.Buong.ToString() + "_" + kq.Giuong.ToString();
                rep.CDinh.Value = (kq.MaDV == 122 && kq.ChiDinh1 != null) ? (kq.TenDV.ToString() + "(" + kq.ChiDinh1 + ")") : kq.TenDV.ToString();
                string Loai = "";
                if (kq.Loai != null)
                {
                    switch (Convert.ToInt32(kq.Loai))
                    {
                        case 1:
                            Loai = "I";
                            break;
                        case 2:
                            Loai = "II";
                            break;
                        case 3:
                            Loai = "III";
                            break;
                        case 4:
                            Loai = "IV";
                            break;
                        default:
                            break;
                    }

                }
                rep.Loai.Value = Loai;
                if (ketluan != null)
                    rep.ketqua.Value = ketluan;
                var cb = _db.CanBoes.Where(p => p.MaCB == kq.MaCBth).FirstOrDefault();
                if (cb != null)
                    rep.cbchinh.Value = cb.TenCB.ToString();

            }

            rep.Lab_tencq.Text = DungChung.Bien.TenCQCQ.ToUpper();
            rep.lab_tenbv.Text = DungChung.Bien.TenCQ.ToUpper();
            rep.CreateDocument();
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();


        }
        private void mmChiDinh_EditValueChanged(object sender, EventArgs e)
        {

        }
        class LSCCB
        {
            public string MKPSD { get; set; }
        }
        List<LSCCB> _lst = new List<LSCCB>();
        private void grvChiDinhPTTT_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            var row = (ChiDinhADO)grvChiDinhPTTT.GetFocusedRow();
            if (row == null)
                return;
            if (grvChiDinhPTTT.GetFocusedRowCellValue(colMaDV) != null)
            {
                focus = grvChiDinhPTTT.FocusedRowHandle;
                // FocusedRowHandle = e.FocusedRowHandle;
                // int IDCD = 0;
                if (grvChiDinhPTTT.GetFocusedRowCellValue(colIDCD) != null)
                    _idcd = Convert.ToInt32(grvChiDinhPTTT.GetFocusedRowCellValue(colIDCD));
                if (grvChiDinhPTTT.GetFocusedRowCellValue(colNgayTT) != null)
                {
                    lupNgayTH.DateTime = Convert.ToDateTime(grvChiDinhPTTT.GetFocusedRowCellValue(colNgayTT));
                }
                else
                {
                    if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "56789")
                    {
                        DateTime NgayCD = Convert.ToDateTime(grvChiDinhPTTT.GetFocusedRowCellValue(colNgayCD));
                        DateTime NgayTH1 = NgayCD.Date;
                        DateTime NgayTH = NgayTH1.AddHours(System.DateTime.Now.Hour).AddMinutes(System.DateTime.Now.Minute).AddSeconds(System.DateTime.Now.Second);
                        lupNgayTH.DateTime = NgayTH;
                    }

                }
                LupCanBo.EditValue = row.MaCBth;
                if (grvChiDinhPTTT.GetFocusedRowCellValue(colStatus) != null)
                    status = Convert.ToInt32(grvChiDinhPTTT.GetFocusedRowCellValue(colStatus));
                else
                    status = 0;
                if (grvChiDinhPTTT.GetFocusedRowCellValue(colMaKPth) != null)
                {
                    string makp = Convert.ToString(DungChung.Bien.MaKP);
                    int _makpth = Convert.ToInt32(grvChiDinhPTTT.GetFocusedRowCellValue(colMaKPth));
                    if (DungChung.Bien.MaBV == "27194")
                    {
                        var _lscb = _Data.CanBoes.Where(p => p.MaKPsd.Contains(makp) && p.MaCB == DungChung.Bien.MaCB).ToList();
                        List<string> CB = _lscb.First().MaKPsd.Split(';').ToList();
                        foreach (var ab in CB)
                        {
                            LSCCB item = new LSCCB();
                            item.MKPSD = ab.ToString();
                            _lst.Add(item);
                            if (!string.IsNullOrEmpty(item.MKPSD))
                            {
                                if (DungChung.Bien.MaKP == Convert.ToInt32(item.MKPSD) || DungChung.Bien.MaKP == _makpth)
                                {
                                    btnPhauThuat.Enabled = true;
                                    break;
                                }
                                else
                                    btnPhauThuat.Enabled = false;
                            }
                        }
                    }
                    else
                    {
                        if (DungChung.Bien.MaKP == _makpth)
                        {
                            btnPhauThuat.Enabled = true;
                        }
                        else
                        {
                            btnPhauThuat.Enabled = false;
                        }
                    }
                    //if (DungChung.Bien.CapDo == 9)
                    if (DungChung.Bien.PLoaiKP == DungChung.Bien.st_PhanLoaiKP.Admin)
                        btnPhauThuat.Enabled = true;
                }
                if (status == 1)
                {
                    var dthuoctc = _Data.DThuoccts.Where(p => p.IDCD == _idcd).ToList();
                    if (dthuoctc.Count() > 0)
                    {
                        txtTyLeTT.Text = dthuoctc.First().TyLeTT.ToString();
                    }
                }
                else
                    txtTyLeTT.Text = "100";
                bool _tamthu = true;
                int _idcls = 0;
                if (grvChiDinhPTTT.GetFocusedRowCellValue(colIdCLS) != null)
                    _idcls = Convert.ToInt32(grvChiDinhPTTT.GetFocusedRowCellValue(colIdCLS));
                var bn = _Data.BenhNhans.Where(x => x.MaBNhan == _mabn).FirstOrDefault();
                _tamthu = bn.DTuong == "Dịch vụ" || bn.IDDTBN == 2 ? DungChung.Ham._checkTamThu(_Data, _mabn, _idcls) : true;
                if (status == 1 || _tamthu == false)
                {
                    btnPhauThuat.Text = "Hủy TT-PT";

                    _EnableControl(true);
                    if (_tamthu == false)
                    {
                        btnSaoTT.Enabled = false;
                        btnPhauThuat.Enabled = false;
                    }
                    else if (status == 1)
                    {
                        btnSaoTT.Enabled = true;
                        btnPhauThuat.Enabled = true;
                    }
                    simpleButton1.Enabled = false;

                }
                else
                {
                    btnPhauThuat.Text = "Thực hiện TT-PT";
                    _EnableControl(false);
                    simpleButton1.Enabled = true;
                }

                if (grvChiDinhPTTT.GetFocusedRowCellValue(colICD9) != null)
                    lupMaICD.EditValue = Convert.ToInt32(grvChiDinhPTTT.GetFocusedRowCellValue(colICD9).ToString());
                else
                    lupMaICD.EditValue = 0;
                if (grvChiDinhPTTT.GetFocusedRowCellValue(colKL) != null)
                    memoKL.Text = grvChiDinhPTTT.GetFocusedRowCellValue(colKL).ToString();
                else
                    memoKL.Text = "";


                if (grvChiDinhPTTT.GetFocusedRowCellValue(colLoiDan) != null)
                    memo_PPVC.Text = grvChiDinhPTTT.GetFocusedRowCellValue(colLoiDan).ToString();
                else
                    memo_PPVC.Text = "";
                if (grvChiDinhPTTT.GetFocusedRowCellValue(colKetQua) != null)
                    memoKetQua.Text = grvChiDinhPTTT.GetFocusedRowCellValue(colKetQua).ToString();
                else
                    memoKetQua.Text = "";

                var chiDinh = _Data.ChiDinhs.FirstOrDefault(o => o.IDCD == _idcd);
                if (chiDinh != null)
                {
                    txtTuThe.Text = chiDinh.Mau_Lan_MTruongXN;
                    memoTienMe.Text = chiDinh.MoTa;
                    memoTacDung.Text = chiDinh.GhiChu;

                    dtpNgayBatDau.DateTime = chiDinh.NgayBDTH ?? (DateTime)_Data.CLS.Where(p => p.IdCLS == _idcls).First().NgayThang;
                    if (status == 0)
                    {
                        if (DungChung.Bien.MaBV == "24012")
                        {
                            lupNgayTH.DateTime = dtpNgayBatDau.DateTime.AddMinutes(20);
                        }
                        else
                        {
                            if (DungChung.Bien.MaBV == "27001")
                            {
                                lupNgayTH.DateTime = DateTime.Now;
                            }
                            else
                            {
                                lupNgayTH.DateTime = dtpNgayBatDau.DateTime.AddMinutes(10);
                            }
                        }
                    }
                }

                fillImage();
                if (grvChiDinhPTTT.GetFocusedRowCellValue(colDSCBTH) != null)
                {
                    string[] arrCB = grvChiDinhPTTT.GetFocusedRowCellValue(colDSCBTH).ToString().Split(';');
                    //string[] arrCB = QLBV_Library.QLBV_Ham.LayChuoi(';', grvChiDinhPTTT.GetFocusedRowCellValue(colDSCBTH).ToString());
                    string[] arrMaCB = new string[8] { "", "", "", "", "", "", "", "" };
                    for (int i = 0; i < arrCB.Count(); i++)
                    {
                        string a = arrCB[i];
                        var macb = _Data.CanBoes.Where(p => p.TenCB == a && p.Status == 1).ToList();
                        if (macb.Count > 0)
                        {
                            arrMaCB[i] = macb.First().MaCB.ToString();
                        }
                    }
                    for (int i = 0; i < arrCB.Count(); i++)
                    {
                        switch (i)
                        {
                            case 0:
                                txtPTVPhu.EditValue = arrMaCB[i];
                                break;
                            case 1:
                                txtGMChinh.EditValue = arrMaCB[i];
                                break;
                            case 2:
                                txtGMPhu.EditValue = arrMaCB[i];
                                break;
                            case 3:
                                txtGiupViec.Text = arrCB[i];
                                break;
                            case 4:
                                txtPTVPhu2.EditValue = arrMaCB[i];
                                break;
                            case 5:
                                txtPTVPhu3.EditValue = arrMaCB[i];
                                break;
                            case 6:
                                txtGMPhu2.EditValue = arrMaCB[i];
                                break;
                            case 7:
                                txtGMPhu3.EditValue = arrMaCB[i];
                                break;
                            default:
                                break;
                        }
                    }
                }
                else
                {
                    txtPTVPhu.ResetText();
                    txtGMChinh.ResetText();
                    txtGMPhu.ResetText();
                    txtGiupViec.ResetText();
                    memoKL.ResetText();
                    lupMaICD.EditValue = 0;
                    memo_PPVC.ResetText();
                    memoKetQua.ResetText();
                    txtPTVPhu3.ResetText();
                    txtPTVPhu2.ResetText();
                    txtGMPhu2.ResetText();
                    txtGMPhu3.ResetText();
                    txtTuThe.ResetText();
                    memoTienMe.ResetText();
                    memoTacDung.ResetText();
                }
            }
            else
            {
                if (!loadsinger)
                    _idcd = 0;
                LupCanBo.EditValue = DungChung.Bien.MaCB;
                lupNgayTH.DateTime = lupNgayTH.DateTime;
            }
        }

        private void grvChiDinhPTTT_DataSourceChanged(object sender, EventArgs e)
        {
            grvChiDinhPTTT_FocusedRowChanged(null, null);
        }

        private void btnCPKem_Click(object sender, EventArgs e)
        {

        }

        private void btnChiPhikem_Click(object sender, EventArgs e)
        {
            if (rvien != null)
            {
                MessageBox.Show("Bệnh nhân đã ra viện!");
                return;
            }
            int idcd = 0;
            if (grvChiDinhPTTT.GetFocusedRowCellValue(colIDCD) != null)
                idcd = Convert.ToInt32(grvChiDinhPTTT.GetFocusedRowCellValue(colIDCD));


            frm_kedon frm = new frm_kedon(_mabn, idcd, makpth, false);
            frm.ShowDialog();
        }

        private void btnSaoTT_Click(object sender, EventArgs e)
        {
            if (rvien != null)
            {
                MessageBox.Show("Bệnh nhân đã ra viện!");
                return;
            }
            int idcd = 0;
            if (grvChiDinhPTTT.GetFocusedRowCellValue(colIDCD) != null)
                idcd = Convert.ToInt32(grvChiDinhPTTT.GetFocusedRowCellValue(colIDCD));

            if (status == 1)
            {
                TTluu = 2;
                btnPhauThuat.Text = "Thực hiện TT-PT";
                _EnableControl(false);
            }
        }

        private void btnKetQuaMau_Click(object sender, EventArgs e)
        {
            int _madv = 0;
            if (grvChiDinhPTTT.GetFocusedRowCellValue(colMaDV) != null)
                _madv = Convert.ToInt32(grvChiDinhPTTT.GetFocusedRowCellValue(colMaDV));
            ChucNang.frm_KQmau frm = new ChucNang.frm_KQmau(_madv, "TTPT");
            frm.GetData = new ChucNang.frm_KQmau._getstring(_getValuesa);
            frm.ShowDialog();
        }
        public void _getValuesa(string gt1, string gt2, string gt3)
        {


            memoKL.Text = gt2;
            memo_PPVC.Text = gt3;
            memoKetQua.Text = gt1;



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

        /// <summary>
        /// đường dẫn ảnh
        /// </summary>
        string filePath = "";
        private void btnChonAnh_Click(object sender, EventArgs e)
        {
            bool tontai = true; bool kt = false;
            if (ptPhoto.Image == null)
                tontai = false;

            if (tontai)
            {
                DialogResult _dresult = MessageBox.Show("Bạn có muốn chọn lại ảnh!", "Hỏi lưu", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (_dresult == DialogResult.Yes)
                {

                    kt = true;
                }
            }
            else
            {
                kt = true;
            }
            if (kt)
            {
                string fileName = string.Empty;
                OpenFileDialog op = new OpenFileDialog();
                op.Multiselect = false;
                op.Filter = "JPEG (*.jpg)|*.jpg|BMP(*.bmp)| *.bmp";

                if (op.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    fileName = op.FileName;
                    string ex = Path.GetExtension(op.FileName);
                    if (!string.IsNullOrEmpty(fileName))
                        ptPhoto.Image = Image.FromFile(fileName);
                    else
                        ptPhoto.Image = null;
                    string _tenfileanh = DungChung.Bien.DuongDan;
                    // _tenfileanh += _mabn + "_" + IdCLS + "_" + i + ".jpg";
                    _tenfileanh += _mabn + "_" + _idcd + ex;
                    filePath = layTenFileAnh(fileName, _tenfileanh);
                }
            }


        }

        private void btnXoaAnh1_Click(object sender, EventArgs e)
        {
            filePath = "";
            ptPhoto.Image = null;
        }


        public void fillImage()
        {
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var qduongdan = (from cd in data.ChiDinhs.Where(p => p.IDCD == _idcd)
                             join clsct in data.CLScts on cd.IDCD equals clsct.IDCD
                             select clsct).FirstOrDefault();
            if (qduongdan != null)
            {
                filePath = qduongdan.DuongDan;
                if (!string.IsNullOrEmpty(filePath))
                {

                    ptPhoto.Image = Image.FromFile(filePath);
                }
                else
                    ptPhoto.Image = null;
            }
            else
                ptPhoto.Image = null;
        }
        private void btnReset_Click(object sender, EventArgs e)
        {
            fillImage();
        }

        bool _ThucHienTatCa = false;
        private void simpleButton1_Click_1(object sender, EventArgs e)
        {
            DungChung.Bien.Checkin = false;
            if ((lupMaICD.EditValue == null || Convert.ToInt32(lupMaICD.EditValue) == 0) && DungChung.Bien.MaBV == "30007")
            {
                MessageBox.Show("Chưa nhập mã ICD9");
                return;
            }
            int dem = 0;
            if (DialogResult.Yes == MessageBox.Show("Bạn muốn thực hiện tất cả dịch vụ đã chỉ định", "Thực hiện TTPT", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                if (!ktraLuu())
                    return;
                

                if (DungChung.Bien.MaBV == "24272")
                {
                    var lmadv = chiDinhADOs.Where(x => x.Check == true).Select(s => s.MaDV).ToList();

                    if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "24012")
                    {
                        if (LupCanBo.Text == null || LupCanBo.Text == "")
                        {
                            int bsth = (_Data.BenhNhans.Where(p => p.MaBNhan == _mabn).Where(x => x.DTuong == "BHYT" || x.NoThe == true)).Count();
                            if (bsth > 0)
                            {
                                MessageBox.Show("Bệnh nhân bảo hiểm nợ thẻ không thể thực hiện");
                            }
                            else
                            {
                                if (lmadv != null)
                                {
                                    foreach (var item in lmadv)
                                    {
                                        var lidcd = (from cls in _Data.CLS.Where(p => p.MaBNhan == _mabn)
                                                     join cd in _Data.ChiDinhs.Where(p => p.Status == 0 && p.MaDV == item) on cls.IdCLS equals cd.IdCLS
                                                     select new { cd.IDCD }).Distinct().ToList();
                                        _ThucHienTatCa = true;
                                        foreach (var item1 in lidcd)
                                        {
                                            if (ThucHienTTPT(item1.IDCD, lupNgayTH.DateTime))//.AddYears(-3)
                                                dem++;
                                        }
                                        _ThucHienTatCa = false;
                                    }

                                }
                                var tongcp = (from dt in _Data.DThuocs.Where(p => p.MaBNhan == _mabn)
                                              join dtct in _Data.DThuoccts.Where(p => p.TrongBH == 1) on dt.IDDon equals dtct.IDDon
                                              select new { dtct }).ToList();

                                double tongcptrbh = 0;
                                tongcptrbh = tongcp.Sum(p => p.dtct.ThanhTien);
                                if (tongcptrbh >= 10000000)
                                {
                                    MessageBox.Show("Tổng chi phí điều trị trong danh mục của bệnh nhân đã vướt quá 10.000.000");
                                }
                                MessageBox.Show("Thực hiện thành công " + dem + " TTPT");

                            }
                        }
                        else
                        {
                            if (lmadv != null)
                            {
                                foreach (var item in lmadv)
                                {
                                    var lidcd = (from cls in _Data.CLS
                                                 join cd in _Data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                                                 join dv in _Data.DichVus on cd.MaDV equals dv.MaDV
                                                 join tn in _Data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                                                 where (cls.MaBNhan == _mabn && tn.TenTN == "Thủ thuật" && cls.Status == 0)
                                                 select new
                                                 {
                                                     dv.TenDV,
                                                     cd.ChiDinh1,
                                                     cd.IDCD
                                                 }).Distinct().OrderBy(p => p.TenDV).ToList();
                                    _ThucHienTatCa = true;
                                    foreach (var item1 in lidcd)
                                    {
                                        if (ThucHienTTPT(item1.IDCD, lupNgayTH.DateTime))//.AddYears(-3)
                                            dem++;
                                    }
                                    _ThucHienTatCa = false;
                                }
                            }
                            var tongcp = (from dt in _Data.DThuocs.Where(p => p.MaBNhan == _mabn)
                                          join dtct in _Data.DThuoccts.Where(p => p.TrongBH == 1) on dt.IDDon equals dtct.IDDon
                                          select new { dtct }).ToList();

                            double tongcptrbh = 0;
                            tongcptrbh = tongcp.Sum(p => p.dtct.ThanhTien);
                            if (tongcptrbh >= 10000000)
                            {
                                MessageBox.Show("Tổng chi phí điều trị trong danh mục của bệnh nhân đã vướt quá 10.000.000");
                            }
                            MessageBox.Show("Thực hiện thành công " + dem + " TTPT");
                        }
                    }
                    else
                    {
                        List<string> listErrors = new List<string>();
                        if (lmadv != null)
                        {
                            foreach (var item in lmadv)
                            {
                                var lidcd = (from cls in _Data.CLS.Where(p => p.MaBNhan == _mabn)
                                             join cd in _Data.ChiDinhs.Where(p => p.Status == 0 && p.MaDV == item) on cls.IdCLS equals cd.IdCLS
                                             select new { cd.IDCD, cls.NgayThang }).Distinct().ToList();
                                _ThucHienTatCa = true;
                                BenhNhan benhNhan = _Data.BenhNhans.FirstOrDefault(p => p.MaBNhan == _mabn);
                                foreach (var item1 in lidcd)
                                {
                                    if (benhNhan != null && benhNhan.CapCuu != 1 && (benhNhan.IDDTBN != 1 || benhNhan.DTuong != "BHYT") && string.IsNullOrWhiteSpace(benhNhan.SThe) && (DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "24272") && !DungChung.Ham.Check_DuyetTamThu(item1.IDCD))
                                    {
                                        listErrors.Add(string.Format("Dịch vụ chỉ định ngày {0} chưa được duyệt tạm thu không thể thực hiện", item1.NgayThang.Value.ToString("dd/MM/yyyy HH:mm:ss")));
                                        continue;
                                    }
                                    if (ThucHienTTPT(item1.IDCD, lupNgayTH.DateTime))//.AddYears(-3)
                                        dem++;
                                }
                                _ThucHienTatCa = false;
                            }
                        }
                        MessageBox.Show("Thực hiện thành công " + dem + " TTPT" + (listErrors.Count > 0 ? (Environment.NewLine + string.Join(Environment.NewLine, listErrors)) : ""));
                    }
                }
                else
                {
                    var madv = _Data.ChiDinhs.Where(p => p.IDCD == _idcd).Select(p => p.MaDV).FirstOrDefault();

                    if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "24012")
                    {
                        if (LupCanBo.Text == null || LupCanBo.Text == "")
                        {
                            int bsth = (_Data.BenhNhans.Where(p => p.MaBNhan == _mabn).Where(x => x.DTuong == "BHYT" || x.NoThe == true)).Count();
                            if (bsth > 0)
                            {
                                MessageBox.Show("Bệnh nhân bảo hiểm nợ thẻ không thể thực hiện");
                            }
                            else
                            {
                                if (madv != null)
                                {
                                    var lidcd = (from cls in _Data.CLS.Where(p => p.MaBNhan == _mabn)
                                                 join cd in _Data.ChiDinhs.Where(p => p.Status == 0 && p.MaDV == madv) on cls.IdCLS equals cd.IdCLS
                                                 select new { cd.IDCD }).Distinct().ToList();
                                    _ThucHienTatCa = true;
                                    foreach (var item in lidcd)
                                    {
                                        if (ThucHienTTPT(item.IDCD, lupNgayTH.DateTime))//.AddYears(-3)
                                            dem++;
                                    }
                                    _ThucHienTatCa = false;
                                }
                                var tongcp = (from dt in _Data.DThuocs.Where(p => p.MaBNhan == _mabn)
                                              join dtct in _Data.DThuoccts.Where(p => p.TrongBH == 1) on dt.IDDon equals dtct.IDDon
                                              select new { dtct }).ToList();

                                double tongcptrbh = 0;
                                tongcptrbh = tongcp.Sum(p => p.dtct.ThanhTien);
                                if (tongcptrbh >= 10000000)
                                {
                                    MessageBox.Show("Tổng chi phí điều trị trong danh mục của bệnh nhân đã vướt quá 10.000.000");
                                }
                                MessageBox.Show("Thực hiện thành công " + dem + " TTPT");

                            }
                        }
                        else
                        {
                            if (madv != null)
                            {
                                var lidcd = (from cls in _Data.CLS
                                             join cd in _Data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                                             join dv in _Data.DichVus on cd.MaDV equals dv.MaDV
                                             join tn in _Data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                                             where (cls.MaBNhan == _mabn && tn.TenTN == "Thủ thuật" && cls.Status == 0)
                                             select new
                                             {
                                                 dv.TenDV,
                                                 cd.ChiDinh1,
                                                 cd.IDCD
                                             }).Distinct().OrderBy(p => p.TenDV).ToList();
                                _ThucHienTatCa = true;
                                foreach (var item in lidcd)
                                {
                                    if (ThucHienTTPT(item.IDCD, lupNgayTH.DateTime))//.AddYears(-3)
                                        dem++;
                                }
                                _ThucHienTatCa = false;
                            }
                            var tongcp = (from dt in _Data.DThuocs.Where(p => p.MaBNhan == _mabn)
                                          join dtct in _Data.DThuoccts.Where(p => p.TrongBH == 1) on dt.IDDon equals dtct.IDDon
                                          select new { dtct }).ToList();

                            double tongcptrbh = 0;
                            tongcptrbh = tongcp.Sum(p => p.dtct.ThanhTien);
                            if (tongcptrbh >= 10000000)
                            {
                                MessageBox.Show("Tổng chi phí điều trị trong danh mục của bệnh nhân đã vướt quá 10.000.000");
                            }
                            MessageBox.Show("Thực hiện thành công " + dem + " TTPT");
                        }
                    }
                    else
                    {
                        List<string> listErrors = new List<string>();
                        if (madv != null)
                        {
                            var lidcd = (from cls in _Data.CLS.Where(p => p.MaBNhan == _mabn)
                                         join cd in _Data.ChiDinhs.Where(p => p.Status == 0 && p.MaDV == madv) on cls.IdCLS equals cd.IdCLS
                                         select new { cd.IDCD, cls.NgayThang }).Distinct().ToList();
                            _ThucHienTatCa = true;
                            BenhNhan benhNhan = _Data.BenhNhans.FirstOrDefault(p => p.MaBNhan == _mabn);
                            foreach (var item in lidcd)
                            {
                                if (benhNhan != null && string.IsNullOrWhiteSpace(benhNhan.SThe) && (DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297") && !DungChung.Ham.Check_DuyetTamThu(item.IDCD))
                                {
                                    listErrors.Add(string.Format("Dịch vụ chỉ định ngày {0} chưa được duyệt tạm thu không thể thực hiện", item.NgayThang.Value.ToString("dd/MM/yyyy HH:mm:ss")));
                                    continue;
                                }
                                if (ThucHienTTPT(item.IDCD, lupNgayTH.DateTime))//.AddYears(-3)
                                    dem++;
                            }
                            _ThucHienTatCa = false;
                        }
                        MessageBox.Show("Thực hiện thành công " + dem + " TTPT" + (listErrors.Count > 0 ? (Environment.NewLine + string.Join(Environment.NewLine, listErrors)) : ""));
                    }
                }

            }
            frm_ThucHienPT_Load(null, null);
            if (reload != null)
                reload();
        }

        public static void _inPhieu_Glocom(QLBV_Database.QLBVEntities _Data, int _Mabn, int idcls)
        {
            frmIn frm = new frmIn();
            BaoCao.rep_PhieuPTGlocom rep = new BaoCao.rep_PhieuPTGlocom();
            var vv = _Data.VaoViens.Where(p => p.MaBNhan == _Mabn).ToList();
            if (vv.Count > 0)
            {
                rep.celSoBA.Text = vv.First().SoBA;
                rep.celNgayVV.Text = vv.First().NgayVao.Value.Day.ToString() + "/" + vv.First().NgayVao.Value.Month.ToString() + "/" + vv.First().NgayVao.Value.Year.ToString();
            }

            var par3 = (from bn in _Data.BenhNhans.Where(p => p.MaBNhan == _Mabn)
                        join cls3 in _Data.CLS on bn.MaBNhan equals cls3.MaBNhan
                        join cdcls in _Data.ChiDinhs.Where(p => p.IDCD == idcls) on cls3.IdCLS equals cdcls.IdCLS
                        join dv in _Data.DichVus on cdcls.MaDV equals dv.MaDV
                        join clsct in _Data.CLScts on cdcls.IDCD equals clsct.IDCD
                        select new { cls3.CapCuu, cls3.MaKP, NgayCD = cls3.NgayThang, NgayTH = cls3.NgayTH, cls3.MaCBth, bn.MaBNhan, bn.TenBNhan, bn.DChi, bn.GTinh, bn.Tuoi, bn.SThe, MaCBDT = cls3.MaCB, cdcls.ChiDinh1, cdcls.KetLuan, cdcls.DSCBTH }).ToList();
            var bn1 = (from a in _Data.BenhNhans.Where(p => p.MaBNhan == _Mabn) select a).ToList();
            if (bn1.Count > 0)
            {
                rep.celTenBN.Text = bn1.First().TenBNhan.ToUpper();
                rep.celTuoi.Text = DungChung.Bien.MaBV == "14018" ? DungChung.Ham.TinhTuoiBenhNhan(_Data, _Mabn) : bn1.First().Tuoi.ToString();
                rep.celNam.Text = bn1.First().GTinh == 1 ? "X" : "";
                rep.celNu.Text = bn1.First().GTinh == 0 ? "X" : "";
                rep.celNgayThang.Text = DungChung.Bien.DiaDanh + ", Ngày........tháng.......năm......";
            }

            if (par3.Count > 0)
            {
                rep.celGioPT.Text = par3.First().NgayTH == null ? "" : par3.First().NgayTH.Value.Hour.ToString() + "giờ, ngày " + par3.First().NgayTH.Value.Day.ToString() + "/" + par3.First().NgayTH.Value.Month.ToString() + "/" + par3.First().NgayTH.Value.Year.ToString();
                string macb = par3.First().MaCBth;
                var tencb = _Data.CanBoes.Where(p => p.MaCB == macb).ToList();
                if (tencb.Count > 0)
                {
                    rep.celPTVC.Text = tencb.First().TenCB;
                    rep.celPTVCKy.Text = "Họ và tên " + tencb.First().TenCB;
                }
                if (par3.First().DSCBTH != null)
                {
                    string[] arr = par3.First().DSCBTH.Split(';');
                    rep.celPTVP.Text = arr[0] + ", " + arr[4] + ", " + arr[5];
                    rep.celBSGMC.Text = arr[1];
                }
                if (par3.First().NgayTH != null)
                    rep.celNgayThang.Text = DungChung.Bien.DiaDanh + ", Ngày" + par3.First().NgayTH.Value.Day + " tháng " + par3.First().NgayTH.Value.Month + " năm " + par3.First().NgayTH.Value.Year;
            }
            rep.CreateDocument();
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();
        }

        public static void _inPhieu_Mong(QLBV_Database.QLBVEntities _Data, int _Mabn, int idcls)
        {
            frmIn frm = new frmIn();
            BaoCao.rep_PhieuPTMong rep = new BaoCao.rep_PhieuPTMong();
            var vv = _Data.VaoViens.Where(p => p.MaBNhan == _Mabn).ToList();
            if (vv.Count > 0)
            {
                rep.celSoBA.Text = vv.First().SoBA;
                rep.celNgayVV.Text = vv.First().NgayVao.Value.Day.ToString() + "/" + vv.First().NgayVao.Value.Month.ToString() + "/" + vv.First().NgayVao.Value.Year.ToString();
            }

            var par3 = (from bn in _Data.BenhNhans.Where(p => p.MaBNhan == _Mabn)
                        join cls3 in _Data.CLS on bn.MaBNhan equals cls3.MaBNhan
                        join cdcls in _Data.ChiDinhs.Where(p => p.IDCD == idcls) on cls3.IdCLS equals cdcls.IdCLS
                        join dv in _Data.DichVus on cdcls.MaDV equals dv.MaDV
                        join clsct in _Data.CLScts on cdcls.IDCD equals clsct.IDCD
                        select new { dv.TenDV, cls3.CapCuu, cls3.MaKP, NgayCD = cls3.NgayThang, NgayTH = cls3.NgayTH, cls3.MaCBth, bn.MaBNhan, bn.TenBNhan, bn.DChi, bn.GTinh, bn.Tuoi, bn.SThe, MaCBDT = cls3.MaCB, cdcls.ChiDinh1, cdcls.KetLuan, cdcls.DSCBTH }).ToList();

            var bn1 = (from a in _Data.BenhNhans.Where(p => p.MaBNhan == _Mabn) select a).ToList();
            if (bn1.Count > 0)
            {
                rep.celTenBN.Text = bn1.First().TenBNhan.ToUpper();
                rep.celTuoi.Text = DungChung.Bien.MaBV == "14018" ? DungChung.Ham.TinhTuoiBenhNhan(_Data, _Mabn) : bn1.First().Tuoi.ToString();
                rep.celNam.Text = bn1.First().GTinh == 1 ? "X" : "";
                rep.celNu.Text = bn1.First().GTinh == 0 ? "X" : "";
                rep.celNgayThang.Text = DungChung.Bien.DiaDanh + ", Ngày........tháng.......năm......";
            }

            if (par3.Count > 0)
            {
                rep.celGioPT.Text = par3.First().NgayTH == null ? "" : par3.First().NgayTH.Value.Hour.ToString() + "giờ, ngày " + par3.First().NgayTH.Value.Day.ToString() + "/" + par3.First().NgayTH.Value.Month.ToString() + "/" + par3.First().NgayTH.Value.Year.ToString();
                string macb = par3.First().MaCBth;
                var tencb = _Data.CanBoes.Where(p => p.MaCB == macb).ToList();
                if (tencb.Count > 0)
                {
                    rep.celPTVC.Text = tencb.First().TenCB;
                    rep.celPTVCKy.Text = tencb.First().TenCB;
                }
                if (par3.First().DSCBTH != null)
                {
                    string[] arr = par3.First().DSCBTH.Split(';');
                    rep.celPTVP.Text = arr[0] + ", " + arr[4] + ", " + arr[5];
                    rep.celBSGMC.Text = arr[1];
                }
                if (par3.First().NgayTH != null)
                    rep.celNgayThang.Text = DungChung.Bien.DiaDanh + ", Ngày" + par3.First().NgayTH.Value.Day + " tháng " + par3.First().NgayTH.Value.Month + " năm " + par3.First().NgayTH.Value.Year;
                rep.celCTPT.Text = par3.First().TenDV;
            }
            var cd = (from a in _Data.CLS.Where(p => p.IdCLS == idcls)
                      join b in _Data.BNKBs.Where(p => p.MaBNhan == _Mabn) on a.MaKP equals b.MaKP
                      select new { a, b }).ToList();
            if (cd.Count > 0)
            {
                rep.celChanDoan.Text = cd.First().b.ChanDoan;
            }
            rep.CreateDocument();
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();
        }

        public static void _inPhieu_MoQuam(QLBV_Database.QLBVEntities _Data, int _Mabn, int idcls)
        {
            frmIn frm = new frmIn();
            BaoCao.rep_PhieuPTMoQuam rep = new BaoCao.rep_PhieuPTMoQuam();
            var vv = _Data.VaoViens.Where(p => p.MaBNhan == _Mabn).ToList();
            if (vv.Count > 0)
            {
                rep.celSoBA.Text = vv.First().SoBA;
                rep.celNgayVV.Text = vv.First().NgayVao.Value.Day.ToString() + "/" + vv.First().NgayVao.Value.Month.ToString() + "/" + vv.First().NgayVao.Value.Year.ToString();
            }

            var par3 = (from bn in _Data.BenhNhans.Where(p => p.MaBNhan == _Mabn)
                        join cls3 in _Data.CLS on bn.MaBNhan equals cls3.MaBNhan
                        join cdcls in _Data.ChiDinhs.Where(p => p.IDCD == idcls) on cls3.IdCLS equals cdcls.IdCLS
                        join dv in _Data.DichVus on cdcls.MaDV equals dv.MaDV
                        join clsct in _Data.CLScts on cdcls.IDCD equals clsct.IDCD
                        select new { dv.TenDV, cls3.CapCuu, cls3.MaKP, NgayCD = cls3.NgayThang, NgayTH = cls3.NgayTH, cls3.MaCBth, bn.MaBNhan, bn.TenBNhan, bn.DChi, bn.GTinh, bn.Tuoi, bn.SThe, MaCBDT = cls3.MaCB, cdcls.ChiDinh1, cdcls.KetLuan, cdcls.DSCBTH }).ToList();
            var bn1 = (from a in _Data.BenhNhans.Where(p => p.MaBNhan == _Mabn) select a).ToList();
            if (bn1.Count > 0)
            {
                rep.celTenBN.Text = bn1.First().TenBNhan.ToUpper();
                rep.celTuoi.Text = DungChung.Bien.MaBV == "14018" ? DungChung.Ham.TinhTuoiBenhNhan(_Data, _Mabn) : bn1.First().Tuoi.ToString();
                rep.celNam.Text = bn1.First().GTinh == 1 ? "X" : "";
                rep.celNu.Text = bn1.First().GTinh == 0 ? "X" : "";
                rep.celNgayThang.Text = DungChung.Bien.DiaDanh + ", Ngày........tháng.......năm......";
            }

            if (par3.Count > 0)
            {
                rep.celGioPT.Text = par3.First().NgayTH == null ? "" : par3.First().NgayTH.Value.Hour.ToString() + "giờ, ngày " + par3.First().NgayTH.Value.Day.ToString() + "/" + par3.First().NgayTH.Value.Month.ToString() + "/" + par3.First().NgayTH.Value.Year.ToString();
                string macb = par3.First().MaCBth;
                var tencb = _Data.CanBoes.Where(p => p.MaCB == macb).ToList();
                if (tencb.Count > 0)
                {
                    rep.celPTVC.Text = tencb.First().TenCB;
                    rep.celPTVCKy.Text = "Họ và tên " + tencb.First().TenCB;
                }
                if (par3.First().DSCBTH != null)
                {
                    string[] arr = par3.First().DSCBTH.Split(';');
                    rep.celPTVP.Text = arr[0] + ", " + arr[4] + ", " + arr[5];
                    rep.celBSGMC.Text = arr[1];
                }
                if (par3.First().NgayTH != null)
                    rep.celNgayThang.Text = DungChung.Bien.DiaDanh + ", Ngày" + par3.First().NgayTH.Value.Day + " tháng " + par3.First().NgayTH.Value.Month + " năm " + par3.First().NgayTH.Value.Year;
                rep.celCDPT.Text = par3.First().TenDV;
            }
            var cd = (from a in _Data.CLS.Where(p => p.IdCLS == idcls)
                      join b in _Data.BNKBs.Where(p => p.MaBNhan == _Mabn) on a.MaKP equals b.MaKP
                      select new { a, b }).ToList();
            if (cd.Count > 0)
            {
                rep.celChanDoan.Text = cd.First().b.ChanDoan;
            }
            rep.CreateDocument();
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();
        }

        public static void _inPhieu_ThuyTinh(QLBV_Database.QLBVEntities _Data, int _Mabn, int idcls)
        {
            frmIn frm = new frmIn();
            BaoCao.rep_PhieuPTThethuyTinh rep = new BaoCao.rep_PhieuPTThethuyTinh();
            var vv = _Data.VaoViens.Where(p => p.MaBNhan == _Mabn).ToList();
            if (vv.Count > 0)
            {
                rep.celSoBA.Text = vv.First().SoBA;
                rep.celNgayVV.Text = vv.First().NgayVao.Value.Day.ToString() + "/" + vv.First().NgayVao.Value.Month.ToString() + "/" + vv.First().NgayVao.Value.Year.ToString();
            }

            var par3 = (from bn in _Data.BenhNhans.Where(p => p.MaBNhan == _Mabn)
                        join cls3 in _Data.CLS on bn.MaBNhan equals cls3.MaBNhan
                        join cdcls in _Data.ChiDinhs.Where(p => p.IDCD == idcls) on cls3.IdCLS equals cdcls.IdCLS
                        join dv in _Data.DichVus on cdcls.MaDV equals dv.MaDV
                        join clsct in _Data.CLScts on cdcls.IDCD equals clsct.IDCD
                        select new { dv.TenDV, cls3.CapCuu, cls3.MaKP, NgayCD = cls3.NgayThang, NgayTH = cls3.NgayTH, cls3.MaCBth, bn.MaBNhan, bn.TenBNhan, bn.DChi, bn.GTinh, bn.Tuoi, bn.SThe, MaCBDT = cls3.MaCB, cdcls.ChiDinh1, cdcls.KetLuan, cdcls.DSCBTH }).ToList();

            var bn1 = (from a in _Data.BenhNhans.Where(p => p.MaBNhan == _Mabn) select a).ToList();
            if (bn1.Count > 0)
            {
                rep.celTenBN.Text = bn1.First().TenBNhan.ToUpper();
                rep.celTuoi.Text = DungChung.Bien.MaBV == "14018" ? DungChung.Ham.TinhTuoiBenhNhan(_Data, _Mabn) : bn1.First().Tuoi.ToString();
                rep.celNam.Text = bn1.First().GTinh == 1 ? "X" : "";
                rep.celNu.Text = bn1.First().GTinh == 0 ? "X" : "";
                rep.celNgayThang.Text = DungChung.Bien.DiaDanh + ", Ngày........tháng.......năm......";
            }

            if (par3.Count > 0)
            {
                rep.celGioPT.Text = par3.First().NgayTH == null ? "" : par3.First().NgayTH.Value.Hour.ToString() + "giờ, ngày " + par3.First().NgayTH.Value.Day.ToString() + "/" + par3.First().NgayTH.Value.Month.ToString() + "/" + par3.First().NgayTH.Value.Year.ToString();
                string macb = par3.First().MaCBth;
                var tencb = _Data.CanBoes.Where(p => p.MaCB == macb).ToList();
                if (tencb.Count > 0)
                {
                    rep.celPTVC.Text = tencb.First().TenCB;
                    rep.celPTVCKy.Text = tencb.First().TenCB;
                }
                if (par3.First().DSCBTH != null)
                {
                    string[] arr = par3.First().DSCBTH.Split(';');
                    rep.celPTVP.Text = arr[0] + ", " + arr[4] + ", " + arr[5];
                    rep.celBSGMC.Text = arr[1];
                }
                if (par3.First().NgayTH != null)
                    rep.celNgayThang.Text = DungChung.Bien.DiaDanh + ", Ngày" + par3.First().NgayTH.Value.Day + " tháng " + par3.First().NgayTH.Value.Month + " năm " + par3.First().NgayTH.Value.Year;
            }
            rep.CreateDocument();
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();
        }

        public static void _inPhieu_TTT_CatBe(QLBV_Database.QLBVEntities _Data, int _Mabn, int idcls)
        {
            frmIn frm = new frmIn();
            BaoCao.rep_PhieuPTTTT_CatBe rep = new BaoCao.rep_PhieuPTTTT_CatBe();
            var vv = _Data.VaoViens.Where(p => p.MaBNhan == _Mabn).ToList();
            if (vv.Count > 0)
            {
                rep.celSoBA.Text = vv.First().SoBA;
                rep.celNgayVV.Text = vv.First().NgayVao.Value.Day.ToString() + "/" + vv.First().NgayVao.Value.Month.ToString() + "/" + vv.First().NgayVao.Value.Year.ToString();
            }

            var par3 = (from bn in _Data.BenhNhans.Where(p => p.MaBNhan == _Mabn)
                        join cls3 in _Data.CLS on bn.MaBNhan equals cls3.MaBNhan
                        join cdcls in _Data.ChiDinhs.Where(p => p.IDCD == idcls) on cls3.IdCLS equals cdcls.IdCLS
                        join dv in _Data.DichVus on cdcls.MaDV equals dv.MaDV
                        join clsct in _Data.CLScts on cdcls.IDCD equals clsct.IDCD
                        select new { dv.TenDV, cls3.CapCuu, cls3.MaKP, NgayCD = cls3.NgayThang, NgayTH = cls3.NgayTH, cls3.MaCBth, bn.MaBNhan, bn.TenBNhan, bn.DChi, bn.GTinh, bn.Tuoi, bn.SThe, MaCBDT = cls3.MaCB, cdcls.ChiDinh1, cdcls.KetLuan, cdcls.DSCBTH }).ToList();

            var bn1 = (from a in _Data.BenhNhans.Where(p => p.MaBNhan == _Mabn) select a).ToList();
            if (bn1.Count > 0)
            {
                rep.celTenBN.Text = bn1.First().TenBNhan.ToUpper();
                rep.celTuoi.Text = DungChung.Bien.MaBV == "14018" ? DungChung.Ham.TinhTuoiBenhNhan(_Data, _Mabn) : bn1.First().Tuoi.ToString();
                rep.celNam.Text = bn1.First().GTinh == 1 ? "X" : "";
                rep.celNu.Text = bn1.First().GTinh == 0 ? "X" : "";
                rep.celNgayThang.Text = DungChung.Bien.DiaDanh + ", Ngày........tháng.......năm......";
            }

            if (par3.Count > 0)
            {
                rep.celGioPT.Text = par3.First().NgayTH == null ? "" : par3.First().NgayTH.Value.Hour.ToString() + "giờ, ngày " + par3.First().NgayTH.Value.Day.ToString() + "/" + par3.First().NgayTH.Value.Month.ToString() + "/" + par3.First().NgayTH.Value.Year.ToString();
                string macb = par3.First().MaCBth;
                var tencb = _Data.CanBoes.Where(p => p.MaCB == macb).ToList();
                if (tencb.Count > 0)
                {
                    rep.celPTVC.Text = tencb.First().TenCB;
                    rep.celPTVCKy.Text = "Họ và tên " + tencb.First().TenCB;
                }
                if (par3.First().DSCBTH != null)
                {
                    string[] arr = par3.First().DSCBTH.Split(';');
                    rep.celPTVP.Text = arr[0] + ", " + arr[4] + ", " + arr[5];
                    rep.celBSGMC.Text = arr[1];
                }
                if (par3.First().NgayTH != null)
                    rep.celNgayThang.Text = DungChung.Bien.DiaDanh + ", Ngày" + par3.First().NgayTH.Value.Day + " tháng " + par3.First().NgayTH.Value.Month + " năm " + par3.First().NgayTH.Value.Year;
                rep.celCDPT.Text = par3.First().TenDV;
            }
            var cd = (from a in _Data.CLS.Where(p => p.IdCLS == idcls)
                      join b in _Data.BNKBs.Where(p => p.MaBNhan == _Mabn) on a.MaKP equals b.MaKP
                      select new { a, b }).ToList();
            //if (cd.Count > 0)
            //{
            //    rep.celChanDoan.Text = cd.First().b.ChanDoan;
            //}
            rep.CreateDocument();
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();
        }



        private void lupMaICD_EditValueChanged(object sender, EventArgs e)
        {
            int icd9 = 0;
            if (lupMaICD.EditValue != null)
            {
                icd9 = Convert.ToInt32(lupMaICD.EditValue);
                QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                var qicd9 = data.ICD9.Where(p => p.ID == icd9).FirstOrDefault();
                if (qicd9 != null)
                    memoKL.Text = qicd9.TenPTTT;
                else
                    memoKL.Text = "";

            }
        }

        private void btnTimKiemICD_Click(object sender, EventArgs e)
        {
            FormThamSo.frm_TimKiemICD9 frm = new FormThamSo.frm_TimKiemICD9();
            frm.GetData = new FormThamSo.frm_TimKiemICD9._getstring(getICD);
            frm.ShowDialog();
        }

        private void getICD(int ID)
        {
            lupMaICD.EditValue = ID;
        }

        private void btnHCVTYT_Click(object sender, EventArgs e)
        {
            if (rvien != null)
            {
                MessageBox.Show("Bệnh nhân đã ra viện!");
                return;
            }
            int idcd = 0;
            if (grvChiDinhPTTT.GetFocusedRowCellValue(colIDCD) != null)
                idcd = Convert.ToInt32(grvChiDinhPTTT.GetFocusedRowCellValue(colIDCD));
            frm_kedon frm = new frm_kedon(_mabn, idcd, makpth, true, "");
            frm.ShowDialog();
        }

        private void grvChiDinhPTTT_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            var row = (ChiDinhADO)grvChiDinhPTTT.GetRow(e.RowHandle);
            if (row != null)
            {
                if (e.Column.FieldName == "Check")
                    if (DungChung.Bien.MaBV != "24012")
                    {
                        e.RepositoryItem = row.Status == 1 ? repositoryItemCheckEdit_Disable : repositoryItemCheckEdit_Enable;
                    }

            }
        }

        private void LoadData(List<ChiDinhADO> dt, string searchKey, DevExpress.XtraGrid.GridControl gridControl, int status)
        {
            int _int_maBN = 0;
            if (!string.IsNullOrEmpty(searchKey))
            {
                searchKey.ToLower();
                int rs;
                if (Int32.TryParse(searchKey, out rs))
                    _int_maBN = Convert.ToInt32(searchKey);
            }
            var result = dt.Where(p => p.MaDV == _int_maBN).OrderByDescending(o => o.TenDV).ToList();
            if (result.Count() > 0)
            {
                gridControl.DataSource = result.Where(p => p.Status == status);
                if (DungChung.Bien.MaBV == "24272")
                {
                    gridControl.DataSource = result.Where(p => p.Status == status);
                }
            }
            else
            {
                result = dt.Where(p => p.TenDV.ToLower().Contains(searchKey.ToLower())).OrderByDescending(o => o.TenDV).ToList();
                if (result.Count() > 0)
                {
                    gridControl.DataSource = result.Where(p => p.Status == status);
                    if (DungChung.Bien.MaBV == "24272")
                    {
                        gridControl.DataSource = result.Where(p => p.Status == status).Where(c => c.Check == true);
                    }
                }
                else
                {
                    gridControl.DataSource = null;
                }
            }
        }
        private List<ChiDinhADO> chiDinhADOs = new List<ChiDinhADO>();

        private class ChiDinhADO
        {
            public bool Check { get; set; }
            public int? Status { get; set; }
            public string MaCB { get; set; }
            public DateTime? NgayThang { get; set; }
            public string KetQua { get; set; }
            public string LoiDan { get; set; }
            public string DSCBTH { get; set; }
            public string KetLuan { get; set; }
            public int? IdCLS { get; set; }
            public int? MaKP { get; set; }
            public int? MaKPth { get; set; }
            public DateTime? NgayTH { get; set; }
            public int? IDCD { get; set; }
            public int? MaDV { get; set; }
            public string MaCBth { get; set; }
            public string TenDV { get; set; }
            public string ChiDinh { get; set; }
            public int? ICD9 { get; set; }
        }

        private void lupNgayTH_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void dtpNgayBatDau_EditValueChanged(object sender, EventArgs e)
        {

        }
        private void LupCanBo_EditValueChanged(object sender, EventArgs e)
        {
            
        }
        private void txtPTVPhu_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void txtGMChinh_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void memoKetQua_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void grcChiDinhPTTT_Click(object sender, EventArgs e)
        {

        }

        private void panelControl2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void grvChiDinhPTTT_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            int _MaDV = grvChiDinhPTTT.GetFocusedRowCellValue(colMaDV) == null ? 0 : Convert.ToInt32(grvChiDinhPTTT.GetFocusedRowCellValue(colMaDV));
            //if (DungChung.Bien.MaBV == "24009")
            //{
            //    var _GioiHan = ((C_DichVu)grvDichvu.GetRow(e.RowHandle)).GHCDTrongNgay;
            //    var now = DateTime.Now.Date;
            //    if (_GioiHan != null)
            //    {
            //        var _chiDinhDV = (from cls in _data.CLS
            //                          join cd in _data.ChiDinhs.Where(o => o.MaDV == _MaDV) on cls.IdCLS equals cd.IdCLS
            //                          select cls).ToList();
            //        var _chiDinhTrongNgay = _chiDinhDV.Where(o => o.NgayThang.Value.Date == DateTime.Now.Date).ToList();
            //        if (_chiDinhTrongNgay.Count >= _GioiHan)
            //        {
            //            MessageBox.Show("Dịch vụ đã quá mức giới hạn chỉ định trong ngày");
            //        }
            //    }
            //}

            string _T = grvChiDinhPTTT.GetFocusedRowCellValue(colTenDV).ToString();

            if (chiDinhADOs.Where(p => p.MaDV == _MaDV).Count() > 0)
            {
                ChiDinhADO sua = chiDinhADOs.Where(p => p.MaDV == _MaDV).First();

                if (e.Column == Chon1) // Nếu click vào cột Chọn  
                {
                    //mmYlenh.Text = "";
                    if (sua.Check == false)
                    {
                        sua.Check = true;
                        //colXHH.OptionsColumn.ReadOnly = false;
                    }
                    else if (sua.Check == true)
                    {
                        sua.Check = false;
                    }
                    //sua.YLenh = mmYlenh.Text;
                    //sua.SoTT = 1;
                    //sua.Trangthai = 0;
                }

            }
            //grvChiDinhPTTT.DataSource = null;
            //grcCLS.DataSource = _listDichVu.Where(p => p.Chon == true).ToList();
        }

        private void grvChiDinhPTTT_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            int _MaDV = grvChiDinhPTTT.GetFocusedRowCellValue(colMaDV) == null ? 0 : Convert.ToInt32(grvChiDinhPTTT.GetFocusedRowCellValue(colMaDV));
            //if (DungChung.Bien.MaBV == "24009")
            //{
            //    var _GioiHan = ((C_DichVu)grvDichvu.GetRow(e.RowHandle)).GHCDTrongNgay;
            //    var now = DateTime.Now.Date;
            //    if (_GioiHan != null)
            //    {
            //        var _chiDinhDV = (from cls in _data.CLS
            //                          join cd in _data.ChiDinhs.Where(o => o.MaDV == _MaDV) on cls.IdCLS equals cd.IdCLS
            //                          select cls).ToList();
            //        var _chiDinhTrongNgay = _chiDinhDV.Where(o => o.NgayThang.Value.Date == DateTime.Now.Date).ToList();
            //        if (_chiDinhTrongNgay.Count >= _GioiHan)
            //        {
            //            MessageBox.Show("Dịch vụ đã quá mức giới hạn chỉ định trong ngày");
            //        }
            //    }
            //}


            if (chiDinhADOs.Where(p => p.MaDV == _MaDV).Count() > 0)
            {
                ChiDinhADO sua = chiDinhADOs.Where(p => p.MaDV == _MaDV).First();

                if (e.Column == Chon1) // Nếu click vào cột Chọn  
                {
                    //mmYlenh.Text = "";
                    if (sua.Check == false)
                    {
                        sua.Check = false;
                        //colXHH.OptionsColumn.ReadOnly = false;
                    }
                    else if (sua.Check == true)
                    {
                        sua.Check = true;
                    }
                    //sua.YLenh = mmYlenh.Text;
                    //sua.SoTT = 1;
                    //sua.Trangthai = 0;
                }

            }
        }
    }
}