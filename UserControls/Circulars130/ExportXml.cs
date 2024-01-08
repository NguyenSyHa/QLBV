using DevExpress.XtraEditors;
using QLBV.Models.Business.HealthInsuranceAppraisals.Circular130;
using QLBV.Models.Dictionaries.KPhongs;
using QLBV.Utilities.Commons;
using QLBV_Database;
using QLBV_Database.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLBV.UserControls.Circulars130
{
    public partial class ExportXml : XtraUserControl
    {
        private readonly QLBVEntities _dbContext;

        public ExportXml()
        {
            InitializeComponent();
            _dbContext = EntityDbContext.DbContext;

            this.Load += ExportXml_Load;
        }

        private void ExportXml_Load(object sender, EventArgs e)
        {
            InitData();
            //LoadDataSource();
        }

        #region Properties

        public IList<PatientViewExportModel> PatientViewExports
        {
            get => (IList<PatientViewExportModel>)grcExportXml.DataSource;
            set => grcExportXml.DataSource = value;
        }

        public IList<KPhongModel> KPhongs
        {
            get => (List<KPhongModel>)lkuDepartment.Properties.DataSource;
            set => lkuDepartment.Properties.DataSource = value;
        }

        public int DepartmentId
        {
            get => (int)lkuDepartment.EditValue;
            set => lkuDepartment.EditValue = value;
        }

        public DateTime FromDate
        {
            get => (DateTime)dtFromDate.EditValue;
            set => dtFromDate.EditValue = value;
        }

        public DateTime ToDate
        {
            get => (DateTime)dtToDate.EditValue;
            set => dtToDate.EditValue = value;
        }

        public int TreatmentType
        {
            get => rdoTreatment.SelectedIndex;
            set => rdoTreatment.SelectedIndex = value;
        }

        public int TypeDate
        {
            get => cboTypeDate.SelectedIndex;
            set => cboTypeDate.SelectedIndex = value;
        }

        public bool Status
        {
            get => rdoStatus.SelectedIndex == 1 ? true : false;
            set => rdoStatus.SelectedIndex = value ? 1 : 0;
        }

        public int ExportType
        {
            get => rdoExportType.SelectedIndex;
            set => rdoExportType.SelectedIndex = value;
        }

        public int PatientFilterType
        {
            get => rdoPatientFilterType.SelectedIndex;
            set => rdoPatientFilterType.SelectedIndex = value;
        }

        public bool WrongPatient
        {
            get => chkWrongPatient.Checked;
            set => chkWrongPatient.Checked = value;
        }

        public string FilePath
        {
            get => txtFilePath.Text;
            set => txtFilePath.Text = value;
        }

        public string Search
        {
            get => txtSearch.Text;
            set => txtSearch.Text = value;
        }

        public int IdThuoc = 0;
        public int IdMau = 0;
        public int IdXN = 0;
        public int IdCDHA = 0;
        public int IdTTPT = 0;
        public int IdCongKham = 0;
        public int IdDVKTC = 0;
        public int IdVTYT = 0;
        public int IdNgayGiuongNoiTru = 0;
        public int IdNgayGiuongNgoaiTru = 0;
        public int IdChiPhiVC = 0;
        public int IdVTTT = 0;
        public int IdThuocUngThuCTG = 0;
        public int IdHoaChat = 0;

        #endregion

        #region Function

        public void InitData()
        {
            FromDate = DateTime.Now.FromDate();
            ToDate = DateTime.Now.ToDate();
            TypeDate = 1;
            TreatmentType = 2;
            Status = false;
            ExportType = 1;
            PatientFilterType = 0;
            WrongPatient = false;

            KPhongs = GetKPhongs();
            ServiceGroup();
        }

        public void ServiceGroup()
        {
            var tenNhom = _dbContext.NhomDVs.ToList();
            foreach (var item in tenNhom)
            {
                switch (item.TenNhomCT)
                {
                    case "Thuốc trong danh mục BHYT":
                        IdThuoc = item.IDNhom;
                        break;
                    case "Máu và chế phẩm của máu":
                        IdMau = item.IDNhom;
                        break;
                    case "Xét nghiệm":
                        IdXN = item.IDNhom;
                        break;
                    case "Chẩn đoán hình ảnh":
                        IdCDHA = item.IDNhom;
                        break;
                    case "Thủ thuật, phẫu thuật":
                        IdTTPT = item.IDNhom;
                        break;
                    case "Khám bệnh":
                        IdCongKham = item.IDNhom;
                        break;
                    case "DVKT thanh toán theo tỷ lệ":
                        IdDVKTC = item.IDNhom;
                        break;
                    case "Vật tư y tế trong danh mục BHYT":
                        IdVTYT = item.IDNhom;
                        break;
                    case "Giường điều trị ngoại trú":
                        IdNgayGiuongNgoaiTru = item.IDNhom;
                        break;
                    case "Giường điều trị nội trú":
                        IdNgayGiuongNoiTru = item.IDNhom;
                        break;
                    case "Vận chuyển":
                        IdChiPhiVC = item.IDNhom;
                        break;
                    case "VTYT thanh toán theo tỷ lệ":
                        IdVTTT = item.IDNhom;
                        break;
                    case "Thuốc điều trị ung thư, chống thải ghép ngoài danh mục":
                        IdThuocUngThuCTG = item.IDNhom;
                        break;
                    case "Nhóm hóa chất":
                        IdHoaChat = item.IDNhom;
                        break;
                }
            }
        }

        public void LoadDataSource()
        {
            CursorState.SetBusyState();

            try
            {
                int patientType = _dbContext.DTBNs.FirstOrDefault(p => p.HTTT == 1).IDDTBN;

                var data = (from bn in _dbContext.BenhNhans
                            join rv in _dbContext.RaViens.Where(p => DungChung.Bien.MaBV == "01049" ? (p.Status == 1 ? p.MaBVC != "01071" : true) : true) on bn.MaBNhan equals rv.MaBNhan
                            join vp in _dbContext.VienPhis on rv.MaBNhan equals vp.MaBNhan
                            join vpct in _dbContext.VienPhicts.Where(p => p.TrongBH == 1) on vp.idVPhi equals vpct.idVPhi
                            where (TypeDate == 2 ? (vp.NgayDuyet >= ToDate && vp.NgayDuyet <= FromDate) : (TypeDate == 1 ? (vp.NgayTT >= FromDate && vp.NgayTT <= ToDate) : (rv.NgayRa >= FromDate && rv.NgayRa <= ToDate)))
                            where (bn.MaKCB == DungChung.Bien.MaBV && (ExportType == 0 ? (vp.ExportBYT == Status) : (ExportType == 1 ? vp.ExportBHXH == Status : vp.Export == Status)))
                            where (TreatmentType == 2 ? true : bn.NoiTru == TreatmentType) && bn.IDDTBN == patientType
                            select new
                            {
                                vp.NgayGuiBHXH,
                                ExPort = false,
                                vp.LyDo,
                                vp.MaGD_BHXH,
                                vpct.TrongBH,
                                vp.MaKP,
                                bn.DChi,
                                bn.HanBHDen,
                                bn.HanBHTu,
                                bn.TuyenDuoi,
                                bn.DTNT,
                                bn.DTuong,
                                bn.NoiTru,
                                bn.NoiTinh,
                                bn.Tuyen,
                                bn.MaBNhan,
                                bn.TenBNhan,
                                bn.NamSinh,
                                bn.NgaySinh,
                                bn.ThangSinh,
                                bn.SThe,
                                bn.GTinh,
                                bn.MaCS,
                                bn.MaDTuong,
                                bn.CapCuu,
                                rv.MaICD,
                                ChanDoan = (rv.MaICD != null && rv.MaICD != "") ? rv.ChanDoan : "",
                                rv.NgayVao,
                                rv.NgayRa,
                                rv.SoNgaydt,
                                rv.Status,
                                rv.KetQua,
                                vpct.MaDV,
                                vpct.ThanhTien,
                                vpct.TienBN,
                                vpct.TienBH,
                                vp.NgayTT,
                                bn.Tuoi,
                                bn.KhuVuc,
                                bn.MaBV,
                                bn.NNhap
                            });

                var resutl = (from a in data
                              join dv in _dbContext.DichVus on a.MaDV equals dv.MaDV
                              where (a.TrongBH == 1)
                              where (DepartmentId == 0 ? true : a.MaKP == DepartmentId)
                              group new { a, dv }
                              by new
                              {
                                  a.NgayGuiBHXH,
                                  a.MaKP,
                                  a.MaGD_BHXH,
                                  a.LyDo,
                                  a.NNhap,
                                  a.ExPort,
                                  a.HanBHDen,
                                  a.HanBHTu,
                                  a.DChi,
                                  a.SoNgaydt,
                                  a.DTNT,
                                  a.TuyenDuoi,
                                  a.NgayTT,
                                  a.DTuong,
                                  a.NoiTru,
                                  a.TrongBH,
                                  a.NoiTinh,
                                  a.MaBNhan,
                                  a.TenBNhan,
                                  a.NamSinh,
                                  a.NgaySinh,
                                  a.ThangSinh,
                                  a.GTinh,
                                  a.SThe,
                                  a.MaCS,
                                  a.Tuyen,
                                  a.NgayVao,
                                  a.MaICD,
                                  a.ChanDoan,
                                  a.NgayRa,
                                  a.MaDTuong,
                                  a.CapCuu,
                                  a.KetQua,
                                  a.Status,
                                  a.Tuoi,
                                  a.KhuVuc,
                                  a.MaBV
                              } into kq
                              select new PatientViewExportModel()
                              {
                                  PatientCode = kq.Key.MaBNhan,
                                  PatientName = kq.Key.TenBNhan,
                                  DOB = kq.Key.NgaySinh,
                                  Selected = false,
                                  HeinCard = kq.Key.SThe,
                                  IcdCode = kq.Key.MaICD,
                                  ExamDate = kq.Key.NgayVao,
                                  OutTime = kq.Key.NgayRa,
                                  TotalTreatment = kq.Key.SoNgaydt == null ? 0 : (kq.Key.SoNgaydt ?? 0),
                                  TotalAmont = kq.Sum(s => s.a.ThanhTien),
                                  TestAmount = kq.Where(p => p.dv.IDNhom == IdXN).Sum(p => p.a.ThanhTien),
                                  //ImageAnalysationAmount = kq.Where(p => p.dv.IDNhom == IdCDHA).Sum(p => p.a.ThanhTien) + kq.Where(p => p.dv.IDNhom == 3).Sum(p => p.a.ThanhTien),
                                  //MedicineAmount = kq.Where(p => p.dv.IDNhom == IdThuoc).Where(p => p.dv.BHTT == 100).Sum(p => p.a.ThanhTien),
                                  //BloodAmout = kq.Where(p => p.dv.IDNhom == IdMau).Sum(p => p.a.ThanhTien),
                                  //SurgicalAmount = kq.Where(p => p.dv.IDNhom == IdTTPT).Sum(p => p.a.ThanhTien),
                                  //MaterialAmount = kq.Where(p => p.dv.IDNhom == IdVTYT).Sum(p => p.a.ThanhTien),
                                  //MaterialRateAmount = kq.Where(p => p.dv.IDNhom == IdVTTT).Sum(p => p.a.ThanhTien),
                                  //OtherAmount = kq.Where(p => p.dv.IDNhom == IdDVKTC).Sum(p => p.a.ThanhTien),
                                  //MedicineRateAmount = kq.Where(p => p.dv.IDNhom == IdThuocUngThuCTG).Where(p => p.dv.BHTT != 100).Sum(p => p.a.ThanhTien),
                                  //BedAmount = ExportType == 0 ? kq.Where(p => p.dv.IDNhom == IdNgayGiuongNgoaiTru).Sum(p => p.a.ThanhTien) : kq.Where(p => p.dv.IDNhom == IdNgayGiuongNoiTru).Sum(p => p.a.ThanhTien),
                                  //TransportAmount = kq.Where(p => p.dv.IDNhom == IdChiPhiVC).Sum(p => p.a.ThanhTien),
                                  PatientPayAmount = kq.Sum(p => p.a.TienBN),
                                  InsurancePayAmount = kq.Sum(p => p.a.TienBH),
                                  //OutHeinAmount = kq.Where(p => p.dv.IDNhom == IdChiPhiVC).Sum(p => p.a.ThanhTien),
                                  SeeCost = "",
                                  Reason = kq.Key.LyDo ?? "" + kq.Key.MaGD_BHXH ?? "",
                                  PaymentTime = kq.Key.NgayTT,
                                  SendingTime = kq.Key.NgayGuiBHXH
                              }).ToList();

                grcExportXml.DataSource = resutl;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tìm kiếm! " + ex.Message);
            }
        }

        private IList<KPhongModel> GetKPhongs()
        {
            var kPhongs = _dbContext.KPhongs.Where(p => p.PLoai != null && p.PLoai.ToUpper() == ("LÂM SÀNG") || p.PLoai.ToUpper() == ("PHÒNG KHÁM")).ToList();
            return AppConfig.MyMapper.Map<IList<KPhongModel>>(kPhongs);
        }

        #endregion

        #region Event

        private void btnSearch_CheckedChanged(object sender, EventArgs e)
        {
            LoadDataSource();
        }

        private void btnCheckData_Click(object sender, EventArgs e)
        {

        }

        #endregion
    }
}
