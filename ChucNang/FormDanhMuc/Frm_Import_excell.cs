using System;
using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Diagnostics;
using QLBV.FormDanhMuc;

namespace QLBV.ChucNang.FormDanhMuc
{
    public partial class Frm_Import_excell : DevExpress.XtraEditors.XtraForm
    {
        public Frm_Import_excell()
        {
            InitializeComponent();
        }
        private class Dv : DichVu
        {



        }
        QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private string[] DmCanNhap = new string[] { "Dược", "Khoa Phòng", "Cán bộ" }; // Thêm tự add theo
        int _NhapDanhMuc = 0;
        /// <summary>
        ///  f12 xem chi tiết DmCanNhap
        /// </summary>
        /// <param name="NhapDanhMuc">Cho danh mục nào </param>
        public Frm_Import_excell(int NhapDanhMuc)
        {
            InitializeComponent();
            switch (NhapDanhMuc)
            {
                case 0:
                    XtraMessageBox.Show("lỗi! xảy ra xin vui lòng gọi hỗ trợ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Dispose();
                    break;
                case 1:
                    this.Text = "Nhập excell cho danh mục dược";
                    DmDuoc.DataSource = data.DichVus.Where(p => p.MaDV == -1).ToList();
                    grcDichVu.DataSource = DmDuoc;
                    break;

            }
            _NhapDanhMuc = NhapDanhMuc;
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void Frm_Import_excell_Load(object sender, EventArgs e)
        {
            if (DungChung.Bien.MaBV == "27022")
            {
                colPhanLoaiT.VisibleIndex = 38;
            }
            LoadDK();
        }

        private void btnXuatFileMau_Click(object sender, EventArgs e)
        {
            SaveFileDialog sv = new SaveFileDialog();
            sv.Filter = "xlsx files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
            if (sv.ShowDialog() == DialogResult.OK)
            {
                grcDichVu.ExportToXlsx(sv.FileName);
                try
                {
                    Process.Start(sv.FileName);
                }
                catch (Exception ex)
                {

                    DungChung.Ham.Utilities.WriteLogError("Không tìm thấy file excell.exe: " + ex);
                }

            }

        }
        private class TrongDM { public int id { get; set; } public string TenDM { get; set; } }



        List<TrongDM> TrongDm = new List<TrongDM>();
        private void LoadDK()
        {
            {
                //lupMaDuongDung.Items.Add(data.DichVus.Select(p => p.MaDuongDung).ToList());
            }
            {
                lupTieuNhom.DisplayMember = "TenTN";
                lupTieuNhom.ValueMember = "IdTieuNhom";
                lupTieuNhom.DataSource = (from tn in data.TieuNhomDVs.Where(p => p.Status == 1 && p.IDNhom == 4) select new { tn.IdTieuNhom, tn.TenTN }).ToList();
            }
            {
                lupMaDuongDung.DisplayMember = "MaDuongDung";
                lupMaDuongDung.ValueMember = "MaDuongDung";
                lupMaDuongDung.DataSource = (from dd in data.DuongDungs select new { dd.MaDuongDung }).ToList();
            }
            {
                lupNhom.DisplayMember = "TenNhom";
                lupNhom.ValueMember = "IdTieuNhom";
                lupNhom.DataSource = (from nhom in _data.NhomDVs.Where(p => p.Status == 1)
                                      join tn in _data.TieuNhomDVs on nhom.IDNhom equals tn.IDNhom
                                      select new { TenNhom = nhom.TenNhom, IDTieuNhom = tn.IdTieuNhom }).ToList();


            }

            {
                NhaSX.DisplayMember = "TenDonVi";
                NhaSX.ValueMember = "TenDonVi";
                NhaSX.DataSource = (from a in _data.DonVis select new { TenDonVi = a.TenDonVi }).ToList();

            }
            {
                lupNhaCC.DisplayMember = "TenCC";
                lupNhaCC.ValueMember = "MaCC";
                lupNhaCC.DataSource = (from a in _data.NhaCCs select new { TenCC = a.TenCC, MaCC = a.MaCC }).ToList();


            }
            {

                lupPhanLoai.DisplayMember = "TenNhom";
                lupPhanLoai.ValueMember = "IDNhom";
                lupPhanLoai.DataSource = (from a in _data.NhomDVs.Where(p => p.Status == 1) select new { IDNhom = a.IDNhom, TenNhom = a.TenNhom }).ToList();
            }
            {
                TrongDm.Add(new TrongDM() { id = 0, TenDM = " Ngoài DM" });
                TrongDm.Add(new TrongDM() { id = 1, TenDM = " Trong DM" });
                TrongDm.Add(new TrongDM() { id = 2, TenDM = " Chi phí kèm DV" });
                LupTrongDM.DisplayMember = "TenDM";
                LupTrongDM.ValueMember = "id";
                LupTrongDM.DataSource = TrongDm.ToList();
            }

        }
        List<Dv> dmduoc = new List<Dv>();
        class CheckTN
        {
            public int checkTenTN { get; set; }
        }
        class CheckCC
        {
            public String checkMaCC { get; set; }
        }
        class CheckNhom
        {
            public int checkIDNhom { get; set; }
        }
        List<CheckCC> _lscheckCC = new List<CheckCC>();
        List<CheckNhom> _lscheckN = new List<CheckNhom>();
        List<CheckTN> _lscheckTN = new List<CheckTN>();
        private void btnChonFile_Click(object sender, EventArgs e)
        {
            dmduoc.Clear();
            _lscheckTN.Clear();
            var Nhomdvs = (from tn in data.TieuNhomDVs.Where(p => p.Status == 1 && p.IDNhom == 4) select new { tn.IdTieuNhom, tn.TenTN }).ToList();
            OpenFileDialog dl = new OpenFileDialog();
            dl.Filter = "xlsx files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
            if (dl.ShowDialog() == DialogResult.OK)
            {
                #region Thuốc Đông Y
                if (chk_DongY.Checked)
                {
                    var q = DungChung.Ham.LoadXlsx(dl.FileName, 45, "Danh mục dược");
                    if (DungChung.Bien.MaBV == "27022")
                        q = DungChung.Ham.LoadXlsx(dl.FileName, 46, "Danh mục dược"); 
                    int f = q.GetLength(0);
                    for (int i = 0; i < f; i++)
                    {
                        if (!string.IsNullOrEmpty(q[i, 0].ToString()))
                        {
                            Dv dv = new Dv();
                            string tendv = q[i, 0].ToString();
                            if (tendv != "0")
                            {
                                dv.TenDV = tendv;
                            }
                            else
                            {
                                MessageBox.Show("Trong file có thuốc chưa nhập tên DV!!");
                                return;
                            }
                            string MaNoiBo = q[i, 1].ToString();
                            if (dmduoc.Count > 0)
                            {
                                foreach (var dm in dmduoc)
                                {
                                    if (dm.MaTam == MaNoiBo)
                                    {
                                        MessageBox.Show("Thuốc " + dm.TenDV + " trùng mã nội bộ!!");
                                        return;
                                    }
                                }
                            }
                            dv.MaTam = MaNoiBo;
                            dv.SoDK = q[i, 2].ToString();
                            dv.SoTT = Convert.ToInt32(q[i, 3].ToString());
                            dv.NuocSX = q[i, 4].ToString();
                            dv.MaQD = q[i, 5].ToString();
                            dv.TieuChuan = q[i, 6].ToString();
                            string Tentn = q[i, 7].ToString();
                            #region ktra TenTN
                            foreach (var a in Nhomdvs)
                            {
                                if (a.TenTN == Tentn)
                                {
                                    CheckTN b = new CheckTN();
                                    b.checkTenTN = Nhomdvs.Where(p => p.TenTN.Contains(Tentn)).FirstOrDefault().IdTieuNhom;
                                    _lscheckTN.Add(b);
                                }
                            }
                            if (_lscheckTN.Count() > 0)
                            {
                                dv.IdTieuNhom = Nhomdvs.Where(p => p.TenTN.Contains(Tentn)).FirstOrDefault().IdTieuNhom;
                            }
                            else
                            {
                                MessageBox.Show("Tên TN không đúng!!");
                                return;
                            }
                            #endregion
                            dv.MaDuongDung = q[i, 8].ToString();
                            dv.DuongD = q[i, 9].ToString();
                            dv.SoTTqd = q[i, 10].ToString();
                            dv.QCPC = q[i, 11].ToString();
                            dv.BHTT = q[i, 12] != null ? int.Parse(q[i, 12].ToString()) : 0;
                            dv.TenHC = q[i, 13].ToString();
                            string Macc = q[i, 14].ToString();
                            #region Ktra tenThau
                            var nhacc = (from cc in data.NhaCCs select new { cc.MaCC, cc.TenCC }).ToList();
                            foreach (var a in nhacc)
                            {
                                if (a.TenCC == Macc)
                                {
                                    CheckCC b = new CheckCC();
                                    b.checkMaCC = nhacc.Where(p => p.TenCC.Contains(Macc)).FirstOrDefault().MaCC;
                                    _lscheckCC.Add(b);
                                }
                            }
                            if (_lscheckCC.Count() > 0)
                            {
                                dv.MaCC = nhacc.Where(p => p.TenCC.Contains(Macc)).FirstOrDefault().MaCC;
                            }
                            else
                            {
                                MessageBox.Show("Tên Nhà Thầu không đúng!!");
                                return;
                            }
                            #endregion
                            string idnhom = q[i, 15].ToString();
                            #region ktra TenNhom
                            var tennhom = (from nhom in data.NhomDVs.Where(p => p.Status == 1) select new { nhom.TenNhom, nhom.IDNhom, nhom.Status }).ToList();
                            foreach (var a in tennhom)
                            {
                                if (a.TenNhom == idnhom)
                                {
                                    CheckNhom b = new CheckNhom();
                                    b.checkIDNhom = tennhom.Where(p => p.TenNhom.Contains(idnhom) && p.Status == 1).FirstOrDefault().IDNhom;
                                    _lscheckN.Add(b);
                                }
                            }
                            if (_lscheckN.Count() > 0)
                            {
                                dv.IDNhom = tennhom.Where(p => p.TenNhom.Contains(idnhom) && p.Status == 1).FirstOrDefault().IDNhom;
                            }
                            else
                            {
                                MessageBox.Show("Tên nhóm không đúng!!");
                                return;
                            }
                            #endregion
                            string donvi = q[i, 16].ToString();
                            if (donvi != "0")
                            {
                                dv.DonVi = donvi;
                            }
                            else
                            {
                                MessageBox.Show("Thuốc " + dv.TenDV + " chưa có đơn vị!!");
                                return;
                            }
                            dv.DonGia = Convert.ToDouble(q[i, 17].ToString());
                            dv.TyLeSD = Convert.ToInt32(q[i, 18].ToString());
                            dv.DonViN = q[i, 19].ToString();
                            dv.DonGia2 = Convert.ToDouble(q[i, 20].ToString());
                            dv.SLuong = Convert.ToDouble(q[i, 21].ToString());
                            dv.GiaBHGioiHanTT = Convert.ToDouble(q[i, 22].ToString());
                            dv.SoQD = q[i, 23].ToString();
                            dv.GiaNhap = Convert.ToDouble(q[i, 24].ToString());
                            string tdm = q[i, 25].ToString();
                            dv.TrongDM = TrongDm.Where(p => p.TenDM.Contains(tdm)).FirstOrDefault().id;
                            dv.LThuoc = Convert.ToInt32(q[i, 26].ToString());
                            dv.MaNhom = q[i, 27].ToString();
                            dv.SLMin = Convert.ToInt32(q[i, 28].ToString());
                            dv.LThau = Convert.ToInt32(q[i, 29].ToString());
                            dv.NhomThau = q[i, 30].ToString();
                            string ngayqd = q[i, 31].ToString();
                            #region Ngày QĐ
                            if (ngayqd != "0")
                            {
                                int b = 0; int c = 0; int d = 0;
                                b = Convert.ToInt32(q[i, 31].ToString().Substring(0, 2));
                                c = Convert.ToInt32(q[i, 31].ToString().Substring(3, 2));
                                d = Convert.ToInt32(q[i, 31].ToString().Substring(6, 4));
                                DateTime a = new DateTime(d, c, b);
                                dv.NgayQD = a;
                            }
                            else
                            {
                                MessageBox.Show("Thuốc " + dv.TenDV + " chưa có ngày QĐ!!");
                                return;
                            }
                            #endregion
                            dv.MaNhom5937 = Convert.ToInt32(q[i, 32].ToString());
                            dv.HamLuong = q[i, 33].ToString();
                            dv.DangBC = q[i, 34].ToString();
                            dv.TenRG = q[i, 35].ToString();
                            dv.DinhMuc = Convert.ToInt32(q[i, 36].ToString());
                            dv.NhaSX = q[i, 37].ToString();
                            #region PL thuốc 27022
                            if (DungChung.Bien.MaBV == "27022")
                            {
                                string PLT = q[i, 38].ToString();
                                int PhanLoaiiT = 0;
                                if (PLT == "Thuốc thường")
                                    PhanLoaiiT = 1;
                                else if (PLT == "Thuốc kê đơn")
                                    PhanLoaiiT = 2;
                                else if (PLT == "Thuốc không kê đơn")
                                    PhanLoaiiT = 3;
                                else if (PLT == "Thuốc kháng sinh")
                                    PhanLoaiiT = 4;
                                else if (PLT == "Thuốc kiểm soát đặc biệt")
                                    PhanLoaiiT = 5;
                                dv.PLThuoc = PhanLoaiiT;
                            }
                            #endregion
                            dv.TinhTNhap = q[i, 39].ToString();
                            dv.NguonGoc = q[i, 40].ToString();
                            dv.BPDung = q[i, 41].ToString();
                            dv.YCSD = q[i, 42].ToString();
                            dv.TyLeSP = Convert.ToDouble(q[i, 43].ToString());
                            dv.TyLeBQ = Convert.ToDouble(q[i, 44].ToString());
                            dmduoc.Add(dv);
                        }

                    }
                }
                #endregion
                #region thuốc tân dược
                else
                {
                    var q = DungChung.Ham.LoadXlsx(dl.FileName, 39, "Danh mục dược");
                    if (DungChung.Bien.MaBV == "27022")
                        q = DungChung.Ham.LoadXlsx(dl.FileName, 40, "Danh mục dược");
                    int f = q.GetLength(0);
                    for (int i = 0; i < f; i++)
                    {
                        if (!string.IsNullOrEmpty(q[i, 0].ToString()))
                        {
                            Dv dv = new Dv();
                            string tendv = q[i, 0].ToString();
                            if (tendv != "0")
                            {
                                dv.TenDV = tendv;
                            }
                            else
                            {
                                MessageBox.Show("Trong file có thuốc chưa nhập tên DV!!");
                                return;
                            }
                            string MaNoiBo = q[i, 1].ToString();
                            if (dmduoc.Count > 0)
                            {
                                foreach (var dm in dmduoc)
                                {
                                    if (dm.MaTam == MaNoiBo)
                                    {
                                        MessageBox.Show("Thuốc " + dm.TenDV + " trùng mã nội bộ!!");
                                        return;
                                    }
                                }
                            }
                            dv.MaTam = MaNoiBo;
                            dv.SoDK = q[i, 2].ToString();
                            dv.SoTT = Convert.ToInt32(q[i, 3].ToString());
                            dv.NuocSX = q[i, 4].ToString();
                            dv.MaQD = q[i, 5].ToString();
                            dv.TieuChuan = q[i, 6].ToString();
                            string Tentn = q[i, 7].ToString();
                            #region ktra TenTN
                            if(Tentn == "0")
                            {
                                MessageBox.Show("Thuốc " + dv.TenDV + " Chưa chọn tên tiểu nhóm!!");
                                return;
                            }    
                            foreach (var a in Nhomdvs)
                            {
                                if (a.TenTN == Tentn)
                                {
                                    CheckTN b = new CheckTN();
                                    b.checkTenTN = Nhomdvs.Where(p => p.TenTN.Contains(Tentn)).FirstOrDefault().IdTieuNhom;
                                    _lscheckTN.Add(b);
                                }
                            }
                            if (_lscheckTN.Count() > 0)
                            {
                                dv.IdTieuNhom = Nhomdvs.Where(p => p.TenTN.Contains(Tentn)).FirstOrDefault().IdTieuNhom;
                            }
                            else
                            {
                                MessageBox.Show("Tên TN không đúng!!");
                                return;
                            }
                            #endregion
                            dv.MaDuongDung = q[i, 8].ToString();
                            dv.DuongD = q[i, 9].ToString();
                            dv.SoTTqd = q[i, 10].ToString();
                            dv.QCPC = q[i, 11].ToString();
                            dv.BHTT = q[i, 12] != null ? int.Parse(q[i, 12].ToString()) : 0;
                            dv.TenHC = q[i, 13].ToString();
                            string Macc = q[i, 14].ToString();
                            #region Ktra tenThau
                            var nhacc = (from cc in data.NhaCCs select new { cc.MaCC, cc.TenCC }).ToList();
                            foreach (var a in nhacc)
                            {
                                if (a.TenCC == Macc)
                                {
                                    CheckCC b = new CheckCC();
                                    b.checkMaCC = nhacc.Where(p => p.TenCC.Contains(Macc)).FirstOrDefault().MaCC;
                                    _lscheckCC.Add(b);
                                }
                            }
                            if (_lscheckCC.Count() > 0)
                            {
                                dv.MaCC = nhacc.Where(p => p.TenCC.Contains(Macc)).FirstOrDefault().MaCC;
                            }
                            else
                            {
                                MessageBox.Show("Tên Nhà Thầu không đúng!!");
                                return;
                            }
                            #endregion
                            string idnhom = q[i, 15].ToString();
                            #region ktra TenNhom
                            var tennhom = (from nhom in data.NhomDVs.Where(p => p.Status == 1) select new { nhom.TenNhom, nhom.IDNhom, nhom.Status }).ToList();
                            foreach (var a in tennhom)
                            {
                                if (a.TenNhom == idnhom)
                                {
                                    CheckNhom b = new CheckNhom();
                                    b.checkIDNhom = tennhom.Where(p => p.TenNhom.Contains(idnhom) && p.Status == 1).FirstOrDefault().IDNhom;
                                    _lscheckN.Add(b);
                                }
                            }
                            if (_lscheckN.Count() > 0)
                            {
                                dv.IDNhom = tennhom.Where(p => p.TenNhom.Contains(idnhom) && p.Status == 1).FirstOrDefault().IDNhom;
                            }
                            else
                            {
                                MessageBox.Show("Tên nhóm không đúng!!");
                                return;
                            }
                            #endregion
                            string donvi = q[i, 16].ToString();
                            if (donvi != "0")
                            {
                                dv.DonVi = donvi;
                            }
                            else
                            {
                                MessageBox.Show("Thuốc " + dv.TenDV + " chưa có đơn vị!!");
                                return;
                            }
                            dv.DonGia = Convert.ToDouble(q[i, 17].ToString());
                            dv.TyLeSD = Convert.ToInt32(q[i, 18].ToString());
                            dv.DonViN = q[i, 19].ToString();
                            dv.DonGia2 = Convert.ToDouble(q[i, 20].ToString());
                            dv.SLuong = Convert.ToDouble(q[i, 21].ToString());
                            dv.GiaBHGioiHanTT = Convert.ToDouble(q[i, 22].ToString());
                            dv.SoQD = q[i, 23].ToString();
                            dv.GiaNhap = Convert.ToDouble(q[i, 24].ToString());
                            string tdm = q[i, 25].ToString();
                            dv.TrongDM = TrongDm.Where(p => p.TenDM.Contains(tdm)).FirstOrDefault().id;
                            dv.LThuoc = Convert.ToInt32(q[i, 26].ToString());
                            dv.MaNhom = q[i, 27].ToString();
                            dv.SLMin = Convert.ToInt32(q[i, 28].ToString());
                            dv.LThau = Convert.ToInt32(q[i, 29].ToString());
                            dv.NhomThau = q[i, 30].ToString();
                            string ngayqd = q[i, 31].ToString();
                            #region Ngày QĐ
                            if (ngayqd != "0")
                            {
                                int b = 0; int c = 0; int d = 0;
                                b = Convert.ToInt32(q[i, 31].ToString().Substring(0, 2));
                                c = Convert.ToInt32(q[i, 31].ToString().Substring(3, 2));
                                d = Convert.ToInt32(q[i, 31].ToString().Substring(6, 4));
                                DateTime a = new DateTime(d, c, b);
                                dv.NgayQD = a;
                            }
                            else
                            {
                                MessageBox.Show("Thuốc " + dv.TenDV + " chưa có ngày QĐ!!");
                                return;
                            }
                            #endregion
                            dv.MaNhom5937 = Convert.ToInt32(q[i, 32].ToString());
                            dv.HamLuong = q[i, 33].ToString();
                            dv.DangBC = q[i, 34].ToString();
                            dv.TenRG = q[i, 35].ToString();
                            dv.DinhMuc = Convert.ToInt32(q[i, 36].ToString());
                            dv.NhaSX = q[i, 37].ToString();
                            #region PL thuốc 27022
                            if (DungChung.Bien.MaBV == "27022")
                            {
                                string PLT = q[i, 38].ToString();
                                int PhanLoaiiT = 0;
                                if (PLT == "Thuốc thường")
                                    PhanLoaiiT = 1;
                                else if (PLT == "Thuốc kê đơn")
                                    PhanLoaiiT = 2;
                                else if (PLT == "Thuốc không kê đơn")
                                    PhanLoaiiT = 3;
                                else if (PLT == "Thuốc kháng sinh")
                                    PhanLoaiiT = 4;
                                else if (PLT == "Thuốc kiểm soát đặc biệt")
                                    PhanLoaiiT = 5;
                                dv.PLThuoc = PhanLoaiiT;
                            }
                            #endregion
                            dmduoc.Add(dv);
                        }

                    }
                }
                #endregion
            }
            if (dmduoc.Count() > 0)
            {
                grcDichVu.DataSource = dmduoc.ToList();
            }

        }

        private void btnXoaDuLieuTam_Click(object sender, EventArgs e)
        {
            grcDichVu.DataSource = null;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
          var  _lKPsd = (from kp in _data.KPhongs.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.CanLamSang || p.PLoai == DungChung.Bien.st_PhanLoaiKP.LamSang || p.PLoai == DungChung.Bien.st_PhanLoaiKP.PhongKham || p.PLoai == DungChung.Bien.st_PhanLoaiKP.KhoaDuoc || p.PLoai == DungChung.Bien.st_PhanLoaiKP.TuTruc)
                      select new{MaKP = kp.MaKP}).Distinct().ToList();
            string makpsd = "";
            if(_lKPsd.Count > 0)
            {
                foreach (var a in _lKPsd)
                {
                    makpsd += Convert.ToString(a.MaKP) + ";";
                }
            }
            makpsd.Remove(makpsd.Length - 1);
            if (_NhapDanhMuc == 1)
            {
                if (dmduoc.Count > 0)
                {
                    foreach (var item in dmduoc)
                    {
                        DichVu dv = new DichVu();
                        dv.TenDV = item.TenDV;
                        var matam = (from mt in data.DichVus.Where(p => p.MaTam != null) select new { mt.MaTam }).ToList();
                        foreach (var a in matam)
                        {
                            if (a.MaTam == item.MaTam)
                            {
                                MessageBox.Show("Trùng mã nội bộ: " + a.MaTam);
                                return;
                            }
                        }
                        dv.MaTam = item.MaTam;
                        dv.SoDK = item.SoDK;
                        dv.NuocSX = item.NuocSX;
                        dv.MaQD = item.MaQD;
                        dv.TieuChuan = item.TieuChuan;
                        dv.IdTieuNhom = item.IdTieuNhom;
                        dv.MaDuongDung = item.MaDuongDung;
                        dv.DuongD = item.DuongD;
                        dv.NhaSX = item.NhaSX;
                        dv.SoTTqd = item.SoTTqd;
                        dv.NguonGoc = item.NguonGoc;
                        dv.QCPC = item.QCPC;
                        dv.BHTT = item.BHTT;
                        dv.MaCC = item.MaCC;
                        dv.TenHC = item.TenHC;
                        dv.IDNhom = item.IDNhom;
                        dv.DonVi = item.DonVi;
                        dv.DonGia = item.DonGia;
                        dv.DonGia2 = item.DonGia2;
                        dv.TrongDM = item.TrongDM;
                        dv.DSDonGia = "0";
                        dv.LThuoc = item.LThuoc;
                        dv.LThau = item.LThau;
                        dv.NhomThau = item.NhomThau;
                        dv.MaNhom = item.MaNhom;
                        dv.SLuong = item.SLuong;
                        dv.MaNhom5937 = item.MaNhom5937;
                        dv.NgayQD = item.NgayQD;
                        dv.SoQD = item.SoQD;
                        dv.PLoai = 1;
                        dv.Status = 1;
                        dv.HamLuong = item.HamLuong;
                        dv.DangBC = item.DangBC;
                        dv.TenRG = item.TenRG;
                        dv.DinhMuc = item.DinhMuc;
                        dv.TinhTNhap = item.TinhTNhap;
                        dv.NguonGoc = item.NguonGoc;
                        dv.BPDung = item.BPDung;
                        dv.YCSD = item.YCSD;
                        dv.TyLeSP = item.TyLeSP;
                        dv.TyLeBQ = item.TyLeBQ;
                        dv.DonViN = item.DonViN;
                        dv.SoTT = item.SoTT;
                        dv.SLMin = item.SLMin;
                        dv.GiaBHGioiHanTT = item.GiaBHGioiHanTT;
                        dv.GiaNhap = item.GiaNhap;
                        dv.ISTrongThau = true;
                        dv.MaKPsd = makpsd;
                        dv.TyLeSD = item.TyLeSD;
                        if (DungChung.Bien.MaBV == "27022")
                        {
                            dv.PLThuoc = item.PLThuoc;
                        }
                        if (chk_DongY.Checked)
                        {
                            dv.DongY = 1;
                        }
                        dv.isGayTe = chk_GayTe.Checked ? true : false;
                        data.DichVus.Add(dv);

                    }
                    if (data.SaveChanges() >= 0)
                    {
                        XtraMessageBox.Show("Thêm mới thành công dịch vụ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }



            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void chk_DongY_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_DongY.Checked)
            {
                if(DungChung.Bien.MaBV == "27022")
                {
                    colTTNhap.VisibleIndex = 39;
                    colNguonGoc.VisibleIndex = 40;
                    colBPDung.VisibleIndex = 41;
                    colYeuCauSP.VisibleIndex = 42;
                    colTyLeSP.VisibleIndex = 43;
                    colTyLeBQ.VisibleIndex = 44;
                }   
                else
                {

                    colTTNhap.VisibleIndex = 38;
                    colNguonGoc.VisibleIndex = 39;
                    colBPDung.VisibleIndex = 40;
                    colYeuCauSP.VisibleIndex = 41;
                    colTyLeSP.VisibleIndex = 42;
                    colTyLeBQ.VisibleIndex = 43;
                }    
                chk_GayTe.Checked = false;
            }
            else
            {
                colTTNhap.Visible = false;
                colNguonGoc.Visible = false;
                colBPDung.Visible = false;
                colYeuCauSP.Visible = false;
                colTyLeSP.Visible = false;
                colTyLeBQ.Visible = false;
            }
        }

        private void chk_GayTe_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_GayTe.Checked)
            {
                chk_DongY.Checked = false;
            }
        }
    }
}