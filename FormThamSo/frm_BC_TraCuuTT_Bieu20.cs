using QLBV.DungChung;
using QLBV_Database;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLBV.FormThamSo
{
    public partial class frm_BC_TraCuuTT_Bieu20 : Form
    {
        public frm_BC_TraCuuTT_Bieu20()
        {
            InitializeComponent();
        }

        int _madv = 0;
        string tenbc = string.Empty;

        public frm_BC_TraCuuTT_Bieu20(int madv)
        {
            InitializeComponent();
            _madv = madv;
        }

        class MyObject
        {
            public string value { set; get; }
            public string Text { set; get; }
        }

        public class KhoaPhong
        {
            public bool _check;
            public int _maKP;
            public string _kp;

            public int MaKP { get { return _maKP; } set { _maKP = value; } }
            public bool Check { get { return _check; } set { _check = value; } }
            public string TenKP { get { return _kp; } set { _kp = value; } }

            public string PLoai { get; set; }
        }

        public class CSKCB
        {
            public bool _check1;
            public string _maKP1;
            public string _kp1;

            public string MaKP { get { return _maKP1; } set { _maKP1 = value; } }
            public bool Check { get { return _check1; } set { _check1 = value; } }
            public string TenKP { get { return _kp1; } set { _kp1 = value; } }
        }

        private string MaKPQD(int mKP)
        {
            string rs = "";
            var a = _lKP.Where(p => p.MaKP == mKP).Select(p => p.MaQD).FirstOrDefault();
            if (a != null)
                rs = a.ToString();
            return rs;
        }

        private string theoquy()
        {
            string quy = "";

            if (ckQuy.Checked == true)
            {
                switch (timquy(lupTuNgay.DateTime.Month))
                {
                    case 1:
                        quy = " QUÝ I NĂM " + lupTuNgay.DateTime.Year;
                        break;
                    case 2:
                        quy = " QUÝ II NĂM " + lupTuNgay.DateTime.Year;
                        break;
                    case 3:
                        quy = " QUÝ III NĂM " + lupTuNgay.DateTime.Year;
                        break;
                    case 4:
                        quy = " QUÝ IV NĂM " + lupTuNgay.DateTime.Year;
                        break;
                }

            }
            else
            {
                quy = "Từ ngày  " + lupTuNgay.DateTime.ToString().Substring(0, 10) + "  đến ngày  " + lupDenNgay.DateTime.ToString().Substring(0, 10);
            }
            return quy;
        }

        private int timquy(int month)
        {
            if (month >= 1 && month <= 3)
            {
                return (1);
            }
            if (month > 3 && month <= 6)
            {
                return (2);
            }
            if (month > 6 && month <= 9)
            {
                return (3);
            }
            else { return (4); }
        }

        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        List<CSKCB> _lCSKCB = new List<CSKCB>();
        List<KPhong> _lKP = new List<KPhong>();
        List<int> _lDSMaDV = new List<int>();
        List<NhomDV> _lnhom = new List<NhomDV>();

        int load = 0;
        private void frm_BC_TraCuuTT_Bieu20_Load(object sender, EventArgs e)
        {
            ckc_BNCCT.Checked = true;
            ckBHTT.Checked = true;
            ckBNTT.Checked = true;
            rdHTThanhToan.SelectedIndex = 2;
            rdCKhoan.SelectedIndex = 2;
            var nhacc = data.NhaCCs.OrderBy(p => p.TenCC).ToList();
            lupNhaCC.Properties.DataSource = nhacc;
            lupDenNgay.DateTime = System.DateTime.Now;
            lupTuNgay.DateTime = System.DateTime.Now;
            if (DungChung.Bien.MaBV == "12001")// bệnh viện tam đường
                ckDtuongCT.Visible = true;
            else
                ckDtuongCT.Visible = false;

            List<MyObject> lMaDtuong = new List<MyObject>();
            lMaDtuong = data.DTuongs.Where(p => p.Status == 1).Select(p => new MyObject { value = p.MaDTuong, Text = p.MaDTuong }).OrderBy(p => p.Text).ToList();
            //lMaDtuong = data.BenhNhans.Select(p => new MyObject { value = p.MaDTuong == null ? "" : p.MaDTuong.Trim().ToUpper(), Text = p.MaDTuong == null ? "" : p.MaDTuong.Trim().ToUpper() }).Distinct().OrderBy(p => p.Text).ToList();
            lMaDtuong.Insert(0, new MyObject { value = "", Text = "Tất cả" });
            if (lMaDtuong.Count <= 0)
                MessageBox.Show("Danh mục Đối Tượng chưa được thiết lập!");
            cklMaDTuong.DataSource = lMaDtuong;
            cklMaDTuong.CheckAll();


            // load nhóm Dv

            // load danh sách khoa phòng
            var q3 = (from k in data.KPhongs
                      join rv in data.RaViens on k.MaKP equals rv.MaKP
                      select k).ToList();
            var q2 = (from k in q3
                      select k).Distinct().ToList();
            var q = (from k in q2
                     select new KhoaPhong()
                     {
                         Check = false,
                         MaKP = k.MaKP,
                         TenKP = k.TenKP,
                         PLoai = k.PLoai
                     }).Distinct().OrderBy(p => p.PLoai).ToList();
            List<KhoaPhong> _lKP2 = new List<KhoaPhong>(q.Count + 1);
            _lKP2.Add(new KhoaPhong { Check = false, MaKP = 0, TenKP = "Tất cả" });
            _lKP2.InsertRange(1, q);
            grcKhoaPhong.DataSource = _lKP2;
            for (int i = 0; i < grvKhoaPhong.RowCount; i++)
            {
                grvKhoaPhong.SetRowCellValue(i, colCheckGrvKP, true);
            }

            List<DTBN> _lDTBN = new List<DTBN>();
            _lDTBN = data.DTBNs.Where(p => p.Status == 1).ToList();
            lupDoituong.Properties.DataSource = _lDTBN;
            lupDoituong.Text = "BHYT";
            radNoiTru_SelectedIndexChanged(sender, e);
            radio_DTNT.SelectedIndex = 2;
            radXP_SelectedIndexChanged(sender, e);
            load++;
        }

        public string[,] NhomTheoTT(string tennhom, string tennhomct, string tenrg)
        {
            string[,] re = new string[1, 2] { { tennhom, "99" } };
            if (ck_CV285_20001.Checked)
            {
                if (tennhomct == "Khám bệnh")
                {
                    re[0, 0] = tennhom;
                    re[0, 1] = "1";
                }
                if (tennhomct == "Giường điều trị ngoại trú")
                {
                    re[0, 0] = "Ngày giường";
                    re[0, 1] = "2";
                }
                if (tennhomct == "Giường điều trị nội trú")
                {
                    re[0, 0] = "Ngày giường";
                    re[0, 1] = "2";
                }
                if (tennhomct == "Xét nghiệm")
                {
                    re[0, 0] = tennhom;
                    re[0, 1] = "3";
                }
                if (tennhomct == "Chẩn đoán hình ảnh")
                {
                    re[0, 0] = tennhom; //+ ", Thăm dò chức năng";
                    re[0, 1] = "4";
                }

                if (tennhomct == "Thăm dò chức năng")
                {
                    re[0, 0] = tennhom;// "Chẩn đoán hình ảnh, " + tennhom;
                    re[0, 1] = "5";
                }

                if (DungChung.Bien.MaBV == "26007" || DungChung.Bien.MaBV == "30009")
                {
                    if (tennhomct == "Thủ thuật, phẫu thuật")
                    {
                        re[0, 0] = tennhom;
                        re[0, 1] = "6";
                    }
                }
                else
                {
                    if (tenrg == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat)//(tennhomct == "Thủ thuật, phẫu thuật")
                    {
                        re[0, 0] = tenrg;
                        re[0, 1] = "6";
                    }
                    if (tenrg == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.PhauThuat)
                    {
                        re[0, 0] = tenrg;
                        re[0, 1] = "7";
                    }
                }
                if (tennhomct == "Vận chuyển")
                {
                    re[0, 0] = tennhom;
                    re[0, 1] = "8";
                }
                if (tennhomct == "VTYT thanh toán theo tỷ lệ")
                {
                    re[0, 0] = tennhom;
                    re[0, 1] = "9";
                }
                if (tennhomct == "DVKT thanh toán theo tỷ lệ")
                {
                    re[0, 0] = tennhom;
                    re[0, 1] = "10";
                }
                if (tennhomct == "Máu và chế phẩm của máu")
                {
                    re[0, 0] = tennhom;
                    re[0, 1] = "11";
                }
                if (tennhomct == "Thuốc thanh toán theo tỷ lệ")
                {
                    re[0, 0] = tennhom;
                    re[0, 1] = "12";
                }
                if (tennhomct == "Thuốc trong danh mục BHYT")
                {
                    re[0, 0] = tennhom;
                    re[0, 1] = "13";
                }
                if (tennhomct == "Vật tư y tế trong danh mục BHYT")
                {
                    re[0, 0] = tennhom;
                    re[0, 1] = "14";
                }
                if (tennhomct == "Thuốc điều trị ung thư, chống thải ghép ngoài danh mục")
                {
                    re[0, 0] = tennhom;
                    re[0, 1] = "15";
                }
            }
            else
            {

                if (tennhomct == "Khám bệnh")
                {
                    re[0, 0] = tennhom;
                    re[0, 1] = "1";
                }
                if (tennhomct == "Giường điều trị ngoại trú")
                {
                    re[0, 0] = tennhom;
                    re[0, 1] = "1";
                }
                if (tennhomct == "Giường điều trị nội trú")
                {
                    re[0, 0] = tennhom;
                    re[0, 1] = "2";
                }
                if (tennhomct == "Xét nghiệm")
                {
                    re[0, 0] = tennhom;
                    re[0, 1] = "3";
                }
                if (tennhomct == "Chẩn đoán hình ảnh")
                {
                    re[0, 0] = tennhom + ", Thăm dò chức năng";
                    re[0, 1] = "4";
                }

                if (tennhomct == "Thăm dò chức năng")
                {
                    re[0, 0] = "Chẩn đoán hình ảnh, " + tennhom;
                    re[0, 1] = "4";
                }
                if (tennhomct == "Thủ thuật, phẫu thuật")
                {
                    re[0, 0] = tennhom;
                    re[0, 1] = "5";
                }
                if (tennhomct == "Vận chuyển")
                {
                    re[0, 0] = tennhom;
                    re[0, 1] = "6";
                }
                if (tennhomct == "VTYT thanh toán theo tỷ lệ")
                {
                    re[0, 0] = tennhom;
                    re[0, 1] = "7";
                }
                if (tennhomct == "DVKT thanh toán theo tỷ lệ")
                {
                    re[0, 0] = tennhom;
                    re[0, 1] = "8";
                }
                if (tennhomct == "Máu và chế phẩm của máu")
                {
                    re[0, 0] = tennhom;
                    re[0, 1] = "9";
                }
                if (tennhomct == "Thuốc thanh toán theo tỷ lệ")
                {
                    re[0, 0] = tennhom;
                    re[0, 1] = "10";
                }
                if (tennhomct == "Thuốc trong danh mục BHYT")
                {
                    re[0, 0] = tennhom;
                    re[0, 1] = "11";
                }
                if (tennhomct == "Vật tư y tế trong danh mục BHYT")
                {
                    re[0, 0] = tennhom;
                    re[0, 1] = "12";
                }
                if (tennhomct == "Thuốc điều trị ung thư, chống thải ghép ngoài danh mục")
                {
                    re[0, 0] = tennhom;
                    re[0, 1] = "13";
                }
            }
            return re;
        }

        private void radNoiTru_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (radNoiTru.SelectedIndex == 0)
            {
                radio_DTNT.Properties.ReadOnly = false;
            }
            else
            {
                radio_DTNT.SelectedIndex = 2;
                radio_DTNT.Properties.ReadOnly = true;
            }
        }

        private void radXP_SelectedIndexChanged(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities _dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            _lCSKCB.Clear();
            var kphong = _dataContext.KPhongs.ToList();
            if (radXP.SelectedIndex == 0)
            {
                _lCSKCB.Add(new CSKCB { Check = true, MaKP = DungChung.Bien.MaBV, TenKP = DungChung.Bien.TenCQ });


            }
            if (radXP.SelectedIndex == 1)
            {
                //DungChung.Bien.mack_theoHangBV
                _lCSKCB = (from kp in kphong
                           where kp.PLoai == DungChung.Bien.st_PhanLoaiKP.XaPhuong //|| (kp.PLoai == DungChung.Bien.st_PhanLoaiKP.PhongKham && kp.TrongBV == 0)
                           select new CSKCB()
                           {
                               Check = false,
                               MaKP = kp.MaBVsd,
                               TenKP = kp.TenKP
                           }).Distinct().OrderBy(p => p.TenKP).ToList();

            }
            if (radXP.SelectedIndex == 2)
            {
                _lCSKCB = (from kp in kphong
                           where kp.PLoai == DungChung.Bien.st_PhanLoaiKP.PKKhuVuc
                           select new CSKCB()
                           {
                               Check = false,
                               MaKP = kp.MaBVsd,
                               TenKP = kp.TenKP
                           }).Distinct().OrderBy(p => p.TenKP).ToList();


            }
            if (radXP.SelectedIndex == 3)
            {
                _lCSKCB = (from kp in _dataContext.BenhViens.Where(p => p.Connect)
                           select new CSKCB()
                           {
                               Check = false,//ád
                               MaKP = kp.MaBV,
                               TenKP = kp.TenBV
                           }).Distinct().OrderBy(p => p.TenKP).ToList();

            }
            _lCSKCB.Insert(0, new CSKCB { Check = false, MaKP = "0", TenKP = "Tất cả" });
            cklKP.DataSource = _lCSKCB;
            cklKP.CheckAll();
        }
        private bool KTtaoBcMau20()
        {

            if (lupTuNgay.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn ngày bắt đầu in báo cáo");
                lupTuNgay.Focus();
                return false;
            }
            if (lupDenNgay.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn ngày kết thúc in báo cáo");
                lupDenNgay.Focus();
                return false;
            }
            else return true;
        }

        int _mauso = 20, Font = 0;
        private void btnInBC_Click(object sender, EventArgs e)
        {
            List<string> _dsCSKCB = new List<string>();
            for (int i = 0; i < cklKP.ItemCount; i++)
            {
                if (cklKP.GetItemChecked(i))
                {
                    _dsCSKCB.Add(cklKP.GetItemValue(i).ToString());
                }
            }
            _dsCSKCB = _dsCSKCB.Distinct().ToList();

            #region Lấy danh sách khoa phòng
            List<int> _lMaKhoa = new List<int>();
            int kp = 0;
            for (int i = 0; i < grvKhoaPhong.RowCount; i++)
            {
                if (grvKhoaPhong.GetRowCellValue(i, colCheckGrvKP) != null)
                {
                    if (grvKhoaPhong.GetRowCellValue(i, colCheckGrvKP).ToString() == "True")
                    {
                        int mKhoa = grvKhoaPhong.GetRowCellValue(i, colmaKP) == null ? -1 : Convert.ToInt32(grvKhoaPhong.GetRowCellValue(i, colmaKP));

                        if (mKhoa == 0)
                        {
                            kp = 0;

                            break;
                        }
                        else
                            _lMaKhoa.Add(mKhoa);
                    }
                    else
                    {
                        kp = -1;
                    }
                }
            }
            #endregion lấy danh sách khoa phòng
            #region Biến
            _lKP = data.KPhongs.ToList();
            string macc = "";
            int _makp = -1;

            DateTime tungay, denngay;
            int trongBH = 5;

            int _idDtuong = -1;
            if (lupDoituong.EditValue != null)
                _idDtuong = Convert.ToInt16(lupDoituong.EditValue);
            List<string> lmaDtuong = new List<string>();
            for (int i = 0; i < cklMaDTuong.ItemCount; i++)
            {
                if (cklMaDTuong.GetItemChecked(i))
                    lmaDtuong.Add(cklMaDTuong.GetItemValue(i).ToString());
            }
            if (lupNhaCC.EditValue != null && lupNhaCC.EditValue.ToString() != "")
                macc = lupNhaCC.EditValue.ToString();
            int xp = radXP.SelectedIndex;
            int _intduyet = 2;
            if (rad_Duyet.SelectedIndex != null)
                _intduyet = rad_Duyet.SelectedIndex;
            if (KTtaoBcMau20())
            {

                trongBH = rdTrongBH.SelectedIndex;

                tungay = DungChung.Ham.NgayTu(lupTuNgay.DateTime);
                denngay = DungChung.Ham.NgayDen(lupDenNgay.DateTime);
                int _ngaytt = radTimKiem.SelectedIndex;
                List<string> _ltamung = new List<string>();
                int _CP_BH = -1;
                if ((ckc_BNCCT.Checked && ckBHTT.Checked && ckBNTT.Checked) || (!ckc_BNCCT.Checked && !ckBHTT.Checked && !ckBNTT.Checked))
                    _CP_BH = 0;
                int HTThanhToan = rdHTThanhToan.SelectedIndex;
                int ChuyenKhoan = rdCKhoan.SelectedIndex;
                #endregion biến

                #region select tất cả

                //List<int> _idNhomDV = new List<int>();
                //for (int i = 0; i < cklNhomDV.ItemCount; i++)
                //{
                //    if (cklNhomDV.GetItemCheckState(i) == CheckState.Checked)
                //        _idNhomDV.Add(Convert.ToInt32(cklNhomDV.GetItemValue(i)));
                //}

                //_idNhomDV.Add(_madv);
                //_lnhom = (from nhom in data.NhomDVs join id in _idNhomDV on nhom.IDNhom equals id select nhom).ToList();
                int? id = 0;
                var _idnhom = (from a in data.DichVus.Where(p => p.MaDV == _madv) select new { a.IDNhom}).ToList();

                foreach (var item in _idnhom)
                {
                    id = item.IDNhom;
                }

                var qdv = (from nhom in data.NhomDVs.Where(p => p.IDNhom == id)
                           join tn in data.TieuNhomDVs on nhom.IDNhom equals tn.IDNhom
                           join dv in data.DichVus.Where(p =>p.MaDV == _madv) on tn.IdTieuNhom equals dv.IdTieuNhom
                           select new
                           {
                               nhom.IDNhom,
                               nhom.TenNhomCT,
                               nhom.TenNhom,
                               tn.TenTN,
                               tn.TenRG,
                               dv
                           }).ToList();
                int count = qdv.Count();
                Console.WriteLine("AA" + count);
                #region tạm ứng: lấy phần thu thẳng của bệnh nhân khi chọn thu thẳng và mẫu 21, BV 30002
                var qtu = (from tu in data.TamUngs.Where(p => p.NgayThu >= tungay && p.NgayThu <= denngay).Where(p => p.PhanLoai == 3)
                           join tuct in data.TamUngcts on tu.IDTamUng equals tuct.IDTamUng
                           select new { tu.MaBNhan, tu.IDTamUng, tuct.MaDV, tu.ThanhToan }).ToList();
                #endregion
                var q = (from rv in data.RaViens
                         join bn in data.BenhNhans
                         on rv.MaBNhan equals bn.MaBNhan
                         join ttbx in data.TTboXungs.Where(p => HTThanhToan == 2 ? true : p.HTThanhToan == HTThanhToan) on bn.MaBNhan equals ttbx.MaBNhan
                         join vp in data.VienPhis on bn.MaBNhan equals vp.MaBNhan
                         join vpct in data.VienPhicts
                         on vp.idVPhi equals vpct.idVPhi
                         where ((radTimKiem.SelectedIndex == 4) ? true : (_ngaytt == 2 ? (vp.NgayDuyet >= tungay && vp.NgayDuyet <= denngay) : (_ngaytt == 1 ? (vp.NgayTT >= tungay && vp.NgayTT <= denngay) : (_ngaytt == 3 ? (vp.NgayDuyetCP >= tungay && vp.NgayDuyetCP <= denngay) : (rv.NgayRa >= tungay && rv.NgayRa <= denngay)))))
                         where (cboNoiTinh.SelectedIndex == 0 ? true : bn.NoiTinh == cboNoiTinh.SelectedIndex)
                         where (radio_DTNT.SelectedIndex == 2 ? true : (radio_DTNT.SelectedIndex == 1 ? bn.DTNT : bn.DTNT == false))
                         where (radioThuChi.SelectedIndex == 2 || (radioThuChi.SelectedIndex == 0 && vpct.ThanhToan == 1) || (radioThuChi.SelectedIndex == 1 && vpct.ThanhToan == 0))
                         select new
                         {

                             bn.MaKCB,
                             bn.DTNT,
                             bn.MaBNhan,
                             bn.DTuong,
                             MaDTuong = bn.MaDTuong == null ? "" : bn.MaDTuong.Trim().ToUpper(),
                             bn.IDDTBN,
                             bn.NoiTru,
                             bn.TuyenDuoi,
                             vpct.TrongBH,
                             MaKP = vpct.MaKP == null ? 0 : vpct.MaKP,
                             rv.NgayRa,
                             rv.MaICD,
                             vp.NgayTT,
                             vpct.DonGia,
                             vpct.MaDV,
                             vpct.SoLuong,
                             vpct.ThanhTien,
                             vpct.TienBH,
                             vpct.TienBN,
                             vpct.IDTamUng,
                             vp.NgayDuyet,
                             vpct.TyLeTT,
                             vpct.XHH,
                             vpct.LoaiDV,
                             vpct.TBNCTT,
                             vpct.TBNTT
                         }).ToList();

                #endregion

                List<cls19_20> _list = new List<cls19_20>();
                List<cls19_20> _listXML = new List<cls19_20>();

                var q6 = (from l in q.Where(p => (radTimKiem.SelectedIndex == 4) ? (qtu.Where(o => o.MaBNhan == p.MaBNhan).Where(o => o.MaDV == p.MaDV).Count() > 0) : true)
                          join dt in lmaDtuong on l.MaDTuong equals dt
                          join nhom in qdv on l.MaDV equals nhom.dv.MaDV
                          join lcs in _dsCSKCB on l.MaKCB equals lcs
                          where (l.IDDTBN == _idDtuong)
                          where (macc == "" ? true : nhom.dv.MaCC == macc)
                          where (kp == 0 ? true : _lMaKhoa.Contains(l.MaKP == null ? 0 : l.MaKP.Value))
                          where trongBH > 2 ? true : (l.TrongBH == trongBH) && (radXP.SelectedIndex < 3 ? l.TuyenDuoi == radXP.SelectedIndex : true)
                          where (radNoiTru.SelectedIndex < 2 ? l.NoiTru == radNoiTru.SelectedIndex : true)
                          where (_intduyet == 2 ? true : (_intduyet == 1 ? l.NgayDuyet != null : l.NgayDuyet == null))
                          where (ckcTheoYC.Checked ? l.LoaiDV == 1 : l.LoaiDV == 0)
                          select new
                          {
                              l.TyLeTT,
                              l.MaKCB,
                              //  IDTamUng = _lIDtamung.Where(p => p.IDTamUng == l.IDTamUng).ToList().Count > 0 ? _lIDtamung.Where(p => p.IDTamUng == l.IDTamUng).First().IDTamUng : 0,
                              l.MaBNhan,
                              l.NoiTru,
                              l.MaDTuong,
                              MaKP = kp == 0 ? "" : MaKPQD(l.MaKP ?? 0),
                              l.MaICD,
                              l.TrongBH,
                              DonGia = chkTTTheoTyLe.Checked ? ((l.DonGia * l.TyLeTT) / 100) : l.DonGia,
                              nhom.dv.DonGiaTT15,
                              nhom.dv.SoTTqd,
                              nhom.dv.IDNhom,
                              nhom.dv.BHTT,
                              nhom.TenNhomCT,
                              nhom.TenNhom,
                              nhom.TenTN,
                              nhom.dv.TenHC,
                              nhom.dv.MaQD,
                              nhom.dv.DuongD,
                              nhom.dv.MaDuongDung,
                              TenDV = (DungChung.Bien.MaBV == "12122" && _mauso == 20) ? (nhom.dv.TenDV + " " + nhom.dv.HamLuong) : nhom.dv.TenDV,
                              nhom.dv.HamLuong,
                              nhom.dv.DonVi,
                              nhom.dv.SoDK,
                              nhom.dv.MaDV,
                              nhom.dv.QCPC,
                              nhom.dv.MaCC,
                              nhom.dv.SoQD,
                              nhom.dv.NuocSX,
                              nhom.dv.NhaSX,
                              nhom.dv.MaTam,
                              nhom.TenRG,
                              TenRGG = nhom.dv.TenRG,
                              GiaThau = nhom.dv.DonGia,
                              sapxep = nhom.dv.DongY == 1 ? 3 : ((nhom.TenRG == "Thuốc đông y" || nhom.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuocThuong_CPYHCT) ? 2 : 1),
                              tensapxep = nhom.dv.DongY == 1 ? "Thuốc đông y" : ((nhom.TenRG == "Thuốc đông y" || nhom.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuocThuong_CPYHCT) ? "Chế phẩm y học cổ truyền" : "Thuốc tân dược"),
                              l.SoLuong,
                              ThanhTien = Math.Round(_CP_BH == 0 ? ((DungChung.Bien.MaBV == "20001" || DungChung.Bien.MaBV == "30009" || DungChung.Bien.MaBV == "30003" || DungChung.Bien.MaBV == "30303") ? l.ThanhTien : l.ThanhTien * (chkTTTheoTyLe.Checked ? (l.TyLeTT / 100) : 1)) : (((ckBHTT.Checked ? l.TienBH : 0) + (ckBNTT.Checked ? l.TBNTT : 0) + (ckc_BNCCT.Checked ? l.TBNCTT : 0)) * (chkTTTheoTyLe.Checked ? (l.TyLeTT / 100) : 1)), 2, MidpointRounding.AwayFromZero), //chkLamTron.Checked ? (_CP_BH == 0 ? (Math.Round(Math.Round(l.SoLuong, 2) * Math.Round(l.DonGia, 2) * (l.TyLeTT / 100), 0)) : (_CP_BH == 1 ? l.TienBN : l.TienBH)) : (_CP_BH == 0 ? l.ThanhTien : (_CP_BH == 1 ? l.TienBN : l.TienBH))
                              TienBH = Math.Round(_CP_BH == 0 ? l.TienBH : (ckBHTT.Checked ? l.TienBH : 0), 2, MidpointRounding.AwayFromZero),
                          }).ToList();

                #region chỉ tìm những bệnh nhân có thực hiện dịch vụ nào đấy
                List<int> _lMaBN_DV = new List<int>();
                if (_lDSMaDV.Count > 0 && _mauso == 21)
                {
                    var qdsbn = (from bn in q join dv in _lDSMaDV on bn.MaDV equals dv group bn by bn.MaBNhan into kq1 select new { MaBNhan = kq1.Key }).ToList();
                    _lMaBN_DV = qdsbn.Select(p => p.MaBNhan).ToList();
                }

                List<int> _ltranfer = data.Tranfers.Select(p => p.MaBNhan).ToList();
                #endregion

                #region group để tính tổng tiền theo bệnh nhân
                var q2 = (from lq in q6.Where(p => (_lDSMaDV.Count > 0 && _mauso == 21) ? _lMaBN_DV.Contains(p.MaBNhan) : true).Where(p => ChuyenKhoan == 1 ? _ltranfer.Contains(p.MaBNhan) : (ChuyenKhoan == 0 ? (!_ltranfer.Contains(p.MaBNhan)) : true))
                          group lq by new
                          {
                              lq.BHTT,
                              //lq.MaKCB,
                              lq.TrongBH,
                              lq.DonGia,
                              lq.DonGiaTT15,
                              lq.SoTTqd,
                              lq.IDNhom,
                              lq.TenNhomCT,
                              lq.TenNhom,
                              lq.TenTN,
                              lq.TenHC,
                              lq.MaQD,
                              lq.DuongD,
                              lq.MaDuongDung,
                              lq.QCPC,
                              lq.TenDV,
                              lq.MaCC,
                              lq.HamLuong,
                              lq.DonVi,
                              lq.SoDK,
                              lq.MaDV,
                              lq.SoQD,
                              lq.NuocSX,
                              lq.NhaSX,
                              lq.GiaThau,
                              lq.MaTam,
                              lq.sapxep,
                              lq.tensapxep,
                              lq.TenRG,
                              lq.TenRGG,
                              TyLeTT = DungChung.Bien.MaBV == "27194" ? lq.TyLeTT : (ck_CV285_20001.Checked ? lq.TyLeTT : 1)
                          } into kq
                          select new
                          {
                              // kq.Key.MaKCB,
                              TyLeTT = kq.Key.TyLeTT,
                              MaKCB = string.Join(",", kq.Select(p => p.MaKCB).Distinct()),
                              TrongBH = kq.Key.TrongBH,
                              DonGia = kq.Key.DonGia,
                              DonGiaTT15 = kq.Key.DonGiaTT15,
                              SoTTqd = kq.Key.SoTTqd,
                              IdNhom = kq.Key.IDNhom,
                              TenNhomCT = kq.Key.TenNhomCT,
                              TenNhomThuoc = kq.Key.TenNhom,
                              TenTNhom = kq.Key.TenTN,
                              TenHC = kq.Key.TenHC,
                              MaQD = kq.Key.MaQD,
                              DuongDung = kq.Key.DuongD,
                              kq.Key.MaDuongDung,
                              kq.Key.NuocSX,
                              kq.Key.NhaSX,
                              kq.Key.GiaThau,
                              kq.Key.MaTam,
                              TenThuoc = kq.Key.TenDV,
                              HamLuong = kq.Key.HamLuong,
                              DonVi = kq.Key.DonVi,
                              SoDK = kq.Key.SoDK,
                              MaDV = kq.Key.MaDV,
                              MaCC = kq.Key.MaCC,
                              SoQD = kq.Key.SoQD,
                              TenRGG = kq.Key.TenRGG,
                              kq.Key.QCPC,
                              kq.Key.sapxep,
                              kq.Key.tensapxep,
                              SoLuongNT = kq.Where(p => p.NoiTru == 1).Sum(p => p.SoLuong),
                              SoLuongNgT = kq.Where(p => p.NoiTru == 0).Sum(p => p.SoLuong),
                              BHTT = kq.Key.BHTT,
                              Thanhtien_NoiTru = kq.Where(p => p.NoiTru == 1).Sum(p => p.ThanhTien),
                              Thanhtien_Ngtru = kq.Where(p => p.NoiTru == 0).Sum(p => p.ThanhTien),
                              SoLuong139 = kq.Where(p => p.MaDTuong == "DT" || p.MaDTuong == "HN" || p.MaDTuong == "DK").Sum(p => p.SoLuong),
                              SoLuongTE = kq.Where(p => p.MaDTuong == "TE").Sum(p => p.SoLuong),
                              SoLuongBHYT_DV = (lupDoituong.Text == "Dịch vụ") ? (kq.Sum(p => p.SoLuong)) : ((kq.Where(p => p.MaDTuong != "DT" && p.MaDTuong != "HN" && p.MaDTuong != "DK" && p.MaDTuong != "TE").Sum(p => p.SoLuong))),
                              SoLuong = kq.Sum(p => p.SoLuong),
                              ThanhTien = kq.Sum(p => p.ThanhTien)
                              ,
                              TienBH = kq.Sum(p => p.TienBH),
                              kq.Key.TenRG

                          }).OrderBy(p => p.TenThuoc).ToList();
                #endregion group để tính theo tiền bệnh nhân

                #region đổ vào list cls19_20
                foreach (var l in q2)
                {
                    if (l.TenThuoc == "Ngưu tất")
                    {
                        string a = "";
                    }
                    cls19_20 cls = new cls19_20();
                    // cls.Ma_CSKCB = l.MaKCB;
                    cls.Stt = Convert.ToInt32(NhomTheoTT(l.TenNhomThuoc, l.TenNhomCT, l.TenRG)[0, 1]);
                    switch (cls.Stt)
                    {
                        case 1:
                            cls.STTNhomHT = "I.";
                            break;
                        case 2:
                            cls.STTNhomHT = "II.";
                            break;
                        case 3:
                            cls.STTNhomHT = "III.";
                            break;
                        case 4:
                            cls.STTNhomHT = "IV.";
                            break;
                        case 5:
                            cls.STTNhomHT = "V.";
                            break;
                        case 6:
                            cls.STTNhomHT = "VI.";
                            break;
                        case 7:
                            cls.STTNhomHT = "VII.";
                            break;
                        case 8:
                            cls.STTNhomHT = "VIII.";
                            break;
                        case 9:
                            cls.STTNhomHT = "IX.";
                            break;
                        case 10:
                            cls.STTNhomHT = "X.";
                            break;
                        case 11:
                            cls.STTNhomHT = "XI.";
                            break;
                        case 12:
                            cls.STTNhomHT = "XII.";
                            break;
                    }
                    cls.SoQD = l.SoQD;
                    cls.TrongBH = Convert.ToInt16(l.TrongBH);
                    cls.Don_gia = Math.Round(l.DonGia, 3);
                    cls.Don_gia_tt39 = Math.Round(l.DonGiaTT15, 3);
                    cls.SoTTqd = l.SoTTqd != null ? l.SoTTqd.ToString() : "";
                    cls.Ma_nhom = Convert.ToInt32(l.IdNhom);
                    cls.Tennhom = (NhomTheoTT(l.TenNhomThuoc, l.TenNhomCT, l.TenRG)[0, 0]);
                    cls.TenTN = l.TenTNhom == null ? "" : l.TenTNhom;
                    cls.TenHC = l.TenHC == null ? "" : l.TenHC;
                    cls.MaQD = l.MaQD == null ? "" : l.MaQD;
                    cls.QCPC = l.QCPC == null ? "" : l.QCPC;
                    cls.NuocSX = l.NuocSX == null ? "" : l.NuocSX;
                    cls.Hangsx = l.NhaSX == null ? "" : l.NhaSX;
                    cls.GiaThau = l.GiaThau;
                    cls.Matam = l.MaTam;
                    cls.Duong_dung = l.DuongDung == null ? "" : l.DuongDung;
                    cls.Ma_DuongD = l.MaDuongDung == null ? "" : l.MaDuongDung;
                    cls.Ten_thuoc = l.TenThuoc == null ? "" : l.TenThuoc;
                    cls.Ham_luong = l.HamLuong == null ? "" : l.HamLuong;
                    cls.Don_vi_tinh = l.DonVi == null ? "" : l.DonVi;
                    cls.So_dang_ky = l.SoDK == null ? "" : l.SoDK;
                    cls.Ma_thuoc = l.MaDV.ToString();
                    cls.MaCC = l.MaCC == null ? "" : l.MaCC;
                    cls.SoluongNT = Math.Round(Convert.ToDouble(l.SoLuongNT), 3);
                    cls.SoluongNgT = Math.Round(Convert.ToDouble(l.SoLuongNgT), 3);
                    cls.Thanhtien_noitru = Convert.ToDouble(l.Thanhtien_NoiTru);
                    cls.Thanhtien_ngtru = Convert.ToDouble(l.Thanhtien_Ngtru);
                    cls.SoLuong139 = Convert.ToDouble(l.SoLuong139);
                    cls.SoLuongTE = Convert.ToDouble(l.SoLuongTE);
                    cls.SoLuongBHYT_DV = Convert.ToDouble(l.SoLuongBHYT_DV);
                    cls.So_luong = Convert.ToDouble(l.SoLuong);
                    //string tt = ((cls.SoluongNT + cls.SoluongNgT) * cls.Don_gia).ToString("N2");
                    cls.Thanh_tien = Math.Round(l.ThanhTien, 2);////Convert.ToDouble(l.ThanhTien); // a Quý yc ngày 28062018
                    if (l.IdNhom == 15)
                    {
                        cls.TenTieuNhom = ".Ngày giường điều trị nội trú";
                        cls.STTTieuNhom = 2;
                    }
                    else if (l.IdNhom == 14)
                    {
                        if (l.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.GiuongNgoaiTru)
                        {
                            cls.TenTieuNhom = ".Ngày giường điều trị ban ngày";
                            cls.STTTieuNhom = 1;
                        }
                        else if (l.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.GiuongLuu)
                        {
                            cls.TenTieuNhom = ".Ngày giường lưu";
                            cls.STTTieuNhom = 3;
                        }
                    }
                    cls.Tenrg = l.TenRGG;
                    cls.SapXep = l.sapxep;
                    cls.TenSapXep = l.tensapxep;
                    cls.TyLeTT = Convert.ToDouble(l.TyLeTT);
                    cls.TienBH = l.TienBH;
                    _list.Add(cls);

                }
                _list = _list.OrderBy(p => p.SapXep).ThenBy(p => p.Ten_thuoc).ToList();
                
                #endregion đổ vào list
                BaoCao.repBcMau20_1399 rep;
                rep = new BaoCao.repBcMau20_1399(chkTieuNhom.Checked);
                if (lupDoituong.Text == "BHYT")
                {
                    rep.TuNgayDenNgay.Value = theoquy();
                    rep.paramTenBC.Value = trongBH == 0 ? "THỐNG KÊ THUỐC THANH TOÁN NGOÀI BHYT" : "THỐNG KÊ THUỐC THANH TOÁN BHYT";
                    tenbc = trongBH == 0 ? "THỐNG KÊ THUỐC THANH TOÁN NGOÀI BHYT" : "THỐNG KÊ THUỐC THANH TOÁN BHYT";
                }
                else
                {
                    rep.TuNgayDenNgay.Value = theoquy();
                    rep.paramTenBC.Value = "THỐNG KÊ THUỐC THANH TOÁN DỊCH VỤ";
                    tenbc = "THỐNG KÊ THUỐC THANH TOÁN DỊCH VỤ";
                }
                if (!string.IsNullOrEmpty(macc))
                    rep.NhaCC.Value = lupNhaCC.Text;
                rep.DataSource = _list.OrderBy(p => p.SapXep).ThenBy(p => p.Ten_thuoc).ToList();

                #region xuat Excel



                string[] _arr = new string[] { "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@" };
                int[] _arrWidth = new int[] { };
                string[] _tieude = { "STT", "STT theo DMT của BYT", "Tên hoạt chất", "Tên thuốc thành phẩm", "Đường dùng, dạng bào chế", "Hàm lượng nồng độ", "Số đăng ký hoặc số GPNK", "Đơn vị tính", "Số lượng ngoại trú", "Số lượng nội trú", "Đơn giá", "Thành tiền" };
                DungChung.Bien.MangHaiChieu = new Object[_list.OrderBy(p => p.SapXep).ThenBy(p => p.Ten_thuoc).ToList().Count + 6, 12];
                DungChung.Bien.MangHaiChieu[1, 0] = "Tên CSYT:"; DungChung.Bien.MangHaiChieu[1, 1] = DungChung.Bien.TenCQ.ToUpper(); DungChung.Bien.MangHaiChieu[1, 4] = "Mẫu số 20/BHYT";
                DungChung.Bien.MangHaiChieu[2, 0] = "Mã CSYT:"; DungChung.Bien.MangHaiChieu[2, 1] = DungChung.Bien.MaBV;
                DungChung.Bien.MangHaiChieu[3, 2] = tenbc;
                DungChung.Bien.MangHaiChieu[4, 2] = theoquy();
                for (int i = 0; i < _tieude.Length; i++)
                {
                    DungChung.Bien.MangHaiChieu[5, i] = _tieude[i];
                }

                int num = 6;
                foreach (var r in _list.OrderBy(p => p.SapXep).ThenBy(p => p.Ten_thuoc).ToList())
                {
                    DungChung.Bien.MangHaiChieu[num, 0] = r.SoTTqd;
                    DungChung.Bien.MangHaiChieu[num, 1] = r.MaQD;
                    DungChung.Bien.MangHaiChieu[num, 2] = r.TenHC;
                    DungChung.Bien.MangHaiChieu[num, 3] = r.Ten_thuoc;
                    DungChung.Bien.MangHaiChieu[num, 4] = r.Duong_dung;
                    DungChung.Bien.MangHaiChieu[num, 5] = r.Ham_luong;
                    DungChung.Bien.MangHaiChieu[num, 6] = r.So_dang_ky;
                    DungChung.Bien.MangHaiChieu[num, 7] = r.Don_vi_tinh;
                    DungChung.Bien.MangHaiChieu[num, 8] = r.SoluongNgT;
                    DungChung.Bien.MangHaiChieu[num, 9] = r.SoluongNT;
                    DungChung.Bien.MangHaiChieu[num, 10] = r.Don_gia;
                    DungChung.Bien.MangHaiChieu[num, 11] = r.Thanh_tien;
                    num++;

                }

                frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, tenbc, "C:\\Biểu20.xls", true, this.Name);
                #endregion

                rep.BindingData();
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
            }
        }
    }
}
