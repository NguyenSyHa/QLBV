using DevExpress.ClipboardSource.SpreadsheetML;
using DevExpress.XtraEditors;
using QLBV.FormNhap;
using QLBV.FormThamSo;
using QLBV_Database;
using QLBV_Database.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static QLBV.DungChung.cls_KetNoiXP_SA;
using static QLBV.FormNhap.frm_CapNhatNgayGiuong;
using static QLBV.FormNhap.usThongKeToanVien;
using static QLBV.FormThamSo.frm_CTTonThucSDNoiTru;

namespace QLBV.ChucNang.FormDanhMuc
{
    public partial class Frm_Import_DsBNhan_excel : DevExpress.XtraEditors.XtraForm
    {
        private QLBV_Database.QLBVEntities _dataContext = EntityDbContext.DbContext;
        public Frm_Import_DsBNhan_excel(int sodk)
        {
            InitializeComponent();
            this.soDK = sodk;
        }

        private void btnXuatFileMau_Click(object sender, EventArgs e)
        {
            SaveFileDialog sv = new SaveFileDialog();
            sv.Filter = "xlsx files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
            if (sv.ShowDialog() == DialogResult.OK)
            {
                grcDsBNhan.ExportToXlsx(sv.FileName);
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
        private List<LoaiCapCuu> loaiCapCuus = new List<LoaiCapCuu>();
        private List<LoaiNoiTru> loaiNoiTrus = new List<LoaiNoiTru>();
        private List<LoaiDoiTuong> loaiDoiTuongs = new List<LoaiDoiTuong>();
        private List<LoaiTuyen> loaiTuyens = new List<LoaiTuyen>();
        private List<LoaiNoiTinh> loaiNoiTinhs = new List<LoaiNoiTinh>();
        private List<LoaiKhuVuc> loaiKhuVucs = new List<LoaiKhuVuc>();
        private List<BenhVien> _lBenhVien = new List<BenhVien>();
        private void Frm_Import_DsBNhan_excel_Load(object sender, EventArgs e)
        {
            //if (chk_NotBHYT.Checked)
            //{
            //    colSThe.Visible = false;
            //    colHanBHTu.Visible = false;
            //    colHanBHDen.Visible = false;
            //    colMaCS.Visible = false;
            //    colNoiTinh.Visible = false;
            //    colMaBV.Visible = false;
            //    colMucHuong.Visible = false;
            //    colKhuVuc.Visible = false;
            //    colNgayHM.Visible = false;
            //    colLuongCS.Visible = false;
            //    colTuyen.Visible = false;
            //    colTuyenDuoi.Visible = false;
            //}

            
            loaiCapCuus.Add(new LoaiCapCuu { Id = 0, Name = "Thường" });
            loaiCapCuus.Add(new LoaiCapCuu { Id = 1, Name = "Cấp cứu" });
            loaiCapCuus.Add(new LoaiCapCuu { Id = 2, Name = "Tử vong" });
            lupCapCuu.DataSource = loaiCapCuus;

            
            loaiNoiTrus.Add(new LoaiNoiTru { Id = 0, Name = "Ngoại trú" });
            loaiNoiTrus.Add(new LoaiNoiTru { Id = 1, Name = "Nội trú" });
            lupNoiTru.DataSource = loaiNoiTrus;

            
            loaiDoiTuongs.Add(new LoaiDoiTuong { Id = 1, Name = "BHYT" });
            loaiDoiTuongs.Add(new LoaiDoiTuong { Id = 2, Name = "Dịch vụ" });
            loaiDoiTuongs.Add(new LoaiDoiTuong { Id = 3, Name = "KSK" });
            loaiDoiTuongs.Add(new LoaiDoiTuong { Id = 4, Name = "Khám tuyến" });
            lupDTuong.DataSource = loaiDoiTuongs.Distinct();

            
            loaiTuyens.Add(new LoaiTuyen { Id = 0, Name = "Đúng tuyến" });
            loaiTuyens.Add(new LoaiTuyen { Id = 1, Name = "Trái tuyến" });
            loaiTuyens.Add(new LoaiTuyen { Id = 2, Name = "Thông tuyến" });
            lupTuyen.DataSource = loaiTuyens;

            
            loaiNoiTinhs.Add(new LoaiNoiTinh { Id = 1, Name = "A. BN nội tỉnh KCB ban đầu" });
            loaiNoiTinhs.Add(new LoaiNoiTinh { Id = 2, Name = "B. BN nội tỉnh đến" });
            loaiNoiTinhs.Add(new LoaiNoiTinh { Id = 3, Name = "C. BN ngoại tỉnh đến" });
            lupNoiTinh.DataSource = loaiNoiTinhs;

            
            loaiKhuVucs.Add(new LoaiKhuVuc { Id = 0, Name = "" });
            loaiKhuVucs.Add(new LoaiKhuVuc { Id = 1, Name = "K1" });
            loaiKhuVucs.Add(new LoaiKhuVuc { Id = 2, Name = "K2" });
            loaiKhuVucs.Add(new LoaiKhuVuc { Id = 3, Name = "K3" });
            lupKhuVuc.DataSource = loaiKhuVucs;

            var _lkp = _dataContext.KPhongs.Where(p => p.Status == 1).ToList();
            var q = (from KhoaKham in _lkp
                     where (KhoaKham.PLoai == ("Lâm sàng") || KhoaKham.PLoai == ("Phòng khám") || ((DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "30303") ? KhoaKham.PLoai == DungChung.Bien.st_PhanLoaiKP.HanhChinh : false))
                     select KhoaKham).Select(x => new { x.PLoai, x.MaKP, x.TenKP }).OrderByDescending(p => p.PLoai).ThenBy(p => p.TenKP).ToList();
            if (q.Count > 0)
            {
                lupKP.DataSource = q.ToList();
            }

            var dtoc = _dataContext.DanTocs.Select(x => new { x.MaDT, x.TenDT }).OrderBy(p => p.TenDT).ToList();

            lupDanToc.DataSource = dtoc.ToList();

            var _lbv = (from ten in _lBenhVien.Where(p => p.status == 2 || p.MaChuQuan == DungChung.Bien.MaBV)
                    select ten).Select(x => new { x.MaBV, x.TenBV }).OrderBy(p => p.TenBV).ToList();
            if (DungChung.Bien.MaBV == "24012")
            {
                _lbv = (from ten in _lBenhVien.Where(p => p.status == 2 || p.status == 1 || p.MaChuQuan == DungChung.Bien.MaBV)
                        select ten).Select(x => new { x.MaBV, x.TenBV }).OrderBy(p => p.TenBV).ToList();
            }

            var lICD = (from ICD in _dataContext.ICD10 select new c_ICD { MaICD = ICD.MaICD ?? "", TenICD = ICD.TenICD ?? "" }).OrderBy(p => p.TenICD).ToList();
            lICD.Insert(0, new c_ICD { MaICD = "0", TenICD = "" });

            lupNoiGT.DataSource = lICD.ToList();

            lupCanBo.DataSource = _dataContext.CanBoes.Select(x => new { x.MaCB, x.TenCB }).ToList();
            var NN = _dataContext.DmNNs.Select(x => new { x.MaNN, x.TenNN }).OrderBy(p => p.TenNN).ToList();
            lupNgheNghiep.DataSource = NN;

            var chuyenkhoa = DungChung.Bien._listTaiNan.Where(p => p.Status);
            lupChuyenKhoa.DataSource = chuyenkhoa;

            var tinh = (from tin in _dataContext.DmTinhs select new { tin.TenTinh, tin.MaTinh }).OrderBy(p => p.TenTinh).ToList();
            lupMaTinh.DataSource = tinh.ToList();
            lupMaTinhKhaiSinh.DataSource = tinh.ToList();
            var huyen = (from h in _dataContext.DmHuyens.Where(p => p.MaTinh == (_matinh)) select new { h.TenHuyen, h.MaHuyen }).OrderBy(p => p.TenHuyen).ToList();
            lupMaHuyen.DataSource = huyen.ToList();
            var xa = (from x in _dataContext.DmXas.Where(p => p.MaHuyen == (_mahuyen)) select new { x.TenXa, x.MaXa }).OrderBy(p => p.TenXa).ToList();
            lupMaXa.DataSource = xa.ToList();
            if (DungChung.Bien.MaBV == "24009")
            {
                lupMaHuyenKhaiSinh.DataSource = huyen.ToList();
                lupMaXaKhaiSinh.DataSource = xa.ToList();
            }
        }
        private string _matinh = "", _mahuyen = "", _maxa = "";
        private void chk_NotBHYT_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_NotBHYT.Checked)
            {
                colSThe.Visible = false;
                colHanBHTu.Visible = false;
                colHanBHDen.Visible = false;
                colMaCS.Visible = false;
                colNoiTinh.Visible = false;
                colMaBV.Visible = false;
                colMucHuong.Visible = false;
                colKhuVuc.Visible = false;
                colNgayHM.Visible = false;
                colLuongCS.Visible = false;
                colTuyen.Visible = false;
                colTuyenDuoi.Visible = false;
            }
            else
            {
                colSThe.Visible = true;
                colHanBHTu.Visible = true;
                colHanBHDen.Visible = true;
                colMaCS.Visible = true;
                colNoiTinh.Visible = true;
                colMaBV.Visible = true;
                colMucHuong.Visible = true;
                colKhuVuc.Visible = true;
                colNgayHM.Visible = true;
                colLuongCS.Visible = true;
                colTuyen.Visible = true;
                colTuyenDuoi.Visible = true;
            }
        }

        private void btnXoaDuLieuTam_Click(object sender, EventArgs e)
        {
            grcDsBNhan.DataSource = null;
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        class LoaiCapCuu
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        class LoaiNoiTru
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        class LoaiDoiTuong
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        class LoaiTuyen
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
        class LoaiNoiTinh
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        private void lupMaTinh_EditValueChanged(object sender, EventArgs e)
        {
            var matinh = grvDsBNhan.GetFocusedRowCellValue(colMaTinh);
            if (matinh != null)
                _matinh = matinh.ToString();
            else
                _matinh = "";
            var huyen = (from h in _dataContext.DmHuyens.Where(p => p.MaTinh == (_matinh)) select new { h.TenHuyen, h.MaHuyen }).OrderBy(p => p.TenHuyen).ToList();
            lupMaHuyen.DataSource = huyen.ToList();

            var dtuong = grvDsBNhan.GetFocusedRowCellValue(colDTuong);
            var dchi = grvDsBNhan.GetFocusedRowCellValue(colDiaChi).ToString();
            if (dtuong.ToString() == "Dịch vụ" && (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "56789") || DungChung.Bien.MaBV == "30303")
            {
                TenTinh = _dataContext.DmTinhs.Where(p => p.MaTinh == _matinh).Select(p => p.TenTinh).FirstOrDefault();
                if (TenTinh != null)
                    grvDsBNhan.SetFocusedRowCellValue(colDiaChi, TenTinh);
            }
            //Frm_Import_DsBNhan_excel_Load(sender, e);
        }
        string TenTinh = "";
        private void lupMaTinhKhaiSinh_EditValueChanged(object sender, EventArgs e)
        {
            var matinh = grvDsBNhan.GetFocusedRowCellValue(colMaTinhKhaiSinh);
            if (matinh != null)
                _matinh = matinh.ToString();
            else
                _matinh = "";
            var huyen = (from h in _dataContext.DmHuyens.Where(p => p.MaTinh == (_matinh)) select new { h.TenHuyen, h.MaHuyen }).OrderBy(p => p.TenHuyen).ToList();
            lupMaHuyenKhaiSinh.DataSource = huyen.ToList();

            //var dtuong = grvDsBNhan.GetFocusedRowCellValue(colDTuong);
            //var dchi = grvDsBNhan.GetFocusedRowCellValue(colDiaChi).ToString();
            //if (dtuong.ToString() == "Dịch vụ" && (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "56789") || DungChung.Bien.MaBV == "30303")
            //{
            //    var TenTinh = _dataContext.DmTinhs.Where(p => p.MaTinh == _matinh).Select(p => p.TenTinh).FirstOrDefault();
            //    if (TenTinh != null)
            //        grvDsBNhan.SetFocusedRowCellValue(colDiaChi, TenTinh);
            //}
            //Frm_Import_DsBNhan_excel_Load(sender, e);
        }

        private void lupMaHuyen_EditValueChanged(object sender, EventArgs e)
        {
            var maHuyen = grvDsBNhan.GetFocusedRowCellValue(colMaHuyen);
            if (lupMaHuyen != null)
                _mahuyen = maHuyen.ToString();
            else
                _mahuyen = "";
            string DiaChi = "";
            if (DungChung.Bien.MaBV == "30372")
                DiaChi = grvDsBNhan.GetFocusedRowCellValue(colDiaChi).ToString();
            else
                DiaChi = grvDsBNhan.GetFocusedRowCellValue(colDiaChi).ToString();
            var xa = (from x in _dataContext.DmXas.Where(p => p.MaHuyen == (_mahuyen)) select new { x.TenXa, x.MaXa }).OrderBy(p => p.TenXa).ToList();
            lupMaXa.DataSource = xa.ToList();
            var dtuong = grvDsBNhan.GetFocusedRowCellValue(colDTuong).ToString();
            if (dtuong == "Dịch vụ" && (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "56789" || DungChung.Bien.MaBV == "30303"))
            {
                string[] _lTentinh = DiaChi.Split('-');
                var TenHuyen = _dataContext.DmHuyens.Where(p => p.MaHuyen == _mahuyen).Select(p => p.TenHuyen).FirstOrDefault();
                if (TenHuyen != null)
                {
                    if (_lTentinh.Length > 1)
                    {
                        grvDsBNhan.SetFocusedRowCellValue(colMaHuyen, TenHuyen.ToString() + "-" + _lTentinh[1].ToString());
                    }
                    else
                    {
                        grvDsBNhan.SetFocusedRowCellValue(colMaHuyen, TenHuyen.ToString() + "-" + TenTinh);
                    }
                }
            }
            //Frm_Import_DsBNhan_excel_Load(sender, e);
        }

        private void lupMaHuyenKhaiSinh_EditValueChanged(object sender, EventArgs e)
        {
            var maHuyen = grvDsBNhan.GetFocusedRowCellValue(colMaHuyenKhaiSinh);
            if (lupMaHuyen != null)
                _mahuyen = maHuyen.ToString();
            else
                _mahuyen = "";
            string DiaChi = "";
            if (DungChung.Bien.MaBV == "30372")
                DiaChi = grvDsBNhan.GetFocusedRowCellValue(colDiaChi).ToString();
            else
                DiaChi = grvDsBNhan.GetFocusedRowCellValue(colDiaChi).ToString();
            var xa = (from x in _dataContext.DmXas.Where(p => p.MaHuyen == (_mahuyen)) select new { x.TenXa, x.MaXa }).OrderBy(p => p.TenXa).ToList();
            lupMaXaKhaiSinh.DataSource = xa.ToList();
            var dtuong = grvDsBNhan.GetFocusedRowCellValue(colDTuong).ToString();
            if (dtuong == "Dịch vụ" && (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "56789" || DungChung.Bien.MaBV == "30303"))
            {
                string[] _lTentinh = DiaChi.Split('-');
                var TenHuyen = _dataContext.DmHuyens.Where(p => p.MaHuyen == _mahuyen).Select(p => p.TenHuyen).FirstOrDefault();
                if (TenHuyen != null)
                {
                    if (_lTentinh.Length > 1)
                    {
                        grvDsBNhan.SetFocusedRowCellValue(colMaHuyen, TenHuyen.ToString() + "-" + _lTentinh[1].ToString());
                    }
                    else
                    {
                        grvDsBNhan.SetFocusedRowCellValue(colMaHuyen, TenHuyen.ToString() + "-" + TenTinh);
                    }
                }
            }
            //Frm_Import_DsBNhan_excel_Load(sender, e);
        }

        private void lupMaXa_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void lupMaXaKhaiSinh_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void grcDsBNhan_Click(object sender, EventArgs e)
        {
        }
        List<CheckCC> _lscheckCC = new List<CheckCC>();
        List<CheckNhom> _lscheckN = new List<CheckNhom>();
        List<CheckTN> _lscheckTN = new List<CheckTN>();
        private void btnChonFile_Click(object sender, EventArgs e)
        {
            dmBNhan.Clear();
            //var Nhomdvs = (from tn in _dataContext.TieuNhomDVs.Where(p => p.Status == 1 && p.IDNhom == 4) select new { tn.IdTieuNhom, tn.TenTN }).ToList();
            OpenFileDialog dl = new OpenFileDialog();
            dl.Filter = "xlsx files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
            if (dl.ShowDialog() == DialogResult.OK)
            {
                var q = DungChung.Ham.LoadXlsx(dl.FileName, 68, "Danh mục bệnh nhân");
                int f = q.GetLength(0);
                for (int i = 0; i < f; i++)
                {
                    if (!string.IsNullOrEmpty(q[i, 0].ToString()))
                    {
                        Bn bn = new Bn();
                        string tenBnhan = q[i, 0].ToString();
                        if (tenBnhan != "0")
                        {
                            bn.TenBNhan = tenBnhan;
                        }
                        else
                        {
                            MessageBox.Show("Chưa nhập tên bệnh nhân");
                            return;
                        }

                        bn.GTinh = q[i, 1].ToString();
                        bn.NgaySinh = q[i, 2].ToString().Length == 1 ? 0 + q[i, 2].ToString() : q[i, 2].ToString();
                        bn.ThangSinh = q[i, 3].ToString().Length == 1 ? 0 + q[i, 3].ToString() : q[i, 3].ToString();
                        bn.NamSinh = q[i, 4].ToString();
                        if (q[i, 4].ToString() == null)
                        {
                            MessageBox.Show("Chưa điền năm sinh của bệnh nhân.");
                            return;
                        }
                        bn.DiaChi = q[i, 5].ToString();
                        string dt = q[i, 6].ToString();
                        var doituong = loaiDoiTuongs.Where(x => x.Name.Equals(dt)).Select(x => x.Id).FirstOrDefault();
                        bn.DTuong = doituong.ToString();
                        bn.SThe = q[i, 7].ToString();
                        bn.MaKCB = q[i, 8].ToString();
                        bn.MaCS = q[i, 9].ToString();
                        string tuy = q[i, 6].ToString();
                        var tuyen = loaiTuyens.Where(x => x.Name == tuy).Select(x => x.Id).FirstOrDefault();
                        bn.Tuyen = tuyen.ToString();
                        bn.MucHuong = q[i, 11].ToString();
                        bn.HanBHTu = q[i, 12].ToString();
                        bn.HanBHDen = q[i, 13].ToString();
                        bn.Ma_lk = q[i, 14].ToString();
                        string ntru = q[i, 15].ToString();
                        var noitru = loaiNoiTrus.Where(x => x.Name.Equals(ntru)).Select(x => x.Id).FirstOrDefault();
                        bn.NoiTru = noitru.ToString();
                        bn.NNhap = q[i, 16].ToString();
                        var cb = q[i, 17].ToString();
                        var mabnhan = _dataContext.CanBoes.Where(x => x.TenCB.Equals(cb)).Select(x => x.MaCB).FirstOrDefault();
                        bn.MaCB = mabnhan;
                        string loaint = q[i, 18].ToString();
                        var noitinh = loaiNoiTinhs.Where(x => x.Name.Equals(loaint)).Select(x => x.Id).FirstOrDefault();
                        bn.NoiTinh = noitinh.ToString();
                        var kp = q[i, 19].ToString();
                        var kphong = _dataContext.KPhongs.Where(x => x.TenKP.Equals(kp)).Select(x => x.MaKP).FirstOrDefault();
                        bn.MaKP = kphong.ToString();
                        bn.Tuoi = q[i, 20].ToString();// != null ? Convert.ToInt32(q[i, 20].ToString()) : DateTime.Now.Year - Convert.ToInt32(q[i, 4]);
                        bn.TChung = q[i, 21].ToString();
                        bn.PID = q[i, 22].ToString();
                        var bv = q[i, 23].ToString();
                        var benhvien = _dataContext.BenhViens.Where(x => x.TenBV.Equals(bv)).Select(x => x.MaBV).FirstOrDefault();
                        bn.MaBV = benhvien;
                        string ctnoigt = q[i, 24].ToString();
                        var noigioithieu = _dataContext.ICD10.Where(x=>x.TenICD.Equals(ctnoigt)).Select(x => x.MaICD).FirstOrDefault();
                        bn.CDNoiGT = noigioithieu;
                        bn.NgoaiGio = q[i, 25].ToString();
                        string cc = q[i, 26].ToString();
                        var capcuu = loaiCapCuus.Where(x => x.Name.Equals(cc)).Select(x => x.Id).FirstOrDefault();
                        bn.CapCuu = capcuu.ToString();
                        bn.SoTT = q[i, 27].ToString();
                        string kv = q[i, 28].ToString();
                        var khuvuc = loaiKhuVucs.Where(x => x.Name.Equals(kv)).Select(x => x.Id).FirstOrDefault();
                        bn.KhuVuc = khuvuc.ToString();
                        var ck = q[i, 29].ToString();
                        var chuyenkhoa = DungChung.Bien._listTaiNan.Where(x => x.Tenloai == ck).Select(x => x.Tenloai).FirstOrDefault();
                        bn.ChuyenKhoa = chuyenkhoa;
                        bn.NgayHM = q[i, 30].ToString();
                        bn.LuongCS = q[i, 31].ToString();
                        bn.Export = q[i, 32].ToString();
                        bn.TuyenDuoi = q[i, 33].ToString();
                        bn.UuTien = q[i, 34].ToString();
                        bn.DTNT = q[i, 35].ToString();
                        bn.SoDK = q[i, 36].ToString();
                        bn.IDPerson = q[i, 37].ToString();
                        bn.NoThe = q[i, 38].ToString();
                        bn.Normal = q[i, 39].ToString();
                        bn.PLKham = q[i, 40].ToString();
                        bn.SoHSBA = q[i, 41].ToString();
                        bn.PersonCode = q[i, 42].ToString();
                        bn.BNhanLao = q[i, 43].ToString();
                        bn.IsCCCD = q[i, 44].ToString();
                        bn.MaBNhan = q[i, 45].ToString();
                        string nn = q[i, 46].ToString();
                        var nghe = _dataContext.DmNNs.Where(x => x.TenNN.Equals(nn)).Select(x => x.MaNN).FirstOrDefault();
                        bn.MaNN = nghe;
                        bn.NoiLV = q[i, 47].ToString();
                        bn.DiaChiNoiLV = q[i, 48].ToString();
                        bn.DThoai = q[i, 49].ToString();
                        bn.NThan = q[i, 50].ToString();
                        bn.DCNguoiThan = q[i, 51].ToString();
                        bn.DThoaiNT = q[i, 52].ToString();
                        string dantc = q[i, 53].ToString();
                        var dantoc = _dataContext.DanTocs.Where(x => x.TenDT.Equals(dantc)).Select(x => x.MaDT).FirstOrDefault();
                        bn.MaDT = dantoc;
                        bn.SoKSinh = q[i, 54].ToString();
                        bn.NgoaiKieu = q[i, 55].ToString();
                        var tinh = q[i, 56].ToString();
                        var matinh = _dataContext.DmTinhs.Where(x => x.TenTinh.Equals(tinh)).Select(x => x.MaTinh).FirstOrDefault();
                        bn.MaTinh = matinh;
                        //lupMaTinh.DataSource = matinh;
                        var huyen = q[i, 57].ToString();
                        var mahuyen = _dataContext.DmHuyens.Where(x => x.TenHuyen.Equals(huyen)).Select(x => x.MaHuyen).FirstOrDefault();
                        bn.MaHuyen = mahuyen;
                        //lupMaHuyen.DataSource = mahuyen;
                        var xa = q[i, 58].ToString();
                        var maxa = _dataContext.DmXas.Where(x => x.TenXa.Equals(xa)).Select(x => x.MaXa).FirstOrDefault();
                        bn.MaXa = maxa;
                        //lupMaXa.DataSource = maxa;
                        var tinhks = q[i, 59].ToString();
                        var matinhks = _dataContext.DmTinhs.Where(x => x.TenTinh.Equals(tinhks)).Select(x => x.MaTinh).FirstOrDefault();
                        bn.MaTinhKhaiSinh = matinhks;
                        //lupMaTinhKhaiSinh.DataSource = matinhks;
                        bn.DiaChiKhaiSinh = q[i, 60].ToString();
                        var huyenks = q[i, 61].ToString();
                        var mahuyenks = _dataContext.DmHuyens.Where(x => x.TenHuyen.Equals(huyenks)).Select(x => x.MaHuyen).FirstOrDefault();
                        bn.MaHuyenKhaiSinh = mahuyenks;
                        //lupMaHuyenKhaiSinh.DataSource = mahuyenks;
                        var xaks = q[i, 62].ToString();
                        var maxaks = _dataContext.DmXas.Where(x => x.TenXa.Equals(xaks)).Select(x => x.MaXa).FirstOrDefault();
                        bn.MaXaKhaiSinh = maxaks;
                        //lupMaXaKhaiSinh.DataSource = maxaks;
                        bn.CMT = q[i, 63].ToString();
                        bn.NgayCapCMT = q[i, 64].ToString();
                        bn.NoiCapCMT = q[i, 65].ToString();
                        bn.HTThanhToan = q[i, 66].ToString();
                        dmBNhan.Add(bn);
                    }
                }
            }
            if (dmBNhan.Count() > 0)
            {
                grcDsBNhan.DataSource = dmBNhan.ToList();
            }
        }
        List<Bn> dmBNhan = new List<Bn>();

        class Bn
        {
            public string TenBNhan { get; set; }
            public string GTinh { get; set; }
            public string NgaySinh { get; set; }
            public string ThangSinh { get; set; }
            public string NamSinh { get; set; }
            public string DiaChi { get; set; }
            public string NoiTru { get; set; }
            public string DTuong { get; set; }
            public string SThe { get; set; }
            public string MaCS { get; set; }
            public string Tuyen { get; set; }
            public string HanBHTu { get; set; } 
            public string HanBHDen { get; set; }
            public string NoiTinh { get; set; }
            public string MucHuong { get; set; }
            public string MaKP { get; set; }
            public string NNhap { get; set; }
            public string Tuoi { get; set; }
            public string TChung { get; set; }
            public string CDNoiGT { get; set; }
            public string CapCuu { get; set; }
            public string SoTT { get; set; }
            public string MaBV { get; set; }
            public string ChuyenKhoa { get; set; }
            public string NgoaiGio { get; set; }
            public string TuyenDuoi { get; set; }
            public string KhuVuc { get; set; }
            public string NgayHM { get; set; }
            public string LuongCS { get; set; }
            public string UuTien { get; set; }
            public string DTNT { get; set; } = "false";
            public string IDPerson { get; set; }
            public string NoThe { get; set; }
            public string Ma_lk { get; set; }
            public string MaKCB { get; set; }
            public string SoDK { get; set; }
            public string Export { get; set; }
            public string Normal { get; set; }
            public string MaCB { get; set; }
            public string PID { get; set; }
            public string PLKham { get; set; }
            public string MaKPDTKH { get; set; }
            public string SoHSBA { get; set; }
            public string PersonCode { get; set; }
            public string BNhanLao { get; set; }
            public string IsCCCD { get; set; }
            public string MaBNhan { get; set; }
            public string MaNN { get; set; }
            public string NThan { get; set; }
            public string NoiLV { get; set; }
            public string DCNguoiThan { get; set; }
            public string DiaChiNoiLV { get; set; }
            public string SoKSinh { get; set; }
            public string MaTinh { get; set; }
            public string MaHuyen { get; set; }
            public string MaXa { get; set; }
            public string MaTinhKhaiSinh { get; set; }
            public string MaHuyenKhaiSinh { get; set; }
            public string MaXaKhaiSinh { get; set; }
            public string DiaChiKhaiSinh { get; set; }
            public string MaDT { get; set; }
            public string NgoaiKieu { get; set; }
            public string DThoai { get; set; }
            public string DThoaiNT { get; set; }
            public string CMT { get; set; }
            public string NgayCapCMT { get; set; }
            public string NoiCapCMT { get; set; }
            public string HTThanhToan { get; set; }
        }

        class c_ICD
        {
            private string maICD;

            public string MaICD
            {
                get { return maICD; }
                set { maICD = value; }
            }

            private string tenICD;

            public string TenICD
            {
                get { return tenICD; }
                set { tenICD = value; }
            }
        }
        class CheckTN
        {
            public int checkTenTN { get; set; }
        }
        class CheckCC
        {
            public String checkMaCC { get; set; }
        }

        private void lupMaTinh_Leave(object sender, EventArgs e)
        {
            var matinh = grvDsBNhan.GetFocusedRowCellValue(colMaTinh);
            if (matinh != null)
                _matinh = matinh.ToString();
            else
                _matinh = "";
            var huyen = (from h in _dataContext.DmHuyens.Where(p => p.MaTinh == (_matinh)) select new { h.TenHuyen, h.MaHuyen }).OrderBy(p => p.TenHuyen).ToList();
            lupMaHuyen.DataSource = huyen.ToList();

            var dtuong = grvDsBNhan.GetFocusedRowCellValue(colDTuong);
            var dchi = grvDsBNhan.GetFocusedRowCellValue(colDiaChi).ToString();
            if (dtuong.ToString() == "Dịch vụ" && (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "56789") || DungChung.Bien.MaBV == "30303")
            {
                TenTinh = _dataContext.DmTinhs.Where(p => p.MaTinh == _matinh).Select(p => p.TenTinh).FirstOrDefault();
                if (TenTinh != null)
                    grvDsBNhan.SetFocusedRowCellValue(colDiaChi, TenTinh);
            }
            Frm_Import_DsBNhan_excel_Load(sender, e);
        }

        private void lupMaTinhKhaiSinh_Leave(object sender, EventArgs e)
        {
            var matinh = grvDsBNhan.GetFocusedRowCellValue(colMaTinhKhaiSinh);
            if (matinh != null)
                _matinh = matinh.ToString();
            else
                _matinh = "";
            var huyen = (from h in _dataContext.DmHuyens.Where(p => p.MaTinh == (_matinh)) select new { h.TenHuyen, h.MaHuyen }).OrderBy(p => p.TenHuyen).ToList();
            lupMaHuyenKhaiSinh.DataSource = huyen.ToList();

            //var dtuong = grvDsBNhan.GetFocusedRowCellValue(colDTuong);
            //var dchi = grvDsBNhan.GetFocusedRowCellValue(colDiaChi).ToString();
            //if (dtuong.ToString() == "Dịch vụ" && (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "56789") || DungChung.Bien.MaBV == "30303")
            //{
            //    var TenTinh = _dataContext.DmTinhs.Where(p => p.MaTinh == _matinh).Select(p => p.TenTinh).FirstOrDefault();
            //    if (TenTinh != null)
            //        grvDsBNhan.SetFocusedRowCellValue(colDiaChi, TenTinh);
            //}
            Frm_Import_DsBNhan_excel_Load(sender, e);
        }

        private void lupMaHuyen_Leave(object sender, EventArgs e)
        {
            var maHuyen = grvDsBNhan.GetFocusedRowCellValue(colMaHuyen);
            if (lupMaHuyen != null)
                _mahuyen = maHuyen.ToString();
            else
                _mahuyen = "";
            string DiaChi = "";
            if (DungChung.Bien.MaBV == "30372")
                DiaChi = grvDsBNhan.GetFocusedRowCellValue(colDiaChi).ToString();
            else
                DiaChi = grvDsBNhan.GetFocusedRowCellValue(colDiaChi).ToString();
            var xa = (from x in _dataContext.DmXas.Where(p => p.MaHuyen == (_mahuyen)) select new { x.TenXa, x.MaXa }).OrderBy(p => p.TenXa).ToList();
            lupMaXa.DataSource = xa.ToList();
            var dtuong = grvDsBNhan.GetFocusedRowCellValue(colDTuong).ToString();
            if (dtuong == "Dịch vụ" && (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "56789" || DungChung.Bien.MaBV == "30303"))
            {
                string[] _lTentinh = DiaChi.Split('-');
                var TenHuyen = _dataContext.DmHuyens.Where(p => p.MaHuyen == _mahuyen).Select(p => p.TenHuyen).FirstOrDefault();
                if (TenHuyen != null)
                {
                    if (_lTentinh.Length > 1)
                    {
                        grvDsBNhan.SetFocusedRowCellValue(colMaHuyen, TenHuyen.ToString() + "-" + _lTentinh[1].ToString());
                    }
                    else
                    {
                        grvDsBNhan.SetFocusedRowCellValue(colMaHuyen, TenHuyen.ToString() + "-" + TenTinh);
                    }
                }
            }
            Frm_Import_DsBNhan_excel_Load(sender, e);
        }

        private void lupMaHuyenKhaiSinh_Leave(object sender, EventArgs e)
        {
            var maHuyen = grvDsBNhan.GetFocusedRowCellValue(colMaHuyenKhaiSinh);
            if (lupMaHuyen != null)
                _mahuyen = maHuyen.ToString();
            else
                _mahuyen = "";
            string DiaChi = "";
            if (DungChung.Bien.MaBV == "30372")
                DiaChi = grvDsBNhan.GetFocusedRowCellValue(colDiaChi).ToString();
            else
                DiaChi = grvDsBNhan.GetFocusedRowCellValue(colDiaChi).ToString();
            var xa = (from x in _dataContext.DmXas.Where(p => p.MaHuyen == (_mahuyen)) select new { x.TenXa, x.MaXa }).OrderBy(p => p.TenXa).ToList();
            lupMaXaKhaiSinh.DataSource = xa.ToList();
            var dtuong = grvDsBNhan.GetFocusedRowCellValue(colDTuong).ToString();
            if (dtuong == "Dịch vụ" && (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "56789" || DungChung.Bien.MaBV == "30303"))
            {
                string[] _lTentinh = DiaChi.Split('-');
                var TenHuyen = _dataContext.DmHuyens.Where(p => p.MaHuyen == _mahuyen).Select(p => p.TenHuyen).FirstOrDefault();
                if (TenHuyen != null)
                {
                    if (_lTentinh.Length > 1)
                    {
                        grvDsBNhan.SetFocusedRowCellValue(colMaHuyen, TenHuyen.ToString() + "-" + _lTentinh[1].ToString());
                    }
                    else
                    {
                        grvDsBNhan.SetFocusedRowCellValue(colMaHuyen, TenHuyen.ToString() + "-" + TenTinh);
                    }
                }
            }
            Frm_Import_DsBNhan_excel_Load(sender, e);
        }

        private void lupMaXa_Leave(object sender, EventArgs e)
        {

        }

        private void lupMaXaKhaiSinh_Leave(object sender, EventArgs e)
        {

        }
        private int soDK = 0;
        private List<Person> _person = new List<Person>();
        private string personCode = "";
        private void btnNhapDsBN_Click(object sender, EventArgs e)
        {

            if (dmBNhan.Count > 0)
            {
                foreach (var item in dmBNhan)
                {
                    _person = (_dataContext.People.Where(p => p.SThe.Contains(item.SThe)).OrderBy(a => a.TenBNhan)).ToList();
                    var personid = _person.First().IDPerson;
                    Person person = new Person();
                    if (personid <= 0)
                    {
                        if (item.TenBNhan != null)
                        {
                            person.TenBNhan = item.TenBNhan;
                        }
                        else
                        {
                            XtraMessageBox.Show("Chưa nhập tên của bệnh nhân", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            continue;
                        }

                        if (item.GTinh != null)
                        {
                            person.GTinh = item.GTinh == "Nam" ? 1 : 0;
                        }
                        else
                        {
                            XtraMessageBox.Show("Chưa nhập giới tính của bệnh nhân", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            continue;
                        }

                        person.NgaySinh = item.NgaySinh;
                        person.ThangSinh = item.ThangSinh;
                        if (item.NamSinh != null)
                        {
                            person.NSinh = Convert.ToInt32(item.NamSinh);
                        }
                        else
                        {
                            XtraMessageBox.Show("Chưa nhập năm sinh của bệnh nhân", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            continue;
                        }
                        if (item.DiaChi != null)
                        {
                            person.DChi = item.DiaChi;
                        }
                        else
                        {
                            XtraMessageBox.Show("Chưa nhập địa chỉ của bệnh nhân", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            continue;
                        }

                        if (item.DTuong != null)
                        {
                            var dtuong = loaiDoiTuongs.Where(x => x.Name.ToLower().Equals(item.DTuong.ToLower())).Select(x => new { x.Name, x.Id }).FirstOrDefault();

                            if (dtuong.Name.ToLower().Equals("BHYT".ToLower()))
                            {
                                if (!string.IsNullOrEmpty(item.SThe))
                                {
                                    person.SThe = item.SThe;
                                }
                                else
                                {
                                    XtraMessageBox.Show("Chưa nhập số thẻ bh của bệnh nhân", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    continue;
                                }

                                if (!string.IsNullOrEmpty(item.MaCS))
                                {
                                    var cs = _dataContext.BenhViens.Where(x => x.MaBV == item.MaCS).Select(x => new { x.MaBV, x.status }).FirstOrDefault();
                                    if (cs.status == 4 || cs.status == null)
                                    {
                                        person.MaCS = cs.MaBV;
                                    }
                                    else
                                    {
                                        XtraMessageBox.Show("Bệnh nhân có cơ sở đăng ký khám bệnh trên bảo hiểm trong trạng thái không được khám tại đây", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        continue;
                                    }

                                    if (!string.IsNullOrEmpty(item.HanBHTu))
                                    {
                                        person.HanBHTu = Convert.ToDateTime(item.HanBHTu);
                                    }
                                    else
                                    {
                                        XtraMessageBox.Show("Chưa nhập hạn bắt đầu bảo hiểm của bệnh nhân", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        continue;
                                    }

                                    if (!string.IsNullOrEmpty(item.HanBHDen))
                                    {
                                        person.HanBHDen = Convert.ToDateTime(item.HanBHDen);
                                    }
                                    else
                                    {
                                        XtraMessageBox.Show("Chưa nhập hạn cuối bảo hiểm của bệnh nhân", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        continue;
                                    }

                                    if (!string.IsNullOrEmpty(item.NgayHM))
                                    {
                                        person.NgayHM = Convert.ToDateTime(item.NgayHM);
                                    }
                                    else
                                    {
                                        XtraMessageBox.Show("Chưa nhập ngày hạn mức bảo hiểm của bệnh nhân", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        continue;
                                    }
                                }
                                else
                                {
                                    XtraMessageBox.Show("Chưa nhập cơ sở đăng ký khám bệnh trên bảo hiểm của bệnh nhân", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    continue;
                                }
                                person.KhuVuc = item.KhuVuc;
                            }
                            else
                            {
                                person.SThe = null;
                                person.HanBHTu = null;
                                person.HanBHDen = null;
                                person.NgayHM = null;
                                person.KhuVuc = null;
                                person.MaCS = null;
                            }
                        }
                        else
                        {
                            XtraMessageBox.Show("Chưa nhập giới tính của bệnh nhân", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            continue;
                        }

                        _dataContext.People.Add(person);
                        _dataContext.SaveChanges();
                        var updatePersonCode = _dataContext.People.FirstOrDefault(o => o.IDPerson == person.IDPerson);
                        if (updatePersonCode != null)
                        {
                            updatePersonCode.PersonCode = DungChung.Bien.MaBV + person.NSinh.Value.ToString("D4") + person.GTinh + person.IDPerson;
                            _dataContext.SaveChanges();
                            personCode = updatePersonCode.PersonCode;
                        }
                        personid = person.IDPerson;
                    }
                    else
                    {
                        var _person = _dataContext.People.Single(p => p.IDPerson == personid);
                        if (_person != null)
                        {
                            if (!string.IsNullOrWhiteSpace(_person.SThe)
                                && !string.IsNullOrWhiteSpace(item.SThe) &&
                                _person.SThe.ToUpper() != item.SThe.ToUpper() ||
                                _person.TenBNhan != item.TenBNhan)
                            {
                                MessageBox.Show("Bệnh nhân không khớp với thông tin đã có. " +
                                    "Vui lòng gọi hỗ trợ");
                                continue;
                            }
                        }
                    }
                    

                    BenhNhan benhnhan = new BenhNhan();
                    //benhnhan
                    if (item.TenBNhan != null)
                    {
                        benhnhan.TenBNhan = item.TenBNhan;
                    }
                    else
                    {
                        XtraMessageBox.Show("Chưa nhập tên của bệnh nhân", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        continue;
                    }

                    if (item.GTinh != null)
                    {
                        benhnhan.GTinh = item.GTinh == "Nam" ? 1 : 0;
                    }
                    else
                    {
                        XtraMessageBox.Show("Chưa nhập giới tính của bệnh nhân", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        continue;
                    }
                    benhnhan.NgaySinh = item.NgaySinh;
                    benhnhan.ThangSinh = item.ThangSinh;
                    if (item.NamSinh != null)
                    {
                        benhnhan.NamSinh = item.NamSinh;
                    }
                    else
                    {
                        XtraMessageBox.Show("Chưa nhập năm sinh của bệnh nhân", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        continue;
                    }
                    if (item.DiaChi != null)
                    {
                        benhnhan.DChi = item.DiaChi;
                    }
                    else
                    {
                        XtraMessageBox.Show("Chưa nhập địa chỉ của bệnh nhân", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        continue;
                    }
                    if (item.NoiTru != null)
                    {
                        //if (item.NoiTru.ToLower().Equals("Ngoại trú".ToLower()))
                        //{
                        //    benhnhan.NoiTru = 0;
                        //}
                        //else if (item.NoiTru.ToLower().Equals("Nội trú".ToLower()))
                        //{
                        //    benhnhan.NoiTru = 1;
                        //}
                        var ntru = loaiNoiTrus.Where(x => x.Name.ToLower().Equals(item.NoiTru.ToLower())).Select(x => x.Id).FirstOrDefault();
                        benhnhan.NoiTru = ntru;

                    }
                    else
                    {
                        XtraMessageBox.Show("Chưa nhập tình hình mong muốn nhập viện của bệnh nhân", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        continue;
                    }

                    if (item.DTuong != null)
                    {
                        var dtuong = loaiDoiTuongs.Where(x => x.Name.ToLower().Equals(item.DTuong.ToLower())).Select(x => new { x.Name, x.Id }).FirstOrDefault();
                        benhnhan.DTuong = dtuong.Name;
                        benhnhan.IDDTBN = Convert.ToByte(dtuong.Id);
                        if (dtuong.Name.ToLower().Equals("BHYT".ToLower()))
                        {
                            if (!string.IsNullOrEmpty(item.SThe))
                            {
                                benhnhan.SThe = item.SThe;
                            }
                            else
                            {
                                XtraMessageBox.Show("Chưa nhập số thẻ bh của bệnh nhân", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                continue;
                            }

                            if (!string.IsNullOrEmpty(item.MaCS))
                            {
                                var cs = _dataContext.BenhViens.Where(x => x.MaBV == item.MaCS).Select(x => new { x.MaBV, x.status }).FirstOrDefault();
                                if (cs.status == 4 || cs.status == null)
                                {
                                    benhnhan.MaCS = cs.MaBV;
                                }
                                else
                                {
                                    XtraMessageBox.Show("Bệnh nhân có cơ sở đăng ký khám bệnh trên bảo hiểm trong trạng thái không được khám tại đây", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    continue;
                                }

                                if (!string.IsNullOrEmpty(item.Tuyen))
                                {
                                    var tuyen = loaiTuyens.Where(x => x.Name.ToLower().Equals(item.Tuyen)).Select(x => x.Id).FirstOrDefault();
                                    benhnhan.Tuyen = tuyen;
                                }
                                else
                                {
                                    XtraMessageBox.Show("Chưa nhập tuyến của bệnh nhân", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    continue;
                                }

                                if (!string.IsNullOrEmpty(item.HanBHTu))
                                {
                                    benhnhan.HanBHTu = Convert.ToDateTime(item.HanBHTu);
                                }
                                else
                                {
                                    XtraMessageBox.Show("Chưa nhập hạn bắt đầu bảo hiểm của bệnh nhân", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    continue;
                                }

                                if (!string.IsNullOrEmpty(item.HanBHDen))
                                {
                                    benhnhan.HanBHDen = Convert.ToDateTime(item.HanBHDen);
                                }
                                else
                                {
                                    XtraMessageBox.Show("Chưa nhập hạn cuối bảo hiểm của bệnh nhân", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    continue;
                                }

                                if (!string.IsNullOrEmpty(item.NgayHM))
                                {
                                    benhnhan.NgayHM = Convert.ToDateTime(item.NgayHM);
                                }
                                else
                                {
                                    XtraMessageBox.Show("Chưa nhập ngày hạn mức bảo hiểm của bệnh nhân", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    continue;
                                }
                                benhnhan.MucHuong = Convert.ToDecimal(item.SThe.Substring(2, 1));

                                if (!string.IsNullOrEmpty(item.NoiTinh))
                                {
                                    var noitinh = loaiNoiTinhs.Where(x => x.Name.ToLower().Equals(item.Tuyen)).Select(x => x.Id).FirstOrDefault();
                                    benhnhan.NoiTinh = noitinh;
                                }
                                else
                                {
                                    XtraMessageBox.Show("Chưa nhập khu vực của bệnh nhân đến khám", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    continue;
                                }
                                benhnhan.KhuVuc = item.KhuVuc;
                            }
                            else
                            {
                                XtraMessageBox.Show("Chưa nhập cơ sở đăng ký khám bệnh trên bảo hiểm của bệnh nhân", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                continue;
                            }
                        }
                    }
                    else
                    {
                        XtraMessageBox.Show("Chưa nhập giới tính của bệnh nhân", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        continue;
                    }

                    benhnhan.NNhap = DateTime.UtcNow;
                    bool _ngoaih = false;
                    _ngoaih = DungChung.Ham.CheckNGioHC(DateTime.UtcNow);

                    if (_ngoaih == true)
                    {
                        benhnhan.NgoaiGio = 1;
                    }
                    else { benhnhan.NgoaiGio = 0; }

                    if (item.MaKP != null)
                    {
                        benhnhan.MaKP = Convert.ToInt32(item.MaKP);
                    }
                    else
                    {
                        XtraMessageBox.Show("Chưa nhập khoa phòng khám của bệnh nhân " + item.TenBNhan, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        continue;
                    }
                    if (!string.IsNullOrWhiteSpace(item.Tuoi))
                    {
                        benhnhan.Tuoi = Convert.ToInt32(item.Tuoi);
                    }
                    else
                        benhnhan.Tuoi = null;
                    if (item.TChung != null)
                    {
                        benhnhan.TChung = item.TChung;
                    }
                    else
                    {
                        XtraMessageBox.Show("Chưa nhập triệu chứng của bệnh nhân " + item.TenBNhan, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        continue;
                    }
                    if (!string.IsNullOrEmpty(item.CapCuu))
                    {
                        var capcuu = loaiCapCuus.Where(x => x.Name.ToLower().Equals(item.Tuyen)).Select(x => x.Id).FirstOrDefault();
                        benhnhan.CapCuu = capcuu;
                    }
                    else
                    {
                        XtraMessageBox.Show("Chưa nhập khu vực của bệnh nhân đến khám", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        continue;
                    }
                    benhnhan.CDNoiGT = item.CDNoiGT;
                    benhnhan.DTNT = false;
                    benhnhan.MaKCB = DungChung.Bien.MaBV;
                    benhnhan.MaBV = item.MaBV;
                    benhnhan.ChuyenKhoa = item.ChuyenKhoa;
                    benhnhan.TuyenDuoi = DungChung.Bien._tuyenduoi;
                    benhnhan.KhuVuc = item.KhuVuc;
                    if (item.LuongCS != null)
                    {
                        benhnhan.LuongCS = Convert.ToDecimal(item.LuongCS);
                    }
                    benhnhan.UuTien = item.UuTien == null ? false : Convert.ToBoolean(item.UuTien);
                    benhnhan.NoThe = item.NoThe == null ? false : Convert.ToBoolean(item.NoThe);
                    benhnhan.Export = item.Export == null ? false : Convert.ToBoolean(item.Export);
                    benhnhan.Normal = item.Normal == null ? -1 : Convert.ToInt32(item.Normal);
                    benhnhan.IDPerson = personid;
                    benhnhan.Ma_lk = item.Ma_lk;
                    benhnhan.SoDK = soDK;
                    benhnhan.MaCB = item.MaCB == null ? DungChung.Bien.MaCB : item.MaCB;
                    benhnhan.PID = item.PID;
                    benhnhan.PLKham = item.PLKham == null ? -1 : Convert.ToInt32(item.PLKham);
                    benhnhan.MaKPDTKH = item.MaKPDTKH == null ? -1 : Convert.ToInt32(item.MaKPDTKH);
                    benhnhan.SoHSBA = item.SoHSBA;
                    benhnhan.PersonCode = personCode;
                    benhnhan.BNhanLao = item.BNhanLao == null ? false : Convert.ToBoolean(item.BNhanLao);
                    benhnhan.IsCCCD = item.IsCCCD == null ? false : Convert.ToBoolean(item.IsCCCD);
                    int _sott = 1;
                    if (!string.IsNullOrEmpty(item.SoTT))
                    {
                        _sott = int.Parse(item.SoTT);
                        int _sott_tam = _sott;
                        for (int i = 1; i < 10; i++)
                        {
                            int _soTT_KT = _KT_SoTT(_dataContext, DateTime.UtcNow.Date,
                                item.MaKP == null ? 0 :
                                Convert.ToInt32(item.MaKP), _sott);
                            if (_sott_tam < _soTT_KT)
                            {
                                _sott_tam++;
                            }
                            else
                            {
                                _sott = _sott_tam;
                                break;
                            }
                        }
                        item.SoTT = _sott.ToString();

                        benhnhan.SoTT = int.Parse(item.SoTT);
                    }
                    benhnhan.Status = 0;
                    _dataContext.BenhNhans.Add(benhnhan);
                    if (_dataContext.SaveChanges() >= 0)
                    {
                        string ten = frmHSBN.ToFirstUpper(item.TenBNhan);
                        _dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

                        _setSoTT(_dataContext, benhnhan.NNhap.Value, item.MaKP == null ? 0 : Convert.ToInt32(item.MaKP), _sott, benhnhan.MaBNhan);
                        _mabnhan = benhnhan.MaBNhan;
                        //lưu thông tin bổ xung

                        var kt = _dataContext.TTboXungs.Where(p => p.MaBNhan == _mabnhan).ToList();
                        TTboXung tboXung = new TTboXung();
                        tboXung.MaBNhan = _mabnhan;
                        tboXung.MaNN = item.MaNN;
                        tboXung.NoiLV = item.NoiLV;
                        tboXung.DiaChiNoiLV = item.DiaChiNoiLV;
                        tboXung.DThoai = item.DThoai;
                        tboXung.NThan = item.NThan;
                        tboXung.DCNguoiThan = item.DCNguoiThan;
                        tboXung.DThoaiNT = item.DThoaiNT;
                        tboXung.SoKSinh = item.SoKSinh;
                        tboXung.NgoaiKieu = item.NgoaiKieu;
                        tboXung.MaDT = item.MaDT;
                        tboXung.CMT = item.CMT;
                        tboXung.NgayCapCMT = Convert.ToDateTime(item.NgayCapCMT);
                        tboXung.NoiCapCMT = item.NoiCapCMT;
                        tboXung.MaTinh = item.MaTinh;
                        tboXung.MaHuyen = item.MaHuyen;
                        tboXung.MaXa = item.MaXa;
                        tboXung.MaTinhKhaiSinh = item.MaTinhKhaiSinh;
                        tboXung.DchiKhaiSinh = item.DiaChiKhaiSinh;
                        tboXung.MaHuyenKhaiSinh = item.MaHuyenKhaiSinh;
                        tboXung.MaXaKhaiSinh = item.MaXaKhaiSinh;
                        tboXung.HTThanhToan = chkChuyenKhoan.ValueChecked == null ? 0 : Convert.ToInt32(chkChuyenKhoan.ValueChecked);
                        _dataContext.TTboXungs.Add(tboXung);
                        if (_dataContext.SaveChanges() >= 0)
                        {
                            XtraMessageBox.Show("Thêm mới thành công bệnh nhân " + item.TenBNhan, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    soDK++;
                }
            }
        }

        public static bool _setSoTT(QLBV_Database.QLBVEntities _data, DateTime _dt, int _makp, int _sott, int _mabn)
        {
            try
            {
                var kt = _data.SoTTs.Where(p => p.NgayDK == _dt.Date && p.MaKP == _makp && p.SoTT1 == _sott).ToList();
                if (kt.Count <= 0)
                {
                    SoTT moi = new SoTT();
                    moi.NgayDK = _dt.Date;
                    moi.MaKP = _makp;
                    moi.SoTT1 = _sott;
                    moi.MaBNhan = _mabn;
                    _data.SoTTs.Add(moi);
                    _data.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private int _mabnhan = 0;
        private int _KT_SoTT(QLBV_Database.QLBVEntities _data, DateTime _dt, int _makp, int _sott)
        {
            var maxSTT = _data.SoTTs.Where(p => p.NgayDK == _dt.Date && p.MaKP == _makp && p.SoTT1 == _sott).ToList();
            if (maxSTT.Count > 0)
                return _getSoTT(_data, _dt, _makp);
            return _sott;
        }

        public static int _getSoTT(QLBV_Database.QLBVEntities _data, DateTime _dt, int _makp)
        {
            int _sott = 1;
            var maxSTT = _data.SoTTs.Where(p => p.NgayDK == _dt.Date && p.MaKP == _makp).ToList();
            if (maxSTT.Count > 0)
                _sott = maxSTT.Max(p => p.SoTT1) + 1;
            return _sott;
        }

        class CheckNhom
        {
            public int checkIDNhom { get; set; }
        }
        class LoaiKhuVuc 
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}