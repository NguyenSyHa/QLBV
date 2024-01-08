using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;

namespace QLBV.ChucNang
{
    public partial class frm_NhapNghiOm : DevExpress.XtraEditors.XtraForm
    {
        public frm_NhapNghiOm()
        {
            InitializeComponent();
        }
        int IDKB = 0;
        string x1 = "", x2 = "", x3 = "";
        QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        public frm_NhapNghiOm(int idKB)
        {
            InitializeComponent();
            IDKB = idKB;
        }

        private void ResetControl()
        {
            txtHoTen.ResetText();
            txtNgaySinh.ResetText();
            txtDonviCT.ResetText();
            memoGhiChu.ResetText();
            txtTenBo.ResetText();
            txtTenMe.ResetText();
            txtSoNgayNghi.ResetText();
            txtSoChungNhan.ResetText();
            txtGhiChuNghiThaiSan.ResetText();
            checkBox1.Checked = false;
            dateNgayNghi.EditValue = null;
            dateNgayHen.EditValue = null;
            txtSoGanNhat.ResetText();
            txtSoCT.ResetText();
            txtSoTheBHXH.ResetText();
            txtSoThe.ResetText();
            lupCBDaiDien.EditValue = null;
            dtNgayChungNhan.EditValue = null;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            // khi xóa ngày hẹn và ngày nghỉ phải set = null
            DialogResult rs = MessageBox.Show("Bạn có chắc chắn muốn xóa!", "Hỏi xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (rs == DialogResult.Yes)
            {
                var q = _data.BNKBs.Where(p => p.IDKB == IDKB).ToList();
                if (q.Count > 0)
                {
                    if (!string.IsNullOrEmpty(q.First().SoNghiOm))
                    {
                        string snghiom = q.First().SoNghiOm;
                        int thang = 0;// = Convert.ToInt32(snghiom.Substring(5, 2));
                        int nam = 0;//Convert.ToInt32(snghiom.Substring(8, snghiom.Length));
                        int so = 0;
                        if (DungChung.Bien.MaBV == "27022" && q.FirstOrDefault().SoNghiOm != null && q.FirstOrDefault().SoNghiOm.Length > 8)
                        {
                            thang = Convert.ToInt32(snghiom.Substring(5, 2));
                            nam = Convert.ToInt32(snghiom.Substring(8));
                            string abc = snghiom.Substring(0, 4);
                            so = Convert.ToInt32(abc);
                        }
                        if (DungChung.Bien.MaBV == "27001" && q.FirstOrDefault().SoNghiOm != null && q.FirstOrDefault().SoNghiOm.Length > 6)
                        {
                            if (q.First().NgayChungNhanNghiOm != null)
                            {
                                nam = q.First().NgayChungNhanNghiOm.Value.Year;
                                string abc = snghiom.Substring(6);
                                so = Convert.ToInt32(abc);
                            }
                        }
                        else if ((DungChung.Bien.MaBV.Substring(0, 2) == "30" && DungChung.Bien.MaBV != "30007") && q.FirstOrDefault().SoNghiOm != null && q.FirstOrDefault().SoNghiOm.Length > 6)
                        {
                            string abc = snghiom.Substring(9);
                            so = Convert.ToInt32(abc);
                            nam = Convert.ToInt32("20" + snghiom.Substring(6, 2));
                        }
                        else if ((DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049") && q.FirstOrDefault().SoNghiOm != null && q.FirstOrDefault().SoNghiOm.Length > 9)
                        {
                            nam = Convert.ToInt32("20" + snghiom.Substring(2, 2));
                            thang = Convert.ToInt32(snghiom.Substring(0, 2));
                            so = Convert.ToInt32(snghiom.Substring(9));
                        }
                        else
                        {

                            // so = Convert.ToInt32(q.FirstOrDefault().SoNghiOm.Substring(4));
                        }
                        if (DungChung.Bien.MaBV == "27022" || DungChung.Bien.MaBV == "27001" || (DungChung.Bien.MaBV.Substring(0, 2) == "30" && DungChung.Bien.MaBV != "30007"))
                        {
                            var sopl = _data.SoPLs.Where(p => p.PhanLoai == 10 && p.SoPL1 == so).ToList();
                            if (sopl.Count > 0)
                            {
                                int idMax = sopl.Max(p => p.IdSoPL);
                                SoPL sua = _data.SoPLs.Single(p => p.IdSoPL == idMax);
                                if (sua.SoPL1 == so && so > 0)
                                {
                                    DateTime? dt = sua.NgayNhap;
                                    _data.SoPLs.Remove(sua);
                                    _data.SaveChanges();
                                    SoPL moi = new SoPL();
                                    moi.PhanLoai = 10;
                                    moi.MaKP = 0;
                                    moi.SoPL1 = so - 1;
                                    moi.Status = 1;
                                    moi.NoiTru = -1;
                                    moi.NgayNhap = dt;
                                    _data.SoPLs.Add(moi);
                                    _data.SaveChanges();

                                }
                            }
                        }
                        else
                        {
                            var soom = _data.SoPLs.Where(p => p.PhanLoai == 10 && p.SoPL1 == so && p.NgayNhap.Value.Month == thang && p.NgayNhap.Value.Year == nam).ToList();
                            if (soom.Count > 0)
                            {
                                var sopl = _data.SoPLs.Where(p => p.PhanLoai == 10 && p.SoPL1 == so && p.NgayNhap.Value.Month == thang && p.NgayNhap.Value.Year == nam).ToList();
                                foreach (var a in sopl)
                                {
                                    SoPL xoa = _data.SoPLs.Single(p => p.IdSoPL == a.IdSoPL);
                                    _data.SoPLs.Remove(xoa);
                                }

                            }
                        }
                    }
                    var bnkb = q.First();
                    bnkb.GhiChu = "";
                    bnkb.NghiOmDen = null;
                    bnkb.NghiOmTu = null;
                    bnkb.SoNghiOm = null;
                    bnkb.NgayChungNhanNghiOm = null;
                    bnkb.NguoiDaiDien = null;
                    bnkb.LyDoNghiThaiSan = null;

                    var ttbx = _data.TTboXungs.Where(p => p.MaBNhan == bnkb.MaBNhan).FirstOrDefault();
                    ttbx.So_eTBM = null;
                    ttbx.NThan = null;
                    ttbx.NoiLV = null;
                    if (_data.SaveChanges() > 0)
                    {
                        MessageBox.Show("Xóa thành công!");
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Xóa thất bại!");
                    }
                }
            }
        }
        string _songhiom = "";
        string _soct = "";
        public class sonm
        {
            int iDKB;

            public int IDKB
            {
                get { return iDKB; }
                set { iDKB = value; }
            }
            long soNghiOm;

            public long SoNghiOm
            {
                get { return soNghiOm; }
                set { soNghiOm = value; }
            }
        }
        List<sonm> somax = new List<sonm>();
        string Dtuong = "";
        int _soctint = 0;
        int _mabn = 0;
        private void frm_NhapNghiOm_Load(object sender, EventArgs e)
        {
            _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            dtNgayChungNhan.DateTime = DateTime.Now;
            if (DungChung.Bien.MaBV == "30007")
                this.txtSoChungNhan.Properties.MaxLength = 15;
            if (DungChung.Bien.MaBV == "27183")
                checkBox1.Enabled = true;
            if (DungChung.Bien.MaBV == "27001")
                txtSoCT.Enabled = false;
            var dscb = _data.CanBoes.Where(p => p.Status == 1).Where(p => p.ChucVu.ToLower().Contains("gđ") || p.ChucVu.ToLower().Contains("giám đốc") || p.ChucVu.ToLower().Contains("pgđ") || p.ChucVu.ToLower().Contains("phó giám đốc")).ToList();
            lupCBDaiDien.Properties.DataSource = dscb;

            var nghiOm = _data.BNKBs.FirstOrDefault(o => o.IDKB == IDKB);
            _mabn = nghiOm.MaBNhan ?? 0;

            if ((nghiOm.NghiOmDen != null || DungChung.Bien.MaBV == "27183") && nghiOm.NghiOmTu != null && !string.IsNullOrEmpty(nghiOm.SoNghiOm) && !String.IsNullOrWhiteSpace(nghiOm.GhiChu))
            {
                memoGhiChu.Text = nghiOm.GhiChu;
            }
            else
            {
                if (DungChung.Bien.MaBV == "30007")
                {
                    string[] _MaICDarr = DungChung.Ham.getMaICDarrFull(_data, _mabn, DungChung.Bien.GetICD, 0);
                    string[] icd = _MaICDarr[0].Split(';');
                    string[] tenicd = _MaICDarr[1].Split(';');
                    string lydo = "";
                    if (icd.Length > 0 && !string.IsNullOrEmpty(icd[0]))
                    {
                        lydo += tenicd[0] + " (" + icd[0] + ")" + ";";
                    }
                    if (icd.Length > 1 && !string.IsNullOrEmpty(icd[1]))
                    {
                        lydo += tenicd[1] + " (" + icd[1] + ")" + ";";
                    }
                    if (icd.Length > 2 && !string.IsNullOrEmpty(icd[2]))
                    {
                        lydo += tenicd[2] + " (" + icd[2] + ")" + ";";
                    }
                    if (icd.Length > 3 && !string.IsNullOrEmpty(icd[3]))
                    {
                        string mabk = DungChung.Ham.FreshString(string.Join(";", icd.Skip(3)));
                        string mab1k = DungChung.Ham.FreshString(string.Join(";", tenicd.Skip(3)));
                        lydo += mab1k + " (" + icd[2] + ")" + ";";
                    }
                    if (_MaICDarr.Length >= 8)
                        lydo += DungChung.Ham.FreshString(_MaICDarr[7]);
                    memoGhiChu.Text = DungChung.Ham.FreshString(lydo);
                }
                else
                    memoGhiChu.Text = DungChung.Ham.FreshString(nghiOm.ChanDoan + ";" + nghiOm.BenhKhac);
            }

            if ((nghiOm.NghiOmDen != null || DungChung.Bien.MaBV == "27183") && nghiOm.NghiOmTu != null && !string.IsNullOrEmpty(nghiOm.SoNghiOm))
            {
                nghiOm.GhiChu = memoGhiChu.Text.Trim();
                _data.SaveChanges();
            }

            var kq = (from bnkb in _data.BNKBs.Where(p => p.IDKB == IDKB)
                      join bn in _data.BenhNhans on bnkb.MaBNhan equals bn.MaBNhan
                      select new
                      {
                          bnkb.BenhKhac,
                          bn.MaBNhan,
                          bn.TenBNhan,
                          bn.NgaySinh,
                          bn.ThangSinh,
                          bn.NamSinh,
                          bnkb.GhiChu,
                          bnkb.ChanDoan,
                          SoNghiOm = bnkb.SoNghiOm,
                          bnkb.NgayChungNhanNghiOm,
                          bnkb.NghiOmTu,
                          bnkb.NghiOmDen,
                          bnkb.LyDoNghiThaiSan,
                          bn.DTuong,
                          bnkb.NguoiDaiDien
                      }).ToList();
            foreach (var item in kq)
            {
                if ((item.NghiOmDen != null || DungChung.Bien.MaBV == "27183") && item.NghiOmTu != null && !string.IsNullOrEmpty(item.SoNghiOm))
                {
                    enableControl(false);
                    if ((DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049") && DungChung.Bien.PLoaiKP != DungChung.Bien.st_PhanLoaiKP.Admin)
                    {
                        btnXoa.Enabled = false;
                    }
                    else
                        btnXoa.Enabled = true;
                }
                else
                {
                    enableControl(true);
                    btnXoa.Enabled = false;
                }
                Dtuong = item.DTuong;
                txtHoTen.Text = item.TenBNhan;
                lupCBDaiDien.EditValue = item.NguoiDaiDien;
                string ngaysinh = "";
                txtGhiChuNghiThaiSan.Text = item.LyDoNghiThaiSan;
                if (!String.IsNullOrEmpty(item.NgaySinh))
                {
                    ngaysinh += item.NgaySinh + "/";
                }
                if (!String.IsNullOrEmpty(item.ThangSinh))
                {
                    ngaysinh += item.ThangSinh + "/";
                }
                if (!String.IsNullOrEmpty(item.NamSinh))
                {
                    ngaysinh += item.NamSinh;
                }
                txtNgaySinh.Text = ngaysinh;

                if (item.NghiOmDen != null)
                {
                    dateNgayHen.DateTime = item.NghiOmDen.Value;
                }
                else if (DungChung.Bien.MaBV == "27183" && item.NghiOmTu != null && item.SoNghiOm != null && item.SoNghiOm.Trim() != "")
                {
                    checkBox1.Checked = true;
                }
                else
                {
                    dateNgayHen.DateTime = System.DateTime.Now;
                }

                if (item.NghiOmTu != null)
                {
                    dateNgayNghi.DateTime = item.NghiOmTu.Value;
                }
                else
                    dateNgayNghi.DateTime = System.DateTime.Now;
                if (!string.IsNullOrEmpty(item.SoNghiOm))
                {
                    txtSoChungNhan.Text = item.SoNghiOm;
                    _songhiom = item.SoNghiOm;
                }
                else
                {
                    _songhiom = "";
                    if (DungChung.Bien.MaBV == "27022" || DungChung.Bien.MaBV == "26007" || DungChung.Bien.MaBV == "27001" || (DungChung.Bien.MaBV.Substring(0, 2) == "30" && DungChung.Bien.MaBV != "30007"))
                    {
                        int nam = DateTime.Now.Year;
                        var qsopl = (from spl in _data.SoPLs.Where(p => p.PhanLoai == 10 && p.NgayNhap != null && p.NgayNhap.Value.Year == nam) select spl).ToList();
                        if (qsopl.Count == 0)
                        {
                            MessageBox.Show("Bạn chưa thiết lập số nghỉ ốm");
                        }
                        else
                        {
                            int idMax = qsopl.Max(p => p.IdSoPL);
                            SoPL sua = _data.SoPLs.Single(p => p.IdSoPL == idMax);

                            if (DungChung.Bien.MaBV == "27022")
                                txtSoChungNhan.Text = (sua.SoPL1 + 1).ToString("D4") + "." + dtNgayChungNhan.DateTime.Month.ToString("D2") + "." + dtNgayChungNhan.DateTime.Year;

                            else if (DungChung.Bien.MaBV == "27001")
                                txtSoChungNhan.Text = DungChung.Bien.MaBV + "." + (sua.SoPL1 + 1).ToString("D5");
                            else if (DungChung.Bien.MaBV == "26007")
                                txtSoChungNhan.Text = DungChung.Bien.MaBV + "." + dtNgayChungNhan.DateTime.Year.ToString() + "." + (sua.SoPL1 + 1).ToString("D5");
                            else if (DungChung.Bien.MaBV == "30003" || DungChung.Bien.MaBV == "30004")
                            {
                                txtSoChungNhan.Text = DungChung.Bien.MaBV + "." + dtNgayChungNhan.DateTime.Year.ToString().Substring(2, 2) + "." + (sua.SoPL1 + 1).ToString();
                            }
                            else
                                txtSoChungNhan.Text = DungChung.Bien.MaBV + "." + dtNgayChungNhan.DateTime.Year.ToString().Substring(2, 2) + "." + (sua.SoPL1 + 1).ToString("D3");
                            _songhiom = txtSoChungNhan.Text;

                        }
                    }


                }

                if (item.NgayChungNhanNghiOm != null)
                    dtNgayChungNhan.DateTime = item.NgayChungNhanNghiOm.Value;

                string tuoi = DungChung.Ham.TuoitheoThang(_data, _mabn, "72-00");

                var _lTTBN = (from bn in _data.BenhNhans.Where(p => p.MaBNhan == _mabn)
                              join ttbx in _data.TTboXungs on bn.MaBNhan equals ttbx.MaBNhan
                              select new { bn.TenBNhan, ttbx.NThan, ttbx.NoiLV, ttbx.So_eTBM, bn.DChi, bn.SThe }).FirstOrDefault();
                if (_lTTBN != null)
                {
                    if (!string.IsNullOrEmpty(_lTTBN.NoiLV))
                        txtDonviCT.Text = _lTTBN.NoiLV;
                    else
                        txtDonviCT.Text = _lTTBN.DChi;
                    if (!string.IsNullOrEmpty(_lTTBN.So_eTBM))
                    {
                        if (_lTTBN.So_eTBM.Contains("|"))
                        {
                            string[] _arr = _lTTBN.So_eTBM.Split('|');
                            txtSoTheBHXH.Text = _arr[0];
                            _soct = _arr[1];
                            if (_arr.Length > 2)
                                txtSoThe.Text = _arr[2];
                            else
                                txtSoThe.Text = _lTTBN.SThe;
                        }
                    }
                    else
                    {
                        if (Dtuong == "BHYT")
                        {
                            txtSoThe.Text = _lTTBN.SThe;
                            txtSoTheBHXH.Text = _lTTBN.SThe.Substring(5);
                        }
                    }
                    if (DungChung.Bien.MaBV == "27001")
                    {
                        if (!string.IsNullOrEmpty(_soct))
                        {
                            txtSoCT.Text = _soct;
                        }
                        else
                        {
                            #region Tạo số ct nghỉ ốm
                            int nam = DateTime.Now.Year;
                            var qsopl = (from spl in _data.SoPLs.Where(p => p.PhanLoai == 11 && p.NgayNhap != null && p.NgayNhap.Value.Year == nam) select spl).ToList();
                            if (qsopl.Count == 0)
                            {
                                MessageBox.Show("Bạn chưa thiết lập số chứng từ");
                            }
                            else
                            {
                                int idMax = qsopl.Max(p => p.IdSoPL);
                                SoPL sua = _data.SoPLs.Single(p => p.IdSoPL == idMax);
                                _soctint = sua.SoPL1 + 1;
                                _soct = "27001" + _soctint.ToString("D5");
                                txtSoCT.Text = _soct;
                            }
                            #endregion
                        }
                    }
                    else
                        txtSoCT.Text = _soct;

                    if (!string.IsNullOrEmpty(_lTTBN.NThan))
                    {
                        if (_lTTBN.NThan.Contains(";"))
                        {
                            string[] arrtt = _lTTBN.NThan.Split(';');
                            if (arrtt.Length > 1 && !string.IsNullOrEmpty(arrtt[0]))
                            {
                                string[] arrttt = arrtt[0].Split(',');
                                if (arrttt.Length > 1 && !string.IsNullOrEmpty(arrttt[0]))
                                {
                                    txtTenBo.Text = arrttt[0];
                                    txtTenMe.Text = arrttt[1];
                                }
                            }
                            if (arrtt.Length > 2 && !string.IsNullOrEmpty(arrtt[1]))
                                x1 = arrtt[1];
                            if (arrtt.Length > 3 && !string.IsNullOrEmpty(arrtt[2]))
                                x2 = arrtt[2];
                            if (arrtt.Length > 3 && !string.IsNullOrEmpty(arrtt[3]))
                                x3 = arrtt[3];
                        }
                        else
                        {
                            if (_lTTBN.NThan.Contains(","))
                            {
                                string[] arrttt = _lTTBN.NThan.Split(',');
                                if (arrttt.Length > 1 && !string.IsNullOrEmpty(arrttt[0]))
                                {
                                    txtTenBo.Text = arrttt[0];
                                    txtTenMe.Text = arrttt[1];
                                }
                            }
                            else
                                txtTenBo.Text = "";
                        }
                    }
                }
            }
            if (Dtuong == "BHYT")
                txtSoThe.Enabled = false;
            if ((DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049") && DungChung.Bien.PLoaiKP != DungChung.Bien.st_PhanLoaiKP.Admin)
            {
                btnSua.Enabled = false;
                btnXoa.Enabled = false;
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void dateNgayNghi_EditValueChanged(object sender, EventArgs e)
        {
            DateTime ngayhen = dateNgayHen.DateTime;
            DateTime ngaynghi = dateNgayNghi.DateTime;
            setSoNgayNghi(ngayhen, ngaynghi);
        }
        private void setSoNgayNghi(DateTime ngayhen, DateTime ngaynghi)
        {
            if (checkBox1.Checked)
            {
                txtSoNgayNghi.Text = "";
            }
            else
            {
                int day = (ngayhen.Date - ngaynghi.Date).Days + 1;
                txtSoNgayNghi.Text = day.ToString();
            }
        }

        private void dateNgayHen_EditValueChanged(object sender, EventArgs e)
        {
            DateTime ngayhen = dateNgayHen.DateTime;
            DateTime ngaynghi = dateNgayNghi.DateTime;
            setSoNgayNghi(ngayhen, ngaynghi);
        }
        bool sua = false;
        private void btnSua_Click(object sender, EventArgs e)
        {
            enableControl(true);
            sua = true;
        }
        private void enableControl(bool T)
        {
            memoGhiChu.Properties.ReadOnly = !T;
            dateNgayHen.Properties.ReadOnly = !T;
            lupCBDaiDien.Properties.ReadOnly = !T;
            txtSoThe.Properties.ReadOnly = !T;
            txtSoCT.Properties.ReadOnly = !T;

            if (DungChung.Bien.MaBV != "01071" && DungChung.Bien.MaBV != "01049" && DungChung.Bien.MaBV != "26007" && DungChung.Bien.MaBV != "27022" && DungChung.Bien.MaBV != "27001" && (DungChung.Bien.MaBV.Substring(0, 2) != "30" || DungChung.Bien.MaBV == "30007"))
                txtSoChungNhan.Properties.ReadOnly = !T;
            else
                txtSoChungNhan.Enabled = false;

            if (DungChung.Bien.MaBV == "27183")
            {
                checkBox1.Enabled = T;
            }
            else
                checkBox1.Enabled = false;

            if (DungChung.Bien.MaBV != "26007")
            {
                dateNgayNghi.Properties.ReadOnly = !T;
                dtNgayChungNhan.Properties.ReadOnly = !T;
                txtSoTheBHXH.Properties.ReadOnly = !T;
            }
            else
            {
                dateNgayNghi.Properties.ReadOnly = true;
                dtNgayChungNhan.Properties.ReadOnly = true;
                txtSoTheBHXH.Properties.ReadOnly = true;
            }

            txtDonviCT.Properties.ReadOnly = !T;
            if ((DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049") && DungChung.Bien.PLoaiKP != DungChung.Bien.st_PhanLoaiKP.Admin)
            {
                btnSua.Enabled = false;
            }
            else
                btnSua.Enabled = !T;
            btnLuu.Enabled = T;
            btnIn.Enabled = !T;
            btnIn2Lien.Enabled = !T;
        }
        int sopl2 = 0;
        private bool IsNumber(string a)
        {
            foreach (Char item in a)
            {
                if (!Char.IsDigit(item))
                    return false;
            }
            return true;
        }
        private void btnLuu_Click(object sender, EventArgs e)
        {
            dtNgayChungNhan_EditValueChanged(sender, e);
            _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            //int _mabn = _data.BNKBs.Where(p => p.IDKB == IDKB).ToList().FirstOrDefault().MaBNhan ?? 0;
            string tuoi = DungChung.Ham.TuoitheoThang(_data, _mabn, "72-00");
            int sopl1 = 0;
            if (sopl2 == 0)
                sopl1 = _data.SoPLs.Where(p => p.PhanLoai == 10).Count() > 0 ? _data.SoPLs.Where(p => p.PhanLoai == 10).Max(p => p.MaKP) : 0;
            else
                sopl1 = sopl2;

            if (tuoi.Length > 3 && txtTenBo.Text == "" && txtTenMe.Text == "")
            {
                MessageBox.Show("Bệnh nhân dưới 72 tháng tuổi phải có tên bố/mẹ!");
                txtTenBo.Focus();
            }
            else if (dtNgayChungNhan.DateTime.Date < dateNgayNghi.DateTime.Date)
            {
                MessageBox.Show("Ngày chứng nhận nghỉ ốm phải sau ngày bắt đầu nghỉ");
                dtNgayChungNhan.Focus();
            }
            else if (checkBox1.Checked == false && (dateNgayHen.DateTime.Date < dateNgayNghi.DateTime.Date))
            {
                MessageBox.Show("Ngày đến phải lớn hơn ngày bắt đầu nghỉ");
                dateNgayHen.Focus();
            }
            //else if (qbnkb.Count > 0)//txtSoChungNhan.Text != _songhiom && 
            //{
            //    MessageBox.Show("Số nghỉ ốm đã tồn tại, bạn không thể thêm");
            //    txtSoChungNhan.Focus();
            //}
            else if (string.IsNullOrEmpty(txtSoChungNhan.Text))
            {

                MessageBox.Show("Số nghỉ ốm không được để trống, kiểm tra thiết lập");
                txtSoChungNhan.Focus();
            }
            else if (DungChung.Bien.MaBV == "30003" && DungChung.Bien.MaBV == "30004" && txtSoChungNhan.Text.Length < 10)
            {
                MessageBox.Show("Số nghỉ ốm chưa đúng định dạng");
                txtSoChungNhan.Focus();
            }
            else if (DungChung.Bien.MaBV.Substring(0, 2) == "30" && DungChung.Bien.MaBV != "30007" && DungChung.Bien.MaBV != "30003" && DungChung.Bien.MaBV != "30004" && txtSoChungNhan.Text.Length < 12)
            {
                MessageBox.Show("Số nghỉ ốm chưa đúng định dạng");
                txtSoChungNhan.Focus();
            }

            else if (memoGhiChu.Text == "")
            {
                MessageBox.Show("Phải có lý do nghỉ việc!");
                memoGhiChu.Focus();
            }
            else if (txtDonviCT.Text == "")
            {
                MessageBox.Show("Phải có đơn vị công tác!");
                txtDonviCT.Focus();
            }
            else if (lupCBDaiDien.EditValue == null)
            {
                MessageBox.Show("Phải người đại diện!");
                lupCBDaiDien.Focus();
            }
            else if (string.IsNullOrEmpty(txtSoTheBHXH.Text) || !IsNumber(txtSoTheBHXH.Text.Trim()) || txtSoTheBHXH.Text.Trim().Length > 10)
            {
                MessageBox.Show("Mã số BHXH không được để trống, phải là dãy 10 số tự nhiên!");
                txtSoTheBHXH.Focus();
            }
            else if (string.IsNullOrEmpty(txtSoThe.Text))
            {
                MessageBox.Show("Số thẻ không được để trống");
                txtSoThe.Focus();
            }
            else if (string.IsNullOrEmpty(txtSoCT.Text))
            {
                MessageBox.Show("Số chứng từ không được để trống");
                txtSoCT.Focus();
            }
            else
            {

                var qkt = _data.BNKBs.Where(p => p.MaBNhan == _mabn).Where(p => p.IDKB != IDKB).Where(p => p.SoNghiOm != null && p.SoNghiOm != "" && p.NgayChungNhanNghiOm != null).ToList();
                string strsonghiom = "";
                int sopl = 1;
                if (qkt.Count > 0)
                    MessageBox.Show("Bệnh nhân đã có xác nhận nghỉ ốm, bạn không thể thêm");
                else
                {
                    if ((DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "26007" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "27022" || DungChung.Bien.MaBV == "27001" || (DungChung.Bien.MaBV.Substring(0, 2) == "30" && DungChung.Bien.MaBV != "30007")))//txtSoChungNhan.Text != _songhiom && 
                    {
                        int ot;
                        if (DungChung.Bien.MaBV == "27022" || DungChung.Bien.MaBV == "27001" || DungChung.Bien.MaBV == "26007" || (DungChung.Bien.MaBV.Substring(0, 2) == "30" && DungChung.Bien.MaBV != "30007"))
                        {
                            bool ktra = true;
                            if (DungChung.Bien.MaBV == "27001")
                                ktra = false;

                            while (ktra)
                            {
                                string sno = txtSoChungNhan.Text;
                                var qbnkb = _data.BNKBs.Where(p => p.IDKB != IDKB && p.SoNghiOm != null && p.SoNghiOm != "" && p.SoNghiOm == sno).ToList();
                                if (qbnkb.Count > 0)
                                {
                                    if (DungChung.Bien.MaBV == "27022")
                                    {
                                        int Sonew = Convert.ToInt32(sno.Substring(0, 4));
                                        string somoi = (Sonew + 1).ToString("D4");
                                        txtSoChungNhan.Text = txtSoChungNhan.Text.Replace(sno.Substring(0, 4), somoi);
                                    }
                                    else if (DungChung.Bien.MaBV == "26007")
                                    {
                                        int Sonew = Convert.ToInt32(sno.Substring(12));
                                        string somoi = (Sonew + 1).ToString();
                                        txtSoChungNhan.Text = txtSoChungNhan.Text.Replace(sno.Substring(12), somoi);
                                    }
                                    //else  if (DungChung.Bien.MaBV == "27001")
                                    // {
                                    //     if (sno != null && sno.Length > 6)
                                    //     {
                                    //         int Sonew = Convert.ToInt32(sno.Substring(6));
                                    //         string somoi = (Sonew + 1).ToString("D5");
                                    //         txtSoChungNhan.Text = txtSoChungNhan.Text.Replace(sno.Substring(6), somoi);
                                    //     }
                                    // }
                                    else if (DungChung.Bien.MaBV == "30003" || DungChung.Bien.MaBV == "30004")
                                    {
                                        int Sonew = Convert.ToInt32(sno.Substring(9));
                                        string somoi = (Sonew + 1).ToString();
                                        txtSoChungNhan.Text = txtSoChungNhan.Text.Replace(sno.Substring(9), somoi);
                                    }
                                    else
                                    {
                                        int Sonew = Convert.ToInt32(sno.Substring(9));
                                        string somoi = (Sonew + 1).ToString("D3");
                                        txtSoChungNhan.Text = txtSoChungNhan.Text.Replace(sno.Substring(9), somoi);
                                    }
                                    ktra = true;
                                }
                                else
                                {
                                    ktra = false;
                                }
                            }

                            if (DungChung.Bien.MaBV == "27022")
                                strsonghiom = txtSoChungNhan.Text.Substring(0, 4);
                            else if (DungChung.Bien.MaBV == "27001")
                            {
                                if (txtSoChungNhan.Text != null && txtSoChungNhan.Text.Length > 6)
                                    strsonghiom = txtSoChungNhan.Text.Substring(6);
                            }
                            else if (DungChung.Bien.MaBV == "26007")
                            {
                                if (txtSoChungNhan.Text != null && txtSoChungNhan.Text.Length > 12)
                                    strsonghiom = txtSoChungNhan.Text.Substring(12);
                            }
                            else
                                strsonghiom = txtSoChungNhan.Text.Substring(9);

                            #region
                            if (Int32.TryParse(strsonghiom, out ot))
                            {
                                int nam = DateTime.Now.Year;
                                var qsopl = (from spl in _data.SoPLs.Where(p => p.PhanLoai == 10 && p.NgayNhap != null && p.NgayNhap.Value.Year == nam) select spl).ToList();
                                if (qsopl.Count == 0)
                                {
                                    //SoPL moi = new SoPL();
                                    //moi.PhanLoai = 10;
                                    //moi.MaKP = 0;
                                    //moi.SoPL1 = Convert.ToInt32(strsonghiom);
                                    //moi.Status = 1;
                                    //moi.NgayNhap = DateTime.Now;
                                    //_data.SoPLs.Add(moi);
                                    //_data.SaveChanges();
                                    //sopl = moi.SoPL1;
                                }
                                else
                                {
                                    int idMax = qsopl.Max(p => p.IdSoPL);
                                    SoPL sua1 = _data.SoPLs.Single(p => p.IdSoPL == idMax);
                                    if (Convert.ToInt32(strsonghiom) > sua1.SoPL1)
                                    {
                                        DateTime? dt = sua1.NgayNhap;
                                        _data.SoPLs.Remove(sua1);
                                        _data.SaveChanges();
                                        SoPL moi = new SoPL();
                                        moi.PhanLoai = 10;
                                        moi.MaKP = 0;
                                        moi.SoPL1 = Convert.ToInt32(strsonghiom);
                                        moi.Status = 1;
                                        moi.NoiTru = -1;
                                        moi.NgayNhap = dt;
                                        _data.SoPLs.Add(moi);
                                        _data.SaveChanges();
                                        sopl = moi.SoPL1;
                                    }
                                }
                            }
                            else
                                MessageBox.Show("Số nghỉ ốm chưa đúng định dạng");
                            #endregion

                        }
                        else
                        {
                            if (Int32.TryParse(txtSoChungNhan.Text.Substring(9), out ot))
                            {
                                SoPL moi = new SoPL();
                                moi.MaKP = sopl1 + 1;
                                moi.SoPL1 = Convert.ToInt32(txtSoChungNhan.Text.Substring(9));
                                moi.Status = 1;
                                moi.PhanLoai = 10;
                                moi.NgayNhap = DateTime.Now;
                                moi.NoiTru = -1;
                                _data.SoPLs.Add(moi);
                                _data.SaveChanges();
                            }
                            else
                                MessageBox.Show("Số nghỉ ốm chưa đúng định dạng");
                        }


                    }

                    if (DungChung.Bien.MaBV == "27001" && !sua)
                    {
                        bool ktct = true;
                        int dem = 0;
                        while (ktct)
                        {
                            _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

                            int nam = DateTime.Now.Year;
                            var qsopl = (from spl in _data.SoPLs.Where(p => p.PhanLoai == 11 && p.NgayNhap != null && p.NgayNhap.Value.Year == nam) select spl).ToList();
                            if (qsopl.Count == 0)
                            {

                            }
                            else
                            {
                                int idMax = qsopl.Max(p => p.IdSoPL);
                                SoPL sua1 = _data.SoPLs.Single(p => p.IdSoPL == idMax);
                                if (_soctint > sua1.SoPL1)
                                {
                                    DateTime? dt = sua1.NgayNhap;
                                    _data.SoPLs.Remove(sua1);
                                    _data.SaveChanges();
                                    SoPL moi = new SoPL();
                                    moi.PhanLoai = 11;
                                    moi.MaKP = 0;
                                    moi.SoPL1 = _soctint;
                                    moi.Status = 1;
                                    moi.NoiTru = -1;
                                    moi.NgayNhap = dt;
                                    _data.SoPLs.Add(moi);
                                    _data.SaveChanges();
                                    ktct = false;
                                    //sopl = moi.SoPL1;
                                }
                                else
                                {
                                    _soctint++;
                                    ktct = true;
                                    dem++;
                                    if (dem > 10)
                                    {
                                        MessageBox.Show("Lỗi update số chứng từ nghỉ ốm, kiểm tra lại thiết lập!");
                                        return;
                                    }
                                }
                            }
                        }
                        txtSoCT.Text = "27001" + _soctint.ToString("D5");
                    }
                    var bnkb = _data.BNKBs.Single(p => p.IDKB == IDKB);
                    var ttbx = _data.TTboXungs.Where(p => p.MaBNhan == bnkb.MaBNhan).FirstOrDefault();
                    if (bnkb != null)
                    {
                        // lưu bnkb
                        bnkb.GhiChu = memoGhiChu.Text.Trim();
                        if (!checkBox1.Checked)
                            bnkb.NghiOmDen = dateNgayHen.DateTime;
                        else
                            bnkb.NghiOmDen = null;
                        bnkb.NghiOmTu = dateNgayNghi.DateTime;

                        bnkb.NguoiDaiDien = lupCBDaiDien.EditValue.ToString();
                        bnkb.SoNghiOm = txtSoChungNhan.Text;
                        bnkb.NgayChungNhanNghiOm = dtNgayChungNhan.DateTime;
                        bnkb.LyDoNghiThaiSan = txtGhiChuNghiThaiSan.Text;
                        if (ttbx != null)
                        {
                            ttbx.NoiLV = txtDonviCT.Text;
                            ttbx.NThan = txtTenBo.Text + "," + txtTenMe.Text + ";" + x1 + ";" + x2 + ";" + x3;
                            if (Dtuong == "Dịch vụ")
                                ttbx.So_eTBM = txtSoTheBHXH.Text + "|" + txtSoCT.Text + "|" + txtSoThe.Text;
                            else
                                ttbx.So_eTBM = txtSoTheBHXH.Text + "|" + txtSoCT.Text;
                        }
                        else
                        {
                            TTboXung moi = new TTboXung();
                            moi.MaBNhan = _mabn;
                            moi.NoiLV = txtDonviCT.Text;
                            moi.NThan = txtTenBo.Text + "," + txtTenMe.Text + ";" + x1 + ";" + x2 + ";" + x3;
                            if (Dtuong == "Dịch vụ")
                                moi.So_eTBM = txtSoTheBHXH.Text + "|" + txtSoCT.Text + "|" + txtSoThe.Text;
                            else
                                moi.So_eTBM = txtSoTheBHXH.Text + "|" + txtSoCT.Text;
                        }
                        var qsonghiom = _data.BNKBs.Where(p => p.SoNghiOm == bnkb.SoNghiOm && p.IDKB != bnkb.IDKB).FirstOrDefault();
                        if (qsonghiom != null && sua == false)
                        {
                            MessageBox.Show("Số nghỉ ốm đã tồn tại");

                        }
                        else
                        {
                            _data.SaveChanges();
                            _songhiom = txtSoChungNhan.Text;
                            MessageBox.Show("Lưu thành công!");

                        }
                    }

                    frm_NhapNghiOm_Load(sender, e);
                    //enableControl(false);

                }
            }
        }

        private void btnIn_Click(object sender, EventArgs e)
        {

            #region bv chung
            frmIn frm = new frmIn();
            BaoCao.rep_NghiOm rep = new BaoCao.rep_NghiOm();
            var kq = (from bnkb in _data.BNKBs.Where(p => p.IDKB == IDKB)
                      join bn in _data.BenhNhans on bnkb.MaBNhan equals bn.MaBNhan
                      select new
                      {
                          bn.MaBNhan,
                          bn.TenBNhan,
                          bn.SThe,
                          bn.NgaySinh,
                          bn.ThangSinh,
                          bn.NamSinh,
                          bnkb.NghiOmDen,
                          bnkb.NghiOmTu,
                          bnkb.GhiChu,
                          bnkb.ChanDoan,
                          bnkb.LyDoNghiThaiSan
                      }).ToList();
            int _mabn = 0;
            foreach (var item in kq)
            {
                _mabn = item.MaBNhan;
                string ngaysinh = "";
                if (!String.IsNullOrEmpty(item.NgaySinh))
                {
                    ngaysinh += item.NgaySinh + "/";
                }
                if (!String.IsNullOrEmpty(item.ThangSinh))
                {
                    ngaysinh += item.ThangSinh + "/";
                }
                if (!String.IsNullOrEmpty(item.NamSinh))
                {
                    ngaysinh += item.NamSinh;
                }
                rep.paramHoten.Value = item.TenBNhan;
                rep.paramNgaySinh.Value = ngaysinh;
                rep.paramSThe.Value = item.SThe;
                rep.paramLyDoNghiViec.Value = item.GhiChu;
                if (item.NghiOmDen != null && item.NghiOmTu != null)
                {
                    int day = (item.NghiOmDen.Value.Date - item.NghiOmTu.Value.Date).Days + 1;
                    if (day.ToString().Length == 1)
                    {
                        rep.paramSoNgayNghi.Value = "0" + day + " ngày";
                    }
                    else
                        rep.paramSoNgayNghi.Value = day + " ngày";
                    rep.paramNgay.Value = item.NghiOmTu.Value.ToShortDateString() + "    đến hết ngày: " + item.NghiOmDen.Value.ToShortDateString() + " )";
                }
                else if (item.NghiOmDen == null && item.NghiOmTu != null)
                {
                    rep.paramSoNgayNghi.Value = item.LyDoNghiThaiSan;
                    rep.paramNgay.Value = item.NghiOmTu.Value.ToShortDateString() + "    đến hết ngày: ................... )";
                }
                var ttbx = _data.TTboXungs.Where(p => p.MaBNhan == _mabn).ToList();
                if (ttbx.Count > 0)
                    rep.paramDonViCT.Value = ttbx.First().NoiLV;
            }
            if (DungChung.Bien.MaBV == "30303")
            {
                var tencqcq = (from name in _data.HTHONGs select name).ToList();
                DungChung.Bien.TenCQCQ = tencqcq.Select(p => p.TenCQCQ).First().ToString();
                DungChung.Bien.TenCQ = "PHÒNG KHÁM ĐA KHOA HÀ ĐÔNG";
            }
            rep.CreateDocument();
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();
            #endregion
        }
        private void giaynghiom_30007(int IDKB)
        {
            QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);


            var kq = (from bnkb in _data.BNKBs.Where(p => p.IDKB == IDKB)
                      join bn in _data.BenhNhans on bnkb.MaBNhan equals bn.MaBNhan
                      join cb in _data.CanBoes on bnkb.MaCB equals cb.MaCB
                      join ttbx in _data.TTboXungs on bnkb.MaBNhan equals ttbx.MaBNhan
                      join kp in _data.KPhongs on bnkb.MaKP equals kp.MaKP
                      select new
                      {
                          bn.MaBNhan,
                          bn.TenBNhan,
                          bn.SThe,
                          bn.NgaySinh,
                          bn.ThangSinh,
                          bn.NamSinh,
                          bnkb.NgayHen,
                          bnkb.NgayNghi,
                          GTinh = (bn.GTinh == 1) ? "Nam" : "Nữ",
                          bnkb.ChanDoan,
                          bnkb.BenhKhac,
                          cb.TenCB,
                          ttbx.NoiLV,
                          bnkb.SoNghiOm,
                          kp.TenKP
                      }).ToList();
            int _mabn = 0;

            for (int i = 0; i < 2; i++)
            {

                if (i == 0)
                {
                    BaoCao.rep_NghiOm_New rep = new BaoCao.rep_NghiOm_New();

                    foreach (var item in kq)
                    {

                        _mabn = item.MaBNhan;
                        rep.idkb.Value = "Số: " + _mabn + " /KCB-" + item.TenKP;
                        string ngaysinh = "";
                        if (!String.IsNullOrEmpty(item.NgaySinh))
                        {
                            ngaysinh += item.NgaySinh + "/";
                        }
                        if (!String.IsNullOrEmpty(item.ThangSinh))
                        {
                            ngaysinh += item.ThangSinh + "/";
                        }
                        if (!String.IsNullOrEmpty(item.NamSinh))
                        {
                            ngaysinh += item.NamSinh;
                        }
                        rep.hoten.Value = item.TenBNhan;
                        rep.ngaysinh.Value = ngaysinh;
                        rep.bhyt.Value = item.SThe;
                        rep.gioitinh.Value = item.GTinh;
                        rep.chandoan.Value = item.ChanDoan + "; " + item.BenhKhac;
                        rep.bacsykcb.Value = item.TenCB;

                        if (item.NgayHen != null && item.NgayNghi != null)
                        {
                            int day = (item.NgayHen.Value.Date - item.NgayNghi.Value.Date).Days + 1;
                            if (day.ToString().Length == 1)
                            {
                                rep.songaynghi.Value = "0" + day + " ngày";
                            }
                            else
                                rep.songaynghi.Value = day + " ngày";
                            rep.khoangngay.Value = " (Từ ngày: " + item.NgayNghi.Value.ToShortDateString() + "    đến ngày: " + item.NgayHen.Value.ToShortDateString() + " )";
                        }
                        else rep.khoangngay.Value = " (Từ ngày: " + "                    đến ngày:                    " + " )";
                        if (item.NoiLV != null && item.NoiLV != "")
                            rep.donvi.Value = "Đơn vị làm việc: " + item.NoiLV;
                        else rep.donvi.Value = "Đơn vị làm việc: ";
                        rep.soseri.Value = item.SoNghiOm;
                        rep.ngay.Value = DungChung.Ham.NgaySangChu(DateTime.Now, 1);

                    }
                    rep.lien.Value = "Liên 1: Giao người lao động";
                    rep.CreateDocument();
                    frmIn frm = new frmIn();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                }
                else
                {
                    BaoCao.rep_NghiOm_New rep1 = new BaoCao.rep_NghiOm_New();
                    //rep1.idkb.Value = "Số:         " + " /KCB";
                    foreach (var item in kq)
                    {

                        _mabn = item.MaBNhan;
                        rep1.idkb.Value = "Số: " + _mabn + " /KCB-" + item.TenKP;
                        string ngaysinh = "";
                        if (!String.IsNullOrEmpty(item.NgaySinh))
                        {
                            ngaysinh += item.NgaySinh + "/";
                        }
                        if (!String.IsNullOrEmpty(item.ThangSinh))
                        {
                            ngaysinh += item.ThangSinh + "/";
                        }
                        if (!String.IsNullOrEmpty(item.NamSinh))
                        {
                            ngaysinh += item.NamSinh;
                        }
                        rep1.hoten.Value = item.TenBNhan.ToUpper();
                        rep1.ngaysinh.Value = ngaysinh;
                        rep1.bhyt.Value = item.SThe;
                        rep1.gioitinh.Value = item.GTinh;
                        rep1.chandoan.Value = item.ChanDoan + "; " + item.BenhKhac;
                        rep1.bacsykcb.Value = item.TenCB;

                        if (item.NgayHen != null && item.NgayNghi != null)
                        {
                            int day = (item.NgayHen.Value.Date - item.NgayNghi.Value.Date).Days + 1;
                            if (day.ToString().Length == 1)
                            {
                                rep1.songaynghi.Value = "0" + day + " ngày";
                            }
                            else
                                rep1.songaynghi.Value = day + " ngày";
                            rep1.khoangngay.Value = " (Từ ngày: " + item.NgayNghi.Value.ToShortDateString() + "    đến ngày: " + item.NgayHen.Value.ToShortDateString() + " )";
                        }
                        else rep1.khoangngay.Value = " (Từ ngày: " + "                    đến ngày:                    " + " )";
                        if (item.NoiLV != null && item.NoiLV != "")
                            rep1.donvi.Value = "Đơn vị làm việc: " + item.NoiLV;
                        else rep1.donvi.Value = "Đơn vị làm việc: ";
                        rep1.soseri.Value = item.SoNghiOm;
                        rep1.ngay.Value = DungChung.Ham.NgaySangChu(DateTime.Now, 1);

                    }
                    rep1.lien.Value = "Liên 2: Lưu";
                    rep1.CreateDocument();
                    frmIn frm1 = new frmIn();
                    frm1.prcIN.PrintingSystem = rep1.PrintingSystem;
                    frm1.ShowDialog();
                }

            }
        }
        public static BaoCao.rep_NghiOm_TT56 in2lien(int IDKB, string lien)
        {
            QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

            var kq = (from bnkb in _data.BNKBs.Where(p => p.IDKB == IDKB)
                      join bn in _data.BenhNhans on bnkb.MaBNhan equals bn.MaBNhan
                      join cb in _data.CanBoes on bnkb.MaCB equals cb.MaCB
                      join ttbx in _data.TTboXungs on bnkb.MaBNhan equals ttbx.MaBNhan
                      join kp in _data.KPhongs on bnkb.MaKP equals kp.MaKP
                      select new
                      {
                          bn.MaBNhan,
                          bn.TenBNhan,
                          bn.SThe,
                          bn.NgaySinh,
                          bn.ThangSinh,
                          bn.NamSinh,
                          bnkb.NghiOmDen,
                          bnkb.NghiOmTu,
                          GTinh = (bn.GTinh == 1) ? "Nam" : "Nữ",
                          bnkb.ChanDoan,
                          bnkb.BenhKhac,
                          cb.TenCB,
                          ttbx.NoiLV,
                          bnkb.SoNghiOm,
                          kp.TenKP,
                          ttbx.NThan
                      }).ToList();
            int _mabn = 0;
            BaoCao.rep_NghiOm_TT56 rep = new BaoCao.rep_NghiOm_TT56();

            foreach (var item in kq)
            {
                if (item.NThan != null)
                {
                    if (item.NThan.Contains(";"))
                    {
                        string[] arrtt = item.NThan.Split(';');
                        if (arrtt.Length > 1 && !string.IsNullOrEmpty(arrtt[0]))
                        {
                            string[] arrttt = arrtt[0].Split(',');
                            if (arrttt.Length > 1 && !string.IsNullOrEmpty(arrttt[0]))
                            {
                                rep.hotencha.Value = arrttt[0];
                                rep.hotenme.Value = arrttt[1];
                            }
                        }
                    }
                    else
                    {
                        if (item.NThan.Contains(","))
                        {
                            string[] arrttt = item.NThan.Split(',');
                            if (arrttt.Length > 1 && !string.IsNullOrEmpty(arrttt[0]))
                            {
                                rep.hotencha.Value = arrttt[0];
                                rep.hotenme.Value = arrttt[1];
                            }
                        }
                        else
                            rep.hotencha.Value = item.NThan;
                    }
                }

                _mabn = item.MaBNhan;
                rep.idkb.Value = "Số: " + _mabn + "/KCB";
                string ngaysinh = "";
                if (!String.IsNullOrEmpty(item.NgaySinh))
                {
                    ngaysinh += item.NgaySinh + "/";
                }
                if (!String.IsNullOrEmpty(item.ThangSinh))
                {
                    ngaysinh += item.ThangSinh + "/";
                }
                if (!String.IsNullOrEmpty(item.NamSinh))
                {
                    ngaysinh += item.NamSinh;
                }
                rep.hoten.Value = item.TenBNhan.ToUpper();
                rep.ngaysinh.Value = ngaysinh;
                rep.bhyt.Value = item.SThe;
                rep.gioitinh.Value = item.GTinh;
                rep.chandoan.Value = item.ChanDoan + "; " + item.BenhKhac;
                rep.bacsykcb.Value = item.TenCB;
                rep.PhongKham.Value = item.TenKP;
                rep.CQ.Value = DungChung.Bien.TenCQ.ToUpper();
                if (item.NghiOmDen != null && item.NghiOmTu != null)
                {
                    int day = (item.NghiOmDen.Value.Date - item.NghiOmTu.Value.Date).Days + 1;
                    if (day.ToString().Length == 1)
                    {
                        rep.songaynghi.Value = "0" + day + " ngày";
                    }
                    else
                        rep.songaynghi.Value = day + " ngày";
                    rep.khoangngay.Value = " (Từ ngày: " + item.NghiOmTu.Value.ToShortDateString() + "    đến ngày: " + item.NghiOmDen.Value.ToShortDateString() + " )";
                }
                else rep.khoangngay.Value = " (Từ ngày: " + "                    đến hết ngày:                    " + " )";
                if (item.NoiLV != null && item.NoiLV != "")
                    rep.donvi.Value = "Đơn vị làm việc: " + item.NoiLV;
                else rep.donvi.Value = "Đơn vị làm việc: ";
                rep.soseri.Value = item.SoNghiOm;
                rep.ngay.Value = DungChung.Ham.NgaySangChu(DateTime.Now, 1);

            }
            rep.lien.Value = lien;
            rep.CreateDocument();
            return rep;
        }
        private void btnIn2Lien_Click(object sender, EventArgs e)
        {
            //if (DungChung.Bien.MaBV == "01071")
            //{
            //BaoCao.rep_NghiOm_TT56 rep = frm_NhapNghiOm.in2lien(IDKB, "Liên số 1");
            //BaoCao.rep_NghiOm_TT56 rep2 = frm_NhapNghiOm.in2lien(IDKB, "Liên số 2");
            //rep.Pages.AddRange(rep2.Pages);
            ////BaoCao.Rep_GiayNGhiOm_01071_A4 rep = new BaoCao.Rep_GiayNGhiOm_01071_A4(IDKB);
            QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

            var kq = (from bnkb in _data.BNKBs.Where(p => p.IDKB == IDKB)
                      join bn in _data.BenhNhans on bnkb.MaBNhan equals bn.MaBNhan
                      join cb in _data.CanBoes on bnkb.MaCB equals cb.MaCB
                      join ttbx in _data.TTboXungs on bnkb.MaBNhan equals ttbx.MaBNhan
                      join kp in _data.KPhongs on bnkb.MaKP equals kp.MaKP

                      select new
                      {
                          bn.MaBNhan,
                          bn.TenBNhan,
                          bn.SThe,
                          bn.NgaySinh,
                          bn.ThangSinh,
                          bn.NamSinh,
                          bnkb.NghiOmDen,
                          bnkb.NghiOmTu,
                          GTinh = (bn.GTinh == 1) ? "Nam" : "Nữ",
                          bnkb.ChanDoan,
                          bnkb.BenhKhac,
                          bnkb.LyDoNghiThaiSan,
                          cb.TenCB,
                          ttbx.NoiLV,
                          bnkb.SoNghiOm,
                          kp.TenKP,
                          ttbx.NThan,
                          ttbx.So_eTBM,
                          bn.DTuong,
                          bnkb.GhiChu,
                          bnkb.NgayChungNhanNghiOm,
                          bnkb.NgayNghi,
                          bnkb.NgayKham
                      }).ToList();
            int _mabn = 0;
            if (DungChung.Bien.MaBV == "26007")
            {
                #region
                BaoCao.rep_NghiOm_TT56_01071_A4_26007 rep = new BaoCao.rep_NghiOm_TT56_01071_A4_26007();

                foreach (var item in kq)
                {
                    if (item.NThan != null)
                    {
                        if (item.NThan.Contains(";"))
                        {
                            string[] arrtt = item.NThan.Split(';');
                            if (arrtt.Length > 1 && !string.IsNullOrEmpty(arrtt[0]))
                            {
                                string[] arrttt = arrtt[0].Split(',');
                                if (arrttt.Length > 0)
                                    rep.hotencha.Value = arrttt[0];
                                if (arrttt.Length > 1)
                                    rep.hotenme.Value = arrttt[1];
                                //if (arrttt.Length > 1 && !string.IsNullOrEmpty(arrttt[0]))
                                //{
                                //    rep.hotencha.Value = arrttt[0];
                                //    rep.hotenme.Value = arrttt[1];
                                //}
                            }
                        }
                        else
                        {
                            if (item.NThan.Contains(","))
                            {
                                string[] arrttt = item.NThan.Split(',');
                                if (arrttt.Length > 0)
                                    rep.hotencha.Value = arrttt[0];
                                if (arrttt.Length > 1)
                                    rep.hotenme.Value = arrttt[1];
                                //if (arrttt.Length > 1 && !string.IsNullOrEmpty(arrttt[0]))
                                //{
                                //    rep.hotencha.Value = arrttt[0];
                                //    rep.hotenme.Value = arrttt[1];
                                //}
                            }
                            else
                                rep.hotencha.Value = item.NThan;
                        }
                    }

                    _mabn = item.MaBNhan;
                    rep.xrBarCode1.Text = _mabn.ToString();
                    rep.idkb.Value = "Số: " + _mabn + "/KCB";
                    string ngaysinh = "";
                    if (!String.IsNullOrEmpty(item.NgaySinh))
                    {
                        ngaysinh += item.NgaySinh + "/";
                    }
                    if (!String.IsNullOrEmpty(item.ThangSinh))
                    {
                        ngaysinh += item.ThangSinh + "/";
                    }
                    if (!String.IsNullOrEmpty(item.NamSinh))
                    {
                        ngaysinh += item.NamSinh;
                    }
                    rep.hoten.Value = item.TenBNhan.ToUpper();
                    rep.ngaysinh.Value = ngaysinh;
                    if (item.DTuong == "BHYT")
                    {
                        rep.The1.Value = item.SThe.Substring(0, 2);
                        rep.The2.Value = item.SThe.Substring(2, 1);
                        rep.The3.Value = item.SThe.Substring(3, 2);
                        rep.The4.Value = item.SThe.Substring(5);
                        if (!string.IsNullOrEmpty(item.So_eTBM) && item.So_eTBM.Contains("|"))
                        {
                            rep.MasoBHXH.Value = item.So_eTBM.Split('|')[0];
                        }
                        else
                            rep.MasoBHXH.Value = item.So_eTBM;
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(item.So_eTBM) && item.So_eTBM.Contains("|"))
                        {
                            string[] _arr = item.So_eTBM.Split('|');
                            rep.MasoBHXH.Value = _arr[0];
                            if (_arr.Length > 2 && _arr[2].Length > 6)
                            {
                                rep.The1.Value = _arr[2].Substring(0, 2);
                                rep.The2.Value = _arr[2].Substring(2, 1);
                                rep.The3.Value = _arr[2].Substring(3, 2);
                                rep.The4.Value = _arr[2].Substring(5);
                            }
                        }
                        else
                            rep.MasoBHXH.Value = item.So_eTBM;

                    }
                    //rep.bhyt.Value = (item.DTuong == "BHYT") ? item.SThe : item.So_eTBM;
                    rep.gioitinh.Value = item.GTinh;
                    rep.chandoan.Value = item.GhiChu;//DungChung.Bien.MaBV == "27001" ? item.GhiChu : DungChung.Ham.FreshString(item.ChanDoan + "; " + item.BenhKhac);
                    rep.bacsykcb.Value = item.TenCB;
                    rep.PhongKham.Value = item.TenKP;
                    rep.CQ.Value = DungChung.Bien.TenCQ.ToUpper();
                    if (item.NghiOmDen != null && item.NghiOmTu != null)
                    {
                        int day = (item.NghiOmDen.Value.Date - item.NghiOmTu.Value.Date).Days + 1;
                        if (day.ToString().Length == 1)
                        {
                            rep.songaynghi.Value = "0" + day + " ngày";
                        }
                        else
                            rep.songaynghi.Value = day + " ngày";
                        rep.khoangngay.Value = " (Từ ngày: " + item.NghiOmTu.Value.ToShortDateString() + "    đến ngày: " + item.NghiOmDen.Value.ToShortDateString() + " )";
                    }
                    else if (item.NghiOmDen == null && item.NghiOmTu != null)
                    {
                        rep.songaynghi.Value = item.LyDoNghiThaiSan;
                        rep.khoangngay.Value = " (Từ ngày: " + item.NghiOmTu.Value.ToShortDateString() + "   đến hết ngày:                    " + " )";
                    }
                    else rep.khoangngay.Value = " (Từ ngày: " + "                    đến hết ngày:                    " + " )";
                    if (item.NoiLV != null && item.NoiLV != "")
                        rep.donvi.Value = "Đơn vị làm việc: " + item.NoiLV;
                    else rep.donvi.Value = "Đơn vị làm việc: ";
                    rep.soseri.Value = item.SoNghiOm;
                    rep.ngay.Value = DungChung.Ham.NgaySangChu(DateTime.Now, 1);//"Ngày ..... tháng .... năm ........"; //  DungChung.Ham.NgaySangChu(DateTime.Now, 1);

                }
                rep.CreateDocument();
                frmIn frm = new frmIn();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
                #endregion
            }
            else
            {
                BaoCao.rep_NghiOm_TT56_01071_A4 rep = new BaoCao.rep_NghiOm_TT56_01071_A4();

                foreach (var item in kq)
                {
                    if (item.NThan != null)
                    {
                        if (item.NThan.Contains(";"))
                        {
                            string[] arrtt = item.NThan.Split(';');
                            if (arrtt.Length > 1 && !string.IsNullOrEmpty(arrtt[0]))
                            {
                                string[] arrttt = arrtt[0].Split(',');
                                if (arrttt.Length > 0)
                                    rep.hotencha.Value = arrttt[0];
                                if (arrttt.Length > 1)
                                    rep.hotenme.Value = arrttt[1];
                                //if (arrttt.Length > 1 && !string.IsNullOrEmpty(arrttt[0]))
                                //{
                                //    rep.hotencha.Value = arrttt[0];
                                //    rep.hotenme.Value = arrttt[1];
                                //}
                            }
                        }
                        else
                        {
                            if (item.NThan.Contains(","))
                            {
                                string[] arrttt = item.NThan.Split(',');
                                if (arrttt.Length > 0)
                                    rep.hotencha.Value = arrttt[0];
                                if (arrttt.Length > 1)
                                    rep.hotenme.Value = arrttt[1];
                                //if (arrttt.Length > 1 && !string.IsNullOrEmpty(arrttt[0]))
                                //{
                                //    rep.hotencha.Value = arrttt[0];
                                //    rep.hotenme.Value = arrttt[1];
                                //}
                            }
                            else
                                rep.hotencha.Value = item.NThan;
                        }
                    }

                    _mabn = item.MaBNhan;
                    rep.xrBarCode1.Text = _mabn.ToString();
                    rep.idkb.Value = "Số: " + _mabn + "/KCB";
                    string ngaysinh = "";
                    if (!String.IsNullOrEmpty(item.NgaySinh))
                    {
                        ngaysinh += item.NgaySinh + "/";
                    }
                    if (!String.IsNullOrEmpty(item.ThangSinh))
                    {
                        ngaysinh += item.ThangSinh + "/";
                    }
                    if (!String.IsNullOrEmpty(item.NamSinh))
                    {
                        ngaysinh += item.NamSinh;
                    }
                    rep.hoten.Value = item.TenBNhan.ToUpper();
                    rep.ngaysinh.Value = ngaysinh;
                    if (item.DTuong == "BHYT")
                    {
                        rep.The1.Value = item.SThe.Substring(0, 2);
                        rep.The2.Value = item.SThe.Substring(2, 1);
                        rep.The3.Value = item.SThe.Substring(3, 2);
                        rep.The4.Value = item.SThe.Substring(5);
                        if (!string.IsNullOrEmpty(item.So_eTBM) && item.So_eTBM.Contains("|"))
                        {
                            rep.MasoBHXH.Value = item.So_eTBM.Split('|')[0];
                        }
                        else
                            rep.MasoBHXH.Value = item.So_eTBM;
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(item.So_eTBM) && item.So_eTBM.Contains("|"))
                        {
                            string[] _arr = item.So_eTBM.Split('|');
                            rep.MasoBHXH.Value = _arr[0];
                            if (_arr.Length > 2 && _arr[2].Length > 6)
                            {
                                rep.The1.Value = _arr[2].Substring(0, 2);
                                rep.The2.Value = _arr[2].Substring(2, 1);
                                rep.The3.Value = _arr[2].Substring(3, 2);
                                rep.The4.Value = _arr[2].Substring(5);
                            }
                        }
                        else
                            rep.MasoBHXH.Value = item.So_eTBM;

                    }
                    //rep.bhyt.Value = (item.DTuong == "BHYT") ? item.SThe : item.So_eTBM;
                    rep.gioitinh.Value = item.GTinh;
                    rep.chandoan.Value = item.GhiChu;//DungChung.Bien.MaBV == "27001" ? item.GhiChu : DungChung.Ham.FreshString(item.ChanDoan + "; " + item.BenhKhac);
                    rep.bacsykcb.Value = item.TenCB;
                    rep.PhongKham.Value = item.TenKP;
                    if (DungChung.Bien.MaBV == "30303")
                    {
                        var tencqcq = (from name in _data.HTHONGs select name).ToList();
                        DungChung.Bien.TenCQCQ = tencqcq.Select(p => p.TenCQCQ).First().ToString();
                        rep.TenCQCQ.Value = DungChung.Bien.TenCQCQ;
                    }
                    rep.CQ.Value = DungChung.Bien.TenCQ.ToUpper();
                    if (item.NghiOmDen != null && item.NghiOmTu != null)
                    {
                        int day = (item.NghiOmDen.Value.Date - item.NghiOmTu.Value.Date).Days + 1;
                        if (day.ToString().Length == 1)
                        {
                            rep.songaynghi.Value = "0" + day + " ngày";
                        }
                        else
                            rep.songaynghi.Value = day + " ngày";
                        rep.khoangngay.Value = " (Từ ngày: " + item.NghiOmTu.Value.ToShortDateString() + "    đến ngày: " + item.NghiOmDen.Value.ToShortDateString() + " )";
                    }
                    else if (item.NghiOmDen == null && item.NghiOmTu != null)
                    {
                        rep.songaynghi.Value = item.LyDoNghiThaiSan;
                        rep.khoangngay.Value = " (Từ ngày: " + item.NghiOmTu.Value.ToShortDateString() + "   đến hết ngày:                    " + " )";
                    }
                    else rep.khoangngay.Value = " (Từ ngày: " + "                    đến hết ngày:                    " + " )";
                    if (item.NoiLV != null && item.NoiLV != "")
                        rep.donvi.Value = "Đơn vị làm việc: " + item.NoiLV;
                    else rep.donvi.Value = "Đơn vị làm việc: ";
                    rep.soseri.Value = item.SoNghiOm;
                    rep.ngay.Value = DungChung.Bien.MaBV == "27001" ? DungChung.Ham.NgaySangChu(Convert.ToDateTime(item.NgayNghi), DungChung.Bien.FormatDate) : DungChung.Ham.NgaySangChu(DateTime.Now, DungChung.Bien.FormatDate);//"Ngày ..... tháng .... năm ........"; //  DungChung.Ham.NgaySangChu(DateTime.Now, 1);
                    if(DungChung.Bien.MaBV == "27001")
                    {
                        //DateTime now = DateTime.Now;
                        //rep.ngay.Value = "Ngày " + now.Day + " Tháng " + now.Month + " Năm " + now.Year;
                        rep.ngay.Value ="Ngày " + item.NgayKham.Value.ToString("dd") + " Tháng " + item.NgayKham.Value.ToString("MM") + " Năm " + item.NgayKham.Value.ToString("yyyy");
                    }
                }
                rep.CreateDocument();
                frmIn frm = new frmIn();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
            }
            //}
            //else
            //    giaynghiom_30007(IDKB);

        }
        int so1 = 0, so3 = 0;
        private void dtNgayChungNhan_EditValueChanged(object sender, EventArgs e)
        {
            DateTime ngay1 = DateTime.Now;
            int thang = ngay1.Month, nam = ngay1.Year;
            DateTime ngay2 = Convert.ToDateTime("1/" + thang.ToString() + "/" + nam.ToString());
            string nam1 = Convert.ToString(nam);
            if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049")
            {
                var songom = _data.SoPLs.Where(p => p.PhanLoai == 10 && p.NgayNhap >= ngay2 && p.NgayNhap <= ngay1).ToList();
                if (songom.Count > 1)
                {
                    if (_songhiom == "")
                    {
                        so1 = songom.Max(p => p.SoPL1);
                    }
                    else
                    {
                        int so4 = Convert.ToInt32(_songhiom.Substring(9));
                        so1 = songom.Where(p => p.SoPL1 < so4).Max(p => p.SoPL1);
                    }
                    int chenh = 6 - so1.ToString().Length;
                    string so2 = chenh == 0 ? so1.ToString() : (chenh == 1 ? "0" + so1.ToString() : (chenh == 2 ? "00" + so1.ToString() : (chenh == 3 ? "000" + so1.ToString() : (chenh == 4 ? "0000" + so1.ToString() : ("00000" + so1.ToString())))));
                    txtSoGanNhat.Text = DungChung.Bien.MaBV + (thang > 9 ? thang.ToString() : "0" + thang.ToString()) + nam1.Substring(2) + so2;
                }
                else
                {
                    txtSoGanNhat.Text = DungChung.Bien.MaBV + (thang > 9 ? thang.ToString() : "0" + thang.ToString()) + nam1.Substring(2) + "000000";
                }
                if (_songhiom == "")
                {
                    so3 = songom.Count > 0 ? songom.Max(p => p.SoPL1) + 1 : 1;
                    int chenh = 6 - so3.ToString().Length;
                    string so2 = chenh == 0 ? so3.ToString() : (chenh == 1 ? "0" + so3.ToString() : (chenh == 2 ? "00" + so3.ToString() : (chenh == 3 ? "000" + so3.ToString() : (chenh == 4 ? "0000" + so3.ToString() : ("00000" + so3.ToString())))));
                    txtSoChungNhan.Text = DungChung.Bien.MaBV + (thang > 9 ? thang.ToString() : "0" + thang.ToString()) + nam1.Substring(2) + so2;
                    ktso(txtSoChungNhan.Text);
                }
                else
                {
                    string soo = txtSoChungNhan.Text.Substring(9, 6);
                    int so4 = Convert.ToInt32(soo) - 1;
                    int chenh = 6 - so1.ToString().Length;
                    string so2 = chenh == 0 ? so4.ToString() : (chenh == 1 ? "0" + so4.ToString() : (chenh == 2 ? "00" + so4.ToString() : (chenh == 3 ? "000" + so4.ToString() : (chenh == 4 ? "0000" + so4.ToString() : ("00000" + so4.ToString())))));
                    txtSoGanNhat.Text = DungChung.Bien.MaBV + (thang > 9 ? thang.ToString() : "0" + thang.ToString()) + nam1.Substring(2) + so2;
                }
            }
        }
        private void ktso(string x)
        {
            DateTime ngay1 = DateTime.Now;
            int thang = ngay1.Month, nam = ngay1.Year;
            DateTime ngay2 = Convert.ToDateTime("1/" + thang.ToString() + "/" + nam.ToString());
            string nam1 = Convert.ToString(nam);
            var test = _data.BNKBs.Where(p => p.SoNghiOm == x).ToList();
            if (test.Count() > 0)
            {
                so3 = so3 + 1;
                sopl2 = so3;
                int chenh = 6 - so3.ToString().Length;
                string so2 = chenh == 0 ? so3.ToString() : (chenh == 1 ? "0" + so3.ToString() : (chenh == 2 ? "00" + so3.ToString() : (chenh == 3 ? "000" + so3.ToString() : (chenh == 4 ? "0000" + so3.ToString() : ("00000" + so3.ToString())))));
                txtSoChungNhan.Text = DungChung.Bien.MaBV + (thang > 9 ? thang.ToString() : "0" + thang.ToString()) + nam1.Substring(2) + so2;

                string soo = txtSoChungNhan.Text.Substring(9, 6);
                so1 = Convert.ToInt32(soo) - 1;
                chenh = 6 - so1.ToString().Length;
                so2 = chenh == 0 ? so1.ToString() : (chenh == 1 ? "0" + so1.ToString() : (chenh == 2 ? "00" + so1.ToString() : (chenh == 3 ? "000" + so1.ToString() : (chenh == 4 ? "0000" + so1.ToString() : ("00000" + so1.ToString())))));
                txtSoGanNhat.Text = DungChung.Bien.MaBV + (thang > 9 ? thang.ToString() : "0" + thang.ToString()) + nam1.Substring(2) + so2;
            }
        }

        private void labelControl8_Click(object sender, EventArgs e)
        {

        }

        private void txtSoTheBHXH_EditValueChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSoTheBHXH.Text))
                txtSoTheBHXH.Text = txtSoTheBHXH.Text.ToUpper();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                txtGhiChuNghiThaiSan.Enabled = true;
                dateNgayHen.Text = "";
                dateNgayHen.Enabled = false;
            }
            else
            {
                txtGhiChuNghiThaiSan.Enabled = false;
                dateNgayHen.DateTime = DateTime.Now;
                dateNgayHen.Enabled = true;
            }
        }

        private void txtSoThe_EditValueChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSoThe.Text))
                txtSoThe.Text = txtSoThe.Text.ToUpper();
        }
    }
}