using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Localization;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using QLBV.DungChung;
using System;
using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Windows.Forms;
using QLBV.Class;
using System.Net.Http;
using Newtonsoft.Json;
using RestSharp;
using System.Net;
using System.IO;
using System.Net.Http.Headers;
using QLBV.KETNOIV20;
using System.Globalization;
using System.Web.Script.Serialization;

namespace QLBV.ChucNang
{
    public partial class frm_GuiHSSKV20 : Form
    {
        public frm_GuiHSSKV20()
        {
            InitializeComponent();
            //GridLocalizer.Active = new MyGridLocalizer();

        }

        List<BenhNhanADO> listSelecteds;
        List<BenhNhanADO> listAll;
        int stt;
        string checkData;
        private void frm_GuiHSSK_Load(object sender, EventArgs e)
        {
            //if (DungChung.Bien.MaBV != "24009")
            //{
            //    colXXemHSBN.Visible = false;
            //    gridColumn20.Visible = false;
            //}
           
            listSelecteds = new List<BenhNhanADO>();
            txtSearch.ResetText();
           
            LoadDataToForm();
            gridColumn12.Image = imageList1.Images[0];
            cbbDoiTuong.Text = cbbDoiTuong.Items[0].ToString();
        }

        private void LoadDataToForm()
        {
            var dTuong = cbbDoiTuong.SelectedIndex;
            var index = radioGroupDoiTuong.SelectedIndex;
            QLBV_Database.QLBVEntities dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            listAll = (from bn in dataContext.BenhNhans.Where(o => o.MaKCB == DungChung.Bien.MaBV && (dTuong == 0 ? true : (dTuong == 1 ? o.DTuong == "BHYT" : (dTuong == 2 ? o.DTuong == "Dịch vụ" : o.DTuong == "KSK"))))
                       .Where(o => (index == 0 ? true : (index == 1 ? o.NoiTru == 1 : (index == 2 ? (o.NoiTru != 1 && o.DTNT == false) : (o.DTNT == true && o.NoiTru != 1)))))
                       join vp in dataContext.VienPhis on bn.MaBNhan equals vp.MaBNhan
                       join rv in dataContext.RaViens on bn.MaBNhan equals rv.MaBNhan
                       select new BenhNhanADO { MaBNhan = bn.MaBNhan, DChi = bn.DChi, TenBNhan = bn.TenBNhan, DTuong = bn.DTuong, Tuoi = bn.Tuoi, Check = false, GTinh = bn.GTinh, NgaySinh = bn.NgaySinh, ThangSinh = bn.ThangSinh, NamSinh = bn.NamSinh, EXPORTV20  = vp.ExportBYT  }).ToList();
            listAll.ForEach(x => { if (listSelecteds.Exists(o => o.MaBNhan == x.MaBNhan)) x.Check = true; x.Is_Send = x.EXPORTV20; });
            gridControlSearch.BeginUpdate();
            gridControlSearch.DataSource = listAll;
            gridControlSearch.EndUpdate();
        }



        private void btnSend_Click(object sender, EventArgs e)
        {
            List<BenhNhanADO> listMaBNhan = new List<BenhNhanADO>();
            List<string> listErrors = new List<string>();
            int CountSuccess = 0;
            int CountError = 0;
            string PersonID = "";
            QLBV_Database.QLBVEntities dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var dataSource = (List<BenhNhanADO>)gridControlChoose.DataSource;
            if (dataSource != null && dataSource.Count > 0)
            {

                if (string.IsNullOrEmpty(DungChung.Bien.xmlFilePath_LIS[41]) || string.IsNullOrEmpty(DungChung.Bien.xmlFilePath_LIS[42])|| string.IsNullOrEmpty(DungChung.Bien.xmlFilePath_LIS[43]))
                {
                    MessageBox.Show("Bạn chưa nhập username hoặc password vào file thiết lập hệ thống !!!");
                    return;
                }

                foreach (var item in dataSource)
                {
                    var bn = dataContext.BenhNhans.FirstOrDefault(o => o.MaBNhan == item.MaBNhan);
                    var vienphi = dataContext.VienPhis.FirstOrDefault(t => t.MaBNhan == item.MaBNhan);
                   
                    if (bn!=null)
                    {
                        if (createCheckOut(dataContext, bn.MaBNhan))
                        {
                            listMaBNhan.Add(item);
                            vienphi.ExportBYT = true;
                         
                            //  MessageBox.Show("Gửi dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            dataContext.SaveChanges();
                            CountSuccess++;
                        }
                        else CountError++;
                    }    

                   
                }
                gridControlSend.BeginUpdate();
                gridControlSend.DataSource = listMaBNhan;
                gridControlSend.EndUpdate();
                XtraMessageBox.Show(string.Format("Gửi thành công {0} bệnh nhân." + Environment.NewLine + "Gửi thất bại {1} bệnh nhân.", CountSuccess, CountError++));
                listSelecteds = listSelecteds.Where(o => !listMaBNhan.Select(p => p.MaBNhan).Contains(o.MaBNhan)).ToList();
                gridControlChoose.BeginUpdate();
                gridControlChoose.DataSource = listSelecteds;
                gridControlChoose.EndUpdate();
                LoadDataToForm();
                txtSearch_KeyDown(null, new KeyEventArgs(Keys.Enter));
            }
        }

        public bool createCheckOut(QLBV_Database.QLBVEntities data, int maBN)
        {
            BenhNhan bn = (from bnhan in data.BenhNhans.Where(p => p.MaBNhan == maBN) join rvien in data.RaViens on bnhan.MaBNhan equals rvien.MaBNhan select bnhan).Where(p => p.SThe != null && p.SThe.Length == 15).FirstOrDefault();
            if (DungChung.Bien.MaBV == "24012")
            {
                bn = (from bnhan in data.BenhNhans.Where(p => p.MaBNhan == maBN) join rvien in data.RaViens on bnhan.MaBNhan equals rvien.MaBNhan select bnhan).FirstOrDefault();
            }
            RaVien rv = data.RaViens.Where(p => p.MaBNhan == maBN).FirstOrDefault();
            TTboXung ttbs = data.TTboXungs.Where(p => p.MaBNhan == maBN).FirstOrDefault();
            VaoVien vv = data.VaoViens.Where(p => p.MaBNhan == maBN).FirstOrDefault();
            VienPhi vp = data.VienPhis.Where(p => p.MaBNhan == maBN).FirstOrDefault();
            List<VienPhict> lvpct = new List<VienPhict>();
            tongtienXML2_XML3 _tongtienXML2_XML3 = new tongtienXML2_XML3();

            string dlBang1 = getBang1(data, bn, ttbs, vv, rv, vp, lvpct, _tongtienXML2_XML3);
            string dlBang2 = getbang2(data, bn, ttbs, vv, rv, vp, lvpct, _tongtienXML2_XML3);
            string dlBang3 = getBang3(data, bn, ttbs, vv, rv, vp, lvpct, ref _tongtienXML2_XML3);
            string dlBang4 = getBang4(data, bn, ttbs, vv, rv, vp);
            string dlBang5 = getBang5(data, bn, ttbs, vv, rv, vp);
  
            string result = "";
            if (dlBang1 != null)
            {
                result = @"{    ""BANG1"": " + dlBang1;
            }
            if (dlBang2 != null)
            {
                result += @", ""BANG2"": " + dlBang2;
            }
            if (dlBang3 != null)
            {
                result += @", ""BANG3"": " + dlBang3;
            }
            if (dlBang4 != null)
            {
                result += @", ""BANG4"": " + dlBang4;
            }
            if (dlBang5 != null)
            {
                result += @", ""BANG5"": " + dlBang5;
            }
            result += "}";

            string client_id = DungChung.Bien.xmlFilePath_LIS[41];
            string user = DungChung.Bien.xmlFilePath_LIS[42];
            string password = DungChung.Bien.xmlFilePath_LIS[43];
            string kq=DungChung.Ham.GuiYTCS(client_id, "password", user,password, result);
           // string kq=DungChung.Ham.GuiYTCS("3E46A49F-EE20-4A48-B29C-48B010026E1F", "password", "24012_BV", "Z!rUb3#ehsHL+", result);
            JavaScriptSerializer jss = new JavaScriptSerializer();
            Ketqua kqq = jss.Deserialize<Ketqua>(kq);
            if (kqq.maKetQua==200)
            {
                return true;
            }
            else

            return false;

        }


        public string getNgaySinh(string ngaysinh, string thangsinh, string namsinh)
        {
            try
            {
                string rs = "";


                int ot;
                int ngs = 0, ths = 0, ns = 0;
                if (Int32.TryParse(ngaysinh, out ot))
                    ngs = Convert.ToInt32(ngaysinh);
                if (Int32.TryParse(thangsinh, out ot))
                    ths = Convert.ToInt32(thangsinh);
                if (Int32.TryParse(namsinh, out ot))
                    ns = Convert.ToInt32(namsinh);
                if (namsinh.Trim().Length == 4)
                {
                    if (ngs >= 1 && ngs <= 31 && ths >= 1 && ths <= 12)
                    {
                        rs = ns.ToString() + ths.ToString("D2") + ngs.ToString("D2");
                    }
                    else
                    {
                        //if (ths >= 1 && ths <= 12)
                        //    rs = ns.ToString() + ths.ToString("D2");
                        //else
                        rs = ns.ToString();
                    }
                }
                return rs;
            }
            catch
            {
                return namsinh;
            }
        }
        private static string[] GetICD(string maBenh)
        {
            string _strIcd = "";
            string _strIcdKhac = "";
            int index = maBenh.LastIndexOf(';');
            if (maBenh.Length > 1 && maBenh.Length - 1 == index)
                maBenh = maBenh.Substring(0, maBenh.Length - 1);
            if (maBenh != "")
            {
                string[] _arr = maBenh.Split(';');

                for (int i = 0; i < _arr.Length; i++)
                {
                    if (i == 0)
                        _strIcd = _arr[i];
                    else
                    {
                        if (_arr[i].Length > 1)
                        {

                            if (i == _arr.Length - 1)
                            {
                                _strIcdKhac += _arr[i];
                            }
                            else
                            {
                                _strIcdKhac += _arr[i] + ";";
                            }
                        }
                    }
                }
            }
            return new string[] { _strIcd, _strIcdKhac };
        }
        static int checkThongTuyen(string maNoiKcb, string maDkKcb)
        {
            int ttuyen = 1;
            QLBV_Database.QLBVEntities _db = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var bv = (from a in _db.BenhViens select new { a.MaBV, a.HangBV, a.TuyenBV, a.MaHuyen }).ToList();
            var bvkham = bv.Where(p => p.MaBV == maNoiKcb).Select(p => p).FirstOrDefault();
            var bvdk = bv.Where(p => p.MaBV == maDkKcb).Select(p => p).FirstOrDefault();
            if (bvdk != null && bvkham != null)
            {
                if (bvkham.TuyenBV.Trim() == "C" && (bvdk.TuyenBV.Trim() == "C" || bvdk.TuyenBV.Trim() == "D"))
                {
                    if (bvkham.MaHuyen != bvdk.MaHuyen)
                    {
                        ttuyen = 4;
                    }
                }
            }
            return ttuyen;
        }
        private string getMaKhoa_QD(int? maKP)
        {
            if (maKP == null)
                return "";
            else
            {
                QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                var kp = data.KPhongs.Where(p => p.MaKP == maKP).Select(p => p.MaQD).FirstOrDefault();
                if (kp == null)
                    return "";
                else if (kp.Length < 2)
                    return "";
                else
                    return kp;
            }
        }
        static bool KtraTreEm(DateTime _NgaySinh, DateTime _NgayVao)//Ktra xem bn có nhỏ hơn 1 tuổi
        {
            var ngaySinh = _NgaySinh.Date;
            var ngayVao = _NgayVao.Date;
            if (ngaySinh.AddYears(1) >= ngayVao)
                return true;
            else
                return false;
        }
        string PID;
        private string getBang1(QLBV_Database.QLBVEntities data, BenhNhan bn, TTboXung ttbs, VaoVien vv, RaVien rv, VienPhi vp, List<VienPhict> lvpct, tongtienXML2_XML3 _tongtienXML2_XML3)
        {

            try
            {
                TongHopKBCB moi = new TongHopKBCB();

                stt = 0;

                #region get Object tonghop
                int Demicd = 0, Demcn = 0, demtt = 0;
                if (bn != null && rv != null && vp != null)//kiểm tra các trường dữ liệu 
                {
                    stt++;
                    string IDPerson = bn.IDPerson.ToString();
                    while(IDPerson.Length <13)
                    {
                        IDPerson = "0" + IDPerson;
                    }
                    moi.MA_DINH_DANH_V20 = IDPerson;

                    moi.MA_LK = bn.MaBNhan.ToString();
                    moi.STT = stt.ToString();
                    moi.MA_BN = bn.MaBNhan.ToString();
                    moi.HO_TEN = bn.TenBNhan;
                    moi.NGAY_SINH = getNgaySinh(bn.NgaySinh, bn.ThangSinh, bn.NamSinh);
                    moi.GIOI_TINH = bn.GTinh == 1 ? "1" : (bn.GTinh == 3 ? "3" : "2");
                    moi.DIA_CHI = bn.DChi;
                    string sthe = bn.SThe;
                    string madkbd = bn.MaCS;
                    string hanBHtu = bn.HanBHTu != null ? bn.HanBHTu.Value.ToString("yyyyMMdd") : "";
                    string hanBHden = bn.HanBHDen != null ? bn.HanBHDen.Value.ToString("yyyyMMdd") : "";
                    if (bn.Tuoi < 6)
                    {
                        moi.DOI_TUONG = "5";
                    }
                    else if (bn.DTuong == "BHYT")
                    {
                        moi.DOI_TUONG = "2";
                    }
                    else moi.DOI_TUONG = "1";
                    if (!string.IsNullOrEmpty(ttbs.TTTheBHYTold) && ttbs.TTTheBHYTold.Contains(';'))
                    {
                        string[] ttthemoi = ttbs.TTTheBHYTold.Split(';');
                        string hantu = "";
                        if (ttthemoi.Length >= 5 && !string.IsNullOrEmpty(ttthemoi[5]))
                        {
                            hantu = Convert.ToDateTime(ttthemoi[5]).ToString("yyyyMMdd");
                        }
                        string handen = "";
                        if (ttthemoi.Length >= 6 && !string.IsNullOrEmpty(ttthemoi[6]) && madkbd != ttthemoi[6])
                        {
                            handen = Convert.ToDateTime(ttthemoi[6]).ToString("yyyyMMdd");


                        }
                        if (!string.IsNullOrEmpty(ttthemoi[0]) && ((sthe != ttthemoi[0]) || (hanBHtu != hantu) || (handen != hanBHden) || (madkbd != ttthemoi[7])))
                        {
                            sthe = sthe + ";" + ttthemoi[0];
                            hanBHtu = hanBHtu + ";" + hantu;
                            hanBHden = hanBHden + ";" + handen;
                            madkbd = madkbd + ";" + ttthemoi[7];
                        }

                    }
                    else
                    {
                        sthe = !string.IsNullOrEmpty(bn.SThe) ? bn.SThe : PID;
                        if (bn.DTuong.ToLower() == "dịch vụ") // 22/04/2022 xml1 đang bị sai ngày từ, ngày đến -> tách riêng TH bn dịch vụ mới vào case này
                        {
                            hanBHtu = DungChung.Ham.ngayBHYT(bn.NNhap ?? DateTime.Now);
                            hanBHden = DungChung.Ham.ngayBHYT(rv.NgayRa ?? DateTime.Now);
                        }
                    }
                    moi.MA_THE = sthe;
                    moi.MA_DKBD = !string.IsNullOrEmpty(madkbd) ? madkbd : (DungChung.Bien.MaBV.Substring(0, 2) + "000");
                    moi.GT_THE_TU = hanBHtu;
                    moi.GT_THE_DEN = hanBHden;

                    string miencungct = "";
                    if (bn.NgayHM != null)
                    {
                        if (bn.NgayHM.Value.Year > 1000 && bn.NgayHM.Value.Year < 2999)
                            moi.MIEN_CUNG_CT = bn.NgayHM == null ? "" : (bn.NgayHM.Value.Year + bn.NgayHM.Value.Month.ToString("D2") + bn.NgayHM.Value.Day.ToString("D2"));
                    }

                    moi.TEN_BENH = rv.ChanDoan;
                    moi.MA_BENH = DungChung.Ham.FreshString(GetICD(rv.MaICD)[0].Trim());
                    if (string.IsNullOrEmpty(moi.MA_BENH))
                    {
                        Demicd++;
                    }
                    moi.MA_BENHKHAC = DungChung.Ham.FreshString(GetICD(rv.MaICD)[1].Trim());
                    string malydo = "";
                    if (bn.CapCuu == 1)
                    {
                        malydo = "2";
                    }
                    else
                    {
                        if (bn.Tuyen == 1)
                        {
                            malydo = checkThongTuyen(bn.MaKCB, bn.MaCS).ToString();
                        }
                        else
                        {
                            malydo = "3";
                        }
                    }
                    moi.MA_LYDO_VVIEN = malydo;
                    //moi.ma_noi_chuyen = bn.MaBV;//rv.MaBVC;
                    if (!string.IsNullOrEmpty(bn.MaBV))
                        moi.MA_NOI_CHUYEN = bn.MaBV;
                    else
                        moi.MA_NOI_CHUYEN = "";

                    if (!string.IsNullOrEmpty(rv.MaBVC))
                        moi.MA_NOI_CHUYEN = rv.MaBVC;
                    else
                        moi.MA_NOI_CHUYEN = rv.MaBVC;


                    int? matainan = DungChung.Bien._listTaiNan.Where(p => p.Tenloai == bn.ChuyenKhoa).Select(p => p.Ma9324).FirstOrDefault();
                    moi.MA_TAI_NAN = matainan == null ? "" : matainan.ToString();
                    moi.NGAY_VAO = bn.NNhap.Value.ToString("yyyyMMddHHmm");// khác byt
                    moi.NGAY_RA = rv.NgayRa.Value.ToString("yyyyMMddHHmm");// khác Byt
                    moi.SO_NGAY_DTRI = rv.SoNgaydt.Value.ToString();
                    if (rv.KetQua == null)
                        moi.KET_QUA_DTRI = "2";
                    else if (rv.KetQua == "Khỏi")
                        moi.KET_QUA_DTRI = "1";
                    else if (rv.KetQua == "Đỡ|Giảm")
                        moi.KET_QUA_DTRI = "2";
                    else if (rv.KetQua == "Không T.đổi")
                        moi.KET_QUA_DTRI = "3";
                    else if (rv.KetQua == "Tử vong")
                        moi.KET_QUA_DTRI = "5";
                    else
                        moi.KET_QUA_DTRI = "4";
                    if (rv.Status == null || rv.Status == 2)// ra viện
                        moi.TINH_TRANG_RV = "1";
                    else if (rv.Status == 1)// chuyển viện
                        moi.TINH_TRANG_RV = "2";
                    else
                        moi.TINH_TRANG_RV = rv.Status.Value.ToString();// 3: trốn viện, 4: xin ra viện
                    if (vp.NgayTT.Value >= DateTime.Now)
                    {
                        demtt++;
                    }
                    else
                        moi.NGAY_TTOAN = vp.NgayTT.Value.ToString("yyyyMMddHHmm");// khác BYT


                    moi.T_THUOC = Math.Round(_tongtienXML2_XML3.T_THUOC, 2).ToString();
                    moi.T_VTYT = Math.Round(_tongtienXML2_XML3.T_VTYT, 2).ToString();
                    moi.T_TONGCHI = Math.Round(_tongtienXML2_XML3.T_TONGCHI, 2).ToString();
                    moi.T_BHTT = Math.Round(_tongtienXML2_XML3.T_BHTT, 2).ToString();
                    moi.T_BNCCT = Math.Round(_tongtienXML2_XML3.T_BNCCT, 2).ToString(); ; //4210
                    moi.T_NGUONKHAC = Math.Round(_tongtienXML2_XML3.T_NGUONKHAC, 2).ToString(); ;
                    moi.T_BNTT = Math.Round(_tongtienXML2_XML3.T_BNTT, 2).ToString();
                    double t_ngoaids = 0;
                    var test = (from vpct in lvpct
                                join dv in data.DichVus on vpct.MaDV equals dv.MaDV
                                join ndv in data.NhomDVs on dv.IDNhom equals ndv.IDNhom
                                select new
                                {
                                    vpct.ThanhTien,
                                    ndv.TenNhomCT,
                                    dv.TenDV
                                }).ToList();
                    var checkDV = test.Where(p => p.TenDV.Contains("Thận nhân tạo thường qui") || p.TenDV.Contains("Lọc màng bụng cấp cứu liên tục") || p.TenDV.Contains("Lọc màng bụng chu kỳ"));
                    if (rv.MaICD.Contains("C0") || rv.MaICD.Contains("D0"))
                    {
                        t_ngoaids = test.Sum(p => p.ThanhTien);
                    }
                    else if (checkDV != null && checkDV.Count() > 0)
                    {
                        t_ngoaids = test.Sum(p => p.ThanhTien);
                    }
                    else if (bn.SThe != null && bn.SThe != "" && ((bn.SThe.Substring(0, 2)) == "QN" || (bn.SThe.Substring(0, 2)) == "CA" || (bn.SThe.Substring(0, 2)) == "CY"))
                    {
                        t_ngoaids = test.Sum(p => p.ThanhTien);
                    }
                    else
                    {
                        t_ngoaids = test.Where(p => p.TenNhomCT == "Vận chuyển").Sum(p => p.ThanhTien);
                    }
                    moi.T_NGOAIDS = Math.Round(t_ngoaids, 2).ToString();
                    moi.NAM_QT = vp.NgayTT.Value.Year.ToString();
                    moi.THANG_QT = vp.NgayTT.Value.Month.ToString("D2");
                    if (bn.NoiTru == 1)
                        moi.MA_LOAI_KCB = "3";
                    else if (bn.DTNT == true)
                        moi.MA_LOAI_KCB = "2";
                    else
                        moi.MA_LOAI_KCB = "1";
                    moi.MA_KHOA = getMaKhoa_QD(rv.MaKP);
                    moi.MA_CSKCB = bn.MaKCB;
                    moi.MA_KHUVUC = bn.KhuVuc;

                    if (!string.IsNullOrEmpty(_tongtienXML2_XML3.MA_PTTT_QT))
                        moi.MA_PTTT_QT = _tongtienXML2_XML3.MA_PTTT_QT;
                    else
                        moi.MA_PTTT_QT = "";
                    string Ngaysinh = (string.IsNullOrEmpty(bn.NgaySinh != null ? bn.NgaySinh.Trim() : "") ? "1" : bn.NgaySinh) + "/" + (string.IsNullOrEmpty(bn.ThangSinh != null ? bn.ThangSinh.Trim() : "") ? "1" : bn.ThangSinh) + "/" + (string.IsNullOrEmpty(bn.NamSinh.Trim()) ? "1" : bn.NamSinh);
                    DateTime _ngaysinh = Convert.ToDateTime(Ngaysinh);
                    if (KtraTreEm(_ngaysinh, bn.NNhap.Value))
                    {
                        if (ttbs.CanNang_ChieuCao != null && ttbs.CanNang_ChieuCao.Contains(";"))
                        {
                            if (!string.IsNullOrEmpty(ttbs.CanNang_ChieuCao.Split(';')[0]))
                            {
                                string cn = ttbs.CanNang_ChieuCao.Split(';')[0];
                                if (cn.Contains("."))
                                {

                                    if (cn.Split('.')[1].Length != 2)
                                    {
                                        Demcn++;
                                    }
                                    else
                                    {
                                        moi.CAN_NANG = cn;
                                    }
                                }
                                else
                                {
                                    moi.CAN_NANG = cn;
                                }
                            }
                            else
                            {
                                Demcn++;
                            }
                        }
                        else
                            Demcn++;
                    }

                    return JsonConvert.SerializeObject(moi);
                }
                else return null;

                #endregion


            }
            catch (Exception ex)
            {
                return null;
            }

        }
        private string getbang2(QLBV_Database.QLBVEntities data, BenhNhan bn, TTboXung ttbs, VaoVien vv, RaVien rv, VienPhi vp, List<VienPhict> lvpct, tongtienXML2_XML3 _tongtienXML2_XML3)
        {
            try
            {
                stt = 0;
                List<ChiTietThuocTT> listThuoc = new List<ChiTietThuocTT>();

                var qvpct = (
                             from vpct in data.VienPhicts.Where(p => p.idVPhi == vp.idVPhi)
                             join dv in data.DichVus on vpct.MaDV equals dv.MaDV
                             join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                             join n in data.NhomDVs.Where(p => p.IDNhom == 4 || p.IDNhom == 5 || p.IDNhom == 6 || p.IDNhom == 7) on tn.IDNhom equals n.IDNhom
                             join cb in data.CanBoes on vpct.MaCB equals cb.MaCB into kq
                             from kq1 in kq.DefaultIfEmpty()
                             select new
                             {
                                 vpct.NgayChiPhi,
                                 vpct.TyLeTT,
                                 vpct.TyLeBHTT,
                                 vpct.MaDV,
                                 vpct.SoLuong,
                                 vpct.DonGia,
                                 vpct.ThanhTien,
                                 vpct.TienBH,
                                 vpct.TienBN,
                                 vpct.TrongBH,
                                 vpct.MaKP,
                                 vp.NgayRa,//tạm lấy ngày y lệnh
                                 tn.IdTieuNhom,
                                 n.IDNhom,
                                 n.TenNhomCT,
                                 dv.MaQD,
                                 dv.SoQD,
                                 dv.TenHC,
                                 dv.DonVi,
                                 dv.HamLuong,
                                 dv.MaDuongDung,
                                 dv.SoDK,
                                 dv.TenDV,
                                 dv.MaNhom,
                                 dv.NhomThau,
                                 MaCB = kq1 == null ? "" : kq1.MaCCHN,
                                 dv.DongY
                             }).ToList();




                string malydo = "";
                if (bn.CapCuu == 1)
                {
                    malydo = "2";
                }
                else
                {
                    if (bn.Tuyen == 1)
                    {
                        malydo = checkThongTuyen(bn.MaKCB, bn.MaCS).ToString();
                    }
                    else
                    {
                        malydo = "3";
                    }
                }
                #region get DS thuốc
                int _NoiTru = 0;
                string _KhuVuc = "";
                if (bn != null && rv != null && vp != null)//kiểm tra các trường dữ liệu 
                {
                    string muchuong = "";
                    int hangbv = 0;
                    _NoiTru = bn.NoiTru ?? 0;
                    _KhuVuc = bn.KhuVuc;
                    hangbv = DungChung.Ham.hangBV(DungChung.Bien.MaBV);
                    if (bn.NoiTru == null || bn.Tuyen == null)
                        muchuong = "0";
                    else
                    {
                        var qtylett = qvpct.Where(p => p.TyLeBHTT > 0).ToList();
                        if (qtylett.Count() > 0)
                            muchuong = qvpct.First().TyLeBHTT.ToString();
                        else
                        {
                            muchuong = _getmuc(hangbv, bn.SThe, bn.Tuyen.Value, bn.NoiTru.Value, vp.NgayTT.Value).ToString();
                        }
                    }
                    int kt = 0;
                    int ktthau = 0;
                    int ktMaBS = 0;
                    string dichvuChuaCoMaBS = "";
                    int ktLieuDung = 0, ktsodk = 0;
                    string thuocChuaCoLieuDung = "", chuacosodk = "";
                    string thongtinthau = "";
                    double thanhtienthuoc = 0;
                    foreach (var a in qvpct)
                    {
                        ChiTietThuocTT moi = new ChiTietThuocTT();
                        stt++;
                        moi.MA_LK = bn.MaBNhan.ToString();
                        moi.STT = stt.ToString();
                        if (String.IsNullOrEmpty(a.MaQD))
                        {
                            checkData += a.TenDV + "; ";
                            kt++;
                        }
                        moi.MA_THUOC = String.IsNullOrEmpty(a.MaQD) ? "" : a.MaQD;
                        moi.MA_NHOM = a.IDNhom.ToString();// id nhóm dịch vụ
                        moi.TEN_THUOC = String.IsNullOrEmpty(a.TenDV) ? a.TenHC : a.TenDV; //????? Tên ghi đúng theo danh mục thuốc cục qld công bố
                        moi.DON_VI_TINH = a.DonVi;
                        moi.HAM_LUONG = a.HamLuong;
                        moi.DUONG_DUNG = a.MaDuongDung;
                        moi.LIEU_DUNG = getLieuDung(bn.MaBNhan, a.MaDV ?? 0, a.MaKP ?? 0);
                        if (moi.LIEU_DUNG == "")
                        {
                            thuocChuaCoLieuDung += a.TenDV + ", ";
                            ktLieuDung++;
                        }
                        moi.SO_DANG_KY = a.SoDK;
                        if (moi.SO_DANG_KY == "" && a.DongY != 1) //riêng đông y ko bắt lỗi thiếu sô đăng kí a quý 03-05
                        {
                            if (a.TenDV.ToLower().Contains("ôxy") || a.TenDV.ToLower().Contains("oxy") || a.IDNhom == 7)// || a.DongY == 1 
                            {

                            }
                            else
                            {
                                chuacosodk += a.TenDV + ", ";
                                ktsodk++;
                            }
                        }

                        moi.TT_THAU = a.SoQD + ";" + a.MaNhom + ";" + a.NhomThau; // số quyết định trúng thầu + gói thầu + nhóm thầu
                        if (a.DongY != 1) //thuốc đông y ko cần check lỗi a quý 03-05
                        {
                            if ((string.IsNullOrEmpty(a.SoQD) || string.IsNullOrEmpty(a.MaNhom) || string.IsNullOrEmpty(a.NhomThau)) && (a.IDNhom != 7))
                            {
                                moi.TT_THAU = "";
                                thongtinthau += a.TenDV + ", ";
                                ktthau++;
                            }
                        }
                        moi.PHAM_Vl = "1";
                        double tyleBHtt = Math.Round(a.TyLeBHTT / 100, 2);
                        double tylett = Math.Round(a.TyLeTT / 100, 2);
                        double soluong = 0, dongia = 0, thanhtien = 0, bhtt = 0, bntt = 0, bncct = 0;
                        soluong = Math.Round(a.SoLuong, 2);// a.SoLuong; //(Math.Round(a.SoLuong, 2));
                        dongia = Math.Round(a.DonGia, 3);//a.DonGia; //(Math.Round(a.DonGia, 2));
                        thanhtien = Round_custom(Math.Round(soluong * dongia, 4));
                        if (a.TrongBH == 1)
                        {
                            //bhtt = Math.Round((thanhtien * tylett * tyleBHtt), 2, MidpointRounding.AwayFromZero); //làm tròn đến số thập phân thứ 2 VD 0,5=>1
                            bhtt = Round_custom(Math.Round(thanhtien * tylett * tyleBHtt, 4));
                            if (malydo == "3")
                            {
                                if (_NoiTru == 1)
                                {
                                    if (_KhuVuc != null && _KhuVuc.ToLower().Contains("k"))
                                    {
                                        bncct = Round_custom(Math.Round(thanhtien * tylett * 100 / 100, 4) - bhtt);
                                    }
                                    else
                                    {
                                        if (hangbv == 1)
                                        {
                                            bncct = Round_custom(Math.Round(thanhtien * tylett * 40 / 100, 4) - bhtt);
                                        }
                                        else if (hangbv == 2)
                                        {
                                            bncct = Round_custom(Math.Round(thanhtien * tylett * 60 / 100, 4) - bhtt);
                                        }
                                        else
                                            bncct = 0;
                                    }
                                }
                                else
                                {
                                    bncct = 0;
                                }
                            }
                            else
                            {
                                bncct = Round_custom(Math.Round(thanhtien * tylett, 4) - bhtt);
                            }
                            bntt = Round_custom(thanhtien - bhtt - bncct);
                        }
                        else
                        {
                            bntt = thanhtien;
                        }


                        // moi.SO_LUONG = a.SoLuong;
                        moi.SO_LUONG = soluong.ToString("G", CultureInfo.InvariantCulture);
                        moi.DON_GIA = dongia.ToString("G", CultureInfo.InvariantCulture);
                        moi.TYLE_TT = a.TyLeTT.ToString();
                        // 
                        _tongtienXML2_XML3.T_THUOC += thanhtien;
                        _tongtienXML2_XML3.T_TONGCHI += thanhtien;
                        _tongtienXML2_XML3.T_BNTT += bntt;
                        _tongtienXML2_XML3.T_BHTT += bhtt;
                        _tongtienXML2_XML3.T_BNCCT += bncct;
                        _tongtienXML2_XML3.T_NGOAIDS += 0;
                        //
                        moi.THANH_TIEN = (thanhtien).ToString("G", CultureInfo.InvariantCulture);// (soluong * dongia * tylett).ToString("G", CultureInfo.InvariantCulture); //a.ThanhTien.ToString("G", CultureInfo.InvariantCulture);
                        moi.MUC_HUONG = muchuong;

                        moi.T_BHTT = (bhtt).ToString("G", CultureInfo.InvariantCulture);//4210
                        moi.T_BNCCT = (bncct).ToString("G", CultureInfo.InvariantCulture);//4210

                        moi.T_BNTT = (bntt).ToString("G", CultureInfo.InvariantCulture);//(Math.Round((soluong * dongia * tylett * (1 - tyleBHtt)), 2)).ToString("G", CultureInfo.InvariantCulture);//4210
                        moi.T_NGUON_KHAC = "0";//4210
                        moi.T_NGOAIDS = "0";//4210: T_NGOAIDS = T_BHTT đối với các chi phí ngoài định suất, làm tròn số đến 2 chữ số thập phân, Sử dụng dấu Chấm (".") để phân cách giữa số Nguyên (hàng đơn vị) với số thập phân đầu tiên.



                        string makhoa = getMaKhoa_QD(a.MaKP);// mã kpQD
                        if (String.IsNullOrEmpty(makhoa))
                        {
                            checkData += a.TenDV + "; ";
                            kt++;
                        }
                        moi.MA_KHOA = makhoa;
                        moi.MA_BAC_SI = a.MaCB;
                        if (string.IsNullOrEmpty(moi.MA_BAC_SI))
                            moi.MA_BAC_SI = getMaBacSy(bn.MaBNhan, a.MaDV, a.MaKP);
                        if (moi.MA_BAC_SI == "")
                        {
                            dichvuChuaCoMaBS += a.TenDV + ", ";
                            ktMaBS++;
                        }
                        moi.MA_BENH = DungChung.Ham.FreshString(rv.MaICD.Trim());
                        moi.NGAY_YL = a.NgayChiPhi.ToString("yyyyMMddHHmm");// đang tạm lấy bằng ra viện
                        moi.MA_PTTT = "1";// Chưa có dữ liệu để lấy nên fix cứng
                        listThuoc.Add(moi);
                    }
                    if (kt > 0)
                    {
                        checkData += " chưa có mã dùng chung hoặc mã khoa. ";
                        return null;
                    }
                    if (ktMaBS > 0)
                    {
                        checkData += dichvuChuaCoMaBS + " chưa có mã bác sỹ. ";
                        return null;
                    }
                    if (ktLieuDung > 0)
                    {
                        checkData += thuocChuaCoLieuDung + " chưa có liều dùng. ";
                        return null;
                    }
                    if (ktthau > 0)
                    {
                        checkData += thongtinthau + " chưa có đầy đủ thông tin thầu. ";
                        return null;
                    }
                    if (ktsodk > 0)
                    {
                        checkData += chuacosodk + " chưa có số đăng ký.  ";
                        return null;
                    }
                    return JsonConvert.SerializeObject(listThuoc);

                }
                else return null;

                #endregion

            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Lỗi tạo file XML2: " + ("Exception: " + ex.Message + Environment.NewLine + (ex.InnerException != null ? " InnerException:" + ex.InnerException.Message : "")));
                return null;
            }


        }
        private string getLieuDung(int mabn, int MaDV, int MaKP)
        {

            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            string LieuDung = "";
            var qdt = (from dt in data.DThuocs.Where(p => p.MaBNhan == mabn).Where(p => p.PLDV == 1)
                       join dtct in data.DThuoccts.Where(p => p.MaDV == MaDV).Where(p => p.Luong != null && p.SoLan != null) on dt.IDDon equals dtct.IDDon
                       select dtct).FirstOrDefault();
            if (qdt != null)
                LieuDung = qdt.Luong + qdt.DviUong + "/lần" + "*" + qdt.SoLan + "lần/ngày";
            return LieuDung;

        }
        public double Round_custom(double a)
        {

            double kq = Math.Round(a, 2, MidpointRounding.AwayFromZero);
            string z = a.ToString();
            if (z.Contains('.'))
            {
                string[] arr_z = z.Split('.');
                if (arr_z[1].Length > 2)
                {
                    double so3 = Convert.ToDouble(arr_z[1].Substring(2, 1));
                    double so2 = Convert.ToDouble(arr_z[1].Substring(1, 1));
                    if (so3 == 5)
                    {
                        kq = Math.Round(a + 0.001, 2);
                    }
                }
            }
            return kq;
        }

        private string getMaBacSy(int maBN, int? maDV, int? maKP)
        {
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var qdtct = (from dthuoc in data.DThuocs.Where(p => p.MaBNhan == maBN)
                         join dtct in data.DThuoccts.Where(p => p.MaDV == maDV) on dthuoc.IDDon equals dtct.IDDon
                         join cb in data.CanBoes on dthuoc.MaCB equals cb.MaCB
                         select new { dthuoc.MaKP, cb.MaCCHN, cb.MaCB }).ToList();

            if (qdtct.Count == 0)
            {
                return "";
            }
            else
            {
                var qdtct1 = (from dt in qdtct.Where(p => p.MaKP == maKP) select dt).FirstOrDefault();// chỗ này cần lấy theo mã số chứng chỉ hành nghề
                if (qdtct1 != null)
                {
                    if (string.IsNullOrEmpty(qdtct1.MaCCHN))
                        return "";
                    else
                        return qdtct1.MaCCHN.ToString();
                }
                else
                {
                    if (string.IsNullOrEmpty(qdtct.First().MaCCHN))
                        return "";
                    else
                        return qdtct.First().MaCCHN.ToString();
                }
            }
        }
        private bool getMaNhom(string Manhom)
        {
            bool kt = true;
            foreach (char item in Manhom)
            {
                if (!char.IsDigit(item))
                    kt = false;
            }
            return kt;
        }
        private string getBang3(QLBV_Database.QLBVEntities data, BenhNhan bn, TTboXung ttbs, VaoVien vv, RaVien rv, VienPhi vp, List<VienPhict> lvpct, ref tongtienXML2_XML3 _tongtienXML2_XML3)
        {

            try
            {
                List<ChiTietDVKTvaVTYT> listDV = new List<ChiTietDVKTvaVTYT>();
                #region lấy ra thoong tin chung
                stt = 0;

                var qvpct1 = (from vpct in data.VienPhicts.Where(p => p.idVPhi == vp.idVPhi && p.TrongBH == 1) // lấy dv chạy thận (ID nhóm = 8 )
                              join dv in data.DichVus.Where(p => p.TenDV.Contains("Thận nhân tạo thường qui")) on vpct.MaDV equals dv.MaDV
                              select new
                              {
                                  dv.TenDV,
                              }).ToList();
                var qvpct = (
                             from vpct in data.VienPhicts.Where(p => p.idVPhi == vp.idVPhi && p.TrongBH == 1)
                             join dv in data.DichVus on vpct.MaDV equals dv.MaDV
                             join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                             join n in data.NhomDVs on tn.IDNhom equals n.IDNhom
                             join cb in data.CanBoes on vpct.MaCB equals cb.MaCB into kq
                             from kq1 in kq.DefaultIfEmpty()
                             where (dv.PLoai == 2 || dv.PLoai == 3 || (dv.PLoai == 1 && (n.IDNhom == 10 || n.IDNhom == 11)))
                             select new
                             {
                                 dv.MaNhom5937,
                                 vpct.NgayChiPhi,
                                 vpct.NgayYLenh,
                                 vp.NgayRa,//tạm lấy ngày y lệnh
                                 dv.BHTT,
                                 vpct.TyLeTT,
                                 vpct.TyLeBHTT,
                                 vpct.MaDV,
                                 vpct.SoLuong,
                                 vpct.DonGia,
                                 vpct.ThanhTien,
                                 vpct.TienBH,
                                 vpct.TienBN,
                                 vpct.TrongBH,
                                 vpct.MaKP,
                                 tn.IdTieuNhom,
                                 n.IDNhom,
                                 n.TenNhomCT,
                                 MaQD = dv.MaQD ?? "",
                                 dv.TenRG,
                                 dv.TenDV,
                                 dv.SoQD,
                                 dv.TenHC,
                                 dv.DonVi,
                                 dv.HamLuong,
                                 dv.MaDuongDung,
                                 dv.SoDK,
                                 dv.PLoai,
                                 dv.NgayQD,
                                 dv.MaNhom,
                                 dv.NhomThau,
                                 dv.GiaBHGioiHanTT,
                                 MaCB = kq1 == null ? "" : kq1.MaCCHN

                             }).ToList();

                var phauthuat_thuthuat = (from a in qvpct where a.MaNhom5937 == 8 select new { a.MaDV, a.MaQD, a.MaNhom }).ToList();

                _tongtienXML2_XML3.MA_PTTT_QT = string.Join(";", phauthuat_thuthuat.Where(p => p.MaNhom != null && p.MaNhom != "").Select(p => p.MaNhom).ToArray());

                #endregion
                string malydo = "";
                if (bn.CapCuu == 1)
                {
                    malydo = "2";
                }
                else
                {
                    if (bn.Tuyen == 1)
                    {
                        malydo = checkThongTuyen(bn.MaKCB, bn.MaCS).ToString();
                    }
                    else
                    {
                        malydo = "3";
                    }
                }
                #region get DS Dịch vụ
                if (bn != null && rv != null && vp != null)//kiểm tra các trường dữ liệu 
                {
                    int kt = 0;
                    int ktMaBS = 0;
                    string dichvuChuaCoMaBS = "";
                    int ktgiuong = 0, ktragiuongnew = 0;
                    string muchuong = "", _khuvuc = "";
                    int hangbv = 0, _noitru = 0;
                    _khuvuc = bn.KhuVuc;
                    _noitru = bn.NoiTru ?? 0;
                    hangbv = DungChung.Ham.hangBV(QLBV.DungChung.Bien.MaBV);

                    var qVTYT0 = (from dt in data.DThuocs.Where(p => p.MaBNhan == bn.MaBNhan)
                                  join dtct in data.DThuoccts.Where(p => p.AttachIDDonct != null && p.AttachIDDonct > 0) on dt.IDDon equals dtct.IDDon
                                  select dtct).ToList();
                    var qVTYT1 = (from a in qVTYT0 group a by new { a.AttachIDDonct } into kq select new { kq.Key.AttachIDDonct }).OrderBy(p => p.AttachIDDonct).ToList();
                    int num = 1;
                    var qVTYT2 = (from a in qVTYT1 select new { STT = num++, a.AttachIDDonct }).ToList();
                    int ktthau = 0;
                    string thongtinthau = "";
                    var bvBD = data.BenhViens.FirstOrDefault(o => o.MaBV == bn.MaCS);
                    var bv = data.BenhViens.FirstOrDefault(o => o.MaBV == DungChung.Bien.MaBV);
                    foreach (var a in qvpct)
                    {
                        if (bn.NoiTru == null || bn.Tuyen == null)
                            muchuong = "0";
                        else
                        {
                            var qtylett = a.TyLeBHTT;
                            if (qtylett > 0)
                                muchuong = qtylett.ToString();
                            else
                            {

                                muchuong = _getmuc(hangbv, bn.SThe, bn.Tuyen.Value, bn.NoiTru.Value, vp.NgayTT.Value).ToString();
                            }
                        }
                        ChiTietDVKTvaVTYT moi = new ChiTietDVKTvaVTYT();
                        stt++;

                        moi.MA_LK = bn.MaBNhan.ToString();
                        moi.STT = stt.ToString();
                        if (String.IsNullOrEmpty(a.MaQD))
                        {
                            checkData += a.TenDV + "; ";
                            kt++;
                        }
                        moi.MA_DICH_VU = a.MaQD == null ? "" : a.MaQD;
                        moi.MA_VAT_TU = "";

                        moi.TEN_DICH_VU = (a.PLoai == 1 && DungChung.Bien.MaBV != "30007") ? a.TenHC : a.TenDV; //????? Tên ghi đúng theo danh mục thuốc cục qld công bố
                        moi.TEN_VAT_TU = "";

                        if (a.MaNhom5937 == 10 || a.MaNhom5937 == 11)
                        {

                            if (a.TrongBH == 1)
                            {
                                moi.MA_DICH_VU = "";
                                moi.MA_VAT_TU = a.MaQD == null ? "" : a.MaQD; //?????? mã vật tư sử dụng quy định tại bộ mã danh mục dùng chung của bộ y tế, chỉ ghi các vật tư chưa có trong cơ cấu giá dịch vụ ??????

                                moi.TEN_DICH_VU = "";
                                moi.TEN_VAT_TU = a.TenDV;
                            }
                            else
                            {
                                // lấy mã của phẫu thuật
                                if (phauthuat_thuthuat.Count > 0)
                                    moi.MA_DICH_VU = phauthuat_thuthuat.First().MaQD;
                            }

                            if (qVTYT2.Count > 0)
                            {
                                var qVTYT3 = (from dt in qVTYT0.Where(p => p.MaDV == a.MaDV)
                                              join iddon in qVTYT2 on dt.AttachIDDonct equals iddon.AttachIDDonct
                                              group iddon by new { iddon.STT } into kq
                                              select new { GoiVTYT = "G" + kq.Key.STT }).OrderBy(p => p.GoiVTYT).ToList();

                                moi.GOI_VTYT = string.Join(";", qVTYT3.Select(p => p.GoiVTYT).ToArray());//Lấy ra gói vật tư y tế (vật tư y tế dính kem, được kê cho các dịch vụ có iddonct là (attackIddonct)
                            }
                            string _manhom = "", _soqd = "";
                            if (!string.IsNullOrEmpty(a.MaNhom) && a.MaNhom.Trim().Length <= 2)
                            {
                                if (getMaNhom(a.MaNhom))
                                    _manhom = a.MaNhom;
                            }
                            if (!string.IsNullOrEmpty(a.SoQD))
                                _soqd = a.SoQD;
                            if (a.NgayQD != null && _manhom != "" && _soqd != "")
                            {
                                moi.TT_THAU = a.NgayQD.Value.Year.ToString() + "." + _manhom + "." + _soqd;

                            }
                            else
                                moi.TT_THAU = "";
                            if (moi.TT_THAU == "")
                            {
                                thongtinthau += a.TenDV + ", ";
                                ktthau++;
                            }
                        }
                        else
                            moi.TT_THAU = "";

                        if (a.IdTieuNhom == 22 && a.MaNhom5937 == 80000000000)
                        {
                            moi.MA_NHOM = "18";
                        }
                        else
                        {
                            moi.MA_NHOM = a.MaNhom5937.ToString();
                        }

                        moi.DON_VI_TINH = a.DonVi;
                        moi.PHAM_VI = "1";
                        double tyleBHtt = a.TyLeBHTT / 100;
                        double tylett = a.TyLeTT / 100;
                        double soluong = 0, dongia = 0, thanhtien = 0, bhtt = 0, bntt = 0, bncct = 0;
                        soluong = Math.Round(a.SoLuong, 2);
                        dongia = Math.Round(a.DonGia, 3);
                        if (a.MaNhom5937 == 13 || a.MaNhom5937 == 15 || a.MaNhom5937 == 8)
                        {
                            thanhtien = Round_custom(Math.Round(soluong * dongia * tylett, 4)); //Round_custom(Math.Round(soluong * dongia * tylett, 4));
                            if (a.TrongBH == 1)
                            {
                                bhtt = Round_custom(Math.Round((thanhtien * tyleBHtt), 4)); //làm tròn đến số thập phân thứ 2 VD 0,5=>1
                                if (malydo == "3")
                                {
                                    if ((!string.IsNullOrWhiteSpace(bn.SThe) && bvBD != null && DungChung.Bien.MaBV.Substring(0, 2) != bvBD.MaTinh.Trim() && bv != null && bv.TuyenBV != null && bv.TuyenBV.Trim() == "C") || (!string.IsNullOrWhiteSpace(bn.SThe) && bvBD != null && DungChung.Bien.MaBV.Substring(0, 2) == bvBD.MaTinh.Trim() && (bvBD.TuyenBV == "A" || bvBD.TuyenBV == "B")))
                                    {
                                        bncct = Round_custom(Math.Round(thanhtien, 4) - bhtt);
                                    }
                                    else
                                    {
                                        if (_noitru == 1 || _noitru != 1)
                                        {
                                            if (a.NgayChiPhi >= DateTime.Parse("01/01/2021"))
                                                bncct = Round_custom(Math.Round(thanhtien * 100 / 100, 2) - bhtt);
                                            else
                                            {
                                                if (_khuvuc != null && _khuvuc.ToLower().Contains("k"))
                                                {
                                                    bncct = Round_custom(Math.Round(thanhtien * 100 / 100, 2) - bhtt);
                                                }
                                                else
                                                {
                                                    if (hangbv == 1)
                                                    {
                                                        bncct = Round_custom(Math.Round(thanhtien * 40 / 100, 2) - bhtt);
                                                    }
                                                    else if (hangbv == 2)
                                                    {
                                                        bncct = Round_custom(Math.Round(thanhtien * 60 / 100, 2) - bhtt);
                                                    }
                                                    else
                                                    {
                                                        bncct = 0;
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (DungChung.Bien.MaBV == "30303")
                                                bncct = Round_custom(Math.Round(thanhtien, 2) - bhtt);
                                            else
                                                bncct = 0;
                                        }
                                    }
                                }
                                else
                                {
                                    bncct = Round_custom(Math.Round(thanhtien, 2) - bhtt);
                                }
                                bntt = Round_custom(thanhtien - bhtt - bncct);
                            }
                            else
                            {
                                bntt = thanhtien;
                            }
                        }
                        else
                        {
                            thanhtien = Round_custom(Math.Round(soluong * dongia * tylett, 4));
                            if (a.TrongBH == 1)
                            {
                                if (DungChung.Bien.MaBV == "27022" && a.GiaBHGioiHanTT > 0)
                                {
                                    double ttbh = Round_custom(Math.Round(soluong * a.GiaBHGioiHanTT, 4));
                                    bhtt = Round_custom(Math.Round((ttbh * tyleBHtt * tylett), 4)); //làm tròn đến số thập phân thứ 2 VD 0,5=>1
                                    if (malydo == "3")
                                    {
                                        if (_noitru == 1)
                                        {
                                            if (a.NgayChiPhi >= DateTime.Parse("01/01/2021"))
                                                bncct = Round_custom(Math.Round(ttbh * tylett * 100 / 100, 2) - bhtt);
                                            else
                                            {
                                                if (_khuvuc != null && _khuvuc.ToLower().Contains("k"))
                                                {
                                                    bncct = Round_custom(Math.Round(ttbh * tylett * 100 / 100, 2) - bhtt);
                                                }
                                                else
                                                {
                                                    if (hangbv == 1)
                                                    {
                                                        bncct = Round_custom(Math.Round(ttbh * tylett * 40 / 100, 2) - bhtt);
                                                    }
                                                    else if (hangbv == 2)
                                                    {
                                                        bncct = Round_custom(Math.Round(ttbh * tylett * 60 / 100, 2) - bhtt);
                                                    }
                                                    else
                                                    {
                                                        bncct = 0;
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            bncct = 0;
                                        }

                                    }

                                    else
                                    {
                                        bncct = Round_custom(Math.Round(ttbh * tylett, 2) - bhtt);
                                    }
                                    bntt = Round_custom(thanhtien - bhtt - bncct);
                                }
                                else
                                {
                                    bhtt = Round_custom(Math.Round((thanhtien * tyleBHtt), 4)); //làm tròn đến số thập phân thứ 2 VD 0,5=>1
                                    if (malydo == "3")
                                    {
                                        if ((!string.IsNullOrWhiteSpace(bn.SThe) && bvBD != null && DungChung.Bien.MaBV.Substring(0, 2) != bvBD.MaTinh.Trim() && bv != null && bv.TuyenBV != null && bv.TuyenBV.Trim() == "C") || (!string.IsNullOrWhiteSpace(bn.SThe) && bvBD != null && DungChung.Bien.MaBV.Substring(0, 2) == bvBD.MaTinh.Trim() && (bvBD.TuyenBV == "A" || bvBD.TuyenBV == "B")))
                                        {
                                            bncct = Round_custom(Math.Round(thanhtien * 100 / 100, 4) - bhtt);
                                        }
                                        else
                                        {
                                            if (_noitru == 1 || _noitru != 1)
                                            {
                                                if (a.NgayChiPhi >= DateTime.Parse("01/01/2021"))
                                                    bncct = Round_custom(Math.Round(thanhtien * 100 / 100, 2) - bhtt);
                                                else
                                                {
                                                    if (_khuvuc != null && _khuvuc.ToLower().Contains("k"))
                                                    {
                                                        bncct = Round_custom(Math.Round(thanhtien * 100 / 100, 2) - bhtt);
                                                    }
                                                    else
                                                    {
                                                        if (hangbv == 1)
                                                        {
                                                            bncct = Round_custom(Math.Round(thanhtien * 40 / 100, 2) - bhtt);
                                                        }
                                                        else if (hangbv == 2)
                                                        {
                                                            bncct = Round_custom(Math.Round(thanhtien * 60 / 100, 2) - bhtt);
                                                        }
                                                        else
                                                        {
                                                            bncct = 0;
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                bncct = 0;
                                            }
                                        }
                                    }

                                    else
                                    {
                                        bncct = Round_custom(Math.Round(thanhtien, 2) - bhtt);
                                    }
                                    bntt = Round_custom(thanhtien - bhtt - bncct); //Math.Round(thanhtien - bhtt - bncct, 2);
                                }
                            }
                            else
                            {
                                bntt = thanhtien;
                            }
                        }
                        // 
                        if (a.MaNhom5937 == 10 || a.MaNhom5937 == 11)
                        {
                            _tongtienXML2_XML3.T_VTYT += thanhtien;
                        }

                        _tongtienXML2_XML3.T_TONGCHI += thanhtien;
                        _tongtienXML2_XML3.T_BNTT += bntt;
                        _tongtienXML2_XML3.T_BHTT += bhtt;
                        _tongtienXML2_XML3.T_BNCCT += bncct;
                        //_tongtienXML2_XML3.T_NGOAIDS += 0;
                        //
                        moi.SO_LUONG = (soluong).ToString("G", CultureInfo.InvariantCulture);
                        moi.DON_GIA = (dongia).ToString("G", CultureInfo.InvariantCulture);

                        moi.TT_THAU = (a.NgayQD == null ? "" : a.NgayQD.Value.Year.ToString()) + "." + a.MaNhom + "." + a.SoQD;
                        if (a.NgayQD == null || string.IsNullOrEmpty(a.MaNhom) || string.IsNullOrEmpty(a.SoQD))
                        {
                            moi.TT_THAU = "";
                            if ((a.MaNhom5937 == 10 || a.MaNhom5937 == 11) && moi.TT_THAU == "")
                            {
                                thongtinthau += a.TenDV + ", ";
                                ktthau++;
                            }
                        }
                        moi.TYLE_TT = a.TyLeTT == 0 ? "100" : a.TyLeTT.ToString();
                        moi.THANH_TIEN = (thanhtien).ToString("G", CultureInfo.InvariantCulture);
                        moi.T_TRANTT = DungChung.Bien.MaBV == "27022" ? (a.GiaBHGioiHanTT).ToString("G", CultureInfo.InvariantCulture) : "0";
                        moi.MUC_HUONG = muchuong;
                        moi.T_NGUONKHAC = "0";//4210
                        moi.T_BNTT = (bntt).ToString("G", CultureInfo.InvariantCulture);
                        moi.T_BHTT = (bhtt).ToString("G", CultureInfo.InvariantCulture);//4210
                        moi.T_BNCCT = (bncct).ToString("G", CultureInfo.InvariantCulture);//4210 

                        if (rv.MaICD.Contains("C0") || rv.MaICD.Contains("D0"))
                        {
                            moi.T_NGOAlDS = a.ThanhTien.ToString();
                        }
                        else if (a.TenDV.Contains("Thận nhân tạo thường qui") || a.TenDV.Contains("Lọc màng bụng cấp cứu liên tục") || a.TenDV.Contains("Lọc màng bụng chu kỳ"))
                        {
                            moi.T_NGOAlDS = a.ThanhTien.ToString();
                        }
                        else if ((bn.SThe.Substring(0, 2)) == "QN" || (bn.SThe.Substring(0, 2)) == "CA" || (bn.SThe.Substring(0, 2)) == "CY")
                        {
                            moi.T_NGOAlDS = a.ThanhTien.ToString();
                        }
                        else if (a.TenNhomCT == "Vận chuyển")
                        {
                            moi.T_NGOAlDS = a.ThanhTien.ToString();
                        }
                        else if (qvpct1.Count > 0)
                        {
                            moi.T_NGOAlDS = a.ThanhTien.ToString();
                        }
                        string makhoa = getMaKhoa_QD(a.MaKP);// mã kpQD
                        if (String.IsNullOrEmpty(makhoa))
                        {
                            checkData += a.TenDV + "; ";
                            kt++;
                        }
                        moi.MA_KHOA = makhoa;
                        var qbnkb = data.BNKBs.Where(p => p.MaBNhan == bn.MaBNhan && p.Giuong != null && p.Giuong != "").Select(p => p.Giuong).ToList();
                        string giuong = string.Join(";", qbnkb);
                        if (giuong.Length > 14)
                        {
                            ktgiuong++;
                        }
                        else
                            moi.MA_GIUONG = giuong; // Lấy mã giường trong bảng bn khám bệnh
                        if (bn.NoiTru == 1 && string.IsNullOrEmpty(giuong))
                        {
                            ktragiuongnew++;
                        }
                        moi.MA_BAC_SI = a.MaCB;
                        if (string.IsNullOrEmpty(moi.MA_BAC_SI))
                            moi.MA_BAC_SI = getMaBacSy(bn.MaBNhan, a.MaDV, a.MaKP);
                        if (moi.MA_BAC_SI == "")
                        {
                            dichvuChuaCoMaBS += a.TenDV + "; ";
                            ktMaBS++;
                        }
                        moi.MA_BENH = DungChung.Ham.FreshString(rv.MaICD.Trim());
                        moi.NGAY_YL = a.NgayYLenh.Value.ToString("yyyyMMddHHmm");
                        moi.NGAY_KQ = a.NgayChiPhi.ToString("yyyyMMddHHmm");
                        moi.MA_PTTT = "1";
                        listDV.Add(moi);
                    }
                    if (kt > 0)
                    {
                        checkData += " chưa có mã dùng chung hoặc mã khoa. ";

                    }
                    if (ktMaBS > 0)
                    {
                        checkData += dichvuChuaCoMaBS + " chưa có mã bác sỹ. ";

                    }
                    if (ktthau > 0)
                    {
                        checkData += thongtinthau + " chưa có đầy đủ hoặc sai thông tin thầu. ";

                    }
                    if (ktgiuong > 0)
                    {
                        checkData += " MA_GIUONG nhiều hơn 15 kí tự";

                    }
                    if (ktragiuongnew > 0)
                    {
                        checkData += " MA_GIUONG không được để trống";

                    }
                    return JsonConvert.SerializeObject(listDV);
                }
                else return null;
                #endregion
                if (listDV == null)
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Lỗi tạo file XML3: " + ex.Message);
                return null;
            }

        }
        private string getBang4(QLBV_Database.QLBVEntities data, BenhNhan bn, TTboXung ttbs, VaoVien vv, RaVien rv, VienPhi vp)
        {
            try
            {

                #region lấy thông tin chung
                int stt = 0;
                List<ChiSoKetQuaCLS> listcls = new List<ChiSoKetQuaCLS>();
                var _ldv = (from dv in data.DichVus
                            join dvct in data.DichVucts on dv.MaDV equals dvct.MaDV
                            join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                            join nhom in data.NhomDVs on tn.IDNhom equals nhom.IDNhom
                            select new { dv.MaDV, tn.TenRG, nhom.TenNhom, nhom.TenNhomCT, dv.MaQD, dvct.MaDVct, dvct.TenDVct, dv.TenDV }).ToList();
                var _lcls = (from cls in data.CLS.Where(p => p.MaBNhan == bn.MaBNhan)
                             join cd in data.ChiDinhs.Where(p => p.TrongBH == 1 && p.Status == 1) on cls.IdCLS equals cd.IdCLS
                             join clsct in data.CLScts on cd.IDCD equals clsct.IDCD
                             select new { cd.MaDV, cls.NgayTH, cd.MaMay, cd.IDCD, cd.KetLuan, clsct.KetQua, clsct.MaDVct }).ToList();
                var qcls = (from dv in _ldv
                            join cls in _lcls on dv.MaDVct equals cls.MaDVct
                            select new
                            {
                                dv.MaQD,
                                dv.MaDVct,
                                dv.TenDVct,
                                dv.TenNhom,
                                cls.KetLuan,
                                cls.KetQua,
                                cls.MaMay,
                                cls.NgayTH,
                                dv.TenDV
                            }).OrderByDescending(p => p.NgayTH).Distinct().ToList();

                #endregion
                #region get DS CLS
                if (bn != null && rv != null && vp != null)//kiểm tra các trường dữ liệu 
                {
                    int ktmaqd = 0, ktmamay = 0, ktngayth = 0;
                    string ktmaqds = "", ktmamays = "", ktngayths = "";
                    // var qdv = data.DichVus.ToList();
                    foreach (var a in qcls)
                    {
                        stt++;
                        ChiSoKetQuaCLS moi = new ChiSoKetQuaCLS();
                        moi.MA_LK = bn.MaBNhan.ToString();
                        moi.STT = stt.ToString();
                        if (String.IsNullOrEmpty(a.MaQD))
                        {
                            ktmaqds += a.TenDV + "; ";
                            ktmaqd++;
                        }
                        else
                        {
                            moi.MA_DICH_VU = a.MaQD;
                        }
                        //if (a.TenNhom.ToLower().Contains("phẫu thuật") || a.TenNhom.ToLower().Contains("thủ thuật"))
                        //{
                        //    moi.ma_may = "";
                        //}
                        //else
                        //{
                        //    if (string.IsNullOrEmpty(a.MaMay))
                        //    {
                        //        ktmamay++;
                        //        ktmamays += a.TenDV + "; ";
                        //    }
                        //    else
                        //        moi.ma_may = a.MaMay;
                        //}
                        moi.MA_MAY = a.MaMay;
                        moi.MA_CHI_SO = a.MaDVct;
                        moi.TEN_CHI_SO = a.TenDVct;
                        if (a.NgayTH != null)
                            moi.NGAY_KQ = a.NgayTH.Value.ToString("yyyyMMddHHmm");
                        else
                        {
                            ktngayths = a.TenDV + "; ";
                            ktngayth++;
                        }
                        if (a.TenNhom.Contains("Xét nghiệm"))
                        {

                            moi.GIA_TRI = a.KetQua;

                            moi.MO_TA = "";
                            moi.KET_LUAN = "";

                        }
                        else
                        {
                            moi.GIA_TRI = "";
                            moi.MO_TA = a.KetQua;
                            moi.KET_LUAN = a.KetLuan;
                        }
                        listcls.Add(moi);
                    }
                    if (ktmaqd > 0)
                    {
                        checkData += ktmaqds + " chưa có mã dùng chung";

                    }
                    //if(ktmamay>0)
                    //{
                    //    checkData += ktmamays + " chưa có mã máy";//tạm bỏ mã máy để ktra lỗi trên cổng nếu ko có mã máy
                    //    return null;
                    //}
                    if (ktngayth > 0)
                    {
                        checkData += ktngayths + " chưa có ngày thực hiện";

                    }

                    #endregion
                    return JsonConvert.SerializeObject(listcls);
                }
                else return null;


            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Lỗi tạo file XML4: " + ex.Message);
                return null;

            }
        }
        private string getBang5(QLBV_Database.QLBVEntities data, BenhNhan bn, TTboXung ttbs, VaoVien vv, RaVien rv, VienPhi vp)
        {
            try
            {
                #region lấy thông tin chung
                stt = 0;
                List<TheoDoiDienBienLS> listDienBien = new List<TheoDoiDienBienLS>();
                var _lDienBien = data.DienBiens.Where(p => p.MaBNhan == bn.MaBNhan).Where(p => p.Ploai == 0 || p.Ploai == 99).ToList();
                var _lBBHC = data.BBHCs.Where(p => p.MaBNhan == bn.MaBNhan).ToList();
                List<DIENBIEN> _lkq = new List<DIENBIEN>();

                if (DungChung.Bien.MaBV == "20001")
                {
                    var NgayYLenh = (from a in _lDienBien
                                     group a by new { a.NgayNhap.Value.Date } into kq
                                     select new
                                     {
                                         kq.Key.Date,
                                     }).Distinct().ToList();
                    foreach (var item1 in NgayYLenh)
                    {
                        var a = _lDienBien.Where(p => p.NgayNhap.Value.Date == item1.Date).ToList();
                        if (a.Count > 0)
                        {
                            string db = "";
                            DIENBIEN moi = new DIENBIEN();
                            moi.HOI_CHAN = "";
                            moi.PHAU_THUAT = "";
                            moi.NGAY_YL = item1.Date;
                            foreach (var item in a)
                            {
                                db += item.DienBien1 + ";";
                            }
                            moi.DIEN_BIEN = DungChung.Ham.FreshString(db);
                            _lkq.Add(moi);
                        }
                    }
                }
                else if (bn.NoiTru == 0 && !bn.DTNT && (DungChung.Bien.MaBV == "24012" || DungChung.Bien.MaBV == "24389"))
                {
                    DIENBIEN moi = new DIENBIEN();
                    moi.HOI_CHAN = "";
                    moi.PHAU_THUAT = "";
                    var bnkb = data.BNKBs.Where(p => p.MaBNhan == bn.MaBNhan).ToList();
                    foreach (var item in bnkb)
                    {
                        string db = "";
                        DIENBIEN a = new DIENBIEN();
                        a.HOI_CHAN = "";
                        a.PHAU_THUAT = "";
                        a.NGAY_YL = item.NgayKham ?? DateTime.Now;
                        a.DIEN_BIEN = DungChung.Ham.FreshString(item.ChanDoanBD);
                        _lkq.Add(a);
                    }

                }
                else
                {
                    foreach (var item in _lDienBien)
                    {
                        DIENBIEN moi = new DIENBIEN();
                        moi.DIEN_BIEN = item.DienBien1;
                        moi.HOI_CHAN = "";
                        moi.PHAU_THUAT = "";
                        moi.NGAY_YL = Convert.ToDateTime(item.NgayNhap);
                        _lkq.Add(moi);
                    }
                }
                foreach (var item in _lBBHC)
                {
                    DIENBIEN moi = new DIENBIEN();
                    moi.DIEN_BIEN = item.QTDBDT;
                    moi.HOI_CHAN = item.KetLuan;
                    moi.PHAU_THUAT = item.PPPhauThuat;
                    moi.NGAY_YL = Convert.ToDateTime(item.NgayHC);
                    _lkq.Add(moi);
                }


                //var _LKQ=from a in _lDienBien
                //         join b in _lBBHC on a.MaBNhan equals b.MaBNhan
                //         group new {a,b} by new {a.MaBNhan,a.NgayNhap,b.NgayHC}
                #endregion
                #region get DS Diễn biến
                if (bn != null && rv != null && vp != null)//kiểm tra các trường dữ liệu 
                {
                    int kt = 0;
                    string Ngayylenhsai = "";
                    // var qdv = data.DichVus.ToList();
                    foreach (var a in _lkq.OrderByDescending(p => p.NGAY_YL))
                    {
                        stt++;
                        TheoDoiDienBienLS moi = new TheoDoiDienBienLS();
                        moi.MA_LK = bn.MaBNhan.ToString();
                        moi.STT = stt.ToString();
                        if (string.IsNullOrEmpty(a.DIEN_BIEN))
                        {
                            //kt++;
                            //Ngayylenhsai += a.Ngay_Y_Lenh.ToShortDateString() + ";";
                            moi.DIEN_BIEN = ";";
                        }
                        else
                            moi.DIEN_BIEN = a.DIEN_BIEN;
                        moi.HOI_CHAN = a.HOI_CHAN;
                        moi.PHAU_THUAT = a.PHAU_THUAT;
                        moi.NGAY_YL = a.NGAY_YL.ToString("yyyyMMddHHmm");
                        listDienBien.Add(moi);
                    }
                    //if (kt > 0)
                    //{
                    //    checkData += Ngayylenhsai + " chưa có diễn biến";
                    //    return null;
                    //}
                    return JsonConvert.SerializeObject(listDienBien);
                }
                else return null; ;
                #endregion

            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Lỗi tạo file XML5: " + ex.Message);
                return null;
            }
        }


        private void gridViewChoose_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            var row = (BenhNhanADO)gridViewChoose.GetRow(gridViewChoose.GetRowHandle(e.ListSourceRowIndex));
            if (row != null)
            {
                if (e.IsGetData && e.Column.UnboundType == DevExpress.Data.UnboundColumnType.Object)
                {
                    if (e.Column.FieldName == "GioiTinh")
                        e.Value = row.GTinh == 1 ? "Nam" : "Nữ";
                    else if (e.Column.FieldName == "Tuoi_Str")
                        e.Value = DungChung.Ham.CalculateAgeByDate(row.NgaySinh, row.ThangSinh, row.NamSinh);
                }
            }
        }

        private void gridViewSearch_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            var row = (BenhNhanADO)gridViewSearch.GetRow(gridViewSearch.GetRowHandle(e.ListSourceRowIndex));
            if (row != null)
            {
                if (e.IsGetData && e.Column.UnboundType == DevExpress.Data.UnboundColumnType.Object)
                {
                    if (e.Column.FieldName == "GioiTinh")
                        e.Value = row.GTinh == 1 ? "Nam" : "Nữ";
                    else if (e.Column.FieldName == "Tuoi_Str")
                        e.Value = DungChung.Ham.CalculateAgeByDate(row.NgaySinh, row.ThangSinh, row.NamSinh);
                }
            }
        }

        private void gridViewSend_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            var row = (BenhNhanADO)gridViewSend.GetRow(gridViewSend.GetRowHandle(e.ListSourceRowIndex));
            if (row != null)
            {
                if (e.IsGetData && e.Column.UnboundType == DevExpress.Data.UnboundColumnType.Object)
                {
                    if (e.Column.FieldName == "GioiTinh")
                        e.Value = row.GTinh == 1 ? "Nam" : "Nữ";
                    else if (e.Column.FieldName == "Tuoi_Str")
                        e.Value = DungChung.Ham.CalculateAgeByDate(row.NgaySinh, row.ThangSinh, row.NamSinh);
                }
            }
        }

        private void repositoryItemButtonEdit_Delete_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var row = (BenhNhanADO)gridViewChoose.GetFocusedRow();
            if (row != null)
            {
                listSelecteds.Remove(row);
                if (listAll.FirstOrDefault(o => o.MaBNhan == row.MaBNhan) != null)
                {
                    listAll.FirstOrDefault(o => o.MaBNhan == row.MaBNhan).Check = false;
                }
                gridControlChoose.BeginUpdate();
                gridControlChoose.DataSource = listSelecteds;
                gridControlChoose.EndUpdate();
                txtSearch_KeyDown(null, new KeyEventArgs(Keys.Enter));
            }
        }
        private void repositoryItemButtonEdit_ViewBN_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var row = (BenhNhanADO)gridViewSearch.GetFocusedRow();
            if (row != null)
            {
                FormNhap.frmHSBNNhapMoi frm = new FormNhap.frmHSBNNhapMoi(2, row.MaBNhan, 1);
                frm.ShowDialog();
            }
        }


        public class MyGridLocalizer : GridLocalizer
        {
            public override string GetLocalizedString(GridStringId id)
            {
                switch (id)
                {
                    case GridStringId.FindControlFindButton:
                        return "Tìm kiếm";
                    case GridStringId.FindControlClearButton:
                        return "Xóa";
                    case GridStringId.FilterPanelCustomizeButton:
                        return "Lọc";
                    default:
                        return base.GetLocalizedString(id);
                }
            }
        }

        private void gridViewSearch_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {

        }

        private void txtSearch_EditValueChanged(object sender, EventArgs e)
        {

        }

        public class BenhNhanADO : BenhNhan
        {
            public bool Check { get; set; }
            public string Error { get; set; }
            public bool Is_Send { get; set; }
            public bool  EXPORTV20 { get; set; }
            public BenhNhanADO() { }
            public BenhNhanADO(BenhNhan data)
            {
                LibraryStore.Mapper.DataObjectMapper.Map<BenhNhanADO>(this, data);
            }
        }

        private void gridViewSearch_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            var row = (BenhNhanADO)gridViewSearch.GetRow(e.RowHandle);
            if (row != null && e.Column.FieldName == "Check")
            {
                ChooseRow(e, row);
                gridControlChoose.BeginUpdate();
                gridControlChoose.DataSource = listSelecteds;
                gridControlChoose.EndUpdate();
            }

        }

        private void ChooseRow(DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e, BenhNhanADO row)
        {
            if (listSelecteds.Exists(o => o.MaBNhan == row.MaBNhan))
            {
                if ((bool)e.Value)
                {
                    row.Check = (bool)e.Value;
                    listSelecteds.Add(row);
                }
                else
                {
                    listSelecteds = listSelecteds.Where(o => o.MaBNhan != row.MaBNhan).ToList();
                }
            }
            else
            {
                row.Check = (bool)e.Value;
                listSelecteds.Add(row);
            }
            if (listAll.FirstOrDefault(o => o.MaBNhan == row.MaBNhan) != null)
                listAll.FirstOrDefault(o => o.MaBNhan == row.MaBNhan).Check = (bool)e.Value;
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Search_Condition();
            }
        }

        private void dtTuNgay_EditValueChanged(object sender, EventArgs e)
        {
            LoadDataToForm();
            txtSearch_KeyDown(null, new KeyEventArgs(Keys.Enter));
        }

        private void dtDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            LoadDataToForm();
            txtSearch_KeyDown(null, new KeyEventArgs(Keys.Enter));
        }

        private void GuiTest()
        {
            //lấy danh sách gửi test
            int i = 0;
            foreach (var item in listAll)
            {
                if ((item.PID == null || item.PID == "") && item.Tuoi > 0 && i < 500)
                {
                    listSelecteds.Add(item);
                }
                i++;
            }
            gridControlChoose.BeginUpdate();
            gridControlChoose.DataSource = listSelecteds;
            gridControlChoose.EndUpdate();
        }

        private void gridViewChoose_CustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
        {
            var row = (BenhNhanADO)gridViewChoose.GetRow(e.RowHandle);
            if (row != null)
            {
                if (e.Column.FieldName == "Error")
                {
                    e.RepositoryItem = string.IsNullOrWhiteSpace(row.Error) ? repositoryItemButtonEdit_Error_Disable : repositoryItemButtonEdit_Error_Enable;
                }
            }
        }

        private void repositoryItemButtonEdit_Error_Enable_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var row = (BenhNhanADO)gridViewChoose.GetFocusedRow();
            if (row != null)
            {
                //frm_DS_HSSK_Error frm = new frm_DS_HSSK_Error(row);
               // frm.ShowDialog();
            }
        }

        bool IsCheckAll = false;
        private void gridViewSearch_MouseDown(object sender, MouseEventArgs e)
        {
            if ((Control.ModifierKeys & Keys.Control) != Keys.Control)
            {
                GridView view = sender as GridView;
                GridViewInfo viewInfo = view.GetViewInfo() as GridViewInfo;
                GridHitInfo hi = view.CalcHitInfo(e.Location);

                if (hi.HitTest == GridHitTest.Column)
                {
                    if (hi.Column.FieldName == "Check" || hi.Column.FieldName == "View")
                    {
                        gridControlSearch.BeginUpdate();
                        gridControlChoose.BeginUpdate();
                        if (IsCheckAll)
                        {
                            hi.Column.Image = imageList1.Images[0];
                            var dataSource = (List<BenhNhanADO>)gridControlSearch.DataSource;
                            if (dataSource != null)
                            {
                                foreach (BenhNhanADO row in dataSource)
                                {
                                    if (row.Is_Send) break;
                                    listAll.FirstOrDefault(o => o.MaBNhan == row.MaBNhan).Check = false;
                                    var checkChoose = listSelecteds.FirstOrDefault(o => o.MaBNhan == row.MaBNhan);
                                    if (checkChoose != null)
                                    {
                                        listSelecteds = listSelecteds.Where(o => o.MaBNhan != row.MaBNhan).ToList();
                                    }
                                }
                                gridControlSearch.DataSource = listAll.Where(o => o.Is_Send == chkDaGui.Checked).ToList();
                                IsCheckAll = false;
                            }
                        }
                        else
                        {
                            hi.Column.Image = imageList1.Images[1];
                            var dataSource = (List<BenhNhanADO>)gridControlSearch.DataSource;
                            if (dataSource != null)
                            {
                                foreach (BenhNhanADO row in dataSource)
                                {
                                    if (row.Is_Send) break;
                                    listAll.FirstOrDefault(o => o.MaBNhan == row.MaBNhan).Check = true;
                                    var checkChoose = listSelecteds.FirstOrDefault(o => o.MaBNhan == row.MaBNhan);
                                    if (checkChoose == null)
                                    {
                                        listSelecteds.Add(row);
                                    }
                                }
                                gridControlSearch.DataSource = listAll.Where(o => o.Is_Send == chkDaGui.Checked).ToList();
                                IsCheckAll = true;
                            }
                        }
                        gridControlChoose.DataSource = listSelecteds;
                        gridControlSearch.EndUpdate();
                        gridControlChoose.EndUpdate();
                    }
                }
            }
        }

        private void gridControlSearch_Click(object sender, EventArgs e)
        {

        }

        private void chkDaGui_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDaGui.Checked)
                listAll.ForEach(o => { o.Check = false; });
            listSelecteds = listSelecteds.Where(o => o.Is_Send == chkDaGui.Checked).ToList();
            gridControlChoose.BeginUpdate();
            gridControlChoose.DataSource = listSelecteds;
            gridControlChoose.EndUpdate();
            Search_Condition();
            btnSend.Enabled = !chkDaGui.Checked;
            btnCancel.Enabled = chkDaGui.Checked;
        }

        private void Search_Condition()
        {
            List<BenhNhanADO> search = new List<BenhNhanADO>();

            if (!string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                search = listAll.Where(o => (o.MaBNhan.ToString() == txtSearch.Text || o.TenBNhan.ToLower().Contains(txtSearch.Text.ToLower()) || o.DChi.ToLower().Contains(txtSearch.Text.ToLower())) && (o.Is_Send == chkDaGui.Checked)).ToList();
            }
            else
                search = listAll.Where(o => o.Is_Send == chkDaGui.Checked).ToList();
            gridControlSearch.BeginUpdate();
            gridControlSearch.DataSource = search;
            gridControlSearch.EndUpdate();
        }

        private void gridViewSearch_CustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
        {
            var row = (BenhNhanADO)gridViewSearch.GetRow(e.RowHandle);
            if (row != null)
            {
                if (e.Column.FieldName == "Check")
                {
                    e.RepositoryItem = repositoryItemCheckEdit_Check;
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            var dataSource = (List<BenhNhanADO>)gridControlChoose.DataSource;
            if (dataSource != null && dataSource.Count > 0)
            {
                QLBV_Database.QLBVEntities dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                foreach (var item in dataSource)
                {
                    var vienphi = dataContext.VienPhis.FirstOrDefault(o => o.MaBNhan == item.MaBNhan);
                    if (vienphi != null)
                    {
                        vienphi.ExportBYT = false;
                    }
                   
                }
                if (dataContext.SaveChanges() > 0)
                {
                    MessageBox.Show("Hủy thành công!");
                    listSelecteds = new List<BenhNhanADO>();
                    gridControlChoose.BeginUpdate();
                    gridControlChoose.DataSource = listSelecteds;
                    gridControlChoose.EndUpdate();
                    LoadDataToForm();
                    txtSearch_KeyDown(null, new KeyEventArgs(Keys.Enter));
                }
            }

        }

        private void radioGroupDoiTuong_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDataToForm();
            txtSearch_KeyDown(null, new KeyEventArgs(Keys.Enter));
        }

        private void gridViewSearch_CustomDrawFooterCell(object sender, FooterCellCustomDrawEventArgs e)
        {
            int count = 0;
            if (gridControlSearch.DataSource != null)
            {
                count = ((List<BenhNhanADO>)gridControlSearch.DataSource).Count;
            }
            e.Appearance.DrawString(e.Cache, string.Format("Tổng: {0}", count), e.Bounds);
            e.Handled = true;
        }

        private void gridViewChoose_CustomDrawFooterCell(object sender, FooterCellCustomDrawEventArgs e)
        {
            int count = 0;
            if (gridControlChoose.DataSource != null)
            {
                count = ((List<BenhNhanADO>)gridControlChoose.DataSource).Count;
            }
            e.Appearance.DrawString(e.Cache, string.Format("Tổng: {0}", count), e.Bounds);
            e.Handled = true;
        }

        private void gridViewSend_CustomDrawFooterCell(object sender, FooterCellCustomDrawEventArgs e)
        {
            int count = 0;
            if (gridControlSend.DataSource != null)
            {
                count = ((List<BenhNhanADO>)gridControlSend.DataSource).Count;
            }
            e.Appearance.DrawString(e.Cache, string.Format("Tổng: {0}", count), e.Bounds);
            e.Handled = true;
        }
        public static double _getmuc(int hangBV, string mathe, int tuyen, int noingoaitru, DateTime ngayTT)
        {
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            double _muctt = 0;
            string mamuc = "";

            if (mathe.Length > 2 && ngayTT != null)
            {
                mamuc = mathe.Substring(2, 1);
                var qmuc = data.MucTTs.Where(p => p.MaMuc == mamuc).ToList();
                if (qmuc.Count > 0 && qmuc.First().PTTT != null)
                {

                    // var q = data.MucTTs.(p => p.MaMuc == mamuc).PTTT;
                    if (tuyen == 1) // đúng tuyến
                    {
                        _muctt = Convert.ToDouble(qmuc.First().PTTT.ToString());
                    }
                    else // trái tuyến
                    {
                        double tylevuottuyen = 0;

                        if (noingoaitru == 0)
                        {
                            if (ngayTT >= new DateTime(2015, 1, 1) && ngayTT < new DateTime(2016, 1, 1))
                                switch (hangBV)
                                {
                                    case 3:
                                        tylevuottuyen = 0.7;
                                        break;
                                }
                            else if (ngayTT >= new DateTime(2016, 1, 1))
                            {
                                if (hangBV == 4 || hangBV == 3)
                                    tylevuottuyen = 1;
                            }
                        }
                        else if (noingoaitru == 1) // nội trú
                        {
                            if (ngayTT >= new DateTime(2015, 1, 1) && ngayTT < new DateTime(2016, 1, 1))
                                switch (hangBV)
                                {
                                    case 3:
                                        tylevuottuyen = 0.7;
                                        break;
                                    case 2:
                                        tylevuottuyen = 0.6;
                                        break;
                                    case 1:
                                        tylevuottuyen = 0.4;
                                        break;
                                }
                            else if (ngayTT >= new DateTime(2016, 1, 1) && ngayTT < new DateTime(2021, 1, 1))
                                switch (hangBV)
                                {
                                    case 4:
                                        tylevuottuyen = 1;
                                        break;
                                    case 3:
                                        tylevuottuyen = 1;
                                        break;
                                    case 2:
                                        tylevuottuyen = 0.6;
                                        break;
                                    case 1:
                                        tylevuottuyen = 0.4;
                                        break;
                                }
                            else if (ngayTT >= new DateTime(2021, 1, 1))
                            {
                                switch (hangBV)
                                {
                                    case 4:
                                        tylevuottuyen = 1;
                                        break;
                                    case 3:
                                        tylevuottuyen = 1;
                                        break;
                                    case 2:
                                        tylevuottuyen = 1;
                                        break;
                                    case 1:
                                        tylevuottuyen = 0.4;
                                        break;
                                }
                            }
                        }
                        _muctt = Convert.ToDouble(qmuc.First().PTTT) * tylevuottuyen;
                    }
                }
            }
            return _muctt;
        }

        private void radioGroupDoiTuong_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            LoadDataToForm();
            txtSearch_KeyDown(null, new KeyEventArgs(Keys.Enter));
        }

        private void cbbDoiTuong_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDataToForm();
            txtSearch_KeyDown(null, new KeyEventArgs(Keys.Enter));
        }
    }
    class tongtienXML2_XML3
    {
        public double T_THUOC { set; get; }
        public double T_VTYT { set; get; }
        public double T_TONGCHI { set; get; }
        public double T_BNTT { set; get; }
        public double T_BNCCT { set; get; }
        public double T_NGUONKHAC { set; get; }
        public double T_BHTT { set; get; }
        public double T_NGOAIDS { set; get; }
        public string MA_PTTT_QT { set; get; }
    }
}
