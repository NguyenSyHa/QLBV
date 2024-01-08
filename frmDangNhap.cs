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
using System.IO;
using System.Management;
using System.Security.Cryptography;
using QLBV.FormThamSo;
using Microsoft.Win32;
using System.Diagnostics;
using System.Configuration;
using System.Globalization;
using System.Threading;
using QLBV.Utilities.Commons;
using QLBV.Models.Dictionaries.Thuoc;
using System.Drawing.Text;
using QLBV.Providers.Dictionaries.CanBo;
using QLBV.Signature.Models;
using QLBV.Signature.Common;
using System.Threading.Tasks;
using QLBV.Signature.Services;

namespace QLBV
{
    public partial class frmDangNhap : DevExpress.XtraEditors.XtraForm
    {
        public frmDangNhap()
        {
            InitializeComponent();
        }
        string _NgayKichHoat = "";
        string _mabv = "";
        string _tenbv = "";
        public delegate void GoBackForm1(object sender);
        //public event GoBackForm1 OnGoBack;
        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        string licensecode = "";
        string _serial = "";
        #region Kiểm tra active
        private bool ktActive(string seria)
        {
            if (DateTime.Now < new DateTime(2020, 01, 01))// tạm ko check serial
                return true;
            // serial may code
            if (seria == "9AD1A139457509BC36779CC59")
                return true;
            try
            {

                string output1 = QLBV_Library.QLBV_Ham.HardDiskSeriesNumber();
                //string output2 = ConvertStringToSecureCode(output1.Substring(0, 9));
                string output2 = output1.Trim() + "_@1#";
                string output3 = QLBV_Library.QLBV_Ham.ConvertStringToSecureCode(output2);
                if (!string.IsNullOrEmpty(output3) && output3.Length >= 25)
                    licensecode = output3.Substring(0, 25).ToUpper();
                if (licensecode == seria)
                    return true;
                return false;
            }
            catch (Exception)
            {
                return false;
            }

        }

        private StaffProvider _staffProvider;
        public StaffProvider StaffProvider
        {
            get
            {
                if (_staffProvider == null)
                    _staffProvider = new StaffProvider();

                return _staffProvider;
            }
        }

        #endregion
        int testconnect = 0;
        #region enable sql
        private void EnableSQl(bool t)
        {
            //txtMatKhausql.Properties.ReadOnly = t;
            //txtTaiKhoan.Properties.ReadOnly = t;
            //txtTenCSDL.Properties.ReadOnly = t;
            //txtTenMaychu.Properties.ReadOnly = t;


        }
        #endregion
        DateTime lastdate(string path, string taikhoan, string MK)
        {

            DateTime maxdate = new DateTime();
            try
            {
                Frm_Upload.ftp ftpClient = new Frm_Upload.ftp(@"ftp://118.70.117.247/", taikhoan, MK);
                DateTime[] dtime = new DateTime[100];
                DateTime ot = new DateTime();
                //MessageBox.Show("Login thành công");
                string _fileroot = DungChung.Bien.MaBV;
                _fileroot = taikhoan;
                string[] list;
                list = ftpClient.directoryListSimple(_fileroot);
                int i = 0;
                foreach (var a in list)
                {

                    if (a.Contains(".exe"))
                    {
                        string b = "";
                        b = a.Substring(4, 10);
                        try
                        {
                            dtime[i] = DateTime.ParseExact(b, "ddMMyyyyHH", null);
                            i++;
                        }
                        catch
                        {

                        }

                    }
                }
                maxdate = dtime[0];
                for (int k = 1; k < dtime.Length; k++)
                {
                    if (dtime[k] > maxdate)
                        maxdate = dtime[k];
                }

                return maxdate;
            }
            catch
            {
                return maxdate;

            }
        }

        private bool CheckUpdate(string _localPath, string _updatePath)
        {
            bool rs = false;
            // Get the subdirectories for the specified directory.
            DirectoryInfo dirLocal = new DirectoryInfo(_localPath);
            if (!dirLocal.Exists)
            {
                MessageBox.Show(
                    "Source directory does not exist or could not be found: "
                    + _localPath);
                return false;
            }

            DirectoryInfo dirUpdate = new DirectoryInfo(_updatePath);
            if (!dirUpdate.Exists)
            {
                MessageBox.Show(
                    "Update directory does not exist or could not be found: "
                    + _updatePath);
                return false;
            }
            rs = DirectoryCheck(_updatePath, _localPath);

            return rs;
        }

        private bool DirectoryCheck(string sourceDirName, string destDirName)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);

            DirectoryInfo[] dirs = dir.GetDirectories();
            // If the destination directory doesn't exist, create it.
            if (!Directory.Exists(destDirName))
            {
                return true;
            }

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                if (!file.Attributes.HasFlag(FileAttributes.Hidden) && !file.Attributes.HasFlag(FileAttributes.ReadOnly) && file.Name != "AutoUpdate.exe" && file.Name != "Logs.txt")
                {
                    string temppath = Path.Combine(destDirName, file.Name);
                    if (File.Exists(temppath))
                    {
                        FileInfo fileExist = new FileInfo(temppath);
                        if (file.LastWriteTime != fileExist.LastWriteTime)
                        {
                            return true;
                        }
                    }
                    else
                    {
                        return true;
                    }
                }
            }

            foreach (DirectoryInfo subdir in dirs)
            {
                if (!subdir.Attributes.HasFlag(FileAttributes.Hidden) && !subdir.Attributes.HasFlag(FileAttributes.ReadOnly))
                {
                    string temppath = Path.Combine(destDirName, subdir.Name);
                    DirectoryCheck(subdir.FullName, temppath);
                }
            }
            return false;
        }

        [Obsolete]
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (/*ktActive(_serial) &&*/ _ktserial)
            {
                var updatePath = ConfigurationSettings.AppSettings["UPDATE_PATH"];

                if (!string.IsNullOrWhiteSpace(updatePath))
                {
                    if (CheckUpdate(AppDomain.CurrentDomain.BaseDirectory, updatePath) && MessageBox.Show("Có phiên bản mới bạn có muốn cập nhật?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        foreach (var process in Process.GetProcessesByName("CapNhatTuDongQLBV"))
                        {
                            process.Kill();
                        }
                        string path = AppDomain.CurrentDomain.BaseDirectory + "\\CapNhatTuDongQLBV.exe";
                        if (!string.IsNullOrEmpty(path))
                        {
                            ProcessStartInfo startInfo = new ProcessStartInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "CapNhatTuDongQLBV.exe"));
                            startInfo.Arguments = updatePath + "|" + AppDomain.CurrentDomain.BaseDirectory;
                            startInfo.UseShellExecute = false;
                            System.Diagnostics.Process.Start(startInfo);

                            this.Dispose();
                            Application.Exit();
                            return;
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy CapNhatTuDongQLBV.exe");
                        }
                    }
                }

                if (!string.IsNullOrEmpty(txtTenDN.Text))
                {
                    DungChung.Bien.ngayCNhat = _GDcu.ToString("dd/MM/yyyy HH:mm");
                    // tai khoan mac dinh
                    if (txtTenDN.Text.ToLower() == "admin" && txt_MK.Text == "vssoft")
                    {
                        DungChung.Bien.MaCB = "admin";
                        Menu frm = new Menu();
                        this.Hide();
                        frm.ShowDialog();
                        this.Dispose();
                    }
                    //ket thuc tkhoan mac dinh
                    else
                    {
                        string tendn = txtTenDN.Text.Trim();
                        if (!string.IsNullOrEmpty(txt_MK.Text))
                        {
                            if (!string.IsNullOrEmpty(txt_TenMC.Text))
                            {
                                DungChung.Bien.TenServer = txt_TenMC.Text;
                                DungChung.Bien.TenCSDL = txtTenCSDL.Text;
                                DungChung.Bien.accountsql = txtTaiKhoan.Text;
                                DungChung.Bien.passql = txtMatKhausql.Text;

                                DungChung.Bien.StrCon = @"metadata=res://*/QLBVEntity.csdl|res://*/QLBVEntity.ssdl|res://*/QLBVEntity.msl;provider=System.Data.SqlClient;provider connection string=';Data Source=" + txt_TenMC.Text + ";Initial Catalog=" + txtTenCSDL.Text + ";User ID=" + txtTaiKhoan.Text + ";Password=" + txtMatKhausql.Text + ";MultipleActiveResultSets=True';";
                            }
                            QLBVEntities Datacontect = new QLBVEntities(DungChung.Bien.StrCon);
                            string mk = QLBV_Library.QLBV_Ham.ConvertStringToSecureCode(txt_MK.Text);
                            // kiểm tra lại
                            var query = (from TenDN in Datacontect.ADMINs.Where(P => P.TenDN == tendn) select new { TenDN.TenDN, TenDN.CapDo, TenDN.MatK, TenDN.MaCB }).ToList();
                            if (query.Count > 0)
                            {
                                if (query.Where(p => p.MatK == (mk)).ToList().Count > 0)
                                {
                                    string CapDo = "", MaCB = "";
                                    CapDo = query.First().CapDo.ToString();
                                    MaCB = query.First().MaCB.ToString();
                                    // select ma khoa phong trung voi ten dag nhap
                                    var q = (from Ma in Datacontect.CanBoes select new { Ma.MaKP, Ma.MaCB }).Where(p => p.MaCB == (MaCB)).ToList();
                                    int Makp = 0;
                                    if (q.Count > 0)
                                    {
                                        Makp = q.First().MaKP == null ? 0 : q.First().MaKP.Value;
                                        DungChung.Bien.MaKP = Makp;
                                        //DungChung.Bien.MaBV = "04008";
                                        DungChung.Bien.MaCB = MaCB;
                                        DungChung.Bien.TenDN = txtTenDN.Text;
                                        //Load QLBV config
                                        LoadQLBV_Config();
                                        //DungChung.Bien.CapDo = int.Parse(CapDo);
                                        DateTime ngay;
                                        if (DateTime.TryParse(_NgayKichHoat, out ngay))
                                        {
                                            DungChung.Bien.NgayKichHoat = ngay;
                                        }
                                        // luu connect
                                        DungChung.Bien.MaBV = _mabv;
                                        if (!string.IsNullOrEmpty(txt_TenMC.Text))
                                        {
                                            DungChung.Bien.TenCQ = _tenbv;
                                            DungChung.Bien.LoaiPM = r_loaiPM;
                                            DungChung.Bien.TenServer = txt_TenMC.Text;
                                            DungChung.Bien.TenCSDL = txtTenCSDL.Text;
                                            DungChung.Bien.accountsql = txtTaiKhoan.Text;

                                            DungChung.Bien.passql = txtMatKhausql.Text;

                                            DungChung.Bien.StrCon = @"metadata=res://*/QLBVEntity.csdl|res://*/QLBVEntity.ssdl|res://*/QLBVEntity.msl;provider=System.Data.SqlClient;provider connection string=';Data Source=" + DungChung.Bien.TenServer + ";Initial Catalog=" + DungChung.Bien.TenCSDL + ";User ID=" + DungChung.Bien.accountsql + ";Password=" + DungChung.Bien.passql + ";MultipleActiveResultSets=True';";


                                            Program._connect.Close();
                                            Program._connect.Connect();
                                        }
                                        var bv = Datacontect.BenhViens.Where(p => p.MaBV.Trim() == (DungChung.Bien.MaBV)).ToList();
                                        if (bv.Count > 0)
                                        {
                                            if (!bv.First().Connect)
                                                MessageBox.Show(bv.First().TenBV + " - " + bv.First().MaBV + " chưa chọn connect data trong danh mục bệnh viện!");
                                            DungChung.Bien.MaTinh = bv.First().MaTinh.Trim();
                                            DungChung.Bien.MaHuyen = bv.First().MaHuyen.Trim();
                                            string urlBHXH = ConfigurationManager.AppSettings["URL_BHXH"];
                                            if (!string.IsNullOrEmpty(urlBHXH))
                                            {
                                                DungChung.Bien.URL_BHXH = ConfigurationSettings.AppSettings["URL_BHXH"];
                                            }
                                            string urlBHXH_DaoTao = ConfigurationManager.AppSettings["URL_BHXH_DaoTao"];
                                            if (!string.IsNullOrEmpty(urlBHXH_DaoTao))
                                            {
                                                DungChung.Bien.URL_BHXH_DaoTao = ConfigurationSettings.AppSettings["URL_BHXH_DaoTao"];
                                            }
                                            GiamDinhBHXH.BHXH_Model.Service.apiUrlChinh = DungChung.Bien.URL_BHXH;
                                            GiamDinhBHXH.BHXH_Model.Service.apiUrlDaotao = DungChung.Bien.URL_BHXH_DaoTao;
                                            List<HTHONG> hethong = Datacontect.HTHONGs.Where(p => p.MaBV == DungChung.Bien.MaBV).ToList();
                                            var hethongus = Datacontect.HThong_User.Where(p => p.TenDN == DungChung.Bien.TenDN).FirstOrDefault();
                                            if (hethongus != null)
                                            {
                                                if (hethongus.NangCap != null)
                                                {
                                                    if (_ddan != hethongus.NangCap)
                                                    {
                                                        //QLBV_Library.QLBV_Ham.Write_update("Cuong.Update", hethong.First().NangCap + "*" + _GDcu);
                                                    }
                                                }

                                                DungChung.Bien.NguoiLapBieu = hethongus.NguoiLapBieu;
                                                DungChung.Bien.TruongKhoaLS = hethongus.TruongKhoa;
                                                DungChung.Bien.FormatString = QLBV_Library.QLBV_Ham.LayChuoi('/', hethongus.FormatString);

                                                if (hethongus.FormatVn == true)
                                                {
                                                    var vn = CultureInfo.CreateSpecificCulture("vi-VN");
                                                    var customCulture = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
                                                    customCulture.DateTimeFormat = vn.DateTimeFormat;
                                                    customCulture.NumberFormat = vn.NumberFormat;
                                                    Thread.CurrentThread.CurrentCulture = customCulture;
                                                }

                                                //
                                                if (bool.TryParse(QLBV_Library.QLBV_Ham.LayChuoi(';', hethongus.KieuDoc)[0], out DungChung.Bien._Visible_CDHA[0]) == false)
                                                { DungChung.Bien._Visible_CDHA[0] = true; }
                                                if (bool.TryParse(QLBV_Library.QLBV_Ham.LayChuoi(';', hethongus.KieuDoc)[1], out DungChung.Bien._Visible_CDHA[1]) == false)
                                                { DungChung.Bien._Visible_CDHA[1] = true; }

                                                DungChung.Bien.DuongDan = hethongus.DuongDan;
                                                if (hethongus.FormatDate != null)
                                                    DungChung.Bien.FormatDate = hethongus.FormatDate.Value;
                                                if (hethongus.GetICD != null)
                                                    DungChung.Bien.GetICD = hethongus.GetICD.Value;

                                                DungChung.Bien.HDSDDonThuoc = hethongus.HDSDThuoc;
                                                DungChung.Bien.ThuKho = hethongus.ThuKho;
                                                DungChung.Bien.MaKho = hethongus.MaKho == null ? 0 : hethongus.MaKho.Value;
                                                DungChung.Bien.KeToanVP = hethongus.KeToanVP;
                                                DungChung.Bien.KeToanVPnt = hethongus.KeToanVPnt;
                                                DungChung.Bien.KeToanVPdv = hethongus.KeToanVPdv;
                                                DungChung.Bien.UserPasswordHDDT = hethongus.HDDTInfo;
                                                //
                                            }
                                            else
                                            {
                                                MessageBox.Show("Tài khoản chưa thiết lập hệ thống");
                                            }
                                            if (hethong.Count > 0)
                                            {
                                                if (hethong.First().InPhoi != null)
                                                {
                                                    DungChung.Bien.InPhoi = hethong.First().InPhoi.Value;
                                                    DungChung.Bien.TamThuCLS = hethong.First().InPhoi.Value;
                                                }
                                                if (hethong.First().NgayGiaMoi != null)
                                                    DungChung.Bien.ngayGiaMoi = hethong.First().NgayGiaMoi.Value;
                                                if (hethong.First().NgayGiaMoiDV != null)
                                                    DungChung.Bien.ngayGiaMoiDV = hethong.First().NgayGiaMoiDV.Value;

                                                if (hethong.First().NgayGiaMoiTT39 != null)
                                                    DungChung.Bien.ngayGiaMoiTT39 = hethong.First().NgayGiaMoiTT39.Value;

                                                //if (DungChung.Bien.MaTinh == "30")
                                                //    DungChung.Bien.ngayGiaMoi = Convert.ToDateTime("01/11/2016");
                                                if (hethong.First().LamTron != null)
                                                    DungChung.Bien.LamTronSo = hethong.First().LamTron.Value;
                                                else
                                                    DungChung.Bien.LamTronSo = 0;
                                                DungChung.Bien.GiamDinhBH = hethong.First().GiamDinhBH;
                                                DungChung.Bien.GiamDoc = hethong.First().GiamDoc;
                                                if (hethong.First().GHanTT100 != null)
                                                    DungChung.Bien.GHanTT100 = hethong.First().GHanTT100.Value;
                                                DungChung.Bien.DiaChi = hethong.First().DiaChi;
                                                DungChung.Bien.KeToanTruong = hethong.First().KeToanTruong;
                                                DungChung.Bien.SDTCQ = hethong.First().SDT;
                                                DungChung.Bien.TruongKhoaDuoc = hethong.First().TruongKhoaDuoc;
                                                DungChung.Bien.UrlPID = hethong.First().UrlPID;
                                                DungChung.Bien.UrlUploadHSSK = hethong.First().UrlUploadHSSK;

                                                DungChung.Bien.ChiTamUng = hethong.First().ChiTamUng;
                                                //if (hethong.First().PPXuat != null)
                                                if (hethong.First().PPXuat != null)
                                                {
                                                    DungChung.Bien.PPXuat_BHXH = FormThamSo.us_hethong.PhuongPGuiBHXH(hethong.First().PPXuat);
                                                    DungChung.Bien.PPXuat = hethong.First().PPXuat.Value;

                                                }
                                                DungChung.Bien.formatAge = hethong.First().FormatAge;
                                                DungChung.Bien.TenCQCQ = hethong.First().TenCQCQ ?? "";
                                                DungChung.Bien.MaNSach = hethong.First().MaNganSach;
                                                DungChung.Bien.DiaDanh = hethong.First().DiaDanh;
                                                DungChung.Bien.TenCQrg = hethong.First().TenCQrg;
                                                DungChung.Bien.GiamDinhBH2 = hethong.First().GiamDinhBH2;
                                                DungChung.Bien.keNhieuKho = hethong.First().KeDonNhieuKho;
                                                DungChung.Bien.PP_SoBA = hethong.First().SoBenhAn;
                                                DungChung.Bien.PP_SoCV = hethong.First().SoChuyenVien;
                                                DungChung.Bien.PP_SoLT = hethong.First().SoLuuTru;
                                                //DungChung.Bien.PP_SoRV = hethong.First().so;
                                                DungChung.Bien.PP_SoVV = hethong.First().SoVaoVien;
                                                DungChung.Bien.PP_SoKB = hethong.First().SoKB;
                                                string[] MauIn = hethong.First().MauIn == null ? new string[] { } : hethong.First().MauIn.Split(';');
                                                if (MauIn.Length > 4 && !string.IsNullOrEmpty(MauIn[4]))
                                                {
                                                    int mau = Convert.ToInt16(MauIn[4].Trim());
                                                    DungChung.Bien.PPTinhTon = mau;
                                                }
                                                try
                                                {
                                                    string[] giotu = hethong.First().GioTu.Split(';');
                                                    string[] gioden = hethong.First().GioDen.Split(';');
                                                    string[] gioPhutTu1 = giotu[0].Split(':');
                                                    string[] gioPhutTu2 = giotu[1].Split(':');
                                                    string[] gioPhutDen1 = gioden[0].Split(':');
                                                    string[] gioPhutDen2 = gioden[1].Split(':');
                                                    if (!string.IsNullOrEmpty(gioPhutDen1[0]))
                                                        DungChung.Bien.GioDen[0] = Convert.ToInt32(gioPhutDen1[0]);
                                                    if (!string.IsNullOrEmpty(gioPhutDen1[1]))
                                                        DungChung.Bien.PhutDen[0] = Convert.ToInt32(gioPhutDen1[1]);
                                                    if (!string.IsNullOrEmpty(gioPhutDen2[0]))
                                                        DungChung.Bien.GioDen[1] = Convert.ToInt32(gioPhutDen2[0]);
                                                    DungChung.Bien.PhutDen[1] = Convert.ToInt32(gioPhutDen2[1]);
                                                    if (!string.IsNullOrEmpty(gioPhutTu1[0]))
                                                        DungChung.Bien.GioTu[0] = Convert.ToInt32(gioPhutTu1[0]);
                                                    if (!string.IsNullOrEmpty(gioPhutTu1[1]))
                                                        DungChung.Bien.PhutTu[0] = Convert.ToInt32(gioPhutTu1[1]);
                                                    if (!string.IsNullOrEmpty(gioPhutTu2[0]))
                                                        DungChung.Bien.GioTu[1] = Convert.ToInt32(gioPhutTu2[0]);
                                                    if (!string.IsNullOrEmpty(gioPhutTu1[1]))
                                                        DungChung.Bien.PhutTu[1] = Convert.ToInt32(gioPhutTu2[1]);
                                                }
                                                catch (Exception)
                                                {
                                                }

                                                //
                                                DungChung.Ham._listBC = FormThamSo.us_menubc.SetDM();
                                            }
                                            else
                                            {
                                                MessageBox.Show("Chưa thiết lập hệ thống");

                                            }
                                            this.Hide();

                                            Menu frm = new Menu();
                                            LuuDN();

                                            // Đăng nhập misa
                                            //LoginInfoMisa(DungChung.Bien.MaCB);
                                            //Task.Run(async () => await Signature.Signature.LoginAndUserCertificateInfo(ConfigSign.LoginInfoMisa)).Wait();

                                            //QLBV.Signature.Common.AppApi.RefreshToken = () =>
                                            //{
                                            //    RefreshTokenMisa();
                                            //};

                                            frm.ShowDialog();

                                            this.Dispose();
                                        }
                                        else
                                        {
                                            MessageBox.Show("Mã bệnh viện chưa có trong danh mục bệnh viện");
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Tên đăng nhập không có trong danh sách cán bộ");
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Sai mật khẩu");
                                    txt_MK.Focus();
                                }
                            }
                            else
                            {
                                MessageBox.Show("Tên đăng nhập không tồn tại, vui lòng nhập tên khác!");
                                txtTenDN.Focus();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Chưa nhập mật khẩu");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Chưa nhập tên đăng nhập");
                    txtTenDN.Focus();
                }
            }
            else
            {
                DialogResult _result = MessageBox.Show("Phần mềm chưa được cấp phép! Đăng ký ngay?", "Active", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (_result == DialogResult.OK)
                {
                    this.Hide();
                    frm_KichHoat frm = new frm_KichHoat();
                    frm.ShowDialog();
                    this.Close();
                }
            }
        }

        //public void LoginInfoMisa(string code)
        //{
        //    var staff = StaffProvider.GetStaffByCode(code);
        //    var login = new LoginModel()
        //    {
        //        Username = staff.Email,
        //        Password = Security.Decrypt(staff.MKChuKySo),
        //        PhoneNumber = staff.SoDT,
        //        FileName = null,
        //        Id = Guid.NewGuid().ToString(),
        //        FirstName = staff.TenCB,
        //    };

        //    if (staff.ChuKySo != null
        //        && staff.ChuKySo.Length > 0)
        //    {
        //        login.Image = new ImageModel()
        //        {
        //            Img = staff.ChuKySo,
        //            SignatureImage = staff.ChuKySo != null ? Convert.ToBase64String(staff.ChuKySo) : null
        //        };
        //    }

        //    if (!string.IsNullOrEmpty(login.Username)
        //        && !string.IsNullOrWhiteSpace(login.Username)
        //        && !string.IsNullOrEmpty(login.Password)
        //        && !string.IsNullOrWhiteSpace(login.Password))
        //    {
        //        ConfigSign.LoginInfoMisa = login;
        //    }
        //}

        public void RefreshTokenMisa()
        {
            Task.Run(async () => await new AuthService().RefreshAccessTokenAsync(ConfigSign.LoginESign.Data.RefreshToken)).Wait();
        }

        public void LoadQLBV_Config()
        {
            int sbn = 0;
            if (ConfigurationManager.AppSettings["SoBenhNhanHienThi"] != null && int.TryParse(ConfigurationManager.AppSettings["SoBenhNhanHienThi"], out sbn))
                DungChung.Bien.SoBenhNhanHienThi = sbn;
            if (ConfigurationManager.AppSettings["URL_POS_AGRIBANK"] != null)
                DungChung.Bien.URL_POS_AGRIGANK = ConfigurationManager.AppSettings["URL_POS_AGRIBANK"];
            if (ConfigurationManager.AppSettings["F1"] != null)
                DungChung.Bien.F1 = ConfigurationManager.AppSettings["F1"];
            if (ConfigurationManager.AppSettings["F2"] != null)
                DungChung.Bien.F2 = ConfigurationManager.AppSettings["F2"];
            if (ConfigurationManager.AppSettings["F3"] != null)
                DungChung.Bien.F3 = ConfigurationManager.AppSettings["F3"];
            if (ConfigurationManager.AppSettings["F4"] != null)
                DungChung.Bien.F4 = ConfigurationManager.AppSettings["F4"];
            if (ConfigurationManager.AppSettings["F5"] != null)
                DungChung.Bien.F5 = ConfigurationManager.AppSettings["F5"];
        }

        public string r_chuoi = "";
        public string r_serial = "";
        public string r_serialmc = "";
        public string r_ngaydk = "";
        public string r_mabv = "";
        public string r_tenbv = "";
        public string r_mass = "";
        public string r_loaiPM = "QLBV";
        //public  struct TTDV
        //{
        //    public string chuoi = "";
        //    public string _serial = "";
        //    public string _serialmc = "";
        //    public string ngaydk = "";
        //    public string _mabv = "";
        //    public string _tenbv = "";
        //    public string mass = "";
        //    public TTDV(string x1, string x2, string x3, string x4, string x5, string x6)
        //    {
        //        _tenbv = x1;
        //        _mabv = x2;
        //        ngaydk = x3;
        //        _serial = x4;
        //        _serialmc = x5;

        //    }
        //}
        // lấy số pm
        public string CatChuoi(string chuoi, char kytu)
        {
            //TTDV _ttdv = new TTDV();
            string chuoi_dau = "";
            string[] chuoi_tach = chuoi.Split(new Char[] { kytu });
            int i = 0;
            foreach (string s in chuoi_tach)
            {

                if (s.Trim() != "")
                    chuoi_dau += s;
                i++;
                switch (i)
                {
                    case 1:
                        r_tenbv = s;
                        break;
                    case 2:
                        r_mabv = s;
                        break;
                    case 3:
                        r_ngaydk = s;
                        break;
                    case 4:
                        r_serial = s;
                        break;
                    case 5:
                        r_serialmc = s;
                        break;
                    case 6:
                        r_loaiPM = s;
                        break;
                }
            }
            return chuoi_dau;
        }
        public string CatChuoi_update(string chuoi, char kytu)
        {
            //TTDV _ttdv = new TTDV();
            string chuoi_dau = "";
            string[] chuoi_tach = chuoi.Split(new Char[] { kytu });
            int i = 0;
            foreach (string s in chuoi_tach)
            {

                if (s.Trim() != "")
                    chuoi_dau += s;
                i++;
                switch (i)
                {
                    case 1:
                        _ddan = s;
                        break;
                    case 2:
                        _update = s;
                        break;
                }

            }
            return chuoi_dau;
        }
        bool _ktserial = false;
        string _ddan = "", _update = ""; DateTime _GDcu = Convert.ToDateTime("01/01/2001");
        public string[] TTKNFTP = new string[3] { "", "", "" };
        private void frmDangNhap_Load(object sender, EventArgs e)
        {
            txtTenDN.Focus();
            txt_MK.Text = Convert.ToString(txtMatKhau.Text);
            this.txtMatKhausql.PasswordChar = '*';
            this.txt_MK.PasswordChar = '*';
            //kiểm tra co file 
            try
            {
                string path = "C:\\Program Files\\VSSoft\\VSSOFT.SERIAL\\Serial.exe";
                System.Diagnostics.Process.Start(path);
                //
                System.Diagnostics.Process p = new System.Diagnostics.Process();
                p.StartInfo.FileName = "C:\\Program Files\\VSSoft\\VSSOFT.SERIAL\\Serial.exe";
                p.StartInfo.Arguments = "C:\\Program Files\\VSSoft\\VSSOFT.SERIAL\\Serial.exe";
                p.Start();
                p.Close();
                _ktserial = true;
            }
            catch (Exception)
            {
                try
                {
                    string path = "C:\\Program Files (x86)\\VSSoft\\VSSOFT.SERIAL\\Serial.exe";
                    System.Diagnostics.Process.Start(path);
                    //
                    System.Diagnostics.Process p = new System.Diagnostics.Process();
                    p.StartInfo.FileName = "C:\\Program Files (x86)\\VSSoft\\VSSOFT.SERIAL\\Serial.exe";
                    p.StartInfo.Arguments = "C:\\Program Files (x86)\\VSSoft\\VSSOFT.SERIAL\\Serial.exe";
                    p.Start();
                    p.Close();
                    _ktserial = true;
                }
                catch (Exception)
                {
                    MessageBox.Show("Không tìm thấy file serial.exe");
                    _ktserial = false;
                }
            }

            try
            {
                string path = AppDomain.CurrentDomain.BaseDirectory + "\\QLBV.exe";
                _GDcu = File.GetLastWriteTime(path);
            }
            catch (Exception)
            {
                MessageBox.Show("Không tìm thấy file QLBV.exe");
            }

            string chuoi = QLBV_Library.QLBV_Ham.Read_Update("Cuong.Update", 1);
            TTKNFTP = QLBV_Library.QLBV_Ham.LayChuoi('*', chuoi);

            for (int i = 0; i < 3; i++)
            {
                if (TTKNFTP[i] == null)
                    TTKNFTP[i] = "";
            }
            CatChuoi_update(chuoi, '*');

            //luu user nang cap
            string dateGDcu = _GDcu.ToString("ddMMyyyyHH");

            // lấy số PM
            string chuoiten = DungChung.Ham.Read("cuong", 1);
            chuoiten = CatChuoi(chuoiten, '*');

            try
            {
                _serial = r_serial;
                _NgayKichHoat = r_ngaydk;
                _mabv = r_mabv;
                _tenbv = r_tenbv;
                //this.ClientSize = new System.Drawing.Size(400, 200);

                string acc_pass = "";
                acc_pass = QLBV_Library.QLBV_Ham.Read_Update("Cuong.acc", 1);
                string[] acc_passArr = new string[3] { "", "", "" };
                acc_passArr = acc_pass.Split('*');
                if (!string.IsNullOrEmpty(acc_passArr[2]) && acc_passArr[2] == "1")
                {
                    txt_MK.Text = acc_passArr[1];
                    txtTenDN.Text = acc_passArr[0];
                    chkGhiNho.Checked = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load tài khoản ghi nhớ: " + ex.Message);
            }

            try
            {
                string _connect = "";
                _connect = QLBV_Library.QLBV_Ham.Read_Update("Cuong.Connect", 1);
                string[] _connectArr = new string[4] { "", "", "", "" };
                if (!string.IsNullOrEmpty(_connect))
                    _connectArr = _connect.Split('*');
                txt_TenMC.Text = _connectArr[0];
                txtTenCSDL.Text = _connectArr[1];
                txtTaiKhoan.Text = _connectArr[2];
                txtMatKhausql.Text = _connectArr[3];

                if (!string.IsNullOrEmpty(txt_TenMC.Text))
                {
                    DungChung.Bien.TenServer = txt_TenMC.Text;
                    DungChung.Bien.TenCSDL = txtTenCSDL.Text;
                    DungChung.Bien.accountsql = txtTaiKhoan.Text;
                    DungChung.Bien.passql = txtMatKhausql.Text;

                    DungChung.Bien.StrCon = @"metadata=res://*/QLBVEntity.csdl|res://*/QLBVEntity.ssdl|res://*/QLBVEntity.msl;provider=System.Data.SqlClient;provider connection string=';Data Source=" + DungChung.Bien.TenServer + ";Initial Catalog=" + DungChung.Bien.TenCSDL + ";User ID=" + DungChung.Bien.accountsql + ";Password=" + DungChung.Bien.passql + ";MultipleActiveResultSets=True';";

                    Program._connect.Close();
                    Program._connect.Connect();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi! không tạo được biến connect: " + ex.Message);
                if (Program._connect.isConnect)
                    Program._connect.Close();
            }
        }

        int i = 0;
        #region luu dang nhap
        private void LuuDN()
        {
            if (chkGhiNho.Checked)
            {
                string _acc = txtTenDN.Text.Trim() + "*" + txt_MK.Text + "*" + "1";
                QLBV_Library.QLBV_Ham.Write_update("Cuong.acc", _acc);
            }
            else
            {
                string _acc = txtTenDN.Text.Trim() + "*" + txt_MK.Text + "*" + "0";
                QLBV_Library.QLBV_Ham.Write_update("Cuong.acc", _acc);
            }

        }
        #endregion
        #region luu connect
        private void LuuConnect()
        {
            try
            {
                string _chuoikn = txt_TenMC.Text.Trim() + "*" + txtTenCSDL.Text.Trim() + "*" + txtTaiKhoan.Text.Trim() + "*" + txtMatKhausql.Text;
                //QLBV_Library.QLBV_Ham.Write_update("Cuong.Connect", _chuoikn);
                Write_update("Cuong.Connect", _chuoikn);
            }
            catch
            {
                MessageBox.Show("Lỗi lưu connect");
            }
        }

        public static bool Write_update(string KeyName, object Value)
        {
            try
            {
                RegistryKey localMachine = Registry.LocalMachine;
                string subkey = @"SOFTWARE\" + KeyName;
                localMachine.CreateSubKey(subkey).SetValue(KeyName.ToUpper(), Value);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion
        private void btnMorong_Click(object sender, EventArgs e)
        {
        }
        private void btnKetnoi_Click(object sender, EventArgs e)
        {
            try
            {
                DungChung.Bien.TenServer = txt_TenMC.Text;
                DungChung.Bien.TenCSDL = txtTenCSDL.Text;
                DungChung.Bien.accountsql = txtTaiKhoan.Text;
                DungChung.Bien.passql = txtMatKhausql.Text;

                DungChung.Bien.StrCon = @"metadata=res://*/QLBVEntity.csdl|res://*/QLBVEntity.ssdl|res://*/QLBVEntity.msl;provider=System.Data.SqlClient;provider connection string=';Data Source=" + DungChung.Bien.TenServer + ";Initial Catalog=" + DungChung.Bien.TenCSDL + ";User ID=" + DungChung.Bien.accountsql + ";Password=" + DungChung.Bien.passql + ";MultipleActiveResultSets=True';";


                QLBVEntities Datacontect = new QLBVEntities(DungChung.Bien.StrCon);
                var ds = Datacontect.ADMINs.ToList();
                LuuConnect();
                MessageBox.Show("Tạo kết nối thành công!");
                pcxThoat2.Visible = false;
                P_KetNoi.Visible = false;
            }
            catch (Exception ex)
            {
                LibraryStore.WriteLog.Error(ex);
                MessageBox.Show("Lỗi! không kết nối được tới server: " + ex.Message);
            }

            //c2
        }
        private void txtMatKhau_EditValueChanged(object sender, EventArgs e)
        {
            if (txtTenDN.Text.ToLower() == "admin" && txtMatKhau.Text == "vssoft")
            {
                EnableSQl(false);

            }
            else
            {
                EnableSQl(true);
            }
        }
        private void frmDangNhap_FormClosed(object sender, FormClosedEventArgs e)
        {
            //File.Delete("C:\\VSS\\Serial.txt");
        }
        private void btnMoRong2_Click(object sender, EventArgs e)
        {
            btnMorong_Click(sender, e);
        }
        private void labelControl2_Click(object sender, EventArgs e)
        {
            P_KetNoi.Visible = true;
            pcxThoat2.Visible = true;
            txt_TenMC.Text = Convert.ToString(txt_TenMC.Text);
            txtTenCSDL.Focus();
        }
        private void rjButton1_Click(object sender, EventArgs e)
        {
            txtMatKhau.Text = Convert.ToString(txt_MK.Text);
            btnOK_Click(sender, e);
        }

        private void rjTextBox1_Leave_1(object sender, EventArgs e)
        {
            txtMatKhau.Text = Convert.ToString(txt_MK.Text);
            if (txtTenDN.Text.ToLower() == "admin" && txtMatKhau.Text == "vssoft")
            {
                EnableSQl(false);
            }
            else
            {
                EnableSQl(true);
            }
            if (txtTenDN.Text == "")
            {
                txtTenDN.Text = "Tên đăng nhập";
                txtTenDN.ForeColor = Color.DarkGray;
            }
        }

        private void txtTenDN_Enter(object sender, EventArgs e)
        {
            if (txtTenDN.Text == "Tên đăng nhập")
            {
                txtTenDN.Text = "";
                txtTenDN.ForeColor = Color.Black;
            }
        }

        private void txt_MK_Enter(object sender, EventArgs e)
        {
            if (txt_MK.Text == "Mật khẩu")
            {
                txt_MK.Text = "";
                txt_MK.ForeColor = Color.Black;
            }
        }

        private void txt_MK_Leave(object sender, EventArgs e)
        {

            if (txt_MK.Text == "")
            {
                txt_MK.Text = "Mật khẩu";
                txt_MK.ForeColor = Color.DarkGray;
            }
        }

        private void txt_TenMC_Enter(object sender, EventArgs e)
        {
            if (txt_TenMC.Text == "Tên máy chủ")
            {
                txt_TenMC.Text = "";
                txt_TenMC.ForeColor = Color.Black;
            }
        }

        private void txt_TenMC_Leave(object sender, EventArgs e)
        {
            if (txt_TenMC.Text == "")
            {
                txt_TenMC.Text = "Tên máy chủ";
                txt_TenMC.ForeColor = Color.DarkGray;
            }
        }

        private void txtTenCSDL_Enter(object sender, EventArgs e)
        {
            if (txtTenCSDL.Text == "Tên CSDL")
            {
                txtTenCSDL.Text = "";
                txtTenCSDL.ForeColor = Color.Black;
            }
        }

        private void txtTenCSDL_Leave(object sender, EventArgs e)
        {
            if (txtTenCSDL.Text == "")
            {
                txtTenCSDL.Text = "Tên CSDL";
                txtTenCSDL.ForeColor = Color.DarkGray;
            }
        }

        private void txtTaiKhoan_Enter(object sender, EventArgs e)
        {
            if (txtTaiKhoan.Text == "Tài khoản")
            {
                txtTaiKhoan.Text = "";
                txtTaiKhoan.ForeColor = Color.Black;
            }
        }

        private void txtTaiKhoan_Leave(object sender, EventArgs e)
        {
            if (txtTaiKhoan.Text == "")
            {
                txtTaiKhoan.Text = "Tài khoản";
                txtTaiKhoan.ForeColor = Color.DarkGray;
            }
        }

        private void txtMatKhausql_Enter(object sender, EventArgs e)
        {
            if (txtMatKhausql.Text == "Mật khẩu")
            {
                txtMatKhausql.Text = "";
                txtMatKhausql.ForeColor = Color.Black;
            }
        }

        private void txtMatKhausql_Leave(object sender, EventArgs e)
        {
            if (txtMatKhausql.Text == "")
            {
                txtMatKhausql.Text = "Mật khẩu";
                txtMatKhausql.ForeColor = Color.DarkGray;
            }
        }

        private void pcxThoat_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void txt_TenMC_Click(object sender, EventArgs e)
        {

        }

        private void pcxThoat2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void labelControl1_Click(object sender, EventArgs e)
        {
            P_KetNoi.Visible = false;
            P_DangNhap.Visible = true;
            txtTenDN.Focus();
        }

        private void btnKetnoi2_Click(object sender, EventArgs e)
        {
            btnKetnoi_Click(sender, e);
        }
    }
}