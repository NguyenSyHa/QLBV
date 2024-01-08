using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO.Ports;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using System.Configuration;
using System.Threading;
using DevExpress.XtraBars;
using DevExpress.Data;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using System.Diagnostics;
namespace QLBV.FormNhap
{

    public partial class frmHSBN : DevExpress.XtraEditors.XtraUserControl
    {
        QLBV_Database.QLBVEntities DaTaContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        public string TEN = "";
        public int MaKPkb = 0;
        int Luu = 1;
        public frmHSBN()
        {

            InitializeComponent();
            this.Load += (e, r) =>
            {

            };

        }
        List<KPhong> _lKphong = new List<KPhong>();
        public frmHSBN(string TENBN)
            : this()
        {
            InitializeComponent();
            this.Load += (e, r) =>
            {
                TENBN = TEN;
            };
        }




        private void simpleButton1_Click(object sender, EventArgs e)
        {
            frmHSBNttll frm = new frmHSBNttll();
            frm.ShowDialog();
        }

        private void comboBoxEdit2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        class gioitinh
        {
            public int GTinh { set; get; }
            public string TenGTinh { set; get; }
        }

        #region EnableControl
        private void enableControl(bool T)
        {
            btnMoi2.Enabled = T;
            btnLuu2.Enabled = !T;
            btnsua2.Enabled = T;
            btnxoa2.Enabled = T;
        }
        #endregion
        List<BenhNhan> _lBenhNhan = new List<BenhNhan>();
        DateTime _dttu = DungChung.Ham.NgayTu(System.DateTime.Now);
        DateTime _dtden = DungChung.Ham.NgayDen(System.DateTime.Now);
        bool formLoad = false;
        private void frmHSBN_Load(object sender, EventArgs e)
        {
            formLoad = true;
            dtTuNgay.DateTime = System.DateTime.Now;
            dtDenNgay.DateTime = System.DateTime.Now;

            if (DungChung.Bien.MaBV != "30372" && DungChung.Bien.MaBV != "01071" && DungChung.Bien.MaBV != "27183" && DungChung.Bien.MaBV != "01049" && DungChung.Bien.MaBV != "12345" && DungChung.Bien.MaBV != "24297" && DungChung.Bien.MaBV != "56789" && DungChung.Bien.MaBV != "30303" && DungChung.Bien.MaBV != "30005" && DungChung.Bien.MaBV != "30007")
            {
                btnCDCLS.Enabled = false;
            }
            enableControl(true);

            if (DungChung.Ham.checkQuyen("frmHSBNNhapMoi")[3])
                barSuaBN.Enabled = true;
            else
                barSuaBN.Enabled = false;
            if (DungChung.Ham.checkQuyen("frmHSBNNhapMoi")[2])
                mnXoa.Enabled = true;
            else
                mnXoa.Enabled = false;

            if (DungChung.Ham.checkQuyen("frmHSBNNhapMoi")[4])
            {
                barSubItem4.Enabled = true;
                barButtonItem6.Enabled = true;
            }
            else
            {
                barSubItem4.Enabled = false;
                barButtonItem6.Enabled = false;
            }
            List<gioitinh> gtinh = new List<gioitinh>();
            gtinh.Add(new gioitinh { GTinh = 0, TenGTinh = "Nữ" });
            gtinh.Add(new gioitinh { GTinh = 1, TenGTinh = "Nam" });
            lup_Gtinh.DataSource = gtinh;
            dtNgayDK.DateTime = DateTime.Now;
            DaTaContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            strPort = SerialPort.GetPortNames();
            foreach (var item in strPort)
            {
                Port = item;
                if (!string.IsNullOrWhiteSpace(Port) && !string.IsNullOrEmpty(Port))
                {
                    this.ComPort = new Library_LED.LEDCommunication(Port);
                    if (!string.IsNullOrWhiteSpace("0") && !string.IsNullOrEmpty(""))
                    {
                        string a = this.ComPort.ShowView(0.ToString());
                        if (string.IsNullOrEmpty(a))
                            break;
                    }
                }
                else
                {
                    //
                }
            }
            _dttu = DungChung.Ham.NgayTu(dtTuNgay.DateTime);
            _dtden = DungChung.Ham.NgayDen(dtDenNgay.DateTime);
            MaKPkb = DungChung.Bien.MaKP;
            List<listKP> Kphong = (from kp in DaTaContext.KPhongs.Where(p => p.Status == 1) select new listKP { MaKP = kp.MaKP, TenKP = kp.TenKP }).OrderBy(p => p.TenKP).ToList();
            List<DTBN> _ldtbn = DaTaContext.DTBNs.OrderBy(p => p.DTBN1).Where(p => p.DTBN1.ToLower() != "ksk").ToList();
            _ldtbn.Add(new DTBN { IDDTBN = 99, DTBN1 = " Tất cả" });
            cboDTuong.Properties.DataSource = null;
            cboDTuong.Properties.DataSource = _ldtbn.OrderBy(p => p.DTBN1);

            if (Kphong.Count > 0)
            {

                //List<listKP> _lkp = new List<listKP>();
                //foreach (var i in Kphong)
                //{
                //    listKP themmoi = new listKP();
                //    themmoi.MaKP = i.MaKP;
                //    themmoi.TenKP = i.TenKP;
                //    _lkp.Add(themmoi);
                //}
                //listKP themmoi2 = new listKP();
                //themmoi2.MaKP = 0;
                //themmoi2.TenKP = " tất cả";

                Kphong.Insert(0, new listKP { MaKP = 0, TenKP = " tất cả" });
                lupMaKP.Properties.DataSource = Kphong.OrderBy(p => p.TenKP).ToList();
                lupKPhongcount.DataSource = Kphong.OrderBy(p => p.TenKP).ToList();
            }
            lupMaKPhs.DataSource = Kphong.ToList();
            cboDTuong.EditValue = 99;
            if (grvDSBN.GetFocusedRowCellValue(colTenBNhan) != null)
            {
                txtTENBN.Text = grvDSBN.GetFocusedRowCellValue(colTenBNhan).ToString();

            }
            if (DungChung.Bien.MaBV == "30007")
            {
                colPersonID.FieldName = "PersonCode";
            }
            if (DungChung.Bien.MaBV != "30372" && DungChung.Bien.MaBV != "12345" && DungChung.Bien.MaBV != "24297") colDThoai.Visible = false;
            TimKiem();
            TimKiemSoDK();
        }

        //Hàm tìm kiếm

        private void textEdit1_Properties_EditValueChanged(object sender, EventArgs e)
        {

        }
        private void cboRAVIEN_EditValueChanged(object sender, EventArgs e)
        {
            //string RaVien = "";
            string SoThe = "";
            if (!string.IsNullOrEmpty(txtTkTenBN.Text))
            {
                SoThe = txtTkTenBN.Text;

            }

            //var query = from BN in DaTaContext.BenhNhans
            //            select BN;
            //if (!string.IsNullOrEmpty(cboTkRAVIEN.Text)) {
            //    RaVien = cboTkRAVIEN.Text;
            //    if (RaVien == "Tất cả")
            //        grcDSBN.DataSource = query.Where(p => p.TenBNhan.Contains(SoThe));
            //    else
            //    grcDSBN.DataSource = query.Where(p => p.RaVien.Contains(RaVien)).Where(p => p.TenBNhan.Contains(SoThe));
            //}


        }

        private void txtTkTEN_EditValueChanged(object sender, EventArgs e)
        {
            //TimKiem();
            timkiem2();
            //var query = from BN in DaTaContext.BenhNhans
            //            select BN;
            //grcDSBN.DataSource = query.Where(p => p.RaVien.Contains(cboTkRAVIEN.Text)).Where(p => p.TenBNhan.Contains(txtTkTEN.Text));
        }

        private void btnSua_Click(object sender, EventArgs e)
        {


        }

        private void btnXoa_Click(object sender, EventArgs e)
        {

        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            /*//Thêm mới
            BNHAN BN = new BNHAN();
            BN.TENBN = txtNhapTBN.Text;
            BN.MSBN = "12123";
            //BN.STHE = txtSOTHE.Text;
            DaTaContext.BenhNhans.Add(BN);
            DaTaContext.SaveChanges();
            frmHSBN_Load(sender,e); 
             ketthuc*/
            //if (KTLuu())
            //{
            //    int ID = 0;
            //    if (!string.IsNullOrEmpty(txtIDBN.Text))
            //    {
            //        ID = Int32.Parse(txtIDBN.Text);
            //    }
            //    BenhNhan BN = DaTaContext.BenhNhans.Single(p => p.MaBNhan == ID);
            //    //BN.CapCuu = cboCapCuu.Text;
            //    //if (!string.IsNullOrEmpty(txtSOTHE.Text) && txtSOTHE.Text.Length > 0)
            //    //{
            //    //    BN.DTuong = txtSOTHE.Text.Substring(0, 2);
            //    //}

            //    DaTaContext.SaveChanges();
            //    grcDSBN.Enabled = true;
            //    btnLuu.Enabled = false;
            //    btnSua.Enabled = true;
            //    btnXoa.Enabled = true;
            //    frmHSBN_Load(sender, e);
            //}
        }
        // ham viet hoa chu cai dau
        #region ham viet hoa chu cai dau
        public static string ToFirstUpper(string s)
        {
            if (String.IsNullOrEmpty(s))
                return s;

            string result = "";

            //lấy danh sách các từ  

            string[] words = s.Split(' ');

            foreach (string word in words)
            {
                // từ nào là các khoảng trắng thừa thì bỏ  
                if (word.Trim() != "")
                {
                    if (word.Length > 1)
                        result += word.Substring(0, 1).ToUpper() + word.Substring(1).ToLower() + " ";
                    else
                        result += word.ToUpper() + " ";
                }

            }
            return result.Trim();
        }
        #endregion
        private void txtSOTHE_EditValueChanged(object sender, EventArgs e)
        {
        }

        private void txtSOTHE_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtMSCS_KeyPress(object sender, KeyPressEventArgs e)
        {


        }

        private void TabTTBN_Click(object sender, EventArgs e)
        {
            /*
            var que = (from DS in DaTaContext.BenhNhans
                       join KB in DaTaContext.BNKBs on DS.MSBN equals KB.MSBN
                       select new { DS.TENBN, DS.TUOI, DS.DTNHA, DS.STHE, DS.BPHAN, DS.DCHI, DS.DTHOAI, DS.GTINH, DS.IDBN, DS.MSBN, DS.MSBP, DS.NSINH, DS.TCHUNG, DS.TUYEN }).OrderByDescending(p => p.IDBN);
            */
            // lay benh nhan chua kham benh
            //var que = (from DS in DaTaContext.BenhNhans
            //           where !(from KB in DaTaContext.BNKBs select KB.MaBNhan).Contains(DS.MaBNhan)
            //           select new { DS.TenBNhan, DS.Tuoi, DS.DTNThan, DS.SThe, DS.MaKP, DS.DChi, DS.DThoai, DS.GTinh, DS.MaBNhan, DS.MaBNhan, DS.NSinh, DS.TChung, DS.Tuyen }).OrderByDescending(p => p.MaBNhan);
            //grcBNhankb.DataSource = que.ToList();
            //var q = from KhoaKham in DaTaContext.KPhongs.Where(p => p.PLoai== ("Lâm sàng")).Where(p => p.Status ==1).OrderBy(p => p.TenKP) select KhoaKham;
            //if (q.Count() > 0)
            //{
            //    lupTimMaKPkb.Properties.DataSource = q.ToList();
            //}
            //var que = (from DS in DaTaContext.BenhNhans
            //           select new { DS.TenBNhan, DS.Tuoi, DS.SThe, DS.MaKP, DS.DChi, DS.GTinh, DS.MaBNhan, DS.MaBNhan, DS.TChung, DS.Tuyen }).Where(p => p.MaKP== (MaKPkb)).OrderByDescending(p => p.MaBNhan).ToList();
            //if(que.Count>0)
            //grcBNhankb.DataSource = que.ToList();

        }



        private void xtraKhamBenh_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Test");
            //var que = (from DS in DaTaContext.BenhNhans
            //           where !(from KB in DaTaContext.BNKBs select KB.MaBNhan).Contains(DS.MaBNhan)
            //           select new { DS.TenBNhan, DS.Tuoi, DS.DTNThan, DS.SThe, DS.MaKP, DS.DChi, DS.DThoai, DS.GTinh, DS.MaBNhan, DS.MaBNhan, DS.NSinh, DS.TChung, DS.Tuyen }).OrderByDescending(p => p.MaBNhan);

        }

        private void btnDonThuoc_Click(object sender, EventArgs e)
        {
            frmDonNgoaiTru frm = new frmDonNgoaiTru();
            frm.ShowDialog();
        }

        private void btnChiDinh_Click(object sender, EventArgs e)
        {
            //frmChiDinh frm = new frmChiDinh();
            //frm.ShowDialog();
        }

        private void simpleButton9_Click(object sender, EventArgs e)
        {
            frmDonNoiTru frm = new frmDonNoiTru();
            frm.ShowDialog();
        }




        private void simpleButton2_Click(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities DataContect = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            BNKB KBenh = new BNKB();
        }

        private void grvDSBN_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (grvDSBN.GetFocusedRowCellValue(colTenBNhan) != null)
            {
                txtTENBN.Text = grvDSBN.GetFocusedRowCellValue(colTenBNhan).ToString();
                if (grvDSBN.GetFocusedRowCellValue(colMaBNhan) != null)
                    txtMaBNhan.Text = grvDSBN.GetFocusedRowCellValue(colMaBNhan).ToString();
                if (grvDSBN.GetFocusedRowCellValue(colTuoi) != null)
                {
                    txtTuoi.Text = grvDSBN.GetFocusedRowCellValue(colTuoi).ToString();
                }
                if (grvDSBN.GetFocusedRowCellValue(colMaBNhan) != null)
                    txtIDBN.Text = grvDSBN.GetFocusedRowCellValue(colMaBNhan).ToString();
            }
        }

        private void grvDSBN_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (grvDSBN.GetRowCellValue(e.RowHandle, colStatus) != null && grvDSBN.GetRowCellValue(e.RowHandle, colStatus).ToString() == "1")
            {
                e.Appearance.ForeColor = Color.DarkRed;
            }
        }
        public static void _InGiayDK(QLBV_Database.QLBVEntities Data, int _MB)
        {

            var b = (from bn in Data.BenhNhans.Where(p => p.MaBNhan == _MB)
                     join kp in Data.KPhongs on bn.MaKP equals kp.MaKP
                     select new  { bn.DTuong, bn.MaBNhan, bn.TenBNhan, bn.Tuoi, bn.SThe, bn.SoTT, bn.UuTien, kp.TenKP, bn.NNhap, bn.PLKham, bn.NoiTru, kp.DChi }).ToList();
            if (b.Count > 0)
            {
                if (DungChung.Bien.MaBV == "30007")
                {
                    frmIn frm = new frmIn();
                    QLBV.Phieu.rep_PhieuKB_30007 rep = new QLBV.Phieu.rep_PhieuKB_30007();
                    if (b.First().SoTT.Value <= 9)
                    {

                        rep.So.Value = "STT:  0" + b.First().SoTT.Value;
                    }
                    else
                    {
                        rep.So.Value = "STT:  " + b.First().SoTT.Value;
                    }

                    rep.HoTenBN.Value = b.First().TenBNhan.ToUpper();
                    rep.MaBenhNhan.Value = b.First().MaBNhan.ToString();
                    rep.PhongKham.Value = b.First().TenKP.ToUpper();
                    rep.ThoiGianIn.Value = DungChung.Ham.NgaySangChu(System.DateTime.Now, 10);
                    rep.DiaChi.Value = "Đ/c: " + b.First().DChi;
                    rep.ShowPrintMarginsWarning = false;
                    rep.CreateDocument();
                    if ((DungChung.Bien.MaBV == "30007") && b.First().NoiTru == 0)
                    {
                        DevExpress.XtraReports.UI.ReportPrintTool rpt = new DevExpress.XtraReports.UI.ReportPrintTool(rep);
                        rpt.Print();
                    }
                    else
                    {
                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                        frm.ShowDialog();
                    }

                }
                else
                {
                    BaoCao.Rep_PhieuKB rep = new BaoCao.Rep_PhieuKB();
                    frmIn frm = new frmIn();
                    if (b.First().UuTien == true)
                    {
                        rep.UT.Value = "(ƯU TIÊN)";
                    }
                    else
                    { rep.UT.Value = ""; }
                    rep.STT.Value = b.First().SoTT;
                    rep.PK.Value = b.First().TenKP.ToUpper();
                    rep.HT.Value = b.First().TenBNhan.ToUpper();
                    rep.MaBN.Value = b.First().MaBNhan.ToString().ToUpper();
                    rep.NamSinh.Value = b.First().Tuoi;
                    string _dt = "";
                    _dt = b.First().DTuong;
                    if (b.First().DTuong == "Dịch vụ" && DungChung.Bien.MaBV != "01830")
                    {
                        if (DungChung.Bien.MaBV == "26007")
                            _dt += "  Giá: 31.000đ";
                        else if (DungChung.Bien.MaBV != "24009" && DungChung.Bien.MaBV != "34019")
                            _dt += "  Giá: 11.000đ";

                    }

                    if (DungChung.Bien.MaBV == "24009")
                    {
                        rep.xrLabel15.Visible = false;
                        rep.xrLabel16.Visible = false;
                    }

                    if (b.First().PLKham == 0)
                        rep.LoaiKham.Value = "Thường";
                    if (b.First().PLKham == 2)
                        rep.LoaiKham.Value = "Yêu cầu";
                    if (b.First().PLKham == 1)
                        rep.LoaiKham.Value = "Tư vấn";

                    rep.DTuong.Value = _dt;
                    DateTime Ngay = Convert.ToDateTime(b.First().NNhap);
                    rep.Ngaygio.Value = DungChung.Ham.NgaySangChu(Ngay, 3);
                    rep.ShowPrintMarginsWarning = false;
                    rep.CreateDocument();
                    //rep.DataMember = "Table";
                    if ((DungChung.Bien.MaBV == "34019" || DungChung.Bien.MaBV == "30007") && b.First().NoiTru == 0)
                    {
                        DevExpress.XtraReports.UI.ReportPrintTool rpt = new DevExpress.XtraReports.UI.ReportPrintTool(rep);
                        rpt.Print();
                    }
                    else
                    {
                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                        frm.ShowDialog();
                    }
                }

            }

        }
        #region inphieu30009
        void inphieu30009(int mabn)
        {

            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            frmIn frm = new frmIn();
            BaoCao.repPhieuDangKyKB_TH rep = new BaoCao.repPhieuDangKyKB_TH();

            var par = (from bn in data.BenhNhans
                       join kp in data.KPhongs on bn.MaKP equals kp.MaKP
                       where (bn.MaBNhan == mabn)
                       select new { bn.Tuyen, bn.MaCS, bn.TenBNhan, bn.SoTT, bn.MaBNhan, bn.NoiTinh, bn.Tuoi, bn.DTuong, bn.HanBHTu, bn.HanBHDen, bn.NgaySinh, bn.ThangSinh, bn.NamSinh, bn.SThe, bn.DChi, kp.TenKP, bn.NNhap, bn.TChung }).ToList();
            if (par.Count > 0)
            {
                string _mabv = "";
                if (par.First().MaCS != null)
                    _mabv = par.First().MaCS;
                var ten = data.BenhViens.Where(p => p.MaBV == (_mabv)).Select(p => p.TenBV).ToList();
                if (ten.Count > 0)
                    _mabv += " - " + ten.First();

                string _namsinh = "";
                if (par.First().NgaySinh != null && par.First().NgaySinh.ToString() != "")
                    _namsinh += par.First().NgaySinh.ToString();
                else
                    _namsinh += "...";
                if (par.First().ThangSinh != null && par.First().ThangSinh.ToString() != "")
                    _namsinh += " / " + par.First().ThangSinh.ToString();
                else
                    _namsinh += " / ...";
                if (par.First().NamSinh != null && par.First().NamSinh.ToString() != "")
                    _namsinh += " / " + par.First().NamSinh.ToString();
                else
                    _namsinh += " / ...";
                if (par.First().Tuyen != null && par.First().DTuong == "BHYT")
                {
                    if (par.First().Tuyen == 1)
                        rep.Tuyen.Value = "Đúng|Trái tuyến: Đúng tuyến";
                    else
                        rep.Tuyen.Value = "Đúng|Trái tuyến: Trái tuyến";
                }
                if (par.First().NoiTinh == 1)
                {
                    rep.NoiNgoaiTinh.Value = "BN nội tỉnh KCB ban đầu";
                }
                else if (par.First().NoiTinh == 2)
                {
                    rep.NoiNgoaiTinh.Value = "BN nội tỉnh đến";
                }
                else if (par.First().NoiTinh == 3)
                {
                    rep.NoiNgoaiTinh.Value = "BN ngoại tỉnh đến";
                }
                rep.NgaySinh.Value = _namsinh;
                rep.MaCSDK.Value = _mabv;
                rep.TenKP.Value = par.First().TenKP;
                rep.TenBNhan.Value = par.First().TenBNhan.ToUpper();
                rep.MaBNhan.Value = par.First().MaBNhan;
                rep.Tuoi.Value = par.First().Tuoi;
                rep.DTuong.Value = par.First().DTuong;
                rep.SThe.Value = par.First().SThe.ToString();
                rep.SoTT.Value = par.First().SoTT;
                rep.DChi.Value = par.First().DChi;
                rep.TChung.Value = par.First().TChung;
                rep.NgayTu.Value = par.First().HanBHTu;
                rep.NgayDen.Value = par.First().HanBHDen;
                if (par.First().NNhap != null)
                {
                    rep.Ngaythang.Value = DungChung.Ham.NgaySangChu(par.First().NNhap.Value, DungChung.Bien.FormatDate);
                }
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
            }

        }
        #endregion
        #region phieutonghop
        void InPhieuChung(int mbn)
        {
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            frmIn frm = new frmIn();
            BaoCao.repPhieuDangKyKB rep = new BaoCao.repPhieuDangKyKB();

            var par = (from bn in data.BenhNhans
                       join kp in data.KPhongs on bn.MaKP equals kp.MaKP
                       where (bn.MaBNhan == mbn)
                       select new { bn.MaCB, bn.MaCS, bn.TenBNhan, bn.SoTT, bn.MaBNhan, bn.Tuoi, bn.DTuong, bn.HanBHTu, bn.HanBHDen, bn.NgaySinh, bn.ThangSinh, bn.NamSinh, bn.SThe, bn.DChi, kp.TenKP, bn.NNhap, bn.TChung }).ToList();
            if (par.Count > 0)
            {
                string _mabv = "";
                if (par.First().MaCS != null)
                    _mabv = par.First().MaCS;
                var ten = data.BenhViens.Where(p => p.MaBV == (_mabv)).Select(p => p.TenBV).ToList();
                if (ten.Count > 0)
                    _mabv += " - " + ten.First();
                string _namsinh = "";
                if (par.First().NgaySinh != null && par.First().NgaySinh.ToString() != "")
                    _namsinh += par.First().NgaySinh.ToString();
                else
                    _namsinh += "...";
                if (par.First().ThangSinh != null && par.First().ThangSinh.ToString() != "")
                    _namsinh += " / " + par.First().ThangSinh.ToString();
                else
                    _namsinh += " / ...";
                if (par.First().NamSinh != null && par.First().NamSinh.ToString() != "")
                    _namsinh += " / " + par.First().NamSinh.ToString();
                else
                    _namsinh += " / ...";
                rep.NgaySinh.Value = _namsinh;
                rep.MaCSDK.Value = _mabv;
                rep.TenKP.Value = par.First().TenKP;
                rep.TenBNhan.Value = par.First().TenBNhan.ToUpper();
                rep.MaBNhan.Value = par.First().MaBNhan;
                if (DungChung.Bien.MaBV == "24012")
                {
                    rep.Tuoi.Value = DungChung.Ham.TuoitheoThang(data, mbn, DungChung.Bien.formatAge_24012);
                }
                else
                    rep.Tuoi.Value = par.First().Tuoi;
                rep.DTuong.Value = par.First().DTuong;
                rep.SThe.Value = par.First().SThe.ToString();
                rep.SoTT.Value = par.First().SoTT;
                rep.DChi.Value = par.First().DChi;
                string macb = par.First().MaCB;
                rep.colCBTNhan.Text = data.CanBoes.Where(x => x.MaCB == macb).Select(x => x.TenCB).FirstOrDefault();
                rep.TChung.Value = par.First().TChung;
                rep.NgayTu.Value = par.First().HanBHTu;
                rep.NgayDen.Value = par.First().HanBHDen;
                if (par.First().NNhap != null)
                {
                    rep.Ngaythang.Value = DungChung.Ham.NgaySangChu(par.First().NNhap.Value, 2);
                }
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
            }
        }
        #endregion
        #region phieukhamthaibinh
        public static void InPhieuGiuThe_TB(int mabn)
        {
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var qbn = (from bn in data.BenhNhans.Where(p => p.MaBNhan == mabn)
                       join bv in data.BenhViens on bn.MaKCB equals bv.MaBV
                       join kp in data.KPhongs on bn.MaKP equals kp.MaKP
                       select new { bn.SoTT, bn.NNhap, bn.TenBNhan, bn.NgaySinh, bn.ThangSinh, bn.NamSinh, bn.GTinh, bn.DChi, bn.SThe, bn.HanBHDen, bn.HanBHTu, bn.MaCS, bv.TenBV, kp.TenKP, DiaChiPK = kp.DChi, bn.TChung }).ToList();
            if (qbn.Count > 0)
            {
                BaoCao.rep_GiayGiuThe rep = new BaoCao.rep_GiayGiuThe();
                //var SoDK = data.SoDKKBs.Where(p => p.MaBNhan == mabn).Select(p => p.SoDK).FirstOrDefault();
                //if (SoDK != null)
                //   rep.SoTT.Value = SoDK;
                rep.SoTT.Value = qbn.First().SoTT.Value.ToString("d3");
                rep.HoTen.Value = qbn.First().TenBNhan;
                rep.mabn.Value = mabn.ToString();
                rep.lydokham.Value = qbn.First().TChung;
                rep.NgaySinh.Value = qbn.First().NgaySinh + "/" + qbn.First().ThangSinh + "/" + qbn.First().NamSinh;
                rep.GioiTinh.Value = qbn.First().GTinh == 0 ? "Nữ" : "Nam";
                rep.DiaChi.Value = qbn.First().DChi;
                rep.BHYT1.Value = (qbn.First().SThe.Trim() == "" || qbn.First().SThe == null) ? "" : qbn.First().SThe.Substring(0, 2);
                rep.BHYT2.Value = (qbn.First().SThe.Trim() == "" || qbn.First().SThe == null) ? "" : qbn.First().SThe.Substring(2, 1);
                rep.BHYT3.Value = (qbn.First().SThe.Trim() == "" || qbn.First().SThe == null) ? "" : qbn.First().SThe.Substring(3, 2);
                rep.BHYT4.Value = (qbn.First().SThe.Trim() == "" || qbn.First().SThe == null) ? "" : qbn.First().SThe.Substring(5, 2);
                rep.BHYT5.Value = (qbn.First().SThe.Trim() == "" || qbn.First().SThe == null) ? "" : qbn.First().SThe.Substring(7, 3);
                rep.BHYT6.Value = (qbn.First().SThe.Trim() == "" || qbn.First().SThe == null) ? "" : qbn.First().SThe.Substring(10, 5);
                rep.HanBHTu.Value = String.Format("{0:dd/MM/yyyy}", qbn.First().HanBHTu);
                rep.HanBHDen.Value = String.Format("{0:dd/MM/yyyy}", qbn.First().HanBHDen);
                rep.TenCSKCB.Value = qbn.First().TenBV;
                rep.MaCSKCB.Value = qbn.First().MaCS;
                rep.PhongKham.Value = qbn.First().DiaChiPK + " - " + qbn.First().TenKP;

                frmIn frm = new frmIn();
                rep.lblNgayThangNam.Text = (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "56789") ? ("Ngày " + qbn.First().NNhap.Value.Date + " tháng " + qbn.First().NNhap.Value.Month + " năm " + qbn.First().NNhap.Value.Year) : ("Ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year);
                //rep.DataSource = qcls.ToList();
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
            }
        }
        #endregion
        private void btnInPhieu_Click(object sender, EventArgs e)
        {//
            int rs;
            int mbn = 0;
            if (Int32.TryParse(txtMaBNhan.Text, out rs))
                mbn = Convert.ToInt32(txtMaBNhan.Text);

            if (DungChung.Bien.MaBV == "30009")
            {

                inphieu30009(mbn);

            }
            else
            {
                if (DungChung.Bien.MaBV == "24009" || DungChung.Bien.MaBV == "26007" || DungChung.Bien.MaBV == "01830" || DungChung.Bien.MaBV == "34019")
                {
                    QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                    _InGiayDK(data, mbn);
                }
                else
                {
                    InPhieuChung(mbn);
                }

            }

            //
        }

        private void grcDSBN_Click(object sender, EventArgs e)
        {

        }

        private void grvDSBN_DoubleClick(object sender, EventArgs e)
        {

            if (grvDSBN.GetFocusedRowCellValue(colMaBNhan) != null)
            {
                int _mabn = 0;
                _mabn = Convert.ToInt32(grvDSBN.GetFocusedRowCellValue(colMaBNhan));
                ChucNang.frm_TTKCBenh frm = new ChucNang.frm_TTKCBenh(_mabn);
                frm.ShowDialog();
            }
            //MessageBox.Show(grvDSBN.GetFocusedRowCellValue(colTenBNhan).ToString());
        }
        List<BenhNhan> _lTKbn = new List<BenhNhan>();
        void timkiem2()
        {

            string _tk = "";
            int _int_maBN = 0;
            List<int> _lmabnbx = new List<int>();
            if (!string.IsNullOrEmpty(txtTkTenBN.Text))
            {
                _tk = txtTkTenBN.Text.ToLower();
                int rs;

                if (Int32.TryParse(txtTkTenBN.Text, out rs))
                    _int_maBN = Convert.ToInt32(txtTkTenBN.Text);
                List<int> lmabn = _lTKbn.Select(p => p.MaBNhan).ToList();
                _lmabnbx = DaTaContext.TTboXungs.Where(p => lmabn.Contains(p.MaBNhan)).Where(p => p.DThoai != null && p.DThoai.Contains(_tk)).Select(p => p.MaBNhan).ToList();
            }
            grcDSBN.DataSource = _lTKbn.Where(p => p.TenBNhan.ToLower().Contains(_tk) || p.MaBNhan == _int_maBN || _lmabnbx.Contains(p.MaBNhan) || p.IDPerson == _int_maBN).OrderByDescending(o => o.NNhap).ToList();
        }

        private void TimKiem()
        {
            formLoad = false;
            _lTKbn.Clear();
            _dttu = DungChung.Ham.NgayTu(dtTuNgay.DateTime);
            _dtden = DungChung.Ham.NgayDen(dtDenNgay.DateTime);
            string _timten = "";
            int _makp = 0;
            int noingoaitru = cboNoiNgoaiTru.SelectedIndex;
            int iddtbn = 99;
            if (cboDTuong.EditValue != null)
                iddtbn = Convert.ToInt32(cboDTuong.EditValue);
            int _int_maBN = 0;
            List<int> _lmabnbx = new List<int>();
            if (lupMaKP.EditValue != null)
            {

                _makp = Convert.ToInt32(lupMaKP.EditValue);
            }
            if (!string.IsNullOrEmpty(txtTkTenBN.Text))
            {
                _timten = txtTkTenBN.Text.ToLower();
                int rs;
                if (Int32.TryParse(txtTkTenBN.Text, out rs))
                    _int_maBN = Convert.ToInt32(txtTkTenBN.Text);
            }

            if (DungChung.Bien.MaBV == "20001")
            {
                var qbn = (from bn in DaTaContext.BenhNhans.Where(p => p.NNhap >= _dttu && p.NNhap <= _dtden)
                           join kb in DaTaContext.BNKBs on bn.MaBNhan equals kb.MaBNhan into kq
                           from kq1 in kq.DefaultIfEmpty()
                           where (bn.MaKCB == DungChung.Bien.MaBV)
                           select new { MaBNhan = bn.MaBNhan, TenBNhan = bn.TenBNhan, Tuoi = bn.Tuoi, GTinh = bn.GTinh, DChi = bn.DChi, MaDTuong = bn.DTuong, NoiTru = bn.NoiTru, NNhap = bn.NNhap, TChung = bn.TChung, Tuyen = bn.Tuyen, CDNoiGT = bn.CDNoiGT, CapCuu = bn.CapCuu, SThe = bn.SThe, DTuong = bn.DTuong, NgoaiGio = bn.NgoaiGio, MaKP = kq1 == null ? bn.MaKP : kq1.MaKP, Status = kq1 == null ? 0 : 1, SoKB = bn.SoKB, DTNT = bn.DTNT, NoThe = bn.NoThe, IDDTBN = bn.IDDTBN, MaKCB = bn.MaKCB, Export = bn.Export, SoDK = bn.SoDK, NgaySinh = bn.NgaySinh, ThangSinh = bn.ThangSinh, NamSinh = bn.NamSinh, IDPerson = bn.IDPerson }).ToList();
                var _Dem0 = (from bn in qbn
                             group new { bn } by new { bn.MaKP, bn.MaBNhan, bn.Status, bn.TenBNhan, bn.SThe, bn.DTuong, bn.NgoaiGio, DTNT = bn.DTNT, NoThe = bn.NoThe, IDDTBN = bn.IDDTBN, MaKCB = bn.MaKCB, Export = bn.Export, SoDK = bn.SoDK, bn.SoKB, Tuoi = bn.Tuoi, GTinh = bn.GTinh, DChi = bn.DChi, MaDTuong = bn.DTuong, NoiTru = bn.NoiTru, NNhap = bn.NNhap, TChung = bn.TChung, Tuyen = bn.Tuyen, CDNoiGT = bn.CDNoiGT, CapCuu = bn.CapCuu, bn.NgaySinh, bn.ThangSinh, bn.NamSinh, bn.IDPerson } into kq
                             select new { MaKP = kq.Key.MaKP, MaBNhan = kq.Key.MaBNhan, Status = kq.Key.Status, TenBNhan = kq.Key.TenBNhan, SThe = kq.Key.SThe, DTuong = kq.Key.DTuong, NgoaiGio = kq.Key.NgoaiGio, DTNT = kq.Key.DTNT, NoThe = kq.Key.NoThe, SoKB = kq.Key.SoKB, IDDTBN = kq.Key.IDDTBN, MaKCB = kq.Key.MaKCB, Export = kq.Key.Export, SoDK = kq.Key.SoDK, Tuoi = kq.Key.Tuoi, GTinh = kq.Key.GTinh, DChi = kq.Key.DChi, MaDTuong = kq.Key.DTuong, NoiTru = kq.Key.NoiTru, NNhap = kq.Key.NNhap, TChung = kq.Key.TChung, Tuyen = kq.Key.Tuyen, CDNoiGT = kq.Key.CDNoiGT, CapCuu = kq.Key.CapCuu, NgaySinh = kq.Key.NgaySinh, ThangSinh = kq.Key.ThangSinh, NamSinh = kq.Key.NamSinh, IDPerson = kq.Key.IDPerson }).ToList();
                var _Dem = (from bn in _Dem0
                            group new { bn } by new { bn.MaKP } into kq
                            select new { kq.Key.MaKP, SoLuong = kq.Select(p => p.bn.MaBNhan).Count(), ChuaKham = kq.Where(p => p.bn.Status == 0).Select(p => p.bn.MaBNhan).Count(), DaKham = kq.Where(p => p.bn.Status == 1).Select(p => p.bn.MaBNhan).Count() }).ToList();
                grcKhoaPhong.DataSource = _Dem;

                var qbc = (from bn in _Dem0
                           where (_timten == "" || bn.MaBNhan == _int_maBN || bn.TenBNhan.ToLower().Contains(_timten) || bn.SThe.ToLower().Contains(_timten) || bn.IDPerson == _int_maBN)
                           where (_makp == 0 || bn.MaKP == _makp)
                           where chkNgoaiGio.Checked ? bn.NgoaiGio == 1 : true
                           where chkKSK.Checked ? bn.DTuong == "KSK" : bn.DTuong != "KSK"
                           where cboDaKham.SelectedIndex == 2 ? true : (cboDaKham.SelectedIndex == 0 ? bn.Status == 0 : bn.Status > 0)
                           where noingoaitru == 2 ? true : bn.NoiTru == noingoaitru
                           where iddtbn == 99 ? true : bn.IDDTBN == iddtbn

                           select bn).OrderByDescending(p => p.MaBNhan).ToList();
                grcDSBN.DataSource = qbc.ToList();
            }
            else
            {
                _lTKbn = (from bn in DaTaContext.BenhNhans
                          where (bn.MaKCB == DungChung.Bien.MaBV)
                          where (bn.NNhap >= _dttu && bn.NNhap <= _dtden)
                          select bn).ToList();
                List<int> lmabn = _lTKbn.Select(p => p.MaBNhan).ToList();
                if (DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297")
                    _lmabnbx = DaTaContext.TTboXungs.Where(p => lmabn.Contains(p.MaBNhan)).Where(p => p.DThoai != null && p.DThoai.Contains(_timten)).Select(p => p.MaBNhan).ToList();
                var _Dem = (from bn in _lTKbn
                            group new { bn } by new { bn.MaKP } into kq
                            select new { kq.Key.MaKP, SoLuong = kq.Select(p => p.bn.MaBNhan).Count(), ChuaKham = kq.Where(p => p.bn.Status == 0 || p.bn.Status == null).Select(p => p.bn.MaBNhan).Count(), DaKham = kq.Where(p => p.bn.Status > 0).Select(p => p.bn.MaBNhan).Count() }).ToList();
                grcKhoaPhong.DataSource = _Dem;

                _lTKbn = (from bn in _lTKbn
                          where (_timten == "" || bn.MaBNhan == _int_maBN || (bn.TenBNhan != null && bn.TenBNhan.ToLower().Contains(_timten)) || (bn.SThe == null || bn.SThe.ToLower().Contains(_timten)) || _lmabnbx.Contains(bn.MaBNhan) || bn.IDPerson == _int_maBN)
                          where (_makp == 0 || bn.MaKP == _makp)
                          where chkNgoaiGio.Checked ? bn.NgoaiGio == 1 : true
                          where chkKSK.Checked ? bn.DTuong == "KSK" : bn.DTuong != "KSK"
                          where cboDaKham.SelectedIndex == 2 ? true : (cboDaKham.SelectedIndex == 0 ? bn.Status == 0 : bn.Status > 0)
                          where noingoaitru == 2 ? true : bn.NoiTru == noingoaitru
                          where iddtbn == 99 ? true : bn.IDDTBN == iddtbn
                          select bn).OrderByDescending(p => p.MaBNhan).ToList();

                if (DungChung.Bien.MaBV == "30372" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297")
                {
                    var _ttbx = (from bx in DaTaContext.TTboXungs select bx).ToList();

                    var _getlistTTBN = _lTKbn.GroupJoin(_ttbx, p => p.MaBNhan, c => c.MaBNhan, (p, bx) => new { p, bx = bx }).SelectMany(k => k.bx.DefaultIfEmpty(), (k, t) => new
                    {
                        DThoai = (t == null) ? "<null>" : t.DThoai,
                        k.p.CapCuu,
                        k.p.CDNoiGT,
                        k.p.ChuyenKhoa,
                        k.p.DChi,
                        k.p.DTNT,
                        k.p.DTuong,
                        k.p.GTinh,
                        k.p.HanBHDen,
                        k.p.HanBHTu,
                        k.p.IDDTBN,
                        k.p.IDPerson,
                        k.p.KhuVuc,
                        k.p.LuongCS,
                        k.p.Ma_lk,
                        k.p.MaBNhan,
                        k.p.MaBV,
                        k.p.MaCB,
                        k.p.MaCS,
                        k.p.MaDTuong,
                        k.p.MaKCB,
                        k.p.MaKP,
                        k.p.MaKPDTKH,
                        k.p.MucHuong,
                        k.p.NamSinh,
                        k.p.NgayHM,
                        k.p.NgaySinh,
                        k.p.NgoaiGio,
                        k.p.NNhap,
                        k.p.NoiTinh,
                        k.p.NoiTru,
                        k.p.Normal,
                        k.p.NoThe,
                        k.p.PID,
                        k.p.PLKham,
                        k.p.SoDK,
                        k.p.SoHSBA,
                        k.p.SoKB,
                        k.p.SoTT,
                        k.p.Status,
                        k.p.TChung,
                        k.p.SThe,
                        k.p.TenBNhan,
                        k.p.ThangSinh,
                        k.p.Tuoi,
                        k.p.Tuyen,
                        k.p.TuyenDuoi,
                        k.p.UuTien,
                    }).ToList();

                    //grcDSBN.DataSource = _lTKbn.ToList();
                    grcDSBN.DataSource = _getlistTTBN.ToList();
                }
                else
                    grcDSBN.DataSource = _lTKbn.ToList();
            }
        }
        private void txtTkTenBN_Leave(object sender, EventArgs e)
        {
            //TimKiem();
        }

        private void dtTuNgay_Leave(object sender, EventArgs e)
        {

        }

        private void dtDenNgay_Leave(object sender, EventArgs e)
        {
            //TimKiem();
        }

        private void cboDaKham_Leave(object sender, EventArgs e)
        {
            //txtTkTenBN.Focus();
        }


        private void simpleButton10_Click(object sender, EventArgs e)
        {

        }

        private void cboDaKham_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!formLoad)
                TimKiem();
        }

        private void grvDSBN_DataSourceChanged(object sender, EventArgs e)
        {
            if (grvDSBN.GetFocusedRowCellValue(colTenBNhan) != null)
            {
                txtTENBN.Text = grvDSBN.GetFocusedRowCellValue(colTenBNhan).ToString();
                if (grvDSBN.GetFocusedRowCellValue(colMaBNhan) != null)
                    txtMaBNhan.Text = grvDSBN.GetFocusedRowCellValue(colMaBNhan).ToString();
                if (grvDSBN.GetFocusedRowCellValue(colTuoi) != null)
                {
                    txtTuoi.Text = grvDSBN.GetFocusedRowCellValue(colTuoi).ToString();
                }
                if (grvDSBN.GetFocusedRowCellValue(colMaBNhan) != null)
                    txtIDBN.Text = grvDSBN.GetFocusedRowCellValue(colMaBNhan).ToString();
            }
        }
        int selecttab = 0;
        private void TabHSBNhan_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (TabHSBNhan.SelectedTabPage == xtraDieuTri)
            {
                selecttab = 1;
                dt_ngayDen.DateTime = System.DateTime.Now;
                dt_ngayTu.DateTime = System.DateTime.Now.AddDays(-10);
                //var tk = from bn in DaTaContext.BenhNhans
                //         join kp in DaTaContext.KPhongs on bn.MaKP equals kp.MaKP
                //         group bn by new { bn.MaKP, kp.TenKP } into kq
                //         select new { TenKP = kq.Key.TenKP, TongSo = kq.Where(p => p.MaBNhan == 0).Count() };
            }
            else
                selecttab = 0;
        }

        private void lupMaKP_EditValueChanged(object sender, EventArgs e)
        {
            if (!formLoad)
                TimKiem();
        }

        private void chkNgoaiGio_CheckedChanged(object sender, EventArgs e)
        {
            if (!formLoad)
                TimKiem();
        }

        private void chkKSK_CheckedChanged(object sender, EventArgs e)
        {
            if (!formLoad)
                TimKiem();
        }

        private void simpleButton1_Click_1(object sender, EventArgs e)
        {

            ChucNang.frm_CheckIn frm = new ChucNang.frm_CheckIn();
            frm.ShowDialog();
        }
        private void _bieuDo()
        {
            chartControl1.DataSource = null;
            DateTime _dttu1 = DungChung.Ham.NgayTu(dt_ngayTu.DateTime);
            DateTime _dtden1 = DungChung.Ham.NgayDen(dt_ngayDen.DateTime);
            var b = (from bn in DaTaContext.BenhNhans
                     where (bn.NNhap >= _dttu1 && bn.NNhap <= _dtden1 && bn.NNhap != null)
                     select bn).ToList();
            var a = (from bn in b
                     group new { bn } by new { Date = bn.NNhap.Value.Date }
                         into kq
                     select new
                     {
                         kq.Key.Date,
                         SL = kq.Select(p => p.bn.MaBNhan).Count(),
                         SLVV = kq.Where(p => p.bn.NoiTru == 1).Select(p => p.bn.MaBNhan).Count(),
                     }).ToList();
            chartControl1.DataSource = a;
        }
        private void dt_ngayTu_EditValueChanged(object sender, EventArgs e)
        {
            if (selecttab == 1)
                _bieuDo();
        }

        private void dt_ngayDen_EditValueChanged(object sender, EventArgs e)
        {
            if (selecttab == 1)
                _bieuDo();
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int sodk = 0;
            if (grvSoDK.RowCount > 0 && grvSoDK.GetFocusedRowCellValue(colSoDKKB) != null)
                sodk = Convert.ToInt32(grvSoDK.GetFocusedRowCellValue(colSoDKKB));
            frmHSBNNhapMoi frm = new frmHSBNNhapMoi(sodk);
            frm.FormClosed += new FormClosedEventHandler(this.frmHSBN_Load);
            frm.ShowDialog();

            //enableControl(false);
            Luu = 1;
        }

        private void barSuaBN_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtMaBNhan.Text))
            {
                int _mabn = Convert.ToInt32(txtMaBNhan.Text);
                //if (DungChung.Bien.MaBV == "01830")
                //{
                //    QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                //    if (DungChung.Ham.KTraCBNhap(_data, _mabn, DungChung.Bien.MaCB))
                //    {
                //        frmHSBNNhapMoi frm = new frmHSBNNhapMoi(2, _mabn);
                //        frm.FormClosed += new FormClosedEventHandler(this.frmHSBN_Load);
                //        frm.ShowDialog();
                //    }
                //}
                //else
                //{
                frmHSBNNhapMoi frm = new frmHSBNNhapMoi(2, _mabn);
                frm.FormClosed += new FormClosedEventHandler(this.frmHSBN_Load);
                frm.ShowDialog();

                //}
            }
            else
            {
                MessageBox.Show("Bạn chưa chọn bệnh nhân!");
            }
        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int rs;
            int mbn = 0;
            if (Int32.TryParse(txtMaBNhan.Text, out rs))
                mbn = Convert.ToInt32(txtMaBNhan.Text);
            InPhieuChung(mbn);
        }

        private void barPKCB_tb_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int rs;
            int mbn = 0;
            if (Int32.TryParse(txtMaBNhan.Text, out rs))
                mbn = Convert.ToInt32(txtMaBNhan.Text);
            InPhieuGiuThe_TB(mbn);
        }

        private void mnInPhieuKCB_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }
        Library_LED.LEDCommunication ComPort;
        string[] strPort = new string[10];
        string Port = "";
        int mabn = 0;
        void getvalue_deleg(int mabn)
        {
            this.mabn = mabn;
        }
        private void grvSoDK_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {

            if (e.Column.Name == "colSoDKKB" || e.Column.Name == "col_mabN")
            {
                int sodk = 0;
                if (grvSoDK.GetFocusedRowCellValue(colSoDKKB) != null)
                    sodk = Convert.ToInt32(grvSoDK.GetFocusedRowCellValue(colSoDKKB));
                getSoKB.ComPort(sodk);
                mabn = 0;
                QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                DateTime dt = dtNgayDK.DateTime;
                if (sodk > 0)
                {

                    var updateSoDK = _data.SoDKKBs.Where(p => p.SoDK == sodk && p.NgayDK == dt.Date).ToList();
                    foreach (var item in updateSoDK)
                    {

                        if (item.MaBNhan == null || item.MaBNhan == 0)
                        {
                            int sophong = 0;
                            if (ConfigurationManager.AppSettings["phongdoc"] != null)
                                sophong = Convert.ToInt32(ConfigurationManager.AppSettings["phongdoc"]);
                            item.Status = 1;
                            item.ThoiGian = DateTime.Now;
                            item.Phong = sophong;
                            _data.SaveChanges();
                            frmHSBNNhapMoi frm = new frmHSBNNhapMoi(sodk);
                            frm.getdata = new frmHSBNNhapMoi.getString(getvalue_deleg);
                            frm.FormClosed += new FormClosedEventHandler(this.frmHSBN_Load);
                            frm.ShowDialog();
                            item.MaBNhan = mabn;
                            _data.SaveChanges();
                            TimKiemSoDK();
                        }
                        else
                        {
                            MessageBox.Show("Sửa thông tin bệnh nhân");
                            frmHSBNNhapMoi frm = new frmHSBNNhapMoi(2, Convert.ToInt32(item.MaBNhan));
                            frm.ShowDialog();
                        }
                    }

                }

            }
            if (e.Column.Name == "col_mabN")
            {
                if (grvSoDK.GetFocusedRowCellValue(colMaBNhan) != null)
                {
                    frmHSBN.InPhieuGiuThe_TB(Convert.ToInt32(grvSoDK.GetFocusedRowCellValue(colMaBNhan)));
                }
            }
        }

        void TimKiemSoDK()
        {
            int status = cboTimKiemSoDK.SelectedIndex;
            QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            grcSoDK.DataSource = (from so in _data.SoDKKBs.Where(p => p.NgayDK == dtNgayDK.DateTime.Date)
                                  where status == 0 ? (so.Status == -1) : (status == 0 ? (so.MaBNhan == null || so.MaBNhan == 0) : (status == 1 ? (so.MaBNhan > 0) : true))
                                  select so).OrderBy(p => p.SoDK).ToList();
        }

        private void cboTimKiemSoDK_SelectedIndexChanged(object sender, EventArgs e)
        {
            TimKiemSoDK();
        }

        private void dtNgayDK_EditValueChanged(object sender, EventArgs e)
        {
            TimKiemSoDK();
        }
        private bool KTraXoaBN(QLBV_Database.QLBVEntities _data, int _maBN)
        {
            var KtraKB = _data.BNKBs.Where(p => p.MaBNhan == _maBN).ToList();
            var KtraCLS = _data.CLS.Where(p => p.MaBNhan == _maBN).ToList();
            var KtraDthuoc = _data.DThuocs.Where(p => p.MaBNhan == _maBN).ToList();
            var ktratamthu = _data.TamUngs.Where(p => p.MaBNhan == _maBN).ToList();
            var tk = _data.ADMINs.FirstOrDefault(o => o.TenDN == DungChung.Bien.TenDN);
            var vv = _data.VaoViens.FirstOrDefault(p => p.MaBNhan == _maBN);

            if (DungChung.Bien.MaBV == "56789" && tk != null && tk.CapDo != 9)
            {
                MessageBox.Show("Tài khoản không có quyền xóa HSBN");
                return false;
            }
            if(vv != null)
            {
                MessageBox.Show("Bệnh nhân đã vào viện, không thể xóa!");
                return false;
            }
            if (ktratamthu.Count > 0)
            {
                MessageBox.Show("Bệnh nhân " + (txtTENBN.Text.ToUpper()) + " đã có tạm thu, \nBạn không được xóa");
                return false;
            }
            if (KtraKB.Count > 0)
            {
                MessageBox.Show("Bệnh nhân " + (txtTENBN.Text.ToUpper()) + " đã được khám bệnh, \nBạn không được xóa");
                return false;
            }
            if (KtraCLS.Count > 0)
            {
                MessageBox.Show("Bệnh nhân " + (txtTENBN.Text.ToUpper()) + " đã có chỉ định CLS, \nBạn không được xóa");
                return false;
            }
            if (KtraCLS.Count > 0)
            {
                MessageBox.Show("Bệnh nhân " + (txtTENBN.Text.ToUpper()) + " đã có đơn thuốc, \nBạn không được xóa");
                return false;
            }
            //if (DungChung.Bien.MaBV == "01830" )
            //{
            //    if(DungChung.Ham.KTraCBNhap(_data, _maBN, DungChung.Bien.MaCB) == false)
            //    return false;
            //}
            return true;
        }
        private void mnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //Xóa
            if (!string.IsNullOrEmpty(txtMaBNhan.Text))
            {
                int mabn = Convert.ToInt32(txtMaBNhan.Text);
                var _xoa = DaTaContext.BNKBs.Where(p => p.MaBNhan == mabn).ToList();

                if (KTraXoaBN(DaTaContext, mabn))
                {
                    DialogResult _result;
                    _result = MessageBox.Show("Bạn muốn xóa BN: " + txtTENBN.Text + " ?", "Xóa bệnh nhân", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (_result == DialogResult.Yes)
                    {
                        //int ID = 0;
                        //if (!string.IsNullOrEmpty(txtIDBN.Text))
                        //{
                        //    ID = Int32.Parse(txtIDBN.Text);
                        //}
                        //var BN = DaTaContext.BenhNhans.Single(p => p.MaBNhan== (ID));
                        var xoaTTbx1 = DaTaContext.TTboXungs.Where(p => p.MaBNhan == mabn).ToList();
                        if (xoaTTbx1.Count > 0)
                        {
                            var xoaTTbx = DaTaContext.TTboXungs.Single(p => p.MaBNhan == mabn);
                            DaTaContext.TTboXungs.Remove(xoaTTbx);
                            DaTaContext.SaveChanges();
                        }
                        var xoaTSTC1 = DaTaContext.TienSuTiemChungs.Where(p => p.MaBNhan == mabn).ToList();
                        if (xoaTSTC1.Count > 0)
                        {
                            foreach (var item in xoaTSTC1)
                            {
                                int id = item.ID_TiemChung;
                                TienSuTiemChung tstc3 = DaTaContext.TienSuTiemChungs.Single(p => p.ID_TiemChung == id);
                                DaTaContext.TienSuTiemChungs.Remove(tstc3);
                            }
                            DaTaContext.SaveChanges();
                        } 
                        var BN = DaTaContext.BenhNhans.Single(p => p.MaBNhan == mabn);
                        int makp = 0;
                        int so = BN.SoKB;
                        if (lupMaKP.EditValue != null)
                        {
                            makp = Convert.ToInt32(lupMaKP.EditValue);
                        }
                        if (so > 0)
                        {
                            DungChung.Ham.UpdateHSHuy(mabn, makp, BN.SoKB.ToString(), 3, -1);
                        }

                        DaTaContext.BenhNhans.Remove(BN);
                        DaTaContext.SaveChanges();
                        var updateSoDK = DaTaContext.SoDKKBs.Where(p => p.MaBNhan == mabn).ToList();
                        foreach (var item in updateSoDK)
                        {
                            item.MaBNhan = 0;
                            DaTaContext.SaveChanges();
                        }

                        if (DungChung.Bien.MaBV == "30004" && BN.SThe != null && BN.SThe.Contains("TE") && BN.SThe.Contains("KT"))
                        {
                            var person = DaTaContext.People.FirstOrDefault(o => o.IDPerson == BN.IDPerson);
                            if (person != null)
                            {
                                DaTaContext.People.Remove(person);
                                DaTaContext.SaveChanges();
                            }
                        }

                        TimKiem();
                    }
                }

            }
            else
            {
                MessageBox.Show("Bạn chưa chọn BN");
            }
        }
        void showLichSuKCB(int mabn)
        {
            var bn = DaTaContext.BenhNhans.Where(p => p.MaBNhan == mabn).FirstOrDefault();

            if (bn != null && !string.IsNullOrEmpty(bn.SThe) && !string.IsNullOrEmpty(bn.TenBNhan) && !string.IsNullOrEmpty(bn.MaCS) && bn.HanBHTu != null && bn.HanBHDen != null)
            {
                GiamDinhBHXH.BHXH_Model.ApiTheBHYT2018 sthe = new GiamDinhBHXH.BHXH_Model.ApiTheBHYT2018();
                sthe.maThe = bn.SThe;
                sthe.hoTen = bn.TenBNhan;
                ////sthe.maCSKCB = bn.MaCS;
                //sthe.gioiTinh = bn.GTinh == 0 ? 2 : 1;
                sthe.ngaySinh = bn.NgaySinh.Trim() + "/" + bn.ThangSinh.Trim() + "/" + bn.NamSinh;
                //sthe.ngayBD = bn.HanBHTu.Value.ToString("dd/MM/yyyy");
                //sthe.ngayKT = bn.HanBHDen.Value.ToString("dd/MM/yyyy");
                GiamDinhBHXH.BHXH_Model.theBHYT sthecu = new GiamDinhBHXH.BHXH_Model.theBHYT();
                sthecu.maThe = bn.SThe;
                sthecu.hoTen = bn.TenBNhan;
                sthecu.maCSKCB = bn.MaCS;
                sthecu.gioiTinh = bn.GTinh == 0 ? 2 : 1;
                sthecu.ngaySinh = bn.NgaySinh.Trim() + "/" + bn.ThangSinh.Trim() + "/" + bn.NamSinh;
                sthecu.ngayBD = bn.HanBHTu.Value.ToString("dd/MM/yyyy");
                sthecu.ngayKT = bn.HanBHDen.Value.ToString("dd/MM/yyyy");
                string _lLS = frmHSBNNhapMoi.KTLSKCB(sthe, sthecu, 0);
                if (!string.IsNullOrEmpty(_lLS))
                {
                    //ChucNang.frm_LichSuKCB frm = new ChucNang.frm_LichSuKCB(_lLS, bn.SThe);
                    //frm.Show();
                    GiamDinhBHXH.BHXH_Model.ApiTheBHYT2018 the = new GiamDinhBHXH.BHXH_Model.ApiTheBHYT2018();
                    //the.maCSKCB = bn.MaCS;
                    //the.gioiTinh = bn.GTinh??0;
                    the.hoTen = bn.TenBNhan;
                    the.maThe = bn.SThe;
                    //the.ngayBD = bn.HanBHTu  == null ? "01/01/1970" : bn.HanBHTu.Value.ToString("dd/MM/yyyy") ;
                    //the.ngayKT = bn.HanBHDen == null ? "01/01/1970" : bn.HanBHDen.Value.ToString("dd/MM/yyyy");                  
                    the.ngaySinh = bn.NgaySinh + "/" + bn.ThangSinh + "/" + bn.NamSinh;
                    ChucNang.frm_LichSuKCB frm = new ChucNang.frm_LichSuKCB(the, DungChung.Bien.xmlFilePath_LIS[10], DungChung.Bien.xmlFilePath_LIS[11]);
                    frm.Show();

                }

            }

        }
        private void grvDSBN_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Column.Name == "colSThe")
            {
                int mabn = 0;
                if (grvDSBN.GetFocusedRowCellValue(colMaBNhan) != null)
                    mabn = Convert.ToInt32(grvDSBN.GetFocusedRowCellValue(colMaBNhan));
                Thread t = new Thread(ThreadStart => { showLichSuKCB(mabn); });
                t.Start();
            }
        }

        private void barInPhieuDK_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DungChung.Bien.MaBV == "34019" || DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "56789")
            {
                int rs;
                int mbn = 0;
                if (Int32.TryParse(txtMaBNhan.Text, out rs))
                    mbn = Convert.ToInt32(txtMaBNhan.Text);
                InPhieuGiuThe_TB(mbn);
            }
        }

        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int rs;
            int mbn = 0;
            if (Int32.TryParse(txtMaBNhan.Text, out rs))
                mbn = Convert.ToInt32(txtMaBNhan.Text);
            if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "56789")
            {
                InPhieuGiuThe_TB(mbn);
                return;
            }
            if (DungChung.Bien.MaBV == "30009")
            {

                inphieu30009(mbn);
                return;
            }

            if (DungChung.Bien.MaBV == "24009" || DungChung.Bien.MaBV == "26007" || DungChung.Bien.MaBV == "01830" || DungChung.Bien.MaBV == "34019" || DungChung.Bien.MaBV == "30007")
            {
                QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                _InGiayDK(data, mbn);
                return;
            }

            InPhieuChung(mbn);


        }

        private void cboDTuong_EditValueChanged(object sender, EventArgs e)
        {
            if (!formLoad)
                TimKiem();
        }

        private void cboNoiNgoaiTru_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!formLoad)
                TimKiem();
        }

        private void btnCDCLS_ItemClick(object sender, ItemClickEventArgs e)
        {
            int rs;
            int mbn = 0;
            if (Int32.TryParse(txtMaBNhan.Text, out rs))
                mbn = Convert.ToInt32(txtMaBNhan.Text);
            var _lTTBN = DaTaContext.BenhNhans.Where(p => p.MaBNhan == mbn).ToList();
            if (_lTTBN.First().DTuong == "Dịch vụ")//||_lTTBN.First().CapCuu==1)
            {
                var _lBNKB = DaTaContext.BNKBs.Where(p => p.MaBNhan == mbn).ToList();
                if (_lBNKB.Count == 0)
                {
                    FormThamSo.FRM_chidinh_Moi frm = new FormThamSo.FRM_chidinh_Moi(String.IsNullOrEmpty(txtMaBNhan.Text) ? 0 : Convert.ToInt32(txtMaBNhan.Text), txtTENBN.Text);
                    frm.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Bệnh nhân đã có chẩn đoán tại khoa, phòng;\n Không thể chỉ định CLS", "Thông báo", MessageBoxButtons.OK);
                }
            }
            else
            {
                MessageBox.Show("Chỉ định CLS tại phòng khám chỉ dành cho bệnh nhân dịch vụ", "Thông báo", MessageBoxButtons.OK);
            }
        }

        private void dtTuNgay_EditValueChanged(object sender, EventArgs e)
        {
            if (!formLoad)
                TimKiem();
        }

        private void dtDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            if (!formLoad)
                TimKiem();
        }

        private void btnGetPID_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtMaBNhan.Text))
            {
                int _mabn = Convert.ToInt32(txtMaBNhan.Text);
                QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                QLBV.DungChung.LienThongHSSK hssk = new QLBV.DungChung.LienThongHSSK();
                string pid = "";
                List<QLBV.DungChung.LienThongHSSK.MessageHSSK> listMessage = new List<DungChung.LienThongHSSK.MessageHSSK>();

                if (hssk.GetPID(data, _mabn, ref pid, ref listMessage))
                {
                    MessageBox.Show("Gửi thành công");
                }
                else
                {
                    MessageBox.Show("Gửi thất bại" + Environment.NewLine + string.Join(Environment.NewLine, listMessage.Select(o => o.Message)));
                }
            }
            else
                MessageBox.Show("Bạn chưa chọn bệnh nhân");
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void btnChiSoCoThe_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtMaBNhan.Text))
            {
                int _mabn = Convert.ToInt32(txtMaBNhan.Text);
                frm_TTKhamBenh frm = new frm_TTKhamBenh(_mabn);
                frm.ShowDialog();
            }
            else
                MessageBox.Show("Bạn chưa chọn bệnh nhân");
        }

        private void grvDSBN_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {

            if (e.IsGetData && e.Column.FieldName == "NgayThangNamSinh" && e.Column.UnboundType != UnboundColumnType.Bound)
            {
                var rowHandle = grvDSBN.GetRowHandle(e.ListSourceRowIndex);
                var ngaysinh = grvDSBN.GetRowCellValue(rowHandle, "NgaySinh").ToString();
                var thangsinh = grvDSBN.GetRowCellValue(rowHandle, "ThangSinh").ToString();
                var namsinh = grvDSBN.GetRowCellValue(rowHandle, "NamSinh").ToString();
                e.Value = (!string.IsNullOrWhiteSpace(ngaysinh) ? ngaysinh + "/" : "") + (!string.IsNullOrWhiteSpace(thangsinh) ? thangsinh + "/" : "") + (!string.IsNullOrWhiteSpace(namsinh) ? namsinh : "");
            }
        }

        private void btnThongTinBenhNhan_ItemClick(object sender, ItemClickEventArgs e)
        {
            frm_ThongTinBenhNhan frm = new frm_ThongTinBenhNhan();
            frm.ShowDialog();
        }

        private void btnSoTiepDon_ItemClick(object sender, ItemClickEventArgs e)
        {
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            frmIn frm = new frmIn();
            BaoCao.repBC_SoTiepDon rep = new BaoCao.repBC_SoTiepDon();

            _dttu = DungChung.Ham.NgayTu(dtTuNgay.DateTime);
            _dtden = DungChung.Ham.NgayDen(dtDenNgay.DateTime);

            int _makp = 0;
            if (lupMaKP.EditValue != null)
            {

                _makp = Convert.ToInt32(lupMaKP.EditValue);
            }

            int iddtbn = 0;

            if (cboDTuong.EditValue != null)
            {
                iddtbn = Convert.ToInt32(cboDTuong.EditValue);
            }

            var dsTiepDon = (from bnhan in data.BenhNhans
                             where bnhan.NNhap >= _dttu && bnhan.NNhap <= _dtden
                             where iddtbn == 99 ? true : bnhan.IDDTBN == iddtbn
                             where _makp == 0 ? true : bnhan.MaKP == _makp
                             join kphong in data.KPhongs on bnhan.MaKP equals kphong.MaKP
                             join canbo in data.CanBoes on bnhan.MaCB equals canbo.MaCB
                             join ttbs in data.TTboXungs on bnhan.MaBNhan equals ttbs.MaBNhan
                             join bnkb in data.BNKBs on bnhan.MaBNhan equals bnkb.MaBNhan into g
                             select new
                             {
                                 MaBNhan = data.BenhNhans.Where(s => s.MaBNhan == bnhan.MaBNhan).Select(s => s.MaBNhan).Distinct().FirstOrDefault(),
                                 bnhan.TenBNhan,
                                 Ngaysinh = bnhan.NgaySinh + "/" + bnhan.ThangSinh + "/" + bnhan.NamSinh,
                                 GTinh = bnhan.GTinh == 1 ? "Nam" : "Nữ",
                                 bnhan.DChi,
                                 bnhan.DTuong,
                                 bnhan.IDDTBN,
                                 bnhan.SThe,
                                 CMT = ttbs.SoKSinh,
                                 DienThoai = ttbs.DThoai + "; " + ttbs.DThoaiNT,
                                 CDNoiGT = data.BenhViens.Where(s => s.MaBV == bnhan.MaBV).Select(x => x.TenBV).FirstOrDefault(),
                                 TenKP = data.KPhongs.Where(s => s.MaKP == bnhan.MaKP).Select(x => x.TenKP).FirstOrDefault(),
                                 TenCB = data.CanBoes.Where(s => s.MaCB == g.FirstOrDefault().MaCB).Select(s => s.TenCB).Take(1).FirstOrDefault(),
                                 bnhan.NNhap
                             })
                             .OrderByDescending(p => p.NNhap)
                             .ToList();

            if (dsTiepDon.Count > 0)
            {
                rep.DataSource = dsTiepDon;
                int SoTT = 1;
                foreach (var item in dsTiepDon)
                {
                    rep.SoTT.Value = SoTT;
                    rep.MaBNhan.Value = item.MaBNhan;
                    rep.TenBNhan.Value = item.TenBNhan;
                    rep.NgaySinh.Value = item.Ngaysinh;
                    rep.GTinh.Value = item.GTinh;
                    rep.DChi.Value = item.DChi;
                    rep.DTuong.Value = item.DTuong;
                    rep.SThe.Value = item.SThe;
                    rep.CMT.Value = item.CMT;
                    rep.DienThoai.Value = item.DienThoai;
                    rep.CDNoiGT.Value = item.CDNoiGT;
                    rep.TenKP.Value = item.TenKP;
                    rep.TenCB.Value = item.TenCB;
                    rep.NNhap.Value = item.NNhap;
                    SoTT++;
                }
                rep.DataBinding();
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Chưa tiếp đón bệnh nhân có đội tượng này tại phòng khoa này trong khoảng thời gian này");
            } 
        }

        private void txtIDBN_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void btnMoi2_Click(object sender, EventArgs e)
        {

        }

        private void btnXLSX_ItemClick(object sender, ItemClickEventArgs e)
        {
            int sodk = 0;
            if (grvSoDK.RowCount > 0 && grvSoDK.GetFocusedRowCellValue(colSoDKKB) != null)
                sodk = Convert.ToInt32(grvSoDK.GetFocusedRowCellValue(colSoDKKB));
            QLBV.ChucNang.FormDanhMuc.Frm_Import_DsBNhan_excel frm = new QLBV.ChucNang.FormDanhMuc.Frm_Import_DsBNhan_excel(sodk);
            frm.ShowDialog();
        }
    }
    public class listKP
    {
        public int makp;
        public string tenkp;
        public int MaKP
        {
            set { makp = value; }
            get { return makp; }
        }
        public string TenKP
        {
            set { tenkp = value; }
            get { return tenkp; }

        }

        public string PLoai { get; set; }
    }
}
